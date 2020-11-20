using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmAVS5028 : System.Web.UI.Page
{
    ClsCustomerDetails CD = new ClsCustomerDetails();
    ClsInsertTrans ITrans = new ClsInsertTrans();
    ClsMultiVoucher MV = new ClsMultiVoucher();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsAuthorized AT = new ClsAuthorized();
    DbConnection conn = new DbConnection();
    ClsAccopen accop = new ClsAccopen();
    ClsLoanInfo LI = new ClsLoanInfo();
    ClsAVS5028 CLS = new ClsAVS5028();
    DataTable DT1 = new DataTable();
    DataTable DT = new DataTable();
    ClsSurity SD = new ClsSurity();
    DataTable dt = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsCommon CC = new ClsCommon();

    string FL = "";

    int Result = 0, Resacc = 0, resultint = 0, SrNum = 0;
    string SrNumber = "", intr = "", penalint = "";
    string SetNo1 = "", SetNo2 = "", CustName = "";
    int GlCode = 0, LoanGlCode = 0, PlAccNo = 0;
    int NetPaid = 0, IntCalType = 0, resultout = 0;
    int BrCode = 0, AccNo = 0, CustNo = 0;
    static int CN = 0;

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
                autoglname.ContextKey = Session["BRCD"].ToString();
                TxtBrcd.Text = Session["BRCD"].ToString();
                TxtBrcdName.Text = AST.GetBranchName(TxtBrcd.Text);
                BD.BindPayment(ddlPayType, "1");
                CN = 0;
                BtnBondCr.Enabled = true;

                //Delete data From Tempprary Table (Avs_TempMultiTransfer) Here
                MV.DelAllRecTable(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                if (Request.QueryString["AppNo"].ToString() != null)
                {
                    TxtAppNo.Text = Request.QueryString["AppNo"].ToString();
                    TxtPtype.Text = Request.QueryString["GL"].ToString();
                    BindProductName();
                    BindDetails();
                }
                else
                {
                    TxtPtype.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void TxtBrcd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtBrcdName.Text = AST.GetBranchName(TxtBrcd.Text);
            TxtPtype.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void TxtPtype_TextChanged(object sender, EventArgs e)
    {
        BindProductName();
    }

    protected void TxtPname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] custnob = TxtPname.Text.Split('_');
            if (custnob.Length > 1)
            {
                TxtPtype.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                BindProductName();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void BindProductName()
    {
        try
        {
            DT = new DataTable();
            DT = CLS.GetProdDetails(Session["BRCD"].ToString(), TxtPtype.Text.ToString());
            if (DT.Rows.Count > 0)
            {
                if (Convert.ToString(DT.Rows[0]["UnOperate"].ToString()) != "3")
                {
                    if (Convert.ToString(DT.Rows[0]["GlName"].ToString()) != "")
                    {
                        ViewState["GLCODE"] = Convert.ToString(DT.Rows[0]["GlCode"].ToString()).ToString();
                        TxtPtype.Text = Convert.ToString(DT.Rows[0]["SubGlCode"].ToString()).ToString();
                        TxtPname.Text = Convert.ToString(DT.Rows[0]["GlName"].ToString()).ToString();
                        ViewState["IntApp"] = DT.Rows[0]["Int_App"].ToString();

                        TxtAppNo.Focus();
                    }
                    else
                    {
                        TxtPtype.Text = "";
                        TxtPname.Text = "";
                        TxtPtype.Focus();
                        WebMsgBox.Show("Enter valid product code ...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    TxtPtype.Text = "";
                    TxtPname.Text = "";
                    TxtPtype.Focus();
                    WebMsgBox.Show("Agent is not operating ...!!", this.Page);
                    return;
                }
            }
            else
            {
                TxtPtype.Text = "";
                TxtPname.Text = "";
                TxtPtype.Focus();
                WebMsgBox.Show("Enter valid product code ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAppNo_TextChanged(object sender, EventArgs e)
    {
        BindDetails();
    }

    public void BindDetails()
    {
        try
        {
            dt = CLS.GetData(TxtBrcd.Text.Trim().ToString(), TxtPtype.Text.Trim().ToString(), TxtAppNo.Text.Trim().ToString());
            if (dt.Rows.Count > 0)
            {
                TxtDeduct.Text = CLS.GetDeductAmt(TxtBrcd.Text.Trim().ToString(), dt.Rows[0]["CUSTNO"].ToString(), TxtAppNo.Text.Trim().ToString());
                TxtLoanSanc.Text = dt.Rows[0]["SANCITONAMOUNT"].ToString() == "" ? "0" : dt.Rows[0]["SANCITONAMOUNT"].ToString();
                TxtNetPaid.Text = ((Convert.ToInt32(TxtLoanSanc.Text.Trim().ToString())) - (Convert.ToInt32(TxtDeduct.Text.Trim().ToString() == "" ? "0" : TxtDeduct.Text.Trim().ToString()))).ToString();
                TxtCustno.Text = dt.Rows[0]["CUSTNO"].ToString();
                Session["CustNo"] = dt.Rows[0]["CUSTNO"].ToString();
                ViewState["InstType"] = Convert.ToString(dt.Rows[0]["InstType"].ToString());

                DT1 = CD.GetCustName(TxtCustno.Text.Trim().ToString());
                if (DT1.Rows.Count > 0)
                    TxtCustname.Text = DT1.Rows[0]["CUSTNAME"].ToString();

                TxtMemNo.Text = dt.Rows[0]["MEMNO"].ToString();
                TxtBondNo.Text = dt.Rows[0]["BONDNO"].ToString();
                ViewState["PERIOD"] = string.IsNullOrEmpty(dt.Rows[0]["PERIOD"].ToString()) ? "0" : dt.Rows[0]["PERIOD"].ToString();
                ViewState["INSTALLMENT"] = dt.Rows[0]["INSTMANUAL"].ToString();
                ViewState["INSTDATE"] = conn.GetNextMonthStartDate(conn.ConvertDate(Session["EntryDate"].ToString()));

                CD.GetAccountInfo(GrdDirectLiab, Session["BRCD"].ToString(), TxtCustno.Text, Session["EntryDate"].ToString(), "DL");
                CLS.GetFromSurity(GrdFromSurity, Session["BRCD"].ToString(), TxtCustno.Text, TxtPtype.Text, Session["EntryDate"].ToString(), "BOND");
            }
            ddlPayType.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void BtnBondCr_Click(object sender, EventArgs e)
    {
        try
        {
            string[] ROI = LI.GetROI(TxtBrcd.Text, TxtPtype.Text).Split('_');
            intr = ROI[0].ToString();
            penalint = ROI[1].ToString();

            if (Convert.ToString(CLS.GetParameter(Session["BRCD"].ToString(), "AccNoIncrese")) == "N")
            {
                //  Create account as it is customer no as account no
                if (CLS.CheckExists(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), TxtPtype.Text.ToString(), TxtCustno.Text.ToString()) == "0")
                {
                    //  If account not exists then create new account (same as custno as accno)
                    Resacc = CLS.insert("N", Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), TxtPtype.Text, TxtCustno.Text, Session["EntryDate"].ToString(), Session["MID"].ToString(), "1", "1", "2", "1003");
                    ViewState["ACCNO"] = Resacc.ToString();

                    if (Resacc > 0)
                    {
                        CLS.UpdateAcc(TxtBondNo.Text, TxtPtype.Text, Session["CustNo"].ToString(), Session["BRCD"].ToString(), ViewState["ACCNO"].ToString());
                        Result = LI.InsertLoanApp(Session["CustNo"].ToString(), ViewState["ACCNO"].ToString(), TxtPtype.Text, TxtAppNo.Text.Trim().ToString(), TxtLoanSanc.Text,
                            Session["EntryDate"].ToString(), intr.ToString(), conn.ConvertDate(ViewState["INSTDATE"].ToString()), ViewState["INSTALLMENT"].ToString(),
                            ViewState["PERIOD"].ToString(), conn.AddMonthDay7(Session["EntryDate"].ToString(), ViewState["PERIOD"].ToString(), "M"), penalint.ToString(),
                            "1", TxtBondNo.Text, Session["MID"].ToString(), TxtBrcd.Text, "1001", ViewState["InstType"].ToString(), "1", "2", "1", "2", Session["EntryDate"].ToString());
                    }
                }
                else
                {
                    //  If already account exists then update account details
                    ViewState["ACCNO"] = TxtCustno.Text.ToString();
                    Result = CLS.UpdateAccDetails(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), TxtPtype.Text.ToString(), TxtCustno.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    if (Result > 0)
                    {
                        CLS.UpdateAcc(TxtBondNo.Text, TxtPtype.Text, TxtCustno.Text.ToString(), Session["BRCD"].ToString(), ViewState["ACCNO"].ToString());
                        Result = LI.InsertLoanApp(TxtCustno.Text.ToString(), ViewState["ACCNO"].ToString(), TxtPtype.Text, TxtAppNo.Text.Trim().ToString(), TxtLoanSanc.Text,
                            Session["EntryDate"].ToString(), intr.ToString(), conn.ConvertDate(ViewState["INSTDATE"].ToString()), ViewState["INSTALLMENT"].ToString(),
                            ViewState["PERIOD"].ToString(), conn.AddMonthDay7(Session["EntryDate"].ToString(), ViewState["PERIOD"].ToString(), "M"), penalint.ToString(),
                            "1", TxtBondNo.Text, Session["MID"].ToString(), TxtBrcd.Text, "1001", ViewState["InstType"].ToString(), "1", "2", "1", "2", Session["EntryDate"].ToString());
                    }
                }
            }
            else
            {
                Resacc = CLS.insert("Y", Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), TxtPtype.Text, TxtCustno.Text, Session["EntryDate"].ToString(), Session["MID"].ToString(), "1", "1", "2", "1003");
                ViewState["ACCNO"] = Resacc.ToString();
                if (Resacc > 0)
                {
                    CLS.UpdateAcc(TxtBondNo.Text, TxtPtype.Text, Session["CustNo"].ToString(), Session["BRCD"].ToString(), ViewState["ACCNO"].ToString());
                    Result = LI.InsertLoanApp(TxtCustno.Text.ToString(), ViewState["ACCNO"].ToString(), TxtPtype.Text.ToString(), TxtAppNo.Text.ToString(), TxtLoanSanc.Text.ToString(),
                            Session["EntryDate"].ToString(), intr.ToString(), conn.ConvertDate(ViewState["INSTDATE"].ToString()), ViewState["INSTALLMENT"].ToString(),
                            ViewState["PERIOD"].ToString(), conn.AddMonthDay7(Session["EntryDate"].ToString(), ViewState["PERIOD"].ToString(), "M"), penalint.ToString(),
                            "1", TxtBondNo.Text, Session["MID"].ToString(), TxtBrcd.Text, "1001", ViewState["InstType"].ToString(), "1", "2", "1", "2", Session["EntryDate"].ToString());
                }
            }

            if (Result > 0)
            {
                CN = 1;
                BtnBondCr.Enabled = false;
                WebMsgBox.Show("Record Added Successfully With Bond No:" + TxtBondNo.Text.ToString(), this.Page);
                CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanBondIssue_Add _" + TxtBondNo.Text + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                BindGridLoan();
                return;
            }
            else
            {
                BtnBondCr.Enabled = true;
                BtnSurCr.Enabled = false;
                BtnVouchCr.Enabled = false;
                BtnPost.Enabled = false;
                BtnAllVoucher.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void GetBondinfo()
    {
        try
        {
            string[] ROI = LI.GetROI(TxtBrcd.Text, TxtPtype.Text).Split('_');
            intr = ROI[0].ToString();
            penalint = ROI[1].ToString();

            if (Convert.ToString(CLS.GetParameter(Session["BRCD"].ToString(), "AccNoIncrese")) == "N")
            {
                //  Create account as it is customer no as account no
                if (CLS.CheckExists(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), TxtPtype.Text.ToString(), TxtCustno.Text.ToString()) == "0")
                {
                    //  If account not exists then create new account (same as custno as accno)
                    Resacc = CLS.insert("N", Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), TxtPtype.Text, TxtCustno.Text, Session["EntryDate"].ToString(), Session["MID"].ToString(), "1", "1", "2", "1003");
                    ViewState["ACCNO"] = Resacc.ToString();

                    if (Resacc > 0)
                    {
                        CLS.UpdateAcc(TxtBondNo.Text, TxtPtype.Text, Session["CustNo"].ToString(), Session["BRCD"].ToString(), ViewState["ACCNO"].ToString());
                        Result = LI.InsertLoanApp(Session["CustNo"].ToString(), ViewState["ACCNO"].ToString(), TxtPtype.Text, TxtAppNo.Text.Trim().ToString(), TxtLoanSanc.Text,
                            Session["EntryDate"].ToString(), intr.ToString(), conn.ConvertDate(ViewState["INSTDATE"].ToString()), ViewState["INSTALLMENT"].ToString(),
                            ViewState["PERIOD"].ToString(), conn.AddMonthDay7(Session["EntryDate"].ToString(), ViewState["PERIOD"].ToString(), "M"), penalint.ToString(),
                            "1", TxtBondNo.Text, Session["MID"].ToString(), TxtBrcd.Text, "1001", ViewState["InstType"].ToString(), "1", "2", "1", "2", Session["EntryDate"].ToString());
                    }
                }
                else
                {
                    //  If already account exists then update account details
                    ViewState["ACCNO"] = TxtCustno.Text.ToString();
                    Result = CLS.UpdateAccDetails(Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), TxtPtype.Text.ToString(), TxtCustno.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    if (Result > 0)
                    {
                        CLS.UpdateAcc(TxtBondNo.Text, TxtPtype.Text, TxtCustno.Text.ToString(), Session["BRCD"].ToString(), ViewState["ACCNO"].ToString());
                        Result = LI.InsertLoanApp(TxtCustno.Text.ToString(), ViewState["ACCNO"].ToString(), TxtPtype.Text, TxtAppNo.Text.Trim().ToString(), TxtLoanSanc.Text,
                            Session["EntryDate"].ToString(), intr.ToString(), conn.ConvertDate(ViewState["INSTDATE"].ToString()), ViewState["INSTALLMENT"].ToString(),
                            ViewState["PERIOD"].ToString(), conn.AddMonthDay7(Session["EntryDate"].ToString(), ViewState["PERIOD"].ToString(), "M"), penalint.ToString(),
                            "1", TxtBondNo.Text, Session["MID"].ToString(), TxtBrcd.Text, "1001", ViewState["InstType"].ToString(), "1", "2", "1", "2", Session["EntryDate"].ToString());
                    }
                }
            }
            else
            {
                Resacc = CLS.insert("Y", Session["BRCD"].ToString(), ViewState["GLCODE"].ToString(), TxtPtype.Text, TxtCustno.Text, Session["EntryDate"].ToString(), Session["MID"].ToString(), "1", "1", "2", "1003");
                ViewState["ACCNO"] = Resacc.ToString();
                if (Resacc > 0)
                {
                    CLS.UpdateAcc(TxtBondNo.Text, TxtPtype.Text, Session["CustNo"].ToString(), Session["BRCD"].ToString(), ViewState["ACCNO"].ToString());
                    Result = LI.InsertLoanApp(TxtCustno.Text.ToString(), ViewState["ACCNO"].ToString(), TxtPtype.Text.ToString(), TxtAppNo.Text.ToString(), TxtLoanSanc.Text.ToString(),
                            Session["EntryDate"].ToString(), intr.ToString(), conn.ConvertDate(ViewState["INSTDATE"].ToString()), ViewState["INSTALLMENT"].ToString(),
                            ViewState["PERIOD"].ToString(), conn.AddMonthDay7(Session["EntryDate"].ToString(), ViewState["PERIOD"].ToString(), "M"), penalint.ToString(),
                            "1", TxtBondNo.Text, Session["MID"].ToString(), TxtBrcd.Text, "1001", ViewState["InstType"].ToString(), "1", "2", "1", "2", Session["EntryDate"].ToString());
                }
            }

            if (Result > 0)
            {
                CN = 1;
                BtnBondCr.Enabled = false;
                //WebMsgBox.Show("Record Added Successfully With Bond No:" + TxtBondNo.Text.ToString(), this.Page);
                CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanBondIssue_Add _" + TxtBondNo.Text + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                BindGridLoan();
                return;
            }
            else
            {
                BtnBondCr.Enabled = true;
                BtnSurCr.Enabled = false;
                BtnVouchCr.Enabled = false;
                BtnPost.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void BtnSurCr_Click(object sender, EventArgs e)
    {
        try
        {
            if (CN <= 0)
            {
                WebMsgBox.Show("First Create bond", this.Page);
                return;
            }
            else
            {
                DataTable DT = new DataTable();
                DT = CD.GetAllSurity(Session["BRCD"].ToString(), Session["CustNo"].ToString(), Session["EntryDate"].ToString());
                if (DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        SD.DeleteSurity(TxtPtype.Text, DT.Rows[i]["LOANACCNO"].ToString(), DT.Rows[i]["MEMBERNO"].ToString(), DT.Rows[i]["BRCD"].ToString());
                    }
                }
                dt = CLS.GetSurityDetails(TxtBrcd.Text, TxtPtype.Text, TxtAppNo.Text);

                //SrNumber = SD.InsertData(Session["BRCD"].ToString(), txtPrCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), txtCustNo.Text.Trim().ToString(), Type.ToString(), txtSrNo.Text.Trim().ToString(), txtSrName.Text.Trim().ToString(), txtMemNo.Text.Trim().ToString(), txtMemNumber.Text.Trim().ToString() == "" ? "0" : txtMemNumber.Text.Trim().ToString(), ddlProfName.SelectedValue.ToString(), txtEmpNo.Text.Trim().ToString() == "" ? "0" : txtEmpNo.Text.Trim().ToString(), txtBusNo.Text.Trim().ToString() == "" ? "0" : txtBusNo.Text.Trim().ToString(), txtBusName.Text.Trim().ToString() == "" ? "" : txtBusName.Text.Trim().ToString(), ddlTieUpName.SelectedValue.ToString(), txtEmpCode.Text.Trim().ToString() == "" ? "0" : txtEmpCode.Text.Trim().ToString(), txtEmpName.Text.Trim().ToString() == "" ? "" : txtEmpName.Text.Trim().ToString(), txtDesNo.Text.Trim().ToString() == "" ? "0" : txtDesNo.Text.Trim().ToString(), txtDesName.Text.Trim().ToString() == "" ? "" : txtDesName.Text.Trim().ToString(), txtAddrs1.Text.ToString(), txtAddrs2.Text.ToString(), txtCity.Text.Trim().ToString(), txtPinCode.Text.Trim().ToString(), txtTelNo.Text.Trim().ToString(), txtMonIncome.Text.Trim().ToString(), ddlStatusName.SelectedValue.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SrNumber = SD.InsertData(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), ViewState["ACCNO"].ToString(), dt.Rows[i]["CUSTNO"].ToString(), "Surity", dt.Rows[i]["SURITYSRNO"].ToString(), dt.Rows[i]["SURITYNAME"].ToString(), "1", dt.Rows[i]["MEM_NO_SURITY"].ToString(), "", "", "", "", "", "", "", "", "", "", "", "", "", "0", "", "", Session["EntryDate"].ToString(), Session["MID"].ToString(), "");
                    }
                    if (SrNumber != "")
                    {
                        string[] srno = SrNumber.Split('_');
                        SrNum = Convert.ToInt32(srno[0].ToString());
                        resultint = Convert.ToInt32(srno[1].ToString());
                    }
                    if (resultint > 0)
                    {
                        BtnSurCr.Enabled = false;
                        WebMsgBox.Show("Surity Added Successfully..!!", this.Page);
                        CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanBondIssue_Surity_Add _" + TxtBondNo.Text + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        CLS.GetFromSurity(GrdFromSurity, Session["BRCD"].ToString(), Session["CustNo"].ToString(), TxtPtype.Text, Session["EntryDate"].ToString(), "BOND");
                        CD.GetAccountInfo(GrdDirectLiab, Session["BRCD"].ToString(), Session["CustNo"].ToString(), Session["EntryDate"].ToString(), "DL");
                        return;
                    }
                    else
                    {
                        BtnBondCr.Enabled = false;
                        BtnSurCr.Enabled = true;
                        BtnVouchCr.Enabled = false;
                        BtnPost.Enabled = false;
                        BtnAllVoucher.Enabled = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void GetSurityinfo()
    {
        try
        {
            if (CN <= 0)
            {
                WebMsgBox.Show("First Create bond", this.Page);
                return;
            }
            else
            {
                DataTable DT = new DataTable();
                DT = CD.GetAllSurity(Session["BRCD"].ToString(), Session["CustNo"].ToString(), Session["EntryDate"].ToString());
                if (DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        SD.DeleteSurity(TxtPtype.Text, DT.Rows[i]["LOANACCNO"].ToString(), DT.Rows[i]["MEMBERNO"].ToString(), DT.Rows[i]["BRCD"].ToString());
                    }
                }
                dt = CLS.GetSurityDetails(TxtBrcd.Text, TxtPtype.Text, TxtAppNo.Text);

                //SrNumber = SD.InsertData(Session["BRCD"].ToString(), txtPrCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), txtCustNo.Text.Trim().ToString(), Type.ToString(), txtSrNo.Text.Trim().ToString(), txtSrName.Text.Trim().ToString(), txtMemNo.Text.Trim().ToString(), txtMemNumber.Text.Trim().ToString() == "" ? "0" : txtMemNumber.Text.Trim().ToString(), ddlProfName.SelectedValue.ToString(), txtEmpNo.Text.Trim().ToString() == "" ? "0" : txtEmpNo.Text.Trim().ToString(), txtBusNo.Text.Trim().ToString() == "" ? "0" : txtBusNo.Text.Trim().ToString(), txtBusName.Text.Trim().ToString() == "" ? "" : txtBusName.Text.Trim().ToString(), ddlTieUpName.SelectedValue.ToString(), txtEmpCode.Text.Trim().ToString() == "" ? "0" : txtEmpCode.Text.Trim().ToString(), txtEmpName.Text.Trim().ToString() == "" ? "" : txtEmpName.Text.Trim().ToString(), txtDesNo.Text.Trim().ToString() == "" ? "0" : txtDesNo.Text.Trim().ToString(), txtDesName.Text.Trim().ToString() == "" ? "" : txtDesName.Text.Trim().ToString(), txtAddrs1.Text.ToString(), txtAddrs2.Text.ToString(), txtCity.Text.Trim().ToString(), txtPinCode.Text.Trim().ToString(), txtTelNo.Text.Trim().ToString(), txtMonIncome.Text.Trim().ToString(), ddlStatusName.SelectedValue.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SrNumber = SD.InsertData(Session["BRCD"].ToString(), TxtPtype.Text.Trim().ToString(), ViewState["ACCNO"].ToString(), dt.Rows[i]["CUSTNO"].ToString(), "Surity", dt.Rows[i]["SURITYSRNO"].ToString(), dt.Rows[i]["SURITYNAME"].ToString(), "1", dt.Rows[i]["MEM_NO_SURITY"].ToString(), "", "", "", "", "", "", "", "", "", "", "", "", "", "0", "", "", Session["EntryDate"].ToString(), Session["MID"].ToString(), "");
                    }
                    if (SrNumber != "")
                    {
                        string[] srno = SrNumber.Split('_');
                        SrNum = Convert.ToInt32(srno[0].ToString());
                        resultint = Convert.ToInt32(srno[1].ToString());
                    }
                    if (resultint > 0)
                    {
                        BtnSurCr.Enabled = false;
                        //WebMsgBox.Show("Surity Added Successfully..!!", this.Page);
                        CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanBondIssue_Surity_Add _" + TxtBondNo.Text + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        CLS.GetFromSurity(GrdFromSurity, Session["BRCD"].ToString(), Session["CustNo"].ToString(), TxtPtype.Text, Session["EntryDate"].ToString(), "BOND");
                        CD.GetAccountInfo(GrdDirectLiab, Session["BRCD"].ToString(), Session["CustNo"].ToString(), Session["EntryDate"].ToString(), "DL");
                        return;
                    }
                    else
                    {
                        BtnBondCr.Enabled = false;
                        BtnSurCr.Enabled = true;
                        BtnVouchCr.Enabled = false;
                        BtnPost.Enabled = false;
                        BtnAllVoucher.Enabled = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void BtnVouchCr_Click(object sender, EventArgs e)
    {
        string GL = "";
        try
        {
            if (Convert.ToDouble(TxtDeduct.Text) > 0.00)
            {
                dt = new DataTable();
                dt = CLS.GetLoanData(TxtBrcd.Text.Trim().ToString(), Session["CustNo"].ToString(), TxtAppNo.Text.Trim().ToString());

                NetPaid = Convert.ToInt32(CLS.NetPaidProduct(Session["BrCd"].ToString()));
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["GlCode"].ToString() == "3")
                        {
                            BrCode = Convert.ToInt32(dt.Rows[i]["BrCd"].ToString());
                            CustName = dt.Rows[i]["CustName"].ToString();
                            CustNo = Convert.ToInt32(dt.Rows[i]["CustNo"].ToString());
                            AccNo = Convert.ToInt32(dt.Rows[i]["CustAccNo"].ToString());
                            GlCode = Convert.ToInt32(dt.Rows[i]["GlCode"].ToString());
                            LoanGlCode = Convert.ToInt32(dt.Rows[i]["SubGlCode"].ToString());
                            PlAccNo = Convert.ToInt32(dt.Rows[i]["PlAccNo"].ToString());
                            IntCalType = Convert.ToInt32(dt.Rows[i]["IntCalType"].ToString());
                        }
                    }

                    if (ddlPayType.SelectedValue.ToString() != "0")
                    {
                        if (ddlPayType.SelectedValue.ToString() == "1")
                        {
                            if (txtNarration.Text.Trim().ToString() == "")
                            {
                                txtNarration.Focus();
                                WebMsgBox.Show("Enter any narration first ...!!", this.Page);
                                return;
                            }
                        }
                        else if (ddlPayType.SelectedValue.ToString() == "2")
                        {
                            if (txtProdType1.Text.Trim().ToString() == "")
                            {
                                txtProdType1.Focus();
                                WebMsgBox.Show("Enter product code first ...!!", this.Page);
                                return;
                            }
                            if (TxtAccNo1.Text.Trim().ToString() == "")
                            {
                                TxtAccNo1.Focus();
                                WebMsgBox.Show("Enter account number first ...!!", this.Page);
                                return;
                            }
                        }
                        else if (ddlPayType.SelectedValue.ToString() == "4")
                        {
                            if (txtProdType1.Text.Trim().ToString() == "")
                            {
                                txtProdType1.Focus();
                                WebMsgBox.Show("Enter product code first ...!!", this.Page);
                                return;
                            }
                            if (TxtAccNo1.Text.Trim().ToString() == "")
                            {
                                TxtAccNo1.Focus();
                                WebMsgBox.Show("Enter account number first ...!!", this.Page);
                                return;
                            }
                            if (TxtChequeNo.Text.Trim().ToString() == "")
                            {
                                TxtChequeNo.Focus();
                                WebMsgBox.Show("Enter account number first ...!!", this.Page);
                                return;
                            }
                            if (TxtChequeDate.Text.Trim().ToString() == "")
                            {
                                TxtChequeDate.Focus();
                                WebMsgBox.Show("Enter account number first ...!!", this.Page);
                                return;
                            }
                        }
                        else
                        {
                            ddlPayType.Focus();
                            WebMsgBox.Show("Select Proper Payment Mode First ...!!", this.Page);
                            return;
                        }

                        //interest Debit to GL 3
                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), Session["CustNo"].ToString(), "3", TxtPtype.Text.Trim().ToString(), ViewState["ACCNO"].ToString(), CustName.ToString(), TxtLoanSanc.Text.Trim().ToString(), "2", "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYDR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                        if (resultout > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                //Insert Data to Tempprary Table (Avs_TempMultiTransfer) Here
                                if (dt.Rows[i]["GlCode"].ToString() == "11")
                                {
                                    IntCalType = Convert.ToInt32(dt.Rows[i]["IntCalType"]);
                                    PlAccNo = Convert.ToInt32(dt.Rows[i]["PlAccNo"].ToString());
                                    if (IntCalType == 1)
                                    {
                                        //interest Credit to GL 11
                                        GL = "";
                                        GL = CLS.GetGLCode(Session["BRCD"].ToString(), dt.Rows[i]["SubGlCode"].ToString());
                                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), dt.Rows[i]["CustNo"].ToString(), GL, dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), dt.Rows[i]["CustName"].ToString(), dt.Rows[i]["Amount"].ToString(), dt.Rows[i]["Trxtype"].ToString(), "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "INTCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[i]["Amount"].ToString()) > 0)
                                            {
                                                //Current Interest Debit To 2 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), LoanGlCode.ToString(), dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), "2", "2", "7", "Interest Debit", dt.Rows[i]["Amount"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[i]["Amount"].ToString()) > 0)
                                            {
                                                //Current Interest Credit To 2 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), LoanGlCode.ToString(), dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), "2", "1", "7", "Interest Credit", dt.Rows[i]["Amount"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            //interest Applied Contra
                                            if (Convert.ToDouble(dt.Rows[i]["Amount"].ToString()) > 0)
                                            {
                                                //interest Applied Debit To GL 11

                                                GL = "";
                                                GL = CLS.GetGLCode(Session["BRCD"].ToString(), dt.Rows[i]["SubGlCode"].ToString());
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), dt.Rows[i]["CustNo"].ToString(), GL, dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), dt.Rows[i]["CustName"].ToString(), dt.Rows[i]["Amount"].ToString(), "2", "11", "TR_INT", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "INTDR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    //interest Applied Credit to GL 100
                                                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), dt.Rows[i]["CustNo"].ToString(), "100", PlAccNo.ToString(), dt.Rows[i]["CustAccNo"].ToString(), dt.Rows[i]["CustName"].ToString(), dt.Rows[i]["Amount"].ToString(), "1", "11", "TR_INT", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "INTCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), conn.ConvertDate(Session["EntryDate"].ToString()), Session["MID"].ToString());
                                                }
                                            }
                                        }
                                    }
                                    else if (IntCalType == 2)
                                    {
                                        //interest Credit to GL 3

                                        GL = "";
                                        GL = CLS.GetGLCode(Session["BRCD"].ToString(), dt.Rows[i]["SubGlCode"].ToString());
                                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), dt.Rows[i]["CustNo"].ToString(), GL, dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), dt.Rows[i]["CustName"].ToString(), dt.Rows[i]["Amount"].ToString(), dt.Rows[i]["Trxtype"].ToString(), "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "INTCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[i]["Amount"].ToString()) > 0)
                                            {
                                                //interest Applied Debit To GL 3

                                                GL = "";
                                                GL = CLS.GetGLCode(Session["BRCD"].ToString(), LoanGlCode.ToString());
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), dt.Rows[i]["CustNo"].ToString(), GL, LoanGlCode.ToString(), dt.Rows[i]["CustAccNo"].ToString(), dt.Rows[i]["CustName"].ToString(), dt.Rows[i]["Amount"].ToString(), "2", "11", "TR_INT", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "INTDR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    //interest Applied Credit to GL 100
                                                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), dt.Rows[i]["CustNo"].ToString(), "100", PlAccNo.ToString(), dt.Rows[i]["CustAccNo"].ToString(), dt.Rows[i]["CustName"].ToString(), dt.Rows[i]["Amount"].ToString(), "1", "11", "TR_INT", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "INTCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                }
                                            }
                                        }
                                    }
                                    else if (IntCalType == 3)
                                    {
                                        //interest Credit to GL 11

                                        GL = "";
                                        GL = CLS.GetGLCode(Session["BRCD"].ToString(), dt.Rows[i]["SubGlCode"].ToString());
                                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), dt.Rows[i]["CustNo"].ToString(), GL, dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), dt.Rows[i]["CustName"].ToString(), dt.Rows[i]["Amount"].ToString(), dt.Rows[i]["Trxtype"].ToString(), "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "INTCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[i]["Amount"].ToString()) > 0)
                                            {
                                                //Current Interest Debit To 2 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), LoanGlCode.ToString(), dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), "2", "2", "7", "Interest Debit", dt.Rows[i]["Amount"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[i]["Amount"].ToString()) > 0)
                                            {
                                                //Current Interest Credit To 2 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), LoanGlCode.ToString(), dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), "2", "1", "7", "Interest Credit", dt.Rows[i]["Amount"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                    }
                                }
                                else if (dt.Rows[i]["GlCode"].ToString() == "3")
                                {
                                    GL = "";
                                    GL = CLS.GetGLCode(Session["BRCD"].ToString(), dt.Rows[i]["SubGlCode"].ToString());
                                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), dt.Rows[i]["CustNo"].ToString(), GL, dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), dt.Rows[i]["CustName"].ToString(), dt.Rows[i]["Amount"].ToString(), dt.Rows[i]["Trxtype"].ToString(), "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PRNCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[i]["Amount"].ToString()) > 0)
                                        {
                                            //Current Principle Debit To 1 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), LoanGlCode.ToString(), dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), "1", "2", "7", "Principle Debit", dt.Rows[i]["Amount"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[i]["Amount"].ToString()) > 0)
                                        {
                                            //Current Principle Credit To 1 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), LoanGlCode.ToString(), dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), "1", "1", "7", "Principle Credit", dt.Rows[i]["Amount"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    GL = "";
                                    GL = CLS.GetGLCode(Session["BRCD"].ToString(), dt.Rows[i]["SubGlCode"].ToString());
                                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), dt.Rows[i]["CustNo"].ToString(), GL, dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), dt.Rows[i]["CustName"].ToString(), dt.Rows[i]["Amount"].ToString(), dt.Rows[i]["Trxtype"].ToString(), "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "OTHCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());
                                }
                            }

                            if (resultout > 0)
                            {
                                if (Convert.ToInt32(TxtNetPaid.Text.Trim().ToString()) > 0)
                                {
                                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), CustNo.ToString(), NetPaid.ToString(), NetPaid.ToString(), AccNo.ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "1", "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "NETCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());
                                }
                            }

                            if (resultout > 0)
                            {
                                if (Convert.ToInt32(TxtNetPaid.Text.Trim().ToString()) > 0)
                                {
                                    if (resultout > 0)
                                    {
                                        if (ddlPayType.SelectedValue.ToString() == "1")
                                        {
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), CustNo.ToString(), NetPaid.ToString(), NetPaid.ToString(), AccNo.ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "2", "3", "CR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYDR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), CustNo.ToString(), "99", "99", ViewState["ACCNO"].ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "1", "3", "CR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }
                                        }
                                        else if (ddlPayType.SelectedValue.ToString() == "2")
                                        {
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), CustNo.ToString(), NetPaid.ToString(), NetPaid.ToString(), AccNo.ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "2", "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYDR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), CustNo.ToString(), ViewState["GLCODE1"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "1", "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }
                                        }
                                        else if (ddlPayType.SelectedValue.ToString() == "4")
                                        {
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), CustNo.ToString(), NetPaid.ToString(), NetPaid.ToString(), AccNo.ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "2", "6", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYDR", TxtChequeNo.Text.Trim().ToString(), conn.ConvertDate(TxtChequeDate.Text.Trim().ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), CustNo.ToString(), ViewState["GLCODE1"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "1", "6", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYCR", TxtChequeNo.Text.Trim().ToString(), conn.ConvertDate(TxtChequeDate.Text.Trim().ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ddlPayType.Focus();
                        WebMsgBox.Show("Select Proper Payment Mode First ...!!", this.Page);
                        return;
                    }

                    if (resultout > 0)
                    {
                        Transfer.Visible = false;
                        Transfer1.Visible = false;
                        DivAmount.Visible = false;
                        BtnVouchCr.Enabled = false;
                        BtnPost.Enabled = true;

                        Getinfo();
                        BtnPost.Focus();
                        ddlPayType.SelectedValue = "0";
                        WebMsgBox.Show("Successfully Added ...!!", this.Page);
                        CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanBondIssue_Add _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        return;
                    }
                }
            }
            else
            {
                NetPaid = Convert.ToInt32(CLS.NetPaidProduct(Session["BrCd"].ToString()));
                if (ddlPayType.SelectedValue.ToString() == "1" || ddlPayType.SelectedValue.ToString() == "2")
                {
                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), Session["CustNo"].ToString(), "3", TxtPtype.Text.Trim().ToString(), ViewState["ACCNO"].ToString(), CustName.ToString(), TxtLoanSanc.Text.Trim().ToString(), "2", "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYDR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (Convert.ToInt32(TxtNetPaid.Text.Trim().ToString()) > 0)
                    {
                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), Session["CustNo"].ToString(), NetPaid.ToString(), NetPaid.ToString(), ViewState["ACCNO"].ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "1", "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "NETCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    }
                }
                else
                {
                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), Session["CustNo"].ToString(), "3", TxtPtype.Text.Trim().ToString(), ViewState["ACCNO"].ToString(), CustName.ToString(), TxtLoanSanc.Text.Trim().ToString(), "2", "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYDR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (Convert.ToInt32(TxtNetPaid.Text.Trim().ToString()) > 0)
                    {
                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), Session["CustNo"].ToString(), NetPaid.ToString(), NetPaid.ToString(), ViewState["ACCNO"].ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "1", "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "NETCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    }
                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), Session["CustNo"].ToString(), NetPaid.ToString(), NetPaid.ToString(), ViewState["ACCNO"].ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "2", "6", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYDR", TxtChequeNo.Text.Trim().ToString(), conn.ConvertDate(TxtChequeDate.Text.Trim().ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (resultout > 0)
                    {
                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), Session["CustNo"].ToString(), ViewState["GLCODE1"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "1", "6", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYCR", TxtChequeNo.Text.Trim().ToString(), conn.ConvertDate(TxtChequeDate.Text.Trim().ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    }
                }
                if (resultout > 0)
                {
                    Transfer.Visible = false;
                    Transfer1.Visible = false;
                    DivAmount.Visible = false;
                    BtnVouchCr.Enabled = false;
                    BtnPost.Enabled = true;

                    Getinfo();
                    BtnPost.Focus();
                    ddlPayType.SelectedValue = "0";
                    WebMsgBox.Show("Successfully Added ...!!", this.Page);
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanBondIssue_Add _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void GetVoucherinfo()
    {
        string GL = "";
        try
        {
            if (Convert.ToDouble(TxtDeduct.Text) > 0.00)
            {
                dt = new DataTable();
                dt = CLS.GetLoanData(TxtBrcd.Text.Trim().ToString(), Session["CustNo"].ToString(), TxtAppNo.Text.Trim().ToString());

                NetPaid = Convert.ToInt32(CLS.NetPaidProduct(Session["BrCd"].ToString()));
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["GlCode"].ToString() == "3")
                        {
                            BrCode = Convert.ToInt32(dt.Rows[i]["BrCd"].ToString());
                            CustName = dt.Rows[i]["CustName"].ToString();
                            CustNo = Convert.ToInt32(dt.Rows[i]["CustNo"].ToString());
                            AccNo = Convert.ToInt32(dt.Rows[i]["CustAccNo"].ToString());
                            GlCode = Convert.ToInt32(dt.Rows[i]["GlCode"].ToString());
                            LoanGlCode = Convert.ToInt32(dt.Rows[i]["SubGlCode"].ToString());
                            PlAccNo = Convert.ToInt32(dt.Rows[i]["PlAccNo"].ToString());
                            IntCalType = Convert.ToInt32(dt.Rows[i]["IntCalType"].ToString());
                        }
                    }

                    if (ddlPayType.SelectedValue.ToString() != "0")
                    {
                        if (ddlPayType.SelectedValue.ToString() == "1")
                        {
                            if (txtNarration.Text.Trim().ToString() == "")
                            {
                                txtNarration.Focus();
                                WebMsgBox.Show("Enter any narration first ...!!", this.Page);
                                return;
                            }
                        }
                        else if (ddlPayType.SelectedValue.ToString() == "2")
                        {
                            if (txtProdType1.Text.Trim().ToString() == "")
                            {
                                txtProdType1.Focus();
                                WebMsgBox.Show("Enter product code first ...!!", this.Page);
                                return;
                            }
                            if (TxtAccNo1.Text.Trim().ToString() == "")
                            {
                                TxtAccNo1.Focus();
                                WebMsgBox.Show("Enter account number first ...!!", this.Page);
                                return;
                            }
                        }
                        else if (ddlPayType.SelectedValue.ToString() == "4")
                        {
                            if (txtProdType1.Text.Trim().ToString() == "")
                            {
                                txtProdType1.Focus();
                                WebMsgBox.Show("Enter product code first ...!!", this.Page);
                                return;
                            }
                            if (TxtAccNo1.Text.Trim().ToString() == "")
                            {
                                TxtAccNo1.Focus();
                                WebMsgBox.Show("Enter account number first ...!!", this.Page);
                                return;
                            }
                            if (TxtChequeNo.Text.Trim().ToString() == "")
                            {
                                TxtChequeNo.Focus();
                                WebMsgBox.Show("Enter account number first ...!!", this.Page);
                                return;
                            }
                            if (TxtChequeDate.Text.Trim().ToString() == "")
                            {
                                TxtChequeDate.Focus();
                                WebMsgBox.Show("Enter account number first ...!!", this.Page);
                                return;
                            }
                        }
                        else
                        {
                            ddlPayType.Focus();
                            WebMsgBox.Show("Select Proper Payment Mode First ...!!", this.Page);
                            return;
                        }

                        //interest Debit to GL 3
                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), Session["CustNo"].ToString(), "3", TxtPtype.Text.Trim().ToString(), ViewState["ACCNO"].ToString(), CustName.ToString(), TxtLoanSanc.Text.Trim().ToString(), "2", "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYDR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                        if (resultout > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                //Insert Data to Tempprary Table (Avs_TempMultiTransfer) Here
                                if (dt.Rows[i]["GlCode"].ToString() == "11")
                                {
                                    IntCalType = Convert.ToInt32(dt.Rows[i]["IntCalType"]);
                                    PlAccNo = Convert.ToInt32(dt.Rows[i]["PlAccNo"].ToString());
                                    if (IntCalType == 1)
                                    {
                                        //interest Credit to GL 11
                                        GL = "";
                                        GL = CLS.GetGLCode(Session["BRCD"].ToString(), dt.Rows[i]["SubGlCode"].ToString());
                                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), dt.Rows[i]["CustNo"].ToString(), GL, dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), dt.Rows[i]["CustName"].ToString(), dt.Rows[i]["Amount"].ToString(), dt.Rows[i]["Trxtype"].ToString(), "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "INTCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[i]["Amount"].ToString()) > 0)
                                            {
                                                //Current Interest Debit To 2 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), LoanGlCode.ToString(), dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), "2", "2", "7", "Interest Debit", dt.Rows[i]["Amount"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[i]["Amount"].ToString()) > 0)
                                            {
                                                //Current Interest Credit To 2 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), LoanGlCode.ToString(), dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), "2", "1", "7", "Interest Credit", dt.Rows[i]["Amount"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            //interest Applied Contra
                                            if (Convert.ToDouble(dt.Rows[i]["Amount"].ToString()) > 0)
                                            {
                                                //interest Applied Debit To GL 11

                                                GL = "";
                                                GL = CLS.GetGLCode(Session["BRCD"].ToString(), dt.Rows[i]["SubGlCode"].ToString());
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), dt.Rows[i]["CustNo"].ToString(), GL, dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), dt.Rows[i]["CustName"].ToString(), dt.Rows[i]["Amount"].ToString(), "2", "11", "TR_INT", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "INTDR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    //interest Applied Credit to GL 100
                                                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), dt.Rows[i]["CustNo"].ToString(), "100", PlAccNo.ToString(), dt.Rows[i]["CustAccNo"].ToString(), dt.Rows[i]["CustName"].ToString(), dt.Rows[i]["Amount"].ToString(), "1", "11", "TR_INT", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "INTCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), conn.ConvertDate(Session["EntryDate"].ToString()), Session["MID"].ToString());
                                                }
                                            }
                                        }
                                    }
                                    else if (IntCalType == 2)
                                    {
                                        //interest Credit to GL 3

                                        GL = "";
                                        GL = CLS.GetGLCode(Session["BRCD"].ToString(), dt.Rows[i]["SubGlCode"].ToString());
                                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), dt.Rows[i]["CustNo"].ToString(), GL, dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), dt.Rows[i]["CustName"].ToString(), dt.Rows[i]["Amount"].ToString(), dt.Rows[i]["Trxtype"].ToString(), "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "INTCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[i]["Amount"].ToString()) > 0)
                                            {
                                                //interest Applied Debit To GL 3

                                                GL = "";
                                                GL = CLS.GetGLCode(Session["BRCD"].ToString(), LoanGlCode.ToString());
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), dt.Rows[i]["CustNo"].ToString(), GL, LoanGlCode.ToString(), dt.Rows[i]["CustAccNo"].ToString(), dt.Rows[i]["CustName"].ToString(), dt.Rows[i]["Amount"].ToString(), "2", "11", "TR_INT", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "INTDR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                                if (resultout > 0)
                                                {
                                                    //interest Applied Credit to GL 100
                                                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), dt.Rows[i]["CustNo"].ToString(), "100", PlAccNo.ToString(), dt.Rows[i]["CustAccNo"].ToString(), dt.Rows[i]["CustName"].ToString(), dt.Rows[i]["Amount"].ToString(), "1", "11", "TR_INT", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "INTCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                }
                                            }
                                        }
                                    }
                                    else if (IntCalType == 3)
                                    {
                                        //interest Credit to GL 11

                                        GL = "";
                                        GL = CLS.GetGLCode(Session["BRCD"].ToString(), dt.Rows[i]["SubGlCode"].ToString());
                                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), dt.Rows[i]["CustNo"].ToString(), GL, dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), dt.Rows[i]["CustName"].ToString(), dt.Rows[i]["Amount"].ToString(), dt.Rows[i]["Trxtype"].ToString(), "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "INTCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[i]["Amount"].ToString()) > 0)
                                            {
                                                //Current Interest Debit To 2 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), LoanGlCode.ToString(), dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), "2", "2", "7", "Interest Debit", dt.Rows[i]["Amount"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }

                                        if (resultout > 0)
                                        {
                                            if (Convert.ToDouble(dt.Rows[i]["Amount"].ToString()) > 0)
                                            {
                                                //Current Interest Credit To 2 In AVS_LnTrx
                                                resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), LoanGlCode.ToString(), dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), "2", "1", "7", "Interest Credit", dt.Rows[i]["Amount"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                            }
                                        }
                                    }
                                }
                                else if (dt.Rows[i]["GlCode"].ToString() == "3")
                                {
                                    GL = "";
                                    GL = CLS.GetGLCode(Session["BRCD"].ToString(), dt.Rows[i]["SubGlCode"].ToString());
                                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), dt.Rows[i]["CustNo"].ToString(), GL, dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), dt.Rows[i]["CustName"].ToString(), dt.Rows[i]["Amount"].ToString(), dt.Rows[i]["Trxtype"].ToString(), "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PRNCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[i]["Amount"].ToString()) > 0)
                                        {
                                            //Current Principle Debit To 1 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), LoanGlCode.ToString(), dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), "1", "2", "7", "Principle Debit", dt.Rows[i]["Amount"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }

                                    if (resultout > 0)
                                    {
                                        if (Convert.ToDouble(dt.Rows[i]["Amount"].ToString()) > 0)
                                        {
                                            //Current Principle Credit To 1 In AVS_LnTrx
                                            resultout = ITrans.TempLoanTrx(Session["BRCD"].ToString(), LoanGlCode.ToString(), dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), "1", "1", "7", "Principle Credit", dt.Rows[i]["Amount"].ToString(), "0", Session["MID"].ToString(), "0", Session["EntryDate"].ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    GL = "";
                                    GL = CLS.GetGLCode(Session["BRCD"].ToString(), dt.Rows[i]["SubGlCode"].ToString());
                                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), dt.Rows[i]["CustNo"].ToString(), GL, dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["CustAccNo"].ToString(), dt.Rows[i]["CustName"].ToString(), dt.Rows[i]["Amount"].ToString(), dt.Rows[i]["Trxtype"].ToString(), "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "OTHCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());
                                }
                            }

                            if (resultout > 0)
                            {
                                if (Convert.ToInt32(TxtNetPaid.Text.Trim().ToString()) > 0)
                                {
                                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), CustNo.ToString(), NetPaid.ToString(), NetPaid.ToString(), AccNo.ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "1", "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "NETCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());
                                }
                            }

                            if (resultout > 0)
                            {
                                if (Convert.ToInt32(TxtNetPaid.Text.Trim().ToString()) > 0)
                                {
                                    if (resultout > 0)
                                    {
                                        if (ddlPayType.SelectedValue.ToString() == "1")
                                        {
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), CustNo.ToString(), NetPaid.ToString(), NetPaid.ToString(), AccNo.ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "2", "3", "CR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYDR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), CustNo.ToString(), "99", "99", ViewState["ACCNO"].ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "1", "3", "CR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }
                                        }
                                        else if (ddlPayType.SelectedValue.ToString() == "2")
                                        {
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), CustNo.ToString(), NetPaid.ToString(), NetPaid.ToString(), AccNo.ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "2", "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYDR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), CustNo.ToString(), ViewState["GLCODE1"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "1", "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }
                                        }
                                        else if (ddlPayType.SelectedValue.ToString() == "4")
                                        {
                                            resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), CustNo.ToString(), NetPaid.ToString(), NetPaid.ToString(), AccNo.ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "2", "6", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYDR", TxtChequeNo.Text.Trim().ToString(), conn.ConvertDate(TxtChequeDate.Text.Trim().ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                            if (resultout > 0)
                                            {
                                                resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), CustNo.ToString(), ViewState["GLCODE1"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "1", "6", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYCR", TxtChequeNo.Text.Trim().ToString(), conn.ConvertDate(TxtChequeDate.Text.Trim().ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ddlPayType.Focus();
                        WebMsgBox.Show("Select Proper Payment Mode First ...!!", this.Page);
                        return;
                    }

                    if (resultout > 0)
                    {
                        Transfer.Visible = false;
                        Transfer1.Visible = false;
                        DivAmount.Visible = false;
                        BtnVouchCr.Enabled = false;
                        BtnPost.Enabled = true;

                        Getinfo();
                        BtnPost.Focus();
                        ddlPayType.SelectedValue = "0";
                        //WebMsgBox.Show("Successfully Added ...!!", this.Page);
                        CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanBondIssue_Add _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        return;
                    }
                }
            }
            else
            {
                NetPaid = Convert.ToInt32(CLS.NetPaidProduct(Session["BrCd"].ToString()));
                if (ddlPayType.SelectedValue.ToString() == "1" || ddlPayType.SelectedValue.ToString() == "2")
                {
                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), Session["CustNo"].ToString(), "3", TxtPtype.Text.Trim().ToString(), ViewState["ACCNO"].ToString(), CustName.ToString(), TxtLoanSanc.Text.Trim().ToString(), "2", "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYDR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (Convert.ToInt32(TxtNetPaid.Text.Trim().ToString()) > 0)
                    {
                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), Session["CustNo"].ToString(), NetPaid.ToString(), NetPaid.ToString(), ViewState["ACCNO"].ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "1", "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "NETCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    }
                }
                else
                {
                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), Session["CustNo"].ToString(), "3", TxtPtype.Text.Trim().ToString(), ViewState["ACCNO"].ToString(), CustName.ToString(), TxtLoanSanc.Text.Trim().ToString(), "2", "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYDR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (Convert.ToInt32(TxtNetPaid.Text.Trim().ToString()) > 0)
                    {
                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), Session["CustNo"].ToString(), NetPaid.ToString(), NetPaid.ToString(), ViewState["ACCNO"].ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "1", "7", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "NETCR", "0", conn.ConvertDate(Session["EntryDate"].ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    }
                    resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), Session["CustNo"].ToString(), NetPaid.ToString(), NetPaid.ToString(), ViewState["ACCNO"].ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "2", "6", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYDR", TxtChequeNo.Text.Trim().ToString(), conn.ConvertDate(TxtChequeDate.Text.Trim().ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (resultout > 0)
                    {
                        resultout = MV.InsertIntoTable(Session["BRCD"].ToString(), Session["CustNo"].ToString(), ViewState["GLCODE1"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), CustName.ToString(), TxtNetPaid.Text.Trim().ToString(), "1", "6", "TR", "0", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), "PAYCR", TxtChequeNo.Text.Trim().ToString(), conn.ConvertDate(TxtChequeDate.Text.Trim().ToString()), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    }
                }
                if (resultout > 0)
                {
                    Transfer.Visible = false;
                    Transfer1.Visible = false;
                    DivAmount.Visible = false;
                    BtnVouchCr.Enabled = false;
                    BtnPost.Enabled = true;

                    Getinfo();
                    BtnPost.Focus();
                    ddlPayType.SelectedValue = "0";
                    WebMsgBox.Show("Successfully Added ...!!", this.Page);
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanBondIssue_Add _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void BtnPost_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            string PAYMAST = "LOANAPP";
            string ActivityType = "";

            if (Convert.ToDouble(txtCrAmount.Text.Trim().ToString() == "" ? "0" : txtCrAmount.Text.Trim().ToString()) == Convert.ToDouble(txtDrAmount.Text.Trim().ToString() == "" ? "0" : txtDrAmount.Text.Trim().ToString()))
            {
                //Get All Transaction From Temporary Table
                dt = MV.GetTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                if (dt.Rows.Count > 0)
                {
                    SetNo1 = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString());

                    ActivityType = MV.GetTransDetails_Activity(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (ActivityType.ToString() == "6")
                    {
                        SetNo2 = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString());
                    }

                    string RefNo = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                    ViewState["RID"] = (Convert.ToInt32(RefNo) + 1).ToString();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //Insert Data to Original Table Here
                        if (dt.Rows[i]["Activity"].ToString() == "6")
                            resultout = AT.LoanAppAuthorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[i]["GLCODE"].ToString(), dt.Rows[i]["SUBGLCODE"].ToString(), dt.Rows[i]["ACCNO"].ToString(), dt.Rows[i]["PARTICULARS"].ToString(), dt.Rows[i]["PARTICULARS2"].ToString(), dt.Rows[i]["AMOUNT"].ToString(), dt.Rows[i]["TRXTYPE"].ToString(), dt.Rows[i]["ACTIVITY"].ToString(), dt.Rows[i]["PmtMode"].ToString(), SetNo2, dt.Rows[i]["InstNo"].ToString(), dt.Rows[i]["InstDate"].ToString(), "0", "0", "1003", "", dt.Rows[i]["BRCD"].ToString(), Session["MID"].ToString(), TxtPtype.Text.Trim().ToString() + "/" + ViewState["ACCNO"].ToString(), "0", PAYMAST, dt.Rows[i]["CUSTNO"].ToString(), dt.Rows[i]["CustName"].ToString(), ViewState["RID"].ToString(), "0");
                        else
                            resultout = AT.LoanAppAuthorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[i]["GLCODE"].ToString(), dt.Rows[i]["SUBGLCODE"].ToString(), dt.Rows[i]["ACCNO"].ToString(), dt.Rows[i]["PARTICULARS"].ToString(), dt.Rows[i]["PARTICULARS2"].ToString(), dt.Rows[i]["AMOUNT"].ToString(), dt.Rows[i]["TRXTYPE"].ToString(), dt.Rows[i]["ACTIVITY"].ToString(), dt.Rows[i]["PmtMode"].ToString(), SetNo1, dt.Rows[i]["InstNo"].ToString(), dt.Rows[i]["InstDate"].ToString(), "0", "0", "1003", "", dt.Rows[i]["BRCD"].ToString(), Session["MID"].ToString(), TxtPtype.Text.Trim().ToString() + "/" + ViewState["ACCNO"].ToString(), "0", PAYMAST, dt.Rows[i]["CUSTNO"].ToString(), dt.Rows[i]["CustName"].ToString(), ViewState["RID"].ToString(), "0");

                        if ((dt.Rows[i]["TrxType"].ToString() == "1") && (dt.Rows[i]["GLCODE"].ToString() == "3"))
                        {
                            CLS.OldAccClose(Session["BRCD"].ToString(), dt.Rows[i]["SUBGLCODE"].ToString(), dt.Rows[i]["ACCNO"].ToString(), Session["EntryDate"].ToString());
                        }
                    }

                    //Get All Transaction From Temporary Table (TempLnTrx)
                    dt = new DataTable();
                    dt = MV.GetLnTrxTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //Insert Data to Original Table Here
                        if (dt.Rows[i]["Activity"].ToString() == "6")
                            resultout = ITrans.LoanTrx(Session["BRCD"].ToString(), dt.Rows[i]["LoanGlCode"].ToString(), dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["AccountNo"].ToString(), dt.Rows[i]["HeadDesc"].ToString(), dt.Rows[i]["TrxType"].ToString(), dt.Rows[i]["Activity"].ToString(), dt.Rows[i]["Narration"].ToString(), dt.Rows[i]["Amount"].ToString(), SetNo2, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), "0");
                        else
                            resultout = ITrans.LoanTrx(Session["BRCD"].ToString(), dt.Rows[i]["LoanGlCode"].ToString(), dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["AccountNo"].ToString(), dt.Rows[i]["HeadDesc"].ToString(), dt.Rows[i]["TrxType"].ToString(), dt.Rows[i]["Activity"].ToString(), dt.Rows[i]["Narration"].ToString(), dt.Rows[i]["Amount"].ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), "0");
                    }

                    grdvoucher.Visible = false;
                    //  Delete All Data From Temporary Table Here
                    MV.DelAllRecTable(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (resultout > 0)
                    {
                        BtnPost.Enabled = false;
                        WebMsgBox.Show("Successfully Transfer With Set No : " + SetNo1 + " and " + SetNo2 + " ...!! and New A/C = " + ViewState["ACCNO"].ToString() + " ...!!", this.Page);
                        CLS.UpdateF7(TxtBondNo.Text, TxtAppNo.Text, TxtCustno.Text, TxtPtype.Text, Session["BRCD"].ToString(), SetNo1);
                        CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanBondIssue_Transfer _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearAllInfo();
                        return;
                    }
                }
            }
            else
            {
                BtnPost.Focus();
                WebMsgBox.Show("Amount Difference in Credit and Debit Transaction ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void GetPostinfo()
    {
        DataTable dt = new DataTable();
        try
        {
            string PAYMAST = "LOANAPP";
            string ActivityType = "";

            if (Convert.ToDouble(txtCrAmount.Text.Trim().ToString() == "" ? "0" : txtCrAmount.Text.Trim().ToString()) == Convert.ToDouble(txtDrAmount.Text.Trim().ToString() == "" ? "0" : txtDrAmount.Text.Trim().ToString()))
            {
                //Get All Transaction From Temporary Table
                dt = MV.GetTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                if (dt.Rows.Count > 0)
                {
                    SetNo1 = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString());

                    ActivityType = MV.GetTransDetails_Activity(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (ActivityType.ToString() == "6")
                    {
                        SetNo2 = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString());
                    }

                    string RefNo = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                    ViewState["RID"] = (Convert.ToInt32(RefNo) + 1).ToString();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //Insert Data to Original Table Here
                        if (dt.Rows[i]["Activity"].ToString() == "6")
                            resultout = AT.LoanAppAuthorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[i]["GLCODE"].ToString(), dt.Rows[i]["SUBGLCODE"].ToString(), dt.Rows[i]["ACCNO"].ToString(), dt.Rows[i]["PARTICULARS"].ToString(), dt.Rows[i]["PARTICULARS2"].ToString(), dt.Rows[i]["AMOUNT"].ToString(), dt.Rows[i]["TRXTYPE"].ToString(), dt.Rows[i]["ACTIVITY"].ToString(), dt.Rows[i]["PmtMode"].ToString(), SetNo2, dt.Rows[i]["InstNo"].ToString(), dt.Rows[i]["InstDate"].ToString(), "0", "0", "1003", "", dt.Rows[i]["BRCD"].ToString(), Session["MID"].ToString(), TxtPtype.Text.Trim().ToString() + "/" + ViewState["ACCNO"].ToString(), "0", PAYMAST, dt.Rows[i]["CUSTNO"].ToString(), dt.Rows[i]["CustName"].ToString(), ViewState["RID"].ToString(), "0");
                        else
                            resultout = AT.LoanAppAuthorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[i]["GLCODE"].ToString(), dt.Rows[i]["SUBGLCODE"].ToString(), dt.Rows[i]["ACCNO"].ToString(), dt.Rows[i]["PARTICULARS"].ToString(), dt.Rows[i]["PARTICULARS2"].ToString(), dt.Rows[i]["AMOUNT"].ToString(), dt.Rows[i]["TRXTYPE"].ToString(), dt.Rows[i]["ACTIVITY"].ToString(), dt.Rows[i]["PmtMode"].ToString(), SetNo1, dt.Rows[i]["InstNo"].ToString(), dt.Rows[i]["InstDate"].ToString(), "0", "0", "1003", "", dt.Rows[i]["BRCD"].ToString(), Session["MID"].ToString(), TxtPtype.Text.Trim().ToString() + "/" + ViewState["ACCNO"].ToString(), "0", PAYMAST, dt.Rows[i]["CUSTNO"].ToString(), dt.Rows[i]["CustName"].ToString(), ViewState["RID"].ToString(), "0");

                        if ((dt.Rows[i]["TrxType"].ToString() == "1") && (dt.Rows[i]["GLCODE"].ToString() == "3"))
                        {
                            CLS.OldAccClose(Session["BRCD"].ToString(), dt.Rows[i]["SUBGLCODE"].ToString(), dt.Rows[i]["ACCNO"].ToString(), Session["EntryDate"].ToString());
                        }
                    }

                    //Get All Transaction From Temporary Table (TempLnTrx)
                    dt = new DataTable();
                    dt = MV.GetLnTrxTransDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //Insert Data to Original Table Here
                        if (dt.Rows[i]["Activity"].ToString() == "6")
                        {
                            CC.UpdateChqStatus(Session["BRCD"].ToString(), txtProdType1.Text.ToString(), TxtAccNo1.Text.ToString(), TxtChequeNo.Text.ToString(), Session["EntryDate"].ToString(), SetNo2, Session["MID"].ToString());
                            resultout = ITrans.LoanTrx(Session["BRCD"].ToString(), dt.Rows[i]["LoanGlCode"].ToString(), dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["AccountNo"].ToString(), dt.Rows[i]["HeadDesc"].ToString(), dt.Rows[i]["TrxType"].ToString(), dt.Rows[i]["Activity"].ToString(), dt.Rows[i]["Narration"].ToString(), dt.Rows[i]["Amount"].ToString(), SetNo2, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), "0");
                        }
                        else
                        {
                            resultout = ITrans.LoanTrx(Session["BRCD"].ToString(), dt.Rows[i]["LoanGlCode"].ToString(), dt.Rows[i]["SubGlCode"].ToString(), dt.Rows[i]["AccountNo"].ToString(), dt.Rows[i]["HeadDesc"].ToString(), dt.Rows[i]["TrxType"].ToString(), dt.Rows[i]["Activity"].ToString(), dt.Rows[i]["Narration"].ToString(), dt.Rows[i]["Amount"].ToString(), SetNo1, "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), "0");
                        }
                    }

                    grdvoucher.Visible = false;
                    //  Delete All Data From Temporary Table Here
                    MV.DelAllRecTable(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                    if (resultout > 0)
                    {
                        BtnPost.Enabled = false;
                        WebMsgBox.Show("Successfully Transfer With Set No : " + SetNo1 + " and " + SetNo2 + " ...!! and New A/C = " + ViewState["ACCNO"].ToString() + " ...!!", this.Page);
                        CLS.UpdateF7(TxtBondNo.Text, TxtAppNo.Text, TxtCustno.Text, TxtPtype.Text, Session["BRCD"].ToString(), SetNo1);
                        CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanBondIssue_Transfer _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearAllInfo();
                        return;
                    }
                }
            }
            else
            {
                BtnPost.Focus();
                WebMsgBox.Show("Amount Difference in Credit and Debit Transaction ...!!", this.Page);
                return;
            }
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
            DT = MV.GetCRDR(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (DT.Rows.Count > 0)
            {
                txtDrAmount.Text = DT.Rows[0]["DEBIT"].ToString();
                txtCrAmount.Text = DT.Rows[0]["CREDIT"].ToString();
                double CR, DR;
                CR = DR = 0;
                CR = Convert.ToDouble(txtCrAmount.Text);
                DR = Convert.ToDouble(txtDrAmount.Text);

                DataTable DT1 = new DataTable();
                DT1 = MV.GetCRVoucher(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                if (DT1.Rows.Count > 0)
                {
                    grdCredit.DataSource = DT1;
                    grdCredit.DataBind();
                }

                DataTable DT2 = new DataTable();
                DT2 = MV.GetDRVoucher(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                if (DT2.Rows.Count > 0)
                {
                    grdDebit.DataSource = DT2;
                    grdDebit.DataBind();
                }

                if (CR == DR)
                {
                    BtnPost.Enabled = true;
                    BtnPost.Focus();
                }
                else
                {
                    BtnPost.Enabled = false;
                    TxtPtype.Focus();
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
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void BindGrid()
    {
        try
        {
            int RC = MV.Getinfotable(grdvoucher, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (grdvoucher.Rows.Count > 0)
            {

            }
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

    protected void GrsLoanInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrsLoanInfo.PageIndex = e.NewPageIndex;
            BindGridLoan();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindGridLoan()
    {
        try
        {
            Result = LI.BindGrid(GrsLoanInfo, TxtPtype.Text, ViewState["ACCNO"].ToString(), TxtBrcd.Text, "");
            div_GridVw.Visible = true;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void GrdLoanSurity1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdLoanSurity1.PageIndex = e.NewPageIndex;
            BindGrid2(ViewState["ACCNO"].ToString(), Session["BRCD"].ToString(), TxtPtype.Text);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindGrid2(string accno, string brcd, string subgl)
    {
        try
        {
            int Result;
            Result = SD.BindGrid2(GrdLoanSurity1, brcd, accno, subgl);
            div_grd1.Visible = true;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
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
                txtAmount.Text = TxtNetPaid.Text.Trim().ToString() == "" ? "0" : TxtNetPaid.Text.Trim().ToString();
                Clear();
                BtnVouchCr.Focus();
            }
            else if (ddlPayType.SelectedValue.ToString() == "2")
            {
                ViewState["PAYTYPE"] = "TRANSFER";
                Transfer.Visible = true;
                Transfer1.Visible = false;
                DivAmount.Visible = true;
                txtNarration.Text = "By TRF";
                autoglname1.ContextKey = TxtBrcd.Text.Trim().ToString();
                txtAmount.Text = TxtNetPaid.Text.Trim().ToString() == "" ? "0" : TxtNetPaid.Text.Trim().ToString();

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
                autoglname1.ContextKey = TxtBrcd.Text.Trim().ToString();
                txtAmount.Text = TxtNetPaid.Text.Trim().ToString() == "" ? "0" : TxtNetPaid.Text.Trim().ToString();

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
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
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
            txtBalance.Text = "";
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

    protected void ClearAllInfo()
    {
        try
        {
            Clear();
            TxtPtype.Text = "";
            TxtPname.Text = "";
            TxtAppNo.Text = "";
            TxtCustno.Text = "";
            TxtCustname.Text = "";
            TxtLoanSanc.Text = "";
            TxtBondNo.Text = "";
            TxtDeduct.Text = "";
            TxtNetPaid.Text = "";

            txtCrAmount.Text = "";
            txtDrAmount.Text = "";
            grdCredit.DataSource = null;
            grdCredit.DataBind();
            grdDebit.DataSource = null;
            grdDebit.DataBind();

            ddlPayType.SelectedValue = "0";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtProdType1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1 = CLS.Getaccno(txtProdType1.Text, TxtBrcd.Text.Trim().ToString());
            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                ViewState["GLCODE1"] = AC[0].ToString();
                txtProdName1.Text = AC[1].ToString();
                AutoAccname1.ContextKey = TxtBrcd.Text.Trim().ToString() + "_" + txtProdType1.Text + "_" + ViewState["GLCODE1"].ToString();

                if (Convert.ToInt32(ViewState["GLCODE1"].ToString() == "" ? "0" : ViewState["GLCODE1"].ToString()) >= 100)
                {
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";

                    TxtAccNo1.Text = txtProdType1.Text.ToString();
                    TxtAccName1.Text = txtProdName1.Text.ToString();

                    txtBalance.Text = CLS.GetOpenClose(TxtBrcd.Text.Trim().ToString(), txtProdType1.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();

                    TxtChequeNo.Focus();
                }
                else
                {
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";
                    txtBalance.Text = "";

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
                txtBalance.Text = "";
                txtProdType1.Focus();
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
                txtProdName1.Text = custnob[0].ToString();
                txtProdType1.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                string[] AC = CLS.Getaccno(txtProdType1.Text, TxtBrcd.Text.Trim().ToString()).Split('_');
                ViewState["GLCODE1"] = AC[0].ToString();
                AutoAccname1.ContextKey = TxtBrcd.Text.Trim().ToString() + "_" + txtProdType1.Text + "_" + ViewState["GLCODE1"].ToString();

                if (Convert.ToInt32(ViewState["GLCODE1"].ToString() == "" ? "0" : ViewState["GLCODE1"].ToString()) >= 100)
                {
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";

                    TxtAccNo1.Text = txtProdType1.Text.ToString();
                    TxtAccName1.Text = txtProdName1.Text.ToString();

                    txtBalance.Text = CLS.GetOpenClose(TxtBrcd.Text.Trim().ToString(), txtProdType1.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();

                    TxtChequeNo.Focus();
                }
                else
                {
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";
                    txtBalance.Text = "";

                    TxtAccNo1.Focus();
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
            AT = BD.Getstage1(TxtAccNo1.Text, TxtBrcd.Text.Trim().ToString(), txtProdType1.Text);
            if (AT != null)
            {
                if (AT != "1003")
                {
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";
                    TxtAccNo1.Focus();
                    WebMsgBox.Show("Sorry account not authorise ...!!", this.Page);
                    return;
                }
                else
                {
                    DataTable DT = new DataTable();
                    DT = CLS.GetCustName(txtProdType1.Text, TxtAccNo1.Text, TxtBrcd.Text.Trim().ToString());
                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                        TxtAccName1.Text = CustName[0].ToString();

                        txtBalance.Text = CLS.GetOpenClose(TxtBrcd.Text.Trim().ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
                        TxtChequeNo.Focus();
                    }
                }
            }
            else
            {
                TxtAccNo1.Text = "";
                TxtAccName1.Text = "";
                TxtAccNo1.Focus();
                WebMsgBox.Show("Enter valid account number ...!!", this.Page);
                return;
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
            string[] custnob = TxtAccName1.Text.Split('_');
            if (custnob.Length > 1)
            {
                TxtAccNo1.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string AT = BD.Getstage1(TxtAccNo1.Text, TxtBrcd.Text.Trim().ToString(), txtProdType1.Text);
                if (AT != null)
                {
                    if (AT != "1003")
                    {
                        TxtAccNo1.Text = "";
                        TxtAccName1.Text = "";
                        TxtAccNo1.Focus();
                        WebMsgBox.Show("Sorry account not authorise ...!!", this.Page);
                        return;
                    }
                    else
                    {
                        DataTable DT = new DataTable();
                        DT = CLS.GetCustName(txtProdType1.Text, TxtAccNo1.Text, TxtBrcd.Text.Trim().ToString());
                        if (DT.Rows.Count > 0)
                        {
                            string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                            TxtAccName1.Text = CustName[0].ToString();

                            txtBalance.Text = CLS.GetOpenClose(TxtBrcd.Text.Trim().ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();
                            TxtChequeNo.Focus();
                        }
                    }
                }
                else
                {
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";
                    TxtAccNo1.Focus();
                    WebMsgBox.Show("Enter valid account number ...!!", this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int Res = 0;
        LinkButton objlnk = (LinkButton)sender;
        string[] AT = objlnk.CommandArgument.ToString().Split('_');
        ViewState["SetNo"] = AT[0].ToString();
        ViewState["ScrollNo"] = AT[1].ToString();
        ViewState["EntryMid"] = AT[2].ToString();
        Res = CLS.CancelEntryLoan(Session["BRCD"].ToString(), TxtCustno.Text, TxtPtype.Text, ViewState["SetNo"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());
        if (Res > 0)
        {
            WebMsgBox.Show("SetNo " + ViewState["SetNo"].ToString() + " deleted Successfully!!!!", this.Page);
            BindVoucher();
        }
    }

    public void BindVoucher()
    {
        try
        {
            DataTable dt = new DataTable();
            if (TxtCustno.Text != "" && TxtPtype.Text != "")
            {
                dt = CLS.Getinfotable_All(grdShow, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), TxtCustno.Text);
                if (dt.Rows.Count > 0)
                {
                    grdShow.DataSource = dt;
                    grdShow.DataBind();
                }
                else
                {
                    grdShow.DataSource = null;
                    grdShow.DataBind();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtCustno_TextChanged(object sender, EventArgs e)
    {
        BindVoucher();
    }

    protected void BtnAllVoucher_Click(object sender, EventArgs e)
    {
        try
        {
            GetBondinfo();
            if (Result > 0)
            {
                GetSurityinfo();
            }
            if (resultint > 0)
            {
                GetVoucherinfo();
            }
            if (resultout > 0)
            {
                GetPostinfo();
            }
            if (resultout > 0)
            {
                BtnBondCr.Enabled = false;
                BtnSurCr.Enabled = false;
                BtnVouchCr.Enabled = false;
                BtnPost.Enabled = false;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtChequeNo_TextChanged(object sender, EventArgs e)
    {
        try
        {

            int ChqLen = 0;
            string Chqno_Text = "";
            Chqno_Text = TxtChequeNo.Text;
            ChqLen = Chqno_Text.Length;
            if (ChqLen > 6 || ChqLen < 6)
            {
                WebMsgBox.Show("Enter 6 digit Instrument number ....!!", this.Page);
                TxtChequeNo.Text = "";
                TxtChequeNo.Focus();
            }
            else
            {
                string InstStatus = "";
                try
                {
                    string Para = Convert.ToString(conn.sExecuteScalar("select LISTVALUE from PARAMETER where LISTFIELD='ChequeValidate'"));

                    if (Para == "Y")
                    {
                        InstStatus = CC.ChequeExists(Session["BRCD"].ToString(), txtProdType1.Text.ToString(), TxtAccNo1.Text.ToString(), TxtChequeNo.Text.ToString()).ToString();
                        if ((InstStatus != null) && (InstStatus != "Issued"))
                        {
                            TxtChequeNo.Text = "";
                            TxtChequeNo.Focus();
                            WebMsgBox.Show("Instrument number " + TxtChequeNo.Text.ToString() + " with status - " + InstStatus.ToString(), this.Page);
                            return;
                        }
                        else
                        {
                            if (TxtChequeNo.Text == "")
                                TxtChequeNo.Text = Session["EntryDate"].ToString();
                            txtNarration.Focus();
                            return;
                        }
                    }
                    else
                    {
                        if (TxtChequeNo.Text == "")
                            TxtChequeNo.Text = Session["EntryDate"].ToString();
                        txtNarration.Focus();
                        return;
                    }
                }
                catch (Exception Ex)
                {
                    ExceptionLogging.SendErrorToText(Ex);
                }
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}