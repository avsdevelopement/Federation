
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

public partial class FrmS0002 : System.Web.UI.Page
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
    string sroname = "", AC_Status = "", stage = "", usrgrp = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            autoglname.ContextKey = Session["brcd"].ToString();

            ViewState["FLAG"] = "AD";
            if (ViewState["FLAG"].ToString() == "AD")
            {
                TxtNoticeIssDt.Text = Session["EntryDate"].ToString();
                TxtNoticeIssDt.Enabled = true;
                BtnSubmit.Text = "Submit";
                Div_Submit.Visible = true;
               // Div15.Visible = false;
                div_All.Visible = true;
                BD.BindMemberType(ddlMemType);
                TxtBRCD.Text = Session["BRCD"].ToString();
                TxtBRCDName.Text = Session["BName"].ToString();
                BD.BindWard(txtWard);
                BindGrdMain();
                BD.BindOccupation(ddlOccupation);
                BD.BindDesig(ddlDesignation);
                BD.BindOrder(ddlorder);
                BD.BindAddType(DDLType);
                BD.BindAddType(ddltype1);
                BD.BindRCNO(ddlRCNo);
                txtdefaulterNo.Text = SRO.DefaulterNo(CASENO:txtCaseNo.Text,CASE_YEAR:txtCaseY.Text);
                lbl101Rc.Text = "101 RC NO.";
                lbl101rcd.Text = "101 RC Order Date";
               // BD.BindPaymentMode(ddlPaymentMode);
                DT = SRO.GetCaseYear(Session["EntryDate"].ToString());
                if (DT.Rows.Count > 0)
                {
                    txtCaseNo.Text = DT.Rows[0]["CASENO"].ToString();
                    txtCaseY.Text = DT.Rows[0]["CASE_YEAR"].ToString();
                }
              
            }


          
            BindGrdMain();
        }
    }


    protected void TxtBRCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBRCD.Text != "")
            {
                string bname = AST.GetBranchName(TxtBRCD.Text);
                if (bname != null)
                {
                    TxtBRCDName.Text = bname;
                    txtCaseNo.Focus();

                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    TxtBRCD.Text = "";
                    TxtBRCD.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                TxtBRCD.Text = "";
                TxtBRCD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
             
           
            stage = SRO.GETSTAGE(TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
            if (ViewState["FLAG"].ToString() == "AD")
            {

                //if (txtDefaulterName.Text != "")
                //{
                //    WebMsgBox.Show("Click On Add For Insert Defaulter Record  ", this.Page);

                //}
                //if (txtDefaultProperty.Text != "")
                //{
                //    WebMsgBox.Show("Click On Add For Insert Defaulter Record  ", this.Page);

                //}

                result = SRO.InsertDem(BRCD: TxtBRCD.Text, CASENO: txtCaseNo.Text, CASEYAER: txtCaseY.Text, MEMNO: txtMember.Text, DEFNAME: txtDefaulterName.Text, SRO_NO: TxtSRNO.Text, C_F_N_101: TxtCaseFlNo.Text, C_F_D_101: TxtCaseFlDate.Text, C_F_N_91: TxtCaseFlNo91.Text, C_F_DT_91: TxtCaseFlDate91.Text, NOTICE_ISS_DT: TxtNoticeIssDt.Text, TOT_RECV: TxtTotalRec.Text, DIV_CITY: TxtDivCityName.Text, COURT_NAME: "", BSD_SRO: "", AWARD_EXP: txtAwardAmt.Text, TALATHI_OW: "", COMP_OW: "", REMARK1: "", CaseSts: "", MID: Session["MID"].ToString(), R_O_DT: TxtRODT.Text, PrincipleAmount: txtPriAmt.Text, Rate: txtRate.Text, Fdate: txtFromDate.Text, Tdate: txtToDate.Text, diffMonth: txtMonth.Text, totInt: txtInterest.Text, WARD: txtWard.Text, PINCODE: txtPincode.Text, COSTPROCESS: txtCostProcss.Text, COSTAPPLICATION: txtCost.Text, DESIGNATION: ddlDesignation.SelectedValue.ToString(), COMM_NAME: txtNamed.Text, COM_MOBILE1: txtdMob1.Text, COM_MOBILE2: txtdMob2.Text, COM_ADDRESS: txtDeflterAddress.Text, COM_WARD: dddlWardComm.SelectedValue, COM_CITY: txtCityComm.Text, COM_PINCODE: txtPincodeComm.Text, DEFAULTVALUE: txtDefaultValue.Text, MEMTYPE: ddlMemType.SelectedValue.ToString(), RCNOTYPE: ddlRCNo.SelectedValue.ToString(), ORDERBY: ddlorder.SelectedValue.ToString(), PROPERTYTYPE: DDLType.SelectedValue.ToString(), PROPERTYNO: txtTypeNo.Text,FloorNO:txtFloor.Text, EXECUTIONCHARG:txtExecutionC.Text,AMOUNT:"0",PAYMENTMODE:"0",CHEQUENO:"0");//S_O_DT: TxtSAODT.Text, ImmuvableDate: txtImmuvableDate.Text, MovableDate: txtMovable.Text,

              if (result > 0)
                {
                   // result = InsertDefaulterName();
              
                    WebMsgBox.Show("Data Saved successfully", this.Page);

                    div_All.Visible = true;
                    Div_Submit.Visible = true;
                    div_Grid.Visible = true;
                    grdCommitee.Visible = false;
                    grdDefulter.Visible = false;
                    div_Sro.Visible = true;
                    BindGrdMain();
                    FL = "Insert";//Dhanya Shetty
                    string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_Demand_Add _" + txtCaseNo.Text + "_" + txtMember.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    clear();

                }
            }
            else if (ViewState["FLAG"].ToString() == "MD")
            {
                result = SRO.ModifyDem(BRCD: TxtBRCD.Text, CASENO: txtCaseNo.Text, CASEYAER: txtCaseY.Text, MEMNO: txtMember.Text, DEFNAME: txtDefaulterName.Text, SRO_NO: TxtSRNO.Text, C_F_N_101: TxtCaseFlNo.Text, C_F_D_101: TxtCaseFlDate.Text, C_F_N_91: TxtCaseFlNo91.Text, C_F_DT_91: TxtCaseFlDate91.Text, NOTICE_ISS_DT: TxtNoticeIssDt.Text, TOT_RECV: TxtTotalRec.Text, DIV_CITY: TxtDivCityName.Text, COURT_NAME: "", BSD_SRO: "", AWARD_EXP: txtAwardAmt.Text, TALATHI_OW: "", COMP_OW: "", REMARK1: "", CaseSts: "", MID: Session["MID"].ToString(), R_O_DT: TxtRODT.Text, PrincipleAmount: txtPriAmt.Text, Rate: txtRate.Text, Fdate: txtFromDate.Text, Tdate: txtToDate.Text, diffMonth: txtMonth.Text, totInt: txtInterest.Text, WARD: txtWard.Text, PINCODE: txtPincode.Text, COSTPROCESS: txtCostProcss.Text, COSTAPPLICATION: txtCost.Text, DESIGNATION: ddlDesignation.SelectedValue.ToString(), COMM_NAME: txtNamed.Text, COM_MOBILE1: txtdMob1.Text, COM_MOBILE2: txtdMob2.Text, COM_ADDRESS: txtDeflterAddress.Text, COM_WARD: dddlWardComm.SelectedValue, COM_CITY: txtCityComm.Text, COM_PINCODE: txtPincodeComm.Text, DEFAULTVALUE: txtDefaultValue.Text, MEMTYPE: ddlMemType.SelectedValue.ToString(), RCNOTYPE: ddlRCNo.SelectedValue.ToString(), ORDERBY: ddlorder.SelectedValue.ToString(), PROPERTYTYPE: DDLType.SelectedValue.ToString(), PROPERTYNO: txtTypeNo.Text, FloorNO: txtFloor.Text, EXECUTIONCHARG: txtExecutionC.Text, AMOUNT: "0", PAYMENTMODE: "0", CHEQUENO: "0");// S_O_DT: TxtSAODT.Text, ImmuvableDate: txtImmuvableDate.Text, MovableDate: txtMovable.Text, 
               
                if (result > 0)
                {
                   // result = ModifyDefaulterName();
                    WebMsgBox.Show("Data Modified successfully..!!", this.Page);

                    div_All.Visible = true;
                    Div_Submit.Visible = false;
                    div_Grid.Visible = true;
                    grdCommitee.Visible = false;
                    grdDefulter.Visible = false;
                    div_Sro.Visible = true;
                    BindGrdMain();
                    FL = "Insert";//Dhanya Shetty
                    string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_Demand_Mod _" + txtCaseNo.Text + "_" + txtCaseNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    clear();
                    

                }

            }
            else if (ViewState["FLAG"].ToString() == "AT")
            {
                if (stage != "1003" && stage != "1004")
                {
                 
                    result = SRO.AuthoriseDem(BRCD: TxtBRCD.Text, CASENO: txtCaseNo.Text, CASEYEAR: txtCaseY.Text, SRO_NO: TxtSRNO.Text, VID: Session["MID"].ToString(), CaseSts: txtPincode.Text);
                    SRO.AuthoriseDefaulter(CASENO: txtCaseNo.Text, CASEYEAR: txtCaseY.Text);
                    if (result >= 1)
                    {
                        WebMsgBox.Show("Data Authorised successfully..!!", this.Page);

                        div_All.Visible = true;
                        Div_Submit.Visible = false;
                        div_Grid.Visible = true;
                        grdCommitee.Visible = false;
                        grdDefulter.Visible = false;
                        div_Sro.Visible = true;
                        BindGrdMain();
                        FL = "Insert";//Dhanya Shetty
                        string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_Demand_Auth _" + txtCaseNo.Text + "_" + txtMember.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        clear();
                    }
                    else
                    {

                        WebMsgBox.Show("Warning: User is restricted to perform this operation..........!!", this.Page);

                    }
                }
                else if (stage == "1003")
                {
                    WebMsgBox.Show("Data Already Authorised ..!!", this.Page);
                }
            }
            else if (ViewState["FLAG"].ToString() == "CA")
            {
                if (stage != "1004")
                {
                    result = SRO.CancelDem(BRCD: TxtBRCD.Text, CASENO: txtCaseNo.Text, CASEYEAR: txtCaseY.Text, SRO_NO: TxtSRNO.Text, VID: Session["MID"].ToString());
                    if (result > 0)
                    {
                        result = DeleteDefaulterName();
                        WebMsgBox.Show("Data Canceled successfully..!!", this.Page);

                        div_All.Visible = true;
                        Div_Submit.Visible = true;
                        div_Grid.Visible = true;
                        grdCommitee.Visible = false;
                        grdDefulter.Visible = false;
                        div_Sro.Visible = true;
                        BindGrdMain();
                        FL = "Insert";//Dhanya Shetty
                        string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_Demand_Cancel _" + txtCaseNo.Text + "_" + txtMember.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        clear();
                    }
                }
                else if (stage == "1004")
                {
                    WebMsgBox.Show("Data Already Delete .!!", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        try
        {
            clear();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        try
        {
            div_All.Visible = false;
            Div_Submit.Visible = false;
            div_Grid.Visible = true;
            div_Sro.Visible = false;
            clear();
          //  BindGrdMain();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }
    public void  clear1()
    {
        txtDefaulterName.Text = "";
        txtDefaultProperty.Text = "";
        txtCorrespondence.Text = "";
        txtOccupationAdd.Text = "";
        txtMob1.Text = "";
        txtmob2.Text = "";
        ddlOccupation.SelectedValue = "0";
        if (ViewState["FLAG"].ToString() == "AD")
        {
            txtdefaulterNo.Text = SRO.DefaulterNo(CASENO:txtCaseNo.Text,CASE_YEAR:txtCaseY.Text);

        }
        else

            txtdefaulterNo.Text = "";
    }
    public void clearCommitee()
    {
        ddlDesignation.SelectedValue = "0";
        txtNamed.Text = "";
        txtdMob1.Text = "";
        txtDeflterAddress.Text = "";
        ddltype1.SelectedValue = "0";
        txtFloor1.Text = "";
        txtPropertytype1.Text = "";
    }

    public void clear()
    {


        txtmemADD.Text = "";
        txtMember.Text = "";
        txtMemberName.Text = "";
        txtDefaulterName.Text = "";

        TxtSRNO.Text = "";
        TxtCaseFlNo.Text = "";
        TxtCaseFlDate.Text = "";
        TxtCaseFlNo91.Text = "";
        TxtCaseFlDate91.Text = "";
        TxtNoticeIssDt.Text = "";
        TxtTotalRec.Text = "";
        TxtDivCityName.Text = "";
        txtFloor.Text = "";
        TXTSROName.Text = "";
        TxtRODT.Text = "";
         LblMid.Text = "";
        LblVid.Text = "";
        lstarea.Items.Clear();
        lstCorrespondenceAdd.Items.Clear();
        lstDefProperty.Items.Clear();
         txtPriAmt.Text = "";
        txtCostProcss.Text = "";
        txtCost.Text = "";
        txtRate.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtMonth.Text ="";
        txtInterest.Text = "";
        txtWard.SelectedValue = "0";
        txtPincode.Text = "";
        txtDefaulterName.Text = "";
        txtDefaultProperty.Text = "";
        txtCorrespondence.Text = "";
        txtOccupationAdd.Text = "";
        txtMob1.Text = "";
        txtmob2.Text = "";
        ddlorder.SelectedValue = "0";
        ddlOccupation.SelectedValue = "0";
        ddlDesignation.SelectedValue = "0";
        txtDefaultValue.Text = "";
        txtNamed.Text = "";
        txtdMob1.Text = "";
        txtDeflterAddress.Text = "";
        txtExecutionD.Text = "";
        txtAwardAmt.Text = "";
        ddlMemType.SelectedValue = "0";
        txtTypeNo.Text = "";
        DDLType.SelectedValue="0";
     //   ddlPaymentMode.SelectedValue = "0";
      //  txtAmount.Text = "";
        txtExecutionC.Text = "";
       // txtChequeNo.Text = "";



        if (ViewState["FLAG"].ToString() == "AD")
        {
            txtdefaulterNo.Text = SRO.DefaulterNo(CASENO: txtCaseNo.Text, CASE_YEAR: txtCaseY.Text);

        }
        else

            txtdefaulterNo.Text = "";

        if (ViewState["FLAG"].ToString() == "AD")
        {
            TxtBRCD.Text = Session["BRCD"].ToString();

        }
        else
            TxtBRCD.Text = "";
        if (ViewState["FLAG"].ToString() == "AD")
        {
            TxtBRCDName.Text = Session["BName"].ToString();

        }
        else
            TxtBRCDName.Text = "";
        if (ViewState["FLAG"].ToString() == "AD")
        {
            DT = SRO.GetCaseYear(Session["EntryDate"].ToString());
            if (DT.Rows.Count > 0)
            {
                txtCaseNo.Text = DT.Rows[0]["CASENO"].ToString();
                txtCaseY.Text = DT.Rows[0]["CASE_YEAR"].ToString();
            }
        }

        else

            txtCaseNo.Text = "";
        if (ViewState["FLAG"].ToString() == "AD")
        {
            DT = SRO.GetCaseYear(Session["EntryDate"].ToString());
            if (DT.Rows.Count > 0)
            {
                txtCaseNo.Text = DT.Rows[0]["CASENO"].ToString();
                txtCaseY.Text = DT.Rows[0]["CASE_YEAR"].ToString();
            }
        }

        else
            txtCaseY.Text = "";
    }
    public void ViewDetails(string BRCD, string CASENO, string CASE_YEAR)
    {
        try
        {
            DataTable DT1 = new DataTable();
            DT = SRO.ViewDetailsDem(BRCD, CASENO, CASE_YEAR);
            //DT1 = SRO.ViewDetailsDefaulter(BRCD, CASENO, CASE_YEAR);
            //if (DT1.Rows.Count > 0)
            //{
            //    for (int i = 0; i < DT1.Rows.Count; i++)
            //            {
            //               lstarea.Items.Add(Convert.ToString(DT1.Rows[i]["ID"]) +'|' +  Convert.ToString(DT1.Rows[i]["DEFAULTERNAME"]));
            //               lstDefProperty.Items.Add(Convert.ToString(DT1.Rows[i]["DEFAULTPROPERTY"]));
            //               lstCorrespondenceAdd.Items.Add(Convert.ToString(DT1.Rows[i]["CORRESPONDENCEADDRESS"]));
            //             //  lstCorrespondenceAdd.Visible = true;
            //               lstarea.Visible = true;
            //              // lstDefProperty.Visible = true;
            //            }
            //}
            
            
            
            if (DT.Rows.Count > 0)
            {
                TxtSRNO.Text = DT.Rows[0]["SRO_NO"].ToString();
                TXTSROName.Text = SRO.GetSROName(TxtSRNO.Text);
                txtMember.Text = DT.Rows[0]["MEMBERNO"].ToString();
                ddlMemType.SelectedValue = DT.Rows[0]["MEMTYPE"].ToString();
                if (ddlMemType.SelectedValue == "1")
                {
                    FL = "GETMEMBERNAME";
                }
                if (ddlMemType.SelectedValue == "2")
                {
                    FL = "GETMEMBERNAMENOMINAL";
                }
                txtMemberName.Text = SRO.GetMemberID2(txtMember.Text, FL);
              //  txtMemberName.Text = SRO.GetMemberID(txtMember.Text);
             //   txtDefaulterName.Text = DT.Rows[0]["DEFAULTERNAME"].ToString();
                TxtCaseFlNo.Text = DT.Rows[0]["C_F_N_101"].ToString();
                TxtCaseFlDate.Text = DT.Rows[0]["C_F_D_101"].ToString() == "01/01/1900" ? "" : DT.Rows[0]["C_F_D_101"].ToString();
                TxtCaseFlNo91.Text = DT.Rows[0]["C_F_N_91"].ToString();
                TxtCaseFlDate91.Text = DT.Rows[0]["C_F_DT_91"].ToString() == "01/01/1900" ? "" : DT.Rows[0]["C_F_DT_91"].ToString();
                TxtNoticeIssDt.Text = DT.Rows[0]["NOTICE_ISS_DT"].ToString() == "01/01/1900" ? "" : DT.Rows[0]["NOTICE_ISS_DT"].ToString();
                TxtTotalRec.Text = DT.Rows[0]["TOT_RECV"].ToString();
                TxtDivCityName.Text = DT.Rows[0]["DIV_CITY"].ToString();
                txtFloor.Text = DT.Rows[0]["FloorNO"].ToString();
                TxtRODT.Text = DT.Rows[0]["R_O_Dt"].ToString() == "01/01/1900" ? "" : DT.Rows[0]["R_O_Dt"].ToString();
            
                txtPriAmt.Text = DT.Rows[0]["PRINCIPLE"].ToString();
                txtAwardAmt.Text = DT.Rows[0]["AWARD_EXP"].ToString();
                txtExecutionC.Text = DT.Rows[0]["EXECUTIONCHARG"].ToString();
                txtRate.Text = DT.Rows[0]["RATE"].ToString();
                txtFromDate.Text = DT.Rows[0]["FROMDATE"].ToString() == "01/01/1900" ? "" : DT.Rows[0]["FROMDATE"].ToString();
                txtToDate.Text = DT.Rows[0]["TODATE"].ToString() == "01/01/1900" ? "" : DT.Rows[0]["TODATE"].ToString();
                txtMonth.Text = DT.Rows[0]["MONTH"].ToString();
                txtInterest.Text = DT.Rows[0]["TOTINT"].ToString();
                txtWard.SelectedValue = DT.Rows[0]["WARD"].ToString();
                txtPincode.Text = DT.Rows[0]["PINCODE"].ToString();
                txtCost.Text = DT.Rows[0]["COSTAPPLICATION"].ToString();
                txtCostProcss.Text = DT.Rows[0]["COSTPROCESS"].ToString();
                txtDefaultValue.Text = DT.Rows[0]["DEFAULTVALUE"].ToString();
                ddlDesignation.SelectedValue = DT.Rows[0]["DESIGNATION"].ToString();
              //  ddlPaymentMode.SelectedValue = DT.Rows[0]["PAYMENTMODE"].ToString();
               // txtAmount.Text = DT.Rows[0]["AMOUNT"].ToString();
              //  txtChequeNo.Text = DT.Rows[0]["CHEQUENO"].ToString();
                txtNamed.Text = DT.Rows[0]["COMM_NAME"].ToString();
                txtdMob1.Text = DT.Rows[0]["COM_MOBILE1"].ToString();
                txtdMob2.Text = DT.Rows[0]["COM_MOBILE2"].ToString();
                txtDeflterAddress.Text = DT.Rows[0]["COM_ADDRESS"].ToString();
              
                ddlRCNo.SelectedValue = DT.Rows[0]["RCNOTYPE"].ToString();
                ddlorder.SelectedValue = DT.Rows[0]["ORDERBY"].ToString();
                DDLType.SelectedValue = DT.Rows[0]["PROPERTYTYPE"].ToString();
                txtTypeNo.Text = DT.Rows[0]["PROPERTYTYPENO"].ToString();
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

    public void ViewDetailsCommite(string BRCD, string CASENO, string CASE_YEAR,string ID)
    {
        try
        {
            DataTable DT1 = new DataTable();
            DT = SRO.ViewDetailsCOMMI(BRCD, CASENO, CASE_YEAR,ID);
           

            if (DT.Rows.Count > 0)
            {
                TxtSRNO.Text = DT.Rows[0]["SRO_NO"].ToString();
                TXTSROName.Text = SRO.GetSROName(TxtSRNO.Text);
                txtMember.Text = DT.Rows[0]["MEMBERNO"].ToString();
                txtMemberName.Text = SRO.GetMemberID(txtMember.Text);
                //   txtDefaulterName.Text = DT.Rows[0]["DEFAULTERNAME"].ToString();
              
              
                ddlDesignation.SelectedValue = DT.Rows[0]["DESIGNATION"].ToString();
                //  ddlPaymentMode.SelectedValue = DT.Rows[0]["PAYMENTMODE"].ToString();
                // txtAmount.Text = DT.Rows[0]["AMOUNT"].ToString();
                //  txtChequeNo.Text = DT.Rows[0]["CHEQUENO"].ToString();
                txtNamed.Text = DT.Rows[0]["COMM_NAME"].ToString();
                txtdMob1.Text = DT.Rows[0]["COM_MOBILE1"].ToString();
                txtdMob2.Text = DT.Rows[0]["COM_MOBILE2"].ToString();
                txtDeflterAddress.Text = DT.Rows[0]["COM_ADDRESS"].ToString();
                ddltype1.SelectedValue=DT.Rows[0]["PROPERTYTYPE"].ToString();
                txtPropertytype1.Text=DT.Rows[0]["PROPERTYTYPENO"].ToString();
                txtFloor1.Text=DT.Rows[0]["FloorNO"].ToString();
                
             
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
    public bool ENTF(bool TF)
    {
        try
        {
            TxtCaseFlNo.Enabled = TF;
            TxtCaseFlDate.Enabled = TF;
            TxtCaseFlNo91.Enabled = TF;
            TxtCaseFlDate91.Enabled = TF;
            TxtNoticeIssDt.Enabled = TF;
            TxtTotalRec.Enabled = TF;
            TxtDivCityName.Enabled = TF;
             txtAwardAmt.Enabled = TF;
             TxtCaseFlDate91.Enabled = TF;
            TxtRODT.Enabled = TF;
              TxtBRCD.Enabled = TF;
            TxtBRCDName.Enabled = TF;
            txtCaseY.Enabled = TF;
            txtCaseNo.Enabled = TF;
            txtMember.Enabled = TF;
            txtMemberName.Enabled = TF;
            TxtSRNO.Enabled = TF;
            txtDefaulterName.Enabled = TF;
             txtCost.Enabled = TF;
            txtCostProcss.Enabled = TF;
            txtExecutionD.Enabled = TF;
            txtCorrespondence.Enabled = TF;
            txtPriAmt.Enabled = TF;
            txtNamed.Enabled = TF;
            txtdMob1.Enabled = TF;
            txtRate.Enabled = TF;
            txtFromDate.Enabled = TF;
            txtToDate.Enabled = TF;
            txtMonth.Enabled = TF;
            txtInterest.Enabled = TF;
            txtWard.Enabled = TF;
            txtPincode.Enabled = TF;
            txtmob2.Enabled = TF;
            txtMob1.Enabled = TF;
            txtDefaulterName.Enabled = TF;
            txtdefaulterNo.Enabled = TF;
            txtDefaultProperty.Enabled = TF;
            txtDefaultValue.Enabled = TF;
            txtDeflterAddress.Enabled = TF;
            txtOccupationAdd.Enabled = TF;
            ddlDesignation.Enabled = TF;
            ddlOccupation.Enabled = TF;
            ddlMemType.Enabled = TF;
            txtmemADD.Enabled = TF;
            ddlMemType.Enabled = TF;
            ddlorder.Enabled = TF;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return TF;
    }
    protected void TxtSRNO_TextChanged(object sender, EventArgs e)
    {
        try
        {

            sroname = SRO.GetSROName(TxtSRNO.Text);
            TXTSROName.Text = sroname;
           ddlRCNo.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

   
    public void BindGrd()
    {
        try
        {
            result = SRO.BindGrdDem(GrdDemand, TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
            div_Grid.Visible = true;
            if (result > 0)
            {
                div_All.Visible = true;
                Div_Submit.Visible = true;
                div_Sro.Visible = true;
            //   ViewDetails(TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
                if (ViewState["FLAG"].ToString() == "")
                    BtnSubmit.Visible = false;
            }
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
            SRO.BindGrdDemMain(GrdDemand, TxtBRCD.Text);
          
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindGrdCommitee()
    {
        try
        {
            result = SRO.BindGrdCommitee(grdCommitee, TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
            div_Grid.Visible = true;
            if (result > 0)
            {
                div_All.Visible = true;
                Div_Submit.Visible = true;
                div_Sro.Visible = true;
                //   ViewDetails(TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
                if (ViewState["FLAG"].ToString() == "")
                    BtnSubmit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void BindGrdDefaulter()
    {
        try
        {
            result = SRO.BindGrdDefName(grdDefulter, TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
            div_Grid.Visible = true;
            if (result > 0)
            {
                div_All.Visible = true;
                Div_Submit.Visible = true;
                div_Sro.Visible = true;
                //   ViewDetails(TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
                if (ViewState["FLAG"].ToString() == "")
                    BtnSubmit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "VW";
         
            div_All.Visible = true;
            Div_Submit.Visible = true;
            div_Grid.Visible = false;

            BtnSubmit.Visible = false;
            div_Sro.Visible = true;
            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');
            ViewState["BRCD"] = id[0].ToString();
            ViewState["CASENO"] = id[1].ToString();
            ViewState["CASE_YEAR"] = id[2].ToString();
            TxtBRCD.Text = ViewState["BRCD"].ToString();
            txtCaseNo.Text = ViewState["CASENO"].ToString();
            txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
            TxtBRCD.Text = ViewState["BRCD"].ToString();
             TxtBRCDName.Text = AST.GetBranchName(TxtBRCD.Text);
            ViewDetails(ViewState["BRCD"].ToString(), ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString());
            lblActivity.Text = "View Demand";
           // BindGrdMain();
            BindGrd();
            BindGrdDefaulter();
            BindGrdCommitee();
            ENTF(false);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "MD";
            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');
            ViewState["BRCD"] = id[0].ToString();
            ViewState["CASENO"] = id[1].ToString();
            ViewState["CASE_YEAR"] = id[2].ToString();

            TxtBRCD.Text = ViewState["BRCD"].ToString();
            txtCaseNo.Text = ViewState["CASENO"].ToString();
            txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
            TxtBRCDName.Text = AST.GetBranchName(TxtBRCD.Text);
            stage = SRO.GETSTAGE(TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
           if (stage != "1004")
            {
           //   //  usrgrp = SRO.ChkUser(TxtBRCD.Text, Session["UID"].ToString(), Session["UserName"].ToString());

           //    // if (stage == "1003")
           //     {
           //         //if (usrgrp != "1")
           //        // {
           //           //  WebMsgBox.Show("Data has Already authorized..!!", this.Page);
           //           //  clear();
           //            // return;
           //        // }
           //        // else
           //         {
           //             if (ViewState["FLAG"].ToString() != "MD")
           //             {
           //                 WebMsgBox.Show("Data has Already authorized..!!", this.Page);
           //                 clear();
           //                 return;
           //             }
           //         }
           //     }

            }
            div_All.Visible = true;
            Div_Submit.Visible = true;
            div_Grid.Visible = false;
            div_Sro.Visible = true;
            BtnSubmit.Visible = true;
            BtnSubmit.Text = "Modify";
            ViewDetails(ViewState["BRCD"].ToString(), ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString());
           // BindGrdMain();
           // SRO.BindGrdCOMMITEE(grdCommitee, TxtBRCD.Text);
            BindGrdCommitee();
            BindGrdDefaulter();
            BindGrd();
             lblActivity.Text = "Modify Demand";
             btnmodifycomm.Visible = true;
             btnAddComm.Visible = false;
            ENTF(true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkDelete_Click1(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "CA";
            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');
            ViewState["BRCD"] = id[0].ToString();
            ViewState["CASENO"] = id[1].ToString();
            ViewState["CASE_YEAR"] = id[2].ToString();
            TxtBRCD.Text = ViewState["BRCD"].ToString();
            txtCaseNo.Text = ViewState["CASENO"].ToString();
            txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
            TxtBRCDName.Text = AST.GetBranchName(TxtBRCD.Text);
            stage = SRO.GETSTAGE(TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
            if (stage != "1004")
            {
                usrgrp = SRO.ChkUser(TxtBRCD.Text, Session["UID"].ToString(), Session["UserName"].ToString());

                if (stage == "1003")
                {
                    if (usrgrp != "1")
                    {
                        WebMsgBox.Show("Data has Already Deleted..!!", this.Page);
                        clear();
                        return;
                    }

                }

            }
            TxtBRCDName.Text = AST.GetBranchName(TxtBRCD.Text);
             div_All.Visible = true;
            Div_Submit.Visible = true;
            div_Grid.Visible = true;

            div_Sro.Visible = true;
            BtnSubmit.Visible = true;
            BtnSubmit.Text = "Cancel";
            ViewDetails(ViewState["BRCD"].ToString(), ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString());
            BindGrd();
            //SRO.BindGrdCOMMITEE(grdCommitee, TxtBRCD.Text);
            BindGrdCommitee();
            //BindGrdMain();
            BindGrdDefaulter();
              lblActivity.Text = "Cancel Demand";
            ENTF(false);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }
    protected void lnkAuthorize_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "AT";
            LinkButton objlnk = (LinkButton)sender;
            string[] id = objlnk.CommandArgument.Split('_');
            ViewState["BRCD"] = id[0].ToString();
            ViewState["CASENO"] = id[1].ToString();
            ViewState["CASE_YEAR"] = id[2].ToString();
            TxtBRCD.Text = ViewState["BRCD"].ToString();
            txtCaseNo.Text = ViewState["CASENO"].ToString();
            txtCaseY.Text = ViewState["CASE_YEAR"].ToString();
            TxtBRCDName.Text = AST.GetBranchName(TxtBRCD.Text);
            BtnRecipt.Visible = true;
            stage = SRO.GETSTAGE(TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
            if (stage != "1004")
            {
                usrgrp = SRO.ChkUser(TxtBRCD.Text, Session["UID"].ToString(), Session["UserName"].ToString());

                if (stage == "1003")
                {
                    if (usrgrp != "1")
                    {
                        WebMsgBox.Show("Data has Already authorized..!!", this.Page);
                        clear();
                        return;
                    }
                    else
                    {
                        if (ViewState["FLAG"].ToString() != "MD")
                        {
                            WebMsgBox.Show("Data has Already authorized..!!", this.Page);
                            clear();
                            return;
                        }
                    }
                }

            }
            TxtBRCDName.Text = AST.GetBranchName(TxtBRCD.Text);
             div_All.Visible = true;
            Div_Submit.Visible = true;
            div_Grid.Visible = false;

            div_Sro.Visible = true;
            BtnSubmit.Visible = true;
            BtnSubmit.Text = "Authorise";
            ViewDetails(ViewState["BRCD"].ToString(), ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString());
            BindGrd();
         //   SRO.BindGrdCOMMITEE(grdCommitee, TxtBRCD.Text);
            BindGrdCommitee();
            BindGrdDefaulter();
           // BindGrdMain();
             lblActivity.Text = "Authorise Demand";
            ENTF(false);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void GetMidVid(string brcd, string accno, string subgl)
    {
        try
        {
            string[] midvid = SRO.GetmidVid(brcd, accno, subgl).Split('_');
            LblMid.Text = midvid[0].ToString();
            LblVid.Text = midvid[1].ToString();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "CA";
            clear();
            BtnSubmit.Text = "Cancel";
            lblActivity.Text = "Cancel File";
            ENTF(false);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }
    protected void lnkAuthorized_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "AT";
            clear();
            BtnSubmit.Text = "Authorise";
            lblActivity.Text = "Authorise File";

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }
    protected void lnkModify_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "MD";
            BtnSubmit.Text = "Submit";
            Div_Submit.Visible = true;
            clear();
            BtnSubmit.Text = "Modify";
            lblActivity.Text = "Modify File";

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }
    protected void txtMember_TextChanged(object sender, EventArgs e)
    {

        if (ddlMemType.SelectedValue == "1")
        {
            FL = "GETMEMBERNAME";
        }
        if (ddlMemType.SelectedValue == "2")
        {
            FL = "GETMEMBERNAMENOMINAL";
        }
        txtMemberName.Text = SRO.GetMemberID2(txtMember.Text,FL);
        if (txtMemberName.Text == "")
        {
            txtMemberName.Text = txtMemberName.Text.ToString();
        }
        TxtSRNO.Focus();
    }
    protected void txtMemberName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = txtMemberName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtMemberName.Text = CT[0].ToString();
                txtMember.Text = CT[1].ToString();


            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
         
            DateTime FromYear = Convert.ToDateTime(txtFromDate.Text);
        DateTime ToYear = Convert.ToDateTime(txtToDate.Text);
        int Years = ToYear.Year - FromYear.Year;
        int month = (ToYear.Month+1) - FromYear.Month;
        int TotalMonths = (Years * 12) + month;
        txtMonth.Text = TotalMonths.ToString();
        float PriAmt = Convert.ToSingle(txtPriAmt.Text);
        float Rate = Convert.ToSingle(txtRate.Text);
        string EMI = "";
        EMI = Math.Round(Convert.ToDouble((PriAmt * Convert.ToDouble(Rate)) / 100)).ToString();

        txtInterest.Text = Math.Round(Convert.ToDouble((((Convert.ToDouble(EMI)) / 12) * TotalMonths)), 0).ToString();

        double AwardAmt = Convert.ToDouble(txtAwardAmt.Text);
        double INTRest = Convert.ToDouble(txtInterest.Text);
        string EMI1 = "";
        EMI1 = Math.Round(Convert.ToDouble((AwardAmt + Convert.ToDouble(INTRest)))).ToString();

        txtDefaultValue.Text = EMI1 ;
        float PriAmt2 = Convert.ToSingle(txtDefaultValue.Text);

        string EMI2 = "";
       // EMI2 = Math.Round(Convert.ToDouble((PriAmt2) * Convert.ToDouble(2)) / 100)).ToString();
        EMI2 = Math.Round(Convert.ToDouble((PriAmt2)) * 2 / 100).ToString();

        txtExecutionC.Text = EMI2;// Math.Round(Convert.ToDouble((Convert.ToDouble(EMI)))).ToString();
        txtCost.Focus();
      
    }
   
   
    protected void btnarea_Click(object sender, EventArgs e)
    {
       // if (Convert.ToString(ViewState["FLAG"]) == "AD")
        {
          //txtdefaulterNo.Text = SRO.DefaulterNo(Session["brcd"].ToString());
            result = InsertDefaulterName();
            if (result > 0)
            {
                // lstarea.Items.Add(txtDefaulterName.Text);


                WebMsgBox.Show("Defaulter  Add successfully", this.Page);
              //  txtdefaulterNo.Text = SRO.DefaulterNo(CASENO:txtCaseNo.Text,CASE_YEAR:txtCaseY.Text);
                BindGrdDefaulter();
                clear1();
                // lstarea.Visible = true;


            }
            else
            {
                WebMsgBox.Show("Defaulter Not  Save", this.Page);
            }
        }
      
      

       
    }

    
    protected void lstarea_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (lstarea.Items.Count > 0)
            {
              //  btnareaCancel.Visible = false;
                if (Convert.ToString(ViewState["FLAG"]) == "AD")
                {
                    string name = lstarea.SelectedItem.ToString();
                    if (name != "")
                    {
                        txtDefaulterName.Text = name;
                      //  btnarea.Text = "Modify";
                        ViewState["Flag"] = "MD";
                        ViewState["LFlag"] = "MD";
                    }
                    else
                    {
                        btnarea.Text = "ADD";
                        ViewState["LFlag"] = "AD";

                    }
                }

                else if (ViewState["FLAG"].ToString() == "MD")
                {
                      string name = lstarea.SelectedItem.ToString();
                    if (name != "")
                    {
                        DataTable DT1 = new DataTable();
                    
                        //DT1 = SRO.ViewDetailsDefaulter(TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
                        if (DT1.Rows.Count > 0)
                        {

                           
                            // txtdefaulterNo.Text =DT1.Rows[0]["ID"].ToString().Split(|);
                          // txtDefaulterName.Text=  DT1.Rows[0]["DEFAULTERNAME"].ToString();
                              string custno = txtDefaulterName.Text;
                              string[] CT = name.Split('|');
                            if (CT.Length > 0)
                            {
                                txtdefaulterNo.Text = CT[0].ToString();
                               txtDefaulterName.Text = CT[1].ToString();


                            }
                            txtDefaultProperty.Text= DT1.Rows[0]["DEFAULTPROPERTY"].ToString();
                            txtCorrespondence.Text= DT1.Rows[0]["CORRESPONDENCEADDRESS"].ToString();
                            ddlOccupation.SelectedValue = DT1.Rows[0]["OCC_DETAIL"].ToString();
                            txtOccupationAdd.Text = DT1.Rows[0]["OCC_ADD"].ToString();
                            txtMob1.Text = DT1.Rows[0]["MOBILE1"].ToString();
                            txtmob2.Text = DT1.Rows[0]["MOBILE2"].ToString();
                            lstarea.Visible = true;
                           // lstDefProperty.Visible = true;
                        }
                        txtDefaulterName.Text = name;
                        btnarea.Text = "Modify";
                        ViewState["Flag"] = "MD";
                        ViewState["LFlag"] = "MD";
                    }
                    else
                    {
                        btnarea.Text = "ADD";
                        ViewState["LFlag"] = "AD";
                        ViewState["Flag"] = "AD";

                    }


                }
            
            }
            else
            {
            }

            string name1 = lstarea.SelectedItem.ToString();
            if (name1 != "")
            {
                DataTable DT1 = new DataTable();

             //   DT1 = SRO.ViewDetailsDefaulter(TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
                if (DT1.Rows.Count > 0)
                {


                    //  txtdefaulterNo.Text =DT1.Rows[0]["ID"].ToString();
                  //   txtDefaulterName.Text=  DT1.Rows[0]["DEFAULTERNAME"].ToString();
                    //string custno = txtDefaulterName.Text;
                    string[] CT = name1.Split('|');
                 if (CT.Length > 0)
                  {
                    txtdefaulterNo.Text = CT[0].ToString();
                      txtDefaulterName.Text = CT[1].ToString();


                   }
                    txtDefaultProperty.Text = DT1.Rows[0]["DEFAULTPROPERTY"].ToString();
                    txtCorrespondence.Text = DT1.Rows[0]["CORRESPONDENCEADDRESS"].ToString();
                    ddlOccupation.SelectedValue = DT1.Rows[0]["OCC_DETAIL"].ToString();
                    txtOccupationAdd.Text = DT1.Rows[0]["OCC_ADD"].ToString();
                    txtMob1.Text = DT1.Rows[0]["MOBILE1"].ToString();
                    txtmob2.Text = DT1.Rows[0]["MOBILE2"].ToString();
                    lstarea.Visible = true;
                    // lstDefProperty.Visible = true;
                }
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public int InsertDefaulterName()
    {

        try
        {
           {
                List<string> values = new List<string>();
                if (lstarea.Items.Count != 0 || lstDefProperty.Items.Count!=0 || lstCorrespondenceAdd.Items.Count!=0)
                {
                    for (int i = 0; i < lstarea.Items.Count || i<lstDefProperty.Items.Count || i< lstCorrespondenceAdd.Items.Count; i++)
                    {

                        result = SRO.InsertDefaulterName(ID: txtdefaulterNo.Text, BRCD: Session["brcd"].ToString(), SRO_NO: TxtSRNO.Text, NOTICE_ISS_DT: TxtNoticeIssDt.Text, STAGE: "1001", MID: Session["MID"].ToString(), CID: "", VID: "", SYSTEMDATE: Session["EntryDate"].ToString(), CASENO: txtCaseNo.Text, CASE_YEAR: txtCaseY.Text, MEMBERNO: txtMember.Text, DEFAULTERNAME: Convert.ToString(lstarea.Items[i]), DEFAULTPROPERTY: Convert.ToString(lstDefProperty.Items[i]), CORRESPONDEADD: Convert.ToString(lstCorrespondenceAdd.Items[i]), CASESTATUS: "", OCC_ADD: txtOccupationAdd.Text, OCC_DETAIL: ddlOccupation.SelectedValue.ToString(), MOBILE1: txtMob1.Text, MOBILE2: txtmob2.Text);
                  
                    }                                                                                                                  
                }
                else if (lstarea.Items.Count == 0 || lstDefProperty.Items.Count==0 || lstCorrespondenceAdd.Items.Count==0)
                {
                
                        result = SRO.InsertDefaulterName(ID: txtdefaulterNo.Text, BRCD: Session["brcd"].ToString(), SRO_NO: TxtSRNO.Text, NOTICE_ISS_DT: TxtNoticeIssDt.Text, STAGE: "1001", MID: Session["MID"].ToString(), CID: "", VID: "", SYSTEMDATE: Session["EntryDate"].ToString(), CASENO: txtCaseNo.Text, CASE_YEAR: txtCaseY.Text, MEMBERNO: txtMember.Text, DEFAULTERNAME: Convert.ToString(txtDefaulterName.Text), DEFAULTPROPERTY: Convert.ToString(txtDefaultProperty.Text), CORRESPONDEADD: Convert.ToString(txtCorrespondence.Text), CASESTATUS: "", OCC_ADD: txtOccupationAdd.Text, OCC_DETAIL: ddlOccupation.SelectedValue.ToString(), MOBILE2: txtmob2.Text, MOBILE1: txtMob1.Text);
                   
                }
            }


        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return result;
    }
    public int ModifyDefaulterName()
    {

        try
        {
            {
                int index = lstarea.SelectedIndex;
                if (index >= 0)
                {
                    List<string> values = new List<string>();
                    if (lstarea.Items.Count != 0)
                    {
                        for (int i = 0; i < lstarea.Items.Count; i++)
                        {

                              result=SRO.ModifyDefulter( BRCD:TxtBRCD.Text,  CASENO:txtCaseNo.Text,  CASEYEAR:txtCaseY.Text, SRO_NO:TxtSRNO.Text, STAGE:"1002", MEMNO:txtMember.Text, CaseSts:"", VID:Session["MID"].ToString(), DEFNAME:txtDefaulterName.Text, NOTICE_ISS_DT:"",ID:txtdefaulterNo.Text, DEFAULTPROPERTY:txtDefaultProperty.Text,CORRESPONDENCEADDRESS:txtCorrespondence.Text,OCC_DETAIL:ddlOccupation.SelectedValue,OCC_ADD:txtOccupationAdd.Text,MOBILE1:txtdMob1.Text,MOBILE2:txtdMob2.Text);
         
                        }
                    }
                    else if (lstarea.Items.Count == 0)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            result = SRO.ModifyDefulter(BRCD: TxtBRCD.Text, CASENO: txtCaseNo.Text, CASEYEAR: txtCaseY.Text, SRO_NO: TxtSRNO.Text, STAGE: "1002", MEMNO: txtMember.Text, CaseSts: "", VID: Session["MID"].ToString(), DEFNAME: txtDefaulterName.Text, NOTICE_ISS_DT: "", ID: txtdefaulterNo.Text, DEFAULTPROPERTY: txtDefaultProperty.Text, CORRESPONDENCEADDRESS: txtCorrespondence.Text, OCC_DETAIL: ddlOccupation.SelectedValue, OCC_ADD: txtOccupationAdd.Text, MOBILE1: txtdMob1.Text, MOBILE2: txtdMob2.Text);
            }
                    }
                }
            }

        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return result;
    }
    public int DeleteDefaulterName()
    {

        try
        {
            {
                List<string> values = new List<string>();
                if (lstarea.Items.Count != 0)
                {
                    for (int i = 0; i < lstarea.Items.Count; i++)
                    {

                        //result = SRO.DeleteDefaulter(BRCD :TxtBRCD.Text, CASENO: txtCaseNo.Text, CASEYEAR: txtCaseY.Text);

                    }
                }
                else if (lstarea.Items.Count == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                      //  result = SRO.DeleteDefaulter(BRCD:TxtBRCD.Text, CASENO: txtCaseNo.Text, CASEYEAR: txtCaseY.Text);
                    }
                }
            }


        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return result;
    }
   
    protected void txtCostProcss_TextChanged(object sender, EventArgs e)
    {
        float AwardAmt = Convert.ToSingle(txtAwardAmt.Text);
        float Interest = Convert.ToSingle(txtInterest.Text);
        float Cost = Convert.ToSingle(txtCost.Text);
        float CostApp = Convert.ToSingle(txtCostProcss.Text); 
       

        TxtTotalRec.Text = Math.Round(Convert.ToDouble((AwardAmt + Interest + Cost + CostApp))).ToString();

    }
    protected void ddlRCNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRCNo.SelectedValue.ToString() == "1")
        {
            lbl101Rc.Text = "101 RC NO";
            lbl101rcd.Text = "101 RC Order Date";

        }
        else if (ddlRCNo.SelectedValue.ToString() == "2")
        {
            lbl101Rc.Text = "154(29)B RC NO";
            lbl101rcd.Text = "154(29)B RC Order Date";

        }
        else if (ddlRCNo.SelectedValue.ToString() == "3")
        {
            lbl101Rc.Text = "98 RC NO";
            lbl101rcd.Text = "98  RC Order Date";
        }
        TxtCaseFlNo.Focus();
    }
  
    protected void BtnModify_Click(object sender, EventArgs e)
    {
          // result = SRO.ModifyMember(BRCD: TxtBRCD.Text, MEMNO: txtMember.Text,ADDRESS:txtmemADD.Text);

           //if (result > 0)
           //{
           //    // result = ModifyDefaulterName();
           //    WebMsgBox.Show("Data Modified successfully..!!", this.Page);
           //}

        Response.Redirect("FrmSocietyMaster.aspx?FLAG=MD&CustNo=" + txtMember.Text + "");
    }

    protected void txtCaseNo_TextChanged(object sender, EventArgs e)
    {
         stage =SRO.CHKGETCASENO(BRCD: TxtBRCD.Text, CASENO: txtCaseNo.Text, CASE_YEAR: txtCaseY.Text);
         if (stage != null)
         {
             ViewDetails(BRCD: TxtBRCD.Text, CASENO: txtCaseNo.Text, CASE_YEAR: txtCaseY.Text);
             txtdefaulterNo.Text = SRO.DefaulterNo(CASENO: txtCaseNo.Text, CASE_YEAR: txtCaseY.Text);
              
             BindGrdCommitee();
             BindGrdDefaulter();
         }
         else
         {
             ddlMemType.Focus();
         }
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
    //protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlPaymentMode.SelectedValue=="1")
    //    {
    //        Div15.Visible = false;
    //    }
    //    else if (ddlPaymentMode.SelectedValue == "4")
    //    {
    //        Div15.Visible = true;
    //    }
    //}
    protected void txtDefaultValue_TextChanged(object sender, EventArgs e)
    {

        //float PriAmt = Convert.ToSingle(txtDefaultValue.Text);
        
        //string EMI = "";
        //EMI = Math.Round(Convert.ToDouble((PriAmt * Convert.ToDouble(2.5)) / 100)).ToString();

        //txtExecutionC.Text = Math.Round(Convert.ToDouble((Convert.ToDouble(EMI)))).ToString();
       // txtDefaultValue.Focus();
    }
    protected void BtnRecipt_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("frmAVS51203.aspx?BRCD=" + TxtBRCD.Text + "&CASE_YEAR=" + txtCaseY.Text + "&CASENO=" + txtCaseNo.Text + "", true);
    }



    protected void ddlMemType_SelectedIndexChanged(object sender, EventArgs e)
    {
       
       
        txtMember.Focus();
    }
    protected void ddlorder_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtWard.Focus();
    }
    protected void txtWard_SelectedIndexChanged(object sender, EventArgs e)
    {
        TxtDivCityName.Focus();
    }
    protected void txtCost_TextChanged(object sender, EventArgs e)
    {
        txtCostProcss.Focus();
    }
    protected void DDLType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTypeNo.Focus();
    }
    protected void ddlOccupation_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtOccupationAdd.Focus();
    }
    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtNamed.Focus();
    }
    protected void btnAddComm_Click(object sender, EventArgs e)
    {

        
            if (Convert.ToString(ViewState["FLAG"]) == "AD")
            {


                result = SRO.InsertCommitDetail(BRCD: TxtBRCD.Text, CASENO: txtCaseNo.Text, CASEYAER: txtCaseY.Text, MEMNO: txtMember.Text, SRO_NO: TxtSRNO.Text, NOTICE_ISS_DT: TxtNoticeIssDt.Text, CaseSts: "1", MID: Session["MID"].ToString(), DESIGNATION: ddlDesignation.SelectedValue.ToString(), COMM_NAME: txtNamed.Text, COM_MOBILE1: txtdMob1.Text, COM_MOBILE2: "9999999999", COM_ADDRESS: txtDeflterAddress.Text, PROPERTYTYPE: ddltype1.SelectedValue, PROPERTYTYPENO: txtPropertytype1.Text, FloorNO: txtFloor1.Text);

                if (result > 0)
                {
                    // result = InsertDefaulterName();

                    WebMsgBox.Show("Data Saved successfully", this.Page);

                    div_All.Visible = true;
                    Div_Submit.Visible = true;
                    div_Grid.Visible = true;

                    div_Sro.Visible = true;
                    BindGrdCommitee();
                    FL = "Insert";//Dhanya Shetty
                    string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_Demand_Add _" + txtCaseNo.Text + "_" + txtMember.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    ///clear();
                    clearCommitee();
                }

                else
                {
                    WebMsgBox.Show("Commitee Details  Not  Save", this.Page);
                }
            }
        
       
           
    }
    protected void grdCommitee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdCommitee.PageIndex = e.NewPageIndex;
            BindGrdCommitee();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void lnkSelect1_Click(object sender, EventArgs e)
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
            TxtBRCDName.Text = AST.GetBranchName(TxtBRCD.Text);
            stage = SRO.GETSTAGE(TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
            if (stage != "1004")
            {
                usrgrp = SRO.ChkUser(TxtBRCD.Text, Session["UID"].ToString(), Session["UserName"].ToString());

                //if (stage == "1003")
                //{
                //    if (usrgrp != "1")
                //    {
                //        WebMsgBox.Show("Data has Already authorized..!!", this.Page);
                //        clear();
                //        return;
                //    }
                //    else
                //    {
                //        if (ViewState["FLAG"].ToString() != "MD")
                //        {
                //            WebMsgBox.Show("Data has Already authorized..!!", this.Page);
                //            clear();
                //            return;
                //        }
                //    }
                //}

            }
            div_All.Visible = true;
            Div_Submit.Visible = true;
            div_Grid.Visible = false;
            div_Sro.Visible = true;
            BtnSubmit.Visible = true;
            btnmodifycomm.Visible = true;
            btnAddComm.Visible = false;
            ViewDetailsCommite(ViewState["BRCD"].ToString(), ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString(), ViewState["ID"].ToString());
            // BindGrdMain();
            BindGrdCommitee();
           
            lblActivity.Text = "Modify Demand";
            //ENTF(true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void grdDefulter_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdDefulter.PageIndex = e.NewPageIndex;
            BindGrdDefaulter();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void lnkDefaulter_Click(object sender, EventArgs e)
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

             DT1 = SRO.ViewDetailsDefaulter(ViewState["BRCD"].ToString(), ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString(), ViewState["id"].ToString());
                        if (DT1.Rows.Count > 0)
                        {

                           
                             txtdefaulterNo.Text =DT1.Rows[0]["ID"].ToString();
                           txtDefaulterName.Text=  DT1.Rows[0]["DEFAULTERNAME"].ToString();
                          
                            txtDefaultProperty.Text= DT1.Rows[0]["DEFAULTPROPERTY"].ToString();
                            txtCorrespondence.Text= DT1.Rows[0]["CORRESPONDENCEADDRESS"].ToString();
                            ddlOccupation.SelectedValue = DT1.Rows[0]["OCC_DETAIL"].ToString();
                            txtOccupationAdd.Text = DT1.Rows[0]["OCC_ADD"].ToString();
                            txtMob1.Text = DT1.Rows[0]["MOBILE1"].ToString();
                            txtmob2.Text = DT1.Rows[0]["MOBILE2"].ToString();
                            lstarea.Visible = false;
                           // lstDefProperty.Visible = true;
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

            DT1 = SRO.ViewDetailsDefaulter(ViewState["BRCD"].ToString(), ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString(), ViewState["id"].ToString());
            if (DT1.Rows.Count > 0)
            {


                txtdefaulterNo.Text = DT1.Rows[0]["ID"].ToString();
                txtDefaulterName.Text = DT1.Rows[0]["DEFAULTERNAME"].ToString();

                txtDefaultProperty.Text = DT1.Rows[0]["DEFAULTPROPERTY"].ToString();
                txtCorrespondence.Text = DT1.Rows[0]["CORRESPONDENCEADDRESS"].ToString();
                ddlOccupation.SelectedValue = DT1.Rows[0]["OCC_DETAIL"].ToString();
                txtOccupationAdd.Text = DT1.Rows[0]["OCC_ADD"].ToString();
                txtMob1.Text = DT1.Rows[0]["MOBILE1"].ToString();
                txtmob2.Text = DT1.Rows[0]["MOBILE2"].ToString();
                lstarea.Visible = false;
                // lstDefProperty.Visible = true;
                btnarea.Visible = false;
                BtnModifyd.Visible = false;
                btndel.Visible = true;
            }


            else
            {
                btnarea.Text = "Delete";
                ViewState["LFlag"] = "AD";
                ViewState["FLAG"] = "AD";

            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnModifyd_Click(object sender, EventArgs e)
    {
       // if (Convert.ToString(ViewState["FLAG"]) == "MD")
        {

            result = SRO.ModifyDefulter(BRCD: TxtBRCD.Text, CASENO: txtCaseNo.Text, CASEYEAR: txtCaseY.Text, SRO_NO: TxtSRNO.Text, STAGE: "1002", MEMNO: txtMember.Text, CaseSts: "", VID: Session["MID"].ToString(), DEFNAME: txtDefaulterName.Text, NOTICE_ISS_DT: "", ID: txtdefaulterNo.Text, DEFAULTPROPERTY: txtDefaultProperty.Text, CORRESPONDENCEADDRESS: txtCorrespondence.Text, OCC_DETAIL: ddlOccupation.SelectedValue, OCC_ADD: txtOccupationAdd.Text, MOBILE1: txtMob1.Text, MOBILE2: txtmob2.Text);
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
       // if (Convert.ToString(ViewState["FLAG"]) == "DEL")
        {

            result = SRO.DeleteDefaulter(BRCD: TxtBRCD.Text, CASENO: txtCaseNo.Text, CASEYEAR: txtCaseY.Text, ID: txtdefaulterNo.Text);
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
    protected void TxtCaseFlNo_TextChanged(object sender, EventArgs e)
    {
        TxtCaseFlDate.Focus();
    }
    protected void TxtCaseFlDate_TextChanged(object sender, EventArgs e)
    {
        TxtCaseFlNo91.Focus();
    }
    protected void ddltype1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtPropertytype1.Focus();
    }
    protected void btnmodifycomm_Click(object sender, EventArgs e)
    {
        //stage = SRO.GETCommite(TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
        //if (stage != "null")
        //{

           


        //        result = SRO.InsertCommitDetail(BRCD: TxtBRCD.Text, CASENO: txtCaseNo.Text, CASEYAER: txtCaseY.Text, MEMNO: txtMember.Text, SRO_NO: TxtSRNO.Text, NOTICE_ISS_DT: TxtNoticeIssDt.Text, CaseSts: "1", MID: Session["MID"].ToString(), DESIGNATION: ddlDesignation.SelectedValue.ToString(), COMM_NAME: txtNamed.Text, COM_MOBILE1: txtdMob1.Text, COM_MOBILE2: "9999999999", COM_ADDRESS: txtDeflterAddress.Text, PROPERTYTYPE: ddltype1.SelectedValue, PROPERTYTYPENO: txtPropertytype1.Text, FloorNO: txtFloor1.Text);

        //        if (result > 0)
        //        {
        //            // result = InsertDefaulterName();

        //            WebMsgBox.Show("Data Saved successfully", this.Page);

        //            div_All.Visible = true;
        //            Div_Submit.Visible = true;
        //            div_Grid.Visible = true;

        //            div_Sro.Visible = true;
        //            BindGrdCommitee();
        //            FL = "Insert";//Dhanya Shetty
        //            string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_Demand_Add _" + txtCaseNo.Text + "_" + txtMember.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        //            ///clear();
        //            clearCommitee();
        //        }

        //        else
        //        {
        //            WebMsgBox.Show("Commitee Details  Not  Save", this.Page);
        //        }
        //    }
        
        //else

            if (Convert.ToString(ViewState["FLAG"]) == "MD")
            {

                result = SRO.ModifyCommitDetail(BRCD: TxtBRCD.Text, CASENO: txtCaseNo.Text, CASEYAER: txtCaseY.Text, MEMNO: txtMember.Text, SRO_NO: TxtSRNO.Text, NOTICE_ISS_DT: TxtNoticeIssDt.Text, CaseSts: "1", MID: Session["MID"].ToString(), DESIGNATION: ddlDesignation.SelectedValue.ToString(), COMM_NAME: txtNamed.Text, COM_MOBILE1: txtdMob1.Text, COM_MOBILE2: "9999999999", COM_ADDRESS: txtDeflterAddress.Text, PROPERTYTYPE: ddltype1.SelectedValue, PROPERTYTYPENO: txtPropertytype1.Text, FloorNO: txtFloor1.Text);
                if (result > 0)
                {


                    WebMsgBox.Show("Committee  Modify successfully", this.Page);
                    clear1();
                    clearCommitee();

                }
                else
                {
                    WebMsgBox.Show("Committee Not  Modify", this.Page);
                }
            }
    }
    protected void btnDownlod_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        FL = "DOWNLOADECASERPT";
        dt = SRO.RptNOCASE(FL,txtCaseY.Text,txtCaseNo.Text);

            GridView gv2 = new GridView();
        gv2.DataSource = dt;
        gv2.DataBind();
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=NO_OF_CASE_RPT.xls");
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gv2.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.End();
    }
    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmDemandADD.aspx?FLAG=AD&CustNo=" + txtMember.Text+"");
    }


    protected void LNKCOMCANL_Click(object sender, EventArgs e)
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
            TxtBRCDName.Text = AST.GetBranchName(TxtBRCD.Text);
            stage = SRO.GETSTAGE(TxtBRCD.Text, txtCaseNo.Text, txtCaseY.Text);
            // if (stage != "1004")
            // {
            usrgrp = SRO.ChkUser(TxtBRCD.Text, Session["UID"].ToString(), Session["UserName"].ToString());

            //if (stage == "1003")
            //{
            //    if (usrgrp != "1")
            //    {
            //        WebMsgBox.Show("Data has Already authorized..!!", this.Page);
            //        clear();
            //        return;
            //    }
            //    else
            //    {
            //        if (ViewState["FLAG"].ToString() != "MD")
            //        {
            //            WebMsgBox.Show("Data has Already authorized..!!", this.Page);
            //            clear();
            //            return;
            //        }
            //    }
            //}

            //   }
            div_All.Visible = true;
            Div_Submit.Visible = true;
            div_Grid.Visible = false;
            div_Sro.Visible = true;
            BtnSubmit.Visible = true;
            btnmodifycomm.Visible = true;
            btnAddComm.Visible = false;
            SRO.deletecomminte(ViewState["BRCD"].ToString(), ViewState["CASENO"].ToString(), ViewState["CASE_YEAR"].ToString(), ViewState["ID"].ToString());
            // BindGrdMain();
            WebMsgBox.Show("Data has delete!!", this.Page);
            BindGrdCommitee();

           
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}