using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmPeriodWiseClassOfOD : System.Web.UI.Page
{   
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    ClsBindDropdown BD = new ClsBindDropdown();
    scustom customcs = new scustom();
     
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
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
            if (Rdeatils.SelectedValue == "1")
            {
                string redirectURL = "FrmRView.aspx?FBRCD=" + Txtfrmbrcd.Text + "&TBRCD=" + Txttobrcd.Text + "&FPRCD=" + TxtFprdcode.Text + "&TPRCD=" + TxtTprdcode.Text + "&Date=" + TxtAsonDate.Text + "&Flag=" + "TD" + " &rptname=LNODPeriodWiseSumry.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (Rdeatils.SelectedValue == "2")
            {
                string redirectURL = "FrmRView.aspx?FBRCD=" + Txtfrmbrcd.Text + "&TBRCD=" + Txttobrcd.Text + "&FPRCD=" + TxtFprdcode.Text + "&TPRCD=" + TxtTprdcode.Text + "&Date=" + TxtAsonDate.Text + "&Flag=" + "TDSU" + " &rptname=Isp_AVS0015.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
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
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
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
                string[] GLS = BD.GetAccTypeGL(TxtTprdcode.Text, Session["BRCD"].ToString()).Split('_');
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
    protected void Txttobrcd_TextChanged(object sender, EventArgs e)
    {
        TxtAsonDate.Focus();
    }
    protected void Txtfrmbrcd_TextChanged(object sender, EventArgs e)
    {
        Txttobrcd.Focus();
    }
    protected void BtnSlab_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmCreateSlab.aspx", true);
    }
}