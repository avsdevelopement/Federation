using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmLoanDisburtment : System.Web.UI.Page
{
    ClsAccopen AO = new ClsAccopen();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLoanInfo LI = new ClsLoanInfo();
    ClsLoanDisburstMent LD = new ClsLoanDisburstMent();
    ClsAuthorized AT = new ClsAuthorized();
    ClsOpenClose OC = new ClsOpenClose();
    ClsAccopen accop = new ClsAccopen();
    DataTable DT = new DataTable();
    ClsCashPayment CurrentCls = new ClsCashPayment();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    string ST = "", AccStatus = "";
    double DisbAmount = 0;
    int Result;
    public static int CNN = 0;

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

                ViewState["SETNO"] = "";
                autoglname.ContextKey = Session["BRCD"].ToString();
                LD.DelAllRecTable(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                BD.BindPayment(ddlPayType, "1");
                BindGrid1();
                TxtPtype.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtPtype_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL1;

            //Added By Amol on 22092017 as per ambika mam instruction
            if (BD.GetProdOperate(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString()).ToString() != "3")
            {
                GL1 = BD.GetAccTypeGL(TxtPtype.Text, Session["BRCD"].ToString());
                if (GL1 != null)
                {
                    string[] GL = GL1.Split('_');
                    TxtPname.Text = GL[0].ToString();
                    ViewState["GL"] = GL[1].ToString();
                    AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtPtype.Text + "_" + ViewState["GL"].ToString();
                    //Added By AmolB ON 20170131 for Check GlgGroup
                    ViewState["GLGroup"] = LD.CheckGlGroup(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString());
                    TxtAccNo.Focus();
                }
                else
                {
                    WebMsgBox.Show("Enter valid product code!....", this.Page);
                    TxtPtype.Focus();
                    TxtPtype.Text = "";
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
            string CUNAME = TxtPname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                //Added By Amol on 22092017 as per ambika mam instruction
                if (BD.GetProdOperate(Session["BRCD"].ToString(), string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString()).ToString() != "3")
                {
                    TxtPname.Text = custnob[0].ToString();
                    TxtPtype.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                    string[] GL = BD.GetAccTypeGL(TxtPtype.Text, Session["BRCD"].ToString()).Split('_');
                    ViewState["GL"] = GL[1].ToString();
                    AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtPtype.Text.ToString() + "_" + ViewState["GL"];
                    //Added By AmolB ON 20170131 for Check GlgGroup
                    ViewState["GLGroup"] = LD.CheckGlGroup(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString());
                    TxtAccNo.Focus();
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

            AT = BD.Getstage1(TxtAccNo.Text, Session["BRCD"].ToString(), TxtPtype.Text);
            if (AT != null)
            {
                if (AT != "1003")
                {
                    lblMessage.Text = "Sorry Customer Not Authorise...!!";
                    ModalPopup.Show(this.Page);
                }
                else
                {
                    //Added By AmolB ON 20170131 for Loan Against Gold
                    if (ViewState["GLGroup"].ToString() == "LAG")
                    {
                        Result = LD.CheckRecords(Session["BRCD"].ToString(), TxtPtype.Text, TxtAccNo.Text);

                        if (Result < 1)
                        {
                            TxtPtype.Text = "";
                            TxtPname.Text = "";
                            TxtAccNo.Text = "";

                            lblMessage.Text = "First Enter Ornament Details...!!";
                            ModalPopup.Show(this.Page);
                            return;
                        }
                    }

                      ST = LD.CheckAccount(TxtAccNo.Text, TxtPtype.Text, Session["BRCD"].ToString());
                    if (ST != null)
                    {
                        TxtCustName.Text = AO.Getcustname(ST.ToString());
                        txtCustNo1.Text = ST.ToString();

                        DT = LD.GetDisburstAMT(TxtPtype.Text, TxtAccNo.Text, Session["EntryDate"].ToString(), Session["BRCD"].ToString());
                        if (Convert.ToDouble(DT.Rows[0]["Limit"].ToString()) == -1)
                        {
                            WebMsgBox.Show("Sorry Not having Disburstment Amount...!!", this.Page);
                            ClearData();
                            TxtPtype.Focus();
                            return;
                        }
                        else
                        {
                            ShowLoanDetails();
                            string LoanCat = LD.GetLoancat(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString());

                            if (LoanCat != null && LoanCat == "CCOD")
                            {
                                string[] TD = Session["EntryDate"].ToString().Split('/');
                                TxtBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtPtype.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString()).ToString();
                                TxtTotalBal.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtPtype.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString()).ToString();

                                DisbAmount = Convert.ToDouble(Convert.ToDouble(txtLimitAmt.Text.Trim().ToString()) - Math.Abs(Convert.ToDouble(TxtTotalBal.Text.Trim().ToString())));
                                TxtDSAmt.Text = (DisbAmount > 0 ? DisbAmount : 0).ToString();
                                txtDrAmount.Text = Convert.ToDouble(TxtDSAmt.Text.Trim().ToString()).ToString();
                                hdnLimitAmt.Value = Convert.ToDouble(TxtDSAmt.Text.Trim().ToString()).ToString();
                            }
                            else
                            {
                                string[] TD = Session["EntryDate"].ToString().Split('/');
                                TxtBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtPtype.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString()).ToString();
                                TxtTotalBal.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtPtype.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString()).ToString();

                                DisbAmount = Convert.ToDouble(Convert.ToDouble(txtLimitAmt.Text.Trim().ToString()) - Math.Abs(Convert.ToDouble(TxtTotalBal.Text.Trim().ToString())));
                                TxtDSAmt.Text = (DisbAmount > 0 ? DisbAmount : 0).ToString();
                                txtDrAmount.Text = Convert.ToDouble(TxtDSAmt.Text.Trim().ToString()).ToString();
                                hdnLimitAmt.Value = Convert.ToDouble(TxtDSAmt.Text.Trim().ToString()).ToString();
                            }
                            TxtDSAmt.Focus();
                            DivPayment.Visible = true;
                        }
                    }
                    else
                    {
                        TxtAccNo.Focus();
                        WebMsgBox.Show("Please Enter valid Account Number.. Account Not Exist...!!", this.Page);
                        return;
                    }
                    BindTempGrid();
                }
            }
            else
            {
                WebMsgBox.Show("Enter valid account number...!!", this.Page);
                TxtAccNo.Focus();
                TxtAccNo.Text = "";
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
            string AT = "";

            string CUNAME = TxtCustName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtCustName.Text = custnob[0].ToString();
                TxtAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
            }

            AT = BD.Getstage1(TxtAccNo.Text, Session["BRCD"].ToString(), TxtPtype.Text);
            if (AT != null)
            {
                if (AT != "1003")
                {
                    lblMessage.Text = "Sorry Customer Not Authorise...!!";
                    ModalPopup.Show(this.Page);
                }
                else
                {
                    //Added By AmolB ON 20170131 for Loan Against Gold
                    if (ViewState["GLGroup"].ToString() == "LAG")
                    {
                        Result = LD.CheckRecords(Session["BRCD"].ToString(), TxtPtype.Text, TxtAccNo.Text);

                        if (Result < 1)
                        {
                            TxtPtype.Text = "";
                            TxtPname.Text = "";
                            TxtAccNo.Text = "";
                            TxtCustName.Text = "";

                            lblMessage.Text = "First Enter Ornament Details...!!";
                            ModalPopup.Show(this.Page);
                            return;
                        }
                    }

                    ST = LD.CheckAccount(TxtAccNo.Text, TxtPtype.Text, Session["BRCD"].ToString());
                    if (ST != null)
                    {
                        TxtCustName.Text = AO.Getcustname(ST.ToString());
                        txtCustNo1.Text = ST.ToString();

                        DT = LD.GetDisburstAMT(TxtPtype.Text, TxtAccNo.Text, Session["EntryDate"].ToString(), Session["BRCD"].ToString());
                        if (Convert.ToDouble(DT.Rows[0]["Limit"].ToString()) == -1)
                        {
                            WebMsgBox.Show("Sorry Not having Disburstment Amount...!!", this.Page);
                            ClearData();
                            TxtPtype.Focus();
                            return;
                        }
                        else
                        {
                            ShowLoanDetails();
                            string LoanCat = LD.GetLoancat(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString());

                            if (LoanCat != null && LoanCat == "CCOD")
                            {
                                string[] TD = Session["EntryDate"].ToString().Split('/');
                                TxtBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtPtype.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString()).ToString();
                                TxtTotalBal.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtPtype.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString()).ToString();

                                DisbAmount = Convert.ToDouble(Convert.ToDouble(txtLimitAmt.Text.Trim().ToString()) - Math.Abs(Convert.ToDouble(TxtTotalBal.Text.Trim().ToString())));
                                TxtDSAmt.Text = (DisbAmount > 0 ? DisbAmount : 0).ToString();
                                txtDrAmount.Text = Convert.ToDouble(TxtDSAmt.Text.Trim().ToString()).ToString();
                                hdnLimitAmt.Value = Convert.ToDouble(TxtDSAmt.Text.Trim().ToString()).ToString();
                            }
                            else
                            {
                                string[] TD = Session["EntryDate"].ToString().Split('/');
                                TxtBalance.Text = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtPtype.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString()).ToString();
                                TxtTotalBal.Text = OC.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), TxtPtype.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString()).ToString();

                                DisbAmount = Convert.ToDouble(Convert.ToDouble(txtLimitAmt.Text.Trim().ToString()) - Math.Abs(Convert.ToDouble(TxtTotalBal.Text.Trim().ToString())));
                                TxtDSAmt.Text = (DisbAmount > 0 ? DisbAmount : 0).ToString();
                                txtDrAmount.Text = Convert.ToDouble(TxtDSAmt.Text.Trim().ToString()).ToString();
                                hdnLimitAmt.Value = Convert.ToDouble(TxtDSAmt.Text.Trim().ToString()).ToString();
                            }
                            TxtDSAmt.Focus();
                            DivPayment.Visible = true;
                        }
                    }
                    else
                    {
                        TxtAccNo.Focus();
                        WebMsgBox.Show("Please Enter valid Account Number.. Account Not Exist...!!", this.Page);
                        return;
                    }
                    BindTempGrid();
                }
            }
            else
            {
                WebMsgBox.Show("Enter valid account number...!!", this.Page);
                TxtAccNo.Focus();
                TxtAccNo.Text = "";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ShowLoanDetails()
    {
        try
        {
            ViewState["CT"] = DT.Rows[0]["CustNo"].ToString();
            txtLimitAmt.Text = DT.Rows[0]["Limit"].ToString() == "" ? "0" : DT.Rows[0]["Limit"].ToString();
            txtIntRate.Text = DT.Rows[0]["IntRate"].ToString();
            txtAccOpenDate.Text = DT.Rows[0]["SancDate"].ToString();
            txtInstAmt.Text = DT.Rows[0]["Installment"].ToString();
            txtPeriod.Text = DT.Rows[0]["Period"].ToString();
            txtDueDate.Text = DT.Rows[0]["DueDate"].ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ClearData()
    {
        TxtPtype.Text = "";
        TxtPname.Text = "";
        TxtAccNo.Text = "";
        TxtCustName.Text = "";
        txtLimitAmt.Text = "";
        TxtDSAmt.Text = "";
        TxtBalance.Text = "";
        TxtTotalBal.Text = "";

        txtProdType1.Text = "";
        txtProdName1.Text = "";
        TxtAccNo1.Text = "";
        TxtAccName1.Text = "";
        TxtChequeNo.Text = "";
        TxtChequeDate.Text = "";
        txtAmount.Text = "";
        TxtChequeDate.Text = Session["EntryDate"].ToString();
        ddlPayType.SelectedIndex = 0;
        TxtPtype.Focus();
    }

    protected void TxtDSAmt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtDrAmount.Text = Convert.ToDouble(TxtDSAmt.Text.Trim().ToString()).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindTempGrid()
    {
        try
        {
            Result = LD.BindTemp(GrsLoanInfo, Session["MID"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), TxtPtype.Text, TxtAccNo.Text);
            if (Result > 0)
            {
                double AMT = LD.GetAMT(Session["MID"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), TxtPtype.Text, TxtAccNo.Text);
                if (AMT == Convert.ToDouble(TxtDSAmt.Text))
                {
                    btnSubmit.Visible = true;
                }
                else
                {
                    btnSubmit.Visible = false;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void GrsLoanInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrsLoanInfo.PageIndex = e.NewPageIndex;
            BindTempGrid();
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

    protected void BtnReceipt_Click(object sender, EventArgs e)
    {
        try
        {
            string Setno = ViewState["SETNO"].ToString();
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Loan_Disbursement_Receipt _" + TxtPtype.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + Session["EntryDate"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&rptname=RptReceiptPrint.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            BtnReceipt.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearData();
    }

    protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPayType.SelectedValue.ToString() == "0")
        {
            Transfer.Visible = false;
            Transfer1.Visible = false;
            DivAmount.Visible = false;
        }
        else if (ddlPayType.SelectedValue.ToString() == "1")
        {
            ViewState["PAYTYPE"] = "CASH";
            Transfer.Visible = false;
            Transfer1.Visible = false;
            DivAmount.Visible = true;
            txtNarration.Text = "By Cash";

            Clear();
            txtAmount.Focus();
        }
        else if (ddlPayType.SelectedValue.ToString() == "2")
        {
            ViewState["PAYTYPE"] = "TRANSFER";
            Transfer.Visible = true;
            Transfer1.Visible = false;
            DivAmount.Visible = true;
            txtNarration.Text = "By TRF";

            autoglname1.ContextKey = Session["BRCD"].ToString();
            Clear();
            txtProdType1.Focus();
        }
        else if (ddlPayType.SelectedValue.ToString() == "4")
        {
            ViewState["PAYTYPE"] = "CHEQUE";
            Transfer.Visible = true;
            Transfer1.Visible = true;
            DivAmount.Visible = true;
            txtNarration.Text = "By TRF";

            autoglname1.ContextKey = Session["BRCD"].ToString();
            Clear();
            txtProdType1.Focus();
        }
        else
        {
            Clear();
            Transfer.Visible = false;
            Transfer1.Visible = false;
        }
    }

    protected void Clear()
    {
        try
        {
            txtProdType1.Text = "";
            txtProdName1.Text = "";
            TxtAccNo1.Text = "";
            TxtAccName1.Text = "";
            TxtChequeNo.Text = "";
            TxtChequeDate.Text = "";
            TxtChequeDate.Text = Session["EntryDate"].ToString();
            txtProdType1.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        finally
        {
            txtProdType1.Focus();
        }

    }

    protected void txtProdType1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;

            //Added By Amol on 22092017 as per ambika mam instruction
            if (BD.GetProdOperate(Session["BRCD"].ToString(), txtProdType1.Text.Trim().ToString()).ToString() != "3")
            {
                AC1 = LD.Getaccno(txtProdType1.Text, Session["BRCD"].ToString());

                if (AC1 != null)
                {
                    string[] AC = AC1.Split('_'); ;
                    ViewState["GLCODE1"] = AC[0].ToString();
                    txtProdName1.Text = AC[1].ToString();
                    AutoAccname1.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType1.Text + "_" + ViewState["GLCODE1"].ToString();

                    if (Convert.ToInt32(ViewState["GLCODE1"].ToString() == "" ? "0" : ViewState["GLCODE1"].ToString()) > 100)
                    {
                        TxtAccNo1.Text = "";
                        TxtAccName1.Text = "";

                        TxtAccNo1.Text = txtProdType1.Text.ToString();
                        TxtAccName1.Text = txtProdName1.Text.ToString();
                        txtCustNo2.Text = "0";

                        TxtChequeNo.Focus();
                    }
                    else
                    {
                        TxtAccNo1.Text = "";
                        TxtAccName1.Text = "";

                        TxtAccNo1.Focus();
                    }
                }
                else
                {
                    WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                    txtProdType1.Text = "";
                    txtProdName1.Text = "";
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";

                    txtProdType1.Focus();
                }
            }
            else
            {
                txtProdType1.Text = "";
                txtProdName1.Text = "";
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

    protected void txtProdName1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtProdName1.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                //Added By Amol on 22092017 as per ambika mam instruction
                if (BD.GetProdOperate(Session["BRCD"].ToString(), string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString()).ToString() != "3")
                {
                    txtProdName1.Text = custnob[0].ToString();
                    txtProdType1.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                    string[] AC = LD.Getaccno(txtProdType1.Text, Session["BRCD"].ToString()).Split('_');
                    ViewState["GLCODE1"] = AC[0].ToString();
                    AutoAccname1.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType1.Text;

                    if (Convert.ToInt32(ViewState["GLCODE1"].ToString() == "" ? "0" : ViewState["GLCODE1"].ToString()) > 100)
                    {
                        TxtAccNo1.Text = "";
                        TxtAccName1.Text = "";

                        TxtAccNo1.Text = txtProdType1.Text.ToString();
                        TxtAccName1.Text = txtProdName1.Text.ToString();
                        txtCustNo2.Text = "0";

                        TxtChequeNo.Focus();
                    }
                    else
                    {
                        TxtAccNo1.Text = "";
                        TxtAccName1.Text = "";
                        txtCustNo2.Text = "";

                        TxtAccNo1.Focus();
                    }
                }
                else
                {
                    txtProdType1.Text = "";
                    txtProdName1.Text = "";
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

    protected void TxtAccNo1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";
            AT = BD.Getstage1(TxtAccNo1.Text, Session["BRCD"].ToString(), txtProdType1.Text);
            if (AT != null)
            {
                if (AT != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise.........!!";
                    ModalPopup.Show(this.Page);
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";
                    TxtAccNo1.Focus();
                }
                else
                {
                    AccStatus = LD.GetAccStatus(Session["BRCD"].ToString(), txtProdType1.Text, TxtAccNo1.Text);
                    if (AccStatus == "1" || AccStatus == "4")
                    {
                        DT = new DataTable();
                        DT = LD.GetCustName(txtProdType1.Text, TxtAccNo1.Text, Session["BRCD"].ToString());
                        if (DT.Rows.Count > 0)
                        {
                            string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                            TxtAccName1.Text = CustName[0].ToString();
                            txtCustNo2.Text = CustName[1].ToString();

                            txtAmount.Focus();
                        }
                    }
                    else if (AccStatus == "3")
                    {
                        TxtAccNo1.Text = "";
                        lblMessage.Text = "Account is closed...!!";
                        ModalPopup.Show(this.Page);
                        TxtAccNo1.Focus();
                        return;
                    }
                    else
                    {
                        TxtAccNo1.Text = "";
                        lblMessage.Text = "Enter valid account number...!!";
                        ModalPopup.Show(this.Page);
                        TxtAccNo1.Focus();
                        return;
                    }
                }
            }
            else
            {
                lblMessage.Text = "Enter valid account number...!!";
                ModalPopup.Show(this.Page);
                TxtAccNo1.Text = "";
                TxtAccNo1.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAccName1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtAccName1.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtAccName1.Text = custnob[0].ToString();
                TxtAccNo1.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                txtCustNo2.Text = custnob[2].ToString() == "" ? "0" : custnob[2].ToString();

                txtAmount.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //Check product code is entered or not
            if (TxtPtype.Text.Trim().ToString() == "")
            {
                lblMessage.Text = "Enter product code first...!!";
                ModalPopup.Show(this.Page);
                return;
            }

            //Check account number is entered or not
            if (TxtAccNo.Text.Trim().ToString() == "")
            {
                lblMessage.Text = "Enter account number first....!!";
                ModalPopup.Show(this.Page);
                return;
            }

            //Check payment mode is selected or not
            if (ddlPayType.SelectedValue.ToString() == "0")
            {
                lblMessage.Text = "Select payment mode first...!!";
                ModalPopup.Show(this.Page);
                return;
            }

            //Check credit amount is entered or not
            if (txtAmount.Text.Trim().ToString() == "")
            {
                lblMessage.Text = "Enter amount first...!!";
                ModalPopup.Show(this.Page);
                return;
            }

            //Check Amount is grater than zero or not
            if (Convert.ToDouble(txtAmount.Text.Trim().ToString() == "" ? "0" : txtAmount.Text.Trim().ToString()) > 0.00)
            {
                double DisAmt = Convert.ToDouble(txtAmount.Text.Trim().ToString() == "" ? "0" : txtAmount.Text.Trim().ToString());
                double LimitAmt = (Convert.ToDouble(txtDrAmount.Text.Trim().ToString() == "" ? "0" : txtDrAmount.Text.Trim().ToString()) - Convert.ToDouble(txtCrAmount.Text.Trim().ToString() == "" ? "0" : txtCrAmount.Text.Trim().ToString()));

                if (DisAmt > LimitAmt)
                {
                    txtAmount.Text = LimitAmt.ToString();
                    lblMessage.Text = "Enter proper amount first...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }

            //Submit record into temp table (Avs_TempLoanDisb) 
            if (ddlPayType.SelectedValue.ToString() == "1")
            {
                string Parti2 = "From " + TxtPtype.Text.Trim().ToString() + "/" + TxtAccNo.Text.Trim().ToString() + "";

                Result = LD.InsertIntoTable(Session["EntryDate"].ToString(), Session["BRCD"].ToString(), "0", "By Cash", "99", "99", "0", txtNarration.Text.ToString(), Parti2, txtAmount.Text.Trim().ToString(), "1", "4", "CP", "", "1900-01-01", Session["MID"].ToString());

                if (Result > 0)
                {
                    lblMessage.Text = "Successfully Added...!!";
                    ModalPopup.Show(this.Page);
                    BindGrid1();
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Loan_Disbursement_submit1 _" + TxtPtype.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());


                }
            }
            else if (ddlPayType.SelectedValue.ToString() == "2")
            {
                string Parti2 = "From " + TxtPtype.Text.Trim().ToString() + "/" + TxtAccNo.Text.Trim().ToString() + "";

                Result = LD.InsertIntoTable(Session["EntryDate"].ToString(), Session["BRCD"].ToString(), txtCustNo2.Text.Trim().ToString(), TxtAccName1.Text.Trim().ToString(), ViewState["GLCODE1"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), txtNarration.Text.ToString(), Parti2, txtAmount.Text.Trim().ToString(), "1", "7", "TR", "", "1900-01-01", Session["MID"].ToString());

                if (Result > 0)
                {
                    lblMessage.Text = "Successfully Added...!!";
                    ModalPopup.Show(this.Page);
                    BindGrid1();
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Loan_Disbursement_submit2 _" + TxtPtype.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }
            }
            else if (ddlPayType.SelectedValue.ToString() == "4")
            {
                string Parti2 = "From " + TxtPtype.Text.Trim().ToString() + "/" + TxtAccNo.Text.Trim().ToString() + "";

                Result = LD.InsertIntoTable(Session["EntryDate"].ToString(), Session["BRCD"].ToString(), txtCustNo2.Text.Trim().ToString() == "" ? "0" : txtCustNo2.Text.Trim().ToString(), TxtAccName1.Text.Trim().ToString(), ViewState["GLCODE1"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), txtNarration.Text.ToString(), Parti2, txtAmount.Text.Trim().ToString(), "1", "6", "TR", TxtChequeNo.Text.Trim(), TxtChequeDate.Text.Trim().ToString(), Session["MID"].ToString());

                if (Result > 0)
                {
                    lblMessage.Text = "Successfully Added...!!";
                    ModalPopup.Show(this.Page);
                    BindGrid1();
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Loan_Disbursement_submit4 _" + TxtPtype.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }
            }

            if (Result > 0)
            {
                TxtDSAmt.Enabled = false;
                Getinfo();
                BindGrid();
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
            bool Amol = true;
            string FSet = "";

            //Check product code is entered or not
            if (TxtPtype.Text.Trim().ToString() == "")
            {
                lblMessage.Text = "Enter product code first...!!";
                ModalPopup.Show(this.Page);
                return;
            }

            //Check account number is entered or not
            if (TxtAccNo.Text.Trim().ToString() == "")
            {
                lblMessage.Text = "Enter account number first....!!";
                ModalPopup.Show(this.Page);
                return;
            }

            if (Convert.ToDouble(txtDiffAmount.Text) == 0.00)
            {
                //Get All Transaction From Temporary Table
                DataTable DT = new DataTable();
                DT = LD.GetTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                if (DT.Rows.Count > 0)
                {
                    //Generate Reference Number Here
                    string RefId = "";
                    RefId = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");

                    ViewState["RefId"] = (Convert.ToInt32(RefId) + 1).ToString();

                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        //Generate Set Number Here
                        ST = "";
                        ST = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
                        if (Amol == true)
                        {
                            FSet = ST;
                            Amol = false;
                        }

                        //Insert Data to Original Table Here for Debit
                        Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString(), TxtPtype.Text.Trim().ToString(), TxtAccNo.Text.Trim().ToString(), "", "TRF To " + DT.Rows[i]["SubGlCode"].ToString() + "/" + DT.Rows[i]["AccNo"].ToString(), DT.Rows[i]["Amount"].ToString(), "2", DT.Rows[i]["Activity"].ToString(), DT.Rows[i]["PmtMode"].ToString(), ST, "", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanDisb", txtCustNo1.Text.Trim().ToString(), TxtCustName.Text.Trim().ToString(), ViewState["RefId"].ToString(), "0");

                        if (Result > 0)
                        {
                            //Insert Data to Original Table Here for Credit
                            Result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[i]["GlCode"].ToString(), DT.Rows[i]["SubGlCode"].ToString(), DT.Rows[i]["AccNo"].ToString(), DT.Rows[i]["Particulars"].ToString(), DT.Rows[i]["Particulars2"].ToString(), DT.Rows[i]["Amount"].ToString(), "1", DT.Rows[i]["Activity"].ToString(), DT.Rows[i]["PmtMode"].ToString(), ST, DT.Rows[i]["InstNo"].ToString(), DT.Rows[i]["InstDate"].ToString(), "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "LoanDisb", DT.Rows[i]["CustNo"].ToString(), DT.Rows[i]["CustName"].ToString(), ViewState["RefId"].ToString(), "0");
                        }
                    }
                }

                //Delete All Record From Table
                LD.DelAllRecTable(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                if (Result > 0)
                {
                    ClearAll();
                    BindGrid1();
                    lblMessage.Text = "Transfer Seccessfully With Set No : '" + FSet + "' To '" + ST + "' ...!!";
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Loan_Disbursement_transfer _'" + FSet + "' To '" + ST + "'" + TxtPtype.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    DivPayment.Visible = false;
                    btnPost.Enabled = false;
                    TxtDSAmt.Enabled = true;
                    grdvoucher.Visible = false;
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

    protected void lnkbtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string id = objlink.CommandArgument;

            Result = LD.DelRecTable(id, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (Result > 0)
            {
                lblMessage.Text = "Record Deleted Successfully...!!";
                ModalPopup.Show(this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Loan_Disbursement_del _" + TxtPtype.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());


            }

            BindGrid();
            Getinfo();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void Getinfo()
    {
        try
        {
            DataTable DT = new DataTable();
            DT = LD.GetCreditAmt(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (DT.Rows.Count > 0)
            {
                double CR, DR;
                CR = DR = 0;
                CR = Convert.ToDouble(DT.Rows[0]["CREDIT"].ToString());
                DR = Convert.ToDouble(txtDrAmount.Text.Trim().ToString());

                txtCrAmount.Text = Convert.ToDouble(DT.Rows[0]["CREDIT"].ToString()).ToString();
                txtDiffAmount.Text = (CR - DR).ToString();

                if (CR == DR)
                {
                    btnPost.Enabled = true;
                    btnPost.Focus();
                }
                else
                {
                    btnPost.Enabled = false;
                    ddlPayType.Focus();
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
            int RC = LD.Getinfotable(grdvoucher, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearText()
    {
        ddlPayType.SelectedIndex = 0;
        txtProdType1.Text = "";
        txtProdName1.Text = "";
        TxtAccNo1.Text = "";
        TxtAccName1.Text = "";
        txtCustNo2.Text = "";

        TxtChequeNo.Text = "";
        TxtChequeDate.Text = Session["EntryDate"].ToString();
        txtNarration.Text = "";
        txtAmount.Text = "";

        Transfer.Visible = false;
        Transfer1.Visible = false;
        DivAmount.Visible = false;
    }

    public void ClearAll()
    {
        try
        {
            TxtPtype.Text = "";
            TxtPname.Text = "";
            TxtAccNo.Text = "";
            TxtCustName.Text = "";
            txtCustNo1.Text = "";

            TxtDSAmt.Text = "";
            txtLimitAmt.Text = "";
            txtIntRate.Text = "";
            txtAccOpenDate.Text = "";

            txtInstAmt.Text = "";
            txtPeriod.Text = "";
            txtDueDate.Text = "";
            TxtBalance.Text = "";
            TxtTotalBal.Text = "";

            txtDrAmount.Text = "";
            txtCrAmount.Text = "";
            txtDiffAmount.Text = "";

            ddlPayType.SelectedIndex = 0;
            txtProdType1.Text = "";
            txtProdName1.Text = "";
            TxtAccNo1.Text = "";
            TxtAccName1.Text = "";
            txtCustNo2.Text = "";

            TxtChequeNo.Text = "";
            TxtChequeDate.Text = Session["EntryDate"].ToString();
            txtNarration.Text = "";
            txtAmount.Text = "";
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
            CurrentCls.Getinfotable(grdCashRct, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "LOANDISB");
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
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Loan_Disbursement_Print _" + TxtPtype.Text + "_" + TxtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

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
}