using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmLrAndNr : System.Web.UI.Page
{
    Cls_RecoBindDropdown BD = new Cls_RecoBindDropdown();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBr();
            TxtMM.Focus();
        }

    }
    public void ClearData()
    {
        ddlBrCode.SelectedValue = Session["BRCD"].ToString();
        TxtMM.Text = "";
        TxtYYYY.Text = "";
        DdlRecDept.SelectedValue = "0";
        ddlBrCode.Focus();
    }
    public void BindBr()
    {
        BD.Ddl = ddlBrCode;
        BD.RECDIV = DdlRecDiv.SelectedValue.ToString();
        BD.RECCODE = DdlRecDiv.SelectedValue.ToString();
        BD.FnBL_BindDropDown(BD);
        ddlBrCode.SelectedValue = Session["BRCD"].ToString();
        BindRecDiv();
        BindRecDept();
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
                DdlRecDiv.Focus();

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

    protected void ddlBrCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DdlRecDiv.SelectedValue = "1";
            BindRecDiv();
            BindRecDept();
            TxtMM.Focus();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_ShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            string RecFor = "";
            RecFor ="Div -"+ DdlRecDiv.SelectedItem.Text + " - Dept - " + DdlRecDept.SelectedItem.Text;

            string redirectURL = "FrmRecRView.aspx?FL=LRNR&RECFOR=" + RecFor + "&BRCD=" + ddlBrCode.SelectedValue.ToString() + "&ASONDATE=" + Session["EntryDate"].ToString() + "&RECDIV=" + DdlRecDiv.SelectedValue.ToString() + "&RECCODE=" + DdlRecDept.SelectedValue.ToString() + "&UID=" + Session["UserName"].ToString() + "&MM=" + TxtMM.Text + "&YY=" + TxtYYYY.Text + "&rptname=RptLrAndNr.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {

    }
}