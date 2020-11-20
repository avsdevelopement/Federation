using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.Data;
using Oracle;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

public partial class FrmOtherRecovery : System.Web.UI.Page
{
    DbConnection Conn = new DbConnection();
    DataTable DT = new DataTable();
    DataTable dt1 = new DataTable();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCommon CMN = new ClsCommon();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsOthRecovery Rec = new ClsOthRecovery();
    CLsCustNoChanges CLS = new CLsCustNoChanges();

    int Res = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TxtFDate.Text = Session["EntryDate"].ToString();
            TxtBRCD.Text = Session["BRCD"].ToString();
            TxtBrname.Text = AST.GetBranchName(TxtBRCD.Text);
            BindGrid();
            BindProduct(ddlProductName);
            TxtAccType.Focus(); 
            ViewState["Flag"] = "AD";
        }
    }
    protected void BindProduct(DropDownList ddlAccType)
    {
        try
        {
            Rec.BindProductName(ddlAccType, null);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtBRCD_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtAccType_TextChanged(object sender, EventArgs e)
    {
        try
        {
                string[] TD = BD.GetLoanGL(TxtAccType.Text, Session["BRCD"].ToString()).Split('_');
                BindGrid();
                if (TD.Length > 1)
                {

                }
                txtcusno.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAccno_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtAccHName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Flag"].ToString() == "AD")
            {
                string GlS1 = Rec.CustNoOpenYN(TxtAccType.Text);
                Rec.CreateSharAccounts(Session["BRCD"].ToString(), txtcusno.Text.ToString(), TxtAccType.Text, TxtFDate.Text, Session["MID"].ToString());

                if (GlS1 != "N")
                {
                    Res = Rec.AddECS(Session["BRCD"].ToString(), TxtAccType.Text, txtcusno.Text, txtcusno.Text, TxtFDate.Text, DDLStatus.SelectedValue, TxtPrincipal.Text, TxtIntRate.Text, TxtPeriod.Text, TxtIntAmt.Text, TxtMaturityAmt.Text, TxtTDate.Text, Session["MID"].ToString());
                }
                else
                {
                    Res = Rec.AddECS(Session["BRCD"].ToString(), TxtAccType.Text, txtcusno.Text, TxtAccno.Text, TxtFDate.Text, DDLStatus.SelectedValue, TxtPrincipal.Text, TxtIntRate.Text, TxtPeriod.Text, TxtIntAmt.Text, TxtMaturityAmt.Text, TxtTDate.Text, Session["MID"].ToString());
                }

                if (Res > 0)
                {
                    WebMsgBox.Show("Deposit Created successfully", this.Page);
                    //BindGrid();
                    Clear();
                    btnSubmit.Enabled = false;
                    return;
                }
            }
            if (ViewState["Flag"].ToString() == "MD")
            {
                Res = Rec.UpdateECS(Session["BRCD"].ToString(), TxtAccType.Text, txtcusno.Text, TxtAccno.Text, TxtFDate.Text, DDLStatus.SelectedValue, TxtPrincipal.Text, TxtIntRate.Text, TxtPeriod.Text, TxtIntAmt.Text, TxtMaturityAmt.Text, TxtTDate.Text, Session["MID"].ToString());
                if (Res > 0)
                {
                    WebMsgBox.Show("Deposit Modified Successfully", this.Page);
                    Clear();
                    btnSubmit.Enabled = false;
                    return;
                }
            }
            if (ViewState["Flag"].ToString() == "AT")
            {
                Res = Rec.AuthoECS(Session["BRCD"].ToString(), TxtAccType.Text, TxtAccno.Text, Session["MID"].ToString());
                if (Res > 0)
                {
                    WebMsgBox.Show("Deposit Authorized Successfully", this.Page);
                    Clear();
                    btnSubmit.Enabled = false;
                    return;
                }
            }
            if (ViewState["Flag"].ToString() == "DL")
            {
                Res = Rec.AuthoDelete(Session["BRCD"].ToString(), TxtAccType.Text, TxtAccno.Text, Session["MID"].ToString());
                if (Res > 0)
                {
                    WebMsgBox.Show("Deposit Authorized Successfully", this.Page);
                    Clear();
                    btnSubmit.Enabled = false;
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtcusno_TextChanged(object sender, EventArgs e)
    {
        string AT = "";

        AT = CLS.Getstage1(txtcusno.Text, Session["BRCD"].ToString());
        if (AT != "1003")
        {
            WebMsgBox.Show ("Sorry Customer not Authorise.........!!", this.Page); 
            ModalPopup.Show(this.Page);
            txtcusno.Text = "";
            txtcusname.Text = "";
            txtcusno.Focus();
        }
        else
        {
            string CUSTNAME = CLS.GetCustNAme(txtcusno.Text, Session["BRCD"].ToString());
            BindGrid();
            if (CUSTNAME != null || CUSTNAME != "")
            {
                txtcusname.Text = CUSTNAME;
                TxtAccno.Text = txtcusno.Text;
                TxtFDate.Focus();
            }
            else
            {
                WebMsgBox.Show("Cust No Not Exit In Master   ...!!", this.Page); 
                ModalPopup.Show(this.Page);
                return;
            }
        }
    }
    protected void txtcusname_TextChanged(object sender, EventArgs e)
    {
       
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
    protected void CalculatedepositINT(float amt, string subgl, float intrate, int Period, string intpay, string PTYPE)
    {
        try
        {
            float interest = 0;
            float maturityamt = 0;

            //interest = Convert.ToInt32(((((amt) * (intrate) / 100)) / 12) * (Period));
            interest = Convert.ToInt32((((amt) * (intrate) * (Period) / 100)));
            maturityamt = (interest) + (amt * Period);
            TxtIntAmt.Text = interest.ToString("N");
            TxtMaturityAmt.Text = maturityamt.ToString("N");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void Clear()
    {
        TxtAccType.Text = "";
        txtcusno.Text = "";
        TxtAccno.Text = "";
        TxtFDate.Text = "";
        DDLStatus.SelectedValue = "";
        TxtPrincipal.Text = "";
        TxtIntRate.Text = "";
        TxtPeriod.Text = "";
        TxtMaturityAmt.Text = "";
        TxtTDate.Text = "";
        ddlProductName.SelectedValue = "";
        txtcusname.Text = "";
        TxtIntAmt.Text = "";
    }
    protected void TxtPeriod_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtPeriod.Text == "")
            {
                return;
            }

            if (TxtPrincipal.Text == "")
            {
                WebMsgBox.Show("Enter Deposit Amount", this.Page);
                TxtPrincipal.Focus();
                return;
            }

            //double rate = 0;

            //TxtIntRate.Text = rate.ToString();
            
            TxtTDate.Text = Conn.AddMonthDay(TxtFDate.Text, TxtPeriod.Text, "M").Replace("12:00:00", "");

            float amt = (float)Convert.ToDouble(TxtPrincipal.Text);
            float intrate = (float)Convert.ToDouble(TxtIntRate.Text);
            CalculatedepositINT(amt, TxtAccType.Text.ToString(), intrate, Convert.ToInt32(TxtPeriod.Text), "ON MATURITY", "M");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void grdMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdMaster.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindGrid()
    {
        try
        {
            int Result = 0;
            Result = Rec.GridBindData(grdMaster, Session["BRCD"].ToString(), TxtAccType.Text,txtcusno.Text);
            if (Result > 0)
            {
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["Depositglcode"] = ARR[0].ToString();
            ViewState["CustAccno"] = ARR[1].ToString();
            ViewState["Flag"] = "MD";
            if (Session["UGRP"].ToString() == "1" || Session["UGRP"].ToString() == "2" || Session["UGRP"].ToString() == "3")
            {
                btnSubmit.Text = "Modify";
                LtrlHeading.Text = "Deposit Modify";

                getalldata();
            }
            else
            {
                WebMsgBox.Show("User is restricted to Modify", this.Page);
            }
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
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["Depositglcode"] = ARR[0].ToString();
            ViewState["CustAccno"] = ARR[1].ToString();
            ViewState["Flag"] = "DL";
            btnSubmit.Text = "Delete";
            LtrlHeading.Text = "Deposit Delete";

            getalldata();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void lnkAuthorize_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            ViewState["Depositglcode"] = ARR[0].ToString();
            ViewState["CustAccno"] = ARR[1].ToString();
            ViewState["Flag"] = "AT";
            btnSubmit.Text = "Authorise";
            LtrlHeading.Text = "Deposit Authorize";
            getalldata();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void getalldata()
    {
        try
        {
            dt1 = Rec.GetAllFieldData(ViewState["Depositglcode"].ToString(), ViewState["CustAccno"].ToString(),Session["BRCD"].ToString());
            if (dt1.Rows.Count > 0)
            {
                string STAGE = "";
                STAGE = dt1.Rows[0]["STAGE"].ToString();
                if ((ViewState["Flag"].ToString() == "MD" || ViewState["Flag"].ToString() == "DL") && Session["UGRP"].ToString() != "1")
                {
                    if (STAGE == "1003")
                    {
                        Clear();
                        WebMsgBox.Show("Record Authorized cannot Modify....", this.Page);
                        ModalPopup.Show(this.Page);
                        return;
                    }
                    else if (STAGE == "1004")
                    {
                        Clear();
                        WebMsgBox.Show("Record Not Present", this.Page);
                        ModalPopup.Show(this.Page);
                        return;
                    }
                }
                TxtAccType.Text = ViewState["Depositglcode"].ToString();
                txtcusno.Text = dt1.Rows[0]["CUSTNO"].ToString();
                txtcusname.Text = dt1.Rows[0]["CUSTNAME"].ToString();
                TxtAccno.Text = dt1.Rows[0]["CUSTACCNO"].ToString();
                TxtFDate.Text = Convert.ToDateTime(dt1.Rows[0]["OPENINGDATE"].ToString()).ToString("dd/MM/yyyy");
                TxtPrincipal.Text = dt1.Rows[0]["PRNAMT"].ToString();
                TxtPeriod.Text = dt1.Rows[0]["PERIOD"].ToString();
                TxtIntRate.Text = dt1.Rows[0]["RATEOFINT"].ToString();
                TxtIntAmt.Text = Convert.ToInt32(dt1.Rows[0]["INTAMT"]).ToString();
                TxtMaturityAmt.Text = Convert.ToInt32(dt1.Rows[0]["MATURITYAMT"]).ToString();
                TxtTDate.Text = Convert.ToDateTime(dt1.Rows[0]["DUEDATE"].ToString()).ToString("dd/MM/yyyy");
                ddlProductName.SelectedItem.Text = Rec.GetProductName_1(ViewState["Depositglcode"].ToString());
                DDLStatus.SelectedValue = dt1.Rows[0]["LMSTATUS"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void ddlProductName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            TxtAccType.Text = "";
            TxtAccType.Text = ddlProductName.SelectedValue.ToString();
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}