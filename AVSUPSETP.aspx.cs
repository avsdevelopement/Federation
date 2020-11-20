
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

public partial class AVSUPSETP : System.Web.UI.Page
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
            //BD.BindWard(DropDownList1);
            //BD.BindDesig(ddlDesignation);
            //BD.BindRCNO(ddlRCNo);
            
        }
        if (ViewState["FLAG"].ToString() == "AD")
        {
            BD.BindWard(DropDownList1);
            BD.BindDesig(ddlDesignation);
            BD.BindRCNO(ddlRCNo1);
            lbl101Rc.Text = "101 वसुली दाखलाचा क्र";
            lbl101rcd.Text = "101 वसुली दाखलाची तारीख";
            
            txtArrearno.Text = SRO.MarathiDefaulterNo(CASENO: txtCaseNo.Text, CASE_YEAR: txtCaseY.Text);

            BindGrdDefaulter();
            BindGrdCommitee();
             
            BindGrdUPSET();
            BindGrdUPSETPRIZ();
        }
    }
    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void TxtBRCD_TextChanged(object sender, EventArgs e)
    {

    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        stage = SRO.GETSTAGE(TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
        if (ViewState["FLAG"].ToString() == "AD")
        {

            result = SRO.InsertDETUPSETNOTICE(BRCD: TxtBRCD.Text, CASE_YEAR: txtCaseY.Text, CASENO: txtCaseNo.Text, MEMBERNO: "", PRDCD: "", SRO_NO: "", EDATE: txtdate.Text, DESIGNATION: ddlDesignation.SelectedValue.ToString(), WARD: DropDownList1.SelectedValue.ToString(), ADDRESS: txtaddreess.Text,
                SROORDERREFNO: TxtSRNO.Text, SROORDERDATE: txtSROORDERDATE.Text, AREAOFPROPERTY: txtarea.Text, ATTACHEDPROPERTYTITLE: txtATTACHED.Text, VALUATIONDATE: txtVALUATIONDATE.Text, VALUERNAME: txtVALUERNAME.Text, REGNO: TXTrEGNO.Text, REGDATE: TXTREGNDATE.Text,
                MARKETVALUE: TXTMARKET.Text, FAIRMARKETVALUE: TXTFAIRMARKETVALUE.Text, SUBREGVALUE: TXTSUBREG.Text, CONCERNSUBREGDESIGATION: DDLCCONCERNSUB.Text.ToString(), SRNO: TXTRSRNO.Text, YEAR: TXTYEAR.Text, DETAILSPROERTY: TXTDETAILSP.Text, DPREGNO: TXTREGNOPR.Text, PRICE: TXTPRICE.Text,
                RATEFORPERSQMETER: TXTRATE.Text, MID: Session["MID"].ToString(), CID: Session["MID"].ToString(), VID: Session["MID"].ToString(), PCMAC: Session["mid"].ToString(),
                BranchName: txtBranchname.Text, RecoveryOfficeNo: txtrecoveryoffNo.Text, Rec_OfficerName: txtrecoveryOffName.Text, RecOfficerCastNo: txtRecOffCastno.Text, RecoveryAdmissionType: ddlRCNo1.SelectedValue, RecoveryAdmissionNO: txtAdmisnNo.Text, RecoveryAdmissionDate: txtadmisnDate.Text,
                CertificateDate98: txtcertificateDate.Text, WardName: txtWardName.Text, WardAddress: txtaddreess.Text, AwardAmount: txtawardAmt.Text, PrincipalAmount: txtprinciAmt.Text, InterestAmount: txtInterestAmt.Text, FromInterestDate: txtFromInterestdate.Text, TotalRecoveryAmount: txtToatalAmt.Text, PropertyType: txtpropType.Text, HouseNo: txthouseno.Text, FlatNo: txtFlatNo.Text, Arrearsno: txtArrearno.Text, Arrearsname: txtArrname.Text, ArrearsAddress: txtAddress.Text, PostalAddress: txtPostalAddress.Text,
                BusinessAddress: txtbusinessAdd.Text, OrderofPossessionDate: txtOrderPo.Text, OfficerType: txtType.Text, officerHouseno: txtHouseNoOfficer.Text, officerFlat: txtFlat.Text, officerDesignation: txtDesignation.Text, officerAddress: txtAddressOfficer.Text, officerName: txtName.Text,Font:ddlmarathi.SelectedValue.ToString());

            if (result > 0)
            {
                // result = InsertDefaulterName();

                WebMsgBox.Show("Data Saved successfully", this.Page);
                BindGrdUPSET();
                clear();


            }
        }

        if (ViewState["FLAG"].ToString() == "MD")
        {

            result = SRO.ModifyDETUPSETNOTICE(BRCD: TxtBRCD.Text, CASE_YEAR: txtCaseY.Text, CASENO: txtCaseNo.Text, MEMBERNO: "", PRDCD: "", SRO_NO: "", EDATE: txtdate.Text, DESIGNATION: ddlDesignation.SelectedValue.ToString(), WARD: DropDownList1.SelectedValue.ToString(), ADDRESS: txtaddreess.Text,
                SROORDERREFNO: TxtSRNO.Text, SROORDERDATE: txtSROORDERDATE.Text, AREAOFPROPERTY: txtarea.Text, ATTACHEDPROPERTYTITLE: txtATTACHED.Text, VALUATIONDATE: txtVALUATIONDATE.Text, VALUERNAME: txtVALUERNAME.Text, REGNO: TXTrEGNO.Text, REGDATE: TXTREGNDATE.Text,
                MARKETVALUE: TXTMARKET.Text, FAIRMARKETVALUE: TXTFAIRMARKETVALUE.Text, SUBREGVALUE: TXTSUBREG.Text, CONCERNSUBREGDESIGATION: DDLCCONCERNSUB.Text.ToString(), SRNO: TXTRSRNO.Text, YEAR: TXTYEAR.Text, DETAILSPROERTY: TXTDETAILSP.Text, DPREGNO: TXTREGNOPR.Text, PRICE: TXTPRICE.Text,
                RATEFORPERSQMETER: TXTRATE.Text, MID: Session["MID"].ToString(), CID: Session["MID"].ToString(), VID: Session["MID"].ToString(), PCMAC: Session["mid"].ToString(),
                 BranchName: txtBranchname.Text, RecoveryOfficeNo: txtrecoveryoffNo.Text, Rec_OfficerName: txtrecoveryOffName.Text, RecOfficerCastNo: txtRecOffCastno.Text, RecoveryAdmissionType: ddlRCNo1.SelectedValue, RecoveryAdmissionNO: txtAdmisnNo.Text, RecoveryAdmissionDate: txtadmisnDate.Text,
                CertificateDate98: txtcertificateDate.Text, WardName: txtWardName.Text, WardAddress: txtaddreess.Text, AwardAmount: txtawardAmt.Text, PrincipalAmount: txtprinciAmt.Text, InterestAmount: txtInterestAmt.Text, FromInterestDate: txtFromInterestdate.Text, TotalRecoveryAmount: txtToatalAmt.Text, PropertyType: txtpropType.Text, HouseNo: txthouseno.Text, FlatNo: txtFlatNo.Text, Arrearsno: txtArrearno.Text, Arrearsname: txtArrname.Text, ArrearsAddress: txtAddress.Text, PostalAddress: txtPostalAddress.Text,
                BusinessAddress: txtbusinessAdd.Text, OrderofPossessionDate: txtOrderPo.Text, OfficerType: txtType.Text, officerHouseno: txtHouseNoOfficer.Text, officerFlat: txtFlat.Text, officerDesignation: txtDesignation.Text, officerAddress: txtAddressOfficer.Text, officerName: txtName.Text,Font:ddlmarathi.SelectedValue.ToString());

            if (result > 0)
            {
                // result = InsertDefaulterName();

                WebMsgBox.Show("Data Modify and Saved successfully", this.Page);
                BindGrdUPSET();
                clear();


            }
        }
        else if (ViewState["FLAG"].ToString() == "CA")
        {
            if (stage != "1004")
            {
                result = SRO.CancelUPSETNOTICE(BRCD: TxtBRCD.Text, CASENO: txtCaseNo.Text, CASEYEAR: txtCaseY.Text, SRO_NO: TxtSRNO.Text, VID: Session["MID"].ToString());
                if (result > 0)
                {
                    WebMsgBox.Show("Data Delete Successfully", this.Page);

                    BindGrdUPSET();

                    clear();
                }
            }
            else if (stage == "1004")
            {
                WebMsgBox.Show("Data Already Delete .!!", this.Page);
            }
        }
        BindGrdUPSET();
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {

    }

    public void clear()
    {


        TxtBRCD.Text = "";
        txtCaseY.Text = "";
        txtCaseNo.Text = "";
        txtdate.Text = "";
        ddlDesignation.SelectedValue = "0";
        DropDownList1.SelectedValue = "0";
        txtaddreess.Text = "";
        TxtSRNO.Text = "";
        txtSROORDERDATE.Text = "";
        txtarea.Text = "";
        txtATTACHED.Text = "";
        txtVALUATIONDATE.Text = "";
        txtVALUERNAME.Text = "";
        TXTrEGNO.Text = "";
        TXTREGNDATE.Text = "";

        TXTMARKET.Text = "";
        TXTFAIRMARKETVALUE.Text = "";
        TXTSUBREG.Text = "";
        DDLCCONCERNSUB.Text = "";
        TXTRSRNO.Text = "";
        TXTYEAR.Text = "";
        TXTDETAILSP.Text = "";
        TXTREGNOPR.Text = "";
        TXTPRICE.Text = "";
        TXTRATE.Text = "";
        txtBranchname.Text = "";
        txtrecoveryoffNo.Text = "";
        txtrecoveryOffName.Text = "";
        txtRecOffCastno.Text = "";
       // ddlRCNo1.SelectedValue = "0";
        txtAdmisnNo.Text = "";
        txtadmisnDate.Text = "";
        txtcertificateDate.Text = "";
        txtWardName.Text = "";
        txtaddreess.Text = "";
        txtawardAmt.Text = "";
        txtprinciAmt.Text = "";
        txtInterestAmt.Text = "";
        txtFromInterestdate.Text = "";
        txtToatalAmt.Text = "";
        txtpropType.Text = "";
        txthouseno.Text = "";
        txtFlatNo.Text = "";
        txtArrearno.Text = "";
        txtArrname.Text = "";
        txtAddress.Text = "";
        txtPostalAddress.Text = "";
        txtbusinessAdd.Text = "";
        txtOrderPo.Text = "";
        txtType.Text = "";
        txtHouseNoOfficer.Text = "";
        txtFlat.Text = "";
        txtDesignation.Text = "";
        txtAddressOfficer.Text = "";
        txtName.Text = "";

    }
    public void clear1()
    {

        TXTRSRNO.Text = "";
        TXTYEAR.Text = "";
        TXTDETAILSP.Text = "";
        TXTREGNOPR.Text = "";
        TXTPRICE.Text = "";
        TXTRATE.Text = "";


    }
    public void clear2()
    {
        txtArrearno.Text = "";
        txtArrname.Text = "";
        txtAddress.Text = "";
        txtPostalAddress.Text = "";
        txtbusinessAdd.Text = "";
        txtOrderPo.Text = "";
    }
    public void clear3()
    {
        txtDesignation.Text = "";
        
        txtName.Text = "";
        txtAddressOfficer.Text = "";
        txtType.Text = "";
        txtHouseNoOfficer.Text = "";
        txtFlat.Text = "";

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
        BindGrdUPSET();
        BindGrdUPSETPRIZ();

    }
    protected void lnkDelete1_Click(object sender, EventArgs e)
    {
        ViewState["FLAG"] = "CA";
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
        BtnSubmit.Text = "Cancel";

        ViewDetails(ViewState["BRCD"].ToString(), ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString(), ViewState["id"].ToString());
        BindGrdUPSET();
        BindGrdUPSETPRIZ();

    }
    protected void grdCommitee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGrdUPSET();
    }
    public void BindGrdUPSET()
    {
        try
        {
            result = SRO.BindGrdUPSETPZ(grdCommitee, TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
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

    public void ViewDetails(string BRCD, string CASENO, string CASE_YEAR, string id)
    {
        try
        {
            DataTable DT1 = new DataTable();
            DT = SRO.ViewDetailUPSETP(BRCD, CASENO, CASE_YEAR, id);


            if (DT.Rows.Count > 0)
            {
                TxtBRCD.Text = DT.Rows[0]["BRCD"].ToString();
                txtCaseY.Text = DT.Rows[0]["CASE_YEAR"].ToString();
                txtCaseNo.Text = DT.Rows[0]["CASENO"].ToString();
                txtdate.Text = Convert.ToDateTime(DT.Rows[0]["EDATE"]).ToString("dd/MM/yyyy");
                ddlDesignation.SelectedValue = DT.Rows[0]["DESIGNATION"].ToString();
                DropDownList1.SelectedValue = DT.Rows[0]["WARD"].ToString();
                txtaddreess.Text = DT.Rows[0]["ADDRESS"].ToString();
                TxtSRNO.Text = DT.Rows[0]["SROORDERREFNO"].ToString();
                txtSROORDERDATE.Text = Convert.ToDateTime(DT.Rows[0]["SROORDERDATE"]).ToString("dd/MM/yyyy");
                txtarea.Text = DT.Rows[0]["AREAOFPROPERTY"].ToString();
                txtATTACHED.Text = DT.Rows[0]["ATTACHEDPROPERTYTITLE"].ToString();
                txtVALUATIONDATE.Text = Convert.ToDateTime(DT.Rows[0]["VALUATIONDATE"]).ToString("dd/MM/yyyy");
                txtVALUERNAME.Text = DT.Rows[0]["VALUERNAME"].ToString();
                TXTrEGNO.Text = DT.Rows[0]["REGNO"].ToString();
                TXTREGNDATE.Text = Convert.ToDateTime(DT.Rows[0]["REGDATE"]).ToString("dd/MM/yyyy");

                TXTMARKET.Text = DT.Rows[0]["MARKETVALUE"].ToString();
                TXTFAIRMARKETVALUE.Text = DT.Rows[0]["FAIRMARKETVALUE"].ToString();
                TXTSUBREG.Text = DT.Rows[0]["SUBREGVALUE"].ToString();
                DDLCCONCERNSUB.Text = DT.Rows[0]["CONCERNSUBREGDESIGATION"].ToString();
                TXTRSRNO.Text = DT.Rows[0]["SRNO"].ToString();
                TXTYEAR.Text = DT.Rows[0]["YEAR"].ToString();
                TXTDETAILSP.Text = DT.Rows[0]["DETAILSPROERTY"].ToString();
                TXTREGNOPR.Text = DT.Rows[0]["DPREGNO"].ToString();
                TXTPRICE.Text = DT.Rows[0]["PRICE"].ToString();
                TXTRATE.Text = DT.Rows[0]["RATEFORPERSQMETER"].ToString();
                ddlmarathi.SelectMethod = DT.Rows[0]["Font"].ToString();

                txtBranchname.Text=DT.Rows[0]["BranchName"].ToString();
                txtrecoveryoffNo.Text=DT.Rows[0]["RecoveryOfficerNo"].ToString(); 
                txtrecoveryOffName.Text =DT.Rows[0]["Rec_OfficerName"].ToString();
                 txtRecOffCastno.Text =DT.Rows[0]["RecOfficerCastNo"].ToString();
                 ddlRCNo1.SelectedValue = DT.Rows[0]["RecoveryAdmissionType"].ToString();
                 txtAdmisnNo.Text=DT.Rows[0]["RecoveryAdmissionNO"].ToString();
                 txtadmisnDate.Text = Convert.ToDateTime(DT.Rows[0]["RecoveryAdmissionDate"]).ToString("dd/MM/yyyy");
                 txtcertificateDate.Text = Convert.ToDateTime(DT.Rows[0]["CertificateDate98"]).ToString("dd/MM/yyyy");
                 txtWardName.Text =DT.Rows[0]["WardName"].ToString();
               txtaddreess.Text =DT.Rows[0]["WardAddress"].ToString();
               txtawardAmt.Text =DT.Rows[0]["AwardAmount"].ToString();
                txtprinciAmt.Text =DT.Rows[0]["PrincipalAmount"].ToString();
                txtInterestAmt.Text =DT.Rows[0]["InterestAmount"].ToString();
                txtFromInterestdate.Text= Convert.ToDateTime(DT.Rows[0]["FromInterestDate"]).ToString("dd/MM/yyyy");
                txtToatalAmt.Text =DT.Rows[0]["TotalRecoveryAmount"].ToString();
                txtpropType.Text =DT.Rows[0]["PropertyType"].ToString();
                txthouseno.Text =DT.Rows[0]["HouseNo"].ToString();
                txtFlatNo.Text =DT.Rows[0]["FlatNo"].ToString();
                txtArrearno.Text =DT.Rows[0]["Arrearsno"].ToString();
                txtArrname.Text =DT.Rows[0]["Arrearsname"].ToString();
                txtAddress.Text =DT.Rows[0]["ArrearsAddress"].ToString();
                txtPostalAddress.Text =DT.Rows[0]["PostalAddress"].ToString();
                txtbusinessAdd.Text =DT.Rows[0]["BusinessAddress"].ToString();
                txtOrderPo.Text= Convert.ToDateTime(DT.Rows[0]["OrderofPossessionDate"]).ToString("dd/MM/yyyy");
                txtType.Text = DT.Rows[0]["OfficerType"].ToString();
                txtHouseNoOfficer.Text = DT.Rows[0]["officerHouseno"].ToString();
                txtFlat.Text = DT.Rows[0]["officerFlat"].ToString();
                txtDesignation.Text = DT.Rows[0]["officerDesignation"].ToString();
                txtAddressOfficer.Text = DT.Rows[0]["officerAddress"].ToString();
                txtName.Text = DT.Rows[0]["officerName"].ToString();
            }
            else
            {
                //  WebMsgBox.Show("No record found..!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    //protected void linReport_Click(object sender, EventArgs e)
    //{
    //    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBRCD.Text + "&LOANGL=" + txtCaseY.Text + "&AddFlag=" + "" + "&Edate=" + txtdate.Text.ToString() + "&ACCNO=" + txtCaseNo.Text + "&rptname=RptUpsetPrizeNotice_Sro.rdlc";
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
    //}
    //protected void ddlmarathi_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //     if (DropDownList1.SelectedValue == "0")
    //    {

    //        TxtBRCD.Font.Name = "Kruti Dev 010";
    //        TxtBRCD.Font.Size = 16;


    //        txtaddreess.Font.Name = "Kruti Dev 010";
    //        txtaddreess.Font.Size = 16;
    //        TxtSRNO.Font.Name = "Kruti Dev 010";
    //        TxtSRNO.Font.Size = 16;

    //          txtSROORDERDATE.Font.Name = "Kruti Dev 010";
    //        txtSROORDERDATE.Font.Size = 16;

    //         txtarea.Font.Name = "Kruti Dev 010";
    //        txtarea.Font.Size = 16;

    //          txtATTACHED.Font.Name = "Kruti Dev 010";
    //        txtATTACHED.Font.Size = 16;
    //           txtVALUATIONDATE.Font.Name = "Kruti Dev 010";
    //        txtVALUATIONDATE.Font.Size = 16;
    //           txtVALUERNAME.Font.Name = "Kruti Dev 010";
    //        txtVALUERNAME.Font.Size = 16;
    //          TXTrEGNO.Font.Name = "Kruti Dev 010";
    //        TXTrEGNO.Font.Size = 16;
    //         TXTREGNDATE.Font.Name = "Kruti Dev 010";
    //        TXTREGNDATE.Font.Size = 16;
    //          TXTMARKET.Font.Name = "Kruti Dev 010";
    //        TXTMARKET.Font.Size = 16;
    //          TXTFAIRMARKETVALUE.Font.Name = "Kruti Dev 010";
    //        TXTFAIRMARKETVALUE.Font.Size = 16;
    //           TXTSUBREG.Font.Name = "Kruti Dev 010";
    //        TXTSUBREG.Font.Size = 16;

    //          DDLCCONCERNSUB.Font.Name = "Kruti Dev 010";
    //        DDLCCONCERNSUB.Font.Size = 16;

    //          TXTRSRNO.Font.Name = "Kruti Dev 010";
    //        TXTRSRNO.Font.Size = 16;

    //         TXTDETAILSP.Font.Name = "Kruti Dev 010";
    //        TXTDETAILSP.Font.Size = 16;

    //         TXTREGNOPR.Font.Name = "Kruti Dev 010";
    //        TXTREGNOPR.Font.Size = 16;

    //    }
    //}

    public void BindGrdUPSETPRIZ()
    {
        try
        {
            result = SRO.BindGrdUPSETPZdata(Grdinfo, TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ViewState["FLAG"].ToString() == "AD")
        {

            result = SRO.Insertdataup(BRCD: TxtBRCD.Text, CASE_YEAR: txtCaseY.Text, CASENO: txtCaseNo.Text, EDATE: txtdate.Text, SRNO: TXTRSRNO.Text, YEAR: TXTYEAR.Text, DETAILSPROERTY: TXTDETAILSP.Text, DPREGNO: TXTREGNOPR.Text, PRICE: TXTPRICE.Text,
                RATEFORPERSQMETER: TXTRATE.Text, MID: Session["MID"].ToString(), CID: Session["MID"].ToString(), VID: Session["MID"].ToString(), PCMAC: Session["mid"].ToString());

            if (result > 0)
            {
                // result = InsertDefaulterName();

                WebMsgBox.Show("Data Saved successfully", this.Page);
                BindGrdUPSETPRIZ();
                clear1();


            }
        }
        if (ViewState["FLAG"].ToString() == "UMD")
        {

            result = SRO.ModifyDETUPSETNOTICEDET(BRCD: TxtBRCD.Text, CASE_YEAR: txtCaseY.Text, CASENO: txtCaseNo.Text, EDATE: txtdate.Text, SRNO: TXTRSRNO.Text, YEAR: TXTYEAR.Text, DETAILSPROERTY: TXTDETAILSP.Text, DPREGNO: TXTREGNOPR.Text, PRICE: TXTPRICE.Text,
                RATEFORPERSQMETER: TXTRATE.Text, MID: Session["MID"].ToString(), CID: Session["MID"].ToString(), VID: Session["MID"].ToString(), PCMAC: Session["mid"].ToString());

            if (result > 0)
            {
                // result = InsertDefaulterName();

                WebMsgBox.Show("Data Modify and Saved successfully", this.Page);
                BindGrdUPSET();
                clear1();


            }
        }

    }
    protected void Grdinfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGrdUPSETPRIZ();
    }
    protected void lnkmd_Click(object sender, EventArgs e)
    {
        ViewState["FLAG"] = "UMD";
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
        btnAdd.Text = "Modify";

        ViewDetailsData(ViewState["BRCD"].ToString(), ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString(), ViewState["id"].ToString());
        BindGrdUPSETPRIZ();
    }
    protected void lnkdel_Click(object sender, EventArgs e)
    {
        ViewState["FLAG"] = "UCA";
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
        btnAdd.Text = "Cancel";

        ViewDetailsData(ViewState["BRCD"].ToString(), ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString(), ViewState["id"].ToString());
        SRO.CancelUPSETNOTICEdet(ViewState["BRCD"].ToString(), ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString(), ViewState["id"].ToString());
        WebMsgBox.Show("Record deleted..!!", this.Page);
        clear1();
        BindGrdUPSETPRIZ();
    }

    public void ViewDetailsData(string BRCD, string CASENO, string CASE_YEAR, string id)
    {
        try
        {
            DataTable DT1 = new DataTable();
            DT = SRO.ViewDetailUPSETPDet(BRCD, CASENO, CASE_YEAR, id);


            if (DT.Rows.Count > 0)
            {
                TXTRSRNO.Text = DT.Rows[0]["SRNO"].ToString();
                TXTYEAR.Text = DT.Rows[0]["YEAR"].ToString();
                TXTDETAILSP.Text = DT.Rows[0]["DETAILSPROERTY"].ToString();
                TXTREGNOPR.Text = DT.Rows[0]["DPREGNO"].ToString();
                TXTPRICE.Text = DT.Rows[0]["PRICE"].ToString();
                TXTRATE.Text = DT.Rows[0]["RATEFORPERSQMETER"].ToString();
            }
            else
            {
                //  WebMsgBox.Show("No record found..!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void ddlmarathi_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlmarathi.SelectedValue == "1")
        {
            TxtBRCD.Font.Name = "Shivaji01";
            TxtBRCD.Font.Size = 16;


            txtaddreess.Font.Name = "Shivaji01";
            txtaddreess.Font.Size = 16;

            TxtSRNO.Font.Name = "Shivaji01";
            TxtSRNO.Font.Size = 16;

            txtSROORDERDATE.Font.Name = "Shivaji01";
            txtSROORDERDATE.Font.Size = 16;

            txtarea.Font.Name = "Shivaji01";
            txtarea.Font.Size = 16;

            txtATTACHED.Font.Name = "Shivaji01";
            txtATTACHED.Font.Size = 16;

            txtVALUATIONDATE.Font.Name = "Shivaji01";
            txtVALUATIONDATE.Font.Size = 16;

            txtVALUERNAME.Font.Name = "Shivaji01";
            txtVALUERNAME.Font.Size = 16;

            TXTrEGNO.Font.Name = "Shivaji01";
            TXTrEGNO.Font.Size = 16;

           
            TXTREGNDATE.Font.Name = "Shivaji01";
            TXTREGNDATE.Font.Size = 16;

            TXTMARKET.Font.Name = "Shivaji01";
            TXTMARKET.Font.Size = 16;

            TXTFAIRMARKETVALUE.Font.Name = "Shivaji01";
            TXTFAIRMARKETVALUE.Font.Size = 16;

            TXTSUBREG.Font.Name = "Shivaji01";
            TXTSUBREG.Font.Size = 16;

            DDLCCONCERNSUB.Font.Name = "Shivaji01";
            DDLCCONCERNSUB.Font.Size = 16;

            TXTRSRNO.Font.Name = "Shivaji01";
            TXTRSRNO.Font.Size = 16;

            TXTDETAILSP.Font.Name = "Shivaji01";
            TXTDETAILSP.Font.Size = 16;

            TXTREGNOPR.Font.Name = "Shivaji01";
            TXTREGNOPR.Font.Size = 16;

            txtBranchname.Font.Name = "Shivaji01";
            txtBranchname.Font.Size = 16;

            txtrecoveryoffNo.Font.Name = "Shivaji01";
            txtrecoveryoffNo.Font.Size = 16;

            txtrecoveryOffName.Font.Name = "Shivaji01";
            txtrecoveryOffName.Font.Size = 16;

            txtRecOffCastno.Font.Name = "Shivaji01";
            txtRecOffCastno.Font.Size = 16;

            ddlRCNo1.Font.Name = "Shivaji01";
            ddlRCNo1.Font.Size = 16;

            txtAdmisnNo.Font.Name = "Shivaji01";
            txtAdmisnNo.Font.Size = 16;

            txtWardName.Font.Name = "Shivaji01";
            txtWardName.Font.Size = 16;

            txtaddreess.Font.Name = "Shivaji01";
            txtaddreess.Font.Size = 16;

            txtawardAmt.Font.Name = "Shivaji01";
            txtawardAmt.Font.Size = 16;

            txtprinciAmt.Font.Name = "Shivaji01";
            txtprinciAmt.Font.Size = 16;

            txtInterestAmt.Font.Name = "Shivaji01";
            txtInterestAmt.Font.Size = 16;

            txtToatalAmt.Font.Name = "Shivaji01";
            txtToatalAmt.Font.Size = 16;

            txtpropType.Font.Name = "Shivaji01";
            txtpropType.Font.Size = 16;

            txthouseno.Font.Name = "Shivaji01";
            txthouseno.Font.Size = 16;

            txtFlatNo.Font.Name = "Shivaji01";
            txtFlatNo.Font.Size = 16;

            txtArrearno.Font.Name = "Shivaji01";
            txtArrearno.Font.Size = 16;

            txtArrname.Font.Name = "Shivaji01";
            txtArrname.Font.Size = 16;

            txtAddress.Font.Name = "Shivaji01";
            txtAddress.Font.Size = 16;

            txtPostalAddress.Font.Name = "Shivaji01";
            txtPostalAddress.Font.Size = 16;

            txtOrderPo.Font.Name = "Shivaji01";
            txtOrderPo.Font.Size = 16;

            txtbusinessAdd.Font.Name = "Shivaji01";
            txtbusinessAdd.Font.Size = 16;

            txtType.Font.Name = "Shivaji01";
            txtType.Font.Size = 16;

            txtHouseNoOfficer.Font.Name = "Shivaji01";
            txtHouseNoOfficer.Font.Size = 16;

            txtFlat.Font.Name = "Shivaji01";
            txtFlat.Font.Size = 16;

            txtDesignation.Font.Name = "Shivaji01";
            txtDesignation.Font.Size = 16;

            txtAddressOfficer.Font.Name = "Shivaji01";
            txtAddressOfficer.Font.Size = 16;

            txtName.Font.Name = "Shivaji01";
            txtName.Font.Size = 16;
           
        }

    }
    protected void txtAdmsnType_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void ddlRCNo1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRCNo1.SelectedValue == "1")
        {
            lbl101Rc.Text = "101 वसुली दाखलाचा क्र";
            lbl101rcd.Text = "101 वसुली दाखलाची तारीख";

        }
        else if (ddlRCNo1.SelectedValue == "2")
        {
            lbl101Rc.Text = "154(29)वसुली दाखलाचा क्र";
            lbl101rcd.Text = "154(29)B वसुली दाखलाची तारीख";
        }
        else if (ddlRCNo1.SelectedValue.ToString() == "3")
        {
            lbl101Rc.Text = "98 वसुली दाखलाचा क्र";
            lbl101rcd.Text = "98 वसुली दाखलाची तारीख";
        }
    }
    protected void btnarea_Click(object sender, EventArgs e)
    {
        {
            result = SRO.InsertDefaulterNameMarathi(ID: txtArrearno.Text, BRCD: Session["brcd"].ToString(), SRO_NO: txtRecOffCastno.Text, NOTICE_ISS_DT: txtOrderPo.Text, STAGE: "1001", MID: Session["MID"].ToString(), CID: "", VID: "", SYSTEMDATE: Session["EntryDate"].ToString(), CASENO: txtCaseNo.Text, CASE_YEAR: txtCaseY.Text, MEMBERNO: txtrecoveryoffNo.Text, DEFAULTERNAME: txtArrname.Text.ToString(), DEFAULTPROPERTY: txtAddress.Text, CORRESPONDEADD: txtPostalAddress.Text, CASESTATUS: "", OCC_ADD: txtbusinessAdd.Text, OCC_DETAIL: "", MOBILE1: "", MOBILE2: "");

            if (result > 0)
            {
                


                WebMsgBox.Show("Defaulter  Add successfully", this.Page);
                 BindGrdDefaulter();
                clear2();
                // lstarea.Visible = true;


            }
            else
            {
                WebMsgBox.Show("Defaulter Not  Save", this.Page);
            }
        }
      
    }
    public void BindGrdDefaulter()
    {
        try
        {
            result = SRO.BindGrdDefNameMARATHI(grdDefulter, TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
            divdef.Visible = true;
            if (result > 0)
            {
              
                Div_Submit.Visible = true;
              
                   if (ViewState["FLAG"].ToString() == "")
                    BtnSubmit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnModifyd_Click(object sender, EventArgs e)
    {
        {
            result = SRO.ModifyDefulterMARATHI(BRCD: TxtBRCD.Text, CASENO: txtCaseNo.Text, CASEYEAR: txtCaseY.Text, SRO_NO: txtRecOffCastno.Text, STAGE: "1002", MEMNO: txtrecoveryoffNo.Text, CaseSts: "", VID: Session["MID"].ToString(), DEFNAME: txtArrname.Text, NOTICE_ISS_DT: "", ID: txtArrearno.Text, DEFAULTPROPERTY: txtAddress.Text, CORRESPONDENCEADDRESS: txtPostalAddress.Text, OCC_DETAIL: "", OCC_ADD: txtbusinessAdd.Text, MOBILE1: "", MOBILE2: "");
            if (result > 0)
            {


                WebMsgBox.Show("Defaulter  Modify successfully", this.Page);
                BindGrdDefaulter();
                clear1();
                btnarea.Visible = true;
                BtnModifyd.Visible = false;
                btndel.Visible = false;


            }
            else
            {
                WebMsgBox.Show("Defaulter Not  Modify", this.Page);
            }
        }
    }
    protected void btndel_Click(object sender, EventArgs e)
    {
        {

            result = SRO.DeleteDefaulterMarathi(BRCD: TxtBRCD.Text, CASENO: txtCaseNo.Text, CASEYEAR: txtCaseY.Text, ID: txtArrearno.Text);
            if (result > 0)
            {


                WebMsgBox.Show("Defaulter  Delete successfully", this.Page);
                BindGrdDefaulter();
                clear1();
                btnarea.Visible = true;
                BtnModifyd.Visible = false;
                btndel.Visible = false;

            }
            else
            {
                WebMsgBox.Show("Defaulter Not  Delete", this.Page);
            }
        }
    }
    protected void grdDefulter_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void lnkDefaulter_Click(object sender, EventArgs e)
    { try
        {

            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');
            ViewState["BRCD"] = id[0].ToString();
            ViewState["id"] = id[1].ToString();
            ViewState["CASENO"] = id[2].ToString();
            ViewState["CASE_YEAR"] = id[3].ToString();

             DataTable DT1 = new DataTable();

             DT1 = SRO.ViewDetailsDefaulterMarathi(ViewState["BRCD"].ToString(), ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString(), ViewState["id"].ToString());
                        if (DT1.Rows.Count > 0)
                        {

                           
                             txtArrearno.Text =DT1.Rows[0]["ID"].ToString();
                           txtArrname.Text=  DT1.Rows[0]["DEFAULTERNAME"].ToString();
                          
                            txtAddress.Text= DT1.Rows[0]["DEFAULTPROPERTY"].ToString();
                            txtPostalAddress.Text= DT1.Rows[0]["CORRESPONDENCEADDRESS"].ToString();
                              btnarea.Visible = false;
                            BtnModifyd.Visible = true;
                            btndel.Visible = false;
                        }                    
                       
                      
                    else
                    {
                        btnarea.Text = "ADD";
                        ViewState["LFlag"] = "AD";
                        ViewState["Flag"] = "AD";

                    }
            
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    
    protected void lnkDeldefl_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');
            ViewState["BRCD"] = id[0].ToString();
            ViewState["id"] = id[1].ToString();
            ViewState["CASENO"] = id[2].ToString();
            ViewState["CASE_YEAR"] = id[3].ToString();

            DataTable DT1 = new DataTable();

            DT1 = SRO.ViewDetailsDefaulterMarathi(ViewState["BRCD"].ToString(), ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString(), ViewState["id"].ToString());
            if (DT1.Rows.Count > 0)
            {


                txtArrearno.Text = DT1.Rows[0]["ID"].ToString();
                txtArrname.Text = DT1.Rows[0]["DEFAULTERNAME"].ToString();

                txtAddress.Text = DT1.Rows[0]["DEFAULTPROPERTY"].ToString();
                txtPostalAddress.Text = DT1.Rows[0]["CORRESPONDENCEADDRESS"].ToString();
                btnarea.Visible = false;
                BtnModifyd.Visible = false;
                btndel.Visible = true;
            }


            else
            {
                btnarea.Text = "ADD";
                ViewState["LFlag"] = "AD";
                ViewState["Flag"] = "AD";

            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void GridViewCommit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGrdCommitee();
    }
   
    protected void lnkcomm_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "MD";
            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');
            ViewState["BRCD"] = id[0].ToString();
            ViewState["ID"] = id[1].ToString();
            ViewState["CASENO"] = id[2].ToString();
            ViewState["CASE_YEAR"] = id[3].ToString();

            TxtBRCD.Text = ViewState["BRCD"].ToString();
            txtCaseNo.Text = ViewState["CASENO"].ToString();
            txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
             stage = SRO.GETSTAGE(TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
           
          
            BtnSubmit.Visible = true;
            btnmodifycomm.Visible = true;
            btnAddComm.Visible = false;
            ViewDetailsCommite(ViewState["BRCD"].ToString(), ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString(), ViewState["ID"].ToString());
            // BindGrdMain();
            BindGrdCommitee();

             //ENTF(true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void ViewDetailsCommite(string BRCD, string CASENO, string CASE_YEAR, string ID)
    {
        try
        {
            DataTable DT1 = new DataTable();
            DT = SRO.ViewDetailsCOMMIMARATHI(BRCD, CASENO, CASE_YEAR, ID);


            if (DT.Rows.Count > 0)
            {
                TxtSRNO.Text = DT.Rows[0]["SRO_NO"].ToString();
               // TXTSROName.Text = SRO.GetSROName(TxtSRNO.Text);
                //txtMember.Text = DT.Rows[0]["MEMBERNO"].ToString();
               // txtMemberName.Text = SRO.GetMemberID(txtMember.Text);
                //   txtDefaulterName.Text = DT.Rows[0]["DEFAULTERNAME"].ToString();


                txtDesignation.Text = DT.Rows[0]["DESIGNATION"].ToString();
                //  ddlPaymentMode.SelectedValue = DT.Rows[0]["PAYMENTMODE"].ToString();
                // txtAmount.Text = DT.Rows[0]["AMOUNT"].ToString();
                //  txtChequeNo.Text = DT.Rows[0]["CHEQUENO"].ToString();
                txtName.Text = DT.Rows[0]["COMM_NAME"].ToString();
                txtAddressOfficer.Text = DT.Rows[0]["COM_ADDRESS"].ToString();
                txtType.Text = DT.Rows[0]["PROPERTYTYPE"].ToString();
                txtHouseNoOfficer.Text = DT.Rows[0]["PROPERTYTYPENO"].ToString();
                txtFlat.Text = DT.Rows[0]["FloorNO"].ToString();


            }
            else
            {
                //  WebMsgBox.Show("No record found..!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnlcancom_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');
            ViewState["BRCD"] = id[0].ToString();
            ViewState["ID"] = id[1].ToString();
            ViewState["CASENO"] = id[2].ToString();
            ViewState["CASE_YEAR"] = id[3].ToString();

            TxtBRCD.Text = ViewState["BRCD"].ToString();
            txtCaseNo.Text = ViewState["CASENO"].ToString();
            txtCaseY.Text = ViewState["CASE_YEAR"].ToString();

            Div_Submit.Visible = true;
            BtnSubmit.Visible = true;
            btnmodifycomm.Visible = true;
            btnAddComm.Visible = false;
            SRO.deletecommintemarathi(ViewState["BRCD"].ToString(), ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString(), ViewState["ID"].ToString());
            WebMsgBox.Show("Data has delete!!", this.Page);
            BindGrdCommitee();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void btnmodifycomm_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["FLAG"]) == "MD")
        {

            result = SRO.ModifyCommitDetailMARATHI(BRCD: TxtBRCD.Text, CASENO: txtCaseNo.Text, CASEYAER: txtCaseY.Text, MEMNO: txtRecOffCastno.Text, SRO_NO: TxtSRNO.Text, NOTICE_ISS_DT: txtdate.Text, CaseSts: "1", MID: Session["MID"].ToString(), DESIGNATION: txtDesignation.Text.ToString(), COMM_NAME: txtName.Text, COM_MOBILE1: "", COM_MOBILE2: "9999999999", COM_ADDRESS: txtAddressOfficer.Text, PROPERTYTYPE: txtType.Text, PROPERTYTYPENO: txtHouseNoOfficer.Text, FloorNO: txtFlat.Text);
            if (result > 0)
            {


                WebMsgBox.Show("Committee  Modify successfully", this.Page);
                clear3();
                //clearCommitee();

            }
            else
            {
                WebMsgBox.Show("Committee Not  Modify", this.Page);
            }
        }
    }
    public void BindGrdCommitee()
    {
        try
        {
            result = SRO.BindGrdCommiteeMARATHI(grdCommitee, TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
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

    protected void btnAddComm_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["FLAG"]) == "AD")
        {


            result = SRO.InsertCommitDetailMARATHI(BRCD: TxtBRCD.Text, CASENO: txtCaseNo.Text, CASEYAER: txtCaseY.Text, MEMNO: txtrecoveryoffNo.Text, SRO_NO: txtRecOffCastno.Text, NOTICE_ISS_DT: txtdate.Text, CaseSts: "1", MID: Session["MID"].ToString(), DESIGNATION: txtDesignation.Text.ToString(), COMM_NAME: txtName.Text, COM_MOBILE1: "", COM_MOBILE2: "9999999999", COM_ADDRESS: txtAddressOfficer.Text, PROPERTYTYPE: txtType.Text, PROPERTYTYPENO: txtHouseNoOfficer.Text, FloorNO: txtFlat.Text);

            if (result > 0)
            {
                // result = InsertDefaulterName();

                WebMsgBox.Show("Data Saved successfully", this.Page);

             
                Div_Submit.Visible = true;
               
                BindGrdCommitee();
                FL = "Insert";//Dhanya Shetty
                clear3();
                //clearCommitee();
            }

            else
            {
                WebMsgBox.Show("Commitee Details  Not  Save", this.Page);
            }
        }
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        //string redirectURL = "FrmRView.aspx?BRCD=" + TxtBRCD.Text + "&LOANGL=" + txtCaseY.Text + "&AddFlag=" + "" + "&Edate=" + txtdate.Text.ToString() + "&ACCNO=" + txtCaseNo.Text + "&rptname=RptUpsetPrizeNotice_Sro.rdlc";
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

        string redirectURL = "FrmRView.aspx?BRCD=" + TxtBRCD.Text + "&LOANGL=" + txtCaseY.Text + "&AddFlag="+FL+"&Edate=" + txtdate.Text.ToString() + "&ACCNO=" + txtCaseNo.Text + "&rptname=RptUpsetPrizeNotice_Sro.rdlc";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
    }
}