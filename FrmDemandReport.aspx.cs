using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmDemandReport : System.Web.UI.Page
{
    Cls_RecoBindDropdown BD = new Cls_RecoBindDropdown();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBr();
        }
    }
    public void BindBr()
    {
        BD.Ddl = DdlFromBrCode;
        BD.FnBL_BindDropDown(BD);
        DdlFromBrCode.SelectedValue = Session["BRCD"].ToString();

        BD.Ddl = DdlToBrCode;
        BD.FnBL_BindDropDown(BD);
        DdlToBrCode.SelectedValue = Session["BRCD"].ToString();
    
    }
    public void ClearData()
    {
        DdlFromBrCode.SelectedValue = Session["BRCD"].ToString();
        DdlToBrCode.SelectedValue = Session["BRCD"].ToString();
        TxtMM.Text = "";
        TxtYYYY.Text = "";
        
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
                Btn_ShowReport.Focus();

            }
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
            string FL = "",RptName="";
            FL = Rdb_Type.SelectedValue;
            if (FL == "S")
            {
                RptName = "RptDemandSummary.rdlc";
            }
            else
            {
                RptName = "RptDemandDetails.rdlc";
            }


            string redirectURL = "FrmRecRView.aspx?FL=" + FL + "&FBRCD=" + DdlFromBrCode.SelectedValue.ToString() + "&TBRCD=" + DdlToBrCode.SelectedValue.ToString() + "&ASONDATE=" + Session["EntryDate"].ToString() + "&UID=" + Session["UserName"].ToString() + "&MM=" + TxtMM.Text + "&YY=" + TxtYYYY.Text + "&rptname=" + RptName + "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}