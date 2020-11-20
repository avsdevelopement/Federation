using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmHoliday : System.Web.UI.Page
{
    
    int Result,Res;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    ClsHoliday CH = new ClsHoliday();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
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

                BD.BindYear(DDlYear);
                BD.BindDay(DdlDay);
               
             }
  }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void RbtnWeekly_CheckedChanged(object sender, EventArgs e)
    {
        RbtnWeekly.Checked = true;
        RbtnHoliday.Checked = false;
        if (RbtnWeekly.Checked == true)
        {

            div_Day.Visible = true;
            lblDay.Visible = true;
            div_holiday.Visible = false;
            GrdHoliday.Visible = false;
        }
        else
        {
            div_holiday.Visible = false;
            GrdHoliday.Visible = false;
            div_Day.Visible = false;
            lblDay.Visible = false;
        }
       
        
    }
    protected void RbtnHoliday_CheckedChanged(object sender, EventArgs e)
    {
        RbtnWeekly.Checked = false;
        RbtnHoliday.Checked = true;
        if (RbtnHoliday.Checked == true)
        {
            div_holiday.Visible = true;
            GrdHoliday.Visible = true;
            Bindgrid();
            div_Day.Visible = false;
            lblDay.Visible = false;
        }
        else
        {
            div_holiday.Visible = false;
            GrdHoliday.Visible = false;
            div_Day.Visible = false;
          lblDay.Visible = false;
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (RbtnHoliday.Checked == true)
            {
                if (Txtdate.Text != "" && Txtreason.Text != "")
                {
                    Result = CH.insertdata(Txtdate.Text, Txtreason.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), conn.PCNAME(), "1", "1001");

                    if (Result > 0)
                    {
                        WebMsgBox.Show("Data inserted successfully", this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Holiday _Add_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearData();
                        Bindgrid();
                    }
                }
            }
            else if(RbtnWeekly.Checked ==true)
            {
              if( DdlDay.SelectedValue!="")
                {
                    Res = CH.InsertWeekly(DdlDay.SelectedValue, DDlYear.SelectedItem.Text);
                    if (Res > 0)
                    {
                        WebMsgBox.Show("Data inserted successfully", this.Page);
                        FL = "Insert";//Dhanya Shetty
                        string Res1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Holiday _Add_Weekly_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        ClearData();
                        lblDay.Visible = true;
                    }
                }
            }
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        ClearData();
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    public void ClearData()
    {
        Txtdate.Text="";
        Txtreason.Text = "";
        DDlYear.SelectedItem.Text =" 0";
    }
    protected void Bindgrid()
    {
        try
        {
            DT = CH.Display(GrdHoliday, Session["EntryDate"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
 
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
           
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
             string[] ARR = str.Split(',');
            ViewState["Id"] = ARR[0].ToString();
            ViewState["HOLIDAYDATE"] = ARR[1].ToString();
            ViewState["REASON"] = ARR[2].ToString();
            BtnSubmit.Text = "Delete";
             Res = CH.Deletedata(ViewState["Id"].ToString(), ViewState["HOLIDAYDATE"].ToString(), ViewState["REASON"].ToString());
            if (Res > 0)
            {
                WebMsgBox.Show("Data Deleted!",this.Page);
                 FL = "Insert";//Dhanya Shetty
                 string Res1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Holiday _Del_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                ClearData();
                Bindgrid();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void DdlDay_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblDay.Text = DdlDay.SelectedItem.Text;
    }
}