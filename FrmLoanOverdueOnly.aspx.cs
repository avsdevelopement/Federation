using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmLoanOverdueOnly : System.Web.UI.Page
{
    scustom customcs = new scustom();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLoanOverdue LC = new ClsLoanOverdue();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TxtDate.Text = Session["EntryDate"].ToString();
            autoglname.ContextKey = Session["BRCD"].ToString();
            autoglname1.ContextKey = Session["BRCD"].ToString();
            TxtFPRD.Focus();
            
        }
    }
   
    protected void Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }

    protected void TxtFPRD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] TD = BD.GetLoanGL(TxtFPRD.Text, Session["BRCD"].ToString()).Split('_');
            if (TD.Length > 1)
            {

            }
            TxtFPRDName.Text = TD[0].ToString();
            TxtTPRD.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtTPRD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] TD = BD.GetLoanGL(TxtTPRD.Text, Session["BRCD"].ToString()).Split('_');
            if (TD.Length > 1)
            {

            }
            TXtTPRDName.Text = TD[0].ToString();
            TxtDate.Focus();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            TxtDate.Focus();
        }
    }

    protected void TxtFPRDName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtFPRDName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TxtFPRDName.Text = CT[0].ToString();
                TxtFPRD.Text = CT[1].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtFPRD.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();

                if (TxtFPRDName.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    TxtFPRD.Text = "";
                    TxtFPRD.Focus();
                }
                else
                {
                    TxtTPRD.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
    protected void TXtTPRDName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TXtTPRDName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TXtTPRDName.Text = CT[0].ToString();
                TxtTPRD.Text = CT[1].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtTPRD.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();

                if (TXtTPRDName.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    TxtTPRD.Text = "";
                    TxtTPRD.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
  
    protected void Btn_LOReport_Click(object sender, EventArgs e)
    {
        try
        {
            string SL = "";

            if (Rdb_AccType.SelectedValue == "1")
                SL = "CF";
            else if (Rdb_AccType.SelectedValue == "2")
                SL = "NCF";
            else if (Rdb_AccType.SelectedValue == "3")
                SL = "ALL";

            string redirectURL = "FrmRView.aspx?Date=" + TxtDate.Text + "&UserName=" + Session["UserName"].ToString() + "&brcd=" + Session["BRCD"].ToString() + "&EntryDate=" + Session["EntryDate"].ToString() + "&FSUBGL=" + TxtFPRD.Text + "&TSUBGL=" + TxtTPRD.Text + "&SL=" + SL + "&rptname=RptLoanOverdue_Only.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Rdb_AccType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}