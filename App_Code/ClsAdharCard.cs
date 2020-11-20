using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClsAdharCard
/// </summary>
public class ClsAdharCard
{
    DbConnection conn = new DbConnection();
    string sql = "";
    DataTable Dt = new DataTable();
	public ClsAdharCard()
	{
		
	}

    public DataTable AdharlinkRpt(string BRCD, string Date, string EDate)
    {
        try
        {
            sql = "exec SP_ADHARLINK '"+BRCD+"','"+conn.ConvertDate(Date)+"','0'";
            Dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
         ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }

    public DataTable AdharlinkRptDetails(string BRCD, string Date, string EDate,string Flag)
    {
        try
        {
            sql = "exec SP_ADHARLINK '" + BRCD + "','" + conn.ConvertDate(Date) + "','" + Flag + "'";
            Dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
}