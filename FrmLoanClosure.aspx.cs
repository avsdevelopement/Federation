using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmLoanClosure : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsGetLoanStatDetails LTD = new ClsGetLoanStatDetails();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsLoanClosure LI = new ClsLoanClosure();
    ClsLoanSchedule LS = new ClsLoanSchedule();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsInsertTrans ITrans = new ClsInsertTrans();
    ClsCashReciept CurrentCls = new ClsCashReciept();
    DataTable GST = new DataTable(); 
    DataTable DT = new DataTable();
    string sResult = "", AC1 = "";
    string RefNumber, PmtMode, FL = "";
    string DrawPower = "0", PenalType = "N";
    int resultint = 0, Activity, IntCalType = 0, IntApp = 0, IntId = 0;
    double OSOverDue = 0, SurIntRate = 0, TotalAmt = 0;
    decimal TotalCr = 0, TotalDE = 0, TotalIn = 0, TotalPI = 0, TotalIR = 0, TotalNC = 0, TotalSC = 0;
    decimal TotalCC = 0, TotalSC1 = 0, TotalOC = 0, TotalI = 0, TotalBC = 0, TotalBal = 0, ToptalCL = 0;
    double CGST = 0, SGST = 0;

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ////Added by ankita on 23/09/2017
            if (!string.IsNullOrEmpty(Request.QueryString["SUBGLCODE"].ToString()))
            {
                txtProdType.Text = Request.QueryString["SUBGLCODE"].ToString();
                AC1 = LI.Getaccno(txtProdType.Text, Session["BRCD"].ToString());

                if (AC1 != null)
                {
                    string[] AC = AC1.Split('_'); ;
                    ViewState["GLCODE"] = AC[0].ToString();
                    txtProdName.Text = AC[1].ToString();
                    ViewState["LNCAT"] = BD.GetLoanCategory(txtProdType.Text, Session["BRCD"].ToString());
                    AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType.Text + "_" + ViewState["GLCODE"].ToString();

                    ClearCustAllDetails();
                    txtAccNo.Focus();
                }
                txtAccNo.Text = Request.QueryString["ACCNO"].ToString();
                GetLoanData();
            }
            autoglname.ContextKey = Session["BRCD"].ToString();
            BD.BindPayment(ddlPayType, "1");
            ViewState["TBLNAME"] = "";
            txtProdType.Focus();
            BD.BindAccStatus(ddlAccStatus);
            BindLabel();
        }
        BindGrid1();
        ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
    }

    #endregion

    #region Text Changed Event

    protected void txtProdType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;

            btnSubmit.Visible = true;

            //Added By Amol on 22092017 as per ambika mam instruction
            if (BD.GetProdOperate(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString()).ToString() != "3")
            {
                AC1 = LI.Getaccno(txtProdType.Text, Session["BRCD"].ToString());

                if (AC1 != null)
                {
                    string[] AC = AC1.Split('_');
                    ViewState["GLCODE"] = AC[0].ToString();
                    txtProdName.Text = AC[1].ToString();
                    ViewState["LNCAT"] = BD.GetLoanCategory(txtProdType.Text, Session["BRCD"].ToString());
                    AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType.Text + "_" + ViewState["GLCODE"].ToString();

                    ClearCustAllDetails();
                    txtAccNo.Focus();
                    return;
                }
                else
                {
                    WebMsgBox.Show("Enter valid Product code ...!!", this.Page);
                    ClearAll();
                    txtProdType.Focus();
                    return;
                }
            }
            else
            {
                ClearAll();
                WebMsgBox.Show("Product is not operating ...!!", this.Page);
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
            btnSubmit.Visible = true;
            string CUNAME = txtProdName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                //Added By Amol on 22092017 as per ambika mam instruction
                if (BD.GetProdOperate(Session["BRCD"].ToString(), string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString()).ToString() != "3")
                {
                    txtProdName.Text = custnob[0].ToString();
                    txtProdType.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                    string[] AC = LI.Getaccno(txtProdType.Text, Session["BRCD"].ToString()).Split('_');
                    ViewState["GLCODE"] = AC[0].ToString();
                    ViewState["LNCAT"] = BD.GetLoanCategory(txtProdType.Text, Session["BRCD"].ToString());
                    AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType.Text;

                    ClearCustAllDetails();
                    txtAccNo.Focus();
                    return;
                }
                else
                {
                    ClearAll();
                    WebMsgBox.Show("Product is not operating ...!!", this.Page);
                    return;
                }
            }
            else
            {
                ClearAll();
                txtProdType.Focus();
                return;
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
            btnSubmit.Visible = true;
            sResult = BD.Getstage1(txtAccNo.Text, Session["BRCD"].ToString(), txtProdType.Text);
            if (sResult != null)
            {
                if (sResult != "1003")
                {
                    WebMsgBox.Show("Sorry account not authorise ...!!", this.Page);
                    ClearCustAllDetails();
                    txtAccNo.Focus();
                    return;
                }
                else
                {
                    GetLoanData();
                    return;
                }
            }
            else
            {
                WebMsgBox.Show("Enter valid account number ...!!", this.Page);
                ClearCustAllDetails();
                txtAccNo.Focus();
                return;
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
            btnSubmit.Visible = true;
            string[] custnob = txtAccName.Text.ToString().Split('_');
            if (custnob.Length > 1)
            {
                txtAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                sResult = BD.Getstage1(txtAccNo.Text, Session["BRCD"].ToString(), txtProdType.Text);
                if (sResult != null)
                {
                    if (sResult != "1003")
                    {
                        WebMsgBox.Show("Sorry account not authorise ...!!", this.Page);
                        ClearCustAllDetails();
                        txtAccNo.Focus();
                        return;
                    }
                    else
                    {
                        GetLoanData();
                        return;
                    }
                }
                else
                {
                    WebMsgBox.Show("Enter valid account number ...!!", this.Page);
                    ClearCustAllDetails();
                    txtAccNo.Focus();
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPayType.SelectedValue.ToString() == "0")
        {
            Transfer.Visible = false;
            Transfer1.Visible = false;
            DivAmount.Visible = false;
        }
        else if (ddlPayType.SelectedValue.ToString() == "1")
        {
            ViewState["PAYTYPE"] = "CASH";
            Transfer.Visible = false;
            Transfer1.Visible = false;
            DivAmount.Visible = true;
            txtNarration.Text = "By Cash";
            txtAmount.Text = txtTotPaidAmt.Text.Trim().ToString() == "" ? "" : txtTotPaidAmt.Text.Trim().ToString();
            Clear();
            txtNarration.Focus();
        }
        else if (ddlPayType.SelectedValue.ToString() == "2")
        {
            ViewState["PAYTYPE"] = "TRANSFER";
            Transfer.Visible = true;
            Transfer1.Visible = false;
            DivAmount.Visible = true;
            txtNarration.Text = "By TRF";
            autoglname1.ContextKey = Session["BRCD"].ToString();
            txtAmount.Text = txtTotPaidAmt.Text.Trim().ToString() == "" ? "" : txtTotPaidAmt.Text.Trim().ToString();

            Clear();
            txtProdType1.Focus();
        }
        else if (ddlPayType.SelectedValue.ToString() == "4")
        {
            ViewState["PAYTYPE"] = "CHEQUE";
            Transfer.Visible = true;
            Transfer1.Visible = true;
            DivAmount.Visible = true;
            txtNarration.Text = "By TRF";
            autoglname1.ContextKey = Session["BRCD"].ToString();
            txtAmount.Text = txtTotPaidAmt.Text.Trim().ToString() == "" ? "" : txtTotPaidAmt.Text.Trim().ToString();

            Clear();
            txtProdType1.Focus();
        }
        else
        {
            Clear();
            Transfer.Visible = false;
            Transfer1.Visible = false;
        }
    }

    protected void txtProdType1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;

            //Added By Amol on 22092017 as per ambika mam instruction
            if (BD.GetProdOperate(Session["BRCD"].ToString(), txtProdType1.Text.Trim().ToString()).ToString() != "3")
            {
                AC1 = LI.Getaccno(txtProdType1.Text, Session["BRCD"].ToString());

                if (AC1 != null)
                {
                    string[] AC = AC1.Split('_'); ;
                    ViewState["GLCODE1"] = AC[0].ToString();
                    txtProdName1.Text = AC[1].ToString();
                    AutoAccname1.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType1.Text + "_" + ViewState["GLCODE1"].ToString();

                    if (Convert.ToInt32(ViewState["GLCODE1"].ToString() == "" ? "0" : ViewState["GLCODE1"].ToString()) >= 100)
                    {
                        TxtAccNo1.Text = "";
                        TxtAccName1.Text = "";

                        TxtAccNo1.Text = txtProdType1.Text.ToString();
                        TxtAccName1.Text = txtProdName1.Text.ToString();

                        txtBalance.Text = LI.GetOpenClose(Session["BRCD"].ToString(), txtProdType1.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();

                        TxtChequeNo.Focus();
                    }
                    else
                    {
                        TxtAccNo1.Text = "";
                        TxtAccName1.Text = "";
                        txtBalance.Text = "";

                        TxtAccNo1.Focus();
                    }
                }
                else
                {
                    WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                    txtProdType1.Text = "";
                    txtProdName1.Text = "";
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";
                    txtBalance.Text = "";
                    txtProdType1.Focus();
                }
            }
            else
            {
                txtProdType1.Text = "";
                txtProdName1.Text = "";
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
            string CUNAME = txtProdName1.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                //Added By Amol on 22092017 as per ambika mam instruction
                if (BD.GetProdOperate(Session["BRCD"].ToString(), string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString()).ToString() != "3")
                {
                    txtProdName1.Text = custnob[0].ToString();
                    txtProdType1.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                    string[] AC = LI.Getaccno(txtProdType1.Text, Session["BRCD"].ToString()).Split('_');
                    ViewState["GLCODE1"] = AC[0].ToString();
                    AutoAccname1.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType1.Text + "_" + ViewState["GLCODE1"].ToString();

                    if (Convert.ToInt32(ViewState["GLCODE1"].ToString() == "" ? "0" : ViewState["GLCODE1"].ToString()) >= 100)
                    {
                        TxtAccNo1.Text = "";
                        TxtAccName1.Text = "";

                        TxtAccNo1.Text = txtProdType1.Text.ToString();
                        TxtAccName1.Text = txtProdName1.Text.ToString();

                        txtBalance.Text = LI.GetOpenClose(Session["BRCD"].ToString(), txtProdType1.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();

                        TxtChequeNo.Focus();
                    }
                    else
                    {
                        TxtAccNo1.Text = "";
                        TxtAccName1.Text = "";
                        txtBalance.Text = "";

                        TxtAccNo1.Focus();
                    }
                }
                else
                {
                    txtProdType1.Text = "";
                    txtProdName1.Text = "";
                    WebMsgBox.Show("Product is not operating ...!!", this.Page);
                    return;
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
            AT = BD.Getstage1(TxtAccNo1.Text, Session["BRCD"].ToString(), txtProdType1.Text);
            if (AT != null)
            {
                if (AT != "1003")
                {
                    WebMsgBox.Show("Sorry Customer not Authorise ...!!", this.Page);
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";
                    TxtAccNo1.Focus();
                }
                else
                {
                    DataTable DT = new DataTable();
                    DT = LI.GetCustName(txtProdType1.Text, TxtAccNo1.Text, Session["BRCD"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                        TxtAccName1.Text = CustName[0].ToString();

                        txtBalance.Text = LI.GetOpenClose(Session["BRCD"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
                        txtNarration.Focus();
                    }
                }
            }
            else
            {
                WebMsgBox.Show("Enter valid account number ...!!", this.Page);
                TxtAccNo1.Text = "";
                TxtAccName1.Text = "";
                TxtAccNo1.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAccName1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtAccName1.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtAccName1.Text = custnob[0].ToString();
                TxtAccNo1.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                txtBalance.Text = LI.GetOpenClose(Session["BRCD"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
                txtNarration.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void GSTCalculate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GSTCalculation();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtGSTCurrAmt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDouble(txtGSTCurrAmt.Text.ToString() == "" ? "0" : txtGSTCurrAmt.Text.ToString()) > 0)
            {
                CGST = Math.Round(Convert.ToSingle(Convert.ToSingle((Convert.ToDouble(txtGSTCurrAmt.Text.ToString()) * 18) / 100) / 2), 2);
                SGST = Math.Round(Convert.ToSingle(Convert.ToSingle((Convert.ToDouble(txtGSTCurrAmt.Text.ToString()) * 18) / 100) / 2), 2);
                txtGSTTotalAmt.Text = Math.Round(Convert.ToDouble(CGST + SGST), 0).ToString();

                txtTotPrevAmt.Text = Math.Round(Convert.ToDouble(Math.Abs(Convert.ToDouble(txtPrinPrev.Text.Trim().ToString() == "" ? "0" : txtPrinPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtIntPrev.Text.Trim().ToString() == "" ? "0" : txtIntPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtPIntPrev.Text.Trim().ToString() == "" ? "0" : txtPIntPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtIntRecPrev.Text.Trim().ToString() == "" ? "0" : txtIntRecPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtNotChrgPrev.Text.Trim().ToString() == "" ? "0" : txtNotChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtSerChrgPrev.Text.Trim().ToString() == "" ? "0" : txtSerChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtCrtChrgPrev.Text.Trim().ToString() == "" ? "0" : txtCrtChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtSurChrgPrev.Text.Trim().ToString() == "" ? "0" : txtSurChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtOtherChrgPrev.Text.Trim().ToString() == "" ? "0" : txtOtherChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtBankChrgPrev.Text.Trim().ToString() == "" ? "0" : txtBankChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtInsurancePrev.Text.Trim().ToString() == "" ? "0" : txtInsurancePrev.Text.Trim().ToString()))), 2).ToString();
                txtTotCurrAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTCurrAmt.Text.ToString() == "" ? "0" : txtGSTCurrAmt.Text.ToString()) + Math.Abs(Convert.ToDouble(txtPrinCurr.Text.ToString()) < 0 ? 0 : Convert.ToDouble(txtPrinCurr.Text.ToString())) + Convert.ToDouble(txtIntCurr.Text.Trim().ToString() == "" ? "0" : txtIntCurr.Text.Trim().ToString()) + Convert.ToDouble(txtPIntCurr.Text.Trim().ToString() == "" ? "0" : txtPIntCurr.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecCurr.Text.Trim().ToString() == "" ? "0" : txtIntRecCurr.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgCurr.Text.Trim().ToString() == "" ? "0" : txtNotChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSerChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgCurr.Text.Trim().ToString() == "" ? "0" : txtCrtChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSurChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgCurr.Text.Trim().ToString() == "" ? "0" : txtOtherChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgCurr.Text.Trim().ToString() == "" ? "0" : txtBankChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceCurr.Text.Trim().ToString() == "" ? "0" : txtInsuranceCurr.Text.Trim().ToString())), 2).ToString();
                txtTotalAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString()) + Convert.ToDouble(txtPrinTotal.Text.Trim().ToString() == "" ? "0" : txtPrinTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntTotal.Text.Trim().ToString() == "" ? "0" : txtIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtPIntTotal.Text.Trim().ToString() == "" ? "0" : txtPIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString() == "" ? "0" : txtIntRecTotal.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgTotal.Text.Trim().ToString() == "" ? "0" : txtNotChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSerChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgTotal.Text.Trim().ToString() == "" ? "0" : txtCrtChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSurChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgTotal.Text.Trim().ToString() == "" ? "0" : txtOtherChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgTotal.Text.Trim().ToString() == "" ? "0" : txtBankChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceTotal.Text.Trim().ToString() == "" ? "0" : txtInsuranceTotal.Text.Trim().ToString())), 2).ToString();
                txtTotPaidAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTAmt.Text.ToString() == "" ? "0" : txtGSTAmt.Text.ToString()) + Convert.ToDouble(txtPrinAmt.Text.Trim().ToString() == "" ? "0" : txtPrinAmt.Text.Trim().ToString()) + Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString()) + Convert.ToDouble(txtPIntAmt.Text.Trim().ToString() == "" ? "0" : txtPIntAmt.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecAmt.Text.Trim().ToString() == "" ? "0" : txtIntRecAmt.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgAmt.Text.Trim().ToString() == "" ? "0" : txtNotChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSerChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgAmt.Text.Trim().ToString() == "" ? "0" : txtCrtChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSurChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgAmt.Text.Trim().ToString() == "" ? "0" : txtOtherChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgAmt.Text.Trim().ToString() == "" ? "0" : txtBankChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceAmt.Text.Trim().ToString() == "" ? "0" : txtInsuranceAmt.Text.Trim().ToString())), 2).ToString();
            }
            else
            {
                txtGSTCurrAmt.Text = "0"; txtGSTTotalAmt.Text = "0";
                txtTotPrevAmt.Text = Math.Round(Convert.ToDouble(Math.Abs(Convert.ToDouble(txtPrinPrev.Text.Trim().ToString() == "" ? "0" : txtPrinPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtIntPrev.Text.Trim().ToString() == "" ? "0" : txtIntPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtPIntPrev.Text.Trim().ToString() == "" ? "0" : txtPIntPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtIntRecPrev.Text.Trim().ToString() == "" ? "0" : txtIntRecPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtNotChrgPrev.Text.Trim().ToString() == "" ? "0" : txtNotChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtSerChrgPrev.Text.Trim().ToString() == "" ? "0" : txtSerChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtCrtChrgPrev.Text.Trim().ToString() == "" ? "0" : txtCrtChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtSurChrgPrev.Text.Trim().ToString() == "" ? "0" : txtSurChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtOtherChrgPrev.Text.Trim().ToString() == "" ? "0" : txtOtherChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtBankChrgPrev.Text.Trim().ToString() == "" ? "0" : txtBankChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtInsurancePrev.Text.Trim().ToString() == "" ? "0" : txtInsurancePrev.Text.Trim().ToString()))), 2).ToString();
                txtTotCurrAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTCurrAmt.Text.ToString() == "" ? "0" : txtGSTCurrAmt.Text.ToString()) + Math.Abs(Convert.ToDouble(txtPrinCurr.Text.ToString()) < 0 ? 0 : Convert.ToDouble(txtPrinCurr.Text.ToString())) + Convert.ToDouble(txtIntCurr.Text.Trim().ToString() == "" ? "0" : txtIntCurr.Text.Trim().ToString()) + Convert.ToDouble(txtPIntCurr.Text.Trim().ToString() == "" ? "0" : txtPIntCurr.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecCurr.Text.Trim().ToString() == "" ? "0" : txtIntRecCurr.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgCurr.Text.Trim().ToString() == "" ? "0" : txtNotChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSerChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgCurr.Text.Trim().ToString() == "" ? "0" : txtCrtChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSurChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgCurr.Text.Trim().ToString() == "" ? "0" : txtOtherChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgCurr.Text.Trim().ToString() == "" ? "0" : txtBankChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceCurr.Text.Trim().ToString() == "" ? "0" : txtInsuranceCurr.Text.Trim().ToString())), 2).ToString();
                txtTotalAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString()) + Convert.ToDouble(txtPrinTotal.Text.Trim().ToString() == "" ? "0" : txtPrinTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntTotal.Text.Trim().ToString() == "" ? "0" : txtIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtPIntTotal.Text.Trim().ToString() == "" ? "0" : txtPIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString() == "" ? "0" : txtIntRecTotal.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgTotal.Text.Trim().ToString() == "" ? "0" : txtNotChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSerChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgTotal.Text.Trim().ToString() == "" ? "0" : txtCrtChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSurChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgTotal.Text.Trim().ToString() == "" ? "0" : txtOtherChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgTotal.Text.Trim().ToString() == "" ? "0" : txtBankChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceTotal.Text.Trim().ToString() == "" ? "0" : txtInsuranceTotal.Text.Trim().ToString())), 2).ToString();
                txtTotPaidAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTAmt.Text.ToString() == "" ? "0" : txtGSTAmt.Text.ToString()) + Convert.ToDouble(txtPrinAmt.Text.Trim().ToString() == "" ? "0" : txtPrinAmt.Text.Trim().ToString()) + Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString()) + Convert.ToDouble(txtPIntAmt.Text.Trim().ToString() == "" ? "0" : txtPIntAmt.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecAmt.Text.Trim().ToString() == "" ? "0" : txtIntRecAmt.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgAmt.Text.Trim().ToString() == "" ? "0" : txtNotChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSerChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgAmt.Text.Trim().ToString() == "" ? "0" : txtCrtChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSurChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgAmt.Text.Trim().ToString() == "" ? "0" : txtOtherChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgAmt.Text.Trim().ToString() == "" ? "0" : txtBankChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceAmt.Text.Trim().ToString() == "" ? "0" : txtInsuranceAmt.Text.Trim().ToString())), 2).ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtGSTTotalAmt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString()) > 0)
            {
                txtTotPrevAmt.Text = Math.Round(Convert.ToDouble(Math.Abs(Convert.ToDouble(txtPrinPrev.Text.Trim().ToString() == "" ? "0" : txtPrinPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtIntPrev.Text.Trim().ToString() == "" ? "0" : txtIntPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtPIntPrev.Text.Trim().ToString() == "" ? "0" : txtPIntPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtIntRecPrev.Text.Trim().ToString() == "" ? "0" : txtIntRecPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtNotChrgPrev.Text.Trim().ToString() == "" ? "0" : txtNotChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtSerChrgPrev.Text.Trim().ToString() == "" ? "0" : txtSerChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtCrtChrgPrev.Text.Trim().ToString() == "" ? "0" : txtCrtChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtSurChrgPrev.Text.Trim().ToString() == "" ? "0" : txtSurChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtOtherChrgPrev.Text.Trim().ToString() == "" ? "0" : txtOtherChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtBankChrgPrev.Text.Trim().ToString() == "" ? "0" : txtBankChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtInsurancePrev.Text.Trim().ToString() == "" ? "0" : txtInsurancePrev.Text.Trim().ToString()))), 2).ToString();
                txtTotCurrAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTCurrAmt.Text.ToString() == "" ? "0" : txtGSTCurrAmt.Text.ToString()) + Math.Abs(Convert.ToDouble(txtPrinCurr.Text.ToString()) < 0 ? 0 : Convert.ToDouble(txtPrinCurr.Text.ToString())) + Convert.ToDouble(txtIntCurr.Text.Trim().ToString() == "" ? "0" : txtIntCurr.Text.Trim().ToString()) + Convert.ToDouble(txtPIntCurr.Text.Trim().ToString() == "" ? "0" : txtPIntCurr.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecCurr.Text.Trim().ToString() == "" ? "0" : txtIntRecCurr.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgCurr.Text.Trim().ToString() == "" ? "0" : txtNotChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSerChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgCurr.Text.Trim().ToString() == "" ? "0" : txtCrtChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSurChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgCurr.Text.Trim().ToString() == "" ? "0" : txtOtherChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgCurr.Text.Trim().ToString() == "" ? "0" : txtBankChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceCurr.Text.Trim().ToString() == "" ? "0" : txtInsuranceCurr.Text.Trim().ToString())), 2).ToString();
                txtTotalAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString()) + Convert.ToDouble(txtPrinTotal.Text.Trim().ToString() == "" ? "0" : txtPrinTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntTotal.Text.Trim().ToString() == "" ? "0" : txtIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtPIntTotal.Text.Trim().ToString() == "" ? "0" : txtPIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString() == "" ? "0" : txtIntRecTotal.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgTotal.Text.Trim().ToString() == "" ? "0" : txtNotChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSerChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgTotal.Text.Trim().ToString() == "" ? "0" : txtCrtChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSurChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgTotal.Text.Trim().ToString() == "" ? "0" : txtOtherChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgTotal.Text.Trim().ToString() == "" ? "0" : txtBankChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceTotal.Text.Trim().ToString() == "" ? "0" : txtInsuranceTotal.Text.Trim().ToString())), 2).ToString();
                txtTotPaidAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTAmt.Text.ToString() == "" ? "0" : txtGSTAmt.Text.ToString()) + Convert.ToDouble(txtPrinAmt.Text.Trim().ToString() == "" ? "0" : txtPrinAmt.Text.Trim().ToString()) + Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString()) + Convert.ToDouble(txtPIntAmt.Text.Trim().ToString() == "" ? "0" : txtPIntAmt.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecAmt.Text.Trim().ToString() == "" ? "0" : txtIntRecAmt.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgAmt.Text.Trim().ToString() == "" ? "0" : txtNotChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSerChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgAmt.Text.Trim().ToString() == "" ? "0" : txtCrtChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSurChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgAmt.Text.Trim().ToString() == "" ? "0" : txtOtherChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgAmt.Text.Trim().ToString() == "" ? "0" : txtBankChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceAmt.Text.Trim().ToString() == "" ? "0" : txtInsuranceAmt.Text.Trim().ToString())), 2).ToString();
            }
            else
            {
                txtGSTCurrAmt.Text = "0"; txtGSTTotalAmt.Text = "0";
                txtTotPrevAmt.Text = Math.Round(Convert.ToDouble(Math.Abs(Convert.ToDouble(txtPrinPrev.Text.Trim().ToString() == "" ? "0" : txtPrinPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtIntPrev.Text.Trim().ToString() == "" ? "0" : txtIntPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtPIntPrev.Text.Trim().ToString() == "" ? "0" : txtPIntPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtIntRecPrev.Text.Trim().ToString() == "" ? "0" : txtIntRecPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtNotChrgPrev.Text.Trim().ToString() == "" ? "0" : txtNotChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtSerChrgPrev.Text.Trim().ToString() == "" ? "0" : txtSerChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtCrtChrgPrev.Text.Trim().ToString() == "" ? "0" : txtCrtChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtSurChrgPrev.Text.Trim().ToString() == "" ? "0" : txtSurChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtOtherChrgPrev.Text.Trim().ToString() == "" ? "0" : txtOtherChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtBankChrgPrev.Text.Trim().ToString() == "" ? "0" : txtBankChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtInsurancePrev.Text.Trim().ToString() == "" ? "0" : txtInsurancePrev.Text.Trim().ToString()))), 2).ToString();
                txtTotCurrAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTCurrAmt.Text.ToString() == "" ? "0" : txtGSTCurrAmt.Text.ToString()) + Math.Abs(Convert.ToDouble(txtPrinCurr.Text.ToString()) < 0 ? 0 : Convert.ToDouble(txtPrinCurr.Text.ToString())) + Convert.ToDouble(txtIntCurr.Text.Trim().ToString() == "" ? "0" : txtIntCurr.Text.Trim().ToString()) + Convert.ToDouble(txtPIntCurr.Text.Trim().ToString() == "" ? "0" : txtPIntCurr.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecCurr.Text.Trim().ToString() == "" ? "0" : txtIntRecCurr.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgCurr.Text.Trim().ToString() == "" ? "0" : txtNotChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSerChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgCurr.Text.Trim().ToString() == "" ? "0" : txtCrtChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSurChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgCurr.Text.Trim().ToString() == "" ? "0" : txtOtherChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgCurr.Text.Trim().ToString() == "" ? "0" : txtBankChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceCurr.Text.Trim().ToString() == "" ? "0" : txtInsuranceCurr.Text.Trim().ToString())), 2).ToString();
                txtTotalAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString()) + Convert.ToDouble(txtPrinTotal.Text.Trim().ToString() == "" ? "0" : txtPrinTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntTotal.Text.Trim().ToString() == "" ? "0" : txtIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtPIntTotal.Text.Trim().ToString() == "" ? "0" : txtPIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString() == "" ? "0" : txtIntRecTotal.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgTotal.Text.Trim().ToString() == "" ? "0" : txtNotChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSerChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgTotal.Text.Trim().ToString() == "" ? "0" : txtCrtChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSurChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgTotal.Text.Trim().ToString() == "" ? "0" : txtOtherChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgTotal.Text.Trim().ToString() == "" ? "0" : txtBankChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceTotal.Text.Trim().ToString() == "" ? "0" : txtInsuranceTotal.Text.Trim().ToString())), 2).ToString();
                txtTotPaidAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTAmt.Text.ToString() == "" ? "0" : txtGSTAmt.Text.ToString()) + Convert.ToDouble(txtPrinAmt.Text.Trim().ToString() == "" ? "0" : txtPrinAmt.Text.Trim().ToString()) + Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString()) + Convert.ToDouble(txtPIntAmt.Text.Trim().ToString() == "" ? "0" : txtPIntAmt.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecAmt.Text.Trim().ToString() == "" ? "0" : txtIntRecAmt.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgAmt.Text.Trim().ToString() == "" ? "0" : txtNotChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSerChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgAmt.Text.Trim().ToString() == "" ? "0" : txtCrtChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSurChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgAmt.Text.Trim().ToString() == "" ? "0" : txtOtherChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgAmt.Text.Trim().ToString() == "" ? "0" : txtBankChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceAmt.Text.Trim().ToString() == "" ? "0" : txtInsuranceAmt.Text.Trim().ToString())), 2).ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtDepositeAmt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double DepAmount = 0;
            DateTime DDate = Convert.ToDateTime(conn.ConvertDate(txtDueDate.Text.ToString()).ToString()).Date;
            DateTime EDate = Convert.ToDateTime(conn.ConvertDate(Session["EntryDate"].ToString()).ToString()).Date;

            if (Convert.ToDouble(txtDepositeAmt.Text.Trim().ToString()) > 0)
            {
                if (AccStatus.Value.ToString() != "9")
                {
                    ClearTotAmt();
                    DepAmount = Convert.ToDouble(Convert.ToDouble(txtDepositeAmt.Text.Trim().ToString()));

                    //  Calculate GST Amount
                    if (Convert.ToDouble(txtGSTTotalAmt.Text.Trim().ToString() == "" ? "0" : txtGSTTotalAmt.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtGSTTotalAmt.Text.Trim().ToString() == "" ? "0" : txtGSTTotalAmt.Text.Trim().ToString()))
                        {
                            txtGSTAmt.Text = Convert.ToDouble(txtGSTTotalAmt.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtGSTTotalAmt.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtGSTAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtGSTTotPendAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTTotalAmt.Text) - Convert.ToDouble(txtGSTAmt.Text)), 2).ToString();
                    }
                    else
                    {
                        txtGSTTotPendAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTTotalAmt.Text.Trim().ToString() == "" ? "0" : txtGSTTotalAmt.Text.Trim().ToString()) - Convert.ToDouble(txtGSTAmt.Text.Trim().ToString() == "" ? "0" : txtGSTAmt.Text.Trim().ToString())), 2).ToString();
                    }

                    if (Convert.ToDouble(txtInsuranceTotal.Text.Trim().ToString() == "" ? "0" : txtInsuranceTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtInsuranceTotal.Text.Trim().ToString() == "" ? "0" : txtInsuranceTotal.Text.Trim().ToString()))
                        {
                            txtInsuranceAmt.Text = Convert.ToDouble(txtInsuranceTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtInsuranceTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtInsuranceAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtInsurancePen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtInsuranceTotal.Text) - Convert.ToDouble(txtInsuranceAmt.Text)), 2).ToString();
                    }
                    else
                    {
                        txtInsurancePen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtInsuranceTotal.Text.Trim().ToString() == "" ? "0" : txtInsuranceTotal.Text.Trim().ToString()) - Convert.ToDouble(txtInsuranceAmt.Text.Trim().ToString() == "" ? "0" : txtInsuranceAmt.Text.Trim().ToString())), 2).ToString();
                    }

                    if (Convert.ToDouble(txtBankChrgTotal.Text.Trim().ToString() == "" ? "0" : txtBankChrgTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtBankChrgTotal.Text.Trim().ToString() == "" ? "0" : txtBankChrgTotal.Text.Trim().ToString()))
                        {
                            txtBankChrgAmt.Text = Convert.ToDouble(txtBankChrgTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtBankChrgTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtBankChrgAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtBankChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtBankChrgTotal.Text) - Convert.ToDouble(txtBankChrgAmt.Text)), 2).ToString();
                    }
                    else
                    {
                        txtBankChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtBankChrgTotal.Text.Trim().ToString() == "" ? "0" : txtBankChrgTotal.Text.Trim().ToString()) - Convert.ToDouble(txtBankChrgAmt.Text.Trim().ToString() == "" ? "0" : txtBankChrgAmt.Text.Trim().ToString())), 2).ToString();
                    }

                    if (Convert.ToDouble(txtOtherChrgTotal.Text.Trim().ToString() == "" ? "0" : txtOtherChrgTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtOtherChrgTotal.Text.Trim().ToString() == "" ? "0" : txtOtherChrgTotal.Text.Trim().ToString()))
                        {
                            txtOtherChrgAmt.Text = Convert.ToDouble(txtOtherChrgTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtOtherChrgTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtOtherChrgAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtOtherChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtOtherChrgTotal.Text) - Convert.ToDouble(txtOtherChrgAmt.Text)), 2).ToString();
                    }
                    else
                    {
                        txtOtherChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtOtherChrgTotal.Text.Trim().ToString() == "" ? "0" : txtOtherChrgTotal.Text.Trim().ToString()) - Convert.ToDouble(txtOtherChrgAmt.Text.Trim().ToString() == "" ? "0" : txtOtherChrgAmt.Text.Trim().ToString())), 2).ToString();
                    }


                    if (Convert.ToDouble(txtSurChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSurChrgTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtSurChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSurChrgTotal.Text.Trim().ToString()))
                        {
                            txtSurChrgAmt.Text = Convert.ToDouble(txtSurChrgTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtSurChrgTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtSurChrgAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtSurChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtSurChrgTotal.Text) - Convert.ToDouble(txtSurChrgAmt.Text)), 2).ToString();
                    }
                    else
                    {
                        txtSurChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtSurChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSurChrgTotal.Text.Trim().ToString()) - Convert.ToDouble(txtSurChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSurChrgAmt.Text.Trim().ToString())), 2).ToString();
                    }


                    if (Convert.ToDouble(txtCrtChrgTotal.Text.Trim().ToString() == "" ? "0" : txtCrtChrgTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtCrtChrgTotal.Text.Trim().ToString() == "" ? "0" : txtCrtChrgTotal.Text.Trim().ToString()))
                        {
                            txtCrtChrgAmt.Text = Convert.ToDouble(txtCrtChrgTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtCrtChrgTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtCrtChrgAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtCrtChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtCrtChrgTotal.Text) - Convert.ToDouble(txtCrtChrgAmt.Text)), 2).ToString();
                    }
                    else
                    {
                        txtCrtChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtCrtChrgTotal.Text.Trim().ToString() == "" ? "0" : txtCrtChrgTotal.Text.Trim().ToString()) - Convert.ToDouble(txtCrtChrgAmt.Text.Trim().ToString() == "" ? "0" : txtCrtChrgAmt.Text.Trim().ToString())), 2).ToString();
                    }


                    if (Convert.ToDouble(txtSerChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSerChrgTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtSerChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSerChrgTotal.Text.Trim().ToString()))
                        {
                            txtSerChrgAmt.Text = Convert.ToDouble(txtSerChrgTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtSerChrgTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtSerChrgAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtSerChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtSerChrgTotal.Text) - Convert.ToDouble(txtSerChrgAmt.Text)), 2).ToString();
                    }
                    else
                    {
                        txtSerChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtSerChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSerChrgTotal.Text.Trim().ToString()) - Convert.ToDouble(txtSerChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSerChrgAmt.Text.Trim().ToString())), 2).ToString();
                    }


                    if (Convert.ToDouble(txtNotChrgTotal.Text.Trim().ToString() == "" ? "0" : txtNotChrgTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtNotChrgTotal.Text.Trim().ToString() == "" ? "0" : txtNotChrgTotal.Text.Trim().ToString()))
                        {
                            txtNotChrgAmt.Text = Convert.ToDouble(txtNotChrgTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtNotChrgTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtNotChrgAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtNotChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtNotChrgTotal.Text) - Convert.ToDouble(txtNotChrgAmt.Text)), 2).ToString();
                    }
                    else
                    {
                        txtNotChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtNotChrgTotal.Text.Trim().ToString() == "" ? "0" : txtNotChrgTotal.Text.Trim().ToString()) - Convert.ToDouble(txtNotChrgAmt.Text.Trim().ToString() == "" ? "0" : txtNotChrgAmt.Text.Trim().ToString())), 2).ToString();
                    }


                    if (Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString() == "" ? "0" : txtIntRecTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString() == "" ? "0" : txtIntRecTotal.Text.Trim().ToString()))
                        {
                            txtIntRecAmt.Text = Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtIntRecAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtIntRecPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtIntRecTotal.Text) - Convert.ToDouble(txtIntRecAmt.Text)), 2).ToString();
                    }
                    else
                    {
                        txtIntRecPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString() == "" ? "0" : txtIntRecTotal.Text.Trim().ToString()) - Convert.ToDouble(txtIntRecAmt.Text.Trim().ToString() == "" ? "0" : txtIntRecAmt.Text.Trim().ToString())), 2).ToString();
                    }


                    if (Convert.ToDouble(txtPIntTotal.Text.Trim().ToString() == "" ? "0" : txtPIntTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtPIntTotal.Text.Trim().ToString() == "" ? "0" : txtPIntTotal.Text.Trim().ToString()))
                        {
                            txtPIntAmt.Text = Convert.ToDouble(txtPIntTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtPIntTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtPIntAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtPIntPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtPIntTotal.Text) - Convert.ToDouble(txtPIntAmt.Text)), 2).ToString();
                    }
                    else
                    {
                        txtPIntPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtPIntTotal.Text.Trim().ToString() == "" ? "0" : txtPIntTotal.Text.Trim().ToString()) - Convert.ToDouble(txtPIntAmt.Text.Trim().ToString() == "" ? "0" : txtPIntAmt.Text.Trim().ToString())), 2).ToString();
                    }

                    if (Convert.ToDouble(txtIntTotal.Text.Trim().ToString() == "" ? "0" : txtIntTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtIntTotal.Text.Trim().ToString() == "" ? "0" : txtIntTotal.Text.Trim().ToString()))
                        {
                            txtIntAmt.Text = Convert.ToDouble(txtIntTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtIntTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtIntAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtIntPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtIntTotal.Text) - Convert.ToDouble(txtIntAmt.Text)), 2).ToString();
                    }
                    else
                    {
                        txtIntPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtIntTotal.Text.Trim().ToString() == "" ? "0" : txtIntTotal.Text.Trim().ToString()) - Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString())), 2).ToString();
                    }


                    //Calculate Principle Amount
                    if (Convert.ToDouble(txtPrinTotal.Text.Trim().ToString() == "" ? "0" : txtPrinTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtPrinTotal.Text.Trim().ToString() == "" ? "0" : txtPrinTotal.Text.Trim().ToString()))
                        {
                            txtPrinAmt.Text = Convert.ToDouble(txtPrinTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtPrinTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtPrinAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtPrinPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtPrinTotal.Text.Trim().ToString() == "" ? "0" : txtPrinTotal.Text.Trim().ToString()) - Convert.ToDouble(txtPrinAmt.Text.Trim().ToString() == "" ? "0" : txtPrinAmt.Text.Trim().ToString())), 2).ToString();
                    }
                    else
                    {
                        txtPrinPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtPrinTotal.Text.Trim().ToString() == "" ? "0" : txtPrinTotal.Text.Trim().ToString()) - Convert.ToDouble(txtPrinAmt.Text.Trim().ToString() == "" ? "0" : txtPrinAmt.Text.Trim().ToString())), 2).ToString();
                    }
                }
                else
                {
                    ClearTotAmt();
                    DepAmount = Convert.ToDouble(Convert.ToDouble(txtDepositeAmt.Text.Trim().ToString()));

                    //  Calculate interest receivable
                    if (Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString() == "" ? "0" : txtIntRecTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString() == "" ? "0" : txtIntRecTotal.Text.Trim().ToString()))
                        {
                            txtIntRecAmt.Text = Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtIntRecAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtIntRecPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtIntRecTotal.Text) - Convert.ToDouble(txtIntRecAmt.Text)), 2).ToString();
                    }
                    else
                    {
                        txtIntRecPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString() == "" ? "0" : txtIntRecTotal.Text.Trim().ToString()) - Convert.ToDouble(txtIntRecAmt.Text.Trim().ToString() == "" ? "0" : txtIntRecAmt.Text.Trim().ToString())), 2).ToString();
                    }


                    //Calculate Interest
                    if (Convert.ToDouble(txtIntTotal.Text.Trim().ToString() == "" ? "0" : txtIntTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtIntTotal.Text.Trim().ToString() == "" ? "0" : txtIntTotal.Text.Trim().ToString()))
                        {
                            txtIntAmt.Text = Convert.ToDouble(txtIntTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtIntTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtIntAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtIntPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtIntTotal.Text) - Convert.ToDouble(txtIntAmt.Text)), 2).ToString();
                    }
                    else
                    {
                        txtIntPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtIntTotal.Text.Trim().ToString() == "" ? "0" : txtIntTotal.Text.Trim().ToString()) - Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString())), 2).ToString();
                    }


                    //  Calculate Insurence
                    if (Convert.ToDouble(txtInsuranceTotal.Text.Trim().ToString() == "" ? "0" : txtInsuranceTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtInsuranceTotal.Text.Trim().ToString() == "" ? "0" : txtInsuranceTotal.Text.Trim().ToString()))
                        {
                            txtInsuranceAmt.Text = Convert.ToDouble(txtInsuranceTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtInsuranceTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtInsuranceAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtInsurancePen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtInsuranceTotal.Text) - Convert.ToDouble(txtInsuranceAmt.Text)), 2).ToString();
                    }
                    else
                    {
                        txtInsurancePen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtInsuranceTotal.Text.Trim().ToString() == "" ? "0" : txtInsuranceTotal.Text.Trim().ToString()) - Convert.ToDouble(txtInsuranceAmt.Text.Trim().ToString() == "" ? "0" : txtInsuranceAmt.Text.Trim().ToString())), 2).ToString();
                    }


                    //  calculate Bank charge
                    if (Convert.ToDouble(txtBankChrgTotal.Text.Trim().ToString() == "" ? "0" : txtBankChrgTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtBankChrgTotal.Text.Trim().ToString() == "" ? "0" : txtBankChrgTotal.Text.Trim().ToString()))
                        {
                            txtBankChrgAmt.Text = Convert.ToDouble(txtBankChrgTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtBankChrgTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtBankChrgAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtBankChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtBankChrgTotal.Text) - Convert.ToDouble(txtBankChrgAmt.Text)), 2).ToString();
                    }
                    else
                    {
                        txtBankChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtBankChrgTotal.Text.Trim().ToString() == "" ? "0" : txtBankChrgTotal.Text.Trim().ToString()) - Convert.ToDouble(txtBankChrgAmt.Text.Trim().ToString() == "" ? "0" : txtBankChrgAmt.Text.Trim().ToString())), 2).ToString();
                    }


                    //  Calculate other charge
                    if (Convert.ToDouble(txtOtherChrgTotal.Text.Trim().ToString() == "" ? "0" : txtOtherChrgTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtOtherChrgTotal.Text.Trim().ToString() == "" ? "0" : txtOtherChrgTotal.Text.Trim().ToString()))
                        {
                            txtOtherChrgAmt.Text = Convert.ToDouble(txtOtherChrgTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtOtherChrgTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtOtherChrgAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtOtherChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtOtherChrgTotal.Text) - Convert.ToDouble(txtOtherChrgAmt.Text)), 2).ToString();
                    }
                    else
                    {
                        txtOtherChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtOtherChrgTotal.Text.Trim().ToString() == "" ? "0" : txtOtherChrgTotal.Text.Trim().ToString()) - Convert.ToDouble(txtOtherChrgAmt.Text.Trim().ToString() == "" ? "0" : txtOtherChrgAmt.Text.Trim().ToString())), 2).ToString();
                    }


                    //  Calculate Court Charge
                    if (Convert.ToDouble(txtCrtChrgTotal.Text.Trim().ToString() == "" ? "0" : txtCrtChrgTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtCrtChrgTotal.Text.Trim().ToString() == "" ? "0" : txtCrtChrgTotal.Text.Trim().ToString()))
                        {
                            txtCrtChrgAmt.Text = Convert.ToDouble(txtCrtChrgTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtCrtChrgTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtCrtChrgAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtCrtChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtCrtChrgTotal.Text) - Convert.ToDouble(txtCrtChrgAmt.Text)), 2).ToString();
                    }
                    else
                    {
                        txtCrtChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtCrtChrgTotal.Text.Trim().ToString() == "" ? "0" : txtCrtChrgTotal.Text.Trim().ToString()) - Convert.ToDouble(txtCrtChrgAmt.Text.Trim().ToString() == "" ? "0" : txtCrtChrgAmt.Text.Trim().ToString())), 2).ToString();
                    }


                    //  Calculate service charge
                    if (Convert.ToDouble(txtSerChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSerChrgTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtSerChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSerChrgTotal.Text.Trim().ToString()))
                        {
                            txtSerChrgAmt.Text = Convert.ToDouble(txtSerChrgTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtSerChrgTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtSerChrgAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtSerChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtSerChrgTotal.Text) - Convert.ToDouble(txtSerChrgAmt.Text)), 2).ToString();
                    }
                    else
                    {
                        txtSerChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtSerChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSerChrgTotal.Text.Trim().ToString()) - Convert.ToDouble(txtSerChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSerChrgAmt.Text.Trim().ToString())), 2).ToString();
                    }


                    //  Calculate notice charge
                    if (Convert.ToDouble(txtNotChrgTotal.Text.Trim().ToString() == "" ? "0" : txtNotChrgTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtNotChrgTotal.Text.Trim().ToString() == "" ? "0" : txtNotChrgTotal.Text.Trim().ToString()))
                        {
                            txtNotChrgAmt.Text = Convert.ToDouble(txtNotChrgTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtNotChrgTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtNotChrgAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtNotChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtNotChrgTotal.Text) - Convert.ToDouble(txtNotChrgAmt.Text)), 2).ToString();
                    }
                    else
                    {
                        txtNotChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtNotChrgTotal.Text.Trim().ToString() == "" ? "0" : txtNotChrgTotal.Text.Trim().ToString()) - Convert.ToDouble(txtNotChrgAmt.Text.Trim().ToString() == "" ? "0" : txtNotChrgAmt.Text.Trim().ToString())), 2).ToString();
                    }


                    //  calculate penal interest
                    if (Convert.ToDouble(txtPIntTotal.Text.Trim().ToString() == "" ? "0" : txtPIntTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtPIntTotal.Text.Trim().ToString() == "" ? "0" : txtPIntTotal.Text.Trim().ToString()))
                        {
                            txtPIntAmt.Text = Convert.ToDouble(txtPIntTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtPIntTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtPIntAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtPIntPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtPIntTotal.Text) - Convert.ToDouble(txtPIntAmt.Text)), 2).ToString();
                    }
                    else
                    {
                        txtPIntPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtPIntTotal.Text.Trim().ToString() == "" ? "0" : txtPIntTotal.Text.Trim().ToString()) - Convert.ToDouble(txtPIntAmt.Text.Trim().ToString() == "" ? "0" : txtPIntAmt.Text.Trim().ToString())), 2).ToString();
                    }

                    //Calculate Principle Amount
                    if (Convert.ToDouble(txtPrinTotal.Text.Trim().ToString() == "" ? "0" : txtPrinTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtPrinTotal.Text.Trim().ToString() == "" ? "0" : txtPrinTotal.Text.Trim().ToString()))
                        {
                            txtPrinAmt.Text = Convert.ToDouble(txtPrinTotal.Text.Trim().ToString()).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtPrinTotal.Text.Trim().ToString()));
                        }
                        else
                        {
                            txtPrinAmt.Text = DepAmount.ToString();
                            DepAmount = 0;
                        }

                        txtPrinPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtPrinTotal.Text.Trim().ToString() == "" ? "0" : txtPrinTotal.Text.Trim().ToString()) - Convert.ToDouble(txtPrinAmt.Text.Trim().ToString() == "" ? "0" : txtPrinAmt.Text.Trim().ToString())), 2).ToString();
                    }
                    else
                    {
                        txtPrinPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtPrinTotal.Text.Trim().ToString() == "" ? "0" : txtPrinTotal.Text.Trim().ToString()) - Convert.ToDouble(txtPrinAmt.Text.Trim().ToString() == "" ? "0" : txtPrinAmt.Text.Trim().ToString())), 2).ToString();
                    }

                    if (Convert.ToDouble(txtGSTTotalAmt.Text.Trim().ToString() == "" ? "0" : txtGSTTotalAmt.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString()))
                        {
                            txtGSTAmt.Text = Convert.ToDouble(txtGSTTotalAmt.Text).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString()));
                        }
                        else if (Convert.ToDouble(txtPrinAmt.Text.ToString() == "" ? "0" : txtPrinAmt.Text.ToString()) >= Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString()))
                        {
                            txtPrinAmt.Text = Convert.ToDouble(Convert.ToDouble(txtPrinAmt.Text.ToString() == "" ? "0" : txtPrinAmt.Text.ToString()) - Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString())).ToString();
                            txtGSTAmt.Text = Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString()).ToString();
                        }
                        else if (Convert.ToDouble(txtIntAmt.Text.ToString() == "" ? "0" : txtIntAmt.Text.ToString()) >= Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString()))
                        {
                            txtIntAmt.Text = Convert.ToDouble(Convert.ToDouble(txtIntAmt.Text.ToString() == "" ? "0" : txtIntAmt.Text.ToString()) - Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString())).ToString();
                            txtGSTAmt.Text = Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString()).ToString();
                        }
                        else if (Convert.ToDouble(txtIntRecAmt.Text.ToString() == "" ? "0" : txtIntRecAmt.Text.ToString()) >= Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString()))
                        {
                            txtIntRecAmt.Text = Convert.ToDouble(Convert.ToDouble(txtIntRecAmt.Text.ToString() == "" ? "0" : txtIntRecAmt.Text.ToString()) - Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString())).ToString();
                            txtGSTAmt.Text = Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString()).ToString();
                        }

                        txtGSTTotPendAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTTotalAmt.Text.Trim().ToString() == "" ? "0" : txtGSTTotalAmt.Text.Trim().ToString()) - Convert.ToDouble(txtGSTAmt.Text.Trim().ToString() == "" ? "0" : txtGSTAmt.Text.Trim().ToString())), 2).ToString();
                    }
                    else
                    {
                        txtGSTTotPendAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTTotalAmt.Text.Trim().ToString() == "" ? "0" : txtGSTTotalAmt.Text.Trim().ToString()) - Convert.ToDouble(txtGSTAmt.Text.Trim().ToString() == "" ? "0" : txtGSTAmt.Text.Trim().ToString())), 2).ToString();
                    }

                    //  Sur Charge paid calculation
                    if (Convert.ToDouble(txtSurChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSurChrgTotal.Text.Trim().ToString()) > 0)
                    {
                        if (DepAmount >= Convert.ToDouble(txtSurChrgTotal.Text.ToString() == "" ? "0" : txtSurChrgTotal.Text.ToString()))
                        {
                            txtSurChrgAmt.Text = Convert.ToDouble(txtSurChrgTotal.Text).ToString();
                            DepAmount = Convert.ToDouble(DepAmount - Convert.ToDouble(txtSurChrgTotal.Text.ToString() == "" ? "0" : txtSurChrgTotal.Text.ToString()));
                        }
                        else if (Convert.ToDouble(txtPrinAmt.Text.ToString() == "" ? "0" : txtPrinAmt.Text.ToString()) >= Convert.ToDouble(txtSurChrgTotal.Text.ToString() == "" ? "0" : txtSurChrgTotal.Text.ToString()))
                        {
                            txtPrinAmt.Text = Convert.ToDouble(Convert.ToDouble(txtPrinAmt.Text.ToString() == "" ? "0" : txtPrinAmt.Text.ToString()) - Convert.ToDouble(txtSurChrgTotal.Text.ToString() == "" ? "0" : txtSurChrgTotal.Text.ToString())).ToString();
                            txtSurChrgAmt.Text = Convert.ToDouble(txtSurChrgTotal.Text.ToString() == "" ? "0" : txtSurChrgTotal.Text.ToString()).ToString();
                        }
                        else if (Convert.ToDouble(txtIntAmt.Text.ToString() == "" ? "0" : txtIntAmt.Text.ToString()) >= Convert.ToDouble(txtSurChrgTotal.Text.ToString() == "" ? "0" : txtSurChrgTotal.Text.ToString()))
                        {
                            txtIntAmt.Text = Convert.ToDouble(Convert.ToDouble(txtIntAmt.Text.ToString() == "" ? "0" : txtIntAmt.Text.ToString()) - Convert.ToDouble(txtSurChrgTotal.Text.ToString() == "" ? "0" : txtSurChrgTotal.Text.ToString())).ToString();
                            txtSurChrgAmt.Text = Convert.ToDouble(txtSurChrgTotal.Text.ToString() == "" ? "0" : txtSurChrgTotal.Text.ToString()).ToString();
                        }
                        else if (Convert.ToDouble(txtIntRecAmt.Text.ToString() == "" ? "0" : txtIntRecAmt.Text.ToString()) >= Convert.ToDouble(txtSurChrgTotal.Text.ToString() == "" ? "0" : txtSurChrgTotal.Text.ToString()))
                        {
                            txtIntRecAmt.Text = Convert.ToDouble(Convert.ToDouble(txtIntRecAmt.Text.ToString() == "" ? "0" : txtIntRecAmt.Text.ToString()) - Convert.ToDouble(txtSurChrgTotal.Text.ToString() == "" ? "0" : txtSurChrgTotal.Text.ToString())).ToString();
                            txtSurChrgAmt.Text = Convert.ToDouble(txtSurChrgTotal.Text.ToString() == "" ? "0" : txtSurChrgTotal.Text.ToString()).ToString();
                        }

                        txtSurChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtSurChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSurChrgTotal.Text.Trim().ToString()) - Convert.ToDouble(txtSurChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSurChrgAmt.Text.Trim().ToString())), 2).ToString();
                    }
                    else
                    {
                        txtSurChrgPen.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtSurChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSurChrgTotal.Text.Trim().ToString()) - Convert.ToDouble(txtSurChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSurChrgAmt.Text.Trim().ToString())), 2).ToString();
                    }
                }

                txtTotCurrAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTCurrAmt.Text.ToString() == "" ? "0" : txtGSTCurrAmt.Text.ToString()) + Math.Abs(Convert.ToDouble(txtPrinCurr.Text.ToString()) < 0 ? 0 : Convert.ToDouble(txtPrinCurr.Text.ToString())) + Convert.ToDouble(txtIntCurr.Text.Trim().ToString() == "" ? "0" : txtIntCurr.Text.Trim().ToString()) + Convert.ToDouble(txtPIntCurr.Text.Trim().ToString() == "" ? "0" : txtPIntCurr.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecCurr.Text.Trim().ToString() == "" ? "0" : txtIntRecCurr.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgCurr.Text.Trim().ToString() == "" ? "0" : txtNotChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSerChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgCurr.Text.Trim().ToString() == "" ? "0" : txtCrtChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSurChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgCurr.Text.Trim().ToString() == "" ? "0" : txtOtherChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgCurr.Text.Trim().ToString() == "" ? "0" : txtBankChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceCurr.Text.Trim().ToString() == "" ? "0" : txtInsuranceCurr.Text.Trim().ToString())), 2).ToString();
                txtTotalAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString()) + Convert.ToDouble(txtPrinTotal.Text.Trim().ToString() == "" ? "0" : txtPrinTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntTotal.Text.Trim().ToString() == "" ? "0" : txtIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtPIntTotal.Text.Trim().ToString() == "" ? "0" : txtPIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString() == "" ? "0" : txtIntRecTotal.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgTotal.Text.Trim().ToString() == "" ? "0" : txtNotChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSerChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgTotal.Text.Trim().ToString() == "" ? "0" : txtCrtChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSurChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgTotal.Text.Trim().ToString() == "" ? "0" : txtOtherChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgTotal.Text.Trim().ToString() == "" ? "0" : txtBankChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceTotal.Text.Trim().ToString() == "" ? "0" : txtInsuranceTotal.Text.Trim().ToString())), 2).ToString();

                //  Bottom Total Calculation
                txtTotPrevAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(txtIntPrev.Text.Trim().ToString()) < 0 ? "0" : txtIntPrev.Text.Trim().ToString()) + Convert.ToDouble(Convert.ToDouble(txtPIntPrev.Text.Trim().ToString()) < 0 ? "0" : txtPIntPrev.Text.Trim().ToString()) + Convert.ToDouble(Convert.ToDouble(txtIntRecPrev.Text.Trim().ToString()) < 0 ? "0" : txtIntRecPrev.Text.Trim().ToString()) + Convert.ToDouble(Convert.ToDouble(txtNotChrgPrev.Text.Trim().ToString()) < 0 ? "0" : txtNotChrgPrev.Text.Trim().ToString()) + Convert.ToDouble(Convert.ToDouble(txtSerChrgPrev.Text.Trim().ToString()) < 0 ? "0" : txtSerChrgPrev.Text.Trim().ToString()) + Convert.ToDouble(Convert.ToDouble(txtCrtChrgPrev.Text.Trim().ToString()) < 0 ? "0" : txtCrtChrgPrev.Text.Trim().ToString()) + Convert.ToDouble(Convert.ToDouble(txtSurChrgPrev.Text.Trim().ToString()) < 0 ? "0" : txtSurChrgPrev.Text.Trim().ToString()) + Convert.ToDouble(Convert.ToDouble(txtOtherChrgPrev.Text.Trim().ToString()) < 0 ? "0" : txtOtherChrgPrev.Text.Trim().ToString()) + Convert.ToDouble(Convert.ToDouble(txtBankChrgPrev.Text.Trim().ToString()) < 0 ? "0" : txtBankChrgPrev.Text.Trim().ToString()) + Convert.ToDouble(Convert.ToDouble(txtInsurancePrev.Text.Trim().ToString()) < 0 ? "0" : txtInsurancePrev.Text.Trim().ToString())), 2).ToString();
                txtTotCurrAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTCurrAmt.Text.ToString() == "" ? "0" : txtGSTCurrAmt.Text.ToString()) + Convert.ToDouble(Convert.ToDouble(txtPrinCurr.Text.Trim().ToString()) < 0 ? "0" : txtPrinCurr.Text.Trim().ToString()) + Convert.ToDouble(txtIntCurr.Text.Trim().ToString() == "" ? "0" : txtIntCurr.Text.Trim().ToString()) + Convert.ToDouble(txtPIntCurr.Text.Trim().ToString() == "" ? "0" : txtPIntCurr.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecCurr.Text.Trim().ToString() == "" ? "0" : txtIntRecCurr.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgCurr.Text.Trim().ToString() == "" ? "0" : txtNotChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSerChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgCurr.Text.Trim().ToString() == "" ? "0" : txtCrtChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSurChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgCurr.Text.Trim().ToString() == "" ? "0" : txtOtherChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgCurr.Text.Trim().ToString() == "" ? "0" : txtBankChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceCurr.Text.Trim().ToString() == "" ? "0" : txtInsuranceCurr.Text.Trim().ToString())), 2).ToString();
                txtTotalAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString()) + Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString()) + Convert.ToDouble(txtPrinTotal.Text.Trim().ToString() == "" ? "0" : txtPrinTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntTotal.Text.Trim().ToString() == "" ? "0" : txtIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtPIntTotal.Text.Trim().ToString() == "" ? "0" : txtPIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString() == "" ? "0" : txtIntRecTotal.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgTotal.Text.Trim().ToString() == "" ? "0" : txtNotChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSerChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgTotal.Text.Trim().ToString() == "" ? "0" : txtCrtChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSurChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgTotal.Text.Trim().ToString() == "" ? "0" : txtOtherChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgTotal.Text.Trim().ToString() == "" ? "0" : txtBankChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceTotal.Text.Trim().ToString() == "" ? "0" : txtInsuranceTotal.Text.Trim().ToString())), 2).ToString();
                txtTotPaidAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTAmt.Text.ToString() == "" ? "0" : txtGSTAmt.Text.ToString()) + Convert.ToDouble(txtPrinAmt.Text.Trim().ToString() == "" ? "0" : txtPrinAmt.Text.Trim().ToString()) + Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString()) + Convert.ToDouble(txtPIntAmt.Text.Trim().ToString() == "" ? "0" : txtPIntAmt.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecAmt.Text.Trim().ToString() == "" ? "0" : txtIntRecAmt.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgAmt.Text.Trim().ToString() == "" ? "0" : txtNotChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSerChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgAmt.Text.Trim().ToString() == "" ? "0" : txtCrtChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSurChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgAmt.Text.Trim().ToString() == "" ? "0" : txtOtherChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgAmt.Text.Trim().ToString() == "" ? "0" : txtBankChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceAmt.Text.Trim().ToString() == "" ? "0" : txtInsuranceAmt.Text.Trim().ToString())), 2).ToString();
                txtTotPendAmt.Text = Math.Round(Convert.ToDouble(Math.Abs(Convert.ToDouble(txtPrinPen.Text.Trim().ToString() == "" ? "0" : txtPrinPen.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtIntPen.Text.Trim().ToString() == "" ? "0" : txtIntPen.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtPIntPen.Text.Trim().ToString() == "" ? "0" : txtPIntPen.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtIntRecPen.Text.Trim().ToString() == "" ? "0" : txtIntRecPen.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtNotChrgPen.Text.Trim().ToString() == "" ? "0" : txtNotChrgPen.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtSerChrgPen.Text.Trim().ToString() == "" ? "0" : txtSerChrgPen.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtCrtChrgPen.Text.Trim().ToString() == "" ? "0" : txtCrtChrgPen.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtSurChrgPen.Text.Trim().ToString() == "" ? "0" : txtSurChrgPen.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtOtherChrgPen.Text.Trim().ToString() == "" ? "0" : txtOtherChrgPen.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtBankChrgPen.Text.Trim().ToString() == "" ? "0" : txtBankChrgPen.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtInsurancePen.Text.Trim().ToString() == "" ? "0" : txtInsurancePen.Text.Trim().ToString()))), 2).ToString();

                ddlPayType.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter Amount Greather Than Zero ...!!", this.Page);
                return;
            }
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
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }

    protected void grdCashRct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["CHECK_FLAG"].ToString() == "PRINT")
            {
                string Setno = (grdCashRct.SelectedRow.FindControl("SET_NO") as Label).Text;
                string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + Session["EntryDate"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&FN=R&rptname=RptReceiptPrint.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Functions

    protected void GetLoanData()
    {
        double PrincipleAmt = 0;

        try
        {
            DT = LI.GetAllFieldData(txtProdType.Text, txtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString());

            if (DT.Rows.Count > 0)
            {
                txtAccNo.Text = DT.Rows[0]["CustAccNo"].ToString();
                txtAccName.Text = DT.Rows[0]["CustName"].ToString();
                txtCustNo.Text = DT.Rows[0]["CustNo"].ToString();
                txtDepositeAmt.Text = "";

                string AccStat = LI.GetAccStatus(Session["BRCD"].ToString(), txtProdType.Text.ToString(), txtAccNo.Text.ToString(), Session["EntryDate"].ToString());
                AccType.Value = string.IsNullOrEmpty(DT.Rows[0]["SubAccStatus"].ToString()) ? "" : DT.Rows[0]["SubAccStatus"].ToString();
                AccStatus.Value = string.IsNullOrEmpty(DT.Rows[0]["Acc_Status"].ToString()) ? "" : DT.Rows[0]["Acc_Status"].ToString();

                if (AccStatus.Value.ToString() != "9")
                    txtAccType.Text = LI.GetAccType(AccType.Value.ToString());
                else
                    txtAccType.Text = LI.GetAccType(AccType.Value.ToString()) + " / " + LI.GetAccType(AccStat.ToString());

                ddlAccStatus.SelectedIndex = Convert.ToInt32(AccStatus.Value.ToString());

                txtAccOpenDate.Text = DT.Rows[0]["SanssionDate"].ToString();
                txtSancLimit.Text = DT.Rows[0]["Limit"].ToString();
                txtPeriod.Text = DT.Rows[0]["Period"].ToString();
                txtAppliedRate.Text = DT.Rows[0]["AppliedRate"].ToString();
                PenalType = string.IsNullOrEmpty(DT.Rows[0]["PenalType"].ToString()) ? "" : DT.Rows[0]["PenalType"].ToString();

                string[] DueDate = DT.Rows[0]["DueDate"].ToString().Split('-');
                txtDueDate.Text = DueDate[2].ToString() + '/' + DueDate[1].ToString() + '/' + DueDate[0].ToString();
                txtInstAmt.Text = DT.Rows[0]["Installment"].ToString();
                txtDays.Text = DT.Rows[0]["Days"].ToString();

                txtIntRate.Text = DT.Rows[0]["IntRate"].ToString();
                txtLastIntDate.Text = DT.Rows[0]["LastIntDate"].ToString();

                DateTime DDate = Convert.ToDateTime(DT.Rows[0]["DueDate"].ToString()).Date;
                DateTime EDate = Convert.ToDateTime(conn.ConvertDate(Session["EntryDate"].ToString()).ToString()).Date;

                if (Convert.ToInt32(AccStatus.Value.ToString()) != 3)
                {
                    string Penal = DT.Rows[0]["PenalIntRate"].ToString();

                    DT = new DataTable();
                    DT = LI.GetAllBalData(Session["BRCD"].ToString(), txtProdType.Text, txtAccNo.Text, Session["EntryDate"].ToString());

                    if (DT.Rows.Count > 0)
                    {
                        IntApp = LI.GetIntApp(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString());
                        Session["IntApp"] = IntApp.ToString();

                        //  Pl Account Number 
                        ViewState["PlAccNo1"] = DT.Rows[0]["PlAccNo1"].ToString();
                        ViewState["PlAccNo2"] = DT.Rows[0]["PlAccNo2"].ToString();

                        PrincipleAmt = Math.Round(Convert.ToDouble(DT.Rows[0]["Principle"].ToString() == "" ? "0" : DT.Rows[0]["Principle"].ToString()));

                        //  Claculate Principle
                        txtPrinPrev.Text = "0";
                        txtPrinCurr.Text = PrincipleAmt.ToString();
                        txtPrinTotal.Text = Math.Abs(PrincipleAmt < 0 ? 0 : PrincipleAmt).ToString();
                        txtPrinAmt.Text = "";
                        txtPrinPen.Text = "";

                        ViewState["PrincipleGl"] = DT.Rows[0]["PrincipleGl"].ToString();
                        ViewState["PrincipleSub"] = DT.Rows[0]["PrincipleSub"].ToString();


                        //Claculate Interest
                        txtIntPrev.Text = DT.Rows[0]["Interest"].ToString();
                        if ((IntApp == 1) || (IntApp == 2) || (IntApp == 3))
                        {
                            txtIntCurr.Text = Convert.ToDouble(LI.GetLoanInterest(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), txtLastIntDate.Text.ToString(), Session["Entrydate"].ToString())).ToString();
                        }
                        else if (IntApp == 4)
                        {
                            txtIntCurr.Text = Math.Round(Math.Round(Convert.ToDouble(((Math.Abs(PrincipleAmt < 0 ? 0 : PrincipleAmt) * Convert.ToDouble(txtAppliedRate.Text.Trim().ToString() == "" ? "0" : txtIntRate.Text.Trim().ToString())) / 100) / 12)) * Convert.ToDouble(Math.Round(Convert.ToDouble(txtDays.Text.Trim().ToString()) / 30))).ToString("#.##");
                        }

                        txtIntTotal.Text = Convert.ToDouble(Math.Abs(Convert.ToDouble(txtIntPrev.Text.Trim().ToString()) < 0 ? 0 : Convert.ToDouble(txtIntPrev.Text.Trim().ToString())) + Convert.ToDouble(txtIntCurr.Text.Trim().ToString() == "" ? "0" : txtIntCurr.Text.Trim().ToString())).ToString();
                        txtIntAmt.Text = "";
                        txtIntPen.Text = "";

                        ViewState["InterestGl"] = DT.Rows[0]["InterestGl"].ToString();
                        ViewState["InterestSub"] = DT.Rows[0]["InterestSub"].ToString();


                        //Calculate OverDue here
                        if (LI.ChkSheduleExists(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString()) == false)
                            LS.ProcessLoanSchedule(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["MID"].ToString());

                        DrawPower = LI.GetDrawPower(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString());
                        if (DrawPower != null && DrawPower != "")
                        {
                            txtTotalDP.Text = Math.Round(Convert.ToDouble(DrawPower.ToString())).ToString();

                            if (Convert.ToDouble(txtTotalDP.Text.Trim().ToString()) < 0)
                            {
                                txtTotOverdue.Text = "0";
                            }
                            else if (DDate < EDate)
                            {
                                txtTotOverdue.Text = Math.Abs(PrincipleAmt < 0 ? 0 : PrincipleAmt).ToString();
                            }
                            else if (Convert.ToDouble(Math.Abs(PrincipleAmt < 0 ? 0 : PrincipleAmt) - Convert.ToDouble(txtTotalDP.Text.Trim().ToString() == "" ? "0" : txtTotalDP.Text.Trim().ToString())) > 0)
                            {
                                txtTotOverdue.Text = Convert.ToDouble(Math.Abs(PrincipleAmt < 0 ? 0 : PrincipleAmt) - Convert.ToDouble(txtTotalDP.Text.Trim().ToString() == "" ? "0" : txtTotalDP.Text.Trim().ToString())).ToString();
                            }
                            else
                            {
                                txtTotOverdue.Text = "0";
                            }
                        }
                        else
                        {
                            txtTotalDP.Text = "0";
                            txtTotOverdue.Text = "0";
                        }

                        //Claculate Interest Receivable
                        txtIntRecPrev.Text = DT.Rows[0]["InterestRec"].ToString() == "" ? "0" : DT.Rows[0]["InterestRec"].ToString();
                        txtIntRecCurr.Text = "0";
                        txtIntRecTotal.Text = Convert.ToDouble(Math.Abs(Convert.ToDouble(txtIntRecPrev.Text.Trim().ToString()) < 0 ? 0 : Convert.ToDouble(txtIntRecPrev.Text.Trim().ToString())) + Convert.ToDouble(txtIntRecCurr.Text.Trim().ToString() == "" ? "0" : txtIntRecCurr.Text.Trim().ToString())).ToString();
                        txtIntRecAmt.Text = "";
                        txtIntRecPen.Text = "";

                        ViewState["InterestRecGl"] = DT.Rows[0]["InterestRecGl"].ToString();
                        ViewState["InterestRecSub"] = DT.Rows[0]["InterestRecSub"].ToString();

                        //Claculate Penal Interest
                        txtPIntPrev.Text = DT.Rows[0]["PInterest"].ToString() == "" ? "0" : DT.Rows[0]["PInterest"].ToString();
                        txtPIntCurr.Text = "";
                        if (PenalType.ToString() == "Y")
                        {
                            if ((Convert.ToDouble(txtTotOverdue.Text.Trim().ToString()) < 0 ? 0 : Convert.ToDouble(txtTotOverdue.Text.Trim().ToString())) > 0)
                            {
                                double PrinOD = Convert.ToDouble(txtTotOverdue.Text.Trim().ToString()) < 0 ? 0 : Convert.ToDouble(txtTotOverdue.Text.Trim().ToString());
                                double IntTotal = Convert.ToDouble(txtIntTotal.Text.Trim().ToString()) < 0 ? 0 : Convert.ToDouble(txtIntTotal.Text.Trim().ToString());
                                double IntRecPrev = Convert.ToDouble(txtIntRecPrev.Text.Trim().ToString()) < 0 ? 0 : Convert.ToDouble(txtIntRecPrev.Text.Trim().ToString());
                                double TotalBalance = PrinOD + IntTotal + IntRecPrev;
                                txtPIntCurr.Text = Math.Round(Convert.ToDouble(((TotalBalance * Convert.ToDouble(Penal == "" ? "0" : Penal)) * Convert.ToInt32(txtDays.Text.Trim().ToString())) / 36500)).ToString("#.##");
                            }
                            else
                            {
                                txtPIntCurr.Text = "0";
                            }
                        }
                        else if (PenalType.ToString() == "N")
                        {
                            if (ViewState["LNCAT"].ToString() == "LNSTF")
                            {
                                txtPIntCurr.Text = "0";
                            }
                            //  Special condition apply for chikotra only
                            else if ((Session["BankCode"].ToString() == "1015") && (ViewState["LNCAT"].ToString() == "LAG") && (DDate >= EDate))
                            {
                                txtPIntCurr.Text = "0";
                            }
                            else
                            {
                                if (Convert.ToInt32(txtTotOverdue.Text.Trim().ToString() == "" ? "0" : txtTotOverdue.Text.Trim().ToString()) > 0)
                                {
                                    txtPIntCurr.Text = Math.Round(Convert.ToDouble(((Convert.ToDouble(txtTotOverdue.Text.Trim().ToString()) * Convert.ToDouble(Penal == "" ? "0" : Penal)) * Convert.ToInt32(txtDays.Text.Trim().ToString())) / 36500)).ToString("#.##");

                                    if (txtPIntCurr.Text.Trim().ToString() == "")
                                        txtPIntCurr.Text = "0";
                                }
                                else
                                {
                                    txtPIntCurr.Text = "0";
                                }
                            }
                        }

                        txtPIntTotal.Text = Convert.ToDouble(Math.Abs(Convert.ToDouble(txtPIntPrev.Text.Trim().ToString()) < 0 ? 0 : Convert.ToDouble(txtPIntPrev.Text.Trim().ToString())) + Convert.ToDouble(txtPIntCurr.Text.Trim().ToString() == "" ? "0" : txtPIntCurr.Text.Trim().ToString())).ToString();
                        txtPIntAmt.Text = "";
                        txtPIntPen.Text = "";

                        ViewState["PInterestGl"] = DT.Rows[0]["PInterestGl"].ToString();
                        ViewState["PInterestSub"] = DT.Rows[0]["PInterestSub"].ToString();


                        //Claculate Notice Charges
                        txtNotChrgPrev.Text = DT.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : DT.Rows[0]["NoticeChrg"].ToString();
                        txtNotChrgCurr.Text = "0";
                        txtNotChrgTotal.Text = Convert.ToDouble(Math.Abs(Convert.ToDouble(txtNotChrgPrev.Text.Trim().ToString()) < 0 ? 0 : Convert.ToDouble(txtNotChrgPrev.Text.Trim().ToString())) + Convert.ToDouble(txtNotChrgCurr.Text.Trim().ToString() == "" ? "0" : txtNotChrgCurr.Text.Trim().ToString())).ToString();
                        txtNotChrgAmt.Text = "";
                        txtNotChrgPen.Text = "";

                        ViewState["NoticeChrgGl"] = DT.Rows[0]["NoticeChrgGl"].ToString();
                        ViewState["NoticeChrgSub"] = DT.Rows[0]["NoticeChrgSub"].ToString();

                        //Claculate Service Charges
                        txtSerChrgPrev.Text = DT.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : DT.Rows[0]["ServiceChrg"].ToString();
                        txtSerChrgCurr.Text = "0";
                        txtSerChrgTotal.Text = Convert.ToDouble(Math.Abs(Convert.ToDouble(txtSerChrgPrev.Text.Trim().ToString()) < 0 ? 0 : Convert.ToDouble(txtSerChrgPrev.Text.Trim().ToString())) + Convert.ToDouble(txtSerChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSerChrgCurr.Text.Trim().ToString())).ToString();
                        txtSerChrgAmt.Text = "";
                        txtSerChrgPen.Text = "";

                        ViewState["ServiceChrgGl"] = DT.Rows[0]["ServiceChrgGl"].ToString();
                        ViewState["ServiceChrgSub"] = DT.Rows[0]["ServiceChrgSub"].ToString();

                        //Claculate Court Charges
                        txtCrtChrgPrev.Text = DT.Rows[0]["CourtChrg"].ToString() == "" ? "0" : DT.Rows[0]["CourtChrg"].ToString();
                        txtCrtChrgCurr.Text = "0";
                        txtCrtChrgTotal.Text = Convert.ToDouble(Math.Abs(Convert.ToDouble(txtCrtChrgPrev.Text.Trim().ToString()) < 0 ? 0 : Convert.ToDouble(txtCrtChrgPrev.Text.Trim().ToString())) + Convert.ToDouble(txtCrtChrgCurr.Text.Trim().ToString() == "" ? "0" : txtCrtChrgCurr.Text.Trim().ToString())).ToString();
                        txtCrtChrgAmt.Text = "";
                        txtCrtChrgPen.Text = "";

                        ViewState["CourtChrgGl"] = DT.Rows[0]["CourtChrgGl"].ToString();
                        ViewState["CourtChrgSub"] = DT.Rows[0]["CourtChrgSub"].ToString();

                        //Claculate Other Charges
                        txtOtherChrgPrev.Text = DT.Rows[0]["OtherChrg"].ToString() == "" ? "0" : DT.Rows[0]["OtherChrg"].ToString();
                        txtOtherChrgCurr.Text = Math.Round(Convert.ToDouble(((Math.Abs(PrincipleAmt < 0 ? 0 : PrincipleAmt) * Convert.ToDouble(LI.GetOtherIntRate(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString()))) * (Convert.ToInt32(txtDays.Text.Trim().ToString()))) / 36500)).ToString("#.##");
                        txtOtherChrgTotal.Text = Convert.ToDouble(Math.Abs(Convert.ToDouble(txtOtherChrgPrev.Text.Trim().ToString()) < 0 ? 0 : Convert.ToDouble(txtOtherChrgPrev.Text.Trim().ToString())) + Convert.ToDouble(txtOtherChrgCurr.Text.Trim().ToString() == "" ? "0" : txtOtherChrgCurr.Text.Trim().ToString())).ToString();
                        txtOtherChrgAmt.Text = "";
                        txtOtherChrgPen.Text = "";

                        ViewState["OtherChrgGl"] = DT.Rows[0]["OtherChrgGl"].ToString();
                        ViewState["OtherChrgSub"] = DT.Rows[0]["OtherChrgSub"].ToString();

                        //Claculate Bank Charges
                        txtBankChrgPrev.Text = DT.Rows[0]["BankChrg"].ToString() == "" ? "0" : DT.Rows[0]["BankChrg"].ToString();
                        txtBankChrgCurr.Text = "0";
                        txtBankChrgTotal.Text = Convert.ToDouble(Math.Abs(Convert.ToDouble(txtBankChrgPrev.Text.Trim().ToString()) < 0 ? 0 : Convert.ToDouble(txtBankChrgPrev.Text.Trim().ToString())) + Convert.ToDouble(txtBankChrgCurr.Text.Trim().ToString() == "" ? "0" : txtBankChrgCurr.Text.Trim().ToString())).ToString();
                        txtBankChrgAmt.Text = "";
                        txtBankChrgPen.Text = "";

                        ViewState["BankChrgGl"] = DT.Rows[0]["BankChrgGl"].ToString();
                        ViewState["BankChrgSub"] = DT.Rows[0]["BankChrgSub"].ToString();

                        //Claculate Insurance Charges
                        txtInsurancePrev.Text = DT.Rows[0]["InsChrg"].ToString() == "" ? "0" : DT.Rows[0]["InsChrg"].ToString();
                        txtInsuranceCurr.Text = "0";
                        txtInsuranceTotal.Text = Convert.ToDouble(Math.Abs(Convert.ToDouble(txtInsurancePrev.Text.Trim().ToString()) < 0 ? 0 : Convert.ToDouble(txtInsurancePrev.Text.Trim().ToString())) + Convert.ToDouble(txtInsuranceCurr.Text.Trim().ToString() == "" ? "0" : txtInsuranceCurr.Text.Trim().ToString())).ToString();
                        txtInsuranceAmt.Text = "";
                        txtInsurancePen.Text = "";

                        ViewState["InsChrgGl"] = DT.Rows[0]["InsChrgGl"].ToString();
                        ViewState["InsChrgSub"] = DT.Rows[0]["InsChrgSub"].ToString();


                        //Claculate Sur Charges
                        txtSurChrgPrev.Text = DT.Rows[0]["SurChrg"].ToString() == "" ? "0" : DT.Rows[0]["SurChrg"].ToString();
                        txtSurChrgCurr.Text = "0";
                        if (Convert.ToDouble(AccStatus.Value.ToString()) == 9)
                        {
                            TotalAmt = Math.Round(Convert.ToDouble(Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString() == "" ? "0" : txtIntRecTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntTotal.Text.Trim().ToString() == "" ? "0" : txtIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtPrinTotal.Text.Trim().ToString() == "" ? "0" : txtPrinTotal.Text.Trim().ToString())));
                            SurIntRate = LI.GetSurIntRate(AccType.Value.ToString());
                            txtSurChrgCurr.Text = Math.Abs(Math.Round(Convert.ToDouble(Convert.ToDouble(TotalAmt * SurIntRate) / 100))).ToString();
                        }
                        txtSurChrgTotal.Text = Math.Round(Convert.ToDouble(txtSurChrgPrev.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSurChrgCurr.Text.Trim().ToString())).ToString();
                        txtSurChrgAmt.Text = "";
                        txtSurChrgPen.Text = "";

                        ViewState["SurChrgGl"] = DT.Rows[0]["SurChrgGl"].ToString();
                        ViewState["SurChrgSub"] = DT.Rows[0]["SurChrgSub"].ToString();

                        //  Outstanding Overdue
                        if (DDate < EDate)
                        {
                            txtOSOverdue.Text = Convert.ToDouble(Convert.ToDouble(txtPrinTotal.Text.Trim().ToString() == "" ? "0" : txtPrinTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntTotal.Text.Trim().ToString() == "" ? "0" : txtIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtPIntTotal.Text.Trim().ToString() == "" ? "0" : txtPIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString() == "" ? "0" : txtIntRecTotal.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgTotal.Text.Trim().ToString() == "" ? "0" : txtNotChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSerChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgTotal.Text.Trim().ToString() == "" ? "0" : txtCrtChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSurChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgTotal.Text.Trim().ToString() == "" ? "0" : txtOtherChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgTotal.Text.Trim().ToString() == "" ? "0" : txtBankChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceTotal.Text.Trim().ToString() == "" ? "0" : txtInsuranceTotal.Text.Trim().ToString())).ToString();
                        }
                        else
                        {
                            OSOverDue = Convert.ToDouble(Convert.ToDouble(txtPrinTotal.Text.Trim().ToString() == "" ? "0" : txtPrinTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntTotal.Text.Trim().ToString() == "" ? "0" : txtIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtPIntTotal.Text.Trim().ToString() == "" ? "0" : txtPIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString() == "" ? "0" : txtIntRecTotal.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgTotal.Text.Trim().ToString() == "" ? "0" : txtNotChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSerChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgTotal.Text.Trim().ToString() == "" ? "0" : txtCrtChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSurChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgTotal.Text.Trim().ToString() == "" ? "0" : txtOtherChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgTotal.Text.Trim().ToString() == "" ? "0" : txtBankChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceTotal.Text.Trim().ToString() == "" ? "0" : txtInsuranceTotal.Text.Trim().ToString()));
                            txtOSOverdue.Text = Convert.ToDouble(OSOverDue - Convert.ToDouble(txtTotalDP.Text.Trim().ToString() == "" ? "0" : txtTotalDP.Text.Trim().ToString())).ToString();
                            txtOSOverdue.Text = (Convert.ToDouble(txtOSOverdue.Text.ToString()) < 0 ? 0 : Convert.ToDouble(txtOSOverdue.Text.ToString())).ToString();
                        }

                        //  Bottom Total Calculation
                        txtTotPrevAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(txtIntPrev.Text.Trim().ToString()) < 0 ? "0" : txtIntPrev.Text.Trim().ToString()) + Convert.ToDouble(Convert.ToDouble(txtPIntPrev.Text.Trim().ToString()) < 0 ? "0" : txtPIntPrev.Text.Trim().ToString()) + Convert.ToDouble(Convert.ToDouble(txtIntRecPrev.Text.Trim().ToString()) < 0 ? "0" : txtIntRecPrev.Text.Trim().ToString()) + Convert.ToDouble(Convert.ToDouble(txtNotChrgPrev.Text.Trim().ToString()) < 0 ? "0" : txtNotChrgPrev.Text.Trim().ToString()) + Convert.ToDouble(Convert.ToDouble(txtSerChrgPrev.Text.Trim().ToString()) < 0 ? "0" : txtSerChrgPrev.Text.Trim().ToString()) + Convert.ToDouble(Convert.ToDouble(txtCrtChrgPrev.Text.Trim().ToString()) < 0 ? "0" : txtCrtChrgPrev.Text.Trim().ToString()) + Convert.ToDouble(Convert.ToDouble(txtSurChrgPrev.Text.Trim().ToString()) < 0 ? "0" : txtSurChrgPrev.Text.Trim().ToString()) + Convert.ToDouble(Convert.ToDouble(txtOtherChrgPrev.Text.Trim().ToString()) < 0 ? "0" : txtOtherChrgPrev.Text.Trim().ToString()) + Convert.ToDouble(Convert.ToDouble(txtBankChrgPrev.Text.Trim().ToString()) < 0 ? "0" : txtBankChrgPrev.Text.Trim().ToString()) + Convert.ToDouble(Convert.ToDouble(txtInsurancePrev.Text.Trim().ToString()) < 0 ? "0" : txtInsurancePrev.Text.Trim().ToString())), 2).ToString();
                        txtTotCurrAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTCurrAmt.Text.ToString() == "" ? "0" : txtGSTCurrAmt.Text.ToString()) + Convert.ToDouble(Convert.ToDouble(txtPrinCurr.Text.Trim().ToString()) < 0 ? "0" : txtPrinCurr.Text.Trim().ToString()) + Convert.ToDouble(txtIntCurr.Text.Trim().ToString() == "" ? "0" : txtIntCurr.Text.Trim().ToString()) + Convert.ToDouble(txtPIntCurr.Text.Trim().ToString() == "" ? "0" : txtPIntCurr.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecCurr.Text.Trim().ToString() == "" ? "0" : txtIntRecCurr.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgCurr.Text.Trim().ToString() == "" ? "0" : txtNotChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSerChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgCurr.Text.Trim().ToString() == "" ? "0" : txtCrtChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSurChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgCurr.Text.Trim().ToString() == "" ? "0" : txtOtherChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgCurr.Text.Trim().ToString() == "" ? "0" : txtBankChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceCurr.Text.Trim().ToString() == "" ? "0" : txtInsuranceCurr.Text.Trim().ToString())), 2).ToString();
                        txtTotalAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString()) + Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString()) + Convert.ToDouble(txtPrinTotal.Text.Trim().ToString() == "" ? "0" : txtPrinTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntTotal.Text.Trim().ToString() == "" ? "0" : txtIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtPIntTotal.Text.Trim().ToString() == "" ? "0" : txtPIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString() == "" ? "0" : txtIntRecTotal.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgTotal.Text.Trim().ToString() == "" ? "0" : txtNotChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSerChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgTotal.Text.Trim().ToString() == "" ? "0" : txtCrtChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSurChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgTotal.Text.Trim().ToString() == "" ? "0" : txtOtherChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgTotal.Text.Trim().ToString() == "" ? "0" : txtBankChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceTotal.Text.Trim().ToString() == "" ? "0" : txtInsuranceTotal.Text.Trim().ToString())), 2).ToString();

                        txtDepositeAmt.Focus();
                    }
                }
                else
                {
                    ClearAllDetails();
                    WebMsgBox.Show("Account is already closed ...!!", this.Page);
                    return;
                }
            }
            else
            {
                ClearCustAllDetails();
                WebMsgBox.Show("Details Not Found For This Account Number ...!!", this.Page);
                txtAccNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void GSTCalculation()
    {
        try
        {
            //  GST Calculation
            GST = LI.GetGSTFlag();
            if (GST.Rows.Count > 0)
            {
                txtGSTCurrAmt.Text = "0";
                for (int i = 0; i < GST.Rows.Count; i++)
                {
                    if ((GST.Rows[i]["SrNumber"].ToString() == "2") && (GST.Rows[i]["GSTFlag"].ToString() == "Y"))
                        txtGSTCurrAmt.Text = Convert.ToDouble(Convert.ToDouble(txtGSTCurrAmt.Text.ToString()) + Convert.ToDouble(txtIntCurr.Text.Trim().ToString() == "" ? "0" : txtIntCurr.Text.Trim().ToString())).ToString();
                    if ((GST.Rows[i]["SrNumber"].ToString() == "3") && (GST.Rows[i]["GSTFlag"].ToString() == "Y"))
                        txtGSTCurrAmt.Text = Convert.ToDouble(Convert.ToDouble(txtGSTCurrAmt.Text.ToString()) + Convert.ToDouble(txtPIntCurr.Text.Trim().ToString() == "" ? "0" : txtPIntCurr.Text.Trim().ToString())).ToString();
                    if ((GST.Rows[i]["SrNumber"].ToString() == "4") && (GST.Rows[i]["GSTFlag"].ToString() == "Y"))
                        txtGSTCurrAmt.Text = Convert.ToDouble(Convert.ToDouble(txtGSTCurrAmt.Text.ToString()) + Convert.ToDouble(txtIntRecCurr.Text.Trim().ToString() == "" ? "0" : txtIntRecCurr.Text.Trim().ToString())).ToString();
                    if ((GST.Rows[i]["SrNumber"].ToString() == "5") && (GST.Rows[i]["GSTFlag"].ToString() == "Y"))
                        txtGSTCurrAmt.Text = Convert.ToDouble(Convert.ToDouble(txtGSTCurrAmt.Text.ToString()) + Convert.ToDouble(txtNotChrgCurr.Text.Trim().ToString() == "" ? "0" : txtNotChrgCurr.Text.Trim().ToString())).ToString();
                    if ((GST.Rows[i]["SrNumber"].ToString() == "6") && (GST.Rows[i]["GSTFlag"].ToString() == "Y"))
                        txtGSTCurrAmt.Text = Convert.ToDouble(Convert.ToDouble(txtGSTCurrAmt.Text.ToString()) + Convert.ToDouble(txtSerChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSerChrgCurr.Text.Trim().ToString())).ToString();
                    if ((GST.Rows[i]["SrNumber"].ToString() == "7") && (GST.Rows[i]["GSTFlag"].ToString() == "Y"))
                        txtGSTCurrAmt.Text = Convert.ToDouble(Convert.ToDouble(txtGSTCurrAmt.Text.ToString()) + Convert.ToDouble(txtCrtChrgCurr.Text.Trim().ToString() == "" ? "0" : txtCrtChrgCurr.Text.Trim().ToString())).ToString();
                    if ((GST.Rows[i]["SrNumber"].ToString() == "8") && (GST.Rows[i]["GSTFlag"].ToString() == "Y"))
                        txtGSTCurrAmt.Text = Convert.ToDouble(Convert.ToDouble(txtGSTCurrAmt.Text.ToString()) + Convert.ToDouble(txtSurChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSurChrgCurr.Text.Trim().ToString())).ToString();
                    if ((GST.Rows[i]["SrNumber"].ToString() == "9") && (GST.Rows[i]["GSTFlag"].ToString() == "Y"))
                        txtGSTCurrAmt.Text = Convert.ToDouble(Convert.ToDouble(txtGSTCurrAmt.Text.ToString()) + Convert.ToDouble(txtOtherChrgCurr.Text.Trim().ToString() == "" ? "0" : txtOtherChrgCurr.Text.Trim().ToString())).ToString();
                    if ((GST.Rows[i]["SrNumber"].ToString() == "10") && (GST.Rows[i]["GSTFlag"].ToString() == "Y"))
                        txtGSTCurrAmt.Text = Convert.ToDouble(Convert.ToDouble(txtGSTCurrAmt.Text.ToString()) + Convert.ToDouble(txtBankChrgCurr.Text.Trim().ToString() == "" ? "0" : txtBankChrgCurr.Text.Trim().ToString())).ToString();
                    if ((GST.Rows[i]["SrNumber"].ToString() == "11") && (GST.Rows[i]["GSTFlag"].ToString() == "Y"))
                        txtGSTCurrAmt.Text = Convert.ToDouble(Convert.ToDouble(txtGSTCurrAmt.Text.ToString()) + Convert.ToDouble(txtInsuranceCurr.Text.Trim().ToString() == "" ? "0" : txtInsuranceCurr.Text.Trim().ToString())).ToString();
                }
                CGST = Math.Round(Convert.ToSingle(Convert.ToSingle((Convert.ToDouble(txtGSTCurrAmt.Text.ToString()) * 18) / 100) / 2), 2);
                SGST = Math.Round(Convert.ToSingle(Convert.ToSingle((Convert.ToDouble(txtGSTCurrAmt.Text.ToString()) * 18) / 100) / 2), 2);
                txtGSTTotalAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(CGST.ToString()) + Convert.ToDouble(SGST.ToString()))).ToString();

                //  Total show
                txtTotPrevAmt.Text = Math.Round(Convert.ToDouble(Math.Abs(Convert.ToDouble(txtPrinPrev.Text.Trim().ToString() == "" ? "0" : txtPrinPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtIntPrev.Text.Trim().ToString() == "" ? "0" : txtIntPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtPIntPrev.Text.Trim().ToString() == "" ? "0" : txtPIntPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtIntRecPrev.Text.Trim().ToString() == "" ? "0" : txtIntRecPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtNotChrgPrev.Text.Trim().ToString() == "" ? "0" : txtNotChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtSerChrgPrev.Text.Trim().ToString() == "" ? "0" : txtSerChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtCrtChrgPrev.Text.Trim().ToString() == "" ? "0" : txtCrtChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtSurChrgPrev.Text.Trim().ToString() == "" ? "0" : txtSurChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtOtherChrgPrev.Text.Trim().ToString() == "" ? "0" : txtOtherChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtBankChrgPrev.Text.Trim().ToString() == "" ? "0" : txtBankChrgPrev.Text.Trim().ToString())) + Math.Abs(Convert.ToDouble(txtInsurancePrev.Text.Trim().ToString() == "" ? "0" : txtInsurancePrev.Text.Trim().ToString()))), 2).ToString();
                txtTotCurrAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTCurrAmt.Text.ToString() == "" ? "0" : txtGSTCurrAmt.Text.ToString()) + Math.Abs(Convert.ToDouble(txtPrinCurr.Text.ToString()) < 0 ? 0 : Convert.ToDouble(txtPrinCurr.Text.ToString())) + Convert.ToDouble(txtIntCurr.Text.Trim().ToString() == "" ? "0" : txtIntCurr.Text.Trim().ToString()) + Convert.ToDouble(txtPIntCurr.Text.Trim().ToString() == "" ? "0" : txtPIntCurr.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecCurr.Text.Trim().ToString() == "" ? "0" : txtIntRecCurr.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgCurr.Text.Trim().ToString() == "" ? "0" : txtNotChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSerChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgCurr.Text.Trim().ToString() == "" ? "0" : txtCrtChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSurChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgCurr.Text.Trim().ToString() == "" ? "0" : txtOtherChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgCurr.Text.Trim().ToString() == "" ? "0" : txtBankChrgCurr.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceCurr.Text.Trim().ToString() == "" ? "0" : txtInsuranceCurr.Text.Trim().ToString())), 2).ToString();
                txtTotalAmt.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(txtGSTTotalAmt.Text.ToString() == "" ? "0" : txtGSTTotalAmt.Text.ToString()) + Convert.ToDouble(txtPrinTotal.Text.Trim().ToString() == "" ? "0" : txtPrinTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntTotal.Text.Trim().ToString() == "" ? "0" : txtIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtPIntTotal.Text.Trim().ToString() == "" ? "0" : txtPIntTotal.Text.Trim().ToString()) + Convert.ToDouble(txtIntRecTotal.Text.Trim().ToString() == "" ? "0" : txtIntRecTotal.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgTotal.Text.Trim().ToString() == "" ? "0" : txtNotChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSerChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgTotal.Text.Trim().ToString() == "" ? "0" : txtCrtChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtSurChrgTotal.Text.Trim().ToString() == "" ? "0" : txtSurChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgTotal.Text.Trim().ToString() == "" ? "0" : txtOtherChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtBankChrgTotal.Text.Trim().ToString() == "" ? "0" : txtBankChrgTotal.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceTotal.Text.Trim().ToString() == "" ? "0" : txtInsuranceTotal.Text.Trim().ToString())), 2).ToString();
            }
            else
            {
                txtGSTCurrAmt.Text = "0";
                txtGSTTotalAmt.Text = "0";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindLabel()
    {
        try
        {
            LblName1.InnerText = LI.GetLabelName("1");
            LblName2.InnerText = LI.GetLabelName("2");
            LblName3.InnerText = LI.GetLabelName("3");
            LblName4.InnerText = LI.GetLabelName("4");
            LblName5.InnerText = LI.GetLabelName("5");
            LblName6.InnerText = LI.GetLabelName("6");
            LblName7.InnerText = LI.GetLabelName("7");
            LblName8.InnerText = LI.GetLabelName("8");
            LblName9.InnerText = LI.GetLabelName("9");
            LblName10.InnerText = LI.GetLabelName("10");
            LblName11.InnerText = LI.GetLabelName("11");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public DataSet GetLoanStatDetails(string FDate, string TDate, string PT, string AC, string FBC)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = LTD.GetLnStatData(FDate, TDate, PT, AC, FBC);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public bool CheckInterest()
    {
        bool Interest = false;
        try
        {
            if (Convert.ToDouble(txtIntAmt.Text.ToString() == "" ? "0" : txtIntAmt.Text.ToString()) > Convert.ToDouble(txtIntTotal.Text.ToString() == "" ? "0" : txtIntTotal.Text.ToString()))
                Interest = false;
            else
                Interest = true;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Interest;
    }

    public bool CheckPenal()
    {
        bool Penal = false;
        try
        {
            if (Convert.ToDouble(txtPIntAmt.Text.ToString() == "" ? "0" : txtPIntAmt.Text.ToString()) > Convert.ToDouble(txtPIntTotal.Text.ToString() == "" ? "0" : txtPIntTotal.Text.ToString()))
                Penal = false;
            else
                Penal = true;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Penal;
    }

    protected void Clear()
    {
        try
        {
            txtProdType1.Text = "";
            txtProdName1.Text = "";
            TxtAccNo1.Text = "";
            TxtAccName1.Text = "";
            txtBalance.Text = "";
            TxtChequeNo.Text = "";
            TxtChequeDate.Text = "";
            TxtChequeDate.Text = Session["EntryDate"].ToString();
            txtProdType1.Focus();
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

    protected void ClearCustAllDetails()
    {
        try
        {
            Session["IntApp"] = null;
            txtAccNo.Text = "";
            txtAccName.Text = "";
            txtCustNo.Text = "";
            txtDepositeAmt.Text = "";

            AccStatus.Value = "";
            txtAccType.Text = "";
            txtAccOpenDate.Text = "";
            txtSancLimit.Text = "";
            txtDueDate.Text = "";
            txtInstAmt.Text = "";
            txtIntRate.Text = "";
            txtTotalDP.Text = "";
            txtTotOverdue.Text = "";

            txtDepositeAmt.Text = "";
            txtPeriod.Text = "";
            txtAppliedRate.Text = "";
            txtDays.Text = "";
            txtLastIntDate.Text = "";

            ddlAccStatus.SelectedIndex = 0;
            ddlPayType.SelectedIndex = 0;
            txtProdType1.Text = "";
            txtProdName1.Text = "";
            TxtAccNo1.Text = "";
            TxtAccName1.Text = "";
            txtBalance.Text = "";
            TxtChequeNo.Text = "";
            TxtChequeDate.Text = "";
            txtNarration.Text = "";

            txtPrinPrev.Text = "";
            txtPrinCurr.Text = "";
            txtPrinTotal.Text = "";
            txtPrinAmt.Text = "";
            txtPrinPen.Text = "";

            txtIntPrev.Text = "";
            txtIntCurr.Text = "";
            txtIntTotal.Text = "";
            txtIntAmt.Text = "";
            txtIntPen.Text = "";

            txtPIntPrev.Text = "";
            txtPIntCurr.Text = "";
            txtPIntTotal.Text = "";
            txtPIntAmt.Text = "";
            txtPIntPen.Text = "";

            txtIntRecPrev.Text = "";
            txtIntRecCurr.Text = "";
            txtIntRecTotal.Text = "";
            txtIntRecAmt.Text = "";
            txtIntRecPen.Text = "";

            txtNotChrgPrev.Text = "";
            txtNotChrgCurr.Text = "";
            txtNotChrgTotal.Text = "";
            txtNotChrgAmt.Text = "";
            txtNotChrgPen.Text = "";

            txtSerChrgPrev.Text = "";
            txtSerChrgCurr.Text = "";
            txtSerChrgTotal.Text = "";
            txtSerChrgAmt.Text = "";
            txtSerChrgPen.Text = "";

            txtCrtChrgPrev.Text = "";
            txtCrtChrgCurr.Text = "";
            txtCrtChrgTotal.Text = "";
            txtCrtChrgAmt.Text = "";
            txtCrtChrgPen.Text = "";

            txtSurChrgPrev.Text = "";
            txtSurChrgCurr.Text = "";
            txtSurChrgTotal.Text = "";
            txtSurChrgAmt.Text = "";
            txtSurChrgPen.Text = "";

            txtOtherChrgPrev.Text = "";
            txtOtherChrgCurr.Text = "";
            txtOtherChrgTotal.Text = "";
            txtOtherChrgAmt.Text = "";
            txtOtherChrgPen.Text = "";

            txtBankChrgPrev.Text = "";
            txtBankChrgCurr.Text = "";
            txtBankChrgTotal.Text = "";
            txtBankChrgAmt.Text = "";
            txtBankChrgPen.Text = "";

            txtInsurancePrev.Text = "";
            txtInsuranceCurr.Text = "";
            txtInsuranceTotal.Text = "";
            txtInsuranceAmt.Text = "";
            txtInsurancePen.Text = "";

            txtGSTPrevAmt.Text = "";
            txtGSTCurrAmt.Text = "";
            txtGSTTotalAmt.Text = "";
            txtGSTAmt.Text = "";
            txtGSTTotPendAmt.Text = "";

            txtTotPrevAmt.Text = "";
            txtTotCurrAmt.Text = "";
            txtTotalAmt.Text = "";
            txtTotPaidAmt.Text = "";
            txtTotPendAmt.Text = "";
            txtOSOverdue.Text = "";

            txtAmount.Text = "";

            txtProdType.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        finally
        {
            txtProdType.Focus();
        }
    }

    protected void ClearAllDetails()
    {
        try
        {
            txtPrinPrev.Text = "";
            txtPrinCurr.Text = "";
            txtPrinTotal.Text = "";
            txtPrinAmt.Text = "";
            txtPrinPen.Text = "";

            txtIntPrev.Text = "";
            txtIntCurr.Text = "";
            txtIntTotal.Text = "";
            txtIntAmt.Text = "";
            txtIntPen.Text = "";

            txtPIntPrev.Text = "";
            txtPIntCurr.Text = "";
            txtPIntTotal.Text = "";
            txtPIntAmt.Text = "";
            txtPIntPen.Text = "";

            txtIntRecPrev.Text = "";
            txtIntRecCurr.Text = "";
            txtIntRecTotal.Text = "";
            txtIntRecAmt.Text = "";
            txtIntRecPen.Text = "";

            txtNotChrgPrev.Text = "";
            txtNotChrgCurr.Text = "";
            txtNotChrgTotal.Text = "";
            txtNotChrgAmt.Text = "";
            txtNotChrgPen.Text = "";

            txtSerChrgPrev.Text = "";
            txtSerChrgCurr.Text = "";
            txtSerChrgTotal.Text = "";
            txtSerChrgAmt.Text = "";
            txtSerChrgPen.Text = "";

            txtCrtChrgPrev.Text = "";
            txtCrtChrgCurr.Text = "";
            txtCrtChrgTotal.Text = "";
            txtCrtChrgAmt.Text = "";
            txtCrtChrgPen.Text = "";

            txtSurChrgPrev.Text = "";
            txtSurChrgCurr.Text = "";
            txtSurChrgTotal.Text = "";
            txtSurChrgAmt.Text = "";
            txtSurChrgPen.Text = "";

            txtOtherChrgPrev.Text = "";
            txtOtherChrgCurr.Text = "";
            txtOtherChrgTotal.Text = "";
            txtOtherChrgAmt.Text = "";
            txtOtherChrgPen.Text = "";

            txtBankChrgPrev.Text = "";
            txtBankChrgCurr.Text = "";
            txtBankChrgTotal.Text = "";
            txtBankChrgAmt.Text = "";
            txtBankChrgPen.Text = "";

            txtInsurancePrev.Text = "";
            txtInsuranceCurr.Text = "";
            txtInsuranceTotal.Text = "";
            txtInsuranceAmt.Text = "";
            txtInsurancePen.Text = "";

            txtGSTPrevAmt.Text = "";
            txtGSTCurrAmt.Text = "";
            txtGSTTotalAmt.Text = "";
            txtGSTAmt.Text = "";
            txtGSTTotPendAmt.Text = "";

            txtTotPrevAmt.Text = "";
            txtTotCurrAmt.Text = "";
            txtTotalAmt.Text = "";
            txtTotPaidAmt.Text = "";
            txtTotPendAmt.Text = "";
            txtOSOverdue.Text = "";

            ddlAccStatus.SelectedIndex = 0;
            ddlPayType.SelectedIndex = 0;
            txtProdType1.Text = "";
            txtProdName1.Text = "";
            TxtAccNo1.Text = "";
            TxtAccName1.Text = "";
            txtBalance.Text = "";
            TxtChequeNo.Text = "";
            TxtChequeDate.Text = "";
            txtNarration.Text = "";

            txtAmount.Text = "";

            txtProdType.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        finally
        {
            txtProdType.Focus();
        }
    }

    protected void ClearAll()
    {
        try
        {
            Session["IntApp"] = null;
            txtProdType.Text = "";
            txtProdName.Text = "";
            txtAccNo.Text = "";
            txtAccName.Text = "";
            txtCustNo.Text = "";
            txtDepositeAmt.Text = "";

            AccStatus.Value = "";
            txtAccType.Text = "";
            txtAccOpenDate.Text = "";
            txtSancLimit.Text = "";
            txtPeriod.Text = "";
            txtDueDate.Text = "";
            txtInstAmt.Text = "";
            txtIntRate.Text = "";
            txtTotalDP.Text = "";
            txtTotOverdue.Text = "";

            txtDepositeAmt.Text = "";
            ddlAccStatus.SelectedIndex = 0;
            ddlPayType.SelectedIndex = 0;
            Transfer.Visible = false;
            Transfer1.Visible = false;
            DivAmount.Visible = false;

            //txtPeriod.Text = "";
            txtAppliedRate.Text = "";
            txtDays.Text = "";
            txtLastIntDate.Text = "";

            ddlPayType.SelectedIndex = 0;
            txtProdType1.Text = "";
            txtProdName1.Text = "";
            TxtAccNo1.Text = "";
            TxtAccName1.Text = "";
            txtBalance.Text = "";
            TxtChequeNo.Text = "";
            TxtChequeDate.Text = "";
            txtNarration.Text = "";

            txtPrinPrev.Text = "";
            txtPrinCurr.Text = "";
            txtPrinTotal.Text = "";
            txtPrinAmt.Text = "";
            txtPrinPen.Text = "";

            txtIntPrev.Text = "";
            txtIntCurr.Text = "";
            txtIntTotal.Text = "";
            txtIntAmt.Text = "";
            txtIntPen.Text = "";

            txtPIntPrev.Text = "";
            txtPIntCurr.Text = "";
            txtPIntTotal.Text = "";
            txtPIntAmt.Text = "";
            txtPIntPen.Text = "";

            txtIntRecPrev.Text = "";
            txtIntRecCurr.Text = "";
            txtIntRecTotal.Text = "";
            txtIntRecAmt.Text = "";
            txtIntRecPen.Text = "";

            txtNotChrgPrev.Text = "";
            txtNotChrgCurr.Text = "";
            txtNotChrgTotal.Text = "";
            txtNotChrgAmt.Text = "";
            txtNotChrgPen.Text = "";

            txtSerChrgPrev.Text = "";
            txtSerChrgCurr.Text = "";
            txtSerChrgTotal.Text = "";
            txtSerChrgAmt.Text = "";
            txtSerChrgPen.Text = "";

            txtCrtChrgPrev.Text = "";
            txtCrtChrgCurr.Text = "";
            txtCrtChrgTotal.Text = "";
            txtCrtChrgAmt.Text = "";
            txtCrtChrgPen.Text = "";

            txtSurChrgPrev.Text = "";
            txtSurChrgCurr.Text = "";
            txtSurChrgTotal.Text = "";
            txtSurChrgAmt.Text = "";
            txtSurChrgPen.Text = "";

            txtOtherChrgPrev.Text = "";
            txtOtherChrgCurr.Text = "";
            txtOtherChrgTotal.Text = "";
            txtOtherChrgAmt.Text = "";
            txtOtherChrgPen.Text = "";

            txtBankChrgPrev.Text = "";
            txtBankChrgCurr.Text = "";
            txtBankChrgTotal.Text = "";
            txtBankChrgAmt.Text = "";
            txtBankChrgPen.Text = "";

            txtInsurancePrev.Text = "";
            txtInsuranceCurr.Text = "";
            txtInsuranceTotal.Text = "";
            txtInsuranceAmt.Text = "";
            txtInsurancePen.Text = "";

            txtGSTPrevAmt.Text = "";
            txtGSTCurrAmt.Text = "";
            txtGSTTotalAmt.Text = "";
            txtGSTAmt.Text = "";
            txtGSTTotPendAmt.Text = "";

            txtTotPrevAmt.Text = "";
            txtTotCurrAmt.Text = "";
            txtTotalAmt.Text = "";
            txtTotPaidAmt.Text = "";
            txtTotPendAmt.Text = "";
            txtOSOverdue.Text = "";

            txtProdType1.Text = "";
            txtProdName1.Text = "";
            TxtAccNo1.Text = "";
            TxtAccName1.Text = "";
            txtBalance.Text = "";
            TxtChequeNo.Text = "";
            TxtChequeDate.Text = "";
            txtNarration.Text = "";
            txtAmount.Text = "";

            txtProdType.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        finally
        {
            txtProdType.Focus();
        }
    }

    protected void ClearTotAmt()
    {
        try
        {
            txtPrinAmt.Text = "";
            txtIntAmt.Text = "";
            txtPIntAmt.Text = "";
            txtIntRecAmt.Text = "";
            txtNotChrgAmt.Text = "";
            txtSerChrgAmt.Text = "";
            txtCrtChrgAmt.Text = "";
            txtSurChrgAmt.Text = "";
            txtOtherChrgAmt.Text = "";
            txtBankChrgAmt.Text = "";
            txtInsuranceAmt.Text = "";
            txtGSTAmt.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    ////Added by ankita on 26/06/2017
    public void BindGrid1()
    {
        try
        {
            CurrentCls.Getinfotable(grdCashRct, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "LoanClose");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Grid

    protected void GridRecords_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TotalCr += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Credit_PR"));
            TotalDE += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Debit_PR"));
            TotalIn += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "InterestAmt"));
            TotalPI += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PenalInt"));
            TotalIR += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "IntReceivable"));
            TotalNC += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoticeCharge"));
            TotalSC += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ServiceCharge"));
            TotalCC += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CourtCharge"));
            TotalSC1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SerCharge"));
            TotalOC += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "OtherCharge"));
            TotalI += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Insurance"));
            TotalBC += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BankCharge"));
            TotalBal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TBal"));
            ToptalCL += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BALANCE"));

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            ((Label)e.Row.FindControl("lblTCr")).Text = TotalCr.ToString();
            ((Label)e.Row.FindControl("lblTDe")).Text = TotalDE.ToString();
            ((Label)e.Row.FindControl("lblTIn")).Text = TotalIn.ToString();
            ((Label)e.Row.FindControl("lblTPI")).Text = TotalPI.ToString();
            ((Label)e.Row.FindControl("lblTIR")).Text = TotalIR.ToString();
            ((Label)e.Row.FindControl("lblTNC")).Text = TotalNC.ToString();
            ((Label)e.Row.FindControl("lblTSC")).Text = TotalSC.ToString();
            ((Label)e.Row.FindControl("lblTCC")).Text = TotalCC.ToString();
            ((Label)e.Row.FindControl("lblTSC1")).Text = TotalSC1.ToString();
            ((Label)e.Row.FindControl("lblTOC")).Text = TotalOC.ToString();
            ((Label)e.Row.FindControl("lblTI")).Text = TotalI.ToString();
            ((Label)e.Row.FindControl("lblTBC")).Text = TotalBC.ToString();
            ((Label)e.Row.FindControl("lblTTC")).Text = TotalBal.ToString();

        }
    }

    protected void GridGurantor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((Label)e.Row.FindControl("lblGurantor")).Text = "Gurantor" + (IntId + 1).ToString() + ":";
            IntId = IntId + 1;
        }
    }

    #endregion

    #region Click Event

    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            string SetNo, SetNo1 = "";
            double BAL, CurBal, PendingTotal;
            string CN = "", CD = "";

            IntCalType = LI.GetIntCal(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString());

            BAL = Convert.ToDouble(txtBalance.Text == "" ? "0" : txtBalance.Text);
            CurBal = Convert.ToDouble(txtAmount.Text == "" ? "0" : txtAmount.Text);

            if (ddlPayType.SelectedValue.ToString() != "0")
            {
                string ConfirmValue = Response.Value;

                if (CheckInterest() == true)
                {
                    if (CheckPenal() == true)
                    {
                        if (btnSubmit.Visible == true)
                        {
                            if (Convert.ToDouble(txtDepositeAmt.Text.Trim().ToString() == "" ? "0" : txtDepositeAmt.Text.Trim().ToString()) == Convert.ToDouble(txtTotPaidAmt.Text.Trim().ToString() == "" ? "0" : txtTotPaidAmt.Text.Trim().ToString()))
                            {
                                PendingTotal = Convert.ToDouble(Convert.ToDouble(txtTotPendAmt.Text.Trim().ToString() == "" ? "0" : txtTotPendAmt.Text.Trim().ToString()));

                                if (PendingTotal == 0)
                                {
                                    btnSubmit.Visible = false;

                                    RefNumber = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                                    ViewState["RID"] = (Convert.ToInt32(RefNumber) + 1).ToString();
                                    SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
                                    ViewState["SetNo"] = SetNo.ToString();

                                    //If Check No is Empty
                                    if (TxtChequeNo.Text.Trim().ToString() == "")
                                    {
                                        CN = "0";
                                    }
                                    else
                                    {
                                        CN = TxtChequeNo.Text.Trim().ToString();
                                    }

                                    //If Check Date is Empty
                                    if (TxtChequeDate.Text.Trim().ToString() == "")
                                    {
                                        CD = "01/01/1990";
                                    }
                                    else
                                    {
                                        CD = TxtChequeDate.Text.Trim().ToString();
                                    }

                                    if (IntCalType == 1 || IntCalType == 2)
                                    {
                                        if (ddlPayType.SelectedValue.ToString() == "1")
                                        {
                                            Activity = 3; PmtMode = "CR";
                                            string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());

                                            resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", "99", txtAccNo.Text.Trim().ToString(),
                                                        txtNarration.Text, "PAYDR", Convert.ToDouble(txtTotPaidAmt.Text.Trim().ToString() == "" ? "0" : txtTotPaidAmt.Text.Trim().ToString()), "2", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                        "", Session["BRCD"].ToString(), Session["MID"].ToString(), txtProdType.Text.Trim().ToString() + "/" + txtAccNo.Text.Trim().ToString(), "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());
                                        }
                                        //Amol
                                        else if (ddlPayType.SelectedValue.ToString() == "2")
                                        {
                                            Activity = 7; PmtMode = "TR";
                                            if (Convert.ToInt32(ViewState["GLCODE1"].ToString() == "" ? "0" : ViewState["GLCODE1"].ToString()) < 100)
                                            {
                                                if (Convert.ToDouble(txtTotPaidAmt.Text.Trim().ToString() == "" ? "0" : txtTotPaidAmt.Text.Trim().ToString()) > Convert.ToDouble(txtBalance.Text.Trim().ToString() == "" ? "0" : txtBalance.Text.Trim().ToString()))
                                                {
                                                    Clear();
                                                    WebMsgBox.Show("Sorry Insufficient Account Balance ...!!", this.Page);
                                                    return; 
                                                }
                                            }

                                            resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE1"].ToString(), txtProdType1.Text.Trim().ToString(),
                                                        TxtAccNo1.Text.Trim().ToString(), txtNarration.Text, "PAYDR", Convert.ToDouble(txtTotPaidAmt.Text.Trim().ToString() == "" ? "0" : txtTotPaidAmt.Text.Trim().ToString()), "2", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                        "", Session["BRCD"].ToString(), Session["MID"].ToString(), txtProdType.Text.Trim().ToString() + "/" + txtAccNo.Text.Trim().ToString(), "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());
                                        }
                                        //Amol
                                        else if (ddlPayType.SelectedValue.ToString() == "4")
                                        {
                                            Activity = 5; PmtMode = "TR";

                                            resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE1"].ToString(), txtProdType1.Text.Trim().ToString(),
                                                        TxtAccNo1.Text.Trim().ToString(), txtNarration.Text, "PAYDR", Convert.ToDouble(txtTotPaidAmt.Text.Trim().ToString() == "" ? "0" : txtTotPaidAmt.Text.Trim().ToString()), "2", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                        "", Session["BRCD"].ToString(), Session["MID"].ToString(), txtProdType.Text.Trim().ToString() + "/" + txtAccNo.Text.Trim().ToString(), "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());
                                        }

                                        if (resultint > 0)
                                        {
                                            if (Convert.ToDouble(txtPrinAmt.Text.Trim().ToString() == "" ? "0" : txtPrinAmt.Text.Trim().ToString()) > 0)
                                            {
                                                //Principle O/S Credit To Specific GL (e.g 3)
                                                resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PrincipleGl"].ToString(), ViewState["PrincipleSub"].ToString(),
                                                    txtAccNo.Text.ToString(), txtNarration.Text, "PRNCR", Convert.ToDouble(txtPrinAmt.Text.Trim().ToString() == "" ? "0" : txtPrinAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                            "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                if (resultint > 0)
                                                {
                                                    if (Convert.ToDouble(txtPrinCurr.Text.Trim().ToString() == "" ? "0" : txtPrinCurr.Text.Trim().ToString()) > 0)
                                                    {
                                                        // Principle O/S Debit To 1 In AVS_LnTrx
                                                        resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["PrincipleSub"].ToString(), txtAccNo.Text.Trim().ToString(), "1", "2", Activity.ToString(), "Principle Debited", Convert.ToDouble(txtPrinCurr.Text.Trim().ToString() == "" ? "0" : txtPrinCurr.Text.Trim().ToString()).ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }

                                                if (resultint > 0)
                                                {
                                                    if (Convert.ToDouble(txtPrinAmt.Text.Trim().ToString() == "" ? "0" : txtPrinAmt.Text.Trim().ToString()) > 0)
                                                    {
                                                        // Principle O/S Credit To 1 In AVS_LnTrx
                                                        resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["PrincipleSub"].ToString(), txtAccNo.Text.Trim().ToString(), "1", "1", Activity.ToString(), "Principle Credited", txtPrinAmt.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }
                                            }

                                            if (resultint > 0)
                                            {
                                                if (IntCalType == 1)
                                                {
                                                    if (Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString()) > 0)
                                                    {
                                                        //interest Received Credit to GL 11
                                                        resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["InterestGl"].ToString(), ViewState["InterestSub"].ToString(),
                                                            txtAccNo.Text.ToString(), txtNarration.Text, "INTCR", Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());
                                                    }

                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtIntCurr.Text.Trim().ToString() == "" ? "0" : txtIntCurr.Text.Trim().ToString()) > 0)
                                                        {
                                                            //Current Interest Debit To 2 In AVS_LnTrx
                                                            resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["InterestSub"].ToString(), txtAccNo.Text.Trim().ToString(), "2", "2", Activity.ToString(), txtNarration.Text.ToString(), txtIntCurr.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString()) > 0)
                                                        {
                                                            //Current Interest Credit To 2 In AVS_LnTrx
                                                            resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["InterestSub"].ToString(), txtAccNo.Text.Trim().ToString(), "2", "1", Activity.ToString(), txtNarration.Text.ToString(), txtIntAmt.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (resultint > 0)
                                                    {
                                                        //interest Applied Contra
                                                        if (Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString()) > 0)
                                                        {
                                                            //interest Applied Debit To GL 11
                                                            resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["InterestGl"].ToString(), ViewState["InterestSub"].ToString(),
                                                                txtAccNo.Text.ToString(), txtNarration.Text, "INTDR", Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString()), "2", "11", "TR_INT", SetNo, CN, CD, "0", "0", "1001",
                                                                        "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                            if (resultint > 0)
                                                            {
                                                                //interest Applied Credit to GL 100
                                                                resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", ViewState["PlAccNo1"].ToString(),
                                                                            txtAccNo.Text.ToString(), txtNarration.Text.ToString(), "INTCR", Convert.ToDouble(txtIntAmt.Text.Trim().ToString()), "1", "11", "TR_INT", SetNo, CN, CD, "0", "0", "1001",
                                                                            "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (IntCalType == 2)
                                                {
                                                    if (Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString()) > 0)
                                                    {
                                                        //interest Received Credit to GL 3
                                                        resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PrincipleGl"].ToString(), ViewState["PrincipleSub"].ToString(),
                                                            txtAccNo.Text.ToString(), txtNarration.Text, "INTCR", Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());
                                                    }

                                                    //  Added As Per ambika mam Instruction 22-06-2017
                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString()) > 0)
                                                        {
                                                            //interest Applied Debit To GL 3
                                                            resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PrincipleGl"].ToString(), ViewState["PrincipleSub"].ToString(),
                                                                        txtAccNo.Text.ToString(), txtNarration.Text, "INTDR", Convert.ToDouble(txtIntAmt.Text.Trim().ToString()), "2", "11", "TR_INT", SetNo, CN, CD, "0", "0", "1001",
                                                                        "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                            if (resultint > 0)
                                                            {
                                                                //interest Applied Credit to GL 100
                                                                resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", ViewState["PlAccNo1"].ToString(),
                                                                            txtAccNo.Text.ToString(), txtNarration.Text.ToString(), "INTCR", Convert.ToDouble(txtIntAmt.Text.Trim().ToString()), "1", "11", "TR_INT", SetNo, CN, CD, "0", "0", "1001",
                                                                            "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());
                                                            }
                                                        }
                                                    }
                                                }
                                                else if (IntCalType == 3)
                                                {
                                                    if (Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString()) > 0)
                                                    {
                                                        //interest Received Credit to GL 11
                                                        resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["InterestGl"].ToString(), ViewState["InterestSub"].ToString(),
                                                            txtAccNo.Text.ToString(), txtNarration.Text, "INTCR", Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());
                                                    }

                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtIntCurr.Text.Trim().ToString() == "" ? "0" : txtIntCurr.Text.Trim().ToString()) > 0)
                                                        {
                                                            //Current Interest Debit To 2 In AVS_LnTrx
                                                            resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["InterestSub"].ToString(), txtAccNo.Text.Trim().ToString(), "2", "2", Activity.ToString(), txtNarration.Text.ToString(), txtIntCurr.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString()) > 0)
                                                        {
                                                            //Current Interest Credit To 2 In AVS_LnTrx
                                                            resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["InterestSub"].ToString(), txtAccNo.Text.Trim().ToString(), "2", "1", Activity.ToString(), txtNarration.Text.ToString(), txtIntAmt.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                }
                                            }

                                            if (resultint > 0)
                                            {
                                                if (Convert.ToDouble(txtPIntAmt.Text.Trim().ToString() == "" ? "0" : txtPIntAmt.Text.Trim().ToString()) > 0)
                                                {
                                                    //Penal Charge Credit To GL 12
                                                    resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PInterestGl"].ToString(), ViewState["PInterestSub"].ToString(),
                                                        txtAccNo.Text.ToString(), txtNarration.Text, "PENCR", Convert.ToDouble(txtPIntAmt.Text.Trim().ToString() == "" ? "0" : txtPIntAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                                "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());
                                                }

                                                if (resultint > 0)
                                                {
                                                    if (Convert.ToDouble(txtPIntCurr.Text.Trim().ToString() == "" ? "0" : txtPIntCurr.Text.Trim().ToString()) > 0)
                                                    {
                                                        //Penal Interest Debit To 3 In AVS_LnTrx
                                                        resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["PInterestSub"].ToString(), txtAccNo.Text.Trim().ToString(), "3", "2", Activity.ToString(), "Penal Debited", txtPIntCurr.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }

                                                if (resultint > 0)
                                                {
                                                    if (Convert.ToDouble(txtPIntAmt.Text.Trim().ToString() == "" ? "0" : txtPIntAmt.Text.Trim().ToString()) > 0)
                                                    {
                                                        //Penal Interest Credit To 3 In AVS_LnTrx
                                                        resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["PInterestSub"].ToString(), txtAccNo.Text.Trim().ToString(), "3", "1", Activity.ToString(), "Penal Credited", txtPIntAmt.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }

                                                if (resultint > 0)
                                                {
                                                    //Penal Charge Contra
                                                    if (Convert.ToDouble(txtPIntAmt.Text.Trim().ToString() == "" ? "0" : txtPIntAmt.Text.Trim().ToString()) > 0)
                                                    {
                                                        //Penal chrg Applied Debit To GL 12
                                                        resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PInterestGl"].ToString(), ViewState["PInterestSub"].ToString(),
                                                                    txtAccNo.Text.ToString(), txtNarration.Text, "PENDR", Convert.ToDouble(txtPIntAmt.Text.Trim().ToString()), "2", "12", "TR_INT", SetNo, CN, CD, "0", "0", "1001",
                                                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                        if (resultint > 0)
                                                        {
                                                            //Penal chrg Applied Credit to GL 100
                                                            resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", ViewState["PlAccNo2"].ToString(),
                                                                        txtAccNo.Text.ToString(), txtNarration.Text.ToString(), "PENCR", Convert.ToDouble(txtPIntAmt.Text.Trim().ToString()), "1", "12", "TR_INT", SetNo, CN, CD, "0", "0", "1001",
                                                                        "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                }
                                            }

                                            if (resultint > 0)
                                            {
                                                if (Convert.ToDouble(txtIntRecAmt.Text == "" ? "0" : txtIntRecAmt.Text) > 0)
                                                {
                                                    if (IntCalType == 1 || IntCalType == 3)
                                                    {
                                                        // Interest Received Credited To GL 11
                                                        resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["InterestRecGl"].ToString(), ViewState["InterestRecSub"].ToString(),
                                                            txtAccNo.Text.ToString(), txtNarration.Text, "INTRCR", Convert.ToDouble(txtIntRecAmt.Text.Trim().ToString() == "" ? "0" : txtIntRecAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                        if (resultint > 0)
                                                        {
                                                            if (Convert.ToDouble(txtIntRecAmt.Text.Trim().ToString() == "" ? "0" : txtIntRecAmt.Text.Trim().ToString()) > 0)
                                                            {
                                                                // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                                resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["InterestRecSub"].ToString(), txtAccNo.Text.Trim().ToString(), "4", "1", Activity.ToString(), "Interest Credited", txtIntRecAmt.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                        }
                                                    }
                                                    else if (IntCalType == 2)
                                                    {
                                                        // Interest Received Credited to GL 3
                                                        resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PrincipleGl"].ToString(), ViewState["PrincipleSub"].ToString(),
                                                            txtAccNo.Text.ToString(), txtNarration.Text, "INTRCR", Convert.ToDouble(txtIntRecAmt.Text.Trim().ToString() == "" ? "0" : txtIntRecAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());
                                                    }
                                                }
                                            }

                                            if (resultint > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(txtNotChrgPrev.Text == "" ? "0" : txtNotChrgPrev.Text) + Convert.ToDouble(txtNotChrgCurr.Text == "" ? "0" : txtNotChrgCurr.Text)) > 0)
                                                {
                                                    // Notice Charges
                                                    resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["NoticeChrgGl"].ToString(), ViewState["NoticeChrgSub"].ToString(),
                                                        txtAccNo.Text.ToString(), txtNarration.Text, "NOTCR", Convert.ToDouble(txtNotChrgAmt.Text.Trim().ToString() == "" ? "0" : txtNotChrgAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                                "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtNotChrgCurr.Text.Trim().ToString() == "" ? "0" : txtNotChrgCurr.Text.Trim().ToString()) > 0)
                                                        {
                                                            // Notice Charges Debit To 5 In AVS_LnTrx
                                                            resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["NoticeChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "5", "2", Activity.ToString(), txtNarration.Text.ToString(), txtNotChrgCurr.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtNotChrgAmt.Text.Trim().ToString() == "" ? "0" : txtNotChrgAmt.Text.Trim().ToString()) > 0)
                                                        {
                                                            // Notice Charges Credit To 5 In AVS_LnTrx
                                                            resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["NoticeChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "5", "1", Activity.ToString(), txtNarration.Text.ToString(), txtNotChrgAmt.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                }
                                            }

                                            if (resultint > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(txtSerChrgPrev.Text == "" ? "0" : txtSerChrgPrev.Text) + Convert.ToDouble(txtSerChrgCurr.Text == "" ? "0" : txtSerChrgCurr.Text)) > 0)
                                                {
                                                    // Service Charges
                                                    resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["ServiceChrgGl"].ToString(), ViewState["ServiceChrgSub"].ToString(),
                                                        txtAccNo.Text.ToString(), txtNarration.Text, "SERCR", Convert.ToDouble(txtSerChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSerChrgAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                                "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtSerChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSerChrgCurr.Text.Trim().ToString()) > 0)
                                                        {
                                                            // Service Charges Amt Debit To 6 In AVS_LnTrx
                                                            resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["ServiceChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "6", "2", Activity.ToString(), txtNarration.Text.ToString(), txtSerChrgCurr.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtSerChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSerChrgAmt.Text.Trim().ToString()) > 0)
                                                        {
                                                            // Service Charges Amt Credit To 6 In AVS_LnTrx
                                                            resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["ServiceChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "6", "1", Activity.ToString(), txtNarration.Text.ToString(), txtSerChrgAmt.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                }
                                            }

                                            if (resultint > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(txtCrtChrgPrev.Text == "" ? "0" : txtCrtChrgPrev.Text) + Convert.ToDouble(txtCrtChrgCurr.Text == "" ? "0" : txtCrtChrgCurr.Text)) > 0)
                                                {
                                                    // Court Charges
                                                    resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["CourtChrgGl"].ToString(), ViewState["CourtChrgSub"].ToString(),
                                                        txtAccNo.Text.ToString(), txtNarration.Text, "CRTCR", Convert.ToDouble(txtCrtChrgAmt.Text.Trim().ToString() == "" ? "0" : txtCrtChrgAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                                "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtCrtChrgCurr.Text.Trim().ToString() == "" ? "0" : txtCrtChrgCurr.Text.Trim().ToString()) > 0)
                                                        {
                                                            // Court Charges Amt Debit To 7 In AVS_LnTrx
                                                            resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["CourtChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "7", "2", Activity.ToString(), txtNarration.Text.ToString(), txtCrtChrgCurr.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtCrtChrgAmt.Text.Trim().ToString() == "" ? "0" : txtCrtChrgAmt.Text.Trim().ToString()) > 0)
                                                        {
                                                            // Court Charges Amt Credit To 7 In AVS_LnTrx
                                                            resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["CourtChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "7", "1", Activity.ToString(), txtNarration.Text.ToString(), txtCrtChrgAmt.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                }
                                            }

                                            if (resultint > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(txtSurChrgPrev.Text == "" ? "0" : txtSurChrgPrev.Text) + Convert.ToDouble(txtSurChrgCurr.Text == "" ? "0" : txtSurChrgCurr.Text)) > 0)
                                                {
                                                    // Sur Charges
                                                    resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["SurChrgGl"].ToString(), ViewState["SurChrgSub"].ToString(),
                                                        txtAccNo.Text.ToString(), txtNarration.Text, "SURCR", Convert.ToDouble(txtSurChrgAmt.Text.ToString().ToString() == "" ? "0" : txtSurChrgAmt.Text.ToString().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                                "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtSurChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSurChrgCurr.Text.Trim().ToString()) > 0)
                                                        {
                                                            // Sur Charges debit To 8 In AVS_LnTrx
                                                            resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["SurChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "8", "2", Activity.ToString(), txtNarration.Text.ToString(), txtSurChrgCurr.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtSurChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSurChrgAmt.Text.Trim().ToString()) > 0)
                                                        {
                                                            // Sur Charges Credit To 8 In AVS_LnTrx
                                                            resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["SurChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "8", "1", Activity.ToString(), txtNarration.Text.ToString(), txtSurChrgAmt.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                }
                                            }

                                            if (resultint > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(txtOtherChrgPrev.Text == "" ? "0" : txtOtherChrgPrev.Text) + Convert.ToDouble(txtOtherChrgCurr.Text == "" ? "0" : txtOtherChrgCurr.Text)) > 0)
                                                {
                                                    // Other Charges
                                                    resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["OtherChrgGl"].ToString(), ViewState["OtherChrgSub"].ToString(),
                                                        txtAccNo.Text.ToString(), txtNarration.Text, "OTHCR", Convert.ToDouble(txtOtherChrgAmt.Text.Trim().ToString() == "" ? "0" : txtOtherChrgAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                                "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtOtherChrgCurr.Text.Trim().ToString() == "" ? "0" : txtOtherChrgCurr.Text.Trim().ToString()) > 0)
                                                        {
                                                            // Other Charges Amt Debit To 9 In AVS_LnTrx
                                                            resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["OtherChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "9", "2", Activity.ToString(), txtNarration.Text.ToString(), txtOtherChrgCurr.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtOtherChrgAmt.Text.Trim().ToString() == "" ? "0" : txtOtherChrgAmt.Text.Trim().ToString()) > 0)
                                                        {
                                                            // Other Charges Amt Credit To 9 In AVS_LnTrx
                                                            resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["OtherChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "9", "1", Activity.ToString(), txtNarration.Text.ToString(), txtOtherChrgAmt.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                }
                                            }

                                            if (resultint > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(txtBankChrgPrev.Text == "" ? "0" : txtBankChrgPrev.Text) + Convert.ToDouble(txtBankChrgCurr.Text == "" ? "0" : txtBankChrgCurr.Text)) > 0)
                                                {
                                                    // Bank Charges
                                                    resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["BankChrgGl"].ToString(), ViewState["BankChrgSub"].ToString(),
                                                                txtAccNo.Text.ToString(), txtNarration.Text, "BNKCR", Convert.ToDouble(txtBankChrgAmt.Text.Trim().ToString() == "" ? "0" : txtBankChrgAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                                "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtBankChrgCurr.Text.Trim().ToString() == "" ? "0" : txtBankChrgCurr.Text.Trim().ToString()) > 0)
                                                        {
                                                            // Bank Charges Amt Debit To 10 In AVS_LnTrx
                                                            resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["BankChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "10", "2", Activity.ToString(), txtNarration.Text.ToString(), txtBankChrgCurr.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtBankChrgAmt.Text.Trim().ToString() == "" ? "0" : txtBankChrgAmt.Text.Trim().ToString()) > 0)
                                                        {
                                                            // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                                            resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["BankChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "10", "1", Activity.ToString(), txtNarration.Text.ToString(), txtBankChrgAmt.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                }
                                            }

                                            if (resultint > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(txtInsurancePrev.Text == "" ? "0" : txtInsurancePrev.Text) + Convert.ToDouble(txtInsuranceCurr.Text == "" ? "0" : txtInsuranceCurr.Text)) > 0)
                                                {
                                                    //  Insurance Charge
                                                    resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["InsChrgGl"].ToString(), ViewState["InsChrgSub"].ToString(),
                                                                txtAccNo.Text.ToString(), txtNarration.Text, "INSCR", Convert.ToDouble(txtInsuranceAmt.Text.Trim().ToString() == "" ? "0" : txtInsuranceAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                                "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtInsuranceCurr.Text.Trim().ToString() == "" ? "0" : txtInsuranceCurr.Text.Trim().ToString()) > 0)
                                                        {
                                                            //  Insurance Charge Debit To 11 In AVS_LnTrx
                                                            resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["InsChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "11", "2", Activity.ToString(), txtNarration.Text.ToString(), txtInsuranceCurr.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtInsuranceAmt.Text.Trim().ToString() == "" ? "0" : txtInsuranceAmt.Text.Trim().ToString()) > 0)
                                                        {
                                                            //  Insurance Charge Credit To 11 In AVS_LnTrx
                                                            resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["InsChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "11", "1", Activity.ToString(), txtNarration.Text.ToString(), txtInsuranceAmt.Text.Trim().ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                }
                                            }

                                            if ((resultint > 0) && (Convert.ToDouble(txtGSTAmt.Text.ToString() == "" ? "0" : txtGSTAmt.Text.ToString()) > 0))
                                            {
                                                DT = LI.GstDetails(Session["BRCD"].ToString());
                                                if (DT.Rows.Count > 0)
                                                {
                                                    CGST = Math.Round(Convert.ToDouble(txtGSTAmt.Text.ToString()) / 2, 2);
                                                    SGST = Math.Round(Convert.ToDouble(txtGSTAmt.Text.ToString()) / 2, 2);

                                                    resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["SGSTGlCode"].ToString(), DT.Rows[0]["SGSTPrdCd"].ToString(),
                                                            txtAccNo.Text.ToString(), txtNarration.Text, "GST_State", CGST, "1", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LOANINST", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                    resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["CGSTGlCode"].ToString(), DT.Rows[0]["CGSTPrdCd"].ToString(),
                                                            txtAccNo.Text.ToString(), txtNarration.Text, "GST_Central", SGST, "1", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LOANINST", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());
                                                }
                                            }
                                        }
                                    }
                                    if (IntCalType == 3)
                                    {
                                        if (ddlPayType.SelectedValue.ToString() == "1")
                                        {
                                            Activity = 3; PmtMode = "CR";
                                            string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());

                                            resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", "99", txtAccNo.Text.Trim().ToString(),
                                                        txtNarration.Text, "PAYDR", Convert.ToDouble(txtTotPaidAmt.Text.Trim().ToString() == "" ? "0" : txtTotPaidAmt.Text.Trim().ToString()), "2", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                        "", Session["BRCD"].ToString(), Session["MID"].ToString(), txtProdType.Text.Trim().ToString() + "/" + txtAccNo.Text.Trim().ToString(), "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());
                                        }
                                        else if (ddlPayType.SelectedValue.ToString() == "2")
                                        {
                                            Activity = 7; PmtMode = "TR";
                                            if (Convert.ToInt32(ViewState["GLCODE1"].ToString() == "" ? "0" : ViewState["GLCODE1"].ToString()) < 100)
                                            {
                                                if (Convert.ToDouble(txtTotPaidAmt.Text.Trim().ToString() == "" ? "0" : txtTotPaidAmt.Text.Trim().ToString()) > Convert.ToDouble(txtBalance.Text.Trim().ToString() == "" ? "0" : txtBalance.Text.Trim().ToString()))
                                                {
                                                    Clear();
                                                    WebMsgBox.Show("Sorry Insufficient Account Balance ...!!", this.Page);
                                                    return;
                                                }
                                            }

                                            resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE1"].ToString(), txtProdType1.Text.Trim().ToString(),
                                                        TxtAccNo1.Text.Trim().ToString(), txtNarration.Text, "PAYDR", Convert.ToDouble(txtTotPaidAmt.Text.Trim().ToString() == "" ? "0" : txtTotPaidAmt.Text.Trim().ToString()), "2", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                        "", Session["BRCD"].ToString(), Session["MID"].ToString(), txtProdType.Text.Trim().ToString() + "/" + txtAccNo.Text.Trim().ToString(), "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());
                                        }
                                        else if (ddlPayType.SelectedValue.ToString() == "4")
                                        {
                                            Activity = 5; PmtMode = "TR";

                                            resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE1"].ToString(), txtProdType1.Text.Trim().ToString(),
                                                        TxtAccNo1.Text.Trim().ToString(), txtNarration.Text, "PAYDR", Convert.ToDouble(txtTotPaidAmt.Text.Trim().ToString() == "" ? "0" : txtTotPaidAmt.Text.Trim().ToString()), "2", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                        "", Session["BRCD"].ToString(), Session["MID"].ToString(), txtProdType.Text.Trim().ToString() + "/" + txtAccNo.Text.Trim().ToString(), "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());
                                        }

                                        if (resultint > 0)
                                        {
                                            //Principle O/S Credit To Specific GL (e.g 3)
                                            resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PrincipleGl"].ToString(), ViewState["PrincipleSub"].ToString(),
                                                        txtAccNo.Text.ToString(), txtNarration.Text, "PRNCR", Convert.ToDouble(txtTotPaidAmt.Text.Trim().ToString() == "" ? "0" : txtTotPaidAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo, CN, CD, "0", "0", "1001",
                                                        "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                            if (resultint > 0)
                                            {
                                                if (Convert.ToDouble(txtPrinCurr.Text.Trim().ToString() == "" ? "0" : txtPrinCurr.Text.Trim().ToString()) > 0)
                                                {
                                                    // Principle O/S Debit To 1 In AVS_LnTrx
                                                    resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["PrincipleSub"].ToString(), txtAccNo.Text.Trim().ToString(), "1", "2", Activity.ToString(), "Principle Debited", Convert.ToDouble(txtPrinCurr.Text.Trim().ToString() == "" ? "0" : txtPrinCurr.Text.Trim().ToString()).ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                }
                                            }

                                            if (resultint > 0)
                                            {
                                                if (Convert.ToDouble(txtPrinAmt.Text.Trim().ToString() == "" ? "0" : txtPrinAmt.Text.Trim().ToString()) > 0)
                                                {
                                                    // Principle O/S Credit To 1 In AVS_LnTrx
                                                    resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["PrincipleSub"].ToString(), txtAccNo.Text.Trim().ToString(), "1", "1", Activity.ToString(), "Principle Credited", Convert.ToDouble(txtTotPaidAmt.Text.Trim().ToString() == "" ? "0" : txtTotPaidAmt.Text.Trim().ToString()).ToString(), SetNo, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                }
                                            }
                                        }

                                        //Generate transfer set number here
                                        Activity = 7; PmtMode = "TR";
                                        SetNo1 = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
                                        ViewState["SetNo1"] = SetNo1.ToString();

                                        //Transfer Entry
                                        if (resultint > 0)
                                        {
                                            TotalAmt = Convert.ToDouble(Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString()) + Convert.ToDouble(txtPIntAmt.Text.Trim().ToString() == "" ? "0" : txtPIntAmt.Text.Trim().ToString()) +
                                                       Convert.ToDouble(txtIntRecAmt.Text.Trim().ToString() == "" ? "0" : txtIntRecAmt.Text.Trim().ToString()) + Convert.ToDouble(txtNotChrgAmt.Text.Trim().ToString() == "" ? "0" : txtNotChrgAmt.Text.Trim().ToString()) +
                                                       Convert.ToDouble(txtSerChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSerChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtCrtChrgAmt.Text.Trim().ToString() == "" ? "0" : txtCrtChrgAmt.Text.Trim().ToString()) +
                                                       Convert.ToDouble(txtSurChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSurChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtOtherChrgAmt.Text.Trim().ToString() == "" ? "0" : txtOtherChrgAmt.Text.Trim().ToString()) +
                                                       Convert.ToDouble(txtBankChrgAmt.Text.Trim().ToString() == "" ? "0" : txtBankChrgAmt.Text.Trim().ToString()) + Convert.ToDouble(txtInsuranceAmt.Text.Trim().ToString() == "" ? "0" : txtInsuranceAmt.Text.Trim().ToString()) +
                                                       Convert.ToDouble(txtGSTAmt.Text.ToString() == "" ? "0" : txtGSTAmt.Text.ToString()));

                                            //Principle O/S Debit To Specific GL (e.g 3)
                                            resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PrincipleGl"].ToString(), ViewState["PrincipleSub"].ToString(),
                                                txtAccNo.Text.ToString(), txtNarration.Text, "PAYDR", Convert.ToDouble(TotalAmt.ToString() == "" ? "0" : TotalAmt.ToString()), "2", Activity.ToString(), PmtMode.ToString(), SetNo1.ToString(), "0", "01/01/1900", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Session["MID"].ToString(), txtProdType.Text.Trim().ToString() + "/" + txtAccNo.Text.Trim().ToString(), "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                            if (resultint > 0)
                                            {
                                                if (Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString()) > 0)
                                                {
                                                    //interest Credit to GL 11
                                                    resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["InterestGl"].ToString(), ViewState["InterestSub"].ToString(),
                                                        txtAccNo.Text.ToString(), txtNarration.Text, "INTCR", Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo1.ToString(), "0", "01/01/1900", "0", "0", "1003",
                                                                "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());
                                                }

                                                if (resultint > 0)
                                                {
                                                    if (Convert.ToDouble(txtIntCurr.Text.Trim().ToString() == "" ? "0" : txtIntCurr.Text.Trim().ToString()) > 0)
                                                    {
                                                        //Current Interest Debit To 2 In AVS_LnTrx
                                                        resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["InterestSub"].ToString(), txtAccNo.Text.Trim().ToString(), "2", "2", Activity.ToString(), txtNarration.Text.ToString(), txtIntCurr.Text.Trim().ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }

                                                if (resultint > 0)
                                                {
                                                    if (Convert.ToDouble(txtIntAmt.Text.Trim().ToString() == "" ? "0" : txtIntAmt.Text.Trim().ToString()) > 0)
                                                    {
                                                        //Current Interest Credit To 2 In AVS_LnTrx
                                                        resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["InterestSub"].ToString(), txtAccNo.Text.Trim().ToString(), "2", "1", Activity.ToString(), txtNarration.Text.ToString(), txtIntAmt.Text.Trim().ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }

                                                if (resultint > 0)
                                                {
                                                    if (Convert.ToDouble(txtPIntAmt.Text.Trim().ToString() == "" ? "0" : txtPIntAmt.Text.Trim().ToString()) > 0)
                                                    {
                                                        //Penal Charge Credit To GL 12
                                                        resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PInterestGl"].ToString(), ViewState["PInterestSub"].ToString(),
                                                            txtAccNo.Text.ToString(), txtNarration.Text, "PENCR", Convert.ToDouble(txtPIntAmt.Text.Trim().ToString() == "" ? "0" : txtPIntAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo1.ToString(), "0", "01/01/1900", "0", "0", "1003",
                                                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());
                                                    }

                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtPIntCurr.Text.Trim().ToString() == "" ? "0" : txtPIntCurr.Text.Trim().ToString()) > 0)
                                                        {
                                                            //Penal Interest Debit To 3 In AVS_LnTrx
                                                            resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["PInterestSub"].ToString(), txtAccNo.Text.Trim().ToString(), "3", "2", Activity.ToString(), "Penal Debited", txtPIntCurr.Text.Trim().ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (resultint > 0)
                                                    {
                                                        if (Convert.ToDouble(txtPIntAmt.Text.Trim().ToString() == "" ? "0" : txtPIntAmt.Text.Trim().ToString()) > 0)
                                                        {
                                                            //Penal Interest Credit To 3 In AVS_LnTrx
                                                            resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["PInterestSub"].ToString(), txtAccNo.Text.Trim().ToString(), "3", "1", Activity.ToString(), "Penal Credited", txtPIntAmt.Text.Trim().ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (resultint > 0)
                                                    {
                                                        //Penal Charge Contra
                                                        if (Convert.ToDouble(txtPIntAmt.Text.Trim().ToString() == "" ? "0" : txtPIntAmt.Text.Trim().ToString()) > 0)
                                                        {
                                                            //Penal chrg Applied Debit To GL 12
                                                            resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PInterestGl"].ToString(), ViewState["PInterestSub"].ToString(),
                                                                txtAccNo.Text.ToString(), txtNarration.Text, "PENDR", Convert.ToDouble(txtPIntAmt.Text.Trim().ToString()), "2", "12", "TR_INT", SetNo1.ToString(), "0", "01/01/1900", "0", "0", "1003",
                                                                        "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                            if (resultint > 0)
                                                            {
                                                                //Penal chrg Applied Credit to GL 100
                                                                resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", ViewState["PlAccNo2"].ToString(),
                                                                            txtAccNo.Text.ToString(), txtNarration.Text.ToString(), "PENCR", Convert.ToDouble(txtPIntAmt.Text.Trim().ToString()), "1", "12", "TR_INT", SetNo1.ToString(), "0", "01/01/1900", "0", "0", "1003",
                                                                            "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());
                                                            }
                                                        }
                                                    }
                                                }

                                                if (resultint > 0)
                                                {
                                                    if (Convert.ToDouble(txtIntRecAmt.Text == "" ? "0" : txtIntRecAmt.Text) > 0)
                                                    {
                                                        // Interest Received Credited To GL 11
                                                        resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["InterestRecGl"].ToString(), ViewState["InterestRecSub"].ToString(),
                                                            txtAccNo.Text.ToString(), txtNarration.Text, "INTRCR", Convert.ToDouble(txtIntRecAmt.Text.Trim().ToString() == "" ? "0" : txtIntRecAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo1.ToString(), "0", "01/01/1900", "0", "0", "1003",
                                                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                        if (resultint > 0)
                                                        {
                                                            if (Convert.ToDouble(txtIntRecAmt.Text.Trim().ToString() == "" ? "0" : txtIntRecAmt.Text.Trim().ToString()) > 0)
                                                            {
                                                                // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                                resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["InterestRecSub"].ToString(), txtAccNo.Text.Trim().ToString(), "4", "1", Activity.ToString(), "Interest Credited", txtIntRecAmt.Text.Trim().ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                        }
                                                    }
                                                }

                                                if (resultint > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(txtNotChrgPrev.Text == "" ? "0" : txtNotChrgPrev.Text) + Convert.ToDouble(txtNotChrgCurr.Text == "" ? "0" : txtNotChrgCurr.Text)) > 0)
                                                    {
                                                        // Notice Charges
                                                        resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["NoticeChrgGl"].ToString(), ViewState["NoticeChrgSub"].ToString(),
                                                            txtAccNo.Text.ToString(), txtNarration.Text, "NOTCR", Convert.ToDouble(txtNotChrgAmt.Text.Trim().ToString() == "" ? "0" : txtNotChrgAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo1.ToString(), "0", "01/01/1900", "0", "0", "1003",
                                                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                        if (resultint > 0)
                                                        {
                                                            if (Convert.ToDouble(txtNotChrgCurr.Text.Trim().ToString() == "" ? "0" : txtNotChrgCurr.Text.Trim().ToString()) > 0)
                                                            {
                                                                // Notice Charges Debit To 5 In AVS_LnTrx
                                                                resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["NoticeChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "5", "2", Activity.ToString(), txtNarration.Text.ToString(), txtNotChrgCurr.Text.Trim().ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                        }

                                                        if (resultint > 0)
                                                        {
                                                            if (Convert.ToDouble(txtNotChrgAmt.Text.Trim().ToString() == "" ? "0" : txtNotChrgAmt.Text.Trim().ToString()) > 0)
                                                            {
                                                                // Notice Charges Credit To 5 In AVS_LnTrx
                                                                resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["NoticeChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "5", "1", Activity.ToString(), txtNarration.Text.ToString(), txtNotChrgAmt.Text.Trim().ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                        }
                                                    }
                                                }

                                                if (resultint > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(txtSerChrgPrev.Text == "" ? "0" : txtSerChrgPrev.Text) + Convert.ToDouble(txtSerChrgCurr.Text == "" ? "0" : txtSerChrgCurr.Text)) > 0)
                                                    {
                                                        // Service Charges
                                                        resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["ServiceChrgGl"].ToString(), ViewState["ServiceChrgSub"].ToString(),
                                                            txtAccNo.Text.ToString(), txtNarration.Text, "SERCR", Convert.ToDouble(txtSerChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSerChrgAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo1.ToString(), "0", "01/01/1900", "0", "0", "1003",
                                                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                        if (resultint > 0)
                                                        {
                                                            if (Convert.ToDouble(txtSerChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSerChrgCurr.Text.Trim().ToString()) > 0)
                                                            {
                                                                // Service Charges Amt Debit To 6 In AVS_LnTrx
                                                                resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["ServiceChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "6", "2", Activity.ToString(), txtNarration.Text.ToString(), txtSerChrgCurr.Text.Trim().ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                        }

                                                        if (resultint > 0)
                                                        {
                                                            if (Convert.ToDouble(txtSerChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSerChrgAmt.Text.Trim().ToString()) > 0)
                                                            {
                                                                // Service Charges Amt Credit To 6 In AVS_LnTrx
                                                                resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["ServiceChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "6", "1", Activity.ToString(), txtNarration.Text.ToString(), txtSerChrgAmt.Text.Trim().ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                        }
                                                    }
                                                }

                                                if (resultint > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(txtCrtChrgPrev.Text == "" ? "0" : txtCrtChrgPrev.Text) + Convert.ToDouble(txtCrtChrgCurr.Text == "" ? "0" : txtCrtChrgCurr.Text)) > 0)
                                                    {
                                                        // Court Charges
                                                        resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["CourtChrgGl"].ToString(), ViewState["CourtChrgSub"].ToString(),
                                                            txtAccNo.Text.ToString(), txtNarration.Text, "CRTCR", Convert.ToDouble(txtCrtChrgAmt.Text.Trim().ToString() == "" ? "0" : txtCrtChrgAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo1.ToString(), "0", "01/01/1900", "0", "0", "1003",
                                                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                        if (resultint > 0)
                                                        {
                                                            if (Convert.ToDouble(txtCrtChrgCurr.Text.Trim().ToString() == "" ? "0" : txtCrtChrgCurr.Text.Trim().ToString()) > 0)
                                                            {
                                                                // Court Charges Amt Debit To 7 In AVS_LnTrx
                                                                resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["CourtChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "7", "2", Activity.ToString(), txtNarration.Text.ToString(), txtCrtChrgCurr.Text.Trim().ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                        }

                                                        if (resultint > 0)
                                                        {
                                                            if (Convert.ToDouble(txtCrtChrgAmt.Text.Trim().ToString() == "" ? "0" : txtCrtChrgAmt.Text.Trim().ToString()) > 0)
                                                            {
                                                                // Court Charges Amt Credit To 7 In AVS_LnTrx
                                                                resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["CourtChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "7", "1", Activity.ToString(), txtNarration.Text.ToString(), txtCrtChrgAmt.Text.Trim().ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                        }
                                                    }
                                                }

                                                if (resultint > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(txtSurChrgPrev.Text == "" ? "0" : txtSurChrgPrev.Text) + Convert.ToDouble(txtSurChrgCurr.Text == "" ? "0" : txtSurChrgCurr.Text)) > 0)
                                                    {
                                                        // Sur Charges
                                                        resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["SurChrgGl"].ToString(), ViewState["SurChrgSub"].ToString(),
                                                            txtAccNo.Text.ToString(), txtNarration.Text, "SURCR", Convert.ToDouble(txtSurChrgAmt.Text.ToString().ToString() == "" ? "0" : txtSurChrgAmt.Text.ToString().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo1.ToString(), "0", "01/01/1900", "0", "0", "1003",
                                                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                        if (resultint > 0)
                                                        {
                                                            if (Convert.ToDouble(txtSurChrgCurr.Text.Trim().ToString() == "" ? "0" : txtSurChrgCurr.Text.Trim().ToString()) > 0)
                                                            {
                                                                // Sur Charges debit To 8 In AVS_LnTrx
                                                                resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["SurChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "8", "2", Activity.ToString(), txtNarration.Text.ToString(), txtSurChrgCurr.Text.Trim().ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                        }

                                                        if (resultint > 0)
                                                        {
                                                            if (Convert.ToDouble(txtSurChrgAmt.Text.Trim().ToString() == "" ? "0" : txtSurChrgAmt.Text.Trim().ToString()) > 0)
                                                            {
                                                                // Sur Charges Credit To 8 In AVS_LnTrx
                                                                resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["SurChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "8", "1", Activity.ToString(), txtNarration.Text.ToString(), txtSurChrgAmt.Text.Trim().ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                        }
                                                    }
                                                }

                                                if (resultint > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(txtOtherChrgPrev.Text == "" ? "0" : txtOtherChrgPrev.Text) + Convert.ToDouble(txtOtherChrgCurr.Text == "" ? "0" : txtOtherChrgCurr.Text)) > 0)
                                                    {
                                                        // Other Charges
                                                        resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["OtherChrgGl"].ToString(), ViewState["OtherChrgSub"].ToString(),
                                                            txtAccNo.Text.ToString(), txtNarration.Text, "OTHCR", Convert.ToDouble(txtOtherChrgAmt.Text.Trim().ToString() == "" ? "0" : txtOtherChrgAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo1.ToString(), "0", "01/01/1900", "0", "0", "1003",
                                                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                        if (resultint > 0)
                                                        {
                                                            if (Convert.ToDouble(txtOtherChrgCurr.Text.Trim().ToString() == "" ? "0" : txtOtherChrgCurr.Text.Trim().ToString()) > 0)
                                                            {
                                                                // Other Charges Amt Debit To 9 In AVS_LnTrx
                                                                resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["OtherChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "9", "2", Activity.ToString(), txtNarration.Text.ToString(), txtOtherChrgCurr.Text.Trim().ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                        }

                                                        if (resultint > 0)
                                                        {
                                                            if (Convert.ToDouble(txtOtherChrgAmt.Text.Trim().ToString() == "" ? "0" : txtOtherChrgAmt.Text.Trim().ToString()) > 0)
                                                            {
                                                                // Other Charges Amt Credit To 9 In AVS_LnTrx
                                                                resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["OtherChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "9", "1", Activity.ToString(), txtNarration.Text.ToString(), txtOtherChrgAmt.Text.Trim().ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                        }
                                                    }
                                                }

                                                if (resultint > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(txtBankChrgPrev.Text == "" ? "0" : txtBankChrgPrev.Text) + Convert.ToDouble(txtBankChrgCurr.Text == "" ? "0" : txtBankChrgCurr.Text)) > 0)
                                                    {
                                                        // Bank Charges
                                                        resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["BankChrgGl"].ToString(), ViewState["BankChrgSub"].ToString(),
                                                            txtAccNo.Text.ToString(), txtNarration.Text, "BNKCR", Convert.ToDouble(txtBankChrgAmt.Text.Trim().ToString() == "" ? "0" : txtBankChrgAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo1.ToString(), "0", "01/01/1900", "0", "0", "1003",
                                                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                        if (resultint > 0)
                                                        {
                                                            if (Convert.ToDouble(txtBankChrgCurr.Text.Trim().ToString() == "" ? "0" : txtBankChrgCurr.Text.Trim().ToString()) > 0)
                                                            {
                                                                // Bank Charges Amt Debit To 10 In AVS_LnTrx
                                                                resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["BankChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "10", "2", Activity.ToString(), txtNarration.Text.ToString(), txtBankChrgCurr.Text.Trim().ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                        }

                                                        if (resultint > 0)
                                                        {
                                                            if (Convert.ToDouble(txtBankChrgAmt.Text.Trim().ToString() == "" ? "0" : txtBankChrgAmt.Text.Trim().ToString()) > 0)
                                                            {
                                                                // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                                                resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["BankChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "10", "1", Activity.ToString(), txtNarration.Text.ToString(), txtBankChrgAmt.Text.Trim().ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                        }
                                                    }
                                                }

                                                if (resultint > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(txtInsurancePrev.Text == "" ? "0" : txtInsurancePrev.Text) + Convert.ToDouble(txtInsuranceCurr.Text == "" ? "0" : txtInsuranceCurr.Text)) > 0)
                                                    {
                                                        //  Insurance Charge
                                                        resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["InsChrgGl"].ToString(), ViewState["InsChrgSub"].ToString(),
                                                            txtAccNo.Text.ToString(), txtNarration.Text, "INSCR", Convert.ToDouble(txtInsuranceAmt.Text.Trim().ToString() == "" ? "0" : txtInsuranceAmt.Text.Trim().ToString()), "1", Activity.ToString(), PmtMode.ToString(), SetNo1.ToString(), "0", "01/01/1900", "0", "0", "1003",
                                                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanClose", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                        if (resultint > 0)
                                                        {
                                                            if (Convert.ToDouble(txtInsuranceCurr.Text.Trim().ToString() == "" ? "0" : txtInsuranceCurr.Text.Trim().ToString()) > 0)
                                                            {
                                                                //  Insurance Charge Debit To 11 In AVS_LnTrx
                                                                resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["InsChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "11", "2", Activity.ToString(), txtNarration.Text.ToString(), txtInsuranceCurr.Text.Trim().ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                        }

                                                        if (resultint > 0)
                                                        {
                                                            if (Convert.ToDouble(txtInsuranceAmt.Text.Trim().ToString() == "" ? "0" : txtInsuranceAmt.Text.Trim().ToString()) > 0)
                                                            {
                                                                //  Insurance Charge Credit To 11 In AVS_LnTrx
                                                                resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), ViewState["InsChrgSub"].ToString(), txtAccNo.Text.Trim().ToString(), "11", "1", Activity.ToString(), txtNarration.Text.ToString(), txtInsuranceAmt.Text.Trim().ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                            }
                                                        }
                                                    }
                                                }

                                                if ((resultint > 0) && (Convert.ToDouble(txtGSTAmt.Text.ToString() == "" ? "0" : txtGSTAmt.Text.ToString()) > 0))
                                                {
                                                    DT = LI.GstDetails(Session["BRCD"].ToString());
                                                    if (DT.Rows.Count > 0)
                                                    {
                                                        CGST = Math.Round(Convert.ToDouble(txtGSTAmt.Text.ToString()) / 2, 2);
                                                        SGST = Math.Round(Convert.ToDouble(txtGSTAmt.Text.ToString()) / 2, 2);

                                                        resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["SGSTGlCode"].ToString(), DT.Rows[0]["SGSTPrdCd"].ToString(),
                                                            txtAccNo.Text.ToString(), txtNarration.Text, "GST_State", CGST, "1", Activity.ToString(), PmtMode.ToString(), SetNo1, CN, CD, "0", "0", "1001",
                                                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LOANINST", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                                                        resultint = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["CGSTGlCode"].ToString(), DT.Rows[0]["CGSTPrdCd"].ToString(),
                                                                txtAccNo.Text.ToString(), txtNarration.Text, "GST_Central", SGST, "1", Activity.ToString(), PmtMode.ToString(), SetNo1, CN, CD, "0", "0", "1001",
                                                                        "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LOANINST", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (resultint > 0)
                                    {
                                        resultint = LI.CloseLoanAcc(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultint > 0)
                                        {
                                            CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Loan_Close _" + txtProdType.Text + "_" + txtAccNo.Text + "_" + txtDepositeAmt.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                            WebMsgBox.Show("Loan Account Close With SetNo : " + Convert.ToInt32(SetNo), this.Page);
                                            BindGrid1();
                                            ClearAll();
                                            txtProdType.Focus();
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    WebMsgBox.Show("Closing Balance MisMatch ...!!", this.Page);
                                    return;
                                }
                            }
                            else
                            {
                                WebMsgBox.Show("Closing Balance MisMatch ...!!", this.Page);
                                return;
                            }
                        }
                        else
                        {
                            CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Loan_Paid _" + txtProdType.Text + "_" + txtAccNo.Text + "_" + txtDepositeAmt.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            WebMsgBox.Show("Successfully Paid With SetNo : " + Convert.ToInt32(ViewState["SetNo"].ToString()), this.Page);
                            ViewState["SetNo"] = "";
                            ClearAll();
                            txtProdType.Focus();
                            return;
                        }
                    }
                    else
                    {
                        WebMsgBox.Show("Excess amount to penal interest ...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    WebMsgBox.Show("Excess amount to interest ...!!", this.Page);
                    return;
                }
            }
            else
            {
                WebMsgBox.Show("Select Payment Type First ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnVoucherView_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Btn_Loan_Sch_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Loan_Close_Schdul_Rpt _" + txtProdType.Text + "_" + txtAccNo.Text + "_" + txtDepositeAmt.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&SBGL=" + txtProdType.Text + "&ACCNO=" + txtAccNo.Text + "&FL=LS&rptname=Rpt_LoanSchedule_Parti.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnAccStat_Click(object sender, EventArgs e)
    {
        try
        {
            //string FromDate = CurrentCls.GetFinStartDate(Session["EntryDate"].ToString());
            string OpDate = LI.GetAccOpenDate(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());

            FL = "Insert";
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Loan_Close_Acc_Stat _" + txtProdType.Text + "_" + txtAccNo.Text + "_" + txtDepositeAmt.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?FDate=" + OpDate + "&TDate=" + Session["EntryDate"].ToString() + "&ProdCode=" + txtProdType.Text + "&AccNo=" + txtAccNo.Text + "&BRCD=" + Session["BRCD"].ToString() + "&rptname=RptLoanStatementDetails.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
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

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dt = new DataSet();
            //DataTable DT = new DataTable();
            //DataTable DT1 = new DataTable();
            //DataTable DTOD = new DataTable();
            int CurrentYear = DateTime.Today.Year;
            int PreviousYear = DateTime.Today.Year - 1;
            int NextYear = DateTime.Today.Year + 1;
            string PreYear = PreviousYear.ToString();
            //string NexYear = NextYear.ToString();
            string CurYear = CurrentYear.ToString();
            //string FinYear = null;

            //if (DateTime.Today.Month > 3)
            //    FinYear = CurYear;
            //else
            //    FinYear = PreYear;

            //string OpDate= "01/04/"+FinYear.Trim();
            string OpDate = LI.GetAccOpenDate(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());

            dt = GetLoanStatDetails(OpDate.ToString(), Session["EntryDate"].ToString(), txtProdType.Text, txtAccNo.Text, Session["BRCD"].ToString());
            if (dt.Tables[0].Rows.Count > 0)
            {

                GridRecords.DataSource = dt;
                GridRecords.DataBind();
                //txtCustId.Text=dt.Tables[0].Rows[0]["CustNO"].ToString(); 
                //txtBranchCode.Text=Session["BRCD"].ToString(); 
                //txtAccId.Text= dt.Tables[0].Rows[0]["ACCNO"].ToString();
                //txtAddress.Text = dt.Tables[0].Rows[0]["BR_Add1"].ToString();
                //txtAcName.Text = dt.Tables[0].Rows[0]["CustName"].ToString();
                //GLcode.Text = txtProdType.Text +"-"+ txtProdName.Text;
                //txtCity.Text = dt.Tables[0].Rows[0]["City"].ToString() + "-" + dt.Tables[0].Rows[0]["PINCODE"].ToString();
                //txtLD.Text = dt.Tables[0].Rows[0]["LASTINTDATE1"].ToString();
                //txtED.Text = dt.Tables[0].Rows[0]["EDATE1"].ToString();
                //txtCustAddress.Text = dt.Tables[0].Rows[0]["FLAT_ROOMNO"].ToString();
                //txtSD.Text = dt.Tables[0].Rows[0]["SANSSIONDATE"].ToString();
                //DT = LI.FetchNomineeDetails(dt.Tables[0].Rows[0]["CustNO"].ToString(), txtProdType.Text.ToString(), Session["BRCD"].ToString());
                //if (DT.Rows.Count > 0)
                //{
                //    //GridGurantor.DataSource = DT;
                //    //GridGurantor.DataBind();
                //    //txtNomiee.Text = DT.Rows[0]["Nominee"].ToString();
                //}
                //DT1 = LI.FetchLoanDetails(dt.Tables[0].Rows[0]["CustNO"].ToString(), dt.Tables[0].Rows[0]["ACCNO"].ToString(), Session["BRCD"].ToString(),txtProdType.Text);
                //if (DT1.Rows.Count > 0)
                //{
                //    txtIRate.Text = DT1.Rows[0]["INTRate"].ToString() + "%";
                //    txtlimit.Text = DT1.Rows[0]["LIMIT"].ToString();
                //}
                //DTOD = LI.FetchOpeningDate(dt.Tables[0].Rows[0]["CustNO"].ToString(), dt.Tables[0].Rows[0]["ACCNO"].ToString(), Session["BRCD"].ToString(), txtProdType.Text);
                //if (DTOD.Rows.Count > 0)
                //{

                //    txtPh.Text = DTOD.Rows[0]["Mobile"].ToString();
                //}
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('.bs-example-modal-lg').modal('show');</script>", false);
            }
            else
            {
                WebMsgBox.Show("Details Not Found For This Account Number ...!!", this.Page);
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
            Session["densamt"] = CurrentCls.GetTotalAmt(Session["BRCD"].ToString(), Session["densset"].ToString(), Session["EntryDate"].ToString());
            Session["denssubgl"] = dens[2].ToString();
            Session["densact"] = dens[3].ToString();

            string i = CurrentCls.CheckDenom(Session["BRCD"].ToString(), Session["densset"].ToString(), Session["EntryDate"].ToString());
            if (i == null)
            {
                string redirectURL = "FrmCashDenom.aspx";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else
            {
                WebMsgBox.Show("Already Cash Denominations ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnViewDt_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["GLCODE"].ToString() == "4")
                CurrentCls.GetAccDetails(grdAccDetails, "1", txtCustNo.Text.Trim().ToString(), Session["EntryDate"].ToString());
            else
                CurrentCls.GetAccDetails(grdAccDetails, Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), Session["EntryDate"].ToString());
            DivGrd1.Visible = true;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    #endregion

}