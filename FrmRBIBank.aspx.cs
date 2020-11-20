using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmRBIBank : System.Web.UI.Page
{
    clsRBIBank objRBI = new clsRBIBank();
    ClsBindDropdown BD = new ClsBindDropdown();

    protected void Page_Load(object sender, EventArgs e)
    {

    }



    protected void btnclear_Click(object sender, EventArgs e)
    {
        try
        {
            clearAll();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmBlank.aspx");
    }
    protected void Exit_Click(object sender, EventArgs e)
    {
    }

    void clearAll()
    {
        txtBANKCD.Text = "";
        txtBRANCHCD.Text = "";
        txtDESCR.Text = "";
        ddlDDYN.Items.Clear();
        ddlLMOYN.Items.Clear();
        ddlMSYN.Items.Clear();
        ddlMTYN.Items.Clear();
        ddlTTYN.Items.Clear();

        //  txtLMOYN.Text = "";
        // txtMSYN.Text = "";
        //  txtDDYN.Text = "";
        //  txtMTYN.Text = "";
        // txtTTYN.Text = "";
        txtDDLIMIT.Text = "";
        txtMTLIMIT.Text = "";
        txtTTLIMIT.Text = "";
        txtDDCOLLBRCD.Text = "";
        txtTTCOLLBRCD.Text = "";
        txtDISTRICT.Text = "";
        txtSTATECD.Text = "";
        txtMICRCode.Text = String.Empty;

    }

    protected void txtBANKCD_TextChanged(object sender, EventArgs e) { txtBRANCHCD.Focus(); }
    protected void txtBRANCHCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtBRANCHCD.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtBRANCHCD.Text = custnob[0].ToString();

                string[] AC = objRBI.GetBranchName(txtBANKCD.Text, custnob[2].ToString()).Split('-');

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        txtDESCR.Focus();
    }
    protected void txtDESCR_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtDESCR.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtDESCR.Text = custnob[0].ToString();

                string[] AC = objRBI.GetBankName(txtBANKCD.Text, custnob[2].ToString()).Split('-');

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }


        txtSTATECD.Focus();
    }
    // protected void txtLMOYN_TextChanged(object sender, EventArgs e) { txtMSYN.Focus(); }
    // protected void txtMSYN_TextChanged(object sender, EventArgs e) { txtDDYN.Focus(); }
    // protected void txtDDYN_TextChanged(object sender, EventArgs e) { txtMTYN.Focus(); }
    // protected void txtMTYN_TextChanged(object sender, EventArgs e) { txtTTYN.Focus(); }
    //  protected void txtTTYN_TextChanged(object sender, EventArgs e) { txtDDLIMIT.Focus(); }
    protected void txtDDLIMIT_TextChanged(object sender, EventArgs e) { txtMTLIMIT.Focus(); }
    protected void txtMTLIMIT_TextChanged(object sender, EventArgs e) { txtTTLIMIT.Focus(); }
    protected void txtTTLIMIT_TextChanged(object sender, EventArgs e) { txtDDCOLLBRCD.Focus(); }
    protected void txtDDCOLLBRCD_TextChanged(object sender, EventArgs e) { txtTTCOLLBRCD.Focus(); }
    protected void txtTTCOLLBRCD_TextChanged(object sender, EventArgs e) { txtSTATECD.Focus(); }
    protected void txtSTATECD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtSTATECD.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtSTATECD.Text = custnob[0].ToString();

                string[] AC = objRBI.GetStateName(txtBANKCD.Text, custnob[2].ToString()).Split('-');

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }


        txtDISTRICT.Focus();
    }
    protected void txtDISTRICT_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtDISTRICT.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtDISTRICT.Text = custnob[0].ToString();

                string[] AC = objRBI.GetDISTName(txtSTATECD.Text, custnob[2].ToString()).Split('-');

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }


    }




    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            string ans = objRBI.spRBI("INSERT_RBI", txtBANKCD.Text, txtBRANCHCD.Text, txtDESCR.Text, ddlLMOYN.SelectedValue, ddlMSYN.SelectedValue, ddlDDYN.SelectedValue,
                ddlMTYN.SelectedValue, ddlTTYN.SelectedValue, txtDDLIMIT.Text,
                txtMTLIMIT.Text, txtTTLIMIT.Text, txtDDCOLLBRCD.Text, txtTTCOLLBRCD.Text, txtDISTRICT.Text, txtSTATECD.Text, txtMICRCode.Text);
            switch (ans)
            {
                case "EXISTS":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RBI Bank", "alert('Record Already Exists!')", true);
                    break;
                case "INSERT":
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RBI Bank", "alert('Record Inserted Successfully!')", true);
                    break;
                default:
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RBI Bank", "alert('Record not Inserted!')", true);
                    break;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        clearAll();
    }

}