using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmDayReopen : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsBindDropdown BD = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    ClsDayReopen DR = new ClsDayReopen();
    ClsLogin LG = new ClsLogin();
    int Result = 0;
    string DStatus = "",FL="";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            BindBranch();
            btnOpen.Visible = false;
            btnReOpen.Visible = true;

            ddlBrName.Focus();
        }
    }

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

    protected void ddlBrName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtBrCode.Text = "";
            txtWorkDate.Text = "";
            txtOpenDate.Text = "";

            txtBrCode.Text = ddlBrName.SelectedValue.ToString();

            ViewState["EntryDate"] = LG.openDay(txtBrCode.Text.Trim().ToString());

            string NextDay = ViewState["EntryDate"].ToString();
            NextDay = conn.AddMonthDay(NextDay, "-1", "D");
        A:
            string Holdy = conn.sExecuteScalar("SELECT COUNT(*) FROM AVS1026 WHERE HOLIDAYDATE = '" + conn.ConvertDate(NextDay) + "' AND STATUS = 1 ");
            if (Holdy != "0")
            {
                NextDay = conn.AddMonthDay(NextDay, "-1", "D");
                goto A;
            }
            else
            {
                txtWorkDate.Text = ViewState["EntryDate"].ToString();
                txtOpenDate.Text = NextDay.ToString();
                BindGrid();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnReOpen_Click(object sender, EventArgs e)
    {
        try
        {
            //Check user is admin or not
            string ADM = DR.checkAdmin(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString());

            if (ADM != null && ADM != "")
            {
                //Check how many user currently login
                Result = DR.CheckLoginUser(GrdUserLogin, txtBrCode.Text.Trim().ToString());

                if (Result > 0)
                {
                    DivUserLog.Visible = true;
                    lblMessage.Text = "User Is Login.. Please Logout All User...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
                else
                {
                    DStatus = DR.ReOpenDay(txtBrCode.Text.Trim().ToString(), ViewState["EntryDate"].ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString(), "CP");

                    if (DStatus != "" || DStatus != null)
                    {
                        BindGrid();

                        FL = "Insert";
                        string Res = CLM.LOGDETAILS(FL, txtBrCode.Text.Trim().ToString(), Session["MID"].ToString(), "Day_Reopen _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                        lblMessage.Text = "" + DStatus + "";
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

    protected void btnOpen_Click(object sender, EventArgs e)
    {
        try
        {
             //Check user is admin or not
            string ADM = DR.checkAdmin(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString());

            if (ADM != null && ADM != "")
            {
                if (ViewState["DayBeginDate"].ToString() != ViewState["EntryDate"].ToString())
                {
                    DStatus = DR.OpenDay(txtBrCode.Text.Trim().ToString(), ViewState["DayBeginDate"].ToString(), "DO");

                    if (DStatus != "" || DStatus != null)
                    {
                        lblMessage.Text = "" + DStatus + "";
                        ModalPopup.Show(this.Page);
                        BindGrid();
                        return;
                    }
                }
                else
                {
                    lblMessage.Text = "Already Same Day Opened...!!";
                    ModalPopup.Show(this.Page);
                    BindGrid();
                    return;
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

    protected void BtnExit_Click(object sender, EventArgs e)
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

    protected void DefaultValue()
    {
        txtWorkDate.Text = Session["EntryDate"].ToString();
        txtOpenDate.Text = Session["EntryDate"].ToString();

        BindGrid();
        txtOpenDate.Enabled = true;
        btnOpen.Visible = false;
        btnReOpen.Visible = true;
    }

    protected void BindGrid()
    {
        try
        {
            int RC = DR.GetReOpenedDay(grdOpenDay, txtBrCode.Text.Trim().ToString(), ViewState["EntryDate"].ToString(), "DR");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grdOpenDay_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdOpenDay.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void lnkbtnSelect_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string strnumid = objlink.CommandArgument;

            ViewState["DayBeginDate"] = strnumid.ToString();
            CallData();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void CallData()
    {
        try
        {
            txtOpenDate.Text = ViewState["DayBeginDate"].ToString();
            txtOpenDate.Enabled = false;
            btnOpen.Visible = true;
            btnReOpen.Visible = false;
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
            Response.Redirect("FrmBRHAuthorize.aspx?FL=" + str);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}