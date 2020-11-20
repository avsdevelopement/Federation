using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmNewTDA : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    ClsCommon CMN = new ClsCommon();
    scustom customcs = new scustom();
    ClsNewTDA CurrentCls = new ClsNewTDA();
    ClsBindDropdown ddlbind = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    
    // Array for Interest calculator
    float[] RtnArray = new float[2];

    protected void Page_Load(object sender, EventArgs e)
    {
        string op = "";
        if (! IsPostBack)
        {
            // Get operations from query (Insert / Modify / Authorize) 
            op=Request.QueryString["op"].ToString();
            ViewState["op"] = op;

            if (op == "AD")
            {
                Literal1.Text ="Deposit Entry";
            }
            else if (op == "MD")
            {
                Literal1.Text = "Deposit Modify";
            }
            else if (op == "VW")
            {
                Literal1.Text = "Deposit View";
            }
            else if (op == "AT")
            {
                Literal1.Text = "Deposit Authorize";
            }
            else if (op == "DL")
            {
                Literal1.Text = "Deposit Delete";
            }            

            ddlbind.BindOperation(ddlOpType);
            ddlbind.BindAccType(ddlAccType);
            ddlbind.BindIntrstPayout(ddlIntrestPay);
            dtDeposDate.Text = Session["EntryDate"].ToString();
            TxtProcode.Focus();
        }        
    }

    // Customer name
    protected void Txtcustno_TextChanged(object sender, EventArgs e)
    {
        TxtcustName.Text=CMN.GetCustName(Txtcustno.Text.ToString(), Session["BRCD"].ToString());
        if (TxtcustName.Text == "" && Txtcustno.Text !="")
        {
            WebMsgBox.Show("Invalid Customer Number", this.Page);
            Txtcustno.Focus();
        }        
    }   

    // Product name
    protected void TxtProcode_TextChanged(object sender, EventArgs e)
    {
        string nm = CMN.GetProductName(Convert.ToInt32(TxtProcode.Text), Session["BRCD"].ToString());
        TxtProName.Text = nm;
        if (TxtProName.Text == "")
        {
            WebMsgBox.Show("Invalid product code",this.Page);
            clear();
            TxtProcode.Focus();
            return;
        }        
        txtAccNo.Focus();
    }    

    // Get Customer name from Account no and product code
    protected void txtAccNo_TextChanged(object sender, EventArgs e)
    {
        dt=CurrentCls.GetCustNoName(TxtProcode.Text.ToString(),txtAccNo.Text.ToString(),Session["BRCD"].ToString());

        if (dt.Rows.Count > 0)
        {
            Txtcustno.Text = dt.Rows[0][0].ToString();
            ddlAccType.SelectedValue = dt.Rows[0][1].ToString();
            ddlOpType.SelectedValue = dt.Rows[0][2].ToString();
            TxtcustName.Text = dt.Rows[0][3].ToString();
            ddlIntrestPay.Focus();
        }
        else
        {
            WebMsgBox.Show("Account Number not found",this.Page);
            txtAccNo.Text = "";
            Txtcustno.Text = "";
            TxtcustName.Text = "";
            ddlAccType.SelectedIndex = 0;
            ddlOpType.SelectedIndex = 0;
            txtAccNo.Focus();
        }
        
    }

    // Product name for transfer
    protected void TxtProcode1_TextChanged(object sender, EventArgs e)
    {

        TxtProName1.Text = customcs.GetProductName(TxtProcode.Text, Session["BRCD"].ToString());
        TxtAccNo1.Focus();
    }

    // Get Customer name from Account no and product code for transfer
    protected void txtAccNo1_TextChanged(object sender, EventArgs e)
    {
        string custname=CMN.GetCustNameAc(TxtAccNo1.Text.ToString(), Session["BRCD"].ToString(), TxtProcode1.Text.ToString());
        TxtcustName1.Text=custname;              
    }
   
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string AccNoTransf = "0";
        string ProCodeTransf = "0";
        string PCMAC = "";///System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.ToString();
        if (TxtAccNo1.Text == "")
        {
            AccNoTransf = "0";
        }
        else
        {
            AccNoTransf = TxtAccNo1.Text;
        }
        if (TxtProcode1.Text == "")
        {
            ProCodeTransf = "0";
        }
        else
        {
            ProCodeTransf = TxtProcode1.Text;
        }

        int result = CurrentCls.EntryDeposite(Txtcustno.Text.ToString(), txtAccNo.Text.ToString(), TxtProcode.Text.ToString(), TxtDepoAmt.Text.ToString(), TxtRate.Text.ToString(), dtDeposDate.Text.ToString(), DtDueDate.Text.ToString(), TxtPeriod.Text.ToString(), TxtIntrest.Text.ToString(), TxtMaturity.Text.ToString(), "1001", Session["BRCD"].ToString(), Session["MID"].ToString(), PCMAC, ddlIntrestPay.SelectedValue.ToString(), ddlduration.SelectedValue, "1", TxtProcode1.Text, TxtAccNo1.Text, "", Session["EntryDate"].ToString());
        if (result > 0)
        {
            WebMsgBox.Show("Record Saved...", this.Page);
            clear();
            TxtProcode.Focus();
        }
        else
        {
            WebMsgBox.Show("Could not be saved...", this.Page);
        }
        //CurrentCls.DepositeEntry(CUSTNO, CUSTACCNO,DEPOSITGLCODE, PRNAMT, RATEOFINT, OPENINGDATE, DUEDATE,                                                                               PERIOD, INTAMT, MATURITYAMT, STAGE, BRCD, MID, CID, VID, PCMAC, INTPAYOUT, PRDTYPE);
    }

    protected void TxtPeriod_TextChanged(object sender, EventArgs e)
    {
        if (TxtPeriod.Text == "")
        {
            return;
        }

        if (TxtDepoAmt.Text=="")
        {
            WebMsgBox.Show("Please enter amount", this.Page);
            TxtDepoAmt.Focus();
            return;
        }
        // Check Duration
        string IsPvalid = "0";
        IsPvalid=CurrentCls.CheckPeriod(TxtProcode.Text.ToString(), TxtPeriod.Text.ToString(), ddlduration.SelectedValue.ToString(), Session["BRCD"].ToString(),ddlAccType.SelectedValue);
        if (Convert.ToInt32(IsPvalid) > 0) {}
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
        float rate = CurrentCls.GetIntrestRate(TxtProcode.Text.ToString(), TxtPeriod.Text.ToString(), Session["BRCD"].ToString(), ddlduration.SelectedValue.ToString(),false);
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
        // Calculate Due date
        CalDueDate( Convert.ToDateTime(dtDeposDate.Text.ToString()), ddlduration.SelectedItem.Text.ToString(), Convert.ToInt32(TxtPeriod.Text));

        // Calculate Interest 
        float amt=(float)Convert.ToDouble(TxtDepoAmt.Text);
        float intrate = (float)Convert.ToDouble(TxtRate.Text);
        CalculatedepositINT(amt, TxtProcode.Text.ToString(), intrate, Convert.ToInt32(TxtPeriod.Text), ddlIntrestPay.SelectedItem.Text.ToString(), ddlduration.SelectedItem.Text.ToString());
        TxtProcode1.Focus();
    }

    // Calculate Due date
    protected void CalDueDate(DateTime DeposDate, string durationtype, int duration)
    {
        DateTime today = DateTime.Today;
        DateTime duedate = new DateTime();
        switch (durationtype)
        {
            case "Days":
                duedate = DeposDate.AddDays(duration);
                DtDueDate.Text = duedate.ToShortDateString();
               break;

            case "Months":
               duedate = DeposDate.AddMonths(duration);
               duedate = DeposDate.AddDays(-1);
               DtDueDate.Text = duedate.ToShortDateString();
               break;

            case "Years":
               duedate = DeposDate.AddYears(duration);
               DtDueDate.Text = duedate.ToShortDateString();
               break;
        }
    }

     //INREREST CALCULATOR
    protected void CalculatedepositINT(float amt, string subgl, float intrate, int Period, string intpay, string PTYPE)
    {        
        float interest = 0;
        float maturityamt = 0;
        float QUATERS = 0;
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
                TxtIntrest.Text = interest.ToString();
                TxtMaturity.Text = maturityamt.ToString();
                break;

            case "MIS":
                interest = Convert.ToInt32(amt * intrate / 1209.75);
                maturityamt = interest + amt;
                TxtIntrest.Text = interest.ToString();
                TxtMaturity.Text = maturityamt.ToString();
                break;

            case "CUM":
                QUATERS = (Period / 3);
                maturityamt = Convert.ToInt32((Math.Pow( ((intrate / 400) + 1) , (QUATERS))) * amt);
                interest = maturityamt - amt;
                TxtIntrest.Text = interest.ToString();
                TxtMaturity.Text = maturityamt.ToString();
                break;
            
            case "FDS":
                if (intpay == "On Maturity")
                {
                    if (PTYPE == "Days" || PTYPE == "idvasa")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 365) * (Period));
                        maturityamt = (interest) + (amt);
                        TxtIntrest.Text = interest.ToString();
                        TxtMaturity.Text = maturityamt.ToString();
                    }
                    else if (PTYPE == "Months" || PTYPE == "maihnao")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (Period));
                        maturityamt = (interest) + (amt);
                        TxtIntrest.Text = interest.ToString();
                        TxtMaturity.Text = maturityamt.ToString();
                    }
                }
                else if (intpay == "Quaterly")
                {
                    interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (3));
                    maturityamt = amt; //Val(interest) + Val(amt);                    
                    TxtIntrest.Text = interest.ToString();
                    TxtMaturity.Text = maturityamt.ToString();
                }
                else if (intpay == "Monthly" || PTYPE == "maihano")
                {
                    interest = Convert.ToInt32((amt) * (intrate) / 1209.75);
                    maturityamt = amt; //Val(interest) + Val(amt);
                    //maturityamt = interest + amt;
                    TxtIntrest.Text = interest.ToString();
                    TxtMaturity.Text = maturityamt.ToString();
                }
                break;

            case "RD":
                float deamt = amt;
                float tempamt = amt;
                Period = Period;
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
                        int interest1 = conn.sExecuteQuery(SQL);
                        maturityamt = (deamt * Period) + interest;
                        TxtIntrest.Text = interest.ToString();
                        TxtMaturity.Text = maturityamt.ToString();
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
                        TxtIntrest.Text = interest.ToString();
                        TxtMaturity.Text = maturityamt.ToString();
                    }
                }
                break;

            default:
                WebMsgBox.Show("Record not found",this.Page);
                break;
        }
    }

    public void clear()
    {
        TxtProcode.Text = "";
        TxtProName.Text = "";
        txtAccNo.Text = "";
        Txtcustno.Text = "";
        TxtcustName.Text = "";
        ddlAccType.SelectedIndex = 0;
        ddlOpType.SelectedIndex = 0;
        TxtDepoAmt.Text = "";
        ddlduration.SelectedIndex = 0;
        TxtPeriod.Text = "";
        TxtRate.Text = "";
        TxtIntrest.Text = "";
        TxtMaturity.Text = "";
        DtDueDate.Text = "";
        TxtProcode1.Text = "";
        TxtProName1.Text = "";
        TxtAccNo1.Text = "";
        TxtcustName1.Text = "";
    }
    protected void TxtProName1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtcustName1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtAccNo1_TextChanged(object sender, EventArgs e)
    {

    }
}