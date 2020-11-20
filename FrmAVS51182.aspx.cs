using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAVS51182 : System.Web.UI.Page
{
    DbConnection Conn = new DbConnection();
    ClsBindDropdown DD = new ClsBindDropdown();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsAccountSTS AST = new ClsAccountSTS();
    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }

                TxtAsonDate.Text = Session["EntryDate"].ToString();
                autoglname.ContextKey = Session["BRCD"].ToString();
                TxtFBrID.Text = Session["BRCD"].ToString();
                txtFBrName.Text = AST.GetBranchName(TxtFBrID.Text);
                txtFPrCode.Focus();

                //DD.DdlODActivity(DdlODActivity);
                //DD.DdlODInstActivity(DdlODInstActivity);
                //DD.DdlODLoanActivity(DdlODLoanActivity);
                
                TxtFBrID.Focus();
                //added by ankita 07/10/2017 to make user frndly
                TxtAsonDate.Text = Session["EntryDate"].ToString();
                TxtFBrID.Text = Session["BRCD"].ToString();
            }
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
            if (rbtnType.SelectedValue == "1")
            {
                string redirectURL = "FrmRView.aspx?FBRCD=" + TxtFBrID.Text + "&FPRCD=" + txtFPrCode.Text + "&FAccNo=" + TxtFACID.Text + "&TAccNo=" + TxtTACID.Text + "&Date=" + TxtAsonDate.Text + "&ShrAmt=" + TxtShrAmt.Text + "&DepAmt=" + TxtDepAmt.Text + "&DepPeriod=" + TxtDPPeriod.Text + "&LoanAmt=" + TxtLoanAmt.Text + "&LnPeriod=" + TxtLnvPeriod.Text + "&Flag=" + rbtnType.SelectedValue + "&Atte=" + TxtAttendance.Text + "&AtteMin=" + TxtAtteMin.Text + " &rptname=RptAVS51182.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (rbtnType.SelectedValue == "2")
            {
                string redirectURL = "FrmRView.aspx?FBRCD=" + TxtFBrID.Text + "&FPRCD=" + txtFPrCode.Text + "&FAccNo=" + TxtFACID.Text + "&TAccNo=" + TxtTACID.Text + "&Date=" + TxtAsonDate.Text + "&ShrAmt=" + TxtShrAmt.Text + "&DepAmt=" + TxtDepAmt.Text + "&DepPeriod=" + TxtDPPeriod.Text + "&LoanAmt=" + TxtLoanAmt.Text + "&LnPeriod=" + TxtLnvPeriod.Text + "&Flag=" + rbtnType.SelectedValue + "&Atte=" + TxtAttendance.Text + "&AtteMin=" + TxtAtteMin.Text + " &rptname=RptAVS51182.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (rbtnType.SelectedValue == "3")
            {
                string redirectURL = "FrmRView.aspx?FBRCD=" + TxtFBrID.Text + "&FPRCD=" + txtFPrCode.Text + "&FAccNo=" + TxtFACID.Text + "&TAccNo=" + TxtTACID.Text + "&Date=" + TxtAsonDate.Text + "&ShrAmt=" + TxtShrAmt.Text + "&DepAmt=" + TxtDepAmt.Text + "&DepPeriod=" + TxtDPPeriod.Text + "&LoanAmt=" + TxtLoanAmt.Text + "&LnPeriod=" + TxtLnvPeriod.Text + "&Flag=" + rbtnType.SelectedValue + "&Atte=" + TxtAttendance.Text + "&AtteMin=" + TxtAtteMin.Text + " &rptname=RptAVS51182.rdlc";
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
        ClearData();
    }
    public void ClearData()
    {
        try
        {
            TxtFBrID.Text = "";
            txtFPrCode.Text = "";
            TxtFACID.Text = "";
            TxtTACID.Text = "";
            TxtAsonDate.Text = "";
            TxtShrAmt.Text = "";
            TxtDepAmt.Text = "";
            TxtDPPeriod.Text = "";
            TxtLoanAmt.Text = "";
            TxtLnvPeriod.Text = "";
            TxtAttendance.Text = "";
            TxtAtteMin.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void TxtFBrID_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFBrID.Text != "")
            {
                if (TxtFBrID.Text != "" && (Convert.ToInt32(TxtFBrID.Text) > Convert.ToInt32(TxtFBrID.Text)))
                {
                    WebMsgBox.Show("Invalid FROM and TO Branch Code....!", this.Page);
                    return;
                }

                string bname = AST.GetBranchName(TxtFBrID.Text);
                if (bname != null)
                {
                    txtFBrName.Text = bname;
                    TxtFBrID.Focus();
                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    TxtFBrID.Text = "";
                    TxtFBrID.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                TxtFBrID.Text = "";
                TxtFBrID.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtFPrCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] TD = DD.GetLoanGL(txtFPrCode.Text, Session["BRCD"].ToString()).Split('_');
            if (TD.Length > 1)
            {
                txtFPrName.Text = TD[0].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}