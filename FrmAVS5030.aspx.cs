using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

public partial class FrmAVS5030 : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAVS5030 AC = new ClsAVS5030();
    ClsCommon cmn = new ClsCommon();
    scustom customcs = new scustom();
    ClsAuthorized AT = new ClsAuthorized();
    DataTable DT = new DataTable();
    DataTable DT1 = new DataTable();
    string Parti1 = "", Parti2 = "";
    string sResult = "";
    int Result = 0;

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
                Response.Redirect("FrmLogin.aspx");

            if (!IsPostBack)
            {
                //  Added by amol on 03/10/2018 for log details
                LogDetails();

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
                string[] AN = sResult.ToString().Split('_');
                TxtAccName.Text = AN[1].ToString();
                GetAccInfo();
            }
            else
            {
                TxtAccNo.Text = "";
                TxtAccNo.Focus();
                WebMsgBox.Show("Account not exists ...!!", this.Page);
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
            TxtservChg.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtservChg_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Calculation();
            TxterlyClosure.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxterlyClosure_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Calculation();
            TxtGSTState.Focus();
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
        string ChqNo = "0", ChqDate = "01/01/1900", Activity = "", SetNo = "";
        double Balance, Interest, UnChqChrg, SerChrg, ErlyChrg, SGST, CGST, TotDeduction, NetBalance, TotalDebit = 0;

        try
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
            SerChrg = Math.Round(Convert.ToDouble(TxtservChg.Text.ToString() == "" ? "0" : TxtservChg.Text.ToString()), 2);
            ErlyChrg = Math.Round(Convert.ToDouble(TxterlyClosure.Text.ToString() == "" ? "0" : TxterlyClosure.Text.ToString()), 2);
            SGST = Math.Round(Convert.ToDouble(TxtGSTState.Text.ToString() == "" ? "0" : TxtGSTState.Text.ToString()), 2);
            CGST = Math.Round(Convert.ToDouble(TxtGstCentral.Text.ToString() == "" ? "0" : TxtGstCentral.Text.ToString()), 2);
            TotDeduction = Math.Round(Convert.ToDouble(Txtdeductn.Text.ToString() == "" ? "0" : Txtdeductn.Text.ToString()), 2);
            NetBalance = Math.Round(Convert.ToDouble(TxtNetBal.Text.ToString() == "" ? "0" : TxtNetBal.Text.ToString()), 2);

            string BasicPay = AC.GetBasicPay_Month("TDAMOUNT");
            string BasicPayAbove = AC.GetBasicPay_Month("TDBALMINI");

            if (Math.Round(Convert.ToDouble(TotDeduction + NetBalance), 2) == Math.Round(Convert.ToDouble(Balance + Interest), 2))
            {
                if (((Math.Round(Convert.ToDouble(Balance + Interest), 2) > Convert.ToSingle(BasicPayAbove)) && (Convert.ToSingle(BasicPay) < Convert.ToSingle(NetBalance))) || ((Math.Round(Convert.ToDouble(Balance + Interest), 2) < Convert.ToSingle(BasicPayAbove)) && (Convert.ToSingle(BasicPay) > Convert.ToSingle(NetBalance))) ) 
                {
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
                            DT = AC.GetChargeHead(Session["BRCD"].ToString(), "Interest", Session["EntryDate"].ToString());
                            if (DT.Rows.Count > 0 && Interest > 0 && Convert.ToDouble(DT.Rows[0]["GlCode"].ToString()) > 0 && Convert.ToDouble(DT.Rows[0]["SubGlCode"].ToString()) > 0)
                            {
                                Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GlCode"].ToString(), DT.Rows[0]["SubGlCode"].ToString(),
                                    "0", "Assist_to_closure", "Interest_DR", Interest.ToString(), "2", "7", "TR_INT", SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                    Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "SBClose", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");

                                if (Result > 0)
                                {
                                    Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString(), TxtPrd.Text.ToString(),
                                    TxtAccNo.Text.ToString(), "Assist_to_closure", "Interest_CR", Interest.ToString(), "1", "7", "TR_INT", SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                    Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "SBClose", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");
                                }
                            }

                            //  For total debit to account
                            if (((Interest > 0) && (Result > 0)) || ((Balance + Interest) > 0))
                            {
                                Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString(), TxtPrd.Text.ToString(),
                                    TxtAccNo.Text.ToString(), Parti2.ToString(), "Total_Deduction", (Balance + Interest).ToString(), "2", Activity, "TR_Close", SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                    Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "SBClose", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");

                                //  For credit un-used charges to respective head
                                //if (Result > 0)
                                //{
                                //    DT = AC.GetChargeHead(Session["BRCD"].ToString(), "Unused Cheque", Session["EntryDate"].ToString());
                                //    if (Convert.ToDouble(DT.Rows[0]["GlCode"].ToString()) > 0 && Convert.ToDouble(DT.Rows[0]["SubGlCode"].ToString()) > 0)
                                //    {
                                //        Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GlCode"].ToString(), DT.Rows[0]["SubGlCode"].ToString(),
                                //            "0", Parti1.ToString(), "Unused_Cheque", "", "1", Activity, "TR_Close", SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                //            Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "SBClose", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");

                                //        TotalDebit = TotalDebit;
                                //    }
                                //}

                                //  For credit service charges to respective head
                                if (Result > 0)
                                {
                                    DT = AC.GetChargeHead(Session["BRCD"].ToString(), "Service Charge", Session["EntryDate"].ToString());
                                    if (DT.Rows.Count > 0 && SerChrg > 0 && Convert.ToDouble(DT.Rows[0]["GlCode"].ToString()) > 0 && Convert.ToDouble(DT.Rows[0]["SubGlCode"].ToString()) > 0)
                                    {
                                        Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GlCode"].ToString(), DT.Rows[0]["SubGlCode"].ToString(),
                                            "0", Parti1.ToString(), "Service_Charge", SerChrg.ToString(), "1", Activity, "TR_Close", SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                            Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "SBClose", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");

                                        TotalDebit = TotalDebit + SerChrg;
                                    }
                                }

                                //  For credit early closure charges to respective head
                                if (Result > 0)
                                {
                                    DT = AC.GetChargeHead(Session["BRCD"].ToString(), "Early Closer", Session["EntryDate"].ToString());
                                    if (DT.Rows.Count > 0 && ErlyChrg > 0 && Convert.ToDouble(DT.Rows[0]["GlCode"].ToString()) > 0 && Convert.ToDouble(DT.Rows[0]["SubGlCode"].ToString()) > 0)
                                    {
                                        Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GlCode"].ToString(), DT.Rows[0]["SubGlCode"].ToString(),
                                            "0", Parti1.ToString(), "Early_Charge", ErlyChrg.ToString(), "1", Activity, "TR_Close", SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                            Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "SBClose", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");

                                        TotalDebit = TotalDebit + ErlyChrg;
                                    }
                                }

                                //  For credit SGST charges to respective head
                                if (Result > 0)
                                {
                                    DT = AC.GetChargeHead(Session["BRCD"].ToString(), "SGST State", Session["EntryDate"].ToString());
                                    if (DT.Rows.Count > 0 && SGST > 0 && Convert.ToDouble(DT.Rows[0]["GlCode"].ToString()) > 0 && Convert.ToDouble(DT.Rows[0]["SubGlCode"].ToString()) > 0)
                                    {
                                        Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GlCode"].ToString(), DT.Rows[0]["SubGlCode"].ToString(),
                                            "0", Parti1.ToString(), "GST_State", SGST.ToString(), "1", Activity, "TR_Close", SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                            Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "SBClose", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");

                                        TotalDebit = TotalDebit + SGST;
                                    }
                                }

                                //  For credit CGST charges to respective head
                                if (Result > 0)
                                {
                                    DT = AC.GetChargeHead(Session["BRCD"].ToString(), "CGST Central", Session["EntryDate"].ToString());
                                    if (DT.Rows.Count > 0 && CGST > 0 && Convert.ToDouble(DT.Rows[0]["GlCode"].ToString()) > 0 && Convert.ToDouble(DT.Rows[0]["SubGlCode"].ToString()) > 0)
                                    {
                                        Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GlCode"].ToString(), DT.Rows[0]["SubGlCode"].ToString(),
                                            "0", Parti1.ToString(), "GST_Central", CGST.ToString(), "1", Activity, "TR_Close", SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                            Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "SBClose", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");

                                        TotalDebit = TotalDebit + CGST;
                                    }
                                }

                                //  For credit total net balance to account if exists
                                if ((((Balance + Interest) - TotalDebit) > 0) && (Result > 0))
                                {
                                    if ((Session["BRCD"].ToString() == Session["HOBRCD"].ToString()) && (txtPayBrCode.Text.ToString() == Session["HOBRCD"].ToString()))
                                    {
                                        Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode1, SubGlCode1, AccNo1,
                                            Parti1.ToString(), "Net_Payable", ((Balance + Interest) - TotalDebit).ToString(), "1", Activity, "TR_Close", SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                            Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "SBClose", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");
                                    }
                                    else if ((Session["BRCD"].ToString() == Session["HOBRCD"].ToString()) || (txtPayBrCode.Text.ToString() == Session["HOBRCD"].ToString()))
                                    {
                                        if (Result > 0)
                                        {
                                            DT1 = AC.GetADMSubGl(Session["HOBRCD"].ToString());
                                            Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT1.Rows[0]["ADMGlCode"].ToString(), DT1.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                                Parti1.ToString(), "Net_Payable", ((Balance + Interest) - TotalDebit).ToString(), "1", Activity, "TR_Close", SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                                Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "SBClose", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");
                                        }

                                        if (Result > 0)
                                        {
                                            DT1 = AC.GetADMSubGl(Session["BRCD"].ToString());
                                            Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT1.Rows[0]["ADMGlCode"].ToString(), DT1.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                                Parti2.ToString(), "Net_Payable", ((Balance + Interest) - TotalDebit).ToString(), "2", Activity, "TR_Close", SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                                Session["HOBRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "SBClose", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");
                                        }

                                        if (Result > 0)
                                        {
                                            Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode1, SubGlCode1, AccNo1,
                                                Parti1.ToString(), "Net_Payable", ((Balance + Interest) - TotalDebit).ToString(), "1", Activity, "TR_Close", SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                                Session["HOBRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "SBClose", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");
                                        }
                                    }
                                    else if (Session["BRCD"].ToString() != txtPayBrCode.Text.ToString())
                                    {
                                        if (Result > 0)
                                        {
                                            DT1 = AC.GetADMSubGl(Session["HOBRCD"].ToString());
                                            Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT1.Rows[0]["ADMGlCode"].ToString(), DT1.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                                Parti1.ToString(), "Net_Payable", ((Balance + Interest) - TotalDebit).ToString(), "1", Activity, "TR_Close", SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                                Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "SBClose", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");
                                        }

                                        if (Result > 0)
                                        {
                                            DT1 = AC.GetADMSubGl(Session["BRCD"].ToString());
                                            Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT1.Rows[0]["ADMGlCode"].ToString(), DT1.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                                Parti2.ToString(), "Net_Payable", ((Balance + Interest) - TotalDebit).ToString(), "2", Activity, "TR_Close", SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                                Session["HOBRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "SBClose", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");
                                        }

                                        if (Result > 0)
                                        {
                                            DT1 = AC.GetADMSubGl(txtPayBrCode.Text.ToString());
                                            Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT1.Rows[0]["ADMGlCode"].ToString(), DT1.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                                Parti1.ToString(), "Net_Payable", ((Balance + Interest) - TotalDebit).ToString(), "1", Activity, "TR_Close", SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                                Session["HOBRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "SBClose", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");
                                        }

                                        if (Result > 0)
                                        {
                                            DT1 = AC.GetADMSubGl(Session["HOBRCD"].ToString());
                                            Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT1.Rows[0]["ADMGlCode"].ToString(), DT1.Rows[0]["ADMSubGlCode"].ToString(), "0",
                                                Parti2.ToString(), "Net_Payable", ((Balance + Interest) - TotalDebit).ToString(), "2", Activity, "TR_Close", SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                                txtPayBrCode.Text.ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "SBClose", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");
                                        }

                                        if (Result > 0)
                                        {
                                            Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode1, SubGlCode1, AccNo1,
                                                Parti1.ToString(), "Net_Payable", ((Balance + Interest) - TotalDebit).ToString(), "1", Activity, "TR_Close", SetNo.ToString(), ChqNo, ChqDate, "0", "0", "1001", "0",
                                                txtPayBrCode.Text.ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), "0", "SBClose", ViewState["CustNo"].ToString(), TxtAccName.Text.ToString(), "0", "0");
                                        }
                                    }
                                }

                                if (Result > 0)
                                {
                                    // Log Details Insert
                                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Assisttoclosure_" + SetNo.ToString() + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                                    //  For close respective account status, closingdate and cid
                                    AC.CloseAccount(Session["BRCD"].ToString(), TxtPrd.Text.ToString(), TxtAccNo.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                                    //  Bind recent posted voucher
                                    AC.GetEntryData(Grdentry, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), SetNo.ToString());
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
                else
                {
                    TxtservChg.Focus();
                    WebMsgBox.Show("Withdrawal Amount Not Eligibale  ...!!", this.Page);
                    return;
                }
            }
            else
            {
                TxtservChg.Focus();
                WebMsgBox.Show("Insufficient account balance ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtPrd.Text.ToString() == "")
            {
                TxtPrd.Focus();
                WebMsgBox.Show("Enter product code first...!!", this.Page);
                return;
            }
            else if (TxtAccNo.Text.ToString() == "")
            {
                TxtAccNo.Focus();
                WebMsgBox.Show("Enter account number first...!!", this.Page);
                return;
            }
            else
            {
                CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Assisttoclosure_" + TxtPrd.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                string redirectURL = "FrmRView.aspx?BC=" + Session["BRCD"].ToString() + "&PC=" + TxtPrd.Text.ToString() + "&FD=" + Session["EntryDate"].ToString() + "&TD=" + Session["EntryDate"].ToString() + "&rptname=RptSBIntCalculation.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

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

                    TxtservChg.Focus();
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
                    WebMsgBox.Show("Acc Number " + TxtAccNo.Text + " have Interest Suspended ...!!", this.Page);
                    return;
                }
                else
                {
                    TxtAccNo.Text = "";
                    TxtAccName.Text = "";
                    TxtAccNo.Focus();
                    WebMsgBox.Show("Account Number is Invalid ...!!", this.Page);
                    return;
                }
            }
            else
            {
                TxtAccNo.Text = "";
                TxtAccName.Text = "";
                TxtAccNo.Focus();
                WebMsgBox.Show("Sorry account is not present ...!!", this.Page);
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
            //TxtCCheque.Text = TxtCCheque.Text.ToString() == "" ? "0" : TxtCCheque.Text.ToString();
            //  TxtCunused.Text = TxtCunused.Text.ToString() == "" ? "0" : TxtCunused.Text.ToString();
            // TxtUnusedChrg.Text = Math.Round(Convert.ToDouble(TxtCCheque.Text.ToString()) * Convert.ToDouble(TxtCunused.Text.ToString())).ToString();

            TxtservChg.Text = TxtservChg.Text.ToString() == "" ? "0" : TxtservChg.Text.ToString();
            TxterlyClosure.Text = TxterlyClosure.Text.ToString() == "" ? "0" : TxterlyClosure.Text.ToString();
            TxtGSTState.Text = TxtGSTState.Text.ToString() == "" ? "0" : TxtGSTState.Text.ToString();
            TxtGstCentral.Text = TxtGstCentral.Text.ToString() == "" ? "0" : TxtGstCentral.Text.ToString();
            TxttotalGSt.Text = TxttotalGSt.Text.ToString() == "" ? "0" : TxttotalGSt.Text.ToString();

            //  double Unused = Math.Round(Convert.ToDouble(TxtUnusedChrg.Text.ToString()), 2);
            double Service = Math.Round(Convert.ToDouble(TxtservChg.Text.ToString()), 2);
            double Early = Math.Round(Convert.ToDouble(TxterlyClosure.Text.ToString()), 2);

            if (cmn.GetUniversalPara("SBGSTCAL") == "Y")
            {
                //  Double Amount = Math.Round(Math.Round((Unused + Service) + Early), 2);
                //  TxtGSTState.Text = Math.Round(Math.Round(Convert.ToDouble((Amount * 9) / 100)), 2).ToString();
                //  TxtGstCentral.Text = Math.Round(Math.Round(Convert.ToDouble((Amount * 9) / 100)), 2).ToString();
                TxttotalGSt.Text = Math.Round((Convert.ToDouble(TxtGSTState.Text.ToString()) + Convert.ToDouble(TxtGstCentral.Text.ToString())), 2).ToString();
            }
            else
            {
                TxtGSTState.Text = "0";
                TxtGstCentral.Text = "0";
                TxttotalGSt.Text = "0";
            }

            double TotalGst = Math.Round(Convert.ToDouble(TxttotalGSt.Text.ToString()), 2);
            float ClosingBal = (float)Math.Round(Convert.ToDouble(TxtCBal.Text.ToString() == "" ? "0" : TxtCBal.Text.ToString()), 2);
            float Interest = (float)Math.Round(Convert.ToDouble(TxtIntAppld.Text.ToString() == "" ? "0" : TxtIntAppld.Text.ToString()), 2);
            Txtpayable.Text = Math.Round(Convert.ToDouble(ClosingBal + Interest), 2).ToString();

            Txtdeductn.Text = Math.Round(Convert.ToDouble(Service + Early + TotalGst), 2).ToString();
            TxtNetBal.Text = Math.Round(Convert.ToDouble((ClosingBal + Interest) - (Service + Early + TotalGst)), 2).ToString();

            double TotDeduction = Math.Round(Convert.ToDouble(Txtdeductn.Text.ToString() == "" ? "0" : Txtdeductn.Text.ToString()), 2);
            double NetBalance = Math.Round(Convert.ToDouble(TxtNetBal.Text.ToString() == "" ? "0" : TxtNetBal.Text.ToString()), 2);

            // TxtCCheque.Text = TxtCCheque.Text.ToString() == "" ? "0" : TxtCCheque.Text.ToString();
            TxtservChg.Text = TxtservChg.Text.ToString() == "" ? "0" : TxtservChg.Text.ToString();
            TxterlyClosure.Text = TxterlyClosure.Text.ToString() == "" ? "0" : TxterlyClosure.Text.ToString();
            TxtGSTState.Text = TxtGSTState.Text.ToString() == "" ? "0" : TxtGSTState.Text.ToString();
            TxtGstCentral.Text = TxtGstCentral.Text.ToString() == "" ? "0" : TxtGstCentral.Text.ToString();
            TxttotalGSt.Text = TxttotalGSt.Text.ToString() == "" ? "0" : TxttotalGSt.Text.ToString();
            Txtdeductn.Text = Txtdeductn.Text.ToString() == "" ? "0" : Txtdeductn.Text.ToString();
            TxtNetBal.Text = Math.Round(Convert.ToDouble(TxtNetBal.Text.ToString() == "" ? "0" : TxtNetBal.Text.ToString()), 2).ToString();

            if (Math.Round(Convert.ToDouble(TotDeduction + NetBalance), 2) > Math.Round(Convert.ToDouble(ClosingBal + Interest), 2))
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
        TxtservChg.Text = "";
        TxterlyClosure.Text = "";
        TxttotalGSt.Text = "";
        TxtGSTState.Text = "";
        TxtGstCentral.Text = "";
        TxtNetBal.Text = "";
        Txtdeductn.Text = "";
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

    protected void TxtGstCentral_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Calculation();
            TxttotalGSt.Text = Math.Round((Convert.ToDouble(TxtGSTState.Text.ToString()) + Convert.ToDouble(TxtGstCentral.Text.ToString())), 2).ToString();
            ddlPayType.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtGSTState_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Calculation();
            TxttotalGSt.Text = Math.Round((Convert.ToDouble(TxtGSTState.Text.ToString()) + Convert.ToDouble(TxtGstCentral.Text.ToString())), 2).ToString();
            TxtGstCentral.Focus();
            //TxttotalGSt.Text = Convert.ToSingle((Convert.ToSingle(TxtGSTState.Text) + Convert.ToSingle(TxtGstCentral.Text))).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

}
