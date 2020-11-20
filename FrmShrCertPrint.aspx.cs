using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmShrCertPrint : System.Web.UI.Page
{
    DataTable DT = new DataTable();
    DataTable DT1 = new DataTable();
    ClsCustomerDetails CD = new ClsCustomerDetails();
    DbConnection conn = new DbConnection();
    scustom cc = new scustom();
    ClsOpenClose OC = new ClsOpenClose();
    Cls_RecoBindDropdown BD1 = new Cls_RecoBindDropdown();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsGetCustomerDT customcs = new ClsGetCustomerDT();
    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindRecDiv();
            }
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DdlRecDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindRecDept();
            DdlRecDept.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindRecDiv()
    {
        try
        {
            BD1.BRCD = Txtfrmbrcd.Text;
            BD1.Ddl = DdlRecDiv;
            BD1.FnBL_BindRecDiv(BD1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindRecDept()
    {
        try
        {
            BD1.BRCD = Txtfrmbrcd.Text;
            BD1.Ddl = DdlRecDept;
            BD1.RECDIV = DdlRecDiv.SelectedValue.ToString();
            BD1.FnBL_BindRecDept(BD1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Txtfrmbrcd_TextChanged(object sender, EventArgs e)
    {

    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (DdlRecDept.SelectedValue.ToString() == "")
            {
                DdlRecDept.SelectedValue = "0";
            }
            if (rbtnRptType.SelectedValue == "1")
            {
                FL = "Insert";//ankita 15/09/2017
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SroRecovry_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                string redirectURL = "FrmRView.aspx?FBRCD=" + Txtfrmbrcd.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&FAccNo=" + TxtFAcc.Text + "&TAccNo=" + TxtTAcc.Text + "&Div=" + DdlRecDiv.SelectedValue.ToString() + "&Dep=" + DdlRecDept.SelectedValue.ToString() + "&rptname=RptShareTZMP.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (rbtnRptType.SelectedValue == "2")
            {
                FL = "Insert";//ankita 15/09/2017
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SroRecovry_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                string redirectURL = "FrmRView.aspx?FBRCD=" + Txtfrmbrcd.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&FAccNo=" + TxtFAcc.Text + "&TAccNo=" + TxtTAcc.Text + "&Div=" + DdlRecDiv.SelectedValue.ToString() + "&Dep=" + DdlRecDept.SelectedValue.ToString() + "&rptname=RptSanchitTZMP.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    public void ClearData()
    {
        Txtfrmbrcd.Text = "";
        //DdlAlS.SelectedValue = "0";
        TxtFDate.Text = "";
        TxtTDate.Text = "";
        TxtFAcc.Text = "";
        TxtTAcc.Text = "";
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void TxtTAcc_TextChanged(object sender, EventArgs e)
    {
        {
            try
            {
                string[] AN;
                AN = customcs.GetAccountName(TxtTAcc.Text, TxtTAccName.Text, Session["BRCD"].ToString()).Split('_');
                if (AN != null)
                {
                    TxtTAccName.Text = AN[1].ToString();
                }
                else
                {
                    WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                    TxtTAcc.Text = "";
                    TxtTAcc.Focus();
                }
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
    }
    protected void TxtTAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtTAccName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtTAccName.Text = custnob[0].ToString();
                TxtTAcc.Text = custnob[1].ToString();
            }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                TxtTAcc.Text = "";
                TxtTAcc.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFAccName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtFAccName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtFAccName.Text = custnob[0].ToString();
                TxtFAcc.Text = custnob[1].ToString();
                TxtFAcc.Focus();
            }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                TxtFAcc.Text = "";
                TxtFAcc.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFAcc_TextChanged(object sender, EventArgs e)
    {
        {
            try
            {
                string[] AN;
                AN = customcs.GetAccountName(TxtFAcc.Text, TxtFAccName.Text, Session["BRCD"].ToString()).Split('_');
                if (AN != null)
                {
                    TxtFAccName.Text = AN[1].ToString();
                    TxtFAcc.Focus();

                }
                else
                {
                    WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                    TxtFAcc.Text = "";
                    TxtFAcc.Focus();
                }

            }
            catch (Exception Ex)
            {

                ExceptionLogging.SendErrorToText(Ex);
            }
        }
    }
    protected void rbtnRptType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}