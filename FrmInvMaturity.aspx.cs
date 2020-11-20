using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmInvMaturity : System.Web.UI.Page
{
    double Total = 0;
    ClsInvClosure IC = new ClsInvClosure();
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
            txtFromDate.Text = Session["EntryDate"].ToString();
            txtToDate.Text = Session["EntryDate"].ToString();
            
        }
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//Dhanya Shetty
            string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_Maturity_Rpt _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&FDAT=" + txtFromDate.Text + "&UName=" + Session["UserName"] + "&TDAT=" + txtToDate.Text + "&rptname=RptInvMaturity.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmBlank.aspx");
    }
    protected void GrdInv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Total += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CLOSINGBAL"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblamount = (Label)e.Row.FindControl("lblTotal");
            lblamount.Text = Total.ToString();
        }
    }
    public void BindGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = IC.bindMatureInv(Session["BRCD"].ToString(), txtFromDate.Text, txtToDate.Text);
            GrdInv.DataSource = dt;
            GrdInv.DataBind();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}