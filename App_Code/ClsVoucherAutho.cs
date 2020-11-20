using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsVoucherAutho
{
    DbConnection conn = new DbConnection();
    ClsCommon cmn = new ClsCommon();
    DataTable DT = new DataTable();
    string sql = "", sResult = "";
    int Result = 0;
    double ClBal = 0;

	public ClsVoucherAutho()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string GetAuthoParam(string BrCode)
    {
        try
        {
            sql = "Select ListValue From Parameter Where BrCd = '0' And ListField = 'AUTALL'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
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

    public string GetGlCode(string BrCode, string IntR)
    {
        try
        {
            sql = " Select SubGlCode From GlMast Where BrCd = '" + BrCode + "' And IR = '" + IntR + "'";
            BrCode = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BrCode;
    }

    public double GetClBal(string BrCode, string PrCode, string AccNo, string EDate, string Flag)
    {
        try
        {
            sql = "Exec SP_OpClBalance '" + BrCode + "','" + PrCode + "','" + AccNo + "','" + conn.ConvertDate(EDate).ToString() + "','" + Flag + "'";
            ClBal = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ClBal;
    }

    public string GetAccStatus(string BrCode, string ProdCode, string AccNo)
    {
        try
        {
            sql = " Select Acc_Status From Avs_Acc Where BrCd = '" + BrCode + "' And SubGlCode = '" + ProdCode + "' And AccNo = '" + AccNo + "'";
            BrCode = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BrCode;
    }

    public string GetUserMID(string LoginCode)
    {
        try
        {
            sql = "Select PermissionNo From UserMaster Where LoginCode = '" + LoginCode + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string CheckAdminVoucher(string BrCode, string EDate, string SetNo)
    {
        try
        {
            string[] TD = EDate.Split('/');
            string TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "Select Count(1) From " + TableName + " M With(NoLock) " +
                  "Inner Join GlMast G With(NoLock) On M.BrCd = G.BrCd And M.GlCode = G.GlCode And M.SubGlCode = G.SubGlCode " +
                  "Where M.BrCd = '" + BrCode + "' And M.EntryDate = '" + conn.ConvertDate(EDate) + "' And M.SetNo = '" + SetNo + "' " +
                  "And G.PlGroup In ('AE') ";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    public int GetVoucherInfo(GridView Gview, string sQuery, string BrCode, string EDate)
    {
        try
        {
            string[] TD = EDate.Split('/');
            string TBNAME = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "Select (ConVert(VarChar(10), A.SetNo)+'_'+ ConVert(VarChar(10), A.ScrollNo)+'_'+ ConVert(VarChar(10), A.MID)) As ScrollNo, A.SetNo, A.ScrollNo As ScrollNo1, " +
                      "A.SubGlCode, A.AccNo, A.PartiCulars, " +
                      "(Case When A.TrxType = '1' Then A.Amount Else '0' End) As CREDIT, " +
                      "(Case When A.TrxType = '2' Then A.Amount Else '0' End) As DEBIT, U.LOGINCODE From " + TBNAME + " A With(NoLock) " +
                      "Left Join UserMaster U With(NoLock) On A.MID = U.PERMISSIONNO " +
                      "Where (A.BRCD = '" + BrCode + "' Or A.RefBrCd = '" + BrCode + "') And A.EntryDate = '" + conn.ConvertDate(EDate) + "' " +
                      " " + sQuery + " And A.Stage Not In (1003, 1004) " +
                      "And A.SetNo < 20000 And SubGlCode <> 99 And A.SubGlCode <> 0 And Activity Not In (31, 32) And PmtMode Not In ('IC','OC') " +
                      "Order By EntryDate, SetNo, ScrollNo";
        
            //sql = "SELECT Setno,(CONVERT(varchar(10), setno)+'_'+CONVERT(varchar(10),Scrollno)+'_'+CONVERT(varchar(10),MID)) Scrollno, Scrollno Scrollno1,SUBGLCODE,ACCNO,Particulars,(case when trxtype='1' then AMOUNT else '0' end) CREDIT, "+
            //      "(case when trxtype='2' then AMOUNT else '0' end) DEBIT,U.LOGINCODE From " + TBNAME + " A "+
            //      "LEFT JOIN USERMASTER U ON A.MID=U.Permissionno "+
            //      "WHERE (A.BRCD='" + BRCD + "'  or A.RefBrcd='" + BRCD + "') AND A.STAGE in(1001,1002) " + STR + " And A.SetNo < 20000 AND SUBGLCODE<>99 and A.SUBGLCODE<>0 and ACTIVITY NOT IN (31,32) and PMTMODE NOT IN ('IC','OC') order by Entrydate,setno,scrollno ";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }   
        return Result;
    }

    public DataTable VGetInfo(string BRCD, string ST,string SRNO,string EDT)
    {
        try 
        {
            string[] TD = EDT.Split('/');
            string TBNAME = "AVSM_" + TD[2].ToString() + TD[1].ToString();

        sql = "SELECT M.CUSTNO, A.Setno,A.Scrollno,A.SUBGLCODE,A.ACCNO,A.Particulars,AMOUNT AMT ,A.ENTRYDATE,A.PCMAC,U.LOGINCODE MID,M.CUSTNAME,GL.GLNAME,A.SUBGLCODE FROM "+TBNAME+" A "+
              " LEFT JOIN AVS_ACC AC ON AC.ACCNO=A.ACCNO AND AC.SUBGLCODE=A.SUBGLCODE AND AC.BRCD=A.BRCD "+
              " LEFT JOIN MASTER M ON M.CUSTNO=AC.CUSTNO "+
              " LEFT JOIN USERMASTER U ON A.MID=U.Permissionno AND A.BRCD=U.BRCD "+
              " LEFT JOIN GLMAST GL ON A.GLCODE=GL.GLCODE AND A.SUBGLCODE=GL.SUBGLCODE AND A.BRCD=GL.BRCD WHERE A.BRCD='" + BRCD + "' AND A.SETNO='" + ST + "' AND A.SCROLLNO='"+SRNO+"' AND A.ENTRYDATE='"+conn.ConvertDate(EDT)+"'";
        DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return DT;
    }

    //Added By Amol on 07/11/2017 as per darade sir instruction

    public string GetParameter(string BrCode, string ListField, string Stage)
    {
        try
        {
            sql = "Select ListValue From Parameter Where BrCd = '" + BrCode + "' And ListField = '" + ListField + "' And Stage = '" + Stage + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public string CheckVoucher(string BrCode, string EDate, string VoucherNo)
    {
        try
        {
            sql = "Exec SP_CheckVoucher @BrCode = '" + BrCode + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @SetNo = '" + VoucherNo + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetPAYMAST(string BRCD, string EDT, string SETNO, string ScrollNo)
    {
        try
        {
            sql = "EXEC SP_VOUCHER_AUTHOPROCESS @FLAG='GET_PAYMAST',@SETNO='" + SETNO + "',@EDT='" + conn.ConvertDate(EDT) + "',@BRCD='" + BRCD + "', @ScNo = '" + ScrollNo + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public DataTable GetDetails_ToFill(string SETNO, string BRCD, string EDT,string FL, string ScrollNo)
    {
        try
        {
            sql = "EXEC SP_VOUCHER_AUTHOPROCESS @FLAG='" + FL + "', @SETNO='" + SETNO + "', @EDT='" + conn.ConvertDate(EDT).ToString() + "', @BRCD='" + BRCD + "', @ScNo='" + ScrollNo + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetDetails_ToFill1(string SETNO, string BRCD, string EDT, string FL,string Scroll)
    {
        try
        {

            sql = "EXEC SP_VOUCHER_MultiAUTHOPROCESS @FLAG='" + FL + "', @SETNO='" + SETNO + "', @EDT='" + conn.ConvertDate(EDT).ToString() + "', @BRCD='" + BRCD + "',@Scroll='"+Scroll+"'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetLoanAccDetails(string BrCode, string PrCode, string AccNo)
    {
        try
        {
            sql = "Select Top 1 IntRate, Installment, ConVert(VarChar(10), InstDate, 103) As InstDate, IsNull(RecommAutho, '') As Recommded, IsNull(SancAutho, '') As SancBy " +
                  "From LoanInfo Where BrCd = '" + BrCode + "' And Loanglcode = '" + PrCode + "' and CustAccNo = '" + AccNo + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string Getbankcode()
    {
        try
        {
            sql = "Select Distinct(BankCD) From BankName ";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public DataTable GetTotalSetAmt(string setno, string EntryDate, string BRCD)
    {
        DataTable dt1 = new DataTable();
        string[] TD = EntryDate.Split('/');
        string TBNAME = "AVSM_" + TD[2].ToString() + TD[1].ToString();
        try
        {
            sql = "select sum(amount) amount,M.subglcode,m.accno,g.GLNAME,g1.GLNAME as ACCNAME,M.MID,M.CustNo,m.pcMAC, convert(nvarchar(10),M.entrydate,103) entrydate,ac.ACC_TYPE,m.glcode from " + TBNAME + " M ";
            sql += " inner join glmast g on g.subglcode=m.subglcode and g.brcd=m.brcd ";
            sql += " inner join glmast g1 on g1.subglcode=m.accno and g1.brcd=m.brcd  ";
            sql += " inner join AVS_ACC ac on ac.subglcode=M.subglcode and ac.brcd=M.BRCD and m.accno=ac.accno";
            sql += " where PARTICULARS='Daily Collection' and setno='" + setno + "' ";
            sql += " and M.brcd='" + BRCD + "' and entrydate='" + conn.ConvertDate(EntryDate) + "' and trxtype=2 ";
            sql += "group by M.subglcode,m.accno,g.GLNAME,g1.GLNAME,M.MID,M.CustNo,m.pcMAC, M.entrydate,ac.ACC_TYPE ,m.glcode ";
            dt1 = conn.GetDatatable(sql) ;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt1;
    }

    public string GetTotalLoanAmount(string SetNo, string BRCD, string EntryDate)
    {
        string Result = "";
        DataTable dt1 = new DataTable();
        string[] TD = EntryDate.Split('/');
        string TBNAME = "AVSM_" + TD[2].ToString() + TD[1].ToString();
        try
        {
            sql = "select sum(amount) from "+TBNAME+" where setno='"+SetNo+"' and entrydate='"+conn.ConvertDate(EntryDate)+"' and brcd='"+BRCD+"' and trxtype=1 and PMTMODE not in ('TR-INT','TR_INT')";
            Result = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
    public int DisplayLotUntallySet(GridView Gview, string Flag, string EDate, string FSetNo, string TSetNo, string BrCode, string Mid)//Dhanya Shetty //25/06/2018
    {
        try
        {

             sql = "Exec Isp_AVS0002 @Flag='" + Flag + "',@Edt='" + conn.ConvertDate(EDate) + "',@FSetno='" + FSetNo + "',@TSetno='" + TSetNo + "',@Mid='" + Mid + "',@Brcd='" + BrCode + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
}