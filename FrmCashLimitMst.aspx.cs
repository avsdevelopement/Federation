using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmCashLimitMst : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    ClsCashLimitMst CM = new ClsCashLimitMst();
    int result = 0;
    string sql = "",FL="";
    DataTable dt = new DataTable();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            bindgrid();
        }
    }
    protected void TxtPrd_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string tds = BD.GetLoanGL(TxtPrd.Text, Session["BRCD"].ToString());
            if (tds != null)
            {
                string[] TD = tds.Split('_');
                if (TD.Length > 0)
                {

                }
                TxtProdName.Text = TD[0].ToString();
                TxtPrd.Text = TD[1].ToString();
                TxtEffectDate.Focus();
            }
            else
            {
                WebMsgBox.Show("Invalid Deposit Code......!", this.Page);
                TxtPrd.Text = "";
                TxtProdName.Text = "";
                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtProdName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string pno = TxtProdName.Text;
            string[] prd = pno.Split('_');
            if (prd.Length > 0)
            {
                TxtProdName.Text = prd[0].ToString();
                TxtPrd.Text = prd[1].ToString();
            }
            TxtEffectDate.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void grdCashLimit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

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
            Btn_Submit.Visible = false;
            Btn_Delete.Visible = true;
            Btn_Modify.Visible = false;
            LinkButton objlnk = (LinkButton)sender;
            string srno = objlnk.CommandArgument;
            ViewState["id"] = srno;
            dt=CM.GetCashLimit( ViewState["id"].ToString(),Session["BRCD"].ToString());
            assign(dt);
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
            Btn_Submit.Visible = false;
            Btn_Modify.Visible = true;
            Btn_Delete.Visible = false;
            LinkButton objlnk = (LinkButton)sender;
            string srno = objlnk.CommandArgument;
            ViewState["id"] = srno;
            dt=CM.GetCashLimit( ViewState["id"].ToString(),Session["BRCD"].ToString());
            assign(dt);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void assign(DataTable dt1)
    {
        TxtPrd.Text = dt1.Rows[0]["SUBGLCODE"].ToString();
        TxtEffectDate.Text = dt1.Rows[0]["EFFECTIVEDATE"].ToString();
        TxtLimit.Text = dt1.Rows[0]["LIMIT"].ToString();
        string tds = BD.GetLoanGL(TxtPrd.Text, Session["BRCD"].ToString());
        if (tds != null)
        {
            string[] TD = tds.Split('_');
            if (TD.Length > 0)
            {

            }
            TxtProdName.Text = TD[0].ToString();
            TxtPrd.Text = TD[1].ToString();
          
        }
    }
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            Btn_Submit.Visible = true;
            Btn_Modify.Visible = false;
            Btn_Delete.Visible = false;
            clear();
            TxtPrd.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            result = CM.insert(TxtPrd.Text, TxtEffectDate.Text, TxtLimit.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["MID"].ToString());
            if (result > 0)
            {
                WebMsgBox.Show("Data Saved Successfully!!!", this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CashLimitMst _Add_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                clear();
                bindgrid();
                return;
               
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void Btn_ClearAll_Click(object sender, EventArgs e)
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
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    public void clear()
    {
        TxtPrd.Text = "";
        TxtProdName.Text = "";
        TxtEffectDate.Text = "";
        TxtLimit.Text = "";
    }
    protected void Btn_Modify_Click(object sender, EventArgs e)
    {
        try
        {
            result = CM.modify( ViewState["id"].ToString(),TxtPrd.Text, TxtEffectDate.Text, TxtLimit.Text, Session["BRCD"].ToString(), Session["MID"].ToString(), Session["MID"].ToString());
            if (result > 0)
            {
                WebMsgBox.Show("Data Modified Successfully!!!", this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CashLimitMst _Mod_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                clear();
                bindgrid();
                return;

            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void Btn_Delete_Click(object sender, EventArgs e)
    {
        try
        {
            result = CM.delete(ViewState["id"].ToString(), Session["BRCD"].ToString(), Session["MID"].ToString(), Session["MID"].ToString());
            if (result > 0)
            {
                WebMsgBox.Show("Data Deleted Successfully!!!", this.Page);
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CashLimitMst _Del_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                clear();
                bindgrid();
                return;

            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void bindgrid()
    {
        try
        {
            CM.bindall(grdCashLimit, Session["BRCD"].ToString());
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
   
}