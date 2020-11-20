using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmFDRFromToAmtReport : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCTRReport CTR = new ClsCTRReport();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TxtFDate.Text = Session["EntryDate"].ToString();
            TxtTDate.Text = Session["EntryDate"].ToString();
        }
    }
    protected void Report_Click(object sender, EventArgs e)
    {
        try
        {
            string redirectURL = "FrmRView.aspx?FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&FSGL=" + TxtFPRD.Text + "&TSGL=" + TxtTPRD.Text + "&FAmount=" + TxtFAmount.Text + "&TAmount=" + TxtTAmount.Text + "&rptname=RptFDClassificationList.rdlc" + "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFPRD_TextChanged(object sender, EventArgs e)
    {
        TxtFPRDName.Text = BD.GetAccType(TxtFPRD.Text, Session["BRCD"].ToString());

        TxtTPRD.Focus();
    }
    protected void TxtTPRD_TextChanged(object sender, EventArgs e)
    {
        TxtTPRDName.Text = BD.GetAccType(TxtTPRD.Text, Session["BRCD"].ToString());

        TxtFAmount.Focus();
    }
}