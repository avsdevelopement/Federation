using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmCashReceiptAuth : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    DbConnection conn = new DbConnection();
    ClsCashAuth CurrentCls = new ClsCashAuth();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    public void BindGrid()
    {
        CurrentCls.Getinfotable(grdCashRct, Session["MID"].ToString(), Session["BRCD"].ToString(),Session["EntryDate"].ToString());
    }
    protected void grdOwgData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    public void NewWindows(string url)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + url + "', 'popup_window', 'width=1000,height=400,left=50,top=50,resizable=no');", true);
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        LinkButton objlink = (LinkButton)sender;
        string asetno = objlink.CommandArgument;
        string IsAuthAllow = "YES";

        // Check maker (Maker can not authorizer entry)
        string maker = CurrentCls.Checkmaker(asetno, Session["BRCD"].ToString());
        if (maker == Session["MID"].ToString())
        {
            IsAuthAllow = "NO";
        }

        string url = "FrmCashReceiptAuthDo.aspx" + "?setno=" + asetno + "&allow=" + IsAuthAllow;
        NewWindows(url);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
}