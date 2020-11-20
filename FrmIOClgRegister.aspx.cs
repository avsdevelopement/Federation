using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmIOClgRegister : System.Web.UI.Page
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
    protected void IOClgRegister_Click(object sender, EventArgs e)
    {
        try
        {
            string SFL="";
            
            if(rbtnPaRegister.Checked)
                SFL="1";
            else if(rbtnRegister.Checked)
                SFL="ALL";
            else if(rbtnUnpaRegister.Checked)
                SFL="4";

            if (rbtnInward.Checked)
            {
                if(rbtnRetRegister.Checked)
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&BankCode=" + TxtBankCD.Text + "&FDate=" + TxtTDate.Text + "&FL=IW&rptname=RptClearngReturnRegister.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                else
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&BankCode=" + TxtBankCD.Text + "&FDate=" + TxtTDate.Text + "&SFL=" + SFL + "&FL=IW&rptname=RptClearngRegister.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                
            }
            else if (rbtnOutward.Checked)
            {
                if (rbtnRetRegister.Checked)
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&BankCode=" + TxtBankCD.Text + "&FDate=" + TxtTDate.Text + "&FL=OW&rptname=RptClearngReturnRegister.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                else
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&BankCode=" + TxtBankCD.Text + "&FDate=" + TxtTDate.Text + "&SFL=" + SFL + "&FL=OW&rptname=RptClearngRegister.rdlc";
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
    protected void rbtnRegister_CheckedChanged(object sender, EventArgs e)
    {
        rbtnRegister.Checked = true;
        rbtnRetRegister.Checked = false;
        rbtnPaRegister.Checked = false;
        rbtnUnpaRegister.Checked = false;
    }
    protected void rbtnRetRegister_CheckedChanged(object sender, EventArgs e)
    {
        rbtnRetRegister.Checked = true;
        rbtnRegister.Checked = false;
        rbtnPaRegister.Checked = false;
        rbtnUnpaRegister.Checked = false;
    }
    protected void rbtnPaRegister_CheckedChanged(object sender, EventArgs e)
    {
        rbtnPaRegister.Checked = true;
        rbtnRetRegister.Checked = false;
        rbtnRegister.Checked = false;
        rbtnUnpaRegister.Checked = false;
    }
    protected void rbtnUnpaRegister_CheckedChanged(object sender, EventArgs e)
    {
        rbtnUnpaRegister.Checked = true;
        rbtnRegister.Checked = false;
        rbtnPaRegister.Checked = false;
        rbtnRetRegister.Checked = false;
    }
}