using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmCustMobileDT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string redirectURL = "FrmRView.aspx?FBrcd=" + Txtfrmbrcd.Text + "&TBrcd=" + Txttobrcd.Text + "&FromDate=" + TxtFDate.Text + "&ToDate=" + TxtTDate.Text + "&rptname=RptCustMobile.rdlc" + "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {

    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {

    }
}