using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CLsCustNoChanges
/// </summary>
public class CLsCustNoChanges
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0;
	public CLsCustNoChanges()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string GetAccType(string AccT, string BRCD)
    {
        try
        {
            sql = "SELECT GLNAME FROM GLMAST WHERE SUBGLCODE='" + AccT + "' AND BRCD='" + BRCD + "'";
            AccT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);

        }
        return AccT;
    }
    public string GetCustNAme(string custno, string BRCD)
    {
         string  AccT="";
        try
        {
            sql = "select custname from master where brcd='" + BRCD + "' and custno='" + custno + "'";
             AccT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);

        }
        return AccT;
    }
    public DataTable GetAccName(string AccT, string AccNo, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME,AC.OPENINGDATE FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO AND M.BRCD=AC.BRCD WHERE AC.ACCNO='" + AccNo + "' AND AC.SUBGLCODE='" + AccT + "' AND AC.BRCD='" + BRCD + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public string Getstage1(string CNO, string BRCD)
    {
        string RS = "";
        sql = "SELECT STAGE FROM MASTER WHERE BRCD='"+BRCD+"' AND CUSTNO='"+CNO+"'";
        RS = conn.sExecuteScalar(sql);
        return RS;
    }
    public DataTable GETCUSTGRID(string CUSTNO, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    
    }
    public DataTable GETGRIDCUST(string CUSTNO, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "EXEC SP_CUSTNOCHANGES 'CUSTNO','" + BRCD + "','','" + CUSTNO + "' ";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GETGRIDACCNO(string CUSTNO, string BRCD,string SUBGLCODE)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "EXEC SP_CUSTNOCHANGES 'SUBGLCODE','" + BRCD + "','"+SUBGLCODE+"','" + CUSTNO + "' ";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public int UPDATECUSTNO(string BRCD,string CUSTNO, string ACCTYPE, string ACCNO, string NEWCUSTNO)
    {
        int Result = 0;
        try
        {
            sql = "EXEC SP_UPDATECUSTNO '" + BRCD + "','" + CUSTNO + "','" + ACCTYPE + "','" + ACCNO + "','" + NEWCUSTNO + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int UpdateAccnoDT(string BRCD, string ACCTYPE, string ACCNO, string NEWACCNO)
    {
        int Result = 0;
        try
        {
            sql = "EXEC SP_UpdateAccnoChange @Brcd='" + BRCD + "',@SubGlCode='" + ACCTYPE + "',@AccNo='" + ACCNO + "',@NewAccNo='" + NEWACCNO + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    
}