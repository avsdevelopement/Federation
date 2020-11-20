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
/// Summary description for ClsGetFormA
/// </summary>
public class ClsGetFormA
{
    string sql = "";
    int Result;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

	public ClsGetFormA()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetCTRTable(string FPT, string TPT, string ason, string FDT, string TDT, string CTRL)
    {
        try
        {
            sql = "Exec RptAccountInfoDetails @FSubGlCode='" + FPT + "' ,@TSubGlCode='" + TPT + "' ,@Asondate='" + conn.ConvertDate(ason) + "',@Fromdate='" + conn.ConvertDate(FDT) + "',@Todate='" + conn.ConvertDate(TDT) + "',@CTRLIMIT='" + CTRL + "'";
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