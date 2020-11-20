using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmChequeStockReport : System.Web.UI.Page
{
    ClsChequeStockReport ck = new ClsChequeStockReport();
    string RButton;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            autoglname.ContextKey = Session["BRCD"].ToString();
            rbtnSpecificProd.Checked = true;
            rbtnAllProd.Checked = false;
        }
    }

    protected void txtProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtProdName.Text = ck.GetAccType(txtProdCode.Text, Session["BRCD"].ToString());
            txtAsOnDate.Focus();
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

    protected void grdChequeStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdChequeStockReport.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindGrid()
    {
        int Result;

        if (rbtnSpecificProd.Checked)
        {
            RButton = "S";
        }
        else if (rbtnAllProd.Checked)
        {
            RButton = "A";
        }

        Result = ck.BindGrid(grdChequeStockReport, Session["BRCD"].ToString(), txtProdCode.Text, txtAsOnDate.Text, RButton);
    }

    protected void SpecificProd_CheckedChanged(object sender, EventArgs e)
    {
        rbtnSpecificProd.Checked = true;
        rbtnAllProd.Checked = false;
        divProd.Visible = true;
    }

    protected void AllProd_CheckedChanged(object sender, EventArgs e)
    {
        rbtnAllProd.Checked = true;
        rbtnSpecificProd.Checked = false;
        divProd.Visible = false;
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
                string[] AC = ck.Getaccno(txtProdCode.Text, Session["BRCD"].ToString(), custnob[2].ToString()).Split('-');
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


    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (rbtnSpecificProd.Checked)
            {
                RButton = "S";
                string redirectURL = "FrmRView.aspx?BCode=" + Session["BRCD"].ToString() + "&PCode=" + txtProdCode.Text + "&AsOnDate=" + txtAsOnDate.Text + "&Flag=" + RButton + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptChequeStock.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (rbtnAllProd.Checked)
            {
                RButton = "A";
                string redirectURL = "FrmRView.aspx?BCode=" + Session["BRCD"].ToString() + "&AsOnDate=" + txtAsOnDate.Text + "&Flag=" + RButton + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptChequeStock.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }

            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}