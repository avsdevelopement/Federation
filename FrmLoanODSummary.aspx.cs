using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmLoanODSummary : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    ClsBindDropdown BD = new ClsBindDropdown();
    scustom customcs = new scustom();
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
                autoglname1.ContextKey = Session["BRCD"].ToString();


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
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DefaultersList_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?FBRCD=" + Txtfrmbrcd.Text + "&TBRCD=" + Txttobrcd.Text + "&FPRCD=" + TxtFprdcode.Text + "&TPRCD=" + TxtTprdcode.Text + "&FAccNo=" + TxtFaccno.Text + "&TAccNo=" + TxtTaccno.Text + "&Date=" + TxtAsonDate.Text + "&SL=" + rbtnType.SelectedValue + "&rptname=RptLoanODSummaryList.rdlc";
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
        //DdlAlS.SelectedValue = "0";
        TxtAsonDate.Text = "";
        TxtFprdcode.Text = "";
        TxtFprdname.Text = "";
        TxtTprdcode.Text = "";
        TxtTprdname.Text = "";
        TxtFaccno.Text = "";
        TxtFaccname.Text = "";
        TxtTaccno.Text = "";
        TxtTaccname.Text = "";
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void TxtTaccname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtTaccname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtTaccname.Text = custnob[0].ToString();
                TxtTaccno.Text = custnob[1].ToString();

            }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                TxtTaccno.Text = "";
                TxtTaccno.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTaccno_TextChanged(object sender, EventArgs e)
    {
        {
            try
            {
                string[] AN;
                AN = customcs.GetAccountName(TxtTaccno.Text, TxtTprdcode.Text, Session["BRCD"].ToString()).Split('_');
                if (AN != null)
                {
                    TxtTaccname.Text = AN[1].ToString();


                }
                else
                {
                    WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                    TxtTaccno.Text = "";
                    TxtTaccno.Focus();
                }

            }
            catch (Exception Ex)
            {

                ExceptionLogging.SendErrorToText(Ex);
            }
        }
    }
    protected void TxtFaccno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] AN;
            AN = customcs.GetAccountName(TxtFaccno.Text, TxtFprdcode.Text, Session["BRCD"].ToString()).Split('_');
            if (AN != null)
            {
                TxtFaccname.Text = AN[1].ToString();
                TxtTaccno.Focus();

            }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                TxtFaccno.Text = "";
                TxtFaccno.Focus();
            }

        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFaccname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtFaccname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtFaccname.Text = custnob[0].ToString();
                TxtFaccno.Text = custnob[1].ToString();
                TxtTaccno.Focus();
            }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                TxtFaccno.Text = "";
                TxtFaccno.Focus();
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
            AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtFprdcode.Text + "_" + ViewState["DRGL"].ToString();
            string PDName = customcs.GetProductName(TxtFprdcode.Text, Session["BRCD"].ToString());
            if (PDName != null)
            {
                TxtFprdname.Text = PDName;
                TxtTprdcode.Focus();
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
                TxtFaccno.Focus();
                string[] GLS = BD.GetAccTypeGL(TxtFprdcode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtFprdcode.Text + "_" + ViewState["DRGL"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTprdname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtTprdname.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TxtTprdname.Text = CT[0].ToString();
                TxtTprdcode.Text = CT[1].ToString();
                TxtTaccno.Focus();
                string[] GLS = BD.GetAccTypeGL(TxtTprdcode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                autoAccname1.ContextKey = Session["BRCD"].ToString() + "_" + TxtTprdcode.Text + "_" + ViewState["DRGL"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTprdcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = BD.GetAccTypeGL(TxtTprdcode.Text, Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');

            ViewState["DRGL"] = GL[1].ToString();
            autoAccname1.ContextKey = Session["BRCD"].ToString() + "_" + TxtTprdcode.Text + "_" + ViewState["DRGL"].ToString();
            string PDName = customcs.GetProductName(TxtTprdcode.Text, Session["BRCD"].ToString());
            if (PDName != null)
            {
                TxtTprdname.Text = PDName;
                TxtFaccno.Focus();
            }
            else
            {
                WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                TxtTprdcode.Text = "";
                TxtTprdcode.Focus();
            }


        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Txttobrcd_TextChanged(object sender, EventArgs e)
    {
        TxtAsonDate.Focus();
    }
    protected void Txtfrmbrcd_TextChanged(object sender, EventArgs e)
    {
        Txttobrcd.Focus();
    }
}