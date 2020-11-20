using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for ClsAVS5073
/// </summary>
public class ClsAVS5073
{
    DbConnection conn = new DbConnection();
    string FL = "",sql="";
    int result = 0;
    DataTable dt = new DataTable();
	public ClsAVS5073()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable  getCount(string date,string flag,string brcd)
    {
        try
        {
            sql = "EXEC ISP_AVS0120 '" + flag + "','" + conn.ConvertDate(date) + "','" + brcd + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public int getdata(GridView grd, string date, string flag, string brcd)
    {
        try
        {
            sql = "EXEC ISP_AVS0120 '" + flag + "','" + conn.ConvertDate(date) + "','" + brcd + "'";
            result = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
    public int getdata1(GridView grd, string date, string flag, string brcd)
    {
        try
        {
            sql = "EXEC ISP_AVS0120 'ALL','" + conn.ConvertDate(date) + "','" + brcd + "','" + flag + "'";
            result = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
}