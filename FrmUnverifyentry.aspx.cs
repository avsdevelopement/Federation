using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmUnverifyentry : System.Web.UI.Page
{
    ClsUnverifiedlist UV = new ClsUnverifiedlist();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            TxtBrcd.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrcd.Text + "&Date=" + TxtAsonDate.Text + "&UserName=" + Session["UserName"].ToString() + "&EntryDate=" + Session["EntryDate"].ToString() + "&rptname=RptUnverifyentry.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    public void ClearData()
    {
     TxtBrcd.Text="";
     TxtAsonDate.Text = "";

    }
}