using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAVSMBTableTransaction : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    DbConnection Conn = new DbConnection();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void TxtFPRD_TextChanged(object sender, EventArgs e)
    {
        TxtFPRDName.Text = BD.GetAccType(TxtFPRD.Text, Session["BRCD"].ToString());

        TxtFPRD.Focus();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&SubGlCode=" + TxtFPRD.Text + "&AsOnDate=" + Conn.ConvertDate(TxtAsonDate.Text).ToString() + "&rptname=RptAVSBMTableReport.rdlc" + "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}