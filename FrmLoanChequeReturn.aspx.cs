using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmLoanChequeReturn : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLoanChequeReturn CR = new ClsLoanChequeReturn();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string EntryMid = "";
    string sQuery = "";
    int intresult;

    #region PageLoad

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            if (!IsPostBack)
            {
                divInst.Visible = false;
                AutoLoanGlname.ContextKey = Session["BRCD"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Link Button Click

    protected void lnkCreate_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAll();
            lblActivity.Text = "Create";
            txtChequeDate.Text = Session["EntryDate"].ToString();

            divInst.Visible = false;
            DivReturn.Visible = true;
            btnSubmit.Visible = true;
            btnCreate.Visible = false;
            btnAuthorise.Visible = false;
            btnCancel.Visible = false;
            btnReport.Visible = false;
            DivReport.Visible = false;

            txtLoanProdType.Focus();
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
            ClearAll();
            lblActivity.Text = "Authorise";
            txtChequeDate.Text = Session["EntryDate"].ToString();

            divInst.Visible = false;
            DivReturn.Visible = true;
            btnSubmit.Visible = true;
            btnCreate.Visible = false;
            btnAuthorise.Visible = false;
            btnCancel.Visible = false;
            btnReport.Visible = false;
            DivReport.Visible = false;

            txtLoanProdType.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAll();
            lblActivity.Text = "Cancel";
            txtChequeDate.Text = Session["EntryDate"].ToString();

            divInst.Visible = false;
            DivReturn.Visible = true;
            btnSubmit.Visible = true;
            btnCreate.Visible = false;
            btnAuthorise.Visible = false;
            btnCancel.Visible = false;
            btnReport.Visible = false;
            DivReport.Visible = false;

            txtLoanProdType.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkReport_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAll();
            lblActivity.Text = "Report";
            txtChequeDate.Text = Session["EntryDate"].ToString();

            divInst.Visible = false;
            DivReturn.Visible = false;
            btnSubmit.Visible = false;
            btnCreate.Visible = false;
            btnAuthorise.Visible = false;
            btnCancel.Visible = false;
            DivReport.Visible = true;
            btnReport.Visible = true;

            txtFBranch.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region TextChange Event

    protected void txtLoanProdType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (lblActivity.Text != "")
            {
                btnCreate.Visible = false;

                //Added By Amol on 22092017 as per ambika mam instruction
                if (BD.GetProdOperate(Session["BRCD"].ToString(), txtLoanProdType.Text.Trim().ToString()).ToString() != "3")
                {
                    sQuery = CR.GetLoanAccNo(txtLoanProdType.Text, Session["BRCD"].ToString());

                    if (sQuery != null)
                    {
                        string[] AC = sQuery.Split('_'); ;
                        ViewState["LoanGlCode"] = AC[0].ToString();
                        txtLoanProdName.Text = AC[1].ToString();
                        AutoLoanAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtLoanProdType.Text + "_" + ViewState["LoanGlCode"].ToString();

                        ClearProdDetails();
                        txtLoanAccNo.Focus();
                        return;
                    }
                    else
                    {
                        WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                        ClearAll();
                        txtLoanProdType.Focus();
                        return;
                    }
                }
                else
                {
                    txtLoanProdType.Text = "";
                    txtLoanProdName.Text = "";
                    lblMessage.Text = "Product is not operating...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
            else
            {
                ClearAll();
                lnkCreate.Focus();
                lblMessage.Text = "Select Activity First...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtLoanProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            btnCreate.Visible = false;
            string CUNAME = txtLoanProdName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                //Added By Amol on 22092017 as per ambika mam instruction
                if (BD.GetProdOperate(Session["BRCD"].ToString(), string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString()).ToString() != "3")
                {
                    txtLoanProdName.Text = custnob[0].ToString();
                    txtLoanProdType.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                    string[] AC = CR.GetLoanAccNo(txtLoanProdType.Text, Session["BRCD"].ToString()).Split('_');
                    ViewState["LoanGlCode"] = AC[0].ToString();
                    AutoLoanAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtLoanProdType.Text + "_" + ViewState["LoanGlCode"].ToString();

                    ClearProdDetails();
                    txtLoanAccNo.Focus();
                    return;
                }
                else
                {
                    txtLoanProdType.Text = "";
                    txtLoanProdName.Text = "";
                    lblMessage.Text = "Product is not operating...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
            else
            {
                ClearAll();
                txtLoanProdType.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtLoanAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            btnCreate.Visible = false;
            sQuery = CR.Getstage1(Session["BRCD"].ToString(), txtLoanProdType.Text, txtLoanAccNo.Text);
            if (sQuery != null)
            {
                if (sQuery != "1003")
                {
                    lblMessage.Text = "Sorry customer not authorise...!!";
                    ModalPopup.Show(this.Page);

                    ClearProdDetails();
                    txtLoanAccNo.Focus();
                    return;
                }
                else
                {
                    DataTable DT = new DataTable();
                    DT = CR.GetCustName(txtLoanProdType.Text, txtLoanAccNo.Text, Session["BRCD"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                        txtLoanAccName.Text = CustName[0].ToString();
                        txtLoanCustNo.Text = CustName[1].ToString();

                        txtEntryDate.Focus();
                        return;
                    }
                }
            }
            else
            {
                lblMessage.Text = "Enter valid account number...!!";
                ModalPopup.Show(this.Page);

                ClearProdDetails();
                txtLoanAccNo.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtLoanAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            btnCreate.Visible = false;
            string CUNAME = txtLoanAccName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtLoanAccName.Text = custnob[0].ToString();
                txtLoanAccNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                txtLoanCustNo.Text = (string.IsNullOrEmpty(custnob[2].ToString()) ? "0" : custnob[2].ToString());

                txtEntryDate.Focus();
            }
            else
            {
                lblMessage.Text = "Enter valid account number...!!";
                ModalPopup.Show(this.Page);

                ClearProdDetails();
                txtLoanAccNo.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Button Click

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DT = CR.GetTransaction(Session["BRCD"].ToString(), txtLoanProdType.Text.Trim().ToString(), txtLoanAccNo.Text.Trim().ToString(), txtChequeNo.Text.Trim().ToString(), txtChequeDate.Text.Trim().ToString(), txtEntryDate.Text.Trim().ToString());

            if (DT.Rows.Count > 0)
            {
                if (lblActivity.Text.ToString() == "Create")
                {
                    btnCreate.Visible = true;
                    divInst.Visible = true;
                }
                else if (lblActivity.Text.ToString() == "Authorise")
                {
                    btnAuthorise.Visible = true;
                    divInst.Visible = true;
                }
                else if (lblActivity.Text.ToString() == "Cancel")
                {
                    btnCancel.Visible = true;
                    divInst.Visible = true;
                }
                DivGrd1.Visible = true;
                grdLoanDDS.DataSource = DT;
                grdLoanDDS.DataBind();
            }
            else
            {
                DivGrd1.Visible = false;
                lblMessage.Text = "No record found for cheque no " + txtChequeNo.Text.Trim().ToString() + " and cheque date " + txtChequeDate.Text.Trim().ToString() + "";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            sQuery = CR.CreateNewSet(Session["BRCD"].ToString(), txtLoanProdType.Text.Trim().ToString(), txtLoanAccNo.Text.Trim().ToString(), txtChequeNo.Text.ToString(), txtChequeDate.Text.ToString(), txtEntryDate.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

            if (Convert.ToInt32(sQuery) > 0)
            {
                btnCreate.Visible = false;
                grdLoanDDS.DataSource = null;
                grdLoanDDS.DataBind();
                lblMessage.Text = "Successfully Return with Set No : " + sQuery + "";
                ModalPopup.Show(this.Page);
                ClearAll();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnAuthorise_Click(object sender, EventArgs e)
    {
        try
        {
            EntryMid = CR.GetEntryMid(Session["BRCD"].ToString(), txtLoanProdType.Text.Trim().ToString(), txtLoanAccNo.Text.Trim().ToString(), txtChequeNo.Text.ToString(), txtChequeDate.Text.ToString(), txtEntryDate.Text.ToString());

            if (EntryMid.ToString() != Session["Mid"].ToString())
            {
                intresult = CR.AuthoriseNewSet(Session["BRCD"].ToString(), txtLoanProdType.Text.Trim().ToString(), txtLoanAccNo.Text.Trim().ToString(), txtChequeNo.Text.ToString(), txtChequeDate.Text.ToString(), txtEntryDate.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                if (Convert.ToInt32(intresult) > 0)
                {
                    btnAuthorise.Visible = false;
                    grdLoanDDS.DataSource = null;
                    grdLoanDDS.DataBind();
                    lblMessage.Text = "Voucher Authorise Successfully...!!";
                    ModalPopup.Show(this.Page);
                    ClearAll();
                    return;
                }
            }
            else
            {
                lblMessage.Text = "Not allow for same user...!!";
                ModalPopup.Show(this.Page);
                return;
            }
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
            EntryMid = CR.GetEntryMid(Session["BRCD"].ToString(), txtLoanProdType.Text.Trim().ToString(), txtLoanAccNo.Text.Trim().ToString(), txtChequeNo.Text.ToString(), txtChequeDate.Text.ToString(), txtEntryDate.Text.ToString());

            if (EntryMid.ToString() != Session["Mid"].ToString())
            {
                intresult = CR.DeleteNewSet(Session["BRCD"].ToString(), txtLoanProdType.Text.Trim().ToString(), txtLoanAccNo.Text.Trim().ToString(), txtChequeNo.Text.ToString(), txtChequeDate.Text.ToString(), txtEntryDate.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());

                if (Convert.ToInt32(intresult) > 0)
                {
                    btnCancel.Visible = false;
                    grdLoanDDS.DataSource = null;
                    grdLoanDDS.DataBind();
                    lblMessage.Text = "Voucher Canceled Successfully...!!";
                    ModalPopup.Show(this.Page);
                    ClearAll();
                    return;
                }
            }
            else
            {
                lblMessage.Text = "Not allow for same user...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {

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

    #region Functions

    protected void ClearProdDetails()
    {
        try
        {
            txtLoanAccNo.Text = "";
            txtLoanAccName.Text = "";
            txtLoanCustNo.Text = "";

            txtChequeDate.Text = "";
            txtChequeNo.Text = "";
            txtEntryDate.Text = "";

            txtLoanProdType.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        finally
        {
            txtLoanProdType.Focus();
        }
    }

    protected void ClearAll()
    {
        try
        {
            txtLoanProdType.Text = "";
            txtLoanProdName.Text = "";
            txtLoanAccNo.Text = "";
            txtLoanAccName.Text = "";
            txtLoanCustNo.Text = "";

            txtChequeDate.Text = "";
            txtChequeNo.Text = "";
            txtEntryDate.Text = "";

            divInst.Visible = false;
            txtLoanProdType.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        finally
        {
            txtLoanProdType.Focus();
        }
    }

    #endregion

}