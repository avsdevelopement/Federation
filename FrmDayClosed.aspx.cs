using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmDayClosed : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsDayClosedReport act = new ClsDayClosedReport();
    ClsBindDropdown BD = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    ClsDayClose DC = new ClsDayClose();
    ClsLogin LG = new ClsLogin();
    DataTable DT1 = new DataTable();
    DataTable DT2 = new DataTable();
    Mobile_Service MS = new Mobile_Service();
    int output = 0;
    string FL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            BindBranch();
        }
        ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 500000;
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

            //Check user is admin or not
            string ADM = act.checkAdmin(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString());

            if (ADM != null && ADM != "")
            {
                txtBrCode.Text = ddlBrName.SelectedValue.ToString();

                ViewState["EntryDate"] = LG.openDay(txtBrCode.Text.Trim().ToString());

                string NextDay = ViewState["EntryDate"].ToString();
                NextDay = conn.AddMonthDay(NextDay, "1", "D");
            A:
                string Holdy = conn.sExecuteScalar("SELECT COUNT(*) FROM AVS1026 WHERE HOLIDAYDATE = '" + conn.ConvertDate(NextDay) + "' AND STATUS = 1 ");
                if (Holdy != "0")
                {
                    NextDay = conn.AddMonthDay(NextDay, "1", "D");
                    goto A;
                }
                else
                {
                    txtWorkDate.Text = ViewState["EntryDate"].ToString();
                    txtOpenDate.Text = NextDay.ToString();
                    return;
                }
            }
            else
            {
                ddlBrName.SelectedValue = Session["BRCD"].ToString();
                txtBrCode.Text = Session["BRCD"].ToString();

                ViewState["EntryDate"] = LG.openDay(txtBrCode.Text.Trim().ToString());

                string NextDay = ViewState["EntryDate"].ToString();
                NextDay = conn.AddMonthDay(NextDay, "1", "D");
            A:
                string Holdy = conn.sExecuteScalar("SELECT COUNT(*) FROM AVS1026 WHERE HOLIDAYDATE = '" + conn.ConvertDate(NextDay) + "' AND STATUS = 1 ");
                if (Holdy != "0")
                {
                    NextDay = conn.AddMonthDay(NextDay, "1", "D");
                    goto A;
                }
                else
                {
                    txtWorkDate.Text = ViewState["EntryDate"].ToString();
                    txtOpenDate.Text = NextDay.ToString();
                    return;
                }
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

    public void ClearText()
    {
        try
        {
            ddlBrName.SelectedValue = "0";
            txtBrCode.Text = "";
            txtWorkDate.Text = "";
            txtOpenDate.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnHandover_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtBrCode.Text.ToString() == "")
            {
                ddlBrName.Focus();
                WebMsgBox.Show("Select branch first ...!!", this.Page);
                return;
            }
            else if (txtWorkDate.Text.ToString() == "")
            {
                btnHandover.Focus();
                WebMsgBox.Show("Something wrong working date not show ...!!", this.Page);
                return;
            }
            else if (txtOpenDate.Text.ToString() == "")
            {
                btnHandover.Focus();
                WebMsgBox.Show("Something wrong next open date not show ...!!", this.Page);
                return;
            }
            if (DC.CheckAdminAccess(Convert.ToString(Session["BRCD"]), Convert.ToString(Session["UGRP"])) == 1)
            {
                WebMsgBox.Show("Branch Handover Can Be Done Only By Admin Or Manager ID", this.Page);
                return;
            }
            //Check how many user currently login
            output = act.CheckLoginUser(GrdBranchH, txtBrCode.Text.Trim().ToString(), Session["MID"].ToString());
            if (output > 0)
            {
                btnHandover.Focus();
                WebMsgBox.Show("User is login ...!! Please logout all user ...!!", this.Page);
                return;
            }
            else
            {
                //Check Branch Current Status
                int Result = act.CheckDayStatus(txtBrCode.Text.Trim().ToString(), ViewState["EntryDate"].ToString());
                if (Result == 1)
                {
                    BindGrid();
                }
                else if (Result == 2)
                {
                    ddlBrName.Focus();
                    WebMsgBox.Show("Branch handover already done ...!!", this.Page);
                    return;
                }
                else if (Result == 3)
                {
                    ddlBrName.Focus();
                    WebMsgBox.Show("Branch closed already done ...!!", this.Page);
                    return;
                }
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
            if (txtBrCode.Text.ToString() == "")
            {
                ddlBrName.Focus();
                WebMsgBox.Show("Select branch first ...!!", this.Page);
                return;
            }
            else if (txtWorkDate.Text.ToString() == "")
            {
                btnDayClose.Focus();
                WebMsgBox.Show("Something wrong working date not show ...!!", this.Page);
                return;
            }
            else if (txtOpenDate.Text.ToString() == "")
            {
                btnDayClose.Focus();
                WebMsgBox.Show("Something wrong next open date not show ...!!", this.Page);
                return;
            }
            else
            {
                //Check how many user currently login
                output = act.CheckLoginUser(GrdBranchH, txtBrCode.Text.Trim().ToString(), Session["MID"].ToString());
                if (output > 0)
                {
                    btnDayClose.Focus();
                    WebMsgBox.Show("User is login ...!! Please logout all user ...!!", this.Page);
                    return;
                }
                else
                {
                    //Check Branch Current Status
                    output = act.CheckDayStatus(txtBrCode.Text.ToString(), ViewState["EntryDate"].ToString());
                    if (output == 1)
                    {
                        ddlBrName.Focus();
                        WebMsgBox.Show("Branch handover not completed ...!!", this.Page);
                        return;
                    }
                    else if (output == 2)
                    {
                        output = act.DayCloseActivity(txtBrCode.Text.ToString(), ViewState["EntryDate"].ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString());

                        if (output > 0)
                        {
                            WebMsgBox.Show("Day close successfully complete ...!!", this.Page);
                            SendSMS(txtBrCode.Text.ToString(), ViewState["EntryDate"].ToString());
                            LG.RealizedUser(HttpContext.Current.Session["LOGINCODE"].ToString(), HttpContext.Current.Session["BRCD"].ToString());
                            CLM.LOGDETAILS("Insert", txtBrCode.Text.Trim().ToString(), Session["MID"].ToString(), "Day_Close _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            Response.Redirect("FrmLogin.aspx", false);
                            return;
                        }
                    }
                    else if (output == 3)
                    {
                        ddlBrName.Focus();
                        WebMsgBox.Show("Day Closed Already Done ...!!", this.Page);
                        return;
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void SendSMS(string Bankcode, string EDAT)
    {
        DataTable DT = new DataTable();
        string Message = "";

        try
        {
            DT = act.SendSMS(Bankcode, EDAT);
            if (DT.Rows.Count > 0)
            {
                Message = "Our Society Fin. Possi of " + DT.Rows[0]["BranchName"].ToString() + " dt " + EDAT + " Shares " + DT.Rows[0]["Shares"].ToString() + ", Depo " + DT.Rows[0]["Deposit"].ToString() + ", Unpaid Divi " + DT.Rows[0]["Dividend"].ToString() + ", Cash Bal " + DT.Rows[0]["Cash"].ToString() + ", Bank Bal " + DT.Rows[0]["Bank"].ToString() + ", Invest " + DT.Rows[0]["INV"].ToString() + ", Loan " + DT.Rows[0]["Loans"].ToString() + ", Int Rec. On Loan " + DT.Rows[0]["Interest"].ToString() + ", Profit " + DT.Rows[0]["Profit"].ToString() + "";

                DT = act.GetDetails();
                if (DT.Rows.Count > 0)
                {
                    //  Insert records only
                    for (int i = 0; i < DT.Rows.Count; i++)
                        act.InsertSMS(DT.Rows[i]["CustNo"].ToString(), DT.Rows[i]["MOBILENO"].ToString(), EDAT, Bankcode, Message);

                    //  Shoot SMS here for all records where status is '1' 
                    MS.Send_DCSMS(Bankcode, EDAT);
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void BindGrid()
    {
        try
        {
            int RS = 0;
            RS = act.BrHandActivity(GrdBranchH, txtBrCode.Text.Trim().ToString(), ViewState["EntryDate"].ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString());

            if (ViewState["Flag"].ToString() == "DAYC")
            {
                if (RS == 0)
                {
                    btnDayClose.Visible = true;
                    btnHandover.Visible = false;
                }
            }
            else if (ViewState["Flag"].ToString() == "BRH")
            {
                if (RS == 0)
                {
                    ddlBrName.Focus();
                    WebMsgBox.Show("Branch handover successfully ...!!", this.Page);
                    CLM.LOGDETAILS("Insert", txtBrCode.Text.Trim().ToString(), Session["MID"].ToString(), "Branch_HandOver _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;
                }
            }
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