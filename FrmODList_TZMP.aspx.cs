using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmODList_TZMP : System.Web.UI.Page
{
    DataTable DT = new DataTable();
    DataTable DT1 = new DataTable();
    ClsCustomerDetails CD = new ClsCustomerDetails();
    DbConnection conn = new DbConnection();
    scustom customcs = new scustom();
    ClsOpenClose OC = new ClsOpenClose();
    ClsMemberPassbook MP = new ClsMemberPassbook();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    Cls_RecoBindDropdown BD1 = new Cls_RecoBindDropdown();

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
                BindRecDiv();
                Txtfrmbrcd.Focus();
                autoglname.ContextKey = Session["BRCD"].ToString();
                autoglname1.ContextKey = Session["BRCD"].ToString();
                //added by ankita 07/10/2017 to make user frndly
                Txtfrmbrcd.Text = Session["BRCD"].ToString();
                TxtAsonDate.Text = Session["EntryDate"].ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void Txtfrmbrcd_TextChanged(object sender, EventArgs e)
    {

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
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        try
        { 
            if (DdlRecDept.SelectedValue.ToString() == "")
            {
                DdlRecDept.SelectedValue = "0";
            }
            string redirectURL = "FrmRView.aspx?FBRCD=" + Txtfrmbrcd.Text + "&FPRCD=" + TxtFprdcode.Text + "&TPRCD=" + TxtTprdcode.Text + "&AsOnDate=" + TxtAsonDate.Text + "&Div=" + DdlRecDiv.SelectedValue.ToString() + "&Dep=" + DdlRecDept.SelectedValue.ToString() + "&rptname=RptODlIstDivWise_TZMP.rdlc";
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
    protected void DdlRecDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindRecDept();
            DdlRecDept.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindRecDiv()
    {
        try
        {
            BD1.BRCD = Txtfrmbrcd.Text;
            BD1.Ddl = DdlRecDiv;
            BD1.FnBL_BindRecDiv(BD1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindRecDept()
    {
        try
        {
            BD1.BRCD = Txtfrmbrcd.Text;
            BD1.Ddl = DdlRecDept;
            BD1.RECDIV = DdlRecDiv.SelectedValue.ToString();
            BD1.FnBL_BindRecDept(BD1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}