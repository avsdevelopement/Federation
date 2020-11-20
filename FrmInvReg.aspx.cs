using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmInvReg : System.Web.UI.Page
{
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

        }
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmBlank.aspx");
    }
    protected void RdblSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RdblSelection.SelectedValue == "0")
            DivShow.Visible = false;
        else
            DivShow.Visible = true;
    }
    public void Clear()
    {
        txtfromdate.Text = "";
        txttodate.Text = "";
        txtfdepositgl.Text = "";
    }
    protected void GrdInv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdInv.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    public void BindGrid()
    {
        DataTable dt = new DataTable();
        dt = IC.GetInvDetails(RdblSelection.SelectedValue, "1", txtfromdate.Text, txttodate.Text, Session["EntryDate"].ToString(), txtfdepositgl.Text);
      //  dt = IC.BINDGRID(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), txtfdepositgl.Text);
        GrdInv.DataSource = dt;
        GrdInv.DataBind();
    }
  
    protected void BtnDueDateRpt_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//Dhanya Shetty
            string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_DueDate_Rpt _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString()+ "&FDate=" + txtfromdate.Text + "&TDate=" + txttodate.Text + "&EDAT=" + Session["EntryDate"].ToString() +  "&UserName=" + Session["UserName"].ToString() +"&rptname=RptduedateInvst.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
           ExceptionLogging.SendErrorToText(Ex);
        }
    }
    //protected void BtnBalList_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&FDate=" + txtfromdate.Text + "&TDate=" + txttodate.Text + "&EDAT=" + Session["EntryDate"].ToString() + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptInvBalList.rdlc";
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
    protected void BtnCloseList_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//Dhanya Shetty
            string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_CloseInv_Rpt _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&FDate=" + txtfromdate.Text + "&TDate=" + txttodate.Text + "&EDAT=" + Session["EntryDate"].ToString() + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptCloseInvList.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnStartdate_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//Dhanya Shetty
            string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_Startdate_Rpt _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&FDate=" + txtfromdate.Text + "&TDate=" + txttodate.Text +"&UserName=" + Session["UserName"].ToString() + "&rptname=RptStartDateInvst.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string flag = RdblSelection.SelectedValue;

            if (RdblSelection.SelectedValue == "0")
            {
                FL = "Insert";//Dhanya Shetty
                string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment_Rpt _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&UName=" + Session["UserName"].ToString() + "&FDAT=" + txtfromdate.Text + "&TDAT=" + txttodate.Text + "&EDAT=" + Session["EntryDate"].ToString() + "&Flag=" + flag.ToString() + "&rptname=RptInvMat.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else
            {
                FL = "Insert";//Dhanya Shetty
                string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Investment__Rpt _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&UName=" + Session["UserName"].ToString() + "&FDAT=" + txtfromdate.Text + "&TDAT=" + txttodate.Text + "&EDAT=" + Session["EntryDate"].ToString() + "&PROD=" + txtfdepositgl.Text + "&Flag=" + flag.ToString() + "&rptname=RptInvMat.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}