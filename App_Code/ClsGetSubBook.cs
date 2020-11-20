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
/// Summary description for ClsGetSubBook
/// </summary>
public class ClsGetSubBook
{
    string sql = "";
    int Result;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

	public ClsGetSubBook()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetVoucherSubBook(string BranchID, string Fdate, string Tdate, string SGL)
    {
        try
        {
            sql = "Exec RptVoucherDetailsList '" + BranchID + "' ,'" + conn.ConvertDate(Fdate) + "', '" + conn.ConvertDate(Tdate) + "','" + SGL + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
  

    public DataTable GetABBSubBook(string BranchID, string Fdate, string Tdate, string flag)
    {
        try
        {
            sql = "ISP_AVS0097 '" + BranchID + "','" + conn.ConvertDate(Fdate) + "','" + conn.ConvertDate(Tdate) + "','" + flag + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }   


    public DataTable GetVcrSubBookSum(string BranchID, string Fdate, string Tdate, string SGL)
    {
        try
        {
            sql = "Exec RptVoucherSummaryList '" + BranchID + "' ,'" + conn.ConvertDate(Fdate) + "', '" + conn.ConvertDate(Tdate) + "','" + SGL + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetVoucherPrintDts (string BranchID, string Fdate, string SGL)
    {
        try
        {
            sql = "Exec RptVoucherPrinting '" + BranchID + "' ,'" + conn.ConvertDate(Fdate) + "','" + SGL + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetVoucherCrDr (string BranchID, string Fdate, string SGL)
    {
        try
        {
            sql = "Exec RptVoucherPrintingCrDr '" + BranchID + "' ,'" + conn.ConvertDate(Fdate) + "','" + SGL + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetVoucherFD(string BranchID, string Fdate, string SGL)
    {
        try
        {
            sql = "Exec RptVoucherPrintingFD '" + BranchID + "' ,'" + conn.ConvertDate(Fdate) + "','" + SGL + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetVoucherRetire(string BranchID, string Fdate, string SGL)
    {
        try
        {
            sql = "Exec RptVoucherPrintRetired '" + BranchID + "' ,'" + conn.ConvertDate(Fdate) + "','" + SGL + "'";
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