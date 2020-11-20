using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsGrtClgMemoDetails
/// </summary>
public class ClsGrtClgMemoDetails
{
    string sql = "";
    int Result;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

	public ClsGrtClgMemoDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetClgMemo(string BRCD, string Ason)
    {
        try
        {
            sql = "Exec RptClearngMemoList '" + BRCD + "' , '" + Ason + "' ";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}