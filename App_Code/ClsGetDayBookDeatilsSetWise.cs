using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsGetDayBookDeatilsSetWise
/// </summary>
public class ClsGetDayBookDeatilsSetWise
{
    string sql = "";
    int Result = 0;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    double BAL;

	public ClsGetDayBookDeatilsSetWise()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetDaySetInfoDetails (string BranchID, string FL, string FLT, string FDate) //Rakesh 09-12-2016 
    {
        try
        {
            sql = "Exec RptDayBookRegistrerDetailsSetWise '" + BranchID + "' ,'" + FL + "' ,'" + FLT + "' , '" + conn.ConvertDate(FDate).ToString() + "' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetDayBook_ALLDetailsDT(string BranchID, string FL, string FLT, string FDate) //Rakesh 09-12-2016 
    {
        try
        {
            sql = "Exec RptDayBookDetailsCrDr @BranchID='" + BranchID + "' ,@SKIP_DAILY='" + FL + "' ,@SKIP_INT='" + FLT + "' , @AsonDate='" + conn.ConvertDate(FDate).ToString() + "' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GETDayBookRegDT_TZMP(string BranchID, string FL, string FLT, string FDate, string TDate) //Rakesh 03-01-2018 For TZMP 
    {
        try
        {
            sql = "Exec RptDayBookReg_TZMP '" + BranchID + "' ,'" + FL + "' ,'" + FLT + "' , '" + conn.ConvertDate(FDate).ToString() + "', '" + conn.ConvertDate(TDate).ToString() + "'  ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GETDayBookReg_FromToDt (string BranchID, string FL, string FLT, string FDate, string TDate) //Rakesh 03-01-2018 For TZMP
    {
        try
        {
            sql = "Exec RptDayBookReg_FromTo '" + BranchID + "' ,'" + FL + "' ,'" + FLT + "' , '" + conn.ConvertDate(FDate).ToString() + "', '" + conn.ConvertDate(TDate).ToString() + "'  ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetDayBookReg_RenewalDT(string BranchID, string FDate, string TDate) //Rakesh 03-01-2018 For TZMP
    {
        try
        {
            sql = "Exec RptDayBookReg_Renewal '" + BranchID + "' ,'" + conn.ConvertDate(FDate).ToString() + "', '" + conn.ConvertDate(TDate).ToString() + "'  ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetDayBookDP_Reg_DT (string BranchID, string FDate, string TDate) //Rakesh 03-01-2018 For TZMP
    {
        try
        {
            sql = "Exec RptDayBookDP_Register '" + BranchID + "' ,'" + conn.ConvertDate(FDate).ToString() + "', '" + conn.ConvertDate(TDate).ToString() + "'  ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public double GetOpening(string BranchID, string Type, string Fdate) //Rakesh 09-12-2016 Op/Cl cash 
    {
        try
        {
            sql = "Exec RptCashPostionReport_Day '" + BranchID + "' ,'" + Type + "','" + conn.ConvertDate(Fdate).ToString() + "' ";
            BAL = Convert.ToDouble(conn.sExecuteScalar(sql));

            //string B = "";
            //sql = "Exec RptCashPostionReport_Day '" + Type + "','" + conn.ConvertDate(Fdate).ToString() + "' ";
            //B = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BAL;
    }
}