using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsMenuPermission
{
    DataTable DT = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";

	public ClsMenuPermission()
	{
		
	}

    public DataTable GetHead(string sql)
    {
        try
        {
            DT = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}