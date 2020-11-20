using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmLoanODListDayEnd : System.Web.UI.Page
{
    DbConnection Conn = new DbConnection();
    ClsReport LC = new ClsReport();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ProdTrans_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string fmonth, fyear;
            string tmonth, tyear;

            string[] fdate = txtAsOnDate.Text.Split('/');
            fmonth = fdate[1].ToString();
            fyear = fdate[2].ToString();

            string[] tdate = txtAsOnDate.Text.Split('/');
            tmonth = tdate[1].ToString();
            tyear = tdate[2].ToString();

            string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&ProdCode=" + TxtAccType.Text + "&FDate=" + txtAsOnDate.Text + "&rptname=RptLoanOverdueReport.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtATName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtATName.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtATName.Text = custnob[0].ToString();
                TxtAccType.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                string[] AC = LC.Getaccno(TxtAccType.Text, Session["BRCD"].ToString(), custnob[2].ToString()).Split('-');
                ViewState["ACCNO"] = AC[0].ToString();
                ViewState["GLCODE"] = AC[1].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtAccType.Text;
                txtAsOnDate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAccType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string AC1;
            AC1 = LC.Getaccno(TxtAccType.Text, Session["BRCD"].ToString(), "");

            if (AC1 != null)
            {
                string[] AC = AC1.Split('-'); ;
                ViewState["ACCNO"] = AC[0].ToString();
                ViewState["GLCODE"] = AC[1].ToString();
                TxtATName.Text = AC[2].ToString();
                AutoAccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtAccType.Text + "_" + ViewState["GLCODE"].ToString();
                txtAsOnDate.Focus();
            }
            else
            {
                WebMsgBox.Show("Enter valid Product code!.....", this.Page);
                TxtAccType.Text = "";
                TxtATName.Text = "";
                TxtAccType.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}