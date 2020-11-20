using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public class ClsRegisterReport
{
    DbConnection conn = new DbConnection();
    string sql = "", sResult = "";
    int Result;
    public ClsRegisterReport()
    {
    }
    public DataTable GetInfo(string BRCD, string SGLCD)
    {
        DataTable DT = new DataTable();
        try 
        {
        sql = "select Distinct loanglcode,custno,custaccno,limit,duedate,intrate,sanssiondate,installment,period from loaninfo where loanglcode='" + SGLCD + "' and brcd='" + BRCD + "'";
       
        DT = conn.GetDatatable(sql);
        if (DT.Rows.Count > 0)
        {
            sql = "truncate table TREGISTER";
            Result = conn.sExecuteQuery(sql);
        }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return DT;
    }

    public string GetCategory(string BrCode, string SubGlCode)
    {
        try
        {
            sql = "Select IsNull(Category, '') From DepositGl Where BrCd = '" + BrCode + "' and DepositGlCode = '" + SubGlCode + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return sResult = "";
        }
        return sResult;
    }

    public int InsertData(string SGL, string Custno, string accno, string limit, string duedate, string Rate, string sanssion, string install, string period, string balance)
    {
        try 
        {
        sql = "insert into TRegister (Subglcode,custno,accno,limit,duedate,intrate,sanssiondate,installment,period,balance) " +
        "values('" + SGL + "','" + Custno + "','" + accno + "','" + limit + "','" + duedate + "','" + Rate + "','" + sanssion + "','" + install + "','" + period + "','" + balance + "')";
        Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }
    public int BindADD(GridView Gview,string Ptype,string BRCD,string Tdate)
    {
        try 
        {
        string sql ="exec SP_LOANDEPOREG @FLAG ='LOAN',@SUBGLCODE ='"+Ptype+"',@BRCD ='"+BRCD+"',@TDATE ='"+conn.ConvertDate(Tdate)+"'";//"SELECT * FROM TREGISTER";

        Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }

    public DataTable DepositInfo(string BRCD, string SGLCD)
    {
        DataTable DT = new DataTable();
        try 
        {
            sql = "select distinct custno,custaccno,DEPOSITGLCODE,PRNAMT,RATEOFINT,OPENINGDATE,DUEDATE,PERIOD,INTAMT,MATURITYAMT from DEPOSITINFO WHERE DEPOSITGLCODE='" + SGLCD + "' and brcd='" + BRCD + "' and lmstatus=1";
       
        DT = conn.GetDatatable(sql);
        if (DT.Rows.Count > 0)
        {
            sql = "truncate table TDeposit";
            Result = conn.sExecuteQuery(sql);
        }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return DT;
    }

    public int insertdeposit(string Custno, string Custaccno, string depositglcode, string prnamt, string rateofint, string openingdate, string duedate, string period, string intamt, string maturityamt, string balance)
    {
        try 
        {
        sql = "insert into TDeposit(custno,custaccno,depositglcode,prnamt,rateofint,openingdate,duedate,period,intamt,maturityamt,balance)" +
            "values('" + Custno + "','" + Custaccno + "','" + depositglcode + "','" + prnamt + "','" + rateofint + "','" + openingdate + "','" + duedate + "','" + period + "','" + intamt + "','" + maturityamt + "','" + balance + "')";
        Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }
    public int BindDepo(GridView gview,string Ptype,string BRCD,string Tdate)
    {
        try 
        {
        string sql ="exec SP_LOANDEPOREG @FLAG ='DEPOSIT',@SUBGLCODE ='"+Ptype+"',@BRCD ='"+BRCD+"',@TDATE ='"+conn.ConvertDate(Tdate)+"'"; //"select * from TDeposit";
        Result = conn.sBindGrid(gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }
    public DataTable GetReport(string FL, string Ptype, string BRCD, string Tdate)
    {
        DataTable DT = new DataTable();
        try 
        {
        if (FL == "LN")
        {
            sql = "exec SP_LOANDEPOREG @FLAG ='LOAN',@SUBGLCODE ='" + Ptype + "',@BRCD ='" + BRCD + "',@TDATE ='" + conn.ConvertDate(Tdate) + "'"; 
        }
        else if (FL == "DP")
        {
            sql = "exec SP_LOANDEPOREG @FLAG ='DEPOSIT',@SUBGLCODE ='" + Ptype + "',@BRCD ='" + BRCD + "',@TDATE ='" + conn.ConvertDate(Tdate) + "'";
        }
        DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return DT;
    }
    public DataTable GetLoanDeposR_EMPDT (string FL, string Ptype, string BRCD, string Tdate)
    {
        DataTable DT = new DataTable();
        try 
        {
        if (FL == "LN")
        {
            sql = "exec SP_LOANDEPOREG_EMP @FLAG ='LOAN',@SUBGLCODE ='" + Ptype + "',@BRCD ='" + BRCD + "',@TDATE ='" + conn.ConvertDate(Tdate) + "'"; 
        }
        else if (FL == "DP")
        {
            sql = "exec SP_LOANDEPOREG_EMP @FLAG ='DEPOSIT',@SUBGLCODE ='" + Ptype + "',@BRCD ='" + BRCD + "',@TDATE ='" + conn.ConvertDate(Tdate) + "'";
        }
        DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return DT;
    }
    
    public DataTable GetDDSR(string Ptype, string BRCD, string Tdate)//Amruta 15/04/2017
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "exec SP_Closing @BRCD ='" + BRCD + "',@FromDate ='" + conn.ConvertDate(Tdate) + "',@ToDate ='" + conn.ConvertDate(Tdate) + "', @Gl='" + Ptype + "'"; ;
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }

    public int BindDDS(GridView gview,string Glcode, string BRCD, string Tdate)
    {
        try
        {
            string sql = "exec SP_Closing @BRCD ='" + BRCD + "',@FromDate ='" + conn.ConvertDate(Tdate) + "',@ToDate ='" + conn.ConvertDate(Tdate) + "', @Gl='"+Glcode+"'"; //"select * from TDeposit";
            Result = conn.sBindGrid(gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return Result;
    }
}