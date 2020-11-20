using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmCashDenomRpt : System.Web.UI.Page
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
            //added by ankita 07/10/2017 to make user frndly
            txtFrBrch.Text = Session["BRCD"].ToString();
            txtTobrch.Text = Session["BRCD"].ToString();
            txtAsOnDate.Text = Session["EntryDate"].ToString();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FL = "Insert";//ankita 14/09/2017
        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CashDenom_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

        string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&FBC=" + txtFrBrch.Text + "&TBC=" + txtTobrch.Text + "&FD=" + txtAsOnDate.Text + "&rptname=RPTCashDenom.rdlc";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
    }
}