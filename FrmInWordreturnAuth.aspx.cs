using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmInWordreturnAuth : System.Web.UI.Page
{
    ClsOutReturnAuth CLSOUTRETURNAUTH = new ClsOutReturnAuth();
    ClsCommon CM = new ClsCommon();
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
        CLSOUTRETURNAUTH.Getinfotable(grdOwgData, Session["MID"].ToString(), Session["BRCD"].ToString(),Session["EntryDate"].ToString(),"I","IR");
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string strnumid = objlink.CommandArgument;

            // Get SetNo and ScrollNo from Linkbuton
            string[] setscroll = strnumid.Split('-');


            string MID = CM.GetIOMid("IW", Session["BRCD"].ToString(), setscroll[0].ToString(), Session["EntryDate"].ToString());
            if (MID != null && MID != Session["MID"].ToString())
            {
                string url = "frmInwordAuthoDo.aspx" + "?setno=" + setscroll[0] + "&scrollno=" + setscroll[1] + "&op=returnauth";
                NewWindows(url);

            }
            else
            {
                WebMsgBox.Show("Warning : User " + Session["LOGINCODE"].ToString() + " is restricted to authorized...!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void NewWindows(string url)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + url + "', 'popup_window', 'width=1000,height=600,left=50,top=50,resizable=no');", true);
    }

    protected void grdOwgData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdOwgData.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}