using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using Microsoft.ReportingServices;
using System.Data.SqlClient;

public partial class FrmAVS5089 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAVS5089 AC = new ClsAVS5089();
    DataTable dtFirst = new DataTable();
    DataTable DT = new DataTable();
    string sResult = "";
    int Result = 0;

    #region Page Load
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtBrCode.Text = Session["BRCD"].ToString();
                AutoGlName.ContextKey = txtBrCode.Text.ToString();
                txtBrName.Text = AC.GetBranchName(txtBrCode.Text.ToString());
                txtFrDate.Text = Session["EntryDate"].ToString();
                txtToDate.Text = Session["EntryDate"].ToString();
                
                txtProdType.Focus();
            }
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Text Changed Event

    protected void txtBrCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtBrCode.Text.ToString() != "")
            {
                string BrName = AC.GetBranchName(txtBrCode.Text.ToString());
                if (BrName != null || BrName != "")
                {
                    ClearProdText();
                    AutoGlName.ContextKey = txtBrCode.Text.ToString();
                    txtBrName.Text = BrName;

                    btnSubmit.Visible = true;
                    btnChange.Visible = false;
                    txtProdType.Focus();
                }
                else
                {
                    ClearAllData();
                    WebMsgBox.Show("Enter valid branch code...!!", this.Page);
                    txtBrCode.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter branch code first...!!", this.Page);
                txtBrName.Text = "";
                txtBrCode.Text = "";
                txtBrCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtProdType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            sResult = AC.GetProduct(txtBrCode.Text.ToString(), txtProdType.Text.ToString());

            if (sResult != null)
            {
                if (BD.GetProdOperate(txtBrCode.Text.ToString(), txtProdType.Text.ToString()).ToString() != "3")
                {
                    string[] ACC = sResult.Split('_'); ;
                    ViewState["GlCode"] = ACC[0].ToString();
                    txtProdName.Text = ACC[2].ToString();
                    AutoAccname.ContextKey = txtBrCode.Text.ToString() + "_" + txtProdType.Text.ToString();

                    btnSubmit.Visible = true;
                    btnChange.Visible = false;
                    txtAccNo.Focus();
                }
                else
                {
                    ClearProdText();
                    lblMessage.Text = "Product is not operating...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
            else
            {
                ClearProdText();
                WebMsgBox.Show("Enter valid Product code!.....", this.Page);
                txtProdType.Focus();
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
            string[] custnob = txtProdName.Text.ToString().Split('_');
            if (custnob.Length > 1)
            {
                if (BD.GetProdOperate(txtBrCode.Text.ToString(), string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString()).ToString() != "3")
                {
                    txtProdName.Text = custnob[0].ToString();
                    ViewState["GlCode"] = custnob[1].ToString();
                    txtProdType.Text = (string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString());
                    AutoAccname.ContextKey = txtBrCode.Text.ToString() + "_" + txtProdType.Text.ToString();

                    btnSubmit.Visible = true;
                    btnChange.Visible = false;
                    txtAccNo.Focus();
                }
                else
                {
                    ClearProdText();
                    lblMessage.Text = "Product is not operating...!!";
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

    protected void txtAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DT = AC.GetAccStage(txtBrCode.Text.ToString(), txtProdType.Text.ToString(), txtAccNo.Text.ToString());
            if (DT.Rows.Count > 0)
            {
                if (DT.Rows[0]["Stage"].ToString() == "1003")
                {
                    if (DT.Rows[0]["Acc_Status"].ToString() != "3")
                    {
                        sResult = AC.GetCustName(txtBrCode.Text.ToString(), txtProdType.Text, txtAccNo.Text);
                        if (sResult.ToString() != "")
                        {
                            txtAccName.Text = sResult.ToString();
                            txtAccNo.Text = DT.Rows[0]["AccNo"].ToString();
                            txtCustNo.Text = DT.Rows[0]["CustNo"].ToString();

                            btnSubmit.Visible = true;
                            btnChange.Visible = false;
                            txtFrDate.Focus();
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Account is already closed...!!";
                        ModalPopup.Show(this.Page);
                        txtAccNo.Text = "";
                        txtAccName.Text = "";
                        txtAccNo.Focus();
                    }
                }
                else
                {
                    lblMessage.Text = "Sorry customer not authorise...!!";
                    ModalPopup.Show(this.Page);
                    txtAccNo.Text = "";
                    txtAccName.Text = "";
                    txtAccNo.Focus();
                }
            }
            else
            {
                lblMessage.Text = "Enter valid account number...!!";
                ModalPopup.Show(this.Page);
                txtAccNo.Text = "";
                txtAccNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] AccDet = txtAccName.Text.ToString().Split('_');
            if (AccDet.Length > 1)
            {
                DT = AC.GetAccStage(txtBrCode.Text.ToString(), txtProdType.Text.ToString(), (string.IsNullOrEmpty(AccDet[1].ToString()) ? "" : AccDet[1].ToString()));
                if (DT.Rows.Count > 0)
                {
                    if (DT.Rows[0]["Stage"].ToString() == "1003")
                    {
                        if (DT.Rows[0]["Acc_Status"].ToString() != "3")
                        {
                            sResult = AC.GetCustName(txtBrCode.Text.ToString(), txtProdType.Text, txtAccNo.Text);
                            if (sResult.ToString() != "")
                            {
                                txtAccName.Text = sResult.ToString();
                                txtAccNo.Text = DT.Rows[0]["AccNo"].ToString();
                                txtCustNo.Text = DT.Rows[0]["CustNo"].ToString();

                                btnSubmit.Visible = true;
                                btnChange.Visible = false;
                                txtFrDate.Focus();
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Account is already closed...!!";
                            ModalPopup.Show(this.Page);
                            txtAccNo.Text = "";
                            txtAccName.Text = "";
                            txtAccNo.Focus();
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Sorry customer not authorise...!!";
                        ModalPopup.Show(this.Page);
                        txtAccNo.Text = "";
                        txtAccName.Text = "";
                        txtAccNo.Focus();
                    }
                }
                else
                {
                    lblMessage.Text = "Enter valid account number...!!";
                    ModalPopup.Show(this.Page);
                    txtAccNo.Text = "";
                    txtAccNo.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtChangAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DT = AC.GetAccStage(txtBrCode.Text.ToString(), txtProdType.Text.ToString(), txtChangAccNo.Text.ToString());
            if (DT.Rows.Count > 0)
            {
                if (DT.Rows[0]["Stage"].ToString() == "1003")
                {
                    if (DT.Rows[0]["Acc_Status"].ToString() != "3")
                    {
                        sResult = AC.GetCustName(txtBrCode.Text.ToString(), txtProdType.Text.ToString(), txtChangAccNo.Text.ToString());
                        if (sResult.ToString() != "")
                        {
                            txtChangAccName.Text = sResult.ToString();
                            txtChangAccNo.Text = DT.Rows[0]["AccNo"].ToString();
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Account is already closed...!!";
                        ModalPopup.Show(this.Page);
                        txtChangAccNo.Text = "";
                        txtChangAccName.Text = "";
                        txtChangAccNo.Focus();
                    }
                }
                else
                {
                    lblMessage.Text = "Sorry customer not authorise...!!";
                    ModalPopup.Show(this.Page);
                    txtChangAccNo.Text = "";
                    txtChangAccName.Text = "";
                    txtChangAccNo.Focus();
                }
            }
            else
            {
                lblMessage.Text = "Enter valid account number...!!";
                ModalPopup.Show(this.Page);
                txtChangAccNo.Text = "";
                txtChangAccNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtChangAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] AccDet = txtChangAccName.Text.ToString().Split('_');
            if (AccDet.Length > 1)
            {
                DT = AC.GetAccStage(txtBrCode.Text.ToString(), txtProdType.Text.ToString(), (string.IsNullOrEmpty(AccDet[1].ToString()) ? "" : AccDet[1].ToString()));
                if (DT.Rows.Count > 0)
                {
                    if (DT.Rows[0]["Stage"].ToString() == "1003")
                    {
                        if (DT.Rows[0]["Acc_Status"].ToString() != "3")
                        {
                            sResult = AC.GetCustName(txtBrCode.Text.ToString(), txtProdType.Text.ToString(), txtChangAccNo.Text.ToString());
                            if (sResult.ToString() != "")
                            {
                                txtChangAccName.Text = sResult.ToString();
                                txtChangAccNo.Text = DT.Rows[0]["AccNo"].ToString();
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Account is already closed...!!";
                            ModalPopup.Show(this.Page);
                            txtChangAccNo.Text = "";
                            txtChangAccName.Text = "";
                            txtChangAccNo.Focus();
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Sorry customer not authorise...!!";
                        ModalPopup.Show(this.Page);
                        txtChangAccNo.Text = "";
                        txtChangAccName.Text = "";
                        txtChangAccNo.Focus();
                    }
                }
                else
                {
                    lblMessage.Text = "Enter valid account number...!!";
                    ModalPopup.Show(this.Page);
                    txtChangAccNo.Text = "";
                    txtChangAccNo.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Click Event
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            BindGridData();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        try
        {
            Result = AC.ChangeAccNo(txtBrCode.Text.ToString(), txtProdType.Text.ToString(), txtAccNo.Text.ToString(), txtChangAccNo.Text.ToString(), txtFrDate.Text.ToString(), txtToDate.Text.ToString(), Session["MID"].ToString());
            if (Result > 0)
            {
                ClearAllData();
                WebMsgBox.Show("Account number change successfully...!!", this.Page);
                return;
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
            ClearAllData();
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
            return;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Functions

    public void BindGridData()
    {
        try
        {
            DT = AC.BindGridData(txtBrCode.Text.ToString(), txtProdType.Text.ToString(), txtAccNo.Text.ToString(), txtFrDate.Text.ToString(), txtToDate.Text.ToString(), Session["MID"].ToString());
            if (DT.Rows.Count > 0)
            {
                grdTransaction.DataSource = DT;
                grdTransaction.DataBind();
                DivChangeAcc.Visible = true;
                btnSubmit.Visible = false;
                btnChange.Visible = true;
            }
            else
            {
                grdTransaction.DataSource = null;
                grdTransaction.DataBind();
                DivChangeAcc.Visible = false;
                btnSubmit.Visible = true;
                btnChange.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearProdText()
    {
        try
        {
            txtProdType.Text = "";
            txtProdName.Text = "";
            txtAccNo.Text = "";
            txtAccName.Text = "";
            txtFrDate.Text = Session["EntryDate"].ToString();
            txtToDate.Text = Session["EntryDate"].ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearAllData()
    {
        try
        {
            txtBrCode.Text = Session["BRCD"].ToString();
            AutoGlName.ContextKey = txtBrCode.Text.ToString();
            txtBrName.Text = AC.GetBranchName(txtBrCode.Text.ToString());
            txtProdType.Text = "";
            txtProdName.Text = "";
            txtAccNo.Text = "";
            txtAccName.Text = "";
            txtFrDate.Text = Session["EntryDate"].ToString();
            txtToDate.Text = Session["EntryDate"].ToString();
            
            DivChangeAcc.Visible = false;
            btnSubmit.Visible = true;
            btnChange.Visible = false;
            grdTransaction.DataSource = null;
            grdTransaction.DataBind();

            txtProdType.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

}