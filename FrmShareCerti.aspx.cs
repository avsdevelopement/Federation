using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmShareCerti : System.Web.UI.Page
{
    CLsShareCertificate sc = new CLsShareCertificate();
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
        BindGrid();
    }

    protected void TxtAccType_TextChanged(object sender, EventArgs e)
    {
        string accname = sc.Acname(TxtBRCD.Text, TxtAccType.Text);
        if (accname == null || accname == "")
        {
            lblMessage.Text = "Please Enter Correct Account No";
            ModalPopup.Show(this.Page);
        }
        else
        {
            TxtATName.Text = accname;
        }
    }

    public void BindGrid()
    {
        try
        {
            sc.BindData(Grid_ViewSC, TxtBRCD.Text, TxtAccType.Text);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtATName_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Btn_Exit_Click(object sender, EventArgs e)
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

    protected void Report_Click(object sender, EventArgs e)
    {
        CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "ShareCerti_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        string Result = ASM.GetPrintStatus(Session["BRCD"].ToString(), TxtAccType.Text);
        if (Result == "0" || Session["UGRP"].ToString() == "1")
        {
            bankcd = ASM.GetBankcd(Session["BRCD"].ToString());
            if (bankcd == "1008")
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBRCD.Text + "&AccNo=" + TxtAccType.Text + "&rptname=RptShareCerti_Palghar.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (bankcd == "1004")// ASHOK 15/10/2017 FOR SHIVSAMARTH
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBRCD.Text + "&AccNo=" + TxtAccType.Text + "&rptname=RptShareCerti_ShivSamarth.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (bankcd == "1003") // ASHOK 07/12/2017 FOR marathwada
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBRCD.Text + "&AccNo=" + TxtAccType.Text + "&Entrydate=" + Session["EntryDate"].ToString() + "&MID=" + Session["MID"].ToString() + "&rptname=RptShareCerti_Marathwada.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

            }
            else if (bankcd == "1002") // ASHOK 20/02/2018 FOR yspm
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBRCD.Text + "&AccNo=" + TxtAccType.Text + "&Entrydate=" + Session["EntryDate"].ToString() + "&MID=" + Session["MID"].ToString() + "&rptname=RptShrYSPM.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (bankcd == "1007") // Dhanya Shetty//08/06/2018// For Ajinkyatara
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBRCD.Text + "&AccNo=" + TxtAccType.Text + "&Entrydate=" + Session["EntryDate"].ToString() + "&MID=" + Session["MID"].ToString() + "&rptname=RptShareAjinkyatara.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else
                try
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBRCD.Text + "&AccNo=" + TxtAccType.Text + "&Entrydate=" + Session["EntryDate"].ToString() + "&MID=" + Session["MID"].ToString() + "&rptname=RptShareCerti.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                catch (Exception Ex)
                {

                    ExceptionLogging.SendErrorToText(Ex);
                }
        }
        else
        {
            lblMessage.Text = " Print AllReady taken Please Login By Admin Authority........!!";
            ModalPopup.Show(this.Page);
            return;
        }
    }

    protected void lnkAddshare_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            ViewState["CERTNO"] = str.ToString();
            GetAddShareCer();
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    
    public void GetAddShareCer()
    {
        string Result = ASM.GetPrintStatusCerti(Session["BRCD"].ToString(), TxtAccType.Text, ViewState["CERTNO"].ToString());
        if (Result == "0" || Session["UGRP"].ToString() == "1")
        {
            try
            {
                bankcd = ASM.GetBankcd(Session["BRCD"].ToString());
                if (bankcd == "1002")
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBRCD.Text + "&AccNo=" + TxtAccType.Text + "&CerNo=" + ViewState["CERTNO"].ToString() + "&Entrydate=" + Session["EntryDate"].ToString() + "&MID=" + Session["MID"].ToString() + "&&rptname=RptShrYSPMAddShr.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                if (bankcd == "1001")
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBRCD.Text + "&AccNo=" + TxtAccType.Text + "&CerNo=" + ViewState["CERTNO"].ToString() + "&Entrydate=" + Session["EntryDate"].ToString() + "&MID=" + Session["MID"].ToString() + "&&rptname=RptSharesCert_SHIV.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                else if (bankcd == "1003")
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBRCD.Text + "&AccNo=" + TxtAccType.Text + "&CerNo=" + ViewState["CERTNO"].ToString() + "&Entrydate=" + Session["EntryDate"].ToString() + "&MID=" + Session["MID"].ToString() + "&rptname=RptShareCerti_MarathwadaAddshr.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }

                else if (bankcd == "1032")
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBRCD.Text + "&AccNo=" + TxtAccType.Text + "&CerNo=" + ViewState["CERTNO"].ToString() + "&Entrydate=" + Session["EntryDate"].ToString() + "&MID=" + Session["MID"].ToString() + "&rptname=RptSharesCert_GSM.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                else
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBRCD.Text + "&AccNo=" + TxtAccType.Text + "&CerNo=" + ViewState["CERTNO"].ToString() + "&Entrydate=" + Session["EntryDate"].ToString() + "&MID=" + Session["MID"].ToString() + "&rptname=RptShrYSPMAddShr.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }


            }
            catch (Exception Ex)
            {

                ExceptionLogging.SendErrorToText(Ex);
            }
        }
        else
        {
            lblMessage.Text = " Print AllReady taken Please Login By Admin Authority........!!";
            ModalPopup.Show(this.Page);
            return;
        }



    }

}