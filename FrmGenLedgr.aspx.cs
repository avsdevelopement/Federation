using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmGenLedgr : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BtnGLDayly_Click(object sender, EventArgs e)
    {
        try 
        {
        string url = "FrmRview.aspx?rptname=RptGenLedgrDaily.rdlc&SGL=" + txtsgl.Text.ToString() + "&GL=" + txtgl.Text.ToString() + "&FD=" + txtfrmdate.Text.ToString() + "&TD=" + Txttodate.Text.ToString() + "&FLG=Daily";
        Response.Redirect(url);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnGLMonthly_Click(object sender, EventArgs e)
    {
        try 
        {
        string url = "FrmRview.aspx?rptname=RptGenLedgrDaily.rdlc&SGL=" + txtsgl.Text.ToString() + "&GL=" + txtgl.Text.ToString() + "&FD=" + txtfrmdate.Text.ToString() + "&TD=" + Txttodate.Text.ToString() + "&FLG=Monthly";
        Response.Redirect(url);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}