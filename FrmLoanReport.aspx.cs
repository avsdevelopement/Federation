using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmLoanReport : System.Web.UI.Page
{
    ClsLoanInfo LI = new ClsLoanInfo();
    ClsAccopen AO = new ClsAccopen();
    ClsBindDropdown BD = new ClsBindDropdown();   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                ViewState["PT"] = Request.QueryString["PT"].ToString();
                ViewState["AC"] = Request.QueryString["AC"].ToString();
               // ViewState["GL"] = Request.QueryString["GL"].ToString();
                BindGrid();
                TxtPtype.Text = ViewState["PT"].ToString();
                TxtPname.Text = BD.GetAccType(TxtPtype.Text, Session["BRCD"].ToString());
                TxtAccNo.Text = ViewState["AC"].ToString();
                int RC = LI.CheckAccount(TxtAccNo.Text, TxtPtype.Text, Session["BRCD"].ToString());
                TxtCustName.Text = AO.Getcustname(RC.ToString());
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
    }
    protected void GrdLoanSchedule_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdLoanSchedule.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindGrid()
    {
        try
        {
            int RC = LI.BindSchedule(GrdLoanSchedule, ViewState["PT"].ToString(), ViewState["AC"].ToString(), Session["BRCD"].ToString());
            if (RC > 0)
            {
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
            string redirectURL = "FrmRView.aspx?PT=" + TxtPtype.Text + "&AC=" + TxtAccNo.Text + "&BRCD=" + Session["BRCD"].ToString() + "&rptname=RptLoanSchedule.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}