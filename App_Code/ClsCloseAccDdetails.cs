using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClsCloseAccDdetails
/// </summary>
public class ClsCloseAccDdetails
{
    DbConnection conn = new DbConnection();
    string Result, sql;
    int IntResult = 0;
    DataTable DT = new DataTable();
    string FL = "";
    int Res = 0;
	public ClsCloseAccDdetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable ShareCloseAccDetails(string FDATE, string TDATE, string BRCD,string Edate,string FBRCD,string TBRCD)
    {
        sql = "EXEC ISP_AVS0129 '"+BRCD+"','4','"+conn.ConvertDate(FDATE)+"','"+conn.ConvertDate(TDATE)+"','"+conn.ConvertDate(Edate)+"','"+FBRCD+"','"+TBRCD+"'";
        DT = conn.GetDatatable(sql);
        return DT;
    }

}