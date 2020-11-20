using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Net;
using System.Data;


public partial class FrmAVS51183 : System.Web.UI.Page
{

    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAVS51173 CS = new ClsAVS51173();
    ClsCommon cmn = new ClsCommon();
    DataTable DT = new DataTable();
    string sResult = "", sResult1 = "";
    int Result = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
            Response.Redirect("FrmLogin.aspx");

        if (!IsPostBack)
        {
            txtVendorID.ReadOnly = true;
            autoglname.ContextKey = Session["MID"].ToString();


        }
    }

    protected void txtVendorName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = txtVendorName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtVendorName.Text = CT[0].ToString();
                txtVendorID.Text = CT[1].ToString();
                string[] GLS = CS.GetName(txtVendorID.Text, Session["MID"].ToString()).Split('_');

            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }

    protected void txtProductName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = txtProductName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtProductName.Text = CT[0].ToString();
                txtProductID.Text = CT[1].ToString();
                string[] GLS = CS.GetName(txtProductID.Text, Session["MID"].ToString()).Split('_');

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnClearAll_Click(object sender, EventArgs e)
    {

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string FLAG = "ALL";
            if (RbtAll.SelectedValue.ToString() == "2")
            {
                if (txtVendorID.Text.ToString() == "")
                {
                    WebMsgBox.Show("Enter Vendor ID!", this.Page); return;
                }
                FLAG = "SPECIFICVENDOR";           
            }
            else if (RbtAll.SelectedValue.ToString() == "3")
            {
                if (txtVendorID.Text.ToString() == "")
                {
                    WebMsgBox.Show("Enter Vendor ID!", this.Page); return;
                }
                if (txtProductID.Text.ToString() == "")
                {
                    WebMsgBox.Show("Enter Vendor product ID!", this.Page); return;
                }
                FLAG = "SPECIFIC";
            }

            string redirectURL = "FrmRView.aspx?FLAG=" + FLAG + "&VENDORID=" + txtVendorID.Text + "&PRODID=" + txtProductID.Text + "&rptname=RptProductMaster.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        SHOWUNAUTHORIZE();
    }


    public void SHOWUNAUTHORIZE()
    {
        DataTable dt = new DataTable();
        // CS.GridRPtProduct("ALL", GrdProduct);
        string FLAG = "ALL";

        if (RbtAll.SelectedValue.ToString() == "2")
        {
            if (txtVendorID.Text.ToString() == "")
            {
                WebMsgBox.Show("Enter Vendor ID!", this.Page); return;
            }
            FLAG = "SPECIFICVENDOR";
        }
        else if (RbtAll.SelectedValue.ToString() == "3")
        {
            if (txtVendorID.Text.ToString() == "")
            {
                WebMsgBox.Show("Enter Vendor ID!", this.Page); return;
            }
            if (txtProductID.Text.ToString() == "")
            {
                WebMsgBox.Show("Enter Vendor product ID!", this.Page); return;
            }
            FLAG = "SPECIFIC";
        }
        CS.GridRPtProduct("ALL", GrdProduct);
        dt = CS.rptProductMaster(FLAG: FLAG, VENDORID: txtVendorID.Text, PRODID:txtProductID.Text);

    }
    protected void txtProductID_TextChanged(object sender, EventArgs e)
    {
        txtProductName.Text = CS.GetProductID(txtProductID.Text, Session["MID"].ToString());
    }
    protected void txtVendorID_TextChanged(object sender, EventArgs e)
    {
        txtVendorName.Text = CS.GetVendorID(txtVendorID.Text, Session["MID"].ToString());
    }
    protected void GrdProduct_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GrdProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdProduct.PageIndex = e.NewPageIndex;
            SHOWUNAUTHORIZE();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void GrdProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}