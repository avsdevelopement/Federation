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
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OleDb;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System;
using System.Data.Common; 


public partial class FrmAVS51175 : System.Web.UI.Page
{ ClsBindDropdown BD = new ClsBindDropdown();
    ClsAVS51173 CS = new ClsAVS51173();
    ClsCommon cmn = new ClsCommon();
    DataTable DT = new DataTable();
    DbConnection conn = new DbConnection();
    string sResult = "", sResult1 = "";
    int Result = 0;
    public static string strConn;
    public static string strLocIp;
    public static string strLocIpStatic;
    public static int intUserId;
    public static int intSocId;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
            Response.Redirect("FrmLogin.aspx");

        if (!IsPostBack)
        {
            ViewState["Flag"] = "AD";
            TxtProductId.ReadOnly = true;
            autoglname.ContextKey = Session["MID"].ToString();
            TxtProductId.Text = CS.ProductMaster(FLAG: "GETID");
            SHOWUNAUTHORIZE();
            strLocIpStatic = Request.ServerVariables["REMOTE_ADDR"].ToString();
            strLocIp = strLocIp + ">>" + strLocIpStatic;
            string constr = "";
        }
    }
    protected void btnExit1_Click(object sender, EventArgs e)
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
    protected void Btnclear_Click(object sender, EventArgs e)
    {

    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (ViewState["Flag"].ToString() == "AD")
        {
            //TxtProductId.Text = CS.ProductMaster(FLAG: "GETID");
            if (TxtVendorId.Text == "")
            {
                //WebMsgBox.Show("Enter Vendor Name ..!!", this.Page);
                //return ;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Enter Vendor ID ..!!')", true);
            }
            if (txtProductNAme.Text == "")
            {
                WebMsgBox.Show("Enter Product  Name..!!", this.Page);
                return;
            }

            if (TxtRate.Text == "")
            {
                WebMsgBox.Show("Enter Rate..!!", this.Page);
                return;
            }

        //    if (TxtCGST.Text == "")
          //  {
           //     WebMsgBox.Show("Enter CGST..!!", this.Page);
           //     return;
           // }
           // if (txtSGstPrcd.Text == "")
           // {
            //    WebMsgBox.Show("Enter SGST Product Code..!!", this.Page);
             //   return;
           // }
           

           // if (txtSGST.Text == "")
           // {
           //     WebMsgBox.Show("Enter SGST..!!", this.Page);
            //    return;
          //  }
           // if (txtCGDTPROCODE.Text == "")
           // {
            //    WebMsgBox.Show("Enter CGST Product Code..!!", this.Page);
            //    return;
           // }


            try
            {
              // sResult = CS.DeadStock("VENDORID", TxtVendorId.Text);
               // if (sResult == "")
                //{
                    sResult = CS.ProductMaster(FLAG: "AD", PRODID: TxtProductId.Text, VENDORID: TxtVendorId.Text.Trim(), PRODNAME: txtProductNAme.Text, RATE: TxtRate.Text, SGST: txtSGST.Text, CGST: TxtCGST.Text, SGSTPRD: txtSGstPrcd.Text, CGSTPRD: txtCGDTPROCODE.Text, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString());//

                    if (sResult.StartsWith("AD#"))
                    {
                        string[] Array = sResult.Split('#');

                        string title = sResult.Replace("\n", " "); ;
                        string body = "Welcome to ASPSnippets.com";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "ShowPopup('Product " + Array[1] + " " + Array[2] + " Create Succefully!');", true);
                        Clear1();
                        SHOWUNAUTHORIZE();
                    }

                    else if (sResult == "ALREADYEXISTS")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Already Present!')", true);
                        Clear1();
                    }
                    else if (sResult.StartsWith("ERROR"))
                    {

                        // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Please Try Again!!!!!');", true);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Try Again!')", true);
                    }
               // }
                if (sResult == "NOTAUTH")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vendor Not Authorize!')", true);
                    TxtVendorId.Text = "";
                        
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

            try
            {


                sResult = CS.ProductMaster(FLAG: "MD", PRODID: TxtProductId.Text, VENDORID: TxtVendorId.Text.Trim(), PRODNAME: txtProductNAme.Text, RATE: TxtRate.Text, SGST: txtSGST.Text, CGST: TxtCGST.Text, SGSTPRD: txtSGstPrcd.Text, CGSTPRD: txtCGDTPROCODE.Text, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString());//

                if (sResult.StartsWith("MD#"))
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Modify Successfully!')", true);
                    Clear();
                    SHOWUNAUTHORIZE();
                }
                else if (sResult == "NOTEXISTS")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product  Not Present!')", true);
                  //  Clear();
                }
                else
                {
                    
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Try Again!')", true);
                    Clear();
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

            try
            {
                sResult = CS.ProductMaster(FLAG: "ATH", PRODID: TxtProductId.Text, VENDORID: TxtVendorId.Text.Trim(), PRODNAME: txtProductNAme.Text, RATE: TxtRate.Text, SGST: txtSGST.Text, CGST: TxtCGST.Text, SGSTPRD: txtSGstPrcd.Text, CGSTPRD: txtCGDTPROCODE.Text, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString());//

                if (sResult.StartsWith("ATH#"))
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Authorize Successfully!')", true);
                    Clear();
                    SHOWUNAUTHORIZE();
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
                sResult = CS.ProductMaster(FLAG: "DEL", PRODID: TxtProductId.Text, VENDORID: TxtVendorId.Text.Trim(), PRODNAME: txtProductNAme.Text, RATE: TxtRate.Text, SGST: txtSGST.Text, CGST: TxtCGST.Text, SGSTPRD: txtSGstPrcd.Text, CGSTPRD: txtCGDTPROCODE.Text, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString());//

                if (sResult.StartsWith("DEL#"))
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Delete Successfully!')", true);
                    Clear();
                    SHOWUNAUTHORIZE();
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
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Flag"] = "AD";
            TxtProductId.Text = CS.ProductMaster(FLAG: "GETID");
            TxtProductId.ReadOnly = true;
            TxtVendorId.Enabled = true;
            txtVendorName.Enabled = true;
            txtProductNAme.Enabled = true;
            TxtRate.Enabled = true;
            txtSGST.Enabled = true;
            txtSGstPrcd.Enabled = true;
            TxtCGST.Enabled = true;
            txtCGDTPROCODE.Enabled = true;
            BtnCancle.Visible = true;
            BtnSubmit.Text = "Create";
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
        try
        {

            ViewState["Flag"] = "MD";
            SHOWUNAUTHORIZE();
            TxtProductId.ReadOnly = true;
            TxtVendorId.Enabled = true;
            txtVendorName.Enabled = true;
            txtProductNAme.Enabled = true;
            TxtRate.Enabled = true;
            txtSGST.Enabled = true;
            txtSGstPrcd.Enabled = true;
            TxtCGST.Enabled = true;
            txtCGDTPROCODE.Enabled = true;
            BtnCancle.Visible = true;
            BtnSubmit.Text = "Modify";
            BtnSubmit.Visible = false;
            Clear();
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
            SHOWUNAUTHORIZE();
            TxtProductId.ReadOnly = true;
            TxtVendorId.Enabled = false;
            txtVendorName.Enabled = false;
            txtProductNAme.Enabled = false;
            TxtRate.Enabled = false;
            txtSGST.Enabled = false;
            txtSGstPrcd.Enabled = false;
            TxtCGST.Enabled = false;
            txtCGDTPROCODE.Enabled = false;
            BtnCancle.Visible = true;
            BtnSubmit.Text = "Authorize";
            BtnSubmit.Visible = false;
            Clear();
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
            SHOWUNAUTHORIZE();
            TxtProductId.ReadOnly = true;
            TxtVendorId.Enabled = false;
            txtVendorName.Enabled = false;
            txtProductNAme.Enabled = false;
            TxtRate.Enabled = false;
            txtSGST.Enabled = false;
            txtSGstPrcd.Enabled = false;
            TxtCGST.Enabled = false;
            txtCGDTPROCODE.Enabled = false;
            BtnCancle.Visible = true;
            BtnSubmit.Text = "Delete";
             Clear();
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
                TxtVendorId.Text = CT[1].ToString();
                string[] GLS = CS.GetName(TxtVendorId.Text, Session["MID"].ToString()).Split('_');

            }
           

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void Clear()
    {
        TxtProductId.Text = "";
        txtVendorName.Text = "";
        TxtVendorId.Text = "";
        txtProductNAme.Text = "";
        txtVendorName.Text = "";
        TxtCGST.Text = "";
        txtSGST.Text = "";
        txtCGDTPROCODE.Text = "";
        txtSGstPrcd.Text = "";
         TxtRate.Text="";
    }
    public void Clear1()
    {
       
        txtVendorName.Text = "";
        TxtVendorId.Text = "";
        txtProductNAme.Text = "";
        txtVendorName.Text = "";
        TxtCGST.Text = "";
        txtSGST.Text = "";
        txtCGDTPROCODE.Text = "";
        txtSGstPrcd.Text = "";
        TxtRate.Text = "";
    }
    public void SHOWUNAUTHORIZE()
    {


        DataTable dt = new DataTable();
        
        CS.GridViewProduct("SHOWUNAUTHORIZE", Session["MID"].ToString(), GrdProduct);
        dt = CS.VieWProduct(FLAG: "SHOWUNAUTHORIZE", PRODID: TxtProductId.Text);


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
    //protected void GrdProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    //{

    //}
    protected void GrdProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           // GrdProduct.PageIndex = e.NewPageIndex;
            SHOWUNAUTHORIZE();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
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
    protected void LnkAutorise_Click(object sender, EventArgs e)
    {
        LinkButton lnkedit = (LinkButton)sender;
        string ProductID = lnkedit.CommandArgument.ToString();
      
        ViewState["PRODID"] = ProductID;
        ShowProduct();
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        LinkButton lnkedit = (LinkButton)sender;
        string str = lnkedit.CommandArgument.ToString();
        string[] ARR = str.Split(',');
       
        ViewState["PRODID"] = ARR[0].ToString();
        ViewState["VENDORID"] = ARR[1].ToString();
        BtnSubmit.Visible = true;
        ShowProduct();
    }

    public void ShowProduct()
    {
        TxtProductId.Text = ViewState["PRODID"].ToString();
        TxtVendorId.Text = ViewState["VENDORID"].ToString();
        DataTable dt = new DataTable();
        string str = "0";
        dt = CS.VieWProduct(FLAG: "SHOW", PRODID: TxtProductId.Text,VENDORID:TxtVendorId.Text);
        if (dt.Columns.Count != 0)
        {

            TxtVendorId.Text = dt.Rows[0]["VENDORID"].ToString();
              txtVendorName.Text = dt.Rows[0]["VENDERNAME"].ToString();  
             txtProductNAme.Text = dt.Rows[0]["PRODNAME"].ToString();
              TxtRate.Text = dt.Rows[0]["RATE"].ToString();
              TxtCGST.Text = dt.Rows[0]["CGST"].ToString();
              txtSGST.Text = dt.Rows[0]["SGST"].ToString();
              txtSGstPrcd.Text = dt.Rows[0]["SGSTPRD"].ToString();
              txtCGDTPROCODE.Text = dt.Rows[0]["CGSTPRD"].ToString();
         
        }


    }
    protected void BtnCancle_Click(object sender, EventArgs e)
    {
        try
        {
            sResult1 = CS.GetProductCancle(FLAG: "MD ROLLBACK", PRODID: TxtProductId.Text, VENDORID:TxtVendorId.Text);


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
    protected void TxtProductId_TextChanged(object sender, EventArgs e)
    {


       
        ShowProduct();
    }
    protected void TxtVendorId_TextChanged(object sender, EventArgs e)
    {
        try
        {

            txtVendorName.Text = CS.GetVendorID(TxtVendorId.Text, Session["MID"].ToString());


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

  
    //protected void btnupload_Click(object sender, EventArgs e)
    //{
    //    //Upload and save the file
    //    string excelPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
    //    FileUpload1.SaveAs(excelPath);

    //    string conString = string.Empty;
    //    string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
    //    switch (extension)
    //    {
    //        case ".xls": //Excel 97-03
    //            conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
    //            break;
    //        case ".xlsx": //Excel 07 or higher
    //            conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
    //            break;

    //    }
    //    conString = string.Format(conString, excelPath);
    //    using (OleDbConnection excel_con = new OleDbConnection(conString))
    //    {
    //        excel_con.Open();
    //        string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
    //        DataTable dtExcelData = new DataTable();

    //        //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
    //        dtExcelData.Columns.AddRange(new DataColumn[3] { new DataColumn("Id", typeof(int)),
    //            new DataColumn("Name", typeof(string)),
    //            new DataColumn("Salary", typeof(decimal)) });

    //        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
    //        {
    //            oda.Fill(dtExcelData);
    //        }
    //        excel_con.Close();

    //        string consString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
    //        using (SqlConnection con = new SqlConnection(consString))
    //        {
    //            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
    //            {
    //                //Set the database table name
    //                sqlBulkCopy.DestinationTableName = "dbo.tblPersons";

    //                //[OPTIONAL]: Map the Excel columns with that of the database table
    //                sqlBulkCopy.ColumnMappings.Add("Id", "PersonId");
    //                sqlBulkCopy.ColumnMappings.Add("Name", "Name");
    //                sqlBulkCopy.ColumnMappings.Add("Salary", "Salary");
    //                con.Open();
    //                sqlBulkCopy.WriteToServer(dtExcelData);
    //                con.Close();
    //            }
    //        }
    //    }



        // CHECK IF A FILE HAS BEEN SELECTED.
        ////if ((FileUpload1.HasFile))
        ////{

        ////    if (!Convert.IsDBNull(FileUpload1.PostedFile) &
        ////        FileUpload1.PostedFile.ContentLength > 0)
        ////    {

        ////        //FIRST, SAVE THE SELECTED FILE IN THE ROOT DIRECTORY.
        ////        FileUpload1.SaveAs(Server.MapPath(".") + "\\" + FileUpload1.FileName);

        ////        SqlBulkCopy oSqlBulk = null;

        ////        // SET A CONNECTION WITH THE EXCEL FILE.
        ////        OleDbConnection myExcelConn = new OleDbConnection
        ////            ("Provider=Microsoft.ACE.OLEDB.12.0; " +
        ////                "Data Source=" + Server.MapPath(".") + "\\" + FileUpload1.FileName +
        ////                ";Extended Properties=Excel 12.0;");
        ////        try
        ////        {
        ////            myExcelConn.Open();

        ////            // GET DATA FROM EXCEL SHEET.
        ////            OleDbCommand objOleDB =
        ////                new OleDbCommand("SELECT *FROM [Sheet1$]", myExcelConn);

        ////            // READ THE DATA EXTRACTED FROM THE EXCEL FILE.
        ////            OleDbDataReader objBulkReader = null;
        ////            objBulkReader = objOleDB.ExecuteReader();

                  
                   
        ////          //  using (SqlConnection con = new SqlConnection(conn))
        ////            {
                        

        ////                // FINALLY, LOAD DATA INTO THE DATABASE TABLE.
        ////             //   oSqlBulk = new SqlBulkCopy(conn);
        ////                oSqlBulk.DestinationTableName = "EmployeeDetails"; // TABLE NAME.
        ////                oSqlBulk.WriteToServer(objBulkReader);
        ////            }

        ////            //lblConfirm.Text = "DATA IMPORTED SUCCESSFULLY.";
        ////            //lblConfirm.Attributes.Add("style", "color:green");
        ////            ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('DATA IMPORTED SUCCESSFULLY.');", true);

        ////        }
        ////        catch (Exception ex)
        ////        {

        ////            ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Invalid Data.');", true);

        ////        }
        ////        finally
        ////        {
        ////            // CLEAR.
        ////            oSqlBulk.Close();
        ////            oSqlBulk = null;
        ////            myExcelConn.Close();
        ////            myExcelConn = null;
        ////        }
        ////    }
        ////}
  //  }
   
   
    //private void Import_To_Grid(string FilePath, string Extension, string isHDR)
    //{
    //    // To import the data from various excel sheets in respective tables 
    //    try
    //    {
    //        string conStr = "";

            

    //            //case ".xls": //Excel 97-03

    //            //    conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";

    //            //    break;

    //         //   case ".xlsx": //Excel 07

    //                //conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
    //                conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

    //              //  break;

            
    //        conStr = String.Format(conStr, FilePath, isHDR);
    //        OleDbConnection connExcel = new OleDbConnection(conStr);
    //        OleDbCommand cmdExcel = new OleDbCommand();
    //        OleDbDataAdapter oda = new OleDbDataAdapter();

    //        cmdExcel.Connection = connExcel;
    //        connExcel.Open();
    //        DataTable dtExcelSchema;
    //        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

    //        String[] excelSheetNames = new String[dtExcelSchema.Rows.Count];
    //        excelSheetNames[0] = "sheet1$";
         
    //        int i = 0;
    //        for (i = 0; i <= 1; i++)
    //        {
    //            DataTable dt = new DataTable();
    //            //int i = 1;
    //            string SheetName = excelSheetNames[i].ToString().Trim();
    //            connExcel.Close();

    //            //Read Data from a Sheet
    //            connExcel.Open();
    //            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
    //            oda.SelectCommand = cmdExcel;
    //            oda.Fill(dt);


    //            connExcel.Close();
    //            if (dt.Columns.Count >= 1)
    //            {
    //                String Sql = "";
    //                DataSet objDS = new DataSet();
    //                objDS.Tables.Add(dt);
    //                objDS.AcceptChanges();
    //                //XmlDocument doc = new XmlDocument();
    //                //doc.PreserveWhitespace = false;
    //                //doc=(objDS.GetXml());
    //                String StrXml = objDS.GetXml();
    //                StrXml = StrXml.Replace("_x0020_", "");
    //                StrXml = StrXml.Replace("<Table1 />", "");
    //                //String StrXml = objDS.GetXml();
    //                StrXml = StrXml.Replace("'", "''").Replace("T00:00:00+05:30", "");
    //                switch (i)
    //                {
    //                    case 0:
    //                        try
    //                        {
    //                            string Query = "exec  USP_ExcelProduct @Items='" + StrXml + "',@MID='" + Convert.ToInt32(Session["UserId"]) + "',@intClId='" + Convert.ToInt32(Session["brcd"]) + "'";
    //                            int Result = conn.sExecuteQuery(Query);
    //                        }
    //                        catch (Exception ex)
    //                        {
    //                            continue;
    //                        }
                           
    //                        break;
    //                    default:
    //                        break;
    //                }
    //                string Result1 = conn.sExecuteReader(Sql);
    //                objDS.Clear();
    //                objDS.Tables.Remove(dt);
    //                string j = SheetName + Result1;
    //            }
    //            else
    //            {
    //                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "clientScript", "alert('Invalid Data.')", true);
    //                ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Invalid Data.');", true);
    //            }
    //        }
    //        ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Records Uploaded Successfully.');", true);

    //    }
    //    catch
    //    {
    //        ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Invalid Data.');", true);
    //    }
    //}
    protected void btnUpload_Click(object sender, System.EventArgs e)
    {
        if (FileUpload1.HasFile)
        {

            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);

            string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];



            string FilePath = Server.MapPath(FolderPath + FileName);

            FileUpload1.SaveAs(FilePath);

            Import_To_Grid(FilePath, Extension, "Yes");

        }
    }
    private void Import_To_Grid(string FilePath, string Extension,string isHDR)
    {

        string conStr = "";

        switch (Extension)
        {

            case ".xls": //Excel 97-03

                conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]

                         .ConnectionString;

                break;

            case ".xlsx": //Excel 07

                conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]

                          .ConnectionString;

                break;

        }




      //  conStr = String.Format(conStr, FilePath, isHDR);

        //OleDbConnection connExcel = new OleDbConnection(conStr);

        //OleDbCommand cmdExcel = new OleDbCommand();

        //OleDbDataAdapter oda = new OleDbDataAdapter();

        //DataTable dt = new DataTable();

        //cmdExcel.Connection = connExcel;



        ////Get the name of First Sheet

        //connExcel.Open();

        //DataTable dtExcelSchema;

        //dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

        //string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

        //connExcel.Close();



        //Read Data from First Sheet

        //connExcel.Open();

        //cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";

        //oda.SelectCommand = cmdExcel;

        //oda.Fill(dt);

        //connExcel.Close();

         conStr = String.Format(conStr, FilePath, isHDR);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();

            cmdExcel.Connection = connExcel;
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            String[] excelSheetNames = new String[dtExcelSchema.Rows.Count];
            excelSheetNames[0] = "Sheet1$";

          int i = 0;
          for (i = 0; i <= 1; i++)
          {
              DataTable dt = new DataTable();
              //int i = 1;
              string SheetName = excelSheetNames[i].ToString().Trim();
              connExcel.Close();

              //Read Data from a Sheet
              connExcel.Open();
              cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
              oda.SelectCommand = cmdExcel;
              oda.Fill(dt);


              connExcel.Close();
              if (dt.Columns.Count >= 1)
              {
                  //string Query = "exec  USP_ExcelProduct @MID='" + Convert.ToInt32(Session["UserId"]) + "',@intClId='" + Convert.ToInt32(Session["BRCD"]) + "'";
                  //int Result = conn.sExecuteQuery(Query);
                  String Sql = "";
                  DataSet objDS = new DataSet();
                  objDS.Tables.Add(dt);
                  objDS.AcceptChanges();
                  //XmlDocument doc = new XmlDocument();
                  //doc.PreserveWhitespace = false;
                  //doc=(objDS.GetXml());
                  String StrXml = objDS.GetXml();
                  StrXml = StrXml.Replace("_x0020_", "");
                  StrXml = StrXml.Replace("<Table1 />", "");
                  //String StrXml = objDS.GetXml();
                  StrXml = StrXml.Replace("'", "''").Replace("T00:00:00+05:30", "");
                  switch (0)
                  {
                      case 0:
                          try
                          {
                              string Query = "exec  USP_ExcelProduct @Items='" + StrXml + "',@MID='" + Convert.ToInt32(Session["UserId"]) + "',@intClId='" + Convert.ToInt32(Session["brcd"]) + "'";
                              int Result = conn.sExecuteQuery(Query);
                          }
                          catch (Exception ex)
                          {

                          }

                          break;
                      default:
                          break;
                  }
                  string Result1 = conn.sExecuteReader(Sql);
                  objDS.Clear();
                  objDS.Tables.Remove(dt);
                  string j = SheetName + Result1;
              }
          }
      

        //GridView1.Caption = Path.GetFileName(FilePath);

        //GridView1.DataSource = dt;

        //GridView1.DataBind();

    }
    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

        string FileName = GridView1.Caption;

        string Extension = Path.GetExtension(FileName);
       

        string FilePath = Server.MapPath(FolderPath + FileName);



         Import_To_Grid(FilePath, Extension, "Yes");

        GridView1.PageIndex = e.NewPageIndex;

        GridView1.DataBind();

    }
}