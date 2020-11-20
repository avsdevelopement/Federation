using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmPTRegister : System.Web.UI.Page
{
    Cls_RecoBindDropdown BD = new Cls_RecoBindDropdown();
    ClsSMSRecovery SMSR = new ClsSMSRecovery();
    //Cls_BL_RDM_WebService RWS = new Cls_BL_RDM_WebService();
    ClsRecoveryGeneration RS = new ClsRecoveryGeneration();
    ClsRecoveryOperation RO = new ClsRecoveryOperation();
    DbConnection Conn = new DbConnection();
    int Res = 0;
    string MM, YY, EDT, STRRes;
    DataTable dt = new DataTable();
    double SumFooterValue = 0, TotalValue = 0;
    string Message = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            BindBr();
            ddlBrCode.SelectedValue = Session["BRCD"].ToString();
            TxtMM.Focus();
        }

        string BKCD = RS.FnBl_GetBANKCode(RS);
        if (BKCD != null)
        {
            ViewState["BANKCODE"] = BKCD;
        }
        else
        {
            ViewState["BANKCODE"] = "0";
        }

        TxtMM.Focus();
    }
    public void ClearData()
    {
        ddlBrCode.SelectedValue = "0";
        TxtMM.Text = "";
        TxtYYYY.Text = "";
        ddlBrCode.Focus();
    }
    public void BindBr()
    {
        BD.Ddl = ddlBrCode;
        BD.FnBL_BindDropDown(BD);
        ddlBrCode.SelectedValue = Session["BRCD"].ToString();// Amruta
        BindRecDiv();
    }

    protected void Btn_Report_Click(object sender, EventArgs e)
    {
        try
        {
            string RptName = "", RecFor = "";
            EDT = Session["EntryDate"].ToString();


            string BKCD = RS.FnBl_GetBANKCode(RS);
            if (BKCD == "1009")
            {
                RptName = "RptPTRegister_1009.rdlc";
            }
            else
            {
                RptName = "RptPTRegister.rdlc";
            }

            string redirectURL = "FrmReportViewer.aspx?FL=Report&BRCD=" + ddlBrCode.SelectedValue.ToString() + "&BANKCD=" + ViewState["BANKCODE"].ToString() + "&ASONDATE=" + EDT.ToString() + "&UID=" + Session["UserName"].ToString() + "&MM=" + TxtMM.Text + "&YY=" + TxtYYYY.Text + "&Div=" + DdlRecDiv.SelectedValue.ToString() + "&Dep=" + DdlRecDept.SelectedValue.ToString() + "&rptname=" + RptName + "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_ClearAll_Click(object sender, EventArgs e)
    {

    }
    
    protected void TxtMM_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int Len = 0;
            Len = TxtMM.Text.Length;
            if (Len > 2 || Convert.ToInt32(TxtMM.Text) > 12)
            {
                WebMsgBox.Show("Inavlid month, Enter valid month....!", this.Page);
                TxtMM.Text = "";
                TxtMM.Focus();
            }
            else
            {
                TxtYYYY.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtYYYY_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int Len = 0;
            Len = TxtYYYY.Text.Length;
            if (Len != 4)
            {
                WebMsgBox.Show("Inavlid month, Enter valid month....!", this.Page);
                TxtYYYY.Text = "";
                TxtYYYY.Focus();
            }
            else
            {
                Btn_Report.Focus();
            }
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

    public void BindRecDept()
    {
        BD.BRCD = ddlBrCode.SelectedValue.ToString();
        BD.Ddl = DdlRecDept;
        BD.RECDIV = DdlRecDiv.SelectedValue.ToString();
        BD.FnBL_BindRecDept(BD);
    }
    public void BindRecDiv()
    {
        BD.BRCD = ddlBrCode.SelectedValue.ToString();
        BD.Ddl = DdlRecDiv;
        BD.FnBL_BindRecDiv(BD);
    }
}