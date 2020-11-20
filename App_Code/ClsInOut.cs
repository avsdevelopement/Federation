using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsInOut
/// </summary>
public class ClsInOut
{
    DbConnection conn = new DbConnection();
    int Res = 0;
    string sql = "", STR = "";
    DataTable DT = new DataTable();
	public ClsInOut()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int GetSumGrid(GridView GD, string BRCD, string ASON, string FL)
    {
        try
        {
            sql = "Exec Isp_ZonePosting_Details @Flag='" + FL + "',@Brcd='" + BRCD + "',@Edt='" + conn.ConvertDate(ASON) + "'";
            Res = conn.sBindGrid(GD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }
}