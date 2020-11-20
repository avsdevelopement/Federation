using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmShareDividend : System.Web.UI.Page
{
    ClsCustomerDetails CD = new ClsCustomerDetails();
    ClsInsertTrans ITrans = new ClsInsertTrans();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsShareDividend SD = new ClsShareDividend();
    ClsMultiVoucher MV = new ClsMultiVoucher();
    ClsInsertTrans Trans = new ClsInsertTrans();
    DataTable DT = new DataTable();
    CLsShareAccountStatment AS = new CLsShareAccountStatment();
    DataTable dtFirst;
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string AccNo = "", FL = "";
    string RefNumber, CustName, Stage, sResult = "";
    double TotalAmount, PrincAmt, TotalDrAmt, TotalAmt = 0;
    double SurTotal, OtherChrg, CurSurChrg = 0;
    int Result, Count = 0;

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

                AutoMemberName.ContextKey = "1_4_4";
                txtMemberNo.Focus();
            }
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtMemberNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Stage = BD.Getstage1(txtMemberNo.Text.Trim().ToString(), "1", "4");
            if (Stage != null)
            {
                if (Stage != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise...!!";
                    ModalPopup.Show(this.Page);
                    txtMemberNo.Text = "";
                    txtMemberName.Text = "";
                    txtCustNo.Text = "";
                    txtMemberNo.Focus();
                }
                else
                {
                    DataTable DT = new DataTable();
                    DT = SD.GetCustName("1", "4", txtMemberNo.Text.Trim().ToString());
                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                        txtMemberName.Text = CustName[0].ToString();
                        txtMemberNo.Text = CustName[1].ToString();
                        txtCustNo.Text = CustName[2].ToString();
                        BindStatementGrid();
                    }
                }
            }
            else
            {
                lblMessage.Text = "Enter valid account number...!!";
                ModalPopup.Show(this.Page);
                txtMemberNo.Text = "";
                txtMemberName.Text = "";
                txtCustNo.Text = "";
                txtMemberNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtMemberName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CustName = txtMemberName.Text.ToString();
            string[] custnob = CustName.Split('_');
            if (custnob.Length > 1)
            {
                txtMemberName.Text = custnob[0].ToString();
                txtMemberNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                txtCustNo.Text = custnob[2].ToString();

                btnShow.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
            FL = "Insert";//Dhanya 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "sharedivident_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindStatementGrid()
    {
        try
        {
            int Res = 0;
            Res = AS.GetStatementGrid(GrdStatementSum, "SUMMARY", "1", Session["EntryDate"].ToString(), txtCustNo.Text, txtMemberNo.Text);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindGrid()
    {
        try
        {
            if (rbtnType.SelectedValue == "1")
            {
                Result = SD.BindSpecialGrid(grdShareDividend, "1", txtMemberNo.Text.Trim().ToString(), Session["EntryDate"].ToString());
                if (Result > 0)
                {
                    CD.GetAccountInfo(grdAccDetails, "1", txtCustNo.Text, Session["EntryDate"].ToString(), "SB");
                    CD.GetAccountInfo(GrdDirectLiab, "1", txtCustNo.Text, Session["EntryDate"].ToString(), "DL");
                    CD.GetAccountInfo(GrdInDirectLiab, "1", txtCustNo.Text, Session["EntryDate"].ToString(), "SUR");
                    DivPaymentType.Visible = true;
                }
            }
            else if (rbtnType.SelectedValue == "2")
            {
                Result = SD.BindMultipleGrid(grdShareDividend, "1", ddlDivision.SelectedValue, ddlDepartment.SelectedValue, txtProdCode.Text.Trim().ToString(), Session["EntryDate"].ToString());
                if (Result > 0)
                {
                    DivPaymentType.Visible = true;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvrow in grdShareDividend.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chk");

                if (chk != null && chk.Checked)
                {
                    Count = Count + 1;
                }
            }

            if (Convert.ToInt32(Count) > 0)
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
                    SelectBranch();
                    txtNarration.Focus();
                }
                else if (ddlPaymentType.SelectedValue.ToString() == "2")
                {
                    Transfer.Visible = true;
                    Transfer1.Visible = false;
                    DivAmount.Visible = true;
                    txtNarration.Text = "By TRF";

                    Clear();
                    CreditAmount();
                    SelectBranch();
                }

                else if (ddlPaymentType.SelectedValue.ToString() == "4")
                {
                    Transfer.Visible = true;
                    Transfer1.Visible = true;
                    DivAmount.Visible = true;
                    txtNarration.Text = "By TRF";
                    TxtChequeDate.Text = Session["EntryDate"].ToString();

                    Clear();
                    CreditAmount();
                    SelectBranch();
                }
                else
                {
                    Clear();
                    Transfer.Visible = false;
                    Transfer1.Visible = false;
                }
            }
            else
            {
                ddlPaymentType.SelectedValue = "0";
                lblMessage.Text = "Select at least one or more than one record from grid...!!";
                ModalPopup.Show(this.Page);
                return;
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
                txtPayBrName.Text = SD.GetBranchName(txtPayBrCode.Text.Trim().ToString());
                if (txtPayBrName.Text.Trim().ToString() == "")
                {
                    txtPayBrCode.Text = "";
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
            Stage = SD.GetAccNo(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text);

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

                string[] AC = SD.GetAccNo(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text).Split('_');
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
            Stage = BD.Getstage1(TxtPayAccNo.Text, txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text);
            if (Stage != null)
            {
                if (Stage != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise...!!";
                    ModalPopup.Show(this.Page);
                    TxtPayAccNo.Text = "";
                    TxtPayAccName.Text = "";
                    TxtPayAccNo.Focus();
                }
                else
                {
                    DataTable DT = new DataTable();
                    DT = SD.GetCustName(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text, TxtPayAccNo.Text);
                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                        TxtPayAccName.Text = CustName[0].ToString();

                        //  Added for loan case
                        if (ViewState["PayGlCode"] == "3")
                        {
                            DataTable LoanDT = new DataTable();
                            LoanDT = MV.GetLoanTotalAmount(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), TxtPayAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString());

                            if (LoanDT.Rows.Count > 0)
                            {
                                if (LoanDT.Rows[0]["Acc_Status"].ToString() == "3")
                                {
                                    txtPayProdType.Text = "";
                                    txtPayProdName.Text = "";
                                    TxtPayAccNo.Text = "";
                                    TxtPayAccName.Text = "";
                                    txtPayProdType.Focus();
                                    lblMessage.Text = "Account is already closed...!!";
                                    ModalPopup.Show(this.Page);
                                    return;
                                }
                                else if (LoanDT.Rows[0]["Acc_Status"].ToString() == "9")
                                {
                                    txtPayProdType.Text = "";
                                    txtPayProdName.Text = "";
                                    TxtPayAccNo.Text = "";
                                    TxtPayAccName.Text = "";
                                    txtPayProdType.Focus();
                                    lblMessage.Text = "Close account in loan Installment...!!";
                                    ModalPopup.Show(this.Page);
                                    return;
                                }
                            }
                        }
                        txtNarration.Focus();
                    }
                }
            }
            else
            {
                lblMessage.Text = "Enter valid account number...!!";
                ModalPopup.Show(this.Page);
                TxtPayAccNo.Text = "";
                TxtPayAccName.Text = "";
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
            CustName = TxtPayAccName.Text;
            string[] custnob = CustName.Split('_');
            if (custnob.Length > 1)
            {
                TxtPayAccName.Text = custnob[0].ToString();
                TxtPayAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                //  Added for loan case
                if (ViewState["PayGlCode"] == "3")
                {
                    DataTable LoanDT = new DataTable();
                    LoanDT = MV.GetLoanTotalAmount(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), TxtPayAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString());

                    if (LoanDT.Rows.Count > 0)
                    {
                        if (LoanDT.Rows[0]["Acc_Status"].ToString() == "3")
                        {
                            txtPayProdType.Text = "";
                            txtPayProdName.Text = "";
                            TxtPayAccNo.Text = "";
                            TxtPayAccName.Text = "";
                            txtPayProdType.Focus();
                            lblMessage.Text = "Account is already closed...!!";
                            ModalPopup.Show(this.Page);
                            return;
                        }
                        else if (LoanDT.Rows[0]["Acc_Status"].ToString() == "9")
                        {
                            txtPayProdType.Text = "";
                            txtPayProdName.Text = "";
                            TxtPayAccNo.Text = "";
                            TxtPayAccName.Text = "";
                            txtPayProdType.Focus();
                            lblMessage.Text = "Close account in loan Installment...!!";
                            ModalPopup.Show(this.Page);
                            return;
                        }
                    }
                }
                txtNarration.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
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

    protected void ClearAll()
    {
        txtMemberNo.Text = "";
        txtMemberName.Text = "";
        txtCustNo.Text = "";
        grdShareDividend.DataSource = null;
        grdShareDividend.DataBind();
        ddlPaymentType.SelectedValue = "0";
        txtPayBrCode.Text = "";
        txtPayBrName.Text = "";
        txtPayProdType.Text = "";
        txtPayProdName.Text = "";
        TxtPayAccNo.Text = "";
        TxtPayAccName.Text = "";
        txtCrAmount.Text = "";
        ddlDivision.SelectedValue = "0";
        ddlDepartment.SelectedValue = "0";
        txtProdCode.Text = "";
        txtProdName.Text = "";
        Transfer.Visible = false;
        Transfer1.Visible = false;
        DivAmount.Visible = false;
    }

    protected void CreditAmount()
    {
        try
        {
            dtFirst = new DataTable();
            dtFirst = CreateFirst(dtFirst);
            double Value = 0;
            int tmp = 0;

            foreach (GridViewRow gvRow in grdShareDividend.Rows)
            {
                if (((CheckBox)gvRow.FindControl("chk")).Checked)
                {
                    dtFirst.Rows.Add(((Label)gvRow.FindControl("lblSubGlCode")).Text, ((Label)gvRow.FindControl("lblAccNo")).Text, ((Label)gvRow.FindControl("lblBalance")).Text);
                    Value = Value + double.Parse(dtFirst.Rows[tmp]["Balance"].ToString());
                    tmp++;
                }
            }

            txtCrAmount.Text = Value.ToString();
            if (Convert.ToDouble(txtCrAmount.Text.Trim().ToString()) > 0)
                btnSubmit.Visible = true;
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

    protected void SelectBranch()
    {
        try
        {
            if (Session["BRCD"].ToString() == "1")
            {
                txtPayBrCode.Text = "";
                txtPayBrName.Text = "";
                txtPayBrCode.Enabled = true;
                txtPayBrCode.Focus();
            }
            else
            {
                txtPayBrCode.Enabled = false;
                txtPayBrCode.Text = Session["BRCD"].ToString();
                txtPayBrName.Text = SD.GetBranchName(txtPayBrCode.Text.Trim().ToString());
                if (txtPayBrName.Text.Trim().ToString() == "")
                {
                    txtPayBrCode.Text = "";
                    txtPayBrCode.Focus();
                }
                else
                {
                    AutoPayGlName.ContextKey = txtPayBrCode.Text.Trim().ToString();
                    txtPayProdType.Focus();
                }
                txtPayProdType.Focus();
            }
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

    private DataTable CreateFirst(DataTable dt)
    {
        dt.Columns.Add("SubGlCode");
        dt.Columns.Add("AccNo");
        dt.Columns.Add("Balance");
        dt.AcceptChanges();
        return dt;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string SetNo, SetNo1 = "", GlCode, SubGlCode = "";
            string CN = "", CD = "";

            dtFirst = new DataTable();
            dtFirst = CreateFirst(dtFirst);
            double Value = 0;
            int tmp = 0;

            foreach (GridViewRow gvrow in grdShareDividend.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chk");

                if (chk != null && chk.Checked)
                {
                    Count = Count + 1;
                }
            }

            if (Convert.ToInt32(Count) > 0)
            {
                if (Convert.ToDouble(txtCrAmount.Text.Trim().ToString()) > 0)
                {
                    if (ddlPaymentType.SelectedValue == "2")
                    {
                        if (txtPayProdType.Text.Trim().ToString() == "")
                        {
                            lblMessage.Text = "Enter product code first...!!";
                            ModalPopup.Show(this.Page);
                            return;
                        }

                        if (TxtPayAccNo.Text.Trim().ToString() == "")
                        {
                            lblMessage.Text = "Enter account number first....!!";
                            ModalPopup.Show(this.Page);
                            return;
                        }
                    }
                    else if (ddlPaymentType.SelectedValue == "4")
                    {
                        if (txtPayProdType.Text.Trim().ToString() == "")
                        {
                            lblMessage.Text = "Enter product code first...!!";
                            ModalPopup.Show(this.Page);
                            return;
                        }

                        if (TxtPayAccNo.Text.Trim().ToString() == "")
                        {
                            lblMessage.Text = "Enter account number first....!!";
                            ModalPopup.Show(this.Page);
                            return;
                        }

                        if (TxtChequeNo.Text.Trim().ToString() == "")
                        {
                            lblMessage.Text = "Enter Cheque Number first...!!";
                            ModalPopup.Show(this.Page);
                            return;
                        }

                        if (TxtChequeDate.Text.Trim().ToString() == "")
                        {
                            lblMessage.Text = "Enter Cheque Date first....!!";
                            ModalPopup.Show(this.Page);
                            return;
                        }
                    }

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

                    RefNumber = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                    ViewState["RID"] = (Convert.ToInt32(RefNumber) + 1).ToString();
                    SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "IBTSetNo", Session["BRCD"].ToString()).ToString();

                    if (ddlPaymentType.SelectedValue.ToString() == "1")
                    {
                        txtPayBrCode.Text = Session["BRCD"].ToString();
                    }

                    foreach (GridViewRow gvRow in grdShareDividend.Rows)
                    {
                        Value = 0;
                        if (((CheckBox)gvRow.FindControl("chk")).Checked)
                        {
                            dtFirst.Rows.Add(((Label)gvRow.FindControl("lblSubGlCode")).Text, ((Label)gvRow.FindControl("lblAccNo")).Text, ((Label)gvRow.FindControl("lblBalance")).Text);
                            SubGlCode = dtFirst.Rows[tmp]["SubGlCode"].ToString();

                            if (rbtnType.SelectedValue == "1")
                                AccNo = txtMemberNo.Text.ToString();
                            else if (rbtnType.SelectedValue == "2")
                                AccNo = dtFirst.Rows[tmp]["AccNo"].ToString();

                            Value = double.Parse(dtFirst.Rows[tmp]["Balance"].ToString());

                            if (Value > 0)
                            {
                                if (txtPayBrCode.Text.Trim().ToString() != "1" && rbtnType.SelectedValue == "1")
                                {
                                    Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "9", SubGlCode.ToString(),
                                            AccNo.ToString(), txtNarration.Text.ToString(), "BY TRF", Value, "2", "7", "TR", SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", "1",
                                            Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtMemberName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                    TotalAmount = TotalAmount + Value;
                                }
                                else
                                {
                                    if (ddlPaymentType.SelectedValue.ToString() == "1")
                                    {
                                        Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "9", SubGlCode.ToString(),
                                            AccNo.ToString(), txtNarration.Text.ToString(), "BY TRF", Value, "2", "4", "CP", SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", rbtnType.SelectedValue == "1" ? "1" : txtPayBrCode.Text.Trim().ToString(),
                                            Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtMemberName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                        TotalAmount = TotalAmount + Value;
                                    }
                                    else if (ddlPaymentType.SelectedValue.ToString() == "2")
                                    {
                                        Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "9", SubGlCode.ToString(),
                                            AccNo.ToString(), txtNarration.Text.ToString(), "BY TRF", Value, "2", "7", "TR", SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", rbtnType.SelectedValue == "1" ? "1" : txtPayBrCode.Text.Trim().ToString(),
                                            Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtMemberName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                        TotalAmount = TotalAmount + Value;
                                    }
                                    else if (ddlPaymentType.SelectedValue.ToString() == "4")
                                    {
                                        Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "9", SubGlCode.ToString(),
                                            AccNo.ToString(), txtNarration.Text.ToString(), "BY TRF", Value, "2", "6", "TR", SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", rbtnType.SelectedValue == "1" ? "1" : txtPayBrCode.Text.Trim().ToString(),
                                            Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtMemberName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                        TotalAmount = TotalAmount + Value;
                                    }
                                }
                            }
                            tmp++;
                        }
                    }

                    if (Result > 0)
                    {
                        if (txtPayBrCode.Text.Trim().ToString() != "1" && rbtnType.SelectedValue == "1")
                        {
                            if (Convert.ToDouble(TotalAmount) > 0)
                            {
                                //  Credit Parking Account To Branch
                                DT = SD.GetADMSubGl(txtPayBrCode.Text.Trim().ToString());
                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["ADMGlCode"].ToString(), DT.Rows[0]["ADMSubGlCode"].ToString(),
                                        "0", txtNarration.Text.ToString(), "BY TRF", TotalAmount, "1", "7", "TR", SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", "1",
                                        Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtMemberName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                            }

                            if (Result > 0)
                            {
                                if (Convert.ToDouble(TotalAmount) > 0)
                                {
                                    //  Debit Parking Account To HO
                                    DT = SD.GetADMSubGl("1");

                                    if (ddlPaymentType.SelectedValue.ToString() == "1")
                                    {
                                        Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["ADMGlCode"].ToString(), DT.Rows[0]["ADMSubGlCode"].ToString(),
                                                            "0", txtNarration.Text.ToString(), "BY TRF", TotalAmount, "2", "4", "CP", SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                            Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtMemberName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                    }
                                    else if (ddlPaymentType.SelectedValue.ToString() == "2")
                                    {
                                        Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["ADMGlCode"].ToString(), DT.Rows[0]["ADMSubGlCode"].ToString(),
                                                    "0", txtNarration.Text.ToString(), "BY TRF", TotalAmount, "2", "7", "TR", SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                    Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtMemberName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                    }
                                    else if (ddlPaymentType.SelectedValue.ToString() == "4")
                                    {
                                        Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["ADMGlCode"].ToString(), DT.Rows[0]["ADMSubGlCode"].ToString(),
                                                    "0", txtNarration.Text.ToString(), "BY TRF", TotalAmount, "2", "6", "TR", SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                    Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtMemberName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                    }
                                }
                            }
                        }

                        if (Result > 0)
                        {
                            if (ddlPaymentType.SelectedValue.ToString() == "1")
                            {
                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", "99",
                                    "0", txtNarration.Text.ToString(), "BY TRF", TotalAmount, "1", "4", "CP", SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                    Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtMemberName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                            }
                            else if (ddlPaymentType.SelectedValue.ToString() == "2")
                            {
                                if (ViewState["PayGlCode"].ToString() == "3")
                                {
                                    //Added for loan case
                                    DataTable LoanDT = new DataTable();
                                    LoanDT = MV.GetLoanTotalAmount(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), TxtPayAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString());

                                    TotalAmt = Math.Round(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Principle"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Principle"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrPInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["InterestRec"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["NoticeChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["NoticeChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["ServiceChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["ServiceChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CourtChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CourtChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["SurChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["OtherChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["BankChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["BankChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["InsChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["InsChrg"].ToString()));

                                    if (TotalAmount < TotalAmt)
                                    {
                                        TotalDrAmt = TotalAmount;

                                        if (LoanDT.Rows.Count > 0)
                                        {
                                            if (LoanDT.Rows[0]["Acc_Status"].ToString() == "3")
                                            {
                                                lblMessage.Text = "Account is already closed...!!";
                                                ModalPopup.Show(this.Page);
                                                return;
                                            }
                                            else if (LoanDT.Rows[0]["Acc_Status"].ToString() == "1" || LoanDT.Rows[0]["Acc_Status"].ToString() == "9")
                                            {
                                                if (LoanDT.Rows[0]["Acc_Status"].ToString() == "9")
                                                {
                                                    SurTotal = Math.Round(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["InterestRec"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Principle"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Principle"].ToString()));

                                                    if (Convert.ToDouble(TotalDrAmt) > Convert.ToDouble(SurTotal))
                                                    {
                                                        CurSurChrg = Math.Round(Convert.ToDouble((SurTotal * 1.75) / 100));
                                                    }
                                                    else
                                                    {
                                                        CurSurChrg = Math.Round(Convert.ToDouble((TotalDrAmt * 1.75) / 100));
                                                    }
                                                    TotalAmt = TotalAmt + CurSurChrg;
                                                }
                                                else
                                                {
                                                    CurSurChrg = 0;
                                                }

                                                OtherChrg = Math.Round(Convert.ToDouble(((Math.Abs(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Principle"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Principle"].ToString())) * Convert.ToDouble(SD.GetOtherIntRate(Session["BRCD"].ToString(), LoanDT.Rows[0]["SubGlCode"].ToString()))) * (Convert.ToInt32(LoanDT.Rows[0]["Days"].ToString()))) / 36500));

                                                if (OtherChrg == null || OtherChrg.ToString() == "")
                                                    OtherChrg = 0;
                                                else
                                                    TotalAmt = TotalAmt + OtherChrg;

                                                if (LoanDT.Rows[0]["IntCalType"].ToString() == "1" || LoanDT.Rows[0]["IntCalType"].ToString() == "2")
                                                {
                                                    Result = 1;
                                                    //  For Insurance Charge
                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(LoanDT.Rows[0]["InsChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(LoanDT.Rows[0]["InsChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["InsChrg"].ToString()))
                                                        {
                                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["InsChrgGl"].ToString(), LoanDT.Rows[0]["InsChrgSub"].ToString(),
                                                                        TxtPayAccNo.Text.Trim().ToString(), "INSCR", txtNarration.Text.ToString(), Convert.ToDouble(LoanDT.Rows[0]["InsChrg"].ToString()), "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                        Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                            if (Result > 0)
                                                            {
                                                                //  Insurance Charge Credit To 11 In AVS_LnTrx
                                                                Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["InsChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "11", "1", "7", "Insurance Charge Credit", LoanDT.Rows[0]["InsChrg"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(LoanDT.Rows[0]["InsChrg"].ToString()));
                                                        }
                                                        else if (Convert.ToDouble(LoanDT.Rows[0]["InsChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                                        {
                                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["InsChrgGl"].ToString(), LoanDT.Rows[0]["InsChrgSub"].ToString(),
                                                                        TxtPayAccNo.Text.Trim().ToString(), "INSCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                        Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                            if (Result > 0)
                                                            {
                                                                //  Insurance Charge Credit To 11 In AVS_LnTrx
                                                                Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["InsChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "11", "1", "7", "Insurance Charge Credit", TotalDrAmt.ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                            TotalDrAmt = 0;
                                                        }
                                                    }

                                                    //  For Bank Charges
                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(LoanDT.Rows[0]["BankChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["BankChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(LoanDT.Rows[0]["BankChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["BankChrg"].ToString()))
                                                        {
                                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["BankChrgGl"].ToString(), LoanDT.Rows[0]["BankChrgSub"].ToString(),
                                                                        TxtPayAccNo.Text.Trim().ToString(), "BNKCR", txtNarration.Text.ToString(), Convert.ToDouble(LoanDT.Rows[0]["BankChrg"].ToString()), "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                        Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                            if (Result > 0)
                                                            {
                                                                // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                                                Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["BankChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "10", "1", "7", "Bank Charges Credit", LoanDT.Rows[0]["BankChrg"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(LoanDT.Rows[0]["BankChrg"].ToString()));
                                                        }
                                                        else if (Convert.ToDouble(LoanDT.Rows[0]["BankChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["BankChrg"].ToString()) > 0 && TotalDrAmt > 0 && TotalDrAmt > 0)
                                                        {
                                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["BankChrgGl"].ToString(), LoanDT.Rows[0]["BankChrgSub"].ToString(),
                                                                        TxtPayAccNo.Text.Trim().ToString(), "BNKCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                        Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                            if (Result > 0)
                                                            {
                                                                // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                                                Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["BankChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "10", "1", "7", "Bank Charges Credit", TotalDrAmt.ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                            TotalDrAmt = 0;
                                                        }
                                                    }

                                                    //  For Other Charges
                                                    if (Result > 0)
                                                    {
                                                        if ((Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0 && TotalDrAmt >= (Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["OtherChrg"].ToString()) + OtherChrg))
                                                        {
                                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["OtherChrgGl"].ToString(), LoanDT.Rows[0]["OtherChrgSub"].ToString(),
                                                                        TxtPayAccNo.Text.Trim().ToString(), "OTHCR", txtNarration.Text.ToString(), Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["OtherChrg"].ToString()) + OtherChrg), "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                        Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                            if (Result > 0)
                                                            {
                                                                if (OtherChrg > 0)
                                                                {
                                                                    // Other Charges Amt Debit To 9 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["OtherChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "9", "2", "7", "Other Charges Debit", LoanDT.Rows[0]["OtherChrg"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                            }
                                                            if (Result > 0)
                                                            {
                                                                // Other Charges Amt Credit To 9 In AVS_LnTrx
                                                                Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["OtherChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "9", "1", "7", "Other Charges Credit", Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["OtherChrg"].ToString()) + OtherChrg).ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                            TotalDrAmt = Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["OtherChrg"].ToString()) + OtherChrg);
                                                        }
                                                        else if ((Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0 && TotalDrAmt > 0)
                                                        {
                                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["OtherChrgGl"].ToString(), LoanDT.Rows[0]["OtherChrgSub"].ToString(),
                                                                        TxtPayAccNo.Text.Trim().ToString(), "OTHCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                        Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                            if (Result > 0)
                                                            {
                                                                if (OtherChrg > 0)
                                                                {
                                                                    // Other Charges Amt Debit To 9 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["OtherChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "9", "2", "7", "Other Charges Debit", LoanDT.Rows[0]["OtherChrg"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                            }
                                                            if (Result > 0)
                                                            {
                                                                // Other Charges Amt Credit To 9 In AVS_LnTrx
                                                                Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["OtherChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "9", "1", "7", "Other Charges Credit", TotalDrAmt.ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                            TotalDrAmt = 0;
                                                        }
                                                    }

                                                    //  For Sur Charges
                                                    if (Result > 0)
                                                    {
                                                        if ((Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0 && TotalDrAmt >= Convert.ToDouble(LoanDT.Rows[0]["SurChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["SurChrg"].ToString()))
                                                        {
                                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["SurChrgGl"].ToString(), LoanDT.Rows[0]["SurChrgSub"].ToString(),
                                                                        TxtPayAccNo.Text.Trim().ToString(), "SURCR", txtNarration.Text.ToString(), Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["SurChrg"].ToString()) + CurSurChrg), "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                        Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                            if (Result > 0)
                                                            {
                                                                if (CurSurChrg > 0)
                                                                {
                                                                    // Sur Charges Debited To 8 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(Session["BRCD"].ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["SurChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "8", "2", "7", "Sur Charges Debited", CurSurChrg.ToString(), SetNo.ToString(), "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                                if ((Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0)
                                                                {
                                                                    // Sur Charges Credit To 8 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["SurChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "8", "1", "7", "Sur Charges Credit", Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["SurChrg"].ToString()) + CurSurChrg).ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                            }
                                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["SurChrg"].ToString()) + CurSurChrg));
                                                        }
                                                        else if ((Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0 && TotalDrAmt > 0)
                                                        {
                                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["SurChrgGl"].ToString(), LoanDT.Rows[0]["SurChrgSub"].ToString(),
                                                                        TxtPayAccNo.Text.Trim().ToString(), "SURCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                        Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                            if (Result > 0)
                                                            {
                                                                if (CurSurChrg > 0)
                                                                {
                                                                    // Sur Charges Debited To 8 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(Session["BRCD"].ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["SurChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "8", "2", "7", "Sur Charges Debited", CurSurChrg.ToString(), SetNo.ToString(), "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                                if ((Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0)
                                                                {
                                                                    // Sur Charges Credit To 8 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["SurChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "8", "1", "7", "Sur Charges Credit", TotalDrAmt.ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                            }
                                                            TotalDrAmt = 0;
                                                        }
                                                    }

                                                    // For Court Charges
                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(LoanDT.Rows[0]["CourtChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["CourtChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(LoanDT.Rows[0]["CourtChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["CourtChrg"].ToString()))
                                                        {
                                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["CourtChrgGl"].ToString(), LoanDT.Rows[0]["CourtChrgSub"].ToString(),
                                                                        TxtPayAccNo.Text.Trim().ToString(), "CRTCR", txtNarration.Text.ToString(), Convert.ToDouble(LoanDT.Rows[0]["CourtChrg"].ToString()), "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                        Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                            if (Result > 0)
                                                            {
                                                                // Court Charges Amt Credit To 7 In AVS_LnTrx
                                                                Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["CourtChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "7", "1", "7", "Court Charges Credit", LoanDT.Rows[0]["CourtChrg"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(LoanDT.Rows[0]["CourtChrg"].ToString()));
                                                        }
                                                        else if (Convert.ToDouble(LoanDT.Rows[0]["CourtChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["CourtChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                                        {
                                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["CourtChrgGl"].ToString(), LoanDT.Rows[0]["CourtChrgSub"].ToString(),
                                                                        TxtPayAccNo.Text.Trim().ToString(), "CRTCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                        Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                            if (Result > 0)
                                                            {
                                                                // Court Charges Amt Credit To 7 In AVS_LnTrx
                                                                Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["CourtChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "7", "1", "7", "Court Charges Credit", TotalDrAmt.ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                            TotalDrAmt = 0;
                                                        }
                                                    }

                                                    //  For Service Charges
                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(LoanDT.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["ServiceChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(LoanDT.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["ServiceChrg"].ToString()))
                                                        {
                                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["ServiceChrgGl"].ToString(), LoanDT.Rows[0]["ServiceChrgSub"].ToString(),
                                                                        TxtPayAccNo.Text.Trim().ToString(), "SERCR", txtNarration.Text.ToString(), Convert.ToDouble(LoanDT.Rows[0]["ServiceChrg"].ToString()), "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                        Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                            if (Result > 0)
                                                            {
                                                                // Service Charges Amt Credit To 6 In AVS_LnTrx
                                                                Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["ServiceChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "6", "1", "7", "Service Charges Credit", LoanDT.Rows[0]["ServiceChrg"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(LoanDT.Rows[0]["ServiceChrg"].ToString()));
                                                        }
                                                        else if (Convert.ToDouble(LoanDT.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["ServiceChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                                        {
                                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["ServiceChrgGl"].ToString(), LoanDT.Rows[0]["ServiceChrgSub"].ToString(),
                                                                        TxtPayAccNo.Text.Trim().ToString(), "SERCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                        Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                            if (Result > 0)
                                                            {
                                                                // Service Charges Amt Credit To 6 In AVS_LnTrx
                                                                Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["ServiceChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "6", "1", "7", "Service Charges Credit", TotalDrAmt.ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                            TotalDrAmt = 0;
                                                        }
                                                    }

                                                    //  For Notice Charges
                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(LoanDT.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["NoticeChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(LoanDT.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["NoticeChrg"].ToString()))
                                                        {
                                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["NoticeChrgGl"].ToString(), LoanDT.Rows[0]["NoticeChrgSub"].ToString(),
                                                                        TxtPayAccNo.Text.Trim().ToString(), "NOTCR", txtNarration.Text.ToString(), Convert.ToDouble(LoanDT.Rows[0]["NoticeChrg"].ToString()), "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                        Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                            if (Result > 0)
                                                            {
                                                                // Notice Charges Credit To 5 In AVS_LnTrx
                                                                Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["NoticeChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "5", "1", "7", "Notice Charges Credit", LoanDT.Rows[0]["NoticeChrg"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(LoanDT.Rows[0]["NoticeChrg"].ToString()));
                                                        }
                                                        else if (Convert.ToDouble(LoanDT.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["NoticeChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                                        {
                                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["NoticeChrgGl"].ToString(), LoanDT.Rows[0]["NoticeChrgSub"].ToString(),
                                                                        TxtPayAccNo.Text.Trim().ToString(), "NOTCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                        Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                            if (Result > 0)
                                                            {
                                                                // Notice Charges Credit To 5 In AVS_LnTrx
                                                                Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["NoticeChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "5", "1", "7", "Notice Charges Credit", TotalDrAmt.ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                            TotalDrAmt = 0;
                                                        }
                                                    }

                                                    //  For Interest Receivable
                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()))
                                                        {
                                                            if (LoanDT.Rows[0]["IntCalType"].ToString() == "1")
                                                            {
                                                                // Interest Received credit to GL 11
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["InterestRecGl"].ToString(), LoanDT.Rows[0]["InterestRecSub"].ToString(),
                                                                        TxtPayAccNo.Text.Trim().ToString(), "INTRCR", txtNarration.Text.ToString(), Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()), "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                        Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["InterestRecSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "4", "1", "7", "Interest Received Credit", LoanDT.Rows[0]["InterestRec"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                            }
                                                            else if (LoanDT.Rows[0]["IntCalType"].ToString() == "2")
                                                            {
                                                                // Interest Received credit to GL 3
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PayGlCode"].ToString(), txtPayProdType.Text.Trim().ToString(),
                                                                        TxtPayAccNo.Text.Trim().ToString(), "INTRCR", txtNarration.Text.ToString(), Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()), "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                        Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                                            }
                                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()));
                                                        }
                                                        else if (Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt > 0)
                                                        {
                                                            if (LoanDT.Rows[0]["IntCalType"].ToString() == "1")
                                                            {
                                                                // Interest Received credit to GL 11
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["InterestRecGl"].ToString(), LoanDT.Rows[0]["InterestRecSub"].ToString(),
                                                                        TxtPayAccNo.Text.Trim().ToString(), "INTRCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                        Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["InterestRecSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "4", "1", "7", "Interest Received Credit", TotalDrAmt.ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                            }
                                                            else if (LoanDT.Rows[0]["IntCalType"].ToString() == "2")
                                                            {
                                                                // Interest Received credit to GL 3
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PayGlCode"].ToString().ToString(), txtPayProdType.Text.Trim().ToString(),
                                                                        TxtPayAccNo.Text.Trim().ToString(), "INTRCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                        Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                                            }
                                                            TotalDrAmt = 0;
                                                        }
                                                    }

                                                    //  For Penal Charge
                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString())))
                                                        {
                                                            //Penal Charge Credit To GL 12
                                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["PInterestGl"].ToString(), LoanDT.Rows[0]["PInterestSub"].ToString(),
                                                                    TxtPayAccNo.Text.Trim().ToString(), "PENCR", txtNarration.Text.ToString(), Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString())), "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                    Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                            if (Result > 0)
                                                            {
                                                                //Penal Interest Debit To 3 In AVS_LnTrx
                                                                if (Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : LoanDT.Rows[0]["CurrPInterest"].ToString()) > 0)
                                                                {
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["PInterestSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "3", "2", "7", "Penal Interest Debit", LoanDT.Rows[0]["CurrPInterest"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                            }

                                                            if (Result > 0)
                                                            {
                                                                //Penal Interest Credit To 3 In AVS_LnTrx
                                                                Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["PInterestSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "3", "1", "7", "Penal Interest Credit", Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString())).ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }

                                                            if (Result > 0)
                                                            {
                                                                //  (Penal Charge Contra) - Penal chrg Applied Debit To GL 12
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["PInterestGl"].ToString(), LoanDT.Rows[0]["PInterestSub"].ToString(),
                                                                        TxtPayAccNo.Text.Trim().ToString(), "PENDR", txtNarration.Text.ToString(), Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString())), "2", "12", "TR_INT", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                        Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    //Penal chrg Applied Credit to GL 100
                                                                    Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", LoanDT.Rows[0]["PlAccNo2"].ToString(),
                                                                            TxtPayAccNo.Text.Trim().ToString(), "PENCR", txtNarration.Text.ToString(), Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString())), "1", "12", "TR_INT", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                            Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                                                }
                                                            }

                                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString())));
                                                        }
                                                        else if (Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                                        {
                                                            //Penal Charge Credit To GL 12
                                                            Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["PInterestGl"].ToString(), LoanDT.Rows[0]["PInterestSub"].ToString(),
                                                                                TxtPayAccNo.Text.Trim().ToString(), "PENCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                                Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                            if (Result > 0)
                                                            {
                                                                //Penal Interest Debit To 3 In AVS_LnTrx
                                                                if (Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : LoanDT.Rows[0]["CurrPInterest"].ToString()) > 0)
                                                                {
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["PInterestSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "3", "2", "7", "Penal Interest Debit", LoanDT.Rows[0]["CurrPInterest"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                            }

                                                            if (Result > 0)
                                                            {
                                                                //Penal Interest Credit To 3 In AVS_LnTrx
                                                                Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["PInterestSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "3", "1", "7", "Penal Interest Credit", TotalDrAmt.ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }

                                                            if (Result > 0)
                                                            {
                                                                //  (Penal Charge Contra) - Penal chrg Applied Debit To GL 12
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["PInterestGl"].ToString(), LoanDT.Rows[0]["PInterestSub"].ToString(),
                                                                            TxtPayAccNo.Text.Trim().ToString(), "PENCR", txtNarration.Text.ToString(), TotalDrAmt, "2", "12", "TR_INT", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                            Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    //Penal chrg Applied Credit to GL 100
                                                                    Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", LoanDT.Rows[0]["PlAccNo2"].ToString(),
                                                                            TxtPayAccNo.Text.Trim().ToString(), "PENCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "12", "TR_INT", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                            Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                                                }
                                                            }

                                                            TotalDrAmt = 0;
                                                        }
                                                    }

                                                    //  For Interest
                                                    if (Result > 0)
                                                    {
                                                        if (LoanDT.Rows[0]["IntCalType"].ToString() == "1")
                                                        {
                                                            if (Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())))
                                                            {
                                                                //interest Credit to GL 11
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["InterestGl"].ToString(), LoanDT.Rows[0]["InterestSub"].ToString(),
                                                                            TxtPayAccNo.Text.Trim().ToString(), "INTCR", txtNarration.Text.ToString(), Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())), "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                            Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    //Current Interest Debit To 2 In AVS_LnTrx
                                                                    if (Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString() == "" ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString()) > 0)
                                                                    {
                                                                        Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "2", "2", "7", "Interest Debit", LoanDT.Rows[0]["CurrInterest"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                    }

                                                                    //  Update Last interest date
                                                                    if (Result > 0)
                                                                    {
                                                                        Result = SD.UpdateLastIntDate(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), TxtPayAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                                    }
                                                                }

                                                                if (Result > 0)
                                                                {
                                                                    //Current Interest Credit To 2 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "2", "1", "7", "Interest Credit", Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())).ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }

                                                                if (Result > 0)
                                                                {
                                                                    //  (interest Applied Contra) - interest Applied Debit To GL 11
                                                                    Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["InterestGl"].ToString(), LoanDT.Rows[0]["InterestSub"].ToString(),
                                                                            TxtPayAccNo.Text.Trim().ToString(), "INTDR", txtNarration.Text.ToString(), Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())), "2", "11", "TR_INT", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                            Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                    if (Result > 0)
                                                                    {
                                                                        //interest Applied Credit to GL 100
                                                                        Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", LoanDT.Rows[0]["PlAccNo1"].ToString(),
                                                                                TxtPayAccNo.Text.Trim().ToString(), "INTCR", txtNarration.Text.ToString(), Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())), "1", "11", "TR_INT", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                                Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                                                    }
                                                                }

                                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())));
                                                            }
                                                            else if (Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                                            {
                                                                //interest Credit to GL 11
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["InterestGl"].ToString(), LoanDT.Rows[0]["InterestSub"].ToString(),
                                                                            TxtPayAccNo.Text.Trim().ToString(), "INTCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                            Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    //Current Interest Debit To 2 In AVS_LnTrx
                                                                    if (Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString() == "" ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString()) > 0)
                                                                    {
                                                                        Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "2", "2", "7", "Interest Debit", LoanDT.Rows[0]["CurrInterest"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                    }
                                                                    
                                                                    //  Update Last interest date
                                                                    if (Result > 0)
                                                                    {
                                                                        Result = SD.UpdateLastIntDate(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), TxtPayAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                                    }
                                                                }

                                                                if (Result > 0)
                                                                {
                                                                    //Current Interest Credit To 2 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "2", "1", "7", "Interest Credit", TotalDrAmt.ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }

                                                                if (Result > 0)
                                                                {
                                                                    //  (interest Applied Contra) - interest Applied Debit To GL 11
                                                                    Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["InterestGl"].ToString(), LoanDT.Rows[0]["InterestSub"].ToString(),
                                                                            TxtPayAccNo.Text.Trim().ToString(), "INTDR", txtNarration.Text.ToString(), TotalDrAmt, "2", "11", "TR_INT", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                            Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                    if (Result > 0)
                                                                    {
                                                                        //interest Applied Credit to GL 100
                                                                        Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", LoanDT.Rows[0]["PlAccNo1"].ToString(),
                                                                                TxtPayAccNo.Text.Trim().ToString(), "INTCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "11", "TR_INT", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                                Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                                                    }
                                                                }

                                                                TotalDrAmt = 0;
                                                            }
                                                        }
                                                        else if (LoanDT.Rows[0]["IntCalType"].ToString() == "2")
                                                        {
                                                            if (Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())))
                                                            {
                                                                //interest Received Credit to GL 3
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PayGlCode"].ToString(), txtPayProdType.Text.Trim().ToString(),
                                                                                TxtPayAccNo.Text.Trim().ToString(), "INTCR", txtNarration.Text.ToString(), Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())), "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                                Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                //  Added As Per ambika mam Instruction 22-06-2017
                                                                if (Result > 0)
                                                                {
                                                                    //interest Applied Debit To GL 3
                                                                    Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PayGlCode"].ToString(), txtPayProdType.Text.Trim().ToString(),
                                                                                TxtPayAccNo.Text.Trim().ToString(), "INTDR", txtNarration.Text.ToString(), Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())), "2", "11", "TR_INT", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                                Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                    if (Result > 0)
                                                                    {
                                                                        //interest Applied Credit to GL 100
                                                                        Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", LoanDT.Rows[0]["PlAccNo1"].ToString(),
                                                                                TxtPayAccNo.Text.Trim().ToString(), "INTCR", txtNarration.Text.ToString(), Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())), "1", "11", "TR_INT", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                                Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                                                    }
                                                                }

                                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())));
                                                            }
                                                            else if (Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                                            {
                                                                //interest Received Credit to GL 3
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PayGlCode"].ToString(), txtPayProdType.Text.Trim().ToString(),
                                                                                    TxtPayAccNo.Text.Trim().ToString(), "INTCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                                    Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                //  Added As Per ambika mam Instruction 22-06-2017
                                                                if (Result > 0)
                                                                {
                                                                    //interest Applied Debit To GL 3
                                                                    Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PayGlCode"].ToString(), txtPayProdType.Text.Trim().ToString(),
                                                                                TxtPayAccNo.Text.Trim().ToString(), "INTCR", txtNarration.Text.ToString(), TotalDrAmt, "2", "11", "TR_INT", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                                Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                    if (Result > 0)
                                                                    {
                                                                        //interest Applied Credit to GL 100
                                                                        Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", LoanDT.Rows[0]["PlAccNo1"].ToString(),
                                                                                TxtPayAccNo.Text.Trim().ToString(), "INTCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "11", "TR_INT", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                                Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                                                    }
                                                                }

                                                                TotalDrAmt = 0;
                                                            }
                                                        }
                                                    }

                                                    //Principle O/S Credit To Specific GL (e.g 3)
                                                    if (Convert.ToDouble(LoanDT.Rows[0]["Principle"].ToString() == "" ? "0" : LoanDT.Rows[0]["Principle"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(LoanDT.Rows[0]["Principle"].ToString() == "" ? "0" : LoanDT.Rows[0]["Principle"].ToString()))
                                                    {
                                                        Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PayGlCode"].ToString(), txtPayProdType.Text.Trim().ToString(),
                                                                   TxtPayAccNo.Text.Trim().ToString(), "PRNCR", txtNarration.Text.ToString(), Convert.ToDouble(LoanDT.Rows[0]["Principle"].ToString()), "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                   Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                        if (Result > 0)
                                                        {
                                                            //Current Principle Debit To 1 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), TxtPayAccNo.Text.Trim().ToString(), "1", "2", "7", "Principle Debit", LoanDT.Rows[0]["Principle"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }

                                                        if (Result > 0)
                                                        {
                                                            //Current Principle Credit To 1 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), TxtPayAccNo.Text.Trim().ToString(), "1", "1", "7", "Principle Credit", LoanDT.Rows[0]["Principle"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }

                                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(LoanDT.Rows[0]["Principle"].ToString()));
                                                    }
                                                    else if (Convert.ToDouble(LoanDT.Rows[0]["Principle"].ToString() == "" ? "0" : LoanDT.Rows[0]["Principle"].ToString()) > 0 && TotalDrAmt > 0)
                                                    {
                                                        Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PayGlCode"].ToString(), txtPayProdType.Text.Trim().ToString(),
                                                                   TxtPayAccNo.Text.Trim().ToString(), "PRNCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                   Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                        if (Result > 0)
                                                        {
                                                            //Current Principle Debit To 1 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), TxtPayAccNo.Text.Trim().ToString(), "1", "2", "7", "Principle Debit", LoanDT.Rows[0]["Principle"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }

                                                        if (Result > 0)
                                                        {
                                                            //Current Principle Credit To 1 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), TxtPayAccNo.Text.Trim().ToString(), "1", "1", "7", "Principle Credit", TotalDrAmt.ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }

                                                        TotalDrAmt = 0;
                                                    }
                                                }
                                                else if (LoanDT.Rows[0]["IntCalType"].ToString() == "3")
                                                {
                                                    //Principle O/S Credit To Specific GL (e.g 3)
                                                    Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PayGlCode"].ToString(), txtPayProdType.Text.Trim().ToString(),
                                                                   TxtPayAccNo.Text.Trim().ToString(), "PRNCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, CN, CD, "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                                                   Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(LoanDT.Rows[0]["Principle"].ToString() == "" ? "0" : LoanDT.Rows[0]["Principle"].ToString()) > 0)
                                                        {
                                                            //Current Principle Debit To 1 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), TxtPayAccNo.Text.Trim().ToString(), "1", "2", "7", "Principle Debit", LoanDT.Rows[0]["Principle"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());

                                                            //Current Principle Credit To 1 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), TxtPayAccNo.Text.Trim().ToString(), "1", "1", "7", "Principle Credit", TotalDrAmt.ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }

                                                        //TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(LoanDT.Rows[0]["Principle"].ToString()));
                                                        PrincAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["InsChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["InsChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["BankChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["BankChrg"].ToString()) +
                                                            Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["OtherChrg"].ToString()) + OtherChrg + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["SurChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CourtChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CourtChrg"].ToString()) +
                                                            Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["ServiceChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["ServiceChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["NoticeChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["NoticeChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["InterestRec"].ToString()) +
                                                            Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrPInterest"].ToString())) +
                                                            Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString())))) < 0 ? 0 :
                                                            Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["InsChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["InsChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["BankChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["BankChrg"].ToString()) +
                                                            Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["OtherChrg"].ToString()) + OtherChrg + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["SurChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CourtChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CourtChrg"].ToString()) +
                                                            Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["ServiceChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["ServiceChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["NoticeChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["NoticeChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["InterestRec"].ToString()) +
                                                            Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrPInterest"].ToString())) +
                                                            Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString()))));
                                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - PrincAmt);
                                                    }

                                                    SetNo1 = BD.GetSetNo(Session["EntryDate"].ToString(), "IBTSetNo", Session["BRCD"].ToString()).ToString();

                                                    //Principle O/S Debit To Specific GL (e.g 3) And Credit to interest GL (e.g 11)
                                                    if (Result > 0)
                                                    {
                                                        Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PayGlCode"].ToString(), txtPayProdType.Text.Trim().ToString(),
                                                                   TxtPayAccNo.Text.Trim().ToString(), "PAYDR", txtNarration.Text.ToString(), TotalDrAmt, "2", "7", "TR", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                   Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                        //  For Insurance Charge
                                                        if (Result > 0)
                                                        {
                                                            if (Convert.ToDouble(LoanDT.Rows[0]["InsChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(LoanDT.Rows[0]["InsChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["InsChrg"].ToString()))
                                                            {
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["InsChrgGl"].ToString(), LoanDT.Rows[0]["InsChrgSub"].ToString(),
                                                                       TxtPayAccNo.Text.Trim().ToString(), "INSCR", txtNarration.Text.ToString(), Convert.ToDouble(LoanDT.Rows[0]["InsChrg"].ToString()), "1", "7", "TR", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                       Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    //  Insurance Charge Credit To 11 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["InsChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "11", "1", "7", "Insurance Charge Credit", LoanDT.Rows[0]["InsChrg"].ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(LoanDT.Rows[0]["InsChrg"].ToString()));
                                                            }
                                                            else if (Convert.ToDouble(LoanDT.Rows[0]["InsChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                                            {
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["InsChrgGl"].ToString(), LoanDT.Rows[0]["InsChrgSub"].ToString(),
                                                                       TxtPayAccNo.Text.Trim().ToString(), "INSCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                       Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    //  Insurance Charge Credit To 11 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["InsChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "11", "1", "7", "Insurance Charge Credit", TotalDrAmt.ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                                TotalDrAmt = 0;
                                                            }
                                                        }

                                                        //  For Bank Charges
                                                        if (Result > 0)
                                                        {
                                                            if (Convert.ToDouble(LoanDT.Rows[0]["BankChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["BankChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(LoanDT.Rows[0]["BankChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["BankChrg"].ToString()))
                                                            {
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["BankChrgGl"].ToString(), LoanDT.Rows[0]["BankChrgSub"].ToString(),
                                                                       TxtPayAccNo.Text.Trim().ToString(), "BNKCR", txtNarration.Text.ToString(), Convert.ToDouble(LoanDT.Rows[0]["BankChrg"].ToString()), "1", "7", "TR", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                       Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["BankChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "10", "1", "7", "Bank Charges Credit", LoanDT.Rows[0]["BankChrg"].ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(LoanDT.Rows[0]["BankChrg"].ToString()));
                                                            }
                                                            else if (Convert.ToDouble(LoanDT.Rows[0]["BankChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["BankChrg"].ToString()) > 0 && TotalDrAmt > 0 && TotalDrAmt > 0)
                                                            {
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["BankChrgGl"].ToString(), LoanDT.Rows[0]["BankChrgSub"].ToString(),
                                                                       TxtPayAccNo.Text.Trim().ToString(), "BNKCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                       Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["BankChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "10", "1", "7", "Bank Charges Credit", TotalDrAmt.ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                                TotalDrAmt = 0;
                                                            }
                                                        }

                                                        //  For Other Charges
                                                        if (Result > 0)
                                                        {
                                                            if ((Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0 && TotalDrAmt >= (Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["OtherChrg"].ToString()) + OtherChrg))
                                                            {
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["OtherChrgGl"].ToString(), LoanDT.Rows[0]["OtherChrgSub"].ToString(),
                                                                            TxtPayAccNo.Text.Trim().ToString(), "OTHCR", txtNarration.Text.ToString(), Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["OtherChrg"].ToString()) + OtherChrg), "1", "7", "TR", SetNo, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                            Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    if (OtherChrg > 0)
                                                                    {
                                                                        // Other Charges Amt Debit To 9 In AVS_LnTrx
                                                                        Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["OtherChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "9", "2", "7", "Other Charges Debit", LoanDT.Rows[0]["OtherChrg"].ToString(), SetNo, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                    }
                                                                }
                                                                if (Result > 0)
                                                                {
                                                                    if ((Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0)
                                                                    {
                                                                        // Other Charges Amt Credit To 9 In AVS_LnTrx
                                                                        Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["OtherChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "9", "1", "7", "Other Charges Credit", Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["OtherChrg"].ToString()) + OtherChrg).ToString(), SetNo, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                    }
                                                                }
                                                                TotalDrAmt = Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["OtherChrg"].ToString()) + OtherChrg);
                                                            }
                                                            else if ((Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0 && TotalDrAmt > 0)
                                                            {
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["OtherChrgGl"].ToString(), LoanDT.Rows[0]["OtherChrgSub"].ToString(),
                                                                            TxtPayAccNo.Text.Trim().ToString(), "OTHCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                            Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    if (OtherChrg > 0)
                                                                    {
                                                                        // Other Charges Amt Debit To 9 In AVS_LnTrx
                                                                        Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["OtherChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "9", "2", "7", "Other Charges Debit", LoanDT.Rows[0]["OtherChrg"].ToString(), SetNo, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                    }
                                                                }
                                                                if (Result > 0)
                                                                {
                                                                    if ((Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0)
                                                                    {
                                                                        // Other Charges Amt Credit To 9 In AVS_LnTrx
                                                                        Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["OtherChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "9", "1", "7", "Other Charges Credit", TotalDrAmt.ToString(), SetNo, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                    }
                                                                }
                                                                TotalDrAmt = 0;
                                                            }
                                                        }

                                                        //  For Sur Charges
                                                        if (Result > 0)
                                                        {
                                                            if ((Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0 && TotalDrAmt >= Convert.ToDouble(LoanDT.Rows[0]["SurChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["SurChrg"].ToString()))
                                                            {
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["SurChrgGl"].ToString(), LoanDT.Rows[0]["SurChrgSub"].ToString(),
                                                                            TxtPayAccNo.Text.Trim().ToString(), "SURCR", txtNarration.Text.ToString(), Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["SurChrg"].ToString()) + CurSurChrg), "1", "7", "TR", SetNo, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                            Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    if (CurSurChrg > 0)
                                                                    {
                                                                        // Sur Charges Debited To 8 In AVS_LnTrx
                                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["SurChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "8", "2", "7", "Sur Charges Debited", CurSurChrg.ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                    }
                                                                    if ((Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0)
                                                                    {
                                                                        // Sur Charges Credit To 8 In AVS_LnTrx
                                                                        Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["SurChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "8", "1", "7", "Sur Charges Credit", Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["SurChrg"].ToString()) + CurSurChrg).ToString(), SetNo, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                    }
                                                                }
                                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["SurChrg"].ToString()) + CurSurChrg));
                                                            }
                                                            else if ((Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0 && TotalDrAmt > 0)
                                                            {
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["SurChrgGl"].ToString(), LoanDT.Rows[0]["SurChrgSub"].ToString(),
                                                                            TxtPayAccNo.Text.Trim().ToString(), "SURCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                            Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    if (CurSurChrg > 0)
                                                                    {
                                                                        // Sur Charges Debited To 8 In AVS_LnTrx
                                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["SurChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "8", "2", "7", "Sur Charges Debited", CurSurChrg.ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                    }
                                                                    if ((Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : LoanDT.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0)
                                                                    {
                                                                        // Sur Charges Credit To 8 In AVS_LnTrx
                                                                        Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["SurChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "8", "1", "7", "Sur Charges Credit", TotalDrAmt.ToString(), SetNo, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                    }
                                                                }
                                                                TotalDrAmt = 0;
                                                            }
                                                        }

                                                        // For Court Charges
                                                        if (Result > 0)
                                                        {
                                                            if (Convert.ToDouble(LoanDT.Rows[0]["CourtChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["CourtChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(LoanDT.Rows[0]["CourtChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["CourtChrg"].ToString()))
                                                            {
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["CourtChrgGl"].ToString(), LoanDT.Rows[0]["CourtChrgSub"].ToString(),
                                                                       TxtPayAccNo.Text.Trim().ToString(), "CRTCR", txtNarration.Text.ToString(), Convert.ToDouble(LoanDT.Rows[0]["CourtChrg"].ToString()), "1", "7", "TR", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                       Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    // Court Charges Amt Credit To 7 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["CourtChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "7", "1", "7", "Court Charges Credit", LoanDT.Rows[0]["CourtChrg"].ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(LoanDT.Rows[0]["CourtChrg"].ToString()));
                                                            }
                                                            else if (Convert.ToDouble(LoanDT.Rows[0]["CourtChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["CourtChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                                            {
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["CourtChrgGl"].ToString(), LoanDT.Rows[0]["CourtChrgSub"].ToString(),
                                                                       TxtPayAccNo.Text.Trim().ToString(), "CRTCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                       Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    // Court Charges Amt Credit To 7 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["CourtChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "7", "1", "7", "Court Charges Credit", TotalDrAmt.ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                                TotalDrAmt = 0;
                                                            }
                                                        }

                                                        //  For Service Charges
                                                        if (Result > 0)
                                                        {
                                                            if (Convert.ToDouble(LoanDT.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["ServiceChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(LoanDT.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["ServiceChrg"].ToString()))
                                                            {
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["ServiceChrgGl"].ToString(), LoanDT.Rows[0]["ServiceChrgSub"].ToString(),
                                                                      TxtPayAccNo.Text.Trim().ToString(), "SERCR", txtNarration.Text.ToString(), Convert.ToDouble(LoanDT.Rows[0]["ServiceChrg"].ToString()), "1", "7", "TR", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                      Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    // Service Charges Amt Credit To 6 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["ServiceChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "6", "1", "7", "Service Charges Credit", LoanDT.Rows[0]["ServiceChrg"].ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(LoanDT.Rows[0]["ServiceChrg"].ToString()));
                                                            }
                                                            else if (Convert.ToDouble(LoanDT.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["ServiceChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                                            {
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["ServiceChrgGl"].ToString(), LoanDT.Rows[0]["ServiceChrgSub"].ToString(),
                                                                      TxtPayAccNo.Text.Trim().ToString(), "SERCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                      Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    // Service Charges Amt Credit To 6 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["ServiceChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "6", "1", "7", "Service Charges Credit", TotalDrAmt.ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                                TotalDrAmt = 0;
                                                            }
                                                        }

                                                        //  For Notice Charges
                                                        if (Result > 0)
                                                        {
                                                            if (Convert.ToDouble(LoanDT.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["NoticeChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(LoanDT.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["NoticeChrg"].ToString()))
                                                            {
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["NoticeChrgGl"].ToString(), LoanDT.Rows[0]["NoticeChrgSub"].ToString(),
                                                                      TxtPayAccNo.Text.Trim().ToString(), "NOTCR", txtNarration.Text.ToString(), Convert.ToDouble(LoanDT.Rows[0]["NoticeChrg"].ToString()), "1", "7", "TR", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                      Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    // Notice Charges Credit To 5 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["NoticeChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "5", "1", "7", "Notice Charges Credit", LoanDT.Rows[0]["NoticeChrg"].ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(LoanDT.Rows[0]["NoticeChrg"].ToString()));
                                                            }
                                                            else if (Convert.ToDouble(LoanDT.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : LoanDT.Rows[0]["NoticeChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                                            {
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["NoticeChrgGl"].ToString(), LoanDT.Rows[0]["NoticeChrgSub"].ToString(),
                                                                      TxtPayAccNo.Text.Trim().ToString(), "NOTCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                      Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    // Notice Charges Credit To 5 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["NoticeChrgSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "5", "1", "7", "Notice Charges Credit", TotalDrAmt.ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                                TotalDrAmt = 0;
                                                            }
                                                        }

                                                        //  For Interest Receivable
                                                        if (Result > 0)
                                                        {
                                                            if (Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()))
                                                            {
                                                                // Interest Received credit to GL 11
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["InterestRecGl"].ToString(), LoanDT.Rows[0]["InterestRecSub"].ToString(),
                                                                      TxtPayAccNo.Text.Trim().ToString(), "INTRCR", txtNarration.Text.ToString(), Convert.ToDouble((LoanDT.Rows[0]["InterestRec"].ToString())), "1", "7", "TR", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                      Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                                                if (Result > 0)
                                                                {
                                                                    // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["InterestRecSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "4", "1", "7", "Interest Received Credit", Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()).ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()));
                                                            }
                                                            else if (Convert.ToDouble(LoanDT.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt > 0)
                                                            {
                                                                // Interest Received credit to GL 11
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["InterestRecGl"].ToString(), LoanDT.Rows[0]["InterestRecSub"].ToString(),
                                                                      TxtPayAccNo.Text.Trim().ToString(), "INTRCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                      Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["InterestRecSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "4", "1", "7", "Interest Received Credit", TotalDrAmt.ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }
                                                                TotalDrAmt = 0;
                                                            }
                                                        }

                                                        //  For Penal Charge
                                                        if (Result > 0)
                                                        {
                                                            if (Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString())))
                                                            {
                                                                //Penal Charge Credit To GL 12
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["PInterestGl"].ToString(), LoanDT.Rows[0]["PInterestSub"].ToString(),
                                                                      TxtPayAccNo.Text.Trim().ToString(), "PENCR", txtNarration.Text.ToString(), Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString())), "1", "7", "TR", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                      Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    if (Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : LoanDT.Rows[0]["CurrPInterest"].ToString()) > 0)
                                                                    {
                                                                        //Penal Interest Debit To 3 In AVS_LnTrx
                                                                        Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["PInterestSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "3", "2", "7", "Penal Interest Debit", LoanDT.Rows[0]["CurrPInterest"].ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                    }
                                                                }

                                                                if (Result > 0)
                                                                {
                                                                    //Penal Interest Credit To 3 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["PInterestSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "3", "1", "7", "Penal Interest Credit", Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString())).ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }

                                                                if (Result > 0)
                                                                {
                                                                    //  (Penal Charge Contra) - Penal chrg Applied Debit To GL 12
                                                                    Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["PInterestGl"].ToString(), LoanDT.Rows[0]["PInterestSub"].ToString(),
                                                                          TxtPayAccNo.Text.Trim().ToString(), "PENDR", txtNarration.Text.ToString(), Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString())), "2", "12", "TR_INT", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                          Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                    if (Result > 0)
                                                                    {
                                                                        //Penal chrg Applied Credit to GL 100
                                                                        Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", LoanDT.Rows[0]["PlAccNo2"].ToString(),
                                                                              TxtPayAccNo.Text.Trim().ToString(), "PENCR", txtNarration.Text.ToString(), Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString())), "1", "12", "TR_INT", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                              Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                                                    }
                                                                }

                                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString())));
                                                            }
                                                            else if (Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                                            {
                                                                //Penal Charge Credit To GL 12
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["PInterestGl"].ToString(), LoanDT.Rows[0]["PInterestSub"].ToString(),
                                                                          TxtPayAccNo.Text.Trim().ToString(), "PENCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                          Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    if (Convert.ToDouble(LoanDT.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : LoanDT.Rows[0]["CurrPInterest"].ToString()) > 0)
                                                                    {
                                                                        //Penal Interest Debit To 3 In AVS_LnTrx
                                                                        Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["PInterestSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "3", "2", "7", "Penal Interest Debit", LoanDT.Rows[0]["CurrPInterest"].ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                    }
                                                                }

                                                                if (Result > 0)
                                                                {
                                                                    //Penal Interest Credit To 3 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["PInterestSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "3", "1", "7", "Penal Interest Credit", TotalDrAmt.ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }

                                                                if (Result > 0)
                                                                {
                                                                    //  (Penal Charge Contra) - Penal chrg Applied Debit To GL 12
                                                                    Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["PInterestGl"].ToString(), LoanDT.Rows[0]["PInterestSub"].ToString(),
                                                                          TxtPayAccNo.Text.Trim().ToString(), "PENDR", txtNarration.Text.ToString(), TotalDrAmt, "2", "12", "TR_INT", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                          Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                    if (Result > 0)
                                                                    {
                                                                        //Penal chrg Applied Credit to GL 100
                                                                        Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", LoanDT.Rows[0]["PlAccNo2"].ToString(),
                                                                              TxtPayAccNo.Text.Trim().ToString(), "PENCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "12", "TR_INT", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                              Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                                                    }
                                                                }

                                                                TotalDrAmt = 0;
                                                            }
                                                        }

                                                        //  For Interest
                                                        if (Result > 0)
                                                        {
                                                            if (Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())))
                                                            {
                                                                //interest Credit to GL 11
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["InterestGl"].ToString(), LoanDT.Rows[0]["InterestSub"].ToString(),
                                                                              TxtPayAccNo.Text.Trim().ToString(), "INTCR", txtNarration.Text.ToString(), Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())), "1", "7", "TR", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                              Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    if (Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString() == "" ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString()) > 0)
                                                                    {
                                                                        //Current Interest Debit To 2 In AVS_LnTrx
                                                                        Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "2", "2", "7", "Interest Debit", LoanDT.Rows[0]["CurrInterest"].ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                    }
                                                                }

                                                                if (Result > 0)
                                                                {
                                                                    //Current Interest Credit To 2 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "2", "1", "7", "Interest Credit", Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())).ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }

                                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())));
                                                            }
                                                            else if (Convert.ToDouble(Convert.ToDouble(LoanDT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                                            {
                                                                //interest Credit to GL 11
                                                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), LoanDT.Rows[0]["InterestGl"].ToString(), LoanDT.Rows[0]["InterestSub"].ToString(),
                                                                                  TxtPayAccNo.Text.Trim().ToString(), "INTCR", txtNarration.Text.ToString(), TotalDrAmt, "1", "7", "TR", SetNo1, CN, CD, "0", "0", "1003", "", txtPayBrCode.Text.Trim().ToString(),
                                                                                  Session["MID"].ToString(), "", "", "", txtCustNo.Text.Trim().ToString(), TxtPayAccName.Text.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());

                                                                if (Result > 0)
                                                                {
                                                                    if (Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString() == "" ? "0" : LoanDT.Rows[0]["CurrInterest"].ToString()) > 0)
                                                                    {
                                                                        //Current Interest Debit To 2 In AVS_LnTrx
                                                                        Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "2", "2", "7", "Interest Debit", Convert.ToDouble(LoanDT.Rows[0]["CurrInterest"].ToString()).ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                    }
                                                                }

                                                                if (Result > 0)
                                                                {
                                                                    //Current Interest Credit To 2 In AVS_LnTrx
                                                                    Result = ITrans.LoanTrx(txtPayBrCode.Text.Trim().ToString(), txtPayProdType.Text.Trim().ToString(), LoanDT.Rows[0]["InterestSub"].ToString(), TxtPayAccNo.Text.Trim().ToString(), "2", "1", "7", "Interest Credit", TotalDrAmt.ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                                }

                                                                TotalDrAmt = 0;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lblMessage.Text = "Excess amount of receipt than total payable amount " + Convert.ToDouble(TotalAmt.ToString()).ToString() + "...!!";
                                        ModalPopup.Show(this.Page);
                                        return;
                                    }
                                    //end added for loan
                                }
                                else
                                {
                                    Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PayGlCode"].ToString(), txtPayProdType.Text.Trim().ToString(),
                                        TxtPayAccNo.Text.Trim().ToString(), txtNarration.Text.ToString(), "BY TRF", TotalAmount, "1", "7", "TR", SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                        Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtMemberName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                                }
                            }
                            else if (ddlPaymentType.SelectedValue.ToString() == "4")
                            {
                                Result = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PayGlCode"].ToString(), txtPayProdType.Text.Trim().ToString(),
                                    "0", txtNarration.Text.ToString(), "BY TRF", TotalAmount, "1", "6", "TR", SetNo, CN.ToString(), CD.ToString(), "0", "0", "1001", "", txtPayBrCode.Text.Trim().ToString(),
                                    Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtMemberName.Text.Trim().ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), txtPayBrCode.Text.Trim().ToString());
                            }
                        }
                    }

                    if (Result > 0)
                    {
                        ClearAll();
                        lblMessage.Text = "Successfully Transfer with setno : " + SetNo.ToString();
                        ModalPopup.Show(this.Page);
                        FL = "Insert";//Dhanya 14/09/2017
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "sharedivident_" + SetNo + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        return;
                    }
                }
            }
            else
            {
                ddlPaymentType.SelectedValue = "0";
                lblMessage.Text = "Select at least one or more than one record from grid...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grdAccDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdAccDetails.PageIndex = e.NewPageIndex;
            BindGrid();
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
            if (rbtnType.SelectedValue == "1")
            {
                divSpecific.Visible = true;
                divMultiple.Visible = false;
                grdShareDividend.DataSource = null;
                grdShareDividend.DataBind();
            }
            else if (rbtnType.SelectedValue == "2")
            {
                AutoGlName.ContextKey = Session["BRCD"].ToString();
                sResult = SD.GetECSParam(Session["BRCD"].ToString());

                if (sResult == "Y")
                {
                    divSpecific.Visible = false;
                    divMultiple.Visible = true;
                    grdShareDividend.DataSource = null;
                    grdShareDividend.DataBind();
                    SD.GetDivision(ddlDivision);
                }
                else
                {
                    divSpecific.Visible = false;
                    divMultiple.Visible = true;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDouble(ddlDivision.SelectedValue) > 0)
            {
                SD.GetDepartment(ddlDepartment, Session["BRCD"].ToString(), ddlDivision.SelectedValue.ToString());
            }
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
            string GL = BD.GetAccTypeGL(txtProdCode.Text.Trim().ToString(), Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');

            ViewState["GlCode"] = GLCODE[1].ToString();
            string PDName = SD.GetProductName(txtProdCode.Text.Trim().ToString(), Session["BRCD"].ToString());
            if (PDName != null && PDName != "")
            {
                txtProdName.Text = PDName;
                btnShow.Focus();
            }
            else
            {
                WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                txtProdCode.Text = "";
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
            string custno = txtProdName.Text.ToString();
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtProdName.Text = CT[0].ToString();
                txtProdCode.Text = CT[1].ToString();

                string[] GLS = BD.GetAccTypeGL(txtProdCode.Text.Trim().ToString(), Session["BRCD"].ToString()).Split('_');
                ViewState["GlCode"] = GLS[1].ToString();

                btnShow.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnView_Click(object sender, EventArgs e)
    {
        try
        {


            DT = AS.getAccstatment("DETAILS", Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), txtMemberNo.Text);
            if (DT.Rows.Count > 0)
            {
                grdAccStatement.DataSource = DT;
                grdAccStatement.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('.bs-example-modal-lg').modal('show');</script>", false);
            }
            else
            {
                lblMessage.Text = "Details Not Found For This Account Number...!!";
                ModalPopup.Show(this.Page);
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    //protected void LnkViewDetails_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        LinkButton LNK = new LinkButton();
    //        string STR1 = LNK.CommandArgument.ToString();
    //        string[] STR = STR1.Split('_');

    //        DT = AS.getAccstatment("DETAILS", STR[0].ToString(), Session["ENTRYDATE"].ToString(), STR[2].ToString());
    //        if (DT.Rows.Count > 0)
    //        {
    //            grdAccStatement.DataSource = DT;
    //            grdAccStatement.DataBind();
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('.bs-example-modal-lg').modal('show');</script>", false);
    //        }
    //        else
    //        {
    //            lblMessage.Text = "Details Not Found For This Account Number...!!";
    //            ModalPopup.Show(this.Page);
    //        }

    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
    protected void Lnk_ClickView_Click(object sender, EventArgs e)
    {

        try
        {
            LinkButton LNK = (LinkButton)sender;
            string STR1 = LNK.CommandArgument.ToString();
            string[] STR = STR1.Split('_');

            DT = AS.getAccstatment("DETAILS", STR[0].ToString(), Session["ENTRYDATE"].ToString(), STR[2].ToString());
            if (DT.Rows.Count > 0)
            {
                grdAccStatement.DataSource = DT;
                grdAccStatement.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('.bs-example-modal-lg').modal('show');</script>", false);
            }
            else
            {
                lblMessage.Text = "Details Not Found For This Account Number...!!";
                ModalPopup.Show(this.Page);
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }


    }
}