using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsAVS5048
/// </summary>
public class ClsAVS5048
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "";
    int Result = 0;
	public ClsAVS5048()
	{
		
	}
    public int calculatedata(string brcd,string edate,string login,string mid,string pcmac)
    {
        try
        {
            sql = "Exec RptLoanODDetails_Mig @BrCode='" + brcd + "',@EDate='" + conn.ConvertDate(edate).ToString() + "',@LoginCode='" + login + "',@Mid='" + mid + "',@PcMac='" + pcmac + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }


    public DataTable GetODcaldetails(string BRCD, string Edate)
    {
        try
        {
            sql = "Select * from AVSOD_Loan where brcd='" + BRCD + "' and EntryDate='" + conn.ConvertDate(Edate).ToString() + "' Order By BRCD, LoanGlCode, Cast(CustAccNo As Int)";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
}