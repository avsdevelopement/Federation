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

public partial class FrmAVS5109 : System.Web.UI.Page
{
    ClsInsertTrans ITrans = new ClsInsertTrans(); 
    ClsBindDropdown BD = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    ClsAVS5109 IO = new ClsAVS5109();
    DataTable DT = new DataTable();
    string sResult = "", RefNumber = "", SetNo = "";
    int Result = 0;

    #region Page Load
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AutoGlName.ContextKey = Session["BRCD"].ToString();
                AutoPayGlName.ContextKey = Session["BRCD"].ToString();
                txtFDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
                txtTDate.Text = Session["EntryDate"].ToString();
                divTransaction.Visible = false;

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

    #region Text Changed

    protected void txtProdType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            sResult = IO.GetProduct(Session["BRCD"].ToString(), txtProdType.Text.ToString());
            if (sResult != null)
            {
                if (BD.GetProdOperate(Session["BRCD"].ToString(), txtProdType.Text.ToString()).ToString() != "3")
                {
                    string[] ACC = sResult.Split('_'); ;
                    ViewState["GlCode"] = ACC[0].ToString();
                    txtProdName.Text = ACC[2].ToString();
                    AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType.Text.ToString();

                    btnSubmit.Visible = true;
                    btnTransfer.Visible = false;
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
                if (BD.GetProdOperate(Session["BRCD"].ToString(), string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString()).ToString() != "3")
                {
                    txtProdName.Text = custnob[0].ToString();
                    ViewState["GlCode"] = custnob[1].ToString();
                    txtProdType.Text = (string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString());
                    AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType.Text.ToString();

                    btnSubmit.Visible = true;
                    btnTransfer.Visible = false;
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
            DT = IO.GetAccStage(Session["BRCD"].ToString(), txtProdType.Text.ToString(), txtAccNo.Text.ToString());
            if (DT.Rows.Count > 0)
            {
                if (DT.Rows[0]["Stage"].ToString() == "1003")
                {
                    if (DT.Rows[0]["Acc_Status"].ToString() != "3")
                    {
                        sResult = IO.GetCustName(Session["BRCD"].ToString(), txtProdType.Text, txtAccNo.Text);
                        if (sResult.ToString() != "")
                        {
                            txtAccName.Text = sResult.ToString();
                            txtAccNo.Text = DT.Rows[0]["AccNo"].ToString();
                            txtCustNo.Text = DT.Rows[0]["CustNo"].ToString();
                            ClearAccText();

                            btnSubmit.Visible = true;
                            btnTransfer.Visible = false;
                            txtFDate.Focus();
                        }
                    }
                    else
                    {
                        ClearAccText();
                        lblMessage.Text = "Account is already closed...!!";
                        ModalPopup.Show(this.Page);
                        txtAccNo.Text = "";
                        txtAccName.Text = "";
                        txtAccNo.Focus();
                    }
                }
                else
                {
                    ClearAccText();
                    lblMessage.Text = "Sorry customer not authorise...!!";
                    ModalPopup.Show(this.Page);
                    txtAccNo.Text = "";
                    txtAccName.Text = "";
                    txtAccNo.Focus();
                }
            }
            else
            {
                ClearAccText();
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
                DT = IO.GetAccStage(Session["BRCD"].ToString(), txtProdType.Text.ToString(), (string.IsNullOrEmpty(AccDet[1].ToString()) ? "" : AccDet[1].ToString()));
                if (DT.Rows.Count > 0)
                {
                    if (DT.Rows[0]["Stage"].ToString() == "1003")
                    {
                        if (DT.Rows[0]["Acc_Status"].ToString() != "3")
                        {
                            sResult = IO.GetCustName(Session["BRCD"].ToString(), txtProdType.Text, txtAccNo.Text);
                            if (sResult.ToString() != "")
                            {
                                txtAccName.Text = sResult.ToString();
                                txtAccNo.Text = DT.Rows[0]["AccNo"].ToString();
                                txtCustNo.Text = DT.Rows[0]["CustNo"].ToString();
                                ClearAccText();

                                btnSubmit.Visible = true;
                                btnTransfer.Visible = false;
                                txtFDate.Focus();
                            }
                        }
                        else
                        {
                            ClearAccText();
                            lblMessage.Text = "Account is already closed...!!";
                            ModalPopup.Show(this.Page);
                            txtAccNo.Text = "";
                            txtAccName.Text = "";
                            txtAccNo.Focus();
                        }
                    }
                    else
                    {
                        ClearAccText();
                        lblMessage.Text = "Sorry customer not authorise...!!";
                        ModalPopup.Show(this.Page);
                        txtAccNo.Text = "";
                        txtAccName.Text = "";
                        txtAccNo.Focus();
                    }
                }
                else
                {
                    ClearAccText();
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

    protected void txtPayProdType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            sResult = IO.GetProduct(Session["BRCD"].ToString(), txtPayProdType.Text.ToString());
            if (sResult != null)
            {
                if (BD.GetProdOperate(Session["BRCD"].ToString(), txtPayProdType.Text.ToString()).ToString() != "3")
                {
                    string[] ACC = sResult.Split('_'); ;
                    ViewState["PayGlCode"] = ACC[0].ToString();
                    txtPayProdName.Text = ACC[2].ToString();
                    AutoPayAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtPayProdType.Text.ToString();

                    txtPayAccNo.Focus();
                }
                else
                {
                    txtPayProdType.Text = "";
                    txtPayProdName.Text = "";
                    WebMsgBox.Show("Product is not operating...!!", this.Page);
                    txtPayProdType.Focus();
                    return;
                }
            }
            else
            {
                txtPayProdType.Text = "";
                txtPayProdName.Text = "";
                WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                txtPayProdType.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtPayProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] custnob = txtPayProdName.Text.ToString().Split('_');
            if (custnob.Length > 1)
            {
                if (BD.GetProdOperate(Session["BRCD"].ToString(), string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString()).ToString() != "3")
                {
                    txtPayProdName.Text = custnob[0].ToString();
                    ViewState["PayGlCode"] = custnob[1].ToString();
                    txtPayProdType.Text = (string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString());
                    AutoPayAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtPayProdType.Text.ToString();

                    txtPayAccNo.Focus();
                }
                else
                {
                    txtPayProdType.Text = "";
                    txtPayProdName.Text = "";
                    WebMsgBox.Show("Product is not operating...!!", this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtPayAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DT = IO.GetAccStage(Session["BRCD"].ToString(), txtPayProdType.Text.ToString(), txtPayAccNo.Text.ToString());
            if (DT.Rows.Count > 0)
            {
                if (DT.Rows[0]["Stage"].ToString() == "1003")
                {
                    if (DT.Rows[0]["Acc_Status"].ToString() != "3")
                    {
                        sResult = IO.GetCustName(Session["BRCD"].ToString(), txtPayProdType.Text, txtPayAccNo.Text);
                        if (sResult.ToString() != "")
                        {
                            txtPayAccName.Text = sResult.ToString();
                            txtPayAccNo.Text = DT.Rows[0]["AccNo"].ToString();
                            txtPayBalance.Text = IO.GetOpenClose(Session["BRCD"].ToString(), txtPayProdType.Text.Trim().ToString(), txtPayAccNo.Text.ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();

                            btnTransfer.Focus();
                        }
                    }
                    else
                    {
                        txtPayAccNo.Text = "";
                        txtPayAccName.Text = "";
                        WebMsgBox.Show("Account is already closed...!!", this.Page);
                        txtPayAccNo.Focus();
                        return;
                    }
                }
                else
                {
                    txtPayAccNo.Text = "";
                    txtPayAccName.Text = "";
                    WebMsgBox.Show("Sorry customer not authorise...!!", this.Page);
                    txtPayAccNo.Focus();
                    return;
                }
            }
            else
            {
                txtPayAccNo.Text = "";
                txtPayAccName.Text = "";
                WebMsgBox.Show("Enter valid account number...!!", this.Page);
                txtPayAccNo.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtPayAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] AccDet = txtPayAccName.Text.ToString().Split('_');
            if (AccDet.Length > 1)
            {
                DT = IO.GetAccStage(Session["BRCD"].ToString(), txtPayProdType.Text.ToString(), (string.IsNullOrEmpty(AccDet[1].ToString()) ? "" : AccDet[1].ToString()));
                if (DT.Rows.Count > 0)
                {
                    if (DT.Rows[0]["Stage"].ToString() == "1003")
                    {
                        if (DT.Rows[0]["Acc_Status"].ToString() != "3")
                        {
                            sResult = IO.GetCustName(Session["BRCD"].ToString(), txtPayProdType.Text, txtPayAccNo.Text);
                            if (sResult.ToString() != "")
                            {
                                txtPayAccName.Text = sResult.ToString();
                                txtPayAccNo.Text = DT.Rows[0]["AccNo"].ToString();
                                txtPayBalance.Text = IO.GetOpenClose(Session["BRCD"].ToString(), txtPayProdType.Text.Trim().ToString(), txtPayAccNo.Text.ToString(), Session["EntryDate"].ToString(), "ClBal").ToString();

                                btnTransfer.Focus();
                            }
                        }
                        else
                        {
                            txtPayAccNo.Text = "";
                            txtPayAccName.Text = "";
                            WebMsgBox.Show("Account is already closed...!!", this.Page);
                            txtPayAccNo.Focus();
                        }
                    }
                    else
                    {
                        txtPayAccNo.Text = "";
                        txtPayAccName.Text = "";
                        WebMsgBox.Show("Sorry customer not authorise...!!", this.Page);
                        txtPayAccNo.Focus();
                    }
                }
                else
                {
                    txtPayAccNo.Text = "";
                    txtPayAccName.Text = "";
                    WebMsgBox.Show("Enter valid account number...!!", this.Page);
                    txtPayAccNo.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Click Events

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

    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            string CustAcc = "";
            double ReverseBal = 0;
            int tmp = 0, cnt = 0;

            foreach (GridViewRow gvRow in grdTransaction.Rows)
            {
                if (((CheckBox)gvRow.FindControl("chk")).Checked)
                {
                    CustAcc = ((Label)gvRow.FindControl("id")).Text.ToString();
                    ReverseBal = Convert.ToDouble(((Label)gvRow.FindControl("lblAmount")).Text.ToString());

                    cnt = IO.InsertTrans(CustAcc.ToString(), Session["BRCD"].ToString(), ViewState["GlCode"].ToString(), txtProdType.Text.ToString(), txtAccNo.Text.ToString());
                    tmp++;
                }
            }

            if (cnt > 0)
            {
                RefNumber = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                ViewState["RID"] = (Convert.ToInt32(RefNumber) + 1).ToString();
                SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString()).ToString();

                if (Convert.ToDouble(SetNo) > 0)
                {
                    Result = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["PayGlCode"].ToString(), txtPayProdType.Text.ToString(),
                             txtPayAccNo.Text.Trim().ToString(), txtNarration.Text.ToString(), "By Transfer", ReverseBal, "2", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                             "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());

                    Result = ITrans.Authorized(Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), Session["EntryDate"].ToString(), ViewState["GlCode"].ToString(), txtProdType.Text.ToString(),
                             txtAccNo.Text.Trim().ToString(), txtNarration.Text.ToString(), "By Transfer", ReverseBal, "1", "7", "TR", SetNo, "0", "01/01/1900", "0", "0", "1001",
                             "", Session["BRCD"].ToString(), Session["MID"].ToString(), "0", "0", "", txtCustNo.Text.Trim().ToString(), txtAccName.Text.ToString(), ViewState["RID"].ToString());
                    
                    if (Result > 0)
                    {
                        WebMsgBox.Show("Successfully Transfer With SetNo : " + Convert.ToDouble(SetNo), this.Page);
                        ClearAllData();
                        txtProdType.Focus();
                        return;
                    }
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

    #region Public Function

    public void BindGridData()
    {
        try
        {
            DT = IO.BindGridData(Session["BRCD"].ToString(), txtProdType.Text.ToString(), txtAccNo.Text.ToString(), txtFDate.Text.ToString(), txtTDate.Text.ToString(), Session["MID"].ToString());
            if (DT.Rows.Count > 0)
            {
                divPayment.Visible = true;
                grdTransaction.DataSource = DT;
                grdTransaction.DataBind();
                divTransaction.Visible = true;
                btnSubmit.Visible = false;
                btnTransfer.Visible = true;
            }
            else
            {
                divPayment.Visible = false;
                grdTransaction.DataSource = null;
                grdTransaction.DataBind();
                divTransaction.Visible = true;
                btnSubmit.Visible = true;
                btnTransfer.Visible = false;
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
            txtCustNo.Text = "";
            txtFDate.Text = "";
            txtTDate.Text = "";
            grdTransaction.DataSource = null;
            grdTransaction.DataBind();
            divTransaction.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearAccText()
    {
        try
        {
            txtTDate.Text = Session["EntryDate"].ToString();
            txtFDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
            grdTransaction.DataSource = null;
            grdTransaction.DataBind();
            divTransaction.Visible = false;
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
            AutoGlName.ContextKey = Session["BRCD"].ToString();
            AutoPayGlName.ContextKey = Session["BRCD"].ToString();
            txtProdType.Text = "";
            txtProdName.Text = "";
            txtAccNo.Text = "";
            txtAccName.Text = "";
            txtCustNo.Text = "";
            txtFDate.Text = "";
            txtTDate.Text = "";

            txtPayProdType.Text = "";
            txtPayProdName.Text = "";
            txtPayAccNo.Text = "";
            txtPayAccName.Text = "";
            txtNarration.Text = "";

            grdTransaction.DataSource = null;
            grdTransaction.DataBind();
            btnSubmit.Visible = true;
            btnTransfer.Visible = false;
            divPayment.Visible = false;
            divTransaction.Visible = false;
            txtTDate.Text = Session["EntryDate"].ToString();
            txtFDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

}