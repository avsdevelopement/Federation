using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAVSAuctionMarthi : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCommon CMN = new ClsCommon();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsSRO SRO = new ClsSRO();
    DataTable DT = new DataTable();
    ClsCaseStatus ccs = new ClsCaseStatus();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    int result = 0;
    string sroname = "", AC_Status = "", results = "", ENTRYDATE;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
            Response.Redirect("FrmLogin.aspx");
        if (!IsPostBack)
        {
            if (Request.QueryString["CaseY"] != null)
                txtCaseY.Text = Request.QueryString["CaseY"];

            if (Request.QueryString["CaseNo"] != null)
                txtCaseNO.Text = Request.QueryString["CaseNo"];

            if (Request.QueryString["Edate"] != null)
                ENTRYDATE = Request.QueryString["Edate"];

            BtnSubmit.Visible = true;
            ViewState["FLAG"] = "AD";
            BindGrdMain();



        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Clear();

    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void Clear()
    {

        txtCaseY.Text = "";
        txtCaseNO.Text = "";
        txtedate.Text = "";
        txtissueno.Text = "";
        txtIDate.Text = "";
        txtIFT.Text = "";
        txtITTM.Text = "";
        txtoFtm.Text = "";
        txtottm.Text = "";
        txtsftm.Text = "";
        txtsttm.Text = "";
        txtodate.Text = "";
        txtrsodate.Text = "";
        txtREsprice.Text = "";
        txtEaMoDe.Text = "";
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(ViewState["FLAG"]) == "AD")
            {

                result = SRO.INSERPUBLICNOTIC(ISSUERecovery: txtissueno.Text, CaseNo: txtCaseNO.Text, CASEYEAR: txtCaseY.Text, Date: txtedate.Text, InspectionDATE: txtIDate.Text, TendersDATE: txtodate.Text, SealDateDATE: txtrsodate.Text, MID: Session["MID"].ToString(), CID: Session["MID"].ToString(), VID: Session["MID"].ToString(), PCMAC: Session["MID"].ToString(), brcd: Session["brcd"].ToString(), InspectionFromTime: txtIFT.Text, InspectiontoTime: txtITTM.Text, TendersFromTiom: txtoFtm.Text, TendersTOTiom: txtottm.Text, SealDateFromTimeE: txtsftm.Text, SealDateToTimeE: txtsttm.Text, Reserve_price: txtREsprice.Text, Money_Deposit: txtEaMoDe.Text);

                // InsertDefaulterName();
                if (result > 0)
                {
                    WebMsgBox.Show("Data Saved successfully", this.Page);

                }
            }
            else
                if (Convert.ToString(ViewState["FLAG"]) == "MD")
                {

                    result = SRO.MODIFYPUBLICNOTIC(ISSUERecovery: txtissueno.Text, CaseNo: txtCaseNO.Text, CASEYEAR: txtCaseY.Text, Date: txtedate.Text, InspectionDATE: txtIDate.Text, TendersDATE: txtodate.Text, SealDateDATE: txtrsodate.Text, MID: Session["MID"].ToString(), CID: Session["MID"].ToString(), VID: Session["MID"].ToString(), PCMAC: Session["MID"].ToString(), brcd: Session["brcd"].ToString(), InspectionFromTime: txtIFT.Text, InspectiontoTime: txtITTM.Text, TendersFromTiom: txtoFtm.Text, TendersTOTiom: txtottm.Text, SealDateFromTimeE: txtsftm.Text, SealDateToTimeE: txtsttm.Text, Reserve_price: txtREsprice.Text, Money_Deposit: txtEaMoDe.Text);

                    // InsertDefaulterName();
                    if (result > 0)
                    {
                        WebMsgBox.Show("Data Modify successfully", this.Page);

                    }
                }
                else
                    if (Convert.ToString(ViewState["FLAG"]) == "DL")
                    {

                        result = SRO.DELETEPUBLICNOTIC(ISSUERecovery: txtissueno.Text, CaseNo: txtCaseNO.Text, CASEYEAR: txtCaseY.Text, Date: txtedate.Text, InspectionDATE: txtIDate.Text, TendersDATE: txtodate.Text, SealDateDATE: txtrsodate.Text, MID: Session["MID"].ToString(), CID: Session["MID"].ToString(), VID: Session["MID"].ToString(), PCMAC: Session["MID"].ToString(), brcd: Session["brcd"].ToString());

                        // InsertDefaulterName();
                        if (result > 0)
                        {
                            WebMsgBox.Show("Data Delete successfully", this.Page);

                        }
                    }
            Clear();
            BindGrdMain();

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }

    public void BindGrdMain()
    {
        try
        {
            SRO.BindPUBLICAUCNOT(GridCase, Session["BRCD"].ToString());

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void GridCase_PageIndexChanged(object sender, EventArgs e)
    {
        BindGrdMain();

    }

    public void ViewDetails(string BRCD, string CASENO, string CASE_YEAR, string ID)
    {
        try
        {
            string id = "";
            DataTable DT1 = new DataTable();
            DT = SRO.ViewDetailAUCTIONNOTCE(BRCD, CASENO, CASE_YEAR, ID);

            if (DT.Rows.Count > 0)
            {


                txtCaseY.Text = DT.Rows[0]["CASEYEAR"].ToString(); ;
                txtCaseNO.Text = DT.Rows[0]["CaseNo"].ToString(); ;
                txtedate.Text = Convert.ToDateTime(DT.Rows[0]["Date"]).ToString("dd/MM/yyyy");
                txtissueno.Text = DT.Rows[0]["ISSUERecovery"].ToString(); ;
                txtIDate.Text = Convert.ToDateTime(DT.Rows[0]["InspectionDATE"]).ToString("dd/MM/yyyy");
                txtodate.Text = Convert.ToDateTime(DT.Rows[0]["TendersDATE"]).ToString("dd/MM/yyyy");
                txtrsodate.Text = Convert.ToDateTime(DT.Rows[0]["SealDateDATE"]).ToString("dd/MM/yyyy");


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
    protected void lnkModify_Click(object sender, EventArgs e)
    {
        LinkButton objlnk = (LinkButton)sender;
        string[] id = objlnk.CommandArgument.Split('_');


        ViewState["ID"] = id[0].ToString();
        ViewState["brcd"] = id[1].ToString();
        ViewState["CaseNo"] = id[2].ToString();
        ViewState["CASEYEAR"] = id[3].ToString();
        string BRCD = ViewState["brcd"].ToString();
        txtCaseNO.Text = ViewState["CaseNo"].ToString();
        txtCaseY.Text = ViewState["CASEYEAR"].ToString();
        string srno = ViewState["ID"].ToString();
        ViewState["FLAG"] = "MD";
        BtnSubmit.Text = "MODIFY";

        ViewDetails(BRCD: BRCD, CASENO: txtCaseNO.Text, CASE_YEAR: txtCaseY.Text, ID: srno);
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        LinkButton objlnk = (LinkButton)sender;
        string[] id = objlnk.CommandArgument.Split('_');


        ViewState["ID"] = id[0].ToString();
        ViewState["brcd"] = id[1].ToString();
        ViewState["CaseNo"] = id[2].ToString();
        ViewState["CASE_YEAR"] = id[3].ToString();
        string BRCD = ViewState["brcd"].ToString();
        txtCaseNO.Text = ViewState["CaseNo"].ToString();
        txtCaseY.Text = ViewState["CASEYEAR"].ToString();
        string srno = ViewState["ID"].ToString();
        ViewState["FLAG"] = "DL";
        BtnSubmit.Text = "Delete";

        ViewDetails(BRCD: BRCD, CASENO: txtCaseNO.Text, CASE_YEAR: txtCaseY.Text, ID: srno);
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        LinkButton objlnk = (LinkButton)sender;
        string[] id = objlnk.CommandArgument.Split('_');


        ViewState["ID"] = id[0].ToString();
        ViewState["brcd"] = id[1].ToString();
        ViewState["CaseNo"] = id[2].ToString();
        ViewState["CASE_YEAR"] = id[3].ToString();
        string BRCD = ViewState["brcd"].ToString();
        txtCaseNO.Text = ViewState["CaseNo"].ToString();
        txtCaseY.Text = ViewState["CASEYEAR"].ToString();
        string srno = ViewState["ID"].ToString();
        ViewState["FLAG"] = "VW";
        BtnSubmit.Text = "Delete";
    }
    protected void BTNReport_Click(object sender, EventArgs e)
    {
        string redirectURL = "FrmRView.aspx?BRCD=" + Session["brcd"].ToString() + "&LOANGL=" + txtCaseY.Text + "&AddFlag=" + "" + "&Edate=" + txtedate.Text.ToString() + "&ACCNO=" + txtCaseNO.Text + "&rptname=RptPublicLetter_Sro.rdlc";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

    }
    protected void txtCaseNO_TextChanged(object sender, EventArgs e)
    {
        BindGrdMain();

    }
}