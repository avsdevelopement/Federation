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

public partial class FrmAVS51173 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAVS51173 CS = new ClsAVS51173();
    ClsCommon cmn = new ClsCommon();
    DataTable DT = new DataTable();
    string sResult = "";
    int Result = 0;



    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["UserName"] == null)
        //    Response.Redirect("FrmLogin.aspx");

        //if (!IsPostBack)
        //{
        //    BD.BindState(ddlParmState);

        //}
        //ViewState["Flag"] = Request.QueryString["Flag"].ToString();

    }
    protected void TxtVenderno_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtVenderName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtComName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtMobileNumber_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlParmState_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   
    protected void Vender_Click(object sender, EventArgs e)
    {
      //  ViewState["Flag"] = "AD";
       Response.Redirect("FrmAVS51174.aspx");

    }
    protected void btnProduct_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "PT";
       
        Response.Redirect("FrmAVS51175.aspx");
    }
    protected void btnPurchase_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "PRT";
        Response.Redirect("FrmAVS51176.aspx");
    }
    protected void BTNeXIT1_Click(object sender, EventArgs e)
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
    protected void btnExit2_Click(object sender, EventArgs e)
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
    protected void btnExit3_Click(object sender, EventArgs e)
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
    protected void BtnVenderMaster_Click(object sender, EventArgs e)
    {
        //    if (Convert.ToString(ViewState["Flag"]) == "AD")
        //        TxtVenderno.Enabled = false;


        //    {
        //        if (TxtVenderName.Text == "")
        //        {
        //            //WebMsgBox.Show("Enter Vendor Name ..!!", this.Page);
        //            //return ;
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Enter Vendor Name ..!!')", true);
        //        }
        //        if (txtComName.Text == "")
        //        {
        //            WebMsgBox.Show("Enter Contact Person  Name ..!!", this.Page);
        //            return;
        //        }

        //        if (txtMobileNumber.Text == "")
        //        {
        //            WebMsgBox.Show("Enter Mobile Number ..!!", this.Page);
        //            return;
        //        }

        //        if (txtEmailId.Text == "")
        //        {
        //            WebMsgBox.Show("Enter Email Id ..!!", this.Page);
        //            return;
        //        }
        //        if (txtAddline1.Text == "")
        //        {
        //            WebMsgBox.Show("Enter Address  ..!!", this.Page);
        //            return;
        //        }
        //        if (ddlParmState.SelectedValue == "0")
        //        {
        //            WebMsgBox.Show("Select  State  ..!!", this.Page);
        //            return;
        //        }

        //        if (txtParmCity.Text == "")
        //        {
        //            WebMsgBox.Show("Enter City  ..!!", this.Page);
        //            return;
        //        }
        //        if (txtPin.Text == "")
        //        {
        //            WebMsgBox.Show("Enter Pincode  ..!!", this.Page);
        //            return;
        //        }


        //        try
        //        {


        //            CS.VendorMaster(FLAG: "AD", VENDERNAME: TxtVenderName.Text.Trim(), CNTCTPRSNNAME: txtComName.Text.Trim(), MOBNO: txtMobileNumber.Text, EMAILID: txtEmailId.Text, ADDRESS1: txtAddline1.Text, ADDRESS2: txtAddline2.Text, STATE: ddlParmState.SelectedValue, CITY: txtParmCity.Text, PINCODE: txtPin.Text, ENTRYDATE: Session["EntryDate"].ToString());

        //            if (sResult.Contains("AD"))
        //            {
        //                string title = sResult.Replace("\n", " "); ;
        //                string body = "Welcome to ASPSnippets.com";
        //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "');", true);

        //            }
        //            else
        //            {
        //                // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Please Try Again!!!!!');", true);
        //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Try Again!')", true);
        //            }

        //        }

        //        catch (Exception EX)
        //        {
        //            WebMsgBox.Show(EX.Message.ToString(), this.Page);
        //            ExceptionLogging.SendErrorToText(EX);

        //        }
        //    }
    }
    protected void btnStock_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmAVS51177.aspx");
    }
    protected void btnIssue_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmAVSSales.aspx");
    }
    protected void btnUse_Click(object sender, EventArgs e)
    {

        Response.Redirect("FrmAVS51179.aspx");
    }
    protected void btnClosing_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmAVS51180.aspx");
    }
}


