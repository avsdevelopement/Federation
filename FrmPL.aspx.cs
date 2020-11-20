using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmPL : System.Web.UI.Page
{
    ClsPLTransfer PL = new ClsPLTransfer(); 
    ClsPLTransfer PLT = new ClsPLTransfer();
    ClsEncryptValue EV = new ClsEncryptValue();
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
           txtDate.Text = Session["EntryDate"].ToString();
           txtDT.Text = Session["EntryDate"].ToString();
        }
    }
    protected void txtReport_Click(object sender, EventArgs e)
    {
         try
        {
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "PL" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?EDate=" + Session["EntryDate"].ToString() + "&Date=" + txtDate.Text + "&AC=" + txtPL.Text + "&BRCD=" + Session["BRCD"].ToString() + "&UserName=" + Session["MID"].ToString() + "&Flag=Show&rptname=RptPLTransfer.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        btnPost.Enabled = false;
        DataTable dt=new DataTable();
        string MID = EV.GetMK(Session["MID"].ToString());
        dt = PL.GetPLRecord(txtDate.Text, txtPL.Text, Session["BRCD"].ToString(), "Transfer", Session["EntryDate"].ToString(), Session["MID"].ToString(),MID);
        if (dt.Rows.Count > 0)
        WebMsgBox.Show("Data posted Successfully!!! Set No="+dt.Rows[0]["SetNo"], this.Page);
        else
            WebMsgBox.Show("No Records Found", this.Page);
        txtPL.Text = "";
        txtName.Text = "";
        txtDate.Text = "";
            btnPost.Enabled=true;
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "PL_Post" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
    }
    protected void txtPL_TextChanged(object sender, EventArgs e)
    {
        Div_SPECIFIC.Visible = true;
        DIV_Div.Visible = false;
        DataTable DTId = new DataTable();
        DataTable DTName = new DataTable();
        DTId = PL.GetGLCode(txtPL.Text,Session["BRCD"].ToString());
        if (DTId.Rows.Count == 0 || DTId.Rows.Count == null)
        {
            txtPL.Text = "";
            txtName.Text = "";
            WebMsgBox.Show("Invalid Sub Gl code!!!", this.Page);
        }
        else
            txtName.Text = DTId.Rows[0]["GLNAME"].ToString();
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void txtDivTo_TextChanged(object sender, EventArgs e)
    {
        Div_SPECIFIC.Visible = false;
        DIV_Div.Visible = true;
        DataTable DTId = new DataTable();
        DataTable DTName = new DataTable();
        DTId = PL.GetGLCode(txtDivTo.Text, txtBRCD.Text);
        if (DTId.Rows.Count == 0 || DTId.Rows.Count == null)
        {
            txtDivTo.Text = "";
            txtDivFrom.Text = "";
            WebMsgBox.Show("Invalid Sub Gl code!!!", this.Page);
        }
        else
            txtDivFrom.Text = DTId.Rows[0]["GLNAME"].ToString();
    }
    protected void txtPrdFrm_TextChanged(object sender, EventArgs e)
    {
        Div_SPECIFIC.Visible = false;
        DIV_Div.Visible = true;
        DataTable DTId = new DataTable();
        DataTable DTName = new DataTable();
        DTId = PL.GetGLCode(txtPrdFrm.Text, Session["BRCD"].ToString());
        if (DTId.Rows.Count == 0 || DTId.Rows.Count == null)
        {
            txtPrdFrm.Text = "";
            txtPrdNameFrm.Text = "";
            WebMsgBox.Show("Invalid Sub Gl code!!!", this.Page);
        }
        else
        {
            txtPrdNameFrm.Text = DTId.Rows[0]["GLNAME"].ToString();
            ViewState["GLCode"] = DTId.Rows[0]["GLCOde"].ToString();
        }
    }
    protected void btnDivReport_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "PL_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?EDate=" + txtDT.Text + "&BRCD=" + Session["BRCD"].ToString() + "&GLCode=" + ViewState["GLCode"].ToString() + "&MID=" + Session["MID"].ToString() + "&SubGlCode=" + txtPrdFrm.Text + "&rptname=RptDivIntitalize.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnDivPost_Click(object sender, EventArgs e)
    {
        string  Result = "0";
        string MID = EV.GetMK(Session["MID"].ToString());
        Result = PLT.InsertDividend(txtDT.Text, Session["BRCD"].ToString(), ViewState["GLCode"].ToString(), txtPrdFrm.Text, Session["MID"].ToString(), txtBRCD.Text, txtDivTo.Text, Session["EntryDate"].ToString(), MID);
        if (Result!="0")
        {
            WebMsgBox.Show("Data Transfer Successfully!!!SetNo=" + Result, this.Page);
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "PL_Post" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        else
        {
            WebMsgBox.Show("Fail", this.Page);
        }
    }
    protected void rbOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbOption.SelectedValue == "1")
        {
            Div_SPECIFIC.Visible = true;
            DIV_Div.Visible = false;
        }
        else
        {
            Div_SPECIFIC.Visible = false;
            DIV_Div.Visible = true;
        }
    }
}