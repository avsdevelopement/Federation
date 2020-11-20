using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmClgRegList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TxtTDate.Text = Session["EntryDate"].ToString();
            TxtBankCD.Text = "0000";
            TxtBrID.Text = Session["BRCD"].ToString();
        }
    }
protected void ClgRegister_Click(object sender, EventArgs e)
{
    try
    {
        if (rbtnInward.Checked)
        {
            if (rbtnDetails.Checked)
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&BankCode=" + TxtBankCD.Text + "&FDate=" + TxtTDate.Text + "&FL=IW &rptname=RptClgRegList.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&BankCode=" + TxtBankCD.Text + "&FDate=" + TxtTDate.Text + "&FL=IW &rptname=RptClgRegListSummary.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        else if (rbtnOutward.Checked)
        {
            if (rbtnDetails.Checked)
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&BankCode=" + TxtBankCD.Text + "&FDate=" + TxtTDate.Text + "&FL=OW &rptname=RptClgRegList.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&BankCode=" + TxtBankCD.Text + "&FDate=" + TxtTDate.Text + "&FL=OW &rptname=RptClgRegListSummary.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
    }
    catch (Exception Ex)
    {
        ExceptionLogging.SendErrorToText(Ex);
    }
}
    protected void rbtnInward_CheckedChanged(object sender, EventArgs e)
    {
        rbtnInward.Checked = true;
        rbtnOutward.Checked = false;
    }

    protected void rbtnOutward_CheckedChanged(object sender, EventArgs e)
    {
        rbtnOutward.Checked = true;
        rbtnInward.Checked = false;
    }

    protected void rbtnDetails_CheckedChanged(object sender, EventArgs e)
    {
        rbtnDetails.Checked = true;
        rbtnSummary.Checked = false;
    }

    protected void rbtnSummary_CheckedChanged(object sender, EventArgs e)
    {
        rbtnSummary.Checked = true;
        rbtnDetails.Checked = false;
    }
}