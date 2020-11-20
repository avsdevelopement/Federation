using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAuthorizeLoan : System.Web.UI.Page
{
    ClsAuthoriseCommon AC = new ClsAuthoriseCommon();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCommon CMN = new ClsCommon();
    DataTable AccStat = new DataTable();
    ClsVoucherAutho VA = new ClsVoucherAutho();
    DataTable DT = new DataTable();
    string sResult = "", PayMast = "";
    string Message = "";
    double Balance = 0;
    int Result;

    #region Page Load
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState["SetNo"] = Request.QueryString["VN"].ToString();
                ViewState["ScrollNo"] = Request.QueryString["SN"].ToString();
                GetVoucherDetails(ViewState["SetNo"].ToString(), ViewState["ScrollNo"].ToString());

                if (Request.QueryString["Flag"].ToString() == "AT")
                {
                    btnAuthorize.Visible = true;
                    divReason.Visible = false;
                    divPassAmt.Visible = true;
                }
                else if (Request.QueryString["Flag"].ToString() == "CN")
                {
                    BindCancelReason(ddlReason);
                    btnCancel.Visible = true;
                    divReason.Visible = true;
                    divPassAmt.Visible = false;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Public Function

    public void GetVoucherDetails(string SetNo, string Scroll)
    {
        try
        {
            string OpDate = AC.GetFinStartDate(Session["EntryDate"].ToString());
            PayMast = AC.GetPayMast(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), SetNo.ToString(), Scroll.ToString());

            if (PayMast != null)
            {
                DT = AC.GetDetails_ToFill(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), SetNo, PayMast, Scroll);
                if (DT.Rows.Count > 0)
                {
                    txtVoucherNo.Text = SetNo.ToString();
                    txtScrollNo.Text = Scroll.ToString();
                    BD.BindAccStatus(ddlAccStatus);
                    BD.BindPayment(ddlPayType, "1");
                    hdnamount.Value = VA.GetTotalLoanAmount(SetNo, Session["BRCD"].ToString(), Session["EntryDate"].ToString());//amruta 06-02-2018
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        if (DT.Rows[i]["Parti"].ToString() == "PRNCR")
                            txtPrinAmt.Text = DT.Rows[i]["Amount"].ToString();

                        if (DT.Rows[i]["Parti"].ToString() == "INTCR")
                            txtIntAmt.Text = DT.Rows[i]["Amount"].ToString();

                        if (DT.Rows[i]["Parti"].ToString() == "PENCR")
                            txtPIntAmt.Text = DT.Rows[i]["Amount"].ToString();

                        if (DT.Rows[i]["Parti"].ToString() == "INTRCR")
                            txtIntRecAmt.Text = DT.Rows[i]["Amount"].ToString();

                        if (DT.Rows[i]["Parti"].ToString() == "NOTCR")
                            txtNotChrgAmt.Text = DT.Rows[i]["Amount"].ToString();

                        if (DT.Rows[i]["Parti"].ToString() == "SERCR")
                            txtSerChrgAmt.Text = DT.Rows[i]["Amount"].ToString();

                        if (DT.Rows[i]["Parti"].ToString() == "CRTCR")
                            txtCrtChrgAmt.Text = DT.Rows[i]["Amount"].ToString();

                        if (DT.Rows[i]["Parti"].ToString() == "SURCR")
                            txtSurChrgAmt.Text = DT.Rows[i]["Amount"].ToString();

                        if (DT.Rows[i]["Parti"].ToString() == "OTHCR")
                            txtOtherChrgAmt.Text = DT.Rows[i]["Amount"].ToString();

                        if (DT.Rows[i]["Parti"].ToString() == "BNKCR")
                            txtBankChrgAmt.Text = DT.Rows[i]["Amount"].ToString();

                        if (DT.Rows[i]["Parti"].ToString() == "INSCR")
                            txtInsuranceAmt.Text = DT.Rows[i]["Amount"].ToString();

                        if (DT.Rows[i]["Parti"].ToString() == "PAYDR")
                        {
                            string[] PrAcc = DT.Rows[i]["Amount_1"].ToString().Split('/');

                            if (PrAcc.Length > 0)
                            {
                                txtProdType.Text = PrAcc[0].ToString();
                                txtProdName.Text = AC.GetProdName(Session["BRCD"].ToString(), txtProdType.Text.ToString());

                                txtAccNo.Text = PrAcc[1].ToString();
                                DataTable CN = new DataTable();
                                CN = AC.GetCustName(Session["BRCD"].ToString(), txtProdType.Text, txtAccNo.Text);
                                if (CN.Rows.Count > 0)
                                {
                                    string[] CustName = CN.Rows[0]["CustName"].ToString().Split('_');
                                    txtAccName.Text = CustName[0].ToString();
                                    txtCustNo.Text = CustName[1].ToString();
                                }

                                txtAccStatus.Text = AC.GetAccStatus(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                                ddlAccStatus.SelectedIndex = Convert.ToInt32(txtAccStatus.Text.Trim().ToString());
                                //txtBefClbal.Text = Convert.ToDouble(VA.GetClBal(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal")).ToString();
                                //txtAftClBal.Text = Convert.ToDouble(VA.GetClBal(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "MainBal")).ToString();
                                //txtPanNo.Text = PanCard(Session["BRCD"].ToString(), DT.Rows[0]["CustNo"].ToString());
                            }

                            if (DT.Rows[i]["Activity"].ToString() == "3")
                            {
                                ddlPayType.SelectedIndex = Convert.ToInt32(1);
                                txtNarration.Text = "By Cash";

                                divTransfer.Visible = false;
                                divInstrment.Visible = false;
                                txtDrAmount.Text = DT.Rows[i]["Amount"].ToString();
                                //txtPanL.Text = PanCard(Session["BRCD"].ToString(), DT.Rows[0]["CustNo"].ToString());//added by ankita 22/07/2017
                            }
                            else if (DT.Rows[i]["Activity"].ToString() == "7")
                            {
                                ddlPayType.SelectedIndex = Convert.ToInt32(2);
                                txtNarration.Text = "By TRF";

                                divTransfer.Visible = true;
                                divInstrment.Visible = false;
                                txtProdType1.Text = DT.Rows[i]["SubGlCode"].ToString();
                                txtProdName1.Text = AC.GetProdName(Session["BRCD"].ToString(), txtProdType1.Text.ToString());

                                TxtAccNo1.Text = DT.Rows[i]["AccNo"].ToString();
                                DataTable CN = new DataTable();
                                CN = AC.GetCustName(Session["BRCD"].ToString(), txtProdType1.Text, TxtAccNo1.Text);
                                if (CN.Rows.Count > 0)
                                {
                                    string[] CustName = CN.Rows[0]["CustName"].ToString().Split('_');
                                    TxtAccName1.Text = CustName[0].ToString();
                                }

                                txtDrAmount.Text = DT.Rows[i]["Amount"].ToString();
                                //txtPanL.Text = PanCard(Session["BRCD"].ToString(), DT.Rows[0]["CustNo"].ToString());//added by ankita 22/07/2017
                            }
                            else if (DT.Rows[i]["Activity"].ToString() == "5")
                            {
                                ddlPayType.SelectedIndex = Convert.ToInt32(3);
                                txtNarration.Text = "By TRF";

                                divTransfer.Visible = true;
                                divInstrment.Visible = true;
                                txtProdType1.Text = DT.Rows[i]["SubGlCode"].ToString();
                                txtProdName1.Text = DT.Rows[i]["GlName"].ToString();
                                TxtAccNo1.Text = DT.Rows[i]["SubGlCode"].ToString();
                                TxtAccName1.Text = DT.Rows[i]["GlName"].ToString();
                                TxtInstNo.Text = DT.Rows[i]["InstrumentNo"].ToString();
                                TxtInstDate.Text = DT.Rows[i]["InstrumentDate"].ToString();
                                //txtPanL.Text = PanCard(Session["BRCD"].ToString(), DT.Rows[0]["CustNo"].ToString());//added by ankita 22/07/2017
                                txtDrAmount.Text = DT.Rows[i]["Amount"].ToString();
                            }
                        }
                    }

                    ChangeColor();
                    Photo_Sign(Session["BRCD"].ToString(), txtAccNo.Text.Trim().ToString(), txtCustNo.Text.Trim().ToString());
                    AccStat = GetAccStatDetails(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), OpDate);
                    if (DT.Rows.Count > 0)
                    {
                        grdAccStat.DataSource = DT;
                        grdAccStat.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('.bs-example-modal-lg').modal('show');</script>", false);
                    }

                    if (Request.QueryString["Flag"].ToString() == "AT")
                        txtPassAmount.Focus();
                    else if (Request.QueryString["Flag"].ToString() == "CN")
                        ddlReason.Focus();
                }
                else
                {
                    lblMessage.Text = "Set Not Found...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ChangeColor()
    {
        try
        {
            txtDrAmount.ForeColor = System.Drawing.Color.Red;
            txtPrinAmt.ForeColor = System.Drawing.Color.Green;
            txtIntAmt.ForeColor = System.Drawing.Color.Green;
            txtPIntAmt.ForeColor = System.Drawing.Color.Green;
            txtIntRecAmt.ForeColor = System.Drawing.Color.Green;
            txtNotChrgAmt.ForeColor = System.Drawing.Color.Green;
            txtSerChrgAmt.ForeColor = System.Drawing.Color.Green;
            txtCrtChrgAmt.ForeColor = System.Drawing.Color.Green;
            txtSurChrgAmt.ForeColor = System.Drawing.Color.Green;
            txtOtherChrgAmt.ForeColor = System.Drawing.Color.Green;
            txtBankChrgAmt.ForeColor = System.Drawing.Color.Green;
            txtInsuranceAmt.ForeColor = System.Drawing.Color.Green;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public DataTable GetAccStatDetails(string BrCode, string PrCode, string AccNo, string FinDate)
    {
        try
        {
            DT = new DataTable();
            string[] DTF, DTT;

            DTF = FinDate.ToString().Split('/');
            DTT = Session["EntryDate"].ToString().Split('/');

            DT = AC.GetAccStatDetails(DTF[1].ToString(), DTT[1].ToString(), DTF[2].ToString(), DTT[2].ToString(), FinDate.ToString(), Session["EntryDate"].ToString(), AccNo.ToString(), PrCode.ToString(), Session["BRCD"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public void Photo_Sign(string BrCode, string AccNo, string CustNo)
    {
        try
        {
            DT = CMN.ShowIMAGE(CustNo.ToString(), BrCode.ToString(), AccNo.ToString());
            if (DT.Rows.Count > 0)
            {
                int i = 0;
                String FilePath = "";
                byte[] bytes = null;
                for (int y = 0; y < 2; y++)
                {
                    if (y == 0)
                    {
                        FilePath = DT.Rows[i]["SignIMG"].ToString();
                        if (FilePath != "")
                            bytes = (byte[])DT.Rows[i]["SignIMG"];
                    }
                    else
                    {
                        FilePath = DT.Rows[i]["PhotoImg"].ToString();
                        if (FilePath != "")
                            bytes = (byte[])DT.Rows[i]["PhotoImg"];
                    }
                    if (FilePath != "")
                    {
                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                        if (y == 0)
                        {

                            Img1.Src = "data:image/tif;base64," + base64String;
                        }
                        else if (y == 1)
                        {
                            Img2.Src = "data:image/tif;base64," + base64String;
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

    public void BindCancelReason(DropDownList ddlBrName)
    {
        try
        {
            AC.BindReason(ddlBrName);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Click Event

    protected void btnAuthorize_Click(object sender, EventArgs e)
    {
        try
        {
            sResult = AC.CheckVoucherStage(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString(), ViewState["ScrollNo"].ToString());

            if (sResult != "1003" && sResult != "1004")
            {
                if (Convert.ToDouble(txtDrAmount.Text.ToString()) == Convert.ToDouble(txtPassAmount.Text.ToString() == "" ? "0" : txtPassAmount.Text.ToString()))
                {
                    Result = AC.VoucherAuthorizeLoan(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString(), Session["MID"].ToString(), ViewState["ScrollNo"].ToString());

                    if (Result > 0)
                    {
                        Balance = BD.ClBalance(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal");
                        Message = "Your a/c no " + Convert.ToInt32(txtAccNo.Text.Trim().ToString()) + " has been credited with Rs " + Convert.ToDouble(hdnamount.Value.Trim().ToString()) + ". Available a/c balance is Rs " + Balance + ".";
                        BD.InsertSMSRec(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Message, Session["MID"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "Receipt");

                        WebMsgBox.Show("Voucher no " + ViewState["SetNo"].ToString() + " successfully authorised...!!", this.Page);
                        String x = "<script type='text/javascript'>self.close();</script>";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", x, false);
                    }
                }
                else
                {
                    txtPassAmount.Focus();
                    lblMessage.Text = "Pass amount does not match with amount to authorise...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
            else
            {
                txtPassAmount.Focus();
                lblMessage.Text = "Voucher no " + ViewState["SetNo"].ToString() + " already authorise...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlReason.SelectedValue.ToString() != "0")
            {
                sResult = AC.CheckVoucherStage(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString(), ViewState["ScrollNo"].ToString());

                if (sResult != "1004")
                {
                    if (sResult != "1003" || sResult == "1003" && Session["UGRP"].ToString() == "1")
                    {
                        Result = AC.VoucherCancelLoan(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString(), ddlReason.SelectedValue, Session["MID"].ToString(), ViewState["ScrollNo"].ToString());

                        if (Result > 0)
                        {
                            WebMsgBox.Show("Voucher no " + ViewState["SetNo"].ToString() + " successfully canceled...!!", this.Page);
                            String x = "<script type='text/javascript'>self.close();</script>";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "script", x, false);
                        }
                    }
                    else
                    {
                        txtPassAmount.Focus();
                        lblMessage.Text = "Voucher no " + ViewState["SetNo"].ToString() + " already auhtorized, Contact to administrator...!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                }
                else
                {
                    txtPassAmount.Focus();
                    lblMessage.Text = "Voucher no " + ViewState["SetNo"].ToString() + " already canceled...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
            else
            {
                ddlReason.Focus();
                lblMessage.Text = "Select Reason for cancel first...!!";
                ModalPopup.Show(this.Page);
                return;
            }
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