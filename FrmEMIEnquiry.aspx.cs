using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmEMIEnquiry : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsEMIEnquiry LE = new ClsEMIEnquiry();
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
                    return;
                }
                TxtLoanAmt.Focus();
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
            BindGrid();
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Emicalculator_Emienquiry _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
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
            LE.GetLoanEnquiry(GrdLoanInfo, Convert.ToDouble(TxtLoanAmt.Text), Convert.ToDouble(TxtAnnualINT.Text), Convert.ToDouble(TxtPeriodY.Text), TxtSDate.Text == "" ? Session["EntryDate"].ToString() : TxtSDate.Text, DDl_PeriodType.SelectedValue.ToString());
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

    protected void TxtRepayAmt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string PERIOD = LE.GetPeriod("GETPERIOD", TxtLoanAmt.Text, TxtAnnualINT.Text, TxtRepayAmt.Text, TxtSDate.Text == "" ? Session["EntryDate"].ToString() : TxtSDate.Text);
            if (PERIOD != null)
            {
                TxtPeriodY.Text = PERIOD;
                BindGrid();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string SDT = TxtSDate.Text == "" ? Session["EntryDate"].ToString() : TxtSDate.Text;

            string redirectURL = "FrmReportViewer.aspx?LoanAmt=" + TxtLoanAmt.Text + "&Period=" + TxtPeriodY.Text + "&Rate=" + TxtAnnualINT.Text + "&RepayAmount=" + TxtRepayAmt.Text + "&StartDate=" + SDT + "&PeriodType=" + DDl_PeriodType.SelectedValue.ToString() + "&rptname=RptEmiChart.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DDl_PeriodType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtPeriodY.Text = "";
            TxtPeriodY.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}