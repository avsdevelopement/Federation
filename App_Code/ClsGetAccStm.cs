using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class ClsGetAccStm
{
    DataTable Dt = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";

	public ClsGetAccStm()
	{

	}

    public DataTable GetAccStmReg(string FDate, string TDate, string PT, string AC, string FBC)
    {
        try
        {
            sql = "Exec RptAccountStatement '' ,'' ,'" + Conn.ConvertDate(FDate).ToString() + "' ,'" + Conn.ConvertDate(TDate).ToString() + "','" + PT + "','" + AC + "','" + FBC  + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
}