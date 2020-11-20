using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmCustRpt : System.Web.UI.Page
{
    int result = 0;
    DbConnection conn = new DbConnection();
    ClsAccountSTS AST = new ClsAccountSTS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtfromdate.Text = Session["EntryDate"].ToString();
            txttodate.Text = Session["EntryDate"].ToString();
            TxtFBRCD.Text = Session["BRCD"].ToString();
            TxtTBRCD.Text = Session["BRCD"].ToString();
            TxtFBRCDName.Text = Session["BName"].ToString();
            TxtTBRCDName.Text = Session["BName"].ToString();
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
        txtfromdate.Text = "";
        txttodate.Text = "";
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
    protected void BtnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string redirectURL = "FrmRView.aspx?&FDATE=" + conn.ConvertDate(txtfromdate.Text) + "&TDATE=" + conn.ConvertDate(txttodate.Text) + "&FBRCD=" + TxtFBRCD.Text + "&TBRCD=" + TxtTBRCD.Text + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptCustDetails.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}