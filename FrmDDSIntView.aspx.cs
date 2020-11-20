using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmDDSIntView : System.Web.UI.Page
{
   
    int result = 0;
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsDDSIntView DV = new ClsDDSIntView();
    DbConnection conn = new DbConnection();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsLogMaintainance CLM = new ClsLogMaintainance();

    string STR = "";
    string sql = "";
    string FL = "", FBRCD = "", TBCRD = "", FPRD = "", TPRD = "", CT = "", PR = "";
    
   
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
                DV.BindCustType(ddlCustType);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void TxtFPRD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string tds = BD.GetLoanGL(TxtFPRD.Text, Session["BRCD"].ToString());
            if (tds != null)
            {
                string[] TD = tds.Split('_');
                if (TD.Length > 1)
                {

                }
                TxtFPRDName.Text = TD[0].ToString();
                TXtTPRDName.Text = TD[0].ToString();
                TxtTPRD.Text = TxtFPRD.Text;
                TxtTPRD.Focus();
            }
            else
            {
                WebMsgBox.Show("Invalid Deposit Code......!", this.Page);
                Clear();
                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void Clear()
    {

        //TxtFBRCD.Text = "";
        //TxtFBRCDName.Text = "";
        //TxtTBRCD.Text = "";
        //TxtTBRCDName.Text = "";
        TxtFPRD.Text = "";
        TxtTPRD.Text = "";
        TxtFPRDName.Text = "";
        TXtTPRDName.Text = "";
        //TxtFBRCD.Focus();
        ddlCustType.SelectedIndex = 0;
        ddlperiodtype.SelectedIndex = 0;
    }

    protected void TxtTPRD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string tds = BD.GetLoanGL(TxtTPRD.Text, Session["BRCD"].ToString());
            if (tds != null)
            {
                string[] TD = tds.Split('_');

                if (TD.Length > 1)
                {

                }
                TxtFPRDName.Text = TD[0].ToString();
                TXtTPRDName.Text = TD[0].ToString();
                TxtFPRD.Text = TxtTPRD.Text;
                ddlCustType.Focus();

            }
            else
            {
                WebMsgBox.Show("Invalid Deposit Code......!", this.Page);
                Clear();
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
        Clear();
    }
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
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
    //protected void TxtTBRCD_TextChanged1(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (TxtFBRCD.Text != "")
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

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            //TxtFBRCD.Text != null && TxtTBRCD.Text != null && 
            if (TxtFPRD.Text != null && TxtTPRD.Text != null)
            {
                FL = "BRPC";
                //FBRCD = TxtFBRCD.Text;
                //TBCRD = TxtTBRCD.Text;
                FPRD = TxtFPRD.Text;
                TPRD = TxtTPRD.Text;
            }

            if (ddlCustType.SelectedValue != "0")
            {
                FL = "CT";
                CT = ddlCustType.SelectedValue.ToString();
            }
            if (ddlperiodtype.SelectedValue != "0")
            {
                FL = "PR";
                PR = ddlperiodtype.SelectedValue.ToString();
            }
            //FBRCD, TBCRD,
            int Res =DV.GetFilter(grdVInterest, CT, PR, FPRD, TPRD, FL);
            FL = "Insert";//Dhanya Shetty
            string Res1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSIntMast_Rpt_" + TxtFPRD.Text + "_" + TxtTPRD.Text +"_"+ Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            if (Res <= 0)
            {
                WebMsgBox.Show("Sorry No Records Found", this.Page);
            }
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void Bindgrid()
    {
        try
        {
            //tim.GetGridData(grdInterest, Session["BRCD"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnreport_Click(object sender, EventArgs e)
    {
        try
        {
            //TxtFBRCD.Text != null && TxtTBRCD.Text != null &&
            if (TxtFPRD.Text != null && TxtTPRD.Text != null)
            {
                FL = "BRPC";
                //FBRCD = TxtFBRCD.Text;
                //TBCRD = TxtTBRCD.Text;
                FPRD = TxtFPRD.Text;
                TPRD = TxtTPRD.Text;
            }
            if (ddlCustType.SelectedValue != "0")
            {
                FL = "CT";
                CT = ddlCustType.SelectedValue.ToString();
            }
            if (ddlperiodtype.SelectedValue != "0")
            {
                FL = "PR";
                PR = ddlperiodtype.SelectedValue.ToString();
            }
           // "&Brcd=" + FBRCD + "&BRCD1=" + TBCRD + 
            FL = "Insert";//Dhanya Shetty
            string Res1 = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "DDSIntMast_Rpt_" + TxtFPRD.Text + "_" + TxtTPRD.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?flag=" + FL + "&ProdCode1=" + FPRD + "&PRODUCT2=" + TPRD + "&CT=" + CT + "&PR=" + PR + "&UserName=" + Session["UserName"].ToString() + "&rptname=RPTDDSIntMst.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}