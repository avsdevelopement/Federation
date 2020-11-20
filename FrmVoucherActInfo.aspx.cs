using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmVoucherActInfo : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsVoucherActInfo VA = new ClsVoucherActInfo();
    ClsBindBrDetails ASM = new ClsBindBrDetails();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsEncryptValue EV = new ClsEncryptValue();
    int Result = 0;
    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            BD.BindActi(DdlActivity, Session["BRCD"].ToString());
            TxtDate.Text = Session["EntryDate"].ToString();
            TxtDate.Focus();
        }
    }

    public void ClearData()
    {
        try
        {
            DdlActivity.SelectedValue = "0";
            TxtDate.Text = "";
            TxtUserCode.Text = "";
            TxtSetNo.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            string FL = "";
            getdrcr();

            if (TxtDate.Text != "" && TxtSetNo.Text == "" && TxtUserCode.Text == "" && DdlActivity.SelectedValue == "0" && txtamount.Text == "")
                FL = "ALL";
            else if (TxtDate.Text != "" && TxtUserCode.Text != "" && TxtSetNo.Text == "" && DdlActivity.SelectedValue == "0" && txtamount.Text == "")
                FL = "SPE";
            else if (TxtDate.Text != "" && TxtSetNo.Text != "" && TxtUserCode.Text == "" && DdlActivity.SelectedValue == "0" && txtamount.Text == "")
                FL = "SET";
            else if (TxtDate.Text != "" && TxtSetNo.Text != "" && TxtUserCode.Text != "" && DdlActivity.SelectedValue == "0" && txtamount.Text == "")
                FL = "STUSR";
            else if (TxtDate.Text != "" && DdlActivity.SelectedValue != "0" && TxtSetNo.Text == "" && TxtUserCode.Text == "" && txtamount.Text == "")
                FL = "ALLACT";
            else if (TxtDate.Text != "" && DdlActivity.SelectedValue != "0" && TxtSetNo.Text == "" && TxtUserCode.Text != "" && txtamount.Text == "")
                FL = "EAU";
            else if (TxtDate.Text != "" && TxtSetNo.Text == "" && TxtUserCode.Text == "" && txtamount.Text != "")
                FL = "AMT";
            else
                FL = "EAS";

            Result = VA.GetInfo(GrdView, Session["BRCD"].ToString(), FL, TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text);
            if (Result <= 0)
            {
                WebMsgBox.Show("No Records Found ....!", this.Page);
                return;
            }
            CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "VoucheractInfo_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Btn_Clear_Click(object sender, EventArgs e)
    {
        ClearData();
    }

    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }

    protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Font.Bold = true;
        }
    }

    protected void LnkPrintReceipt_Click(object sender, EventArgs e)
    {
        try
        {
            string Para = VA.GetParameter();
            if (Para == "Y")//Dhanya Shetty
            {
                LinkButton lnkedit = (LinkButton)sender;
                string str = lnkedit.CommandArgument.ToString();
                string[] ARR = str.Split(',');
                ViewState["SUBGLCODE"] = ARR[0].ToString();
                ViewState["ACCNO"] = ARR[1].ToString();
                ViewState["SETNO"] = ARR[2].ToString();
                ViewState["CHECK_FLAG"] = "PRINT";
                FL = "VVPRINT";
                string Setno = ViewState["SETNO"].ToString();
                int res = VA.PrintUpdate(Session["BRCD"].ToString(), TxtDate.Text, Setno, FL);
            }
            else
            {
                LinkButton lnkedit = (LinkButton)sender;
                string str = lnkedit.CommandArgument.ToString();
                string[] ARR = str.Split(',');
                ViewState["SUBGLCODE"] = ARR[0].ToString();
                ViewState["ACCNO"] = ARR[1].ToString();
                ViewState["SETNO"] = ARR[2].ToString();
                ViewState["CHECK_FLAG"] = "PRINT";
                FL = "VVPRINT";
                string Setno = ViewState["SETNO"].ToString();
                int res = VA.PrintUpdate(Session["BRCD"].ToString(), TxtDate.Text, Setno, FL);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void GrdView_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string bankcd;
            bankcd = ASM.GetBankcd(Session["BRCD"].ToString());

            string ACTIVITY = (GrdView.SelectedRow.FindControl("ACTIVITY") as Label).Text;
            string FL = "", FL1 = "";
            if (ACTIVITY == "CASH-R" || ACTIVITY == "Cash-r")
                FL = "R";
            else if (ACTIVITY == "Payment" || ACTIVITY == "PAYMENT")
                FL = "V";
            else if (ACTIVITY == "TRF" || ACTIVITY == "TRANSFER")
                FL = "TRF";
            else if (ACTIVITY == "BANK-R" || ACTIVITY == "BANK-R")
                FL = "TRF";

            if (bankcd == "1001")
            {
                if (ViewState["CHECK_FLAG"].ToString() == "PRINT")
                {
                    string Setno = (GrdView.SelectedRow.FindControl("SETNO") as Label).Text;
                    string EntryDate = (GrdView.SelectedRow.FindControl("ENTRYDATE") as Label).Text;
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "VoucheractInfo_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + EntryDate + "&BRCD=" + Session["BRCD"].ToString() + "&FN=" + FL + "&rptname=RptReceiptPrint_SHIV.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
            else if (bankcd == "1002")
            {
                if (ViewState["CHECK_FLAG"].ToString() == "PRINT")
                {
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "VoucheractInfo_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    string Setno = (GrdView.SelectedRow.FindControl("SETNO") as Label).Text;
                    string EntryDate = (GrdView.SelectedRow.FindControl("ENTRYDATE") as Label).Text;
                    string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + EntryDate + "&BRCD=" + Session["BRCD"].ToString() + "&FN=" + FL + "&rptname=RptReceiptPrint.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
            else if (bankcd == "1004")
            {
                if (ViewState["CHECK_FLAG"].ToString() == "PRINT")
                {
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "VoucheractInfo_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    string Setno = (GrdView.SelectedRow.FindControl("SETNO") as Label).Text;
                    string EntryDate = (GrdView.SelectedRow.FindControl("ENTRYDATE") as Label).Text;
                    string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + EntryDate + "&BRCD=" + Session["BRCD"].ToString() + "&FN=" + FL + "&rptname=RptReceiptPrint.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
            else if (bankcd == "1007")
            {
                if (ViewState["CHECK_FLAG"].ToString() == "PRINT")
                {
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "VoucheractInfo_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    string Setno = (GrdView.SelectedRow.FindControl("SETNO") as Label).Text;
                    string EntryDate = (GrdView.SelectedRow.FindControl("ENTRYDATE") as Label).Text;
                    string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + EntryDate + "&BRCD=" + Session["BRCD"].ToString() + "&FN=" + FL + "&rptname=RptReceiptPrint_AKYT.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
            else if (bankcd == "100")
            {
                if (ViewState["CHECK_FLAG"].ToString() == "PRINT")
                {
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "VoucheractInfo_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    string Setno = (GrdView.SelectedRow.FindControl("SETNO") as Label).Text;
                    string EntryDate = (GrdView.SelectedRow.FindControl("ENTRYDATE") as Label).Text;
                    string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + EntryDate + "&BRCD=" + Session["BRCD"].ToString() + "&FN=" + FL + "&rptname=RptReceiptPrint.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
            else if (bankcd == "1008")
            {
                if (ViewState["CHECK_FLAG"].ToString() == "PRINT")
                {
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "VoucheractInfo_Print_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                    string Para = VA.GetParameter();
                    if (Para == "Y")//Dhanya Shetty
                    {
                        string Setno = (GrdView.SelectedRow.FindControl("SETNO") as Label).Text;
                        string EntryDate = (GrdView.SelectedRow.FindControl("ENTRYDATE") as Label).Text;
                        string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + EntryDate + "&BRCD=" + Session["BRCD"].ToString() + "&FN=" + FL + "&Subg=" + ViewState["SUBGLCODE"].ToString() + "&Acc=" + ViewState["ACCNO"].ToString() + "&rptname=RptReceiptPrintPal.rdlc";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                    }
                    else
                    {
                        string Setno = (GrdView.SelectedRow.FindControl("SETNO") as Label).Text;
                        string EntryDate = (GrdView.SelectedRow.FindControl("ENTRYDATE") as Label).Text;
                        string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + EntryDate + "&BRCD=" + Session["BRCD"].ToString() + "&FN=" + FL + "&rptname=RptReceiptPrint.rdlc";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                    }
                }
            }
            else if (bankcd == "1017")
            {
                if (ViewState["CHECK_FLAG"].ToString() == "PRINT")
                {
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "VoucheractInfo_Print_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                    string Para = VA.GetParameter();
                    if (Para == "Y")//Dhanya Shetty
                    {
                        string Setno = (GrdView.SelectedRow.FindControl("SETNO") as Label).Text;
                        string EntryDate = (GrdView.SelectedRow.FindControl("ENTRYDATE") as Label).Text;
                        string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + EntryDate + "&BRCD=" + Session["BRCD"].ToString() + "&FN=" + FL + "&Subg=" + ViewState["SUBGLCODE"].ToString() + "&Acc=" + ViewState["ACCNO"].ToString() + "&rptname=RptReceiptPrintsai.rdlc";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                    }
                    else
                    {
                        string Setno = (GrdView.SelectedRow.FindControl("SETNO") as Label).Text;
                        string EntryDate = (GrdView.SelectedRow.FindControl("ENTRYDATE") as Label).Text;
                        string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + EntryDate + "&BRCD=" + Session["BRCD"].ToString() + "&FN=" + FL + "&rptname=RptReceiptPrint.rdlc";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                    }
                }
            }

            // added by Prerana pawar on 11/07/2018
            else if (bankcd == "1018" )//yuva
            {
                if (ViewState["CHECK_FLAG"].ToString() == "PRINT")
                {
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "VoucheractInfo_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    string Setno = (GrdView.SelectedRow.FindControl("SETNO") as Label).Text;
                    string EntryDate = (GrdView.SelectedRow.FindControl("ENTRYDATE") as Label).Text;
                    string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + EntryDate + "&BRCD=" + Session["BRCD"].ToString() + "&FN=" + FL + "&rptname=RptReceiptPrintYuva1.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
            else if (bankcd == "1014") //bharath
            {
                if (ViewState["CHECK_FLAG"].ToString() == "PRINT")
                {
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "VoucheractInfo_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    string Setno = (GrdView.SelectedRow.FindControl("SETNO") as Label).Text;
                    string EntryDate = (GrdView.SelectedRow.FindControl("ENTRYDATE") as Label).Text;
                    string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + EntryDate + "&BRCD=" + Session["BRCD"].ToString() + "&FN=" + FL + "&rptname=RptReceiptPrintD.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
            //added by ankita on 08/08/2017  to solve issue of voucher view
            else
            {
                if (ViewState["CHECK_FLAG"].ToString() == "PRINT")
                {
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "VoucheractInfo_Print_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    string Setno = (GrdView.SelectedRow.FindControl("SETNO") as Label).Text;
                    string EntryDate = (GrdView.SelectedRow.FindControl("ENTRYDATE") as Label).Text;
                    string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + EntryDate + "&BRCD=" + Session["BRCD"].ToString() + "&FN=" + FL + "&rptname=RptReceiptPrint.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnView_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtDate.Text != "" && TxtSetNo.Text == "" && TxtUserCode.Text == "" && DdlActivity.SelectedValue == "0" && txtamount.Text == "")
            {
                TxtCR.Text = string.IsNullOrEmpty(VA.GETCRDR(Session["BRCD"].ToString(), "CRDTCANC", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text)) ? "0" : VA.GETCRDR(Session["BRCD"].ToString(), "CRDTCANC", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text);
                TxtDR.Text = string.IsNullOrEmpty(VA.GETCRDR(Session["BRCD"].ToString(), "DRDTCANC", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text)) ? "0" : VA.GETCRDR(Session["BRCD"].ToString(), "DRDTCANC", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text);
                FL = "ALLCANC";
            }
            else if (TxtDate.Text != "" && TxtSetNo.Text != "" && TxtUserCode.Text == "" && DdlActivity.SelectedValue == "0" && txtamount.Text == "")
            {
                TxtCR.Text = string.IsNullOrEmpty(VA.GETCRDR(Session["BRCD"].ToString(), "CRSETCANC", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text)) ? "0" : VA.GETCRDR(Session["BRCD"].ToString(), "CRSETCANC", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text);
                TxtDR.Text = string.IsNullOrEmpty(VA.GETCRDR(Session["BRCD"].ToString(), "DRSETCANC", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text)) ? "0" : VA.GETCRDR(Session["BRCD"].ToString(), "DRSETCANC", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text);
                FL = "SETCANC";
            }

            Result = VA.GetInfo(GrdView, Session["BRCD"].ToString(), FL, TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text);
            if (Result <= 0)
            {
                WebMsgBox.Show("No Records Found ....!", this.Page);
                return;
            }
            CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "VoucheractInfoCancelView_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void getdrcr()
    {
        try
        {
            if (TxtDate.Text != "" && TxtSetNo.Text != "")
            {
                TxtCR.Text = string.IsNullOrEmpty(VA.GETCRDR(Session["BRCD"].ToString(), "CRSET", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text)) ? "0" : VA.GETCRDR(Session["BRCD"].ToString(), "CRSET", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text);
                TxtDR.Text = string.IsNullOrEmpty(VA.GETCRDR(Session["BRCD"].ToString(), "DRSET", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text)) ? "0" : VA.GETCRDR(Session["BRCD"].ToString(), "DRSET", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text);
            }
            else if (TxtDate.Text != "" && DdlActivity.SelectedValue != "0")
            {
                TxtCR.Text = string.IsNullOrEmpty(VA.GETCRDR(Session["BRCD"].ToString(), "CRACT", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text)) ? "0" : VA.GETCRDR(Session["BRCD"].ToString(), "CRACT", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text);
                TxtDR.Text = string.IsNullOrEmpty(VA.GETCRDR(Session["BRCD"].ToString(), "DRACT", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text)) ? "0" : VA.GETCRDR(Session["BRCD"].ToString(), "DRACT", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text);
            }
            else if (TxtDate.Text != "" && TxtSetNo.Text == "" && TxtUserCode.Text == "" && txtamount.Text != "")
            {
                TxtCR.Text = string.IsNullOrEmpty(VA.GETCRDR(Session["BRCD"].ToString(), "CRAMT", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text)) ? "0" : VA.GETCRDR(Session["BRCD"].ToString(), "CRAMT", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text);
                TxtDR.Text = string.IsNullOrEmpty(VA.GETCRDR(Session["BRCD"].ToString(), "DRAMT", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text)) ? "0" : VA.GETCRDR(Session["BRCD"].ToString(), "DRAMT", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text);
            }
            else
            {
                TxtCR.Text = string.IsNullOrEmpty(VA.GETCRDR(Session["BRCD"].ToString(), "CRDT", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text)) ? "0" : VA.GETCRDR(Session["BRCD"].ToString(), "CRDT", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text);
                TxtDR.Text = string.IsNullOrEmpty(VA.GETCRDR(Session["BRCD"].ToString(), "DRDT", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text)) ? "0" : VA.GETCRDR(Session["BRCD"].ToString(), "DRDT", TxtDate.Text, DdlActivity.SelectedValue, TxtUserCode.Text.ToUpper(), Session["MID"].ToString(), TxtSetNo.Text, txtamount.Text);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

}