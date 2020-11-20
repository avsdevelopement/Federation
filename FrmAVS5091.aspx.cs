using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAVS5091 : System.Web.UI.Page
{
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsBindDropdown BD = new ClsBindDropdown();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            BindBranchTDS();
        }
    }
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
    //                TxtDate.Focus();

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
    protected void BtReport_Click(object sender, EventArgs e)
    {
        try
        {
            string brcd = DDlBranchHO.SelectedValue.ToString();
            string redirectURL = "FrmRView.aspx?FDate=" + TxtDate.Text + "&TDate=" + txtTdate.Text + "&BRCD=" + brcd.ToString() + "&EDate=" + Session["ENTRYDATE"].ToString() + "&FBRCD=" + txtFbrcd.Text + "&TBRCD=" + txtTobrcd.Text + "&rptname=RptShareCloseAccDetails.rdlc";
           ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Exit_Click(object sender, EventArgs e)
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
    protected void DDlBranchHO_TextChanged(object sender, EventArgs e)
    {

    }
    public void BindBranchTDS()
    {
        try
        {
            BD.BindBRANCHNAME(DDlBranchHO, null);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}