using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsGetTransSumarryMonhWise
/// </summary>
public class ClsGetTransSumarryMonhWise
{
    DataTable Dt = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";

	public ClsGetTransSumarryMonhWise()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetMonthWiseSumry(string FBC, string FDate, string TDate)
    {
        try
        {
            sql = "Exec TransSummaryMonthWise_SP '" + FBC + "','" + Conn.ConvertDate(FDate).ToString() + "' ,'" + Conn.ConvertDate(TDate).ToString() + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
}