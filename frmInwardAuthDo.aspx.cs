using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class frmInwordAuthoDo : System.Web.UI.Page
{
    int setNo, scrollNo, result;
    string op = "";
    DataTable dt = new DataTable();
    scustom customcs = new scustom();
    ClsOutAuthDo maincode = new ClsOutAuthDo();
    ClsOutAuthReturnDo maincode2 = new ClsOutAuthReturnDo();
    ClsOutClear OWGCL = new ClsOutClear();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            customcs.BindInstruType(ddlinsttype);
        }

        setNo = Convert.ToInt32(Request.QueryString["setno"]);
        scrollNo = Convert.ToInt32(Request.QueryString["scrollno"]);
        op = Request.QueryString["op"].ToString();


        if (op == "delete")
        {
            btnAuthorize.Text = "Cancel";
            ViewState["op"] = "delete";
        }
        else if (op == "authorize")
        {
            btnAuthorize.Text = "Authorize";
            ViewState["op"] = "authorize";
        }
        else if (op == "return")
        {
            btnAuthorize.Text = "return";
            ViewState["op"] = "return";
        }
        else if (op == "returnauth")
        {
            btnAuthorize.Text = "returnauth";
            ViewState["op"] = "returnauth";
        }

        if (!IsPostBack)
        {
            if (ViewState["op"] == "delete" || ViewState["op"] == "authorize")
            {
                dt = maincode.GetFormData(setNo, scrollNo, Convert.ToInt32(Session["BRCD"]),Session["EntryDate"].ToString(),"I");
            }

            if (ViewState["op"] == "return" || ViewState["op"] == "returnauth")
            {
                dt = maincode2.GetFormDatareturn(setNo, scrollNo, Convert.ToInt32(Session["BRCD"]), Session["EntryDate"].ToString(), "I");
            }

            if (dt.Rows.Count > 0)
            {
                TxtEntrydate.Text = Convert.ToDateTime(dt.Rows[0]["ENTRYDATE"]).ToString("dd-MM-yyyy");
                txtsetno.Text = dt.Rows[0]["SET_NO"].ToString();
                TxtProcode.Text = dt.Rows[0]["PRDUCT_CODE"].ToString();
                TxtProName.Text = dt.Rows[0]["PRDNAME"].ToString();
                TxtAccNo.Text = dt.Rows[0]["ACC_NO"].ToString();
                TxtAccName.Text = dt.Rows[0]["CUSTNAME"].ToString();
                txtAccTypeid.Text = dt.Rows[0]["ACC_TYPE"].ToString();
                TxtAccTypeName.Text = dt.Rows[0]["ACCTYPEA"].ToString();
                txtOpTypeId.Text = dt.Rows[0]["OPRTN_TYPE"].ToString();
                TxtOpTypeName.Text = dt.Rows[0]["OPRTYPE"].ToString();
                txtpartic.Text = dt.Rows[0]["PARTICULARS"].ToString();
                ddlinsttype.SelectedValue = dt.Rows[0]["INSTRU_TYPE"].ToString();
                txtbankcd.Text = dt.Rows[0]["BANK_CODE"].ToString();
                txtbnkdname.Text = dt.Rows[0]["BANK"].ToString();
                txtbrnchcd.Text = dt.Rows[0]["BRANCH_CODE"].ToString();
                txtbrnchcdname.Text = dt.Rows[0]["BRANCH"].ToString();
                txtinstno.Text = dt.Rows[0]["INSTRU_NO"].ToString();
                txtinstdate.Text = Convert.ToDateTime(dt.Rows[0]["INSTRUDATE"]).ToString("dd-MM-yyyy");
                txtinstamt.Text = dt.Rows[0]["INSTRU_AMOUNT"].ToString();
            }
        }

        TxtEntrydate.Focus();
    }
    protected void btnAuthorize_Click(object sender, EventArgs e)
    {
        // Get CLG_GL_NO 
        string CLG_GL_NO = OWGCL.Get_CLG_GL_NO_return(Session["BRCD"].ToString()).ToString();

        // Get PACMAC
        string PACMAC = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.ToString();


        if (ViewState["op"] == "authorize")
        {
            result = maincode.Authorize(1003, setNo, scrollNo, Convert.ToInt32(Session["BRCD"]), Session["MID"].ToString());
            if (result > 0)
            {
                WebMsgBox.Show("Record Authorized successfully", this.Page);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();window.opener.location.href=window.opener.location.href;", true);
            }
        }

        if (ViewState["op"] == "delete")
        {
            result = maincode.cancel(1004, setNo, scrollNo, Convert.ToInt32(Session["BRCD"]), Session["MID"].ToString());
            if (result > 0)
            {
                WebMsgBox.Show("Record Canceled successfully", this.Page);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closePage", "window.close();", true);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();window.opener.location.href=window.opener.location.href;", true);
            }
        }

        if (ViewState["op"] == "return")
        {
            maincode2.returnout("1002", "3", txtsetno.Text.ToString(), scrollNo, Session["BRCD"].ToString(), Session["MID"].ToString(), TxtEntrydate.Text.ToString(), TxtProcode.Text.ToString(), TxtAccNo.Text.ToString(), txtAccTypeid.Text.ToString(), txtOpTypeId.Text.ToString(), txtpartic.Text.ToString(), txtbankcd.Text.ToString(), txtbrnchcd.Text.ToString(), ddlinsttype.SelectedValue.ToString(), txtinstdate.Text.ToString(), txtinstno.Text.ToString(), txtinstamt.Text.ToString(), PACMAC, CLG_GL_NO);

            WebMsgBox.Show("Record Return successfully", this.Page);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();window.opener.location.href=window.opener.location.href;", true);

        }

        if (ViewState["op"] == "returnauth")
        {
            maincode2.returnoutauth("1003", "3", txtsetno.Text.ToString(), scrollNo, Session["BRCD"].ToString(), Session["MID"].ToString(), TxtEntrydate.Text.ToString(), TxtProcode.Text.ToString(), TxtAccNo.Text.ToString(), txtAccTypeid.Text.ToString(), txtOpTypeId.Text.ToString(), txtpartic.Text.ToString(), txtbankcd.Text.ToString(), txtbrnchcd.Text.ToString(), ddlinsttype.SelectedValue.ToString(), txtinstdate.Text.ToString(), txtinstno.Text.ToString(), txtinstamt.Text.ToString(), PACMAC, CLG_GL_NO);
            WebMsgBox.Show("Record Return successfully", this.Page);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();window.opener.location.href=window.opener.location.href;", true);
        } 
    }
    protected void txtbankcd_TextChanged(object sender, EventArgs e)
    {

    }
}