using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmExcessCasRep : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TxtFDate.Focus();
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            //added by ankita 07/10/2017 to make user frndly 
            TxtFDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
            TxtTDate.Text = Session["EntryDate"].ToString();
        }
    }
    protected void Exit_Click(object sender, EventArgs e)
    {
        //  Response.Redirect("FrmBlank.aspx");
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }

    protected void ClearData()
    {
        TxtFDate.Text = "";
        TxtFBankcode.Text="";
        TxtFBankname.Text = "";
        TxtTDate.Text = "";
        TxtTBankcode.Text = "";
        TxtTBankname.Text = "";
        TxtFDate.Focus();
    }

    protected void BtnReport_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ExcessCash_Rpt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            string redirecturl = "FrmRView.aspx?FDATE="+TxtFDate.Text+"&TDATE="+TxtTDate.Text+"&TBRCD="+TxtTBankcode.Text+"&FBRCD="+TxtFBankcode.Text+"&USERNAME="+Session["UserName"].ToString()+"&EDT="+Session["EntryDate"].ToString()+"&rptname=RptExcessCashHold.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirecturl + "','_blank')", true);
        }
        catch(Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}