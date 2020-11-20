using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AVS5050 : System.Web.UI.Page
{
    ClsAVS5050 BD = new ClsAVS5050();
    ClsFDIntCalculation FD = new ClsFDIntCalculation();
    ClsAccountSTS AST = new ClsAccountSTS();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string sResult = "", Skip = "";
    int result = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            TxtFPRD.Focus();
            txtFDate.Text = Session["EntryDate"].ToString();
            txtTDate.Text = Session["EntryDate"].ToString();
            txtBrCode.Text = Session["BRCD"].ToString();
            txtbrName.Text = AST.GetBranchName(txtBrCode.Text);
        }
        ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
    }

    protected void TxtFBRCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtBrCode.Text != "")
            {
                string bname = AST.GetBranchName(txtBrCode.Text.Trim().ToString());
                if (bname != null)
                {
                    txtbrName.Text = bname;
                    TxtFPRD.Focus();
                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    txtBrCode.Text = "";
                    txtBrCode.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                txtBrCode.Text = "";
                txtBrCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtFPRD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //if (TxtTPRD.Text != "" && (Convert.ToInt32(TxtFPRD.Text.Trim().ToString()) > Convert.ToInt32(TxtTPRD.Text.Trim().ToString())))
            //{
            //    WebMsgBox.Show("Invalid From And To Product code....!", this.Page);
            //    TxtFPRD.Text = "";
            //    TxtTPRD.Text = "";
            //    TxtFPRD.Focus();
            //    return;
            //}
            //string TD1 = BD.GetDepoGL(TxtFPRD.Text.Trim().ToString(), txtBrCode.Text.Trim().ToString());
            //if (TD1 != null)
            //{
            //    string[] TD = TD1.Split('_');
            //    if (TD.Length > 1)
            //    {
                    TxtTPRD.Focus();
            //    }
            //}
            //else
            //{
            //    WebMsgBox.Show("Invalid Product code...!", this.Page);
            //    TxtFPRD.Text = "";
            //    TxtFPRD.Focus();
            //    return;
            //}
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtTPRD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //if ((Convert.ToInt32(TxtFPRD.Text.Trim().ToString()) > Convert.ToInt32(TxtTPRD.Text.Trim().ToString())))
            //{
            //    WebMsgBox.Show("Invalid From And To Product code....!", this.Page);
            //    TxtTPRD.Text = "";
            //    TxtTPRD.Focus();
            //    return;
            //}
            //string TD1 = BD.GetDepoGL(TxtTPRD.Text.Trim().ToString(), txtBrCode.Text.Trim().ToString());
            //if (TD1 != null)
            //{
            //    string[] TD = TD1.Split('_');

            //    if (TD.Length > 1)
            //    {
                    TxtFAcc.Focus();
            //    }
            //}
            //else
            //{
            //    WebMsgBox.Show("Invalid Product code...!", this.Page);
            //    TxtTPRD.Text = "";
            //    TxtTPRD.Focus();
            //    return;
            //}
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtFAcc_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtTAcc.Text != "" && (Convert.ToInt32(TxtFAcc.Text) > Convert.ToInt32(TxtTAcc.Text)))
            {
                WebMsgBox.Show("Invalid From And To Account Number....!", this.Page);
                TxtFAcc.Text = "";
                TxtTAcc.Text = "";
                TxtFAcc.Focus();
                return;
            }
            TxtTAcc.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtTAcc_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(TxtFAcc.Text) > Convert.ToInt32(TxtTAcc.Text))
            {
                WebMsgBox.Show("Invalid FROM and TO Account Number....!", this.Page);
                TxtFAcc.Text = "";
                TxtTAcc.Text = "";
                TxtFAcc.Focus();
                return;
            }
            else
            {
                txtFDate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void Clear()
    {
        txtBrCode.Text = "";
        txtbrName.Text = "";
        TxtTPRD.Text = "";
        TxtFPRD.Text = "";
        TxtFAcc.Text = "";
        TxtTAcc.Text = "";
        txtFDate.Text = Session["EntryDate"].ToString();
        txtTDate.Text = Session["EntryDate"].ToString();
        txtBrCode.Focus();
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        try
        {
            result = BD.CalculateDDSInt(txtBrCode.Text.Trim().ToString(), TxtFPRD.Text.Trim().ToString(), TxtTPRD.Text.Trim().ToString(), TxtFAcc.Text.Trim().ToString(), TxtTAcc.Text.Trim().ToString(), txtFDate.Text.ToString(), txtTDate.Text.ToString(), Session["MID"].ToString(), "Calc");
            if (result > 0)
            {
                WebMsgBox.Show("Successfully Calculated....!", this.Page);
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
            string redirectURL = "FrmReportViewer.aspx?BRCD=" + txtBrCode.Text.Trim().ToString() + "&FPRCD=" + TxtFPRD.Text.Trim().ToString() + "&TPRCD=" + TxtTPRD.Text.Trim().ToString() + "&FAccNo=" + TxtFAcc.Text.Trim().ToString() + "&TAccNo=" + TxtTAcc.Text.Trim().ToString() + "&FDate=" + txtFDate.Text.ToString() + "&TDate=" + txtTDate.Text.ToString() + "&MID=" + Session["MID"].ToString() + "&FLAG=Report&rptname=RptAVS5050.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnApplyInt_Click(object sender, EventArgs e)
    {
        try
        {
            sResult = BD.ApplyDDSInt(txtBrCode.Text.Trim().ToString(), TxtFPRD.Text.Trim().ToString(), TxtTPRD.Text.Trim().ToString(), TxtFAcc.Text.Trim().ToString(), TxtTAcc.Text.Trim().ToString(), txtFDate.Text.ToString(), txtTDate.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), "POST");
            if (Convert.ToInt32(sResult) > 0)
            {
                Clear();
                WebMsgBox.Show("Successfully Post With SetNo : " + sResult + "....!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnClearAll_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
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

}