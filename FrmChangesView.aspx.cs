using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmChangesView : System.Web.UI.Page
{
    ClsChangesView CV = new ClsChangesView();
    DataTable dt = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            bindgrid();
            ViewState["FLAG"] = "AD";
        }

    }
    public void bindgrid()
    {
        try
        {
            int res = CV.GetChangesData(grdChanges);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
 
    protected void grdChanges_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           
            
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void ChkSts_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void Btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            string sts="";
            int RESULT = 0;
            if (ChkSts.Checked==true)
                sts="yes";
            else
                sts="";



            if (ViewState["FLAG"].ToString() == "MD")
            {
                RESULT = CV.UPDATE(ViewState["FLAG"].ToString(), ViewState["PID"].ToString(), txtRemark.Text, sts);
                if (RESULT > 0)
                {
                    WebMsgBox.Show("Data Updated Successfully!!", this.Page);
                    bindgrid();
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "IsueeDetails _Mod_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;
                }
            }

            else if (ViewState["FLAG"].ToString() == "DL")
            {
                RESULT = CV.DELETE(ViewState["FLAG"].ToString(), ViewState["PID"].ToString(), txtRemark.Text, sts);
                if (RESULT > 0)
                {
                    WebMsgBox.Show("Data Deleted Successfully!!", this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "IsueeDetails _Del_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    bindgrid();
                    return;
                }
            }
            else if (ViewState["FLAG"].ToString() == "AD")
            {
                RESULT = CV.INSERT(ViewState["FLAG"].ToString(), txtRemark.Text, sts);
                if (RESULT > 0)
                {
                    WebMsgBox.Show("Data Added Successfully!!", this.Page);
                    bindgrid();
                    FL = "Insert";//Dhanya Shetty
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "IsueeDetails _Add_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    return;
                }
            }
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
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
            Btn_submit.Text = "Modify";
            LinkButton objlink = (LinkButton)sender;
            string srno = objlink.CommandArgument;
            ViewState["FLAG"]="MD";
            ViewState["PID"] = srno;
            dt = CV.GetChangesDataTable(srno);
            assigndata(dt);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void assigndata(DataTable dt)
    {
        txtRemark.Text = dt.Rows[0]["REMARK"].ToString();
        string sts = dt.Rows[0]["STATUS"].ToString();
        if (sts == "yes")
            ChkSts.Checked = true;
        else
            ChkSts.Checked = false;
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            Btn_submit.Text = "Delete";
            LinkButton objlink = (LinkButton)sender;
            string srno = objlink.CommandArgument;
            ViewState["FLAG"] = "DL";
            ViewState["PID"] = srno;
            dt = CV.GetChangesDataTable(srno);
            assigndata(dt);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            Btn_submit.Text = "Submit";
            txtRemark.Text = "";
            ChkSts.Checked = false;
            LinkButton objlink = (LinkButton)sender;
            string srno = objlink.CommandArgument;
            ViewState["FLAG"] = "AD";
            ViewState["PID"] = srno;
            
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}