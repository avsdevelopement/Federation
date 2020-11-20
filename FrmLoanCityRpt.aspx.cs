using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmLoanCityRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TxtBrID.Text = Session["BRCD"].ToString();
        }
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            string Flag = "";
            if (rbnclose.Checked == true)
            {
                Flag = "CLOSE";
                string redirectURL = "FrmRView.aspx?FDate=" + TxtFDate.Text + "&TDATE=" + TxtTDate.Text + "&Flag=" + Flag + "&UserName=" + Session["UserName"].ToString() + "&BRCD=" + TxtBrID.Text + "&rptname=RptLoanClose.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (rbncity.Checked == true)
            {
                Flag = "CITY";
                string redirectURL = "FrmRView.aspx?FDate=" + TxtFDate.Text + "&TDATE=" + TxtTDate.Text + "&Flag=" + Flag + "&UserName=" + Session["UserName"].ToString() + "&BRCD=" + TxtBrID.Text + "&rptname=RptloanCityWise.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (rbnamount.Checked == true)
            {

                string redirectURL = "FrmRView.aspx?FDate=" + TxtFDate.Text + "&TDATE=" + TxtTDate.Text + "&FAMT=" + txtfromamt.Text + "&TAMT=" + txttoamt.Text + "&UserName=" + Session["UserName"].ToString() + "&BRCD=" + TxtBrID.Text + "&rptname=RptLoanAmountWise.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
          


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void btnclear_Click(object sender, EventArgs e)
    {

    }
    protected void btnExit_Click(object sender, EventArgs e)
    {

    }
    protected void GrdEmployeeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GrdEmployeeDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GrdEmployeeDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void rbnamount_CheckedChanged(object sender, EventArgs e)
    {
        amount.Visible = true;
    }
    protected void rbncity_CheckedChanged(object sender, EventArgs e)
    {
        amount.Visible = false;
    }
    protected void rbnclose_CheckedChanged(object sender, EventArgs e)
    {
        amount.Visible = false;
    }
}