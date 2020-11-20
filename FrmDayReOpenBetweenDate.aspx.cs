using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmDayReOpenBetweenDate : System.Web.UI.Page
{
    ClsDayReOpenBetweenDate DOR = new ClsDayReOpenBetweenDate();
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
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }
                txtBrCode.Text = Session["BRCD"].ToString();
                txtBrName.Text = Session["BName"].ToString();
                hdnUserGrp.Value = Session["UGRP"].ToString();

                txtFDate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Text Changed Event

    protected void txtBrCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtBrCode.Text != "")
            {
                string bname = DOR.GetBranchName(txtBrCode.Text.Trim().ToString());
                if (bname != null)
                {
                    txtBrName.Text = bname;
                    Btn_DayReOpen.Focus();
                }
                else
                {
                    lblMessage.Text = "Enter valid branch code...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
            else
            {
                lblMessage.Text = "Enter branch code first...!!";
                ModalPopup.Show(this.Page);
                txtBrCode.Focus();
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
            string ADM = DOR.checkAdmin(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString());

            if (ADM != null && ADM != "")
            {
                //Check how many user currently login
                result = DOR.CheckLoginUser(txtBrCode.Text.Trim().ToString());

                if (result > 0)
                {
                    lblMessage.Text = "User Is Login.. Please Logout All User...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
                else
                {
                    //  Finally Process for Day Re-Open
                    result = DOR.DayReOpenBetweenDate(txtBrCode.Text.Trim().ToString(), txtFDate.Text.Trim().ToString(), txtTDate.Text.Trim().ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString());

                    if (result < 0)
                    {
                        lblMessage.Text = "User Is Login.. Please Logout All User...!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                    else if (result > 0)
                    {
                        lblMessage.Text = "Day day re-opened from " + txtFDate.Text.Trim().ToString() + " to " + txtTDate.Text.Trim().ToString() + "...!!";
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
            txtFDate.Text = "";
            txtTDate.Text = "";

            txtFDate.Focus();
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