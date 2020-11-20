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
public partial class FrmAVS51177 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAVS51173 CS = new ClsAVS51173();
    ClsCommon cmn = new ClsCommon();
    DataTable DT = new DataTable();
    string sResult = "", sResult1 = "";
    int Result = 0;
    string UNITCOST = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
        {
            Response.Redirect("FrmLogin.aspx",false);
        }
        if (!IsPostBack)
        {
            ViewState["Flag"] = "AD";
           autoglname.ContextKey = Session["MID"].ToString();
            BtnVenderMasterAdd.Text = "Create";
            txtBRCD.Text = Session["BRCD"].ToString();
            SHOWStockData();
        

        }


    }
    protected void txtProductID_TextChanged(object sender, EventArgs e)
    {
        
        
        try
        {
            txtProductName.Text = CS.GetProductID(txtProductID.Text, Session["MID"].ToString());

         sResult1 = CS.GetProductID(txtProductID.Text, Session["MID"].ToString());
           // sResult = CS.DeadStock("PRODID", txtProductID.Text.ToString(), txtVendorID.Text);
            if (sResult == "NOTEXISTS")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Present!')", true);
                txtProductID.Text = "";
                txtProductName.Text = "";
                txtProductName.Focus();
                return;
            }
            else
                if (sResult == "NOTAUTH")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Authorize!')", true);
                    txtProductID.Text = "";
                    txtProductName.Text = "";
                    txtProductName.Focus();
                    return;
                }
                else
                    if (sResult == "PRODIDNOTBELONGSTOVENDORID")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Available In VendorMaster!')", true);
                        txtProductID.Text = "";
                        txtProductName.Text = "";
                        txtProductName.Focus();
                        return;
                    }
            txtProductName.Text = sResult1;
  
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        try
        {

            ViewState["Flag"] = "AD";
            BtnVenderMasterAdd.Text = "Create";
            BtnVenderMasterAdd.Visible = true;
            txtBRCD.Text = Session["BRCD"].ToString();
            txtBRCD.Enabled = false;
            txtVendorID.Enabled = true;
            txtVendorName.Enabled = true;
            txtProductID.Enabled = true;
            txtProductName.Enabled = true;
            txtQuantity.Enabled = true;

            SHOWStockData();
            clear1();
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

            ViewState["Flag"] = "MD";
           
            BtnVenderMasterAdd.Text = "Modify";
            BtnVenderMasterAdd.Visible = false;
            txtBRCD.Enabled = true;
            txtVendorID.Enabled = true;
            txtVendorName.Enabled = true;
            txtProductID.Enabled = true;
            txtProductName.Enabled = true;
            txtQuantity.Enabled = true;
            clear();
            SHOWStockData(); 
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void LnkAuthorize_Click(object sender, EventArgs e)
    {
        try
        {

            ViewState["Flag"] = "ATH";
          
            BtnVenderMasterAdd.Visible = false;
            BtnVenderMasterAdd.Text = "Authorize";
              txtBRCD.Enabled = false;
              txtVendorID.Enabled = false;
              txtVendorName.Enabled = false;
              txtProductID.Enabled = false;
              txtProductName.Enabled = false;
              txtQuantity.Enabled = false;
              txtSGSTAmount.Enabled = false;
              txtSGSTPercent.Enabled = false;
              txtTotalAmount.Enabled = false;
              txtCGSTAmount.Enabled = false;
              txtCGSTPercent.Enabled = false;
              txtRate.Enabled = false;
            clear();
            SHOWStockData();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {

            ViewState["Flag"] = "DEL";
         
            BtnVenderMasterAdd.Text = "Delete";
            txtBRCD.Enabled = false;
            txtVendorID.Enabled = false;
            txtVendorName.Enabled = false;
            txtProductID.Enabled = false;
            txtProductName.Enabled = false;
            txtQuantity.Enabled = false;
            txtSGSTAmount.Enabled = false;
            txtSGSTPercent.Enabled = false;
            txtTotalAmount.Enabled = false;
            txtCGSTAmount.Enabled = false;
            txtCGSTPercent.Enabled = false;
            txtRate.Enabled = false;
            clear();
            SHOWStockData();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
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
              //  sResult = CS.DeadStock("PRODID", CT[1].ToString(), txtVendorID.Text);
                if (sResult == "NOTEXISTS")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Present!')", true);
                    txtProductID.Text = "";
                    txtProductName.Text = "";
                    txtProductName.Focus();
                    return;
                }
                else
                    if (sResult == "NOTAUTH")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Authorize!')", true);
                        txtProductID.Text = "";
                        txtProductName.Text = "";
                        txtProductName.Focus();
                        return;
                    }
                    else
                        if (sResult == "PRODIDNOTBELONGSTOVENDORID")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Available In VendorMaster!')", true);
                            txtProductID.Text = "";
                            txtProductName.Text = "";
                            txtProductName.Focus();
                            return;
                        }
                string[] GLS = CS.GetName(txtProductID.Text, Session["MID"].ToString()).Split('_');

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnVenderMasterAdd_Click(object sender, EventArgs e)
    {
        if (ViewState["Flag"].ToString() == "AD")
        {

            if (txtVendorID.Text == "")
            {
                WebMsgBox.Show("Enter Vendor ID ..!!", this.Page);
                return;

            }
            if (txtVendorName.Text == "")
            {
                WebMsgBox.Show("Enter Vendor Name..!!", this.Page);
                return;
            }
            if (txtProductID.Text == "")
            {
                WebMsgBox.Show("Enter Product ID..!!", this.Page);
                return;
            }
            if (txtProductName.Text == "")
            {
                WebMsgBox.Show("Enter Product Name..!!", this.Page);
                return;
            }
            if (txtQuantity.Text == "")
            {
                WebMsgBox.Show("Enter Quantity..!!", this.Page);
                return;
            }
            try
            {


                sResult = CS.OpeningStock(FLAG: "AD",BRCD:txtBRCD.Text, VENDORID: txtVendorID.Text, PRODID: txtProductID.Text, UNITCOST: txtRate.Text, QTY: txtQuantity.Text, AMOUNT: txtTotalAmount.Text, SGST: txtSGSTPercent.Text, CGST: txtCGSTPercent.Text, SGSTAMT: txtSGSTAmount.Text, CGSTAMT: txtCGSTAmount.Text, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString());//

                if (sResult.StartsWith("AD#"))
                {
                    string[] Array = sResult.Split('#');

                    string title = sResult.Replace("\n", " "); ;
                    string body = "Welcome to ASPSnippets.com";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "ShowPopup('Stock Create Succefully!');", true);


                    clear();
               }

                else if (sResult == "ALREADYEXISTS")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Stock Already Present!')", true);
                   
                }
                else if (sResult.StartsWith("NOTEXISTS"))
                {

                    // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Please Try Again!!!!!');", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Stock Not Available Under Vender Master!')", true);
                }
            }


            catch (Exception EX)
            {
                WebMsgBox.Show(EX.Message.ToString(), this.Page);
                ExceptionLogging.SendErrorToText(EX);

            }
        }

        if (ViewState["Flag"].ToString() == "ATH")
        {
            try
            {

                sResult = CS.OpeningStock(FLAG: "ATH", BRCD: txtBRCD.Text, VENDORID: txtVendorID.Text, PRODID: txtProductID.Text, QTY: txtQuantity.Text, UNITCOST: txtRate.Text, SGST: txtSGSTPercent.Text, CGST: txtCGSTPercent.Text, SGSTAMT: txtSGSTAmount.Text, CGSTAMT: txtCGSTAmount.Text,AMOUNT:txtTotalAmount.Text, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString());//

                if (sResult.StartsWith("ATH#"))
                {
                    string[] Array = sResult.Split('#');

                    string title = sResult.Replace("\n", " "); ;
                    string body = "Welcome to ASPSnippets.com";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "ShowPopup('Authorize Succefully!');", true);
                    clear();
                }

                else if (sResult == "NOTEXISTS")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Not Present!')", true);

                }
                else if (sResult.StartsWith("NOTAUTH"))
                {

                    // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Please Try Again!!!!!');", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Your Are Not Authorize Person')", true);
                }
                else if (sResult.StartsWith("ALREADYAUTH"))
                {

                    // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Please Try Again!!!!!');", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Already Authorize ')", true);
                }
            }


            catch (Exception EX)
            {
                WebMsgBox.Show(EX.Message.ToString(), this.Page);
                ExceptionLogging.SendErrorToText(EX);

            }
        }
      
        if (ViewState["Flag"].ToString() == "MD")
        {
            try
            {

                sResult = CS.OpeningStock(FLAG: "MD", BRCD: txtBRCD.Text, VENDORID: txtVendorID.Text, PRODID: txtProductID.Text, QTY: txtQuantity.Text, UNITCOST: txtRate.Text, SGST: txtSGSTPercent.Text, CGST: txtCGSTPercent.Text, SGSTAMT: txtSGSTAmount.Text, CGSTAMT: txtCGSTAmount.Text, AMOUNT: txtTotalAmount.Text, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString());//

                if (sResult.StartsWith("MD#"))
                {
                    string[] Array = sResult.Split('#');

                    string title = sResult.Replace("\n", " "); ;
                    string body = "Welcome to ASPSnippets.com";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "ShowPopup('Modify Succefully!');", true);
                    clear();

                }

                else if (sResult == "ALREADYAUTH")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Already Authoize Stock!')", true);

                }
                else if (sResult.StartsWith("NOTEXISTS"))
                {

                    // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Please Try Again!!!!!');", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Stock Not Presnt Try Again!')", true);
                }
            }


            catch (Exception EX)
            {
                WebMsgBox.Show(EX.Message.ToString(), this.Page);
                ExceptionLogging.SendErrorToText(EX);

            }
            
        }
      
        if (ViewState["Flag"].ToString() == "DEL")
        {
            try
            {

                sResult = CS.OpeningStock(FLAG: "DEL", BRCD: txtBRCD.Text, VENDORID: txtVendorID.Text, PRODID: txtProductID.Text, QTY: txtQuantity.Text, UNITCOST: txtRate.Text, SGST: txtSGSTPercent.Text, CGST: txtCGSTPercent.Text, SGSTAMT: txtSGSTAmount.Text, CGSTAMT: txtCGSTAmount.Text, AMOUNT: txtTotalAmount.Text, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString());//

                if (sResult.StartsWith("DEL#"))
                {
                    string[] Array = sResult.Split('#');

                    string title = sResult.Replace("\n", " "); ;
                    string body = "Welcome to ASPSnippets.com";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "ShowPopup('Delete Succefully!');", true);

                }

                else if (sResult == "ALREADYAUTH")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Already Authorize!')", true);

                }
                else if (sResult.StartsWith("NOTEXISTS"))
                {

                    // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Please Try Again!!!!!');", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Stock not Present')", true);
                }
                clear();
            }


            catch (Exception EX)
            {
                WebMsgBox.Show(EX.Message.ToString(), this.Page);
                ExceptionLogging.SendErrorToText(EX);

            }
        }

       
    }
    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        ShowProduct();
    }
    public void ShowProduct()
    {
       

        DataTable dt = new DataTable();
        string str = "0";
        dt = CS.ProductCalculate(FLAG: "GETPRODUCTCALC", PRODUCTID: txtProductID.Text,VendorID:txtVendorID.Text);
        if (dt.Rows.Count != 0)
        {
            if (txtQuantity.Text == "") txtQuantity.Text = "1";
            txtRate.Text = dt.Rows[0]["RATE"].ToString();
            txtSGSTPercent.Text = dt.Rows[0]["SGST"].ToString();
            txtSGSTAmount.Text = (Convert.ToDouble(dt.Rows[0]["SGSTAMT"].ToString())*Convert.ToDouble(txtQuantity.Text.ToString())).ToString();
            txtCGSTPercent.Text = dt.Rows[0]["CGST"].ToString();
            txtCGSTAmount.Text = (Convert.ToDouble(dt.Rows[0]["CGSTAMT"].ToString()) * Convert.ToDouble(txtQuantity.Text.ToString())).ToString(); 
            txtTotalAmount.Text = (Convert.ToDouble(dt.Rows[0]["TOTALAMT"].ToString()) * Convert.ToDouble(txtQuantity.Text.ToString())).ToString(); 
           
        }


    }
    protected void GrdStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdStock.PageIndex = e.NewPageIndex;
             SHOWStockData();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    //protected void GrdStock_RowCommand(object sender, GridViewCommandEventArgs e)
    //{

    //}
    protected void GrdStock_SelectedIndexChanged(object sender, EventArgs e)
    {
        SHOWStockData();
    }
    protected void LnkAutorise_Click(object sender, EventArgs e)
    {
        LinkButton lnkedit = (LinkButton)sender;
        string str = lnkedit.CommandArgument.ToString();
        string[] ARR = str.Split(',');
        ViewState["BRCD"] = ARR[0].ToString();
        ViewState["PRODID"] = ARR[1].ToString();
        ViewState["VENDORID"] = ARR[2].ToString();
        ShowStock();
   
      
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
       
        LinkButton lnkedit = (LinkButton)sender;
        string str = lnkedit.CommandArgument.ToString();
        string[] ARR = str.Split(',');
        ViewState["BRCD"] = ARR[0].ToString();
        ViewState["PRODID"] = ARR[1].ToString();
        ViewState["VENDORID"] = ARR[2].ToString();
        BtnCancle.Visible = true;
        BtnVenderMasterAdd.Visible = true;

        ShowStock();


    }

    public void SHOWStockData()
    {

        txtBRCD.Text = Session["BRCD"].ToString();
        DataTable dt = new DataTable();

        CS.GridViewStock("SHOWUNAUTHORIZE", txtBRCD.Text, GrdStock,VENDORID:txtVendorID.Text,PRODID:txtProductID.Text);
        dt = CS.VieWStock(FLAG: "SHOWUNAUTHORIZE", BRCD: txtBRCD.Text);


    }
    public void ShowStock()
    {
       
        txtBRCD.Text = ViewState["BRCD"].ToString();
        txtProductID.Text = ViewState["PRODID"].ToString();
        txtVendorID.Text = ViewState["VENDORID"].ToString(); 
         DataTable dt = new DataTable();
        string str = "0";
        dt = CS.VieWStockPRID(FLAG: "SHOW", BRCD: txtBRCD.Text, VENDORID: txtVendorID.Text, PRODID: txtProductID.Text);
        if (dt.Rows.Count != 0)
        {

        //   txtVendorID.Text = dt.Rows[0]["VENDORID"].ToString();
            txtVendorName.Text = dt.Rows[0]["VENDERNAME"].ToString();
            txtProductName.Text = dt.Rows[0]["PRODNAME"].ToString();
            txtRate.Text = dt.Rows[0]["UNITCOST"].ToString();
            txtQuantity.Text = dt.Rows[0]["QTY"].ToString();
          // txtBRCD.Text = dt.Rows[0]["BRCD"].ToString();
            txtSGSTAmount.Text = dt.Rows[0]["SGSTAMT"].ToString();
            txtSGSTPercent.Text = dt.Rows[0]["SGSTPER"].ToString();
            txtCGSTAmount.Text = dt.Rows[0]["CGSTAMT"].ToString();
            txtCGSTPercent.Text = dt.Rows[0]["CGSTPER"].ToString();
            txtTotalAmount.Text = dt.Rows[0]["AMOUNT"].ToString();
            txtCGSTAmount.Text = dt.Rows[0]["CGSTAMT"].ToString();

        }


    }
    protected void BtnCancle_Click(object sender, EventArgs e)
    {
       
            try
            {

                sResult = CS.OpeningStock(FLAG: "MD ROLLBACK", BRCD: txtBRCD.Text, VENDORID: txtVendorID.Text, PRODID: txtProductID.Text, QTY: txtQuantity.Text, UNITCOST: txtRate.Text, SGST: txtSGSTPercent.Text, CGST: txtCGSTPercent.Text, SGSTAMT: txtSGSTAmount.Text, CGSTAMT: txtCGSTAmount.Text, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString());//

                if (sResult.StartsWith("MDRLBK#"))
                {
                    string[] Array = sResult.Split('#');

                    string title = sResult.Replace("\n", " "); ;
                    string body = "Welcome to ASPSnippets.com";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "ShowPopup('Cancle Succefully!');", true);

                }

                else if (sResult == "NOTMODIFY")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Not Modify!')", true);

                }
                else if (sResult.StartsWith("NOTEXISTS"))
                {

                    // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Please Try Again!!!!!');", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Stock not Present')", true);
                }
                clear();
            }


            catch (Exception EX)
            {
                WebMsgBox.Show(EX.Message.ToString(), this.Page);
                ExceptionLogging.SendErrorToText(EX);

            }
        
    }
    public void clear1()
    {
        txtVendorID.Text = "";
        txtVendorName.Text = "";
        txtProductID.Text = "";
        txtProductName.Text = "";
        txtQuantity.Text = "";
        txtSGSTAmount.Text = "";
        txtSGSTPercent.Text = "";
        txtTotalAmount.Text = "";
        txtCGSTAmount.Text = "";
        txtCGSTPercent.Text = "";
        txtRate.Text = "";
    }
    public void clear()
    {
        txtBRCD.Text = "";
        txtVendorID.Text="";
        txtVendorName.Text="";
        txtProductID.Text="";
        txtProductName.Text = "";
        txtQuantity.Text = "";
        txtSGSTAmount.Text = "";
        txtSGSTPercent.Text = "";
        txtTotalAmount.Text = "";
        txtCGSTAmount.Text = "";
        txtCGSTPercent.Text = "";
        txtRate.Text = "";
    }
    protected void BTNEXIT1_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmAVS51173.aspx", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtVendorID_TextChanged(object sender, EventArgs e)
    {
        txtVendorName.Text = CS.GetVendorID(txtVendorID.Text, Session["MID"].ToString());
    }
}