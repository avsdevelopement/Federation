using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsDayBookDetails
/// </summary>
public class ClsDayBookDetails
{
    string sql = "";
    int Result = 0;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    double BAL;

	public ClsDayBookDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetDayInfoDetails(string BranchID, string FL, string FLT, string FDate) //Rakesh 09-12-2016 
    {
        try
        {
            sql = "Exec RptDayBookRegisterDetails '" + BranchID + "' ,'" + FL + "' ,'" + FLT + "' , '" + conn.ConvertDate(FDate).ToString() + "' ";
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