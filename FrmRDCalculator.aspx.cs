using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmRDCalculator : System.Web.UI.Page
{
    ClsRDCalculator RD = new ClsRDCalculator();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("FrmLogin.aspx");
        }

        TxtTotalMat.Enabled = false;
        TxtTotalIntAmt.Enabled = false;
        RdbCompoundQ.Focus();
    }
    protected void ClearData()
    {
        TxtMonthAmt.Text = "";
        TxtPeriodMonths.Text = "";
        TxtROI.Text = "";
        TxtTotalIntAmt.Text = "";
        TxtTotalMat.Text = "";
        
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            double Total,TotalInt;
            int n = 0;
            if (RdbCompoundQ.Checked == true)
                n = 4;
            else if (RdbCompoundM.Checked == true)
                n = 12;
            else if (RdbCompoundHY.Checked == true)
                n = 2;


            Total = RD.Calc_RD(Convert.ToDouble(TxtMonthAmt.Text), Convert.ToDouble(TxtROI.Text),n,Convert.ToDouble(TxtPeriodMonths.Text));
            Total = Math.Round(Total, 2);
            TxtTotalMat.Text = Total.ToString();

            TotalInt = RD.Calc_IntRD(Convert.ToDouble(TxtMonthAmt.Text),Total,Convert.ToDouble(TxtPeriodMonths.Text));
            TotalInt = Math.Round(TotalInt, 2);
            TxtTotalIntAmt.Text = TotalInt.ToString();
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "RDcalculator _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
    }
    protected void ClearAll_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    protected void Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
}