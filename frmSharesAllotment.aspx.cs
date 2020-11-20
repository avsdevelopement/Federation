using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmSharesAllotment : System.Web.UI.Page
{
    ClsAuthorized Voucher = new ClsAuthorized();
    ClsSharesAllotment SA = new ClsSharesAllotment();
    ClsInsertTrans Trans = new ClsInsertTrans();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAccopen accop = new ClsAccopen();
    ClsLogin LG = new ClsLogin();
    DataTable ShrData = new DataTable();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    double TotalAmount = 0;
    string MeetDate, MemberNo, CertNo, FromNo, BoardRegNo, ToNo = "";
    string RefNo, sResult, WelFee, WelFeeLoan, FSet, EntFee = "";
    bool SetNumber = true;
    int resultint;

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            ViewState["Flag"] = Request.QueryString["FL"].ToString();
            if (Request.QueryString["FL"].ToString() == "VW")
            {
                DivApplications.Visible = true;
                btnAppDetails.Visible = true;
                btnBatchAllocate.Visible = true;
                btnNextDetails.Visible = false;
                btnAllocate.Visible = false;
                BindBranch(ddlBranchName);

                ddlBranchName.Focus();
            }
            else if (Request.QueryString["FL"].ToString() == "AD")
            {
                DivApplications.Visible = false;
                DivAppDetails.Visible = true;
                btnAppDetails.Visible = false;
                btnBatchAllocate.Visible = false;
                btnNextDetails.Visible = true;
                btnAllocate.Visible = false;
                BindBranch(ddlBranchName);
                BD.BindRelation(ddlRelation1);
                BD.BindRelation(ddlRelation2);
                ShowDetails();
                btnNextDetails.Focus();
            }
            else if (Request.QueryString["FL"].ToString() == "AT")
            {
                Display(Request.QueryString["BrCode"].ToString(), Request.QueryString["CustNo"].ToString(), Request.QueryString["AppNo"].ToString());
                DivApplications.Visible = false;
                DivAppDetails.Visible = false;
                DivApp.Visible = true;
                btnAppDetails.Visible = false;
                btnBatchAllocate.Visible = false;
                btnNextDetails.Visible = false;
                btnAllocate.Visible = true;
                ddlStatus.Focus();
            }
        }
    }

    #endregion

    #region Select Index Changed

    protected void ddlBranchName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SetNumber = true;
            txtBranchCode.Text = "";
            txtBranchCode.Text = ddlBranchName.SelectedValue.ToString();
            txtWorkDate.Text = LG.openDay(txtBranchCode.Text.Trim().ToString());

            BindUnallotedGrid();
            BindAllotedGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStatus.SelectedValue == "11")
            {
                DivRegNo.Visible = true;
                txtMeetDate.Focus();
            }
            else if (ddlStatus.SelectedValue == "12")
            {
                DivRegNo.Visible = false;
                btnAllocate.Focus();
            }
            else if (ddlStatus.SelectedValue == "13")
            {
                DivRegNo.Visible = false;
                btnAllocate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Button Click Event

    protected void btnFetchApp_Click(object sender, EventArgs e)
    {
        try
        {
            BindUnallotedGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnAllotedApp_Click(object sender, EventArgs e)
    {
        try
        {
            BindAllotedGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnAppDetails_Click(object sender, EventArgs e)
    {
        try
        {
            string[] AppDtls;
            int Count = 0;

            foreach (GridViewRow gvrow in grdAppDetails.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkBox");

                if (chk != null && chk.Checked)
                {
                    Count = Count + 1;
                }
            }

            if (Convert.ToInt32(Count) == 1)
            {
                foreach (GridViewRow gvRow in grdAppDetails.Rows)
                {
                    if (((CheckBox)gvRow.FindControl("chkBox")).Checked)
                    {
                        AppDtls = grdAppDetails.DataKeys[gvRow.DataItemIndex].Value.ToString().Split('_');
                        ViewState["BrCode"] = AppDtls[0].ToString();
                        ViewState["CustNo"] = AppDtls[1].ToString();
                        ViewState["AppNo"] = AppDtls[2].ToString();
                    }
                }

                if (ViewState["BrCode"].ToString() != "" && ViewState["CustNo"].ToString() != "" && ViewState["AppNo"].ToString() != "")
                {
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Shares_Allotmnt _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                    string redirectURL = "frmSharesAllotment.aspx?FL=AD&BrCode=" + ViewState["BrCode"].ToString() + "&CustNo=" + ViewState["CustNo"].ToString() + "&AppNo=" + ViewState["AppNo"].ToString() + "";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
            else
            {
                lblMessage.Text = "Select single application only...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnNextDetails_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Flag"].ToString() == "AD")
            {
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Shares_Allotmnt _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                string url = "frmSharesAllotment.aspx?FL=AT&BrCode=" + txtBrCode.Text.Trim().ToString() + "&CustNo=" + txtcustno.Text.Trim().ToString() + "&AppNo=" + txtAppNo.Text.Trim().ToString() + "";
                Response.Redirect(url, false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnAllocate_Click(object sender, EventArgs e)
    {
        try
        {
            if (((SA.GetShrAlloAutho()) == "HO" && Session["BRCD"].ToString() == "1") || ((SA.GetShrAlloAutho()) == "BR") && (Session["BRCD"].ToString() == Request.QueryString["BrCode"].ToString()))
            {
                ApplicationAllocate(Request.QueryString["BrCode"].ToString(), Request.QueryString["CustNo"].ToString(), Request.QueryString["AppNo"].ToString());
            }
            else
            {
                lblMessage.Text = "Not allow for branch...!!";
                ModalPopup.Show(this.Page);
                return;
            }
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

            if (((SA.GetShrAlloAutho()) == "HO" && Session["BRCD"].ToString() == "1") || (SA.GetShrAlloAutho()) == "BR")
            {
                foreach (GridViewRow gvRow in grdAppDetails.Rows)
                {
                    if (((CheckBox)gvRow.FindControl("chkBox")).Checked)
                    {
                        AppDtls = grdAppDetails.DataKeys[gvRow.DataItemIndex].Value.ToString().Split('_');

                        ApplBatchAllocate(AppDtls[0].ToString(), AppDtls[1].ToString(), AppDtls[2].ToString(), "11");
                    }
                }
            }
            else
            {
                lblMessage.Text = "Not allow for branch...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Flag"].ToString() == "AD")
            {
                ClearAddAll();
            }
            if (ViewState["Flag"].ToString() == "AT")
            {
                ClearTrfAll();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Public Function

    protected void BindBranch(DropDownList ddlBrName)
    {
        try
        {
            BD.BindBRANCHNAME(ddlBrName, null);
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
            SA.BindUnallotedGrid(grdAppDetails, txtBranchCode.Text.Trim().ToString(), txtWorkDate.Text.ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BindAllotedGrid()
    {
        try
        {
            SA.BindAllotedGrid(grdAllotedAppDetails, txtBranchCode.Text.Trim().ToString(), txtWorkDate.Text.ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ShowDetails()
    {
        try
        {
            sResult = SA.CheckAppStage(Request.QueryString["BrCode"].ToString(), Request.QueryString["CustNo"].ToString(), Request.QueryString["AppNo"].ToString());

            if (sResult == "11")
            {
                btnNextDetails.Visible = false;
                lblMessage.Text = "Application Is Allready Allocated...!!";
                ModalPopup.Show(this.Page);
                return;
            }
            if (sResult == "12")
            {
                btnNextDetails.Visible = false;
                lblMessage.Text = "Application Is Pending...!!";
                ModalPopup.Show(this.Page);
                return;
            }
            if (sResult == "13")
            {
                btnNextDetails.Visible = false;
                lblMessage.Text = "Application Is Rejected...!!";
                ModalPopup.Show(this.Page);
                return;
            }
            if (sResult == "1")
            {
                DT = SA.GetApplicationData(Request.QueryString["BrCode"].ToString(), Request.QueryString["CustNo"].ToString(), Request.QueryString["AppNo"].ToString());

                if (DT != null || DT.Rows.Count > 0)
                {
                    txtBrCode.Text = Convert.ToDouble(DT.Rows[0]["BRCD"].ToString()).ToString();
                    txtAppNo.Text = Convert.ToInt32(DT.Rows[0]["AppNo"].ToString()).ToString();
                    txtNoOfShr.Text = Convert.ToInt32(DT.Rows[0]["NoOfSHR"].ToString()).ToString();
                    txtShrValue.Text = DT.Rows[0]["SHRValue"].ToString();
                    txtTotShr.Text = Convert.ToDouble(Convert.ToInt32(DT.Rows[0]["NoOfSHR"].ToString()) * Convert.ToDouble(DT.Rows[0]["SHRValue"].ToString())).ToString();

                    txtAccNo.Text = DT.Rows[0]["SavAccNo"].ToString();
                    txtcusname.Text = DT.Rows[0]["CustName"].ToString();
                    txtcustno.Text = Convert.ToInt32(DT.Rows[0]["CustNo"].ToString()).ToString();
                    txtAccName.Text = DT.Rows[0]["CustName"].ToString();

                    txtSavFee.Text = Convert.ToDouble(DT.Rows[0]["SavFee"].ToString()).ToString();
                    txtEntFee.Text = Convert.ToDouble(DT.Rows[0]["EntFee"].ToString()).ToString();
                    txtOther1.Text = Convert.ToDouble(DT.Rows[0]["Other1"].ToString()).ToString();
                    txtOther2.Text = Convert.ToDouble(DT.Rows[0]["Other2"].ToString()).ToString();

                    txtMemWelFee.Text = Convert.ToDouble(DT.Rows[0]["MemberWelFee"].ToString()).ToString();
                    txtSerChrFee.Text = Convert.ToDouble(DT.Rows[0]["ServiceFee"].ToString()).ToString();

                    txtRemark.Text = DT.Rows[0]["REAMARK"].ToString();
                    txtTotAmount.Text = Convert.ToDouble((((Convert.ToDouble(txtTotShr.Text) + Convert.ToDouble(txtSavFee.Text)) + Convert.ToDouble(txtEntFee.Text)) + Convert.ToDouble(txtOther1.Text)) + Convert.ToDouble(txtOther2.Text)).ToString();

                    txtNomName1.Text = DT.Rows[0]["NOMI_1_NAME"].ToString();
                    TxtDOB1.Text = DT.Rows[0]["DOB1"].ToString();
                    TxtAge1.Text = DT.Rows[0]["NOMI_1_AGE"].ToString();
                    ddlRelation1.SelectedValue = DT.Rows[0]["NOMI_1_RALATION"].ToString();

                    txtNomName2.Text = DT.Rows[0]["NOMI_2_NAME"].ToString();
                    TxtDOB2.Text = DT.Rows[0]["DOB2"].ToString();
                    TxtAge2.Text = DT.Rows[0]["NOMI_2_AGE"].ToString();
                    ddlRelation2.SelectedValue = DT.Rows[0]["NOMI_2_RALATION"].ToString();

                    DivAppDetails.Visible = true;
                    btnNextDetails.Visible = true;
                    btnAllocate.Visible = false;

                    btnNextDetails.Focus();
                }
                else
                {
                    lblMessage.Text = "Information Not Available...!!";
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

    protected void Display(string BrCode, string CustNo, string AppNo)
    {
        try
        {
            DT = SA.GetApplicationData(BrCode, CustNo, AppNo);
            if (DT.Rows.Count > 0)
            {
                if (DT.Rows[0]["ApplType"].ToString() == "1")
                {
                    txtMemNo.Text = Convert.ToInt32(SA.GetMaxMemNo(Session["BRCD"].ToString(), Request.QueryString["CustNo"].ToString())).ToString();   // Rakesh 07-02-2019
                }
                else if (DT.Rows[0]["ApplType"].ToString() == "2")
                {
                    txtMemNo.Text = Convert.ToInt32(SA.GetMemNo(Session["BRCD"].ToString(), DT.Rows[0]["MemberNo"].ToString())).ToString();
                }

                txtRegNo.Text = Convert.ToInt32(SA.GetBoardRegNo(Session["BRCD"].ToString())).ToString();
                txtCertNo.Text = Convert.ToInt32(SA.GetCertNo("1")).ToString();
                txtMeetDate.Text = Session["EntryDate"].ToString();

                txtFromNumber.Text = Convert.ToInt32(SA.GetFromNo(Session["BRCD"].ToString())).ToString();
                txtToNumber.Text = Convert.ToInt32(Convert.ToInt32(txtFromNumber.Text.Trim().ToString()) + (Convert.ToInt32(DT.Rows[0]["NoOfSHR"].ToString()) - 1)).ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ApplicationAllocate(string BrCode, string CustNo, string AppNo)
    {
        try
        {
            int AccNo = 0;

            if (ddlStatus.SelectedValue == "11")
            {
                string SetNo = "", GlCode = "";

                ShrData = SA.GetApplicationData(BrCode.ToString(), CustNo.ToString(), AppNo.ToString());
                if (ShrData.Rows.Count > 0 && ShrData.Rows[0]["ApplStatus"].ToString() == "1")
                {
                    if (ShrData.Rows[0]["ApplType"].ToString() == "2" && ShrData.Rows[0]["MemberNo"].ToString() != "" && ShrData.Rows[0]["MemberNo"].ToString() != null && ShrData.Rows[0]["MemberNo"].ToString() != "0" || ShrData.Rows[0]["ApplType"].ToString() == "1")
                    {
                        DT = new DataTable();
                        DT = SA.GetSubGlCode(BrCode.ToString());

                        if (DT.Rows.Count > 0)
                        {
                            RefNo = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                            ViewState["RID"] = (Convert.ToInt32(RefNo) + 1).ToString();
                            SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "IBTSetNo", Session["BRCD"].ToString()).ToString();
                            string CS = accop.Getcustname(CustNo.ToString());

                            if (ShrData.Rows[0]["ApplType"].ToString() == "1")
                            {
                                // Insert Record Into Avs_Acc Table Under subglcode 4 and BranchCode 1
                                AccNo = accop.insert(Session["BRCD"].ToString(), "4", "4", CustNo.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), "1", "1", "", "", "", "", "", "", "", "", "", "", "", "", "0", ShrData.Rows[0]["TotShrValue"].ToString(), "1003", BrCode.ToString(), "0", "0","");
                                
                                // Insert Record Into SharesInfo Table if application is New Shares
                                if (AccNo > 0)
                                {
                                    //Added By Amol On 02/02/2018 for create welfare accounts
                                    SA.CreateAccounts(Session["BRCD"].ToString(), CustNo.ToString(), DT.Rows[0]["Others_GL1"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                    //Added By Amol On 02/02/2018 for create welfare loan accounts
                                    SA.CreateAccounts(Session["BRCD"].ToString(), CustNo.ToString(), DT.Rows[0]["MemberWel_GL"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                    resultint = SA.InsertShrInfo(Session["BRCD"].ToString(), CustNo.ToString(), txtMeetDate.Text.ToString(), txtRegNo.Text.ToString(), txtMemNo.Text.Trim().ToString(), txtFromNumber.Text.Trim().ToString(), txtToNumber.Text.Trim().ToString(), ShrData.Rows[0]["NoOfSHR"].ToString(), ShrData.Rows[0]["SHRValue"].ToString(), ShrData.Rows[0]["TotShrValue"].ToString(), txtCertNo.Text.Trim().ToString(), EntFee, WelFee, WelFeeLoan, AppNo.ToString(), SetNo, BrCode.ToString(), "1003", Session["MID"].ToString(), Session["EntryDate"].ToString(), txtRemark1.Text.ToString());
                                }
                            }
                            else if (ShrData.Rows[0]["ApplType"].ToString() == "2")
                            {
                                // Insert Record Into SharesInfo Table if application is Additional Shares
                                resultint = SA.InsertShrInfo(Session["BRCD"].ToString(), CustNo.ToString(), txtMeetDate.Text.ToString(), txtRegNo.Text.ToString(), txtMemNo.Text.Trim().ToString(), txtFromNumber.Text.Trim().ToString(), txtToNumber.Text.Trim().ToString(), ShrData.Rows[0]["NoOfSHR"].ToString(), ShrData.Rows[0]["SHRValue"].ToString(), ShrData.Rows[0]["TotShrValue"].ToString(), txtCertNo.Text.Trim().ToString(), EntFee, WelFee, WelFeeLoan, AppNo.ToString(), SetNo, BrCode.ToString(), "1003", Session["MID"].ToString(), Session["EntryDate"].ToString(), txtRemark1.Text.ToString());
                            }

                            if (resultint > 0)
                            {
                                GlCode = SA.GetGlCode(BrCode.ToString(), DT.Rows[0]["SHARES_GL"].ToString());
                                resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, DT.Rows[0]["SHARES_GL"].ToString(), ShrData.Rows[0]["ShareAccNo"].ToString() == "" ? "0" : ShrData.Rows[0]["ShareAccNo"].ToString(),
                                            "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["TotShrValue"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                            "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), BrCode.ToString());
                                TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["TotShrValue"].ToString());

                                if (Session["BRCD"].ToString() != BrCode.ToString())
                                {
                                    if (resultint > 0)
                                    {
                                        if (ChkEntrance.Checked == true)
                                        {
                                            EntFee = "Y";

                                            if (Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()) > 0)
                                            {
                                                GlCode = SA.GetGlCode(BrCode.ToString(), DT.Rows[0]["ENTRY_GL"].ToString());
                                                resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, DT.Rows[0]["ENTRY_GL"].ToString(),
                                                           "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                           "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), BrCode.ToString());
                                                TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            EntFee = "N";
                                        }
                                    }

                                    if (resultint > 0)
                                    {
                                        if (ChkWelfare.Checked == true)
                                        {
                                            WelFee = "Y";

                                            if (Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()) > 0)
                                            {
                                                GlCode = SA.GetGlCode(BrCode.ToString(), DT.Rows[0]["OTHERS_GL1"].ToString());
                                                resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, DT.Rows[0]["OTHERS_GL1"].ToString(),
                                                            "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                            "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), BrCode.ToString());
                                                TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            WelFee = "N";
                                        }
                                    }

                                    if (resultint > 0)
                                    {
                                        if (ChkWelLoan.Checked == true)
                                        {
                                            WelFeeLoan = "Y";

                                            if (Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()) > 0)
                                            {
                                                GlCode = SA.GetGlCode(BrCode.ToString(), DT.Rows[0]["MemberWel_GL"].ToString());
                                                resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, DT.Rows[0]["MemberWel_GL"].ToString(),
                                                           "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                           "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), BrCode.ToString());
                                                TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            WelFeeLoan = "N";
                                        }
                                    }
                                }

                                if ((Session["BRCD"].ToString() != BrCode.ToString()))
                                {
                                    if (resultint > 0)
                                    {
                                        if (Convert.ToDouble(TotalAmount) > 0)
                                        {
                                            //  Credit Parking Account To HO
                                            DT = SA.GetADMSubGl("1");
                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["ADMGlCode"].ToString(), DT.Rows[0]["ADMSubGlCode"].ToString(),
                                                        "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(TotalAmount.ToString()), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                                        "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), BrCode.ToString());
                                        }
                                    }

                                    if (resultint > 0)
                                    {
                                        if (Convert.ToDouble(TotalAmount) > 0)
                                        {
                                            //  Debit Parking Account To Branch
                                            DT = SA.GetADMSubGl(BrCode.ToString());
                                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["ADMGlCode"].ToString(), DT.Rows[0]["ADMSubGlCode"].ToString(),
                                                            "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(TotalAmount.ToString()), "2", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                                                            "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), BrCode.ToString());
                                        }
                                    }
                                }

                                if (resultint > 0)
                                {
                                    DT = new DataTable();
                                    DT = SA.GetSubGlCode(Session["BRCD"].ToString());

                                    GlCode = SA.GetGlCode(Session["BRCD"].ToString(), DT.Rows[0]["SHARES_GL"].ToString());
                                    resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "4", "4", txtMemNo.Text.Trim().ToString(),
                                                "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["TotShrValue"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), BrCode.ToString());
                                }

                                if (Session["BRCD"].ToString() != BrCode.ToString())
                                {
                                    if (resultint > 0)
                                    {
                                        if (ChkEntrance.Checked == true)
                                        {
                                            if (Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()) > 0)
                                            {
                                                GlCode = SA.GetGlCode(Session["BRCD"].ToString(), DT.Rows[0]["ENTRY_GL"].ToString());
                                                resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, DT.Rows[0]["ENTRY_GL"].ToString(), "0",
                                                           "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                           "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), BrCode.ToString());
                                            }
                                        }
                                    }

                                    if (resultint > 0)
                                    {
                                        if (ChkWelfare.Checked == true)
                                        {
                                            if (Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()) > 0)
                                            {
                                                GlCode = SA.GetGlCode(Session["BRCD"].ToString(), DT.Rows[0]["OTHERS_GL1"].ToString());
                                                resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, DT.Rows[0]["OTHERS_GL1"].ToString(), "0",
                                                            "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                            "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), BrCode.ToString());
                                            }
                                        }
                                    }

                                    if (resultint > 0)
                                    {
                                        if (ChkWelLoan.Checked == true)
                                        {
                                            if (Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()) > 0)
                                            {
                                                GlCode = SA.GetGlCode(Session["BRCD"].ToString(), DT.Rows[0]["MemberWel_GL"].ToString());
                                                resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, DT.Rows[0]["MemberWel_GL"].ToString(), "0",
                                                            "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1001",
                                                            "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), BrCode.ToString());
                                            }
                                        }
                                    }
                                }

                                if (resultint > 0)
                                {
                                    resultint = SA.UpDateStatus(Session["BRCD"].ToString(), CustNo.ToString(), txtCertNo.Text.Trim().ToString(), txtMemNo.Text.Trim().ToString(), ddlStatus.SelectedValue, AppNo.ToString(), BrCode.ToString(), Session["EntryDate"].ToString(), "AD");

                                    if (resultint > 0)
                                    {
                                        lblMessage.Text = "Successfully Allocated With Set No : " + SetNo + " And Member No is : " + txtMemNo.Text.Trim().ToString() + "";
                                        ModalPopup.Show(this.Page);
                                        ClearAddAll();
                                        ClearTrfAll();
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Member no not exists for appliaction : " + AppNo + "...!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                }
                else
                {
                    lblMessage.Text = "Appliaction " + AppNo + " is already alloted...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
            else if (ddlStatus.SelectedValue == "12")
            {
                resultint = SA.UpDateStat(BrCode.ToString(), CustNo.ToString(), AppNo.ToString(), ddlStatus.SelectedValue, Session["MID"].ToString(), Session["EntryDate"].ToString(), "PD");

                if (resultint > 0)
                {
                    lblMessage.Text = "Pending Application Successfully...!!";
                    ModalPopup.Show(this.Page);
                    ClearAddAll();
                    ClearTrfAll();
                    return;
                }
            }
            else if (ddlStatus.SelectedValue == "13")
            {
                resultint = SA.UpDateStat(BrCode.ToString(), CustNo.ToString(), AppNo.ToString(), ddlStatus.SelectedValue, Session["MID"].ToString(), Session["EntryDate"].ToString(), "RJ");

                if (resultint > 0)
                {
                    lblMessage.Text = "Reject Application Successfully...!!";
                    ModalPopup.Show(this.Page);
                    ClearAddAll();
                    ClearTrfAll();
                    return;
                }
            }
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
            int AccNo = 0;
            string SetNo = "", GlCode = "";
            TotalAmount = 0;

            DT = new DataTable();
            DT = SA.GetSubGlCode(BrCode.ToString());

            if (DT.Rows.Count > 0)
            {
                ShrData = SA.GetApplicationData(BrCode.ToString(), CustNo.ToString(), AppNo.ToString());
                if (ShrData.Rows.Count > 0 && ShrData.Rows[0]["ApplStatus"].ToString() == "1")
                {
                    if (ShrData.Rows[0]["ApplType"].ToString() == "2" && ShrData.Rows[0]["MemberNo"].ToString() != "" && ShrData.Rows[0]["MemberNo"].ToString() != null && ShrData.Rows[0]["MemberNo"].ToString() != "0" || ShrData.Rows[0]["ApplType"].ToString() == "1")
                    {
                        if (ShrData.Rows[0]["ApplType"].ToString() == "1")
                            MemberNo = Convert.ToInt32(SA.GetMaxMemNo(Session["BRCD"].ToString(), CustNo)).ToString();  // Rakesh 07-02-2019
                        if (ShrData.Rows[0]["ApplType"].ToString() == "2")
                            MemberNo = Convert.ToInt32(SA.GetMemNo(Session["BRCD"].ToString(), ShrData.Rows[0]["MemberNo"].ToString())).ToString();

                        CertNo = Convert.ToInt32(SA.GetCertNo("1")).ToString();
                        FromNo = Convert.ToInt32(SA.GetFromNo(Session["BRCD"].ToString())).ToString();
                        ToNo = Convert.ToInt32(Convert.ToInt32(FromNo.ToString()) + (Convert.ToInt32(ShrData.Rows[0]["NoOfSHR"].ToString()) - 1)).ToString();
                        MeetDate = Session["EntryDate"].ToString();
                        BoardRegNo = Convert.ToInt32(SA.GetBoardRegNo(Session["BRCD"].ToString())).ToString();
                        RefNo = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                        ViewState["RID"] = (Convert.ToInt32(RefNo) + 1).ToString();
                        SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "IBTSetNo", Session["BRCD"].ToString()).ToString();
                        string CS = accop.Getcustname(CustNo.ToString());

                        if (SetNumber == true)
                        {
                            FSet = SetNo;
                            SetNumber = false;
                        }

                        if (ShrData.Rows[0]["ApplType"].ToString() == "1")
                        {
                            // Insert Record Into Avs_Acc Table Under subglcode 4 and BranchCode 1
                            AccNo = accop.insert(Session["BRCD"].ToString(), "4", "4", CustNo.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), "1", "1", "", "", "", "", "", "", "", "", "", "", "", "", "0", "0", "1003", BrCode.ToString(), "0", "0","");

                            // Insert Record Into SharesInfo Table if application is New Shares
                            if (AccNo > 0)
                            {
                                //Added By Amol On 02/02/2018 for create welfare accounts
                                SA.CreateAccounts(Session["BRCD"].ToString(), CustNo.ToString(), DT.Rows[0]["Others_GL1"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                //Added By Amol On 02/02/2018 for create welfare loan accounts
                                SA.CreateAccounts(Session["BRCD"].ToString(), CustNo.ToString(), DT.Rows[0]["MemberWel_GL"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                                resultint = SA.InsertShrInfo(Session["BRCD"].ToString(), CustNo.ToString(), MeetDate.ToString(), BoardRegNo.ToString(), MemberNo.ToString(), FromNo.ToString(), ToNo.ToString(), ShrData.Rows[0]["NoOfSHR"].ToString(), ShrData.Rows[0]["SHRValue"].ToString(), ShrData.Rows[0]["TotShrValue"].ToString(), CertNo.ToString(), EntFee, WelFee, WelFeeLoan, AppNo.ToString(), SetNo, BrCode.ToString(), "1003", Session["MID"].ToString(), Session["EntryDate"].ToString(), "Application " + AppNo.ToString() + " allocated");
                            }
                        }
                        else if (ShrData.Rows[0]["ApplType"].ToString() == "2")
                        {
                            // Insert Record Into SharesInfo Table if application is Additional Shares
                            resultint = SA.InsertShrInfo(Session["BRCD"].ToString(), CustNo.ToString(), MeetDate.ToString(), BoardRegNo.ToString(), MemberNo.ToString(), FromNo.ToString(), ToNo.ToString(), ShrData.Rows[0]["NoOfSHR"].ToString(), ShrData.Rows[0]["SHRValue"].ToString(), ShrData.Rows[0]["TotShrValue"].ToString(), CertNo.ToString(), EntFee, WelFee, WelFeeLoan, AppNo.ToString(), SetNo, BrCode.ToString(), "1003", Session["MID"].ToString(), Session["EntryDate"].ToString(), "Application " + AppNo.ToString() + " allocated");
                        }

                        if (resultint > 0)
                        {
                            GlCode = SA.GetGlCode(BrCode.ToString(), DT.Rows[0]["SHARES_GL"].ToString());
                            resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, DT.Rows[0]["SHARES_GL"].ToString(), ShrData.Rows[0]["ShareAccNo"].ToString() == "" ? "0" : ShrData.Rows[0]["ShareAccNo"].ToString(),
                                        "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["TotShrValue"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                        "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), BrCode.ToString());
                            TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["TotShrValue"].ToString());

                            if (Session["BRCD"].ToString() != BrCode.ToString())
                            {
                                if (resultint > 0)
                                {
                                    if (Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()) > 0)
                                    {
                                        EntFee = "Y";
                                        GlCode = SA.GetGlCode(BrCode.ToString(), DT.Rows[0]["ENTRY_GL"].ToString());
                                        resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, DT.Rows[0]["ENTRY_GL"].ToString(),
                                                   "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                   "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), BrCode.ToString());
                                        TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString());
                                    }
                                }

                                if (resultint > 0)
                                {
                                    if (Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()) > 0)
                                    {
                                        WelFee = "Y";
                                        GlCode = SA.GetGlCode(BrCode.ToString(), DT.Rows[0]["OTHERS_GL1"].ToString());
                                        resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, DT.Rows[0]["OTHERS_GL1"].ToString(),
                                                    "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                    "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), BrCode.ToString());
                                        TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString());
                                    }
                                }

                                if (resultint > 0)
                                {
                                    if (Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()) > 0)
                                    {
                                        WelFeeLoan = "Y";
                                        GlCode = SA.GetGlCode(BrCode.ToString(), DT.Rows[0]["MemberWel_GL"].ToString());
                                        resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, DT.Rows[0]["MemberWel_GL"].ToString(),
                                                   "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "2", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                   "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), BrCode.ToString());
                                        TotalAmount = TotalAmount + Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString());
                                    }
                                }
                            }

                            if ((Session["BRCD"].ToString() != BrCode.ToString()))
                            {
                                if (resultint > 0)
                                {
                                    if (Convert.ToDouble(TotalAmount) > 0)
                                    {
                                        //  Credit Parking Account To HO
                                        DT = SA.GetADMSubGl("1");
                                        resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["ADMGlCode"].ToString(), DT.Rows[0]["ADMSubGlCode"].ToString(),
                                                    "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(TotalAmount.ToString()), "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1003",
                                                    "", BrCode.ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), BrCode.ToString());
                                    }
                                }

                                if (resultint > 0)
                                {
                                    if (Convert.ToDouble(TotalAmount) > 0)
                                    {
                                        //  Debit Parking Account To Branch
                                        DT = SA.GetADMSubGl(BrCode.ToString());
                                        resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["ADMGlCode"].ToString(), DT.Rows[0]["ADMSubGlCode"].ToString(),
                                                        "0", "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(TotalAmount.ToString()), "2", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1003",
                                                        "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), BrCode.ToString());
                                    }
                                }
                            }

                            if (resultint > 0)
                            {
                                DT = new DataTable();
                                DT = SA.GetSubGlCode(Session["BRCD"].ToString());

                                GlCode = SA.GetGlCode(Session["BRCD"].ToString(), DT.Rows[0]["SHARES_GL"].ToString());
                                resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "4", "4", MemberNo.ToString(),
                                            "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["TotShrValue"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                            "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), BrCode.ToString());
                            }

                            if (Session["BRCD"].ToString() != BrCode.ToString())
                            {
                                if (resultint > 0)
                                {
                                    if (Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), DT.Rows[0]["ENTRY_GL"].ToString());
                                        resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, DT.Rows[0]["ENTRY_GL"].ToString(), "0",
                                                   "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["EntFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                   "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), BrCode.ToString());
                                    }
                                }

                                if (resultint > 0)
                                {
                                    if (Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), DT.Rows[0]["OTHERS_GL1"].ToString());
                                        resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, DT.Rows[0]["OTHERS_GL1"].ToString(), "0",
                                                    "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["Other1"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                    "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), BrCode.ToString());
                                    }
                                }

                                if (resultint > 0)
                                {
                                    if (Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()) > 0)
                                    {
                                        GlCode = SA.GetGlCode(Session["BRCD"].ToString(), DT.Rows[0]["MemberWel_GL"].ToString());
                                        resultint = Trans.AddMTable(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode, DT.Rows[0]["MemberWel_GL"].ToString(), "0",
                                                    "ABB - " + BrCode.ToString() + " - " + CustNo.ToString() + "/" + AppNo.ToString() + " - " + CS.ToString() + "", "ShrCustNo " + CustNo.ToString() + "", Convert.ToDouble(ShrData.Rows[0]["MemberWelFee"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1003",
                                                    "", "1", Session["MID"].ToString(), "0", "0", "SHAREALLOTMENT", CustNo.ToString(), CS.ToString(), ViewState["RID"].ToString(), Session["BRCD"].ToString(), BrCode.ToString());
                                    }
                                }
                            }

                            if (resultint > 0)
                            {
                                resultint = SA.UpDateStatus(Session["BRCD"].ToString(), CustNo.ToString(), CertNo.ToString(), MemberNo.ToString(), AppStatus.ToString(), AppNo.ToString(), BrCode.ToString(), Session["EntryDate"].ToString(), "AD");

                                if (resultint > 0)
                                {
                                    lblMessage.Text = "Batch Successfully Alloted With Set No : '" + FSet + "' To '" + SetNo + "' ...!!";
                                    ModalPopup.Show(this.Page);
                                    ClearAddAll();
                                    ClearTrfAll();
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Member no not exists for appliaction : " + AppNo + "...!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                }
                else
                {
                    ClearAddAll();
                    ClearTrfAll();
                    lblMessage.Text = "Appliaction " + AppNo + " is already alloted...!!";
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

    protected void ClearAddAll()
    {
        try
        {
            txtBrCode.Text = "";
            txtAppNo.Text = "";
            txtcusname.Text = "";
            txtcustno.Text = "";
            txtNoOfShr.Text = "";
            txtShrValue.Text = "";
            txtTotShr.Text = "";
            txtAccNo.Text = "";
            txtAccName.Text = "";
            txtSavFee.Text = "";
            txtEntFee.Text = "";
            txtOther1.Text = "";
            txtOther2.Text = "";
            txtMemWelFee.Text = "";
            txtSerChrFee.Text = "";
            txtRemark.Text = "";
            txtTotAmount.Text = "";

            txtNomName1.Text = "";
            TxtDOB1.Text = "";
            TxtAge1.Text = "";
            ddlRelation1.SelectedValue = "0";

            txtNomName2.Text = "";
            TxtDOB2.Text = "";
            TxtAge2.Text = "";
            ddlRelation2.SelectedValue = "0";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ClearTrfAll()
    {
        try
        {
            ddlStatus.SelectedValue = "0";
            txtMeetDate.Text = "";
            txtRegNo.Text = "";
            txtRemark1.Text = "";
            txtFromNumber.Text = "";
            txtToNumber.Text = "";
            ChkEntrance.Checked = false;
            ChkWelfare.Checked = false;
            ChkWelLoan.Checked = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

}