using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class FrmLoanCaseFile : System.Web.UI.Page
{
    int result = 0;
    ClsBindDropdown BD = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsLoanCaseFile LF = new ClsLoanCaseFile();
    string sql = "";
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    string FL = "";
    static string accno = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            autoglname.ContextKey = Session["brcd"].ToString();
            autoaccname.ContextKey = Session["brcd"].ToString();
            ViewState["Flag"] = Request.QueryString["Flag"].ToString();

            if(ViewState["Flag"].ToString()=="SB")
            {
                BtnSubmit.Visible = true;
                BtnAuthorize.Visible = false;
            }
            if (ViewState["Flag"].ToString() == "AT")
            {
                BtnSubmit.Visible = false;
                BtnAuthorize.Visible = true;
                TxtCaseOfDate.ReadOnly = true;
                TxtReason.ReadOnly = true;
            }
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            result = LF.update(TxtAccno.Text, TxtBRCD.Text, TxtCustno.Text, TxtProdCde.Text, TxtCaseOfDate.Text,Session["MID"].ToString(),Session["MID"].ToString(), TxtReason.Text);
            if (result > 0)
            {
                WebMsgBox.Show("Data saved Successfully!!!", this.Page);
                FL = "Insert";//Dhanya 14/09/2017
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Loancasefile_Add_" + TxtAccno.Text + "_" + TxtProdCde.Text +"_"+ Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                clear();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void BtnAuthorize_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtCaseOfDate.Text != "" && TxtReason.Text != "")
            {
                string status = "";
                sql = "SELECT ACC_STATUS FROM AVS_ACC WHERE BRCD='" + TxtBRCD.Text + "' AND ACCNO='" + TxtAccno.Text + "' AND CUSTNO='" + TxtCustno.Text + "' AND SUBGLCODE='" + TxtProdCde.Text + "' AND STAGE<>1004";
                status = conn.sExecuteScalar(sql);
                if (status == "9")
                {
                    WebMsgBox.Show("Already Authorized!!", this.Page);
                }
                else
                {
                    result = LF.Autho(TxtAccno.Text, TxtBRCD.Text, TxtCustno.Text, TxtProdCde.Text, Session["MID"].ToString());
                    if (result > 1)
                    {
                        WebMsgBox.Show("Data Authorised Successfully!!!", this.Page);
                        FL = "Insert";//Dhanya 14/09/2017
                        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Loancasefile_Autho_" + TxtAccno.Text + "_" + TxtProdCde.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                        clear();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }

    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
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
                     TxtProdCde.Focus();

                 }
                 else
                 {
                     WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                     TxtBRCD.Text = "";
                     TxtBRCD.Focus();
                 }
             }
             else
             {
                 WebMsgBox.Show("Enter Branch Code!....", this.Page);
                 TxtBRCD.Text = "";
                 TxtBRCD.Focus();
             }
         }
         catch (Exception Ex)
         {
             ExceptionLogging.SendErrorToText(Ex);
         }
    }
    protected void TxtProdCde_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string tds = BD.GetLoanGL(TxtProdCde.Text, Session["BRCD"].ToString());
            if (tds != null)
            {
                string[] TD = tds.Split('_');
                if (TD.Length > 0)
                {

                }
                string GlS1;
                TxtProdName.Text = TD[0].ToString();
                TxtProdCde.Text = TD[1].ToString();
                GlS1 = BD.GetAccTypeGL(TxtProdCde.Text, Session["BRCD"].ToString());
                if (GlS1 != null)
                {
                    string[] GLS = GlS1.Split('_');
                    ViewState["DRGL"] = GLS[1].ToString();
                    autoaccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProdCde.Text + "_" + ViewState["DRGL"].ToString();
                    int GL = 0;
                    int.TryParse(ViewState["DRGL"].ToString(), out GL);
                }
                TxtAccno.Focus();

            }
            else
            {
                WebMsgBox.Show("Invalid Deposit Code......!", this.Page);
                TxtProdName.Text = "";
                TxtProdCde.Text = "";
                return;
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtAccno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Flag"].ToString() == "SB")
            {
                accno = TxtAccno.Text;
                string AT = "";
                AT = BD.Getstage1(TxtAccno.Text, Session["BRCD"].ToString(), TxtProdCde.Text);
                if (AT != null)
                {
                    if (AT != "1003")
                    {
                        WebMsgBox.Show("Sorry Customer not Authorise.........!!", this.Page);
                        TxtAccno.Text = "";
                        TxtAccname.Text = "";
                        TxtAccno.Focus();
                    }
                    else
                    {
                        DataTable DT = new DataTable();
                        DT = LF.GetCustName(TxtProdCde.Text, TxtAccno.Text, Session["BRCD"].ToString());
                        if (DT.Rows.Count > 0)
                        {
                            string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                            TxtAccname.Text = CustName[0].ToString();
                            TxtCustno.Text = CustName[1].ToString();
                            TxtAccSts.Text = DT.Rows[0]["ACC_STATUS"].ToString();
                        }
                    }
                }
                else
                {
                    WebMsgBox.Show("Enter valid account number...!!", this.Page);
                    TxtAccno.Text = "";
                    TxtAccno.Focus();
                }
            }
            if (ViewState["Flag"].ToString() == "AT")
            {
                string status = "";
                sql = "SELECT ACC_STATUS FROM AVS_ACC WHERE BRCD='" + TxtBRCD.Text + "' AND ACCNO='" + TxtAccno.Text + "' AND CUSTNO='" + TxtCustno.Text + "' AND SUBGLCODE='" + TxtProdCde.Text + "' AND STAGE<>1004";
                status = conn.sExecuteScalar(sql);
                if (status == "9")
                {
                    WebMsgBox.Show("Already Authorized!!", this.Page);
                }
                else
                {
                    DataTable DT = new DataTable();
                    DT = LF.GetCustName(TxtProdCde.Text, TxtAccno.Text, TxtBRCD.Text);
                    if (DT.Rows.Count > 0)
                    {
                        string[] CustName = DT.Rows[0]["CustName"].ToString().Split('_');
                        TxtAccname.Text = CustName[0].ToString();
                        TxtCustno.Text = CustName[1].ToString();
                        TxtAccSts.Text = DT.Rows[0]["ACC_STATUS"].ToString();
                    }

                    DataTable dt = new DataTable();
                    dt = LF.GetInfo(TxtAccno.Text, TxtProdCde.Text, TxtBRCD.Text);
                    if (dt.Rows.Count > 0)
                    {
                        TxtCaseOfDate.Text = dt.Rows[0]["CASE_OF_DATE"].ToString();
                        TxtReason.Text = dt.Rows[0]["REASON"].ToString();
                    }
                    else
                    {
                        WebMsgBox.Show("Invalid Account number for Loan case File!!", this.Page);
                        clear();
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtBRCDName_TextChanged(object sender, EventArgs e)
    {

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
                TxtProdCde.Text = prd[1].ToString();
            }
            string GlS1;
            GlS1 = BD.GetAccTypeGL(TxtProdCde.Text, Session["BRCD"].ToString());
            if (GlS1 != null)
            {
                string[] GLS = GlS1.Split('_');
                ViewState["DRGL"] = GLS[1].ToString();
                autoaccname.ContextKey = Session["BRCD"].ToString() + "_" + TxtProdCde.Text + "_" + ViewState["DRGL"].ToString();
                int GL = 0;
                int.TryParse(ViewState["DRGL"].ToString(), out GL);
            }

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void TxtAccname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string CUNAME = TxtAccname.Text;
            string[] custnob = CUNAME.Split('_');
            if (custnob.Length > 1)
            {
                TxtAccname.Text = custnob[0].ToString();
                TxtAccno.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                TxtCustno.Text = custnob[2].ToString();
                sql = "select acc_status from avs_Acc where brcd='" + TxtBRCD.Text + "' and accno='" + TxtAccno.Text + "' and custno='" + TxtCustno.Text + "'";
                TxtAccSts.Text = conn.sExecuteScalar(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void clear()
    {
        TxtBRCD.Text = "";
        TxtBRCDName.Text = "";
        TxtProdCde.Text = "";
        TxtProdName.Text = "";
        TxtAccno.Text = "";
        TxtAccname.Text = "";
        TxtCustno.Text = "";
        TxtAccSts.Text = "";
        TxtReason.Text = "";
        TxtCaseOfDate.Text = "";
    }
}