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

public partial class FrmProposalSale : System.Web.UI.Page
{

    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCommon CMN = new ClsCommon();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsSRO SRO = new ClsSRO();
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
            TxtBRCD.Text = Session["BRCD"].ToString();
            ViewState["FLAG"] = "AD";
        }



        BindGrd();

    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        stage = SRO.GETSTAGE(TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
        if (ViewState["FLAG"].ToString() == "AD")
        {

            result = SRO.InsertProposalSale(BRCD: TxtBRCD.Text, CASE_YEAR: txtCaseY.Text, CASENO: txtCaseNo.Text,  EDATE: txtdate.Text,
                DETAILSPROERTY: txtProperty.Text, PROPETYREGNO: txthouseno.Text, FLATNO: txtplot.Text, RATEFORPERSQMETER: txtArrears.Text, Branchname: txtbranch.Text, WARDNO: txtward.Text,
                MOSTAMTPAIDNAME: txtArrearsname.Text, PUBLISHDATE: txtnewspaperDate.Text, LASTAUCTIONAMT: txtAmount.Text, AUCTIONDATE: txtDatee.Text,
                AUCTIONAMTNO: txmanjuriAmt.Text, AUCTIONAMT: txtbolliAmt.Text, MostAUCTIONAMT: txtToatalAmt.Text,
                DEFAULTERNAME: txtbolidarak.Text, ArrearnDate: txtArrearnodate.Text, MID: Session["MID"].ToString(), CID: Session["MID"].ToString(), VID: Session["MID"].ToString(), PCMAC: Session["MID"].ToString(),Font:ddlmarathi.SelectedValue.ToString());



            if (result > 0)
            {
                // result = InsertDefaulterName();

                WebMsgBox.Show("Data Saved successfully", this.Page);
                BindGrd();

                clear();


            }
        }

        if (ViewState["FLAG"].ToString() == "MD")
        {

            result = SRO.ModifyProposalSale(BRCD: TxtBRCD.Text, CASE_YEAR: txtCaseY.Text, CASENO: txtCaseNo.Text, SRO_NO: "", EDATE: txtdate.Text,
               DETAILSPROERTY: txtProperty.Text, PROPETYREGNO: txthouseno.Text, FLATNO: txtplot.Text, RATEFORPERSQMETER: txtArrears.Text, Branchname: txtbranch.Text, WARDNO: txtward.Text,
               MOSTAMTPAIDNAME: txtArrearsname.Text, PUBLISHDATE: txtnewspaperDate.Text, LASTAUCTIONAMT: txtAmount.Text, AUCTIONDATE: txtDatee.Text,
               AUCTIONAMTNO: txmanjuriAmt.Text, AUCTIONAMT: txtbolliAmt.Text, MostAUCTIONAMT: txtToatalAmt.Text,
               DEFAULTERNAME: txtbolidarak.Text, ArrearnDate: txtArrearnodate.Text,
               MID: Session["MID"].ToString(), CID: Session["MID"].ToString(), VID: Session["MID"].ToString(), PCMAC: Session["mid"].ToString(),Font:ddlmarathi.SelectedValue.ToString());

            if (result > 0)
            {
                // result = InsertDefaulterName();
                WebMsgBox.Show("Data Modify and Saved successfully", this.Page);
                BindGrd();
                clear();


            }
        }
        else if (ViewState["FLAG"].ToString() == "DL")
        {
            if (stage != "1004")
            {
                result = SRO.CancelproposalSale(BRCD: TxtBRCD.Text, CASENO: txtCaseNo.Text, CASEYEAR: txtCaseY.Text, VID: Session["MID"].ToString());
                if (result > 0)
                {
                    WebMsgBox.Show("Data Delete Successfully", this.Page);


                }
            }
            else if (stage == "1004")
            {
                WebMsgBox.Show("Data Already Delete .!!", this.Page);
            }
        }
        BindGrd();
    }
    public void clear()
    {
        TxtBRCD.Text = "";
        txtCaseY.Text = "";
        txtCaseNo.Text = "";
        txtdate.Text = "";
        txtProperty.Text = "";
        txthouseno.Text = "";
        txtplot.Text = "";
        txtArrears.Text = "";
        txtbranch.Text = "";
        txtward.Text = "";
        txtArrearsname.Text = "";
        txtnewspaperDate.Text = "";
        txtAmount.Text = "";
        txtDatee.Text = "";
        txmanjuriAmt.Text = "";
        txtbolliAmt.Text = "";
        txtToatalAmt.Text = "";
        txtbolidarak.Text = "";
        txtArrearnodate.Text = "";
    }
    protected void lnkSelect1_Click(object sender, EventArgs e)
    {

        ViewState["FLAG"] = "MD";
        LinkButton objlnk = (LinkButton)sender;
        string[] id = objlnk.CommandArgument.Split('_');
        ViewState["id"] = id[0].ToString();
        ViewState["BRCD"] = id[1].ToString();
        ViewState["CASENO"] = id[2].ToString();
        ViewState["CASE_YEAR"] = id[3].ToString();

        TxtBRCD.Text = ViewState["BRCD"].ToString();
        txtCaseNo.Text = ViewState["CASENO"].ToString();
        txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
        stage = SRO.GETSTAGE(TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
        BtnSubmit.Visible = true;
        BtnSubmit.Text = "Modify";
        ViewDetails(ViewState["BRCD"].ToString(), ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString(), ViewState["id"].ToString());
       
        BindGrd();
    }
    public void BindGrd()
    {
        try
        {
            result = SRO.BindGrdSales(grdSale, TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
            if (result > 0)
            {

                if (ViewState["FLAG"].ToString() == "")
                    BtnSubmit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkDelete1_Click(object sender, EventArgs e)
    {

    }
    public void ViewDetails(string BRCD, string CASENO, string CASE_YEAR, string id)
    {
        try
        {
            DataTable DT1 = new DataTable();
            DT = SRO.ViewDetailSale(BRCD, CASENO, CASE_YEAR, id);


            if (DT.Rows.Count > 0)
            {
                TxtBRCD.Text = DT.Rows[0]["BRCD"].ToString();
                txtCaseY.Text = DT.Rows[0]["CASE_YEAR"].ToString();
                txtCaseNo.Text = DT.Rows[0]["CASENO"].ToString();
                txtdate.Text = Convert.ToDateTime(DT.Rows[0]["EDATE"]).ToString("dd/MM/yyyy");
                txtProperty.Text = DT.Rows[0]["DETAILSPROERTY"].ToString();
                txthouseno.Text = DT.Rows[0]["PROPETYREGNO"].ToString();
                txtplot.Text = DT.Rows[0]["FLATNO"].ToString();
                txtArrears.Text = DT.Rows[0]["RATEFORPERSQMETER"].ToString();
                txtbranch.Text = DT.Rows[0]["Branchname"].ToString();
                txtward.Text = DT.Rows[0]["WARDNO"].ToString();
                txtArrearsname.Text = DT.Rows[0]["MOSTAMTPAIDNAME"].ToString();
                txtnewspaperDate.Text = Convert.ToDateTime(DT.Rows[0]["PUBLISHDATE"]).ToString("dd/MM/yyyy");
                txtAmount.Text = DT.Rows[0]["LASTAUCTIONAMT"].ToString();
                txtDatee.Text = DT.Rows[0]["AUCTIONDATE"].ToString();
                txmanjuriAmt.Text = DT.Rows[0]["AUCTIONAMTNO"].ToString();
                txtbolliAmt.Text = DT.Rows[0]["AUCTIONAMT"].ToString();
                txtToatalAmt.Text = DT.Rows[0]["MostAUCTIONAMT"].ToString();
                txtbolidarak.Text = DT.Rows[0]["DEFAULTERNAME"].ToString();
                txtArrearnodate.Text = Convert.ToDateTime(DT.Rows[0]["ArrearnDate"]).ToString("dd/MM/yyyy");

            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void grdSale_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void ddlmarathi_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmarathi.SelectedValue == "1")
        {

            TxtBRCD.Font.Name = "Shivaji01";
            TxtBRCD.Font.Size = 16;

            txtCaseY.Font.Name = "Shivaji01";
            txtCaseY.Font.Size = 16;

            txtCaseNo.Font.Name = "Shivaji01";
            txtCaseNo.Font.Size = 16;

            //txtdate.Font.Name = "Shivaji01";
            //txtdate.Font.Size = 16;

            txtProperty.Font.Name = "Shivaji01";
            txtProperty.Font.Size = 16;
            
            txthouseno.Font.Name = "Shivaji01";
            txthouseno.Font.Size = 16;

            txtplot.Font.Name = "Shivaji01";
            txtplot.Font.Size = 16;

            txtArrears.Font.Name = "Shivaji01";
            txtArrears.Font.Size = 16;

            txtbranch.Font.Name = "Shivaji01";
            txtbranch.Font.Size = 16;

            txtward.Font.Name = "Shivaji01";
            txtward.Font.Size = 16;

            txtArrearsname.Font.Name = "Shivaji01";
            txtArrearsname.Font.Size = 16;

            //txtnewspaperDate.Font.Name = "Shivaji01";
            //txtnewspaperDate.Font.Size = 16;

            txtAmount.Font.Name = "Shivaji01";
            txtAmount.Font.Size = 16;

            //txtDatee.Font.Name = "Shivaji01";
            //txtDatee.Font.Size = 16;

            txmanjuriAmt.Font.Name = "Shivaji01";
            txmanjuriAmt.Font.Size = 16;

            txtbolliAmt.Font.Name = "Shivaji01";
            txtbolliAmt.Font.Size = 16;

            txtToatalAmt.Font.Name = "Shivaji01";
            txtToatalAmt.Font.Size = 16;

            txtbolidarak.Font.Name = "Shivaji01";
            txtbolidarak.Font.Size = 16;

            //txtArrearnodate.Font.Name = "Shivaji01";
            //txtArrearnodate.Font.Size = 16;
        }
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        //int Result = CR.NoticeLogs(Session["BRCD"].ToString(), hdnid.Value, lblheader.Text, txtloancode1.Text, TXTACCNO.Text, TxtNoticeIssDt.Text.ToString(), Session["MID"].ToString(), "1");
       // string redirectURL = "FrmRView.aspx?BRCD=" + TxtBRCD.Text + "&LOANGL=" + txtloancode1.Text + "&AddFlag=" + AddFlag + "&Edate=" + TxtNoticeIssDt.Text.ToString() + "&ACCNO=" + TXTACCNO.Text + "&rptname=RPTproposalforsale.rdlc";
        string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&LOANGL=" + txtCaseY.Text + "&AddFlag=" + "" + "&Edate=" + Request.QueryString["Edate"].ToString() + "&ACCNO=" + txtCaseNo.Text + "&rptname=RPTproposalforsale.rdlc";
        
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        //Response.Redirect("FrmProposalSale.aspx?&CaseY=" + txtloancode1.Text + "&CaseNo=" + TXTACCNO.Text + "&Edate=" + TxtNoticeIssDt.Text, false);
    }
}