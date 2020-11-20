using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Reflection;
using System.Drawing;

public partial class frmTDAClear : System.Web.UI.Page
{
    DataTable DS = new DataTable();
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    ClsCommon CMN = new ClsCommon();
    scustom customcs = new scustom();
    ClsStatementView SV = new ClsStatementView();
    ClsNewTDA CurrentCls = new ClsNewTDA();
    ClsTDAClear CurrentCls1 = new ClsTDAClear();
    DbConnection conn = new DbConnection();
    ClsOpenClose OC = new ClsOpenClose();
    ClsAuthorized POSTV = new ClsAuthorized();
    ClsInsertTrans ITrans = new ClsInsertTrans();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLoanInfo LI = new ClsLoanInfo();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsLoanClosure LC = new ClsLoanClosure();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsMISTransfer MT = new ClsMISTransfer();
    ClsDDCR DDS = new ClsDDCR();
    ClsCashReciept CR = new ClsCashReciept();
    ClsCashPayment CP = new ClsCashPayment();
    ClsFDARenew FDR = new ClsFDARenew();
    ClsMultiVoucher MV = new ClsMultiVoucher();
    ClsAVS5074 CLS = new ClsAVS5074();
    int result = 0, resultintrst = 0, resultout = 0;
    double TotalAmt = 0, TotalDrAmt = 0, TxtAmount = 0;
    string Res = "", FL = "";
    string REFERENCEID = "", CN = "", CD = "";
    string sResult = "", AC_Status = "";
    int resultint = 0;

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //  int totalParameters = Request.QueryString.AllKeys.Length;
                ////Added by ankita on 23/09/2017
                //if(totalParameters != 0)
                //{
                if (Request.QueryString["SUBGLCODE"] != null && !string.IsNullOrEmpty(Request.QueryString["SUBGLCODE"].ToString()))
                {
                    TxtProcode.Text = Request.QueryString["SUBGLCODE"].ToString();
                    productcd();
                    if (Request.QueryString["ACCNO"] != null && !string.IsNullOrEmpty(Request.QueryString["ACCNO"].ToString()))
                    {
                        txtAccNo.Text = Request.QueryString["ACCNO"].ToString();
                        accno();
                    }
                }
                // }
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }


                //Added By Amol on 03/07/2017 for dds to loan
                ITrans.DelAllRecTable(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                BD.BindIntrstPayout(ddlIntrestPay);
                ViewState["MATURITY"] = "MA";
                autoglname.ContextKey = Session["BRCD"].ToString();
                if (RdbClose.Checked == true)
                    BD.BindPayment(ddlPayType, "3");//BD.BindPayment(ddlPayType, "3");
                else
                    BD.BindPayment(ddlPayType, "2");
                TxtProcode.Focus();
                REFERENCEID = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                ViewState["RID"] = (Convert.ToInt32(REFERENCEID) + 1).ToString();
                autoglname1.ContextKey = Session["BRCD"].ToString();

                //Added By Amol
                ViewState["LoanAmt"] = "0";
                ViewState["TotLoanBal"] = "0";
                // Invisi(false);

                EnableDisableInt();
                BindGrid1();
                string BKCD = CMN.GetBANKCode();
                if (BKCD != null)
                {
                    ViewState["BKCD"] = BKCD.ToString();
                }
                else
                {
                    ViewState["BKCD"] = "0";
                }


            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
    }

    #endregion

    #region Text changed event
    protected void TxtTNarration_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPayType.SelectedValue == "2")
            {
                BtnSubmit.Focus();
            }
            else
            {
                TxtChequeNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Txt_ExistSetno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string Op = CurrentCls1.FnMultipleClose_Opr("CHECKSETNO", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Txt_ExistSetno.Text);
            if (Op == "1")
            {
                string AmtTally = CurrentCls1.FnMultipleClose_Opr("CHECKSETALLY", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Txt_ExistSetno.Text);
                if (AmtTally != "0")
                {
                    string SumAmount = CurrentCls1.FnMultipleClose_Opr("GETSUM", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Txt_ExistSetno.Text);
                    if (SumAmount != null)
                    {
                        ViewState["SumAmount"] = SumAmount.ToString();
                    }
                    else
                    {
                        WebMsgBox.Show("Invalid SumAmount....!", this.Page);
                        Txt_ExistSetno.Text = "";
                        return;
                    }

                    string SumAmountInt = CurrentCls1.FnMultipleClose_Opr("GETSUMINT", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Txt_ExistSetno.Text);
                    if (SumAmountInt != null)
                    {
                        ViewState["SumAmountInt"] = SumAmountInt.ToString();
                    }
                    else
                    {
                        WebMsgBox.Show("Invalid SumAmount....!", this.Page);
                        Txt_ExistSetno.Text = "";
                        return;
                    }

                    Txt_ExistSetno.Enabled = false;
                    DdlCrDr.Focus();
                }
                else
                {
                    WebMsgBox.Show("Set No " + Txt_ExistSetno.Text + " is already Credited....!", this.Page);
                    Txt_ExistSetno.Text = "";
                    Txt_ExistSetno.Focus();
                    return;
                }
            }
            else if (Op == "2")
            {
                WebMsgBox.Show("Set No " + Txt_ExistSetno.Text + " is already authorized....!", this.Page);
                Txt_ExistSetno.Text = "";
                Txt_ExistSetno.Focus();
                return;
            }
            else
            {
                WebMsgBox.Show("Set No " + Txt_ExistSetno.Text + " not find for Multiple TDA Closure....!", this.Page);
                Txt_ExistSetno.Text = "";
                Txt_ExistSetno.Focus();
                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void RdbMultipleClose_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            BD.BindMTDAClose(ddlPayType);

            RdbClose.Checked = true;

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Rdb_ExistingSetno_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            Div_ExSetno.Visible = true;
            BtnPostInt.Enabled = true;
            EnableCompo(false);
            Txt_ExistSetno.Focus();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Rdb_NewSetno_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            Div_ExSetno.Visible = false;
            RdbMultipleClose.Checked = false;
            RdbClose.Checked = true;
            Txt_ExistSetno.Text = "";
            Txt_ExistSetno.Enabled = true;
            EnableCompo(true);
            RdbMultipleClose.Enabled = true;
            TxtProcode.Focus();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DdlCrDr_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DdlCrDr.SelectedValue == "2")
            {
                TxtProcode.Focus();
                txtPayAmnt.Text = "";
                Div_SSubmit.Visible = true;
                Div_MSubmit.Visible = false;
            }
            else
            {
                txtPayAmnt.Text = ViewState["SumAmount"].ToString();
                BD.BindMTDAClose(ddlPayType);
                RdbMultipleClose.Checked = true;
                Div_SSubmit.Visible = false;
                Div_MSubmit.Visible = true;
                ddlPayType.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    // Product name
    protected void TxtProcode_TextChanged(object sender, EventArgs e)
    {
        productcd();
    }

    // Get Customer name from Account no and product code
    protected void txtAccNo_TextChanged(object sender, EventArgs e)
    {
        accno();
    }

    // Deposit amount changed
    protected void TxtDepoAmt_TextChanged(object sender, EventArgs e)
    {
        TxtPeriod.Text = "";
        TxtRate.Text = "";
        TxtIntrest.Text = "";
        TxtMaturity.Text = "";
        DtDueDate.Text = "";
        ddlduration.Focus();
    }

    protected void TxtPeriod_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtPeriod.Text == "")
            {
                return;
            }

            if (TxtDepoAmt.Text == "")
            {
                WebMsgBox.Show("Please enter amount", this.Page);
                TxtDepoAmt.Focus();
                return;
            }
            // Check Duration
            string IsPvalid = "0";
            IsPvalid = CurrentCls.CheckPeriod(TxtProcode.Text.ToString(), TxtPeriod.Text.ToString(), ddlduration.SelectedValue.ToString(), Session["BRCD"].ToString(), "");
            if (Convert.ToInt32(IsPvalid) > 0) { }
            else
            {
                WebMsgBox.Show("Invalid Period...", this.Page);
                TxtPeriod.Text = "";
                TxtRate.Text = "";
                TxtIntrest.Text = "";
                TxtMaturity.Text = "";
                DtDueDate.Text = "";
                TxtPeriod.Focus();
                return;
            }

            // Get rates for Product Code
            float rate = CurrentCls.GetIntrestRate(TxtProcode.Text.ToString(), TxtPeriod.Text.ToString(), Session["BRCD"].ToString(), ddlduration.SelectedValue.ToString(), rdbPreMature.Checked);
            if (rate == 0)
            {
                WebMsgBox.Show(" Invalid Value... ", this.Page);
                TxtPeriod.Text = "";
                TxtPeriod.Focus();
                return;
            }
            else
            {
                TxtRate.Text = rate.ToString();
            }
            // Calculate Due date
            CalDueDate(Convert.ToDateTime(dtDeposDate.Text.ToString()), ddlduration.SelectedItem.Text.ToString(), Convert.ToInt32(TxtPeriod.Text));

            // Calculate Interest 
            float amt = (float)Convert.ToDouble(TxtDepoAmt.Text);
            float intrate = (float)Convert.ToDouble(TxtRate.Text);
            CalculatedepositINT(amt, TxtProcode.Text.ToString(), intrate, Convert.ToInt32(TxtPeriod.Text), ddlIntrestPay.SelectedItem.Text.ToString(), ddlduration.SelectedItem.Text.ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtCROI_TextChanged(object sender, EventArgs e)
    {
        try
        {
            AddCommision();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtLoanTotAmt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //if (Convert.ToDouble(txtLoanTotAmt.Text.Trim().ToString()) < Convert.ToDouble(ViewState["LoanTotAmt"].ToString()))
            //{
            //    txtLoanTotAmt.Text = ViewState["LoanTotAmt"].ToString();
            //    lblMessage.Text = "Amount of receipt is less than paid amount " + Convert.ToDouble(ViewState["LoanTotAmt"].ToString()).ToString() + "...!!";
            //    ModalPopup.Show(this.Page);
            //    return;
            //}
            if (Convert.ToDouble(txtLoanTotAmt.Text.Trim().ToString()) > Convert.ToDouble(ViewState["LoanTotAmt"].ToString()))
            {
                txtLoanTotAmt.Text = ViewState["LoanTotAmt"].ToString();
                lblMessage.Text = "Excess amount of receipt than paid amount " + Convert.ToDouble(ViewState["LoanTotAmt"].ToString()).ToString() + "...!!";
                ModalPopup.Show(this.Page);
                return;
            }
            else if (Convert.ToDouble(txtLoanTotAmt.Text.Trim().ToString()) > Convert.ToDouble(txtPayAmnt.Text.Trim().ToString() == "" ? "0" : txtPayAmnt.Text.Trim().ToString()))
            {
                txtLoanTotAmt.Text = ViewState["LoanTotAmt"].ToString();
                lblMessage.Text = "Excess amount of receipt than total payable amount " + Convert.ToDouble(txtPayAmnt.Text.Trim().ToString() == "" ? "0" : txtPayAmnt.Text.Trim().ToString()).ToString() + "...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtINTPcode_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    int result = 0;
        //    int.TryParse(TxtINTPcode.Text, out result);
        //    TxtINTPName.Text = customcs.GetProductName(result.ToString(), TxtINTGL.Text);
        //    //string[] GLT = BD.GetAccTypeGL(TxtINTPcode.Text, Session["BRCD"].ToString(), TxtINTGL.Text).Split('_');
        //    ViewState["IGL"] = TxtINTGL.Text;           
        //    if (TxtINTPName.Text == "")
        //    {
        //        WebMsgBox.Show("Invalid product code", this.Page);
        //        clear();
        //        TxtProcode.Focus();
        //        return;
        //    }            
        //    TxtINTAcc.Focus();
        //}
        //catch (Exception Ex)
        //{
        //    ExceptionLogging.SendErrorToText(Ex);
        //}
    }

    protected void TxtINTAcc_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";
            // AT = BD.GetStage1(TxtAccno.Text, Session["BRCD"].ToString(), ViewState["Flag"].ToString());
            AT = BD.Getstage3(TxtINTAcc.Text, Session["BRCD"].ToString(), "23", "1");
            if (AT != "1003")
            {
                lblMessage.Text = "Sorry Customer not Authorise.........!!";
                ModalPopup.Show(this.Page);
                clear();
            }
            else
            {
                string PRD = "";
                PRD = "23";
                TxtIntAccName.Text = customcs.GetAccountName(TxtINTAcc.Text.ToString(), PRD, Session["BRCD"].ToString());
                if (TxtIntAccName.Text == "" & TxtINTAcc.Text != "")
                {
                    WebMsgBox.Show("Please enter valid Account number", this.Page);
                    TxtINTAcc.Text = "";
                    TxtINTAcc.Focus();
                    return;
                }
                if (TxtIntAccName.Text != "")
                {
                    //txtnaration1.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    // Select Payment type

    public int CheckPayMode()
    {
        try
        {
            int Cn = 0;
            string ACT = CurrentCls1.FnMultipleClose_Opr("GETPAYMODE", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Txt_ExistSetno.Text);
            if (ACT != null)
            {
                string[] ACT1 = ACT.Split('_');
                if (ACT1[0].ToString() == "CP" && ACT1[1].ToString() == "4")
                {
                    if (ddlPayType.SelectedValue != "1")
                    {
                        WebMsgBox.Show("Invalid Paymode Selected for Setno " + Txt_ExistSetno.Text + ", Previous Debit was in Payment Mode.....!", this.Page);
                        Cn = 0;
                        ddlPayType.SelectedValue = "0";
                    }
                    else
                        Cn = 1;

                }
                else if (ACT1[0].ToString() == "TR" && ACT1[1].ToString() == "7")
                {
                    if (ddlPayType.SelectedValue != "2")
                    {
                        WebMsgBox.Show("Invalid Paymode Selected for Setno " + Txt_ExistSetno.Text + ", Previous Debit was in Transfer Mode.....!", this.Page);
                        Cn = 0;
                        ddlPayType.SelectedValue = "0";
                    }
                    else
                        Cn = 1;
                }
                else if (ACT1[0].ToString() == "TR" && ACT1[1].ToString() == "5")
                {
                    if (ddlPayType.SelectedValue != "3")
                    {
                        WebMsgBox.Show("Invalid Paymode Selected for Setno " + Txt_ExistSetno.Text + ", Previous Debit was in Cheque Mode.....!", this.Page);
                        Cn = 0;
                        ddlPayType.SelectedValue = "0";
                    }
                    else
                        Cn = 1;
                }
                else if (ACT1[0].ToString() == "TRR" && ACT1[1].ToString() == "11")
                {
                    if (ddlPayType.SelectedValue != "4")
                    {
                        WebMsgBox.Show("Invalid Paymode Selected for Setno " + Txt_ExistSetno.Text + ", Previous Debit was in Renew Mode.....!", this.Page);
                        Cn = 0;
                        ddlPayType.SelectedValue = "0";
                    }
                    else
                        Cn = 1;
                }

                else
                {
                    WebMsgBox.Show("Invalid PayMode selected....!", this.Page);
                    Cn = 0;
                    ddlPayType.SelectedValue = "0";
                }


            }
            else
            {
                Cn = 0;
                ddlPayType.SelectedValue = "0";
            }

            return Cn;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            ddlPayType.SelectedValue = "0";
            return 0;
        }
    }
    protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtInterestNew.Text == "0" && TxtSbintrest.Text == "0")
            {
                int CheckCn = 0;
                if (Rdb_ExistingSetno.Checked == true && Txt_ExistSetno.Text != "" && DdlCrDr.SelectedValue == "2")
                {
                    CheckCn = CheckPayMode();
                    if (CheckCn == 0)
                    {
                        ddlPayType.Focus();
                        return;
                    }

                }

                else if (Rdb_ExistingSetno.Checked == true && Txt_ExistSetno.Text != "" && txtPayAmnt.Text != "" && RdbMultipleClose.Checked == true && DdlCrDr.SelectedValue == "1")
                {
                    CheckCn = CheckPayMode();
                    if (CheckCn == 0)
                    {
                        ddlPayType.Focus();
                        return;
                    }

                    if (ddlPayType.SelectedValue.ToString() == "0")
                    {
                        ABB.Visible = false;
                        Transfer.Visible = false;
                        Transfer2.Visible = false;
                        divLoan.Visible = false;
                        hdnValue.Value = "0"; //Added by Amruta as per Ambika madam for ABB-Transfer 06/03/2018
                        ClearPayInfo();
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "1")
                    {
                        if (TxtInterestNew.Text == "")
                        {
                            return;
                        }
                        ViewState["PAYTYPE"] = "CASH";
                        ABB.Visible = false;
                        Transfer.Visible = false;
                        Transfer2.Visible = false;
                        divLoan.Visible = false;
                        BtnMultipleSubmit.Focus();
                        hdnValue.Value = "0";
                        ClearPayInfo();
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "2")
                    {
                        if (TxtInterestNew.Text == "")
                        {
                            return;
                        }
                        ViewState["PAYTYPE"] = "TRANSFER";
                        ABB.Visible = false;
                        Transfer.Visible = true;
                        Transfer2.Visible = false;
                        divLoan.Visible = false;
                        hdnValue.Value = "0";
                        autoglname1.ContextKey = Session["BRCD"].ToString();
                        ClearPayInfo();
                        Txtprocode1.Focus();
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "3")
                    {
                        if (TxtInterestNew.Text == "")
                        {
                            return;
                        }
                        ViewState["PAYTYPE"] = "CHEQUE";
                        ABB.Visible = false;
                        Transfer.Visible = true;
                        Transfer2.Visible = true;
                        divLoan.Visible = false;
                        hdnValue.Value = "0";
                        autoglname1.ContextKey = Session["BRCD"].ToString();
                        ClearPayInfo();
                        Txtprocode1.Focus();
                    }
                    else if (ddlPayType.SelectedValue == "4")
                    {
                        BtnMultipleSubmit.Focus();
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "5" || ddlPayType.SelectedValue.ToString() == "6")
                    {
                        if (TxtInterestNew.Text == "")
                        {
                            return;
                        }
                        ViewState["PAYTYPE"] = "TRANSFER";
                        ABB.Visible = false;
                        Transfer.Visible = true;
                        Transfer2.Visible = false;
                        divLoan.Visible = true;
                        hdnValue.Value = "0";
                        autoglname1.ContextKey = Session["BRCD"].ToString();
                        ClearPayInfo();
                        Txtprocode1.Focus();
                    }
                    else if (ddlPayType.SelectedValue == "7")
                    {
                        if (TxtInterestNew.Text == "")
                        {
                            return;
                        }
                        ViewState["PAYTYPE"] = "ABB-TRANSFER";
                        ABB.Visible = true;
                        Transfer.Visible = true;
                        Transfer2.Visible = false;
                        divLoan.Visible = false;
                        hdnValue.Value = "1";
                        autoglname1.ContextKey = Session["BRCD"].ToString();
                        ClearPayInfo();

                        txtBrcd.Focus();
                    }
                    else
                    {
                        Transfer.Visible = false;
                        divLoan.Visible = false;
                        ClearPayInfo();
                    }
                    return;
                }





                double TotalPaid;
                if (Rdb_IntPay.Checked == true) // For MIS Interest Transfer Only
                {
                    if (Convert.ToInt32(TxtIntrestPaybl.Text) != 0)
                        TotalPaid = Convert.ToDouble(TxtMaturity.Text) - (Convert.ToDouble(TxtPrincPaybl.Text) + Convert.ToDouble(TxtIntrestPaybl.Text));
                    else
                        TotalPaid = 0;

                    BtnPostInt.Visible = false;

                    txtPayAmnt.Text = (Convert.ToInt32(TxtIntrestPaybl.Text)).ToString();
                }

                else // Other Cases of Deposit except MIS Interest Transfer Only
                {
                    if (ViewState["RD"].ToString() != "YES")
                    {
                        string[] arraydt = Session["EntryDate"].ToString().Split('/');

                        if (Convert.ToInt32(TxtInterestNew.Text) != 0)
                            TotalPaid = Convert.ToDouble(TxtMaturity.Text) - (Convert.ToDouble(TxtPrincPaybl.Text) + Convert.ToDouble(TxtIntrestPaybl.Text));
                        else
                            TotalPaid = 0;

                        if (rdbPreMature.Checked != true)
                        {
                            TxtInterestNew.Text = TotalPaid.ToString();
                        }
                        else
                        {
                            TxtInterestNew.Text = "0";
                        }

                        if (TotalPaid > 0)
                        {
                            BtnPostInt.Visible = true;
                        }
                        else
                        {
                            BtnPostInt.Visible = false;
                        }

                        txtPayAmnt.Text = ((Convert.ToDouble(TxtPrincPaybl.Text.Trim().ToString() == "" ? "0" : TxtPrincPaybl.Text.Trim().ToString()) + Convert.ToDouble(TxtIntrestPaybl.Text.Trim().ToString() == "" ? "0" : TxtIntrestPaybl.Text.Trim().ToString())) - (Convert.ToDouble(TxtAdminCharges.Text.Trim().ToString() == "" ? "0" : TxtAdminCharges.Text.Trim().ToString()) + Convert.ToDouble((ViewState["GLCODE"].ToString() == "5" ? "0" : TxtCommission.Text.Trim().ToString() == "" ? "0" : TxtCommission.Text.Trim().ToString())))).ToString();//Convert.ToDouble((ViewState["GLCODE"].ToString() == "5" ? "0" : TxtCommission.Text))
                    }
                    else
                    {
                        dt = new DataTable();
                        dt = CMN.CalRDintrest(Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text, Session["EntryDate"].ToString(), "C", "0");
                        if (dt.Rows[0]["Rate"].ToString() != "" && dt.Rows[0]["Rate"].ToString() != null)
                        {
                            //Added By Amol on 2016-12-12
                            Res = CurrentCls1.GetLastIntDate(Session["BRCD"].ToString(), Txtcustno.Text.Trim().ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                            if (Res != Session["Entrydate"].ToString())
                            {
                                double CH = Convert.ToDouble(dt.Rows[0]["Rate"].ToString());
                                if (CH != 0 && CH != null)
                                {
                                    double PP = CMN.GetPPandINT("CLOSING", TxtProcode.Text, txtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString());
                                    double INTP = CMN.GetPPandINT("CLOSING", ViewState["IR"].ToString(), txtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "10");
                                    if (dt.Rows.Count > 0)
                                    {

                                        TxtPrincPaybl.Text = PP.ToString();
                                        TxtIntrestPaybl.Text = INTP.ToString();
                                        //if (ViewState["GLCODE"].ToString() == "15") //For Balvikas Deposit --Abhishek
                                        //{
                                        TxtInterestNew.Text = Math.Round(Convert.ToDouble(dt.Rows[0]["Interest"]), 0).ToString();// - Convert.ToDouble(dt.Rows[0]["InterestPaid"])), 0).ToString();
                                        //}
                                        //else
                                        //{
                                        //    TxtInterestNew.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"]) - Convert.ToDouble(dt.Rows[0]["InterestPaid"])), 0).ToString();
                                        //}
                                        // TxtSbintrest.Text = dt.Rows[0]["IntRate"].ToString();
                                    }
                                }
                                else
                                {
                                    lblMessage.Text = "";
                                    string P = dt.Rows[0]["Period"].ToString() == null ? "0" : dt.Rows[0]["Period"].ToString();
                                    string PT = dt.Rows[0]["Periodtype"].ToString() == null ? "0" : dt.Rows[0]["Periodtype"].ToString();
                                    lblMessage.Text = "Rate for GLCODE = " + ViewState["GLCODE"].ToString() + " is not set for Period = " + P + " and Period type ='" + PT + "'";
                                    ModalPopup.Show(this.Page);
                                    //  return;
                                }
                            }
                            TotalPaid = Convert.ToDouble(Convert.ToDouble(TxtInterestNew.Text.Trim().ToString() == "" ? "0" : TxtInterestNew.Text.Trim().ToString()) + Convert.ToDouble(TxtIntrestPaybl.Text.Trim().ToString() == "" ? "0" : TxtIntrestPaybl.Text.Trim().ToString()));

                            if (TotalPaid > 0)
                            {
                                BtnPostInt.Visible = true;
                            }
                            else
                            {
                                BtnPostInt.Visible = false;
                            }

                            txtPayAmnt.Text = ((Convert.ToDouble(TxtPrincPaybl.Text.Trim().ToString() == "" ? "0" : TxtPrincPaybl.Text.Trim().ToString()) + Convert.ToDouble(TxtIntrestPaybl.Text.Trim().ToString() == "" ? "0" : TxtIntrestPaybl.Text.Trim().ToString())) - (Convert.ToDouble(TxtAdminCharges.Text.Trim().ToString() == "" ? "0" : TxtAdminCharges.Text.Trim().ToString()) + Convert.ToDouble((ViewState["GLCODE"].ToString() == "5" ? "0" : TxtCommission.Text.Trim().ToString() == "" ? "0" : TxtCommission.Text.Trim().ToString())))).ToString();

                            //Rd Calculation Grid Bind
                            BindRDGrid(Grid_RDCal, Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text, Session["EntryDate"].ToString());
                        }
                        else
                        {
                            WebMsgBox.Show("Deposit details having invalid Period....!", this.Page);
                            return;
                        }
                    }

                    //End Added by Amol
                }

                if (RdbMultipleClose.Checked != true)
                {
                    if (ddlPayType.SelectedValue.ToString() == "0")
                    {
                        hdnValue.Value = "0";
                        Transfer.Visible = false;
                        ABB.Visible = false;
                        Transfer2.Visible = false;
                        divLoan.Visible = false;
                        ClearPayInfo();
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "1")
                    {
                        if (TxtInterestNew.Text == "")
                        {
                            return;
                        }
                        ViewState["PAYTYPE"] = "CASH";
                        ABB.Visible = false;
                        hdnValue.Value = "0";
                        Transfer.Visible = false;
                        Transfer2.Visible = false;
                        divLoan.Visible = false;
                        ClearPayInfo();
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "2")
                    {
                        if (TxtInterestNew.Text == "")
                        {
                            return;
                        }
                        ViewState["PAYTYPE"] = "TRANSFER";
                        ABB.Visible = false;
                        hdnValue.Value = "0";
                        Transfer.Visible = true;
                        Transfer2.Visible = false;
                        divLoan.Visible = false;
                        autoglname1.ContextKey = Session["BRCD"].ToString();
                        ClearPayInfo();
                        Txtprocode1.Focus();
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "4")
                    {
                        if (TxtInterestNew.Text == "")
                        {
                            return;
                        }
                        ViewState["PAYTYPE"] = "CHEQUE";
                        ABB.Visible = false;
                        Transfer.Visible = true;
                        hdnValue.Value = "0";
                        Transfer2.Visible = true;
                        divLoan.Visible = false;
                        autoglname1.ContextKey = Session["BRCD"].ToString();
                        ClearPayInfo();
                        Txtprocode1.Focus();
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "5" || ddlPayType.SelectedValue.ToString() == "6")
                    {
                        if (TxtInterestNew.Text == "")
                        {
                            return;
                        }
                        ViewState["PAYTYPE"] = "TRANSFER";
                        ABB.Visible = false;
                        hdnValue.Value = "0";
                        Transfer.Visible = true;
                        Transfer2.Visible = false;
                        divLoan.Visible = true;
                        autoglname1.ContextKey = Session["BRCD"].ToString();
                        ClearPayInfo();
                        Txtprocode1.Focus();
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "7")
                    {
                        if (TxtInterestNew.Text == "")
                        {
                            return;
                        }
                        ViewState["PAYTYPE"] = "ABB-TRANSFER";
                        ABB.Visible = true;
                        hdnValue.Value = "1";
                        Transfer.Visible = true;
                        Transfer2.Visible = false;
                        divLoan.Visible = false;
                        autoglname1.ContextKey = Session["BRCD"].ToString();
                        ClearPayInfo();
                        txtBrcd.Focus();
                    }
                    else
                    {
                        Transfer.Visible = false;
                        divLoan.Visible = false;
                        ClearPayInfo();
                    }
                    txtPayAmnt.Text = Convert.ToDouble(Convert.ToDouble(txtPayAmnt.Text.Trim().ToString() == "" ? "0" : txtPayAmnt.Text.Trim().ToString()) - Convert.ToDouble(ViewState["LoanAmt"].ToString() == "" ? "0" : ViewState["LoanAmt"].ToString())).ToString();
                }

                //else
                //{
                //    double SumAmt=0;
                //    if (Rdb_ExistingSetno.Checked == true)
                //    {
                //        SumAmt=Convert.ToDouble(ViewState["SumAmount"].ToString());
                //    }
                //    txtPayAmnt.Text = Convert.ToDouble(SumAmt + Convert.ToDouble(txtPayAmnt.Text.Trim().ToString() == "" ? "0" : txtPayAmnt.Text.Trim().ToString()) - Convert.ToDouble(ViewState["LoanAmt"].ToString() == "" ? "0" : ViewState["LoanAmt"].ToString())).ToString();
                //}
                //added by amol for DDS to loan on 23/06/2017


                //Added By AmolB ON 2017-01-16 Bcoz interest not post twice as per valmik sir instruction
                Res = CurrentCls1.GetLastIntDate(Session["BRCD"].ToString(), Txtcustno.Text.Trim().ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                if (Res == Session["Entrydate"].ToString() || Convert.ToInt32(TxtInterestNew.Text) == 0)
                {
                    BtnPostInt.Visible = false;
                }
                else
                {
                    BtnPostInt.Visible = true;
                }

                autoglname1.ContextKey = Session["BRCD"].ToString();
            }
            else
            {
                WebMsgBox.Show("Interest not posted , First post the interest...!", this.Page);
                BtnPostInt.Focus();
                ddlPayType.SelectedValue = "0";
            }



        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Txtprocode1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (hdnValue.Value == "1" && txtBrcd.Text == string.Empty)     //added by amruta as per ambika madam 06/03/2018 for ABB-transfer
            {                                                              //added by amruta as per ambika madam 06/03/2018 for ABB-transfer
                WebMsgBox.Show("Enter BankCode first", this.Page);         //added by amruta as per ambika madam 06/03/2018 for ABB-transfer
                Txtprocode1.Text = "";                                     //added by amruta as per ambika madam 06/03/2018 for ABB-transfer
            }                                                              //added by amruta as per ambika madam 06/03/2018 for ABB-transfer
            else if (hdnValue.Value == "1" && Txtprocode1.Text != "1")     //added by amruta as per ambika madam 06/03/2018 for ABB-transfer
            {                                                              //added by amruta as per ambika madam 06/03/2018 for ABB-transfer
                WebMsgBox.Show("Invalid Product Code", this.Page);         //added by amruta as per ambika madam 06/03/2018 for ABB-transfer
                Txtprocode1.Text = "";                                     //added by amruta as per ambika madam 06/03/2018 for ABB-transfer
            }                                                              //added by amruta as per ambika madam 06/03/2018 for ABB-transfer
            else
            {
                string AC1;
                AC1 = CurrentCls1.Getaccno(Txtprocode1.Text, Session["BRCD"].ToString(), "");

                if (AC1 != null)
                {
                    string[] AC = AC1.Split('_');
                    ViewState["GlCode1"] = AC[0].ToString();
                    Txtglcode.Text = AC[1].ToString();

                    if (Convert.ToInt32(ViewState["GlCode1"].ToString() == "" ? "0" : ViewState["GlCode1"].ToString()) > 100)
                    {
                        TxtAccNo1.Text = "";
                        TxtCustName1.Text = "";

                        TxtAccNo1.Text = Txtprocode1.Text.ToString();
                        TxtCustName1.Text = Txtglcode.Text.ToString();
                        TxtTNarration.Focus();
                    }
                    else
                    {
                        if (hdnValue.Value == "1")
                            TrfAccName.ContextKey = txtBrcd.Text + "_" + Txtprocode1.Text + "_" + ViewState["GlCode1"].ToString();
                        else
                            TrfAccName.ContextKey = Session["BRCD"].ToString() + "_" + Txtprocode1.Text + "_" + ViewState["GlCode1"].ToString();
                        TxtAccNo1.Text = "";
                        TxtCustName1.Text = "";
                        TxtAccNo1.Focus();
                    }
                }
                else
                {
                    WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                    Txtprocode1.Text = "";
                    Txtglcode.Text = "";
                    TxtAccNo1.Text = "";
                    TxtCustName1.Text = "";
                    Txtprocode1.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Txtglcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = Txtglcode.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                Txtglcode.Text = custnob[0].ToString();
                Txtprocode1.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] AC = CurrentCls1.Getaccno(Txtprocode1.Text, Session["BRCD"].ToString(), custnob[2].ToString()).Split('_');
                ViewState["GlCode1"] = AC[0].ToString();

                if (Convert.ToInt32(ViewState["GlCode1"].ToString() == "" ? "0" : ViewState["GlCode1"].ToString()) > 100)
                {
                    TxtAccNo1.Text = "";
                    TxtCustName1.Text = "";

                    TxtAccNo1.Text = Txtprocode1.Text.ToString();
                    TxtCustName1.Text = Txtglcode.Text.ToString();
                    TxtChequeNo.Focus();
                }
                else
                {
                    TrfAccName.ContextKey = Session["BRCD"].ToString() + "_" + Txtprocode1.Text + "_" + ViewState["GlCode1"].ToString();
                    TxtAccNo1.Text = "";
                    TxtCustName1.Text = "";
                    TxtAccNo1.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAccNo1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";
            AC_Status = CMN.GetAccStatus(Session["BRCD"].ToString(), Txtprocode1.Text, TxtAccNo1.Text);
            if (AC_Status == "1" || AC_Status == "4")
            {

                dt = CMN.GetCustNoNameGL1(Txtprocode1.Text.ToString(), TxtAccNo1.Text.ToString(), Session["BRCD"].ToString());
                if (dt.Rows.Count > 0)
                {
                    ViewState["CustName"] = dt.Rows[0]["CUSTNO"].ToString();
                    TxtCustName1.Text = dt.Rows[0]["CUSTNAME"].ToString();
                    if (TxtProcode.Text != "" && txtAccNo.Text != "")
                    {
                        DataTable dtmodal = new DataTable();
                        dtmodal = CR.GetInfoTbl(Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), txtAccNo.Text, TxtProcode.Text);
                        if (dtmodal.Rows.Count > 0)
                        {
                            resultout = CR.GetInfo(GrdView, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), txtAccNo.Text, TxtProcode.Text);
                            if (resultout > 0)
                            {
                                string Modal_Flag = "VOUCHERVIEW";
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$('#" + Modal_Flag + "').modal('show');");
                                sb.Append(@"</script>");

                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                            }
                        }
                    }

                    if (ddlPayType.SelectedValue == "5" || ddlPayType.SelectedValue == "6")
                    {
                        dt = new DataTable();

                        if (ddlPayType.SelectedValue == "5")
                        {
                            dt = CurrentCls1.GetLoanTotalAmount(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Session["EntryDate"].ToString(), "LoanInst");
                        }
                        if (ddlPayType.SelectedValue == "6")
                        {
                            dt = CurrentCls1.GetLoanTotalAmount(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Session["EntryDate"].ToString(), "LoanClose");
                        }

                        if (dt.Rows.Count > 0)
                        {
                            TotalAmt = Math.Round(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) + Convert.ToDouble(dt.Rows[0]["Interest"].ToString() == "" ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["PInterest"].ToString() == "" ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString() == "" ? "0" : dt.Rows[0]["InterestRec"].ToString()) + Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString())));

                            //if (Convert.ToDouble(txtPayAmnt.Text.Trim().ToString()) < Convert.ToDouble(TotalAmt))
                            //{
                            //    txtLoanTotAmt.Text = "0";
                            //    ViewState["LoanTotAmt"] = TotalAmt.ToString();
                            //    lblMessage.Text = "Amount of receipt is less than paid amount " + Convert.ToDouble(TotalAmt).ToString() + "...!!";
                            //    ModalPopup.Show(this.Page);
                            //    return;
                            //}
                            //else
                            //{
                            txtLoanTotAmt.Text = TotalAmt.ToString();
                            ViewState["LoanTotAmt"] = TotalAmt.ToString();
                            return;
                            //}
                        }
                        else
                        {
                            txtLoanTotAmt.Text = "0";
                            ViewState["LoanTotAmt"] = Convert.ToDouble(txtLoanTotAmt.Text.Trim().ToString());
                        }
                    }
                    TxtTNarration.Focus();
                }
                else
                {
                    TxtAccNo1.Text = "";
                    TxtCustName1.Text = "";
                    TxtAccNo1.Focus();
                    WebMsgBox.Show("Account numer not found...", this.Page);
                }
            }
            else if (AC_Status == "2")
            {

                lblMessage.Text = "Acc number " + TxtAccNo1.Text + " is In-operative...!!";
                ModalPopup.Show(this.Page);
                TxtAccNo1.Text = "";

            }
            else if (AC_Status == "3")
            {

                lblMessage.Text = "Acc number " + TxtAccNo1.Text + " is Closed...!!";
                ModalPopup.Show(this.Page);
                TxtAccNo1.Text = "";
                TxtAccNo1.Focus();
            }
            else if (AC_Status == "5")
            {

                lblMessage.Text = "Acc number " + TxtAccNo1.Text + " is Credit Freezed...!!";
                ModalPopup.Show(this.Page);
                TxtAccNo1.Text = "";
                TxtAccNo1.Focus();
            }
            else if (AC_Status == "6")
            {

                lblMessage.Text = "Acc number " + TxtAccNo1.Text + " is Debit Freezed...!!";
                ModalPopup.Show(this.Page);
                TxtAccNo1.Text = "";
                TxtAccNo1.Focus();
            }
            else if (AC_Status == "7")
            {

                lblMessage.Text = "Acc number " + TxtAccNo1.Text + " is Total Freezed...!!";
                ModalPopup.Show(this.Page);
                TxtAccNo1.Text = "";
                TxtAccNo1.Focus();
            }
            else
            {

                WebMsgBox.Show("Invalid Account Number...!!", this.Page);
                TxtAccNo1.Text = "";
                TxtAccNo1.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtCustName1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC = TxtCustName1.Text;
            string[] ACT = AC.Split('_');
            if (ACT.Length > 1)
            {
                TxtCustName1.Text = ACT[0].ToString();
                TxtAccNo1.Text = ACT[1].ToString();
                ViewState["CustName"] = ACT[2].ToString();
                CheckData();

                if (ddlPayType.SelectedValue == "5" || ddlPayType.SelectedValue == "6")
                {
                    dt = new DataTable();
                    if (ddlPayType.SelectedValue == "5")
                    {
                        dt = CurrentCls1.GetLoanTotalAmount(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Session["EntryDate"].ToString(), "LoanInst");
                    }
                    if (ddlPayType.SelectedValue == "6")
                    {
                        dt = CurrentCls1.GetLoanTotalAmount(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Session["EntryDate"].ToString(), "LoanClose");
                    }

                    if (dt.Rows.Count > 0)
                    {
                        TotalAmt = Math.Round(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) + Convert.ToDouble(dt.Rows[0]["Interest"].ToString() == "" ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["PInterest"].ToString() == "" ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString() == "" ? "0" : dt.Rows[0]["InterestRec"].ToString()) + Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString())));

                        //if (Convert.ToDouble(txtPayAmnt.Text.Trim().ToString()) < Convert.ToDouble(TotalAmt))
                        //{
                        //    txtLoanTotAmt.Text = "0";
                        //    ViewState["LoanTotAmt"] = TotalAmt.ToString();
                        //    lblMessage.Text = "Amount of receipt is less than paid amount " + Convert.ToDouble(TotalAmt).ToString() + "...!!";
                        //    ModalPopup.Show(this.Page);
                        //    return;
                        //}
                        //else
                        //{
                        txtLoanTotAmt.Text = TotalAmt.ToString();
                        ViewState["LoanTotAmt"] = TotalAmt.ToString();
                        return;
                        //}
                    }
                    else
                    {
                        txtLoanTotAmt.Text = "0";
                        ViewState["LoanTotAmt"] = Convert.ToDouble(txtLoanTotAmt.Text.Trim().ToString());
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtProName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = TxtProName.Text;
            string[] GLS = GL.Split('_');
            if (GLS.Length > 1)
            {
                //Added By Amol on 22092017 as per ambika mam instruction
                if (BD.GetProdOperate(Session["BRCD"].ToString(), GLS[1].ToString()).ToString() != "3")
                {
                    TxtProName.Text = GLS[0].ToString();
                    TxtProcode.Text = GLS[1].ToString();

                    if (TxtProcode.Text != "")
                    {
                        string CAT = MT.GetDepositCat(Session["BRCD"].ToString(), TxtProcode.Text, "MISTRF");
                        if (CAT != "MIS" && Rdb_IntPay.Checked == true)
                        {
                            WebMsgBox.Show("Interest Pay facility is only for MIS group," + TxtProcode.Text + " is not MIS!...", this.Page);
                            clear();
                            return;
                        }
                    }

                    string GLL = BD.GetAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString());
                    if (GLL != null)
                    {
                        string[] GLLL = GLL.Split('_');
                        ViewState["GLCODE"] = GLLL[1].ToString();
                        if (ViewState["GLCODE"].ToString() == "15")
                        {
                            Invisi(true);
                        }
                    }

                    string[] GLT = BD.GetTDAAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString(), ViewState["GLCODE"].ToString()).Split('_');
                    ViewState["DRGL"] = GLT[1].ToString();
                    ViewState["IR"] = GLT[2].ToString();
                    AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();
                }
                else
                {
                    TxtProcode.Text = "";
                    TxtProName.Text = "";
                    lblMessage.Text = "Product is not operating...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
            if (TxtProName.Text == "")
            {
                WebMsgBox.Show("Invalid product code", this.Page);
                clear();
                TxtProcode.Focus();
                return;
            }
            else
            {
                // Check RD or LKP

                string IsRD = CMN.CheckRD(TxtProcode.Text.ToString(), Session["BRCD"].ToString());
                if (IsRD == null)
                {
                    ViewState["RD"] = "NO";
                    string CATLKP = MT.GetDepositCat(Session["BRCD"].ToString(), TxtProcode.Text, "MISTRF");
                    if (CATLKP == "LKPRD")
                    {
                        string LKPRD = CMN.CheckLKPLikeRD("1");
                        if (LKPRD == "Y")
                        {
                            ViewState["LKP"] = "YES";
                        }
                        else
                        {
                            ViewState["LKP"] = "NO";
                        }
                    }
                    else
                    {
                        ViewState["LKP"] = "NO";
                    }
                }
                else
                {
                    ViewState["RD"] = "YES";
                    ViewState["LKP"] = "NO";
                }

            }
            txtAccNo.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAccname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC = TxtAccname.Text;
            string[] ACT = AC.Split('_');
            if (ACT.Length > 1)
            {
                txtAccNo.Text = ACT[1].ToString();
                TxtAccname.Text = ACT[0].ToString();
                AC_Status = CMN.GetAccStatus(Session["BRCD"].ToString(), TxtProcode.Text, ACT[1].ToString());
                if (AC_Status == "1" || AC_Status == "3" || AC_Status == "4")
                {
                    accno();
                }

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Radio button checked change

    //RadioButton checked Mature
    protected void rdbMature_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["MATURITY"] = "MA";
            lblpenal.Text = "SB int rate";
            lblpenalrate.Text = "SB int";
            clear();
            TxtProcode.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    //RadioButton checked PreMature
    protected void rdbPreMature_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["MATURITY"] = "PRE";
            lblpenal.Text = "Penal int rate";
            lblpenalrate.Text = "Penal int";
            clear();
            TxtProcode.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void RdbYes_CheckedChanged(object sender, EventArgs e)
    {
        ddlIntrestPay.Enabled = true;
        ddlduration.Enabled = true;
        TxtRate.Enabled = true;
        TxtPeriod.Enabled = true;
        PWI.Visible = true;
    }

    protected void RdbNo_CheckedChanged(object sender, EventArgs e)
    {
        ddlIntrestPay.Enabled = false;
        ddlduration.Enabled = false;
        TxtRate.Enabled = false;
        TxtPeriod.Enabled = false;
        PWI.Visible = false;
    }

    protected void RdbP_CheckedChanged(object sender, EventArgs e)
    {
        DIVINT.Visible = true;
    }

    protected void RdbPI_CheckedChanged(object sender, EventArgs e)
    {
        DIVINT.Visible = false;
    }

    protected void RdbRenew_CheckedChanged(object sender, EventArgs e)
    {
        int count = 0;
        try
        {
            BD.BindPayment(ddlPayType, "2");
            //For Selecting Renew As Default for Renew Case
            ddlPayType.SelectedValue = "3";
            count = GetPaymentModeValue();
            if (count == 1)
            {
                RdbClose.Checked = true;
                RdbRenew.Checked = false;
                ddlPayType.SelectedValue = "1";

            }
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }


    }

    protected void RdbClose_CheckedChanged(object sender, EventArgs e)
    {
        //  amol
        // BD.BindPayment(ddlPayType, "3");
        BD.BindPayment(ddlPayType, "3");
    }

    //protected void GrdFDLedger_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GrdFDLedger.PageIndex = e.NewPageIndex;
    //    // BindGrid();
    //}

    protected void Rdb_IntPay_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            string CAT = MT.GetDepositCat(Session["BRCD"].ToString(), TxtProcode.Text, "MISTRF");
            if (CAT != "MIS" && Rdb_IntPay.Checked == true)
            {
                WebMsgBox.Show("Interest Pay facility is only for MIS group!...", this.Page);
                clear();
                return;
            }
            else
            {
                accno();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void RdbTrfInt_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            BD.BindPayment(ddlPayType, "1");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Button click event

    protected void btnLoanInt_Click(object sender, EventArgs e)
    {
        try
        {
            string SetNo = "";
            string glcode = "";
            string CN = "", CD = "";
            string PAYMAST = "TDCLOSE";
            if (Convert.ToDouble(txtTotLoanBal.Text.Trim().ToString() == "" ? "0" : txtTotLoanBal.Text.Trim().ToString()) > 0)
            {
                glcode = LC.getGlCode(Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString());
                SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();

                CN = "0";
                CD = "01/01/1990";

                result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode, TxtProcode.Text.Trim().ToString(),
                            txtAccNo.Text.Trim().ToString(), "TR", "", txtTotLoanBal.Text.Trim().ToString(), "2", "7", "TR", SetNo, CN, CD, "0", "0", "1003",
                            "0", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", PAYMAST, "0", "", ViewState["RID"].ToString(), "0");
                if (result > 0)
                {
                    dt = new DataTable();
                    dt = LC.GETDATA(Session["BRCD"].ToString(), "3", ViewState["LSubGlCode"].ToString());

                    //principle o/s
                    result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["GLCODE"].ToString(), ViewState["LSubGlCode"].ToString(),
                    ViewState["LAccNo"].ToString(), "TR", "", txtLoanBal.Text.Trim().ToString(), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1003",
                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", PAYMAST, "0", "", ViewState["RID"].ToString(), "0");

                    if (result > 0)
                    {
                        resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), ViewState["LSubGlCode"].ToString(), ViewState["LSubGlCode"].ToString(), ViewState["LAccNo"].ToString(), "1", "1", "7", "TDA Closure Principle Credited", txtLoanBal.Text.Trim().ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());

                        //interest Applied credit to acc
                        if (Convert.ToDouble(txtLoanInt.Text.Trim().ToString() == "" ? "0" : txtLoanInt.Text.Trim().ToString()) > 0)
                        {
                            result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "11", dt.Rows[0]["IR"].ToString(),
                            ViewState["LAccNo"].ToString(), "TR", "", txtLoanInt.Text.Trim().ToString(), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1003",
                            "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", PAYMAST, "0", "", ViewState["RID"].ToString(), "0");

                            if (result > 0)
                            {
                                //interest Applied Debit
                                result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "11", dt.Rows[0]["IR"].ToString(),
                                ViewState["LAccNo"].ToString(), "TR", "", txtLoanInt.Text.Trim().ToString(), "2", "7", "TR_INT", SetNo, "0", "01/01/1900", "0", "0", "1003",
                                "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", PAYMAST, "0", "", ViewState["RID"].ToString(), "0");

                                if (result > 0)
                                {
                                    //interest Applied Credit to GL 100
                                    result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", dt.Rows[0]["PLACCNO"].ToString(),
                                    ViewState["LAccNo"].ToString(), "TR", "", txtLoanInt.Text.Trim().ToString(), "1", "7", "TR_INT", SetNo, "0", "01/01/1900", "0", "0", "1003",
                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", PAYMAST, "0", "", ViewState["RID"].ToString(), "0");

                                    resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), ViewState["LSubGlCode"].ToString(), dt.Rows[0]["IR"].ToString(), ViewState["LAccNo"].ToString(), "2", "1", "7", "TDA Closure Interest Credited", txtLoanInt.Text.Trim().ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                    resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), ViewState["LSubGlCode"].ToString(), dt.Rows[0]["IR"].ToString(), ViewState["LAccNo"].ToString(), "2", "2", "7", "TDA Closure Interest Debited", txtLoanInt.Text.Trim().ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());


                                }
                            }
                        }
                        if (result > 0)
                        {
                            int i = LC.CloseLoanAcc(Session["BRCD"].ToString(), ViewState["LSubGlCode"].ToString(), ViewState["LAccNo"].ToString(), Session["EntryDate"].ToString(), "LAD");

                            ViewState["TotLoanBal"] = txtTotLoanBal.Text.Trim().ToString();

                            if (i > 0)
                            {
                                if (Convert.ToDouble(txtPayAmnt.Text.Trim().ToString() == "" ? "0" : txtPayAmnt.Text.Trim().ToString()) > 0)
                                    txtPayAmnt.Text = System.Math.Abs((Convert.ToDouble(TxtPrincPaybl.Text.Trim().ToString() == "" ? "0" : TxtPrincPaybl.Text.Trim().ToString()) + Convert.ToDouble(TxtIntrestPaybl.Text.Trim().ToString() == "" ? "0" : TxtIntrestPaybl.Text.Trim().ToString())) - Convert.ToDouble(ViewState["TotLoanBal"].ToString())).ToString();

                                if (Convert.ToDouble(TxtPrincPaybl.Text.Trim().ToString() == "" ? "0" : TxtPrincPaybl.Text.Trim().ToString()) > 0)
                                    TxtPrincPaybl.Text = System.Math.Abs(Convert.ToDouble(TxtPrincPaybl.Text.Trim().ToString()) - Convert.ToDouble(ViewState["TotLoanBal"].ToString())).ToString();

                                ClearLoanInfo();
                                btnLoanInt.Enabled = false;

                                lblMessage.Text = "Loan Close with SetNo : " + Convert.ToInt32(SetNo);
                                ModalPopup.Show(this.Page);
                                FL = "Insert";//Dhanya Shetty
                                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DepCloser_LoanClose_" + TxtProcode + "_" + txtAccNo + "_" + txtPayAmnt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
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

    protected void BtnPostInt_Click(object sender, EventArgs e)
    {
        try
        {
            //if (Rdb_ExistingSetno.Checked == true)  // For Existing Setno 
            //{
            //    string SetNo = Txt_ExistSetno.Text;

            //    string PLACC = CMN.GetPLACC(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString());
            //    string PAYMAST = "TDCLOSE";
            //    double INTP = 0;
            //    double INTPAID = 0;
            //    double SBINT = 0;
            //    double ADMINFEE = 0;
            //    double COMMICHG = 0;
            //    string CD, CD1;
            //    CD = CD1 = "";
            //    INTP = Convert.ToDouble(TxtInterestNew.Text);
            //    if (INTP > 0)
            //    {
            //        CD = "1";
            //        CD1 = "2";
            //    }
            //    else
            //    {
            //        CD = "2";
            //        CD1 = "1";
            //    }
            //    INTPAID = Math.Abs(Convert.ToDouble(TxtInterestNew.Text));
            //    SBINT = Math.Abs(Convert.ToDouble(TxtSbintrest.Text));
            //    ADMINFEE = Math.Abs(Convert.ToDouble(TxtAdminCharges.Text));
            //    COMMICHG = Math.Abs(Convert.ToDouble(TxtCommission.Text));
            //    if (INTPAID != 0)
            //    {

            //        result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
            //                              , txtAccNo.Text, "BY INTEREST", "INTR /" + TxtProcode.Text + "/" + txtAccNo.Text, INTPAID.ToString(), CD, "10", "TR-INT", SetNo, "0", "", "",
            //                              Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
            //                              PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

            //        result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", PLACC
            //                              , txtAccNo.Text, "BY INEREST", "INTR /" + TxtProcode.Text + "/" + txtAccNo.Text, INTPAID.ToString(), CD1, "10", "TR-INT", SetNo, "0", "", "",
            //                              Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
            //                              PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
            //    }
            //    if (SBINT != 0)
            //    {
            //        result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
            //            , txtAccNo.Text, "SB INTEREST", "SB INT /" + TxtProcode.Text + "/" + txtAccNo.Text, SBINT.ToString(), Convert.ToInt32(TxtSbintrest.Text) > 0 ? "1" : "2", "10", "TR-INT", SetNo, "0", "", "",
            //                             Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
            //                             PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

            //        PLACC = CMN.GetPLACC(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text);
            //        result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", PLACC
            //                        , txtAccNo.Text, "SB INTEREST", "SB INT /" + TxtProcode.Text + "/" + txtAccNo.Text, SBINT.ToString(), Convert.ToInt32(TxtSbintrest.Text) > 0 ? "2" : "1", "10", "TR-INT", SetNo, "0", "", "",
            //                        Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
            //                        PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
            //    }
            //    if (result > 0)
            //    {
            //        //Added By amolb ON 2017-01-14 for update last interest date
            //        result = CurrentCls1.UpdateLastIntDate(Session["EntryDate"].ToString(), Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Txtcustno.Text.Trim().ToString());
            //        TxtIntrestPaybl.Text = ((Convert.ToDouble(TxtInterestNew.Text) + Convert.ToDouble(TxtIntrestPaybl.Text) + Convert.ToDouble(TxtSbintrest.Text))).ToString();


            //    }



            //    bool PrinValid = false;
            //    bool IntPayValid = false;
            //    if (COMMICHG != 0 || ADMINFEE != 0)
            //    {
            //        double INT_Total = 0, Total = 0;

            //        INT_Total = Convert.ToDouble(TxtIntrestPaybl.Text);
            //        Total = Convert.ToDouble(TxtCommission.Text) + Convert.ToDouble(TxtAdminCharges.Text);

            //        if (INT_Total >= Total)
            //        {
            //            IntPayValid = true;
            //            PrinValid = false;
            //        }
            //        else
            //        {
            //            IntPayValid = false;
            //            PrinValid = true;
            //        }

            //    }


            //    dt = CurrentCls1.GetPLComm_Admin(Session["BRCD"].ToString(), TxtProcode.Text);
            //    if (COMMICHG != 0)//commision applied on premature
            //    {
            //        if (PrinValid == true) // For Interst payable is insufficient then deduct from Principle payable
            //        {
            //            result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text
            //                                 , txtAccNo.Text, "BY INEREST", "COMM /" + TxtProcode.Text + "/" + txtAccNo.Text, TxtCommission.Text, "2", "7", "TR", SetNo, "0", "", "",
            //                                 Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
            //                                 PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

            //            result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", dt.Rows[0]["COMMPL"].ToString(),
            //                                  txtAccNo.Text, "BY INEREST", "COMM /" + TxtProcode.Text + "/" + txtAccNo.Text, TxtCommission.Text, "1", "7", "TR", SetNo, "0", "", "",
            //                                  Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
            //                                  PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
            //        }
            //        else  // For Interst payable is sufficient 
            //        {

            //            result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
            //                                 , txtAccNo.Text, "BY INEREST", "COMM /" + TxtProcode.Text + "/" + txtAccNo.Text, TxtCommission.Text, "2", "7", "TR", SetNo, "0", "", "",
            //                                 Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
            //                                 PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

            //            result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", dt.Rows[0]["COMMPL"].ToString(),
            //                                  txtAccNo.Text, "BY INEREST", "COMM /" + TxtProcode.Text + "/" + txtAccNo.Text, TxtCommission.Text, "1", "7", "TR", SetNo, "0", "", "",
            //                                  Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
            //                                  PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
            //        }
            //    }

            //    if (ADMINFEE != 0)//Admin Fees for Premature
            //    {
            //        if (PrinValid == true) // For Interst payable is insufficient then deduct from Principle payable
            //        {
            //            result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text
            //                               , txtAccNo.Text, "BY INEREST", "OTH Charge /" + TxtProcode.Text + "/" + txtAccNo.Text, TxtAdminCharges.Text, "2", "7", "TR", SetNo, "0", "", "",
            //                               Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
            //                               PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

            //            result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", dt.Rows[0]["OTHPL"].ToString(),
            //                                  txtAccNo.Text, "BY INEREST", "OTH Charge /" + TxtProcode.Text + "/" + txtAccNo.Text, TxtAdminCharges.Text, "1", "7", "TR", SetNo, "0", "", "",
            //                                  Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
            //                                  PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
            //        }
            //        else   // For Interst payable is sufficient 
            //        {
            //            result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
            //                             , txtAccNo.Text, "BY INEREST", "OTH Charge /" + TxtProcode.Text + "/" + txtAccNo.Text, TxtAdminCharges.Text, "2", "7", "TR", SetNo, "0", "", "",
            //                             Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
            //                             PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

            //            result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", dt.Rows[0]["OTHPL"].ToString(),
            //                                  txtAccNo.Text, "BY INEREST", "OTH Charge /" + TxtProcode.Text + "/" + txtAccNo.Text, TxtAdminCharges.Text, "1", "7", "TR", SetNo, "0", "", "",
            //                                  Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
            //                                  PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

            //        }
            //    }


            //    if (result > 0)
            //    {
            //        //Added By amolb ON 2017-01-14 for update last interest date
            //        //   result = CurrentCls1.UpdateLastIntDate(Session["EntryDate"].ToString(), Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Txtcustno.Text.Trim().ToString());
            //        if (PrinValid == true)
            //        {
            //            TxtPrincPaybl.Text = (Convert.ToDouble(TxtPrincPaybl.Text) - (Convert.ToDouble(TxtCommission.Text) + Convert.ToDouble(TxtAdminCharges.Text))).ToString();
            //        }
            //        else
            //        {
            //            TxtIntrestPaybl.Text = (Convert.ToDouble(TxtIntrestPaybl.Text) - (Convert.ToDouble(TxtCommission.Text) + Convert.ToDouble(TxtAdminCharges.Text))).ToString();

            //        }
            //        TxtInterestNew.Text = "0";
            //        TxtSbintrest.Text = "0";
            //        TxtCommission.Text = "0";
            //        TxtAdminCharges.Text = "0";
            //        BtnPostInt.Enabled = false;

            //        lblMessage.Text = "Interest Post Successfully Int paid no is :" + SetNo + "..........!!";
            //        ModalPopup.Show(this.Page);
            //        FL = "Insert";//Dhanya Shetty
            //        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DepCloser_Interest_" + TxtProcode + "_" + txtAccNo + "_" + txtPayAmnt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            //    }


            //}
            //else
            //{
            string SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
            string PLACC = CMN.GetPLACC(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString());
            string PAYMAST = "TDCLOSE", P1;
            double INTP = 0;
            double INTPAID = 0;
            double SBINT = 0;
            double ADMINFEE = 0;
            double COMMICHG = 0;
            string CD, CD1;
            CD = CD1 = "";
            INTP = Convert.ToDouble(TxtInterestNew.Text);
            if (INTP > 0)
            {
                CD = "1";
                CD1 = "2";
            }
            else
            {
                CD = "2";
                CD1 = "1";
            }
            INTPAID = Math.Abs(Convert.ToDouble(TxtInterestNew.Text));
            SBINT = Math.Abs(Convert.ToDouble(TxtSbintrest.Text));
            ADMINFEE = Math.Abs(Convert.ToDouble(TxtAdminCharges.Text));
            COMMICHG = Math.Abs(Convert.ToDouble(TxtCommission.Text));
            if (INTPAID != 0)
            {

                result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
                    , txtAccNo.Text, CD == "1" ? "TO INTEREST" : "BY INTEREST", "INTR /" + TxtProcode.Text + "/" + txtAccNo.Text, INTPAID.ToString(), CD, "10", "TR-INT", SetNo, "0", "", "",
                                      Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                      PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

                result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", PLACC
                                      , txtAccNo.Text, CD1 == "1" ? "TO INTEREST" : "BY INTEREST", "INTR /" + TxtProcode.Text + "/" + txtAccNo.Text, INTPAID.ToString(), CD1, "10", "TR-INT", SetNo, "0", "", "",
                                      Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                      PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
            }
            if (SBINT != 0)
            {
                result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
                    , txtAccNo.Text, Convert.ToInt32(TxtSbintrest.Text) > 0 ? "TO SB INTEREST" : "BY SB INTEREST", "SB INT /" + TxtProcode.Text + "/" + txtAccNo.Text, SBINT.ToString(),
                    Convert.ToInt32(TxtSbintrest.Text) > 0 ? "1" : "2", "10", "TR-INT", SetNo, "0", "", "",
                                     Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                     PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

                PLACC = CMN.GetPLACC(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text);
                result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", PLACC
                                , txtAccNo.Text, Convert.ToInt32(TxtSbintrest.Text) > 0 ? "TO SB INTEREST" : "BY SB INTEREST",
                                "SB INT /" + TxtProcode.Text + "/" + txtAccNo.Text, SBINT.ToString(), Convert.ToInt32(TxtSbintrest.Text) > 0 ? "2" : "1", "10", "TR-INT", SetNo, "0", "", "",
                                Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
            }
            if (result > 0)
            {
                //Added By amolb ON 2017-01-14 for update last interest date
                result = CurrentCls1.UpdateLastIntDate(Session["EntryDate"].ToString(), Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Txtcustno.Text.Trim().ToString());
                TxtIntrestPaybl.Text = ((Convert.ToDouble(TxtInterestNew.Text) + Convert.ToDouble(TxtIntrestPaybl.Text) + Convert.ToDouble(TxtSbintrest.Text))).ToString();


            }



            bool PrinValid = false;
            bool IntPayValid = false;
            if (COMMICHG != 0 || ADMINFEE != 0)
            {
                double INT_Total = 0, Total = 0;

                INT_Total = Convert.ToDouble(TxtIntrestPaybl.Text);
                Total = Convert.ToDouble(TxtCommission.Text) + Convert.ToDouble(TxtAdminCharges.Text);

                if (INT_Total >= Total)
                {
                    IntPayValid = true;
                    PrinValid = false;
                }
                else
                {
                    IntPayValid = false;
                    PrinValid = true;
                }

            }


            dt = CurrentCls1.GetPLComm_Admin(Session["BRCD"].ToString(), TxtProcode.Text);
            if (COMMICHG != 0)//commision applied on premature
            {
                if (PrinValid == true) // For Interst payable is insufficient then deduct from Principle payable
                {
                    result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text
                                         , txtAccNo.Text, "BY INEREST", "COMM /" + TxtProcode.Text + "/" + txtAccNo.Text, TxtCommission.Text, "2", "7", "TR", SetNo, "0", "", "",
                                         Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                         PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

                    result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", dt.Rows[0]["COMMPL"].ToString(),
                                          txtAccNo.Text, "TO INEREST", "COMM /" + TxtProcode.Text + "/" + txtAccNo.Text, TxtCommission.Text, "1", "7", "TR", SetNo, "0", "", "",
                                          Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                          PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                }
                else  // For Interst payable is sufficient 
                {

                    result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
                                         , txtAccNo.Text, "BY INEREST", "COMM /" + TxtProcode.Text + "/" + txtAccNo.Text, TxtCommission.Text, "2", "7", "TR", SetNo, "0", "", "",
                                         Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                         PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

                    result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", dt.Rows[0]["COMMPL"].ToString(),
                                          txtAccNo.Text, "TO INEREST", "COMM /" + TxtProcode.Text + "/" + txtAccNo.Text, TxtCommission.Text, "1", "7", "TR", SetNo, "0", "", "",
                                          Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                          PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                }
            }

            if (ADMINFEE != 0)//Admin Fees for Premature
            {
                if (PrinValid == true) // For Interst payable is insufficient then deduct from Principle payable
                {
                    result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text
                                       , txtAccNo.Text, "BY INEREST", "OTH Charge /" + TxtProcode.Text + "/" + txtAccNo.Text, TxtAdminCharges.Text, "2", "7", "TR", SetNo, "0", "", "",
                                       Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                       PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

                    result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", dt.Rows[0]["OTHPL"].ToString(),
                                          txtAccNo.Text, "TO INEREST", "OTH Charge /" + TxtProcode.Text + "/" + txtAccNo.Text, TxtAdminCharges.Text, "1", "7", "TR", SetNo, "0", "", "",
                                          Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                          PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                }
                else   // For Interst payable is sufficient 
                {
                    result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
                                     , txtAccNo.Text, "BY INEREST", "OTH Charge /" + TxtProcode.Text + "/" + txtAccNo.Text, TxtAdminCharges.Text, "2", "7", "TR", SetNo, "0", "", "",
                                     Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                     PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

                    result = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", dt.Rows[0]["OTHPL"].ToString(),
                                          txtAccNo.Text, "TO INEREST", "OTH Charge /" + TxtProcode.Text + "/" + txtAccNo.Text, TxtAdminCharges.Text, "1", "7", "TR", SetNo, "0", "", "",
                                          Session["BRCD"].ToString(), "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                          PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

                }
            }


            if (result > 0)
            {
                //Added By amolb ON 2017-01-14 for update last interest date
                //   result = CurrentCls1.UpdateLastIntDate(Session["EntryDate"].ToString(), Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Txtcustno.Text.Trim().ToString());
                if (PrinValid == true)
                {
                    TxtPrincPaybl.Text = (Convert.ToDouble(TxtPrincPaybl.Text) - (Convert.ToDouble(TxtCommission.Text) + Convert.ToDouble(TxtAdminCharges.Text))).ToString();
                }
                else
                {
                    TxtIntrestPaybl.Text = (Convert.ToDouble(TxtIntrestPaybl.Text) - (Convert.ToDouble(TxtCommission.Text) + Convert.ToDouble(TxtAdminCharges.Text))).ToString();

                }
                TxtInterestNew.Text = "0";
                TxtSbintrest.Text = "0";
                TxtCommission.Text = "0";
                TxtAdminCharges.Text = "0";
                BtnPostInt.Enabled = false;

                //lblMessage.Text = "
                //ModalPopup.Show(this.Page);

                WebMsgBox.Show("Interest Post Successfully Int paid Voucher No :" + SetNo, this.Page);
                // WebMsgBox.Show("Interest Post Successfully Int paid no is :" + SetNo, this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DepCloser_Interest_" + TxtProcode + "_" + txtAccNo + "_" + txtPayAmnt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
            // }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    //  Voucher Entry - Interest
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtPayAmnt.Text == "" || txtPayAmnt.Text == "0")
            {
                WebMsgBox.Show("Invalid Total Pay Amount .....!", this.Page);
                return;
            }
            if (RdbMultipleClose.Checked == true)
            {
                Multiple_TDAClose(ddlPayType.SelectedValue.ToString());
            }
            else
            {

                if (ddlPayType.SelectedValue != "0")
                {
                    if ((TxtInterestNew.Text == "0" || TxtInterestNew.Text == "") && (TxtSbintrest.Text == "0" || TxtSbintrest.Text == ""))
                    {
                        if (ddlPayType.SelectedValue == "3") //renew
                        {
                            string AFEE = "0", AFEESUBGL = "0", COMMchg = "0", COMMSBGL = "0";

                            ////BtnSubmit.Enabled = false;
                            //if (TxtAdminCharges.Text != "0") //Admin Fee is exists
                            //{
                            //    dt = CurrentCls1.GetPLComm_Admin(Session["BRCD"].ToString(), TxtProcode.Text);
                            //    if(dt.Rows.Count>0)
                            //    {
                            //        AFEE = TxtAdminCharges.Text;
                            //        AFEESUBGL = dt.Rows[0]["OTHPL"].ToString();
                            //        if (ViewState["GLCODE"].ToString() == "15") //Commision Chrages is exists
                            //        {
                            //            COMMchg = TxtCommission.Text;
                            //            COMMSBGL = dt.Rows[0]["COMMPL"].ToString();
                            //        }

                            //    }
                            //}
                            string IntPostYpe = "";
                            if (Convert.ToDouble(TxtIntrestPaybl.Text) < 0)
                            {
                                IntPostYpe = "REV";
                            }
                            else
                            {
                                IntPostYpe = "NOR";
                            }
                            string url = "FrmTDARenewN.aspx?IPostType=" + IntPostYpe + "&MS=N&TPAMT=" + txtPayAmnt.Text + "&FD=" + txtAccNo.Text + "&PRD=" + TxtProcode.Text + "&PrdName=" + TxtProName.Text + "&IntType=" + ddlIntrestPay.SelectedValue + "&Period=" + TxtPeriod.Text + "&FDP=" + TxtProcode.Text + "&CT=" + Txtcustno.Text + "&DAMT=" + TxtDepoAmt.Text + "&ACCT=" + ViewState["ACCT"].ToString() + "&OPRTYPE=" + ViewState["OPRTYPE"].ToString() + "&INTPAY=" + TxtIntrestPaybl.Text + "&IR=" + ViewState["IR"].ToString() + "&REFID=" + ViewState["RID"].ToString() + "&ADFEE=" + AFEE + "&ADSBGL=" + AFEESUBGL + "&COMMCHG=" + COMMchg + "&COMMSBGL=" + COMMSBGL + "&GLC=" + ViewState["GLCODE"].ToString();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + url + "', 'popup_window', 'width=1150,height=600,left=50,top=50,resizable=no');", true);
                            BindGrid1();
                            clear();
                            return;
                        }
                        else if (RdbTrfInt.Checked == true && Rdb_IntPay.Checked == true)
                        {
                            MisIntTrfOnly(); // Only Interest Transfer
                        }
                        else
                        {
                            double credit = 0;
                            int result = 0;
                            if (TxtInterestNew.Text == "")
                            {
                                WebMsgBox.Show("Invalid payable Amount", this.Page);
                                return;
                            }

                            if (TxtIntrestPaybl.Text != "" && TxtInterestNew.Text != "")
                            {
                                credit = Convert.ToDouble(TxtIntrestPaybl.Text) - Convert.ToDouble(TxtInterestNew.Text);
                            }
                            else
                            {
                                WebMsgBox.Show("Please Calculate Interest..", this.Page);
                                return;
                            }
                            string[] arraydt = Session["EntryDate"].ToString().Split('/');
                            string TBLNAME = "AVSM_" + arraydt[2] + arraydt[1];

                            // get SetNo     

                            string ScrollNo = "1";

                            // Get GetPLACC
                            string PLACC = string.Empty;
                            if (hdnValue.Value == "1")
                                PLACC = CMN.GetPLACC(txtBrcd.Text, ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString());
                            else
                                PLACC = CMN.GetPLACC(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString());

                            PaidInt(PLACC, "", "N", "N");

                        }
                        BindGrid1();
                        clear();
                    }
                    else
                    {
                        WebMsgBox.Show("Interest not Posted..! Post The Interest First..!", this.Page);
                        return;
                    }
                }
                else
                {
                    if (Convert.ToDouble(txtPayAmnt.Text.Trim().ToString()) == 0.00)
                    {
                        //Amolb
                        double credit = 0;
                        int result = 0;
                        if (TxtInterestNew.Text == "")
                        {
                            WebMsgBox.Show("Invalid payable Amount", this.Page);
                            return;
                        }

                        if (TxtIntrestPaybl.Text != "" && TxtInterestNew.Text != "")
                        {
                            credit = Convert.ToDouble(TxtIntrestPaybl.Text) - Convert.ToDouble(TxtInterestNew.Text);
                        }
                        else
                        {
                            WebMsgBox.Show("Please Calculate Interest..", this.Page);
                            return;
                        }
                        string[] arraydt = Session["EntryDate"].ToString().Split('/');
                        string TBLNAME = "AVSM_" + arraydt[2] + arraydt[1];

                        // get SetNo     

                        string ScrollNo = "1";

                        // Get GetPLACC
                        string PLACC = string.Empty;
                        if (hdnValue.Value == "1")
                            PLACC = CMN.GetPLACC(txtBrcd.Text, ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString());
                        else
                            PLACC = CMN.GetPLACC(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString());

                        PostEntry(PLACC, "", "N", "N");

                        BindGrid1();
                        clear();
                    }
                    else
                    {
                        lblMessage.Text = "Select Payment Type First...!!";
                        ModalPopup.Show(this.Page);
                        ddlPayType.Focus();
                        return;
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    // Calculate Interest
    protected void BtnCalIntrst_Click(object sender, EventArgs e)
    {
        try
        {
            // dt = CMN.GetMonthYearList(dtDeposDate.Text.ToString(), Session["EntryDate"].ToString());
            int count = dt.Rows.Count;
            string GetFMonth = DateTime.DaysInMonth(2015, 02).ToString();

            // Calculate days Difference
            string depositdt = "";
            // DateTime depositdate = Convert.ToDateTime(dtDeposDate.Text);
            depositdt = dtDeposDate.Text;
            //DateTime closuredate = DateTime.Now;
            string cldate = "";

            if (ViewState["MATURITY"].ToString() == "PRE")
            {
                //closuredate = Convert.ToDateTime(Session["EntryDate"].ToString());
                cldate = Session["EntryDate"].ToString();
            }

            if (ViewState["MATURITY"].ToString() == "MA")
            {
                //closuredate = Convert.ToDateTime(DtDueDate.Text);
                cldate = DtDueDate.Text;
            }

            string daysDiff = "";// (closuredate - depositdate).TotalDays.ToString(); // Calculate days Difference
            daysDiff = conn.GetDayDiff(cldate, depositdt);


            //Get Interest Rate Balance        
            float rate = 0;
            if (rdbPreMature.Checked == true)
            {
                rate = CurrentCls.GetIntrestRate(TxtProcode.Text.ToString(), daysDiff.ToString(), Session["BRCD"].ToString(), ddlduration.SelectedValue.ToString(), rdbPreMature.Checked);
            }
            else
            {
                rate = CurrentCls.GetIntrestRate(TxtProcode.Text.ToString(), TxtPeriod.Text.ToString(), Session["BRCD"].ToString(), ddlduration.SelectedValue.ToString(), rdbPreMature.Checked);
            }
            //Get Penal interest
            float penalintrst = 0;
            if (rdbPreMature.Checked == true)
            {
                penalintrst = CurrentCls.GetPenalIntrestRate(TxtProcode.Text.ToString(), daysDiff.ToString(), Session["BRCD"].ToString(), ddlduration.SelectedValue.ToString(), rdbPreMature.Checked);
                rate = rate - penalintrst;
            }
            else
            {
                penalintrst = CurrentCls.GetPenalIntrestRate(TxtProcode.Text.ToString(), TxtPeriod.Text.ToString(), Session["BRCD"].ToString(), ddlduration.SelectedValue.ToString(), rdbPreMature.Checked);
            }
            //Calculate Interest
            double interest = Convert.ToInt32((((Convert.ToDouble(TxtPrincPaybl.Text.ToString())) * (rate) / 100) / 365) * Convert.ToDouble(daysDiff));
            double interestfinal = interest - Convert.ToDouble(TxtIntrestPaybl.Text);
            TxtInterestNew.Text = Convert.ToInt32(interestfinal).ToString();

            if (rdbPreMature.Checked == true)
            {
                TxtInterestNew.Text = (Convert.ToInt32((((Convert.ToDouble(TxtPrincPaybl.Text.ToString())) * (rate) / 100) / 365) * Convert.ToDouble(daysDiff))).ToString();
            }
            if (dtDeposDate.Text == "" || DtDueDate.Text == "")
            {
                return;
            }

            // IF PREMATURED
            if (ViewState["MATURITY"].ToString() == "PRE")
            {
                // Get Penal interest

                if (ViewState["RD"].ToString() == "YES")
                {
                    penalintrst = CurrentCls.GetPenalIntrestRate(TxtProcode.Text.ToString(), TxtPeriod.Text.ToString(), Session["BRCD"].ToString(), ddlduration.SelectedValue.ToString(), rdbPreMature.Checked);
                    // Get last date and interest rate
                    string dtr = Session["EntryDate"].ToString();
                    float intrate = rate - penalintrst;

                    // Get months between opening date and final date
                    dtDeposDate.Text.ToString();
                    Session["EntryDate"].ToString();

                    // List of months and years beteeen dates
                    dt = CMN.GetMonthYearList(dtDeposDate.Text.ToString(), Session["EntryDate"].ToString());
                    if (ddlIntrestPay.SelectedValue == "1")
                    {
                        CalRDintrestMnth(count, dt.ToString(), dtr, intrate);
                        return;
                    }

                    if (ddlIntrestPay.SelectedValue == "2")
                    {
                        CalRDintrestQrtly(count, dt.ToString(), dtr, intrate);
                        return;
                    }
                    // PAYABLE AMOUNT
                    //TxtPaybleAmount0.Text = (Convert.ToInt32(TxtPrincPaybl.Text.ToString()) + interest).ToString();
                }
            }
            ////MAK 27/09/2016

            // IF MATURED
            if (ViewState["MATURITY"].ToString() == "MA")
            {
                // Calculate DaysDifference for SB Interest
                int SbIntrst = 0;
                string daysDiffSB = "";
                int SBD = 0;
                daysDiffSB = conn.GetDayDiff(Session["EntryDate"].ToString(), DtDueDate.Text.ToString());
                SBD = CurrentCls.GetDays(Session["BRCD"].ToString());
                if (SBD > Convert.ToInt32(daysDiffSB.ToString()))
                {
                    TxtSbintrest.Text = "0";
                    TxtInterestNew.Text = TxtIntrest.Text;
                    // TxtPaybleAmount0.Text = "0";
                    txtPayAmnt.Text = (Convert.ToInt32(TxtPrincPaybl.Text.ToString()) + Convert.ToDouble(TxtIntrest.Text) + SbIntrst).ToString();
                    return;
                }
                // Get Interest rate for SBIT
                string SBINTPARAM = CMN.GetSBInt(Session["BRCD"].ToString());
                txt1.Text = SBINTPARAM;
                TxtInterestNew.Text = TxtIntrestPaybl.Text;
                // Get SBinterest
                if (Convert.ToInt32(daysDiffSB) > 0)
                {
                    SbIntrst = Convert.ToInt32((Convert.ToDouble(TxtPrincPaybl.Text.ToString()) * Convert.ToDouble(SBINTPARAM)) / 100 / 365 * Convert.ToDouble(daysDiffSB));
                }
                // Assign SBinterest
                TxtSbintrest.Text = SbIntrst.ToString();

                // Calculate Payable amount
                if (ViewState["RD"].ToString() == "YES")
                {
                    // Get last date and interest rate
                    string dtr = Session["EntryDate"].ToString();
                    float intrate = rate + penalintrst;

                    //TxtPaybleAmount0.Text = (Convert.ToInt32(TxtPrincPaybl.Text.ToString()) + interest - SbIntrst).ToString();

                    // Monthly Interest payout
                    if (ddlIntrestPay.SelectedValue == "1")
                    {
                        CalRDintrestMnth(count, dt.ToString(), dtr, intrate);
                        return;
                    }

                    // Quartarly Interest Payout
                    if (ddlIntrestPay.SelectedValue == "2")
                    {
                        CalRDintrestQrtly(count, dt.ToString(), dtr, intrate);
                        return;
                    }
                    // TxtPaybleAmount0.Text = (Convert.ToInt32(TxtPrincPaybl.Text.ToString()) + interest + SbIntrst).ToString();
                }
                //else if (ViewState["RD"].ToString() == "NO")
                //{
                //  //  TxtPaybleAmount0.Text = (Convert.ToInt32(TxtPrincPaybl.Text.ToString()) + interest + SbIntrst).ToString();
                //}
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    // payment Voucher
    protected void BtnPayment_Click(object sender, EventArgs e)
    {
        try
        {
            //   string PCMAC = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.ToString();
            string PCMAC = conn.PCNAME();
            string Setno7 = CMN.GetSetNoAll(Session["BRCD"].ToString(), "7");
            string Setno4 = CMN.GetSetNoAll(Session["BRCD"].ToString(), "4");
            string ScrollNo = "1";
            string particulars = "";
            int resultout = 0;
            string PAYMAST = "TDCLOSE";
            // Calculate days Difference
            DateTime depositdate = Convert.ToDateTime(dtDeposDate.Text);
            DateTime closuredate = DateTime.Now;

            if (ViewState["MATURITY"].ToString() == "PRE")
            {
                closuredate = Convert.ToDateTime(Session["EntryDate"].ToString());
                particulars = "Pre-Maturity Closure";
            }

            if (ViewState["MATURITY"].ToString() == "MA")
            {
                closuredate = Convert.ToDateTime(DtDueDate.Text);
                particulars = "Maturity Closure";
            }

            // get Fyear and Fmonths
            string[] arraydt = closuredate.ToString().Split('/');

            // Get SubGlCode and AccNo for GL 99
            dt = CMN.GetSubglAccNo("99", Session["BRCD"].ToString());

            // For Cash
            if (ddlPayType.SelectedValue == "1")
            {
                if (txtPayAmnt.Text.ToString() == "" || txtPayAmnt.Text.ToString() == "0")
                {
                    WebMsgBox.Show("Invalid Payable amount", this.Page);
                    return;
                }

                // get closing balance for interest
                double PaybleIntrest = OC.GetOpenClose("CLOSING", arraydt[2].ToString(), arraydt[1].ToString(), TxtProcode.Text.ToString(), txtAccNo.Text.ToString(), Session["BRCD"].ToString(), closuredate.ToString(), "10");
                decimal PaybleIntrest1 = (decimal)PaybleIntrest;
                // DEBIT Entry
                resultout = POSTV.Authorized(Session["EntryDAte"].ToString(), Session["EntryDAte"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString(),
                txtAccNo.Text.ToString(), particulars, "", TxtPrincPaybl.Text.ToString(), "2", "4", "CASH", Setno4, "", "", "", "", "1001",
                PAYMAST, Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "0", Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

                ScrollNo = (Convert.ToDecimal(ScrollNo) + 1).ToString();
                // DEDIT Entry calculated INTEREST            
                resultout = POSTV.Authorized(Session["EntryDAte"].ToString(), Session["EntryDAte"].ToString(), Session["EntryDate"].ToString(), "10", TxtProcode.Text.ToString(),
                txtAccNo.Text.ToString(), "FD interest", "", PaybleIntrest.ToString(), "2", "4", "CASH", Setno4, "", "", "", "", "1001",
                PAYMAST, Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "0", Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

                // DEBIT Entry SB interest
                if (Convert.ToInt32(TxtSbintrest.Text) != 0)
                {
                    ScrollNo = (Convert.ToDecimal(ScrollNo) + 1).ToString();
                    resultout = POSTV.Authorized(Session["EntryDAte"].ToString(), Session["EntryDAte"].ToString(), Session["EntryDate"].ToString(), "10", TxtProcode.Text.ToString(),
                    txtAccNo.Text.ToString(), "SB interest", "", TxtSbintrest.Text.ToString(), "2", "4", "CASH", Setno4, "", "", "", "", "1001",
                    PAYMAST, Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "0", Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                }

                ScrollNo = (Convert.ToDecimal(ScrollNo) + 1).ToString();
                // CREDIT Entry Payable amount
                resultout = POSTV.Authorized(Session["EntryDAte"].ToString(), Session["EntryDAte"].ToString(), Session["EntryDate"].ToString(), "99",
                dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), "CASH", "", txtPayAmnt.Text.ToString(), "1", "4", "CASH", Setno4, "", "", "", "", "1001",
                PAYMAST, Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "0", Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

                if (resultout > 0)
                {
                    CMN.GetSetNo_All("4", "4", Session["BRCD"].ToString());
                    WebMsgBox.Show(" Successfully saved with Set No -  " + Setno4, this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Deposit_CloserAmt_" + TxtProcode + "_" + txtAccNo + "_" + txtPayAmnt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;
                }
            }

            // For Transfer
            if (ddlPayType.SelectedValue == "2")
            {
                if (txtPayAmnt.Text.ToString() == "" || txtPayAmnt.Text.ToString() == "0")
                {
                    WebMsgBox.Show("Invalid Payable amount", this.Page);
                    return;
                }

                if (Txtprocode1.Text.ToString() == "")
                {
                    WebMsgBox.Show("Invalid subglcode", this.Page);
                    Txtprocode1.Focus();
                    return;
                }

                if (TxtAccNo1.Text.ToString() == "")
                {
                    WebMsgBox.Show("Invalid Account number", this.Page);
                    TxtAccNo1.Focus();
                    return;
                }

                // CREDIT
                resultout = POSTV.Authorized(Session["EntryDAte"].ToString(), Session["EntryDAte"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(),
                TxtProcode.Text.ToString(), txtAccNo.Text.ToString(), particulars, "", txtPayAmnt.Text.ToString(), "1", "7", "TRANSFER", Setno7, "", "", "", "", "1001",
                PAYMAST, Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "0", Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

                // DEBIT
                resultout = POSTV.Authorized(Session["EntryDAte"].ToString(), Session["EntryDAte"].ToString(), Session["EntryDate"].ToString(), "99",
                Txtprocode1.Text.ToString(), TxtAccNo1.Text.ToString(), particulars, "", txtPayAmnt.Text.ToString(), "2", "7", "TRANSFER", Setno7, "", "", "", "", "1001",
                PAYMAST, Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "0", Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

                if (resultout > 0)
                {
                    WebMsgBox.Show("Successfully saved", this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Deposit_Closer_transfer_" + TxtProcode + "_" + txtAccNo + "_" + txtPayAmnt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }
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
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DepCloser_LoanClose_Rpt_" + TxtProcode + "_" + txtAccNo + "_" + txtPayAmnt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&SubGlCode=" + TxtProcode.Text + "&Accno=" + txtAccNo.Text + "&rptname=RptFDPrinting.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmBlank.aspx");
    }

    protected void ClearData_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void btnPayLoan_Click(object sender, EventArgs e)
    {
        try
        {
            if (Txtprocode1.Text.ToString() == "")
            {
                Txtprocode1.Text = "";
                Txtprocode1.Focus();
                lblMessage.Text = "Enter loan product code first...!!";
                ModalPopup.Show(this.Page);
                return;
            }

            if (TxtAccNo1.Text.ToString() == "")
            {
                TxtAccNo1.Text = "";
                Txtprocode1.Focus();
                lblMessage.Text = "Enter loan account number first...!!";
                ModalPopup.Show(this.Page);
                return;
            }

            //if (Convert.ToDouble(txtLoanTotAmt.Text.Trim().ToString() == "" ? "0" : txtLoanTotAmt.Text.Trim().ToString()) < Convert.ToDouble(ViewState["LoanTotAmt"].ToString()))
            //{
            //    txtLoanTotAmt.Text = ViewState["LoanTotAmt"].ToString();
            //    lblMessage.Text = "Amount of receipt is less than paid amount " + Convert.ToDouble(ViewState["LoanTotAmt"].ToString()).ToString() + "...!!";
            //    ModalPopup.Show(this.Page);
            //    return;
            //}
            if (Convert.ToDouble(txtLoanTotAmt.Text.Trim().ToString() == "" ? "0" : txtLoanTotAmt.Text.Trim().ToString()) > Convert.ToDouble(ViewState["LoanTotAmt"].ToString()))
            {
                txtLoanTotAmt.Text = ViewState["LoanTotAmt"].ToString();
                lblMessage.Text = "Excess amount of receipt than paid amount " + Convert.ToDouble(ViewState["LoanTotAmt"].ToString()).ToString() + "...!!";
                ModalPopup.Show(this.Page);
                return;
            }
            else if (Convert.ToDouble(txtLoanTotAmt.Text.Trim().ToString()) > Convert.ToDouble(txtPayAmnt.Text.Trim().ToString() == "" ? "0" : txtPayAmnt.Text.Trim().ToString()))
            {
                txtLoanTotAmt.Text = ViewState["LoanTotAmt"].ToString();
                lblMessage.Text = "Excess amount of receipt than total payable amount " + Convert.ToDouble(txtPayAmnt.Text.Trim().ToString() == "" ? "0" : txtPayAmnt.Text.Trim().ToString()).ToString() + "...!!";
                ModalPopup.Show(this.Page);
                return;
            }

            if (Convert.ToDouble(txtLoanTotAmt.Text.Trim().ToString() == "" ? "0" : txtLoanTotAmt.Text.Trim().ToString()) > 0)
            {
                dt = new DataTable();
                if (ddlPayType.SelectedValue == "5" || ddlPayType.SelectedValue == "6")
                {
                    dt = new DataTable();

                    if (ddlPayType.SelectedValue == "5")
                    {
                        dt = CurrentCls1.GetLoanTotalAmount(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Session["EntryDate"].ToString(), "LoanInst");
                    }
                    if (ddlPayType.SelectedValue == "6")
                    {
                        dt = CurrentCls1.GetLoanTotalAmount(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Session["EntryDate"].ToString(), "LoanClose");
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Acc_Status"].ToString() == "3")
                    {
                        lblMessage.Text = "Account is already closed...!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                    else if (dt.Rows[0]["Acc_Status"].ToString() == "9")
                    {
                        lblMessage.Text = "Close account in loan Installment...!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                    else if (dt.Rows[0]["Acc_Status"].ToString() == "1")
                    {
                        resultout = 1;
                        CN = "0";
                        CD = "1900-01-01";

                        //amol
                        TotalAmt = Math.Round(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) + Convert.ToDouble(dt.Rows[0]["Interest"].ToString() == "" ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["PInterest"].ToString() == "" ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString() == "" ? "0" : dt.Rows[0]["InterestRec"].ToString()) + Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString())));
                        TotalDrAmt = Convert.ToDouble(txtLoanTotAmt.Text.Trim().ToString() == "" ? "0" : txtLoanTotAmt.Text.Trim().ToString());

                        //  For Insurance Charge
                        if (resultout > 0)
                        {
                            if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()))
                            {
                                resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["InsChrgGl"].ToString(), dt.Rows[0]["InsChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "INSCR", Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()), "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()) > 0)
                                    {
                                        //  Insurance Charge Credit To 11 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["InsChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "11", "1", "7", "Insurance Charge Credit", dt.Rows[0]["InsChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }
                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()));
                            }
                            else if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt > 0)
                            {
                                resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["InsChrgGl"].ToString(), dt.Rows[0]["InsChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "INSCR", TotalDrAmt, "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()) > 0)
                                    {
                                        //  Insurance Charge Credit To 11 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["InsChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "11", "1", "7", "Insurance Charge Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }
                                TotalDrAmt = 0;
                            }
                        }

                        //  For Bank Charges
                        if (resultout > 0)
                        {
                            if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()))
                            {
                                resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["BankChrgGl"].ToString(), dt.Rows[0]["BankChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "BNKCR", Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()), "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) > 0)
                                    {
                                        // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["BankChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "10", "1", "7", "Bank Charges Credit", dt.Rows[0]["BankChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }
                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()));
                            }
                            else if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) > 0 && TotalDrAmt > 0 && TotalDrAmt > 0)
                            {
                                resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["BankChrgGl"].ToString(), dt.Rows[0]["BankChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "BNKCR", TotalDrAmt, "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) > 0)
                                    {
                                        // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["BankChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "10", "1", "7", "Bank Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }
                                TotalDrAmt = 0;
                            }
                        }

                        //  For Other Charges
                        if (resultout > 0)
                        {
                            if (Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()))
                            {
                                resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["OtherChrgGl"].ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "OTHCR", Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()), "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) > 0)
                                    {
                                        // Other Charges Amt Credit To 9 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "9", "1", "7", "Other Charges Credit", dt.Rows[0]["OtherChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }
                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()));
                            }
                            else if (Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) > 0 && TotalDrAmt > 0)
                            {
                                resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["OtherChrgGl"].ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "OTHCR", TotalDrAmt, "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) > 0)
                                    {
                                        // Other Charges Amt Credit To 9 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "9", "1", "7", "Other Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }
                                TotalDrAmt = 0;
                            }
                        }

                        //  For Sur Charges
                        if (resultout > 0)
                        {
                            if (Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()))
                            {
                                resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["SurChrgGl"].ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "SURCR", Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()), "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()) > 0)
                                    {
                                        // Sur Charges Credit To 8 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "8", "1", "7", "Sur Charges Credit", dt.Rows[0]["SurChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }
                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()));
                            }
                            else if (Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()) > 0 && TotalDrAmt > 0)
                            {
                                resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["SurChrgGl"].ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "SURCR", TotalDrAmt, "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()) > 0)
                                    {
                                        // Sur Charges Credit To 8 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "8", "1", "7", "Sur Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }
                                TotalDrAmt = 0;
                            }
                        }

                        // For Court Charges
                        if (resultout > 0)
                        {
                            if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()))
                            {
                                resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["CourtChrgGl"].ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "CRTCR", Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()), "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) > 0)
                                    {
                                        // Court Charges Amt Credit To 7 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "7", "1", "7", "Court Charges Credit", dt.Rows[0]["CourtChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }
                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()));
                            }
                            else if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) > 0 && TotalDrAmt > 0)
                            {
                                resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["CourtChrgGl"].ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "CRTCR", TotalDrAmt, "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) > 0)
                                    {
                                        // Court Charges Amt Credit To 7 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "7", "1", "7", "Court Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }
                                TotalDrAmt = 0;
                            }
                        }

                        //  For Service Charges
                        if (resultout > 0)
                        {
                            if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()))
                            {
                                resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["ServiceChrgGl"].ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "SERCR", Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()), "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) > 0)
                                    {
                                        // Service Charges Amt Credit To 6 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "6", "1", "7", "Service Charges Credit", dt.Rows[0]["ServiceChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }
                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()));
                            }
                            else if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) > 0 && TotalDrAmt > 0)
                            {
                                resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["ServiceChrgGl"].ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "SERCR", TotalDrAmt, "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) > 0)
                                    {
                                        // Service Charges Amt Credit To 6 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "6", "1", "7", "Service Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }
                                TotalDrAmt = 0;
                            }
                        }

                        //  For Notice Charges
                        if (resultout > 0)
                        {
                            if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()))
                            {
                                resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["NoticeChrgGl"].ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "NOTCR", Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()), "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) > 0)
                                    {
                                        // Notice Charges Credit To 5 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "5", "1", "7", "Notice Charges Credit", dt.Rows[0]["NoticeChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }
                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()));
                            }
                            else if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) > 0 && TotalDrAmt > 0)
                            {
                                resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["NoticeChrgGl"].ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "NOTCR", TotalDrAmt, "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) > 0)
                                    {
                                        // Notice Charges Credit To 5 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "5", "1", "7", "Notice Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }
                                TotalDrAmt = 0;
                            }
                        }

                        //  For Interest Receivable
                        if (resultout > 0)
                        {
                            if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()))
                            {
                                if (dt.Rows[0]["IntCalType"].ToString() == "1" || dt.Rows[0]["IntCalType"].ToString() == "3")
                                {
                                    // Interest Received credit to GL 11
                                    resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["InterestRecGl"].ToString(), dt.Rows[0]["InterestRecSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "INTRCR", Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()), "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0)
                                        {
                                            // Interest Received Amt Credit To 4 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["InterestRecSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "4", "1", "7", "Interest Received Credit", Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }
                                }
                                else if (dt.Rows[0]["IntCalType"].ToString() == "2")
                                {
                                    // Interest Received credit to GL 3
                                    resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode1"].ToString(), Txtprocode1.Text, TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "INTRCR", Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()), "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");
                                }
                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()));
                            }
                            else if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt > 0)
                            {
                                if (dt.Rows[0]["IntCalType"].ToString() == "1" || dt.Rows[0]["IntCalType"].ToString() == "3")
                                {
                                    // Interest Received credit to GL 11
                                    resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["InterestRecGl"].ToString(), dt.Rows[0]["InterestRecSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "INTRCR", TotalDrAmt, "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0)
                                        {
                                            // Interest Received Amt Credit To 4 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["InterestRecSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "4", "1", "7", "Interest Received Credit", Convert.ToDouble(TotalDrAmt.ToString()).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }
                                }
                                else if (dt.Rows[0]["IntCalType"].ToString() == "2")
                                {
                                    // Interest Received credit to GL 3
                                    resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode1"].ToString(), Txtprocode1.Text, TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "INTRCR", TotalDrAmt, "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");
                                }
                                TotalDrAmt = 0;
                            }
                        }

                        //  For Penal Charge
                        if (resultout > 0)
                        {
                            if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())))
                            {
                                if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                {
                                    //Penal Charge Credit To GL 12
                                    resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "PENCR", Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()), "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - (Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())));
                                }

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) > 0)
                                    {
                                        //Penal Interest Debit To 3 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "3", "2", "7", "Penal Interest Debit", dt.Rows[0]["CurrPInterest"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                    {
                                        //Penal Interest Credit To 3 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "3", "1", "7", "Penal Interest Credit", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }

                                if (resultout > 0)
                                {
                                    //Penal Charge Contra
                                    if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                    {
                                        //Penal chrg Applied Debit To GL 12
                                        resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "PENDR", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())), "2", "12", "TR_INT", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                        if (resultout > 0)
                                        {
                                            //Penal chrg Applied Credit to GL 100
                                            resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", dt.Rows[0]["PlAccNo2"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "PENCR", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())), "1", "12", "TR_INT", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");
                                        }
                                    }
                                }
                            }
                            else if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0 && TotalDrAmt > 0)
                            {
                                //Penal Charge Credit To GL 12
                                resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "PENCR", TotalDrAmt, "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) > 0)
                                    {
                                        //Penal Interest Debit To 3 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "3", "2", "7", "Penal Interest Debit", dt.Rows[0]["CurrPInterest"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                    {
                                        //Penal Interest Credit To 3 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "3", "1", "7", "Penal Interest Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }

                                if (resultout > 0)
                                {
                                    //Penal Charge Contra
                                    if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                    {
                                        //Penal chrg Applied Debit To GL 12
                                        resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "PENDR", TotalDrAmt, "2", "12", "TR_INT", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                        if (resultout > 0)
                                        {
                                            //Penal chrg Applied Credit to GL 100
                                            resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", dt.Rows[0]["PlAccNo2"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "PENCR", TotalDrAmt, "1", "12", "TR_INT", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");
                                        }
                                    }
                                }
                                TotalDrAmt = 0;
                            }
                        }

                        //  For Interest
                        if (resultout > 0)
                        {
                            if (dt.Rows[0]["IntCalType"].ToString() == "1")
                            {
                                if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())))
                                {
                                    if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                    {
                                        //interest Credit to GL 11
                                        resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "INTCR", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())), "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - (Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())));
                                    }

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString()) > 0)
                                        {
                                            //Current Interest Debit To 2 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "2", "2", "7", "Interest Debit", dt.Rows[0]["CurrInterest"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                        {
                                            //Current Interest Credit To 2 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "2", "1", "7", "Interest Credit", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }

                                    if (resultout > 0)
                                    {
                                        //interest Applied Contra
                                        if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                        {
                                            //interest Applied Debit To GL 11
                                            resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "INTDR", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())), "2", "11", "TR_INT", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                            if (resultout > 0)
                                            {
                                                //interest Applied Credit to GL 100
                                                resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", dt.Rows[0]["PlAccNo1"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "INTCR", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())), "1", "11", "TR_INT", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");
                                            }
                                        }
                                    }
                                }
                                else if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                {
                                    //interest Credit to GL 11
                                    resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "INTCR", TotalDrAmt, "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString()) > 0)
                                        {
                                            //Current Interest Debit To 2 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "2", "2", "7", "Interest Debit", Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                        {
                                            //Current Interest Credit To 2 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "2", "1", "7", "Interest Credit", Convert.ToDouble(TotalDrAmt.ToString()).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }

                                    if (resultout > 0)
                                    {
                                        //interest Applied Contra
                                        if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                        {
                                            //interest Applied Debit To GL 11
                                            resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "INTDR", TotalDrAmt, "2", "11", "TR_INT", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                            if (resultout > 0)
                                            {
                                                //interest Applied Credit to GL 100
                                                resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", dt.Rows[0]["PlAccNo1"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "INTCR", TotalDrAmt, "1", "11", "TR_INT", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");
                                            }
                                        }
                                    }
                                    TotalDrAmt = 0;
                                }
                            }
                            else if (dt.Rows[0]["IntCalType"].ToString() == "2")
                            {
                                if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())))
                                {
                                    if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                    {
                                        //interest Received Credit to GL 3
                                        resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode1"].ToString(), Txtprocode1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "INTCR", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())), "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - (Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())));
                                    }

                                    //  Added As Per ambika mam Instruction 22-06-2017
                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                        {
                                            //interest Applied Debit To GL 3
                                            resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode1"].ToString(), Txtprocode1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "INTDR", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())), "2", "11", "TR_INT", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                            if (resultout > 0)
                                            {
                                                //interest Applied Credit to GL 100
                                                resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", dt.Rows[0]["PlAccNo1"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "INTCR", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())), "1", "11", "TR_INT", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");
                                            }
                                        }
                                    }
                                }
                                else if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                {
                                    //interest Received Credit to GL 3
                                    resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode1"].ToString(), Txtprocode1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "INTCR", TotalDrAmt, "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                    //  Added As Per ambika mam Instruction 22-06-2017
                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                        {
                                            //interest Applied Debit To GL 3
                                            resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode1"].ToString(), Txtprocode1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "INTDR", TotalDrAmt, "2", "11", "TR_INT", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                            if (resultout > 0)
                                            {
                                                //interest Applied Credit to GL 100
                                                resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", dt.Rows[0]["PlAccNo1"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "INTCR", TotalDrAmt, "1", "11", "TR_INT", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");
                                            }
                                        }
                                    }
                                    TotalDrAmt = 0;
                                }
                            }
                            if (dt.Rows[0]["IntCalType"].ToString() == "3")
                            {
                                if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())))
                                {
                                    if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                    {
                                        //interest Credit to GL 11
                                        resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "INTCR", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())), "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - (Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())));
                                    }

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString()) > 0)
                                        {
                                            //Current Interest Debit To 2 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "2", "2", "7", "Interest Debit", dt.Rows[0]["CurrInterest"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                        {
                                            //Current Interest Credit To 2 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "2", "1", "7", "Interest Credit", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }
                                }
                                else if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                {
                                    //interest Credit to GL 11
                                    resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "INTCR", TotalDrAmt, "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString()) > 0)
                                        {
                                            //Current Interest Debit To 2 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "2", "2", "7", "Interest Debit", Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                        {
                                            //Current Interest Credit To 2 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo1.Text.Trim().ToString(), "2", "1", "7", "Interest Credit", Convert.ToDouble(TotalDrAmt.ToString()).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }
                                }
                            }
                        }

                        //Principle O/S Credit To Specific GL (e.g 3)
                        if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()))
                        {
                            resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode1"].ToString(), Txtprocode1.Text, TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "PRNCR", Convert.ToDouble(dt.Rows[0]["Principle"].ToString()), "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                            if (resultout > 0)
                            {
                                if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0)
                                {
                                    //Current Principle Debit To 1 In AVS_LnTrx
                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), Txtprocode1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), "1", "2", "7", "Principle Debit", dt.Rows[0]["Principle"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                }
                            }

                            if (resultout > 0)
                            {
                                if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0)
                                {
                                    //Current Principle Credit To 1 In AVS_LnTrx
                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), Txtprocode1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), "1", "1", "7", "Principle Credit", dt.Rows[0]["Principle"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                }
                            }

                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["Principle"].ToString()));
                        }
                        else if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0 && TotalDrAmt > 0)
                        {
                            resultout = ITrans.TempSaction(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode1"].ToString(), Txtprocode1.Text, TxtAccNo1.Text.Trim().ToString(),
                                     "BY TRF", "PRNCR", TotalDrAmt, "1", "7", "TR", "0", "0", "01/01/1900", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", ViewState["CustName"].ToString(), TxtCustName1.Text.ToString(), "0");

                            if (resultout > 0)
                            {
                                if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0)
                                {
                                    //Current Principle Debit To 1 In AVS_LnTrx
                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), Txtprocode1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), "1", "2", "7", "Principle Debit", dt.Rows[0]["Principle"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                }
                            }

                            if (resultout > 0)
                            {
                                if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0)
                                {
                                    //Current Principle Credit To 1 In AVS_LnTrx
                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), Txtprocode1.Text.Trim().ToString(), Txtprocode1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), "1", "1", "7", "Principle Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                }
                            }

                            TotalDrAmt = 0;
                        }

                        if (resultout > 0)
                        {
                            ViewState["LoanAmt"] = Convert.ToDouble(txtLoanTotAmt.Text.Trim().ToString() == "" ? "0" : txtLoanTotAmt.Text.Trim().ToString()).ToString();
                            txtPayAmnt.Text = Convert.ToDouble(Convert.ToDouble(txtPayAmnt.Text.Trim().ToString()) - Convert.ToDouble(txtLoanTotAmt.Text.Trim().ToString())).ToString();

                            if (Convert.ToDouble(txtPayAmnt.Text.Trim().ToString()) == 0.00)
                            {
                                BtnSubmit.Text = "Post Voucher";
                            }
                            lblMessage.Text = "Amount Transfer to Loan Account Successfully...!!";
                            ModalPopup.Show(this.Page);
                            FL = "Insert";//Dhanya Shetty
                            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Deposit_Closer_Amt_Loan" + TxtProcode + "_" + txtAccNo + "_" + txtPayAmnt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            ClearAll();
                            Txtprocode1.Focus();
                            return;
                        }
                    }
                }
            }
            else
            {
                txtLoanTotAmt.Text = ViewState["LoanTotAmt"].ToString();
                lblMessage.Text = "Enter amount first...!!";
                ModalPopup.Show(this.Page);
                txtLoanTotAmt.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Functions

    public void STPay()
    {
        string script = "ShowTotalPayable();";

        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("myPostBackScript"))
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myPostBackScript", script.ToString(), true);
            //this.Page.ClientScript.RegisterClientScriptBlock(this,this.GetType(),"myPostBackScript", script, true);
        }
    }

    public void EnableDisableInt()
    {
        try
        {
            string ParaV = CMN.GetTDINT();
            if (ParaV != null && ParaV == "Y")
            {
                string UG = CMN.GetTDUG();
                if (UG == "0")
                {
                    TxtInterestNew.Enabled = true;
                }
                else if (Session["UGRP"].ToString() == UG)
                {
                    TxtInterestNew.Enabled = true;
                }
                else
                {
                    TxtInterestNew.Enabled = true;
                }
            }
            else
            {
                TxtInterestNew.Enabled = false;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    public void productcd()
    {
        try
        {
            //Added By Amol on 22092017 as per ambika mam instruction
            if (BD.GetProdOperate(Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString()).ToString() != "3")
            {
                if (TxtProcode.Text != "")
                {
                    string CAT = MT.GetDepositCat(Session["BRCD"].ToString(), TxtProcode.Text, "MISTRF");
                    ViewState["CAT"] = string.IsNullOrEmpty(CAT) ? "0" : CAT;
                    if (CAT != "MIS" && Rdb_IntPay.Checked == true)
                    {
                        WebMsgBox.Show("Interest Pay facility is only for MIS group," + TxtProcode.Text + " is not MIS!...", this.Page);
                        clear();
                        return;
                    }
                }
                string GLL = BD.GetAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString());
                if (GLL != null)
                {
                    string[] GLLL = GLL.Split('_');
                    ViewState["GLCODE"] = GLLL[1].ToString();
                    if (ViewState["GLCODE"].ToString() == "15")
                    {
                        Invisi(true);
                    }
                }

                string nm = BD.GetTDAAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString(), ViewState["GLCODE"].ToString());
                if (nm != null)
                {
                    string[] GLT = nm.Split('_');
                    ViewState["DRGL"] = GLT[1].ToString();
                    ViewState["IR"] = GLT[2].ToString();
                    TxtProName.Text = GLT[0].ToString();
                    AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();
                    if (TxtProName.Text == "")
                    {
                        WebMsgBox.Show("Invalid product code", this.Page);
                        clear();
                        TxtProcode.Focus();
                        return;
                    }
                    else
                    {
                        // Check RD or LKP

                        string IsRD = CMN.CheckRD(TxtProcode.Text.ToString(), Session["BRCD"].ToString());
                        if (IsRD == null)
                        {
                            ViewState["RD"] = "NO";
                            string CATLKP = MT.GetDepositCat(Session["BRCD"].ToString(), TxtProcode.Text, "MISTRF");
                            if (CATLKP == "LKPRD")
                            {
                                string LKPRD = CMN.CheckLKPLikeRD("1");
                                if (LKPRD == "Y")
                                {
                                    ViewState["LKP"] = "YES";
                                }
                                else
                                {
                                    ViewState["LKP"] = "NO";
                                }
                            }
                            else
                            {
                                ViewState["LKP"] = "NO";
                            }
                        }
                        else
                        {
                            ViewState["RD"] = "YES";
                            ViewState["LKP"] = "NO";
                        }

                    }
                    txtAccNo.Focus();
                }
                else
                {
                    WebMsgBox.Show("Enter Valid Product Code!....", this.Page);
                    TxtProcode.Text = "";
                    TxtProcode.Focus();

                }
            }
            else
            {
                TxtProcode.Text = "";
                TxtProName.Text = "";
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

    public void accno()
    {
        try
        {
            string AT = "";

            if (ViewState["CAT"].ToString() == "DP")
            {
                AC_Status = CMN.GetAccStatus(Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text);
                if (AC_Status == "1" || AC_Status == "3" || AC_Status == "4")
                {
                    if (AC_Status == "3")
                    {
                        WebMsgBox.Show("Account is closed....!", this.Page);
                    }
                    AT = BD.Getstage1(txtAccNo.Text, Session["BRCD"].ToString(), TxtProcode.Text);
                    if (AT != "1003")
                    {
                        lblMessage.Text = "Sorry Customer not Authorise.........!!";
                        ModalPopup.Show(this.Page);
                        clear();
                    }
                    else
                    {
                        if (TxtProcode.Text != "" && txtAccNo.Text != "")
                        {
                            DataTable dtmodal = new DataTable();
                            dtmodal = CR.GetInfoTbl(Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), txtAccNo.Text, TxtProcode.Text);
                            if (dtmodal.Rows.Count > 0)
                            {
                                resultout = CR.GetInfo(GrdView, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), txtAccNo.Text, TxtProcode.Text);
                                if (resultout > 0)
                                {
                                    string Modal_Flag = "VOUCHERVIEW";
                                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                                    sb.Append(@"<script type='text/javascript'>");
                                    sb.Append("$('#" + Modal_Flag + "').modal('show');");
                                    sb.Append(@"</script>");

                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                                }
                            }
                        }

                        CheckData_DP();
                        STPay();
                        Photo_Sign();
                        RdbClose.Focus();
                        if (AC_Status == "4")
                        {
                            ////Added by ankita on 19/06/2017 to display Lean details
                            dt = DDS.getDetails(Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                lblMessage.Text = "Acc number " + txtAccNo.Text + " is Lean Marked / Loan Advanced .........!! with LoanGlCode=" + dt.Rows[0]["LOANGLCODE"].ToString() + " and LoanAccNo=" + dt.Rows[0]["LOANACCNO"].ToString() + " and Loan Amount=" + dt.Rows[0]["LOANAMT"].ToString() + "";
                                GetLoanDetails(dt.Rows[0]["LOANGLCODE"].ToString(), dt.Rows[0]["LOANACCNO"].ToString());
                                ModalPopup.Show(this.Page);
                            }
                            else
                            {
                                WebMsgBox.Show("Account is Lean Marked, Lien details not found...", this.Page);
                            }
                        }
                    }
                }
            }
            else
            {
                AC_Status = CMN.GetAccStatus(Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text);
                if (AC_Status == "1" || AC_Status == "3" || AC_Status == "4")
                {
                    if (AC_Status == "3")
                    {
                        WebMsgBox.Show("Account is closed....!", this.Page);
                    }
                    AT = BD.Getstage1(txtAccNo.Text, Session["BRCD"].ToString(), TxtProcode.Text);
                    if (AT != "1003")
                    {
                        lblMessage.Text = "Sorry account not authorise ...!!";
                        ModalPopup.Show(this.Page);
                        clear();
                    }
                    else
                    {
                        if (TxtProcode.Text != "" && txtAccNo.Text != "")
                        {
                            DataTable dtmodal = new DataTable();
                            dtmodal = CR.GetInfoTbl(Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), txtAccNo.Text, TxtProcode.Text);
                            if (dtmodal.Rows.Count > 0)
                            {
                                resultout = CR.GetInfo(GrdView, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), txtAccNo.Text, TxtProcode.Text);
                                if (resultout > 0)
                                {
                                    string Modal_Flag = "VOUCHERVIEW";
                                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                                    sb.Append(@"<script type='text/javascript'>");
                                    sb.Append("$('#" + Modal_Flag + "').modal('show');");
                                    sb.Append(@"</script>");

                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                                }
                            }
                        }
                        CheckData();
                        STPay();

                        if (ViewState["GLCODE"].ToString() != null && ViewState["GLCODE"].ToString() == "15" && ViewState["MATURITY"].ToString() == "PRE")
                        {
                            AddCommision();
                        }

                        Photo_Sign();
                        RdbClose.Focus();
                        if (AC_Status == "4")
                        {
                            ////Added by ankita on 19/06/2017 to display Lean details
                            dt = DDS.getDetails(Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                lblMessage.Text = "Acc number " + txtAccNo.Text + " is Lean Marked / Loan Advanced .........!! with LoanGlCode=" + dt.Rows[0]["LOANGLCODE"].ToString() + " and LoanAccNo=" + dt.Rows[0]["LOANACCNO"].ToString() + " and Loan Amount=" + dt.Rows[0]["LOANAMT"].ToString() + "";
                                GetLoanDetails(dt.Rows[0]["LOANGLCODE"].ToString(), dt.Rows[0]["LOANACCNO"].ToString());
                                ModalPopup.Show(this.Page);
                            }
                            else
                            {
                                WebMsgBox.Show("Account is Lean Marked, Lien details not found...", this.Page);
                            }
                        }
                    }
                    ShowSBInt();

                }
                else if (AC_Status == "2")
                {
                    lblMessage.Text = "Acc number " + txtAccNo.Text + " is In-operative.........!!";
                    ModalPopup.Show(this.Page);
                    clear();
                }
                else if (AC_Status == "3")
                {
                    lblMessage.Text = "Acc number " + txtAccNo.Text + " is Closed.........!!";
                    ModalPopup.Show(this.Page);
                    clear();
                }
                else if (AC_Status == "4")
                {
                    ////Added by ankita on 19/06/2017 to display Lean details
                    dt = DDS.getDetails(Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text);
                    if (dt != null)
                    {
                        lblMessage.Text = "Acc number " + txtAccNo.Text + " is Lean Marked / Loan Advanced .........!! with LoanGlCode=" + dt.Rows[0]["LOANGLCODE"].ToString() + " and LoanAccNo=" + dt.Rows[0]["LOANACCNO"].ToString() + " and Loan Amount=" + dt.Rows[0]["LOANAMT"].ToString() + "";
                        // GetLoanDetails(dt.Rows[0]["LOANGLCODE"].ToString(), dt.Rows[0]["LOANACCNO"].ToString());
                        ModalPopup.Show(this.Page);
                    }
                    else
                    {
                        WebMsgBox.Show("Account is Lean Marked, Lien details not found...", this.Page);
                    }
                    clear();
                }
                else if (AC_Status == "5")
                {
                    lblMessage.Text = "Acc number " + txtAccNo.Text + " is Credit Freezed.........!!";
                    ModalPopup.Show(this.Page);
                    clear();
                }
                else if (AC_Status == "6")
                {
                    lblMessage.Text = "Acc number " + txtAccNo.Text + " is Debit Freezed.........!!";
                    ModalPopup.Show(this.Page);
                    clear();
                }
                else if (AC_Status == "7")
                {
                    lblMessage.Text = "Acc number " + txtAccNo.Text + " is Total Freezed.........!!";
                    ModalPopup.Show(this.Page);
                    clear();
                }
                else
                {
                    WebMsgBox.Show("Enter Valid Account number!...", this.Page);
                    txtAccNo.Text = "";
                    txtAccNo.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void EnableCompo(bool TF)
    {
        RdbClose.Enabled = TF;
        RdbRenew.Enabled = TF;
        RdbTrfInt.Enabled = TF;
        RdbMultipleClose.Enabled = !TF;
    }
    //public void BindGrid()
    //{
    //    try
    //    {
    //        string FDT = "";
    //        string[] DTF;

    //        DTF = dtDeposDate.Text.ToString().Split('/');
    //        FDT = dtDeposDate.Text;
    //        string[] DTT = Session["EntryDate"].ToString().Split('/');

    //        DataTable DT = new DataTable();
    //        DT = AST.FDAccountStatment(DTF[1].ToString(), DTT[1].ToString(), DTF[2].ToString(), DTT[2].ToString(), FDT, Session["EntryDate"].ToString(), txtAccNo.Text, TxtProcode.Text, Session["MID"].ToString(), Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), ViewState["IR"].ToString());
    //        if (DT.Rows.Count > 0)
    //        {
    //            GrdFDLedger.DataSource = DT;
    //            GrdFDLedger.DataBind();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //        //Response.Redirect("FrmLogin.aspx", true);
    //    }
    //}

    // Clear Form
    public void clear()
    {
        TxtProcode.Text = "";
        TxtProName.Text = "";
        txtAccNo.Text = "";
        Txtcustno.Text = "";
        TxtAccname.Text = "";
        dtDeposDate.Text = "";
        TxtDepoAmt.Text = "";
        ddlduration.SelectedIndex = 0;
        TxtPeriod.Text = "";
        ddlIntrestPay.SelectedIndex = -1;
        TxtRate.Text = "";
        TxtIntrest.Text = "0";
        TxtMaturity.Text = "";
        DtDueDate.Text = "";
        TxtPrincPaybl.Text = "0";
        TxtIntrestPaybl.Text = "0";
        TxtInterestNew.Text = "0";
        TxtSbintrest.Text = "0";
        txt1.Text = "0";
        txtPayAmnt.Text = "0";
        ddlPayType.SelectedIndex = -1;
        TxtOpenClose.Text = "";
        Txtprocode1.Text = "";
        Txtglcode.Text = "";
        TxtAccNo1.Text = "";
        TxtCustName1.Text = "";
        TxtChequeNo.Text = "";
        TxtChequeDate.Text = "";
        txtSancAmount.Text = "";
        txtLoanBal.Text = "";
        txtLastIntDate.Text = "";
        txtDays.Text = "";
        txtLoanInt.Text = "";
        TxtAdminCharges.Text = "0";
        TxtCommission.Text = "0";
        txtLastIntDate.Text = "";
        TxtIntPaid.Text = "";
        Transfer.Visible = false;
        btnLoanInt.Visible = false;
        //GrdFDLedger.DataSource = null;
        //GrdFDLedger.DataBind();
    }

    public void ClearLoanInfo()
    {
        try
        {
            txtSancAmount.Text = "";
            txtLoanBal.Text = "";
            txtLastIntDate.Text = "";
            txtDays.Text = "";
            txtLoanInt.Text = "";
            txtTotLoanBal.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Font.Bold = true;
        //}
    }

    protected void ClearAll()
    {
        try
        {
            ClearPayInfo();
            ddlPayType.SelectedIndex = 0;
            Transfer.Visible = false;
            Transfer2.Visible = false;
            divLoan.Visible = false;
            ddlPayType.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        finally
        {
            Txtprocode1.Focus();
        }
    }

    public void Invisi(bool TF)
    {
        //Lbl_AdminChr.Visible = TF;
        Lbl_Commi.Visible = TF;
        //TxtAdminCharges.Visible = TF;
        TxtCommission.Visible = TF;
        TxtCROI.Visible = TF;
    }

    public void GetLoanDetails(string Subgl, string Accno)
    {
        try
        {
            dt = new DataTable();
            dt = MV.GetLoanTotalAmount(Session["BRCD"].ToString(), Subgl.Trim().ToString(), Accno.Trim().ToString(), Session["EntryDate"].ToString(), "");
            //dt = CurrentCls1.GetLoanDetails(Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["LGlCode"] = dt.Rows[0]["GlCode"].ToString() == "" ? "0" : dt.Rows[0]["GlCode"].ToString();
                ViewState["LSubGlCode"] = dt.Rows[0]["SubGlCode"].ToString() == "" ? "0" : dt.Rows[0]["SubGlCode"].ToString();
                ViewState["LAccNo"] = dt.Rows[0]["AccountNo"].ToString() == "" ? "0" : dt.Rows[0]["AccountNo"].ToString();

                txtSancAmount.Text = dt.Rows[0]["Limit"].ToString() == "" ? "0" : dt.Rows[0]["Limit"].ToString();
                txtLoanBal.Text = dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString();
                txtLastIntDate.Text = dt.Rows[0]["LastIntdate"].ToString() == "" ? "" : dt.Rows[0]["LastIntdate"].ToString().Substring(0, 10);
                txtDays.Text = dt.Rows[0]["Days"].ToString() == "" ? "0" : dt.Rows[0]["Days"].ToString();
                txtLoanInt.Text = dt.Rows[0]["Interest"].ToString() == "" ? "0" : dt.Rows[0]["Interest"].ToString();
                txtLoanInt.Text = (Convert.ToDouble(txtLoanInt.Text) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString())).ToString();
                txtTotLoanBal.Text = (Convert.ToDouble(txtLoanBal.Text) + Convert.ToDouble(txtLoanInt.Text)).ToString();
                btnLoanInt.Visible = true;
            }
            else
            {
                txtSancAmount.Text = "0";
                txtLoanBal.Text = "0";
                txtLastIntDate.Text = "";
                txtDays.Text = "0";
                txtLoanInt.Text = "0";
                txtTotLoanBal.Text = "0";
                btnLoanInt.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void AddCommision()
    {
        try
        {
            double TotalCommi = 0, ROI = 0, PriAmt = 0;

            dt = CurrentCls1.GetPLComm_Admin(Session["BRCD"].ToString(), TxtProcode.Text);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["PRECOMM_RATE"].ToString() != null)
                {
                    TxtCROI.Text = dt.Rows[0]["PRECOMM_RATE"].ToString() + " % ";
                    string ADM_CHG = CurrentCls1.GetAdminChg(ViewState["GLCODE"].ToString() == "15" ? ViewState["GLCODE"].ToString() : TxtProcode.Text);
                    TxtAdminCharges.Text = ADM_CHG.ToString();
                    ROI = Convert.ToDouble(dt.Rows[0]["PRECOMM_RATE"]);
                    PriAmt = Convert.ToDouble(TxtPrincPaybl.Text);
                    if (ViewState["GLCODE"].ToString() == "15")
                    {
                        string CR;
                        CR = CMN.CalCredit(Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text, Session["EntryDate"].ToString());
                        TotalCommi = Convert.ToDouble(CR) * ROI / 100;
                        TxtCommission.Text = Math.Round(TotalCommi, 0).ToString();
                    }

                }

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void AddCommision_Suraksha()
    {
        try
        {
            double TotalCommi = 0, ROI = 0, PriAmt = 0;

            dt = CurrentCls1.GetPLComm_Admin(Session["BRCD"].ToString(), TxtProcode.Text);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["PRECOMM_RATE"].ToString() != null)
                {
                    TxtCROI.Text = dt.Rows[0]["PRECOMM_RATE"].ToString() + " % ";
                    string ADM_CHG = CurrentCls1.GetAdminChg(TxtProcode.Text);
                    TxtAdminCharges.Text = ADM_CHG.ToString();

                }

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public string PanCard(string BRCD, string CustNo)
    {
        string PanNo = CR.GetPanNo(BRCD, CustNo);
        return PanNo;
    }

    public void CloseText(bool TF)
    {
        if (TF == true)
        {
            TxtOpenClose.ForeColor = System.Drawing.Color.Red;
            TxtMaturity.ForeColor = System.Drawing.Color.Red;
            TxtIntrest.ForeColor = System.Drawing.Color.Red;
            dtDeposDate.ForeColor = System.Drawing.Color.Red;
            DtDueDate.ForeColor = System.Drawing.Color.Red;
            ddlIntrestPay.ForeColor = System.Drawing.Color.Red;
            ddlduration.ForeColor = System.Drawing.Color.Red;
            TxtDepoAmt.ForeColor = System.Drawing.Color.Red;
            TxtRate.ForeColor = System.Drawing.Color.Red;
            TxtPrincPaybl.ForeColor = System.Drawing.Color.Red;
            TxtIntrestPaybl.ForeColor = System.Drawing.Color.Red;
            TxtAccname.ForeColor = System.Drawing.Color.Red;
            txtAccNo.ForeColor = System.Drawing.Color.Red;
            TxtProcode.ForeColor = System.Drawing.Color.Red;
            TxtProName.ForeColor = System.Drawing.Color.Red;
            Txtcustno.ForeColor = System.Drawing.Color.Red;

        }
        else
        {
            TxtOpenClose.ForeColor = System.Drawing.Color.Black;
            TxtMaturity.ForeColor = System.Drawing.Color.Black;
            TxtIntrest.ForeColor = System.Drawing.Color.Black;
            dtDeposDate.ForeColor = System.Drawing.Color.Black;
            DtDueDate.ForeColor = System.Drawing.Color.Black;
            ddlIntrestPay.ForeColor = System.Drawing.Color.Black;
            ddlduration.ForeColor = System.Drawing.Color.Black;
            TxtDepoAmt.ForeColor = System.Drawing.Color.Black;
            TxtRate.ForeColor = System.Drawing.Color.Black;
            TxtPrincPaybl.ForeColor = System.Drawing.Color.Black;
            TxtIntrestPaybl.ForeColor = System.Drawing.Color.Black;
            TxtAccname.ForeColor = System.Drawing.Color.Black;
            txtAccNo.ForeColor = System.Drawing.Color.Black;
            TxtProcode.ForeColor = System.Drawing.Color.Black;
            TxtProName.ForeColor = System.Drawing.Color.Black;
            Txtcustno.ForeColor = System.Drawing.Color.Black;
        }

    }

    public void CheckData_DP()
    {
        try
        {
            dt1 = CurrentCls1.GetAllFieldData_DP(TxtProcode.Text.ToString(), txtAccNo.Text.ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());

            if (dt1.Rows.Count > 0)
            {
                Txtcustno.Text = dt1.Rows[0]["CUSTNO"].ToString();
                TxtAccname.Text = dt1.Rows[0]["CUSTNAME"].ToString();
                txtAccNo.Text = dt1.Rows[0]["ACCNO"].ToString();
                txtPan.Text = PanCard(Session["BRCD"].ToString(), Txtcustno.Text);
                dtDeposDate.Text = Convert.ToDateTime(dt1.Rows[0]["OPENINGDATE"].ToString()).ToString("dd/MM/yyyy");
                // ddlIntrestPay.Text = dt1.Rows[0]["INTPAYOUT"].ToString();
                TxtDepoAmt.Text = dt1.Rows[0]["PRNAMT"].ToString();
                TxtPeriod.Text = dt1.Rows[0]["PERIOD"].ToString();

                txtMaturityCalByPara.Text = (Convert.ToDouble(TxtPeriod.Text) - Convert.ToDouble(CurrentCls.GetMaturityParaDetails(CurrentROI: Convert.ToDouble(TxtPeriod.Text)))).ToString();
                // ddlduration.SelectedValue = dt1.Rows[0]["PRDTYPE"].ToString();



                TxtIntrest.Text = Convert.ToInt32(dt1.Rows[0]["INTAMT"]).ToString();
                TxtMaturity.Text = Convert.ToInt32(dt1.Rows[0]["MATURITYAMT"]).ToString();
                DtDueDate.Text = Convert.ToDateTime(dt1.Rows[0]["DUEDATE"].ToString()).ToString("dd/MM/yyyy");
                TxtTDLastIntDt.Text = Convert.ToDateTime(dt1.Rows[0]["LASTINTDATE"]).ToString("dd/MM/yyyy");
                string LMSTS = dt1.Rows[0]["ACC_STATUS"].ToString();

                if (LMSTS == "1")
                    TxtOpenClose.Text = "Open";
                else if (LMSTS == "4")
                    TxtOpenClose.Text = "Lien";
                else if (LMSTS == "3")
                    TxtOpenClose.Text = "Close";

                if (LMSTS == "3")
                {
                    BtnSubmit.Enabled = false;
                    BtnCalcSbInt.Enabled = false;
                    CloseText(true);
                }
                else
                {
                    CloseText(false);
                    BtnSubmit.Enabled = true;
                    BtnCalcSbInt.Enabled = true;
                }


                ViewState["ACCT"] = dt1.Rows[0]["ACC_TYPE"].ToString();
                ViewState["OPRTYPE"] = dt1.Rows[0]["OPR_TYPE"].ToString();

                double rate;
                string daysDiff;
                daysDiff = (Convert.ToInt32(conn.GetDayDiff(dtDeposDate.Text, Session["EntryDate"].ToString())) / 30).ToString();

                rate = Convert.ToDouble(CurrentCls.GetInterestRateED("TDA", "", TxtProcode.Text, ViewState["ACCT"].ToString(), "M", daysDiff.ToString(), Session["EntryDate"].ToString(), rdbPreMature.Checked));
                if (rate != null)
                {

                    TxtRate.Text = rate.ToString();
                }

                int DF = Convert.ToInt32(conn.GetDayDiff(Session["EntryDate"].ToString(), DtDueDate.Text));

                if (DF <= 0 || ViewState["CAT"].ToString() == "DP")  // added for Group "DP" as per requirement 07-02-2018
                {

                    rdbMature.Checked = true;
                    ViewState["MATURITY"] = "MA";
                    RdbRenew.Enabled = true;
                }
                else
                {
                    rdbPreMature.Checked = true;
                    ViewState["MATURITY"] = "PRE";
                    RdbRenew.Enabled = false;
                }


                string[] arraydt = { "" };


                arraydt = Session["EntryDate"].ToString().Split('/');
                if (rdbMature.Checked == true && LMSTS != "3")
                {
                    Res = BD.GetFDSBINTStatus(Session["BRCD"].ToString());
                    if (Res == "Y")
                        GetSBINT();
                    else
                    {
                        TxtSbintrest.Text = "0";
                    }
                }


                CalculateDP_INT();


                if (LMSTS == "3")
                {
                    TxtInterestNew.Text = "0";
                    TxtSbintrest.Text = "0";
                }


                // Checking Interset is paid today or not
                if (Rdb_IntPay.Checked != true)
                {
                    Res = CurrentCls1.GetLastIntDate(Session["BRCD"].ToString(), Txtcustno.Text.Trim().ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());

                    if (LMSTS == "3")
                    {
                        BtnPostInt.Visible = false;
                    }
                    else if (Res == Session["Entrydate"].ToString())
                    {
                        BtnPostInt.Visible = false;
                    }
                    else
                    {
                        BtnPostInt.Visible = true;
                    }

                }
                else
                {
                    BtnPostInt.Visible = false;
                    TxtInterestNew.Text = "0";
                }
            }

            else
            {
                WebMsgBox.Show("Account Number not found", this.Page);
                txtAccNo.Text = "";
                Txtcustno.Text = "";
                TxtDepoAmt.Text = "";
                ddlduration.SelectedIndex = 0;
                TxtPeriod.Text = "";
                TxtRate.Text = "";
                TxtIntrest.Text = "";
                TxtMaturity.Text = "";
                DtDueDate.Text = "";
                txtAccNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    public void CheckData()
    {
        try
        {
            dt1 = CurrentCls1.GetAllFieldData(TxtProcode.Text.ToString(), txtAccNo.Text.ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());

            if (dt1.Rows.Count > 0)
            {
                Txtcustno.Text = dt1.Rows[0]["CUSTNO"].ToString();
                TxtAccname.Text = dt1.Rows[0]["CUSTNAME"].ToString();
                txtAccNo.Text = dt1.Rows[0]["CUSTACCNO"].ToString();
                txtPan.Text = PanCard(Session["BRCD"].ToString(), Txtcustno.Text);
                dtDeposDate.Text = Convert.ToDateTime(dt1.Rows[0]["OPENINGDATE"].ToString()).ToString("dd/MM/yyyy");
                ddlIntrestPay.SelectedValue = Convert.ToInt32(dt1.Rows[0]["INTPAYOUT"].ToString() == "" ? "0" : dt1.Rows[0]["INTPAYOUT"].ToString()).ToString();
                TxtDepoAmt.Text = dt1.Rows[0]["PRNAMT"].ToString();
                TxtPeriod.Text = dt1.Rows[0]["PERIOD"].ToString();
                ddlduration.SelectedValue = dt1.Rows[0]["PRDTYPE"].ToString();
                TxtRate.Text = dt1.Rows[0]["RATEOFINT"].ToString();
                double periodCalculatedByPara = Convert.ToDouble(CurrentCls.GetMaturityParaDetails(CurrentROI: Convert.ToDouble(TxtRate.Text)));
                txtMaturityCalByPara.Text = Convert.ToString(periodCalculatedByPara);
                //  txtMaturityCalByPara.Text = (Convert.ToDouble(TxtRate.Text) - periodCalculatedByPara).ToString();

                TxtIntrest.Text = Convert.ToInt32(dt1.Rows[0]["INTAMT"]).ToString();
                TxtMaturity.Text = Convert.ToInt32(dt1.Rows[0]["MATURITYAMT"]).ToString();
                DtDueDate.Text = Convert.ToDateTime(dt1.Rows[0]["DUEDATE"].ToString()).ToString("dd/MM/yyyy");
                TxtTDLastIntDt.Text = Convert.ToDateTime(dt1.Rows[0]["LASTINTDATE"]).ToString("dd/MM/yyyy");
                string LMSTS = dt1.Rows[0]["ACC_STATUS"].ToString();

                if (LMSTS == "1")
                    TxtOpenClose.Text = "Open";
                else if (LMSTS == "4")
                    TxtOpenClose.Text = "Lien";
                else if (LMSTS == "3")
                    TxtOpenClose.Text = "Close";

                if (LMSTS == "3")
                {
                    BtnSubmit.Enabled = false;
                    BtnCalcSbInt.Enabled = false;
                    CloseText(true);
                }
                else
                {
                    CloseText(false);
                    BtnSubmit.Enabled = true;
                    BtnCalcSbInt.Enabled = true;
                }

                ViewState["ACCT"] = dt1.Rows[0]["ACC_TYPE"].ToString();
                ViewState["OPRTYPE"] = dt1.Rows[0]["OPR_TYPE"].ToString();


                int DF = Convert.ToInt32(conn.GetDayDiff(Session["EntryDate"].ToString(), DtDueDate.Text));

                if (DF <= 0 || ViewState["CAT"].ToString() == "DP")  // added for Group "DP" as per requirement 07-02-2018
                {

                    rdbMature.Checked = true;
                    ViewState["MATURITY"] = "MA";
                    RdbRenew.Enabled = true;
                }
                else
                {
                    rdbPreMature.Checked = true;
                    ViewState["MATURITY"] = "PRE";
                    RdbRenew.Enabled = false;
                }


                string[] arraydt = { "" };


                arraydt = Session["EntryDate"].ToString().Split('/');
                if (rdbMature.Checked == true && LMSTS != "3")
                {
                    Res = BD.GetFDSBINTStatus(Session["BRCD"].ToString());
                    if (Res == "Y")
                        GetSBINT();
                    else
                    {
                        TxtSbintrest.Text = "0";
                    }
                }

                if (ViewState["RD"].ToString() == "YES" || ViewState["LKP"].ToString() == "YES") // For RD Interest Calculation
                {
                    if (ViewState["BKCD"].ToString() != "1001")
                        CalculateRDInt();
                    else
                        CalculateRDInt_1001();


                }

                else// For other than RD Interest Calculation
                {
                    if (ViewState["CAT"].ToString() == "DP") // For DP Interest Calculation
                    {
                        CalculateDP_INT();

                    }
                    else
                    {
                        CalculateINT();
                    }
                }

                if (LMSTS == "3")
                {
                    TxtInterestNew.Text = "0";
                    TxtSbintrest.Text = "0";
                }


                // Checking Interset is paid today or not
                //if (Rdb_IntPay.Checked != true) //As per confrim changes commented this code
                //{
                Res = CurrentCls1.GetLastIntDate(Session["BRCD"].ToString(), Txtcustno.Text.Trim().ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());

                if (LMSTS == "3")
                {
                    BtnPostInt.Visible = false;
                    BtnCalcSbInt.Enabled = false;
                }
                else if (Res == Session["Entrydate"].ToString())
                {
                    BtnPostInt.Visible = false;
                    BtnCalcSbInt.Enabled = false;
                }
                else
                {
                    BtnPostInt.Visible = true;
                    BtnCalcSbInt.Enabled = true;
                }

                //}
                //else
                //{
                //    BtnPostInt.Visible = false;
                //    TxtInterestNew.Text = "0";
                //}
            }

            else
            {
                WebMsgBox.Show("Account Number not found", this.Page);
                txtAccNo.Text = "";
                Txtcustno.Text = "";
                TxtDepoAmt.Text = "";
                ddlduration.SelectedIndex = 0;
                TxtPeriod.Text = "";
                TxtRate.Text = "";
                TxtIntrest.Text = "";
                TxtMaturity.Text = "";
                DtDueDate.Text = "";
                txtAccNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    #region Shivjyoti Rd Calculation
    //Shivjyoti RD Interest Calculations
    public void CalculateRDInt_1001()
    {
        try
        {
            string cldate;
            cldate = "";
            if (ViewState["MATURITY"].ToString() == "PRE")
            {
                cldate = Session["EntryDate"].ToString();
            }

            if (ViewState["MATURITY"].ToString() == "MA")
            {
                cldate = DtDueDate.Text;
            }

            string daysDiff = "";
            daysDiff = (Convert.ToInt32(conn.GetDayDiff(dtDeposDate.Text, cldate)) / 30).ToString();

            double penalintrst, rate;
            penalintrst = 0;
            rate = 0;

            //New Interest rate added for Effectivedate 03-11-2017
            if (rdbPreMature.Checked == true)
            {
                rate = Convert.ToDouble(CurrentCls.GetInterestRateED("TDA", "", TxtProcode.Text, ViewState["ACCT"].ToString(), ddlduration.SelectedValue.ToString(), daysDiff.ToString(), Session["EntryDate"].ToString(), rdbPreMature.Checked));
                rate = Convert.ToDouble(CurrentCls.GetInterestRateED("TDA_PENAL", "", TxtProcode.Text, ViewState["ACCT"].ToString(), ddlduration.SelectedValue.ToString(), TxtPeriod.Text, Session["EntryDate"].ToString(), rdbPreMature.Checked));
            }
            else
            {
                // as per new requirement on 24-01-2018 for premature it should take Recent interest for all premature cases
                rate = Convert.ToDouble(CurrentCls.GetInterestRateED("TDA", "", TxtProcode.Text, ViewState["ACCT"].ToString(), ddlduration.SelectedValue.ToString(), daysDiff.ToString(), dtDeposDate.Text, rdbPreMature.Checked));
                rate = Convert.ToDouble(CurrentCls.GetInterestRateED("TDA_PENAL", "", TxtProcode.Text, ViewState["ACCT"].ToString(), ddlduration.SelectedValue.ToString(), TxtPeriod.Text, dtDeposDate.Text, rdbPreMature.Checked));
            }


            // Get last date and interest rate
            string dtr = Session["EntryDate"].ToString();
            double intrate = rate - penalintrst;

            // Get months between opening date and final date
            dtDeposDate.Text.ToString();
            Session["EntryDate"].ToString();


            //Added By Amol for RD Interest Calculation on 2016-12-12
            dt = new DataTable();
            dt = CMN.CalRDintrest_1001(Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text, Session["EntryDate"].ToString(), "C", "0");
            double PP = CMN.GetPPandINT("CLOSING", TxtProcode.Text, txtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString());
            double INTP = CMN.GetPPandINT("CLOSING", ViewState["IR"].ToString(), txtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "10");

            if (PP == 0 && TxtOpenClose.Text != "Close")// if balance is Zero then Return to start // Show Closed account deatils added by abhishek on 15-01-2018 
            {
                WebMsgBox.Show("Balance Mismatch/ Lein Account but Balance Zero...!", this.Page);
                txtAccNo.Text = "";
                TxtAccname.Text = "";
                clear();
                TxtProcode.Focus();
                return;
            }
            if (dt.Rows.Count > 0)
            {

                TxtPrincPaybl.Text = PP.ToString();
                TxtIntrestPaybl.Text = INTP.ToString();

                Res = CurrentCls1.GetLastIntDate(Session["BRCD"].ToString(), Txtcustno.Text.Trim().ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                if (Res != Session["Entrydate"].ToString())
                {
                    double T_IntApp = Math.Round((Convert.ToDouble(dt.Rows[0]["ActualCredit"]) + Convert.ToDouble(dt.Rows[0]["SumInterest"])) - (Convert.ToDouble(TxtPrincPaybl.Text) + Convert.ToDouble(TxtIntrestPaybl.Text)), 0);
                    TxtInterestNew.Text = T_IntApp.ToString();

                    if (ViewState["MATURITY"].ToString() != "MA")
                    {
                        AddCommision();
                    }

                    BindRDGrid_1001(Grid_RDCal, Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text, Session["EntryDate"].ToString());
                }
                else
                {
                    TxtInterestNew.Text = "0";
                    TxtSbintrest.Text = "0";
                }
            }
            else
            {
                WebMsgBox.Show("Wrong Interest payout for Deposit details....", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Other than Shivjyoti Like YSPM,SHIVSAMARTH,VAISHNAVI,PUCBL
    //*** RD Interest Calculation ***
    public void CalculateRDInt()
    {
        try
        {
            string cldate;
            cldate = "";
            if (ViewState["MATURITY"].ToString() == "PRE")
            {
                cldate = Session["EntryDate"].ToString();
            }

            if (ViewState["MATURITY"].ToString() == "MA")
            {
                cldate = DtDueDate.Text;
            }

            string daysDiff = "";
            daysDiff = (Convert.ToInt32(conn.GetDayDiff(dtDeposDate.Text, cldate)) / 30).ToString();

            double penalintrst, rate;
            penalintrst = 0;
            rate = 0;

            //New Interest rate added for Effectivedate 03-11-2017
            if (rdbPreMature.Checked == true)
            {
                rate = Convert.ToDouble(CurrentCls.GetInterestRateED("TDA", "", TxtProcode.Text, ViewState["ACCT"].ToString(), ddlduration.SelectedValue.ToString(), daysDiff.ToString(), Session["EntryDate"].ToString(), rdbPreMature.Checked));
                rate = Convert.ToDouble(CurrentCls.GetInterestRateED("TDA_PENAL", "", TxtProcode.Text, ViewState["ACCT"].ToString(), ddlduration.SelectedValue.ToString(), TxtPeriod.Text, Session["EntryDate"].ToString(), rdbPreMature.Checked));
            }
            else
            {
                // as per new requirement on 24-01-2018 for premature it should take Recent interest for all premature cases
                rate = Convert.ToDouble(CurrentCls.GetInterestRateED("TDA", "", TxtProcode.Text, ViewState["ACCT"].ToString(), ddlduration.SelectedValue.ToString(), daysDiff.ToString(), dtDeposDate.Text, rdbPreMature.Checked));
                rate = Convert.ToDouble(CurrentCls.GetInterestRateED("TDA_PENAL", "", TxtProcode.Text, ViewState["ACCT"].ToString(), ddlduration.SelectedValue.ToString(), TxtPeriod.Text, dtDeposDate.Text, rdbPreMature.Checked));
            }


            // Get last date and interest rate
            string dtr = Session["EntryDate"].ToString();
            double intrate = rate - penalintrst;

            // Get months between opening date and final date
            dtDeposDate.Text.ToString();
            Session["EntryDate"].ToString();


            //Added By Amol for RD Interest Calculation on 2016-12-12
            dt = new DataTable();
            dt = CMN.CalRDintrest(Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text, Session["EntryDate"].ToString(), "C", "0");
            double PP = CMN.GetPPandINT("CLOSING", TxtProcode.Text, txtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString());
            double INTP = CMN.GetPPandINT("CLOSING", ViewState["IR"].ToString(), txtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "10");

            if (PP == 0 && TxtOpenClose.Text != "Close")// if balance is Zero then Return to start // Show Closed account deatils added by abhishek on 15-01-2018 
            {
                WebMsgBox.Show("Balance Mismatch/ Lein Account but Balance Zero...!", this.Page);
                txtAccNo.Text = "";
                TxtAccname.Text = "";
                clear();
                TxtProcode.Focus();
                return;
            }
            if (dt.Rows.Count > 0)
            {

                TxtPrincPaybl.Text = PP.ToString();
                TxtIntrestPaybl.Text = INTP.ToString();

                Res = CurrentCls1.GetLastIntDate(Session["BRCD"].ToString(), Txtcustno.Text.Trim().ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                if (Res != Session["Entrydate"].ToString())
                {

                    DataTable Dtr = CMN.CalRDShceduleChk(Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text, Session["EntryDate"].ToString(), "");
                    double T_IntApp = 0;
                    if (Dtr.Rows.Count > 0 && ViewState["MATURITY"].ToString() == "MA")
                    {
                        //SumScheduleCr	SumCredit
                        if (Convert.ToDouble(Dtr.Rows[0]["SumCredit"]) >= Convert.ToDouble(Dtr.Rows[0]["SumScheduleCr"]))
                        {
                            T_IntApp = Convert.ToDouble(TxtMaturity.Text) - (Convert.ToDouble(TxtPrincPaybl.Text) + Convert.ToDouble(TxtIntrestPaybl.Text)) + Convert.ToDouble(Dtr.Rows[0]["SumAfterDueIntr"]);
                        }
                        else
                        {
                            T_IntApp = Math.Round((Convert.ToDouble(dt.Rows[0]["ActualCredit"]) + Convert.ToDouble(dt.Rows[0]["SumInterest"])) - (Convert.ToDouble(TxtPrincPaybl.Text) + Convert.ToDouble(TxtIntrestPaybl.Text)), 0);
                        }
                    }
                    else
                    {
                        T_IntApp = Math.Round((Convert.ToDouble(dt.Rows[0]["ActualCredit"]) + Convert.ToDouble(dt.Rows[0]["SumInterest"])) - (Convert.ToDouble(TxtPrincPaybl.Text) + Convert.ToDouble(TxtIntrestPaybl.Text)), 0);
                    }

                    TxtInterestNew.Text = T_IntApp.ToString();

                    if (ViewState["MATURITY"].ToString() != "MA")
                    {
                        AddCommision();
                    }

                    //Rd Calculation Grid Bind
                    BindRDGrid(Grid_RDCal, Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text, Session["EntryDate"].ToString());
                    //}

                    //if (ViewState["MATURITY"].ToString() == "MA")
                    //{
                    //    //Case Added for maturity as per abika mam 05/10/2017
                    //    double TotalAmtCheck = 0, TotalPaid = 0;
                    //    TotalAmtCheck = Convert.ToDouble(TxtDepoAmt.Text) * Convert.ToDouble(TxtPeriod.Text);

                    //    //if (TotalAmtCheck >= PP)      //  Commented and added new line by amol on 10-07-2017 as per ambika mam instruction
                    //    //if (PP >= TotalAmtCheck && Convert.ToDouble(TxtMaturity.Text)>=Convert.ToDouble(TxtPrincPaybl.Text))
                    //    //{

                    //    //    TotalPaid = Convert.ToDouble(TxtMaturity.Text) - (PP + INTP);
                    //    //}
                    //    //else if (Convert.ToDouble(TxtMaturity.Text) < Convert.ToDouble(TxtPrincPaybl.Text)) //added on 21-11-2017 For Excess RD Amount from customer Abhishek
                    //    //{
                    //    //     result = CMN.BindRDCal(Grid_RDCal, Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text, Session["EntryDate"].ToString());
                    //    //     TotalPaid = Math.Round(Convert.ToDouble(dt.Rows[0]["Interest"]) - Convert.ToDouble(TxtIntrestPaybl.Text), 0);// - Convert.ToDouble(dt.Rows[0]["InterestPaid"])), 0).ToString();
                    //    //}
                    //    //else
                    //    //{
                    //    //    TotalPaid = Math.Round(Convert.ToDouble(dt.Rows[0]["Interest"]) - Convert.ToDouble(TxtIntrestPaybl.Text), 0);// - Convert.ToDouble(dt.Rows[0]["InterestPaid"])), 0).ToString();
                    //    //}
                    //    //TxtInterestNew.Text = TotalPaid.ToString();


                    //    //if (PP >= TotalAmtCheck)
                    //    //{
                    //    //    TotalPaid = Convert.ToDouble(TxtMaturity.Text) - (PP + INTP);
                    //    //}
                    //    //else
                    //    //{
                    //    TotalPaid = Math.Round(Convert.ToDouble(dt.Rows[0]["SumInterest"]) - Convert.ToDouble(TxtIntrestPaybl.Text), 0);// - Convert.ToDouble(dt.Rows[0]["InterestPaid"])), 0).ToString();
                    //    BindRDGrid(Grid_RDCal, Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text, Session["EntryDate"].ToString());
                    //    //}
                    //    TxtInterestNew.Text = TotalPaid.ToString();
                    //}
                }
                else
                {
                    TxtInterestNew.Text = "0";
                    TxtSbintrest.Text = "0";
                }
            }
            else
            {
                WebMsgBox.Show("Wrong Interest payout for Deposit details....", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #endregion

    #region DP Category Calculation
    public void CalculateDP_INT()
    {
        try
        {
            string cldate;
            cldate = "";
            if (ViewState["MATURITY"].ToString() == "PRE")
            {
                cldate = Session["EntryDate"].ToString();
            }

            if (ViewState["MATURITY"].ToString() == "MA")
            {
                cldate = DtDueDate.Text;
            }

            string daysDiff = "";
            daysDiff = (Convert.ToInt32(conn.GetDayDiff(dtDeposDate.Text, cldate)) / 30).ToString();

            double penalintrst, rate;
            penalintrst = 0;
            rate = 0;

            //New Interest rate added for Effectivedate 03-11-2017
            if (rdbPreMature.Checked == true)
            {
                rate = Convert.ToDouble(CurrentCls.GetInterestRateED("TDA", "", TxtProcode.Text, ViewState["ACCT"].ToString(), ddlduration.SelectedValue.ToString(), daysDiff.ToString(), Session["EntryDate"].ToString(), rdbPreMature.Checked));
                rate = Convert.ToDouble(CurrentCls.GetInterestRateED("TDA_PENAL", "", TxtProcode.Text, ViewState["ACCT"].ToString(), ddlduration.SelectedValue.ToString(), TxtPeriod.Text, Session["EntryDate"].ToString(), rdbPreMature.Checked));
            }
            else
            {
                // as per new requirement on 24-01-2018 for premature it should take Recent interest for all premature cases
                rate = Convert.ToDouble(CurrentCls.GetInterestRateED("TDA", "", TxtProcode.Text, ViewState["ACCT"].ToString(), ddlduration.SelectedValue.ToString(), daysDiff.ToString(), dtDeposDate.Text, rdbPreMature.Checked));
                rate = Convert.ToDouble(CurrentCls.GetInterestRateED("TDA_PENAL", "", TxtProcode.Text, ViewState["ACCT"].ToString(), ddlduration.SelectedValue.ToString(), TxtPeriod.Text, dtDeposDate.Text, rdbPreMature.Checked));
            }


            // Get last date and interest rate
            string dtr = Session["EntryDate"].ToString();
            double intrate = rate - penalintrst;

            // Get months between opening date and final date
            dtDeposDate.Text.ToString();
            Session["EntryDate"].ToString();


            //Added By Amol for RD Interest Calculation on 2016-12-12
            dt = new DataTable();
            dt = CMN.CalDpintrest(Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text, Session["EntryDate"].ToString(), "C");
            double PP = CMN.GetPPandINT("CLOSING", TxtProcode.Text, txtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString());
            double INTP = CMN.GetPPandINT("CLOSING", ViewState["IR"].ToString(), txtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "10");

            if (PP == 0 && TxtOpenClose.Text != "Close")// if balance is Zero then Return to start // Show Closed account deatils added by abhishek on 15-01-2018 
            {
                WebMsgBox.Show("Balance Mismatch/ Lein Account but Balance Zero...!", this.Page);
                txtAccNo.Text = "";
                TxtAccname.Text = "";
                clear();
                TxtProcode.Focus();
                return;
            }
            if (dt.Rows.Count > 0)
            {

                TxtPrincPaybl.Text = PP.ToString();
                TxtIntrestPaybl.Text = INTP.ToString();

                Res = CurrentCls1.GetLastIntDate(Session["BRCD"].ToString(), Txtcustno.Text.Trim().ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                if (Res != Session["Entrydate"].ToString())
                {

                    //Case Added for maturity as per abika mam 05/10/2017
                    double TotalAmtCheck = 0, TotalPaid = 0;
                    TotalAmtCheck = Convert.ToDouble(TxtDepoAmt.Text) * Convert.ToDouble(TxtPeriod.Text);

                    TotalPaid = Math.Round(Convert.ToDouble(dt.Rows[0]["SumInterest"]));// - Convert.ToDouble(dt.Rows[0]["InterestPaid"])), 0).ToString();
                    BindDPGrid(Grid_RDCal, Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text, Session["EntryDate"].ToString());

                    TxtInterestNew.Text = TotalPaid.ToString();
                    AddCommision_Suraksha();
                }
                else
                {
                    TxtInterestNew.Text = "0";
                    TxtSbintrest.Text = "0";
                }
            }
            else
            {
                WebMsgBox.Show("Wrong Interest payout for Deposit details....", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #endregion

    #region Other than RD for ALL
    public void CalculateINT()
    {
        try
        {
            bool Demon = false;
            double GridIntApplied = 0;
            // Showing Grid for Calculation


            if (ViewState["RD"].ToString() != "YES") // For mature but not Recurring Deposit **********************
            {
                if (rdbMature.Checked == true)
                {
                    DataTable DTT1 = new DataTable();
                    DTT1.Columns.Add("MA");
                    DTT1.Columns.Add("PP");
                    DTT1.Columns.Add("IP");
                    DTT1.Columns.Add("IPI");
                    DTT1.Columns.Add("TOTAL");

                    string[] arraydt = Session["EntryDate"].ToString().Split('/');
                    double INTPRI = OC.GetOpenClose("CLOSING", arraydt[2].ToString(), arraydt[1].ToString(), TxtProcode.Text, txtAccNo.Text.ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "5");
                    double INTEREST = OC.GetOpenClose("CLOSING", arraydt[2].ToString(), arraydt[1].ToString(), ViewState["IR"].ToString(), txtAccNo.Text.ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "10");// Get Interest Payble
                    string INTPAIDDebit = CurrentCls.GetIntPaid(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text, txtAccNo.Text, "INTPAID");
                    INTPAIDDebit = string.IsNullOrEmpty(INTPAIDDebit) == true ? "0" : INTPAIDDebit.ToString();
                    if (INTPRI == 0 && TxtOpenClose.Text != "Close")// if balance is Zero then Return to start // Show Closed account deatils added by abhishek on 15-01-2018 
                    {
                        WebMsgBox.Show("Balance Mismatch/ Lein Account but Balance Zero..!", this.Page);
                        txtAccNo.Text = "";
                        TxtAccname.Text = "";
                        clear();
                        TxtProcode.Focus();
                        return;
                    }
                    TxtPrincPaybl.Text = INTPRI.ToString();
                    TxtIntrestPaybl.Text = Convert.ToInt32(INTEREST).ToString();
                    TxtIntPaid.Text = INTPAIDDebit.ToString();

                    double TotalPaid = Convert.ToDouble(TxtMaturity.Text) - (INTPRI + INTEREST);
                    Res = CurrentCls1.GetLastIntDate(Session["BRCD"].ToString(), Txtcustno.Text.Trim().ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                    if (Res != Session["Entrydate"].ToString())
                    {
                        if (ViewState["CAT"].ToString() == "MIS")
                        {
                            if (Convert.ToDateTime(conn.ConvertDate(Res)) < Convert.ToDateTime(conn.ConvertDate(Session["Entrydate"].ToString())))
                            {
                                dt = CurrentCls1.GetIntAmountMA(Session["BRCD"].ToString(), (rdbMature.Checked == true) ? "MAT" : "PRE", TxtProcode.Text, txtAccNo.Text, TxtPrincPaybl.Text, TxtRate.Text, dtDeposDate.Text, DtDueDate.Text, TxtTDLastIntDt.Text, Session["EntryDate"].ToString(), ViewState["CAT"].ToString());
                                if (dt.Rows.Count > 0)
                                {
                                    TxtInterestNew.Text = Math.Round(Convert.ToDouble(dt.Rows[0]["IntNew"].ToString()), 0).ToString();
                                }
                                else
                                {
                                    TxtInterestNew.Text = "0";
                                }
                            }
                        }
                        else if (ViewState["CAT"].ToString() == "QIS")
                        {
                            if (Convert.ToDateTime(conn.ConvertDate(Res)) < Convert.ToDateTime(conn.ConvertDate(Session["Entrydate"].ToString())))
                            {
                                dt = CurrentCls1.GetIntAmountQISMA(Session["BRCD"].ToString(), (rdbMature.Checked == true) ? "MAT" : "PRE", TxtProcode.Text, txtAccNo.Text, TxtPrincPaybl.Text, TxtRate.Text, dtDeposDate.Text, DtDueDate.Text, TxtTDLastIntDt.Text, Session["EntryDate"].ToString(), ViewState["CAT"].ToString());
                                if (dt.Rows.Count > 0)
                                {
                                    TxtInterestNew.Text = Math.Round(Convert.ToDouble(dt.Rows[0]["IntNew"].ToString()), 0).ToString();
                                }
                                else
                                {
                                    TxtInterestNew.Text = "0";
                                }
                            }
                        }
                        else if (ViewState["CAT"].ToString() != "MIS")
                        {
                            TxtInterestNew.Text = Math.Round(TotalPaid, 0).ToString();
                        }
                        else
                        {
                            TxtInterestNew.Text = "0";
                        }
                        if (TotalPaid == 0)
                        {
                            BtnPostInt.Visible = false;
                        }
                        else
                        {
                            BtnPostInt.Visible = true;
                        }
                    }
                    else
                    {
                        TxtInterestNew.Text = "0";
                        TxtSbintrest.Text = "0";
                    }

                    if (TxtDepoAmt.Text != null || TxtDepoAmt.Text != "")
                    {
                        DTT1.Rows.Add(TxtMaturity.Text, TxtPrincPaybl.Text, TxtIntrestPaybl.Text, (INTPRI + INTEREST).ToString(), Math.Round(TotalPaid, 0).ToString());
                    }

                    Grid_MatureCalculation.DataSource = DTT1;
                    Grid_MatureCalculation.DataBind();
                }
                else if (rdbPreMature.Checked == true) // For Premature  **********************
                {
                    int Days;
                    int Mon;
                    string RT;
                    DataTable DTT = new DataTable();
                    DTT.Columns.Add("P");
                    DTT.Columns.Add("R");
                    DTT.Columns.Add("PR");
                    DTT.Columns.Add("RPR");
                    DTT.Columns.Add("D");
                    DTT.Columns.Add("M");
                    DTT.Columns.Add("T");

                    double ROI, PL, CROI, PRI, CINT;
                    ROI = PL = CROI = PRI = CINT = 0;

                    string Allcat_DedPara = CMN.GetUniversalPara("TDAPRE_ADMINCHG");
                    if (Allcat_DedPara != null && Allcat_DedPara == "Y")
                    {
                        AddCommision_Suraksha();
                    }

                    Res = CurrentCls1.GetLastIntDate(Session["BRCD"].ToString(), Txtcustno.Text.Trim().ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                    string CAT = MT.GetDepositCat(Session["BRCD"].ToString(), TxtProcode.Text, "MISTRF");
                    if (CAT != null && CAT == "MIS")
                    {
                        Days = Convert.ToInt32(conn.GetDayDiff(dtDeposDate.Text, Session["EntryDate"].ToString()));
                        Mon = 1;

                        RT = CurrentCls.GetInterestRateED("TDA_RATE_PENAL", "", TxtProcode.Text, ViewState["ACCT"].ToString(), "D", Days.ToString(), Session["EntryDate"].ToString(), rdbPreMature.Checked);// as per new requirement on 24-01-2018 for premature it should take Recent interest for all premature cases
                        if (RT == null) //if Days Data not found then calculate in Month
                        {
                            Mon = Convert.ToInt32(conn.GetMonthDiff(dtDeposDate.Text, Session["EntryDate"].ToString()));
                            RT = CurrentCls.GetInterestRateED("TDA_RATE_PENAL", "", TxtProcode.Text, ViewState["ACCT"].ToString(), "M", Mon.ToString(), Session["EntryDate"].ToString(), rdbPreMature.Checked);

                            if (RT != null)
                                Demon = true;
                        }


                        //Get Rate by Remove DEPOSITGL Condition --Abhishek 03-06-2017
                        if (RT == null)
                        {
                            Days = Convert.ToInt32(conn.GetDayDiff(dtDeposDate.Text, Session["EntryDate"].ToString()));
                            Mon = 1;

                            RT = CurrentCls.GetInterestRateED("TDA_RATE_PENAL", "", TxtProcode.Text, ViewState["ACCT"].ToString(), "D", Days.ToString(), Session["EntryDate"].ToString(), false);
                            if (RT == null) //if Days Data not found then calculate in Month
                            {
                                Mon = Convert.ToInt32(conn.GetMonthDiff(dtDeposDate.Text, Session["EntryDate"].ToString()));
                                RT = CurrentCls.GetInterestRateED("TDA_RATE_PENAL", "", TxtProcode.Text, ViewState["ACCT"].ToString(), "M", Mon.ToString(), Session["EntryDate"].ToString(), false);
                                Demon = true;
                            }
                        }
                    }
                    else if (CAT != null && CAT == "QIS")
                    {
                        Days = Convert.ToInt32(conn.GetDayDiff(TxtTDLastIntDt.Text, Session["EntryDate"].ToString()));
                        Mon = 1;

                        RT = CurrentCls.GetInterestRateED("TDA_RATE_PENAL", "", TxtProcode.Text, ViewState["ACCT"].ToString(), "D", Days.ToString(), Session["EntryDate"].ToString(), rdbPreMature.Checked);// as per new requirement on 24-01-2018 for premature it should take Recent interest for all premature cases
                        if (RT == null) //if Days Data not found then calculate in Month
                        {
                            Mon = Convert.ToInt32(conn.GetMonthDiff(dtDeposDate.Text, Session["EntryDate"].ToString()));
                            RT = CurrentCls.GetInterestRateED("TDA_RATE_PENAL", "", TxtProcode.Text, ViewState["ACCT"].ToString(), "M", Mon.ToString(), Session["EntryDate"].ToString(), rdbPreMature.Checked);

                            if (RT != null)
                                Demon = true;
                        }


                        //Get Rate by Remove DEPOSITGL Condition --Abhishek 03-06-2017
                        if (RT == null)
                        {
                            Days = Convert.ToInt32(conn.GetDayDiff(dtDeposDate.Text, Session["EntryDate"].ToString()));
                            Mon = 1;

                            RT = CurrentCls.GetInterestRateED("TDA_RATE_PENAL", "", TxtProcode.Text, ViewState["ACCT"].ToString(), "D", Days.ToString(), Session["EntryDate"].ToString(), false);
                            if (RT == null) //if Days Data not found then calculate in Month
                            {
                                Mon = Convert.ToInt32(conn.GetMonthDiff(dtDeposDate.Text, Session["EntryDate"].ToString()));
                                RT = CurrentCls.GetInterestRateED("TDA_RATE_PENAL", "", TxtProcode.Text, ViewState["ACCT"].ToString(), "M", Mon.ToString(), Session["EntryDate"].ToString(), false);
                                Demon = true;
                            }
                        }
                    }


                    else
                    {
                        Days = Convert.ToInt32(conn.GetDayDiff(dtDeposDate.Text, Session["EntryDate"].ToString()));
                        Mon = 1;

                        RT = CurrentCls.GetInterestRateED("TDA_RATE_PENAL", "", TxtProcode.Text, ViewState["ACCT"].ToString(), "D", Days.ToString(), Session["EntryDate"].ToString(), rdbPreMature.Checked);
                       // RT = CurrentCls.GetInterestRateED(FL: "TDA_RATE_PENAL",SFL: "",SBGL: TxtProcode.Text,CUSTTYPE: ViewState["ACCT"].ToString(), PERIODTYPE: "D",  PERIOD: Days.ToString(), OPDT: Session["EntryDate"].ToString(),SN: rdbPreMature.Checked,AccNo: txtAccNo.Text);

                        if (RT == null) //if Days Data not found then calculate in Month
                        {
                            Mon = Convert.ToInt32(conn.GetMonthDiff(dtDeposDate.Text, Session["EntryDate"].ToString()));
                            RT = CurrentCls.GetInterestRateED( FL:"TDA_RATE_PENAL",SFL: "",SBGL: TxtProcode.Text, CUSTTYPE: ViewState["ACCT"].ToString(),PERIODTYPE: "M",PERIOD: Mon.ToString(),OPDT: Session["EntryDate"].ToString(),SN: rdbPreMature.Checked,AccNo:txtAccNo.Text);
                            Demon = true;
                        }
                    }


                    if (RT != null)
                    {
                        string[] TD = RT.Split('_');
                        if (TD.Length > 1)
                        {
                            ROI = Convert.ToDouble(TD[0].ToString());
                            PL = Convert.ToDouble(TD[1].ToString());
                            PRI = Convert.ToDouble(TxtDepoAmt.Text);
                            CROI = ROI - PL;
                            if (CAT != null && CAT == "MIS")
                            {
                                if (Rdb_IntPay.Checked == true)
                                {
                                    ROI = Convert.ToDouble(TxtRate.Text.ToString() == "" ? "0" : TxtRate.Text.ToString());
                                    PL = 0;
                                    CROI = ROI - PL;
                                    if (Demon == false)
                                        CINT = PRI * CROI / 100 / 365 * Convert.ToDouble(Days);
                                    else
                                        CINT = PRI * CROI / 100 / 12 * Convert.ToDouble(Mon);
                                }
                                else
                                {
                                    if (Demon == false)
                                        CINT = PRI * CROI / 100 / 365 * Convert.ToDouble(Days);
                                    else
                                        CINT = PRI * CROI / 100 / 12 * Convert.ToDouble(Mon);
                                }
                            }
                            else
                            {
                                if (Demon == false)
                                    CINT = PRI * CROI / 100 / 365 * Convert.ToDouble(Days);
                                else
                                    CINT = PRI * CROI / 100 / 12 * Convert.ToDouble(Mon);
                            }
                            string[] arraydt = Session["EntryDate"].ToString().Split('/');
                            double INTPRI = OC.GetOpenClose("CLOSING", arraydt[2].ToString(), arraydt[1].ToString(), TxtProcode.Text, txtAccNo.Text.ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "5");
                            double INTEREST = OC.GetOpenClose("CLOSING", arraydt[2].ToString(), arraydt[1].ToString(), ViewState["IR"].ToString(), txtAccNo.Text.ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "10");// Get Interest Payble
                            string INTPAIDDebit = CurrentCls.GetIntPaid(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text, txtAccNo.Text, "INTPAID");
                            INTPAIDDebit = string.IsNullOrEmpty(INTPAIDDebit) == true ? "0" : INTPAIDDebit.ToString();

                            if (INTPRI == 0 && TxtOpenClose.Text != "Close")// if balance is Zero then Return to start // Show Closed account deatils added by abhishek on 15-01-2018 
                            {
                                WebMsgBox.Show("Balance Mismatch/ Lein Account but Balance Zero...!", this.Page);
                                txtAccNo.Text = "";
                                TxtAccname.Text = "";
                                clear();
                                TxtProcode.Focus();
                                return;
                            }
                            double TotalPaid = INTPRI + INTEREST;
                            // CINT = Math.Round(TotalPaid, 0) - Math.Round(CINT + PRI, 0); 
                            // As Per Reverse Int Discussion (Changed) with Ambika Mam

                            CINT = (CINT - INTEREST) - (Convert.ToDouble(INTPAIDDebit));
                            GridIntApplied = CINT - INTEREST;


                            TxtPrincPaybl.Text = Math.Round(INTPRI, 0).ToString();

                            string CAT1 = MT.GetDepositCat(Session["BRCD"].ToString(), TxtProcode.Text, "MISTRF");

                            //For CUM Case in Which Interest is Directly Credited to Principal Amount 04/08/2017
                            if (TxtDepoAmt.Text != INTPRI.ToString() && INTPAIDDebit == "0" && CAT1 == "LKP")
                            {
                                TxtIntPaid.Text = INTPAIDDebit.ToString();
                                TxtIntrestPaybl.Text = Math.Round(INTEREST, 0).ToString();
                            }
                            else if (TxtDepoAmt.Text != INTPRI.ToString() && INTPAIDDebit == "0" && CAT1 != "DD")
                            {
                                TxtIntPaid.Text = ((INTPRI + CINT) - (Convert.ToDouble(TxtDepoAmt.Text) + CINT)).ToString();
                                CINT = CINT - Convert.ToDouble(TxtIntPaid.Text);
                                TxtIntrestPaybl.Text = Math.Round(INTEREST, 0).ToString();
                            }
                            else
                            {
                                TxtIntPaid.Text = INTPAIDDebit.ToString();
                                TxtIntrestPaybl.Text = Math.Round(INTEREST, 0).ToString();
                            }

                            Res = CurrentCls1.GetLastIntDate(Session["BRCD"].ToString(), Txtcustno.Text.Trim().ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                            if (Res != Session["Entrydate"].ToString())
                            {
                                CAT1 = MT.GetDepositCat(Session["BRCD"].ToString(), TxtProcode.Text, "MISTRF");

                                if (CAT1 != null && CAT == "DD")// For Depo Category "DD" Calculate Interest As below
                                {
                                    TxtInterestNew.Text = Math.Round((Convert.ToDouble(TxtDepoAmt.Text) + Convert.ToDouble(INTEREST) + Convert.ToDouble(CINT)) - (Convert.ToDouble(TxtPrincPaybl.Text)), 0).ToString();
                                    CINT = Convert.ToDouble(TxtInterestNew.Text);
                                }
                                else
                                {
                                    TxtInterestNew.Text = Math.Round(CINT, 0).ToString();
                                }
                                if (CINT == 0)
                                {
                                    BtnPostInt.Visible = false;
                                }
                                else
                                {
                                    if (CINT > 0)
                                    {
                                        BtnPostInt.Text = "Post INT";
                                    }
                                    else
                                    {
                                        BtnPostInt.Text = "Rev INT";
                                    }
                                    BtnPostInt.Visible = true;
                                }
                            }
                            else
                            {
                                TxtInterestNew.Text = "0";
                                TxtSbintrest.Text = "0";
                            }
                        }


                        if (Demon == true)
                            Days = 0;
                        else
                            Mon = 0;


                        if ((TxtDepoAmt.Text != null || TxtDepoAmt.Text != "") && RT != null)
                        {
                            DTT.Rows.Add(TxtDepoAmt.Text, ROI, PL, (Convert.ToDouble(ROI) - Convert.ToDouble(PL)).ToString(), Days, Mon, Math.Round(GridIntApplied, 0).ToString());
                        }

                        GridCalculation.DataSource = DTT;
                        GridCalculation.DataBind();

                    }
                    else
                    {
                        clear();
                        txtAccNo.Focus();
                        WebMsgBox.Show("Interest Rate Invalid !... Add Interest", this.Page);
                        return;
                    }
                }
            }
            else
            {
                double lastint = Convert.ToDouble(CMN.Interest(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), TxtProcode.Text, txtAccNo.Text));
                TxtIntrestPaybl.Text = Convert.ToDouble(Convert.ToDouble(TxtIntrestPaybl.Text) + Convert.ToDouble(TxtInterestNew.Text)).ToString();
                txtPayAmnt.Text = Convert.ToDouble(Convert.ToDouble(TxtPrincPaybl.Text) + Convert.ToDouble(TxtIntrestPaybl.Text)).ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #endregion

    public void GetSBINT()
    {
        try
        {
            DataTable DTSB = new DataTable();
            string duedate = "";
            string daysDiff = "";
            int SbIntrst = 0;
            string cldate = "";
            duedate = DtDueDate.Text;
            cldate = Session["EntryDate"].ToString();
            daysDiff = conn.GetDayDiff(duedate, cldate);

            string SBINTPARAM = CMN.GetSBInt(Session["BRCD"].ToString());
            txt1.Text = SBINTPARAM;
            //TxtInterestNew.Text = TxtIntrestPaybl.Text;
            // Get SBinterest

            string LIMIT = CMN.GetSBLimit(Session["BRCD"].ToString());
            if (Convert.ToInt32(daysDiff) > Convert.ToInt32(LIMIT))
            {
                //  Changes by amol on 30/10/2018 (as per ambika mam requirement)
                sResult = CMN.GetUniversalPara("MATUREFD");
                if ((sResult != null) && (sResult == "Y"))
                    SbIntrst = Convert.ToInt32((Convert.ToDouble(TxtMaturity.Text.ToString()) * Convert.ToDouble(SBINTPARAM)) / 100 / 365 * Convert.ToDouble(daysDiff));
                else
                    SbIntrst = Convert.ToInt32((Convert.ToDouble(TxtDepoAmt.Text.ToString()) * Convert.ToDouble(SBINTPARAM)) / 100 / 365 * Convert.ToDouble(daysDiff));
            }

            // Assign SBinterest
            TxtSbintrest.Text = SbIntrst.ToString();
            if (TxtSbintrest.Text != "0" && TxtDepoAmt.Text != "")
            {
                DTSB.Columns.Add("D");
                DTSB.Columns.Add("R");
                DTSB.Columns.Add("DD");
                DTSB.Columns.Add("L");
                DTSB.Columns.Add("T");

                //  Changes by amol on 30/10/2018 (as per ambika mam requirement)
                sResult = CMN.GetUniversalPara("MATUREFD");
                if ((sResult != null) && (sResult == "Y"))
                    DTSB.Rows.Add(TxtMaturity.Text.ToString(), SBINTPARAM.ToString(), daysDiff.ToString(), LIMIT.ToString(), SbIntrst.ToString());
                else
                    DTSB.Rows.Add(TxtDepoAmt.Text.ToString(), SBINTPARAM.ToString(), daysDiff.ToString(), LIMIT.ToString(), SbIntrst.ToString());

                GridSBInt.DataSource = DTSB;
                GridSBInt.DataBind();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    // Calculate Due date
    protected void CalDueDate(DateTime DeposDate, string durationtype, int duration)
    {
        try
        {
            DateTime today = DateTime.Today;
            DateTime duedate = new DateTime();
            switch (durationtype)
            {
                case "Days":
                    duedate = DeposDate.AddDays(duration);
                    DtDueDate.Text = duedate.ToShortDateString();
                    break;

                case "Months":
                    duedate = DeposDate.AddMonths(duration);
                    DateTime duedate1 = duedate.AddDays(-1);
                    DtDueDate.Text = duedate1.ToShortDateString();
                    break;
                case "Years":
                    duedate = DeposDate.AddYears(duration);
                    DtDueDate.Text = duedate.ToShortDateString();
                    break;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    //INREREST CALCULATOR
    protected void CalculatedepositINT(float amt, string subgl, float intrate, int Period, string intpay, string PTYPE)
    {
        try
        {
            float interest = 0;
            float maturityamt = 0;
            float QUATERS = 0;
            //string sql = "select GLGROUP from glmast where glcode=5 AND SUBGLCODE='"+subgl+"'";
            string sql = "SELECT CATEGORY FROM DEPOSITGL where DEPOSITGLCODE='" + subgl + "'";
            string category = conn.sExecuteScalar(sql);

            if (category == "")
            {
                return;
            }

            switch (category)
            {
                case "LKP":
                    //dt1 = CurrentCls1.GetLKPDetails(TxtPeriod.Text, TxtDepoAmt.Text);
                    //if (dt1.Rows.Count > 0)
                    //{

                    //}
                    break;
                case "DD":
                    interest = amt;
                    maturityamt = interest + amt;
                    TxtIntrest.Text = interest.ToString();
                    TxtMaturity.Text = maturityamt.ToString();
                    break;

                case "MIS":
                    interest = Convert.ToInt32(amt * intrate / 1209.75);
                    //maturityamt = interest + amt;
                    maturityamt = amt;
                    TxtIntrest.Text = interest.ToString();
                    TxtMaturity.Text = maturityamt.ToString();
                    break;

                case "CUM":
                    QUATERS = (Period / 3);
                    maturityamt = Convert.ToInt32((Math.Pow(((intrate / 400) + 1), (QUATERS))) * amt);
                    interest = maturityamt - amt;
                    TxtIntrest.Text = interest.ToString();
                    TxtMaturity.Text = maturityamt.ToString();
                    break;

                case "FDS":
                    if (intpay == "On Maturity")
                    {
                        if (PTYPE == "Days" || PTYPE == "idvasa")
                        {
                            interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 365) * (Period));
                            maturityamt = (interest) + (amt);
                            TxtIntrest.Text = interest.ToString();
                            TxtMaturity.Text = maturityamt.ToString();
                        }
                        else if (PTYPE == "Months" || PTYPE == "maihnao")
                        {
                            interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (Period));
                            maturityamt = (interest) + (amt);
                            TxtIntrest.Text = interest.ToString();
                            TxtMaturity.Text = maturityamt.ToString();
                        }
                    }
                    else if (intpay == "Quaterly")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (3));
                        maturityamt = amt; //Val(interest) + Val(amt);                    
                        TxtIntrest.Text = interest.ToString();
                        TxtMaturity.Text = maturityamt.ToString();
                    }
                    else if (intpay == "Monthly" || PTYPE == "maihano")
                    {
                        interest = Convert.ToInt32((amt) * (intrate) / 1209.75);
                        maturityamt = amt; //Val(interest) + Val(amt);
                        //maturityamt = interest + amt;
                        TxtIntrest.Text = interest.ToString();
                        TxtMaturity.Text = maturityamt.ToString();
                    }
                    break;

                case "RD":
                    float deamt = amt;
                    float tempamt = amt;
                    Period = Period;
                    string SQL = "";
                    float temprate = intrate;
                    conn.sExecuteQuery("delete from RRD");

                    string RDPARA = conn.sExecuteScalar("SELECT LISTVALUE FROM PARAMETER WHERE LISTFIELD='RDINT'");
                    //float RDPARA = Convert.ToInt32(RDPARAstring);

                    if (RDPARA == "")
                    {
                        WebMsgBox.Show("Create parameter RDINT AND VALUE Monthly(M) or Quaterly(Q)", this.Page);
                        return;
                    }
                    else
                    {
                        if (RDPARA == "M")
                        {
                            int i = 1;
                            int srno = 1;
                            float deamt1 = deamt;
                            for (i = 1; i <= (Period / 1); i++)
                            {
                                float int1 = (deamt1 * temprate) / 1200;
                                SQL = "insert into RRD(srno,Install,Balance,interest) values(' " + srno + " ' , ' " + deamt + " ' , ' " + deamt1 + " ' , ' " + int1 + " ' )";
                                conn.sExecuteQuery(SQL);
                                srno = srno + 1;
                                deamt1 = deamt1 + deamt;
                                float int2 = (deamt1 * temprate) / 1200;
                            }

                            SQL = "select sum(interest) from RRD";
                            string interest1 = conn.sExecuteScalar(SQL);
                            double MAMT = Convert.ToDouble(deamt * Period) + Convert.ToDouble(interest1);
                            //maturityamt = (deamt * Period) +interest1F;
                            TxtIntrest.Text = interest1.ToString();
                            TxtMaturity.Text = MAMT.ToString();
                        }
                        else if (RDPARA == "Q")
                        {
                            int i = 1;
                            int srno = 1;
                            float deamt1 = deamt;

                            for (i = 1; i <= (Period / 3); i++)
                            {
                                float int1 = (deamt1 * temprate) / 1200;

                                SQL = "insert into RRD(srno,Install,Balance,interest) values('" + srno + "' , '" + deamt + "' , '" + deamt1 + "' , '" + int1 + "')";
                                conn.sExecuteQuery(SQL);
                                srno = srno + 1;
                                deamt1 = deamt1 + deamt;
                                float int2 = (deamt1 * temprate) / 1200;

                                SQL = "insert into RRD(srno,Install,Balance,interest) values('" + srno + "' , '" + deamt + "' , '" + deamt1 + "' , '" + int2 + "' )";
                                conn.sExecuteQuery(SQL);
                                srno = srno + 1;

                                deamt1 = deamt1 + deamt;
                                float int3 = (deamt1 * temprate) / 1200;

                                SQL = "insert into RRD(srno,Install,Balance,interest) values('" + srno + "' , '" + deamt + "' , '" + deamt1 + "' , '" + int3 + "' )";
                                conn.sExecuteReader(SQL);
                                srno = srno + 1;

                                float TOTINT = int1 + int2 + int3;
                                deamt1 = deamt1 + deamt + TOTINT;
                                float int4 = (deamt1 * temprate) / 1200;
                            }
                            SQL = "select sum(interest) from RRD";
                            interest = Convert.ToInt32(conn.sExecuteScalar(SQL));
                            maturityamt = (deamt * Period) + interest;
                            TxtIntrest.Text = interest.ToString();
                            TxtMaturity.Text = maturityamt.ToString();
                        }
                    }
                    break;

                default:
                    WebMsgBox.Show("Record not found", this.Page);
                    break;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void MisIntTrfOnly()
    {
        try
        {
            string CN = "", CDt = "";
            string PAYMAST = "MISINTTRF";
            string SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString(); //CMN.GetSetNoAll(Session["BRCD"].ToString(), "4");
            string CD, CD1;
            double INTPA, INTPAID;
            string Acti = "", PMTMD = "";
            CD = CD1 = "";
            INTPA = Convert.ToDouble(TxtIntrestPaybl.Text);
            if (INTPA > 0)
            {
                CD = "2";
            }
            else
            {
                CD = "1";
            }
            if (ddlPayType.SelectedValue == "1") // ********  Cash In Hand Interest
            {

                string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());

                if (TxtIntrestPaybl.Text != "" || TxtIntrestPaybl.Text != "0")
                {
                    INTPAID = Math.Abs(Convert.ToDouble(TxtIntrestPaybl.Text));
                    //changed abhishek
                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
                                        , txtAccNo.Text, "BY INEREST", "INEREST AGAINST/" + TxtProcode.Text + "/" + txtAccNo.Text, INTPAID.ToString(), CD, "4", "CP", SetNo, CN, CDt, "",
                                        Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                    PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                    if (resultintrst > 0)
                    {
                        resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", cgl,
                                   txtAccNo.Text, "TO CASH", "CASH AGAINST" + TxtProcode.Text, txtPayAmnt.Text.ToString(), "1", "4", "CP", SetNo, CN, CDt, "",
                                   Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                  PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                    }

                }
            }
            else if (ddlPayType.SelectedValue == "2") // ********  Transfer Activity Interest
            {
                string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());

                if (TxtIntrestPaybl.Text != "" || TxtIntrestPaybl.Text != "0")
                {
                    INTPAID = Math.Abs(Convert.ToDouble(TxtIntrestPaybl.Text));
                    //changed abhishek
                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
                                        , txtAccNo.Text, "BY INEREST", "INEREST AGAINST/" + TxtProcode.Text + "/" + txtAccNo.Text, INTPAID.ToString(), CD, "7", "TR", SetNo, CN, CDt, "",
                                        Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                    PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                    if (resultintrst > 0)
                    {
                        resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode1"].ToString(), Txtprocode1.Text,
                                                        TxtAccNo1.Text, "TRF FROM" + TxtProcode.Text + "/" + txtAccNo.Text, "TRANSFER AGAINST" + TxtProcode.Text, txtPayAmnt.Text.ToString(), "1", "7", "TR", SetNo, CN, CDt, "",
                                                        Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                                        PAYMAST, Txtcustno.Text.ToString(), TxtCustName1.Text, ViewState["RID"].ToString(), "0");
                    }

                }


            }
            else if (ddlPayType.SelectedValue == "4")
            {
                string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());

                if (TxtIntrestPaybl.Text != "" || TxtIntrestPaybl.Text != "0")
                {
                    INTPAID = Math.Abs(Convert.ToDouble(TxtIntrestPaybl.Text));
                    //changed abhishek
                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
                                        , txtAccNo.Text, "BY INEREST", "INEREST AGAINST/" + TxtProcode.Text + "/" + txtAccNo.Text, INTPAID.ToString(), CD, "5", "TR", SetNo, CN, CDt, "",
                                        Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                    PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                    if (resultintrst > 0)
                    {
                        resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode1"].ToString(), Txtprocode1.Text,
                                                        TxtAccNo1.Text, "trf from " + TxtProcode.Text + "/" + txtAccNo.Text, "TRANSFER AGAINST" + TxtProcode.Text, txtPayAmnt.Text.ToString(), "1", "5", "TR", SetNo, TxtChequeNo.Text, TxtChequeDate.Text, "",
                                                        Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                                        PAYMAST, Txtcustno.Text.ToString(), TxtCustName1.Text, ViewState["RID"].ToString(), "0");
                    }

                }
            }
            if (resultintrst > 0)
            {

                // BindGrid();
                lblMessage.Text = "Your Reciept With SetNo : " + SetNo;
                ModalPopup.Show(this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DepositCloser_FDReceipt_" + TxtProcode + "_" + txtAccNo + "_" + txtPayAmnt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                clear();
            }


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void Multiple_TDAClose(string PayType)
    {
        try
        {
            string CN = "", CDt = "";
            string PAYMAST = "TDCLOSEMULTIPLE";
            string SetNo = "";
            if (Rdb_NewSetno.Checked == true)
            {
                SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString(); //CMN.GetSetNoAll(Session["BRCD"].ToString(), "4");
            }
            else
            {
                SetNo = Txt_ExistSetno.Text;
            }
            string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());
            double INT, SINT, PRI, TOTAL, INTP, COMMI, ADMINFEE;
            string ACTIVI = "", PMTMD = "";

            if (DdlCrDr.SelectedValue == "2" || Rdb_NewSetno.Checked == true)
            {

                if (Convert.ToDouble(TxtInterestNew.Text.ToString() == "" ? "0" : TxtInterestNew.Text.ToString()) > 0)
                {
                    if (TxtChequeNo.Text == "")
                    {
                        CN = "0";
                    }
                    else
                    {
                        CN = TxtChequeNo.Text;
                    }

                    if (TxtChequeDate.Text == "")
                    {
                        CDt = "01/01/1990";
                    }
                    else
                    {
                        CDt = TxtChequeDate.Text;
                    }
                }
                //if (rn == "Y" && wp == "P")
                //{
                //    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString()
                //                                     , txtAccNo.Text, "TO INTEREST", "TRANSFER FROM " + txtAccNo.Text + "/" + TxtProcode.Text + "/" + txtAccNo.Text, TxtPrincPaybl.Text.ToString(), "1", "7", "TR-INT", SetNo, CN, CDt, "",
                //                                     Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                //                                     PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

                //    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString()
                //                 , txtAccNo.Text.ToString(), "BY INEREST", "INEREST AGAINST/" + TxtProcode.Text + "/" + txtAccNo.Text, TxtPrincPaybl.Text.ToString(), "2", "7", "TR-INT", SetNo, CN, CDt, "",
                //                 Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                //                 PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                //}


                INT = SINT = PRI = TOTAL = 0;
                INT = Convert.ToDouble(TxtInterestNew.Text == "" ? "0" : TxtInterestNew.Text);
                SINT = Convert.ToDouble(TxtSbintrest.Text == "" ? "0" : TxtSbintrest.Text);
                PRI = Convert.ToDouble(TxtPrincPaybl.Text);
                INTP = Convert.ToDouble(TxtIntrest.Text);

                TOTAL = PRI + (INTP - INT) + SINT;




                if (PayType == "1") //Multiple Cash in Hand Closure
                {
                    double INTPA = 0;
                    double INTPAID = 0, DBPRI = 0;
                    string CD, CD1;
                    CD = CD1 = "";
                    INTPA = Convert.ToDouble(TxtIntrestPaybl.Text);
                    if (INTPA != 0 && INTPA < 0)
                    {
                        DBPRI = PRI - Math.Abs(INTPA);
                    }
                    else
                    {
                        DBPRI = PRI;
                    }

                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString()
                           , txtAccNo.Text.ToString(), "BY CASH", "CASH AGAINST" + TxtProcode.Text + "/" + txtAccNo.Text, DBPRI.ToString(), "2", "4", "CP", SetNo, CN, CDt, "",
                           Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                           PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");


                    //resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", cgl,
                    //                txtAccNo.Text, "TO CASH", "CASH AGAINST" + TxtProcode.Text, txtPayAmnt.Text.ToString(), "1", "4", "CP", SetNo, CN, CDt, "",
                    //                Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                    //                PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

                    if (INTPA > 0)   //Postive Int Payable
                    {
                        CD = "2";
                        CD1 = "1";
                    }
                    else            // Negative Int Payable
                    {
                        CD = "1";
                        CD1 = "2";
                    }
                    if (TxtIntrestPaybl.Text != "" && TxtIntrestPaybl.Text != "0")
                    {
                        INTPAID = Math.Abs(Convert.ToDouble(TxtIntrestPaybl.Text));
                        //To divide the Principle by Interest and fire the entry as Acitvit=7 and pmtmode='TR' --Abhishek 03-06-2017
                        if (CD1 != "1")
                        {
                            resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString()
                              , txtAccNo.Text.ToString(), "BY Transfer", "INT TRF AGAINST" + TxtProcode.Text + "/" + txtAccNo.Text, INTPAID.ToString(), CD1, "7", "TR", SetNo, CN, CDt, "",
                              Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                              PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                        }

                        if (CD == "2")
                        {
                            ACTIVI = "4";
                            PMTMD = "CP";
                        }
                        else
                        {
                            ACTIVI = "7";
                            PMTMD = "TR";
                        }

                        //changed abhishek
                        resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
                                         , txtAccNo.Text, "BY CASH", "INEREST TRF AGAINST/" + TxtProcode.Text + "/" + txtAccNo.Text, INTPAID.ToString(), CD, ACTIVI, PMTMD, SetNo, CN, CDt, "",
                                         Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                        PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                    }

                    //Get All Transaction From TemporaryTable(TempMTable) to Main Table
                    DS = new DataTable();
                    DS = ITrans.GetAllTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (DS.Rows.Count > 0)
                    {
                        for (int i = 0; i < DS.Rows.Count; i++)
                        {
                            resultintrst = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DS.Rows[i]["GlCode"].ToString(), DS.Rows[i]["SubGlCode"].ToString(), DS.Rows[i]["AccNo"].ToString(),
                                           DS.Rows[i]["Particulars"].ToString(), DS.Rows[i]["Particulars2"].ToString(), Convert.ToDouble(DS.Rows[i]["Amount"].ToString()), DS.Rows[i]["TrxType"].ToString(), DS.Rows[i]["Activity"].ToString(), DS.Rows[i]["PmtMode"].ToString(), SetNo, "0", "01/01/1900", "0", "0", "1001",
                                           "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", DS.Rows[i]["CustNo"].ToString(), DS.Rows[i]["CustName"].ToString(), "0");
                        }

                        //Get All Transaction From Temporary Table(TempLnTrx)
                        DS = new DataTable();
                        DS = ITrans.GetAllLnTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                        if (DS.Rows.Count > 0)
                        {
                            for (int i = 0; i < DS.Rows.Count; i++)
                            {
                                resultintrst = ITrans.LoanTrx(Session["BRCD"].ToString(), DS.Rows[i]["LoanGlCode"].ToString(), DS.Rows[i]["SubGlCode"].ToString(), DS.Rows[i]["AccountNo"].ToString(), DS.Rows[i]["HeadDesc"].ToString(), DS.Rows[i]["TrxType"].ToString(), DS.Rows[i]["Activity"].ToString(), DS.Rows[i]["Narration"].ToString(), DS.Rows[i]["Amount"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), "0");
                            }

                            //Update Last Interest Date only when Int_App is "1" (Get Int_App From LoanGl)
                            result = CurrentCls1.UpdateLoanIntDate(Session["BRCD"].ToString(), DS.Rows[0]["LoanGlCode"].ToString(), DS.Rows[0]["AccountNo"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                        }
                    }
                    else
                    {

                    }
                }//Multiple Cash In Hand Endsss.............................




                else if (PayType == "2")// Multiple Transfer Closure Starst........................
                {
                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString()
                             , txtAccNo.Text.ToString(), "TRANSFER", "FTRANSFER AGAINST" + TxtProcode.Text + "/" + txtAccNo.Text, PRI.ToString(), "2", "7", "TR", SetNo, CN, CDt, "",
                             Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                            PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");


                    //resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode1"].ToString(), Txtprocode1.Text,
                    //                                    TxtAccNo1.Text, "trf from " + TxtProcode.Text + "/" + txtAccNo.Text + " - " + TxtTNarration.Text, "TRANSFER AGAINST" + TxtProcode.Text, txtPayAmnt.Text.ToString(), "1", "7", "TR", SetNo, CN, CDt, "",
                    //                                    Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                    //                                    PAYMAST, Txtcustno.Text.ToString(), TxtCustName1.Text, ViewState["RID"].ToString(), "0");
                    double INTPA = 0;
                    double INTPAID = 0;
                    string CD, CD1;
                    CD = CD1 = "";
                    INTPA = Convert.ToDouble(TxtIntrestPaybl.Text);
                    if (INTPA > 0)
                    {
                        CD = "2";
                    }
                    else
                    {
                        CD = "1";
                    }
                    if (TxtIntrestPaybl.Text != "" && TxtIntrestPaybl.Text != "0")
                    {
                        INTPAID = Math.Abs(Convert.ToDouble(TxtIntrestPaybl.Text));
                        //changed abhishek
                        resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
                                         , txtAccNo.Text, "BY INEREST", "INEREST AGAINST/" + TxtProcode.Text + "/" + txtAccNo.Text, INTPAID.ToString(), CD, "7", "TR", SetNo, CN, CDt, "",
                                         Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                         PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                    }

                    //Get All Transaction From TemporaryTable(TempMTable) to Main Table
                    DS = new DataTable();
                    DS = ITrans.GetAllTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (DS.Rows.Count > 0)
                    {
                        for (int i = 0; i < DS.Rows.Count; i++)
                        {
                            resultintrst = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DS.Rows[i]["GlCode"].ToString(), DS.Rows[i]["SubGlCode"].ToString(), DS.Rows[i]["AccNo"].ToString(),
                                           DS.Rows[i]["Particulars"].ToString(), DS.Rows[i]["Particulars2"].ToString(), Convert.ToDouble(DS.Rows[i]["Amount"].ToString()), DS.Rows[i]["TrxType"].ToString(), DS.Rows[i]["Activity"].ToString(), DS.Rows[i]["PmtMode"].ToString(), SetNo, "0", "01/01/1900", "0", "0", "1001",
                                           "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", DS.Rows[i]["CustNo"].ToString(), DS.Rows[i]["CustName"].ToString(), "0");
                        }

                        //Get All Transaction From Temporary Table(TempLnTrx)
                        DS = new DataTable();
                        DS = ITrans.GetAllLnTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                        if (DS.Rows.Count > 0)
                        {
                            for (int i = 0; i < DS.Rows.Count; i++)
                            {
                                resultintrst = ITrans.LoanTrx(Session["BRCD"].ToString(), DS.Rows[i]["LoanGlCode"].ToString(), DS.Rows[i]["SubGlCode"].ToString(), DS.Rows[i]["AccountNo"].ToString(), DS.Rows[i]["HeadDesc"].ToString(), DS.Rows[i]["TrxType"].ToString(), DS.Rows[i]["Activity"].ToString(), DS.Rows[i]["Narration"].ToString(), DS.Rows[i]["Amount"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), "0");
                            }

                            //Update Last Interest Date only when Int_App is "1" (Get Int_App From LoanGl)
                            result = CurrentCls1.UpdateLoanIntDate(Session["BRCD"].ToString(), DS.Rows[0]["LoanGlCode"].ToString(), DS.Rows[0]["AccountNo"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                        }
                    }
                }//Multiple Transfer Endss...........................




                else if (PayType == "3")// Multiple Cheque Closure Starts..........................................
                {
                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString()
                             , txtAccNo.Text.ToString(), "TO CHEQUE", "FTRANSFER AGAINST" + TxtProcode.Text + "/" + txtAccNo.Text, PRI.ToString(), "2", "5", "TR", SetNo, CN, CDt, "",
                             Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                            PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");


                    //resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode1"].ToString(), Txtprocode1.Text,
                    //                                    TxtAccNo1.Text, "trf from " + TxtProcode.Text + "/" + txtAccNo.Text + " - " + TxtTNarration.Text, "TRANSFER AGAINST" + TxtProcode.Text, txtPayAmnt.Text.ToString(), "1", "5", "TR", SetNo, CN, CDt, "",
                    //                                    Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                    //                                    PAYMAST, Txtcustno.Text.ToString(), TxtCustName1.Text, ViewState["RID"].ToString(), "0");
                    double INTPA = 0;
                    double INTPAID = 0;
                    string CD, CD1;
                    CD = CD1 = "";
                    INTPA = Convert.ToDouble(TxtIntrestPaybl.Text);
                    if (INTPA > 0)
                    {
                        CD = "2";
                    }
                    else
                    {
                        CD = "1";
                    }
                    if (TxtIntrestPaybl.Text != "" && TxtIntrestPaybl.Text != "0")
                    {
                        INTPAID = Math.Abs(Convert.ToDouble(TxtIntrestPaybl.Text));
                        //changed abhishek
                        resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
                                         , txtAccNo.Text, "BY INEREST", "INEREST AGAINST " + TxtProcode.Text + "/" + txtAccNo.Text, INTPAID.ToString(), CD, "5", "TR", SetNo, CN, CDt, "",
                                         Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                         PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                    }

                    //Get All Transaction From TemporaryTable(TempMTable) to Main Table
                    DS = new DataTable();
                    DS = ITrans.GetAllTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (DS.Rows.Count > 0)
                    {
                        for (int i = 0; i < DS.Rows.Count; i++)
                        {
                            resultintrst = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DS.Rows[i]["GlCode"].ToString(), DS.Rows[i]["SubGlCode"].ToString(), DS.Rows[i]["AccNo"].ToString(),
                                           DS.Rows[i]["Particulars"].ToString(), DS.Rows[i]["Particulars2"].ToString(), Convert.ToDouble(DS.Rows[i]["Amount"].ToString()), DS.Rows[i]["TrxType"].ToString(), DS.Rows[i]["Activity"].ToString(), DS.Rows[i]["PmtMode"].ToString(), SetNo, "0", "01/01/1900", "0", "0", "1001",
                                           "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", DS.Rows[i]["CustNo"].ToString(), DS.Rows[i]["CustName"].ToString(), "0");
                        }

                        //Get All Transaction From Temporary Table(TempLnTrx)
                        DS = new DataTable();
                        DS = ITrans.GetAllLnTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                        if (DS.Rows.Count > 0)
                        {
                            for (int i = 0; i < DS.Rows.Count; i++)
                            {
                                resultintrst = ITrans.LoanTrx(Session["BRCD"].ToString(), DS.Rows[i]["LoanGlCode"].ToString(), DS.Rows[i]["SubGlCode"].ToString(), DS.Rows[i]["AccountNo"].ToString(), DS.Rows[i]["HeadDesc"].ToString(), DS.Rows[i]["TrxType"].ToString(), DS.Rows[i]["Activity"].ToString(), DS.Rows[i]["Narration"].ToString(), DS.Rows[i]["Amount"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), "0");
                            }

                            //Update Last Interest Date only when Int_App is "1" (Get Int_App From LoanGl)
                            result = CurrentCls1.UpdateLoanIntDate(Session["BRCD"].ToString(), DS.Rows[0]["LoanGlCode"].ToString(), DS.Rows[0]["AccountNo"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                        }
                    }
                }//Multiple Cheque Closure Ends................................


                else if (PayType == "4") //Multiple Renewal 
                {
                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString()
                            , txtAccNo.Text.ToString(), "TRANSFER", "FTRANSFER AGAINST" + TxtProcode.Text + "/" + txtAccNo.Text, PRI.ToString(), "2", "11", "TRR", SetNo, CN, CDt, "",
                            Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                           PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");


                    //resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode1"].ToString(), Txtprocode1.Text,
                    //                                    TxtAccNo1.Text, "trf from " + TxtProcode.Text + "/" + txtAccNo.Text + " - " + TxtTNarration.Text, "TRANSFER AGAINST" + TxtProcode.Text, txtPayAmnt.Text.ToString(), "1", "7", "TR", SetNo, CN, CDt, "",
                    //                                    Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                    //                                    PAYMAST, Txtcustno.Text.ToString(), TxtCustName1.Text, ViewState["RID"].ToString(), "0");
                    double INTPA = 0;
                    double INTPAID = 0;
                    string CD, CD1;
                    CD = CD1 = "";
                    INTPA = Convert.ToDouble(TxtIntrestPaybl.Text);
                    if (INTPA > 0)
                    {
                        CD = "2";
                    }
                    else
                    {
                        CD = "1";
                    }
                    if (TxtIntrestPaybl.Text != "" && TxtIntrestPaybl.Text != "0")
                    {
                        INTPAID = Math.Abs(Convert.ToDouble(TxtIntrestPaybl.Text));
                        //changed abhishek
                        resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
                                         , txtAccNo.Text, "BY INEREST", "INEREST AGAINST/" + TxtProcode.Text + "/" + txtAccNo.Text, INTPAID.ToString(), CD, "11", "TRR", SetNo, CN, CDt, "",
                                         Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                         PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                    }

                    //Get All Transaction From TemporaryTable(TempMTable) to Main Table
                    DS = new DataTable();
                    DS = ITrans.GetAllTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (DS.Rows.Count > 0)
                    {
                        for (int i = 0; i < DS.Rows.Count; i++)
                        {
                            resultintrst = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DS.Rows[i]["GlCode"].ToString(), DS.Rows[i]["SubGlCode"].ToString(), DS.Rows[i]["AccNo"].ToString(),
                                           DS.Rows[i]["Particulars"].ToString(), DS.Rows[i]["Particulars2"].ToString(), Convert.ToDouble(DS.Rows[i]["Amount"].ToString()), DS.Rows[i]["TrxType"].ToString(), DS.Rows[i]["Activity"].ToString(), DS.Rows[i]["PmtMode"].ToString(), SetNo, "0", "01/01/1900", "0", "0", "1001",
                                           "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", DS.Rows[i]["CustNo"].ToString(), DS.Rows[i]["CustName"].ToString(), "0");
                        }

                        //Get All Transaction From Temporary Table(TempLnTrx)
                        DS = new DataTable();
                        DS = ITrans.GetAllLnTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                        if (DS.Rows.Count > 0)
                        {
                            for (int i = 0; i < DS.Rows.Count; i++)
                            {
                                resultintrst = ITrans.LoanTrx(Session["BRCD"].ToString(), DS.Rows[i]["LoanGlCode"].ToString(), DS.Rows[i]["SubGlCode"].ToString(), DS.Rows[i]["AccountNo"].ToString(), DS.Rows[i]["HeadDesc"].ToString(), DS.Rows[i]["TrxType"].ToString(), DS.Rows[i]["Activity"].ToString(), DS.Rows[i]["Narration"].ToString(), DS.Rows[i]["Amount"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), "0");
                            }

                            //Update Last Interest Date only when Int_App is "1" (Get Int_App From LoanGl)
                            result = CurrentCls1.UpdateLoanIntDate(Session["BRCD"].ToString(), DS.Rows[0]["LoanGlCode"].ToString(), DS.Rows[0]["AccountNo"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                        }
                    }



                }//Multiple Renewal Ends.............................................
            }

            else
            {
                if (PayType == "1")
                {

                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", cgl,
                                                   "0", "TO CASH", "CASH AGAINST MULTIPLE TDA CLOSURE", txtPayAmnt.Text, "1", "4",
                                                   "CP", SetNo, "0", "1990-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0",
                                                   PAYMAST, "0", "NA", ViewState["RID"].ToString(), "0");
                }
                else if (PayType == "2")
                {
                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode1"].ToString(), Txtprocode1.Text,
                                                       TxtAccNo1.Text, "trf from Multiple TDA Closure" + TxtTNarration.Text, "TRANSFER AGAINST MULTIPLE TDA CLOSURE", txtPayAmnt.Text.ToString(), "1", "7", "TR", SetNo, CN, CDt, "",
                                                       Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0",
                                                       PAYMAST, "0", "NA", ViewState["RID"].ToString(), "0");
                }
                else if (PayType == "3")
                {
                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode1"].ToString(), Txtprocode1.Text,
                                                       TxtAccNo1.Text, "trf from Multiple TDA Closure" + TxtTNarration.Text, "TRANSFER AGAINST MULTIPLE TDA CLOSURE", txtPayAmnt.Text.ToString(), "1", "5", "TR", SetNo, CN, CDt, "",
                                                       Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                                       PAYMAST, "0", "NA", ViewState["RID"].ToString(), "0");
                }
                else if (PayType == "4")
                {
                    string AFEE = "0", AFEESUBGL = "0", COMMchg = "0", COMMSBGL = "0";
                    string AC = CurrentCls1.FnMultipleClose_Opr("GETACCTYPE", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Txt_ExistSetno.Text);
                    string ExistingRefId = CurrentCls1.FnMultipleClose_Opr("GETEXISTSREFID", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Txt_ExistSetno.Text);
                    if (AC != null)
                    {
                        string[] ACC = AC.Split('_');

                        string IntPostYpe = "";
                        if (Convert.ToDouble(TxtIntrestPaybl.Text) < 0)
                        {
                            IntPostYpe = "REV";
                        }
                        else
                        {
                            IntPostYpe = "NOR";
                        }
                        string url = "FrmTDARenewN.aspx?IPostType=" + IntPostYpe + "&MS=Y&TPAMT=" + txtPayAmnt.Text + "&INTPAY=" + ViewState["SumAmountInt"].ToString() + "&ACCT=" + ACC[0].ToString() + "&CT=" + ACC[1].ToString() + "&SNO=" + Txt_ExistSetno.Text + "&OPR=" + AC[2].ToString() + "&REFID=" + ExistingRefId + "";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + url + "', 'popup_window', 'width=1150,height=600,left=50,top=50,resizable=no');", true);
                        BindGrid1();
                        clear();
                        return;
                    }
                }
            }
            if (resultintrst > 0)
            {
                int sts = 0;
                if (DdlCrDr.SelectedValue == "2" || Rdb_NewSetno.Checked == true)
                    sts = CurrentCls.UpdateAcc(ViewState["GLCODE"].ToString(), TxtProcode.Text, txtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "0");

                if (sts > 0 || DdlCrDr.SelectedValue == "1")
                {
                    //Added By Amol on 03/07/2017 for dds to loan
                    ViewState["LoanAmt"] = "0";
                    ITrans.DelAllRecTable(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    lblMessage.Text = "Your Reciept With SetNo : " + SetNo;
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DepositCloser_FDReceipt_" + TxtProcode + "_" + txtAccNo + "_" + txtPayAmnt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                    string SumAmount = CurrentCls1.FnMultipleClose_Opr("GETSUM", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Txt_ExistSetno.Text);
                    if (DdlCrDr.SelectedValue == "2" || Rdb_NewSetno.Checked == true)
                    {
                        if (SumAmount != null)
                        {
                            ViewState["SumAmount"] = SumAmount.ToString();
                        }
                    }
                    else
                    {
                        ViewState["SumAmount"] = "0";
                    }

                    string SumAmountInt = CurrentCls1.FnMultipleClose_Opr("GETSUMINT", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Txt_ExistSetno.Text);
                    if (DdlCrDr.SelectedValue == "2" || Rdb_NewSetno.Checked == true)
                    {
                        if (SumAmountInt != null)
                        {
                            ViewState["SumAmountInt"] = SumAmountInt.ToString();
                        }
                    }
                    else
                    {
                        ViewState["SumAmountInt"] = "0";
                    }



                    BtnPostInt.Enabled = true;
                }

                clear();
            }


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void PaidInt(string PLACC, string ACCNO, string rn, string wp)
    {
        try
        {
            string CN = "", CDt = "";
            string PAYMAST = "TDCLOSE";
            string SetNo = string.Empty;
            if (ddlPayType.SelectedValue == "7")
                SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "IBTSetNo", Session["BRCD"].ToString()).ToString();
            else
                SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString(); //CMN.GetSetNoAll(Session["BRCD"].ToString(), "4");

            if (Convert.ToDouble(TxtInterestNew.Text.ToString() == "" ? "0" : TxtInterestNew.Text.ToString()) > 0)
            {
                if (TxtChequeNo.Text == "")
                {
                    CN = "0";
                }
                else
                {
                    CN = TxtChequeNo.Text;
                }

                if (TxtChequeDate.Text == "")
                {
                    CDt = "01/01/1990";
                }
                else
                {
                    CDt = TxtChequeDate.Text;
                }
            }
            if (rn == "Y" && wp == "P")
            {
                resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString()
                                                 , ACCNO, "TO INTEREST", "TRANSFER FROM " + txtAccNo.Text + "/" + TxtProcode.Text + "/" + ACCNO, TxtPrincPaybl.Text.ToString(), "1", "7", "TR-INT", SetNo, CN, CDt, "",
                                                 Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", Session["BRCD"].ToString(),
                                                 PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

                resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString()
                             , txtAccNo.Text.ToString(), "BY INEREST", "INEREST AGAINST/" + TxtProcode.Text + "/" + txtAccNo.Text, TxtPrincPaybl.Text.ToString(), "2", "7", "TR-INT", SetNo, CN, CDt, "",
                             Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", Session["BRCD"].ToString(),
                             PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
            }

            string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());
            double INT, SINT, PRI, TOTAL, INTP, COMMI, ADMINFEE;
            string ACTIVI = "", PMTMD = "";
            INT = SINT = PRI = TOTAL = 0;
            INT = Convert.ToDouble(TxtInterestNew.Text == "" ? "0" : TxtInterestNew.Text);
            SINT = Convert.ToDouble(TxtSbintrest.Text == "" ? "0" : TxtSbintrest.Text);
            PRI = Convert.ToDouble(TxtPrincPaybl.Text);
            INTP = Convert.ToDouble(TxtIntrest.Text);

            TOTAL = PRI + (INTP - INT) + SINT;
            //TOTAL = (PRI + (INTP - INT) + SINT) - (Convert.ToDouble(ViewState["GLCODE"].ToString()=="5"?"0":COMMI.ToString()) + ADMINFEE);



            if (rn == "N")
            {
                if (ddlPayType.SelectedValue == "1")   // **** For Cash In Hand Closure
                {
                    double INTPA = 0;
                    double INTPAID = 0, DBPRI = 0;
                    string CD, CD1;
                    CD = CD1 = "";
                    INTPA = Convert.ToDouble(TxtIntrestPaybl.Text);
                    if (INTPA != 0 && INTPA < 0)
                    {
                        DBPRI = PRI - Math.Abs(INTPA);
                    }
                    else
                    {
                        DBPRI = PRI;
                    }

                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString()
                           , txtAccNo.Text.ToString(), "BY CASH", "CASH AGAINST" + TxtProcode.Text + "/" + txtAccNo.Text, DBPRI.ToString(), "2", "4", "CP", SetNo, CN, CDt, "",
                           Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", Session["BRCD"].ToString(),
                           PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");


                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", cgl,
                                    txtAccNo.Text, "TO CASH", "CASH AGAINST" + TxtProcode.Text, txtPayAmnt.Text.ToString(), "1", "4", "CP", SetNo, CN, CDt, "",
                                    Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", Session["BRCD"].ToString(),
                                    PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

                    if (INTPA > 0)   //Postive Int Payable
                    {
                        CD = "2";
                        CD1 = "1";
                    }
                    else            // Negative Int Payable
                    {
                        CD = "1";
                        CD1 = "2";
                    }
                    if (TxtIntrestPaybl.Text != "" && TxtIntrestPaybl.Text != "0")
                    {
                        INTPAID = Math.Abs(Convert.ToDouble(TxtIntrestPaybl.Text));
                        //To divide the Principle by Interest and fire the entry as Acitvit=7 and pmtmode='TR' --Abhishek 03-06-2017
                        if (CD1 != "1")
                        {
                            resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString()
                              , txtAccNo.Text.ToString(), "BY Transfer", "INT TRF AGAINST" + TxtProcode.Text + "/" + txtAccNo.Text, INTPAID.ToString(), CD1, "7", "TR", SetNo, CN, CDt, "",
                              Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", Session["BRCD"].ToString(),
                              PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                        }

                        if (CD == "2")
                        {
                            ACTIVI = "4";
                            PMTMD = "CP";
                        }
                        else
                        {
                            ACTIVI = "7";
                            PMTMD = "TR";
                        }

                        //changed abhishek
                        resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
                                         , txtAccNo.Text, "BY CASH", "INEREST TRF AGAINST/" + TxtProcode.Text + "/" + txtAccNo.Text, INTPAID.ToString(), CD, ACTIVI, PMTMD, SetNo, CN, CDt, "",
                                         Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", Session["BRCD"].ToString(),
                                        PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                    }

                    //Get All Transaction From TemporaryTable(TempMTable) to Main Table
                    DS = new DataTable();
                    DS = ITrans.GetAllTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (DS.Rows.Count > 0)
                    {
                        for (int i = 0; i < DS.Rows.Count; i++)
                        {
                            resultintrst = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DS.Rows[i]["GlCode"].ToString(), DS.Rows[i]["SubGlCode"].ToString(), DS.Rows[i]["AccNo"].ToString(),
                                           DS.Rows[i]["Particulars"].ToString(), DS.Rows[i]["Particulars2"].ToString(), Convert.ToDouble(DS.Rows[i]["Amount"].ToString()), DS.Rows[i]["TrxType"].ToString(), DS.Rows[i]["Activity"].ToString(), DS.Rows[i]["PmtMode"].ToString(), SetNo, "0", "01/01/1900", "0", "0", "1001",
                                           "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", DS.Rows[i]["CustNo"].ToString(), DS.Rows[i]["CustName"].ToString(), ViewState["RID"].ToString());
                        }

                        //Get All Transaction From Temporary Table(TempLnTrx)
                        DS = new DataTable();
                        DS = ITrans.GetAllLnTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                        if (DS.Rows.Count > 0)
                        {
                            for (int i = 0; i < DS.Rows.Count; i++)
                            {
                                resultintrst = ITrans.LoanTrx(Session["BRCD"].ToString(), DS.Rows[i]["LoanGlCode"].ToString(), DS.Rows[i]["SubGlCode"].ToString(), DS.Rows[i]["AccountNo"].ToString(), DS.Rows[i]["HeadDesc"].ToString(), DS.Rows[i]["TrxType"].ToString(), "7", DS.Rows[i]["Narration"].ToString(), DS.Rows[i]["Amount"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                            }

                            //Update Last Interest Date only when Int_App is "1" (Get Int_App From LoanGl)
                            result = CurrentCls1.UpdateLoanIntDate(Session["BRCD"].ToString(), DS.Rows[0]["LoanGlCode"].ToString(), DS.Rows[0]["AccountNo"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                        }
                    }
                    else
                    {

                    }
                }

                // *** For TDA Transfer Closure 
                else if (ddlPayType.SelectedValue == "2")
                {

                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString()
                           , txtAccNo.Text.ToString(), "TRANSFER", "FTRANSFER AGAINST" + TxtProcode.Text + "/" + txtAccNo.Text, PRI.ToString(), "2", "7", "TR", SetNo, CN, CDt, "",
                           Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), Session["BRCD"].ToString(),
                          PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");


                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode1"].ToString(), Txtprocode1.Text,
                                                        TxtAccNo1.Text, "trf from " + TxtProcode.Text + "/" + txtAccNo.Text + " - " + TxtTNarration.Text, "TRANSFER AGAINST" + TxtProcode.Text, txtPayAmnt.Text.ToString(), "1", "7", "TR", SetNo, CN, CDt, "",
                                                        Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), Session["BRCD"].ToString(),
                                                        PAYMAST, Txtcustno.Text.ToString(), TxtCustName1.Text, ViewState["RID"].ToString(), "0");
                    double INTPA = 0;
                    double INTPAID = 0;
                    string CD, CD1;
                    CD = CD1 = "";
                    INTPA = Convert.ToDouble(TxtIntrestPaybl.Text);
                    if (INTPA > 0)
                    {
                        CD = "2";
                    }
                    else
                    {
                        CD = "1";
                    }
                    if (TxtIntrestPaybl.Text != "" && TxtIntrestPaybl.Text != "0")
                    {
                        INTPAID = Math.Abs(Convert.ToDouble(TxtIntrestPaybl.Text));
                        //changed abhishek
                        resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
                                         , txtAccNo.Text, "BY INEREST", "INEREST AGAINST/" + TxtProcode.Text + "/" + txtAccNo.Text, INTPAID.ToString(), CD, "7", "TR", SetNo, CN, CDt, "",
                                         Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), Session["BRCD"].ToString(),
                                         PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                    }

                    //Get All Transaction From TemporaryTable(TempMTable) to Main Table
                    DS = new DataTable();
                    DS = ITrans.GetAllTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (DS.Rows.Count > 0)
                    {
                        for (int i = 0; i < DS.Rows.Count; i++)
                        {
                            resultintrst = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DS.Rows[i]["GlCode"].ToString(), DS.Rows[i]["SubGlCode"].ToString(), DS.Rows[i]["AccNo"].ToString(),
                                           DS.Rows[i]["Particulars"].ToString(), DS.Rows[i]["Particulars2"].ToString(), Convert.ToDouble(DS.Rows[i]["Amount"].ToString()), DS.Rows[i]["TrxType"].ToString(), DS.Rows[i]["Activity"].ToString(), DS.Rows[i]["PmtMode"].ToString(), SetNo, "0", "01/01/1900", "0", "0", "1001",
                                           "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", DS.Rows[i]["CustNo"].ToString(), DS.Rows[i]["CustName"].ToString(), ViewState["RID"].ToString());
                        }

                        //Get All Transaction From Temporary Table(TempLnTrx)
                        DS = new DataTable();
                        DS = ITrans.GetAllLnTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                        if (DS.Rows.Count > 0)
                        {
                            for (int i = 0; i < DS.Rows.Count; i++)
                            {
                                resultintrst = ITrans.LoanTrx(Session["BRCD"].ToString(), DS.Rows[i]["LoanGlCode"].ToString(), DS.Rows[i]["SubGlCode"].ToString(), DS.Rows[i]["AccountNo"].ToString(), DS.Rows[i]["HeadDesc"].ToString(), DS.Rows[i]["TrxType"].ToString(), "7", DS.Rows[i]["Narration"].ToString(), DS.Rows[i]["Amount"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                            }

                            //Update Last Interest Date only when Int_App is "1" (Get Int_App From LoanGl)
                            result = CurrentCls1.UpdateLoanIntDate(Session["BRCD"].ToString(), DS.Rows[0]["LoanGlCode"].ToString(), DS.Rows[0]["AccountNo"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                        }
                    }
                }
                else if (ddlPayType.SelectedValue == "7")
                {
                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString()
                             , txtAccNo.Text.ToString(), "TRANSFER", "FTRANSFER AGAINST" + TxtProcode.Text + "/" + txtAccNo.Text, PRI.ToString(), "2", "7", "TR", SetNo, CN, CDt, "",
                             Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), Session["BRCD"].ToString(),
                            "ABB-" + PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                    DataTable DT2 = new DataTable();
                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode1"].ToString(), Txtprocode1.Text,
                                                   TxtAccNo1.Text, "trf from " + TxtProcode.Text + "/" + txtAccNo.Text + " - " + TxtTNarration.Text, "TRANSFER AGAINST" + TxtProcode.Text, txtPayAmnt.Text.ToString(), "1", "7", "TR", SetNo, CN, CDt, "",
                                                  Session["BRCD"].ToString(), "1001", "", txtBrcd.Text, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["BRCD"].ToString(),
                                                   "ABB-" + PAYMAST, Txtcustno.Text.ToString(), TxtCustName1.Text, ViewState["RID"].ToString(), "0");
                    DT2 = CLS.GetADMSubGl("1");
                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(),
                                                   TxtAccNo1.Text, "trf from " + TxtProcode.Text + "/" + txtAccNo.Text + " - " + TxtTNarration.Text, "TRANSFER AGAINST" + TxtProcode.Text, txtPayAmnt.Text.ToString(), "2", "7", "TR", SetNo, CN, CDt, "",
                                                   Session["BRCD"].ToString(), "1001", "", txtBrcd.Text, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["BRCD"].ToString(),
                                                   "ABB-" + PAYMAST, Txtcustno.Text.ToString(), TxtCustName1.Text, ViewState["RID"].ToString(), "0");
                    DT2 = CLS.GetADMSubGl("1");
                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(),
                                                   TxtAccNo1.Text, "trf from " + TxtProcode.Text + "/" + txtAccNo.Text + " - " + TxtTNarration.Text, "TRANSFER AGAINST" + TxtProcode.Text, txtPayAmnt.Text.ToString(), "1", "7", "TR", SetNo, CN, CDt, "",
                                                   Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), Session["BRCD"].ToString(),
                                                   "ABB-" + PAYMAST, Txtcustno.Text.ToString(), TxtCustName1.Text, ViewState["RID"].ToString(), "0");
                    if (Session["BRCD"].ToString() != "1" && txtBrcd.Text != "1")
                    {
                        DT2 = CLS.GetADMSubGl(Session["BRCD"].ToString());
                        resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(),
                                                       TxtAccNo1.Text, "trf from " + TxtProcode.Text + "/" + txtAccNo.Text + " - " + TxtTNarration.Text, "TRANSFER AGAINST" + TxtProcode.Text, txtPayAmnt.Text.ToString(), "2", "7", "TR", SetNo, CN, CDt, "",
                                                       Session["BRCD"].ToString(), "1001", "", "1", Session["MID"].ToString(), Session["BRCD"].ToString(), Session["BRCD"].ToString(),
                                                       "ABB-" + PAYMAST, Txtcustno.Text.ToString(), TxtCustName1.Text, ViewState["RID"].ToString(), "0");
                        DT2 = CLS.GetADMSubGl(txtBrcd.Text);
                        resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(),
                                                       TxtAccNo1.Text, "trf from " + TxtProcode.Text + "/" + txtAccNo.Text + " - " + TxtTNarration.Text, "TRANSFER AGAINST" + TxtProcode.Text, txtPayAmnt.Text.ToString(), "1", "7", "TR", SetNo, CN, CDt, "",
                                                       Session["BRCD"].ToString(), "1001", "", "1", Session["MID"].ToString(), Session["BRCD"].ToString(), Session["BRCD"].ToString(),
                                                       "ABB-" + PAYMAST, Txtcustno.Text.ToString(), TxtCustName1.Text, ViewState["RID"].ToString(), "0");
                    }
                    double INTPA = 0;
                    double INTPAID = 0;
                    string CD, CD1;
                    CD = CD1 = "";
                    INTPA = Convert.ToDouble(TxtIntrestPaybl.Text);
                    if (INTPA > 0)
                    {
                        CD = "2";
                    }
                    else
                    {
                        CD = "1";
                    }
                    if (TxtIntrestPaybl.Text != "" && TxtIntrestPaybl.Text != "0")
                    {
                        INTPAID = Math.Abs(Convert.ToDouble(TxtIntrestPaybl.Text));
                        //changed abhishek
                        resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
                                         , txtAccNo.Text, "BY INEREST", "INEREST AGAINST/" + TxtProcode.Text + "/" + txtAccNo.Text, INTPAID.ToString(), CD, "7", "TR", SetNo, CN, CDt, "",
                                         Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), Session["BRCD"].ToString(),
                                         "ABB-" + PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                    }

                    //Get All Transaction From TemporaryTable(TempMTable) to Main Table
                    DS = new DataTable();
                    DS = ITrans.GetAllTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (DS.Rows.Count > 0)
                    {
                        for (int i = 0; i < DS.Rows.Count; i++)
                        {
                            //resultintrst = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DS.Rows[i]["GlCode"].ToString(), DS.Rows[i]["SubGlCode"].ToString(), DS.Rows[i]["AccNo"].ToString(),
                            //               DS.Rows[i]["Particulars"].ToString(), DS.Rows[i]["Particulars2"].ToString(), Convert.ToDouble(DS.Rows[i]["Amount"].ToString()), DS.Rows[i]["TrxType"].ToString(), DS.Rows[i]["Activity"].ToString(), DS.Rows[i]["PmtMode"].ToString(), SetNo, "0", "01/01/1900", "0",
                            //               "0", "1001","", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", 
                            //               "TDCLOSE", DS.Rows[i]["CustNo"].ToString(), DS.Rows[i]["CustName"].ToString(), ViewState["RID"].ToString());

                            resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DS.Rows[i]["GlCode"].ToString(), DS.Rows[i]["SubGlCode"].ToString(), DS.Rows[i]["AccNo"].ToString(),
                                           DS.Rows[i]["Particulars"].ToString(), DS.Rows[i]["Particulars2"].ToString(), DS.Rows[i]["Amount"].ToString(), DS.Rows[i]["TrxType"].ToString(), DS.Rows[i]["Activity"].ToString(), DS.Rows[i]["PmtMode"].ToString(), SetNo, "0", "01/01/1900", "0",
                                           Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), Session["BRCD"].ToString(),
                                           "ABB-" + PAYMAST, Txtcustno.Text.ToString(), TxtCustName1.Text, ViewState["RID"].ToString(), "0");
                        }

                        //Get All Transaction From Temporary Table(TempLnTrx)
                        DS = new DataTable();
                        DS = ITrans.GetAllLnTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                        if (DS.Rows.Count > 0)
                        {
                            for (int i = 0; i < DS.Rows.Count; i++)
                            {
                                resultintrst = ITrans.LoanTrx(Session["BRCD"].ToString(), DS.Rows[i]["LoanGlCode"].ToString(), DS.Rows[i]["SubGlCode"].ToString(), DS.Rows[i]["AccountNo"].ToString(), DS.Rows[i]["HeadDesc"].ToString(), DS.Rows[i]["TrxType"].ToString(), "7", DS.Rows[i]["Narration"].ToString(), DS.Rows[i]["Amount"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                            }

                            //Update Last Interest Date only when Int_App is "1" (Get Int_App From LoanGl)
                            result = CurrentCls1.UpdateLoanIntDate(Session["BRCD"].ToString(), DS.Rows[0]["LoanGlCode"].ToString(), DS.Rows[0]["AccountNo"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                        }
                    }
                }
                // *** For Cheque Closure
                else
                {
                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString()
                         , txtAccNo.Text.ToString(), "TO CHEQUE", "FTRANSFER AGAINST" + TxtProcode.Text + "/" + txtAccNo.Text, PRI.ToString(), "2", "5", "TR", SetNo, CN, CDt, "",
                         Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", Session["BRCD"].ToString(),
                        PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");


                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode1"].ToString(), Txtprocode1.Text,
                                                        TxtAccNo1.Text, "trf from " + TxtProcode.Text + "/" + txtAccNo.Text + " - " + TxtTNarration.Text, "TRANSFER AGAINST" + TxtProcode.Text, txtPayAmnt.Text.ToString(), "1", "5", "TR", SetNo, CN, CDt, "",
                                                        Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", Session["BRCD"].ToString(),
                                                        PAYMAST, Txtcustno.Text.ToString(), TxtCustName1.Text, ViewState["RID"].ToString(), "0");
                    double INTPA = 0;
                    double INTPAID = 0;
                    string CD, CD1;
                    CD = CD1 = "";
                    INTPA = Convert.ToDouble(TxtIntrestPaybl.Text);
                    if (INTPA > 0)
                    {
                        CD = "2";
                    }
                    else
                    {
                        CD = "1";
                    }
                    if (TxtIntrestPaybl.Text != "" && TxtIntrestPaybl.Text != "0")
                    {
                        INTPAID = Math.Abs(Convert.ToDouble(TxtIntrestPaybl.Text));
                        //changed abhishek
                        resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
                                         , txtAccNo.Text, "BY INEREST", "INEREST AGAINST " + TxtProcode.Text + "/" + txtAccNo.Text, INTPAID.ToString(), CD, "5", "TR", SetNo, CN, CDt, "",
                                         Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", Session["BRCD"].ToString(),
                                         PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                    }

                    //Get All Transaction From TemporaryTable(TempMTable) to Main Table
                    DS = new DataTable();
                    DS = ITrans.GetAllTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (DS.Rows.Count > 0)
                    {
                        for (int i = 0; i < DS.Rows.Count; i++)
                        {
                            resultintrst = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DS.Rows[i]["GlCode"].ToString(), DS.Rows[i]["SubGlCode"].ToString(), DS.Rows[i]["AccNo"].ToString(),
                                           DS.Rows[i]["Particulars"].ToString(), DS.Rows[i]["Particulars2"].ToString(), Convert.ToDouble(DS.Rows[i]["Amount"].ToString()), DS.Rows[i]["TrxType"].ToString(), DS.Rows[i]["Activity"].ToString(), DS.Rows[i]["PmtMode"].ToString(), SetNo, "0", "01/01/1900", "0", "0", "1001",
                                           "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", DS.Rows[i]["CustNo"].ToString(), DS.Rows[i]["CustName"].ToString(), ViewState["RID"].ToString());
                        }

                        //Get All Transaction From Temporary Table(TempLnTrx)
                        DS = new DataTable();
                        DS = ITrans.GetAllLnTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                        if (DS.Rows.Count > 0)
                        {
                            for (int i = 0; i < DS.Rows.Count; i++)
                            {
                                resultintrst = ITrans.LoanTrx(Session["BRCD"].ToString(), DS.Rows[i]["LoanGlCode"].ToString(), DS.Rows[i]["SubGlCode"].ToString(), DS.Rows[i]["AccountNo"].ToString(), DS.Rows[i]["HeadDesc"].ToString(), DS.Rows[i]["TrxType"].ToString(), "7", DS.Rows[i]["Narration"].ToString(), DS.Rows[i]["Amount"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                            }

                            //Update Last Interest Date only when Int_App is "1" (Get Int_App From LoanGl)
                            result = CurrentCls1.UpdateLoanIntDate(Session["BRCD"].ToString(), DS.Rows[0]["LoanGlCode"].ToString(), DS.Rows[0]["AccountNo"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                        }
                    }
                }
            }
            else
            {

                if (rn == "Y" && wp == "PI")
                {

                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString()
                            , txtAccNo.Text.ToString(), "BY CASH", "CASH AGAINST " + TxtProcode.Text + "/" + txtAccNo.Text, PRI.ToString(), "2", "4", "CP", SetNo, CN, CDt, "",
                            Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", Session["BRCD"].ToString(),
                            PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");


                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
                                 , txtAccNo.Text.ToString(), "BY CASH", "CASH AGAINST " + TxtProcode.Text + "/" + txtAccNo.Text, INT.ToString(), "2", "4", "CP", SetNo, CN, CDt, "",
                                 Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", Session["BRCD"].ToString(),
                                 PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");


                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text,
                                                              ACCNO, "TO CASH", "TRANSFER AGAINST " + txtAccNo.Text + TxtProcode.Text, TOTAL.ToString(), "1", "4", "CP", SetNo, CN, CDt, "",
                                                               Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", Session["BRCD"].ToString(),
                                                               PAYMAST, Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                }
            }

            if (resultintrst > 0)
            {
                int sts = CurrentCls.UpdateAcc(ViewState["GLCODE"].ToString(), TxtProcode.Text, txtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "0");

                if (sts > 0)
                {
                    //Added By Amol on 03/07/2017 for dds to loan
                    ViewState["LoanAmt"] = "0";
                    ITrans.DelAllRecTable(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    lblMessage.Text = "Your Reciept With SetNo : " + SetNo;
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DepositCloser_FDReceipt_" + TxtProcode + "_" + txtAccNo + "_" + txtPayAmnt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }

                clear();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    public void PostEntry(string PLACC, string ACCNO, string rn, string wp)
    {
        try
        {
            string SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();

            if (rn == "Y" && wp == "P")
            {
                resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString()
                                                 , ACCNO, "TO INTEREST", "TRANSFER FROM " + txtAccNo.Text + "/" + TxtProcode.Text + "/" + ACCNO, TxtPrincPaybl.Text.ToString(), "1", "7", "TR-INT", SetNo, "0", "01/01/1990", "",
                                                 Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                                 "TDCLOSE", Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");

                resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString()
                             , txtAccNo.Text.ToString(), "BY INEREST", "INEREST AGAINST/" + TxtProcode.Text + "/" + txtAccNo.Text, TxtPrincPaybl.Text.ToString(), "2", "7", "TR-INT", SetNo, "0", "01/01/1990", "",
                             Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                             "TDCLOSE", Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
            }

            string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());
            double INT, SINT, PRI, TOTAL, INTP, COMMI, ADMINFEE;
            string ACTIVI = "", PMTMD = "";
            INT = SINT = PRI = TOTAL = 0;
            INT = Convert.ToDouble(TxtInterestNew.Text == "" ? "0" : TxtInterestNew.Text);
            SINT = Convert.ToDouble(TxtSbintrest.Text == "" ? "0" : TxtSbintrest.Text);
            PRI = Convert.ToDouble(TxtPrincPaybl.Text);
            INTP = Convert.ToDouble(TxtIntrest.Text);

            TOTAL = PRI + (INTP - INT) + SINT;

            if (rn == "N")
            {
                resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString()
                           , txtAccNo.Text.ToString(), "TRANSFER", "FTRANSFER AGAINST" + TxtProcode.Text + "/" + txtAccNo.Text, PRI.ToString(), "2", "7", "TR", SetNo, "0", "01/01/1990", "",
                           Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                          "TDCLOSE", Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");


                if (Convert.ToDouble(txtPayAmnt.Text.Trim().ToString()) > 0)
                {
                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode1"].ToString(), Txtprocode1.Text,
                                                        TxtAccNo1.Text, "trf from " + TxtProcode.Text + "/" + txtAccNo.Text + " - " + TxtTNarration.Text, "TRANSFER AGAINST" + TxtProcode.Text, txtPayAmnt.Text.ToString(), "1", "7", "TR", SetNo, "0", "01/01/1990", "",
                                                        Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                                        "TDCLOSE", Txtcustno.Text.ToString(), TxtCustName1.Text, ViewState["RID"].ToString(), "0");
                }

                double INTPA = 0;
                double INTPAID = 0;
                string CD, CD1;
                CD = CD1 = "";
                INTPA = Convert.ToDouble(TxtIntrestPaybl.Text);
                if (INTPA > 0)
                {
                    CD = "2";
                }
                else
                {
                    CD = "1";
                }
                if (TxtIntrestPaybl.Text != "" && TxtIntrestPaybl.Text != "0")
                {
                    INTPAID = Math.Abs(Convert.ToDouble(TxtIntrestPaybl.Text));
                    //changed abhishek
                    resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
                                     , txtAccNo.Text, "BY INEREST", "INEREST AGAINST/" + TxtProcode.Text + "/" + txtAccNo.Text, INTPAID.ToString(), CD, "7", "TR", SetNo, "0", "01/01/1990", "",
                                     Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                     "TDCLOSE", Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
                }
            }
            else if (rn == "Y" && wp == "PI")
            {

                resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.ToString()
                            , txtAccNo.Text.ToString(), "BY CASH", "CASH AGAINST " + TxtProcode.Text + "/" + txtAccNo.Text, PRI.ToString(), "2", "4", "CP", SetNo, "0", "01/01/1990", "",
                            Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                            "TDCLOSE", Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");


                resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "10", ViewState["IR"].ToString()
                             , txtAccNo.Text.ToString(), "BY CASH", "CASH AGAINST " + TxtProcode.Text + "/" + txtAccNo.Text, INT.ToString(), "2", "4", "CP", SetNo, "0", "01/01/1990", "",
                             Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                             "TDCLOSE", Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");


                resultintrst = POSTV.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text,
                                                          ACCNO, "TO CASH", "TRANSFER AGAINST " + txtAccNo.Text + TxtProcode.Text, TOTAL.ToString(), "1", "4", "CP", SetNo, "0", "01/01/1990", "",
                                                           Session["BRCD"].ToString(), "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "",
                                                           "TDCLOSE", Txtcustno.Text.ToString(), TxtAccname.Text.ToString(), ViewState["RID"].ToString(), "0");
            }

            //Get All Transaction From TemporaryTable(TempMTable) to Main Table
            DS = new DataTable();
            DS = ITrans.GetAllTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

            if (DS.Rows.Count > 0)
            {
                for (int i = 0; i < DS.Rows.Count; i++)
                {
                    resultintrst = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DS.Rows[i]["GlCode"].ToString(), DS.Rows[i]["SubGlCode"].ToString(), DS.Rows[i]["AccNo"].ToString(),
                                   DS.Rows[i]["Particulars"].ToString(), DS.Rows[i]["Particulars2"].ToString(), Convert.ToDouble(DS.Rows[i]["Amount"].ToString()), DS.Rows[i]["TrxType"].ToString(), DS.Rows[i]["Activity"].ToString(), DS.Rows[i]["PmtMode"].ToString(), SetNo, "0", "01/01/1900", "0", "0", "1001",
                                   "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "TDCLOSE", DS.Rows[i]["CustNo"].ToString(), DS.Rows[i]["CustName"].ToString(), ViewState["RID"].ToString());
                }

                //Get All Transaction From Temporary Table(TempLnTrx)
                DS = new DataTable();
                DS = ITrans.GetAllLnTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                if (DS.Rows.Count > 0)
                {
                    for (int i = 0; i < DS.Rows.Count; i++)
                    {
                        resultintrst = ITrans.LoanTrx(Session["BRCD"].ToString(), DS.Rows[i]["LoanGlCode"].ToString(), DS.Rows[i]["SubGlCode"].ToString(), DS.Rows[i]["AccountNo"].ToString(), DS.Rows[i]["HeadDesc"].ToString(), DS.Rows[i]["TrxType"].ToString(), "7", DS.Rows[i]["Narration"].ToString(), DS.Rows[i]["Amount"].ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                    }

                    //Update Last Interest Date only when Int_App is "1" (Get Int_App From LoanGl)
                    result = CurrentCls1.UpdateLoanIntDate(Session["BRCD"].ToString(), DS.Rows[0]["LoanGlCode"].ToString(), DS.Rows[0]["AccountNo"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                }
            }

            if (resultintrst > 0)
            {
                int sts = CurrentCls.UpdateAcc(ViewState["GLCODE"].ToString(), TxtProcode.Text, txtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "0");

                if (sts > 0)
                {
                    //Added By Amol on 03/07/2017 for dds to loan
                    BtnSubmit.Text = "Submit";
                    ViewState["LoanAmt"] = "0";
                    ITrans.DelAllRecTable(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    lblMessage.Text = "Your Reciept With SetNo : " + SetNo;
                    ModalPopup.Show(this.Page);
                }

                clear();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    // Calculate Monthly interest for RD
    protected float CalRDintrestMnth(int count, string dt, string dtr, double intrate)
    {
        float resultfloat = 0;
        try
        {
            string EDT = "";

            //string PCMAC = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.ToString();
            string PCMAC = conn.PCNAME();
            // TRUNCATE temp table
            CMN.truncateTemp();

            for (int i = 1; i < (count + 1); i++)
            {
                if (i == 1)
                {
                    EDT = dtDeposDate.Text.ToString();
                    string[] arraMY = EDT.Split('/');

                    // Get Monthly closing balance
                    double CloseBal = OC.GetOpenClose("CLOSING", arraMY[2].ToString(), arraMY[2].ToString(), TxtProcode.Text.ToString(), txtAccNo.Text.ToString(), Session["BRCD"].ToString(), EDT, "5");
                    double intrstcalculated = CloseBal * intrate / 100 / 12;

                    // Insert into temp payable 
                    CMN.EntryDepPayable(Session["BRCD"].ToString(), TxtProcode.Text.ToString(), Txtcustno.Text.ToString(), txtAccNo.Text.ToString(), dtDeposDate.Text.ToString(),
                                      dt.ToString(), TxtDepoAmt.Text.ToString(), "", "", CloseBal.ToString(), intrate.ToString(), "0", intrstcalculated.ToString(), "", "", dtDeposDate.ToString(), arraMY[1].ToString()
                                      , arraMY[2].ToString(), "", "1001", Session["MID"].ToString(), "", "", PCMAC);
                }
                else if (i == count)
                {
                    EDT = Session["EntryDate"].ToString();
                }
                else
                {
                    string dtsplit = dt.ToString();
                    string[] arrayMonYear = dtsplit.Split(' ');
                    string lastday = (DateTime.DaysInMonth(Convert.ToInt32(arrayMonYear[1]), Convert.ToInt32(arrayMonYear[0]))).ToString();
                    EDT = lastday + "/" + Convert.ToInt32(arrayMonYear[0]) + "/" + Convert.ToInt32(arrayMonYear[1]);
                }
            }

            // Show calculated interest
            TxtInterestNew.Text = CMN.GetDepPayable(TxtProcode.Text.ToString(), txtAccNo.Text.ToString(), Txtcustno.Text.ToString(), Session["BRCD"].ToString());
            return resultfloat;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return -1;
        }
        return resultfloat;
    }

    // Calculate Quartarly interest for RD
    protected float CalRDintrestQrtly(int count, string dt, string dtr, double intrate)
    {
        float resultfloat = 0;
        try
        {

            string EDT = "";
            double quartarlyinterest = 0;
            double Finalintrstcalculated = 0;
            double TempIntrest = 0;

            // string PCMAC = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.ToString();
            string PCMAC = conn.PCNAME();
            // TRUNCATE temp table
            CMN.truncateTemp();

            for (int i = 1; i < (count + 1); i++)
            {
                if (i == 1)
                {
                    EDT = dtDeposDate.Text.ToString();
                    string[] arraMY = EDT.Split('/');

                    // Get Monthly closing balance
                    double CloseBal = OC.GetOpenClose("CLOSING", arraMY[2].ToString(), arraMY[2].ToString(), TxtProcode.Text.ToString(), txtAccNo.Text.ToString(), Session["BRCD"].ToString(), EDT, "5");
                    double intrstcalculated = CloseBal * intrate / 100 / 12;

                    string QintrstTotal = dt.ToString();
                    string[] MonthNo = (QintrstTotal.Split(' '));

                    // Addition 3 months interest
                    if (Convert.ToInt32(MonthNo[0]) == 3 || Convert.ToInt32(MonthNo[0]) == 6 || Convert.ToInt32(MonthNo[0]) == 9 || Convert.ToInt32(MonthNo[0]) == 12)
                    {

                        Finalintrstcalculated = TempIntrest;
                        CloseBal = CloseBal + Finalintrstcalculated;
                        TempIntrest = 0;
                    }
                    else
                    {

                        TempIntrest = TempIntrest + intrstcalculated;
                        Finalintrstcalculated = TempIntrest;
                    }

                    // Insert into temp payable 
                    CMN.EntryDepPayable(Session["BRCD"].ToString(), TxtProcode.Text.ToString(), Txtcustno.Text.ToString(), txtAccNo.Text.ToString(), dtDeposDate.Text.ToString(),
                                      dt.ToString(), TxtDepoAmt.Text.ToString(), "", "", CloseBal.ToString(), intrate.ToString(), "0", Finalintrstcalculated.ToString(), "", "", dtDeposDate.ToString(), arraMY[1].ToString()
                                      , arraMY[2].ToString(), "", "1001", Session["MID"].ToString(), "", "", PCMAC);
                }
                else if (i == count)
                {
                    EDT = Session["EntryDate"].ToString();
                }
                else
                {
                    string dtsplit = dt.ToString();
                    string[] arrayMonYear = dtsplit.Split(' ');
                    string lastday = (DateTime.DaysInMonth(Convert.ToInt32(arrayMonYear[1]), Convert.ToInt32(arrayMonYear[0]))).ToString();
                    EDT = lastday + "/" + Convert.ToInt32(arrayMonYear[0]) + "/" + Convert.ToInt32(arrayMonYear[1]);
                }
            }

            // Show calculated interest
            TxtInterestNew.Text = CMN.GetDepPayable(TxtProcode.Text.ToString(), txtAccNo.Text.ToString(), Txtcustno.Text.ToString(), Session["BRCD"].ToString());
            return resultfloat;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return -1;
        }
        return resultfloat;
    }

    // Calculate Final Payable Amount
    protected void CalPaybleAmount(decimal PrinPaybl, decimal IntrstPayble, decimal CalIntrst, decimal SbPenalIntrst)
    {
        try
        {
            if (ViewState["MATURITY"].ToString() == "MA")
            {
                decimal PrinciplePayble = PrinPaybl + (CalIntrst - IntrstPayble) + SbPenalIntrst;
                //TxtPaybleAmount0.Text = PrinciplePayble.ToString();
                return;
            }

            else if (ViewState["MATURITY"].ToString() == "PRE")
            {
                decimal PrinciplePayble = PrinPaybl + (CalIntrst - IntrstPayble) + SbPenalIntrst;
                //TxtPaybleAmount0.Text = PrinciplePayble.ToString();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearPayInfo()
    {
        try
        {
            Txtprocode1.Text = "";
            Txtglcode.Text = "";
            TxtAccNo1.Text = "";
            TxtCustName1.Text = "";
            TxtChequeNo.Text = "";
            TxtChequeDate.Text = "";
            txtLoanTotAmt.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void Photo_Sign()
    {
        try
        {
            string FileName = "";
            DataTable dt = CMN.ShowIMAGE(Txtcustno.Text, Session["BRCD"].ToString(), txtAccNo.Text);
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                String FilePath = "";
                byte[] bytes = null;
                for (int y = 0; y < 2; y++)
                {
                    if (y == 0)
                    {
                        FilePath = dt.Rows[i]["SignIMG"].ToString();
                        if (FilePath != "")
                            bytes = (byte[])dt.Rows[i]["SignIMG"];

                    }
                    else
                    {
                        FilePath = dt.Rows[i]["PhotoImg"].ToString();
                        if (FilePath != "")
                            bytes = (byte[])dt.Rows[i]["PhotoImg"];
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
                    else
                    {
                        if (y == 0)
                        {

                            Img1.Src = "";
                        }
                        else if (y == 1)
                        {
                            Img2.Src = "";
                        }
                    }
                }
            }
            else
            {
                Img1.Src = "";
                Img2.Src = "";
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    ////Added by ankita on 26/06/2017
    #region For Denomination

    public void BindGrid1()
    {
        try
        {
            CP.Getinfotable(grdCashRct, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "TDCLOSE");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grdOwgData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdCashRct.PageIndex = e.NewPageIndex;
            BindGrid1();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }

    protected void grdCashRct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["CHECK_FLAG"].ToString() == "PRINT")
            {
                string Setno = (grdCashRct.SelectedRow.FindControl("SET_NO") as Label).Text;
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DepCloser_LoanClos_Print_" + TxtProcode + "_" + txtAccNo + "_" + txtPayAmnt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + Session["EntryDate"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&FN=V&rptname=RptReceiptPrint.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void LnkPrintReceipt_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["CHECK_FLAG"] = "PRINT";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkDens_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["CHECK_FLAG"] = "DENS";
            LinkButton objlink = (LinkButton)sender;
            string id = objlink.CommandArgument;
            string[] dens = id.ToString().Split('_');
            Session["densset"] = dens[0].ToString();
            Session["densamt"] = CP.GetTotalAmt(Session["BRCD"].ToString(), Session["densset"].ToString(), Session["EntryDate"].ToString());
            Session["denssubgl"] = dens[2].ToString();
            Session["densact"] = dens[3].ToString();

            string i = CP.checkDenom(Session["BRCD"].ToString(), Session["densset"].ToString(), Session["EntryDate"].ToString());
            if (i == null)
            {
                string redirectURL = "FrmCashDenom.aspx";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else
            {
                lblMessage.Text = "Already Cash Denominations..!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion


    protected void BtnMultipleSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (RdbMultipleClose.Checked == true)
            {
                Multiple_TDAClose(ddlPayType.SelectedValue.ToString());
                BtnPostInt.Enabled = true;
            }
            else
            {

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void grdStmtGrd_DataBound(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            //GridViewRow row2 = new GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Normal);
            TableHeaderCell cell = new TableHeaderCell();
            cell.Text = "";
            cell.ColumnSpan = 3;
            row1.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.Text = "Principle";
            cell.ColumnSpan = 4;
            row1.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 4;
            cell.Text = "Interest";
            row1.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 2;
            cell.Text = "Total";
            row1.Controls.Add(cell);

            row1.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            //row2.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            grdStmtGrd.HeaderRow.Parent.Controls.AddAt(0, row1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindGridAccSts()
    {
        try
        {
            DataTable DT = new DataTable();
            DT = SV.GetStatementView(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), TxtProcode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), dtDeposDate.Text.ToString(), Session["EntryDate"].ToString());
            if (DT.Rows.Count > 0)
            {
                grdStmtGrd.DataSource = DT;
                grdStmtGrd.DataBind();

                divStmtGrd.Visible = true;


                //Calculate Sum and display in Footer Row
                decimal PCreditTotal = DT.AsEnumerable().Sum(row => row.Field<decimal>("PCredit"));
                decimal PDebitTotal = DT.AsEnumerable().Sum(row => row.Field<decimal>("PDebit"));
                decimal ICreditTotal = DT.AsEnumerable().Sum(row => row.Field<decimal>("ICredit"));
                decimal IDebitTotal = DT.AsEnumerable().Sum(row => row.Field<decimal>("IDebit"));
                grdStmtGrd.FooterRow.Cells[0].Text = "Total";
                grdStmtGrd.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                grdStmtGrd.FooterRow.Cells[3].Text = PCreditTotal.ToString("N2");
                grdStmtGrd.FooterRow.Cells[4].Text = PDebitTotal.ToString("N2");
                grdStmtGrd.FooterRow.Cells[7].Text = ICreditTotal.ToString("N2");
                grdStmtGrd.FooterRow.Cells[8].Text = IDebitTotal.ToString("N2");
            }
            else
            {

                grdStmtGrd.DataSource = null;
                grdStmtGrd.DataBind();

                divStmtGrd.Visible = true;

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_AccountStatement_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtProcode.Text == "")
            {
                WebMsgBox.Show("Enter Product code.....!", this.Page);
                txtAccNo.Focus();
            }
            else if (txtAccNo.Text == "")
            {
                WebMsgBox.Show("Enter Account number.....!", this.Page);
                txtAccNo.Focus();

            }
            else if (dtDeposDate.Text == "")
            {
                WebMsgBox.Show("Deposit Date is blank not allowed.....!", this.Page);
                txtAccNo.Focus();
            }
            else
            {
                BindGridAccSts();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }

    public void BindRDGrid(GridView GD, string Brcd, string Procode, string Accno, string EDT)
    {
        try
        {
            dt = CMN.BindRDCal(GD, Brcd, Procode, Accno, EDT);
            if (dt != null)
            {
                GD.DataSource = dt;
                GD.DataBind();

                decimal SchCrtotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("ScheduleCr"));
                decimal Crtotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("Credit"));
                decimal SchPritotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("SchedulePri"));
                decimal SchIntrTotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("IntrOnSchedule"));
                decimal PriTotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("Principle"));
                decimal OnBalTotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("OnBalance"));
                decimal IntrTotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("Intr"));

                ViewState["TotalCredit"] = Crtotal.ToString();// to calculta ecommision for premature

                Grid_RDCal.FooterRow.Cells[1].Text = "Total";
                Grid_RDCal.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[2].Text = SchCrtotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[3].Text = Crtotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[4].Text = SchPritotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[5].Text = SchIntrTotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[6].Text = PriTotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[7].Text = OnBalTotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[8].Text = IntrTotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterStyle.BackColor = System.Drawing.Color.Yellow;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    public void BindRDGrid_1001(GridView GD, string Brcd, string Procode, string Accno, string EDT)
    {
        try
        {
            dt = CMN.BindRDCal_1001(GD, Brcd, Procode, Accno, EDT);
            if (dt != null)
            {
                GD.DataSource = dt;
                GD.DataBind();

                decimal SchCrtotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("ScheduleCr"));
                decimal Crtotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("Credit"));
                decimal SchPritotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("SchedulePri"));
                decimal SchIntrTotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("IntrOnSchedule"));
                decimal PriTotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("Principle"));
                decimal OnBalTotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("OnBalance"));
                decimal IntrTotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("Intr"));

                ViewState["TotalCredit"] = Crtotal.ToString();// to calculta ecommision for premature

                Grid_RDCal.FooterRow.Cells[1].Text = "Total";
                Grid_RDCal.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[2].Text = SchCrtotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[3].Text = Crtotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[4].Text = SchPritotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[5].Text = SchIntrTotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[6].Text = PriTotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[7].Text = OnBalTotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[8].Text = IntrTotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterStyle.BackColor = System.Drawing.Color.Yellow;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindDPGrid(GridView GD, string Brcd, string Procode, string Accno, string EDT)
    {
        try
        {
            dt = CMN.BindDPCal(GD, Brcd, Procode, Accno, EDT);
            if (dt != null)
            {
                GD.DataSource = dt;
                GD.DataBind();

                decimal SchCrtotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("ScheduleCr"));
                decimal Crtotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("Credit"));
                decimal SchPritotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("SchedulePri"));
                decimal SchIntrTotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("IntrOnSchedule"));
                decimal PriTotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("Principle"));
                decimal OnBalTotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("OnBalance"));
                decimal IntrTotal = dt.AsEnumerable().Sum(row => row.Field<decimal>("Intr"));

                ViewState["TotalCredit"] = Crtotal.ToString();// to calculta ecommision for premature

                Grid_RDCal.FooterRow.Cells[1].Text = "Total";
                Grid_RDCal.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[2].Text = SchCrtotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[3].Text = Crtotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[4].Text = SchPritotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[5].Text = SchIntrTotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[6].Text = PriTotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[7].Text = OnBalTotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterRow.Cells[8].Text = IntrTotal.ToString("N2");
                Grid_RDCal.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;

                Grid_RDCal.FooterStyle.BackColor = System.Drawing.Color.Yellow;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void OnConfirm(object sender, EventArgs e)
    {
        string duedate = DtDueDate.Text;
        string cldate = Session["EntryDate"].ToString();
        string daysDiff = conn.GetDayDiff(duedate, cldate);
        string LIMIT = CMN.GetSBLimit(Session["BRCD"].ToString());
        if (Convert.ToInt32(daysDiff) < Convert.ToInt32(LIMIT))
        {
            WebMsgBox.Show("Min Days to Calulate SBINT is " + LIMIT + " Days..", this.Page);
        }
        else
        {
            GetSBINT();
        }
    }

    public void ShowSBInt()
    {
        if (rdbMature.Checked == true)
        {
            BtnCalcSbInt.Visible = true;
        }
        else
        {
            BtnCalcSbInt.Visible = false;
        }
    }
    protected void txtBrcd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtBrcd.Text == Session["BRCD"].ToString())
            {
                WebMsgBox.Show("Not allow for same branch", this.Page);
                txtBrcd.Text = "";
                txtBrcd.Focus();
                return;
            }
            else
            {
                txtBName.Text = CLS.GetBRCD(txtBrcd.Text);
                Txtprocode1.Focus();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }



    private int GetPaymentModeValue()
    {
        int count = 0;
        try
        {
            if (TxtInterestNew.Text == "0" && TxtSbintrest.Text == "0")
            {
                int CheckCn = 0;
                if (Rdb_ExistingSetno.Checked == true && Txt_ExistSetno.Text != "" && DdlCrDr.SelectedValue == "2")
                {
                    CheckCn = CheckPayMode();
                    if (CheckCn == 0)
                    {
                        ddlPayType.Focus();
                        return 0;
                    }

                }

                else if (Rdb_ExistingSetno.Checked == true && Txt_ExistSetno.Text != "" && txtPayAmnt.Text != "" && RdbMultipleClose.Checked == true && DdlCrDr.SelectedValue == "1")
                {
                    CheckCn = CheckPayMode();
                    if (CheckCn == 0)
                    {
                        ddlPayType.Focus();
                        return 0;
                    }

                    if (ddlPayType.SelectedValue.ToString() == "0")
                    {
                        ABB.Visible = false;
                        Transfer.Visible = false;
                        Transfer2.Visible = false;
                        divLoan.Visible = false;
                        hdnValue.Value = "0"; //Added by Amruta as per Ambika madam for ABB-Transfer 06/03/2018
                        ClearPayInfo();
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "1")
                    {
                        if (TxtInterestNew.Text == "")
                        {
                            return 0;
                        }
                        ViewState["PAYTYPE"] = "CASH";
                        ABB.Visible = false;
                        Transfer.Visible = false;
                        Transfer2.Visible = false;
                        divLoan.Visible = false;
                        BtnMultipleSubmit.Focus();
                        hdnValue.Value = "0";
                        ClearPayInfo();
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "2")
                    {
                        if (TxtInterestNew.Text == "")
                        {
                            return 0;
                        }
                        ViewState["PAYTYPE"] = "TRANSFER";
                        ABB.Visible = false;
                        Transfer.Visible = true;
                        Transfer2.Visible = false;
                        divLoan.Visible = false;
                        hdnValue.Value = "0";
                        autoglname1.ContextKey = Session["BRCD"].ToString();
                        ClearPayInfo();
                        Txtprocode1.Focus();
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "3")
                    {
                        if (TxtInterestNew.Text == "")
                        {
                            return 0;
                        }
                        ViewState["PAYTYPE"] = "CHEQUE";
                        ABB.Visible = false;
                        Transfer.Visible = true;
                        Transfer2.Visible = true;
                        divLoan.Visible = false;
                        hdnValue.Value = "0";
                        autoglname1.ContextKey = Session["BRCD"].ToString();
                        ClearPayInfo();
                        Txtprocode1.Focus();
                    }
                    else if (ddlPayType.SelectedValue == "4")
                    {
                        BtnMultipleSubmit.Focus();
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "5" || ddlPayType.SelectedValue.ToString() == "6")
                    {
                        if (TxtInterestNew.Text == "")
                        {
                            return 0;
                        }
                        ViewState["PAYTYPE"] = "TRANSFER";
                        ABB.Visible = false;
                        Transfer.Visible = true;
                        Transfer2.Visible = false;
                        divLoan.Visible = true;
                        hdnValue.Value = "0";
                        autoglname1.ContextKey = Session["BRCD"].ToString();
                        ClearPayInfo();
                        Txtprocode1.Focus();
                    }
                    else if (ddlPayType.SelectedValue == "7")
                    {
                        if (TxtInterestNew.Text == "")
                        {
                            return 0;
                        }
                        ViewState["PAYTYPE"] = "ABB-TRANSFER";
                        ABB.Visible = true;
                        Transfer.Visible = true;
                        Transfer2.Visible = false;
                        divLoan.Visible = false;
                        hdnValue.Value = "1";
                        autoglname1.ContextKey = Session["BRCD"].ToString();
                        ClearPayInfo();

                        txtBrcd.Focus();
                    }
                    else
                    {
                        Transfer.Visible = false;
                        divLoan.Visible = false;
                        ClearPayInfo();
                    }
                    return 0;
                }

                double TotalPaid;
                if (Rdb_IntPay.Checked == true) // For MIS Interest Transfer Only
                {
                    if (Convert.ToInt32(TxtIntrestPaybl.Text) != 0)
                        TotalPaid = Convert.ToDouble(TxtMaturity.Text) - (Convert.ToDouble(TxtPrincPaybl.Text) + Convert.ToDouble(TxtIntrestPaybl.Text));
                    else
                        TotalPaid = 0;

                    BtnPostInt.Visible = false;

                    txtPayAmnt.Text = (Convert.ToInt32(TxtIntrestPaybl.Text)).ToString();
                }

                else // Other Cases of Deposit except MIS Interest Transfer Only
                {
                    if (ViewState["RD"].ToString() != "YES")
                    {
                        string[] arraydt = Session["EntryDate"].ToString().Split('/');

                        if (Convert.ToInt32(TxtInterestNew.Text) != 0)
                            TotalPaid = Convert.ToDouble(TxtMaturity.Text) - (Convert.ToDouble(TxtPrincPaybl.Text) + Convert.ToDouble(TxtIntrestPaybl.Text));
                        else
                            TotalPaid = 0;

                        if (rdbPreMature.Checked != true)
                        {
                            TxtInterestNew.Text = TotalPaid.ToString();
                        }
                        else
                        {
                            TxtInterestNew.Text = "0";
                        }

                        if (TotalPaid > 0)
                        {
                            BtnPostInt.Visible = true;
                        }
                        else
                        {
                            BtnPostInt.Visible = false;
                        }

                        txtPayAmnt.Text = ((Convert.ToDouble(TxtPrincPaybl.Text.Trim().ToString() == "" ? "0" : TxtPrincPaybl.Text.Trim().ToString()) + Convert.ToDouble(TxtIntrestPaybl.Text.Trim().ToString() == "" ? "0" : TxtIntrestPaybl.Text.Trim().ToString())) - (Convert.ToDouble(TxtAdminCharges.Text.Trim().ToString() == "" ? "0" : TxtAdminCharges.Text.Trim().ToString()) + Convert.ToDouble((ViewState["GLCODE"].ToString() == "5" ? "0" : TxtCommission.Text.Trim().ToString() == "" ? "0" : TxtCommission.Text.Trim().ToString())))).ToString();//Convert.ToDouble((ViewState["GLCODE"].ToString() == "5" ? "0" : TxtCommission.Text))
                    }
                    else
                    {
                        dt = new DataTable();
                        dt = CMN.CalRDintrest(Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text, Session["EntryDate"].ToString(), "C", "0");
                        if (dt.Rows[0]["Rate"].ToString() != "" && dt.Rows[0]["Rate"].ToString() != null)
                        {
                            //Added By Amol on 2016-12-12
                            Res = CurrentCls1.GetLastIntDate(Session["BRCD"].ToString(), Txtcustno.Text.Trim().ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                            if (Res != Session["Entrydate"].ToString())
                            {
                                double CH = Convert.ToDouble(dt.Rows[0]["Rate"].ToString());
                                if (CH != 0 && CH != null)
                                {
                                    double PP = CMN.GetPPandINT("CLOSING", TxtProcode.Text, txtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString());
                                    double INTP = CMN.GetPPandINT("CLOSING", ViewState["IR"].ToString(), txtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "10");
                                    if (dt.Rows.Count > 0)
                                    {

                                        TxtPrincPaybl.Text = PP.ToString();
                                        TxtIntrestPaybl.Text = INTP.ToString();
                                        //if (ViewState["GLCODE"].ToString() == "15") //For Balvikas Deposit --Abhishek
                                        //{
                                        TxtInterestNew.Text = Math.Round(Convert.ToDouble(dt.Rows[0]["Interest"]), 0).ToString();// - Convert.ToDouble(dt.Rows[0]["InterestPaid"])), 0).ToString();

                                    }
                                }
                                else
                                {
                                    lblMessage.Text = "";
                                    string P = dt.Rows[0]["Period"].ToString() == null ? "0" : dt.Rows[0]["Period"].ToString();
                                    string PT = dt.Rows[0]["Periodtype"].ToString() == null ? "0" : dt.Rows[0]["Periodtype"].ToString();
                                    lblMessage.Text = "Rate for GLCODE = " + ViewState["GLCODE"].ToString() + " is not set for Period = " + P + " and Period type ='" + PT + "'";
                                    ModalPopup.Show(this.Page);
                                    //  return;
                                }
                            }
                            TotalPaid = Convert.ToDouble(Convert.ToDouble(TxtInterestNew.Text.Trim().ToString() == "" ? "0" : TxtInterestNew.Text.Trim().ToString()) + Convert.ToDouble(TxtIntrestPaybl.Text.Trim().ToString() == "" ? "0" : TxtIntrestPaybl.Text.Trim().ToString()));

                            if (TotalPaid > 0)
                            {
                                BtnPostInt.Visible = true;
                            }
                            else
                            {
                                BtnPostInt.Visible = false;
                            }

                            txtPayAmnt.Text = ((Convert.ToDouble(TxtPrincPaybl.Text.Trim().ToString() == "" ? "0" : TxtPrincPaybl.Text.Trim().ToString()) + Convert.ToDouble(TxtIntrestPaybl.Text.Trim().ToString() == "" ? "0" : TxtIntrestPaybl.Text.Trim().ToString())) - (Convert.ToDouble(TxtAdminCharges.Text.Trim().ToString() == "" ? "0" : TxtAdminCharges.Text.Trim().ToString()) + Convert.ToDouble((ViewState["GLCODE"].ToString() == "5" ? "0" : TxtCommission.Text.Trim().ToString() == "" ? "0" : TxtCommission.Text.Trim().ToString())))).ToString();

                            //Rd Calculation Grid Bind
                            BindRDGrid(Grid_RDCal, Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text, Session["EntryDate"].ToString());
                        }
                        else
                        {
                            WebMsgBox.Show("Deposit details having invalid Period....!", this.Page);
                            return 0;
                        }
                    }

                    //End Added by Amol
                }

                if (RdbMultipleClose.Checked != true)
                {
                    if (ddlPayType.SelectedValue.ToString() == "0")
                    {
                        hdnValue.Value = "0";
                        Transfer.Visible = false;
                        ABB.Visible = false;
                        Transfer2.Visible = false;
                        divLoan.Visible = false;
                        ClearPayInfo();
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "1")
                    {
                        if (TxtInterestNew.Text == "")
                        {
                            return 0;
                        }
                        ViewState["PAYTYPE"] = "CASH";
                        ABB.Visible = false;
                        hdnValue.Value = "0";
                        Transfer.Visible = false;
                        Transfer2.Visible = false;
                        divLoan.Visible = false;
                        ClearPayInfo();
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "2")
                    {
                        if (TxtInterestNew.Text == "")
                        {
                            return 0;
                        }
                        ViewState["PAYTYPE"] = "TRANSFER";
                        ABB.Visible = false;
                        hdnValue.Value = "0";
                        Transfer.Visible = true;
                        Transfer2.Visible = false;
                        divLoan.Visible = false;
                        autoglname1.ContextKey = Session["BRCD"].ToString();
                        ClearPayInfo();
                        Txtprocode1.Focus();
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "4")
                    {
                        if (TxtInterestNew.Text == "")
                        {
                            return 0;
                        }
                        ViewState["PAYTYPE"] = "CHEQUE";
                        ABB.Visible = false;
                        Transfer.Visible = true;
                        hdnValue.Value = "0";
                        Transfer2.Visible = true;
                        divLoan.Visible = false;
                        autoglname1.ContextKey = Session["BRCD"].ToString();
                        ClearPayInfo();
                        Txtprocode1.Focus();
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "5" || ddlPayType.SelectedValue.ToString() == "6")
                    {
                        if (TxtInterestNew.Text == "")
                        {
                            return 0;
                        }
                        ViewState["PAYTYPE"] = "TRANSFER";
                        ABB.Visible = false;
                        hdnValue.Value = "0";
                        Transfer.Visible = true;
                        Transfer2.Visible = false;
                        divLoan.Visible = true;
                        autoglname1.ContextKey = Session["BRCD"].ToString();
                        ClearPayInfo();
                        Txtprocode1.Focus();
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "7")
                    {
                        if (TxtInterestNew.Text == "")
                        {
                            return 0;
                        }
                        ViewState["PAYTYPE"] = "ABB-TRANSFER";
                        ABB.Visible = true;
                        hdnValue.Value = "1";
                        Transfer.Visible = true;
                        Transfer2.Visible = false;
                        divLoan.Visible = false;
                        autoglname1.ContextKey = Session["BRCD"].ToString();
                        ClearPayInfo();
                        txtBrcd.Focus();
                    }
                    else
                    {
                        Transfer.Visible = false;
                        divLoan.Visible = false;
                        ClearPayInfo();
                    }
                    txtPayAmnt.Text = Convert.ToDouble(Convert.ToDouble(txtPayAmnt.Text.Trim().ToString() == "" ? "0" : txtPayAmnt.Text.Trim().ToString()) - Convert.ToDouble(ViewState["LoanAmt"].ToString() == "" ? "0" : ViewState["LoanAmt"].ToString())).ToString();
                }


                //Added By AmolB ON 2017-01-16 Bcoz interest not post twice as per valmik sir instruction
                Res = CurrentCls1.GetLastIntDate(Session["BRCD"].ToString(), Txtcustno.Text.Trim().ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                if (Res == Session["Entrydate"].ToString() || Convert.ToInt32(TxtInterestNew.Text) == 0)
                {
                    BtnPostInt.Visible = false;
                }
                else
                {
                    BtnPostInt.Visible = true;
                }

                autoglname1.ContextKey = Session["BRCD"].ToString();
            }
            else
            {
                WebMsgBox.Show("Interest not posted , First post the interest...!", this.Page);
                BtnPostInt.Focus();
                ddlPayType.SelectedValue = "0";
                count = 1;
            }



        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return count;

    }
  protected void BtnAddNew_1_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmVoucherPrint.aspx", true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }}