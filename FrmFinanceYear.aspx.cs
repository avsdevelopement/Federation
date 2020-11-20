using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmFinanceYear : System.Web.UI.Page
{
    ClsFinanceYear FY = new ClsFinanceYear();
    ClsBindDropdown BD = new ClsBindDropdown();
    int Res = 0;
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    string STR = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            Res = FY.GetFinaceYearTable(GridFinance);
            // Lbl_Finan.Text = "Finacial Year " + Session["EntryDate"].ToString() + "";
            //added by ankita 07/10/2017 to make user frndly
            TxtFDate.Text = Session["EntryDate"].ToString();
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string TYPE = "";
            if (Rdb_EntryType.SelectedValue == "1")
            {
                TYPE = "1000";
            }
            if (Rdb_EntryType.SelectedValue == "2")
            {
                TYPE = "100000";
            }
            if (Rdb_EntryType.SelectedValue == "3")
            {
                TYPE = "10000000";
            }
            //  Res = FY.GetFinaceYearTable(GridFinance);
            Res = FY.GetDepLoanTable(GridDL, TxtFDate.Text, TYPE);
            Lbl_DepLon.Text = "Deposit & Loan From   [" + TxtFDate.Text.Replace("/", "-") + "] ";
            // Lbl_Finan.Text = "Finacial Year " + Session["EntryDate"].ToString() + "";
            FL = "Insert";//ankita 14/09/2017
            string Res1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DailyPosition_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnBack_Click (object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string TYPE = "";
            if (Rdb_EntryType.SelectedValue == "1")
            {
                TYPE = "1000";
            }
            if (Rdb_EntryType.SelectedValue == "2")
            {
                TYPE = "100000";
            }
            if (Rdb_EntryType.SelectedValue == "3")
            {
                TYPE = "10000000";
            }
            FL = "Insert";//ankita 14/09/2017
            string Res1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DailyPosition_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            string redirectURL = "FrmRView.aspx?AsOnDate=" + TxtFDate.Text + "&Type=" + TYPE + "&rptname=RptBrWiseDepositLoanList.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}