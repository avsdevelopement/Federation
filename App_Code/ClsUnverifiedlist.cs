using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsUnverifiedlist
/// </summary>
public class ClsUnverifiedlist
{
    DataTable DT = new DataTable();
    DbConnection conn = new DbConnection();
    string sql = "";
	public ClsUnverifiedlist()
	{
	}
    public DataTable GetUnverified(string BRCD, string Date)//Dhanya Shetty for Unverified List 
    {
        try
        {
            sql = "EXEC RptUnverifyEntryList @BranchCode='" + BRCD + "',@AsonDate='" + conn.ConvertDate(Date) + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }      

}