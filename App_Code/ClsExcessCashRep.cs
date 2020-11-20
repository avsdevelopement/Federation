using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsExcessCashRep
/// </summary>
public class ClsExcessCashRep
{
    string sql = "";
    int Result = 0;
    DataTable DT = new DataTable();
    DbConnection Dbconn = new DbConnection();

	public ClsExcessCashRep()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable Create_ExcessCash(string FDT, string TDT, string FBRCD, string TBRCD,string BRCD)
    {
        try
        {
            sql = "EXEC SP_EXCESS_CASH @FDATE='" + Dbconn.ConvertDate(FDT) + "',@TDATE='" + Dbconn.ConvertDate(TDT) + "',@FBRCD='" + FBRCD + "',@TBRCD='" + TBRCD + "',@BRCD='" + BRCD + "'";
            DT = Dbconn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}