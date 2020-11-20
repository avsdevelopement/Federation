using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmCashLimitReport : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    ClsCashLimitMst CM = new ClsCashLimitMst();
    int result = 0;
    string sql = "";
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Btn_Report_Click(object sender, EventArgs e)
    {
        try
        {
             string redirectURL = "FrmRView.aspx?&BRCD=" + Session["BRCD"].ToString() + "&EDATE=" + TxtAsOnDate.Text + "&SUBGLCODE=" + TxtPrd.Text + "&rptname=RptCashLimit.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void Btn_ClearAll_Click(object sender, EventArgs e)
    {
        try
        {
            clear();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void TxtPrd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string tds = BD.GetLoanGL(TxtPrd.Text, Session["BRCD"].ToString());
            if (tds != null)
            {
                string[] TD = tds.Split('_');
                if (TD.Length > 0)
                {

                }
                TxtProdName.Text = TD[0].ToString();
                TxtPrd.Text = TD[1].ToString();
                TxtAsOnDate.Focus();
            }
            else
            {
                WebMsgBox.Show("Invalid Deposit Code......!", this.Page);
                TxtPrd.Text = "";
                TxtProdName.Text = "";
                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string pno = TxtProdName.Text;
            string[] prd = pno.Split('_');
            if (prd.Length > 0)
            {
                TxtProdName.Text = prd[0].ToString();
                TxtPrd.Text = prd[1].ToString();
            }
            TxtAsOnDate.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void clear()
    {
        TxtPrd.Text = "";
        TxtProdName.Text = "";
        TxtAsOnDate.Text = "";
      
    }
}