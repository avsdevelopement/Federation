using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAVS5170 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAVS5170 Para = new ClsAVS5170();
    ClsCommon cmn = new ClsCommon();
    DataTable DT = new DataTable();
    string sResult = "";
    int Result = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Para.BindBranch(ddlBrName, null);
                txtBrCode.Text = "0";

                ddlBrName.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlBrName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtBrCode.Text = "";
            txtBrCode.Text = ddlBrName.SelectedValue.ToString();

            btnReport.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if ((txtBrCode.Text.ToString() == "0") || (txtBrCode.Text.ToString() == ""))
            {
                ddlBrName.Focus();
                WebMsgBox.Show("Select branch code first ...!!", this.Page);
                return;
            }
            else
            {
                string redirectURL = "FrmReportViewer.aspx?BRCD=" + txtBrCode.Text.ToString() + "&rptname=RptAVS5170.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            ddlBrName.SelectedValue = "0";
            txtBrCode.Text = ddlBrName.SelectedValue;

            ddlBrName.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}