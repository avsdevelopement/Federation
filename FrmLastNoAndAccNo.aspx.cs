using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class FrmLastNoAndAccNo : System.Web.UI.Page
{
    ClsLastNoAccNoupdate LAU = new ClsLastNoAccNoupdate();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsBindDropdown BD = new ClsBindDropdown();
    scustom customcs = new scustom();
    int result = 0;
    string BRCD = "", PRD = "", AC = "", GLCODE="";
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
   protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            autoglname.ContextKey = Session["BRCD"].ToString();
            Div_ACCNO.Visible = false;
            Div_Buttons.Visible = false;
            Div_LASTNO.Visible = false;
        }
    }
    protected void Rdb_No_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Rdb_No.SelectedValue == "1")
            {
                Div_ACCNO.Visible = true;
                Div_Buttons.Visible = true;
                Div_LASTNO.Visible = false;
            }
            else if (Rdb_No.SelectedValue == "2")
            {
                Div_ACCNO.Visible = false;
                Div_Buttons.Visible = true;
                Div_LASTNO.Visible = true;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void txtbrcdno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtbrcdno.Text!="")
            {
                string bname = AST.GetBranchName(txtbrcdno.Text);
                 if (bname != null)
                {
                    txtbrcdname.Text = bname;
                    txtprdcode.Focus();
                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    txtbrcdno.Text = "";
                    txtbrcdno.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                txtbrcdno.Text = "";
                txtbrcdno.Focus();
            }
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
            string tds = BD.GetAccTypeGL(txtprdcode.Text, Session["BRCD"].ToString());
            if (tds != null)
            {
                string[] TD = tds.Split('_');
                if (TD.Length > 1)
                {
                    txtprdname.Text = TD[0].ToString();
                    ViewState["GLCODE"] = TD[1].ToString();
                    txtaccno.Focus();
                    autoacc.ContextKey = Session["BRCD"].ToString() + "_" + txtprdcode.Text + "_" + ViewState["GLCODE"].ToString();
                }
            }
            else
            {
                WebMsgBox.Show("Invalid Deposit Code......!", this.Page);
                Clear();
                return;
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
    protected void txtBR2no_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtBR2no.Text != "")
            {
                string bname = AST.GetBranchName(txtBR2no.Text);
                if (bname != null)
                {
                    txtBR2name.Text = bname;
                 }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    txtBR2no.Text = "";
                    txtBR2no.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                txtBR2no.Text = "";
                txtBR2no.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Rdb_No.SelectedValue == "1")
            {
                if (txtbrcdno.Text != null && txtprdcode.Text != null && txtaccno.Text!=null)
             {
                    BRCD=txtbrcdno.Text;
                    PRD=txtprdcode.Text;
                    AC = txtaccno.Text;
             }
                result = LAU.AccNoUpdate(txtbrcdno.Text, txtprdcode.Text, txtaccno.Text, ViewState["GLCODE"].ToString());
                if (result > 0)
                {
                    WebMsgBox.Show("Updated Successfully!!!!", this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "LastNoUpdate _Update_" + txtprdcode.Text + "_" + txtaccno .Text+"_"+ Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    Clear();
                }
                else
                {
                    WebMsgBox.Show("Cannot update autorized data",this.Page);
                }
            }
            else if (Rdb_No.SelectedValue == "2")
            {
                if (txtBR2no.Text != null)
                {
                    BRCD=txtBR2no.Text;
                }
                result = LAU.LastNoUpdate(BRCD);
                if (result > 0)
                {
                    WebMsgBox.Show("Updated Successfully!!!!", this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "LastNoUpdate _Update_" + txtprdcode.Text + "_" + txtaccno.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    Clear();
                }
               
             }
        }
        catch (Exception Ex)
        {
                ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_Clear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    public void Clear()
    {
        txtbrcdno.Text = "";
        txtbrcdname.Text = "";
        txtprdcode.Text = "";
        txtprdname.Text = "";
        txtaccno.Text = "";
        txtaccname.Text = "";
        txtBR2no.Text = "";
        txtBR2name.Text = "";
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
                ViewState["GLCODE"] = GLS[1].ToString();
                autoacc.ContextKey = Session["BRCD"].ToString() + "_" + txtprdcode.Text + "_" + ViewState["GLCODE"].ToString();
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
}