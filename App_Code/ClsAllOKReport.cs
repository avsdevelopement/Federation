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


/// <summary>
/// Summary description for ClsAllOKReport
/// </summary>
public class ClsAllOKReport
{
    string sql = "";
    int Result = 0;
    DbConnection DBconn = new DbConnection();
    DataTable DT = new DataTable();

	public ClsAllOKReport()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int GetExportDetails(GridView gd,string dt, string brcd)
    {
        try
        {
            int result = 0;
            sql = "Exec SP_ALLOK @ASONDT='"+DBconn.ConvertDate(dt)+"',@BRCD='"+brcd+"'";
            result = DBconn.sBindGrid(gd,sql);
           
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result; 
    }

    public DataTable CreateAllOK(string Ason,string brcd)
    {
        DataTable DT = new DataTable();
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        ClsOpenClose OC = new ClsOpenClose();
        try
        {
          //  string tbname;
            sql = "Exec SP_ALLOK @ASONDT='" + DBconn.ConvertDate(Ason) + "',@BRCD='" + brcd + "'";


            DT = DBconn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

   
}