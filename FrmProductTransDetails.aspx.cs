using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmProductTransDetails : System.Web.UI.Page
{
    DbConnection Conn = new DbConnection();
    ClsReport LC = new ClsReport();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsCommon cmn = new ClsCommon();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("FrmLogin.aspx");
        }
        //added by ankita 07/10/2017 to make user frndly
        if (!IsPostBack)
        {
            TxtBrID.Text = Session["BRCD"].ToString();
            TxtFDate.Text = Conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + Conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
            TxtTDate.Text = Session["EntryDate"].ToString();
            if (cmn.MultiBranch(Session["LOGINCODE"].ToString()) != "Y")
            {
                TxtBrID.Enabled = false;
                TxtFDate.Focus();
            }
            else
            {
                TxtBrID.Enabled = true;
                TxtBrID.Focus();
            }
        }
        AutoAccname.ContextKey = Session["BRCD"].ToString();

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ProdTrans_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            if (RbtlType.SelectedValue == "M")
            {
                string fmonth, fyear;
                string tmonth, tyear;

                string[] fdate = TxtFDate.Text.Split('/');
                fmonth = fdate[1].ToString();
                fyear = fdate[2].ToString();

                string[] tdate = TxtTDate.Text.Split('/');
                tmonth = tdate[1].ToString();
                tyear = tdate[2].ToString();

                string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&ProdCode=" + TxtAccType.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&rptname=RptGLWiseTransMonthWise.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (RbtlType.SelectedValue == "D")
            {
                string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&ProdCode=" + TxtAccType.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&rptname=RptGLWiseTransDetails.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else if (RbtlType.SelectedValue == "DT")
            {
                string fmonth, fyear;
                string tmonth, tyear;

                string[] fdate = TxtFDate.Text.Split('/');
                fmonth = fdate[1].ToString();
                fyear = fdate[2].ToString();

                string[] tdate = TxtTDate.Text.Split('/');
                tmonth = tdate[1].ToString();
                tyear = tdate[2].ToString();

                string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&ProdCode=" + TxtAccType.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&rptname=RptOfficeGLWiseTransDetails.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
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
                TxtFDate.Focus();
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
                TxtFDate.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void btnSumryPrint_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ProdTrans_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string fmonth, fyear;
            string tmonth, tyear;

            string[] fdate = TxtFDate.Text.Split('/');
            fmonth = fdate[1].ToString();
            fyear = fdate[2].ToString();

            string[] tdate = TxtTDate.Text.Split('/');
            tmonth = tdate[1].ToString();
            tyear = tdate[2].ToString();

            string redirectURL = "FrmRView.aspx?BranchID=" + TxtBrID.Text + "&ProdCode=" + TxtAccType.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&rptname=RptOfficeGLWiseTransSumary.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}