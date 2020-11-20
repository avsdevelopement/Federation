using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsUniqueCustomer
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sResult = "";
    int Result = 0;

	public ClsUniqueCustomer()
	{
		
	}

    public int AssignUniqueCustNo(string BrCode, string WorkDate)
    {
        try
        {
            sql = "Exec ProcForUniqueCustomer @BrCode = '" + BrCode + "', @WorkDate = '" + conn.ConvertDate(WorkDate).ToString() + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

}