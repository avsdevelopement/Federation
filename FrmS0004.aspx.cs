using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmS0004 : System.Web.UI.Page
{
    ClsAccountSTS CAS = new ClsAccountSTS();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsFollowoupmaster CFM = new ClsFollowoupmaster();
    ClsSRO SRO = new ClsSRO();
    ClsCommon CMN = new ClsCommon();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    string sroname = "", AC_Status = "";
    DataTable DT = new DataTable();
    DataTable DT1 = new DataTable();
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
                    txtBrcd.Focus();
                    txtBrcd.Text = Session["BRCD"].ToString();
                    txtBrcdname.Text=CAS.GetBranchName(Session["BRCD"].ToString());
                    TxtFollowDt.Text = Session["EntryDate"].ToString();
                    TxtFdate.Text = Session["EntryDate"].ToString();
                    SRO.BindMainGrd(GrdFollowup, Session["BRCD"].ToString(), Session["EntryDate"].ToString());
                    DT = SRO.GetCaseYearFile(Session["EntryDate"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        txtCaseNO.Text = DT.Rows[0]["CASENO"].ToString();
                        txtCaseY.Text = DT.Rows[0]["CASE_YEAR"].ToString();
                    }
            }
            
        }
        catch (Exception Ex) 
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        
    }
    protected void txtBrcd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtBrcd.Text != "")
            {
                string bname = CAS.GetBranchName(txtBrcd.Text);
                if (bname != null)
                {
                    txtBrcdname.Text = bname;
                    txtCaseY.Focus();

                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    txtBrcd.Text = "";
                    txtBrcd.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                txtBrcd.Text = "";
                txtBrcd.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
       
    }
    public void clearall()
    {
       // txtCaseY.Text = "";
        //txtCaseNO.Text = "";
        
        txtSRo.Text = "";
        TXTREMARKS.Text = "";
        TxtFdate.Text = Session["EntryDate"].ToString();
        txtnextfile.Text = "";
       // TxtMob.Text = "";
        //TxtTel.Text = "";
       
    }
    
    protected void Submit_Click(object sender, EventArgs e)
    {
        if (txtCaseNO.Text == "")
        {
            WebMsgBox.Show("Please Enter Case No.", this.Page);
            return;
        }
        if(txtBrcd.Text=="")
        {
            WebMsgBox.Show("Please Enter Branch code.", this.Page);
            return;
        }
        if (txtCaseY.Text=="")
        {
            WebMsgBox.Show("Please Enter Case Year.", this.Page);
            return;
        }
        if (txtnextfile.Text=="")
        {
            WebMsgBox.Show("Please Enter Next Follow Up date.", this.Page);
            return;
        }
       
        int RESULT = CFM.insertdata(txtBrcd.Text, txtCaseY.Text, txtCaseNO.Text, string.IsNullOrEmpty(txtSRo.Text)?"0":txtSRo.Text, txtFilestatus.Text, TxtFdate.Text, TXTREMARKS.Text, txtnextfile.Text, Session["MID"].ToString(), Session["EntryDate"].ToString(),txtMemberNo.Text);
        if (RESULT > 0)
        {
            
            WebMsgBox.Show("Record Saved Succesfully.", this.Page);
            bind();
            FL = "Insert";//Dhanya Shetty
            string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_Followup_Add _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            clearall();
        }
        else
        {
            WebMsgBox.Show("Record Not Saved.", this.Page);
        }
    }
    protected void Clear_Click(object sender, EventArgs e)
    {
        clearall();
    }
    protected void Exit_Click(object sender, EventArgs e)
    {
        clearall();
        div_followup.Visible = false;
        div_grid.Visible = false;
        div_maingrd.Visible = true;
    }
    protected void Submit_Click1(object sender, EventArgs e)
    {

    }
  
    protected void txtSRo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            sroname = SRO.GetSROName(txtSRo.Text);
            TXTSROName.Text = sroname;
            txtFilestatus.Focus();                    
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        
    }
   
    protected void txtFilestatus_TextChanged(object sender, EventArgs e)
    {
        TxtFdate.Focus();
    }
    protected void TxtFdate_TextChanged(object sender, EventArgs e)
    {
        TXTREMARKS.Focus();
    }
    protected void TXTREMARKS_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TXTREMARKS.Text.Length > 35)
            {
                WebMsgBox.Show("Remark should be of 35 character long only..!!", this.Page);
                TXTREMARKS.Text = "";
            }
            txtnextfile.Focus();

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {
             LinkButton objlnk = (LinkButton)sender;
             string id = objlnk.CommandArgument;
             DT = SRO.ViewMainGrid(Session["BRCD"].ToString(), id);
             if (DT.Rows.Count > 0)
             {
                 div_followup.Visible = true;
                 div_grid.Visible = true;
                 div_maingrd.Visible = false;
                 txtBrcd.Text = DT.Rows[0]["BRCD"].ToString();
                 txtBrcdname.Text = CAS.GetBranchName(txtBrcd.Text);
                 txtCaseY.Text = DT.Rows[0]["CASE_YEAR"].ToString();

                 txtCaseNO.Text = DT.Rows[0]["CASENO"].ToString();
                  txtSRo.Text = DT.Rows[0]["SRNO"].ToString();
                 TXTSROName.Text = SRO.GetSROName(txtSRo.Text);
                 txtFilestatus.Text = DT.Rows[0]["FILE_STATUS"].ToString();
                 TxtFdate.Text = DT.Rows[0]["F_DATE"].ToString();
                 TXTREMARKS.Text = DT.Rows[0]["F_REEAMRKS"].ToString();
                 txtnextfile.Text = DT.Rows[0]["NEXT_F_DT"].ToString();
                 txtMemberNo.Text = DT.Rows[0]["MEMBERNO"].ToString();
                // garDetails(DT.Rows[0]["BRCD"].ToString(), DT.Rows[0]["PRCDCD"].ToString(), Session["EntryDate"].ToString(), DT.Rows[0]["ACCNO"].ToString());
                
                 bind();
             }
             else
             {
                 WebMsgBox.Show("No record found..!!", this.Page);
             }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void bind()
    {
        try
        {
            SRO.BindGrd(grd: GrdS0004,brcd: txtBrcd.Text,CASE_YEAR: txtCaseY.Text,CASENO: txtCaseNO.Text);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnAddNew_Click(object sender, EventArgs e)
    {
        try
        {
                div_followup.Visible = true;
                div_grid.Visible = true;
                div_maingrd.Visible = false;

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
      
    }
    
    protected void BtnSubmit1_Click(object sender, EventArgs e)
    {
        try
        {
            SRO.BindMainGrd(GrdFollowup, Session["BRCD"].ToString(), TxtFollowDt.Text);
            FL = "Insert";//Dhanya Shetty
            string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_Followup _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
  
    public void contact()
    {
        try
        {
            DT = SRO.GetContact(txtBrcd.Text, txtCaseNO.Text, txtCaseY.Text);
           // TxtMob.Text = DT.Rows[0]["mobile1"].ToString();
           // TxtTel.Text = DT.Rows[0]["tel"].ToString();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}