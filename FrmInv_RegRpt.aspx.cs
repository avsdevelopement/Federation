using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmInv_RegRpt : System.Web.UI.Page
{
    double Total = 0;
    ClsInvClosure IC = new ClsInvClosure();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    scustom customcs = new scustom();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            txtfromdate.Text = Session["EntryDate"].ToString();
            customcs.BindInvType(ddlInv);
        }
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    public void BindGrid()
    {
        if (RdblSelection.SelectedValue == "0")
        {
            try
            {
                DataTable dt = new DataTable();
                dt = IC.BINDGRIDall(Session["BRCD"].ToString(), txtfromdate.Text);
                GrdInv.DataSource = dt;
                GrdInv.DataBind();
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
        else
        {
            try
            {
                DataTable dt = new DataTable();
                dt = IC.BINDGRID(Session["BRCD"].ToString(), txtfromdate.Text, txtfdepositgl.Text);
                GrdInv.DataSource = dt;
                GrdInv.DataBind();
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
    }
    
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmBlank.aspx");
    }
    protected void GrdInv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdInv.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void RdblSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RdblSelection.SelectedValue == "0")
        {
            DivShow.Visible = false;
            DivGroup.Visible = false;
            hdnFlag.Value = "0";
        }
        else if (RdblSelection.SelectedValue == "1")
        {
            DivShow.Visible = true;
            DivGroup.Visible = false;
            hdnFlag.Value = "1";
        }
        else if (RdblSelection.SelectedValue == "2")
        {
            DivShow.Visible = false;
            DivGroup.Visible = true;
            hdnFlag.Value = "2";
        }
    }
    protected void BtnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (RdblSelection.SelectedValue == "0")
            {
                FL = "Insert";//Dhanya Shetty
                string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_Bal_Rpt _"+ Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"] + "&UName=" + Session["UserName"] + "&EDAT=" + txtfromdate.Text + "&Flag=" + RdblSelection.SelectedValue + "&rptname=RptInv_Reg.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (RdblSelection.SelectedValue == "1")
            {
                FL = "Insert";//Dhanya Shetty
                string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_Bal_Rpt _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"] + "&UName=" + Session["UserName"] + "&EDAT=" + txtfromdate.Text + "&PROD=" + txtfdepositgl.Text + "&Flag=" + RdblSelection.SelectedValue + "&rptname=RptInv_Reg.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else
            {

                FL = "Insert";//Dhanya Shetty
                string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_Bal_Rpt _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                 string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"] + "&UName=" + Session["UserName"] + "&EDAT=" + txtfromdate.Text + " &Flag=" + RdblSelection.SelectedValue + "&Value=" + ddlInv.SelectedValue+ "&rptname=RptInvProd.rdlc";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
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
}