using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for FrmRegister
/// </summary>
public class FrmRegister
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0;
	public FrmRegister()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int BindADD(GridView Gview, string cstno)
    {
        string sql = "SELECT * FROM TREGISTER";
        try 
        {
        Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }
}