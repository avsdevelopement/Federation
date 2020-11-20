using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmAccOpenClseRpt : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAccOpenClseRpt AOC = new ClsAccOpenClseRpt();
    DbConnection conn = new DbConnection();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "", FLag = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }
                autoFglname.ContextKey = Session["brcd"].ToString();
                //added by ankita 07/10/2017 to make user frndly 
                txtfromdate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
                txttodate.Text = Session["EntryDate"].ToString();
                TxtFBRCD.Text = Session["BRCD"].ToString();
                TxtFBRCDName.Text = AST.GetBranchName(TxtFBRCD.Text);
                TxtTBRCD.Text = Session["BRCD"].ToString();
                TxtTBRCDName.Text = AST.GetBranchName(TxtTBRCD.Text);

            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string FL = "";
            if (RblType.SelectedValue == "0")
            {
                FL = "OPEN";
            }
            else if (RblType.SelectedValue == "1")
            {
                FL = "CLOSE";
            }
            FLag = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FLag, Session["BRCD"].ToString(), Session["MID"].ToString(), "AccOpnClse_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?&flag=" + FL + "&FDATE=" + conn.ConvertDate(txtfromdate.Text) + "&TDATE=" + conn.ConvertDate(txttodate.Text) + "&FBRCD=" + TxtFBRCD.Text + "&TBRCD=" + TxtTBRCD.Text + "&SUBGL=" + TxtProdCode.Text + "&UserName=" + Session["UserName"].ToString() + "&prodname=" + TxtProdName.Text + "&rptname=RptAccOpCl.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        try
        {
            clear();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
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
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtTBRCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFBRCD.Text != "")
            {
                string bname = AST.GetBranchName(TxtTBRCD.Text);
                if (bname != null)
                {
                    TxtTBRCDName.Text = bname;
                    TxtProdCode.Focus();

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
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtProdCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string tds = BD.GetLoanGL(TxtProdCode.Text, Session["BRCD"].ToString());
            if (tds != null)
            {
                string[] TD = tds.Split('_');
                if (TD.Length > 0)
                {

                }
                TxtProdName.Text = TD[0].ToString();
                TxtProdCode.Text = TD[1].ToString();

            }
            //else
            //{
            //    WebMsgBox.Show("Invalid Deposit Code......!", this.Page);
            //    TxtProdName.Text = "";
            //    TxtProdCode.Text = "";
            //    return;
            //}
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string pno = TxtProdName.Text;
            string[] prd = pno.Split('_');
            if (prd.Length > 0)
            {
                TxtProdName.Text = prd[0].ToString();
                TxtProdCode.Text = prd[1].ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void clear()
    {
        txtfromdate.Text = "";
        txttodate.Text = "";
        TxtFBRCD.Text = "";
        TxtTBRCD.Text = "";
        TxtFBRCDName.Text = "";
        TxtTBRCDName.Text = "";
        TxtProdCode.Text = "";
        TxtProdName.Text = "";
    }
    protected void txttodate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime Edate, TDATE;
                Edate = Convert.ToDateTime(Session["ENTRYDATE"].ToString());
                TDATE = DateTime.Parse(txttodate.Text);


                if (Edate < TDATE)
                {
                    txttodate.Text = "";
                    WebMsgBox.Show("ToDate Should Not Greater Than EntryDate", this.Page);
                    return;

                }
                else
                {
                    TxtFBRCD.Focus();
                }
            
            }
            catch (Exception Ex)
            {

                ExceptionLogging.SendErrorToText(Ex);
            }

           
    }
}