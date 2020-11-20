using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAVS5085 : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsBindDropdown BD = new ClsBindDropdown();
    scustom customcs = new scustom();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "", prdcd = "", prdname = "";
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
                //added by ankita 07/10/2017 to make user frndly
                TxtFBRCD.Text = Session["BRCD"].ToString();
                TxtTBrcd.Text = Session["BRCD"].ToString();
                TxtFBrname.Text = AST.GetBranchName(TxtFBRCD.Text);
                TxtTBrcdName.Text = AST.GetBranchName(TxtTBrcd.Text);
                TxtFDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
                TxtTDate.Text = Session["EntryDate"].ToString();

            }
            string[] prdcd1 = AST.GetGoldPrdcd().Split('_');
            prdcd = prdcd1[0].ToString();
            prdname = prdcd1[1].ToString();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "GldSnctn_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            string CURRENT = TxtCurRt.Text == "" ? "0" : TxtCurRt.Text;
            string redirectURL = "FrmRView.aspx?&FBRCD=" + TxtFBRCD.Text + "&TBRCD=" + TxtTBrcd.Text + "&FPRCD=" + prdcd + "&FDT=" + TxtFDate.Text + "&TDT=" + TxtTDate.Text + "&EDT=" + Session["EntryDate"].ToString() + "&PRDNAME=" + prdname + "&CURRATE=" + CURRENT + "&rptname=RptAVS5085.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true); 
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
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
                       TxtFBrname.Text = bname;
                       TxtFBRCD.Focus();

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
    protected void TxtTBrcd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtTBrcd.Text != "")
            {
                string bname = AST.GetBranchName(TxtTBrcd.Text);
                if (bname != null)
                {
                    TxtTBrcdName.Text = bname;
                    TxtTBrcd.Focus();

                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    TxtTBrcd.Text = "";
                    TxtTBrcd.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                TxtTBrcd.Text = "";
                TxtTBrcd.Focus();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
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
            else
            {
                WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                TxtFprdcode.Text = "";
                TxtFprdcode.Focus();
            }


        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}