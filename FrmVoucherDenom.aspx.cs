using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmVoucherDenom : System.Web.UI.Page
{
    ClsVoucherDenom VD = new ClsVoucherDenom();
    DataTable DT = new DataTable();
    string sResult ="", sFlag = "";
    int Result = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //if(Session["UGRP"].ToString() == "5")
                txtEntryDate.Text = Session["EntryDate"].ToString();
                //else
                //Response.Redirect("~/FrmBlank.aspx?ShowMessage=true");
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtSetNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(txtSetNo.Text.Trim().ToString() == "" ? "0" : txtSetNo.Text.Trim().ToString()) > 0)
            {
                sResult = VD.CheckCashSet(Session["BRCD"].ToString(), txtEntryDate.Text.ToString(), txtSetNo.Text.Trim().ToString());

                if ((sResult != null && sResult == "3") || (sResult != null && sResult == "4"))
                {
                    btnSubmit.Focus();
                }
                else
                {
                    txtSetNo.Text = "";
                    txtSetNo.Focus();
                    lblMessage.Text = "Voucher is not of type-cash...!!";
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSetNo.Text.Trim().ToString() != "" && Convert.ToInt32(txtSetNo.Text.Trim().ToString() == "" ? "0" : txtSetNo.Text.Trim().ToString()) > 0)
                VD.GetSpecificVoucher(grdVoucher, Session["BRCD"].ToString(), txtEntryDate.Text.ToString(), txtSetNo.Text.ToString());
            else
                VD.GetAllVoucher(grdVoucher, Session["BRCD"].ToString(), txtEntryDate.Text.ToString());
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
            LinkButton objlink = (LinkButton)sender;
            string[] dens = objlink.CommandArgument.ToString().Split('_');
            Session["densset"] = dens[0].ToString();
            Session["densamt"] = dens[1].ToString();
            Session["denssubgl"] = dens[2].ToString();
            Session["densact"] = dens[3].ToString();

            sResult = VD.CheckDenom(Session["BRCD"].ToString(), Session["densset"].ToString(), Session["EntryDate"].ToString());
            if (sResult == null)
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            txtSetNo.Text = "";
            txtEntryDate.Text = Session["EntryDate"].ToString();
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

}