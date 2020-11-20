using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAVS5147 : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAVS5147 FC = new ClsAVS5147();
    string sResult = "";
    int Result = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtEDate.Focus();
        }
        ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
    }

    protected void txtDrProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (BD.GetProdOperate(txtFBrCode.Text.ToString(), txtDrProdCode.Text.ToString()).ToString() != "3")
            {
                sResult = FC.GetProduct(txtFBrCode.Text.ToString(), txtDrProdCode.Text.ToString());

                if (sResult != null)
                {
                    string[] ACC = sResult.Split('_'); ;
                    txtDrProdCode.Text = ACC[1].ToString();
                    txtDrProdName.Text = ACC[2].ToString();
                    AutoDrGlName.ContextKey = txtFBrCode.Text.ToString() + "_" + txtDrProdCode.Text.ToString();
                    AutoCrGlName.ContextKey = txtFBrCode.Text.ToString() + "_" + txtDrProdCode.Text.ToString();

                    txtCrProdCode.Focus();
                }
                else
                {
                    txtDrProdCode.Text = "";
                    txtDrProdName.Text = "";
                    txtDrProdCode.Focus();
                    WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                    return;
                }
            }
            else
            {
                txtDrProdCode.Text = "";
                txtDrProdName.Text = "";
                txtDrProdCode.Focus();
                WebMsgBox.Show("Product is not operating...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtDrProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] custnob = txtDrProdName.Text.ToString().Split('_');
            if (custnob.Length > 1)
            {
                if (BD.GetProdOperate(txtFBrCode.Text.ToString(), string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString()).ToString() != "3")
                {
                    txtDrProdName.Text = custnob[0].ToString();
                    txtDrProdCode.Text = (string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString());

                    txtCrProdCode.Focus();
                }
                else
                {
                    txtDrProdCode.Text = "";
                    txtDrProdName.Text = "";
                    txtDrProdCode.Focus();
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

    protected void txtCrProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (BD.GetProdOperate(txtFBrCode.Text.ToString(), txtCrProdCode.Text.ToString()).ToString() != "3")
            {
                sResult = FC.GetProduct(txtFBrCode.Text.ToString(), txtCrProdCode.Text.ToString());

                if (sResult != null)
                {
                    string[] ACC = sResult.Split('_'); ;
                    txtCrProdCode.Text = ACC[1].ToString();
                    txtCrProdName.Text = ACC[2].ToString();
                    AutoDrGlName.ContextKey = txtFBrCode.Text.ToString() + "_" + txtCrProdCode.Text.ToString();
                    AutoCrGlName.ContextKey = txtFBrCode.Text.ToString() + "_" + txtCrProdCode.Text.ToString();

                    txtNarration.Focus();
                }
                else
                {
                    txtCrProdCode.Text = "";
                    txtCrProdName.Text = "";
                    txtCrProdCode.Focus();
                    WebMsgBox.Show("Enter valid Product code...!!", this.Page);
                    return;
                }
            }
            else
            {
                txtCrProdCode.Text = "";
                txtCrProdName.Text = "";
                txtCrProdCode.Focus();
                WebMsgBox.Show("Product is not operating...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtCrProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] custnob = txtCrProdName.Text.ToString().Split('_');
            if (custnob.Length > 1)
            {
                if (BD.GetProdOperate(txtFBrCode.Text.ToString(), string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString()).ToString() != "3")
                {
                    txtCrProdName.Text = custnob[0].ToString();
                    txtCrProdCode.Text = (string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString());

                    txtNarration.Focus();
                }
                else
                {
                    txtCrProdCode.Text = "";
                    txtCrProdName.Text = "";
                    txtCrProdCode.Focus();
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

    protected void btnTrail_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtEDate.Text.ToString() == "")
            {
                txtEDate.Focus();
                WebMsgBox.Show("Enter posting date first ...!!", this.Page);
                return;
            }
            else if (txtEDate.Text.ToString() != Session["EntryDate"].ToString())
            {
                txtEDate.Focus();
                WebMsgBox.Show("Not allow for less than working date ...!!", this.Page);
                return;
            }
            else if (txtCharges.Text.ToString() == "")
            {
                txtCharges.Focus();
                WebMsgBox.Show("Enter applied charges first ...!!", this.Page);
                return;
            }
            else if (txtFBrCode.Text.ToString() == "")
            {
                txtFBrCode.Focus();
                WebMsgBox.Show("Enter from branch code first ...!!", this.Page);
                return;
            }
            else if (txtTBrCode.Text.ToString() == "")
            {
                txtTBrCode.Focus();
                WebMsgBox.Show("Enter to branch code first ...!!", this.Page);
                return;
            }
            else if (txtDrProdCode.Text.ToString() == "")
            {
                txtDrProdCode.Focus();
                WebMsgBox.Show("Enter debit product code first ...!!", this.Page);
                return;
            }
            else if (txtCrProdCode.Text.ToString() == "")
            {
                txtCrProdCode.Focus();
                WebMsgBox.Show("Enter credit product code first ...!!", this.Page);
                return;
            }
            else
            {
                string redirectURL = "FrmReportViewer.aspx?FB=" + txtFBrCode.Text + "&TB=" + txtTBrCode.Text + "&DP=" + txtDrProdCode.Text + "&CP=" + txtCrProdCode.Text + "&CHG=" + txtCharges.Text + "&Part=" + txtNarration.Text + "&ED=" + txtEDate.Text.ToString() + "&rptname=RptAVS5147.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
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
            if (txtEDate.Text.ToString() == "")
            {
                txtEDate.Focus();
                WebMsgBox.Show("Enter posting date first ...!!", this.Page);
                return;
            }
            else if (txtEDate.Text.ToString() != Session["EntryDate"].ToString())
            {
                txtEDate.Focus();
                WebMsgBox.Show("Not allow for less than working date ...!!", this.Page);
                return;
            }
            else if (txtCharges.Text.ToString() == "")
            {
                txtCharges.Focus();
                WebMsgBox.Show("Enter applied charges first ...!!", this.Page);
                return;
            }
            else if (txtFBrCode.Text.ToString() == "")
            {
                txtFBrCode.Focus();
                WebMsgBox.Show("Enter from branch code first ...!!", this.Page);
                return;
            }
            else if (txtTBrCode.Text.ToString() == "")
            {
                txtTBrCode.Focus();
                WebMsgBox.Show("Enter to branch code first ...!!", this.Page);
                return;
            }
            else if (txtDrProdCode.Text.ToString() == "")
            {
                txtDrProdCode.Focus();
                WebMsgBox.Show("Enter debit product code first ...!!", this.Page);
                return;
            }
            else if (txtCrProdCode.Text.ToString() == "")
            {
                txtCrProdCode.Focus();
                WebMsgBox.Show("Enter credit product code first ...!!", this.Page);
                return;
            }
            else
            {
                Result = FC.PassVoucher(txtFBrCode.Text.ToString(), txtTBrCode.Text.ToString(), txtDrProdCode.Text.ToString(), txtCharges.Text.ToString(),
                    txtCrProdCode.Text.ToString(), txtNarration.Text.ToString(), txtEDate.Text.ToString(), Session["MID"].ToString());
                if (Result > 0)
                {
                    ClearAllData();
                    WebMsgBox.Show("Successfully applied ...!!", this.Page);
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

    public void ClearAllData()
    {
        try
        {
            txtEDate.Text = "";
            txtCharges.Text = "";
            txtFBrCode.Text = "";
            txtTBrCode.Text = "";

            txtDrProdCode.Text = "";
            txtDrProdName.Text = "";
            txtCrProdCode.Text = "";
            txtCrProdName.Text = "";
            txtNarration.Text = "";

            txtEDate.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

}