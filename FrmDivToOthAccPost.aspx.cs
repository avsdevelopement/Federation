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

public partial class FrmDivToOthAccPost : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    Cls_RecoBindDropdown RBD = new Cls_RecoBindDropdown();
    ClsRecoveryStatement RS = new ClsRecoveryStatement();
    ClsDividentPayTran DP = new ClsDividentPayTran();
    ClsBindDropdown BD = new ClsBindDropdown();
    Cls_RecoBindDropdown BD1 = new Cls_RecoBindDropdown();
    DataTable DT = new DataTable();
    string sResult = "";
    int Result = 0;
    string STR = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtBrCode.Text = Session["BRCD"].ToString();
                AutoGlName.ContextKey = txtBrCode.Text.ToString();
                txtBrName.Text = DP.GetBranchName(txtBrCode.Text.ToString());
                txtAsOnDate.Text = Session["EntryDate"].ToString();
                txtBrCode.Focus();
            }
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #region Text Change Events

    protected void txtBrCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtBrCode.Text.ToString() != "")
            {
                string BrName = DP.GetBranchName(txtBrCode.Text.ToString());
                if (BrName != null || BrName != "")
                {
                    AutoGlName.ContextKey = txtBrCode.Text.ToString();
                    txtBrName.Text = BrName;

                    txtProdCode.Focus();
                    return;
                }
                else
                {
                    txtBrName.Text = "";
                    txtBrCode.Text = "";
                    txtBrCode.Focus();
                    WebMsgBox.Show("Enter valid branch code...!!", this.Page);
                    return;
                }
            }
            else
            {
                txtBrName.Text = "";
                txtBrCode.Text = "";
                txtBrCode.Focus();
                WebMsgBox.Show("Enter branch code first...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            sResult = DP.GetProduct(txtBrCode.Text.ToString(), txtProdCode.Text.ToString());

            if (sResult != null)
            {
                if (BD.GetProdOperate(txtBrCode.Text.ToString(), txtProdCode.Text.ToString()).ToString() != "3")
                {
                    string[] ACC = sResult.Split('_'); ;
                    ViewState["GlCode"] = ACC[0].ToString();
                    txtProdName.Text = ACC[2].ToString();

                    txtAsOnDate.Focus();
                }
                else
                {
                    txtProdCode.Text = "";
                    txtProdName.Text = "";
                    txtProdCode.Focus();
                    WebMsgBox.Show("Product is not operating...!!", this.Page);
                    return;
                }
            }
            else
            {
                txtProdCode.Text = "";
                txtProdName.Text = "";
                txtProdCode.Focus();
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
                if (BD.GetProdOperate(txtBrCode.Text.ToString(), string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString()).ToString() != "3")
                {
                    txtProdName.Text = custnob[0].ToString();
                    ViewState["GlCode"] = custnob[1].ToString();
                    txtProdCode.Text = (string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString());

                    txtAsOnDate.Focus();
                }
                else
                {
                    txtProdCode.Text = "";
                    txtProdName.Text = "";
                    txtProdCode.Focus();
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

    #region Click Event

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtBrCode.Text.ToString() == "")
            {
                txtBrCode.Focus();
                WebMsgBox.Show("Enter Branch Code First", this.Page);
                return;
            }
            else if (txtProdCode.Text.ToString() == "")
            {
                txtProdCode.Focus();
                WebMsgBox.Show("Enter Procudt Code First", this.Page);
                return;
            }
            else if (txtAsOnDate.Text.ToString() == "")
            {
                txtAsOnDate.Focus();
                WebMsgBox.Show("Enter Date", this.Page);
                return;
            }
            else if (txtProdCode2.Text.ToString() == "")
            {
                txtProdCode2.Focus();
                WebMsgBox.Show("Enter Cr Product Code ...!!", this.Page);
                return;
            }
            else
            {
                string redirectURL = "FrmRView.aspx?FDate=" + txtAsOnDate.Text + "&BRCD=" + txtBrCode.Text + "&Product=" + txtProdCode.Text + "&CrProduct=" + txtProdCode2.Text + "&MID=" + Session["MID"].ToString() + "&rptname=RptDividentTRFProcess.rdlc"; 
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnPost_Click(object sender, EventArgs e)
    {
        try
        {
            int tmp = 0;
            string SetNo = "", RefId = "";

            if (txtBrCode.Text.ToString() == "")
            {
                txtBrCode.Focus();
                WebMsgBox.Show("Enter Branch Code First", this.Page);
                return;
            }
            else if (txtProdCode.Text.ToString() == "")
            {
                txtProdCode.Focus();
                WebMsgBox.Show("Enter Procudt Code First", this.Page);
                return;
            }
            else if (txtAsOnDate.Text.ToString() == "")
            {
                txtAsOnDate.Focus();
                WebMsgBox.Show("Enter Date", this.Page);
                return;
            }
            else if (txtProdCode2.Text.ToString() == "")
            {
                txtProdCode2.Focus();
                WebMsgBox.Show("Enter Cr Product Code ...!!", this.Page);
                return;
            }
            else
            {
                //DT = CreateFirst(DT);
                RefId = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                ViewState["RefId"] = (Convert.ToInt32(RefId) + 1).ToString();

                STR = DP.DivToOtherAccData(txtBrCode.Text.ToString(), txtProdCode.Text, txtProdCode2.Text.ToString(), txtAsOnDate.Text.ToString(), Session["MID"].ToString(), "Final");

                if (STR != null)
                {
                    ClearAllData();
                    txtProdCode.Focus();
                    WebMsgBox.Show("Voucher Post Successfully With Set No - " + STR, this.Page);
                    CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Divident_Transfer_SetNo" + SetNo + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
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

    #endregion

    #region Function
    
    public void ClearAllData()
    {
        try
        {
            btnSubmit.Visible = true;
            btnPost.Visible = false;

            txtBrCode.Text = Session["BRCD"].ToString();
            AutoGlName.ContextKey = txtBrCode.Text.ToString();
            txtBrName.Text = DP.GetBranchName(txtBrCode.Text.ToString());
            txtProdCode.Text = "";
            txtProdName.Text = "";
            txtAsOnDate.Text = Session["EntryDate"].ToString();
            txtProdCode2.Text = "";
            txtBrCode.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion
    protected void txtProdname2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtProdCode2_TextChanged(object sender, EventArgs e)
    {

    }
}