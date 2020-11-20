using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmAccCount : System.Web.UI.Page
{
    string BRCD, GLCODE;
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsLogin LG = new ClsLogin();
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
            TxtBRCD.Text = Session["BRCD"].ToString();
            DataTable DT = new DataTable();
            DT = LG.GetBankNameDetails(TxtBRCD.Text.ToString());
            if (DT.Rows.Count > 0)
            {
                TxtBRCDName.Text = DT.Rows[0]["BranchName"].ToString();
            }
          
        }

    }
    protected void TxtBRCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBRCD.Text != "")
            {
                string bname = AST.GetBranchName(TxtBRCD.Text);
                if (bname != null)
                {
                    TxtBRCDName.Text = bname;
                    TxtPRD.Focus();

                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    TxtBRCD.Text = "";
                    TxtBRCD.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtPRD_TextChanged(object sender, EventArgs e)
    {

    }
   
    protected void Btnreport_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtBRCD.Text != null && TxtPRD.Text != null)
            {
                BRCD = TxtBRCD.Text.Trim();
                GLCODE = TxtPRD.Text.Trim();
            }
            else
            {
                WebMsgBox.Show("Fields cannot be Blanked!!",this.Page);
            }
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "AccCount_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?&BRCD=" + BRCD + "&GLCODE=" + GLCODE + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptAccCount.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }    
    }
}