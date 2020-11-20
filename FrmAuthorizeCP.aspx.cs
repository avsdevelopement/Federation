using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAuthorizeCP : System.Web.UI.Page
{
    ClsAuthoriseCommon AC = new ClsAuthoriseCommon();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCommon CMN = new ClsCommon();
    DataTable AccStat = new DataTable();
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
                    txtEntryDate.Text = Session["EntryDate"].ToString();
                    txtProdType.Text = DT.Rows[0]["SUBGLCODE"].ToString();
                    txtProdName.Text = DT.Rows[0]["GLNAME"].ToString();
                    txtAccNo.Text = DT.Rows[0]["ACCNO"].ToString();
                    txtAccName.Text = DT.Rows[0]["CUSTNAME"].ToString();
                    txtNarration1.Text = DT.Rows[0]["PARTICULARS"].ToString();
                    txtNarration2.Text = DT.Rows[0]["PARTICULARS2"].ToString();
                    txtDrAmount.Text = DT.Rows[0]["AMOUNT"].ToString();
                    txtspclInst.Text = DT.Rows[0]["Spl_Instruction"].ToString();
                    txtTokenNo.Text = DT.Rows[0]["TokenNo"].ToString();

                    if (DT.Rows[0]["PARTICULARS2"].ToString() == "Withdrawal")
                    {
                        divInstrument.Visible = false;
                        txtVTypeNo.Text = "1";
                        txtVTypeName.Text = DT.Rows[0]["PARTICULARS2"].ToString();
                    }
                    else if (DT.Rows[0]["PARTICULARS2"].ToString() == "Cheque")
                    {
                        divInstrument.Visible = true;
                        txtVTypeNo.Text = "2";
                        txtVTypeName.Text = DT.Rows[0]["PARTICULARS2"].ToString();
                        txtInstNo.Text = DT.Rows[0]["INSTRUMENTNO"].ToString();
                        txtInstDate.Text = DT.Rows[0]["InstDate"].ToString();
                    }
                    else if (DT.Rows[0]["PARTICULARS2"].ToString() == "Other")
                    {
                        divInstrument.Visible = false;
                        txtVTypeNo.Text = "3";
                        txtVTypeName.Text = DT.Rows[0]["PARTICULARS2"].ToString();
                    }

                    txtAccType.Text = AC.GetAccType(Session["BRCD"].ToString(), txtProdType.Text.ToString(), txtAccNo.Text.ToString());
                    if (txtAccType.Text.ToString() == "JOINT")
                    {
                        txtJointName.Text = AC.GetJointName(Session["BRCD"].ToString(), txtProdType.Text.ToString(), txtAccNo.Text.ToString());
                        lblJoint.Visible = true;
                        txtJointName.Visible = true;
                    }
                    else
                    {
                        lblJoint.Visible = false;
                        txtJointName.Visible = false;
                    }

                    if (txtProdType.Text.ToString() != "" && txtAccNo.Text.ToString() != "")
                    {
                        txtCustNo.Text = AC.GetCustNo(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                        txtPanCardNo.Text = AC.GetPanCardNo(Session["BRCD"].ToString(), txtCustNo.Text.ToString());
                        Photo_Sign(Session["BRCD"].ToString(), txtAccNo.Text.Trim().ToString(), txtCustNo.Text.Trim().ToString());
                        txtClearBal.Text = AC.GetOpenClose(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
                        txtTotalBal.Text = AC.GetOpenClose(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "MainBal").ToString();
                    }
                    else
                    {
                        txtAccNo.Text = "0";
                        txtCustNo.Text = "0";
                        txtClearBal.Text = AC.GetOpenClose(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();
                        txtTotalBal.Text = AC.GetOpenClose(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "MainBal").ToString();
                    }

                    txtDrAmount.ForeColor = System.Drawing.Color.Red;
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
                    Result = AC.VoucherAuthorizeCRCP(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString(), Session["MID"].ToString(), ViewState["ScrollNo"].ToString());

                    if (Result > 0)
                    {
                        Balance = BD.ClBalance(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal");
                        Message = "Your a/c no " + Convert.ToInt32(txtAccNo.Text.Trim().ToString()) + " has been debited with Rs " + Convert.ToDouble(txtPassAmount.Text.Trim().ToString()) + ". Available a/c balance is Rs " + Balance + ".";
                        BD.InsertSMSRec(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Message, Session["MID"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "Payment");

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
                        Result = AC.VoucherCancelCRCP(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString(), ddlReason.SelectedValue, Session["MID"].ToString(), ViewState["ScrollNo"].ToString());

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