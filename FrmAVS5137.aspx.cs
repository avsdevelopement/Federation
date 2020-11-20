using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class FrmAVS5137 : System.Web.UI.Page
{

    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAVS5137 CS = new ClsAVS5137();
    ClsCommon cmn = new ClsCommon();
    DataTable DT = new DataTable();
    string sResult = "";
    int Result = 0;
    #region PageLoad

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
                Response.Redirect("FrmLogin.aspx");

            if (!IsPostBack)
            {
                //  Added by amol on 03/10/2018 for log details
                LogDetails();

                WorkingDate.Value = Session["EntryDate"].ToString();
                txtIssueDate.Text = Session["EntryDate"].ToString();
               
                AutoGlName.ContextKey = Session["BRCD"].ToString();
                AutoBankname.ContextKey = Session["BRCD"].ToString();
                BindGrid();
              
                txtNoOfBooks.Focus();

            }
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 900000;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Text Changed

    protected void rbtnType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtnType.SelectedValue == "1")
            {

                ClearAllData();
                DivBank.Visible = true;
                divAccInfo.Visible = false;
                txtNoOfBooks.Enabled = true;
                ddlBookSize.Enabled = true;
                txtIssueDate.Text = Session["EntryDate"].ToString();
              
            }

            else if (rbtnType.SelectedValue == "2")
            {
                ClearAllData();
                DivBank.Visible = false;
                divAccInfo.Visible = false;
                txtNoOfBooks.Enabled = true;
                ddlBookSize.Enabled = true;
                txtIssueDate.Text = Session["EntryDate"].ToString();
            
                txtNoOfBooks.Focus();
              
            }
            else if (rbtnType.SelectedValue == "3")
            {
                ClearAllData();
                DivBank.Visible = false;
                divAccInfo.Visible = true;
                txtNoOfBooks.Enabled = false;
                ddlBookSize.Enabled = false;
                txtIssueDate.Text = Session["EntryDate"].ToString();
     
                txtSInstNo.Text = "";
                txtEInstNo.Text = "";

                txtProdType.Focus();
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
            sResult = CS.GetProduct(Session["BRCD"].ToString(), txtProdType.Text.ToString());
            if (sResult != null)
            {
                if (BD.GetProdOperate(Session["BRCD"].ToString(), txtProdType.Text.ToString()).ToString() != "3")
                {
                    string[] ACC = sResult.Split('_');
                    ViewState["GlCode"] = ACC[0].ToString();
                    txtProdName.Text = ACC[2].ToString();
                    AutoAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType.Text.ToString();
                    sResult = cmn.GetIntACCYN(Session["BRCD"].ToString(), txtProdType.Text.ToString());

                    if (sResult != "Y")
                    {
                        txtAccNo.Text = txtProdType.Text.ToString();
                        txtAccName.Text = txtProdName.Text.ToString();
                        txtCustNo.Text = "0";

                        if (rbtnType.SelectedValue == "1")
                            txtIssueDate.Focus();
                        else if (rbtnType.SelectedValue == "2")
                            txtSInstNo.Focus();
                        return;
                    }

                    txtAccNo.Text = "";
                    txtAccName.Text = "";
                    txtCustNo.Text = "";

                    if (rbtnType.SelectedValue == "1")
                        txtIssueDate.Focus();
                    else if (rbtnType.SelectedValue == "2")
                        txtAccNo.Focus();
                }
                else
                {
                    txtProdType.Text = "";
                    txtProdName.Text = "";
                    txtProdType.Focus();
                    WebMsgBox.Show("Product is not operating...!!", this.Page);
                    return;
                }
            }
            else
            {
                txtProdType.Text = "";
                txtProdName.Text = "";
                txtProdType.Focus();
                WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                return;
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
                txtProdType.Text = (string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString());
                sResult = CS.GetProduct(Session["BRCD"].ToString(), txtProdType.Text.ToString());
                if (sResult != null)
                {
                    if (BD.GetProdOperate(Session["BRCD"].ToString(), txtProdType.Text.ToString()).ToString() != "3")
                    {
                        string[] ACC = sResult.Split('_');
                        ViewState["GlCode"] = ACC[0].ToString();
                        txtProdName.Text = ACC[2].ToString();
                        AutoAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtProdType.Text.ToString();
                        sResult = cmn.GetIntACCYN(Session["BRCD"].ToString(), txtProdType.Text.ToString());

                        if (sResult != "Y")
                        {
                            txtAccNo.Text = txtProdType.Text.ToString();
                            txtAccName.Text = txtProdName.Text.ToString();
                            txtCustNo.Text = "0";

                            if (rbtnType.SelectedValue == "1")
                                txtIssueDate.Focus();
                            else if (rbtnType.SelectedValue == "2")
                                txtSInstNo.Focus();
                            return;
                        }

                        txtAccNo.Text = "";
                        txtAccName.Text = "";
                        txtCustNo.Text = "";

                        if (rbtnType.SelectedValue == "1")
                            txtIssueDate.Focus();
                        else if (rbtnType.SelectedValue == "2")
                            txtAccNo.Focus();
                    }
                    else
                    {
                        txtProdType.Text = "";
                        txtProdName.Text = "";
                        txtProdType.Focus();
                        WebMsgBox.Show("Product is not operating...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    txtProdType.Text = "";
                    txtProdName.Text = "";
                    txtProdType.Focus();
                    WebMsgBox.Show("Enter valid Product code...!!", this.Page);
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
            DT = CS.GetAccStage(Session["BRCD"].ToString(), txtProdType.Text.ToString(), txtAccNo.Text.ToString());
            if (DT.Rows.Count > 0)
            {
                if (DT.Rows[0]["Stage"].ToString() == "1003")
                {
                    if (DT.Rows[0]["Acc_Status"].ToString() == "3")
                    {
                        txtAccNo.Text = "";
                        txtAccName.Text = "";
                        txtCustNo.Text = "";
                        txtAccNo.Focus();
                        WebMsgBox.Show("Account is closed ...!!", this.Page);
                        return;
                    }
                    else
                    {
                        sResult = CS.GetCustName(Session["BRCD"].ToString(), txtProdType.Text.ToString(), txtAccNo.Text.ToString());
                        if (sResult.ToString() != "")
                        {
                            txtAccName.Text = sResult.ToString();
                            txtAccNo.Text = DT.Rows[0]["AccNo"].ToString();
                            txtCustNo.Text = DT.Rows[0]["CustNo"].ToString();

                            txtSInstNo.Focus();
                        }
                    }
                }
                else
                {
                    txtAccNo.Text = "";
                    txtAccName.Text = "";
                    txtCustNo.Text = "";
                    txtAccNo.Focus();
                    WebMsgBox.Show("Sorry account not authorise ...!!", this.Page);
                    return;
                }
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
                txtAccNo.Text = (string.IsNullOrEmpty(AccDet[1].ToString()) ? "" : AccDet[1].ToString()).ToString();
                DT = CS.GetAccStage(Session["BRCD"].ToString(), txtProdType.Text.ToString(), txtAccNo.Text.ToString());
                if (DT.Rows.Count > 0)
                {
                    if (DT.Rows[0]["Stage"].ToString() == "1003")
                    {
                        if (DT.Rows[0]["Acc_Status"].ToString() == "3")
                        {
                            txtAccNo.Text = "";
                            txtAccName.Text = "";
                            txtCustNo.Text = "";
                            txtAccNo.Focus();
                            WebMsgBox.Show("Account is closed ...!!", this.Page);
                            return;
                        }
                        else
                        {
                            sResult = CS.GetCustName(Session["BRCD"].ToString(), txtProdType.Text.ToString(), txtAccNo.Text.ToString());
                            if (sResult.ToString() != "")
                            {
                                txtAccName.Text = sResult.ToString();
                                txtAccNo.Text = DT.Rows[0]["AccNo"].ToString();
                                txtCustNo.Text = DT.Rows[0]["CustNo"].ToString();

                                txtSInstNo.Focus();
                            }
                        }
                    }
                    else
                    {
                        txtAccNo.Text = "";
                        txtAccName.Text = "";
                        txtCustNo.Text = "";
                        txtAccNo.Focus();
                        WebMsgBox.Show("Sorry account not authorise ...!!", this.Page);
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

    protected void txtNoOfBooks_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if ((txtNoOfBooks.Text.ToString() != "") && (rbtnType.SelectedValue == "1"))
                InoCalc();

            ddlBookSize.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlBookSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if ((ddlBookSize.SelectedValue != "0") && (rbtnType.SelectedValue == "1"))
                InoCalc();

            txtSInstNo.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtSInstNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtnType.SelectedValue == "1")
            {
                sResult = CS.CheckExists(Session["BRCD"].ToString(), "0", txtSInstNo.Text.ToString(), rbtnType.SelectedValue);
                if ((Convert.ToDouble(sResult) == 0) && (txtSInstNo.Text.ToString() != ""))
                {
                    txtNoOfBooks.Text = txtNoOfBooks.Text.ToString() == "" ? "0" : txtNoOfBooks.Text.ToString();
                    txtSInstNo.Text = txtSInstNo.Text.ToString();
                    txtEInstNo.Text = (Convert.ToInt32(Convert.ToInt32(txtSInstNo.Text.ToString()) + (Convert.ToInt32(ddlBookSize.SelectedValue) * Convert.ToDouble(txtNoOfBooks.Text.ToString()))) - 1).ToString();

                    txtIssueDate.Focus();
                }
            }
            if (rbtnType.SelectedValue == "2")
            {
                sResult = CS.CheckExists(Session["BRCD"].ToString(), "0", txtSInstNo.Text.ToString(), rbtnType.SelectedValue);
                if ((Convert.ToDouble(sResult) == 0) && (txtSInstNo.Text.ToString() != ""))
                {
                    txtNoOfBooks.Text = txtNoOfBooks.Text.ToString() == "" ? "0" : txtNoOfBooks.Text.ToString();
                    txtSInstNo.Text = txtSInstNo.Text.ToString();
                    txtEInstNo.Text = (Convert.ToInt32(Convert.ToInt32(txtSInstNo.Text.ToString()) + (Convert.ToInt32(ddlBookSize.SelectedValue) * Convert.ToDouble(txtNoOfBooks.Text.ToString()))) - 1).ToString();

                    txtIssueDate.Focus();
                }
                else
                {
                    txtSInstNo.Text = "";
                    txtEInstNo.Text = "";
                    txtSInstNo.Focus();
                    WebMsgBox.Show("Cheque number already exists in stock ...!!", this.Page);
                    return;
                }
            }
            else if (rbtnType.SelectedValue == "3")
            {
                sResult = CS.CheckExists(Session["BRCD"].ToString(), txtProdType.Text.ToString(), txtSInstNo.Text.ToString(), rbtnType.SelectedValue);
                if ((Convert.ToDouble(sResult) == 0) && (txtSInstNo.Text.ToString() != ""))
                {
                    txtNoOfBooks.Text = txtNoOfBooks.Text.ToString() == "" ? "0" : txtNoOfBooks.Text.ToString();
                    txtSInstNo.Text = txtSInstNo.Text.ToString();
                    txtEInstNo.Text = (Convert.ToInt32(Convert.ToInt32(txtSInstNo.Text.ToString()) + (Convert.ToInt32(ddlBookSize.SelectedValue) * Convert.ToDouble(txtNoOfBooks.Text.ToString()))) - 1).ToString();

                    txtIssueDate.Focus();
                }
                else
                {
                    txtSInstNo.Text = "";
                    txtEInstNo.Text = "";
                    txtSInstNo.Focus();
                    WebMsgBox.Show("Cheque number already exists in stock ...!!", this.Page);
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtEInstNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtnType.SelectedValue == "1")
            {
                sResult = CS.CheckExists(Session["BRCD"].ToString(), "0", txtSInstNo.Text.ToString(), rbtnType.SelectedValue);
                if ((Convert.ToDouble(sResult) == 0) && (txtSInstNo.Text.ToString() != ""))
                {
                    txtNoOfBooks.Text = txtNoOfBooks.Text.ToString() == "" ? "0" : txtNoOfBooks.Text.ToString();
                    txtSInstNo.Text = txtSInstNo.Text.ToString();
                    txtEInstNo.Text = (Convert.ToInt32(Convert.ToInt32(txtSInstNo.Text.ToString()) + (Convert.ToInt32(ddlBookSize.SelectedValue) * Convert.ToDouble(txtNoOfBooks.Text.ToString()))) - 1).ToString();

                    txtRemark.Focus();
                }
                else
                {
                    txtSInstNo.Text = "";
                    txtEInstNo.Text = "";
                    txtSInstNo.Focus();
                    WebMsgBox.Show("Cheque number already exists ...!!", this.Page);
                    return;
                }
            }
            else if (rbtnType.SelectedValue == "2")
            {
                sResult = CS.CheckExists(Session["BRCD"].ToString(), txtProdType.Text.ToString(), txtSInstNo.Text.ToString(), rbtnType.SelectedValue);
                if ((Convert.ToDouble(sResult) == 0) && (txtSInstNo.Text.ToString() != ""))
                {
                    txtNoOfBooks.Text = txtNoOfBooks.Text.ToString() == "" ? "0" : txtNoOfBooks.Text.ToString();
                    txtSInstNo.Text = txtSInstNo.Text.ToString();
                    txtEInstNo.Text = (Convert.ToInt32(Convert.ToInt32(txtSInstNo.Text.ToString()) + (Convert.ToInt32(ddlBookSize.SelectedValue) * Convert.ToDouble(txtNoOfBooks.Text.ToString()))) - 1).ToString();

                    txtRemark.Focus();
                }
                else
                {
                    txtSInstNo.Text = "";
                    txtEInstNo.Text = "";
                    txtSInstNo.Focus();
                    WebMsgBox.Show("Cheque number already exists ...!!", this.Page);
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

    #region Function

    //  Added by amol on 03/10/2018 for log details
    public void LogDetails()
    {
        try
        {
            cmn.LogDetails(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "Create Cheque Stock", "", "", Session["MID"].ToString());
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
            DT = CS.BindStockGrid(Session["BRCD"].ToString(), Session["EntryDate"].ToString());
            if (DT.Rows.Count > 0)
            {
                grdCHQStock.DataSource = DT;
                grdCHQStock.DataBind();
            }
            else
            {
                grdCHQStock.DataSource = null;
                grdCHQStock.DataBind();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ShowDetails(string SrNumber, string sFlag)
    {
        DT = new DataTable();
        try
        {
            DT = CS.BindStockGrid(Session["BRCD"].ToString(), SrNumber.ToString());
            if (DT.Rows.Count > 0)
            {
                rbtnType.SelectedValue = "1";
                txtIssueDate.Text = DT.Rows[0]["IssuedDate"].ToString();
                txtNoOfBooks.Text = DT.Rows[0]["NoOfBooks"].ToString();
                ddlBookSize.SelectedValue = DT.Rows[0]["BookSize"].ToString();
                txtSInstNo.Text = DT.Rows[0]["StartInsNo"].ToString();
                txtEInstNo.Text = DT.Rows[DT.Rows.Count - 1]["StartInsNo"].ToString();
                txtRemark.Text = DT.Rows[0]["Remarks"].ToString();
                txtBankcode.Text = DT.Rows[0]["SubGlCode"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void InoCalc()
    {
        try
        {
            string SIno = CS.GetMaxIno(Session["BRCD"].ToString());
            if (SIno != null)
            {
                txtNoOfBooks.Text = txtNoOfBooks.Text.ToString() == "" ? "0" : txtNoOfBooks.Text.ToString();
                txtSInstNo.Text = SIno.ToString();

                if ((Convert.ToDouble(txtNoOfBooks.Text.ToString()) > 0) && (Convert.ToDouble(ddlBookSize.SelectedValue) > 0))
                    txtEInstNo.Text = (Convert.ToInt32(Convert.ToInt32(txtSInstNo.Text.ToString()) + (Convert.ToInt32(ddlBookSize.SelectedValue) * Convert.ToDouble(txtNoOfBooks.Text.ToString()))) - 1).ToString();
                else
                    txtEInstNo.Text = txtSInstNo.Text.ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void Enabled(bool Flag)
    {
        try
        {
            rbtnType.Enabled = Flag;
            txtProdType.Enabled = Flag;
            txtProdName.Enabled = Flag;
            txtIssueDate.Enabled = Flag;
            txtNoOfBooks.Enabled = Flag;
            ddlBookSize.Enabled = Flag;
            txtSInstNo.Enabled = Flag;
            txtEInstNo.Enabled = Flag;
            txtRemark.Enabled = Flag;
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
            txtBankcode.Text = "";
            txtBankName.Text = "";
            txtProdType.Text = "";
            txtProdName.Text = "";
            txtAccNo.Text = "";
            txtAccName.Text = "";
            txtCustNo.Text = "";
            txtIssueDate.Text = "";
            txtNoOfBooks.Text = "";
            ddlBookSize.SelectedIndex = 0;
            txtSInstNo.Text = "";
            txtEInstNo.Text = "";
            txtRemark.Text = "";

            txtProdType.Focus();
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
            if ((rbtnType.SelectedValue == "3") && (txtProdType.Text.ToString() == ""))
            {
                txtProdType.Focus();
                WebMsgBox.Show("Enter product code first ...!!", this.Page);
                return;
            }
            else if ((rbtnType.SelectedValue == "3") && (txtAccNo.Text.ToString() == ""))
            {
                txtAccNo.Focus();
                WebMsgBox.Show("Enter account no first ...!!", this.Page);
                return;
            }
            else if ((rbtnType.SelectedValue == "1") && (Convert.ToDouble(txtNoOfBooks.Text.ToString() == "" ? "0" : txtNoOfBooks.Text.ToString()) <= 0))
            {
                txtNoOfBooks.Focus();
                WebMsgBox.Show("Enter no of pages first ...!!", this.Page);
                return;
            }
            else if ((rbtnType.SelectedValue == "1") && (txtBankcode.Text.ToString() == ""))
            {
                txtSInstNo.Focus();
                WebMsgBox.Show("Enter Bank Code first ...!!", this.Page);
                return;
            }
            else if ((rbtnType.SelectedValue == "1") && (txtBankName.Text.ToString() == ""))
            {
                txtSInstNo.Focus();
                WebMsgBox.Show("Enter Bank Name first ...!!", this.Page);
                return;
            }
            else if ((rbtnType.SelectedValue == "1") && (ddlBookSize.SelectedValue == "0"))
            {
                ddlBookSize.Focus();
                WebMsgBox.Show("Enter Leaf per page first ...!!", this.Page);
                return;
            }
            else if ((txtSInstNo.Text.ToString() == "") || (txtSInstNo.Text.ToString() == ""))
            {
                txtSInstNo.Focus();
                WebMsgBox.Show("Enter Proper stock first ...!!", this.Page);
                return;
            }
          
                  else if ((rbtnType.SelectedValue == "2") && (Convert.ToDouble(txtNoOfBooks.Text.ToString() == "" ? "0" : txtNoOfBooks.Text.ToString()) <= 0))
            {
                txtNoOfBooks.Focus();
                WebMsgBox.Show("Enter no of pages first ...!!", this.Page);
                return;
            }
            else if ((rbtnType.SelectedValue == "2") && (ddlBookSize.SelectedValue == "0"))
            {
                ddlBookSize.Focus();
                WebMsgBox.Show("Enter Leaf per page first ...!!", this.Page);
                return;
            }
            else if ((txtSInstNo.Text.ToString() == "") || (txtSInstNo.Text.ToString() == ""))
            {
                txtSInstNo.Focus();
                WebMsgBox.Show("Enter Proper stock first ...!!", this.Page);
                return;
            }
            else
            {
                if (rbtnType.SelectedValue == "1")
                {
                    txtIssueDate.Text = txtIssueDate.Text.ToString() == "" ? Session["EntryDate"].ToString() : txtIssueDate.Text.ToString();
                    Result = CS.BankChequeStock(Session["BRCD"].ToString(),txtBankcode.Text.ToString(), txtNoOfBooks.Text.ToString(), ddlBookSize.SelectedValue, txtSInstNo.Text.ToString(),
                            txtEInstNo.Text.ToString(), txtIssueDate.Text.ToString(), Session["EntryDate"].ToString(), txtRemark.Text.ToString(), Session["MID"].ToString());

                    if (Result > 0)
                    {
                        BindGrid();
                        ClearAllData();
                        WebMsgBox.Show("Successfully stock assign ...!!", this.Page);
                        return;
                    }
                }
                if (rbtnType.SelectedValue == "2")
                {
                    txtIssueDate.Text = txtIssueDate.Text.ToString() == "" ? Session["EntryDate"].ToString() : txtIssueDate.Text.ToString();
                    Result = CS.ChequeStock(Session["BRCD"].ToString(), txtNoOfBooks.Text.ToString(), ddlBookSize.SelectedValue, txtSInstNo.Text.ToString(),
                            txtEInstNo.Text.ToString(), txtIssueDate.Text.ToString(), Session["EntryDate"].ToString(), txtRemark.Text.ToString(), Session["MID"].ToString());

                    if (Result > 0)
                    {
                        BindGrid();
                        ClearAllData();
                        WebMsgBox.Show("Successfully stock assign ...!!", this.Page);
                        return;
                    }
                }
                else if (rbtnType.SelectedValue == "3")
                {
                    txtIssueDate.Text = txtIssueDate.Text.ToString() == "" ? Session["EntryDate"].ToString() : txtIssueDate.Text.ToString();
                    Result = CS.LooseChqStock(Session["BRCD"].ToString(), txtProdType.Text.ToString(), txtAccNo.Text.ToString(), txtSInstNo.Text.ToString(), txtEInstNo.Text.ToString(),
                        txtIssueDate.Text.ToString(), Session["EntryDate"].ToString(), txtRemark.Text.ToString(), Session["MID"].ToString());

                    if (Result > 0)
                    {
                        Result = CS.ChequeIssue(Session["BRCD"].ToString(), txtProdType.Text.ToString(), txtAccNo.Text.ToString(), txtSInstNo.Text.ToString(),
                            txtIssueDate.Text.ToString(), "1", "1", Session["EntryDate"].ToString(), txtRemark.Text.ToString(), Session["MID"].ToString());

                        if (Result > 0)
                        {
                            ClearAllData();
                            WebMsgBox.Show("Successfully stock assign ...!!", this.Page);
                            return;
                        }
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
            LinkButton LnkEdit = (LinkButton)sender;
            string[] Info = LnkEdit.CommandArgument.ToString().Split(',');
            ViewState["SrNumber"] = Info[0].ToString();
           ViewState["Mid"] = Info[1].ToString();
            ViewState["Flag"] = "AT";

            if (ViewState["Mid"].ToString() != Session["MID"].ToString())
            {
                Enabled(false);
                btnSubmit.Visible = false;
                btnAuthorize.Visible = true;
                btnAuthorize.Text = "Authorize";
                ShowDetails(ViewState["SrNumber"].ToString(), ViewState["Flag"].ToString());

                btnAuthorize.Focus();
            }
            else
            {
                WebMsgBox.Show("Same user is restricted to authorize ...!!", this.Page);
                return;
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
            LinkButton LnkEdit = (LinkButton)sender;
            string[] Info = LnkEdit.CommandArgument.ToString().Split(',');
            ViewState["SrNumber"] = Info[0].ToString();
            ViewState["Mid"] = Info[1].ToString();
            ViewState["Flag"] = "DL";

            if (ViewState["Mid"].ToString() != Session["MID"].ToString())
            {
                Enabled(false);
                btnSubmit.Visible = false;
                btnAuthorize.Visible = true;
                btnAuthorize.Text = "Cancel";
                ShowDetails(ViewState["SrNumber"].ToString(), ViewState["Flag"].ToString());

                btnAuthorize.Focus();
            }
            else
            {
                WebMsgBox.Show("Same user is restricted to Delete ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnAuthorize_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Flag"].ToString() == "AT")
            {
                Result = CS.AuthorizeStock(Session["BRCD"].ToString(), Session["MID"].ToString());
                if (Result > 0)
                {
                    BindGrid();
                    ClearAllData();
                    Enabled(true);
                    btnSubmit.Visible = true;
                    btnAuthorize.Visible = false;
                    WebMsgBox.Show("Stock successfully authorized ...!!", this.Page);
                    return;
                }
            }
            else if (ViewState["Flag"].ToString() == "DL")
            {
                Result = CS.DeleteStock(Session["BRCD"].ToString(), Session["MID"].ToString());
                if (Result > 0)
                {
                    BindGrid();
                    ClearAllData();
                    Enabled(true);
                    btnSubmit.Visible = true;
                    btnAuthorize.Visible = false;
                    WebMsgBox.Show("Stock successfully canceled ...!!", this.Page);
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
    protected void txtBankcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            sResult = CS.GetProduct(Session["BRCD"].ToString(), txtBankcode.Text.ToString());
            if (sResult != null)
            {
                if (BD.GetProdOperate(Session["BRCD"].ToString(), txtBankcode.Text.ToString()).ToString() != "3")
                {
                    string[] ACC = sResult.Split('_');
                    ViewState["GlCode"] = ACC[0].ToString();
                    txtBankName.Text = ACC[2].ToString();
                    AutoAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtBankcode.Text.ToString();
                    sResult = cmn.GetIntACCYN(Session["BRCD"].ToString(), txtBankcode.Text.ToString());

                    if (sResult != "Y")
                    {
                       
                       
                        if (rbtnType.SelectedValue == "1")
                            txtNoOfBooks.Focus();
                        else if (rbtnType.SelectedValue == "2")
                            txtIssueDate.Focus();
                        else if (rbtnType.SelectedValue == "3")
                            txtSInstNo.Focus();
                        return;
                    }

                    //  txtAccNo.Text = "";
                    //  txtAccName.Text = "";
                    // txtCustNo.Text = "";

                    if (rbtnType.SelectedValue == "1")
                        txtNoOfBooks.Focus();
                    else if (rbtnType.SelectedValue == "2")
                        txtIssueDate.Focus();
                    else if (rbtnType.SelectedValue == "3")
                        txtSInstNo.Focus();
                    return;
                }
                else
                {
                    txtBankcode.Text = "";
                    txtBankName.Text = "";
                    txtBankcode.Focus();
                    WebMsgBox.Show("Bank is not operating...!!", this.Page);
                    return;
                }
            }
            else
            {

                txtBankcode.Text = "";
                txtBankName.Text = "";
                txtBankcode.Focus();
                WebMsgBox.Show("Enter valid Bank code...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtBankName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] custnob = txtBankName.Text.ToString().Split('_');
            if (custnob.Length > 1)
            {
                txtBankcode.Text = (string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString());
                sResult = CS.GetProduct(Session["BRCD"].ToString(), txtBankcode.Text.ToString());
                if (sResult != null)
                {
                    if (BD.GetProdOperate(Session["BRCD"].ToString(), txtBankcode.Text.ToString()).ToString() != "3")
                    {
                        string[] ACC = sResult.Split('_');
                        ViewState["GlCode"] = ACC[0].ToString();
                        txtBankName.Text = ACC[2].ToString();
                        AutoAccName.ContextKey = Session["BRCD"].ToString() + "_" + txtBankcode.Text.ToString();
                        sResult = cmn.GetIntACCYN(Session["BRCD"].ToString(), txtBankcode.Text.ToString());

                        if (sResult != "Y")
                        {


                            if (rbtnType.SelectedValue == "1")
                                txtNoOfBooks.Focus();
                            else if (rbtnType.SelectedValue == "2")
                                txtIssueDate.Focus();
                            else if (rbtnType.SelectedValue == "3")
                                txtSInstNo.Focus();
                            return;
                        }

                       
                      if (rbtnType.SelectedValue == "1")
                        txtNoOfBooks.Focus();
                    else if (rbtnType.SelectedValue == "2")
                        txtIssueDate.Focus();
                    else if (rbtnType.SelectedValue == "3")
                        txtAccNo.Focus();
                    return;
                        
                    }
                    else
                    {
                        txtBankcode.Text = "";
                        txtBankName.Text = "";
                        txtBankcode.Focus();
                        WebMsgBox.Show("Bank is not operating...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    txtBankcode.Text = "";
                    txtBankName.Text = "";
                    txtBankcode.Focus();
                    WebMsgBox.Show("Enter valid Bank code...!!", this.Page);
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