using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

public partial class FrmVoucherAutho : System.Web.UI.Page
{
    ClsCashPayment CurrentCls = new ClsCashPayment();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsVoucherAutho VA = new ClsVoucherAutho();
    ClsOpenClose OC = new ClsOpenClose();
    ClsAuthorized AT = new ClsAuthorized();
    DbConnection conn = new DbConnection();
    ClsCashReciept CR = new ClsCashReciept();
    scustom customcs = new scustom();
    ClsCommon cmn=new ClsCommon();
    ClsMultiVoucher MV = new ClsMultiVoucher();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    DataTable DT = new DataTable();
    string STR = "", FL = "", Setno = "";
    int Result, result = 0;
    string sql = "", FST = "", TST = "";
    string Modal_Flag = "", Message = "";
    double EAMT, AMT, DBT, CRT;
    double Balance = 0;
    string acctypeno = "", acctype = "", jointname = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            if (Session["BNKCDE"].ToString() == "1001")
            {
                Rdb_Lot.Visible = false;
                LblMsg.Visible = false;
            }
            else
                Rdb_Lot.Visible = true;

            if (VA.GetAuthoParam(Session["BRCD"].ToString()) == "Y" || (Session["UGRP"].ToString() != "5" && Session["UGRP"].ToString() != "6"))
            {
                autoUName.ContextKey = Session["BRCD"].ToString();
                TxtDate.Text = Session["EntryDate"].ToString();
                TxtDate.Enabled = false;
                if (Request.QueryString["chkat"] != null)
                {
                    grdCashRct.DataBind();
                    BindData();
                    if (Session["BNKCDE"].ToString() == "1001")
                    {
                        Rdb_Lot.Visible = false;
                        LblMsg.Visible = false;
                    }
                    else
                        Rdb_Lot.Visible = true;
                    if (Rdb_Single.Checked == true)
                    {
                        Div_Lot.Visible = false;
                        Div_Single.Visible = true;
                        btnSearch.Visible = true;
                        Btn_Submit.Visible = false;
                        btnSearch.Text = "Submit";
                        TxtInstNo.Focus();
                    }
                    else
                    {
                        Div_Lot.Visible = true;
                        Div_Single.Visible = false;
                        btnSearch.Visible = true;
                        Btn_Submit.Visible = true;
                        btnSearch.Text = "Search";
                        TxtFSetno.Focus();
                    }
                }
            }
            else
            {
                Response.Redirect("~/FrmBlank.aspx?ShowMessage=true");
            }
        }
        else
        {
            if (Request.QueryString["chkat"] != null)
            {
                grdCashRct.DataBind();
                BindData();
                if (Session["BNKCDE"].ToString() == "1001")
                {
                    Rdb_Lot.Visible = false;
                    LblMsg.Visible = false;
                }
                else
                    Rdb_Lot.Visible = true;
                if (Rdb_Single.Checked == true)
                {
                    Div_Lot.Visible = false;
                    Div_Single.Visible = true;
                    btnSearch.Visible = true;
                    Btn_Submit.Visible = false;
                    btnSearch.Text = "Submit";
                    TxtInstNo.Focus();
                }
                else
                {
                    Div_Lot.Visible = true;
                    Div_Single.Visible = false;
                    btnSearch.Visible = true;
                    Btn_Submit.Visible = true;
                    btnSearch.Text = "Search";
                    TxtFSetno.Focus();
                }
            }
        }
    }

    protected void grdCashRct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCashRct.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlnk = (LinkButton)sender;
            string[] AT = objlnk.CommandArgument.ToString().Split('_');
            ViewState["SetNo"] = AT[0].ToString();
            ViewState["ScrollNo"] = AT[1].ToString();
            ViewState["EntryMid"] = AT[2].ToString();

            //Added By Amol on 07/11/2017 as per darade sir instruction
            Message = VA.CheckVoucher(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString());
            if (Message.ToString() == "0")
            {
                //Added By Amol ON 17/11/2017 
                if (ViewState["EntryMid"].ToString() != Session["MID"].ToString())
                {
                    //if(VA.CheckAdminVoucher(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString()) == 0)
                    if (cmn.GetParameter("1", "AdExp", "1003").ToString() == "N")
                    {
                        if ((VA.CheckAdminVoucher(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString()) == "1") && (cmn.GetBrCode(Session["LOGINCODE"].ToString(), "1003").ToString() != "1"))
                        {
                            WebMsgBox.Show("This user have no permission ...!!", this.Page);
                            return;
                        }
                    }

                    STR = VA.GetPAYMAST(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString(), ViewState["ScrollNo"].ToString());
                    hdnset.Value = ViewState["SetNo"].ToString();
                    ViewState["SETNO"] = ViewState["SetNo"].ToString();

                    if (STR == "CASHR")
                    {
                        string redirectLink = "FrmAuthorizeCR.aspx?Flag=AT&VN=" + ViewState["SetNo"].ToString() + "&SN=" + ViewState["ScrollNo"].ToString() + "";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectLink + "','_blank')", true);
                        return;
                    }
                    else if (STR == "CASHP")
                    {
                        string redirectLink = "FrmAuthorizeCP.aspx?Flag=AT&VN=" + ViewState["SetNo"].ToString() + "&SN=" + ViewState["ScrollNo"].ToString() + "";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectLink + "','_blank')", true);
                        return;
                    }
                    else if (STR == "LOANINST" || STR == "LoanClose")
                    {
                        string redirectLink = "FrmAuthorizeLoan.aspx?Flag=AT&VN=" + ViewState["SetNo"].ToString() + "&SN=" + ViewState["ScrollNo"].ToString() + "";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectLink + "','_blank')", true);
                        return;
                    }

                    if (STR == "LoanClose")
                        STR = "LOANINST";

                    if (STR == "CASHP") //Cash Receipt
                        Modal_Flag = STR;
                    else if (STR == "CASHR") //Cash Payment
                        Modal_Flag = STR;
                    else if (STR == "LOANAPP")//loan app amruta ahirrao 07-11-2017
                    {
                        STR = "LOANAPP";
                        Modal_Flag = "LOANAPP";
                    }
                    else if (STR == "MULTITRANSFER" || STR == "INV-CLO") //Multiple Transfer
                    {
                        STR = "MULTITRANSFER";
                        Modal_Flag = "MULTITRANSFER";
                    }
                    else if (STR == "DDSCLOSE") //Daily Deposit Closure
                        Modal_Flag = STR;
                    else if (STR == "TDCLOSE")  //Deposit Closure
                        Modal_Flag = STR;
                    else if (STR == "TDRENEWAL") //Deposit Renewal
                        Modal_Flag = STR;
                    else if (STR == "LOANINST") //Loan Installment
                        Modal_Flag = STR;
                    else
                    {
                        string url = "FrmVoucherPA.aspx?setno=" + ViewState["SetNo"].ToString() + "&sr=" + ViewState["ScrollNo"].ToString();
                        NewWindows(url);
                        return;
                    }

                    if (STR == "MULTITRANSFER" || STR == "INV-CLO")
                    {
                        ViewState["MODAL"] = Modal_Flag.ToString();
                        hdnscroll.Value = "1";
                        GetVoucherDetails(ViewState["SetNo"].ToString(), Session["BRCD"].ToString(), STR, ViewState["ScrollNo"].ToString());
                    }
                    else if (STR == "LOANAPP")
                    {

                        STR = "LOANINST";
                        ViewState["MODAL"] = Modal_Flag.ToString();
                        GetVoucherDetails(ViewState["SetNo"].ToString(), Session["BRCD"].ToString(), STR, ViewState["ScrollNo"].ToString());

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("$('#LOANINST').modal('show');");
                        sb.Append(@"</script>");
                        ShowImage(Modal_Flag);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                    }
                    else
                    {
                        ViewState["MODAL"] = Modal_Flag.ToString();
                        GetVoucherDetails(ViewState["SetNo"].ToString(), Session["BRCD"].ToString(), STR, ViewState["ScrollNo"].ToString());

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("$('#" + Modal_Flag + "').modal('show');");
                        sb.Append(@"</script>");
                        ShowImage(Modal_Flag);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                    }
                }
                else
                {
                    WebMsgBox.Show("Not allow for same user...!!", this.Page);
                    return;
                }
            }
            else
            {
                WebMsgBox.Show("Voucher Not Tally... Please check in voucher view ...!!", this.Page);
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
        string PanNo = CR.GetPanNo(BRCD, CustNo);
        return PanNo;
    }

    public void GetVoucherDetails(string SETNO, string BRCD, string PAYMAST, string Scroll)
    {
        try
        {
            DataTable DT = new DataTable();
            string GLCODE = "";

            DT = VA.GetDetails_ToFill(SETNO, BRCD, Session["EntryDate"].ToString(), PAYMAST, Scroll);
            if (DT.Rows.Count > 0)
            {
                if (PAYMAST == "CASHP")
                {
                    TxtEntrydateCP.Text = DT.Rows[0]["ENTRYDATE"].ToString().Replace(" 12:00:00", "");
                    TxtProcodeCP.Text = DT.Rows[0]["SUBGLCODE"].ToString();
                    TxtAccNoCP.Text = DT.Rows[0]["ACCNO"].ToString();
                    txtnarationCP.Text = DT.Rows[0]["PARTICULARS"].ToString();

                    TxtVouchertype.Text = DT.Rows[0]["PARTICULARS2"].ToString();
                    string sql;
                    sql = "SELECT SRNO FROM LOOKUPFORM1 WHERE DESCRIPTION='" + TxtVouchertype.Text + "' AND LNO=1059";
                    TxtVoucherTypeno.Text = conn.sExecuteScalar(sql);
                    if (TxtVoucherTypeno.Text == "2")
                    {
                        DIVINSTRNO.Visible = true;
                        Div_Instdate.Visible = true;
                        TxtInstruNo.Text = DT.Rows[0]["INSTRUMENTNO"].ToString();
                        TextBox7.Text = DT.Rows[0]["INSTRUMENTDATE"].ToString();
                    }
                    else
                    {
                        DIVINSTRNO.Visible = false;
                        Div_Instdate.Visible = false; ;
                    }
                    TxtAmountCP.Text = DT.Rows[0]["AMOUNT"].ToString();
                    TxtProNameCP.Text = DT.Rows[0]["GLNAME"].ToString();
                    TxtAccNameCP.Text = DT.Rows[0]["CUSTNAME"].ToString();
                    GLCODE = DT.Rows[0]["GLCODE"].ToString();
                    txtToken.Text = DT.Rows[0]["TokenNo"].ToString();
                    TextBox6.Text = DT.Rows[0]["CustNo"].ToString();
                    txtPan.Text = PanCard(Session["BRCD"].ToString(), TextBox6.Text);

                    if (TxtAccNoCP.Text != "" && TxtProcodeCP.Text != "")
                    {
                        string[] TD = Session["EntryDate"].ToString().Split('/');
                        txtBalanceCP.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcodeCP.Text, TxtAccNoCP.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), GLCODE).ToString();
                        TxtNewBalanceCP.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcodeCP.Text, TxtAccNoCP.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), GLCODE).ToString();
                    }
                    // added by ankita on 09/08/2017
                    TxtSplInst.Text = DT.Rows[0]["Spl_Instruction"].ToString();
                    acctypeno = DT.Rows[0]["OPR_TYPE"].ToString();
                    acctype = CR.GetAcctype(acctypeno);
                    txtAccTypeName.Text = acctype.ToString();
                    if (txtAccTypeName.Text == "JOINT")
                    {
                        jointname = CR.Getjointname(Session["BRCD"].ToString(), TxtAccNoCP.Text.ToString(), TxtProcodeCP.Text);
                        lbjoint.Visible = true;
                        TxtJointName.Visible = true;
                        TxtJointName.Text = jointname.ToString();
                    }
                    else
                    {
                        lbjoint.Visible = false;
                        TxtJointName.Visible = false;
                    }
                    DT = new DataTable();
                    //string OpDate = OC.GetAccOpenDate(Session["BRCD"].ToString(), TxtProcodeCP.Text.Trim().ToString(), TxtAccNoCP.Text.Trim().ToString());
                    string OpDate = OC.GetFinStartDate(Session["EntryDate"].ToString());

                    DT = GetAccStatDetails(Session["BRCD"].ToString(), TxtProcodeCP.Text.Trim().ToString(), TxtAccNoCP.Text.Trim().ToString(), OpDate);

                    if (DT.Rows.Count > 0)
                    {
                        grdAccStatement.DataSource = DT;
                        grdAccStatement.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('.bs-example-modal-lg').modal('show');</script>", false);
                    }

                }
                else if (PAYMAST == "CASHR")
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
                    txtPanR.Text = PanCard(Session["BRCD"].ToString(), DT.Rows[0]["CustNo"].ToString());//added by ankita 22/07/2017
                    if (TxtAccnoCR.Text != "" && TxtProcodeCR.Text != "")
                    {
                        string[] TD = Session["EntryDate"].ToString().Split('/');
                        TxtOldBalCR.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcodeCR.Text, TxtAccnoCR.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), GLCODE).ToString();
                        TxtNewBalCR.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcodeCR.Text, TxtAccnoCR.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), GLCODE).ToString();
                    }
                    // added by ankita on 09/08/2017
                    TxtSplInstCR.Text = DT.Rows[0]["Spl_Instruction"].ToString();
                    acctypeno = DT.Rows[0]["OPR_TYPE"].ToString();

                    acctype = CR.GetAcctype(acctypeno);
                    TxtAcctypeCR.Text = acctype.ToString();
                    if (TxtAcctypeCR.Text == "JOINT")
                    {
                        jointname = CR.Getjointname(Session["BRCD"].ToString(), TxtAccnoCR.Text.ToString(), TxtProcodeCR.Text);
                        lbjointCR.Visible = true;
                        TxtJoinCR.Visible = true;
                        TxtJoinCR.Text = jointname.ToString();
                    }
                    else
                    {
                        lbjointCR.Visible = false;
                        TxtJoinCR.Visible = false;
                    }
                    DT = new DataTable();
                    //string OpDate = OC.GetAccOpenDate(Session["BRCD"].ToString(), TxtProcodeCR.Text.Trim().ToString(), TxtAccnoCR.Text.Trim().ToString());
                    string OpDate = OC.GetFinStartDate(Session["EntryDate"].ToString());

                    DT = GetAccStatDetails(Session["BRCD"].ToString(), TxtProcodeCR.Text.Trim().ToString(), TxtAccnoCR.Text.Trim().ToString(), OpDate);

                    if (DT.Rows.Count > 0)
                    {
                        grdAccStat.DataSource = DT;
                        grdAccStat.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('.bs-example-modal-lg').modal('show');</script>", false);
                    }

                }
                else if (PAYMAST == "LOANINST")
                {
                    BD.BindAccStatus(ddlAccStatus);
                    BD.BindPayment(ddlPayType, "1");
                    hdnamount.Value = VA.GetTotalLoanAmount(SETNO, Session["BRCD"].ToString(), Session["EntryDate"].ToString());
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
                                txtBefClbal.Text = Convert.ToDouble(VA.GetClBal(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal")).ToString();
                                txtAftClBal.Text = Convert.ToDouble(VA.GetClBal(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "MainBal")).ToString();
                                txtPanL.Text = PanCard(Session["BRCD"].ToString(), DT.Rows[0]["CustNo"].ToString());//added by ankita 22/07/2017
                            }

                            if (DT.Rows[i]["Activity"].ToString() == "3")
                            {
                                ddlPayType.SelectedIndex = Convert.ToInt32(1);
                                txtNarration.Text = "By Cash";

                                Transfer.Visible = false;
                                Transfer1.Visible = false;
                                txtDepositeAmt.Text = DT.Rows[i]["Amount"].ToString();
                                txtAmount.Text = DT.Rows[i]["Amount"].ToString();
                                txtPanL.Text = PanCard(Session["BRCD"].ToString(), DT.Rows[0]["CustNo"].ToString());//added by ankita 22/07/2017
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
                                txtPanL.Text = PanCard(Session["BRCD"].ToString(), DT.Rows[0]["CustNo"].ToString());//added by ankita 22/07/2017
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
                                txtPanL.Text = PanCard(Session["BRCD"].ToString(), DT.Rows[0]["CustNo"].ToString());//added by ankita 22/07/2017
                                txtDepositeAmt.Text = DT.Rows[i]["Amount"].ToString();
                                txtAmount.Text = DT.Rows[i]["Amount"].ToString();
                            }
                        }
                    }
                }
                else if (PAYMAST == "LOANAPP")
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
                                txtBefClbal.Text = Convert.ToDouble(VA.GetClBal(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal")).ToString();
                                txtAftClBal.Text = Convert.ToDouble(VA.GetClBal(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "MainBal")).ToString();
                                txtPanL.Text = PanCard(Session["BRCD"].ToString(), DT.Rows[0]["CustNo"].ToString());//added by ankita 22/07/2017
                            }

                            if (DT.Rows[i]["Activity"].ToString() == "3")
                            {
                                ddlPayType.SelectedIndex = Convert.ToInt32(1);
                                txtNarration.Text = "By Cash";

                                Transfer.Visible = false;
                                Transfer1.Visible = false;
                                txtDepositeAmt.Text = DT.Rows[i]["Amount"].ToString();
                                txtAmount.Text = DT.Rows[i]["Amount"].ToString();
                                txtPanL.Text = PanCard(Session["BRCD"].ToString(), DT.Rows[0]["CustNo"].ToString());//added by ankita 22/07/2017
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
                                txtPanL.Text = PanCard(Session["BRCD"].ToString(), DT.Rows[0]["CustNo"].ToString());//added by ankita 22/07/2017
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
                                txtPanL.Text = PanCard(Session["BRCD"].ToString(), DT.Rows[0]["CustNo"].ToString());//added by ankita 22/07/2017
                                txtDepositeAmt.Text = DT.Rows[i]["Amount"].ToString();
                                txtAmount.Text = DT.Rows[i]["Amount"].ToString();
                            }
                        }
                    }
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
                    txtPanD.Text = PanCard(Session["BRCD"].ToString(), DT.Rows[0]["CustNo"].ToString());//added by ankita 22/07/2017
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

                    string Close_t = DT.Rows[0]["PAY_MODE"].ToString();
                    if (Close_t == "RENEW")
                        RdbTDRenew.Checked = true;
                    else
                        RdbTDClose.Checked = true;

                    TxtTDPCode1.Text = DT.Rows[0]["TRFPRD"].ToString();
                    TxtTDPName1.Text = DT.Rows[0]["TRFPRDNAME"].ToString();
                    TxtTDAccno1.Text = DT.Rows[0]["TRFACCNO"].ToString();
                    TxtTDAccName1.Text = DT.Rows[0]["TRFACCNAME"].ToString();
                    TxtTNarration.Text = DT.Rows[0]["P1"].ToString();


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

                else if (PAYMAST == "DDSCLOSE")
                {
                    //id	Agcd	AgName	Accno	AccName	AccSts	AccStsName	AccType	AccTypeName	ODate	Period	CDate	
                    //SIntsruc	CBal	PayType	PartPayAmt	Activity	Provi	CalcIntr	PrematCommi	OtherRec	
                    //  PayableIntr	TotPayAmt	PayMode	TrfPrdCode	TrfPrdName	TrfAccno	TrfAccName	InstruNo	Intstrudate

                    TxtDDSAGCD.Text = DT.Rows[0]["Agcd"].ToString();
                    TxtDDSAGName.Text = DT.Rows[0]["AgName"].ToString();
                    TxtDDSAccNo.Text = DT.Rows[0]["Accno"].ToString();
                    TxtDDSAccName.Text = DT.Rows[0]["AccName"].ToString();
                    TxtDDSOpeningDate.Text = DT.Rows[0]["ODate"].ToString().Replace(" 12:00:00", "");
                    
                    TxtDDSCBal.Text = DT.Rows[0]["CBal"].ToString();
                    TxtDDSCalcInt.Text = DT.Rows[0]["CalcIntr"].ToString();
                    TxtDDSPayAmt.Text = Convert.ToDecimal(Convert.ToDecimal(DT.Rows[0]["CBal"]) + Convert.ToDecimal(DT.Rows[0]["CalcIntr"])).ToString();
                    txtTotalWithdraw.Text = DT.Rows[0]["PartPayAmt"].ToString();

                    if (DT.Rows[0]["PayMode"].ToString() == "" || DT.Rows[0]["PayMode"].ToString() == null)
                    {
                        lblTitle.InnerText = "";
                        lblTrf.Text = "";
                    }
                    else if (DT.Rows[0]["PayMode"].ToString() == "CASH")
                    {
                        lblTitle.InnerText = "Paid By Cash";
                        lblTrf.Text = "Cash To";
                        Lbl_ChqNo.Text = "";
                        Lbl_ChqDate.Text = "";
                    }
                    else if (DT.Rows[0]["PayMode"].ToString() == "TRANSFER")
                    {
                        lblTitle.InnerText = "Transfer";
                        lblTrf.Text = "Trf To";
                        Lbl_ChqNo.Text = "";
                        Lbl_ChqDate.Text = "";
                    }
                    else if (DT.Rows[0]["PayMode"].ToString() == "CHEQUE")
                    {
                        lblTitle.InnerText = "Paid by Cheque";
                        lblTrf.Text = "Cheque To";
                        Lbl_ChqNo.Text = DT.Rows[0]["InstruNo"].ToString();
                        Lbl_ChqDate.Text = DT.Rows[0]["Intstrudate"].ToString();
                    }

                    TxtDDSPreMCAMT.Text = DT.Rows[0]["PrematCommi"].ToString();
                    TxtDDSServCHRS.Text = DT.Rows[0]["OtherRec"].ToString();
                    TxtGstAmt.Text = DT.Rows[0]["GSTAmt"].ToString();
                    txtDeduction.Text = (Convert.ToDouble(DT.Rows[0]["PrematCommi"]) + Convert.ToDouble(DT.Rows[0]["OtherRec"]) + Convert.ToDouble(DT.Rows[0]["GSTAmt"])).ToString(); // GST Added by Abhishek on demand by Darade sir 11/08/2017
                  //  txtNet.Text = Convert.ToDecimal(Convert.ToDecimal(txtTotalWithdraw.Text) - Convert.ToDecimal(txtDeduction.Text)).ToString();
                    txtNet.Text = Convert.ToDecimal(Convert.ToDecimal(TxtDDSPayAmt.Text) - Convert.ToDecimal(txtDeduction.Text)).ToString();
                    TxtDDSPType.Text = DT.Rows[0]["TrfPrdCode"].ToString();
                    TxtDDSPTName.Text = DT.Rows[0]["TrfPrdName"].ToString();
                    TxtDDSTAccNo.Text = DT.Rows[0]["TrfAccno"].ToString();
                    TxtDDSTAName.Text = DT.Rows[0]["TrfAccName"].ToString();

                    txtPanDDS.Text = PanCard(Session["BRCD"].ToString(), DT.Rows[0]["CustNo"].ToString());//added by ankita 22/07/2017
                }

                else if (PAYMAST == "MULTITRANSFER" || PAYMAST == "INV-CLO")
                {
                    DataTable DT1 = new DataTable();
                    DT1 = VA.GetDetails_ToFill1(SETNO, BRCD, Session["EntryDate"].ToString(), PAYMAST, Scroll);
                    //hdnscroll.Value = Scroll;
                    if (DT1.Rows.Count > 0)
                    {
                        lblset.Text = ViewState["SETNO"].ToString();
                        lblScroll.Text = Scroll;

                        if (DT1.Rows[0]["PMTMode"].ToString() == "TR") //Added by abhishek for DDS CLOSURE req for balance
                            rbtnTransferType.SelectedValue = "T";
                        else
                            rbtnTransferType.SelectedValue = "C";

                        DdlCRDR.SelectedValue = DT1.Rows[0]["TRXTYPE"].ToString();
                        TxtCRBAL.Text = DT1.Rows[0]["CREDIT"].ToString();
                        TxtDRBAL.Text = DT1.Rows[0]["DEBIT"].ToString();
                        TextBox1.Text = DT1.Rows[0]["SUBGLCODE"].ToString();
                        TxtPname.Text = DT1.Rows[0]["GLNAME"].ToString();
                        TextBox2.Text = DT1.Rows[0]["CUSTNO"].ToString();
                        TextBox3.Text = DT1.Rows[0]["ACCNO"].ToString();
                        TxtCustName.Text = DT1.Rows[0]["CUSTNAME"].ToString();
                        txtPanM.Text = PanCard(Session["BRCD"].ToString(), TextBox2.Text);//added by ankita 22/07/2017
                        TxtBalance.Text = "";
                        TxtTotalBal.Text = "";
                        TxtChequeNo.Text = DT1.Rows[0]["INSTRUMENTNO"].ToString();
                        TxtPassMUL.Text = "";

                        TxtChequeDate.Text = DT1.Rows[0]["INSTRUMENTDATE"].ToString();
                        TextBox4.Text = DT1.Rows[0]["PARTICULARS2"].ToString();
                        TextBox5.Text = DT1.Rows[0]["AMOUNT"].ToString();
                        TxtBalance.Text = MV.GetOpenClose(Session["BRCD"].ToString(), TextBox1.Text.Trim().ToString(), TextBox3.Text, "0", Session["EntryDate"].ToString(), "ClBal").ToString();
                        TxtTotalBal.Text = MV.GetOpenClose(Session["BRCD"].ToString(), TextBox1.Text.Trim().ToString(), TextBox3.Text, "0", Session["EntryDate"].ToString(), "MainBal").ToString();
                        TxtDiff.Text = (Convert.ToInt32(DT1.Rows[0]["CREDIT"]) - Convert.ToInt32(DT1.Rows[0]["DEBIT"])).ToString();
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("$('#MULTITRANSFER').modal('show');");
                        sb.Append(@"</script>");
                        ShowImage(Modal_Flag);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                    }
                    else
                    {
                        WebMsgBox.Show("All Entries Authorized Successfully!!!", this.Page);
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("location.reload();");
                        sb.Append(@"</script>");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                    }
                }
                else if (PAYMAST == "TDRENEWAL")
                {
                    BD.BindIntrstPayout(DdlRIntPayout);
                    TxtRPrdDb.Text = DT.Rows[0]["DrSubgl"].ToString();
                    TxtRPrdDbName.Text = DT.Rows[0]["DrSubglName"].ToString();
                    TxtRAccDb.Text = DT.Rows[0]["DrAccno"].ToString();
                    TxtRAccDbName.Text = DT.Rows[0]["DrCustName"].ToString();

                    TxtRIntPayable.Text = DT.Rows[0]["IntPayabale"].ToString();
                    TxtRIntApplied.Text = DT.Rows[0]["IntApplied"].ToString();

                    TxtRPrdCr.Text = DT.Rows[0]["CrSubgl"].ToString();
                    TxtRPrdCrName.Text = DT.Rows[0]["CrSubglName"].ToString();
                    TxtRAccCr.Text = DT.Rows[0]["CrAccno"].ToString();
                    TxtRAccCrName.Text = DT.Rows[0]["CrCustName"].ToString();

                    TxtRIntTrfPRCD.Text = DT.Rows[0]["IntTrfSubgl"].ToString();
                    TxtRIntTrfPrdname.Text = DT.Rows[0]["IntTrfPrdName"].ToString();
                    TxtRIntTrfAccno.Text = DT.Rows[0]["IntTrfAccno"].ToString();
                    TxtRIntTrfAccname.Text = DT.Rows[0]["IntTrfCustName"].ToString();

                    TxtRDeposDate.Text = DT.Rows[0]["OpeningDate"].ToString();
                    TxtRDepoAmt.Text = DT.Rows[0]["DepoAmt"].ToString();
                    TxtRDueDate.Text = DT.Rows[0]["DueDate"].ToString();
                    DdlRIntPayout.SelectedValue = DT.Rows[0]["IntPayOut"].ToString();
                    TxtRPeriod.Text = DT.Rows[0]["Period"].ToString();
                    TxtRRate.Text = DT.Rows[0]["RateOfInt"].ToString();
                    TxtRReceiptNo.Text = DT.Rows[0]["ReceiptNo"].ToString();
                    TxtRProcode4.Text = DT.Rows[0]["TrfDepSubgl"].ToString();
                    TxtRMaturity.Text = DT.Rows[0]["MaturityAmount"].ToString();
                    TxtRIntrest.Text = DT.Rows[0]["IntAmount"].ToString();
                    RdbSingle.Checked = true;
                    ddlduration.SelectedValue = DT.Rows[0]["PeriodType"].ToString();

                    if (TxtRIntTrfPRCD.Text != "0")
                    {
                        rdbOP.Checked = true;
                    }
                    else
                    {
                        RdbPI.Checked = true;
                    }
                    if (TxtRProcode4.Text != "0")
                    {
                        int.TryParse(TxtRProcode4.Text, out result);
                        TxtRProName4.Text = customcs.GetProductName(result.ToString(), Session["BRCD"].ToString());

                    }
                    else
                    {
                        TxtRProName4.Text = "NA";
                    }

                    TxtRAccNo4.Text = DT.Rows[0]["TrfDepAccno"].ToString();
                    if (TxtRAccNo4.Text != "0")
                    {
                        TxtRAccName4.Text = BD.GetAccName(TxtRAccNo4.Text, TxtRProcode4.Text, Session["BRCD"].ToString());
                    }
                    else
                    {
                        TxtRAccName4.Text = "NA";
                    }

                }
            }
            else
            {
                lblMessage.Text = "Set Not Found...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public string encrypt(string encryptString)
    {
        string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {  
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                encryptString = Convert.ToBase64String(ms.ToArray());
            }
        }
        return encryptString;
    }

    public string Decrypt(string cipherText)
    {
        string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {  
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76  
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    protected void BtnModal_AuthorizeCP_Click(object sender, EventArgs e)
    {
        try
        {

            int MID = AT.GetSetMid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SETNO"].ToString());

            if (MID != Convert.ToInt32(Session["MID"].ToString()))
            {
                #region Change by Amruta 03/06/2017
                DataTable Dt1 = new DataTable();
                Dt1 = AT.GetAmount(Session["BRCD"].ToString(), ViewState["SETNO"].ToString(), Session["EntryDate"].ToString());
                if (Dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt1.Rows.Count; i++)
                    {
                        string str = "";
                        str = Dt1.Rows[i]["Amount"].ToString();
                        str = encrypt(str);
                        byte[] str1 = Encoding.ASCII.GetBytes(str);
                        //AT.UpdateAmount(Dt1.Rows[i]["AID"].ToString(), Dt1.Rows[i]["SubGLCODE"].ToString(), Dt1.Rows[i]["TRXTYPE"].ToString(), Dt1.Rows[i]["Amount"].ToString(), str,ViewState["SETNO"].ToString(),Session["BRCD"].ToString(),Session["EntryDate"].ToString());
                        string[] TD = Session["EntryDate"].ToString().Split('/');
                        string TBNAME = "";
                        TBNAME = TD[2].ToString() + TD[1].ToString();
                        SqlConnection con = new SqlConnection(conn.DbName());
                        SqlCommand cmd = new SqlCommand("update AVSM_" + TBNAME + " set AMOUNT_2=@Amount_2 where SETNO = @SetNo AND BrCd = @BRCD AND  ENTRYDATE =@EDAT and AID=@AID and SUBGLCODE=@SUBGL and TRXTYPE=@TRX and Amount=@AMOUNT", con);

                        cmd.Parameters.Add("@Amount_2", SqlDbType.Binary).Value = str1;
                        cmd.Parameters.Add("@SetNo", SqlDbType.VarChar).Value = ViewState["SETNO"].ToString();
                        cmd.Parameters.Add("@BRCD", SqlDbType.Int).Value = Session["BRCD"].ToString();
                        cmd.Parameters.Add("@AID", SqlDbType.Float).Value = Convert.ToDouble(Dt1.Rows[i]["AID"]);
                        cmd.Parameters.Add("@EDAT", SqlDbType.DateTime).Value = conn.ConvertDate(Session["EntryDate"].ToString());
                        cmd.Parameters.Add("@SUBGL", SqlDbType.VarChar).Value = Dt1.Rows[i]["SubGLCODE"].ToString();
                        cmd.Parameters.Add("@TRX", SqlDbType.VarChar).Value = Dt1.Rows[i]["TRXTYPE"].ToString();
                        cmd.Parameters.Add("@AMOUNT", SqlDbType.Float).Value = Convert.ToDouble(Dt1.Rows[i]["Amount"]);


                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                //byte[] sd1 = (byte[])Dt1.Rows[0]["Amount_2"];
                //string sd = Encoding.ASCII.GetString(sd1);
                //Decrypt(sd);
                #endregion
                if (ViewState["MODAL"].ToString() == "LOANINST")//Loan ////Dhanya Shetty//03-07-2017
                {

                    if (Txtentramt.Text != "")
                    {
                        EAMT = Convert.ToDouble(Txtentramt.Text);
                        AMT = Convert.ToDouble(txtAmount.Text);
                        if (EAMT == AMT)
                        {
                            Result = AT.AuthoriseEntryLoan(Session["BRCD"].ToString(), ViewState["SETNO"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());

                            //added by amol on 26/07/2017 For Insert record into avs1092 table
                            if (Result > 0)
                            {
                                Balance = BD.ClBalance(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal");
                                Message = "Your a/c no " + Convert.ToInt32(txtAccNo.Text.Trim().ToString()) + " has been credited with Rs " + Convert.ToDouble(hdnamount.Value.Trim().ToString()) + ". Available a/c balance is Rs " + Balance + ".";
                                BD.InsertSMSRec(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Message, MID.ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "Receipt");
                            }
                        }
                        else
                        {
                            WebMsgBox.Show("Pass Amount does not Match with Amount to authorise...!!", this.Page);
                            Txtentramt.Text = "";
                            Txtentramt.Focus();
                            string Modal_Flag = "LOANINST";
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append(@"<script type='text/javascript'>");
                            sb.Append("$('#" + Modal_Flag + "').modal('show');");
                            sb.Append(@"</script>");
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                        }
                    }
                    else
                    {

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("$('#" + ViewState["MODAL"].ToString() + "').modal('show');");
                        sb.Append("var control = document.getElementById('<% =Txtentramt.ClientID%>');");
                        sb.Append(" if( control != null ) control.focus();");
                        sb.Append(@"</script>");
                        ShowImage(ViewState["MODAL"].ToString());
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                        this.Page.SetFocus(Txtentramt);
                        Lbl_PassAmount.Text = "Enter Pass Amount".ToUpper();
                        Lbl_PassAmount.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    if (ViewState["MODAL"].ToString() == "CASHR")//Cash Receipt//Dhanya Shetty//06-07-2017
                    {
                        if (TxtPassCR.Text != "")
                        {
                            EAMT = Convert.ToDouble(TxtPassCR.Text);
                            AMT = Convert.ToDouble(TxtAmountCR.Text);
                            if (EAMT == AMT)
                            {
                                Result = AT.AuthoriseEntry(Session["BRCD"].ToString(), ViewState["SETNO"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["MODAL"].ToString(), ViewState["ScrollNo"].ToString());

                                //added by amol on 26/07/2017 For Insert record into avs1092 table
                                if (Result > 0)
                                {
                                    Balance = BD.ClBalance(Session["BRCD"].ToString(), TxtProcodeCR.Text.Trim().ToString(), TxtAccnoCR.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal");
                                    Message = "Your a/c no " + Convert.ToInt32(TxtAccnoCR.Text.Trim().ToString()) + " has been credited with Rs " + Convert.ToDouble(TxtPassCR.Text.Trim().ToString()) + ". Available a/c balance is Rs " + Balance + ".";
                                    BD.InsertSMSRec(Session["BRCD"].ToString(), TxtProcodeCR.Text.Trim().ToString(), TxtAccnoCR.Text.Trim().ToString(), Message, MID.ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "Receipt");
                                }
                            }
                            else
                            {
                                WebMsgBox.Show("Pass Amount does not Match with Amount to authorise...!!", this.Page);
                                TxtPassCR.Text = "";
                                string Modal_Flag = "CASHR";
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$('#" + Modal_Flag + "').modal('show');");
                                sb.Append(@"</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                            }
                        }
                        else
                        {

                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append(@"<script type='text/javascript'>");
                            sb.Append("$('#" + ViewState["MODAL"].ToString() + "').modal('show');");
                            sb.Append("var control = document.getElementById('<% =Txtentramt.ClientID%>');");
                            sb.Append(" if( control != null ) control.focus();");
                            sb.Append(@"</script>");
                            ShowImage(ViewState["MODAL"].ToString());
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                            Lbl_PassAmountCR.Text = "Enter Pass Amount".ToUpper();
                            Lbl_PassAmountCR.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else if (ViewState["MODAL"].ToString() == "LOANAPP")//Loan app ////Amruta Ahirrao//07-11-2017
                    {

                        if (Txtentramt.Text != "")
                        {
                            EAMT = Convert.ToDouble(Txtentramt.Text);
                            AMT = Convert.ToDouble(txtAmount.Text);
                            if (EAMT == AMT)
                            {
                                Result = AT.AuthoriseEntryLoan1(Session["BRCD"].ToString(), ViewState["SETNO"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());

                                //added by amol on 26/07/2017 For Insert record into avs1092 table
                                if (Result > 0)
                                {
                                    Balance = BD.ClBalance(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal");
                                    Message = "Your a/c no " + Convert.ToInt32(txtAccNo.Text.Trim().ToString()) + " has been credited with Rs " + Convert.ToDouble(Txtentramt.Text.Trim().ToString()) + ". Available a/c balance is Rs " + Balance + ".";
                                    BD.InsertSMSRec(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Message, MID.ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "Receipt");
                                }
                            }
                            else
                            {
                                WebMsgBox.Show("Pass Amount does not Match with Amount to authorise...!!", this.Page);
                                Txtentramt.Text = "";
                                Txtentramt.Focus();
                                string Modal_Flag = "LOANINST";
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$('#" + Modal_Flag + "').modal('show');");
                                sb.Append(@"</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                            }
                        }
                        else
                        {

                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append(@"<script type='text/javascript'>");
                            sb.Append("$('#" + ViewState["MODAL"].ToString() + "').modal('show');");
                            sb.Append("var control = document.getElementById('<% =Txtentramt.ClientID%>');");
                            sb.Append(" if( control != null ) control.focus();");
                            sb.Append(@"</script>");
                            ShowImage(ViewState["MODAL"].ToString());
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                            this.Page.SetFocus(Txtentramt);
                            Lbl_PassAmount.Text = "Enter Pass Amount".ToUpper();
                            Lbl_PassAmount.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else if (ViewState["MODAL"].ToString() == "CASHP")//Cash Payment//Dhanya Shetty//06-07-2017
                    {
                        if (TxtPassCP.Text != "")
                        {
                            EAMT = Convert.ToDouble(TxtPassCP.Text);
                            AMT = Convert.ToDouble(TxtAmountCP.Text);
                            if (EAMT == AMT)
                            {
                                Result = AT.AuthoriseEntry(Session["BRCD"].ToString(), ViewState["SETNO"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["MODAL"].ToString(), ViewState["ScrollNo"].ToString());

                                //added by amol on 26/07/2017 For Insert record into avs1092 table
                                if (Result > 0)
                                {
                                    Balance = BD.ClBalance(Session["BRCD"].ToString(), TxtProcodeCP.Text.Trim().ToString(), TxtAccNoCP.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal");
                                    Message = "Your a/c no " + Convert.ToInt32(TxtAccNoCP.Text.Trim().ToString()) + " has been debited with Rs " + Convert.ToDouble(TxtPassCP.Text.Trim().ToString()) + ". Available a/c balance is Rs " + Balance + ".";
                                    BD.InsertSMSRec(Session["BRCD"].ToString(), TxtProcodeCP.Text.Trim().ToString(), TxtAccNoCP.Text.Trim().ToString(), Message, MID.ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "Payment");
                                }
                            }
                            else
                            {
                                WebMsgBox.Show("Pass Amount does not Match with Amount to authorise...!!", this.Page);
                                TxtPassCP.Text = "";
                                string Modal_Flag = "CASHP";
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$('#" + Modal_Flag + "').modal('show');");
                                sb.Append(@"</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                            }
                        }
                        else
                        {

                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append(@"<script type='text/javascript'>");
                            sb.Append("$('#" + ViewState["MODAL"].ToString() + "').modal('show');");
                            sb.Append("var control = document.getElementById('<% =Txtentramt.ClientID%>');");
                            sb.Append(" if( control != null ) control.focus();");
                            sb.Append(@"</script>");
                            ShowImage(ViewState["MODAL"].ToString());
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                            Lbl_PassAmountCP.Text = "Enter Pass Amount".ToUpper();
                            Lbl_PassAmountCP.ForeColor = System.Drawing.Color.Red;
                        }
                    }

                    else if (ViewState["MODAL"].ToString() == "MULTITRANSFER")//Multiple Transfer//Dhanya Shetty//06-07-2017
                    {
                        DBT = Convert.ToDouble(TextBox5.Text);
                        CRT = Convert.ToDouble(TxtPassMUL.Text);

                        if (DBT == CRT)
                        {

                            if (TxtPassMUL.Text != "")
                            {
                                Result = AT.AuthoriseEntryMutli(Session["BRCD"].ToString(), ViewState["SETNO"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), hdnscroll.Value);
                                if (Result > 0)
                                {
                                    //added by amol on 26/07/2017 For Insert record into avs1092 table
                                    if (Result > 0 && Convert.ToDouble(TxtCRBAL.Text.ToString() == "" ? "0" : TxtCRBAL.Text.ToString()) > 0)
                                    {
                                        Balance = BD.ClBalance(Session["BRCD"].ToString(), TextBox1.Text.Trim().ToString(), TextBox3.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal");
                                        Message = "Your a/c no " + Convert.ToInt32(TextBox3.Text.Trim().ToString()) + " has been credited with Rs " + Convert.ToDouble(TxtPassMUL.Text.Trim().ToString()) + ". Available a/c balance is Rs " + Balance + ".";
                                        BD.InsertSMSRec(Session["BRCD"].ToString(), TextBox1.Text.Trim().ToString(), TextBox3.Text.Trim().ToString(), Message, MID.ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "Receipt");
                                    }
                                    else if (Result > 0 && Convert.ToDouble(TxtDRBAL.Text.ToString() == "" ? "0" : TxtDRBAL.Text.ToString()) > 0)
                                    {
                                        //added by amol on 26/07/2017 For Insert record into avs1092 table (Debit)
                                        Balance = BD.ClBalance(Session["BRCD"].ToString(), TextBox1.Text.Trim().ToString(), TextBox3.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal");
                                        Message = "Your a/c no " + Convert.ToInt32(TextBox3.Text.Trim().ToString()) + " has been debited with Rs " + Convert.ToDouble(TxtPassMUL.Text.Trim().ToString()) + ". Available a/c balance is Rs " + Balance + ".";
                                        BD.InsertSMSRec(Session["BRCD"].ToString(), TextBox1.Text.Trim().ToString(), TextBox3.Text.Trim().ToString(), Message, MID.ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "Payment");
                                    }
                                    WebMsgBox.Show("Authorized Successfully!!!", this.Page);
                                    FL = "Insert";
                                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Voucher_Multitransfr_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                    hdnscroll.Value = ((Convert.ToInt32(hdnscroll.Value)) + 1).ToString();
                                    GetVoucherDetails(hdnset.Value, Session["BRCD"].ToString(), "MULTITRANSFER", hdnscroll.Value);

                                }
                            }
                            else
                            {

                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$('#" + ViewState["MODAL"].ToString() + "').modal('show');");
                                sb.Append("var control = document.getElementById('<% =Txtentramt.ClientID%>');");
                                sb.Append(" if( control != null ) control.focus();");
                                sb.Append(@"</script>");
                                ShowImage(ViewState["MODAL"].ToString());
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                                Lbl_PassAmountMUL.Text = "Enter Pass Amount".ToUpper();
                                Lbl_PassAmountMUL.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            WebMsgBox.Show("Pass Amount does not Match with Amount to authorise...!!", this.Page);
                            TxtPassMUL.Text = "";
                            string Modal_Flag = "MULTITRANSFER";
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append(@"<script type='text/javascript'>");
                            sb.Append("$('#" + Modal_Flag + "').modal('show');");
                            sb.Append(@"</script>");
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                        }
                    }
                    else if (ViewState["MODAL"].ToString() == "TDCLOSE")//Deposit Closer//Dhanya Shetty//06-07-2017
                    {
                        if (TxtPassDepc.Text != "")
                        {
                            EAMT = Convert.ToDouble(TxtPassDepc.Text);
                            AMT = Convert.ToDouble(txtTDPayAmnt.Text);
                            if (EAMT == AMT)
                            {
                                Result = AT.AuthoriseEntry(Session["BRCD"].ToString(), ViewState["SETNO"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["MODAL"].ToString(), ViewState["ScrollNo"].ToString());

                                //added by amol on 26/07/2017 For Insert record into avs1092 table
                                if (Result > 0)
                                {
                                    Balance = BD.ClBalance(Session["BRCD"].ToString(), TxtTDProcode.Text.Trim().ToString(), TxtTDAccno.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal");
                                    Message = "Your a/c no " + Convert.ToInt32(TxtTDAccno.Text.Trim().ToString()) + " has been debited with Rs " + Convert.ToDouble(TxtPassDepc.Text.Trim().ToString()) + ". Available a/c balance is Rs " + Balance + ".";
                                    BD.InsertSMSRec(Session["BRCD"].ToString(), TxtTDProcode.Text.Trim().ToString(), TxtTDAccno.Text.Trim().ToString(), Message, MID.ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "Payment");
                                }
                            }
                            else
                            {
                                WebMsgBox.Show("Pass Amount does not Match with Amount to authorise...!!", this.Page);
                                TxtPassDepc.Text = "";
                                string Modal_Flag = "TDCLOSE";
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$('#" + Modal_Flag + "').modal('show');");
                                sb.Append(@"</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                            }
                        }
                        else
                        {

                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append(@"<script type='text/javascript'>");
                            sb.Append("$('#" + ViewState["MODAL"].ToString() + "').modal('show');");
                            sb.Append("var control = document.getElementById('<% =Txtentramt.ClientID%>');");
                            sb.Append(" if( control != null ) control.focus();");
                            sb.Append(@"</script>");
                            ShowImage(ViewState["MODAL"].ToString());
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                            Lbl_PassAmountDEPC.Text = "Enter Pass Amount".ToUpper();
                            Lbl_PassAmountDEPC.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else if (ViewState["MODAL"].ToString() == "DDSCLOSE")//DDS Closer//Dhanya Shetty//10-07-2017
                    {
                        if (TxtPassDDS.Text != "")
                        {
                            EAMT = Convert.ToDouble(TxtPassDDS.Text);
                            AMT = Convert.ToDouble(txtNet.Text);
                            if (EAMT == AMT)
                            {
                                Result = AT.AuthoriseEntry(Session["BRCD"].ToString(), ViewState["SETNO"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["MODAL"].ToString(), ViewState["ScrollNo"].ToString());

                                //added by amol on 26/07/2017 For Insert record into avs1092 table
                                if (Result > 0)
                                {
                                    string Amount = AT.GetExactAmount(Session["BRCD"].ToString(), ViewState["SETNO"].ToString(), Session["Entrydate"].ToString());//amruta 28/05/2018 as per task assign
                                    Balance = BD.ClBalance(Session["BRCD"].ToString(), TxtDDSAGCD.Text.Trim().ToString(), TxtDDSAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal");
                                    Message = "Your A/C no " + Convert.ToInt32(TxtDDSAccNo.Text).ToString().PadLeft(4, 'X') + " has been debited with Rs " + Convert.ToDouble(Amount.Trim().ToString()) + " by cash Available balance is Rs " + Balance + ".";

                                    BD.InsertSMSRec(Session["BRCD"].ToString(), TxtDDSAGCD.Text.Trim().ToString(), TxtDDSAccNo.Text.Trim().ToString(), Message, MID.ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), "Payment");
                                }
                            }
                            else
                            {
                                WebMsgBox.Show("Pass Amount does not Match with NetPayable to authorise...!!", this.Page);
                                TxtPassDDS.Text = "";
                                string Modal_Flag = "DDSCLOSE";
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$('#" + Modal_Flag + "').modal('show');");
                                sb.Append(@"</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                            }
                        }
                        else
                        {

                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append(@"<script type='text/javascript'>");
                            sb.Append("$('#" + ViewState["MODAL"].ToString() + "').modal('show');");
                            sb.Append("var control = document.getElementById('<% =Txtentramt.ClientID%>');");
                            sb.Append(" if( control != null ) control.focus();");
                            sb.Append(@"</script>");
                            ShowImage(ViewState["MODAL"].ToString());
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                            Lbl_PassDDS.Text = "Enter Pass Amount".ToUpper();
                            Lbl_PassDDS.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else if (ViewState["MODAL"].ToString() == "TDRENEWAL")//FD Renew//Dhanya Shetty//04-09-2017
                    {
                        if (TxtFTDpass.Text != "")
                        {
                            //int BNK = Convert.ToInt32( VA.Getbankcode());
                            //if (BNK == 100)
                            //{
                            //    EAMT = Convert.ToDouble(TxtFTDpass.Text);
                            //    AMT = Convert.ToDouble(TxtRMaturity.Text);
                            //}
                            //else
                            //{
                            EAMT = Convert.ToDouble(TxtFTDpass.Text);
                            AMT = Convert.ToDouble(TxtRDepoAmt.Text);
                            //}
                            if (EAMT == AMT)
                            {
                                Result = AT.AuthoriseEntry(Session["BRCD"].ToString(), ViewState["SETNO"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["MODAL"].ToString(), ViewState["ScrollNo"].ToString());
                            }
                            else
                            {
                                WebMsgBox.Show("Pass Amount does not Match with Amount to authorise...!!", this.Page);
                                TxtFTDpass.Text = "";
                                string Modal_Flag = "TDRENEWAL";
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("$('#" + Modal_Flag + "').modal('show');");
                                sb.Append(@"</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);

                            }
                        }
                        else
                        {

                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append(@"<script type='text/javascript'>");
                            sb.Append("$('#" + ViewState["MODAL"].ToString() + "').modal('show');");
                            sb.Append("var control = document.getElementById('<% =Txtentramt.ClientID%>');");
                            sb.Append(" if( control != null ) control.focus();");
                            sb.Append(@"</script>");
                            ShowImage(ViewState["MODAL"].ToString());
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                            Lbl_TDFPass.Text = "Enter Pass Amount".ToUpper();
                            Lbl_TDFPass.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        Result = AT.AuthoriseEntry(Session["BRCD"].ToString(), ViewState["SETNO"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), ViewState["MODAL"].ToString(), ViewState["ScrollNo"].ToString());
                    }
                }

                if (Result > 0)
                {
                    WebMsgBox.Show("SetNo " + ViewState["SETNO"].ToString() + " Successfully Authorised...!!", this.Page);
                    BindData();
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Voucher_Autho_" + ViewState["SETNO"].ToString() + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "", "alert('Entry " + ViewState["SETNO"].ToString() + " sucessfully authorized....!');", true);
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
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnModal_CancelInst_Click(object sender, EventArgs e)
    {
        try
        {
            int MID = AT.GetSetMid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SETNO"].ToString());

            if (MID != Convert.ToInt32(Session["MID"].ToString()))
            {
                if (ViewState["MODAL"].ToString() == "LOANINST")
                {
                    Result = AT.CancelEntryLoan(Session["BRCD"].ToString(), txtProdType.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), ViewState["SETNO"].ToString(), MID.ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());
                }
                else
                {
                    Result = AT.CancelEntry(Session["BRCD"].ToString(), ViewState["SETNO"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());
                }

                if (Result > 0)
                {
                    WebMsgBox.Show("SetNo " + ViewState["SETNO"].ToString() + " Sucessfully Canceled...!!", this.Page);
                    BindData();
                    FL = "Insert";
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Voucher_CancelEntry_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "", "alert('Entry " + ViewState["SETNO"].ToString() + " sucessfully authorized....!');", true);
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

            DT = OC.GetAccStatDetails(DTF[1].ToString(), DTT[1].ToString(), DTF[2].ToString(), DTT[2].ToString(), FinDate.ToString(), Session["EntryDate"].ToString(), AccNo.ToString(), PrCode.ToString(), Session["BRCD"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    protected void TxtUserId_TextChanged(object sender, EventArgs e)
    {
        string[] TD = TxtUserId.Text.Split('_');
        if (TD.LongLength > 1)
        {
            TxtUserId.Text = TD[0].ToString();
            ViewState["UID"] = TD[1].ToString();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    public void BindData()
    {
        try
        {
            string PRD = "", UID = "", ST = "", STR = "";

            if (TxtPType.Text.ToString() == "")
                PRD = "";
            else
                PRD = " And A.SubGlCode = '" + TxtPType.Text.ToString() + "'";

            if (TxtUserId.Text.ToString() == "")
                UID = "";
            else
                UID = " And A.Mid = '" + VA.GetUserMID(Session["LOGINCODE"].ToString()).ToString() + "'";

            if (TxtSetno.Text.ToString() == "")
                ST = "";
            else
                ST = " And A.SetNo = '" + TxtSetno.Text.ToString() + "' ";

            STR = PRD + UID + ST;
            //  Display voucher into grid here
            VA.GetVoucherInfo(grdCashRct, STR, Session["BRCD"].ToString(), Session["EntryDate"].ToString());
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

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        ClearData();
    }

    public void ClearData()
    {
        TxtPType.Text = "";
        TxtUserId.Text = "";
        TxtSetno.Text = "";
    }

    protected void btn_Statement_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    public void BindGrid()
    {
        try
        {
            string Modal_Flag = "DDSCLOSE";
            DT = new DataTable();

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
                //DIVPHOTO.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public DataTable GetAccStatDetails(string FinDate)
    {
        try
        {
            DT = new DataTable();
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

    public void ShowImage(string Modal_Flag)
    {
        try
        {
            DataTable dt = new DataTable();
            if (Modal_Flag == "LOANINST")
                dt = GetData("select id,SignName,PhotoName,SignIMG,PhotoImg from  Imagerelation where BRCD='" + Session["BRCD"].ToString() + "' and CustNo=" + txtCustNo.Text + "");
            if (Modal_Flag == "CASHR")
            {
                DataTable CN = new DataTable();
                CN = VA.GetCustName(TxtProcodeCR.Text, TxtAccnoCR.Text, Session["BRCD"].ToString());
                if (CN.Rows.Count > 0)
                {
                    string[] CustName = CN.Rows[0]["CustName"].ToString().Split('_');
                    dt = GetData("select id,SignName,PhotoName,SignIMG,PhotoImg from  Imagerelation where BRCD='" + Session["BRCD"].ToString() + "' and CustNo=" + CustName[1].ToString() + "");
                }

            }
            if (Modal_Flag == "DDSCLOSE")
            {

                DataTable CN = new DataTable();
                CN = VA.GetCustName(TxtDDSAGCD.Text, TxtDDSAccNo.Text, Session["BRCD"].ToString());
                if (CN.Rows.Count > 0)
                {
                    string[] CustName = CN.Rows[0]["CustName"].ToString().Split('_');
                    dt = GetData("select id,SignName,PhotoName,SignIMG,PhotoImg from  Imagerelation where BRCD='" + Session["BRCD"].ToString() + "' and CustNo=" + CustName[1].ToString() + "");
                }
            }
            if (Modal_Flag == "TDCLOSE")
            {
                dt = GetData("select id,SignName,PhotoName,SignIMG,PhotoImg from  Imagerelation where BRCD='" + Session["BRCD"].ToString() + "' and CustNo=" + TxtTDCustno.Text + "");
            }
            if (Modal_Flag == "CASHP")
            {
                DataTable CN = new DataTable();
                CN = VA.GetCustName(TxtProcodeCP.Text, TxtAccNoCP.Text, Session["BRCD"].ToString());
                if (CN.Rows.Count > 0)
                {
                    string[] CustName = CN.Rows[0]["CustName"].ToString().Split('_');
                    dt = GetData("select id,SignName,PhotoName,SignIMG,PhotoImg from  Imagerelation where BRCD='" + Session["BRCD"].ToString() + "' and CustNo=" + CustName[1].ToString() + "");
                }
            }
            ////string SaveLocation = Server.MapPath("~/Uploads/");
            ////string[] filePaths = Directory.GetFiles(Server.MapPath("~/Uploads/")); //Get File List in chosen directory
            ////List<ListItem> files = new List<ListItem>();
            string FileName = "";
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > Convert.ToInt32(hdnRow.Value))
                {

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
                            if (Modal_Flag == "LOANINST")
                            {
                                if (y == 0)
                                {
                                    image1.Src = "data:image/tif;base64," + base64String;
                                }
                                else if (y == 1)
                                {
                                    image2.Src = "data:image/tif;base64," + base64String;
                                }
                            }
                            if (Modal_Flag == "CASHR")
                            {
                                if (y == 0)
                                {
                                    Img1.Src = "data:image/tif;base64," + base64String;
                                }
                                else if (y == 1)
                                {
                                    Img2.Src = "data:image/tif;base64," + base64String;
                                }
                            }
                            if (Modal_Flag == "DDSCLOSE")
                            {
                                if (y == 0)
                                {
                                    Img3.Src = "data:image/tif;base64," + base64String;
                                }
                                else if (y == 1)
                                {
                                    Img4.Src = "data:image/tif;base64," + base64String;
                                }
                            }
                            if (Modal_Flag == "TDCLOSE")
                            {
                                if (y == 0)
                                {
                                    Img5.Src = "data:image/tif;base64," + base64String;
                                }
                                else if (y == 1)
                                {
                                    Img6.Src = "data:image/tif;base64," + base64String;
                                }
                            }
                            if (Modal_Flag == "CASHP")
                            {
                                if (y == 0)
                                {
                                    Img7.Src = "data:image/tif;base64," + base64String;
                                }
                                else if (y == 1)
                                {
                                    Img8.Src = "data:image/tif;base64," + base64String;
                                }
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
                Img1.Src = "";
                Img2.Src = "";
            }
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
    }

    protected void Rdb_Single_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            Div_Lot.Visible = false;
            Div_Single.Visible = true;
            btnSearch.Visible = true;
            Btn_Submit.Visible = false;
            TxtPType.Focus();
            btnSearch.Text = "Submit";
            grdCashRct.Columns[8].Visible = true;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void Rdb_Lot_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            Div_Lot.Visible = true;
            Div_Single.Visible = false;
            btnSearch.Visible = true;
            Btn_Submit.Visible = true;
            btnSearch.Text = "Search";
            TxtFSetno.Focus();
            grdCashRct.Columns[8].Visible = false;

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void TxtFSetno_TextChanged(object sender, EventArgs e)
    {
        TxtTSetno.Focus();
    }

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtFSetno.Text == "")
            {
                FST = TxtTSetno.Text;
                TST = TxtTSetno.Text;
            }
            else if (TxtTSetno.Text == "")
            {
                FST = TxtFSetno.Text;
                TST = TxtFSetno.Text;
            }
            else
            {
                FST = TxtFSetno.Text;
                TST = TxtTSetno.Text;
            }
            DT = AT.GetSetMidL("Mid", Session["EntryDate"].ToString(), FST, TST, Session["BRCD"].ToString(), Session["MID"].ToString());
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    Setno += DT.Rows[i]["SETNO"].ToString() + " ";
                }
                WebMsgBox.Show("User is restricted to authorise set numbers : " + Setno, this.Page);

            }
            else
            {

                //result = AT.UpdtLotSt("SETAUTH", Session["EntryDate"].ToString(), FST, TST, Session["BRCD"].ToString(), Session["MID"].ToString());
                VA.DisplayLotUntallySet(GrdLotUntally, "SETAUTH", Session["EntryDate"].ToString(), FST, TST, Session["BRCD"].ToString(), Session["MID"].ToString());
                //if (result > 0)
                //{
                //    WebMsgBox.Show("Set Numbers From " + FST + " to " + TST + " authorised successfully..!!", this.Page);
                Div_LotpassingUntally.Visible = true;
                BindData();
                FL = "Insert";
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Voucher_Set_Autho_" + FST + "_" + TST + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                TxtFSetno.Text = "";
                TxtTSetno.Text = "";

                //}
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

}