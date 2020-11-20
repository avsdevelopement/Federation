using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmDayClosedActivity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Btnprint_Click(object sender, EventArgs e)
    {
        try
        {
            //string redirectURL = "FrmRView.aspx?FD=" + txtfromdate.Text + "&TD=" + txttodate.Text + "&BRCD=" + Session["BRCD"].ToString() + "&rptname=RptDepositRep.rdlc";
            string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&rptname=RptDayOpenClosedreport.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}