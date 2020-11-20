using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class frmloanparameter : System.Web.UI.Page
{
    ClsBindDropdown DB = new ClsBindDropdown();
    ClsLoanpara cp = new ClsLoanpara();
    DbConnection conn = new DbConnection();
    string Flag;
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsDayClose dayObj = new ClsDayClose();
    string FLA = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            //    ViewState["Flag"] = "MD";


            DT = new DataTable();



            ViewState["Flag"] = Request.QueryString["Flag"].ToString();
            if (Convert.ToString(ViewState["Flag"]) == "AD")
            {

                txtproductcode.Text = cp.GETMAX(Session["BRCD"].ToString());
                txtintre.Text = cp.GETMAX11(Session["BRCD"].ToString());
                TXTPRODUCTNAME.Focus();
                btnSubmit.Visible = true;
                btnclear.Visible = true;
                txtproductcode.Enabled = false;
                txtintre.Enabled = false;
                btnSubmit.Text = "Submit";



            }
            if (Convert.ToString(ViewState["Flag"]) == "MD")
            {

                txtproductcode.Focus();
                btnSubmit.Visible = false;
                txtintre.Enabled = false;
                btnSubmit.Text = "Modify";

            }
            else if (Convert.ToString(ViewState["Flag"]) == "DL")
            {

                txtproductcode.Focus();
                btnclear.Visible = true;
                txtintre.Enabled = false;
                btnSubmit.Text = "Delete";

            }
            else if (Convert.ToString(ViewState["Flag"]) == "VW")
            {
                txtproductcode.Focus();
                BOOL();
                btnSubmit.Visible = false;
                btnclear.Visible = true;
                btnSubmit.Text = "View";
                //For Disbling all Controls
                btnclear.Visible = false;

                EnableDisable(true);


            }
        }

    }
    public void Autoid()
    {
        //int result = cp.autoid(Session["BRCD"].ToString());
        //txtproductcode.Text = result.ToString();
    }
    public void BOOL()
    {
        // txtproductcode.Enabled = false;
        TXTPRODUCTNAME.Enabled = false;
        txtoirgl.Enabled = false;
        txtloanlimit.Enabled = false;
        txtrateint.Enabled = false;

        txteffectivedate.Enabled = false;
        txtintre.Enabled = false;
        txtintname.Enabled = false;
    }
    protected void grdloanpara_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdloanpara.PageIndex = e.NewPageIndex;

    }

    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "M";
        btnSubmit.Text = "Modify";
        LinkButton lnkedit = (LinkButton)sender;

        if (!btnSubmit.Enabled)
        {
            btnSubmit.Enabled = true;
        }


    }
    protected void grdloanpara_SelectedIndexChanged(object sender, EventArgs e)
    {
        DATA();

    }
    protected void txtproductcode_TextChanged(object sender, EventArgs e)
    {
        DATA11();

    }


    public void clear()
    {
        txtproductcode.Text = "";
        TXTPRODUCTNAME.Text = "";
        txtoirgl.Text = "";
        txtloanlimit.Text = "";
        txtrateint.Text = "";
        txtIntCal.Text = "";
        txtIntApp.Text = "";
        txtSecured.Text = "";

        TXtpenInt.Text = "";
        txteffectivedate.Text = "";
        txtintre.Text = "";
        txtintname.Text = "";
        txtOtherCHarges.Text = "";
        txtperiod.Text = "";
        //txtproductcode.Text = "";
        //TXTPRODUCTNAME.Text = "";
        //txtoirgl.Text = "";
        //txtloanlimit.Text = "";
        //txtrateint.Text = "";

        //txteffectivedate.Text = "";
        //txtintre.Text = "";
        //txtintname.Text = "";
    }
    public void bindgrid()
    {
        cp.bindgrid(txtproductcode.Text, Session["BRCD"].ToString(), grdloanpara);
    }

    protected void TXTPRODUCTNAME_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click1(object sender, EventArgs e)
    {


        try
        {
            if (dayObj.CheckAdminAccess(Convert.ToString(Session["BRCD"]), Convert.ToString(Session["UGRP"])) == 1)
            {
                WebMsgBox.Show("User Is Restricted For This Activity.", this.Page);
                return;
            }
            if (ViewState["Flag"].ToString() == "AD")
            {

                int Result = cp.INSERTDATA(txtproductcode.Text, TXTPRODUCTNAME.Text, txtoirgl.Text, txtrateint.Text, txtperiod.Text, "100", txtintre.Text, txtOtherCHarges.Text, txtIntCal.Text, txtloanlimit.Text, TXtpenInt.Text, txteffectivedate.Text, Session["BRCD"].ToString(), txtIntApp.Text, txtSecured.Text);
                if (Result > 0)
                {
                    WebMsgBox.Show("Record Inserted Successfully...!!", this.Page);
                    FLA = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FLA, Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanParameterE_Add _" + txtproductcode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    clear();
                }
                else
                {
                    WebMsgBox.Show("Error Occured While Adding Records...!!", this.Page);
                    return;
                }
            }


            else if (ViewState["Flag"].ToString() == "MD")
            {
                int Result = cp.UPDATEdata(txtproductcode.Text, TXTPRODUCTNAME.Text, txtoirgl.Text, txtrateint.Text, txtperiod.Text, "100", txtintre.Text, txtOtherCHarges.Text, txtIntCal.Text, txtloanlimit.Text, TXtpenInt.Text, txteffectivedate.Text, Session["BRCD"].ToString(), txtIntApp.Text, txtSecured.Text);
                if (Result > 0)
                {
                    WebMsgBox.Show("Record Modified Successfully...!!", this.Page);
                    FLA = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FLA, Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanParameterE_Mod _" + txtproductcode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    clear();
                }
                else
                {
                    WebMsgBox.Show("Error Occured While Modifying Data..!", this.Page);
                    return;
                }

            }
            else if (ViewState["Flag"].ToString() == "DL")
            {

                int result = cp.DELETELOAN(SUGLCODE: txtproductcode.Text, BRCD: Convert.ToString(Session["BRCD"]));
                if (result > 0)
                {
                    WebMsgBox.Show("Record Deleted Successfully...!!", this.Page);
                    FLA = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FLA, Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanParameterE_Del _" + txtproductcode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    clear();
                }
                else
                {
                    WebMsgBox.Show("Error Occured While Deleting Data..!", this.Page);
                    return;
                }
            }

        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }



    }
    public void DATA11()
    {
        DataTable DT = new DataTable();
        DT = cp.showdata(txtproductcode.Text, Session["BRCD"].ToString());
        if (DT.Rows.Count > 0)
        {
            txtproductcode.Text = DT.Rows[0]["LOANGLCODE"].ToString();
            TXTPRODUCTNAME.Text = DT.Rows[0]["LOANTYPE"].ToString();
            txtoirgl.Text = DT.Rows[0]["LOANCATEGORY"].ToString();
            txtrateint.Text = DT.Rows[0]["ROI"].ToString();
            txtperiod.Text = DT.Rows[0]["PERIOD"].ToString();
            txtintre.Text = DT.Rows[0]["PPL"].ToString();
            txtintname.Text = DT.Rows[0]["PGL"].ToString();
            txtOtherCHarges.Text = DT.Rows[0]["OTHERCHG"].ToString();
            txtIntCal.Text = DT.Rows[0]["INTCALTYPE"].ToString();
            txtloanlimit.Text = DT.Rows[0]["LOANLIMIT"].ToString();
            TXtpenInt.Text = DT.Rows[0]["PENALINT"].ToString();
            txteffectivedate.Text = DT.Rows[0]["EFFECTIVEDATE"].ToString();
            txtIntApp.Text = DT.Rows[0]["Int_App"].ToString();
            txtSecured.Text = DT.Rows[0]["SECURED"].ToString();


        }
        else if (DT == null || DT.Rows.Count <= 0)
        {
            WebMsgBox.Show("No Records Found", this.Page);
            clear();
            return;
        }
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void txtoirgl_TextChanged(object sender, EventArgs e)
    {
        string aa = cp.getorgname(txtoirgl.Text, Session["BRCD"].ToString());

    }
    protected void txtplacc_TextChanged1(object sender, EventArgs e)
    {

    }
    protected void txtacchead_TextChanged(object sender, EventArgs e)
    {

    }
    protected void lnkedit_Click(object sender, EventArgs e)
    {
        LinkButton lnkedit = (LinkButton)sender;
        string str = lnkedit.CommandArgument.ToString();
        string[] ARR = str.Split(',');
        ViewState["LOANGLCODE"] = ARR[0].ToString();
        ViewState["intsub"] = ARR[1].ToString();

    }
    public void DATA()
    {
        DataTable DT = new DataTable();
        DT = cp.showdata(ViewState["LOANGLCODE"].ToString(), Session["BRCD"].ToString());
        if (DT.Rows.Count > 0)
        {

            txteffectivedate.Text = DT.Rows[0]["EFFECTIVEDATE"].ToString();
            txtintre.Text = DT.Rows[0]["INTSUB"].ToString();
            txtintname.Text = DT.Rows[0]["INTNAME"].ToString();



        }
    }

    protected void txtpenalint_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlIntCal_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Focus();
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        txteffectivedate.Focus();
    }
    protected void TBNAUTHORIZE_Click(object sender, EventArgs e)
    {

    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Flag"] = "AD";
            btnSubmit.Text = "Submit";
            txtproductcode.Text = cp.GETMAX(Session["BRCD"].ToString());
            txtintre.Text = cp.GETMAX11(Session["BRCD"].ToString());
            TXTPRODUCTNAME.Focus();
            btnSubmit.Visible = true;
            btnclear.Visible = true;
            txtproductcode.Enabled = false;
            txtintre.Enabled = false;
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
            clear();
            txtproductcode.Enabled = true;
            txtintre.Enabled = true;
            ViewState["Flag"] = "MD";
            btnSubmit.Text = "Modify";
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
            clear();
            txtproductcode.Enabled = true;
            txtintre.Enabled = true;
            ViewState["Flag"] = "DL";
            btnSubmit.Text = "Delete";
            btnSubmit.Enabled = true;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void lnkAuthorized_Click(object sender, EventArgs e)
    {
        try
        {
            clear();
            txtproductcode.Enabled = true;
            txtintre.Enabled = true;
            ViewState["Flag"] = "AT";
            btnSubmit.Text = "Authorise";

            if (!btnSubmit.Enabled)
            {
                btnSubmit.Enabled = true;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }



    private void EnableDisable(bool IsHide)
    {
        try
        {
            if (IsHide)
            {
                lnkAdd.Attributes.Remove("href");
                lnkAdd.Attributes.CssStyle[HtmlTextWriterStyle.Color] = "gray";
                lnkAdd.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";
                lnkAdd.Enabled = false;


                lnkModify.Attributes.Remove("href");
                lnkModify.Attributes.CssStyle[HtmlTextWriterStyle.Color] = "gray";
                lnkModify.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";
                lnkModify.Enabled = false;


                lnkDelete.Attributes.Remove("href");
                lnkDelete.Attributes.CssStyle[HtmlTextWriterStyle.Color] = "gray";
                lnkDelete.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";
                lnkDelete.Enabled = false;

                lnkAuthorized.Attributes.Remove("href");
                lnkAuthorized.Attributes.CssStyle[HtmlTextWriterStyle.Color] = "gray";
                lnkAuthorized.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";
                lnkAuthorized.EnableTheming = false;



            }


        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }


}