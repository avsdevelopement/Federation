using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmRDayBook : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
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
            string Dex1="",Iex2="";
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
            if (Rdeatils.SelectedValue == "1")
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&DEX=" + Dex1 + "&IEX=" + Iex2 + "&FDate=" + TxtTDate.Text + "&rptname=RptDayBookDetails.rdlc";
                   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (Rdeatils.SelectedValue == "2")
            {
                if (Session["BNKCDE"].ToString() == "100")
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&DEX=" + Dex1 + "&IEX=" + Iex2 + "&FDate=" + TxtTDate.Text + "&rptname=RptDayBook_PEN.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                else if (Session["BNKCDE"].ToString() == "423800")
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&DEX=" + Dex1 + "&IEX=" + Iex2 + "&FDate=" + TxtTDate.Text + "&rptname=RptDayBookReg_Mamco.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                else
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&DEX=" + Dex1 + "&IEX=" + Iex2 + "&FDate=" + TxtTDate.Text + "&rptname=RptDayBook.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
            
            if (Rdeatils.SelectedValue == "3")
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&DEX=" + Dex1 + "&IEX=" + Iex2 + "&FDate=" + TxtTDate.Text + "&rptname=RptDayBookRegistrerDetailsSetWise.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (Rdeatils.SelectedValue == "4")
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&DEX=" + Dex1 + "&IEX=" + Iex2 + "&FDate=" + TxtTDate.Text + "&rptname=RptDayBook_SHIV.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (Rdeatils.SelectedValue == "5")
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&DEX=" + Dex1 + "&IEX=" + Iex2 + "&FDate=" + TxtTDate.Text + "&rptname=RptDayBook_ALLDetails.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}