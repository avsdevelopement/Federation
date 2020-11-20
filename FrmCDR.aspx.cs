using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmCDR : System.Web.UI.Page
{
    ClsBindDropdown DD=new ClsBindDropdown();
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
            TxtBrID.Focus();
            //added by ankita 07/10/2017 to make user frndly
            TxtBrID.Text = Session["BRCD"].ToString();
            txtBrName.Text=DD.GetBranchName(TxtBrID.Text);
            TxtOnDate.Text = Session["EntryDate"].ToString();
        }
    }

    protected void TxtBrID_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBrID.Text.Trim().ToString() == "0000")
            {
                txtBrName.Text = "All Branch";
                TxtOnDate.Focus();
            }
            else
            {
                txtBrName.Text = DD.GetBranchName(TxtBrID.Text);
                if (txtBrName.Text.Trim().ToString() == "")
                {
                    TxtBrID.Text = "";
                    TxtBrID.Focus();
                }
                else
                {
                    TxtOnDate.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Rdb_AccType.SelectedValue == "2")  //ashok misal
            {
                FL = "Insert";//ankita 15/09/2017
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CDRatio_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                string redirectURL = "FrmRView.aspx?Date=" + TxtOnDate.Text + "&UserName=" + Session["UserName"].ToString() + "&brcd=" + TxtBrID.Text.ToString() + "&EntryDate=" + Session["EntryDate"].ToString() + "&rptname=RptCDR.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (Rdb_AccType.SelectedValue == "1")
            {
                string redirectURL = "FrmRView.aspx?Date=" + TxtOnDate.Text + "&UserName=" + Session["UserName"].ToString() + "&brcd=" + TxtBrID.Text.ToString() + "&EntryDate=" + Session["EntryDate"].ToString() + "&rptname=RptCDRSummary.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnExit_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Rdb_AccType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}