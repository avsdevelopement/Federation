using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
/// <summary>
/// Summary description for ClsABBVoucharView
/// </summary>
public class ClsABBVoucharView
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sql1 = "", sResult = "";
    int Result = 0, St = 0;
	public ClsABBVoucharView()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int GetInfo(GridView gd, string BRCD, string FL, string EDT, string UCODE,  string SETNO)
    {
        try
        {
            sql = "EXEC ISP_AVS0096 @FLAG='"+FL+"',@BRCD='"+BRCD+"',@DATE='"+conn.ConvertDate(EDT)+"',@USERCODE='"+UCODE+"',@SETNO='"+SETNO+"'";

            Result = conn.sBindGrid(gd, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
}