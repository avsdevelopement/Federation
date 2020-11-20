using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmSBIntCalcReport : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsSBIntCalculation SB = new ClsSBIntCalculation();
    string sResult = "";
    int Result = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtBrCode.Text = Session["BRCD"].ToString();
                AutoGlName.ContextKey = txtBrCode.Text.ToString();
                txtBrName.Text = SB.GetBranchName(txtBrCode.Text.ToString());

                txtProdType.Focus();
            }
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
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
            if (txtBrCode.Text.ToString() == "")
            {
                txtBrCode.Focus();
                WebMsgBox.Show("Enter branch code first...!!", this.Page);
                return;
            }
            else if (txtProdType.Text.ToString() == "")
            {
                txtProdType.Focus();
                WebMsgBox.Show("Enter product code first...!!", this.Page);
                return;
            }
            else if (TxtFDate.Text.ToString() == "")
            {
                TxtFDate.Focus();
                WebMsgBox.Show("Enter From Date first...!!", this.Page);
                return;
            }
            else if (TxtTDate.Text.ToString() == "")
            {
                TxtTDate.Focus();
                WebMsgBox.Show("Enter To date Fisrt...!!", this.Page);
                return;
            }
            else if (Rdeatils.SelectedValue == "1")
            {
                string redirectURL = "FrmRView.aspx?BC=" + txtBrCode.Text.Trim().ToString() + "&PC=" + txtProdType.Text.Trim().ToString() + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&MID=" + Session["MID"].ToString() + "&FL=" + "D" + "&rptname=RptSBIntCalcReport_DT.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (Rdeatils.SelectedValue == "2")
            {
                string redirectURL = "FrmRView.aspx?BC=" + txtBrCode.Text.Trim().ToString() + "&PC=" + txtProdType.Text.Trim().ToString() + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&MID=" + Session["MID"].ToString() + "&FL=" + "S" + "&rptname=RptSBIntCalcReport_Sumry.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
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
    protected void txtBrCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtBrCode.Text.ToString() != "")
            {
                string BrName = SB.GetBranchName(txtBrCode.Text.ToString());
                if (BrName != null || BrName != "")
                {
                    ClearProdText();
                    AutoGlName.ContextKey = txtBrCode.Text.ToString();
                    txtBrName.Text = BrName;

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
                ClearAllData();
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
            sResult = SB.GetProduct(txtBrCode.Text.ToString(), txtProdType.Text.ToString());

            if (sResult != null)
            {
                if (BD.GetProdOperate(txtBrCode.Text.ToString(), txtProdType.Text.ToString()).ToString() != "3")
                {
                    string[] ACC = sResult.Split('_'); ;
                    ViewState["GlCode"] = ACC[0].ToString();
                    txtProdName.Text = ACC[2].ToString();

                    TxtFDate.Focus();
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

                    TxtFDate.Focus();
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
    public void ClearProdText()
    {
        try
        {
            txtProdType.Text = "";
            txtProdName.Text = "";
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
            txtBrName.Text = SB.GetBranchName(txtBrCode.Text.ToString());
            txtProdType.Text = "";
            txtProdName.Text = "";
            TxtFDate.Text = Session["EntryDate"].ToString();
            TxtTDate.Text = Session["EntryDate"].ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnParameter_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtBrCode.Text.ToString() == "")
            {
                txtBrCode.Focus();
                WebMsgBox.Show("Enter branch code first...!!", this.Page);
                return;
            }
            else if (txtProdType.Text.ToString() == "")
            {
                txtProdType.Focus();
                WebMsgBox.Show("Enter product code first...!!", this.Page);
                return;
            }
            else if (TxtFDate.Text.ToString() == "")
            {
                TxtFDate.Focus();
                WebMsgBox.Show("Enter From Date first...!!", this.Page);
                return;
            }
            else if (TxtTDate.Text.ToString() == "")
            {
                TxtTDate.Focus();
                WebMsgBox.Show("Enter To date Fisrt...!!", this.Page);
                return;
            }
            else
            {
                string redirectURL = "FrmRView.aspx?BC=" + txtBrCode.Text.Trim().ToString() + "&PC=" + txtProdType.Text.Trim().ToString() + "&rptname=RptSB_INTCalcPara.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}