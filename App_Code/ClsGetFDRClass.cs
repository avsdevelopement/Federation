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
/// Summary description for ClsGetFDRClass
/// </summary>
public class ClsGetFDRClass
{
    string sql = "";
    int Result;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

	public ClsGetFDRClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetFDClassDetails(string FPT, string TPT, string FDT, string TDT, string FBC, string TBC)
    {
        try
        {
            string[] FT = FDT.Split('/');
            string[] TT = TDT.Split('/');
            sql = "Exec RptFDClassificationList '" + FPT + "' ,'" + TPT + "' ,'" + conn.ConvertDate(FDT) + "' ,'" + conn.ConvertDate(TDT) + "' ,'" + FBC + "','" + TBC + "'";
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