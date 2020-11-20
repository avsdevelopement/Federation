using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAVS5056 : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsBindDropdown BD = new ClsBindDropdown();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtfromdate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
                txttodate.Text = Session["EntryDate"].ToString();
                TxtFBRCD.Text = Session["BRCD"].ToString();
                TxtFBRCDName.Text = AST.GetBranchName(TxtFBRCD.Text);
                TxtTBRCD.Text = Session["BRCD"].ToString();
                TxtTBRCDName.Text = AST.GetBranchName(TxtTBRCD.Text);
            
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
              string redirectURL = "FrmRView.aspx?&FDATE=" + conn.ConvertDate(txtfromdate.Text) + "&TDATE=" + conn.ConvertDate(txttodate.Text) + "&FBRCD=" + TxtFBRCD.Text + "&TBRCD=" + TxtTBRCD.Text + "&Sub1=" + TxtProdCode1.Text + "&Sub2=" + TxtProdCode2.Text + "&rptname=RptLoanNill.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void ClearAll()
    {
        txtfromdate.Text = "";
        txttodate.Text = "";
        TxtFBRCD.Text = "";
        TxtTBRCD.Text = "";
        TxtFBRCDName.Text = "";
        TxtTBRCDName.Text = "";
        TxtProdCode1.Text = "";
        TxtProdCode2.Text = "";
        TxtProdCodeName1.Text = "";
        TxtProdCodeName2.Text = "";
    
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAll();
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
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
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
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
                    TxtProdCode1.Focus();

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
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtProdCode1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string tds = BD.GetLoanGL(TxtProdCode1.Text, Session["BRCD"].ToString());
            if (tds != null)
            {
                string[] TD = tds.Split('_');
                if (TD.Length > 0)
                {

                }
                TxtProdCodeName1.Text = TD[0].ToString();
                TxtProdCode1.Text = TD[1].ToString();

            }
            else
            {
                WebMsgBox.Show("Invalid Deposit Code......!", this.Page);
                TxtProdCodeName1.Text = "";
                TxtProdCode1.Text = "";
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtProdCode2_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string tds = BD.GetLoanGL(TxtProdCode2.Text, Session["BRCD"].ToString());
            if (tds != null)
            {
                string[] TD = tds.Split('_');
                if (TD.Length > 0)
                {

                }
                TxtProdCodeName2.Text = TD[0].ToString();
                TxtProdCode2.Text = TD[1].ToString();

            }
            else
            {
                WebMsgBox.Show("Invalid Deposit Code......!", this.Page);
                TxtProdCodeName2.Text = "";
                TxtProdCode2.Text = "";
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}