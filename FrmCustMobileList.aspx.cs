﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmCustMobileList : System.Web.UI.Page
{
    DataTable DT = new DataTable();
    DataTable DT1 = new DataTable();
    ClsCustomerDetails CD = new ClsCustomerDetails();
    DbConnection conn = new DbConnection();
    scustom cc = new scustom();
    ClsOpenClose OC = new ClsOpenClose();
    Cls_RecoBindDropdown BD1 = new Cls_RecoBindDropdown();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["CUSTNO"]))
                {
                    txtCustNo.Text = Request.QueryString["CUSTNO"].ToString();
                    DT1 = CD.GetCustName(txtCustNo.Text.ToString());
                    txtCustName.Text = DT1.Rows[0]["CUSTNAME"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void txtCustNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DT = CD.GetStage(txtCustNo.Text);

            if (DT.Rows[0]["STAGE"].ToString() == "1001")
            {
                WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
                return;
            }
            else if (DT.Rows[0]["STAGE"].ToString() == "1004")
            {
                WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                return;
            }
            else if (DT.Rows[0]["STAGE"].ToString() == "" || DT.Rows[0]["STAGE"].ToString() == null)
            {
                WebMsgBox.Show("Customer Not Exists...!!", this.Page);
                return;
            }
            else
            {
                DT1 = CD.GetCustName(txtCustNo.Text.ToString());
                txtCustName.Text = DT1.Rows[0]["CUSTNAME"].ToString();
                DT = CD.GetCustAccInfo(Session["BRCD"].ToString(), txtCustNo.Text);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtCustName_TextChanged(object sender, EventArgs e)
    {
        try
        {

            string CUNAME = txtCustName.Text;
            string[] custnob = CUNAME.Split('_');

            if (custnob.Length > 1)
            {
                txtCustName.Text = custnob[0].ToString();
                txtCustNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                DT = CD.GetStage(txtCustNo.Text);

                if (DT.Rows[0]["STAGE"].ToString() == "1001")
                {
                    WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
                    return;
                }
                else if (DT.Rows[0]["STAGE"].ToString() == "1004")
                {
                    WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                    return;
                }
                else
                {

                    DT = CD.GetCustAccInfo(Session["BRCD"].ToString(), txtCustNo.Text);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtToCustno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DT = CD.GetStage(TxtToCustno.Text);

            if (DT.Rows[0]["STAGE"].ToString() == "1001")
            {
                WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
                return;
            }
            else if (DT.Rows[0]["STAGE"].ToString() == "1004")
            {
                WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                return;
            }
            else if (DT.Rows[0]["STAGE"].ToString() == "" || DT.Rows[0]["STAGE"].ToString() == null)
            {
                WebMsgBox.Show("Customer Not Exists...!!", this.Page);
                return;
            }
            else
            {
                DT1 = CD.GetCustName(TxtToCustno.Text.ToString());
                TxtToCustName.Text = DT1.Rows[0]["CUSTNAME"].ToString();
                DT = CD.GetCustAccInfo(Session["BRCD"].ToString(), TxtToCustno.Text);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtToCustName_TextChanged(object sender, EventArgs e)
    {
        try
        {

            string CUNAME = TxtToCustName.Text;
            string[] custnob = CUNAME.Split('_');

            if (custnob.Length > 1)
            {
                TxtToCustName.Text = custnob[0].ToString();
                TxtToCustno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                DT = CD.GetStage(TxtToCustno.Text);

                if (DT.Rows[0]["STAGE"].ToString() == "1001")
                {
                    WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
                    return;
                }
                else if (DT.Rows[0]["STAGE"].ToString() == "1004")
                {
                    WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                    return;
                }
                else
                {
                    DT = CD.GetCustAccInfo(Session["BRCD"].ToString(), TxtToCustno.Text);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string redirectURL = "FrmRView.aspx?FBrcd=" + Txtfrmbrcd.Text + "&TBrcd=" + Txttobrcd.Text + "&FCustNo=" + txtCustNo.Text + "&TCustNo=" + TxtToCustno.Text + "&FromDate=" + TxtFDate.Text + "&Flag=" + DdlActivity.Text + "&Live=" + DdlSUActivity.Text + "&rptname=RptMobileData.rdlc" + "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {

    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {

    }
}