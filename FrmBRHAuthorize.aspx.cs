using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmBRHAuthorize : System.Web.UI.Page
{
    ClsBranchHandover BRH = new ClsBranchHandover();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["FL"] != null)
            {
                ViewState["FL"] = Request.QueryString["FL"].ToString();
            }
            else
            {
                ViewState["FL"] = "";
            }
            GrdBranchH.Columns[0].Visible = false;
            BindGrid();
           
        }
    }
    public void BindGrid()
    {
        try
        {
            int RS = BRH.BindGrid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["FL"].ToString(), GrdBranchH);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void rdbSingle_CheckedChanged(object sender, EventArgs e)
    {
        GrdBranchH.Columns[0].Visible = true;
        ALLBTN.Visible = false;
    }
    protected void rdbMultiple_CheckedChanged(object sender, EventArgs e)
    {
        GrdBranchH.Columns[0].Visible = false;
        ALLBTN.Visible = true;
    }
    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        LinkButton objlink = (LinkButton)sender;
        string strlnk = objlink.CommandArgument;
        int RS = BRH.AuthorisedEntry(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["FL"].ToString(), strlnk, "SINGLE", Session["MID"].ToString());
        BindGrid();
        GrdBranchH.Columns[0].Visible = true;
    }
    protected void GrdBranchH_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdBranchH.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void BtnAll_Click(object sender, EventArgs e)
    {
        try
        {
            int RS = BRH.AuthorisedEntry(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["FL"].ToString(), "", "ALL", Session["MID"].ToString());
            BindGrid();
            GrdBranchH.Columns[0].Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}