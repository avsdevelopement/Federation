using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmOutReturn : System.Web.UI.Page
{
    ClsOutReturn CLSOUTRETURN = new ClsOutReturn();
    DataSet dt = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("FrmLogin.aspx");
        }

        BindGrid();
    }

    public void BindGrid()
    {
        CLSOUTRETURN.Getinfotable(grdOwgData, Session["MID"].ToString(), Session["BRCD"].ToString());
    }

    public void NewWindows(string url)
    {
        //string s = "window.open('" + url + "', 'popup_window', 'width=1000,height=650,left=50,top=50,resizable=yes');";
        //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + url + "', 'popup_window', 'width=1000,height=600,left=50,top=50,resizable=no');", true);
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton objlink = (LinkButton)sender;
        string strnumid = objlink.CommandArgument;

        // Get SetNo and ScrollNo from Linkbuton
        string[] setscroll = strnumid.Split('-');

        string url = "FrmOutAuthDo.aspx" + "?setno=" + setscroll[0] + "&scrollno=" + setscroll[1] + "&op=return";
        NewWindows(url);
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton objlink = (LinkButton)sender;
        string strnumid = objlink.CommandArgument;

        // Get SetNo and ScrollNo from Linkbuton
        string[] setscroll = strnumid.Split('-');

        string url = "FrmOutAuthDo.aspx" + "?setno=" + setscroll[0] + "&scrollno=" + setscroll[1] + "&op=delete";
        NewWindows(url);
    }

    protected void grdOwgData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    
}