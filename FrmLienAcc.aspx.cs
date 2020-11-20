using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmLienAcc : System.Web.UI.Page
{
    ClsLienMark lm = new ClsLienMark();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lm.Bindacctype(ddlacctype);
        }
    }
    protected void txtRpcode_TextChanged(object sender, EventArgs e)
    {
        string GLNAME = lm.getglname(txtRpcode.Text, Session["BRCD"].ToString());
        if (GLNAME != "")
        {

            txtRname.Text = GLNAME;

        }
        else
        {
            WebMsgBox.Show("Please Enter Deposit Code", this.Page);
        }
    }
    protected void txtRname_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btOkReport_Click(object sender, EventArgs e)
    {
        try
        {
            string RedirectUrl = "FrmRView.aspx?FDate=" + TxtFdate.Text + "&TDate=" + txttdate.Text + "&UserName=" + Session["UserName"].ToString() + " &Pcode=" + txtRpcode.Text + "&BRCD1=" + txtbrcd1.Text + "&BRCD2=" + txtbrcd2.Text + "&LIENTYPE=" + ddlacctype.SelectedValue + " &rptname=RptLienMarkLientype.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + RedirectUrl + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}