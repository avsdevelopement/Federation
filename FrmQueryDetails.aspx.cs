using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmQueryDetails : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsQueryDetails QD = new ClsQueryDetails();
    protected void Page_Load(object sender, EventArgs e)
    {
       if(!IsPostBack)
        {
            BindPending();
            BindSolved();
        }
    }
    protected void grdPending_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdSolved_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public void BindPending()
    {
        try
        {
            int res = QD.GetPending(grdPending, Session["LOGINCODE"].ToString(), Session["BRCD"].ToString(), Session["BNKCDE"].ToString());
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void BindSolved()
    {
        try
        {
            int res1 = QD.GetSolevd(grdSolved, Session["BRCD"].ToString(), Session["BNKCDE"].ToString());
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
       // HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
}