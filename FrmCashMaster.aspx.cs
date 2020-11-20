using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class CashMaster : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    int Result;
    string PNO;
    double Credit, Debit;
    ClsCashCounter cc = new ClsCashCounter();
    protected void Page_Load(object sender, EventArgs e)
    {
        string Flag;
        Flag = Request.QueryString["Flag"].ToString();
        try
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }
                BD.bindCashtype(ddltype);
                if (Flag == "AD")
                {
                    txtusercode.Focus();
                    btnSubmit.Visible = true;
                    btnModify.Visible = false;
                    btnDelete.Visible = false;
                    BTNAUTHO.Visible = false;

                }
                else if (Flag == "MD")
                {
                    BindData();
                    btnSubmit.Visible = false;
                    btnModify.Visible = true;
                    btnDelete.Visible = false;
                    BTNAUTHO.Visible = false;
                }
                else if (Flag == "DL")
                {
                    BindData();
                    btnSubmit.Visible = false;
                    btnModify.Visible = false;
                    btnDelete.Visible = true;
                    BTNAUTHO.Visible = false;
                }
                else if (Flag == "AT")
                {
                    BindData();
                    btnSubmit.Visible = false;
                    btnModify.Visible = false;
                    btnDelete.Visible = false;
                    BTNAUTHO.Visible = true;
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void Cleardata()
    {
        txtusername.Text = "";
        txtusercode.Text = "";
        txtcreditlimit.Text = "";
        txtdebitlimit.Text = "";
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (txtusercode.Text == "")
        {
            WebMsgBox.Show("Enter user code", this.Page);
        }
        if (txtcreditlimit.Text == "")
        {
            WebMsgBox.Show("Enter credit limit", this.Page);

        }
        if (txtdebitlimit.Text == "")
        {
            WebMsgBox.Show("Enter debit limit", this.Page);
        }
        else
        {
            Result = cc.DeleteData(txtusercode.Text, conn.PCNAME(), Session["BRCD"].ToString(), Session["MID"].ToString());
            if (Result > 0)
            {
                WebMsgBox.Show("Data Delete Successfully..!!", this.Page);
                BindData();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CashCountrMgm_Del _" + txtusercode.Text + "_"  + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                Cleardata();
                return;
            }

        }
    }
   
    protected void btnModify_Click(object sender, EventArgs e)
    {
        if (txtcreditlimit.Text == "")
        {
            WebMsgBox.Show("Enter Credit limit", this.Page);
        }
        if (txtdebitlimit.Text == "")
        {
            WebMsgBox.Show("Enter debit Limit", this.Page);
        }
        else
        {
            Result = cc.ModifyData(txtusercode.Text, ddltype.SelectedValue, txtdebitlimit.Text, txtcreditlimit.Text, conn.PCNAME(), Session["BRCD"].ToString());
            if (Result > 0)
            {
                WebMsgBox.Show("Data Modified Successfully", this.Page);
                BindData();
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CashCountrMgm_Mod _" + txtusercode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                Cleardata();
                return;
            }
        }
    }
    
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtusercode.Text == "")
            {
                WebMsgBox.Show("Enter User Code", this.Page);
            }
            if (txtcreditlimit.Text == "")
            {
                WebMsgBox.Show("Enter Credit Limit", this.Page);
            }
            if (txtdebitlimit.Text == "")
            {
                WebMsgBox.Show("Enter Debit Limit ", this.Page);
            }
            else
            {
                int Data = Convert.ToInt32(cc.CheckEntry(txtusercode.Text, Session["BRCD"].ToString()));
                    if (Data > 0)
                    {
                        WebMsgBox.Show("Record already exists for this User..!!", this.Page);
                    }
                    else
                    {
                string PERMISSIONNO = cc.GETPNO(txtusercode.Text, Session["BRCD"].ToString());
                     Result = cc.InsertData(txtusercode.Text, txtusername.Text, ddltype.SelectedValue, txtdebitlimit.Text, txtcreditlimit.Text, "",PERMISSIONNO, Session["BRCD"].ToString());
                //Result = cc.InsertData(Session["BRCD"].ToString(), Session["UGRP"].ToString(), txtusercode.Text, txtusername.Text, ddltype.SelectedValue, txtdebitlimit.Text, txtcreditlimit.Text, conn.PCNAME(), Session["MID"].ToString(), "");

                     if (Result > 0)
                     {
                         BindData();

                         BTNAUTHO.Enabled = true;
                         lblMessage.Text = "Volt Create Successfully Volt No : " + PERMISSIONNO;
                         ModalPopup.Show(this.Page);
                         FL = "Insert";//Dhanya Shetty
                         string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CashCountrMgm_Add _" + txtusercode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                         Cleardata();
                         return;

                     }
                  
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    
    public int BindData()
    {
        int RC = cc.bindlimit(grdlimit, Session["BRCD"].ToString());
        return RC;
    }
    
    protected void grdlimit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdlimit.PageIndex = e.NewPageIndex;
        BindData();
    }
    
    protected void grdlimit_SelectedIndexChanged(object sender, EventArgs e)
    {
        data();


        if (ViewState["Flag"].ToString() == "D")
        {
            try
            {
                Result = cc.DeleteData(txtusercode.Text, conn.PCNAME(), Session["BRCD"].ToString(), Session["MID"].ToString());
                if (Result > 0)
                {
                    BindData();
                    WebMsgBox.Show("Record Delete Successfully...!!", this.Page);
                    Cleardata();
                    return;
                }
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }

    }
    
    protected void lnkSelect_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "M";
       LinkButton lnkedit = (LinkButton)sender;
        ViewState["USERCODE"] = lnkedit.CommandArgument;
        btnSubmit.Visible = false;
    }
   
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        ViewState["Flag"] = "D";
        LinkButton lnkdelete = (LinkButton)sender;
         ViewState["USERCODE"] = lnkdelete.CommandArgument;
         btnSubmit.Visible = false;
    }
    
    public void data()
    {
        DataTable DT = new DataTable();
        DT = cc.showdata(ViewState["USERCODE"].ToString(), Session["BRCD"].ToString());
        if (DT.Rows.Count > 0)
        {
            BD.bindCashtype(ddltype);
            txtusercode.Text = DT.Rows[0]["USERCODE"].ToString();
            txtusername.Text = DT.Rows[0]["USERNAME"].ToString();
            ddltype.SelectedValue = DT.Rows[0]["TYPE"].ToString();
            txtcreditlimit.Text = DT.Rows[0]["CASHCREDITLIMIT"].ToString();
            txtdebitlimit.Text = DT.Rows[0]["CASHCREDITLIMIT"].ToString();
            BindData();
   
        }
        else
        {
            WebMsgBox.Show("NO RECORD FOUND!......", this.Page);
        }
    
    }
    
    protected void txtusercode_TextChanged(object sender, EventArgs e)
    {
        
        try
        {
            
            string RC = cc.checkuser(txtusercode.Text, Session["BRCD"].ToString());
            if (Convert.ToInt32(RC) <= 0)
            {
                WebMsgBox.Show("User  not Exist..!!", this.Page);

            }
            else
            {
                string Result = cc.getuname(txtusercode.Text, Session["BRCD"].ToString());
                txtusername.Text = Result;
                string Rc = cc.CheckGroup(txtusercode.Text, Session["BRCD"].ToString());
                if (Convert.ToInt32(Rc) != 5)
                {
                    WebMsgBox.Show("User is not  Cashier...!", this.Page);
                    txtusercode.Text = "";
                    txtusername.Text = "";
                    txtusercode.Focus();
                    BindData();
                }
                else
                    ddltype.Focus();
            }
           
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
       
    }
    
    protected void txtusername_TextChanged(object sender, EventArgs e)
    {
        ddltype.Focus();
    }
    
    protected void txtdebitlimit_TextChanged(object sender, EventArgs e)
    {
        Debit = Convert.ToDouble(txtdebitlimit.Text == "" ? "0" : txtdebitlimit.Text);
        if (Debit == 0)
        {
            WebMsgBox.Show("Amount Cannot be zero ", this.Page);
            txtdebitlimit.Text = "";
            txtdebitlimit.Focus();
        }
        else
        {
            txtcreditlimit.Focus();
        }
        
    }
    
    protected void txtcreditlimit_TextChanged(object sender, EventArgs e)
    {
        Credit = Convert.ToDouble(txtcreditlimit.Text == "" ? "0" : txtcreditlimit.Text);
        if (Credit == 0)
        {
            WebMsgBox.Show("Amount Cannot be zero ", this.Page);
            txtcreditlimit.Text = "";
            txtcreditlimit.Focus();
        }
        else
        {
            btnSubmit.Focus();
        }
       
    }
    
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtdebitlimit.Focus();
    }
    
    protected void BTNAUTHO_Click(object sender, EventArgs e)
    {

        PNO = cc.GETPNO(txtusercode.Text, Session["BRCD"].ToString());
        if (PNO != null)
        {
            DT = cc.GETNOTETYPE(Session["BRCD"].ToString());
            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    Result = cc.insertcashdemo(PNO, DT.Rows[i]["NOTE_TYPE"].ToString(), PNO, Session["BRCD"].ToString(), conn.PCNAME());
                }
            }
            WebMsgBox.Show("Record is Authorised successfully..!!", this.Page);
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CashCountrMgm_Autho _" + txtusercode.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            Result = cc.UPDATE5010(ViewState["USERCODE"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString());
            BindData();
        }
    }

}