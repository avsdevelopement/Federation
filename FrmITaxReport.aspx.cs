using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmITaxReport : System.Web.UI.Page
{
    DbConnection Conn = new DbConnection();
    ClsBindDropdown DD = new ClsBindDropdown();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
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
                Rdeatils.SelectedValue = "1";
                DD.BindMemActivity(DdlActivity);
                DD.BindSActivity(DdlSActivity, Rdeatils.SelectedValue);
                TxtBrID.Text = Session["BRCD"].ToString();
                //added by ankita 07/10/2017 to make user frndly 
                TxtFDate.Text = Conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + Conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
                TxtTDate.Text = Session["EntryDate"].ToString();
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
            FL = "Insert";//Dhanya 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Itax_Rpt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            if (Rdeatils.SelectedValue == "1")
            {
                if (DdlSActivity.SelectedValue == "1")
                {
                    string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&AType=" + DdlActivity.SelectedValue + "&SType=SHR&rptname=RptInComeTaxReport_SHR.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                else if (DdlSActivity.SelectedValue == "2")
                {
                    string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&AType=" + DdlActivity.SelectedValue + "&SType=DP&rptname=RptInComeTaxReport_DP.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                else if (DdlSActivity.SelectedValue == "3")
                {
                    string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&AType=" + DdlActivity.SelectedValue + "&SType=LOAN&rptname=RptInComeTaxReport_LN.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
            if (Rdeatils.SelectedValue == "2")
            {
                string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&AType=" + DdlActivity.SelectedValue + "&SType=" + DdlSActivity.SelectedValue + "&rptname=RptInComeTaxReport.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Rdeatils_SelectedIndexChanged(object sender, EventArgs e)
    {
        DD.BindSActivity(DdlSActivity, Rdeatils.SelectedValue);
    }
}