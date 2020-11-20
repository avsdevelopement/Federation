using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;

public class ClsGenrateAVSBTable
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int result = 0;

	public ClsGenrateAVSBTable()
	{
	}

    public string GetBranchName(string BrCode)
    {
        try
        {
            sql = "Select MidName From BankName Where BrCd = '" + BrCode + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }

    public int GenrateTable(string BrCode, string FMonth, string TMonth, string FYear, string TYear, string Mid)
    {
        try
        {
            sql = "Exec SP_CREATEAVSB_FRONT @BrCode = '" + BrCode + "', @FMonth ='" + FMonth + "', @TMonth ='" + TMonth + "', @FYear ='" + FYear + "', @TYear ='" + TYear + "', @Mid='" + Mid + "', @PcMac = '" + conn.PCNAME().ToString() + "' ";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
}