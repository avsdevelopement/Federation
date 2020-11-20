using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAVS5048 : System.Web.UI.Page
{
    ClsAVS5048 ODC = new ClsAVS5048();
    DbConnection conn = new DbConnection();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FLa = "";
    int Res = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }
                txtbrcd.Focus();
            } 
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtbrcd.Text != "" && txtfromdate.Text != "")
            {
                Res = ODC.calculatedata(txtbrcd.Text, txtfromdate.Text, Session["LOGINCODE"].ToString(), Session["MID"].ToString(), conn.PCNAME());
                if (Res > 0)
                {
                    WebMsgBox.Show("OD calculated successfully", this.Page);
                    FLa = "Insert";//Dhanya Shetty//05/10/2017
                    string Res1 = CLM.LOGDETAILS(FLa, Session["BRCD"].ToString(), Session["MID"].ToString(), "ODList_Cal_" + txtbrcd.Text +"_"+txtfromdate.Text+"_"+ Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    Cleardata();
                }
            }
            else
            {
                WebMsgBox.Show("Please enter brcd and As on date", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void Cleardata()
    {
        txtbrcd.Text="";
        txtfromdate.Text = "";
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtbrcd.Text != "" && txtfromdate.Text != "")
            {
                FLa = "Insert";//Dhanya Shetty//05/10/2017
                string Res1 = CLM.LOGDETAILS(FLa, Session["BRCD"].ToString(), Session["MID"].ToString(), "ODList_Cal_Rpt_" + txtbrcd.Text + "_" + txtfromdate.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                string redirectURL = "FrmRView.aspx?BRCD=" + txtbrcd.Text + "&EDT=" + txtfromdate.Text + "&UserId=" + Session["UserName"].ToString() + "&rptname=RptAVS5048.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else
            {
                WebMsgBox.Show("Please enter brcd and Ason date", this.Page);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}