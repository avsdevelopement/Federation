using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ClsMaturityLoanReport
/// </summary>
public class ClsMaturityLoanReport
{
    DataTable DT = new DataTable();
    DbConnection conn = new DbConnection();
    string sql = "";
    int Disp;
	public ClsMaturityLoanReport()
	{
	
	}

    public int GetFilter(GridView grid,string BRCD,string EDATE,  string FDATE, string TDATE, string FL)
    {
        try
        {
            if (FL == "MLR")
            {
                sql = "EXEC D_MaturityLoanReport @BrCode='" + BRCD + "',@EDate='" + conn.ConvertDate(EDATE).ToString() + "',@FDate='" + conn.ConvertDate(FDATE).ToString() + "',@TDate='" + conn.ConvertDate(TDATE).ToString() + "',@Flag='" + FL + "'";
            }
            Disp = conn.sBindGrid(grid, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Disp;
    }

    public DataTable GetMaturityReportD(string EDT, string BRCD, string DATE, string SUBGL, string FLL, string SFL, string FDate, string TDate)//Dhanya Shetty Maturity loan report
    {
        try
        {
            sql = "EXEC D_MatLoanDisplay @Flag='OD',@SFlag='" + SFL + "',@Brcd='" + BRCD + "',@Sbgl='" + SUBGL + "',@OnDate='" + conn.ConvertDate(DATE) + "',@FDate='" + conn.ConvertDate(FDate) + "',@TDate='" + conn.ConvertDate(TDate) + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}