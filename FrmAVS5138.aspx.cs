using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using Microsoft.ReportingServices;
using System.Data.SqlClient;

public partial class FrmAVS5138 : System.Web.UI.Page
{
    ClsAuthoriseCommon comn = new ClsAuthoriseCommon();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsGenrateAVSBTable TC = new ClsGenrateAVSBTable();
    ClsVoucherAutho VA = new ClsVoucherAutho();
    DbConnection conn = new DbConnection();
    ClsAVS5138 TV = new ClsAVS5138();
    ClsCommon cmn = new ClsCommon();
    DataTable DRCR = new DataTable();
    DataTable DT = new DataTable();
    string[] FromDate, ToDate;
    string Message = "";
    int Result = 0;

    #region PageLoad

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            if (!IsPostBack)
            {
                //Only admin will have the access to view the form
                if ((Session["UGRP"].ToString() != "1"))
                {
                    HttpContext.Current.Response.Redirect("FrmBlank.aspx", false);
                }
                else
                {
                    TallyDate.Value = cmn.GetLastTallyDate().ToString();
                    WorkingDate.Value = Session["EntryDate"].ToString();
                    txtEDate.Text = Session["EntryDate"].ToString();
                    txtEDate.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Functions

    public void DebitCredit()
    {
        DRCR = new DataTable();
        try
        {
            DRCR = TV.GetDrCr(Session["BRCD"].ToString(), txtEDate.Text.ToString(), txtSetNo.Text.ToString(), "CRDR");
            if (DRCR.Rows.Count > 0)
            {
                txtTotalDebit.Text = DRCR.Rows[0]["TotalDebit"].ToString();
                txtTotalCredit.Text = DRCR.Rows[0]["TotalCredit"].ToString();

                if (Convert.ToDouble(txtTotalDebit.Text.ToString()) == Convert.ToDouble(txtTotalCredit.Text.ToString()))
                {
                    lblStatus.Text = "Voucher is tally";
                    lblStatus.Font.Size = 15;
                    lblStatus.ForeColor = Color.Green;
                    lblStatus.Font.Name = "Verdana";
                }
                else
                {
                    lblStatus.Text = "Voucher is not tally";
                    lblStatus.Font.Size = 15;
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.Font.Name = "Verdana";
                }
            }
            else
            {
                txtTotalDebit.Text = "0";
                txtTotalCredit.Text = "0";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindGridVoucher()
    {
        DT = new DataTable();
        try
        {
            DT = TV.GetVoucher(Session["BRCD"].ToString(), txtEDate.Text.ToString(), txtSetNo.Text.ToString(), "ALL");
            if (DT.Rows.Count > 0)
            {
                ClearUpdate();
                txtEDate.Enabled = false;
                txtSetNo.Enabled = false;
                divDetailInfo.Visible = false;
                grdVoucher.DataSource = DT;
                grdVoucher.DataBind();

                //  Bind unauthorize voucher
                TV.BindVoucher(grdAuthorize, Session["BRCD"].ToString(), txtEDate.Text.ToString(), txtSetNo.Text.ToString(), "UALL");
            }
            else
            {
                ClearUpdate();
                txtEDate.Enabled = true;
                txtSetNo.Enabled = true;
                divDetailInfo.Visible = false;

                grdVoucher.DataSource = null;
                grdVoucher.DataBind();

                grdAuthorize.DataSource = null;
                grdAuthorize.DataBind();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindCancelReason(DropDownList ddlBrName)
    {
        try
        {
            comn.BindReason(ddlBrName);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void CreateAVSB(string sFlag)
    {
        try
        {
            if (sFlag.ToString() == "DL")
            {
                FromDate = txtEDate.Text.ToString().Split('/');
                ToDate = txtEDate.Text.ToString().Split('/');
            }
            else if (sFlag.ToString() == "AT")
            {
                FromDate = txtEDate.Text.ToString().Split('/');
                ToDate = Session["EntryDate"].ToString().Split('/');
            }
            else if (sFlag.ToString() == "TRF")
            {
                DateTime FDate = Convert.ToDateTime(conn.ConvertDate(txtTrfDate.Text.ToString()).ToString()).Date;
                DateTime TDate = Convert.ToDateTime(conn.ConvertDate(Session["EntryDate"].ToString()).ToString()).Date;

                if (FDate < TDate)
                {
                    FromDate = txtEDate.Text.ToString().Split('/');
                    ToDate = Session["EntryDate"].ToString().Split('/');
                }
                else if (FDate > TDate)
                {
                    FromDate = Session["EntryDate"].ToString().Split('/');
                    ToDate = txtEDate.Text.ToString().Split('/');
                }
            }
            //Create avsb table 
            TC.GenrateTable(Session["BRCD"].ToString().ToString(), FromDate[1].ToString(), ToDate[1].ToString(), FromDate[2].ToString(), ToDate[2].ToString(), Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearUpdate()
    {
        try
        {
            txtGlCode.Text = "";
            txtPrCode.Text = "";
            txtAccNo.Text = "";
            txtParti1.Text = "";
            txtAmount.Text = "";
            txtTrxType.Text = "";
            txtActivity.Text = "";
            txtPmtMode.Text = "";
            txtInstNo.Text = "";
            txtInstDate.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearAllData()
    {
        try
        {
            txtEDate.Text = Session["EntryDate"].ToString();
            txtSetNo.Text = "";
            txtTotalDebit.Text = "";
            txtTotalCredit.Text = "";
            lblStatus.Text = "";

            txtGlCode.Text = "";
            txtPrCode.Text = "";
            txtAccNo.Text = "";
            txtParti1.Text = "";
            txtAmount.Text = "";
            txtTrxType.Text = "";
            txtActivity.Text = "";
            txtPmtMode.Text = "";
            txtInstNo.Text = "";
            txtInstDate.Text = "";

            txtEDate.Enabled = true;
            txtSetNo.Enabled = true;
            btnModify.Visible = false;
            btnInsert.Visible = false;

            divDetailInfo.Visible = false;
            grdVoucher.DataSource = null;
            grdVoucher.DataBind();

            grdAuthorize.DataSource = null;
            grdAuthorize.DataBind();

            txtEDate.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Text Change

    protected void txtSetNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DebitCredit();

            btnDisplay.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Click Event

    protected void btnDisplay_Click(object sender, EventArgs e)
    {
        try
        {
            BindGridVoucher();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkModify_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            ViewState["ScrollNo"] = lnkedit.CommandArgument.ToString();

            if (Convert.ToDouble(ViewState["ScrollNo"].ToString()) > 0)
            {
                DT = TV.GetVoucherScroll(Session["BRCD"].ToString(), txtEDate.Text.ToString(), txtSetNo.Text.ToString(), ViewState["ScrollNo"].ToString(), "SINGLE");
                if (DT.Rows.Count > 0)
                {
                    btnModify.Visible = true;
                    btnInsert.Visible = true;
                    divDetailInfo.Visible = true;

                    ViewState["OldPrCode"] = DT.Rows[0]["SubGlCode"].ToString();
                    ViewState["OldAccNo"] = DT.Rows[0]["AccNo"].ToString();
                    ViewState["OldAmount"] = DT.Rows[0]["Amount"].ToString();

                    txtGlCode.Text = DT.Rows[0]["GlCode"].ToString();
                    txtPrCode.Text = DT.Rows[0]["SubGlCode"].ToString();
                    txtAccNo.Text = DT.Rows[0]["AccNo"].ToString();
                    txtParti1.Text = DT.Rows[0]["Particulars"].ToString();
                    txtAmount.Text = DT.Rows[0]["Amount"].ToString();
                    txtTrxType.Text = DT.Rows[0]["TrxType"].ToString();
                    txtActivity.Text = DT.Rows[0]["Activity"].ToString();
                    txtPmtMode.Text = DT.Rows[0]["PmtMode"].ToString();
                    txtInstNo.Text = DT.Rows[0]["InstNo"].ToString();
                    txtInstDate.Text = DT.Rows[0]["InstDate"].ToString();

                    txtGlCode.Focus();
                }
            }
            else
            {
                ClearUpdate();
                btnModify.Visible = false;
                btnInsert.Visible = false;
                divDetailInfo.Visible = false;
                WebMsgBox.Show("Check voucher scrollno ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            //  before update move voucher into AVS5041 table
            Result = TV.MoveVoucher(Session["BrCd"].ToString(), txtEDate.Text.ToString(), txtSetNo.Text.ToString(), Session["MID"].ToString());
            //  Update Record
            Result = TV.UpdateRecord(Session["BRCD"].ToString(), txtEDate.Text.ToString(), txtSetNo.Text.ToString(), ViewState["ScrollNo"].ToString(), txtGlCode.Text.ToString(),
                txtPrCode.Text.ToString(), txtAccNo.Text.ToString(), txtParti1.Text.ToString(), txtAmount.Text.ToString(), txtTrxType.Text.ToString(), txtActivity.Text.ToString(),
                txtPmtMode.Text.ToString(), txtInstNo.Text.ToString(), txtInstDate.Text.ToString(), ViewState["OldPrCode"].ToString(), ViewState["OldAccNo"].ToString(),
                ViewState["OldAmount"].ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

            if (Result > 0)
            {
                btnModify.Visible = false;
                btnInsert.Visible = false;
                divDetailInfo.Visible = false;
                btnDisplay.Focus();
                ClearUpdate();
                DebitCredit();
                BindGridVoucher();
                WebMsgBox.Show("Updated Successfully ...!!", this.Page);
                CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "AllField _Mod_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        try
        {
            //  before insert move voucher into AVS5041 table
            Result = TV.MoveVoucher(Session["BrCd"].ToString(), txtEDate.Text.ToString(), txtSetNo.Text.ToString(), Session["MID"].ToString());
            //  Insert Record
            Result = TV.InsertRecord(Session["BRCD"].ToString(), txtEDate.Text.ToString(), txtSetNo.Text.ToString(), ViewState["ScrollNo"].ToString(), txtGlCode.Text.ToString(),
                txtPrCode.Text.ToString(), txtAccNo.Text.ToString(), txtParti1.Text.ToString(), txtAmount.Text.ToString(), txtTrxType.Text.ToString(), txtActivity.Text.ToString(),
                txtPmtMode.Text.ToString(), txtInstNo.Text.ToString(), txtInstDate.Text.ToString(), Session["MID"].ToString());

            if (Result > 0)
            {
                btnModify.Visible = false;
                btnInsert.Visible = false;
                divDetailInfo.Visible = false;
                btnDisplay.Focus();
                ClearUpdate();
                DebitCredit();
                BindGridVoucher();
                WebMsgBox.Show("Saved Successfully ...!!", this.Page);
                CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "AllField _lstIntdt_update_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkAuthorize_Click(object sender, EventArgs e)
    {
        string EntryMid = "";
        try
        {
            //Added By Amol on 07/11/2017 as per darade sir instruction
            Message = VA.CheckVoucher(Session["BRCD"].ToString(), txtEDate.Text.ToString(), txtSetNo.Text.ToString());
            if (Message.ToString() == "0")
            {
                EntryMid = TV.GetEntryMid(Session["BRCD"].ToString(), txtEDate.Text.ToString(), txtSetNo.Text.ToString());
                if (Session["MID"].ToString() != EntryMid.ToString())
                {
                    DT = TV.GetVoucher(Session["BRCD"].ToString(), txtEDate.Text.ToString(), txtSetNo.Text.ToString(), "ALL");
                    if (DT.Rows.Count > 0)
                    {
                        txtEDate1.Text = txtEDate.Text.ToString();
                        txtSetNo1.Text = txtSetNo.Text.ToString();

                        grdTransaction.DataSource = DT;
                        grdTransaction.DataBind();

                        DRCR = TV.GetDrCr(Session["BRCD"].ToString(), txtEDate.Text.ToString(), txtSetNo.Text.ToString(), "CRDR");
                        if (DRCR.Rows.Count > 0)
                        {
                            txtTotalDr.Text = DRCR.Rows[0]["TotalDebit"].ToString();
                            txtTotalCr.Text = DRCR.Rows[0]["TotalCredit"].ToString();
                        }

                        txtTrfDate.Focus();
                        btnAuthorize.Visible = true;
                        btnTransfer.Visible = false;
                        btnCancel.Visible = false;
                        divTransfer.Visible = false;
                        divReason.Visible = false;
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("$('#DivVoucher').modal('show');");
                        sb.Append(@"</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                    }
                }
                else
                {
                    WebMsgBox.Show("Not allow for same user...!!", this.Page);
                    return;
                }
            }
            else
            {
                WebMsgBox.Show("Voucher Not Tally... Please check in voucher view ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        DT = new DataTable();
        DRCR = new DataTable();
        try
        {
            BindCancelReason(ddlReason);

            DT = TV.GetVoucher(Session["BRCD"].ToString(), txtEDate.Text.ToString(), txtSetNo.Text.ToString(), "ALL");
            if (DT.Rows.Count > 0)
            {
                txtEDate1.Text = txtEDate.Text.ToString();
                txtSetNo1.Text = txtSetNo.Text.ToString();

                grdTransaction.DataSource = DT;
                grdTransaction.DataBind();

                DRCR = TV.GetDrCr(Session["BRCD"].ToString(), txtEDate.Text.ToString(), txtSetNo.Text.ToString(), "CRDR");
                if (DRCR.Rows.Count > 0)
                {
                    txtTotalDr.Text = DRCR.Rows[0]["TotalDebit"].ToString();
                    txtTotalCr.Text = DRCR.Rows[0]["TotalCredit"].ToString();
                }

                btnCancel.Visible = true;
                btnTransfer.Visible = false;
                btnAuthorize.Visible = false;
                divTransfer.Visible = false;
                divReason.Visible = true;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#DivVoucher').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            DT = TV.GetVoucher(Session["BRCD"].ToString(), txtEDate.Text.ToString(), txtSetNo.Text.ToString(), "ALL");
            if (DT.Rows.Count > 0)
            {
                txtEDate1.Text = txtEDate.Text.ToString();
                txtSetNo1.Text = txtSetNo.Text.ToString();

                grdTransaction.DataSource = DT;
                grdTransaction.DataBind();

                DRCR = TV.GetDrCr(Session["BRCD"].ToString(), txtEDate.Text.ToString(), txtSetNo.Text.ToString(), "CRDR");
                if (DRCR.Rows.Count > 0)
                {
                    txtTotalDr.Text = DRCR.Rows[0]["TotalDebit"].ToString();
                    txtTotalCr.Text = DRCR.Rows[0]["TotalCredit"].ToString();
                }

                txtTrfDate.Focus();
                btnTransfer.Visible = true;
                btnAuthorize.Visible = false;
                btnCancel.Visible = false;
                divTransfer.Visible = true;
                divReason.Visible = false;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#DivVoucher').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnAuthorize_Click(object sender, EventArgs e)
    {
        try
        {
            Result = TV.VoucherAuthorise(Session["BRCD"].ToString(), txtEDate.Text.ToString(), txtSetNo.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (Result > 0)
            {
                btnModify.Visible = false;
                btnInsert.Visible = false;
                divDetailInfo.Visible = false;
                btnDisplay.Focus();
                DebitCredit();
                BindGridVoucher();
                WebMsgBox.Show("SetNo " + txtSetNo.Text.ToString() + " Successfully Authorised...!!", this.Page);
                ClearUpdate();
                CreateAVSB("AT");
                CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "AllField _Mod_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("location.reload();");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);

                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Result = TV.VoucherCancel(Session["BRCD"].ToString(), txtEDate.Text.ToString(), txtSetNo.Text.ToString(), ddlReason.SelectedValue, Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (Result > 0)
            {
                btnModify.Visible = false;
                btnInsert.Visible = false;
                divDetailInfo.Visible = false;
                btnDisplay.Focus();
                DebitCredit();
                BindGridVoucher();
                WebMsgBox.Show("SetNo " + txtSetNo.Text.ToString() + " Successfully cancelled...!!", this.Page);
                ClearUpdate();
                CreateAVSB("DL");
                CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "AllField _Mod_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("location.reload();");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);

                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTrfDate.Text.ToString() != "")
            {
                if (txtEDate.Text.ToString() != txtTrfDate.Text.ToString())
                {
                    Result = TV.VoucherTransfer(Session["BRCD"].ToString(), txtEDate.Text.ToString(), txtSetNo.Text.ToString(), txtTrfDate.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                    if (Result > 0)
                    {
                        btnModify.Visible = false;
                        btnInsert.Visible = false;
                        divDetailInfo.Visible = false;
                        btnDisplay.Focus();
                        DebitCredit();
                        BindGridVoucher();
                        WebMsgBox.Show("SetNo " + txtSetNo.Text.ToString() + " Successfully cancelled and transfer with setno : " + Result + " ...!!", this.Page);
                        ClearUpdate();
                        CreateAVSB("TRF");
                        CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "AllField _Mod_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("location.reload();");
                        sb.Append(@"</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);

                        return;
                    }
                }
                else
                {
                    WebMsgBox.Show("Allready voucher is on same date ...!!", this.Page);

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$('#DivVoucher').modal('show');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);

                    return;
                }
            }
            else
            {
                WebMsgBox.Show("Enter transfer date first ...!!", this.Page);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#DivVoucher').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);

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
            ClearAllData();
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
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

}