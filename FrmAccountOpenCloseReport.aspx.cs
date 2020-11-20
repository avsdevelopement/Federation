using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAccountOpenCloseReport : System.Web.UI.Page
{
    ClsAccountOpenCloseReport OC = new ClsAccountOpenCloseReport();
    string RButton;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            autoglname.ContextKey = Session["BRCD"].ToString();
            rbtnOpen.Checked = true;
            rbtnClose.Checked = false;
        }
    }

    protected void rbtnOpen_CheckedChanged(object sender, EventArgs e)
    {
        rbtnOpen.Checked = true;
        rbtnClose.Checked = false;
    }

    protected void rbtnClose_CheckedChanged(object sender, EventArgs e)
    {
        rbtnClose.Checked = true;
        rbtnOpen.Checked = false;
    }

    protected void txtProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtProdName.Text = OC.GetAccType(txtProdCode.Text, Session["BRCD"].ToString());
            txtAsOnDate.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtProdName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtProdName.Text = custnob[0].ToString();
                txtProdCode.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] AC = OC.Getaccno(txtProdCode.Text, Session["BRCD"].ToString(), custnob[2].ToString()).Split('-');
                ViewState["ProdName"] = AC[0].ToString();
                ViewState["ProdCode"] = AC[1].ToString();
                txtAsOnDate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    public void BindGrid()
    {
        int Result;

        if (rbtnOpen.Checked)
        {
            RButton = "OPENING";
        }
        else if (rbtnClose.Checked)
        {
            RButton = "CLOSING";
        }

        Result = OC.BindGrid(grdAcctsopenclose, Session["BRCD"].ToString(), txtProdCode.Text, txtAsOnDate.Text, RButton);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (rbtnOpen.Checked)
            {
                RButton = "OPENING";
            }
            else if (rbtnClose.Checked)
            {
                RButton = "CLOSING";
            }

            string redirectURL = "FrmRView.aspx?BCode=" + Session["BRCD"].ToString() + "&PCode=" + txtProdCode.Text + "&AsOnDate=" + txtAsOnDate.Text + "&Flag=" + RButton + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptAccountOpenCloseReport.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grdAcctsopenclose_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdAcctsopenclose.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}