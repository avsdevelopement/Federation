using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsGetTransSubMonth
/// </summary>
public class ClsGetTransSubMonth
{
    DataTable Dt = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";

	public ClsGetTransSubMonth()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetMonthWiseSUBGL (string FBC, string PT, string FDate, string TDate)
    {
        try
        {
            sql = "Exec TransSubGlMonthWise_SP '" + FBC + "','" + PT + "','" + Conn.ConvertDate(FDate).ToString() + "' ,'" + Conn.ConvertDate(TDate).ToString() + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
}