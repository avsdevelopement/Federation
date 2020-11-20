using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsAVSBMTransaction
/// </summary>
public class ClsAVSBMTransaction
{
    DataTable Dt = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";

	public ClsAVSBMTransaction()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetMBDetails(string BRCD, string FD, string ason)
    {
        try
        {
            sql = "Exec RptAVSBMTableReport '" + BRCD + "' ,'" + FD + "','" + ason + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception)
        {

            throw;
        }
        return Dt;
    }
}