using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAVS5080 : System.Web.UI.Page
{
    ClsAVS5074 CLS = new ClsAVS5074();
    DbConnection conn = new DbConnection();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string Flag = "", PayMast = "",FL="";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }
                TxtEntryDate.Text = Session["EntryDate"].ToString();
                TxtEntryDate.Enabled = false;
                TxtSetNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtSetNo.Text == "")
            {
                BindGrid();
            }
            else
            {
                BindByVoucher();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindGrid() //New Logic
    {
        try
        {
            if (Session["UGRP"].ToString() != "1")
            {
                CLS.Getinfotable_All(grdShow, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());
            }
            else
            {
                CLS.GetinfotableAdmin_All(grdShow, Session["MID"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindByVoucher()
    {
        if (Session["UGRP"].ToString() != "1")
        {
            CLS.GetByVoucherGridAdmin_Spe(Session["BRCD"].ToString(), Session["MID"].ToString(), grdShow, TxtSetNo.Text, TxtEntryDate.Text);
        }
        else
        {
            CLS.GetByVoucherGridAdmin_Spe(Session["BRCD"].ToString(), Session["MID"].ToString(), grdShow, TxtSetNo.Text, TxtEntryDate.Text);
        }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlnk = (LinkButton)sender;
            string[] AT = objlnk.CommandArgument.ToString().Split('_');
            ViewState["SetNo"] = AT[0].ToString();
            ViewState["ScrollNo"] = AT[1].ToString();
            ViewState["EntryMid"] = AT[2].ToString();

            string Modal_Flag = "";
            PayMast = CLS.GetPAYMAST(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString());

            //Added By Amol ON 17/11/2017 
            if (ViewState["EntryMid"].ToString() != Session["MID"].ToString())
            {
                if (PayMast.ToString() == "CUST_TR")
                    PayMast = "CUST_TR";
                Modal_Flag = "CUST_TR";
                ViewState["MODAL"] = PayMast.ToString();
                GetVoucherDetails(ViewState["SetNo"].ToString(), Session["BRCD"].ToString(), PayMast.ToString(), ViewState["ScrollNo"].ToString());
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#" + Modal_Flag + "').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
            }
            else
            {
                WebMsgBox.Show("Not allow for same user...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void GetVoucherDetails(string SETNO, string BRCD, string PAYMAST, string ScrollNo)
    {
        try
        {
            DataTable DT = new DataTable();
            DT = CLS.GetDetails_ToFill(SETNO, BRCD, Session["EntryDate"].ToString(), PAYMAST, ScrollNo);
            {
                if (PAYMAST == "CUST_TR")
                {
                    txtamountt.Text = DT.Rows[0]["AMT"].ToString(); //DT.Rows[0]["CREDIT"].ToString() == "0" ? DT.Rows[0]["DEBIT"].ToString() : DT.Rows[0]["CREDIT"].ToString();
                    TxtProcode.Text = DT.Rows[0]["SUBGLCODE"].ToString();
                    TxtProName.Text = DT.Rows[0]["GLNAME"].ToString();
                    TxtMakName.Text = DT.Rows[0]["MID"].ToString();
                    TextBox1.Text = SETNO;
                    ViewState["CT"] = DT.Rows[0]["CUSTNO"].ToString();
                    TxtPCMAC.Text = DT.Rows[0]["pcMAC"].ToString();
                    TextBox2.Text = DT.Rows[0]["EntryDate"].ToString();
                    TxtAccNo.Text = DT.Rows[0]["ACCNO"].ToString();
                    TxtAccName.Text = DT.Rows[0]["CUSTNAME"].ToString();
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
        Response.Redirect("FrmBlank.aspx");
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            int Res = 0;
            string STG = CLS.CheckStage(ViewState["SetNo"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString());
            if (STG != "1003" || Session["UGRP"].ToString() == "1")
            {
                int MID = CLS.GetSetMid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString());

                if (MID != Convert.ToInt32(Session["MID"].ToString()))
                {
                    if (ViewState["MODAL"].ToString() == "CUST_TR")
                    {
                        Res = CLS.CancelPost(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["SetNo"].ToString(), Session["MID"].ToString(), ViewState["CT"].ToString(), ViewState["EntryMid"].ToString());
                    }
                   
                    if (Res > 0)
                    {
                        WebMsgBox.Show("SetNo " + ViewState["SetNo"].ToString() + " Sucessfully Canceled...!!", this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string RR = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Voucher_CancelEntry_" + ViewState["SETNO"].ToString() + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "", "alert('Entry " + ViewState["SetNo"].ToString() + " sucessfully authorized....!');", true);
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("location.reload();");
                        sb.Append(@"</script>");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "", "alert('Not allow for same user...!!');", true);
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("location.reload();");
                    sb.Append(@"</script>");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "", "alert('Entry already Auhtorized,Contact to Administrator...!!');", true);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append(@"<script type='text/javascript'>");
                sb.Append("location.reload();");
                sb.Append(@"</script>");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}