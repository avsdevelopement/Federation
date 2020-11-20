using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmAVS51178 : System.Web.UI.Page
{
      ClsAuthorized PVOUCHER = new ClsAuthorized();
    ClsShareApp SA = new ClsShareApp();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAVS51173 CS = new ClsAVS51173();
    clsAVS51178 Cbr = new clsAVS51178();
    ClsCommon cmn = new ClsCommon();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string sResult = "", sResult1 = "";
    int Result = 0;
    int resultint;
      string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
            Response.Redirect("FrmLogin.aspx");

        if (!IsPostBack)
        {
            BD.BindPayment(ddlPayType, "1");
            ViewState["Flag"] = "AD";
            txtFBrcd.Text = Session["BRCD"].ToString();
            txtIssueDate.Text = Session["EntryDate"].ToString();
            EmptyGridBind();
            AutoBrcd.ContextKey = Session["BRCD"].ToString();
            // Autoglname.ContextKey = Session["MID"].ToString();
            txtBrcdName.Text = Cbr.getbrcode(txtFBrcd.Text);
            txtIssueId.Text = Cbr.IssueMaster(FLAG: "GETID");
            Transfer.Visible = false;
            divIntrument.Visible = false;
            divNarration.Visible = false;
            BtnSubmit.Text = "Create";
            SHOWGRID();
            if (ViewState["Flag"].ToString() == "ATH")
            {
                txtBrcdName.Text = Cbr.getbrcode(txtFBrcd.Text);
                txtTBrcdName.Text = Cbr.getbrcode(txtFBrcd.Text);
            }
            if (ViewState["Flag"].ToString() == "MD")
            {
                txtBrcdName.Text = Cbr.getbrcode(txtFBrcd.Text);
                txtTBrcdName.Text = Cbr.getbrcode(txtFBrcd.Text);
            }
            if (ViewState["Flag"].ToString() == "DEl")
            {
                txtBrcdName.Text = Cbr.getbrcode(txtFBrcd.Text);
                txtTBrcdName.Text = Cbr.getbrcode(txtFBrcd.Text);
            }
        }
    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Flag"] = "AD";
            txtIssueId.Text = Cbr.IssueMaster(FLAG: "GETID");
            BtnSubmit.Text = "Create";
            txtFBrcd.Text = Session["BRCD"].ToString();
            txtIssueDate.Text = Session["EntryDate"].ToString();
            EmptyGridBind();
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
            BtnSubmit.Visible = false;
            txtIssueId.Enabled = true;
            txtFBrcd.Enabled = true;
            txtBrcdName.Enabled = true;
            BtnSubmit.Text = "Modify";
            txtTBrcdName.Text = Cbr.getbrcode(txtFBrcd.Text);
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
            Clear();
            ViewState["Flag"] = "ATH";
            BtnSubmit.Visible = true;
            txtIssueId.Enabled = false;
            txtFBrcd.Enabled = false;
            txtBrcdName.Enabled = false;
            txttBRCD.Enabled = false;
            txtTBrcdName.Enabled = false;
            BtnSubmit.Text = "Authorize";
            txtTBrcdName.Text = Cbr.getbrcode(txtFBrcd.Text);

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
            BtnSubmit.Visible = false;
            txtIssueId.Enabled = false;
            txtFBrcd.Enabled = false;
            txtBrcdName.Enabled = false;
            txttBRCD.Enabled = false;
            txtTBrcdName.Enabled = false;
            BtnSubmit.Text = "Delete";
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
        DT.Columns.AddRange(new DataColumn[11] {
           new DataColumn("VENDORID", typeof(int)),
                              new DataColumn("VENDERNAME", typeof(string)),
                               new DataColumn("PRODID", typeof(int)),
                            new DataColumn("PRODNAME", typeof(string)),new DataColumn("QTY", typeof(int)),
                            new DataColumn("UNITCOST", typeof(string)),new DataColumn("SGSTAMT", typeof(int)),
                            new DataColumn("SGSTPER", typeof(int)),new DataColumn("CGSTPER", typeof(int)),
                            new DataColumn("CGSTAMT", typeof(string)),new DataColumn("AMOUNT", typeof(int))   });

        DataRow dr = DT.NewRow();
        DT.Rows.Add(dr);


        grdInsert.DataSource = DT;
        grdInsert.DataBind();



    }
    protected void txtTBrcd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = txtBrcdName.Text.ToString();
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtTBrcdName.Text = CT[0].ToString();
                txttBRCD.Text = CT[1].ToString();
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
            dt.Columns.AddRange(new DataColumn[11] {
              new DataColumn("VENDORID", typeof(int)),                                               //changes as per darade sir requirement
                              new DataColumn("VENDERNAME", typeof(string)),
                               new DataColumn("PRODID", typeof(int)),
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

                sResult = CS.DeadStock("PRODID", CT[1].ToString(), "VENDORID", CT[2].ToString());  //, VENDORID
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
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Belongs to Vendormaster!')", true);
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

                sResult = CS.DeadStock("VENDORID", CT[1].ToString(), PRODID);
                if (sResult == "NOTEXISTS")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Present!')", true);

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
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Authorize!')", true);
                        ((TextBox)gvRow.FindControl("VENDERNAME")).Text = (string.IsNullOrEmpty(CT[0].ToString()) ? "" : "");
                        ((TextBox)gvRow.FindControl("VENDORID")).Text = (string.IsNullOrEmpty(CT[1].ToString()) ? "" : "");
                        ((TextBox)gvRow.FindControl("VENDORID")).Focus();
                        ((TextBox)gvRow.FindControl("VENDERNAME")).Focus();
                        return;
                    }
                    else
                        if (sResult == "PRODIDNOTBELONGSTOVENDORID")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Product Not Available In VendorMaster!')", true);
                            ((TextBox)gvRow.FindControl("VENDERNAME")).Text = (string.IsNullOrEmpty(CT[0].ToString()) ? "" : "");
                            ((TextBox)gvRow.FindControl("VENDORID")).Text = (string.IsNullOrEmpty(CT[1].ToString()) ? "" : "");
                            ((TextBox)gvRow.FindControl("VENDORID")).Focus();
                            ((TextBox)gvRow.FindControl("VENDERNAME")).Focus();
                            return;
                        }

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

            string VENDORID = Convert.ToString(((TextBox)gvRow.FindControl("VENDORID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("VENDORID")).Text);
            string QTY = Convert.ToString(((TextBox)gvRow.FindControl("QTY")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("QTY")).Text);

            string PRODID = Convert.ToString(((TextBox)gvRow.FindControl("PRODID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODID")).Text);

            sResult = CS.DeadStock("CKSTOCKEXISTS", PRODID, txtFBrcd.Text, QTY, VENDORID); //VENDORID,
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
            GridViewRow gvRow = grdInsert.Rows[Convert.ToInt32(Index) - 1];
            string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODID")).Text);
            string QUANTITY = Convert.ToString(((TextBox)gvRow.FindControl("QTY")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("QTY")).Text);
            string VENDORID = Convert.ToString(((TextBox)gvRow.FindControl("VENDORID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("VENDORID")).Text); //
            DT = CS.ProductCalculate("GETPRODUCTCALC", PRODUCTID, VENDORID);                    //, VENDORID
            if (QUANTITY != null)
            {
                // ((TextBox)gvRow.FindControl("SGSTPER")).Text = ((Convert.ToDouble(DT.Rows[0]["SGSTPER"])) * (Convert.ToDouble(QUANTITY))).ToString();
                // ((TextBox)gvRow.FindControl("CGST")).Text = ((Convert.ToDouble(DT.Rows[0]["CGSTPER"])) * (Convert.ToDouble(QUANTITY))).ToString();
                ((TextBox)gvRow.FindControl("SGSTPER")).Text = DT.Rows[0]["SGST"].ToString();
                ((TextBox)gvRow.FindControl("CGSTPER")).Text = DT.Rows[0]["CGST"].ToString();
                //((TextBox)gvRow.FindControl("GST")).Text = DT.Rows[0]["GST"].ToString();
                ((TextBox)gvRow.FindControl("UNITCOST")).Text = DT.Rows[0]["RATE"].ToString();
                ((TextBox)gvRow.FindControl("SGSTAMT")).Text = ((Convert.ToDouble(DT.Rows[0]["SGSTAMT"])) * (Convert.ToDouble(QUANTITY))).ToString();
                ((TextBox)gvRow.FindControl("CGSTAMT")).Text = ((Convert.ToDouble(DT.Rows[0]["CGSTAMT"])) * (Convert.ToDouble(QUANTITY))).ToString();
                // ((TextBox)gvRow.FindControl("GSTAMT")).Text = ((Convert.ToDouble(DT.Rows[0]["GSTAMT"])) * (Convert.ToDouble(QUANTITY))).ToString();
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
        Cbr.BindForIssue("SHOWUNAUTHORIZE", Session["BRCD"].ToString(), GrdIssue);
        dt = Cbr.ShowIssue(FLAG: "SHOWUNAUTHORIZE", IssueID: txtIssueId.Text);


    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (ViewState["Flag"].ToString() == "AD")
        {

            if (txttBRCD.Text == "")
            {
                WebMsgBox.Show("Enter BRCD..!!", this.Page);
                return;

            }

            try
            {

                txtIssueId.Text = Cbr.IssueMaster(FLAG: "GETID"); ;
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
                    string SGSTPER = Convert.ToString(((TextBox)gvRow.FindControl("SGSTPER")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("SGSTPER")).Text);
                    string CGSTPER = Convert.ToString(((TextBox)gvRow.FindControl("CGSTPER")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("CGSTPER")).Text);
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

                    ViewState["ShowNext"] = Cbr.IssueMaster(FLAG: "AD", ISSUENO: txtIssueId.Text, FROMBRCD: txtFBrcd.Text, TOBRCD: txttBRCD.Text, PRODID: PRODUCTID, CGSTAMT: CGSTAMT, SGSTAMT: SGSTAMT, SRNO: SRNO, QTY: QUANTITY, UNITCOST: UNITCOST, CGSTPER: CGSTPER, SGSTPER: SGSTPER, AMOUNT: AMOUNT, SubGlCode: txtProdType1.Text, Accno: TxtAccNo1.Text, Particulars: txtNarration.Text, InstNo: TxtChequeNo.Text, InstDate: TxtChequeDate.Text, PmtMode: ddlPayType.SelectedValue, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString());            //VENDORID: VENDORID
                    sResult = ViewState["ShowNext"].ToString();
                    if (sResult == "1")
                    {
                        t += 1;
                    }

                    j = j + 1;


                    //if (ddlPayType.SelectedValue.ToString() == "1")
                    //{

                    //    //For Cash

                    //    Result = Cbr.InsertData( ddlPayType.SelectedValue, txtProdType1.Text, TxtAccNo1.Text,txtNarration.Text,TxtChequeNo.Text,TxtChequeDate.Text, Session["EntryDate"].ToString(),Session["MID"].ToString());           

                    //}
                    //else if (ddlPayType.SelectedValue.ToString() == "2")
                    //{

                    //    //For Transfer
                    //    Result = Cbr.InsertData(ddlPayType.SelectedValue, txtProdType1.Text, TxtAccNo1.Text, txtNarration.Text, TxtChequeNo.Text, TxtChequeDate.Text, Session["EntryDate"].ToString(), Session["MID"].ToString());           


                    //}
                    //else if (ddlPayType.SelectedValue.ToString() == "4")
                    //{

                    //    //For Cheque
                    //    Result = Cbr.InsertData(ddlPayType.SelectedValue, txtProdType1.Text, TxtAccNo1.Text, txtNarration.Text, TxtChequeNo.Text, TxtChequeDate.Text, Session["EntryDate"].ToString(), Session["MID"].ToString());           

                    //}
                }

                if (t > 0)
                {
                    Cbr.IssueMaster(FLAG: "UPDATEID");
                    string title = sResult.Replace("\n", " "); ;
                    string body = "Welcome to ASPSnippets.com";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "ShowPopup('" + txtIssueId.Text + " Create Succefully!');", true);
                    //  Clear();
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
                    //string VENDORID = Convert.ToString(((TextBox)gvRow.FindControl("VENDORID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("VENDORID")).Text);   //changes As per darade Sir requirements
                    // string VENDERNAME = Convert.ToString(((TextBox)gvRow.FindControl("VENDERNAME")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("VENDERNAME")).Text);   //changes As per darade Sir requirements
                    //  string GST = Convert.ToString(((TextBox)gvRow.FindControl("GST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("GST")).Text);
                    string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODID")).Text);
                    string PRODUCTNAME = Convert.ToString(((TextBox)gvRow.FindControl("PRODNAME")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODNAME")).Text);
                    string QUANTITY = Convert.ToString(((TextBox)gvRow.FindControl("QTY")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("QTY")).Text);
                    string UNITCOST = Convert.ToString(((TextBox)gvRow.FindControl("UNITCOST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("UNITCOST")).Text);
                    string SGSTPER = Convert.ToString(((TextBox)gvRow.FindControl("SGSTPER")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("SGSTPER")).Text);
                    string CGSTPER = Convert.ToString(((TextBox)gvRow.FindControl("CGSTPER")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("CGSTPER")).Text);
                    string SGSTAMT = Convert.ToString(((TextBox)gvRow.FindControl("SGSTAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("SGSTAMT")).Text);
                    string CGSTAMT = Convert.ToString(((TextBox)gvRow.FindControl("CGSTAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("CGSTAMT")).Text);
                    string AMOUNT = Convert.ToString(((TextBox)gvRow.FindControl("AMOUNT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("AMOUNT")).Text);


                    ViewState["ShowNext"] = Cbr.IssueMaster(FLAG: "ATH", ISSUENO: txtIssueId.Text, FROMBRCD: txtFBrcd.Text, TOBRCD: txttBRCD.Text, PRODID: PRODUCTID, CGSTAMT: CGSTAMT, SGSTAMT: SGSTAMT, SRNO: SRNO, QTY: QUANTITY, UNITCOST: UNITCOST, CGSTPER: CGSTPER, SGSTPER: SGSTPER, AMOUNT: AMOUNT, SubGlCode: txtProdType1.Text, Accno: TxtAccNo1.Text, Particulars: txtNarration.Text, InstNo: TxtChequeNo.Text, InstDate: TxtChequeDate.Text, PmtMode: ddlPayType.SelectedValue, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString());   //VENDORID: VENDORID,
                   AuthoriseApplication();
                    sResult = ViewState["ShowNext"].ToString();
                    if (sResult.StartsWith("ATH#"))
                    {

                        string[] Array = sResult.Split('#');

                        string title = sResult.Replace("\n", " "); ;
                        string body = "Welcome to ASPSnippets.com";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "ShowPopup('Authorize Succefully!');", true);
                        //  Clear();
                    }

                    else if (sResult == "NOTEXISTS")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Purchaser Not Present!')", true);
                        // Clear();
                    }
                    else if (sResult == ("NOTAUTH"))
                    {

                        // ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('Please Try Again!!!!!');", true);
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You Are Not Authorize Person!')", true);
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
                    //string VENDORID = Convert.ToString(((TextBox)gvRow.FindControl("VENDORID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("VENDORID")).Text);  //changes As per darade Sir requirements
                    //string VENDERNAME = Convert.ToString(((TextBox)gvRow.FindControl("VENDERNAME")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("VENDERNAME")).Text);
                    //  string GST = Convert.ToString(((TextBox)gvRow.FindControl("GST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("GST")).Text);
                    string PRODUCTID = Convert.ToString(((TextBox)gvRow.FindControl("PRODID")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODID")).Text);
                    string PRODUCTNAME = Convert.ToString(((TextBox)gvRow.FindControl("PRODNAME")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("PRODNAME")).Text);
                    string QUANTITY = Convert.ToString(((TextBox)gvRow.FindControl("QTY")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("QTY")).Text);
                    string UNITCOST = Convert.ToString(((TextBox)gvRow.FindControl("UNITCOST")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("UNITCOST")).Text);
                    string SGSTPER = Convert.ToString(((TextBox)gvRow.FindControl("SGSTPER")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("SGSTPER")).Text);
                    string CGSTPER = Convert.ToString(((TextBox)gvRow.FindControl("CGSTPER")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("CGSTPER")).Text);
                    string SGSTAMT = Convert.ToString(((TextBox)gvRow.FindControl("SGSTAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("SGSTAMT")).Text);
                    string CGSTAMT = Convert.ToString(((TextBox)gvRow.FindControl("CGSTAMT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("CGSTAMT")).Text);
                    string AMOUNT = Convert.ToString(((TextBox)gvRow.FindControl("AMOUNT")).Text == "" ? "0" : ((TextBox)gvRow.FindControl("AMOUNT")).Text);

                    ViewState["ShowNext"] = Cbr.IssueMaster(FLAG: "DEL", ISSUENO: txtIssueId.Text, FROMBRCD: txtFBrcd.Text, TOBRCD: txttBRCD.Text, PRODID: PRODUCTID, CGSTAMT: CGSTAMT, SGSTAMT: SGSTAMT, SRNO: SRNO, QTY: QUANTITY, UNITCOST: UNITCOST, CGSTPER: CGSTPER, SGSTPER: SGSTPER, AMOUNT: AMOUNT, ENTRYDATE: Session["EntryDate"].ToString(), MID: Session["MID"].ToString());  //VENDORID: VENDORID,
                    sResult = ViewState["ShowNext"].ToString();
                    if (sResult.StartsWith("DEL#"))
                    {

                        string[] Array = sResult.Split('#');

                        string title = sResult.Replace("\n", " "); ;
                        string body = "Welcome to ASPSnippets.com";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Popup", "ShowPopup('Delete Succefully!');", true);
                        //  Clear();
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
    protected void GrdIssue_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GrdIssue_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    public void SHOWIssueData()
    {
        txtIssueId.Text = ViewState["ISSUENO"].ToString();

        DataTable dt = new DataTable();
        string str = "0";
        DataRow dr;
        int j = 0;
        dt = Cbr.ShowIssue(FLAG: "SHOW", IssueID: txtIssueId.Text);

        grdInsert.DataSource = dt;
        grdInsert.DataBind();

        txtFBrcd.Text = dt.Rows[0]["FROMBRCD"].ToString();
        txttBRCD.Text = dt.Rows[0]["TOBRCD"].ToString();
        txtIssueDate.Text = dt.Rows[0]["ENTRYDATE"].ToString();
        txtIssueId.Text = dt.Rows[0]["ISSUENO"].ToString();
        ddlPayType.SelectedValue = dt.Rows[0]["PMTMode"].ToString() == "" ? "0" : DT.Rows[0]["PMTMode"].ToString();
        txtProdType1.Text=dt.Rows[0]["SubGlCode"].ToString();
       // txtProdName1.Text=dt.Rows[0][].ToString();
        TxtAccNo1.Text=dt.Rows[0]["Accno"].ToString();
       //TxtAccName1.Text=dt.Rows[0][].ToString();
        TxtChequeNo.Text=dt.Rows[0]["InstNo"].ToString();
        TxtChequeDate.Text=dt.Rows[0]["InstDate"].ToString();
        //txtBalance.Text=dt.Rows[0][].ToString();
        txtNarration.Text=dt.Rows[0]["Particulars"].ToString();
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        LinkButton lnkedit = (LinkButton)sender;
        string ISSUENO = lnkedit.CommandArgument.ToString();
        ViewState["ISSUENO"] = ISSUENO;
        txtBrcdName.Text = Cbr.getbrcode(txtFBrcd.Text);
        txtTBrcdName.Text = Cbr.getbrcode(txtFBrcd.Text);
        BtnSubmit.Visible = true;

        SHOWIssueData();
    }
    public void Clear1()
    {

        txttBRCD.Text = "";
        txtTBrcdName.Text = "";


    }
    public void Clear()
    {
        txtBrcdName.Text = "";
        txttBRCD.Text = "";
        txtTBrcdName.Text = "";
        txtFBrcd.Text = "";
        txtIssueDate.Text = "";
        txtIssueId.Text = "";
    }
    protected void txttBRCD_TextChanged(object sender, EventArgs e)
    {
        txtTBrcdName.Text = Cbr.getbrcode(txttBRCD.Text);
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
    protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPayType.SelectedValue.ToString() == "0")
        {
            txtFBrcd.Text = Session["BRCD"].ToString();
            txtIssueDate.Text = Session["EntryDate"].ToString();
            Transfer.Visible = false;
            divIntrument.Visible = false;
            divNarration.Visible = false;
            ddlPayType.Focus();

        }
        else if (ddlPayType.SelectedValue.ToString() == "1")
        {
            Transfer.Visible = false;
            divIntrument.Visible = false;
            divNarration.Visible = true;
            txtNarration.Text = "By Cash";
            //txtAmount.Text = txtTotAmount.Text.Trim().ToString();
            txtFBrcd.Text = Session["BRCD"].ToString();
            txtIssueDate.Text = Session["EntryDate"].ToString();

            txtNarration.Focus();
        }
        else if (ddlPayType.SelectedValue.ToString() == "2")
        {

            Transfer.Visible = true;
            divIntrument.Visible = false;
            divNarration.Visible = true;
            txtNarration.Text = "By Transfer";
            //txtAmount.Text = txtTotAmount.Text.Trim().ToString();

            txtFBrcd.Text = Session["BRCD"].ToString();
            txtIssueDate.Text = Session["EntryDate"].ToString();
            txtProdType1.Focus();
        }
        else if (ddlPayType.SelectedValue.ToString() == "4")
        {

            Transfer.Visible = true;
            divIntrument.Visible = true;
            divNarration.Visible = true;
            txtNarration.Text = "By Cheque";
            //txtAmount.Text = txtTotAmount.Text.Trim().ToString();
            TxtChequeDate.Text = Session["EntryDate"].ToString();
            txtFBrcd.Text = Session["BRCD"].ToString();
            txtIssueDate.Text = Session["EntryDate"].ToString();

            txtProdType1.Focus();
        }
        else
        {

            Transfer.Visible = false;
            divIntrument.Visible = false;
            divNarration.Visible = false;
            txtFBrcd.Text = Session["BRCD"].ToString();
            txtIssueDate.Text = Session["EntryDate"].ToString();
            ddlPayType.Focus();
        }
    }
    protected void txtProdType1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;
            AC1 = Cbr.Getaccno(txtProdType1.Text, Session["BRCD"].ToString(), "");

            if (AC1 != null)
            {
                string[] AC = AC1.Split('-'); ;
                ViewState["GlCode"] = AC[1].ToString();
                txtProdName1.Text = AC[2].ToString();
                AutoAccname1.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType1.Text + "_" + ViewState["GlCode"].ToString();

                if (Convert.ToInt32(txtProdType1.Text) >= 100)
                {
                    string[] TD = Session["EntryDate"].ToString().Split('/');
                    txtBalance.Text = Cbr.GetOpenClose(TD[2].ToString(), TD[1].ToString(), txtProdType1.Text, TxtAccNo1.Text.ToString() == "" ? "0" : TxtAccNo1.Text.ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString()).ToString();
                    TxtAccNo1.Text = txtProdType1.Text.ToString();
                    TxtAccName1.Text = txtProdName1.Text.ToString();

                    TxtChequeNo.Focus();
                }
                else
                {
                    TxtAccNo1.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter valid Product code!.....", this.Page);
                txtProdType1.Text = "";
                txtProdName1.Text = "";
                txtProdType1.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtProdName1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtProdName1.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtProdName1.Text = custnob[0].ToString();
                txtProdType1.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] AC = Cbr.Getaccno(txtProdType1.Text, Session["BRCD"].ToString(), custnob[2].ToString()).Split('-');
                ViewState["GlCode"] = AC[1].ToString();
                AutoAccname1.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType1.Text;

                if (Convert.ToInt32(txtProdType1.Text) > 100)
                {
                    string[] TD = Session["EntryDate"].ToString().Split('/');
                    txtBalance.Text = Cbr.GetOpenClose(TD[2].ToString(), TD[1].ToString(), txtProdType1.Text, TxtAccNo1.Text.ToString() == "" ? "0" : TxtAccNo1.Text.ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString()).ToString();

                    TxtChequeNo.Focus();
                }
                else
                {
                    TxtAccNo1.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAccNo1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";
            AT = BD.GetSHstage(TxtAccNo1.Text, Session["BRCD"].ToString(), txtProdType1.Text);
            if (AT != null)
            {
                if (AT != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise.........!!";
                    ModalPopup.Show(this.Page);
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";
                    TxtAccNo1.Focus();
                }
                else
                {
                    DataTable DT = new DataTable();
                    DT = SA.GetCustName(txtProdType1.Text, TxtAccNo1.Text, Session["BRCD"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                        TxtAccName1.Text = CustName[0].ToString();

                        string[] TD = Session["EntryDate"].ToString().Split('/');
                        txtBalance.Text = Cbr.GetOpenClose(TD[2].ToString(), TD[1].ToString(), txtProdType1.Text, TxtAccNo1.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString()).ToString();

                        TxtChequeNo.Focus();
                    }
                }
            }
            else
            {
                lblMessage.Text = "Enter valid account number...!!";
                ModalPopup.Show(this.Page);
                TxtAccNo1.Text = "";
                TxtAccNo1.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAccName1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtAccName1.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtAccName1.Text = custnob[0].ToString();
                TxtAccNo1.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                string[] TD = Session["EntryDate"].ToString().Split('/');
                txtBalance.Text = Cbr.GetOpenClose(TD[2].ToString(), TD[1].ToString(), txtProdType1.Text, TxtAccNo1.Text, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString()).ToString();

                TxtChequeNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void AuthoriseApplication()
    {
        try
        {
            string SetNo = "", SetNo2 = "", GlCode = "";//, SAccNo = "";

            int  Mid = Cbr.CheckMid(txtIssueId.Text);

            if (Mid !=0)
            {
                string Stage = Cbr.CheckStage(txtIssueId.Text);

                if (Stage != "1003" && Stage != "1004")
                {
                    
                        if (ddlPayType.SelectedValue.ToString() == "1")
                        {
                            SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
                            DataTable dt=new DataTable();
                            //For Cash
                            dt = Cbr.GetProdCode(Session["BRCD"].ToString(), txtIssueId.Text);

                            if (dt.Rows.Count > 0)
                            {
                                //double FinalValue;
                                    //FinalValue = Convert.ToDouble(Convert.ToDouble(txtTotShr.Text.ToString()) + Convert.ToDouble(txtEntFee.Text.ToString()) + Convert.ToDouble(txtSavFee.Text.ToString()) + Convert.ToDouble(txtOther1.Text.ToString()) + Convert.ToDouble(txtOther2.Text.ToString()) + Convert.ToDouble(txtOther3.Text.ToString()) + Convert.ToDouble(txtOther4.Text.ToString()) + Convert.ToDouble(txtOther5.Text.ToString()) + Convert.ToDouble(txtMemWelFee.Text.ToString()) + Convert.ToDouble(txtSerChrFee.Text.ToString()));

                                    string cgl = BD.GetCashGl("100", Session["BRCD"].ToString());

                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", cgl, "0",
                                              "ISSUE ID " + ViewState["ISSUENO"].ToString() + "","", txtAmount.Text.ToString(), "2", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                              "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "By Cash", txtProdType1.Text.ToString(), TxtAccNo1.Text.ToString(),TxtAccName1.Text, "0", "0");
                                
                                                                    
                               
                            }
                        }
                        else if (ddlPayType.SelectedValue.ToString() == "2")
                        {
                            SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
                            DataTable dt=new DataTable();
                            //For Transfer
                            dt = Cbr.GetProdCode(Session["BRCD"].ToString(), txtIssueId.Text);

                            if (dt.Rows.Count > 0)
                            {
                                  
                            
                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString(), txtProdType1.Text.Trim().ToString(),
                                              TxtAccNo1.Text.Trim().ToString(), "ISSUE ID " + ViewState["ISSUENO"].ToString() + "", "", txtAmount.Text.ToString(), "2", "7", "TR", SetNo, TxtChequeNo.Text.ToString() == "" ? "" : TxtChequeNo.Text.ToString(), TxtChequeDate.Text.ToString() == "" ? "" : TxtChequeDate.Text.ToString(), "0", "0", "1003",
                                              "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "By Trans", txtProdType1.Text.ToString(), TxtAccNo1.Text.ToString(), TxtAccName1.Text, "0", "0");
                                }

                        
                        }
                        else if (ddlPayType.SelectedValue.ToString() == "4")
                        {
                            SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
                            DataTable dt=new DataTable();
                            //For Cheque
                            dt = Cbr.GetProdCode(Session["BRCD"].ToString(), txtIssueId.Text);

                            if (dt.Rows.Count > 0)
                            {
                                
                                         resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString(), txtProdType1.Text.Trim().ToString(),
                                              TxtAccNo1.Text.Trim().ToString(), "ISSUE ID " + ViewState["ISSUENO"].ToString() + "", "", txtAmount.Text.ToString(), "2", "5", "TR", SetNo, TxtChequeNo.Text.ToString() == "" ? "" : TxtChequeNo.Text.ToString(), TxtChequeDate.Text.ToString() == "" ? "" : TxtChequeDate.Text.ToString(), "0", "0", "1003",
                                              "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "By Cheque", txtProdType1.Text.ToString(), TxtAccNo1.Text.ToString(), TxtAccName1.Text, "0", "0");


                                

                            }
                        }

                        }

                        if (resultint > 0)
                        {
                            
                                ViewState["SetNo"] = Convert.ToInt32(SetNo.ToString()).ToString();
                                
                                lblMessage.Text = "Authorise Successfully With SetNo : '" + SetNo.ToString() + "'";
                                ModalPopup.Show(this.Page);
                                FL = "Insert";//Dhanya Shetty
                                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Sales_Autho _" + SetNo.ToString() + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                Clear();
                                return;
                            }
                        
                    }
                    else if (ddlPayType.SelectedValue.ToString() == "3")
                    {
                            //Generate Normal set No here
                            SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();
                        DataTable dt =new DataTable();
                            //For Cash
                        dt = Cbr.GetProdCode(Session["BRCD"].ToString(), txtIssueId.Text);

                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToDouble(txtAmount.Text.ToString() == "" ? "0" : txtAmount.Text.ToString()) > 0)
                                {
                                    string cgl = BD.GetCashGl("99", Session["BRCD"].ToString());

                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", cgl, "0",
                                              "ISSUE ID " + ViewState["ISSUENO"].ToString() + "", "", txtAmount.Text.ToString(), "2", "3", "CR", SetNo, "", "", "0", "0", "1003",
                                              "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "By Cash", txtProdType1.Text.ToString(), TxtAccNo1.Text.ToString(),TxtAccName1.Text, "0", "0");
                                }
                              
                            }

                                    if (Session["BRCD"].ToString() != "1")
                                    {
                                        //Generate Normal set No here
                                        SetNo2 = BD.GetSetNo(Session["EntryDate"].ToString(), "IBTSetNo", Session["BRCD"].ToString()).ToString();

                                        if (resultint > 0)
                                        {

                                            //GlCode = Cbr.GetGlCode(Session["BRCD"].ToString());
                                            //    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString(), txtProdType1.Text.Trim().ToString(),
                                            //  TxtAccNo1.Text.Trim().ToString(), "ISSUE ID " + ViewState["ISSUENO"].ToString() + "", "" + ViewState["CustNo"].ToString() + "", txtAmount.Text.ToString(), "2", "7", "TR", SetNo, TxtChequeNo.Text.ToString() == "" ? "" : TxtChequeNo.Text.ToString(), TxtChequeDate.Text.ToString() == "" ? "" : TxtChequeDate.Text.ToString(), "0", "0", "1003",
                                            //  "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "By Cash", txtProdType1.Text.ToString(), TxtAccNo1.Text.ToString(), TxtAccName1.Text, "0", "0");
                                            //}

                                //                if (resultint > 0)
                                //                {
                                //                   DataTable dt1=new DataTable();
                                //                   dt1 = Cbr.GetADMSubGl("1");

                                //                     resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString(), txtProdType1.Text.Trim().ToString(),
                                //              TxtAccNo1.Text.Trim().ToString(), "ISSUE ID " + ViewState["ISSUENO"].ToString() + "", "" + ViewState["CustNo"].ToString() + "", txtAmount.Text.ToString(), "2", "7", "TR", SetNo, TxtChequeNo.Text.ToString() == "" ? "" : TxtChequeNo.Text.ToString(), TxtChequeDate.Text.ToString() == "" ? "" : TxtChequeDate.Text.ToString(), "0", "0", "1003",
                                //              "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "By Cash", txtProdType1.Text.ToString(), TxtAccNo1.Text.ToString(), TxtAccName1.Text, "0", "0");
                                //}

                                        }
                    
                        
                        else if (ddlPayType.SelectedValue.ToString() == "2")
                        {
                             
                            if (dt.Rows.Count > 0)
                            {
                                 if (Convert.ToDouble(txtAmount.Text.ToString() == "" ? "0" : txtAmount.Text.ToString()) > 0)
                                {    
                                    
                                }
                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString(), txtProdType1.Text.Trim().ToString(),
                                              TxtAccNo1.Text.Trim().ToString(), "ISSUE ID " + ViewState["ISSUENO"].ToString() + "", "By Trans " + ViewState["CustNo"].ToString() + "", txtAmount.Text.ToString(), "2", "7", "TR", SetNo, TxtChequeNo.Text.ToString() == "" ? "" : TxtChequeNo.Text.ToString(), TxtChequeDate.Text.ToString() == "" ? "" : TxtChequeDate.Text.ToString(), "0", "0", "1003",
                                              "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "By Trans", txtProdType1.Text.ToString(), TxtAccNo1.Text.ToString(), TxtAccName1.Text, "0", "0");
                                }

                               
                        }
                                        else if (ddlPayType.SelectedValue.ToString() == "4")
                                        {
                                            SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();

                                            //For Cheque
                                            dt = Cbr.GetProdCode(Session["BRCD"].ToString(), txtIssueId.Text);

                                            if (dt.Rows.Count > 0)
                                            {
                                                if (Convert.ToDouble(txtAmount.Text.ToString() == "" ? "0" : txtAmount.Text.ToString()) > 0)
                                                {
                                                    resultint = PVOUCHER.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString(), txtProdType1.Text.Trim().ToString(),
                                                         TxtAccNo1.Text.Trim().ToString(), "ISSUE ID " + ViewState["ISSUENO"].ToString() + "", "", txtAmount.Text.ToString(), "2", "5", "TR", SetNo, TxtChequeNo.Text.ToString() == "" ? "" : TxtChequeNo.Text.ToString(), TxtChequeDate.Text.ToString() == "" ? "" : TxtChequeDate.Text.ToString(), "0", "0", "1003",
                                                         "", Session["BRCD"].ToString(), Mid.ToString(), "0", Session["MID"].ToString(), "by Cheque", txtProdType1.Text.ToString(), TxtAccNo1.Text.ToString(), TxtAccName1.Text, "0", "0");


                                                }

                                            }
                                        }
                        if (resultint > 0)
                        {
                           
                                
                                    if (Session["BRCD"].ToString() == "1")
                                    {
                                        lblMessage.Text = "Authorise Successfully With SetNo : '" + SetNo.ToString() + "'";
                                        FL = "Insert";//Dhanya Shetty
                                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Share_Autho _" + SetNo.ToString() + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                    }
                                    else if (Session["BRCD"].ToString() != "1")
                                    {
                                        lblMessage.Text = "Authorise Successfully With SetNo : '" + SetNo.ToString() + "' And '" + SetNo2.ToString() + "'";
                                        FL = "Insert";//Dhanya Shetty
                                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Share_Autho _" + SetNo.ToString() + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                    }
                                    ViewState["SetNo"] = Convert.ToInt32(SetNo.ToString()).ToString();
                                   
                                    ModalPopup.Show(this.Page);
                                    Clear();
                                    SHOWGRID();
                                    return;
                                }
                           
                        
  
                else
                {
                    lblMessage.Text = "Application already authorise...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
            else
            {
                lblMessage.Text = "Maker Not Authorise...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
    }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    
}