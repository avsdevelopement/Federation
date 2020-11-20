using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmCancelConfirm : System.Web.UI.Page
{
    ClsCancelEntry CE = new ClsCancelEntry();
    DbConnection conn = new DbConnection();
    string[] NO;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState["SetNo"] = Request.QueryString["setno"].ToString();
                ViewState["FL"] = Request.QueryString["FL"].ToString();
                ViewState["STR"] = Request.QueryString["STR"].ToString();
                GetData();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void GetData()
    {
        try
        {
            DataTable DT = new DataTable();
            DT = CE.GetVInfo(ViewState["SetNo"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(),ViewState["STR"].ToString());
            if (DT.Rows.Count > 0)
            {
                NO = DT.Rows[0]["SETNO"].ToString().Split('_');
                TxtVoucherNo.Text = NO[0].ToString();
                ViewState["GLCODE"] = NO[1].ToString();
                ViewState["SUBGLCODE"] = NO[2].ToString();

                TxtPrdType.Text = DT.Rows[0]["SUBGLCODE"].ToString();
                TxtAccNo.Text = DT.Rows[0]["ACCNO"].ToString();
                TxtCustName.Text = DT.Rows[0]["CUSTNAME"].ToString();
                
                if (DT.Rows[0]["CREDIT"].ToString() != "0.00")
                {
                    TxtAmount.Text = DT.Rows[0]["CREDIT"].ToString();
                }
                if (DT.Rows[0]["DEBIT"].ToString() != "0.00")
                {
                    TxtAmount.Text = DT.Rows[0]["DEBIT"].ToString();
                }
                TxtNarration.Text = DT.Rows[0]["PARTICULARS"].ToString();
                //TxtInsNo.Text = DT.Rows[0]["INSTRUMENTNO"].ToString();
                //TxtInsDate.Text = DT.Rows[0]["INSTRUMENTDATE"].ToString();
                TxtMaker.Text = DT.Rows[0]["MID"].ToString();
            
            }
            if (TxtMaker.Text == Session["MID"].ToString())
            {
                WebMsgBox.Show("Sorry Maker is not Authorized!....", this.Page);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();window.opener.location.href=window.opener.location.href;", true);
               // HttpContext.Current.Response.Redirect("FrmCancelEntryMain.aspx", true);
                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int result;
            string RC = CE.CheckStage(ViewState["SetNo"].ToString(), Session["EntryDate"].ToString(),Session["BRCD"].ToString());
            if (RC != "1003" || Session["UGRP"].ToString()=="1")
            {
                if (ViewState["STR"].ToString() != "ABB-MultiTRF")
                {
                    result = CE.CancelVoucher(ViewState["SetNo"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), ViewState["SUBGLCODE"].ToString(), TxtAccNo.Text, Session["MID"].ToString(), ViewState["FL"].ToString());
                    if (ViewState["STR"].ToString() == "INV-CLO")
                    {
                        result = CE.UpdateInv(ViewState["SetNo"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE"].ToString(), ViewState["SUBGLCODE"].ToString(), TxtAccNo.Text, Session["MID"].ToString(), ViewState["FL"].ToString());
                    }
                }
                else
                    result = CE.CancelCRCP1(ViewState["SetNo"].ToString(), Session["EntryDate"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
                if (result > 0)
                {
                    WebMsgBox.Show("Entry Canceled for Voucher No-" + ViewState["SetNo"].ToString() + "....", this.Page);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();window.opener.location.href=window.opener.location.href;", true);
                }

            }
            else if (RC == "1003")
            {
                WebMsgBox.Show("The Voucher is already Authorized, Cannot delete!.....", this.Page);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();window.opener.location.href=window.opener.location.href;", true);
                // TxtGLCD.Focus();
            }
            else
            {
                WebMsgBox.Show("Records not present!....", this.Page);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();window.opener.location.href=window.opener.location.href;", true);
            }

            //CE.CallUpdate(ViewState["SetNo"].ToString(),Session["BRCD"].ToString(),Session["EntryDate"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Exit_Click(object sender, EventArgs e)
    {
        WebMsgBox.Show("Want to EXIT!......Press OK", this.Page);
        ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();window.opener.location.href=window.opener.location.href;", true);
      //  HttpContext.Current.Response.Redirect("FrmCancelEntryMain.aspx", true);
    }

   

    
}