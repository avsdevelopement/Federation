using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmReOpenDateRange : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    ClsReOpenDateRange DOR = new ClsReOpenDateRange();
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
                ddlBrName.Focus();
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
            string ADM = DOR.checkAdmin(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString());

            if (ADM != null && ADM != "")
            {
                txtBrCode.Text = ddlBrName.SelectedValue.ToString();

                ViewState["EntryDate"] = DOR.openDay(txtBrCode.Text.Trim().ToString());

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

    #region Button Click Event

    protected void Btn_DayReOpen_Click(object sender, EventArgs e)
    {
        try
        {
            //Check user is admin or not
            string ADM = DOR.checkAdmin(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString());

            if (ADM != null && ADM != "")
            {
                //Check how many user currently login
                result = DOR.CheckLoginUser(GrdDayReOpen, txtBrCode.Text.Trim().ToString(), Session["MID"].ToString());

                if (result > 0)
                {
                    DivUserLog.Visible = true;
                    lblMessage.Text = "User Login for Branch.. Please Logout this user";
                    ModalPopup.Show(this.Page);
                    return;
                }
                else
                {
                    //  Finally Process for Day Re-Open
                    result = DOR.DayReOpenDateRange(txtBrCode.Text.Trim().ToString(), txtFDate.Text.Trim().ToString(), txtTDate.Text.Trim().ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString());

                    if (result > 0)
                    {
                        lblMessage.Text = "Day re-opened from " + txtFDate.Text.Trim().ToString() + " to " + txtTDate.Text.Trim().ToString() + "...!!";
                        ModalPopup.Show(this.Page);
                        FL = "Insert";
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DayReopnBetweenDate _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
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

    #endregion

}