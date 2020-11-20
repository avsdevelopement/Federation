using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmAVS5161 : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    ClsBindDropdown BD = new ClsBindDropdown();
    scustom customcs = new scustom();
    ClsBindDropdown DD = new ClsBindDropdown();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
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

                Txtfrmbrcd.Focus();
                autoglname.ContextKey = Session["BRCD"].ToString();
                //added by ankita 07/10/2017 to make user frndly 
                TxtFDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
                TxtTDate.Text = Session["EntryDate"].ToString();
                Txtfrmbrcd.Text = Session["BRCD"].ToString();
                Txttobrcd.Text = Session["BRCD"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Txtfrmbrcd_TextChanged(object sender, EventArgs e)
    {
        Txttobrcd.Focus();
    }
    protected void Txttobrcd_TextChanged(object sender, EventArgs e)
    {
        TxtFprdcode.Focus();
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
                TxtFDate.Focus();
            }
            else
            {
                WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                TxtFprdcode.Text = "";
                TxtFDate.Focus();
            }
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
                TxtFDate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SroRecovry_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            string redirectURL = "FrmRView.aspx?FBRCD=" + Txtfrmbrcd.Text + "&TBRCD=" + Txttobrcd.Text + "&FPRCD=" + TxtFprdcode.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&IntRate=" + TxtIntRate.Text + "&CommRate=" + TxtComm.Text + "&TDSRate=" + TxtTDS.Text + "&rptname=ISP_AVS0204.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
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
    public void ClearData()
    {
        Txtfrmbrcd.Text = "";
        Txttobrcd.Text = "";
        TxtFDate.Text = "";
        TxtTDate.Text = "";
        TxtFprdcode.Text = "";
        TxtFprdname.Text = "";
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
}