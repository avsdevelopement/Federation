using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmDayClose : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsDayActivityView DAV = new ClsDayActivityView();
    ClsDayClosedReport act = new ClsDayClosedReport();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "", BRCD = "", FDATE = "", TDATE = "";
    int output = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            BindBranch();
            Div_ALL.Visible = false;
            Div_SPECIFIC.Visible = false;
            Div_Buttons.Visible = false;
        }
    }
    public void BindBranch()
    {
        BD.BindBRANCHNAME(ddlBrName, null);
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Rdb_No.SelectedValue == "1")
            {
                if (txtfrmdateS.Text != null && txttodateS.Text != null)
                {
                    FL = "ALLB";
                    FDATE = txtfrmdateS.Text;
                    TDATE = txttodateS.Text;
                }
                int result = DAV.GetDayCloseDetails(grddayactivity, FL, txtfrmdateS.Text, txttodateS.Text);
                if (result <= 0)
                {
                    WebMsgBox.Show("Sorry No Records Found", this.Page);

                    //grddayactivity.Visible = false;
                }
            }
            else if (Rdb_No.SelectedValue == "2")
            {
                if (txtfromdate.Text != null && txttodate.Text != null)
                {
                    FL = "BRCDS";
                    BRCD = txtBrCode.Text;
                    FDATE = txtfromdate.Text;
                    TDATE = txttodate.Text;
                }
                int result = DAV.GetBrDayCloseDetails(grddayactivity, FL, txtBrCode.Text, txtfromdate.Text, txttodate.Text);
                if (result <= 0)
                {
                    WebMsgBox.Show("Sorry No Records Found", this.Page);
                    //grddayactivity.Visible = false;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void ddlBrName_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtBrCode.Text = ddlBrName.SelectedValue.ToString();
    }
    public void Clear()
    {
        txtBrCode.Text = "";
        ddlBrName.SelectedValue = "0";
        txtfromdate.Text = "";
        txttodate.Text = "";
        txtfrmdateS.Text = "";
        txttodateS.Text = "";
    }
    protected void BtnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Rdb_No.SelectedValue == "1")
            {
                if (txtfrmdateS.Text != null && txttodateS.Text != null)
                {
                    FL = "ALLB";
                    FDATE = txtfrmdateS.Text;
                    TDATE = txttodateS.Text;
                }
                string redirectURL = "FrmRView.aspx?flag=" + FL + "&Brcd=" + Session["BRCD"] + "&FDate=" + FDATE + "&TDate=" + TDATE + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptDayClose.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }

            else if (Rdb_No.SelectedValue == "2")
            {
                if (txtfromdate.Text != null && txttodate.Text != null)
                {
                    FL = "BRCDS";
                    BRCD = txtBrCode.Text;
                    FDATE = txtfromdate.Text;
                    TDATE = txttodate.Text;
                }
                string redirectURL = "FrmRView.aspx?flag=" + FL + "&Brcd=" + BRCD + "&FDate=" + FDATE + "&TDate=" + TDATE + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptDayClose.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
  
   
    protected void Rdb_No_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Rdb_No.SelectedValue == "1")
            {
                Div_ALL.Visible = true;
                Div_SPECIFIC.Visible = false;
                Div_Buttons.Visible = true;
             //   grddayactivity.Visible = false;
            }
            else if (Rdb_No.SelectedValue == "2")
            {
                Div_ALL.Visible = false;
                Div_SPECIFIC.Visible = true;
                Div_Buttons.Visible = true;
                //grddayactivity.Visible = false;
            }
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
    public void BindGrid()
    {
        try
        {
            int RS = 0;
            RS = act.BrHandActivity(GrdBranchH, Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString());
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

    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {
            //Check user is admin or not
            string ADM = act.checkAdmin(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString());

            if (ADM != null && ADM != "")
            {
                //Check how many user currently login
                output = act.CheckLoginUser(GrdBranchH, Session["BRCD"].ToString(), Session["MID"].ToString());

                if (output > 0)
                {
                    lblMessage.Text = "User Is Login.. Please Logout All User...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
                else
                {
                    //Check Branch Current Status
                    int Result = act.CheckDayStatus(Session["BRCD"].ToString(), Session["EntryDate"].ToString());

                    if (Result == 1)
                    {
                        lblMessage.Text = "Branch Handover Not Completed...!!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                    else if (Result == 2)
                    {
                        output = act.DayCloseActivity(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString());

                        if (output > 0)
                        {
                            Response.Redirect("FrmLogin.aspx", false);
                            lblMessage.Text = "Day Close Successfully Complete...!!";
                            ModalPopup.Show(this.Page);
                            FL = "Insert";//Dhanya Shetty
                            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Day_Close _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                            return;
                        }
                    }
                    else if (Result == 3)
                    {
                        lblMessage.Text = "Day Closed Already Done...!!";
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
}