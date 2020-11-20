using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using Microsoft.ReportingServices;
using System.Data.SqlClient;

public partial class FrmPreSanLoanAPPlist : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    scustom customcs = new scustom();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
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

                BindBranch(ddlFromBrName);
                //added by ankita 07/10/2017 to make user frndly
                ddlFromBrName.SelectedValue = Session["BRCD"].ToString();
                txtFrBranch.Text = ddlFromBrName.SelectedValue.ToString();
                txtFromSancDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
                txtToSancDate.Text = Session["EntryDate"].ToString();
                ddlFromBrName.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindBranch(DropDownList ddlBrName)
    {
        try
        {
            BD.BindBRANCHNAME(ddlBrName, null);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlFromBrName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtFrBranch.Text = "";
            txtFrBranch.Text = ddlFromBrName.SelectedValue.ToString();
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
            if (Rdeatils.SelectedValue == "1")
            {
                FL = "Insert"; 
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "LnSnctn_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                string redirectURL = "FrmRView.aspx?FrBrCode=" + txtFrBranch.Text + "&FrSancDate=" + txtFromSancDate.Text + "&ToSancDate=" + txtToSancDate.Text + "&rptname=RptPreSanLoanAPPList.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (Rdeatils.SelectedValue == "2")
            {
                FL = "Insert";
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "LnSnctn_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                string redirectURL = "FrmRView.aspx?FrBrCode=" + txtFrBranch.Text + "&FrSancDate=" + txtFromSancDate.Text + "&ToSancDate=" + txtToSancDate.Text + "&rptname=RptSanLoanAPPList.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    public void ClearData()
    {
        txtFrBranch.Text = "";
        txtFromSancDate.Text = "";
        txtToSancDate.Text = "";
    }
}