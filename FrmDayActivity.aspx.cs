using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmDayActivity : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsDayActivityView DAV = new ClsDayActivityView();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL="",FL1 = "", BRCD = "", FDATE = "", TDATE = "", RBD = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
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
    protected void txtBrCode_TextChanged(object sender, EventArgs e)
    {

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
                    RBD = Rbd_all.SelectedValue;
                }
                int result = DAV.AGetFilter(grddayactivity, FL, txtfrmdateS.Text, txttodateS.Text, Rbd_all.SelectedValue);
                if (result <= 0)
                {
                    WebMsgBox.Show("Sorry No Records Found", this.Page);
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
                    RBD = Rdb_Status.SelectedValue;
                }
                int result = DAV.GetFilter(grddayactivity, FL, txtBrCode.Text, txtfromdate.Text, txttodate.Text, Rdb_Status.SelectedValue);
                if (result <= 0)
                {
                    WebMsgBox.Show("Sorry No Records Found", this.Page);
                }
            }
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DayActVw _sub" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
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
                        RBD = Rbd_all.SelectedValue;
                        FL1 = "Insert";//ankita 14/09/2017
                        string Res = CLM.LOGDETAILS(FL1, Session["BRCD"].ToString(), Session["MID"].ToString(), "DayActVw _Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                        string redirectURL = "FrmRView.aspx?flag=" + FL + "&FDate=" + FDATE + "&TDate=" + TDATE + "&RBD=" + RBD + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptDayActivity.rdlc";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                if (txtfrmdateS.Text != null && txttodateS.Text != null)
                {
                    if (Rbd_all.SelectedValue == "6")
                    {
                        FDATE = txtfrmdateS.Text;
                        TDATE = txttodateS.Text;
                        RBD = Rbd_all.SelectedValue;
                        FL1 = "Insert";//ankita 14/09/2017
                        string Res = CLM.LOGDETAILS(FL1, Session["BRCD"].ToString(), Session["MID"].ToString(), "DayActVw _Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                        string redirectURL = "FrmRView.aspx?FDate=" + FDATE + "&TDate=" + TDATE + "&RBD=" + RBD + "&UserName=" + Session["UserName"].ToString() + "&rptname=Rpt_AVS0003.rdlc";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                    }
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
                    RBD = Rdb_Status.SelectedValue;
                    FL1 = "Insert";//ankita 14/09/2017
                    string Res = CLM.LOGDETAILS(FL1, Session["BRCD"].ToString(), Session["MID"].ToString(), "DayActVw _Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    
                    string redirectURL = "FrmRView.aspx?flag=" + FL + "&Brcd=" + BRCD + "&FDate=" + FDATE + "&TDate=" + TDATE + "&RBD=" + RBD + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptDayActivity.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                    if (txtfromdate.Text != null && txttodate.Text != null)
                    {
                        if (Rdb_Status.SelectedValue == "6")
                        {
                            BRCD = txtBrCode.Text;
                            FDATE = txtfromdate.Text;
                            TDATE = txttodate.Text;
                            RBD = Rdb_Status.SelectedValue;
                            FL1 = "Insert";//ankita 14/09/2017
                            string Res = CLM.LOGDETAILS(FL1, Session["BRCD"].ToString(), Session["MID"].ToString(), "DayActVw _Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                            string redirectURL = "FrmRView.aspx?FDate=" + txtfromdate.Text + "&TDate=" + txttodate.Text + "&UserName=" + Session["UserName"].ToString() + "&BRCD=" + txtBrCode.Text + "&rptname=Rpt_AVS0003.rdlc";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                            //string redirectURL = "FrmRView.aspx?FDate=" + FDATE + "&TDate=" + TDATE + "&RBD=" + RBD + "&UserName=" + Session["UserName"].ToString() + "&rptname=Rpt_AVS0003.rdlc";
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                        }
                    }

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
            }
            else if (Rdb_No.SelectedValue == "2")
            {
                Div_ALL.Visible = false;
                Div_SPECIFIC.Visible = true;
                Div_Buttons.Visible = true;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Rdb_Status_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void Rbd_all_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
}