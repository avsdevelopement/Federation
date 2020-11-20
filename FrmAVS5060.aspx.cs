using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class FrmAVS5060 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    DataTable DS = new DataTable();
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    ClsCommon CMN = new ClsCommon();
    scustom customcs = new scustom();
    ClsNewTDA CurrentCls = new ClsNewTDA();
    ClsTDAClear CurrentCls1 = new ClsTDAClear();
    DbConnection conn = new DbConnection();
    ClsOpenClose OC = new ClsOpenClose();
    ClsAuthorized POSTV = new ClsAuthorized();
    ClsInsertTrans ITrans = new ClsInsertTrans();

    ClsLoanInfo LI = new ClsLoanInfo();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsLoanClosure LC = new ClsLoanClosure();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsMISTransfer MT = new ClsMISTransfer();
    ClsDDCR DDS = new ClsDDCR();
    ClsCashReciept CR = new ClsCashReciept();
    ClsCashPayment CP = new ClsCashPayment();
    ClsFDARenew FDR = new ClsFDARenew();

    ClsRDExcess RD = new ClsRDExcess();
    int result = 0, resultintrst = 0, resultout = 0;
    double TotalAmt = 0, TotalDrAmt = 0, TxtAmount = 0;
    string Res = "", FL = "";
    string REFERENCEID = "", CN = "", CD = "";
    string AC_Status = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }

            }
            catch (Exception Ex)
            {

                ExceptionLogging.SendErrorToText(Ex);
            }
        }
    }
    protected void TxtProcode_TextChanged(object sender, EventArgs e)
    {
        productcd();
    }
    protected void TxtProName_TextChanged(object sender, EventArgs e)
    {
       
    }
    protected void txtAccNo_TextChanged(object sender, EventArgs e)
    {
        accno();
    }
    protected void TxtAccname_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtDepoAmt_TextChanged(object sender, EventArgs e)
    {

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
                  
                }
                string GLL = BD.GetAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString());
                if (GLL != null)
                {
                    string[] GLLL = GLL.Split('_');
                    ViewState["GLCODE"] = GLLL[1].ToString();
                    if (ViewState["GLCODE"].ToString() == "15")
                    {
                    
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
                    
                        TxtProcode.Focus();
                        return;
                    }
                    else
                    {
                      
                        string IsRD = CMN.CheckRD(TxtProcode.Text.ToString(), Session["BRCD"].ToString());
                        if (IsRD == null)
                        {
                            ViewState["RD"] = "NO";
                        }
                        else
                        {
                            ViewState["RD"] = "YES";
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
            AC_Status = CMN.GetAccStatus(Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text);
            if (AC_Status == "1")
            {

                AT = BD.Getstage1(txtAccNo.Text, Session["BRCD"].ToString(), TxtProcode.Text);
                if (AT != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise.........!!";
                    ModalPopup.Show(this.Page);
                 
                }
                else
                {
                    if (TxtProcode.Text != "" && txtAccNo.Text != "")
                    {
                        DataTable dtmodal = new DataTable();
                        dtmodal = CR.GetInfoTbl(Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), txtAccNo.Text, TxtProcode.Text);
                        if (dtmodal.Rows.Count > 0)
                        {
                          
                        }
                    }
                    CheckData();
                  

                    if (ViewState["GLCODE"].ToString() != null && ViewState["GLCODE"].ToString() == "15" && ViewState["MATURITY"].ToString() == "PRE")
                    {
                      
                    }
                 
                }
            }
            else if (AC_Status == "2")
            {
                lblMessage.Text = "Acc number " + txtAccNo.Text + " is In-operative.........!!";
                ModalPopup.Show(this.Page);
              
            }
            else if (AC_Status == "3")
            {
                lblMessage.Text = "Acc number " + txtAccNo.Text + " is Closed.........!!";
                ModalPopup.Show(this.Page);
           
            }
            else if (AC_Status == "4")
            {
               
                dt = DDS.getDetails(Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text);
                if (dt != null)
                {
                    lblMessage.Text = "Acc number " + txtAccNo.Text + " is Lean Marked / Loan Advanced .........!! with LoanGlCode=" + dt.Rows[0]["LOANGLCODE"].ToString() + " and LoanAccNo=" + dt.Rows[0]["LOANACCNO"].ToString() + " and Loan Amount=" + dt.Rows[0]["LOANAMT"].ToString() + "";
                  
                    ModalPopup.Show(this.Page);
                }
                else
                {
                    WebMsgBox.Show("Account is Lean Marked, Lien details not found...", this.Page);
                }
               // clear();
            }
            else if (AC_Status == "5")
            {
                lblMessage.Text = "Acc number " + txtAccNo.Text + " is Credit Freezed.........!!";
                ModalPopup.Show(this.Page);
              
            }
            else if (AC_Status == "6")
            {
                lblMessage.Text = "Acc number " + txtAccNo.Text + " is Debit Freezed.........!!";
                ModalPopup.Show(this.Page);
               
            }
            else if (AC_Status == "7")
            {
                lblMessage.Text = "Acc number " + txtAccNo.Text + " is Total Freezed.........!!";
                ModalPopup.Show(this.Page);
               
            }
            else
            {
                WebMsgBox.Show("Enter Valid Account number!...", this.Page);
                txtAccNo.Text = "";
                txtAccNo.Focus();
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
                ddlIntrestPay.Text = dt1.Rows[0]["INTPAYOUT"].ToString();
                TxtDepoAmt.Text = dt1.Rows[0]["PRNAMT"].ToString();
                TxtPeriod.Text = dt1.Rows[0]["PERIOD"].ToString();
                ddlduration.SelectedValue = dt1.Rows[0]["PRDTYPE"].ToString();
                TxtRate.Text = dt1.Rows[0]["RATEOFINT"].ToString();
                TxtIntrest.Text = Convert.ToInt32(dt1.Rows[0]["INTAMT"]).ToString();
                TxtMaturity.Text = Convert.ToInt32(dt1.Rows[0]["MATURITYAMT"]).ToString();
                DtDueDate.Text = Convert.ToDateTime(dt1.Rows[0]["DUEDATE"].ToString()).ToString("dd/MM/yyyy");
               // TxtTDLastIntDt.Text = Convert.ToDateTime(dt1.Rows[0]["LASTINTDATE"]).ToString("dd/MM/yyyy");
                string LMSTS = dt1.Rows[0]["LMSTATUS"].ToString();

                if (LMSTS == "1")
                    TxtOpenClose.Text = "Open";
                else if (LMSTS == "2")
                    TxtOpenClose.Text = "Lien";
                else if (LMSTS == "3")
                    TxtOpenClose.Text = "Close";

                if (LMSTS == "3")
                {
                   // BtnSubmit.Enabled = false;
                }

                ViewState["ACCT"] = dt1.Rows[0]["ACC_TYPE"].ToString();
                ViewState["OPRTYPE"] = dt1.Rows[0]["OPR_TYPE"].ToString();


                int DF = Convert.ToInt32(conn.GetDayDiff(Session["EntryDate"].ToString(), DtDueDate.Text));
                if (DF > 0)
                {
                   /// rdbPreMature.Checked = true;
                   // ViewState["MATURITY"] = "PRE";
                }
                else
                {
                  //  rdbMature.Checked = true;
                  //  ViewState["MATURITY"] = "MA";
                }

                string[] arraydt = { "" };


                arraydt = Session["EntryDate"].ToString().Split('/');
                //if (rdbMature.Checked == true)
                //{
                //    Res = BD.GetFDSBINTStatus(Session["BRCD"].ToString());
                //    if (Res == "Y")
                //        GetSBINT();
                //    else
                //    {
                //        TxtSbintrest.Text = "0";
                //    }
                //}

                if (ViewState["RD"].ToString() == "YES") // For RD Interest Calculation
                {
                   // CalculateRDInt();

                }
                else // For other than RD Interest Calculation
                {
                   // CalculateINT();
                }


                // Checking Interset is paid today or not
               
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
    public void ClearAll()
    {
        TxtProcode.Text = "";
        TxtProName.Text = "";
        txtAccNo.Text = "";
        Txtcustno.Text = "";
        TxtAccname.Text = "";
        dtDeposDate.Text = "";
        TxtDepoAmt.Text = "";
       // ddlduration.SelectedIndex = 0;
        TxtPeriod.Text = "";
       // ddlIntrestPay.SelectedIndex = 0;
        TxtRate.Text = "";
        TxtIntrest.Text = "0";
        TxtMaturity.Text = "";
        DtDueDate.Text = "";
        txtRDExcess.Text = "";
        txtRemark.Text = "";
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int Result = RD.insertExcess(Session["BRCD"].ToString(), TxtProcode.Text, txtAccNo.Text, txtRDExcess.Text, txtRemark.Text,Session["MID"].ToString());
            if (Result > 0)
            {
                WebMsgBox.Show("RD Excess Amount Added Successfully..!!", this.Page);
                ClearAll();
            }
            else
            {
                WebMsgBox.Show("RD Excess Amount Not Added Successfully..!!", this.Page);
                ClearAll();
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {

    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {

    }
}