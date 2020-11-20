using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAVS5054 : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsBindDropdown bd = new ClsBindDropdown();
    Mobile_Service MS = new Mobile_Service();
    ClsAVS5054 AVS5054 = new ClsAVS5054();
    string FLAG = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                hdnCount.Value = "1";
                //GetMobile("0");
               // bd.BindBulkType(ddltype);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void Rdb_Specific_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            GetMobile("0");
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void Rdb_All_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            GetMobile("1");
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void GetMobile(string Flag)
    {
        try
        {
            string Mobile = string.Empty;
            DataTable DT = new DataTable();
            DT = AVS5054.GetMobileNo(Session["BRCD"].ToString(), Flag);
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (i == DT.Rows.Count)
                        Mobile += DT.Rows[i]["Mobile1"].ToString();
                    else
                        Mobile += DT.Rows[i]["Mobile1"].ToString() + ",";
                }
            }
            TxtMobile.Text = Mobile;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void Btn_send_Click(object sender, EventArgs e)
    {
        try
        {
            //if (ddltype.SelectedValue == "0")
            //{
            //    WebMsgBox.Show("Please Select type..!!", this.Page);
            //    return;
            //}
            if (TxtMsg.Text == "" && TxtMsgE.Text == "")
            {
                WebMsgBox.Show("You cannnot send blank message please write a message..!!", this.Page);
                return;
            }
           // string PARA = AVS5054.GETSMSPARA(Session["BRCD"].ToString());
            //if (PARA != "Y")
            //{
                
            //    WebMsgBox.Show("SMS send failed..!!", this.Page);
            //    return;
            //}
          //if(ddltype.SelectedValue=="1")
          //    FLAG="CUST";
          //  if(ddltype.SelectedValue=="2")
          //    FLAG="MEMB";
          //  if(ddltype.SelectedValue=="3")
          //    FLAG="AGENT";
            string MSG = "";
            if (RDBLANG.SelectedValue == "0")
                MSG = TxtMsgE.Text;
            else
                MSG = TxtMsg.Text;
            int RS = AVS5054.InsertRecord(Session["BRCD"].ToString(), MSG, Session["EntryDate"].ToString(), TxtMobile.Text, RDBLANG.SelectedValue.ToString(), txtSMSDate.Text, RDBLANG.SelectedValue.ToString());
            //string RESPONSE = MS.SenBulkSMS(FLAG, Session["BRCD"].ToString(), "'N'" + TxtMsg.Text, Session["EntryDate"].ToString(), TxtMobile.Text, RDBLANG.SelectedValue.ToString());
            if (RS>0)
            WebMsgBox.Show("Saved Successfully..!!", this.Page);
            TxtMobile.Text = "";
            TxtMsg.Text = "";
            TxtMsgE.Text = "";
            txtSMSDate.Text = "";
            hdnCount.Value = "1";

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void Exit_Click(object sender, EventArgs e)
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void RDBLANG_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (RDBLANG.SelectedValue == "0")
            {
                DivMsg.Visible = false;
                DivMsgE.Visible = true;
                TxtMsg.Text = "";
                TxtMsgE.Text = "";
                hdnCount.Value = "1";
            }
            else
            {
                DivMsg.Visible = true;
                DivMsgE.Visible = false;
                TxtMsg.Text = "";
                TxtMsgE.Text = "";
                hdnCount.Value = "1";
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
}