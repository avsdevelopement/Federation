using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsOpenClosedActivity
/// </summary>
public class ClsOpenClosedActivity
{
    string sql = "";
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

	public ClsOpenClosedActivity()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetOpenClosed(string BRCD)
    {
        try
        {
            sql = "Select ACTIVITY,BRCD,DESCRIPTION,STATUS,REASON From DayClosedActivity WHERE BRCD = '" + BRCD + "' order by ACTIVITY";
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
}