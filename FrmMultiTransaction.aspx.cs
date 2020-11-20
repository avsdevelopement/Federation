using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class FrmMultiTransaction : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsInsertTrans ITrans = new ClsInsertTrans();
    ClsLoanInfo LI = new ClsLoanInfo();
    ClsAccopen AO = new ClsAccopen();
    ClsAuthorized AT = new ClsAuthorized();
    ClsMultiTransaction MT = new ClsMultiTransaction();
    ClsOpenClose OC = new ClsOpenClose();
    ClsAccopen accop = new ClsAccopen();
    ClsCommon CC = new ClsCommon();
    DbConnection conn = new DbConnection();
    ClsCashReciept CR = new ClsCashReciept();
    DataTable Image = new DataTable();
    DataTable DT2 = new DataTable();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string RefNumber, FL = "";
    int resultout = 0, ShareSuspGl = 0, ShareAccNo = 0;
    string GlCode = "", ST = "", SetNo = "";
    string sResult = "", Activity = "";
    double DebitAmt, PrincAmt, TotalDrAmt, TotalAmt = 0;
    double SurTotal, OtherChrg, CurSurChrg = 0;
    private string Param;
    string AC1 = "";
    string[] CustName ;

    #region Page Load

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
                if (Convert.ToString(Request.QueryString["FL"]) == "TD" || Convert.ToString(Request.QueryString["FL"]) == "ACO")
                {
                    ddlPMTMode.SelectedValue = "2";
                    TxtPtype.Text = Convert.ToString(Request.QueryString["P"]);
                    AC1 = MT.Getaccno(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString());

                    if (AC1 != null)
                    {
                        string[] AC = AC1.Split('_'); ;
                        ViewState["GlCode"] = AC[0].ToString();
                        TxtPname.Text = AC[1].ToString();
                       // TxtCRBAL.Text = Convert.ToString(Request.QueryString["AMT"]);
                        TxtCRBAL.Text = "0";

                        TxtDRBAL.Text = "0";
                        TxtDiff.Text = "0";
                        TxtAccNo.Text = Convert.ToString(Request.QueryString["A"]);
                        DT = MT.GetCustName(TxtPtype.Text, TxtAccNo.Text, Session["BRCD"].ToString());
                        if (DT.Rows.Count > 0)
                        {
                            CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                            TxtCustName.Text = CustName[0].ToString();
                            txtCustNo.Text = CustName[1].ToString();
                        }
                        DdlCRDR.Items.Clear();
                        DdlCRDR.Items.Insert(0, new ListItem("--Select--", "0"));
                        DdlCRDR.Items.Insert(1, new ListItem("Credit", "1"));
                        DdlCRDR.Items.Insert(2, new ListItem("Debit", "2"));
                        divCheque.Visible = false;
                        DdlCRDR.Focus();
                        DdlCRDR.SelectedValue = "1";


                        TxtTotalBal.Text ="0";
                        TxtBalance.Text = "0";
                       // TxtBalance.Text = Convert.ToString(Request.QueryString["AMT"]);
                        TxtAmount.Text = Convert.ToString(Request.QueryString["AMT"]);
                        DdlCRDR.Focus();

                    }


                }
                else
                {


                    autoglname.ContextKey = Session["BRCD"].ToString();
                    TxtChequeDate.Text = Session["EntryDate"].ToString();
                    //MV.DelAllRecTable(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    Getinfo();
                    BindGrid();
                    ddlPMTMode.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Index Changed Event

    protected void ddlPMTMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPMTMode.SelectedValue.ToString() == "1")
            {
                DdlCRDR.Items.Clear();
                DdlCRDR.Items.Insert(0, new ListItem("--Select--", "0"));
                DdlCRDR.Items.Insert(1, new ListItem("Receipt", "1"));
                DdlCRDR.Items.Insert(2, new ListItem("Payment", "2"));
                divCheque.Visible = false;
                DdlCRDR.Focus();
            }
            else if (ddlPMTMode.SelectedValue.ToString() == "2")
            {
                DdlCRDR.Items.Clear();
                DdlCRDR.Items.Insert(0, new ListItem("--Select--", "0"));
                DdlCRDR.Items.Insert(1, new ListItem("Credit", "1"));
                DdlCRDR.Items.Insert(2, new ListItem("Debit", "2"));
                divCheque.Visible = false;
                DdlCRDR.Focus();
            }
            else if (ddlPMTMode.SelectedValue.ToString() == "3")
            {
                DdlCRDR.Items.Clear();
                DdlCRDR.Items.Insert(0, new ListItem("--Select--", "0"));
                DdlCRDR.Items.Insert(1, new ListItem("Credit", "1"));
                DdlCRDR.Items.Insert(2, new ListItem("Debit", "2"));
                divCheque.Visible = true;
                DdlCRDR.Focus();
            }
            else
            {
                ddlPMTMode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void DdlCRDR_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DdlCRDR.SelectedValue != "0")
                TxtPtype.Focus();
            else
                DdlCRDR.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grdvoucher_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdvoucher.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grdvoucher_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    #endregion

    #region Text Change Event

    protected void TxtPtype_TextChanged(object sender, EventArgs e)
    {
        try
        {
            
            //Added By Amol on 22092017 as per ambika mam instruction
            if (BD.GetProdOperate(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString()).ToString() != "3")
            {
                AC1 = MT.Getaccno(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString());

                if (AC1 != null)
                {
                    string[] AC = AC1.Split('_'); ;
                    ViewState["GlCode"] = AC[0].ToString();
                    TxtPname.Text = AC[1].ToString();

                    //  Added By amol On 2018-02-06 for share account name search
                    if ((TxtPtype.Text == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))//changes by ankita checked prod code instead of glcode 06/03/2018 06/03/2018
                    {
                        Param = CC.getShrParam();
                        if (Param == "HO" || Param == "ho" || Param == "Ho")
                            AutoAccname.ContextKey = "1" + "_" + TxtPtype.Text + "_" + ViewState["GlCode"].ToString();
                        else
                            AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtPtype.Text + "_" + ViewState["GlCode"].ToString();
                    }
                    else
                        AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtPtype.Text + "_" + ViewState["GlCode"].ToString();

                    string YN = CC.GetIntACCYN(Session["BRCD"].ToString(), TxtPtype.Text);
                    if ((Convert.ToInt32(ViewState["GlCode"].ToString() == "" ? "0" : ViewState["GlCode"].ToString()) >= 100 && YN != "Y") || (Convert.ToInt32(ViewState["GlCode"].ToString() == "" ? "0" : ViewState["GlCode"].ToString()) == 99)) //--abhishek as per GL LEVEL Requirment
                    {
                        TxtAccNo.Text = "";
                        TxtCustName.Text = "";
                        txtCustNo.Text = "";
                        TxtBalance.Text = "";
                        TxtTotalBal.Text = "";

                        TxtAccNo.Text = TxtPtype.Text.ToString();
                        TxtCustName.Text = TxtPname.Text.ToString();
                        hdfGlCode.Value = Convert.ToInt32(ViewState["GlCode"].ToString() == "" ? "0" : ViewState["GlCode"].ToString()).ToString();
                        txtCustNo.Text = "0";

                        TxtBalance.Text = MT.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();
                        TxtTotalBal.Text = MT.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "MainBal").ToString();

                        TxtAmount.Focus();
                    }
                    else
                    {
                        TxtAccNo.Text = "";
                        TxtCustName.Text = "";
                        TxtBalance.Text = "";
                        TxtTotalBal.Text = "";
                        hdfGlCode.Value = Convert.ToInt32(ViewState["GlCode"].ToString() == "" ? "0" : ViewState["GlCode"].ToString()).ToString();

                        TxtAccNo.Focus();
                    }
                }
                else
                {
                    lblMessage.Text = "Enter Valid Product code...!!";
                    ModalPopup.Show(this.Page);
                    ClearText();
                    TxtPtype.Focus();
                }
            }
            else
            {
                TxtPtype.Text = "";
                TxtPname.Text = "";
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

    protected void TxtPname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] TD = TxtPname.Text.Split('_');
            if (TD.Length > 1)
            {
                //Added By Amol on 22092017 as per ambika mam instruction
                if (BD.GetProdOperate(Session["BRCD"].ToString(), TD[1].ToString()).ToString() != "3")
                {
                    TxtPname.Text = TD[0].ToString();
                    TxtPtype.Text = TD[1].ToString();

                    string[] AC = MT.Getaccno(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString()).Split('_');
                    ViewState["GlCode"] = AC[0].ToString();

                    //  Added By amol On 2018-02-06 for share account name search
                    if ((TxtPtype.Text.ToString() == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))
                    {
                        Param = CC.getShrParam();
                        if (Param == "HO" || Param == "ho" || Param == "Ho")
                            AutoAccname.ContextKey = "1" + "_" + TxtPtype.Text + "_" + ViewState["GlCode"].ToString();
                        else
                            AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtPtype.Text + "_" + ViewState["GlCode"].ToString();
                    }
                    else
                        AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtPtype.Text + "_" + ViewState["GlCode"].ToString();

                    string YN = CC.GetIntACCYN(Session["BRCD"].ToString(), TxtPtype.Text);
                    if ((Convert.ToInt32(ViewState["GlCode"].ToString() == "" ? "0" : ViewState["GlCode"].ToString()) >= 100 && YN != "Y") || (Convert.ToInt32(ViewState["GlCode"].ToString() == "" ? "0" : ViewState["GlCode"].ToString()) == 99)) //--abhishek as per GL LEVEL Requirment
                    {
                        TxtAccNo.Text = "";
                        TxtCustName.Text = "";
                        txtCustNo.Text = "";
                        TxtBalance.Text = "";
                        TxtTotalBal.Text = "";

                        TxtAccNo.Text = TxtPtype.Text.ToString();
                        TxtCustName.Text = TxtPname.Text.ToString();
                        hdfGlCode.Value = Convert.ToInt32(ViewState["GlCode"].ToString() == "" ? "0" : ViewState["GlCode"].ToString()).ToString();
                        txtCustNo.Text = "0";

                        TxtBalance.Text = MT.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();
                        TxtTotalBal.Text = MT.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "MainBal").ToString();

                        TxtAmount.Focus();
                    }
                    else
                    {
                        TxtAccNo.Text = "";
                        TxtCustName.Text = "";
                        TxtBalance.Text = "";
                        TxtTotalBal.Text = "";
                        hdfGlCode.Value = Convert.ToInt32(ViewState["GlCode"].ToString() == "" ? "0" : ViewState["GlCode"].ToString()).ToString();

                        TxtAccNo.Focus();
                    }
                }
                else
                {
                    TxtPtype.Text = "";
                    TxtPname.Text = "";
                    lblMessage.Text = "Product is not operating...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
            else
            {
                lblMessage.Text = "Enter Valid Product code...!!";
                ModalPopup.Show(this.Page);
                ClearText();
                TxtPtype.Focus();
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
            //  Added By amol On 2018-02-06 for share account name search
            if ((TxtPtype.Text.ToString() == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))
            {
                Param = CC.getShrParam();
                if (Param == "HO" || Param == "ho" || Param == "Ho")
                    sResult = CC.GetAccStatus("1", TxtPtype.Text.ToString(), TxtAccNo.Text.ToString());
                else
                    sResult = CC.GetAccStatus(Session["BRCD"].ToString(), TxtPtype.Text.ToString(), TxtAccNo.Text.ToString());
            }
            else if (ViewState["GlCode"].ToString() == "9")
                sResult = CC.GetAccStatus(Session["BRCD"].ToString(), "4", TxtAccNo.Text.ToString());
            else
                sResult = CC.GetAccStatus(Session["BRCD"].ToString(), TxtPtype.Text.ToString(), TxtAccNo.Text.ToString());

            if ((sResult == "1" && DdlCRDR.SelectedValue == "1" || sResult == "2" && DdlCRDR.SelectedValue == "1" || sResult == "4" && DdlCRDR.SelectedValue == "1" || sResult == "6" && DdlCRDR.SelectedValue == "1") || (sResult == "1" && DdlCRDR.SelectedValue == "2" || sResult == "2" && DdlCRDR.SelectedValue == "2"))
            {
                //  Added By amol On 2018-02-06 for share account name search
                if ((TxtPtype.Text.ToString() == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))
                {
                    Param = CC.getShrParam();
                    if (Param == "HO" || Param == "ho" || Param == "Ho")
                        sResult = BD.Getstage1(TxtAccNo.Text, "1", TxtPtype.Text);
                    else
                        sResult = BD.Getstage1(TxtAccNo.Text, Session["BRCD"].ToString(), TxtPtype.Text);
                }
                else if (ViewState["GlCode"].ToString() == "9")
                    sResult = BD.Getstage1(TxtAccNo.Text, Session["BRCD"].ToString(), "4");
                else
                    sResult = BD.Getstage1(TxtAccNo.Text, Session["BRCD"].ToString(), TxtPtype.Text);

                if (sResult != null)
                {
                    if (sResult != "1003" && ViewState["GlCode"].ToString() != "5")
                    {
                        Clear();
                        TxtAccNo.Focus();
                        WebMsgBox.Show("Sorry account not authorise...!!", this.Page);
                        return;
                    }
                    else
                    {
                        DataTable DT = new DataTable();
                        //  Added By amol On 2018-02-06 for share account name search
                        if ((TxtPtype.Text.ToString() == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))
                        {
                            Param = CC.getShrParam();
                            if (Param == "HO" || Param == "ho" || Param == "Ho")
                                DT = MT.GetCustName(TxtPtype.Text, TxtAccNo.Text, "1");
                            else
                                DT = MT.GetCustName(TxtPtype.Text, TxtAccNo.Text, Session["BRCD"].ToString());
                        }
                        else if (ViewState["GlCode"].ToString() == "9")
                            DT = MT.GetCustName("4", TxtAccNo.Text, Session["BRCD"].ToString());
                        else
                            DT = MT.GetCustName(TxtPtype.Text, TxtAccNo.Text, Session["BRCD"].ToString());

                        if (DT.Rows.Count > 0)
                        {
                            CustName = DT.Rows[0]["CustName"].ToString().Split('_');

                            TxtCustName.Text = CustName[0].ToString();
                            txtCustNo.Text = CustName[1].ToString();

                            //  Added By amol On 2018-02-06 for share account name search
                            if ((TxtPtype.Text.ToString() == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))
                            {
                                Param = CC.getShrParam();
                                if (Param == "HO" || Param == "ho" || Param == "Ho")
                                    txtPan.Text = PanCard("1", txtCustNo.Text);
                                else
                                    txtPan.Text = PanCard(Session["BRCD"].ToString(), txtCustNo.Text);
                            }
                            else
                                txtPan.Text = PanCard(Session["BRCD"].ToString(), txtCustNo.Text);

                            if (Convert.ToInt32(txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString()) < 0)
                            {
                                TxtAccNo.Text = "";
                                TxtCustName.Text = "";
                                txtCustNo.Text = "";
                                TxtBalance.Text = "";
                                TxtTotalBal.Text = "";
                                TxtAccNo.Focus();
                                WebMsgBox.Show("Please Enter valid Account Number Customer Not Exist...!!", this.Page);
                                return;
                            }

                            //  Added By amol On 2018-02-06 for share account name search
                            if ((TxtPtype.Text.ToString() == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))
                            {
                                Param = CC.getShrParam();
                                if (Param == "HO" || Param == "ho" || Param == "Ho")
                                {
                                    TxtBalance.Text = MT.GetOpenClose("1", TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
                                    TxtTotalBal.Text = MT.GetOpenClose("1", TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "MainBal").ToString();
                                }
                                else
                                {
                                    TxtBalance.Text = MT.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
                                    TxtTotalBal.Text = MT.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "MainBal").ToString();
                                }
                            }
                            else
                            {
                                TxtBalance.Text = MT.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
                                TxtTotalBal.Text = MT.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "MainBal").ToString();
                            }

                            TxtAmount.Enabled = true;

                            if (ddlPMTMode.SelectedValue == "1" && DdlCRDR.SelectedValue == "2")
                            {
                                if (MT.CheckValidation(TxtPtype.Text.Trim().ToString(), Session["BRCD"].ToString()).ToString() == "N")
                                {
                                    if (Convert.ToDouble(TxtTotalBal.Text) <= 0)
                                    {
                                        TxtAmount.Enabled = false;
                                        lblMessage.Text = "Sorry Insufficient Account Balance...!!";
                                        ModalPopup.Show(this.Page);
                                    }
                                }
                            }
                            if ((ddlPMTMode.SelectedValue == "2" || ddlPMTMode.SelectedValue == "3") && (DdlCRDR.SelectedValue == "2"))
                            {
                                if (MT.CheckMultiValidation(TxtPtype.Text.Trim().ToString(), Session["BRCD"].ToString()).ToString() == "N")
                                {
                                    if (Convert.ToDouble(TxtTotalBal.Text) <= 0)
                                    {
                                        TxtAmount.Enabled = false;
                                        lblMessage.Text = "Sorry Insufficient Account Balance...!!";
                                        ModalPopup.Show(this.Page);
                                    }
                                }
                            }

                            //  Added By amol On 2018-02-06 for share account name search
                            if ((TxtPtype.Text.ToString() == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))
                            {
                                Param = CC.getShrParam();
                                if (Param == "HO" || Param == "ho" || Param == "Ho")
                                    DebitAmt = MT.DebitAmount("1", TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["Mid"].ToString());
                                else
                                    DebitAmt = MT.DebitAmount(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["Mid"].ToString());
                            }
                            else
                                DebitAmt = MT.DebitAmount(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["Mid"].ToString());

                            if (DebitAmt.ToString() != "")
                            {
                                TxtBalance.Text = (Convert.ToDouble(TxtBalance.Text) - Convert.ToDouble(DebitAmt)).ToString();
                            }

                            if (ddlPMTMode.SelectedValue == "1")
                                TxtNarration.Text = "By Cash";
                            else if (ddlPMTMode.SelectedValue == "2")
                                TxtNarration.Text = "To TRF";
                            else if (ddlPMTMode.SelectedValue == "3")
                                TxtNarration.Text = "To Cheque";
                        }
                        if (TxtAccNo.Text != "" && TxtPtype.Text != "")
                        {
                            ////Displayed modal popup of voucher info by ankita 20/05/2017
                            DataTable dtmodal = new DataTable();

                            //  Added By amol On 2018-02-06 for share account name search
                            if ((TxtPtype.Text.ToString() == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))
                            {
                                Param = CC.getShrParam();
                                if (Param == "HO" || Param == "ho" || Param == "Ho")
                                    dtmodal = CR.GetInfoTbl("1", Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtPtype.Text);
                                else
                                    dtmodal = CR.GetInfoTbl(Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtPtype.Text);
                            }
                            else
                                dtmodal = CR.GetInfoTbl(Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtPtype.Text);

                            if (dtmodal.Rows.Count > 0)
                            {
                                //  Added By amol On 2018-02-06 for share account name search
                                if ((TxtPtype.Text.ToString() == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))
                                {
                                    Param = CC.getShrParam();
                                    if (Param == "HO" || Param == "ho" || Param == "Ho")
                                        resultout = CR.GetInfo(GrdView, "1", Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtPtype.Text);
                                    else
                                        resultout = CR.GetInfo(GrdView, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtPtype.Text);
                                }
                                else
                                    resultout = CR.GetInfo(GrdView, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtPtype.Text);

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

                            //  Added by amol on 27/12/2017 for show message only if customer loan acc is in overdue
                            if (txtCustNo.Text.ToString() != "0")
                            {
                                Photo_Sign();

                                //  Added By amol On 2018-02-06 for share account name search
                                if ((TxtPtype.Text.ToString() == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))
                                {
                                    Param = CC.getShrParam();
                                    if (Param == "HO" || Param == "ho" || Param == "Ho")
                                        DT = CC.CheckCustODAcc("1", txtCustNo.Text.ToString(), Session["EntryDate"].ToString());
                                    else
                                        DT = CC.CheckCustODAcc(Session["BRCD"].ToString(), txtCustNo.Text.ToString(), Session["EntryDate"].ToString());
                                }
                                else
                                    DT = CC.CheckCustODAcc(Session["BRCD"].ToString(), txtCustNo.Text.ToString(), Session["EntryDate"].ToString());

                                if (DT.Rows.Count > 0)
                                    WebMsgBox.Show("Operation in defaulter account...!!", this.Page);
                            }
                        }
                        TxtAmount.Focus();
                    }
                }
                else
                {
                    TxtAccNo.Text = "";
                    TxtCustName.Text = "";
                    txtCustNo.Text = "";
                    TxtBalance.Text = "";
                    TxtTotalBal.Text = "";
                    lblMessage.Text = "Enter Valid Account Number...!!";
                    ModalPopup.Show(this.Page);
                    TxtAccNo.Focus();
                }
            }
            else
            {
                TxtAccNo.Text = "";
                TxtCustName.Text = "";
                txtCustNo.Text = "";
                TxtBalance.Text = "";
                TxtTotalBal.Text = "";
                lblMessage.Text = "Acc number " + TxtAccNo.Text.ToString() + " is Closed...!!";
                ModalPopup.Show(this.Page);
                TxtAccNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtCustName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] TD = TxtCustName.Text.ToString().Split('_');
            if (TD.Length > 1)
            {
                TxtAccNo.Text = TD[1].ToString();
                //  Added By amol On 2018-02-06 for share account name search
                if ((TxtPtype.Text.ToString() == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))
                {
                    Param = CC.getShrParam();
                    if (Param == "HO" || Param == "ho" || Param == "Ho")
                        sResult = CC.GetAccStatus("1", TxtPtype.Text.ToString(), TxtAccNo.Text.ToString());
                    else
                        sResult = CC.GetAccStatus(Session["BRCD"].ToString(), TxtPtype.Text.ToString(), TxtAccNo.Text.ToString());
                }
                else if (ViewState["GlCode"].ToString() == "9")
                    sResult = CC.GetAccStatus(Session["BRCD"].ToString(), "4", TxtAccNo.Text.ToString());
                else
                    sResult = CC.GetAccStatus(Session["BRCD"].ToString(), TxtPtype.Text.ToString(), TxtAccNo.Text.ToString());

                if ((sResult == "1" && DdlCRDR.SelectedValue == "1" || sResult == "2" && DdlCRDR.SelectedValue == "1" || sResult == "4" && DdlCRDR.SelectedValue == "1" || sResult == "6" && DdlCRDR.SelectedValue == "1") || (sResult == "1" && DdlCRDR.SelectedValue == "2" || sResult == "2" && DdlCRDR.SelectedValue == "2"))
                {
                    //  Added By amol On 2018-02-06 for share account name search
                    if ((TxtPtype.Text.ToString() == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))
                    {
                        Param = CC.getShrParam();
                        if (Param == "HO" || Param == "ho" || Param == "Ho")
                            sResult = BD.Getstage1(TxtAccNo.Text, "1", TxtPtype.Text);
                        else
                            sResult = BD.Getstage1(TxtAccNo.Text, Session["BRCD"].ToString(), TxtPtype.Text);
                    }
                    else if (ViewState["GlCode"].ToString() == "9")
                        sResult = BD.Getstage1(TxtAccNo.Text, Session["BRCD"].ToString(), "4");
                    else
                        sResult = BD.Getstage1(TxtAccNo.Text, Session["BRCD"].ToString(), TxtPtype.Text);

                    if (sResult != null)
                    {
                        if (sResult != "1003" && ViewState["GlCode"].ToString() != "5")
                        {
                            Clear();
                            TxtAccNo.Focus();
                            WebMsgBox.Show("Sorry customer not authorise...!!", this.Page);
                            return;
                        }
                        else
                        {
                            DataTable DT = new DataTable();
                            //  Added By amol On 2018-02-06 for share account name search
                            if ((TxtPtype.Text.ToString() == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))
                            {
                                Param = CC.getShrParam();
                                if (Param == "HO" || Param == "ho" || Param == "Ho")
                                    DT = MT.GetCustName(TxtPtype.Text, TxtAccNo.Text, "1");
                                else
                                    DT = MT.GetCustName(TxtPtype.Text, TxtAccNo.Text, Session["BRCD"].ToString());
                            }
                            else if (ViewState["GlCode"].ToString() == "9")
                                DT = MT.GetCustName("4", TxtAccNo.Text, Session["BRCD"].ToString());
                            else
                                DT = MT.GetCustName(TxtPtype.Text, TxtAccNo.Text, Session["BRCD"].ToString());

                            if (DT.Rows.Count > 0)
                            {
                                string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');

                                TxtCustName.Text = CustName[0].ToString();
                                txtCustNo.Text = CustName[1].ToString();

                                //  Added By amol On 2018-02-06 for share account name search
                                if ((TxtPtype.Text.ToString() == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))
                                {
                                    Param = CC.getShrParam();
                                    if (Param == "HO" || Param == "ho" || Param == "Ho")
                                        txtPan.Text = PanCard("1", txtCustNo.Text);
                                    else
                                        txtPan.Text = PanCard(Session["BRCD"].ToString(), txtCustNo.Text);
                                }
                                else
                                    txtPan.Text = PanCard(Session["BRCD"].ToString(), txtCustNo.Text);

                                if (Convert.ToInt32(txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString()) < 0)
                                {
                                    TxtAccNo.Text = "";
                                    TxtCustName.Text = "";
                                    txtCustNo.Text = "";
                                    TxtBalance.Text = "";
                                    TxtTotalBal.Text = "";
                                    TxtAccNo.Focus();
                                    WebMsgBox.Show("Please Enter valid Account Number Customer Not Exist...!!", this.Page);
                                    return;
                                }

                                //  Added By amol On 2018-02-06 for share account name search
                                if ((TxtPtype.Text.ToString() == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))
                                {
                                    Param = CC.getShrParam();
                                    if (Param == "HO" || Param == "ho" || Param == "Ho")
                                    {
                                        TxtBalance.Text = MT.GetOpenClose("1", TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
                                        TxtTotalBal.Text = MT.GetOpenClose("1", TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "MainBal").ToString();
                                    }
                                    else
                                    {
                                        TxtBalance.Text = MT.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
                                        TxtTotalBal.Text = MT.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "MainBal").ToString();
                                    }
                                }
                                else
                                {
                                    TxtBalance.Text = MT.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
                                    TxtTotalBal.Text = MT.GetOpenClose(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "MainBal").ToString();
                                }

                                TxtAmount.Enabled = true;

                                if (ddlPMTMode.SelectedValue == "1" && DdlCRDR.SelectedValue == "2")
                                {
                                    if (MT.CheckValidation(TxtPtype.Text.Trim().ToString(), Session["BRCD"].ToString()).ToString() == "N")
                                    {
                                        if (Convert.ToDouble(TxtTotalBal.Text) <= 0)
                                        {
                                            TxtAmount.Enabled = false;
                                            lblMessage.Text = "Sorry Insufficient Account Balance...!!";
                                            ModalPopup.Show(this.Page);
                                        }
                                    }
                                }
                                if ((ddlPMTMode.SelectedValue == "2" || ddlPMTMode.SelectedValue == "3") && (DdlCRDR.SelectedValue == "2"))
                                {
                                    if (MT.CheckMultiValidation(TxtPtype.Text.Trim().ToString(), Session["BRCD"].ToString()).ToString() == "N")
                                    {
                                        if (Convert.ToDouble(TxtTotalBal.Text) <= 0)
                                        {
                                            TxtAmount.Enabled = false;
                                            lblMessage.Text = "Sorry Insufficient Account Balance...!!";
                                            ModalPopup.Show(this.Page);
                                        }
                                    }
                                }

                                //  Added By amol On 2018-02-06 for share account name search
                                if ((TxtPtype.Text.ToString() == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))
                                {
                                    Param = CC.getShrParam();
                                    if (Param == "HO" || Param == "ho" || Param == "Ho")
                                        DebitAmt = MT.DebitAmount("1", TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["Mid"].ToString());
                                    else
                                        DebitAmt = MT.DebitAmount(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["Mid"].ToString());
                                }
                                else
                                    DebitAmt = MT.DebitAmount(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["Mid"].ToString());

                                if (DebitAmt.ToString() != "")
                                {
                                    TxtBalance.Text = (Convert.ToDouble(TxtBalance.Text) - Convert.ToDouble(DebitAmt)).ToString();
                                }

                                if (ddlPMTMode.SelectedValue == "1")
                                    TxtNarration.Text = "By Cash";
                                else if (ddlPMTMode.SelectedValue == "2")
                                    TxtNarration.Text = "To TRF";
                                else if (ddlPMTMode.SelectedValue == "3")
                                    TxtNarration.Text = "To Cheque";
                            }
                            if (TxtAccNo.Text != "" && TxtPtype.Text != "")
                            {
                                ////Displayed modal popup of voucher info by ankita 20/05/2017
                                DataTable dtmodal = new DataTable();

                                //  Added By amol On 2018-02-06 for share account name search
                                if ((TxtPtype.Text.ToString() == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))
                                {
                                    Param = CC.getShrParam();
                                    if (Param == "HO" || Param == "ho" || Param == "Ho")
                                        dtmodal = CR.GetInfoTbl("1", Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtPtype.Text);
                                    else
                                        dtmodal = CR.GetInfoTbl(Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtPtype.Text);
                                }
                                else
                                    dtmodal = CR.GetInfoTbl(Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtPtype.Text);

                                if (dtmodal.Rows.Count > 0)
                                {
                                    //  Added By amol On 2018-02-06 for share account name search
                                    if ((TxtPtype.Text.ToString() == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))
                                    {
                                        Param = CC.getShrParam();
                                        if (Param == "HO" || Param == "ho" || Param == "Ho")
                                            resultout = CR.GetInfo(GrdView, "1", Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtPtype.Text);
                                        else
                                            resultout = CR.GetInfo(GrdView, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtPtype.Text);
                                    }
                                    else
                                        resultout = CR.GetInfo(GrdView, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtPtype.Text);

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

                                //  Added by amol on 27/12/2017 for show message only if customer loan acc is in overdue
                                if (txtCustNo.Text.ToString() != "0")
                                {
                                    Photo_Sign();

                                    //  Added By amol On 2018-02-06 for share account name search
                                    if ((TxtPtype.Text.ToString() == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))
                                    {
                                        Param = CC.getShrParam();
                                        if (Param == "HO" || Param == "ho" || Param == "Ho")
                                            DT = CC.CheckCustODAcc("1", txtCustNo.Text.ToString(), Session["EntryDate"].ToString());
                                        else
                                            DT = CC.CheckCustODAcc(Session["BRCD"].ToString(), txtCustNo.Text.ToString(), Session["EntryDate"].ToString());
                                    }
                                    else
                                        DT = CC.CheckCustODAcc(Session["BRCD"].ToString(), txtCustNo.Text.ToString(), Session["EntryDate"].ToString());

                                    if (DT.Rows.Count > 0)
                                        WebMsgBox.Show("Operation in defaulter account...!!", this.Page);
                                }
                            }
                            TxtAmount.Focus();
                        }
                    }
                    else
                    {
                        TxtAccNo.Text = "";
                        TxtCustName.Text = "";
                        txtCustNo.Text = "";
                        TxtBalance.Text = "";
                        TxtTotalBal.Text = "";
                        lblMessage.Text = "Enter Valid Account Number...!!";
                        ModalPopup.Show(this.Page);
                        TxtAccNo.Focus();
                    }
                }
                else
                {
                    TxtAccNo.Text = "";
                    TxtCustName.Text = "";
                    txtCustNo.Text = "";
                    TxtBalance.Text = "";
                    TxtTotalBal.Text = "";
                    lblMessage.Text = "Acc number " + TxtAccNo.Text.ToString() + " is Closed...!!";
                    ModalPopup.Show(this.Page);
                    TxtAccNo.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double BAL = Convert.ToDouble(TxtBalance.Text.Trim().ToString() == "" ? "0" : TxtBalance.Text.Trim().ToString());
            double curbal = Convert.ToDouble(TxtAmount.Text.Trim().ToString() == "" ? "0" : TxtAmount.Text.Trim().ToString());

            if (ddlPMTMode.SelectedValue == "1" && DdlCRDR.SelectedValue == "2")
            {
                if (MT.CheckValidation(TxtPtype.Text.Trim().ToString(), Session["BRCD"].ToString()).ToString() == "N")
                {
                    if (curbal > BAL)
                    {
                        TxtAmount.Text = "";
                        TxtAmount.Focus();
                        lblMessage.Text = "Sorry Insufficient Account Balance...!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                }
            }
            else if ((ddlPMTMode.SelectedValue == "2" && DdlCRDR.SelectedValue == "2") || (ddlPMTMode.SelectedValue == "3" && DdlCRDR.SelectedValue == "2"))
            {
                if (MT.CheckMultiValidation(TxtPtype.Text.Trim().ToString(), Session["BRCD"].ToString()).ToString() == "N")
                {
                    if (curbal > BAL)
                    {
                        TxtAmount.Text = "";
                        TxtAmount.Focus();
                        lblMessage.Text = "Sorry Insufficient Account Balance...!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                }
            }

            if (ddlPMTMode.SelectedValue == "1" || ddlPMTMode.SelectedValue == "2")
                TxtNarration.Focus();
            else if (ddlPMTMode.SelectedValue == "3")
                TxtChequeNo.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Click Event Here

    protected void LnkVerify_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtAccNo.Text != "")
            {
                string custno = ViewState["CRCUSTNO"].ToString();
                string url = "FrmVerifySign.aspx?CUSTNO=" + custno + "";
                NewWindowsVerify(url);
            }
            else
            {
                WebMsgBox.Show("Enter Account number!....", this.Page);
                TxtAccNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            string AC, CRAC, CN, CD;
            AC = CRAC = CN = CD = "";

            //If Cheque Number is blank Then Set it to Zero
            if (TxtChequeNo.Text.Trim().ToString() == "")
            {
                CN = "0";
            }
            else
            {
                CN = TxtChequeNo.Text.Trim().ToString();
            }

            //If Cheque Date is blank Then Set it to Default Value 01/01/1900
            if (TxtChequeDate.Text.Trim().ToString() == "")
            {
                CD = "1900-01-01";
            }
            else
            {
                CD = TxtChequeDate.Text.Trim().ToString();
                string[] cd1 = CD.Split('/');
                CD = cd1[2].ToString() + '-' + cd1[1].ToString() + '-' + cd1[0].ToString();
            }

            //If Account Number is blank Then Set Acc No and Cust No to Zero
            if (TxtAccNo.Text.Trim().ToString() == "")
            {
                AC = "0";
            }
            else
            {
                AC = TxtAccNo.Text.Trim().ToString();
            }

            //Check Amount is grater than zero or not
            if (Convert.ToDouble(TxtAmount.Text.Trim().ToString() == "" ? "0" : TxtAmount.Text.Trim().ToString()) > 0.00)
            {
                if (ViewState["GlCode"].ToString() == "3" && DdlCRDR.SelectedValue == "1")
                {
                    //Added for loan case
                    DataTable dt = new DataTable();
                    dt = MT.GetLoanTotalAmount(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString());

                    TotalAmt = Math.Round(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Principle"].ToString()) < 0 ? "0" : dt.Rows[0]["Principle"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) < 0 ? "0" : dt.Rows[0]["InterestRec"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["NoticeChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["ServiceChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["CourtChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["BankChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["InsChrg"].ToString()));

                    if (Convert.ToDouble(TxtAmount.Text.Trim().ToString() == "" ? "0" : TxtAmount.Text.Trim().ToString()) < TotalAmt)
                    {
                        TotalDrAmt = Convert.ToDouble(TxtAmount.Text.Trim().ToString() == "" ? "0" : TxtAmount.Text.Trim().ToString());

                        if (dt.Rows.Count > 0)
                        {
                            if (ddlPMTMode.SelectedValue.ToString() == "1" && DdlCRDR.SelectedValue.ToString() == "1")
                                Activity = "3";
                            if (ddlPMTMode.SelectedValue.ToString() == "2" && DdlCRDR.SelectedValue.ToString() == "1")
                                Activity = "7";
                            if (ddlPMTMode.SelectedValue.ToString() == "3" && DdlCRDR.SelectedValue.ToString() == "1")
                                Activity = "5";

                            if (dt.Rows[0]["Acc_Status"].ToString() == "9")
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

                            OtherChrg = Math.Round(Convert.ToDouble(((Math.Abs(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Principle"].ToString()) < 0 ? "0" : dt.Rows[0]["Principle"].ToString())) * Convert.ToDouble(MT.GetOtherIntRate(Session["BRCD"].ToString(), dt.Rows[0]["SubGlCode"].ToString()))) * (Convert.ToInt32(dt.Rows[0]["Days"].ToString()))) / 36500));

                            if (OtherChrg == null || OtherChrg.ToString() == "")
                                OtherChrg = 0;
                            else
                                TotalAmt = TotalAmt + OtherChrg;

                            if (dt.Rows[0]["IntCalType"].ToString() == "1" || dt.Rows[0]["IntCalType"].ToString() == "2")
                            {
                                resultout = 1;
                                if (ddlPMTMode.SelectedValue.ToString() == "1" && DdlCRDR.SelectedValue.ToString() == "1")
                                {
                                    resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString(), "99", "99", "0", TxtCustName.Text, TxtAmount.Text, DdlCRDR.SelectedValue == "1" ? "2" : "1", DdlCRDR.SelectedValue == "1" ? "3" : "4", DdlCRDR.SelectedValue == "1" ? "CR" : "CP", "0", "By Cash", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                }

                                //  For Insurance Charge
                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()))
                                    {
                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InsChrgGl"].ToString(), dt.Rows[0]["InsChrgSub"].ToString(), AC, TxtCustName.Text, dt.Rows[0]["InsChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "INSCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()) > 0)
                                            {
                                                //  Insurance Charge Credit To 11 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InsChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "11", "1", Activity.ToString(), "Insurance Charge Credit", dt.Rows[0]["InsChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()));
                                    }
                                    else if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                    {
                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InsChrgGl"].ToString(), dt.Rows[0]["InsChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "INSCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()) > 0)
                                            {
                                                //  Insurance Charge Credit To 11 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InsChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "11", "1", Activity.ToString(), "Insurance Charge Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = 0;
                                    }
                                }

                                //  For Bank Charges
                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()))
                                    {
                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["BankChrgGl"].ToString(), dt.Rows[0]["BankChrgSub"].ToString(), AC, TxtCustName.Text, dt.Rows[0]["BankChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "BNKCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()) > 0)
                                            {
                                                // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["BankChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "10", "1", Activity.ToString(), "Bank Charges Credit", dt.Rows[0]["BankChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()));
                                    }
                                    else if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()) > 0 && TotalDrAmt > 0 && TotalDrAmt > 0)
                                    {
                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["BankChrgGl"].ToString(), dt.Rows[0]["BankChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "BNKCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()) > 0)
                                            {
                                                // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["BankChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "10", "1", Activity.ToString(), "Bank Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = 0;
                                    }
                                }

                                //  For Other Charges
                                if (resultout > 0)
                                {
                                    if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0 && TotalDrAmt >= (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg))
                                    {
                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["OtherChrgGl"].ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg).ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "OTHCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (OtherChrg > 0)
                                            {
                                                // Other Charges Amt Debit To 9 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "9", "2", Activity.ToString(), "Other Charges Debit", OtherChrg.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        if (resultout > 0)
                                        {
                                            if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0)
                                            {
                                                // Other Charges Amt Credit To 9 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "9", "1", Activity.ToString(), "Other Charges Credit", Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg));
                                    }
                                    else if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0 && TotalDrAmt > 0)
                                    {
                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["OtherChrgGl"].ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "OTHCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (OtherChrg > 0)
                                            {
                                                // Other Charges Amt Debit To 9 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "9", "2", Activity.ToString(), "Other Charges Debit", OtherChrg.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        if (resultout > 0)
                                        {
                                            if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0)
                                            {
                                                // Other Charges Amt Credit To 9 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "9", "1", Activity.ToString(), "Other Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
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
                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["SurChrgGl"].ToString(), dt.Rows[0]["SurChrgSub"].ToString(), AC, TxtCustName.Text, (Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg).ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "SURCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (CurSurChrg > 0)
                                            {
                                                // Sur Charges Debited To 8 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "8", "2", Activity.ToString(), "Sur Charges Debited", CurSurChrg.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                            if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0)
                                            {
                                                // Sur Charges Credit To 8 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "8", "1", Activity.ToString(), "Sur Charges Credit", Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg));
                                    }
                                    else if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0 && TotalDrAmt > 0)
                                    {
                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["SurChrgGl"].ToString(), dt.Rows[0]["SurChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "SURCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (CurSurChrg > 0)
                                            {
                                                // Sur Charges Debited To 8 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "8", "2", Activity.ToString(), "Sur Charges Debited", CurSurChrg.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                            if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0)
                                            {
                                                // Sur Charges Credit To 8 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "8", "1", Activity.ToString(), "Sur Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = 0;
                                    }
                                }

                                // For Court Charges
                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()))
                                    {
                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["CourtChrgGl"].ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), AC, TxtCustName.Text, dt.Rows[0]["CourtChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "CRTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()) > 0)
                                            {
                                                // Court Charges Amt Credit To 7 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "7", "1", Activity.ToString(), "Court Charges Credit", dt.Rows[0]["CourtChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()));
                                    }
                                    else if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                    {
                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["CourtChrgGl"].ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "CRTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()) > 0)
                                            {
                                                // Court Charges Amt Credit To 7 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "7", "1", Activity.ToString(), "Court Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = 0;
                                    }
                                }

                                //  For Service Charges
                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()))
                                    {
                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgGl"].ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), AC, TxtCustName.Text, dt.Rows[0]["ServiceChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "SERCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()) > 0)
                                            {
                                                // Service Charges Amt Credit To 6 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "6", "1", Activity.ToString(), "Service Charges Credit", dt.Rows[0]["ServiceChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()));
                                    }
                                    else if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                    {
                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgGl"].ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "SERCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()) > 0)
                                            {
                                                // Service Charges Amt Credit To 6 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "6", "1", Activity.ToString(), "Service Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = 0;
                                    }
                                }

                                //  For Notice Charges
                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : dt.Rows[0]["NoticeChrg"].ToString()))
                                    {
                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgGl"].ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), AC, TxtCustName.Text, (dt.Rows[0]["NoticeChrg"].ToString()), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "NOTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()) > 0)
                                            {
                                                // Notice Charges Credit To 5 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "5", "1", Activity.ToString(), "Notice Charges Credit", dt.Rows[0]["NoticeChrg"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()));
                                    }
                                    else if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                    {
                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgGl"].ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "NOTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()) > 0)
                                            {
                                                // Notice Charges Credit To 5 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "5", "1", Activity.ToString(), "Notice Charges Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
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
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InterestRecGl"].ToString(), dt.Rows[0]["InterestRecSub"].ToString(), AC, TxtCustName.Text, (dt.Rows[0]["InterestRec"].ToString()), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "INTRCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0)
                                                {
                                                    // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestRecSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "4", "1", Activity.ToString(), "Interest Received Credit", Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                        }
                                        else if (dt.Rows[0]["IntCalType"].ToString() == "2")
                                        {
                                            // Interest Received credit to GL 3
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text.Trim().ToString(), AC, TxtCustName.Text, (dt.Rows[0]["InterestRec"].ToString()), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "INTRCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                        }
                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()));
                                    }
                                    else if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt > 0)
                                    {
                                        if (dt.Rows[0]["IntCalType"].ToString() == "1")
                                        {
                                            // Interest Received credit to GL 11
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InterestRecGl"].ToString(), dt.Rows[0]["InterestRecSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "INTRCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0)
                                                {
                                                    // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestRecSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "4", "1", Activity.ToString(), "Interest Received Credit", Convert.ToDouble(TotalDrAmt.ToString()).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                        }
                                        else if (dt.Rows[0]["IntCalType"].ToString() == "2")
                                        {
                                            // Interest Received credit to GL 3
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text.Trim().ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "INTRCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                        }
                                        TotalDrAmt = 0;
                                    }
                                }

                                //  For Penal Charge
                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString())))
                                    {
                                        if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                        {
                                            //Penal Charge Credit To GL 12
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "PENCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                        }

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) > 0)
                                            {
                                                //Penal Interest Debit To 3 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "3", "2", Activity.ToString(), "Penal Interest Debit", dt.Rows[0]["CurrPInterest"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                            {
                                                //Penal Interest Credit To 3 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "3", "1", Activity.ToString(), "Penal Interest Credit", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            //Penal Charge Contra
                                            if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                            {
                                                //Penal chrg Applied Debit To GL 12
                                                resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), "2", "12", "TR_INT", "0", "PENDR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    //Penal chrg Applied Credit to GL 100
                                                    resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), "100", dt.Rows[0]["PlAccNo2"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), "1", "12", "TR_INT", "0", "PENCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                }
                                            }
                                        }

                                        TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString())));
                                    }
                                    else if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                    {
                                        //Penal Charge Credit To GL 12
                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "PENCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) > 0)
                                            {
                                                //Penal Interest Debit To 3 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "3", "2", Activity.ToString(), "Penal Interest Debit", dt.Rows[0]["CurrPInterest"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                            {
                                                //Penal Interest Credit To 3 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "3", "1", Activity.ToString(), "Penal Interest Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            //Penal Charge Contra
                                            if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                            {
                                                //Penal chrg Applied Debit To GL 12
                                                resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), "2", "12", "TR_INT", "0", "PENDR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    //Penal chrg Applied Credit to GL 100
                                                    resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), "100", dt.Rows[0]["PlAccNo2"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), "1", "12", "TR_INT", "0", "PENCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
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
                                        if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())))
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                            {
                                                //interest Credit to GL 11
                                                resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), AC, TxtCustName.Text, (Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "INTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString()) > 0)
                                                {
                                                    //Current Interest Debit To 2 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "2", "2", Activity.ToString(), "Interest Debit", dt.Rows[0]["CurrInterest"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                                {
                                                    //Current Interest Credit To 2 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "2", "1", Activity.ToString(), "Interest Credit", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }

                                            if (resultout > 0)
                                            {
                                                //interest Applied Contra
                                                if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                                {
                                                    //interest Applied Debit To GL 11
                                                    resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "2", "11", "TR_INT", "0", "INTDR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                    if (resultout > 0)
                                                    {
                                                        //interest Applied Credit to GL 100
                                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), "100", dt.Rows[0]["PlAccNo1"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "1", "11", "TR_INT", "0", "INTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                    }
                                                }
                                            }

                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())));
                                        }
                                        else if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                        {
                                            //interest Credit to GL 11
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(TotalDrAmt).ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "INTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString()) > 0)
                                                {
                                                    //Current Interest Debit To 2 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "2", "2", Activity.ToString(), "Interest Debit", Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                                {
                                                    //Current Interest Credit To 2 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "2", "1", Activity.ToString(), "Interest Credit", Convert.ToDouble(TotalDrAmt.ToString()).ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }

                                            if (resultout > 0)
                                            {
                                                //interest Applied Contra
                                                if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                                {
                                                    //interest Applied Debit To GL 11
                                                    resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(TotalDrAmt.ToString()).ToString(), "2", "11", "TR_INT", "0", "INTDR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                    if (resultout > 0)
                                                    {
                                                        //interest Applied Credit to GL 100
                                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), "100", dt.Rows[0]["PlAccNo1"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(TotalDrAmt.ToString()).ToString(), "1", "11", "TR_INT", "0", "INTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                    }
                                                }
                                            }

                                            TotalDrAmt = 0;
                                        }
                                    }
                                    else if (dt.Rows[0]["IntCalType"].ToString() == "2")
                                    {
                                        if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())))
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                            {
                                                //interest Received Credit to GL 3
                                                resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text.Trim().ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "INTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }

                                            //  Added As Per ambika mam Instruction 22-06-2017
                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                                {
                                                    //interest Applied Debit To GL 3
                                                    resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text.Trim().ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "2", "11", "TR_INT", "0", "INTDR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                    if (resultout > 0)
                                                    {
                                                        //interest Applied Credit to GL 100
                                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), "100", dt.Rows[0]["PlAccNo1"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "1", "11", "TR_INT", "0", "INTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                    }
                                                }
                                            }

                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())));
                                        }
                                        else if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                        {
                                            //interest Received Credit to GL 3
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text.Trim().ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "INTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            //  Added As Per ambika mam Instruction 22-06-2017
                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                                {
                                                    //interest Applied Debit To GL 3
                                                    resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text.Trim().ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), "2", "11", "TR_INT", "0", "INTDR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                    if (resultout > 0)
                                                    {
                                                        //interest Applied Credit to GL 100
                                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), "100", dt.Rows[0]["PlAccNo1"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), "1", "11", "TR_INT", "0", "INTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                    }
                                                }
                                            }

                                            TotalDrAmt = 0;
                                        }
                                    }
                                }

                                //Principle O/S Credit To Specific GL (e.g 3)
                                if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["Principle"].ToString()))
                                {
                                    resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text, AC, TxtCustName.Text, dt.Rows[0]["Principle"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "PRNCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString()) > 0)
                                        {
                                            //Current Principle Debit To 1 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), "1", "2", Activity.ToString(), "Principle Debit", dt.Rows[0]["Principle"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString()) > 0)
                                        {
                                            //Current Principle Credit To 1 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), "1", "1", Activity.ToString(), "Principle Credit", dt.Rows[0]["Principle"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }

                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["Principle"].ToString()));
                                }
                                else if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString()) > 0 && TotalDrAmt > 0)
                                {
                                    resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text, AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "PRNCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString()) > 0)
                                        {
                                            //Current Principle Debit To 1 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), "1", "2", Activity.ToString(), "Principle Debit", dt.Rows[0]["Principle"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString()) > 0)
                                        {
                                            //Current Principle Credit To 1 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), "1", "1", Activity.ToString(), "Principle Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }

                                    TotalDrAmt = 0;
                                }
                            }
                            else if (dt.Rows[0]["IntCalType"].ToString() == "3")
                            {
                                //Principle O/S Credit To Specific GL (e.g 3)
                                resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text, AC, TxtCustName.Text, TotalDrAmt.ToString(), "1", Activity.ToString(), Activity.ToString() == "3" ? "CR" : "TR", "0", "PRNCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                if (resultout > 0)
                                {
                                    if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0)
                                    {
                                        //Current Principle Debit To 1 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), "1", "2", Activity.ToString(), "Principle Debit", dt.Rows[0]["Principle"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                    }

                                    if (Convert.ToDouble(dt.Rows[0]["Principle"].ToString() == "" ? "0" : dt.Rows[0]["Principle"].ToString()) > 0)
                                    {
                                        //Current Principle Credit To 1 In AVS_LnTrx
                                        resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), "1", "1", Activity.ToString(), "Principle Credit", TotalDrAmt.ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
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
                                    resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text, AC, TxtCustName.Text, TotalDrAmt.ToString(), "2", "7", "TR", "1", "PAYDR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                    //  For Insurance Charge
                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()))
                                        {
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InsChrgGl"].ToString(), dt.Rows[0]["InsChrgSub"].ToString(), AC, TxtCustName.Text, dt.Rows[0]["InsChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "INSCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()) > 0)
                                                {
                                                    //  Insurance Charge Credit To 11 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InsChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "11", "1", "7", "Insurance Charge Credit", dt.Rows[0]["InsChrg"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()));
                                        }
                                        else if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                        {
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InsChrgGl"].ToString(), dt.Rows[0]["InsChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "INSCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["InsChrg"].ToString()) > 0)
                                                {
                                                    //  Insurance Charge Credit To 11 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InsChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "11", "1", "7", "Insurance Charge Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = 0;
                                        }
                                    }

                                    //  For Bank Charges
                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString() == "" ? "0" : dt.Rows[0]["BankChrg"].ToString()))
                                        {
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["BankChrgGl"].ToString(), dt.Rows[0]["BankChrgSub"].ToString(), AC, TxtCustName.Text, dt.Rows[0]["BankChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "BNKCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()) > 0)
                                                {
                                                    // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["BankChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "10", "1", "7", "Bank Charges Credit", dt.Rows[0]["BankChrg"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()));
                                        }
                                        else if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()) > 0 && TotalDrAmt > 0 && TotalDrAmt > 0)
                                        {
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["BankChrgGl"].ToString(), dt.Rows[0]["BankChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "BNKCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["BankChrg"].ToString()) > 0)
                                                {
                                                    // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["BankChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "10", "1", "7", "Bank Charges Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = 0;
                                        }
                                    }

                                    //  For Other Charges
                                    if (resultout > 0)
                                    {
                                        if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0 && TotalDrAmt >= (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg))
                                        {
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["OtherChrgGl"].ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg).ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "OTHCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (OtherChrg > 0)
                                                {
                                                    // Other Charges Amt Debit To 9 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "9", "2", "7", "Other Charges Credit", OtherChrg.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }

                                            if (resultout > 0)
                                            {
                                                if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0)
                                                {
                                                    // Other Charges Amt Credit To 9 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "9", "1", "7", "Other Charges Credit", Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg));
                                        }
                                        else if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0 && TotalDrAmt > 0)
                                        {
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["OtherChrgGl"].ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "OTHCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (OtherChrg > 0)
                                                {
                                                    // Other Charges Amt Debit To 9 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "9", "2", "7", "Other Charges Credit", OtherChrg.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }

                                            if (resultout > 0)
                                            {
                                                if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0)
                                                {
                                                    // Other Charges Amt Credit To 9 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["OtherChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "9", "1", "7", "Other Charges Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
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
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["SurChrgGl"].ToString(), Convert.ToDouble(dt.Rows[0]["SurChrgSub"].ToString() + CurSurChrg).ToString(), AC, TxtCustName.Text, dt.Rows[0]["SurChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "SURCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (CurSurChrg > 0)
                                                {
                                                    // Sur Charges Debited To 8 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "8", "2", "7", "Sur Charges Debited", CurSurChrg.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                                if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0)
                                                {
                                                    // Sur Charges Credit To 8 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "8", "1", "7", "Sur Charges Credit", Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg));
                                        }
                                        else if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0 && TotalDrAmt > 0)
                                        {
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["SurChrgGl"].ToString(), dt.Rows[0]["SurChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "SURCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (CurSurChrg > 0)
                                                {
                                                    // Sur Charges Debited To 8 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "8", "2", "7", "Sur Charges Debited", CurSurChrg.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                                if ((Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : dt.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0)
                                                {
                                                    // Sur Charges Credit To 8 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["SurChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "8", "1", "7", "Sur Charges Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = 0;
                                        }
                                    }

                                    // For Court Charges
                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()))
                                        {
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["CourtChrgGl"].ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), AC, TxtCustName.Text, dt.Rows[0]["CourtChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "CRTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()) > 0)
                                                {
                                                    // Court Charges Amt Credit To 7 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "7", "1", "7", "Court Charges Credit", dt.Rows[0]["CourtChrg"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()));
                                        }
                                        else if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                        {
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["CourtChrgGl"].ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "CRTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["CourtChrg"].ToString()) > 0)
                                                {
                                                    // Court Charges Amt Credit To 7 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["CourtChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "7", "1", "7", "Court Charges Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = 0;
                                        }
                                    }

                                    //  For Service Charges
                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()))
                                        {
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgGl"].ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), AC, TxtCustName.Text, dt.Rows[0]["ServiceChrg"].ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "SERCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()) > 0)
                                                {
                                                    // Service Charges Amt Credit To 6 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "6", "1", "7", "Service Charges Credit", dt.Rows[0]["ServiceChrg"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()));
                                        }
                                        else if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                        {
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgGl"].ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "SERCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["ServiceChrg"].ToString()) > 0)
                                                {
                                                    // Service Charges Amt Credit To 6 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["ServiceChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "6", "1", "7", "Service Charges Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = 0;
                                        }
                                    }

                                    //  For Notice Charges
                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()))
                                        {
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgGl"].ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), AC, TxtCustName.Text, (dt.Rows[0]["NoticeChrg"].ToString()), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "NOTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()) > 0)
                                                {
                                                    // Notice Charges Credit To 5 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "5", "1", "7", "Notice Charges Credit", dt.Rows[0]["NoticeChrg"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()));
                                        }
                                        else if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                        {
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgGl"].ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "NOTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["NoticeChrg"].ToString()) > 0)
                                                {
                                                    // Notice Charges Credit To 5 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["NoticeChrgSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "5", "1", "7", "Notice Charges Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
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
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InterestRecGl"].ToString(), dt.Rows[0]["InterestRecSub"].ToString(), AC, TxtCustName.Text, (dt.Rows[0]["InterestRec"].ToString()), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "INTRCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0)
                                                {
                                                    // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestRecSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "4", "1", "7", "Interest Received Credit", Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()));
                                        }
                                        else if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt > 0)
                                        {
                                            // Interest Received credit to GL 11
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InterestRecGl"].ToString(), dt.Rows[0]["InterestRecSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "INTRCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(dt.Rows[0]["InterestRec"].ToString()) > 0)
                                                {
                                                    // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestRecSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "4", "1", "7", "Interest Received Credit", Convert.ToDouble(TotalDrAmt.ToString()).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }
                                            TotalDrAmt = 0;
                                        }
                                    }

                                    //  For Penal Charge
                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString())))
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                            {
                                                //Penal Charge Credit To GL 12
                                                resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "PENCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) > 0)
                                                {
                                                    //Penal Interest Debit To 3 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "3", "2", "7", "Penal Interest Debit", dt.Rows[0]["CurrPInterest"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                {
                                                    //Penal Interest Credit To 3 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "3", "1", "7", "Penal Interest Credit", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }

                                            if (resultout > 0)
                                            {
                                                //Penal Charge Contra
                                                if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                {
                                                    //Penal chrg Applied Debit To GL 12
                                                    resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), "2", "12", "TR_INT", "1", "PENDR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                    if (resultout > 0)
                                                    {
                                                        //Penal chrg Applied Credit to GL 100
                                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), "100", dt.Rows[0]["PlAccNo2"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString())).ToString(), "1", "12", "TR_INT", "1", "PENCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                    }
                                                }
                                            }

                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString())));
                                        }
                                        else if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                        {
                                            //Penal Charge Credit To GL 12
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "PENCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString()) > 0)
                                                {
                                                    //Penal Interest Debit To 3 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "3", "2", "7", "Penal Interest Debit", dt.Rows[0]["CurrPInterest"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                {
                                                    //Penal Interest Credit To 3 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["PInterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "3", "1", "7", "Penal Interest Credit", TotalDrAmt.ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }

                                            if (resultout > 0)
                                            {
                                                //Penal Charge Contra
                                                if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["PInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                {
                                                    //Penal chrg Applied Debit To GL 12
                                                    resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["PInterestGl"].ToString(), dt.Rows[0]["PInterestSub"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), "2", "12", "TR_INT", "1", "PENDR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                    if (resultout > 0)
                                                    {
                                                        //Penal chrg Applied Credit to GL 100
                                                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), "100", dt.Rows[0]["PlAccNo2"].ToString(), AC, TxtCustName.Text, TotalDrAmt.ToString(), "1", "12", "TR_INT", "1", "PENCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                    }
                                                }
                                            }

                                            TotalDrAmt = 0;
                                        }
                                    }

                                    //  For Interest
                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())))
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                            {
                                                //interest Credit to GL 11
                                                resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), AC, TxtCustName.Text, (Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "INTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString()) > 0)
                                                {
                                                    //Current Interest Debit To 2 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "2", "2", "7", "Interest Debit", dt.Rows[0]["CurrInterest"].ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                                {
                                                    //Current Interest Credit To 2 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "2", "1", "7", "Interest Credit", Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString())).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }

                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())));
                                        }
                                        else if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                        {
                                            //interest Credit to GL 11
                                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), dt.Rows[0]["InterestGl"].ToString(), dt.Rows[0]["InterestSub"].ToString(), AC, TxtCustName.Text, Convert.ToDouble(TotalDrAmt).ToString(), DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "1", "INTCR", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString()) > 0)
                                                {
                                                    //Current Interest Debit To 2 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "2", "2", "7", "Interest Debit", Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }

                                            if (resultout > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["Interest"].ToString()) < 0 ? "0" : dt.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(dt.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : dt.Rows[0]["CurrInterest"].ToString())) > 0)
                                                {
                                                    //Current Interest Credit To 2 In AVS_LnTrx
                                                    resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), dt.Rows[0]["InterestSub"].ToString(), TxtAccNo.Text.Trim().ToString(), "2", "1", "7", "Interest Credit", Convert.ToDouble(TotalDrAmt.ToString()).ToString(), "1", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                                }
                                            }

                                            TotalDrAmt = 0;
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
                    //Insert Data into Temporary Table (Avs_TempMultiTransfer) in Database here
                    if (ddlPMTMode.SelectedValue.ToString() == "1")
                    {
                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text, AC, TxtCustName.Text, TxtAmount.Text, DdlCRDR.SelectedValue == "1" ? "1" : "2", DdlCRDR.SelectedValue == "1" ? "3" : "4", DdlCRDR.SelectedValue == "1" ? "CR" : "CP", "0", "By Cash", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());

                        if (resultout > 0)
                        {
                            resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString(), "99", "99", "0", TxtCustName.Text, TxtAmount.Text, DdlCRDR.SelectedValue == "1" ? "2" : "1", DdlCRDR.SelectedValue == "1" ? "3" : "4", DdlCRDR.SelectedValue == "1" ? "CR" : "CP", "0", "By Cash", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                        }
                    }
                    else if (ddlPMTMode.SelectedValue.ToString() == "2")
                        resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text, AC, TxtCustName.Text, TxtAmount.Text, DdlCRDR.SelectedValue == "1" ? "1" : "2", "7", "TR", "0", "By Transfer", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                    else if (ddlPMTMode.SelectedValue.ToString() == "3")
                    {
                        ST = MT.CheckGlGroup(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString());
                        if (String.IsNullOrEmpty(ST))
                        {
                            if (DdlCRDR.SelectedValue == "1")
                                resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text, AC, TxtCustName.Text, TxtAmount.Text, DdlCRDR.SelectedValue == "1" ? "1" : "2", "5", "TR", "0", "By Cheque", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                            if (DdlCRDR.SelectedValue == "2")
                                resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text, AC, TxtCustName.Text, TxtAmount.Text, DdlCRDR.SelectedValue == "1" ? "1" : "2", "6", "TR", "0", "By Cheque", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                        }
                        else if (ST.Trim().ToString() == "CBB")
                        {
                            if (DdlCRDR.SelectedValue == "1")
                                resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text, AC, TxtCustName.Text, TxtAmount.Text, DdlCRDR.SelectedValue == "1" ? "1" : "2", "6", "TR", "0", "By Cheque", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                            else if (DdlCRDR.SelectedValue == "2")
                                resultout = ITrans.InsertIntoTable(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString() == "" ? "0" : txtCustNo.Text.Trim().ToString(), ViewState["GlCode"].ToString(), TxtPtype.Text, AC, TxtCustName.Text, TxtAmount.Text, DdlCRDR.SelectedValue == "1" ? "1" : "2", "5", "TR", "0", "By Cheque", TxtNarration.Text, CN, CD, Session["EntryDate"].ToString(), Session["MID"].ToString());
                        }
                    }
                }

                if (resultout > 0)
                {
                    BindGrid();
                    ddlPMTMode.Enabled = false;
                    lblMessage.Text = "Successfully Added...!!";
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Multi_voucher _" + TxtPtype.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }
            }
            else
            {
                lblMessage.Text = "Enter Amount First...!!";
                ModalPopup.Show(this.Page);
                return;
            }

            if (resultout > 0)
            {
                DdlCRDR.SelectedValue = "1";
                Getinfo();
                ClearText();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnPost_Click(object sender, EventArgs e)
    {
        try
        {
            string PAYMAST = "MULTITRANSFER";
            RefNumber = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
            ViewState["RID"] = (Convert.ToInt32(RefNumber) + 1).ToString();

            if (Convert.ToDouble(TxtDiff.Text.Trim().ToString() == "" ? "0" : TxtDiff.Text.Trim().ToString()) == 0.00)
            {
                //Get All Transaction From Temporary Table For First Set Number
                DT = new DataTable();
                DT = MT.GetTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                sResult = MT.GetCrTrans(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                if (DT.Rows.Count > 0)
                {
                    //Generate First Set Number Here
                    ST = "";
                    if (Convert.ToDouble(sResult) > 0)
                        ST = BD.GetSetNo(Session["EntryDate"].ToString(), "IBTSetNo", Session["BRCD"].ToString());
                    else
                        ST = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString());

                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        //Insert Data to Original Table Here
                        if (Convert.ToDouble(DT.Rows[i]["GLCODE"].ToString()) == 4 && Convert.ToDouble(DT.Rows[i]["TRXTYPE"].ToString()) == 1)
                        {
                            if (DT.Rows[i]["ACTIVITY"].ToString() == "3")
                                Activity = "43";
                            else if (DT.Rows[i]["ACTIVITY"].ToString() == "7")
                                Activity = "43";
                            else
                                Activity = "44";

                            //ShareSuspGl = MT.GetShareSuspGl(Session["BRCD"].ToString());
                            //// Insert Record Into Avs_Acc Table Under subglcode (e.g 44)
                            //if (ShareSuspGl > 0)
                            //    ShareAccNo = accop.insert(Session["BRCD"].ToString(), "4", ShareSuspGl.ToString(), DT.Rows[i]["CUSTNO"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), "1", "1", "", "", "", "", "", "", "", "", "", "", "", "", "0", "0", "1003", Session["BRCD"].ToString(), "0", "0");

                            //if (ShareAccNo > 0)
                            //{
                            //    // credit and debit through share suspense account
                            //    GlCode = BD.GetCashSubglcode(ShareSuspGl.ToString(), Session["BRCD"].ToString());

                            //    if (Convert.ToDouble(GlCode) > 0)
                            //    {
                            //        resultout = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode.ToString(), ShareSuspGl.ToString(), ShareAccNo.ToString(), "ABB- From " + Session["BRCD"].ToString() + " To 1 - " + DT.Rows[i]["SUBGLCODE"].ToString() + "/" + DT.Rows[i]["ACCNO"].ToString() + "", DT.Rows[i]["PARTICULARS2"].ToString(), DT.Rows[i]["AMOUNT"].ToString(), "1", DT.Rows[i]["ACTIVITY"].ToString(), DT.Rows[i]["PmtMode"].ToString(), ST, DT.Rows[i]["InstNo"].ToString(), DT.Rows[i]["InstDate"].ToString(), "0", "0", "1001", "", DT.Rows[i]["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), DT.Rows[i]["BRCD"].ToString(), PAYMAST, DT.Rows[i]["CUSTNO"].ToString(), DT.Rows[i]["CustName"].ToString(), ViewState["RID"].ToString(), "0");

                            //        resultout = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode.ToString(), ShareSuspGl.ToString(), ShareAccNo.ToString(), "ABB- From " + Session["BRCD"].ToString() + " To 1 - " + DT.Rows[i]["SUBGLCODE"].ToString() + "/" + DT.Rows[i]["ACCNO"].ToString() + "", DT.Rows[i]["PARTICULARS2"].ToString(), DT.Rows[i]["AMOUNT"].ToString(), "2", DT.Rows[i]["ACTIVITY"].ToString(), DT.Rows[i]["PmtMode"].ToString(), ST, DT.Rows[i]["InstNo"].ToString(), DT.Rows[i]["InstDate"].ToString(), "0", "0", "1001", "", DT.Rows[i]["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), DT.Rows[i]["BRCD"].ToString(), PAYMAST, DT.Rows[i]["CUSTNO"].ToString(), DT.Rows[i]["CustName"].ToString(), ViewState["RID"].ToString(), "0");
                            //    }
                            //}

                            if (Convert.ToDouble(Session["BRCD"].ToString()) != 1)
                            {
                                //Credit to Selected Branch
                                DT2 = MT.GetADMSubGl("1");
                                resultout = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(), "0", "ABB- From " + Session["BRCD"].ToString() + " To 1 - " + DT.Rows[i]["SUBGLCODE"].ToString() + "/" + DT.Rows[i]["ACCNO"].ToString() + "", DT.Rows[i]["PARTICULARS2"].ToString(), DT.Rows[i]["AMOUNT"].ToString(), "1", DT.Rows[i]["ACTIVITY"].ToString(), DT.Rows[i]["PmtMode"].ToString(), ST, DT.Rows[i]["InstNo"].ToString(), DT.Rows[i]["InstDate"].ToString(), "0", "0", "1001", "", DT.Rows[i]["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), DT.Rows[i]["BRCD"].ToString(), PAYMAST, DT.Rows[i]["CUSTNO"].ToString(), DT.Rows[i]["CustName"].ToString(), ViewState["RID"].ToString(), "0");

                                if (resultout > 0)
                                {
                                    //Debit to Selected Branch
                                    DT2 = MT.GetADMSubGl(Session["BRCD"].ToString());
                                    resultout = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT2.Rows[0]["ADMGlCode"].ToString(), DT2.Rows[0]["ADMSubGlCode"].ToString(), "0", "ABB- From " + Session["BRCD"].ToString() + " To 1 - " + DT.Rows[i]["SUBGLCODE"].ToString() + "/" + DT.Rows[i]["ACCNO"].ToString() + "", DT.Rows[i]["PARTICULARS2"].ToString(), DT.Rows[i]["AMOUNT"].ToString(), "2", Activity.ToString(), DT.Rows[i]["PmtMode"].ToString(), ST, DT.Rows[i]["InstNo"].ToString(), DT.Rows[i]["InstDate"].ToString(), "0", "0", "1001", "", "1", Session["MID"].ToString(), Session["BRCD"].ToString(), DT.Rows[i]["BRCD"].ToString(), PAYMAST, DT.Rows[i]["CUSTNO"].ToString(), DT.Rows[i]["CustName"].ToString(), ViewState["RID"].ToString(), "0");
                                }
                            }

                            //  Added By Amol On 2018-02-05 for additional share allotment
                            if (Convert.ToDouble(Session["BRCD"].ToString()) == 1)
                            {
                                resultout = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i]["GLCODE"].ToString(), DT.Rows[i]["SUBGLCODE"].ToString(), DT.Rows[i]["ACCNO"].ToString(), "ABB- From " + Session["BRCD"].ToString() + " To 1 - " + DT.Rows[i]["SUBGLCODE"].ToString() + "/" + DT.Rows[i]["ACCNO"].ToString() + "", DT.Rows[i]["PARTICULARS2"].ToString(), DT.Rows[i]["AMOUNT"].ToString(), "1", DT.Rows[i]["ACTIVITY"].ToString(), DT.Rows[i]["PmtMode"].ToString(), ST, DT.Rows[i]["InstNo"].ToString(), DT.Rows[i]["InstDate"].ToString(), "0", "0", "1001", "", DT.Rows[i]["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), DT.Rows[i]["BRCD"].ToString(), PAYMAST, DT.Rows[i]["CUSTNO"].ToString(), DT.Rows[i]["CustName"].ToString(), ViewState["RID"].ToString(), "0");
                            }
                            else
                            {
                                resultout = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i]["GLCODE"].ToString(), DT.Rows[i]["SUBGLCODE"].ToString(), DT.Rows[i]["ACCNO"].ToString(), "ABB- From " + Session["BRCD"].ToString() + " To 1 - " + DT.Rows[i]["SUBGLCODE"].ToString() + "/" + DT.Rows[i]["ACCNO"].ToString() + "", DT.Rows[i]["PARTICULARS2"].ToString(), DT.Rows[i]["AMOUNT"].ToString(), "1", Activity.ToString(), DT.Rows[i]["PmtMode"].ToString(), ST, DT.Rows[i]["InstNo"].ToString(), DT.Rows[i]["InstDate"].ToString(), "0", "0", "1001", "", "1", Session["MID"].ToString(), Session["BRCD"].ToString(), DT.Rows[i]["BRCD"].ToString(), PAYMAST, DT.Rows[i]["CUSTNO"].ToString(), DT.Rows[i]["CustName"].ToString(), ViewState["RID"].ToString(), "0");
                            }

                            if (resultout > 0)
                            {
                                resultout = MT.InsertData(Session["BRCD"].ToString(), "2", DT.Rows[i]["ACCNO"].ToString(), DT.Rows[i]["CUSTNO"].ToString(), "1", "100", DT.Rows[i]["AMOUNT"].ToString(), ST, "1", "Share Application", "Share Application", Session["EntryDate"].ToString(), Session["MID"].ToString());
                            }
                        }
                        else
                        {
                            resultout = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i]["GLCODE"].ToString(), DT.Rows[i]["SUBGLCODE"].ToString(), DT.Rows[i]["ACCNO"].ToString(), DT.Rows[i]["PARTICULARS"].ToString(), DT.Rows[i]["PARTICULARS2"].ToString(), DT.Rows[i]["AMOUNT"].ToString(), DT.Rows[i]["TRXTYPE"].ToString(), DT.Rows[i]["ACTIVITY"].ToString(), DT.Rows[i]["PmtMode"].ToString(), ST, DT.Rows[i]["InstNo"].ToString(), DT.Rows[i]["InstDate"].ToString(), "0", "0", "1001", "", DT.Rows[i]["BRCD"].ToString(), Session["MID"].ToString(), Session["BRCD"].ToString(), DT.Rows[i]["BRCD"].ToString(), PAYMAST, DT.Rows[i]["CUSTNO"].ToString(), DT.Rows[i]["CustName"].ToString(), ViewState["RID"].ToString(), "0");
                        }
                    }

                    //Get All Transaction From Temporary Table (TempLnTrx)
                    DT = new DataTable();
                    DT = MT.GetLnTrxTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        //Insert Data to Original Table Here
                        resultout = ITrans.LoanTrx(Session["BRCD"].ToString(), DT.Rows[i]["LoanGlCode"].ToString(), DT.Rows[i]["SubGlCode"].ToString(), DT.Rows[i]["AccountNo"].ToString(), DT.Rows[i]["HeadDesc"].ToString(), DT.Rows[i]["TrxType"].ToString(), DT.Rows[i]["Activity"].ToString(), DT.Rows[i]["Narration"].ToString(), DT.Rows[i]["Amount"].ToString(), ST, "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());

                        if (resultout > 0 && DT.Rows[i]["HeadDesc"].ToString() == "2" && DT.Rows[i]["TrxType"].ToString() == "2")
                        {
                            string IntApp = LI.GetIntApp(Session["BRCD"].ToString(), DT.Rows[i]["LoanGlCode"].ToString());
                            if (Convert.ToDouble(IntApp.ToString()) == 1)
                            {
                                resultout = LI.UpdateLastIntDate(Session["BRCD"].ToString(), DT.Rows[i]["LoanGlCode"].ToString(), DT.Rows[i]["AccountNo"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                            }
                        }
                    }
                }

                //Get All Transaction From Temporary Table For Second Set Number
                DT = new DataTable();
                DT = MT.GetTransDetails1(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                if (DT.Rows.Count > 0)
                {
                    //Generate Second Set Number Here
                    SetNo = "";
                    SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString());
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        //Insert Data to Original Table Here
                        resultout = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i]["GLCODE"].ToString(), DT.Rows[i]["SUBGLCODE"].ToString(), DT.Rows[i]["ACCNO"].ToString(), DT.Rows[i]["PARTICULARS"].ToString(), DT.Rows[i]["PARTICULARS2"].ToString(), DT.Rows[i]["AMOUNT"].ToString(), DT.Rows[i]["TRXTYPE"].ToString(), DT.Rows[i]["ACTIVITY"].ToString(), DT.Rows[i]["PmtMode"].ToString(), SetNo.ToString(), DT.Rows[i]["InstNo"].ToString(), DT.Rows[i]["InstDate"].ToString(), "0", "0", "1003", "", DT.Rows[i]["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", PAYMAST, DT.Rows[i]["CUSTNO"].ToString(), DT.Rows[i]["CustName"].ToString(), ViewState["RID"].ToString(), "0");
                    }

                    //Get All Transaction From Temporary Table (TempLnTrx)
                    DT = new DataTable();
                    DT = MT.GetLnTrxTransDetails1(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        //Insert Data to Original Table Here
                        resultout = ITrans.LoanTrx(Session["BRCD"].ToString(), DT.Rows[i]["LoanGlCode"].ToString(), DT.Rows[i]["SubGlCode"].ToString(), DT.Rows[i]["AccountNo"].ToString(), DT.Rows[i]["HeadDesc"].ToString(), DT.Rows[i]["TrxType"].ToString(), DT.Rows[i]["Activity"].ToString(), DT.Rows[i]["Narration"].ToString(), DT.Rows[i]["Amount"].ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                    }
                }

                ClearAll();
                //Delete All Data From Temporary Table Here
                MT.DelAllRecTable(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                grdvoucher.Visible = false;

                if (resultout > 0)
                {
                    ddlPMTMode.Enabled = true;
                    btnPost.Enabled = false;
                    ddlPMTMode.Focus();
                    lblMessage.Text = "Transfer Seccessfully With Set No : '" + ST.ToString() + "'...!!";
                    ModalPopup.Show(this.Page);
                    FL = "Insert";
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Multi_voucher _post_" + TxtPtype.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;
                }
            }
            else
            {
                lblMessage.Text = "Amount Difference in Credit and Debit Transaction...!!";
                ModalPopup.Show(this.Page);
                return;
            }
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
            ClearText();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Exit_Click(object sender, EventArgs e)
    {
        try
        {
            //Delete All Data From Temporary Table Here
            MT.DelAllRecTable(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
            return;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkbtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string id = objlink.CommandArgument;

            resultout = MT.DeleteSingleRecTable(id, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (resultout > 0)
            {
                lblMessage.Text = "Record Deleted Successfully...!!";
                ModalPopup.Show(this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Multi_voucher _Del_" + TxtPtype.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }

            BindGrid();
            Getinfo();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Public Function

    public void Getinfo()
    {
        try
        {
            DataTable DT = new DataTable();
            DT = MT.GetCRDR(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (DT.Rows.Count > 0)
            {
                TxtCRBAL.Text = DT.Rows[0]["CREDIT"].ToString();
                TxtDRBAL.Text = DT.Rows[0]["DEBIT"].ToString();
                double CR, DR;
                CR = DR = 0;
                CR = Convert.ToDouble(TxtCRBAL.Text);
                DR = Convert.ToDouble(TxtDRBAL.Text);

                TxtDiff.Text = (CR - DR).ToString();

                if (CR == DR)
                {
                    btnPost.Enabled = true;
                    btnPost.Focus();
                }
                else
                {
                    btnPost.Enabled = false;
                    TxtPtype.Focus();
                }
            }
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
            int RC = MT.Getinfotable(grdvoucher, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void NewWindowsVerify(string url)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + url + "', 'popup_window', 'width=600,height=250,left=50,top=50,resizable=no');", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearText()
    {
        try
        {
            TxtPtype.Text = "";
            TxtPname.Text = "";
            TxtAccNo.Text = "";
            TxtCustName.Text = "";
            txtCustNo.Text = "";

            TxtBalance.Text = "";
            TxtTotalBal.Text = "";

            TxtChequeNo.Text = "";
            TxtChequeDate.Text = Session["EntryDate"].ToString();

            TxtNarration.Text = "";
            TxtAmount.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearAll()
    {
        try
        {
            DdlCRDR.SelectedIndex = 0;
            TxtCRBAL.Text = "";
            TxtDRBAL.Text = "";
            TxtDiff.Text = "";

            TxtPtype.Text = "";
            TxtPname.Text = "";
            TxtAccNo.Text = "";
            TxtCustName.Text = "";

            TxtBalance.Text = "";
            TxtTotalBal.Text = "";

            TxtChequeNo.Text = "";
            TxtChequeDate.Text = "";

            TxtNarration.Text = "";
            TxtAmount.Text = "";
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

    public void Photo_Sign()
    {
        try
        {
            string FileName = "";
            //  Added By amol On 2018-02-06 for share account name search
            if ((TxtPtype.Text.ToString() == "4") && (Convert.ToDouble(DdlCRDR.SelectedValue) == 1))
            {
                Param = CC.getShrParam();
                if (Param == "HO" || Param == "ho" || Param == "Ho")
                Image = CC.ShowIMAGE(txtCustNo.Text, "1", TxtAccNo.Text);
                else
                    Image = CC.ShowIMAGE(txtCustNo.Text, Session["BRCD"].ToString(), TxtAccNo.Text);
            }
            else
                Image = CC.ShowIMAGE(txtCustNo.Text, Session["BRCD"].ToString(), TxtAccNo.Text);

            if (Image.Rows.Count > 0)
            {
                int i = 0;
                String FilePath = "";
                byte[] bytes = null;
                for (int y = 0; y < 2; y++)
                {
                    if (y == 0)
                    {
                        FilePath = Image.Rows[i]["SignIMG"].ToString();
                        if (FilePath != "")
                            bytes = (byte[])Image.Rows[i]["SignIMG"];

                    }
                    else
                    {
                        FilePath = Image.Rows[i]["PhotoImg"].ToString();
                        if (FilePath != "")
                            bytes = (byte[])Image.Rows[i]["PhotoImg"];
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
    public void Clear()
    {
        try
        {
            TxtAccNo.Text = "";
            TxtCustName.Text = "";
            txtCustNo.Text = "";
            TxtBalance.Text = "";
            TxtTotalBal.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

}