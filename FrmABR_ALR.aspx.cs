using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmABR_ALR : System.Web.UI.Page
{
    DbConnection Conn = new DbConnection();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsCommon cmn = new ClsCommon();
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
            TxtBrID.Text = Session["BRCD"].ToString();
            TxtAsonDate.Text = Session["EntryDate"].ToString();
            if (cmn.MultiBranch(Session["LOGINCODE"].ToString()) != "Y")
            {
                TxtBrID.Enabled = false;
                TxtAsonDate.Focus();
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
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ABR_ALR_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&AsOnDate=" + Conn.ConvertDate(TxtAsonDate.Text).ToString() + "&rptname=Isp_AVS0029.rdlc" + "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}