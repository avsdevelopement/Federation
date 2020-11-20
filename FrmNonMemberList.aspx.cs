using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmNonMemberList : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsMaturityLoanReport MLR = new ClsMaturityLoanReport();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsLogMaintainance CLM = new ClsLogMaintainance();

    string FL = "", FDATE = "", TDATE = "";
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

                TxtBRCD.Text = Session["BRCD"].ToString();
                TxtBRCDName.Text = AST.GetBranchName(TxtBRCD.Text);
                txtfromdate.Text = Session["EntryDate"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }

    public void Clear()
    {
        TxtBRCD.Text = "";
        TxtBRCDName.Text = "";
        txtfromdate.Text = "";
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void BtnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtfromdate.Text != null && txtprdcode.Text != null)
            {
                string SL = "";
                SL = "ALL";
                FL = "Insert";//ankita 14/09/2017
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "MatLoan_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                string redirectURL = "FrmRView.aspx?Brcd=" + TxtBRCD.Text + "&Product=" + txtprdcode.Text + "&AsonDate=" + txtfromdate.Text + "&rptname=RptNonMemList.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtBRCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
                if (TxtBRCD.Text != "")
                {
                    string bname = AST.GetBranchName(TxtBRCD.Text);
                    if (bname != null)
                    {
                        TxtBRCDName.Text = bname;
                     }
                    else
                    {
                        WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                        TxtBRCD.Text = "";
                        TxtBRCD.Focus();
                    }
                }
                else
                {
                    WebMsgBox.Show("Enter Branch Code!....", this.Page);
                    TxtBRCD.Text = "";
                    TxtBRCD.Focus();
                }
            }
        catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
    }
}