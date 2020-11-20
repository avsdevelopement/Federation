using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsGLreport
/// </summary>
public class ClsGLreport
{
    string sql = "";
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sresult = "";

	public ClsGLreport()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable getglreport(string FL,string BRCD)
    {
        try 
        {
            sql = "SELECT GLCODE,SUBGLCODE,GLGROUP,GLNAME,LASTNO FROM GLMAST WHERE BRCD = '" + BRCD + "' Order By GLCODE,SUBGLCODE,GLGROUP";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return DT;

    }
    public DataTable GetReport(string FDAT, string EDAT, string SGL, string BRCD, int Amount, string Accno, int trxtype, int activity, string flag)
    {
        string Amt = Convert.ToString(Amount);
        string Type = "";
        if (trxtype == 1)
            Type = "CR";
        else if (trxtype == 2)
            Type = "DR";
        try
        {
            string[] FDT=FDAT.Split('/');
            string[] TDT=EDAT.Split('/');
            sql = "EXEC TRANSREPORT @pfmonth='" + FDT[1].ToString() + "',@ptmonth='" + TDT[1].ToString() + "',@PFDT='" + conn.ConvertDate(FDAT) + "',@PTDT='" + conn.ConvertDate(EDAT) + "',@pfyear='" + FDT[2].ToString() + "',@ptyear='" + TDT[2].ToString() + "'";
            sql += " ,@SUBGLCODE='" + @SGL + "',@BRCD='" + @BRCD + "', @AMOUNT='" + Convert.ToString(Amt)+".00"+"',@ACCNO='" + @Accno + "',@TYPE='" + Type + "',@Activity=" + activity + ",@FLAG='" + flag + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public DataTable RptDBRegDetails_Mamco(string Edate, string Productcode, string BRCD, string Amt, string FACCNO, string TACCNO, string FL, string Accno)
    {
        try
        {
            sql = "EXEC RptDayBookRegDetails_Mamco @AsonDate ='" + conn.ConvertDate(Edate) + "',@SUBGLCODE='" + Productcode + "',@BRCD='" + BRCD + "', @AMOUNT='" + Amt + "',@FromAccno='" + FACCNO + "',@ToAccno='" + TACCNO + "',@FLAG='" + FL + "',@UserID='" + Accno + "' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public DataTable RptdailyAgentColl_DT(string BRCD, string FL, string Prd, string FD, string FLT, string GL)
    {
        try
        {
            sql = "EXEC RptDailyAgentCollection @Brcd ='" + BRCD + "', @Month ='" + FD + "',@Subgl='" + Prd + "', @Year='" + FLT + "', @Agent='" + FL + "' ,@Flag='" + GL + "' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    
    public string getGSTNO(string BRCD)
    {
        try
        {
            sql = "SELECT LISTVALUE FROM Parameter Where LISTFIELD='GSTNO' And BRCD = '" + BRCD + "' ";
            sresult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return sresult ;

    }
}