using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmTDSCustDetails : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("FrmLogin.aspx");
        }
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//Dhanya Shetty
            string Result = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "TDS_Details_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&FBRCD=" + Txtfrmbrcd.Text + "&FCutNo=" + TxtFromCust.Text + "&TCutNo=" + TxtToCust.Text + "&rptname=RptTDSDetails.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
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
    public void ClearData()
    {
        Txtfrmbrcd.Text = "";
        TxtFDate.Text = "";
        TxtTDate.Text = "";
        TxtFromCust.Text = "";
        TxtToCust.Text = "";
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void Txtfrmbrcd_TextChanged(object sender, EventArgs e)
    {
        TxtFDate.Focus();
    }
}