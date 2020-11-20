using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmGoldAuction : System.Web.UI.Page
{
    ClsRptSurity CR = new ClsRptSurity();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void txtloancode1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AGCD = "";
            AGCD = txtloancode1.Text;

            string TD = CR.GETLOANNAME(txtloancode1.Text, txtbranchcode1.Text);

            if (TD != null)
            {
                txtloan1name.Text = TD;
                TXTACCNO.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter valid Loan code!.....", this.Page);
                txtloancode1.Text = "";
                txtloan1name.Text = "";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TXTACCNO_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AGCD = "";
            AGCD = txtloancode1.Text;

            string TD = CR.getname(txtloancode1.Text, txtbranchcode1.Text, TXTACCNO.Text);

            if (TD != null)
            {
                txtaccname.Text = TD;

            }
            else
            {
                WebMsgBox.Show("Enter valid accno No!.....", this.Page);
                TXTACCNO.Text = "";
                txtaccname.Text = "";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptGoldAuction.rdlc";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

    }
    protected void btnExit_Click(object sender, EventArgs e)
    {

    }
}