using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmNoticeCharges : System.Web.UI.Page
{
    ClsNoticeCharges NC = new ClsNoticeCharges();

    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("FrmLogin.aspx");
        }

       string dt= DateTime.Now.Date.ToString("dd-MMM-yy").Trim();
        BindGrid();
    }
  
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            NC.InsertRecord(txtDate.Text, txtDesc.Text, RdtSecured.SelectedValue, Convert.ToInt32(txtCharges.Text), Convert.ToInt32(txtTax.Text), Session["MID"].ToString(), Session["EntryDate"].ToString());
            WebMsgBox.Show("Record Added Successfully!!!", this.Page);
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Notice_Charges _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                BindGrid();
                Clear();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindGrid()
    {
        try
        {
            NC.BindGrid(GrdNotice);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void Clear()
    {
        txtCharges.Text = "";
        txtDate.Text = "";
        txtDesc.Text = "";
        txtTax.Text = "";
    }
}