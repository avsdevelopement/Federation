using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmNotice_SRODetail : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsSurityDetails SD = new ClsSurityDetails();
    ClsRptSurity CR = new ClsRptSurity();
    ClsChargesMaster CM = new ClsChargesMaster();
    DataTable dt = new DataTable();
    static int chrg = 0;
    string AddressMast = "";
    string AddFlag = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            BD.BindBRANCHNAME(ddlBrName, null);
            ddlBrName.SelectedValue = Session["BRCD"].ToString();
            txtbranchcode1.Text = ddlBrName.SelectedValue.ToString();
            if (Request.QueryString["Value"].ToString() != null)
            {
                hdnid.Value = Request.QueryString["Value"].ToString();
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
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
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
            if (hdnid.Value == "g1")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, Session["ENTRYDATE"].ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptDemandNotice_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "g2")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, Session["ENTRYDATE"].ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptBeforeAttchment_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "g3")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, Session["ENTRYDATE"].ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptAttchment_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "g4")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, Session["ENTRYDATE"].ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptVisit_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "g5")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, Session["ENTRYDATE"].ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptSymbolicNotice_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "g6")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, Session["ENTRYDATE"].ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptPropertyNotice_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "g7")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, Session["ENTRYDATE"].ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptAccAttchNotice_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "g8")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, Session["ENTRYDATE"].ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptIntimationNotice_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "g9")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, Session["ENTRYDATE"].ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptProtectionNotice_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "g10")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, Session["ENTRYDATE"].ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptPossessionNotice_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "g11")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, Session["ENTRYDATE"].ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptUpsetPrizeNotice_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "g12")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, Session["ENTRYDATE"].ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptUpsetCoverletter_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "g13")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, Session["ENTRYDATE"].ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptPublicLetter_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (hdnid.Value == "g14")
            {
                int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, Session["ENTRYDATE"].ToString(), Session["MID"].ToString(), "1");
                string redirectURL = "FrmRView.aspx?BRCD=" + txtbranchcode1.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RptSushilLetter_Sro.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmNotice_SRO.aspx");
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
            btnSubmit.Focus();
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
}