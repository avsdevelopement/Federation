using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmLoanOverdue : System.Web.UI.Page
{
    scustom customcs = new scustom();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLoanOverdue LC = new ClsLoanOverdue();
    ClsAccountSTS AST = new ClsAccountSTS();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TxtDate.Text = Session["EntryDate"].ToString();
            autoglname.ContextKey = Session["BRCD"].ToString();
            autoglname1.ContextKey = Session["BRCD"].ToString();
            TxtFBRCD.Text = Session["BRCD"].ToString();
            TxtTBRCD.Text = Session["BRCD"].ToString();
            TxtFBRCDName.Text = AST.GetBranchName(TxtFBRCD.Text);
            TxtTBRCDName.Text = AST.GetBranchName(TxtTBRCD.Text);
            TxtFPRD.Focus();

        }
    }

    protected void Exit_Click(object sender, EventArgs e)
    {
        //  Response.Redirect("FrmBlank.aspx");
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    
    

    protected void New_Report_Click(object sender, EventArgs e)
    {
        ShowRpt();
    }

    public void ShowRpt()
    {
        try
        {
            string SL1 = "";

            if (Rdb_AccType.SelectedValue == "1")
                SL1 = "CF";
            else if (Rdb_AccType.SelectedValue == "2")
                SL1 = "NCF";
            else if (Rdb_AccType.SelectedValue == "3")
                SL1 = "ALL";




            //IMP NOTE
            // RptLoanOverdue.rdlc Mentioned here but it calls the RptLonaoverude.rdlc

            if (Chk_Address.Checked == false)
            {
                string redirectURL = "FrmRView.aspx?Date=" + TxtDate.Text + "&UserName=" + Session["UserName"].ToString() + "&Fbrcd=" + TxtFBRCD.Text + "&Tbrcd=" + TxtTBRCD.Text + "&EntryDate=" + Session["EntryDate"].ToString() + "&FSUBGL=" + TxtFPRD.Text + "&TSUBGL=" + TxtTPRD.Text + "&SL=" + SL1 + "&rptname=RptLoanOverdue_New.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else
            {
                string redirectURL = "FrmReportViewer.aspx?Date=" + TxtDate.Text + "&UserName=" + Session["UserName"].ToString() + "&Fbrcd=" + TxtFBRCD.Text + "&Tbrcd=" + TxtTBRCD.Text + "&EntryDate=" + Session["EntryDate"].ToString() + "&FSUBGL=" + TxtFPRD.Text + "&TSUBGL=" + TxtTPRD.Text + "&SL=" + SL1 + "&rptname=RptLoanBalanceList_Pen.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFBRCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFBRCD.Text != "")
            {
                if (TxtTBRCD.Text != "" && (Convert.ToInt32(TxtFBRCD.Text) > Convert.ToInt32(TxtTBRCD.Text)))
                {
                    WebMsgBox.Show("Invalid FROM and TO Branch Code....!", this.Page);
                    //Clear();
                    return;
                }

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
                    TxtFPRD.Focus();

                    if (Convert.ToInt32(TxtFBRCD.Text) > Convert.ToInt32(TxtTBRCD.Text))
                    {
                        WebMsgBox.Show("Invalid FROM and TO Branch Code....!", this.Page);
                        //Clear();
                        return;
                    }

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

    protected void TxtFPRD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] TD = BD.GetLoanGL(TxtFPRD.Text, Session["BRCD"].ToString()).Split('_');
            if (TD.Length > 1)
            {

            }
            TxtFPRDName.Text = TD[0].ToString();
            TxtTPRD.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTPRD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] TD = BD.GetLoanGL(TxtTPRD.Text, Session["BRCD"].ToString()).Split('_');
            if (TD.Length > 1)
            {

            }
            TXtTPRDName.Text = TD[0].ToString();
            TxtDate.Focus();
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtFPRDName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtFPRDName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {

                TxtFPRDName.Text = CT[0].ToString();
                TxtFPRD.Text = CT[1].ToString();
                //TxtGLCD.Text = CT[2].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtFPRD.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                //AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();

                if (TxtFPRDName.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    TxtFPRD.Text = "";
                    TxtFPRD.Focus();

                }
                else
                {
                    TxtTPRD.Focus();
                }


            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }

    }
    protected void TXtTPRDName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TXtTPRDName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TXtTPRDName.Text = CT[0].ToString();
                TxtTPRD.Text = CT[1].ToString();
                //TxtGLCD.Text = CT[2].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtTPRD.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                //AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProcode.Text + "_" + ViewState["DRGL"].ToString();

                if (TXtTPRDName.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    TxtTPRD.Text = "";
                    TxtTPRD.Focus();

                }
               
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }

    protected void Rdb_AccType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


}