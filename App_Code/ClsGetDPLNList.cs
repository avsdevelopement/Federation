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
/// Summary description for ClsGetDPLNList
/// </summary>
public class ClsGetDPLNList
{
    string sql = "";
    int Result;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

	public ClsGetDPLNList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable DPLNAcclist(string BranchID, string FDT, string FL, string FLT)
    {
        try
        {
            sql = "Exec RptClassificationDPLN @BRCD='" + BranchID + "' , @AsonDate='" + conn.ConvertDate(FDT) + "',@AccType='" + FL + "',@Type='" + FLT + "'";
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