using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmProfitLossList : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsCommon cmn = new ClsCommon();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }
                //added by ankita 07/10/2017 to make user frndly
                TxtFDT.Text = Session["EntryDate"].ToString();
                TxtFBrID.Text = Session["BRCD"].ToString();
                TxtTBrID.Text = Session["BRCD"].ToString();
                TxtFBrID.Focus();
                if (cmn.MultiBranch(Session["LOGINCODE"].ToString()) != "Y")
                {
                    TxtFBrID.Enabled = false;
                    TxtTBrID.Enabled = false;
                    TxtFDT.Focus();
                }
                else
                {
                    TxtFBrID.Enabled = true;
                    TxtTBrID.Enabled = true;
                    TxtFBrID.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    { 
        try
        {
            if (RBSel.SelectedValue == "1")
            {
                string redirectURL = "FrmRView.aspx?FBRCD=" + TxtFBrID.Text + "&TBRCD=" + TxtTBrID.Text + "&AsOnDate=" + TxtFDT.Text + "&rptname=RptPLALLBrReport.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else
            {
                string redirectURL = "FrmRView.aspx?FDATE=" + txtFromDate.Text + "&TDATE=" + txtToDate.Text + "&FBRCD=" + TxtFBrID.Text + "&TBRCD=" + TxtTBrID.Text + "&rptname=RptPLALLBrReport_PL.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void RBSel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RBSel.SelectedValue == "1")
        {
            divDate.Visible = false;
            divOnDate.Visible = true;
        }
        else
        {
            if (RBSel.SelectedValue == "2")
                hdnFlag.Value = "1";
            else
                hdnFlag.Value = "2";
            divDate.Visible = true;
            divOnDate.Visible = false;
        }
    }
}