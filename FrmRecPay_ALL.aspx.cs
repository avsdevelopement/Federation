using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmRecPay_ALL : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    ClsBindDropdown BD = new ClsBindDropdown();
    scustom customcs = new scustom();
    ClsBindDropdown DD = new ClsBindDropdown();
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

                Txtfrmbrcd.Focus();
                TxtFDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
                TxtTDate.Text = Session["EntryDate"].ToString();
                Txtfrmbrcd.Text = Session["BRCD"].ToString();
                Txttobrcd.Text = Session["BRCD"].ToString();
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
            if (Rdeatils.SelectedValue == "1")
            {
                FL = "Insert";//ankita 15/09/2017
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SroRecovry_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                string redirectURL = "FrmRView.aspx?FBRCD=" + Txtfrmbrcd.Text + "&TBRCD=" + Txttobrcd.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&rptname=RptRecPayCLBal_ALL.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (Rdeatils.SelectedValue == "2")
            {
                FL = "Insert";//ankita 15/09/2017
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SroRecovry_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                string redirectURL = "FrmRView.aspx?FBRCD=" + Txtfrmbrcd.Text + "&TBRCD=" + Txttobrcd.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&rptname=RptRecPayCLBal_ALL.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
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
    protected void Txtfrmbrcd_TextChanged(object sender, EventArgs e)
    {
        Txttobrcd.Focus();
    }
    protected void Txttobrcd_TextChanged(object sender, EventArgs e)
    {

    }
}