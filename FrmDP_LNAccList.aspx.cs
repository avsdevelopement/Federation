using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmDP_LNAccList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TxtTDate.Text = Session["EntryDate"].ToString();
        }
    }

    protected void BtnDPLNList_Click(object sender, EventArgs e)
    {
        try
        {
            if (Rdb_DPLN.SelectedValue == "F" )
            {
                if (Rdb_DS.SelectedValue == "D")
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&AsOnDate=" + TxtTDate.Text + "&AccType=" + Rdb_DPLN.SelectedValue + "&Type=" + Rdb_DS.SelectedValue + "&rptname=RptClassificationDPLN.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                else
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&AsOnDate=" + TxtTDate.Text + "&AccType=" + Rdb_DPLN.SelectedValue + "&Type=" + Rdb_DS.SelectedValue + "&rptname=RptClassificationDPLNSumy.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
            else if (Rdb_DPLN.SelectedValue == "L")
            {
                if (Rdb_DS.SelectedValue == "D")
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&AsOnDate=" + TxtTDate.Text + "&AccType=" + Rdb_DPLN.SelectedValue + "&Type=" + Rdb_DS.SelectedValue + "&rptname=RptClassificationDPLN.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                else
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&AsOnDate=" + TxtTDate.Text + "&AccType=" + Rdb_DPLN.SelectedValue + "&Type=" + Rdb_DS.SelectedValue + "&rptname=RptClassificationDPLNSumy.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}