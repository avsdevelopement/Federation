using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmCreateAvsbTable : System.Web.UI.Page
{
    ClsGenrateAVSBTable TC = new ClsGenrateAVSBTable();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    string[] FromDate, ToDate;
    int result = 0;

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
                txtBrCode.Text = Session["BRCD"].ToString();
                txtBrName.Text = Session["BName"].ToString();
                hdnUserGrp.Value = Session["UGRP"].ToString();

                txtBrCode.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtBrCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtBrCode.Text != "")
            {
                string bname = TC.GetBranchName(txtBrCode.Text.Trim().ToString());
                if (bname != null)
                {
                    txtBrName.Text = bname;
                    Btn_Submit.Focus();
                }
                else
                {
                    lblMessage.Text = "Enter valid Branch Code...!!";
                    ModalPopup.Show(this.Page);
                    return;
                }
            }
            else
            {
                lblMessage.Text = "Enter Branch Code...!!";
                ModalPopup.Show(this.Page);
                txtBrCode.Focus();
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            FromDate = txtFDate.Text.ToString().Split('/');
            ToDate = txtTDate.Text.ToString().Split('/');

            result = TC.GenrateTable(txtBrCode.Text.Trim().ToString(), FromDate[1].ToString(), ToDate[1].ToString(), FromDate[2].ToString(), ToDate[2].ToString(), Session["MID"].ToString());

            if (result < 0)
            {
                lblMessage.Text = "Somthing wrong..Table not created...!!";
                ModalPopup.Show(this.Page);
                return;
            }
            else if (result > 0)
            {
                lblMessage.Text = "Record Inserted Successfully In AVSB...!!";
                ModalPopup.Show(this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CreateBtable _"+ "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Btn_ClearAll_Click(object sender, EventArgs e)
    {
        try
        {
            txtFDate.Text = "";
            txtTDate.Text = "";

            txtFDate.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
  
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}