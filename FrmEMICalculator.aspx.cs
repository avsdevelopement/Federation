using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmEMICalculator : System.Web.UI.Page
{
    ClsEMICalculator EMI = new ClsEMICalculator();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            TxtTotalEMIAmt.Enabled = false;
            TxtReypayPeriod.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void ClearData()
    {
        TxtReypayPeriod.Text = "";
        TxtROI.Text = "";
        TxtLoanAmt.Text = "";
        TxtTotalEMIAmt.Text = "";

    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            double Total;
            Total = EMI.Calc_EMI(Convert.ToDouble(TxtReypayPeriod.Text), Convert.ToDouble(TxtROI.Text), Convert.ToDouble(TxtLoanAmt.Text));
            Total = Math.Round(Total, 2);
            TxtTotalEMIAmt.Text = Total.ToString();
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Emicalculator _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void ClearAll_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    protected void Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx",true);
    }
}