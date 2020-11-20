using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmInvBalList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
        
        }
    }
    protected void BtnBalList_Click(object sender, EventArgs e)
    {
        try
        {
            string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&AsOnDate=" +txtasondate.Text + "&EDAT=" + Session["EntryDate"].ToString() + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptInvBalList.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmBlank.aspx");
    }
}