using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmAVS5045 : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsAVS5045 avs5045 = new ClsAVS5045();
    ClsLoanInfo LI = new ClsLoanInfo();
    int res = 0;
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

                string usr = LI.GETUSERGRP(Session["MID"].ToString());
                if (usr != "1")
                {
                    HttpContext.Current.Response.Redirect("FrmBlank.aspx?ShowMessage=msg", true);
                }

                TxtBrcd.Text = Session["BRCD"].ToString();
                TxtBrcdName.Text = AST.GetBranchName(TxtBrcd.Text);
                bindgrd();
               
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtBrcd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBrcd.Text != "")
            {
                string bname = AST.GetBranchName(TxtBrcd.Text);
                if (bname != null)
                {
                    TxtBrcdName.Text = bname;
                    bindgrd();
                    TxtMsg.Focus();
                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    TxtBrcd.Text = "";
                    TxtBrcd.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                TxtBrcd.Text = "";
                TxtBrcd.Focus();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
           res= avs5045.insert(TxtBrcd.Text, TxtMsg.Text, TxtActivity.Text, ddlPara.SelectedValue, Session["MID"].ToString());
           if (res > 0)
           {
               WebMsgBox.Show("Record Added Successfully..!!", this.Page);
             //  tab1.Visible = false;
               clear();
               bindgrd();
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
            res = avs5045.Modify(ViewState["ID"].ToString(), TxtBrcd.Text, TxtMsg.Text, TxtActivity.Text, ddlPara.SelectedValue, Session["MID"].ToString());
            if (res > 0)
            {
                WebMsgBox.Show("Record Modified Successfully..!!", this.Page);
               // tab1.Visible = false;
                clear();
                bindgrd();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnAuthorise_Click(object sender, EventArgs e)
    {
        try
        {
            res = avs5045.Authorise(ViewState["ID"].ToString(), TxtBrcd.Text);
            if (res > 0)
            {
                WebMsgBox.Show("Record Authorised Successfully..!!", this.Page);
             //   tab1.Visible = false;
                clear();
                bindgrd();
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
            res = avs5045.Delete(ViewState["ID"].ToString(), TxtBrcd.Text);
            if (res > 0)
            {
                WebMsgBox.Show("Record Deleted Successfully..!!", this.Page);
             //   tab1.Visible = false;
                clear();
                bindgrd();
            }
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
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    public void clear()
    {
        TxtBrcd.Text = "";
        TxtBrcdName.Text = "";
        TxtActivity.Text = "";
        TxtMsg.Text = "";
        ddlPara.SelectedValue = "0";
    }
    public void bindgrd()
    {
        try
        {
            avs5045.bind(GrdSMSPara);
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
            dt = avs5045.getdetails(id);
            if(dt.Rows.Count>0)
            {
                TxtBrcd.Text = dt.Rows[0]["BRCD"].ToString();
                TxtBrcdName.Text = AST.GetBranchName(TxtBrcd.Text);
                TxtMsg.Text = dt.Rows[0]["Message"].ToString();
                TxtActivity.Text = dt.Rows[0]["Activity"].ToString();
                ddlPara.SelectedValue = dt.Rows[0]["Parameter"].ToString();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        clear();
        BtnSubmit.Visible = true;
        BtnModify.Visible = false;
        BtnDelete.Visible = false;
        BtnAuthorise.Visible = false;
        TxtBrcd.Text = Session["BRCD"].ToString();
        TxtBrcdName.Text = AST.GetBranchName(TxtBrcd.Text);
    }
    protected void lnkMod_Click(object sender, EventArgs e)
    {
        try
        {
            BtnSubmit.Visible = false;
            BtnModify.Visible = true;
            BtnDelete.Visible = false;
            BtnAuthorise.Visible = false;
            LinkButton objlink = (LinkButton)sender;
            string strnumId = objlink.CommandArgument;
            ViewState["ID"] = strnumId.ToString();
            viewdetails(ViewState["ID"].ToString());
          //  tab1.Visible = true;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkDel_Click(object sender, EventArgs e)
    {
        try
        {
            BtnSubmit.Visible = false;
            BtnModify.Visible = false;
            BtnDelete.Visible = true;
            BtnAuthorise.Visible = false;
            LinkButton objlink = (LinkButton)sender;
            string strnumId = objlink.CommandArgument;
            ViewState["ID"] = strnumId.ToString();
            viewdetails(ViewState["ID"].ToString());
          //  tab1.Visible = true;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkAuth_Click(object sender, EventArgs e)
    {
        try
        {
            BtnSubmit.Visible = false;
            BtnModify.Visible = false;
            BtnDelete.Visible = false;
            BtnAuthorise.Visible = true;
            LinkButton objlink = (LinkButton)sender;
            string strnumId = objlink.CommandArgument;
            ViewState["ID"] = strnumId.ToString();
            viewdetails(ViewState["ID"].ToString());
           // tab1.Visible = true;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}