using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Win32;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using Microsoft.ReportingServices;
using System.Data.SqlClient;
using System.Windows.Forms;

public partial class FrmAccnoUpdate : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    CLsCustNoChanges CLS = new CLsCustNoChanges();
    ClsStatementView SV = new ClsStatementView();
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sResult = "";
    int Result = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AutoProdName.ContextKey = Session["BRCD"].ToString();
                BindBranch(ddlBrName);
                ddlBrName.SelectedValue = Session["BRCD"].ToString();
                txtBrCode.Text = Session["BRCD"].ToString();

                txtProdCode.Focus();
            }
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
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
            ClearAll();
            AutoProdName.ContextKey = txtBrCode.Text.Trim().ToString();

            txtProdCode.Focus();
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

    protected void txtProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Clear();
            sResult = SV.GetProductName(txtBrCode.Text.ToString(), txtProdCode.Text.ToString());
            if (sResult != null)
            {
                string[] CT = sResult.Split('_');

                if (CT.Length > 0)
                {
                    txtProdName.Text = CT[0].ToString();
                    txtProdCode.Text = CT[2].ToString();
                    ViewState["GlCode"] = CT[1].ToString();
                    txtAccNo.Text = "";
                    txtAccName.Text = "";

                    AutoCustName.ContextKey = txtBrCode.Text.ToString() + "_" + txtProdCode.Text.ToString();
                    sResult = SV.GetAccYN(txtBrCode.Text.ToString(), txtProdCode.Text.ToString());

                    if (ViewState["GlCode"].ToString() == "3")
                    {
                        DivAmount.Visible = true;
                        DivPeriod.Visible = true;
                        DivIntRate.Visible = true;
                        DivOverDue.Visible = true;
                        LblName1.InnerText = "Sanction Amt";
                    }
                    else if (ViewState["GlCode"].ToString() == "5")
                    {
                        DivAmount.Visible = true;
                        DivPeriod.Visible = true;
                        DivIntRate.Visible = true;
                        DivOverDue.Visible = false;
                        LblName1.InnerText = "Deposit Amt";
                    }
                    else
                    {
                        DivAmount.Visible = false;
                        DivPeriod.Visible = false;
                        DivIntRate.Visible = false;
                        DivOverDue.Visible = false;
                    }

                    //  Show clear and unclear blance
                    txtClearBal.Text = SV.GetOpenClose(txtBrCode.Text.ToString(), txtProdCode.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();

                    if (Convert.ToDouble(ViewState["GlCode"].ToString()) >= 100 && sResult != "Y")
                    {
                    }
                    else
                    {
                        txtAccNo.Focus();
                    }
                }
                else
                {
                    txtProdCode.Text = "";
                    txtAccNo.Text = "";
                    txtAccName.Text = "";
                    WebMsgBox.Show("Sorry product not exists ....!", this.Page);
                    txtProdCode.Focus();
                }
            }
            else
            {
                txtProdCode.Text = "";
                txtAccNo.Text = "";
                txtAccName.Text = "";
                WebMsgBox.Show("Sorry product not exists ....!", this.Page);
                txtProdCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Clear();
            sResult = txtProdName.Text.ToString();
            string[] CT = sResult.Split('_');

            if (CT.Length > 0)
            {
                txtProdName.Text = CT[0].ToString();
                txtProdCode.Text = CT[2].ToString();
                ViewState["GlCode"] = CT[1].ToString();
                txtAccNo.Text = "";
                txtAccName.Text = "";
                Clear();

                AutoCustName.ContextKey = txtBrCode.Text.ToString() + "_" + txtProdCode.Text.ToString();
                sResult = SV.GetAccYN(txtBrCode.Text.ToString(), txtProdCode.Text.ToString());

                if (ViewState["GlCode"].ToString() == "3")
                {
                    DivAmount.Visible = true;
                    DivPeriod.Visible = true;
                    DivIntRate.Visible = true;
                    DivOverDue.Visible = true;
                    LblName1.InnerText = "Loan Amt";
                }
                else if (ViewState["GlCode"].ToString() == "5")
                {
                    DivAmount.Visible = true;
                    DivPeriod.Visible = true;
                    DivIntRate.Visible = true;
                    DivOverDue.Visible = false;
                    LblName1.InnerText = "DepositAmt";
                }
                else
                {
                    DivAmount.Visible = false;
                    DivPeriod.Visible = false;
                    DivIntRate.Visible = false;
                    DivOverDue.Visible = false;
                }

                //  Show clear and unclear blance
                txtClearBal.Text = SV.GetOpenClose(txtBrCode.Text.ToString(), txtProdCode.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();

                if (Convert.ToDouble(ViewState["GlCode"].ToString()) >= 100 && sResult != "Y")
                {
                }
                else
                {
                    txtAccNo.Focus();
                }
            }
            else
            {
                txtProdCode.Text = "";
                txtAccNo.Text = "";
                txtAccName.Text = "";
                WebMsgBox.Show("Product Not Exits ....!", this.Page);
                txtProdCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Clear();
            sResult = SV.GetAccountName(txtBrCode.Text.ToString(), txtProdCode.Text.ToString(), txtAccNo.Text.ToString());
            if (sResult != null)
            {
                string[] CT = sResult.Split('_');

                if (CT.Length > 0)
                {
                    txtAccName.Text = CT[0].ToString();
                    txtAccNo.Text = CT[1].ToString();

                    sResult = SV.GetAccDetails(txtBrCode.Text.ToString(), txtProdCode.Text.ToString(), txtAccNo.Text.ToString());
                    string[] CT1 = sResult.Split('_');

                    if (CT1.Length > 0)
                    {
                        txtAccOpenDate.Text = CT1[0].ToString();
                        txtAccStatus.Text = CT1[1].ToString();
                        txtClearBal.Text = SV.GetOpenClose(txtBrCode.Text.ToString(), txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();

                        if (ViewState["GlCode"].ToString() == "3")
                        {
                            DT = SV.AccountInfo(txtBrCode.Text.ToString(), "3", txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                            if (DT.Rows.Count > 0)
                            {
                                txtLimitAmt.Text = DT.Rows[0]["Limit"].ToString();
                                txtPeriod.Text = DT.Rows[0]["Period"].ToString();
                                txtIntRate.Text = DT.Rows[0]["IntRate"].ToString();
                                txtOverDue.Text = SV.OVerdueAmt(txtBrCode.Text.ToString(), txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Convert.ToDouble(txtClearBal.Text.ToString()), Session["EntryDate"].ToString()).ToString();
                            }
                        }
                        else if (ViewState["GlCode"].ToString() == "5")
                        {
                            DT = SV.AccountInfo(txtBrCode.Text.ToString(), "5", txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                            if (DT.Rows.Count > 0)
                            {
                                txtLimitAmt.Text = DT.Rows[0]["PrnAmt"].ToString();
                                txtPeriod.Text = DT.Rows[0]["Period"].ToString();
                                txtIntRate.Text = DT.Rows[0]["RateOfInt"].ToString();
                            }
                        }
                    }
                }
                else
                {
                    WebMsgBox.Show("Customer Not Exits...!!", this.Page);
                    txtAccName.Text = "";
                    txtAccNo.Text = "";
                    TxtNewAccNo.Focus();
                }
                TxtNewAccNo.Focus(); 
            }
            else
            {
                WebMsgBox.Show("Account Not Exits...!!", this.Page);
                txtAccName.Text = "";
                txtAccNo.Text = "";
                TxtNewAccNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Clear();
            sResult = txtAccName.Text.ToString();
            string[] CT = sResult.Split('_');

            if (CT.Length > 0)
            {
                txtAccName.Text = CT[0].ToString();
                txtAccNo.Text = CT[1].ToString();

                sResult = SV.GetAccDetails(txtBrCode.Text.ToString(), txtProdCode.Text.ToString(), txtAccNo.Text.ToString());
                string[] CT1 = sResult.Split('_');

                if (CT1.Length > 0)
                {
                    txtAccOpenDate.Text = CT1[0].ToString();
                    txtAccStatus.Text = CT1[1].ToString();
                    txtClearBal.Text = SV.GetOpenClose(txtBrCode.Text.ToString(), txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();

                    if (ViewState["GlCode"].ToString() == "3")
                    {
                        DT = SV.AccountInfo(txtBrCode.Text.ToString(), "3", txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                        if (DT.Rows.Count > 0)
                        {
                            txtLimitAmt.Text = DT.Rows[0]["Limit"].ToString();
                            txtPeriod.Text = DT.Rows[0]["Period"].ToString();
                            txtIntRate.Text = DT.Rows[0]["IntRate"].ToString();
                        }
                    }
                    else if (ViewState["GlCode"].ToString() == "5")
                    {
                        DT = SV.AccountInfo(txtBrCode.Text.ToString(), "5", txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                        if (DT.Rows.Count > 0)
                        {
                            txtLimitAmt.Text = DT.Rows[0]["PrnAmt"].ToString();
                            txtPeriod.Text = DT.Rows[0]["Period"].ToString();
                            txtIntRate.Text = DT.Rows[0]["RateOfInt"].ToString();
                        }
                    }
                }
            }
            else
            {
                WebMsgBox.Show("Customer Not Exits...!!", this.Page);
                txtAccName.Text = "";
                txtAccNo.Text = "";
                txtAccNo.Focus();
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
            int Result = CLS.UpdateAccnoDT(txtBrCode.Text, txtProdCode.Text, txtAccNo.Text, TxtNewAccNo.Text);
            if (Result > 0)
            {
                ClearAll();
                lblMessage.Text = "Account Number Updated Successfully ....!!!";
                ModalPopup.Show(this.Page);
                return;

            }
            else
            {
                lblMessage.Text = "Account Number Not Updated Please Check Fields ...!!";
                ModalPopup.Show(this.Page);
                return;

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
            ClearAll();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void ClearAll()
    {
        try
        {
            txtProdCode.Text = "";
            txtProdName.Text = "";
            txtAccNo.Text = "";
            txtAccName.Text = "";
            txtClearBal.Text = "";
            txtAccOpenDate.Text = "";
            txtAccStatus.Text = "";
            txtLimitAmt.Text = "";
            txtPeriod.Text = "";
            txtIntRate.Text = "";
            txtOverDue.Text = "";
            txtSpecialInst.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void Clear()
    {
        try
        {
            //  Clear all fields
            txtClearBal.Text = "";
            txtAccOpenDate.Text = "";
            txtAccStatus.Text = "";
            txtLimitAmt.Text = "";
            txtPeriod.Text = "";
            txtIntRate.Text = "";
            txtOverDue.Text = "";
            txtSpecialInst.Text = "";
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
    protected void TxtNewAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Clear();
            sResult = txtAccName.Text.ToString();
            string[] CT = sResult.Split('_');

            if (CT.Length > 0)
            {
                txtAccName.Text = CT[0].ToString();
                txtAccNo.Text = CT[1].ToString();

                sResult = SV.GetAccDetails(txtBrCode.Text.ToString(), txtProdCode.Text.ToString(), txtAccNo.Text.ToString());
                string[] CT1 = sResult.Split('_');

                if (CT1.Length > 0)
                {
                    txtAccOpenDate.Text = CT1[0].ToString();
                    txtAccStatus.Text = CT1[1].ToString();
                    txtClearBal.Text = SV.GetOpenClose(txtBrCode.Text.ToString(), txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();

                    if (ViewState["GlCode"].ToString() == "3")
                    {
                        DT = SV.AccountInfo(txtBrCode.Text.ToString(), "3", txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                        if (DT.Rows.Count > 0)
                        {
                            txtLimitAmt.Text = DT.Rows[0]["Limit"].ToString();
                            txtPeriod.Text = DT.Rows[0]["Period"].ToString();
                            txtIntRate.Text = DT.Rows[0]["IntRate"].ToString();
                        }
                    }
                    else if (ViewState["GlCode"].ToString() == "5")
                    {
                        DT = SV.AccountInfo(txtBrCode.Text.ToString(), "5", txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                        if (DT.Rows.Count > 0)
                        {
                            txtLimitAmt.Text = DT.Rows[0]["PrnAmt"].ToString();
                            txtPeriod.Text = DT.Rows[0]["Period"].ToString();
                            txtIntRate.Text = DT.Rows[0]["RateOfInt"].ToString();
                        }
                    }
                }
            }
            else
            {
                WebMsgBox.Show("Customer Not Exists ...!!", this.Page);
                txtAccName.Text = "";
                txtAccNo.Text = "";
                txtAccNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtNewAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //Clear();
            sResult = SV.GetAccountName(txtBrCode.Text.ToString(), txtProdCode.Text.ToString(), TxtNewAccNo.Text.ToString());
            if (sResult != null)
            {
                WebMsgBox.Show("Account Already Exits...!!", this.Page);
                TxtNewAccName.Text = "";
                TxtNewAccNo.Text = "";
                TxtNewAccNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}