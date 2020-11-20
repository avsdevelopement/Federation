using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAvs51213 : System.Web.UI.Page
{
    ClsShareApp SA = new ClsShareApp();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAuthorized PVOUCHER = new ClsAuthorized();
    ClsCashReciept CurrentCls = new ClsCashReciept();
    ClsSharesAllotment AAS = new ClsSharesAllotment();
    ClsInsertTrans Trans = new ClsInsertTrans();
    ClsCustomerMast CM = new ClsCustomerMast();
    ClsAccopen accop = new ClsAccopen();
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    DataTable ShrData = new DataTable();
    CLSAVS51213 App = new CLSAVS51213();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    double TotalAmount = 0;
    string MeetDate, MemberNo, CertNo, FromNo, BoardRegNo, ToNo = "";
    string Result, WelFee, WelFeeLoan, FSet, EntFee = "";
    bool SetNumber = true;
    int resultint, AppNum, ShareSuspGl, BClassGl, ShareAccNo = 0,CUSTNO=0;

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            BD.BindWard(ddlWard);
            BD.BindMemberType(ddlSocietyType);
            BD.BindState(ddstate);
            BD.BindPayment(ddlPayType, "1");
            Transfer.Visible = false;
            divIntrument.Visible = false;
            divNarration.Visible = false;
            Submit.Visible = true;
          
            divShareAllot.Visible = false;
        }
    }
    protected void ddlSocietyType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        string PCode;
        try
        {
            div_cashrct.Visible = false;
            ViewState["Flag"] = "AD";
            Submit.Text = "Submit";
          
            lblActivity.Text = "Add App";
            //txtAppNo.Text = SA.GetAppNo(Session["BRCD"].ToString()).ToString();
            txtdate.Text = Session["EntryDate"].ToString();
          

            PCode = SA.SavProdCode(Session["BRCD"].ToString());
            ViewState["PrCode"] = PCode.ToString();
            SetDefaultAmount();
        

            divShareApp.Visible = true;
            btnReceipt.Visible = false;
            Submit.Visible = true;
          
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {

    }
    protected void lnkAuthorized_Click(object sender, EventArgs e)
    {

    }
    protected void lnkView_Click(object sender, EventArgs e)
    {

    }
    protected void lnkShrAllot_Click(object sender, EventArgs e)
    {

    }
    protected void lnkShrCancel_Click(object sender, EventArgs e)
    {

    }
    protected void ddtaluka_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnAddComm_Click(object sender, EventArgs e)
    {

    }
    protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPayType.SelectedValue.ToString() == "0")
        {
            Transfer.Visible = false;
            divIntrument.Visible = false;
            divNarration.Visible = false;
            ddlPayType.Focus();
        }
        else if (ddlPayType.SelectedValue.ToString() == "1")
        {
            Transfer.Visible = false;
            divIntrument.Visible = false;
            divNarration.Visible = true;
            txtNarration.Text = "By Cash";
           

          
            txtNarration.Focus();
        }
        else if (ddlPayType.SelectedValue.ToString() == "2")
        {
            Transfer.Visible = true;
            divIntrument.Visible = false;
            divNarration.Visible = true;
            txtNarration.Text = "By Transfer";
          
            txtProdType1.Focus();
        }
        else if (ddlPayType.SelectedValue.ToString() == "4")
        {
            Transfer.Visible = true;
            divIntrument.Visible = true;
            divNarration.Visible = true;
            txtNarration.Text = "By Cheque";
           // txtAmount.Text = txtTotAmount.Text.Trim().ToString();
            TxtChequeDate.Text = Session["EntryDate"].ToString();

          //  Clear();
            txtProdType1.Focus();
        }
        else
        {
            Transfer.Visible = false;
            divIntrument.Visible = false;
            divNarration.Visible = false;
            ddlPayType.Focus();
        }
    }
    protected void ddstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BD.BindDistrict(dddistrict, ddstate.SelectedValue);
            dddistrict.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void dddistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BD.BindArea(ddtaluka, dddistrict.SelectedValue);
            ddtaluka.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

   
    protected void txtProdType1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtProdName1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtAccNo1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtAccName1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Flag"].ToString() == "AD")
            {
                if (ddlSocietyType.SelectedValue.ToString() == "1")
                {
                    ShareSuspGl = SA.GetShareSuspGl(Session["BRCD"].ToString());
                    CUSTNO = Convert.ToInt32(CM.GetCustNo("0")); 

                    ShareAccNo = App.insert(BRCD: Session["BRCD"].ToString(), CUSTNO: CUSTNO.ToString(), CUSTNAME: txtMemName.Text, OPENINGDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString(), PCMAC: Session["MID"].ToString(), MEM_TYPE: ddlSocietyType.SelectedValue, MEMBERNO: txtRegistration.Text, TOTMEMBERN: txtmemnos.Text, FLAT_ROOMNO: ddlWard.SelectedValue, ADDRESS: txtAddress.Text, DISTRICT: dddistrict.SelectedValue, STATE: ddstate.SelectedValue, AREA_TALUKA: ddtaluka.SelectedValue, PINCODE: txtpin.Text, MOBILE1: txtMob.Text, EMAIL_ID: txtemailid.Text, COMM_NAME: txtNamed.Text, COM_MOBILE1: txtdMob1.Text, COMM_NAME1: txtsecname.Text, COM_MOBILE2: txtMobile.Text);

                    //Added By Amol On 16/01/2018 for Create accounts  AppNum
                     SA.CreateSharAccounts(Session["BRCD"].ToString(), CUSTNO.ToString(), ShareSuspGl.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                }
                else if (ddlSocietyType.SelectedValue.ToString() == "2")
                {
                    //ShareSuspGl = SA.GetShareSuspGl(Session["BRCD"].ToString());
                    //ShareAccNo = Convert.ToInt32(SA.GetShareAccNo(Session["BRCD"].ToString(), ShareSuspGl.ToString()));
                    //dt = SA.CheckExists(Session["BRCD"].ToString(), ShareSuspGl.ToString(), txtShareAccNo.Text.Trim().ToString() == "" ? "0" : txtShareAccNo.Text.Trim().ToString());

                    //if (dt.Rows.Count <= 0)
                    //{
                    //    // Insert Record Into Avs_Acc Table Under subglcode (e.g 44)
                    //    ShareAccNo = accop.insert(Session["BRCD"].ToString(), "4", ShareSuspGl.ToString(), txtcustno.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), "1", "1", "", "", "", "", "", "", "", "", "", "", "", "", "0", "0", "1003", Session["BRCD"].ToString(), "0", "0","");
                    //    SA.CreateSharAccounts(Session["BRCD"].ToString(), txtcustno.Text.ToString(), ShareSuspGl.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    //}AppNum

                    CUSTNO = Convert.ToInt32(CM.GetCustNoForNominal("0"));


                    // Insert Record Into Avs_Acc Table Under subglcode (e.g 44)
                    ShareAccNo = App.insert(BRCD: Session["BRCD"].ToString(), CUSTNO: CUSTNO.ToString(), CUSTNAME: txtMemName.Text, OPENINGDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString(), PCMAC: Session["MID"].ToString(), MEM_TYPE: ddlSocietyType.SelectedValue, MEMBERNO: txtRegistration.Text, TOTMEMBERN: txtmemnos.Text, FLAT_ROOMNO: ddlWard.SelectedValue, ADDRESS: txtAddress.Text, DISTRICT: dddistrict.SelectedValue, STATE: ddstate.SelectedValue, AREA_TALUKA: ddtaluka.SelectedValue, PINCODE: txtpin.Text, MOBILE1: txtMob.Text, EMAIL_ID: txtemailid.Text, COMM_NAME: txtNamed.Text, COM_MOBILE1: txtdMob1.Text, COMM_NAME1: txtsecname.Text, COM_MOBILE2: txtMobile.Text);

                    //Added By Amol On 16/01/2018 for Create accounts
                    //SA.CreateSharAccounts(Session["BRCD"].ToString(), CUSTNO.ToString(), ShareSuspGl.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            
                }
              

                if (ddlPayType.SelectedValue.ToString() == "1")
                {
                    if (ShareAccNo > 0)
                    {
                        //For Cash
                        Result = SA.InsertData(Session["BRCD"].ToString(), Convert.ToDouble(ShareAccNo).ToString(), ddlSocietyType.SelectedValue, txtRegistration.Text.Trim().ToString() == "" ? "0" : txtRegistration.Text.Trim().ToString(), CUSTNO.ToString(), "", Convert.ToInt32(txtAmount.Text).ToString(), Convert.ToInt32(txtEntFee.Text).ToString(), txtRegistration.Text.ToString() == "" ? "0" : txtRegistration.Text.Trim().ToString(), "0", "0", "0", "0", "0", "0", Convert.ToInt32(txtOther5.Text).ToString() == "" ? "0" : Convert.ToInt32(txtOther5.Text).ToString(), "", "", "", "", "", "", "", "", "", "", ddlPayType.SelectedValue, "", "", "", "", "", "", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    }
                }
                else if (ddlPayType.SelectedValue.ToString() == "2")
                {
                    if (ShareAccNo > 0)
                    {
                        //For Transferstring , string , string , string ,                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            string                                              , string , string , string , string , string                              , string , string , string , string , string , string , string , string , string ,                                                                                                                                                           string , string , string , string , string )

                        Result = SA.InsertData1(BrCode: Session["BRCD"].ToString(), ShareAccNo: Convert.ToDouble(CUSTNO).ToString(), ApplType: ddlSocietyType.SelectedValue, CustNo: txtRegistration.Text.Trim().ToString() == "" ? "0" : txtRegistration.Text.Trim().ToString(), MemberNo: CUSTNO.ToString(),  TotalShr: Convert.ToInt32(txtShare.Text).ToString(), EntryFee: Convert.ToInt32(txtEntFee.Text).ToString(), SavAccNo: txtRegistration.Text.ToString() == "" ? "0" : txtRegistration.Text.Trim().ToString(), Other5: Convert.ToInt32(txtOther5.Text).ToString(), PmtMode: ddlPayType.SelectedValue, GlCode: ViewState["GlCode"].ToString(), SubGlCode: txtProdType1.Text.ToString(), AcccNo: TxtAccNo1.Text.ToString(), ChkNo: "", ChkDate: "", Parti: txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), EDate: Session["EntryDate"].ToString(), Mid: Session["MID"].ToString());

                       // Result = SA.InsertData(Session["BRCD"].ToString(), ShareAccNo.ToString(), ddlAppType.SelectedValue, txtMemNo.Text.Trim().ToString() == "" ? "0" : txtMemNo.Text.Trim().ToString(), txtcustno.Text.Trim().ToString(), txtNoOfShr.Text.Trim().ToString(), txtShrValue.Text.Trim().ToString(), txtTotShr.Text.Trim().ToString(), txtEntFee.Text.Trim().ToString(), txtAccNo.Text.ToString() == "" ? "0" : txtAccNo.Text.Trim().ToString(), txtSavFee.Text.Trim().ToString() == "" ? "0" : txtSavFee.Text.Trim().ToString(), txtOther1.Text.Trim().ToString() == "" ? "0" : txtOther1.Text.Trim().ToString(), txtOther2.Text.Trim().ToString() == "" ? "0" : txtOther2.Text.Trim().ToString(), txtOther3.Text.Trim().ToString() == "" ? "0" : txtOther3.Text.Trim().ToString(), txtOther4.Text.Trim().ToString() == "" ? "0" : txtOther4.Text.Trim().ToString(), txtOther5.Text.Trim().ToString() == "" ? "0" : txtOther5.Text.Trim().ToString(), txtMemWelFee.Text.Trim().ToString() == "" ? "0" : txtMemWelFee.Text.Trim().ToString(), txtSerChrFee.Text.Trim().ToString() == "" ? "0" : txtSerChrFee.Text.Trim().ToString(), txtNomName1.Text == "" ? "" : txtNomName1.Text, TxtDOB1.Text == "" ? "" : TxtDOB1.Text, TxtAge1.Text == "" ? "" : TxtAge1.Text, ddlRelation1.SelectedValue, txtNomName2.Text == "" ? "" : txtNomName2.Text, TxtDOB2.Text == "" ? "" : TxtDOB2.Text, TxtAge2.Text == "" ? "" : TxtAge2.Text, ddlRelation2.SelectedValue, ddlPayType.SelectedValue, txtRemark.Text.ToString(), ViewState["GlCode"].ToString(), txtProdType1.Text.ToString(), TxtAccNo1.Text.ToString(), "", "", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    }
                }
                else if (ddlPayType.SelectedValue.ToString() == "4")
                {
                    if (ShareAccNo > 0)
                    {
                        //For Cheque
                        Result = SA.InsertData(Session["BRCD"].ToString(), ShareAccNo.ToString(), ddlSocietyType.SelectedValue, txtRegistration.Text.Trim().ToString() == "" ? "0" : txtRegistration.Text.Trim().ToString(), CUSTNO.ToString(), "", txtAmount.Text.Trim().ToString(), txtEntFee.Text.Trim().ToString(), txtRegistration.Text.ToString() == "" ? "0" : txtRegistration.Text.Trim().ToString(), "", "", "", "", "", txtOther5.Text.Trim().ToString() == "" ? "0" : txtOther5.Text.Trim().ToString(), "", "", "", "", "", "", "", "", "", "", "", ddlPayType.SelectedValue, "", txtProdType1.Text.ToString(), txtProdType1.Text.ToString(), "0", TxtChequeNo.Text.Trim().ToString(), TxtChequeDate.Text.Trim().ToString(), txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                       // Result = SA.InsertData(Session["BRCD"].ToString(), ShareAccNo.ToString(), ddlAppType.SelectedValue, txtMemNo.Text.Trim().ToString() == "" ? "0" : txtMemNo.Text.Trim().ToString(), txtcustno.Text.Trim().ToString(), txtNoOfShr.Text.Trim().ToString(), txtShrValue.Text.Trim().ToString(), txtTotShr.Text.Trim().ToString(), txtEntFee.Text.Trim().ToString(), txtAccNo.Text.ToString() == "" ? "0" : txtAccNo.Text.Trim().ToString(), txtSavFee.Text.Trim().ToString() == "" ? "0" : txtSavFee.Text.Trim().ToString(), txtOther1.Text.Trim().ToString() == "" ? "0" : txtOther1.Text.Trim().ToString(), txtOther2.Text.Trim().ToString() == "" ? "0" : txtOther2.Text.Trim().ToString(), txtOther3.Text.Trim().ToString() == "" ? "0" : txtOther3.Text.Trim().ToString(), txtOther4.Text.Trim().ToString() == "" ? "0" : txtOther4.Text.Trim().ToString(), txtOther5.Text.Trim().ToString() == "" ? "0" : txtOther5.Text.Trim().ToString(), txtMemWelFee.Text.Trim().ToString() == "" ? "0" : txtMemWelFee.Text.Trim().ToString(), txtSerChrFee.Text.Trim().ToString() == "" ? "0" : txtSerChrFee.Text.Trim().ToString(), txtNomName1.Text == "" ? "" : txtNomName1.Text, TxtDOB1.Text == "" ? "" : TxtDOB1.Text, TxtAge1.Text == "" ? "" : TxtAge1.Text, ddlRelation1.SelectedValue, txtNomName2.Text == "" ? "" : txtNomName2.Text, TxtDOB2.Text == "" ? "" : TxtDOB2.Text, TxtAge2.Text == "" ? "" : TxtAge2.Text, ddlRelation2.SelectedValue, ddlPayType.SelectedValue, txtRemark.Text.ToString(), txtProdType1.Text.ToString(), txtProdType1.Text.ToString(), "0", TxtChequeNo.Text.Trim().ToString(), TxtChequeDate.Text.Trim().ToString(), txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    }
                }

                if (Result != "")
                {
                    string[] srno = Result.Split('_');
                    AppNum = Convert.ToInt32(srno[0].ToString());
                    resultint = Convert.ToInt32(srno[1].ToString());
                }
                if (resultint > 0)
                {
                  //  autoglname.ContextKey = Session["BRCD"].ToString();
                    //txtAppNo.Text = SA.GetAppNo(Session["BRCD"].ToString()).ToString();
                    if (ddlSocietyType.SelectedValue.ToString() == "1" || ddlSocietyType.SelectedValue.ToString() == "2")
                    {
                        lblMessage.Text = "Application Created Successfully With Member  No : " + CUSTNO.ToString() + " ";
                        ModalPopup.Show(this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Share_create _" + ShareAccNo.ToString() + "_" + AppNum.ToString() + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                     //   ClearData();
                        return;
                    }
                   

                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnAllotment_Click(object sender, EventArgs e)
    {

    }
    protected void btnReceipt_Click(object sender, EventArgs e)
    {

    }
    protected void Exit_Click(object sender, EventArgs e)
    {

    }
    protected void LnkPrintReceipt_Click(object sender, EventArgs e)
    {

    }
    protected void lnkDens_Click(object sender, EventArgs e)
    {

    }
   
    protected void ddtaluka_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public void SetDefaultAmount()
    {
        try
        {
            dt = new DataTable();
            dt = SA.GetShrInfo(Session["BRCD"].ToString());
            if (dt.Rows.Count > 0)
            {
                ViewState["PrCode"] = dt.Rows[0]["SAVING_GL"].ToString();
              //  txtNoOfShr.Text = Convert.ToInt32(dt.Rows[0]["NO_OF_SHARES"].ToString()).ToString();
                txtShare.Text = Convert.ToDouble(dt.Rows[0]["SHR_VALUE"].ToString()).ToString();

               // LblName0.InnerText = SA.GetGlName(Session["BRCD"].ToString(), ViewState["PrCode"].ToString());
               // LblName15.InnerText = SA.GetGlName(Session["BRCD"].ToString(), ViewState["PrCode"].ToString());
                txtEntFee.Text = Convert.ToDouble(dt.Rows[0]["EnterenceAmt"].ToString() == "" ? "0" : dt.Rows[0]["EnterenceAmt"].ToString()).ToString();
           //     txtOther1.Text = Convert.ToDouble(dt.Rows[0]["WelFareAmt"].ToString() == "" ? "0" : dt.Rows[0]["WelFareAmt"].ToString()).ToString();
             //   txtMemWelFee.Text = Convert.ToDouble(dt.Rows[0]["WelFareLoanAmt"].ToString() == "" ? "0" : dt.Rows[0]["WelFareLoanAmt"].ToString()).ToString();

                
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}