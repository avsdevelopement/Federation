using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmCashPayAuth : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    DbConnection conn = new DbConnection();
    ClsCashPayAuth CurrentCls = new ClsCashPayAuth();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                BindGrid();
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
                //Response.Redirect("FrmLogin.aspx", true);
            }
        }
    }

    public void BindGrid()
    {
        try
        {
            CurrentCls.Getinfotable(grdCashRct, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
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
        try
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

            // QUERY URL
            string url = "FrmCashPayAuthDo.aspx" + "?setno=" + asetno + "&allow=" + IsAuthAllow;
            NewWindows(url);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
}