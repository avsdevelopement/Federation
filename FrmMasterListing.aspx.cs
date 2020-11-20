using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmMasterListing : System.Web.UI.Page
{
    scustom customcs = new scustom();
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsDeadStock DS = new ClsDeadStock();
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

                txtbrchcode.Text = Session["BRCD"].ToString();
                ddlallsel.Focus();
                autoFAFglname.ContextKey = Session["BRCD"].ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);;
        }
    }
    protected void txtprdcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string res = "";
            string GL = BD.GetAccTypeGL(txtprdcode.Text, Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');
            ViewState["DRGL"] = GL[1].ToString();
            AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtprdcode.Text + "_" + ViewState["DRGL"].ToString();
            string PDName = customcs.GetFAFProductName(txtprdcode.Text, Session["BRCD"].ToString());
            if (PDName != null)
            {
                txtprdname.Text = PDName;
                res = DS.AccNodisplay(txtbrchcode.Text, txtprdcode.Text);
                if (res == "0")
                {
                    txtaccno.Text = txtprdcode.Text;
                    txtaccname.Text = txtprdname.Text;
                    
                }
                else
                {
                    WebMsgBox.Show("Please enter account No..!!", this.Page);
                    txtaccno.Focus();

                }
             }
            else
            {
                WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                txtprdcode.Text = "";
                txtprdcode.Focus();
            }
          }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtprdname_TextChanged(object sender, EventArgs e)
    {
    try
        {
            string res = "";
            string custno = txtprdname.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                txtprdname.Text = CT[0].ToString();
                txtprdcode.Text = CT[1].ToString();
                txtaccno.Focus();
                string[] GLS = BD.GetAccTypeGL(txtprdcode.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtprdcode.Text + "_" + ViewState["DRGL"].ToString();
                res = DS.AccNodisplay(txtbrchcode.Text, txtprdcode.Text);
                if (res == "0")
                {
                    txtaccno.Text = txtprdcode.Text;
                    txtaccname.Text = txtprdname.Text;
                }
                else
                {
                    WebMsgBox.Show("Please enter account No..!!", this.Page);

                }
             }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtstatusno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string SName = DS.GetData("STS", txtstatusno.Text);
            if (SName != null)
            {
                txtstatusname.Text = SName;
            }
            else
            {
                WebMsgBox.Show("Status Code is Invalid....!", this.Page);
                txtstatusno.Text = "";
                txtstatusno.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BthReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlallsel.SelectedValue != "0" && ddlstatus.SelectedValue != "0")
            {
                FL = "Insert";//Dhanya Shetty
                string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Deadstock_Rpt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                string redirectURL = "FrmRView.aspx?BRCD=" + txtbrchcode.Text + "&EDAT=" + Session["EntryDate"].ToString() + "&ProdCode=" + txtprdcode.Text + "&AccNo=" + txtaccno.Text + "&Status=" + txtstatusno.Text + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptMasterListing.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else
            {
                WebMsgBox.Show("Select Product and Status!!", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtaccno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] AN;
            AN = customcs.GetAccountName(txtaccno.Text, txtprdcode.Text, Session["BRCD"].ToString()).Split('_');
            if (AN != null)
            {
                txtaccname.Text = AN[1].ToString();
  }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                txtaccno.Text = "";
                txtaccno.Focus();
            }
         }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtaccname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtaccname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtaccname.Text = custnob[0].ToString();
                txtaccno.Text = custnob[1].ToString();
            }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                txtaccno.Text = "";
                txtaccno.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void ddlallsel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlallsel.SelectedValue=="S")
            {
                div_prd.Visible = true;
            }
            else if (ddlallsel.SelectedValue == "A")
                {
                    div_prd.Visible = false;
                }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
       
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlstatus.SelectedValue == "S")
            {
                div_Status.Visible = true;
            }
            else if (ddlstatus.SelectedValue == "A")
            {
                div_Status.Visible = false;
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
    public void ClearData()
    {
        ddlallsel.SelectedValue = "0";
        txtprdcode.Text = "";
        txtprdname.Text = "";
        txtaccno.Text = "";
        txtaccname.Text = "";
        ddlstatus.SelectedValue = "0";
        txtstatusno.Text = "";
        txtstatusname.Text = "";
    }
}