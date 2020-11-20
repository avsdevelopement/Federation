using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmUnoperativeAccountReport : System.Web.UI.Page
{
    ClsUnOperativeAccountsReport UOA = new ClsUnOperativeAccountsReport();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            autoglname.ContextKey = Session["BRCD"].ToString();
            //added by ankita 07/10/2017 to make user frndly
            txtAsOnDate.Text = Session["EntryDate"].ToString();
            txtFromBr.Text = Session["BRCD"].ToString();
            txtToBr.Text = Session["BRCD"].ToString();
        }
    }

    protected void txtProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtProdName.Text = UOA.GetAccType(txtProdCode.Text, Session["BRCD"].ToString());
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
                string[] AC = UOA.Getaccno(txtProdCode.Text, Session["BRCD"].ToString(), custnob[2].ToString()).Split('-');
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
        FL = "Insert";//ankita 15/09/2017
        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "UnOpAcc_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
    }

    public void BindGrid()
    {
        int Result;

        divmsg.Visible = true;
        lblMsg.Text = "Please Wait";
        Result = UOA.BindGrid(grdUnOperativeAccts, txtProdCode.Text, txtAsOnDate.Text, txtMonth.Text, txtFromBr.Text, txtToBr.Text);
        divmsg.Visible = false;
        lblMsg.Text = "";
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "UnOpAcc_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            string redirectURL = "FrmRView.aspx?FBCode=" + txtFromBr.Text + "&TBCode=" + txtToBr.Text + "&PCode=" + txtProdCode.Text + "&AsOnDate=" + txtAsOnDate.Text + "&Month=" + txtMonth.Text + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptUnOpAccts.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grdUnOperativeAccts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdUnOperativeAccts.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}