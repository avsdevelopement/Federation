using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ClsOverDueSummary
/// </summary>
public class ClsOverDueSummary
{
    DataTable DT = new DataTable();
    DbConnection conn = new DbConnection();
    string sql = "";
    int Disp;
	public ClsOverDueSummary()
	{
		
	}

    public DataTable GetFilter(string SL, string FL, string EDT, string BRCD)
    {
        try
        {
            sql = "EXEC Isp_LoanOverdue_Summary @Flag='" + SL + "',@SFlag='" + FL + "',@OnDate='" + conn.ConvertDate(EDT).ToString() + "',@Brcd='" + BRCD + "'";
            //Disp = conn.sBindGrid(grid, sql);
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
       // return Disp;
        return DT;
    }

    public DataTable GetOverdueReport(string EDT, string BRCD, string DATE, string FLL, string SFL)//Dhanya Shetty FOR Overdue summary
    {
        try
        {
            sql = "EXEC Isp_LoanOverdue_Summary @Flag='OD',@SFlag='" + SFL + "',@Brcd='" + BRCD + "',@OnDate='" + conn.ConvertDate(DATE) + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}