using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmKycReport : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

        }
    }
    protected void Report_Click(object sender, EventArgs e)
    {
        FL = "Insert";//ankita 15/09/2017
        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "KYC_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        string redirectURL = "FrmRView.aspx?rptname=RptKycDoc.rdlc&KycTP="+ddlKYCType.SelectedValue+"&EXPF="+ddlExportT.SelectedItem.Text+"";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
    }
}