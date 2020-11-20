using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmSharesAppliaction : System.Web.UI.Page
{
    DbConnection Conn = new DbConnection();
    ClsShareMember SA = new ClsShareMember();
    ClsBindDropdown DD = new ClsBindDropdown();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            BindBranch(ddlFromBrName);
            BindBranch(ddlToBrName);
            DD.BindSHRActivtyType(DdlActivityType);
            DD.BindSHRActivty(DdlActivity);
            ddlFromBrName.Focus();
        }
    }

    public void BindBranch(DropDownList ddlBrName)
    {
        try
        {
            DD.BindBRANCHNAME(ddlBrName, null);
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
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Share_App_Report _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?FBrCode=" + txtFrBranch.Text.Trim().ToString() + "&TBrCode=" + txtToBranch.Text.Trim().ToString() + "&PrCode=" + txtProdType.Text.Trim().ToString() + "&FDate=" + TxtFDate.Text.ToString() + "&TDate=" + TxtTDate.Text.ToString() + "&AppType=" + DdlActivityType.SelectedValue + "&AType=" + DdlActivity.SelectedValue + "&rptname=RptSharesApplicationList.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
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
            if (DD.GetProdOperate(txtFrBranch.Text.Trim().ToString(), txtProdType.Text.Trim().ToString()).ToString() != "3")
            {
                string AC1 = SA.Getaccno(txtProdType.Text, txtFrBranch.Text.Trim().ToString());

                if (AC1 != null)
                {
                    string[] AC = AC1.Split('_'); ;
                    txtProdName.Text = AC[1].ToString();
                    TxtFDate.Focus();
                    return;
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
            else
            {
                txtProdType.Text = "";
                txtProdName.Text = "";
                txtProdType.Focus();
                WebMsgBox.Show("Product is not operating...!!", this.Page);
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
            string CUNAME = txtProdName.Text;
            string[] custnob = CUNAME.Split('_');

            if (DD.GetProdOperate(txtFrBranch.Text.Trim().ToString(), (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString())).ToString() != "3")
            {
                txtProdName.Text = custnob[0].ToString();
                txtProdType.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                TxtFDate.Focus();
                return;
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
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    
    protected void ddlFromBrName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtFrBranch.Text = "";
            txtFrBranch.Text = ddlFromBrName.SelectedValue.ToString();
            autoglname.ContextKey = txtFrBranch.Text.Trim().ToString();
            ddlToBrName.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlToBrName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtToBranch.Text = "";
            txtToBranch.Text = ddlToBrName.SelectedValue.ToString();
            txtProdType.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}