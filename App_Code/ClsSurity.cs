using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsSurity
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", Res = "";
    int Result = 0;

	public ClsSurity()
	{
		
	}

    public string GetProdName(string AT, string BRCD, string GLCD)
    {
        try
        {
            sql = " SELECT (CONVERT(VARCHAR(10),MAX(LASTNO)+1))+'-'+(CONVERT (VARCHAR(10),GLCODE))+'-'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }

    public string ChkExists(string BrCd, string MemberNo)
    {
        try
        {
            sql = "Select 1 From AVSLnSurityTable Where MemberNo = '" + MemberNo + "'";
            Res = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }

    public DataTable GetMemName(string MemberNo, string BRCD)
    {
        try
        {
            //  Commented by amol on 04/01/2019 (bcoz show wrong custname )
            //sql = "Select CustNo From avs_acc Where BrCd = '1' And GlCode = '4' And AccNo = '" + MemberNo + "' And Stage = '1003'";

            sql = "Select CustNo From avs_acc Where BrCd = '1' And SubGlCode = '4' And AccNo = '" + MemberNo + "' And Stage = '1003'";
            Res = conn.sExecuteScalar(sql);

            if (Res != null)
            {
                sql = "Select A.AddType, M.CustName, A.FLAT_ROOMNO, A.SOCIETY_NAME, A.STREET_SECTOR, A.CITY, A.PINCODE, D.Mobile1, M.CUSTNO From Master M " +
                      "LEFT Join AddMast A With(NoLock) On A.CustNo = M.CustNo AND A.ADDTYPE In ('1', '2', '3')" +
                      "AND A.SrNo = (Select Max(SrNo) From AddMast Where ADDTYPE In ('1', '2', '3') AND CustNo = A.CustNo) " +
                      "Left Join Avs_ContactD D on  D.CustNo = M.CustNo " +
                      "Where M.CustNo = '" + Res + "'";

                //commented by prasad because of no customer addess type present as addtype =3
                //sql = "Select M.CustName, A.FLAT_ROOMNO, A.SOCIETY_NAME, A.STREET_SECTOR, A.CITY, A.PINCODE, D.Mobile1, M.CUSTNO From Master M " +
                //      "LEFT Join AddMast A With(NoLock) On A.CustNo = M.CustNo AND A.ADDTYPE='3'" +
                //      "AND A.SrNo = (Select Max(SrNo) From AddMast Where ADDTYPE='3' AND CustNo = A.CustNo) " +
                //      "Left Join Avs_contactD D on  D.CustNo = M.CustNo "+
                //      "Where M.CustNo = '" + Res + "'";
                DT = conn.GetDatatable(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetMemName1(string MemberNo, string BRCD)
    {
        try
        {

            sql = "Select M.CustName, A.FLAT_ROOMNO, A.SOCIETY_NAME, A.STREET_SECTOR, A.CITY, A.PINCODE,D.Mobile1 From Master M " +
                  "LEFT Join AddMast A With(NoLock) On A.CustNo = M.CustNo AND A.ADDTYPE='3'" +
                  "AND A.SrNo = (Select Max(SrNo) From AddMast Where ADDTYPE='3' AND CustNo = A.CustNo) " +
                  "Left Join Avs_contactD D on  D.CustNo = M.CustNo " +
                  "Where M.CustNo = '" + MemberNo + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string GetMaxSrNo(string BrCode)
    {
        string SRNO = "0";
        Result = 0;

        try
        {
            sql = "SELECT MAX(ISNULL(A.SRNO, 0)) + 1 AS SRNO FROM (SELECT MAX(ISNULL(LnSrNo, 0)) AS SRNO FROM AVSLnSurityTable)A";
            SRNO = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return SRNO.ToString();
    }

    public string GetStage(string MemberNo)
    {
        try
        {
            sql = "Select Stage From Avs_Acc Where BrCd = 1 And SubGlCode = 4 And AccNo = '" + MemberNo + "'";
            Res = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }

    public DataTable GetCustName(string GLCODE, string ACCNO, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME+'_'+CONVERT(VARCHAR(10),AC.CUSTNO) CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO WHERE AC.ACCNO='" + ACCNO + "' AND AC.SUBGLCODE='" + GLCODE + "' AND AC.BRCD='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string InsertData(string BrCode, string PrCode, string AccCode, string CustNo, string LoanType, string SrNum, string SrName, string MemType, string MemNumber, string ProfCode, string EmpNumber, string BussCode, string BussName, string TieUp, string EmpCode, string EmpName, string txtDesgNo, string DesgName, string Address1, string Address2, string City, string PinCode, string MobNo, string MonthlyInc, string Status, string EDate, string MId,string surity_custno)
    {
        string SRNO = "0";
        Result = 0;
        
        try
        {
            //sql = "SELECT MAX(ISNULL(A.SRNO, 0)) + 1 AS SRNO FROM (SELECT MAX(ISNULL(LnSrNo, 0)) AS SRNO FROM AVSLnSurityTable)A";
            //SRNO = conn.sExecuteScalar(sql);   ////commented by ankita on 04/08/2017 to add srno as per accno suggested by darade sir

            sql = "Exec RptLoanSurity '" + BrCode + "','" + PrCode + "','" + AccCode + "'," + CustNo + ",'" + LoanType + "'," + SrNum + ",'" + SrName + "','" + MemType + "'," + MemNumber + ",'Y','" + ProfCode + "','" + EmpNumber + "','" + BussCode + "','" + BussName + "','" + EmpCode + "','" + EmpName + "','" + TieUp + "','" + txtDesgNo + "','" + DesgName + "','" + Address1 + "','" + Address2 + "','" + City + "','" + PinCode + "','" + MobNo + "','" + MonthlyInc + "','" + Status + "','" + MId + "','" + conn.PCNAME().ToString() + "','" + conn.ConvertDate(EDate).ToString() + "','N','"+surity_custno+"','AD'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return (SrNum + '_' + Result).ToString();
    }

    public void DeleteSurity(string GlCode,string AccNo,string MemNo,string BRCD)
    {
        try
        {
            sql = "Update AVSLnSurityTable set stage=1004 where subglcode='" + GlCode + "' " +
                  "And brcd='" + BRCD + "' and accno='" + AccNo + "' and MemberNo='" + MemNo + "' and LnType In ('Surity', 'Surety')";
            conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public int ModifyData(string BrCode, string PrCode, string AccCode, string CustNo, string LoanType, string SrNo, string SrName, string MemType, string MemNumber, string ProfCode, string EmpNumber, string BussCode, string BussName, string TieUp, string EmpCode, string EmpName, string txtDesgNo, string DesgName, string Address1, string Address2, string City, string PinCode, string MobNo, string MonthlyInc, string Status, string MId, string surity_custno)
    {
        try
        {
            sql = "Exec RptLoanSurity '" + BrCode + "','" + PrCode + "','" + AccCode + "'," + CustNo + ",'" + LoanType + "'," + SrNo + ",'" + SrName + "','" + MemType + "'," + MemNumber + ",'Y','" + ProfCode + "','" + EmpNumber + "','" + BussCode + "','" + BussName + "','" + EmpCode + "','" + EmpName + "','" + TieUp + "','" + txtDesgNo + "','" + DesgName + "','" + Address1 + "','" + Address2 + "','" + City + "','" + PinCode + "','" + MobNo + "','" + MonthlyInc + "','" + Status + "','" + MId + "','" + conn.PCNAME().ToString() + "','','N','" + surity_custno + "','MD'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DeleteData(string BrCode, string PrCode, string AccCode, string SrNo, string Mid)
    {
        try
        {
            sql = "Exec RptLoanSurity @BRCD= '" + BrCode + "', @SubGlCode='" + PrCode + "', @AccNo='" + AccCode + "', @DeleteFlag = 'Y', @LnSrNo='" + SrNo + "', @MID = '" + Mid + "', @Flag='DL'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int AuthoriseData(string BrCode, string PrCode, string AccCode, string SrNo, string Mid)
    {
        try
        {
            sql = "Exec RptLoanSurity @BRCD= '" + BrCode + "', @SubGlCode='" + PrCode + "', @AccNo='" + AccCode + "', @LnSrNo='" + SrNo + "', @MID = '" + Mid + "', @Flag='AT'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int BindGrid(GridView Gview, string BRCD, string accno, string subgl)//added accno and subgl by ankita 21/08/2017 as suggested by darade sir
    {
        try
        {
            sql = "Select Convert(VarChar(10),BRCD)+'_'+Convert(VarChar(10),SubGlCode)+'_'+Convert(VarChar(10),AccNo)+'_'+Convert(VarChar(10),LnSrNo) as id, BRCD, SubGlCode, AccNo, CustNo, LnType, LnSrNo, LnSrName From AVSLnSurityTable Where Stage <> '1004' and BRCD='" + BRCD + "' and AccNo='" + accno + "' and SubGlCode='" + subgl + "'";//ADDED BRCD BY ANKITA 18/08/2017 BY SUGGESTION OF GAURI AND AMBIKA MAM
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int BindGrid1(GridView Gview, string BRCD, string MemberNo)
    {
        try
        {
            sql = "Select Convert(varchar(10),BRCD)+'_'+Convert(varchar(10),SubGlCode)+'_'+Convert(varchar(10),AccNo)+'_'+Convert(varchar(10),LnSrNo) as id, BRCD, SubGlCode, AccNo, CustNo, LnType, LnSrNo, LnSrName From AVSLnSurityTable Where MemberNo = '" + MemberNo + "' and BRCD='" + BRCD + "'";//ADDED BRCD BY ANKITA 18/08/2017 BY SUGGESTION OF GAURI AND AMBIKA MAM";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int BindGrid2(GridView Gview, string BRCD, string accno,string subgl)
    {
        try
        {
            sql = "Select Convert(varchar(10),BRCD)+'_'+Convert(varchar(10),SubGlCode)+'_'+Convert(varchar(10),AccNo)+'_'+Convert(varchar(10),LnSrNo) as id, BRCD, SubGlCode, AccNo, CustNo, LnType, LnSrNo, LnSrName From AVSLnSurityTable Where brcd = '" + BRCD + "' and AccNo='" + accno + "' and SubGlCode='"+subgl+"' and stage <> '1004'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public DataTable GetInfo(string BRCD, string PrCode, string AccNo, string SrNo)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Exec RptLoanSurity @BRCD= '" + BRCD + "', @SubGlCode='" + PrCode + "', @AccNo='" + AccNo + "', @LnSrNo='" + SrNo + "', @Flag='VW'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }

    public int CheckMid(string BRCD, string PrCode, string AccNo, string SrNo)
    {
        try
        {
            sql = "SELECT MID FROM AVSLnSurityTable WHERE BRCD = '" + BRCD + "' AND SubGlCode = '" + PrCode + "' AND AccNo = '" + AccNo + "' AND LnSrNo = '" + SrNo + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return Result = 0;
        }
        return Result;
    }
    public string GetMaxSrNoSL(string BrCode,string accno,string prdcd,string type)
    {
        string SRNO = "0";
        Result = 0;

        try
        {
            sql = "SELECT isnull(MAX(LnSrNo),0)+1 AS SRNO FROM AVSLnSurityTable where LnType='" + type + "' and SubGlCode='" + prdcd + "' and AccNo='" + accno + "' and BRCD='" + BrCode + "' and Stage<>1004";
            SRNO = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return SRNO.ToString();
    }
    public string GetCustWiseInfo(string BRCD, string PrCode, string custno)
    {
        try
        {
            sql = "select distinct(AccNo) from AVSLnSurityTable where custno='" + custno + "' and brcd='" + BRCD + "' and SubGlCode='" + PrCode + "'";
            Res = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return Res;
    }
    public string getmemno(string custno)
    {
        try
        {
            sql = "select ACCNO from AVS_ACC where custno='" + custno + "' AND SubGlCode='4' AND BRCD=1";
            Res = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return Res;
    }
}