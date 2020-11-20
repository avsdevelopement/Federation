using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmAVS5115 : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    DataTable DT = new DataTable();
    scustom customcs = new scustom();
    ClsBindDropdown BD = new ClsBindDropdown();
    string FL = "";
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
                autoglname.ContextKey = Session["BRCD"].ToString();
                TxtFBrcd.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtPrd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = BD.GetAccTypeGL(TxtPrd.Text, Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');
            if (GLCODE[1] == "3")
            {
                ViewState["DRGL"] = GL[1].ToString();
               string PDName = customcs.GetProductName(TxtPrd.Text, Session["BRCD"].ToString());
                if (PDName != null)
                {
                    TxtPrdName.Text = PDName;
                    TxtMonth.Focus();
                }
                else
                {
                    WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                    TxtPrd.Text = "";
                    TxtPrd.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter only Loan Product Code....!", this.Page);
                TxtPrd.Text = "";
                TxtPrd.Focus();

            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtPrdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtPrdName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TxtPrdName.Text = CT[0].ToString();
                TxtPrd.Text = CT[1].ToString();
               
                string[] GLS = BD.GetAccTypeGL(TxtPrd.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                TxtMonth.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void BtnRpt_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtFBrcd.Text != "" && TxtTBrcd.Text != "" && TxtPrd.Text != "" && TxtPrdName.Text != "" && TxtMonth.Text != "" && TxtYear.Text != "")
            {
                 FL = "Insert";
                 string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "LoanIntRateRecovery" + "_" + TxtFBrcd.Text + "_" + TxtTBrcd.Text + "_" + TxtPrd.Text + "_" + TxtMonth.Text + "_" + TxtYear.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                 string redirectURL = "FrmRView.aspx?FBrcd=" + TxtFBrcd.Text + "&TBrcd=" + TxtTBrcd.Text + "&Prd=" + TxtPrd.Text + "&Month=" + TxtMonth.Text + "&Year=" + TxtYear.Text +  " &rptname=RptAVS5115.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
               
                
            }
            else
            {
                WebMsgBox.Show("Enter the details", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void ClearData()
    {
        TxtFBrcd.Text = "" ;
        TxtTBrcd.Text = "";
        TxtPrd.Text = "" ;
        TxtPrdName.Text = "" ;
        TxtMonth.Text = "" ;
        TxtYear.Text = "";
    }
}