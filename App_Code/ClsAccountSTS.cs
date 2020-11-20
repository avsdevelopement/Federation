using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;

public class ClsAccountSTS
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0;

    public ClsAccountSTS()
    {
    }
    public string GetAccType(string AccT, string BRCD)
    {
        try 
        {
            sql = "SELECT GLNAME FROM GLMAST WHERE SUBGLCODE='" + AccT + "' AND BRCD='" + BRCD + "' AND GLCODE IN (2,15,6)";//glcode added by ankita for balvikas on 13/07/2017
            AccT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return AccT;
    }
    public string GetGlname(string AccT, string BRCD)
    {
        try
        {
            sql = "SELECT GLNAME FROM GLMAST WHERE brcd='"+BRCD+"' and subglcode='"+AccT+"'";
            AccT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return AccT;
    }
    public DataTable GetCustName(string AccT, string AccNo, string BRCD)
    {
        DataTable DT = new DataTable();
        try 
        {
        sql = "SELECT M.CUSTNAME,AC.OPENINGDATE,M.CUSTNO FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO  WHERE AC.ACCNO='" + AccNo + "' AND AC.SUBGLCODE='" + AccT + "' AND AC.BRCD='" + BRCD + "'";
        
        DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable AccountStatment(string PFMONTH, string PTMONTH, string PFYEAR, string PTYEAR, string PFDT, string PTDT, string PAC, string PAT, string PMID,string PBRCD, string GL,string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {           
           // sql = "Exec SP_ACCSTATUS @pfmonth='"+PFMONTH+"',@ptmonth='"+PTMONTH+"',@PFDT='"+conn.ConvertDate(PFDT)+"',@PTDT='"+conn.ConvertDate(PTDT)+"',@pfyear='"+PFYEAR+"',@ptyear='"+PTYEAR+"',@pac='"+PAC+"',@pat='"+PAT+"'";
            sql = "Exec SP_ACCSTATUS_R @pfmonth='" + PFMONTH + "',@ptmonth='" + PTMONTH + "',@PFDT='" + conn.ConvertDate(PFDT) + "',@PTDT='" + conn.ConvertDate(PTDT) + "',@pfyear='" + PFYEAR + "',@ptyear='" + PTYEAR + "',@pac='" + PAC + "',@pat='" + PAT + "', @BRCD='" + BRCD + "'";
            DT = new DataTable(sql);
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            return DT = null;
        }        
        return DT;
    }
    public DataTable MonthlyAccStat(string BRCD,string SUBGL,string ACCNO,string FDATE,string TDATE)
    {
        DataTable DT = new DataTable();
        try
        {
            // sql = "Exec SP_ACCSTATUS @pfmonth='"+PFMONTH+"',@ptmonth='"+PTMONTH+"',@PFDT='"+conn.ConvertDate(PFDT)+"',@PTDT='"+conn.ConvertDate(PTDT)+"',@pfyear='"+PFYEAR+"',@ptyear='"+PTYEAR+"',@pac='"+PAC+"',@pat='"+PAT+"'";
            sql = "EXEC monthlystatment @Brcd='"+BRCD+"',@SubGlCode='"+SUBGL+"',@Accno='"+ACCNO+"',@FromDate='"+conn.ConvertDate(FDATE)+"',@ToDate='"+conn.ConvertDate(TDATE)+"'";
            DT = new DataTable(sql);
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            return DT = null;
        }
        return DT;
    }
    public DataTable FDAccountStatment(string PFMONTH, string PTMONTH, string PFYEAR, string PTYEAR, string PFDT, string PTDT, string PAC, string PAT, string PMID, string PBRCD, string GL,string IR)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Exec SP_FDLEDGER @pfmonth='" + PFMONTH + "',@ptmonth='" + PTMONTH + "',@PFDT='" + conn.ConvertDate(PFDT) + "',@PTDT='" + conn.ConvertDate(PTDT) + "',@pfyear='" + PFYEAR + "',@ptyear='" + PTYEAR + "',@pac='" + PAC + "',@pat='" + PAT + "',@ir='" + IR + "'";
            DT = new DataTable(sql);
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            return DT = null;
        }
        return DT;
    }

    public string GetBranchName(string brcd)
    {
        try
        {
            sql = "select MIDNAME from BANKNAME where BRCD='" + brcd + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }

    public string GetAccStatus(string brcd,string ACNO,string SUBGL)
    {
        try
        {
            sql = "select ACC_STATUS from AVS_ACC where ACCNO='" + ACNO + "' and SUBGLCODE='" + SUBGL + "' and BRCD='" + brcd + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }

    public DataTable GetUnClearBal(string PFMONTH, string PTMONTH, string PFYEAR, string PTYEAR, string PFDT, string PTDT, string PAC, string PAT, string PMID, string PBRCD, string GL, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {

            sql = "exec SP_ACCSTATUS_UnClear '" + PFMONTH + "','" + PTMONTH + "','" + conn.ConvertDate(PFDT) + "','" + conn.ConvertDate(PTDT) + "','" + PFYEAR + "','" + PTYEAR + "','" + PAC + "','" + PAT + "','" + BRCD + "'";
            DT = new DataTable(sql);
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            return DT = null;
        }
        return DT;
    }
    public string checkstatus(string brcd, string ACNO, string SUBGL)
    {
        try
        {
            sql = "select PRINT_STATUS from depositinfo where brcd='"+brcd+"' and depositglcode='"+SUBGL+"' and custaccno='"+ACNO+"'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }

    public int InsertTrailSMS(string Mobile, string Date, string Desc, string Status)
    {
        try
        {
            sql = "insert into AVS1092 (Mobile,SMS_Date,SMS_Description,SMS_Status,Systemdate) values ('"+Mobile+"','"+conn.ConvertDate(Date)+"','"+Desc+"','"+Status+"',getdate())";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }

    public string GetBankName()
    {
        string BN = "";
        try
        {
            sql = "select BANKNAME from BANKNAME where BRCD=1";
            BN = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return BN;
    }
    //added by ankita 09/01/2018
    public string GetGoldPrdcd()
    {
        string BN = "";
        try
        {
            sql = " Select top 1 convert(varchar(10),LOANGLCODE)+'_'+LOANTYPE From LoanGl Where  LoanCategory ='LAG'";
            BN = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return BN;
    }
}