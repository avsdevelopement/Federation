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
public partial class FrmAVS51174 : System.Web.UI.Page
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
            ViewState["Flag"] = "AD";
            TxtVenderno.ReadOnly = true;
            BD.BindState(ddlParmState);
            autoglname.ContextKey = Session["MID"].ToString();
            TxtVenderno.Text = CS.VendorMaster(FLAG: "GETID");
            SHOWUNAUTHORIZE();
          //  BtnVenderMasterAdd.Visible = false;
        }


    }
    protected void TxtVenderno_TextChanged(object sender, EventArgs e)
    {
           // SHOW();
    }
    protected void TxtVenderName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtVenderName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TxtVenderName.Text = CT[0].ToString();
                TxtVenderno.Text = CT[1].ToString();
                string[] GLS = CS.GetName(TxtVenderno.Text, Session["MID"].ToString()).Split('_');

            }
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

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
    protected void BtnVenderMaster_Click(object sender, EventArgs e)
    {
        //if (ViewState["Flag"].ToString() == "AD")

        if (ViewState["Flag"].ToString() == "AD")
        {
            if (TxtVenderName.Text == "")
            {
                //WebMsgBox.Show("Enter Vendor Name ..!!", this.Page);
                //return ;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Enter Vendor Name ..!!')", true);
            }
            if (txtComName.Text == "")
            {
                WebMsgBox.Show("Enter Contact Person  Name ..!!", this.Page);
                return;
            }

            if (txtMobileNumber.Text == "")
            {
                WebMsgBox.Show("Enter Mobile Number ..!!", this.Page);
                return;
            }

            //if (txtEmailId.Text == "")
            //{
            //    WebMsgBox.Show("Enter Email Id ..!!", this.Page);
            //    return;
            //}
            //if (txtAddline1.Text == "")
            //{
            //    WebMsgBox.Show("Enter Address  ..!!", this.Page);
            //    return;
            //}
            //if (ddlParmState.SelectedValue == "0")
            //{
            //    WebMsgBox.Show("Select  State  ..!!", this.Page);
            //    return;
            //}

            //if (txtParmCity.Text == "")
            //{
            //    WebMsgBox.Show("Enter City  ..!!", this.Page);
            //    return;
            //}
            //if (txtPin.Text == "")
            //{
            //    WebMsgBox.Show("Enter Pincode  ..!!", this.Page);
            //    return;
            //}


            try
            {


                sResult = CS.VendorMaster(FLAG: "AD", VENDORID: TxtVenderno.Text.Trim(), VENDERNAME: TxtVenderName.Text.Trim(), CNTCTPRSNNAME: txtComName.Text.Trim(), MOBNO: txtMobileNumber.Text, EMAILID: txtEmailId.Text, ADDRESS1: txtAddline1.Text, ADDRESS2: txtAddline2.Text, STATE: ddlParmState.SelectedValue, CITY: txtParmCity.Text, PINCODE: txtPin.Text, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString(), GSTNO:txtGSTNO.Text.ToString());

                if (sResult.StartsWith("AD#"))
                {
                    string[]  Array = sResult.Split('#');

                    string title = sResult.Replace("\n", " "); ;
                    string body = "Welcome to ASPSnippets.com";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "ShowPopup('Vendor " + Array[1] + " " + Array[2] + " Create Succefully!');", true);
                    Clear();
                }
               
                 else if (sResult == "ALREADYEXISTS")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vendor Already Present!')", true);
                        Clear();
                    }
                else if (sResult.StartsWith("ERROR") )
                {

                 // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Please Try Again!!!!!');", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Try Again!')", true);
                }

            }

            catch (Exception EX)
            {
                WebMsgBox.Show(EX.Message.ToString(), this.Page);
                ExceptionLogging.SendErrorToText(EX);

            }
        }

        else if (ViewState["Flag"].ToString() == "MD")
        {
            ViewState["VENDORID"] = "";
            try
            {


                sResult = CS.VendorMaster(FLAG: "MD", VENDORID: TxtVenderno.Text.Trim(), VENDERNAME: TxtVenderName.Text.Trim(), CNTCTPRSNNAME: txtComName.Text.Trim(), MOBNO: txtMobileNumber.Text, EMAILID: txtEmailId.Text, ADDRESS1: txtAddline1.Text, ADDRESS2: txtAddline2.Text, STATE: ddlParmState.SelectedValue, CITY: txtParmCity.Text, PINCODE: txtPin.Text, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString(), GSTNO: txtGSTNO.Text.ToString());

                if (sResult.StartsWith("MD#"))
                {
               
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Modify Successfully!')", true);
                    Clear();
                }

                else if (sResult == "NOTEXISTS")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vendor Not Present!')", true);
                   // Clear();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vendor Not Modify Try Again!')", true);
                 
                }


            }

            catch (Exception EX)
            {
                WebMsgBox.Show(EX.Message.ToString(), this.Page);
                ExceptionLogging.SendErrorToText(EX);

            }


        }
        else if (ViewState["Flag"].ToString() == "ATH")
        {
            ViewState["VENDORID"] = "";
            try
            {
                sResult = CS.VendorMaster(FLAG: "ATH", VENDORID: TxtVenderno.Text.Trim(), VENDERNAME: TxtVenderName.Text.Trim(), CNTCTPRSNNAME: txtComName.Text.Trim(), MOBNO: txtMobileNumber.Text, EMAILID: txtEmailId.Text, ADDRESS1: txtAddline1.Text, ADDRESS2: txtAddline2.Text, STATE: ddlParmState.SelectedValue, CITY: txtParmCity.Text, PINCODE: txtPin.Text, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString(), GSTNO: txtGSTNO.Text.ToString());

                if (sResult.StartsWith("ATH#"))
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Authorize Successfully!')", true);
                    Clear();
                }
                else if (sResult == "NOTEXISTS")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vendor Not Present!')", true);
                        Clear();
                    }
                else if (sResult == "NOTAUTH")
                {
                    // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Please Try Again!!!!!');", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You Are Not Authorize Person!')", true);
                }
            }
            catch (Exception EX)
            {
                WebMsgBox.Show(EX.Message.ToString(), this.Page);
                ExceptionLogging.SendErrorToText(EX);

            }
        }
        else if (ViewState["Flag"].ToString() == "DEL")
        {

            try
            {
                sResult = CS.VendorMaster(FLAG: "DEL", VENDORID: TxtVenderno.Text.Trim(), VENDERNAME: TxtVenderName.Text.Trim(), CNTCTPRSNNAME: txtComName.Text.Trim(), MOBNO: txtMobileNumber.Text, EMAILID: txtEmailId.Text, ADDRESS1: txtAddline1.Text, ADDRESS2: txtAddline2.Text, STATE: ddlParmState.SelectedValue, CITY: txtParmCity.Text, PINCODE: txtPin.Text, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString(), GSTNO: txtGSTNO.Text.ToString());

                if (sResult.StartsWith("DEL#"))
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Delete Successfully!')", true);
                    Clear();
                }
                else if (sResult == "NOTEXISTS")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vendor Not Present!')", true);
                    Clear();
                }
                else if (sResult == "ALREADYPRODMAST")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Already Vendor Product Exists!')", true);
                    Clear();
                }
                else
                {
                    // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Please Try Again!!!!!');", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Try Again!')", true);
                }
            }
            catch (Exception EX)
            {
                WebMsgBox.Show(EX.Message.ToString(), this.Page);
                ExceptionLogging.SendErrorToText(EX);

            }
        }
    }

    public void SHOW()
    {


        DataTable dt = new DataTable();
        string str = "0";
        dt = CS.VieWVendor(FLAG: "SHOW", VENDORID: TxtVenderno.Text);
        if (dt.Rows.Count > 0)
        {
            TxtVenderName.Text = dt.Rows[0]["VENDERNAME"].ToString();
            txtComName.Text = dt.Rows[0]["CNTCTPRSNNAME"].ToString();
            txtMobileNumber.Text = dt.Rows[0]["MOBNO"].ToString();
            txtEmailId.Text = dt.Rows[0]["EMAILID"].ToString();
            txtAddline1.Text = dt.Rows[0]["ADDRESS1"].ToString();
            txtAddline2.Text = dt.Rows[0]["ADDRESS2"].ToString();
            ddlParmState.SelectedValue = dt.Rows[0]["STATE"].ToString();
            txtParmCity.Text = dt.Rows[0]["CITY"].ToString();
            txtPin.Text = dt.Rows[0]["PINCODE"].ToString();
            txtGSTNO.Text = dt.Rows[0]["GSTNO"].ToString();
        }
        else
        {
            WebMsgBox.Show("Vendor not exists!", this.Page);
            Clear();
        }


    }
      public void SHOW2()
    {
          TxtVenderno.Text=ViewState["VENDORID"].ToString();

        DataTable dt = new DataTable();
        string str = "0";
        dt = CS.VieWVendor(FLAG: "SHOW", VENDORID: TxtVenderno.Text );
        if (dt.Rows.Count > 0)
        {
            TxtVenderName.Text = dt.Rows[0]["VENDERNAME"].ToString();
            txtComName.Text = dt.Rows[0]["CNTCTPRSNNAME"].ToString();
            txtMobileNumber.Text = dt.Rows[0]["MOBNO"].ToString();
            txtEmailId.Text = dt.Rows[0]["EMAILID"].ToString();
            txtAddline1.Text = dt.Rows[0]["ADDRESS1"].ToString();
            txtAddline2.Text = dt.Rows[0]["ADDRESS2"].ToString();
            ddlParmState.SelectedValue = dt.Rows[0]["STATE"].ToString();
            txtParmCity.Text = dt.Rows[0]["CITY"].ToString();
            txtPin.Text = dt.Rows[0]["PINCODE"].ToString();
            txtGSTNO.Text = dt.Rows[0]["GSTNO"].ToString();
        }
        else
        {
            WebMsgBox.Show("Vendor not exists!", this.Page);

        }

    }
    

    public void SHOWUNAUTHORIZE()
    {


        DataTable dt = new DataTable();
        string str = "0";
        CS.GridViewBind("SHOWUNAUTHORIZE", Session["MID"].ToString(), GrdAcc);
        dt = CS.VieWVendor(FLAG: "SHOWUNAUTHORIZE", VENDORID: TxtVenderno.Text);
       

    }
    int GetColumnIndexByName(GridViewRow row, string columnName)
    {
        int columnIndex = 0;
        foreach (DataControlFieldCell cell in row.Cells)
        {
            if (cell.ContainingField is BoundField)
                if (((BoundField)cell.ContainingField).DataField.Equals(columnName))
                    break;
            columnIndex++; // keep adding 1 while we don't have the correct name
        }
        return columnIndex;
    }


    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Flag"] = "AD";
            BD.BindState(ddlParmState);
            TxtVenderno.Text = CS.VendorMaster(FLAG: "GETID");
            TxtVenderno.ReadOnly = true;
            TxtVenderName.Enabled = true;
            txtMobileNumber.Enabled = true;
            txtComName.Enabled = true;
            txtEmailId.Enabled = true;
            txtAddline1.Enabled = true;
            txtAddline2.Enabled = true;
            ddlParmState.Enabled = true;
            txtParmCity.Enabled = true;
            txtPin.Enabled = true;
            BtnVenderMasterAdd.Text = "Create";
            BtnVenderMasterAdd.Visible = true;
            SHOWUNAUTHORIZE();
           Clear1();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void lnkModify_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "MD";
        BD.BindState(ddlParmState);
        TxtVenderno.ReadOnly = true;
        BtnVenderMasterAdd.Text = "Modify";
        TxtVenderno.ReadOnly = true;
        TxtVenderName.Enabled = true;
        txtMobileNumber.Enabled = true;
        txtComName.Enabled = true;
        txtEmailId.Enabled = true;
        txtAddline1.Enabled = true;
        txtAddline2.Enabled = true;
        ddlParmState.Enabled = true;
        txtParmCity.Enabled = true;
        txtPin.Enabled = true;
        BtnVenderMasterAdd.Visible = false;
        BtnCancle.Visible = true;
        SHOWUNAUTHORIZE();
        Clear();
    }

    public void Clear()
    {
        TxtVenderno.Text = "";
        TxtVenderName.Text = "";
        txtComName.Text = "";
        txtMobileNumber.Text = "";
        txtEmailId.Text = "";
        txtAddline1.Text = "";
        txtAddline2.Text = "";
        txtParmCity.Text = "";
        txtPin.Text = "";
        ddlParmState.SelectedValue = "0";
        TxtVenderno.Focus();

    }
    public void Clear1()
    {
        
        TxtVenderName.Text = "";
        txtComName.Text = "";
        txtMobileNumber.Text = "";
        txtEmailId.Text = "";
        txtAddline1.Text = "";
        txtAddline2.Text = "";
        txtParmCity.Text = "";
        txtPin.Text = "";
        ddlParmState.SelectedValue = "0";
        TxtVenderno.Focus();

    }

    protected void LnkAuthorize_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "ATH";
        BD.BindState(ddlParmState);
        SHOWUNAUTHORIZE();
        TxtVenderno.ReadOnly = true;
        TxtVenderName.Enabled = false;
        txtMobileNumber.Enabled = false;
        txtComName.Enabled = false;
        txtEmailId.Enabled = false;
        txtAddline1.Enabled = false;
        txtAddline2.Enabled = false;
        ddlParmState.Enabled = false;
        txtParmCity.Enabled = false;
        txtPin.Enabled = false;
        BtnCancle.Visible = false;
        BtnDelete2.Visible = false;
        BtnVenderMasterAdd.Text = "Authorize ";
        BtnVenderMasterAdd.Visible = false;
        Clear();
        
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "DEL";
        BD.BindState(ddlParmState);
        TxtVenderno.ReadOnly = true;
        TxtVenderName.Enabled = false;
        txtMobileNumber.Enabled = false;
        txtComName.Enabled = false;
        txtEmailId.Enabled = false;
        txtAddline1.Enabled = false;
        txtAddline2.Enabled = false;
        ddlParmState.Enabled = false;
        txtParmCity.Enabled = false;
        txtPin.Enabled = false;
        BtnVenderMasterAdd.Text = "Delete ";
        BtnVenderMasterAdd.Visible = false;
        SHOWUNAUTHORIZE();
       Clear();
    }
    protected void GrdAcc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdAcc.PageIndex = e.NewPageIndex;
    }
    protected void GrdAcc_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void LnkAutorise_Click(object sender, EventArgs e)
    {
        LinkButton lnkedit = (LinkButton)sender;
        string VendorID = lnkedit.CommandArgument.ToString();
        ViewState["VENDORID"] = VendorID;

        SHOW2();

    }
    protected void lnkDelete1_Click(object sender, EventArgs e)
    {

    }
    protected void GrdAcc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }
    protected void BtnDelete2_Click(object sender, EventArgs e)
    {

    }
    protected void BtnCancle_Click(object sender, EventArgs e)
    {
      
        try
        {
           sResult1 = CS.VieWVendorM(FLAG: "MD ROLLBACK", VENDORID: TxtVenderno.Text);
            

            if (sResult1 == "NOTEXISTS")
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vendor Not Present!')", true);
                Clear();
            }

            else if (sResult1 == "NOTMODIFY")
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Not Modify!')", true);
                Clear();
            }
            else if (sResult1.StartsWith("MDRLBK#"))
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cancel Successfuly!')", true);
                Clear();
            }
        }
        catch (Exception EX)
        {
            WebMsgBox.Show(EX.Message.ToString(), this.Page);
            ExceptionLogging.SendErrorToText(EX);

        }
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        LinkButton lnkedit = (LinkButton)sender;
        string VendorID = lnkedit.CommandArgument.ToString();
        ViewState["VENDORID"] = VendorID;
        BtnVenderMasterAdd.Visible = true;
        SHOW2();
    }
    protected void btnClearAll_Click(object sender, EventArgs e)
    {
        Clear(); ;
    }
    protected void BtnUpdateGST_Click(object sender, EventArgs e)
    {
        lblgst.Visible = true;
        txtGSTNO.Visible = true;
    }
}