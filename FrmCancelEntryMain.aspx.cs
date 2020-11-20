using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class FrmCancelEntryMain : System.Web.UI.Page
{
    ClsVoucherAutho VA = new ClsVoucherAutho();
    ClsCashPayment CurrentCls = new ClsCashPayment();
    ClsCancelEntry CE = new ClsCancelEntry();
    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsAgentCommision AC = new ClsAgentCommision();
    ClsOpenClose OC = new ClsOpenClose();
    string sResult = "", STR = "", FL = "";
    string Flag = "", PayMast = "";
    int Result = 0;

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
                TxtEntryDate.Text = Session["EntryDate"].ToString();
                TxtEntryDate.Enabled = false;
                TxtSetNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    
    public void BindGrid(string FL)
    {
        try
        {
            if (Session["UGRP"].ToString() != "1")
            {
                CE.Getinfotable(grdShow, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), FL);
            }
            else
            {
                CE.GetinfotableAdmin(grdShow, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), FL);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    //New Logic for Showing All entries
    public void BindGrid() //New Logic
    {
        try
        {
            if (Session["UGRP"].ToString() != "1")
            {
                CE.Getinfotable_All(grdShow, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());
            }
            else
            {
                CE.GetinfotableAdmin_All(grdShow, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindByVoucher()
    {
        if (Session["UGRP"].ToString() != "1")
        {
            CE.GetByVoucherGridAdmin_Spe(Session["BRCD"].ToString(), Session["MID"].ToString(), grdShow, TxtSetNo.Text, TxtEntryDate.Text);
        }
        else
        {
            CE.GetByVoucherGridAdmin_Spe(Session["BRCD"].ToString(), Session["MID"].ToString(), grdShow, TxtSetNo.Text, TxtEntryDate.Text);
        }
    }
    //End New Logic for Showing All entries

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //if (Rdb_EntryType.SelectedValue == "1")
            //{
            //    if (TxtSetNo.Text == "")
            //    {
            //        BindGrid("OTH");
            //    }
            //    else
            //    {
            //        BindByVoucher("OTH");
            //        FL = "Insert";//Dhanya Shetty
            //        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "OtherEntry-Cancel_FDReceipt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            //    }
            //}
            //else if (Rdb_EntryType.SelectedValue == "2") //Deposit Entires
            //{
            //    if (TxtSetNo.Text == "")
            //    {
            //        BindGrid("DEP");
            //    }
            //    else
            //    {
            //        BindByVoucher("DEP");
            //        FL = "Insert";//Dhanya Shetty
            //        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Cancel_DepositEntry_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            //    }
            //}

            //else if (Rdb_EntryType.SelectedValue == "3") //Loan Installment Entires
            //{
            //    if (TxtSetNo.Text == "")
            //    {
            //        BindGrid("LOANINST");
            //    }
            //    else
            //    {
            //        BindByVoucher("LOANINST");
            //    }
            //}

            if (TxtSetNo.Text == "")
            {
                BindGrid();
            }
            else
            {
                BindByVoucher();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindByVoucher(string FL)
    {
        if (Session["UGRP"].ToString() != "1")
        {
            CE.GetByVoucherGrid(Session["BRCD"].ToString(), Session["MID"].ToString(), grdShow, TxtSetNo.Text, TxtEntryDate.Text, FL);
        }
        else
        {
            CE.GetByVoucherGridAdmin(Session["BRCD"].ToString(), Session["MID"].ToString(), grdShow, TxtSetNo.Text, TxtEntryDate.Text, FL);
        }
    }

    //protected void grdShow_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    try
    //    {
    //        grdShow.PageIndex = e.NewPageIndex;
    //        if (Rdb_EntryType.SelectedValue == "1")
    //        {
    //            if (TxtSetNo.Text == "")
    //            {
    //                BindGrid("OTH");
    //            }
    //            else
    //            {
    //                BindByVoucher("OTH");
    //            }
    //        }
    //        else if (Rdb_EntryType.SelectedValue == "2") //Deposit Entires
    //        {
    //            if (TxtSetNo.Text == "")
    //            {
    //                BindGrid("DEP");
    //            }
    //            else
    //            {
    //                BindByVoucher("DEP");
    //            }
    //        }

    //        else if (Rdb_EntryType.SelectedValue == "3") //Loan Installment Entires
    //        {
    //            if (TxtSetNo.Text == "")
    //            {
    //                BindGrid("LOANINST");
    //            }
    //            else
    //            {
    //                BindByVoucher("LOANINST");
    //            }
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }

    //}

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlnk = (LinkButton)sender;
            string[] AT = objlnk.CommandArgument.ToString().Split('_');
            ViewState["SetNo"] = AT[0].ToString();
            ViewState["ScrollNo"] = AT[1].ToString();
            ViewState["EntryMid"] = AT[2].ToString();

            string Modal_Flag = "";
            PayMast = VA.GetPAYMAST(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString(), ViewState["ScrollNo"].ToString());

            //Added By Amol ON 17/11/2017 
            if (ViewState["EntryMid"].ToString() != Session["MID"].ToString())
            {
                DateTime TransDate = Convert.ToDateTime("2017-09-28");
                DateTime EDate = Convert.ToDateTime(conn.ConvertDate(Session["EntryDate"].ToString()).ToString());

                if ((PayMast.ToString() == "ABB-LOANINST" || PayMast.ToString() == "LOANINST" || PayMast.ToString() == "LoanClose") && (EDate <= TransDate))
                {
                    lblMessage.Text = "Back dated entry...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
                else
                {
                    if (PayMast == "CASHR")
                    {
                        string redirectLink = "FrmAuthorizeCR.aspx?Flag=CN&VN=" + ViewState["SetNo"].ToString() + "&SN=" + ViewState["ScrollNo"].ToString() + "";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectLink + "','_blank')", true);
                        //Response.Redirect("~/FrmAuthorizeCR.aspx?Flag=CN&VN=" + ViewState["SetNo"].ToString() + "&SN=" + ViewState["ScrollNo"].ToString() + "");
                        return;
                    }
                    else if (PayMast == "CASHP")
                    {
                        string redirectLink = "FrmAuthorizeCP.aspx?Flag=CN&VN=" + ViewState["SetNo"].ToString() + "&SN=" + ViewState["ScrollNo"].ToString() + "";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectLink + "','_blank')", true);
                        //Response.Redirect("~/FrmAuthorizeCP.aspx?Flag=CN&VN=" + ViewState["SetNo"].ToString() + "&SN=" + ViewState["ScrollNo"].ToString() + "");
                        return;
                    }
                    else if (PayMast == "LOANINST" || PayMast == "LoanClose")
                    {
                        string redirectLink = "FrmAuthorizeLoan.aspx?Flag=CN&VN=" + ViewState["SetNo"].ToString() + "&SN=" + ViewState["ScrollNo"].ToString() + "";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectLink + "','_blank')", true);
                        //Response.Redirect("~/FrmAuthorizeLoan.aspx?Flag=CN&VN=" + ViewState["SetNo"].ToString() + "&SN=" + ViewState["ScrollNo"].ToString() + "");
                        return;
                    }

                    if (PayMast.ToString() == "LoanClose")
                        PayMast = "LOANINST";

                    if (PayMast.ToString() == "Reverse" || PayMast.ToString() == "DDSCLOSE" || PayMast.ToString() == "LOANINST" || PayMast.ToString() == "CASHP" || PayMast.ToString() == "CASHR" || PayMast.ToString() == "TDCLOSE" || PayMast.ToString() == "MULTITRANSFER" || PayMast.ToString() == "SIPOST" || PayMast.ToString() == "ABB-LOANINST" || PayMast.ToString() == "ABB-CASHP" || PayMast.ToString() == "ABB-CASHR" || PayMast.ToString() == "AgentCommision" || PayMast.ToString() == "MobileMultiPost")
                    {
                        if (PayMast.ToString() == "SIPOST")
                        {
                            Modal_Flag = "MULTITRANSFER";
                            PayMast = "SIPOST";
                        }
                        else if (PayMast.ToString() == "Reverse")// amruta 20170711 ABB module
                        {
                            Modal_Flag = "MULTITRANSFER";
                        }
                        else if (PayMast.ToString() == "ABB-CASHP")// amruta 20170711 ABB module
                        {
                            PayMast = "CASHP";
                            Modal_Flag = PayMast.ToString();
                            Flag = "1";
                        }
                        else if (PayMast.ToString() == "ABB-CASHR")// amruta 20170711 ABB module
                        {
                            PayMast = "CASHR";
                            Modal_Flag = PayMast.ToString();
                            Flag = "1";
                        }
                        else if (PayMast.ToString() == "AgentCommision")// ashok 20170711 AgentCommision module
                        {
                            PayMast = "AgentCommision";
                            Modal_Flag = PayMast.ToString();
                            Flag = "1";
                        }
                        else if (PayMast.ToString() == "MobileMultiPost")// ashok 20180314 MobileMultiPost module
                        {
                            PayMast = "MobileMultiPost";
                            Modal_Flag = PayMast.ToString();
                            Flag = "1";
                        }
                        else if (PayMast == "LoanApp")
                        {
                            PayMast = "LOANINST";
                            Modal_Flag = PayMast.ToString();
                            Flag = "1";
                        }
                        else if (PayMast.ToString() == "ABB-LOANINST")// amruta 20170711 ABB module
                        {
                            PayMast = "LOANINST";
                            Modal_Flag = PayMast.ToString();
                            Flag = "1";
                        }
                        else
                        {
                            Modal_Flag = PayMast.ToString();
                        }

                        ViewState["MODAL"] = PayMast.ToString();
                        GetVoucherDetails(ViewState["SetNo"].ToString(), Session["BRCD"].ToString(), PayMast.ToString(), ViewState["ScrollNo"].ToString());
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("$('#" + Modal_Flag + "').modal('show');");
                        sb.Append(@"</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);

                    }
                    else
                    {
                        string SFL = "";
                        if (Rdb_EntryType.SelectedValue == "1")
                        {
                            SFL = "CRCP";
                        }
                        else if (Rdb_EntryType.SelectedValue == "2")
                        {
                            SFL = "DP";
                        }

                        string url = "FrmCancelConfirm.aspx?setno=" + ViewState["SetNo"].ToString() + "&FL=" + SFL + "&STR=" + PayMast.ToString();
                        NewWindows(url);
                    }
                }
            }
            else
            {
                WebMsgBox.Show("Not allow for same user...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void GetVoucherDetails(string SETNO, string BRCD, string PAYMAST, string ScrollNo)
    {
        try
        {
            DataTable DT = new DataTable();
            string GLCODE = "";

            DT = VA.GetDetails_ToFill(SETNO, BRCD, Session["EntryDate"].ToString(), PAYMAST, ScrollNo);
            {
                if (PAYMAST == "DDSCLOSE")
                {
                    //id	Agcd	AgName	Accno	AccName	AccSts	AccStsName	AccType	AccTypeName	ODate	Period	CDate	
                    //SIntsruc	CBal	PayType	PartPayAmt	Activity	Provi	CalcIntr	PrematCommi	OtherRec	
                    //  PayableIntr	TotPayAmt	PayMode	TrfPrdCode	TrfPrdName	TrfAccno	TrfAccName	InstruNo	Intstrudate

                    TxtDDSAGCD.Text = DT.Rows[0]["Agcd"].ToString();
                    TxtDDSAGName.Text = DT.Rows[0]["AgName"].ToString();
                    TxtDDSAccNo.Text = DT.Rows[0]["Accno"].ToString();
                    TxtDDSAccName.Text = DT.Rows[0]["AccName"].ToString();
                    //TxtDDSAccSTS.Text = DT.Rows[0]["AccSts"].ToString();
                    //TxtDDSAccSTSName.Text = DT.Rows[0]["AccStsName"].ToString();
                    //TxtDDSAccType.Text = DT.Rows[0]["AccType"].ToString();
                    //TxtDDSAccTName.Text = DT.Rows[0]["AccTypeName"].ToString();
                    TxtDDSOpeningDate.Text = DT.Rows[0]["ODate"].ToString().Replace(" 12:00:00", "");
                    //TxtDDSPeriod.Text = DT.Rows[0]["Period"].ToString();
                    //TxtDDSCLDate.Text = DT.Rows[0]["CDate"].ToString().Replace(" 12:00:00", "");
                    //if (DT.Rows[0]["PayType"].ToString() == "1")
                    //{
                    //    rdbDDSfull.Checked = true;
                    //}
                    //else
                    //{
                    //    rdbDDSpart.Checked = true;
                    //}
                    TxtDDSCBal.Text = DT.Rows[0]["CBal"].ToString();
                    //TxtDDSpartpay.Text = DT.Rows[0]["PartPayAmt"].ToString();
                    //if (DT.Rows[0]["Activity"].ToString() == "1")
                    //{
                    //    ddlDDSActivity.SelectedValue = "2";
                    //}
                    //else
                    //{
                    //    ddlDDSActivity.SelectedValue = "3";
                    //}
                    //TxtDDS_Provi.Text = DT.Rows[0]["Provi"].ToString();
                    TxtDDSCalcInt.Text = DT.Rows[0]["CalcIntr"].ToString();
                    //txtTAmount.Text = DT.Rows[0]["TotPayAmt"].ToString();
                    //TxtDDSPreMC.Text = DT.Rows[0]["PrematCommi"].ToString();
                    //txtAdmin.Text = DT.Rows[0]["OtherRec"].ToString();
                    txtDeduction.Text = (Convert.ToDouble(DT.Rows[0]["PrematCommi"]) + Convert.ToDouble(DT.Rows[0]["OtherRec"])).ToString();
                    if (DT.Rows[0]["PayMode"].ToString() == "" || DT.Rows[0]["PayMode"].ToString() == null)
                    {
                        lblTitle.InnerText = "";
                        lblTrf.Text = "";
                    }
                    else if (DT.Rows[0]["PayMode"].ToString() == "CASH")
                    {
                        lblTitle.InnerText = "Paid By Cash";
                        lblTrf.Text = "Cash To";
                    }
                    else if (DT.Rows[0]["PayMode"].ToString() == "TRANSFER")
                    {
                        lblTitle.InnerText = "Transfer";
                        lblTrf.Text = "Trf To";
                    }
                    else if (DT.Rows[0]["PayMode"].ToString() == "CHEQUE")
                    {
                        lblTitle.InnerText = "Paid by Cheque";
                        lblTrf.Text = "Cheque To";
                    }
                    txtNet.Text = ((Convert.ToDouble(DT.Rows[0]["CBal"]) + Convert.ToDouble(DT.Rows[0]["CalcIntr"])) - (Convert.ToDouble(DT.Rows[0]["PrematCommi"]) + Convert.ToDouble(DT.Rows[0]["OtherRec"]))).ToString();
                    // txtProdCode.Text = DT.Rows[0]["TrfPrdCode"].ToString();
                    // txtAccnoo.Text = DT.Rows[0]["TrfAccNo"].ToString();
                    TxtDDSPreMCAMT.Text = DT.Rows[0]["PrematCommi"].ToString();
                    TxtDDSServCHRS.Text = DT.Rows[0]["OtherRec"].ToString();
                    //TxtDDSINTC.Text = DT.Rows[0]["PayableIntr"].ToString();
                    TxtDDSPayAmt.Text = (Convert.ToDouble(DT.Rows[0]["CBal"]) + Convert.ToDouble(DT.Rows[0]["CalcIntr"])).ToString();
                    //TxtDDSPayMode.Text = DT.Rows[0]["PayMode"].ToString();
                    TxtDDSPType.Text = DT.Rows[0]["TrfPrdCode"].ToString();
                    TxtDDSPTName.Text = DT.Rows[0]["TrfPrdName"].ToString();
                    TxtDDSTAccNo.Text = DT.Rows[0]["TrfAccno"].ToString();
                    TxtDDSTAName.Text = DT.Rows[0]["TrfAccName"].ToString();
                    //TxtDDSInstNo.Text = DT.Rows[0]["InstruNo"].ToString();
                    //TxtDDSInstDate.Text = DT.Rows[0]["Intstrudate"].ToString().Replace(" 12:00:00", "");
                }
                else if (PAYMAST == "LOANINST" || PAYMAST == "ABB-LOANINST")
                {
                    BD.BindAccStatus(ddlAccStatus);
                    BD.BindPayment(ddlPayType, "1");

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
                            string AC1 = "";
                            string[] PrAcc = DT.Rows[i]["Amount_1"].ToString().Split('/');

                            if (PrAcc.Length > 0)
                            {
                                txtProdType.Text = PrAcc[0].ToString();

                                AC1 = VA.Getaccno(txtProdType.Text, Session["BRCD"].ToString());
                                if (AC1 != null)
                                {
                                    string[] AC = AC1.Split('_'); ;
                                    txtProdName.Text = AC[1].ToString();
                                }

                                txtAccNo.Text = PrAcc[1].ToString();
                                DataTable CN = new DataTable();
                                CN = VA.GetCustName(txtProdType.Text, txtAccNo.Text, Session["BRCD"].ToString());
                                if (CN.Rows.Count > 0)
                                {
                                    string[] CustName = CN.Rows[0]["CustName"].ToString().Split('_');
                                    txtAccName.Text = CustName[0].ToString();
                                    txtCustNo.Text = CustName[1].ToString();
                                }

                                txtAccStatus.Text = VA.GetAccStatus(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());

                                ddlAccStatus.SelectedIndex = Convert.ToInt32(txtAccStatus.Text.Trim().ToString());
                            }

                            if (DT.Rows[i]["Activity"].ToString() == "3")
                            {
                                ddlPayType.SelectedIndex = Convert.ToInt32(1);
                                txtNarration.Text = "By Cash";

                                Transfer.Visible = false;
                                Transfer1.Visible = false;
                                txtDepositeAmt.Text = DT.Rows[i]["Amount"].ToString();
                                txtAmount.Text = DT.Rows[i]["Amount"].ToString();
                            }
                            else if (DT.Rows[i]["Activity"].ToString() == "7")
                            {
                                ddlPayType.SelectedIndex = Convert.ToInt32(2);
                                txtNarration.Text = "By TRF";

                                Transfer.Visible = true;
                                Transfer1.Visible = false;
                                txtProdType1.Text = DT.Rows[i]["SubGlCode"].ToString();
                                AC1 = VA.Getaccno(txtProdType1.Text, Session["BRCD"].ToString());
                                if (AC1 != null)
                                {
                                    string[] AC = AC1.Split('_'); ;
                                    txtProdName1.Text = AC[1].ToString();
                                }

                                TxtAccNo1.Text = DT.Rows[i]["AccNo"].ToString();
                                DataTable CN = new DataTable();
                                CN = VA.GetCustName(txtProdType1.Text, TxtAccNo1.Text, Session["BRCD"].ToString());
                                if (CN.Rows.Count > 0)
                                {
                                    string[] CustName = CN.Rows[0]["CustName"].ToString().Split('_');
                                    TxtAccName1.Text = CustName[0].ToString();
                                }

                                txtDepositeAmt.Text = DT.Rows[i]["Amount"].ToString();
                                txtAmount.Text = DT.Rows[i]["Amount"].ToString();
                            }
                            else if (DT.Rows[i]["Activity"].ToString() == "5")
                            {
                                ddlPayType.SelectedIndex = Convert.ToInt32(3);
                                txtNarration.Text = "By TRF";

                                Transfer.Visible = true;
                                Transfer1.Visible = true;
                                txtProdType1.Text = DT.Rows[i]["SubGlCode"].ToString();
                                txtProdName1.Text = DT.Rows[i]["GlName"].ToString();
                                TxtAccNo1.Text = DT.Rows[i]["SubGlCode"].ToString();
                                TxtAccName1.Text = DT.Rows[i]["GlName"].ToString();
                                TxtInstNo.Text = DT.Rows[i]["InstrumentNo"].ToString();
                                TxtInstDate.Text = DT.Rows[i]["InstrumentDate"].ToString();

                                txtDepositeAmt.Text = DT.Rows[i]["Amount"].ToString();
                                txtAmount.Text = DT.Rows[i]["Amount"].ToString();
                            }
                        }
                    }
                }
                else if (PAYMAST == "CASHR" || PAYMAST == "ABB-CASHR")
                {
                    TxtEntrydateCR.Text = DT.Rows[0]["ENTRYDATE"].ToString().Replace(" 12:00:00", "");
                    TxtProcodeCR.Text = DT.Rows[0]["SUBGLCODE"].ToString();
                    TxtAccnoCR.Text = DT.Rows[0]["ACCNO"].ToString();
                    TxtNarrationCR.Text = DT.Rows[0]["PARTICULARS"].ToString();
                    TxtNarration2CR.Text = DT.Rows[0]["PARTICULARS2"].ToString();
                    TxtAmountCR.Text = DT.Rows[0]["AMOUNT"].ToString();
                    TxtProNameCR.Text = DT.Rows[0]["GLNAME"].ToString();
                    TxtAccNameCR.Text = DT.Rows[0]["CUSTNAME"].ToString();
                    GLCODE = DT.Rows[0]["GLCODE"].ToString();
                    if (TxtAccnoCR.Text != "" && TxtProcodeCR.Text != "")
                    {
                        string[] TD = Session["EntryDate"].ToString().Split('/');
                        TxtOldBalCR.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcodeCR.Text, TxtAccnoCR.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), GLCODE).ToString();
                        TxtNewBalCR.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcodeCR.Text, TxtAccnoCR.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), GLCODE).ToString();
                    }

                    //    DT = new DataTable();
                    //    string OpDate = OC.GetAccOpenDate(Session["BRCD"].ToString(), TxtProcodeCR.Text.Trim().ToString(), TxtAccnoCR.Text.Trim().ToString());

                    ////    DT = GetAccStatDetails(Session["BRCD"].ToString(), TxtProcodeCR.Text.Trim().ToString(), TxtAccnoCR.Text.Trim().ToString(), OpDate);

                    //    if (DT.Rows.Count > 0)
                    //    {
                    //        grdAccStat.DataSource = DT;
                    //        grdAccStat.DataBind();
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('.bs-example-modal-lg').modal('show');</script>", false);
                    //    }
                    //    else
                    //    {
                    //        lblMessage.Text = "Details Not Found For This Account Number...!!";
                    //        ModalPopup.Show(this.Page);
                    //    }
                }
                else if (PAYMAST == "CASHP" || PAYMAST == "ABB-CASHP")
                {
                    TxtEntrydateCP.Text = DT.Rows[0]["ENTRYDATE"].ToString().Replace(" 12:00:00", "");
                    TxtProcodeCP.Text = DT.Rows[0]["SUBGLCODE"].ToString();
                    TxtAccNoCP.Text = DT.Rows[0]["ACCNO"].ToString();
                    txtnarationCP.Text = DT.Rows[0]["PARTICULARS"].ToString();
                    txtnaration1CP.Text = DT.Rows[0]["PARTICULARS2"].ToString();
                    TxtAmountCP.Text = DT.Rows[0]["AMOUNT"].ToString();
                    TxtProNameCP.Text = DT.Rows[0]["GLNAME"].ToString();
                    TxtAccNameCP.Text = DT.Rows[0]["CUSTNAME"].ToString();
                    GLCODE = DT.Rows[0]["GLCODE"].ToString();
                    if (TxtAccNoCP.Text != "" && TxtProcodeCP.Text != "")
                    {
                        string[] TD = Session["EntryDate"].ToString().Split('/');
                        txtBalanceCP.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcodeCP.Text, TxtAccNoCP.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), GLCODE).ToString();
                        TxtNewBalanceCP.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcodeCP.Text, TxtAccNoCP.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), GLCODE).ToString();
                    }

                    //  DT = new DataTable();
                    //  string OpDate = OC.GetAccOpenDate(Session["BRCD"].ToString(), TxtProcodeCP.Text.Trim().ToString(), TxtAccNoCP.Text.Trim().ToString());

                    ////  DT = GetAccStatDetails(Session["BRCD"].ToString(), TxtProcodeCP.Text.Trim().ToString(), TxtAccNoCP.Text.Trim().ToString(), OpDate);

                    //  if (DT.Rows.Count > 0)
                    //  {
                    //      grdAccStatement.DataSource = DT;
                    //      grdAccStatement.DataBind();
                    //      ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('.bs-example-modal-lg').modal('show');</script>", false);
                    //  }
                    //  else
                    //  {
                    //      lblMessage.Text = "Details Not Found For This Account Number...!!";
                    //      ModalPopup.Show(this.Page);
                    //  }
                }
                else if (PAYMAST == "TDCLOSE")
                {
                    //PAYMASTTACCNO	ACC_TYPE	OPR_TYPE	OPENINGDATE	INTPAYOUT	PRNAMT	PRDTYPE	PERIOD	RATEOFINT	
                    //INTAMT	MATURITYAMT	DUEDATE	P_PAYABLE	INT_PAYABLE	INT_APPLIED	ADMIN_FEE	COMMISSION	SB_INT	CLOSE_TYPE	PAY_MODE	TOTAL_PAY	TRFACCNO	TRFPRD
                    BD.BindIntrstPayout(ddlTDIntrestPay);
                    TxtTDProcode.Text = DT.Rows[0]["PRDNO"].ToString();
                    TxtTDProName.Text = DT.Rows[0]["PRDNAME"].ToString();
                    TxtTDAccno.Text = DT.Rows[0]["ACCNO"].ToString();
                    TxtTDAccName.Text = DT.Rows[0]["CUSTNAME"].ToString();
                    TxtTDDepoDate.Text = Convert.ToDateTime(DT.Rows[0]["OPENINGDATE"].ToString()).ToString("dd/MM/yyyy"); ;
                    TxtTDCustno.Text = DT.Rows[0]["CUSTNO"].ToString();
                    ddlTDIntrestPay.Text = DT.Rows[0]["INTPAYOUT"].ToString();
                    TxtTDDepoAmt.Text = DT.Rows[0]["PRNAMT"].ToString();
                    TxtTDPeriod.Text = DT.Rows[0]["PERIOD"].ToString();
                    ddlTDduration.SelectedValue = DT.Rows[0]["PRDTYPE"].ToString();
                    TxtTDRate.Text = DT.Rows[0]["RATEOFINT"].ToString();
                    TxtTDIntrest.Text = Convert.ToInt32(DT.Rows[0]["INTAMT"]).ToString();
                    TxtTDMaturity.Text = Convert.ToInt32(DT.Rows[0]["MATURITYAMT"]).ToString();
                    TxtTDDueDate.Text = Convert.ToDateTime(DT.Rows[0]["DUEDATE"].ToString()).ToString("dd/MM/yyyy");
                    TxtTDSbintrest.Text = DT.Rows[0]["SB_INT"].ToString();
                    string LMSTS = DT.Rows[0]["LMSTATUS"].ToString();

                    if (LMSTS == "1")
                        TxtTDOpenClose.Text = "Open";
                    else if (LMSTS == "2")
                        TxtTDOpenClose.Text = "Lien";
                    else if (LMSTS == "3")
                        TxtTDOpenClose.Text = "Close";

                    TxtTDPrincPaybl.Text = DT.Rows[0]["P_PAYABLE"].ToString();
                    TxtTDIntrestPaybl.Text = DT.Rows[0]["INT_PAYABLE"].ToString();
                    TxtTDInterestNew.Text = DT.Rows[0]["INT_APPLIED"].ToString();
                    TxtTDAdminCharges.Text = DT.Rows[0]["ADMIN_FEE"].ToString();
                    TxtTDCommission.Text = DT.Rows[0]["COMMISSION"].ToString();
                    txtTDPayAmnt.Text = DT.Rows[0]["TOTAL_PAY"].ToString();
                    TxtTDPayMode.Text = DT.Rows[0]["CLOSE_TYPE"].ToString();
                    TxtTNarration.Text = DT.Rows[0]["P1"].ToString();

                    string Close_t = DT.Rows[0]["PAY_MODE"].ToString();
                    if (Close_t == "RENEW")
                        RdbTDRenew.Checked = true;
                    else
                        RdbTDClose.Checked = true;

                    TxtTDPCode1.Text = DT.Rows[0]["TRFPRD"].ToString();
                    TxtTDPName1.Text = DT.Rows[0]["TRFPRDNAME"].ToString();
                    TxtTDAccno1.Text = DT.Rows[0]["TRFACCNO"].ToString();
                    TxtTDAccName1.Text = DT.Rows[0]["TRFACCNAME"].ToString();

                    int DF = Convert.ToInt32(conn.GetDayDiff(Session["EntryDate"].ToString(), TxtTDDueDate.Text));
                    if (DF > 0)
                    {
                        rdbTDPreMature.Checked = true;
                    }
                    else
                    {
                        rdbTDMature.Checked = true;
                    }
                }
                else if (PAYMAST == "MULTITRANSFER" || PAYMAST == "Reverse" )
                {
                    if (DT.Rows.Count > 0)
                    {
                        double DrTotal = 0, CrTotal = 0;
                        int i = 0;
                        for (i = 0; i < DT.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(DT.Rows[i]["CREDIT"].ToString()) != 0)
                            {
                                CrTotal += Convert.ToDouble(DT.Rows[i]["CREDIT"].ToString());
                            }
                            else
                            {
                                DrTotal += Convert.ToDouble(DT.Rows[i]["DEBIT"].ToString());
                            }
                        }
                        GrdMultiTransfer.DataSource = DT;
                        GrdMultiTransfer.DataBind();
                        Txt_MTCrAmt.Text = CrTotal.ToString();
                        Txt_MTDrAmt.Text = DrTotal.ToString();

                    }
                }
                else if (PAYMAST == "AgentCommision")
                {
                    if (DT.Rows.Count > 0)
                    {

                        txtAgCode.Text = DT.Rows[0]["AGENTCODE"].ToString();
                        txtAgName.Text = DT.Rows[0]["AGENTNAME"].ToString();
                        txtFDate.Text = DT.Rows[0]["FROMDATE"].ToString();
                        txtTDate.Text = DT.Rows[0]["TODATWE"].ToString();
                        txttcoll.Text = DT.Rows[0]["TOTAL_COLL"].ToString();
                        txtTotColl.Text = DT.Rows[0]["COMMI_COLL"].ToString();
                        txtCommision.Text = AC.AgentComm().ToString();
                        txtCommAmt.Text = DT.Rows[0]["COMMISION"].ToString();
                        txtTDDeduction.Text = AC.AgentCommTds().ToString();
                        txtTdAmt.Text = DT.Rows[0]["TDS"].ToString();
                        TxtAGCDSEC.Text = AC.AgentSecurity().ToString();
                        TxtAgentSec.Text = DT.Rows[0]["AGENTSEC"].ToString();
                        TXtAGCDAMC.Text = DT.Rows[0]["AMC"].ToString();
                        txttrev.Text = AC.Agenttravelling().ToString();
                        txttravelexp.Text = DT.Rows[0]["TRV_EXP"].ToString();
                        txtNetCommision.Text = DT.Rows[0]["NETCOMMISION"].ToString();
                      

                    }
                }
                else if (PAYMAST == "MobileMultiPost")
                {
                    if (DT.Rows.Count > 0)
                    {

                        if (DT.Rows.Count > 0)
                        {
                            double DrTotal = 0, CrTotal = 0;
                            int i = 0;
                            for (i = 0; i < DT.Rows.Count; i++)
                            {
                                if (Convert.ToDouble(DT.Rows[i]["CREDIT"].ToString()) != 0)
                                {
                                    CrTotal += Convert.ToDouble(DT.Rows[i]["CREDIT"].ToString());
                                }
                                else
                                {
                                    DrTotal += Convert.ToDouble(DT.Rows[i]["DEBIT"].ToString());
                                }
                            }
                            GrdMultipost.DataSource = DT;
                            GrdMultipost.DataBind();
                            txtCredit.Text = CrTotal.ToString();
                            txtdebit.Text = DrTotal.ToString();
                        }


                    }
                }
                else if (PAYMAST == "SIPOST") //Abhishek 15-06-2017
                {
                    if (DT.Rows.Count > 0)
                    {
                        double DrTotal = 0, CrTotal = 0;
                        int i = 0;
                        for (i = 0; i < DT.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(DT.Rows[i]["CREDIT"].ToString()) != 0)
                            {
                                CrTotal += Convert.ToDouble(DT.Rows[i]["CREDIT"].ToString());
                            }
                            else
                            {
                                DrTotal += Convert.ToDouble(DT.Rows[i]["DEBIT"].ToString());
                            }
                        }
                        GrdMultiTransfer.DataSource = DT;
                        GrdMultiTransfer.DataBind();
                        Txt_MTCrAmt.Text = CrTotal.ToString();
                        Txt_MTDrAmt.Text = DrTotal.ToString();

                    }
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

    public void NewWindows(string url)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + url + "', 'popup_window', 'width=1000,height=400,left=50,top=50,resizable=no');", true);
    }

    public void Clear()
    {
        TxtSetNo.Text = "";
        TxtEntryDate.Text = "";
    }

    protected void Exit_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmBlank.aspx");
    }
    
    protected void btnModal_CancelInst_Click(object sender, EventArgs e)
    {
        try
        {
            int Res = 0;
            string STG = CE.CheckStage(ViewState["SetNo"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString());
            if (STG != "1003" || Session["UGRP"].ToString() == "1")
            {
                int MID = CE.GetSetMid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString());

                if (MID != Convert.ToInt32(Session["MID"].ToString()))
                {
                    if (ViewState["MODAL"].ToString() == "SIPOST")
                    {
                        //  Added by amol On 18/12/2017 for cancel sipost voucher as per darade sir instruction
                        Res = CE.CancelSIPost(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString(), Session["MID"].ToString());
                    }
                    else if (ViewState["MODAL"].ToString() == "Reverse")
                    {
                        Res = CE.CancelReverse(ViewState["SetNo"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                    }
                    else if (ViewState["MODAL"].ToString() == "DDSCLOSE")
                    {
                        Res = CE.CancelDDS(ViewState["SetNo"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                    }
                    else if (ViewState["MODAL"].ToString() == "MULTITRANSFER")
                    {
                        Res = CE.CancelEntryMulti(ViewState["SetNo"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), MID.ToString(), Session["MID"].ToString());
                    }
                    else if (ViewState["MODAL"].ToString() == "LOANINST")
                    {
                        Res = CE.CancelEntryLoan(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), ViewState["SetNo"].ToString(), MID.ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());
                    }
                    else if (ViewState["MODAL"].ToString() == "ABB-LOANINST")//Amruta 11/07/2017
                    {
                        Res = CE.CancelEntryLoan1(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), ViewState["SetNo"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());
                    }
                    else if (ViewState["MODAL"].ToString() == "CASHR" || ViewState["MODAL"].ToString() == "CASHP" || ViewState["MODAL"].ToString() == "MULTITRANSFER")
                    {
                        Res = CE.CancelCRCP(ViewState["SetNo"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                    }
                    else if (ViewState["MODAL"].ToString() == "ABB-CASHR" || ViewState["MODAL"].ToString() == "ABB-CASHP")//Amruta 12/07/2017
                    {
                        Res = CE.CancelCRCP1(ViewState["SetNo"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                    }
                    else if (ViewState["MODAL"].ToString() == "TDCLOSE")
                    {
                        Res = CE.CancelTDA(ViewState["SetNo"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                    }
                    else if (ViewState["MODAL"].ToString() == "SIPOST")
                    {
                        Res = CE.CancelSIPOST(ViewState["SetNo"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                    }
                    else if (ViewState["MODAL"].ToString() == "AgentCommision")
                    {
                        Res = CE.CancelEntryAgentCo(ViewState["SetNo"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), MID.ToString(), Session["MID"].ToString());
                    }
                    if (Res > 0)
                    {
                        WebMsgBox.Show("SetNo " + ViewState["SetNo"].ToString() + " Sucessfully Canceled...!!", this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string RR = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Voucher_CancelEntry_" + ViewState["SetNo"].ToString() + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "", "alert('Entry " + ViewState["SetNo"].ToString() + " sucessfully authorized....!');", true);
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("location.reload();");
                        sb.Append(@"</script>");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "", "alert('Not allow for same user...!!');", true);
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("location.reload();");
                    sb.Append(@"</script>");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "", "alert('Entry already Auhtorized,Contact to Administrator...!!');", true);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("location.reload();");
                sb.Append(@"</script>");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    
    protected void btn_LoanDetails_Click(object sender, EventArgs e)
    {

    }

    protected void btn_PhotoSign_Click(object sender, EventArgs e)
    {

    }

    protected void btn_JoinName_Click(object sender, EventArgs e)
    {

    }

    protected void btn_Statement_Click(object sender, EventArgs e)
    {
        BindGrid1();
    }

    public void BindGrid1()
    {
        DataTable DT = new DataTable();
        try
        {
            string Modal_Flag = "DDSCLOSE";

            //string FinDate = CurrentCls.GetFinStartDate(Session["EntryDate"].ToString());
            string FinDate = OC.GetAccOpenDate(Session["BRCD"].ToString(), TxtDDSAGCD.Text.Trim().ToString(), TxtDDSAccNo.Text.Trim().ToString());

            DT = GetAccStatDetails(FinDate);
            if (DT.Rows.Count > 0)
            {
                GridView1.DataSource = DT;
                GridView1.DataBind();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#" + Modal_Flag + "').modal('show');");
                sb.Append(@"</script>");
                DIVPHOTO.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
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
    
    public DataTable GetAccStatDetails(string FinDate)
    {
        DataTable DT = new DataTable();
        try
        {
            string[] DTF, DTT;

            DTF = FinDate.ToString().Split('/');
            DTT = Session["EntryDate"].ToString().Split('/');

            DT = CurrentCls.GetAccStatReport(DTF[1].ToString(), DTT[1].ToString(), DTF[2].ToString(), DTT[2].ToString(), FinDate.ToString(), Session["EntryDate"].ToString(), TxtDDSAccNo.Text.Trim().ToString(), TxtDDSAGCD.Text.Trim().ToString(), Session["BRCD"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    
    protected void btnModal_BtnAgCancel_Click(object sender, EventArgs e)
    {
        try
        {
            int Res = 0;
            string STG = CE.CheckStage(ViewState["SetNo"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString());
            if (STG != "1003" || Session["UGRP"].ToString() == "1")
            {
                int MID = CE.GetSetMid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString());

                if (MID != Convert.ToInt32(Session["MID"].ToString()))
                {

                   
                     if (ViewState["MODAL"].ToString() == "AgentCommision")
                    {
                        Res = CE.CancelEntryAgentCo(ViewState["SetNo"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), MID.ToString(), Session["MID"].ToString());
                    }
                    if (Res > 0)
                    {
                        WebMsgBox.Show("SetNo " + ViewState["SetNo"].ToString() + " Sucessfully Canceled...!!", this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string RR = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Voucher_CancelEntry_" + ViewState["SetNo"].ToString() + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "", "alert('Entry " + ViewState["SetNo"].ToString() + " sucessfully authorized....!');", true);
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("location.reload();");
                        sb.Append(@"</script>");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "", "alert('Not allow for same user...!!');", true);
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("location.reload();");
                    sb.Append(@"</script>");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "", "alert('Entry already Auhtorized,Contact to Administrator...!!');", true);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("location.reload();");
                sb.Append(@"</script>");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnModal_CancelMulti_Click(object sender, EventArgs e)
    {
        try
        {
            int Res = 0;
            string STG = CE.CheckStage(ViewState["SetNo"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString());
            if (STG != "1003" || Session["UGRP"].ToString() == "1")
            {
                int MID = CE.GetSetMid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString());

                if (MID != Convert.ToInt32(Session["MID"].ToString()))
                {


                    if (ViewState["MODAL"].ToString() == "MobileMultiPost")
                    {
                        Res = CE.CancelEntryMobileMultiPosting(ViewState["SetNo"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), MID.ToString(), Session["MID"].ToString());
                    }
                    if (Res > 0)
                    {
                        WebMsgBox.Show("SetNo " + ViewState["SetNo"].ToString() + " Sucessfully Canceled...!!", this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string RR = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Voucher_CancelEntry_" + ViewState["SetNo"].ToString() + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "", "alert('Entry " + ViewState["SetNo"].ToString() + " sucessfully authorized....!');", true);
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("location.reload();");
                        sb.Append(@"</script>");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "", "alert('Not allow for same user...!!');", true);
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("location.reload();");
                    sb.Append(@"</script>");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "", "alert('Entry already Auhtorized,Contact to Administrator...!!');", true);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("location.reload();");
                sb.Append(@"</script>");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}