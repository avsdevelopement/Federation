using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

public partial class FrmAVS5159 : System.Web.UI.Page
{
    ClsAVS5159 DDS = new ClsAVS5159();
    DataTable DT = new DataTable();
    string sResult = "";
    int Result = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
                Response.Redirect("FrmLogin.aspx");

            if (!IsPostBack)
            {
                DDS.BindBranch(ddlBrName, null);
                txtBrCode.Text = "0";

                ddlBrName.Focus();
            }
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 900000;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlBrName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtBrCode.Text = "";
            txtBrCode.Text = ddlBrName.SelectedValue.ToString();
            if (txtAgentCode.Text == "")
                ProdCodeBranch();
            else
                ProdCode();
            txtAgentCode.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtAgentCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ProdCode();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtAgentName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] custnob = txtAgentName.Text.ToString().Split('_');
            if (custnob.Length > 1)
            {
                txtAgentCode.Text = (string.IsNullOrEmpty(custnob[2].ToString()) ? "" : custnob[2].ToString());
                ProdCode();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtEDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if ((Convert.ToString(txtEDate.Text.ToString()) != "") && (Convert.ToString(txtEDate.Text.Length.ToString()) == "10"))
            {
                DT = new DataTable();
                DT = DDS.GetMobAppBalance(Session["BankCode"].ToString(), txtBrCode.Text.ToString(), txtAgentCode.Text.ToString(), txtEDate.Text.ToString(), Session["MID"].ToString());
                if (DT.Rows.Count > 0)
                {
                    txtOnlineColl.Text = DT.Rows[0]["OnlineColl"].ToString();
                }

                txtEDate.Focus();
            }
            else
            {
                txtEDate.Focus();
                WebMsgBox.Show("Enter proper date first ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ProdCode()
    {
        DT = new DataTable();
        try
        {
            DT = DDS.GetProdDetails(Convert.ToString(txtBrCode.Text.ToString()), Convert.ToString(txtAgentCode.Text.ToString()));
            if (DT.Rows.Count > 0)
            {
                if (Convert.ToString(DT.Rows[0]["UnOperate"].ToString()) != "3")
                {
                    ViewState["GlCode"] = Convert.ToString(DT.Rows[0]["GlCode"].ToString()).ToString();
                    txtAgentCode.Text = Convert.ToString(DT.Rows[0]["SubGlCode"].ToString()).ToString();
                    txtAgentName.Text = Convert.ToString(DT.Rows[0]["GlName"].ToString()).ToString();
                    txtEDate.Text = Session["EntryDate"].ToString();

                    if (Convert.ToString(txtAgentName.Text.ToString()) != "")
                    {
                        sResult = DDS.GetSubGlCode(txtBrCode.Text.ToString());
                        string[] TD = Session["EntryDate"].ToString().Split('/');
                        txtAgentBalance.Text = DDS.GetOpenClose("MAIN_CLOSING", TD[2].ToString(), TD[1].ToString(), txtBrCode.Text.ToString(), "6", sResult.ToString(), txtAgentCode.Text.ToString(), Session["EntryDate"].ToString()).ToString();

                        DT = new DataTable();
                        DT = DDS.GetMobAppBalance(Session["BankCode"].ToString(), txtBrCode.Text.ToString(), txtAgentCode.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                        if (DT.Rows.Count > 0)
                        {
                            txtOnlineColl.Text = DT.Rows[0]["OnlineColl"].ToString();
                            txtMobDataSend.Text = DT.Rows[0]["MobDataSend"].ToString();
                        }

                        DT = new DataTable();
                        DT = DDS.GetCBSBalance(Session["BankCode"].ToString(), txtBrCode.Text.ToString(), txtAgentCode.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
                        if (DT.Rows.Count > 0)
                        {
                            txtMobDataRec.Text = DT.Rows[0]["MobDataRec"].ToString();
                            txtPostingData.Text = DT.Rows[0]["PostingData"].ToString();
                        }

                        btnMobDataSend.Focus();
                    }
                    else
                    {
                        txtAgentCode.Text = "";
                        txtAgentName.Text = "";
                        txtAgentCode.Focus();
                        WebMsgBox.Show("Enter valid product code ...!!", this.Page);
                        return;
                    }
                }
                else
                {
                    txtAgentCode.Text = "";
                    txtAgentName.Text = "";
                    txtAgentCode.Focus();
                    WebMsgBox.Show("Agent is not operating ...!!", this.Page);
                    return;
                }
            }
            else
            {
                txtAgentCode.Text = "";
                txtAgentName.Text = "";
                txtAgentCode.Focus();
                WebMsgBox.Show("Enter valid product code ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }


    protected void ProdCodeBranch()
    {
        DT = new DataTable();
        try
        {
            txtEDate.Text = Session["EntryDate"].ToString();
            DT = new DataTable();
            DT = DDS.GetMobAppBranchBalance(Session["BankCode"].ToString(), txtBrCode.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (DT.Rows.Count > 0)
            {
                txtOnlineColl.Text = DT.Rows[0]["OnlineColl"].ToString();
                txtMobDataSend.Text = DT.Rows[0]["MobDataSend"].ToString();
            }

            DT = new DataTable();
            DT = DDS.GetCBSBranchBalance(Session["BankCode"].ToString(), txtBrCode.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (DT.Rows.Count > 0)
            {
                txtMobDataRec.Text = DT.Rows[0]["MobDataRec"].ToString();
                txtPostingData.Text = DT.Rows[0]["PostingData"].ToString();
            }


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ClearAllData()
    {
        try
        {
            ddlBrName.SelectedValue = "0";
            txtBrCode.Text = ddlBrName.SelectedValue;
            txtAgentCode.Text = "";
            txtAgentName.Text = "";
            txtAgentBalance.Text = "";
            txtOnlineColl.Text = "";
            txtMobDataSend.Text = "";
            txtMobDataRec.Text = "";
            txtPostingData.Text = "";

            ddlBrName.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnMobDataSend_Click(object sender, EventArgs e)
    {
        try
        {
            Result = DDS.OnlineCollection(Session["BankCode"].ToString(), txtBrCode.Text.ToString(), txtAgentCode.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (Result > 0)
            {
                btnMobDataSend.Focus();
                WebMsgBox.Show("Successfully Completed ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnUnAuthorize_Click(object sender, EventArgs e)
    {
        try
        {
            Result = DDS.UnAuthorize(Session["BankCode"].ToString(), txtBrCode.Text.ToString(), txtAgentCode.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (Result > 0)
            {
                btnMobDataSend.Focus();
                WebMsgBox.Show("Successfully Un-Authorized ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnDeleteData_Click(object sender, EventArgs e)
    {
        try
        {
            Result = DDS.DeleteDate(Session["BankCode"].ToString(), txtBrCode.Text.ToString(), txtAgentCode.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString());
            if (Result > 0)
            {
                btnMobDataSend.Focus();
                WebMsgBox.Show("Successfully Deleted ...!!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAllData();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}