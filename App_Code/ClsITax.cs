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
/// Summary description for ClsITax
/// </summary>
public class ClsITax
{
    string sql = "";
    int Result;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

	public ClsITax()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetITaxDT(string BranchID, string Fdate, string Tdate, string SGL, string FL)
    {
        try
        {
            sql = "Exec RptInComeTaxReport @BranchID='" + BranchID + "' ,@FromDate='" + conn.ConvertDate(Fdate) + "',@ToDate= '" + conn.ConvertDate(Tdate) + "',@Type='" + SGL + "',@SType='" + FL + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetITaxDT_SHR (string BranchID, string Fdate, string Tdate, string SGL, string FL)
    {
        try
        {
            sql = "Exec RptITaxMemReport @BranchID='" + BranchID + "' ,@FromDate='" + conn.ConvertDate(Fdate) + "',@ToDate= '" + conn.ConvertDate(Tdate) + "',@Type='" + SGL + "',@SType='" + FL + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetITaxDT_LN (string BranchID, string Fdate, string Tdate, string SGL, string FL)
    {
        try
        {
            sql = "Exec RptITaxMemReport @BranchID='" + BranchID + "' ,@FromDate='" + conn.ConvertDate(Fdate) + "',@ToDate= '" + conn.ConvertDate(Tdate) + "',@Type='" + SGL + "',@SType='" + FL + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetITaxDT_DP (string BranchID, string Fdate, string Tdate, string SGL, string FL)
    {
        try
        {
            sql = "Exec RptITaxMemReport @BranchID='" + BranchID + "' ,@FromDate='" + conn.ConvertDate(Fdate) + "',@ToDate= '" + conn.ConvertDate(Tdate) + "',@Type='" + SGL + "',@SType='" + FL + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetLoanOverdueMIS_Details(string BranchID, string FL, string AsOnDate )
    {
        try
        {
            sql = "Exec RptLoanOD_MIS1 @Brcd='" + BranchID + "' ,@PrdCd='" + FL + "',@AsOnDate= '" + conn.ConvertDate(AsOnDate) + "' ";
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