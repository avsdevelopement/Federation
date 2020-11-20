using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmCloseDateRange : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    ClsCloseDateRange DC = new ClsCloseDateRange();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    int result = 0;
    
    #region Page Loan

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindBranch();
                rbtnBrHandover.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Text Changed Event

    protected void ddlBrName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtBrCode.Text = "";
            txtFDate.Text = "";
            txtTDate.Text = "";

            //Check user is admin or not
            string ADM = DC.checkAdmin(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString());

            if (ADM != null && ADM != "")
            {
                txtBrCode.Text = ddlBrName.SelectedValue.ToString();

                ViewState["EntryDate"] = DC.openDay(txtBrCode.Text.Trim().ToString());

                txtFDate.Text = ViewState["EntryDate"].ToString();
                txtTDate.Text = "";
            }
            else
            {
                lblMessage.Text = "User Is Not Admin.. Please Login Admin User...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Public Function

    public void BindBranch()
    {
        try
        {
            BD.BindBRANCHNAME(ddlBrName, null);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void ClearText()
    {
        try
        {
            ddlBrName.SelectedValue = "0";
            txtBrCode.Text = "";
            txtFDate.Text = "";
            txtTDate.Text = "";

            ddlBrName.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    
    #endregion

    #region Index changed event
    
    protected void rbtnBrHandover_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["Flag"] = "BRH";

            btnHandover.Visible = true;
            btnDayClose.Visible = false;
            GrdBranchH.DataSource = null;
            GrdBranchH.DataBind();
            ClearText();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void rbtnDayClose_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["Flag"] = "DAYC";

            btnDayClose.Visible = true;
            btnHandover.Visible = false;
            GrdBranchH.DataSource = null;
            GrdBranchH.DataBind();
            ClearText();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void GrdBranchH_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdBranchH.PageIndex = e.NewPageIndex;
            //BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Button Click Event

    protected void btnHandover_Click(object sender, EventArgs e)
    {
        try
        {
            //Check user is admin or not
            string ADM = DC.checkAdmin(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString());

            if (ADM != null && ADM != "")
            {
                //Check how many user currently login
                result = DC.CheckLoginUser(GrdBranchH, txtBrCode.Text.Trim().ToString(), Session["MID"].ToString());

                if (result > 0)
                {
                    divgrd.Visible = true;
                    lblMessage.Text = "User Login for Branch.. Please Logout this user";
                    ModalPopup.Show(this.Page);
                    return;
                }
                else
                {
                    result = DC.BrHandActivity(GrdBranchH, txtBrCode.Text.Trim().ToString(), txtFDate.Text.ToString(), txtTDate.Text.ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString());

                    if (result == 0)
                    {
                        lblMessage.Text = "Branch Handover Successfully Complete Between " + txtFDate.Text.ToString() + " And " + txtTDate.Text.ToString() + " ...!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                }
            }
            else
            {
                lblMessage.Text = "User Is Not Admin.. Please Login Admin User...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnDayClose_Click(object sender, EventArgs e)
    {
        try
        {
            //Check user is admin or not
            string ADM = DC.checkAdmin(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString());

            if (ADM != null && ADM != "")
            {
                //Check how many user currently login
                result = DC.CheckLoginUser(GrdBranchH, txtBrCode.Text.Trim().ToString(), Session["MID"].ToString());

                if (result > 0)
                {
                    divgrd.Visible = true;
                    lblMessage.Text = "User Login for Branch.. Please Logout this user";
                    ModalPopup.Show(this.Page);
                    return;
                }
                else
                {
                    result = DC.DayCloseActivity(txtBrCode.Text.Trim().ToString(), txtFDate.Text.ToString(), txtTDate.Text.ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString());
                    
                    if (result > 0)
                    {
                        lblMessage.Text = "Day Close Successfully Complete Between " + txtFDate.Text.ToString() + " And " + txtTDate.Text.ToString() + " ...!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                }
            }
            else
            {
                lblMessage.Text = "User Is Not Admin.. Please Login Admin User...!!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Btn_ClearAll_Click(object sender, EventArgs e)
    {
        try
        {
            ClearText();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Btn_Exit_Click(object sender, EventArgs e)
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

    #endregion

}