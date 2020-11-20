
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

public partial class frmAVS51203 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCommon CMN = new ClsCommon();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsSRO SRO = new ClsSRO();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsVoucherAutho VA = new ClsVoucherAutho();
    ClsAuthoVoucher VA1 = new ClsAuthoVoucher();
    ClsAuthorized AT = new ClsAuthorized();
    ClsOpenClose OC = new ClsOpenClose();
    ClsInsertTrans ITrans = new ClsInsertTrans();
    DbConnection conn = new DbConnection();
    Mobile_Service mob = new Mobile_Service();
    ClsSRO sro = new ClsSRO();
    string RefNumber, PmtMode;
    string FL = "";
    int result = 0;
    int Res = 0,TotalAmt = 0;
     int resultint = 0, Activity, IntCalType = 0, IntApp = 0, IntId = 0, resultout = 0, resmodal = 0;
    TextBox tb;
    static int i = 0;
    string sroname = "", AC_Status = "", stage = "", usrgrp = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
                Response.Redirect("FrmLogin.aspx");

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["BRCD"]))
                {
                    TxtBRCD.Text = Request.QueryString["BRCD"].ToString();

                }
                if (!string.IsNullOrEmpty(Request.QueryString["CASE_YEAR"]))
                {
                    txtCaseY.Text = Request.QueryString["CASE_YEAR"].ToString();

                }
               if (!string.IsNullOrEmpty(Request.QueryString["CASENO"]))
                    {
                        txtCaseNo.Text = Request.QueryString["CASENO"].ToString();
                       
                    }
               //else
               //{
               //    Response.Redirect("~/FrmBlank.aspx?ShowMessage=true");
               //}
                  
              
             ViewState["FLAG"] = "AD";
             if (ViewState["FLAG"].ToString() == "AD")
             {
                // Div15.Visible = false;
                 BD.BindPaymentMode(ddlPaymentMode);
                 BD.BindPaymentModeEXPENCE(ddlEXPENCE);
                 BD.BindPaymentModeStage(DDLSTAGE);
             }
             AutoAccname.ContextKey = Session["BRCD"].ToString();
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmBlank.aspx");
    }
    protected void BtnRecipt_Click(object sender, EventArgs e)
    {

        try
        {
            if (ViewState["FLAG"].ToString() == "RES")
            {


                if (txtBankGl.Text == "")
                {
                    WebMsgBox.Show("Please Enter BankGl Code.", this.Page);
                }
                if (txtprcd.Text == "")
                {
                    WebMsgBox.Show("Please Enter Product code.", this.Page);
                }
                if (txtRecipt.Text == "")
                {
                    WebMsgBox.Show("Please Enter Receipt No.", this.Page);
                }
                if (ddlPaymentMode.SelectedValue == "0")
                {
                    WebMsgBox.Show("Please Select Payment mode", this.Page);
                }
                if (txtentdate.Text == "")
                {
                    WebMsgBox.Show("Please Enter Date.", this.Page);
                }
                if (ddlPaymentMode.SelectedValue == "0" && txtbcd.Text == "")
                {
                    WebMsgBox.Show("Please Enter Bank Code .", this.Page);
                }

                string BRCD = Session["BRCD"].ToString();
                string MID = Session["MID"].ToString();
                string EntryDate = "";
                string custno = "";
                string SetNo = "";
                string PACMAC = "";
                string glcode = "", prcd = "";
                string CN = "", CD = "01/01/1990";
                string InstdAte = "";
                string PAYMAST = "";

                string REFERENCEID = "";
                REFERENCEID = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                ViewState["RID"] = (Convert.ToInt32(REFERENCEID) + 1).ToString();

                EntryDate = Session["EntryDate"].ToString();
                PACMAC = conn.PCNAME().ToString();

                if (ddlPaymentMode.SelectedValue == "1")
                {
                    prcd = "100";
                }
                else
                    if (ddlPaymentMode.SelectedValue == "4")
                    {
                        prcd = "102";
                    }
                glcode = txtprcd.Text;//BD.GetCashGl(prcd, Session["BRCD"].ToString());
                SetNo = txtRecipt.Text; //.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
                // SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();



                CN = txtChequeNo.Text != "" ? txtChequeNo.Text : "";
                InstdAte = string.IsNullOrEmpty(TxtChequeDate.Text) ? "01/01/1990" : TxtChequeDate.Text;

                if (ddlPaymentMode.SelectedValue == "1")
                {
                    if (txtprcd.Text == "501")
                    {

                        resultint = SRO.Authorized(txtentdate.Text, Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), prcd, glcode,
                            glcode.ToString(), "", ddlPaymentMode.SelectedValue, txtAmount.Text.ToString(), "1", "4", ddlPaymentMode.SelectedValue, txtRecipt.Text, CN, InstdAte, txtbcd.Text, "0", "1003",
                          "0", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", DDLSTAGE.SelectedValue, txtCaseNo.Text, txtCaseY.Text, ViewState["RID"].ToString(), "");

                        if (resultint > 0)
                        {
                            // string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());

                            //resultint = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), prcd, txtprcd.Text,
                            //         txtprcd.Text, "", ddlPaymentMode.SelectedValue, txtAmount.Text.ToString(), "1", "4", ddlPaymentMode.SelectedValue, SetNo, CN, InstdAte, "0", "0", "1003",
                            //           DDLSTAGE.SelectedValue, Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "CostProcess", txtCaseNo.Text, txtCaseY.Text, ViewState["RID"].ToString(), "");
                            //if (resultint > 0)
                            {
                                string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());

                                resultint = SRO.Authorized(txtentdate.Text, Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", cgl,
                                          "99", "", "", txtAmount.Text.ToString(), "2", "4", ddlPaymentMode.SelectedValue, txtRecipt.Text, CN, InstdAte, txtbcd.Text, "0", "1003",
                                          DDLSTAGE.SelectedValue, Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", DDLSTAGE.SelectedValue, txtCaseNo.Text, txtCaseY.Text, ViewState["RID"].ToString(), "");
                                if (resultint > 0)
                                {

                                    lblMessage.Text = "Record Submitted Successfully With Recipt No :" + txtRecipt.Text;
                                    ModalPopup.Show(this.Page);
                                    FL = "Insert";//Dhanya Shetty
                                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Cash_Payment _" + prcd + "_" + prcd + "_" + txtAmount.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                }
                                //  BindGrid();
                                BindGriddate();
                                clear1();
                            }
                        }
                    }
                    else if (txtprcd.Text == "502")
                    {
                        resultint = SRO.Authorized(txtentdate.Text, Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), prcd, glcode,
                            glcode.ToString(), "", ddlPaymentMode.SelectedValue, txtAmount.Text.ToString(), "1", "4", ddlPaymentMode.SelectedValue, txtRecipt.Text, CN, InstdAte, txtbcd.Text, "0", "1003",
                            DDLSTAGE.SelectedValue, Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", DDLSTAGE.SelectedValue, txtCaseNo.Text, txtCaseY.Text, ViewState["RID"].ToString(), "");

                        if (resultint > 0)
                        {
                            // string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());

                            //resultint = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), prcd, txtprcd.Text,
                            //         txtprcd.Text, "", ddlPaymentMode.SelectedValue, txtAmount.Text.ToString(), "1", "4", ddlPaymentMode.SelectedValue, SetNo, CN, InstdAte, "0", "0", "1003",
                            //           DDLSTAGE.SelectedValue, Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "CostProcess", txtCaseNo.Text, txtCaseY.Text, ViewState["RID"].ToString(), "");
                            //if (resultint > 0)
                            {
                                string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());

                                resultint = SRO.Authorized(txtentdate.Text, Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", cgl,
                                          "99", "", "", txtAmount.Text.ToString(), "2", "4", ddlPaymentMode.SelectedValue, txtRecipt.Text, CN, InstdAte, txtbcd.Text, "0", "1003",
                                          DDLSTAGE.SelectedValue, Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", DDLSTAGE.SelectedValue, txtCaseNo.Text, txtCaseY.Text, ViewState["RID"].ToString(), "");
                                if (resultint > 0)
                                {

                                    lblMessage.Text = "Record Submitted Successfully With Recipt No :" + txtRecipt.Text;
                                    ModalPopup.Show(this.Page);
                                    FL = "Insert";//Dhanya Shetty
                                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Cash_Payment _" + prcd + "_" + prcd + "_" + txtAmount.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                }
                                // BindGrid();
                                BindGriddate();
                                clear1();
                            }
                        }
                    }
                }
                else if (ddlPaymentMode.SelectedValue == "4")
                {


                    string cgl = BD.GetCashGl("102", Session["BRCD"].ToString());

                    resultint = SRO.Authorized(txtentdate.Text, Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), prcd, txtprcd.Text,
                                txtBankGl.Text, "", "", txtAmount.Text.ToString(), "1", "4", ddlPaymentMode.SelectedValue, txtRecipt.Text, CN, InstdAte, txtbcd.Text, "0", "1003",
                                 "0", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", DDLSTAGE.SelectedValue, txtCaseNo.Text, txtCaseY.Text, ViewState["RID"].ToString(), "");
                    if (resultint > 0)
                    {
                        resultint =SRO.Authorized(txtentdate.Text, Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), prcd, txtprcd.Text,
                              txtBankGl.Text, "", "", txtAmount.Text.ToString(), "2", "4", ddlPaymentMode.SelectedValue, txtRecipt.Text, CN, InstdAte, txtbcd.Text, "0", "1003",
                               "0", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", DDLSTAGE.SelectedValue, txtCaseNo.Text, txtCaseY.Text, ViewState["RID"].ToString(), "");

                        if (resultint > 0)
                        {

                            lblMessage.Text = "Record Submitted Successfully With Recipt No :" + txtRecipt.Text;
                            ModalPopup.Show(this.Page);
                            FL = "Insert";//Dhanya Shetty
                            //string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Cash_Payment _" + TxtProcode.Text + "_" + TxtAccNo.Text + "_" + txtamountt.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        }
                        // BindGrid();
                        BindGriddate();
                        clear1();
                    }

                }
            }
            else
            if (ViewState["FLAG"].ToString() == "MD")
            {
                if (txtBankGl.Text == "")
                {
                    WebMsgBox.Show("Please Enter BankGl Code.", this.Page);
                }
                if (txtprcd.Text == "")
                {
                    WebMsgBox.Show("Please Enter Product code.", this.Page);
                }
                if (txtRecipt.Text == "")
                {
                    WebMsgBox.Show("Please Enter Receipt No.", this.Page);
                }
                if (ddlPaymentMode.SelectedValue == "0")
                {
                    WebMsgBox.Show("Please Select Payment mode", this.Page);
                }
                if (txtentdate.Text == "")
                {
                    WebMsgBox.Show("Please Enter Date.", this.Page);
                }
                if (ddlPaymentMode.SelectedValue == "0" && txtbcd.Text == "")
                {
                    WebMsgBox.Show("Please Enter Bank Code .", this.Page);
                }
                string id2 = ViewState["SCROLLNO"].ToString();

                result = SRO.ModCashRecipt(brcd: TxtBRCD.Text, caseno: txtCaseNo.Text, caseyear: txtCaseY.Text, setno: txtRecipt.Text, ENTRYDATE: txtentdate.Text, id: id2, GLCODE: txtBankGl.Text, SUBGLCODE: txtprcd.Text, CR: txtAmount.Text, INSTRUMENTNO: txtChequeNo.Text, INSTRUMENTDATE: TxtChequeDate.Text, INSTBANKCD: txtbcd.Text, PayType: ddlPaymentMode.SelectedValue, PAYMAST: DDLSTAGE.SelectedValue);


                if (result > 0)
                {
                    WebMsgBox.Show("Data Modify successfully", this.Page);
                    BindGriddate();
                    clear1();
                }

            }



        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    
   public void BindGrid()
    {
        try
        {
            SRO.Getinfotable(GridCAsh, txtCaseY.Text,TxtBRCD.Text , Session["EntryDate"].ToString(), txtCaseNo.Text);//Session["BRCD"].ToString()
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

   public void BindGriddate()
   {
       try
       {
           SRO.Getinfotable1(GridCAsh, txtCaseY.Text, TxtBRCD.Text, txtCaseNo.Text);//Session["BRCD"].ToString()
       }
       catch (Exception Ex)
       {
           ExceptionLogging.SendErrorToText(Ex);
       }
   }

    protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPaymentMode.SelectedValue== "1")
        {
            Div15.Visible = false;
        }
        else if (ddlPaymentMode.SelectedValue == "4")
        {
            Div15.Visible = true;
          
        }
    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        ViewState["FLAG"] = "RES";
        BtnRecipt.Visible = true;
        btnSociety.Visible = false;
        txtRecipt.Text=BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
        BindGriddate();
    }
    //protected void lnkModify_Click(object sender, EventArgs e)
    //{
    //    btnSociety.Visible = true;
    //    BtnRecipt.Visible = false;
    //    GrdDemand.Visible = false;
    //    GridCAsh.Visible = false;
    //}
    protected void btnSociety_Click(object sender, EventArgs e)
    {
          try
        {

            result = SRO.InserttSociety(BRCD: TxtBRCD.Text, PRDCD: "", SRO_NO: "", CASE_YEAR: txtCaseY.Text, CASENO: txtCaseNo.Text, MEMBERNO: "", DEFAULTERNAME: "", accno: "", PRCDCD: "", rate: "", principle: "", ENTRYDATE: Session["EntryDate"].ToString(), fromdate: "", todate: "", month: "", AMOUNT: txtAmount.Text, PAYMENTMODE: ddlPaymentMode.SelectedValue, CHEQUENO: txtChequeNo.Text, STAGE: "1001", MID: Session["MID"].ToString());
                  

              if (result > 0)
                {
                    WebMsgBox.Show("Data Saved successfully", this.Page);
                    //BindGrdMain();
                  clear();
                }
            }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    //protected void GrdDemand_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    try
    //    {
    //        GrdDemand.PageIndex = e.NewPageIndex;
    //        BindGrdMain();
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }

    //}
    //public void BindGrdMain()
    //{
    //    try
    //    {
    //        SRO.BindGrdDemScociety(GrdDemand, TxtBRCD.Text);
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionLogging.SendErrorToText(ex);
    //    }
    //}
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "VW";

           
            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');
            ViewState["BRCD"] = id[0].ToString();
            ViewState["CASENO"] = id[1].ToString();
            ViewState["CASE_YEAR"] = id[2].ToString();
            ViewState["SETNO"] = id[3].ToString();
            ViewState["SCROLLNO"] = id[4].ToString();
            ViewState["EntryDate"] = id[5].ToString();
            txtCaseNo.Text = ViewState["CASENO"].ToString();
            txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
            TxtBRCD.Text = ViewState["BRCD"].ToString();
            string Setno = ViewState["SETNO"].ToString();
            string date = ViewState["EntryDate"].ToString();

            Convert.ToDateTime(date).ToString("dd/MM/yyyy");
            string id2 = ViewState["SCROLLNO"].ToString();

            string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&SCROLLNO=" + id2 + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + date + "&BRCD=" + Session["BRCD"].ToString() + "&FN=R&rptname=RptReceiptPrintHSFM.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
      

            //BindGrid();
           
           
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }
    public void clear()
    {
        txtCaseNo.Text = "";
        txtCaseY.Text = "";
        TxtBRCD.Text = "";
        txtChequeNo.Text = "";
        ddlPaymentMode.SelectedValue = "0";
        txtAmount.Text = "";
        txtBankGl.Text = "";
        txtbcd.Text = "";
        TxtBankName.Text = "";
        txtbcdname.Text = "";
        txtRecipt.Text = "";
        txtprcdname.Text = "";
        TxtChequeDate.Text = "";
        txtentdate.Text = "";
        txtBankGl.Text = "";
        txtprcd.Text = "";
        DDLSTAGE.SelectedValue = "0";
    }

    public void clear1()
    {
        
        txtChequeNo.Text = "";
        ddlPaymentMode.SelectedValue = "0";
        txtAmount.Text = "";
        txtBankGl.Text = "";
        txtbcd.Text = "";
        TxtBankName.Text = "";
        txtbcdname.Text = "";
        txtRecipt.Text = "";
        txtprcd.Text = "";
        DDLSTAGE.SelectedValue = "0";
        txtprcdname.Text = "";
        TxtChequeDate.Text = "";
        txtentdate.Text = "";

    }
    protected void GridCAsh_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           
            //{
            //    string Setno = (GridCAsh.SelectedRow.FindControl("SET_NO") as Label).Text;
            //    FL = "Insert";
            //   // string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Cash_Payment_Abb _" + txtProdType.Text + "_" + txtAccNo.Text + "_" + txtAmount.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            //    string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + Session["EntryDate"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&FN=R&rptname=RptReceiptPrint.rdlc";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            //}
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmS0002.aspx", true);

    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        Div3.Visible = true;
      //  div_Grid.Visible = false;
    }
    protected void btnExpence_Click(object sender, EventArgs e)
    {

    }
    protected void txtBankGl_TextChanged(object sender, EventArgs e)
    {
        if (txtBankGl.Text != "")
        {
            string bname = sro.GetBANGLCDName(Session["BRCD"].ToString(), txtBankGl.Text);
            if (bname != null)
            {
                TxtBankName.Text = bname;

            }
            else
            {
                WebMsgBox.Show("Enter valid Product Code.....!", this.Page);
               
            }
        }
        else
        {
            WebMsgBox.Show("Enter Product Code!....", this.Page);
           
        }
    }

    protected void txtprcd_TextChanged(object sender, EventArgs e)
    {
        if (txtprcd.Text != "")
        {
            string bname = sro.GetPRCDName(Session["BRCD"].ToString(), txtprcd.Text);
            if (bname != null)
            {
                txtprcdname.Text = bname;
               
            }
            else
            {
                WebMsgBox.Show("Enter valid Product Code.....!", this.Page);
              
            }
        }
        else
        {
            WebMsgBox.Show("Enter Product Code!....", this.Page);
            
        }
       
    }
    protected void GridCAsh_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridCAsh.PageIndex = e.NewPageIndex;
           // BindGrid();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtRecipt_TextChanged(object sender, EventArgs e)
    {
        stage = BD.CHKRECP(Session["EntryDate"].ToString(), Session["BRCD"].ToString(), txtRecipt.Text);
        if (stage != null)
        {
            WebMsgBox.Show("Recipt No Already Present", this.Page);
            txtRecipt.Text = "";

        }
    }
    protected void txtbcd_TextChanged(object sender, EventArgs e)
    {
        try
        {


            txtbcdname.Text = sro.GetBankName(txtbcd.Text);

           
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    //protected void txtbrchcode_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string CUNAME = txtbrchcode.Text;
    //        string[] custnob = CUNAME.Split('_');
    //        if (custnob.Length > 1)
    //        {
    //            txtbrchcode.Text = custnob[0].ToString();

    //            string[] AC = SRO.GetBranchName(txtbrchcode.Text, custnob[2].ToString()).Split('-');

    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }

    //}
    protected void txtbcdname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtbcdname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtbcd.Text = custnob[1].ToString();
                txtbcdname.Text = custnob[0].ToString();
              //  string[] AC = SRO.GetBranchName(txtbcd.Text, custnob[0].ToString()).Split('-');

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtentdate_TextChanged(object sender, EventArgs e)
    {
      //  BindGriddate(); 
    }
    protected void lnkDelete_Click1(object sender, EventArgs e)
    {
        try
        {



            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');
            ViewState["BRCD"] = id[0].ToString();
            ViewState["CASENO"] = id[1].ToString();
            ViewState["CASE_YEAR"] = id[2].ToString();
            ViewState["SETNO"] = id[3].ToString();
            ViewState["SCROLLNO"] = id[4].ToString();
            ViewState["EntryDate"] = id[5].ToString();
            txtCaseNo.Text = ViewState["CASENO"].ToString();
            txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
            TxtBRCD.Text = ViewState["BRCD"].ToString();
            string Setno = ViewState["SETNO"].ToString();
            string date = ViewState["EntryDate"].ToString();

            Convert.ToDateTime(date).ToString("dd/MM/yyyy");
            string id2 = ViewState["SCROLLNO"].ToString();

            result = SRO.DeleteCashRecipt(TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text, Setno, date, id2);


            if (result > 0)
            {
                WebMsgBox.Show("Data Delete successfully", this.Page);
                BindGriddate();
                clear1();
            }

            



        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }
    protected void txtCaseNo_TextChanged(object sender, EventArgs e)
    {
        BindGriddate();
    }
    protected void lnkModify_Click1(object sender, EventArgs e)
    {
        try
        {



            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');
            ViewState["BRCD"] = id[0].ToString();
            ViewState["CASENO"] = id[1].ToString();
            ViewState["CASE_YEAR"] = id[2].ToString();
            ViewState["SETNO"] = id[3].ToString();
            ViewState["SCROLLNO"] = id[4].ToString();
            ViewState["EntryDate"] = id[5].ToString();
            txtCaseNo.Text = ViewState["CASENO"].ToString();
            txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
            TxtBRCD.Text = ViewState["BRCD"].ToString();
            string Setno = ViewState["SETNO"].ToString();
            string date = ViewState["EntryDate"].ToString();

            Convert.ToDateTime(date).ToString("dd/MM/yyyy");
            string id2 = ViewState["SCROLLNO"].ToString();
            ViewCashDetails(TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text, Setno,id2, txtentdate.Text);
            BtnRecipt.Text = "Modify";
            BtnRecipt.Visible = true;
            ViewState["FLAG"] = "MD";


        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void ViewCashDetails(string BRCD, string CASENO, string CASE_YEAR,string SetNo,string ScrollNo,string Entrydate)
    {
        try
        {
            DataTable DT1 = new DataTable();
            DT = SRO.ViewCashRecipt( BRCD:BRCD,  CASENO:CASENO,  CASE_YEAR:CASE_YEAR,  setno:SetNo,id:ScrollNo, ENTRYDATE:Entrydate);
           


            if (DT.Rows.Count > 0)
            {
                
                txtChequeNo.Text = DT.Rows[0]["INSTRUMENTNO"].ToString();
                ddlPaymentMode.SelectedValue = DT.Rows[0]["PMTMODE"].ToString();
                txtAmount.Text = DT.Rows[0]["CREDIT"].ToString();
                txtBankGl.Text =  DT.Rows[0]["GLCODE"].ToString();
                txtbcd.Text =  DT.Rows[0]["BRCD"].ToString();
                TxtBankName.Text = sro.GetBANGLCDName(Session["BRCD"].ToString(), txtBankGl.Text);
                txtbcdname.Text = sro.GetBankName(txtbcd.Text);
                txtRecipt.Text = DT.Rows[0]["SETNO"].ToString();
                txtprcd.Text = DT.Rows[0]["SUBGLCODE"].ToString();
                DDLSTAGE.SelectedValue = DT.Rows[0]["PAYMAST"].ToString();
                txtprcdname.Text = sro.GetPRCDName(Session["BRCD"].ToString(), txtprcd.Text);
                TxtChequeDate.Text = Convert.ToDateTime(DT.Rows[0]["INSTRUMENTDATE"]).ToString("dd/MM/yyyy"); // DT.Rows[0]["INSTRUMENTDATE"].ToString("dd/MM/yyyy"); 
                txtentdate.Text = Convert.ToDateTime(DT.Rows[0]["ENTRYDATE"]).ToString("dd/MM/yyyy"); //DT.Rows[0]["ENTRYDATE"].ToString("dd/MM/yyyy");

                if (ddlPaymentMode.SelectedValue == "1")
                {
                    Div15.Visible = false;
                }
                else if (ddlPaymentMode.SelectedValue == "4")
                {
                    Div15.Visible = true;

                }

            }
            else
            {
                 WebMsgBox.Show("No record found..!!", this.Page);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}