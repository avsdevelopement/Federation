using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data.SqlClient;

public class ClsMigTransaction
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sQuery = "";
    int Result = 0;

	public ClsMigTransaction()
	{
		
	}

    public int MoveTransactions(string BrCd, string EDate, string LoginCode, string Mid)
    {
        try
        {
            sql = "Exec RptLoanODDetails_Mig @BrCode = '" + BrCd + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @LoginCode = '" + LoginCode + "', @Mid = '" + Mid + "', @PcMac = '" + conn.PCNAME().ToString() + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
}