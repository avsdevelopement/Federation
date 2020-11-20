using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmOverDueSummary : System.Web.UI.Page
{
    ClsOverDueSummary ODS = new ClsOverDueSummary();
    DataTable DT = new DataTable();
    string FL = "",SL="";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                TxtDate.Text = Session["EntryDate"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging .SendErrorToText(Ex);
        }
    }
    protected void Btn_Report_Click(object sender, EventArgs e)
    {
        try
        {
            string SL = "";
            SL = "ALL";
            string redirectURL = "FrmRView.aspx?Date=" + TxtDate.Text + "&UserName=" + Session["UserName"].ToString() + "&brcd=" + Session["BRCD"].ToString() + "&EntryDate=" + Session["EntryDate"].ToString() + "&SL=" + SL + "&rptname=RptOverDueSummary.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Rdb_AccType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void Btn_Grid_Click(object sender, EventArgs e)
    {
        try
        {
            
            FL = "ALL";
            SL = "OD";
            DT = ODS.GetFilter(SL,FL,Session["EntryDate"].ToString(), Session["BRCD"].ToString());
            grdDetails.DataSource = DT;
            grdDetails.DataBind();
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetails.PageIndex = e.NewPageIndex;
        FL = "ALL";
        SL = "OD";
        DT = ODS.GetFilter(SL, FL, Session["EntryDate"].ToString(), Session["BRCD"].ToString());
        grdDetails.DataSource = DT;
        grdDetails.DataBind();
      
    }
}