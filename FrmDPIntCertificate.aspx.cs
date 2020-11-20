using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmDPIntCertificate : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsAccopen accop = new ClsAccopen();
    ClsBindDropdown BD = new ClsBindDropdown();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            BD.BindBRANCHNAME(ddlBrName, null);
        }
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//Dhanya Shetty
            string Result = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "FD_IntCertificate _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&FBRCD = " + Txtfrmbrcd.Text + "&CutNo=" + TxtCust.Text + "&rptname=Isp_AVS0038.rdlc";
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
        TxtFDate.Text = "";
        TxtTDate.Text = "";
        TxtCust.Text = "";
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void Txtfrmbrcd_TextChanged(object sender, EventArgs e)
    {
        TxtFDate.Focus();
    }
    protected void txtname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                txtname.Text = custnob[0].ToString();
                TxtCust.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtCust_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string sql, AT;
            sql = AT = "";

            if (TxtCust.Text == "")
            {
                return;
            }
            string custname = accop.GetcustnameYN(accop.GetCENTCUST(), TxtCust.Text, Session["BRCD"].ToString());

            if (custname != null)
            {
                string[] name = custname.Split('_');
                txtname.Text = name[0].ToString();
            }

            string RC = txtname.Text;
            if (RC == "")
            {
                WebMsgBox.Show("Customer not found", this.Page);
                TxtCust.Text = "";
                TxtCust.Focus();
                return;
            }
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void ddlBrName_SelectedIndexChanged(object sender, EventArgs e)
    {
        Txtfrmbrcd.Text = ddlBrName.SelectedValue.ToString();
    }
}