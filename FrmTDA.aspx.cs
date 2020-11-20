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

public partial class FrmTDA : System.Web.UI.Page
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
    int result = 0;
    string SetNo = "0";
    String ScrollNo = "0";
    string sql = "", FL = "", GetSRNo;
    DataTable DT = new DataTable();

    // Array for Interest calculator
    float[] RtnArray = new float[2];
    protected void Page_Load(object sender, EventArgs e)
    {

        string Flag = "";
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            // ddlbind.BindRelation(ddlRelation);
            autoglname.ContextKey = Session["BRCD"].ToString();
            Autoprd4.ContextKey = Session["BRCD"].ToString();
            BindGrid();
            ddlbind.BindOperation(ddlOpType);
            ddlbind.BindAccType(ddlAccType);
            ddlbind.BindIntrstPayout(ddlIntrestPay);
            dtDeposDate.Text = Session["EntryDate"].ToString();
            TxtProcode.Focus();
            Txtcustno.Enabled = false;
            LtrlHeading.Text = "Term Deposit";
            string FL = Request.QueryString["FL"].ToString();
            ViewState["FL"] = FL;
            if (ViewState["FL"].ToString() == "ACO")
            {
                Fn_AddNew();
                string P = Request.QueryString["P"].ToString();
                string A = Request.QueryString["A"].ToString();
                ViewState["PCode"] = string.IsNullOrEmpty(P.ToString()) ? "0" : P.ToString();
                ViewState["ACode"] = string.IsNullOrEmpty(A.ToString()) ? "0" : A.ToString();
                TxtProcode.Text = ViewState["PCode"].ToString();
                txtAccNo.Text = ViewState["ACode"].ToString();
                ProcdeOpr();
                string ANM = NM.GetAccName(txtAccNo.Text, TxtProcode.Text, Session["BRCD"].ToString());
                string[] AC = ANM.Split('-');

                ViewState["CT"] = AC[1].ToString();

                FillData();
            }

            // Get operations from query (Insert / Modify / Authorize) 
            //        op = Request.QueryString["op"].ToString();
            //        ViewState["op"] = op;

            //        if (op == "AD")
            //        {
            //         
            //            LtrlHeading.Text = "Deposit Create";
            //            BtnSubmit.Visible = true; ;
            //            BtnSubmit.Text = "Submit";
            //            ENDN(true);
            //        }
            //        else if (op == "MD")
            //        {
            //            LtrlHeading.Text = "Deposit Modify";
            //            BtnSubmit.Text = "Modify";
            //            ENDN(true);
            //        }
            //        else if (op == "VW")
            //        {
            //            LtrlHeading.Text = "Deposit View";
            //            BtnSubmit.Text = "View";
            //            ENDN(false);
            //        }
            //        else if (op == "AT")
            //        {
            //            LtrlHeading.Text = "Deposit Authorize";
            //            BtnSubmit.Text = "Authorize";
            //            ENDN(false);
            //        }
            //        else if (op == "DL")
            //        {
            //            LtrlHeading.Text = "Deposit Delete";
            //            BtnSubmit.Text = "Delete";
            //            ENDN(false);
            //        }

            //        ddlbind.BindOperation(ddlOpType);
            //        ddlbind.BindAccType(ddlAccType);
            //        ddlbind.BindIntrstPayout(ddlIntrestPay);
            //        dtDeposDate.Text = Session["EntryDate"].ToString();
            //        TxtProcode.Focus();
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    ExceptionLogging.SendErrorToText(Ex);
            //}
        }
    }

    // Customer name
    //protected void Txtcustno_TextChanged(object sender, EventArgs e)
    //{
    //    try 
    //    {
    //    TxtcustName.Text = CMN.GetCustName(Txtcustno.Text.ToString(), Session["BRCD"].ToString());
    //    if (TxtcustName.Text == "" && Txtcustno.Text != "")
    //    {
    //        WebMsgBox.Show("Invalid Customer Number", this.Page);
    //        Txtcustno.Focus();
    //    }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}

    // Product name
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
            // string AT = "";
            // // AT = BD.GetStage1(TxtAccno.Text, Session["BRCD"].ToString(), ViewState["Flag"].ToString());
            // AT = BD.Getstage1(txtAccNo.Text, Session["BRCD"].ToString(), TxtProcode.Text);
            //if (AT != "1003" )
            // {
            //     lblMessage.Text = "";
            //     lblMessage.Text = "Sorry Customer not Authorise.........!!";
            //     ModalPopup.Show(this.Page);

            //     clear();
            // }
            //else
            //{
            if (ViewState["Flag"].ToString() == "AD")
            {
                int RC = CurrentCls.CheckAccount(TxtProcode.Text, txtAccNo.Text, Session["BRCD"].ToString());
                if (RC == 1)
                {
                    clear();
                    TxtProcode.Focus();
                    lblMessage.Text = "Sorry Already have Deposit Receipt...........";
                    ModalPopup.Show(this.Page);
                    //lblMessage.Text = "";
                    return;
                }
            }

            string ACNM = NM.GetAccName(txtAccNo.Text, TxtProcode.Text, Session["BRCD"].ToString());
            string[] AC = ACNM.Split('-');

            ViewState["CT"] = AC[1].ToString();

            FillData();
            // BindGrid();
            //}
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
                else if (Cat == "FDS" || Cat == "CUM" || Cat == "DD" || Cat == "RD")
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

            dt1 = CurrentCls.GetAllFieldData(TxtProcode.Text.ToString(), txtAccNo.Text.ToString(), Session["BRCD"].ToString(), "0");
            if (dt1.Rows.Count > 0)
            {
                string STAGE = "";
                STAGE = dt1.Rows[0]["STAGE"].ToString();
                if ((ViewState["Flag"].ToString() == "MD" || ViewState["Flag"].ToString() == "DL") && Session["UGRP"].ToString() != "1")
                {
                    if (STAGE == "1003")
                    {
                        clear();
                        lblMessage.Text = "Record Authorized cannot Modify.......!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                    else if (STAGE == "1004")
                    {

                        clear();
                        lblMessage.Text = "Record Not Present.......!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                }
                TxtReceiptNo.Text = dt1.Rows[0]["RECEIPT_NO"].ToString();
                Txtcustno.Text = dt1.Rows[0]["CUSTNO"].ToString();
                TxtAccName.Text = dt1.Rows[0]["CUSTNAME"].ToString();
                txtAccNo.Text = dt1.Rows[0]["CUSTACCNO"].ToString();
                ddlAccType.SelectedValue = dt1.Rows[0]["ACC_TYPE"].ToString();
                ddlOpType.SelectedValue = dt1.Rows[0]["OPR_TYPE"].ToString();
                dtDeposDate.Text = Convert.ToDateTime(dt1.Rows[0]["OPENINGDATE"].ToString()).ToString("dd/MM/yyyy");
                ddlIntrestPay.Text = dt1.Rows[0]["INTPAYOUT"].ToString();
                TxtDepoAmt.Text = dt1.Rows[0]["PRNAMT"].ToString();
                TxtPeriod.Text = dt1.Rows[0]["PERIOD"].ToString();
                ddlduration.SelectedValue = dt1.Rows[0]["PRDTYPE"].ToString();
                TxtRate.Text = dt1.Rows[0]["RATEOFINT"].ToString();
                TxtIntrest.Text = Convert.ToInt32(dt1.Rows[0]["INTAMT"]).ToString();
                TxtMaturity.Text = Convert.ToInt32(dt1.Rows[0]["MATURITYAMT"]).ToString();
                DtDueDate.Text = Convert.ToDateTime(dt1.Rows[0]["DUEDATE"].ToString()).ToString("dd/MM/yyyy");
                TxtProcode4.Text = dt1.Rows[0]["TRFSUBTYPE"].ToString();
                TxtProName4.Text = customcs.GetProductName(TxtProcode4.Text, Session["BRCD"].ToString());
                TxtAccNo4.Text = dt1.Rows[0]["TRFACCNO"].ToString();
                if (TxtAccNo4.Text == "0")
                {
                    TxtAccName4.Text = " ";
                }
                else
                {
                    TxtAccName4.Text = NM.GetAccName(TxtAccNo4.Text, TxtProcode4.Text, Session["BRCD"].ToString());
                }
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
            dt = CurrentCls.GetAllFieldData(TxtProcode.Text.ToString(), txtAccNo.Text.ToString(), Session["BRCD"].ToString(), "0");
            if (dt.Rows.Count > 0)
            {
                //Txtcustno.Text = dt.Rows[0]["CUSTNO"].ToString();
                TxtAccName.Text = dt.Rows[0]["CUSTNAME"].ToString();
                txtAccNo.Text = dt.Rows[0]["CUSTACCNO"].ToString();
                ddlAccType.SelectedValue = dt.Rows[0]["ACC_TYPE"].ToString();
                ddlOpType.SelectedValue = dt.Rows[0]["OPR_TYPE"].ToString();
                dtDeposDate.Text = Convert.ToDateTime(dt.Rows[0]["OPENINGDATE"].ToString()).ToString("dd/MM/yyyy");
                ddlIntrestPay.Text = dt.Rows[0]["INTPAYOUT"].ToString();
                TxtDepoAmt.Text = dt.Rows[0]["PRNAMT"].ToString();
                TxtPeriod.Text = dt.Rows[0]["PERIOD"].ToString();
                ddlduration.SelectedValue = dt.Rows[0]["PRDTYPE"].ToString();
                TxtRate.Text = dt.Rows[0]["RATEOFINT"].ToString();
                TxtIntrest.Text = Convert.ToInt32(dt.Rows[0]["INTAMT"]).ToString();
                TxtMaturity.Text = Convert.ToInt32(dt.Rows[0]["MATURITYAMT"]).ToString();
                DtDueDate.Text = Convert.ToDateTime(dt.Rows[0]["DUEDATE"].ToString()).ToString("dd/MM/yyyy");
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
            string AccNoTransf = "0";
            string ProCodeTransf = "0";
            string PCMAC = "";// System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.ToString();

            string STR = MT.GetDepositCat(Session["BRCD"].ToString(), TxtProcode.Text, "MISTRF");
            if (STR == "MIS" && (TxtProcode4.Text == "" || TxtAccNo4.Text == ""))
            {
                WebMsgBox.Show("Enter Transfer Account for MIS Deposit...!", this.Page);
                TxtProcode4.Text = "";
                TxtProcode4.Focus();

            }
            else
            {
                // To Deposit table
                if (ViewState["Flag"].ToString() == "AD")
                {
                    string exist = CurrentCls.EntryDepositeIsExists(Txtcustno.Text.ToString(), txtAccNo.Text.ToString(), TxtProcode.Text.ToString(), TxtDepoAmt.Text.ToString(), TxtRate.Text.ToString(), dtDeposDate.Text.ToString(), DtDueDate.Text.ToString(), TxtPeriod.Text.ToString(), TxtIntrest.Text.ToString(), TxtMaturity.Text.ToString(), "1001", Session["BRCD"].ToString(), Session["MID"].ToString(), PCMAC, ddlIntrestPay.SelectedValue.ToString(), ddlduration.SelectedValue, "1", TxtProcode4.Text, TxtAccNo4.Text, TxtReceiptNo.Text);
                    if (exist != "0")
                    {
                        WebMsgBox.Show("Deposit already exists...!", this.Page);
                        return;
                    }
                    int result = CurrentCls.EntryDeposite(Txtcustno.Text.ToString(), txtAccNo.Text.ToString(), TxtProcode.Text.ToString(), TxtDepoAmt.Text.ToString(), TxtRate.Text.ToString(), dtDeposDate.Text.ToString(), DtDueDate.Text.ToString(), TxtPeriod.Text.ToString(), TxtIntrest.Text.ToString(), TxtMaturity.Text.ToString(), "1001", Session["BRCD"].ToString(), Session["MID"].ToString(), PCMAC, ddlIntrestPay.SelectedValue.ToString(), ddlduration.SelectedValue, "1", TxtProcode4.Text, TxtAccNo4.Text, TxtReceiptNo.Text, Convert.ToString(Session["EntryDate"]));
                    GetSRNo = CurrentCls.GetSRNo(TxtProcode.Text.ToString(), txtAccNo.Text.ToString(), Convert.ToString(Session["BRCD"]));

                    if (result > 0)
                    {
                        if (ViewState["DRGL"].ToString() == "5")
                        {
                            Opr_TRFCR();

                        }
                        else
                        {
                            WebMsgBox.Show("Deposit Created Succesfully...!", this.Page);
                            FL = "Insert";//Dhanya Shetty
                            string Result = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Deposit_Add _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        }

                    }
                    else
                    {
                        WebMsgBox.Show("Could not be saved...", this.Page);
                    }

                }
                else if (ViewState["Flag"].ToString() == "MD")
                {
                    if (Session["UGRP"].ToString() == "1" || Session["UGRP"].ToString() == "2" || Session["UGRP"].ToString() == "3")//12/02/2017(only Ceo,admin,manager can modify)Dhanya Shetty
                    {
                        int result = CurrentCls.ModifyDeposit(Txtcustno.Text.ToString(), txtAccNo.Text.ToString(), TxtProcode.Text.ToString(), TxtDepoAmt.Text.ToString(), TxtRate.Text.ToString(), dtDeposDate.Text.ToString(), DtDueDate.Text.ToString(), TxtPeriod.Text.ToString(), TxtIntrest.Text.ToString(), TxtMaturity.Text.ToString(), "1001", Session["BRCD"].ToString(), Session["MID"].ToString(), PCMAC, ddlIntrestPay.SelectedValue.ToString(), ddlduration.SelectedValue, TxtProcode4.Text, TxtAccNo4.Text, TxtReceiptNo.Text, Convert.ToString(Session["EntryDate"]));
                        if (result > 0)
                        {
                            WebMsgBox.Show("Record Modify Successfully...", this.Page);
                            BindGridPro();
                            FL = "Insert";//Dhanya Shetty
                            string Result = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Deposit_Modify _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                        }
                        else
                        {
                            WebMsgBox.Show("Could not be saved...", this.Page);
                        }
                        clear();
                        TxtProcode.Text = "";
                        TxtProcode.Focus();
                    }
                    else
                    {
                        WebMsgBox.Show("User is restricted to Modify", this.Page);
                    }
                }
                else if (ViewState["Flag"].ToString() == "DL" || ViewState["Flag"].ToString() == "AT")
                {
                    string ST = ViewState["Flag"].ToString() == "DL" ? "1004" : "1003";
                    int RC = CurrentCls.DelAutho(TxtProcode.Text, txtAccNo.Text, Session["BRCD"].ToString(), ST, Session["MID"].ToString(), Convert.ToString(Session["EntryDate"]), "0");
                    if (ST == "1003")
                    {
                        lblMessage.Text = "Record Authorize Successfully..............";
                        ModalPopup.Show(this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Result = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Deposit_Authorized _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    }
                    else
                    {
                        lblMessage.Text = "Record Deleted Successfully..............";
                        ModalPopup.Show(this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Result = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Deposit_Delete _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                    }
                    clear();
                    TxtProcode.Text = "";
                    TxtProcode.Focus();
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
                //HttpContext.Current.Response.Redirect("FrmMultiVoucher.aspx?&FL=ACO&P=" + TxtProcode.Text + "&A=" + txtAccNo.Text + "", false);
                HttpContext.Current.Response.Redirect("FrmMultiTransaction.aspx?&FL=ACO&P=" + TxtProcode.Text + "&AMT=" + Convert.ToString(TxtDepoAmt.Text) + "&RecSRNo=" + GetSRNo + "&A=" + txtAccNo.Text + "", false);
                clear();
            }
            else
            {
                HttpContext.Current.Response.Redirect("FrmCashReceipt.aspx?op=AD&FL=ACO&P=" + TxtProcode.Text + "&A=" + txtAccNo.Text + "", false);
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
            if (Convert.ToInt32(IsPvalid) > 0)
            {
            }
            else
            {
                if (ViewState["DRGL"].ToString() == "5")
                {
                    WebMsgBox.Show("Invalid Period in " + ddlduration.SelectedItem.Text + " - " + TxtPeriod.Text + " for " + TxtProName.Text + " ...", this.Page);
                }
                else if (ViewState["DRGL"].ToString() == "15")
                {
                    WebMsgBox.Show("Invalid Period in " + ddlduration.SelectedItem.Text + " - " + TxtPeriod.Text + " for GLCODE " + ViewState["DRGL"].ToString() + " ...", this.Page);
                }
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
                rate = Convert.ToDouble(CurrentCls.GetInterestRateED("TDA", "", TxtProcode.Text, CUSTTYPE.ToString(), ddlduration.SelectedValue.ToString(), TxtPeriod.Text.ToString(), dtDeposDate.Text, true));
            //rate = CurrentCls.GetIntrestRateADD(TxtProcode.Text.ToString(), TxtPeriod.Text.ToString(), Session["BRCD"].ToString(), ddlduration.SelectedValue.ToString(), false, CUSTTYPE,dtDeposDate.Text);
            else
                rate = Convert.ToDouble(CurrentCls.GetInterestRateED("TDA", "", ViewState["DRGL"].ToString(), CUSTTYPE.ToString(), ddlduration.SelectedValue.ToString(), TxtPeriod.Text.ToString(), dtDeposDate.Text, true));
            //rate = CurrentCls.GetIntrestRateADD(ViewState["DRGL"].ToString(), TxtPeriod.Text.ToString(), Session["BRCD"].ToString(), ddlduration.SelectedValue.ToString(), false, CUSTTYPE, dtDeposDate.Text);

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

            // Calculate Due date
            //string currdt = dtDeposDate.Text.ToString();
            //DateTime DUEDT = Convert.ToDateTime("01/01/1900");
            //DateTime currdt = CMN.ConvertDate(dtDeposDate.Text.ToString());
            //DateTime currdt = InsertDate(dtDeposDate.Text);
            if (ddlduration.SelectedValue == "M")
            {
                // DUEDT = currdt.AddMonths(Convert.ToInt32(TxtPeriod.Text));
                //DtDueDate.Text = DUEDT.ToString("dd/MM/yyyy");
                //txtDueDate.Text = DUEDT.ToString("dd/MM/yyyy");
                DtDueDate.Text = conn.AddMonthDay(dtDeposDate.Text, TxtPeriod.Text, "M").Replace("12:00:00", "");
                //txtDueDate.Text = conn.AddMonthDay(dtDeposDate.Text, TxtPeriod.Text, "M").Replace("12:00:00", "");
            }
            else if (ddlduration.SelectedValue == "D")
            {
                //DUEDT = currdt.AddDays(Convert.ToInt32(TxtPeriod.Text));
                //DtDueDate.Text = DUEDT.ToString("dd/MM/yyyy");
                //txtDueDate.Text = DUEDT.ToString("dd/MM/yyyy");
                DtDueDate.Text = conn.AddMonthDay(dtDeposDate.Text, TxtPeriod.Text, "D").Replace("12:00:00", "");
                // txtDueDate.Text = conn.AddMonthDay(dtDeposDate.Text, TxtPeriod.Text, "D").Replace("12:00:00", "");
            }
            //CalDueDate(Convert.ToDateTime(dtDeposDate.Text.ToString()), ddlduration.SelectedItem.Text.ToString(), Convert.ToInt32(TxtPeriod.Text));

            // Calculate Interest 
            float amt = (float)Convert.ToDouble(TxtDepoAmt.Text);
            float intrate = (float)Convert.ToDouble(TxtRate.Text);
            CalculatedepositINT(amt, TxtProcode.Text.ToString(), intrate, Convert.ToInt32(TxtPeriod.Text), ddlIntrestPay.SelectedItem.Text.ToString(), ddlduration.SelectedItem.Text.ToString());
            Rdb_Trf.Focus();

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
    protected void CalculatedepositINT(float amt, string subgl, float intrate, int Period, string intpay, string PTYPE)
    {
        try
        {
            float interest = 0;
            float maturityamt = 0;
            float QUATERS = 0;
            float tmp1 = 0;
            float tmp2 = 0;
            string category;
            //string sql = "select GLGROUP from glmast where glcode=5 AND SUBGLCODE='"+subgl+"'";
            //string sql = "SELECT TOP 1 CATEGORY FROM DEPOSITGL where DEPOSITGLCODE='" + subgl + "'";// AND BRCD='" + Session["BRCD"].ToString() + "'";
            //string category = conn.sExecuteScalar(sql);

            category = CurrentCls1.GetTDACategory(Session["BRCD"].ToString(), TxtProcode.Text);

            if (category == "")
            {
                return;
            }

            switch (category)
            {
                case "PLKP":
                    DT = CurrentCls.TDACase("PLKP", Session["EntryDate"].ToString(), TxtPeriod.Text, TxtDepoAmt.Text, TxtProcode.Text, ddlduration.SelectedValue);
                    if (DT.Rows.Count > 0)
                    {
                        //MatAmt	IntAmt
                        TxtMaturity.Text = DT.Rows[0]["MatAmt"].ToString();
                        TxtIntrest.Text = DT.Rows[0]["IntAmt"].ToString();

                    }
                    break;
                case "MISLT":
                    DT = CurrentCls.TDACase("MISLT", Session["EntryDate"].ToString(), TxtPeriod.Text, TxtDepoAmt.Text, TxtProcode.Text, ddlduration.SelectedValue);
                    if (DT.Rows.Count > 0)
                    {
                        //MatAmt	IntAmt
                        TxtMaturity.Text = DT.Rows[0]["MatAmt"].ToString();
                        TxtIntrest.Text = DT.Rows[0]["IntAmt"].ToString();

                    }
                    break;

                case "LKP":

                    DT = CurrentCls.GetLKPData(TxtProcode.Text, Period.ToString(), ddlduration.SelectedValue.ToString(), TxtDepoAmt.Text, Session["EntryDate"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        TxtMaturity.Text = DT.Rows[0]["MATAMT"].ToString();
                        //double TOTPRI = Convert.ToDouble(TxtDepoAmt.Text) * Convert.ToDouble(TxtPeriod.Text);
                        //double INTAMT = Convert.ToDouble(TxtMaturity.Text) - TOTPRI;  // Changes on 24-01-2018 Interest amount wrong calculation Abhishek
                        double INTAMT = Convert.ToDouble(TxtMaturity.Text) - (Convert.ToDouble(TxtDepoAmt.Text) * Convert.ToDouble(TxtPeriod.Text));
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
                        //double TOTPRI = Convert.ToDouble(TxtDepoAmt.Text) * Convert.ToDouble(TxtPeriod.Text);
                        //double INTAMT = Convert.ToDouble(TxtMaturity.Text) - TOTPRI;  // Changes on 24-01-2018 Interest amount wrong calculation Abhishek
                        double INTAMT = Convert.ToDouble(TxtMaturity.Text) - (Convert.ToDouble(TxtDepoAmt.Text) * Convert.ToDouble(TxtPeriod.Text));
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
                case "DD":
                    interest = amt;
                    maturityamt = interest + amt;
                    TxtIntrest.Text = interest.ToString("N");
                    TxtMaturity.Text = maturityamt.ToString("N");
                    TxtReceiptNo.Focus();
                    break;

                case "MIS":
                    interest = Convert.ToInt32(amt * intrate / (1200 + intrate));
                    // maturityamt = interest + amt;
                    maturityamt = amt;
                    TxtIntrest.Text = interest.ToString("N");
                    TxtMaturity.Text = maturityamt.ToString("N");
                    TxtReceiptNo.Focus();
                    break;

                case "CUM":
                    float PrmAmt = amt;
                    string CALCTYPE = CurrentCls.GetCUMCal(Session["BRCD"].ToString(), TxtProcode.Text, "CUM");

                    if (CALCTYPE == "Y" || CALCTYPE == "y")
                    {
                        QUATERS = (Period / 12);
                        for (int i = 1; i <= QUATERS; i++)
                        {

                            interest = amt * intrate / 100;
                            maturityamt = amt + interest;
                            amt = maturityamt;

                        }

                    }
                    else if (CALCTYPE == "H" || CALCTYPE == "h")
                    {
                        QUATERS = (Period / 6);
                        for (int i = 1; i <= QUATERS; i++)
                        {

                            interest = amt * intrate / 100 / 2;
                            maturityamt = amt + interest;
                            amt = maturityamt;

                        }


                    }
                    else if (CALCTYPE == "Q" || CALCTYPE == "q")
                    {
                        QUATERS = (Period / 3);
                        for (int i = 1; i <= QUATERS; i++)
                        {

                            interest = amt * intrate / 100 / 4;
                            maturityamt = amt + interest;
                            amt = maturityamt;

                        }
                    }
                    else if (CALCTYPE == "M" || CALCTYPE == "m")
                    {
                        QUATERS = (Period / 1);
                        for (int i = 1; i <= QUATERS; i++)
                        {

                            interest = amt * intrate / 100 / 12;
                            maturityamt = amt + interest;
                            amt = maturityamt;

                        }
                    }
                    //else if (CALCTYPE == "D")
                    //{
                    //    QUATERS = (Period / 3);
                    //}
                    else
                    {
                        QUATERS = (Period / 3);
                        for (int i = 1; i <= QUATERS; i++)
                        {

                            interest = amt * intrate / 100 / 4;
                            maturityamt = amt + interest;
                            amt = maturityamt;

                        }
                    }


                    //maturityamt = Convert.ToInt32((Math.Pow(((intrate / 400) + 1), (QUATERS))) * amt);
                    interest = maturityamt - PrmAmt;
                    TxtIntrest.Text = Math.Round(interest, 0).ToString("N");
                    TxtMaturity.Text = Math.Round(maturityamt, 0).ToString("N");

                    TxtReceiptNo.Focus();
                    break;

                case "FDS":
                    if (intpay == "On Maturity" || intpay == "ON MATURITY")
                    {
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
                    }
                    else if (intpay == "Quaterly" || intpay == "QUATERLY")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (3));
                        maturityamt = interest + amt;
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                        TxtReceiptNo.Focus();
                    }
                    else if (intpay == "Half Yearly" || intpay == "HALF YEARLY")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (6));
                        maturityamt = interest + amt;
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                        TxtReceiptNo.Focus();
                    }

                    else if (intpay == "Monthly" || intpay == "MONTHLY" || PTYPE == "maihano")
                    {
                        interest = Convert.ToInt32((amt) * (intrate) / 1200 + intrate);
                        maturityamt = interest + amt;
                        //maturityamt = interest + amt;
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                        TxtReceiptNo.Focus();
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




                case "RD":
                    DataTable DT1 = CurrentCls.GetRDCalc("RD", amt.ToString(), ddlIntrestPay.SelectedValue.ToString(), ddlduration.SelectedValue.ToString(), TxtPeriod.Text, intrate.ToString());
                    if (DT1 != null)
                    {
                        string intamt = DT1.Rows[0]["Interest"].ToString();
                        string MatAmt = DT1.Rows[0]["Maturity"].ToString();

                        TxtIntrest.Text = Convert.ToDouble(intamt).ToString("N");
                        TxtMaturity.Text = Convert.ToDouble(MatAmt).ToString("N");

                    }
                    else
                    {
                        WebMsgBox.Show("Calculation failed...!", this.Page);
                    }




                    //float deamt = amt;
                    //float tempamt = amt;
                    //string SQL = "";
                    //float temprate = intrate;
                    //conn.sExecuteQuery("delete from RRD");

                    //string RDPARA = conn.sExecuteScalar("SELECT LISTVALUE FROM PARAMETER WHERE LISTFIELD='RDINT'");
                    ////float RDPARA = Convert.ToInt32(RDPARAstring);
                    //if (ViewState["DRGL"].ToString() == "15")
                    //{
                    //    RDPARA = "H";
                    //}
                    //if (RDPARA == "")
                    //{
                    //    WebMsgBox.Show("Create parameter RDINT AND VALUE Monthly(M) or Quaterly(Q)", this.Page);
                    //    return;
                    //}
                    //else
                    //{
                    //    if (RDPARA == "M")
                    //    {
                    //        int i = 1;
                    //        int srno = 1;
                    //        float deamt1 = deamt;
                    //        for (i = 1; i <= (Period / 1); i++)
                    //        {
                    //            float int1 = (deamt1 * temprate) / 1200;
                    //            SQL = "insert into RRD(srno,Install,Balance,interest) values(' " + srno + " ' , ' " + deamt + " ' , ' " + deamt1 + " ' , ' " + int1 + " ' )";
                    //            conn.sExecuteQuery(SQL);
                    //            srno = srno + 1;
                    //            deamt1 = deamt1 + deamt;
                    //            float int2 = (deamt1 * temprate) / 1200;
                    //        }
                    //        SQL = "select sum(interest) from RRD";
                    //        string interest1 = conn.sExecuteScalar(SQL);
                    //        double MAMT = Convert.ToDouble(deamt * Period) + Convert.ToDouble(interest1);
                    //        //maturityamt = (deamt * Period) +interest1F;
                    //        //TxtIntrest.Text = interest1.ToString();
                    //        tmp1 = (float)Convert.ToDouble(interest1);
                    //        TxtIntrest.Text = tmp1.ToString("N");
                    //        tmp2 = (float)Convert.ToDouble(MAMT);
                    //        TxtMaturity.Text = tmp2.ToString("N");
                    //    }
                    //    else if (RDPARA == "Q")
                    //    {
                    //        int i = 1;
                    //        int srno = 1;
                    //        float deamt1 = deamt;

                    //        for (i = 1; i <= (Period / 3); i++)
                    //        {
                    //            float int1 = (deamt1 * temprate) / 1200;

                    //            SQL = "insert into RRD(srno,Install,Balance,interest) values('" + srno + "' , '" + deamt + "' , '" + deamt1 + "' , '" + int1 + "')";
                    //            conn.sExecuteQuery(SQL);
                    //            srno = srno + 1;
                    //            deamt1 = deamt1 + deamt;
                    //            float int2 = (deamt1 * temprate) / 1200;

                    //            SQL = "insert into RRD(srno,Install,Balance,interest) values('" + srno + "' , '" + deamt + "' , '" + deamt1 + "' , '" + int2 + "' )";
                    //            conn.sExecuteQuery(SQL);
                    //            srno = srno + 1;

                    //            deamt1 = deamt1 + deamt;
                    //            float int3 = (deamt1 * temprate) / 1200;

                    //            SQL = "insert into RRD(srno,Install,Balance,interest) values('" + srno + "' , '" + deamt + "' , '" + deamt1 + "' , '" + int3 + "' )";
                    //            conn.sExecuteReader(SQL);
                    //            srno = srno + 1;

                    //            float TOTINT = int1 + int2 + int3;
                    //            deamt1 = deamt1 + deamt + TOTINT;
                    //            float int4 = (deamt1 * temprate) / 1200;
                    //        }
                    //        SQL = "select sum(interest) from RRD";
                    //        interest = Convert.ToInt32(conn.sExecuteScalar(SQL));
                    //        maturityamt = (deamt * Period) + interest;
                    //        TxtIntrest.Text = interest.ToString("N");
                    //        TxtMaturity.Text = maturityamt.ToString("N");
                    //    }
                    //    else if (RDPARA == "H")
                    //    {
                    //        int i = 1;
                    //        int srno = 1;
                    //        float deamt1 = deamt;

                    //        for (i = 1; i <= (Period / 6); i++)
                    //        {
                    //            float int1 = (deamt1 * temprate) / 1200;

                    //            SQL = "insert into RRD(srno,Install,Balance,interest) values('" + srno + "' , '" + deamt + "' , '" + deamt1 + "' , '" + int1 + "')";
                    //            conn.sExecuteQuery(SQL);
                    //            srno = srno + 1;
                    //            deamt1 = deamt1 + deamt;
                    //            float int2 = (deamt1 * temprate) / 1200;

                    //            SQL = "insert into RRD(srno,Install,Balance,interest) values('" + srno + "' , '" + deamt + "' , '" + deamt1 + "' , '" + int2 + "' )";
                    //            conn.sExecuteQuery(SQL);
                    //            srno = srno + 1;
                    //            deamt1 = deamt1 + deamt;
                    //            float int3 = (deamt1 * temprate) / 1200;


                    //            SQL = "insert into RRD(srno,Install,Balance,interest) values('" + srno + "' , '" + deamt + "' , '" + deamt1 + "' , '" + int3 + "' )";
                    //            conn.sExecuteReader(SQL);
                    //            srno = srno + 1;
                    //            deamt1 = deamt1 + deamt;
                    //            float int4 = (deamt1 * temprate) / 1200;


                    //            SQL = "insert into RRD(srno,Install,Balance,interest) values('" + srno + "' , '" + deamt + "' , '" + deamt1 + "' , '" + int4 + "' )";
                    //            conn.sExecuteReader(SQL);
                    //            srno = srno + 1;
                    //            deamt1 = deamt1 + deamt;
                    //            float int5 = (deamt1 * temprate) / 1200;


                    //            SQL = "insert into RRD(srno,Install,Balance,interest) values('" + srno + "' , '" + deamt + "' , '" + deamt1 + "' , '" + int5 + "' )";
                    //            conn.sExecuteReader(SQL);
                    //            srno = srno + 1;
                    //            deamt1 = deamt1 + deamt;
                    //            float int6 = (deamt1 * temprate) / 1200;

                    //            SQL = "insert into RRD(srno,Install,Balance,interest) values('" + srno + "' , '" + deamt + "' , '" + deamt1 + "' , '" + int6 + "' )";
                    //            conn.sExecuteReader(SQL);
                    //            srno = srno + 1;

                    //            float TOTINT = int1 + int2 + int3 + int4 + int5 + int6;
                    //            deamt1 = deamt1 + deamt + TOTINT;
                    //            float int7 = (deamt1 * temprate) / 1200;


                    //        }
                    //        SQL = "select sum(interest) from RRD";
                    //        string INTR = conn.sExecuteScalar(SQL);
                    //        float INTRTemp = (float)Convert.ToDouble(INTR);
                    //        maturityamt = (deamt * Period) + INTRTemp;
                    //        double T1 = Math.Round(maturityamt, 0);
                    //        double T2 = Math.Round(INTRTemp, 0);
                    //        TxtIntrest.Text = T2.ToString();
                    //        TxtMaturity.Text = T1.ToString();
                    //        TxtReceiptNo.Focus();

                    //    }

                    //}
                    break;
                case "FDSS":
                    if (intpay == "On Maturity" || intpay == "ON MATURITY")
                    {
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
                    }
                    else if (intpay == "Quaterly" || intpay == "QUATERLY")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (3));
                        maturityamt = interest + amt;
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                        TxtReceiptNo.Focus();
                    }
                    else if (intpay == "Half Yearly" || intpay == "HALF YEARLY")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (6));
                        maturityamt = interest + amt;
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                        TxtReceiptNo.Focus();
                    }

                    else if (intpay == "Monthly" || intpay == "MONTHLY" || PTYPE == "maihano")
                    {
                        interest = Convert.ToInt32((amt) * (intrate) / 1200 + intrate);
                        maturityamt = interest + amt;
                        //maturityamt = interest + amt;
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                        TxtReceiptNo.Focus();
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

    public void clear()
    {
        TxtProName.Text = "";
        TxtProcode.Text = "";
        txtAccNo.Text = "";
        Txtcustno.Text = "";
        TxtAccName.Text = "";
        //ddlAccType.SelectedIndex = 0;
        // ddlOpType.SelectedIndex = 0;
        TxtDepoAmt.Text = "";
        // ddlIntrestPay.SelectedIndex = 0;
        // ddlduration.SelectedIndex = 0;
        TxtProcode4.Text = "";
        TxtProName4.Text = "";
        TxtAccName4.Text = "";
        TxtAccNo4.Text = "";
        TxtPeriod.Text = "";
        TxtRate.Text = "";
        TxtIntrest.Text = "";
        TxtMaturity.Text = "";
        DtDueDate.Text = "";
        TxtReceiptNo.Text = "";
        TxtProcode.Focus();
    }




    // Deposit amount changed
    protected void TxtDepoAmt_TextChanged(object sender, EventArgs e)
    {
        //txtDeposit22.Text = TxtDepoAmt.Text.ToString();
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

    // Get Customer Number and customer name for transfer







    protected void BtnSubmit4_Click(object sender, EventArgs e)
    {
        try
        {
            //CurrentCls.EntryTransfer(TxtProcode.Text.ToString, txtAccNo.Text.ToString(), TxtProcode4.Text.ToString(), TxtAccNo4.Text.ToString());
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
            if (ViewState["Flag"].ToString() != "AD")
            {

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
            int Result = 0;
            Result = NM.BindData(grdMaster, Session["BRCD"].ToString());
            if (Result > 0)
            {
            }
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
            int Result = 0;
            Result = NM.BindDataPro(grdMaster, Session["BRCD"].ToString(), TxtSPrdCode.Text, TxtSAccno.Text);
            if (Result > 0)
            {
            }
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
            string custno = TxtProName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TxtProName.Text = CT[0].ToString();
                TxtProcode.Text = CT[1].ToString();
                //TxtGLCD.Text = CT[2].ToString();
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
                ddlIntrestPay.Focus();
                FillData();
            }
            else
            {
                lblMessage.Text = "Invalid Account Number.........!!";
                ModalPopup.Show(this.Page);
                return;
            }
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

            if (TxtProcode4.Text == "")
            {
                TxtProcode4.Focus();
                TxtProcode4.Text = "";
            }
            int result = 0;
            string GlS1;
            int.TryParse(TxtProcode4.Text, out result);
            TxtProName4.Text = customcs.GetProductName(result.ToString(), Session["BRCD"].ToString());
            GlS1 = BD.GetAccTypeGL(TxtProcode4.Text, Session["BRCD"].ToString());
            if (GlS1 != null)
            {
                string[] GLS = GlS1.Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                Autoaccname4.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode4.Text + "_" + ViewState["DRGL"].ToString();
                int GL = 0;
                int.TryParse(ViewState["DRGL"].ToString(), out GL);
                TxtAccNo4.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter Valid Product code!....", this.Page);
                TxtProcode4.Text = "";
                TxtProName4.Text = "";
                TxtProcode4.Focus();
            }
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
            string custno = TxtProName4.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TxtProName4.Text = CT[0].ToString();
                TxtProcode4.Text = CT[1].ToString();
                //TxtGLCD.Text = CT[2].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtProcode4.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["TGL"] = GLS[1].ToString();
                Autoaccname4.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode4.Text + "_" + ViewState["TGL"].ToString();

                int GL = 0;
                int.TryParse(ViewState["TGL"].ToString(), out GL);

                if (TxtProName4.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    TxtProcode4.Text = "";
                    TxtProcode4.Focus();

                }
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
            dt = CMN.GetCustNoNameGL1(TxtProcode4.Text.ToString(), TxtAccNo4.Text.ToString(), Session["BRCD"].ToString());

            if (dt.Rows.Count > 0)
            {
                //Txtcustno4.Text = dt.Rows[0][0].ToString();
                TxtAccName4.Text = dt.Rows[0][3].ToString();
            }
            else
            {
                WebMsgBox.Show("Account Number not found", this.Page);
                TxtAccNo4.Text = "";
                TxtAccName4.Text = "";
                TxtAccNo4.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAccName4_TextChanged(object sender, EventArgs e)
    {
        string CUNAME = TxtAccName4.Text;
        string[] custnob = CUNAME.Split('_');
        if (custnob.Length > 1)
        {
            TxtAccName4.Text = custnob[0].ToString();
            TxtAccNo4.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
        }
        else
        {
            lblMessage.Text = "Invalid Account Number.........!!";
            ModalPopup.Show(this.Page);
            return;
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
            LtrlHeading.Text = " Create Deposit";
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
        {
            lblMessage.Text = "please Enter Correct Account No";
            ModalPopup.Show(this.Page);
        }
        else
        {
            TxtSaccName.Text = accname;
        }
    }
    protected void TxtSPrdCode_TextChanged(object sender, EventArgs e)
    {

        string Pname = NM.GetProductName(TxtSPrdCode.Text, Session["BRCD"].ToString());
        if (Pname == null || Pname == "")
        {
            lblMessage.Text = "please Enter Correct Product No";
            ModalPopup.Show(this.Page);
        }
        else
        {
            TXtSPrdname.Text = Pname;
            TxtSAccno.Focus();
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
            ViewState["Flag"] = "MD";
            if (Session["UGRP"].ToString() == "1" || Session["UGRP"].ToString() == "2" || Session["UGRP"].ToString() == "3")//12/02/2017(only Ceo,admin,manager can modify)Dhanya Shetty
            {
                DIV_Term.Visible = true;
                BtnSubmit.Text = "Modify";
                LtrlHeading.Text = "Deposit Modify";
                Search.Visible = false;
                Main.Visible = false;
                getalldata();
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
            ViewState["Flag"] = "DL";
            DIV_Term.Visible = true;
            BtnSubmit.Text = "Delete";
            Main.Visible = false;
            Search.Visible = false;
            LtrlHeading.Text = "Deposit Delete";

            getalldata();
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
            ViewState["Flag"] = "AT";
            DIV_Term.Visible = true;
            BtnSubmit.Text = "Authorise";
            Search.Visible = false;
            Main.Visible = false;
            LtrlHeading.Text = "Deposit Authorize";
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
            dt1 = CurrentCls.GetAllFieldData(ViewState["Depositglcode"].ToString(), ViewState["CustAccno"].ToString(), Session["BRCD"].ToString(), "0");
            if (dt1.Rows.Count > 0)
            {
                string STAGE = "";
                STAGE = dt1.Rows[0]["STAGE"].ToString();
                if ((ViewState["Flag"].ToString() == "MD" || ViewState["Flag"].ToString() == "DL") && Session["UGRP"].ToString() != "1")
                {
                    if (STAGE == "1003")
                    {
                        clear();
                        lblMessage.Text = "Record Authorized cannot Modify.......!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                    else if (STAGE == "1004")
                    {

                        clear();
                        lblMessage.Text = "Record Not Present.......!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                }
                TxtProName.Text = NM.GetProductName(ViewState["Depositglcode"].ToString(), Session["BRCD"].ToString());
                TxtProcode.Text = ViewState["Depositglcode"].ToString();
                TxtReceiptNo.Text = dt1.Rows[0]["RECEIPT_NO"].ToString();
                Txtcustno.Text = dt1.Rows[0]["CUSTNO"].ToString();
                TxtAccName.Text = dt1.Rows[0]["CUSTNAME"].ToString();
                txtAccNo.Text = dt1.Rows[0]["CUSTACCNO"].ToString();
                ddlAccType.SelectedValue = dt1.Rows[0]["ACC_TYPE"].ToString();
                ddlOpType.SelectedValue = dt1.Rows[0]["OPR_TYPE"].ToString();
                dtDeposDate.Text = Convert.ToDateTime(dt1.Rows[0]["OPENINGDATE"].ToString()).ToString("dd/MM/yyyy");
                ddlIntrestPay.Text = dt1.Rows[0]["INTPAYOUT"].ToString();
                TxtDepoAmt.Text = dt1.Rows[0]["PRNAMT"].ToString();
                TxtPeriod.Text = dt1.Rows[0]["PERIOD"].ToString();
                ddlduration.SelectedValue = dt1.Rows[0]["PRDTYPE"].ToString();
                TxtRate.Text = dt1.Rows[0]["RATEOFINT"].ToString();
                TxtIntrest.Text = Convert.ToInt32(dt1.Rows[0]["INTAMT"]).ToString();
                TxtMaturity.Text = Convert.ToInt32(dt1.Rows[0]["MATURITYAMT"]).ToString();
                DtDueDate.Text = Convert.ToDateTime(dt1.Rows[0]["DUEDATE"].ToString()).ToString("dd/MM/yyyy");
                TxtProcode4.Text = dt1.Rows[0]["TRFSUBTYPE"].ToString();
                TxtProName4.Text = customcs.GetProductName(TxtProcode4.Text, Session["BRCD"].ToString());
                TxtAccNo4.Text = dt1.Rows[0]["TRFACCNO"].ToString();
                TxtAccName4.Text = NM.GetAccName(TxtAccNo4.Text, TxtProcode4.Text, Session["BRCD"].ToString());


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
            {
                Btn_Redirect.Text = "Redirect to Trf";
            }
            else
            {
                Btn_Redirect.Text = "Redirect to CR";
            }

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
}