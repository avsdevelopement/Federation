using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UserMaster : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsUserMaster clsa = new ClsUserMaster();  // CREATE OBJECT OF CLASS FILE 
    ClsBindDropdown BD = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    string FL = "";

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            if (Session["UGRP"].ToString() == "1")
            {
                ViewState["Flag"] = Request.QueryString["Flag"].ToString();
                txtpermission.Text = clsa.Get_PERMISSIONNO();
                txtpermission.Enabled = false;
                BD.BindBRANCHNAME(DdlBranchName, Session["BRCD"].ToString());
                txtBranchCode.Enabled = false;

                if (Request.QueryString["Flag"].ToString() == "AD")
                {
                    div_main.Visible = true;
                    div_view.Visible = false;
                    btncreate.Visible = true;
                    btnmodify.Visible = false;
                    btnsuspend.Visible = false;
                    btnauthorize.Visible = false;
                    BtnReport.Visible = true;
                }
                else if (Request.QueryString["Flag"].ToString() == "MD")
                {
                    div_main.Visible = true;
                    div_view.Visible = false;
                    BindGrid();
                    btncreate.Visible = false;
                    btnmodify.Visible = true;
                    btnsuspend.Visible = false;
                    btnauthorize.Visible = false;
                    txtLoginCode.Enabled = false;
                    BtnReport.Visible = true;
                }
                else if (Request.QueryString["Flag"].ToString() == "DL")
                {
                    div_main.Visible = true;
                    div_view.Visible = false;
                    BindGrid();
                    btncreate.Visible = false;
                    btnmodify.Visible = false;
                    btnsuspend.Visible = true;
                    btnauthorize.Visible = false;
                    txtLoginCode.Enabled = false;
                    BtnReport.Visible = true;
                }
                else if (Request.QueryString["Flag"].ToString() == "AT")
                {
                    div_main.Visible = true;
                    div_view.Visible = false;
                    BindGrid();
                    btncreate.Visible = false;
                    btnmodify.Visible = false;
                    btnsuspend.Visible = false;
                    btnauthorize.Visible = true;
                    txtLoginCode.Enabled = false;
                    BtnReport.Visible = true;
                }
                else if (Request.QueryString["Flag"].ToString() == "VW")
                {
                    div_main.Visible = false;
                    div_view.Visible = true;
                    BtnReport.Visible = true;
                }
                DdlBranchName.Focus();
                BD.BINDUSERGRP(ddusergroup);
            }
            else
            {
                Response.Redirect("~/FrmBlank.aspx?ShowMessage=true");
            }
        }
    }

    #endregion

    #region Text Change Event

    protected void txtLoginCode_TextChanged(object sender, EventArgs e)
    {
        DataTable DT = new DataTable();
        DT = clsa.txtChangeUM(txtBranchCode.Text, Session["BRCD"].ToString(), txtLoginCode.Text);

        if (DT.Rows.Count > 0)
        {
            DdlBranchName.SelectedValue = DT.Rows[0]["BRCD"].ToString();
            txtBranchCode.Text = DT.Rows[0]["BRCD"].ToString();
            txtUserName.Text = DT.Rows[0]["USERNAME"].ToString();
            txtLoginCode.Text = DT.Rows[0]["LOGINCODE"].ToString();
            ddusergroup.SelectedIndex = Convert.ToInt32(DT.Rows[0]["USERGROUP"]);
            if (DT.Rows[0]["AUTOLOCK"].ToString() == "Y")
                ddautolock.SelectedValue = "Y";
            else
                ddautolock.SelectedValue = "N";

            if (DT.Rows[0]["MULTIBRANCH"].ToString() == "Y")
                ddMultibranchaccess.SelectedValue = "Y";
            else
                ddMultibranchaccess.SelectedValue = "N";

            txtmobile.Text = DT.Rows[0]["USERMOBILENO"].ToString();
            txtpermission.Text = DT.Rows[0]["PERMISSIONNO"].ToString();

            if (DT.Rows[0]["CSCR"].ToString() == "Y")
            {
                chkCashCr.Checked = true;
                txtCSCredit.Enabled = true;
                txtCSCredit.Text = DT.Rows[0]["CSCRL"].ToString();
            }
            if (DT.Rows[0]["CSDR"].ToString() == "Y")
            {
                chkCashDr.Checked = true;
                txtCSDebit.Enabled = true;
                txtCSDebit.Text = DT.Rows[0]["CSDRL"].ToString();
            }
            if (DT.Rows[0]["TRFCR"].ToString() == "Y")
            {
                chkTrfCr.Checked = true;
                txttrfCredit.Enabled = true;
                txttrfCredit.Text = DT.Rows[0]["TRFCRL"].ToString();
            }
            if (DT.Rows[0]["TRFDR"].ToString() == "Y")
            {
                chkTrfDr.Checked = true;
                txttrfDebit.Enabled = true;
                txttrfDebit.Text = DT.Rows[0]["TRFDRL"].ToString();
            }
            if (DT.Rows[0]["RTGSCR"].ToString() == "Y")
            {
                chkRTGSCr.Checked = true;
                txtrtgsCredit.Enabled = true;
                txtrtgsCredit.Text = DT.Rows[0]["RTGSCRL"].ToString();
            }
            if (DT.Rows[0]["RTGSDR"].ToString() == "Y")
            {
                chkRTGSDr.Checked = true;
                txtrtgsDebit.Enabled = true;
                txtrtgsDebit.Text = DT.Rows[0]["RTGSDRL"].ToString();
            }
            if (DT.Rows[0]["IWCR"].ToString() == "Y")
            {
                chkInwardCr.Checked = true;
                txtIWCredit.Enabled = true;
                txtIWCredit.Text = DT.Rows[0]["IWCRL"].ToString();
            }
            if (DT.Rows[0]["IWDR"].ToString() == "Y")
            {
                chkInwardDr.Checked = true;
                txtIWDebit.Enabled = true;
                txtIWDebit.Text = DT.Rows[0]["IWDRL"].ToString();
            }
            if (DT.Rows[0]["OWCR"].ToString() == "Y")
            {
                chkOWCr.Checked = true;
                txtOWCredit.Enabled = true;
                txtOWCredit.Text = DT.Rows[0]["OWCRL"].ToString();
            }
            if (DT.Rows[0]["OWDR"].ToString() == "Y")
            {
                chkOWDr.Checked = true;
                txtOWDebit.Enabled = true;
                txtOWDebit.Text = DT.Rows[0]["OWDRL"].ToString();
            }
            if (DT.Rows[0]["ABBCR"].ToString() == "Y")
            {
                chkABBCr.Checked = true;
                txtABBCredit.Enabled = true;
                txtABBCredit.Text = DT.Rows[0]["ABBCRL"].ToString();
            }
            if (DT.Rows[0]["ABBDR"].ToString() == "Y")
            {
                chkABBDr.Checked = true;
                txtABBDebit.Enabled = true;
                txtABBDebit.Text = DT.Rows[0]["ABBDRL"].ToString();
            }
            if (DT.Rows[0]["IBTCR"].ToString() == "Y")
            {
                chkIBTCr.Checked = true;
                txtIBTCredit.Enabled = true;
                txtIBTCredit.Text = DT.Rows[0]["IBTCRL"].ToString();
            }
            if (DT.Rows[0]["IBTDR"].ToString() == "Y")
            {
                chkIBTDr.Checked = true;
                txtIBTDebit.Enabled = true;
                txtIBTDebit.Text = DT.Rows[0]["IBTDRL"].ToString();
            }

            btncreate.Enabled = false;
            lblMessage.Text = "Allready Exists.. Please Choose Different LoginCode...!";
            ModalPopup.Show(this.Page);
            return;
        }
        else
        {
            txtmobile.Text = "";
            ddusergroup.SelectedIndex = 0;
            ddautolock.SelectedIndex = 0;
            ddMultibranchaccess.SelectedIndex = 0;
            chkCashCr.Checked = false;
            chkTrfCr.Checked = false;
            chkRTGSCr.Checked = false;
            chkInwardCr.Checked = false;
            chkOWCr.Checked = false;
            chkABBCr.Checked = false;
            chkIBTCr.Checked = false;

            chkCashDr.Checked = false;
            chkTrfDr.Checked = false;
            chkRTGSDr.Checked = false;
            chkInwardDr.Checked = false;
            chkOWDr.Checked = false;
            chkABBDr.Checked = false;
            chkIBTDr.Checked = false;
            txtCSCredit.Enabled = false;
            txtCSDebit.Enabled = false;
            txttrfCredit.Enabled = false;
            txttrfDebit.Enabled = false;
            txtrtgsCredit.Enabled = false;
            txtrtgsDebit.Enabled = false;
            txtIWCredit.Enabled = false;
            txtIWDebit.Enabled = false;
            txtOWCredit.Enabled = false;
            txtOWDebit.Enabled = false;
            txtABBCredit.Enabled = false;
            txtABBDebit.Enabled = false;
            txtIBTCredit.Enabled = false;
            txtIBTDebit.Enabled = false;

            txtCSCredit.Text = "";
            txtCSDebit.Text = "";
            txttrfCredit.Text = "";
            txttrfDebit.Text = "";
            txtrtgsCredit.Text = "";
            txtrtgsDebit.Text = "";
            txtIWCredit.Text = "";
            txtIWDebit.Text = "";
            txtOWCredit.Text = "";
            txtOWDebit.Text = "";
            txtABBCredit.Text = "";
            txtABBDebit.Text = "";
            txtIBTCredit.Text = "";
            txtIBTDebit.Text = "";

            btncreate.Enabled = true;
            ddusergroup.Focus();
        }
    }

    #endregion

    #region Public Function

    public void BindGrid()
    {
        int Result;
        Result = clsa.BindGrid(grduDetails, Session["BRCD"].ToString());
    }

    public void callData()
    {
        try
        {
            CheckStage();
            if (ViewState["Flag"].ToString() != "AD")
            {
                DataTable DT = new DataTable();
                DT = clsa.GetInfo(ViewState["id"].ToString());
                if (DT.Rows.Count > 0)
                {
                    if (DT.Rows[0]["PERMISSIONNO"].ToString() != Session["MID"].ToString())
                    {
                        if (DT.Rows[0]["MIDCODE"].ToString() != Session["MID"].ToString())
                        {
                            txtBranchCode.Text = DT.Rows[0]["BRCD"].ToString();
                            DdlBranchName.SelectedValue = DT.Rows[0]["BRCD"].ToString();
                            txtUserName.Text = DT.Rows[0]["USERNAME"].ToString();
                            txtLoginCode.Text = DT.Rows[0]["LOGINCODE"].ToString();
                            ddusergroup.SelectedIndex = Convert.ToInt32(DT.Rows[0]["USERGROUP"]);
                            if (DT.Rows[0]["AUTOLOCK"].ToString() == "Y")
                                ddautolock.SelectedValue = "Y";
                            else
                                ddautolock.SelectedValue = "N";

                            if (DT.Rows[0]["MULTIBRANCH"].ToString() == "Y")
                                ddMultibranchaccess.SelectedValue = "Y";
                            else
                                ddMultibranchaccess.SelectedValue = "N";

                            txtmobile.Text = DT.Rows[0]["USERMOBILENO"].ToString();
                            txtpermission.Text = DT.Rows[0]["PERMISSIONNO"].ToString();

                            if (DT.Rows[0]["CSCR"].ToString() == "Y")
                            {
                                chkCashCr.Checked = true;
                                txtCSCredit.Enabled = true;
                                txtCSCredit.Text = DT.Rows[0]["CSCRL"].ToString();
                            }
                            if (DT.Rows[0]["CSDR"].ToString() == "Y")
                            {
                                chkCashDr.Checked = true;
                                txtCSDebit.Enabled = true;
                                txtCSDebit.Text = DT.Rows[0]["CSDRL"].ToString();
                            }
                            if (DT.Rows[0]["TRFCR"].ToString() == "Y")
                            {
                                chkTrfCr.Checked = true;
                                txttrfCredit.Enabled = true;
                                txttrfCredit.Text = DT.Rows[0]["TRFCRL"].ToString();
                            }
                            if (DT.Rows[0]["TRFDR"].ToString() == "Y")
                            {
                                chkTrfDr.Checked = true;
                                txttrfDebit.Enabled = true;
                                txttrfDebit.Text = DT.Rows[0]["TRFDRL"].ToString();
                            }
                            if (DT.Rows[0]["RTGSCR"].ToString() == "Y")
                            {
                                chkRTGSCr.Checked = true;
                                txtrtgsCredit.Enabled = true;
                                txtrtgsCredit.Text = DT.Rows[0]["RTGSCRL"].ToString();
                            }
                            if (DT.Rows[0]["RTGSDR"].ToString() == "Y")
                            {
                                chkTrfDr.Checked = true;
                                txtrtgsDebit.Enabled = true;
                                txtrtgsDebit.Text = DT.Rows[0]["RTGSDRL"].ToString();
                            }
                            if (DT.Rows[0]["IWCR"].ToString() == "Y")
                            {
                                chkInwardCr.Checked = true;
                                txtIWCredit.Enabled = true;
                                txtIWCredit.Text = DT.Rows[0]["IWCRL"].ToString();
                            }
                            if (DT.Rows[0]["IWDR"].ToString() == "Y")
                            {
                                chkInwardDr.Checked = true;
                                txtIWDebit.Enabled = true;
                                txtIWDebit.Text = DT.Rows[0]["IWDRL"].ToString();
                            }
                            if (DT.Rows[0]["OWCR"].ToString() == "Y")
                            {
                                chkOWCr.Checked = true;
                                txtOWCredit.Enabled = true;
                                txtOWCredit.Text = DT.Rows[0]["OWCRL"].ToString();
                            }
                            if (DT.Rows[0]["OWDR"].ToString() == "Y")
                            {
                                chkOWDr.Checked = true;
                                txtOWDebit.Enabled = true;
                                txtOWDebit.Text = DT.Rows[0]["OWDRL"].ToString();
                            }
                            if (DT.Rows[0]["ABBCR"].ToString() == "Y")
                            {
                                chkABBCr.Checked = true;
                                txtABBCredit.Enabled = true;
                                txtABBCredit.Text = DT.Rows[0]["ABBCRL"].ToString();
                            }
                            if (DT.Rows[0]["ABBDR"].ToString() == "Y")
                            {
                                chkABBDr.Checked = true;
                                txtABBDebit.Enabled = true;
                                txtABBDebit.Text = DT.Rows[0]["ABBDRL"].ToString();
                            }
                            if (DT.Rows[0]["IBTCR"].ToString() == "Y")
                            {
                                chkIBTCr.Checked = true;
                                txtIBTCredit.Enabled = true;
                                txtIBTCredit.Text = DT.Rows[0]["IBTCRL"].ToString();
                            }
                            if (DT.Rows[0]["IBTDR"].ToString() == "Y")
                            {
                                chkIBTDr.Checked = true;
                                txtIBTDebit.Enabled = true;
                                txtIBTDebit.Text = DT.Rows[0]["IBTDRL"].ToString();
                            }
                        }
                        else
                        {
                            ViewState["id"] = "0";
                            lblMessage.Text = "Not allow for same user...!!";
                            ModalPopup.Show(this.Page);
                            return;
                        }
                    }
                    else
                    {
                        ViewState["id"] = "0";
                        lblMessage.Text = "Not allow to self modify...!";
                        ModalPopup.Show(this.Page);
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

    public void CheckStage()
    {
        try
        {
            string ST = clsa.GetStage(ViewState["id"].ToString());
            if (ST == "1003")
            {
                WebMsgBox.Show("Record Already Authorize......!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void clear()
    {
        txtUserName.Text = "";
        txtmobile.Text = "";
        txtpermission.Text = "";
        txtBranchCode.Text = "";
        txtLoginCode.Text = "";
        ddusergroup.SelectedIndex = 0;
        ddautolock.SelectedIndex = 0;
        ddMultibranchaccess.SelectedIndex = 0;
        chkCashCr.Checked = false;
        chkCashDr.Checked = false;
        chkTrfCr.Checked = false;
        chkTrfDr.Checked = false;
        chkRTGSCr.Checked = false;
        chkRTGSDr.Checked = false;
        chkInwardCr.Checked = false;
        chkInwardDr.Checked = false;
        chkOWCr.Checked = false;
        chkOWDr.Checked = false;
        chkABBCr.Checked = false;
        chkABBDr.Checked = false;
        chkIBTCr.Checked = false;
        chkIBTDr.Checked = false;
        txtCSCredit.Enabled = false;
        txtCSDebit.Enabled = false;
        txttrfCredit.Enabled = false;
        txttrfDebit.Enabled = false;
        txtrtgsCredit.Enabled = false;
        txtrtgsDebit.Enabled = false;
        txtIWCredit.Enabled = false;
        txtIWDebit.Enabled = false;
        txtOWCredit.Enabled = false;
        txtOWDebit.Enabled = false;
        txtABBCredit.Enabled = false;
        txtABBDebit.Enabled = false;
        txtIBTCredit.Enabled = false;
        txtIBTDebit.Enabled = false;

        txtCSCredit.Text = "";
        txtCSDebit.Text = "";
        txttrfCredit.Text = "";
        txttrfDebit.Text = "";
        txtrtgsCredit.Text = "";
        txtrtgsDebit.Text = "";
        txtIWCredit.Text = "";
        txtIWDebit.Text = "";
        txtOWCredit.Text = "";
        txtOWDebit.Text = "";
        txtABBCredit.Text = "";
        txtABBDebit.Text = "";
        txtIBTCredit.Text = "";
        txtIBTDebit.Text = "";
    }

    #endregion

    #region Index Changed Events

    protected void DdlBranchName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtBranchCode.Text = DdlBranchName.SelectedValue.ToString();
            txtUserName.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grduDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grduDetails.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void chkCashCr_CheckedChanged(object sender, EventArgs e)
    {
        txtCSCredit.Enabled = true;
    }

    protected void chkCashDr_CheckedChanged(object sender, EventArgs e)
    {
        txtCSDebit.Enabled = true;
    }

    protected void chkTrfCr_CheckedChanged(object sender, EventArgs e)
    {
        txttrfCredit.Enabled = true;
    }

    protected void chkTrfDr_CheckedChanged(object sender, EventArgs e)
    {
        txttrfDebit.Enabled = true;
    }

    protected void chkRTGSCr_CheckedChanged(object sender, EventArgs e)
    {
        txtrtgsCredit.Enabled = true;
    }

    protected void chkRTGSDr_CheckedChanged(object sender, EventArgs e)
    {
        txtrtgsDebit.Enabled = true;
    }

    protected void chkInwardCr_CheckedChanged(object sender, EventArgs e)
    {
        txtIWCredit.Enabled = true;
    }

    protected void chkInwardDr_CheckedChanged(object sender, EventArgs e)
    {
        txtIWDebit.Enabled = true;
    }

    protected void chkABBCr_CheckedChanged(object sender, EventArgs e)
    {
        txtABBCredit.Enabled = true;
    }

    protected void chkABBDr_CheckedChanged(object sender, EventArgs e)
    {
        txtABBDebit.Enabled = true;
    }

    protected void chkOWCr_CheckedChanged(object sender, EventArgs e)
    {
        txtOWCredit.Enabled = true;
    }

    protected void chkOWDr_CheckedChanged(object sender, EventArgs e)
    {
        txtOWDebit.Enabled = true;
    }

    protected void chkIBTCr_CheckedChanged(object sender, EventArgs e)
    {
        txtIBTCredit.Enabled = true;
    }

    protected void chkIBTDr_CheckedChanged(object sender, EventArgs e)
    {
        txtIBTDebit.Enabled = true;

    }

    #endregion

    #region Click Event

    protected void btncreate_Click(object sender, EventArgs e)
    {
        string CSCB, TRFCB, RTGSCB, INWCB, OWCB, ABBCB, IBTCB;
        string CSDB, TRFDB, RTGSDB, INWDB, OWDB, ABBDB, IBTDB;
        if (txtmobile.Text == "" && Convert.ToInt64(txtmobile.Text.Length) < 10)
        {
            WebMsgBox.Show("Please enter valid mobile number..!!", this.Page);
            return;
        }
        if (txtBranchCode.Text == "")
        {
            WebMsgBox.Show("Please Enter Branch Code....!!", this.Page);
            return;
        }
        if (txtUserName.Text == "")
        {
            WebMsgBox.Show("Please Enter username....!!", this.Page);
            return;
        }
        if (txtLoginCode.Text == "")
        {
            WebMsgBox.Show("Please Enter logincode....!!", this.Page);
            return;
        }
        if (chkCashCr.Checked)
        {
            if (txtCSCredit.Text == "")
            {
                lblMessage.Text = "Enter Cash Credit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            CSCB = "Y";
        }
        else
        {
            CSCB = "N";
            txtCSCredit.Text = Convert.ToString(0);
        }

        if (chkCashDr.Checked)
        {
            if (txtCSDebit.Text == "")
            {
                lblMessage.Text = "Enter Cash Debit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            CSDB = "Y";
        }
        else
        {
            CSDB = "N";
            txtCSDebit.Text = Convert.ToString(0);
        }

        if (chkTrfCr.Checked)
        {
            if (txttrfCredit.Text == "")
            {
                lblMessage.Text = "Enter Transfer Credit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            TRFCB = "Y";
        }
        else
        {
            TRFCB = "N";
            txttrfCredit.Text = Convert.ToString(0);
        }

        if (chkTrfDr.Checked)
        {
            if (txttrfDebit.Text == "")
            {
                lblMessage.Text = "Enter Transfer Debit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            TRFDB = "Y";
        }
        else
        {
            TRFDB = "N";
            txttrfDebit.Text = Convert.ToString(0);
        }

        if (chkRTGSCr.Checked)
        {
            if (txtrtgsCredit.Text == "")
            {
                lblMessage.Text = "Enter RTGS Credit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            RTGSCB = "Y";
        }
        else
        {
            RTGSCB = "N";
            txtrtgsCredit.Text = Convert.ToString(0);
        }

        if (chkRTGSDr.Checked)
        {
            if (txtrtgsDebit.Text == "")
            {
                lblMessage.Text = "Enter RTGS Debit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            RTGSDB = "Y";
        }
        else
        {
            RTGSDB = "N";
            txtrtgsDebit.Text = Convert.ToString(0);
        }

        if (chkInwardCr.Checked)
        {
            if (txtIWCredit.Text == "")
            {
                lblMessage.Text = "Enter Inward Credit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            INWCB = "Y";
        }
        else
        {
            INWCB = "N";
            txtIWCredit.Text = Convert.ToString(0);
        }

        if (chkInwardDr.Checked)
        {
            if (txtIWDebit.Text == "")
            {
                lblMessage.Text = "Enter Inward Debit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            INWDB = "Y";
        }
        else
        {
            INWDB = "N";
            txtIWDebit.Text = Convert.ToString(0);
        }

        if (chkOWCr.Checked)
        {
            if (txtOWCredit.Text == "")
            {
                lblMessage.Text = "Enter Outward Credit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            OWCB = "Y";
        }
        else
        {
            OWCB = "N";
            txtOWCredit.Text = Convert.ToString(0);
        }

        if (chkOWDr.Checked)
        {
            if (txtOWDebit.Text == "")
            {
                lblMessage.Text = "Enter Outward Debit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            OWDB = "Y";
        }
        else
        {
            OWDB = "N";
            txtOWDebit.Text = Convert.ToString(0);
        }

        if (chkABBCr.Checked)
        {
            if (txtABBCredit.Text == "")
            {
                lblMessage.Text = "Enter ABB Credit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            ABBCB = "Y";
        }
        else
        {
            ABBCB = "N";
            txtABBCredit.Text = Convert.ToString(0);
        }

        if (chkABBDr.Checked)
        {
            if (txtABBDebit.Text == "")
            {
                lblMessage.Text = "Enter ABB Debit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            ABBDB = "Y";
        }
        else
        {
            ABBDB = "N";
            txtABBDebit.Text = Convert.ToString(0);
        }

        if (chkIBTCr.Checked)
        {
            if (txtIBTCredit.Text == "")
            {
                lblMessage.Text = "Enter IBT Credit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            IBTCB = "Y";
        }
        else
        {
            IBTCB = "N";
            txtIBTCredit.Text = Convert.ToString(0);
        }

        if (chkIBTDr.Checked)
        {
            if (txtIBTDebit.Text == "")
            {
                lblMessage.Text = "Enter IBT Debit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            IBTDB = "Y";
        }
        else
        {
            IBTDB = "N";
            txtIBTDebit.Text = Convert.ToString(0);
        }

        string EX = clsa.ChkUsrExists(txtBranchCode.Text.Trim().ToString(), txtLoginCode.Text);
        if (EX != null || EX != "")
        {
            int cnt = clsa.submitUserMaster(txtUserName.Text, txtLoginCode.Text, ddMultibranchaccess.SelectedValue.ToString(), ddautolock.SelectedValue.ToString(), txtmobile.Text,
                txtpermission.Text, txtBranchCode.Text.Trim().ToString(), Session["MID"].ToString(), ddusergroup.SelectedValue.ToString(), CSCB, txtCSCredit.Text, CSDB, txtCSDebit.Text,
                TRFCB, txttrfCredit.Text, TRFDB, txttrfDebit.Text, RTGSCB, txtrtgsCredit.Text, RTGSDB, txtrtgsDebit.Text, INWCB, txtIWCredit.Text, INWDB, txtIWDebit.Text,
                OWCB, txtOWCredit.Text, OWDB, txtOWDebit.Text, ABBCB, txtABBCredit.Text, ABBDB, txtABBDebit.Text, IBTCB, txtIBTCredit.Text, IBTDB, txtIBTDebit.Text);

            if (cnt > 0)
            {
                lblMessage.Text = "User Created Successfully With UserName : " + txtLoginCode.Text.Trim().ToString() + "...!";
                ModalPopup.Show(this.Page);
                clear();
                FL = "Insert";
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "User_Add _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                return;
            }
        }
        else
        {
            lblMessage.Text = "Login Code Allready Exists.. Please Choose Different...!";
            ModalPopup.Show(this.Page);
            return;
        }
    }

    protected void btnmodify_Click(object sender, EventArgs e)
    {
        string CSCB, TRFCB, RTGSCB, INWCB, OWCB, ABBCB, IBTCB;
        string CSDB, TRFDB, RTGSDB, INWDB, OWDB, ABBDB, IBTDB;
        string[] id = ViewState["id"].ToString().Split('_');

        if (chkCashCr.Checked)
        {
            if (txtCSCredit.Text == "")
            {
                lblMessage.Text = "Enter Cash Credit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            CSCB = "Y";
        }
        else
        {
            CSCB = "N";
            txtCSCredit.Text = Convert.ToString(0);
        }

        if (chkCashDr.Checked)
        {
            if (txtCSDebit.Text == "")
            {
                lblMessage.Text = "Enter Cash Debit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            CSDB = "Y";
        }
        else
        {
            CSDB = "N";
            txtCSDebit.Text = Convert.ToString(0);
        }

        if (chkTrfCr.Checked)
        {
            if (txttrfCredit.Text == "")
            {
                lblMessage.Text = "Enter Transfer Credit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            TRFCB = "Y";
        }
        else
        {
            TRFCB = "N";
            txttrfCredit.Text = Convert.ToString(0);
        }

        if (chkTrfDr.Checked)
        {
            if (txttrfDebit.Text == "")
            {
                lblMessage.Text = "Enter Transfer Debit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            TRFDB = "Y";
        }
        else
        {
            TRFDB = "N";
            txttrfDebit.Text = Convert.ToString(0);
        }

        if (chkRTGSCr.Checked)
        {
            if (txtrtgsCredit.Text == "")
            {
                lblMessage.Text = "Enter RTGS Credit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            RTGSCB = "Y";
        }
        else
        {
            RTGSCB = "N";
            txtrtgsCredit.Text = Convert.ToString(0);
        }

        if (chkRTGSDr.Checked)
        {
            if (txtrtgsDebit.Text == "")
            {
                lblMessage.Text = "Enter RTGS Debit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            RTGSDB = "Y";
        }
        else
        {
            RTGSDB = "N";
            txtrtgsDebit.Text = Convert.ToString(0);
        }

        if (chkInwardCr.Checked)
        {
            if (txtIWCredit.Text == "")
            {
                lblMessage.Text = "Enter Inward Credit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            INWCB = "Y";
        }
        else
        {
            INWCB = "N";
            txtIWCredit.Text = Convert.ToString(0);
        }

        if (chkInwardDr.Checked)
        {
            if (txtIWDebit.Text == "")
            {
                lblMessage.Text = "Enter Inward Debit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            INWDB = "Y";
        }
        else
        {
            INWDB = "N";
            txtIWDebit.Text = Convert.ToString(0);
        }

        if (chkOWCr.Checked)
        {
            if (txtOWCredit.Text == "")
            {
                lblMessage.Text = "Enter Outward Credit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            OWCB = "Y";
        }
        else
        {
            OWCB = "N";
            txtOWCredit.Text = Convert.ToString(0);
        }

        if (chkOWDr.Checked)
        {
            if (txtOWDebit.Text == "")
            {
                lblMessage.Text = "Enter Outward Debit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            OWDB = "Y";
        }
        else
        {
            OWDB = "N";
            txtOWDebit.Text = Convert.ToString(0);
        }

        if (chkABBCr.Checked)
        {
            if (txtABBCredit.Text == "")
            {
                lblMessage.Text = "Enter ABB Credit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            ABBCB = "Y";
        }
        else
        {
            ABBCB = "N";
            txtABBCredit.Text = Convert.ToString(0);
        }

        if (chkABBDr.Checked)
        {
            if (txtABBDebit.Text == "")
            {
                lblMessage.Text = "Enter ABB Debit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            ABBDB = "Y";
        }
        else
        {
            ABBDB = "N";
            txtABBDebit.Text = Convert.ToString(0);
        }

        if (chkIBTCr.Checked)
        {
            if (txtIBTCredit.Text == "")
            {
                lblMessage.Text = "Enter IBT Credit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            IBTCB = "Y";
        }
        else
        {
            IBTCB = "N";
            txtIBTCredit.Text = Convert.ToString(0);
        }

        if (chkIBTDr.Checked)
        {
            if (txtIBTDebit.Text == "")
            {
                lblMessage.Text = "Enter IBT Debit Limit...!";
                ModalPopup.Show(this.Page);
                return;
            }
            IBTDB = "Y";
        }
        else
        {
            IBTDB = "N";
            txtIBTDebit.Text = Convert.ToString(0);
        }

        if (Convert.ToDouble(id[0].ToString()) > 0)
        {
            int cnt = 0;
            if (Session["UGRP"].ToString() == "1" || Session["UGRP"].ToString() == "2" || Session["UGRP"].ToString() == "3")
            {
                cnt = clsa.Modifyusermaster(ViewState["id"].ToString(), txtBranchCode.Text, txtUserName.Text, txtLoginCode.Text, ddusergroup.SelectedValue.ToString(), ddautolock.SelectedValue.ToString(),
                  ddMultibranchaccess.SelectedValue.ToString(), txtmobile.Text, txtpermission.Text, CSCB, txtCSCredit.Text, CSDB, txtCSDebit.Text,
                  TRFCB, txttrfCredit.Text, TRFDB, txttrfDebit.Text, RTGSCB, txtrtgsCredit.Text, RTGSDB, txtrtgsDebit.Text, INWCB, txtIWCredit.Text, INWDB, txtIWDebit.Text,
                  OWCB, txtOWCredit.Text, OWDB, txtOWDebit.Text, ABBCB, txtABBCredit.Text, ABBDB, txtABBDebit.Text, IBTCB, txtIBTCredit.Text, IBTDB, txtIBTDebit.Text,Session["MID"].ToString());

                if (cnt > 0)
                {
                    BindGrid();
                    clear();
                    lblMessage.Text = "User Modified Successfully...!";
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "User_Modify _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }
            }
            else
            {
                lblMessage.Text = "Only Admin or Manager User can modify User Details...!";
                ModalPopup.Show(this.Page);
            }
        }
    }

    protected void btnsuspend_Click(object sender, EventArgs e)
    {
        string[] id = ViewState["id"].ToString().Split('_');

        if (Convert.ToDouble(id[0].ToString()) > 0)
        {
            int cnt = 0;
            if (Session["UGRP"].ToString() == "1" || Session["UGRP"].ToString() == "2" || Session["UGRP"].ToString() == "3")
            {
                cnt = clsa.Suspendusermaster(ViewState["id"].ToString(),Session["MID"].ToString());
                if (cnt > 0)
                {
                    BindGrid();
                    clear();
                    lblMessage.Text = "User Deleted Successfully...!";
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "User_Deleted _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                }
            }
            else
            {
                lblMessage.Text = "Only Admin or Manager User can Delete User Details...!";
                ModalPopup.Show(this.Page);
            }
        }
    }

    protected void btnauthorize_Click(object sender, EventArgs e)
    {
        string[] id = ViewState["id"].ToString().Split('_');

        if (Convert.ToDouble(id[0].ToString()) > 0)
        {
            int cnt = clsa.authorizeusermaster(ViewState["id"].ToString(), Session["MID"].ToString());

            if (cnt > 0)
            {
                BindGrid();
                clear();
                lblMessage.Text = "User Authorize Successfully...!";
                ModalPopup.Show(this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "User_Authorized _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
        }
    }

    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Flag"].ToString() != "AD")
            {
                LinkButton objlink = (LinkButton)sender;
                string id = objlink.CommandArgument;
                ViewState["id"] = id;
                callData();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region View
    protected void TxtPermNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtPermNo.Text == "")
            {
                clsa.GetAllDataPerm(grduDetails, Session["BRCD"].ToString(), TxtPermNo.Text);
                grduDetails.Columns[0].Visible = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtUname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtUname.Text == "")
            {
                clsa.GetAllDataName(grduDetails, Session["BRCD"].ToString(), TxtUname.Text);
                grduDetails.Columns[0].Visible = false;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    #endregion
    protected void BtnReport_Click(object sender, EventArgs e)
    {
        try
        {
              string redirectURL = "FrmRView.aspx?BRCD=" +Session["BRCD"].ToString()+ "&MID=" +Session["MID"].ToString() + "&rptname=RptUserReportAll.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}