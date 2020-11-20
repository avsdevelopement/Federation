using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmCalculator : System.Web.UI.Page
{
    string sql = "";
    ClsBindDropdown ddlbind = new ClsBindDropdown();
    ClsCalculator CurrentCls = new ClsCalculator();
    DbConnection conn = new DbConnection();
    string glcode;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            BindDropDown();
        }
    }

    public void BindDropDown()
    {
        dtDeposDate.Text = Session["EntryDate"].ToString();
        ddlbind.BindProd(ddlProdCode);
        ddlbind.BindAccType(ddlAccType);
        ddlbind.BindIntrstPayout(ddlIntrestPay);
    }

    protected void TxtDepoAmt_TextChanged(object sender, EventArgs e)
    {
        TxtPeriod.Text = "";
        TxtRate.Text = "";
        TxtIntrest.Text = "";
        TxtMaturity.Text = "";
        DtDueDate.Text = "";
        ddlduration.Focus();
    }

    protected void TxtRate_TextChanged(object sender, EventArgs e)
    {
        // Get rates wise
        if (ddlduration.SelectedValue == "M")
        {
            DtDueDate.Text = conn.AddMonthDay(dtDeposDate.Text, TxtPeriod.Text, "M").Replace("12:00:00", "");
            DtDueDate.Text = conn.AddMonthDay(dtDeposDate.Text, TxtPeriod.Text, "M").Replace("12:00:00", "");
        }
        else if (ddlduration.SelectedValue == "D")
        {
            DtDueDate.Text = conn.AddMonthDay(dtDeposDate.Text, TxtPeriod.Text, "D").Replace("12:00:00", "");
            DtDueDate.Text = conn.AddMonthDay(dtDeposDate.Text, TxtPeriod.Text, "D").Replace("12:00:00", "");
        }
        // Calculate Interest 
        float amt = (float)Convert.ToDouble(TxtDepoAmt.Text);
        float intrate = (float)Convert.ToDouble(TxtRate.Text);
        CalculatedepositINT(amt, ddlProdCode.SelectedValue.ToString(), intrate, Convert.ToInt32(TxtPeriod.Text), ddlIntrestPay.SelectedItem.Text.ToString(), ddlduration.SelectedItem.Text.ToString());
    }

    protected void ddlProdCode_TextChanged(object sender, EventArgs e)
    {
        glcode = ddlProdCode.SelectedValue.ToString();
        ClearAll();
    }

    public void ClearAll()
    {
        ddlAccType.SelectedIndex = 0;
        dtDeposDate.Text = Session["EntryDate"].ToString();
        ddlIntrestPay.SelectedIndex = 0;
        TxtDepoAmt.Text = "";
        ddlduration.SelectedIndex = 0;
        TxtPeriod.Text = "";
        TxtRate.Text = "";
        TxtIntrest.Text = "";
        TxtMaturity.Text = "";
        DtDueDate.Text="";
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
            // Check Duration
            string IsPvalid = "0";
            IsPvalid = CurrentCls.CheckPeriod(ddlAccType.SelectedValue.ToString(),ddlProdCode.SelectedValue.ToString(), TxtPeriod.Text.ToString(), ddlduration.SelectedValue.ToString(), Session["BRCD"].ToString());
            if (Convert.ToInt32(IsPvalid) > 0)
            {
            }
            else
            {
                WebMsgBox.Show("Invalid Period...", this.Page);
                TxtPeriod.Text = "";
                TxtRate.Text = "";
                TxtIntrest.Text = "";
                TxtMaturity.Text = "";
                DtDueDate.Text = "";
                TxtPeriod.Focus();
                return;
            }
            // Get rates for Product Code
            float rate = CurrentCls.GetIntrestRate(ddlProdCode.SelectedValue.ToString(), TxtPeriod.Text.ToString(), Session["BRCD"].ToString(), ddlduration.SelectedValue.ToString(), false);
            if (rate == 0)
            {
                WebMsgBox.Show(" Invalid Value... ", this.Page);
                TxtPeriod.Text = "";
                TxtPeriod.Focus();
                return;
            }
            else
            {
                TxtRate.Text = rate.ToString();
            }
            if (ddlduration.SelectedValue == "M")
            {
                DtDueDate.Text = conn.AddMonthDay(dtDeposDate.Text, TxtPeriod.Text, "M").Replace("12:00:00", "");
                DtDueDate.Text = conn.AddMonthDay(dtDeposDate.Text, TxtPeriod.Text, "M").Replace("12:00:00", "");
            }
            else if (ddlduration.SelectedValue == "D")
            {
                DtDueDate.Text = conn.AddMonthDay(dtDeposDate.Text, TxtPeriod.Text, "D").Replace("12:00:00", "");
                DtDueDate.Text = conn.AddMonthDay(dtDeposDate.Text, TxtPeriod.Text, "D").Replace("12:00:00", "");
            }
            // Calculate Interest 
            float amt = (float)Convert.ToDouble(TxtDepoAmt.Text);
            float intrate = (float)Convert.ToDouble(TxtRate.Text);
            CalculatedepositINT(amt, ddlProdCode.SelectedValue.ToString(), intrate, Convert.ToInt32(TxtPeriod.Text), ddlIntrestPay.SelectedItem.Text.ToString(), ddlduration.SelectedItem.Text.ToString());
        }
        catch (Exception Ex)
        {
            WebMsgBox.Show(Ex.Message, this.Page);
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
                    interest = Convert.ToInt32(amt * intrate / 1209.75);
                    maturityamt = interest + amt;
                    TxtIntrest.Text = interest.ToString("N");
                    TxtMaturity.Text = maturityamt.ToString("N");
                    break;

                case "CUM":
                    QUATERS = (Period / 3);
                    maturityamt = Convert.ToInt32((Math.Pow(((intrate / 400) + 1), (QUATERS))) * amt);
                    interest = maturityamt - amt;
                    TxtIntrest.Text = interest.ToString("N");
                    TxtMaturity.Text = maturityamt.ToString("N");
                    break;

                case "FDS":
                    if (intpay == "On Maturity")
                    {
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
                    }
                    else if (intpay == "Quaterly")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (3));
                        maturityamt = amt; //Val(interest) + Val(amt);                    
                        TxtIntrest.Text = interest.ToString("N");
                        TxtMaturity.Text = maturityamt.ToString("N");
                    }
                    else if (intpay == "Monthly" || PTYPE == "maihano")
                    {
                        interest = Convert.ToInt32((amt) * (intrate) / 1209.75);
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
}