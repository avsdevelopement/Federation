using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAVS5069 : System.Web.UI.Page
{
    ClsAccountSTS AST = new ClsAccountSTS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TxtDate.Text = Session["EntryDate"].ToString();
            txtTdate.Text = Session["EntryDate"].ToString();
            TxtFBRCD.Text = Session["BRCD"].ToString();
            TxtTBRCD.Text = Session["BRCD"].ToString();
            TxtFBRCDName.Text = AST.GetBranchName(TxtFBRCD.Text);
            TxtTBRCDName.Text = AST.GetBranchName(TxtTBRCD.Text);
           // TxtFPRD.Focus();
        }
    }
    protected void New_Report_Click(object sender, EventArgs e)
    {
       string Flag="";
        try
        {
              if (Rdb_AccType.SelectedValue == "1")  //ashok misal
              {
                Flag = "1";
                string redirectURL = "FrmRView.aspx?FDate=" + TxtDate.Text + "&TDate=" + txtTdate.Text + "&Fbrcd=" + TxtFBRCD.Text + "&Tbrcd=" + TxtTBRCD.Text + "&SL=" + Flag.ToString() + "&rptname=RptDeLoanSummery.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
               }
              else if (Rdb_AccType.SelectedValue == "2")
               {
                Flag = "2";
                string redirectURL = "FrmRView.aspx?FDate=" + TxtDate.Text + "&TDate=" + txtTdate.Text + "&Fbrcd=" + TxtFBRCD.Text + "&Tbrcd=" + TxtTBRCD.Text + "&SL=" + Flag.ToString() + "&rptname=RptDLienDetails.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
               }

        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Exit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Rdb_AccType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

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
            if (TxtTBRCD.Text != "")
            {
                string bname = AST.GetBranchName(TxtTBRCD.Text);
                if (bname != null)
                {
                    TxtTBRCDName.Text = bname;
                    //TxtFPRD.Focus();

                    if (Convert.ToInt32(TxtFBRCD.Text) > Convert.ToInt32(TxtTBRCD.Text))
                    {
                        WebMsgBox.Show("Invalid FROM and TO Branch Code....!", this.Page);
                        //Clear();
                        return;
                    }

                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    TxtTBRCD.Text = "";
                    TxtTBRCD.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                TxtTBRCD.Text = "";
                TxtTBRCD.Focus();
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFBRCD_TextChanged(object sender, EventArgs e)
    {
        if (TxtFBRCD.Text != "")
        {
            if (TxtTBRCD.Text != "" && (Convert.ToInt32(TxtFBRCD.Text) > Convert.ToInt32(TxtTBRCD.Text)))
            {
                WebMsgBox.Show("Invalid FROM and TO Branch Code....!", this.Page);
                //Clear();
                return;
            }

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
}