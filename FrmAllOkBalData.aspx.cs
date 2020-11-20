using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAllOkBalData : System.Web.UI.Page
{
    DbConnection Conn = new DbConnection();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //added by ankita 07/10/2017 to make user frndly
    }
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CustIDRep_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?AsOnDate=" + TxtAsonDate.Text + " &FBrcd=" + TxtFBrCd.Text + " &TBrcd=" + TxtTBrCd.Text + "&rptname=RptGLBalanceDataTrf.rdlc" + "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}