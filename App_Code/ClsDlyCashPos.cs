using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data.SqlClient;

public class ClsDlyCashPos
{
    DbConnection DBconn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "";
    int Result = 0;

	public ClsDlyCashPos()
	{
		
	}

    public string GetBranchName(string brcd)
    {
        try
        {
            sql = "select MIDNAME from BANKNAME where BRCD='" + brcd + "'";
            sql = DBconn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }

    public DataSet GetDailyCashPos(string BrCode, string AsOnDate, string Flag)
    {
        try
        {
            DS = new DataSet();
            sql = "Exec SP_DCSHPSTNWDENOM @BrCode='" + BrCode + "', @EDate='" + DBconn.ConvertDate(AsOnDate) + "', @FLAG='" + Flag + "'";
            DT = DBconn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
                DS.Tables.Add(DT);
            else
                DS = null;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
}