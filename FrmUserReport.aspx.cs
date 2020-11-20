using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class FrmUserReport : System.Web.UI.Page
{
    ClsUserReport UR = new ClsUserReport();
    DataTable DT = new DataTable();
     ClsLogMaintainance CLM = new ClsLogMaintainance();

     string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            ENDN(false);
        }
    }

    protected void ENDN(bool TF)
    {
        TxtTBRCD.Enabled = TF;
        TxtFBRCD.Enabled = TF;
    }
    protected void Clear()
    {
        TxtFBRCD.Text = "";
        TxtTBRCD.Text = "";
        Rdb_Choice.Focus();
    }
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void Btn_ClearAll_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        string Flag = "";
        string Flag1 = "";
        string TB="", FB="";
        try
        {
            if (Rdb_Choice.SelectedValue == "All")
            {
                if (rbnStatus.SelectedValue == "All")
                {
                    FB = "0";
                    TB = "0";
                    Flag = "ALL";
                    Flag1 = "ALL";
                }
                else if (rbnStatus.SelectedValue == "Active")
                {
                    FB = "0";
                    TB = "0";
                    Flag = "ALL";
                    Flag1 = "Active";
                }
                else if (rbnStatus.SelectedValue == "Deactive")
                {
                    FB = "0";
                    TB = "0";
                    Flag = "ALL";
                    Flag1 = "Deactive";
                }
            }
            else if(Rdb_Choice.SelectedValue=="Some")
            {
                if (rbnStatus.SelectedValue == "All")
                {
                    FB = TxtFBRCD.Text;
                    TB = TxtTBRCD.Text;
                    Flag = "SOME";
                    Flag1 = "ALL";
                }
                else if (rbnStatus.SelectedValue == "Active")
                {
                    FB = TxtFBRCD.Text;
                    TB = TxtTBRCD.Text;
                    Flag = "SOME";
                    Flag1 = "Active";
                }
                else if (rbnStatus.SelectedValue == "Deactive")
                {
                    FB = TxtFBRCD.Text;
                    TB = TxtTBRCD.Text;
                    Flag = "SOME";
                    Flag1 = "Deactive";
                }
            }
            else if (Rdb_Choice.SelectedValue == "loggedin")
            {
                FB = TxtFBRCD.Text;
                TB = TxtTBRCD.Text;
                Flag = "LOGGEDIN";
                Flag1 = "";
            }
            else if (Rdb_Choice.SelectedValue == "loggedout")
            {
                FB = TxtFBRCD.Text;
                TB = TxtTBRCD.Text;
                Flag = "LOGGEDOUT";
                Flag1 = "";
            }

            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Userreport __" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?FBRCD=" + FB + "&TBRCD=" + TB + "&FLG=" + Flag + "&FLG1=" + Flag1 + "&USERNAME=" + Session["UserName"].ToString() + "&rptname=RptUserReport.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            
           
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    
    protected void Rdb_Choice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(Rdb_Choice.SelectedValue=="All")
                ENDN(false);
            else
                ENDN(true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_View_Click(object sender, EventArgs e)
    {
        try
        {
             string Flag = "";
        string TB="", FB="";
            if (Rdb_Choice.SelectedValue == "All")
            {
                FB = "0";
                TB = "0";
                Flag = "ALL";
            }
            else if(Rdb_Choice.SelectedValue=="Some")
            {
                FB = TxtFBRCD.Text;
                TB = TxtTBRCD.Text;
                Flag = "SOME";
            }
            else if (Rdb_Choice.SelectedValue == "loggedin")
            {
                FB = TxtFBRCD.Text;
                TB = TxtTBRCD.Text;
                Flag = "LOGGEDIN";
            }
            else if (Rdb_Choice.SelectedValue == "loggedout")
            {
                FB = TxtFBRCD.Text;
                TB = TxtTBRCD.Text;
                Flag = "LOGGEDOUT";
            }
            int res = UR.GetFilter(grdUserReport, TB, FB, Flag);
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Userreport __" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            if (res <= 0)
            {
                WebMsgBox.Show("Sorry No Records Found", this.Page);
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}