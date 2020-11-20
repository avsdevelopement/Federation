using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmClgReturnReg : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsFDIntCalculation FD = new ClsFDIntCalculation();
    ClsAccountSTS AST = new ClsAccountSTS();
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

            TxtFDate.Text = Session["EntryDate"].ToString();
            TxtTDate.Text = Session["EntryDate"].ToString();

            TxtFBRCD.Text = Session["BRCD"].ToString();
            TxtTBRCD.Text = Session["BRCD"].ToString();
            TxtFBRCDName.Text = AST.GetBranchName(TxtFBRCD.Text);
            TxtTBRCDName.Text = AST.GetBranchName(TxtTBRCD.Text);
          
        }
    }
    protected void TxtFBRCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFBRCD.Text != "")
            {
                string bname = AST.GetBranchName(TxtFBRCD.Text);
                if (bname != null)
                {
                    TxtFBRCDName.Text = bname;
                    TxtTBRCD.Focus();

                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    TxtFBRCD.Text = "";
                    TxtFBRCD.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                TxtFBRCD.Text = "";
                TxtFBRCD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTBRCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtTBRCD.Text != "")
            {
                string bname = AST.GetBranchName(TxtTBRCD.Text);
                if (bname != null)
                {
                    TxtTBRCDName.Text = bname;
                    Rdb_IwR.Focus();

                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    TxtTBRCD.Text = "";
                    TxtTBRCD.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                TxtTBRCD.Text = "";
                TxtTBRCD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
 
    protected void Btn_Report_Click(object sender, EventArgs e)
    {
        try
        {
            if (Rdb_IwR.Checked)
            {
                if (Rdb_Det.Checked)
                {
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "InwardClearing_Ret_Detail _" + TxtFDate.Text + "_" + TxtTDate.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                    string redirectURL = "FrmRView.aspx?FBRCD=" + TxtFBRCD.Text + "&TBRCD=" + TxtTBRCD.Text + "&FDATE=" + TxtFDate.Text + "&TDATE=" + TxtTDate.Text + "&FL=IWR&SFL=DET&rptname=RptIWReturnRegDetails.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                else
                {
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "InwardClearing_Ret_Summary _" + TxtFDate.Text + "_" + TxtTDate.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                    string redirectURL = "FrmRView.aspx?FBRCD=" + TxtFBRCD.Text + "&TBRCD=" + TxtTBRCD.Text + "&FDATE=" + TxtFDate.Text + "&TDATE=" + TxtTDate.Text + "&FL=IWR&SFL=SUM&rptname=RptIWReturnRegSummary.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
            else if (Rdb_OwR.Checked)
            {
                if (Rdb_Det.Checked)
                {
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "OutwardClearing_Ret_Details _" + TxtFDate.Text + "_" + TxtTDate.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                    string redirectURL = "FrmRView.aspx?FBRCD=" + TxtFBRCD.Text + "&TBRCD=" + TxtTBRCD.Text + "&FDATE=" + TxtFDate.Text + "&TDATE=" + TxtTDate.Text + "&FL=OWR&SFL=DET&rptname=RptOWReturnRegDetails.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                else
                {
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "OutwardClearing_Ret_Summary _" + TxtFDate.Text + "_" + TxtTDate.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                    string redirectURL = "FrmRView.aspx?FBRCD=" + TxtFBRCD.Text + "&TBRCD=" + TxtTBRCD.Text + "&FDATE=" + TxtFDate.Text + "&TDATE=" + TxtTDate.Text + "&FL=OWR&SFL=SUM&rptname=RptOWReturnRegSummary.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_ClearAll_Click(object sender, EventArgs e)
    {
        ClearData();
    }

    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }

    public void ClearData()
    {
        TxtFBRCD.Text = "";
        TxtTBRCD.Text = "";
        TxtFBRCDName.Text = "";
        TxtTBRCDName.Text = "";
        TxtFDate.Text = "";
        TxtTDate.Text = "";
        
    }

    protected void TxtFDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtTDate.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtFBRCD.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}