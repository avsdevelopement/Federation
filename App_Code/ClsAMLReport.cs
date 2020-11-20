using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data.SqlClient;

public class ClsAMLReport
{
    string sql = "";
    int Result = 0;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

	public ClsAMLReport()
	{

	}

    public DataTable GetInfo(string BrCode, string FDate, string FProdCode, string TProdCode)
    {
        try
        {
            string[] FDT = FDate.Split('/');

            sql = "Exec SP_AMLReport @TFMONTH='" + FDT[1].ToString() + "',@TFYEAR='" + FDT[2].ToString() + "',@ToDate='" + conn.ConvertDate(FDate) + "',@BrCode='" + BrCode + "',@FProdCode='" + FProdCode + "',@TProdCode='" + TProdCode + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
            return DT;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
}