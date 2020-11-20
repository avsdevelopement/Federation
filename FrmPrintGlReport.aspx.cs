using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmPrintGlReport : System.Web.UI.Page
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
    protected void Btnprint_Click(object sender, EventArgs e)
    {
        try 
        {
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "PrintGl_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        //string redirectURL = "FrmRView.aspx?FD=" + txtfromdate.Text + "&TD=" + txttodate.Text + "&BRCD=" + Session["BRCD"].ToString() + "&rptname=RptDepositRep.rdlc";
        string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&rptname=RptGLreport.rdlc";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}