using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmS0001 : System.Web.UI.Page
{
   
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsSRO SRO = new ClsSRO();
    DataTable DT = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
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

                TxtBRCD.Text = Session["BRCD"].ToString();
                TxtBRCDName.Text = Session["BName"].ToString();
                TxtEffectDate.Text = Session["EntryDate"].ToString();
                ViewState["FLAG"] = "AD";
                if(ViewState["FLAG"].ToString()== "AD")
                {
                    lblActivity.Text = "Add New SRO";
                    TxtEffectDate.Text = Session["EntryDate"].ToString();
                    ENTF(true);
                }
                TxtSRONo.Text = SRO.GetSrno();
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
   
    protected void TxtBRCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBRCD.Text != "")
            {
                string bname = AST.GetBranchName(TxtBRCD.Text);
                if (bname != null)
                {
                    TxtBRCDName.Text = bname;
                    TxtRegSanc.Focus();

                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    TxtBRCD.Text = "";
                    TxtRegSanc.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                TxtBRCD.Text = "";
                TxtRegSanc.Focus();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["FLAG"].ToString() == "AD")
            {
                result = SRO.Insert(TxtBRCD.Text, TxtEffectDate.Text, TxtBoardResNo.Text, TxtRegSanc.Text, TxtRemark.Text, TxtEmpId.Text, Session["MID"].ToString(),TXTSROName.Text);
                if (result > 0)
                {
                    WebMsgBox.Show("Data Saved successfully with SRNO=" + TxtSRONo.Text, this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_Master_Add _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    clear();
                }
            }
            else if (ViewState["FLAG"].ToString() == "MD")
            {
                result = SRO.Modify(TxtBRCD.Text, TxtSRONo.Text, TxtEffectDate.Text, TxtBoardResNo.Text, TxtRegSanc.Text, TxtRemark.Text, TxtEmpId.Text, Session["MID"].ToString(),TXTSROName.Text);
                if (result > 0)
                {
                    WebMsgBox.Show("Data Modified successfully..!!", this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_Master_Mod _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    clear();
                }
            }
            else if (ViewState["FLAG"].ToString() == "AT")
            {
                result = SRO.Authorise(TxtBRCD.Text, TxtSRONo.Text, Session["MID"].ToString());
                if (result > 0)
                {
                    WebMsgBox.Show("Data Authorised successfully..!!", this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_Master_Auth _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    clear();
                }
            }
            else if (ViewState["FLAG"].ToString() == "CA")
            {
                result = SRO.Cancel(TxtBRCD.Text, TxtSRONo.Text, Session["MID"].ToString());
                if (result > 0)
                {
                    WebMsgBox.Show("Data Canceled successfully..!!", this.Page);
                    FL = "Insert";//Dhanya Shetty
                    string Result1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SRODetails_Master_Cancel _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    clear();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        try
        {
            clear();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        try
        {
             HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
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
            ViewState["FLAG"] = "AD";
            clear();
            BtnSubmit.Text = "Submit";
            lblActivity.Text = "Add New SRO";
            TxtEffectDate.Text = Session["EntryDate"].ToString();
            ENTF(true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "CA";
            clear();
            BtnSubmit.Text = "Cancel";
            TxtSRONo.Enabled = true;
            lblActivity.Text = "Cancel SRO";
            TxtEffectDate.Text = Session["EntryDate"].ToString();
            ENTF(false);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }
    protected void lnkAuthorized_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "AT";
            clear();
            BtnSubmit.Text = "Authorise";
            TxtSRONo.Enabled = true;
            lblActivity.Text = "Authorise SRO";
            TxtEffectDate.Text = Session["EntryDate"].ToString();
            ENTF(false);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }
    protected void lnkModify_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["FLAG"] = "MD";
            clear();
            BtnSubmit.Text = "Modify";
            TxtSRONo.Enabled = true;
            lblActivity.Text = "Modify SRO";
            TxtEffectDate.Text = Session["EntryDate"].ToString();
            ENTF(true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }
    public void clear()
    {
       
        TxtBRCD.Text = Session["BRCD"].ToString();
        TxtBRCDName.Text = Session["BName"].ToString();
        TxtEffectDate.Text = "";
        TxtBoardResNo.Text = "";
        TxtRegSanc.Text = "";
        TxtRemark.Text = "";
        TxtEmpId.Text = "";
        TXTSROName.Text = "";
        if (ViewState["FLAG"].ToString() == "AD")
        TxtSRONo.Text = SRO.GetSrno();
        else
        TxtSRONo.Text = "";
    }
    protected void TxtSRONo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ViewDetails(TxtSRONo.Text, TxtBRCD.Text);
            TxtEffectDate.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void ViewDetails(string brcd, string srno)
    {
        try
        {
            DT = SRO.ViewDetails(TxtSRONo.Text, TxtBRCD.Text);
            if (DT.Rows.Count > 0)
            {
                TxtEffectDate.Text = DT.Rows[0]["EFFECT_DT"].ToString();
                TxtBRCD.Text = DT.Rows[0]["BRCD"].ToString();
                TxtBRCDName.Text = AST.GetBranchName(TxtBRCD.Text);
                TxtBoardResNo.Text = DT.Rows[0]["BOARD_RESO"].ToString();
                TxtRegSanc.Text = DT.Rows[0]["REG_SANCTION"].ToString();
                TxtRemark.Text = DT.Rows[0]["REMARK"].ToString();
                TxtEmpId.Text = DT.Rows[0]["EMP_ID"].ToString();
                TXTSROName.Text = DT.Rows[0]["SRONAME"].ToString();
            }
            else
            {
                WebMsgBox.Show("No record found for srno=" + TxtSRONo.Text , this.Page);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public bool ENTF(bool TF)
    {
        try
        {
            TxtBRCDName.Enabled = TF;
            TxtEffectDate.Enabled = TF;
            TxtBoardResNo.Enabled = TF;
            TxtRegSanc.Enabled = TF;
            TxtRemark.Enabled = TF;
            TxtEmpId.Enabled = TF;
            TXTSROName.Enabled = TF;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return TF;
    }

    protected void TxtEffectDate_TextChanged(object sender, EventArgs e)
    {
        TxtBRCD.Focus();
    }
    protected void TxtRegSanc_TextChanged(object sender, EventArgs e)
    {
        TxtBoardResNo.Focus();
    }
    protected void TxtBoardResNo_TextChanged(object sender, EventArgs e)
    {
        TxtEmpId.Focus();
    }
    protected void TxtEmpId_TextChanged(object sender, EventArgs e)
    {
        TxtRemark.Focus();
    }
}