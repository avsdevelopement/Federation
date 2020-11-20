using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

public partial class FrmAvs5172 : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAVS5030 AC = new ClsAVS5030();
    ClsCommon cmn = new ClsCommon();
    scustom customcs = new scustom();
    ClsAuthorized AT = new ClsAuthorized();
    DataTable DT = new DataTable();
    DataTable DT1 = new DataTable();
    DbConnection conn = new DbConnection();
    ClsVoucherAutho VA = new ClsVoucherAutho();
    ClsAuthoriseCommon AC1 = new ClsAuthoriseCommon();

    string Parti1 = "", Parti2 = "";
    string sResult = "";
    int Result = 0;
    string FL = "";
    string Modal_Flag = "", Message = "";
    double EAMT, AMT, DBT, CRT;
    double Balance = 0;
    string acctypeno = "", acctype = "", jointname = "";
    string STR = "", Setno = "";

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
                Response.Redirect("FrmLogin.aspx");

            if (!IsPostBack)
            {
                LogDetails();
                BindGrid();
                ViewState["Flag"] = "AD";
                BD.BindPayment(ddlPayType, "1");
                TxtPrd.Focus();
            }
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #endregion

    public void BindGrid()
    {
        int R1 = 0;
        R1 = AC.GetEntryData_TD(Grdentry, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "1");
        Grdentry.Visible = true;
    }

    #region Text Changes Events

    protected void TxtPrd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ProdCode();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Txtprdname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] CT = Txtprdname.Text.ToString().Split('_');
            TxtPrd.Text = CT[1].ToString();
            ProdCode();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            sResult = customcs.GetAccountName(TxtAccNo.Text.ToString(), TxtPrd.Text.ToString(), Session["BRCD"].ToString());
            if ((sResult != null) && (sResult != ""))
            {
                DT = AC.GetTDValidaation(Session["BRCD"].ToString(), TxtPrd.Text, TxtAccNo.Text, Session["EntryDate"].ToString(), "FisicalYr", Session["MID"].ToString(), "0");
                if (DT.Rows.Count > 0)
                {
                    DT = AC.GetTDValidaation(Session["BRCD"].ToString(), TxtPrd.Text, TxtAccNo.Text, Session["EntryDate"].ToString(), "LoanBal", Session["MID"].ToString(), "0");
                    if (DT.Rows.Count > 0)
                    {

                        string[] AN = sResult.ToString().Split('_');
                        TxtAccName.Text = AN[1].ToString();
                        GetAccInfo();
                    }
                    else
                    {
                        WebMsgBox.Show("This Account Having Loan Balance...!!", this.Page);
                        string[] AN = sResult.ToString().Split('_');
                        TxtAccName.Text = AN[1].ToString();
                        GetAccInfo();
                    }
                }
                else
                {
                    WebMsgBox.Show("More Than 3 Times Withdrawal Done...!!", this.Page);
                    Cleardata();
                    txtAccNo1.Focus();
                    return;
                }
            }
            else
            {
                TxtAccNo.Text = "";
                TxtAccNo.Focus();
                WebMsgBox.Show("Account not exists ...!!", this.Page);
                Cleardata();
                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] CustNob = TxtAccName.Text.ToString().Split('_');
            if (CustNob.Length > 1)
            {
                TxtAccName.Text = CustNob[0].ToString();
                TxtAccNo.Text = CustNob[1].ToString();
                GetAccInfo();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtCCheque_TextChanged(object sender, EventArgs e)
    {
        Calculation();
        //TxtCunused.Focus();
    }

    protected void TxtCunused_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Calculation();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtChequeNo_TextChanged(object sender, EventArgs e)
    {
        string InstStatus = "";
        try
        {
            string Para = Convert.ToString(conn.sExecuteScalar("select LISTVALUE from PARAMETER where LISTFIELD='ChequeValidate'"));
            if (Para == "Y")
            {
                InstStatus = cmn.ChequeExists(Session["BRCD"].ToString(), TxtPrd.Text.ToString(), TxtAccNo.Text.ToString(), TxtChequeNo.Text.ToString()).ToString();
                if ((InstStatus != null) && (InstStatus != "Issued"))
                {
                    TxtChequeNo.Text = "";
                    TxtChequeNo.Focus();
                    WebMsgBox.Show("Instrument number " + TxtChequeNo.Text.ToString() + " with status - " + InstStatus.ToString(), this.Page);
                    return;
                }
                else
                {
                    if (TxtChequeDate.Text == "")
                        TxtChequeDate.Text = Session["EntryDate"].ToString();
                    TxtChequeDate.Focus();
                    return;
                }
            }
            else
            {
                if (TxtChequeDate.Text == "")
                    TxtChequeDate.Text = Session["EntryDate"].ToString();
                TxtChequeDate.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Payment Mode

    protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtPrd.Text.Trim().ToString() == "")
            {
                TxtPrd.Focus();
                ddlPayType.SelectedValue = "0";
                WebMsgBox.Show("Enter product code first ...!!", this.Page);
                return;
            }
            else if (TxtAccNo.Text.Trim().ToString() == "")
            {
                TxtAccNo.Focus();
                ddlPayType.SelectedValue = "0";
                WebMsgBox.Show("Enter account number first ....!!", this.Page);
                return;
            }
            else
            {
                Clear();
                BindBranch(ddlPayBrName);
                txtAmount.Text = TxtNetBal.Text.ToString() == "" ? "0" : TxtNetBal.Text.ToString();
                txtNarration.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";

                if (ddlPayType.SelectedValue.ToString() == "0")
                {
                    divBranch.Visible = false;
                    Transfer.Visible = false;
                    Transfer1.Visible = false;
                    DivAmount.Visible = false;
                    ddlPayType.Focus();
                }
                else if (ddlPayType.SelectedValue.ToString() == "1")
                {
                    divBranch.Visible = true;
                    Transfer.Visible = false;
                    Transfer1.Visible = false;
                    DivAmount.Visible = true;
                    ddlPayBrName.Enabled = false;

                    txtPayBrCode.Text = Session["BRCD"].ToString();
                    ddlPayBrName.SelectedValue = Session["BRCD"].ToString();
                    AutoGlName2.ContextKey = txtPayBrCode.Text.ToString();
                    txtNarration.Text = "By Cash";
                    txtNarration.Focus();
                }
                else if (ddlPayType.SelectedValue.ToString() == "2")
                {
                    divBranch.Visible = true;
                    Transfer.Visible = true;
                    Transfer1.Visible = false;
                    DivAmount.Visible = true;
                    ddlPayBrName.Enabled = true;

                    txtPayBrCode.Text = Session["BRCD"].ToString();
                    ddlPayBrName.SelectedValue = Session["BRCD"].ToString();
                    AutoGlName2.ContextKey = txtPayBrCode.Text.ToString();
                    txtNarration.Text = "By Trf";
                    ddlPayBrName.Focus();
                }
                else if (ddlPayType.SelectedValue.ToString() == "4")
                {
                    divBranch.Visible = true;
                    Transfer.Visible = true;
                    Transfer1.Visible = true;
                    DivAmount.Visible = true;
                    ddlPayBrName.Enabled = true;

                    txtPayBrCode.Text = Session["BRCD"].ToString();
                    ddlPayBrName.SelectedValue = Session["BRCD"].ToString();
                    AutoGlName2.ContextKey = txtPayBrCode.Text.ToString();
                    txtNarration.Text = "To Self";
                    ddlPayBrName.Focus();
                }
                else
                {
                    divBranch.Visible = false;
                    Transfer.Visible = false;
                    Transfer1.Visible = false;
                    ddlPayType.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlPayBrName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtPayBrCode.Text = ddlPayBrName.SelectedValue;
            if (Convert.ToDouble(txtPayBrCode.Text.ToString()) > 0)
            {
                Clear();
                if (ddlPayType.SelectedValue.ToString() == "1")
                {
                    txtNarration.Attributes["onfocus"] = "var value = this.value; this.value = ''; this.value = value; onfocus = null;";
                    txtNarration.Focus();
                }
                else if ((ddlPayType.SelectedValue.ToString() == "2") || (ddlPayType.SelectedValue.ToString() == "4"))
                {
                    AutoGlName2.ContextKey = txtPayBrCode.Text.ToString();
                    txtProdType1.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("First select proper branch ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtProdType1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //Added By Amol on 22092017 as per ambika mam instruction

            if (BD.GetProdOperate(txtPayBrCode.Text.ToString(), txtProdType1.Text.ToString()).ToString() != "3")
            {
                sResult = AC.GetProduct(txtPayBrCode.Text.ToString(), txtProdType1.Text.ToString());
                if (sResult != null)
                {
                    string[] ACC = sResult.Split('_'); ;
                    ViewState["GlCode1"] = ACC[0].ToString();
                    txtProdName1.Text = ACC[2].ToString();
                    AutoAccName2.ContextKey = txtPayBrCode.Text.ToString() + "_" + txtProdType1.Text.ToString();

                    if (cmn.GetIntACCYN(txtPayBrCode.Text.ToString(), txtProdType1.Text.ToString()) != "Y")
                    {
                        txtAccNo1.Text = txtProdType1.Text;
                        txtAccName1.Text = txtProdName1.Text;
                    }
                    else
                    {
                        txtAccNo1.Text = "";
                        txtAccName1.Text = "";
                    }
                    txtAccNo1.Focus();
                }
                else
                {
                    txtProdType1.Text = "";
                    txtProdName1.Text = "";
                    txtProdType1.Focus();
                    WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                    return;
                }
            }
            else
            {
                txtProdType1.Text = "";
                txtProdName1.Text = "";
                txtProdType1.Focus();
                WebMsgBox.Show("Product is not operating ...!!", this.Page);
                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtProdName1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] CT = txtProdName1.Text.ToString().Split('_');
            txtProdType1.Text = CT[1].ToString();

            if (BD.GetProdOperate(txtPayBrCode.Text.ToString(), txtProdType1.Text.ToString()).ToString() != "3")
            {
                sResult = AC.GetProduct(txtPayBrCode.Text.ToString(), txtProdType1.Text.ToString());
                if (sResult != null)
                {
                    string[] ACC = sResult.Split('_'); ;
                    ViewState["GlCode1"] = ACC[0].ToString();
                    txtProdName1.Text = ACC[2].ToString();
                    AutoAccName2.ContextKey = txtPayBrCode.Text.ToString() + "_" + txtProdType1.Text.ToString();

                    if (cmn.GetIntACCYN(txtPayBrCode.Text.ToString(), txtProdType1.Text.ToString()) != "Y")
                    {
                        txtAccNo1.Text = txtProdType1.Text;
                        txtAccName1.Text = txtProdName1.Text;
                    }
                    else
                    {
                        txtAccNo1.Text = "";
                        txtAccName1.Text = "";
                    }
                    txtAccNo1.Focus();
                }
                else
                {
                    txtProdType1.Text = "";
                    txtProdName1.Text = "";
                    txtProdType1.Focus();
                    WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                    return;
                }
            }
            else
            {
                txtProdType1.Text = "";
                txtProdName1.Text = "";
                txtProdType1.Focus();
                WebMsgBox.Show("Product is not operating ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtAccNo1_TextChanged(object sender, EventArgs e)
    {
        try
        {

            sResult = BD.Getstage1(txtAccNo1.Text.ToString(), txtPayBrCode.Text.ToString(), txtProdType1.Text.ToString());
            if (sResult != null)
            {
                if (sResult != "1003")
                {
                    txtAccNo1.Text = "";
                    txtAccName1.Text = "";
                    txtAccNo1.Focus();
                    WebMsgBox.Show("Sorry Customer not Authorise ...!!", this.Page);
                    return;
                }
                else
                {
                    sResult = AC.GetAccStatus(txtPayBrCode.Text.ToString(), txtProdType1.Text, txtAccNo1.Text.ToString());

                    if (sResult == "3")
                    {
                        txtAccNo1.Text = "";
                        txtAccName1.Text = "";
                        txtAccNo1.Focus();
                        WebMsgBox.Show("Already account is closed ...!!", this.Page);
                        return;
                    }
                    else
                    {
                        DT = new DataTable();
                        DT = AC.GetCustName(txtProdType1.Text.ToString(), txtAccNo1.Text.ToString(), txtPayBrCode.Text);
                        if (DT.Rows.Count > 0)
                        {
                            string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                            txtAccName1.Text = CustName[0].ToString();

                            if (ddlPayType.SelectedValue == "2")
                                txtNarration.Focus();
                            if (ddlPayType.SelectedValue == "4")
                                TxtChequeNo.Focus();
                        }
                    }
                }
            }
            else
            {
                txtAccNo1.Text = "";
                txtAccName1.Text = "";
                txtAccNo1.Focus();
                WebMsgBox.Show("Enter valid account number...!!", this.Page);
            }
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtAccName1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] CustDetails = txtAccName1.Text.ToString().Split('_');
            if (CustDetails.Length > 1)
            {
                txtAccNo1.Text = (string.IsNullOrEmpty(CustDetails[1].ToString()) ? "" : CustDetails[1].ToString());

                sResult = BD.Getstage1(txtAccNo1.Text.ToString(), txtPayBrCode.Text.ToString(), txtProdType1.Text.ToString());
                if (sResult != null)
                {
                    if (sResult != "1003")
                    {
                        txtAccNo1.Text = "";
                        txtAccName1.Text = "";
                        txtAccNo1.Focus();
                        WebMsgBox.Show("Sorry Customer not Authorise ...!!", this.Page);
                        return;
                    }
                    else
                    {
                        sResult = AC.GetAccStatus(txtPayBrCode.Text.ToString(), txtProdType1.Text, txtAccNo1.Text.ToString());

                        if (sResult == "3")
                        {
                            txtAccNo1.Text = "";
                            txtAccName1.Text = "";
                            txtAccNo1.Focus();
                            WebMsgBox.Show("Account is closed ...!!", this.Page);
                            return;
                        }
                        else
                        {
                            DT = new DataTable();
                            DT = AC.GetCustName(txtProdType1.Text.ToString(), txtAccNo1.Text.ToString(), txtPayBrCode.Text.ToString());
                            if (DT.Rows.Count > 0)
                            {
                                string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                                txtAccName1.Text = CustName[0].ToString();

                                if (ddlPayType.SelectedValue == "2")
                                    txtNarration.Focus();
                                if (ddlPayType.SelectedValue == "4")
                                    TxtChequeNo.Focus();
                            }
                        }
                    }
                }
                else
                {
                    txtAccNo1.Text = "";
                    txtAccName1.Text = "";
                    txtAccNo1.Focus();
                    WebMsgBox.Show("Enter valid account number...!!", this.Page);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Click events

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string GlCode1 = "", SubGlCode1 = "", AccNo1 = "";
        string ChqNo = "0", ChqDate = "01/01/1900", Activity = "", PmtMocde = "", SetNo = "";
        double Balance, Interest, UnChqChrg, SerChrg, ErlyChrg, SGST, CGST, TotDeduction, NetBalance, TotalBal, TotalDebit = 0;

        try
        {
            if (ViewState["Flag"].ToString() == "AD")
            {
                if (TxtPrd.Text.ToString() == "")
                {
                    TxtPrd.Focus();
                    WebMsgBox.Show("Enter product code first ...!!", this.Page);
                    return;
                }
                else if (TxtAccNo.Text.ToString() == "")
                {
                    TxtAccNo.Focus();
                    WebMsgBox.Show("Enter account number first ....!!", this.Page);
                    return;
                }
                else if (ddlPayType.SelectedValue.ToString() == "0")
                {
                    WebMsgBox.Show("Select payment mode first ...!!", this.Page);
                    return;
                }
                else if (ddlPayType.SelectedValue.ToString() == "1")
                {
                    Activity = "4";
                    PmtMocde = "CP";
                    if (txtPayBrCode.Text.ToString() == "")
                    {
                        txtPayBrCode.Focus();
                        WebMsgBox.Show("Select branch code first ...!!", this.Page);
                        return;
                    }
                }
                else if (ddlPayType.SelectedValue.ToString() == "2")
                {
                    Activity = "7";
                    PmtMocde = "TR";
                    if (txtPayBrCode.Text.ToString() == "")
                    {
                        txtPayBrCode.Focus();
                        WebMsgBox.Show("Select branch code first ...!!", this.Page);
                        return;
                    }
                    else if (txtProdType1.Text.ToString() == "")
                    {
                        txtProdType1.Focus();
                        WebMsgBox.Show("Enter product code first ...!!", this.Page);
                        return;
                    }
                    else if (txtAccNo1.Text.ToString() == "")
                    {
                        txtAccNo1.Focus();
                        WebMsgBox.Show("Enter account number first ....!!", this.Page);
                        return;
                    }
                }
                else if (ddlPayType.SelectedValue.ToString() == "4")
                {
                    Activity = "6";
                    PmtMocde = "TR";
                    if (txtPayBrCode.Text.ToString() == "")
                    {
                        txtPayBrCode.Focus();
                        WebMsgBox.Show("Select branch code first ...!!", this.Page);
                        return;
                    }
                    else if (txtProdType1.Text.ToString() == "")
                    {
                        txtProdType1.Focus();
                        WebMsgBox.Show("Enter product code first ...!!", this.Page);
                        return;
                    }
                    else if (txtAccNo1.Text.ToString() == "")
                    {
                        txtAccNo1.Focus();
                        WebMsgBox.Show("Enter account number first ....!!", this.Page);
                        return;
                    }
                    else if (TxtChequeNo.Text.ToString() == "")
                    {
                        TxtChequeNo.Focus();
                        WebMsgBox.Show("Enter instrument number first ...!!", this.Page);
                        return;
                    }
                    else if (TxtChequeDate.Text.ToString() == "")
                    {
                        TxtChequeDate.Focus();
                        WebMsgBox.Show("Enter instrument date first ....!!", this.Page);
                        return;
                    }
                    ChqNo = TxtChequeNo.Text.Trim().ToString();
                    ChqDate = TxtChequeDate.Text.Trim().ToString();
                }

                Balance = (float)Math.Round(Convert.ToDouble(TxtCBal.Text.ToString() == "" ? "0" : TxtCBal.Text.ToString()), 2);
                Interest = (float)Math.Round(Convert.ToDouble(TxtIntAppld.Text.ToString() == "" ? "0" : TxtIntAppld.Text.ToString()), 2);
                // UnChqChrg //= Convert.ToInt32(TxtUnusedChrg.Text.ToString() == "" ? "0" : TxtUnusedChrg.Text.ToString());
                NetBalance = Math.Round(Convert.ToDouble(TxtNetBal.Text.ToString() == "" ? "0" : TxtNetBal.Text.ToString()), 2);
                TotalBal = Math.Round(Convert.ToDouble(TxtTotalbal.Text.ToString() == "" ? "0" : TxtTotalbal.Text.ToString()), 2);

                string BasicPay = AC.GetBasicPay_Month("TDAMOUNT");
                string BasicPayAbove = AC.GetBasicPay_Month("TDBALMINI");
                string BasicMaxPay = AC.GetBasicPay_Month("TDBALMAX");

                if (ddlPayType.SelectedValue != "0")
                {
                    TotalDebit = 0;
                    if (txtPayBrCode.Text.ToString() != Session["BRCD"].ToString())
                        SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "IBTSetNo", Session["BRCD"].ToString()).ToString();
                    else
                        SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();

                    if (Convert.ToDouble(SetNo.ToString()) > 0)
                    {
                        if (ddlPayType.SelectedValue == "1")
                        {
                            GlCode1 = "99";
                            SubGlCode1 = "99";
                            AccNo1 = TxtAccNo.Text.ToString();
                            txtAccName1.Text = "Cash";
                        }
                        else if ((ddlPayType.SelectedValue == "2") || (ddlPayType.SelectedValue == "4"))
                        {
                            GlCode1 = ViewState["GlCode1"].ToString();
                            SubGlCode1 = txtProdType1.Text.ToString();
                            AccNo1 = txtAccNo1.Text.ToString();
                        }

                        Parti1 = "From " + Session["BRCD"].ToString() + " - " + TxtPrd.Text.ToString() + "/" + TxtAccNo.Text.ToString() + " - " + TxtAccName.Text.ToString() + " ";
                        Parti2 = "To " + txtPayBrCode.Text.ToString() + " - " + SubGlCode1.ToString() + "/" + AccNo1.ToString() + " - " + txtAccName1.Text.ToString() + " ";

                        //  For credit interest to account if exists

                        DT = AC.GetPLACC(Session["BRCD"].ToString(), TxtPrd.Text.ToString());
                        if (DT.Rows.Count > 0 && Interest > 0 && Convert.ToDouble(DT.Rows[0]["GlCode"].ToString()) > 0 && Convert.ToDouble(DT.Rows[0]["SubGlCode"].ToString()) > 0)
                        {
                            Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GlCode"].ToString(), DT.Rows[0]["SubGlCode"].ToString(),
                                "0", "Assist_to_closure", "Interest_DR", Interest.ToString(), "2", "7", "TR_INT", SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "TDWithdrawal", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");

                            if (Result > 0)
                            {
                                Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString(), TxtPrd.Text.ToString(),
                                TxtAccNo.Text.ToString(), "Assist_to_closure", "Interest_CR", Interest.ToString(), "1", "7", "TR_INT", SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "TDWithdrawal", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");
                            }
                        }

                        //  For total debit to account
                        if (((Interest > 0) && (Result > 0)) || ((Balance + Interest) > 0))
                        {
                            Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString(), TxtPrd.Text.ToString(),
                                TxtAccNo.Text.ToString(), Parti2.ToString(), "Total_Deduction", (NetBalance).ToString(), "2", Activity, PmtMocde, SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "TDWithdrawal", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");

                            //  For credit total net balance to account if exists
                            if ((((Balance + Interest) - TotalDebit) > 0) && (Result > 0))
                            {
                                if ((Session["BRCD"].ToString() == Session["HOBRCD"].ToString()) && (txtPayBrCode.Text.ToString() == Session["HOBRCD"].ToString()))
                                {
                                    Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode1, SubGlCode1, AccNo1,
                                        Parti1.ToString(), "Net_Payable", ((NetBalance) - TotalDebit).ToString(), "1", Activity, PmtMocde, SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                        Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "TDWithdrawal", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");
                                }
                                else if ((Session["BRCD"].ToString() == Session["HOBRCD"].ToString()) || (txtPayBrCode.Text.ToString() == Session["HOBRCD"].ToString()))
                                {
                                    if (Result > 0)
                                    {
                                        DT1 = AC.GetADMSubGl(Session["HOBRCD"].ToString());
                                        Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT1.Rows[0]["ADMGlCode"].ToString(), DT1.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                            Parti1.ToString(), "Net_Payable", ((NetBalance) - TotalDebit).ToString(), "1", Activity, PmtMocde, SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                            Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "TDWithdrawal", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");
                                    }

                                    if (Result > 0)
                                    {
                                        DT1 = AC.GetADMSubGl(Session["BRCD"].ToString());
                                        Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT1.Rows[0]["ADMGlCode"].ToString(), DT1.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                            Parti2.ToString(), "Net_Payable", ((NetBalance) - TotalDebit).ToString(), "2", Activity, PmtMocde, SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                            Session["HOBRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "TDWithdrawal", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");
                                    }

                                    if (Result > 0)
                                    {
                                        Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode1, SubGlCode1, AccNo1,
                                            Parti1.ToString(), "Net_Payable", ((NetBalance) - TotalDebit).ToString(), "1", Activity, PmtMocde, SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                            Session["HOBRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "TDWithdrawal", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");
                                    }
                                }
                                else if (Session["BRCD"].ToString() != txtPayBrCode.Text.ToString())
                                {
                                    if (Result > 0)
                                    {
                                        DT1 = AC.GetADMSubGl(Session["HOBRCD"].ToString());
                                        Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT1.Rows[0]["ADMGlCode"].ToString(), DT1.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                            Parti1.ToString(), "Net_Payable", ((NetBalance) - TotalDebit).ToString(), "1", Activity, PmtMocde, SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                            Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "TDWithdrawal", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");
                                    }

                                    if (Result > 0)
                                    {
                                        DT1 = AC.GetADMSubGl(Session["BRCD"].ToString());
                                        Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT1.Rows[0]["ADMGlCode"].ToString(), DT1.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                            Parti2.ToString(), "Net_Payable", ((NetBalance) - TotalDebit).ToString(), "2", Activity, PmtMocde, SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                            Session["HOBRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "TDWithdrawal", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");
                                    }

                                    if (Result > 0)
                                    {
                                        DT1 = AC.GetADMSubGl(txtPayBrCode.Text.ToString());
                                        Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT1.Rows[0]["ADMGlCode"].ToString(), DT1.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                            Parti1.ToString(), "Net_Payable", ((NetBalance) - TotalDebit).ToString(), "1", Activity, PmtMocde, SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                            Session["HOBRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "TDWithdrawal", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");
                                    }

                                    if (Result > 0)
                                    {
                                        DT1 = AC.GetADMSubGl(Session["HOBRCD"].ToString());
                                        Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT1.Rows[0]["ADMGlCode"].ToString(), DT1.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                            Parti2.ToString(), "Net_Payable", ((NetBalance) - TotalDebit).ToString(), "2", Activity, PmtMocde, SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                            txtPayBrCode.Text.ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "TDWithdrawal", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");
                                    }

                                    if (Result > 0)
                                    {
                                        Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode1, SubGlCode1, AccNo1,
                                            Parti1.ToString(), "Net_Payable", ((NetBalance) - TotalDebit).ToString(), "1", Activity, PmtMocde, SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                            txtPayBrCode.Text.ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "TDWithdrawal", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");
                                    }
                                }
                            }

                            if (Result > 0)
                            {
                                // Log Details Insert
                                CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Assisttoclosure_" + SetNo.ToString() + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                                //  For close respective account status, closingdate and cid
                                if (Rdeatils.SelectedValue == "2")
                                {
                                    AC.CloseAccount(Session["BRCD"].ToString(), TxtPrd.Text.ToString(), TxtAccNo.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                                }
                                //  Bind recent posted voucher
                                AC.GetEntryData_TD(Grdentry, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), SetNo.ToString());
                                Cleardata();
                                divBranch.Visible = false;
                                Transfer.Visible = false;
                                Transfer1.Visible = false;
                                DivAmount.Visible = false;
                                Entry.Visible = true;
                                WebMsgBox.Show("Voucher posted successfully with set no : " + SetNo.ToString() + " ...!!", this.Page);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    ddlPayType.Focus();
                    WebMsgBox.Show("Select Payment mode first ...!!", this.Page);
                    return;
                }
            }
            if (ViewState["Flag"].ToString() == "AT")
            {
                Message = VA.CheckVoucher(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString());
                if (Message.ToString() == "0")
                {
                    sResult = AC1.CheckVoucherStage(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString(), ViewState["ScrollNo"].ToString());

                    if (sResult != "1003" && sResult != "1004")
                    {
                        Result = AC.VoucherAuthorizeCRCP(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString(), Session["MID"].ToString(), ViewState["ScrollNo"].ToString());

                        if (Result > 0)
                        {
                            //Balance = BD.ClBalance(Session["BRCD"].ToString(), ViewState["SubGlCode"].ToString(), ViewState["Accno"].ToString(), Session["EntryDate"].ToString(), "ClBal");
                            //Message = "Your a/c no " + ViewState["Accno"].ToString() + " has been credited with Rs " + Convert.ToDouble(txtPassAmount.Text.Trim().ToString()) + " On Date " + Session["EntryDate"].ToString() + ". Available a/c balance is Rs " + Balance + ".";
                            //BD.InsertSMSRec(Session["BRCD"].ToString(), ViewState["SubGlCode"].ToString(), ViewState["Accno"].ToString(), Message, Session["MID"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "Receipt");
                            WebMsgBox.Show("Voucher no " + ViewState["SetNo"].ToString() + " successfully authorised...!!", this.Page);
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Voucher no " + ViewState["SetNo"].ToString() + " already authorise...!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }

                    STR = VA.GetPAYMAST(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString(), ViewState["ScrollNo"].ToString());
                    hdnset.Value = ViewState["SetNo"].ToString();
                    ViewState["SETNO"] = ViewState["SetNo"].ToString();
                }
                else
                {
                    WebMsgBox.Show("Voucher Not Tally... Please check in voucher view ...!!", this.Page);
                    return;
                }
            }
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (TxtPrd.Text.ToString() == "")
    //        {
    //            TxtPrd.Focus();
    //            WebMsgBox.Show("Enter product code first...!!", this.Page);
    //            return;
    //        }
    //        else if (TxtAccNo.Text.ToString() == "")
    //        {
    //            TxtAccNo.Focus();
    //            WebMsgBox.Show("Enter account number first...!!", this.Page);
    //            return;
    //        }
    //        else
    //        {
    //            CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Assisttoclosure_" + TxtPrd.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
    //            string redirectURL = "FrmRView.aspx?BC=" + Session["BRCD"].ToString() + "&PC=" + TxtPrd.Text.ToString() + "&FD=" + Session["EntryDate"].ToString() + "&TD=" + Session["EntryDate"].ToString() + "&rptname=RptSBIntCalculation.rdlc";
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        try
        {
            Cleardata();
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

    #endregion

    #region Function

    //  Added by amol on 03/10/2018 for log details
    public void LogDetails()
    {
        try
        {
            cmn.LogDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "Assist To Closure", "", "", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ProdCode()
    {
        try
        {
            //Added By Amol on 22092017 as per ambika mam instruction
            if (BD.GetProdOperate(Session["BRCD"].ToString(), TxtPrd.Text.ToString()).ToString() != "3")
            {
                sResult = AC.GetProduct(Session["BRCD"].ToString(), TxtPrd.Text.ToString());
                if (sResult != null)
                {
                    string[] ACC = sResult.ToString().Split('_'); ;
                    ViewState["GlCode"] = ACC[0].ToString();
                    Txtprdname.Text = ACC[2].ToString();
                    AutoAccName1.ContextKey = Session["BRCD"].ToString() + "_" + TxtPrd.Text.ToString();

                    TxtAccNo.Text = "";
                    TxtAccName.Text = "";
                    TxtAccNo.Focus();
                }
                else
                {
                    TxtPrd.Text = "";
                    Txtprdname.Text = "";
                    TxtPrd.Focus();
                    WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                    return;
                }
            }
            else
            {
                TxtPrd.Text = "";
                Txtprdname.Text = "";
                TxtPrd.Focus();
                WebMsgBox.Show("Product is not operating ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindBranch(DropDownList ddlPayBrName)
    {
        try
        {
            BD.BindBRANCHNAME(ddlPayBrName, null);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void GetAccInfo()
    {
        try
        {
            DT = AC.GetAccInfo(Session["BRCD"].ToString(), TxtPrd.Text.ToString(), TxtAccNo.Text.ToString());
            if (DT.Rows.Count > 0)
            {
                if (Convert.ToDouble(DT.Rows[0]["Acc_Status"].ToString()) == 1)
                {
                    TxtAccSTS.Text = DT.Rows[0]["Acc_Status"].ToString();
                    TxtAccSTSName.Text = DT.Rows[0]["AccDesc"].ToString();
                    TxtAccType.Text = DT.Rows[0]["MID"].ToString();
                    TxtAccTName.Text = DT.Rows[0]["UserName"].ToString();
                    TxtOpeningDate.Text = DT.Rows[0]["OpenDate"].ToString();
                    ViewState["CustNo"] = DT.Rows[0]["CustNo"].ToString();
                    ViewState["GlCode"] = DT.Rows[0]["GlCode"].ToString();

                    TxtCBal.Text = BD.ClBalance(Session["BRCD"].ToString(), TxtPrd.Text.ToString(), TxtAccNo.Text.ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
                    TxtTotalbal.Text = BD.ClBalance(Session["BRCD"].ToString(), TxtPrd.Text.ToString(), TxtAccNo.Text.ToString(), Session["EntryDate"].ToString(), "MainBal").ToString();
                    TxtIntAppld.Text = AC.IntBalance(Session["BRCD"].ToString(), TxtPrd.Text.ToString(), TxtAccNo.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString()).ToString();

                    // TxtCCheque.Text = AC.CheckRates().ToString();
                    string TotalChq = AC.UnusedCheck(Session["BRCD"].ToString(), TxtPrd.Text.ToString(), TxtAccNo.Text.ToString());
                    string TotalUse = AC.CheckStock(Session["BRCD"].ToString(), TxtPrd.Text.ToString(), TxtAccNo.Text.ToString());
                    // TxtCunused.Text = (Convert.ToInt32(TotalChq) < Convert.ToInt32(TotalUse) ? 0 : Convert.ToInt32(TotalChq) - Convert.ToInt32(TotalUse)).ToString();
                    Calculation();
                }
                else if (Convert.ToDouble(DT.Rows[0]["Acc_Status"].ToString()) == 2)
                {
                    Cleardata();
                    WebMsgBox.Show("Acc Number " + TxtAccNo.Text + " is In-Operative ...!!", this.Page);
                    return;
                }
                else if (Convert.ToDouble(DT.Rows[0]["Acc_Status"].ToString()) == 3)
                {
                    Cleardata();
                    WebMsgBox.Show("Acc Number " + TxtAccNo.Text + " is Closed ...!!", this.Page);
                    return;
                }
                else if (Convert.ToDouble(DT.Rows[0]["Acc_Status"].ToString()) == 4)
                {
                    Cleardata();
                    WebMsgBox.Show("Acc Number " + TxtAccNo.Text + " is Lean Marked / Loan Advanced ...!!", this.Page);
                    return;
                }
                else if (Convert.ToDouble(DT.Rows[0]["Acc_Status"].ToString()) == 5)
                {
                    Cleardata();
                    WebMsgBox.Show("Acc Number " + TxtAccNo.Text + " is Credit Freezed ...!!", this.Page);
                    return;
                }
                else if (Convert.ToDouble(DT.Rows[0]["Acc_Status"].ToString()) == 6)
                {
                    Cleardata();
                    WebMsgBox.Show("Acc Number " + TxtAccNo.Text + " is Debit Freezed ...!!", this.Page);
                    return;
                }
                else if (Convert.ToDouble(DT.Rows[0]["Acc_Status"].ToString()) == 7)
                {
                    Cleardata();
                    WebMsgBox.Show("Acc Number " + TxtAccNo.Text + " is Total Freezed ...!!", this.Page);
                    return;
                }
                else if (Convert.ToDouble(DT.Rows[0]["Acc_Status"].ToString()) == 8)
                {
                    Cleardata();
                    WebMsgBox.Show("Acc Number " + TxtAccNo.Text + " is Dormant / In Operative ...!!", this.Page);
                    return;
                }
                else if (Convert.ToDouble(DT.Rows[0]["Acc_Status"].ToString()) == 9)
                {
                    Cleardata();
                    WebMsgBox.Show("Acc Number " + TxtAccNo.Text + " is Suit File ...!!", this.Page);
                    return;
                }
                else if (Convert.ToDouble(DT.Rows[0]["Acc_Status"].ToString()) == 10)
                {
                    Cleardata();
                    WebMsgBox.Show("Acc Number " + TxtAccNo.Text + " is Call back Acc ...!!", this.Page);
                    return;
                }
                else if (Convert.ToDouble(DT.Rows[0]["Acc_Status"].ToString()) == 11)
                {
                    Cleardata();
                    WebMsgBox.Show("Acc Number " + TxtAccNo.Text + " is NPA Acc ...!!", this.Page);
                    return;
                }
                else if (Convert.ToDouble(DT.Rows[0]["Acc_Status"].ToString()) == 12)
                {
                    Cleardata();
                    WebMsgBox.Show("Acc Number " + TxtAccNo.Text + " Have Interest Suspended ...!!", this.Page);
                    return;
                }
                else
                {
                    TxtAccNo.Text = "";
                    TxtAccName.Text = "";
                    TxtAccNo.Focus();
                    WebMsgBox.Show("Account Number Is Invalid ...!!", this.Page);
                    return;
                }
            }
            else
            {
                TxtAccNo.Text = "";
                TxtAccName.Text = "";
                TxtAccNo.Focus();
                WebMsgBox.Show("Sorry Account Is Not Present ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    //  Added By Amol on 15/05/2018 for final calculation
    public void Calculation()
    {
        try
        {

            float ClosingBal = (float)Math.Round(Convert.ToDouble(TxtCBal.Text.ToString() == "" ? "0" : TxtCBal.Text.ToString()), 2);
            float Interest = (float)Math.Round(Convert.ToDouble(TxtIntAppld.Text.ToString() == "" ? "0" : TxtIntAppld.Text.ToString()), 2);
            Txtpayable.Text = Math.Round(Convert.ToDouble(ClosingBal + Interest), 2).ToString();

            //TxtNetBal.Text = Math.Round(Convert.ToDouble((ClosingBal + Interest)), 2).ToString();
            TxtNetBal.Text = "0";

            double NetBalance = Math.Round(Convert.ToDouble(TxtNetBal.Text.ToString() == "" ? "0" : TxtNetBal.Text.ToString()), 2);

            TxtNetBal.Text = Math.Round(Convert.ToDouble(TxtNetBal.Text.ToString() == "" ? "0" : TxtNetBal.Text.ToString()), 2).ToString();

            if (Math.Round(Convert.ToDouble(NetBalance), 2) > Math.Round(Convert.ToDouble(ClosingBal + Interest), 2))
            {
                WebMsgBox.Show("Insufficient account balance ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    //  End Added By Amol on 15/05/2018

    public void Cleardata()
    {
        TxtAccNo.Text = "";
        TxtAccName.Text = "";
        TxtAccSTS.Text = "";
        TxtAccSTSName.Text = "";
        TxtAccType.Text = "";
        TxtAccTName.Text = "";
        TxtOpeningDate.Text = "";
        TxtCBal.Text = "";
        TxtTotalbal.Text = "";
        TxtIntAppld.Text = "";
        // TxtCCheque.Text = "";
        // TxtCunused.Text = "";
        // TxtUnusedChrg.Text = "";
        TxtNetBal.Text = "";
        Txtpayable.Text = "";
        Clear();
        ddlPayType.SelectedValue = "0";

        TxtAccNo.Focus();
    }

    protected void Clear()
    {
        try
        {
            txtProdType1.Text = "";
            txtProdName1.Text = "";
            txtAccNo1.Text = "";
            txtAccName1.Text = "";
            TxtChequeNo.Text = "";
            TxtChequeDate.Text = Session["EntryDate"].ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        finally
        {
            txtProdType1.Focus();
        }
    }

    #endregion

    protected void TxtNetBal_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double Balance, Interest, NetBal;
            string BasicPay = AC.GetBasicPay_Month("TDAMOUNT");
            string BasicPayAbove = AC.GetBasicPay_Month("TDBALMINI");
            string BasicMaxPay = AC.GetBasicPay_Month("TDBALMAX");

            Balance = (float)Math.Round(Convert.ToDouble(TxtCBal.Text.ToString() == "" ? "0" : TxtCBal.Text.ToString()), 2);
            Interest = (float)Math.Round(Convert.ToDouble(TxtIntAppld.Text.ToString() == "" ? "0" : TxtIntAppld.Text.ToString()), 2);
            NetBal = (float)Math.Round(Convert.ToDouble(TxtNetBal.Text.ToString() == "" ? "0" : TxtNetBal.Text.ToString()), 2);

            if ((Math.Round(Convert.ToDouble(Balance + Interest), 2) > Convert.ToSingle(BasicPayAbove)))
            {
                DT = AC.GetTDValidaation(Session["BRCD"].ToString(), TxtPrd.Text, TxtAccNo.Text, Session["EntryDate"].ToString(), "Overdue", Session["MID"].ToString(), NetBal.ToString());
                if (DT.Rows.Count > 0)
                {
                    DT = AC.GetTDValidaation(Session["BRCD"].ToString(), TxtPrd.Text, TxtAccNo.Text, Session["EntryDate"].ToString(), "TDBalance", Session["MID"].ToString(), NetBal.ToString());
                    if (DT.Rows.Count > 0)
                    {
                        if (((Math.Round(Convert.ToDouble(Balance + Interest) - Convert.ToDouble(NetBal), 2) >= Convert.ToSingle(BasicMaxPay)) && (Math.Round(Convert.ToDouble(Balance + Interest)) > Convert.ToSingle(BasicMaxPay))) || (Math.Round(Convert.ToDouble(NetBal)) <= Convert.ToSingle(BasicPay)))
                        {
                            ddlPayType.Focus();
                        }
                        else
                        {
                            WebMsgBox.Show("Withdrawal Amount is Not valid...!!", this.Page);
                            Cleardata();
                            return;
                        }
                    }
                    else
                    {
                        WebMsgBox.Show("Withdrwal Not Allowed: " + NetBal.ToString() + "...!!", this.Page);
                        Cleardata();
                        TxtAccNo.Focus();
                    }
                }
                else
                {
                    WebMsgBox.Show("Last 3 Installment Not Paid...!!", this.Page);
                    Cleardata();
                    TxtAccNo.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("This Account Having Minimum Balance: " + BasicPayAbove.ToString() + " Withdrawal Not Allowed...!!", this.Page);
                Cleardata();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void LnkAutorise_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["SetNo"] = ARR[0].ToString();
            ViewState["ScrollNo"] = ARR[1].ToString();
            ViewState["SubGlCode"] = ARR[2].ToString();
            ViewState["Accno"] = ARR[3].ToString();
            ViewState["Flag"] = "AT";
            BtnSubmit.Text = "Authorise";
            getalldata();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void getalldata()
    {
        try
        {
            DT1 = AC.GetAllFieldData(Session["BRCD"].ToString(), ViewState["SubGlCode"].ToString(), ViewState["Accno"].ToString(), ViewState["SetNo"].ToString(), Session["EntryDate"].ToString());
            if (DT1.Rows.Count > 0)
            {
                string STAGE = "";
                STAGE = DT1.Rows[0]["STAGE"].ToString();
                if ((ViewState["Flag"].ToString() == "MD" || ViewState["Flag"].ToString() == "DL") && Session["UGRP"].ToString() != "1")
                {
                    if (STAGE == "1003")
                    {
                        Clear();
                        WebMsgBox.Show("Record Authorized cannot Modify....", this.Page);
                        ModalPopup.Show(this.Page);
                        return;
                    }
                    else if (STAGE == "1004")
                    {
                        Clear();
                        WebMsgBox.Show("Record Not Present", this.Page);
                        ModalPopup.Show(this.Page);
                        return;
                    }
                }

                TxtPrd.Text = DT1.Rows[0]["Subglcode"].ToString();
                Txtprdname.Text = DT1.Rows[0]["Glname"].ToString();
                TxtAccNo.Text = DT1.Rows[0]["Custname"].ToString();
                TxtAccName.Text = DT1.Rows[0]["ACCNO"].ToString();
                TxtAccSTS.Text = DT1.Rows[0]["ACC_STATUS"].ToString();
                TxtAccSTSName.Text = DT1.Rows[0]["ACC_STATUS"].ToString();
                TxtCBal.Text = Convert.ToInt32(DT1.Rows[0]["ClosingBal"]).ToString();
                TxtIntAppld.Text = "0";
                Txtpayable.Text = "0";
                TxtTotalbal.Text = Convert.ToInt32(DT1.Rows[0]["ClosingBal"]).ToString();
                TxtOpeningDate.Text = Convert.ToDateTime(DT1.Rows[0]["OPENINGDATE"].ToString()).ToString("dd/MM/yyyy");
                TxtNetBal.Text = Convert.ToInt32(DT1.Rows[0]["Amount"]).ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Lnk_ReportView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            string SetNo = ARR[0].ToString();
            string Brcd = ARR[1].ToString();
            string Subgl = ARR[2].ToString();
            string Accno = ARR[3].ToString();

            FL = "Insert";
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SroRecovry_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            string redirectURL = "FrmRView.aspx?BRCD=" + Brcd.ToString() + "&PRD=" + Subgl.ToString() + "&Accno=" + Accno.ToString() + "&SetNo=" + SetNo.ToString() + "&FDate=" + Session["EntryDate"].ToString() + "&rptname=Rpt_TDWithdrwalVchr.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}