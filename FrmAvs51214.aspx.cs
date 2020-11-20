using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
public partial class FrmAvs51214 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCommon CMN = new ClsCommon();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsSRO SRO = new ClsSRO();
    ClsAVS51186 MOV = new ClsAVS51186();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "", MEM = "";
    int result = 0;
    string sroname = "", AC_Status = "", results = "", stage = "";
    float rate ;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            // autoglname.ContextKey = Session["brcd"].ToString();
            BD.BindPaymentModeStage(ddlActstatus);
            BD.BindPaymentModeEXPENCE(ddlcasestaus);
            BD.BindPaymentModeRES(ddlPaymentMode);
            BD.BindSTAGENOTICE(ddlNotice, ddlActstatus.SelectedValue.ToString());
                
            //BindGrdMain();
            lbl1.Visible = false;
            TXTTODATE.Visible = false;
            t1.Visible = false;
            txtfdate.Visible = false;
            p1.Visible = false;
            txtpd.Visible = false;
           

        }

    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        ViewState["FLAG"] = "SOCADC";
        DIVCASE.Visible = false;
        DIVACTION.Visible = false;
        div_Grid.Visible = true;
        BindGrdMain();
        lbl1.Visible = false;
        TXTTODATE.Visible = false;
        t1.Visible = false;
        txtfdate.Visible = false;
        p1.Visible = false;
        txtpd.Visible = false;
        txtamtd.Visible = true;
        txtRecAmt.Visible = true;
        // txtRemark.Enabled = false;
        txtBalance.Visible = true;
        //  ddlActstatus.Enabled = false;
        // ddlcasestaus.Enabled = false;
        // lstarea.Enabled = false;
        // TXTSROName.Enabled = false;
        //divrec.Visible = false;
        //Div3.Visible = true;
        BtnSubmit.Visible = true;
        lbl1.Visible = false;
        TXTTODATE.Visible = false;
        t1.Visible = false;
        txtfdate.Visible = false;
        p1.Visible = false;
        txtpd.Visible = false;
        BtnReport.Visible = false;
        TXTPRINCIPL.Visible = false;
        txtotherharges.Visible = false;
        r1.Visible = true;
        b1.Visible = true;
        d1.Visible = true;
        P2.Visible = false;
        o1.Visible = false;
        pm1.Visible = false;
        pbd.Visible = false;
        cn1.Visible = false;
        cd1.Visible = false;
        divstate.Visible = false;
        ddlPaymentMode.Visible = false;
        txtAmount.Visible = false;
        txtChequeNo.Visible = false;
        TxtChequeDate.Visible = false;
        btnpayment.Visible = false;
        // BindGrdMainstatement();
        txttotalamt.Visible = false;
        lbltot.Visible = false;
        txtbal.Visible = false;
        lblbal.Visible = false;
        Div6.Visible = false;
        txtrecamt1.Visible = false;
        lblra.Visible = false;
    }

    protected void lnkCAse_Click(object sender, EventArgs e)
    {
        ViewState["FLAG"] = "CASEADCS";
        div_Grid.Visible = false;
        DIVACTION.Visible = false;
        DIVCASE.Visible = true;
        BtnDownload.Visible = true;
        ClearSociety();
        BtnSubmit.Text = "Submit";
        lbl1.Visible = false;
        TXTTODATE.Visible = false;
        t1.Visible = false;
        txtfdate.Visible = false;
        p1.Visible = false;
        txtpd.Visible = false;
        BindGrdMainCase();
        txtotherharges.Visible = false;
        r1.Visible = true;
        b1.Visible = true;
        d1.Visible = true;
        P2.Visible = false;
        o1.Visible = false;
        pm1.Visible = false;
        pbd.Visible = false;
        cn1.Visible = false;
        cd1.Visible = false;
        divstate.Visible = false;
        ddlPaymentMode.Visible = false;
        txtAmount.Visible = false;
        txtChequeNo.Visible = false;
        TxtChequeDate.Visible = false;
        btnpayment.Visible = false;
        // BindGrdMainstatement();
        txttotalamt.Visible = false;
        lbltot.Visible = false;
        txtbal.Visible = false;
        lblbal.Visible = false;
        Div6.Visible = false;
        txtrecamt1.Visible = false;
        lblra.Visible = false;
    }

    protected void lnkAction_Click(object sender, EventArgs e)
    {
        ViewState["FLAG"] = "ACTIONADAC";
        div_Grid.Visible = false;
        DIVCASE.Visible = false;
        BtnDownload.Visible = true;
        DIVACTION.Visible = true;
        BtnSubmit.Text = "Submit";
        lbl1.Visible = false;
        TXTTODATE.Visible = false;
        t1.Visible = false;
        txtfdate.Visible = false;
        p1.Visible = false;
        txtpd.Visible = false;
        BindGrdMainACTION();
        txtotherharges.Visible = false;
        r1.Visible = true;
        b1.Visible = true;
        d1.Visible = true;
        P2.Visible = false;
        o1.Visible = false;
        pm1.Visible = false;
        pbd.Visible = false;
        cn1.Visible = false;
        cd1.Visible = false;
        divstate.Visible = false;
        ddlPaymentMode.Visible = false;
        txtAmount.Visible = false;
        txtChequeNo.Visible = false;
        TxtChequeDate.Visible = false;
        btnpayment.Visible = false;
        // BindGrdMainstatement();
        txttotalamt.Visible = false;
        lbltot.Visible = false;
        txtbal.Visible = false;
        lblbal.Visible = false;
        Div6.Visible = false;
        txtrecamt1.Visible = false;
        lblra.Visible = false;
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtSRNO_TextChanged(object sender, EventArgs e)
    {
        try
        {

            sroname = SRO.GetSROName(TxtSRNO.Text);
            TXTSROName.Text = sroname;

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void ddlcasestaus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlActstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BD.BindSTAGENOTICE(ddlNotice, ddlActstatus.SelectedValue.ToString());
      
    }
    protected void ddlWard_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(ViewState["FLAG"]) == "SOCADC")
            {

                result = SRO.SocietyRec(BRCD: Session["BRCD"].ToString(), PRDCD: "", SRO_NO: TxtSRNO.Text, CASE_YEAR: txtCaseY.Text, CASENO: txtCaseNO.Text,
                    SOCIETYNAME: txtSociName.Text, MEMBERNO: txtMember.Text, DEFAULTERNAME: lstarea.Text.ToString(), accno: "", PRCDCD: "", ENTRYDATE: txtDate.Text, RECAMOUNT: txtRecAmt.Text, PAID_DEF_AMOUNT: txtamtd.Text, STAGE: "1001", MID: Session["MID"].ToString(), REMARK: txtRemark.Text, CASESTATUSNO: ddlcasestaus.SelectedValue, ACTIONSTATUSNO: ddlActstatus.SelectedValue, Balance: txtBalance.Text);

                // InsertDefaulterName();
                if (result > 0)
                {
                    WebMsgBox.Show("Data Saved successfully", this.Page);

                } BindGrdMain();
            }

            if (Convert.ToString(ViewState["FLAG"]) == "SOCMDC")
            {
                string srno = ViewState["SOCIeTYID"].ToString();

                result = SRO.MODISocietyRec(BRCD: Session["BRCD"].ToString(), PRDCD: "", SRO_NO: TxtSRNO.Text, CASE_YEAR: txtCaseY.Text, CASENO: txtCaseNO.Text,
                    SOCIETYNAME: txtSociName.Text, MEMBERNO: txtMember.Text, DEFAULTERNAME: lstarea.Text.ToString(), accno: "", PRCDCD: "", ENTRYDATE: txtDate.Text, RECAMOUNT: txtRecAmt.Text, PAID_DEF_AMOUNT: txtamtd.Text, STAGE: "1001", MID: Session["MID"].ToString(), REMARK: txtRemark.Text, CASESTATUSNO: ddlcasestaus.SelectedValue, ACTIONSTATUSNO: ddlActstatus.SelectedValue, Balance: txtBalance.Text, ID: srno);

                // InsertDefaulterName();
                if (result > 0)
                {
                    WebMsgBox.Show("Data Modify successfully", this.Page);
                    this.ViewState.Remove("SOCIeTYID");

                } 
                BindGrdMain();
                
            }
            if (Convert.ToString(ViewState["FLAG"]) == "CASEADCS")
            {

                result = SRO.ADDCASESTATUS(BRCD: Session["BRCD"].ToString(), PRDCD: "", SRO_NO: TxtSRNO.Text, CASE_YEAR: txtCaseY.Text, CASENO: txtCaseNO.Text,
                    SOCIETYNAME: txtSociName.Text, MEMBERNO: txtMember.Text, DEFAULTERNAME: lstarea.Text.ToString(), accno: "", PRCDCD: "", ENTRYDATE: txtDate.Text, RECAMOUNT: txtRecAmt.Text, PAID_DEF_AMOUNT: txtamtd.Text, STAGE: "1001", MID: Session["MID"].ToString(), REMARK: txtRemark.Text, CASESTATUSNO: ddlcasestaus.SelectedValue, ACTIONSTATUSNO: ddlActstatus.SelectedValue, Balance: txtBalance.Text);

                // InsertDefaulterName();
                if (result > 0)
                {
                    WebMsgBox.Show("Data saved successfully", this.Page);

                }
                BindGrdMainCase();
            }
            if (Convert.ToString(ViewState["FLAG"]) == "CASEMDCCS")
            {
                string srno = ViewState["SOCIeTYID"].ToString();

                result = SRO.MODICASESTATUS(BRCD: Session["BRCD"].ToString(), PRDCD: "", SRO_NO: TxtSRNO.Text, CASE_YEAR: txtCaseY.Text, CASENO: txtCaseNO.Text,
                    SOCIETYNAME: txtSociName.Text, MEMBERNO: txtMember.Text, DEFAULTERNAME: lstarea.Text.ToString(), accno: "", PRCDCD: "", ENTRYDATE: txtDate.Text, RECAMOUNT: txtRecAmt.Text, PAID_DEF_AMOUNT: txtamtd.Text, STAGE: "1001", MID: Session["MID"].ToString(), REMARK: txtRemark.Text, CASESTATUSNO: ddlcasestaus.SelectedValue, ACTIONSTATUSNO: ddlActstatus.SelectedValue, Balance: txtBalance.Text, id: srno);

                // InsertDefaulterName();
                if (result > 0)
                {
                    WebMsgBox.Show("Data modify successfully", this.Page);
                    this.ViewState.Remove("SOCIeTYID");

                }
                BindGrdMainCase();
            }
            if (Convert.ToString(ViewState["FLAG"]) == "ACTIONADAC")
            {

                result = SRO.ADDACTIONSTATUS(BRCD: Session["BRCD"].ToString(), PRDCD: "", SRO_NO: TxtSRNO.Text, CASE_YEAR: txtCaseY.Text, CASENO: txtCaseNO.Text,
                    SOCIETYNAME: txtSociName.Text, MEMBERNO: txtMember.Text, DEFAULTERNAME: lstarea.Text.ToString(), accno: "", PRCDCD: "", ENTRYDATE: txtDate.Text, RECAMOUNT: txtRecAmt.Text, PAID_DEF_AMOUNT: txtamtd.Text, STAGE: "1001", MID: Session["MID"].ToString(), REMARK: txtRemark.Text, CASESTATUSNO: ddlcasestaus.SelectedValue, ACTIONSTATUSNO: ddlActstatus.SelectedValue, Balance: txtBalance.Text);

                // InsertDefaulterName();
                if (result > 0)
                {
                    WebMsgBox.Show("Data save successfully", this.Page);

                }
                BindGrdMainACTION();
            }
            if (Convert.ToString(ViewState["FLAG"]) == "ACTIONMDCAS")
            {

                string srno = ViewState["SOCIeTYID"].ToString();
                result = SRO.MODIACTIONSTATUS(BRCD: Session["BRCD"].ToString(), PRDCD: "", SRO_NO: TxtSRNO.Text, CASE_YEAR: txtCaseY.Text, CASENO: txtCaseNO.Text,
                        SOCIETYNAME: txtSociName.Text, MEMBERNO: txtMember.Text, DEFAULTERNAME: lstarea.Text.ToString(), accno: "", PRCDCD: "", ENTRYDATE: txtDate.Text, RECAMOUNT: txtRecAmt.Text, PAID_DEF_AMOUNT: txtamtd.Text, STAGE: "1001", MID: Session["MID"].ToString(), REMARK: txtRemark.Text, CASESTATUSNO: ddlcasestaus.SelectedValue, ACTIONSTATUSNO: ddlActstatus.SelectedValue, Balance: txtBalance.Text);

                // InsertDefaulterName();
                if (result > 0)
                {
                    WebMsgBox.Show("Data modify successfully", this.Page);
                    this.ViewState.Remove("SOCIeTYID");


                }
                BindGrdMainACTION();
            }
               
                ClearSociety();
            
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        ClearSociety();
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

    protected void BtnDownload_Click(object sender, EventArgs e)
    {
        try
        {
          // if (Convert.ToString(ViewState["FLAG"]) == "SOCADC")
            {
                DataTable dt = new DataTable();
                dt = SRO.Downloadcase1(BRCD: Session["BRCD"].ToString(), CASENO: txtCaseNO.Text, CASE_YEAR: txtCaseY.Text, casestaus1: ddlcasestaus.SelectedValue);
   
                GridView gv = new GridView();
                gv.DataSource = dt;
                gv.DataBind();
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=SocietyExcelDownload.xls");
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gv.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.End();
            }
            if (Convert.ToString(ViewState["FLAG"]) == "CASEADAC")
            {
                DataTable dt = new DataTable();
                dt = SRO.DownloadACTION(BRCD:Session["BRCD"].ToString(),   CASENO:txtCaseNO.Text,  CASE_YEAR:txtCaseY.Text,  ActionStatus1:ddlActstatus.SelectedValue,  NOTICESTAGE:ddlNotice.SelectedValue);
                GridView gv2 = new GridView();
                gv2.DataSource = dt;
                gv2.DataBind();
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=ExcelDownload.xls");
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gv2.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.End();
            }
           
            if (Convert.ToString(ViewState["FLAG"]) == "ACTIONADCS")
            {
                DataTable dt = new DataTable();
                dt = SRO.DownloadCase(Session["BRCD"].ToString());
                GridView gv1 = new GridView();
                gv1.DataSource = dt;
                gv1.DataBind();
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=ExcelDownload1.xls");
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gv1.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.End();
            }
                
        }
        


        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtCaseNO_TextChanged(object sender, EventArgs e)
    {

        if (Convert.ToString(ViewState["FLAG"]) == "ACTIONADAC")
        {
            stage = SRO.CHKGETCASENO(BRCD: Session["BRCD"].ToString(), CASENO: txtCaseNO.Text, CASE_YEAR: txtCaseY.Text);
            if (stage != null)
            {
                ViewDetailsACC(BRCD: Session["BRCD"].ToString(), CASENO: txtCaseNO.Text, CASE_YEAR: txtCaseY.Text);
                txtDate.Focus();
                BindGrdMainSTATEMENTACCE();
                BindGrdMainSTATEMENTACCEAll();
                div8.Visible = true;
                div_allsta.Visible = true;
            }

        }
        else
        {
            stage = SRO.CHKGETCASENO(BRCD: Session["BRCD"].ToString(), CASENO: txtCaseNO.Text, CASE_YEAR: txtCaseY.Text);
            if (stage != null)
            {
                ViewDetails(BRCD: Session["BRCD"].ToString(), CASENO: txtCaseNO.Text, CASE_YEAR: txtCaseY.Text);
                txtDate.Focus();
            }
            else
            {
            }
        }
    }
    public void ViewDetailsACC(string BRCD, string CASENO, string CASE_YEAR)
    {
        try
        {
            string id = "";
            DataTable DT1 = new DataTable();
            DT = SRO.ViewDetailsDem(BRCD, CASENO, CASE_YEAR);
            DT1 = SRO.ViewDetailsDefaulterS(BRCD, CASENO, CASE_YEAR);
            if (DT1.Rows.Count > 0)
            {
                for (int i = 0; i < DT1.Rows.Count; i++)
                {
                    lstarea.Items.Add(Convert.ToString(DT1.Rows[i]["ID"]) + '|' + Convert.ToString(DT1.Rows[i]["DEFAULTERNAME"]));
                    //  lstDefProperty.Items.Add(Convert.ToString(DT1.Rows[i]["DEFAULTPROPERTY"]));
                    //   lstCorrespondenceAdd.Items.Add(Convert.ToString(DT1.Rows[i]["CORRESPONDENCEADDRESS"]));
                    //  lstCorrespondenceAdd.Visible = true;
                    lstarea.Visible = true;
                    // lstDefProperty.Visible = true;
                }
            }

            if (DT.Rows.Count > 0)
            {
                TxtSRNO.Text = DT.Rows[0]["SRO_NO"].ToString();
                TXTSROName.Text = SRO.GetSROName(TxtSRNO.Text);
                txtMember.Text = DT.Rows[0]["MEMBERNO"].ToString();

                if (DT.Rows[0]["MEMTYPE"].ToString() == "1")
                {
                    FL = "GETMEMBERNAME";
                }
                if (DT.Rows[0]["MEMTYPE"].ToString() == "2")
                {
                    FL = "GETMEMBERNAMENOMINAL";
                }
                txtSociName.Text = SRO.GetMemberID2(txtMember.Text, FL);
                TXTPRINCIPL.Text = DT.Rows[0]["principalamt2"].ToString();
                txtrecamt1.Text = DT.Rows[0]["REMBAL"].ToString();
                txtfdate.Text = DT.Rows[0]["Todate1"].ToString();
                float PRINCI = Convert.ToSingle(TXTPRINCIPL.Text);
                float REMBAL = Convert.ToSingle(txtrecamt1.Text);
                 rate = Convert.ToSingle(DT.Rows[0]["rate"].ToString());
                if (PRINCI >= REMBAL)
                {
                    TXTPRINCIPL.Text = DT.Rows[0]["REMBAL"].ToString();
                  

                }
                
             

                //DateTime FromYear = Convert.ToDateTime(DT.Rows[0]["Todate1"].ToString());
                //   int month = (FromYear.Month + 1);
                //    txtfdate.Text = month.ToString();
                
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
    public void ViewDetails(string BRCD, string CASENO, string CASE_YEAR)
    {
        try
        {
            string id = "";
            DataTable DT1 = new DataTable();
            DT = SRO.ViewDetailsDem(BRCD, CASENO, CASE_YEAR);
            DT1 = SRO.ViewDetailsDefaulterS(BRCD, CASENO, CASE_YEAR);
            if (DT1.Rows.Count > 0)
            {
                for (int i = 0; i < DT1.Rows.Count; i++)
                {
                    lstarea.Items.Add(Convert.ToString(DT1.Rows[i]["ID"]) + '|' + Convert.ToString(DT1.Rows[i]["DEFAULTERNAME"]));
                    //  lstDefProperty.Items.Add(Convert.ToString(DT1.Rows[i]["DEFAULTPROPERTY"]));
                    //   lstCorrespondenceAdd.Items.Add(Convert.ToString(DT1.Rows[i]["CORRESPONDENCEADDRESS"]));
                    //  lstCorrespondenceAdd.Visible = true;
                    lstarea.Visible = true;
                    // lstDefProperty.Visible = true;
                }
            }

            if (DT.Rows.Count > 0)
            {
                TxtSRNO.Text = DT.Rows[0]["SRO_NO"].ToString();
                TXTSROName.Text = SRO.GetSROName(TxtSRNO.Text);
                txtMember.Text = DT.Rows[0]["MEMBERNO"].ToString();

                if (DT.Rows[0]["MEMTYPE"].ToString() == "1")
                {
                    FL = "GETMEMBERNAME";
                }
                if (DT.Rows[0]["MEMTYPE"].ToString() == "2")
                {
                    FL = "GETMEMBERNAMENOMINAL";
                }
                txtSociName.Text = SRO.GetMemberID2(txtMember.Text, FL);
                TXTPRINCIPL.Text = DT.Rows[0]["PRINCIPLE"].ToString();

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
    public void ClearSociety()
    {

        txtCaseY.Text = "";
        txtCaseNO.Text = "";
        txtMember.Text = "";
        TxtSRNO.Text = "";
        txtSociName.Text = "";
        txtamtd.Text = "";
        txtRecAmt.Text = "";
        txtRemark.Text = "";
        txtBalance.Text = "";
        ddlActstatus.SelectedValue = "0";
        ddlcasestaus.SelectedValue = "0";
        lstarea.Items.Clear();
        txtDate.Text = "";
        TXTPRINCIPL.Text = "";
        txtotherharges.Text = "";
        txtfdate.Text = "";
        TXTTODATE.Text = "";
        txtpd.Text = "";
        ddlPaymentMode.SelectedValue = "0";
        txtAmount.Text = "";
        txtbal.Text = "";
        txtrecamt1.Text = "";
    }
        public int InsertDefaulterName()
    {

        try
        {
            //int index = lstarea.SelectedIndex;
            //if (index >= 0)
            {
                List<string> values = new List<string>();
                if (lstarea.Items.Count != 0)
                {
                    for (int i = 0; i < lstarea.Items.Count; i++)
                    {
                        result = SRO.InsertDefaulterNameS(BRCD:Session["BRCD"].ToString(),CASENO:txtCaseNO.Text, CASE_YEAR:txtCaseY.Text);

                    }
                }
                else if (lstarea.Items.Count == 0)
                {

                    result = SRO.InsertDefaulterNameS(BRCD:Session["BRCD"].ToString(),CASENO:txtCaseNO.Text, CASE_YEAR:txtCaseY.Text);

                }
            }


        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return result;
    }

    public void ViewDetailsSOCIETY(string BRCD, string CASENO, string CASE_YEAR,string ID)
    {
        try
        {
            string id = "";
            DataTable DT1 = new DataTable();
            DT = SRO.ViewDetailSCOREC(BRCD, CASENO, CASE_YEAR,ID); 
            DT1 = SRO.ViewDetailsDefaulterS(BRCD, CASENO, CASE_YEAR);
            if (DT1.Rows.Count > 0)
            {
                for (int i = 0; i < DT1.Rows.Count; i++)
                {
                    lstarea.Items.Add(Convert.ToString(DT1.Rows[i]["ID"]) + '|' + Convert.ToString(DT1.Rows[i]["DEFAULTERNAME"]));
                    //  lstDefProperty.Items.Add(Convert.ToString(DT1.Rows[i]["DEFAULTPROPERTY"]));
                    //   lstCorrespondenceAdd.Items.Add(Convert.ToString(DT1.Rows[i]["CORRESPONDENCEADDRESS"]));
                    //  lstCorrespondenceAdd.Visible = true;
                    lstarea.Visible = true;
                    // lstDefProperty.Visible = true;
                }
            }

            if (DT.Rows.Count > 0)
            {
                TxtSRNO.Text = DT.Rows[0]["SRO_NO"].ToString();
                TXTSROName.Text = SRO.GetSROName(TxtSRNO.Text);
                txtMember.Text = DT.Rows[0]["MEMBERNO"].ToString();
                FL = "GETMEMBERNAME";
                txtSociName.Text = SRO.GetMemberID2(txtMember.Text, FL);
                txtRecAmt.Text = DT.Rows[0]["RECAMOUNT"].ToString();
                txtamtd.Text = DT.Rows[0]["PAID_DEF_AMOUNT"].ToString();
                txtRemark.Text = DT.Rows[0]["REMARK"].ToString();
                txtBalance.Text = DT.Rows[0]["Balance"].ToString();
                ddlcasestaus.SelectedValue = DT.Rows[0]["CASESTATUSNO"].ToString();
                txtDate.Text = Convert.ToDateTime(DT.Rows[0]["ENTRYDATE"]).ToString("dd/MM/yyyy");
            
                ddlActstatus.SelectedValue = DT.Rows[0]["ACTIONSTATUSNO"].ToString();

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
    public void ViewDetailsCASESTATUS(string BRCD, string CASENO, string CASE_YEAR,string id1)
    {
        try
        {
            string id = "";
            DataTable DT1 = new DataTable();
            DT = SRO.ViewDetailSCASESTATUS(BRCD, CASENO, CASE_YEAR,id1);
            DT1 = SRO.ViewDetailsDefaulterS(BRCD, CASENO, CASE_YEAR);
            if (DT1.Rows.Count > 0)
            {
                for (int i = 0; i < DT1.Rows.Count; i++)
                {
                    lstarea.Items.Add(Convert.ToString(DT1.Rows[i]["ID"]) + '|' + Convert.ToString(DT1.Rows[i]["DEFAULTERNAME"]));
                    //  lstDefProperty.Items.Add(Convert.ToString(DT1.Rows[i]["DEFAULTPROPERTY"]));
                    //   lstCorrespondenceAdd.Items.Add(Convert.ToString(DT1.Rows[i]["CORRESPONDENCEADDRESS"]));
                    //  lstCorrespondenceAdd.Visible = true;
                    lstarea.Visible = true;
                    // lstDefProperty.Visible = true;
                }
            }

            if (DT.Rows.Count > 0)
            {
                TxtSRNO.Text = DT.Rows[0]["SRO_NO"].ToString();
                TXTSROName.Text = SRO.GetSROName(TxtSRNO.Text);
                txtMember.Text = DT.Rows[0]["MEMBERNO"].ToString();
                FL = "GETMEMBERNAME";
                txtSociName.Text = SRO.GetMemberID2(txtMember.Text, FL);
                txtRecAmt.Text = DT.Rows[0]["RECAMOUNT"].ToString();
                txtamtd.Text = DT.Rows[0]["PAID_DEF_AMOUNT"].ToString();
                txtRemark.Text = DT.Rows[0]["REMARK"].ToString();
                txtBalance.Text = DT.Rows[0]["Balance"].ToString();
                ddlcasestaus.SelectedValue = DT.Rows[0]["CASESTATUSNO"].ToString();
                txtDate.Text = Convert.ToDateTime(DT.Rows[0]["ENTRYDATE"]).ToString("dd/MM/yyyy");
            
                ddlActstatus.SelectedValue = DT.Rows[0]["ACTIONSTATUSNO"].ToString();

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
    public void ViewDetailsACTION(string BRCD, string CASENO, string CASE_YEAR,string id1)
    {
        try
        {
            string id = "";
            DataTable DT1 = new DataTable();
            DT = SRO.ViewDetailSACTIONSTATUS(BRCD, CASENO, CASE_YEAR,id1);
            DT1 = SRO.ViewDetailsDefaulterS(BRCD, CASENO, CASE_YEAR);
            if (DT1.Rows.Count > 0)
            {
                for (int i = 0; i < DT1.Rows.Count; i++)
                {
                    lstarea.Items.Add(Convert.ToString(DT1.Rows[i]["ID"]) + '|' + Convert.ToString(DT1.Rows[i]["DEFAULTERNAME"]));
                    //  lstDefProperty.Items.Add(Convert.ToString(DT1.Rows[i]["DEFAULTPROPERTY"]));
                    //   lstCorrespondenceAdd.Items.Add(Convert.ToString(DT1.Rows[i]["CORRESPONDENCEADDRESS"]));
                    //  lstCorrespondenceAdd.Visible = true;
                    lstarea.Visible = true;
                    // lstDefProperty.Visible = true;
                }
            }

            if (DT.Rows.Count > 0)
            {
                TxtSRNO.Text = DT.Rows[0]["SRO_NO"].ToString();
                TXTSROName.Text = SRO.GetSROName(TxtSRNO.Text);
                txtMember.Text = DT.Rows[0]["MEMBERNO"].ToString();
                FL = "GETMEMBERNAME";
                txtSociName.Text = SRO.GetMemberID2(txtMember.Text, FL);
                txtRecAmt.Text = DT.Rows[0]["RECAMOUNT"].ToString();
                txtamtd.Text = DT.Rows[0]["PAID_DEF_AMOUNT"].ToString();
                txtRemark.Text = DT.Rows[0]["REMARK"].ToString();
                txtBalance.Text = DT.Rows[0]["Balance"].ToString();
                ddlcasestaus.SelectedValue = DT.Rows[0]["CASESTATUSNO"].ToString();
                txtDate.Text = Convert.ToDateTime(DT.Rows[0]["ENTRYDATE"]).ToString("dd/MM/yyyy");
            
                ddlActstatus.SelectedValue = DT.Rows[0]["ACTIONSTATUSNO"].ToString();

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
    protected void lnkView_Click(object sender, EventArgs e)
    {

        LinkButton objlnk = (LinkButton)sender;
        string[] id = objlnk.CommandArgument.Split('_');

        ViewState["BRCD"] = id[0].ToString();
        ViewState["SOCIeTYID"] = id[1].ToString();
        ViewState["CASENO"] = id[2].ToString();
        ViewState["CASE_YEAR"] = id[3].ToString();
        string BRCD = ViewState["BRCD"].ToString();
        txtCaseNO.Text = ViewState["CASENO"].ToString();
        txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
        string srno = ViewState["SOCIeTYID"].ToString();

        ViewDetailsSOCIETY(BRCD: BRCD, CASENO: txtCaseNO.Text, CASE_YEAR: txtCaseY.Text,ID: srno);
    }
    protected void GrdDemand_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdDemand.PageIndex = e.NewPageIndex;
            BindGrdMain();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindGrdMain()
    {
        try
        {
            SRO.BindGrdSCOIETY(GrdDemand, Session["BRCD"].ToString());

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindGrdMainSTATEMENTACCE()
    {
        try
        {
            SRO.BindGrdSTATEMENT(GRIDVIEWACCST, Session["BRCD"].ToString(),txtCaseY.Text,txtCaseNO.Text);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }


    public void BindGrdMainSTATEMENTACCEAll()
    {
        try
        {
            SRO.BindGrdSTATEMENTAll(grd_allstatement, Session["BRCD"].ToString(), txtCaseY.Text, txtCaseNO.Text);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void BindGrdMainstatement()
    {
        try
        {
            SRO.BindGrdDemMain(Grd_statement, Session["BRCD"].ToString());

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindGrdMainCase()
    {
        try
        {
            SRO.BindGrdCASE(GRdCASE, Session["BRCD"].ToString());

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindGrdMainACTION()
    {
        try
        {
            SRO.BindGrdACTION(GRDACTION, Session["BRCD"].ToString());

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void txtamtd_TextChanged(object sender, EventArgs e)
    {
         float PriAmt = Convert.ToSingle(txtRecAmt.Text);
         float PaidAmt = Convert.ToSingle(txtamtd.Text);
        
        string EMI = "";
        EMI = Math.Round(Convert.ToDouble((PriAmt - PaidAmt))).ToString();
            
        txtBalance.Text = Math.Round(Convert.ToDouble((Convert.ToDouble(EMI)))).ToString();
        ddlcasestaus.Focus();
       
    }
    protected void lnkModify_Click1(object sender, EventArgs e)
    {
        LinkButton objlnk = (LinkButton)sender;
        string[] id = objlnk.CommandArgument.Split('_');
        ViewState["BRCD"] = id[0].ToString();
        ViewState["SOCIeTYID"] = id[1].ToString();
        ViewState["CASENO"] = id[2].ToString();
        ViewState["CASE_YEAR"] = id[3].ToString();
        string BRCD = ViewState["BRCD"].ToString();
        txtCaseNO.Text = ViewState["CASENO"].ToString();
        txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
        string srno = ViewState["SOCIeTYID"].ToString();
        ViewDetailsSOCIETY(BRCD: BRCD, CASENO: txtCaseNO.Text, CASE_YEAR: txtCaseY.Text,ID:srno);
        ViewState["FLAG"] = "SOCMDC";
        BtnSubmit.Text = "MODIFY";
    }
    protected void lnkDelete_Click1(object sender, EventArgs e)
    {
        {
            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');
            ViewState["BRCD"] = id[0].ToString();
            ViewState["SOCIeTYID"] = id[1].ToString();
            ViewState["CASENO"] = id[2].ToString();
            ViewState["CASE_YEAR"] = id[3].ToString();
            string BRCD = ViewState["BRCD"].ToString();
            txtCaseNO.Text = ViewState["CASENO"].ToString();
            txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
            string srno = ViewState["SOCIeTYID"].ToString();

            result = SRO.DeletSocietyRec(BRCD: BRCD, CASENO: txtCaseNO.Text, CASEYEAR: txtCaseY.Text,id:srno);
            if (result > 0)
            {


                WebMsgBox.Show("Delete successfully", this.Page);
              

            }
            else
            {
                WebMsgBox.Show("Not  Delete", this.Page);
            }
            BindGrdMain();
        }

    }
    
    protected void GRdCASE_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GRdCASE.PageIndex = e.NewPageIndex;
            BindGrdMainCase();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Lnkedit_Click(object sender, EventArgs e)
    {
        LinkButton objlnk = (LinkButton)sender;
        string[] id = objlnk.CommandArgument.Split('_');
        ViewState["BRCD"] = id[0].ToString();
        ViewState["SOCIeTYID"] = id[1].ToString();
        ViewState["CASENO"] = id[2].ToString();
        ViewState["CASE_YEAR"] = id[3].ToString();
        string BRCD = ViewState["BRCD"].ToString();
        txtCaseNO.Text = ViewState["CASENO"].ToString();
        txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
        string srno = ViewState["SOCIeTYID"].ToString();
        ViewDetailsCASESTATUS(BRCD: BRCD, CASENO: txtCaseNO.Text, CASE_YEAR: txtCaseY.Text,id1:srno);
        ViewState["FLAG"] = "CASEMDCCS";
        BtnSubmit.Text = "MODIFY";
    }
    protected void lnkcancle_Click(object sender, EventArgs e)
    {
        LinkButton objlnk = (LinkButton)sender;
        string[] id = objlnk.CommandArgument.Split('_');
        ViewState["BRCD"] = id[0].ToString();
        ViewState["SOCIeTYID"] = id[1].ToString();
        ViewState["CASENO"] = id[2].ToString();
        ViewState["CASE_YEAR"] = id[3].ToString();
        string BRCD = ViewState["BRCD"].ToString();
        txtCaseNO.Text = ViewState["CASENO"].ToString();
        txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
        string srno = ViewState["SOCIeTYID"].ToString();

        result = SRO.DeletCASESTATUS(BRCD: BRCD, CASENO: txtCaseNO.Text, CASEYEAR: txtCaseY.Text,id:srno);
        if (result > 0)
        {


            WebMsgBox.Show("Delete successfully", this.Page);


        }
        else
        {
            WebMsgBox.Show("Not  Delete", this.Page);
        }
        BindGrdMainCase();
    }
    protected void lnkShow_Click(object sender, EventArgs e)
    {
        LinkButton objlnk = (LinkButton)sender;
        string[] id = objlnk.CommandArgument.Split('_');
        ViewState["BRCD"] = id[0].ToString();
        ViewState["SOCIeTYID"] = id[1].ToString();
        ViewState["CASENO"] = id[2].ToString();
        ViewState["CASE_YEAR"] = id[3].ToString();
        string BRCD = ViewState["BRCD"].ToString();
        txtCaseNO.Text = ViewState["CASENO"].ToString();
        txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
        string srno = ViewState["SOCIeTYID"].ToString();
        ViewDetailsCASESTATUS(BRCD: BRCD, CASENO: txtCaseNO.Text, CASE_YEAR: txtCaseY.Text,id1:srno);
    }
    protected void GRDACTION_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GRDACTION.PageIndex = e.NewPageIndex;
            BindGrdMainACTION();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
   
    protected void lnkModify2_Click(object sender, EventArgs e)
    {
        LinkButton objlnk = (LinkButton)sender;
        string[] id = objlnk.CommandArgument.Split('_');
        ViewState["BRCD"] = id[0].ToString();
        ViewState["SOCIeTYID"] = id[1].ToString();
        ViewState["CASENO"] = id[2].ToString();
        ViewState["CASE_YEAR"] = id[3].ToString();
        string BRCD = ViewState["BRCD"].ToString();
        txtCaseNO.Text = ViewState["CASENO"].ToString();
        txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
        string srno = ViewState["SOCIeTYID"].ToString();
        ViewDetailsACTION(BRCD: BRCD, CASENO: txtCaseNO.Text, CASE_YEAR: txtCaseY.Text,id1:srno);
        ViewState["FLAG"] = "ACTIONMDCAS";
        BtnSubmit.Text = "MODIFY";
    }
    protected void lnkDelete2_Click(object sender, EventArgs e)
    {
        LinkButton objlnk = (LinkButton)sender;
        string[] id = objlnk.CommandArgument.Split('_');
        ViewState["BRCD"] = id[0].ToString();
        ViewState["SOCIeTYID"] = id[1].ToString();
        ViewState["CASENO"] = id[2].ToString();
        ViewState["CASE_YEAR"] = id[3].ToString();
        string BRCD = ViewState["BRCD"].ToString();
        txtCaseNO.Text = ViewState["CASENO"].ToString();
        txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
        string srno = ViewState["SOCIeTYID"].ToString();

        result = SRO.DeletACTIONSTATUS(BRCD: BRCD, CASENO: txtCaseNO.Text, CASEYEAR: txtCaseY.Text,id:srno);
        if (result > 0)
        {


            WebMsgBox.Show("Delete successfully", this.Page);


        }
        else
        {
            WebMsgBox.Show("Not  Delete", this.Page);
        }
        BindGrdMainACTION();

    }
    protected void lnkView2_Click(object sender, EventArgs e)
    {
        LinkButton objlnk = (LinkButton)sender;
        string[] id = objlnk.CommandArgument.Split('_');
        ViewState["BRCD"] = id[0].ToString();
        ViewState["SOCIeTYID"] = id[1].ToString();
        ViewState["CASENO"] = id[2].ToString();
        ViewState["CASE_YEAR"] = id[3].ToString();
        string BRCD = ViewState["BRCD"].ToString();
        txtCaseNO.Text = ViewState["CASENO"].ToString();
        txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
        string srno = ViewState["SOCIeTYID"].ToString();
        ViewDetailsACTION(BRCD: BRCD, CASENO: txtCaseNO.Text, CASE_YEAR: txtCaseY.Text,id1:srno);
    }


    protected void lnkstAcRec_Click(object sender, EventArgs e)
    {
        ViewState["FLAG"] = "ACTIONADAC";
        div_Grid.Visible = false;
        DIVCASE.Visible = false;
        BtnDownload.Visible = true;
        DIVACTION.Visible = false;
        BtnSubmit.Visible = false;
      //  txtMember.Enabled = false;
       // TxtSRNO.Enabled = false;
      //  txtSociName.Enabled = false;
        txtamtd.Visible = false;
        txtRecAmt.Visible = false;
       // txtRemark.Enabled = false;
        txtBalance.Visible = false;
      //  ddlActstatus.Enabled = false;
       // ddlcasestaus.Enabled = false;
       // lstarea.Enabled = false;
       // TXTSROName.Enabled = false;
        //divrec.Visible = false;
        //Div3.Visible = true;
        lbl1.Visible = true;
        TXTTODATE.Visible = true;
        t1.Visible = true;
        txtfdate.Visible = true;
        p1.Visible = true;
        txtpd.Visible = true;
        BtnReport.Visible = true;
        TXTPRINCIPL.Visible = true;
        txtotherharges.Visible = true;
        r1.Visible = false;
        b1.Visible = false;
        d1.Visible = false;
        P2.Visible = true;
        o1.Visible = true;
        pm1.Visible = true;
        pbd.Visible = true;
        cn1.Visible = true;
        cd1.Visible = true;
        divstate.Visible = true;
        ddlPaymentMode.Visible = true;
        txtAmount.Visible = true;
        txtChequeNo.Visible = true;
        TxtChequeDate.Visible = true;
        btnpayment.Visible = true;
       // BindGrdMainstatement();
        txttotalamt.Visible = true;
        lbltot.Visible = true;
        txtbal.Visible = true;
        lblbal.Visible = true;
        Div6.Visible = true;
        txtrecamt1.Visible = true;
        lblra.Visible = true;
        ClearSociety();
        //BindGrdMainSTATEMENTACCE();

    }
    
    protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {
    //    if (ddlPaymentMode.SelectedValue == "1")
    //    {
    //        Div15.Visible = false;
    //        txtAmount.Focus();
    //    }
    //    else if (ddlPaymentMode.SelectedValue == "4")
    //    {
    //        Div15.Visible = true;
    //        txtAmount.Focus();

    //    }
    }
    protected void BtnReport_Click(object sender, EventArgs e)
    {
        string redirectURL = "FrmRView.aspx?CASENO=" + txtCaseNO.Text + "&CASEY=" + txtCaseY.Text + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + txtDate.Text + "&BRCD=" + Session["BRCD"].ToString() + "&FN=R&rptname=RPTSTATEMENTACCREC.rdlc";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
      
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        txtamtd.Focus();
    }
    protected void TXTTODATE_TextChanged(object sender, EventArgs e)
    {
        string EMI = "";
        DateTime FromYear = Convert.ToDateTime(txtfdate.Text);
        DateTime ToYear = Convert.ToDateTime(TXTTODATE.Text);
        int Years = ToYear.Year - FromYear.Year;
        int month = (ToYear.Month + 1) - FromYear.Month;
        int TotalMonths = (Years * 12) + month;
      //  txtMonth.Text = TotalMonths.ToString();
        if (TXTTODATE.Text == txtfdate.Text)
        {
            txtpd.Text = "00";
        }
        else
        {
            float PriAmt = Convert.ToSingle(TXTPRINCIPL.Text);
           // float Rate = Convert.ToSingle(rate);// 21;// Convert.ToSingle(txtRate.Text);
          //  EMI = Math.Round(Convert.ToDouble((PriAmt * Convert.ToDouble(Rate)) / 100)).ToString();
              DT = SRO.ViewDetailsDem(Session["brcd"].ToString(),txtCaseNO.Text, txtCaseY.Text);


              if (DT.Rows.Count > 0)
              {

                  rate = Convert.ToSingle(DT.Rows[0]["rate"].ToString());
              }
                
            EMI = Math.Round(Convert.ToDouble((PriAmt * (Convert.ToDouble(rate))) / 100)).ToString();

            txtpd.Text = Math.Round(Convert.ToDouble((((Convert.ToDouble(EMI)) / 12) * TotalMonths)), 0).ToString();
        }
        float totrecamt = Convert.ToSingle(txtrecamt1.Text);
        float totalamount = Convert.ToSingle(txtpd.Text);
        float otherchr = Convert.ToSingle(txtotherharges.Text);
        txttotalamt.Text = Math.Round(Convert.ToDouble((totrecamt + totalamount + otherchr))).ToString();
        txtAmount.Focus();
        
    }
    protected void btnpayment_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["FLAG"]) == "ACTIONADAC")
        {

            result = SRO.STATEMENTACC(BRCD: Session["BRCD"].ToString(), SRO_NO: TxtSRNO.Text, CASE_YEAR: txtCaseY.Text, CASENO: txtCaseNO.Text, MEMBERNO: txtMember.Text, DEFAULTERNAME: lstarea.Text.ToString(), ENTRYDATE: txtDate.Text, MID: Session["MID"].ToString(),
                CID: Session["MID"].ToString(), VID: Session["MID"].ToString(), PCMAC: "", Remark: txtRemark.Text, PAYMENTMODE: ddlPaymentMode.SelectedValue, CHEQUENO: txtChequeNo.Text, principalamt2: TXTPRINCIPL.Text, Todate1: TXTTODATE.Text, fromdate1: txtfdate.Text, chequedate: TxtChequeDate.Text, OTHECHS: txtotherharges.Text,
                CaseStatus1: ddlcasestaus.SelectedValue, ACTIONSTATUS1: ddlActstatus.SelectedValue, TOTINT2: txtpd.Text, AMTPAID: txtAmount.Text, BAL: txtbal.Text, hrem: txtrecamt1.Text, NOTICESTAGE:ddlNotice.SelectedValue    );



            if (result > 0)
            {
                WebMsgBox.Show("Data Saved successfully", this.Page);
                ClearSociety();
                BindGrdMainSTATEMENTACCE();
                BindGrdMainSTATEMENTACCEAll();
            }
               
            //  BindGrdMainstatement();
            else
            {
                WebMsgBox.Show("Data Not  Saved successfully", this.Page);
            }
        }

        if (Convert.ToString(ViewState["FLAG"]) == "ACTIONMDSACR")
        {

            result = SRO.MODIFYRSTATEMENTACC(BRCD: Session["BRCD"].ToString(), SRO_NO: TxtSRNO.Text, CASE_YEAR: txtCaseY.Text, CASENO: txtCaseNO.Text, MEMBERNO: txtMember.Text, DEFAULTERNAME: lstarea.Text.ToString(), ENTRYDATE: txtDate.Text, MID: Session["MID"].ToString(),
                CID: Session["MID"].ToString(), VID: Session["MID"].ToString(), PCMAC: "", Remark: txtRemark.Text, PAYMENTMODE: ddlPaymentMode.SelectedValue, CHEQUENO: txtChequeNo.Text, principalamt2: TXTPRINCIPL.Text, Todate1: TXTTODATE.Text, fromdate1: txtfdate.Text, chequedate: TxtChequeDate.Text, OTHECHS: txtotherharges.Text,
                CaseStatus1: ddlcasestaus.SelectedValue, ACTIONSTATUS1: ddlActstatus.SelectedValue, TOTINT2: txtpd.Text, AMTPAID: txtAmount.Text, BAL: txtbal.Text, hrem: txtrecamt1.Text, NOTICESTAGE: ddlNotice.SelectedValue);



            if (result > 0)
            {
                WebMsgBox.Show("Data MODIFY successfully", this.Page);
                ClearSociety();
                BindGrdMainSTATEMENTACCE();
                BindGrdMainSTATEMENTACCEAll();
            }

            //  BindGrdMainstatement();
            else
            {
                WebMsgBox.Show("Data Not  MODIFY successfully", this.Page);
            }
        }
         if (Convert.ToString(ViewState["FLAG"]) == "MDACCSTAll")
        {
            string SRID = ViewState["Id"].ToString(); 
            result = SRO.MODSTATEMENTACCALL(BRCD: Session["BRCD"].ToString(), SRO_NO: TxtSRNO.Text, CASE_YEAR: txtCaseY.Text, CASENO: txtCaseNO.Text, MEMBERNO: txtMember.Text, DEFAULTERNAME: lstarea.Text.ToString(), ENTRYDATE: txtDate.Text, MID: Session["MID"].ToString(),
                CID: Session["MID"].ToString(), VID: Session["MID"].ToString(), PCMAC: "", Remark: txtRemark.Text, PAYMENTMODE: ddlPaymentMode.SelectedValue, CHEQUENO: txtChequeNo.Text, principalamt2: TXTPRINCIPL.Text, Todate1: TXTTODATE.Text, fromdate1: txtfdate.Text, chequedate: TxtChequeDate.Text, OTHECHS: txtotherharges.Text,
                CaseStatus1: ddlcasestaus.SelectedValue, ACTIONSTATUS1: ddlActstatus.SelectedValue, TOTINT2: txtpd.Text, AMTPAID: txtAmount.Text, BAL: txtbal.Text, hrem: txtrecamt1.Text, id: SRID.ToString(),NOTICESTAGE:ddlNotice.SelectedValue);



            if (result > 0)
            {
                WebMsgBox.Show("Data Modify successfully", this.Page);
                ClearSociety();
                BindGrdMainSTATEMENTACCE();
                BindGrdMainSTATEMENTACCEAll();
            }
               
            //  BindGrdMainstatement();
            else
            {
                WebMsgBox.Show("Data Not  Modify successfully", this.Page);
            }
        }
    }
    protected void txtotherharges_TextChanged(object sender, EventArgs e)
    {
        txtfdate.Focus();
    }
    protected void txtfdate_TextChanged(object sender, EventArgs e)
    {
        TXTTODATE.Focus();
    }
    protected void Grd_statement_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

      
    }
    protected void lnkSTAmd_Click(object sender, EventArgs e)
    {

    }
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        float paidamount = Convert.ToSingle(txtAmount.Text);
        float totalamt = Convert.ToSingle(txttotalamt.Text);
        txtbal.Text = Math.Round(Convert.ToDouble((totalamt - paidamount ))).ToString();
    }
    protected void GRIDVIEWACCST_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGrdMainSTATEMENTACCE();
    }
    protected void lnkSTACCMD_Click(object sender, EventArgs e)
    {
        LinkButton objlnk = (LinkButton)sender;
        string[] id = objlnk.CommandArgument.Split('_');
        ViewState["BRCD"] = id[0].ToString();
        ViewState["CASENO"] = id[1].ToString();
        ViewState["CASE_YEAR"] = id[2].ToString();
        string BRCD = ViewState["BRCD"].ToString();
        txtCaseNO.Text = ViewState["CASENO"].ToString();
        txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
       ViewDetailsSTATEMENT(BRCD: BRCD, CASENO: txtCaseNO.Text, CASE_YEAR: txtCaseY.Text);
       btnpayment.Text = "Modify";
       ViewState["FLAG"] = "ACTIONMDSACR";
        

    }

    public void ViewDetailsSTATEMENT(string BRCD, string CASENO, string CASE_YEAR)
    {
        try
        {
            string id = "";
            DataTable DT1 = new DataTable();
            DT = SRO.ViewDetailSTATEMENTACC(BRCD, CASENO, CASE_YEAR);
           // DT1 = SRO.ViewDetailsDefaulterS(BRCD, CASENO, CASE_YEAR);
            //if (DT1.Rows.Count > 0)
            //{
            //    for (int i = 0; i < DT1.Rows.Count; i++)
            //    {
            //        lstarea.Items.Add(Convert.ToString(DT1.Rows[i]["ID"]) + '|' + Convert.ToString(DT1.Rows[i]["DEFAULTERNAME"]));
            //        lstarea.Visible = true;
                   
            //    }
            //}
              if (DT.Rows.Count > 0)
            {
                TxtSRNO.Text = DT.Rows[0]["SRO_NO"].ToString();
                TXTSROName.Text = SRO.GetSROName(TxtSRNO.Text);
                txtMember.Text = DT.Rows[0]["MEMBERNO"].ToString();
                FL = "GETMEMBERNAME";
                txtSociName.Text = SRO.GetMemberID2(txtMember.Text, FL);
               

                txtpd.Text = DT.Rows[0]["TOTINT2"].ToString();
                TXTPRINCIPL.Text = DT.Rows[0]["principalamt2"].ToString();
                txtotherharges.Text = DT.Rows[0]["othercharges"].ToString();
                ddlPaymentMode.SelectedValue = DT.Rows[0]["PAYMENTMODE"].ToString();
                txtAmount.Text = DT.Rows[0]["AMTPAID"].ToString();
                ddlActstatus.SelectedValue = DT.Rows[0]["ActionStatus1"].ToString();
                ddlcasestaus.SelectedValue = DT.Rows[0]["CaseStatus1"].ToString();
                  
               
      
                txtbal.Text = DT.Rows[0]["REMBAL"].ToString();
                txtrecamt1.Text = DT.Rows[0]["HREMBAL"].ToString();
                txtRemark.Text=DT.Rows[0]["Remark"].ToString();
                txtDate.Text = Convert.ToDateTime(DT.Rows[0]["ENTRYDATE1"]).ToString("dd/MM/yyyy");
                TXTTODATE.Text = Convert.ToDateTime(DT.Rows[0]["Todate1"]).ToString("dd/MM/yyyy");

                txtfdate.Text = Convert.ToDateTime(DT.Rows[0]["fromdate1"]).ToString("dd/MM/yyyy");
                float totrecamt = Convert.ToSingle(txtrecamt1.Text);
                float totalamount = Convert.ToSingle(txtpd.Text);
                float otherchr = Convert.ToSingle(txtotherharges.Text);
                txttotalamt.Text = Math.Round(Convert.ToDouble((totrecamt + totalamount + otherchr))).ToString();
                ddlNotice.SelectedValue = DT.Rows[0]["NOTICESTAGE"].ToString();
              
                //float PRINCI = Convert.ToSingle(TXTPRINCIPL.Text);
                //float REMBAL = Convert.ToSingle(txtrecamt1.Text);
                //if (PRINCI >= REMBAL)
                //{
                //    TXTPRINCIPL.Text = DT.Rows[0]["HREMBAL"].ToString();

                //}
                
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
    public void ViewDetailsSTATEMENTAll(string BRCD, string CASENO, string CASE_YEAR,string id1)
    {
        try
        {
            string id = "";
            DataTable DT1 = new DataTable();
            DT = SRO.ViewDetailSTATEMENTACCAll(BRCD, CASENO, CASE_YEAR, id1);
            // DT1 = SRO.ViewDetailsDefaulterS(BRCD, CASENO, CASE_YEAR);
            //if (DT1.Rows.Count > 0)
            //{
            //    for (int i = 0; i < DT1.Rows.Count; i++)
            //    {
            //        lstarea.Items.Add(Convert.ToString(DT1.Rows[i]["ID"]) + '|' + Convert.ToString(DT1.Rows[i]["DEFAULTERNAME"]));
            //        lstarea.Visible = true;

            //    }
            //}
            if (DT.Rows.Count > 0)
            {
                TxtSRNO.Text = DT.Rows[0]["SRO_NO"].ToString();
                TXTSROName.Text = SRO.GetSROName(TxtSRNO.Text);
                txtMember.Text = DT.Rows[0]["MEMBERNO"].ToString();
                FL = "GETMEMBERNAME";
                txtSociName.Text = SRO.GetMemberID2(txtMember.Text, FL);
               

                txtpd.Text = DT.Rows[0]["TOTINT2"].ToString();
                TXTPRINCIPL.Text = DT.Rows[0]["principalamt2"].ToString();
                txtotherharges.Text = DT.Rows[0]["othercharges"].ToString();
                ddlPaymentMode.SelectedValue = DT.Rows[0]["PAYMENTMODE"].ToString();
                txtAmount.Text = DT.Rows[0]["AMTPAID"].ToString();
                 txtbal.Text = DT.Rows[0]["REMBAL"].ToString();
                txtrecamt1.Text = DT.Rows[0]["HREMBAL"].ToString();
                txtRemark.Text = DT.Rows[0]["Remark"].ToString();
                txtDate.Text = Convert.ToDateTime(DT.Rows[0]["ENTRYDATE1"]).ToString("dd/MM/yyyy");
                TXTTODATE.Text = Convert.ToDateTime(DT.Rows[0]["Todate1"]).ToString("dd/MM/yyyy");
                ddlActstatus.SelectedValue = DT.Rows[0]["ActionStatus1"].ToString();
                ddlcasestaus.SelectedValue = DT.Rows[0]["CaseStatus1"].ToString();
                txtfdate.Text = Convert.ToDateTime(DT.Rows[0]["fromdate1"]).ToString("dd/MM/yyyy");
                float totrecamt = Convert.ToSingle(txtrecamt1.Text);
                float totalamount = Convert.ToSingle(txtpd.Text);
                float otherchr = Convert.ToSingle(txtotherharges.Text);
                txttotalamt.Text = Math.Round(Convert.ToDouble((totrecamt + totalamount + otherchr))).ToString();
                ddlNotice.SelectedValue = DT.Rows[0]["NOTICESTAGE"].ToString();
                //float PRINCI = Convert.ToSingle(TXTPRINCIPL.Text);
                //float REMBAL = Convert.ToSingle(txtrecamt1.Text);
                //if (PRINCI >= REMBAL)
                //{
                //    TXTPRINCIPL.Text = DT.Rows[0]["HREMBAL"].ToString();

                //}


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
    protected void grd_allstatement_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGrdMainSTATEMENTACCEAll();
    }
    protected void lnkSTACCMDAll_Click(object sender, EventArgs e)
    {
        LinkButton objlnk = (LinkButton)sender;
        string[] id = objlnk.CommandArgument.Split('_');
        ViewState["Id"] = id[0].ToString();
        ViewState["BRCD"] = id[1].ToString();
        ViewState["CASENO"] = id[2].ToString();
        ViewState["CASE_YEAR"] = id[3].ToString();
        string id1 = ViewState["Id"].ToString();
        string BRCD = ViewState["BRCD"].ToString();
        txtCaseNO.Text = ViewState["CASENO"].ToString();
        txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
        ViewDetailsSTATEMENTAll(BRCD: BRCD, CASENO: txtCaseNO.Text, CASE_YEAR: txtCaseY.Text,id1:id1);
        btnpayment.Text = "Modify";
        ViewState["FLAG"] = "MDACCSTAll";
    }
    protected void LnkCAnALL_Click(object sender, EventArgs e)
    {
        LinkButton objlnk = (LinkButton)sender;
        string[] id = objlnk.CommandArgument.Split('_');
        ViewState["Id"] = id[0].ToString();
        ViewState["BRCD"] = id[1].ToString();
        ViewState["CASENO"] = id[2].ToString();
        ViewState["CASE_YEAR"] = id[3].ToString();
        string id1 = ViewState["Id"].ToString();
        string BRCD = ViewState["BRCD"].ToString();
        txtCaseNO.Text = ViewState["CASENO"].ToString();
        txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
       SRO.CANSTATEMENTACC(BRCD: BRCD, CASENO: txtCaseNO.Text, CASE_YEAR: txtCaseY.Text, id: id1);
        WebMsgBox.Show("Record Delete Sucessfully.!!", this.Page);
    }
    protected void ddlNotice_SelectedIndexChanged(object sender, EventArgs e)
    {
       // BD.BindSTAGENOTICE(ddlNotice, ddlActstatus.SelectedValue.ToString());
         
    }
    protected void BtnDownloadcase_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = SRO.Downloadcase1(BRCD: Session["BRCD"].ToString(), CASENO: txtCaseNO.Text, CASE_YEAR: txtCaseY.Text, casestaus1: ddlcasestaus.SelectedValue);
        GridView gv2 = new GridView();
        gv2.DataSource = dt;
        gv2.DataBind();
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=ActionStatusExcelDownload.xls");
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gv2.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.End();
    }
    protected void BtnDownload_action_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = SRO.DownloadACTION(BRCD: Session["BRCD"].ToString(), CASENO: txtCaseNO.Text, CASE_YEAR: txtCaseY.Text, ActionStatus1: ddlActstatus.SelectedValue, NOTICESTAGE: ddlNotice.SelectedValue);
        GridView gv2 = new GridView();
        gv2.DataSource = dt;
        gv2.DataBind();
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=CASEExcelDownload.xls");
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gv2.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.End();
    }
}