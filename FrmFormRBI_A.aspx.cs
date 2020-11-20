using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmFormRBI_A : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCTRReport CTR = new ClsCTRReport();
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
            TxtAsonDate.Text = Session["EntryDate"].ToString();
            TxtFDate.Text = Session["EntryDate"].ToString();
            TxtTDate.Text = Session["EntryDate"].ToString();
        }
    }
    protected void TxtFPRD_TextChanged(object sender, EventArgs e)
    {
        TxtFPRDName.Text = BD.GetAccType(TxtFPRD.Text, Session["BRCD"].ToString());

        TxtTPRD.Focus();
    }
    protected void TxtTPRD_TextChanged(object sender, EventArgs e)
    {
        TxtTPRDName.Text = BD.GetAccType(TxtTPRD.Text, Session["BRCD"].ToString());

        TxtCTRL.Focus();
    }
    protected void TxtCTRL_TextChanged(object sender, EventArgs e)
    {
        TxtTPRDName.Text = BD.GetAccType(TxtCTRL.Text, Session["BRCD"].ToString());

        TxtFDate.Focus();
    }
    protected void GrdCTR_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdCTR.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        BindGrid();
        FL = "Insert";//ankita 14/09/2017
        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "FormRBI_A_Grd_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
    }
    public void BindGrid()
    {
        int RC = CTR.GetCTR(GrdCTR, TxtFPRD.Text, TxtTPRD.Text, TxtFDate.Text, TxtTDate.Text, TxtCTRL.Text);
    }
    public void CallReport()
    {
        try
        {
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "FormRBI_A_Rpt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?FGL=" + TxtFPRD.Text + "&TGL=" + TxtTPRD.Text + "&AsOnDate=" + TxtAsonDate.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&FSGL=" + TxtFPRD.Text + "&TSGL=" + TxtTPRD.Text + "&CTRL=" + TxtCTRL.Text + "&rptname=RptFormA.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Report_Click(object sender, EventArgs e)
    {
        CallReport();
    }
    protected void TextReport_Click(object sender, EventArgs e)
    {
        List<object> lst = new List<object>();
        lst.Add("CTR Report");
        lst.Add(Session["UserName"].ToString());
        lst.Add(TxtFPRD.Text);
        lst.Add(TxtTPRD.Text);
        lst.Add(TxtFDate.Text);
        lst.Add(TxtTDate.Text);
        lst.Add(TxtCTRL.Text);

        CTRReportTxt repObj = new CTRReportTxt();
        repObj.RInit(lst);
        repObj.Start();
    }
}