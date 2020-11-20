using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmInvCreateReceipt : System.Web.UI.Page
{
    ClsInvCreateReceipt ICR = new ClsInvCreateReceipt();
    ClsInsertTrans ITrans = new ClsInsertTrans();
    ClsBindDropdown BD = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataTable dt = new DataTable();
    int IntResult = 0;
    int resultint;
    string SetNo = "";
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

                BD.BindPayment(ddlPayType, "1");

                AutoGlName.ContextKey = Session["BRCD"].ToString();
                //IntAutoGlName.ContextKey = Session["BRCD"].ToString();
                //PrinAutoGlName.ContextKey = Session["BRCD"].ToString();

                ENDN(true);
                //BD.BindAccType(ddlAccType); //Dhanya Shetty //01/07/2017
                BD.BindIntrstPayout(ddlIntrestPay);
                txtDepDate.Text = Session["EntryDate"].ToString();

                Transfer.Visible = false;
                DivCheque.Visible = false;

                txtProdCode.Focus();
                BindGrid("ED");
            }
            if (Request.QueryString["ProdCode"].ToString() != null)
            {
                txtProdCode.Text = Request.QueryString["ProdCode"].ToString();
                txtProdName.Text = Request.QueryString["Name"].ToString();
                txtAccNo.Text = Request.QueryString["AccNo"].ToString();
                txtAccName.Text = Request.QueryString["AccNo"].ToString();
                BindDetails();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ENDN(bool TF)
    {
        txtDepDate.Enabled = TF;
        txtDueDate.Enabled = TF;
        ddlIntrestPay.Enabled = TF;
        txtDepAmt.Enabled = TF;
        ddlDuration.Enabled = TF;
        txtPeriod.Enabled = TF;

        //txtIntProdCode.Enabled = TF;
        //txtIntProdName.Enabled = TF;
        //txtIntAccNo.Enabled = TF;
        //txtIntAccName.Enabled = TF;

        //txtPrinProdCode.Enabled = TF;
        //txtPrinProdName.Enabled = TF;
        //txtPrinAccNo.Enabled = TF;
        //txtPrinAccName.Enabled = TF;
    }

    protected void txtProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;
            AC1 = ICR.Getaccno(txtProdCode.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                ViewState["GlCode"] = AC[0].ToString();
                txtProdName.Text = AC[1].ToString();

                //AutoAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtProdCode.Text + "_" + ViewState["GlCode"].ToString();

                ClearCustAllDetails();
                txtAccNo.Focus();
            }
            else
            {
                lblMessage.Text = "Enter valid Product code...!!";
                ModalPopup.Show(this.Page);
                ClearAll();
                txtProdCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtProdName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtProdName.Text = custnob[0].ToString();
                txtProdCode.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] AC = ICR.Getaccno(txtProdCode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["GlCode"] = AC[0].ToString();

               // AutoAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtProdCode.Text + "_" + ViewState["GlCode"].ToString();

                ClearCustAllDetails();
                txtAccNo.Focus();
            }
            else
            {
                ClearAll();
                txtProdCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ClearCustAllDetails()
    {
        try
        {
            txtAccNo.Text = "";
            txtAccName.Text = "";
            txtCustNo.Text = "";

           // ddlAccType.SelectedIndex = 0;
            txtDepDate.Text = "";
            txtDepAmt.Text = "";
            ddlIntrestPay.SelectedIndex = 0;
            txtIntRate.Text = "";
            txtIntRate.Text = "";
            ddlDuration.SelectedValue = "M";
            txtPeriod.Text = "";

            txtIntAmt.Text = "";
            txtMaturityAmt.Text = "";
            txtDueDate.Text = "";

            //txtIntProdCode.Text = "";
            //txtIntProdName.Text = "";
            //txtIntAccNo.Text = "";
            //txtIntAccName.Text = "";

            //txtPrinProdCode.Text = "";
            //txtPrinProdName.Text = "";
            //txtPrinAccNo.Text = "";
            //txtPrinAccName.Text = "";
            ddlPayType.SelectedIndex = 0;

            txtAccNo.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ClearAll()
    {
        try
        {
            txtProdCode.Text = "";
            txtProdName.Text = "";
            txtAccNo.Text = "";
            txtAccName.Text = "";
            txtCustNo.Text = "";

           // ddlAccType.SelectedIndex = 0;
            txtDepDate.Text = "";
            txtDepAmt.Text = "";
            ddlIntrestPay.SelectedIndex = 0;
            txtIntRate.Text = "";
            txtIntRate.Text = "";
            ddlDuration.SelectedValue = "M";
            txtPeriod.Text = "";

            txtIntAmt.Text = "";
            txtMaturityAmt.Text = "";
            txtDueDate.Text = "";

            //txtIntProdCode.Text = "";
            //txtIntProdName.Text = "";
            //txtIntAccNo.Text = "";
            //txtIntAccName.Text = "";

            //txtPrinProdCode.Text = "";
            //txtPrinProdName.Text = "";
            //txtPrinAccNo.Text = "";
            //txtPrinAccName.Text = "";
            ddlPayType.SelectedIndex = 0;

            txtProdCode.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindDetails()
    {
        DT = new DataTable();
        DT = ICR.GetCustDetails1(Session["BRCD"].ToString(), txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());

        if (DT.Rows.Count > 0)
        {
                hdnreceipt.Value = DT.Rows[0]["receiptno"].ToString();
                txtAccNo.Text = DT.Rows[0]["CustAccNo"].ToString();
                txtAccName.Text = DT.Rows[0]["BANKNAME"].ToString();
                txtCustNo.Text = DT.Rows[0]["CustAccNo"].ToString();
                txtDepDate.Text = DT.Rows[0]["OD"].ToString();
                //txtDepDate.Enabled = false;
                // ddlAccType.SelectedValue = DT.Rows[0]["ACC_TYPE"].ToString();

                ddlIntrestPay.Focus();
        }
        else
        {
            if (Session["BRCD"].ToString() != "1")
            {
                lblMessage.Text = "Not Allowed For Branches...!!";
                ModalPopup.Show(this.Page);
                ClearCustAllDetails();
                return;
            }
            else
            {

                lblMessage.Text = "Account Number not found...!!";
                ModalPopup.Show(this.Page);
                ClearCustAllDetails();
                return;
            }
        }
    }
    protected void txtAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //string AT = "";
            //AT = BD.Getstage1(txtAccNo.Text, Session["BRCD"].ToString(), txtProdCode.Text);
            //if (AT != null)
            //{
            //    if (AT != "1003")
            //    {
            //        lblMessage.Text = "Sorry Customer not Authorise...!!";
            //        ModalPopup.Show(this.Page);
            //        txtAccNo.Text = "";
            //        txtAccName.Text = "";
            //        txtAccNo.Focus();
            //        BindDetails();
            //    }
            //    else
            //    {
            //        int RC = ICR.CheckAccount(Session["BRCD"].ToString(), txtProdCode.Text, txtAccNo.Text);
            //        if (RC == 1)
            //        {
            //            ClearCustAllDetails();
            //            txtProdCode.Focus();
            //            lblMessage.Text = "Sorry Already have Deposit Receipt...!!";
            //            ModalPopup.Show(this.Page);
            //            return;
            //        }
                    BindDetails();//amruta 21/06/2017
                    //DT = new DataTable();
                    //DT = ICR.GetCustDetails(Session["BRCD"].ToString(), txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());

                    //if (DT.Rows.Count > 0)
                    //{
                    //    txtAccNo.Text = DT.Rows[0]["ACCNO"].ToString();
                    //    txtAccName.Text = DT.Rows[0]["CUSTNAME"].ToString();
                    //    txtCustNo.Text = DT.Rows[0]["CUSTNO"].ToString();
                    //    txtDepDate.Text = Session["EntryDate"].ToString();
                    //    txtDepDate.Enabled = false;
                    //    ddlAccType.SelectedValue = DT.Rows[0]["ACC_TYPE"].ToString();

                    //    ddlIntrestPay.Focus();
                    //}
                    //else
                    //{
                    //    lblMessage.Text = "Account Number not found...!!";
                    //    ModalPopup.Show(this.Page);
                    //    ClearCustAllDetails();
                    //    return;
                    //}
            //    }
            //}
            //else
            //{
            //    lblMessage.Text = "Enter valid account number...!!";
            //    ModalPopup.Show(this.Page);
            //    txtAccNo.Text = "";
            //    txtAccNo.Focus();
            //}
                    BindGrid("PA");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtAccName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                int RC = ICR.CheckAccount(Session["BRCD"].ToString(), txtProdCode.Text, (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString()));
                if (RC == 1)
                {
                    ClearCustAllDetails();
                    txtProdCode.Focus();
                    lblMessage.Text = "Sorry Already have Deposit Receipt...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }

                BindDetails();//Amruta 21/06/2017
                //DT = new DataTable();
                //DT = ICR.GetCustDetails(Session["BRCD"].ToString(), txtProdCode.Text.Trim().ToString(), (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString()));

                //if (DT.Rows.Count > 0)
                //{
                //    txtAccNo.Text = DT.Rows[0]["ACCNO"].ToString();
                //    txtAccName.Text = DT.Rows[0]["CUSTNAME"].ToString();
                //    txtCustNo.Text = DT.Rows[0]["CUSTNO"].ToString();
                //    txtDepDate.Text = Session["EntryDate"].ToString();
                //    txtDepDate.Enabled = false;
                //    ddlAccType.SelectedValue = DT.Rows[0]["ACC_TYPE"].ToString();

                //    ddlIntrestPay.Focus();
                //}
                //else
                //{
                //    lblMessage.Text = "Account Number not found...!!";
                //    ModalPopup.Show(this.Page);
                //    ClearCustAllDetails();
                //    return;
                //}
                BindGrid("PA");
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtDepAmt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtPeriod.Text = "";
            txtIntRate.Text = "";
            txtIntAmt.Text = "";
            txtMaturityAmt.Text = "";
            txtDueDate.Text = "";

            ddlDuration.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
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

            // Check Duration//Dhanya Shetty-03-07-2017
            //string IsPvalid = "0";
            //IsPvalid = ICR.CheckPeriod(Session["BRCD"].ToString(), txtProdCode.Text.ToString(), txtPeriod.Text.ToString(), ddlDuration.SelectedValue.ToString());//, ddlAccType.SelectedValue
            
            //if (Convert.ToInt32(IsPvalid) < 0)
            //{
            //    txtPeriod.Text = "";
            //    txtIntRate.Text = "";
            //    txtIntAmt.Text = "";
            //    txtMaturityAmt.Text = "";
            //    txtDueDate.Text = "";

            //    lblMessage.Text = "Invalid Period...!!";
            //    ModalPopup.Show(this.Page);

            //    txtPeriod.Focus();
            //    return;
            //}

            // Get rates for Product Code
            //string CustType = ddlAccType.SelectedValue;
          ////Dhanya Shetty////
            //float rate = ICR.GetIntRate(Session["BRCD"].ToString(), txtProdCode.Text.ToString(), txtPeriod.Text.ToString(), ddlDuration.SelectedValue.ToString(), false);//, CustType
            //if (rate == 0)
            //{
            //    lblMessage.Text = "Invalid Value...!!";
            //    ModalPopup.Show(this.Page);
                
            //    txtPeriod.Text = "";
            //    txtPeriod.Focus();
            //    return;
            //}
            //else
            //{
            //    txtIntRate.Text = rate.ToString();
            //}  ----////Dhanya Shetty////-----

             //Calculate Due date

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

            // Calculate Interest 
          

            //txtIntProdCode.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    //Interest Calculate
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

    //#region Interest transfer // Amruta 21/06/2017

    //protected void txtIntProdCode_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string AC1;
    //        AC1 = ICR.Getaccno(txtIntProdCode.Text, Session["BRCD"].ToString());

    //        if (AC1 != null)
    //        {
    //            string[] AC = AC1.Split('_'); ;
    //            ViewState["IntGlCode"] = AC[0].ToString();
    //            txtIntProdName.Text = AC[1].ToString();

    //            IntAutoAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtIntProdCode.Text + "_" + ViewState["IntGlCode"].ToString();

    //            txtIntAccNo.Text = "";
    //            txtIntAccName.Text = "";

    //            txtIntAccNo.Focus();
    //        }
    //        else
    //        {
    //            txtIntProdCode.Text = "";
    //            txtIntProdName.Text = "";
    //            txtIntAccNo.Text = "";
    //            txtIntAccName.Text = "";

    //            lblMessage.Text = "Enter valid Product code...!!";
    //            ModalPopup.Show(this.Page);

    //            txtIntProdCode.Focus();
    //            return;
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}

    //protected void txtIntProdName_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string CUNAME = txtIntProdName.Text;
    //        string[] custnob = CUNAME.Split('_');
    //        if (custnob.Length > 1)
    //        {
    //            txtIntProdName.Text = custnob[0].ToString();
    //            txtIntProdCode.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
    //            string[] AC = ICR.Getaccno(txtIntProdCode.Text, Session["BRCD"].ToString()).Split('_');
    //            ViewState["IntGlCode"] = AC[0].ToString();

    //            IntAutoAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtIntProdCode.Text + "_" + ViewState["IntGlCode"].ToString();

    //            txtIntAccNo.Text = "";
    //            txtIntAccName.Text = "";

    //            txtIntAccNo.Focus();
    //        }
    //        else
    //        {
    //            txtIntProdCode.Text = "";
    //            txtIntProdName.Text = "";
    //            txtIntAccNo.Text = "";
    //            txtIntAccName.Text = "";

    //            txtIntProdCode.Focus();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}

    //protected void txtIntAccNo_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string AT = "";
    //        AT = BD.Getstage1(txtIntAccNo.Text, Session["BRCD"].ToString(), txtIntProdCode.Text);
    //        if (AT != null)
    //        {
    //            if (AT != "1003")
    //            {
    //                lblMessage.Text = "Sorry Customer not Authorise...!!";
    //                ModalPopup.Show(this.Page);

    //                txtIntAccNo.Text = "";
    //                txtIntAccName.Text = "";

    //                txtIntAccNo.Focus();
    //                return;
    //            }
    //            else
    //            {
    //                DT = new DataTable();
    //                DT = ICR.GetCustName(txtIntProdCode.Text, txtIntAccNo.Text, Session["BRCD"].ToString());
    //                if (DT.Rows.Count > 0)
    //                {
    //                    string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
    //                    txtIntAccName.Text = CustName[0].ToString();
    //                }

    //                txtPrinProdCode.Focus();
    //            }
    //        }
    //        else
    //        {
    //            lblMessage.Text = "Enter valid account number...!!";
    //            ModalPopup.Show(this.Page);

    //            txtIntAccNo.Text = "";
    //            txtIntAccName.Text = "";

    //            txtIntAccNo.Focus();
    //            return;
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}

    //protected void txtIntAccName_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string CUNAME = txtIntAccName.Text;
    //        string[] custnob = CUNAME.Split('_');
    //        if (custnob.Length > 1)
    //        {
    //            txtIntAccName.Text = custnob[0].ToString();
    //            txtIntAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

    //            txtPrinProdCode.Focus();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}

    //#endregion

    //#region Principle Transfer

    //protected void txtPrinProdCode_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string AC1;
    //        AC1 = ICR.Getaccno(txtPrinProdCode.Text, Session["BRCD"].ToString());

    //        if (AC1 != null)
    //        {
    //            string[] AC = AC1.Split('_'); ;
    //            ViewState["PrinGlCode"] = AC[0].ToString();
    //            txtPrinProdName.Text = AC[1].ToString();

    //            PrinAutoAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtPrinProdCode.Text + "_" + ViewState["PrinGlCode"].ToString();

    //            txtPrinAccNo.Text = "";
    //            txtPrinAccName.Text = "";

    //            txtPrinAccNo.Focus();
    //        }
    //        else
    //        {
    //            txtPrinProdCode.Text = "";
    //            txtPrinProdName.Text = "";
    //            txtPrinAccNo.Text = "";
    //            txtPrinAccName.Text = "";

    //            lblMessage.Text = "Enter valid Product code...!!";
    //            ModalPopup.Show(this.Page);

    //            txtPrinProdCode.Focus();
    //            return;
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}

    //protected void txtPrinProdName_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string CUNAME = txtPrinProdName.Text;
    //        string[] custnob = CUNAME.Split('_');
    //        if (custnob.Length > 1)
    //        {
    //            txtPrinProdName.Text = custnob[0].ToString();
    //            txtPrinProdCode.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
    //            string[] AC = ICR.Getaccno(txtPrinProdCode.Text, Session["BRCD"].ToString()).Split('_');
    //            ViewState["PrinGlCode"] = AC[0].ToString();

    //            PrinAutoAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtPrinProdCode.Text + "_" + ViewState["PrinGlCode"].ToString();

    //            txtPrinAccNo.Text = "";
    //            txtPrinAccName.Text = "";

    //            txtPrinAccNo.Focus();
    //        }
    //        else
    //        {
    //            txtPrinProdCode.Text = "";
    //            txtPrinProdName.Text = "";
    //            txtPrinAccNo.Text = "";
    //            txtPrinAccName.Text = "";

    //            txtPrinProdCode.Focus();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}

    //protected void txtPrinAccNo_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string AT = "";
    //        AT = BD.Getstage1(txtPrinAccNo.Text, Session["BRCD"].ToString(), txtPrinProdCode.Text);
    //        if (AT != null)
    //        {
    //            if (AT != "1003")
    //            {
    //                lblMessage.Text = "Sorry Customer not Authorise...!!";
    //                ModalPopup.Show(this.Page);

    //                txtPrinAccNo.Text = "";
    //                txtIntAccName.Text = "";

    //                txtPrinAccNo.Focus();
    //                return;
    //            }
    //            else
    //            {
    //                DT = new DataTable();
    //                DT = ICR.GetCustName(txtPrinProdCode.Text, txtPrinAccNo.Text, Session["BRCD"].ToString());
    //                if (DT.Rows.Count > 0)
    //                {
    //                    string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
    //                    txtPrinAccName.Text = CustName[0].ToString();
    //                }

    //                ddlPayType.Focus();
    //            }
    //        }
    //        else
    //        {
    //            lblMessage.Text = "Enter valid account number...!!";
    //            ModalPopup.Show(this.Page);

    //            txtPrinAccNo.Text = "";
    //            txtPrinAccName.Text = "";

    //            txtPrinAccNo.Focus();
    //            return;
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}

    //protected void txtPrinAccName_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string CUNAME = txtPrinAccName.Text;
    //        string[] custnob = CUNAME.Split('_');
    //        if (custnob.Length > 1)
    //        {
    //            txtPrinAccName.Text = custnob[0].ToString();
    //            txtPrinAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

    //            ddlPayType.Focus();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}

    //#endregion

    #region Payment Mode

    protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPayType.SelectedValue.ToString() == "0")
            {
                Transfer.Visible = false;
                DivCheque.Visible = false;
                DivAmount.Visible = false;
            }
            else if (ddlPayType.SelectedValue.ToString() == "1")
            {
                ViewState["PAYTYPE"] = "CASH";
                Transfer.Visible = false;
                DivCheque.Visible = false;
                DivAmount.Visible = true;

                txtNarration.Text = "By Cash";
                txtAmount.Text = Convert.ToDouble(txtDepAmt.Text.Trim().ToString() == "" ? "0" : txtDepAmt.Text.Trim().ToString()).ToString();

                Clear();
                btnSubmit.Focus();
            }
            else if (ddlPayType.SelectedValue.ToString() == "2")
            {
                ViewState["PAYTYPE"] = "TRANSFER";
                Transfer.Visible = true;
                DivCheque.Visible = false;
                DivAmount.Visible = true;

                txtNarration.Text = "By TRF";
                txtAmount.Text = Convert.ToDouble(txtDepAmt.Text.Trim().ToString() == "" ? "0" : txtDepAmt.Text.Trim().ToString()).ToString();

                Clear();
                TrfAutoGlName.ContextKey = Session["BRCD"].ToString();
                txtTrfProdCode.Focus();
            }
            else if (ddlPayType.SelectedValue.ToString() == "4")
            {
                ViewState["PAYTYPE"] = "CHEQUE";
                Transfer.Visible = true;
                DivCheque.Visible = true;
                DivAmount.Visible = true;

                txtNarration.Text = "TRANSFER";
                txtAmount.Text = Convert.ToDouble(txtDepAmt.Text.Trim().ToString() == "" ? "0" : txtDepAmt.Text.Trim().ToString()).ToString();

                Clear();
                TrfAutoGlName.ContextKey = Session["BRCD"].ToString();
                txtTrfProdCode.Focus();
            }
            else
            {
                Clear();
                Transfer.Visible = false;
                DivCheque.Visible = false;
                DivAmount.Visible = false;
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
            txtTrfProdCode.Text = "";
            txtTrfProdName.Text = "";
            txtTrfAccNo.Text = "";
            txtTrfAccName.Text = "";
            txtBalance.Text = "";
            txtChequeNo.Text = "";
            txtChequeDate.Text = "";
            txtChequeDate.Text = Session["EntryDate"].ToString();

            txtTrfProdCode.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        finally
        {
            txtTrfProdCode.Focus();
        }

    }

    protected void txtProdType1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;
            AC1 = ICR.Getaccno(txtTrfProdCode.Text, Session["BRCD"].ToString());

            if (AC1 != null)
            {
                string[] AC = AC1.Split('_'); ;
                ViewState["TrfGlCode"] = AC[0].ToString();
                txtTrfProdName.Text = AC[1].ToString();
                TrfAutoAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtTrfProdCode.Text + "_" + ViewState["TrfGlCode"].ToString();

                if (Convert.ToInt32(ViewState["TrfGlCode"].ToString() == "" ? "0" : ViewState["TrfGlCode"].ToString()) >= 100)
                {
                    txtTrfAccNo.Text = "";
                    txtTrfAccName.Text = "";

                    txtTrfAccNo.Text = txtTrfProdCode.Text.ToString();
                    txtTrfAccName.Text = txtTrfProdName.Text.ToString();

                    txtBalance.Text = ICR.GetOpenClose(Session["BRCD"].ToString(), txtTrfProdCode.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();

                    txtChequeNo .Focus();
                }
                else
                {
                    txtTrfAccNo.Text = "";
                    txtTrfAccName.Text = "";
                    txtBalance.Text = "";

                    txtTrfAccNo.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                txtTrfProdCode.Text = "";
                txtTrfProdName.Text = "";
                txtTrfAccNo.Text = "";
                txtTrfAccName.Text = "";
                txtBalance.Text = "";
                txtTrfProdCode.Focus();
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
            string CUNAME = txtTrfProdName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtTrfProdName.Text = custnob[0].ToString();
                txtTrfProdCode.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                string[] AC = ICR.Getaccno(txtTrfProdCode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["TrfGlCode"] = AC[0].ToString();
                TrfAutoAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtTrfProdCode.Text;

                if (Convert.ToInt32(ViewState["TrfGlCode"].ToString() == "" ? "0" : ViewState["TrfGlCode"].ToString()) >= 100)
                {
                    txtTrfAccNo.Text = "";
                    txtTrfAccName.Text = "";

                    txtTrfAccNo.Text = txtTrfProdCode.Text.ToString();
                    txtTrfAccName.Text = txtTrfProdName.Text.ToString();

                    txtBalance.Text = ICR.GetOpenClose(Session["BRCD"].ToString(), txtTrfProdCode.Text.Trim().ToString(), "0", Session["EntryDate"].ToString(), "ClBal").ToString();

                    txtChequeNo.Focus();
                }
                else
                {
                    txtTrfAccNo.Text = "";
                    txtTrfAccName.Text = "";
                    txtBalance.Text = "";

                    txtTrfAccNo.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtAccNo1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";
            AT = BD.Getstage1(txtTrfAccNo.Text, Session["BRCD"].ToString(), txtTrfProdCode.Text);
            if (AT != null)
            {
                if (AT != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise.........!!";
                    ModalPopup.Show(this.Page);
                    txtTrfAccNo.Text = "";
                    txtTrfAccName.Text = "";
                    txtTrfAccNo.Focus();
                }
                else
                {
                    DataTable DT = new DataTable();
                    DT = ICR.GetCustName(txtTrfProdCode.Text, txtTrfAccNo.Text, Session["BRCD"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                        txtTrfAccName.Text = CustName[0].ToString();

                        txtBalance.Text = ICR.GetOpenClose(Session["BRCD"].ToString(), txtTrfProdCode.Text.Trim().ToString(), txtTrfAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();

                        txtChequeNo.Focus();
                    }
                }
            }
            else
            {
                lblMessage.Text = "Enter valid account number...!!";
                ModalPopup.Show(this.Page);
                txtTrfAccNo.Text = "";
                txtTrfAccNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtAccName1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtTrfAccName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtTrfAccName.Text = custnob[0].ToString();
                txtTrfAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                txtBalance.Text = ICR.GetOpenClose(Session["BRCD"].ToString(), txtTrfProdCode.Text.Trim().ToString(), txtTrfAccNo.Text.Trim().ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();

                txtChequeNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Button Click Event

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDouble(txtDepAmt.Text.Trim().ToString() == "" ? "0" : txtDepAmt.Text.Trim().ToString()) > 0)
            {
                SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();

               //IntResult = ICR.CreateDeposite(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), txtProdCode.Text.Trim().ToString(), txtPeriod.Text.Trim().ToString(), Convert.ToDouble(txtIntRate.Text.Trim().ToString()), Convert.ToDouble(txtDepAmt.Text.Trim().ToString()), Convert.ToDouble(txtIntAmt.Text.Trim().ToString()), Convert.ToDouble(txtMaturityAmt.Text.Trim().ToString()), txtIntProdCode.Text.Trim().ToString(), txtIntAccNo.Text.Trim().ToString(), txtPrinProdCode.Text.Trim().ToString(), txtPrinAccNo.Text.Trim().ToString(), ddlIntrestPay.SelectedValue.ToString(), ddlDuration.SelectedValue.ToString(), txtDepDate.Text.Trim().ToString(), txtDueDate.Text.Trim().ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString()); //Amruta 21/06/2017
                IntResult = ICR.CreateDeposite1(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), txtProdCode.Text.Trim().ToString(), txtPeriod.Text.Trim().ToString(), Convert.ToDouble(txtIntRate.Text.Trim().ToString()), Convert.ToDouble(txtDepAmt.Text.Trim().ToString()), Convert.ToDouble(txtIntAmt.Text.Trim().ToString()), Convert.ToDouble(txtMaturityAmt.Text.Trim().ToString()), ddlIntrestPay.SelectedValue.ToString(), ddlDuration.SelectedValue.ToString(), txtDepDate.Text.Trim().ToString(), txtDueDate.Text.Trim().ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString(), hdnreceipt.Value);//Amruta 21/06/2017

                if (IntResult > 0)
                {
                    string cgl = "";
                    if (ddlPayType.SelectedValue.ToString() == "1")
                    {
                         cgl = BD.GetCashGl("99", Session["BRCD"].ToString());

                        IntResult = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", cgl,
                                    "0", txtNarration.Text, "", Convert.ToDouble(txtAmount.Text.Trim().ToString()), "1", "3", "CR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "INVCASH", "0", "By Cash", "1");

                        if (IntResult > 0)
                        {
                            cgl = BD.GetCashGl(txtProdCode.Text, Session["BRCD"].ToString());
                            IntResult = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), cgl, txtProdCode.Text.ToString(),
                                        txtAccNo.Text.ToString(), txtNarration.Text, "", Convert.ToDouble(txtAmount.Text.Trim().ToString()), "2", "3", "CR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                        "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "INVCASH", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), "0");

                            if (IntResult > 0)
                            {
                                lblMessage.Text = "Deposite Created Successfully...!! Set No=" + SetNo;
                                ModalPopup.Show(this.Page);
                                BindGrid("PA");
                                FL = "Insert";//Dhanya Shetty
                                string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_Receipt _" + txtProdCode.Text + "_" + txtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                ClearAlldetails();
                               
                                return;
                            }
                        }
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "2")
                    {
                        IntResult = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["TrfGlCode"].ToString(), txtTrfProdCode.Text.ToString(),
                                    txtTrfAccNo.Text.ToString(), txtNarration.Text, "", Convert.ToDouble(txtAmount.Text.Trim().ToString()), "1", "7", "TRF", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "INVTR", "0", txtTrfAccName.Text.ToString(), "1");

                        if (IntResult > 0)
                        {
                            cgl = BD.GetCashGl(txtProdCode.Text, Session["BRCD"].ToString());
                            IntResult = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), cgl, txtProdCode.Text.ToString(),
                                        txtAccNo.Text.ToString(), txtNarration.Text, "", Convert.ToDouble(txtAmount.Text.Trim().ToString()), "2", "7", "TRF", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                        "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "INVTR", "0", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), "0");

                            if (IntResult > 0)
                            {
                                lblMessage.Text = "Deposite Created Successfully...!! Set No=" + SetNo;
                               
                                ModalPopup.Show(this.Page);
                                BindGrid("PA");
                                FL = "Insert";//Dhanya Shetty
                                string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_Receipt _" + txtProdCode.Text + "_" + txtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                ClearAlldetails();
                                return;
                            }
                        }
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "4")
                    {
                        IntResult = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["TrfGlCode"].ToString(), txtTrfProdCode.Text.ToString(),
                                    txtTrfAccNo.Text.ToString(), txtNarration.Text, "", Convert.ToDouble(txtAmount.Text.Trim().ToString()), "1", "6", "TRF", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                    "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "INVCHQ", "0", txtTrfAccName.Text.ToString(), "1");

                        if (IntResult > 0)
                        {
                            cgl = BD.GetCashGl(txtProdCode.Text, Session["BRCD"].ToString());
                            IntResult = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), cgl, txtProdCode.Text.ToString(),
                                        txtAccNo.Text.ToString(), txtNarration.Text, "", Convert.ToDouble(txtAmount.Text.Trim().ToString()), "2", "5", "TRF", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                        "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "INVCHQ", "0", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), "0");

                            if (IntResult > 0)
                            {
                                lblMessage.Text = "Deposite Created Successfully...!! Set No=" + SetNo;
                                
                                ModalPopup.Show(this.Page);
                                BindGrid("PA");
                                FL = "Insert";//Dhanya Shetty
                                string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_Receipt _" + txtProdCode.Text + "_" + txtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                ClearAlldetails();
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                lblMessage.Text = "Enter Deposite Amount First...!!";
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
            ClearAlldetails();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmInvCreateReceipt.aspx", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindGrid(string Flag)
    {
        try
        {
            if (Flag == "ED")
            {
                resultint = ICR.GetAllData(grdInvRectrans, Session["EntryDate"].ToString(), Session["BRCD"].ToString(), "", "");
            }
            else
            {
                resultint = ICR.GetAllData(grdInvRectrans, Session["EntryDate"].ToString(), Session["BRCD"].ToString(), txtProdCode.Text, txtAccNo.Text);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void ClearAlldetails()
    {
        txtAccName.Text = "";
        txtAccNo.Text = "";
        txtAmount.Text = "";
        txtBalance.Text = "";
        txtChequeDate.Text = "";
        txtChequeNo.Text = "";
        txtCustNo.Text = "";
        txtDepAmt.Text = "";
        txtDepDate.Text = "";
        txtDueDate.Text = "";
        txtIntAmt.Text = "";
        txtIntRate.Text = "";
        txtMaturityAmt.Text = "";
        txtNarration.Text = "";
        txtPeriod.Text = "";
        txtProdCode.Text = "";
        txtProdName.Text = "";
        txtTrfAccName.Text = "";
        txtTrfAccNo.Text = "";
        txtTrfProdCode.Text = "";
        txtTrfProdName.Text = "";
    }
    #endregion
    protected void txtIntRate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtIntRate.Text != "")
            {
                float amt = (float)Convert.ToDouble(txtDepAmt.Text);
                float intrate = (float)Convert.ToDouble(txtIntRate.Text);
                CalDepositInterest(amt, txtProdCode.Text.ToString(), intrate, Convert.ToInt32(txtPeriod.Text), ddlIntrestPay.SelectedItem.Text.ToString(), ddlDuration.SelectedItem.Text.ToString());
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
            ViewState["Id"] = ARR[0].ToString();
            ViewState["SubGLCOde"] = ARR[1].ToString();
            ViewState["CustACCNO"] = ARR[2].ToString();
            btmModify.Visible = true;
            btnSubmit.Visible = false;
            CallEdit();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btmModify_Click(object sender, EventArgs e)
    {
        try
        {
            IntResult = ICR.Modify(Session["BRCD"].ToString(), txtAccNo.Text.Trim().ToString(), txtProdCode.Text.Trim().ToString(),
                txtPeriod.Text.Trim().ToString(), Convert.ToDouble(txtIntRate.Text.Trim().ToString()), Convert.ToDouble(txtDepAmt.Text.Trim().ToString()), Convert.ToDouble(txtIntAmt.Text.Trim().ToString()),
                Convert.ToDouble(txtMaturityAmt.Text.Trim().ToString()), ddlIntrestPay.SelectedValue.ToString(), ddlDuration.SelectedValue.ToString(), txtDepDate.Text.Trim().ToString(),
                txtDueDate.Text.Trim().ToString(), ViewState["Id"].ToString());
            if (IntResult > 0)
            {
                WebMsgBox.Show("Data Modified Successfully", this.Page);
                BindGrid("PA");
                string FL1 = "Insert";//Dhanya Shetty
                string Result1 = CLM.LOGDETAILS(FL1, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_Receipt _Mod_" + txtProdCode.Text + "_" + txtAccNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearAlldetails();

            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void CallEdit()
    {
        try
        {

            DataTable DT = new DataTable();
            DT = ICR.GetInfo(Session["BRCD"].ToString(), ViewState["Id"].ToString(), ViewState["SubGLCOde"].ToString(), ViewState["CustACCNO"].ToString());
            if (DT.Rows.Count > 0)
            {
                txtProdCode.Text = DT.Rows[0]["SubGlCode"].ToString();
                string AC1;
                AC1 = ICR.Getaccno(txtProdCode.Text, Session["BRCD"].ToString());

                if (AC1 != null)
                {
                    string[] AC = AC1.Split('_'); ;
                    txtProdName.Text = AC[1].ToString();
                }
                txtAccNo.Text = DT.Rows[0]["CustAccNo"].ToString();
                dt = ICR.GetCustDetails1(Session["BRCD"].ToString(), txtProdCode.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString());
                if (dt.Rows.Count > 0)
                {
                    txtAccName.Text = dt.Rows[0]["BANKNAME"].ToString();
                }
                txtCustNo.Text = DT.Rows[0]["CustNo"].ToString();
                txtPeriod.Text = DT.Rows[0]["Period"].ToString();
                txtIntRate.Text = DT.Rows[0]["RateOfInt"].ToString();
                txtDepAmt.Text = DT.Rows[0]["PrincipleAmt"].ToString();
                txtIntAmt.Text = DT.Rows[0]["InterestAmt"].ToString();
                txtMaturityAmt.Text = DT.Rows[0]["MaturityAmt"].ToString();
                txtDueDate.Text = DT.Rows[0]["DueDate"].ToString();
                txtDepDate.Text = DT.Rows[0]["OpeningDate"].ToString();
                ddlDuration.SelectedValue = string.IsNullOrEmpty(DT.Rows[0]["PeriodType"].ToString()) ? "0" : DT.Rows[0]["PeriodType"].ToString();
               string  payout =  DT.Rows[0]["IntPayOut"].ToString();
               if (payout == "-1" || payout == "0")
               {
                   ddlIntrestPay.SelectedValue = "0";
               }
               else
               {
                   ddlIntrestPay.SelectedValue = string.IsNullOrEmpty(DT.Rows[0]["IntPayOut"].ToString()) ? "0" : DT.Rows[0]["IntPayOut"].ToString();
               }
                

            }
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}