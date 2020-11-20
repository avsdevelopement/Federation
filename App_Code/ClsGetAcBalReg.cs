using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsGetAcBalReg
/// </summary>
public class ClsGetAcBalReg
{
    DataTable Dt = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";

	public ClsGetAcBalReg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetBalReg(string BRCD, string GL, string FD, string ason)
    {
        try
        {
            sql = "Exec RptBalanceRegisterReport '" + BRCD + "' ,'" + GL + "','" + FD + "','" + ason + "'";
            Dt = Conn.GetDatatable(sql); 
        }
        catch (Exception)
        {

            throw;
        }
        return Dt;
    }
}