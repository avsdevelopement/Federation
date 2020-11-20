using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmScrutinySheet : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsScrutinySheet SS = new ClsScrutinySheet();
    string sResult = "";
    int Result = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            AutoCustName.ContextKey = Session["BRCD"].ToString();
            txtCustNo.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #region Text Changed Event

    protected void txtCustNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string sql, AT;
            sql = AT = "";
            AT = BD.GetStage(txtCustNo.Text, Session["BRCD"].ToString(), "");
            if (AT != "1003")
            {
                lblMessage.Text = "Sorry Customer not Authorise...!!";
                ModalPopup.Show(this.Page);
                txtCustNo.Text = "";
                txtCustName.Text = "";
                txtCustNo.Focus();
            }
            else
            {
                if (txtCustNo.Text == "")
                {
                    return;
                }

                string custname = SS.GetCustName(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString());
                string[] cust = custname.Split('_');
                txtCustName.Text = cust[0].ToString();
                txtCustNo.Text = cust[1].ToString();
                string RC = txtCustName.Text;
                if (RC == "")
                {
                    WebMsgBox.Show("Customer not found", this.Page);
                    txtCustNo.Text = "";
                    txtCustNo.Focus();
                    return;
                }
                txtAppNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtCustName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AT;
            AT = "";
            string CUNAME = txtCustName.Text;
            string[] custnob = CUNAME.Split('_');

            if (custnob.Length > 1)
            {
                AT = BD.GetStage((string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString()), Session["BRCD"].ToString(), "");

                if (AT != "1003")
                {
                    lblMessage.Text = "Sorry Customer not Authorise...!!";
                    ModalPopup.Show(this.Page);
                    txtCustNo.Text = "";
                    txtCustName.Text = "";
                    txtCustNo.Focus();
                }
                else
                {
                    txtCustName.Text = custnob[0].ToString();
                    txtCustNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                    string custname = SS.GetCustName(Session["BRCD"].ToString(), txtCustNo.Text.Trim().ToString());
                    string[] cust = custname.Split('_');
                }
                txtAppNo.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtAppNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            btnSubmit.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

    #region Click Event

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string redirectURL = "FrmReportViewer.aspx?BC=" + Session["BRCD"].ToString() + "&CN=" + txtCustNo.Text.Trim().ToString() + "&AN=" + txtAppNo.Text.Trim().ToString() + "&ED=" + Session["EntryDate"].ToString() + "&rptname=RptScrutinySheet.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion
}