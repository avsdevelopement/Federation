using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class FrmDayOpen : System.Web.UI.Page
{
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsDayActivity DA = new ClsDayActivity();
    DbConnection conn = new DbConnection();
    ClsDayOpen DO = new ClsDayOpen();

    ClsLogin LG = new ClsLogin();
    DataTable DT1 = new DataTable();
    string sql = "", FL = "";
    int Result = 0;

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
            string ADM = DO.checkAdmin(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString(), Session["MID"].ToString());

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

    protected void DayBegin_Click(object sender, EventArgs e)
    {
        try
        {
            string NextDay = ViewState["EntryDate"].ToString();

            //Check user is admin or not
            //string ADM = DO.checkAdmin(Session["BRCD"].ToString(), Session["LOGINCODE"].ToString());
            //if (ADM != null && ADM != "")
            //{
            //Check how many user currently login
            Result = DO.CheckLoginUser(GrdDayOpen, txtBrCode.Text.Trim().ToString(), Session["MID"].ToString());

            if (Result > 0)
            {
                DivUserLog.Visible = true;
                lblMessage.Text = "User Login for Branch.. Please Logout this user";
                ModalPopup.Show(this.Page);
                return;
            }
            if (DO.CheckAdminAccess(Convert.ToString(Session["BRCD"]), Convert.ToString(Session["UGRP"])) == 1)
            {
                WebMsgBox.Show("Branch Handover Can Be Done Only By Admin Or Manager ID", this.Page);
                return;
            }
            else
            {
                //Commented By Amol as per darade sir instruction On 2017-03-16
                //  Check Branch Current Status

                //Uncommented by Abhihsek As per Requirement
                //string STR = DO.GetPara_DAYOPEN();
                //if (STR != "N")
                //{
                Result = DO.CheckDayStatus(txtBrCode.Text.Trim().ToString(), ViewState["EntryDate"].ToString());

                if (Result == 1)
                {
                    lblMessage.Text = "Branch Handover Not Completed For " + NextDay + "...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
                else if (Result == 2)
                {
                    lblMessage.Text = "Day Closed Not Completed For " + NextDay + "...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
                else if (Result == 3)
                {
                    NextDay = conn.AddMonthDay(NextDay, "1", "D");
                A:
                    string Holdy = conn.sExecuteScalar("SELECT COUNT(*) FROM AVS1026 WHERE HOLIDAYDATE = '" + conn.ConvertDate(NextDay) + "' AND STATUS = 1 ");
                    if (Holdy != "0")
                    {
                        NextDay = conn.AddMonthDay(NextDay, "1", "D");
                        goto A;
                    }

                    int RM = DO.DayOpnLstProc(txtBrCode.Text.Trim().ToString(), NextDay, Session["MID"].ToString(), Session["LOGINCODE"].ToString());
                    if (RM > 0)
                    {
                        FL = "Insert";
                        string Res = CLM.LOGDETAILS(FL, txtBrCode.Text.Trim().ToString(), Session["MID"].ToString(), "Day_Open _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        LG.RealizedUser(HttpContext.Current.Session["LOGINCODE"].ToString(), HttpContext.Current.Session["BRCD"].ToString());

                        Response.Redirect("FrmLogin.aspx", false);
                        lblMessage.Text = "Day Open Successfully Complete..!";
                        ModalPopup.Show(this.Page);
                        return;
                    }
                }
                //}
                //else
                //{
                //    Result = DO.CheckDayStatus(txtBrCode.Text.Trim().ToString(), ViewState["EntryDate"].ToString());

                //    if (Result == 1)
                //    {
                //        lblMessage.Text = "Branch Handover Not Completed For " + NextDay + "...!!";
                //        ModalPopup.Show(this.Page);
                //        return;
                //    }
                //    else if (Result == 2)
                //    {
                //        lblMessage.Text = "Day Closed Not Completed For " + NextDay + "...!!";
                //        ModalPopup.Show(this.Page);
                //        return;
                //    }
                //    else if (Result == 3)
                //    {
                //    NextDay = conn.AddMonthDay(NextDay, "1", "D");
                //A:
                //    string Holdy = conn.sExecuteScalar("SELECT COUNT(*) FROM AVS1026 WHERE HOLIDAYDATE = '" + conn.ConvertDate(NextDay) + "' AND STATUS = 1 ");
                //    if (Holdy != "0")
                //    {
                //        NextDay = conn.AddMonthDay(NextDay, "1", "D");
                //        goto A;
                //    }

                //    int RM = DO.DayOpnLstProc(txtBrCode.Text.Trim().ToString(), NextDay, Session["MID"].ToString(), Session["LOGINCODE"].ToString());
                //    if (RM > 0)
                //    {
                //        FL = "Insert";
                //        string Res = CLM.LOGDETAILS(FL, txtBrCode.Text.Trim().ToString(), Session["MID"].ToString(), "Day_Open _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                //        if (Session["UGRP"].ToString() != "1")
                //            LG.RealizedUser(HttpContext.Current.Session["LOGINCODE"].ToString(), HttpContext.Current.Session["BRCD"].ToString());

                //        Response.Redirect("FrmLogin.aspx", false);
                //        lblMessage.Text = "Day Open Successfully Complete..!";
                //        ModalPopup.Show(this.Page);
                //        return;
                //    }
                //   }
                //}
            }
            //}
            //else
            //{
            //    lblMessage.Text = "User Is Not Admin.. Please Login Admin User...!!";
            //    ModalPopup.Show(this.Page);
            //    return;
            //}
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void GenerateTextFile()
    {
        List<object> lst = new List<object>();
        lst.Add("Errors");
        TrialBalanceTxt repObj = new TrialBalanceTxt();
        repObj.RInit(lst);
        repObj.Start();
    }

    public void error()
    {
        string message = "Hello! Sir..." +
            "Some errors are occured..." +
            "Please check downloded text file";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("function(){");
        sb.Append("alert('");
        sb.Append(message);
        sb.Append("')};");
        sb.Append("</script>");
        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        Response.Write("<script language=javascript>alert('ERROR');</script>");
    }

    protected void btnExist_Click(object sender, EventArgs e)
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

public class WebMessageBox
{
    public static void ShowAlertMessage(Page aPage, String Message)
    {
        string Output;
        Output = String.Format(" alert('{0}');", Message);
        aPage.ClientScript.RegisterStartupScript(aPage.GetType(), "Key", Output, true);
    }
}