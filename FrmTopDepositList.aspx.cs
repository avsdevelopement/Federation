using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmTopDepositList : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("FrmLogin.aspx");
        }
        if (!IsPostBack)
        {
            TxtBrID.Text = Session["BRCD"].ToString();
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "TopDepoList_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&SubGlCode=" + txtsglcode.Text + "&AsOnDate=" + TxtAsonDate.Text + "&Count=" + tctCount.Text + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptTopDepositList.rdlc" + "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}