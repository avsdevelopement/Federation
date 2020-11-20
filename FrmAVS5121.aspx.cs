using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class FrmAVS5121 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAVS5121 PO = new ClsAVS5121();
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
                AutoGlName.ContextKey = Session["BRCD"].ToString();
                BindBranch(ddlBrName);

                ddlBrName.Focus();
            }
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Select index Change

    protected void rbtnType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlBrName.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlBrName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtBrCode.Text = "";
            txtBrCode.Text = ddlBrName.SelectedValue.ToString();
            AutoGlName.ContextKey = txtBrCode.Text.Trim().ToString();

            txtProdType.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Text Change Event

    protected void txtProdType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            sResult = PO.GetProduct(Session["BRCD"].ToString(), txtProdType.Text.ToString());

            if (sResult != null)
            {
                if (BD.GetProdOperate(Session["BRCD"].ToString(), txtProdType.Text.ToString()).ToString() != "3")
                {
                    string[] ACC = sResult.Split('_'); ;

                    if (ACC[0].ToString() == "3")
                    {
                        ViewState["GlCode"] = ACC[0].ToString();
                        txtProdName.Text = ACC[2].ToString();

                        txtEntryDate.Focus();
                    }
                    else
                    {
                        txtProdType.Text = "";
                        txtProdName.Text = "";
                        ViewState["GlCode"] = "0";
                        txtProdType.Focus();
                        WebMsgBox.Show("Allow only for loan account type...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    txtProdType.Text = "";
                    txtProdType.Focus();
                    WebMsgBox.Show("Product is not operating...!!", this.Page);
                    return;
                }
            }
            else
            {
                txtProdType.Text = "";
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
                if (BD.GetProdOperate(Session["BRCD"].ToString(), string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString()).ToString() != "3")
                {
                    if (custnob[1].ToString() == "3")
                    {
                        txtProdName.Text = custnob[0].ToString();
                        ViewState["GlCode"] = custnob[1].ToString();
                        txtProdType.Text = (string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString());

                        txtEntryDate.Focus();
                    }
                    else
                    {
                        txtProdType.Text = "";
                        txtProdName.Text = "";
                        ViewState["GlCode"] = "0";
                        txtProdType.Focus();
                        WebMsgBox.Show("Allow only for loan account type...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    txtProdType.Text = "";
                    txtProdType.Focus();
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

    #endregion

    #region Public Function

    public void BindBranch(DropDownList ddlBrName)
    {
        try
        {
            BD.BindBRANCHNAME(ddlBrName, null);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #endregion

    #region Click Event

    protected void btnTrail_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtBrCode.Text.ToString() == "")
            {
                ddlBrName.Focus();
                WebMsgBox.Show("select branch first...!!", this.Page);
                return;
            }
            else if (txtProdType.Text.ToString() == "")
            {
                txtProdType.Focus();
                WebMsgBox.Show("Enter product code first...!!", this.Page);
                return;
            }
            else if (txtEntryDate.Text.ToString() == "")
            {
                txtEntryDate.Focus();
                WebMsgBox.Show("Enter entry date first...!!", this.Page);
                return;
            }
            else
            {
                if (rbtnType.SelectedValue == "1")
                {
                    Result = PO.NPAMarking1(txtBrCode.Text.ToString(), txtProdType.Text.ToString(), txtEntryDate.Text.ToString(), Session["MID"].ToString());
                }
                else if (rbtnType.SelectedValue == "2")
                {
                    Result = PO.NPAMarking2(txtBrCode.Text.ToString(), txtProdType.Text.ToString(), txtEntryDate.Text.ToString(), Session["MID"].ToString());
                }

                if (Result > 0)
                {
                    ClearAll();
                    WebMsgBox.Show("NPA marking successfully done", this.Page);
                    return;
                }
            }
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

    #region Function

    public void ClearAll()
    {
        try
        {
            ddlBrName.SelectedValue = "0";
            txtBrCode.Text = "";
            txtProdType.Text = "";
            txtProdName.Text = "";
            txtEntryDate.Text = "";

            ddlBrName.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

}