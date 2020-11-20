using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAVS5134 : System.Web.UI.Page
{

    string FL = "";
    ClsBindDropdown BD = new ClsBindDropdown();
    scustom customcs = new scustom();
       ClsLogMaintainance CLM = new ClsLogMaintainance();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void txtprdname_TextChanged(object sender, EventArgs e)
    {
        try
        {
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
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "NOCYUVA _Rpt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&PRDCD=" + txtprdcode.Text + "&ACCNO=" + txtaccno.Text + "&SRNO=" + ddlsrno.SelectedItem.Text + "&rptname=RptNOC_Certificate.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
           
               
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
            string accn = "";
            string[] AN;
            accn = customcs.GetAccountNme(txtaccno.Text, txtprdcode.Text, Session["BRCD"].ToString());
            if (accn != null)
            {
                AN = customcs.GetAccountNme(txtaccno.Text, txtprdcode.Text, Session["BRCD"].ToString()).Split('_');
                txtaccname.Text = AN[1].ToString();
                ddlsrno.Focus();
            }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                txtaccno.Text = "";
                txtaccno.Focus();
            }
            BD.BindSrNo(ddlsrno, Session["BRCD"].ToString(), txtaccno.Text, txtprdcode.Text);
           
    
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
                ddlsrno.Focus();
            }
            else
            {
                WebMsgBox.Show("Account Number is Invalid....!", this.Page);
                txtaccno.Text = "";
                txtaccno.Focus();
            }
            BD.BindSrNo(ddlsrno, Session["BRCD"].ToString(), txtaccno.Text, txtprdcode.Text);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtprdcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string GL = BD.GetAccTypeGL(txtprdcode.Text, Session["BRCD"].ToString());
            string[] GLCODE = GL.Split('_');
            if (GLCODE[1] == "3")
            {
                ViewState["DRGL"] = GL[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + txtprdcode.Text + "_" + ViewState["DRGL"].ToString();
                string PDName = customcs.GetProductName(txtprdcode.Text, Session["BRCD"].ToString());
                if (PDName != null)
                {
                    txtprdname.Text = PDName;
                    txtaccno.Focus();
                }
                else
                {
                    WebMsgBox.Show("Product Number is Invalid....!", this.Page);
                    txtprdcode.Text = "";
                    txtprdcode.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter only Loan Product Code....!", this.Page);
                txtprdcode.Text = "";
                txtprdcode.Focus();

            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
       
    
    }
}