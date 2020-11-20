using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAVS5058 : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    static string FL = "";
    string glcode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                TxtFbrcd.Text = Session["BRCD"].ToString();
                TxttBrcd.Text = Session["BRCD"].ToString();
                lblheading.InnerText = "Auditrail Report";
                TxtTdate.Text = Session["EntryDate"].ToString();
                TxtFdate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void ddlMainMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlMainMenu.SelectedValue != "0")
            {
                div_brcd.Visible = true;
                div_main.Visible = false;
                if (ddlMainMenu.SelectedValue == "1")
                {
                    FL = "1";
                    div_type.Visible = false;
                    lblheading.InnerText = "Customer Open Report";
                }
                else if (ddlMainMenu.SelectedValue == "2")
                {
                    FL = "2";
                    div_type.Visible = false;
                    lblheading.InnerText = "A/Cs without Mobileno Report";
                }
                else if (ddlMainMenu.SelectedValue == "3")
                {
                    FL = "3";
                    div_type.Visible = false;
                    lblheading.InnerText = "A/Cs without Loan Limit Report";
                }
                else if (ddlMainMenu.SelectedValue == "4")
                {
                    FL = "4";
                    div_type.Visible = false;
                    lblheading.InnerText = "A/Cs without DepositInfo Report";
                }
                else if (ddlMainMenu.SelectedValue == "5")
                {
                    FL = "5";
                    div_type.Visible = false;
                    lblheading.InnerText = "A/Cs without Customer Master Report";
                }
                else if (ddlMainMenu.SelectedValue == "6")
                {
                    FL = "6";
                    div_type.Visible = false;
                    lblheading.InnerText = "A/Cs without Surity Report";
                }
                else if (ddlMainMenu.SelectedValue == "7")
                {
                    FL = "7";
                    div_type.Visible = true;
                    lblheading.InnerText = "Active A/Cs with 0 balance Report";
                }
            }
            else
            {
                div_brcd.Visible = false;
                div_main.Visible = true;

            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void Btnreport_Click(object sender, EventArgs e)
    {
        try
        {
            if (FL == "1")
            {
                string redirectURL = "FrmRView.aspx?&FL=" + FL + "&FBRCD=" + TxtFbrcd.Text + "&TBRCD=" + TxttBrcd.Text + "&FDATE=" + TxtFdate.Text + "&TDATE=" + TxtTdate.Text + "&rptname=RptAuditrail1.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (FL == "2")
            {
                string redirectURL = "FrmRView.aspx?&FL=" + FL + "&FBRCD=" + TxtFbrcd.Text + "&TBRCD=" + TxttBrcd.Text + "&FDATE=" + TxtFdate.Text + "&TDATE=" + TxtTdate.Text + "&rptname=RptAuditrail2.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (FL == "3")
            {
                string redirectURL = "FrmRView.aspx?&FL=" + FL + "&FBRCD=" + TxtFbrcd.Text + "&TBRCD=" + TxttBrcd.Text + "&FDATE=" + TxtFdate.Text + "&TDATE=" + TxtTdate.Text + "&rptname=RptAuditrail3.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (FL == "4")
            {
                string redirectURL = "FrmRView.aspx?&FL=" + FL + "&FBRCD=" + TxtFbrcd.Text + "&TBRCD=" + TxttBrcd.Text + "&FDATE=" + TxtFdate.Text + "&TDATE=" + TxtTdate.Text + "&rptname=RptAuditrail4.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (FL == "5")
            {
                string redirectURL = "FrmRView.aspx?&FL=" + FL + "&FBRCD=" + TxtFbrcd.Text + "&TBRCD=" + TxttBrcd.Text + "&FDATE=" + TxtFdate.Text + "&TDATE=" + TxtTdate.Text + "&rptname=RptAuditrail5.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (FL == "6")
            {
                string redirectURL = "FrmRView.aspx?&FL=" + FL + "&FBRCD=" + TxtFbrcd.Text + "&TBRCD=" + TxttBrcd.Text + "&FDATE=" + TxtFdate.Text + "&TDATE=" + TxtTdate.Text + "&rptname=RptAuditrail6.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (FL == "7")
            {
                if (rdbType.SelectedValue == "1")
                    glcode = "3";
                else if (rdbType.SelectedValue == "2")
                    glcode = "5";
                else if (rdbType.SelectedValue == "3")
                    glcode = "4";
                string redirectURL = "FrmRView.aspx?&FL=" + FL + "&FBRCD=" + TxtFbrcd.Text + "&TBRCD=" + TxttBrcd.Text + "&EDATE=" + conn.ConvertDate(Session["EntryDate"].ToString()) + "&GLCODE=" + glcode + "&FDATE=" + TxtFdate.Text + "&TDATE=" + TxtTdate.Text + "&rptname=RptAuditrail7.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnClose_Click(object sender, EventArgs e)
    {
        try
        {
            div_brcd.Visible = false;
            div_main.Visible = true;
            div_type.Visible = false;
            ddlMainMenu.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}