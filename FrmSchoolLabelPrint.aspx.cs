using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmSchoolLabelPrint : System.Web.UI.Page
{
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsAccopen accop = new ClsAccopen();
    Cls_RecoBindDropdown BD1 = new Cls_RecoBindDropdown();
    string ACC1, ACC2, PWD = "";
    string FL = "";
    string[] cname, cname1, cname2;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindRecDiv();
                if (!string.IsNullOrEmpty(Request.QueryString["CUSTNO"]))
                {
                    TxtFBRCD.Text = Session["Brcd"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtFBRCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFBRCD.Text != "")
            {
                string bname = AST.GetBranchName(TxtFBRCD.Text);
                if (bname != null)
                {
                    TxtFBRCDName.Text = bname;

                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    TxtFBRCD.Text = "";
                    TxtFBRCD.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                TxtFBRCD.Text = "";
                TxtFBRCD.Focus();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtFMemNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            cname2 = accop.Getcustname(TxtFMemNo.Text.ToString()).Split('_');

            TxtFMemName.Text = cname2[0].ToString();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtTMemNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            cname1 = accop.Getcustname(TxtTMemNo.Text.ToString()).Split('_');

            TxtTMemName.Text = cname1[0].ToString();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtFMemNo_TextChanged1(object sender, EventArgs e)
    {

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtFMemNo.Text == "" || TxtTMemNo.Text == "")
            {
                WebMsgBox.Show("Please Enter Account Number..!!", this.Page);
                return;
            }
            if (DdlRecDept.SelectedValue.ToString() == "")
            {
                DdlRecDept.SelectedValue = "0";
            }
            ACC1 = TxtFMemNo.Text;
            ACC2 = TxtTMemNo.Text;

            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "AddressLabelPrint_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?BRCD=" + TxtFBRCD.Text + "&FAccno=" + TxtFMemNo.Text + "&TAccno=" + TxtTMemNo.Text.ToString() + "&FDate=" + TxtFDate.Text + "&Div=" + DdlRecDiv.SelectedValue.ToString() + "&Dep=" + DdlRecDept.SelectedValue.ToString() + "&rptname=RptAddressLabelPrint_TZMP.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        try
        {
            clear();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void clear()
    {
        try
        {
            TxtFMemNo.Text = "";
            TxtFMemName.Text = "";
            TxtTMemNo.Text = "";
            TxtTMemName.Text = "";
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
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
    public void BindRecDept()
    {
        try
        {
            BD1.BRCD = TxtFBRCD.Text;
            BD1.Ddl = DdlRecDept;
            BD1.RECDIV = DdlRecDiv.SelectedValue.ToString();
            BD1.FnBL_BindRecDept(BD1);
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
            BD1.BRCD = TxtFBRCD.Text;
            BD1.Ddl = DdlRecDiv;
            BD1.FnBL_BindRecDiv(BD1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}