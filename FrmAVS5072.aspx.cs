using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class FrmAVS5072 : System.Web.UI.Page
{

    ClsRecoveryGeneration RS = new ClsRecoveryGeneration();
    Cls_RecoBindDropdown BD = new Cls_RecoBindDropdown();
    
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
            string BKCD = RS.FnBl_GetBANKCode(RS);
            if (BKCD != null)
            {
                ViewState["BANKCODE"] = BKCD;
            }
            else
            {
                ViewState["BANKCODE"] = "0";
            }


            TxtFMM.Focus();
        }
    }
    public void BindBr()
    {
        BD.Ddl = ddlBrCode;
        BD.FnBL_BindDropDown(BD);
        ddlBrCode.SelectedValue = Session["BRCD"].ToString();// Amruta 
    }
   
    protected void Btn_Report_Click(object sender, EventArgs e)
    {
        string RptName = "", RecFor = "";
        EDT = Session["EntryDate"].ToString();


        string BKCD = RS.FnBl_GetBANKCode(RS);
        if (BKCD == "1009")
        {
            RptName = "RptPendingRec_1009.rdlc";
        }
        else
        {
            RptName = "RptPendingRec.rdlc";
        }

        string redirectURL = "FrmReportViewer.aspx?FL=Report&BRCD=" + ddlBrCode.SelectedValue.ToString() + "&ASONDATE=" + EDT.ToString() + "&UID=" + Session["UserName"].ToString() + "&FMM=" + TxtFMM.Text + "&FYY=" + TxtFYYYY.Text + "&TMM=" + TxtTMM.Text + "&TYY=" + TxtTYYYY.Text + "&rptname=" + RptName + "";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {

    }
    protected void ddlBrCode_TextChanged(object sender, EventArgs e)
    {

    }
    protected void TxtFMM_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int Len = 0;
            Len = TxtFMM.Text.Length;
            if (Len > 2 || Convert.ToInt32(TxtFMM.Text) > 12)
            {
                WebMsgBox.Show("Inavlid month, Enter valid month....!", this.Page);
                TxtFMM.Text = "";
                TxtFMM.Focus();
            }
            else
            {
                TxtFYYYY.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFYYYY_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int Len = 0;
            Len = TxtFYYYY.Text.Length;
            if (Len != 4)
            {
                WebMsgBox.Show("Inavlid Year, Enter valid Year....!", this.Page);
                TxtFYYYY.Text = "";
                TxtFYYYY.Focus();
            }
            else
            {
                TxtTMM.Focus();

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTMM_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int Len = 0;
            Len = TxtTMM.Text.Length;
            if (Len > 2 || Convert.ToInt32(TxtTMM.Text) > 12)
            {
                WebMsgBox.Show("Inavlid month, Enter valid month....!", this.Page);
                TxtTMM.Text = "";
                TxtTMM.Focus();
            }
            else if (Convert.ToInt32(TxtFMM.Text) > Convert.ToInt32(TxtTMM.Text))
            {
                WebMsgBox.Show("To Month should be greater than from month...!", this.Page);
                TxtTMM.Text = "";
                TxtFMM.Text = "";
                TxtFYYYY.Text = "";
                TxtTYYYY.Text = "";
                TxtFMM.Focus();
            }
            else
            {
                TxtTYYYY.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTYYYY_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int Len = 0;
            Len = TxtFYYYY.Text.Length;
            if (Len != 4)
            {
                WebMsgBox.Show("Inavlid Year, Enter valid Year....!", this.Page);
                TxtTYYYY.Text = "";
                TxtTYYYY.Focus();
            }
            else if (Convert.ToInt32(TxtFYYYY.Text) != Convert.ToInt32(TxtTYYYY.Text))
            {
                WebMsgBox.Show("Invalid To Year, From year and To Year Must be same...!", this.Page);
                TxtTYYYY.Text = "";
                TxtTYYYY.Focus();
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
}