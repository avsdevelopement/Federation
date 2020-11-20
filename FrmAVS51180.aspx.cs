using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmAVS51180 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAVS51173 CS = new ClsAVS51173();
    clsAVS51178 Cbr = new clsAVS51178();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
            Response.Redirect("FrmLogin.aspx");

        if (!IsPostBack)
        {
            txtBrCd.Text = Session["BRCD"].ToString();
            txtBrCdName.Text = Cbr.getbrcode(txtBrCd.Text);
            DataTable dt = new DataTable();
            dt = CS.rptStock(FLAG: "SHOWALL", BRCD: txtBrCd.Text.ToString(), VENDORID: TxtVendorId.Text.ToString(), PRODID: TxtProductId.Text.ToString(), ENTRYDATE: txtEntryDate.Text.ToString());
            GrdStock.DataSource = dt;
            GrdStock.DataBind();
            //  txtBrCd.Text = Session["BRCDNAME"].ToString();
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            //if (txtBrCd.Text == "")
            //{
            //    WebMsgBox.Show("Please select branch!", this.Page); return;
            //}
            string FLAG = "";
            if (txtEntryDate.Text == "")
                FLAG = "SHOWALL";
            else
                FLAG = "SHOWALLASONBAL";

            DataTable dt = new DataTable();

            dt = CS.rptStock(FLAG: FLAG, BRCD: txtBrCd.Text.ToString(), VENDORID: TxtVendorId.Text.ToString(), PRODID: TxtProductId.Text.ToString(), ENTRYDATE: txtEntryDate.Text.ToString());

            //dt = CS.rptStock(FLAG: "SHOWALL", BRCD: txtBrCd.Text.ToString());
            GrdStock.DataSource = dt;
            GrdStock.DataBind();


        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            TxtVendorId.Text = "";
            txtVendorName.Text = "";
            TxtProductId.Text = "";
            txtProductNAme.Text = "";
            txtEntryDate.Text = "";
            TxtVendorId.Focus();
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string FLAG = "";
            if (txtEntryDate.Text == "")
                FLAG = "SHOWALL";
            else
                FLAG = "SHOWALLASONBAL";

            //This Logic for the Vendor Report
            if (TxtVendorId.Text == "")
                FLAG = "ALL";
            else
                FLAG = "SPECIFIC";

       
            string redirectURL = "FrmRView.aspx?BRCD=" + txtBrCd.Text.ToString() + "&VENDORID=" + TxtVendorId.Text + "&PRODID=" + TxtProductId.Text + "&FLAG=" + FLAG + "&ENTRYDATE=" + txtEntryDate.Text + "&rptname=RptDeedStock.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
    }
    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        if (txtEntryDate.Text == "")
        {
            DataTable dt = new DataTable();
            dt = CS.StockCalculate();
            if (dt == null)
            {
                WebMsgBox.Show("Stock Calculate Fail!", this.Page); return;
            }

            else if (dt.Rows.Count > 0)
            {
                WebMsgBox.Show("Stock Calculate successfully!", this.Page); return;
            }
            else
            {
                WebMsgBox.Show("Stock Stock not exists!", this.Page); return;
            }
        }
    }
    protected void txtProductNAme_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = txtProductNAme.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtProductNAme.Text = CT[0].ToString();
                TxtProductId.Text = CT[1].ToString();
                string[] GLS = CS.GetName(TxtProductId.Text, Session["MID"].ToString()).Split('_');

            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtProductId_TextChanged(object sender, EventArgs e)
    {
        txtProductNAme.Text= CS.GetProductID(TxtProductId.Text, Session["MID"].ToString());
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
                TxtVendorId.Text = CT[1].ToString();
                string[] GLS = CS.GetName(TxtVendorId.Text, Session["MID"].ToString()).Split('_');

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtVendorId_TextChanged(object sender, EventArgs e)
    {
        txtVendorName.Text = CS.GetProductID(TxtVendorId.Text, Session["MID"].ToString());
    }
    protected void txtBrCd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = txtBrCdName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtBrCdName.Text = CT[0].ToString();
                txtBrCd.Text = CT[1].ToString();
                //  string[] GLS = Cbr.getBrcdName(txtBrCd.Text).Split('_');

            }


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnClosingStockReport_Click(object sender, EventArgs e)
    {
        try
        {
            string FLAG = "";
            if (txtEntryDate.Text == "")
                FLAG = "CURRENT";
            else
                FLAG = "ASON";

            string redirectURL = "FrmRView.aspx?BRCD=" + txtBrCd.Text.ToString() + "&VENDORID=" + TxtVendorId.Text + "&PRODID=" + TxtProductId.Text + "&FLAG=" + FLAG + "&ENTRYDATE=" + txtEntryDate.Text + "&rptname=RptClosingStock.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
    }
}