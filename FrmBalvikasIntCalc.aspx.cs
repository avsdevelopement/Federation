using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmBalvikasIntCalc : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsBalvikasIntCalc BVC = new ClsBalvikasIntCalc();
    ClsAccountSTS AST = new ClsAccountSTS();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsMISTransfer MT = new ClsMISTransfer();
    ClsEncryptValue Ecry = new ClsEncryptValue();
    DbConnection Conn = new DbConnection();

    string FL = "";
    string Skip = "";
    int ResInt = 0;
    string ResStr = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }


            autoglname.ContextKey = Session["BRCD"].ToString();
            TxtToDate.Text = Session["EntryDate"].ToString();
            TxtBRCD.Text = Session["BRCD"].ToString();
            TxtBRCDName.Text = AST.GetBranchName(TxtBRCD.Text);
            TxtFromDate.Focus();

        }
        ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
    }
    public void Clear()
    {

        TxtBRCD.Text = "";
        TxtBRCDName.Text = "";
        TxtFPRD.Text = "";
        TxtFPRDName.Text = "";
        TxtTPRD.Text = "";
        TXtTPRDName.Text = "";
        TxtFAcc.Text = "";
        TxtTAcc.Text = "";
        TxtFAccName.Text = "";
        TxtTAccName.Text = "";
        TxtBRCD.Focus();

    }
    protected void TxtFPRD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //// string[] TD = BD.GetLoanGL(TxtTPRD.Text, Session["BRCD"].ToString()).Split('_');
            ////added by ankita to solve 304 subgl problem 29/06/2017
            if (TxtTPRD.Text != "" && (Convert.ToInt32(TxtFPRD.Text) > Convert.ToInt32(TxtTPRD.Text)))
            {
                WebMsgBox.Show("Invalid FROM and TO Product code....!", this.Page);
                TxtFPRD.Text = "";
                TxtTPRD.Text = "";
                TxtFPRD.Focus();
                return;
            }
            string TD1 = BD.GetDepoGLRD(TxtFPRD.Text, Session["BRCD"].ToString());
            if (TD1 != null)
            {

                string[] TD = TD1.Split('_');
                if (TD.Length > 1)
                {

                }
                TxtFPRDName.Text = TD[0].ToString();

                string CAT = MT.GetDepositCat(Session["BRCD"].ToString(), TxtFPRD.Text, "MISTRF");
                ViewState["CAT"] = CAT.ToString();

                TxtTPRD.Focus();
            }
            else
            {
                WebMsgBox.Show("Invalid Product code...!", this.Page);
                TxtFPRD.Text = "";
                TxtFPRD.Focus();
                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTPRD_TextChanged(object sender, EventArgs e)
    {
        try
        {

            if ((Convert.ToInt32(TxtFPRD.Text) > Convert.ToInt32(TxtTPRD.Text)))
            {
                WebMsgBox.Show("Invalid FROM and TO Product code....!", this.Page);
                TxtTPRD.Text = "";
                TxtTPRD.Focus();
                return;
            }
            string TD1 = BD.GetDepoGLRD(TxtTPRD.Text, Session["BRCD"].ToString());
            if (TD1 != null)
            {
                string[] TD = TD1.Split('_');

                if (TD.Length > 1)
                {

                }
                TXtTPRDName.Text = TD[0].ToString();
                TxtFAcc.Focus();
            }
            else
            {
                WebMsgBox.Show("Invalid Product code...!", this.Page);
                TxtTPRD.Text = "";
                TxtTPRD.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtBRCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBRCD.Text != "")
            {
                string bname = AST.GetBranchName(TxtBRCD.Text);
                if (bname != null)
                {
                    TxtBRCDName.Text = bname;
                    TxtFPRD.Focus();

                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    TxtBRCD.Text = "";
                    TxtBRCD.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                TxtBRCD.Text = "";
                TxtBRCD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFAcc_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtTAcc.Text != "" && (Convert.ToInt32(TxtFAcc.Text) > Convert.ToInt32(TxtTAcc.Text)))
            {
                WebMsgBox.Show("Invalid FROM and TO Account Number....!", this.Page);
                TxtFAcc.Text = "";
                TxtTAcc.Text = "";
                TxtFAccName.Text = "";
                TxtTAccName.Text = "";
                TxtFAcc.Focus();
                return;
            }
            TxtFAccName.Text = BD.AccName(TxtFAcc.Text, TxtFPRD.Text, TxtTPRD.Text, Session["BRCD"].ToString());
            TxtTAcc.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTAcc_TextChanged(object sender, EventArgs e)
    {

        try
        {
            if (Convert.ToInt32(TxtFAcc.Text) > Convert.ToInt32(TxtTAcc.Text))
            {
                WebMsgBox.Show("Invalid FROM and TO Account Number....!", this.Page);
                TxtFAcc.Text = "";
                TxtTAcc.Text = "";
                TxtFAccName.Text = "";
                TxtTAccName.Text = "";
                TxtFAcc.Focus();
                return;
            }
            TxtTAccName.Text = BD.AccName(TxtTAcc.Text, TxtFPRD.Text, TxtTPRD.Text, Session["BRCD"].ToString());
            BtnCalculate.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnCalculate_Click(object sender, EventArgs e)
    {
        try
        {
            ResInt = BVC.Calc_Balvikas("CALC", TxtBRCD.Text, TxtFPRD.Text, TxtTPRD.Text, TxtFAcc.Text, TxtTAcc.Text, TxtFromDate.Text, TxtToDate.Text, Session["MID"].ToString(), Session["EntryDate"].ToString());
            if (ResInt > 0)
            {
                WebMsgBox.Show("Calculation completed , run Report....!", this.Page);
            }
            else
            {
                WebMsgBox.Show("Calculation failed....!", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_Recalculate_Click(object sender, EventArgs e)
    {
        try
        {
            ResInt = BVC.ReCalc_Balvikas("RECALC", Session["MID"].ToString());
            if (ResInt > 0)
            {
                WebMsgBox.Show("Calculation removed for " + Session["LOGINCODE"].ToString() + "....!", this.Page);
            }
            else
            {
                WebMsgBox.Show(" No Data availble ....!", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_ClearAll_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void BtnReportCalc_Click(object sender, EventArgs e)
    {
        try
        {
            string redirectURL = "FrmReportViewer.aspx?FL=REPORT&MID=" + Session["MID"].ToString() + "&BRCD=" + TxtBRCD.Text.Trim().ToString() + "&FPRD=" + TxtFPRD.Text.Trim().ToString() + "&TPRD=" + TxtTPRD.Text.Trim().ToString() + "&FDATE=" + TxtFromDate.Text.Trim().ToString() + "&TDATE=" + TxtToDate.Text.Trim().ToString() + "&FACCNO=" + TxtFAcc.Text.ToString() + "&rptname=BalvikasCalcReport";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnReportTally_Click(object sender, EventArgs e)
    {
        try
        {
            string redirectURL = "FrmReportViewer.aspx?FL=BOOKTALLY&MID=" + Session["MID"].ToString() + "&BRCD=" + TxtBRCD.Text.Trim().ToString() + "&FPRD=" + TxtFPRD.Text.Trim().ToString() + "&TPRD=" + TxtTPRD.Text.Trim().ToString() +"&FDATE=" + TxtFromDate.Text.Trim().ToString() + "&TDATE=" + TxtToDate.Text.Trim().ToString() + "&FACCNO=" + TxtFAcc.Text.ToString() + "&rptname=BalvikasTally";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void ApplyEntry_Click(object sender, EventArgs e)
    {
        try
        {
            ResStr = BVC.PostEntry("APPLY", TxtBRCD.Text, TxtFPRD.Text, TxtTPRD.Text, TxtFAcc.Text, TxtTAcc.Text, TxtFromDate.Text, TxtToDate.Text, Session["EntryDate"].ToString(), Session["MID"].ToString(), "SYS");
            if (ResStr != null)
            {
                WebMsgBox.Show("" + ResStr, this.Page);
                Clear();
            }
            else
            {
                WebMsgBox.Show("Posting Failed.....!", this.Page);

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtToDate.Text != "")
            {
                if (Convert.ToDateTime(Conn.ConvertDate(TxtFromDate.Text)) > Convert.ToDateTime(Conn.ConvertDate(TxtToDate.Text)))
                {
                    WebMsgBox.Show("From Date cannot be greater than To Date....!", this.Page);
                    TxtFromDate.Text = "";
                    TxtFromDate.Focus();
                }
                else
                {
                    TxtToDate.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtToDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFromDate.Text != "")
            {
                if (Convert.ToDateTime(Conn.ConvertDate(TxtToDate.Text)) < Convert.ToDateTime(Conn.ConvertDate(TxtFromDate.Text)))
                {
                    WebMsgBox.Show("To Date cannot be greater than From Date....!", this.Page);
                    TxtToDate.Text = "";
                    TxtToDate.Focus();
                }
                else
                {
                    TxtBRCD.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}