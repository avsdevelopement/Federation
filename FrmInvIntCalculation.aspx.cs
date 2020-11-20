using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmInvIntCalculation : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsInvInt Int = new ClsInvInt();
    string FL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            //TxtFBRCD.Text = "1";
            //TxtTBRCD.Text = "1";
        }
    }
    public void Clear()
    {
        TxtAsOnDate.Text = "";
        //TxtFBRCD.Text = "";
        //TxtFBRCDName.Text = "";
        //TxtTBRCD.Text = "";
        //TxtTBRCDName.Text = "";
        TxtFPRD.Text = "";
        TxtFPRDName.Text = "";
        TxtFPRDName.Text = "";
        TxtTPRD.Text = "";
        TXtTPRDName.Text = "";
    }
    #region TEXT CHANGE
    //protected void TxtFBRCD_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (TxtFBRCD.Text != "")
    //        {
    //            string bname = AST.GetBranchName(TxtFBRCD.Text);
    //            if (bname != null)
    //            {
    //                TxtFBRCDName.Text = bname;
    //                TxtTBRCD.Focus();

    //            }
    //            else
    //            {
    //                WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
    //                TxtFBRCD.Text = "";
    //                TxtFBRCD.Focus();
    //            }
    //        }
    //        else
    //        {
    //            WebMsgBox.Show("Enter Branch Code!....", this.Page);
    //            TxtFBRCD.Text = "";
    //            TxtFBRCD.Focus();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
    //protected void TxtTBRCD_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (TxtTBRCD.Text != "")
    //        {
    //            string bname = AST.GetBranchName(TxtTBRCD.Text);
    //            if (bname != null)
    //            {
    //                TxtTBRCDName.Text = bname;
    //                TxtFPRD.Focus();

    //            }
    //            else
    //            {
    //                WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
    //                TxtTBRCD.Text = "";
    //                TxtTBRCD.Focus();
    //            }
    //        }
    //        else
    //        {
    //            WebMsgBox.Show("Enter Branch Code!....", this.Page);
    //            TxtTBRCD.Text = "";
    //            TxtTBRCD.Focus();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}
    protected void TxtFPRD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] TD = BD.GetInvGL1(TxtFPRD.Text, Session["BRCD"].ToString()).Split('_');
            if (TD.Length > 1)
            {
                TxtFPRDName.Text = TD[0].ToString();
                TxtTPRD.Focus();
            }
            else
            {
                TxtFPRD.Text = "";
                TxtFPRD.Focus();
            }
           
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtTPRD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] TD = BD.GetInvGL1(TxtTPRD.Text, Session["BRCD"].ToString()).Split('_');
            if (TD.Length > 1)
            {
                TXtTPRDName.Text = TD[0].ToString();
                TrailEntry.Focus();
            }
            else
            {
                TxtTPRD.Text = "";
                TxtTPRD.Focus();
            }
           
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    #endregion

    #region BUTTON CLICK
    protected void Btn_ClearAll_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void TrailEntry_Click(object sender, EventArgs e)
    {
        try
        {
            string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"] + "&UName=" + Session["UserName"] + "&EDAT=" + TxtAsOnDate.Text + "&FPRD="+TxtFPRD.Text+"&TPRD="+TxtTPRD.Text+"&rptname=RptInvInterest.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void ApplyEntry_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["BRCD"].ToString() != "1")
            {
                WebMsgBox.Show("Not Allowed for Branch!!!", this.Page);
            }
            else
            {
                DataTable dt=new DataTable ();
                dt = Int.GetAccNo(TxtFPRD.Text, TxtTPRD.Text, "2", Session["BRCD"].ToString(), Session["MID"].ToString(),TxtAsOnDate.Text);
                if (dt.Rows.Count > 0)
                {
                    WebMsgBox.Show("Update Received A/C for all Product Code!!!", this.Page);
                }
                else
                {
                    dt = Int.GetAccNo(TxtFPRD.Text, TxtTPRD.Text, "3", Session["BRCD"].ToString(), Session["MID"].ToString(), TxtAsOnDate.Text);
                    if (dt.Rows.Count > 0)
                    {
                        WebMsgBox.Show("Interest posted successfully..set no="+dt.Rows[0]["Setno"].ToString()+"",this.Page);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    #endregion

   
}