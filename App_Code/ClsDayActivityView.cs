using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ClsDayActivityView
/// </summary>
public class ClsDayActivityView
{
     DataTable DT = new DataTable();
    DbConnection conn = new DbConnection();
    string sql = "";
    int Disp;
	public ClsDayActivityView()
	{

	}
    public int GetFilter(GridView grid, string FL, string BRCD, string FDATE, string TDATE,string RBD)
    {
        try
        {
            if(FL=="BRCDS")
            {
                sql = "EXEC SP_ACTIVITYVIEW @flag='" + FL + "',@Brcd='" + BRCD + "',@FromDate='" + conn.ConvertDate(FDATE).ToString() + "',@ToDate='" + conn.ConvertDate(TDATE).ToString() + "',@RBD='"+RBD+"'";
            }
            Disp = conn.sBindGrid(grid, sql);
        }
        catch (Exception Ex)
        {
             ExceptionLogging.SendErrorToText(Ex);
        }
        return Disp;
    }
    public DataTable GetDayActivityInfo(string FL, string BRCD, string FDATE, string TDATE, string RBD)
    {
        DataTable DT1 = new DataTable();
        try
        {
            sql = "EXEC SP_ACTIVITYVIEW @flag='" + FL + "',@Brcd='" + BRCD + "',@FromDate='" + conn.ConvertDate(FDATE).ToString() + "',@ToDate='" + conn.ConvertDate(TDATE).ToString() + "',@RBD='" + RBD + "'"; 
             DT1 = new DataTable();
            DT1 = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT1;
    }
    public int AGetFilter(GridView grid, string FL,string FDATE, string TDATE,string RBD)
    {
        try
        {
            if (FL == "ALLB")
            {
                sql = "EXEC SP_ACTIVITYVIEW @flag='" + FL + "',@FromDate='" + conn.ConvertDate(FDATE).ToString() + "',@ToDate='" + conn.ConvertDate(TDATE).ToString() + "',@RBD='"+RBD+"'";
             }
            Disp = conn.sBindGrid(grid, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Disp;
    }
    public int GetDayCloseDetails(GridView grid, string FL, string FDATE, string TDATE)//By amruta for day closed
    {
        try
        {
            if (FL == "ALLB")
            {
                sql = "EXEC SP_DayClose @flag='" + FL + "',@FromDate='" + conn.ConvertDate(FDATE).ToString() + "',@ToDate='" + conn.ConvertDate(TDATE).ToString() + "'";
            }
            Disp = conn.sBindGrid(grid, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Disp;
    }
    public int GetBrDayCloseDetails(GridView grid, string FL, string BRCD, string FDATE, string TDATE)//By amruta for day closed
    {
        try
        {
            if (FL == "BRCDS")
            {
                sql = "EXEC SP_DayClose @flag='" + FL + "',@Brcd='" + BRCD + "',@FromDate='" + conn.ConvertDate(FDATE).ToString() + "',@ToDate='" + conn.ConvertDate(TDATE).ToString() + "'";
            }
            Disp = conn.sBindGrid(grid, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Disp;
    }
    public DataTable GetDayCloseInfo(string FL, string BRCD, string FDATE, string TDATE)// by Amruta for Day close 12/04/2017
    {
        DataTable DT1 = new DataTable();
        try
        {

            sql = "EXEC SP_DayClose @flag='" + FL + "',@Brcd='" + BRCD + "',@FromDate='" + conn.ConvertDate(FDATE).ToString() + "',@ToDate='" + conn.ConvertDate(TDATE).ToString() + "'";
            DT1 = new DataTable();
            DT1 = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT1;
    }
    }
