using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmDormantAcList : System.Web.UI.Page
{
    DbConnection Conn = new DbConnection();
    ClsReport LC = new ClsReport();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("FrmLogin.aspx");
        }
        //added by ankita 07/10/2017 to make user frndly
        if (!IsPostBack)
        {
            TxtBrID.Text = Session["BRCD"].ToString();
        }
        AutoAccname.ContextKey = Session["BRCD"].ToString();
    }
    protected void TxtATName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtATName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtATName.Text = custnob[0].ToString();
                TxtAccType.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] AC = LC.Getaccno(TxtAccType.Text, Session["BRCD"].ToString(), custnob[2].ToString()).Split('-');
                ViewState["ACCNO"] = AC[0].ToString();
                ViewState["GLCODE"] = AC[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtAccType.Text;
                TxtFDate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAccType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;
            AC1 = LC.Getaccno(TxtAccType.Text, Session["BRCD"].ToString(), "");

            if (AC1 != null)
            {
                string[] AC = AC1.Split('-'); ;
                ViewState["ACCNO"] = AC[0].ToString();
                ViewState["GLCODE"] = AC[1].ToString();
                TxtATName.Text = AC[2].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtAccType.Text + "_" + ViewState["GLCODE"].ToString();
                TxtFDate.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter valid Product code!.....", this.Page);
                TxtAccType.Text = "";
                TxtATName.Text = "";
                TxtAccType.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ProdTrans_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string fmonth, fyear;

            string[] fdate = TxtFDate.Text.Split('/');
            fmonth = fdate[1].ToString();
            fyear = fdate[2].ToString();
            if (rbtnRptType.SelectedValue == "1")
            {
                string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&FDate=" + TxtFDate.Text + "&ProdCode=" + TxtAccType.Text + "&IntRate=" + TxtIntrate.Text + "&Amount=" + TxtAmt.Text + "&rptname=RptDormantAcList.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (rbtnRptType.SelectedValue == "2")
            {
                string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&FDate=" + TxtFDate.Text + "&ProdCode=" + TxtAccType.Text + "&IntRate=" + TxtDueDT.Text + "&Amount=" + TxtAmt.Text + "&rptname=RptDormantDueAcList.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void rbtnRptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnRptType.SelectedValue == "2")
        {
            divtype.Visible = true;
            divtype2.Visible = false;
        }
        else
        {
            divtype.Visible = false;
            divtype2.Visible = true;
        }
    }
}