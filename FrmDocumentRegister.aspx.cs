using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class FrmDocumentRegister : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsDocRegister DR = new ClsDocRegister();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        TxtFDOUpload.Focus();
        if (Session["UserName"] == null)
        {
            Response.Redirect("FrmLogin.aspx");
        }
        if (!IsPostBack)
        {
            //added by ankita 07/10/2017 to make user frndly 
            TxtFDOUpload.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
            TxtTDOUpload.Text = Session["EntryDate"].ToString();
        }      
    }
    protected void Exit_Click(object sender, EventArgs e)
    {

        //  Response.Redirect("FrmBlank.aspx");
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void BtnTextReport_Click(object sender, EventArgs e)
    {
        try
        {
            List<object> lst=new List<object>();
            lst.Add(Session["UserName"].ToString());
            lst.Add(Session["BRCD"].ToString());
            lst.Add("Document Register");
            lst.Add(Session["EntryDate"].ToString());
            lst.Add(TxtFDOUpload.Text);
            lst.Add(TxtTDOUpload.Text);
            lst.Add(TxtFDocType.Text);
            lst.Add(TxtTDocType.Text);
            DocTextReport DTR=new DocTextReport();
            DTR.RInit(lst);
            DTR.Start();
            
            WebMsgBox.Show("Report Generated Succesfully!!!....", this.Page);
            TxtFDOUpload.Focus();


        }
        catch(Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
  }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void BtnDownloadPDF_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DocReg_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?FromDocType=" + TxtFDocType.Text + "&ToDocType=" + TxtTDocType.Text + "&FDate=" + TxtFDOUpload.Text + "&TDate=" + TxtTDOUpload.Text + "&Username=" + Session["UserName"].ToString() + "&rptname=RptDocumentReg.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
}