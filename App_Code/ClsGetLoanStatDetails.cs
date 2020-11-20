using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsGetLoanStatDetails
/// </summary>
public class ClsGetLoanStatDetails
{
    DataTable Dt = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";

	public ClsGetLoanStatDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetLnStatData (string FDate, string TDate, string PT, string AC, string FBC)
    {
        try
        {
            sql = "Exec RptLoanStatement @PFDT='" + Conn.ConvertDate(FDate).ToString() + "' ,@PTDT='" + Conn.ConvertDate(TDate).ToString() + "',@SubGlCode='" + PT + "',@Accno='" + AC + "',@BRCD='" + FBC + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
}