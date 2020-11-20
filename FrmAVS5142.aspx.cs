using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

public partial class FrmAVS5142 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    Mobile_Service MS = new Mobile_Service();
    ClsAVS5142 LP = new ClsAVS5142();
    DataTable DT = new DataTable();
    string sResult = "";
    int Result = 0;

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
                txtBrCode.Text = Session["BRCD"].ToString();
                txtEDate.Text = Session["EntryDate"].ToString();
                txtFSetNo.Focus();
            }
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
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
            divVouchers.Visible = true;
            divUnTallySet.Visible = false;
            Result = LP.GetVoucherInfo(grdVoucher, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), txtFSetNo.Text.ToString(), txtTSetNo.Text.ToString());
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
            divUnTallySet.Visible = true;
            divVouchers.Visible = false;

            //  Authorize all voucher and bind untally vouchers
            Result = LP.GetUntallySet(grdUnTallySet, txtBrCode.Text.ToString(), txtEDate.Text.ToString(), txtFSetNo.Text.ToString(), txtTSetNo.Text.ToString(), Session["MID"].ToString());

            //  Send SMS for authorize vouchers (GlCode In 1, 2, 3, 4, 5, 6, 8)
            DT = LP.GetSMS_Data(txtBrCode.Text.ToString(), txtEDate.Text.ToString());
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                    MS.Send_SMS(DT.Rows[i]["CustNo"].ToString(), DT.Rows[i]["SMS_Date"].ToString());
            }

            ClearAll();
            WebMsgBox.Show("Successfully authorized ...!!", this.Page);
            return;
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
            ClearAll();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearAll()
    {
        try
        {
            txtBrCode.Text = Session["BRCD"].ToString();
            txtEDate.Text = Session["EntryDate"].ToString();
            txtFSetNo.Text = "";
            txtTSetNo.Text = "";

            txtFSetNo.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

}