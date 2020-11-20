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
public partial class FrmCashPayment : System.Web.UI.Page
{
    scustom customcs = new scustom();
    ClsCashPayment CurrentCls = new ClsCashPayment();
    ClsCashReciept CR = new ClsCashReciept();
    ClsAuthorized PVOUCHER = new ClsAuthorized();
    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsOpenClose OC = new ClsOpenClose();
    ClsCommon CC = new ClsCommon();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    DataTable DT = new DataTable();
    ClsInwordClear OWGCL = new ClsInwordClear();

    int resultint, resultout = 0;
    string AC_Status = "", FL = "", sResult = "";
    double Limit = 0;

    #region Page Load
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // DIVPHOTO.Visible = false;
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }
                TxtProcode.Focus();
                GetTotalAmt();
                BindGrid();
                LnkVerify.Visible = false;
                TxtEntrydate.Text = Session["EntryDate"].ToString();
                TxtEntrydate.Enabled = false;
                autoglname.ContextKey = Session["BRCD"].ToString();
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
    }

    #endregion

    #region Text Changed Events
    
    protected void TxtProcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtProcode.Text == "")
            {
                TxtProName.Text = "";
                TxtAccNo.Focus();
                goto ext;
            }

            //Added By Amol on 22092017 as per ambika mam instruction
            if (BD.GetProdOperate(Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString()).ToString() != "3")
            {
                int result = 0;
                string GLS1;
                int.TryParse(TxtProcode.Text, out result);
                TxtProName.Text = customcs.GetProductName(result.ToString(), Session["BRCD"].ToString());
                Txtcustno.Text = "0";

                GLS1 = BD.GetAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString());
                if (GLS1 != null)
                {
                    string[] GLS = GLS1.Split('_');
                    ViewState["DRGL"] = GLS[1].ToString();

                    // Added by amol on 27/10/2017 as per Ambika mam instruction
                    sResult = BD.GetDepositCat(Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString());

                    if (GLS[1].ToString() != "3" && GLS[1].ToString() != "5" && GLS[1].ToString() != "2" || GLS[1].ToString() == "5" && sResult == "DP")
                    {

                        AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();
                        int GL = 0;
                        int.TryParse(ViewState["DRGL"].ToString(), out GL);
                        string YN = CC.GetIntACCYN(Session["BRCD"].ToString(), TxtProcode.Text);

                        // Added by Amol On 2017-08-12 for ccod debit as per limit
                        if (ViewState["DRGL"].ToString() == "3")
                        {
                            ViewState["LoanCat"] = BD.GetLoanCategory(TxtProcode.Text.Trim().ToString(), Session["BRCD"].ToString());
                        }

                        if (GL >= 100 && YN != "Y")
                        {
                            TxtAccNo.Text = TxtProcode.Text;
                            TxtAccName.Text = TxtProName.Text;
                            TxtVoucherTypeno.Focus();
                            string[] TD = Session["EntryDate"].ToString().Split('/');

                            //on 16-10-2017 as per instr by Darade
                            txtBalance.Text = OC.GetCPOpenClose("CPClosing", Session["Brcd"].ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text, "0", Session["EntryDate"].ToString(),YN).ToString();
                            TxtNewBalance.Text = OC.GetCPOpenClose("Closing", Session["Brcd"].ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text, "0", Session["EntryDate"].ToString(), YN).ToString();

                        }
                        else
                        {
                            TxtAccNo.Focus();
                        }
                        if (TxtProName.Text == "")
                        {
                            WebMsgBox.Show("Please enter valid Product code", this.Page);
                            TxtProcode.Text = "";
                            TxtProcode.Focus();
                        }
                    }
                    else
                    {
                        WebMsgBox.Show("Product code not allowed....!", this.Page);
                        TxtProcode.Text = "";
                        TxtProName.Text = "";
                        TxtProcode.Focus();
                    }
                }
                else
                {
                    WebMsgBox.Show("Enter Valid Product code!....", this.Page);
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
        ext: ;
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
            string AT = "";
            AC_Status = CC.GetAccStatus(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text);
            if (AC_Status == "1")
            {
                AT = BD.Getstage1(TxtAccNo.Text, Session["BRCD"].ToString(), TxtProcode.Text);
                if (AT != null)
                {
                    if (AT != "1003")
                    {
                        grdAccDetails.DataSource = null;
                        grdAccDetails.DataBind();
                        lblMessage.Text = "Sorry Customer not Authorise.........!!";
                        ModalPopup.Show(this.Page);
                        Clear();
                    }
                    else
                    {
                        string[] TD = Session["EntryDate"].ToString().Split('/');

                        txtBalance.Text = OC.GetCPOpenClose("CPClosing", Session["Brcd"].ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["EntryDate"].ToString()).ToString();
                        TxtNewBalance.Text = OC.GetCPOpenClose("Closing", Session["Brcd"].ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["EntryDate"].ToString()).ToString();
                        txtamountt.Enabled = true;

                        if (CR.CheckValidation(TxtProcode.Text, Session["BRCD"].ToString()) == "N")
                        {
                            if (Convert.ToDouble(TxtNewBalance.Text) <= 0)
                            {
                                TxtAccNo.Focus();
                                txtamountt.Enabled = false;
                                WebMsgBox.Show("Sorry Insufficient Account Balance...!!", this.Page);
                                return;
                            }
                        }
                        if (TxtAccNo.Text == "")
                        {
                            TxtAccName.Text = "";
                            goto ext;
                        }
                        TxtStrtDt.Text = CC.GetOpenDate(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text);

                        DataTable dt1 = new DataTable();
                        if (TxtAccNo.Text != "" & TxtProcode.Text != "")
                        {
                            string acctype, sql = "", acctypeno, jointname = "";
                            string PRD = TxtProcode.Text;
                            string[] accn;
                            ////Added account type,account joint name and special instruction by ankita 03/06/2017
                            DataTable dt = new DataTable();
                            dt = customcs.GetAccName(TxtAccNo.Text.ToString(), PRD, Session["BRCD"].ToString());
                            if (dt.Rows.Count > 0)
                            {
                                accn = dt.Rows[0]["CUSTNAME"].ToString().Split('_');
                                ViewState["CUSTNO"] = accn[0].ToString();
                                Txtcustno.Text = ViewState["CUSTNO"].ToString();
                                getMobile();
                                txtPan.Text = PanCard(Session["BRCD"].ToString(), ViewState["CUSTNO"].ToString());
                                TxtAccName.Text = accn[1].ToString();
                                TxtSplInst.Text = dt.Rows[0]["SPL_INSTRUCTION"].ToString();
                                txtnaration.Text = "To Cash";
                                acctypeno = dt.Rows[0]["OPR_TYPE"].ToString();
                                acctype = CR.GetAcctype(acctypeno);
                                txtAccTypeName.Text = acctype.ToString();
                                if (txtAccTypeName.Text == "JOINT")
                                {
                                    jointname = CR.Getjointname(Session["BRCD"].ToString(), TxtAccNo.Text.ToString(), PRD);
                                    lbjoint.Visible = true;
                                    TxtJointName.Visible = true;
                                    TxtJointName.Text = jointname.ToString();
                                    TxtStrtDt.Text = CC.GetOpenDate(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text);
                                }
                                else
                                {
                                    lbjoint.Visible = false;
                                    TxtJointName.Visible = false;
                                }
                                if (TxtAccName.Text == "" & TxtAccNo.Text != "")
                                {
                                    grdAccDetails.DataSource = null;
                                    grdAccDetails.DataBind();
                                    WebMsgBox.Show("Please enter valid Account number", this.Page);
                                    TxtAccNo.Text = "";
                                    TxtAccNo.Focus();
                                    return;
                                }
                                if (TxtAccName.Text != "")
                                {
                                    TxtVoucherTypeno.Focus();
                                }
                                // Added By Amol as per darade sir instruction on 19/06/2017 bcoz display all account details related to customer in grid
                                resultout = CR.GetAccDetails(grdAccDetails, Session["BRCD"].ToString(), Txtcustno.Text.Trim().ToString(), Session["EntryDate"].ToString());
                                if (resultout < 0)
                                {
                                    grdAccDetails.DataSource = null;
                                    grdAccDetails.DataBind();
                                }
                            }
                            ////Displayed modal popup of voucher info by ankita 20/05/2017
                            DataTable dtmodal = new DataTable();
                            dtmodal = CR.GetInfoTbl(Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtProcode.Text);
                            if (dtmodal.Rows.Count > 0)
                            {
                                resultout = CR.GetInfo(GrdView, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtProcode.Text);
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
                            if (Txtcustno.Text.ToString() != "0")
                            {
                                Photo_Sign();
                                DT = CC.CheckCustODAcc(Session["BRCD"].ToString(), Txtcustno.Text.ToString(), Session["EntryDate"].ToString());
                                if (DT.Rows.Count > 0)
                                    WebMsgBox.Show("Operation in defaulter account...!!", this.Page);
                            }
                        }
                    ext: ;
                    }
                }

                else
                {
                    WebMsgBox.Show("Enter Valid Account number!...", this.Page);
                    grdAccDetails.DataSource = null;
                    grdAccDetails.DataBind();
                    TxtAccNo.Text = "";
                    TxtAccNo.Focus();
                }
                LnkVerify.Visible = true;
            }
            else if (AC_Status == "2")
            {
                grdAccDetails.DataSource = null;
                grdAccDetails.DataBind();
                lblMessage.Text = "Acc number " + TxtAccNo.Text + " is In-operative.........!!";
                ModalPopup.Show(this.Page);
                Clear();
            }
            else if (AC_Status == "3")
            {
                grdAccDetails.DataSource = null;
                grdAccDetails.DataBind();
                lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Closed.........!!";
                ModalPopup.Show(this.Page);
                Clear();
            }
            else if (AC_Status == "4")
            {
                grdAccDetails.DataSource = null;
                grdAccDetails.DataBind();
                lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Lean Marked / Loan Advanced .........!!";
                ModalPopup.Show(this.Page);
                Clear();
            }
            else if (AC_Status == "6")
            {
                grdAccDetails.DataSource = null;
                grdAccDetails.DataBind();
                lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Debit Freezed.........!!";
                ModalPopup.Show(this.Page);
                Clear();
            }
            else if (AC_Status == "7")
            {
                grdAccDetails.DataSource = null;
                grdAccDetails.DataBind();
                lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Total Freezed.........!!";
                ModalPopup.Show(this.Page);
                Clear();
            }
            else
            {
                grdAccDetails.DataSource = null;
                grdAccDetails.DataBind();
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
                    ViewState["CUSTNO"] = custnob[2].ToString();
                    Txtcustno.Text = ViewState["CUSTNO"].ToString();
                    getMobile();
                    txtPan.Text = PanCard(Session["BRCD"].ToString(), ViewState["CUSTNO"].ToString());
                    string[] TD = Session["EntryDate"].ToString().Split('/');

                    //Commented on 16-10-2017 as per instr by Darade Sir
                    //txtBalance.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString()).ToString();
                    //TxtNewBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["DRGL"].ToString()).ToString();

                    txtBalance.Text = OC.GetCPOpenClose("CPClosing", Session["Brcd"].ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["EntryDate"].ToString()).ToString();
                    TxtNewBalance.Text = OC.GetCPOpenClose("Closing", Session["Brcd"].ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text, TxtAccNo.Text, Session["EntryDate"].ToString()).ToString();
                    txtamountt.Enabled = true;
                    txtnaration.Text = "To Cash";

                    if (CR.CheckValidation(TxtProcode.Text, Session["BRCD"].ToString()) == "N")
                    {
                        if (Convert.ToDouble(TxtNewBalance.Text) <= 0)
                        {
                            TxtAccNo.Focus();
                            txtamountt.Enabled = false;
                            WebMsgBox.Show("Sorry Insufficient Account Balance...!!", this.Page);
                            return;
                        }
                    }
                    txtnaration.Focus();
                    if (TxtAccNo.Text != "" & TxtProcode.Text != "")
                    {
                        string acctype, sql = "", acctypeno, jointname = "";
                        string PRD = TxtProcode.Text;
                        string[] accn;
                        ////Added account type,account joint name and special instruction by ankita 03/06/2017
                        DataTable dt = new DataTable();
                        dt = customcs.GetAccName(TxtAccNo.Text.ToString(), PRD, Session["BRCD"].ToString());
                        if (dt.Rows.Count > 0)
                        {
                            accn = dt.Rows[0]["CUSTNAME"].ToString().Split('_');
                            ViewState["CUSTNO"] = accn[0].ToString();
                            Txtcustno.Text = ViewState["CUSTNO"].ToString();
                            getMobile();
                            TxtAccName.Text = accn[1].ToString();
                            TxtSplInst.Text = dt.Rows[0]["SPL_INSTRUCTION"].ToString();
                            acctypeno = dt.Rows[0]["OPR_TYPE"].ToString();
                            acctype = CR.GetAcctype(acctypeno);
                            txtAccTypeName.Text = acctype.ToString();
                            if (txtAccTypeName.Text == "JOINT")
                            {
                                jointname = CR.Getjointname(Session["BRCD"].ToString(), TxtAccNo.Text.ToString(), PRD);
                                lbjoint.Visible = true;
                                TxtJointName.Visible = true;
                                TxtJointName.Text = jointname.ToString();
                            }
                            else
                            {
                                lbjoint.Visible = false;
                                TxtJointName.Visible = false;
                            }
                            if (TxtAccName.Text == "" & TxtAccNo.Text != "")
                            {
                                grdAccDetails.DataSource = null;
                                grdAccDetails.DataBind();
                                WebMsgBox.Show("Please enter valid Account number", this.Page);
                                TxtAccNo.Text = "";
                                TxtAccNo.Focus();
                                return;
                            }
                            if (TxtAccName.Text != "")
                            {
                                TxtVoucherTypeno.Focus();
                            }
                            // Added By Amol as per darade sir instruction on 19/06/2017 bcoz display all account details related to customer in grid
                            resultout = CR.GetAccDetails(grdAccDetails, Session["BRCD"].ToString(), Txtcustno.Text.Trim().ToString(), Session["EntryDate"].ToString());
                            if (resultout < 0)
                            {
                                grdAccDetails.DataSource = null;
                                grdAccDetails.DataBind();
                            }
                        }

                        TxtStrtDt.Text = CC.GetOpenDate(Session["BRCD"].ToString(), TxtProcode.Text, TxtAccNo.Text);
                        ////Displayed modal popup of voucher info by ankita 20/05/2017
                        DataTable dtmodal = new DataTable();
                        dtmodal = CR.GetInfoTbl(Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtProcode.Text);
                        if (dtmodal.Rows.Count > 0)
                        {
                            resultout = CR.GetInfo(GrdView, Session["BRCD"].ToString(), Session["ENTRYDATE"].ToString(), TxtAccNo.Text, TxtProcode.Text);
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
                        if (Txtcustno.Text.ToString() != "0")
                        {
                            Photo_Sign();
                            DT = CC.CheckCustODAcc(Session["BRCD"].ToString(), Txtcustno.Text.ToString(), Session["EntryDate"].ToString());
                            if (DT.Rows.Count > 0)
                                WebMsgBox.Show("Operation in defaulter account...!!", this.Page);
                        }
                    }
                }

                else if (AC_Status == "2")
                {
                    grdAccDetails.DataSource = null;
                    grdAccDetails.DataBind();
                    lblMessage.Text = "Acc number " + TxtAccNo.Text + " is In-operative.........!!";
                    ModalPopup.Show(this.Page);
                    Clear();
                }
                else if (AC_Status == "3")
                {
                    grdAccDetails.DataSource = null;
                    grdAccDetails.DataBind();
                    lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Closed.........!!";
                    ModalPopup.Show(this.Page);
                    Clear();
                }
                else if (AC_Status == "4")
                {
                    grdAccDetails.DataSource = null;
                    grdAccDetails.DataBind();
                    lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Lean Marked / Loan Advanced .........!!";
                    ModalPopup.Show(this.Page);
                    Clear();
                }
                else if (AC_Status == "6")
                {
                    grdAccDetails.DataSource = null;
                    grdAccDetails.DataBind();
                    lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Debit Freezed.........!!";
                    ModalPopup.Show(this.Page);
                    Clear();
                }
                else if (AC_Status == "7")
                {
                    grdAccDetails.DataSource = null;
                    grdAccDetails.DataBind();
                    lblMessage.Text = "Acc number " + TxtAccNo.Text + " is Total Freezed.........!!";
                    ModalPopup.Show(this.Page);
                    Clear();
                }
                else
                {
                    grdAccDetails.DataSource = null;
                    grdAccDetails.DataBind();
                    TxtAccNo.Text = "";
                    TxtAccNo.Focus();
                    WebMsgBox.Show("Enter Valid Account number!...", this.Page);
                }
            }
            else
            {
                grdAccDetails.DataSource = null;
                grdAccDetails.DataBind();
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

    protected void TxtProName_TextChanged(object sender, EventArgs e)
    {
        try
        {

            string custno = TxtProName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                //Added By Amol on 22092017 as per ambika mam instruction
                if (BD.GetProdOperate(Session["BRCD"].ToString(), CT[1].ToString()).ToString() != "3")
                {
                    TxtProName.Text = CT[0].ToString();
                    TxtProcode.Text = CT[1].ToString();
                    Txtcustno.Text = "0";
                    string[] GLS = BD.GetAccTypeGL(TxtProcode.Text, Session["BRCD"].ToString()).Split('_');

                    // Added by amol on 27/10/2017 as per Ambika mam instruction
                    sResult = BD.GetDepositCat(Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString());

                    if (GLS[1].ToString() != "3" && GLS[1].ToString() != "5" && GLS[1].ToString() != "2" || GLS[1].ToString() == "5" && sResult == "DP")
                    {
                        ViewState["DRGL"] = GLS[1].ToString();
                        AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();

                        int GL = 0;
                        int.TryParse(ViewState["DRGL"].ToString(), out GL);
                        string YN = CC.GetIntACCYN(Session["BRCD"].ToString(), TxtProcode.Text);

                        // Added by Amol On 2017-08-12 for ccod debit as per limit
                        if (ViewState["DRGL"].ToString() == "3")
                        {
                            ViewState["LoanCat"] = BD.GetLoanCategory(TxtProcode.Text.Trim().ToString(), Session["BRCD"].ToString());
                        }

                        if (GL >= 100 && YN != "Y") //--abhishek as per GL LEVEL Requirement
                        {
                            TxtAccNo.Text = TxtProcode.Text;
                            TxtAccName.Text = TxtProName.Text;
                            TxtVoucherTypeno.Focus();
                            string[] TD = Session["EntryDate"].ToString().Split('/');

                            //on 16-10-2017 as per instr by Darade Sir
                            txtBalance.Text = OC.GetCPOpenClose("CPClosing", Session["Brcd"].ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text, "0", Session["EntryDate"].ToString(), YN).ToString();
                            TxtNewBalance.Text = OC.GetCPOpenClose("Closing", Session["Brcd"].ToString(), ViewState["DRGL"].ToString(), TxtProcode.Text, "0", Session["EntryDate"].ToString(), YN).ToString();

                        }
                        else
                        {
                            TxtAccNo.Focus();
                        }
                    }
                    else
                    {
                        WebMsgBox.Show("Product code not allowed....!", this.Page);
                        TxtProcode.Text = "";
                        TxtProName.Text = "";
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
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtamountt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double BAL = Convert.ToDouble(txtBalance.Text.Trim().ToString() == "" ? "0" : txtBalance.Text.Trim().ToString());
            double curbal = Convert.ToDouble(txtamountt.Text.Trim().ToString() == "" ? "0" : txtamountt.Text.Trim().ToString());

            // Added by Amol On 2017-08-12 for ccod debit amount as per limit
            if (ViewState["DRGL"].ToString() == "3")
            {
                if (ViewState["LoanCat"].ToString() == "CCOD")
                {
                    Limit = CR.CheckLimit(Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString());

                    if (Math.Abs(curbal - BAL) > Math.Abs(Limit))
                    {
                        BAL = CR.GetOpenClose(Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBalLoan");

                        if (BAL < 0)
                        {
                            if (curbal > Math.Abs(BAL))
                            {
                                lblMessage.Text = "Exceed limit amount...!!";
                                txtamountt.Text = "";
                                txtamountt.Focus();
                                ModalPopup.Show(this.Page);
                                return;
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Exceed limit amount...!!";
                            txtamountt.Text = "";
                            txtamountt.Focus();
                            ModalPopup.Show(this.Page);
                            return;
                        }
                    }
                }
                else if (CR.CheckValidation(TxtProcode.Text, Session["BRCD"].ToString()) == "N")
                {
                    if (curbal > BAL)
                    {
                        txtamountt.Text = "";
                        txtamountt.Focus();
                        lblMessage.Text = "Insufficient Account Balance...!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                }
            }
            else if (CR.CheckValidation(TxtProcode.Text, Session["BRCD"].ToString()) == "N")
            {
                if (curbal > BAL)
                {
                    txtamountt.Text = "";
                    txtamountt.Focus();
                    lblMessage.Text = "Insufficient Account Balance...!!";
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

    protected void TxtVoucherTypeno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string sql;
            sql = "SELECT DESCRIPTION FROM LOOKUPFORM1 WHERE SRNO='" + TxtVoucherTypeno.Text + "' AND LNO=1059";
            TxtVouchertype.Text = conn.sExecuteScalar(sql);
            if (TxtVouchertype.Text == "Cheque")
            {
                DIVINSTRNO.Visible = true;
                Div_Instdate.Visible = true;
            }
            else
            {
                DIVINSTRNO.Visible = false;
                Div_Instdate.Visible = false;
            }
            if (TxtVoucherTypeno.Text == "1")
                txtnaration.Text = "To Self";
            else if (TxtVoucherTypeno.Text == "2")
                txtnaration.Text = "To Cheque";
            else
                txtnaration.Text = "To Self";

            if (TxtVoucherTypeno.Text == "1")
            {
                txtnaration.Attributes["onfocus"] = "var value=this.value;this.value='';this.value=value;onfocus=null;";
                txtnaration.Focus();
            }
            else if (TxtVoucherTypeno.Text == "2")
            {
                TxtInstruNo.Focus();
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void TxtVouchertype_TextChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void Txttoken_TextChanged(object sender, EventArgs e)
    {
        btnSubmit.Focus();
    }

    protected void TxtInstruNo_TextChanged(object sender, EventArgs e)
    {
        try
        {

            int ChqLen = 0;
            string Chqno_Text = "";
            Chqno_Text = TxtInstruNo.Text;
            ChqLen = Chqno_Text.Length;
            if (ChqLen > 6 || ChqLen < 6)
            {
                WebMsgBox.Show("Enter 6 digit Instrument number ....!!", this.Page);
                TxtInstruNo.Text = "";
                TxtInstruNo.Focus();
            }
            else
            {
                txtnaration.Attributes["onfocus"] = "var value=this.value;this.vlaue='';this.value=value;onfocus=null;";
                txtnaration.Focus();

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

                //string STR = OWGCL.InstDateDiff(Session["EntryDate"].ToString(), txtinstdate.Text);
                string STR = OWGCL.InstMonthDiff(Session["EntryDate"].ToString(), TxtInstDate.Text);
                if (Convert.ToInt32(STR) >= 3)
                {

                    WebMsgBox.Show("Cheque no. " + TxtInstDate.Text + " date validity expired....!", this.Page);

                    TxtInstDate.Text = "";
                    TxtInstDate.Focus();
                    return;
                }
                if (Convert.ToDateTime(conn.ConvertDate(TxtInstDate.Text)) > Convert.ToDateTime(conn.ConvertDate(Session["EntryDate"].ToString())))
                {

                    WebMsgBox.Show("Instrument Post Dated...!", this.Page);

                    TxtInstDate.Text = "";
                    TxtInstDate.Focus();
                }
                else
                    txtamountt.Focus();
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

    protected void txtnaration_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtVoucherTypeno.Text == "1")
            {
                txtamountt.Focus();
            }
            else if (TxtVoucherTypeno.Text == "2")
            {
                TxtInstDate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Button click Events
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            // DIVPHOTO.Visible = false;
            string BRCD = Session["BRCD"].ToString();
            string MID = Session["MID"].ToString();
            string EntryDate = "";
            string custno = "";
            string SetNo = "";
            string PACMAC = "";
            string glcode = "";
            string CN = "", CD = "01/01/1990";
            string InstdAte = "";
            string PAYMAST = "CASHP";

            string REFERENCEID = "";
            REFERENCEID = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
            ViewState["RID"] = (Convert.ToInt32(REFERENCEID) + 1).ToString();

            EntryDate = TxtEntrydate.Text.ToString();
            PACMAC = conn.PCNAME().ToString();
            glcode = CurrentCls.getGlCode(BRCD, TxtProcode.Text.ToString());
            SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();

            if (TxtAccNo.Text != "")
            {
                custno = CurrentCls.GetAccountNo(TxtAccNo.Text.ToString(), TxtProcode.Text.ToString(), BRCD);
            }
            else
            {
                custno = "0";
                TxtAccNo.Text = "0";
            }

            double BAL, curbal;
            BAL = curbal = 0;
            BAL = Convert.ToDouble(TxtNewBalance.Text);
            curbal = Convert.ToDouble(txtamountt.Text);

            string Code = CR.CheckValidation(TxtProcode.Text, Session["BRCD"].ToString());
            if (Code == "N")
            {
                if ((Convert.ToInt32(glcode) < 100) && Convert.ToInt32(TxtProcode.Text) != 177)// && TxtGLCD.Text == "3")&& Convert.ToInt32(TxtProcode.Text)!=177 
                {
                    if (BAL < 0)
                    {
                        lblMessage.Text = "Sorry Insufficient Account Balance...!!";
                        TxtAccNo.Text = "";
                        TxtAccName.Text = "";
                        Clear();

                        ModalPopup.Show(this.Page);
                        return;
                    }
                }
            }
            CN = TxtInstruNo.Text != "" ? TxtInstruNo.Text : "";
            InstdAte = string.IsNullOrEmpty(TxtInstDate.Text) ? "01/01/1990" : TxtInstDate.Text;

            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), glcode, TxtProcode.Text.ToString(),
                TxtAccNo.Text.ToString(), txtnaration.Text, TxtVouchertype.Text, txtamountt.Text.ToString(), "2", "4", "CP", SetNo, CN, InstdAte, "0", "0", "1001",
                "0", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", PAYMAST, "0", TxtAccName.Text, ViewState["RID"].ToString(), txtToken.Text);

            if (resultint > 0)
            {
                string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());

                resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", cgl,
                          TxtAccNo.Text, txtnaration.Text, TxtVouchertype.Text, txtamountt.Text.ToString(), "1", "4", "CP", SetNo, CN, InstdAte, "0", "0", "1001",
                          "0", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", PAYMAST, "0", TxtAccName.Text, ViewState["RID"].ToString(), txtToken.Text);
                if (resultint > 0)
                {
                    if (resultint > 0)
                    {
                        grdAccDetails.DataSource = null;
                        grdAccDetails.DataBind();
                        TxtProcode.Focus();
                        lblMessage.Text = "Record Submitted Successfully With Recipt No :" + SetNo;
                        ModalPopup.Show(this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Cash_Payment _" + TxtProcode.Text + "_" + TxtAccNo.Text + "_" + txtamountt.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    }
                    BindGrid();
                    Clear();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string setno = objlink.CommandArgument;
            ViewState["SetNo"] = setno.ToString();
            CallUpdate();
            Clear();
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
            divAcc.Visible = false;
            divPhoto1.Visible = true;
            btnPrev.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
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

    protected void LnkReceipt_Click(object sender, EventArgs e)
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
            Session["densamt"] = dens[1].ToString();
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
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

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            string OpDate = OC.GetAccOpenDate(Session["BRCD"].ToString(), TxtProcode.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString());

            DT = GetAccStatDetails(OpDate);
            if (DT.Rows.Count > 0)
            {
                grdAccStatement.DataSource = DT;
                grdAccStatement.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('.bs-example-modal-lg').modal('show');</script>", false);

                divAcc.Visible = true;
                divPhoto1.Visible = false;
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

    #endregion

    #region Public Function

    public void NewWindows(string url)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + url + "', 'popup_window', 'width=600,height=250,left=50,top=50,resizable=no');", true);
    }

    public void BindGrid()
    {
        try
        {
            CurrentCls.Getinfotable(grdCashRct, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "CASHP");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void CallUpdate()
    {
        try
        {
            string RC = CheckStage();
            string sql;
            int result;

            if (RC != "1003")
            {
                result = CurrentCls.CancelCashpayment(ViewState["SetNo"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());
                if (result > 0)
                {
                    WebMsgBox.Show("Entry Canceled for Voucher No-" + ViewState["SetNo"].ToString() + "....", this.Page);
                    BindGrid();
                    Clear();
                    TxtProcode.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("The Voucher is already Authorized, Cannot delete!.....", this.Page);
                TxtProcode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public string CheckStage()
    {
        string RC = "";
        string ed = Session["EntryDate"].ToString();
        try
        {
            string sql = "select Stage from ALLVCR where SETNO='" + ViewState["SetNo"].ToString() + "' and EntryDate='" + conn.ConvertDate(ed) + "' and STAGE<>1004";
            RC = conn.sExecuteScalar(sql);
            return RC;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return RC;
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
            dt = GetData("select id,SignName,PhotoName,SignIMG,PhotoImg from  Imagerelation where BRCD='" + Session["BRCD"].ToString() + "' and CustNo=" + Txtcustno.Text + "");
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
                            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            if (y == 0)
                            {
                                image1.Src = "data:image/tif;base64," + base64String;
                            }
                            else if (y == 1)
                            {
                                image2.Src = "data:image/tif;base64," + base64String;
                            }
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

    public DataTable GetAccStatDetails(string FinDate)
    {
        try
        {
            DT = new DataTable();
            string[] DTF, DTT;

            DTF = FinDate.ToString().Split('/');
            DTT = Session["EntryDate"].ToString().Split('/');

            DT = CurrentCls.GetAccStatDetails(DTF[1].ToString(), DTT[1].ToString(), DTF[2].ToString(), DTT[2].ToString(), FinDate.ToString(), Session["EntryDate"].ToString(), TxtAccNo.Text.Trim().ToString(), TxtProcode.Text.Trim().ToString(), Session["BRCD"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string PanCard(string BRCD, string CustNo)
    {
        string PanNo = CR.GetPanNo(BRCD, CustNo);
        return PanNo;
    }

    public void Photo_Sign()
    {
        try
        {
            string FileName = "";
            DataTable dt = CC.ShowIMAGE(ViewState["CUSTNO"].ToString(), Session["BRCD"].ToString(), TxtAccNo.Text);
            if (dt.Rows.Count > 0)
            {
                int i = 0;
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

    public void GetTotalAmt()
    {
        try
        {
            TxtTotalCP.Text = CC.CRCPSUM("CR", "TOTAL", Session["BRCD"].ToString(), Session["EntryDate"].ToString());
            TxtTotalUnCp.Text = CC.CRCPSUM("CR", "UNAUTH", Session["BRCD"].ToString(), Session["EntryDate"].ToString());
            TxtTotCR.Text = CC.CRCPSUM("CP", "TOTAL", Session["BRCD"].ToString(), Session["EntryDate"].ToString());
            TxtTotalUnCr.Text = CC.CRCPSUM("CP", "UNAUTH", Session["BRCD"].ToString(), Session["EntryDate"].ToString());

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void Clear()
    {
        TxtProcode.Text = "";
        TxtProName.Text = "";
        TxtAccNo.Text = "";
        TxtAccName.Text = "";
        txtBalance.Text = "";
        txtamountt.Text = "";
        TxtNewBalance.Text = "";
        Txtcustno.Text = "";
        TxtVouchertype.Text = "";
        TxtVoucherTypeno.Text = "";
        TxtInstruNo.Text = "";
        TxtProcode.Focus();
        txtAccTypeName.Text = "";
        TxtSplInst.Text = "";
        TxtJointName.Text = "";
        txtnaration.Text = "";
        Div_Instdate.Visible = false;
        DIVINSTRNO.Visible = false;
    }

    #endregion

    #region Index Changed Events
    
    protected void grdOwgData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdCashRct.PageIndex = e.NewPageIndex;
            BindGrid();
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
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Cash_Payment _print_" + TxtProcode.Text + "_" + TxtAccNo.Text + "_" + txtamountt.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + Session["EntryDate"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&FN=V&rptname=RptReceiptPrint.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
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
    //ADDED BY ANKITA 08/02/2018 FOR UPDATE MOBILE NUMBER WHILE TRANSACTION
    protected void BtnMobUpld_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dt = new DataTable();
            dt = CC.getContct(Txtcustno.Text);
            if (dt.Rows.Count > 0)
            {
                TxtCustno1.Text = Txtcustno.Text;
                TxtBrcd1.Text = dt.Rows[0]["brcd"].ToString();
            }
            TxtMob1.Text = TxtMobile1.Text;
            TxtMob2.Text = TxtMobile2.Text;

            string Modal_Flag = "CNTCT";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#" + Modal_Flag + "').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnModlUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtMob1.Text != "")
            {
                if (TxtMob1.Text.Length < 10)
                {
                    WebMsgBox.Show("Enter 10 digit contact number..!!", this.Page);
                    return;
                }
            }
            if (TxtMob2.Text != "" && TxtMob2.Text != "0")
            {
                if (TxtMob2.Text.Length < 10)
                {
                    WebMsgBox.Show("Enter 10 digit contact number..!!", this.Page);
                    return;
                }
            }
            resultout = CC.insertContct(TxtCustno1.Text, TxtBrcd1.Text, TxtMob1.Text == "" ? "0" : TxtMob1.Text, TxtMob2.Text == "" ? "0" : TxtMob2.Text, Session["MID"].ToString());
            if (resultout > 0)
            {
                WebMsgBox.Show("Contact Number changed Successfully..!!", this.Page);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("location.reload();");
                sb.Append(@"</script>");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                //lblMessage.Text = "Contact Added Successfully..!!";
                //ModalPopup.Show(this.Page);
                //BtnModlUpdate.Attributes.Add("data-dismiss", "modal");
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void getMobile()
    {
        try
        {
            DT = CC.getMobiles(Txtcustno.Text);
            if (DT.Rows.Count > 0)
            {
                TxtMobile1.Text = DT.Rows[0]["Mobile1"].ToString();
                TxtMobile2.Text = DT.Rows[0]["Mobile2"].ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}