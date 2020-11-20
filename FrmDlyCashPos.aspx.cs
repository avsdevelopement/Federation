using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmDlyCashPos : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsDlyCashPos CP = new ClsDlyCashPos();
    string sResult = "";
    int Result = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            if (!IsPostBack)
            {
                txtBrCode.Text = Session["BRCD"].ToString();
                txtBrName.Text = CP.GetBranchName(txtBrCode.Text.ToString());
                txtAsOnDate.Text = Session["EntryDate"].ToString();
                txtBrCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtBrCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtBrCode.Text.ToString() != "")
            {
                string BrName = CP.GetBranchName(txtBrCode.Text.ToString());
                if (BrName != null || BrName != "")
                {
                    txtBrName.Text = BrName;
                    txtBrCode.Focus();
                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    txtBrName.Text = "";
                    txtBrCode.Text = "";
                    txtBrCode.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                txtBrName.Text = ""; 
                txtBrCode.Text = "";
                txtBrCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string redirectURL = "FrmReportViewer.aspx?BC=" + txtBrCode.Text.ToString() + "&DT=" + txtAsOnDate.Text + "&rptname=rptDailyCashPosition.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

}