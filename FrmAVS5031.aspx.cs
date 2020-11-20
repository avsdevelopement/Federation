using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmAVS5031 : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsAVS5031 avs5031 = new ClsAVS5031();
    ClsBindDropdown bd = new ClsBindDropdown();
    int res = 0;
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
                bd.BindHeading(ddlHeading);
                bd.BindHeadingCover(ddlheading1);
                if (ddlSelection.SelectedValue == "0")
                {
                    div_Cover.Visible = true;
                    div_trans.Visible = false;
                    div_passbk.Visible = false;
                    Div_covergrd.Visible = true;
                    bindgrid();
                    ddlheading1.Focus();
                }
                else
                {
                    div_Cover.Visible = false;
                    div_trans.Visible = true;
                    div_passbk.Visible = true;
                    Div_covergrd.Visible = false;
                    bindgrid();
                    ddlHeading.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void ddlHeading_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnADD_Click(object sender, EventArgs e)
    {
        try
        {
            res = avs5031.insert(ddlHeading.SelectedItem.Text, TxtBAlign.Text, TxtAAlign.Text, Session["MID"].ToString(), ddlHeading.SelectedValue, TxtCNO.Text, txtGlcode.Text);
            if (res > 0)
            {
                WebMsgBox.Show("Data Saved Successfully..!!", this.Page);
                bindgrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "PassbookSetting_Add _"+ Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                clear();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnModify_Click(object sender, EventArgs e)
    {
        try
        {
            res = avs5031.Modify(ViewState["ID"].ToString(), ddlHeading.SelectedItem.Text, TxtBAlign.Text, TxtAAlign.Text, Session["MID"].ToString(), ddlHeading.SelectedValue, TxtCNO.Text, txtGlcode.Text);
            if (res > 0)
            {
                WebMsgBox.Show("Data Modified Successfully..!!", this.Page);
                bindgrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "PassbookSetting_Mod _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                clear();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            res = avs5031.Delete(ViewState["ID"].ToString());
            if (res > 0)
            {
                WebMsgBox.Show("Data Deleted Successfully..!!", this.Page);
                bindgrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "PassbookSetting_Del _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void GrdPassbook_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string strnumId = objlink.CommandArgument;
            ViewState["ID"] = strnumId.ToString();
            viewdetails(ViewState["ID"].ToString());
            BtnADD.Visible = false;
            BtnModify.Visible = true;
            BtnDelete.Visible = false;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string strnumId = objlink.CommandArgument;
            ViewState["ID"] = strnumId.ToString();
            viewdetails(ViewState["ID"].ToString());
            BtnADD.Visible = false;
            BtnModify.Visible = false;
            BtnDelete.Visible = true;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void viewdetails(string id)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = avs5031.getdetails(id);
            if (dt.Rows.Count > 0)
            {
                ddlHeading.SelectedValue = dt.Rows[0]["ColumnStatus"].ToString();
                TxtAAlign.Text = dt.Rows[0]["Arows"].ToString();
                TxtBAlign.Text = dt.Rows[0]["BRows"].ToString();
                TxtCNO.Text = dt.Rows[0]["Cno"].ToString();
                txtGlcode.Text = dt.Rows[0]["glcode"].ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void bindgrid()
    {
        try
        {
            avs5031.binddetails(GrdPassbook);
            avs5031.bindcoverdetails(grdCover);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            div_trans.Visible = true;
            div_Cover.Visible = false;
            div_passbk.Visible = true;
            Div_covergrd.Visible = false;
            BtnSub.Visible = false;
            BtnMod.Visible = false;
            BtnDel.Visible = false;
            clear();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void clear()
    {
        ddlHeading.SelectedValue = "0";
        TxtAAlign.Text = "";
        TxtBAlign.Text = "";
        TxtCNO.Text = "";
    }
    protected void ddlSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSelection.SelectedValue == "0")
            {
                div_Cover.Visible = true;
                div_trans.Visible = false;
                div_passbk.Visible = false;
                Div_covergrd.Visible = true;
                bindgrid();
                ddlheading1.Focus();
            }
            else
            {
                div_Cover.Visible = false;
                div_trans.Visible = true;
                div_passbk.Visible = true;
                Div_covergrd.Visible = false;
                bindgrid();
                ddlHeading.Focus();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void ddlheading1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnSub_Click(object sender, EventArgs e)
    {
        try
        {
            res = avs5031.insertCover(ddlheading1.SelectedItem.Text, TxtRowNo.Text, TxtColNo.Text, Session["MID"].ToString(), ddlheading1.SelectedValue);
            if (res > 0)
            {
                WebMsgBox.Show("Data Saved Successfully..!!", this.Page);
                bindgrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "PassbookSetting_Add _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                clear();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnMod_Click(object sender, EventArgs e)
    {
        try
        {
            res = avs5031.ModifyCover(ViewState["ID1"].ToString(), ddlheading1.SelectedItem.Text, TxtRowNo.Text, TxtColNo.Text, Session["MID"].ToString(), ddlheading1.SelectedValue);
            if (res > 0)
            {
                WebMsgBox.Show("Data Modified Successfully..!!", this.Page);
                bindgrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "PassbookSetting_Mod _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                clear();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnDel_Click(object sender, EventArgs e)
    {
        try
        {
            res = avs5031.DeleteCover(ViewState["ID1"].ToString());
            if (res > 0)
            {
                WebMsgBox.Show("Data Deleted Successfully..!!", this.Page);
                bindgrid();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "PassbookSetting_Del _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkAdd1_Click(object sender, EventArgs e)
    {
        try
        {
            div_trans.Visible = false;
            div_Cover.Visible = true;
            div_passbk.Visible = false;
            Div_covergrd.Visible = true;
            BtnSub.Visible = true;
            BtnMod.Visible = false;
            BtnDel.Visible = false;
            clear();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkSelect1_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string strnumId = objlink.CommandArgument;
            ViewState["ID1"] = strnumId.ToString();
            viewdetailsco(ViewState["ID1"].ToString());
            BtnSub.Visible = false;
            BtnMod.Visible = true;
            BtnDel.Visible = false;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string strnumId = objlink.CommandArgument;
            ViewState["ID1"] = strnumId.ToString();
            viewdetailsco(ViewState["ID1"].ToString());
            BtnSub.Visible = false;
            BtnMod.Visible = false;
            BtnDel.Visible = true;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void viewdetailsco(string id)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = avs5031.getdetailscover(id);
            if (dt.Rows.Count > 0)
            {
                ddlheading1.SelectedValue = dt.Rows[0]["ColumnStatus"].ToString();
                TxtRowNo.Text = dt.Rows[0]["PRows"].ToString();
                TxtColNo.Text = dt.Rows[0]["PColumn"].ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void grdCover_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}