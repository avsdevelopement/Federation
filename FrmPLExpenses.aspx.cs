using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmPLExpenses : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsCommon cmn = new ClsCommon();
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
                //added by ankita 07/10/2017 to make user frndly 
                TxtFDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
             
                TxtTDate.Text = Session["EntryDate"].ToString();
                TxtBrID.Text = Session["BRCD"].ToString();
                TxtBrID.Focus();
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
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "PlExpenses_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&FromDate=" + TxtFDate.Text + "&ToDate=" + TxtTDate.Text + "&rptname=RptPLExpenses.rdlc" + "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}