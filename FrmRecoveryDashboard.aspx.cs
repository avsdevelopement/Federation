using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmRecoveryDashboard : System.Web.UI.Page
{
    Cls_RecoBindDropdown BD = new Cls_RecoBindDropdown();
    ClsRecoveryOperation RO = new ClsRecoveryOperation();
    ClsRecoveryStatement RS = new ClsRecoveryStatement();

    DbConnection Conn = new DbConnection();

    int Res = 0;
    string MM, YY, EDT, STRRes;
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TxtMM.Focus();
            string BKCD = RS.FnBl_GetBANKCode(RS);

            if (BKCD != null)
            {
                ViewState["BANKCODE"] = BKCD;
            }
            else
            {
                ViewState["BANKCODE"] = "0";
            }
        }
    }
    #region Event Change
    protected void TxtMM_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int Len = 0;
            Len = TxtMM.Text.Length;
            if (Len > 2 || Convert.ToInt32(TxtMM.Text) > 12)
            {
                WebMsgBox.Show("Invalid month, Enter valid month....!", this.Page);
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
                WebMsgBox.Show("Invalid month, Enter valid month....!", this.Page);
                TxtYYYY.Text = "";
                TxtYYYY.Focus();
            }
            else
            {
                BindSumGrid();


            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void GrdSumDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    #endregion


    #region User functions
    public void BindSumGrid()
    {
        try
        {
            RO.FL = "DETGRID";
            RO.BRCD = Session["BRCD"].ToString();
            RO.MM = TxtMM.Text;
            RO.YY = TxtYYYY.Text;
            RO.GRD = GrdSumDetails;
            RO.BANKCODE = ViewState["BANKCODE"].ToString();
            Res = RO.FnBL_GetSumGrid(RO);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #endregion




    protected void GrdSumDetails_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {

            string RptType = "", BrCode = "", RecDiv = "", RecCode = "", Schoolname = "", RptName = "", RecFor = "";
            GridViewRow row = GrdSumDetails.Rows[e.NewSelectedIndex];


            Label LRecCode = (Label)row.FindControl("LblRecDept");
            Label LRecDiv = (Label)row.FindControl("LblRecDiv");
            Label LSchoolName = (Label)row.FindControl("LblName");

            RecCode = LRecCode.Text;
            RecDiv = LRecDiv.Text;
            Schoolname = LSchoolName.Text;

            BrCode = Session["BRCD"].ToString();
            RptType = "S";
            EDT = Session["EntryDate"].ToString();
            if (ViewState["BANKCODE"].ToString() == "1009")
            {
                RptName = "RptRecoveryStatement_1009.rdlc";
            }
            if (ViewState["BANKCODE"].ToString() == "1010")
            {
                RptName = "RptRecoveryStatement_1010.rdlc";
            }
            else
            {
                RptName = "RptRecoveryStatement.rdlc";
            }


            RecFor = "Div- " + RecDiv + " ( " + RecCode + " - " + Schoolname + " ) ";

            string redirectURL = "FrmRecRView.aspx?FL=" + RptType.ToString() + "&BRCD=" + BrCode.ToString() + "&ASONDATE=" + EDT.ToString() + "&RECDIV=" + RecDiv.ToString() + "&RECCODE=" + RecCode.ToString() + "&UID=" + Session["UserName"].ToString() + "&MM=" + TxtMM.Text + "&YY=" + TxtYYYY.Text + "&RECFOR=" + RecFor.ToString() + "&rptname=" + RptName + "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);



        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}