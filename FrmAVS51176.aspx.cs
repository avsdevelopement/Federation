using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmAVS51176 : System.Web.UI.Page
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
            txtPONO.ReadOnly = true;
            txtBRCD.ReadOnly = true;
            txtENDTRYDATE.ReadOnly = true;
            autoglname.ContextKey = Session["MID"].ToString();
           // autoprname2.ContextKey = Session["MID"].ToString();
            txtPONO.Text = CS.PurchaseMaster(FLAG: "GETID");
            txtENDTRYDATE.Text = Session["EntryDate"].ToString();
            txtBRCD.Text = Session["BRCD"].ToString();
            EmptyGridBind();
              SHOWGRID();
            

        }
    }
    //protected void GrdPurChase_PageIndexChanged(object sender, EventArgs e)
    //{
    //    SHOWGRID();
    //}
    //protected void GrdPurChase_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    try
    //    {
    //        GrdPurchase.PageIndex = e.NewPageIndex;
    //        SHOWGRID();
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Flag"] = "AD";
            clear1();
            txtPONO.Text = CS.PurchaseMaster(FLAG: "GETID");
            txtPONO.ReadOnly = true;
            txtBRCD.ReadOnly = true;
            SHOWGRID();
            txtENDTRYDATE.ReadOnly = true;
            txtVenderID.Enabled = true;
            txtVendorName.Enabled = true;
            BtnSubmit.Text = "Create";
            txtENDTRYDATE.Text = Session["EntryDate"].ToString();
            txtBRCD.Text = Session["BRCD"].ToString();
            

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
            txtPONO.ReadOnly = true;
          
            SHOWGRID();
         
            txtBRCD.ReadOnly = true;
            txtENDTRYDATE.ReadOnly = true;
          //  txtAmount.ReadOnly = true;
            txtENDTRYDATE.Enabled = false;
            txtVenderID.Enabled = false;
            txtVendorName.Enabled = false;
            BtnSubmit.Text = "Modify";
            BtnSubmit.Visible = true;
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

            txtBRCD.ReadOnly = true;
            txtENDTRYDATE.ReadOnly = true;
         //   txtAmount.ReadOnly = true;
            txtENDTRYDATE.Enabled = false;
            txtVenderID.Enabled = false;
            txtVendorName.Enabled = false;
            // BtnCancle.Visible = true;
            SHOWGRID();
            BtnSubmit.Text = "Authorize";
            BtnSubmit.Visible = false;
           clear();
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

            txtPONO.ReadOnly = true;
            txtBRCD.ReadOnly = true;
            txtENDTRYDATE.ReadOnly = true;
           // txtAmount.ReadOnly = true;
            txtENDTRYDATE.Enabled = false;
            txtVenderID.Enabled = false;
            txtVendorName.Enabled = false;
            BtnSubmit.Text = "Delete";
            BtnSubmit.Visible = false;
            SHOWGRID();
          clear();
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
                txtVenderID.Text = CT[1].ToString();
                string[] GLS = CS.GetName(txtVenderID.Text, Session["MID"].ToString()).Split('_');

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtVenderID_TextChanged(object sender, EventArgs e)
    {
        txtVendorName.Text = CS.GetVendorID(txtVenderID.Text, Session["MID"].ToString());
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {

        if (ViewState["Flag"].ToString() == "AD")
        {
            if (txtVendorName.Text == "")
            {
                WebMsgBox.Show("Enter Vendor Name ..!!", this.Page);
                return;

            }
            if (txtVenderID.Text == "")
            {
                WebMsgBox.Show("Enter Vendor Name ..!!", this.Page);
                return;

            }

            try
            {

                txtPONO.Text = CS.PurchaseMaster(FLAG: "GETID");
                int j = 0;
                int t = 0;
                foreach (GridViewRow gvRow in this.grdInsert.Rows)
                {
                    string SRNO = Convert.ToString(((TextBox)gvRow.FindControl("SRNO")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("SRNO")).Text);
                    string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODID")).Text);
                    string PRODUCTNAME = Convert.ToString(((TextBox)gvRow.FindControl("PRODNAME")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODNAME")).Text);
                    string QUANTITY = Convert.ToString(((TextBox)gvRow.FindControl("QTY")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("QTY")).Text);
                    string UNITCOST = Convert.ToString(((TextBox)gvRow.FindControl("UNITCOST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("UNITCOST")).Text);
                    string GST = Convert.ToString(((TextBox)gvRow.FindControl("GST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("GST")).Text);
                    string GSTAMT = Convert.ToString(((TextBox)gvRow.FindControl("GSTAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("GSTAMT")).Text);
                    string AMOUNT = Convert.ToString(((TextBox)gvRow.FindControl("TOTALAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TOTALAMT")).Text);

                    if (PRODUCTID == "")
                    {
                        WebMsgBox.Show("Enter Product ID..!!", this.Page);
                        return;

                    }

                    if (PRODUCTNAME == "")
                    {
                        WebMsgBox.Show("Enter Product Name ..!!", this.Page);
                        return;
                    }

                    if (QUANTITY == "")
                    {
                        WebMsgBox.Show("Enter Quantity Name ..!!", this.Page);
                        return;

                    }

                    sResult = CS.PurchaseMaster(FLAG: "AD", PONO: txtPONO.Text, BRCD: txtBRCD.Text, VENDORID: txtVenderID.Text, SRNO: SRNO, PRODID: PRODUCTID, QTY: QUANTITY, UNITCOST: UNITCOST, SGST: GST, CGSTPRD: GSTAMT, AMOUNT: AMOUNT, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString());
                    if (sResult == "1")
                    {
                        t += 1;
                    }

                    j = j + 1;
                }
                if (t > 0)
                {
                    CS.PurchaseMaster(FLAG: "UPDATEID");
                    string title = sResult.Replace("\n", " "); ;
                    string body = "Welcome to ASPSnippets.com";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "ShowPopup('" + txtPONO.Text + " Create Succefully!');", true);
                    clear();
                }

                else if (sResult == "ALREADYEXISTS")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Purchaser Already Present!')", true);
                    // Clear();
                }
                else if (sResult.StartsWith("ERROR"))
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
        if (ViewState["Flag"].ToString() == "ATH")
        {

            try
            {
                foreach (GridViewRow gvRow in this.grdInsert.Rows)
                {
                    string SRNO = Convert.ToString(((TextBox)gvRow.FindControl("SRNO")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("SRNO")).Text);
                    string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODID")).Text);
                    string PRODUCTNAME = Convert.ToString(((TextBox)gvRow.FindControl("PRODNAME")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODNAME")).Text);
                    string QUANTITY = Convert.ToString(((TextBox)gvRow.FindControl("QTY")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("QTY")).Text);
                    string UNITCOST = Convert.ToString(((TextBox)gvRow.FindControl("UNITCOST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("UNITCOST")).Text);
                    string GST = Convert.ToString(((TextBox)gvRow.FindControl("GST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("GST")).Text);
                    string GSTAMT = Convert.ToString(((TextBox)gvRow.FindControl("GSTAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("GSTAMT")).Text);
                    string AMOUNT = Convert.ToString(((TextBox)gvRow.FindControl("TOTALAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TOTALAMT")).Text);

                    ViewState["ShowNext"] = CS.PurchaseMaster(FLAG: "ATH", PONO: txtPONO.Text, BRCD: txtBRCD.Text, VENDORID: txtVenderID.Text, SRNO: SRNO, PRODID: PRODUCTID, QTY: QUANTITY, UNITCOST: UNITCOST, SGST: GST, CGSTPRD: GSTAMT, AMOUNT: AMOUNT, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString());
                    sResult = ViewState["ShowNext"].ToString();
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
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Purchaser Not Present!')", true);
                        // Clear();
                    }
                    else if (sResult == ("NOTAUTH"))
                    {

                        // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Please Try Again!!!!!');", true);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You Are Not Authorize Person!')", true);
                    }

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
                foreach (GridViewRow gvRow in this.grdInsert.Rows)
                {
                    string SRNO = Convert.ToString(((TextBox)gvRow.FindControl("SRNO")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("SRNO")).Text);
                    string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODID")).Text);
                    string PRODUCTNAME = Convert.ToString(((TextBox)gvRow.FindControl("PRODNAME")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODNAME")).Text);
                    string QUANTITY = Convert.ToString(((TextBox)gvRow.FindControl("QTY")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("QTY")).Text);
                    string UNITCOST = Convert.ToString(((TextBox)gvRow.FindControl("UNITCOST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("UNITCOST")).Text);
                    string GST = Convert.ToString(((TextBox)gvRow.FindControl("GST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("GST")).Text);
                    string GSTAMT = Convert.ToString(((TextBox)gvRow.FindControl("GSTAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("GSTAMT")).Text);
                    string AMOUNT = Convert.ToString(((TextBox)gvRow.FindControl("TOTALAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TOTALAMT")).Text);

                    sResult = CS.PurchaseMaster(FLAG: "DEL", PONO: txtPONO.Text, BRCD: txtBRCD.Text, VENDORID: txtVenderID.Text, SRNO: SRNO, PRODID: PRODUCTID, QTY: QUANTITY, UNITCOST: UNITCOST, SGST: GST, CGSTPRD: GSTAMT, AMOUNT: AMOUNT, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString());

                    if (sResult.StartsWith("DEL#"))
                    {

                        string[] Array = sResult.Split('#');

                        string title = sResult.Replace("\n", " "); ;
                        string body = "Welcome to ASPSnippets.com";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "ShowPopup('Delete Succefully!');", true);
                        clear();
                    }

                    else if (sResult == "NOTEXISTS")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Purchaser Not Present!')", true);
                        // Clear();
                    }
                    else if (sResult == ("ALREADYAUTH"))
                    {

                        // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Please Try Again!!!!!');", true);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You Are Already Authorize !')", true);
                    }

                }
            }

            catch (Exception EX)
            {
                WebMsgBox.Show(EX.Message.ToString(), this.Page);
                ExceptionLogging.SendErrorToText(EX);

            }
        }

    }
    protected void Btnclear_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnExit_Click(object sender, EventArgs e)
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
    protected void btnAddNewRow_Click(object sender, EventArgs e)
    {


        try
        {
           
            double Var = 0;
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[7] { new DataColumn("PRODID", typeof(int)),
                            new DataColumn("PRODNAME", typeof(string)),new DataColumn("QTY", typeof(int)),
                            new DataColumn("UNITCOST", typeof(string)),new DataColumn("GST", typeof(string)),
                            new DataColumn("GSTAMT", typeof(string)),new DataColumn("TOTALAMT", typeof(string)) });
            DataRow dr;

            foreach (GridViewRow gvRow in this.grdInsert.Rows)
            {
                dr = dt.NewRow();

                string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODID")).Text);
                dr[0] = PRODUCTID;
                string PRODUCTNAME = Convert.ToString(((TextBox)gvRow.FindControl("PRODNAME")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODNAME")).Text);
                dr[1] = PRODUCTNAME;

                string QUANTITY = Convert.ToDouble(((TextBox)gvRow.FindControl("QTY")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("QTY")).Text).ToString();
                dr[2] = QUANTITY;

                string RATE = Convert.ToDouble(((TextBox)gvRow.FindControl("UNITCOST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("UNITCOST")).Text).ToString();
                dr[3] = RATE;

                string GST = Convert.ToDouble(((TextBox)gvRow.FindControl("GST")).Text == "" ? "" : ((TextBox)gvRow.FindControl("GST")).Text).ToString();
                dr[4] = GST;

                string GSTAMT = Convert.ToDouble(((TextBox)gvRow.FindControl("GSTAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("GSTAMT")).Text).ToString();
                dr[5] = GSTAMT;

                string TOTALAMT = Convert.ToDouble(((TextBox)gvRow.FindControl("TOTALAMT")).Text == "" ? "" : ((TextBox)gvRow.FindControl("TOTALAMT")).Text).ToString();
                dr[6] = TOTALAMT;

               
               // Var += Convert.ToDouble(TOTALAMT);


                dt.Rows.Add(dr);
            }
           // txtAmount.Text = Var.ToString();
            DataTable MainTable = new DataTable();

            for (int i = dt.Rows.Count; i < grdInsert.Rows.Count + 1; i++)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
            }

            // MainTable = (DataTable)ViewState["PURCHASE"];
            dt.AcceptChanges();
            grdInsert.DataSource = dt;
            grdInsert.DataBind();


        }
        catch (Exception EX)
        {
            WebMsgBox.Show(EX.Message.ToString(), this.Page);
            ExceptionLogging.SendErrorToText(EX);

        }


    }

 

    protected void TxtPRODUCTID_TextChanged(object sender, EventArgs e)
    {

        try
        {

            // int count = grdInsert.Rows.Count;
            
            string currentRowIndex = hdnRowIndex.Value;

            if (!String.IsNullOrEmpty(currentRowIndex) && Convert.ToInt32(currentRowIndex) > 0)
            {
                ShowPurchase(currentRowIndex);
                GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(currentRowIndex)];
            
                string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODID")).Text);
                string PRODUCTNAME = Convert.ToString(((TextBox)gvRow.FindControl("PRODNAME")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODNAME")).Text);

                PRODUCTNAME= CS.GetProductID(PRODUCTID, Session["MID"].ToString());

                ((TextBox)gvRow.FindControl("PRODUCTID")).Focus();

            }


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtPRODUCTNAME_TextChanged(object sender, EventArgs e)
    {

        try
        {


            int rowCount = grdInsert.Rows.Count;

            string currentRowIndex = hdnRowIndex.Value;

            string crosscheckIndex = hdnCrossCheck.Value;
            if (crosscheckIndex != "0")
            {


                if (!String.IsNullOrEmpty(currentRowIndex) && Convert.ToInt32(currentRowIndex) > 0)
                {
                    GetProductName(ref currentRowIndex);

                    if (rowCount == Convert.ToInt32(currentRowIndex))
                    {



                    }
                    else
                    {
                        GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(currentRowIndex)];
                        ((TextBox)gvRow.FindControl("PRODID")).Focus();

                    }
                }
            }
            else
            {
                WebMsgBox.Show("Same Product ID  Entered", this.Page);
                GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(currentRowIndex) - 1];
                ((TextBox)gvRow.FindControl("PRODID")).Focus();
                ((TextBox)gvRow.FindControl("PRODID")).Text = "";

                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

        //try
        //{

        //    string currentRowIndex = hdnRowIndex.Value;
        //    GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(currentRowIndex) - 1];

        //   // string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODUCTID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODUCTID")).Text);
        //    string PRODUCTNAME = Convert.ToString(((TextBox)gvRow.FindControl("PRODNAME")).Text == "" ? "" : ((TextBox)gvRow.FindControl("PRODNAME")).Text);
        //    string[] CT = PRODUCTNAME.Split('_');
        // //  string[] CT = CS.GetProductName1( PRODUCTID, txtVenderID.Text).Split('_');


        //    if (CT.Length > 0)
        //    {


        //        ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(CT[0].ToString()) ? "" : CT[0].ToString());
        //        ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(CT[1].ToString()) ? "" : CT[1].ToString());

        //        sResult = CS.DeadStock("PRODID", CT[1].ToString(), txtVenderID.Text);
        //        if (sResult == "NOTEXISTS")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Present!')", true);

        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Authorize!')", true);
        //            ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(CT[0].ToString()) ? "" : "");
        //            ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(CT[1].ToString()) ? "" : "");
        //            ((TextBox)gvRow.FindControl("PRODID")).Focus();
        //            ((TextBox)gvRow.FindControl("PRODNAME")).Focus();
        //            return;
        //        }
        //        else
        //            if (sResult == "NOTAUTH")
        //            {
        //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Authorize!')", true);
        //                ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(CT[0].ToString()) ? "":"");
        //                    ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(CT[1].ToString()) ? "":"") ;
        //                ((TextBox)gvRow.FindControl("PRODID")).Focus();
        //                ((TextBox)gvRow.FindControl("PRODNAME")).Focus();
        //                return;
        //            }
        //        else
        //            if (sResult == "PRODIDNOTBELONGSTOVENDORID")
        //            {
        //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Available In VendorMaster!')", true);
        //                ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(CT[0].ToString()) ? "":"");
        //                    ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(CT[1].ToString()) ? "":"") ;
        //                ((TextBox)gvRow.FindControl("PRODID")).Focus();
        //                ((TextBox)gvRow.FindControl("PRODNAME")).Focus();
        //                return;
        //            }

        //        string[] name = PRODUCTNAME.Split('_');
        //        ViewState["PURCHASE"] = PRODUCTNAME[1].ToString();

        //        string[] custnob = ViewState["PURCHASE"].ToString().Split('_');
        //        if (custnob.Length > 1)
        //        {



        //            ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(custnob[0].ToString()) ? "" : custnob[0].ToString());
        //            ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

        //        }

        //    }
        //    else
        //    {
        //        WebMsgBox.Show("Product Not Avialable Under Vendermaster..!!", this.Page);
        //        PRODUCTNAME = "";
        //        return ;
        //    }
        //}
        //catch (Exception Ex)
        //{
        //    ExceptionLogging.SendErrorToText(Ex);
        //}
    }

   
    public void ShowPurchase(string Index)
    {
        try
        {
            GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(Index) - 1];
            string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODUCTID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODUCTID")).Text);
            string PRODUCTNAME = Convert.ToString(((TextBox)gvRow.FindControl("PRODUCTNAME")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODUCTNAME")).Text);
            DT = CS.VieWPurchaseId("CKSTOCKEXISTS", PRODUCTNAME);
            ViewState["SHOWGRID"] = DT;
            if (PRODUCTNAME != null)
            {
                string[] name = PRODUCTNAME.Split('_');
                PRODUCTNAME = (string.IsNullOrEmpty(name[0].ToString()) ? "" : name[0].ToString());
                PRODUCTID = (string.IsNullOrEmpty(name[1].ToString()) ? "0" : name[1].ToString());
                ((TextBox)gvRow.FindControl("PRODUCTNAME")).Text = PRODUCTNAME.ToString();
                ((TextBox)gvRow.FindControl("PRODUCTID")).Text = PRODUCTID.ToString();
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void SHOWGRID()
    {


        DataTable dt = new DataTable();
        string str = "0";
        CS.BindForPurchase("SHOWUNAUTHORIZE", Session["BRCD"].ToString(), GrdPurchase);
        dt = CS.VieWPURCHASE(FLAG: "SHOWUNAUTHORIZE", PONO: txtPONO.Text);


    }


    public void EmptyGridBind()
    {


        DataTable DT = new DataTable();
        DT.Columns.AddRange(new DataColumn[7] { new DataColumn("PRODID", typeof(int)),
                            new DataColumn("PRODNAME", typeof(string)),new DataColumn("QTY", typeof(int)),
                            new DataColumn("UNITCOST", typeof(string)),new DataColumn("GST", typeof(int)),
                            new DataColumn("GSTAMT", typeof(string)),new DataColumn("TOTALAMT", typeof(int))   });

        DataRow dr = DT.NewRow();
        DT.Rows.Add(dr);


        grdInsert.DataSource = DT;
        grdInsert.DataBind();



    }

    protected void QUANTITY_TextChanged(object sender, EventArgs e)
    {
        try
        {

            // int count = grdInsert.Rows.Count;

            string currentRowIndex = hdnRowIndex.Value;


            if (!String.IsNullOrEmpty(currentRowIndex) && Convert.ToInt32(currentRowIndex) > 0)
            {
                CalculatePurchase(currentRowIndex);
                GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(currentRowIndex)];
                ((TextBox)gvRow.FindControl("PRODID")).Focus();
            }


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }


     
    }

    public void CalculatePurchase(string Index)
    {
        try
        {
            GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(Index) - 1];
            string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODID")).Text);
            string QUANTITY = Convert.ToString(((TextBox)gvRow.FindControl("QTY")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("QTY")).Text);
           // string Vender = Convert.ToString(((TextBox)gvRow.FindControl("QTY")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("QTY")).Text);
            DT = CS.ProductCalculate("GETPRODUCTCALC", PRODUCTID,txtVenderID.Text);
            if (QUANTITY != null)
            {
                ((TextBox)gvRow.FindControl("UNITCOST")).Text = DT.Rows[0]["RATE"].ToString();
                ((TextBox)gvRow.FindControl("GST")).Text = DT.Rows[0]["GST"].ToString();
                ((TextBox)gvRow.FindControl("GSTAMT")).Text = ((Convert.ToDouble(DT.Rows[0]["GSTAMT"])) * (Convert.ToDouble(QUANTITY))).ToString();
               ((TextBox)gvRow.FindControl("TOTALAMT")).Text = ((Convert.ToDouble(DT.Rows[0]["TOTALAMT"])) * (Convert.ToDouble(QUANTITY))).ToString();

             
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
        string PONO = lnkedit.CommandArgument.ToString();
        ViewState["PONO"] = PONO;
        
       SHOWPurchaseData();
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        LinkButton lnkedit = (LinkButton)sender;
        string PONO = lnkedit.CommandArgument.ToString();
        ViewState["PONO"] = PONO;

        BtnSubmit.Visible = true;
        SHOWPurchaseData();
    }

    public void SHOWPurchaseDataByID()
    {


        DataTable dt = new DataTable();
        string str = "0";
        dt = CS.VieWPURCHASE(FLAG: "SHOW", PONO: txtPONO.Text);
        if (dt.Columns.Count != 0)
        {
            txtENDTRYDATE.Text = dt.Rows[0]["ENTRYDATE"].ToString();
            txtBRCD.Text = dt.Rows[0]["BRCD"].ToString();
            txtVenderID.Text = dt.Rows[0]["VENDORID"].ToString();
            txtVendorName.Text = dt.Rows[0]["VENDERNAME"].ToString();

            foreach (GridViewRow gvRow in this.grdInsert.Rows)
            {
            
                //string SRNO = Convert.ToString(((TextBox)gvRow.FindControl("SRNO")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("SRNO")).Text);
                //string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODUCTID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODUCTID")).Text);
                //string PRODUCTNAME = Convert.ToString(((TextBox)gvRow.FindControl("PRODUCTNAME")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODUCTNAME")).Text);
                //string QUANTITY = Convert.ToString(((TextBox)gvRow.FindControl("QUANTITY")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("QUANTITY")).Text);
                //string UNITCOST = Convert.ToString(((TextBox)gvRow.FindControl("UNITCOST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("UNITCOST")).Text);
                //string GST = Convert.ToString(((TextBox)gvRow.FindControl("GST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("GST")).Text);
                //string GSTAMT = Convert.ToString(((TextBox)gvRow.FindControl("GSTAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("GSTAMT")).Text);
                //string AMOUNT = Convert.ToString(((TextBox)gvRow.FindControl("TOTALAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("TOTALAMT")).Text);
             

                ((TextBox)gvRow.FindControl("SRNO")).Text = dt.Rows[0]["SRNO"].ToString();
                ((TextBox)gvRow.FindControl("PRODUCTID")).Text = dt.Rows[0]["PRODID"].ToString();
                ((TextBox)gvRow.FindControl("PRODUCTNAME")).Text = dt.Rows[0]["PRODNAME"].ToString();
                ((TextBox)gvRow.FindControl("UNITCOST")).Text = dt.Rows[0]["UNITCOST"].ToString();
                ((TextBox)gvRow.FindControl("QUANTITY")).Text = dt.Rows[0]["QTY"].ToString();
                ((TextBox)gvRow.FindControl("GST")).Text = dt.Rows[0]["GST"].ToString();
                ((TextBox)gvRow.FindControl("GSTAMT")).Text = dt.Rows[0]["GSTAMT"].ToString();
                ((TextBox)gvRow.FindControl("TOTALAMT")).Text = dt.Rows[0]["TOTALAMT"].ToString();
               
            }

        }

    }


    public void SHOWPurchaseData()
    {
       txtPONO.Text = ViewState["PONO"].ToString();

       DataTable dt = new DataTable();
       string str = "0";
       
    


        
        DataRow dr;
        int j = 0;
        dt = CS.VieWPURCHASE(FLAG: "SHOW", PONO: txtPONO.Text);

        grdInsert.DataSource = dt;
        grdInsert.DataBind();

     
     
       
        txtENDTRYDATE.Text = dt.Rows[0]["ENTRYDATE"].ToString();
        txtBRCD.Text = dt.Rows[0]["BRCD"].ToString();
        txtVenderID.Text = dt.Rows[0]["VENDORID"].ToString();
        txtVendorName.Text = dt.Rows[0]["VENDERNAME"].ToString();

        
    }


    //protected void GrdPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{

    //}
    //protected void GrdPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    //{

    //}
    //protected void GrdPurchase_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    protected void btnCancle_Click(object sender, EventArgs e)
    {

    }
    protected void txtPONO_TextChanged(object sender, EventArgs e)
    {
        SHOWPurchaseDataByID();
    }
    public void grdInsert_RowDataBound(object sender, GridViewRowEventArgs e)
    {





    }



    public void show2()
    {
       

    //    txtPONO.Text = ViewState["PONO"].ToString();
     
    //    DataRow dr;
    //    DataTable dt = GetGridData();
    //    dr = dt.NewRow();
    //    dt.Rows.Add(.Text, TextBox2.Text);
    //    GridView1.DataSource = dt;
    //    GridView1.DataBind();
    //}

    //private DataTable GetGridData()
    //{
    //    DataTable _datatable = new DataTable();
    //    for (int i = 0; i < grdReport.Columns.Count; i++)
    //    {
    //        _datatable.Columns.Add(grdReport.Columns[i].ToString());
    //    }
    //    foreach (GridViewRow row in grdReport.Rows)
    //    {
    //        DataRow dr = _datatable.NewRow();
    //        for (int j = 0; j < grdReport.Columns.Count; j++)
    //        {
    //            if (!row.Cells[j].Text.Equals("&nbsp;"))
    //                dr[grdReport.Columns[j].ToString()] = row.Cells[j].Text;
    //        }

    //        _datatable.Rows.Add(dr);
    //    }
    //    return _dataTable;

      
    }


    public void clear1()
    {
       
        txtENDTRYDATE.Text = "";
        txtBRCD.Text = "";
        txtVenderID.Text = "";
        txtVendorName.Text = "";

    }

    
    public void clear()
    {
        txtPONO.Text = "";
        txtENDTRYDATE.Text = "";
        txtBRCD.Text = "";
        txtVenderID.Text = "";
        txtVendorName.Text = "";

    }
    protected void PRODID_TextChanged(object sender, EventArgs e)
    {
        try
        {


            int rowCount = grdInsert.Rows.Count;

            string currentRowIndex = hdnRowIndex.Value;

            string crosscheckIndex = hdnCrossCheck.Value;
            if (crosscheckIndex != "0")
            {


                if (!String.IsNullOrEmpty(currentRowIndex) && Convert.ToInt32(currentRowIndex) > 0)
                {
                    GetMemberData(ref currentRowIndex);
                      
                    if (rowCount == Convert.ToInt32(currentRowIndex))
                    {
                        
                        
                        
                    }
                    else
                    {
                      GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(currentRowIndex)];
                        ((TextBox)gvRow.FindControl("PRODID")).Focus();

                    }
                }
            }
            else
            {
                WebMsgBox.Show("Same Product ID  Entered", this.Page);
                GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(currentRowIndex) - 1];
                ((TextBox)gvRow.FindControl("PRODID")).Focus();
                ((TextBox)gvRow.FindControl("PRODID")).Text = "";

                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    private void GetProductName(ref string Index)
    {
        try
        {

            string currentRowIndex = hdnRowIndex.Value;
            GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(currentRowIndex) - 1];

            // string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODUCTID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODUCTID")).Text);
            string PRODUCTNAME = Convert.ToString(((TextBox)gvRow.FindControl("PRODNAME")).Text == "" ? "" : ((TextBox)gvRow.FindControl("PRODNAME")).Text);
            string[] CT = PRODUCTNAME.Split('_');
            //  string[] CT = CS.GetProductName1( PRODUCTID, txtVenderID.Text).Split('_');


            if (CT.Length > 0)
            {


                ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(CT[0].ToString()) ? "" : CT[0].ToString());
                ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(CT[1].ToString()) ? "" : CT[1].ToString());

                sResult = CS.DeadStock("PRODID", CT[1].ToString(), txtVenderID.Text);
                if (sResult == "NOTEXISTS")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Present!')", true);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Authorize!')", true);
                    ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(CT[0].ToString()) ? "" : "");
                    ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(CT[1].ToString()) ? "" : "");
                    ((TextBox)gvRow.FindControl("PRODID")).Focus();
                    ((TextBox)gvRow.FindControl("PRODNAME")).Focus();
                    return;
                }
                else
                    if (sResult == "NOTAUTH")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Authorize!')", true);
                        ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(CT[0].ToString()) ? "" : "");
                        ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(CT[1].ToString()) ? "" : "");
                        ((TextBox)gvRow.FindControl("PRODID")).Focus();
                        ((TextBox)gvRow.FindControl("PRODNAME")).Focus();
                        return;
                    }
                    else
                        if (sResult == "PRODIDNOTBELONGSTOVENDORID")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Available In VendorMaster!')", true);
                            ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(CT[0].ToString()) ? "" : "");
                            ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(CT[1].ToString()) ? "" : "");
                            ((TextBox)gvRow.FindControl("PRODID")).Focus();
                            ((TextBox)gvRow.FindControl("PRODNAME")).Focus();
                            return;
                        }

                string[] name = PRODUCTNAME.Split('_');
                ViewState["PURCHASE"] = PRODUCTNAME[1].ToString();

                string[] custnob = ViewState["PURCHASE"].ToString().Split('_');
                if (custnob.Length > 1)
                {



                    ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(custnob[0].ToString()) ? "" : custnob[0].ToString());
                    ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                }

            }
            else
            {
                WebMsgBox.Show("Product Not Avialable Under Vendermaster..!!", this.Page);
                PRODUCTNAME = "";
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    private void GetMemberData(ref string Index)
    {

        //try
        //{

        //    string currentRowIndex = hdnRowIndex.Value;
        //    GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(currentRowIndex) - 1];

        //    string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODID")).Text);
        //    string[] CT = PRODUCTID.Split('_');
        //    //  string[] CT = CS.GetProductName1( PRODUCTID, txtVenderID.Text).Split('_');


        //    if (CT.Length > 0)
        //    {


        //      ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(CT[0].ToString()) ? "" : CT[0].ToString());
        //        ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(CT[0].ToString()) ? "" : CT[1].ToString());

        //        sResult = CS.DeadStock("PRODID", CT[1].ToString(), txtVenderID.Text);
        //        if (sResult == "NOTEXISTS")
        //        {
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Present!')", true);

        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Authorize!')", true);
        //            ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(CT[0].ToString()) ? "" : "");
        //            ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(CT[1].ToString()) ? "" : "");
        //            ((TextBox)gvRow.FindControl("PRODNAME")).Focus();
        //            ((TextBox)gvRow.FindControl("PRODID")).Focus();
        //            return;
        //        }
        //        else
        //            if (sResult == "NOTAUTH")
        //            {
        //                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Authorize!')", true);
        //                ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(CT[0].ToString()) ? "" : "");
        //                ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(CT[1].ToString()) ? "" : "");
        //                ((TextBox)gvRow.FindControl("PRODID")).Focus();
        //                ((TextBox)gvRow.FindControl("PRODNAME")).Focus();
        //                return;
        //            }
        //            else
        //                if (sResult == "PRODIDNOTBELONGSTOVENDORID")
        //                {
        //                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Available In VendorMaster!')", true);
        //                    ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(CT[0].ToString()) ? "" : "");
        //                    ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(CT[1].ToString()) ? "" : "");
        //                    ((TextBox)gvRow.FindControl("PRODID")).Focus();
        //                    ((TextBox)gvRow.FindControl("PRODNAME")).Focus();
        //                    return;
        //                }

        //        string[] name = PRODUCTID.Split('_');
        //        ViewState["PURCHASE"] = name[1].ToString();

        //        string[] custnob = ViewState["PURCHASE"].ToString().Split('_');
        //        if (custnob.Length > 1)
        //        {



        //            ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(custnob[0].ToString()) ? "" : custnob[0].ToString());
        //            ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

        //        }

        //    }
        //    else
        //    {
        //        WebMsgBox.Show("Product Not Avialable Under Vendermaster..!!", this.Page);
        //        PRODUCTID = "";
        //        return;
        //    }
        //}
        //catch (Exception Ex)
        //{
        //    ExceptionLogging.SendErrorToText(Ex);
        //}


        try
        {

            string currentRowIndex = hdnRowIndex.Value;
            GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(currentRowIndex) - 1];

            string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODID")).Text);
            string PRODNAME = Convert.ToString(((TextBox)gvRow.FindControl("PRODNAME")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODNAME")).Text);

            PRODNAME = CS.GetProductID(PRODUCTID, Session["MID"].ToString());

                ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(PRODNAME) ? "" : PRODNAME.ToString());
                ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(PRODUCTID) ? "" : PRODUCTID.ToString());

                sResult = CS.DeadStock("PRODID",PRODUCTID.ToString(), txtVenderID.Text);
                if (sResult == "NOTEXISTS")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Present!')", true);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Authorize!')", true);
                    ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(PRODNAME.ToString()) ? "" : "");
                    ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(PRODUCTID.ToString()) ? "" : "");
                    ((TextBox)gvRow.FindControl("PRODNAME")).Focus();
                    ((TextBox)gvRow.FindControl("PRODID")).Focus();
                    return;
                }
                else
                    if (sResult == "NOTAUTH")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Authorize!')", true);
                        ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(PRODNAME.ToString()) ? "" : "");
                        ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(PRODUCTID.ToString()) ? "" : "");
                        ((TextBox)gvRow.FindControl("PRODID")).Focus();
                        ((TextBox)gvRow.FindControl("PRODNAME")).Focus();
                        return;
                    }
                    else
                        if (sResult == "PRODIDNOTBELONGSTOVENDORID")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Available In VendorMaster!')", true);
                            ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(PRODNAME.ToString()) ? "" : "");
                            ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(PRODUCTID.ToString()) ? "" : "");
                            ((TextBox)gvRow.FindControl("PRODID")).Focus();
                            ((TextBox)gvRow.FindControl("PRODNAME")).Focus();
                            return;
                        }

              //  string[] name = PRODUCTID.Split('_');
               // ViewState["PURCHASE"] = name[1].ToString();

               // string[] custnob = ViewState["PURCHASE"].ToString().Split('_');
               // if (custnob.Length > 1)
                {



                    ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(PRODNAME.ToString()) ? "" : PRODNAME.ToString());
                    ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(PRODUCTID.ToString()) ? "" : PRODUCTID.ToString());

                }

            }
           
            
               
            
        
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }


    }
    protected void GrdPurchase_PageIndexChanged(object sender, EventArgs e)
    {
        SHOWGRID();
    }
    protected void GrdPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdPurchase.PageIndex = e.NewPageIndex;
            SHOWGRID();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}