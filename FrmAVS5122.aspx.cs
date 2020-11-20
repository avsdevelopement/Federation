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

public partial class FrmAVS5122 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAVS5122 Trf = new ClsAVS5122();
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
                AutoFGlName.ContextKey = Session["BRCD"].ToString();
                AutoTGlName.ContextKey = Session["BRCD"].ToString();

                txtFProdType.Focus();
            }
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Index Changed

    protected void rbtnType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtnType.SelectedValue.ToString() == "P")
            {
                divFAccInfo.Visible = false;
                divTAccInfo.Visible = false;
            }
            else if (rbtnType.SelectedValue.ToString() == "A")
            {
                divFAccInfo.Visible = true;
                divTAccInfo.Visible = true;
            }

            txtFProdType.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Text Changed

    protected void txtFProdType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            sResult = Trf.GetProduct(Session["BRCD"].ToString(), txtFProdType.Text.ToString());

            if (sResult != null)
            {
                if (BD.GetProdOperate(Session["BRCD"].ToString(), txtFProdType.Text.ToString()).ToString() != "3")
                {
                    string[] ACC = sResult.Split('_'); ;
                    ViewState["DrGlCode"] = ACC[0].ToString();
                    txtFProdName.Text = ACC[2].ToString();
                    AutoFAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtFProdType.Text.ToString();

                    txtFAccNo.Text = "";
                    txtFAccName.Text = "";
                    txtFCustNo.Text = "";
                    txtFAccNo.Focus();
                }
                else
                {
                    txtFProdType.Text = "";
                    txtFProdName.Text = "";
                    txtFProdType.Focus();
                    WebMsgBox.Show("Product is not operating...!!", this.Page);
                    return;
                }
            }
            else
            {
                txtFProdType.Text = "";
                txtFProdName.Text = "";
                txtFProdType.Focus();
                WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtFProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] custnob = txtFProdName.Text.ToString().Split('_');
            if (custnob.Length > 1)
            {
                if (BD.GetProdOperate(Session["BRCD"].ToString(), string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString()).ToString() != "3")
                {
                    txtFProdName.Text = custnob[0].ToString();
                    ViewState["DrGlCode"] = custnob[1].ToString();
                    txtFProdType.Text = (string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString());
                    AutoFAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtFProdType.Text.ToString();

                    txtFAccNo.Text = "";
                    txtFAccName.Text = "";
                    txtFCustNo.Text = "";
                    txtFAccNo.Focus();
                }
                else
                {
                    txtFProdType.Text = "";
                    txtFProdName.Text = "";
                    txtFProdType.Focus();
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

    protected void txtFAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DT = Trf.GetAccStage(Session["BRCD"].ToString(), txtFProdType.Text.ToString(), txtFAccNo.Text.ToString());
            if (DT.Rows.Count > 0)
            {
                if (DT.Rows[0]["Stage"].ToString() == "1003")
                {
                    if (DT.Rows[0]["Acc_Status"].ToString() != "3")
                    {
                        sResult = Trf.GetCustName(Session["BRCD"].ToString(), txtFProdType.Text, txtFAccNo.Text);
                        if (sResult.ToString() != "")
                        {
                            txtFAccName.Text = sResult.ToString();
                            txtFAccNo.Text = DT.Rows[0]["AccNo"].ToString();
                            txtFCustNo.Text = DT.Rows[0]["CustNo"].ToString();

                            txtTProdType.Focus();
                        }
                    }
                    else
                    {
                        txtFAccNo.Text = "";
                        txtFAccName.Text = "";
                        txtFCustNo.Text = "";
                        txtFAccNo.Focus();
                        WebMsgBox.Show("Account is already closed...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    txtFAccNo.Text = "";
                    txtFAccName.Text = "";
                    txtFCustNo.Text = "";
                    txtFAccNo.Focus();
                    WebMsgBox.Show("Sorry customer not authorise...!!", this.Page);
                    return;
                }
            }
            else
            {
                txtFAccNo.Text = "";
                txtFAccName.Text = "";
                txtFCustNo.Text = "";
                txtFAccNo.Focus();
                WebMsgBox.Show("Enter valid account number...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtFAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] AccDet = txtFAccName.Text.ToString().Split('_');
            if (AccDet.Length > 1)
            {
                DT = Trf.GetAccStage(Session["BRCD"].ToString(), txtFProdType.Text.ToString(), (string.IsNullOrEmpty(AccDet[1].ToString()) ? "" : AccDet[1].ToString()));
                if (DT.Rows.Count > 0)
                {
                    if (DT.Rows[0]["Stage"].ToString() == "1003")
                    {
                        if (DT.Rows[0]["Acc_Status"].ToString() != "3")
                        {
                            sResult = Trf.GetCustName(Session["BRCD"].ToString(), txtFProdType.Text, txtFAccNo.Text);
                            if (sResult.ToString() != "")
                            {
                                txtFAccName.Text = sResult.ToString();
                                txtFAccNo.Text = DT.Rows[0]["AccNo"].ToString();
                                txtFCustNo.Text = DT.Rows[0]["CustNo"].ToString();

                                txtTProdType.Focus();
                            }
                        }
                        else
                        {
                            txtFAccNo.Text = "";
                            txtFAccName.Text = "";
                            txtFCustNo.Text = "";
                            txtFAccNo.Focus();
                            WebMsgBox.Show("Account is already closed...!!", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        txtFAccNo.Text = "";
                        txtFAccName.Text = "";
                        txtFCustNo.Text = "";
                        txtFAccNo.Focus();
                        WebMsgBox.Show("Sorry customer not authorise...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    txtFAccNo.Text = "";
                    txtFAccName.Text = "";
                    txtFCustNo.Text = "";
                    txtFAccNo.Focus();
                    WebMsgBox.Show("Enter valid account number...!!", this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtTProdType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            sResult = Trf.GetProduct(Session["BRCD"].ToString(), txtTProdType.Text.ToString());

            if (sResult != null)
            {
                if (BD.GetProdOperate(Session["BRCD"].ToString(), txtTProdType.Text.ToString()).ToString() != "3")
                {
                    string[] ACC = sResult.Split('_'); ;
                    ViewState["DrGlCode"] = ACC[0].ToString();
                    txtTProdName.Text = ACC[2].ToString();
                    AutoTAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtTProdType.Text.ToString();

                    txtTAccNo.Text = "";
                    txtTAccName.Text = "";
                    txtTCustNo.Text = "";
                    txtTAccNo.Focus();
                }
                else
                {
                    txtTProdType.Text = "";
                    txtTProdName.Text = "";
                    txtTProdType.Focus();
                    WebMsgBox.Show("Product is not operating...!!", this.Page);
                    return;
                }
            }
            else
            {
                txtTProdType.Text = "";
                txtTProdName.Text = "";
                txtTProdType.Focus();
                WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtTProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] custnob = txtTProdName.Text.ToString().Split('_');
            if (custnob.Length > 1)
            {
                if (BD.GetProdOperate(Session["BRCD"].ToString(), string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString()).ToString() != "3")
                {
                    txtTProdName.Text = custnob[0].ToString();
                    ViewState["DrGlCode"] = custnob[1].ToString();
                    txtTProdType.Text = (string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString());
                    AutoTAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtTProdType.Text.ToString();

                    txtTAccNo.Text = "";
                    txtTAccName.Text = "";
                    txtTCustNo.Text = "";
                    txtTAccNo.Focus();
                }
                else
                {
                    txtTProdType.Text = "";
                    txtTProdName.Text = "";
                    txtTProdType.Focus();
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

    protected void txtTAccNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DT = Trf.GetAccStage(Session["BRCD"].ToString(), txtTProdType.Text.ToString(), txtTAccNo.Text.ToString());
            if (DT.Rows.Count > 0)
            {
                if (DT.Rows[0]["Stage"].ToString() == "1003")
                {
                    if (DT.Rows[0]["Acc_Status"].ToString() != "3")
                    {
                        sResult = Trf.GetCustName(Session["BRCD"].ToString(), txtTProdType.Text, txtTAccNo.Text);
                        if (sResult.ToString() != "")
                        {
                            txtTAccName.Text = sResult.ToString();
                            txtTAccNo.Text = DT.Rows[0]["AccNo"].ToString();
                            txtTCustNo.Text = DT.Rows[0]["CustNo"].ToString();

                            txtFDate.Focus();
                        }
                    }
                    else
                    {
                        txtTAccNo.Text = "";
                        txtTAccName.Text = "";
                        txtTCustNo.Text = "";
                        txtTAccNo.Focus();
                        WebMsgBox.Show("Account is already closed...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    txtTAccNo.Text = "";
                    txtTAccName.Text = "";
                    txtTCustNo.Text = "";
                    txtTAccNo.Focus();
                    WebMsgBox.Show("Sorry customer not authorise...!!", this.Page);
                    return;
                }
            }
            else
            {
                txtTAccNo.Text = "";
                txtTAccName.Text = "";
                txtTCustNo.Text = "";
                txtTAccNo.Focus();
                WebMsgBox.Show("Enter valid account number...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtTAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] AccDet = txtTAccName.Text.ToString().Split('_');
            if (AccDet.Length > 1)
            {
                DT = Trf.GetAccStage(Session["BRCD"].ToString(), txtTProdType.Text.ToString(), (string.IsNullOrEmpty(AccDet[1].ToString()) ? "" : AccDet[1].ToString()));
                if (DT.Rows.Count > 0)
                {
                    if (DT.Rows[0]["Stage"].ToString() == "1003")
                    {
                        if (DT.Rows[0]["Acc_Status"].ToString() != "3")
                        {
                            sResult = Trf.GetCustName(Session["BRCD"].ToString(), txtTProdType.Text, txtTAccNo.Text);
                            if (sResult.ToString() != "")
                            {
                                txtTAccName.Text = sResult.ToString();
                                txtTAccNo.Text = DT.Rows[0]["AccNo"].ToString();
                                txtTCustNo.Text = DT.Rows[0]["CustNo"].ToString();

                                txtFDate.Focus();
                            }
                        }
                        else
                        {
                            txtTAccNo.Text = "";
                            txtTAccName.Text = "";
                            txtTCustNo.Text = "";
                            txtTAccNo.Focus();
                            WebMsgBox.Show("Account is already closed...!!", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        txtTAccNo.Text = "";
                        txtTAccName.Text = "";
                        txtTCustNo.Text = "";
                        txtTAccNo.Focus();
                        WebMsgBox.Show("Sorry customer not authorise...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    txtTAccNo.Text = "";
                    txtTAccName.Text = "";
                    txtTCustNo.Text = "";
                    txtTAccNo.Focus();
                    WebMsgBox.Show("Enter valid account number...!!", this.Page);
                    return;
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

    protected void Transfer_Click(object sender, EventArgs e)
    {
        try
        {
            Result = Trf.TransferAcc(Session["BRCD"].ToString(), txtFProdType.Text.ToString(), txtFAccNo.Text.ToString(), txtTProdType.Text.ToString(), txtTAccNo.Text.ToString(), txtFDate.Text.ToString(), txtTDate.Text.ToString(), Session["Mid"].ToString(), rbtnType.SelectedValue.ToString());
            if (Result > 0)
            {
                ClearAllData();
                WebMsgBox.Show("Successfully transfer...!!", this.Page);
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

    public void ClearAllData()
    {
        try
        {
            txtFProdType.Text = "";
            txtFProdName.Text = "";

            txtFAccNo.Text = "";
            txtFAccName.Text = "";
            txtFCustNo.Text = "";

            txtTProdType.Text = "";
            txtTProdName.Text = "";

            txtTAccNo.Text = "";
            txtTAccName.Text = "";
            txtTCustNo.Text = "";

            txtFDate.Text = "";
            txtTDate.Text = "";

            txtFProdType.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

}