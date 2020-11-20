using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmBranchAdd : System.Web.UI.Page
{
    ClsAddBranch Cbr = new ClsAddBranch();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["Flag"] != null)
                {
                    if (Session["UserName"] == null)
                    {
                        Response.Redirect("FrmLogin.aspx");
                    }
                    ViewState["Flag"] = Request.QueryString["Flag"].ToString();
                    getnamdetails();
                    if (ViewState["Flag"].ToString() == "AD")
                    {
                        Submit.Text = "Submit";
                        LBtn_Add.Visible = false;
                    }
                    else if (ViewState["Flag"].ToString() == "MD")
                    {
                        Submit.Text = "Modify";
                        LBtn_Mod.Visible = false;
                    }
                    else if (ViewState["Flag"].ToString() == "VW")
                    {
                        Submit.Text = "View";
                        LBtn_View.Visible = false;
                    }
                    else
                    {

                    }
                }
                else
                {
                    Submit.Text = "Select Flag";
                }
            }
            catch (Exception EX)
            {
                ExceptionLogging.SendErrorToText(EX);
            }
            
        }
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Flag"].ToString() == "AD")
            {
                string Flag="Insert";
                int Result = Cbr.InsertBrData(Flag, txtbankcode.Text, txtBrcode.Text, txtbankname.Text, Txtbrname.Text, txtadd1.Text, txtadd2.Text, txtmobile.Text, txtEmail.Text, txtreg.Text, txtbanksname.Text, txtAdmingl.Text, txtadminsubgl.Text, txtdayopen.Text);
                if (Result > 0)
                {
                   
                    lblMessage.Text = "Branch Create Successfully..!";
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "BranchCreate_Add _" + txtbankcode.Text + "_" + txtBrcode.Text + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    ClearAll();
                    return;
                
                }
            }
            else if(ViewState["Flag"].ToString() == "MD")
            {
                string Flag = "Modify";
                //string InOutSetno = Convert.ToString(Convert.ToInt32(txtdaysetno.Text) +Convert.ToInt32("1000"));
                int Result = Cbr.updateBrData(Flag, txtbankcode.Text, txtBrcode.Text, txtbankname.Text, Txtbrname.Text, txtadd1.Text, txtadd2.Text, txtmobile.Text, txtEmail.Text, txtreg.Text, txtbanksname.Text, txtAdmingl.Text, txtadminsubgl.Text, txtdayopen.Text);
                if (Result > 0)
                {
                   
                    lblMessage.Text = "Record Modified Successfully..!";
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "BranchCreate_Mod _" + txtbankcode.Text + "_" + txtBrcode.Text + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    ClearAll();
                    return;
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
        ClearAll();
    }
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void txtbankcode_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Txtbrname_TextChanged(object sender, EventArgs e)
    {

    }
    public void ClearAll()
    {
        txtbankcode.Text = "";
        txtbankname.Text = "";
        txtBrcode.Text = "";
        Txtbrname.Text = "";
        txtmobile.Text = "";
        txtEmail.Text = "";
        txtadd1.Text = "";
        txtadd2.Text = "";
        txtreg.Text = "";
        txtbanksname.Text = "";
        txtAdmingl.Text = "";
        txtadminsubgl.Text = "";
        txtdayopen.Text = "";
    
    }
    public void getnamdetails()
    {
        try
        {
            DataTable DT = new DataTable();
            DT = Cbr.getbankdetails();
            txtbankcode.Text = DT.Rows[0]["bankcd"].ToString();
            txtbankname.Text = DT.Rows[0]["bankname"].ToString();
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }

    
    }
    protected void LBtn_Add_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBranchAdd.aspx?Flag=AD", true);
    }
    protected void LBtn_Mod_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBranchAdd.aspx?Flag=MD", true);
    }
    protected void LBtn_View_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBranchAdd.aspx?Flag=VW", true);
    }
    protected void txtBrcode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Flag"].ToString() == "AD")
            {
                string brcd = Cbr.getbrcode(txtBrcode.Text);
                if (brcd != null && brcd != "")
                {
                    txtBrcode.Text = "";
                    txtBrcode.Focus();
                    WebMsgBox.Show("Please Enter Another Br Code you enter existing BrCode", this.Page);
                    
                }
                else
                {
                    Txtbrname.Focus();
                }
            }
            if (ViewState["Flag"].ToString() == "MD" || ViewState["Flag"].ToString() == "VW")
            {
                DataTable DT = new DataTable();
                DT = Cbr.getData(txtBrcode.Text, txtbankcode.Text);
                if (DT.Rows.Count > 0)
                {
                    txtbankcode.Text = DT.Rows[0]["BANKCD"].ToString();
                    txtbankname.Text = DT.Rows[0]["BANKNAME"].ToString();
                    txtBrcode.Text = DT.Rows[0]["BRCD"].ToString();
                    Txtbrname.Text = DT.Rows[0]["MIDNAME"].ToString();
                    txtmobile.Text = DT.Rows[0]["MOBILE"].ToString();
                    txtEmail.Text = DT.Rows[0]["EMAIL"].ToString();
                    txtadd1.Text = DT.Rows[0]["ADDRESS1"].ToString();
                    txtadd2.Text = DT.Rows[0]["ADDRESS2"].ToString();
                    txtreg.Text = DT.Rows[0]["REGISTRATIONNO"].ToString();
                    txtbanksname.Text = DT.Rows[0]["BankNm_Short"].ToString();
                    txtAdmingl.Text = DT.Rows[0]["ADMGlCode"].ToString();
                    txtadminsubgl.Text = DT.Rows[0]["ADMSubGlCode"].ToString();
                    txtdayopen.Text = DT.Rows[0]["LISTVALUE"].ToString();
                }
                else
                {
                    WebMsgBox.Show("Record Not Found", this.Page);
                }
            }
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}