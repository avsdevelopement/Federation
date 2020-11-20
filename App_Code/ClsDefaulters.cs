using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsDefaulters
/// </summary>
public class ClsDefaulters
{
    DataTable DT = new DataTable();
    DbConnection conn = new DbConnection();
    string sql = "";
	public ClsDefaulters()
	{
	}
    public DataTable GetDefault(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string Date)//Dhanya Shetty for Defaulters 
    {
        try
        {
            sql = "EXEC RptAllLoanBalancesList @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@FAccNo='" + FACCNO + "',@TAccNo='" + TACCNO + "', @EDate='" + conn.ConvertDate(Date) + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}