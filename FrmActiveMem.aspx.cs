using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class FrmActiveMem : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
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
            //added by ankita 07/10/2017 to make user frndly 
            TxtFDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
            TxtTDate.Text = Session["EntryDate"].ToString();
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FL = "Insert";//ankita 14/09/2017
        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "ActiveMem_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

        string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&Edate="+Session["ENTRYDATE"].ToString()+"&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&MID=" + "&rptname=RptActiveMem.rdlc";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmBlank.aspx");
    }
}