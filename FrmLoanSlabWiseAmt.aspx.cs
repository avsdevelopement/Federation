using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmLoanSlabWiseAmt : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    ClsBindDropdown BD = new ClsBindDropdown();
    scustom customcs = new scustom();
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
                //added by ankita 07/10/2017 to make user frndly
                TxtAsonDate.Text = Session["EntryDate"].ToString();
                Txtfrmbrcd.Text = Session["BRCD"].ToString();
                Txtfrmbrcd.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DepClassification_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            if (rbtnRptType.SelectedValue == "D")
            {
                string redirectURL = "FrmRView.aspx?FromBrcd=" + Txtfrmbrcd.Text + "&AsOnDate=" + TxtAsonDate.Text + "&rptname=RptLoansSlabWiseDT.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (rbtnRptType.SelectedValue == "S")
            {
                string redirectURL = "FrmRView.aspx?FromBrcd=" + Txtfrmbrcd.Text + "&AsOnDate=" + TxtAsonDate.Text + "&rptname=RptLoansSlabWise.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void Txtfrmbrcd_TextChanged(object sender, EventArgs e)
    {

    }
}