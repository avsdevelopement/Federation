using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmIntRateDepostLoan : System.Web.UI.Page
{
    ClsIntRateDEpositLoan DL = new ClsIntRateDEpositLoan();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
        
        }
    }
    protected void TxtFSubgl_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string Result = DL.GetGLname(Session["BRCD"].ToString(), TxtFSubgl.Text);
            txtsubglname.Text = Result.ToString();
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            //if (Rdeatils.SelectedValue == "1")
            //{
            //    string redirectURL = "FrmRView.aspx?FBRCD=" + TxtFBrID.Text + "&TBRCD=" + TxtTBrID.Text + "&PrdCd=" + TxtFSubgl.Text + "&AsOnDate=" + TxtAsonDate.Text + "&rptname=RptIntRateSummaryDPList.rdlc";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            //}
            //if (Rdeatils.SelectedValue == "2")
            //{
            //    string redirectURL = "FrmRView.aspx?FBRCD=" + TxtFBrID.Text + "&TBRCD=" + TxtTBrID.Text + "&PrdCd=" + TxtFSubgl.Text + "&AsOnDate=" + TxtAsonDate.Text + "&rptname=RptIntRateSummaryLoansList.rdlc";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            //}
            if (Rdeatils.SelectedValue == "1")
            {
                string redirectURL = "FrmRView.aspx?FBRCD=" + TxtFBrID.Text + "&TBRCD=" + TxtTBrID.Text + "&PrdCd=" + TxtFSubgl.Text + "&AsOnDate=" + TxtAsonDate.Text + "&FInTRate=" + txtIntRate.Text + "&TInTRate=" + txtToIntRate.Text + "&rptname=RptIntRateSummaryDPList_DT.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (Rdeatils.SelectedValue == "2")
            {
                string redirectURL = "FrmRView.aspx?FBRCD=" + TxtFBrID.Text + "&TBRCD=" + TxtTBrID.Text + "&PrdCd=" + TxtFSubgl.Text + "&AsOnDate=" + TxtAsonDate.Text + "&rptname=RptIntRateSummaryDPList.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Rdeatils_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Rdeatils.SelectedValue == "1")
                DivIntRate.Visible = true;
            else
                DivIntRate.Visible = false;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}