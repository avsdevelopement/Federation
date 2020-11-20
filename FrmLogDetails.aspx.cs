using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmLogDetails : System.Web.UI.Page
{
    ClsLogDetails LD = new ClsLogDetails();
    DbConnection conn = new DbConnection();
    ClsBindDropdown bindddl = new ClsBindDropdown();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
 string FL = "";
    int res = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            bindddl.BindLogActivity(ddlActivity);
        }
    }
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            bindgrid();
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Logdetails _"  + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void Btn_ClearAll_Click(object sender, EventArgs e)
    {
        try
        {
            clear();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    public void clear()
    {
        TxtBRCD.Text = "";
        TxtBRCDName.Text = "";
        ddlActivity.SelectedIndex = 0;
    }
    protected void TxtBRCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBRCD.Text != "")
            {
                string bname = LD.GetBranchName(TxtBRCD.Text);
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
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void bindgrid()
    {
        try
        {
            if (TxtBRCD.Text != "" && ddlActivity.SelectedValue != "")
            {
                res = LD.getLogDetails(grdLogDetails, TxtBRCD.Text.ToString(), ddlActivity.SelectedValue.ToString(),TxtFdate.Text,TxtTDate.Text);
                if (res < 0)
                {
                    WebMsgBox.Show("Sorry!! Record not found!", this.Page);
                }
            }
            else
            {
                WebMsgBox.Show("Enter the details", this.Page);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    
    }
    protected void Btnreport_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtBRCD.Text != "" && ddlActivity.SelectedValue != "")
            {
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Logdetails_Rpt _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBRCD.Text + "&ACTVT=" + ddlActivity.SelectedValue + "&UserName=" + Session["UserName"].ToString() + "&FDate=" + TxtFdate.Text + "&TDate=" + TxtTDate.Text + "&rptname=RptLogDetails.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else
            {
                WebMsgBox.Show("Enter the details", this.Page);
            }
        }
        catch (Exception Ex)
        {
             ExceptionLogging.SendErrorToText(Ex);
        }
    }
}