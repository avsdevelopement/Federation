
using System.Data;
using System.IO;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;

public partial class FrmBankAttachmentNotice : System.Web.UI.Page

{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCommon CMN = new ClsCommon();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsSRO SRO = new ClsSRO();
    ClsRptSurity CR = new ClsRptSurity();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    int result = 0;
    int Res = 0;
    TextBox tb;
    static int i = 0;
    string sroname = "", AC_Status = "", stage = "", usrgrp = "", ENTRYDATE;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
                
                if (Request.QueryString["CaseY"] != null)
                    txtCaseY.Text = Request.QueryString["CaseY"];

                if (Request.QueryString["CaseNo"] != null)
                    txtCaseNo.Text = Request.QueryString["CaseNo"];

                if (Request.QueryString["Edate"] != null)
                    ENTRYDATE = Request.QueryString["Edate"];


                ViewState["FLAG"] = "AD";

                if (txtCaseY.Text != null & txtCaseNo.Text != null)
                {
                    ViewDetails(txtCaseY.Text, txtCaseNo.Text);
                }
            
             
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //stage = SRO.GETSTAGE(Session["BRCD"].ToString(), txtCaseNo.Text, txtCaseY.Text);
        if (ViewState["FLAG"].ToString() == "AD")
        {

            if (txtCaseY.Text == "")
            {
                WebMsgBox.Show("Please Enter Case Year.", this.Page);

            }
            if (txtCaseNo.Text == "")
            {
                WebMsgBox.Show("Please Enter Case No", this.Page);

            }
            if (txtBankd.Text == "")
            {
                WebMsgBox.Show("Please Enter Bank Designation", this.Page);

            }
            if (txtAcc.Text == "")
            {
                WebMsgBox.Show("Please Enter Account No.", this.Page);

            }
            if (txtAddress.Text == "")
            {
                WebMsgBox.Show("Please Enter Address", this.Page);

            }

            result = SRO.BankAccAttchment(CASENO: txtCaseNo.Text, CASEYEAR: txtCaseY.Text,ToBankDesignation:txtBankd.Text, BAnkAddress:txtAddress.Text, BankAcNo:txtAcc.Text);

            if (result > 0)
            {
                // result = InsertDefaulterName();

                WebMsgBox.Show("Data Saved successfully", this.Page);
                clear();
               

            }
        }

    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmBlank.aspx");
    }
    public void clear()
    {
      //  txtCaseNo.Text = "";
       // txtCaseY.Text = "";
        txtBankd.Text = "";
        txtAddress.Text = "";
        txtAcc.Text = "";
    }
    public void ViewDetails(string CaseY ,string CaseNo)
    {
        try
        {
            DataTable DT1 = new DataTable();
            DT = SRO.ViewDetailsBank(CASE_YEAR: txtCaseY.Text, CASENO: txtCaseNo.Text);
            if (DT.Rows.Count > 0)
            {
                txtBankd.Text = DT.Rows[0]["ToBankDesignation"].ToString();
                txtAddress.Text = DT.Rows[0]["BAnkAddress"].ToString();
                txtAcc.Text = DT.Rows[0]["BankAcNo"].ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
     //   int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, Session["ENTRYDATE"].ToString(), Session["MID"].ToString(), "1");
        string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&LOANGL=" + txtCaseY.Text + "&AddFlag=" + "" + "&Edate=" + Request.QueryString["Edate"].ToString() + "&ACCNO=" + txtCaseNo.Text + "&rptname=RptAccAttchNotice_Sro.rdlc";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
    }
}