using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmNPAClass : System.Web.UI.Page
{
   
    ClsNPA NP = new ClsNPA();
    DbConnection conn = new DbConnection();
    DataTable dt = new DataTable();
    int RESULT = 0;
     ClsLogMaintainance CLM = new ClsLogMaintainance();
     string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            BINDGRID();
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            
            RESULT = NP.INSERT(Convert.ToInt32(ViewState["ID"].ToString()), Convert.ToInt32(TxtGrcde.Text), TxtAsset.Text, TxtGfrom.Text, TxtGto.Text, TxtProvSec.Text, TxtProvUnsec.Text, TxtIntSec.Text, TxtIntUnsec.Text, Session["BRCD"].ToString(), Session["MID"].ToString());
            if (RESULT > 0)
            {
                WebMsgBox.Show("Data Added SuccessFully!!", this.Page);
                BINDGRID();
                FL = "Insert";//ankita 14/09/2017
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "NPAclass_Add_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnModify_Click(object sender, EventArgs e)
    {
        try
        {
            
            RESULT = NP.MODIFY(Convert.ToInt32(ViewState["ID"].ToString()), Convert.ToInt32(TxtGrcde.Text), TxtAsset.Text, TxtGfrom.Text, TxtGto.Text, TxtProvSec.Text, TxtProvUnsec.Text, TxtIntSec.Text, TxtIntUnsec.Text, Session["BRCD"].ToString(), Session["MID"].ToString());
            if (RESULT > 1)
            {
                WebMsgBox.Show("Data Updated SuccessFully!!", this.Page);
                BINDGRID();
                FL = "Insert";//ankita 14/09/2017
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "NPAclass_Mod_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            
            RESULT = NP.DELETE(Convert.ToInt32(ViewState["ID"].ToString()), Convert.ToInt32(TxtGrcde.Text), TxtAsset.Text, TxtGfrom.Text, TxtGto.Text, TxtProvSec.Text, TxtProvUnsec.Text, TxtIntSec.Text, TxtIntUnsec.Text, Session["BRCD"].ToString(), Session["MID"].ToString());
            if (RESULT > 1)
            {
                WebMsgBox.Show("Data Deleted SuccessFully!!", this.Page);
                BINDGRID();
                FL = "Insert";//ankita 14/09/2017
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "NPAclass_Del_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void grdNPA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

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
            BtnSubmit.Visible = true;
            BtnModify.Visible = false;
            BtnDelete.Visible = false;
            clear();
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
            BtnSubmit.Visible = false;
            BtnModify.Visible = true;
            BtnDelete.Visible = false;
            LinkButton objlink = (LinkButton)sender;
            string srno = objlink.CommandArgument;
            ViewState["ID"] = srno;
            dt = NP.GetDetails(srno);
            AssignValues(dt);
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
            BtnSubmit.Visible = false;
            BtnModify.Visible = false;
            BtnDelete.Visible = true;
            LinkButton objlink = (LinkButton)sender;
            string srno = objlink.CommandArgument;
            ViewState["ID"] = srno;
            dt = NP.GetDetails(srno);
            AssignValues(dt);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void AssignValues(DataTable dt)
    {
        try
        {
            TxtGrcde.Text = dt.Rows[0]["GRCODE"].ToString();
            TxtAsset.Text = dt.Rows[0]["ASSET"].ToString();
            TxtGfrom.Text = dt.Rows[0]["GFROM"].ToString();
            TxtGto.Text = dt.Rows[0]["GTO"].ToString();
            TxtProvSec.Text = dt.Rows[0]["PROVSECURED"].ToString();
            TxtProvUnsec.Text = dt.Rows[0]["PROVUNSECURED"].ToString();
            TxtIntSec.Text = dt.Rows[0]["INTSECURED"].ToString();
            TxtIntUnsec.Text = dt.Rows[0]["INTUNSECURED"].ToString();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BINDGRID()
    {
        try
        {
            NP.BindNPAGrid(grdNPA);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void clear()
    { 
    TxtGrcde.Text = "";
    TxtAsset.Text = "";
    TxtGfrom.Text = "";
    TxtGto.Text = "";
    TxtProvSec.Text ="";
    TxtProvUnsec.Text="";
    TxtIntSec.Text = "";
    TxtIntUnsec.Text = "";
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
}