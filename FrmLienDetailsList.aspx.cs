using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using System.IO;

public partial class FrmLienDetailsList : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    scustom customcs = new scustom();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsLoanSanctionReport LS = new ClsLoanSanctionReport();
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
                BindBranch(ddlToBrName);
                //added by ankita 07/10/2017 to make user frndly
                ddlFromBrName.SelectedValue = Session["BRCD"].ToString();
                txtFrBranch.Text = ddlFromBrName.SelectedValue.ToString();
                ddlToBrName.SelectedValue = Session["BRCD"].ToString();
                txtToBranch.Text = ddlToBrName.SelectedValue.ToString();
                txtFromSancDate.Text = Session["EntryDate"].ToString();
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
            autoglname1.ContextKey = txtFrBranch.Text.Trim().ToString();
            ddlToBrName.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlToBrName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtToBranch.Text = "";
            txtToBranch.Text = ddlToBrName.SelectedValue.ToString();
            autoglname2.ContextKey = txtToBranch.Text.Trim().ToString();
            txtFromPrCode.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtFromPrCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = BD.GetAccTypeGL(txtFromPrCode.Text, Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');

            ViewState["DRGL"] = GL[1].ToString();
            string PDName = customcs.GetProductName(txtFromPrCode.Text, Session["BRCD"].ToString());
            if (PDName != null)
            {
                txtFromPrName.Text = PDName;
                txtToPrCode.Focus();
            }
            else
            {
                WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                txtFromPrCode.Text = "";
                txtFromPrCode.Focus();
            }


        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtFromPrName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = txtFromPrName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtFromPrName.Text = CT[0].ToString();
                txtFromPrCode.Text = CT[1].ToString();

                string[] GLS = BD.GetAccTypeGL(txtFromPrCode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();

                txtToPrCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtToPrCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = BD.GetAccTypeGL(txtToPrCode.Text, Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');

            ViewState["DRGL"] = GL[1].ToString();
            string PDName = customcs.GetProductName(txtToPrCode.Text, Session["BRCD"].ToString());
            if (PDName != null)
            {
                txtToPrName.Text = PDName;
            }
            else
            {
                WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                txtToPrCode.Text = "";
                txtToPrCode.Focus();
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtToPrName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = txtToPrName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtToPrName.Text = CT[0].ToString();
                txtToPrCode.Text = CT[1].ToString();

                string[] GLS = BD.GetAccTypeGL(txtToPrCode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
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
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "LnSnctn_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            string redirectURL = "FrmRView.aspx?FrBrCode=" + txtFrBranch.Text + "&ToBrCode=" + txtToBranch.Text + "&FrGlCode=" + txtFromPrCode.Text + "&ToGlCode=" + txtToPrCode.Text + "&AsOndate=" + txtFromSancDate.Text + "&rptname=RptLienMarkList.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}
