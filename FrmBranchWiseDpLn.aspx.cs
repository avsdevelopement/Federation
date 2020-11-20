using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmBranchWiseDpLn : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
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
            //added by ankita 07/10/2017 to make user frndly
            TxtBrID.Text = Session["BRCD"].ToString();
            txtAsOnDate.Text = Session["EntryDate"].ToString();
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "BrnchWsDpLn_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            if (rbtnProdType.SelectedValue == "1")
            {
                if (cnkPrevMonth.Checked == true)
                {
                    string redirectURL = "FrmRView.aspx?BrCode=" + TxtBrID.Text + "&Type=" + Rdb_EntryType.SelectedValue + "&AsOnDate=" + txtAsOnDate.Text.Trim().ToString() + "&rptname=RptBranchWiseDP_PRCR.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                else
                {
                    string redirectURL = "FrmRView.aspx?BrCode=" + TxtBrID.Text + "&Type=" + Rdb_EntryType.SelectedValue + "&AsOnDate=" + txtAsOnDate.Text.Trim().ToString() + "&rptname=RptBranchWiseDP.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
            else if (rbtnProdType.SelectedValue == "2")
            {
                if (cnkPrevMonth.Checked == true)
                {
                    string redirectURL = "FrmRView.aspx?BrCode=" + TxtBrID.Text + "&Type=" + Rdb_EntryType.SelectedValue + "&AsOnDate=" + txtAsOnDate.Text.Trim().ToString() + "&rptname=RptBranchWiseLoanList_PrCr.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                else
                {
                    string redirectURL = "FrmRView.aspx?BrCode=" + TxtBrID.Text + "&Type=" + Rdb_EntryType.SelectedValue + "&AsOnDate=" + txtAsOnDate.Text.Trim().ToString() + "&rptname=RptBranchWiseLoanList.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
            else if (rbtnProdType.SelectedValue == "3")
            {
                if (cnkPrevMonth.Checked == true)
                {
                    string redirectURL = "FrmRView.aspx?BrCode=" + TxtBrID.Text + "&Type=" + Rdb_EntryType.SelectedValue + "&AsOnDate=" + txtAsOnDate.Text.Trim().ToString() + "&rptname=RptBranchWiseLoanDT_PrCr.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}