using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmInvClosure : System.Web.UI.Page
{
    ClsInvAccountMaster IAM = new ClsInvAccountMaster();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsBindDropdown BD = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    ClsInvClosure IC = new ClsInvClosure();
    ClsAuthorized AA = new ClsAuthorized();
    ClsOpenClose OC = new ClsOpenClose();
    scustom customcs = new scustom();
    DataTable DT = new DataTable();
    static double Total = 0;
    int resultint;
    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            autoglname.ContextKey = Session["BRCD"].ToString();
            AutoCompleteExtender1.ContextKey = Session["BRCD"].ToString();
            autoglnametrf.ContextKey = Session["BRCD"].ToString();
            TxtPRDNo.Focus();
            BD.BindIntrstPayout(ddlIntType);
            //BD.BindINTType(Ddl_IntType, Session["BRCD"].ToString());
            //BD.BindINTType(ddlMddl, Session["BRCD"].ToString());
           // BindGrid();

        }
    }

    #region TextChangeClose
    protected void TxtPRDNo_TextChanged(object sender, EventArgs e)
    {

        try
        {
            string tds = BD.GetLoanGL(TxtPRDNo.Text, Session["BRCD"].ToString());
            if (tds != null)
            {
                string[] TD = tds.Split('_');

                TxtPRDName.Text = TD[0].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtPRDNo.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["GL"] = GLS[1].ToString();
                // AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtPRDNo.Text + "_" + ViewState["GL"].ToString();
                //BD.BindAccStatus(TxtAccNo, Session["BRCD"].ToString(), TxtPRDNo.Text);
                TxtAccNo.Focus();

            }
            else
            {
                WebMsgBox.Show("Invalid Product Code......!", this.Page);
                Clear();
                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtPRDName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtPRDName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {

                TxtPRDName.Text = CT[0].ToString();
                TxtPRDNo.Text = CT[1].ToString();
                // TxtGLCD.Text = CT[2].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtPRDNo.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["GL"] = GLS[1].ToString();
                //BD.BindAccStatus(TxtAccNo, Session["BRCD"].ToString(), TxtPRDNo.Text);
                // AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtPRDNo.Text + "_" + ViewState["GL"].ToString();

                if (TxtPRDName.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    TxtPRDNo.Text = "";
                    TxtPRDNo.Focus();

                }
                else
                {
                    TxtAccNo.Focus();
                }


            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
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

                TxtAccName.Text = custnob[1].ToString();
                TxtAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] TD = Session["EntryDate"].ToString().Split('/');

                if (TxtAccNo.Text == "")
                {
                    TxtAccName.Text = "";
                    return;
                }
                string ClosBal = OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtPRDNo.Text, "0", Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString()).ToString();
                Ddl_Remark.Focus();

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
        }
    }
    protected void TxtROI_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string DD = "", MM = "", PTYPE = "", INTPAY = "";
            if (Ddl_IntType.SelectedValue != "0")
            {

                if (TxtROI.Text != "")
                {
                    if (TxtMonths.Text != "" && (TxtDays.Text == "" || TxtDays.Text == "0"))
                    {
                        DD = (Convert.ToInt32(TxtMonths.Text) * 30).ToString();
                        PTYPE = "Days";

                    }
                    if (TxtDays.Text != "" && TxtDays.Text != "0")
                    {
                        if (TxtMonths.Text == "" || TxtMonths.Text == "0")
                            DD = (Convert.ToInt32(TxtDays.Text)).ToString();
                        else
                            DD = ((Convert.ToInt32(TxtMonths.Text) * 30) + Convert.ToInt32(TxtDays.Text)).ToString();
                        PTYPE = "Days";
                    }


                    CalculatedepositINT((float)Convert.ToDouble(TxtInstPrinci.Text), "", (float)Convert.ToDouble(TxtROI.Text), Convert.ToInt32(DD), "ON MATURITY", PTYPE);

                }
            }
            else
            {
                WebMsgBox.Show("Select Int Type First....!", this.Page);
                Ddl_IntType.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTrfAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string tds = BD.GetLoanGL(TxtTrfAccNo.Text, Session["BRCD"].ToString());
            if (tds != null)
            {
                string[] TD = tds.Split('_');
                TxtTrfAccName.Text = TD[0].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtTrfAccNo.Text, Session["BRCD"].ToString()).Split('_');
                //ViewState["GL"] = GLS[1].ToString();
                //AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtPRDNo.Text + "_" + ViewState["GL"].ToString();
                txtTrfAcc.Focus();
                
            }
            else
            {
                WebMsgBox.Show("Invalid Product Code......!", this.Page);
                TxtTrfAccNo.Text = "";
                TxtTrfAccNo.Focus();
            }
        }
        catch (Exception eX)
        {
            ExceptionLogging.SendErrorToText(eX);
        }
    }
    protected void TxtTrfAccName_TextChanged(object sender, EventArgs e)
    {//This account is as same as Product code and product name
        try
        {
            string custno = TxtTrfAccName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {

                TxtTrfAccName.Text = CT[0].ToString();
                TxtTrfAccNo.Text = CT[1].ToString();

                if (TxtTrfAccName.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    TxtTrfAccNo.Text = "";
                    TxtTrfAccNo.Focus();

                }
                else
                {
                    txtTrfAcc.Focus();
                }

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtMonths.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAccNo_TextChanged(object sender, EventArgs e)
    {
        BindAcc();
    }
    public void BindAcc()
    {
        if (rblType.SelectedValue == "1" || rblType.SelectedValue == "3")
        {
            DataTable dt = new DataTable();
            dt = BD.BindAccStatus(TxtAccNo.Text, Session["BRCD"].ToString(), TxtPRDNo.Text);
            if (dt.Rows.Count > 0)
            {
                TxtAccName.Text = dt.Rows[0]["BankName"].ToString();
            }
            else
            {
                WebMsgBox.Show("Invalid Account No", this.Page);
                return;
            }
        }
        else
            TxtAccName.Text = "";
    }
    #endregion

    #region DropDown Change
    protected void Ddl_Remark_TextChanged(object sender, EventArgs e)
    {
        BindDDL();
    }
    protected void Ddl_IntType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string DD = "", MM = "", PTYPE = "";
            if (TxtROI.Text != "" && TxtMonths.Text != "" && TxtDays.Text != "" && TxtInstPrinci.Text != "")
            {
                if (TxtMonths.Text != "" && (TxtDays.Text == "" || TxtDays.Text == "0"))
                {
                    DD = (Convert.ToInt32(TxtMonths.Text) * 30).ToString();
                    PTYPE = "Days";

                }
                if (TxtDays.Text != "" && TxtDays.Text != "0")
                {
                    if (TxtMonths.Text == "" || TxtMonths.Text == "0")
                        DD = (Convert.ToInt32(TxtDays.Text)).ToString();
                    else
                        DD = ((Convert.ToInt32(TxtMonths.Text) * 30) + Convert.ToInt32(TxtDays.Text)).ToString();
                    PTYPE = "Days";
                }


                CalculatedepositINT((float)Convert.ToDouble(TxtInstPrinci.Text), "", (float)Convert.ToDouble(TxtROI.Text), Convert.ToInt32(DD), "ON MATURITY", PTYPE);
                TxtDate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void rblType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblType.SelectedValue == "1" || rblType.SelectedValue == "3")
        {
            if (rblType.SelectedValue == "3")
            {
                TxtInAmt.Enabled = false;
                TxtInt.Enabled = false;
                TxtIntPaid.Enabled = false;
            }
            else
            {
                TxtInAmt.Enabled = true;
                TxtInt.Enabled = true;
                TxtIntPaid.Enabled = true;
            }
            DivCerti.Visible = false;
            //divtrf.Visible = true;
            TxtPRDNo.Text = hdnprd.Value;
            TxtPRDName.Text = hdnprdname.Value;
            TxtAccNo.Text = hdnacc.Value;
            BindDDL();
            ShowAcc();
        }
        else
        {
            DivCerti.Visible = true;
            //divtrf.Visible = false;
            TxtAccNo.Text = Convert.ToInt32(IAM.GetNextReceiptNo(Session["BRCD"].ToString(), TxtPRDNo.Text.Trim().ToString())).ToString();
        }
    }
    protected void ddlMddl_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string DD = "", MM = "", PTYPE = "";
            if (TxtCROI.Text != "" && TxtCMonths.Text != "" && TxtCDays.Text != "" && TxtInstPrinci.Text != "")
            {
                if (TxtCMonths.Text != "" && (TxtCDays.Text == "" || TxtCDays.Text == "0"))
                {
                    DD = (Convert.ToInt32(TxtCMonths.Text) * 30).ToString();
                    PTYPE = "Days";

                }
                if (TxtCDays.Text != "" && TxtCDays.Text != "0")
                {
                    if (TxtCMonths.Text == "" || TxtCMonths.Text == "0")
                        DD = (Convert.ToInt32(TxtCDays.Text)).ToString();
                    else
                        DD = ((Convert.ToInt32(TxtCMonths.Text) * 30) + Convert.ToInt32(TxtCDays.Text)).ToString();
                    PTYPE = "Days";
                }


                CalculateDetails((float)Convert.ToDouble(TxtInstPrinci.Text), "", (float)Convert.ToDouble(TxtCROI.Text), Convert.ToInt32(DD), "ON MATURITY", PTYPE);
                TxtAsOffDate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void CalculateDetails(float amt, string subgl, float intrate, int Period, string intpay, string PTYPE)
    {
        try
        {
            float interest = 0;
            float maturityamt = 0;
            float QUATERS = 0;
            float tmp1 = 0;
            float tmp2 = 0;


            string category = ddlMddl.SelectedItem.Text;

            if (category == "")
            {
                return;
            }
            float Int = 0;
            if (TxtInstPrinci.Text == "")
                Int = 0;
            else
                Int = Convert.ToInt32(txtIntprovision.Text);
            switch (category)
            {
                case "DD":
                    interest = amt - Int;
                    maturityamt = interest + amt - Int;
                    txtMIntCal.Text = interest.ToString();
                    TxtMatValue.Text = maturityamt.ToString();
                    break;

                case "MIS":
                    interest = Convert.ToInt32(amt * intrate / (1200 + intrate));
                    maturityamt = interest + amt - Int;
                    txtMIntCal.Text = interest.ToString();
                    TxtMatValue.Text = maturityamt.ToString();
                    break;

                case "CUM":
                    Period = Period / 30;
                    QUATERS = (Period / 3);
                    maturityamt = Convert.ToInt32((Math.Pow(((intrate / 400) + 1), (QUATERS))) * amt) + Int;
                    interest = maturityamt - amt - Int;
                    txtMIntCal.Text = interest.ToString("N");
                    TxtMatValue.Text = maturityamt.ToString("N");
                    break;

                case "FDS":

                    int Amt = 0, Amt1 = 0;
                    if (intpay == "On Maturity" || intpay == "ON MATURITY")
                    {
                        if (PTYPE == "Days" || PTYPE == "DAYS" || PTYPE == "idvasa")
                        {
                            interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 365) * (Period)) - Int;


                            maturityamt = (interest) + (amt);

                            txtMIntCal.Text = interest.ToString();
                            TxtMatValue.Text = maturityamt.ToString();
                        }
                        else if (PTYPE == "Months" || PTYPE == "MONTHS" || PTYPE == "maihnao")
                        {

                            interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (Period)) - Int;

                            maturityamt = (interest) + (amt);
                            txtMIntCal.Text = interest.ToString();
                            TxtMatValue.Text = maturityamt.ToString();
                        }
                        if (Convert.ToDouble(TxtCDays.Text) != 0)
                            Amt = Convert.ToInt32((((Convert.ToDouble(TxtInstPrinci.Text) * (intrate) / 100)) / 365) * (Convert.ToDouble(TxtCDays.Text)));
                        else
                            Amt = 0;
                        if (Convert.ToDouble(TxtCMonths.Text) != 0)
                            Amt1 = Convert.ToInt32((((Convert.ToDouble(TxtInstPrinci.Text) * (intrate) / 100)) / 12) * (Convert.ToDouble(TxtCMonths.Text)));
                        else
                            Amt1 = 0;
                        txtMIntCal.Text = (Amt + Amt1).ToString();
                        TxtMatValue.Text = (amt + Amt + Amt1).ToString();
                        TextBox13.Text = (amt + Amt + Amt1).ToString();
                        //TxtInstPrinci.Text = (amt + Amt + Amt1).ToString();
                    }
                    else if (intpay == "Quaterly" || intpay == "QUATERLY")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (3)) - Int;
                        maturityamt = interest + amt;
                        txtMIntCal.Text = interest.ToString();
                        TxtMatValue.Text = maturityamt.ToString();
                    }
                    else if (intpay == "Half Yearly" || intpay == "HALF YEARLY")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (6)) - Int;
                        maturityamt = interest + amt;
                        txtMIntCal.Text = interest.ToString();
                        TxtMatValue.Text = maturityamt.ToString();
                    }

                    else if (intpay == "Monthly" || intpay == "MONTHLY" || PTYPE == "maihano")
                    {
                        interest = Convert.ToInt32((amt) * (intrate) / 1209.75) - Int;
                        maturityamt = interest + amt;
                        //maturityamt = interest + amt;
                        txtMIntCal.Text = interest.ToString();
                        TxtMatValue.Text = maturityamt.ToString();
                    }

                    break;
                case "DP":

                    if (PTYPE == "Days" || PTYPE == "DAYS" || PTYPE == "idvasa")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 365) * (Period)) - Int;
                        maturityamt = (interest) + (amt) - Int;
                        txtMIntCal.Text = interest.ToString();
                        TxtMatValue.Text = maturityamt.ToString();
                    }
                    else if (PTYPE == "Months" || PTYPE == "MONTHS" || PTYPE == "maihnao")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (Period)) - Int;
                        maturityamt = (interest) + (amt) - Int;
                        txtMIntCal.Text = interest.ToString();
                        TxtMatValue.Text = maturityamt.ToString();
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
    public void BindDDL()
    {
        try
        {
           // if (Ddl_Remark.SelectedValue == "2")
           // {
                DT = IC.GetDetails(Session["BRCD"].ToString(), TxtPRDNo.Text, TxtAccNo.Text);
                if (DT.Rows.Count > 0)
                {
                    //BRCD	BankCode	SubGlCode	ReceiptNo	CertDate	Days	Months	Years	PrincipleAmt	AssOfdate	MainBalance	RateOfInt	MaturityDate	MaturityAmt	TrfAccID

                    TxtDate.Text = DT.Rows[0]["CertDate"].ToString().Replace("12:00:00", "");
                    TxtMonths.Text = DT.Rows[0]["Months"].ToString();
                    TxtDays.Text = DT.Rows[0]["Days"].ToString();
                    TxtTotalMaturity.Text = DT.Rows[0]["MaturityAmt"].ToString(); 
                    TxtROI.Text = DT.Rows[0]["RateOfInt"].ToString();
                    TxtIntCalc.Text = (Convert.ToDouble(TxtMatValue.Text) - Convert.ToDouble(TxtInstPrinci.Text)).ToString();
                }
                else
                {
                    WebMsgBox.Show("Data not Found Related to Receipt A/C No. " + TxtAccNo.Text + "", this.Page);
                    TxtAccNo.Focus();
                }
                TxtTrfAccNo.Focus();
          // }
          // else
          // {
          //     TxtTrfAccNo.Focus();
          // }
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


            string category = Ddl_IntType.SelectedItem.Text;

            if (category == "")
            {
                return;
            }
            float Int = 0;
            if (TxtInAmt.Text == "")
                Int = 0;
            else
                Int = Convert.ToInt32(TxtInt.Text);
            switch (category)
            {
                case "DD":
                    interest = amt - Int;
                    maturityamt = interest + amt - Int;
                    TxtIntCalc.Text = interest.ToString();
                    TxtTotalMaturity.Text = maturityamt.ToString();
                    break;

                case "MIS":
                    interest = Convert.ToInt32(amt * intrate / (1200 + intrate));
                    maturityamt = interest + amt - Int;
                    TxtIntCalc.Text = interest.ToString();
                    TxtTotalMaturity.Text = maturityamt.ToString();
                    break;

                case "CUM":
                    Period = Period / 30;
                    QUATERS = (Period / 3);
                    maturityamt = Convert.ToInt32((Math.Pow(((intrate / 400) + 1), (QUATERS))) * amt) + Int;
                    interest = maturityamt - amt - Int;
                    TxtIntCalc.Text = interest.ToString();
                    TxtTotalMaturity.Text = maturityamt.ToString();
                    break;

                case "FDS":

                    int Amt = 0, Amt1 = 0;
                    if (intpay == "On Maturity" || intpay == "ON MATURITY")
                    {
                        if (PTYPE == "Days" || PTYPE == "DAYS" || PTYPE == "idvasa")
                        {
                            interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 365) * (Period)) - Int;


                            maturityamt = (interest) + (amt);

                            TxtIntCalc.Text = interest.ToString("N");
                            TxtTotalMaturity.Text = maturityamt.ToString("N");
                        }
                        else if (PTYPE == "Months" || PTYPE == "MONTHS" || PTYPE == "maihnao")
                        {

                            interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (Period)) - Int;

                            maturityamt = (interest) + (amt);
                            TxtIntCalc.Text = interest.ToString();
                            TxtTotalMaturity.Text = maturityamt.ToString();
                        }
                        if (Convert.ToDouble(TxtDays.Text) != 0)
                            Amt = Convert.ToInt32((((Convert.ToDouble(TxtInAmt.Text) * (intrate) / 100)) / 365) * (Convert.ToDouble(TxtDays.Text)));
                        else
                            Amt = 0;
                        if (Convert.ToDouble(TxtMonths.Text) != 0)
                            Amt1 = Convert.ToInt32((((Convert.ToDouble(TxtInAmt.Text) * (intrate) / 100)) / 12) * (Convert.ToDouble(TxtMonths.Text)));
                        else
                            Amt1 = 0;
                        TxtIntCalc.Text = (Amt + Amt1).ToString();
                        TxtTotalMaturity.Text = (amt + Amt + Amt1).ToString();
                        txtNet.Text = (amt + Amt + Amt1).ToString();
                        TxtInstPrinci.Text = (amt + Amt + Amt1).ToString();
                    }
                    else if (intpay == "Quaterly" || intpay == "QUATERLY")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (3)) - Int;
                        maturityamt = interest + amt;
                        TxtIntCalc.Text = interest.ToString();
                        TxtTotalMaturity.Text = maturityamt.ToString();
                    }
                    else if (intpay == "Half Yearly" || intpay == "HALF YEARLY")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (6)) - Int;
                        maturityamt = interest + amt;
                        TxtIntCalc.Text = interest.ToString();
                        TxtTotalMaturity.Text = maturityamt.ToString();
                    }

                    else if (intpay == "Monthly" || intpay == "MONTHLY" || PTYPE == "maihano")
                    {
                        interest = Convert.ToInt32((amt) * (intrate) / 1209.75) - Int;
                        maturityamt = interest + amt;
                        //maturityamt = interest + amt;
                        TxtIntCalc.Text = interest.ToString();
                        TxtTotalMaturity.Text = maturityamt.ToString();
                    }

                    break;
                case "DP":

                    if (PTYPE == "Days" || PTYPE == "DAYS" || PTYPE == "idvasa")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 365) * (Period)) - Int;
                        maturityamt = (interest) + (amt) - Int;
                        TxtIntCalc.Text = interest.ToString();
                        TxtTotalMaturity.Text = maturityamt.ToString();
                    }
                    else if (PTYPE == "Months" || PTYPE == "MONTHS" || PTYPE == "maihnao")
                    {
                        interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (Period)) - Int;
                        maturityamt = (interest) + (amt) - Int;
                        TxtIntCalc.Text = interest.ToString();
                        TxtTotalMaturity.Text = maturityamt.ToString();
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
    #endregion

    #region TextChange REnewal
    protected void TxtIntPaid_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtTDSDeduct.Focus();
            //TxtintProvi.Text = "0";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAccNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowAcc();
    }
    protected void txtInvProd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string tds = BD.GetLoanGL(txtInvProd.Text, Session["BRCD"].ToString());
            if (tds != null)
            {
                string[] TD = tds.Split('_');

                txtInvPrdNm.Text = TD[0].ToString();
                string[] GLS = BD.GetAccTypeGL(txtInvProd.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["GL"] = GLS[1].ToString();
                // AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtPRDNo.Text + "_" + ViewState["GL"].ToString();
                //BD.BindAccStatus(TxtAccNo, Session["BRCD"].ToString(), TxtPRDNo.Text);
                txtInvAc.Focus();

            }
            else
            {
                WebMsgBox.Show("Invalid Product Code......!", this.Page);
                Clear();
                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtInvPrdNm_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = txtInvPrdNm.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {

                txtInvPrdNm.Text = CT[0].ToString();
                txtInvProd.Text = CT[1].ToString();
                // TxtGLCD.Text = CT[2].ToString();
                string[] GLS = BD.GetAccTypeGL(txtInvProd.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["GL"] = GLS[1].ToString();
                //BD.BindAccStatus(TxtAccNo, Session["BRCD"].ToString(), TxtPRDNo.Text);
                // AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtPRDNo.Text + "_" + ViewState["GL"].ToString();

                if (txtInvPrdNm.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    txtInvProd.Text = "";
                    txtInvProd.Focus();

                }
                else
                {
                    txtInvAc.Focus();
                }


            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void txtInvAc_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = BD.BindAccStatus(txtInvAc.Text, Session["BRCD"].ToString(), txtInvProd.Text);
        if (dt.Rows.Count > 0)
        {
            txtInvAcName.Text = dt.Rows[0]["BankName"].ToString();
        }
        else
        {
            WebMsgBox.Show("Invalid Account No", this.Page);
            return;
        }
    }
    protected void txtMIntPaid_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtMTDS.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAsOffDate_TextChanged(object sender, EventArgs e)
    {
        if (TxtCDays.Text != "0" && TxtCMonths.Text != "0")
        {
            TxtMatDate.Text = Convert.ToDateTime(conn.AddMonthDay7(TxtAsOffDate.Text, TxtCMonths.Text, "M").Replace("12:00:00", "")).ToString("dd/MM/yyy").Replace("-", "/");
            TxtMatDate.Text = Convert.ToDateTime(conn.AddMonthDay7(TxtMatDate.Text, TxtCDays.Text, "D").Replace("12:00:00", "")).ToString("dd/MM/yyy").Replace("-", "/");
        }
        else
        {
            if (TxtCMonths.Text != "0")
            {
                TxtMatDate.Text = Convert.ToDateTime(conn.AddMonthDay7(TxtAsOffDate.Text, TxtCMonths.Text, "M").Replace("12:00:00", "")).ToString("dd/MM/yyy").Replace("-", "/");
            }
            else if (TxtCDays.Text != "0")
            {
                TxtMatDate.Text = Convert.ToDateTime(conn.AddMonthDay7(TxtAsOffDate.Text, TxtCDays.Text, "D").Replace("12:00:00", "")).ToString("dd/MM/yyy").Replace("-", "/");
            }
        }
        string DD = "", MM = "", PTYPE = "";
        if (TxtCROI.Text != "" && TxtCMonths.Text != "" && TxtCDays.Text != "" && TxtInstPrinci.Text != "")
        {
            if (TxtCMonths.Text != "" && (TxtCDays.Text == "" || TxtCDays.Text == "0"))
            {
                DD = (Convert.ToInt32(TxtCMonths.Text) * 30).ToString();
                PTYPE = "Days";

            }
            if (TxtCDays.Text != "" && TxtCDays.Text != "0")
            {
                if (TxtCMonths.Text == "" || TxtCMonths.Text == "0")
                    DD = (Convert.ToInt32(TxtCDays.Text)).ToString();
                else
                    DD = ((Convert.ToInt32(TxtCMonths.Text) * 30) + Convert.ToInt32(TxtCDays.Text)).ToString();
                PTYPE = "Days";
            }


            CalculateDetails((float)Convert.ToDouble(TxtInstPrinci.Text), "", (float)Convert.ToDouble(TxtCROI.Text), Convert.ToInt32(DD), "ON MATURITY", PTYPE);
            TxtAsOffDate.Focus();
        }
    }
 
    protected void txtTtrfAcc_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string Flag = "";
            Flag = IC.GetFlag(Session["BRCD"].ToString(), TxtTrfAccNo.Text, txtTrfAcc.Text);
            if (Flag != "" || Flag != null)
            {
                if (Flag == TxtTrfAccNo.Text)
                {
                    WebMsgBox.Show("Internal ACCNO is not available", this.Page);
                    txtTrfAcc.Text = Flag;
                }
            }
            else
            {
                WebMsgBox.Show("Invalid A/C no", this.Page);
                txtTrfAcc.Focus();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    #endregion

    #region Button Click
    protected void Btn_ClearAll_Click(object sender, EventArgs e)
    {
        Clear();
    }


    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtIntAccNo.Text == "" )
            {
                WebMsgBox.Show("Please Update Int A/C No from Investment Master", this.Page);
            }
            else if (ViewState["INT_AC"].ToString() == "" || ViewState["INT_AC"].ToString() == null)
            {
                WebMsgBox.Show("Please Update Received A/C No from Investment Master", this.Page);
            }
            else
            {
                if (rblType.SelectedValue == "1")
                {
                    if (TxtPRDNo.Text != "" && TxtAccNo.Text != "" && TxtTrfAccNo.Text != "")
                    {
                        string REFERENCEID = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                        ViewState["REFID"] = (Convert.ToInt32(REFERENCEID) + 1).ToString();
                        double AMT = 0;
                        int RES = 0;
                        string SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
                        AMT = Convert.ToInt32(Convert.ToDouble(TxtInAmt.Text.Replace(".00", "")) + Convert.ToDouble(TxtIntCalc.Text) + Convert.ToDouble(TxtInt.Text) - Convert.ToDouble(TxtTDSDeduct.Text));
                        //Debit to TRF A/C no under PRDNO with AMOUNT=PRINCIpal
                        RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), TxtTrfAccNo.Text, TxtTrfAccNo.Text, txtTrfAcc.Text == "" ? "0" : txtTrfAcc.Text,
                                              "TRF From " + TxtAccNo.Text + " ", "", Convert.ToString(Convert.ToDecimal(AMT)), "2", "7", "TR", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "INV-CLO", "0",
                                              "N/A", ViewState["REFID"].ToString(), "0");
                        //Credit to A/C no under PRDNO with AMOUNT=PRINCIpal
                        RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString(), TxtPRDNo.Text, TxtAccNo.Text,
                                            "TRF To " + TxtTrfAccNo.Text + " ", "", TxtInAmt.Text, "1", "7", "TR", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "INV-CLO", "0",
                                            "N/A", ViewState["REFID"].ToString(), "0");
                        if (RES > 0)
                        {
                            AA.UpdateInvMaster(TxtAccNo.Text, TxtPRDNo.Text);
                            AMT = Convert.ToDouble(TxtIntCalc.Text);
                            string GLC = BD.GetAccTypeGL(TxtIntAccNo.Text, Session["BRCD"].ToString());
                            string[] GL = GLC.Split('_');

                            if (TxtIntCalc.Text != "" && TxtIntCalc.Text != "0")
                            {
                                RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GL[1].ToString(), TxtIntAccNo.Text, "0",
                                             "Inv Int by " + TxtPRDNo.Text + "/" + TxtAccNo.Text + "", "", AMT.ToString(), "1", "7", "TR", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "INV-CLO", "0",
                                             "N/A", ViewState["REFID"].ToString(), "0");
                                string GLC12 = BD.GetAccTypeGL(ViewState["INT_RECVPRD"].ToString(), Session["BRCD"].ToString());
                                string[] GL12 = GLC12.Split('_');
                                RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GL12[1].ToString(), ViewState["INT_RECVPRD"].ToString(), ViewState["INT_AC"].ToString(),
                                                 "Int received on " + TxtPRDNo.Text + "/ " + TxtIntAccNo.Text + " this to " + TxtTrfAccNo.Text + "/" + txtTrfAcc.Text + "", "", TxtIntCalc.Text, "1", "7", "TR_INT", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "INV-CLO", "0",
                                                            "N/A", ViewState["REFID"].ToString(), "0");
                                RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GL12[1].ToString(), ViewState["INT_RECVPRD"].ToString(), ViewState["INT_AC"].ToString(),
                                                     "Int received on " + TxtPRDNo.Text + "/ " + TxtIntAccNo.Text + " this to " + TxtTrfAccNo.Text + "/" + txtTrfAcc.Text + "", "", TxtIntCalc.Text, "2", "7", "TR_INT", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "INV-CLO", "0",
                                                                "N/A", ViewState["REFID"].ToString(), "0");
                            }
                        }
                        if (RES > 0)
                        {
                            if (TxtInt.Text != "" && TxtInt.Text != "0")
                            {
                                string GLC1 = BD.GetAccTypeGL(ViewState["INT_RECVPRD"].ToString(), Session["BRCD"].ToString());
                                string[] GL1 = GLC1.Split('_');
                                RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GL1[1].ToString(), ViewState["INT_RECVPRD"].ToString(), ViewState["INT_AC"].ToString(),
                                            "Inv Provi " + TxtPRDNo.Text + "/" + TxtAccNo.Text + "", "", TxtInt.Text, "1", "7", "TR", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "INV-CLO", "0",
                                            "N/A", ViewState["REFID"].ToString(), "0");
                            }
                        }
                        if (TxtTDSDeduct.Text != "" && TxtTDSDeduct.Text != "0")
                        {
                            string GLC2 = BD.GetAccTypeGL(ViewState["TDS_PRD"].ToString(), Session["BRCD"].ToString());
                            string[] GL2 = GLC2.Split('_');
                            RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GL2[1].ToString(), ViewState["TDS_PRD"].ToString(), "0",
                                        "Inv TDS Deduct " + TxtTrfAccNo.Text + "", "", TxtTDSDeduct.Text, "2", "7", "TR", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "INV-CLO", "0",
                                        "N/A", ViewState["REFID"].ToString(), "0");

                            //RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), TxtTrfAccNo.Text, TxtTrfAccNo.Text, "0",
                            //           "Inv TDS Deduct " + TxtTrfAccNo.Text + "", "", TxtTDSDeduct.Text, "2", "7", "TR", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "INV-CLO", "0",
                            //           "N/A", ViewState["REFID"].ToString(), "0");
                        }


                        if (RES > 0)
                        {
                            lblMessage.Text = "";
                            lblMessage.Text = "Voucher Posted with No. " + SetNo + "";
                            ModalPopup.Show(this.Page);
                            FL = "Insert";//Dhanya Shetty
                            string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_Closure _" + SetNo + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            Clear();

                        }

                    }
                    else
                    {
                        WebMsgBox.Show("Enter Valid Data....!", this.Page);
                        TxtPRDNo.Focus();
                    }
                }
                else if (rblType.SelectedValue == "2")
                {
                    
                    if (TxtPRDNo.Text != "" && hdnacc.Value != "" && TxtTrfAccNo.Text != "")
                    {
                        string REFERENCEID = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                        ViewState["REFID"] = (Convert.ToInt32(REFERENCEID) + 1).ToString();
                        double AMT = 0;
                        int RES = 0;
                        string SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
                        AMT = Convert.ToDouble(TxtIntCalc.Text) - Convert.ToDouble(TxtIntPaid.Text);
                        //Debit to TRF A/C no under PRDNO with AMOUNT=PRINCIpal
                        if (Convert.ToDouble(TxtTotalMaturity.Text) - Convert.ToDouble(TxtInstPrinci.Text) > 0)
                        {
                            RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), TxtTrfAccNo.Text, TxtTrfAccNo.Text, txtTrfAcc.Text == "" ? "0" : txtTrfAcc.Text,
                                                  "TRF From " + hdnacc.Value + " ", "", (Convert.ToDouble(TxtTotalMaturity.Text) - Convert.ToDouble(TxtInstPrinci.Text)).ToString(), "2", "7", "TR", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "INV-CLO", "0",
                                                  "N/A", ViewState["REFID"].ToString(), "0");
                        }
                        //Credit to A/C no under PRDNO with AMOUNT=PRINCIpal
                        RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString(), TxtPRDNo.Text, hdnacc.Value,
                                            "TRF To " + TxtTrfAccNo.Text + " ", "", TxtInAmt.Text, "1", "7", "TR", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "INV-CLO", "0",
                                            "N/A", ViewState["REFID"].ToString(), "0");
                        RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString(), TxtPRDNo.Text, TxtAccNo.Text,
                                            "TRF To " + TxtTrfAccNo.Text + " ", "", TxtInstPrinci.Text, "2", "7", "TR", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "INV-CLO", "0",
                                            "N/A", ViewState["REFID"].ToString(), "0");
                        if (RES > 0)
                        {
                            AA.UpdateInvMaster(hdnacc.Value, TxtPRDNo.Text);
                            AA.InvRenewal(hdnacc.Value, TxtAccNo.Text, TxtPRDNo.Text, TxtAsOffDate.Text, TxtMatDate.Text, TxtCMonths.Text, TxtCROI.Text, TxtInstPrinci.Text, TxtMatValue.Text, Session["EntryDate"].ToString(), ddlIntType.SelectedValue, txtLastDate.Text);

                            string GLC = BD.GetAccTypeGL(TxtIntAccNo.Text, Session["BRCD"].ToString());
                            string[] GL = GLC.Split('_');
                            if (Convert.ToDouble(TxtIntCalc.Text) > 0)
                            {
                                RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GL[1].ToString(), TxtIntAccNo.Text, "0",
                                             "Inv Int by " + TxtPRDNo.Text + "/" + hdnacc.Value + "", "", TxtIntCalc.Text, "1", "7", "TR", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "INV-CLO", "0",
                                             "N/A", ViewState["REFID"].ToString(), "0");
                            }
                        }
                        if (RES > 0)
                        {
                            string GLC1 = BD.GetAccTypeGL(ViewState["INT_RECVPRD"].ToString(), Session["BRCD"].ToString());
                            string[] GL1 = GLC1.Split('_');
                            AMT = Convert.ToDouble(TxtInt.Text);

                            if (Convert.ToDouble(AMT) > 0)
                            {
                                RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GL1[1].ToString(), ViewState["INT_RECVPRD"].ToString(), ViewState["INT_AC"].ToString(),
                                            "Inv Provi " + TxtPRDNo.Text + "/" + hdnacc.Value + "", "", AMT.ToString(), "1", "7", "TR", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "INV-CLO", "0",
                                            "N/A", ViewState["REFID"].ToString(), "0");
                                string GLC12 = BD.GetAccTypeGL(ViewState["INT_RECVPRD"].ToString(), Session["BRCD"].ToString());
                                string[] GL12 = GLC12.Split('_');
                                RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GL12[1].ToString(), ViewState["INT_RECVPRD"].ToString(), ViewState["INT_AC"].ToString(),
                                                 "Int received on " + TxtPRDNo.Text + "/ " + TxtIntAccNo.Text + " this to " + TxtTrfAccNo.Text + "/" + txtTrfAcc.Text + "", "", TxtIntCalc.Text, "1", "7", "TR_INT", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "INV-CLO", "0",
                                                            "N/A", ViewState["REFID"].ToString(), "0");
                                RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GL12[1].ToString(), ViewState["INT_RECVPRD"].ToString(), ViewState["INT_AC"].ToString(),
                                                     "Int received on " + TxtPRDNo.Text + "/ " + TxtIntAccNo.Text + " this to " + TxtTrfAccNo.Text + "/" + txtTrfAcc.Text + "", "", TxtIntCalc.Text, "2", "7", "TR_INT", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "INV-CLO", "0",
                                                                "N/A", ViewState["REFID"].ToString(), "0");
                            }
                        }
                        if (TxtTDSDeduct.Text != "" && TxtTDSDeduct.Text != "0")
                        {
                            string GLC2 = BD.GetAccTypeGL(ViewState["TDS_PRD"].ToString(), Session["BRCD"].ToString());
                            string[] GL2 = GLC2.Split('_');
                            RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GL2[1].ToString(), ViewState["TDS_PRD"].ToString(), "0",
                                        "Inv TDS Deduct " + TxtTrfAccNo.Text + "", "", TxtTDSDeduct.Text, "2", "7", "TR", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "INV-CLO", "0",
                                        "N/A", ViewState["REFID"].ToString(), "0");

                            //RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), TxtTrfAccNo.Text, TxtTrfAccNo.Text, "0",
                            //           "Inv TDS Deduct " + TxtTrfAccNo.Text + "", "", TxtTDSDeduct.Text, "2", "7", "TR", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "INV-CLO", "0",
                            //           "N/A", ViewState["REFID"].ToString(), "0");
                        }


                        if (RES > 0)
                        {
                            lblMessage.Text = "";
                            lblMessage.Text = "Voucher Posted with No. " + SetNo + "";
                            ModalPopup.Show(this.Page);
                            FL = "Insert";//Dhanya Shetty
                            string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_Closure _" + SetNo + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            Clear();

                        }

                    }
                    else
                    {
                        WebMsgBox.Show("Enter Valid Data....!", this.Page);
                        TxtPRDNo.Focus();
                    }
                }
                else
                {
                    if (TxtIntCalc.Text != "0")
                    {
                        int RES = 0;
                        string ac = IC.GetAccno(TxtPRDNo.Text, TxtAccNo.Text);
                        string REFERENCEID = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                        ViewState["REFID"] = (Convert.ToInt32(REFERENCEID) + 1).ToString();
                        string SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
                        double amount = Convert.ToDouble(TxtIntCalc.Text) - Convert.ToDouble(TxtTDSDeduct.Text);

                        string GLC = BD.GetAccTypeGL(TxtTrfAccNo.Text, Session["BRCD"].ToString());
                        string[] GL = GLC.Split('_');
                        RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GL[1].ToString(), TxtTrfAccNo.Text, txtTrfAcc.Text,
                                                    "Int received on " + TxtPRDNo.Text + "/ " + TxtAccNo.Text + " this to " + TxtTrfAccNo.Text + "/" + txtTrfAcc.Text + "", "", TxtIntCalc.Text, "2", "7", "TRANSFER", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "Inv_IntRec", "0",
                                                    "N/A", ViewState["REFID"].ToString(), "0");
                        string GLC1 = BD.GetAccTypeGL(TxtIntAccNo.Text, Session["BRCD"].ToString());
                        string[] GL1 = GLC1.Split('_');
                        RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GL1[1].ToString(), TxtIntAccNo.Text, ac,
                                                    "Int received on " + TxtPRDNo.Text + "/ " + TxtAccNo.Text + " this to " + TxtTrfAccNo.Text + "/" + txtTrfAcc.Text + "", "", TxtIntCalc.Text, "1", "7", "TRANSFER", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "Inv_IntRec", "0",
                                                    "N/A", ViewState["REFID"].ToString(), "0");
                        string GLC12 = BD.GetAccTypeGL(ViewState["INT_RECVPRD"].ToString(), Session["BRCD"].ToString());
                        string[] GL12 = GLC12.Split('_');
                        RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GL12[1].ToString(), ViewState["INT_RECVPRD"].ToString(), ViewState["INT_AC"].ToString(),
                                         "Int received on " + TxtPRDNo.Text + "/ " + TxtAccNo.Text + " this to " + TxtTrfAccNo.Text + "/" + txtTrfAcc.Text + "", "", TxtIntCalc.Text, "1", "7", "TR_INT", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "Inv_IntRec", "0",
                                                    "N/A", ViewState["REFID"].ToString(), "0");
                        RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GL12[1].ToString(), ViewState["INT_RECVPRD"].ToString(), ViewState["INT_AC"].ToString(),
                                             "Int received on " + TxtPRDNo.Text + "/ " + TxtAccNo.Text + " this to " + TxtTrfAccNo.Text + "/" + txtTrfAcc.Text + "", "", TxtIntCalc.Text, "2", "7", "TR_INT", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "Inv_IntRec", "0",
                                                        "N/A", ViewState["REFID"].ToString(), "0");

                        IC.UpdateLastIntDate(TxtPRDNo.Text, TxtAccNo.Text, txtLastDate.Text);
                        if (RES > 0)
                        {
                            lblMessage.Text = "";
                            lblMessage.Text = "Voucher Posted with No. " + SetNo + "";
                            ModalPopup.Show(this.Page);
                            FL = "Insert";//Dhanya Shetty
                            string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_Closure _" + SetNo + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            Clear();

                        }
                    }
                    else
                    {
                    }
                }
            }
        }
        catch (Exception Ex)
        {

        }
    }
    protected void Btn_Voucher_View_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable DTT = new DataTable();
            DTT.Columns.Add("ID");
            DTT.Columns.Add("SUBGLCODE");
            DTT.Columns.Add("GLNAME");
            DTT.Columns.Add("ACCNO");
            DTT.Columns.Add("NAME");
            //DTT.Columns.Add("AMT");
            DTT.Columns.Add("CR");
            DTT.Columns.Add("DR");
            DTT.Columns.Add("MID");
            DTT.Columns.Add("BRCD");

            if (rblType.SelectedValue == "1")
            {
                string PrDname = IC.GetIntAC(Session["BRCD"].ToString(), TxtPRDNo.Text, "GLNM");
                DTT.Rows.Add("1", TxtPRDNo.Text, PrDname, TxtAccNo.Text, TxtAccName.Text, TxtInAmt.Text.Replace(".00", ""), "", Session["MID"].ToString(), Session["BRCD"].ToString());
                string IntProviName = IC.GetIntAC(Session["BRCD"].ToString(), ViewState["INT_AC"].ToString(), "GLNM");
                string IntProviName1 = IC.GetIntAC(Session["BRCD"].ToString(), ViewState["INT_RECVPRD"].ToString(), "GLNM");
                if (TxtInt.Text != "" && TxtInt.Text != "0")
                {
                   
                    string GLC1 = BD.GetAccTypeGL(ViewState["INT_RECVPRD"].ToString(), Session["BRCD"].ToString());
                    string[] GL1 = GLC1.Split('_');
                    DTT.Rows.Add("2", ViewState["INT_RECVPRD"].ToString(), IntProviName1, ViewState["INT_AC"].ToString(), IntProviName, TxtInt.Text, "", Session["MID"].ToString(), Session["BRCD"].ToString());
					
                }
                if (TxtTDSDeduct.Text != "" && TxtTDSDeduct.Text != "0")
                {
                    string TDSName = IC.GetIntAC(Session["BRCD"].ToString(), ViewState["TDS_PRD"].ToString(), "GLNM");
                    string TDSName1 = IC.GetIntAC(Session["BRCD"].ToString(), ViewState["TDS_PRD"].ToString(), "GLNM");

                    string GLC = BD.GetAccTypeGL(ViewState["TDS_PRD"].ToString(), Session["BRCD"].ToString());
                    string[] GL = GLC.Split('_');
                    DTT.Rows.Add("3", ViewState["TDS_PRD"].ToString(), TDSName1, "0", TDSName, "", TxtTDSDeduct.Text, Session["MID"].ToString(), Session["BRCD"].ToString());
                }
                if (TxtIntCalc.Text != "" && TxtIntCalc.Text != "0")
                {
                    string INTAMT = TxtIntCalc.Text;
                    string IntGlName = IC.GetIntAC(Session["BRCD"].ToString(), TxtIntAccNo.Text, "GLNM");

                    string GLC3 = BD.GetAccTypeGL(TxtIntAccNo.Text, Session["BRCD"].ToString());
                    string[] GL3 = GLC3.Split('_');
                    DTT.Rows.Add("4", TxtIntAccNo.Text, IntGlName, "0", TxtIntAcName.Text, INTAMT, "", Session["MID"].ToString(), Session["BRCD"].ToString());
                }

                if (TxtTrfAccNo.Text != "" && TxtTrfAccNo.Text != "0")
                {

                    string TRFAMT = (Convert.ToInt32(Convert.ToDouble(TxtInAmt.Text.Replace(".00", "")) + Convert.ToDouble(TxtIntCalc.Text) + Convert.ToDouble(TxtInt.Text) - Convert.ToDouble(TxtTDSDeduct.Text))).ToString();
                    //Transfer Amount = (Principal + Interest Calculated + Provision) - ( Interest Paid + TDS Deduct );

                    string GLC2 = BD.GetAccTypeGL(TxtTrfAccNo.Text, Session["BRCD"].ToString());
                    string[] GL2 = GLC2.Split('_');

                    string TrfGlName = IC.GetIntAC(Session["BRCD"].ToString(), TxtTrfAccNo.Text, "GLNM");
                    DTT.Rows.Add("5", TxtTrfAccNo.Text, TrfGlName, txtTrfAcc.Text == "" ? "0" : txtTrfAcc.Text, TxtTrfAccName.Text, "", TRFAMT, Session["MID"].ToString(), Session["BRCD"].ToString());
                }

                //if (TxtIntCalc.Text != "" && TxtIntCalc.Text != "0")
                //{
                //    DTT.Rows.Add("7", ViewState["INT_RECVPRD"].ToString(), IntProviName1, ViewState["INT_AC"].ToString(), "", "", TxtIntCalc.Text.ToString().Replace(".00", ""), Session["MID"].ToString(), Session["BRCD"].ToString());
                //    DTT.Rows.Add("8", ViewState["INT_RECVPRD"].ToString(), IntProviName1, ViewState["INT_AC"].ToString(), "", TxtIntCalc.Text.ToString().Replace(".00", ""), "", Session["MID"].ToString(), Session["BRCD"].ToString());
                //}
                DTT.Rows.Add("Total", "", "", "", "", (Convert.ToInt32(TxtInAmt.Text.Replace(".00", "")) + Convert.ToInt32(TxtInt.Text) + Convert.ToDouble(TxtIntCalc.Text)), Convert.ToInt32(Convert.ToDouble(TxtInAmt.Text.Replace(".00", "")) + Convert.ToDouble(TxtIntCalc.Text) + Convert.ToDouble(TxtInt.Text)), "", "");
                GrdVoucherView.DataSource = DTT;
                GrdVoucherView.DataBind();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#VOUCHERVIEW').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
            }
            else if (rblType.SelectedValue == "2")
            {
                string PrDname = IC.GetIntAC(Session["BRCD"].ToString(), TxtPRDNo.Text, "GLNM");
                DTT.Rows.Add("1", TxtPRDNo.Text, PrDname, hdnacc.Value, TxtAccName.Text, TxtInAmt.Text.Replace(".00", ""), "", Session["MID"].ToString(), Session["BRCD"].ToString());
                DTT.Rows.Add("2", TxtPRDNo.Text, PrDname, TxtAccNo.Text, TxtAccName.Text, "", TxtInstPrinci.Text.Replace(".00", ""), Session["MID"].ToString(), Session["BRCD"].ToString());
                if (TxtIntCalc.Text != "" && TxtIntCalc.Text != "0")
                {

                    string INTAMT = (Convert.ToDouble(TxtIntCalc.Text == "" ? "0" : TxtIntCalc.Text) - (Convert.ToDouble(TxtInt.Text == "" ? "0" : TxtInt.Text))).ToString();
                    string IntGlName = IC.GetIntAC(Session["BRCD"].ToString(), TxtIntAccNo.Text, "GLNM");

                    string GLC3 = BD.GetAccTypeGL(TxtIntAccNo.Text, Session["BRCD"].ToString());
                    string[] GL3 = GLC3.Split('_');
                    DTT.Rows.Add("3", TxtIntAccNo.Text, IntGlName, "0", TxtIntAcName.Text, TxtIntCalc.Text, "", Session["MID"].ToString(), Session["BRCD"].ToString());
                }
                if (TxtTDSDeduct.Text != "" && TxtTDSDeduct.Text != "0")
                {
                    string TDSName = IC.GetIntAC(Session["BRCD"].ToString(), ViewState["TDS_PRD"].ToString(), "GLNM");
                    string TDSName1 = IC.GetIntAC(Session["BRCD"].ToString(), ViewState["TDS_PRD"].ToString(), "GLNM");

                    string GLC = BD.GetAccTypeGL(ViewState["TDS_PRD"].ToString(), Session["BRCD"].ToString());
                    string[] GL = GLC.Split('_');
                    DTT.Rows.Add("4", ViewState["TDS_PRD"].ToString(), TDSName1, "0", TDSName, TxtTDSDeduct.Text, "", TxtTDSDeduct.Text, Session["MID"].ToString(), Session["BRCD"].ToString());
                }

                string INTAMT1 = (Convert.ToDouble(TxtIntCalc.Text == "" ? "0" : TxtIntCalc.Text) - (Convert.ToDouble(TxtInt.Text == "" ? "0" : TxtInt.Text))).ToString();
                string IntProviName = IC.GetIntAC(Session["BRCD"].ToString(), ViewState["INT_AC"].ToString(), "GLNM");
                string IntProviName1 = IC.GetIntAC(Session["BRCD"].ToString(), ViewState["INT_RECVPRD"].ToString(), "GLNM");
                string GLC1 = BD.GetAccTypeGL(ViewState["INT_RECVPRD"].ToString(), Session["BRCD"].ToString());
                string[] GL1 = GLC1.Split('_');
                DTT.Rows.Add("5", ViewState["INT_RECVPRD"].ToString(), IntProviName1, ViewState["INT_AC"].ToString(), IntProviName, TxtInt.Text, "", Session["MID"].ToString(), Session["BRCD"].ToString());

                if (TxtTrfAccNo.Text != "" && TxtTrfAccNo.Text != "0")
                {
                    string TRFAMT1 = "0";

                    string GLC2 = BD.GetAccTypeGL(TxtTrfAccNo.Text, Session["BRCD"].ToString());
                    string[] GL2 = GLC2.Split('_');
                    string TrfGlName = IC.GetIntAC(Session["BRCD"].ToString(), TxtTrfAccNo.Text, "GLNM");
                    if (Convert.ToDouble(TxtInstPrinci.Text) < Convert.ToDouble(TxtTotalMaturity.Text))
                    {
                        TRFAMT1 = (Convert.ToDouble(TxtTotalMaturity.Text) - Convert.ToDouble(TxtInstPrinci.Text)).ToString();
                        DTT.Rows.Add("6", TxtTrfAccNo.Text, TrfGlName, txtTrfAcc.Text == "" ? "0" : txtTrfAcc.Text, TxtTrfAccName.Text, "", TRFAMT1, Session["MID"].ToString(), Session["BRCD"].ToString());
                    }

                    //string TRFAMT = ((Convert.ToDouble(TxtInAmt.Text == "" ? "0" : TxtInAmt.Text) + (Convert.ToDouble(TxtIntCalc.Text == "" ? "0" : TxtIntCalc.Text)) + (Convert.ToDouble(TxtInt.Text == "" ? "0" : TxtInt.Text))) - (Convert.ToDouble(TxtIntPaid.Text == "" ? "0" : TxtIntPaid.Text) + Convert.ToDouble(TxtTDSDeduct.Text == "" ? "0" : TxtTDSDeduct.Text))).ToString();
                    ////Transfer Amount = (Principal + Interest Calculated + Provision) - ( Interest Paid + TDS Deduct );

                    //DTT.Rows.Add("7", TxtTrfAccNo.Text, TrfGlName, "0", TxtTrfAccName.Text, TRFAMT, "Dr", Session["MID"].ToString(), Session["BRCD"].ToString());


                }
                //if (TxtIntCalc.Text != "" && TxtIntCalc.Text != "0")
                //{
                //    DTT.Rows.Add("7", ViewState["INT_RECVPRD"].ToString(), IntProviName1, ViewState["INT_AC"].ToString(), "", "", TxtIntCalc.Text.ToString().Replace(".00", ""), Session["MID"].ToString(), Session["BRCD"].ToString());
                //    DTT.Rows.Add("8", ViewState["INT_RECVPRD"].ToString(), IntProviName1, ViewState["INT_AC"].ToString(), "", TxtIntCalc.Text.ToString().Replace(".00", ""), "", Session["MID"].ToString(), Session["BRCD"].ToString());
                //}

                DTT.Rows.Add("Total", "", "", "", "", (Convert.ToInt32(TxtInAmt.Text.Replace(".00", "")) + Convert.ToInt32(TxtInt.Text) + Convert.ToInt32(TxtIntCalc.Text)), Convert.ToInt32(TxtInstPrinci.Text.Replace(".00", "")) + Convert.ToInt32(TxtTDSDeduct.Text) + (Convert.ToDouble(TxtTotalMaturity.Text) - Convert.ToDouble(TxtInstPrinci.Text)), "", "");

                GrdVoucherView.DataSource = DTT;
                GrdVoucherView.DataBind();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#VOUCHERVIEW').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
            }
            else
            {
                double amount = Convert.ToDouble(TxtIntCalc.Text) - Convert.ToDouble(TxtTDSDeduct.Text);
                string ac = IC.GetAccno(TxtPRDNo.Text, TxtAccNo.Text);

                string IntProviName1 = IC.GetIntAC(Session["BRCD"].ToString(), ViewState["INT_RECVPRD"].ToString(), "GLNM");
                DTT.Rows.Add("1", TxtTrfAccNo.Text, TxtTrfAccName.Text, txtTrfAcc.Text, "", "", TxtIntCalc.Text.ToString().Replace(".00", ""), Session["MID"].ToString(), Session["BRCD"].ToString());
                DTT.Rows.Add("2", TxtIntAccNo.Text, TxtIntAcName.Text, ac, "", TxtIntCalc.Text.ToString().Replace(".00", ""), "", Session["MID"].ToString(), Session["BRCD"].ToString());
                DTT.Rows.Add("3", ViewState["INT_RECVPRD"].ToString(), IntProviName1, ViewState["INT_AC"].ToString(), "", "", TxtIntCalc.Text.ToString().Replace(".00", ""), Session["MID"].ToString(), Session["BRCD"].ToString());
                DTT.Rows.Add("4", ViewState["INT_RECVPRD"].ToString(), IntProviName1, ViewState["INT_AC"].ToString(), "", TxtIntCalc.Text.ToString().Replace(".00", ""), "", Session["MID"].ToString(), Session["BRCD"].ToString());
                DTT.Rows.Add("Total", "", "", "", "", (Convert.ToInt32(TxtIntCalc.Text.ToString().Replace(".00", "")) + Convert.ToInt32(TxtIntCalc.Text.ToString().Replace(".00", ""))), Convert.ToInt32(TxtIntCalc.Text.ToString().Replace(".00", "")) + Convert.ToInt32(TxtIntCalc.Text.ToString().Replace(".00", "")), "", "");

                GrdVoucherView.DataSource = DTT;
                GrdVoucherView.DataBind();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#VOUCHERVIEW').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
            }
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string subglcode = objlink.CommandArgument;
            string[] Code = subglcode.Split('_');
            string GL = Code[0].ToString();
            string AccNo = Code[1].ToString();
            string DueDate = conn.ConvertDate(Code[2].ToString());
            DateTime Date = Convert.ToDateTime(DueDate);

            if (Date > DateTime.Now.Date)
                Ddl_Remark.SelectedValue = "1";
            else
                Ddl_Remark.SelectedValue = "2";

            string bankname = objlink.CommandName;
            divGrid.Visible = false;
            DivRecords.Visible = true;
            DivFilter.Visible = false;
            TxtPRDNo.Text = GL;
            hdnprd.Value = GL;
            TxtPRDName.Text = bankname;
            hdnprdname.Value = bankname;
            //BD.BindAccStatus(TxtAccNo, Session["BRCD"].ToString(), TxtPRDNo.Text);
            TxtAccNo.Text = AccNo;
            hdnacc.Value = AccNo;
            BindDDL();
            ShowAcc();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void btn_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmInvClosure.aspx");
    }
    protected void btngrid_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        txtInvProd.Text = "";
        txtInvAc.Text = "";
        txtInvAcName.Text = "";
        txtInvPrdNm.Text = "";
    }
    protected void btnexit_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region Clear
    public void Clear()
    {
        TxtPRDNo.Text = "";
        TxtPRDName.Text = "";
        TxtAccNo.Text = "";
        TxtAccName.Text = "";
        Ddl_Remark.SelectedValue = "0";
        TxtDate.Text = "";
        TxtMonths.Text = "";
        TxtDays.Text = "";
        Ddl_IntType.SelectedValue = "0";
        TxtROI.Text = "";
        TxtIntCalc.Text = "";
        TxtIntPaid.Text = "";
        TxtTDSDeduct.Text = "";
       // TxtintProvi.Text = "";
        TxtTDSProvi.Text = "";
        //TxtCertDate.Text = "";
        TxtCMonths.Text = "";
        TxtCDays.Text = "";
        TxtInstPrinci.Text = "";
        TxtCROI.Text = "";
        // TxtOffset.Text = "";
        TxtAsOffDate.Text = "";
       // TxtSplInstr.Text = "";
        TxtMatDate.Text = "";
        TxtMatValue.Text = "";
        // TxtMainBal.Text = "";
        // TxtCIntProvi.Text = "";
        // TxtCIntPaid.Text = "";
        // TxtCTDSProvi.Text = "";
        // TxtCTDSPaid.Text = "";
        // TxtStatus.Text = "";
        // TxtStatusName.Text = "";
        TxtTotalMaturity.Text = "";
        TxtIntAcName.Text = "";
        TxtTrfAccNo.Text = "";
        TxtTrfAccName.Text = "";
        TxtIntAccNo.Text = "";
        TxtPRDNo.Focus();

    }
    #endregion

    #region Bind Details

    public void FillDetails()
    {
        try
        {
            string TEMP = IC.GetIntAC1(Session["BRCD"].ToString(), TxtPRDNo.Text, TxtAccNo.Text);
            if (TEMP != null)
            {
                string[] INTACCS = TEMP.Split('_');
                TxtIntAccNo.Text = INTACCS[0].ToString();
                ViewState["INT_RECVPRD"] = INTACCS[1].ToString();
                ViewState["TDS_PRD"] = INTACCS[2].ToString();
                ViewState["INT_AC"] = INTACCS[3].ToString();

                TxtIntAcName.Text = IC.GetIntAC(Session["BRCD"].ToString(), TxtIntAccNo.Text, "GLNM");

                DT = IC.GetDetails(Session["BRCD"].ToString(), TxtPRDNo.Text, TxtAccNo.Text);
                if (DT.Rows.Count > 0)
                {
                    //BRCD	BankCode	SubGlCode	ReceiptNo	CertDate	Days	Months	Years	PrincipleAmt	AssOfdate	MainBalance	RateOfInt	MaturityDate	MaturityAmt	TrfAccID

                  //  TxtCertDate.Text = DT.Rows[0]["CertDate"].ToString().Replace(" 12:00:00 AM", "");
                    TxtCMonths.Text = DT.Rows[0]["Months"].ToString();
                    TxtCDays.Text = DT.Rows[0]["Days"].ToString();
                    TxtInstPrinci.Text = DT.Rows[0]["PrincipleAmt"].ToString();
                    TxtInAmt.Text = DT.Rows[0]["PrincipleAmt"].ToString();
                    TxtCROI.Text = DT.Rows[0]["RateOfInt"].ToString();
                    TxtAsOffDate.Text = DT.Rows[0]["AssOfdate"].ToString().Replace(" 12:00:00 AM", "");
                    TxtMatDate.Text = DT.Rows[0]["MaturityDate"].ToString().Replace(" 12:00:00 AM", "");
                    TxtMatValue.Text = DT.Rows[0]["MaturityAmt"].ToString();
                    TxtintProvi.Text = DT.Rows[0]["MaturityAmt"].ToString();
                    TxtIntCalc.Text = DT.Rows[0]["InterestAmt"].ToString();
                    //TxtMainBal.Text = DT.Rows[0]["MainBalance"].ToString();
                    //TxtStatus.Text = DT.Rows[0]["AccStatus"].ToString();
                    //if (TxtStatus.Text == "1")
                    //    TxtStatusName.Text = "ACTIVE";
                    //else
                    //    TxtStatusName.Text = "CLOSED";

                    // TxtTrfAccNo.Text = DT.Rows[0]["TrfAccID"].ToString();

                    string[] TD = Session["EntryDate"].ToString().Split('/');
                    string[] GLS = BD.GetAccTypeGL(TxtPRDNo.Text, Session["BRCD"].ToString()).Split('_');
                    ViewState["GL"] = GLS[1].ToString();
                    string[] GLR = BD.GetAccTypeGL(ViewState["INT_RECVPRD"].ToString(), Session["BRCD"].ToString()).Split('_');
                    ViewState["GLR"] = GLR[1].ToString();
                    //double IntPaid = Math.Abs(OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), TxtPRDNo.Text, TxtAccNo.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GL"].ToString()));
                    double IntProvision = Math.Abs(OC.GetOpenClose("CLOSING", TD[2].ToString(), TD[1].ToString(), ViewState["INT_RECVPRD"].ToString(), ViewState["INT_AC"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GLR"].ToString()));
                    //if (IntPaid != null && IntPaid > 0)
                    //    TxtIntPaid.Text = IntPaid.ToString();
                    //else
                    //    TxtIntPaid.Text = "0";
                    if (IntProvision != null && IntProvision > 0)
                        TxtInt.Text = IntProvision.ToString();
                    else
                        TxtInt.Text = "0";

                    TxtIntPaid.Text = (Convert.ToDouble(TxtIntCalc.Text) + Convert.ToDouble(TxtInt.Text)).ToString();
                    GridCerti.DataSource = DT;
                    GridCerti.DataBind();
                }
                else
                {
                    WebMsgBox.Show("Data not Found Related to Receipt A/C No. " + TxtAccNo.Text + "", this.Page);
                    TxtAccNo.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Update Credit and Receivable subglcode from Investment Master", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void ShowAcc()
    {
        try
        {
            if (rblType.SelectedValue == "1" || rblType.SelectedValue == "3")
            {
                DataTable dt = new DataTable();
                dt = BD.BindAccStatus(TxtAccNo.Text, Session["BRCD"].ToString(), TxtPRDNo.Text);
                if (dt.Rows.Count > 0)
                {
                    TxtAccName.Text = dt.Rows[0]["BankName"].ToString();
                }
                else
                {
                    WebMsgBox.Show("Invalid Account No", this.Page);
                    return;
                }
            }
            else
                TxtAccName.Text = "";
            FillDetails();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #endregion

    #region Grid Event
    protected void grdInvAccMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdInvAccMaster.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BindGrid()
    {
        try
        {
            divGrid.Visible = true;
            DataTable dt = new DataTable();
            dt = IAM.GetInvData(Session["EntryDate"].ToString(), Session["BRCD"].ToString(),txtInvProd.Text,txtInvAc.Text);
            grdInvAccMaster.DataSource = dt;
            grdInvAccMaster.DataBind();
            if (dt.Rows.Count > 0)
                Total = Convert.ToDouble(dt.Compute("SUM(ClosingBal)", string.Empty));
            else
                Total = 0;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void grdInvAccMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("total");
            lbl.Text = Total.ToString();
        }
    }
    #endregion

    protected void Btn_Provision_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtIntAccNo.Text == "" )
            {
                WebMsgBox.Show("Please Update Int A/C No from Investment Master", this.Page);
            }
            else if (ViewState["INT_AC"].ToString() == "" || ViewState["INT_AC"].ToString() == null)
            {
                WebMsgBox.Show("Please Update Received A/C No from Investment Master", this.Page);
            }
            else
            {
                    if (TxtPRDNo.Text != "" && TxtAccNo.Text != "" && TxtTrfAccNo.Text != "")
                    {
                        string REFERENCEID = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                        ViewState["REFID"] = (Convert.ToInt32(REFERENCEID) + 1).ToString();
                        double AMT = 0;
                        int RES = 0;
                        string SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();

                        AMT = Convert.ToInt32(Convert.ToDouble(TxtInt.Text.Replace(".00", "")));

                        string RecAC = IC.GetRecAC(Session["BRCD"].ToString(),txtInvProd.Text, txtInvAc.Text);
                        string RecGL = IC.GetRecGL(Session["BRCD"].ToString(),txtInvProd.Text, txtInvAc.Text);
    
                        //Debit to TRF A/C no under PRDNO with AMOUNT=PRINCIpal
                        RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), RecGL, RecGL, RecAC == "" ? "0" : RecAC,
                                              "TRF From " + txtInvProd.Text + "/" + txtInvAc.Text + " ", "", Convert.ToString(Convert.ToDecimal(AMT)), "2", "7", "TR", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "INV-CLO", "0",
                                              "N/A", ViewState["REFID"].ToString(), "0");
                        //Credit to A/C no under PRDNO with AMOUNT=PRINCIpal
                        RES = AA.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), TxtTrfAccNo.Text, TxtTrfAccNo.Text, RecAC,
                                            "TRF To " + RecGL + "/" + RecAC + " ", "", Convert.ToString(Convert.ToDecimal(AMT)), "1", "7", "TR", SetNo, "0", "1900-01-01", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "INV-CLO", "0",
                                            "N/A", ViewState["REFID"].ToString(), "0");

                        if (RES > 0)
                        {
                            lblMessage.Text = "";
                            lblMessage.Text = "Voucher Posted with No. " + SetNo + "";
                            ModalPopup.Show(this.Page);
                            FL = "Insert";//Dhanya Shetty
                            string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_Closure _" + SetNo + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            Clear();

                        }
                    }
                    else
                    {
                        WebMsgBox.Show("Enter Valid Data....!", this.Page);
                        TxtPRDNo.Focus();
                    }
                }
            }
        catch (Exception Ex)
        {

        }
    }
}