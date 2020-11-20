﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class FrmAVS5077 : System.Web.UI.Page
{
    ClsLoanRepaymentCerti LR = new ClsLoanRepaymentCerti();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    scustom customcs = new scustom();
    DataTable DT = new DataTable();
    ClsBindDropdown BD = new ClsBindDropdown();
    string FL = "", Principle, Interest, CurrInteresr;
    ClsLoanRecovery LRA = new ClsLoanRecovery();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }
                autoglname.ContextKey = Session["BRCD"].ToString();
                Autoaccname4.ContextKey = Session["BRCD"].ToString() + "_" + txtProdCode.Text;
                txtProdCode.Focus();

            }
            catch (Exception Ex)
            {

                ExceptionLogging.SendErrorToText(Ex);
            }

        }

    }
    protected void txtProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = BD.GetAccTypeGL(txtProdCode.Text, Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');
            if (GLCODE[1] == "3")
            {
                ViewState["DRGL"] = GL[1].ToString();
                Autoaccname4.ContextKey = Session["BRCD"].ToString() + "_" + txtProdCode.Text + "_" + ViewState["DRGL"].ToString();
                string PDName = customcs.GetProductName(txtProdCode.Text, Session["BRCD"].ToString());
                if (PDName != null)
                {
                    txtProdName.Text = PDName;
                    txtaccno.Focus();
                }
                else
                {
                    WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                    txtProdCode.Text = "";
                    txtProdCode.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter only Loan Product Code....!", this.Page);
                txtProdCode.Text = "";
                txtProdCode.Focus();

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = txtProdName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtProdName.Text = CT[0].ToString();
                txtProdCode.Text = CT[1].ToString();
                txtaccno.Focus();
                string[] GLS = BD.GetAccTypeGL(txtProdCode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                Autoaccname4.ContextKey = Session["BRCD"].ToString() + "_" + txtProdCode.Text + "_" + ViewState["DRGL"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtaccno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT = "";
            string AC_Status = LR.GetAccStatus(Session["BRCD"].ToString(), txtProdCode.Text, txtaccno.Text);

            AT = LR.Getstage1(txtaccno.Text, Session["BRCD"].ToString(), txtProdCode.Text);
            if (AT != null)
            {
                if (AT != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise.........!!";
                    ModalPopup.Show(this.Page);
                    txtaccno.Text = "";
                    txtaccname.Text = "";
                    txtaccno.Focus();
                }
                else
                {
                    DataTable DT = new DataTable();
                    DT = LR.GetCustName(txtProdCode.Text, txtaccno.Text, Session["BRCD"].ToString());

                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                        txtaccname.Text = CustName[0].ToString();
                    }
                   

                }
            }
            else
            {
                lblMessage.Text = "Enter valid account number...!!";
                ModalPopup.Show(this.Page);
                txtaccno.Text = "";
                txtaccno.Focus();
            }

           
        }

        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtaccname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtaccname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtaccname.Text = custnob[0].ToString();
                txtaccno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
            }
            else
            {
                lblMessage.Text = "Invalid Account Number...!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtProdCode.Text != "" && txtaccno.Text != "")
            {
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanBalCertificate _Rpt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());


                string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&LOANGL=" + txtProdCode.Text + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + txtaccno.Text + "&rptname=RptAVS5077.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
                

        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }   
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx.aspx", true);
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    public void ClearData()
    {
        txtProdCode.Text = "";
        txtProdName.Text = "";
        txtaccno.Text = "";
        txtaccname.Text = "";
    }
}