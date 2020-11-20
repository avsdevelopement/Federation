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

public partial class FrmAVS5083 : System.Web.UI.Page
{
    ClsInsertTrans Trans = new ClsInsertTrans();
    ClsBindDropdown BD = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    DataTable dtFirst = new DataTable();
    ClsAVS5083 SR = new ClsAVS5083();
    DataTable DT1 = new DataTable();
    DataTable DT = new DataTable();
    string AT = "", FL = "", sResult = "";
    string RefNumber = "", SubGlCode = "", GlCode = "", SetNo = "";
    string Activity1 = "", PmtMode1 = "";
    string Activity2 = "", PmtMode2 = "";
    int Result = 0;

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BD.BindAccStatus(ddlAccStatus);
            autoglname.ContextKey = Session["BRCD"].ToString();
            txtProdType.Focus();
        }
    }

    #endregion

    #region Text Changed

    protected void txtProdType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;
            if (BD.GetProdOperate(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString()).ToString() != "3")
            {
                AC1 = SR.Getaccno(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString());

                if (AC1 != null)
                {
                    string[] AC = AC1.Split('_'); ;
                    ViewState["GlCode"] = AC[0].ToString();
                    txtProdName.Text = AC[1].ToString();
                    AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType.Text + "_" + ViewState["GlCode"].ToString();
                    ClearAccText();

                    txtAccNo.Focus();
                }
                else
                {
                    lblMessage.Text = "Enter Valid Product code...!!";
                    ModalPopup.Show(this.Page);
                    ClearProdText();
                    txtProdType.Focus();
                }
            }
            else
            {
                txtProdType.Text = "";
                txtProdName.Text = "";
                lblMessage.Text = "Product is not operating...!!";
                ModalPopup.Show(this.Page);
                return;
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
            string[] TD = txtProdName.Text.Split('_');
            if (TD.Length > 1)
            {
                if (BD.GetProdOperate(Session["BRCD"].ToString(), TD[1].ToString()).ToString() != "3")
                {
                    txtProdName.Text = TD[0].ToString();
                    txtProdType.Text = TD[1].ToString();

                    string[] AC = SR.Getaccno(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString()).Split('_');
                    ViewState["GlCode"] = AC[0].ToString();
                    AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType.Text + "_" + ViewState["GlCode"].ToString();
                    ClearAccText();

                    txtAccNo.Focus();
                }
                else
                {
                    txtProdType.Text = "";
                    txtProdName.Text = "";
                    lblMessage.Text = "Product is not operating...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
            else
            {
                lblMessage.Text = "Enter Valid Product code...!!";
                ModalPopup.Show(this.Page);
                ClearProdText();
                txtProdType.Focus();
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
            sResult = SR.GetAccStatus(Session["BRCD"].ToString(), txtProdType.Text.ToString(), txtAccNo.Text.ToString());

            if (sResult != "3")
            {
                DT1 = BD.GetAccStage(Session["BRCD"].ToString(), txtProdType.Text, txtAccNo.Text);

                if (DT1.Rows[0]["Stage"].ToString() != "1003")
                {
                    ClearAccText();
                    lblMessage.Text = "Sorry Customer not Authorise...!!";
                    ModalPopup.Show(this.Page);
                    txtAccNo.Focus();
                }
                else
                {
                    DataTable DT = new DataTable();
                    DT = SR.GetCustName(txtProdType.Text, txtAccNo.Text, Session["BRCD"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');

                        ClearAcc();
                        txtAccName.Text = CustName[0].ToString();
                        txtCustNo.Text = CustName[1].ToString();
                        ddlAccStatus.SelectedIndex = Convert.ToInt32(DT1.Rows[0]["Acc_Status"].ToString());
                        txtAccOpen.Text = DT1.Rows[0]["OpenDate"].ToString();

                        txtBalance.Text = SR.GetOpenClose(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
                        //txtDividentBal.Text = SR.GetOpenClose(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "DivClBal").ToString();
                        //txtTotalBalance.Text = Convert.ToDouble(Convert.ToDouble(txtBalance.Text.ToString() == "" ? "0" : txtBalance.Text.ToString()) + Convert.ToDouble(txtDividentBal.Text.ToString() == "" ? "0" : txtDividentBal.Text.ToString())).ToString();
                        BindGrid();
                        CalDividentBal();

                        txtAdmin1.Focus();
                    }
                }
            }
            else
            {
                ClearAccText();
                lblMessage.Text = "Acc number " + txtAccNo.Text.ToString() + " is Closed...!!";
                ModalPopup.Show(this.Page);
                txtAccNo.Focus();
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
            string[] TD = txtAccName.Text.ToString().Split('_');
            if (TD.Length > 1)
            {
                sResult = SR.GetAccStatus(Session["BRCD"].ToString(), txtProdType.Text.ToString(), TD[1].ToString());
                if (sResult != "3")
                {
                    DT1 = BD.GetAccStage(Session["BRCD"].ToString(), txtProdType.Text, TD[1].ToString());
                    if (DT1.Rows[0]["Stage"].ToString() != "1003")
                    {
                        ClearAccText();
                        lblMessage.Text = "Sorry Customer not Authorise...!!";
                        ModalPopup.Show(this.Page);
                        txtAccNo.Focus();
                    }
                    else
                    {
                        ClearAcc();
                        txtAccName.Text = TD[0].ToString();
                        txtAccNo.Text = TD[1].ToString();
                        txtCustNo.Text = TD[2].ToString();
                        ddlAccStatus.SelectedIndex = Convert.ToInt32(DT1.Rows[0]["Acc_Status"].ToString());
                        txtAccOpen.Text = DT1.Rows[0]["OpenDate"].ToString();

                        txtBalance.Text = SR.GetOpenClose(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
                        //txtDividentBal.Text = SR.GetOpenClose(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "DivClBal").ToString();
                        //txtTotalBalance.Text = Convert.ToDouble(Convert.ToDouble(txtBalance.Text.ToString() == "" ? "0" : txtBalance.Text.ToString()) + Convert.ToDouble(txtDividentBal.Text.ToString() == "" ? "0" : txtDividentBal.Text.ToString())).ToString();
                        BindGrid();
                        CalDividentBal();

                        txtAdmin1.Focus();
                    }
                }
                else
                {
                    ClearAccText();
                    lblMessage.Text = "Acc number " + txtAccNo.Text.ToString() + " is Closed...!!";
                    ModalPopup.Show(this.Page);
                    txtAccNo.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtPayBrCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(txtPayBrCode.Text.Trim().ToString()) > 0)
            {
                txtPayBrName.Text = SR.GetBranchName(txtPayBrCode.Text.Trim().ToString());
                if (txtPayBrName.Text.Trim().ToString() == "")
                {
                    txtPayBrCode.Text = "";
                    txtPayBrName.Text = "";
                    txtPayBrCode.Focus();
                }
                else
                {
                    AutoPayGlName.ContextKey = txtPayBrCode.Text.Trim().ToString();
                    txtPayProdType.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtPayProdType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string Stage = SR.Getaccno(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text);

            if (Stage != null)
            {
                string[] AC = Stage.Split('_'); ;
                ViewState["PayGlCode"] = AC[0].ToString();
                txtPayProdName.Text = AC[1].ToString();
                AutoPayAccName.ContextKey = txtPayBrCode.Text.Trim().ToString() + "_" + txtPayProdType.Text.Trim().ToString() + "_" + ViewState["PayGlCode"].ToString();

                if (Convert.ToInt32(ViewState["PayGlCode"].ToString() == "" ? "0" : ViewState["PayGlCode"].ToString()) >= 100)
                {
                    TxtPayAccNo.Text = "";
                    TxtPayAccName.Text = "";

                    TxtPayAccNo.Text = txtPayProdType.Text.ToString();
                    TxtPayAccName.Text = txtPayProdName.Text.ToString();

                    TxtChequeNo.Focus();
                }
                else
                {
                    TxtPayAccNo.Text = "";
                    TxtPayAccName.Text = "";

                    TxtPayAccNo.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                txtPayProdType.Text = "";
                txtPayProdName.Text = "";
                TxtPayAccNo.Text = "";
                TxtPayAccName.Text = "";
                txtPayProdType.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtPayProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtPayProdName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtPayProdName.Text = custnob[0].ToString();
                txtPayProdType.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                string[] AC = SR.Getaccno(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text).Split('_');
                ViewState["PayGlCode"] = AC[0].ToString();
                AutoPayAccName.ContextKey = txtPayBrCode.Text.Trim().ToString() + "_" + txtPayProdType.Text.Trim().ToString() + "_" + ViewState["PayGlCode"].ToString();

                if (Convert.ToInt32(ViewState["PayGlCode"].ToString() == "" ? "0" : ViewState["PayGlCode"].ToString()) >= 100)
                {
                    TxtPayAccNo.Text = "";
                    TxtPayAccName.Text = "";

                    TxtPayAccNo.Text = txtPayProdType.Text.ToString();
                    TxtPayAccName.Text = txtPayProdName.Text.ToString();

                    TxtChequeNo.Focus();
                }
                else
                {
                    TxtPayAccNo.Text = "";
                    TxtPayAccName.Text = "";

                    TxtPayAccNo.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtPayAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            sResult = SR.GetAccStatus(txtPayBrCode.Text.ToString(), txtPayProdType.Text.ToString(), TxtPayAccNo.Text.ToString());
            if (sResult != "3")
            {
                DataTable DT = new DataTable();
                DT = SR.GetCustName(txtPayProdType.Text, TxtPayAccNo.Text, txtPayBrCode.Text.Trim().ToString());
                if (DT.Rows.Count > 0)
                {
                    string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                    TxtPayAccName.Text = CustName[0].ToString();

                    txtNarration.Focus();
                }
            }
            else
            {
                TxtPayAccNo.Text = "";
                TxtPayAccName.Text = "";
                lblMessage.Text = "Account is closed...!!";
                ModalPopup.Show(this.Page);
                TxtPayAccNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtPayAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] custnob = TxtPayAccName.Text.ToString().Split('_');
            if (custnob.Length > 1)
            {
                sResult = SR.GetAccStatus(txtPayBrCode.Text.ToString(), txtPayProdType.Text.ToString(), (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString()));
                if (sResult != "3")
                {
                    TxtPayAccName.Text = custnob[0].ToString();
                    TxtPayAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                    txtNarration.Focus();
                }
                else
                {
                    TxtPayAccNo.Text = "";
                    TxtPayAccName.Text = "";
                    lblMessage.Text = "Account is closed...!!";
                    ModalPopup.Show(this.Page);
                    TxtPayAccNo.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Index Changed

    protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPaymentType.SelectedValue.ToString() == "0")
            {
                Transfer.Visible = false;
                DivAmount.Visible = false;
                Transfer1.Visible = false;
            }
            else if (ddlPaymentType.SelectedValue.ToString() == "1")
            {
                Transfer.Visible = false;
                Transfer1.Visible = false;
                DivAmount.Visible = true;
                txtNarration.Text = "By Cash";

                Clear();
                CreditAmount();
                txtNarration.Focus();
            }
            else if (ddlPaymentType.SelectedValue.ToString() == "2")
            {
                Transfer.Visible = true;
                Transfer1.Visible = false;
                DivAmount.Visible = true;
                txtNarration.Text = "By Transfer";

                Clear();
                CreditAmount();
                txtPayBrCode.Focus();
            }
            else if (ddlPaymentType.SelectedValue.ToString() == "4")
            {
                Transfer.Visible = true;
                Transfer1.Visible = true;
                DivAmount.Visible = true;
                txtNarration.Text = "By Cheque";
                TxtChequeDate.Text = Session["EntryDate"].ToString();

                Clear();
                CreditAmount();
                txtPayBrCode.Focus();
            }
            else
            {
                Clear();
                Transfer.Visible = false;
                Transfer1.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CalDividentBal();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Click Events

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            dtFirst = CreateFirst(dtFirst);
            string CN = "", CD = "";
            int tmp = 0;

            //If Check No is Empty
            if (TxtChequeNo.Text.Trim().ToString() == "")
                CN = "0";
            else
                CN = TxtChequeNo.Text.Trim().ToString();

            //If Check Date is Empty
            if (TxtChequeDate.Text.Trim().ToString() == "")
                CD = "01/01/1990";
            else
                CD = TxtChequeDate.Text.Trim().ToString();

            if (ddlPaymentType.SelectedValue != "0")
            {
                if (Convert.ToDouble(txtTotalBalance.Text.ToString() == "" ? "0" : txtTotalBalance.Text.ToString()) > 0)
                {
                    RefNumber = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                    ViewState["RID"] = (Convert.ToInt32(RefNumber) + 1).ToString();
                    SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "IBTSetNo", Session["BRCD"].ToString()).ToString();

                    if (Convert.ToDouble(SetNo) > 0)
                    {
                        DT1 = SR.GetProdCode(Session["BRCD"].ToString());
                        if (DT1.Rows.Count > 0)
                        {
                            if (ddlPaymentType.SelectedValue == "1")
                            {
                                Activity1 = "4";
                                PmtMode1 = "CP";
                                Activity2 = "42";
                                PmtMode2 = "ABB-CP";
                                txtPayBrCode.Text = Session["BRCD"].ToString();
                                txtPayProdType.Text = "99";
                                TxtPayAccNo.Text = "0";
                            }
                            else if (ddlPaymentType.SelectedValue == "2")
                            {
                                Activity1 = "7";
                                PmtMode1 = "TR";
                                Activity2 = "43";
                                PmtMode2 = "ABB-TR";
                            }
                            else if (ddlPaymentType.SelectedValue == "4")
                            {
                                Activity1 = "5";
                                PmtMode1 = "TR";
                                Activity2 = "44";
                                PmtMode2 = "ABB-TR";
                            }

                            //  Debit to share member account
                            if (Convert.ToDouble(txtBalance.Text.ToString()) > 0)
                            {
                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString(), txtProdType.Text.ToString(), txtAccNo.Text.ToString(),
                                     "ABB_SHR_Closure-" + TxtPayAccNo.Text.ToString() + "", txtNarration.Text.ToString(), Convert.ToDouble(txtBalance.Text.ToString()), "2", Activity1, PmtMode1, SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", Session["BRCD"].ToString(),
                                     Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtAccName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                            }

                            //  Debit to share member divident account
                            if (Result > 0)
                            {
                                foreach (GridViewRow gvRow in grdShareDividend.Rows)
                                {
                                    if (((CheckBox)gvRow.FindControl("chk")).Checked)
                                    {
                                        dtFirst.Rows.Add(((Label)gvRow.FindControl("lblSubGlCode")).Text, ((Label)gvRow.FindControl("lblAccNo")).Text, ((Label)gvRow.FindControl("lblBalance")).Text);
                                        if (Convert.ToDouble(dtFirst.Rows[tmp]["Balance"].ToString()) > 0)
                                        {
                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "9", dtFirst.Rows[tmp]["SubGlCode"].ToString(), dtFirst.Rows[tmp]["AccNo"].ToString(),
                                                "ABB_SHR_Closure-" + TxtPayAccNo.Text.ToString() + "", txtNarration.Text.ToString(), Convert.ToDouble(dtFirst.Rows[tmp]["Balance"].ToString()), "2", Activity1, PmtMode1, SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", Session["BRCD"].ToString(),
                                                Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtAccName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                        }
                                        tmp++;
                                    }
                                }
                            }

                            //  Credit admin 1
                            if (Result > 0)
                            {
                                if (Convert.ToDouble(txtAdmin1.Text.ToString() == "" ? "0" : txtAdmin1.Text.ToString()) > 0)
                                {
                                    SubGlCode = SR.GetAdminHead("REFADMACC");
                                    GlCode = SR.GetGlCode(Session["BRCD"].ToString(), SubGlCode.ToString() == "" ? "0" : SubGlCode.ToString());
                                    if (Convert.ToDouble(GlCode) > 0)
                                    {
                                        Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode.ToString(), SubGlCode.ToString(), "0",
                                             "ABB_SHR_Closure-" + TxtPayAccNo.Text.ToString() + "", txtNarration.Text.ToString(), Convert.ToDouble(txtAdmin1.Text.ToString()), "1", Activity1, PmtMode1, SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", Session["BRCD"].ToString(),
                                             Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtAccName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                    }
                                }
                            }

                            //  Credit admin 2
                            if (Result > 0)
                            {
                                if (Convert.ToDouble(txtAdmin2.Text.ToString() == "" ? "0" : txtAdmin2.Text.ToString()) > 0)
                                {
                                    GlCode = SR.GetGlCode(Session["BRCD"].ToString(), DT1.Rows[0]["Admin2"].ToString() == "" ? "0" : DT1.Rows[0]["Admin2"].ToString());
                                    if (Convert.ToDouble(GlCode) > 0)
                                    {
                                        Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode.ToString(), DT1.Rows[0]["Admin2"].ToString(), "0",
                                         "ABB_SHR_Closure-" + TxtPayAccNo.Text.ToString() + "", txtNarration.Text.ToString(), Convert.ToDouble(txtAdmin2.Text.ToString()), "1", Activity1, PmtMode1, SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", Session["BRCD"].ToString(),
                                         Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtAccName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                    }
                                }
                            }

                            //  Credit SGST
                            if (Result > 0)
                            {
                                if (Convert.ToDouble(txtSGST.Text.ToString() == "" ? "0" : txtSGST.Text.ToString()) > 0)
                                {
                                    GlCode = SR.GetGlCode(Session["BRCD"].ToString(), DT1.Rows[0]["SGST"].ToString() == "" ? "0" : DT1.Rows[0]["SGST"].ToString());
                                    if (Convert.ToDouble(GlCode) > 0)
                                    {
                                        Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode.ToString(), DT1.Rows[0]["SGST"].ToString(), "0",
                                         "ABB_SHR_Closure-" + TxtPayAccNo.Text.ToString() + "", txtNarration.Text.ToString(), Convert.ToDouble(txtSGST.Text.ToString()), "1", Activity1, PmtMode1, SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", Session["BRCD"].ToString(),
                                         Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtAccName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                    }
                                }
                            }

                            //  Credit IGST
                            if (Result > 0)
                            {
                                if (Convert.ToDouble(txtCGST.Text.ToString() == "" ? "0" : txtCGST.Text.ToString()) > 0)
                                {
                                    GlCode = SR.GetGlCode(Session["BRCD"].ToString(), DT1.Rows[0]["IGST"].ToString() == "" ? "0" : DT1.Rows[0]["IGST"].ToString());
                                    if (Convert.ToDouble(GlCode) > 0)
                                    {
                                        Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode.ToString(), DT1.Rows[0]["IGST"].ToString(), "0",
                                         "ABB_SHR_Closure-" + TxtPayAccNo.Text.ToString() + "", txtNarration.Text.ToString(), Convert.ToDouble(txtCGST.Text.ToString()), "1", Activity1, PmtMode1, SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", Session["BRCD"].ToString(),
                                         Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtAccName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                    }
                                }
                            }

                            //  Credit to specific account
                            if (Result > 0)
                            {
                                if (Convert.ToDouble(txtCrAmount.Text.ToString() == "" ? "0" : txtCrAmount.Text.ToString()) > 0)
                                {
                                    if (ddlPaymentType.SelectedValue == "1")    //  For Cash
                                    {
                                        Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", "99", "0",
                                            "ABB_Trf_From-" + Session["BRCD"].ToString() + " - " + txtProdType.Text.ToString() + "/" + txtAccNo.Text.ToString() + "", txtNarration.Text.ToString(), 
                                            Convert.ToDouble(txtCrAmount.Text.ToString()), "1", Activity1, PmtMode1, SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", Session["BRCD"].ToString(),
                                            Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtAccName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                    }
                                    else if (ddlPaymentType.SelectedValue == "2" || ddlPaymentType.SelectedValue == "4")   //  For Transfer or Cheque
                                    {
                                        if (Session["BRCD"].ToString() == txtPayBrCode.Text.ToString())
                                        {
                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PayGlCode"].ToString(), txtPayProdType.Text.ToString(), TxtPayAccNo.Text.ToString(), 
                                                "ABB_Trf_From-" + Session["BRCD"].ToString() + " - " + txtProdType.Text.ToString() + "/" + txtAccNo.Text.ToString() + "", txtNarration.Text.ToString(), 
                                                Convert.ToDouble(txtCrAmount.Text.ToString()), "1", Activity1, PmtMode1, SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", Session["BRCD"].ToString(),
                                                Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtAccName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                        }
                                        else
                                        {
                                            //  Credit to Selected Branch
                                            DT1 = SR.GetADMSubGl(txtPayBrCode.Text.ToString());
                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT1.Rows[0]["ADMGlCode"].ToString(), DT1.Rows[0]["ADMSubGlCode"].ToString(), TxtPayAccNo.Text.ToString(),
                                            "ABB_Trf_To-" + txtPayBrCode.Text.ToString() + " - " + txtPayProdType.Text.ToString() + "/" + TxtPayAccNo.Text.ToString() + "", txtNarration.Text.ToString(), 
                                            Convert.ToDouble(txtCrAmount.Text.ToString()), "1", Activity1, PmtMode1, SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", Session["BRCD"].ToString(),
                                            Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtAccName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                            //  Debit to Selected Branch
                                            DT1 = SR.GetADMSubGl(Session["BRCD"].ToString());
                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT1.Rows[0]["ADMGlCode"].ToString(), DT1.Rows[0]["ADMSubGlCode"].ToString(), TxtPayAccNo.Text.ToString(),
                                            "ABB_Trf_From-" + Session["BRCD"].ToString() + " - " + txtProdType.Text.ToString() + "/" + txtAccNo.Text.ToString() + "", txtNarration.Text.ToString(), 
                                            Convert.ToDouble(txtCrAmount.Text.ToString()), "2", Activity2, PmtMode2, SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", txtPayBrCode.Text.ToString(),
                                            Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtAccName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PayGlCode"].ToString(), txtPayProdType.Text.ToString(), TxtPayAccNo.Text.ToString(),
                                            "ABB_Trf_From-" + Session["BRCD"].ToString() + " - " + txtProdType.Text.ToString() + "/" + txtAccNo.Text.ToString() + "", txtNarration.Text.ToString(), 
                                            Convert.ToDouble(txtCrAmount.Text.ToString()), "1", Activity2, PmtMode2, SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", txtPayBrCode.Text.ToString(),
                                            Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtAccName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                        }
                                    }
                                }
                            }

                            if (Result > 0)
                            {
                                Result = SR.CloseShareAcc(Session["BRCD"].ToString(), txtProdType.Text.ToString(), txtAccNo.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                if (Result > 0)
                                {
                                    ClearAllData();
                                    txtProdType.Focus();
                                    lblMessage.Text = "Submitted Successfully With Voucher No :" + SetNo;
                                    ModalPopup.Show(this.Page);
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    txtProdType.Focus();
                    lblMessage.Text = "Amount is not greater than zero...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
            else
            {
                ddlPaymentType.Focus();
                lblMessage.Text = "Select Payment mode first...!!";
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

    #region Public Function

    public void BindGrid()
    {
        try
        {
            Result = SR.BindDividentGrid(grdShareDividend, Session["BRCD"].ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString());
            //if (Result > 0)
            //    DivShareDividend.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void CalDividentBal()
    {
        try
        {
            double TotalBalance = 0;

            txtDividentBal.Text = "0";
            txtTotalBalance.Text = "0";

            foreach (GridViewRow Grow in grdShareDividend.Rows)
            {
                if (((CheckBox)Grow.FindControl("chk")).Checked)
                {
                    Label DivBalance = (Label)Grow.FindControl("lblBalance") as Label;

                    TotalBalance = TotalBalance + Convert.ToDouble(DivBalance.Text.ToString() == "" ? "0" : DivBalance.Text.ToString());
                }
            }

            txtDividentBal.Text = TotalBalance.ToString();
            txtTotalBalance.Text = Convert.ToDouble(Convert.ToDouble(txtBalance.Text.ToString() == "" ? "0" : txtBalance.Text.ToString()) + Convert.ToDouble(txtDividentBal.Text.ToString() == "" ? "0" : txtDividentBal.Text.ToString())).ToString();
            CreditAmount();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public DataTable CreateFirst(DataTable dt)
    {
        dt.Columns.Add("SubGlCode");
        dt.Columns.Add("AccNo");
        dt.Columns.Add("Balance");
        dt.AcceptChanges();
        return dt;
    }

    protected void Clear()
    {
        try
        {
            txtPayBrCode.Text = "";
            txtPayBrName.Text = "";
            txtPayProdType.Text = "";
            txtPayProdName.Text = "";
            TxtPayAccNo.Text = "";
            TxtPayAccName.Text = "";
            TxtChequeNo.Text = "";
            TxtChequeDate.Text = "";
            txtCrAmount.Text = "";
            txtPayBrCode.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        finally
        {
            txtPayProdType.Focus();
        }

    }

    public void ClearProdText()
    {
        try
        {
            txtProdType.Text = "";
            txtProdName.Text = "";
            txtAccNo.Text = "";
            txtAccName.Text = "";
            txtCustNo.Text = "";

            txtBalance.Text = "";
            txtDividentBal.Text = "";
            txtTotalBalance.Text = "";
            ddlAccStatus.SelectedIndex = 0;
            txtAccOpen.Text = "";

            grdShareDividend.DataSource = null;
            grdShareDividend.DataBind();

            txtAdmin1.Text = "0";
            txtAdmin2.Text = "0";
            txtTotalAdmin.Text = "0";
            txtSGST.Text = "0";
            txtCGST.Text = "0";
            txtTotalGST.Text = "0";

            txtPayBrCode.Text = "";
            txtPayBrName.Text = "";
            txtPayProdType.Text = "";
            txtPayProdName.Text = "";
            TxtPayAccNo.Text = "";
            TxtPayAccName.Text = "";
            TxtChequeNo.Text = "";
            TxtChequeDate.Text = "";
            txtCrAmount.Text = "0";

            ddlPaymentType.SelectedValue = "0";
            Transfer.Visible = false;
            DivAmount.Visible = false;
            Transfer1.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearAccText()
    {
        try
        {
            txtAccNo.Text = "";
            txtAccName.Text = "";
            txtCustNo.Text = "";

            txtBalance.Text = "";
            txtDividentBal.Text = "";
            txtTotalBalance.Text = "";
            ddlAccStatus.SelectedIndex = 0;
            txtAccOpen.Text = "";

            grdShareDividend.DataSource = null;
            grdShareDividend.DataBind();

            txtAdmin1.Text = "0";
            txtAdmin2.Text = "0";
            txtTotalAdmin.Text = "0";
            txtSGST.Text = "0";
            txtCGST.Text = "0";
            txtTotalGST.Text = "0";

            txtPayBrCode.Text = "";
            txtPayBrName.Text = "";
            txtPayProdType.Text = "";
            txtPayProdName.Text = "";
            TxtPayAccNo.Text = "";
            TxtPayAccName.Text = "";
            TxtChequeNo.Text = "";
            TxtChequeDate.Text = "";
            txtCrAmount.Text = "0";

            ddlPaymentType.SelectedValue = "0";
            Transfer.Visible = false;
            DivAmount.Visible = false;
            Transfer1.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearAcc()
    {
        try
        {
            txtBalance.Text = "";
            txtDividentBal.Text = "";
            txtTotalBalance.Text = "";
            ddlAccStatus.SelectedIndex = 0;
            txtAccOpen.Text = "";

            grdShareDividend.DataSource = null;
            grdShareDividend.DataBind();

            txtAdmin1.Text = "0";
            txtAdmin2.Text = "0";
            txtTotalAdmin.Text = "0";
            txtSGST.Text = "0";
            txtCGST.Text = "0";
            txtTotalGST.Text = "0";

            txtPayBrCode.Text = "";
            txtPayBrName.Text = "";
            txtPayProdType.Text = "";
            txtPayProdName.Text = "";
            TxtPayAccNo.Text = "";
            TxtPayAccName.Text = "";
            TxtChequeNo.Text = "";
            TxtChequeDate.Text = "";
            txtCrAmount.Text = "0";

            ddlPaymentType.SelectedValue = "0";
            Transfer.Visible = false;
            DivAmount.Visible = false;
            Transfer1.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearAllData()
    {
        try
        {
            txtProdType.Text = "";
            txtProdName.Text = "";
            txtAccNo.Text = "";
            txtAccName.Text = "";
            txtCustNo.Text = "";

            txtBalance.Text = "";
            txtAccOpen.Text = "";
            ddlAccStatus.SelectedIndex = 0;
            txtDividentBal.Text = "";
            txtTotalBalance.Text = "";

            grdShareDividend.DataSource = null;
            grdShareDividend.DataBind();

            txtAdmin1.Text = "0";
            txtAdmin2.Text = "0";
            txtTotalAdmin.Text = "0";
            txtSGST.Text = "0";
            txtCGST.Text = "0";
            txtTotalGST.Text = "0";

            txtPayBrCode.Text = "";
            txtPayBrName.Text = "";
            txtPayProdType.Text = "";
            txtPayProdName.Text = "";
            TxtPayAccNo.Text = "";
            TxtPayAccName.Text = "";
            TxtChequeNo.Text = "";
            TxtChequeDate.Text = "";
            txtCrAmount.Text = "0";

            ddlPaymentType.SelectedValue = "0";
            Transfer.Visible = false;
            DivAmount.Visible = false;
            Transfer1.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void CreditAmount()
    {
        try
        {
            double TotalAdmin = 0, TotalGST = 0, TotalBalance = 0;

            TotalBalance = Convert.ToDouble(txtTotalBalance.Text.ToString() == "" ? "0" : txtTotalBalance.Text.ToString());
            if (TotalBalance > 0)
            {
                txtAdmin1.Text = SR.GetAdminHead("REFADMAMT");
                TotalAdmin = (Convert.ToDouble(txtAdmin1.Text.ToString() == "" ? "0" : txtAdmin1.Text.ToString()) + Convert.ToDouble(txtAdmin2.Text.ToString() == "" ? "0" : txtAdmin2.Text.ToString()));
                txtTotalAdmin.Text = TotalAdmin.ToString();

                txtCGST.Text = Math.Round(Convert.ToDouble(TotalAdmin * 9) / 100, 1).ToString();
                txtSGST.Text = Math.Round(Convert.ToDouble(TotalAdmin * 9) / 100, 1).ToString();
                TotalGST = (Convert.ToDouble(txtCGST.Text.ToString() == "" ? "0" : txtCGST.Text.ToString()) + Convert.ToDouble(txtSGST.Text.ToString() == "" ? "0" : txtSGST.Text.ToString()));
                txtTotalGST.Text = TotalGST.ToString();
            }
            txtCrAmount.Text = Convert.ToDouble(TotalBalance - (TotalAdmin + TotalGST)).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        finally
        {
            txtPayProdType.Focus();
        }

    }

    #endregion

}