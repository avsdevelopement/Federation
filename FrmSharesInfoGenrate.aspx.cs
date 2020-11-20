using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class FrmSharesInfoGenrate : System.Web.UI.Page
{
    DbConnection conn = new DbConnection();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsCustWiseBalance Cust = new ClsCustWiseBalance();

    string FL = "";
    string Skip = "";
    string STR = "";
    int Result = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("FrmLogin.aspx");
                }

                TxtBRCD.Text = Session["BRCD"].ToString();
                TxtBrname.Text = AST.GetBranchName(TxtBRCD.Text);
                autoglname.ContextKey = Session["BRCD"].ToString();
                TxtBRCD.Focus();

                TxtFDate.Text = conn.sExecuteScalar("select '01/04/'+ convert(varchar(10),(year(dateadd(month, -3,'" + conn.ConvertDate(Session["EntryDate"].ToString()) + "'))))");
                TxtTDate.Text = Session["EntryDate"].ToString();
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
    }
    protected void TxtAccType_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string Glname = AST.GetGlname(TxtAccType.Text, TxtBRCD.Text);
            if (Glname != null)
            {
                TxtATName.Text = Glname;


            }
            else
            {
                WebMsgBox.Show("Please Enter Correct Product Code", this.Page);
            }
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
            BINDGRIDFINAL ();
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SharesInfo_TrailEntry _" + TxtFDate.Text + "_" + TxtTDate.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void TxtATName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Btn_Claer_Click(object sender, EventArgs e)
    {

    }
    protected void TrailEntry_Click(object sender, EventArgs e)
    {
        try
        {
            BINDGRID();
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SharesInfo_TrailEntry _" + TxtFDate.Text + "_" + TxtTDate.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BINDGRID()
    {
        try
        {
            Result = Cust.GetSharesInfo(GrdFDInt, TxtBRCD.Text, TxtAccType.Text, TxtFDate.Text, TxtTDate.Text, "T", Session["MID"].ToString());
            if (Result < 0)
            {
                WebMsgBox.Show("Shares Data Not Found...!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BINDGRIDFINAL()
    {
        try
        {
            Result = Cust.GetSharesInfo(GrdFDInt, TxtBRCD.Text, TxtAccType.Text, TxtFDate.Text, TxtTDate.Text, "F", Session["MID"].ToString());
            if (Result > 0)
            {
                WebMsgBox.Show("Shares Data Genrated Sucessfully...!", this.Page);
                return;
            }
            else
            {
                WebMsgBox.Show("Shares Data Not Found...!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}