using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class FrmFedSub : System.Web.UI.Page
{
    FedSub fedSub = new FedSub();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("FrmLogin.aspx");
        }
        if (!IsPostBack)
        {
            TXTCNO.Focus();
            //instrumentDetails.Style.Add("display", "none");
            instrumentDetails1.Visible = false;
            instrumentDetails2.Visible = false;
            getDefaulValue();
        }
    }

    public void getDefaulValue()
    {
        DataTable dtf = new DataTable();
        dtf = fedSub.GetDefaultValues();
        if (dtf != null || dtf.Rows.Count > 0)
        {
            txtDSubGlCode.Text = dtf.Rows[0]["FEDSUB_BANKPRODCODE"].ToString();//debit product code
            txtDProdName.Text = dtf.Rows[0]["FEDSUB_BANKPRODCODENAME"].ToString();
            TXTPRC.Text = dtf.Rows[0]["FEDSUB_PRODCODE1"].ToString();
            TXTPRNAME.Text = dtf.Rows[0]["FEDSUB_PRODCODE1NAME"].ToString();
            TXTPRCODE2.Text = dtf.Rows[0]["FEDSUB_PRODCODE2"].ToString();
            TXTPRNAME2.Text = dtf.Rows[0]["FEDSUB_PRODCODE2NAME"].ToString();
        }
    }


    protected void TXTCNO_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TXTCNO.Text.Length == 0)
            {
                WebMsgBox.Show("Enter Customer No First", this.Page);
                return;
            }
            dt = fedSub.GetCustomerDetails(ddlMemberType.SelectedValue.ToString(), TXTCNO.Text);
            if (dt == null || dt.Rows.Count == 0)
            {
                WebMsgBox.Show("No Records found with Customer No " + TXTCNO.Text, this.Page);
                TXTCNO.Text = String.Empty;
                TXTCNO.Focus();
            }
            else
            {
                TXTCNAME.Text = dt.Rows[0]["CustName"].ToString();
                DataTable dtR = new DataTable();
                dtR = fedSub.GetBalance(ddlMemberType.SelectedValue.ToString(), TXTCNO.Text);
                if (dtR != null && dtR.Rows.Count > 0)
                {
                    LBLBAL.Text = Convert.ToString(dtR.Rows[0]["LBLBAL"].ToString());
                    TXTBAL3.Text = Convert.ToString(dtR.Rows[0]["BALANCE"].ToString());
                }
                BindStatement();
                ddlPaymentMode.Focus();
            }

        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
    }

    private void BindStatement()
    {

        try
        {
            dt = fedSub.GetCustomerStatement(ddlMemberType.SelectedValue.ToString(), TXTCNO.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdCustomerStatement.DataSource = dt;
                grdCustomerStatement.DataBind();
                grdCustomerStatement.Visible = true;
            }
            else
            {
                grdCustomerStatement.DataSource = null;
                grdCustomerStatement.DataBind();
                grdCustomerStatement.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }
    protected void txtDSubGlCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtDSubGlCode.Text.Length == 0)
            {

                WebMsgBox.Show("Enter Debit Product First", this.Page);
                return;
            }
            dt = fedSub.GetGlDetails(txtDSubGlCode.Text, Session["BRCD"].ToString());
            if (dt == null || dt.Rows.Count == 0)
            {
                WebMsgBox.Show("No Records found with Product Code " + txtDSubGlCode.Text, this.Page);
                txtDSubGlCode.Text = String.Empty;
                txtDProdName.Text = String.Empty;
                txtDSubGlCode.Focus();
            }
            else
            {
                txtDProdName.Text = dt.Rows[0]["GlName"].ToString();
                TXTNARR.Focus();
            }
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TXTPRC_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TXTPRC.Text.Length == 0)
            {

                WebMsgBox.Show("Enter Product First", this.Page);
                TXTPRC.Focus();
                return;
            }
            dt = fedSub.GetGlDetails(TXTPRC.Text, Session["BRCD"].ToString());
            if (dt == null || dt.Rows.Count == 0)
            {
                WebMsgBox.Show("No Records found with Product Code " + TXTPRC.Text, this.Page);
                TXTPRC.Text = String.Empty;
                TXTPRNAME.Text = String.Empty;
                TXTPRC.Focus();
            }
            else
            {
                TXTPRNAME.Text = dt.Rows[0]["GlName"].ToString();
                TXTAMT.Focus();
            }

        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TXTPRCODE2_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TXTPRCODE2.Text.Length == 0)
            {

                WebMsgBox.Show("Enter Product First", this.Page);
                TXTPRCODE2.Focus();
                return;
            }
            dt = fedSub.GetGlDetails(TXTPRCODE2.Text, Session["BRCD"].ToString());
            if (dt == null || dt.Rows.Count == 0)
            {
                WebMsgBox.Show("No Records found with Product Code " + TXTPRCODE2.Text, this.Page);
                TXTPRCODE2.Text = String.Empty;
                TXTPRNAME2.Text = String.Empty;
                TXTPRCODE2.Focus();
            }
            else
            {
                TXTPRNAME2.Text = dt.Rows[0]["GlName"].ToString();
                TXTAMT2.Focus();
            }

        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void DDL1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DDL1.SelectedValue == "Y")
            {
                dt = fedSub.GetGstDetails(Session["BRCD"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    TXTCGST.Text = dt.Rows[0]["CGST"].ToString();
                    TXTSGST.Text = dt.Rows[0]["SGST"].ToString();
                    TXTRATE.Text = dt.Rows[0]["GST"].ToString();
                    TXTAMT.Focus();
                }
                else
                {
                    TXTCGST.Text = "0";
                    TXTSGST.Text = "0";
                    TXTRATE.Text = "0";
                    TXTAMT.Focus();

                }
            }
            else
            {
                TXTCGST.Text = "0";
                TXTSGST.Text = "0";
                TXTRATE.Text = "0";
                TXTAMT.Focus();
            }
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void DDL2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DDL2.SelectedValue == "Y")
            {
                dt = fedSub.GetGstDetails(Session["BRCD"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    TXTCGST2.Text = dt.Rows[0]["CGST"].ToString();
                    TXTSGST2.Text = dt.Rows[0]["SGST"].ToString();
                    TXTRATE2.Text = dt.Rows[0]["GST"].ToString();
                    TXTAMT2.Focus();
                }
                else
                {
                    TXTCGST2.Text = "0";
                    TXTSGST2.Text = "0";
                    TXTRATE2.Text = "0";
                    TXTAMT2.Focus();

                }
            }
            else
            {
                TXTCGST2.Text = "0";
                TXTSGST2.Text = "0";
                TXTRATE2.Text = "0";
                TXTAMT2.Focus();
            }
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TXTAMT_TextChanged(object sender, EventArgs e)
    {
        try
        {

            if (TXTAMT.Text.Length == 0)
            {
                WebMsgBox.Show("Enter Amount", this.Page);
                return;
            }
            if (TXTCGST.Text.Length > 0 && TXTSGST.Text.Length > 0 && TXTRATE.Text.Length > 0 && TXTAMT.Text.Length > 0)
            {
                dt = fedSub.GetstCalculation(amount: TXTAMT.Text, cgst: TXTCGST.Text, sgst: TXTSGST.Text);

                if (dt != null && dt.Rows.Count > 0)
                {
                    TXTSGSTAMT.Text = dt.Rows[0]["SGSTAMT"].ToString();
                    TXTCGSTAMT.Text = dt.Rows[0]["CGSTAMT"].ToString();
                    TXTTGST.Text = dt.Rows[0]["TOTALGST"].ToString();
                    TXTTTL.Text = dt.Rows[0]["TOTAL"].ToString();
                    GrandTotal();

                }
            }
            else
            {
                WebMsgBox.Show("Enter Valid Details", this.Page);
                TXTAMT.Text = String.Empty;
                TXTAMT.Focus();
                return;
            }
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TXTAMT2_TextChanged(object sender, EventArgs e)
    {
        try
        {

            if (TXTAMT2.Text.Length == 0)
            {
                WebMsgBox.Show("Enter Amount", this.Page);
                return;
            }
            if (TXTCGST2.Text.Length > 0 && TXTSGST2.Text.Length > 0 && TXTRATE2.Text.Length > 0 && TXTAMT2.Text.Length > 0)
            {
                dt = fedSub.GetstCalculation(amount: TXTAMT2.Text, cgst: TXTCGST2.Text, sgst: TXTSGST2.Text);

                if (dt != null && dt.Rows.Count > 0)
                {
                    TXTTTSGST2.Text = dt.Rows[0]["SGSTAMT"].ToString();
                    TXTTTCGST2.Text = dt.Rows[0]["CGSTAMT"].ToString();
                    TTLGST2.Text = dt.Rows[0]["TOTALGST"].ToString();
                    TTLAMT2.Text = dt.Rows[0]["TOTAL"].ToString();
                    GrandTotal();

                }
            }
            else
            {
                WebMsgBox.Show("Enter Valid Details", this.Page);
                TXTAMT2.Text = String.Empty;
                TXTAMT2.Focus();
                return;
            }
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
    }

    private void GrandTotal()
    {
        try
        {
            var firstAmount = TXTAMT.Text.Length == 0 ? "0" : TXTAMT.Text;
            var secondAmount = TXTAMT2.Text.Length == 0 ? "0" : TXTAMT2.Text;

            var firstSgst = TXTSGSTAMT.Text.Length == 0 ? "0" : TXTSGSTAMT.Text;
            var secondSgst = TXTTTSGST2.Text.Length == 0 ? "0" : TXTTTSGST2.Text;


            var firstCgst = TXTCGSTAMT.Text.Length == 0 ? "0" : TXTCGSTAMT.Text;
            var secondCgst = TXTTTCGST2.Text.Length == 0 ? "0" : TXTTTCGST2.Text;

            TXTAMT3.Text = (Convert.ToDouble(firstAmount) + Convert.ToDouble(secondAmount)).ToString();
            TXTSGST3.Text = (Convert.ToDouble(firstSgst) + Convert.ToDouble(secondSgst)).ToString();
            TXTCGST3.Text = (Convert.ToDouble(firstCgst) + Convert.ToDouble(secondCgst)).ToString();

            TXTGRTTL3.Text = (Convert.ToDouble(TXTAMT3.Text) + Convert.ToDouble(TXTSGST3.Text) + Convert.ToDouble(TXTCGST3.Text)).ToString();



        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BTNSUBMIT_Click(object sender, EventArgs e)
    {
        try
        {

            if (TXTCNO.Text.Length == 0)
            {
                WebMsgBox.Show("Enter customer no first", this.Page);
                TXTCNO.Focus();
                return;
            }
            if (ddlPaymentMode.SelectedValue == "5" && Txtchno.Text.Length == 0)
            {
                WebMsgBox.Show("Enter instrument no first", this.Page);
                Txtchno.Focus();
                return;
            }

            if (ddlPaymentMode.SelectedValue == "5" && txtChequeDate.Text.Length == 0)
            {
                WebMsgBox.Show("Enter instrument Date first", this.Page);
                txtChequeDate.Focus();
                return;
            }

            if (ddlPaymentMode.SelectedValue == "5" && txtBankCode.Text.Length == 0)
            {
                WebMsgBox.Show("Enter instrument Bank Code First", this.Page);
                txtBankCode.Focus();
                return;
            }

            if (ddlPaymentMode.SelectedValue == "5" && txtBranchCode.Text.Length == 0)
            {
                WebMsgBox.Show("Enter instrument Branch Code first", this.Page);
                txtBranchCode.Focus();
                return;
            }
            if (txtDSubGlCode.Text.Length == 0)
            {
                WebMsgBox.Show("Enter debit product code first", this.Page);
                txtDSubGlCode.Focus();
                return;
            }

            DataTable gstData = new DataTable();
            gstData = SetGstDataTable();
            var instrumentNo = Txtchno.Text.Length == 0 ? "0" : Txtchno.Text;
            var instrumentDate = txtChequeDate.Text.Length == 0 ? "1900-01-01" : txtChequeDate.Text;




            int setNo = fedSub.SaveDetails(MemberType: ddlMemberType.SelectedValue.ToString(), MemberNo: TXTCNO.Text, brcd: Session["BRCD"].ToString(), entryDate: Session["EntryDate"].ToString(), FROMPERIOD: txtFromPeriod.Text, TOPERIOD: txtToPeriod.Text, mid: Session["MID"].ToString(),
                debitSubGlCode: txtDSubGlCode.Text, paymentMode: ddlPaymentMode.SelectedValue, narration: TXTNARR.Text, instrumentNo: instrumentNo, chequeDate: instrumentDate,
                bankCode: txtBankCode.Text, branchCode: txtBranchCode.Text, gstDetails: gstData);
            if (setNo > 0)
            {
                string redirectURL = "FrmRView.aspx?ID=" + setNo.ToString() + "&MEMTYPE=" + ddlMemberType.SelectedValue.ToString() + "&MEMNO=" + TXTCNO.Text.ToString() + "&rptname=RptOrdReceipt.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                ClearDetails();
                TXTCNO.Focus();

                return;
            }
            else
            {
                WebMsgBox.Show("Error occured while adding data", this.Page);
                return;
            }


        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
    }

    private DataTable SetGstDataTable()
    {
        DataTable gstData = new DataTable();

        try
        {
            fedSub.SetGSTDetails(isGst: DDL1.SelectedValue, subGlCode: TXTPRC.Text, narration: TXTPRNAME.Text, AMT: TXTAMT.Text, gstRate: TXTRATE.Text,
                sgst: TXTSGST.Text, cgst: TXTCGST.Text, CGSTAMT: TXTCGSTAMT.Text, SGSTAMT: TXTSGSTAMT.Text, GSTAMT: TXTTGST.Text, AMOUNT: TXTTTL.Text, gstData: ref gstData);
            fedSub.SetGSTDetails(isGst: DDL2.SelectedValue, subGlCode: TXTPRCODE2.Text, narration: TXTPRNAME2.Text, AMT: TXTAMT2.Text, gstRate: TXTRATE2.Text,
               sgst: TXTSGST2.Text, cgst: TXTCGST2.Text, CGSTAMT: TXTTTCGST2.Text, SGSTAMT: TXTTTSGST2.Text, GSTAMT: TTLGST2.Text, AMOUNT: TTLAMT2.Text, gstData: ref gstData);

        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return gstData;
    }

    //private DataTable SetGstDataTable()
    //{
    //    DataTable gstData = new DataTable();

    //    try
    //    {
    //        fedSub.SetGSTDetails(isGst: DDL1.SelectedValue, subGlCode: TXTPRC.Text, narration: TXTPRNAME.Text, AMT: TXTAMT.Text, gstRate: TXTRATE.Text,
    //            sgst: TXTSGST.Text, cgst: TXTCGST.Text, CGSTAMT: TXTAMT.Text, SGSTAMT: TXTCGSTAMT.Text, GSTAMT: TXTSGSTAMT.Text, AMOUNT: TXTTTL.Text, gstData: ref gstData);
    //        fedSub.SetGSTDetails(isGst: DDL2.SelectedValue, subGlCode: TXTPRCODE2.Text, narration: TXTPRNAME2.Text, AMT: TXTAMT2.Text, gstRate: TXTRATE2.Text,
    //           sgst: TXTSGST2.Text, cgst: TXTCGST2.Text, CGSTAMT: TXTTTCGST2.Text, SGSTAMT: TXTTTSGST2.Text, GSTAMT: TTLGST2.Text, AMOUNT: TTLAMT2.Text, gstData: ref gstData);

    //    }
    //    catch (Exception ex)
    //    {

    //        ExceptionLogging.SendErrorToText(ex);
    //    }
    //    return gstData;
    //}

    private void Total()
    {
        try
        {
            TXTRATE.Text = (Convert.ToDouble(TXTSGST.Text == "" ? "0" : TXTSGST.Text) + Convert.ToDouble(TXTCGST.Text == "" ? "0" : TXTCGST.Text)).ToString();
            TXTTGST.Text = (Convert.ToDouble(TXTCGSTAMT.Text == "" ? "0" : TXTCGSTAMT.Text) + Convert.ToDouble(TXTSGSTAMT.Text == "" ? "0" : TXTSGSTAMT.Text)).ToString();
            TXTTTL.Text = (Convert.ToDouble(TXTAMT.Text == "" ? "0" : TXTAMT.Text) + Convert.ToDouble(TXTTGST.Text == "" ? "0" : TXTTGST.Text)).ToString();
            TXTRATE2.Text = (Convert.ToDouble(TXTSGST2.Text == "" ? "0" : TXTSGST2.Text) + Convert.ToDouble(TXTCGST2.Text == "" ? "0" : TXTCGST2.Text)).ToString();
            TTLGST2.Text = (Convert.ToDouble(TXTTTSGST2.Text == "" ? "0" : TXTTTSGST2.Text) + Convert.ToDouble(TXTTTCGST2.Text == "" ? "0" : TXTTTCGST2.Text)).ToString();
            TTLAMT2.Text = (Convert.ToDouble(TXTAMT2.Text == "" ? "0" : TXTAMT2.Text) + Convert.ToDouble(TTLGST2.Text == "" ? "0" : TTLGST2.Text)).ToString();

        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
    }

    private void ClearDetails()
    {
        TXTCNO.Text = String.Empty;
        TXTCNAME.Text = String.Empty;
        TXTBAL3.Text = String.Empty;
        ddlPaymentMode.SelectedValue = "3";
        instrumentDetails1.Visible = false;
        instrumentDetails2.Visible = false;
        txtFromPeriod.Text = "";
        txtToPeriod.Text = "";
        txtDSubGlCode.Text = String.Empty;
        txtDProdName.Text = String.Empty;
        Txtchno.Text = String.Empty;
        txtBranchCode.Text = String.Empty;
        txtBranchName.Text = String.Empty;
        txtBankCode.Text = String.Empty;
        txtBankName.Text = String.Empty;
        txtChequeDate.Text = String.Empty;
        TXTNARR.Text = String.Empty;
        DDL1.SelectedValue = "N";
        TXTPRC.Text = String.Empty;
        TXTPRNAME.Text = String.Empty;
        TXTSGST.Text = "0";
        TXTCGST.Text = "0";
        TXTRATE.Text = "0";
        TXTAMT.Text = "0";
        TXTSGSTAMT.Text = "0";
        TXTCGSTAMT.Text = "0";
        TXTTGST.Text = "0";
        TXTTTL.Text = "0";
        DDL2.SelectedValue = "N";
        TXTPRCODE2.Text = String.Empty;
        TXTPRNAME2.Text = String.Empty;
        grdCustomerStatement.Visible = false;
        TXTSGST2.Text = "0";
        TXTCGST2.Text = "0";
        TXTRATE2.Text = "0";
        TXTAMT2.Text = "0";
        TXTTTSGST2.Text = "0";
        TXTTTCGST2.Text = "0";
        TTLGST2.Text = "0";
        TTLAMT2.Text = "0";
        TXTAMT3.Text = "0";
        TXTSGST3.Text = "0";
        TXTCGST3.Text = "0";
        TXTGRTTL3.Text = "0";
        getDefaulValue();


    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&CustNo="
                + TXTCNO.Text + "&rptname=RptFedStatement.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }

        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPaymentMode.SelectedValue == "5")
            {
                instrumentDetails1.Visible = true;
                instrumentDetails2.Visible = true;
                txtDSubGlCode.Focus();
            }
            else
            {
                instrumentDetails1.Visible = false;
                instrumentDetails2.Visible = false;
            }

        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void grdCustomerStatement_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Select")
        {
            int rowId = Convert.ToInt32(e.CommandArgument);

            if (rowId != 0)
            {
                string redirectURL = "FrmRView.aspx?ID=" + rowId.ToString() + "&MEMTYPE=" + ddlMemberType.SelectedValue.ToString() + "&MEMNO=" + TXTCNO.Text.ToString() + "&rptname=RptOrdReceipt.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }

        }
    }
    protected void txtBankCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var bankName = fedSub.GetBankName(txtBankCode.Text);

            if (String.IsNullOrEmpty(Convert.ToString(bankName)))
            {
                WebMsgBox.Show("Enter Valid Bank Code", this.Page);
                txtBankCode.Focus();
                return;
            }
            else
            {
                txtBankName.Text = bankName;
                txtBranchCode.Focus();
            }
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void txtBranchCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var branchName = fedSub.GetBranchName(txtBankCode.Text, txtBranchCode.Text);

            if (String.IsNullOrEmpty(Convert.ToString(branchName)))
            {
                WebMsgBox.Show("Enter Valid Branch Code", this.Page);
                txtBranchCode.Focus();
                return;
            }
            else
            {
                txtBranchName.Text = branchName;
                DDL1.Focus();
            }
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void grdCustomerStatement_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdCustomerStatement.PageIndex = e.NewPageIndex;
            BindStatement();
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void txtDProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] debitproductname = txtDProdName.Text.Split('_');
            if (debitproductname.Length > 1)
            {
                txtDProdName.Text = debitproductname[0].ToString();
                txtDSubGlCode.Text = debitproductname[1].ToString();
                // BD.BindBRCD(ddlBRCD, ddlbnkcd.SelectedValue);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void TotalAmount(object sender, EventArgs e)
    {
        Total();
    }
    protected void Label4_Click(object sender, EventArgs e)
    {

    }
    protected void lnkBank_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('FrmRBIBank.aspx','_blank')", true);
    }
}