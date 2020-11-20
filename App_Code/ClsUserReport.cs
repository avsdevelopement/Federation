using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for ClsUserReport
/// </summary>
public class ClsUserReport
{
    DataTable DT = new DataTable();
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result=0;
    int DISP;

	public ClsUserReport()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable GetUserRpt(string TBRCD, string FBRCD,string FLAG,string flag1)
    {
        try
        {
            sql = "exec SP_USERREPORT @FLAG='" + FLAG + "',@FLAG1='"+flag1+"',@FBRCD='" + FBRCD + "',@TBRCD='" + TBRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public int GetFilter(GridView grid, string TBRCD, string FBRCD, string FLAG)
    {
        try
        {
            sql = "exec SP_USERREPORT @FLAG='" + FLAG + "',@FBRCD='" + FBRCD + "',@TBRCD='" + TBRCD + "'";
            DISP = conn.sBindGrid(grid, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DISP;
    }

}