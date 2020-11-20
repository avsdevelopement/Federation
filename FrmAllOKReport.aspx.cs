using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Globalization;
public partial class FrmAllOKReport : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAllOKReport AK = new ClsAllOKReport();
    ClsLogin LG = new ClsLogin();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }
                BD.BindExportFile(ddlExport);
                TxtAsonDate.Focus();
                TxtAsonDate.Text = Session["BRCD"].ToString();
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
    }
    protected void Exit_Click(object sender, EventArgs e)
    {
        //  Response.Redirect("FrmBlank.aspx");
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {

            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "AllOk_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            if (ddlExport.SelectedValue == "1")
            {
                try
                {
                    string redirectURL = "FrmRView.aspx?AsOnDate=" + TxtAsonDate.Text + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptAllOK.rdlc" + "&EXPF=" + ddlExport.SelectedItem.Text + "";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

                }
                catch (Exception Ex)
                {
                    ExceptionLogging.SendErrorToText(Ex);
                }

                }
            else if (ddlExport.SelectedValue == "2")
            {
                BindGrid();
            }
            else if (ddlExport.SelectedValue == "3")
            {
                try
                {
                    string redirectURL = "FrmRView.aspx?AsOnDate=" + TxtAsonDate.Text + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptAllOK.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        
                }
                catch (Exception Ex)
                {
                    ExceptionLogging.SendErrorToText(Ex);
                }
            }
            else if (ddlExport.SelectedValue == "4")
            {
                try
                {
                    string BName = "";
                    string BRName = "";
                    DataTable DT = new DataTable();
                    DT = LG.GetBankName(Session["BRCD"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        BName = DT.Rows[0]["BankName"].ToString();
                        BRName = DT.Rows[0]["BranchName"].ToString();
                    }

                     List<object> lst = new List<object>();
                    lst.Add(BName);
                    lst.Add(BRName);
                    lst.Add("ALL OK REPORT");
                    lst.Add(Session["USERNAME"].ToString());
                    lst.Add(Session["BRCD"].ToString());
                    lst.Add(Session["EntryDate"].ToString());
                    lst.Add(TxtAsonDate.Text);
                 
                    AllOKText AK = new AllOKText();
                    AK.RInit(lst);
                    AK.Start();
                    WebMsgBox.Show("Report Generated Succesfully!!!....", this.Page);
                    TxtAsonDate.Focus();
                }
                catch (Exception Ex)
                {
                    ExceptionLogging.SendErrorToText(Ex);
                }

            }
        }

        
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void BindGrid()
    {
        try
        {
            AK.GetExportDetails(grdAllOK,TxtAsonDate.Text, Session["BRCD"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void grdAllOK_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        
        grdAllOK.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}