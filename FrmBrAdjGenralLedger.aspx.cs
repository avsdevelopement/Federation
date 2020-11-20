using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmBrAdjGenralLedger : System.Web.UI.Page
{
    DbConnection Conn = new DbConnection();
    ClsReport LC = new ClsReport();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsCommon cmn = new ClsCommon();
    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("FrmLogin.aspx");
        }
        //added by ankita 07/10/2017 to make user frndly
        if (!IsPostBack)
        {
            TxtBrID.Text = Session["BRCD"].ToString();
            TxtFDate.Text = Conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + Conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
            TxtTDate.Text = Session["EntryDate"].ToString();
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
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ProdTrans_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
              string fmonth, fyear;
                string tmonth, tyear;

                string[] fdate = TxtFDate.Text.Split('/');
                fmonth = fdate[1].ToString();
                fyear = fdate[2].ToString();

                string[] tdate = TxtTDate.Text.Split('/');
                tmonth = tdate[1].ToString();
                tyear = tdate[2].ToString();

                string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&rptname=RptGenralLedgerWise_BrAdj.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}