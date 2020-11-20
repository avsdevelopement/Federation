using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmRecoveryStatement : System.Web.UI.Page
{
    Cls_RecoBindDropdown BD = new Cls_RecoBindDropdown();
    ClsRecoveryStatement RS = new ClsRecoveryStatement();
    DbConnection Conn = new DbConnection();
    string MM, YY, EDT;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBr();
            ddlBrCode.SelectedValue = Session["BRCD"].ToString();
            TxtMM.Focus();

        }
    }
    public void ClearData()
    {
        ddlBrCode.SelectedValue = "0";
        TxtMM.Text = "";
        TxtYYYY.Text = "";
        DdlRecDiv.SelectedValue = "0";
        DdlRecDept.SelectedValue = "0";
        ddlBrCode.Focus();
    }
    public void BindBr()
    {
        BD.Ddl = ddlBrCode;
        BD.FnBL_BindDropDown(BD);
        BindRecDiv();

    }

    protected void Btn_Report_Click(object sender, EventArgs e)
    {
        try
        {

            EDT = DateSet();
            string RptName = "", RecFor = "";

            string BKCD = RS.FnBl_GetBANKCode(RS);
            if (BKCD == "1009")
            {
                RptName = "RptRecoveryStatement_1009.rdlc";
            }
            else if (BKCD == "1010")
            {
                RptName = "RptRecoveryStatement_1010.rdlc";
            }
            else
            {
                RptName = "RptRecoveryStatement.rdlc";
            }

            RecFor = DdlRecDiv.SelectedItem.Text + " ( " + DdlRecDept.SelectedItem.Text + " ) ";

            string redirectURL = "FrmRecRView.aspx?FL=" + Rdb_ReportType.SelectedValue.ToString() + "&BRCD=" + ddlBrCode.SelectedValue.ToString() + "&ASONDATE=" + EDT.ToString() + "&RECDIV=" + DdlRecDiv.SelectedValue.ToString() + "&RECCODE=" + DdlRecDept.SelectedValue.ToString() + "&UID=" + Session["UserName"].ToString() + "&MM=" + TxtMM.Text + "&YY=" + TxtYYYY.Text + "&RECFOR=" + RecFor + "&rptname=" + RptName + "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public string DateSet()
    {
        try
        {
            string ED = Conn.ConvertToDate(Session["EntryDate"].ToString());
            string[] D = ED.Split('-');

            MM = TxtMM.Text;
            YY = TxtYYYY.Text;
            if (TxtMM.Text.Length == 1)
            {
                if (Convert.ToInt32(MM) < 10)
                    MM = "0" + MM;
            }
            // EDT = D[2].ToString() + "/" + MM + "/" + YY;
            EDT = Session["EntryDate"].ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return EDT;
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
                TxtMM.Focus();
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

    protected void Btn_ClearAll_Click(object sender, EventArgs e)
    {
        ClearData();
    }
}