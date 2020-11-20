using System;
using System.Web.UI;
using System.Data;
public partial class FrmAVS5143 : System.Web.UI.Page
{
    ClsAccopen accop = new ClsAccopen();
    ClsLoanBscReport LB = new ClsLoanBscReport();
    ClsBindDropdown DD = new ClsBindDropdown();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    string Divident, Deposit, D1, D2, M1, M2, Y1, Y2, Y3, Y4, Final;
    double TotalPay = 0;
    string STR = "";
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
                autocustname.ContextKey = Session["BRCD"].ToString();
                DD.BindFinancialActivity(DdlAccActivity);
                TxtMemberNo.Focus();
                BtnCrPost.Visible = false;
               txtChqPrintDate.Text= accop.GetChqPrintDate(Convert.ToString(Session["BRCD"]));
                Clear();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtMemberNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string sql, AT;
            sql = AT = "";

            string custname = accop.GetcustnameYN(listfld: accop.GetCENTCUST(), custno: TxtMemberNo.Text, BRCD: Convert.ToString(Session["BRCD"]));

            if (custname != null)
            {
                string[] name = custname.Split('_');

                 TxtName.Text = name[0].ToString();
               // TxtName.Text = accop.GetCustName(BRCD: Convert.ToString(Session["BRCD"]), CustNo: TxtMemberNo.Text);
              //  txtMarathiName.Text = accop.GetCustName(BRCD: Convert.ToString(Session["BRCD"]), CustNo: TxtMemberNo.Text);

                DT = LB.DIsplayChequeDet(TxtMemberNo.Text, DdlAccActivity.SelectedValue);
                if (DT.Rows.Count > 0)
                {
                    TxtDivident.Text = DT.Rows[0]["Shr_INT"].ToString();
                    TxtTAmt.Text = DT.Rows[0]["DP_INT"].ToString();
                    TxtTotPayAmt.Text = DT.Rows[0]["TotalPay"].ToString();
                    TxtChq.Focus();
                }
                else
                    BtnCrPost.Visible = true;
                TxtDivident.Focus();

            }

            string RC = custname;
            if (RC == "" || RC == null)
            {
                WebMsgBox.Show("Customer not found", this.Page);
                TxtMemberNo.Text = "";
                TxtName.Text = "";
                TxtMemberNo.Focus();
                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtName.Text = custnob[0].ToString();
                TxtMemberNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        string redirectURL,marathiCustName="";
        try
        {
            if (TxtMemberNo.Text != "" && TxtDivident.Text != "" && TxtTAmt.Text != "" && TxtTotPayAmt.Text != "" && TxtChq.Text != "" && TxtBankCode.Text != "")
            {


                Final = "Trial";
                DT = LB.SplitEdateS(Session["EntryDate"].ToString());
                if (DT.Rows.Count > 0)
                {
                    D1 = DT.Rows[0]["D1"].ToString();
                    D2 = DT.Rows[0]["D2"].ToString();
                    M1 = DT.Rows[0]["M1"].ToString();
                    M2 = DT.Rows[0]["M2"].ToString();
                    Y1 = DT.Rows[0]["Y1"].ToString();
                    Y2 = DT.Rows[0]["Y2"].ToString();
                    Y3 = DT.Rows[0]["Y3"].ToString();
                    Y4 = DT.Rows[0]["Y4"].ToString();
                }
               // BtnPrint.Enabled = false;
                if (accop.GetFontType(Convert.ToString(Session["BRCD"])).ToString().Equals("M"))
                {
                    marathiCustName = accop.GetCustName(BRCD:Convert.ToString(Session["BRCD"]),CustNo:TxtMemberNo.Text);

                    redirectURL = "FrmRView.aspx?Name=" + marathiCustName + "&MemberNo=" + TxtMemberNo.Text + "&D1=" + D1 + "&D2=" + D2 + "&M1=" + M1 + "&M2=" + M2 + "&Y1=" + Y1 + "&Y2=" + Y2 + "&Y3=" + Y3 + "&Y4=" + Y4 + "&Divident=" + TxtDivident.Text + "&DepositInt=" + TxtTAmt.Text + "&TotalPay=" + TxtTotPayAmt.Text + "&CheqNo=" + TxtChq.Text + "&BankCode=" + TxtBankCode.Text + "&ChqPrintDate=" + txtChqPrintDate.Text + "&FL=" + DdlAccActivity.SelectedValue + "&Flag=" + Final + " &rptname=RptAVS5143_Marathi.rdlc";
                }
                else
                {
                     redirectURL = "FrmRView.aspx?Name=" + TxtName.Text + "&MemberNo=" + TxtMemberNo.Text + "&D1=" + D1 + "&D2=" + D2 + "&M1=" + M1 + "&M2=" + M2 + "&Y1=" + Y1 + "&Y2=" + Y2 + "&Y3=" + Y3 + "&Y4=" + Y4 + "&Divident=" + TxtDivident.Text + "&DepositInt=" + TxtTAmt.Text + "&TotalPay=" + TxtTotPayAmt.Text + "&CheqNo=" + TxtChq.Text +  "&BankCode=" + TxtBankCode.Text + "&ChqPrintDate=" + txtChqPrintDate.Text + "&FL=" + DdlAccActivity.SelectedValue + "&Flag=" + Final + " &rptname=RptAVS5143.rdlc";
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnReprint_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtMemberNo.Text != "" && TxtDivident.Text != "" && TxtTAmt.Text != "" && TxtTotPayAmt.Text != "" && TxtChq.Text != "" && TxtBankCode.Text != "")
            {
                Final = "Trial";
                DT = LB.SplitEdateS(Session["EntryDate"].ToString());
                if (DT.Rows.Count > 0)
                {
                    D1 = DT.Rows[0]["D1"].ToString();
                    D2 = DT.Rows[0]["D2"].ToString();
                    M1 = DT.Rows[0]["M1"].ToString();
                    M2 = DT.Rows[0]["M2"].ToString();
                    Y1 = DT.Rows[0]["Y1"].ToString();
                    Y2 = DT.Rows[0]["Y2"].ToString();
                    Y3 = DT.Rows[0]["Y3"].ToString();
                    Y4 = DT.Rows[0]["Y4"].ToString();
                }

                string redirectURL = "FrmRView.aspx?Name=" + TxtName.Text + "&MemberNo=" + TxtMemberNo.Text + "&D1=" + D1 + "&D2=" + D2 + "&M1=" + M1 + "&M2=" + M2 + "&Y1=" + Y1 + "&Y2=" + Y2 + "&Y3=" + Y3 + "&Y4=" + Y4 + "&Divident=" + TxtDivident.Text + "&DepositInt=" + TxtTAmt.Text + "&TotalPay=" + TxtTotPayAmt.Text + "&CheqNo=" + TxtChq.Text + "&BankCode=" + TxtBankCode.Text + "&FL=" + DdlAccActivity.SelectedValue + "&Flag=" + Final + " &rptname=RptAVS5143.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtDivident_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Divident = TxtDivident.Text == "" ? "0" : TxtDivident.Text;
            Deposit = TxtTAmt.Text == "" ? "0" : TxtTAmt.Text;
            TotalPay = (Convert.ToDouble(Divident) + Convert.ToDouble(Deposit));
            TxtTotPayAmt.Text = Convert.ToDouble(TotalPay).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTAmt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Divident = TxtDivident.Text == "" ? "0" : TxtDivident.Text;
            Deposit = TxtTAmt.Text == "" ? "0" : TxtTAmt.Text;
            TotalPay = (Convert.ToDouble(Divident) + Convert.ToDouble(Deposit));
            TxtTotPayAmt.Text = Convert.ToDouble(TotalPay).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnBKPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtMemberNo.Text != "" && TxtDivident.Text != "" && TxtTAmt.Text != "" && TxtTotPayAmt.Text != "" && TxtChq.Text != "" && TxtBankCode.Text != "")
            {
                Final = "Trial";
                DT = LB.SplitEdateS(Session["EntryDate"].ToString());
                if (DT.Rows.Count > 0)
                {
                    D1 = DT.Rows[0]["D1"].ToString();
                    D2 = DT.Rows[0]["D2"].ToString();
                    M1 = DT.Rows[0]["M1"].ToString();
                    M2 = DT.Rows[0]["M2"].ToString();
                    Y1 = DT.Rows[0]["Y1"].ToString();
                    Y2 = DT.Rows[0]["Y2"].ToString();
                    Y3 = DT.Rows[0]["Y3"].ToString();
                    Y4 = DT.Rows[0]["Y4"].ToString();
                }

                string redirectURL = "FrmRView.aspx?Name=" + TxtName.Text + "&MemberNo=" + TxtMemberNo.Text + "&D1=" + D1 + "&D2=" + D2 + "&M1=" + M1 + "&M2=" + M2 + "&Y1=" + Y1 + "&Y2=" + Y2 + "&Y3=" + Y3 + "&Y4=" + Y4 + "&Divident=" + TxtDivident.Text + "&DepositInt=" + TxtTAmt.Text + "&ChqPrintDate=" + txtChqPrintDate.Text + "&TotalPay=" + TxtTotPayAmt.Text + "&CheqNo=" + TxtChq.Text + "&BankCode=" + TxtBankCode.Text + "&FL=" + DdlAccActivity.SelectedValue + "&Flag=" + Final + " &rptname=RptAVS5143_BACK.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnPost_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtTotPayAmt.Text == "0.00")
            {
                WebMsgBox.Show("Voucher Not Post Total Amount Is Zero...", this.Page);
                Clear();
                BtnPost.Enabled = false;
            }
            else
            {
                if (TxtMemberNo.Text != "" && TxtDivident.Text != "" && TxtTAmt.Text != "" && TxtTotPayAmt.Text != "" && TxtTotPayAmt.Text != "0.00" && TxtChq.Text != "" && TxtBankCode.Text != "")
                {
                    Final = "Final";


                    STR = LB.GetChequeMsebS_P(TxtMemberNo.Text, TxtDivident.Text, TxtTAmt.Text, TxtTotPayAmt.Text, TxtChq.Text, TxtBankCode.Text, DdlAccActivity.SelectedValue, Final);

                    if (STR != null)
                    {
                        WebMsgBox.Show("Voucher Post successfully with Set No - " + STR, this.Page);
                        FL = "Insert";//Dhanya Shetty 23/09/2017
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DivCalc_post" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        Clear();
                    }
                    BtnPost.Enabled = false;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void Clear()
    {
        TxtMemberNo.Text = "";
        TxtName.Text = "";
        TxtDivident.Text = "";
        TxtTAmt.Text = "";
        TxtTotPayAmt.Text = "";
        TxtChq.Text = "";
        TxtBankCode.Text = "";
        DdlAccActivity.SelectedValue = "0";
    }
    protected void BtnCrPost_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtTotPayAmt.Text == "0.00")
            {
                WebMsgBox.Show("Voucher Not Post Total Amount Is Zero...", this.Page);
                Clear();
                BtnPost.Enabled = false;
            }
            else
            {
                if (TxtMemberNo.Text != "" && TxtDivident.Text != "" && TxtTAmt.Text != "" && TxtTotPayAmt.Text != "" && TxtChq.Text != "" && TxtBankCode.Text != "")
                {
                    Final = "Final";


                    STR = LB.GetChequeMsebS_CrP(Session["BRCD"].ToString(), TxtMemberNo.Text, TxtDivident.Text, TxtTAmt.Text, TxtTotPayAmt.Text, TxtChq.Text, TxtBankCode.Text, DdlAccActivity.SelectedValue, Final);

                    if (STR != null)
                    {
                        WebMsgBox.Show("Voucher Post successfully with Set No - " + STR, this.Page);
                        FL = "Insert";//Dhanya Shetty 23/09/2017
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DivCalc_post" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        Clear();
                    }
                    BtnPost.Enabled = false;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}