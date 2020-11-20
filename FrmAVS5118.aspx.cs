using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using Microsoft.ReportingServices;
using System.Data.SqlClient;

public partial class FrmAVS5118 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAuthorized Trans = new ClsAuthorized();
    ClsAVS5118 PO = new ClsAVS5118();
    DataTable DT = new DataTable();
    DataTable DT2 = new DataTable();
    DataTable GST = new DataTable();
    string sResult = "";
    int Result = 0;

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindBranch(ddlCrBrName);
                AutoDrGlName.ContextKey = Session["BRCD"].ToString();
                AutoCrGlName.ContextKey = Session["BRCD"].ToString();
                BindIssuedGrid();

                btnAddNew.Focus();
            }
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Select index Change

    protected void ddlCrBrName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AutoCrGlName.ContextKey = ddlCrBrName.SelectedValue.ToString();

            txtCrProdType.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void rbtnType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtDrProdType.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Text Change Events

    protected void txtDrProdType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            sResult = PO.GetProduct(Session["BRCD"].ToString(), txtDrProdType.Text.ToString());

            if (sResult != null)
            {
                if (BD.GetProdOperate(Session["BRCD"].ToString(), txtDrProdType.Text.ToString()).ToString() != "3")
                {
                    string[] ACC = sResult.Split('_'); ;
                    ViewState["DrGlCode"] = ACC[0].ToString();
                    txtDrProdName.Text = ACC[2].ToString();
                    AutoDrAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtDrProdType.Text.ToString();
                    txtTotBalance.Text = PO.GetOpenClose(Session["BRCD"].ToString(), txtCrProdType.Text.Trim().ToString(), "0", "0", Session["EntryDate"].ToString(), "ClBal").ToString();

                    txtDrAccNo.Text = "";
                    txtDrAccName.Text = "";
                    txtDrCustNo.Text = "";
                    txtDrAccNo.Focus();
                }
                else
                {
                    txtDrProdType.Text = "";
                    txtDrProdName.Text = "";
                    txtDrProdType.Focus();
                    WebMsgBox.Show("Product is not operating...!!", this.Page);
                    return;
                }
            }
            else
            {
                txtDrProdType.Text = "";
                txtDrProdName.Text = "";
                txtDrProdType.Focus();
                WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtDrProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] custnob = txtDrProdName.Text.ToString().Split('_');
            if (custnob.Length > 1)
            {
                if (BD.GetProdOperate(Session["BRCD"].ToString(), string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString()).ToString() != "3")
                {
                    txtDrProdName.Text = custnob[0].ToString();
                    ViewState["DrGlCode"] = custnob[1].ToString();
                    txtDrProdType.Text = (string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString());
                    AutoDrAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtDrProdType.Text.ToString();
                    txtTotBalance.Text = PO.GetOpenClose(Session["BRCD"].ToString(), txtCrProdType.Text.Trim().ToString(), "0", "0", Session["EntryDate"].ToString(), "ClBal").ToString();

                    txtDrAccNo.Text = "";
                    txtDrAccName.Text = "";
                    txtDrCustNo.Text = "";
                    txtDrAccNo.Focus();
                }
                else
                {
                    txtDrProdType.Text = "";
                    txtDrProdName.Text = "";
                    txtDrProdType.Focus();
                    WebMsgBox.Show("Product is not operating...!!", this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtDrAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DT = PO.GetAccStage(Session["BRCD"].ToString(), txtDrProdType.Text.ToString(), txtDrAccNo.Text.ToString());
            if (DT.Rows.Count > 0)
            {
                if (DT.Rows[0]["Stage"].ToString() == "1003")
                {
                    if (DT.Rows[0]["Acc_Status"].ToString() != "3")
                    {
                        sResult = PO.GetCustName(Session["BRCD"].ToString(), txtDrProdType.Text, txtDrAccNo.Text);
                        if (sResult.ToString() != "")
                        {
                            txtDrAccName.Text = sResult.ToString();
                            txtDrAccNo.Text = DT.Rows[0]["AccNo"].ToString();
                            txtDrCustNo.Text = DT.Rows[0]["CustNo"].ToString();
                            txtTotBalance.Text = PO.GetOpenClose(Session["BRCD"].ToString(), txtDrProdType.Text.Trim().ToString(), txtDrAccNo.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();

                            txtChequeAmt.Focus();
                        }
                    }
                    else
                    {
                        txtDrAccNo.Text = "";
                        txtDrAccName.Text = "";
                        txtDrCustNo.Text = "";
                        txtDrAccNo.Focus();
                        WebMsgBox.Show("Account is already closed...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    txtDrAccNo.Text = "";
                    txtDrAccName.Text = "";
                    txtDrCustNo.Text = "";
                    txtDrAccNo.Focus();
                    WebMsgBox.Show("Sorry customer not authorise...!!", this.Page);
                    return;
                }
            }
            else
            {
                txtDrAccNo.Text = "";
                txtDrAccName.Text = "";
                txtDrCustNo.Text = "";
                txtDrAccNo.Focus();
                WebMsgBox.Show("Enter valid account number...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtDrAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] AccDet = txtDrAccName.Text.ToString().Split('_');
            if (AccDet.Length > 1)
            {
                DT = PO.GetAccStage(Session["BRCD"].ToString(), txtDrProdType.Text.ToString(), (string.IsNullOrEmpty(AccDet[1].ToString()) ? "" : AccDet[1].ToString()));
                if (DT.Rows.Count > 0)
                {
                    if (DT.Rows[0]["Stage"].ToString() == "1003")
                    {
                        if (DT.Rows[0]["Acc_Status"].ToString() != "3")
                        {
                            sResult = PO.GetCustName(Session["BRCD"].ToString(), txtDrProdType.Text, txtDrAccNo.Text);
                            if (sResult.ToString() != "")
                            {
                                txtDrAccName.Text = sResult.ToString();
                                txtDrAccNo.Text = DT.Rows[0]["AccNo"].ToString();
                                txtDrCustNo.Text = DT.Rows[0]["CustNo"].ToString();
                                txtTotBalance.Text = PO.GetOpenClose(Session["BRCD"].ToString(), txtDrProdType.Text.Trim().ToString(), txtDrAccNo.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();

                                txtChequeAmt.Focus();
                            }
                        }
                        else
                        {
                            txtDrAccNo.Text = "";
                            txtDrAccName.Text = "";
                            txtDrCustNo.Text = "";
                            txtDrAccNo.Focus();
                            WebMsgBox.Show("Account is already closed...!!", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        txtDrAccNo.Text = "";
                        txtDrAccName.Text = "";
                        txtDrCustNo.Text = "";
                        txtDrAccNo.Focus();
                        WebMsgBox.Show("Sorry customer not authorise...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    txtDrAccNo.Text = "";
                    txtDrAccName.Text = "";
                    txtDrCustNo.Text = "";
                    txtDrAccNo.Focus();
                    WebMsgBox.Show("Enter valid account number...!!", this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtCrProdType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            sResult = PO.GetProduct(ddlCrBrName.SelectedValue, txtCrProdType.Text.ToString());

            if (sResult != null)
            {
                if (BD.GetProdOperate(ddlCrBrName.SelectedValue, txtCrProdType.Text.ToString()).ToString() != "3")
                {
                    string[] ACC = sResult.Split('_'); ;
                    ViewState["CrGlCode"] = ACC[0].ToString();
                    txtCrProdName.Text = ACC[2].ToString();

                    txtDDNo.Focus();
                }
                else
                {
                    txtCrProdType.Text = "";
                    txtCrProdName.Text = "";
                    txtCrProdType.Focus();
                    WebMsgBox.Show("Product is not operating...!!", this.Page);
                    return;
                }
            }
            else
            {
                txtCrProdType.Text = "";
                txtCrProdName.Text = "";
                txtCrProdType.Focus();
                WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtCrProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] custnob = txtCrProdName.Text.ToString().Split('_');
            if (custnob.Length > 1)
            {
                if (BD.GetProdOperate(ddlCrBrName.SelectedValue.ToString(), string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString()).ToString() != "3")
                {
                    txtCrProdName.Text = custnob[0].ToString();
                    ViewState["CrGlCode"] = custnob[1].ToString();
                    txtCrProdType.Text = (string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString());

                    txtDDNo.Focus();
                }
                else
                {
                    txtCrProdType.Text = "";
                    txtCrProdName.Text = "";
                    txtCrProdType.Focus();
                    WebMsgBox.Show("Product is not operating...!!", this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtChequeAmt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDouble(txtChequeAmt.Text.ToString() == "" ? "0" : txtChequeAmt.Text.ToString()) <= 0)
            {
                txtChequeAmt.Text = "0";
                txtCharges.Text = "0";
                CalculateGST();
                txtChequeAmt.Focus();
                WebMsgBox.Show("Enter amount greater than zero...!!", this.Page);
                return;
            }
            else if (Convert.ToDouble(txtChequeAmt.Text.ToString() == "" ? "0" : txtChequeAmt.Text.ToString()) >= Convert.ToDouble(txtTotBalance.Text.ToString() == "" ? "0" : txtTotBalance.Text.ToString()))
            {
                txtChequeAmt.Text = "0";
                txtCharges.Text = "0";
                CalculateGST();
                txtChequeAmt.Focus();
                WebMsgBox.Show("Enter amount less than or equal to balance...!!", this.Page);
                return;
            }
            else if (Convert.ToDouble(txtChequeAmt.Text.ToString() == "" ? "0" : txtChequeAmt.Text.ToString()) <= 200000)
            {
                txtCharges.Text = "50";
                CalculateGST();

                txtChequeNo.Focus();
            }
            else if (Convert.ToDouble(txtChequeAmt.Text.ToString() == "" ? "0" : txtChequeAmt.Text.ToString()) > 200000)
            {
                txtCharges.Text = "80";
                CalculateGST();

                txtChequeNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtCharges_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDouble(txtCharges.Text.ToString() == "" ? "0" : txtCharges.Text.ToString()) < 0)
            {
                CalculateGST();
                txtCharges.Focus();
                WebMsgBox.Show("Enter amount zero or greater than zero...!!", this.Page);
                return;
            }
            else if (Convert.ToDouble(txtCharges.Text.ToString() == "" ? "0" : txtCharges.Text.ToString()) >= 0)
            {
                CalculateGST();
                btnSubmit.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Public Function

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

    public void CalculateGST()
    {
        try
        {
            double ChqAmt = Convert.ToDouble(txtChequeAmt.Text.ToString() == "" ? "0" : txtChequeAmt.Text.ToString());
            double Charges = Convert.ToDouble(txtCharges.Text.ToString() == "" ? "0" : txtCharges.Text.ToString());
            double CGSTChrg = Math.Round(Convert.ToSingle(Convert.ToSingle((Charges * 18) / 118) / 2), 2);
            double SGSTChrg = Math.Round(Convert.ToSingle(Convert.ToSingle((Charges * 18) / 118) / 2), 2);
            txtCGSTChrg.Text = CGSTChrg.ToString();
            txtSGSTChrg.Text = SGSTChrg.ToString();
            txtTotalChrg.Text = Convert.ToDouble((Charges - CGSTChrg) - SGSTChrg).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindIssuedGrid()
    {
        try
        {
            PO.BindIssuedGrid(grdIssued, Session["BRCD"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public int ShowDetails(string SrNumber, string sFlag)
    {
        DT = new DataTable();
        try
        {
            DT = PO.GetDetails(Session["BRCD"].ToString(), SrNumber.ToString());
            if (DT.Rows.Count > 0)
            {
                Result = 1;
                rbtnType.SelectedValue = DT.Rows[0]["TransType"].ToString();
                txtDrProdType.Text = DT.Rows[0]["IssueSubGlCode"].ToString();

                string[] CrProdName = PO.GetProduct(DT.Rows[0]["IssueBrCode"].ToString(), DT.Rows[0]["IssueSubGlCode"].ToString()).ToString().Split('_');
                txtDrProdName.Text = CrProdName[2].ToString();

                txtDrAccNo.Text = DT.Rows[0]["IssueAccNo"].ToString();
                txtDrAccName.Text = PO.GetCustName(DT.Rows[0]["IssueBrCode"].ToString(), DT.Rows[0]["IssueSubGlCode"].ToString(), DT.Rows[0]["IssueAccNo"].ToString()).ToString();

                txtDrCustNo.Text = DT.Rows[0]["IssueCustNo"].ToString();
                txtChequeAmt.Text = DT.Rows[0]["ChequeAmt"].ToString();
                txtChequeNo.Text = DT.Rows[0]["ChequeNo"].ToString();
                txtTotBalance.Text = PO.GetOpenClose(DT.Rows[0]["IssueBrCode"].ToString(), DT.Rows[0]["IssueSubGlCode"].ToString(), DT.Rows[0]["IssueAccNo"].ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();

                ddlCrBrName.SelectedValue = DT.Rows[0]["CrBrCode"].ToString();
                txtCrProdType.Text = DT.Rows[0]["CrSubGlCode"].ToString();

                string[] DrProdName = PO.GetProduct(DT.Rows[0]["CrBrCode"].ToString(), DT.Rows[0]["CrSubGlCode"].ToString()).ToString().Split('_');
                txtCrProdName.Text = DrProdName[2].ToString();

                txtDDNo.Text = DT.Rows[0]["PayOrderNo"].ToString();
                txtBenefName.Text = DT.Rows[0]["BenefName"].ToString();
                txtNarration.Text = DT.Rows[0]["Narration"].ToString();
                txtCharges.Text = DT.Rows[0]["ChargesAmt"].ToString();
                txtCGSTChrg.Text = DT.Rows[0]["CGSTAmt"].ToString();
                txtSGSTChrg.Text = DT.Rows[0]["SGSTAmt"].ToString();
                txtTotalChrg.Text = DT.Rows[0]["TotalAmt"].ToString();
            }
            else
            {
                Result = 0;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public void ClearAllData()
    {
        try
        {
            txtDrProdType.Text = "";
            txtDrProdName.Text = "";
            txtDrAccNo.Text = "";
            txtDrAccName.Text = "";
            txtDrCustNo.Text = "";
            txtTotBalance.Text = "";
            txtChequeAmt.Text = "";
            txtChequeNo.Text = "";

            ddlCrBrName.SelectedIndex = 0;
            txtCrProdType.Text = "";
            txtCrProdName.Text = "";
            txtDDNo.Text = "";

            txtBenefName.Text = "";
            txtNarration.Text = "";
            txtCharges.Text = "";
            txtCGSTChrg.Text = "";
            txtSGSTChrg.Text = "";
            txtTotalChrg.Text = "";

            divNewInfo.Visible = true;
            divDetailInfo.Visible = false;
            divAuthoDetail.Visible = true;
            btnSubmit.Visible = false;
            btnAuthorize.Visible = false;
            btnDelete.Visible = false;
            btnClear.Visible = false;
            btnExit.Visible = false;
            BindIssuedGrid();

            btnAddNew.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public string ChangeToWords(int number)
    {
        string words = "";
        try
        {
            if (number == 0)
                return "Zero";
            if (number < 0)
                return "minus " + ChangeToWords(Math.Abs(number));

            if ((number / 1000000000) > 0)
            {
                words += ChangeToWords(number / 1000000000) + " Billion";
                number %= 1000000000;
            }

            if ((number / 10000000) > 0)
            {
                words += ChangeToWords(number / 10000000) + " Crore";
                number %= 10000000;
            }

            if ((number / 100000) > 0)
            {
                words += ChangeToWords(number / 100000) + " Lack";
                number %= 100000;
            }
            if ((number / 1000) > 0)
            {
                words += ChangeToWords(number / 1000) + " Thousand";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ChangeToWords(number / 100) + " Hundred";
                number %= 100;
            }
            if (number > 0)
            {
                if (words != "")
                    words += " And";
                var unitsMap = new[] { " Zero", " One", " Two", " Three", " Four", " Five", " Six", " Seven", " Eight", " Nine", " Ten", " Eleven", " Twelve", " Thirteen", " Fourteen", " Fifteen", " Sixteen", " Seventeen", " Eighteen", " Nineteen" };
                var tensMap = new[] { " Zero", " Ten", " Twenty", " Thirty", " Forty", " Fifty", " Sixty", " Seventy", " Eighty", " Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "" + unitsMap[number % 10];
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return words;
    }

    #endregion

    #region Click Events

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAllData();
            BindBranch(ddlCrBrName);
            btnSubmit.Visible = true;
            btnClear.Visible = true;
            btnExit.Visible = true;
            divNewInfo.Visible = false;
            divAuthoDetail.Visible = false;
            divDetailInfo.Visible = true;
            AutoDrGlName.ContextKey = Session["BRCD"].ToString();
            AutoCrGlName.ContextKey = Session["BRCD"].ToString();

            txtDrProdType.Focus();
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
            if (txtDrProdType.Text.ToString() == "")
            {
                txtDrProdType.Focus();
                WebMsgBox.Show("Enter debit product code first...!!", this.Page);
                return;
            }
            else if (txtDrAccNo.Text.ToString() == "")
            {
                txtDrAccNo.Focus();
                WebMsgBox.Show("Enter debit account no first...!!", this.Page);
                return;
            }
            else if (txtChequeAmt.Text.ToString() == "")
            {
                txtChequeAmt.Focus();
                WebMsgBox.Show("Enter cheque amount first...!!", this.Page);
                return;
            }
            else if (txtChequeNo.Text.ToString() == "")
            {
                txtChequeNo.Focus();
                WebMsgBox.Show("Enter cheque number first...!!", this.Page);
                return;
            }
            else if (ddlCrBrName.SelectedValue.ToString() == "0")
            {
                ddlCrBrName.Focus();
                WebMsgBox.Show("Select credit branch first...!!", this.Page);
                return;
            }
            else if (txtCrProdType.Text.ToString() == "")
            {
                txtCrProdType.Focus();
                WebMsgBox.Show("Enter credit product code first...!!", this.Page);
                return;
            }
            else if (txtDDNo.Text.ToString() == "")
            {
                txtDDNo.Focus();
                WebMsgBox.Show("Enter DD/PO number first...!!", this.Page);
                return;
            }
            else
            {
                if (txtChequeNo.Text.ToString() == "")
                    txtChequeNo.Text = "0";
                if (txtDDNo.Text.ToString() == "")
                    txtDDNo.Text = "0";
                if (txtCharges.Text.ToString() == "")
                    txtCharges.Text = "0";
                if (txtCGSTChrg.Text.ToString() == "")
                    txtCGSTChrg.Text = "0";
                if (txtSGSTChrg.Text.ToString() == "")
                    txtSGSTChrg.Text = "0";
                if (txtTotalChrg.Text.ToString() == "")
                    txtTotalChrg.Text = "0";

                Result = PO.IssuePayOrder(rbtnType.SelectedValue, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DrGlCode"].ToString(), txtDrProdType.Text.ToString(),
                    txtDrAccNo.Text.ToString(), txtDrCustNo.Text.ToString(), txtDrAccName.Text.ToString(), txtChequeAmt.Text.ToString(), txtChequeNo.Text.ToString(), ddlCrBrName.SelectedValue.ToString(), ViewState["CrGlCode"].ToString(),
                    txtCrProdType.Text.ToString(), txtDDNo.Text.ToString(), "01/01/1900", txtCharges.Text.ToString(), txtCGSTChrg.Text.ToString(), txtSGSTChrg.Text.ToString(), txtTotalChrg.Text.ToString(),
                    txtBenefName.Text.ToString(), txtNarration.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                if (Result > 0)
                {
                    ClearAllData();
                    WebMsgBox.Show("Successfully Issued...!!", this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkAutorise_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton LnkEdit = (LinkButton)sender;
            string[] Info = LnkEdit.CommandArgument.ToString().Split(',');
            ViewState["SrNumber"] = Info[0].ToString();
            ViewState["Mid"] = Info[1].ToString();

            if (ViewState["Mid"].ToString() != Session["MID"].ToString())
            {
                Result = ShowDetails(ViewState["SrNumber"].ToString(), "AT");
                if (Result > 0)
                {
                    divNewInfo.Visible = false;
                    divDetailInfo.Visible = true;
                    divAuthoDetail.Visible = false;
                    btnAuthorize.Visible = true;
                    btnDelete.Visible = false;
                    btnExit.Visible = true;

                    btnAuthorize.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("User is restricted to authorize...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnAuthorize_Click(object sender, EventArgs e)
    {
        string SetNo = "", RefId = "";
        string GlCode = "", SubGlCode = "";
        try
        {
            if (Convert.ToDouble(txtChequeAmt.Text.ToString() == "" ? "0" : txtChequeAmt.Text.ToString()) >= Convert.ToDouble(txtTotBalance.Text.ToString() == "" ? "0" : txtTotBalance.Text.ToString()))
            {
                btnAuthorize.Focus();
                WebMsgBox.Show("Enter amount less than or equal to balance...!!", this.Page);
                return;
            }
            else
            {
                DT = new DataTable();
                GST = new DataTable();
                DT = PO.GetDetails(Session["BRCD"].ToString(), ViewState["SrNumber"].ToString());
                GST = PO.GstDetails(Session["BRCD"].ToString());

                if (DT.Rows.Count > 0 && GST.Rows.Count > 0)
                {
                    if (DT.Rows[0]["IssueBrCode"].ToString() != DT.Rows[0]["CrBrCode"].ToString())
                    {
                        SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "IBTSetNo", Session["BRCD"].ToString()).ToString();
                        RefId = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                        ViewState["RID"] = (Convert.ToInt32(RefId) + 1).ToString();
                    }
                    else
                    {
                        SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
                        RefId = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                        ViewState["RID"] = (Convert.ToInt32(RefId) + 1).ToString();
                    }

                    if (Convert.ToDouble(SetNo) > 0)
                    {
                        double TotalAmount = Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["ChequeAmt"].ToString()) + Convert.ToDouble(DT.Rows[0]["ChargesAmt"].ToString()));

                        //  For Total Debit Amount Voucher
                        Result = Trans.Authorized(DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueGlCode"].ToString(), DT.Rows[0]["IssueSubGlCode"].ToString(),
                                 DT.Rows[0]["IssueAccNo"].ToString(), DT.Rows[0]["Narration"].ToString(), DT.Rows[0]["TransType"].ToString(), TotalAmount.ToString(), "2", "7", "TR", SetNo, DT.Rows[0]["ChequeNo"].ToString(), DT.Rows[0]["IssueDate"].ToString(),
                                 "0", "0", "1003", "", DT.Rows[0]["IssueBrCode"].ToString(), DT.Rows[0]["Mid"].ToString(), "0", "0", "", DT.Rows[0]["IssueCustNo"].ToString(), DT.Rows[0]["IssueCustName"].ToString(), ViewState["RID"].ToString(), "0", "0");

                        //  For Total Credit Amount Voucher
                        if (Result > 0)
                        {
                            //  Bank contra if debit and credit branch are not same
                            if (DT.Rows[0]["IssueBrCode"].ToString() != DT.Rows[0]["CrBrCode"].ToString())
                            {
                                if (DT.Rows[0]["IssueBrCode"].ToString() == "1" || DT.Rows[0]["CrBrCode"].ToString() == "1")
                                {
                                    if (Result > 0)
                                    {
                                        DT2 = new DataTable();
                                        DT2 = PO.GetADMSubGl(DT.Rows[0]["CrBrCode"].ToString());
                                        Result = Trans.Authorized(DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(),
                                             "0", DT.Rows[0]["Narration"].ToString(), DT.Rows[0]["TransType"].ToString(), TotalAmount.ToString(), "1", "7", "TR", SetNo, DT.Rows[0]["ChequeNo"].ToString(), DT.Rows[0]["IssueDate"].ToString(), "0", "0", "1003",
                                             "", DT.Rows[0]["IssueBrCode"].ToString(), DT.Rows[0]["Mid"].ToString(), "0", "0", "", DT.Rows[0]["IssueCustNo"].ToString(), DT.Rows[0]["IssueCustName"].ToString(), ViewState["RID"].ToString(), "0", "0");
                                    }

                                    if (Result > 0)
                                    {
                                        DT2 = new DataTable();
                                        DT2 = PO.GetADMSubGl(DT.Rows[0]["IssueBrCode"].ToString());
                                        Result = Trans.Authorized(DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(),
                                             "0", DT.Rows[0]["Narration"].ToString(), DT.Rows[0]["TransType"].ToString(), TotalAmount.ToString(), "2", "7", "TR", SetNo, DT.Rows[0]["ChequeNo"].ToString(), DT.Rows[0]["IssueDate"].ToString(), "0", "0", "1003",
                                             "", DT.Rows[0]["CrBrCode"].ToString(), DT.Rows[0]["Mid"].ToString(), "0", "0", "", DT.Rows[0]["IssueCustNo"].ToString(), DT.Rows[0]["IssueCustName"].ToString(), ViewState["RID"].ToString(), "0", "0");
                                    }
                                }
                                else
                                {
                                    if (Result > 0)
                                    {
                                        DT2 = new DataTable();
                                        DT2 = PO.GetADMSubGl("1");
                                        Result = Trans.Authorized(DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(),
                                             "0", DT.Rows[0]["Narration"].ToString(), DT.Rows[0]["TransType"].ToString(), TotalAmount.ToString(), "1", "7", "TR", SetNo, DT.Rows[0]["ChequeNo"].ToString(), DT.Rows[0]["IssueDate"].ToString(), "0", "0", "1003",
                                             "", DT.Rows[0]["IssueBrCode"].ToString(), DT.Rows[0]["Mid"].ToString(), "0", "0", "", DT.Rows[0]["IssueCustNo"].ToString(), DT.Rows[0]["IssueCustName"].ToString(), ViewState["RID"].ToString(), "0", "0");
                                    }

                                    if (Result > 0)
                                    {
                                        DT2 = new DataTable();
                                        DT2 = PO.GetADMSubGl(DT.Rows[0]["IssueBrCode"].ToString());
                                        Result = Trans.Authorized(DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(),
                                             "0", DT.Rows[0]["Narration"].ToString(), DT.Rows[0]["TransType"].ToString(), TotalAmount.ToString(), "2", "7", "TR", SetNo, DT.Rows[0]["ChequeNo"].ToString(), DT.Rows[0]["IssueDate"].ToString(), "0", "0", "1003",
                                             "", "1", DT.Rows[0]["Mid"].ToString(), "0", "0", "", DT.Rows[0]["IssueCustNo"].ToString(), DT.Rows[0]["IssueCustName"].ToString(), ViewState["RID"].ToString(), "0", "0");
                                    }

                                    if (Result > 0)
                                    {
                                        DT2 = new DataTable();
                                        DT2 = PO.GetADMSubGl(DT.Rows[0]["CrBrCode"].ToString());
                                        Result = Trans.Authorized(DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(),
                                             "0", DT.Rows[0]["Narration"].ToString(), DT.Rows[0]["TransType"].ToString(), TotalAmount.ToString(), "1", "7", "TR", SetNo, DT.Rows[0]["ChequeNo"].ToString(), DT.Rows[0]["IssueDate"].ToString(), "0", "0", "1003",
                                             "", "1", DT.Rows[0]["Mid"].ToString(), "0", "0", "", DT.Rows[0]["IssueCustNo"].ToString(), DT.Rows[0]["IssueCustName"].ToString(), ViewState["RID"].ToString(), "0", "0");
                                    }

                                    if (Result > 0)
                                    {
                                        DT2 = new DataTable();
                                        DT2 = PO.GetADMSubGl("1");
                                        Result = Trans.Authorized(DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(),
                                             "0", DT.Rows[0]["Narration"].ToString(), DT.Rows[0]["TransType"].ToString(), TotalAmount.ToString(), "2", "7", "TR", SetNo, DT.Rows[0]["ChequeNo"].ToString(), DT.Rows[0]["IssueDate"].ToString(), "0", "0", "1003",
                                             "", DT.Rows[0]["CrBrCode"].ToString(), DT.Rows[0]["Mid"].ToString(), "0", "0", "", DT.Rows[0]["IssueCustNo"].ToString(), DT.Rows[0]["IssueCustName"].ToString(), ViewState["RID"].ToString(), "0", "0");
                                    }
                                }
                            }

                            //  For Cheque Amount
                            if (Convert.ToDouble(DT.Rows[0]["ChequeAmt"].ToString()) > 0)
                            {
                                Result = Trans.Authorized(DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["CrGlCode"].ToString(), DT.Rows[0]["CrSubGlCode"].ToString(),
                                         "0", DT.Rows[0]["Narration"].ToString(), DT.Rows[0]["TransType"].ToString(), DT.Rows[0]["ChequeAmt"].ToString(), "1", "7", "TR", SetNo, DT.Rows[0]["ChequeNo"].ToString(), DT.Rows[0]["IssueDate"].ToString(),
                                         "0", "0", "1003", "", DT.Rows[0]["CrBrCode"].ToString(), DT.Rows[0]["Mid"].ToString(), "0", "0", "", DT.Rows[0]["IssueCustNo"].ToString(), DT.Rows[0]["IssueCustName"].ToString(), ViewState["RID"].ToString(), "0", "0");
                            }

                            //  For CGST charges
                            if (Result > 0 && Convert.ToDouble(DT.Rows[0]["CGSTAmt"].ToString()) > 0)
                            {
                                GlCode = PO.GetGlCode(DT.Rows[0]["CrBrCode"].ToString(), GST.Rows[0]["CGSTPrdCd"].ToString());
                                Result = Trans.Authorized(DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), GlCode.ToString(), GST.Rows[0]["CGSTPrdCd"].ToString(),
                                     "0", DT.Rows[0]["Narration"].ToString(), DT.Rows[0]["TransType"].ToString(), DT.Rows[0]["CGSTAmt"].ToString(), "1", "7", "TR", SetNo, DT.Rows[0]["ChequeNo"].ToString(), DT.Rows[0]["IssueDate"].ToString(),
                                     "0", "0", "1003", "", DT.Rows[0]["CrBrCode"].ToString(), DT.Rows[0]["Mid"].ToString(), "0", "0", "", DT.Rows[0]["IssueCustNo"].ToString(), DT.Rows[0]["IssueCustName"].ToString(), ViewState["RID"].ToString(), "0", "0");
                            }

                            //  For SGST charges
                            if (Result > 0 && Convert.ToDouble(DT.Rows[0]["SGSTAmt"].ToString()) > 0)
                            {
                                GlCode = PO.GetGlCode(DT.Rows[0]["CrBrCode"].ToString(), GST.Rows[0]["SGSTPrdCd"].ToString());
                                Result = Trans.Authorized(DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), GlCode.ToString(), GST.Rows[0]["SGSTPrdCd"].ToString(),
                                     "0", DT.Rows[0]["Narration"].ToString(), DT.Rows[0]["TransType"].ToString(), DT.Rows[0]["SGSTAmt"].ToString(), "1", "7", "TR", SetNo, DT.Rows[0]["ChequeNo"].ToString(), DT.Rows[0]["IssueDate"].ToString(),
                                     "0", "0", "1003", "", DT.Rows[0]["CrBrCode"].ToString(), DT.Rows[0]["Mid"].ToString(), "0", "0", "", DT.Rows[0]["IssueCustNo"].ToString(), DT.Rows[0]["IssueCustName"].ToString(), ViewState["RID"].ToString(), "0", "0");
                            }

                            //  For Service charges
                            if (Result > 0 && Convert.ToDouble(DT.Rows[0]["TotalAmt"].ToString()) > 0)
                            {
                                SubGlCode = PO.GetSubGlCode(DT.Rows[0]["CrBrCode"].ToString());
                                GlCode = PO.GetGlCode(DT.Rows[0]["CrBrCode"].ToString(), SubGlCode.ToString());
                                Result = Trans.Authorized(DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), DT.Rows[0]["IssueDate"].ToString(), GlCode.ToString(), SubGlCode.ToString(),
                                     "0", DT.Rows[0]["Narration"].ToString(), DT.Rows[0]["TransType"].ToString(), DT.Rows[0]["TotalAmt"].ToString(), "1", "7", "TR", SetNo, DT.Rows[0]["ChequeNo"].ToString(), DT.Rows[0]["IssueDate"].ToString(),
                                     "0", "0", "1003", "", DT.Rows[0]["CrBrCode"].ToString(), DT.Rows[0]["Mid"].ToString(), "0", "0", "", DT.Rows[0]["IssueCustNo"].ToString(), DT.Rows[0]["IssueCustName"].ToString(), ViewState["RID"].ToString(), "0", "0");
                            }

                            Result = PO.AuthorizePayOrder(Session["BRCD"].ToString(), ViewState["SrNumber"].ToString(), SetNo.ToString(), Session["MID"].ToString());
                            if (Result > 0)
                            {
                                ClearAllData();
                                WebMsgBox.Show("Successfully authorize with setno : " + SetNo, this.Page);
                                return;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton LnkEdit = (LinkButton)sender;
            string[] Info = LnkEdit.CommandArgument.ToString().Split(',');
            ViewState["SrNumber"] = Info[0].ToString();
            ViewState["Mid"] = Info[1].ToString();

            if (ViewState["Mid"].ToString() != Session["MID"].ToString())
            {
                Result = ShowDetails(ViewState["SrNumber"].ToString(), "AT");
                if (Result > 0)
                {
                    divNewInfo.Visible = false;
                    divDetailInfo.Visible = true;
                    divAuthoDetail.Visible = false;
                    btnDelete.Visible = true;
                    btnAuthorize.Visible = false;
                    btnExit.Visible = true;

                    btnDelete.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("User is restricted to delete...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            Result = PO.DeletePayOrder(Session["BRCD"].ToString(), ViewState["SrNumber"].ToString(), Session["MID"].ToString());
            if (Result > 0)
            {
                ClearAllData();
                WebMsgBox.Show("Successfully Deleted...!!", this.Page);
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
            ClearAllData();
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
            return;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

}