using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmClgMemoList : System.Web.UI.Page
{
    DbConnection Conn = new DbConnection();

    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("FrmLogin.aspx");
        }
        TxtBrID.Text = Session["BRCD"].ToString();
        TxtAsonDate.Text = Session["EntryDate"].ToString();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "OutwardClearing_Memo_list_rpt _" + TxtAsonDate.Text +"_"+Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&AsOnDate=" + Conn.ConvertDate(TxtAsonDate.Text).ToString() + "&rptname=RptClearngMemoList.rdlc" + "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}