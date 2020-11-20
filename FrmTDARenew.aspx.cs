using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmTDARenew : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsMISTransfer MT = new ClsMISTransfer();
    ClsOpenClose OC = new ClsOpenClose();
    scustom SC = new scustom();
    ClsFDARenew FDR = new ClsFDARenew();
    DbConnection conn = new DbConnection();
    ClsCommon CMN = new ClsCommon();
    ClsNewTDA CurrentCls = new ClsNewTDA();
    int Result = 0;
    bool Valid = true;

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {

            ViewState["MS"] = Request.QueryString["MS"].ToString();
            if (ViewState["MS"].ToString() == "N")
            {


                ViewState["PRD"] = Request.QueryString["PRD"].ToString();
                ViewState["ACC"] = Request.QueryString["FD"].ToString();
                ViewState["FDTP"] = Request.QueryString["FDP"].ToString();
                ViewState["TPAY"] = Request.QueryString["TPAMT"].ToString();
                ViewState["CT"] = Request.QueryString["CT"].ToString();
                ViewState["DAMT"] = Request.QueryString["DAMT"].ToString();
                ViewState["ACCTYPE"] = Request.QueryString["ACCT"].ToString();
                ViewState["INTPAY"] = Request.QueryString["INTPAY"].ToString();
                ViewState["IR"] = Request.QueryString["IR"].ToString();
                ViewState["OPRTYPE"] = Request.QueryString["OPRTYPE"].ToString();
                ViewState["REFID"] = Request.QueryString["REFID"].ToString();

                //Admin Fee
                ViewState["AFEE"] = Request.QueryString["ADFEE"].ToString();
                ViewState["ADSBGL"] = Request.QueryString["ADSBGL"].ToString();

                //Commision
                ViewState["COMMCHG"] = Request.QueryString["COMMCHG"].ToString();
                ViewState["COMMSBGL"] = Request.QueryString["COMMSBGL"].ToString();

                ViewState["GLC"] = Request.QueryString["GLC"].ToString();

                TxtACPayAmount.Text = ViewState["TPAY"].ToString();
                BD.BindIntrstPayout(ddlIntrestPay);
                BD.BindWithReceipt(ddlPayType, "1");
                // dtDeposDate.Text = Session["EntryDate"].ToString();
                AutoWRPRD.ContextKey = Session["BRCD"].ToString();
                Autoprd4.ContextKey = Session["BRCD"].ToString();
                BD.BindDepositGL(ddlProductType, "5", Session["BRCD"].ToString());
                int RC = FDR.CreateTable("X_" + Session["MID"].ToString().Trim(), Session["BRCD"].ToString());


                double PrnAmt = Convert.ToDouble(ViewState["TPAY"].ToString()) - Convert.ToDouble(ViewState["INTPAY"].ToString());
                RC = FDR.InsertTemp(ViewState["GLC"].ToString(), ViewState["FDTP"].ToString(), ViewState["ACC"].ToString(), PrnAmt.ToString(), "2", ViewState["CT"].ToString(), "X_" + Session["MID"].ToString(), "7", "TR");
                RC = FDR.InsertTemp(ViewState["GLC"].ToString(), ViewState["FDTP"].ToString(), ViewState["ACC"].ToString(), PrnAmt.ToString(), "2", ViewState["CT"].ToString(), "X_" + Session["MID"].ToString(), "7", "TR");
                //Result = FDR.InsertTempNew("10", ViewState["IR"].ToString(), "9999", ViewState["INTPAY"].ToString(), "1", ViewState["CT"].ToString(), Session["UserName"].ToString(), TxtRate.Text, ddlduration.SelectedValue, TxtPeriod.Text, TxtIntrest.Text, DtDueDate.Text, TxtMaturity.Text, TxtProcode4.Text, TxtAccNo4.Text);
                if (ViewState["INTPAY"].ToString() != "0")
                {
                    RC = FDR.InsertTemp("10", ViewState["IR"].ToString(), ViewState["ACC"].ToString(), ViewState["INTPAY"].ToString(), "2", ViewState["CT"].ToString(), "X_" + Session["MID"].ToString(), "7", "TR");
                }
                
                if (ViewState["AFEE"].ToString() != "0")
                {
                    RC = FDR.InsertTemp("100", ViewState["ADSBGL"].ToString(), ViewState["ACC"].ToString(), ViewState["AFEE"].ToString(), "1", ViewState["CT"].ToString(), "X_" + Session["MID"].ToString());
                    RC = FDR.InsertTemp(ViewState["GLC"].ToString(), ViewState["FDTP"].ToString(), ViewState["ACC"].ToString(), ViewState["AFEE"].ToString(), "2", ViewState["CT"].ToString(), "X_" + Session["MID"].ToString());
                    if (ViewState["COMMCHG"].ToString() != "0")
                    {
                        RC = FDR.InsertTemp("100", ViewState["COMMSBGL"].ToString(), ViewState["ACC"].ToString(), ViewState["COMMCHG"].ToString(), "1", ViewState["CT"].ToString(), "X_" + Session["MID"].ToString());
                        RC = FDR.InsertTemp(ViewState["GLC"].ToString(), ViewState["FDTP"].ToString(), ViewState["ACC"].ToString(), ViewState["COMMCHG"].ToString(), "2", ViewState["CT"].ToString(), "X_" + Session["MID"].ToString());
                    }

                }
                BindGrid();
            }
            else
            {
                ViewState["TPAY"] = Request.QueryString["TPAMT"].ToString();
                ViewState["ACCTYPE"] = Request.QueryString["ACCT"].ToString();
                ViewState["CT"] = Request.QueryString["CT"].ToString();
                ViewState["MSETNO"] = Request.QueryString["SNO"].ToString();
                ViewState["OPRTYPE"] = Request.QueryString["OPR"].ToString();
                TxtACPayAmount.Text = ViewState["TPAY"].ToString();

                BD.BindIntrstPayout(ddlIntrestPay);
                BD.BindWithReceipt(ddlPayType, "1");
                // dtDeposDate.Text = Session["EntryDate"].ToString();
                AutoWRPRD.ContextKey = Session["BRCD"].ToString();
                Autoprd4.ContextKey = Session["BRCD"].ToString();
                BD.BindDepositGL(ddlProductType, "5", Session["BRCD"].ToString());
                int RC = FDR.CreateTable("X_" + Session["MID"].ToString().Trim(), Session["BRCD"].ToString());

                RC = FDR.InsertTempMultiple("IMULTIPLEDR", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["MSETNO"].ToString(), "X_" + Session["MID"].ToString());  



            }
           
        }
    }
    public void BindGrid()
    {
        FDR.BindGrid(GrdFDLedger, "X_"+Session["MID"].ToString());
    }
    protected void TxtProName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //string GL = TxtProName.Text;
            //string[] GLS = GL.Split('_');
            //if (GLS.Length > 1)
            //{
            //TxtProName.Text = GLS[0].ToString();
            TxtProcode.Text = ddlProductType.SelectedValue.ToString();
            string[] GLT = BD.GetTDAAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString(), "5").Split('_');
            ViewState["GL"] = GLT[1].ToString();
            ViewState["IR"] = GLT[2].ToString();
            //}
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
            string GL=BD.GetTDAAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString(), "5");
            if (GL != null)
            {
                string[] GLT = GL.Split('_');
                ViewState["GL"] = GLT[1].ToString();
                ViewState["IR"] = GLT[2].ToString();
                ddlProductType.SelectedValue = TxtProcode.Text;

                dtDeposDate.Focus();
            }
            else
            {
                WebMsgBox.Show("Invalid Product code.....!", this.Page);
                TxtProcode.Text = "";
                TxtProcode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtWRPRCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] GLT = BD.GetTDAAccTypeGL(TxtWRPRCD.Text, Session["BRCD"].ToString(), "0").Split('_');
            ViewState["DRGL"] = GLT[1].ToString();
            TxtWRPRName.Text = GLT[0].ToString();
            AutoWRAccName.ContextKey = Session["BRCD"].ToString() + "_" + TxtWRPRCD.Text + "_" + ViewState["DRGL"].ToString();
            if (GLT[1] != null || GLT[1] != "")
            {
                string res = BD.GetGLGroup(TxtWRPRCD.Text, Session["BRCD"].ToString(), "0");
                if (res == "CBB" || res == "CAS")
                {
                    TxtWRAccNo.Text = TxtWRPRCD.Text;
                    TxtWRAccName.Text = TxtWRPRName.Text;
                }
                else
                {
                    TxtWRAccNo.Focus();
                    TxtWRAccNo.Text = "";
                    TxtWRAccName.Text = "";
                }
            }
            if (TxtWRPRName.Text == "")
            {
                WebMsgBox.Show("Invalid product code", this.Page);
                ClearData();
                TxtProcode.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtWRPRName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = TxtWRPRName.Text;
            string[] GLS = GL.Split('_');
            if (GLS.Length > 1)
            {
                TxtWRPRName.Text = GLS[0].ToString();
                TxtWRPRCD.Text = GLS[1].ToString();
                ViewState["DRGL"] = GLS[2].ToString();
                AutoWRAccName.ContextKey = Session["BRCD"].ToString() + "_" + TxtWRPRCD.Text + "_" + ViewState["DRGL"].ToString();
                if (GLS[2] != null || GLS[2] != "")
                {
                    string res = BD.GetGLGroup(TxtWRPRCD.Text, Session["BRCD"].ToString(), "0");
                    if (res == "CBB" || res == "CAS")
                    {
                        TxtWRAccNo.Text = TxtWRPRCD.Text;
                        TxtWRAccName.Text = TxtWRPRName.Text;
                    }
                    else
                    {
                        TxtWRAccNo.Focus();
                        TxtWRAccNo.Text = "";
                        TxtWRAccName.Text = "";
                    }
                }
                // ViewState["DRGL"] = GLT[1].ToString();
                //ViewState["IR"] = GLT[2].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtWRAccNo_TextChanged(object sender, EventArgs e)
    {
        string[] TD = Session["EntryDate"].ToString().Split('/');
        TxtBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtWRPRCD.Text, TxtWRAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString()).ToString();
        if (TxtWRAccNo.Text == "")
        {
            TxtWRAccNo.Focus();
        }
        DataTable dt1 = new DataTable();
        if (TxtWRAccNo.Text != "" & TxtWRPRCD.Text != "")
        {
            string PRD = TxtWRPRCD.Text;
            string[] namecust;
            namecust = SC.GetAccountName(TxtWRAccNo.Text.ToString(), PRD, Session["BRCD"].ToString()).Split('_');
            if (namecust.Length > 0)
            {
                ViewState["CUSTNO"] = namecust[0].ToString();
                TxtWRAccName.Text = namecust[1].ToString();

                if (TxtWRAccName.Text == "" & TxtWRAccNo.Text != "")
                {
                    WebMsgBox.Show("Please enter valid Account number", this.Page);
                    TxtWRAccNo.Text = "";
                    TxtWRAccNo.Focus();
                    return;
                }
                TxtWRAMT.Focus();
            }
        }
    }
    protected void TxtWRAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtWRAccName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtWRAccName.Text = custnob[0].ToString();
                TxtWRAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                ViewState["CUSTNO"] = custnob[2].ToString();
                string[] TD = Session["EntryDate"].ToString().Split('/');
                TxtBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtWRPRCD.Text, TxtWRAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString()).ToString();
                //txtnaration1.Focus();
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
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }    
    protected void TxtINTAcc_TextChanged(object sender, EventArgs e)
    {
    }    
    protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPayType.SelectedValue == "1")
        {
            AMT.Visible = true;
            DIVWR.Visible = false;
            AMTDetail.Visible = true;
        }
        else if (ddlPayType.SelectedValue == "2")
        {
            AMT.Visible = true;
            DIVWR.Visible = true;
            AMTDetail.Visible = true;
        }
    }
    protected void TxtWRAMT_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToDouble(TxtACPayAmount.Text) <= Convert.ToDouble(TxtWRAMT.Text))
        {
            double CURR, PRIN, TAMT;
            CURR = PRIN = TAMT = 0;
            CURR = Convert.ToDouble(TxtWRAMT.Text);
            PRIN = Convert.ToDouble(ViewState["TPAY"].ToString());
            TAMT = CURR + PRIN;
            TxtDepoAmt.Text = TAMT.ToString();
        }
        else
        {
            WebMsgBox.Show("Invalid Amount....!", this.Page);
            TxtWRAMT.Text = "";
            TxtWRAMT.Focus();
        }

    }
    protected void GrdFDLedger_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }
    protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        TxtProcode.Text = ddlProductType.SelectedValue;
    }
    protected void SubmitR_Click(object sender, EventArgs e)
    {
        if (rdbOP.Checked == true)
        {

                Result = FDR.InsertTemp(ViewState["GLC"].ToString(), ViewState["FDTP"].ToString(), ViewState["ACC"].ToString(), TxtWRAMT.Text, "2", ViewState["CT"].ToString(), "X_" + Session["MID"].ToString(), "4", "CP","NO");
                Result = FDR.InsertTemp(ViewState["DRGL"].ToString(), TxtWRPRCD.Text, TxtWRAccNo.Text, TxtWRAMT.Text, "1", ViewState["CT"].ToString(), "X_" + Session["MID"].ToString(),"4","CP","PNO");
                if (Result > 0)
                {
                    WebMsgBox.Show("Succesfully submitted....!", this.Page);
                    
                }
                else
                {
                    WebMsgBox.Show("Opeartion Failed!", this.Page);
                }
            
        }
        else
        {
            if (ddlPayType.SelectedValue == "1")
            {
                string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());
                Result = FDR.InsertTemp("99", cgl, "0", TxtWRAMT.Text, "2", ViewState["CT"].ToString(), "X_" + Session["MID"].ToString());
            }
            else
            {
                Result = FDR.InsertTemp(ViewState["DRGL"].ToString(), TxtWRPRCD.Text, TxtWRAccNo.Text, TxtWRAMT.Text, "2", ViewState["CT"].ToString(), "X_" + Session["MID"].ToString());
            }
        }

        if (ViewState["MS"].ToString() == "Y")
        {
            ViewState["INTPAY"] = TxtWRAMT.Text;
        }

        ClearDataWR();
        TxtTotalAmount.Text = FDR.GetCurrentBalance("X_" + Session["MID"].ToString(), "Y").ToString();
        double PRIAMT = Convert.ToDouble(TxtACPayAmount.Text) - Convert.ToDouble(TxtTotalAmount.Text);
        TxtTransferAMT.Text = PRIAMT.ToString();
        
        BindGrid();
    }
    public void ClearDataWR()
    {
        TxtWRPRCD.Text = "";
        TxtWRPRName.Text = "";
        TxtWRAccNo.Text = "";
        TxtWRAccName.Text = "";
        TxtWRAMT.Text = "0";
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
             string STR = MT.GetDepositCat(Session["BRCD"].ToString(), TxtProcode.Text, "MISTRF");
             if (STR == "MIS" &&  (TxtProcode4.Text=="" || TxtAccNo4.Text==""))
             {
                 WebMsgBox.Show("Enter Transfer Account for MIS Deposit...!", this.Page);
                 TxtProcode4.Text = "";
                 TxtProcode4.Focus();
                 
             }
             else
             {

                 Result = FDR.InsertTempNew("5", TxtProcode.Text, "9999", TxtDepoAmt.Text, "1", ViewState["CT"].ToString(), "X_" + Session["MID"].ToString(), TxtRate.Text, ddlduration.SelectedValue, TxtPeriod.Text, TxtIntrest.Text, DtDueDate.Text, TxtMaturity.Text, TxtProcode4.Text, TxtAccNo4.Text, ddlIntrestPay.SelectedValue, dtDeposDate.Text, string.IsNullOrEmpty(TxtReceiptNo.Text) ? "0" : TxtReceiptNo.Text, ViewState["OPRTYPE"].ToString(), ViewState["ACCTYPE"].ToString());

                 double US = Convert.ToDouble(FDR.GetCurrentBalance("X_" + Session["MID"].ToString(), "Y"));

                 TxtDiff.Text = US.ToString();

                 BindGrid();

                 double AC = Convert.ToDouble(TxtAMTCOLL.Text);
                 double RM = Convert.ToDouble(TxtDiff.Text);
                 if (RM != 0)
                 {
                     if (ViewState["MS"].ToString() == "N")
                     {
                         PostEntry.Visible = false;
                         BtnPostMultiple.Visible = false;
                         BtnSubmit.Visible = true;
                     }
                     else
                     {
                         BtnPostMultiple.Visible = false;
                         PostEntry.Visible = false;
                         BtnSubmit.Visible = true;
                         if (rdbMultiple.Checked == true)
                         {
                             ClearData();
                         }
                     }
                 }
                 else if (RM == 0)
                 {
                     if (ViewState["MS"].ToString() == "N")
                     {
                         BtnPostMultiple.Visible = false;
                         PostEntry.Visible = true;
                         BtnSubmit.Visible = false;
                         ClearData();
                     }
                     else
                     {
                         BtnPostMultiple.Visible = true;
                         PostEntry.Visible = false;
                         BtnSubmit.Visible = false;
                         if (rdbMultiple.Checked == true)
                         {
                             ClearData();
                         }
                     }
                     
                 }
               
             }
        }
        catch(Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtDepoAmt_TextChanged(object sender, EventArgs e)
    {
        try
        {

            double AC = Convert.ToDouble(TxtDepoAmt.Text);
            double RM = Convert.ToDouble(TxtDiff.Text);
            if (AC > RM)
            {
                lblMessage.Text = "Sorry Deposit  Amount Exceed The Limit Amount.....!!";
                ModalPopup.Show(this.Page);
                TxtDepoAmt.Text = "";
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
            string RT = "";
            string RR = FDR.Getrate(TxtPeriod.Text, TxtProcode.Text, ddlduration.SelectedValue, ViewState["ACCTYPE"].ToString(), Session["BRCD"].ToString(),Session["EntryDate"].ToString());
            if (RR != null)
            {
                TxtRate.Text = RR;
                RT = TxtRate.Text;
                string PT = ddlduration.SelectedItem.Text;
                if (PT == "Days" || PT == "DAYS")
                    PT = "D";
                else if (PT == "Months" || PT == "MONTHS")
                    PT = "M";
                if (RT != "")
                {
                    // TxtDepoAmt.Text = TxtTotalAmount.Text;
                    double RATE = Convert.ToDouble(RT);
                    DtDueDate.Text = conn.AddMonthDay(dtDeposDate.Text, TxtPeriod.Text, PT).Replace("12:00:00", "");
                    CalculatedepositINT(float.Parse(TxtDepoAmt.Text), TxtProcode.Text, float.Parse(RATE.ToString()), Convert.ToInt32(TxtPeriod.Text), ddlIntrestPay.SelectedItem.Text, ddlduration.SelectedValue);
                }
                BtnSubmit.Focus();
            }
            else
            {
                WebMsgBox.Show("Invalid Period, Add from Interest Master....!", this.Page);
                TxtPeriod.Text = "";
                TxtPeriod.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void CalculatedepositINT(float amt, string subgl, float intrate, int Period, string intpay, string PTYPE)
    {
        try
        {
            float interest = 0;
            float maturityamt = 0;
            float QUATERS = 0;
            float tmp1 = 0;
            float tmp2 = 0;
            //string sql = "select GLGROUP from glmast where glcode=5 AND SUBGLCODE='"+subgl+"'";
            string sql = "SELECT CATEGORY FROM DEPOSITGL where DEPOSITGLCODE='" + subgl + "'";
            string category = conn.sExecuteScalar(sql);

            if (category == "")
            {
                return;
            }
            switch (category)
            {
                case "DD":
                    interest = amt;
                    maturityamt = interest + amt;
                    TxtIntrest.Text = interest.ToString("N");
                    TxtMaturity.Text = maturityamt.ToString("N");
                    break;

                case "MIS":
                    interest = Convert.ToInt32(amt * intrate / (1200 + intrate));
                   // maturityamt = interest + amt;
                    maturityamt = amt;
                    TxtIntrest.Text = interest.ToString("N");
                    TxtMaturity.Text = maturityamt.ToString("N");
                    break;

                case "CUM":

                     float PrmAmt = amt;
                    string CALCTYPE = CurrentCls.GetCUMCal(Session["BRCD"].ToString(),TxtProcode.Text, "CUM");

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

                            interest = amt * intrate / 100/2;
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
                    TxtIntrest.Text = Math.Round(interest,0).ToString("N");
                    TxtMaturity.Text = Math.Round(maturityamt,0).ToString("N");

                    TxtReceiptNo.Focus();
                    break;

                                       
                case "FDS":
                    if (intpay == "ON MATURITY")
                    {
                        if (PTYPE == "D" || PTYPE == "idvasa")
                        {
                            interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 365) * (Period));
                            maturityamt = (interest) + (amt);
                            TxtIntrest.Text = interest.ToString("N");
                            TxtMaturity.Text = maturityamt.ToString("N");
                        }
                        else if (PTYPE == "M" || PTYPE == "maihnao")
                        {
                            interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (Period));
                            maturityamt = (interest) + (amt);
                            TxtIntrest.Text = interest.ToString("N");
                            TxtMaturity.Text = maturityamt.ToString("N");
                        }
                    }
                    else if (intpay == "Quaterly" || intpay == "QUATERLY")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (3));
                        maturityamt = amt; //Val(interest) + Val(amt);                    
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                    }
                    else if (intpay =="Half Yearly" || intpay == "HALF YEARLY")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (6));
                        maturityamt = interest + amt;
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                    }
                    else if (intpay == "Monthly" || PTYPE == "maihano")
                    {
                        interest = Convert.ToInt32((amt) * (intrate) / (1200 + intrate));
                        maturityamt = amt; //Val(interest) + Val(amt);
                        //maturityamt = interest + amt;
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                    }
                    break;
                case "DP":
                    if (PTYPE == "Days" || PTYPE == "idvasa")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 365) * (Period));
                        maturityamt = (interest) + (amt);
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                    }
                    else if (PTYPE == "Months" || PTYPE == "maihnao")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (Period));
                        maturityamt = (interest) + (amt);
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                    }
                    break;
                case "RD":
                    float deamt = amt;
                    float tempamt = amt;
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
                            //TxtIntrest.Text = interest1.ToString();
                            tmp1 = (float)Convert.ToDouble(interest1);
                            TxtIntrest.Text = tmp1.ToString("N");
                            tmp2 = (float)Convert.ToDouble(MAMT);
                            TxtMaturity.Text = tmp2.ToString("N");
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
                            TxtIntrest.Text = interest.ToString("N");
                            TxtMaturity.Text = maturityamt.ToString("N");
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
    protected void rdbPWINT_CheckedChanged(object sender, EventArgs e)
    {
        DIVWR.Visible = false;
        AMT.Visible = false;
        WRPAYTP.Visible = false;
        AMTDetail.Visible = false;
        TxtWRAMT.Text = "";
    }
    protected void rdbOP_CheckedChanged(object sender, EventArgs e)
    {
        if (ViewState["MS"].ToString() == "N")
        {
            DIVWR.Visible = true;
            AMT.Visible = true;
            WRPAYTP.Visible = false;
            AMTDetail.Visible = false;
            double DAMT, MAMT, INTAMT;
            DAMT = MAMT = INTAMT = 0;
            DAMT = Convert.ToDouble(ViewState["DAMT"].ToString());
            MAMT = Convert.ToDouble(ViewState["TPAY"].ToString());
            INTAMT = (MAMT - DAMT);
            ViewState["TINTAMT"] = INTAMT.ToString();
            TxtWRAMT.Text = INTAMT.ToString();
        }
        else
        {
            DIVWR.Visible = true;
            AMT.Visible = true;
            WRPAYTP.Visible = false;
            AMTDetail.Visible = false;
            double DAMT, MAMT, INTAMT;
            DAMT = MAMT = INTAMT = 0;
            //DAMT = Convert.ToDouble(ViewState["DAMT"].ToString());
            //MAMT = Convert.ToDouble(ViewState["TPAY"].ToString());
            INTAMT = (MAMT - DAMT);
           // ViewState["TINTAMT"] = INTAMT.ToString();
            TxtWRAMT.Text = INTAMT.ToString();
            TxtWRPRCD.Focus();
        }

    }
    protected void rdbWR_CheckedChanged(object sender, EventArgs e)
    {
        DIVWR.Visible = false;
        AMT.Visible = false;
        WRPAYTP.Visible = true;

    }
    protected void RdbSingle_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["MS"].ToString() == "N")
            {
                TxtAMTCOLL.Text = FDR.GetCurrentBalance("X_"+Session["MID"].ToString(), "Y").ToString();
                if (rdbOP.Checked != true)
                    TxtDiff.Text = TxtAMTCOLL.Text;

                else
                    TxtDiff.Text = (Convert.ToDouble(ViewState["TPAY"].ToString()) - Convert.ToDouble(ViewState["INTPAY"].ToString())).ToString();
                TxtDepoAmt.Enabled = false;

                ClearFD();
                string DD;
                DD = FDR.GetDUEDATE(ViewState["FDTP"].ToString(), Session["BRCD"].ToString(), "DUED", ViewState["ACC"].ToString(), ViewState["CT"].ToString());
                if (DD != null)
                {
                    dtDeposDate.Text = DD.ToString();
                }


                if (rdbOP.Checked != true)
                    TxtDepoAmt.Text = TxtAMTCOLL.Text;

                else if (rdbOP.Checked == true && RdbSingle.Checked == true)
                   TxtDepoAmt.Text = (Convert.ToDouble(ViewState["TPAY"].ToString()) - Convert.ToDouble(ViewState["TINTAMT"].ToString())).ToString();
                    //TxtDepoAmt.Text = FDR.GetCurrentBalance("X_" + Session["MID"].ToString(), "Y").ToString();
                else
                    TxtDepoAmt.Text = (Convert.ToDouble(ViewState["TPAY"].ToString()) - Convert.ToDouble(ViewState["INTPAY"].ToString())).ToString();



                ViewState["CTYPE"] = "S";
            }
            else
            {
               
                TxtAMTCOLL.Text = FDR.GetCurrentBalance("X_" + Session["MID"].ToString(), "Y").ToString();
                if (rdbOP.Checked != true)
                    TxtDiff.Text = TxtAMTCOLL.Text;

                else
                    TxtDiff.Text = (Convert.ToDouble(ViewState["TPAY"].ToString()) - Convert.ToDouble(ViewState["INTPAY"].ToString())).ToString();
                TxtDepoAmt.Enabled = false;

                ClearFD();

                if (rdbOP.Checked != true)
                    TxtDepoAmt.Text = TxtAMTCOLL.Text;

                else if (rdbOP.Checked == true && RdbSingle.Checked == true)
                    //TxtDepoAmt.Text = (Convert.ToDouble(ViewState["TPAY"].ToString()) - Convert.ToDouble(ViewState["TINTAMT"].ToString())).ToString();
                TxtDepoAmt.Text = FDR.GetCurrentBalance("X_" + Session["MID"].ToString(), "Y").ToString();
                else
                    TxtDepoAmt.Text = (Convert.ToDouble(ViewState["TPAY"].ToString()) - Convert.ToDouble(ViewState["INTPAY"].ToString())).ToString();

                TxtProcode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void rdbMultiple_CheckedChanged(object sender, EventArgs e)
    {
        if (ViewState["MS"].ToString() == "N")
        {
            TxtAMTCOLL.Text = FDR.GetCurrentBalance("X_" + Session["MID"].ToString(), "Y").ToString();
            TxtDiff.Text = TxtAMTCOLL.Text;
            TxtDepoAmt.Enabled = true;
            ClearFD();
            string DD;
            DD = FDR.GetDUEDATE(ViewState["FDTP"].ToString(), Session["BRCD"].ToString(), "DUED", ViewState["ACC"].ToString(), ViewState["CT"].ToString());
            if (DD != null)
            {
                dtDeposDate.Text = DD.ToString();
            }
            TxtDepoAmt.Text = "0";
            ViewState["CTYPE"] = "M";
        }
        else
        {
            TxtAMTCOLL.Text = FDR.GetCurrentBalance("X_" + Session["MID"].ToString(), "Y").ToString();
            TxtDiff.Text = TxtAMTCOLL.Text;
            TxtDepoAmt.Enabled = true;
            ClearFD();
            string DD;
            TxtDepoAmt.Text = "0";
            ViewState["CTYPE"] = "M";
            TxtProcode.Focus();
        }
    }
    protected void PostEntry_Click(object sender, EventArgs e)
    {
        try
        {
            string RC = "";
            
            if (ViewState["REFID"] == null)
                ViewState["REFID"] = "";

            RC = FDR.PostData(Session["EntryDate"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), ViewState["CT"].ToString(), "X_" + Session["MID"].ToString(), dtDeposDate.Text, ViewState["ACCTYPE"].ToString(), ViewState["OPRTYPE"].ToString(), ViewState["REFID"].ToString(), ddlIntrestPay.SelectedValue, TxtReceiptNo.Text);
            string[] M = RC.Split('_');
            string ST, Acc;
            ST = Acc = "";
            ST = M[0].ToString();
            string AC = "";
            for (int I = 1; I <= M.Length - 1; I++)
            {
                AC = AC + '_' + M[I].ToString();
            }
            PostEntry.Enabled = false;
            //lblMessage.Text = "Your Voucher No is :" + ST + " and Acc No is " + AC;
            ClearData();
            WebMsgBox.Show("Your Voucher No is :" + ST + " and Acc No is " + AC, this.Page);
            //ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();window.opener.location.href=window.opener.location.href;", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void ClearData()
    {
        TxtWRPRCD.Text = "";
        TxtWRPRName.Text = "";
        TxtWRAccNo.Text = "";
        TxtWRAccName.Text = "";
        TxtWRAMT.Text = "0";
        TxtDepoAmt.Text = "";
        TxtMaturity.Text = "";
        //dtDeposDate.Text = "";
        //DtDueDate.Text = "";
        TxtTotalAmount.Text = "";
        //TxtDiff.Text="";
        ddlPayType.SelectedValue = "0";
        ddlProductType.SelectedValue = "0";
        ddlduration.SelectedValue = "0";
        ddlIntrestPay.SelectedValue = "0";
        TxtPeriod.Text = "";
        TxtProcode.Text ="";
        TxtRate.Text = "";
        TxtIntrest.Text = "";
        TxtReceiptNo.Text = "";
        TxtProcode.Focus();

    }
    protected void TxtProcode4_TextChanged(object sender, EventArgs e)
    {
        try
        {
            
            TxtProName4.Text = CMN.GetAllProductName(Convert.ToInt32(TxtProcode4.Text),Session["BRCD"].ToString());
            if (TxtProName4.Text.ToString() == "")
            {
                WebMsgBox.Show("Product code not present", this.Page);
                TxtProcode4.Text = "";
                TxtProcode4.Focus();
            }
            else
            {
                TxtAccNo4.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
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
            DataTable dt = new DataTable();
            dt = CMN.GetCustNoNameGL1(TxtProcode4.Text.ToString(), TxtAccNo4.Text.ToString(),Session["BRCD"].ToString());

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
    public void ClearFD()
    {
        TxtProcode.Text = "";
        ddlProductType.SelectedValue = "0";
       // dtDeposDate.Text = Session["EntryDate"].ToString();
        DtDueDate.Text = "";
        ddlIntrestPay.SelectedValue = "0";
        ddlduration.SelectedValue = "0";
        TxtPeriod.Text = "";
        TxtMaturity.Text = "";
        TxtIntrest.Text = "";
    }
    protected void ddlIntrestPay_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ddlduration.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnPostMultiple_Click(object sender, EventArgs e)
    {
        try
        {
            string PR=ddlduration.SelectedValue.ToString();
           

            string TGLC="0",TSUBGL="0",TACNO="0";
            if(TxtProcode4.Text!="" || TxtAccNo4.Text!="")
            {
                
                TSUBGL=TxtProcode4.Text;
                TACNO=TxtAccNo4.Text;
                string[] T=BD.GetAccTypeGL(TSUBGL,Session["BRCD"].ToString()).Split('_');
                TGLC=T[1].ToString();
            }


            double IntAmt = 0;
            double MatAmt = 0;
            string RES = FDR.PostMultipleClosure("POSTMULTIPLE", "S", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["MSETNO"].ToString(), "X_" + Session["MID"].ToString(), Session["MID"].ToString(),
                                                TxtProcode.Text, TxtDepoAmt.Text, ViewState["CT"].ToString(), ViewState["ACCTYPE"].ToString(), TxtReceiptNo.Text, TxtRate.Text, PR, TxtPeriod.Text, DtDueDate.Text, IntAmt.ToString(),
                                                MatAmt.ToString(), ddlIntrestPay.SelectedValue.ToString(), TGLC, TSUBGL, TACNO, dtDeposDate.Text);
            if (RES != null)
            {
                WebMsgBox.Show(RES, this.Page);
                ClearData();
                BtnSubmit.Visible = true;
                BtnPostMultiple.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}