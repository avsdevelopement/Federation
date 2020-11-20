using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AjaxControlToolkit;
using System.IO;
using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;
using Microsoft.Reporting.WebForms;

public partial class FrmNotice_SRO : System.Web.UI.Page
{

    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsSurityDetails SD = new ClsSurityDetails();
    ClsRptSurity CR = new ClsRptSurity();
    ClsChargesMaster CM = new ClsChargesMaster();
    DataTable dt = new DataTable();
    ClsSRO SRO = new ClsSRO();
    ClsNotice_SRO clssro=new ClsNotice_SRO();
    static int chrg = 0;
    string AddressMast = "";
    string AddFlag = "";
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //   TxtNoticeIssDt.Text = Session["EntryDate"].ToString();
        if (Session["UserName"] == null)
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }

                ddlBrName.SelectedValue = Session["BRCD"].ToString();
                txtbranchcode1.Text = ddlBrName.SelectedValue.ToString();
                if (Request.QueryString["Value"].ToString() != null)
                {
                    hdnid.Value = Request.QueryString["Value"].ToString();
                }
                if (Request.QueryString["brcd"].ToString() != null)
                {
                    txtbranchcode1.Text = Request.QueryString["brcd"].ToString();
                }
                if (Request.QueryString["caseno"].ToString() != null)
                {
                    txtloancode1.Text = Request.QueryString["caseno"].ToString();
                }
                if (Request.QueryString["caseyear"].ToString() != null)
                {
                    TXTACCNO.Text = Request.QueryString["caseyear"].ToString();
                }

                BindHeader();


            }
        }
    }
    public void BindHeader()
    {
        if (hdnid.Value == "g1")
        {
            lblheader.Text = "DEMAND NOTICE";
        }
        else if (hdnid.Value == "g2")
        {
            lblheader.Text = "NOTICE BEFORE ATTACHMENT";
        }
        else if (hdnid.Value == "g3")
        {
            lblheader.Text = "ATTACHEMENT NOTICE";
        }
        else if (hdnid.Value == "g4")
        {
            lblheader.Text = "VISIT NOTICE";
        }
        else if (hdnid.Value == "g5")
        {
            lblheader.Text = "SYMBOLIC ATTACHMENT NOTICE";
        }
        else if (hdnid.Value == "g6")
        {
            lblheader.Text = "PROPERTY ATTACHMENT ORDER";
        }
        else if (hdnid.Value == "g7")
        {
            lblheader.Text = "AC ATTACHMENT ORDER";
        }
        else if (hdnid.Value == "g8")
        {
            lblheader.Text = "INTIMATION OF VALUATION LETTER";
        }
        else if (hdnid.Value == "g9")
        {
            lblheader.Text = "POLICE PROTECTION LETTER";
        }
        else if (hdnid.Value == "g10")
        {
            lblheader.Text = "POSSESSION NOTICE";
        }
        else if (hdnid.Value == "g11")
        {
            lblheader.Text = "UPSET PRICE PROPOSAL";
        }
        else if (hdnid.Value == "g12")
        {
            lblheader.Text = "UPSET PRICE COVERING LETTER";
        }
        else if (hdnid.Value == "g13")
        {
            lblheader.Text = "PUBLIC NOTICE";
        }
        else if (hdnid.Value == "g14")
        {
            lblheader.Text = "SUSHIL SAMEER";
        }
        //   reportGenerate();
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmBlank.aspx");
    }
    protected void ddlBrName_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtbranchcode1.Text = ddlBrName.SelectedValue.ToString();
    }
    protected void txtloancode1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TXTACCNO.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TXTACCNO_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TXTACCNO.Text != "")
            {
                string bcase = SRO.GetCase(CaseNo: TXTACCNO.Text, CaseYear: txtloancode1.Text);
               


                if (bcase != null)
                {
                    SRO.BindNotice(GrdNotice, TXTACCNO.Text, txtloancode1.Text);
                    string visitdate = SRO.GetCasevisti(CaseNo: TXTACCNO.Text, CaseYear: txtloancode1.Text);
                    string symbildate = SRO.GetCasesymbolic(CaseNo: TXTACCNO.Text, CaseYear: txtloancode1.Text);
                    TXTDATE.Text = Convert.ToDateTime(visitdate).ToString("dd/MM/yyyy");
                    TXTSD.Text = Convert.ToDateTime(symbildate).ToString("dd/MM/yyyy");
                }
                else
                {
                    WebMsgBox.Show("Enter valid CaseNo And CaseYear!", this.Page);

                }
            }
            else
            {
                WebMsgBox.Show("Enter CaseNo!", this.Page);

            }


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void RbnAdd1_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            AddFlag = "1";
            AddressMast = CR.GetAddressSelected(AddFlag, txtbranchcode1.Text, txtloancode1.Text, TXTACCNO.Text);
            TXtShowAdd.Text = AddressMast.ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void RbnAdd2_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            AddFlag = "2";
            AddressMast = CR.GetAddressSelected(AddFlag, txtbranchcode1.Text, txtloancode1.Text, TXTACCNO.Text);
            TXtShowAdd.Text = AddressMast.ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void RbnAdd3_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            AddFlag = "3";
            AddressMast = CR.GetAddressSelected(AddFlag, txtbranchcode1.Text, txtloancode1.Text, TXTACCNO.Text);
            TXtShowAdd.Text = AddressMast.ToString();
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void reportGenerate(string ID)
    {
        try
        {
            hdnid.Value = ID;
            if (chrg == 1)
            {
                //CM.InsertNoticeLog(txtbranchcode1.Text, txtloancode1.Text, TXTACCNO.Text, Session["EntryDate"].ToString(), "0", Session["MID"].ToString());
                chrg = 0;
            }
            //dt = CR.GetCustData(txtbranchcode1.Text, TXTACCNO.Text, txtloancode1.Text);
            //ViewState["CustName"] = dt.Rows[0]["custname"].ToString();
            //ViewState["FLAT_ROOMNO"] = dt.Rows[0]["FLAT_ROOMNO"].ToString();
            //ViewState["custno"] = CR.Getcustno(txtbranchcode1.Text, TXTACCNO.Text, txtloancode1.Text);
            if (RbnAdd1.Checked == true)
            {
                AddFlag = "1";
            }
            else if (RbnAdd2.Checked == true)
            {
                AddFlag = "2";
            }
            else if (RbnAdd3.Checked == true)
            {
                AddFlag = "3";
            }
            if (hdnid.Value == "1")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptDemandNotice_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "2")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptBeforeAttchment_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "3")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptAttchment_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "4")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&Edate1=" + TXTDATE.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptVisit_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                TxtNoticeIssDt.Text = "";
            }
            else if (hdnid.Value == "5")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&Edate1=" + TXTSD.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptSymbolicNotice_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "6")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptPropertyNotice_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "7")
            {
                //int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, Session["ENTRYDATE"].ToString(), Session["MID"].ToString(), "1");
                //string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptAccAttchNotice_Sro.rdlc";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                Response.Redirect("FrmBankAttachmentNotice.aspx?&CaseY=" + txtloancode1.Text + "&CaseNo=" + TXTACCNO.Text + "&Edate=" + TxtNoticeIssDt.Text, false);
            }
            else if (hdnid.Value == "8")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptIntimationNotice_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "9")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptProtectionNotice_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "10")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptPossessionNotice_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "11")
            {
                //    int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                //    string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptUpsetPrizeNotice_Sro.rdlc";
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                //
                Response.Redirect("AVSUPSETP.aspx?&CaseY=" + txtloancode1.Text + "&CaseNo=" + TXTACCNO.Text + "&Edate=" + TxtNoticeIssDt.Text, false);

            }
            else if (hdnid.Value == "12")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptUpsetCoverletter_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "13")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptPublicLetterNotice_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "14")
            {
            //    int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
            //    string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptPublicLetter_Sro.rdlc";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                Response.Redirect("FrmAVSPublicNotice.aspx?&CaseY=" + txtloancode1.Text + "&CaseNo=" + TXTACCNO.Text + "&Edate=" + TxtNoticeIssDt.Text, false);

            }
            else if (hdnid.Value == "15")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptAuction_BLetter.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "16")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptIntimation_Cheque.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "17")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptIntimation_ToSocity.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "18")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptExecutionChargesLetter_ToSocity.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "19")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=Rpt31Remainder_Notice.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }

            else if (hdnid.Value == "20")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptFinalIntimationLetter.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "22")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptTenderForm_Notice.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "21")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptTermCondition_Notice.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "23")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptAutionMarathi.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "24")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RPTPublicNotice(E_M).rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "25")
            {
            ////    int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
            ////    string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RPTproposalforsale.rdlc";
            ////    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                Response.Redirect("FrmProposalSale.aspx?&CaseY=" + txtloancode1.Text + "&CaseNo=" + TXTACCNO.Text + "&Edate=" + TxtNoticeIssDt.Text, false);
          
            
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void GrdNotice_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "VW";
            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');
            ViewState["ID"] = id[0].ToString();
            ViewState["LASTNOTICEDATE"] = id[1].ToString();
            string date;
            if (TxtNoticeIssDt.Text == "")
            {
                date = ViewState["LASTNOTICEDATE"].ToString();
                TxtNoticeIssDt.Text = Convert.ToDateTime(date).ToString("dd/MM/yyyy");


            }
            else
            {
                TxtNoticeIssDt.Text = TxtNoticeIssDt.Text;
            }

            reportGenerate(ViewState["ID"].ToString());

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BTNADDDATE_Click(object sender, EventArgs e)
    {
        DIVVISIT.Visible = true;
        string visitdate = SRO.GetCasevisti(CaseNo: TXTACCNO.Text, CaseYear: txtloancode1.Text);
        string symbildate = SRO.GetCasesymbolic(CaseNo: TXTACCNO.Text, CaseYear: txtloancode1.Text);
        TXTDATE.Text = Convert.ToDateTime(visitdate).ToShortDateString();//("dd/MM/yyyy");
        TXTSD.Text = Convert.ToDateTime(symbildate).ToShortDateString();//ToString("dd/MM/yyyy");
        DIVVISIT.Visible = true;

    }

    protected void DatePopup_Click(object sender, System.EventArgs e)
    {
        Divdate.Visible = true;
        btnUpdate.Visible = true;
        btnUpdate.Visible = true;
        BTNADDDATE.Visible = false;
        ViewState["FLAG"] = "VW";
        LinkButton objlnk = (LinkButton)sender;
        string[] id = objlnk.CommandArgument.Split('_');
        ViewState["ID"] = id[0].ToString();
        ViewState["LASTNOTICEDATE"] = id[1].ToString();
        hdn.Value = ViewState["ID"].ToString();
        string date;
        if (TxtNoticeIssDt.Text == "")
        {
            date = ViewState["LASTNOTICEDATE"].ToString();
            TxtNoticeIssDt.Text = Convert.ToDateTime(date).ToString("yyyy/MM/dd");


        }
        else
        {
            TxtNoticeIssDt.Text = TxtNoticeIssDt.Text;
        }

        dt = SRO.ShowNoticeDetails(TXTACCNO.Text, txtloancode1.Text);
        txtloancode1.Text = dt.Rows[0]["CASE_YEAR"].ToString();
        TXTACCNO.Text = dt.Rows[0]["CASENO"].ToString();

    }
    protected void btnUpdate_Click(object sender, System.EventArgs e)
    {
        try
        {
            Button obj = (Button)sender;
            string[] id = obj.CommandArgument.Split('_');
            ViewState["ID"] = id[0].ToString();
            string noticedid = hdn.Value;
            if (txtdatechange.Text == "")
            {
                WebMsgBox.Show("please enter date", this.Page);
            }
            int master = SRO.ADDdate(noticedid, TxtNoticeIssDt.Text, TXTACCNO.Text, txtloancode1.Text);
            int updatedate = SRO.newDate(txtdatechange.Text, noticedid, TxtNoticeIssDt.Text, TXTACCNO.Text, txtloancode1.Text);

            WebMsgBox.Show("Updated Successfully", this.Page);

            Divdate.Visible = false;
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void wordfile_Click(object sender, System.EventArgs e)
    {
        try
        {
            LinkButton obj = (LinkButton)sender;
            string[] id = obj.CommandArgument.Split('_');
            ViewState["ID"] = id[0].ToString();
            ViewState["LASTNOTICEDATE"] = id[1].ToString();
            //ViewState["Download"] = id[1].ToString();
           
            hdn.Value = ViewState["ID"].ToString();
            string date;
            if (TxtNoticeIssDt.Text == "")
            {
                date = ViewState["LASTNOTICEDATE"].ToString();
                TxtNoticeIssDt.Text = Convert.ToDateTime(date).ToString("dd/MM/yyyy");
            }
            else
            {
                TxtNoticeIssDt.Text = TxtNoticeIssDt.Text;
            }

            reportGenerateWord(ViewState["ID"].ToString());

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void reportGenerateWord(string ID)
    {
        try
        {
            hdnid.Value = ID;
           // hdn.Value = Download;
            if (chrg == 1)
            {
                chrg = 0;
            }

            if (RbnAdd1.Checked == true)
            {
                AddFlag = "1";
            }
            else if (RbnAdd2.Checked == true)
            {
                AddFlag = "2";
            }
            else if (RbnAdd3.Checked == true)
            {
                AddFlag = "3";
            }
            if (hdnid.Value == "1")//
            {
                
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptDemandNotice_Sroword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "2")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptBeforeAttchment_Sroword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "3")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptAttchment_Sroword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "4")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&Edate1=" + TXTDATE.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptVisit_Sroword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                TxtNoticeIssDt.Text = "";
            }
            else if (hdnid.Value == "5")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&Edate1=" + TXTSD.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptSymbolicNotice_Sroword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "6")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptPropertyNotice_Sroword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "7")//
            {
                //int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, Session["ENTRYDATE"].ToString(), Session["MID"].ToString(), "1");
                //string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptAccAttchNotice_Sro.rdlc";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                Response.Redirect("FrmBankAttachmentNotice.aspx?&CaseY=" + txtloancode1.Text + "&CaseNo=" + TXTACCNO.Text + "&Edate=" + TxtNoticeIssDt.Text, false);
            }
            else if (hdnid.Value == "8")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptIntimationNotice_Sroword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "9")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptProtectionNotice_Sroword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "10")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptPossessionNotice_Sroword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "11")
            {
                //    int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                //    string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptUpsetPrizeNotice_Sro.rdlc";
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                //
                Response.Redirect("AVSUPSETP.aspx?&CaseY=" + txtloancode1.Text + "&CaseNo=" + TXTACCNO.Text + "&Edate=" + TxtNoticeIssDt.Text, false);

            }
            else if (hdnid.Value == "12")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptUpsetCoverletter_Sroword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "13")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptPublicLetterNotice_Sroword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "14")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptPublicLetter_Sroword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "15")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptAuction_BLetterword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "16")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptIntimation_Chequeword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "17")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptIntimation_ToSocityword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "18")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptExecutionChargesLetter_ToSocityword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "19")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=Rpt31Remainder_Noticeword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }

            else if (hdnid.Value == "20")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptFinalIntimationLetterword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "22")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptTenderForm_Noticeword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "21")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptTermCondition_Noticeword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "23")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptAutionMarathiword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "24")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RPTPublicNotice(E_M)word.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "25")//
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RPTproposalforsaleword.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }


        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

}
