using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAdharLink : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            txtfromdate.Text = Session["EntryDate"].ToString();
        }
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        try
        {
            string FL = "Insert";//Dhanya Shetty
            string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Adharlink_Rpt _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            if (hdnFlag.Value == "0")
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&FDate=" + txtfromdate.Text + "&EDAT=" + Session["EntryDate"].ToString() + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptAdharLink.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&FDate=" + txtfromdate.Text + "&EDAT=" + Session["EntryDate"].ToString() + "&UserName=" + Session["UserName"].ToString() + "&Flag="+hdnFlag.Value+"&rptname=RptAdharLinkDetails.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        txtfromdate.Text = "";
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmBlank.aspx");
    }
    protected void ddlList_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnFlag.Value = ddlList.SelectedValue;
    }
}