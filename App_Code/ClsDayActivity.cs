using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;

public class ClsDayActivity
{
    DbConnection conn=new DbConnection();
    string sql="";
    int Result=0;
    DataTable DT = new DataTable();
    public ClsDayActivity()
    {       
    }
    public DataTable GetBranch(string BRCD1, string BRCD2, string FL)
    {
        try
        {
        if (FL == "SPE")
        {
            sql = "SELECT DISTINCT (BRCD) BRCD FROM BANKNAME WHERE BRCD BETWEEN '" + BRCD1 + "' AND  '" + BRCD2 + "' ORDER BY BRCD ";
        }
        else
        {
            sql = "SELECT DISTINCT (BRCD) BRCD FROM BANKNAME ORDER BY BRCD ";
        }
        DT = new DataTable();
        DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return DT;
    }
}