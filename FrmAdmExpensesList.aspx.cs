using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAdmExpensesList : System.Web.UI.Page
{
    DbConnection Conn = new DbConnection();
    ClsReport LC = new ClsReport();
    ClsCommon cmn = new ClsCommon();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("FrmLogin.aspx");
        }
        if (!IsPostBack)
        {
            //added by ankita 07/10/2017 to make user frndly
            //DateTime dateI = Convert.ToDateTime(Session["EntryDate"].ToString());
            //dateI = new DateTime(dateI.Year, dateI.Month, 1);
            TxtFDate.Text = Conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + Conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
            TxtTDate.Text = Session["EntryDate"].ToString();
            TxtBrID.Text = Session["BRCD"].ToString();
            if (cmn.MultiBranch(Session["LOGINCODE"].ToString()) != "Y")
            {
                TxtBrID.Enabled = false;
                TxtFDate.Focus();
            }
            else
            {
                TxtBrID.Enabled = true;
                TxtBrID.Focus();
            }
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "BrWiseGl_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            if (rbtnRptType.SelectedValue == "1")
            {
                string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&rptname=RptAdmExpenses_DT.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (rbtnRptType.SelectedValue == "2")
            {
                string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&rptname=RptAdmExpenses_Sumry.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (rbtnRptType.SelectedValue == "3")
            {
                string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&rptname=RptAdmExpenses_Br.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void rbtnRptType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void BtnExit_Click(object sender, EventArgs e)
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