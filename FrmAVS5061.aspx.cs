using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
public partial class FrmAVS5061 : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    ClsAVS5061 DDS = new ClsAVS5061();
    ClsOpenClose OC = new ClsOpenClose();
    ClsAuthorized AT = new ClsAuthorized();
    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLoanInfo LI = new ClsLoanInfo();
    ClsLoanClosure LC = new ClsLoanClosure();
    ClsCommon CC = new ClsCommon();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsBindBrDetails ASM = new ClsBindBrDetails();
    ClsCashPayment CurrentCls = new ClsCashPayment();
    ClsCashReciept CR = new ClsCashReciept();
    int SetN = 0;
    string sResult = "", AC_Status = "", FL = "", Param = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            LnkVerify.Visible = false;
            TxtProcode.Focus();

            EnableDisableInt();

            Btn_Printsummary.Enabled = false;
            BindGrid1();
            GSTLogic();

            AutoAcc.ContextKey = Session["BRCD"].ToString() + "-" + TxtProcode.Text;
            autoAgname.ContextKey = Session["BRCD"].ToString();
            AutoPName.ContextKey = Session["BRCD"].ToString();
        }

    }
    
    public void EnableDisableInt()
    {
        try
        {
            string ParaV = CC.GetRDINT();
            if (ParaV != null && ParaV == "Y")
            {
                string UG = CC.GetRDUG();
                if (UG == "0")
                {
                    TxtCalcInt.Enabled = true;
                }
                else if (Session["UGRP"].ToString() == UG)
                {
                    TxtCalcInt.Enabled = true;
                }
                else
                {
                    TxtCalcInt.Enabled = false;
                }
            }
            else
            {
                TxtCalcInt.Enabled = false;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void GSTLogic()
    {
        string GSTYN = DDS.GetGSTPara();
        ViewState["GSTYN"] = string.IsNullOrEmpty(GSTYN) ? "0" : GSTYN;
        if (GSTYN == "N")
        {
            TxtGST.Enabled = false;
            TxtGST.Text = "0";
        }
        else if (GSTYN == "Y")
        {
            TxtGST.Enabled = true;
        }
        else
        {
            lblMessage.Text = "Add 'GSTYN' Parameter to 'Y' or 'N' for GST Calculation.";
            ModalPopup.Show(this.Page);
           
            TxtGST.Enabled = false;
            TxtProcode.Text = "";
            TxtProName.Text = "";

        }
    }
    public void BindCalculation()
    {
        try
        {
            int RR = DDS.BindCalcu(GridCalcu, Session["BRCD"].ToString(),ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["EntryDate"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    public void Calc_(string FL)
    {
        try
        {
            if (FL == "PR")
            {

                DataTable DT = new DataTable();
                string SC = "";
                DT = DDS.GetDDSInfo("4"); //RD Data
                if (DT.Rows.Count > 0)
                {
                    //Comm_Ded_Rt,Serv_Chg

                    TxtPreMC.Text = DT.Rows[0]["Comm_Ded_Rt"].ToString();
                    SC = DT.Rows[0]["Serv_Chg"].ToString();
                    double CLBAL, PMC, SR, PRMC, IR, SSR;
                    CLBAL = PMC = SR = PRMC = 0;
                    if (rdbpart.Checked == true)
                    {
                        CLBAL = Convert.ToDouble(Txtpartpay.Text);
                    }
                    else
                    {
                        CLBAL = Convert.ToDouble(TxtCBal.Text);
                    }
                    PMC = Convert.ToDouble(TxtPreMC.Text);

                    PRMC = CLBAL * PMC / 100;
                    TxtPreMCAMT.Text = Math.Round(PRMC, 0).ToString();
                    SR = Convert.ToDouble(SC);
                    SSR = CLBAL * SR / 100;
                    TxtServCHRS.Text = Math.Round(SSR, 0).ToString();
                    //TxtServCHRS.Text = "0";
                    // double AMT = CLBAL - PRMC + SSR;
                    double TT = Math.Round(Convert.ToDouble(TxtPreMCAMT.Text) + SSR);
                    double AMT = CLBAL - (TT);
                    double GST = 0;
                    if (ViewState["GSTYN"].ToString() == "Y")
                    {
                        GST = Convert.ToDouble(TxtPreMCAMT.Text) + Convert.ToDouble(TxtServCHRS.Text);
                        GST = Math.Round(GST * 18 / 100, 0);

                        TxtGST.Text = GST.ToString();

                        TxtPayAmt.Text = Math.Round(Convert.ToDouble(AMT) - Convert.ToDouble(GST), 0).ToString();
                    }
                    else
                    {
                        TxtPayAmt.Text = Math.Round(Convert.ToDouble(AMT)).ToString();
                        TxtGST.Text = "0";
                    }
                    TxtCalcInt.Text = "";
                    TxtPreMC.Text = TxtPreMC.Text + "%";
                    
                    double BAL = 0;
                    int MD = Convert.ToInt32(conn.GetMonthDiff(TxtOpeningDate.Text.Replace("12:00:00", ""), Session["EntryDate"].ToString()));
                    
                    string[] TD = Session["EntryDate"].ToString().Split('/');
                    BAL = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), ViewState["IR"].ToString(), TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "10");
                   
                    Txt_Provi.Text = BAL.ToString();



                }

            }
            else if (FL == "MR")
            {
                GridCalcu.Visible = true;
                TxtPreMC.Text = "0";
                TxtServCHRS.Text = "0";
                TxtPreMCAMT.Text = "0";
                double CLBAL = 0;
                if (rdbpart.Checked == true)
                {
                    CLBAL = Convert.ToDouble(Txtpartpay.Text);
                }
                else
                {
                    CLBAL = Convert.ToDouble(TxtCBal.Text);
                }
                double BAL = 0;
                int MD = Convert.ToInt32(conn.GetMonthDiff(TxtOpeningDate.Text.Replace("12:00:00", ""), Session["EntryDate"].ToString()));
               
                string[] TD = Session["EntryDate"].ToString().Split('/');
                BAL = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), ViewState["IR"].ToString(), TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "10");
                
                Txt_Provi.Text = BAL.ToString();

                double IAMT = DDS.GetInterest(Session["BRCD"].ToString(), ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["EntryDate"].ToString());
               
                TxtCalcInt.Text = Math.Round(IAMT, 0).ToString();
              
                Txt_Provi.Focus();
                TxtINTC.Text = (Convert.ToDouble(TxtCalcInt.Text) - Convert.ToDouble(Txt_Provi.Text)).ToString();
                TxtPayAmt.Text = Math.Round((CLBAL + Convert.ToDouble(TxtCalcInt.Text)), 0).ToString();
                if (TxtCalcInt.Text == "0")
                {
                    WebMsgBox.Show("Interest not calculated, Check Interest Rate or Check Effect Date!", this.Page);
                }
                BindCalculation();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void ddlActivity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            lblpayable.Text = ddlActivity.SelectedItem.Text + " Amount";
            if (ddlActivity.SelectedValue == "2")
            {
                Calc_("PR");
                Txt_Provi.Focus();


            }
            else if (ddlActivity.SelectedValue == "3")
            {
                Btn_Printsummary.Enabled = true;
                Calc_("MR");
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPayMode.SelectedValue == "1")
            {
                DivTransfer.Visible = false;
            }
            else if (ddlPayMode.SelectedValue == "2")
            {
                DivTransfer.Visible = true;
                DIV_INSTRUMENT.Visible = false;
            }
            else
            {
                DivTransfer.Visible = true;
                DIV_INSTRUMENT.Visible = true;
            }
            // TxtOutSTD.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtProcode_TextChanged(object sender, EventArgs e)
    {
        try
        {

            string NM = "";

            //Added By Amol on 22092017 as per ambika mam instruction
            if (BD.GetProdOperate(Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString()).ToString() != "3")
            {
                NM = TxtProcode.Text;
                string TDN = DDS.GetAgentName_IR(TxtProcode.Text, Session["BRCD"].ToString());
                if (TDN != "0")
                {
                    string[] TD = TDN.Split('_');
                    ClearData();

                    TxtProcode.Text = NM;
                    TxtProName.Text = TD[0].ToString();
                    ViewState["GL"] = TD[1].ToString();
                    ViewState["IR"] = TD[2].ToString();
                    ViewState["PLACC"] = TD[3].ToString();

                    if (ViewState["GL"].ToString() == "1")
                    {
                        lblpreCommission.Text = "Un-Used Cheque :";
                    }
                    else if (ViewState["GL"].ToString() == "2")
                    {
                        lblpreCommission.Text = "Premature Commission :";
                    }
                    AutoAcc.ContextKey = Session["BRCD"].ToString() + "-" + TxtProcode.Text;
                    TxtAccNo.Focus();
                }
                else
                {
                    WebMsgBox.Show("Enter a valid Agent Code!....", this.Page);
                    TxtProcode.Text = "";
                    TxtProcode.Focus();

                }
                GSTLogic();
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
    public string PanCard(string BRCD, string CustNo)
    {
        string PanNo = "";
        try
        {
            PanNo = CR.GetPanNo(BRCD, CustNo);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return PanNo;
    }
    protected void TxtAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {

            string AT = "";
            AC_Status = CC.GetAccStatus(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text);
            if (AC_Status == "1")
            {


                string[] TD = Session["EntryDate"].ToString().Split('/');
                GetAccInfo();
                double BAL = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString());
                TxtCBal.Text = BAL.ToString();
                if (BAL <= 0)
                {
                    ClearData();
                    TxtProcode.Focus();
                    lblMessage.Text = "Balance Insufficient............!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
                DataTable DT = new DataTable();
                DT = DDS.GetOpeningDate(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["EntryDate"].ToString());
                if (DT.Rows.Count > 0)
                {
                    TxtOpeningDate.Text = DT.Rows[0]["OPENINGDATE"].ToString();
                    TxtLastIntDate.Text = DT.Rows[0]["LASTINTDATE"].ToString();
                    TxtPeriod.Text = DT.Rows[0]["PERIOD"].ToString();
                    TxtCLDate.Text = DT.Rows[0]["CLOSINGDATE"].ToString();

                    txtPan.Text = PanCard(Session["BRCD"].ToString(), ViewState["CT"].ToString());

                    if (Convert.ToDateTime(conn.ConvertDate(TxtCLDate.Text)) > Convert.ToDateTime(conn.ConvertDate(Session["EntryDate"].ToString())))
                    {
                        ddlActivity.SelectedValue = "2";
                    }
                    else
                    {
                        ddlActivity.SelectedValue = "3";
                    }
                }

                BindGrid();
                int RC = LC.BindGrid(GridDeposite, Session["BRCD"].ToString(), ViewState["GL"].ToString(), TxtPType.Text, TxtAccNo.Text, 1);
                Photo_Sign();

                string Modal_Flag = "VOUCHERVIEW";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#" + Modal_Flag + "').modal('show');");
                sb.Append(@"</script>");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);

                LnkVerify.Visible = true;
            }
            else if (AC_Status == "2")
            {
                lblMessage.Text = "Acc number " + TxtAccNo.Text + " is In-operative.........!!";
                ModalPopup.Show(this.Page);
                ClearData();
            }
            else if (AC_Status == "3")
            {
                lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Closed.........!!";
                ModalPopup.Show(this.Page);
                ClearData();
            }
            else if (AC_Status == "4")
            {
                ////Added by ankita on 19/06/2017 to display Lean details
                //  dt = DDS.getDetails(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text); // Commented by abhishek for Accno passing null while lein details 31-10-2017

                dt = DDS.getDetails(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text);

                lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Lean Marked / Loan Advanced .........!! with LoanGlCode=" + dt.Rows[0]["LOANGLCODE"].ToString() + " and LoanAccNo=" + dt.Rows[0]["LOANACCNO"].ToString() + " and Loan Amount=" + dt.Rows[0]["LOANAMT"].ToString() + "";
                ModalPopup.Show(this.Page);
                ClearData();
            }
            else if (AC_Status == "5")
            {
                lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Credit Freezed.........!!";
                ModalPopup.Show(this.Page);
                ClearData();
            }
            else if (AC_Status == "6")
            {
                lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Debit Freezed.........!!";
                ModalPopup.Show(this.Page);
                ClearData();
            }
            else if (AC_Status == "7")
            {
                lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Total Freezed.........!!";
                ModalPopup.Show(this.Page);
                ClearData();
            }
            else
            {
                WebMsgBox.Show("Enter Valid Account number!...", this.Page);
                TxtAccNo.Text = "";
                TxtAccNo.Focus();
            }

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
            DataTable DT = new DataTable();
            DT = DDS.GetAccInfo(TxtProcode.Text, Session["BRCD"].ToString(), TxtAccNo.Text);
            if (DT.Rows.Count > 0)
            {
                TxtAccName.Text = DT.Rows[0]["custname"].ToString();
                TxtAccType.Text = DT.Rows[0]["ACC_TYPE"].ToString();
                TxtAccTName.Text = DT.Rows[0]["NAME"].ToString();
                TxtAccSTS.Text = DT.Rows[0]["Acc_Status"].ToString();
                TxtAccSTSName.Text = DT.Rows[0]["Accsts"].ToString();
                ViewState["CT"] = DT.Rows[0]["CUSTNO"].ToString();
                Txtcustno.Text = ViewState["CT"].ToString();
                TxtDailyAmt.Text = DT.Rows[0]["D_AMOUNT"].ToString().Replace(".00000", "");
                //TxtSpDDS.Focus();
                rdbfull.Focus();

            }
            else
            {
                lblMessage.Text = "Sorry Account not Present.....!!";
                ModalPopup.Show(this.Page);
                TxtAccNo.Text = "";
                TxtAccName.Text = "";
                TxtAccNo.Focus();
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

            string CUNAME = TxtAccName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                AC_Status = CC.GetAccStatus(Session["BRCD"].ToString(), TxtProcode.Text, custnob[1].ToString());
                if (AC_Status == "1")
                {
                    TxtAccName.Text = custnob[0].ToString();
                    TxtAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                    string[] TD = Session["EntryDate"].ToString().Split('/');
                    GetAccInfo();
                    double BAL = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "2");
                    TxtCBal.Text = BAL.ToString();
                    DataTable DT = new DataTable();
                    DT = OC.GetOpeningDate(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["EntryDate"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        TxtOpeningDate.Text = DT.Rows[0]["OPENINGDATE"].ToString().Replace("12:00:00", "");
                        TxtPeriod.Text = DT.Rows[0]["PERIOD"].ToString();
                        TxtCLDate.Text = DT.Rows[0]["CLOSINGDATE"].ToString().Replace("12:00:00", "");
                        txtPan.Text = PanCard(Session["BRCD"].ToString(), ViewState["CT"].ToString());
                        if (Convert.ToDateTime(conn.ConvertDate(TxtCLDate.Text)) > Convert.ToDateTime(conn.ConvertDate(Session["EntryDate"].ToString())))
                        {
                            ddlActivity.SelectedValue = "2";
                        }
                        else
                        {
                            ddlActivity.SelectedValue = "3";
                        }
                    }

                    BindGrid();
                    txtPan.Text = PanCard(Session["BRCD"].ToString(), custnob[1].ToString());
                    int RC = LC.BindGrid(GridDeposite, Session["BRCD"].ToString(), ViewState["GL"].ToString(), TxtPType.Text, TxtAccNo.Text, 1);
                    string Modal_Flag = "VOUCHERVIEW";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$('#" + Modal_Flag + "').modal('show');");
                    sb.Append(@"</script>");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);

                    LnkVerify.Visible = true;
                }
                else if (AC_Status == "2")
                {
                    lblMessage.Text = "Acc number " + TxtAccNo.Text + " is In-operative.........!!";
                    ModalPopup.Show(this.Page);
                    ClearData();
                }
                else if (AC_Status == "3")
                {
                    lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Closed.........!!";
                    ModalPopup.Show(this.Page);
                    ClearData();
                }
                else if (AC_Status == "4")
                {   ////Added by ankita on 19/06/2017 to display Lean details
                    //  dt = DDS.getDetails(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text); // Commented by abhishek for Accno passing null while lein details 31-10-2017

                    dt = DDS.getDetails(Session["BRCD"].ToString(), TxtProcode.Text, custnob[1].ToString());
                    lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Lean Marked / Loan Advanced .........!! with LoanGlCode=" + dt.Rows[0]["LOANGLCODE"].ToString() + " and LoanAccNo=" + dt.Rows[0]["LOANACCNO"].ToString() + " and Loan Amount=" + dt.Rows[0]["LOANAMT"].ToString() + "";
                    ModalPopup.Show(this.Page);
                    ClearData();
                }
                else if (AC_Status == "5")
                {
                    lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Credit Freezed.........!!";
                    ModalPopup.Show(this.Page);
                    ClearData();
                }
                else if (AC_Status == "6")
                {
                    lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Debit Freezed.........!!";
                    ModalPopup.Show(this.Page);
                    ClearData();
                }
                else if (AC_Status == "7")
                {
                    lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Total Freezed.........!!";
                    ModalPopup.Show(this.Page);
                    ClearData();
                }
                else
                {
                    WebMsgBox.Show("Enter Valid Account number!...", this.Page);
                    TxtAccNo.Text = "";
                    TxtAccNo.Focus();
                }
            }
            GetAccInfo();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtPType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;
            AC1 = DDS.Getaccno(Session["BRCD"].ToString(), TxtPType.Text);

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                ViewState["PGL"] = AC[0].ToString();
                TxtPTName.Text = AC[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtPType.Text + "_" + ViewState["PGL"].ToString();

                if (Convert.ToInt32(ViewState["PGL"].ToString() == "" ? "0" : ViewState["PGL"].ToString()) >= 100)
                {
                    TxtTAccNo.Text = "";
                    TxtTAName.Text = "";

                    TxtTAccNo.Text = TxtPType.Text.ToString();
                    TxtTAName.Text = TxtPTName.Text.ToString();

                    TxtTrfNarration.Focus();
                }
                else
                {
                    TxtTAccNo.Text = "";
                    TxtTAName.Text = "";

                    TxtTAccNo.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                TxtPType.Text = "";
                TxtPTName.Text = "";
                TxtTAccNo.Text = "";
                TxtTAName.Text = "";
                TxtPType.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtPTName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtPTName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtPTName.Text = custnob[0].ToString();
                TxtPType.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                string[] AC = DDS.Getaccno(Session["BRCD"].ToString(), TxtPType.Text).Split('_');
                ViewState["PGL"] = AC[0].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtPType.Text;

                if (Convert.ToInt32(ViewState["PGL"].ToString() == "" ? "0" : ViewState["PGL"].ToString()) > 100)
                {
                    TxtTAccNo.Text = "";
                    TxtTAName.Text = "";

                    TxtTAccNo.Text = TxtPType.Text.ToString();
                    TxtTAName.Text = TxtPTName.Text.ToString();

                    TxtInstNo.Focus();
                }
                else
                {
                    TxtTAccNo.Text = "";
                    TxtTAName.Text = "";

                    TxtTAccNo.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtTAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";
            AC_Status = CC.GetAccStatus(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text);
            if (AC_Status == "1")
            {
                AT = BD.Getstage1(TxtTAccNo.Text, Session["BRCD"].ToString(), TxtPType.Text);
                if (AT != null)
                {
                    if (AT != "1003")
                    {
                        lblMessage.Text = "Sorry Customer not Authorise.........!!";
                        ModalPopup.Show(this.Page);
                        TxtTAccNo.Text = "";
                        TxtTAName.Text = "";
                        TxtTAccNo.Focus();
                    }
                    else
                    {
                        DataTable DT = new DataTable();
                        DT = DDS.GetCustName(TxtPType.Text, TxtTAccNo.Text, Session["BRCD"].ToString());
                        if (DT.Rows.Count > 0)
                        {
                            string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                            TxtTAName.Text = CustName[0].ToString();

                            if (ddlPayMode.SelectedValue != "1")
                            {
                                ViewState["TCT"] = CustName[1].ToString(); ;
                            }

                            TxtTrfNarration.Focus();
                        }
                    }
                }
                else
                {
                    lblMessage.Text = "Enter valid account number...!!";
                    ModalPopup.Show(this.Page);
                    TxtTAccNo.Text = "";
                    TxtTAccNo.Focus();
                }
            }
            else if (AC_Status == "2")
            {
                lblMessage.Text = "Acc number " + TxtAccNo.Text + " is In-operative.........!!";
                ModalPopup.Show(this.Page);
                ClearData();
            }
            else if (AC_Status == "3")
            {
                lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Closed.........!!";
                ModalPopup.Show(this.Page);
                ClearData();
            }
            else if (AC_Status == "4")
            {
                lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Lean Marked / Loan Advanced .........!!";
                ModalPopup.Show(this.Page);
                ClearData();
            }
            else if (AC_Status == "5")
            {
                lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Credit Freezed.........!!";
                ModalPopup.Show(this.Page);
                ClearData();
            }
            else if (AC_Status == "6")
            {
                lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Debit Freezed.........!!";
                ModalPopup.Show(this.Page);
                ClearData();
            }
            else if (AC_Status == "7")
            {
                lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Total Freezed.........!!";
                ModalPopup.Show(this.Page);
                ClearData();
            }
            else
            {
                WebMsgBox.Show("Enter Valid Account number!...", this.Page);
                TxtAccNo.Text = "";
                TxtAccNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtTAName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] TD = TxtTAName.Text.Split('_');
            if (TD.Length > 1)
            {
                TxtTAName.Text = TD[0].ToString();
                TxtTAccNo.Text = TD[1].ToString();
                ViewState["TCT"] = TD[2].ToString();

                if (Convert.ToInt32(ViewState["TCT"].ToString() == "" ? "0" : ViewState["TCT"].ToString()) < 0)
                {
                    TxtTAccNo.Text = "";
                    TxtTAName.Text = "";
                    TxtAccNo.Focus();
                    WebMsgBox.Show("Please Enter valid Account Number Customer Not Exist...!!", this.Page);
                    return;
                }
                if (ddlPayMode.SelectedValue == "3")
                {
                    TxtInstNo.Focus();
                }
                else
                {
                    TxtTrfNarration.Focus();
                }

            }
            else
            {
                lblMessage.Text = "Enter Valid Account Number...!!";
                ModalPopup.Show(this.Page);
                TxtAccNo.Text = "";
                TxtTAName.Text = "";
                TxtAccNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnCashPost_Click(object sender, EventArgs e)
    {
        try
        {
            string Para_Deduct = "";
            double BAL1 = 0;
            Para_Deduct = DDS.GetDDSCashDeduct();
            if (Para_Deduct == null)
            {
                lblMessage.Text = "Add 'COMMDEDCASH' Parameter to 'Y' or 'N' for Post Premature Entries.";
                ModalPopup.Show(this.Page);
                //WebMsgBox.Show("Add 'COMMDEDCASH' Parameter to 'Y' or 'N' for Post Premature Entries.", this.Page);
                return;
            }

            GridCalcu.Visible = false;
            if (ddlPayMode.SelectedValue == "1")
            {
                int result;
                double AMT = 0;
                string ST = DDS.GetSetNo("46");
                SetN = Convert.ToInt32(ST);
                ViewState["ST"] = ST.ToString();
                double INTAMT = 0;

                DataTable DT = new DataTable();
                DT = DDS.GetDDSInfo("4");
                if (DT.Rows.Count > 0)
                {

                    if (ddlActivity.SelectedValue == "3") // **************    FOR MATURE CLOSURE  ************* CASH IN HAND
                    {
                        INTAMT = Convert.ToDouble(TxtINTC.Text);// -Convert.ToDouble(Txt_Provi.Text);
                        result = DDS.InsertBatch(ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), TxtCalcInt.Text.ToString(), "1", "10", "TR-INT", "INT TRF FROM " + ViewState["IR"].ToString() + "-" + TxtAccNo.Text + "", "MATURE ACC CLOSURE", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);

                        if (Convert.ToInt32(Txt_Provi.Text) > 0)
                        {
                            result = DDS.InsertBatch("10", ViewState["IR"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Txt_Provi.Text, "2", "10", "TR-INT", "INT TRF TO " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                        }


                        if (INTAMT > 0) //if AMT is Positive
                        {
                            result = DDS.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), INTAMT.ToString(), "2", "10", "TR-INT", "INT Dr TO " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                        }
                        else //if AMT is negative
                        {
                            INTAMT = Math.Abs(INTAMT);
                            result = DDS.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), INTAMT.ToString(), "1", "10", "TR-INT", "INT Cr TO " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                        }



                        if (result > 0)
                        {
                            AMT = Convert.ToDouble(TxtINTC.Text);
                            string[] TD = Session["EntryDate"].ToString().Split('/');
                            double BAL = Convert.ToDouble(TxtCBal.Text);
                            BAL = Convert.ToDouble(TxtPayAmt.Text); //--abhishek after calculation 09-03-2017

                            BAL1 = (BAL);
                            string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());

                            result = DDS.InsertBatch("99", cgl, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), BAL1.ToString(), "1", "4", "CP", "Dr TO " + TxtProcode.Text + "-" + TxtAccNo.Text + "", "MATURE ACC CLOSURE", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                            if (result > 0)
                            {
                                result = DDS.InsertBatch(ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), BAL1.ToString(), "2", "4", "CP", "Cr TO 99 - " + cgl.ToString() + "", "MATURE ACC CLOSURE", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);

                                if (result > 0)
                                {

                                    GrdEntryDate.Visible = true;
                                    BindGrid();
                                    BindGrid1();
                                    //ClearData();
                                    lblMessage.Text = "Record Added Successfully...........!!";
                                    ModalPopup.Show(this.Page);
                                    FL = "Insert";//Dhanya Shetty
                                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSCloser_Mature_" + TxtProcode + "_" + TxtAccNo + "_" + TxtPayAmt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                }
                            }
                        }
                    }

                }


                if (ddlActivity.SelectedValue == "2") // **** For PRemature Closure **** and CASH in HAND
                {
                    string ACTI = "", PMTMODE = "", Dr_GLCOD = "", Dr_SUBGLCODE = "", ACTIDr = "", PMTMODEDr = "";
                    Para_Deduct = DDS.GetDDSCashDeduct();

                    if (Para_Deduct == "Y") // On For Shivjyoti As per demand.
                    {
                        ACTI = "3";
                        PMTMODE = "CR";

                        string Dsbgl1 = BD.GetCashGl("99", Session["BRCD"].ToString());
                        Dr_GLCOD = "99";
                        Dr_SUBGLCODE = Dsbgl1;
                    }
                    else                // For Others
                    {
                        ACTI = "7";
                        PMTMODE = "TR";
                        string Dsbgl1 = TxtProcode.Text;
                        Dr_GLCOD = "2";
                        Dr_SUBGLCODE = Dsbgl1;

                    }
                    if (TxtINTC.Text != "" && TxtINTC.Text != "0")
                    {
                        INTAMT = Convert.ToDouble(TxtINTC.Text);// -Convert.ToDouble(Txt_Provi.Text);
                        result = DDS.InsertBatch(ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), TxtCalcInt.Text.ToString(), "1", "10", "TR-INT", "INT TRF FROM " + ViewState["IR"].ToString() + "-" + TxtAccNo.Text + "", "MATURE ACC CLOSURE", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);

                        if (Convert.ToInt32(Txt_Provi.Text) > 0)
                        {
                            result = DDS.InsertBatch("10", ViewState["IR"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Txt_Provi.Text, "2", "10", "TR-INT", "INT TRF TO " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                        }


                        if (INTAMT > 0) //if AMT is Positive
                        {
                            result = DDS.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), INTAMT.ToString(), "2", "10", "TR-INT", "INT Dr TO " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                        }
                        else //if AMT is negative
                        {
                            INTAMT = Math.Abs(INTAMT);
                            result = DDS.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), INTAMT.ToString(), "1", "10", "TR-INT", "INT Cr TO " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                        }
                    }

                    //Do not Change PARTICULARS it effects the Cancel Entry****************************************************************
                    if (Convert.ToInt32(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) > 0)
                    {
                        result = DDS.InsertBatch("100", DT.Rows[0]["serv_acc"].ToString(), TxtAccNo.Text, "", ViewState["CT"].ToString(), TxtServCHRS.Text, "1", ACTI, PMTMODE, "Dr FROM " + TxtProcode.Text + "-" + TxtAccNo.Text + "", "SERVICE CHG", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                    }
                    if (Convert.ToInt32(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) > 0)
                    {
                        result = DDS.InsertBatch("100", DT.Rows[0]["comm_Accno"].ToString(), TxtAccNo.Text, "", ViewState["CT"].ToString(), TxtPreMCAMT.Text, "1", ACTI, PMTMODE, "Dr FROM " + TxtProcode.Text + "-" + TxtAccNo.Text + "", "COMMISION CR", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                    }
                    if (Convert.ToInt32(Txt_Provi.Text == "" ? "0" : Txt_Provi.Text) > 0)
                    {
                        result = DDS.InsertBatch("10", ViewState["IR"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Txt_Provi.Text.ToString(), "2", "10", "TR-INT", "INT Dr TO " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                        if (result > 0)
                        {
                            result = DDS.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Txt_Provi.Text.ToString(), "1", "10", "TR-INT", "INT Cr TO " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                        }
                    }
                    if (Convert.ToInt32(TxtGST.Text == "" ? "0" : TxtGST.Text) > 0)
                    {
                        string GLCODE = BD.GetAccTypeGL(DT.Rows[0]["GST_ACC"].ToString(), Session["BRCD"].ToString());
                        string[] GLC = GLCODE.Split('_');
                        result = DDS.InsertBatch(GLC[1].ToString(), DT.Rows[0]["GST_ACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), TxtGST.Text.ToString(), "1", "7", "TR", "GST Dr From " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);

                    }
                    AMT = Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) + Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text);
                    result = DDS.InsertBatch(Dr_GLCOD, Dr_SUBGLCODE, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), AMT.ToString(), "2", ACTI, PMTMODE, "Dr FROM " + TxtProcode.Text + "-" + TxtAccNo.Text + "", "COMMISION DEDUCT", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);

                    string[] TD = Session["EntryDate"].ToString().Split('/');


                    double BAL = Convert.ToDouble(TxtCBal.Text);
                    if (rdbpart.Checked == true)
                    {
                        BAL = Convert.ToDouble(Txtpartpay.Text);
                        //BAL = Convert.ToDouble(TxtPayAmt.Text); //--abhishek after calculation 09-03-2017
                    }

                    
                    Para_Deduct = DDS.GetDDSCashDeduct();
                    if (Para_Deduct == "Y")
                    {
                        BAL1 = BAL;

                    }
                    else
                    {
                        BAL1 = BAL - AMT;
                    }
                    BAL1 = Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text) + Math.Abs(BAL1);

                    string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());
                    result = DDS.InsertBatch("99", cgl, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), BAL1.ToString(), "1", "4", "CP", "Dr FROM " + TxtProcode.Text + "-" + TxtAccNo.Text + "", "PREMATURE ACC CLOSURE", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                    if (result > 0)
                    {
                        result = DDS.InsertBatch(ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), BAL1.ToString(), "2", "4", "CP", "Cr TO 99 " + cgl.ToString() + "", "PREMATURE ACC CLOSURE", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                        if (result > 0)
                        {
                            //int RC = DDS.UpdateSetNo("46", ST);
                            GrdEntryDate.Visible = true;
                            BindGrid();
                            BindGrid1();
                            //ClearData();
                            lblMessage.Text = "Record Added Successfully...........!!";
                            ModalPopup.Show(this.Page);
                            FL = "Insert";//Dhanya Shetty
                            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSCloser_Premature_" + TxtProcode + "_" + TxtAccNo + "_" + TxtPayAmt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        }
                    }

                    // }
                }

            }

            //Do not Change PARTICULARS it Affects the Cancel Entry****************************************************************


            else if (ddlPayMode.SelectedValue == "2") // ***** TRANSFER Entry ******
            {
                double PLAMT = 0;
                int result;
                double AMT = 0;
                string ST = DDS.GetSetNo("46");
                ViewState["ST"] = ST.ToString();

                DataTable DT = new DataTable();
                DT = DDS.GetDDSInfo("4");

                if (ddlActivity.SelectedValue == "3")  // **************    FOR MATURE CLOSURE  ************* TRF
                {

                    result = DDS.InsertBatch(ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), TxtCalcInt.Text.ToString(), "1", "10", "TR-INT", "INT TRF FROM " + ViewState["IR"].ToString() + "-" + TxtAccNo.Text + "", "MATURE ACC CLOSURE " + TxtTrfNarration.Text + "", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);

                    if (Txt_Provi != null)
                        PLAMT = Convert.ToDouble(TxtINTC.Text);// -Convert.ToDouble(Txt_Provi.Text);

                    if (PLAMT > 0) //if AMT is Positive
                    {
                        result = DDS.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), PLAMT.ToString(), "2", "10", "TR-INT", "INT Dr TO " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                    }
                    else //if AMT is negative
                    {
                        PLAMT = Math.Abs(PLAMT);
                        result = DDS.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), PLAMT.ToString(), "1", "10", "TR-INT", "INT Cr TO " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                    }


                    if (Convert.ToInt32(Txt_Provi.Text == "" ? "0" : Txt_Provi.Text) > 0)
                    {
                        result = DDS.InsertBatch("10", ViewState["IR"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Txt_Provi.Text.ToString(), "2", "10", "TR-INT", "PROVISION Dr to " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                    }
                    if (result > 0)
                    {
                        AMT = Convert.ToDouble(TxtCalcInt.Text);
                        string[] TD = Session["EntryDate"].ToString().Split('/');
                        double BAL = Convert.ToDouble(TxtCBal.Text); //OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "2");
                        //if (rdbpart.Checked == true)
                        //{
                        //BAL = Convert.ToDouble(Txtpartpay.Text); //DDS SETNO NUMBER NOT GENERATED RANDOMLY SOLUT
                        BAL = Convert.ToDouble(TxtPayAmt.Text); //--abhishek after calculation 09-03-2017
                        //}
                        BAL1 = (BAL);
                      
                        ViewState["TCT"] = ViewState["TCT"] == null ? "0" : ViewState["TCT"].ToString();
                        //result = DDS.InsertBatch("99", "99", "0", TxtAccName.Text, ViewState["CT"].ToString(), BAL.ToString(), "1", "3", "CASH", ddlActivity.SelectedItem.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST);
                        result = DDS.InsertBatch(ViewState["PGL"].ToString(), TxtPType.Text, TxtTAccNo.Text, TxtTAName.Text, ViewState["TCT"].ToString(), BAL1.ToString(), "1", "7", "TR", "TRF Dr FROM AC CLOSURE " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                        if (result > 0)
                        {
                            result = DDS.InsertBatch(ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), BAL1.ToString(), "2", "7", "TR", "TRF Cr TO " + TxtPType.Text + "-" + TxtTAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                            if (result > 0)
                            {//The Set Number Suddenly Rises its count by n number as solution --Abhishek
                                //  int RC = DDS.UpdateSetNo("46", ST);
                                GrdEntryDate.Visible = true;
                                BindGrid();
                                //ClearData();
                                lblMessage.Text = "Record Added Successfully...........!!";
                                ModalPopup.Show(this.Page);
                                FL = "Insert";//Dhanya Shetty
                                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSCloser_matureTRF_" + TxtProcode + "_" + TxtAccNo + "_" + TxtPayAmt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            }
                        }
                    }
                }

                //Do not Change PARTICULARS it Affects the Cancel Entry****************************************************************


                if (ddlActivity.SelectedValue == "2")  // **** For PRemature Closure **** TRF
                {

                    if (TxtINTC.Text != "" && TxtINTC.Text != "0")
                    {
                        result = DDS.InsertBatch(ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), TxtCalcInt.Text.ToString(), "1", "10", "TR-INT", "INT TRF FROM " + ViewState["IR"].ToString() + "-" + TxtAccNo.Text + "", "MATURE ACC CLOSURE " + TxtTrfNarration.Text + "", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);

                        if (Txt_Provi != null)
                            PLAMT = Convert.ToDouble(TxtINTC.Text);// -Convert.ToDouble(Txt_Provi.Text);

                        if (PLAMT > 0) //if AMT is Positive
                        {
                            result = DDS.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), PLAMT.ToString(), "2", "10", "TR-INT", "INT Dr TO " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                        }
                        else //if AMT is negative
                        {
                            PLAMT = Math.Abs(PLAMT);
                            result = DDS.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), PLAMT.ToString(), "1", "10", "TR-INT", "INT Cr TO " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                        }


                        if (Convert.ToInt32(Txt_Provi.Text == "" ? "0" : Txt_Provi.Text) > 0)
                        {
                            result = DDS.InsertBatch("10", ViewState["IR"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Txt_Provi.Text.ToString(), "2", "10", "TR-INT", "PROVISION Dr to " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                        }
                    }

                    if (Convert.ToInt32(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) > 0)
                    {
                        result = DDS.InsertBatch("100", DT.Rows[0]["serv_acc"].ToString(), TxtAccNo.Text, "", ViewState["CT"].ToString(), TxtServCHRS.Text, "1", "7", "TR", "SERVICE CHG " + TxtProcode.Text + "-" + TxtAccNo.Text + " ", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                    }
                    if (Convert.ToInt32(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) > 0)
                    {
                        result = DDS.InsertBatch("100", DT.Rows[0]["comm_Accno"].ToString(), TxtAccNo.Text, "", ViewState["CT"].ToString(), TxtPreMCAMT.Text, "1", "7", "TR", "COMMISSION CHG " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                    }
                    if (Convert.ToInt32(TxtGST.Text == "" ? "0" : TxtGST.Text) > 0)
                    {
                        string GLCODE = BD.GetAccTypeGL(DT.Rows[0]["GST_ACC"].ToString(), Session["BRCD"].ToString());
                        string[] GLC = GLCODE.Split('_');
                        result = DDS.InsertBatch(GLC[1].ToString(), DT.Rows[0]["GST_ACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), TxtGST.Text.ToString(), "1", "7", "TR", "GST Dr From " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);

                    }
                    AMT = Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) + Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text);


                    string[] TD = Session["EntryDate"].ToString().Split('/');
                    double BAL = Convert.ToDouble(TxtCBal.Text);
                    BAL1 = BAL;
                    if (rdbpart.Checked == true)
                    {
                        BAL1 = Convert.ToDouble(Txtpartpay.Text);
                        // BAL = Convert.ToDouble(TxtPayAmt.Text); //--abhishek after calculation 09-03-2017
                    }
                    BAL1 = (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text) + Math.Abs(BAL1));
                    //result = DDS.InsertBatch("99", "99", "0", TxtAccName.Text, ViewState["CT"].ToString(), BAL1.ToString(), "1", "3", "CASH", ddlActivity.SelectedItem.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST);
                    ViewState["TCT"] = ViewState["TCT"] == null ? "0" : ViewState["TCT"].ToString();
                    result = DDS.InsertBatch(ViewState["PGL"].ToString(), TxtPType.Text, TxtTAccNo.Text, TxtTAName.Text, ViewState["TCT"].ToString(), TxtPayAmt.Text.ToString(), "1", "7", "TR", "TRF Dr FROM AC CLOSURE " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                    if (result > 0)
                    {
                        result = DDS.InsertBatch(ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), BAL1.ToString(), "2", "7", "TR", "TRF CR TO " + TxtPType.Text + "-" + TxtTAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                        if (result > 0)
                        {//The Set Number Suddenly Rises its count by n number as solution --Abhishek
                            // int RC = DDS.UpdateSetNo("46", ST);
                            GrdEntryDate.Visible = true;
                            BindGrid();
                            lblMessage.Text = "Record Added Successfully...........!!";
                            ModalPopup.Show(this.Page);
                            FL = "Insert";//Dhanya Shetty
                            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSCloser_PrematureTRF_" + TxtProcode + "_" + TxtAccNo + "_" + TxtPayAmt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        }
                    }

                    //}
                }
            }

            else if (ddlPayMode.SelectedValue == "3")   // ****** Cheque Entry ********
            {
                double PLAMT = 0;
                int result;
                double AMT = 0;
                string ST = DDS.GetSetNo("46"), AC, ACNM;
                ViewState["ST"] = ST.ToString();

                //If Account Number is blank Then Set Acc No and Cust No to Zero
                if (TxtTAccNo.Text == "")
                {
                    AC = "0";
                    ACNM = "";
                    ViewState["TCT"] = "0";
                }
                else
                {
                    AC = TxtTAccNo.Text;
                    ACNM = TxtTAName.Text;

                    if (Convert.ToInt32(ViewState["PGL"].ToString() == "" ? "0" : ViewState["PGL"].ToString()) >= 100)
                    {
                        ViewState["TCT"] = "0";
                    }
                }

                DataTable DT = new DataTable();
                DT = DDS.GetDDSInfo("4");

                if (ddlActivity.SelectedValue == "3")   // ***for Mature Closure***
                {
                    result = DDS.InsertBatch(ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), TxtCalcInt.Text.ToString(), "1", "7", "TR-INT", "INT TRF FROM " + ViewState["IR"].ToString() + "-" + TxtAccNo.Text + "", "MATURE CLOSURE" + TxtTrfNarration.Text + "", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                    if (Txt_Provi != null)
                        PLAMT = Convert.ToDouble(TxtINTC.Text);// -Convert.ToDouble(Txt_Provi.Text);


                    if (PLAMT > 0) //if AMT is Positive
                    {
                        result = DDS.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), PLAMT.ToString(), "2", "7", "TR-INT", "INT Dr TO " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                    }
                    else //if AMT is negative
                    {
                        PLAMT = Math.Abs(PLAMT);
                        result = DDS.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), PLAMT.ToString(), "1", "7", "TR-INT", "INT Cr TO " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                    }


                    if (Convert.ToInt32(Txt_Provi.Text == "" ? "0" : Txt_Provi.Text) > 0)
                    {
                        result = DDS.InsertBatch("10", ViewState["IR"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Txt_Provi.Text.ToString(), "2", "7", "TR-INT", "PROVISION Dr to " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                    }
                    if (result > 0)
                    {
                        AMT = Convert.ToDouble(TxtCalcInt.Text);
                        string[] TD = Session["EntryDate"].ToString().Split('/');
                        double BAL = Convert.ToDouble(TxtCBal.Text);
                        //if (rdbpart.Checked == true)
                        //{
                        // BAL = Convert.ToDouble(Txtpartpay.Text); //DDS SETNO NUMBER NOT GENERATED RANDOMLY SOLUT
                        BAL = Convert.ToDouble(TxtPayAmt.Text); //--abhishek after calculation 09-03-2017
                        //}
                        BAL1 = (BAL);

                        ViewState["TCT"] = ViewState["TCT"] == null ? "0" : ViewState["TCT"].ToString();

                        result = DDS.InsertBatch(ViewState["PGL"].ToString(), TxtPType.Text, AC, ACNM, ViewState["TCT"].ToString(), BAL1.ToString(), "1", "5", "TR", "CHQ Dr FROM  AC CLOSURE " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                        if (result > 0)
                        {
                            result = DDS.InsertBatch(ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), BAL1.ToString(), "2", "5", "TR", "CHQ Cr TO AC CLOSURE " + TxtPType.Text + "-" + AC.ToString() + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                            if (result > 0)
                            {
                                //The Set Number Suddenly Rises its count by n number as solution --Abhishek
                                GrdEntryDate.Visible = true;
                                BindGrid();
                                lblMessage.Text = "Record Added Successfully...........!!";
                                ModalPopup.Show(this.Page);
                                FL = "Insert";//Dhanya Shetty
                                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSCloser_MatureCheque_" + TxtProcode + "_" + TxtAccNo + "_" + TxtPayAmt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            }
                        }
                    }
                }

                ///////////////////// MATURE CLOSURE END



                ///////////////////// PRE MATURE CLOSURE END
                if (ddlActivity.SelectedValue == "2")   // ****premature ****CHEQUE
                {
                    if (TxtINTC.Text != "" && TxtINTC.Text != "0")
                    {
                        result = DDS.InsertBatch(ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), TxtCalcInt.Text.ToString(), "1", "7", "TR-INT", "INT TRF FROM " + ViewState["IR"].ToString() + "-" + TxtAccNo.Text + "", "MATURE CLOSURE" + TxtTrfNarration.Text + "", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                        if (Txt_Provi != null)
                            PLAMT = Convert.ToDouble(TxtINTC.Text);// -Convert.ToDouble(Txt_Provi.Text);


                        if (PLAMT > 0) //if AMT is Positive
                        {
                            result = DDS.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), PLAMT.ToString(), "2", "7", "TR-INT", "INT Dr TO " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                        }
                        else //if AMT is negative
                        {
                            PLAMT = Math.Abs(PLAMT);
                            result = DDS.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), PLAMT.ToString(), "1", "7", "TR-INT", "INT Cr TO " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                        }


                        if (Convert.ToInt32(Txt_Provi.Text == "" ? "0" : Txt_Provi.Text) > 0)
                        {
                            result = DDS.InsertBatch("10", ViewState["IR"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Txt_Provi.Text.ToString(), "2", "7", "TR-INT", "PROVISION Dr to " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                        }
                    }
                    if (Convert.ToInt32(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) > 0)
                    {
                        result = DDS.InsertBatch("100", DT.Rows[0]["serv_acc"].ToString(), TxtAccNo.Text, "", ViewState["CT"].ToString(), TxtServCHRS.Text, "1", "7", "TR", "SERVICE CHG " + TxtProcode.Text + "-" + TxtAccNo.Text + " ", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                    }
                    if (Convert.ToInt32(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) > 0)
                    {
                        result = DDS.InsertBatch("100", DT.Rows[0]["comm_Accno"].ToString(), TxtAccNo.Text, "", ViewState["CT"].ToString(), TxtPreMCAMT.Text, "1", "7", "TR", "COMMISION CHG " + TxtProcode.Text + "-" + TxtAccNo.Text + " ", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                    }
                    if (Convert.ToInt32(TxtGST.Text == "" ? "0" : TxtGST.Text) > 0)
                    {
                        string GLCODE = BD.GetAccTypeGL(DT.Rows[0]["GST_ACC"].ToString(), Session["BRCD"].ToString());
                        string[] GLC = GLCODE.Split('_');
                        result = DDS.InsertBatch(GLC[1].ToString(), DT.Rows[0]["GST_ACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), TxtGST.Text.ToString(), "1", "7", "TR", "GST Dr From " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);

                    }
                    AMT = Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) + Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text);

                    result = DDS.InsertBatch(ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), AMT.ToString(), "2", "7", "TR", "Cr to 100 P and L", ddlActivity.SelectedItem.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                    if (result > 0)
                    {

                        string[] TD = Session["EntryDate"].ToString().Split('/');
                        double BAL = Convert.ToDouble(TxtCBal.Text);//Convert.ToDouble(OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "2"));
                        BAL1 = BAL;
                        if (rdbpart.Checked == true)
                        {
                            // BAL1 = Convert.ToDouble(Txtpartpay.Text);
                            BAL1 = Convert.ToDouble(TxtPayAmt.Text); //--abhishek after calculation 09-03-2017
                        }
                        BAL1 = (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text) + Math.Abs((BAL1 - AMT)));

                        ViewState["TCT"] = ViewState["TCT"] == null ? "0" : ViewState["TCT"].ToString();
                        result = DDS.InsertBatch(ViewState["PGL"].ToString(), TxtPType.Text, AC, ACNM, ViewState["TCT"].ToString(), TxtPayAmt.Text.ToString(), "1", "5", "TR", "CHQ Dr FROM  AC CLOSURE " + TxtProcode.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                        if (result > 0)
                        {
                            result = DDS.InsertBatch(ViewState["GL"].ToString(), TxtProcode.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), BAL1.ToString(), "2", "5", "TR", "CHQ Cr TO AC CLOSURE " + TxtPType.Text + "-" + TxtTAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ST, TxtInstNo.Text, TxtInstDate.Text);
                            if (result > 0)
                            {
                                //The Set Number Suddenly Rises its count by n number as solution --Abhishek
                                GrdEntryDate.Visible = true;
                                BindGrid();
                                lblMessage.Text = "Record Added Successfully...........!!";
                                ModalPopup.Show(this.Page);
                                FL = "Insert";//Dhanya Shetty
                                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSCloser_PrematureCheque_" + TxtProcode + "_" + TxtAccNo + "_" + TxtPayAmt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
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

    public void ClearData()
    {
        TxtProcode.Text = "";
        TxtProName.Text = "";
        TxtAccNo.Text = "";
        TxtAccName.Text = "";
        TxtAccSTS.Text = "";
        TxtAccSTSName.Text = "";
        TxtAccType.Text = "";
        TxtAccTName.Text = "";
        TxtSplINSt.Text = "";
        TxtCBal.Text = "";
        ddlActivity.SelectedIndex = 0;
        TxtPreMC.Text = "";
        TxtPreMCAMT.Text = "";
        TxtServCHRS.Text = "";
        TxtINTC.Text = "";
        TxtPayAmt.Text = "";
        Txtpartpay.Text = "";
        ddlPayMode.SelectedIndex = 0;
        Txt_Provi.Text = "0";
        TxtOpeningDate.Text = "";
        TxtPeriod.Text = "";
        TxtCLDate.Text = "";
        TxtProcode.Focus();
        Txtcustno.Text = "";
        TxtDailyAmt.Text = "";
    }

    protected void PostEntry_Click(object sender, EventArgs e)
    {
        try
        {
            sResult = DDS.PostEntry(Session["MID"].ToString(), TxtAccType.Text, TxtAccNo.Text, Session["EntryDate"].ToString(), Session["BRCD"].ToString(), ViewState["ST"].ToString(), "RDCL");//FLAG ADDED for PAYMAST --ABHISHEK

            string[] SetNo = sResult.Split('_');
            if (Convert.ToDouble(SetNo[0].ToString()) > 0)
            {
               int RC=0;
                if (rdbfull.Checked==true)
                     RC = DDS.UpdateAcc(ViewState["GL"].ToString(),TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString());

                //if (RC > 0)
                //{
                    ClearData();
                    GrdEntryDate.Visible = false;
                    BtnCashPost.Visible = true;
                    PostEntry.Visible = false;
                    if (Convert.ToDouble(SetNo[0].ToString()) > 0 && Convert.ToDouble(SetNo[1].ToString()) > 0)
                        lblMessage.Text = "Your Payment Post Successfully with Receipt no :" + SetNo[0].ToString() + " and " + SetNo[1].ToString();
                    else
                        lblMessage.Text = "Your Payment Post Successfully with Receipt no :" + SetNo[0].ToString();
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "RDCLOSURE_Post_entry_" + TxtProcode + "_" + TxtAccNo + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;
                //}
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void GrdEntryDate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGrid();
    }

    public void BindGrid()
    {
        try
        {
            int Result = 0;
            if (ddlPayMode.SelectedValue == "1")
            {
                Result = DDS.GetInfoRD(GrdEntryDate, Session["MID"].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["EntryDate"].ToString(), Session["BRCD"].ToString(), ViewState["ST"].ToString(), "CP");
            }
            else if (ddlPayMode.SelectedValue == "2")
            {
                Result = DDS.GetInfoRD(GrdEntryDate, Session["MID"].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["EntryDate"].ToString(), Session["BRCD"].ToString(), ViewState["ST"].ToString(), "TR");
            }
            else if (ddlPayMode.SelectedValue == "3")
            {
                Result = DDS.GetInfoRD(GrdEntryDate, Session["MID"].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["EntryDate"].ToString(), Session["BRCD"].ToString(), ViewState["ST"].ToString(), "TR");
            }
            if (Result > 0)
            {
                BtnCashPost.Visible = false;
                PostEntry.Visible = true;
            }
            else
            {
                BtnCashPost.Visible = true;
                PostEntry.Visible = false;
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
            string AGNAME = TxtProName.Text;
            string[] AgentCode = AGNAME.Split('_');
            if (AgentCode.Length > 1)
            {
                //Added By Amol on 22092017 as per ambika mam instruction
                if (BD.GetProdOperate(Session["BRCD"].ToString(), AgentCode[1].ToString()).ToString() != "3")
                {
                    TxtProcode.Text = AgentCode[0].ToString();
                    string GC = BD.GetAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString());
                    if (GC != null)
                    {
                        string[] GCA = GC.Split('_');
                        ViewState["GL"] = GCA[1].ToString();
                        TxtProName.Text = (string.IsNullOrEmpty(AgentCode[2].ToString()) ? "" : AgentCode[1].ToString());
                        TxtAccNo.Focus();
                    }
                    else
                    {
                        WebMsgBox.Show("Invalid Glcode....!", this.Page);
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
            GSTLogic();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnClearALL_Click(object sender, EventArgs e)
    {
        ClearData();
    }

    protected void TxtTAccNo_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            int Result = LI.CheckAccountExist(TxtTAccNo.Text, TxtPType.Text, Session["BRCD"].ToString());
            if (Result == 0)
            {
                WebMsgBox.Show("Sorry Account Number Not Exist......!!", this.Page);
                return;
            }
            int RC = LI.CheckAccount(TxtTAccNo.Text, TxtPType.Text, Session["BRCD"].ToString());
            if (RC < 0)
            {
                TxtAccNo.Focus();
                WebMsgBox.Show("Please Enter valid Account Number Account Not Exist..........!!", this.Page);
                return;
            }
            ViewState["TCT"] = RC;
            TxtTAName.Text = BD.GetCustName(RC.ToString(), Session["BRCD"].ToString());
            TxtTrfNarration.Focus();
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

    protected void TxtServCHRS_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double CLBAL = 0;
            if (rdbpart.Checked == true)
            {
                CLBAL = Convert.ToDouble(Txtpartpay.Text);
            }
            else
            {
                CLBAL = Convert.ToDouble(TxtCBal.Text);
            }
            double GST = 0;
            double AMT = 0;
            if (ViewState["GSTYN"].ToString() == "Y")
            {
                GST = Convert.ToDouble(TxtPreMCAMT.Text) + Convert.ToDouble(TxtServCHRS.Text);
                GST = Math.Round(GST * 18 / 100, 0);

                TxtGST.Text = GST.ToString();
                AMT = ((CLBAL + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) + Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text)));
            }
            else
            {
                AMT = ((CLBAL + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text)));
                TxtGST.Text = "0";
            }

            TxtPayAmt.Text = Math.Round(AMT, 0).ToString();
            if (ViewState["GSTYN"].ToString() == "Y")
            {
                TxtGST.Focus();
            }
            else
            {
                ddlPayMode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    //Added By AmolB ON 2017-01-23 If User Enter Amount Manually
    protected void TxtPreMCAMT_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double CLBAL = 0;
            if (rdbpart.Checked == true)
            {
                CLBAL = Convert.ToDouble(Txtpartpay.Text);
            }
            else
            {
                CLBAL = Convert.ToDouble(TxtCBal.Text);
            }
            double GST = 0;
            double AMT = 0;

            if (ViewState["GSTYN"].ToString() == "Y")
            {
                GST = Convert.ToDouble(TxtPreMCAMT.Text) + Convert.ToDouble(TxtServCHRS.Text);
                GST = Math.Round(GST * 18 / 100, 0);
                TxtGST.Text = GST.ToString();

                AMT = ((CLBAL + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) + Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text)));

            }
            else
            {
                AMT = ((CLBAL + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text)));
            }
            TxtPayAmt.Text = Math.Round(AMT, 0).ToString();
            TxtServCHRS.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Txt_Provi_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double CLBAL = 0;
            if (rdbpart.Checked == true)
            {
                CLBAL = Convert.ToDouble(Txtpartpay.Text);
            }
            else
            {
                CLBAL = Convert.ToDouble(TxtCBal.Text);
            }
            double AMT = 0;

            if (ViewState["GTSYN"].ToString() == "Y")
            {
                AMT = ((CLBAL + (Convert.ToDouble(Txt_Provi.Text == "" ? "0" : Txt_Provi.Text)) + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) + Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text)));
            }
            else
            {
                AMT = ((CLBAL + (Convert.ToDouble(Txt_Provi.Text == "" ? "0" : Txt_Provi.Text)) + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text)));
            }
            // double AMT = ((CLBAL + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text)));
            TxtPayAmt.Text = Math.Round(AMT, 0).ToString();
            TxtCalcInt.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtINTC_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    double CLBAL = 0;
        //    if (rdbpart.Checked == true)
        //    {
        //        CLBAL = Convert.ToDouble(Txtpartpay.Text);
        //    }
        //    else
        //    {
        //        CLBAL = Convert.ToDouble(TxtCBal.Text);
        //    }

        //    double AMT = ((CLBAL + (Convert.ToDouble(Txt_Provi.Text == "" ? "0" : Txt_Provi.Text)) + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text)));
        //    TxtPayAmt.Text = Math.Round(AMT, 0).ToString();
        //}
        //catch (Exception Ex)
        //{
        //    ExceptionLogging.SendErrorToText(Ex);
        //}
    }
    protected void rdbfull_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            Txtpartpay.Text = TxtCBal.Text;
            Txtpartpay.Enabled = false;
            if (ddlActivity.SelectedValue == "2")
            {
                Calc_("PR");
                Txt_Provi.Focus();

            }
            else if (ddlActivity.SelectedValue == "3")
            {
                Btn_Printsummary.Enabled = true;
                Calc_("MR");
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Txtpartpay_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbfull.Checked == true || rdbpart.Checked == true)
            {
                if (Convert.ToDouble(Txtpartpay.Text) > Convert.ToDouble(TxtCBal.Text))
                {
                    WebMsgBox.Show("Amount entered should not be greater than Total Amount...!", this.Page);
                    Txtpartpay.Text = "";
                    Txtpartpay.Focus();
                }
                else
                {
                    ddlActivity.Focus();
                    if (ddlActivity.SelectedValue == "2")
                    {
                        Calc_("PR");
                        Txt_Provi.Focus();

                    }
                    else if (ddlActivity.SelectedValue == "3")
                    {
                        Btn_Printsummary.Enabled = true;
                        Calc_("MR");
                    }
                }
            }
            else
            {
                WebMsgBox.Show("First Select Payment Type .....!", this.Page);
                Txtpartpay.Text = "";
                rdbfull.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void rdbpart_CheckedChanged(object sender, EventArgs e)
    {
        Txtpartpay.Enabled = true;
        Txtpartpay.Focus();
    }
    protected void GridCalcu_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        double SumIntr = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Font.Bold = true;
        }
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        // string sponsorBonus = ((Label)e.Row.FindControl("INTR_AMT")).Text;

        // double totalvalue = Convert.ToDouble(sponsorBonus);
        // e.Row.Cells[4].Text = totalvalue.ToString();
        // SumIntr += totalvalue;
        //}

        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //   Label lbl = (Label)e.Row.FindControl("lblTotal");
        //   lbl.Text = SumIntr.ToString();
        //}
    }
    protected void TxtCalcInt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double CLBAL = 0;
            if (rdbpart.Checked == true)
            {
                CLBAL = Convert.ToDouble(Txtpartpay.Text);
            }
            else
            {
                CLBAL = Convert.ToDouble(TxtCBal.Text);
            }

            double AMT = ((CLBAL + (Convert.ToDouble(Txt_Provi.Text == "" ? "0" : Txt_Provi.Text)) + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text)));
            //  double AMT = ((CLBAL + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text)));
            TxtPayAmt.Text = Math.Round(AMT, 0).ToString();
            TxtPreMC.Focus();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void GridDeposite_PageIndexChanging(object sender, GridViewPageEventArgs e)//amruta 26/04/2017
    {
        GridDeposite.PageIndex = e.NewPageIndex;
    }
    protected void Btn_Printsummary_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSCloser_Print_Summry_" + TxtProcode + "_" + TxtAccNo + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&GLC=2&SUBGL=" + TxtProcode.Text + "&ACCNO=" + TxtAccNo.Text + "&rptname=RptDDSCMonthlySummary.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void LnkVerify_Click(object sender, EventArgs e)
    {
        try
        {
            hdnRow.Value = "0";
            ShowImage();
            btnPrev.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    private DataTable GetData(string query)
    {
        DataTable dt = new DataTable();
        string constr = conn.DbNamePH();
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
            }
            return dt;
        }
    }
    public void ShowImage()
    {
        try
        {

            txtProd.Text = TxtProcode.Text;
            txtProdCode.Text = TxtProName.Text;
            txtaccno1.Text = TxtAccNo.Text;
            txtaccname1.Text = TxtAccName.Text;
            txtcust.Text = Txtcustno.Text;
            DataTable dt = new DataTable();
            dt = GetData("select id,SignName,PhotoName,SignIMG,PhotoImg from  Imagerelation where BRCD='" + Session["BRCD"].ToString() + "' and CustNo=" + Txtcustno.Text + " ");
            ////string SaveLocation = Server.MapPath("~/Uploads/");
            ////string[] filePaths = Directory.GetFiles(Server.MapPath("~/Uploads/")); //Get File List in chosen directory
            ////List<ListItem> files = new List<ListItem>();
            string FileName = "";
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > Convert.ToInt32(hdnRow.Value))
                {
                    if (dt.Rows.Count > (Convert.ToInt32(hdnRow.Value) + 1))
                    {
                        btnNext.Visible = true;
                    }
                    else
                    {
                        btnNext.Visible = false;
                    }
                    int i = Convert.ToInt32(hdnRow.Value);
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
                            ////FileInfo file = new FileInfo(Server.MapPath("~/Uploads/"+FilePath + ".jpg")); //get individual file info
                            ////FileName = Path.GetFileName(file.FullName);  //output individual file name
                            ////string EX = Path.GetExtension(FileName);
                            ////FileName = FileName.Replace(EX, "");
                            ////string input = Server.MapPath("~/Uploads/" + FileName + EX);
                            ////string output = Server.MapPath("~/Uploads/" + FileName + "_dec" + EX);
                            ////this.Decrypt(input, output);
                            ////string base64String;
                            ////using (System.Drawing.Image image = System.Drawing.Image.FromFile(output))
                            ////{
                            ////    using (MemoryStream m = new MemoryStream())
                            ////    {
                            ////        image.Save(m, image.RawFormat);
                            ////        byte[] imageBytes = m.ToArray();
                            ////        base64String = Convert.ToBase64String(imageBytes);
                            ////    }
                            ////}
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            //base64String = Base64Decode(base64String);
                            if (y == 0)
                            {
                                image1.Src = "data:image/tif;base64," + base64String;
                            }
                            else if (y == 1)
                            {
                                image2.Src = "data:image/tif;base64," + base64String;
                            }

                            //File.Delete(output);
                        }
                        else
                        {

                            if (y == 0)
                            {
                                image1.Src = "";

                            }
                            else if (y == 1)
                            {
                                image2.Src = "";
                            }
                        }

                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('.bs-example-modal-lg').modal('show');</script>", false);
            }
            else
            {

                WebMsgBox.Show("Sorry, No record Found !!!!", this.Page);
            }

        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        hdnRow.Value = (Convert.ToInt32(hdnRow.Value) - 1).ToString();
        ShowImage();
        if (Convert.ToInt32(hdnRow.Value) == 0)
            btnPrev.Visible = false;
        else
            btnPrev.Visible = true;

        btnNext.Visible = true;
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        hdnRow.Value = (Convert.ToInt32(hdnRow.Value) + 1).ToString();
        ShowImage();
        btnPrev.Visible = true;
    }
    protected void TxtTrfNarration_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPayMode.SelectedValue == "3")
                TxtInstNo.Focus();
            else if (ddlPayMode.SelectedValue == "2")
                BtnCashPost.Focus();
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
            DDS.GetinfotableRD(grdCashRct, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "RDCLOSE");
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
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSCloser_Print" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
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
            Session["densamt"] = CurrentCls.GetTotalAmt(Session["BRCD"].ToString(), Session["densset"].ToString(), Session["EntryDate"].ToString());
            Session["denssubgl"] = dens[2].ToString();
            Session["densact"] = dens[3].ToString();

            string i = CurrentCls.checkDenom(Session["BRCD"].ToString(), Session["densset"].ToString(), Session["EntryDate"].ToString());
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

    protected void TxtGST_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double CLBAL = 0;
            DataTable DT = DDS.GetDDSInfo("4");
            if (DT.Rows.Count > 0)
            {
                if (DT.Rows[0]["GST_ACC"].ToString() != null && DT.Rows[0]["GST_ACC"].ToString() != "")
                {
                    if (rdbpart.Checked == true)
                    {
                        CLBAL = Convert.ToDouble(Txtpartpay.Text);
                    }
                    else
                    {
                        CLBAL = Convert.ToDouble(TxtCBal.Text);
                    }

                    // double AMT = ((CLBAL + (Convert.ToDouble(Txt_Provi.Text == "" ? "0" : Txt_Provi.Text)) + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text)));
                    double AMT = ((CLBAL + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) + Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text)));
                    TxtPayAmt.Text = Math.Round(AMT, 0).ToString();
                }
                else
                {
                    lblMessage.Text = "Add Goods and Service Tax GL/PL to Post GST..!";
                    ModalPopup.Show(this.Page);
                    TxtGST.Text = "0";
                    return;
                }
            }
            ddlPayMode.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void Photo_Sign()
    {
        DataTable Datatbl = new DataTable();
        try
        {
            //  Added By Amol On 2018-02-05 For take additional share
            if (TxtProcode.Text == "4")//changes by ankita checked prod code instead of glcode 06/03/2018
            {
                Param = CC.getShrParam();
                if (Param == "HO" || Param == "ho" || Param == "Ho")
                    Datatbl = CC.ShowIMAGE(Convert.ToString(ViewState["CT"]), "1", TxtAccNo.Text.ToString());
                else
                    Datatbl = CC.ShowIMAGE(ViewState["CUSTNO"].ToString(), Session["BRCD"].ToString(), TxtAccNo.Text.ToString());
            }
            else
                Datatbl = CC.ShowIMAGE(Convert.ToString(ViewState["CT"]), Session["BRCD"].ToString(), TxtAccNo.Text.ToString());

            if (Datatbl.Rows.Count > 0)
            {
                int i = 0;
                String FilePath = "";
                byte[] bytes = null;
                for (int y = 0; y < 2; y++)
                {
                    if (y == 0)
                    {
                        FilePath = Datatbl.Rows[i]["SignIMG"].ToString();
                        if (FilePath != "")
                            bytes = (byte[])Datatbl.Rows[i]["SignIMG"];

                    }
                    else
                    {
                        FilePath = Datatbl.Rows[i]["PhotoImg"].ToString();
                        if (FilePath != "")
                            bytes = (byte[])Datatbl.Rows[i]["PhotoImg"];
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
}