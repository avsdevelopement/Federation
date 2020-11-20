using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmDailyBalLessClgBal : System.Web.UI.Page
{
    DbConnection Conn = new DbConnection();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsBindDropdown BD = new ClsBindDropdown();
    scustom customcs = new scustom();

    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CustIDRep_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?AsOnDate=" + TxtAsonDate.Text + " &FBrcd=" + TxtFBrCd.Text + " &Product=" + TxtFprdcode.Text + "&Period=" + TxtPeriod.Text + "&rptname=RptDailyBalanceLessThenClg.rdlc" + "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFprdname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtFprdname.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TxtFprdname.Text = CT[0].ToString();
                TxtFprdcode.Text = CT[1].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtFprdcode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFprdcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = BD.GetAccTypeGL(TxtFprdcode.Text, Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');

            ViewState["DRGL"] = GL[1].ToString();
            string PDName = customcs.GetProductName(TxtFprdcode.Text, Session["BRCD"].ToString());
            if (PDName != null)
            {
                TxtFprdname.Text = PDName;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}