using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmDayBook_TZMP : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsCommon com = new ClsCommon();
    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            string allow = com.ChkDayBook(Session["Brcd"].ToString());
            if (allow == "Y")
            {
                DepositDayBook.Visible = true;
                DPDayBookOP.Visible = true;
            }
            else
            {
                DepositDayBook.Visible = false;
                DPDayBookOP.Visible = false;
            }

            TxtBrID.Text = Session["BRCD"].ToString();
            TxtTDate.Text = Session["EntryDate"].ToString();
        }
    }
    protected void DayBook_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DayBk_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            string Dex1 = "", Iex2 = "";
            if (CHK_SKIP_DAILY.Checked == true && CHK_SKIP_INT.Checked == true)
            {
                Dex1 = "Y";
                Iex2 = "Y";
            }
            else if (CHK_SKIP_DAILY.Checked == false && CHK_SKIP_INT.Checked == false)
            {
                Dex1 = "N";
                Iex2 = "N";
            }
            else if (CHK_SKIP_DAILY.Checked == true && CHK_SKIP_INT.Checked == false)
            {
                Dex1 = "Y";
                Iex2 = "N";
            }
            else if (CHK_SKIP_DAILY.Checked == false && CHK_SKIP_INT.Checked == true)
            {
                Dex1 = "N";
                Iex2 = "Y";
            }
            if (Session["BNKCDE"].ToString() == "1008")
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&DEX=" + Dex1 + "&IEX=" + Iex2 + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&rptname=RptDayBookReg_TZMP.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&DEX=" + Dex1 + "&IEX=" + Iex2 + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&rptname=RptDayBookReg_FromTo.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DepositDayBook_Click(object sender, EventArgs e)
    {
        try
        {
            string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&rptname=RptDayBookReg_Renewal.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DPDayBookOP_Click(object sender, EventArgs e)
    {
        try
        {
            string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&rptname=RptDayBookDP_Register.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}