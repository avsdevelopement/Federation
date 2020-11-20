using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClsGetRiskdetails
/// </summary>
public class ClsGetRiskdetails
{
    DbConnection DBconn = new DbConnection();
    string sql = "";
    int Result = 0;
    DataTable DT = new DataTable();

	public ClsGetRiskdetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetRiskType(string FL)
    {
        try
        {
            sql = "Exec RptRisktypeDetails @RISKTYPE='" + FL + "'";
            DT = new DataTable();
            DT = DBconn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;

    }
}