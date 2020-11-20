using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmTally_Check : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsFDIntCalculation FD = new ClsFDIntCalculation();
    ClsAccountSTS AST = new ClsAccountSTS();
    ClsMISTransfer MT = new ClsMISTransfer();
    scustom customcs = new scustom();
    ClsTally_Check TC = new ClsTally_Check();
    string STR = "",FL="";
    int RESULT = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }

            autoglname.ContextKey = Session["BRCD"].ToString();
            if (Rdb_RepType.SelectedValue == "1")
            {
                TxtFPRD.Enabled = false;
                TxtFPRDName.Enabled = false;
            }
            else if (Rdb_RepType.SelectedValue == "2")
            {
                TxtFPRD.Enabled = true;
                TxtFPRDName.Enabled = true;
            }
        }
    }
    protected void TxtFBRCD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFBRCD.Text != "")
            {
                string bname = AST.GetBranchName(TxtFBRCD.Text);
                if (bname != null)
                {
                    TxtFBRCDName.Text = bname;
                    if (Rdb_RepType.SelectedValue == "1")
                        Btn_Submit.Focus();
                    else
                        TxtFPRD.Focus();


                }
                else
                {
                    WebMsgBox.Show("Enter valid Branch Code.....!", this.Page);
                    TxtFBRCD.Text = "";
                    TxtFBRCD.Focus();
                }
            }
            else
            {
                WebMsgBox.Show("Enter Branch Code!....", this.Page);
                TxtFBRCD.Text = "";
                TxtFBRCD.Focus();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFPRD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFPRD.Text == "")
            {
                TxtFPRDName.Text = "";
                goto ext;
            }
            int result = 0;
            string GlS1;
            int.TryParse(TxtFPRD.Text, out result);
            TxtFPRDName.Text = customcs.GetProductName(result.ToString(), Session["BRCD"].ToString());
            GlS1 = BD.GetAccTypeGL(TxtFPRD.Text, Session["BRCD"].ToString());
            if (GlS1 != null)
            {
                string[] GLS = GlS1.Split('_');
                ViewState["DRGL"] = GLS[1].ToString();

                int GL = 0;
                int.TryParse(ViewState["DRGL"].ToString(), out GL);

            }
            else
            {
                WebMsgBox.Show("Enter Valid Product code!....", this.Page);
                TxtFPRD.Text = "";
                TxtFPRD.Text = "";
                TxtFPRD.Focus();
            }

        ext: ;

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtFPRDName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string custno = TxtFPRDName.Text;
            string[] CT = custno.Split('_');
            if (CT.Length > 0)
            {
                TxtFPRDName.Text = CT[0].ToString();
                TxtFPRD.Text = CT[1].ToString();
                //TxtGLCD.Text = CT[2].ToString();
                string[] GLS = BD.GetAccTypeGL(TxtFPRD.Text, Session["BRCD"].ToString()).Split('_');
                ViewState["DRGL"] = GLS[1].ToString();

                int GL = 0;
                int.TryParse(ViewState["DRGL"].ToString(), out GL);

                if (TxtFPRDName.Text == "")
                {
                    WebMsgBox.Show("Please enter valid Product code", this.Page);
                    TxtFPRD.Text = "";
                    TxtFPRD.Focus();

                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }

    }
    
    protected void Grid_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Grid_Details.PageIndex = e.NewPageIndex;
        BindDetails();
    }
    public void BindSummary()
    {
        try
        {
            RESULT = TC.GetSummary(Grid_Summary, TxtFBRCD.Text, Session["MID"].ToString(), TxtFDate.Text, TxtTDate.Text, Session["EntryDate"].ToString());
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Updateinfo _" + TxtFDate.Text + "_" + TxtFDate.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            if (RESULT < 0)
            {
                WebMsgBox.Show("No Records Found....!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    public void BindDetails()
    {
        try
        {
            RESULT = TC.GetDetails(Grid_Details, TxtFBRCD.Text,TxtFPRD.Text, Session["MID"].ToString(), TxtFDate.Text, TxtTDate.Text, Session["EntryDate"].ToString());
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "AdminTally _" + TxtFDate.Text + "_" + TxtTDate.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            if (RESULT < 0)
            {
                WebMsgBox.Show("No Records Found....!", this.Page);
                return;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Rdb_RepType.SelectedValue == "1")
            {
                BindSummary();
                Grid_Summary.Visible = true;
                Grid_Details.Visible = false;
            }
            else if (Rdb_RepType.SelectedValue == "2")
            {
                BindDetails();
                Grid_Summary.Visible = false;
                Grid_Details.Visible = true;
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
    public void Clear()
    {
        TxtFDate.Text = "";
        TxtTDate.Text = "";
        TxtFBRCD.Text = "";
        TxtFBRCDName.Text = "";
        TxtFPRD.Text = "";
        TxtFPRDName.Text = "";
        TxtFDate.Focus();
    }
    protected void Btn_Exit_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect("FrmBlank.aspx", true);
    }
    protected void Rdb_RepType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Rdb_RepType.SelectedValue == "1")
            {
                TxtFPRD.Enabled = false;
                TxtFPRDName.Enabled = false;
            }
            else if (Rdb_RepType.SelectedValue == "2")
            {
                TxtFPRD.Enabled = true;
                TxtFPRDName.Enabled = true;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void Grid_Summary_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid_Summary.PageIndex = e.NewPageIndex;
        BindSummary();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (Rdb_RepType.SelectedValue == "2")
            {
                string fmonth, fyear;
                string tmonth, tyear;

                string[] fdate = TxtFDate.Text.Split('/');
                fmonth = fdate[1].ToString();
                fyear = fdate[2].ToString();

                string[] tdate = TxtTDate.Text.Split('/');
                tmonth = tdate[1].ToString();
                tyear = tdate[2].ToString();

                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "AdminTally_Rpt" + TxtFDate.Text + "_" + TxtTDate.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

                string redirectURL = "FrmRView.aspx?BranchID=" + TxtFBRCD.Text + "&ProdCode=" + TxtFPRD.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&rptname=RptTransSubGlMonthWise.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }

            else if (Rdb_RepType.SelectedValue == "1")
            {
                string fmonth, fyear;
                string tmonth, tyear;

                string[] fdate = TxtFDate.Text.Split('/');
                fmonth = fdate[1].ToString();
                fyear = fdate[2].ToString();

                string[] tdate = TxtTDate.Text.Split('/');
                tmonth = tdate[1].ToString();
                tyear = tdate[2].ToString();

                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "AdminTally_Rpt" + TxtFDate.Text + "_" + TxtTDate.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());


                string redirectURL = "FrmRView.aspx?BranchID=" + TxtFBRCD.Text + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&rptname=RptTransSummaryMonthWise.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}