using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmMenuMaster : System.Web.UI.Page
{
    ClsMenuMaster MM = new ClsMenuMaster();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    int resultint = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

        }
    }

    protected void txtMenuId_TextChanged(object sender, EventArgs e)
    {

    }

    protected void txtMenuTitle_TextChanged(object sender, EventArgs e)
    {

    }

    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Flag"] = "AD";
            Submit.Text = "Submit";
            ClearData();
            ENDN(true);
            lblActivity.Text = "Add Menu";
            grdMenuMaster.Visible = false;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkModify_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Flag"] = "MD";
            Submit.Text = "Modify";
            ClearData();
            ENDN(true);
            lblActivity.Text = "Modify Menu";
            BindGrid();
            grdMenuMaster.Visible = true;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Flag"] = "DL";
            ENDN(false);
            ClearData();
            Submit.Text = "Delete";
            lblActivity.Text = "Delete Menu";
            BindGrid();
            grdMenuMaster.Visible = true;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearData()
    {
        try
        {
            txtMenuId.Text = "";
            txtMenuTitle.Text = "";
            txtPMenuTitle.Text = "";
            txtDesc.Text = "";
            txtUrl.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ENDN(bool TF)
    {
        try
        {
            txtMenuId.Enabled = TF;
            txtMenuTitle.Enabled = TF;
            txtPMenuTitle.Enabled = TF;
            txtDesc.Enabled = TF;
            txtUrl.Enabled = TF;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Flag"].ToString() == "AD")
            {
                resultint = MM.InsertData(txtMenuId.Text.Trim().ToString(), txtPMenuTitle.Text.Trim().ToString(), txtDesc.Text.Trim().ToString(), txtUrl.Text.Trim().ToString());

                if (resultint > 0)
                {
                    lblMessage.Text = "Insert Menu Successfully...!!";
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "MenuMaster _Create_" + txtMenuId.Text + "_"  + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    ClearData();
                    return;
                }
            }
            else if (ViewState["Flag"].ToString() == "MD")
            {
                resultint = MM.ModifyData(ViewState["MenuId"].ToString(), txtMenuId.Text.Trim().ToString(), txtPMenuTitle.Text.Trim().ToString(), txtDesc.Text.Trim().ToString(), txtUrl.Text.Trim().ToString());

                if (resultint > 0)
                {
                    lblMessage.Text = "Modify Menu Successfully...!!";
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "MenuMaster _Mod_" + txtMenuId.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    ClearData();
                    BindGrid();
                    return;
                }
            }
            else if (ViewState["Flag"].ToString() == "DL")
            {
                resultint = MM.DeleteData(ViewState["MenuId"].ToString());

                if (resultint > 0)
                {
                    lblMessage.Text = "Menu Deleted Successfully...!!";
                    ModalPopup.Show(this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "MenuMaster _Delete_" + txtMenuId.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    ClearData();
                    BindGrid();
                    return;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Exit_Click(object sender, EventArgs e)
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

    protected void grdMenuMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdMenuMaster.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string strnumid = objlink.CommandArgument;

            ViewState["MenuId"] = strnumid.ToString();
            callData();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void callData()
    {
        try
        {
            if (ViewState["Flag"].ToString() != "AD")
            {
                DataTable DT = new DataTable();
                DT = MM.GetInfo(ViewState["MenuId"].ToString());

                if (DT.Rows.Count > 0)
                {
                    txtMenuId.Text = DT.Rows[0]["ParentMenuId"].ToString();
                    txtMenuTitle.Text = DT.Rows[0]["MenuTitle"].ToString();
                    txtPMenuTitle.Text = DT.Rows[0]["MenuTitle"].ToString();
                    txtDesc.Text = DT.Rows[0]["PageDesc"].ToString();
                    txtUrl.Text = DT.Rows[0]["PageURL"].ToString();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindGrid()
    {
        try
        {
            MM.BindData(grdMenuMaster);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}