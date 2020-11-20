using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmDayCloseBetweenDate : System.Web.UI.Page
{
    ClsDayClosedBetweenDate DC = new ClsDayClosedBetweenDate();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    DataTable DT = new DataTable();
    string[] FromDate, ToDate;
    string FL = "";
    int result = 0;

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

                ViewState["Flag"] = "BHand";
                rbtnBranchHandover.Checked = true;
                Btn_Handover.Visible = true;
                txtBrCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtBrCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtBrCode.Text != "")
            {
                string bname = DC.GetBranchName(txtBrCode.Text.Trim().ToString());
                if (bname != null)
                {
                    txtBrName.Text = bname;
                    txtFDate.Focus();
                }
                else
                {
                    lblMessage.Text = "Enter valid Branch Code...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
            else
            {
                lblMessage.Text = "Enter Branch Code...!!";
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

    protected void rbtnBranchHandover_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["Flag"] = "BHand";
            Btn_Handover.Visible = true;
            Btn_DayClose.Visible = false;

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
            ViewState["Flag"] = "DClose";
            Btn_DayClose.Visible = true;
            Btn_Handover.Visible = false;

            GrdBranchH.DataSource = null;
            GrdBranchH.DataBind();

            ClearText();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Btn_Handover_Click(object sender, EventArgs e)
    {
        try
        {
            //Check user is admin or not
            string ADM = DC.CheckAdmin(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString());

            if (ADM != null && ADM != "")
            {
                //Check how many user currently login
                result = DC.CheckLoginUser(GrdBranchH, Session["BRCD"].ToString());

                if (result > 0)
                {
                    lblMessage.Text = "User Login for Branch.. Please Logout this user";
                    ModalPopup.Show(this.Page);
                    return;
                }
                else
                {
                    //Check Branch Current Status
                    result = DC.CheckDayStatus(Session["BRCD"].ToString(), txtFDate.Text.Trim().ToString(), txtTDate.Text.Trim().ToString());

                    BindGrid();
                    return;
                }
            }
            else
            {
                lblMessage.Text = "User Not Admin...Please Login Admin User..!.!";
                ModalPopup.Show(this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Btn_DayClose_Click(object sender, EventArgs e)
    {
        try
        {
            FromDate = txtFDate.Text.ToString().Split('/');
            ToDate = txtTDate.Text.ToString().Split('/');

            //Check user is admin or not
            string ADM = DC.CheckAdmin(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString());

            if (ADM != null && ADM != "")
            {
                //Check how many user currently login
                result = DC.CheckLoginUser(GrdBranchH, Session["BRCD"].ToString());

                if (result > 0)
                {
                    lblMessage.Text = "User Is Login.. Please Logout All User...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
                else
                {
                    //Closed Day Between date
                    result = DC.DayCloseBetweenDate(Session["BRCD"].ToString(), txtFDate.Text.Trim().ToString(), txtTDate.Text.Trim().ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString());

                    if (result > 0)
                    {
                        Response.Redirect("FrmLogin.aspx", false);
                        lblMessage.Text = "Day Close Successfully Complete...!!";
                        ModalPopup.Show(this.Page);
                        FL = "Insert";
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Day_Close _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
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

    public void BindGrid()
    {
        try
        {
            int RS = 0;
            RS = DC.BrHandBetweenDate(GrdBranchH, Session["BRCD"].ToString(), txtFDate.Text.Trim().ToString(), txtTDate.Text.Trim().ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString());

            if (ViewState["Flag"].ToString() == "DClose")
            {
                if (RS == 0)
                {
                    Btn_DayClose.Visible = true;
                    Btn_Handover.Visible = false;
                }
            }
            else if (ViewState["Flag"].ToString() == "BHand")
            {
                if (RS == 0)
                {
                    FL = "Insert";
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Branch_HandOver _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    lblMessage.Text = "Branch Handover Successfully...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
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

    private void ClearText()
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

    protected void GrdBranchH_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdBranchH.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string str = objlink.CommandArgument;
            if (str != null && str != "DayNotTally")
                Response.Redirect("FrmBRHAuthorize.aspx?FL=" + str);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}