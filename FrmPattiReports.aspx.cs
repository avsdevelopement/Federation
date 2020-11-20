using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmPattiReports : System.Web.UI.Page
{
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
                }

              TxtBrID.Text = Session["BRCD"].ToString();
              TxtBankCD.Text = "0000";
              TxtFDate.Text = Session["EntryDate"].ToString();
              TxtTDate.Text = Session["EntryDate"].ToString();
              BtnPrint.Focus();
             }
          }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void RbtnInward_CheckedChanged(object sender, EventArgs e)
    {
        RbtnInward.Checked = true;
        RbtnOutward.Checked = false;
    }
    protected void RbtnOutward_CheckedChanged(object sender, EventArgs e)
    {
        RbtnOutward.Checked = true;
        RbtnInward.Checked = false;
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtBrID.Text != "" && TxtBankCD.Text != "" && TxtFDate.Text != "" && TxtTDate.Text != "")
            {
                if(RbtnInward.Checked)
                {
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "InwardClearing_PattiRpt _" + TxtFDate.Text + "_" + TxtTDate.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&BankCode=" + TxtBankCD.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&ADate=" + Session["EntryDate"].ToString() + "&rptname=RptIWReturnPatti.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                else if (RbtnOutward.Checked)
                {
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "OutwardClearing_PattiRpt _" + TxtFDate.Text + "_" + TxtTDate.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&BankCode=" + TxtBankCD.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&ADate=" + Session["EntryDate"].ToString() + "&rptname=RptOutwardReturnPatti.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
    
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    public void ClearData()
    {
        TxtBrID.Text="";
        TxtBankCD.Text="";
        TxtFDate.Text="";
        TxtTDate.Text = "";
    }
}