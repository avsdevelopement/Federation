using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsLoanReceipt
{
	DbConnection conn = new DbConnection();
    string sql = "", PA = "", instDate = "";
    int Result = 0;
    bool res = true;
    DataTable DTG = new DataTable();

	public ClsLoanReceipt()
	{
		
	}

    public DataTable GetCustName(string GLCODE, string ACCNO, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME+'_'+CONVERT(VARCHAR(10),AC.CUSTNO) CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO AND M.BRCD=AC.BRCD WHERE AC.ACCNO='" + ACCNO + "' AND AC.SUBGLCODE='" + GLCODE + "' AND AC.BRCD='" + BRCD + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string Getaccno(string AT, string BRCD)
    {
        try
        {
            sql = " SELECT (CONVERT (VARCHAR(10),GLCODE))+'_'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }

    public string GetCustNo(string brcd, string SubGlCode, string AccNo)
    {
        string Accname = "";
        try
        {
            string sql = "SELECT B.CUSTNO FROM AVS_ACC A, MASTER B WHERE A.CUSTNO = B.CUSTNO AND A.ACCNO='" + AccNo + "' AND A.SUBGLCODE='" + SubGlCode + "' AND A.BRCD='" + brcd + "'";
            Accname = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Accname;
    }

    public DataTable GetAllFieldData(string SubGlCode, string AccNo, string BrCode, string EDate)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Exec RptLoanDetailsInfo @BrCode = '" + BrCode + "', @SubGlCode = '" + SubGlCode + "', @AccNo = '" + AccNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = 'LDI'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetAllBalData(string BrCode, string SubGlCode, string AccNo, string EDate)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Exec SP_LoanInstBalances @BrCode = '" + BrCode + "', @SGlCode = '" + SubGlCode + "', @AccNo = '" + AccNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetABBSubGl(string BrCode)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select GlCode, SubGlCode From GlMast Where BrCd = '" + BrCode + "' And GlGroup = 'ABB'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetADMSubGl(string BrCode)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select GlCode, SubGlCode From GlMast Where BrCd = '" + BrCode + "' And GlGroup = 'ABB'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetAllTrx(string BrCode, string SubGlCode, string AccNo, string EDate)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Exec SP_GetAllTransaction @BrCode = '" + BrCode + "', @SGlCode = '" + SubGlCode + "', @AccNo = '" + AccNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public bool ChkSheduleExists(string BrCode, string CustNo, string SubGlCode, string AccNo)
    {
        try
        {
            sql = "Select Top 1 1 From LoanSchedule Ls With(NoLock)" +
                  " Where Ls.Brcd = '" + BrCode + "' And Ls.CustNo = '" + CustNo + "' And Ls.LoanglCode = '" + SubGlCode + "' And Ls.CustAccNo = '" + AccNo + "' And Ls.Stage = 1003";
            PA = conn.sExecuteScalar(sql);

            if (PA == null|| PA == "")
                res = false;
            else
                res = true;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return res;
    }

    public string GetDrawPower(string BrCode, string CustNo, string SubGlCode, string AccNo, string EDate)
    {
        try
        {
            sql = "Select Balance From LoanSchedule Ls With(NoLock)" +
                  " Where Ls.Brcd = '" + BrCode + "' And Ls.CustNo = '" + CustNo + "' And Ls.LoanglCode = '" + SubGlCode + "' And Ls.CustAccNo = '" + AccNo + "' And Ls.Stage = 1003" +
                  " And Ls.INSTDATE = (Select Max(INSTDATE) From LOANSCHEDULE L" +
                  "             Where Ls.BrCd = L.BrCd And Ls.CustNo = L.CustNo And Ls.LoanglCode = L.LoanglCode" +
                  "             And Ls.CustAccNo = L.CustAccNo And L.INSTDATE <= '" + conn.ConvertDate(EDate).ToString() + "' And L.Stage = 1003)";
            PA = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return PA;
    }

    public double PrAmt(string BRCD, string sGlCode, string AccNo, string Edate)
    {
        try
        {
            sql = "EXEC SP_FDCLOSURECAL '" + BRCD + "', '" + sGlCode + "','" + AccNo + "','" + conn.ConvertDate(Edate).ToString() + "','PrAmt'";
            PA = conn.sExecuteScalar(sql);
            if (PA == null)
                PA = Convert.ToDouble(0.00).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(PA);
    }

    public double IntAmt(string BRCD, string PT, string AN, string Edate)
    {
        try
        {
            sql = "EXEC SP_FDCLOSURECAL '" + BRCD + "', '" + PT + "','" + AN + "','" + conn.ConvertDate(Edate).ToString() + "','IntAmt'";
            PA = conn.sExecuteScalar(sql);
            if (PA == null)
                PA = Convert.ToDouble(0.00).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(PA);
    }

    public double PenalIntAmt(string BRCD, string PT, string AN, string Edate)
    {
        try
        {
            sql = "EXEC SP_FDCLOSURECAL '" + BRCD + "', '" + PT + "','" + AN + "','" + conn.ConvertDate(Edate).ToString() + "','PIntAmt'";
            PA = conn.sExecuteScalar(sql);
            if (PA == null)
                PA = Convert.ToDouble(0.00).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(PA);
    }

    public double BalAmt(string BRCD, string PT, string AN, string Edate)
    {
        try
        {
            sql = "EXEC SP_FDCLOSURECAL '" + BRCD + "', '" + PT + "','" + AN + "','" + conn.ConvertDate(Edate).ToString() + "','Bal'";
            PA = conn.sExecuteScalar(sql);
            if (PA == null)
                PA = Convert.ToDouble(0.00).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(PA == "" ? "0.00" : PA);
    }

    public double GetOpenClose(string Brcode, string ProdCode, string AccNo, string EDate, string Flag)
    {
        double BAL = 0;
        try
        {
            sql = "Exec SP_OpClBalance @BrCode = '" + Brcode + "', @SubGlCode = '" + ProdCode + "', @AccNo = '" + AccNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = '" + Flag + "'";
            BAL = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BAL;
    }

    public int CreateTable(string TABLENAME)
    {
        try
        {
            sql = "CREATE TABLE " + TABLENAME + "(ID INT IDENTITY(1,1), BRCD Varchar(05), SUBGLCODE VarChar(10), ACCNO VarChar(16), PARTICULARS NVarChar(100), AMOUNT Numeric(18, 2), TRXTYPE Int, ACTIVITY Int, PMTMODE VarChar(15), STAGE VarChar(4), LOGINCODE VarChar(10), MID VarChar(5), CID VarChar(5), VID VarChar(5), ENTRYDATE DATETIME)";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GETDATA(string BRCD, string GLCODE, string SGLCODE)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT GLCODE, SUBGLCODE, PLACCNO, IR, IOR FROM GLMAST WITH(NOLOCK) WHERE BRCD = '" + BRCD + "' AND GLCODE = '" + GLCODE + "' AND SUBGLCODE = '" + SGLCODE + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int GetIntCal(string BrCode, string SGlCode)
    {
        try
        {
            sql = "Select IntCalType From LoanGl Where Brcd = '" + BrCode + "' And LoanGlCode='" + SGlCode + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetIntApp(string BrCode, string SGlCode)
    {
        try
        {
            sql = "Select Int_App From LoanGl Where Brcd = '" + BrCode + "' And LoanGlCode='" + SGlCode + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string CheckStaff(string BrCode, string PrCode)
    {
        try
        {
            sql = "Select LoanCategory From LoanGl Where BrCd = '" + BrCode + "' And LoanGlCode = '" + PrCode + "'";
            PA = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return PA;
    }

    public int UpdateLastIntDate(string BrCode, string SGlCode, string AccNo, string EDate, string Mid)
    {
        try
        {
            sql = "Update LoanInfo Set Prev_IntDt = LastIntDate, LastIntDate = '" + conn.ConvertDate(EDate).ToString() + "', Mod_Date = '" + conn.ConvertDate(EDate).ToString() + "' Where BrCd= '" + BrCode + "' And LOANGLCODE = '" + SGlCode + "' And CUSTACCNO = '" + AccNo + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int CloseLoanAcc(string BRCD, string SubglCode, string AccNo, string EDate, string Mid)
    {
        try
        {
            sql = "Update LoanInfo Set LmStatus = 99, Prev_IntDt = LastIntDate, LastIntDate = '" + conn.ConvertDate(EDate).ToString() + "', Stage = 1003, Vid = '" + Mid + "', Mod_Date = '" + conn.ConvertDate(EDate).ToString() + "' Where BRCD = '" + BRCD + "' AND LOANGLCODE = '" + SubglCode + "' AND CUSTACCNO = '" + AccNo + "'";
            Result = conn.sExecuteQuery(sql);

            if (Convert.ToInt32(Result) > 0)
            {
                sql = "Update Avs_Acc Set Acc_status = 3, Stage = 1003, LastIntDt = '" + conn.ConvertDate(EDate).ToString() + "', CLOSINGDATE = '" + conn.ConvertDate(EDate).ToString() + "', Vid = '" + Mid + "' Where BRCD = '" + BRCD + "' AND SUBGLCODE = '" + SubglCode + "' AND ACCNO = '" + AccNo + "'";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string GetAccOpenDate(string BrCode, string CustNo, string SubGlCode, string AccNo)
    {
        try
        {
            sql = "Select Convert(VarChar(10), OpeningDate, 103) From Avs_Acc Where Brcd = '" + BrCode + "' And CustNo = '" + CustNo + "' And SubGlCode = '" + SubGlCode + "' And AccNo = '" + AccNo + "'";
            PA = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return PA;
    }

    public DataTable GetLoanSchedule(string FL, string SBGL ,string ACCNO, string BRCD)
    {
        try
        {
            sql = "EXEC SP_REPORT_LOANSCHEDULE @FLAG='" + FL + "',@SBGL='" + SBGL + "',@ACCNO='" + ACCNO + "',@BRCD='" + BRCD + "'";
            DTG = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DTG;
    }

    public DataTable FetchNomineeDetails(string CustNO, string ACCno, string BRCD)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "select  ISNULL(S_NAME,'')+' '+ISNULL(SF_NAME,'')+' '+ISNULL(SL_NAME,'') as Nominee from SURITY where loanACCNo='118' AND LoanGlcode='" + ACCno + "' and BRCD='" + BRCD + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public DataTable FetchLoanDetails(string CustNO, string ACCno, string BRCD,string GLCode)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "select * from loaninfo where CustACCno='" + ACCno + "' and BRCD='" + BRCD + "' and Custno='" + CustNO + "' and loanglcode='"+GLCode+"'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public DataTable FetchOpeningDate(string CustNO, string ACCno, string BRCD, string GLCode)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "SELECT MOBILE, CONVERT(NVARCHAR(10),OPENINGDATE,103) AS OD FROM MASTER WHERE CUSTNO='" + CustNO + "' AND CUSTACCNO='" + ACCno + "' AND GLCODE='" + GLCode + "' AND BRCD='" + BRCD + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public DataTable GetGSTFlag()
    {
        try
        {
            sql = "Select SrNumber, GSTFlag From AVS_LnTrx_Head Order By SrNumber";
            DTG = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DTG;
    }

    public DataTable GstDetails(string BrCode)
    {
        DTG = new DataTable();
        try
        {
            sql = "Select A.GST, G.GlCode As GSTGlCode, A.PrdCd, G.GlName, A.SGST, G2.GlCode As SGSTGlCode, A.SGSTPrdCd, G2.GlName As SGSTGlName, " +
                  "A.CGST, G1.GlCode As CGSTGlCode, A.CGSTPrdCd, G1.GlName As CGSTGlName From GstMaster A " +
                  "Inner Join GlMast G On A.BrCd = G.BrCd And A.PrdCd = G.SubGlCode " +
                  "Inner Join GlMast G1 On A.BrCd = G1.BrCd And A.CGSTPrdCd = G1.SubGlCode " +
                  "Inner Join GlMast G2 On A.BrCd = G2.BrCd And A.SGSTPrdCd = G2.SubGlCode " +
                  "Where A.BrCd = '" + BrCode + "' And A.Stage <> 1004 ";
            DTG = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DTG;
    }

}