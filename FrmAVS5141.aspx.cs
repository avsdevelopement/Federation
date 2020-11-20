using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAVS5141 : System.Web.UI.Page
{
    //CLsShareCertificate sc = new CLsShareCertificate();
    clsSanchitCertificate sc = new clsSanchitCertificate();
    ClsBindBrDetails ASM = new ClsBindBrDetails();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    string bankcd;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            TxtBRCD.Text = Session["BRCD"].ToString();
            string Brname = sc.getBRCDname(Session["BRCD"].ToString());
            TxtBrname.Text = Brname;
        }

    }

    
    protected void Submit_Click(object sender, EventArgs e)
    {

    }
    protected void Report_Click(object sender, EventArgs e)
    {
        CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "ShareCerti_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        string Result = ASM.GetPrintStatus(Session["BRCD"].ToString(), TxtAccType.Text);
        if (Result == "0" || Session["UGRP"].ToString() == "1")
        {
            bankcd = ASM.GetBankcd(Session["BRCD"].ToString());
            string redirectURL = "FrmRView.aspx?BRCD=" + TxtBRCD.Text + "&AccNo=" + TxtAccType.Text + "&rptname=RptSanchitTZMP.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }


    }
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {

    }
    protected void TxtAccType_TextChanged(object sender, EventArgs e)
    {
        string accname = sc.Acname(TxtBRCD.Text, TxtAccType.Text);
        if (accname == null || accname == "")
        {
            lblMessage.Text = "please Enter Correct Account No";
            ModalPopup.Show(this.Page);
        }
        else
        {
            TxtATName.Text = accname;
        }
    }


    protected void TxtATName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtBRCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string Brname = sc.getBRCDname(TxtBRCD.Text);
            TxtBrname.Text = Brname;
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }


}