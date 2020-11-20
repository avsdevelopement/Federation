using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmAVS5033 : System.Web.UI.Page
{
    ClsInvCreateReceipt ICR = new ClsInvCreateReceipt();
    ClsBindDropdown BD = new ClsBindDropdown();
    scustom customcs = new scustom();
    ClsInvAccountMaster IAM = new ClsInvAccountMaster();
    DataTable DT = new DataTable();
    CLsShareCertificate SC = new CLsShareCertificate();
    ClsInvClosure IC = new ClsInvClosure();
    DbConnection conn = new DbConnection();
    int resultint = 0;
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
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
                customcs.BindInvType(ddlInvestment);
                IntAutoGlName.ContextKey = Session["BRCD"].ToString();
                PrinAutoGlName.ContextKey = Session["BRCD"].ToString();
                BD.BindIntrstPayout(ddlIntrestPay);
                txtDepDate.Text = Session["EntryDate"].ToString();
                txtBankNo.Focus();
                bindgrid();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void txtBankNo_TextChanged(object sender, EventArgs e)
    {
        string GLNAME = SC.getglname(Session["BRCD"].ToString(), txtBankNo.Text);
        if (GLNAME == "" || GLNAME == null)
        {
            lblMessage.Text = "Enter valid Product code...!!";
            ModalPopup.Show(this.Page);
        }
        else
        {
            string ACCNO = SC.GETMAXACCNO(Session["BRCD"].ToString(), txtBankNo.Text);
            if (ACCNO == null || ACCNO == "")
            {
                int DEFAULT = 1;
                txtAC.Text = DEFAULT.ToString();
            }
            txtBankName.Text = GLNAME;
            txtAC.Text = ACCNO;
            DT = IC.BINDGRID(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), txtBankNo.Text);  //Session["BRCD"].ToString() ADDED  BY ASHOK MISAL AS PER SIR REQ --(PRIVOUS HARDCODE 1)
            if (DT.Rows.Count > 0)
            {
                int sum = 0;
                sum = Convert.ToInt32(DT.Compute("SUM(CLOSINGBAL)", string.Empty));
                //foreach (DataRow dr in DT.Rows)
                //{
                //    sum += Convert.ToInt32(dr["CLOSINGBAL"]);
                //}
                TxtClBal.Text = sum.ToString();
            }
            else
                TxtClBal.Text = "0";
            txtBank1.Focus();
            resultint = IAM.GetAllData11(grdInvAccMaster, Session["EntryDate"].ToString(), Session["BRCD"].ToString(), txtBankNo.Text, txtAC.Text);
            if (resultint <= 0)
                resultint = IAM.GetAllData11(grdInvAccMaster, Session["EntryDate"].ToString(), Session["BRCD"].ToString(), txtBankNo.Text, "");

        }

    }
    protected void txtBankName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtIntProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {

            string AC1;
            AC1 = ICR.Getaccno(txtIntProdCode.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                ViewState["IntGlCode"] = AC[0].ToString();
                txtIntProdName.Text = AC[1].ToString();
            }
            else
            {
                txtIntProdCode.Text = "";
                txtIntProdName.Text = "";
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);

                txtIntProdCode.Focus();
                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtIntProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtIntProdName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtIntProdName.Text = custnob[0].ToString();
                txtIntProdCode.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] AC = ICR.Getaccno(txtIntProdCode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["IntGlCode"] = AC[0].ToString();

                //IntAutoAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtIntProdCode.Text + "_" + ViewState["IntGlCode"].ToString();

                //txtIntAccNo.Text = "";
                //txtIntAccName.Text = "";

                //txtIntAccNo.Focus();
            }
            else
            {
                txtIntProdCode.Text = "";
                txtIntProdName.Text = "";
                //txtIntAccNo.Text = "";
                //txtIntAccName.Text = "";

                txtIntProdCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtPrinProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;
            AC1 = ICR.Getaccno(txtPrinProdCode.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                ViewState["PrinGlCode"] = AC[0].ToString();
                txtPrinProdName.Text = AC[1].ToString();
                txtPrinAccNo.Text = ICR.GetAcc(txtPrinProdCode.Text, Session["BRCD"].ToString());
                txtPrinAccName.Text = txtReceiptName.Text;

                txtPrinAccNo.Focus();
            }
            else
            {
                txtPrinProdCode.Text = "";
                txtPrinProdName.Text = "";
                txtPrinAccNo.Text = "";
                txtPrinAccName.Text = "";

                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);

                txtPrinProdCode.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtPrinProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtPrinProdName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtPrinProdName.Text = custnob[0].ToString();
                txtPrinProdCode.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] AC = ICR.Getaccno(txtPrinProdCode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["PrinGlCode"] = AC[0].ToString();
                txtPrinAccNo.Text = ICR.GetAcc(txtPrinProdCode.Text, Session["BRCD"].ToString());
                txtPrinAccName.Text = txtReceiptName.Text;
                txtPrinAccNo.Focus();
            }
            else
            {
                txtPrinProdCode.Text = "";
                txtPrinProdName.Text = "";
                txtPrinAccNo.Text = "";
                txtPrinAccName.Text = "";

                txtPrinProdCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtPrinAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";
            AT = BD.Getstage1(txtPrinAccNo.Text, Session["BRCD"].ToString(), txtPrinProdCode.Text);
            if (AT != null)
            {
                if (AT != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise...!!";
                    ModalPopup.Show(this.Page);

                    txtPrinAccNo.Text = "";
                    txtPrinAccName.Text = "";

                    txtPrinAccNo.Focus();
                    return;
                }
                else
                {
                    DT = new DataTable();
                    DT = ICR.GetCustName(txtPrinProdCode.Text, txtPrinAccNo.Text, Session["BRCD"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                        txtPrinAccName.Text = CustName[0].ToString();
                    }


                }
            }
            else
            {
                lblMessage.Text = "Enter valid account number...!!";
                ModalPopup.Show(this.Page);

                txtPrinAccNo.Text = "";
                txtPrinAccName.Text = "";

                txtPrinAccNo.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtPrinAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtPrinAccName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtPrinAccName.Text = custnob[0].ToString();
                txtPrinAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                //ddlPayType.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtDepAmt_TextChanged(object sender, EventArgs e)
    {
        txtPeriod.Text = "";
        txtIntRate.Text = "";
        txtIntAmt.Text = "";
        txtMaturityAmt.Text = "";
        txtDueDate.Text = "";

        ddlDuration.Focus();
    }
    protected void txtPeriod_TextChanged(object sender, EventArgs e)
    {
        try
        {
             if (txtDepAmt.Text == "")
            {
                lblMessage.Text = "First Enter Amount...!!";
                ModalPopup.Show(this.Page);
                txtDepAmt.Focus();
                return;
            }

            if (txtPeriod.Text == "")
            {
                return;
            }
            if (ddlDuration.SelectedValue == "M")
            {
                txtDueDate.Text = conn.AddMonthDay(txtDepDate.Text, txtPeriod.Text, "M").Replace("12:00:00", "");
                txtDueDate.Text = conn.AddMonthDay(txtDepDate.Text, txtPeriod.Text, "M").Replace("12:00:00", "");
            }
            else if (ddlDuration.SelectedValue == "D")
            {
                txtDueDate.Text = conn.AddMonthDay(txtDepDate.Text, txtPeriod.Text, "D").Replace("12:00:00", "");
                txtDueDate.Text = conn.AddMonthDay(txtDepDate.Text, txtPeriod.Text, "D").Replace("12:00:00", "");
            }
            txtIntRate.Focus();

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void txtIntRate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtIntRate.Text != "")
            {
                float amt = (float)Convert.ToDouble(txtDepAmt.Text);
                float intrate = (float)Convert.ToDouble(txtIntRate.Text);
                CalDepositInterest(amt, txtPrinProdCode.Text.ToString(), intrate, Convert.ToInt32(txtPeriod.Text), ddlIntrestPay.SelectedItem.Text.ToString(), ddlDuration.SelectedItem.Text.ToString());
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtClBal.Text == "0")
            {
                WebMsgBox.Show("Restricted..!! Closing balance is 0..!", this.Page);
                return;
            }
            else
            {  //Session["BRCD"].ToString() ADDED BY ASHOK DUE TO HARD CODE BRCD
                resultint = IAM.AddInvestment(Session["BRCD"].ToString(), txtBankNo.Text.Trim().ToString(), txtBankName.Text.Trim().ToString(), "0", txtBranchName.Text.Trim().ToString(), txtReceiptNo.Text.Trim().ToString(), txtReceiptName.Text.Trim().ToString(), txtBResNo.Text.Trim().ToString(), txtBMeetDate.Text.Trim().ToString(), txtOpenDate.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), txtPrinAccNo.Text, txtPrinProdCode.Text, "0", txtIntProdCode.Text, ddlInvestment.SelectedValue, txtAC.Text, txtBank1.Text, txtPrinAccName.Text, txtPeriod.Text.Trim().ToString(), Convert.ToDouble(txtIntRate.Text.Trim().ToString()), Convert.ToDouble(txtDepAmt.Text.Trim().ToString()), Convert.ToDouble(txtIntAmt.Text.Trim().ToString()), Convert.ToDouble(txtMaturityAmt.Text.Trim().ToString()), ddlIntrestPay.SelectedValue.ToString(), ddlDuration.SelectedValue.ToString(), txtDueDate.Text.Trim().ToString(),"1");

                if (resultint > 0)
                {
                    lblMessage.Text = "Record Added Successfully...!!";
                    ModalPopup.Show(this.Page);
                    bindgrid();
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "InvstDataEntry_Add _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    ClearData();
                    ClearAllData();
                   
                }
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {

    }
    protected void ClearData()
    {
        try
        {
            txtBank1.Text = "";
            ddlInvestment.SelectedValue = "0";
            txtBankNo.Text = "";
            txtBankName.Text = "";

            //txtBranchNo.Text = "";
            txtBranchName.Text = "";

            txtReceiptNo.Text = "";
            txtReceiptName.Text = "";

            txtBMeetDate.Text = Session["EntryDate"].ToString();
            txtOpenDate.Text = Session["EntryDate"].ToString();
            txtBResNo.Text = "";

            txtIntProdCode.Text = "";
            txtIntProdName.Text = "";
            //txtIntAccNo.Text = "";
            //txtIntAccName.Text = "";

            txtPrinProdCode.Text = "";
            txtPrinProdName.Text = "";
            txtPrinAccNo.Text = "";
            txtPrinAccName.Text = "";

            txtBankNo.Focus();
            TxtClBal.Text="";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ClearAllData()
    {
        try
        {
            txtBank1.Text = "";
            ddlInvestment.SelectedValue = "0";
            txtBankNo.Text = "";
            txtBankName.Text = "";
            txtBranchName.Text = "";
            txtReceiptNo.Text = "";
            txtReceiptName.Text = "";
            txtBResNo.Text = "";
            txtBMeetDate.Text = Session["EntryDate"].ToString();
            txtOpenDate.Text = Session["EntryDate"].ToString();

            txtIntProdCode.Text = "";
            txtIntProdName.Text = "";
            txtPrinProdCode.Text = "";
            txtPrinProdName.Text = "";
            txtPrinAccNo.Text = "";
            txtPrinAccName.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        try
        {
            ClearData();
            ClearAllData();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }
    public void bindgrid()
    {
        try
        {
            IAM.getglname(grdGlname);
            resultint = IAM.GetAllData11(grdInvAccMaster, Session["EntryDate"].ToString(), Session["BRCD"].ToString(), txtBankNo.Text, txtAC.Text);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkEdit_Click1(object sender, EventArgs e)
    {
        try
        {
             LinkButton objlink = (LinkButton)sender;
             string[] strnumId = objlink.CommandArgument.ToString().Split('-');
             txtBankNo.Text = strnumId[0].ToString();
             txtBankName.Text = strnumId[1].ToString();
             string ACCNO = SC.GETMAXACCNO(Session["BRCD"].ToString(), txtBankNo.Text);
             if (ACCNO == null || ACCNO == "")
             {
                 int DEFAULT = 1;
                 txtAC.Text = DEFAULT.ToString();
             }
             txtAC.Text = ACCNO;
             DT = IC.BINDGRID("1", Session["EntryDate"].ToString(), txtBankNo.Text);
             if (DT.Rows.Count > 0)
             {
                 int sum = 0;
                 foreach (DataRow dr in DT.Rows)
                 {
                     sum += Convert.ToInt32(dr["CLOSINGBAL"]);
                 }
                 TxtClBal.Text = sum.ToString();
             }
             else
                 TxtClBal.Text = "0";
             btnCreate.Visible = true;
             btnModify.Visible = false;
             txtBank1.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void grdGlname_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdGlname.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void CalDepositInterest(float amt, string subgl, float intrate, int Period, string intpay, string PTYPE)
    {
        try
        {
            float interest = 0, maturityamt = 0, QUATERS = 0, tmp1 = 0, tmp2 = 0;

            //string sql = "SELECT CATEGORY FROM DEPOSITGL where DEPOSITGLCODE='" + subgl + "' AND BRCD='" + Session["BRCD"].ToString() + "'";
            string category = "FDS";
            //string category = conn.sExecuteScalar(Value);

            if (category == "")
            {
                return;
            }

            switch (category)
            {
                case "DD":
                    interest = amt;
                    maturityamt = interest + amt;
                    txtIntAmt.Text = interest.ToString("N");
                    txtMaturityAmt.Text = maturityamt.ToString("N");
                    break;

                case "MIS":
                    interest = Convert.ToInt32(amt * intrate / 1209.75);
                    maturityamt = interest + amt;
                    txtIntAmt.Text = interest.ToString("N");
                    txtMaturityAmt.Text = maturityamt.ToString("N");
                    break;

                case "CUM":
                    QUATERS = (Period / 3);
                    maturityamt = Convert.ToInt32((Math.Pow(((intrate / 400) + 1), (QUATERS))) * amt);
                    interest = maturityamt - amt;
                    txtIntAmt.Text = interest.ToString("N");
                    txtMaturityAmt.Text = maturityamt.ToString("N");
                    break;

                case "FDS":
                    if (intpay == "On Maturity" || intpay == "ON MATURITY")
                    {
                        if (PTYPE == "Days" || PTYPE == "DAYS" || PTYPE == "idvasa")
                        {
                            interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 365) * (Period));
                            maturityamt = (interest) + (amt);
                            txtIntAmt.Text = interest.ToString("N");
                            txtMaturityAmt.Text = maturityamt.ToString("N");
                        }
                        else if (PTYPE == "Months" || PTYPE == "MONTHS" || PTYPE == "maihnao")
                        {
                            interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (Period));
                            maturityamt = (interest) + (amt);
                            txtIntAmt.Text = interest.ToString("N");
                            txtMaturityAmt.Text = maturityamt.ToString("N");
                        }
                    }
                    else if (intpay == "Quaterly" || intpay == "QUATERLY")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (3));
                        maturityamt = interest + amt;
                        txtIntAmt.Text = interest.ToString("N");
                        txtMaturityAmt.Text = maturityamt.ToString("N");
                    }
                    else if (intpay == "Half Yearly" || intpay == "HALF YEARLY")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (6));
                        maturityamt = interest + amt;
                        txtIntAmt.Text = interest.ToString("N");
                        txtMaturityAmt.Text = maturityamt.ToString("N");
                    }

                    else if (intpay == "Monthly" || intpay == "MONTHLY" || PTYPE == "maihano")
                    {
                        interest = Convert.ToInt32((amt) * (intrate) / 1209.75);
                        maturityamt = interest + amt;
                        txtIntAmt.Text = interest.ToString("N");
                        txtMaturityAmt.Text = maturityamt.ToString("N");
                    }
                    break;

                case "DP":
                    if (PTYPE == "Days" || PTYPE == "DAYS" || PTYPE == "idvasa")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 365) * (Period));
                        maturityamt = (interest) + (amt);
                        txtIntAmt.Text = interest.ToString("N");
                        txtMaturityAmt.Text = maturityamt.ToString("N");
                    }
                    else if (PTYPE == "Months" || PTYPE == "MONTHS" || PTYPE == "maihnao")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (Period));
                        maturityamt = (interest) + (amt);
                        txtIntAmt.Text = interest.ToString("N");
                        txtMaturityAmt.Text = maturityamt.ToString("N");
                    }
                    break;

                case "RD":
                    float deamt = amt;
                    float tempamt = amt;
                    string SQL = "";
                    float temprate = intrate;
                    conn.sExecuteQuery("delete from RRD");

                    string RDPARA = conn.sExecuteScalar("SELECT LISTVALUE FROM PARAMETER WHERE LISTFIELD='RDINT'");

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

                            tmp1 = (float)Convert.ToDouble(interest1);
                            txtIntAmt.Text = tmp1.ToString("N");
                            tmp2 = (float)Convert.ToDouble(MAMT);
                            txtMaturityAmt.Text = tmp2.ToString("N");
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
                            txtIntAmt.Text = interest.ToString("N");
                            txtMaturityAmt.Text = maturityamt.ToString("N");
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
    protected void lnkEdit_Click2(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string strnumid = objlink.CommandArgument;
            string[] arr = strnumid.Split('_');
            string Branch = objlink.CommandName;
            btnCreate.Visible = false;
            btnModify.Visible = true;
            CallData(arr[0].ToString(), arr[1].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void CallData(string Identity, string BankCode)
    {
        try
        {
            DT = IAM.GetInfo1(Identity, BankCode);
            if (DT.Rows.Count > 0)
            {
                ddlInvestment.SelectedValue= DT.Rows[0]["GlGroup"].ToString();
                txtBankNo.Text = DT.Rows[0]["SubGlCode"].ToString();
                txtBankName.Text = DT.Rows[0]["GlName"].ToString();
                txtAC.Text = DT.Rows[0]["CustAccno"].ToString();
                txtBank1.Text = DT.Rows[0]["BankName"].ToString();
                txtBranchName.Text = DT.Rows[0]["Branchname"].ToString();
                txtReceiptNo.Text = DT.Rows[0]["ReceiptNo"].ToString();
                txtReceiptName.Text = DT.Rows[0]["ReceiptName"].ToString();
                txtBResNo.Text = DT.Rows[0]["BoardResNo"].ToString();
                txtBMeetDate.Text = DT.Rows[0]["BoardMeetDate"].ToString();
                txtOpenDate.Text = DT.Rows[0]["openingdate1"].ToString();
                //DataTable DT1 = new DataTable();
                //DT1 = IC.BINDGRID(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), txtBankNo.Text);  //Session["BRCD"].ToString() ADDED  BY ASHOK MISAL AS PER SIR REQ --(PRIVOUS HARDCODE 1)
                //if (DT1.Rows.Count > 0)
                //{
                //    int sum = 0;
                //    foreach (DataRow dr in DT1.Rows)
                //    {
                //        sum += Convert.ToInt32(dr["CLOSINGBAL"]);
                //    }
                //    TxtClBal.Text = sum.ToString();
                //}
                //else
                //    TxtClBal.Text = "0";
                //TxtClBal.Text = DT.Rows[0][""].ToString();
                txtIntProdCode.Text = DT.Rows[0]["CRGL"].ToString();
                txtIntProdName.Text = DT.Rows[0]["CR"].ToString();
                txtPrinProdCode.Text = DT.Rows[0]["RecGL"].ToString();
                txtPrinProdName.Text = DT.Rows[0]["RG"].ToString();
                txtPrinAccNo.Text = DT.Rows[0]["RecAC"].ToString();
                txtPrinAccName.Text = DT.Rows[0]["PRACCNAME"].ToString();
                txtDepDate.Text = DT.Rows[0]["openingdate1"].ToString();
                ddlIntrestPay.Text = DT.Rows[0]["IntPayOut"].ToString();
                txtDepAmt.Text = DT.Rows[0]["principleamt"].ToString();
                ddlDuration.SelectedValue = DT.Rows[0]["PeriodType"].ToString();
                txtPeriod.Text = DT.Rows[0]["Period"].ToString();
                txtIntRate.Text = DT.Rows[0]["RateOfInt"].ToString();
                txtIntAmt.Text = DT.Rows[0]["InterestAmt"].ToString();
                txtMaturityAmt.Text = DT.Rows[0]["MaturityAmt"].ToString();
                txtDueDate.Text = DT.Rows[0]["DueDate"].ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        resultint = IAM.AddInvestment(Session["BRCD"].ToString(), txtBankNo.Text.Trim().ToString(), txtBankName.Text.Trim().ToString(), "0", txtBranchName.Text.Trim().ToString(), txtReceiptNo.Text.Trim().ToString(), txtReceiptName.Text.Trim().ToString(), txtBResNo.Text.Trim().ToString(), txtBMeetDate.Text.Trim().ToString(), txtOpenDate.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), txtPrinAccNo.Text, txtPrinProdCode.Text, "0", txtIntProdCode.Text, ddlInvestment.SelectedValue, txtAC.Text, txtBank1.Text, txtPrinAccName.Text, txtPeriod.Text.Trim().ToString(), Convert.ToDouble(txtIntRate.Text.Trim().ToString()), Convert.ToDouble(txtDepAmt.Text.Trim().ToString()), Convert.ToDouble(txtIntAmt.Text.Trim().ToString()), Convert.ToDouble(txtMaturityAmt.Text.Trim().ToString()), ddlIntrestPay.SelectedValue.ToString(), ddlDuration.SelectedValue.ToString(), txtDueDate.Text.Trim().ToString(),"2");

        if (resultint > 0)
        {
            ClearData();
            ClearAllData();
            lblMessage.Text = "Record Modify Successfully...!!";
            ModalPopup.Show(this.Page);
            bindgrid();
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "InvstDataEntry_Add _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            

        }
    }
    protected void grdInvAccMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdInvAccMaster.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtAC_TextChanged(object sender, EventArgs e)
    {
        try
        {
             resultint = IAM.GetAllData11(grdInvAccMaster, Session["EntryDate"].ToString(), Session["BRCD"].ToString(), txtBankNo.Text, txtAC.Text);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}