using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;

public partial class FrmAvs51212 : System.Web.UI.Page
{

    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCommon CMN = new ClsCommon();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsSRO SRO = new ClsSRO();
    ClsAVS51186 MOV = new ClsAVS51186();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "", MEM = "";
    int result = 0;
    string sroname = "", AC_Status = "", results = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");

            }
            BD.BindPaymentModeStage(DDLSTAGE);
            BD.BindPaymentModeEXPENCE(ddlcasestaus);
        }


    }

    protected void rbtSep_CheckedChanged(object sender, EventArgs e)
    {
        txtFsro.Enabled = true;
        txtTsro.Enabled = true;
    }
    protected void rdbAll_CheckedChanged(object sender, EventArgs e)
    {
        txtFsro.Enabled = false;
        txtTsro.Enabled = false;
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (rbtSep.Checked == false && rdbAll.Checked == false)
        {
            WebMsgBox.Show("Please Select Item.", this.Page);
        }

        if (rbtSep.Checked == true)
        {
            if (txtTsro.Text=="")
            {
                WebMsgBox.Show("Please enter From SRNO.", this.Page);
            }
            if (txtFsro.Text == "")
            {
                WebMsgBox.Show("Please enter To SRNO.", this.Page);
            }
            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter from Date.", this.Page);
            }

            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter To Date.", this.Page);
            }
            string redirectURL = "FrmRView.aspx?FDate=" + txtFromDate.Text + "&TDate=" + txtToDate.Text + "&FSRNO=" + txtFsro.Text + "&TSRNO=" + txtTsro.Text + "&FLAG=Specific" + "&rptname=RptSroMonthlyReport.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }

        if (rdbAll.Checked == true)
        {
            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter from Date.", this.Page);
            }

            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter To Date.", this.Page);
            }
            string redirectURL = "FrmRView.aspx?FDate=" + txtFromDate.Text + "&TDate=" + txtToDate.Text + "&FLAG=All" + "&rptname=RptSroMonthlyReport.rdlc";
            // string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&LOANGL=" + txtCaseY.Text + "&AddFlag=" + "" + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + txtCaseNo.Text + "&rptname=RptAccAttchNotice_Sro.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        clear();
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        clear();
    }
    public void clear()
    {
        txtTsro.Text = "";
        txtFsro.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        rdbAll.Checked = false;
        rbtSep.Checked = false;
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
    protected void BtnDownloadCASE_Click(object sender, EventArgs e)
    {
         if (rdbAll.Checked == true)
        {
              FL="All";
            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter from Date.", this.Page);
            }

            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter To Date.", this.Page);
            }

              if (rbtSep.Checked == false && rdbAll.Checked == false)
        {
            WebMsgBox.Show("Please Select Item.", this.Page);
        }}

        if (rbtSep.Checked == true)
        {
            FL="Specific";
            if (txtTsro.Text=="")
            {
                WebMsgBox.Show("Please enter From SRNO.", this.Page);
            }
            if (txtFsro.Text == "")
            {
                WebMsgBox.Show("Please enter To SRNO.", this.Page);
            }
            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter from Date.", this.Page);
            }

            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter To Date.", this.Page);
            }}
        DataTable dt = new DataTable();
      //  dt = SRO.RptSRNOMONTHLYRPTS(FL, txtFromDate.Text, txtToDate.Text, txtFsro.Text, txtTsro.Text);
        dt = SRO.RptSRNOMONTHLYRPTS(FL, txtFromDate.Text, txtToDate.Text, txtFsro.Text, txtTsro.Text, DDLSTAGE.SelectedValue.ToString());

     

        //dt = SRO.RptSRNOMONTHLYRPTS(flag:FL,FDATE:txtFromDate.Text,TDATE:txtToDate.Text,FSRNO:txtFsro.Text,TSRNO:txtTsro.Text);
        GridView gv2 = new GridView();
        gv2.DataSource = dt;
        gv2.DataBind();
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=CaseStatusReport.xls");
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gv2.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.End();
    }
    protected void BTNACTION_Click(object sender, EventArgs e)
    {
        //if (rdbAll.Checked == true)
        //{
        //    FL = "All";
        //    if (txtFromDate.Text == "")
        //    {
        //        WebMsgBox.Show("Please enter from Date.", this.Page);
        //    }

        //    if (txtFromDate.Text == "")
        //    {
        //        WebMsgBox.Show("Please enter To Date.", this.Page);
        //    }

        //    if (rbtSep.Checked == false && rdbAll.Checked == false)
        //    {
        //        WebMsgBox.Show("Please Select Item.", this.Page);
        //    }
        //}

        //if (rbtSep.Checked == true)
        //{
        //    FL = "Specific";
        //    if (txtTsro.Text == "")
        //    {
        //        WebMsgBox.Show("Please enter From SRNO.", this.Page);
        //    }
        //    if (txtFsro.Text == "")
        //    {
        //        WebMsgBox.Show("Please enter To SRNO.", this.Page);
        //    }
        //    if (txtFromDate.Text == "")
        //    {
        //        WebMsgBox.Show("Please enter from Date.", this.Page);
        //    }

        //    if (txtFromDate.Text == "")
        //    {
        //        WebMsgBox.Show("Please enter To Date.", this.Page);
        //    }
        //}
        //DataTable dt = new DataTable();
        //dt = SRO.RptSRNOMONTHLYRPTS(FL, txtFromDate.Text, txtToDate.Text, txtFsro.Text, txtTsro.Text);

        //dt = SRO.RptSRNOMONTHLYRPTS(flag: FL, FDATE: txtFromDate.Text, TDATE: txtToDate.Text, FSRNO: txtFsro.Text, TSRNO: txtTsro.Text);
        //GridView gv2 = new GridView();
        //gv2.DataSource = dt;
        //gv2.DataBind();
        //Response.Clear();
        //Response.AddHeader("content-disposition", "attachment;filename=ActionStatusReport.xls");
        //Response.ContentType = "application/vnd.ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);
        //gv2.RenderControl(hw);
        //Response.Output.Write(sw.ToString());
        //Response.End();
        if (rdbAll.Checked == true)
        {
            FL = "All";
            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter from Date.", this.Page);
            }

            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter To Date.", this.Page);
            }

            if (rbtSep.Checked == false && rdbAll.Checked == false)
            {
                WebMsgBox.Show("Please Select Item.", this.Page);
            }
        }

        if (rbtSep.Checked == true)
        {
            FL = "Specific";
            if (txtTsro.Text == "")
            {
                WebMsgBox.Show("Please enter From SRNO.", this.Page);
            }
            if (txtFsro.Text == "")
            {
                WebMsgBox.Show("Please enter To SRNO.", this.Page);
            }
            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter from Date.", this.Page);
            }

            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter To Date.", this.Page);
            }
        }
        DataTable dt = new DataTable();
        dt = SRO.RptSRNOMONTHLYRPTS(FL, txtFromDate.Text, txtToDate.Text, txtFsro.Text, txtTsro.Text, DDLSTAGE.SelectedValue.ToString());

        //dt = SRO.RptSRNOMONTHLYRPTS(flag:FL,FDATE:txtFromDate.Text,TDATE:txtToDate.Text,FSRNO:txtFsro.Text,TSRNO:txtTsro.Text);
        GridView gv2 = new GridView();
        gv2.DataSource = dt;
        gv2.DataBind();
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=ActionStatusReport.xls");
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gv2.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.End();
    }
    protected void btnExecution_Click(object sender, EventArgs e)
    {
        //if (rdbAll.Checked == true)
        //{
        //    FL = "All";
        //    if (txtFromDate.Text == "")
        //    {
        //        WebMsgBox.Show("Please enter from Date.", this.Page);
        //    }

        //    if (txtFromDate.Text == "")
        //    {
        //        WebMsgBox.Show("Please enter To Date.", this.Page);
        //    }

        //    if (rbtSep.Checked == false && rdbAll.Checked == false)
        //    {
        //        WebMsgBox.Show("Please Select Item.", this.Page);
        //    }
        //    if (DDLSTAGE.SelectedValue == "0")
        //    {
        //        WebMsgBox.Show("Please Select stage.", this.Page);
        //    }
        //}

        //if (rbtSep.Checked == true)
        //{
        //    FL = "Specific";
        //    if (txtTsro.Text == "")
        //    {
        //        WebMsgBox.Show("Please enter From SRNO.", this.Page);
        //    }
        //    if (txtFsro.Text == "")
        //    {
        //        WebMsgBox.Show("Please enter To SRNO.", this.Page);
        //    }
        //    if (txtFromDate.Text == "")
        //    {
        //        WebMsgBox.Show("Please enter from Date.", this.Page);
        //    }

        //    if (txtFromDate.Text == "")
        //    {
        //        WebMsgBox.Show("Please enter To Date.", this.Page);
        //    }
        //    if (DDLSTAGE.SelectedValue == "0")
        //    {
        //        WebMsgBox.Show("Please Select stage.", this.Page);
        //    }
        //}
        //DataTable dt = new DataTable();
        //dt = SRO.RptEXECUSIONMONTHLYRPTS(FL, txtFromDate.Text, txtToDate.Text, txtFsro.Text, txtTsro.Text,DDLSTAGE.SelectedValue.ToString());

        ////dt = SRO.RptSRNOMONTHLYRPTS(flag:FL,FDATE:txtFromDate.Text,TDATE:txtToDate.Text,FSRNO:txtFsro.Text,TSRNO:txtTsro.Text);
        //GridView gv2 = new GridView();
        //gv2.DataSource = dt;
        //gv2.DataBind();
        //Response.Clear();
        //Response.AddHeader("content-disposition", "attachment;filename=eXECUTIONREPORT.xls");
        //Response.ContentType = "application/vnd.ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);
        //gv2.RenderControl(hw);
        //Response.Output.Write(sw.ToString());
        //Response.End();

        if (rdbAll.Checked == true)
        {
            FL = "All";
            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter from Date.", this.Page);
            }

            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter To Date.", this.Page);
            }

            if (rbtSep.Checked == false && rdbAll.Checked == false)
            {
                WebMsgBox.Show("Please Select Item.", this.Page);
            }
            if (DDLSTAGE.SelectedValue == "0")
            {
                WebMsgBox.Show("Please Select stage.", this.Page);
            }
        }

        if (rbtSep.Checked == true)
        {
            FL = "Specific";
            if (txtTsro.Text == "")
            {
                WebMsgBox.Show("Please enter From SRNO.", this.Page);
            }
            if (txtFsro.Text == "")
            {
                WebMsgBox.Show("Please enter To SRNO.", this.Page);
            }
            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter from Date.", this.Page);
            }

            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter To Date.", this.Page);
            }
            if (DDLSTAGE.SelectedValue == "0")
            {
                WebMsgBox.Show("Please Select stage.", this.Page);
            }
        }
        DataTable dt = new DataTable();
        dt = SRO.RptEXECUSIONMONTHLYRPTS(FL, txtFromDate.Text, txtToDate.Text, txtFsro.Text, txtTsro.Text, DDLSTAGE.SelectedValue.ToString());

        //dt = SRO.RptSRNOMONTHLYRPTS(flag:FL,FDATE:txtFromDate.Text,TDATE:txtToDate.Text,FSRNO:txtFsro.Text,TSRNO:txtTsro.Text);
        GridView gv2 = new GridView();
        gv2.DataSource = dt;
        gv2.DataBind();
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=eXECUTIONREPORT.xls");
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gv2.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.End();

    }
    protected void btncost_Click(object sender, EventArgs e)
    {
        if (rdbAll.Checked == true)
        {
            FL = "All";
            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter from Date.", this.Page);
            }

            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter To Date.", this.Page);
            }
            if (DDLSTAGE.SelectedValue == "0")
            {
                WebMsgBox.Show("Please Select stage.", this.Page);
            }

            if (rbtSep.Checked == false && rdbAll.Checked == false)
            {
                WebMsgBox.Show("Please Select Item.", this.Page);
            }
        }

        if (rbtSep.Checked == true)
        {
            FL = "Specific";
            if (txtTsro.Text == "")
            {
                WebMsgBox.Show("Please enter From SRNO.", this.Page);
            }
            if (txtFsro.Text == "")
            {
                WebMsgBox.Show("Please enter To SRNO.", this.Page);
            }
            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter from Date.", this.Page);
            }

            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter To Date.", this.Page);
            }
            if (DDLSTAGE.SelectedValue == "0")
            {
                WebMsgBox.Show("Please Select Stage.", this.Page);
            }
        }
        DataTable dt = new DataTable();
        dt = SRO.RptCOSTOFMONTHLYRPTS(FL, txtFromDate.Text, txtToDate.Text, txtFsro.Text, txtTsro.Text,DDLSTAGE.SelectedValue.ToString());

        //dt = SRO.RptSRNOMONTHLYRPTS(flag:FL,FDATE:txtFromDate.Text,TDATE:txtToDate.Text,FSRNO:txtFsro.Text,TSRNO:txtTsro.Text);
        GridView gv2 = new GridView();
        gv2.DataSource = dt;
        gv2.DataBind();
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=COST_OF_PROCESS_RPT.xls");
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gv2.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.End();
    }
    protected void BTNOCASEPT_Click(object sender, EventArgs e)
    {
        if (rdbAll.Checked == true)
        {
            FL = "All";
            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter from Date.", this.Page);
            }

            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter To Date.", this.Page);
            }
            
            if (rbtSep.Checked == false && rdbAll.Checked == false)
            {
                WebMsgBox.Show("Please Select Item.", this.Page);
            }
        }

        if (rbtSep.Checked == true)
        {
            FL = "Specific";
            if (txtTsro.Text == "")
            {
                WebMsgBox.Show("Please enter From SRNO.", this.Page);
            }
            if (txtFsro.Text == "")
            {
                WebMsgBox.Show("Please enter To SRNO.", this.Page);
            }
            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter from Date.", this.Page);
            }

            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter To Date.", this.Page);
            }
           
        }
        DataTable dt = new DataTable();
        dt = SRO.RPTNOOFCASERPT(FL, txtFromDate.Text, txtToDate.Text, txtFsro.Text, txtTsro.Text);

        //dt = SRO.RptSRNOMONTHLYRPTS(flag:FL,FDATE:txtFromDate.Text,TDATE:txtToDate.Text,FSRNO:txtFsro.Text,TSRNO:txtTsro.Text);
        GridView gv2 = new GridView();
        gv2.DataSource = dt;
        gv2.DataBind();
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=No_OF_CASE.xls");
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gv2.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.End();
    }
    protected void btnsrorpt_Click(object sender, EventArgs e)
    {
        if (rbtSep.Checked == false && rdbAll.Checked == false)
        {
            WebMsgBox.Show("Please Select Item.", this.Page);
        }

        if (rbtSep.Checked == true)
        {
            if (txtTsro.Text == "")
            {
                WebMsgBox.Show("Please enter From SRNO.", this.Page);
            }
            if (txtFsro.Text == "")
            {
                WebMsgBox.Show("Please enter To SRNO.", this.Page);
            }
            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter from Date.", this.Page);
            }

            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter To Date.", this.Page);
            }
            string redirectURL = "FrmRView.aspx?FDate=" + txtFromDate.Text + "&TDate=" + txtToDate.Text + "&FSRNO=" + txtFsro.Text + "&TSRNO=" + txtTsro.Text + "&FLAG=Specific" + "&rptname=RPTSUMMARYSROM.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }

        if (rdbAll.Checked == true)
        {
            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter from Date.", this.Page);
            }

            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter To Date.", this.Page);
            }
            string redirectURL = "FrmRView.aspx?FDate=" + txtFromDate.Text + "&TDate=" + txtToDate.Text + "&FLAG=All" + "&rptname=RPTSUMMARYSROM.rdlc";
            // string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&LOANGL=" + txtCaseY.Text + "&AddFlag=" + "" + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + txtCaseNo.Text + "&rptname=RptAccAttchNotice_Sro.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        clear();
    }
    protected void BTNMONTHLY_Click(object sender, EventArgs e)
    {
        if (rbtSep.Checked == false && rdbAll.Checked == false)
        {
            WebMsgBox.Show("Please Select Item.", this.Page);
        }

        if (rbtSep.Checked == true)
        {
            if (txtTsro.Text == "")
            {
                WebMsgBox.Show("Please enter From SRNO.", this.Page);
            }
            if (txtFsro.Text == "")
            {
                WebMsgBox.Show("Please enter To SRNO.", this.Page);
            }
            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter from Date.", this.Page);
            }

            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter To Date.", this.Page);
            }
            string redirectURL = "FrmRView.aspx?FDate=" + txtFromDate.Text + "&TDate=" + txtToDate.Text + "&FSRNO=" + txtFsro.Text + "&TSRNO=" + txtTsro.Text + "&FLAG=Specific" + "&rptname=RPTMONTHLYSROREPORT.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }

        if (rdbAll.Checked == true)
        {
            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter from Date.", this.Page);
            }

            if (txtFromDate.Text == "")
            {
                WebMsgBox.Show("Please enter To Date.", this.Page);
            }
            string redirectURL = "FrmRView.aspx?FDate=" + txtFromDate.Text + "&TDate=" + txtToDate.Text + "&FLAG=All" + "&rptname=RPTMONTHLYSROREPORT.rdlc";
            // string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&LOANGL=" + txtCaseY.Text + "&AddFlag=" + "" + "&Edate=" + Session["ENTRYDATE"].ToString() + "&ACCNO=" + txtCaseNo.Text + "&rptname=RptAccAttchNotice_Sro.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        clear();
    }
}