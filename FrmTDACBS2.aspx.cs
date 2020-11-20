using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Oracle;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data.SqlClient;

public partial class FrmTDACBS2 : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsTDAClear CurrentCls1 = new ClsTDAClear();
    ClsBindDropdown BD = new ClsBindDropdown();
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    ClsCommon CMN = new ClsCommon();
    scustom customcs = new scustom();
    ClsNomineeMaster NM = new ClsNomineeMaster();
    ClsNewTDA CurrentCls = new ClsNewTDA();
    ClsBindDropdown ddlbind = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    ClsAuthorized VOEN = new ClsAuthorized();
    ClsAccopen accop = new ClsAccopen();
    ClsMISTransfer MT = new ClsMISTransfer();
    ClsTDACalculator ObjCalcu = new ClsTDACalculator();
    int result = 0;
    string sResult = "", SetNo = "0";
    String ScrollNo = "0";
    string sql = "", FL = "";
    DataTable DT = new DataTable();

    // Array for Interest calculator
    float[] RtnArray = new float[2];
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
                Response.Redirect("FrmLogin.aspx");

            if (!IsPostBack)
            {
                LogDetails();

                ddlbind.BindBRANCHNAME(ddlTrfBrName, null);
                ddlTrfBrName.SelectedValue = Session["BRCD"].ToString();
                txtTrfBrCode.Text = Session["BRCD"].ToString();

                hdentrydt.Value = Session["EntryDate"].ToString();
                autoglname.ContextKey = Session["BRCD"].ToString();
                Autoprd4.ContextKey = Session["BRCD"].ToString();
                AutoSPrdcode.ContextKey = Session["BRCD"].ToString();
                BindGrid();
                ddlbind.BindOperation(ddlOpType);
                ddlbind.BindAccType(ddlAccType);
                ddlbind.BindIntrstPayout(ddlIntrestPay);
                dtDeposDate.Text = Session["EntryDate"].ToString();
                Txtcustno.Enabled = false;

                string FL = Request.QueryString["FL"].ToString();
                ViewState["FL"] = FL;
                if (ViewState["FL"].ToString() == "ACO")
                {
                    Fn_AddNew();
                    string P = Request.QueryString["P"].ToString();
                    string A = Request.QueryString["A"].ToString();
                    string R = Request.QueryString["R"].ToString();
                    ViewState["PCode"] = string.IsNullOrEmpty(P.ToString()) ? "0" : P.ToString();
                    ViewState["ACode"] = string.IsNullOrEmpty(A.ToString()) ? "0" : A.ToString();
                    ViewState["RecSrnoCode"] = string.IsNullOrEmpty(R.ToString()) ? "0" : R.ToString();
                    TxtProcode.Text = ViewState["PCode"].ToString();
                    txtAccNo.Text = ViewState["ACode"].ToString();
                    TxtRecNo.Text = ViewState["RecSrnoCode"].ToString();
                    ProcdeOpr();
                    string ANM = NM.GetAccName(txtAccNo.Text, TxtProcode.Text, Session["BRCD"].ToString());
                    string[] AC = ANM.Split('-');

                    ViewState["CT"] = AC[1].ToString();

                    FillData();
                }
                btnAddNew.Focus();
            }
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

            if (TxtProcode.Text == "")
            {
                TxtProcode.Focus();
                TxtProcode.Text = "";
            }
            ProcdeOpr();
        }
        catch (Exception eX)
        {
            ExceptionLogging.SendErrorToText(eX);
        }
    }
    public void ProcdeOpr()
    {
        try
        {
            int result = 0;
            string GlS1;
            int.TryParse(TxtProcode.Text, out result);
            TxtProName.Text = customcs.GetProductName(result.ToString(), Session["BRCD"].ToString());
            GlS1 = BD.GetAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString());
            ViewState["CATEGORY"] = MT.GetDepositCat(Session["BRCD"].ToString(), TxtProcode.Text, "MISTRF");
            if (GlS1 != null)
            {
                string[] GLS = GlS1.Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();
                int GL = 0;
                int.TryParse(ViewState["DRGL"].ToString(), out GL);
                Set_PeriodType();
                SetIntPayout();
                txtAccNo.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter Valid Product code!....", this.Page);
                TxtProcode.Text = "";
                TxtProcode.Text = "";
                TxtProcode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    // Get Customer name from Account no and product code
    protected void txtAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Flag"].ToString() == "AD")
            {
                string Para_Recsrno = CMN.GetUniversalPara("RECSRNO_AVAIL");
                if (Para_Recsrno == "Y")
                {
                    int RC = CurrentCls.CheckAccountCBS2(TxtProcode.Text, txtAccNo.Text, Session["BRCD"].ToString());
                    if (RC == 2)
                    {
                        string RecNo = CurrentCls.GetRecNoCBS2("GETNEXT_RECNO", TxtProcode.Text, txtAccNo.Text, Session["BRCD"].ToString());
                        if (RecNo != null)
                        {
                            TxtRecNo.Text = RecNo;
                        }
                        else
                        {
                            WebMsgBox.Show("Receipt Not available for Account...!", this.Page);
                            txtAccNo.Text = "";
                            txtAccNo.Focus();
                            return;
                        }
                    }
                    else if (RC == 1)
                    {
                        clear();
                        TxtProcode.Focus();
                        WebMsgBox.Show("Sorry Already have Deposit Receipt ...!!", this.Page);
                        return;
                    }
                    else
                    {
                        WebMsgBox.Show("Invalid Account Number....!", this.Page);
                        txtAccNo.Text = "";
                        TxtAccName.Text = "";
                        txtAccNo.Focus();
                    }
                }
                else
                {
                    int RC = CurrentCls.CheckAccountCBS2(TxtProcode.Text, txtAccNo.Text, Session["BRCD"].ToString());
                    if (RC == 1)
                    {
                        clear();
                        TxtProcode.Focus();
                        WebMsgBox.Show("Sorry Already have Deposit Receipt ...!!", this.Page);
                        return;
                    }
                }
            }

            string ACNM = NM.GetAccName(txtAccNo.Text, TxtProcode.Text, Session["BRCD"].ToString());
            string[] AC = ACNM.Split('-');

            ViewState["CT"] = AC[1].ToString();
            if (TxtRecNo.Text == "")
            {
                FillData();
            }
            else
            {
                FillData_RecSrno();
            }
            // BindGrid();
            //}
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void FillData_RecSrno()
    {
        try
        {
            dt = CurrentCls.GetCustNoName(TxtProcode.Text.ToString(), txtAccNo.Text.ToString(), Session["BRCD"].ToString());

            if (dt.Rows.Count > 0)
            {
                //if (ViewState["op"].ToString() == "AD")
                //{

                //    txtAccNo.Text = "";
                //    TxtAccName.Text = "";
                //    lblMessage.Text = "Already Have Deposit ............!!";
                //    ModalPopup.Show(this.Page);
                //    lblMessage.Text = "";
                //}
                string Reciptno = NM.GetRecieptNo(Session["BRCD"].ToString(), TxtProcode.Text);
                TxtReceiptNo.Text = Reciptno;
                Txtcustno.Text = dt.Rows[0][0].ToString();
                ddlAccType.SelectedValue = dt.Rows[0][1].ToString();
                ddlOpType.SelectedValue = dt.Rows[0][2].ToString();
                TxtAccName.Text = dt.Rows[0][3].ToString();
                ddlIntrestPay.Focus();


                string Cat = NM.GEtIntPayout(Session["BRCD"].ToString(), TxtProcode.Text);
                if (Cat == "MIS")
                {
                    ddlIntrestPay.SelectedValue = "1";
                }
                else if (Cat == "QIS")
                {
                    ddlIntrestPay.SelectedValue = "2";
                }
                else if (Cat == "HIF")
                {
                    ddlIntrestPay.SelectedValue = "3";
                }
                else if (Cat == "FDS")
                {
                    ddlIntrestPay.SelectedValue = "4";
                }
                else
                {
                    ddlIntrestPay.SelectedValue = "0";
                }


            }
            else
            {
                WebMsgBox.Show("Account Number not found", this.Page);
                txtAccNo.Text = "";
                //Txtcustno.Text = "";
                TxtAccName.Text = "";
                ddlAccType.SelectedIndex = 0;
                ddlOpType.SelectedIndex = 0;
                TxtDepoAmt.Text = "";
                ddlduration.SelectedIndex = 0;
                TxtPeriod.Text = "";
                TxtRate.Text = "";
                TxtIntrest.Text = "";
                TxtMaturity.Text = "";
                DtDueDate.Text = "";
                txtAccNo.Focus();
            }

            dt1 = CurrentCls.GetAllFieldData(TxtProcode.Text.ToString(), txtAccNo.Text.ToString(), Session["BRCD"].ToString(), ViewState["RecSrno"].ToString());
            if (dt1.Rows.Count > 0)
            {
                string STAGE = "";
                STAGE = dt1.Rows[0]["STAGE"].ToString();
                if ((ViewState["Flag"].ToString() == "MD" || ViewState["Flag"].ToString() == "DL") && Session["UGRP"].ToString() != "1")
                {
                    if (STAGE == "1003")
                    {
                        clear();
                        WebMsgBox.Show("Record Authorized cannot Modify ...!!", this.Page);
                        return;
                    }
                    else if (STAGE == "1004")
                    {

                        clear();
                        WebMsgBox.Show("Record Not Present ...!!", this.Page);
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
    public void FillData()
    {
        try
        {
            dt = CurrentCls.GetCustNoName(TxtProcode.Text.ToString(), txtAccNo.Text.ToString(), Session["BRCD"].ToString());
            if (dt.Rows.Count > 0)
            {
                string Reciptno = NM.GetRecieptNo(Session["BRCD"].ToString(), TxtProcode.Text);
                TxtReceiptNo.Text = Reciptno;
                Txtcustno.Text = dt.Rows[0][0].ToString();
                ddlAccType.SelectedValue = dt.Rows[0][1].ToString();
                ddlOpType.SelectedValue = dt.Rows[0][2].ToString();
                TxtAccName.Text = dt.Rows[0][3].ToString();
                ddlIntrestPay.Focus();

                string Cat = NM.GEtIntPayout(Session["BRCD"].ToString(), TxtProcode.Text);
                if (Cat == "MIS")
                    ddlIntrestPay.SelectedValue = "1";
                else if (Cat == "QIS")
                    ddlIntrestPay.SelectedValue = "2";
                else if (Cat == "HIF")
                    ddlIntrestPay.SelectedValue = "3";
                else if (Cat == "FDS" || Cat == "CUM")
                    ddlIntrestPay.SelectedValue = "4";
                else
                    ddlIntrestPay.SelectedValue = "0";

                dtDeposDate.Focus();
            }
            else
            {
                WebMsgBox.Show("Account Number not found", this.Page);
                txtAccNo.Text = "";
                //Txtcustno.Text = "";
                TxtAccName.Text = "";
                ddlAccType.SelectedIndex = 0;
                ddlOpType.SelectedIndex = 0;
                TxtDepoAmt.Text = "";
                ddlduration.SelectedIndex = 0;
                TxtPeriod.Text = "";
                TxtRate.Text = "";
                TxtIntrest.Text = "";
                TxtMaturity.Text = "";
                DtDueDate.Text = "";
                txtAccNo.Focus();
            }

            dt1 = CurrentCls.GetAllFieldData(TxtProcode.Text.ToString(), txtAccNo.Text.ToString(), Session["BRCD"].ToString(), ViewState["RecSrno"].ToString());
            if (dt1.Rows.Count > 0)
            {
                string STAGE = "";
                STAGE = dt1.Rows[0]["STAGE"].ToString();
                if ((ViewState["Flag"].ToString() == "MD" || ViewState["Flag"].ToString() == "DL") && Session["UGRP"].ToString() != "1")
                {
                    if (STAGE == "1003")
                    {
                        clear();
                        WebMsgBox.Show("Record Authorized cannot Modify ...!!", this.Page);
                        return;
                    }
                    else if (STAGE == "1004")
                    {

                        clear();
                        WebMsgBox.Show("Record Not Present ...!!", this.Page);
                        return;
                    }
                }

                TxtReceiptNo.Text = dt1.Rows[0]["RECEIPT_NO"].ToString();
                Txtcustno.Text = dt1.Rows[0]["CUSTNO"].ToString();
                TxtAccName.Text = dt1.Rows[0]["CUSTNAME"].ToString();
                txtAccNo.Text = dt1.Rows[0]["CUSTACCNO"].ToString();
                ddlAccType.SelectedValue = dt1.Rows[0]["ACC_TYPE"].ToString();
                ddlOpType.SelectedValue = dt1.Rows[0]["OPR_TYPE"].ToString();
                dtDeposDate.Text = dt1.Rows[0]["OPENINGDATE"].ToString();
                ddlIntrestPay.Text = dt1.Rows[0]["INTPAYOUT"].ToString();
                TxtDepoAmt.Text = dt1.Rows[0]["PRNAMT"].ToString();
                TxtPeriod.Text = dt1.Rows[0]["PERIOD"].ToString();
                ddlduration.SelectedValue = dt1.Rows[0]["PRDTYPE"].ToString();
                TxtRate.Text = dt1.Rows[0]["RATEOFINT"].ToString();
                TxtIntrest.Text = Convert.ToInt32(dt1.Rows[0]["INTAMT"]).ToString();
                TxtMaturity.Text = Convert.ToInt32(dt1.Rows[0]["MATURITYAMT"]).ToString();
                DtDueDate.Text = dt1.Rows[0]["DUEDATE"].ToString();
                TxtProcode4.Text = dt1.Rows[0]["TRFSUBTYPE"].ToString();
                TxtProName4.Text = customcs.GetProductName(TxtProcode4.Text, Session["BRCD"].ToString());
                TxtAccNo4.Text = dt1.Rows[0]["TRFACCNO"].ToString();

                if (TxtAccNo4.Text.ToString() == "0")
                    TxtAccName4.Text = " ";
                else
                    TxtAccName4.Text = NM.GetAccName(TxtAccNo4.Text.ToString(), TxtProcode4.Text.ToString(), Session["BRCD"].ToString());

                dtDeposDate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    // Assign Values to all controls
    protected void AssignValues(DataTable dt)
    {
        try
        {
            dt = CurrentCls.GetAllFieldData(TxtProcode.Text.ToString(), txtAccNo.Text.ToString(), Session["BRCD"].ToString(), ViewState["RecSrno"].ToString());
            if (dt.Rows.Count > 0)
            {
                //Txtcustno.Text = dt.Rows[0]["CUSTNO"].ToString();
                TxtAccName.Text = dt.Rows[0]["CUSTNAME"].ToString();
                txtAccNo.Text = dt.Rows[0]["CUSTACCNO"].ToString();
                ddlAccType.SelectedValue = dt.Rows[0]["ACC_TYPE"].ToString();
                ddlOpType.SelectedValue = dt.Rows[0]["OPR_TYPE"].ToString();
                dtDeposDate.Text = dt.Rows[0]["OPENINGDATE"].ToString();
                ddlIntrestPay.Text = dt.Rows[0]["INTPAYOUT"].ToString();
                TxtDepoAmt.Text = dt.Rows[0]["PRNAMT"].ToString();
                TxtPeriod.Text = dt.Rows[0]["PERIOD"].ToString();
                ddlduration.SelectedValue = dt.Rows[0]["PRDTYPE"].ToString();
                TxtRate.Text = dt.Rows[0]["RATEOFINT"].ToString();
                TxtIntrest.Text = Convert.ToInt32(dt.Rows[0]["INTAMT"]).ToString();
                TxtMaturity.Text = Convert.ToInt32(dt.Rows[0]["MATURITYAMT"]).ToString();
                DtDueDate.Text = dt.Rows[0]["DUEDATE"].ToString();
            }
            else
            {
                WebMsgBox.Show("Invalid No...", this.Page);
                txtAccNo.Text = "";
                txtAccNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    // Deposit Entry
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtProcode.Text.ToString() == "")
            {
                TxtProcode.Focus();
                WebMsgBox.Show("Enter product code first ...!!", this.Page);
                return;
            }
            else if (txtAccNo.Text.ToString() == "")
            {
                txtAccNo.Focus();
                WebMsgBox.Show("Enter account code first ...!!", this.Page);
                return;
            }
            else if (ddlAccType.Text.ToString() == "")
            {
                ddlAccType.Focus();
                WebMsgBox.Show("Enter account type first ...!!", this.Page);
                return;
            }
            else if (ddlOpType.SelectedValue == "0")
            {
                ddlOpType.Focus();
                WebMsgBox.Show("Enter member type first ...!!", this.Page);
                return;
            }
            else if (Convert.ToDouble(TxtDepoAmt.Text.ToString() == "" ? "0" : TxtDepoAmt.Text.ToString()) <= 0)
            {
                TxtDepoAmt.Focus();
                WebMsgBox.Show("Enter deposit amount first ...!!", this.Page);
                return;
            }
            else if (ddlduration.SelectedValue == "0")
            {
                ddlduration.Focus();
                WebMsgBox.Show("Select duration first ...!!", this.Page);
                return;
            }
            else if (TxtPeriod.Text.ToString() == "")
            {
                TxtPeriod.Focus();
                WebMsgBox.Show("Enter period first ...!!", this.Page);
                return;
            }
            else if (TxtRate.Text.ToString() == "")
            {
                TxtRate.Focus();
                WebMsgBox.Show("Enter rate of interest first ...!!", this.Page);
                return;
            }
            else if (Convert.ToDouble(TxtIntrest.Text.ToString() == "" ? "0" : TxtIntrest.Text.ToString()) <= 0)
            {
                TxtIntrest.Focus();
                WebMsgBox.Show("Enter interest amount first ...!!", this.Page);
                return;
            }
            else if (Convert.ToDouble(TxtMaturity.Text.ToString() == "" ? "0" : TxtMaturity.Text.ToString()) <= 0)
            {
                TxtMaturity.Focus();
                WebMsgBox.Show("Enter maturity amount first ...!!", this.Page);
                return;
            }
            else if (DtDueDate.Text.ToString() == "")
            {
                DtDueDate.Focus();
                WebMsgBox.Show("Enter duedate first ...!!", this.Page);
                return;
            }
            else
            {
                string STR = MT.GetDepositCat(Session["BRCD"].ToString(), TxtProcode.Text, "MISTRF");
                if ((STR == "MIS") && (txtTrfBrCode.Text.ToString() == ""))
                {
                    ddlTrfBrName.Text = "";
                    txtTrfBrCode.Focus();
                    WebMsgBox.Show("Enter Transfer Account for MIS Deposit ...!!", this.Page);
                    return;
                }
                else if ((STR == "MIS") && (TxtProcode4.Text.ToString() == ""))
                {
                    TxtProcode4.Text = "";
                    TxtProcode4.Focus();
                    WebMsgBox.Show("Enter Transfer Account for MIS Deposit ...!!", this.Page);
                    return;
                }
                else if ((STR == "MIS") && (TxtAccNo4.Text.ToString() == ""))
                {
                    TxtAccNo4.Text = "";
                    TxtAccNo4.Focus();
                    WebMsgBox.Show("Enter Transfer Account for MIS Deposit ...!!", this.Page);
                    return;
                }
                else
                {
                    // To Deposit table
                    if (ViewState["Flag"].ToString() == "AD")
                    {
                        int result = CurrentCls.EntryDepositeCBS2(Txtcustno.Text.ToString(), txtAccNo.Text.ToString(), TxtProcode.Text.ToString(), TxtDepoAmt.Text.ToString(), TxtRate.Text.ToString(),
                            dtDeposDate.Text.ToString(), DtDueDate.Text.ToString(), TxtPeriod.Text.ToString(), Convert.ToDouble(TxtIntrest.Text.ToString()).ToString(), Convert.ToDouble(TxtMaturity.Text.ToString()).ToString(),
                            "1001", Session["BRCD"].ToString(), Session["MID"].ToString(), ddlIntrestPay.SelectedValue, ddlduration.SelectedValue, "1", txtTrfBrCode.Text.ToString(), TxtProcode4.Text.ToString(),
                            TxtAccNo4.Text.ToString(), TxtReceiptNo.Text.ToString(), TxtRecNo.Text.ToString() == "" ? "0" : TxtRecNo.Text.ToString());
                        if (result > 0)
                        {
                            clear();
                            WebMsgBox.Show("Deposit Created Succesfully ...!!", this.Page);
                            CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Deposit_Add _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            return;
                        }
                        else
                        {
                            WebMsgBox.Show("Could not be saved ...!!", this.Page);
                            return;
                        }
                    }
                    else if (ViewState["Flag"].ToString() == "MD")
                    {
                        if ((Session["UGRP"].ToString() == "1") || (Session["UGRP"].ToString() == "2") || (Session["UGRP"].ToString() == "3"))
                        {
                            int result = CurrentCls.ModifyDepositCBS2(Txtcustno.Text.ToString(), txtAccNo.Text.ToString(), TxtProcode.Text.ToString(), TxtDepoAmt.Text.ToString(), TxtRate.Text.ToString(),
                                dtDeposDate.Text.ToString(), DtDueDate.Text.ToString(), TxtPeriod.Text.ToString(), Convert.ToDouble(TxtIntrest.Text.ToString()).ToString(), Convert.ToDouble(TxtMaturity.Text.ToString()).ToString(),
                                "1001", Session["BRCD"].ToString(), Session["MID"].ToString(), ddlIntrestPay.SelectedValue.ToString(), ddlduration.SelectedValue, txtTrfBrCode.Text, TxtProcode4.Text, TxtAccNo4.Text, TxtReceiptNo.Text, TxtRecNo.Text);
                            if (result > 0)
                            {
                                clear();
                                BindGridPro();
                                TxtProcode.Focus();
                                WebMsgBox.Show("Record Modify Successfully ...!!", this.Page);
                                CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Deposit_Modify _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                return;
                            }
                            else
                            {
                                TxtProcode.Focus();
                                WebMsgBox.Show("Could not be saved ...!!", this.Page);
                                return;
                            }
                        }
                        else
                        {
                            WebMsgBox.Show("User is restricted to Modify ...!!", this.Page);
                            return;
                        }
                    }
                    else if (ViewState["Flag"].ToString() == "DL" || ViewState["Flag"].ToString() == "AT")
                    {
                        string ST = ViewState["Flag"].ToString() == "DL" ? "1004" : "1003";
                        int RC = CurrentCls.DelAuthoCBS2(TxtProcode.Text, txtAccNo.Text, Session["BRCD"].ToString(), ST, Session["MID"].ToString(), TxtRecNo.Text);
                        if (ST == "1003")
                        {
                            clear();
                            BindGridPro();
                            TxtProcode.Focus();
                            WebMsgBox.Show("Record Authorize Successfully ...!!", this.Page);
                            CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Deposit_Authorized _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            return;
                        }
                        else
                        {

                            clear();
                            BindGridPro();
                            TxtProcode.Focus();
                            WebMsgBox.Show("Record Deleted Successfully ...!!", this.Page);
                            CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Deposit_Delete _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            return;
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
    protected void Btn_Redirect_Click(object sender, EventArgs e)
    {
        try
        {
            if (Rdb_Trf.Checked == true)
            {
                HttpContext.Current.Response.Redirect("FrmMultiVoucher.aspx?&FL=ACO&P=" + TxtProcode.Text + "&A=" + txtAccNo.Text + "&DepAmt=" + TxtDepoAmt.Text + "&R=" + TxtRecNo.Text + "", false);
                clear();
            }
            else
            {
                HttpContext.Current.Response.Redirect("FrmCashReceipt.aspx?op=AD&FL=ACO&P=" + TxtProcode.Text + "&A=" + txtAccNo.Text + "&R=" + TxtRecNo.Text + "", false);
                clear();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtPeriod_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtPeriod.Text.ToString() == "")
                return;

            if (TxtDepoAmt.Text == "")
            {
                WebMsgBox.Show("Please enter amount", this.Page);
                TxtDepoAmt.Focus();
                return;
            }

            if (ViewState["Flag"].ToString() == "MD" || ViewState["Flag"].ToString() == "DL" || ViewState["Flag"].ToString() == "AT")
            {
                string GlS1 = BD.GetAccTypeGL(ViewState["Depositglcode"].ToString(), Session["BRCD"].ToString());
                if (GlS1 != null)
                {
                    string[] GLS = GlS1.Split('_');
                    ViewState["DRGL"] = GLS[1].ToString();
                    AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();
                    int GL = 0;
                    int.TryParse(ViewState["DRGL"].ToString(), out GL);
                    txtAccNo.Focus();
                }
            }

            if (ViewState["Flag"].ToString() == "AD" && ViewState["FL"].ToString() != "ACO")
            {
                string GlS1 = BD.GetAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString());
                if (GlS1 != null)
                {
                    string[] GLS = GlS1.Split('_');
                    ViewState["DRGL"] = GLS[1].ToString();
                    AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();
                    int GL = 0;
                    int.TryParse(ViewState["DRGL"].ToString(), out GL);
                    txtAccNo.Focus();
                }
            }

            // Check Duration
            string IsPvalid = "0";
            if (ViewState["DRGL"].ToString() == "5")
                IsPvalid = CurrentCls.CheckPeriod(TxtProcode.Text.ToString(), TxtPeriod.Text.ToString(), ddlduration.SelectedValue.ToString(), Session["BRCD"].ToString(), ddlAccType.SelectedValue);
            else
                IsPvalid = CurrentCls.CheckPeriod(ViewState["DRGL"].ToString(), TxtPeriod.Text.ToString(), ddlduration.SelectedValue.ToString(), Session["BRCD"].ToString(), ddlAccType.SelectedValue);

            if (Convert.ToInt32(IsPvalid) <= 0)
            {
                if (ViewState["DRGL"].ToString() == "5")
                    WebMsgBox.Show("Invalid Period in " + ddlduration.SelectedItem.Text + " - " + TxtPeriod.Text + " for " + TxtProName.Text + " ...", this.Page);
                else if (ViewState["DRGL"].ToString() == "15")
                    WebMsgBox.Show("Invalid Period in " + ddlduration.SelectedItem.Text + " - " + TxtPeriod.Text + " for GLCODE " + ViewState["DRGL"].ToString() + " ...", this.Page);

                TxtPeriod.Text = "";
                TxtRate.Text = "";
                TxtIntrest.Text = "";
                TxtMaturity.Text = "";
                DtDueDate.Text = "";
                TxtPeriod.Focus();
                return;
            }

            // Get rates for Product Code
            string CUSTTYPE = ddlAccType.SelectedValue;
            double rate = 0;
            if (ViewState["DRGL"].ToString() == "5")
                rate = Convert.ToDouble(CurrentCls.GetInterestRateED("TDA", "", TxtProcode.Text, "", "", CUSTTYPE.ToString(), ddlduration.SelectedValue.ToString(), TxtPeriod.Text.ToString(), dtDeposDate.Text, Session["EntryDate"].ToString(), true, "NOACC"));
            else
                rate = Convert.ToDouble(CurrentCls.GetInterestRateED("TDA", "", ViewState["DRGL"].ToString(), "", "", CUSTTYPE.ToString(), ddlduration.SelectedValue.ToString(), TxtPeriod.Text.ToString(), dtDeposDate.Text, Session["EntryDate"].ToString(), true, "NOACC"));

            if (rate == 0)
            {
                WebMsgBox.Show(" Invalid Value... ", this.Page);
                TxtPeriod.Text = "";
                TxtProcode4.Focus();
                return;
            }
            else
            {
                TxtRate.Text = rate.ToString();
            }

            if (ddlduration.SelectedValue == "M")
                DtDueDate.Text = conn.AddMonthDay(dtDeposDate.Text, TxtPeriod.Text, "M").Replace("12:00:00", "");
            else if (ddlduration.SelectedValue == "D")
                DtDueDate.Text = conn.AddMonthDay(dtDeposDate.Text, TxtPeriod.Text, "D").Replace("12:00:00", "");

            // Calculate Interest 
            float amt = (float)Convert.ToDouble(TxtDepoAmt.Text);
            float intrate = (float)Convert.ToDouble(TxtRate.Text);
            CalculatedepositINT(amt, TxtProcode.Text.ToString(), intrate, Convert.ToInt32(TxtPeriod.Text), ddlIntrestPay.SelectedItem.Text.ToString(), ddlduration.SelectedItem.Text.ToString());
            TDS_Calc();

            ddlTrfBrName.Focus();
        }
        catch (Exception Ex)
        {
            WebMsgBox.Show(Ex.Message, this.Page);
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
                    //txtDueDate.Text = duedate.ToShortDateString();
                    break;

                case "Months":
                    duedate = DeposDate.AddMonths(duration);
                    DateTime duedate1 = duedate.AddDays(-1);
                    DtDueDate.Text = duedate1.ToShortDateString();
                    //txtDueDate.Text = duedate1.ToShortDateString();
                    break;

                case "Years":
                    duedate = DeposDate.AddYears(duration);
                    DtDueDate.Text = duedate.ToShortDateString();
                    // txtDueDate.Text = duedate.ToShortDateString();
                    break;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    //INREREST CALCULATOR
    protected void CalculatedepositINT(double amt, string subgl, float intrate, int Period, string intpay, string PTYPE)
    {
        try
        {
            double interest = 0;
            double maturityamt = 0;
            string category;

            category = CurrentCls1.GetTDACategory(Session["BRCD"].ToString(), TxtProcode.Text);
            if (category == "")
            {
                return;
            }

            switch (category)
            {
                case "LKP":
                    DT = CurrentCls.GetLKPData(TxtProcode.Text, Period.ToString(), ddlduration.SelectedValue.ToString(), TxtDepoAmt.Text, Session["EntryDate"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        TxtMaturity.Text = DT.Rows[0]["MATAMT"].ToString();
                        double INTAMT = Convert.ToDouble((Convert.ToDouble(TxtMaturity.Text) - Convert.ToDouble(TxtDepoAmt.Text)) * Convert.ToDouble(TxtPeriod.Text));
                        TxtIntrest.Text = INTAMT.ToString();
                        TxtReceiptNo.Focus();
                    }
                    else
                    {
                        WebMsgBox.Show("Invalid Details Deposit Amount or Period....", this.Page);
                        TxtRate.Text = "";
                        TxtPeriod.Focus();
                    }
                    break;

                case "LKPRD":

                    DT = CurrentCls.GetLKPData(TxtProcode.Text, Period.ToString(), ddlduration.SelectedValue.ToString(), TxtDepoAmt.Text, Session["EntryDate"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        TxtMaturity.Text = DT.Rows[0]["MATAMT"].ToString();
                        double INTAMT = Convert.ToDouble((Convert.ToDouble(TxtMaturity.Text) - Convert.ToDouble(TxtDepoAmt.Text)) * Convert.ToDouble(TxtPeriod.Text));
                        TxtIntrest.Text = INTAMT.ToString();
                        TxtReceiptNo.Focus();
                    }
                    else
                    {
                        WebMsgBox.Show("Invalid Details Deposit Amount or Period....", this.Page);
                        TxtRate.Text = "";
                        TxtPeriod.Focus();
                    }
                    break;

                case "MIS":
                    DT = CurrentCls.GetRDCalc("MIS", amt.ToString(), ddlIntrestPay.SelectedValue.ToString(), ddlduration.SelectedValue.ToString(), TxtPeriod.Text, intrate.ToString());
                    if (DT != null)
                    {
                        string intamt = DT.Rows[0]["Interest"].ToString();
                        string MatAmt = DT.Rows[0]["Maturity"].ToString();

                        TxtIntrest.Text = Convert.ToDouble(intamt).ToString("N");
                        TxtMaturity.Text = Convert.ToDouble(MatAmt).ToString("N");
                    }
                    else
                    {
                        WebMsgBox.Show("Calculation failed...!", this.Page);
                    }
                    break;

                case "CUM":
                    //  Added by amol on 13/11/2018 (as per same condition apply on fd calculator)
                    DT = CurrentCls.GetRDCalc("CUM", amt.ToString(), ddlIntrestPay.SelectedValue.ToString(), ddlduration.SelectedValue.ToString(), TxtPeriod.Text, intrate.ToString());
                    if (DT != null)
                    {
                        string intamt = DT.Rows[0]["Interest"].ToString();
                        string MatAmt = DT.Rows[0]["Maturity"].ToString();

                        TxtIntrest.Text = Convert.ToDouble(intamt).ToString("N");
                        TxtMaturity.Text = Convert.ToDouble(MatAmt).ToString("N");
                    }
                    else
                    {
                        WebMsgBox.Show("Calculation failed...!", this.Page);
                    }
                    break;

                case "FDS":
                    DT = CurrentCls.GetRDCalc("FDS", amt.ToString(), ddlIntrestPay.SelectedValue.ToString(), ddlduration.SelectedValue.ToString(), TxtPeriod.Text, intrate.ToString());
                    if (DT != null)
                    {
                        string intamt = DT.Rows[0]["Interest"].ToString();
                        string MatAmt = DT.Rows[0]["Maturity"].ToString();

                        TxtIntrest.Text = Convert.ToDouble(intamt).ToString("N");
                        TxtMaturity.Text = Convert.ToDouble(MatAmt).ToString("N");
                    }
                    else
                    {
                        WebMsgBox.Show("Calculation failed...!", this.Page);
                    }
                    break;

                case "FDSS":
                    DT = CurrentCls.GetRDCalc("FDS", amt.ToString(), ddlIntrestPay.SelectedValue.ToString(), ddlduration.SelectedValue.ToString(), TxtPeriod.Text, intrate.ToString());
                    if (DT != null)
                    {
                        string intamt = DT.Rows[0]["Interest"].ToString();
                        string MatAmt = DT.Rows[0]["Maturity"].ToString();

                        TxtIntrest.Text = Convert.ToDouble(intamt).ToString("N");
                        TxtMaturity.Text = Convert.ToDouble(MatAmt).ToString("N");
                    }
                    else
                    {
                        WebMsgBox.Show("Calculation failed...!", this.Page);
                    }
                    break;

                case "RD":
                    DT = CurrentCls.GetRDCalc("RD", amt.ToString(), ddlIntrestPay.SelectedValue.ToString(), ddlduration.SelectedValue.ToString(), TxtPeriod.Text, intrate.ToString());
                    if (DT != null)
                    {
                        string intamt = DT.Rows[0]["Interest"].ToString();
                        string MatAmt = DT.Rows[0]["Maturity"].ToString();

                        TxtIntrest.Text = Convert.ToDouble(intamt).ToString("N");
                        TxtMaturity.Text = Convert.ToDouble(MatAmt).ToString("N");
                    }
                    else
                    {
                        WebMsgBox.Show("Calculation failed...!", this.Page);
                    }
                    break;

                case "DP":

                    if (PTYPE == "Days" || PTYPE == "DAYS" || PTYPE == "idvasa")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 365) * (Period));
                        maturityamt = (interest) + (amt);
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                        TxtReceiptNo.Focus();
                    }
                    else if (PTYPE == "Months" || PTYPE == "MONTHS" || PTYPE == "maihnao")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (Period));
                        maturityamt = (interest) + (amt);
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                        TxtReceiptNo.Focus();
                    }
                    break;

                case "DD":
                    interest = amt;
                    maturityamt = interest + amt;
                    TxtIntrest.Text = interest.ToString("N");
                    TxtMaturity.Text = maturityamt.ToString("N");
                    TxtReceiptNo.Focus();
                    break;

                default:
                    WebMsgBox.Show("Category not found...!", this.Page);
                    break;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void clear()
    {
        try
        {
            TxtProName.Text = "";
            TxtProcode.Text = "";
            txtAccNo.Text = "";
            Txtcustno.Text = "";
            TxtAccName.Text = "";

            TxtDepoAmt.Text = "";
            TxtPeriod.Text = "";
            TxtRate.Text = "";
            TxtIntrest.Text = "";
            TxtMaturity.Text = "";
            DtDueDate.Text = "";
            TxtReceiptNo.Text = "";
            TxtTDSRate.Text = "";
            TxtTDSAmount.Text = "";
            TxtTDSRemark.Text = "";

            ddlTrfBrName.SelectedValue = Session["BRCD"].ToString();
            txtTrfBrCode.Text = Session["BRCD"].ToString();
            TxtProcode4.Text = "";
            TxtProName4.Text = "";
            TxtAccName4.Text = "";
            TxtAccNo4.Text = "";

            TxtProcode.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
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

    // Product Name in Transfer
    protected void Txtcustno4_TextChanged(object sender, EventArgs e)
    {

    }

    protected void BtnSubmit4_Click(object sender, EventArgs e)
    {
        try
        {
            TxtProcode.Focus();
            result = 0;
            result = CurrentCls.EntryTransfer(TxtProcode.Text.ToString(), txtAccNo.Text.ToString(), TxtProcode4.Text.ToString(), TxtAccNo4.Text.ToString(), Session["BRCD"].ToString());
            if (result > 0)
            {
                WebMsgBox.Show("Record Submitted successfully", this.Page);
                FL = "Insert";//Dhanya Shetty
                string Result = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Transfer_Add _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grdMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdMaster.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void CallGrid()
    {
        try
        {
            string RC = NM.CheckStage(ViewState["strnumId"].ToString(), Session["BRCD"].ToString());
            if (RC == "1003")
            {
                WebMsgBox.Show("Record Already Authorized........!!", this.Page);
                return;
            }
            DataTable DT = new DataTable();
            DT = NM.GetInfo(ViewState["strnumId"].ToString(), txtAccNo.Text, ViewState["CT"].ToString(), Session["BRCD"].ToString());
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
            NM.BindDataCBS2(grdMaster, Session["BRCD"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindGridPro()
    {
        try
        {
            NM.BindDataProCBS2(grdMaster, Session["BRCD"].ToString(), TxtSPrdCode.Text, TxtSAccno.Text);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public DateTime InsertDate(string EDT)
    {
        int RC;
        try
        {
            string sql = "insert into TDatetime('" + EDT + "'";
            RC = conn.sExecuteQuery(sql);
            if (RC > 0)
            {
                sql = "select Getdate from TDatetime";
                SqlCommand cmd = new SqlCommand(sql, conn.GetDBConnection());
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    if (sdr.HasRows)
                    {
                        EDT = sdr[0].ToString();
                    }
                }
            }
            return Convert.ToDateTime(EDT);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return Convert.ToDateTime(EDT);
        }
        return Convert.ToDateTime(EDT);
    }

    protected void txttynam_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] CT = TxtProName.Text.Split('_');
            if (CT.Length > 0)
            {
                TxtProName.Text = CT[0].ToString();
                TxtProcode.Text = CT[1].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();

                int GL = 0;
                int.TryParse(ViewState["DRGL"].ToString(), out GL);

                if (TxtProName.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    TxtProcode.Text = "";
                    TxtProcode.Focus();
                }
                txtAccNo.Focus();
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
                TxtAccName.Text = custnob[0].ToString();
                txtAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string ACNM = NM.GetAccName(txtAccNo.Text, TxtProcode.Text, Session["BRCD"].ToString());
                string[] AC = ACNM.Split('-');
                Txtcustno.Text = AC[1].ToString();
                ViewState["CT"] = AC[1].ToString();

                int RC = CurrentCls.CheckAccount(TxtProcode.Text, txtAccNo.Text, Session["BRCD"].ToString());
                if (RC == 2)
                {
                    string RecNo = CurrentCls.GetRecNo("GETNEXT_RECNO", TxtProcode.Text, txtAccNo.Text, Session["BRCD"].ToString());
                    if (RecNo != null)
                    {
                        TxtRecNo.Text = RecNo;
                    }
                    else
                    {
                        WebMsgBox.Show("Receipt Not available for Account...!", this.Page);
                        txtAccNo.Text = "";
                        TxtAccName.Text = "";
                        txtAccNo.Focus();
                        return;
                    }
                }
                else if (RC == 1)
                {
                    clear();
                    TxtProcode.Focus();
                    WebMsgBox.Show("Sorry Already have Deposit Receipt ...!!", this.Page);
                    return;
                }

                ddlIntrestPay.Focus();
                FillData();
            }
            else
            {
                WebMsgBox.Show("Invalid Account Number ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ENDN(bool TF)
    {
        dtDeposDate.Enabled = TF;
        DtDueDate.Enabled = TF;
        ddlIntrestPay.Enabled = TF;
        TxtDepoAmt.Enabled = TF;
        ddlduration.Enabled = TF;
        TxtPeriod.Enabled = TF;
        TxtProcode4.Enabled = TF;
        TxtProName4.Enabled = TF;
        TxtAccName4.Enabled = TF;
        TxtAccNo4.Enabled = TF;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        Fn_AddNew();
        TxtProcode.Focus();
    }

    public void Fn_AddNew()
    {
        try
        {
            Main.Visible = false;
            Search.Visible = false;

            DIV_Term.Visible = true;
            ViewState["Flag"] = "AD";
            BtnSubmit.Text = "Submit";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnExist_Click(object sender, EventArgs e)
    {
        Search.Visible = true;
        DIV_Term.Visible = false;
    }

    protected void TxtSAccno_TextChanged(object sender, EventArgs e)
    {
        BindGridPro();
        Search.Visible = false;

        string accname = NM.GetAccnameaa(Session["BRCD"].ToString(), TxtSPrdCode.Text, TxtSAccno.Text);
        if (accname == null || accname == "")
            WebMsgBox.Show("please Enter Correct Account No ...!!", this.Page);
        else
            TxtSaccName.Text = accname;
    }

    protected void TxtSPrdCode_TextChanged(object sender, EventArgs e)
    {

        try
        {
            int result = 0;
            string GlS1;
            int.TryParse(TxtSPrdCode.Text, out result);
            TXtSPrdname.Text = customcs.GetProductName(result.ToString(), Session["BRCD"].ToString());
            GlS1 = BD.GetAccTypeGL(TxtSPrdCode.Text, Session["BRCD"].ToString());
            if (GlS1 != null)
            {
                string[] GLS = GlS1.Split('_');
                ViewState["SDRGL"] = GLS[1].ToString();
                AutoSAccno.ContextKey = Session["BRCD"].ToString() + "_" + TxtSPrdCode.Text + "_" + ViewState["SDRGL"].ToString();
                int GL = 0;
                int.TryParse(ViewState["SDRGL"].ToString(), out GL);
                TxtSAccno.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter Valid Product code!....", this.Page);
                TxtSPrdCode.Text = "";
                TXtSPrdname.Text = "";
                TxtSPrdCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["Depositglcode"] = ARR[0].ToString();
            ViewState["CustAccno"] = ARR[1].ToString();
            ViewState["RecSrno"] = ARR[2].ToString();
            ViewState["Flag"] = "MD";
            if (Session["UGRP"].ToString() == "1" || Session["UGRP"].ToString() == "2" || Session["UGRP"].ToString() == "3")//12/02/2017(only Ceo,admin,manager can modify)Dhanya Shetty
            {
                DIV_Term.Visible = true;
                BtnSubmit.Text = "Modify";
                Search.Visible = false;
                Main.Visible = false;
                getalldata();

                dtDeposDate.Focus();
            }
            else
            {
                WebMsgBox.Show("User is restricted to Modify", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["Depositglcode"] = ARR[0].ToString();
            ViewState["CustAccno"] = ARR[1].ToString();
            ViewState["RecSrno"] = ARR[2].ToString();
            ViewState["Flag"] = "DL";
            DIV_Term.Visible = true;
            BtnSubmit.Text = "Delete";
            Main.Visible = false;
            Search.Visible = false;
            getalldata();

            BtnSubmit.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkAuthorize_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["Depositglcode"] = ARR[0].ToString();
            ViewState["CustAccno"] = ARR[1].ToString();
            ViewState["RecSrno"] = ARR[2].ToString();
            ViewState["Flag"] = "AT";
            DIV_Term.Visible = true;
            BtnSubmit.Text = "Authorise";
            Search.Visible = false;
            Main.Visible = false;
            getalldata();

            BtnSubmit.Focus();
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
            dt1 = CurrentCls.GetAllFieldDataCBS2(ViewState["Depositglcode"].ToString(), ViewState["CustAccno"].ToString(), Session["BRCD"].ToString(), ViewState["RecSrno"].ToString());
            if (dt1.Rows.Count > 0)
            {
                string STAGE = "";
                STAGE = dt1.Rows[0]["STAGE"].ToString();

                if ((ViewState["Flag"].ToString() == "MD" || ViewState["Flag"].ToString() == "DL") && Session["UGRP"].ToString() != "1")
                {
                    if (STAGE == "1003")
                    {
                        clear();
                        WebMsgBox.Show("Record Authorized cannot Modify ...!!", this.Page);
                        return;
                    }
                    else if (STAGE == "1004")
                    {

                        clear();
                        WebMsgBox.Show("Record Not Present ...!!", this.Page);
                        return;
                    }
                }
                TxtRecNo.Text = dt1.Rows[0]["RECSRNO"].ToString();
                TxtProName.Text = NM.GetProductName(ViewState["Depositglcode"].ToString(), Session["BRCD"].ToString());
                TxtProcode.Text = ViewState["Depositglcode"].ToString();
                TxtReceiptNo.Text = dt1.Rows[0]["RECEIPT_NO"].ToString();
                Txtcustno.Text = dt1.Rows[0]["CUSTNO"].ToString();
                TxtAccName.Text = dt1.Rows[0]["CUSTNAME"].ToString();
                txtAccNo.Text = dt1.Rows[0]["CUSTACCNO"].ToString();
                ddlAccType.SelectedValue = dt1.Rows[0]["ACC_TYPE"].ToString();
                ddlOpType.SelectedValue = dt1.Rows[0]["OPR_TYPE"].ToString();
                dtDeposDate.Text = dt1.Rows[0]["OPENINGDATE"].ToString();
                ddlIntrestPay.Text = dt1.Rows[0]["INTPAYOUT"].ToString();
                TxtDepoAmt.Text = dt1.Rows[0]["PRNAMT"].ToString();
                TxtPeriod.Text = dt1.Rows[0]["PERIOD"].ToString();
                ddlduration.SelectedValue = dt1.Rows[0]["PRDTYPE"].ToString();
                TxtRate.Text = dt1.Rows[0]["RATEOFINT"].ToString();
                TxtIntrest.Text = Convert.ToInt32(dt1.Rows[0]["INTAMT"]).ToString();
                TxtMaturity.Text = Convert.ToInt32(dt1.Rows[0]["MATURITYAMT"]).ToString();
                DtDueDate.Text = dt1.Rows[0]["DUEDATE"].ToString();

                txtTrfBrCode.Text = dt1.Rows[0]["TrfBrCd"].ToString();
                ddlTrfBrName.SelectedValue = txtTrfBrCode.Text.ToString();
                TxtProcode4.Text = dt1.Rows[0]["TRFSUBTYPE"].ToString();
                TxtProName4.Text = customcs.GetProductName(TxtProcode4.Text.ToString(), txtTrfBrCode.Text.ToString());

                TxtAccNo4.Text = dt1.Rows[0]["TRFACCNO"].ToString();
                //TxtAccName4.Text = NM.GetAccName(TxtAccNo4.Text, TxtProcode4.Text.ToString(), txtTrfBrCode.Text.ToString());
                sResult = CurrentCls1.GetAccName(TxtAccNo4.Text, TxtProcode4.Text.ToString(), txtTrfBrCode.Text.ToString());
                string[] CustName = sResult.Split('-');
                TxtAccName4.Text = CustName[0].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Rdb_Trf_CheckedChanged(object sender, EventArgs e)
    {
        TxtProcode4.Focus();
    }

    protected void Rdb_CR_CheckedChanged(object sender, EventArgs e)
    {
        TxtProcode4.Focus();
    }

    public void Opr_TRFCR()
    {
        try
        {
            Lbl_DepositMsg.Text = "Deposit Created successfully, Click on Redirect for transaction...!";
            if (Rdb_Trf.Checked == true)
                Btn_Redirect.Text = "Redirect to Trf";
            else
                Btn_Redirect.Text = "Redirect to CR";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#DIV_REDIRECT').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtRecNo_TextChanged(object sender, EventArgs e)
    {

    }

    protected void TXtSPrdname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TXtSPrdname.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TXtSPrdname.Text = CT[0].ToString();
                TxtSPrdCode.Text = CT[1].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtSPrdCode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["SDRGL"] = GLS[1].ToString();
                AutoSAccno.ContextKey = Session["BRCD"].ToString() + "_" + TxtSPrdCode.Text + "_" + ViewState["SDRGL"].ToString();

                int GL = 0;
                int.TryParse(ViewState["SDRGL"].ToString(), out GL);

                if (TXtSPrdname.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    TxtSPrdCode.Text = "";
                    TxtSPrdCode.Focus();
                }
                else
                {
                    TxtSAccno.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtSaccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtSaccName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtSaccName.Text = custnob[0].ToString();
                TxtSAccno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string ACNM = NM.GetAccName(TxtSAccno.Text, TxtSPrdCode.Text, Session["BRCD"].ToString());
                string[] AC = ACNM.Split('-');

                BindGridPro();
            }
            else
            {
                WebMsgBox.Show("Invalid Account Number ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void SetIntPayout()
    {
        try
        {
            string Ip = CurrentCls.GetIntPayout(Session["BRCD"].ToString(), TxtProcode.Text);
            if (Ip != null)
            {
                if (Ip == "M" || Ip == "m")
                    ddlIntrestPay.SelectedValue = "1";
                else if (Ip == "Q" || Ip == "q")
                    ddlIntrestPay.SelectedValue = "2";
                else if (Ip == "H" || Ip == "h")
                    ddlIntrestPay.SelectedValue = "3";
                else if (Ip == "Y" || Ip == "y")
                    ddlIntrestPay.SelectedValue = "5";
                else
                    ddlIntrestPay.SelectedValue = "4";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void TDS_Calc()
    {
        try
        {
            double TDSAmt = 0, TDSRate = 0;
            string RR, Remark;

            string Panno = PanCard(Session["BRCD"].ToString(), Txtcustno.Text);
            string EliAmt = CMN.GetUniversalPara("TDS_ELIAMT");

            if (Panno != null)
            {
                RR = ObjCalcu.Get_TDSRate("GetTDSRate_WithPan", TxtProcode.Text);
                Remark = "Pan no - " + Panno + ", Interest Amount should be Greater than " + EliAmt + " to TDS Calc";
                Lnk_RedirectModCust.Visible = false;
            }
            else
            {
                RR = ObjCalcu.Get_TDSRate("GetTDSRate_NoPan", TxtProcode.Text);
                Remark = "Pan no not avaible, Interest Amount should be Greater than " + EliAmt + " to TDS";
                Lnk_RedirectModCust.Visible = true;
            }

            if (EliAmt != null && Convert.ToDouble(TxtIntrest.Text) >= Convert.ToDouble(EliAmt))
            {
                if (TxtIntrest.Text != "")
                {
                    if (RR != null)
                    {
                        TDSRate = Convert.ToDouble(RR);
                        TDSAmt = Convert.ToDouble(TxtIntrest.Text) * TDSRate / 100;

                        TxtTDSRate.Text = TDSRate.ToString() + " %";
                        TxtTDSAmount.Text = Math.Round(TDSAmt, 0).ToString();
                    }
                    else
                    {
                        WebMsgBox.Show("TDS Rate not Found...!", this.Page);
                    }

                    TxtTDSRemark.Text = Remark;
                }
            }
            else
            {
                TxtTDSRate.Text = "0";
                TxtTDSAmount.Text = "0";
                TxtTDSRemark.Text = Remark;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void Set_PeriodType()
    {
        try
        {
            string RR = CurrentCls.Get_PeriodType(TxtProcode.Text);
            if (RR != null)
            {
                if (RR != "0")
                    ddlduration.SelectedValue = RR;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public string PanCard(string BRCD, string CustNo)
    {
        string PanNo = CurrentCls.GetPanNo(BRCD, CustNo);
        return PanNo;
    }

    protected void Lnk_RedirectModCust_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmAVS5023.aspx?FL=MD", false);
    }

    protected void ddlTrfBrName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtTrfBrCode.Text = ddlTrfBrName.SelectedValue;
            if (Convert.ToDouble(txtTrfBrCode.Text.ToString()) > 0)
            {
                TrfClear();
                Autoprd4.ContextKey = txtTrfBrCode.Text.ToString();

                TxtProcode4.Focus();
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

    protected void TrfClear()
    {
        try
        {
            TxtProcode4.Text = "";
            TxtProName4.Text = "";
            TxtAccNo4.Text = "";
            TxtAccName4.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtProcode4_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ProductCode();
        }
        catch (Exception eX)
        {
            ExceptionLogging.SendErrorToText(eX);
        }
    }

    protected void TxtProName4_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] CT = TxtProName4.Text.Split('_');
            if (CT.Length > 0)
            {
                TxtProcode4.Text = CT[1].ToString();
                ProductCode();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ProductCode()
    {
        try
        {
            if (BD.GetProdOperate(txtTrfBrCode.Text.ToString(), TxtProcode4.Text.ToString()).ToString() != "3")
            {
                sResult = CurrentCls1.GetProduct(txtTrfBrCode.Text.ToString(), TxtProcode4.Text.ToString());
                if (sResult != null)
                {
                    string[] ACC = sResult.Split('_'); ;
                    ViewState["TGL"] = ACC[0].ToString();
                    TxtProName4.Text = ACC[2].ToString();
                    Autoaccname4.ContextKey = txtTrfBrCode.Text.ToString() + "_" + TxtProcode4.Text.ToString();

                    if (CMN.GetIntACCYN(txtTrfBrCode.Text.ToString(), TxtProcode4.Text.ToString()) != "Y")
                    {
                        TxtAccNo4.Text = TxtProcode4.Text;
                        TxtAccName4.Text = TxtProName4.Text;
                    }
                    else
                    {
                        TxtAccNo4.Text = "";
                        TxtAccName4.Text = "";
                    }
                    TxtAccNo4.Focus();
                }
                else
                {
                    TxtProcode4.Text = "";
                    TxtProName4.Text = "";
                    TxtProcode4.Focus();
                    WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                    return;
                }
            }
            else
            {
                TxtProcode4.Text = "";
                TxtProName4.Text = "";
                TxtProcode4.Focus();
                WebMsgBox.Show("Product is not operating ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAccNo4_TextChanged(object sender, EventArgs e)
    {
        try
        {
            AccountNo();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAccName4_TextChanged(object sender, EventArgs e)
    {
        string[] custnob = TxtAccName4.Text.Split('_');
        if (custnob.Length > 1)
        {
            TxtAccNo4.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
            AccountNo();
        }
        else
        {
            WebMsgBox.Show("Invalid Account Number ...!!", this.Page);
            return;
        }
    }

    public void AccountNo()
    {
        try
        {
            sResult = CurrentCls1.GetAccStage(txtTrfBrCode.Text.ToString(), TxtProcode4.Text.ToString(), TxtAccNo4.Text.ToString(), "0");
            if (sResult != null)
            {
                if (sResult != "1003")
                {
                    TxtAccNo4.Text = "";
                    TxtAccName4.Text = "";
                    TxtAccNo4.Focus();
                    WebMsgBox.Show("Sorry Customer not Authorise ...!!", this.Page);
                    return;
                }
                else
                {
                    sResult = CurrentCls1.GetAccStatus(txtTrfBrCode.Text.ToString(), TxtProcode4.Text, TxtAccNo4.Text.ToString());
                    if (sResult == "3")
                    {
                        TxtAccNo4.Text = "";
                        TxtAccName4.Text = "";
                        TxtAccNo4.Focus();
                        WebMsgBox.Show("Already account is closed ...!!", this.Page);
                        return;
                    }
                    else
                    {
                        sResult = CurrentCls1.GetAccName(TxtAccNo4.Text.ToString(), TxtProcode4.Text.ToString(), txtTrfBrCode.Text.ToString());
                        string[] CustName = sResult.Split('-');
                        if (CustName.Length > 0)
                        {
                            TxtAccName4.Text = CustName[0].ToString();

                            BtnSubmit.Focus();
                        }
                    }
                }
            }
            else
            {
                TxtAccNo4.Text = "";
                TxtAccName4.Text = "";
                TxtAccNo4.Focus();
                WebMsgBox.Show("Enter valid account number...!!", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void LogDetails()
    {
        try
        {
            CMN.LogDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "Create Deposit", "", "", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

}