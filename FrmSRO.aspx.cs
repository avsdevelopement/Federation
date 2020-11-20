using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmSRO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("FrmLogin.aspx");
        }

    }
    protected void ddlNotic_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(ddlNotic.SelectedValue=="1")
                HttpContext.Current.Response.Redirect("FrmS0001.aspx", true);
            if(ddlNotic.SelectedValue=="2")
                HttpContext.Current.Response.Redirect("FrmS0002.aspx", true);
            if(ddlNotic.SelectedValue=="3")
                HttpContext.Current.Response.Redirect("FrmS0003.aspx", true);
            if (ddlNotic.SelectedValue == "4")
                HttpContext.Current.Response.Redirect("FrmS0004.aspx", true);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}