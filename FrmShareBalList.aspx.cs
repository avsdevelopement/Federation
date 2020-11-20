using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmShareBalList : System.Web.UI.Page
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
            //added by ankita 07/10/2017 to make user frndly
            
            TxtFDate.Text = Session["EntryDate"].ToString();
            TxtBrID.Text = Session["BRCD"].ToString();
        }

    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ShareBalList_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
             string redirectURL = "FrmRView.aspx?FDate=" + TxtFDate.Text + "&UserName=" + Session["UserName"].ToString() + "&BRCD=" + TxtBrID.Text + "&rptname=RptShareBalList.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        TxtBrID.Text = "";
        TxtFDate.Text = "";
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {

    }
}