using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmAVS51179 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAVS51173 CS = new ClsAVS51173();
    clsAVS51178 Cbr = new clsAVS51178();
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
            txtFBrcd.Text = Session["BRCD"].ToString();
            txtEntryDate.Text = Session["EntryDate"].ToString();
            EmptyGridBind();
         
            autoglname.ContextKey = Session["BRCD"].ToString();
            txtBrcdName.Text = Cbr.getbrcode(txtFBrcd.Text);
            txtUseId.Text = Cbr.USEDMASTER(FLAG: "GETID");
            BtnSubmit.Text = "Create";
            SHOWGRID();
            if (ViewState["Flag"].ToString() == "ATH")
            {
                txtBrcdName.Text = Cbr.getbrcode(txtFBrcd.Text);
                txtBrcdName.Text = Cbr.getbrcode(txtFBrcd.Text);
            }
            if (ViewState["Flag"].ToString() == "MD")
            {
                txtBrcdName.Text = Cbr.getbrcode(txtFBrcd.Text);
                txtBrcdName.Text = Cbr.getbrcode(txtFBrcd.Text);
            }
            if (ViewState["Flag"].ToString() == "DEl")
            {
                txtBrcdName.Text = Cbr.getbrcode(txtFBrcd.Text);
                txtBrcdName.Text = Cbr.getbrcode(txtFBrcd.Text);
            }
        }
    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Flag"] = "AD";
            txtUseId.Text = Cbr.USEDMASTER(FLAG: "GETID");
            BtnSubmit.Text = "Create";
            txtFBrcd.Text = Session["BRCD"].ToString();
            txtEntryDate.Text = Session["EntryDate"].ToString();
            EmptyGridBind();
            BtnSubmit.Visible = true;
            txtBrcdName.Text = Cbr.getbrcode(txtFBrcd.Text);
            SHOWGRID();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    //protected void lnkModify_Click(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        ViewState["Flag"] = "MD";
    //        SHOWGRID();
    //        txtEntryDate.Enabled = true;
    //        txtFBrcd.Enabled = true;
    //        txtBrcdName.Enabled = true;
    //        BtnSubmit.Visible = false;
    //       // BtnSubmit.Text = "Modify";
    //        Clear();
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
    protected void LnkAuthorize_Click(object sender, EventArgs e)
    {
        try
        {

            ViewState["Flag"] = "ATH";
            SHOWGRID();
            txtUseId.Enabled = false;
            txtEntryDate.Enabled = false;
            txtFBrcd.Enabled = false;
            txtBrcdName.Enabled = false;
           
            BtnSubmit.Visible = false;
           // BtnSubmit.Text = "Authorize";
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
            SHOWGRID();
            txtUseId.Enabled = false;
            txtEntryDate.Enabled = false;
            txtFBrcd.Enabled = false;
            txtBrcdName.Enabled = false;
         
            BtnSubmit.Visible=false;
           Clear();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void EmptyGridBind()
    {


        DataTable DT = new DataTable();
        DT.Columns.AddRange(new DataColumn[11] {new DataColumn("VENDORID", typeof(int)),
                               new DataColumn("VENDERNAME", typeof(string)),new DataColumn("PRODID", typeof(int)),
                            new DataColumn("PRODNAME", typeof(string)),new DataColumn("QTY", typeof(int)),
                            new DataColumn("UNITCOST", typeof(string)),new DataColumn("SGSTAMT", typeof(int)),
                            new DataColumn("SGSTPER", typeof(int)),new DataColumn("CGSTPER", typeof(int)),
                            new DataColumn("CGSTAMT", typeof(string)),new DataColumn("AMOUNT", typeof(int))   });

        DataRow dr = DT.NewRow();
        DT.Rows.Add(dr);


        grdInsert.DataSource = DT;
        grdInsert.DataBind();



    }
    protected void txtFBrcd_TextChanged(object sender, EventArgs e)
    {
        txtBrcdName.Text = Cbr.getbrcode(txtFBrcd.Text);
       
    
    }

    protected void txtBrcdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = txtBrcdName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtBrcdName.Text = CT[0].ToString();
                txtFBrcd.Text = CT[1].ToString();
                string[] GLS = Cbr.getbrcode(txtFBrcd.Text).Split('_');

            }


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
            dt.Columns.AddRange(new DataColumn[11] {new DataColumn("VENDORID", typeof(int)),
                               new DataColumn("VENDERNAME", typeof(string)),new DataColumn("PRODID", typeof(int)),
                            new DataColumn("PRODNAME", typeof(string)),new DataColumn("QTY", typeof(int)),
                            new DataColumn("UNITCOST", typeof(string)),new DataColumn("SGSTAMT", typeof(string)),
                            new DataColumn("SGSTPER", typeof(string)),new DataColumn("CGSTPER", typeof(string)),
                            new DataColumn("CGSTAMT", typeof(string)),new DataColumn("AMOUNT", typeof(string))   });
            DataRow dr;

            foreach (GridViewRow gvRow in this.grdInsert.Rows)
            {
                dr = dt.NewRow();
                string VENDORID = Convert.ToString(((TextBox)gvRow.FindControl("VENDORID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("VENDORID")).Text);
                dr[0] = VENDORID;

                string VENDERNAME = Convert.ToString(((TextBox)gvRow.FindControl("VENDERNAME")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("VENDERNAME")).Text);
                dr[1] = VENDERNAME;

                string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODID")).Text);
                dr[2] = PRODUCTID;
                string PRODUCTNAME = Convert.ToString(((TextBox)gvRow.FindControl("PRODNAME")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODNAME")).Text);
                dr[3] = PRODUCTNAME;


                string QUANTITY = Convert.ToDouble(((TextBox)gvRow.FindControl("QTY")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("QTY")).Text).ToString();
                dr[4] = QUANTITY;

                string RATE = Convert.ToDouble(((TextBox)gvRow.FindControl("UNITCOST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("UNITCOST")).Text).ToString();
                dr[5] = RATE;

                //string GST = Convert.ToDouble(((TextBox)gvRow.FindControl("UNITCOST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("GST")).Text).ToString();
                //dr[6] = GST;

                string CGSTPER = Convert.ToDouble(((TextBox)gvRow.FindControl("CGSTPER")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("CGSTPER")).Text).ToString();
                dr[6] = CGSTPER;

                string SGSTPER = Convert.ToDouble(((TextBox)gvRow.FindControl("SGSTPER")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("SGSTPER")).Text).ToString();
                dr[7] = SGSTPER;

                string SGSTAMT = Convert.ToDouble(((TextBox)gvRow.FindControl("SGSTAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("SGSTAMT")).Text).ToString();
                dr[8] = SGSTAMT;

                string GSTAMT = Convert.ToDouble(((TextBox)gvRow.FindControl("CGSTAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("CGSTAMT")).Text).ToString();
                dr[9] = GSTAMT;

                string TOTALAMT = Convert.ToDouble(((TextBox)gvRow.FindControl("AMOUNT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("AMOUNT")).Text).ToString();
                dr[10] = TOTALAMT;


                Var += Convert.ToDouble(TOTALAMT);


                dt.Rows.Add(dr);
            }
            txtAmount.Text = Var.ToString();
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
    protected void PRODNAME_TextChanged(object sender, EventArgs e)
    {

        try
        {

            string currentRowIndex = hdnRowIndex.Value;
            GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(currentRowIndex) - 1];
            string VENDORID = Convert.ToString(((TextBox)gvRow.FindControl("VENDORID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("VENDORID")).Text);
            // string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODUCTID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODUCTID")).Text);
            string PRODUCTNAME = Convert.ToString(((TextBox)gvRow.FindControl("PRODNAME")).Text == "" ? "" : ((TextBox)gvRow.FindControl("PRODNAME")).Text);
            string[] CT = PRODUCTNAME.Split('_');
            //  string[] CT = CS.GetProductName1( PRODUCTID, txtVenderID.Text).Split('_');


            if (CT.Length > 0)
            {


                ((TextBox)gvRow.FindControl("PRODNAME")).Text = (string.IsNullOrEmpty(CT[0].ToString()) ? "" : CT[0].ToString());
                ((TextBox)gvRow.FindControl("PRODID")).Text = (string.IsNullOrEmpty(CT[1].ToString()) ? "" : CT[1].ToString());

                if (ViewState["PURCHASE"] == CT[0].ToString())
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Already Enter  !')", true);
                    ViewState["PURCHASE"] = "";
                }


                sResult = CS.DeadStock("PRODID", CT[1].ToString(), VENDORID);
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
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Belong  to VendorMaster!')", true);
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
                WebMsgBox.Show("Product  Already insert..!!", this.Page);
                PRODUCTNAME = "";
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void VENDERNAME_TextChanged(object sender, EventArgs e)
    {

        try
        {

            string currentRowIndex = hdnRowIndex.Value;
            GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(currentRowIndex) - 1];

            string PRODID = Convert.ToString(((TextBox)gvRow.FindControl("PRODID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODID")).Text);
            string VENDERNAME = Convert.ToString(((TextBox)gvRow.FindControl("VENDERNAME")).Text == "" ? "" : ((TextBox)gvRow.FindControl("VENDERNAME")).Text);
            string[] CT = VENDERNAME.Split('_');



            if (CT.Length > 0)
            {


                ((TextBox)gvRow.FindControl("VENDERNAME")).Text = (string.IsNullOrEmpty(CT[0].ToString()) ? "" : CT[0].ToString());
                ((TextBox)gvRow.FindControl("VENDORID")).Text = (string.IsNullOrEmpty(CT[1].ToString()) ? "" : CT[1].ToString());

                sResult = CS.DeadStock("VENDORID",CT[1].ToString());
                if (sResult == "NOTEXISTS")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vendor Not Present!')", true);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Authorize!')", true);
                    ((TextBox)gvRow.FindControl("VENDERNAME")).Text = (string.IsNullOrEmpty(CT[0].ToString()) ? "" : "");
                    ((TextBox)gvRow.FindControl("VENDORID")).Text = (string.IsNullOrEmpty(CT[1].ToString()) ? "" : "");
                    ((TextBox)gvRow.FindControl("VENDORID")).Focus();
                    ((TextBox)gvRow.FindControl("VENDERNAME")).Focus();
                    return;
                }
                else
                    if (sResult == "NOTAUTH")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vendor Not Authorize!')", true);
                        ((TextBox)gvRow.FindControl("VENDERNAME")).Text = (string.IsNullOrEmpty(CT[0].ToString()) ? "" : "");
                        ((TextBox)gvRow.FindControl("VENDORID")).Text = (string.IsNullOrEmpty(CT[1].ToString()) ? "" : "");
                        ((TextBox)gvRow.FindControl("VENDORID")).Focus();
                        ((TextBox)gvRow.FindControl("VENDERNAME")).Focus();
                        return;
                    }
                    //else
                        //if (sResult == "PRODIDNOTBELONGSTOVENDORID")
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vendor Not Available In VendorMaster!')", true);
                        //    ((TextBox)gvRow.FindControl("VENDERNAME")).Text = (string.IsNullOrEmpty(CT[0].ToString()) ? "" : "");
                        //    ((TextBox)gvRow.FindControl("VENDORID")).Text = (string.IsNullOrEmpty(CT[1].ToString()) ? "" : "");
                        //    ((TextBox)gvRow.FindControl("VENDORID")).Focus();
                        //    ((TextBox)gvRow.FindControl("VENDERNAME")).Focus();
                        //    return;
                        //}

                string[] name = VENDERNAME.Split('_');
                ViewState["PURCHASE"] = VENDERNAME[1].ToString();

                string[] custnob = ViewState["PURCHASE"].ToString().Split('_');
                if (custnob.Length > 1)
                {



                    ((TextBox)gvRow.FindControl("VENDERNAME")).Text = (string.IsNullOrEmpty(custnob[0].ToString()) ? "" : custnob[0].ToString());
                    ((TextBox)gvRow.FindControl("VENDORID")).Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                }

            }
            else
            {
                WebMsgBox.Show("Product Not Avialable Under Vendermaster..!!", this.Page);
                VENDERNAME = "";
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void QTY_TextChanged(object sender, EventArgs e)
    {
        try
        {

            
                string currentRowIndex = hdnRowIndex.Value;

            GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(currentRowIndex) - 1];

            string VENDORID = Convert.ToString(((TextBox)gvRow.FindControl("VENDORID")).Text == "" ? "" : ((TextBox)gvRow.FindControl("VENDORID")).Text);
            string QTY = Convert.ToString(((TextBox)gvRow.FindControl("QTY")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("QTY")).Text);

            string PRODID = Convert.ToString(((TextBox)gvRow.FindControl("PRODID")).Text == "" ? "" : ((TextBox)gvRow.FindControl("PRODID")).Text);
            if (VENDORID == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Enter VendorId And Vendorname')", true);
            }

            if (PRODID == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Enter ProductID And Productname')", true);
            }
           
            sResult = CS.DeadStock("CKSTOCKEXISTS", PRODID, VENDORID, txtFBrcd.Text, QTY);
            if (sResult == "NOTEXISTS")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Present!')", true);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Authorize!')", true);
                ((TextBox)gvRow.FindControl("PRODNAME")).Text = "";
                ((TextBox)gvRow.FindControl("PRODID")).Text = "";
                ((TextBox)gvRow.FindControl("PRODID")).Focus();
                ((TextBox)gvRow.FindControl("PRODNAME")).Focus();
                return;
            }
            else
                if (sResult == "NOTAUTH")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Authorize!')", true);
                    ((TextBox)gvRow.FindControl("PRODNAME")).Text = "";
                    ((TextBox)gvRow.FindControl("PRODID")).Text = "";
                    ((TextBox)gvRow.FindControl("PRODID")).Focus();
                    ((TextBox)gvRow.FindControl("PRODNAME")).Focus();
                    return;
                }
                else
                    if (sResult == "PRODIDNOTBELONGSTOVENDORID")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Available In VendorMaster!')", true);
                        ((TextBox)gvRow.FindControl("PRODNAME")).Text = "";
                        ((TextBox)gvRow.FindControl("PRODID")).Text = "";
                        ((TextBox)gvRow.FindControl("PRODID")).Focus();
                        ((TextBox)gvRow.FindControl("PRODNAME")).Focus();
                        return;
                    }

                    else if (sResult == "QTY0")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Enter No. Of Quantity!')", true);
                        ((TextBox)gvRow.FindControl("PRODNAME")).Text = "";
                        ((TextBox)gvRow.FindControl("PRODID")).Text = "";
                        ((TextBox)gvRow.FindControl("PRODID")).Focus();
                        ((TextBox)gvRow.FindControl("PRODNAME")).Focus();
                        return;
                    }
                    else if (sResult.StartsWith("STOCKAVAIL#"))
                    {

                        string[] name = sResult.Split('#');
                        // ViewState["PURCHASE"] = name[1].ToString();

                        // string[] custnob = name.ToString().Split('#');
                        if (name.Length >= 1)
                        {

                            string QTY1 = (string.IsNullOrEmpty(name[1].ToString()) ? "" : name[1].ToString());

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Available Product Quantity is " + QTY1 + "')", true);
                            ((TextBox)gvRow.FindControl("QTY")).Text = "";
                            ((TextBox)gvRow.FindControl(" QTY")).Focus();
                            return;

                        }



                    }
                    else if (sResult.StartsWith("SUCCESS"))
                    {

                        // int count = grdInsert.Rows.Count;

                        //   string currentRowIndex = hdnRowIndex.Value;


                        if (!String.IsNullOrEmpty(currentRowIndex) && Convert.ToInt32(currentRowIndex) > 0)
                        {
                            CalculatePurchase(currentRowIndex);
                            //  GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(currentRowIndex)];

                        }
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
            string str = "";
            GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(Index) - 1];
            string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODID")).Text);
            string QUANTITY = Convert.ToString(((TextBox)gvRow.FindControl("QTY")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("QTY")).Text);
            string VENDORID = Convert.ToString(((TextBox)gvRow.FindControl("VENDORID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("VENDORID")).Text);
            DT = CS.ProductCalculate("GETPRODUCTCALC", PRODUCTID, VENDORID);
            if (QUANTITY != null)
            {
                ((TextBox)gvRow.FindControl("SGSTPER")).Text = DT.Rows[0]["SGST"].ToString();
                ((TextBox)gvRow.FindControl("CGSTPER")).Text = DT.Rows[0]["CGST"].ToString();
                 ((TextBox)gvRow.FindControl("UNITCOST")).Text = DT.Rows[0]["RATE"].ToString();
                     str=  DT.Rows[0]["GST"].ToString();
                ((TextBox)gvRow.FindControl("SGSTAMT")).Text = ((Convert.ToDouble(DT.Rows[0]["SGSTAMT"])) * (Convert.ToDouble(QUANTITY))).ToString();
                ((TextBox)gvRow.FindControl("CGSTAMT")).Text = ((Convert.ToDouble(DT.Rows[0]["CGSTAMT"])) * (Convert.ToDouble(QUANTITY))).ToString();
                ((TextBox)gvRow.FindControl("AMOUNT")).Text = ((Convert.ToDouble(DT.Rows[0]["TOTALAMT"])) * (Convert.ToDouble(QUANTITY))).ToString();


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
        Cbr.BindForUse("SHOWUNAUTHORIZE", Session["BRCD"].ToString(), GrdUse);
        dt = Cbr.ShowUsed(FLAG: "SHOWUNAUTHORIZE", UseId: txtUseId.Text);


    }
    protected void GrdUse_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GrdUse_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        try
        {
            GrdUse.PageIndex = e.NewPageIndex;
            SHOWGRID();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }

    public void SHOWuseData()
    {
        txtUseId.Text = ViewState["USEDNO"].ToString();

        DataTable dt = new DataTable();
        string str = "0";





        DataRow dr;
        int j = 0;
        dt = Cbr.ShowUsed(FLAG: "SHOW", UseId: txtUseId.Text);

        grdInsert.DataSource = dt;
        grdInsert.DataBind();

        txtFBrcd.Text = dt.Rows[0]["BRCD"].ToString();
       
        txtEntryDate.Text = dt.Rows[0]["ENTRYDATE"].ToString();
        txtUseId.Text = dt.Rows[0]["USEDNO"].ToString();

    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        LinkButton lnkedit = (LinkButton)sender;
        string ISSUENO = lnkedit.CommandArgument.ToString();
        ViewState["USEDNO"] = ISSUENO;

        //BtnSubmit.Text = "Authorize";
        SHOWuseData();
        if (ViewState["Flag"].ToString() == "AD")
        {

            txtUseId.Enabled = false;
            txtEntryDate.Enabled = false;
            txtFBrcd.Enabled = false;
            txtBrcdName.Enabled = false;
            BtnSubmit.Text = "Create";
        }

        if (ViewState["Flag"].ToString() == "MD")
        {

            txtUseId.Enabled = false;
            txtEntryDate.Enabled = true;
            txtFBrcd.Enabled = true;
            txtBrcdName.Enabled = true;
            BtnSubmit.Visible = true;
            BtnSubmit.Text = "Modify";
        }
        if (ViewState["Flag"].ToString() == "ATH")
        {

            txtUseId.Enabled = false;
            txtEntryDate.Enabled = false;
            txtFBrcd.Enabled = false;
            txtBrcdName.Enabled = false;
            BtnSubmit.Visible = true;
            BtnSubmit.Text = "Authorized";
        }
        if (ViewState["Flag"].ToString() == "DEL")
        {

            txtUseId.Enabled = false;
            txtEntryDate.Enabled = false;
            txtFBrcd.Enabled = false;
            txtBrcdName.Enabled = false;
            BtnSubmit.Visible = true;
            BtnSubmit.Text = "Delete";
        }

    }

    public void Clear()
    {

        txtBrcdName.Text = "";
        txtFBrcd.Text = "";
        txtEntryDate.Text = "";
        txtUseId.Text = "";
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (ViewState["Flag"].ToString() == "AD")
        {

            if (txtFBrcd.Text == "")
            {
                WebMsgBox.Show("Enter BRCD..!!", this.Page);
                return;

            }

            try
            {

                txtUseId.Text = Cbr.USEDMASTER(FLAG: "GETID"); ;
                int j = 0;
                int t = 0;
                foreach (GridViewRow gvRow in this.grdInsert.Rows)
                {
                    string SRNO = Convert.ToString(((TextBox)gvRow.FindControl("SRNO")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("SRNO")).Text);
                    string VENDORID = Convert.ToString(((TextBox)gvRow.FindControl("VENDORID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("VENDORID")).Text);
                    string VENDERNAME = Convert.ToString(((TextBox)gvRow.FindControl("VENDERNAME")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("VENDERNAME")).Text);
                    //  string GST = Convert.ToString(((TextBox)gvRow.FindControl("GST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("GST")).Text);
                    string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODID")).Text);
                    string PRODUCTNAME = Convert.ToString(((TextBox)gvRow.FindControl("PRODNAME")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODNAME")).Text);
                    string QUANTITY = Convert.ToString(((TextBox)gvRow.FindControl("QTY")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("QTY")).Text);
                    string UNITCOST = Convert.ToString(((TextBox)gvRow.FindControl("UNITCOST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("UNITCOST")).Text);
                    string SGST = Convert.ToString(((TextBox)gvRow.FindControl("SGSTPER")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("SGSTPER")).Text);
                    string CGST = Convert.ToString(((TextBox)gvRow.FindControl("CGSTPER")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("CGSTPER")).Text);
                    string SGSTAMT = Convert.ToString(((TextBox)gvRow.FindControl("SGSTAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("SGSTAMT")).Text);
                    string CGSTAMT = Convert.ToString(((TextBox)gvRow.FindControl("CGSTAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("CGSTAMT")).Text);
                    string AMOUNT = Convert.ToString(((TextBox)gvRow.FindControl("AMOUNT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("AMOUNT")).Text);



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

                    ViewState["ShowNext"] = Cbr.USEDMASTER(FLAG: "AD", USEID: txtUseId.Text, FROMBRCD: txtFBrcd.Text, PRODID: PRODUCTID, VENDORID: VENDORID, CGSTAMT: CGSTAMT, SGSTAMT: SGSTAMT, SRNO: SRNO, QTY: QUANTITY, UNITCOST: UNITCOST, CGST: SGST, SGST: SGST, AMOUNT: AMOUNT, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString());
                    sResult = ViewState["ShowNext"].ToString();
                    if (sResult == "1")
                    {
                        t += 1;
                    }

                    j = j + 1;
                }
                if (t > 0)
                {
                    Cbr.USEDMASTER(FLAG: "UPDATEID");
                    string title = sResult.Replace("\n", " "); ;
                    string body = "Welcome to ASPSnippets.com";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "ShowPopup('Reference No." + txtUseId.Text + " Create Succefully!');", true);
                    Clear();
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
                    string VENDORID = Convert.ToString(((TextBox)gvRow.FindControl("VENDORID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("VENDORID")).Text);
                    string VENDERNAME = Convert.ToString(((TextBox)gvRow.FindControl("VENDERNAME")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("VENDERNAME")).Text);
                    //  string GST = Convert.ToString(((TextBox)gvRow.FindControl("GST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("GST")).Text);
                    string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODID")).Text);
                    string PRODUCTNAME = Convert.ToString(((TextBox)gvRow.FindControl("PRODNAME")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODNAME")).Text);
                    string QUANTITY = Convert.ToString(((TextBox)gvRow.FindControl("QTY")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("QTY")).Text);
                    string UNITCOST = Convert.ToString(((TextBox)gvRow.FindControl("UNITCOST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("UNITCOST")).Text);
                    string SGST = Convert.ToString(((TextBox)gvRow.FindControl("SGSTPER")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("SGSTPER")).Text);
                    string CGST = Convert.ToString(((TextBox)gvRow.FindControl("CGSTPER")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("CGSTPER")).Text);
                    string SGSTAMT = Convert.ToString(((TextBox)gvRow.FindControl("SGSTAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("SGSTAMT")).Text);
                    string CGSTAMT = Convert.ToString(((TextBox)gvRow.FindControl("CGSTAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("CGSTAMT")).Text);
                    string AMOUNT = Convert.ToString(((TextBox)gvRow.FindControl("AMOUNT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("AMOUNT")).Text);


                    sResult = Cbr.USEDMASTER(FLAG: "ATH", USEID: txtUseId.Text, FROMBRCD: txtFBrcd.Text, PRODID: PRODUCTID, VENDORID: VENDORID, CGSTAMT: CGSTAMT, SGSTAMT: SGSTAMT, SRNO: SRNO, QTY: QUANTITY, UNITCOST: UNITCOST, CGST: SGST, SGST: SGST, AMOUNT: AMOUNT, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString());
              
                   
                    if (sResult.StartsWith("ATH#"))
                    {

                        string[] Array = sResult.Split('#');

                        string title = sResult.Replace("\n", " "); ;
                        string body = "Welcome to ASPSnippets.com";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "ShowPopup('Authorize Succefully!');", true);
                          Clear();
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
                    string VENDORID = Convert.ToString(((TextBox)gvRow.FindControl("VENDORID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("VENDORID")).Text);
                    string VENDERNAME = Convert.ToString(((TextBox)gvRow.FindControl("VENDERNAME")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("VENDERNAME")).Text);
                    //  string GST = Convert.ToString(((TextBox)gvRow.FindControl("GST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("GST")).Text);
                    string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODID")).Text);
                    string PRODUCTNAME = Convert.ToString(((TextBox)gvRow.FindControl("PRODNAME")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODNAME")).Text);
                    string QUANTITY = Convert.ToString(((TextBox)gvRow.FindControl("QTY")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("QTY")).Text);
                    string UNITCOST = Convert.ToString(((TextBox)gvRow.FindControl("UNITCOST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("UNITCOST")).Text);
                    string SGST = Convert.ToString(((TextBox)gvRow.FindControl("SGSTPER")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("SGSTPER")).Text);
                    string CGST = Convert.ToString(((TextBox)gvRow.FindControl("CGSTPER")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("CGSTPER")).Text);
                    string SGSTAMT = Convert.ToString(((TextBox)gvRow.FindControl("SGSTAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("SGSTAMT")).Text);
                    string CGSTAMT = Convert.ToString(((TextBox)gvRow.FindControl("CGSTAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("CGSTAMT")).Text);
                    string AMOUNT = Convert.ToString(((TextBox)gvRow.FindControl("AMOUNT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("AMOUNT")).Text);

                    sResult = Cbr.USEDMASTER(FLAG: "DEL", USEID: txtUseId.Text, FROMBRCD: txtFBrcd.Text, PRODID: PRODUCTID, VENDORID: VENDORID, CGSTAMT: CGSTAMT, SGSTAMT: SGSTAMT, SRNO: SRNO, QTY: QUANTITY, UNITCOST: UNITCOST, CGST: SGST, SGST: SGST, AMOUNT: AMOUNT, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString());
                     
                    if (sResult.StartsWith("DEL#"))
                    {

                       
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "ShowPopup('Delete Succefully!');", true);
                        Clear();
                    }

                    else if (sResult == "NOTEXISTS")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Purchaser Not Present!')", true);
                        // Clear();
                    }
                    else if (sResult == ("ALREADYAUTH"))
                    {

                        // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Please Try Again!!!!!');", true);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You Are Already Deleted !')", true);
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
}