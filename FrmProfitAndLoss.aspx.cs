using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class FrmProfitAndLoss : System.Web.UI.Page
{
     public Func<int, bool> CurrentLangHide = (CurrentCode) => CurrentCode == 1 ? true : false;
    ClsProfitAndLoss PL = new ClsProfitAndLoss();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    ClsCommon cmn = new ClsCommon();
    DbConnection conn = new DbConnection();
    string FL = "";
    private  static string CurrentLang;
    private static int CurrentLangCode;
    Func<string> GetLangName;
    Func<int> GetLangCode;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("FrmLogin.aspx");
            }
            //added by ankita 07/10/2017 to make user frndly
            //DateTime date = Convert.ToDateTime(Session["EntryDate"].ToString());
            //date = new DateTime(date.Year, date.Month, 1);
            //txtFromDate.Text = date.ToString("dd/MM/yyyy");
            //txtToDate.Text = Session["EntryDate"].ToString();
            //TxtFDT.Text = Session["EntryDate"].ToString();
            TxtBrID.Text = Session["BRCD"].ToString();
            if (cmn.MultiBranch(Session["LOGINCODE"].ToString()) != "Y")
            {
                TxtBrID.Enabled = false;
                txtFromDate.Focus();
            }
            else
            {
                TxtBrID.Enabled = true;
                TxtBrID.Focus();
            }

            TxtFDT.Text = Convert.ToString(Session["EntryDate"]);
            txtFromDate.Text = Convert.ToString(Session["EntryDate"]);
            Func<string ,string> GetEndOfMonth=(CurrentMonth) => {
                return conn.sExecuteScalar("select Convert(Varchar(10),dateadd(month,1+datediff(month,0,'"+conn.ConvertDate(CurrentMonth)+"' ),-1),103)");
            };
            txtToDate.Text = GetEndOfMonth(TxtFDT.Text);
            txtCurrentLang.Visible = CurrentLangHide(0);
      
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
            FL = "Insert";//Dhanya 14/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Profitandlossrpt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }

    public void BindGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = PL.ProfitAndLoss(TxtBrID.Text, TxtFDT.Text, Session["UserName"].ToString());

            if (dt.Rows.Count > 0)
            {
                GrdProfitLoss.DataSource = dt;
                GrdProfitLoss.DataBind();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }

    protected void GrdProfitAndLoss_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdProfitLoss.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }

    protected void GrdProfitLoss_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }

    protected void Report_Click(object sender, EventArgs e)
    {
        GetLangCode = GetCurrentLangCode;
        if (RBSel.SelectedValue == "4")  //added by ashok misal for Admin expences
        {
            string redirectURL = "FrmRView.aspx?Brcd= " + TxtBrID.Text + " &FDate=" + TxtFDT.Text + "&TDate=" + TxtFDT.Text + "&rptname=RptAdminExp.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        else if (RBSel.SelectedValue == "5") 
        {
            FL = "Insert";//Dhanya 14/09/2017
            if (TxtFDT.Text == Session["EntryDate"].ToString())
            {
                WebMsgBox.Show("DayEnd Not Complete...", this.Page);
            }
            else
            {
                if (GetLangCode() == 1)
                {
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Profitandlossrpt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    string redirectURL = "FrmRView.aspx?Brcd= " + TxtBrID.Text + " &FDate=" + TxtFDT.Text + "&TDate=" + TxtFDT.Text + "&rptname=RptProfitAndLossSarjudas_Marathi.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

                }
                else
                {
                    string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Profitandlossrpt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                    string redirectURL = "FrmRView.aspx?Brcd= " + TxtBrID.Text + " &FDate=" + TxtFDT.Text + "&TDate=" + TxtFDT.Text + "&rptname=RptProfitAndLoss_Marathi.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

                } 
            }
        }
        else 
        {
            FL = "Insert";//Dhanya 14/09/2017
            if (TxtFDT.Text == Session["EntryDate"].ToString())
            {
                WebMsgBox.Show("DayEnd Not Complete...", this.Page);
            }
            else
            {
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Profitandlossrpt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
                string redirectURL = "FrmRView.aspx?Brcd= " + TxtBrID.Text + " &FDate=" + TxtFDT.Text + "&TDate=" + TxtFDT.Text + "&rptname=RptProfitAndLoss.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
    }

    protected void TextReport_Click(object sender, EventArgs e)
    {
        List<object> lst = new List<object>();
        lst.Add("Profit And Loss Report ");
        lst.Add(Session["UserName"].ToString());
        lst.Add(Session["BRCD"].ToString());
        lst.Add(TxtFDT.Text);
        ProfitAndLossTxt repObj = new ProfitAndLossTxt();
        repObj.RInit(lst);
        repObj.Start();
        FL = "Insert";//Dhanya 14/09/2017
        string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Profitandlossrpt_txt_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
    }

    protected void RBSel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RBSel.SelectedValue == "1")
        {
            txtCurrentLang.Visible = CurrentLangHide(0);                

            divDate.Visible = false;
            divOnDate.Visible = true;
        }
        else if (RBSel.SelectedValue == "5")
        {
            txtCurrentLang.Visible = CurrentLangHide(1);                

            divDate.Visible = false;
            divOnDate.Visible = true;
            GetLangName = GetCurrentLang;
            txtCurrentLang.Text = GetLangName();
        }
        else
        {
            if (RBSel.SelectedValue == "2")
            {
                txtCurrentLang.Visible = CurrentLangHide(0);    
                hdnFlag.Value = "1";
            }
            else if (RBSel.SelectedValue == "3")
            {
                txtCurrentLang.Visible = CurrentLangHide(0);    
                hdnFlag.Value = "2";
            }
            else if (RBSel.SelectedValue == "4")
            {
                txtCurrentLang.Visible = CurrentLangHide(0);    
                hdnFlag.Value = "4";
            }
            if (RBSel.SelectedValue == "6")
            {
               txtCurrentLang.Visible= CurrentLangHide(1);                
                hdnFlag.Value = "5";
                GetLangName = GetCurrentLang;
                txtCurrentLang.Text = GetLangName();
            }
            divDate.Visible = true;
            divOnDate.Visible = false;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        
    }

    protected void btnDown_Click(object sender, EventArgs e)
    {

    }

    protected void btnBal_Click(object sender, EventArgs e)
    {
        try
        {
            FL = "Insert";//Dhanya 14/09/2017
            GetLangCode = GetCurrentLangCode;

            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Profitandlossrpt_bal_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            if (hdnFlag.Value == "1")
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&FDate=" + txtFromDate.Text + "&TDate=" + txtToDate.Text + "&rptname=RptPNProfitAndLoss.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            if (hdnFlag.Value == "5")
            {
                if (GetLangCode() == 1)
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&FDate=" + txtFromDate.Text + "&TDate=" + txtToDate.Text + "&rptname=RptPNProfitAndLossSarjudas_Marathi.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }

                else
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&FDate=" + txtFromDate.Text + "&TDate=" + txtToDate.Text + "&rptname=RptPNProfitAndLoss_Marathi.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                
            }
            else if (hdnFlag.Value == "2")
            {
                string redirectURL = "FrmRView.aspx?BRCD=" + TxtBrID.Text + "&FDate=" + txtFromDate.Text + "&TDate=" + txtToDate.Text + "&rptname=RptIncomeExpReport.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
            else  if (RBSel.SelectedValue == "4")  //added by ashok misal for Admin expences
            {
                string redirectURL = "FrmRView.aspx?Brcd= " + TxtBrID.Text + " &FDate=" + txtFromDate.Text + "&TDate=" + txtToDate.Text + "&rptname=RptAdminExp.rdlc";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //Response.Redirect("FrmLogin.aspx", true);
        }
    }



    private string GetCurrentLang()
    {
        try
        {
            CurrentLang = conn.sExecuteScalar("exec SpGetLangName @BRCD='" + Convert.ToString(Session["BRCD"]) + "',@Flag='GetName'");

        }
        catch (Exception Ex)
        {
            CurrentLang = "No Lang";
            ExceptionLogging.SendErrorToText(Ex);
        }
        return CurrentLang;

    }

    private int GetCurrentLangCode()
    {
        try
        {
            CurrentLangCode = Convert.ToInt32(conn.sExecuteScalar("exec SpGetLangName @BRCD='" + Convert.ToString(Session["BRCD"]) + "',@Flag='GetCode'"));

        }
        catch (Exception Ex)
        {
            CurrentLang = "0";
            ExceptionLogging.SendErrorToText(Ex);
        }
        return CurrentLangCode;

    }


    
}