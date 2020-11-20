using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
public partial class FrmFDCalculator : System.Web.UI.Page
{
    ClsFDCalculator FD = new ClsFDCalculator();
    DbConnection Conn = new DbConnection();
    double TotAMT;
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    double TotInTAMT;
    double EffectY;
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
                TxtTotalIntAmt.Enabled = false;
                TxtTotalMatAmt.Enabled = false;
                TxtEffectYeild.Enabled = false;
                TxtEffectDate.Text = Session["EntryDate"].ToString();
                TxtPrinciAmount.Focus();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }

    protected void ClearData()
    {
        TxtPrinciAmount.Text = "";
        TxtROI.Text = "";
        TxtPeriod.Text = "";
        TxtTotalIntAmt.Text = "";
        TxtTotalMatAmt.Text = "";
        TxtEffectYeild.Text = "";
        TxtPrinciAmount.Focus();
        //RdbCompoundHY.Checked = false;
        //RdbCompoundQ.Checked = false;
        //RdbCompoundM.Checked = false;
        //RdbCompoundQ.Focus();
    }
    //protected void Submit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        double T = Convert.ToDouble(TxtPeriod.Text);
    //        double Nofyear = Convert.ToDouble((T / 12));
    //        int n = 0;
    //        if (RdbCompoundQ.Checked == true)
    //            n = 4;
    //        else if (RdbCompoundM.Checked == true)
    //            n = 12;
    //        else if (RdbCompoundHY.Checked == true)
    //            n = 2;


    //        TotAMT = FD.Calc_TotalMA(Convert.ToDouble(TxtPrinciAmount.Text), Convert.ToDouble(TxtROI.Text), n, Nofyear);
    //        TotAMT = Math.Round(TotAMT, 2);
    //        TxtTotalMatAmt.Text = TotAMT.ToString();


    //        TotInTAMT = FD.Calc_IntReceived(TotAMT, Convert.ToDouble(TxtPrinciAmount.Text));
    //        TotInTAMT = Math.Round(TotInTAMT, 2);
    //        TxtTotalIntAmt.Text = TotInTAMT.ToString();


    //        EffectY = FD.Calc_EfectYeild(TotInTAMT, Convert.ToInt16(Nofyear));
    //        EffectY = Math.Round(EffectY, 2);
    //        TxtEffectYeild.Text = EffectY.ToString();
    //        FL = "Insert";//Dhanya Shetty
    //        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "FDcalculator _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
    protected void ClearAll_Click(object sender, EventArgs e)
    {
        ClearData();
        //RdbCompoundQ.Focus();
    }
    protected void Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);

    }
    protected void Btn_Details_Click(object sender, EventArgs e)
    {
        try
        {
            int RES ;
            if (DdlDuartion.SelectedValue == "M")
            {
                RES = FD.GetGridData(Grd_Details, "ALL", TxtPrinciAmount.Text, TxtPeriod.Text, TxtROI.Text, "M");
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "FDcalculator _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
            else
            {
                RES = FD.GetGridData(Grd_Details, "FDS", TxtPrinciAmount.Text, TxtPeriod.Text, TxtROI.Text, "D");
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "FDcalculator _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtEffectDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CalcMatDate();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void CalcMatDate()
    {
        try
        {
            string EffDate, MatDate;
            if (TxtPeriod.Text != "")
            {
                if (DdlDuartion.SelectedValue == "M")
                {
                    EffDate = Conn.ConvertDate(TxtEffectDate.Text);

                    MatDate = Conn.AddMonthDay(TxtEffectDate.Text, TxtPeriod.Text, "M");
                    TxtMaturityDate.Text = MatDate.ToString();
                }
                else
                {
                    EffDate = Conn.ConvertDate(TxtEffectDate.Text);

                    MatDate = Conn.AddMonthDay(TxtEffectDate.Text, TxtPeriod.Text, "D");
                    TxtMaturityDate.Text = MatDate.ToString();
                }

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtPeriod_TextChanged(object sender, EventArgs e)
    {
        try
        {
            CalcMatDate();
            TxtEffectDate.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}