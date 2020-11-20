using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Drawing;

public partial class FrmDDSClose : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    ClsDDCR DDS = new ClsDDCR();
    ClsOpenClose OC = new ClsOpenClose();
    ClsStatementView SV = new ClsStatementView();
    ClsLoanInstallmen LI2 = new ClsLoanInstallmen();
    ClsAuthorized AT = new ClsAuthorized();
    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLoanInfo LI = new ClsLoanInfo();
    ClsLoanInstallment LINST = new ClsLoanInstallment();
    ClsInsertTrans ITrans = new ClsInsertTrans();
    ClsLoanClosure LC = new ClsLoanClosure();
    ClsCommon CC = new ClsCommon();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsBindBrDetails ASM = new ClsBindBrDetails();
    ClsCashPayment CurrentCls = new ClsCashPayment();
    ClsCashReciept CR = new ClsCashReciept();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsMultiVoucher MV = new ClsMultiVoucher();
    scustom SCS = new scustom();
    string sResult = "", AC_Status = "", AC = "", ACNM = "", FL = "";
    double TotalAmt, TotalDrAmt, PrincAmt, AMT, INTAMT, PLAMT = 0;
    double SurTotal, OtherChrg, CurSurChrg = 0;
    int result, resultout = 0;
    double SumFooterValue = 0, TotalValue = 0;
    bool ValidLien = false;

    protected void Page_Load(object sender, EventArgs e)
    {

        AutoAcc.ContextKey = Session["BRCD"].ToString() + "-" + TxtAGCD.Text.ToString();
        autoAgname.ContextKey = Session["BRCD"].ToString();
        AutoPName.ContextKey = Session["BRCD"].ToString();

        AutoAcc.ContextKey = Session["BRCD"].ToString() + "-1_0000";

        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            LnkVerify.Visible = false;
            TxtAGCD.Focus();

            EnableDisableInt();

            Btn_Printsummary.Enabled = false;
            BindGrid1();
            GSTLogic();
            //Added By Amol on 29-09-2017 for clear data from temp table
            DDS.DelAllRecTable(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            ViewState["PayableAmt"] = "";

            string BKCD = CC.GetBANKCode();
            if (BKCD != null)
            {
                ViewState["BKCD"] = BKCD.ToString();
            }
            else
            {
                ViewState["BKCD"] = "0";
            }
            string ParaProvi = CC.GetUniversalPara("DDSPROVI_REV");
            Hdn_ParRevProvi.Value = string.IsNullOrEmpty(ParaProvi) ? "Y" : ParaProvi;

        }

    }
    public void EnableDisableInt()
    {
        try
        {
            string ParaV = CC.GetDDSINT();
            if (ParaV != null && ParaV == "Y")
            {
                string UG = CC.GetDDSUG();
                if (UG == "0")
                {
                    TxtCalcInt.Enabled = true;
                }
                else if (Session["UGRP"].ToString() == UG || Session["UGRP"].ToString() == "4") // Abhishek added hardcode 4 on demand marathwada (Ambika mam) 08-08-2018
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
            //  WebMsgBox.Show("Add 'GSTYN' Parameter to 'Y' or 'N' for GST Calculation...", this.Page);
            TxtGST.Enabled = false;
            TxtAGCD.Text = "";
            TxtAGName.Text = "";

        }
    }

    public void BindCalculation()
    {
        try
        {
            int RR = DDS.BindCalcu(GridCalcu, Session["BRCD"].ToString(), "2", TxtAGCD.Text, TxtAccNo.Text, Session["EntryDate"].ToString());
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
            string ParaProvi = CC.GetUniversalPara("DDSPROVI_REV");
            ViewState["RevProvi"] = ParaProvi;
            if (rdbfull.Checked == true)
            {
                ViewState["RevProvi"] = "Y";
            }
            else if (ParaProvi != null)
            {
                ViewState["RevProvi"] = "N";
            }
            else
            {
                ViewState["RevProvi"] = "Y";
            }

            Hdn_ParRevProvi.Value = ViewState["RevProvi"].ToString();


            if (FL == "PR")
            {
                TxtGST.Enabled = true;
                DataTable DT = new DataTable();
                string SC = "";
                DT = LINST.GetDDSInfo("3");
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
                    string ParaAdmin = CC.GetDDSADMINCHG();
                    if (ParaAdmin == "Y")
                    {
                        SSR = Convert.ToDouble(string.IsNullOrEmpty(DT.Rows[0]["ADMIN_CHG"].ToString()) ? "0" : DT.Rows[0]["ADMIN_CHG"].ToString());
                    }
                    else
                    {
                        SR = Convert.ToDouble(SC);
                        SSR = CLBAL * SR / 100;
                    }

                    TxtServCHRS.Text = Math.Round(SSR, 0).ToString();
                    // double AMT = CLBAL - PRMC + SSR;
                    double TT = Math.Round(Convert.ToDouble(TxtPreMCAMT.Text) + SSR);
                    double AMT = CLBAL - (TT);
                    double GST = 0;
                    if (ViewState["GSTYN"].ToString() == "Y")
                    {

                        GetGst_Details();


                        string ParaOTHERCHGGST = CC.GetUniversalPara("GSTON_OTHERCHG");
                        string ParaCOMMI = CC.GetUniversalPara("GSTON_COMMI");

                        if (ParaOTHERCHGGST == "Y" && ParaCOMMI == "N")
                        {
                            GST = Convert.ToDouble(TxtServCHRS.Text);
                        }
                        else if (ParaOTHERCHGGST == "N" && ParaCOMMI == "Y")
                        {
                            GST = Convert.ToDouble(TxtPreMCAMT.Text);
                        }
                        else
                        {
                            GST = Convert.ToDouble(TxtPreMCAMT.Text) + Convert.ToDouble(TxtServCHRS.Text);
                        }

                        //GST = Math.Round(GST * Convert.ToDouble(ViewState["RATE_GST"]) / 100, 0);

                        double CgstAmt = 0, SgstAmt = 0;
                        CgstAmt = Math.Round(GST * Convert.ToDouble(ViewState["RATE_CGST"].ToString()) / 100, 2);
                        SgstAmt = Math.Round(GST * Convert.ToDouble(ViewState["RATE_SGST"].ToString()) / 100, 2);

                        TxtGST.Text = (CgstAmt + SgstAmt).ToString();

                        ViewState["CGST_AMT"] = CgstAmt.ToString();
                        ViewState["SGST_AMT"] = SgstAmt.ToString();


                        AMT = ((CLBAL + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) + Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text)));

                        TxtPayAmt.Text = AMT.ToString();

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
                    //if (MD >= 12)
                    //{
                    string[] TD = Session["EntryDate"].ToString().Split('/');
                    BAL = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), (Convert.ToInt32(TxtAGCD.Text) + 3000).ToString(), TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "22");
                    //}
                    Txt_Provi.Text = BAL.ToString();

                    #region 6 month Para
                    string ParaMon = CC.GetUniversalPara("DDSMAT_6MON");
                    if (ParaMon == "Y")
                    {
                        string MonDiff = CC.GetFunc_MonDiff(string.IsNullOrEmpty(TxtLastIntDate.Text) ? TxtOpeningDate.Text : TxtLastIntDate.Text, Session["EntryDate"].ToString());
                        if (MonDiff != null)
                        {
                            if (Convert.ToInt32(MonDiff) >= 6)
                            {
                                Calc_("PR_W_MR");
                            }
                        }
                    }
                    else
                    {
                        BindCalculation();
                    }
                    #endregion

                }

            }
            else if (FL == "MR")
            {
                TxtGST.Enabled = false;
                TxtGST.Text = "0";
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
                BAL = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), (Convert.ToInt32(TxtAGCD.Text) + 3000).ToString(), TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "22");

                Txt_Provi.Text = BAL.ToString();

                double IAMT = DDS.GetInterest(Session["BRCD"].ToString(), "2", TxtAGCD.Text, TxtAccNo.Text, Session["EntryDate"].ToString());

                TxtCalcInt.Text = Math.Round(IAMT, 0).ToString();

                Txt_Provi.Focus();


                if (ViewState["RevProvi"].ToString() != "N")
                {
                    TxtINTC.Text = (Convert.ToDouble(TxtCalcInt.Text) - Convert.ToDouble(Txt_Provi.Text)).ToString();
                }
                else
                {
                    TxtINTC.Text = (Convert.ToDouble(TxtCalcInt.Text)).ToString();
                }


                TxtPayAmt.Text = (CLBAL + Convert.ToDouble(TxtCalcInt.Text)).ToString();
                if (TxtCalcInt.Text == "0")
                {
                    WebMsgBox.Show("Interest not calculated, Check Interest Rate or Check Effect Date!", this.Page);
                }
                BindCalculation();
            }
            else if (FL == "PR_W_MR")
            {
                TxtGST.Enabled = true;
                GridCalcu.Visible = true;
                TxtPreMC.Text = "0";
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

                double GST = 0;
                if (ViewState["GSTYN"].ToString() == "Y")
                {

                    GetGst_Details();

                    string ParaOTHERCHGGST = CC.GetUniversalPara("GSTON_OTHERCHG");
                    string ParaCOMMI = CC.GetUniversalPara("GSTON_COMMI");

                    if (ParaOTHERCHGGST == "Y" && ParaCOMMI == "N")
                    {
                        GST = Convert.ToDouble(TxtServCHRS.Text);
                    }
                    else if (ParaOTHERCHGGST == "N" && ParaCOMMI == "Y")
                    {
                        GST = Convert.ToDouble(TxtPreMCAMT.Text);
                    }
                    else
                    {
                        GST = Convert.ToDouble(TxtPreMCAMT.Text) + Convert.ToDouble(TxtServCHRS.Text);
                    }

                    //GST = Math.Round(GST * Convert.ToDouble(ViewState["RATE_GST"]) / 100, 0);

                    double CgstAmt = 0, SgstAmt = 0;
                    CgstAmt = Math.Round(GST * Convert.ToDouble(ViewState["RATE_CGST"].ToString()) / 100, 2);
                    SgstAmt = Math.Round(GST * Convert.ToDouble(ViewState["RATE_SGST"].ToString()) / 100, 2);

                    TxtGST.Text = (CgstAmt + SgstAmt).ToString();

                    ViewState["CGST_AMT"] = CgstAmt.ToString();
                    ViewState["SGST_AMT"] = SgstAmt.ToString();


                    AMT = ((CLBAL + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) + Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text)));



                }
                else
                {

                    TxtGST.Text = "0";
                }

                double BAL = 0;
                int MD = Convert.ToInt32(conn.GetMonthDiff(TxtOpeningDate.Text.Replace("12:00:00", ""), Session["EntryDate"].ToString()));
                string[] TD = Session["EntryDate"].ToString().Split('/');
                BAL = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), (Convert.ToInt32(TxtAGCD.Text) + 3000).ToString(), TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "22");
                Txt_Provi.Text = BAL.ToString();
                double IAMT = DDS.GetInterest(Session["BRCD"].ToString(), "2", TxtAGCD.Text, TxtAccNo.Text, Session["EntryDate"].ToString());
                TxtCalcInt.Text = Math.Round(IAMT, 0).ToString();
                Txt_Provi.Focus();


                if (ViewState["RevProvi"].ToString() != "N")
                {
                    TxtINTC.Text = (Convert.ToDouble(TxtCalcInt.Text) - Convert.ToDouble(Txt_Provi.Text)).ToString();
                }
                else
                {
                    TxtINTC.Text = (Convert.ToDouble(TxtCalcInt.Text)).ToString();
                }

                TxtPayAmt.Text = ((CLBAL + Convert.ToDouble(TxtCalcInt.Text)) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text))).ToString();
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
            if (txtTotLoanBal.Text != "" && ddlPayMode.SelectedValue != "2")
            {
                WebMsgBox.Show("Account is having lien. Only Transfer pay type need to be selecetd...!", this.Page);
                ddlPayMode.SelectedValue = "0";
                return;
            }
            if (ddlPayMode.SelectedValue == "1")
            {
                ViewState["PGL"] = "0";
                DivTransfer.Visible = false;
            }
            else if (ddlPayMode.SelectedValue == "2")
            {
                ViewState["PGL"] = "0";
                DivTransfer.Visible = true;
                DIV_INSTRUMENT.Visible = false;
            }
            else
            {
                ViewState["PGL"] = "0";
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

    public void ProCode()
    {
        try
        {

            string NM = "";

            //Added By Amol on 22092017 as per ambika mam instruction
            if (BD.GetProdOperate(Session["BRCD"].ToString(), TxtAGCD.Text.Trim().ToString()).ToString() != "3")
            {
                NM = TxtAGCD.Text;
                string TDN = DDS.GetAgentName_IR(TxtAGCD.Text, Session["BRCD"].ToString());
                if (TDN != "0")
                {
                    string[] TD = TDN.Split('_');
                    ClearData();

                    TxtAGCD.Text = NM;
                    TxtAGName.Text = TD[0].ToString();
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

                    AutoAcc.ContextKey = Session["BRCD"].ToString() + "-" + TxtAGCD.Text + "_1111";
                    TxtAccNo.Focus();
                }
                else
                {
                    WebMsgBox.Show("Enter a valid Agent Code!....", this.Page);
                    TxtAGCD.Text = "";
                    TxtAGCD.Focus();

                }
                GSTLogic();
            }
            else
            {
                TxtAGCD.Text = "";
                TxtAGName.Text = "";
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

    protected void TxtAGCD_TextChanged(object sender, EventArgs e)
    {
        ProCode();
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
    public void Accno()
    {
        try
        {
            string AT = "";
            AC_Status = CC.GetAccStatus(Session["BRCD"].ToString(), TxtAGCD.Text, TxtAccNo.Text);
            if (AC_Status == "1" || AC_Status == "4")
            {
                string[] TD = Session["EntryDate"].ToString().Split('/');
                GetAccInfo();
                ShowImage();
                double BAL = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtAGCD.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "2");
                TxtCBal.Text = BAL.ToString();
                if (BAL <= 0)
                {
                    ClearData();
                    TxtAGCD.Focus();
                    lblMessage.Text = "Account Balance Insufficient ...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
                DataTable DT = new DataTable();
                DT = OC.GetOpeningDate(Session["BRCD"].ToString(), TxtAGCD.Text, TxtAccNo.Text, Session["EntryDate"].ToString());
                if (DT.Rows.Count > 0)
                {
                    TxtOpeningDate.Text = DT.Rows[0]["OPENINGDATE"].ToString().Replace("12:00:00", "");
                    string LDT = OC.GetLastIntDateDDSHISTORY(Session["BRCD"].ToString(), TxtAGCD.Text, TxtAccNo.Text);
                    if (LDT != null)
                    {
                        TxtLastIntDate.Text = LDT.ToString();
                    }
                    else
                    {
                        TxtLastIntDate.Text = DT.Rows[0]["OPENINGDATE"].ToString().Replace("12:00:00", "");
                    }

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
                if (AC_Status == "4")
                {
                    //  Added by ankita on 19/06/2017 to display Lean details
                    dt = DDS.getDetails(Session["BRCD"].ToString(), TxtAGCD.Text, TxtAccNo.Text);
                    if (dt.Rows.Count > 0)
                    {
                        lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Lean Marked / Loan Advanced .........!! with LoanGlCode=" + dt.Rows[0]["LOANGLCODE"].ToString() + " and LoanAccNo=" + dt.Rows[0]["LOANACCNO"].ToString() + " and Loan Amount=" + dt.Rows[0]["LOANAMT"].ToString() + "";
                        ModalPopup.Show(this.Page);
                        GetLoanDetails(dt.Rows[0]["LOANGLCODE"].ToString(), dt.Rows[0]["LOANACCNO"].ToString());
                        ValidLien = true;
                        Div_LoanDetails.Visible = true;
                    }
                    else
                    {
                        WebMsgBox.Show("Lien Details not found, Add Lien Details...!", this.Page);
                    }

                }
                //int RC = LC.BindGrid(GridDeposite, Session["BRCD"].ToString(), ViewState["GL"].ToString(), TxtAGCD.Text, TxtAccNo.Text, 1);
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

            else if (AC_Status == "5")
            {
                lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Credit Freeze.........!!";
                ModalPopup.Show(this.Page);
                ClearData();
            }
            else if (AC_Status == "6")
            {
                lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Debit Freeze.........!!";
                ModalPopup.Show(this.Page);
                ClearData();
            }
            else if (AC_Status == "7")
            {
                lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Total Freeze.........!!";
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
    protected void TxtAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Accno();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void GetLoanDetails(string Subgl, string Accno)
    {
        try
        {
            dt = new DataTable();
            string AC1;
            dt = MV.GetLoanTotalAmount(Session["BRCD"].ToString(), Subgl.Trim().ToString(), Accno.Trim().ToString(), Session["EntryDate"].ToString(), "");
            //dt = CurrentCls1.GetLoanDetails(Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["LGlCode"] = dt.Rows[0]["GlCode"].ToString() == "" ? "0" : dt.Rows[0]["GlCode"].ToString();
                ViewState["LSubGlCode"] = dt.Rows[0]["SubGlCode"].ToString() == "" ? "0" : dt.Rows[0]["SubGlCode"].ToString();
                ViewState["LAccNo"] = dt.Rows[0]["AccountNo"].ToString() == "" ? "0" : dt.Rows[0]["AccountNo"].ToString();

                TxtLoanSubglcode.Text = dt.Rows[0]["SubGlCode"].ToString() == "" ? "0" : dt.Rows[0]["SubGlCode"].ToString();
                TxtLoanAccno.Text = dt.Rows[0]["AccountNo"].ToString() == "" ? "0" : dt.Rows[0]["AccountNo"].ToString();

                AC1 = LI2.Getaccno(TxtLoanSubglcode.Text, Session["BRCD"].ToString());
                string[] AC = AC1.Split('_'); ;
                TxtLoanSubglName.Text = AC[1].ToString();
                string Name = SCS.GetAccName_DDSloan(TxtLoanAccno.Text, TxtLoanSubglcode.Text, Session["Brcd"].ToString());
                TxtLoanCustName.Text = string.IsNullOrEmpty(Name) ? "N/A" : Name.ToString();
                txtSancAmount.Text = dt.Rows[0]["Limit"].ToString() == "" ? "0" : dt.Rows[0]["Limit"].ToString();
                txtLoanBal.Text = dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString();
                TxtLoanLastIntdt.Text = dt.Rows[0]["LastIntdate"].ToString() == "" ? "" : dt.Rows[0]["LastIntdate"].ToString().Substring(0, 10);
                txtDays.Text = dt.Rows[0]["Days"].ToString() == "" ? "0" : dt.Rows[0]["Days"].ToString();
                txtLoanInt.Text = dt.Rows[0]["Interest"].ToString() == "" ? "0" : dt.Rows[0]["Interest"].ToString();
                txtLoanInt.Text = (Convert.ToDouble(txtLoanInt.Text) + Convert.ToDouble(dt.Rows[0]["Interest"].ToString() == "" ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString())).ToString(); ;
                txtTotLoanBal.Text = (Convert.ToDouble(txtLoanBal.Text) + Convert.ToDouble(txtLoanInt.Text)).ToString();

            }
            else
            {
                txtSancAmount.Text = "0";
                txtLoanBal.Text = "0";
                TxtLoanLastIntdt.Text = "";
                txtDays.Text = "0";
                txtLoanInt.Text = "0";
                txtTotLoanBal.Text = "0";

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
            DT = DDS.GetAccInfo(TxtAGCD.Text, Session["BRCD"].ToString(), TxtAccNo.Text);
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
                //TxtSplINSt.Focus();
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

    public void Name_Change(string CUNAME, string FL)
    {
        try
        {
            string[] custnob = CUNAME.Split('_');
            if (FL == "NCODE")
            {
                string AGCD = DDS.GetAgName_Code(Session["BRCD"].ToString(), custnob[1].ToString());
                if (AGCD != null)
                {
                    string[] AG = AGCD.Split('_');
                    TxtAGCD.Text = AG[0].ToString();
                    ProCode();
                }
                else
                {
                    WebMsgBox.Show("Agent Code for search not found....!", this.Page);
                    TxtAGCD.Text = "";
                    TxtAGName.Text = "";
                    TxtAccName.Text = "";
                    TxtAccNo.Text = "";
                    TxtAGCD.Focus();
                    return;
                }
            }
            
                if (custnob.Length > 1)
                {
                    AC_Status = CC.GetAccStatus(Session["BRCD"].ToString(), TxtAGCD.Text, custnob[1].ToString());
                    if (AC_Status == "1")
                    {
                        TxtAccName.Text = custnob[0].ToString();
                        TxtAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                        string[] TD = Session["EntryDate"].ToString().Split('/');
                        GetAccInfo();
                        ShowImage();
                        double BAL = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtAGCD.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "2");
                        if (BAL <= 0)
                        {
                            ClearData();
                            TxtAGCD.Focus();
                            lblMessage.Text = "Balance Insufficient......!!";
                            ModalPopup.Show(this.Page);
                            return;
                        }
                        TxtCBal.Text = BAL.ToString();
                        DataTable DT = new DataTable();
                        DT = OC.GetOpeningDate(Session["BRCD"].ToString(), TxtAGCD.Text, TxtAccNo.Text, Session["EntryDate"].ToString());
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

                        GetAccInfo();
                        BindGrid();
                        txtPan.Text = PanCard(Session["BRCD"].ToString(), custnob[2].ToString());
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
                        //  dt = DDS.getDetails(Session["BRCD"].ToString(), TxtAGCD.Text, TxtAccNo.Text); // Commented by abhishek for Accno passing null while lein details 31-10-2017

                        dt = DDS.getDetails(Session["BRCD"].ToString(), TxtAGCD.Text, custnob[1].ToString());
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
            Name_Change(CUNAME, TxtAGCD.Text == "" ? "NCODE" : "YCODE");
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
                //if (ValidLien == true)
                //{
                //    if (ViewState["PGL"].ToString() != "3")
                //    {
                //        WebMsgBox.Show("Daily account is lien 
                //    }
                //}
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
                    //Added By Amol on 28/09/2017 for transfer amount to loan
                    if (Convert.ToInt32(ViewState["PGL"].ToString() == "" ? "0" : ViewState["PGL"].ToString()) == 3)
                    {
                        divLoan.Visible = true;
                    }
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
                    //Added By Amol on 28/09/2017 for transfer amount to loan
                    if (Convert.ToInt32(ViewState["PGL"].ToString() == "" ? "0" : ViewState["PGL"].ToString()) == 3)
                    {
                        divLoan.Visible = true;
                    }
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
            AC_Status = CC.GetAccStatus(Session["BRCD"].ToString(), TxtAGCD.Text, TxtAccNo.Text);
            if (AC_Status == "1" || AC_Status == "4")
            {
                AT = BD.Getstage1(TxtTAccNo.Text, Session["BRCD"].ToString(), TxtPType.Text);
                if (AT != null)
                {
                    if (AT != "1003")
                    {
                        lblMessage.Text = "Sorry Customer not Authorise...!!";
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

                            //Added By Amol on 28/09/2017 for transfer amount to loan
                            if (Convert.ToInt32(ViewState["PGL"].ToString() == "" ? "0" : ViewState["PGL"].ToString()) == 3)
                            {
                                dt = DDS.GetLoanTotalAmount(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), TxtTAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "LoanInst");

                                if (dt.Rows.Count > 0)
                                {
                                    TotalAmt = Math.Round(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) + Convert.ToDouble(dt.Rows[0]["Interest"].ToString() == "" ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["PInterest"].ToString() == "" ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString() == "" ? "0" : dt.Rows[0]["InterestRec"].ToString()) + Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString())));
                                    txtLoanTotAmt.Text = TotalAmt.ToString();
                                    ViewState["LoanTotAmt"] = TotalAmt.ToString();
                                    TxtTrfNarration.Focus();
                                    return;
                                    //}
                                }
                                else
                                {
                                    txtLoanTotAmt.Text = "0";
                                    ViewState["LoanTotAmt"] = Convert.ToDouble(txtLoanTotAmt.Text.Trim().ToString());
                                }
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
            //else if (AC_Status == "4")
            //{
            //    lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Lean Marked / Loan Advanced .........!!";
            //    ModalPopup.Show(this.Page);
            //    ClearData();
            //}
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

                //Added By Amol on 28/09/2017 for transfer amount to loan
                if (Convert.ToInt32(ViewState["PGL"].ToString() == "" ? "0" : ViewState["PGL"].ToString()) == 3)
                {
                    dt = DDS.GetLoanTotalAmount(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), TxtTAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "LoanInst");

                    if (dt.Rows.Count > 0)
                    {
                        TotalAmt = Math.Round(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) + Convert.ToDouble(dt.Rows[0]["Interest"].ToString() == "" ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["PInterest"].ToString() == "" ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString() == "" ? "0" : dt.Rows[0]["InterestRec"].ToString()) + Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString())));
                        txtLoanTotAmt.Text = TotalAmt.ToString();
                        ViewState["LoanTotAmt"] = TotalAmt.ToString();
                        TxtTrfNarration.Focus();
                        return;
                        //}
                    }
                    else
                    {
                        txtLoanTotAmt.Text = "0";
                        ViewState["LoanTotAmt"] = Convert.ToDouble(txtLoanTotAmt.Text.Trim().ToString());
                    }
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

    public void GetGst_Details()
    {
        try
        {
            DataTable GST_DT = new DataTable();
            GST_DT = CC.GetGSTDetails("1", ViewState["GL"].ToString());

            if (ddlActivity.SelectedValue == "2")
            {
                if (GST_DT == null)
                {
                    WebMsgBox.Show("Add CGST and SGST Code...!", this.Page);
                }
                else
                {
                    ViewState["RATE_CGST"] = GST_DT.Rows[0]["CGST"].ToString();
                    ViewState["RATE_SGST"] = GST_DT.Rows[0]["SGST"].ToString();
                    ViewState["PRD_CGST"] = GST_DT.Rows[0]["CGSTPRDCD"].ToString();
                    ViewState["PRD_SGST"] = GST_DT.Rows[0]["SGSTPRDCD"].ToString();
                    ViewState["RATE_GST"] = GST_DT.Rows[0]["GST"].ToString();
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
        TxtAGCD.Text = "";
        TxtAGName.Text = "";
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
        TxtAGCD.Focus();
        Txtcustno.Text = "";
        TxtDailyAmt.Text = "";
        TxtGST.Text = "";
        TxtLoanCustName.Text = "";
        TxtLoanSubglcode.Text = "";
        TxtLoanAccno.Text = "";
        TxtLoanSubglName.Text = "";
        txtLoanTotAmt.Text = "";
        txtDays.Text = "";
        txtLoanInt.Text = "";
        TxtLastIntDate.Text = "";
        txtLoanBal.Text = "";
    }

    protected void PostEntry_Click(object sender, EventArgs e)
    {
        try
        {
            sResult = LINST.PostEntry(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), "DDSCL");
            //sResult = LINST.PostEntry(Session["MID"].ToString(), TxtAccType.Text, TxtAccNo.Text, Session["EntryDate"].ToString(), Session["BRCD"].ToString(), ViewState["ST"].ToString(), "DDSCL");//FLAG ADDED for PAYMAST --ABHISHEK

            string[] SetNo = sResult.Split('_');
            if (Convert.ToDouble(SetNo[0].ToString()) > 0)
            {
                if (TxtLoanSubglcode.Text != "")
                {
                    int resultint = DDS.CloseLoanAcc(Session["BRCD"].ToString(), TxtLoanSubglcode.Text.Trim().ToString(), TxtLoanAccno.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    ValidLien = false;
                }

                string ParaUdt = string.IsNullOrEmpty(CC.GetUniversalPara("DDSUPDATE_OPDATE")) ? "0" : CC.GetUniversalPara("DDSUPDATE_OPDATE");

                int RC = DDS.CloseDDS(TxtAGCD.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), rdbpart.Checked, ParaUdt);
                if (RC > 0)
                {
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
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSCloser_Post_entry_" + TxtAGCD + "_" + TxtAccNo + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;
                }
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
            resultout = LINST.GetInfo(GrdEntryDate, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAGName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AGNAME = TxtAGName.Text;
            string[] AgentCode = AGNAME.Split('_');
            if (AgentCode.Length > 1)
            {
                //Added By Amol on 22092017 as per ambika mam instruction
                if (BD.GetProdOperate(Session["BRCD"].ToString(), AgentCode[1].ToString()).ToString() != "3")
                {
                    TxtAGCD.Text = AgentCode[0].ToString();
                    string GC = BD.GetAccTypeGL(TxtAGCD.Text, Session["BRCD"].ToString());
                    if (GC != null)
                    {
                        string[] GCA = GC.Split('_');
                        ViewState["GL"] = GCA[1].ToString();
                        TxtAGName.Text = (string.IsNullOrEmpty(AgentCode[2].ToString()) ? "" : AgentCode[1].ToString());

                        AutoAcc.ContextKey = Session["BRCD"].ToString() + "-" + TxtAGCD.Text;
                        TxtAccNo.Focus();
                    }
                    else
                    {
                        WebMsgBox.Show("Invalid Glcode....!", this.Page);
                        TxtAGCD.Text = "";
                        TxtAGCD.Focus();
                    }

                }
                else
                {
                    TxtAGCD.Text = "";
                    TxtAGName.Text = "";
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
                GetGst_Details();



                string ParaOTHERCHGGST = CC.GetUniversalPara("GSTON_OTHERCHG");
                string ParaCOMMI = CC.GetUniversalPara("GSTON_COMMI");

                if (ParaOTHERCHGGST == "Y" && ParaCOMMI == "N")
                {
                    GST = Convert.ToDouble(TxtServCHRS.Text);
                }
                else if (ParaOTHERCHGGST == "N" && ParaCOMMI == "Y")
                {
                    GST = Convert.ToDouble(TxtPreMCAMT.Text);
                }
                else
                {
                    GST = Convert.ToDouble(TxtPreMCAMT.Text) + Convert.ToDouble(TxtServCHRS.Text);
                }

                //GST = Math.Round(GST * Convert.ToDouble(ViewState["RATE_GST"]) / 100, 0);

                double CgstAmt = 0, SgstAmt = 0;
                CgstAmt = Math.Round(GST * Convert.ToDouble(ViewState["RATE_CGST"].ToString()) / 100, 2);
                SgstAmt = Math.Round(GST * Convert.ToDouble(ViewState["RATE_SGST"].ToString()) / 100, 2);

                TxtGST.Text = (CgstAmt + SgstAmt).ToString();

                ViewState["CGST_AMT"] = CgstAmt.ToString();
                ViewState["SGST_AMT"] = SgstAmt.ToString();


                AMT = ((CLBAL + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) + Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text)));
            }
            else
            {
                AMT = ((CLBAL + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text)));
                TxtGST.Text = "0";
            }

            TxtPayAmt.Text = AMT.ToString();
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

                GetGst_Details();


                string ParaOTHERCHGGST = CC.GetUniversalPara("GSTON_OTHERCHG");
                string ParaCOMMI = CC.GetUniversalPara("GSTON_COMMI");

                if (ParaOTHERCHGGST == "Y" && ParaCOMMI == "N")
                {
                    GST = Convert.ToDouble(TxtServCHRS.Text);
                }
                else if (ParaOTHERCHGGST == "N" && ParaCOMMI == "Y")
                {
                    GST = Convert.ToDouble(TxtPreMCAMT.Text);
                }
                else
                {
                    GST = Convert.ToDouble(TxtPreMCAMT.Text) + Convert.ToDouble(TxtServCHRS.Text);
                }

                //GST = Math.Round(GST * Convert.ToDouble(ViewState["RATE_GST"]) / 100, 0);

                double CgstAmt = 0, SgstAmt = 0;
                CgstAmt = Math.Round(GST * Convert.ToDouble(ViewState["RATE_CGST"].ToString()) / 100, 2);
                SgstAmt = Math.Round(GST * Convert.ToDouble(ViewState["RATE_SGST"].ToString()) / 100, 2);

                TxtGST.Text = (CgstAmt + SgstAmt).ToString();

                ViewState["CGST_AMT"] = CgstAmt.ToString();
                ViewState["SGST_AMT"] = SgstAmt.ToString();


                AMT = ((CLBAL + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) + Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text)));

            }
            else
            {
                AMT = ((CLBAL + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text)));
            }
            TxtPayAmt.Text = AMT.ToString();
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
            TxtPayAmt.Text = AMT.ToString();
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
            if ((txtTotLoanBal.Text == "" ? 0 : Convert.ToDouble(txtTotLoanBal.Text)) > 0)
            {
                WebMsgBox.Show("First Closure the Loan,Do Post Int...!", this.Page);
                rdbfull.Checked = false;
                rdbpart.Checked = false;
                return;
            }
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
        if ((txtTotLoanBal.Text == "" ? 0 : Convert.ToDouble(txtTotLoanBal.Text)) > 0)
        {
            WebMsgBox.Show("First Closure the Loan,Do Post Int...!", this.Page);
            rdbfull.Checked = false;
            rdbpart.Checked = false;
            return;
        }
        Txtpartpay.Enabled = true;
        Txtpartpay.Focus();
    }
    protected void GridCalcu_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Font.Bold = true;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Amt = string.IsNullOrEmpty(((Label)e.Row.FindControl("INTR_AMT")).Text) ? "0" : ((Label)e.Row.FindControl("INTR_AMT")).Text;

            TotalValue = Convert.ToDouble(Amt);
            SumFooterValue += TotalValue;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("LblIntSum");
            lbl.Text = SumFooterValue.ToString();

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

            double AMT = 0;
            if (ViewState["RevProvi"].ToString() != "N")
            {
                AMT = ((CLBAL + (Convert.ToDouble(Txt_Provi.Text == "" ? "0" : Txt_Provi.Text)) + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) + Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text)));
            }
            else
            {
                AMT = ((CLBAL + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) + Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text)));
            }



            //  double AMT = ((CLBAL + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text)));
            TxtPayAmt.Text = AMT.ToString();
            TxtPreMC.Focus();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    //protected void GridDeposite_PageIndexChanging(object sender, GridViewPageEventArgs e)//amruta 26/04/2017
    //{
    //    GridDeposite.PageIndex = e.NewPageIndex;
    //}
    protected void Btn_Printsummary_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSCloser_Print_Summry_" + TxtAGCD + "_" + TxtAccNo + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&GLC=2&SUBGL=" + TxtAGCD.Text + "&ACCNO=" + TxtAccNo.Text + "&rptname=RptDDSCMonthlySummary.rdlc";
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

            txtProd.Text = TxtAGCD.Text;
            txtProdCode.Text = TxtAGName.Text;
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
                                Img1.Src = "data:image/tif;base64," + base64String;
                            }
                            else if (y == 1)
                            {
                                Img2.Src = "data:image/tif;base64," + base64String;
                            }

                            //File.Delete(output);
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

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('.bs-example-modal-lg').modal('show');</script>", false);
            }
            else
            {
                Img1.Src = "";
                
                Img2.Src ="";
                //WebMsgBox.Show("Sorry, No record Found !!!!", this.Page);
                WebMsgBox.Show("Photo signature could not be found !!!!", this.Page);
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
            {
                if (ViewState["PGL"].ToString() == "3")
                    txtLoanTotAmt.Focus();
                else
                    BtnCashPost.Focus();
            }
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
            CurrentCls.Getinfotable(grdCashRct, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "DDSCLOSE");
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
            //double CLBAL = 0;

            //GetGst_Details();
            //double GST = 0;

            //string ParaOTHERCHGGST = CC.GetUniversalPara("GSTON_OTHERCHG");
            //string ParaCOMMI = CC.GetUniversalPara("GSTON_COMMI");

            //if (ParaOTHERCHGGST == "Y" && ParaCOMMI == "N")
            //{
            //    GST = Convert.ToDouble(TxtServCHRS.Text);
            //}
            //else if (ParaOTHERCHGGST == "N" && ParaCOMMI == "Y")
            //{
            //    GST = Convert.ToDouble(TxtPreMCAMT.Text);
            //}
            //else
            //{
            //    GST = Convert.ToDouble(TxtPreMCAMT.Text) + Convert.ToDouble(TxtServCHRS.Text);
            //}

            ////GST = Math.Round(GST * Convert.ToDouble(ViewState["RATE_GST"]) / 100, 0);

            //double CgstAmt = 0, SgstAmt = 0;
            //CgstAmt = Math.Round(GST * Convert.ToDouble(ViewState["RATE_CGST"].ToString()) / 100, 2);
            //SgstAmt = Math.Round(GST * Convert.ToDouble(ViewState["RATE_SGST"].ToString()) / 100, 2);

            //TxtGST.Text = (CgstAmt + SgstAmt).ToString();

            //ViewState["CGST_AMT"] = CgstAmt.ToString();
            //ViewState["SGST_AMT"] = SgstAmt.ToString();


            double AMT = ((Convert.ToDouble(Txtpartpay.Text == "" ? "0" : Txtpartpay.Text) + (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text))) - (Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) + Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text)));
            TxtPayAmt.Text = Math.Round(AMT, 0).ToString();


            ddlPayMode.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #region Loan Amount Transfer

    protected void txtLoanTotAmt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Math.Abs(Convert.ToDouble(txtLoanTotAmt.Text.Trim().ToString())) > Math.Abs(Convert.ToDouble(ViewState["LoanTotAmt"].ToString())))
            {
                txtLoanTotAmt.Text = ViewState["LoanTotAmt"].ToString();
                lblMessage.Text = "Excess amount of receipt than paid amount " + Convert.ToDouble(ViewState["LoanTotAmt"].ToString()).ToString() + "...!!";
                ModalPopup.Show(this.Page);
                return;
            }
            else if (Math.Abs(Convert.ToDouble(txtLoanTotAmt.Text.Trim().ToString())) > Math.Abs(Convert.ToDouble(TxtPayAmt.Text.Trim().ToString() == "" ? "0" : TxtPayAmt.Text.Trim().ToString())))
            {
                txtLoanTotAmt.Text = ViewState["LoanTotAmt"].ToString();
                lblMessage.Text = "Excess amount of receipt than total payable amount " + Convert.ToDouble(TxtPayAmt.Text.Trim().ToString() == "" ? "0" : TxtPayAmt.Text.Trim().ToString()).ToString() + "...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void PaymentAmount()
    {
        try
        {
            ViewState["PayableAmt"] = Convert.ToDouble(ViewState["PayableAmt"].ToString() == "" ? "0" : ViewState["PayableAmt"].ToString()) + Convert.ToDouble(txtLoanTotAmt.Text.Trim().ToString() == "" ? "0" : txtLoanTotAmt.Text.Trim().ToString());

            if (Convert.ToInt32(ViewState["PGL"].ToString()) == 3)
                TxtPayAmt.Text = Convert.ToDouble(Convert.ToDouble(TxtPayAmt.Text.Trim().ToString()) - Convert.ToDouble(txtLoanTotAmt.Text.Trim().ToString())).ToString();
            else
                TxtPayAmt.Text = Convert.ToDouble(Convert.ToDouble(TxtPayAmt.Text.Trim().ToString()) - Convert.ToDouble(TxtPayAmt.Text.Trim().ToString())).ToString();

            if (Convert.ToDouble(TxtPayAmt.Text.Trim().ToString()) == 0.00)
            {
                ddlPayMode.SelectedValue = "0";
                PostEntry.Visible = true;
                BtnCashPost.Visible = false;
                DivTransfer.Visible = false;
                DIV_INSTRUMENT.Visible = false;
            }
            else
            {
                ddlPayMode.SelectedValue = "2";
                divLoan.Visible = false;
                PostEntry.Visible = false;
                BtnCashPost.Visible = true;
                DivTransfer.Visible = true;
                DIV_INSTRUMENT.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearTransfer()
    {
        try
        {
            TxtPType.Text = "";
            TxtPTName.Text = "";
            TxtTAccNo.Text = "";
            TxtTAName.Text = "";
            TxtTrfNarration.Text = "";
            txtLoanTotAmt.Text = "";
            TxtInstNo.Text = "";
            TxtInstDate.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion


    protected void Btn_AccountStatement_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtAGCD.Text == "")
            {
                WebMsgBox.Show("Enter Product code.....!", this.Page);
                TxtAGCD.Focus();
            }
            else if (TxtAccNo.Text == "")
            {
                WebMsgBox.Show("Enter Account number.....!", this.Page);
                TxtAccNo.Focus();

            }
            else if (TxtOpeningDate.Text == "")
            {
                WebMsgBox.Show("Deposit Date is blank not allowed.....!", this.Page);
                TxtAccNo.Focus();
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
            DT = SV.GetStatementView(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), TxtAGCD.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), TxtOpeningDate.Text.ToString(), Session["EntryDate"].ToString());
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
    protected void TxtInstDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime temp;
            if (DateTime.TryParseExact(TxtInstDate.Text, "dd/MM/yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out temp))
            {
                string STR = CC.InstMonthDiff(Session["EntryDate"].ToString(), TxtInstDate.Text);
                if (Convert.ToInt32(STR) >= 3)
                {
                    lblMessage.Text = "";
                    lblMessage.Text = "Cheque no. " + TxtInstNo.Text + " date validity expired....!";
                    ModalPopup.Show(this.Page);
                    TxtInstDate.Text = "";
                    return;
                }
                else if (Convert.ToDateTime(conn.ConvertDate(TxtInstDate.Text)) > Convert.ToDateTime(conn.ConvertDate(Session["EntryDate"].ToString())))
                {
                    WebMsgBox.Show("Invalid Instrument date,Future date not accepted....!", this.Page);
                    TxtInstDate.Text = "";
                    TxtInstDate.Focus();
                    return;
                }
                else
                    BtnCashPost.Focus();

            }
            else
            {
                WebMsgBox.Show("Invalid Instrument date....!", this.Page);
                TxtInstDate.Text = "";
                TxtInstDate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnLoanInt_Click(object sender, EventArgs e)
    {
        try
        {
            if ((TxtCBal.Text == "" ? 0 : Convert.ToDouble(TxtCBal.Text)) < (txtTotLoanBal.Text == "" ? 0 : Convert.ToDouble(txtTotLoanBal.Text)))
            {
                WebMsgBox.Show("DDS Account Balance is low to Close Loan (Total = " + txtTotLoanBal.Text + " ,Cannot close loan...!", this.Page);
                ClearLoanDetails();
                ClearData();
                return;
            }
            string SetNo = "";
            string glcode = "";
            string CN = "", CD = "";
            string PAYMAST = "TDCLOSE";
            int resultint = 0;

            string REFERENCEID = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
            ViewState["RID"] = (Convert.ToInt32(REFERENCEID) + 1).ToString();

            if (Convert.ToDouble(txtTotLoanBal.Text.Trim().ToString() == "" ? "0" : txtTotLoanBal.Text.Trim().ToString()) > 0)
            {
                glcode = LC.getGlCode(Session["BRCD"].ToString(), TxtAGCD.Text.Trim().ToString());
                SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();

                CN = "0";
                CD = "01/01/1990";

                result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode, TxtAGCD.Text.Trim().ToString(),
                            TxtAccNo.Text.Trim().ToString(), "DDS to Loan Closure Cr To" + TxtLoanSubglcode.Text + "/" + TxtLoanAccno.Text + " TR", "Pri Dr", txtTotLoanBal.Text.Trim().ToString(), "2", "7", "TR", SetNo, CN, CD, "0", "0", "1003",
                            "0", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", PAYMAST, "0", "", ViewState["RID"].ToString(), "0");
                if (result > 0)
                {
                    dt = new DataTable();
                    dt = LC.GETDATA(Session["BRCD"].ToString(), "3", ViewState["LSubGlCode"].ToString());

                    //principle o/s
                    result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["GLCODE"].ToString(), ViewState["LSubGlCode"].ToString(),
                    ViewState["LAccNo"].ToString(), "DDS to Loan Closure Dr From " + TxtAGCD.Text + "/" + TxtAccNo.Text + " TR", "Total Dr", txtLoanBal.Text.Trim().ToString(), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1003",
                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", PAYMAST, "0", "", ViewState["RID"].ToString(), "0");

                    if (result > 0)
                    {
                        resultint = ITrans.LoanTrx(Session["BRCD"].ToString(), ViewState["LSubGlCode"].ToString(), ViewState["LSubGlCode"].ToString(), ViewState["LAccNo"].ToString(), "1", "1", "7", "TDA Closure Principle Credited", txtLoanBal.Text.Trim().ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());

                        //interest Applied credit to acc
                        if (Convert.ToDouble(txtLoanInt.Text.Trim().ToString() == "" ? "0" : txtLoanInt.Text.Trim().ToString()) > 0)
                        {
                            result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "11", dt.Rows[0]["IR"].ToString(),
                            ViewState["LAccNo"].ToString(), "DDS to Loan Closure", "IntCr", txtLoanInt.Text.Trim().ToString(), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1003",
                            "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", PAYMAST, "0", "", ViewState["RID"].ToString(), "0");

                            if (result > 0)
                            {
                                //interest Applied Debit
                                result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "11", dt.Rows[0]["IR"].ToString(),
                                ViewState["LAccNo"].ToString(), "DDS to Loan Closure", "IntDr Contra", txtLoanInt.Text.Trim().ToString(), "2", "10", "TR_INT", SetNo, "0", "01/01/1900", "0", "0", "1003",
                                "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", PAYMAST, "0", "", ViewState["RID"].ToString(), "0");

                                if (result > 0)
                                {
                                    //interest Applied Credit to GL 100
                                    result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", dt.Rows[0]["PLACCNO"].ToString(),
                                    ViewState["LAccNo"].ToString(), "TR", "Intcr Contra", txtLoanInt.Text.Trim().ToString(), "1", "10", "TR_INT", SetNo, "0", "01/01/1900", "0", "0", "1003",
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


                                ClearLoanDetails();
                                Accno();
                                divLoan.Visible = false;
                                lblMessage.Text = "Loan Close with SetNo : " + Convert.ToInt32(SetNo);
                                ModalPopup.Show(this.Page);
                                FL = "Insert";//Dhanya Shetty
                                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DepCloser_LoanClose_" + TxtAGCD.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                divLoan.Visible = false;
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
    public void ClearLoanDetails()
    {
        TxtLoanSubglcode.Text = "";
        TxtLoanSubglName.Text = "";
        TxtLoanAccno.Text = "";
        TxtLoanCustName.Text = "";
        txtDays.Text = "";
        txtLoanInt.Text = "";
        txtLoanBal.Text = "";
        txtLoanTotAmt.Text = "";
        txtLoanBal.Text = "";
        txtSancAmount.Text = "";
        TxtLastIntDate.Text = "";
        txtTotLoanBal.Text = "";
    }

    protected void BtnCashPost_Click(object sender, EventArgs e)
    {
        try
        {
            //  Added By amol on 05/05/2018 for clear temporary table records
            string Para = CC.GetUniversalPara("DDS_CLRTEMP");
            if (Para != null && Para == "Y")
                DDS.DelAllRecTable(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

            if (ViewState["GSTYN"].ToString() == "Y") // If GST amount is Changed by User on 21-03-2018
            {
                if (TxtGST.Text != "" && TxtGST.Text != "0")
                {
                    ViewState["CGST_AMT"] = Convert.ToDouble(TxtGST.Text) / 2;
                    ViewState["SGST_AMT"] = Convert.ToDouble(TxtGST.Text) / 2;
                }
            }
            string Para_Deduct = "";

            double BAL1 = 0;
            Para_Deduct = DDS.GetDDSCashDeduct();

            if (rdbfull.Checked == false && rdbpart.Checked == false)
            {
                WebMsgBox.Show("Select Payament Type....!", this.Page);
                return;
            }

            if (ddlPayMode.SelectedValue == "0")
            {
                WebMsgBox.Show("Select Payament Mode....!", this.Page);
                return;
            }

            if (Para_Deduct == null)
            {
                lblMessage.Text = "Add 'COMMDEDCASH' Parameter to 'Y' or 'N' for Post Premature Entries.";
                ModalPopup.Show(this.Page);
                return;
            }
            if (TxtInstDate.Text == "" && ddlPayMode.SelectedValue == "3")
            {
                return;
            }
            if (ViewState["PGL"].ToString() == "0" && ddlPayMode.SelectedValue != "1")
            {
                string Pgl = DDS.Getaccno(Session["BRCD"].ToString(), TxtPType.Text);
                string[] pgl1 = Pgl.Split('_');
                ViewState["PGL"] = Pgl[0].ToString();
            }

            GridCalcu.Visible = false;
            #region Loan Entries (Amol)
            //Added By Amol on 28/09/2017 for transfer amount to loan
            if (Convert.ToInt32(ViewState["PGL"].ToString() == "" ? "0" : ViewState["PGL"].ToString()) == 3)
            {
                //Added for loan case
                DataTable dt = new DataTable();
                dt = DDS.GetLoanTotalAmount(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), TxtTAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString());

                TotalAmt = Math.Round(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) + Convert.ToDouble(dt.Rows[0]["Interest"].ToString() == "" ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["PInterest"].ToString() == "" ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString() == "" ? "0" : dt.Rows[0]["InterestRec"].ToString()) + Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString() == "" ? "0" : dt.Rows[0]["SurChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) + Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString())));

                if (Convert.ToDouble(TxtPayAmt.Text.Trim().ToString() == "" ? "0" : TxtPayAmt.Text.Trim().ToString()) < Math.Abs(TotalAmt))
                {
                    TotalDrAmt = Convert.ToDouble(txtLoanTotAmt.Text.Trim().ToString() == "" ? "0" : txtLoanTotAmt.Text.Trim().ToString());

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
                            SurTotal = Math.Round(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) < 0 ? "0" : dt.Rows[0]["InterestRec"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Principle"].ToString()) < 0 ? "0" : dt.Rows[0]["Principle"].ToString()));

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

                        OtherChrg = Math.Round(Convert.ToDouble(((Math.Abs(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Principle"].ToString()) < 0 ? "0" : dt.Rows[0]["Principle"].ToString())) * Convert.ToDouble(LINST.GetOtherIntRate(Session["BRCD"].ToString(), dt.Rows[0]["SubGlCode"].ToString()))) * (Convert.ToInt32(dt.Rows[0]["Days"].ToString()))) / 36500));

                        if (OtherChrg == null || OtherChrg.ToString() == "")
                            OtherChrg = 0;
                        else
                            TotalAmt = TotalAmt + OtherChrg;

                        // For DDS (Debit to DDS Account if payable amount is zero)
                        if (Convert.ToDouble(Convert.ToDouble(TxtPayAmt.Text.Trim().ToString()) - Convert.ToDouble(txtLoanTotAmt.Text.Trim().ToString())) == 0.00)
                        {
                            DataTable DT = new DataTable();
                            DT = LINST.GetDDSInfo("2");

                            if (ddlActivity.SelectedValue == "2")  //	For Premature Closure
                            {
                                if (Convert.ToInt32(TxtServCHRS.Text.Trim().ToString() == "" ? "0" : TxtServCHRS.Text.Trim().ToString()) > 0)
                                {
                                    resultout = LINST.InsertBatch("100", DT.Rows[0]["serv_acc"].ToString(), TxtAccNo.Text, "", ViewState["CT"].ToString(), TxtServCHRS.Text.Trim().ToString(), "1", "7", "TR", "SERVICE CHG " + TxtAGCD.Text + "-" + TxtAccNo.Text + " ", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                                }
                                if (Convert.ToInt32(TxtPreMCAMT.Text.Trim().ToString() == "" ? "0" : TxtPreMCAMT.Text.Trim().ToString()) > 0)
                                {
                                    resultout = LINST.InsertBatch("100", DT.Rows[0]["comm_Accno"].ToString(), TxtAccNo.Text, "", ViewState["CT"].ToString(), TxtPreMCAMT.Text.Trim().ToString(), "1", "7", "TR", "COMMISSION CHG " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                                }
                                if (Convert.ToInt32(TxtGST.Text.Trim().ToString() == "" ? "0" : TxtGST.Text.Trim().ToString()) > 0)
                                {
                                    string GLCODE = BD.GetAccTypeGL(DT.Rows[0]["GST_ACC"].ToString(), Session["BRCD"].ToString());
                                    string[] GLC = GLCODE.Split('_');
                                    resultout = LINST.InsertBatch(GLC[1].ToString(), DT.Rows[0]["GST_ACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), TxtGST.Text.Trim().ToString(), "1", "7", "TR", "GST Dr From " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                                }
                                AMT = Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) + Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text);

                                BAL1 = Convert.ToDouble(TxtCBal.Text);
                                if (rdbpart.Checked == true)
                                {
                                    BAL1 = Convert.ToDouble(Txtpartpay.Text.Trim().ToString());
                                }

                                if (BAL1 > 0)
                                {
                                    resultout = LINST.InsertBatch("2", TxtAGCD.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), BAL1.ToString(), "2", "7", "TR", "TRF CR TO " + TxtPType.Text + "-" + TxtTAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                                }
                            }
                            else if (ddlActivity.SelectedValue == "3")  // For Mature Closure
                            {
                                if (Convert.ToInt32(TxtCalcInt.Text.Trim().ToString() == "" ? "0" : TxtCalcInt.Text.Trim().ToString()) > 0)
                                {
                                    resultout = LINST.InsertBatch("2", TxtAGCD.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), TxtCalcInt.Text.Trim().ToString(), "1", "10", "TR-INT", "INT TRF FROM " + ViewState["IR"].ToString() + "-" + TxtAccNo.Text + "", "MATURE ACC CLOSURE " + TxtTrfNarration.Text + "", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                                }
                                if (Txt_Provi != null)
                                    PLAMT = Convert.ToDouble(TxtINTC.Text);

                                if (PLAMT > 0)
                                {
                                    resultout = LINST.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), PLAMT.ToString(), "2", "10", "TR-INT", "INT Dr TO " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                                }
                                else
                                {
                                    PLAMT = Math.Abs(PLAMT);
                                    resultout = LINST.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), PLAMT.ToString(), "1", "10", "TR-INT", "INT Cr TO " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                                }

                                if (Convert.ToDouble(Txt_Provi.Text.Trim().ToString() == "" ? "0" : Txt_Provi.Text.Trim().ToString()) > 0)
                                {
                                    resultout = LINST.InsertBatch("22", ViewState["IR"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Txt_Provi.Text.ToString(), "2", "10", "TR-INT", "PROVISION Dr to " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                                }
                                if (resultout > 0)
                                {
                                    BAL1 = Convert.ToDouble(ViewState["PayableAmt"].ToString() == "" ? "0" : ViewState["PayableAmt"].ToString()) + Convert.ToDouble(TxtPayAmt.Text.Trim().ToString() == "" ? "0" : TxtPayAmt.Text.Trim().ToString()); ;
                                    //BAL1 = Convert.ToDouble(TxtPayAmt.Text.Trim().ToString() == "" ? "0" : TxtPayAmt.Text.Trim().ToString());
                                    if (BAL1 > 0)
                                    {
                                        resultout = LINST.InsertBatch("2", TxtAGCD.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), BAL1.ToString(), "2", "7", "TR", "TRF Cr TO " + TxtPType.Text + "-" + TxtTAccNo.Text + "", ddlActivity.SelectedItem.Text + "-" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                                    }
                                }
                            }
                        }

                        //  For Loan (Credit to loan account)
                        if (dt.Rows[0]["IntCalType"].ToString() == "1" || dt.Rows[0]["IntCalType"].ToString() == "2")
                        {
                            resultout = 1;
                            //  For Insurance Charge
                            if (resultout > 0)
                            {
                                if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()))
                                {
                                    resultout = LINST.InsertBatch(dt.Rows[0]["InsChrgGl"].ToString(), dt.Rows[0]["InsChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), dt.Rows[0]["InsChrg"].ToString(), "1", "7", "TR", "INSCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()) > 0)
                                        {
                                            //  Insurance Charge Credit To 11 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["InsChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "11", "1", "7", "Insurance Charge Credit", dt.Rows[0]["InsChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }
                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()));
                                }
                                else if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                {
                                    resultout = LINST.InsertBatch(dt.Rows[0]["InsChrgGl"].ToString(), dt.Rows[0]["InsChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "INSCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()) > 0)
                                        {
                                            //  Insurance Charge Credit To 11 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["InsChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "11", "1", "7", "Insurance Charge Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
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
                                    resultout = LINST.InsertBatch(dt.Rows[0]["BankChrgGl"].ToString(), dt.Rows[0]["BankChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), dt.Rows[0]["BankChrg"].ToString(), "1", "7", "TR", "BNKCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) > 0)
                                        {
                                            // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["BankChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "10", "1", "7", "Bank Charges Credit", dt.Rows[0]["BankChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }
                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()));
                                }
                                else if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) > 0 && TotalDrAmt > 0 && TotalDrAmt > 0)
                                {
                                    resultout = LINST.InsertBatch(dt.Rows[0]["BankChrgGl"].ToString(), dt.Rows[0]["BankChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "BNKCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) > 0)
                                        {
                                            // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["BankChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "10", "1", "7", "Bank Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }
                                    TotalDrAmt = 0;
                                }
                            }

                            //  For Other Charges
                            if (resultout > 0)
                            {
                                if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0 && TotalDrAmt >= (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg))
                                {
                                    resultout = LINST.InsertBatch(dt.Rows[0]["OtherChrgGl"].ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg).ToString(), "1", "7", "TR", "OTHCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                    if (resultout > 0)
                                    {
                                        if (OtherChrg > 0)
                                        {
                                            // Other Charges Amt Debit To 9 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "9", "2", "7", "Other Charges Debit", OtherChrg.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }
                                    if (resultout > 0)
                                    {
                                        if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0)
                                        {
                                            // Other Charges Amt Credit To 9 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "9", "1", "7", "Other Charges Credit", Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }
                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg));
                                }
                                else if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0 && TotalDrAmt > 0)
                                {
                                    resultout = LINST.InsertBatch(dt.Rows[0]["OtherChrgGl"].ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "OTHCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                    if (resultout > 0)
                                    {
                                        if (OtherChrg > 0)
                                        {
                                            // Other Charges Amt Debit To 9 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "9", "2", "7", "Other Charges Debit", OtherChrg.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }
                                    if (resultout > 0)
                                    {
                                        if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0)
                                        {
                                            // Other Charges Amt Credit To 9 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "9", "1", "7", "Other Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }
                                    TotalDrAmt = 0;
                                }
                            }

                            //  For Sur Charges
                            if (resultout > 0)
                            {
                                if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0 && TotalDrAmt >= (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg))
                                {
                                    resultout = LINST.InsertBatch(dt.Rows[0]["SurChrgGl"].ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), (Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg).ToString(), "1", "7", "TR", "SURCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                    if (resultout > 0)
                                    {
                                        if (CurSurChrg > 0)
                                        {
                                            // Sur Charges Debit To 8 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "8", "2", "7", "Sur Charges Debit", CurSurChrg.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                        if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0)
                                        {
                                            // Sur Charges Credit To 8 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "8", "1", "7", "Sur Charges Credit", Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }
                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg));
                                }
                                else if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0 && TotalDrAmt > 0)
                                {
                                    resultout = LINST.InsertBatch(dt.Rows[0]["SurChrgGl"].ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "SURCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                    if (resultout > 0)
                                    {
                                        if (CurSurChrg > 0)
                                        {
                                            // Sur Charges Debit To 8 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "8", "2", "7", "Sur Charges Debit", CurSurChrg.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                        if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0)
                                        {
                                            // Sur Charges Credit To 8 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "8", "1", "7", "Sur Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
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
                                    resultout = LINST.InsertBatch(dt.Rows[0]["CourtChrgGl"].ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), dt.Rows[0]["CourtChrg"].ToString(), "1", "7", "TR", "CRTCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) > 0)
                                        {
                                            // Court Charges Amt Credit To 7 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "7", "1", "7", "Court Charges Credit", dt.Rows[0]["CourtChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }
                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()));
                                }
                                else if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                {
                                    resultout = LINST.InsertBatch(dt.Rows[0]["CourtChrgGl"].ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "CRTCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) > 0)
                                        {
                                            // Court Charges Amt Credit To 7 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "7", "1", "7", "Court Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
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
                                    resultout = LINST.InsertBatch(dt.Rows[0]["ServiceChrgGl"].ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), dt.Rows[0]["ServiceChrg"].ToString(), "1", "7", "TR", "SERCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) > 0)
                                        {
                                            // Service Charges Amt Credit To 6 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "6", "1", "7", "Service Charges Credit", dt.Rows[0]["ServiceChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }
                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()));
                                }
                                else if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                {
                                    resultout = LINST.InsertBatch(dt.Rows[0]["ServiceChrgGl"].ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "SERCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) > 0)
                                        {
                                            // Service Charges Amt Credit To 6 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "6", "1", "7", "Service Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
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
                                    resultout = LINST.InsertBatch(dt.Rows[0]["NoticeChrgGl"].ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), dt.Rows[0]["NoticeChrg"].ToString().ToString(), "1", "7", "TR", "NOTCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) > 0)
                                        {
                                            // Notice Charges Credit To 5 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "5", "1", "7", "Notice Charges Credit", dt.Rows[0]["NoticeChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }
                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()));
                                }
                                else if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                {
                                    resultout = LINST.InsertBatch(dt.Rows[0]["NoticeChrgGl"].ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString().ToString(), "1", "7", "TR", "NOTCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) > 0)
                                        {
                                            // Notice Charges Credit To 5 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "5", "1", "7", "Notice Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
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
                                    if (dt.Rows[0]["IntCalType"].ToString() == "1")
                                    {
                                        // Interest Received credit to GL 11
                                        resultout = LINST.InsertBatch(dt.Rows[0]["InterestRecGl"].ToString(), dt.Rows[0]["InterestRecSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), dt.Rows[0]["InterestRec"].ToString().ToString(), "1", "7", "TR", "INTRCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0)
                                            {
                                                // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["InterestRecSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "4", "1", "7", "Interest Received Credit", Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                    }
                                    else if (dt.Rows[0]["IntCalType"].ToString() == "2")
                                    {
                                        // Interest Received credit to GL 3
                                        resultout = LINST.InsertBatch(ViewState["PGL"].ToString(), TxtPType.Text.Trim().ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), dt.Rows[0]["InterestRec"].ToString().ToString(), "1", "7", "TR", "INTRCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");
                                    }
                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()));
                                }
                                else if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt > 0)
                                {
                                    if (dt.Rows[0]["IntCalType"].ToString() == "1")
                                    {
                                        // Interest Received credit to GL 11
                                        resultout = LINST.InsertBatch(dt.Rows[0]["InterestRecGl"].ToString(), dt.Rows[0]["InterestRecSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString().ToString(), "1", "7", "TR", "INTRCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0)
                                            {
                                                // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["InterestRecSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "4", "1", "7", "Interest Received Credit", Convert.ToDouble(TotalDrAmt.ToString()).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                    }
                                    else if (dt.Rows[0]["IntCalType"].ToString() == "2")
                                    {
                                        // Interest Received credit to GL 3
                                        resultout = LINST.InsertBatch(ViewState["PGL"].ToString(), TxtPType.Text.Trim().ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString().ToString(), "1", "7", "TR", "INTRCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");
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
                                        resultout = LINST.InsertBatch(dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), "1", "7", "TR", "PENCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");
                                    }

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) > 0)
                                        {
                                            //Penal Interest Debit To 3 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "3", "2", "7", "Penal Interest Debit", dt.Rows[0]["CurrPInterest"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                        {
                                            //Penal Interest Credit To 3 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "3", "1", "7", "Penal Interest Credit", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }

                                    if (resultout > 0)
                                    {
                                        //Penal Charge Contra
                                        if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                        {
                                            //Penal chrg Applied Debit To GL 12
                                            resultout = LINST.InsertBatch(dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), "2", "12", "TR_INT", "PENDR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                            if (resultout > 0)
                                            {
                                                //Penal chrg Applied Credit to GL 100
                                                resultout = LINST.InsertBatch("100", dt.Rows[0]["PlAccNo2"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString().ToString(), "1", "12", "TR_INT", "PENCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");
                                            }
                                        }
                                    }

                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())));
                                }
                                else if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                {
                                    //Penal Charge Credit To GL 12
                                    resultout = LINST.InsertBatch(dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "PENCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) > 0)
                                        {
                                            //Penal Interest Debit To 3 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "3", "2", "7", "Penal Interest Debit", dt.Rows[0]["CurrPInterest"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                        {
                                            //Penal Interest Credit To 3 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "3", "1", "7", "Penal Interest Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }

                                    if (resultout > 0)
                                    {
                                        //Penal Charge Contra
                                        if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                        {
                                            //Penal chrg Applied Debit To GL 12
                                            resultout = LINST.InsertBatch(dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "2", "12", "TR_INT", "PENDR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                            if (resultout > 0)
                                            {
                                                //Penal chrg Applied Credit to GL 100
                                                resultout = LINST.InsertBatch("100", dt.Rows[0]["PlAccNo2"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "12", "TR_INT", "PENCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");
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
                                            resultout = LINST.InsertBatch(dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), (Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "1", "7", "TR", "INTCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");
                                        }

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString()) > 0)
                                            {
                                                //Current Interest Debit To 2 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "2", "2", "7", "Interest Debit", dt.Rows[0]["CurrInterest"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["Interest"].ToString() == "" ? "0" : dt.Rows[0]["Interest"].ToString()) > 0)
                                            {
                                                //Current Interest Credit To 2 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "2", "1", "7", "Interest Credit", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            //interest Applied Contra
                                            if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                            {
                                                //interest Applied Debit To GL 11
                                                resultout = LINST.InsertBatch(dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "2", "11", "TR_INT", "INTDR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                                if (resultout > 0)
                                                {
                                                    //interest Applied Credit to GL 100
                                                    resultout = LINST.InsertBatch("100", dt.Rows[0]["PlAccNo1"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "1", "11", "TR_INT", "INTCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");
                                                }
                                            }
                                        }

                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())));
                                    }
                                    else if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                    {
                                        //interest Credit to GL 11
                                        resultout = LINST.InsertBatch(dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "INTCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString()) > 0)
                                            {
                                                //Current Interest Debit To 2 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "2", "2", "7", "Interest Debit", Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                            {
                                                //Current Interest Credit To 2 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "2", "1", "7", "Interest Credit", Convert.ToDouble(TotalDrAmt.ToString()).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            //interest Applied Contra
                                            if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                            {
                                                //interest Applied Debit To GL 11
                                                resultout = LINST.InsertBatch(dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "2", "11", "TR_INT", "INTDR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                                if (resultout > 0)
                                                {
                                                    //interest Applied Credit to GL 100
                                                    resultout = LINST.InsertBatch("100", dt.Rows[0]["PlAccNo1"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "11", "TR_INT", "INTCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");
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
                                            resultout = LINST.InsertBatch(ViewState["PGL"].ToString(), TxtPType.Text.Trim().ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "1", "7", "TR", "INTCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");
                                        }

                                        //  Added As Per ambika mam Instruction 22-06-2017
                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                            {
                                                //interest Applied Debit To GL 3
                                                resultout = LINST.InsertBatch(ViewState["PGL"].ToString(), TxtPType.Text.Trim().ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "2", "11", "TR_INT", "INTDR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                                if (resultout > 0)
                                                {
                                                    //interest Applied Credit to GL 100
                                                    resultout = LINST.InsertBatch("100", dt.Rows[0]["PlAccNo1"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "1", "11", "TR_INT", "INTCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");
                                                }
                                            }
                                        }

                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())));
                                    }
                                    else if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                    {
                                        //interest Received Credit to GL 3
                                        resultout = LINST.InsertBatch(ViewState["PGL"].ToString(), TxtPType.Text.Trim().ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "INTCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                        //  Added As Per ambika mam Instruction 22-06-2017
                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                            {
                                                //interest Applied Debit To GL 3
                                                resultout = LINST.InsertBatch(ViewState["PGL"].ToString(), TxtPType.Text.Trim().ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "2", "11", "TR_INT", "INTDR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                                if (resultout > 0)
                                                {
                                                    //interest Applied Credit to GL 100
                                                    resultout = LINST.InsertBatch("100", dt.Rows[0]["PlAccNo1"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "11", "TR_INT", "INTCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");
                                                }
                                            }
                                        }

                                        TotalDrAmt = 0;
                                    }
                                }
                            }

                            //Principle O/S Credit To Specific GL (e.g 3)
                            if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()))
                            {
                                resultout = LINST.InsertBatch(ViewState["PGL"].ToString(), TxtPType.Text.Trim().ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), dt.Rows[0]["Principle"].ToString(), "1", "7", "TR", "PRNCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0)
                                    {
                                        //Current Principle Debit To 1 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), TxtPType.Text.Trim().ToString(), TxtTAccNo.Text.Trim().ToString(), "1", "2", "7", "Principle Debit", dt.Rows[0]["Principle"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0)
                                    {
                                        //Current Principle Credit To 1 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), TxtPType.Text.Trim().ToString(), TxtTAccNo.Text.Trim().ToString(), "1", "1", "7", "Principle Credit", dt.Rows[0]["Principle"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }

                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["Principle"].ToString()));
                            }
                            else if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0 && TotalDrAmt > 0)
                            {
                                resultout = LINST.InsertBatch(ViewState["PGL"].ToString(), TxtPType.Text.Trim().ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "PRNCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0)
                                    {
                                        //Current Principle Debit To 1 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), TxtPType.Text.Trim().ToString(), TxtTAccNo.Text.Trim().ToString(), "1", "2", "7", "Principle Debit", dt.Rows[0]["Principle"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0)
                                    {
                                        //Current Principle Credit To 1 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), TxtPType.Text.Trim().ToString(), TxtTAccNo.Text.Trim().ToString(), "1", "1", "7", "Principle Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }
                                }

                                TotalDrAmt = 0;
                            }
                        }
                        else if (dt.Rows[0]["IntCalType"].ToString() == "3")
                        {
                            //Principle O/S Credit To Specific GL (e.g 3)
                            resultout = LINST.InsertBatch(ViewState["PGL"].ToString(), TxtPType.Text.Trim().ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "PRNCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", "0", "01/01/1900");

                            if (resultout > 0)
                            {
                                if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0)
                                {
                                    //Current Principle Debit To 1 In AVS_LnTrx
                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), TxtPType.Text.Trim().ToString(), TxtTAccNo.Text.Trim().ToString(), "1", "2", "7", "Principle Debit", dt.Rows[0]["Principle"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                }

                                if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0)
                                {
                                    //Current Principle Credit To 1 In AVS_LnTrx
                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), TxtPType.Text.Trim().ToString(), TxtTAccNo.Text.Trim().ToString(), "1", "1", "7", "Principle Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                }

                                //TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["Principle"].ToString()));
                                PrincAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["InsChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["BankChrg"].ToString()) +
                                           Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["CourtChrg"].ToString()) +
                                           Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) < 0 ? "0" : dt.Rows[0]["InterestRec"].ToString()) +
                                           Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString())) +
                                           Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())))) < 0 ? 0 :
                                           Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["InsChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["BankChrg"].ToString()) +
                                           Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["CourtChrg"].ToString()) +
                                           Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) < 0 ? "0" : dt.Rows[0]["InterestRec"].ToString()) +
                                           Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString())) +
                                           Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString()))));
                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - PrincAmt);
                            }

                            //SetNo1 = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
                            //ViewState["SetNo1"] = SetNo1.ToString();

                            //Principle O/S Debit To Specific GL (e.g 3) And Credit to interest GL (e.g 11)
                            if (resultout > 0)
                            {
                                resultout = LINST.InsertBatch(ViewState["PGL"].ToString(), TxtPType.Text.Trim().ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "2", "7", "TR", "PRNDR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                //  For Insurance Charge
                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()))
                                    {
                                        resultout = LINST.InsertBatch(dt.Rows[0]["InsChrgGl"].ToString(), dt.Rows[0]["InsChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), dt.Rows[0]["InsChrg"].ToString(), "1", "7", "TR", "INSCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()) > 0)
                                            {
                                                //  Insurance Charge Credit To 11 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["InsChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "11", "1", "7", "Insurance Charge Credit", dt.Rows[0]["InsChrg"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()));
                                    }
                                    else if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                    {
                                        resultout = LINST.InsertBatch(dt.Rows[0]["InsChrgGl"].ToString(), dt.Rows[0]["InsChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "INSCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString() == "" ? "0" : dt.Rows[0]["InsChrg"].ToString()) > 0)
                                            {
                                                //  Insurance Charge Credit To 11 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["InsChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "11", "1", "7", "Insurance Charge Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
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
                                        resultout = LINST.InsertBatch(dt.Rows[0]["BankChrgGl"].ToString(), dt.Rows[0]["BankChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), dt.Rows[0]["BankChrg"].ToString(), "1", "7", "TR", "BNKCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) > 0)
                                            {
                                                // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["BankChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "10", "1", "7", "Bank Charges Credit", dt.Rows[0]["BankChrg"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()));
                                    }
                                    else if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) > 0 && TotalDrAmt > 0 && TotalDrAmt > 0)
                                    {
                                        resultout = LINST.InsertBatch(dt.Rows[0]["BankChrgGl"].ToString(), dt.Rows[0]["BankChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "BNKCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()) > 0)
                                            {
                                                // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["BankChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "10", "1", "7", "Bank Charges Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = 0;
                                    }
                                }

                                //  For Other Charges
                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString() == "" ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0 && TotalDrAmt >= (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg))
                                    {
                                        resultout = LINST.InsertBatch(dt.Rows[0]["OtherChrgGl"].ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg).ToString(), "1", "7", "TR", "OTHCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (OtherChrg > 0)
                                            {
                                                // Other Charges Amt Debit To 9 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "9", "2", "7", "Other Charges Debit", OtherChrg.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        if (resultout > 0)
                                        {
                                            if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0)
                                            {
                                                // Other Charges Amt Credit To 9 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "9", "1", "7", "Other Charges Credit", Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg));
                                    }
                                    else if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0 && TotalDrAmt > 0)
                                    {
                                        resultout = LINST.InsertBatch(dt.Rows[0]["OtherChrgGl"].ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "OTHCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (OtherChrg > 0)
                                            {
                                                // Other Charges Amt Debit To 9 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "9", "2", "7", "Other Charges Debit", OtherChrg.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        if (resultout > 0)
                                        {
                                            if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0)
                                            {
                                                // Other Charges Amt Credit To 9 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "9", "1", "7", "Other Charges Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = 0;
                                    }
                                }

                                //  For Sur Charges
                                if (resultout > 0)
                                {
                                    if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0 && TotalDrAmt >= (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg))
                                    {
                                        resultout = LINST.InsertBatch(dt.Rows[0]["SurChrgGl"].ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), (Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg).ToString(), "1", "7", "TR", "SURCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (CurSurChrg > 0)
                                            {
                                                // Sur Charges Debit To 8 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "8", "2", "7", "Sur Charges Debit", CurSurChrg.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                            if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0)
                                            {
                                                // Sur Charges Credit To 8 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "8", "1", "7", "Sur Charges Credit", Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg));
                                    }
                                    else if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0 && TotalDrAmt > 0)
                                    {
                                        resultout = LINST.InsertBatch(dt.Rows[0]["SurChrgGl"].ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "SURCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (CurSurChrg > 0)
                                            {
                                                // Sur Charges Debit To 8 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "8", "2", "7", "Sur Charges Debit", CurSurChrg.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                            if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0)
                                            {
                                                // Sur Charges Credit To 8 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "8", "1", "7", "Sur Charges Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
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
                                        resultout = LINST.InsertBatch(dt.Rows[0]["CourtChrgGl"].ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), dt.Rows[0]["CourtChrg"].ToString(), "1", "7", "TR", "CRTCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) > 0)
                                            {
                                                // Court Charges Amt Credit To 7 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "7", "1", "7", "Court Charges Credit", dt.Rows[0]["CourtChrg"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()));
                                    }
                                    else if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                    {
                                        resultout = LINST.InsertBatch(dt.Rows[0]["CourtChrgGl"].ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "CRTCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString() == "" ? "0" : dt.Rows[0]["CourtChrg"].ToString()) > 0)
                                            {
                                                // Court Charges Amt Credit To 7 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "7", "1", "7", "Court Charges Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
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
                                        resultout = LINST.InsertBatch(dt.Rows[0]["ServiceChrgGl"].ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), dt.Rows[0]["ServiceChrg"].ToString(), "1", "7", "TR", "SERCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) > 0)
                                            {
                                                // Service Charges Amt Credit To 6 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "6", "1", "7", "Service Charges Credit", dt.Rows[0]["ServiceChrg"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()));
                                    }
                                    else if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                    {
                                        resultout = LINST.InsertBatch(dt.Rows[0]["ServiceChrgGl"].ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "SERCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString() == "" ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) > 0)
                                            {
                                                // Service Charges Amt Credit To 6 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "6", "1", "7", "Service Charges Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
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
                                        resultout = LINST.InsertBatch(dt.Rows[0]["NoticeChrgGl"].ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), dt.Rows[0]["NoticeChrg"].ToString(), "1", "7", "TR", "NOTCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) > 0)
                                            {
                                                // Notice Charges Credit To 5 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "5", "1", "7", "Notice Charges Credit", dt.Rows[0]["NoticeChrg"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()));
                                    }
                                    else if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                    {
                                        resultout = LINST.InsertBatch(dt.Rows[0]["NoticeChrgGl"].ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "NOTCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) > 0)
                                            {
                                                // Notice Charges Credit To 5 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "5", "1", "7", "Notice Charges Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
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
                                        // Interest Received credit to GL 11
                                        resultout = LINST.InsertBatch(dt.Rows[0]["InterestRecGl"].ToString(), dt.Rows[0]["InterestRecSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), dt.Rows[0]["InterestRec"].ToString(), "1", "7", "TR", "INTRCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0)
                                            {
                                                // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["InterestRecSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "4", "1", "7", "Interest Received Credit", Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()));
                                    }
                                    else if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt > 0)
                                    {
                                        // Interest Received credit to GL 11
                                        resultout = LINST.InsertBatch(dt.Rows[0]["InterestRecGl"].ToString(), dt.Rows[0]["InterestRecSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "INTRCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0)
                                            {
                                                // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["InterestRecSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "4", "1", "7", "Interest Received Credit", Convert.ToDouble(TotalDrAmt.ToString()).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
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
                                            resultout = LINST.InsertBatch(dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), "1", "7", "TR", "PENCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");
                                        }

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) > 0)
                                            {
                                                //Penal Interest Debit To 3 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "3", "2", "7", "Penal Interest Debit", dt.Rows[0]["CurrPInterest"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                            {
                                                //Penal Interest Credit To 3 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "3", "1", "7", "Penal Interest Credit", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            //Penal Charge Contra
                                            if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                            {
                                                //Penal chrg Applied Debit To GL 12
                                                resultout = LINST.InsertBatch(dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), "2", "12", "TR_INT", "PENDR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                                if (resultout > 0)
                                                {
                                                    //Penal chrg Applied Credit to GL 100
                                                    resultout = LINST.InsertBatch("100", dt.Rows[0]["PlAccNo2"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), "1", "12", "TR_INT", "PENCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");
                                                }
                                            }
                                        }

                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())));
                                    }
                                    else if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                    {
                                        //Penal Charge Credit To GL 12
                                        resultout = LINST.InsertBatch(dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "PENCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) > 0)
                                            {
                                                //Penal Interest Debit To 3 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "3", "2", "7", "Penal Interest Debit", dt.Rows[0]["CurrPInterest"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                            {
                                                //Penal Interest Credit To 3 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "3", "1", "7", "Penal Interest Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            //Penal Charge Contra
                                            if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                            {
                                                //Penal chrg Applied Debit To GL 12
                                                resultout = LINST.InsertBatch(dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "2", "12", "TR_INT", "PENDR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                                if (resultout > 0)
                                                {
                                                    //Penal chrg Applied Credit to GL 100
                                                    resultout = LINST.InsertBatch("100", dt.Rows[0]["PlAccNo2"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "12", "TR_INT", "PENCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");
                                                }
                                            }
                                        }

                                        TotalDrAmt = 0;
                                    }
                                }

                                //  For Interest
                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())))
                                    {
                                        if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                        {
                                            //interest Credit to GL 11
                                            resultout = LINST.InsertBatch(dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), (Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "1", "7", "TR", "INTCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");
                                        }

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString()) > 0)
                                            {
                                                //Current Interest Debit To 2 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "2", "2", "7", "Interest Debit", dt.Rows[0]["CurrInterest"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["Interest"].ToString() == "" ? "0" : dt.Rows[0]["Interest"].ToString()) > 0)
                                            {
                                                //Current Interest Credit To 2 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "2", "1", "7", "Interest Credit", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())));
                                    }
                                    else if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                    {
                                        //interest Credit to GL 11
                                        resultout = LINST.InsertBatch(dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), TxtTAName.Text, ViewState["TCT"].ToString(), TotalDrAmt.ToString(), "1", "7", "TR", "INTCR", TxtTrfNarration.Text.ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "1", "0", "01/01/1900");

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString() == "" ? "0" : dt.Rows[0]["CurrInterest"].ToString()) > 0)
                                            {
                                                //Current Interest Debit To 2 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "2", "2", "7", "Interest Debit", Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["Interest"].ToString() == "" ? "0" : dt.Rows[0]["Interest"].ToString()) > 0)
                                            {
                                                //Current Interest Credit To 2 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPType.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtTAccNo.Text.Trim().ToString(), "2", "1", "7", "Interest Credit", Convert.ToDouble(TotalDrAmt.ToString()).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        TotalDrAmt = 0;
                                    }
                                }
                            }
                        }
                        if (resultout > 0)
                        {
                            PaymentAmount();
                            BindGrid();
                            ClearTransfer();
                            GrdEntryDate.Visible = true;
                            lblMessage.Text = "Amount Transfer to Loan Account Successfully...!!";
                            ModalPopup.Show(this.Page);
                            txtTotLoanBal.Text = "";
                            return;
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

            #endregion

            else if (ddlPayMode.SelectedValue == "1")
            {
                DataTable DT = new DataTable();
                DT = LINST.GetDDSInfo("3");

                if (DT.Rows.Count > 0)
                {
                    if (ddlActivity.SelectedValue == "3") // **************    FOR MATURE CLOSURE  ************* CASH IN HAND
                    {
                        INTAMT = Convert.ToDouble(TxtINTC.Text);// -Convert.ToDouble(Txt_Provi.Text);
                        result = LINST.InsertBatch("2", TxtAGCD.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), TxtCalcInt.Text.ToString(), "1", "10", "TR-INT", "INT TRF FROM " + ViewState["IR"].ToString() + "-" + TxtAccNo.Text + "", "Maturity Closure " + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);

                        if (Convert.ToInt32(Txt_Provi.Text) > 0 && ViewState["RevProvi"].ToString() != "N")
                        {
                            result = LINST.InsertBatch("22", ViewState["IR"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Txt_Provi.Text, "2", "10", "TR-INT", "INT TRF TO " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Maturity Closure " + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                        }
                        if (INTAMT > 0) //if AMT is Positive
                        {
                            result = LINST.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), INTAMT.ToString(), "2", "10", "TR-INT", "INT Dr TO " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Maturity Closure " + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                        }
                        else //if AMT is negative
                        {
                            INTAMT = Math.Abs(INTAMT);
                            result = LINST.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), INTAMT.ToString(), "1", "10", "TR-INT", "INT Cr TO " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Maturity Closure " + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                        }

                        AMT = Convert.ToDouble(TxtINTC.Text);
                        string[] TD = Session["EntryDate"].ToString().Split('/');
                        double BAL = Convert.ToDouble(TxtCBal.Text);
                        BAL = Convert.ToDouble(TxtPayAmt.Text); //--abhishek after calculation 09-03-2017

                        BAL1 = (BAL);
                        string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());

                        result = LINST.InsertBatch("99", cgl, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), BAL1.ToString(), "1", "4", "CP", "Dr TO " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "MATURE ACC CLOSURE", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                        if (result > 0)
                        {
                            result = LINST.InsertBatch("2", TxtAGCD.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Convert.ToDouble(Convert.ToDouble(ViewState["PayableAmt"].ToString() == "" ? "0" : ViewState["PayableAmt"].ToString()) + BAL1).ToString(), "2", "4", "CP", "Cr TO 99 - " + cgl.ToString() + "", "MATURE ACC CLOSURE", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);

                            if (result > 0)
                            {
                                PaymentAmount();    //Amol
                                GrdEntryDate.Visible = true;
                                BindGrid();
                                BindGrid1();
                                //ClearData();
                                lblMessage.Text = "Record Added Successfully...........!!";
                                ModalPopup.Show(this.Page);
                                FL = "Insert";//Dhanya Shetty
                                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSCloser_Mature_" + TxtAGCD + "_" + TxtAccNo + "_" + TxtPayAmt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
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
                        string Dsbgl1 = TxtAGCD.Text;
                        Dr_GLCOD = "2";
                        Dr_SUBGLCODE = Dsbgl1;
                    }

                    if (TxtINTC.Text != "" && TxtINTC.Text != "0")
                    {
                        INTAMT = Convert.ToDouble(TxtINTC.Text);// -Convert.ToDouble(Txt_Provi.Text);
                        result = LINST.InsertBatch("2", TxtAGCD.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), TxtCalcInt.Text.ToString(), "1", "10", "TR-INT", "INT TRF FROM " + ViewState["IR"].ToString() + "-" + TxtAccNo.Text + "", "Premature Closure " + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);

                        if (Convert.ToInt32(Txt_Provi.Text) > 0 && ViewState["RevProvi"].ToString() != "N")
                        {
                            result = LINST.InsertBatch("22", ViewState["IR"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Txt_Provi.Text, "2", "10", "TR-INT", "INT TRF TO " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Premature Closure " + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                        }

                        if (INTAMT > 0) //if AMT is Positive
                        {
                            result = LINST.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), INTAMT.ToString(), "2", "10", "TR-INT", "INT Dr TO " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Premature Closure " + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                        }
                        else //if AMT is negative
                        {
                            INTAMT = Math.Abs(INTAMT);
                            result = LINST.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), INTAMT.ToString(), "1", "10", "TR-INT", "INT Cr TO " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Premature Closure " + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                        }
                    }

                    //Do not Change PARTICULARS it effects the Cancel Entry****************************************************************
                    if (Convert.ToInt32(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) > 0)
                    {
                        result = LINST.InsertBatch("100", DT.Rows[0]["serv_acc"].ToString(), TxtAccNo.Text, "", ViewState["CT"].ToString(), TxtServCHRS.Text, "1", ACTI, PMTMODE, "Dr FROM " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "SERVICE CHG", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                    }
                    if (Convert.ToInt32(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) > 0)
                    {
                        result = LINST.InsertBatch("100", DT.Rows[0]["comm_Accno"].ToString(), TxtAccNo.Text, "", ViewState["CT"].ToString(), TxtPreMCAMT.Text, "1", ACTI, PMTMODE, "Dr FROM " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "COMMISION CR", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                    }
                    if (Convert.ToInt32(Txt_Provi.Text == "" ? "0" : Txt_Provi.Text) > 0 && ViewState["RevProvi"].ToString() != "N")
                    {
                        result = LINST.InsertBatch("22", ViewState["IR"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Txt_Provi.Text.ToString(), "2", "10", "TR-INT", "INT Dr TO " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Premature Closure " + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                        if (result > 0)
                        {
                            result = LINST.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Txt_Provi.Text.ToString(), "1", "10", "TR-INT", "INT Cr TO " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Premature Closure " + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                        }
                    }
                    if (Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text) > 0)
                    {

                        string GLCODE = BD.GetAccTypeGL(ViewState["PRD_CGST"].ToString(), Session["BRCD"].ToString());
                        string[] GLC = GLCODE.Split('_');
                        result = LINST.InsertBatch(GLC[1].ToString(), ViewState["PRD_CGST"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), ViewState["CGST_AMT"].ToString(), "1", ACTI, PMTMODE, "CGST " + ViewState["RATE_CGST"].ToString() + " % Dr From " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Premature Closure " + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);

                        GLCODE = BD.GetAccTypeGL(ViewState["PRD_SGST"].ToString(), Session["BRCD"].ToString());
                        GLC = GLCODE.Split('_');
                        result = LINST.InsertBatch(GLC[1].ToString(), ViewState["PRD_SGST"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), ViewState["SGST_AMT"].ToString(), "1", ACTI, PMTMODE, "SGST " + ViewState["RATE_SGST"].ToString() + " % Dr From " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Premature Closure " + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);

                    }
                    AMT = Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) + Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text);
                    if (AMT != 0)
                    {
                        result = LINST.InsertBatch(Dr_GLCOD, Dr_SUBGLCODE, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), AMT.ToString(), "2", ACTI, PMTMODE, "Dr FROM " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "COMMISION DEDUCT", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                    }

                    string[] TD = Session["EntryDate"].ToString().Split('/');


                    double BAL = Convert.ToDouble(TxtCBal.Text);
                    if (rdbpart.Checked == true)
                    {
                        BAL = Convert.ToDouble(Txtpartpay.Text);
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

                    //Added by amol ON 30/11/2017 because loan case
                    BAL1 = Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text) + Math.Abs(Convert.ToDouble(Convert.ToDouble(ViewState["PayableAmt"].ToString() == "" ? "0" : ViewState["PayableAmt"].ToString()) - BAL1));

                    string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());
                    result = LINST.InsertBatch("99", cgl, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), BAL1.ToString(), "1", "4", "CP", "Dr FROM " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "PREMATURE ACC CLOSURE", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                    result = LINST.InsertBatch("2", TxtAGCD.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Convert.ToDouble(Convert.ToDouble(ViewState["PayableAmt"].ToString() == "" ? "0" : ViewState["PayableAmt"].ToString()) + BAL1).ToString(), "2", "4", "CP", "Cr TO 99 " + cgl.ToString() + "", "PREMATURE ACC CLOSURE", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                    if (result > 0)
                    {
                        PaymentAmount();    //Amol
                        GrdEntryDate.Visible = true;
                        BindGrid();
                        BindGrid1();
                        lblMessage.Text = "Record Added Successfully...........!!";
                        ModalPopup.Show(this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSCloser_Premature_" + TxtAGCD + "_" + TxtAccNo + "_" + TxtPayAmt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    }
                }
            }

            //Do not Change PARTICULARS it Affects the Cancel Entry****************************************************************
            else if (ddlPayMode.SelectedValue == "2") // ***** TRANSFER Entry ******
            {
                DataTable DT = new DataTable();
                DT = LINST.GetDDSInfo("3");

                if (ddlActivity.SelectedValue == "3")  // **************    FOR MATURE CLOSURE  ************* TRF
                {
                    result = LINST.InsertBatch("2", TxtAGCD.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), TxtCalcInt.Text.ToString(), "1", "10", "TR-INT", "INT TRF FROM " + ViewState["IR"].ToString() + "-" + TxtAccNo.Text + "", "Maturity Closure " + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);

                    if (Txt_Provi != null)
                        PLAMT = Convert.ToDouble(TxtINTC.Text);// -Convert.ToDouble(Txt_Provi.Text);

                    if (PLAMT > 0) //if AMT is Positive
                    {
                        result = LINST.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), PLAMT.ToString(), "2", "10", "TR-INT", "INT Dr TO " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Maturity Closure " + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                    }
                    else //if AMT is negative
                    {
                        PLAMT = Math.Abs(PLAMT);
                        result = LINST.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), PLAMT.ToString(), "1", "10", "TR-INT", "INT Cr TO " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Maturity Closure " + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                    }

                    if (Convert.ToInt32(Txt_Provi.Text == "" ? "0" : Txt_Provi.Text) > 0 && ViewState["RevProvi"].ToString() != "N")
                    {
                        result = LINST.InsertBatch("22", ViewState["IR"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Txt_Provi.Text.ToString(), "2", "10", "TR-INT", "PROVISION Dr to " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Maturity Closure " + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                    }
                    AMT = Convert.ToDouble(TxtCalcInt.Text);
                    string[] TD = Session["EntryDate"].ToString().Split('/');

                    //  Added by amol on 29-09-2017 for (before saving some amount transfer to loan account)
                    BAL1 = Convert.ToDouble(TxtPayAmt.Text.Trim().ToString() == "" ? "0" : TxtPayAmt.Text.Trim().ToString());

                    ViewState["TCT"] = ViewState["TCT"] == null ? "0" : ViewState["TCT"].ToString();
                    result = LINST.InsertBatch(ViewState["PGL"].ToString(), TxtPType.Text, TxtTAccNo.Text, TxtTAName.Text, ViewState["TCT"].ToString(), BAL1.ToString(), "1", "7", "TR", "TRF Dr FROM AC CLOSURE " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "MATURE ACC CLOSURE", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);

                    if (result > 0)
                    {
                        result = LINST.InsertBatch("2", TxtAGCD.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Convert.ToDouble(Convert.ToDouble(ViewState["PayableAmt"].ToString() == "" ? "0" : ViewState["PayableAmt"].ToString()) + BAL1).ToString(), "2", "7", "TR", "TRF Cr TO " + TxtPType.Text + "-" + TxtTAccNo.Text + "", "MATURE ACC CLOSURE", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                        if (result > 0)
                        {
                            PaymentAmount();    //Amol
                            GrdEntryDate.Visible = true;
                            BindGrid();
                            lblMessage.Text = "Record Added Successfully...........!!";
                            ModalPopup.Show(this.Page);
                            FL = "Insert";//Dhanya Shetty
                            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSCloser_matureTRF_" + TxtAGCD + "_" + TxtAccNo + "_" + TxtPayAmt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        }
                    }
                }


                //Do not Change PARTICULARS it Affects the Cancel Entry****************************************************************
                if (ddlActivity.SelectedValue == "2")  // **** For PRemature Closure **** TRF
                {

                    if (TxtINTC.Text != "" && TxtINTC.Text != "0")
                    {
                        result = LINST.InsertBatch("2", TxtAGCD.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), TxtCalcInt.Text.ToString(), "1", "10", "TR-INT", "INT TRF FROM " + ViewState["IR"].ToString() + "-" + TxtAccNo.Text + "", "Premature Closure" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);

                        if (Txt_Provi != null)
                            PLAMT = Convert.ToDouble(TxtINTC.Text);// -Convert.ToDouble(Txt_Provi.Text);

                        if (PLAMT > 0) //if AMT is Positive
                        {
                            result = LINST.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), PLAMT.ToString(), "2", "10", "TR-INT", "INT Dr TO " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Premature Closure" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                        }
                        else //if AMT is negative
                        {
                            PLAMT = Math.Abs(PLAMT);
                            result = LINST.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), PLAMT.ToString(), "1", "10", "TR-INT", "INT Cr TO " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Premature Closure" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                        }

                        if (Convert.ToInt32(Txt_Provi.Text == "" ? "0" : Txt_Provi.Text) > 0 && ViewState["RevProvi"].ToString() != "N")
                        {
                            result = LINST.InsertBatch("22", ViewState["IR"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Txt_Provi.Text.ToString(), "2", "10", "TR-INT", "PROVISION Dr to " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Premature Closure" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                        }
                    }

                    if (Convert.ToInt32(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) > 0)
                    {
                        result = LINST.InsertBatch("100", DT.Rows[0]["serv_acc"].ToString(), TxtAccNo.Text, "", ViewState["CT"].ToString(), TxtServCHRS.Text, "1", "7", "TR", "SERVICE CHG " + TxtAGCD.Text + "-" + TxtAccNo.Text + " ", "Premature Closure" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                    }
                    if (Convert.ToInt32(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) > 0)
                    {
                        result = LINST.InsertBatch("100", DT.Rows[0]["comm_Accno"].ToString(), TxtAccNo.Text, "", ViewState["CT"].ToString(), TxtPreMCAMT.Text, "1", "7", "TR", "COMMISSION CHG " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Premature Closure" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                    }
                    if (Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text) > 0)
                    {
                        string GLCODE = BD.GetAccTypeGL(ViewState["PRD_CGST"].ToString(), Session["BRCD"].ToString());
                        string[] GLC = GLCODE.Split('_');
                        result = LINST.InsertBatch(GLC[1].ToString(), ViewState["PRD_CGST"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), ViewState["CGST_AMT"].ToString(), "1", "7", "TR", "CGST " + ViewState["RATE_CGST"].ToString() + " % Dr From " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Premature Closure" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);

                        GLCODE = BD.GetAccTypeGL(ViewState["PRD_SGST"].ToString(), Session["BRCD"].ToString());
                        GLC = GLCODE.Split('_');
                        result = LINST.InsertBatch(GLC[1].ToString(), ViewState["PRD_SGST"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), ViewState["SGST_AMT"].ToString(), "1", "7", "TR", "SGST " + ViewState["RATE_SGST"].ToString() + " % Dr From " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Premature Closure" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                    }
                    AMT = Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) + Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text);

                    string[] TD = Session["EntryDate"].ToString().Split('/');
                    double BAL = Convert.ToDouble(TxtCBal.Text);
                    BAL1 = BAL;
                    if (rdbpart.Checked == true)
                    {
                        BAL1 = Convert.ToDouble(Txtpartpay.Text);
                    }

                    //Added by amol ON 30/11/2017 because loan case
                    BAL1 = (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text) + Math.Abs(Convert.ToDouble(Convert.ToDouble(ViewState["PayableAmt"].ToString() == "" ? "0" : ViewState["PayableAmt"].ToString()) - BAL1)));

                    ViewState["TCT"] = ViewState["TCT"] == null ? "0" : ViewState["TCT"].ToString();
                    result = LINST.InsertBatch(ViewState["PGL"].ToString(), TxtPType.Text, TxtTAccNo.Text, TxtTAName.Text, ViewState["TCT"].ToString(), TxtPayAmt.Text.ToString(), "1", "7", "TR", "TRF Dr FROM AC CLOSURE " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "PREMATURE ACC CLOSURE", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);

                    result = LINST.InsertBatch("2", TxtAGCD.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Convert.ToDouble(Convert.ToDouble(ViewState["PayableAmt"].ToString() == "" ? "0" : ViewState["PayableAmt"].ToString()) + BAL1).ToString(), "2", "7", "TR", "TRF CR TO " + TxtPType.Text + "-" + TxtTAccNo.Text + "", "PREMATURE ACC CLOSURE", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                    if (result > 0)
                    {
                        PaymentAmount();    //Amol
                        GrdEntryDate.Visible = true;
                        BindGrid();
                        lblMessage.Text = "Record Added Successfully...........!!";
                        ModalPopup.Show(this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSCloser_PrematureTRF_" + TxtAGCD + "_" + TxtAccNo + "_" + TxtPayAmt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    }
                }
            }

            else if (ddlPayMode.SelectedValue == "3")   // ****** Cheque Entry ********
            {
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
                DT = LINST.GetDDSInfo("2");

                if (ddlActivity.SelectedValue == "3")   // ***for Mature Closure***
                {
                    result = LINST.InsertBatch("2", TxtAGCD.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), TxtCalcInt.Text.ToString(), "1", "7", "TR-INT", "INT TRF FROM " + ViewState["IR"].ToString() + "-" + TxtAccNo.Text + "", "Maturity Closure " + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                    if (Txt_Provi != null)
                        PLAMT = Convert.ToDouble(TxtINTC.Text);// -Convert.ToDouble(Txt_Provi.Text);

                    if (PLAMT > 0) //if AMT is Positive
                    {
                        result = LINST.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), PLAMT.ToString(), "2", "7", "TR-INT", "INT Dr TO " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Maturity Closure " + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                    }
                    else //if AMT is negative
                    {
                        PLAMT = Math.Abs(PLAMT);
                        result = LINST.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), PLAMT.ToString(), "1", "7", "TR-INT", "INT Cr TO " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Maturity Closure " + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                    }
                    if (Convert.ToInt32(Txt_Provi.Text == "" ? "0" : Txt_Provi.Text) > 0 && ViewState["RevProvi"].ToString() != "N")
                    {
                        result = LINST.InsertBatch("22", ViewState["IR"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Txt_Provi.Text.ToString(), "2", "7", "TR-INT", "PROVISION Dr to " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Maturity Closure " + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                    }

                    AMT = Convert.ToDouble(TxtCalcInt.Text);
                    string[] TD = Session["EntryDate"].ToString().Split('/');
                    double BAL = Convert.ToDouble(TxtCBal.Text);
                    BAL1 = Convert.ToDouble(TxtPayAmt.Text);

                    ViewState["TCT"] = ViewState["TCT"] == null ? "0" : ViewState["TCT"].ToString();
                    result = LINST.InsertBatch(ViewState["PGL"].ToString(), TxtPType.Text, AC, ACNM, ViewState["TCT"].ToString(), BAL1.ToString(), "1", "5", "TR", "CHQ Dr FROM  AC CLOSURE " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "MATURE ACC CLOSURE", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);

                    if (result > 0)
                    {
                        result = LINST.InsertBatch("2", TxtAGCD.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Convert.ToDouble(Convert.ToDouble(ViewState["PayableAmt"].ToString() == "" ? "0" : ViewState["PayableAmt"].ToString()) + BAL1).ToString(), "2", "5", "TR", "CHQ Cr TO AC CLOSURE " + TxtPType.Text + "-" + AC.ToString() + "", "MATURE ACC CLOSURE", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                        if (result > 0)
                        {
                            //The Set Number Suddenly Rises its count by n number as solution --Abhishek
                            PaymentAmount();    //Amol
                            GrdEntryDate.Visible = true;
                            BindGrid();
                            lblMessage.Text = "Record Added Successfully...........!!";
                            ModalPopup.Show(this.Page);
                            FL = "Insert";//Dhanya Shetty
                            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSCloser_MatureCheque_" + TxtAGCD + "_" + TxtAccNo + "_" + TxtPayAmt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        }
                    }
                }
                ///////////////////// MATURE CLOSURE END


                ///////////////////// PRE MATURE CLOSURE END
                if (ddlActivity.SelectedValue == "2")   // ****premature ****CHEQUE
                {

                    if (TxtINTC.Text != "" && TxtINTC.Text != "0")
                    {
                        result = LINST.InsertBatch("2", TxtAGCD.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), TxtCalcInt.Text.ToString(), "1", "7", "TR-INT", "INT TRF FROM " + ViewState["IR"].ToString() + "-" + TxtAccNo.Text + "", "Premature Closure" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                        if (Txt_Provi != null)
                            PLAMT = Convert.ToDouble(TxtINTC.Text);// -Convert.ToDouble(Txt_Provi.Text);

                        if (PLAMT > 0) //if AMT is Positive
                        {
                            result = LINST.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), PLAMT.ToString(), "2", "7", "TR-INT", "INT Dr TO " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Premature Closure" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                        }
                        else //if AMT is negative
                        {
                            PLAMT = Math.Abs(PLAMT);
                            result = LINST.InsertBatch("100", ViewState["PLACC"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), PLAMT.ToString(), "1", "7", "TR-INT", "INT Cr TO " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Premature Closure" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                        }
                        if (Convert.ToInt32(Txt_Provi.Text == "" ? "0" : Txt_Provi.Text) > 0 && ViewState["RevProvi"].ToString() != "N")
                        {
                            result = LINST.InsertBatch("22", ViewState["IR"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Txt_Provi.Text.ToString(), "2", "7", "TR-INT", "PROVISION Dr to " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Premature Closure" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                        }
                    }

                    if (Convert.ToInt32(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) > 0)
                    {
                        result = LINST.InsertBatch("100", DT.Rows[0]["serv_acc"].ToString(), TxtAccNo.Text, "", ViewState["CT"].ToString(), TxtServCHRS.Text, "1", "7", "TR", "SERVICE CHG " + TxtAGCD.Text + "-" + TxtAccNo.Text + " ", "Premature Closure" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                    }
                    if (Convert.ToInt32(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) > 0)
                    {
                        result = LINST.InsertBatch("100", DT.Rows[0]["comm_Accno"].ToString(), TxtAccNo.Text, "", ViewState["CT"].ToString(), TxtPreMCAMT.Text, "1", "7", "TR", "COMMISION CHG " + TxtAGCD.Text + "-" + TxtAccNo.Text + " ", "Premature Closure" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                    }
                    if (Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text) > 0)
                    {

                        string GLCODE = BD.GetAccTypeGL(ViewState["PRD_CGST"].ToString(), Session["BRCD"].ToString());
                        string[] GLC = GLCODE.Split('_');
                        result = LINST.InsertBatch(GLC[1].ToString(), ViewState["PRD_CGST"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), ViewState["CGST_AMT"].ToString(), "1", "7", "TR", "CGST " + ViewState["RATE_CGST"].ToString() + " % Dr From " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Premature Closure" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);

                        GLCODE = BD.GetAccTypeGL(ViewState["PRD_SGST"].ToString(), Session["BRCD"].ToString());
                        GLC = GLCODE.Split('_');
                        result = LINST.InsertBatch(GLC[1].ToString(), ViewState["PRD_SGST"].ToString(), TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), ViewState["SGST_AMT"].ToString(), "1", "7", "TR", "SGST " + ViewState["RATE_SGST"].ToString() + " % Dr From " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "Premature Closure" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                    }
                    AMT = Convert.ToDouble(TxtServCHRS.Text == "" ? "0" : TxtServCHRS.Text) + Convert.ToDouble(TxtPreMCAMT.Text == "" ? "0" : TxtPreMCAMT.Text) + Convert.ToDouble(TxtGST.Text == "" ? "0" : TxtGST.Text);

                    result = LINST.InsertBatch("2", TxtAGCD.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), AMT.ToString(), "2", "7", "TR", "Cr to 100 P and L", "Premature Closure" + TxtTrfNarration.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                    if (result > 0)
                    {

                        string[] TD = Session["EntryDate"].ToString().Split('/');
                        double BAL = Convert.ToDouble(TxtCBal.Text);
                        BAL1 = BAL;
                        if (rdbpart.Checked == true)
                        {
                            BAL1 = Convert.ToDouble(TxtPayAmt.Text); //--abhishek after calculation 09-03-2017
                        }

                        //Added by amol ON 30/11/2017 because loan case
                        BAL1 = (Convert.ToDouble(TxtINTC.Text == "" ? "0" : TxtINTC.Text) + Math.Abs(Convert.ToDouble(Convert.ToDouble(ViewState["PayableAmt"].ToString() == "" ? "0" : ViewState["PayableAmt"].ToString()) - (BAL1 - AMT))));

                        ViewState["TCT"] = ViewState["TCT"] == null ? "0" : ViewState["TCT"].ToString();
                        result = LINST.InsertBatch(ViewState["PGL"].ToString(), TxtPType.Text, AC, ACNM, ViewState["TCT"].ToString(), BAL1.ToString(), "1", "5", "TR", "CHQ Dr FROM  AC CLOSURE " + TxtAGCD.Text + "-" + TxtAccNo.Text + "", "PREMATURE ACC CLOSURE", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                        result = LINST.InsertBatch("2", TxtAGCD.Text, TxtAccNo.Text, TxtAccName.Text, ViewState["CT"].ToString(), Convert.ToDouble(Convert.ToDouble(ViewState["PayableAmt"].ToString() == "" ? "0" : ViewState["PayableAmt"].ToString()) + (BAL1)).ToString(), "2", "5", "TR", "CHQ Cr TO AC CLOSURE " + TxtPType.Text + "-" + TxtTAccNo.Text + "", "PREMATURE ACC CLOSURE", Session["BRCD"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "0", TxtInstNo.Text, TxtInstDate.Text);
                        if (result > 0)
                        {
                            //The Set Number Suddenly Rises its count by n number as solution --Abhishek
                            PaymentAmount();    //Amol
                            GrdEntryDate.Visible = true;
                            BindGrid();
                            lblMessage.Text = "Record Added Successfully...........!!";
                            ModalPopup.Show(this.Page);
                            FL = "Insert";//Dhanya Shetty
                            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSCloser_PrematureCheque_" + TxtAGCD + "_" + TxtAccNo + "_" + TxtPayAmt + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
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

}