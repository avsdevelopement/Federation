using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClsPLTransfer
/// </summary>
public class ClsPLTransfer
{

    DbConnection conn = new DbConnection();
    string sql = "";
    DataTable DT1 = new DataTable();

	public ClsPLTransfer()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetPLRecord(string Date, string AC, string BRCD, string Flag, string EDAT,string MID,string EMID)
    {
        try
        {
            string[] TD = Date.Split('/');
            sql = "Exec SP_PLTransfer @MID='" + MID + "', @EDAT='" + conn.ConvertDate(EDAT) + "', @Flag='" + Flag + "',@BRCD='" + BRCD + "',@MONTH='" + TD[1].ToString() + "',@YEAR='" + TD[2].ToString() + "',@FromDate='" + conn.ConvertDate(Date) + "',@AC='" + AC + "',@EMID='"+EMID+"'";
            DT1 = new DataTable();
            DT1 = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT1;
    }

    public DataTable GetDIVRecord(string Date, string BRCD, string gl, string subgl, string MID)
    {
        try
        {
            string[] TD = Date.Split('/');
            sql = "Exec Isp_AVS0074 @AsonDate='" + conn.ConvertDate(Date) + "',@BrCd='" + BRCD + "',@GlCode='" + gl + "',@SubGlCode='" + subgl + "',@MID='" + MID + "'";
            DT1 = new DataTable();
            DT1 = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT1;
    }
    public DataTable GetGLCode(string SubGl, string BRCD)
    {
        try
        {
            sql = "select * from glmast where subglcode='" + SubGl + "' and BRCD='" + BRCD + "'";
            DT1 = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT1;
    }
    public string InsertDividend(string Date, string BRCD, string gl, string subgl, string MID, string TBRCD, string ToSubgl,string EDat,string EMID)
    {
        string result = "0";
        try
        {
            sql = "exec ISP_AVS0073 '"+conn.ConvertDate(Date)+"','"+BRCD+"','"+gl+"','"+subgl+"','"+MID+"','"+TBRCD+"','"+ToSubgl+"','"+conn.ConvertDate(EDat)+"','"+EMID+"'";
            result = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
}