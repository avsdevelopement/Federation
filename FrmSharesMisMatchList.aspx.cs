using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmSharesMisMatchList : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsShareMember SA = new ClsShareMember();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            BD.BindSHRActivtyALLType(ddlAppType);
            //added by ankita 07/10/2017 to make user frndly 
            TxtFDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
            TxtTDate.Text = Session["EntryDate"].ToString();
            TxtBrID.Text = Session["BRCD"].ToString();
            txtBrName.Text = SA.GetBranchName(TxtBrID.Text);
            TxtBrID.Focus();
        }
    }

    protected void TxtBrID_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtBrID.Text.Trim().ToString() == "0000")
            {
                txtBrName.Text = "All Branch";
                ddlAppType.Focus();
            }
            else
            {
                txtBrName.Text = SA.GetBranchName(TxtBrID.Text);
                if (txtBrName.Text.Trim().ToString() == "")
                {
                    TxtBrID.Text = "";
                    TxtBrID.Focus();
                }
                else
                {
                    ddlAppType.Focus();
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void ddlAppType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtFDate.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//ankita 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Share_Rpt" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text.Trim().ToString() + "&FDATE=" + TxtFDate.Text.Trim().ToString() + "&TDATE=" + TxtTDate.Text.Trim().ToString() + "&EDATE=" + Session["EntryDate"].ToString() + "&UName=" + Session["UserName"].ToString() + "&ApplType=" + ddlAppType.SelectedValue + "&rptname=RptShareMismatchList.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        try
        {
            ClearAll();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnExit_Click(object sender, EventArgs e)
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

    protected void ClearAll()
    {
        try
        {
            TxtBrID.Text = Session["BRCD"].ToString();
            TxtFDate.Text = "";
            TxtTDate.Text = "";
            TxtBrID.Focus();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

}