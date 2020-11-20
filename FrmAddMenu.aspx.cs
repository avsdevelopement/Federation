using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class FrmAddMenu : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAddMenu AM = new ClsAddMenu();
    string Flag;


    protected void Page_Load(object sender, EventArgs e)
    {
      //  BD.BINDUSERGRP(ddusergroup);
       // txtMenuId.Text = BD.Get_MenuId();

        //Flag = Request.QueryString["Flag"].ToString();

        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");

            }
            Submit.Visible = false;
            BtnUpdate.Visible = false;
            ViewState["Flag"] = "MD";
            ViewState["Flag"] = "AD";
            //if (ViewState["Flag"].ToString() == "AD")
            //{
            //    //txtMenuId.ReadOnly = true;
            //    //txtMenuId.Text = BD.Get_MenuId();
            //    //Submit.Visible = true;
            //    //BtnUpdate.Visible = false;


            //}
            //else if (ViewState["Flag"].ToString() == "MD")
            //{


            //    //Submit.Visible = false;
            //    //BtnUpdate.Visible = true;

            //}

        }
    }
    protected void txtmenutt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = txtmenutt.Text;
            string[] custnob = CUNAME.Split('#');
            if (custnob.Length > 1)
            {
                txtmenutt.Text = custnob[0].ToString();
                ParentMenuId.Text = (custnob[1].ToString());
                string GLS = BD.MenuItem(ParentMenuId.Text);

            }
            else
            {
                txtmenutt.Text = "";
                ParentMenuId.Text = "";
            }
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void ParentMenuId_TextChanged(object sender, EventArgs e)
    {
        try
        {
          //  if(int.TryParse(ParentMenuId.Text.ToString(),0))
            string CUNAME = ParentMenuId.Text;
            string[] custnob = CUNAME.Split('#');
            if (custnob.Length > 1)
            {
                txtmenutt.Text = custnob[1].ToString();
                ParentMenuId.Text = (custnob[0].ToString());
                string GLS = BD.MenuItem(ParentMenuId.Text);

            }
            else
            {
                txtmenutt.Text = "";
                ParentMenuId.Text = "";
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        //if (ParentMenuId.Text == "")
        //{
        //    WebMsgBox.Show("Please Enter ParentID....!!", this.Page);
        //    return;
        //}
        //if (txtmenutt.Text == "")
        //{
        //    WebMsgBox.Show("Please Enter Menu Title....!!", this.Page);
        //    return;
        //}
        if (txtPage.Text == "")
        {
            WebMsgBox.Show("Please Enter Page Description....!!", this.Page);
            return;
        }

        if (txtFormName.Text.Contains(".aspx") == false && txtFormName.Text.Contains("#") == false && txtFormName.Text.Contains("Javascript:;") == false)
            {
                WebMsgBox.Show("Please Enter Proper Formate....!!", this.Page);
                return;

            }
      
        string EX = AM.ChkUsrExists(txtMenuId.Text.Trim().ToString());
        if (EX != null || EX != "")
        {
            int cnt = AM.AddMenuTitle(txtMenuId.Text.ToString(), ParentMenuId: ParentMenuId.Text.Trim(), MenuTitle: txtParent.Text, PageDesc: txtPage.Text.Trim(), PageURL: txtFormName.Text, STATUS: RdbStatus.SelectedValue);

            if (cnt > 0)
            {
                lblMessage.Text = "User Created Successfully ";
                ModalPopup.Show(this.Page);
                clear();
                return;
                
            }
           
        }
        else
        {
            lblMessage.Text = "Menu Id Allready Exists.. !";
            ModalPopup.Show(this.Page);
            return;
        }
    }
    protected void txtParent_TextChanged(object sender, EventArgs e)
    {
        //string  st1=txtParent.Text;
        //string st2=txtMenuId.Text;
        if (ViewState["Flag"].ToString() == "AD")
        {

            string str = AM.ChkMenuTitle(txtParent.Text, ParentMenuId.Text);
            if (str != null)
            {
                lblMessage.Text = "Menu Title Allready Exists.. Please Try Again...!";
                ModalPopup.Show(this.Page);
                txtParent.Text = "";
                return;

            }
        }
        if (ViewState["Flag"].ToString() == "MD")
        {
            try
            {
                string CUNAME = txtParent.Text;
                string[] custnob = CUNAME.Split('#');
                if (custnob.Length > 1)
                {
                    txtParent.Text = custnob[0].ToString();
                    txtMenuId.Text = (custnob[1].ToString());
                    string GLS = BD.MenuTitle(txtMenuId.Text);
                    ShowData();
                    if (txtmenutt.Text == "/")
                    {
                        txtmenutt.Text = "";
                    }
                }
                else
                {
                    txtmenutt.Text = "";
                    txtMenuId.Text = "";
                }

            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
    }

    protected void txtFormName_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    string CUNAME = txtFormName.Text;
        //    string[] custnob = CUNAME.Split('#');
        //    if (custnob.Length > 1)
        //    {
        //        txtFormName.Text = custnob[0].ToString();
        //        txtMenuId.Text = (custnob[1].ToString());
        //        string GLS = BD.MenuItem(ParentMenuId.Text);
        //        ShowData();
        //    }
        //    else
        //    {
        //        txtmenutt.Text = "";
        //        ParentMenuId.Text = "";
        //    }

        //}
        //catch (Exception Ex)
        //{
        //    ExceptionLogging.SendErrorToText(Ex);
        //}
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        clear();
    }
    public void clear()
    {
        txtFormName.Text = "";
        txtMenuId.Text = "";
        txtmenutt.Text="";
            txtParent.Text="";
        txtPage.Text="";
        ParentMenuId.Text="";
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        int cnt = AM.UpdateMenuTitle(txtMenuId.Text.ToString(), ParentMenuId: ParentMenuId.Text.Trim(), MenuTitle: txtParent.Text, PageDesc: txtPage.Text.Trim(), PageURL: txtFormName.Text, STATUS: RdbStatus.SelectedValue);

            if (cnt > 0)
            {
                lblMessage.Text = "User update Successfully ";
                ModalPopup.Show(this.Page);
                clear();
                return;
            }
           
        else
        {
            lblMessage.Text = "Try again.. !";
            ModalPopup.Show(this.Page);
            return;
        }
    }
   


    public void ShowData()
    {
        DataTable dt = new DataTable();
        string str = "0";
       dt = AM.MenuTitleDes(txtMenuId.Text.Trim().ToString());
       if (dt.Rows.Count != 0)
       {

           txtMenuId.Text = dt.Rows[0]["MenuId"].ToString();
           txtParent.Text = dt.Rows[0]["MenuTitle"].ToString();
            ParentMenuId.Text = dt.Rows[0]["ParentMenuId"].ToString();
            txtmenutt.Text = AM.GetParent(ParentMenuId.Text);
          // txtmenutt.Text = dt.Rows[0][""].ToString();
           txtFormName.Text = dt.Rows[0]["PageURL"].ToString();
           txtPage.Text = dt.Rows[0]["PageDesc"].ToString();
           RdbStatus.Text = dt.Rows[0]["STATUS"].ToString();
          
       }
    }
    protected void txtMenuId_TextChanged(object sender, EventArgs e)
    {
        ShowData();
    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
         try
         {
             ViewState["Flag"] = "AD";
             txtMenuId.ReadOnly = true;
             txtMenuId.Text = BD.Get_MenuId();
             Submit.Visible = true;
             BtnUpdate.Visible = false;
            
         }
         catch (Exception Ex)
         {
             ExceptionLogging.SendErrorToText(Ex);
         }
    }
    protected void lnkModify_Click(object sender, EventArgs e)
    {
        try
        {
            clear();
           // txtproductcode.Enabled = true;
          //  txtintre.Enabled = true;
            ViewState["Flag"] = "MD";
            Submit.Visible = false;
            txtMenuId.ReadOnly = false;
            BtnUpdate.Visible = true;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string redirectURL = "FrmRView.aspx?rptname=RptMenu.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + redirectURL + "', 'popup_window', 'width=1100,height=400,left=150,top=150,resizable=no');", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}