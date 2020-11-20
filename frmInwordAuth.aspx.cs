using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class frmInwordAuth : System.Web.UI.Page
{
    ClsOutAuth CLSOUTAUTH = new ClsOutAuth();
    DataSet dt = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    public void BindGrid()
    {
        CLSOUTAUTH.Getinfotable(grdOwgData, Session["MID"].ToString(), Session["BRCD"].ToString(),Session["EntryDate"].ToString(),"I");
    }

    public void NewWindows(string url)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + url + "', 'popup_window', 'width=1000,height=600,left=50,top=50,resizable=no');", true);
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton objlink = (LinkButton)sender;
        string strnumid = objlink.CommandArgument;

        // Get SetNo and ScrollNo from Linkbuton
        string[] setscroll = strnumid.Split('-');

        string url = "frmInwordAuthoDo.aspx" + "?setno=" + setscroll[0] + "&scrollno=" + setscroll[1] + "&op=authorize";
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (TxtInstNo.Text == "")
        {
            BindGrid();
        }
        else
        {
            CLSOUTAUTH.GetinfotableInstNo(grdOwgData, Session["MID"].ToString(), Session["BRCD"].ToString(), TxtInstNo.Text.ToString());
        }
    }
    protected void grdOwgData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdOwgData.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void TxtInstNo_TextChanged(object sender, EventArgs e)
    {

    }
}