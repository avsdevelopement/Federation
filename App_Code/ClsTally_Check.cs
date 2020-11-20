using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ClsTally_Check
/// </summary>
public class ClsTally_Check
{
    DbConnection conn = new DbConnection();
    string sql = "", STR = "";
    int RESULT = 0;
    DataTable DT = new DataTable();
	public ClsTally_Check()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int GetSummary(GridView GDS, string BRCD,string MID,string FDATE,string TODATE,string EDT)
   {
        //-- Exec TransSummaryMonthWise_SP '10','2016-01-01','2017-02-28'
       try
       {
           sql = "Exec TransSummaryMonthWise_SP '" + BRCD + "','" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TODATE) + "'";
           RESULT = conn.sBindGrid(GDS, sql);
       }
       catch (Exception Ex)
       {
           ExceptionLogging.SendErrorToText(Ex);
       }
       return RESULT;
    }


    public int GetDetails(GridView GDT, string BRCD,string SUBGL, string MID, string FDATE, string TODATE, string EDT)
    {
        //-- Exec TransSubGlMonthWise_SP '10','209','2016-01-01','2017-02-28'
        try
        {
            sql = "Exec TransSubGlMonthWise_SP '" + BRCD + "','" + SUBGL + "','" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TODATE) + "'";
            RESULT = conn.sBindGrid(GDT, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RESULT;
    }
    
}