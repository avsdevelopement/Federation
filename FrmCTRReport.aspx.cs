using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmCTRReport : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
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
            //added by ankita 07/10/2017 to make user frndly 
            TxtFDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
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
        FL = "Insert";//ankita 15/09/2017
        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CTR_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        
    }
    public void BindGrid()
    {
        int RC = CTR.GetCTR(GrdCTR, TxtFPRD.Text, TxtTPRD.Text, TxtFDate.Text, TxtTDate.Text, TxtCTRL.Text);
    }
    public void CallReport()
    {
        try
        {
            string redirectURL = "FrmRView.aspx?FDate=" + TxtFDate.Text + "&TDate="+TxtTDate.Text+"&FSGL=" + TxtFPRD.Text + "&TSGL=" + TxtTPRD.Text + "&CTRL=" + TxtCTRL.Text +"&rptname=RptCTR.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Report_Click(object sender, EventArgs e)
    {
        FL = "Insert";//ankita 15/09/2017
        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CTR_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        CallReport();
    }
    protected void TextReport_Click(object sender, EventArgs e)
    {
        FL = "Insert";//ankita 15/09/2017
        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CTR_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
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