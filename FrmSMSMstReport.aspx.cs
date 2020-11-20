using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmSMSMstReport : System.Web.UI.Page
{
    int result = 0;
    DbConnection conn = new DbConnection();
    ClsAccountSTS AST = new ClsAccountSTS();
    Mobile_Service MS = new Mobile_Service();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    string mobile;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            txtfromdate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
            txttodate.Text = Session["EntryDate"].ToString();
            TxtFBRCD.Text = Session["BRCD"].ToString();
            TxtTBRCD.Text = Session["BRCD"].ToString();
            TxtFBRCDName.Text = Session["BName"].ToString();
            TxtTBRCDName.Text = Session["BName"].ToString();
            btntrail.Enabled = false;
        }
    }
    protected void BtnReport_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SMSMst_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            if(rblmob.SelectedValue == "1")
            {
                if (TxtMobileno.Text == "")
                {
                    WebMsgBox.Show("Please Enter mobile number", this.Page);
                }
                else
                {
                    mobile =TxtMobileno.Text;
                    string redirectURL = "FrmRView.aspx?&FDATE=" + conn.ConvertDate(txtfromdate.Text) + "&TDATE=" + conn.ConvertDate(txttodate.Text) + "&FBRCD=" + TxtFBRCD.Text + "&TBRCD=" + TxtTBRCD.Text + "&MOBILE=" + mobile + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptSmsMstReport.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
            else
            {
                mobile = "0";
                string redirectURL = "FrmRView.aspx?&FDATE=" + conn.ConvertDate(txtfromdate.Text) + "&TDATE=" + conn.ConvertDate(txttodate.Text) + "&FBRCD=" + TxtFBRCD.Text + "&TBRCD=" + TxtTBRCD.Text + "&MOBILE=" + mobile + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptSmsMstReport.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    public void clear()
    {
        TxtFBRCD.Text = "";
        TxtTBRCD.Text = "";
        txtfromdate.Text="";
        txttodate.Text="";
    }
    protected void TxtFBRCD_TextChanged(object sender, EventArgs e)
    {
         try
         {
             if (TxtFBRCD.Text != "")
             {
                 string bname = AST.GetBranchName(TxtFBRCD.Text);
                 if (bname != null)
                 {
                     TxtFBRCDName.Text = bname;
                     TxtTBRCD.Focus();

                 }
                 else
                 {
                     WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                     TxtFBRCD.Text = "";
                     TxtFBRCD.Focus();
                 }
             }
             else
             {
                 WebMsgBox.Show("Enter Branch Code!....", this.Page);
                 TxtFBRCD.Text = "";
                 TxtFBRCD.Focus();
             }
         }
         catch (Exception Ex)
         {
             ExceptionLogging.SendErrorToText(Ex);
         }
    }
    protected void TxtTBRCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFBRCD.Text != "")
            {
                string bname = AST.GetBranchName(TxtTBRCD.Text);
                if (bname != null)
                {
                    TxtTBRCDName.Text = bname;
                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    TxtFBRCD.Text = "";
                    TxtFBRCD.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                TxtFBRCD.Text = "";
                TxtFBRCD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void rblmob_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblmob.SelectedValue == "1")
            {
                TxtMobileno.Visible = true;
                 btntrail.Enabled = true;
            }
            else
            {
                TxtMobileno.Visible = false;
                btntrail.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void btntrail_Click(object sender, EventArgs e)
    {
        try
        {
            string Status = "";
            string BankName = "";
            int Result = 0;
            BankName = AST.GetBankName();
            Result = AST.InsertTrailSMS(TxtMobileno.Text, Session["EntryDate"].ToString(), "Welcome, Test massage from "+BankName+".", "1");
            if (Result > 0)
                Status = MS.Send_TrailSMS(TxtMobileno.Text, Session["EntryDate"].ToString());
            if (Status == "000" || Status == "1701")
            {
                WebMsgBox.Show("SMS send Successfully!!!", this.Page);
                TxtMobileno.Text = "";
            }
            else
                WebMsgBox.Show("Unsuccessfull!!!", this.Page);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
      
    }
}