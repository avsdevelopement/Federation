using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmVoucherPrint : System.Web.UI.Page
{
    string FL = "";
    string bankcd = "";
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
            bankcd = ASM.GetBankcd(Session["BRCD"].ToString());
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "VouchPrnt_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            if (Rdeatils.SelectedValue == "1")
            {
                if (bankcd == "1006")
                {
                    string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&FDate=" + TxtFDate.Text + "&SetNo=" + txtsetno.Text + "&rptname=RptVoucherPrinting_Eng.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                    FL = "VVPRINT";
                }
                else
                {
                    string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&FDate=" + TxtFDate.Text + "&SetNo=" + txtsetno.Text + "&rptname=RptVoucherPrinting.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }

                FL = "VVPRINT";
                {
                    int res = VA.PrintUpdate(TxtBrID.Text, TxtFDate.Text, txtsetno.Text, FL);
                }

            }
            if (Rdeatils.SelectedValue == "2")
            {

                if (bankcd == "1006")
                {
                    string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&FDate=" + TxtFDate.Text + "&SetNo=" + txtsetno.Text + "&rptname=RptVoucherPrintingCRDR_Eng.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                    FL = "VVPRINT";
                }
                else
                {
                    string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&FDate=" + TxtFDate.Text + "&SetNo=" + txtsetno.Text + "&rptname=RptVoucherPrintingCRDR.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }


                FL = "VVPRINT";
                {
                    int res = VA.PrintUpdate(TxtBrID.Text, TxtFDate.Text, txtsetno.Text, FL);
                }
            }
            if (Rdeatils.SelectedValue == "3")
            {

                string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&FDate=" + TxtFDate.Text + "&SetNo=" + txtsetno.Text + "&rptname=RptVoucherPrintingFD.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

                FL = "VVPRINT";
                {
                    int res = VA.PrintUpdate(TxtBrID.Text, TxtFDate.Text, txtsetno.Text, FL);
                }
            }
            if (Rdeatils.SelectedValue == "4")
            {

                string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&FDate=" + TxtFDate.Text + "&SetNo=" + txtsetno.Text + "&rptname=RptVoucherPrintRetired.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

                FL = "VVPRINT";
                {
                    int res = VA.PrintUpdate(TxtBrID.Text, TxtFDate.Text, txtsetno.Text, FL);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}