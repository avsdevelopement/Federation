using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmHeadTrfTrans : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    ClsBindDropdown BD = new ClsBindDropdown();
    scustom customcs = new scustom();
    ClsBindDropdown DD = new ClsBindDropdown();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsGetNPAList NA = new ClsGetNPAList();

    string FL = "";
    string STR = "";

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
                Txtfrmbrcd.Text = Session["BRCD"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Txtfrmbrcd_TextChanged(object sender, EventArgs e)
    {

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
                TxtFDate.Focus();
                string[] GLS = BD.GetAccTypeGL(TxtFprdcode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
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
            string PDName = customcs.GetProductName(TxtTprdcode.Text, Session["BRCD"].ToString());
            if (PDName != null)
            {
                TxtTprdname.Text = PDName;
                TxtTrfprdcode.Focus();
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
                TxtFDate.Focus();
                string[] GLS = BD.GetAccTypeGL(TxtTprdcode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTrfprdcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = BD.GetAccTypeGL(TxtTrfprdcode.Text, Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');

            ViewState["DRGL"] = GL[1].ToString();
            string PDName = customcs.GetProductName(TxtTrfprdcode.Text, Session["BRCD"].ToString());
            if (PDName != null)
            {
                TxtTrfprdname.Text = PDName;
                TxtFaccno.Focus();
            }
            else
            {
                WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                TxtTrfprdcode.Text = "";
                TxtTrfprdcode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTrfprdname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtTprdname.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TxtTrfprdname.Text = CT[0].ToString();
                TxtTrfprdcode.Text = CT[1].ToString();
                TxtFDate.Focus();
                string[] GLS = BD.GetAccTypeGL(TxtTrfprdcode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
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
            string redirectURL = "FrmRView.aspx?FBRCD=" + Txtfrmbrcd.Text + "&FPRCD=" + TxtFprdcode.Text + "&TPRCD=" + TxtTprdcode.Text + "&TrfPRCD=" + TxtTrfprdcode.Text + "&FAccNo=" + TxtFaccno.Text + "&TAccNo=" + TxtTaccno.Text + "&FDate=" + TxtFDate.Text + "&Amt=" + TxtAmt.Text + "&rptname=HeadAcListVoucherTRF.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        try
        {
            ClearData();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void ClearData()
    {
        Txtfrmbrcd.Text = "";
        TxtFDate.Text = "";
        TxtFprdcode.Text = "";
        TxtFprdname.Text = "";
        TxtTprdcode.Text = "";
        TxtTprdname.Text = "";
        TxtTrfprdcode.Text = "";
        TxtTrfprdname.Text = "";
        TxtFaccno.Text = "";
        TxtTaccno.Text = "";
        TxtAmt.Text = "";
        TxtFaccname.Text = "";
        TxtTaccname.Text = "";
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFaccno_TextChanged(object sender, EventArgs e)
    {
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
                    TxtFDate.Focus();
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
    protected void BtnPost_Click(object sender, EventArgs e)
    {
        FL = "Final";

        STR = NA.GetHeadAcListDT_Post(Txtfrmbrcd.Text, TxtFprdcode.Text, TxtTprdcode.Text, TxtTrfprdcode.Text, TxtFDate.Text, TxtFaccno.Text, TxtTaccno.Text, TxtAmt.Text , FL);

        if (STR != null)
        {
            WebMsgBox.Show("Voucher Post successfully with Set No - " + STR, this.Page);
            FL = "Insert";
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DivCalc_post" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            ClearData();
        }
        else
        {
            WebMsgBox.Show("Voucher Is Already Posted...", this.Page);
        }
    }
}