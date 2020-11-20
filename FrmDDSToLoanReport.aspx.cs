using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmDDSToLoanReport : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }
                BD.BindSIStatus(DdlStatus);
                //added by ankita 07/10/2017 to make user frndly 
                TxtFDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
                TxtTDate.Text = Session["EntryDate"].ToString();
                TxtFDate.Focus();
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
    }

    protected void Exit_Click(object sender, EventArgs e)
    {
        //  Response.Redirect("FrmBlank.aspx");
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void BtnReport_Click(object sender, EventArgs e)
    {
         
            try
            {                    
                string FLAG = "";
                string SI_Type = "";

                if (Rdb_ReportType.SelectedValue == "1")
                    FLAG = "Report1";
                FL = "Insert";//ankita 15/09/2017
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSTOLoan_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            //  string redirectURL = "FrmRView.aspx?FLAG=" + FLAG + "&FromDate=" + TxtFDate.Text + "&ToDate=" + TxtTDate.Text + "&Ddlcheck=" + DdlStatus.SelectedValue + "&BRCD=" + Session["BRCD"].ToString() + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptSI_DDStoLoan.rdlc";
                string redirectURL = "FrmRView.aspx?FLAG=" + FLAG + "&FromDate=" + TxtFDate.Text + "&ToDate=" + TxtTDate.Text + "&Ddlcheck=" + DdlStatus.SelectedValue + "&BRCD=" + Session["BRCD"].ToString() + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptDetailsDDSTOLOAN.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
    
        
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
           
    }
}