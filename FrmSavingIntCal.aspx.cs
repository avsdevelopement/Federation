using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmSavingIntCal : System.Web.UI.Page
{
    scustom custcs = new scustom();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsSavingIntCal SC = new ClsSavingIntCal();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    double Totalcr = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            WorkingDate.Value = Session["EntryDate"].ToString();
            BindBranch(ddlFromBrName);
            BindBranch(ddlToBrName);
            ddlFromBrName.Focus();
        }
        ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
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
            autoglname.ContextKey = txtFrBranch.Text.Trim().ToString();
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
            autoglname.ContextKey = txtToBranch.Text.Trim().ToString();
            TxtPtype.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BntClear_Click(object sender, EventArgs e)
    {
        try
        {
            ClearData();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnExit_Click(object sender, EventArgs e)
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

    protected void ClearData()
    {
        ddlFromBrName.SelectedValue = "0";
        ddlToBrName.SelectedValue = "0";
        txtFrBranch.Text = "";
        txtToBranch.Text = "";
        TxtPtype.Text = "";
        TxtPname.Text = "";
        TxtFDate.Text = "";
        TxtTDate.Text = "";
        TxtFAccno.Text = "";
        TxtTAccno.Text = "";
        ddlFromBrName.Focus();
    }

    protected void TxtPtype_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtPtype.Text != null)
            {
                int Result = 0;
                int.TryParse(TxtPtype.Text, out Result);
                string GL = custcs.GetProductName(Result.ToString(), Session["BRCD"].ToString());
                string GLS1 = BD.GetAccTypeGL(TxtPtype.Text, Session["BRCD"].ToString());
                if (GLS1 != null)
                {
                    string[] NameC = GLS1.Split('_');
                    TxtPname.Text = NameC[0].ToString();
                    ViewState["DRGL"] = NameC[1].ToString();
                    TxtFDate.Focus();
                }
                else
                {
                    WebMsgBox.Show("Enter valid Product Type!!!!.....", this.Page);
                    TxtPtype.Focus();
                    TxtPtype.Text = "";
                }
            }
            else
            {
                WebMsgBox.Show("Enter valid Product Type!!!!.....", this.Page);
                TxtPtype.Focus();
                TxtPtype.Text = "";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }

    protected void TxtPname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtPname.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TxtPname.Text = CT[0].ToString();
                TxtPtype.Text = CT[1].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtPtype.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
            }
            else
            {
                WebMsgBox.Show("NO Product Types!!!!.....", this.Page);
                TxtPtype.Focus();
                TxtPtype.Text = "";
                TxtPname.Text = "";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnCalculate_Click(object sender, EventArgs e)
    {
        try
        {
            string RES = SC.CalculateSBINT("CALC",ddlCalType.SelectedValue.ToString(),txtFrBranch.Text, txtToBranch.Text, TxtPtype.Text, TxtFAccno.Text, TxtTAccno.Text, TxtFDate.Text, TxtTDate.Text,Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (RES != null)
            {

                WebMsgBox.Show("Calculation Process completed, You can run Trail Report..", this.Page);
                Report.Focus();

            }

            else
            {
                WebMsgBox.Show(RES, this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TrailEntry_Click(object sender, EventArgs e)
    {
        try
        {
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ApplyEntry_Click(object sender, EventArgs e)
    {
        try
        {
            string RES = SC.CalculateSBINT("POST", ddlCalType.SelectedValue.ToString(), txtFrBranch.Text, txtToBranch.Text, TxtPtype.Text, TxtFAccno.Text, TxtTAccno.Text, TxtFDate.Text, TxtTDate.Text, Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (RES != null)
            {

                WebMsgBox.Show(RES, this.Page);
                Report.Focus();

            }

            else
            {
                WebMsgBox.Show(RES, this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Report_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCalType.SelectedValue != "0")
            {
                string redirectURL = "FrmReportViewer.aspx?Flag1=REPORT&Flag2=" + ddlCalType.SelectedValue + "&FBRCD=" + txtFrBranch.Text.Trim().ToString() + "&TBRCD=" + txtToBranch.Text.Trim().ToString() + "&PRCD=" + TxtPtype.Text.Trim().ToString() + "&FACCNO=" + TxtFAccno.Text.Trim().ToString() + "&TACCNO=" + TxtTAccno.Text.Trim().ToString() + "&FDate=" + TxtFDate.Text.ToString() + "&TDate=" + TxtTDate.Text.ToString() + "&rptname=rptSavingIntCalculation.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else
            {
                ddlCalType.Focus();
                lblMessage.Text = "Select calculation type first...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnRecalculate_Click(object sender, EventArgs e)
    {
        try
        {
            int RS = 0;
            RS = SC.Recalculate("RECALC", Session["MID"].ToString(),Session["EntryDate"].ToString(), Session["BRCD"].ToString());
            if (RS > 0)
            {
                WebMsgBox.Show("Calculated data removed, " + Session["LOGINCODE"].ToString() + " can calculate from start...!", this.Page);
                BtnCalculate.Focus();
            }
            else
            {
                WebMsgBox.Show("Data not available on " + Session["EntryDate"].ToString() + " for User " + Session["LOGINCODE"].ToString() + " ...!", this.Page);
                BtnCalculate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void GrdFDInt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}