using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmReceiptPrintALL : System.Web.UI.Page
{   
    string FL = "";
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsVoucherActInfo VA = new ClsVoucherActInfo();
    ClsBindBrDetails ASM = new ClsBindBrDetails();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }
                TxtBrID.Text = Session["BRCD"].ToString();
                TxtFDate.Text = Session["EntryDate"].ToString();
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
            string bankcd;
            bankcd = ASM.GetBankcd(Session["BRCD"].ToString());

            string ACTIVITY = VA.GetActi(TxtBrID.Text, txtsetno.Text, TxtFDate.Text);
            string FL = "", FL1 = "";
            if (ACTIVITY == "3")
                FL = "R";
            else if (ACTIVITY == "4")
                FL = "V";
            else if (ACTIVITY == "7")
                FL = "TRF";
            else if (ACTIVITY == "5" || ACTIVITY == "6")
                FL = "TRF";
            
            if (Rdeatils.SelectedValue == "1")
            {
                string redirectURL = "FrmRView.aspx?SETNO=" + txtsetno.Text + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + TxtFDate.Text + "&BRCD=" + TxtBrID.Text + "&FN=" + FL + "&rptname=RptReceiptPrint.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (Rdeatils.SelectedValue == "2")
            {
                string redirectURL = "FrmRView.aspx?SETNO=" + txtsetno.Text + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + TxtFDate.Text + "&BRCD=" + TxtBrID.Text + "&FN=" + FL + "&rptname=RptReceiptPrint_2.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void LnkPrintReceipt_Click(object sender, EventArgs e)
    {
        string Para = VA.GetParameter();
        if (Para == "Y")//Dhanya Shetty
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["SUBGLCODE"] = ARR[0].ToString();
            ViewState["ACCNO"] = ARR[1].ToString();
            ViewState["CHECK_FLAG"] = "PRINT";
            FL = "VVPRINT";
            string Setno = (GrdView.SelectedRow.FindControl("SETNO") as Label).Text;
            int res = VA.PrintUpdate(Session["BRCD"].ToString(), TxtFDate.Text, Setno, FL);
        }
        else
        {
            ViewState["CHECK_FLAG"] = "PRINT";
            FL = "VVPRINT";
            string Setno = (GrdView.SelectedRow.FindControl("SETNO") as Label).Text;
            int res = VA.PrintUpdate(Session["BRCD"].ToString(), TxtFDate.Text, Setno, FL);
        }
    }
    protected void GrdView_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}