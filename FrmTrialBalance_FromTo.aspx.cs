using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmTrialBalance_FromTo : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsTrailBalance TB = new ClsTrailBalance();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsCommon cmn = new ClsCommon();
    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("FrmLogin.aspx");
            TxtBrID.Text = Session["BRCD"].ToString();
            // TxtTDate.Text = Session["EntryDate"].ToString();
            //added by ankita 07/10/2017 to make user frndly 
            TxtFDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
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
    protected void ReportV_Click(object sender, EventArgs e)
    {
        FL = "Insert";//ankita 15/09/2017
        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "TrialBal_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        //TxtFDate.Text, TxtTDate.Text, Session["BRCD"].ToString()
        string FDT;
        string CN = "";
        if (TxtFDate.Text == "")
        {
            FDT = "01/01/1990";
        }
        else
        {
            FDT = TxtFDate.Text;
        }

        CN = rdbcode.Checked == true ? "C" : "N";
        if (TxtTDate.Text == Session["EntryDate"].ToString())
        {
            WebMsgBox.Show("DayEnd Not Complete...", this.Page);
        }
        if (RBSel.SelectedValue == "1")
        {
            string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&FDate=" + FDT + "&TDate=" + TxtTDate.Text + "&YN=" + CN + "&rptname=RptTrialBalance_FromTo.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        else if (RBSel.SelectedValue == "2")
        {
            string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&FDate=" + FDT + "&TDate=" + TxtTDate.Text + "&YN=" + CN + "&rptname=RptTrialBal_FrmToMarathi.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
    }
    protected void RBSel_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}