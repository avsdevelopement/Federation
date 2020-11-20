using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmChargesDebit : System.Web.UI.Page
{
    ClsChargesMaster CM = new ClsChargesMaster();
    ClsLoanInstallmen LI = new ClsLoanInstallmen();
    ClsInsertTrans ITrans = new ClsInsertTrans();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAuthorized AT = new ClsAuthorized();
    DataTable DT = new DataTable();
    DataTable dt = new DataTable();
    DataTable DT2 = new DataTable();
    DataTable DTName = new DataTable();
    int GlCode, SubGlCode, result, IntCalType = 0;
    string RefNumber, SetNo, SetNoD, Parti1 = "";
    Decimal Cr = 0, Dr = 0;
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    DbConnection conn = new DbConnection();
    string FL = "";

    #region PageLoad

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            BindDDL();
            ViewState["TrxType"] = "0";
            ViewState["VoucherNo"] = "0";
            ViewState["EntryDate"] = "0";
            AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtGl.Text;
            txtGl.Focus();
        }
    }

    #endregion

    #region Text Changed Event

    protected void txtGl_TextChanged(object sender, EventArgs e)
    {
        //Added By Amol on 22092017 as per ambika mam instruction
        if (BD.GetProdOperate(Session["BRCD"].ToString(), txtGl.Text.Trim().ToString()).ToString() != "3")
        {
            AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtGl.Text;

            dt = CM.FetchLGL(txtGl.Text, Session["BRCD"].ToString());

            if (dt.Rows.Count > 0)
            {
                txtGlName.Text = dt.Rows[0]["LOANTYPE"].ToString();

                txtAccNo.Focus();
                return;
            }
            else
            {
                ClearAll();
                txtGl.Focus();
                WebMsgBox.Show("Enter Valid Loan Gl Code ...!!", this.Page);
                return;
            }
        }
        else
        {
            ClearAll();
            txtGl.Text = "";
            txtGlName.Text = "";
            WebMsgBox.Show("Product is not operating ...!!", this.Page);
            return;
        }
    }

    protected void txtAccNo_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            DTName = CM.FectAccName(Session["BRCD"].ToString(), txtAccNo.Text.ToString(), txtGl.Text.ToString());
            if (DTName.Rows.Count > 0)
            {
                txtAccName.Text = DTName.Rows[0]["CustName"].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtGl.Text.ToString();

                dt = CM.CheckAccNo(txtAccNo.Text.ToString(), Session["BRCD"].ToString());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Acc_Status"].ToString() != "3")
                    {
                        ViewState["CustNo"] = dt.Rows[0]["CustNo"].ToString();
                        dt = CM.GetBal(txtAccNo.Text.ToString(), Session["BRCD"].ToString(), txtGl.Text.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            grdBal.DataSource = dt;
                            grdBal.DataBind();
                        }
                        else
                        {
                            grdBal.DataSource = null;
                            grdBal.DataBind();
                        }

                        if (rbtnEntryType.SelectedValue == "C")
                            ddlCharges1.Focus();
                        else
                            ddlActivity.Focus();
                    }
                    else
                    {
                        ClearAll();
                        txtAccNo.Focus();
                        WebMsgBox.Show("Account no is closed ...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    ClearAll();
                    txtAccNo.Focus();
                    WebMsgBox.Show("Account no is invalid ...!!", this.Page);
                    return;
                }
            }
            else
            {
                ClearAll();
                txtAccNo.Focus();
                WebMsgBox.Show("Account no is invalid ...!!", this.Page);
                return;
            }
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
    }

    protected void txtAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] custnob = txtAccName.Text.ToString().Split('_');
            if (custnob.Length > 1)
            {
                txtAccNo.Text = custnob[1].ToString();
                DTName = CM.FectAccName(Session["BRCD"].ToString(), txtAccNo.Text.ToString(), txtGl.Text.ToString());
                if (DTName.Rows.Count > 0)
                {
                    txtAccName.Text = DTName.Rows[0]["CustName"].ToString();
                    AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtGl.Text.ToString();

                    dt = CM.CheckAccNo(txtAccNo.Text.ToString(), Session["BRCD"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["Acc_Status"].ToString() != "3")
                        {
                            ViewState["CustNo"] = dt.Rows[0]["CustNo"].ToString();
                            dt = CM.GetBal(txtAccNo.Text.ToString(), Session["BRCD"].ToString(), txtGl.Text.ToString());
                            if (dt.Rows.Count > 0)
                            {
                                grdBal.DataSource = dt;
                                grdBal.DataBind();
                            }
                            else
                            {
                                grdBal.DataSource = null;
                                grdBal.DataBind();
                            }

                            if (rbtnEntryType.SelectedValue == "C")
                                ddlCharges1.Focus();
                            else
                                ddlActivity.Focus();
                        }
                        else
                        {
                            ClearAll();
                            txtAccNo.Focus();
                            WebMsgBox.Show("Account no is closed ...!!", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        ClearAll();
                        txtAccNo.Focus();
                        WebMsgBox.Show("Account no is invalid ...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    ClearAll();
                    txtAccNo.Focus();
                    WebMsgBox.Show("Account no is invalid ...!!", this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtProdType1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;

            //Added By Amol on 22092017 as per ambika mam instruction
            if (BD.GetProdOperate(Session["BRCD"].ToString(), txtProdType1.Text.Trim().ToString()).ToString() != "3")
            {
                AC1 = CM.Getaccno(txtProdType1.Text, Session["BRCD"].ToString());

                if (AC1 != null)
                {
                    string[] AC = AC1.Split('_'); ;
                    ViewState["GLCODE1"] = AC[0].ToString();
                    txtProdName1.Text = AC[1].ToString();
                    AutoAccname1.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType1.Text + "_" + ViewState["GLCODE1"].ToString();

                    if (Convert.ToInt32(ViewState["GLCODE1"].ToString() == "" ? "0" : ViewState["GLCODE1"].ToString()) >= 100)
                    {
                        TxtAccNo1.Text = "";
                        TxtAccName1.Text = "";

                        TxtAccNo1.Text = txtProdType1.Text.ToString();
                        TxtAccName1.Text = txtProdName1.Text.ToString();

                        txtNarration.Focus();
                    }
                    else
                    {
                        TxtAccNo1.Text = "";
                        TxtAccName1.Text = "";

                        TxtAccNo1.Focus();
                    }
                }
                else
                {
                    WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                    txtProdType1.Text = "";
                    txtProdName1.Text = "";
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";
                    txtProdType1.Focus();
                }
            }
            else
            {
                txtProdType1.Text = "";
                txtProdName1.Text = "";
                txtProdType1.Focus();
                WebMsgBox.Show("Product is not operating ...!!", this.Page);
                return;
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
                //Added By Amol on 22092017 as per ambika mam instruction
                if (BD.GetProdOperate(Session["BRCD"].ToString(), string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString()).ToString() != "3")
                {
                    txtProdName1.Text = custnob[0].ToString();
                    txtProdType1.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());

                    string[] AC = CM.Getaccno(txtProdType1.Text, Session["BRCD"].ToString()).Split('_');
                    ViewState["GLCODE1"] = AC[0].ToString();
                    AutoAccname1.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType1.Text + "_" + ViewState["GLCODE1"].ToString();

                    if (Convert.ToInt32(ViewState["GLCODE1"].ToString() == "" ? "0" : ViewState["GLCODE1"].ToString()) >= 100)
                    {
                        TxtAccNo1.Text = "";
                        TxtAccName1.Text = "";

                        TxtAccNo1.Text = txtProdType1.Text.ToString();
                        TxtAccName1.Text = txtProdName1.Text.ToString();

                        txtNarration.Focus();
                    }
                    else
                    {
                        TxtAccNo1.Text = "";
                        TxtAccName1.Text = "";

                        TxtAccNo1.Focus();
                    }
                }
                else
                {
                    txtProdType1.Text = "";
                    txtProdName1.Text = "";
                    txtProdType1.Focus();
                    WebMsgBox.Show("Product is not operating ...!!", this.Page);
                    return;
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
            AT = BD.Getstage1(TxtAccNo1.Text, Session["BRCD"].ToString(), txtProdType1.Text);
            if (AT != null)
            {
                if (AT != "1003")
                {
                    TxtAccNo1.Text = "";
                    TxtAccName1.Text = "";
                    TxtAccNo1.Focus();
                    WebMsgBox.Show("Sorry Customer not Authorise ...!!", this.Page);
                    return;
                }
                else
                {
                    DataTable DT = new DataTable();
                    DT = CM.GetCustName(txtProdType1.Text, TxtAccNo1.Text, Session["BRCD"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                        TxtAccName1.Text = CustName[0].ToString();

                        txtNarration.Focus();
                    }
                }
            }
            else
            {
                TxtAccNo1.Text = "";
                TxtAccName1.Text = "";
                TxtAccNo1.Focus();
                WebMsgBox.Show("Enter valid account number ...!!", this.Page);
                return;
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

                txtNarration.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Select Index Changed event

    protected void rbtnEntryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtnEntryType.SelectedValue == "C")
            {
                ClearAll();
                divActivity.Visible = false;
                divVoucher.Visible = false;
                divCancel.Visible = true;
                btnInsert.Visible = false;
                btnUpdate.Visible = true;
                btnCancel.Visible = true;

                grdBal.DataSource = null;
                grdBal.DataBind();
            }
            else if (rbtnEntryType.SelectedValue == "R" && ddlActivity.SelectedValue == "1")
            {
                ClearAll();
                divActivity.Visible = true;
                divVoucher.Visible = true;
                divCancel.Visible = false;
                ddlVoucher.Enabled = false;
                btnInsert.Visible = true;
                btnUpdate.Visible = false;
                btnCancel.Visible = false;

                grdBal.DataSource = null;
                grdBal.DataBind();
            }
            else
            {
                ClearAll();
                divActivity.Visible = true;
                divVoucher.Visible = true;
                divCancel.Visible = false;
                ddlVoucher.Enabled = true;
                btnInsert.Visible = true;
                btnUpdate.Visible = false;
                btnCancel.Visible = false;

                grdBal.DataSource = null;
                grdBal.DataBind();
            }

            txtGl.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlActivity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtnEntryType.SelectedValue == "R" && ddlActivity.SelectedValue == "1")
                ddlVoucher.Enabled = false;
            else
                ddlVoucher.Enabled = true;

            Charges();
            ddlCharges.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlCharges_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Charges();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlCharges1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Charges1();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlVoucher_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVoucher.SelectedValue.ToString() == "0")
        {
            Transfer.Visible = false;
            DivAmount.Visible = false;
        }
        else if (ddlVoucher.SelectedValue.ToString() == "1")
        {
            Transfer.Visible = false;
            DivAmount.Visible = true;
            txtNarration.Text = "By Cash";

            Clear();
            txtCrAmount.Text = txtAmount.Text.Trim().ToString() == "" ? "0" : txtAmount.Text.Trim().ToString();
            txtNarration.Focus();
        }
        else if (ddlVoucher.SelectedValue.ToString() == "2")
        {
            Transfer.Visible = true;
            DivAmount.Visible = true;
            txtNarration.Text = "By TRF";
            autoglname1.ContextKey = Session["BRCD"].ToString();

            Clear();
            txtCrAmount.Text = txtAmount.Text.Trim().ToString() == "" ? "0" : txtAmount.Text.Trim().ToString();
            txtProdType1.Focus();
        }
        else
        {
            Clear();
            Transfer.Visible = false;
        }
    }

    protected void grdBal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string TRX = ((Label)e.Row.FindControl("lblTrxType")).Text;
            if (TRX == "1")
            {
                Cr += Convert.ToDecimal(((Label)e.Row.FindControl("lblCredit")).Text);
                Dr += 0;
                ((Label)e.Row.FindControl("lblCredit")).Visible = true;
                ((Label)e.Row.FindControl("lblDebit")).Visible = false;
            }
            else
            {
                Cr += 0;
                Dr += Convert.ToDecimal(((Label)e.Row.FindControl("lblDebit")).Text);
                ((Label)e.Row.FindControl("lblCredit")).Visible = false;
                ((Label)e.Row.FindControl("lblDebit")).Visible = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            ((Label)e.Row.FindControl("lblCrTotal")).Text = Cr.ToString();
            ((Label)e.Row.FindControl("lblDrTotal")).Text = Dr.ToString();
        }
    }

    #endregion

    #region Button Click Event

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtGl.Text.Trim().ToString() == "")
            {
                txtGl.Focus();
                WebMsgBox.Show("Enter product code first ...!!", this.Page);
                return;
            }
            else if (txtAccNo.Text.Trim().ToString() == "")
            {
                txtAccNo.Focus();
                WebMsgBox.Show("Enter account number first ...!!", this.Page);
                return;
            }
            else if (ddlActivity.SelectedValue != "0")
            {
                if (rbtnEntryType.SelectedValue == "R" && ddlActivity.SelectedValue == "1")
                {
                    if (ddlCharges.SelectedValue == "0")
                    {
                        ddlCharges.Focus();
                        WebMsgBox.Show("Select head type first ...!!", this.Page);
                        return;
                    }
                    else
                    {
                        if (Convert.ToDouble(txtAmount.Text.ToString() == "" ? "0" : txtAmount.Text.ToString()) > 0)
                        {
                            DataTable dt = new DataTable();
                            DataTable DT1 = new DataTable();
                            dt = CM.GetGlCode(ddlCharges.SelectedValue);
                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToInt32(ddlCharges.SelectedValue) == 1 || Convert.ToInt32(ddlCharges.SelectedValue) == 2 || Convert.ToInt32(ddlCharges.SelectedValue) == 3 || Convert.ToInt32(ddlCharges.SelectedValue) == 4)
                                {
                                    DT1 = CM.GetGlCode(txtGl.Text.Trim().ToString(), Session["BRCD"].ToString());
                                    if (DT1.Rows.Count > 0)
                                    {
                                        if (Convert.ToInt32(ddlCharges.SelectedValue) == 1)
                                            GlCode = Convert.ToInt32(DT1.Rows[0]["SUBGLCODE"]);
                                        if (Convert.ToInt32(ddlCharges.SelectedValue) == 2 || Convert.ToInt32(ddlCharges.SelectedValue) == 4)
                                            GlCode = Convert.ToInt32(DT1.Rows[0]["IR"]);
                                        else if (Convert.ToInt32(ddlCharges.SelectedValue) == 3)
                                            GlCode = Convert.ToInt32(DT1.Rows[0]["IOR"]);
                                    }
                                }
                                else
                                    GlCode = Convert.ToInt32(dt.Rows[0]["SubGlCode"]);
                            }

                            //As per Ambika mam's instruction 
                            SetNoD = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString());//Dhanya Shetty to get setno if chargestype=receivable and activity=charges//15/03/2018
                            result = CM.InsertCharges(txtAccNo.Text.Trim().ToString(), txtGl.Text.Trim().ToString(), GlCode.ToString(), ddlCharges.SelectedValue, Session["BRCD"].ToString(), txtNarration.Text, txtAmount.Text.Trim().ToString(), Session["MID"].ToString(), conn.ConvertDate(Session["EntryDate"].ToString()), "1", SetNoD);
                            if (result > 0)
                            {
                                WebMsgBox.Show("Record Inserted Sucessfully with setno : " + SetNoD.ToString(), this.Page);
                                CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Charges_Debit _" + txtAccNo.Text + "_" + txtGl.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                ClearAll();
                                return;
                            }
                        }
                        else
                        {
                            ddlVoucher.Focus();
                            WebMsgBox.Show("Enter amount greather than zero ...!!", this.Page);
                            return;
                        }
                    }
                }
                else
                {
                    if (ddlCharges.SelectedValue == "0")
                    {
                        ddlCharges.Focus();
                        WebMsgBox.Show("Select head type first ...!!", this.Page);
                        return;
                    }
                    else
                    {
                        if ((Convert.ToDouble(txtAmount.Text.ToString() == "" ? "0" : txtAmount.Text.ToString()) > 0) && (Convert.ToDouble(ViewState["VoucherNo"].ToString()) > 0))
                        {
                            if ((txtProdType1.Text.Trim().ToString() == "") && (ddlVoucher.SelectedValue.ToString() == "2"))
                            {
                                txtProdType1.Focus();
                                WebMsgBox.Show("Enter product code first ...!!", this.Page);
                                return;
                            }
                            else if ((TxtAccNo1.Text.Trim().ToString() == "") && (ddlVoucher.SelectedValue.ToString() == "2"))
                            {
                                TxtAccNo1.Focus();
                                WebMsgBox.Show("Enter Account number first ...!!", this.Page);
                                return;
                            }
                            else
                            {
                                if (ddlVoucher.SelectedValue.ToString() != "0")
                                {
                                    //Generate Set Number Here For Net paid
                                    SetNo = "";
                                    RefNumber = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                                    ViewState["RID"] = (Convert.ToInt32(RefNumber) + 1).ToString();
                                    SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString());

                                    if (Convert.ToDouble(txtAmount.Text.Trim().ToString()) > 0)
                                    {
                                        if (ddlVoucher.SelectedValue.ToString() == "1")
                                        {
                                            if (ddlCharges.SelectedValue.ToString() == "1")
                                            {
                                                Parti1 = "Princ Reverse Date - " + ViewState["EntryDate"].ToString() + " Set - " + ViewState["VoucherNo"].ToString();

                                                //Insert Data to Original Table (Avsm) Here
                                                result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "3", txtGl.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "2", "3", "CR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");

                                                if (result > 0)
                                                {
                                                    result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", "99", txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "1", "3", "CR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                }
                                            }
                                            else if (ddlCharges.SelectedValue.ToString() == "2")
                                            {
                                                DT = new DataTable();
                                                DT = CM.GetGlSubCode(Session["BRCD"].ToString(), txtGl.Text.Trim().ToString());
                                                IntCalType = CM.GetIntCal(Session["BRCD"].ToString(), txtGl.Text.Trim().ToString());
                                                Parti1 = "Int Reverse Date - " + ViewState["EntryDate"].ToString() + " Set - " + ViewState["VoucherNo"].ToString();

                                                if (DT.Rows.Count > 0)
                                                {
                                                    if (IntCalType == 1)
                                                    {
                                                        //Insert Data to Original Table (Avsm) Here
                                                        result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GlCode"].ToString(), DT.Rows[0]["SUBGLCODE"].ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "2", "3", "CR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");

                                                        if (result > 0)
                                                        {
                                                            result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GlCode"].ToString(), DT.Rows[0]["SUBGLCODE"].ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "1", "11", "TR_INT", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                        }
                                                        if (result > 0)
                                                        {
                                                            result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", DT.Rows[0]["PPL"].ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "2", "11", "TR_INT", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                        }
                                                        if (result > 0)
                                                        {
                                                            result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", "99", txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "1", "3", "CR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                        }

                                                        if (result > 0)
                                                        {
                                                            //Insert Data to Original Table (Avs_LnTrx) Here
                                                            result = ITrans.LoanTrx(Session["BRCD"].ToString(), txtGl.Text.Trim().ToString(), DT.Rows[0]["SUBGLCODE"].ToString(), txtAccNo.Text.Trim().ToString(), "2", "2", "3", "Interest Debited", txtAmount.Text.Trim().ToString(), SetNo.ToString(), "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), "0");
                                                        }
                                                    }
                                                    else if (IntCalType == 2)
                                                    {
                                                        //Insert Data to Original Table (Avsm) Here
                                                        result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "3", txtGl.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "2", "3", "CR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");

                                                        if (result > 0)
                                                        {
                                                            result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "3", txtGl.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "1", "11", "TR_INT", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                        }
                                                        if (result > 0)
                                                        {
                                                            result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", DT.Rows[0]["PPL"].ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "2", "11", "TR_INT", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                        }
                                                        if (result > 0)
                                                        {
                                                            result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", "99", txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "1", "3", "CR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                        }
                                                    }
                                                    else if (IntCalType == 3)
                                                    {
                                                        //Insert Data to Original Table (Avsm) Here
                                                        result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GlCode"].ToString(), DT.Rows[0]["SUBGLCODE"].ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "2", "3", "CR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");

                                                        if (result > 0)
                                                        {
                                                            result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", "99", txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "1", "3", "CR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                        }

                                                        if (result > 0)
                                                        {
                                                            //Insert Data to Original Table (Avs_LnTrx) Here
                                                            result = ITrans.LoanTrx(Session["BRCD"].ToString(), txtGl.Text.Trim().ToString(), DT.Rows[0]["SUBGLCODE"].ToString(), txtAccNo.Text.Trim().ToString(), "2", "2", "3", "Interest Debited", txtAmount.Text.Trim().ToString(), SetNo.ToString(), "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), "0");
                                                        }
                                                    }
                                                }
                                            }
                                            else if (ddlCharges.SelectedValue.ToString() == "3")
                                            {
                                                DT = new DataTable();
                                                DT = CM.GetGlCode(txtGl.Text.ToString().ToString(), Session["BRCD"].ToString());
                                                Parti1 = "Int Rec Reverse Date - " + ViewState["EntryDate"].ToString() + " Set - " + ViewState["VoucherNo"].ToString();
                                                SubGlCode = Convert.ToInt32(DT.Rows[0]["IOR"].ToString());

                                                DT2 = new DataTable();
                                                DT2 = CM.GetGlCode(DT.Rows[0]["IOR"].ToString(), Session["BRCD"].ToString());

                                                if (DT2.Rows.Count > 0)
                                                {
                                                    GlCode = Convert.ToInt32(DT2.Rows[0]["GlCode"].ToString());

                                                    //Insert Data to Original Table (Avsm) Here
                                                    result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode.ToString(), SubGlCode.ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "2", "3", "CR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");

                                                    if (result > 0)
                                                    {
                                                        result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", "99", txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "1", "3", "CR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                    }

                                                    if (result > 0)
                                                    {
                                                        //Insert Data to Original Table (Avs_LnTrx) Here
                                                        result = ITrans.LoanTrx(Session["BRCD"].ToString(), txtGl.Text.Trim().ToString(), SubGlCode.ToString(), txtAccNo.Text.Trim().ToString(), ddlCharges.SelectedValue.ToString(), "2", "3", "Debited Reverse", txtAmount.Text.Trim().ToString(), SetNo.ToString(), "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), "0");
                                                    }
                                                }
                                            }
                                            else if (ddlCharges.SelectedValue.ToString() == "4")
                                            {
                                                DT = new DataTable();
                                                DT = CM.GetGlSubCode(Session["BRCD"].ToString(), txtGl.Text.Trim().ToString());
                                                IntCalType = CM.GetIntCal(Session["BRCD"].ToString(), txtGl.Text.Trim().ToString());
                                                Parti1 = "Int Rec Reverse Date - " + ViewState["EntryDate"].ToString() + " Set - " + ViewState["VoucherNo"].ToString();

                                                if (IntCalType == 1 || IntCalType == 3)
                                                {
                                                    //Insert Data to Original Table (Avsm) Here
                                                    result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GlCode"].ToString(), DT.Rows[0]["SUBGLCODE"].ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "2", "3", "CR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");

                                                    if (result > 0)
                                                    {
                                                        result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", "99", txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "1", "3", "CR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                    }

                                                    if (result > 0)
                                                    {
                                                        //Insert Data to Original Table (Avs_LnTrx) Here
                                                        result = ITrans.LoanTrx(Session["BRCD"].ToString(), txtGl.Text.Trim().ToString(), DT.Rows[0]["SUBGLCODE"].ToString(), txtAccNo.Text.Trim().ToString(), "4", "2", "3", "Interest Debited", txtAmount.Text.Trim().ToString(), SetNo.ToString(), "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), "0");
                                                    }
                                                }
                                                else if (IntCalType == 2)
                                                {
                                                    //Insert Data to Original Table (Avsm) Here
                                                    result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "3", txtGl.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "2", "3", "CR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");

                                                    if (result > 0)
                                                    {
                                                        result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", "99", txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "1", "3", "CR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                    }
                                                }
                                            }
                                            else if (Convert.ToInt32(ddlCharges.SelectedValue.ToString()) >= 5)
                                            {
                                                DT = new DataTable();
                                                DT = CM.GetGlCode(ddlCharges.SelectedValue.ToString());
                                                Parti1 = "Int Rec Reverse Date - " + ViewState["EntryDate"].ToString() + " Set - " + ViewState["VoucherNo"].ToString();

                                                DT2 = new DataTable();
                                                DT2 = CM.GetGlCode(DT.Rows[0]["SUBGLCODE"].ToString(), Session["BRCD"].ToString());

                                                if (DT2.Rows.Count > 0)
                                                {
                                                    GlCode = Convert.ToInt32(DT2.Rows[0]["GlCode"].ToString());
                                                    SubGlCode = Convert.ToInt32(DT2.Rows[0]["SubGlCode"].ToString());

                                                    //Insert Data to Original Table (Avsm) Here
                                                    result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode.ToString(), SubGlCode.ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "2", "3", "CR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");

                                                    if (result > 0)
                                                    {
                                                        result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "99", "99", txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "1", "3", "CR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                    }

                                                    if (result > 0)
                                                    {
                                                        //Insert Data to Original Table (Avs_LnTrx) Here
                                                        result = ITrans.LoanTrx(Session["BRCD"].ToString(), txtGl.Text.Trim().ToString(), SubGlCode.ToString(), txtAccNo.Text.Trim().ToString(), ddlCharges.SelectedValue.ToString(), "2", "3", "Debited Reverse", txtAmount.Text.Trim().ToString(), SetNo.ToString(), "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), "0");
                                                    }
                                                }
                                            }
                                        }
                                        else if (ddlVoucher.SelectedValue.ToString() == "2")
                                        {
                                            TxtAccNo1.Text = TxtAccNo1.Text.ToString() == "" ? "0" : TxtAccNo1.Text.ToString();

                                            if (ddlCharges.SelectedValue.ToString() == "1")
                                            {
                                                Parti1 = "Princ Reverse Date - " + ViewState["EntryDate"].ToString() + " Set - " + ViewState["VoucherNo"].ToString();

                                                //Insert Data to Original Table (Avsm) Here
                                                result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "3", txtGl.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "2", "7", "TR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");

                                                if (result > 0)
                                                {
                                                    result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE1"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "1", "7", "TR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                }
                                            }
                                            else if (ddlCharges.SelectedValue.ToString() == "2")
                                            {
                                                DT = new DataTable();
                                                DT = CM.GetGlSubCode(Session["BRCD"].ToString(), txtGl.Text.Trim().ToString());
                                                IntCalType = CM.GetIntCal(Session["BRCD"].ToString(), txtGl.Text.Trim().ToString());
                                                Parti1 = "Int Reverse Date - " + ViewState["EntryDate"].ToString() + " Set - " + ViewState["VoucherNo"].ToString();

                                                if (DT.Rows.Count > 0)
                                                {
                                                    if (IntCalType == 1)
                                                    {
                                                        //Insert Data to Original Table (Avsm) Here
                                                        result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GlCode"].ToString(), DT.Rows[0]["SUBGLCODE"].ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "2", "7", "TR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");

                                                        if (result > 0)
                                                        {
                                                            result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GlCode"].ToString(), DT.Rows[0]["SUBGLCODE"].ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "1", "11", "TR_INT", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                        }
                                                        if (result > 0)
                                                        {
                                                            result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", DT.Rows[0]["PPL"].ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "2", "11", "TR_INT", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                        }
                                                        if (result > 0)
                                                        {
                                                            result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE1"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "1", "7", "TR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                        }

                                                        if (result > 0)
                                                        {
                                                            //Insert Data to Original Table (Avs_LnTrx) Here
                                                            result = ITrans.LoanTrx(Session["BRCD"].ToString(), txtGl.Text.Trim().ToString(), DT.Rows[0]["SUBGLCODE"].ToString(), txtAccNo.Text.Trim().ToString(), "2", "2", "7", "Interest Debited", txtAmount.Text.Trim().ToString(), SetNo.ToString(), "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), "0");
                                                        }
                                                    }
                                                    else if (IntCalType == 2)
                                                    {
                                                        //Insert Data to Original Table (Avsm) Here
                                                        result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "3", txtGl.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "2", "7", "TR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");

                                                        if (result > 0)
                                                        {
                                                            result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "3", txtGl.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "1", "11", "TR_INT", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                        }
                                                        if (result > 0)
                                                        {
                                                            result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "100", DT.Rows[0]["PPL"].ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "2", "11", "TR_INT", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                        }
                                                        if (result > 0)
                                                        {
                                                            result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE1"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "1", "7", "TR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                        }
                                                    }
                                                    else if (IntCalType == 3)
                                                    {
                                                        //Insert Data to Original Table (Avsm) Here
                                                        result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GlCode"].ToString(), DT.Rows[0]["SUBGLCODE"].ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "2", "7", "TR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");

                                                        if (result > 0)
                                                        {
                                                            result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE1"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "1", "7", "TR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                        }

                                                        if (result > 0)
                                                        {
                                                            //Insert Data to Original Table (Avs_LnTrx) Here
                                                            result = ITrans.LoanTrx(Session["BRCD"].ToString(), txtGl.Text.Trim().ToString(), DT.Rows[0]["SUBGLCODE"].ToString(), txtAccNo.Text.Trim().ToString(), "2", "2", "7", "Interest Debited", txtAmount.Text.Trim().ToString(), SetNo.ToString(), "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), "0");
                                                        }
                                                    }
                                                }
                                            }
                                            else if (ddlCharges.SelectedValue.ToString() == "3")
                                            {
                                                DT = new DataTable();
                                                DT = CM.GetGlCode(txtGl.Text.ToString().ToString(), Session["BRCD"].ToString());
                                                Parti1 = "Int Rec Reverse Date - " + ViewState["EntryDate"].ToString() + " Set - " + ViewState["VoucherNo"].ToString();
                                                SubGlCode = Convert.ToInt32(DT.Rows[0]["IOR"].ToString());

                                                DT2 = new DataTable();
                                                DT2 = CM.GetGlCode(DT.Rows[0]["IOR"].ToString(), Session["BRCD"].ToString());

                                                if (DT2.Rows.Count > 0)
                                                {
                                                    GlCode = Convert.ToInt32(DT2.Rows[0]["GlCode"].ToString());

                                                    //Insert Data to Original Table (Avsm) Here
                                                    result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode.ToString(), SubGlCode.ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "2", "7", "TR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");

                                                    if (result > 0)
                                                    {
                                                        result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), txtProdType1.Text.ToString(), TxtAccNo1.Text.ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "1", "7", "TR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                    }

                                                    if (result > 0)
                                                    {
                                                        //Insert Data to Original Table (Avs_LnTrx) Here
                                                        result = ITrans.LoanTrx(Session["BRCD"].ToString(), txtGl.Text.Trim().ToString(), SubGlCode.ToString(), txtAccNo.Text.Trim().ToString(), ddlCharges.SelectedValue.ToString(), "2", "7", "Debited Reverse", txtAmount.Text.Trim().ToString(), SetNo.ToString(), "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), "0");
                                                    }
                                                }
                                            }
                                            else if (ddlCharges.SelectedValue.ToString() == "4")
                                            {
                                                DT = new DataTable();
                                                DT = CM.GetGlSubCode(Session["BRCD"].ToString(), txtGl.Text.Trim().ToString());
                                                IntCalType = CM.GetIntCal(Session["BRCD"].ToString(), txtGl.Text.Trim().ToString());
                                                Parti1 = "Int Rec Reverse Date - " + ViewState["EntryDate"].ToString() + " Set - " + ViewState["VoucherNo"].ToString();

                                                if (IntCalType == 1 || IntCalType == 3)
                                                {
                                                    //Insert Data to Original Table (Avsm) Here
                                                    result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["GlCode"].ToString(), DT.Rows[0]["SUBGLCODE"].ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "2", "7", "TR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");

                                                    if (result > 0)
                                                    {
                                                        result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE1"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "1", "7", "TR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                    }

                                                    if (result > 0)
                                                    {
                                                        //Insert Data to Original Table (Avs_LnTrx) Here
                                                        result = ITrans.LoanTrx(Session["BRCD"].ToString(), txtGl.Text.Trim().ToString(), DT.Rows[0]["SUBGLCODE"].ToString(), txtAccNo.Text.Trim().ToString(), "4", "2", "7", "Interest Debited", txtAmount.Text.Trim().ToString(), SetNo.ToString(), "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), "0");
                                                    }
                                                }
                                                else if (IntCalType == 2)
                                                {
                                                    //Insert Data to Original Table (Avsm) Here
                                                    result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), "3", txtGl.Text.Trim().ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "2", "7", "TR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");

                                                    if (result > 0)
                                                    {
                                                        result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE1"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "1", "7", "TR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                    }
                                                }
                                            }
                                            else if (Convert.ToInt32(ddlCharges.SelectedValue.ToString()) >= 5)
                                            {
                                                DT = new DataTable();
                                                DT = CM.GetGlCode(ddlCharges.SelectedValue.ToString());
                                                Parti1 = "Int Rec Reverse Date - " + ViewState["EntryDate"].ToString() + " Set - " + ViewState["VoucherNo"].ToString();

                                                DT2 = new DataTable();
                                                DT2 = CM.GetGlCode(DT.Rows[0]["SUBGLCODE"].ToString(), Session["BRCD"].ToString());

                                                if (DT2.Rows.Count > 0)
                                                {
                                                    GlCode = Convert.ToInt32(DT2.Rows[0]["GlCode"].ToString());
                                                    SubGlCode = Convert.ToInt32(DT2.Rows[0]["SubGlCode"].ToString());

                                                    //Insert Data to Original Table (Avsm) Here
                                                    result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), GlCode.ToString(), SubGlCode.ToString(), txtAccNo.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "2", "7", "TR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");

                                                    if (result > 0)
                                                    {
                                                        result = AT.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GLCODE1"].ToString(), txtProdType1.Text.Trim().ToString(), TxtAccNo1.Text.Trim().ToString(), Parti1.ToString(), txtNarration.Text.ToString(), txtAmount.Text.Trim().ToString(), "1", "7", "TR", SetNo.ToString(), "0", "", "0", "0", "1001", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "Reverse", ViewState["CustNo"].ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString(), "0");
                                                    }

                                                    if (result > 0)
                                                    {
                                                        //Insert Data to Original Table (Avs_LnTrx) Here
                                                        result = ITrans.LoanTrx(Session["BRCD"].ToString(), txtGl.Text.Trim().ToString(), SubGlCode.ToString(), txtAccNo.Text.Trim().ToString(), ddlCharges.SelectedValue.ToString(), "2", "7", "Debited Reverse", txtAmount.Text.Trim().ToString(), SetNo.ToString(), "1001", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), "0");
                                                    }
                                                }
                                            }
                                        }

                                        if (result > 0)
                                        {
                                            ViewState["TrxType"] = "0";
                                            ViewState["VoucherNo"] = "0";
                                            ViewState["EntryDate"] = "0";
                                            WebMsgBox.Show("Successfully Paid With SetNo : " + SetNo.ToString(), this.Page);
                                            CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Charges_Debit _" + SetNo + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                                            ClearAll();
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        ddlVoucher.Focus();
                                        WebMsgBox.Show("Enter amount greather than zero ...!!", this.Page);
                                        return;
                                    }
                                }
                                else
                                {
                                    ddlVoucher.Focus();
                                    WebMsgBox.Show("Select Proper Payment Mode First ...!!", this.Page);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            txtAmount.Focus();
                            WebMsgBox.Show("Enter amount greter than zero or select voucher ...!!", this.Page);
                            return;
                        }
                    }
                }
            }
            else
            {
                ddlActivity.Focus();
                WebMsgBox.Show("Select activity type first ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string[] ID = objlink.CommandArgument.Split('_');

            if (rbtnEntryType.SelectedValue.ToString() == "C")
            {
                DT = CM.GetSetAmount(Session["BRCD"].ToString(), txtGl.Text.ToString(), txtAccNo.Text.ToString(), ID[1].ToString(), ID[2].ToString(), ddlCharges1.SelectedValue.ToString(), ID[0].ToString());
                if (DT.Rows.Count > 0)
                {
                    ViewState["TrxType"] = ID[0].ToString();
                    ViewState["VoucherNo"] = ID[1].ToString();
                    ViewState["EntryDate"] = ID[2].ToString();
                    txtAmount1.Text = Convert.ToDouble(DT.Rows[0][0].ToString()).ToString();
                    txtAmount1.Focus();
                    return;
                }
            }
            else
            {
                if (ID[0].ToString() == "1")
                {
                    DT = CM.GetSetAmount(Session["BRCD"].ToString(), txtGl.Text.ToString(), txtAccNo.Text.ToString(), ID[1].ToString(), ID[2].ToString(), ddlCharges.SelectedValue.ToString(), ID[0].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        ViewState["TrxType"] = ID[0].ToString();
                        ViewState["VoucherNo"] = ID[1].ToString();
                        ViewState["EntryDate"] = ID[2].ToString();
                        txtAmount.Text = Convert.ToDouble(DT.Rows[0][0].ToString()).ToString();
                        txtCrAmount.Text = Convert.ToDouble(DT.Rows[0][0].ToString()).ToString();
                        txtAmount.Focus();
                        return;
                    }
                    else
                    {
                        txtAmount.Text = "";
                        txtAmount.Focus();
                        return;
                    }
                }
                else
                {
                    txtAmount.Text = "";
                    txtAmount.Focus();
                    WebMsgBox.Show("Not allow for debit transactions ...!!", this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("FrmChargesDebit.aspx");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtGl.Text.Trim().ToString() == "")
            {
                txtGl.Focus();
                WebMsgBox.Show("Enter product code first ...!!", this.Page);
                return;
            }
            else if (txtAccNo.Text.Trim().ToString() == "")
            {
                txtAccNo.Focus();
                WebMsgBox.Show("Enter account number first ...!!", this.Page);
                return;
            }
            else if (ddlCharges1.SelectedValue == "0")
            {
                ddlCharges1.Focus();
                WebMsgBox.Show("Select head type first ...!!", this.Page);
                return;
            }
            else
            {
                if ((Convert.ToDouble(txtAmount1.Text.ToString() == "" ? "0" : txtAmount1.Text.ToString()) > 0) && (Convert.ToDouble(ViewState["VoucherNo"].ToString()) > 0))
                {
                    result = CM.DeleteRecord(Session["BRCD"].ToString(), txtGl.Text.ToString(), txtAccNo.Text.ToString(), ViewState["VoucherNo"].ToString(),
                            ViewState["EntryDate"].ToString(), ddlCharges1.SelectedValue, ViewState["TrxType"].ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());
                    if (result > 0)
                    {
                        Charges1();
                        ViewState["TrxType"] = "0";
                        ViewState["VoucherNo"] = "0";
                        ViewState["EntryDate"] = "0";
                        WebMsgBox.Show("Sucessfully canceled ...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    txtAmount1.Focus();
                    WebMsgBox.Show("Enter amount greter than zero or select voucher ...!!", this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtGl.Text.Trim().ToString() == "")
            {
                txtGl.Focus();
                WebMsgBox.Show("Enter product code first ...!!", this.Page);
                return;
            }
            else if (txtAccNo.Text.Trim().ToString() == "")
            {
                txtAccNo.Focus();
                WebMsgBox.Show("Enter account number first ...!!", this.Page);
                return;
            }
            else if (ddlCharges1.SelectedValue == "0")
            {
                ddlCharges1.Focus();
                WebMsgBox.Show("Select head type first ...!!", this.Page);
                return;
            }
            else
            {
                if ((Convert.ToDouble(txtAmount1.Text.ToString() == "" ? "0" : txtAmount1.Text.ToString()) > 0) && (Convert.ToDouble(ViewState["VoucherNo"].ToString()) > 0))
                {
                    result = CM.UpdateRecord(Session["BRCD"].ToString(), txtGl.Text.ToString(), txtAccNo.Text.ToString(), ViewState["VoucherNo"].ToString(), ViewState["EntryDate"].ToString(),
                                ddlCharges1.SelectedValue, ViewState["TrxType"].ToString(), txtAmount1.Text.ToString(), Session["MID"].ToString(), Session["EntryDate"].ToString());
                    if (result > 0)
                    {
                        Charges1();
                        ViewState["TrxType"] = "0";
                        ViewState["VoucherNo"] = "0";
                        ViewState["EntryDate"] = "0";
                        WebMsgBox.Show("Sucessfully updated ...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    txtAmount1.Focus();
                    WebMsgBox.Show("Enter amount greter than zero or select voucher ...!!", this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAll();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Public Function

    public void CallEdit()
    {
        try
        {
            DT = CM.GetChargesDebOut(ViewState["Id"].ToString());
            if (DT.Rows.Count > 0)
            {
                txtGl.Text = DT.Rows[0]["LoanGlCode"].ToString();
                dt = CM.FetchLGL(txtGl.Text, Session["BRCD"].ToString());
                if (dt.Rows.Count > 0)
                {
                    txtGlName.Text = dt.Rows[0]["LOANTYPE"].ToString();
                }
                txtAccNo.Text = DT.Rows[0]["AccountNo"].ToString();
                DTName = CM.FectAccName(Session["BRCD"].ToString(), txtAccNo.Text, txtGl.Text);
                if (DTName.Rows.Count > 0)
                {
                    txtAccName.Text = DTName.Rows[0]["CustName"].ToString();
                }
                txtAmount.Text = DT.Rows[0]["Amount"].ToString();
                ddlCharges.SelectedValue = DT.Rows[0]["HeadDesc"].ToString();
                ddlActivity.SelectedValue = "1";
                ddlVoucher.Enabled = false;
            }
        }
        catch (Exception EX)
        {
            ExceptionLogging.SendErrorToText(EX);
        }
    }

    public void Charges()
    {
        try
        {
            int Charges = 0;
            DataTable dt = new DataTable();
            dt = CM.GetGlCode(ddlCharges.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(ddlCharges.SelectedValue) == 2 || Convert.ToInt32(ddlCharges.SelectedValue) == 3 || Convert.ToInt32(ddlCharges.SelectedValue) == 4)
                {
                    dt = CM.GetGlCode(txtGl.Text, Session["BRCD"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(ddlCharges.SelectedValue) == 2 || Convert.ToInt32(ddlCharges.SelectedValue) == 4)
                            Charges = Convert.ToInt32(dt.Rows[0]["IR"]);
                        else if (Convert.ToInt32(ddlCharges.SelectedValue) == 3)
                            Charges = Convert.ToInt32(dt.Rows[0]["IOR"]);
                    }
                }
                else
                    Charges = Convert.ToInt32(ddlCharges.SelectedValue);
            }
            if (rbtnEntryType.SelectedValue == "R" && ddlActivity.SelectedValue == "1")
            {
                grdBal.DataSource = null;
                grdBal.DataBind();
            }
            else
            {
                dt = CM.GetPertiBal(txtAccNo.Text, Session["BRCD"].ToString(), txtGl.Text, ddlCharges.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    grdBal.DataSource = dt;
                    grdBal.DataBind();
                }
                else
                {
                    grdBal.DataSource = null;
                    grdBal.DataBind();
                }
            }

            txtCrAmount.Text = "";
            txtAmount.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void Charges1()
    {
        try
        {
            int Charges = 0;
            DataTable dt = new DataTable();
            dt = CM.GetGlCode(ddlCharges1.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(ddlCharges1.SelectedValue) == 2 || Convert.ToInt32(ddlCharges1.SelectedValue) == 3 || Convert.ToInt32(ddlCharges1.SelectedValue) == 4)
                {
                    dt = CM.GetGlCode(txtGl.Text.ToString(), Session["BRCD"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(ddlCharges1.SelectedValue) == 2 || Convert.ToInt32(ddlCharges1.SelectedValue) == 4)
                            Charges = Convert.ToInt32(dt.Rows[0]["IR"]);
                        else if (Convert.ToInt32(ddlCharges1.SelectedValue) == 3)
                            Charges = Convert.ToInt32(dt.Rows[0]["IOR"]);
                    }
                }
                else
                    Charges = Convert.ToInt32(ddlCharges1.SelectedValue);
            }

            if (rbtnEntryType.SelectedValue == "C")
            {
                dt = CM.GetPertiBal(txtAccNo.Text.ToString(), Session["BRCD"].ToString(), txtGl.Text, ddlCharges1.SelectedValue);
                if (dt.Rows.Count > 0)
                {
                    grdBal.DataSource = dt;
                    grdBal.DataBind();
                }
                else
                {
                    grdBal.DataSource = null;
                    grdBal.DataBind();
                }
            }

            txtCrAmount.Text = "";
            txtAmount1.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindDDL()
    {
        try
        {
            CM.BindCharges(Session["UGRP"].ToString(), ddlCharges);
            CM.BindCharges(Session["UGRP"].ToString(), ddlCharges1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Clear()
    {
        try
        {
            txtProdType1.Text = "";
            txtProdName1.Text = "";
            TxtAccNo1.Text = "";
            TxtAccName1.Text = "";
            txtCrAmount.Text = "";
            txtProdType1.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        finally
        {
            txtProdType1.Focus();
        }

    }

    public void ClearAll()
    {
        try
        {
            Clear();
            txtGl.Text = "";
            txtGlName.Text = "";
            txtAccName.Text = "";
            txtAccNo.Text = "";
            txtAmount.Text = "";
            txtAmount1.Text = "";
            ddlActivity.SelectedValue = "0";
            ddlCharges.SelectedValue = "0";
            ddlCharges1.SelectedValue = "0";
            ddlVoucher.SelectedValue = "0";
            Transfer.Visible = false;
            DivAmount.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

}