using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmShareApp : System.Web.UI.Page
{
    ClsShareApp SA = new ClsShareApp();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAuthorized PVOUCHER = new ClsAuthorized();
    ClsCashReciept CurrentCls = new ClsCashReciept();
    ClsSharesAllotment AAS = new ClsSharesAllotment();
    ClsInsertTrans Trans = new ClsInsertTrans();
    ClsAccopen accop = new ClsAccopen();
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    DataTable ShrData = new DataTable();
    CLSAVS51213 App = new CLSAVS51213();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsCustomerMast CM = new ClsCustomerMast();
    string FL = "";
    double TotalAmount = 0;
    string MeetDate, MemberNo, CertNo, FromNo, BoardRegNo, ToNo = "";
    string Result, WelFee, WelFeeLoan, FSet, EntFee = "";
    bool SetNumber = true;
    int resultint, AppNum, ShareSuspGl, BClassGl, ShareAccNo = 0, CUSTNO = 0;

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            BD.BindWard(ddlWard);
            BD.BindMemberType(ddlAppType);
            BD.BindState(ddstate);
          
            BD.BindRelation(ddlRelation1);
            BD.BindRelation(ddlRelation2);
            BD.BindPayment(ddlPayType, "1");
            Transfer.Visible = false;
            divIntrument.Visible = false;
            divNarration.Visible = false;
            Submit.Visible = true;
            btnAllotment.Visible = false;
            divShareAllot.Visible = false;
            
            dt = SA.GetLabelName(Session["BRCD"].ToString());
            if (dt.Rows.Count > 0)
            {
                LblName1.InnerText = dt.Rows[0]["LblEnterence"].ToString();
              //  LblName2.InnerText = dt.Rows[0]["LblOther1"].ToString();
               // LblName3.InnerText = dt.Rows[0]["LblOther2"].ToString();
             //   LblName4.InnerText = dt.Rows[0]["LblMemberWelLoan"].ToString();
              //  LblName5.InnerText = dt.Rows[0]["LblService"].ToString();
             //   LblName6.InnerText = dt.Rows[0]["LblOther3"].ToString();
            //    LblName7.InnerText = dt.Rows[0]["LblOther4"].ToString();
                LblName8.InnerText = dt.Rows[0]["LblOther5"].ToString();
               
            }
            txtcustno.Focus();
        }
      //  autoglname.ContextKey = Session["BRCD"].ToString();
    }

    #endregion

    #region Text Changed Event

    protected void ApplType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAppType.SelectedValue == "1")
            {
                
                ShareSuspGl = SA.GetShareSuspGl(Session["BRCD"].ToString());
                //txtShareAccNo.Text = SA.GetShareAccNo(Session["BRCD"].ToString(), ShareSuspGl.ToString()).ToString();
              txtAppNo.Text = SA.GetAppNo(Session["BRCD"].ToString(), "A").ToString();
                
                txtcustno.Focus();
            }
            else if (ddlAppType.SelectedValue == "2")
            {
               
                ShareSuspGl = SA.GetShareSuspGl(Session["BRCD"].ToString());
              //  txtShareAccNo.Text = SA.GetShareAccNo(Session["BRCD"].ToString(), ShareSuspGl.ToString()).ToString();
               txtAppNo.Text = SA.GetAppNo(Session["BRCD"].ToString(), "A").ToString();
                txtMemNo.Focus();
            }
            else if (ddlAppType.SelectedValue == "3")
            {
               
                ShareSuspGl = SA.GetBClassGl("1");
                txtShareAccNo.Text = SA.GetShareAccNo("1", ShareSuspGl.ToString()).ToString();
               txtAppNo.Text = SA.GetAppNo(Session["BRCD"].ToString(), "B").ToString();
                txtcustno.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Membership_TextChanged(object sender, EventArgs e)
    {
        try
        {
            dt = SA.CheckExists("1", "4", txtMemNo.Text.Trim().ToString() == "" ? "" : txtMemNo.Text.Trim().ToString());

            if (dt.Rows.Count > 0)
            {
                txtMemNo.Text = dt.Rows[0]["AccNo"].ToString();
                txtcustno.Text = dt.Rows[0]["CustNo"].ToString();
                txtcusname.Text = dt.Rows[0]["CustName"].ToString();
                txtcustno.Focus();
            }
            else
            {
                //txtMemNo.Text = "";
                txtMemNo.Focus();

               // lblMessage.Text = "Member not exists...Enter correct member number...!!";
              //  ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtShareAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ShareSuspGl = SA.GetShareSuspGl(Session["BRCD"].ToString());
            dt = SA.CheckExists(Session["BRCD"].ToString(), ShareSuspGl.ToString(), txtShareAccNo.Text.Trim().ToString() == "" ? "0" : txtShareAccNo.Text.Trim().ToString());

            if (dt.Rows.Count > 0)
            {
                txtShareAccNo.Text = dt.Rows[0]["AccNo"].ToString();
                txtcustno.Text = dt.Rows[0]["CustNo"].ToString();
                txtcusname.Text = dt.Rows[0]["CustName"].ToString();
                txtNoOfShr.Focus();
            }
            else
            {
                txtcustno.Text = "";
                txtcusname.Text = "";
                txtShareAccNo.Text = "";
                txtShareAccNo.Text = SA.GetShareAccNo(Session["BRCD"].ToString(), ShareSuspGl.ToString()).ToString();
                txtcustno.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtcustno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (lblActivity.Text != "")
            {
                string sql, AT;
                sql = AT = "";
                AT = BD.GetStage(txtcustno.Text, Session["BRCD"].ToString(), "");
                if (AT != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise...!!";
                    ModalPopup.Show(this.Page);
                    txtcustno.Text = "";
                    txtcusname.Text = "";
                    txtcustno.Focus();
                }
                else
                {
                    if (txtcustno.Text == "")
                    {
                        return;
                    }

                    string custname = SA.Getcustname(txtcustno.Text, Session["BRCD"].ToString());
                    txtAccNo.Text = SA.GetSavingGL(txtcustno.Text, Session["BRCD"].ToString(), ViewState["PrCode"].ToString());
                    DataTable DT = new DataTable();
                    DT = SA.GetAccName(ViewState["PrCode"].ToString(), txtAccNo.Text, Session["BRCD"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                        txtAccName.Text = CustName[0].ToString();
                    }
                    string[] cust = custname.Split('_');
                    txtcusname.Text = cust[0].ToString();
                    string RC = txtcusname.Text;
                    if (RC == "")
                    {
                        WebMsgBox.Show("Customer not found", this.Page);
                        txtcustno.Text = "";
                        txtcustno.Focus();
                        return;
                    }
                    txtNoOfShr.Focus();
                }
            }
            else
            {
                lnkAdd.Focus();
                lblMessage.Text = "Select Activity First...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtcusname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT;
            AT = "";
            string CUNAME = txtcusname.Text;
            string[] custnob = CUNAME.Split('_');

            if (lblActivity.Text != "")
            {
                if (custnob.Length > 1)
                {
                    AT = BD.GetStage((string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString()), Session["BRCD"].ToString(), "");

                    if (AT != "1003")
                    {
                        lblMessage.Text = "Sorry Customer not Authorise...!!";
                        ModalPopup.Show(this.Page);
                        txtcustno.Text = "";
                        txtcusname.Text = "";
                        txtcustno.Focus();
                    }
                    else
                    {
                        txtcusname.Text = custnob[0].ToString();
                        txtcustno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                        txtAccNo.Text = SA.GetSavingGL(txtcustno.Text, Session["BRCD"].ToString(), ViewState["PrCode"].ToString());
                        DataTable DT = new DataTable();
                        DT = SA.GetAccName(ViewState["PrCode"].ToString(), txtAccNo.Text, Session["BRCD"].ToString());
                        if (DT.Rows.Count > 0)
                        {
                            string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                            txtAccName.Text = CustName[0].ToString();
                        }
                    }
                    txtNoOfShr.Focus();
                }
            }
            else
            {
                lnkAdd.Focus();
                lblMessage.Text = "Select Activity First...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";

            AT = BD.GetSHstage(txtAccNo.Text, Session["BRCD"].ToString(), ViewState["PrCode"].ToString());
            if (AT != null)
            {
                if (AT != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise...!!";
                    ModalPopup.Show(this.Page);
                    txtAccNo.Text = "";
                    txtAccName.Text = "";
                    txtAccNo.Focus();
                    return;
                }
                else
                {
                    DataTable DT = new DataTable();
                    DT = SA.GetAccName(ViewState["PrCode"].ToString(), txtAccNo.Text, Session["BRCD"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                        txtAccName.Text = CustName[0].ToString();
                        txtSavFee.Focus();
                    }
                }
            }
            else
            {
                lblMessage.Text = "Enter valid account number...!!";
                ModalPopup.Show(this.Page);
                txtAccNo.Text = "";
                txtAccName.Text = "";
                txtAccNo.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtAccName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtAccName.Text = custnob[0].ToString();
                txtAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                txtSavFee.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtNoOfShr_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtTotShr.Text = (Convert.ToDouble(txtNoOfShr.Text == "" ? "0.00" : txtNoOfShr.Text) * Convert.ToDouble(txtShrValue.Text == "" ? "0.00" : txtShrValue.Text)).ToString();

            txtShrValue.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtShrValue_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtTotShr.Text = (Convert.ToDouble(txtNoOfShr.Text == "" ? "0.00" : txtNoOfShr.Text) * Convert.ToDouble(txtShrValue.Text == "" ? "0.00" : txtShrValue.Text)).ToString();
            txtEntFee.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtDOB1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] AG = TxtDOB1.Text.Split('/');
            int CY, BY;
            CY = DateTime.Now.Date.Year;
            BY = Convert.ToInt32(AG[2].ToString());
            TxtAge1.Text = (CY - BY).ToString();
            ddlRelation1.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
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

    protected void ddtaluka_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void TxtDOB2_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] AG = TxtDOB2.Text.Split('/');
            int CY, BY;
            CY = DateTime.Now.Date.Year;
            BY = Convert.ToInt32(AG[2].ToString());
            TxtAge2.Text = (CY - BY).ToString();
            ddlRelation2.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPayType.SelectedValue.ToString() == "0")
        {
            Transfer.Visible = false;
            divIntrument.Visible = false;
            divNarration.Visible = false;
            ddlPayType.Focus();
            txtAmount.Text = Convert.ToInt32(Convert.ToDouble(txtEntFee.Text.Trim().ToString()) + Convert.ToDouble(txtOther5.Text.Trim().ToString()) + Convert.ToDouble(txtShrValue.Text.Trim().ToString())).ToString();
        }
        else if (ddlPayType.SelectedValue.ToString() == "1")
        {
            Transfer.Visible = false;
            divIntrument.Visible = false;
            divNarration.Visible = true;
            txtNarration.Text = "By Cash";
            txtAmount.Text = Convert.ToInt32(Convert.ToDouble(txtEntFee.Text.Trim().ToString()) + Convert.ToDouble(txtOther5.Text.Trim().ToString()) + Convert.ToDouble(txtShrValue.Text.Trim().ToString())).ToString();
   
            Clear();
            txtNarration.Focus();
        }
        else if (ddlPayType.SelectedValue.ToString() == "2")
        {
            Transfer.Visible = true;
            divIntrument.Visible = false;
            divNarration.Visible = true;
            txtNarration.Text = "By Transfer";
            txtAmount.Text = Convert.ToInt32(Convert.ToDouble(txtEntFee.Text.Trim().ToString()) + Convert.ToDouble(txtOther5.Text.Trim().ToString()) + Convert.ToDouble(txtShrValue.Text.Trim().ToString())).ToString();
            Clear();
            txtProdType1.Focus();
        }
        else if (ddlPayType.SelectedValue.ToString() == "4")
        {
            Transfer.Visible = true;
            divIntrument.Visible = true;
            divNarration.Visible = true;
            txtNarration.Text = "By Cheque";
            txtAmount.Text = Convert.ToInt32(Convert.ToDouble(txtEntFee.Text.Trim().ToString()) + Convert.ToDouble(txtOther5.Text.Trim().ToString()) + Convert.ToDouble(txtShrValue.Text.Trim().ToString())).ToString();
            TxtChequeDate.Text = Session["EntryDate"].ToString();

            Clear();
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

    protected void txtProdType1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;
            AC1 = SA.Getaccno(txtProdType1.Text, Session["BRCD"].ToString(), "");

            if (AC1 != null)
            {
                string[] AC = AC1.Split('-'); ;
                ViewState["GlCode"] = AC[1].ToString();
                txtProdName1.Text = AC[2].ToString();
                AutoAccname1.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType1.Text + "_" + ViewState["GlCode"].ToString();

                if (Convert.ToInt32(txtProdType1.Text) >= 100)
                {
                    string[] TD = Session["EntryDate"].ToString().Split('/');
                    txtBalance.Text = SA.GetOpenClose(TD[2].ToString(), TD[1].ToString(), txtProdType1.Text, TxtAccNo1.Text.ToString() == "" ? "0" : TxtAccNo1.Text.ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString()).ToString();
                    TxtAccNo1.Text = txtProdType1.Text.ToString();
                    TxtAccName1.Text = txtProdName1.Text.ToString();

                    TxtChequeNo.Focus();
                }
                else
                {
                    TxtAccNo1.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter valid Product code!.....", this.Page);
                txtProdType1.Text = "";
                txtProdName1.Text = "";
                txtProdType1.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtProdName1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtProdName1.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtProdName1.Text = custnob[0].ToString();
                txtProdType1.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] AC = SA.Getaccno(txtProdType1.Text, Session["BRCD"].ToString(), custnob[2].ToString()).Split('-');
                ViewState["GlCode"] = AC[1].ToString();
                AutoAccname1.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType1.Text;

                if (Convert.ToInt32(txtProdType1.Text) > 100)
                {
                    string[] TD = Session["EntryDate"].ToString().Split('/');
                    txtBalance.Text = SA.GetOpenClose(TD[2].ToString(), TD[1].ToString(), txtProdType1.Text, TxtAccNo1.Text.ToString() == "" ? "0" : TxtAccNo1.Text.ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString()).ToString();

                    TxtChequeNo.Focus();
                }
                else
                {
                    TxtAccNo1.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAccNo1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";
            AT = BD.GetSHstage(TxtAccNo1.Text, Session["BRCD"].ToString(), txtProdType1.Text);
            if (AT != null)
            {
                if (AT != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise.........!!";
                    ModalPopup.Show(this.Page);
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";
                    TxtAccNo1.Focus();
                }
                else
                {
                    DataTable DT = new DataTable();
                    DT = SA.GetCustName(txtProdType1.Text, TxtAccNo1.Text, Session["BRCD"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                        TxtAccName1.Text = CustName[0].ToString();

                        string[] TD = Session["EntryDate"].ToString().Split('/');
                        txtBalance.Text = SA.GetOpenClose(TD[2].ToString(), TD[1].ToString(), txtProdType1.Text, TxtAccNo1.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString()).ToString();

                        TxtChequeNo.Focus();
                    }
                }
            }
            else
            {
                lblMessage.Text = "Enter valid account number...!!";
                ModalPopup.Show(this.Page);
                TxtAccNo1.Text = "";
                TxtAccNo1.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtAccName1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtAccName1.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtAccName1.Text = custnob[0].ToString();
                TxtAccNo1.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                string[] TD = Session["EntryDate"].ToString().Split('/');
                txtBalance.Text = SA.GetOpenClose(TD[2].ToString(), TD[1].ToString(), txtProdType1.Text, TxtAccNo1.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString()).ToString();

                TxtChequeNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Link Button Click

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        string PCode;
        try
        {
            div_cashrct.Visible = false;
            ViewState["Flag"] = "AD";
            Submit.Text = "Submit";
            ClearData();
            ENDN(true);
            lblActivity.Text = "Add App";
            //txtAppNo.Text = SA.GetAppNo(Session["BRCD"].ToString()).ToString();
            TxtDOB1.Text = Session["EntryDate"].ToString();
            TxtDOB2.Text = Session["EntryDate"].ToString();
            MaxDisbDate.Value = Session["EntryDate"].ToString();
            grdMaster.Visible = false;

            PCode = SA.SavProdCode(Session["BRCD"].ToString());
            ViewState["PrCode"] = PCode.ToString();
            SetDefaultAmount();
            AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + PCode.ToString() + "_";

            divShareApp.Visible = true;
            btnReceipt.Visible = false;
            Submit.Visible = true;
            btnAllotment.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string PCode;
            PCode = SA.SavProdCode(Session["BRCD"].ToString());
            ViewState["PrCode"] = PCode.ToString();
             LblName15.InnerText = SA.GetGlName(Session["BRCD"].ToString(), ViewState["PrCode"].ToString());
            ViewState["Flag"] = "DL";
            ENDN(false);
            ClearData();
            Submit.Text = "Delete";
            lblActivity.Text = "Delete App";
            BindGrid();
            grdMaster.Visible = true;
            div_cashrct.Visible = false;
            btnReceipt.Visible = false;
            divShareApp.Visible = true;
            Submit.Visible = true;
            btnAllotment.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkAuthorized_Click(object sender, EventArgs e)
    {
        try
        {
            string PCode;
            PCode = SA.SavProdCode(Session["BRCD"].ToString());
            ViewState["PrCode"] = PCode.ToString();
            LblName15.InnerText = SA.GetGlName(Session["BRCD"].ToString(), ViewState["PrCode"].ToString());
            ViewState["Flag"] = "AT";
            ClearData();
            Submit.Text = "Authorize";
            ENDN(false);
            lblActivity.Text = "Authorize App";
            BindGrid();
            grdMaster.Visible = true;

            btnReceipt.Visible = false;
            div_cashrct.Visible = true;
            divShareApp.Visible = true;
            Submit.Visible = true;
            btnAllotment.Visible = false;
            divShareAllot.Visible = false;
            BindGrid1();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            string PCode;
            PCode = SA.SavProdCode(Session["BRCD"].ToString());
            ViewState["PrCode"] = PCode.ToString();
            LblName15.InnerText = SA.GetGlName(Session["BRCD"].ToString(), ViewState["PrCode"].ToString());
            ViewState["Flag"] = "VW";
            ENDN(false);
            ClearData();
            Submit.Visible = false;
            lblActivity.Text = "View App";
            BindGridForView();
            grdMaster.Visible = true;
            div_cashrct.Visible = false;
            btnReceipt.Visible = false;
            divShareApp.Visible = true;
            Submit.Visible = true;
            btnAllotment.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkShrAllot_Click(object sender, EventArgs e)
    {
        try
        {
            Submit.Visible = false;
            btnAllotment.Visible = true;
            divShareApp.Visible = false;
            divShareAllot.Visible = true;
            BindUnallotedGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkShrCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Flag"] = "CL";
            ClearData();
            Submit.Text = "Cancel Application";
            ENDN(false);
            lblActivity.Text = "Cancel Application";
            BindAuthoApp();
            grdMaster.Visible = true;

            btnReceipt.Visible = false;
            div_cashrct.Visible = true;
            divShareApp.Visible = true;
            Submit.Visible = true;
            btnAllotment.Visible = false;
            BindGrid1();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void LnkPrintReceipt_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["CHECK_FLAG"] = "PRINT";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkDens_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["CHECK_FLAG"] = "DENS";
            LinkButton objlink = (LinkButton)sender;
            string id = objlink.CommandArgument;
            string[] dens = id.ToString().Split('_');
            Session["densset"] = dens[0].ToString();
            Session["densamt"] = CurrentCls.GetTotalAmt(Session["BRCD"].ToString(), Session["densset"].ToString(), Session["EntryDate"].ToString());
            Session["denssubgl"] = dens[2].ToString();
            Session["densact"] = dens[3].ToString();

            string i = CurrentCls.CheckDenom(Session["BRCD"].ToString(), Session["densset"].ToString(), Session["EntryDate"].ToString());
            if (i == null)
            {
                string redirectURL = "FrmCashDenom.aspx";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else
            {
                lblMessage.Text = "Already Cash Denominations..!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Button click event

    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Flag"].ToString() == "AD")
            {
                if (ddlAppType.SelectedValue.ToString() == "1")
                {
                    ShareSuspGl = SA.GetShareSuspGl(Session["BRCD"].ToString());
                    CUSTNO = Convert.ToInt32(CM.GetMemNo("1")); 

                    // Insert Record Into Avs_Acc Table Under subglcode (e.g 44)
                    ShareAccNo = accop.insert(Session["BRCD"].ToString(), "4", ShareSuspGl.ToString(), CUSTNO.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), "1", "1", "", "", "", "", "", "", "", "", "", "", "", "", "0", "0", "1003", Session["BRCD"].ToString(), "0", "0", "");
                    ShareAccNo = App.insert(BRCD: Session["BRCD"].ToString(), CUSTNO: CUSTNO.ToString(), CUSTNAME: txtcusname.Text, OPENINGDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString(), PCMAC: Session["MID"].ToString(), MEM_TYPE: ddlAppType.SelectedValue, MEMBERNO: txtMemNo.Text, TOTMEMBERN: txtShareAccNo.Text, FLAT_ROOMNO: ddlWard.SelectedValue, ADDRESS: txtAddress.Text, DISTRICT: dddistrict.SelectedValue, STATE: ddstate.SelectedValue, AREA_TALUKA: ddtaluka.SelectedValue, PINCODE: txtpin.Text, MOBILE1: txtMob.Text, EMAIL_ID: txtemailid.Text, COMM_NAME: txtNamed.Text, COM_MOBILE1: txtdMob1.Text, COMM_NAME1: txtsecname.Text, COM_MOBILE2: txtMobile.Text);

                 
                    //Added By Amol On 16/01/2018 for Create accounts
                    SA.CreateSharAccounts(Session["BRCD"].ToString(),CUSTNO.ToString(), ShareSuspGl.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                }
                else if (ddlAppType.SelectedValue.ToString() == "2")
                {
                    ShareSuspGl = SA.GetShareSuspGl(Session["BRCD"].ToString());
                    ShareAccNo = Convert.ToInt32(SA.GetShareAccNo(Session["BRCD"].ToString(), ShareSuspGl.ToString()));
                    dt = SA.CheckExists(Session["BRCD"].ToString(), ShareSuspGl.ToString(), txtShareAccNo.Text.Trim().ToString() == "" ? "0" : txtShareAccNo.Text.Trim().ToString());
                    CUSTNO = Convert.ToInt32(CM.GetMemNo2("1"));
                    ShareAccNo = App.insert(BRCD: Session["BRCD"].ToString(), CUSTNO: CUSTNO.ToString(), CUSTNAME: txtcusname.Text, OPENINGDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString(), PCMAC: Session["MID"].ToString(), MEM_TYPE: ddlAppType.SelectedValue, MEMBERNO: txtMemNo.Text, TOTMEMBERN: txtShareAccNo.Text, FLAT_ROOMNO: ddlWard.SelectedValue, ADDRESS: txtAddress.Text, DISTRICT: dddistrict.SelectedValue, STATE: ddstate.SelectedValue, AREA_TALUKA: ddtaluka.SelectedValue, PINCODE: txtpin.Text, MOBILE1: txtMob.Text, EMAIL_ID: txtemailid.Text, COMM_NAME: txtNamed.Text, COM_MOBILE1: txtdMob1.Text, COMM_NAME1: txtsecname.Text, COM_MOBILE2: txtMobile.Text);

                    if (dt.Rows.Count <= 0)
                    {
                        // Insert Record Into Avs_Acc Table Under subglcode (e.g 44)
                        ShareAccNo = accop.insert(Session["BRCD"].ToString(), "4", ShareSuspGl.ToString(), CUSTNO.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), "1", "1", "", "", "", "", "", "", "", "", "", "", "", "", "0", "0", "1003", Session["BRCD"].ToString(), "0", "0", "");
                      
                        SA.CreateSharAccounts(Session["BRCD"].ToString(), txtcustno.Text.ToString(), ShareSuspGl.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    }
                }
                else if (ddlAppType.SelectedValue.ToString() == "3")
                {
                    BClassGl = SA.GetBClassGl("1");
                    CUSTNO = Convert.ToInt32(CM.GetMemNo2("1")); 

                    // Insert Record Into Avs_Acc Table Under subglcode (e.g 43)
                    ShareAccNo = accop.insert("1", "4", BClassGl.ToString(), txtcustno.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), "1", "1", "", "", "", "", "", "", "", "", "", "", "", "", "0", "0", "1003", Session["BRCD"].ToString(), "0", "0","");
                    ShareAccNo = App.insert(BRCD: Session["BRCD"].ToString(), CUSTNO: CUSTNO.ToString(), CUSTNAME: txtcusname.Text, OPENINGDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString(), PCMAC: Session["MID"].ToString(), MEM_TYPE: ddlAppType.SelectedValue, MEMBERNO: txtMemNo.Text, TOTMEMBERN: txtShareAccNo.Text, FLAT_ROOMNO: ddlWard.SelectedValue, ADDRESS: txtAddress.Text, DISTRICT: dddistrict.SelectedValue, STATE: ddstate.SelectedValue, AREA_TALUKA: ddtaluka.SelectedValue, PINCODE: txtpin.Text, MOBILE1: txtMob.Text, EMAIL_ID: txtemailid.Text, COMM_NAME: txtNamed.Text, COM_MOBILE1: txtdMob1.Text, COMM_NAME1: txtsecname.Text, COM_MOBILE2: txtMobile.Text);

                    SA.CreateSharAccounts(Session["BRCD"].ToString(), CUSTNO.ToString(), ShareSuspGl.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                }

                if (ddlPayType.SelectedValue.ToString() == "1")
                {
                    if (ShareAccNo > 0)
                    {
                        //For Cash
                        Result = SA.InsertData(Session["BRCD"].ToString(), ShareAccNo.ToString(), ddlAppType.SelectedValue, txtMemNo.Text.Trim().ToString() == "" ? "0" : txtMemNo.Text.Trim().ToString(), CUSTNO.ToString(), txtNoOfShr.Text.Trim().ToString(), txtShrValue.Text.Trim().ToString(), txtTotShr.Text.Trim().ToString(), txtEntFee.Text.Trim().ToString(), txtAccNo.Text.ToString() == "" ? "0" : txtAccNo.Text.Trim().ToString(), txtSavFee.Text.Trim().ToString() == "" ? "0" : txtSavFee.Text.Trim().ToString(), txtOther1.Text.Trim().ToString() == "" ? "0" : txtOther1.Text.Trim().ToString(), txtOther2.Text.Trim().ToString() == "" ? "0" : txtOther2.Text.Trim().ToString(), txtOther3.Text.Trim().ToString() == "" ? "0" : txtOther3.Text.Trim().ToString(), txtOther4.Text.Trim().ToString() == "" ? "0" : txtOther4.Text.Trim().ToString(), txtOther5.Text.Trim().ToString() == "" ? "0" : txtOther5.Text.Trim().ToString(), txtMemWelFee.Text.Trim().ToString() == "" ? "0" : txtMemWelFee.Text.Trim().ToString(), txtSerChrFee.Text.Trim().ToString() == "" ? "0" : txtSerChrFee.Text.Trim().ToString(), txtNomName1.Text == "" ? "" : txtNomName1.Text, TxtDOB1.Text == "" ? "" : TxtDOB1.Text, TxtAge1.Text == "" ? "" : TxtAge1.Text, ddlRelation1.SelectedValue, txtNomName2.Text == "" ? "" : txtNomName2.Text, TxtDOB2.Text == "" ? "" : TxtDOB2.Text, TxtAge2.Text == "" ? "" : TxtAge2.Text, ddlRelation2.SelectedValue, ddlPayType.SelectedValue, txtRemark.Text.ToString(), "", "", "", "", "", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    }
                }
                else if (ddlPayType.SelectedValue.ToString() == "2")
                {
                    if (ShareAccNo > 0)
                    {
                        //For Transfer
                        Result = SA.InsertData(Session["BRCD"].ToString(), ShareAccNo.ToString(), ddlAppType.SelectedValue, txtMemNo.Text.Trim().ToString() == "" ? "0" : txtMemNo.Text.Trim().ToString(), CUSTNO.ToString(), txtNoOfShr.Text.Trim().ToString(), txtShrValue.Text.Trim().ToString(), txtTotShr.Text.Trim().ToString(), txtEntFee.Text.Trim().ToString(), txtAccNo.Text.ToString() == "" ? "0" : txtAccNo.Text.Trim().ToString(), txtSavFee.Text.Trim().ToString() == "" ? "0" : txtSavFee.Text.Trim().ToString(), txtOther1.Text.Trim().ToString() == "" ? "0" : txtOther1.Text.Trim().ToString(), txtOther2.Text.Trim().ToString() == "" ? "0" : txtOther2.Text.Trim().ToString(), txtOther3.Text.Trim().ToString() == "" ? "0" : txtOther3.Text.Trim().ToString(), txtOther4.Text.Trim().ToString() == "" ? "0" : txtOther4.Text.Trim().ToString(), txtOther5.Text.Trim().ToString() == "" ? "0" : txtOther5.Text.Trim().ToString(), txtMemWelFee.Text.Trim().ToString() == "" ? "0" : txtMemWelFee.Text.Trim().ToString(), txtSerChrFee.Text.Trim().ToString() == "" ? "0" : txtSerChrFee.Text.Trim().ToString(), txtNomName1.Text == "" ? "" : txtNomName1.Text, TxtDOB1.Text == "" ? "" : TxtDOB1.Text, TxtAge1.Text == "" ? "" : TxtAge1.Text, ddlRelation1.SelectedValue, txtNomName2.Text == "" ? "" : txtNomName2.Text, TxtDOB2.Text == "" ? "" : TxtDOB2.Text, TxtAge2.Text == "" ? "" : TxtAge2.Text, ddlRelation2.SelectedValue, ddlPayType.SelectedValue, txtRemark.Text.ToString(), ViewState["GlCode"].ToString(), txtProdType1.Text.ToString(), TxtAccNo1.Text.ToString(), "", "", txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    }
                }
                else if (ddlPayType.SelectedValue.ToString() == "4")
                {
                    if (ShareAccNo > 0)
                    {
                        //For Cheque
                        Result = SA.InsertData(Session["BRCD"].ToString(), ShareAccNo.ToString(), ddlAppType.SelectedValue, txtMemNo.Text.Trim().ToString() == "" ? "0" : txtMemNo.Text.Trim().ToString(), CUSTNO.ToString(), txtNoOfShr.Text.Trim().ToString(), txtShrValue.Text.Trim().ToString(), txtTotShr.Text.Trim().ToString(), txtEntFee.Text.Trim().ToString(), txtAccNo.Text.ToString() == "" ? "0" : txtAccNo.Text.Trim().ToString(), txtSavFee.Text.Trim().ToString() == "" ? "0" : txtSavFee.Text.Trim().ToString(), txtOther1.Text.Trim().ToString() == "" ? "0" : txtOther1.Text.Trim().ToString(), txtOther2.Text.Trim().ToString() == "" ? "0" : txtOther2.Text.Trim().ToString(), txtOther3.Text.Trim().ToString() == "" ? "0" : txtOther3.Text.Trim().ToString(), txtOther4.Text.Trim().ToString() == "" ? "0" : txtOther4.Text.Trim().ToString(), txtOther5.Text.Trim().ToString() == "" ? "0" : txtOther5.Text.Trim().ToString(), txtMemWelFee.Text.Trim().ToString() == "" ? "0" : txtMemWelFee.Text.Trim().ToString(), txtSerChrFee.Text.Trim().ToString() == "" ? "0" : txtSerChrFee.Text.Trim().ToString(), txtNomName1.Text == "" ? "" : txtNomName1.Text, TxtDOB1.Text == "" ? "" : TxtDOB1.Text, TxtAge1.Text == "" ? "" : TxtAge1.Text, ddlRelation1.SelectedValue, txtNomName2.Text == "" ? "" : txtNomName2.Text, TxtDOB2.Text == "" ? "" : TxtDOB2.Text, TxtAge2.Text == "" ? "" : TxtAge2.Text, ddlRelation2.SelectedValue, ddlPayType.SelectedValue, txtRemark.Text.ToString(), txtProdType1.Text.ToString(), txtProdType1.Text.ToString(), "0", TxtChequeNo.Text.Trim().ToString(), TxtChequeDate.Text.Trim().ToString(), txtNarration.Text.ToString() == "" ? "" : txtNarration.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
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
                   // autoglname.ContextKey = Session["BRCD"].ToString();
                    //txtAppNo.Text = SA.GetAppNo(Session["BRCD"].ToString()).ToString();
                    if (ddlAppType.SelectedValue.ToString() == "1" || ddlAppType.SelectedValue.ToString() == "2")
                    {
                        lblMessage.Text = "Application Created Successfully With Member No : " + CUSTNO.ToString() + " And AppNo : " + AppNum.ToString() + "";
                        ModalPopup.Show(this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Share_create _" + ShareAccNo.ToString() + "_" + AppNum.ToString() + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearData();
                        return;
                    }
                    if (ddlAppType.SelectedValue.ToString() == "3")
                    {
                        lblMessage.Text = "Application Created Successfully With B-ClassAccNo : " + ShareAccNo.ToString() + " And AppNo : " + AppNum.ToString() + "";
                        ModalPopup.Show(this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Share_create _" + ShareAccNo.ToString() + "_" + AppNum.ToString() + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearData();
                        return;
                    }

                }
            }
            else if (ViewState["Flag"].ToString() == "AT")
            {
                AuthoriseApplication();
                div_cashrct.Visible = true;
                BindGrid1();
            }
            else if (ViewState["Flag"].ToString() == "DL")
            {
                resultint = SA.DeleteData(ViewState["Id"].ToString(), ViewState["CustNo"].ToString(), Session["MID"].ToString());

                if (resultint > 0)
                {
                    if (resultint > 0)
                    {
                        resultint = App.DELETEDATA(BRCD: Session["BRCD"].ToString(), CUSTNO: ViewState["CustNo"].ToString(), CUSTNAME: txtcusname.Text, OPENINGDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString(), PCMAC: Session["MID"].ToString(), MEM_TYPE: ddlAppType.SelectedValue, MEMBERNO: txtMemNo.Text, TOTMEMBERN: txtShareAccNo.Text, FLAT_ROOMNO: ddlWard.SelectedValue, ADDRESS: txtAddress.Text, DISTRICT: dddistrict.SelectedValue, STATE: ddstate.SelectedValue, AREA_TALUKA: ddtaluka.SelectedValue, PINCODE: txtpin.Text, MOBILE1: txtMob.Text, EMAIL_ID: txtemailid.Text, COMM_NAME: txtNamed.Text, COM_MOBILE1: txtdMob1.Text, COMM_NAME1: txtsecname.Text, COM_MOBILE2: txtMobile.Text);

                             
                        lblMessage.Text = "Delete Data Successfully...!!";
                        ModalPopup.Show(this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Share_Del _" + ShareAccNo.ToString() + "_" + AppNum.ToString() + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearData();
                        BindGrid();
                        return;
                    }
                }
            }
            if (ViewState["Flag"].ToString() == "CL")
            {
                int Mid = SA.CheckMid(ViewState["Id"].ToString(), ViewState["CustNo"].ToString());

                if (Mid != Convert.ToInt32(Session["MID"].ToString()))
                {
                    resultint = SA.CancelShareData(ViewState["Id"].ToString(), ViewState["CustNo"].ToString(), Session["MID"].ToString());

                    if (resultint > 0)
                    {
                        lblMessage.Text = "Cancel Application Successfully...!!";
                        ModalPopup.Show(this.Page);
                        ClearData();
                        BindAuthoApp();
                        return;
                    }
                }
                else
                {
                    lblMessage.Text = "Maker Not Cancel Application...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnReceipt_Click(object sender, EventArgs e)
    {
        try
        {
            string SetNo = ViewState["SetNo"].ToString();
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Share_Receipt _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            string redirectURL = "FrmRView.aspx?SETNO=" + SetNo + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + Session["EntryDate"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&FN=R&rptname=RptReceiptPrintHSFM_ShareApp.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnBatchAllocate_Click(object sender, EventArgs e)
    {
        try
        {
            string[] AppDtls;

            foreach (GridViewRow gvRow in grdAppDetails.Rows)
            {
                if (((CheckBox)gvRow.FindControl("chkBox")).Checked)
                {
                    AppDtls = grdAppDetails.DataKeys[gvRow.DataItemIndex].Value.ToString().Split('_');

                    ApplBatchAllocate(AppDtls[0].ToString(), AppDtls[1].ToString(), AppDtls[2].ToString(), "11");
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Exit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string strnumid = objlink.CommandArgument;
            string[] sel = strnumid.Split('_');

            ViewState["Id"] = sel[0].ToString();
            ViewState["CustNo"] = sel[1].ToString();
            ViewState["AppNo"] = sel[2].ToString();
            txtcustno.Visible = true;
            txtcustno.Text = ViewState["CustNo"].ToString();
            callData();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Public Functions

    public void SetDefaultAmount()
    {
        try
        {
            dt = new DataTable();
            dt = SA.GetShrInfo(Session["BRCD"].ToString());
            if (dt.Rows.Count > 0)
            {
                ViewState["PrCode"] = dt.Rows[0]["SAVING_GL"].ToString();
                txtNoOfShr.Text = Convert.ToInt32(dt.Rows[0]["NO_OF_SHARES"].ToString()).ToString();
                txtShrValue.Text = Convert.ToDouble(dt.Rows[0]["SHR_VALUE"].ToString()).ToString();

                LblName0.InnerText = SA.GetGlName(Session["BRCD"].ToString(), ViewState["PrCode"].ToString());
                LblName15.InnerText = SA.GetGlName(Session["BRCD"].ToString(), ViewState["PrCode"].ToString());
                txtEntFee.Text = Convert.ToDouble(dt.Rows[0]["EnterenceAmt"].ToString() == "" ? "0" : dt.Rows[0]["EnterenceAmt"].ToString()).ToString();
                txtOther1.Text = Convert.ToDouble(dt.Rows[0]["WelFareAmt"].ToString() == "" ? "0" : dt.Rows[0]["WelFareAmt"].ToString()).ToString();
                txtMemWelFee.Text = Convert.ToDouble(dt.Rows[0]["WelFareLoanAmt"].ToString() == "" ? "0" : dt.Rows[0]["WelFareLoanAmt"].ToString()).ToString();

                txtTotShr.Text = Convert.ToInt32(Convert.ToInt32(dt.Rows[0]["NO_OF_SHARES"].ToString()) * Convert.ToDouble(dt.Rows[0]["SHR_VALUE"].ToString())).ToString();
                txtTotAmount.Text = Convert.ToInt32(Convert.ToInt32(dt.Rows[0]["NO_OF_SHARES"].ToString()) * Convert.ToDouble(dt.Rows[0]["SHR_VALUE"].ToString()) + Convert.ToDouble(txtEntFee.Text.Trim().ToString()) + Convert.ToDouble(txtOther5.Text.Trim().ToString()) + Convert.ToDouble(txtMemWelFee.Text.Trim().ToString())).ToString();
                txtSavFee.Text = "";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BindUnallotedGrid()
    {
        try
        {
            SA.BindUnallotedGrid(grdAppDetails, Session["BRCD"].ToString(), Session["EntryDate"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearData()
    {
        ddlAppType.SelectedIndex = 0;
        txtMemNo.Text = "";
        txtShareAccNo.Text = "";

        txtcustno.Text = "";
        txtcusname.Text = "";
        txtAppNo.Text = "";
        txtNoOfShr.Text = "";
        txtShrValue.Text = "";
        txtTotShr.Text = "";
        txtAccNo.Text = "";
        txtAccName.Text = "";
        txtSavFee.Text = "";
        txtEntFee.Text = "";
        txtOther1.Text = "";
        txtOther2.Text = "";
        txtOther3.Text = "";
        txtOther4.Text = "";
        txtOther5.Text = "";
        txtMemWelFee.Text = "";
        txtSerChrFee.Text = "";
        txtNamed.Text = "";
        txtsecname.Text = "";
        txtdMob1.Text = "";
        ddlPermises.SelectedIndex = 0;
        ddtaluka.SelectedValue = "0";
        txtNomName1.Text = "";
        TxtAge1.Text = "";
        txtNomName2.Text = "";
        TxtAge2.Text = "";
        txtRemark.Text = "";
        txtTotAmount.Text = "";
        txtAddress.Text = "";
        ddstate.SelectedValue = "0";
        dddistrict.SelectedValue = "0";
        ddlWard.SelectedValue = "0";
        txtpin.Text = "";
        txtMob.Text = "";
        txtMobile.Text = "";
        txtemailid.Text = "";

        txtProdType1.Text = "";
        txtProdName1.Text = "";
        TxtAccNo1.Text = "";
        TxtAccName1.Text = "";
        txtBalance.Text = "";
        TxtChequeNo.Text = "";
        TxtChequeDate.Text = Session["EntryDate"].ToString();

        ddlPayType.SelectedIndex = 0;
        ddlRelation1.SelectedIndex = 0;
        ddlRelation2.SelectedIndex = 0;

        Transfer.Visible = false;
        divIntrument.Visible = false;
        divNarration.Visible = false;

        TxtDOB1.Text = Session["EntryDate"].ToString();
        TxtDOB2.Text = Session["EntryDate"].ToString();
    }

    public void ENDN(bool TF)
    {
        try
        {
            ddlAppType.Enabled = TF;
            txtMemNo.Enabled = TF;
            txtcustno.Enabled = TF;
            txtcusname.Enabled = TF;
            txtNoOfShr.Enabled = TF;
            txtShrValue.Enabled = TF;
            //txtTotShr.Enabled = TF;
            txtAccNo.Enabled = TF;
            txtAccName.Enabled = TF;
            txtEntFee.Enabled = TF;
            txtSavFee.Enabled = TF;
            txtOther1.Enabled = TF;
            txtOther2.Enabled = TF;
            txtOther3.Enabled = TF;
            txtOther4.Enabled = TF;
            txtOther5.Enabled = TF;
            txtMemWelFee.Enabled = TF;
            txtSerChrFee.Enabled = TF;

            txtNomName1.Enabled = TF;
            TxtAge1.Enabled = TF;
            txtNomName2.Enabled = TF;
            TxtAge2.Enabled = TF;
            ddlRelation1.Enabled = TF;
            ddlRelation2.Enabled = TF;
            TxtDOB1.Enabled = TF;
            TxtDOB2.Enabled = TF;
            ddlPayType.Enabled = TF;
            txtRemark.Enabled = TF;

            txtProdType1.Enabled = TF;
            txtProdName1.Enabled = TF;
            TxtAccNo1.Enabled = TF;
            TxtAccName1.Enabled = TF;
            TxtChequeNo.Enabled = TF;
            TxtChequeDate.Enabled = TF;
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
            SA.BindData(grdMaster, Session["BRCD"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindAuthoApp()
    {
        try
        {
            SA.BindAuthoData(grdMaster, Session["BRCD"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindGridForView()
    {
        try
        {
            SA.BindDataForView(grdMaster, Session["BRCD"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void AuthoriseApplication()
    {
        try
        {
            string SetNo = "", SetNo2 = "", GlCode = "";//, SAccNo = "";

            int Mid = SA.CheckMid(ViewState["Id"].ToString(), ViewState["CustNo"].ToString());

            //if (Mid != Convert.ToInt32(Session["MID"].ToString()))
            //{
                string Stage = SA.CheckStage(ViewState["Id"].ToString(), ViewState["CustNo"].ToString());

                if (Stage != "1003" && Stage != "1004")
                {
                    if (ddlAppType.SelectedValue.ToString() == "1" || ddlAppType.SelectedValue.ToString() == "2")
                    {
                        if (ddlPayType.SelectedValue.ToString() == "1")
                        {
                            SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();

                            //For Cash
                            dt = SA.GetProdCode(Session["BRCD"].ToString());

                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToDouble(txtTotAmount.Text.ToString() == "" ? "0" : txtTotAmount.Text.ToString()) > 0)
                                {
                                    double FinalValue;
                                    FinalValue = Convert.ToDouble(Convert.ToDouble(txtTotShr.Text.ToString()) + Convert.ToDouble(txtEntFee.Text.ToString()) + Convert.ToDouble(txtSavFee.Text.ToString()) + Convert.ToDouble(txtOther1.Text.ToString()) + Convert.ToDouble(txtOther2.Text.ToString()) + Convert.ToDouble(txtOther3.Text.ToString()) + Convert.ToDouble(txtOther4.Text.ToString()) + Convert.ToDouble(txtOther5.Text.ToString()) + Convert.ToDouble(txtMemWelFee.Text.ToString()) + Convert.ToDouble(txtSerChrFee.Text.ToString()));

                                    string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());

                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", cgl, "0",
                                              "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", FinalValue.ToString(), "2", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                              "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                }

                                if (resultint > 0)
                                {
                                    if (Convert.ToDouble(txtTotShr.Text.ToString() == "" ? "0" : txtTotShr.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["SHARES_GL"].ToString());

                                        resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["SHARES_GL"].ToString(), txtShareAccNo.Text.Trim().ToString(),
                                                    "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                    "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                    }
                                    if (Convert.ToDouble(txtEntFee.Text.ToString() == "" ? "0" : txtEntFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["ENTRY_GL"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["ENTRY_GL"].ToString(), Session["BRCD"].ToString());
                                         if (RSPara == "Y")
                                         {
                                             string Para = SA.GetParameter();
                                             string MWelAccNo = "";
                                             //if (Para == "HO")
                                             //{
                                             //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                             //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             //else
                                             //{
                                             MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["ENTRY_GL"].ToString(), txtcustno.Text.ToString());
                                             // int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                                       (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtEntFee.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                       "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                         else
                                         {
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                                         "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtEntFee.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                    }
                                    if (Convert.ToDouble(txtSavFee.Text.ToString() == "" ? "0" : txtSavFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["SAVING_GL"].ToString());
                                        //SAccNo = SA.SavAccNo(ViewState["Id"].ToString(), ViewState["CustNo"].ToString());
                                        //string RSPara = SA.GetRSPara(dt.Rows[0]["SAVING_GL"].ToString(), Session["BRCD"].ToString());
                                        // if (RSPara == "Y")
                                        // {
                                        //     string Para = SA.GetParameter();
                                        //     string MWelAccNo = "";
                                        //     //if (Para == "HO")
                                        //     //{
                                        //     //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                        //     //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                        //     //}
                                        //     //else
                                        //     //{
                                        //     MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["SAVING_GL"].ToString(), txtcustno.Text);
                                        //     // int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                        //     //}
                                        //     resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(),GlCode,dt.Rows[0]["SAVING_GL"].ToString(),
                                        //                 MWelAccNo, "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSavFee.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                        //                 "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        // }
                                        // else
                                        // {
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["SAVING_GL"].ToString(),
                                                         txtAccNo.Text.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSavFee.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                         //}
                                    }
                                    if (Convert.ToDouble(txtOther1.Text.ToString() == "" ? "0" : txtOther1.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL1"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(),  txtcustno.Text.ToString());
                                               // int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                                        (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther1.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp",  txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                                        "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther1.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtOther2.Text.ToString() == "" ? "0" : txtOther2.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL2"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL2"].ToString(), Session["BRCD"].ToString());
                                         if (RSPara == "Y")
                                         {
                                             string Para = SA.GetParameter();
                                             string MWelAccNo = "";
                                             //if (Para == "HO")
                                             //{
                                             //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                             //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             //else
                                             //{
                                             MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL2"].ToString(), txtcustno.Text.ToString());
                                             // int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL2"].ToString(),
                                                         (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther2.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                         else
                                         {
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL2"].ToString(),
                                                         "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther2.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                    }
                                    if (Convert.ToDouble(txtOther3.Text.ToString() == "" ? "0" : txtOther3.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL3"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL3"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL3"].ToString(), txtcustno.Text.ToString());
                                            // int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL3"].ToString(),
                                                        (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther3.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL3"].ToString(),
                                                        "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther3.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtOther4.Text.ToString() == "" ? "0" : txtOther4.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL4"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL4"].ToString(), Session["BRCD"].ToString());
                                         if (RSPara == "Y")
                                         {
                                             string Para = SA.GetParameter();
                                             string MWelAccNo = "";
                                             //if (Para == "HO")
                                             //{
                                             //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                             //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             //else
                                             //{
                                             MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL4"].ToString(), txtcustno.Text.ToString());
                                             // int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL4"].ToString(),
                                                         (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther4.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                         else
                                         {
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL4"].ToString(),
                                                         "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther4.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                    }
                                    if (Convert.ToDouble(txtOther5.Text.ToString() == "" ? "0" : txtOther5.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL5"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL5"].ToString(), Session["BRCD"].ToString());
                                         if (RSPara == "Y")
                                         {
                                             string Para = SA.GetParameter();
                                             string MWelAccNo = "";
                                             //if (Para == "HO")
                                             //{
                                             //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                             //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             //else
                                             //{
                                             MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL5"].ToString(), txtcustno.Text.ToString());
                                             // int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL5"].ToString(),
                                                                                     (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther5.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                                                     "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", CUSTNO.ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                         else
                                         {
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL5"].ToString(),
                                                         "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther5.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                    }
                                    if (Convert.ToDouble(txtMemWelFee.Text.ToString() == "" ? "0" : txtMemWelFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["MemberWel_GL"].ToString(), Session["BRCD"].ToString());
                                         if (RSPara == "Y")
                                         {
                                             string Para = SA.GetParameter();
                                             string MWelAccNo = "";
                                             //if (Para == "HO")
                                             //{
                                             //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["MemberWel_GL"].ToString());
                                             //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["MemberWel_GL"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             //else
                                             //{
                                             MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString(), txtcustno.Text.ToString());
                                                 //int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                                                                (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtMemWelFee.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                                                "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", CUSTNO.ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                         else
                                         {
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                                         "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtMemWelFee.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                    }
                                    if (Convert.ToDouble(txtSerChrFee.Text.ToString() == "" ? "0" : txtSerChrFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["Service_GL"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["Service_GL"].ToString(), Session["BRCD"].ToString());
                                         if (RSPara == "Y")
                                         {
                                             string Para = SA.GetParameter();
                                             string MWelAccNo = "";
                                             //if (Para == "HO")
                                             //{
                                             //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                             //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             //else
                                             //{
                                             MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["Service_GL"].ToString(), txtcustno.Text.ToString());
                                             // int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["Service_GL"].ToString(),
                                                       (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSerChrFee.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                       "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                         else
                                         {
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["Service_GL"].ToString(),
                                                         "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSerChrFee.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                    }
                                }
                            }
                        }
                        else if (ddlPayType.SelectedValue.ToString() == "2")
                        {
                            SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();

                            //For Transfer
                            dt = SA.GetProdCode(Session["BRCD"].ToString());

                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToDouble(txtTotAmount.Text.ToString() == "" ? "0" : txtTotAmount.Text.ToString()) > 0)
                                {
                                    double FinalValue;
                                    FinalValue = Convert.ToDouble(Convert.ToDouble(txtTotShr.Text.ToString()) + Convert.ToDouble(txtEntFee.Text.ToString()) + Convert.ToDouble(txtSavFee.Text.ToString()) + Convert.ToDouble(txtOther1.Text.ToString()) + Convert.ToDouble(txtOther2.Text.ToString()) + Convert.ToDouble(txtOther3.Text.ToString()) + Convert.ToDouble(txtOther4.Text.ToString()) + Convert.ToDouble(txtOther5.Text.ToString()) + Convert.ToDouble(txtMemWelFee.Text.ToString()) + Convert.ToDouble(txtSerChrFee.Text.ToString()));

                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString(), txtProdType1.Text.Trim().ToString(),
                                              TxtAccNo1.Text.Trim().ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", FinalValue.ToString(), "2", "7", "TR", SetNo, TxtChequeNo.Text.ToString() == "" ? "" : TxtChequeNo.Text.ToString(), TxtChequeDate.Text.ToString() == "" ? "" : TxtChequeDate.Text.ToString(), "0", "0", "1003",
                                              "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                }

                                if (resultint > 0)
                                {
                                    if (Convert.ToDouble(txtTotShr.Text.ToString() == "" ? "0" : txtTotShr.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["SHARES_GL"].ToString());

                                        resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["SHARES_GL"].ToString(), txtShareAccNo.Text.Trim().ToString(),
                                                    "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                    "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                    }
                                    if (Convert.ToDouble(txtEntFee.Text.ToString() == "" ? "0" : txtEntFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["ENTRY_GL"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["ENTRY_GL"].ToString(), Session["BRCD"].ToString());
                                          if (RSPara == "Y")
                                          {
                                              string Para = SA.GetParameter();
                                              string MWelAccNo = "";
                                              //if (Para == "HO")
                                              //{
                                              //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                              //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                              //}
                                              //else
                                              //{
                                              MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["ENTRY_GL"].ToString(), txtcustno.Text.ToString());
                                              //int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                              //}
                                              resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                                         (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtEntFee.Text.ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                          }
                                          else
                                          {
                                              resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                                          "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtEntFee.Text.ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                          "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                          }
                                    }
                                    if (Convert.ToDouble(txtSavFee.Text.ToString() == "" ? "0" : txtSavFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["SAVING_GL"].ToString());
                                        //SAccNo = SA.SavAccNo(ViewState["Id"].ToString(), ViewState["CustNo"].ToString());
                                        //string RSPara = SA.GetRSPara(dt.Rows[0]["SAVING_GL"].ToString(), Session["BRCD"].ToString());
                                        //if (RSPara == "Y")
                                        //{
                                        //    string Para = SA.GetParameter();
                                        //    string MWelAccNo = "";
                                        //    //if (Para == "HO")
                                        //    //{
                                        //    //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                        //    //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                        //    //}
                                        //    //else
                                        //    //{
                                        //    MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["SAVING_GL"].ToString(), txtcustno.Text);
                                        //    //int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                        //    //}
                                        //    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(),GlCode,dt.Rows[0]["SAVING_GL"].ToString(),
                                        //               MWelAccNo, "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSavFee.Text.ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                        //               "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        //}
                                        //else
                                        //{
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(),GlCode,dt.Rows[0]["SAVING_GL"].ToString(),
                                                        txtAccNo.Text.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSavFee.Text.ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", CUSTNO.ToString(), txtcusname.Text.ToString(), "0", "0");
                                        //}
                                    }
                                    if (Convert.ToDouble(txtOther1.Text.ToString() == "" ? "0" : txtOther1.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL1"].ToString(), Session["BRCD"].ToString());
                                          if (RSPara == "Y")
                                          {
                                              string Para = SA.GetParameter();
                                              string MWelAccNo = "";
                                              //if (Para == "HO")
                                              //{
                                              //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                              //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                              //}
                                              //else
                                              //{
                                              MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), txtcustno.Text.ToString());
                                                  //int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                              //}
                                              resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                                         (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther1.Text.ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                          }
                                          else
                                          {
                                              resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                                          "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther1.Text.ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                          "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                          }
                                    }
                                    if (Convert.ToDouble(txtOther2.Text.ToString() == "" ? "0" : txtOther2.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL2"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL2"].ToString(), Session["BRCD"].ToString());
                                          if (RSPara == "Y")
                                          {
                                              string Para = SA.GetParameter();
                                              string MWelAccNo = "";
                                              //if (Para == "HO")
                                              //{
                                              //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                              //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                              //}
                                              //else
                                              //{
                                              MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL2"].ToString(), txtcustno.Text.ToString());
                                              //int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                              //}
                                              resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL2"].ToString(),
                                                         (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther2.Text.ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                          }
                                          else
                                          {
                                              resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL2"].ToString(),
                                                          "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther2.Text.ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                          "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                          }
                                    }
                                    if (Convert.ToDouble(txtOther3.Text.ToString() == "" ? "0" : txtOther3.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL3"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL3"].ToString(), Session["BRCD"].ToString());
                                          if (RSPara == "Y")
                                          {
                                              string Para = SA.GetParameter();
                                              string MWelAccNo = "";
                                              //if (Para == "HO")
                                              //{
                                              //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                              //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                              //}
                                              //else
                                              //{
                                              MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL3"].ToString(), txtcustno.Text.ToString());
                                              //int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                              //}
                                              resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL3"].ToString(),
                                                         (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther3.Text.Trim().ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                          }
                                          else
                                          {
                                              resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL3"].ToString(),
                                                          "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther3.Text.Trim().ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                          "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                          }
                                    }
                                    if (Convert.ToDouble(txtOther4.Text.ToString() == "" ? "0" : txtOther4.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL4"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL4"].ToString(), Session["BRCD"].ToString());
                                          if (RSPara == "Y")
                                          {
                                              string Para = SA.GetParameter();
                                              string MWelAccNo = "";
                                              //if (Para == "HO")
                                              //{
                                              //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                              //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                              //}
                                              //else
                                              //{
                                              MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL4"].ToString(), txtcustno.Text.ToString());
                                              //int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                              //}
                                              resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL4"].ToString(),
                                                           (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther4.Text.Trim().ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                           "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                          }
                                          else
                                          {
                                              resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL4"].ToString(),
                                                          "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther4.Text.Trim().ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                          "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                          }
                                    }
                                    if (Convert.ToDouble(txtOther5.Text.ToString() == "" ? "0" : txtOther5.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL5"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL5"].ToString(), Session["BRCD"].ToString());
                                          if (RSPara == "Y")
                                          {
                                              string Para = SA.GetParameter();
                                              string MWelAccNo = "";
                                              //if (Para == "HO")
                                              //{
                                              //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                              //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                              //}
                                              //else
                                              //{
                                              MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL5"].ToString(), txtcustno.Text.ToString());
                                              //int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                              //}
                                              resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL5"].ToString(),
                                                          (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther5.Text.Trim().ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                          "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                          }
                                          else
                                          {
                                              resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL5"].ToString(),
                                                          "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther5.Text.Trim().ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                          "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                          }
                                    }
                                    if (Convert.ToDouble(txtMemWelFee.Text.ToString() == "" ? "0" : txtMemWelFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["MemberWel_GL"].ToString(), Session["BRCD"].ToString());
                                          if (RSPara == "Y")
                                          {
                                              string Para = SA.GetParameter();
                                              string MWelAccNo = "";
                                              //if (Para == "HO")
                                              //{
                                              //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["MemberWel_GL"].ToString());
                                              //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["MemberWel_GL"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                              //}
                                              //else
                                              //{
                                              MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString(), txtcustno.Text.ToString());
                                                  //int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                              //}
                                              resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                                         (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtMemWelFee.Text.Trim().ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                          }
                                          else
                                          {
                                              resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                                          "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtMemWelFee.Text.Trim().ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                          "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                          }
                                    }
                                    if (Convert.ToDouble(txtSerChrFee.Text.ToString() == "" ? "0" : txtSerChrFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["Service_GL"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["Service_GL"].ToString(), Session["BRCD"].ToString());
                                          if (RSPara == "Y")
                                          {
                                              string Para = SA.GetParameter();
                                              string MWelAccNo = "";
                                              //if (Para == "HO")
                                              //{
                                              //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                              //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                              //}
                                              //else
                                              //{
                                              MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["Service_GL"].ToString(), txtcustno.Text.ToString());
                                              //int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                              //}
                                              resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["Service_GL"].ToString(),
                                                        (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSerChrFee.Text.Trim().ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                          }
                                          else
                                          {
                                              resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["Service_GL"].ToString(),
                                                          "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSerChrFee.Text.Trim().ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                          "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                          }
                                    }
                                }
                            }
                        }
                        else if (ddlPayType.SelectedValue.ToString() == "4")
                        {
                            SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();

                            //For Cheque
                            dt = SA.GetProdCode(Session["BRCD"].ToString());

                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToDouble(txtTotAmount.Text.ToString() == "" ? "0" : txtTotAmount.Text.ToString()) > 0)
                                {
                                    double FinalValue;
                                    FinalValue = Convert.ToDouble(Convert.ToDouble(txtTotShr.Text.ToString()) + Convert.ToDouble(txtEntFee.Text.ToString()) + Convert.ToDouble(txtSavFee.Text.ToString()) + Convert.ToDouble(txtOther1.Text.ToString()) + Convert.ToDouble(txtOther2.Text.ToString()) + Convert.ToDouble(txtOther3.Text.ToString()) + Convert.ToDouble(txtOther4.Text.ToString()) + Convert.ToDouble(txtOther5.Text.ToString()) + Convert.ToDouble(txtMemWelFee.Text.ToString()) + Convert.ToDouble(txtSerChrFee.Text.ToString()));

                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString(), txtProdType1.Text.Trim().ToString(),
                                              TxtAccNo1.Text.Trim().ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", FinalValue.ToString(), "2", "5", "TR", SetNo, TxtChequeNo.Text.ToString() == "" ? "" : TxtChequeNo.Text.ToString(), TxtChequeDate.Text.ToString() == "" ? "" : TxtChequeDate.Text.ToString(), "0", "0", "1003",
                                              "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                }

                                if (resultint > 0)
                                {
                                    if (Convert.ToDouble(txtTotShr.Text.ToString() == "" ? "0" : txtTotShr.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["SHARES_GL"].ToString());

                                        resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["SHARES_GL"].ToString(), txtShareAccNo.Text.Trim().ToString(),
                                                    "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                    "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                    }
                                    if (Convert.ToDouble(txtEntFee.Text.ToString() == "" ? "0" : txtEntFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["ENTRY_GL"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["ENTRY_GL"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["ENTRY_GL"].ToString(), txtcustno.Text.ToString());
                                            // int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                                       (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtEntFee.Text.ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                       "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                                        "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtEntFee.Text.ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtSavFee.Text.ToString() == "" ? "0" : txtSavFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["SAVING_GL"].ToString());
                                        //SAccNo = SA.SavAccNo(ViewState["Id"].ToString(), ViewState["CustNo"].ToString());
                                        //string RSPara = SA.GetRSPara(dt.Rows[0]["SAVING_GL"].ToString(), Session["BRCD"].ToString());
                                        // if (RSPara == "Y")
                                        // {
                                        //     string Para = SA.GetParameter();
                                        //     string MWelAccNo = "";
                                        //     //if (Para == "HO")
                                        //     //{
                                        //     //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                        //     //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                        //     //}
                                        //     //else
                                        //     //{
                                        //     MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["SAVING_GL"].ToString(), txtcustno.Text);
                                        //     // int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                        //     //}
                                        //     resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(),GlCode,dt.Rows[0]["SAVING_GL"].ToString(),
                                        //                MWelAccNo, "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSavFee.Text.ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                        //                "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        // }
                                        // else
                                        // {
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode,dt.Rows[0]["SAVING_GL"].ToString(),
                                                         txtAccNo.Text.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSavFee.Text.ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                         //}
                                    }
                                    if (Convert.ToDouble(txtOther1.Text.ToString() == "" ? "0" : txtOther1.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL1"].ToString(), Session["BRCD"].ToString());
                                         if (RSPara == "Y")
                                         {
                                             string Para = SA.GetParameter();
                                             string MWelAccNo = "";
                                             //if (Para == "HO")
                                             //{
                                             //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                             //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             //else
                                             //{
                                             MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), txtcustno.Text.ToString());
                                                // int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                                         (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther1.Text.ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                         else
                                         {
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                                         "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther1.Text.ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                    }
                                    if (Convert.ToDouble(txtOther2.Text.ToString() == "" ? "0" : txtOther2.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL2"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL2"].ToString(), Session["BRCD"].ToString());
                                             if (RSPara == "Y")
                                             {
                                                 string Para = SA.GetParameter();
                                                 string MWelAccNo = "";
                                                 //if (Para == "HO")
                                                 //{
                                                 //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                                 //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                 //}
                                                 //else
                                                 //{
                                                 MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL2"].ToString(), txtcustno.Text.ToString());
                                                 // int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                                 //}
                                                 resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL2"].ToString(),
                                                            (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther2.Text.ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                            "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                             }
                                             else
                                             {
                                                 resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL2"].ToString(),
                                                             "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther2.Text.ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                             "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.ToString(), txtcusname.Text.ToString(), "0", "0");
                                             }
                                    }
                                    if (Convert.ToDouble(txtOther3.Text.ToString() == "" ? "0" : txtOther3.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL3"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL3"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL3"].ToString(), txtcustno.Text.ToString());
                                            // int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL3"].ToString(),
                                                        (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther3.Text.Trim().ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL3"].ToString(),
                                                        "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther3.Text.Trim().ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtOther4.Text.ToString() == "" ? "0" : txtOther4.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL4"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL4"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL4"].ToString(), txtcustno.Text);
                                            // int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL4"].ToString(),
                                                       (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther4.Text.Trim().ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                       "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL4"].ToString(),
                                                        "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther4.Text.Trim().ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtOther5.Text.ToString() == "" ? "0" : txtOther5.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL5"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL5"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL5"].ToString(), txtcustno.Text);
                                            // int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL5"].ToString(),
                                                        (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther5.Text.Trim().ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL5"].ToString(),
                                                        "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther5.Text.Trim().ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtMemWelFee.Text.ToString() == "" ? "0" : txtMemWelFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["MemberWel_GL"].ToString(), Session["BRCD"].ToString());
                                         if (RSPara == "Y")
                                         {
                                             string Para = SA.GetParameter();
                                             string MWelAccNo = "";
                                             //if (Para == "HO")
                                             //{
                                             //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["MemberWel_GL"].ToString());
                                             //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["MemberWel_GL"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             //else
                                             //{
                                             MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString(),txtcustno.Text);
                                               //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                                        (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtMemWelFee.Text.Trim().ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                         else
                                         {
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                                         "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtMemWelFee.Text.Trim().ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                    }
                                    if (Convert.ToDouble(txtSerChrFee.Text.ToString() == "" ? "0" : txtSerChrFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["Service_GL"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["Service_GL"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["Service_GL"].ToString(), txtcustno.Text);
                                            // int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["Service_GL"].ToString(),
                                                       (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSerChrFee.Text.Trim().ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                       "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["Service_GL"].ToString(),
                                                        "", "ShrAppNo " + ViewState["AppNo"].ToString() + "", "ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSerChrFee.Text.Trim().ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                }
                            }
                        }

                        if (resultint > 0)
                        {
                            resultint = SA.AuthoriseData(Session["BRCD"].ToString(), ViewState["Id"].ToString(), ViewState["CustNo"].ToString(), SetNo.ToString(), Session["MID"].ToString());
                              if (resultint > 0)
                            {
                                resultint = App.AUTHORIZEDATA(BRCD: Session["BRCD"].ToString(), CUSTNO: ViewState["CustNo"].ToString());

                                if (resultint > 0)
                                {
                                    ViewState["SetNo"] = Convert.ToInt32(SetNo.ToString()).ToString();
                                    btnReceipt.Visible = true;
                                    lblMessage.Text = "Authorise Successfully With SetNo : '" + SetNo.ToString() + "'";
                                    ModalPopup.Show(this.Page);
                                    FL = "Insert";//Dhanya Shetty
                                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Share_Autho _" + SetNo.ToString() + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                    ClearData();
                                    BindGrid();
                                    return;
                                }
                            }
                        }
                    }
                    else if (ddlAppType.SelectedValue.ToString() == "3")
                    {
                        if (ddlPayType.SelectedValue.ToString() == "1")
                        {
                            //Generate Normal set No here
                            SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();

                            //For Cash
                            dt = SA.GetProdCode(Session["BRCD"].ToString());

                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToDouble(txtTotAmount.Text.ToString() == "" ? "0" : txtTotAmount.Text.ToString()) > 0)
                                {
                                    double FinalValue;
                                    FinalValue = Convert.ToDouble(Convert.ToDouble(txtTotShr.Text.ToString()) + Convert.ToDouble(txtEntFee.Text.ToString()) + Convert.ToDouble(txtSavFee.Text.ToString()) + Convert.ToDouble(txtOther1.Text.ToString()) + Convert.ToDouble(txtOther2.Text.ToString()) + Convert.ToDouble(txtOther3.Text.ToString()) + Convert.ToDouble(txtOther4.Text.ToString()) + Convert.ToDouble(txtOther5.Text.ToString()) + Convert.ToDouble(txtMemWelFee.Text.ToString()) + Convert.ToDouble(txtSerChrFee.Text.ToString()));

                                    string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());

                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", cgl, "0",
                                              "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", FinalValue.ToString(), "2", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                              "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                }

                                if (resultint > 0)
                                {
                                    if (Convert.ToDouble(txtTotShr.Text.ToString() == "" ? "0" : txtTotShr.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["BCLASS_GL"].ToString());

                                        if (Session["BRCD"].ToString() == "1")
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["BCLASS_GL"].ToString(), txtShareAccNo.Text.Trim().ToString(),
                                                    "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                    "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        else if (Session["BRCD"].ToString() != "1")
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["BCLASS_GL"].ToString(), "0",
                                                    "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                    "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                    }
                                    if (Convert.ToDouble(txtEntFee.Text.ToString() == "" ? "0" : txtEntFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["ENTRY_GL"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["ENTRY_GL"].ToString(), Session["BRCD"].ToString());
                                         if (RSPara == "Y")
                                         {
                                             string Para = SA.GetParameter();
                                             string MWelAccNo = "";
                                             //if (Para == "HO")
                                             //{
                                             //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                             //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             //else
                                             //{
                                             MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["ENTRY_GL"].ToString(), txtcustno.Text);
                                             //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                                         (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtEntFee.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                         else
                                         {
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                                         "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtEntFee.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                    }
                                    if (Convert.ToDouble(txtSavFee.Text.ToString() == "" ? "0" : txtSavFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["SAVING_GL"].ToString());
                                        //SAccNo = SA.SavAccNo(ViewState["Id"].ToString(), ViewState["CustNo"].ToString());
                                        //string RSPara = SA.GetRSPara(dt.Rows[0]["SAVING_GL"].ToString(), Session["BRCD"].ToString());
                                        //if (RSPara == "Y")
                                        //{
                                        //    string Para = SA.GetParameter();
                                        //    string MWelAccNo = "";
                                        //    //if (Para == "HO")
                                        //    //{
                                        //    //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                        //    //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                        //    //}
                                        //    //else
                                        //    //{
                                        //    MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["SAVING_GL"].ToString(), txtcustno.Text);
                                        //    //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                        //    //}
                                        //    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["SAVING_GL"].ToString(),
                                        //               MWelAccNo, "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSavFee.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                        //               "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        //}
                                        //else
                                        //{
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(),GlCode,dt.Rows[0]["SAVING_GL"].ToString(),
                                                        txtAccNo.Text.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSavFee.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        //}
                                    }
                                    if (Convert.ToDouble(txtOther1.Text.ToString() == "" ? "0" : txtOther1.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL1"].ToString(), Session["BRCD"].ToString());
                                         if (RSPara == "Y")
                                         {
                                             string Para = SA.GetParameter();
                                             string MWelAccNo = "";
                                             //if (Para == "HO")
                                             //{
                                             //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                             //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             //else
                                             //{
                                             MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(),txtcustno.Text);
                                               //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                                        (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther1.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                         else
                                         {
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                                         "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther1.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                    }
                                    if (Convert.ToDouble(txtOther2.Text.ToString() == "" ? "0" : txtOther2.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL2"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL2"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL2"].ToString(), txtcustno.Text);
                                            //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL2"].ToString(),
                                                       (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther2.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                       "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL2"].ToString(),
                                                        "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther2.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtOther3.Text.ToString() == "" ? "0" : txtOther3.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL3"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL3"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL3"].ToString(), txtcustno.Text);
                                            //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL3"].ToString(),
                                                       (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther3.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                       "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL3"].ToString(),
                                                        "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther3.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtOther4.Text.ToString() == "" ? "0" : txtOther4.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL4"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL4"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL4"].ToString(), txtcustno.Text);
                                            //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL4"].ToString(),
                                                        (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther4.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL4"].ToString(),
                                                        "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther4.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtOther5.Text.ToString() == "" ? "0" : txtOther5.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL5"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL5"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL5"].ToString(), txtcustno.Text);
                                            //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL5"].ToString(),
                                                        (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther5.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL5"].ToString(),
                                                        "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther5.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtMemWelFee.Text.ToString() == "" ? "0" : txtMemWelFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString());

                                        string RSPara = SA.GetRSPara(dt.Rows[0]["MemberWel_GL"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["MemberWel_GL"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["MemberWel_GL"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString(),txtcustno.Text);
                                              //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                                                             (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtMemWelFee.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                                             "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                                        "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtMemWelFee.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtSerChrFee.Text.ToString() == "" ? "0" : txtSerChrFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["Service_GL"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["Service_GL"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["Service_GL"].ToString(), txtcustno.Text);
                                            //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["Service_GL"].ToString(),
                                                        (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSerChrFee.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["Service_GL"].ToString(),
                                                        "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSerChrFee.Text.Trim().ToString(), "1", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }

                                    if (Session["BRCD"].ToString() != "1")
                                    {
                                        //Generate Normal set No here
                                        SetNo2 = BD.GetSetNo(Session["EntryDate"].ToString(), "IBTSetNo", Session["BRCD"].ToString()).ToString();

                                        if (resultint > 0)
                                        {
                                            if (Convert.ToDouble(txtTotShr.Text.ToString() == "" ? "0" : txtTotShr.Text.ToString()) > 0)
                                            {
                                                GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["BCLASS_GL"].ToString());

                                                resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["BCLASS_GL"].ToString(), "0",
                                                            "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.Trim().ToString(), "2", "7", "TR", SetNo2.ToString(), "", "", "0", "0", "1003",
                                                            "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");

                                                if (resultint > 0)
                                                {
                                                    //  Credit Parking Account To HO
                                                    dt1 = SA.GetADMSubGl("1");

                                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt1.Rows[0]["ADMGlCode"].ToString(), dt1.Rows[0]["ADMSubGlCode"].ToString(),
                                                            "0", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.Trim().ToString(), "1", "7", "TR", SetNo2.ToString(), "", "", "0", "0", "1003",
                                                            "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                                }

                                                if (resultint > 0)
                                                {
                                                    //  Debit Parking Account To Branch
                                                    dt1 = SA.GetADMSubGl(Session["BRCD"].ToString());

                                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt1.Rows[0]["ADMGlCode"].ToString(), dt1.Rows[0]["ADMSubGlCode"].ToString(),
                                                            "0", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.Trim().ToString(), "2", "7", "TR", SetNo2.ToString(), "", "", "0", "0", "1003",
                                                            "", "1", Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                                }

                                                if (resultint > 0)
                                                {
                                                    GlCode = SA.GetGlCode("1", dt.Rows[0]["BCLASS_GL"].ToString());

                                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["BCLASS_GL"].ToString(), txtShareAccNo.Text.Trim().ToString(),
                                                                "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.Trim().ToString(), "1", "7", "TR", SetNo2.ToString(), "", "", "0", "0", "1003",
                                                                "", "1", Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (ddlPayType.SelectedValue.ToString() == "2")
                        {
                            SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();

                            //For Transfer
                            dt = SA.GetProdCode(Session["BRCD"].ToString());

                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToDouble(txtTotAmount.Text.ToString() == "" ? "0" : txtTotAmount.Text.ToString()) > 0)
                                {
                                    double FinalValue;
                                    FinalValue = Convert.ToDouble(Convert.ToDouble(txtTotShr.Text.ToString()) + Convert.ToDouble(txtEntFee.Text.ToString()) + Convert.ToDouble(txtSavFee.Text.ToString()) + Convert.ToDouble(txtOther1.Text.ToString()) + Convert.ToDouble(txtOther2.Text.ToString()) + Convert.ToDouble(txtOther3.Text.ToString()) + Convert.ToDouble(txtOther4.Text.ToString()) + Convert.ToDouble(txtOther5.Text.ToString()) + Convert.ToDouble(txtMemWelFee.Text.ToString()) + Convert.ToDouble(txtSerChrFee.Text.ToString()));

                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString(), txtProdType1.Text.Trim().ToString(),
                                              TxtAccNo1.Text.Trim().ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", FinalValue.ToString(), "2", "7", "TR", SetNo, TxtChequeNo.Text.ToString() == "" ? "" : TxtChequeNo.Text.ToString(), TxtChequeDate.Text.ToString() == "" ? "" : TxtChequeDate.Text.ToString(), "0", "0", "1003",
                                              "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                }

                                if (resultint > 0)
                                {
                                    if (Convert.ToDouble(txtTotShr.Text.ToString() == "" ? "0" : txtTotShr.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["BCLASS_GL"].ToString());

                                        if (Session["BRCD"].ToString() == "1")
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["BCLASS_GL"].ToString(), txtShareAccNo.Text.Trim().ToString(),
                                                    "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                    "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        else if (Session["BRCD"].ToString() != "1")
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["BCLASS_GL"].ToString(), "0",
                                                    "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                    "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                    }
                                    if (Convert.ToDouble(txtEntFee.Text.ToString() == "" ? "0" : txtEntFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["ENTRY_GL"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["ENTRY_GL"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["ENTRY_GL"].ToString(), txtcustno.Text);
                                            //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                                       (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtEntFee.Text.ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                       "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                                        "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtEntFee.Text.ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtSavFee.Text.ToString() == "" ? "0" : txtSavFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["SAVING_GL"].ToString());
                                        //SAccNo = SA.SavAccNo(ViewState["Id"].ToString(), ViewState["CustNo"].ToString());
                                        //string RSPara = SA.GetRSPara(dt.Rows[0]["SAVING_GL"].ToString(), Session["BRCD"].ToString());
                                        // if (RSPara == "Y")
                                        // {
                                        //     string Para = SA.GetParameter();
                                        //     string MWelAccNo = "";
                                        //     //if (Para == "HO")
                                        //     //{
                                        //     //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                        //     //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                        //     //}
                                        //     //else
                                        //     //{
                                        //     MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["SAVING_GL"].ToString(), txtcustno.Text);
                                        //     //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                        //     //}
                                        //     resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(),GlCode,dt.Rows[0]["SAVING_GL"].ToString(),
                                        //                MWelAccNo, "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSavFee.Text.ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                        //                "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        // }
                                        // else
                                        // {
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(),GlCode,dt.Rows[0]["SAVING_GL"].ToString(),
                                                         txtAccNo.Text.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSavFee.Text.ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                         //}
                                    }
                                    if (Convert.ToDouble(txtOther1.Text.ToString() == "" ? "0" : txtOther1.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString());

                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL1"].ToString(), Session["BRCD"].ToString());
                                          if (RSPara == "Y")
                                          {
                                              string Para = SA.GetParameter();
                                              string MWelAccNo = "";
                                              //if (Para == "HO")
                                              //{
                                              //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                              //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                              //}
                                              //else
                                              //{
                                              MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(),txtcustno.Text);
                                                 // int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                              //}
                                              resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                                         (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther1.Text.ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                          }
                                          else
                                          {
                                              resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                                          "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther1.Text.ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                          "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                          }
                                    }
                                    if (Convert.ToDouble(txtOther2.Text.ToString() == "" ? "0" : txtOther2.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL2"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL2"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL2"].ToString(), txtcustno.Text);
                                            //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL2"].ToString(),
                                                       (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther2.Text.ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                       "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL2"].ToString(),
                                                        "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther2.Text.ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtOther3.Text.ToString() == "" ? "0" : txtOther3.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL3"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL3"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL3"].ToString(), txtcustno.Text);
                                            //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL3"].ToString(),
                                                       (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther3.Text.Trim().ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                       "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL3"].ToString(),
                                                        "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther3.Text.Trim().ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtOther4.Text.ToString() == "" ? "0" : txtOther4.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL4"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL4"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL4"].ToString(), txtcustno.Text);
                                            //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL4"].ToString(),
                                                        (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther4.Text.Trim().ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL4"].ToString(),
                                                        "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther4.Text.Trim().ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtOther5.Text.ToString() == "" ? "0" : txtOther5.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL5"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL5"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL5"].ToString(), txtcustno.Text);
                                            //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL5"].ToString(),
                                                        (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther5.Text.Trim().ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL5"].ToString(),
                                                        "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther5.Text.Trim().ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtMemWelFee.Text.ToString() == "" ? "0" : txtMemWelFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString());

                                        string RSPara = SA.GetRSPara(dt.Rows[0]["MemberWel_GL"].ToString(), Session["BRCD"].ToString());
                                         if (RSPara == "Y")
                                         {
                                             string Para = SA.GetParameter();
                                             string MWelAccNo = "";
                                             //if (Para == "HO")
                                             //{
                                             //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["MemberWel_GL"].ToString());
                                             //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["MemberWel_GL"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             //else
                                             //{
                                             MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString(),txtcustno.Text);
                                                 //int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                             //}
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                                                                 (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtMemWelFee.Text.Trim().ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                                                  "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                         else
                                         {

                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                                         "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtMemWelFee.Text.Trim().ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                         }
                                    }
                                    if (Convert.ToDouble(txtSerChrFee.Text.ToString() == "" ? "0" : txtSerChrFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["Service_GL"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["Service_GL"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["Service_GL"].ToString(), txtcustno.Text);
                                            //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["Service_GL"].ToString(),
                                                       (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSerChrFee.Text.Trim().ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                       "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["Service_GL"].ToString(),
                                                        "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSerChrFee.Text.Trim().ToString(), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }

                                    if (Session["BRCD"].ToString() != "1")
                                    {
                                        //Generate Normal set No here
                                        SetNo2 = BD.GetSetNo(Session["EntryDate"].ToString(), "IBTSetNo", Session["BRCD"].ToString()).ToString();

                                        if (resultint > 0)
                                        {
                                            if (Convert.ToDouble(txtTotShr.Text.ToString() == "" ? "0" : txtTotShr.Text.ToString()) > 0)
                                            {
                                                GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["BCLASS_GL"].ToString());

                                                resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["BCLASS_GL"].ToString(), "0",
                                                            "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.Trim().ToString(), "2", "7", "TR", SetNo2.ToString(), "", "", "0", "0", "1003",
                                                            "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");

                                                if (resultint > 0)
                                                {
                                                    //  Credit Parking Account To HO
                                                    dt1 = SA.GetADMSubGl("1");

                                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt1.Rows[0]["ADMGlCode"].ToString(), dt1.Rows[0]["ADMSubGlCode"].ToString(),
                                                            "0", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.Trim().ToString(), "1", "7", "TR", SetNo2.ToString(), "", "", "0", "0", "1003",
                                                            "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                                }

                                                if (resultint > 0)
                                                {
                                                    //  Debit Parking Account To Branch
                                                    dt1 = SA.GetADMSubGl(Session["BRCD"].ToString());

                                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt1.Rows[0]["ADMGlCode"].ToString(), dt1.Rows[0]["ADMSubGlCode"].ToString(),
                                                            "0", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.Trim().ToString(), "2", "7", "TR", SetNo2.ToString(), "", "", "0", "0", "1003",
                                                            "", "1", Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                                }

                                                if (resultint > 0)
                                                {
                                                    GlCode = SA.GetGlCode("1", dt.Rows[0]["BCLASS_GL"].ToString());

                                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["BCLASS_GL"].ToString(), txtShareAccNo.Text.Trim().ToString(),
                                                                "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.Trim().ToString(), "1", "7", "TR", SetNo2.ToString(), "", "", "0", "0", "1003",
                                                                "", "1", Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (ddlPayType.SelectedValue.ToString() == "4")
                        {
                            SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();

                            //For Cheque
                            dt = SA.GetProdCode(Session["BRCD"].ToString());

                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToDouble(txtTotAmount.Text.ToString() == "" ? "0" : txtTotAmount.Text.ToString()) > 0)
                                {
                                    double FinalValue;
                                    FinalValue = Convert.ToDouble(Convert.ToDouble(txtTotShr.Text.ToString()) + Convert.ToDouble(txtEntFee.Text.ToString()) + Convert.ToDouble(txtSavFee.Text.ToString()) + Convert.ToDouble(txtOther1.Text.ToString()) + Convert.ToDouble(txtOther2.Text.ToString()) + Convert.ToDouble(txtOther3.Text.ToString()) + Convert.ToDouble(txtOther4.Text.ToString()) + Convert.ToDouble(txtOther5.Text.ToString()) + Convert.ToDouble(txtMemWelFee.Text.ToString()) + Convert.ToDouble(txtSerChrFee.Text.ToString()));

                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString(), txtProdType1.Text.Trim().ToString(),
                                              TxtAccNo1.Text.Trim().ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", FinalValue.ToString(), "2", "5", "TR", SetNo, TxtChequeNo.Text.ToString() == "" ? "" : TxtChequeNo.Text.ToString(), TxtChequeDate.Text.ToString() == "" ? "" : TxtChequeDate.Text.ToString(), "0", "0", "1003",
                                              "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                }

                                if (resultint > 0)
                                {
                                    if (Convert.ToDouble(txtTotShr.Text.ToString() == "" ? "0" : txtTotShr.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["BCLASS_GL"].ToString());

                                        if (Session["BRCD"].ToString() == "1")
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["BCLASS_GL"].ToString(), txtShareAccNo.Text.Trim().ToString(),
                                                        "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        else if (Session["BRCD"].ToString() != "1")
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["BCLASS_GL"].ToString(), "0",
                                                    "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                    "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                    }
                                    if (Convert.ToDouble(txtEntFee.Text.ToString() == "" ? "0" : txtEntFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["ENTRY_GL"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["ENTRY_GL"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["ENTRY_GL"].ToString(), txtcustno.Text);
                                            //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                                      (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtEntFee.Text.ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                      "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                                        "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtEntFee.Text.ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtSavFee.Text.ToString() == "" ? "0" : txtSavFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["SAVING_GL"].ToString());
                                        //SAccNo = SA.SavAccNo(ViewState["Id"].ToString(), ViewState["CustNo"].ToString());
                                        //string RSPara = SA.GetRSPara(dt.Rows[0]["SAVING_GL"].ToString(), Session["BRCD"].ToString());
                                        // if (RSPara == "Y")
                                        // {
                                        //     string Para = SA.GetParameter();
                                        //     string MWelAccNo = "";
                                        //     //if (Para == "HO")
                                        //     //{
                                        //     //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                        //     //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                        //     //}
                                        //     //else
                                        //     //{
                                        //     MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["SAVING_GL"].ToString(), txtcustno.Text);
                                        //     //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                        //     //}
                                        //     resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(),GlCode,dt.Rows[0]["SAVING_GL"].ToString(),
                                        //                 MWelAccNo, "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSavFee.Text.ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                        //                 "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        // }
                                        // else
                                        // {
                                             resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(),GlCode,dt.Rows[0]["SAVING_GL"].ToString(),
                                                         txtAccNo.Text.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSavFee.Text.ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                         //}
                                    }
                                    if (Convert.ToDouble(txtOther1.Text.ToString() == "" ? "0" : txtOther1.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString());

                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL1"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(),txtcustno.Text);
                                                //int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                                       (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther1.Text.ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                       "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                                        "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther1.Text.ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtOther2.Text.ToString() == "" ? "0" : txtOther2.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL2"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL2"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL2"].ToString(), txtcustno.Text);
                                            //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL2"].ToString(),
                                                        (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther2.Text.ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL2"].ToString(),
                                                        "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther2.Text.ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtOther3.Text.ToString() == "" ? "0" : txtOther3.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL3"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL3"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL3"].ToString(), txtcustno.Text);
                                            //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL3"].ToString(),
                                                       (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther3.Text.Trim().ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                       "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL3"].ToString(),
                                                        "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther3.Text.Trim().ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtOther4.Text.ToString() == "" ? "0" : txtOther4.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL4"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL4"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL4"].ToString(), txtcustno.Text);
                                            //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL4"].ToString(),
                                                         (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther4.Text.Trim().ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL4"].ToString(),
                                                        "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther4.Text.Trim().ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtOther5.Text.ToString() == "" ? "0" : txtOther5.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL5"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["OTHERS_GL5"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL5"].ToString(), txtcustno.Text);
                                            //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL5"].ToString(),
                                                        (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther5.Text.Trim().ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL5"].ToString(),
                                                        "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtOther5.Text.Trim().ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtMemWelFee.Text.ToString() == "" ? "0" : txtMemWelFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString());

                                        string RSPara = SA.GetRSPara(dt.Rows[0]["MemberWel_GL"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["MemberWel_GL"].ToString());
                                            //    int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString(),txtcustno.Text);
                                               // int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                                             (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtMemWelFee.Text.Trim().ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                             "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                                        "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtMemWelFee.Text.Trim().ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }
                                    if (Convert.ToDouble(txtSerChrFee.Text.ToString() == "" ? "0" : txtSerChrFee.Text.ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["Service_GL"].ToString());
                                        string RSPara = SA.GetRSPara(dt.Rows[0]["Service_GL"].ToString(), Session["BRCD"].ToString());
                                        if (RSPara == "Y")
                                        {
                                            string Para = SA.GetParameter();
                                            string MWelAccNo = "";
                                            //if (Para == "HO")
                                            //{
                                            //    MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                            //    int res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            //else
                                            //{
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["Service_GL"].ToString(), txtcustno.Text);
                                            //  int res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, txtcustno.Text.Trim().ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            //}
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["Service_GL"].ToString(),
                                                        (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSerChrFee.Text.Trim().ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                        else
                                        {
                                            resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["Service_GL"].ToString(),
                                                        "", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtSerChrFee.Text.Trim().ToString(), "1", "6", "TR", SetNo, "", "", "0", "0", "1003",
                                                        "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "0", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                        }
                                    }

                                    if (Session["BRCD"].ToString() != "1")
                                    {
                                        //Generate Normal set No here
                                        SetNo2 = BD.GetSetNo(Session["EntryDate"].ToString(), "IBTSetNo", Session["BRCD"].ToString()).ToString();

                                        if (resultint > 0)
                                        {
                                            if (Convert.ToDouble(txtTotShr.Text.ToString() == "" ? "0" : txtTotShr.Text.ToString()) > 0)
                                            {
                                                GlCode = SA.GetGlCode(Session["BRCD"].ToString(), dt.Rows[0]["BCLASS_GL"].ToString());

                                                resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["BCLASS_GL"].ToString(), "0",
                                                            "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.Trim().ToString(), "2", "7", "TR", SetNo2.ToString(), "", "", "0", "0", "1003",
                                                            "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");

                                                if (resultint > 0)
                                                {
                                                    //  Credit Parking Account To HO
                                                    dt1 = SA.GetADMSubGl("1");

                                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt1.Rows[0]["ADMGlCode"].ToString(), dt1.Rows[0]["ADMSubGlCode"].ToString(),
                                                            "0", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.Trim().ToString(), "1", "7", "TR", SetNo2.ToString(), "", "", "0", "0", "1003",
                                                            "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                                }

                                                if (resultint > 0)
                                                {
                                                    //  Debit Parking Account To Branch
                                                    dt1 = SA.GetADMSubGl(Session["BRCD"].ToString());

                                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt1.Rows[0]["ADMGlCode"].ToString(), dt1.Rows[0]["ADMSubGlCode"].ToString(),
                                                            "0", "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.Trim().ToString(), "2", "7", "TR", SetNo2.ToString(), "", "", "0", "0", "1003",
                                                            "", "1", Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                                }

                                                if (resultint > 0)
                                                {
                                                    GlCode = SA.GetGlCode("1", dt.Rows[0]["BCLASS_GL"].ToString());

                                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["BCLASS_GL"].ToString(), txtShareAccNo.Text.Trim().ToString(),
                                                                "BClass-ShrAppNo " + ViewState["AppNo"].ToString() + "", "BClass-ShrCustNo " + ViewState["CustNo"].ToString() + "", txtTotShr.Text.Trim().ToString(), "1", "7", "TR", SetNo2.ToString(), "", "", "0", "0", "1003",
                                                                "", "1", Mid.ToString(), "0", Session["MID"].ToString(), "ShareApp", txtcustno.Text.Trim().ToString(), txtcusname.Text.ToString(), "0", "0");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (resultint > 0)
                        {
                            resultint = SA.AuthoriseData(Session["BRCD"].ToString(), ViewState["Id"].ToString(), ViewState["CustNo"].ToString(), SetNo.ToString(), Session["MID"].ToString());
                            if (resultint > 0)
                            {
                                resultint = App.AUTHORIZEDATA(BRCD: Session["BRCD"].ToString(), CUSTNO: ViewState["CustNo"].ToString());

                                if (resultint > 0)
                                {
                                    if (Session["BRCD"].ToString() == "1")
                                    {
                                        lblMessage.Text = "Authorise Successfully With SetNo : '" + SetNo.ToString() + "'";
                                        FL = "Insert";//Dhanya Shetty
                                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Share_Autho _" + SetNo.ToString() + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                    }
                                    else if (Session["BRCD"].ToString() != "1")
                                    {
                                        lblMessage.Text = "Authorise Successfully With SetNo : '" + SetNo.ToString() + "' And '" + SetNo2.ToString() + "'";
                                        FL = "Insert";//Dhanya Shetty
                                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Share_Autho _" + SetNo.ToString() + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                    }
                                    ViewState["SetNo"] = Convert.ToInt32(SetNo.ToString()).ToString();
                                    btnReceipt.Visible = true;
                                    ModalPopup.Show(this.Page);
                                    ClearData();
                                    BindGrid();
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblMessage.Text = "Application already authorise...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            //}
            //else
            //{
            //    lblMessage.Text = "Maker Not Authorise...!!";
            //    ModalPopup.Show(this.Page);
            //    return;
            //}
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ApplBatchAllocate(string BrCode, string CustNo, string AppNo, string AppStatus)
    {
        try
        {
            string SetNo = "", GlCode = "";
            string Para = "", BrPara = ""; ;
            string MWelAccNo = "";
            TotalAmount = 0;

            dt = new DataTable();
            dt = AAS.GetSubGlCode(BrCode.ToString());

            if (dt.Rows.Count > 0)
            {
                ShrData = AAS.GetApplicationData(BrCode.ToString(), CustNo.ToString(), AppNo.ToString());
                string SharePara = SA.GetParameter();
                if (ShrData.Rows.Count > 0 && ShrData.Rows[0]["ApplStatus"].ToString() == "1")
                {
                    if (ShrData.Rows[0]["MemberNo"].ToString() != "" && ShrData.Rows[0]["MemberNo"].ToString() != null && ShrData.Rows[0]["MemberNo"].ToString() != "0" && ShrData.Rows[0]["ApplType"].ToString() == "2")
                    {
                        MemberNo = Convert.ToInt32(AAS.GetMemNo("1", ShrData.Rows[0]["MemberNo"].ToString())).ToString();
                        CertNo = Convert.ToInt32(AAS.GetCertNo("1")).ToString();
                        FromNo = Convert.ToInt32(AAS.GetFromNo("1")).ToString();
                        ToNo = Convert.ToInt32(Convert.ToInt32(FromNo.ToString()) + (Convert.ToInt32(ShrData.Rows[0]["NoOfSHR"].ToString()) - 1)).ToString();
                        MeetDate = Session["EntryDate"].ToString();
                        BoardRegNo = Convert.ToInt32(AAS.GetBoardRegNo("1")).ToString();
                        SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "IBTSetNo", "1").ToString();

                        if (SetNumber == true)
                        {
                            FSet = SetNo;
                            SetNumber = false;
                        }

                        string CS = accop.Getcustname(CustNo.ToString());

                        GlCode = SA.GetGlCode(BrCode.ToString(), dt.Rows[0]["SHARES_GL"].ToString());
                        resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["SHARES_GL"].ToString(),
                                    ShrData.Rows[0]["ShareAccNo"].ToString(), "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["TotShrValue"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                    "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                        TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["TotShrValue"].ToString());

                        if ("1" != BrCode.ToString())
                        {
                            //if (resultint > 0)
                            //{
                                if (Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()) > 0)
                                {
                                    EntFee = "Y";
                                    GlCode = SA.GetGlCode(BrCode.ToString(), dt.Rows[0]["ENTRY_GL"].ToString());
                                    string RSPara = SA.GetRSPara1(dt.Rows[0]["ENTRY_GL"].ToString());
                                    BrPara = SA.GetHoPara(dt.Rows[0]["ENTRY_GL"].ToString());
                                    if (RSPara == "Y")
                                    {
                                        Para = SA.GetRSPara(dt.Rows[0]["ENTRY_GL"].ToString(), Session["BRCD"].ToString());
                                        MWelAccNo = "";
                                        if (Para == "Y")
                                        {
                                            //if (Para == "HO")
                                            //    MWelAccNo = SA.GetAccnoShr("1", dt.Rows[0]["OTHERS_GL1"].ToString(), CustNo.ToString());
                                            //else
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["ENTRY_GL"].ToString(), CustNo.ToString());
                                        }
                                        if (SharePara == "HO" && BrPara=="Y")
                                       {
                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                        //              (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + 
                                        //              CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //              "","1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                                      (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                      "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                            TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString());
                                        }
                                        //else
                                        //{
                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                        //               (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //               "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        //}
                                    }
                                    else
                                    {
                                        if (SharePara == "HO" && BrPara == "Y")
                                        {
                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                        //               "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", 
                                        //               Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //               "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                                 "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                 "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                            TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString());
                                        }
                                        //else
                                        //{
                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                        //          "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //          "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        //}
                                    }
                                }
                            //}

                            //if (resultint > 0)
                            //{
                                if (Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()) > 0)
                                {
                                    WelFee = "Y";
                                    GlCode = SA.GetGlCode(BrCode.ToString(), dt.Rows[0]["OTHERS_GL1"].ToString());
                                    string RSPara = SA.GetRSPara1(dt.Rows[0]["OTHERS_GL1"].ToString());
                                    BrPara = SA.GetHoPara(dt.Rows[0]["OTHERS_GL1"].ToString());
                                    if (RSPara == "Y")
                                    {
                                        //string Para = SA.GetParameter();
                                         MWelAccNo = "";
                                         Para = SA.GetRSPara(dt.Rows[0]["OTHERS_GL1"].ToString(), Session["BRCD"].ToString());
                                        if (Para == "Y")
                                        {
                                            //if (Para == "HO")
                                            //    MWelAccNo = SA.GetAccnoShr("1", dt.Rows[0]["OTHERS_GL1"].ToString(), CustNo.ToString());
                                            //else
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), CustNo.ToString());
                                        }
                                         if (SharePara == "HO" && BrPara == "Y")
                                         {
                                         //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                         //               (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + 
                                         //               CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                         //               "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                             resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                                  (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                  "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                             TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString());
                                        }
                                         //else
                                         //{
                                         //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                         //            (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " +
                                         //            CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                         //            "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                         //}
                                    }
                                    else
                                    {
                                        if (SharePara == "HO" && BrPara == "Y")
                                        {
                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                        //                "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", 
                                        //                Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //                "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                                  "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                  "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                            TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString());
                                        }
                                        //else
                                        //{
                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                        //           "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //           "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        //}
                                    }
                                }
                            //}

                            //if (resultint > 0)
                            //{
                                if (Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()) > 0)
                                {
                                    WelFeeLoan = "Y";
                                    GlCode = SA.GetGlCode(BrCode.ToString(), dt.Rows[0]["MemberWel_GL"].ToString());
                                    string RSPara = SA.GetRSPara1(dt.Rows[0]["MemberWel_GL"].ToString());

                                    BrPara = SA.GetHoPara(dt.Rows[0]["MemberWel_GL"].ToString());
                                    if (RSPara == "Y")
                                    {
                                         Para = SA.GetRSPara(dt.Rows[0]["MemberWel_GL"].ToString(), Session["BRCD"].ToString());
                                         MWelAccNo = "";
                                        if (Para == "Y")
                                        {
                                            //if (Para == "HO")
                                            //    MWelAccNo = SA.GetAccnoShr("1", dt.Rows[0]["MemberWel_GL"].ToString(), CustNo.ToString());
                                            //else
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString(), CustNo.ToString());
                                        }
                                        if(SharePara=="HO" && BrPara=="Y")
                                        {
                                        //resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                        //(MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //         "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                             resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                        (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                 "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                             TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString());
                                        }
                                        //else
                                        //{
                                        //     resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                        //(MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //         "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        //}
                                    }
                                    else
                                    {
                                        if (SharePara == "HO" && BrPara == "Y")
                                        {
                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                        //               "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //               "","1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                        //     "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //     "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        //}
                                        //else
                                        //{
                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                                  "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                  "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                            TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString());
                                        }
                                    }
                                }
                            //}

                            //if (resultint > 0)
                            //{
                                if (Convert.ToDouble(TotalAmount) > 0)
                                {
                                    //  Credit Parking Account To HO
                                    dt = AAS.GetADMSubGl("1");
                                    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["ADMGlCode"].ToString(), dt.Rows[0]["ADMSubGlCode"].ToString(),
                                                "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(TotalAmount.ToString()), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                                "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                }
                            //}

                            //if (resultint > 0)
                            //{
                                if (Convert.ToDouble(TotalAmount) > 0)
                                {
                                    //  Debit Parking Account To Branch
                                    dt = AAS.GetADMSubGl(BrCode.ToString());
                                    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["ADMGlCode"].ToString(), dt.Rows[0]["ADMSubGlCode"].ToString(),
                                                    "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(TotalAmount.ToString()), "2", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                                    "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                }
                            }
                        //}

                        //if (resultint > 0)
                        //{
                            dt = new DataTable();
                            dt = AAS.GetSubGlCode("1");

                            GlCode = SA.GetGlCode("1", "4");
                        if(SharePara=="HO")
                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "4", "4", MemberNo.ToString(),
                                        "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["TotShrValue"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                        else
                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "4", "4", MemberNo.ToString(),
                                       "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["TotShrValue"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                       "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                        //}

                        if ("1" != BrCode.ToString())
                        {
                            //if (resultint > 0)
                            //{
                            if (Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()) > 0)
                            {
                                GlCode = SA.GetGlCode("1", dt.Rows[0]["ENTRY_GL"].ToString());
                                string RSPara = SA.GetRSPara1(dt.Rows[0]["ENTRY_GL"].ToString());
                                BrPara = SA.GetHoPara(dt.Rows[0]["ENTRY_GL"].ToString());
                                if (RSPara == "Y")
                                {
                                    Para = SA.GetRSPara(dt.Rows[0]["ENTRY_GL"].ToString(), Session["BRCD"].ToString());
                                    MWelAccNo = "";
                                    if (Para == "Y")
                                    {
                                        Para = SA.GetParameter();
                                        if (Para == "HO")
                                        {
                                            MWelAccNo = SA.GetAccno("1", dt.Rows[0]["ENTRY_GL"].ToString(), CustNo.ToString());
                                            int res = 0;
                                            if (MWelAccNo.ToString() != null && MWelAccNo.ToString() != "")
                                                res = SA.InsertAvsACCSHR("1", dt.Rows[0]["ENTRY_GL"].ToString(), GlCode, CustNo.ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                        }
                                        else
                                        {
                                            MWelAccNo = SA.GetAccno(Session["BRCD"].ToString(), dt.Rows[0]["ENTRY_GL"].ToString(), CustNo.ToString());
                                            int res = 0;
                                            if (MWelAccNo.ToString() != null && MWelAccNo.ToString() != "")
                                                res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["ENTRY_GL"].ToString(), GlCode, CustNo.ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                        }
                                    }

                                    if (SharePara == "HO" && BrPara == "Y")
                                    {
                                    //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(), (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(),
                                    //              "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                    //              "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                    //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(), (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(),
                                    //                                                 "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                    //                                                 "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                    //}
                                    //else
                                    //{
                                        resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(), (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(),
                                                                                     "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                                                     "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                    }
                                }
                                else
                                {

                                    if (SharePara == "HO" && BrPara == "Y")
                                    {
                                    //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(), "0",
                                    //               "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                    //               "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                    //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(), "0",
                                    //                                                 "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                    //                                                 "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                    //}
                                    //else
                                    //{
                                        resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(), "0",
                                              "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                              "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                    }
                                }
                            }
                            //}

                            //if (resultint > 0)
                            //{
                                if (Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()) > 0)
                                {
                                    GlCode = SA.GetGlCode("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                    string RSPara = SA.GetRSPara1(dt.Rows[0]["OTHERS_GL1"].ToString());
                                    BrPara = SA.GetHoPara(dt.Rows[0]["OTHERS_GL1"].ToString());
                                    if (RSPara == "Y")
                                    {
                                         Para = SA.GetRSPara(dt.Rows[0]["OTHERS_GL1"].ToString(), Session["BRCD"].ToString());
                                         MWelAccNo = "";
                                        if (Para == "Y")
                                        {
                                            Para = SA.GetParameter();
                                            if (Para == "HO")
                                            {
                                                MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString(),CustNo.ToString());
                                                int res = 0;
                                                if (MWelAccNo.ToString() != null && MWelAccNo.ToString() != "")
                                                    res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, CustNo.ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }
                                            else
                                            {
                                                MWelAccNo = SA.GetAccno(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), CustNo.ToString());
                                                int res = 0;
                                                if (MWelAccNo.ToString() != "" && MWelAccNo.ToString() != "")
                                                    res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, CustNo.ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }
                                        }
                                        if (SharePara == "HO" && BrPara == "Y")
                                        {
                                            //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(), (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(),
                                            //                "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                            //                "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                            //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(), (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(),
                                            //               "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                            //               "",BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                            //}
                                            // else
                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(), (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(),
                                                       "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                       "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        }
                                    }
                                    else
                                    {
                                        if (SharePara == "HO" && BrPara == "Y")
                                        {
                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(), "0",
                                        //                "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //                "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(), "0",
                                        //          "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //          "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        //}
                                        //else
                                        //{
                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(), "0",
                                                   "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                   "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        }
                                    }
                                }
                            //}

                            //if (resultint > 0)
                            //{
                                if (Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()) > 0)
                                {
                                    GlCode = SA.GetGlCode("1", dt.Rows[0]["MemberWel_GL"].ToString());
                                    string RSPara = SA.GetRSPara1(dt.Rows[0]["MemberWel_GL"].ToString());
                                    BrPara = SA.GetHoPara(dt.Rows[0]["MemberWel_GL"].ToString());
                                    if (RSPara == "Y")
                                    {
                                        Para = SA.GetRSPara(dt.Rows[0]["MemberWel_GL"].ToString(), Session["BRCD"].ToString());
                                        MWelAccNo = "";
                                        if (Para == "Y")
                                        {
                                            Para = SA.GetParameter();
                                            if (Para == "HO")
                                            {
                                                MWelAccNo = SA.GetAccno("1", dt.Rows[0]["MemberWel_GL"].ToString(), CustNo.ToString());
                                                int res = 0;
                                                if (MWelAccNo.ToString() != "" && MWelAccNo.ToString() != null)
                                                    res = SA.InsertAvsACCSHR("1", dt.Rows[0]["MemberWel_GL"].ToString(), GlCode, CustNo.ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }
                                            else
                                            {
                                                MWelAccNo = SA.GetAccno(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString(), CustNo.ToString());
                                                int res = 0;
                                                if (MWelAccNo.ToString() != "" && MWelAccNo.ToString() != null)
                                                    res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString(), GlCode, CustNo.ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }
                                        }
                                        if (SharePara == "HO" && BrPara == "Y")
                                        //{
                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(), (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(),
                                        //              "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //              "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(), (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(),
                                        //         "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //         "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        //}
                                        //else
                                        //{
                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(), (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(),
                                                 "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                 "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                       // }
                                    }
                                    else
                                    {
                                        if (SharePara == "HO" && BrPara == "Y")
                                        {
                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(), "0",
                                        //                "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //                "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(), "0",
                                        //          "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //          "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        //}
                                        //else
                                        //{
                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(), "0",
                                                   "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                   "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        }
                                    }
                                }
                            }
                        //}

                        //if (resultint > 0)
                        //{
                            if (ShrData.Rows[0]["ApplType"].ToString() == "2")
                            {
                                // Insert Record Into SharesInfo Table if application is Additional Shares
                                resultint = AAS.InsertShrInfo("1", CustNo.ToString(), MeetDate.ToString(), BoardRegNo.ToString(), MemberNo.ToString(), FromNo.ToString(), ToNo.ToString(), ShrData.Rows[0]["NoOfSHR"].ToString(), ShrData.Rows[0]["SHRValue"].ToString(), ShrData.Rows[0]["TotShrValue"].ToString(), CertNo.ToString(), EntFee, WelFee, WelFeeLoan, AppNo.ToString(), SetNo, BrCode.ToString(), "1003", Session["MID"].ToString(), Session["EntryDate"].ToString(), "Application " + AppNo.ToString() + " allocated");
                            }

                            if (resultint > 0)
                            {
                                resultint = AAS.UpDateAppStatus("1", CustNo.ToString(), CertNo.ToString(), MemberNo.ToString(), AppStatus.ToString(), AppNo.ToString(), BrCode.ToString(), Session["EntryDate"].ToString(), "AD", SetNo);

                                if (resultint > 0)
                                {
                                    BindUnallotedGrid();
                                    lblMessage.Text = "Batch Successfully Alloted With Set No : '" + FSet + "' To '" + SetNo + "' ...!!";
                                    FL = "Insert";//Dhanya Shetty
                                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Share_create _" + FSet + "_" + SetNo + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                    ModalPopup.Show(this.Page);
                                    return;
                                }
                            }
                        //}
                    }
                    else if (ShrData.Rows[0]["ApplType"].ToString() == "1")
                    {
                        MemberNo = Convert.ToInt32(AAS.GetMemNo("1", ShrData.Rows[0]["MemberNo"].ToString())).ToString();
                        CertNo = Convert.ToInt32(AAS.GetCertNo("1")).ToString();
                        FromNo = Convert.ToInt32(AAS.GetFromNo("1")).ToString();
                        ToNo = Convert.ToInt32(Convert.ToInt32(FromNo.ToString()) + (Convert.ToInt32(ShrData.Rows[0]["NoOfSHR"].ToString()) - 1)).ToString();
                        MeetDate = Session["EntryDate"].ToString();
                        BoardRegNo = Convert.ToInt32(AAS.GetBoardRegNo("1")).ToString();
                        SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "IBTSetNo", "1").ToString();

                        if (SetNumber == true)
                        {
                            FSet = SetNo;
                            SetNumber = false;
                        }

                        string CS = accop.Getcustname(CustNo.ToString());

                        GlCode = SA.GetGlCode(BrCode.ToString(), dt.Rows[0]["SHARES_GL"].ToString());
                        resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["SHARES_GL"].ToString(),
                                    ShrData.Rows[0]["ShareAccNo"].ToString(), "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["TotShrValue"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                    "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                        TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["TotShrValue"].ToString());

                        if ("1" != BrCode.ToString())
                        {
                            //if (resultint > 0)
                            //{
                            //    if (Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()) > 0)
                                {
                                    EntFee = "Y";
                                    GlCode = SA.GetGlCode(BrCode.ToString(), dt.Rows[0]["ENTRY_GL"].ToString());
                                    string RSPara = SA.GetRSPara1(dt.Rows[0]["ENTRY_GL"].ToString());
                                    BrPara = SA.GetHoPara(dt.Rows[0]["ENTRY_GL"].ToString());
                                    if (RSPara == "Y")
                                    {
                                        Para = SA.GetRSPara(dt.Rows[0]["ENTRY_GL"].ToString(), Session["BRCD"].ToString());
                                        MWelAccNo = "";
                                        if (Para == "Y")
                                        {
                                            //if (Para == "HO")
                                            //    MWelAccNo = SA.GetAccnoShr("1", dt.Rows[0]["MemberWel_GL"].ToString(), CustNo.ToString());
                                            //else
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["ENTRY_GL"].ToString(), CustNo.ToString());
                                        }
                                        if (SharePara == "HO" && BrPara == "Y")
                                        {
                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                        //                 (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //                 "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                        //            (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //            "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        //}
                                        //else
                                        //{
                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                                    (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                    "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                            TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        if (SharePara == "HO" && BrPara == "Y")
                                        {
                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                        //               "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //               "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                        //           "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //           "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        //}
                                        //else
                                        //{
                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(),
                                                   "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                   "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                            TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString());
                                        }
                                    }
                                }
                            //}

                            //if (resultint > 0)
                            //{
                                if (Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()) > 0)
                                {
                                    WelFee = "Y";
                                    GlCode = SA.GetGlCode(BrCode.ToString(), dt.Rows[0]["OTHERS_GL1"].ToString());
                                    string RSPara = SA.GetRSPara1(dt.Rows[0]["OTHERS_GL1"].ToString());
                                    BrPara = SA.GetHoPara(dt.Rows[0]["OTHERS_GL1"].ToString());
                                    if (RSPara == "Y")
                                    {
                                        Para = SA.GetRSPara(dt.Rows[0]["OTHERS_GL1"].ToString(), Session["BRCD"].ToString());
                                        MWelAccNo = "";
                                        if (Para == "Y")
                                        {
                                            //if (Para == "HO")
                                            //    MWelAccNo = SA.GetAccnoShr("1", dt.Rows[0]["OTHERS_GL1"].ToString(), CustNo.ToString());
                                            //else
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), CustNo.ToString());
                                        }
                                        if (SharePara == "HO" && BrPara == "Y")
                                        {
                                            //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                            //               (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                            //               "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                            //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                            //           (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                            //           "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                            //}
                                            //else
                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                                   (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                   "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                            TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        if (SharePara == "HO" && BrPara == "Y")
                                        {
                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                        //                "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //                "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                        //           "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //           "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        //}
                                        //else
                                        //{
                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(),
                                                   "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                   "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                            TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString());
                                        }
                                    }
                                }
                            //}

                            //if (resultint > 0) //ashok
                            //{
                                if (Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()) > 0)
                                {
                                    WelFeeLoan = "Y";
                                    GlCode = SA.GetGlCode(BrCode.ToString(), dt.Rows[0]["MemberWel_GL"].ToString());
                                    string RSPara = SA.GetRSPara1(dt.Rows[0]["MemberWel_GL"].ToString());
                                    BrPara = SA.GetHoPara(dt.Rows[0]["MemberWel_GL"].ToString());
                                    if (RSPara == "Y")
                                    {
                                        Para = SA.GetRSPara(dt.Rows[0]["MemberWel_GL"].ToString(), Session["BRCD"].ToString());
                                        MWelAccNo = "";
                                        if (Para == "Y")
                                        {
                                            //if (Para == "HO")
                                            //    MWelAccNo = SA.GetAccnoShr("1", dt.Rows[0]["MemberWel_GL"].ToString(), CustNo.ToString());
                                            //else
                                            MWelAccNo = SA.GetAccnoShr(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString(), CustNo.ToString());
                                        }
                                        if (SharePara == "HO" && BrPara == "Y")
                                        {
                                            //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                            //             (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                            //             "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                            //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                            //        (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                            //        "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                            //}
                                            //else
                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                                (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(), "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                            TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        if (SharePara == "HO" && BrPara == "Y")
                                        {
                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                        //               "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //               "","1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                        //          "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //          "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        //}
                                        //else
                                        //{
                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(),
                                                   "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                   "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                            TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString());
                                        }
                                    }
                                }
                            //}

                            //if (resultint > 0)
                            //{
                                if (Convert.ToDouble(TotalAmount) > 0)
                                {
                                    //  Credit Parking Account To HO
                                    dt = AAS.GetADMSubGl("1");
                                    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["ADMGlCode"].ToString(), dt.Rows[0]["ADMSubGlCode"].ToString(),
                                                "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(TotalAmount.ToString()), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                                "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                }
                            //}

                            //if (resultint > 0)
                            //{
                                if (Convert.ToDouble(TotalAmount) > 0)
                                {
                                    //  Debit Parking Account To Branch
                                    dt = AAS.GetADMSubGl(BrCode.ToString());
                                    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), dt.Rows[0]["ADMGlCode"].ToString(), dt.Rows[0]["ADMSubGlCode"].ToString(),
                                                    "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(TotalAmount.ToString()), "2", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                                    "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                }
                            }
                        //}

                        //if (resultint > 0)
                        //{
                            dt = new DataTable();
                            dt = AAS.GetSubGlCode("1");

                            GlCode = SA.GetGlCode("1", dt.Rows[0]["SHARES_GL"].ToString());
                            //string RSPara = SA.GetRSPara(dt.Rows[0]["SHARES_GL"].ToString());
                            // if (RSPara == "Y")
                            // {
                             MWelAccNo = "";
                             Para = SA.GetParameter();
                            if (Para == "HO")
                            {
                                MWelAccNo = SA.GetAccno("1", "4",CustNo.ToString());
                                MemberNo = MWelAccNo;
                                int res = 0;
                                if (MWelAccNo.ToString() != null && MWelAccNo.ToString() != "")
                                    res = SA.InsertAvsACCSHR("1", "4", GlCode, CustNo.ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                            }
                            else
                            {
                                MWelAccNo = SA.GetAccno(Session["BRCD"].ToString(), "4",CustNo.ToString());
                                MemberNo = MWelAccNo;
                                int res = 0;
                                if (MWelAccNo.ToString() != null && MWelAccNo.ToString() != "")
                                    res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), "4", GlCode, CustNo.ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                            }
                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "4", "4", (MWelAccNo == null  || MWelAccNo == "") ? "0" : MWelAccNo.ToString(),
                                       "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["TotShrValue"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                       "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                            //}
                            //else
                            //{
                            //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "4", "4", MemberNo.ToString(),
                            //                "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["TotShrValue"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                            //                "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                            //}
                        //}

                        if ("1" != BrCode.ToString())
                        {
                            //if (resultint > 0)
                            //{
                                if (Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()) > 0)
                                {
                                    GlCode = SA.GetGlCode("1", dt.Rows[0]["ENTRY_GL"].ToString());
                                    string RSPara = SA.GetRSPara1(dt.Rows[0]["ENTRY_GL"].ToString());
                                    BrPara = SA.GetHoPara(dt.Rows[0]["ENTRY_GL"].ToString());
                                    if (RSPara == "Y")
                                    {
                                         Para = SA.GetRSPara(dt.Rows[0]["ENTRY_GL"].ToString(), Session["BRCD"].ToString());
                                         MWelAccNo = "";
                                        if (Para == "Y")
                                        {
                                            Para = SA.GetParameter();
                                            if (Para == "HO")
                                            {
                                                MWelAccNo = SA.GetAccno("1", dt.Rows[0]["ENTRY_GL"].ToString(), CustNo.ToString());
                                                int res = 0;
                                                if (MWelAccNo.ToString() != null && MWelAccNo.ToString() != "")
                                                    res = SA.InsertAvsACCSHR("1", dt.Rows[0]["ENTRY_GL"].ToString(), GlCode, CustNo.ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }
                                            else
                                            {
                                                MWelAccNo = SA.GetAccno(Session["BRCD"].ToString(), dt.Rows[0]["ENTRY_GL"].ToString(), CustNo.ToString());
                                                int res = 0;
                                                if (MWelAccNo.ToString() != null && MWelAccNo.ToString() != "")
                                                    res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["ENTRY_GL"].ToString(), GlCode, CustNo.ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }
                                        }
                                        if (SharePara == "HO" && BrPara == "Y")
                                        //{
                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(), (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(),
                                        //              "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //              "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(), (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(),
                                        //                                                "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //                                                "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        //}
                                        //else
                                        //{
                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(), (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(),
                                                 "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                 "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        //}
                                    }
                                    else
                                    {
                                        if (SharePara == "HO" && BrPara == "Y")
                                        {
                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(), "0",
                                        //               "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //               "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(), "0",
                                        //         "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //         "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        //}
                                        //else
                                        //{
                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["ENTRY_GL"].ToString(), "0",
                                                  "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                  "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        }
                                    }
                                }
                            //}

                            //if (resultint > 0)
                            //{
                                if (Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()) > 0)
                                {
                                    GlCode = SA.GetGlCode("1", dt.Rows[0]["OTHERS_GL1"].ToString());
                                    string RSPara = SA.GetRSPara1(dt.Rows[0]["OTHERS_GL1"].ToString());
                                    BrPara = SA.GetHoPara(dt.Rows[0]["OTHERS_GL1"].ToString());
                                    if (RSPara == "Y")
                                    {
                                         Para = SA.GetRSPara(dt.Rows[0]["OTHERS_GL1"].ToString(), Session["BRCD"].ToString());
                                         MWelAccNo = "";
                                        if (Para == "Y")
                                        {
                                            Para = SA.GetParameter();
                                            if (Para == "HO")
                                            {
                                                MWelAccNo = SA.GetAccno("1", dt.Rows[0]["OTHERS_GL1"].ToString(), CustNo.ToString());
                                                int res = 0;
                                                if (MWelAccNo.ToString() != null && MWelAccNo.ToString() != "")
                                                    res = SA.InsertAvsACCSHR("1", dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, CustNo.ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }
                                            else
                                            {
                                                MWelAccNo = SA.GetAccno(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), CustNo.ToString());
                                                int res = 0;
                                                if (MWelAccNo.ToString() != null && MWelAccNo.ToString() != "")
                                                    res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["OTHERS_GL1"].ToString(), GlCode, CustNo.ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }
                                        }

                                        if (SharePara == "HO" && BrPara == "Y")
                                        //{
                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(), (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(),
                                        //                "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //                "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(), (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(),
                                        //                                                  "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //                                                  "",BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        //}
                                        //else 
                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(), (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(),
                                                    "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                    "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                    }
                                    else
                                    {
                                        if (SharePara == "HO" && BrPara == "Y")
                                        //{
                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(), "0",
                                        //                "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //                "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(), "0",
                                        //           "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //           "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        //}
                                        //else
                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["OTHERS_GL1"].ToString(), "0",
                                                   "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                   "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                    }
                                }
                            //}

                            //if (resultint > 0)
                            //{
                                if (Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()) > 0)
                                {
                                    GlCode = SA.GetGlCode("1", dt.Rows[0]["MemberWel_GL"].ToString());
                                    string RSPara = SA.GetRSPara1(dt.Rows[0]["MemberWel_GL"].ToString());

                                    BrPara = SA.GetHoPara(dt.Rows[0]["MemberWel_GL"].ToString());
                                    if (RSPara == "Y")
                                    {
                                         Para = SA.GetRSPara(dt.Rows[0]["MemberWel_GL"].ToString(), Session["BRCD"].ToString());
                                         MWelAccNo = "";
                                        if (Para == "Y")
                                        {
                                            Para = SA.GetParameter();
                                            if (Para == "HO")
                                            {
                                                MWelAccNo = SA.GetAccno("1", dt.Rows[0]["MemberWel_GL"].ToString(), CustNo.ToString());
                                                int res = 0;
                                                if (MWelAccNo.ToString() != null && MWelAccNo.ToString() != "")
                                                    res = SA.InsertAvsACCSHR("1", dt.Rows[0]["MemberWel_GL"].ToString(), GlCode, CustNo.ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }
                                            else
                                            {
                                                MWelAccNo = SA.GetAccno(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString(), CustNo.ToString());
                                                int res = 0;
                                                if (MWelAccNo.ToString() != null && MWelAccNo.ToString() != "")
                                                    res = SA.InsertAvsACCSHR(Session["BRCD"].ToString(), dt.Rows[0]["MemberWel_GL"].ToString(), GlCode, CustNo.ToString(), MWelAccNo, Session["EntryDate"].ToString(), Session["MID"].ToString());
                                            }
                                        }
                                        if (SharePara == "HO" && BrPara == "Y")
                                        //{
                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(), (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(),
                                        //              "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //              "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(), (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(),
                                        //         "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //         "",BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        //}
                                        //else
                                        //{
                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(), (MWelAccNo == null || MWelAccNo == "") ? "0" : MWelAccNo.ToString(),
                                                 "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                 "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                        //}
                                    }
                                    else
                                    {
                                        if (SharePara == "HO" && BrPara == "Y")
                                        //{
                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(), "0",
                                        //                "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //                "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                        //    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(), "0",
                                        //          "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                        //          "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());

                                        //}
                                        //else
                                        //{
                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, dt.Rows[0]["MemberWel_GL"].ToString(), "0",
                                                   "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                   "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), "0", Session["BRCD"].ToString(), BrCode.ToString());
                                       // }
                                    }
                                }
                            }
                        //}

                        //if (resultint > 0)
                        //{
                            if (ShrData.Rows[0]["ApplType"].ToString() == "1")
                            {
                                // Insert Record Into SharesInfo Table if application is Additional Shares
                                resultint = AAS.InsertShrInfo("1", CustNo.ToString(), MeetDate.ToString(), BoardRegNo.ToString(), MemberNo.ToString(), FromNo.ToString(), ToNo.ToString(), ShrData.Rows[0]["NoOfSHR"].ToString(), ShrData.Rows[0]["SHRValue"].ToString(), ShrData.Rows[0]["TotShrValue"].ToString(), CertNo.ToString(), EntFee, WelFee, WelFeeLoan, AppNo.ToString(), SetNo, BrCode.ToString(), "1003", Session["MID"].ToString(), Session["EntryDate"].ToString(), "Application " + AppNo.ToString() + " allocated");
                            }

                            if (resultint > 0)
                            {
                                resultint = AAS.UpDateAppStatus("1", CustNo.ToString(), CertNo.ToString(), MemberNo.ToString(), AppStatus.ToString(), AppNo.ToString(), BrCode.ToString(), Session["EntryDate"].ToString(), "AD", SetNo);

                                if (resultint > 0)
                                {
                                    BindUnallotedGrid();
                                    lblMessage.Text = "Batch Successfully Alloted With Set No : '" + FSet + "' To '" + SetNo + "' ...!!";
                                    FL = "Insert";//Dhanya Shetty
                                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Share_create _" + FSet + "_" + SetNo + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                    ModalPopup.Show(this.Page);
                                    return;
                                }
                            }
                        //}
                    }
                }
                else
                {
                    lblMessage.Text = "Appliaction " + AppNo + " is already Alloted...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void callData()
    {
        try
        {
            if (ViewState["Flag"].ToString() != "AD")
            {
                DataTable DT = new DataTable();
                DataTable DT1 = new DataTable();
                DT = SA.GetInfo(ViewState["Id"].ToString(), ViewState["CustNo"].ToString());
                DT1 = App.GetInfo( ViewState["CustNo"].ToString(),Session["BRCD"].ToString());
                if (DT1.Rows.Count > 0)
                {
                    txtcustno.Text = DT1.Rows[0]["CustNo"].ToString();
                    if (DT1.Rows[0]["MEM_TYPE"].ToString() == "1")
                        ddlAppType.Enabled = false;
                    else if (DT1.Rows[0]["MEM_TYPE"].ToString() == "2")
                        txtcustno.Text = DT1.Rows[0]["CustNo"].ToString();
                    txtcusname.Text = DT1.Rows[0]["CUSTNAME"].ToString();
                    txtAddress.Text = DT1.Rows[0]["ADDRESS"].ToString();
                   
                    txtpin.Text = DT1.Rows[0]["PINCODE"].ToString();
                    ddstate.SelectedValue = DT1.Rows[0]["state"].ToString();
                    if (DT1.Rows[0]["state"].ToString() == "16")
                    {
                        BD.BindDistrict(dddistrict, DT1.Rows[0]["state"].ToString());
                        dddistrict.SelectedValue = DT1.Rows[0]["district"].ToString();

                        BD.BindArea(ddtaluka, DT1.Rows[0]["district"].ToString());
                        ddtaluka.SelectedValue = DT1.Rows[0]["Area_Taluka"].ToString();



                    }
                    txtemailid.Text = DT1.Rows[0]["EMAIL_ID"].ToString();
                    txtMob.Text = DT1.Rows[0]["MOBILE1"].ToString();
                    ddlWard.SelectedValue = DT1.Rows[0]["FLAT_ROOMNO"].ToString();
                    txtNamed.Text = DT1.Rows[0]["COMITNAME1"].ToString();
                    txtsecname.Text = DT1.Rows[0]["COMMITNAME2"].ToString();
                    txtdMob1.Text = DT1.Rows[0]["MOBILE"].ToString();
                    txtMobile.Text = DT1.Rows[0]["MOBILE1"].ToString();
             
          
                  
                  
                }

                if (DT.Rows.Count > 0)
                {
                    BD.BindRelation(ddlRelation1);
                    BD.BindRelation(ddlRelation2);
                    BD.BindPayment(ddlPayType, "1");

                    ddlAppType.SelectedValue = DT.Rows[0]["ApplType"].ToString();

                    if (DT.Rows[0]["ApplType"].ToString() == "1")
                        ddlAppType.Enabled = false;
                    else if (DT.Rows[0]["ApplType"].ToString() == "2")
                        txtMemNo.Text = DT.Rows[0]["MemberNo"].ToString();

                    txtShareAccNo.Text = DT.Rows[0]["ShareAccNo"].ToString();
                    txtcustno.Text = DT.Rows[0]["CustNo"].ToString();
                    txtcusname.Text = DT.Rows[0]["CUSTNAME"].ToString();
                    txtcustno.Enabled = false;
                    txtcusname.Enabled = false;
                    txtAppNo.Text = DT.Rows[0]["AppNo"].ToString();
                    txtNoOfShr.Text = DT.Rows[0]["NoOfSHR"].ToString();
                    txtShrValue.Text = DT.Rows[0]["TotShrValue"].ToString();
                    txtTotShr.Text = DT.Rows[0]["TotShrValue"].ToString() == "" ? "0" : DT.Rows[0]["TotShrValue"].ToString();
                    txtAccNo.Text = DT.Rows[0]["SavAccNo"].ToString();
                    txtAccName.Text = DT.Rows[0]["SavCustname"].ToString() == "" ? "NA" : DT.Rows[0]["SavCustname"].ToString();
                    txtEntFee.Text = DT.Rows[0]["EntFee"].ToString() == "" ? "0" : DT.Rows[0]["EntFee"].ToString();
                    txtSavFee.Text = DT.Rows[0]["SavFee"].ToString() == "" ? "0" : DT.Rows[0]["SavFee"].ToString();
                    txtOther1.Text = DT.Rows[0]["Other1"].ToString() == "" ? "0" : DT.Rows[0]["Other1"].ToString();
                    txtOther2.Text = DT.Rows[0]["Other2"].ToString() == "" ? "0" : DT.Rows[0]["Other2"].ToString();
                    txtOther3.Text = DT.Rows[0]["Other3"].ToString() == "" ? "0" : DT.Rows[0]["Other3"].ToString();
                    txtOther4.Text = DT.Rows[0]["Other4"].ToString() == "" ? "0" : DT.Rows[0]["Other4"].ToString();
                    txtOther5.Text = DT.Rows[0]["Other5"].ToString() == "" ? "0" : DT.Rows[0]["Other1"].ToString();
                    txtMemWelFee.Text = DT.Rows[0]["MemberWelFee"].ToString() == "" ? "0" : DT.Rows[0]["MemberWelFee"].ToString();
                    txtSerChrFee.Text = DT.Rows[0]["ServiceFee"].ToString() == "" ? "0" : DT.Rows[0]["ServiceFee"].ToString();
                    txtTotAmount.Text = Convert.ToDouble(Convert.ToDouble(txtTotShr.Text.Trim().ToString()) + Convert.ToDouble(txtSavFee.Text.Trim().ToString()) + Convert.ToDouble(txtEntFee.Text.Trim().ToString()) + Convert.ToDouble(txtOther1.Text.Trim().ToString()) + Convert.ToDouble(txtOther2.Text.Trim().ToString()) + Convert.ToDouble(txtOther3.Text.Trim().ToString()) + Convert.ToDouble(txtOther4.Text.Trim().ToString()) + Convert.ToDouble(txtOther5.Text.Trim().ToString()) + Convert.ToDouble(txtMemWelFee.Text.Trim().ToString()) + Convert.ToDouble(txtSerChrFee.Text.Trim().ToString())).ToString();

                    txtNomName1.Text = DT.Rows[0]["NOMI_1_NAME"].ToString();
                    TxtDOB1.Text = DT.Rows[0]["DOB1"].ToString();
                    TxtAge1.Text = DT.Rows[0]["NOMI_1_AGE"].ToString();
                    ddlRelation1.SelectedValue = DT.Rows[0]["NOMI_1_RALATION"].ToString();

                    txtNomName2.Text = DT.Rows[0]["NOMI_2_NAME"].ToString();
                    TxtDOB2.Text = DT.Rows[0]["DOB2"].ToString();
                    TxtAge2.Text = DT.Rows[0]["NOMI_2_AGE"].ToString();
                    ddlRelation2.SelectedValue = DT.Rows[0]["NOMI_2_RALATION"].ToString();

                    ddlPayType.SelectedValue = DT.Rows[0]["PMTMode"].ToString();
                    txtRemark.Text = DT.Rows[0]["REAMARK"].ToString();

                    if (DT.Rows[0]["PMTMode"].ToString() == "1")
                    {
                        txtNarration.Text = DT.Rows[0]["Particulars"].ToString();
                        txtAmount.Text = Convert.ToDouble(txtTotAmount.Text.Trim().ToString()).ToString();

                        txtNarration.Enabled = false;
                        Transfer.Visible = false;
                        divNarration.Visible = true;
                        Clear();
                    }
                    else if (DT.Rows[0]["PMTMode"].ToString() == "2")
                    {

                        ViewState["GlCode"] = DT.Rows[0]["GlCode"].ToString();
                        txtProdType1.Text = DT.Rows[0]["SubGlCode"].ToString();
                        txtProdName1.Text = DT.Rows[0]["GLNAME"].ToString();
                        TxtAccNo1.Text = DT.Rows[0]["AccNo"].ToString();
                        TxtAccName1.Text = DT.Rows[0]["CUSTNAME"].ToString();

                        string[] TD = Session["EntryDate"].ToString().Split('/');
                        txtBalance.Text = SA.GetOpenClose(TD[2].ToString(), TD[1].ToString(), txtProdType1.Text, TxtAccNo1.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString()).ToString();

                        txtNarration.Text = DT.Rows[0]["Particulars"].ToString();
                        txtAmount.Text = Convert.ToDouble(txtTotAmount.Text.Trim().ToString()).ToString();

                        txtNarration.Enabled = false;
                        Transfer.Visible = true;
                        divIntrument.Visible = false;
                        divNarration.Visible = true;
                    }
                    else if (DT.Rows[0]["PMTMode"].ToString() == "4")
                    {
                        ViewState["GlCode"] = DT.Rows[0]["GlCode"].ToString();
                        txtProdType1.Text = DT.Rows[0]["SubGlCode"].ToString();
                        txtProdName1.Text = DT.Rows[0]["GLNAME"].ToString();
                        TxtAccNo1.Text = "0";
                        TxtAccName1.Text = "0";

                        string[] TD = Session["EntryDate"].ToString().Split('/');
                        txtBalance.Text = SA.GetOpenClose(TD[2].ToString(), TD[1].ToString(), txtProdType1.Text, TxtAccNo1.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString()).ToString();

                        TxtChequeNo.Text = DT.Rows[0]["InstNo"].ToString();
                        TxtChequeDate.Text = DT.Rows[0]["InsDate"].ToString();

                        txtNarration.Text = DT.Rows[0]["Particulars"].ToString();
                        txtAmount.Text = Convert.ToDouble(txtTotAmount.Text.Trim().ToString()).ToString();

                        txtNarration.Enabled = false;
                        Transfer.Visible = true;
                        divIntrument.Visible = true;
                        divNarration.Visible = true;
                    }
                    else
                    {
                        Transfer.Visible = false;
                        divNarration.Visible = false;
                    }

                    btnReceipt.Visible = false;
                    Submit.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Clear()
    {
        try
        {
            txtProdType1.Text = "";
            txtProdName1.Text = "";
            TxtAccNo1.Text = "";
            TxtAccName1.Text = "";
            txtBalance.Text = "";
            TxtChequeNo.Text = "";
            TxtChequeDate.Text = Session["EntryDate"].ToString();

            txtProdType1.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        finally
        {
            txtProdType1.Focus();
        }

    }

    ////Added by ankita on 26/06/2017
    public void BindGrid1()
    {
        try
        {
            CurrentCls.Getinfotable(grdCashRct, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "SHAREAPP");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Page Index Changing Event

    protected void grdMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdMaster.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grdOwgData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdCashRct.PageIndex = e.NewPageIndex;
            BindGrid1();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }

    protected void grdCashRct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["CHECK_FLAG"].ToString() == "PRINT")
            {
                string Setno = (grdCashRct.SelectedRow.FindControl("SET_NO") as Label).Text;
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Share_Print _" + ShareAccNo.ToString() + "_" + AppNum.ToString() + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                string redirectURL = "FrmRView.aspx?SETNO=" + Setno + "&UserName=" + Session["UserName"].ToString() + "&EDT=" + Session["EntryDate"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&FN=R&rptname=RptReceiptPrintHSFM_ShareApp.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    //protected void txtShareAccName_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string CustName = txtShareAccName.Text.ToString();
    //        string[] CustNo = CustName.Split('_');
    //        if (CustNo.Length > 1)
    //        {
    //            txtShareAccName.Text = CustNo[0].ToString();
    //            txtcusname.Text = CustNo[0].ToString();
    //            txtcustno.Text = (string.IsNullOrEmpty(CustNo[1].ToString()) ? "" : CustNo[1].ToString()); ;
    //            txtShareAccNo.Text = (string.IsNullOrEmpty(CustNo[2].ToString()) ? "" : CustNo[2].ToString());

    //            txtNoOfShr.Focus();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
    
}