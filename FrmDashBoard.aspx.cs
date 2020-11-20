using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

public partial class FrmDashBoard : System.Web.UI.Page
{
    ClsLogin LG = new ClsLogin();
    DataTable DT = new DataTable();
    DataTable DT1 = new DataTable();
    ClsBlankQ CB = new ClsBlankQ();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsChangesView CV = new ClsChangesView();
    ClsQueryDetails QD = new ClsQueryDetails();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    DbConnection conn = new DbConnection();
    ClsBindBrDetails ASM = new ClsBindBrDetails();
    ClsStatementView STS = new ClsStatementView();
    ClsGetNPAList SRO = new ClsGetNPAList();

    string FL = "";
    int result = 0;
    double SumFooterValue = 0, SumFooterValue1 = 0, SumFooterValue2 = 0, SumFooterValue3 = 0, SumFooterValue4 = 0, SumFooterValue5 = 0, SumFooterValue6 = 0, SumFooterValue7 = 0,
        SumFooterValue8 = 0, SumFooterValue9 = 0, SumFooterValue10 = 0, SumFooterValue11 = 0, SumFooterValue12 = 0, SumFooterValue13 = 0, SumFooterValue14 = 0, SumFooterValue15 = 0;
    double TotalValue = 0, TotalValue1 = 0, TotalValue2 = 0, TotalValue3 = 0, TotalValue4 = 0, TotalValue5 = 0, TotalValue6 = 0, TotalValue7 = 0, TotalValue8 = 0
        , TotalValue9 = 0, TotalValue10 = 0, TotalValue11 = 0, TotalValue12 = 0, TotalValue13 = 0, TotalValue14 = 0, TotalValue15 = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
                Response.Redirect("FrmLogin.aspx");
            
            DataSet ds1 = CV.GetRecord();/*this obj is referring to some class in which GetRecord method is present which return the record from database. You can write your //own class and method.*/
            string s1;
            s1 = "<table><tr><td>";
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                s1 += "<a class='abc' href=# style=font-family: fantasy; font-size: large; font-weight:bold; font-style: normal; color=#D50000!important;>" + ds1.Tables[0].Rows[i][0].ToString() + " </a>| ";
            }
            s1 += "<a class='abc'> Thats all For Today!!</a>" + "</td></tr></table>";
            lt1.Text = s1.ToString();
            
            if (!IsPostBack)
            {
                DT = LG.DashboardDetails(Session["BRCD"].ToString(), LG.openDay(Session["BRCD"].ToString()), "DASH");
                if (DT != null)
                {
                    lblLoans.Text = Convert.ToDouble(DT.Rows[0]["LOANS"].ToString() == "" ? "0" : DT.Rows[0]["LOANS"]).ToString("0.00");
                    lblDeposite.Text = Convert.ToDouble(DT.Rows[0]["DEPOSITS"].ToString() == "" ? "0" : DT.Rows[0]["DEPOSITS"]).ToString("0.00");
                    lblShares.Text = Convert.ToDouble(DT.Rows[0]["SHARES"].ToString() == "" ? "0" : DT.Rows[0]["SHARES"]).ToString("0.00");
                    lblCASADep.Text = Convert.ToDouble(DT.Rows[0]["CSDEPOSITS"].ToString() == "" ? "0" : DT.Rows[0]["CSDEPOSITS"]).ToString("0.00");
                    lblCdRatio.Text = Convert.ToDouble(DT.Rows[0]["CDRATIO"].ToString() == "" ? "0" : DT.Rows[0]["CDRATIO"]).ToString("0.00");
                    lblInv.Text = Convert.ToDouble(DT.Rows[0]["Investment"].ToString() == "" ? "0" : DT.Rows[0]["Investment"]).ToString("0.00");
                    lblRef.Text = Convert.ToDouble(DT.Rows[0]["REF"].ToString() == "" ? "0" : DT.Rows[0]["REF"]).ToString("0.00");
                    lblDDS.Text = Convert.ToDouble(DT.Rows[0]["PIGMY"].ToString() == "" ? "0" : DT.Rows[0]["PIGMY"]).ToString("0.00");
                    lblABRRatio.Text = Convert.ToDouble(DT.Rows[0]["ABR"].ToString() == "" ? "0" : DT.Rows[0]["ABR"]).ToString("0.00");
                    lblALRRatio.Text = Convert.ToDouble(DT.Rows[0]["ALR"].ToString() == "" ? "0" : DT.Rows[0]["ALR"]).ToString("0.00");
                    LblDeptotal.Text=Convert.ToDouble(Convert.ToDouble(lblDeposite.Text) + Convert.ToDouble(lblCASADep.Text) + Convert.ToDouble(lblDDS.Text)).ToString();
                }
                else
                {
                    lblLoans.Text = "0.00";
                    lblDeposite.Text = "0.00";
                    lblShares.Text = "0.00";
                    lblCASADep.Text = "0.00";
                    lblCdRatio.Text = "0.00";
                    lblABRRatio.Text = "0.00";
                    lblALRRatio.Text = "0.00";
                    lblInv.Text = "0.00";
                    lblRef.Text = "0.00";
                    lblDDS.Text = "0.00";
                    LblDeptotal.Text = "0.00";
                }
                Div_AccDisplay.Visible = false;
                divDetails.Visible = false;
                if (Request.QueryString["Flag"] != null)
                {
                    ViewState["Flag"] = Request.QueryString["Flag"].ToString();
                    BindGrid();
                }
                TxtLoginId.Text = Session["LOGINCODE"].ToString();
                TxtName.Text = Session["UserName"].ToString();
                string PARAM = QD.GETPARAM();
                if (PARAM == "Y")
                    IssueNumber();

                MobileNumber();
                BD.BindModuleRQ(ddlModuleRQ);
                //TxtFDate.Value = Session["EntryDate"].ToString();
                //TxtTDate.Value = Session["EntryDate"].ToString();
                //BindPending();
                //BindSolved();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindGrid()
    {
        try
        {
            if (ViewState["Flag"].ToString() == "DP")
            {
                divDetails.Visible = true;
                DIV_TERM.Visible = false;
                DIV_MAT.Visible = false;
                divPigmy.Visible = false;
                DivSRO.Visible = false;
                LG.Getinfotable(grdDetails, Session["BRCD"].ToString(), LG.openDay(Session["BRCD"].ToString()), ViewState["Flag"].ToString());

                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Dash_view _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }

            else if (ViewState["Flag"].ToString() == "MAT")
            {
                divDetails.Visible = false;
                DIV_TERM.Visible = false;
                DIV_MAT.Visible = true;
                divPigmy.Visible = false;
                DivSRO.Visible = false;
                LG.Getinfotable(GrdMatDetails, Session["BRCD"].ToString(), LG.openDay(Session["BRCD"].ToString()), ViewState["Flag"].ToString());

                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Dash_Mat_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
            else if (ViewState["Flag"].ToString() == "PIGMY")
            {
                divDetails.Visible = false;
                DIV_TERM.Visible = false;
                DIV_MAT.Visible = false;
                divPigmy.Visible = true;
                DivSRO.Visible = false;
                LG.Getinfotable(GridPigmy, Session["BRCD"].ToString(), LG.openDay(Session["BRCD"].ToString()), ViewState["Flag"].ToString());

                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Dash_Mat_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
            else if (ViewState["Flag"].ToString() == "SRO")
            {
                divDetails.Visible = false;
                DIV_TERM.Visible = false;
                DIV_MAT.Visible = false;
                divPigmy.Visible = false;
                DivSRO.Visible = true;

                TxtFDate.Text = string.IsNullOrEmpty(Session["FDate"].ToString()) ? "0" : Session["FDate"].ToString();
                TxtTDate.Text = string.IsNullOrEmpty(Session["TDate"].ToString()) ? "0" : Session["TDate"].ToString();
                SRO.GetSROInfoDT(GridSRO, Session["BRCD"].ToString(), TxtFDate.Text.ToString(), TxtTDate.Text.ToString());

                CLM.LOGDETAILS("Insert", Session["BRCD"].ToString(), Session["MID"].ToString(), "Dash_Mat_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
            else if (ViewState["Flag"].ToString() == "ABLR") 
            {
                if (Session["BRCD"].ToString() == "1")
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + "0000" + "&AsOnDate=" + Session["EntryDate"].ToString() + "&rptname=Isp_AVS0029.rdlc" + "";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
                else
                {
                    string redirectURL = "FrmRView.aspx?BRCD=" + Session["BRCD"].ToString() + "&AsOnDate=" + Session["EntryDate"].ToString() + "&rptname=Isp_AVS0029.rdlc" + "";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
            else
            {
                DIV_TERM.Visible = false;
                divDetails.Visible = true;
                DIV_MAT.Visible = false;
                divPigmy.Visible = false;
                DivSRO.Visible = false;
                LG.Getinfotable(grdDetails, Session["BRCD"].ToString(), LG.openDay(Session["BRCD"].ToString()), ViewState["Flag"].ToString());

                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Dash_view _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdDetails.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grdTermDepo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdTermDepo.PageIndex = e.NewPageIndex;
            BindGrid();

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlModuleRQ.SelectedIndex > 0)
            {
                result = CB.InsertData(TxtLoginId.Text, TxtName.Text, TxtMobNo.Text, ddlModuleRQ.SelectedItem.ToString(), TxtQDesc.Text.Replace('\n', ' '), Session["BRCD"].ToString(), Session["BNKCDE"].ToString(), "", Session["MID"].ToString());
                if (result > 0)
                {
                    WebMsgBox.Show("Issue registered successfully..!!", this.Page);
                    ClearData();
                    return;
                }
            }
            else
            {
                WebMsgBox.Show("Please select Module RQ", this.Page);
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnReset_Click(object sender, EventArgs e)
    {
        try
        {
            ClearData();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void ClearData()
    {
        try
        {
            TxtLoginId.Text = "";
            TxtName.Text = "";
            TxIssueNo.Text = "";
            TxtMobNo.Text = "";
            ddlModuleRQ.SelectedIndex = 0;
            TxtQDesc.Text = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void MobileNumber()
    {
        try
        {
            string M = CB.GetMobile(TxtLoginId.Text);
            TxtMobNo.Text = M.ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void IssueNumber()
    {
        try
        {
            string I = CB.GetIssueNo();
            TxIssueNo.Text = I.ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void LBShow_Click(object sender, EventArgs e)
    {
        try
        {
            string Modal_Flag = "QUERYDETAILS";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#" + Modal_Flag + "').modal('show');");
            sb.Append(@"</script>");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);

            //string queryString = "FrmQueryDetails.aspx";
            //string newWin = "window.open('" + queryString + "');";
            ////ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
            ////ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + queryString + "','_blank')", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup_window", "window.open('" + queryString + "', 'popup_window', 'width=1250,height=600,left=150,top=150,resizable=no');", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public void BindPending()
    {
        try
        {
            int res = QD.GetPending(grdPending, Session["LOGINCODE"].ToString(), Session["BRCD"].ToString(), Session["BNKCDE"].ToString());
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    public void BindSolved()
    {
        try
        {
            int res1 = QD.GetSolevd(grdSolved, Session["BRCD"].ToString(), Session["BNKCDE"].ToString());
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void BtnShow_Click(object sender, EventArgs e)
    {
        try
        {
            Div_AccDisplay.Visible = true;
            DT1 = LG.DashboardDetailsMaturity(Session["BRCD"].ToString(), LG.openDay(Session["BRCD"].ToString()), "MATLBL");//Dhanya Shetty
            if (DT1 != null)
            {
                lblAccCount.Text = (DT1.Rows[0]["ACCNO"].ToString() == "" ? "0" : DT1.Rows[0]["ACCNO"]).ToString();
                lblAmount.Text = Convert.ToDouble(DT1.Rows[0]["MATURITYAMT"].ToString() == "" ? "0" : DT1.Rows[0]["MATURITYAMT"]).ToString("0.00");
                FL = "Insert";//Dhanya Shetty
                string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Dash_Mat_Acc_Show _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            }
            else
            {
                lblAccCount.Text = "0";
                lblAmount.Text = "0.00";
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void BtnReport_Click(object sender, EventArgs e) //Dhanya Shetty to display Maturity 
    {
        try
        {
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Dash_Mat_rpt _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            // HttpContext.Current.Response.Redirect("FrmDepositRep.aspx", false);
            DataTable dtnew = new DataTable();
            dtnew = LG.Getinfotabledt(Session["BRCD"].ToString());
            string redirectURL = "FrmRView.aspx?FD=01/01/1990&TD=" + Session["EntryDate"].ToString() + "&BRCD=" + Session["BRCD"].ToString() + "&rptname=RptDepositRep.rdlc&SUBGLFRM=" + dtnew.Rows[0]["SUBGLCODE"].ToString() + "&SUBGLTO=" + dtnew.Rows[dtnew.Rows.Count - 1]["SUBGLCODE"].ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void RptMatuLoan_Click(object sender, EventArgs e) //Dhanya Shetty to display Maturity Loan Account
    {
        LinkButton objlink = (LinkButton)sender;
        string ID = objlink.CommandArgument;
        string name = objlink.CommandName;
        try
        {
            int R1 = 0;
            FL = "Insert";//ankita  15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CutBk_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            string bankcd = ASM.GetBankcd(Session["BRCD"].ToString());

            string fbrcd = conn.sExecuteScalar("select min(brcd) from BANKNAME where brcd<>0");
            string tbrcd = conn.sExecuteScalar("select max(brcd) from BANKNAME where brcd<>0");

            if (Session["BRCD"].ToString() == "1")
            {
                R1 = QD.LoanBalanceDash(Grd_LoanAccount, fbrcd, tbrcd, ID.ToString(), ID.ToString(), Session["EntryDate"].ToString(), "4");
            }
            else
            {
                R1 = QD.LoanBalanceDash(Grd_LoanAccount, Session["BRCD"].ToString(), Session["BRCD"].ToString(), ID.ToString(), ID.ToString(), Session["EntryDate"].ToString(), "4");
            }
            Div_LoanAccount.Visible = true;

            //(******** Below code commented twice by Abhishek as per Requirement given by Ambika mam on 29-06-2018 to show grid for Balance Report Loan

            //if (Session["BRCD"].ToString() == "1")
            //{
            //    if (bankcd == "1008")
            //    {
            //        R1 = QD.CreateCutBook_Pal(Grd_LoanAccount, Session["MID"].ToString(), "3", ID.ToString(), fbrcd, tbrcd, Session["EntryDate"].ToString());
            //    }
            //    else
            //    {
            //        R1 = QD.CreateCutBook(Grd_LoanAccount, Session["MID"].ToString(), "3", ID.ToString(), fbrcd, tbrcd, Session["EntryDate"].ToString());
            //    }
            //}
            //else
            //{
            //    if (bankcd == "1008")
            //    {
            //        R1 = QD.CreateCutBook_Pal(Grd_LoanAccount, Session["MID"].ToString(), "3", ID.ToString(), Session["BRCD"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());
            //    }
            //    else
            //    {
            //        R1 = QD.CreateCutBook(Grd_LoanAccount, Session["MID"].ToString(), "3", ID.ToString(), Session["BRCD"].ToString(), Session["BRCD"].ToString(), Session["EntryDate"].ToString());
            //    }
            //}
            //Div_LoanAccount.Visible = true;

            //(******** Below code commented by Abhishek as per Requirement given by Ambika mam on 18-06-2018 to show grid only 

            //string SL = "", redirectURL;
            //SL = "ALL";
            //FL = "Insert";//Dhanya Shetty
            //string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Dash_Mat_loan_Rpt _" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            //if (ViewState["Flag"].ToString() == "LN")
            //{
            //    if (Session["BRCD"].ToString() == "1")
            //    {
            //        string fbrcd = conn.sExecuteScalar("select min(brcd) from BANKNAME where brcd<>0");
            //        string tbrcd = conn.sExecuteScalar("select max(brcd) from BANKNAME where brcd<>0");
            //        redirectURL = "FrmRView.aspx?Date=" + Session["EntryDate"].ToString() + "&UserName=" + Session["UserName"].ToString() + "&Fbrcd=" + fbrcd + "&Tbrcd=" + tbrcd + "&EntryDate=" + Session["EntryDate"].ToString() + "&FSUBGL=" + ID + "&TSUBGL=" + ID + "&SL=ALL&rptname=RptLoanOverdue_New.rdlc";
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            //    }
            //    else
            //    {
            //        redirectURL = "FrmRView.aspx?Date=" + Session["EntryDate"].ToString() + "&UserName=" + Session["UserName"].ToString() + "&Fbrcd=" + Session["BRCD"].ToString() + "&Tbrcd=" + Session["BRCD"].ToString() + "&EntryDate=" + Session["EntryDate"].ToString() + "&FSUBGL=" + ID + "&TSUBGL=" + ID + "&SL=ALL&rptname=RptLoanOverdue_New.rdlc";
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
            //    }
            //}
            //else
            //{

            //    redirectURL = "FrmRView.aspx?Date=" + Session["EntryDate"].ToString() + "&UserName=" + Session["UserName"].ToString() + "&brcd=" + Session["BRCD"].ToString() + "&EntryDate=" + Session["EntryDate"].ToString() + "&SUBGL=" + ID + "&GLNAME=" + name + "&SL=" + SL + "&rptname=RptDashLoanOverdue.rdlc";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

            //}
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e) //Dhanya Shetty to display Maturity Loan Account
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ViewState["Flag"].ToString() == "LNM" || ViewState["Flag"].ToString() == "LN")

                grdDetails.Columns[5].Visible = true;
            else
                grdDetails.Columns[5].Visible = false;

            string lblAccNo = string.IsNullOrEmpty(((Label)e.Row.FindControl("lblAccNo")).Text) ? "0" : ((Label)e.Row.FindControl("lblAccNo")).Text;
            TotalValue = Convert.ToDouble(lblAccNo);
            SumFooterValue += TotalValue;
            string sanc1 = string.IsNullOrEmpty(((Label)e.Row.FindControl("lblAmount")).Text) ? "0" : ((Label)e.Row.FindControl("lblAmount")).Text;
            TotalValue1 = Convert.ToDouble(sanc1);
            SumFooterValue1 += TotalValue1;

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lblAccNo_tot");
            lbl.Text = SumFooterValue.ToString() + "";
            Label lbl1 = (Label)e.Row.FindControl("lblAmount_tot");
            lbl1.Text = SumFooterValue1.ToString() + "";

            if (lbl.Text == "0")
                lbl.Text = "";
            if (lbl1.Text == "0")
                lbl1.Text = "";
        }
    }

    protected void grdTermDepo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string lblAccNo = string.IsNullOrEmpty(((Label)e.Row.FindControl("lblAccNo")).Text) ? "0" : ((Label)e.Row.FindControl("lblAccNo")).Text;
                TotalValue = Convert.ToDouble(lblAccNo);
                SumFooterValue += TotalValue;
                string sanc1 = string.IsNullOrEmpty(((Label)e.Row.FindControl("lblAmount")).Text) ? "0" : ((Label)e.Row.FindControl("lblAmount")).Text;
                TotalValue1 = Convert.ToDouble(sanc1);
                SumFooterValue1 += TotalValue1;

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl = (Label)e.Row.FindControl("lblAccNo_tot");
                lbl.Text = SumFooterValue.ToString() + "";
                Label lbl1 = (Label)e.Row.FindControl("lblAmount_tot");
                lbl1.Text = SumFooterValue1.ToString() + "";

                if (lbl.Text == "0")
                    lbl.Text = "";
                if (lbl1.Text == "0")
                    lbl1.Text = "";
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void GrdMatDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string lblAccNo = string.IsNullOrEmpty(((Label)e.Row.FindControl("lblCbal")).Text) ? "0" : ((Label)e.Row.FindControl("lblCbal")).Text;
                TotalValue = Convert.ToDouble(lblAccNo);
                SumFooterValue += TotalValue;

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl = (Label)e.Row.FindControl("lblCbal_tot");
                lbl.Text = SumFooterValue.ToString() + "";

                if (lbl.Text == "0")
                    lbl.Text = "";
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    protected void Lnk_StatementView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            string Accno = ARR[0].ToString();
            string Subgl = ARR[1].ToString();
            string Brcd = ARR[2].ToString();
            string FDate = QD.GetSanssionDate(Brcd, Subgl.ToString(), Accno.ToString());

            //string redirectURL = "FrmReportViewer.aspx?BC=" + Brcd.ToString() + "&GC=3&PC=" + Subgl.ToString() + "&AN=" + Accno.ToString() + "&RN=0&FD=" + FDate.ToString() + "&TD=" + Session["EntryDate"].ToString() + "&LAMT=0&InstAmt=0&IRate=0&rptname=RptStatementView.rdlc";
            // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);

            //Commented and added by amol on 26/06/2018 (as per new requirement)
            string redirectURL = "FrmRView.aspx?FDate=" + FDate.ToString() + "&TDate=" + Session["EntryDate"].ToString() + "&ProdCode=" + Subgl.ToString() + "&AccNo=" + Accno.ToString() + "&BRCD=" + Brcd.ToString() + "&rptname=RptLoanStatementDetails.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Grd_LoanAccount_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            string Inst = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L1Installment")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L1Installment")).Text;
            TotalValue = Convert.ToDouble(Inst);
            SumFooterValue += TotalValue;
            string Princ = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L1Principle")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L1Principle")).Text;
            TotalValue1 = Convert.ToDouble(Princ);
            SumFooterValue1 += TotalValue1;

            string ODbal = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L1OverDueBal")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L1OverDueBal")).Text;
            TotalValue2 = Convert.ToDouble(ODbal);
            SumFooterValue2 += TotalValue2;
            string Inttr = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L1Interest")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L1Interest")).Text;
            TotalValue3 = Convert.ToDouble(Inttr);
            SumFooterValue3 += TotalValue3;
            string PInst = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L1PInterest")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L1PInterest")).Text;
            TotalValue4 = Convert.ToDouble(PInst);
            SumFooterValue4 += TotalValue4;
            string InstRec = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L1InterestRec")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L1InterestRec")).Text;
            TotalValue5 = Convert.ToDouble(InstRec);
            SumFooterValue5 += TotalValue5;
            string Notc = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L1NoticeChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L1NoticeChrg")).Text;
            TotalValue6 = Convert.ToDouble(Notc);
            SumFooterValue6 += TotalValue6;
            string Serchrg = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L1ServiceChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L1ServiceChrg")).Text;
            TotalValue7 = Convert.ToDouble(Serchrg);
            SumFooterValue7 += TotalValue7;
            string Court = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L1CourtChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L1CourtChrg")).Text;
            TotalValue8 = Convert.ToDouble(Court);
            SumFooterValue8 += TotalValue8;
            string Sur = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L1SurChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L1SurChrg")).Text;
            TotalValue9 = Convert.ToDouble(Sur);
            SumFooterValue9 += TotalValue9;
            string OtrChrg = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L1OtherChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L1OtherChrg")).Text;
            TotalValue10 = Convert.ToDouble(OtrChrg);
            SumFooterValue10 += TotalValue10;
            string BnkCrg = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L1BankChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L1BankChrg")).Text;
            TotalValue11 = Convert.ToDouble(BnkCrg);
            SumFooterValue11 += TotalValue11;
            string InstCrgg = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L1InsChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L1InsChrg")).Text;
            TotalValue12 = Convert.ToDouble(InstCrgg);
            SumFooterValue12 += TotalValue12;
            string Noofinst = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L1NoOfInst")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L1NoOfInst")).Text;
            TotalValue13 = Convert.ToDouble(Noofinst);
            SumFooterValue13 += TotalValue13;
            string Totttt = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L1TotalDueAmt")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L1TotalDueAmt")).Text;
            TotalValue14 = Convert.ToDouble(Totttt);
            SumFooterValue14 += TotalValue14;
            string SanAMount = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L1SanAmount")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L1SanAmount")).Text;
            TotalValue15 = Convert.ToDouble(SanAMount);
            SumFooterValue15 += TotalValue15;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lblInstTt");
            lbl.Text = SumFooterValue.ToString() + "";
            Label lbl1 = (Label)e.Row.FindControl("lblPrinciTt");
            lbl1.Text = SumFooterValue1.ToString() + "";
            Label lbl2 = (Label)e.Row.FindControl("lblOverdueTt");
            lbl2.Text = SumFooterValue2.ToString() + "";

            Label lbl3 = (Label)e.Row.FindControl("lblInttTt");
            lbl3.Text = SumFooterValue3.ToString() + "";
            Label lbl4 = (Label)e.Row.FindControl("lblPPInttTt");
            lbl4.Text = SumFooterValue4.ToString() + "";
            Label lbl5 = (Label)e.Row.FindControl("lblIntRec");
            lbl5.Text = SumFooterValue5.ToString() + "";

            Label lbl6 = (Label)e.Row.FindControl("lblNtcChrg");
            lbl6.Text = SumFooterValue6.ToString() + "";
            Label lbl7 = (Label)e.Row.FindControl("lblServiceChrg");
            lbl7.Text = SumFooterValue7.ToString() + "";
            Label lbl8 = (Label)e.Row.FindControl("lblCourtChrg");
            lbl8.Text = SumFooterValue8.ToString() + "";
            Label lbl9 = (Label)e.Row.FindControl("lblSurTChrg");
            lbl9.Text = SumFooterValue9.ToString() + "";
            Label lbl10 = (Label)e.Row.FindControl("lblOtherTChrg");
            lbl10.Text = SumFooterValue10.ToString() + "";
            Label lbl11 = (Label)e.Row.FindControl("lblBankTTChrg");
            lbl11.Text = SumFooterValue11.ToString() + "";
            Label lbl12 = (Label)e.Row.FindControl("lblInsTChrg");
            lbl12.Text = SumFooterValue12.ToString() + "";
            Label lbl13 = (Label)e.Row.FindControl("lblNoInsTChrg");
            lbl13.Text = SumFooterValue13.ToString() + "";
            Label lbl14 = (Label)e.Row.FindControl("lblTtotaldue");
            lbl14.Text = SumFooterValue14.ToString() + "";
            Label lbl15 = (Label)e.Row.FindControl("LblSanction");
            lbl15.Text = SumFooterValue15.ToString() + "";

            //if (lbl.Text == "0")
            //    lbl.Text = "";
            //if (lbl1.Text == "0")
            //    lbl1.Text = "";
            //if (lbl2.Text == "0")
            //    lbl2.Text = "";
            //if (lbl3.Text == "0")
            //    lbl3.Text = "";
            //if (lbl4.Text == "0")
            //    lbl4.Text = "";
            //if (lbl5.Text == "0")
            //    lbl5.Text = "";
            //if (lbl6.Text == "0")
            //    lbl6.Text = "";
            //if (lbl7.Text == "0")
            //    lbl7.Text = "";
            //if (lbl8.Text == "0")
            //    lbl8.Text = "";
            //if (lbl9.Text == "0")
            //    lbl9.Text = "";
            //if (lbl10.Text == "0")
            //    lbl10.Text = "";
            //    if (lbl11.Text == "0")
            //        lbl11.Text = "";
            //if (lbl12.Text == "0")
            //    lbl12.Text = "";
            //if (lbl13.Text == "0")
            //    lbl13.Text = "";
            //if (lbl14.Text == "0")
            //    lbl14.Text = "";
            //if (lbl15.Text == "0")
            //    lbl15.Text = "";

        }
    }
    protected void GridPigmy_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridPigmy.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void GridPigmy_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            string lblAccNo = string.IsNullOrEmpty(((Label)e.Row.FindControl("lbpAccNo")).Text) ? "0" : ((Label)e.Row.FindControl("lbpAccNo")).Text;
            TotalValue = Convert.ToDouble(lblAccNo);
            SumFooterValue += TotalValue;
            string sanc1 = string.IsNullOrEmpty(((Label)e.Row.FindControl("lbpAmount")).Text) ? "0" : ((Label)e.Row.FindControl("lbpAmount")).Text;
            TotalValue1 = Convert.ToDouble(sanc1);
            SumFooterValue1 += TotalValue1;

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lbpAccNo_tot");
            lbl.Text = SumFooterValue.ToString() + "";
            Label lbl1 = (Label)e.Row.FindControl("lbpAmount_tot");
            lbl1.Text = SumFooterValue1.ToString() + "";

            if (lbl.Text == "0")
                lbl.Text = "";
            if (lbl1.Text == "0")
                lbl1.Text = "";
        }
    }
    protected void GridSRO_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridSRO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Princ = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L3Principle")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L3Principle")).Text;
            TotalValue1 = Convert.ToDouble(Princ);
            SumFooterValue1 += TotalValue1;
            string PriCrDr = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L3PCrDr")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L3PCrDr")).Text;
            TotalValue2 = Convert.ToDouble(PriCrDr);
            SumFooterValue2 += TotalValue2;
            string Inttr = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L3Interest")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L3Interest")).Text;
            TotalValue3 = Convert.ToDouble(Inttr);
            SumFooterValue3 += TotalValue3;
            string PInst = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L3PInterest")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L3PInterest")).Text;
            TotalValue4 = Convert.ToDouble(PInst);
            SumFooterValue4 += TotalValue4;
            string InstRec = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L3InterestRec")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L3InterestRec")).Text;
            TotalValue5 = Convert.ToDouble(InstRec);
            SumFooterValue5 += TotalValue5;
            string Notc = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L3NoticeChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L3NoticeChrg")).Text;
            TotalValue6 = Convert.ToDouble(Notc);
            SumFooterValue6 += TotalValue6;
            string Serchrg = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L3ServiceChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L3ServiceChrg")).Text;
            TotalValue7 = Convert.ToDouble(Serchrg);
            SumFooterValue7 += TotalValue7;
            string Court = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L3CourtChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L3CourtChrg")).Text;
            TotalValue8 = Convert.ToDouble(Court);
            SumFooterValue8 += TotalValue8;
            string Sur = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L3SurChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L3SurChrg")).Text;
            TotalValue9 = Convert.ToDouble(Sur);
            SumFooterValue9 += TotalValue9;
            string OtrChrg = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L3OtherChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L3OtherChrg")).Text;
            TotalValue10 = Convert.ToDouble(OtrChrg);
            SumFooterValue10 += TotalValue10;
            string BnkCrg = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L3BankChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L3BankChrg")).Text;
            TotalValue11 = Convert.ToDouble(BnkCrg);
            SumFooterValue11 += TotalValue11;
            string InstCrgg = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L3InsChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L3InsChrg")).Text;
            TotalValue12 = Convert.ToDouble(InstCrgg);
            SumFooterValue12 += TotalValue12;
            string Totttt = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L3TotalDueAmt")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L3TotalDueAmt")).Text;
            TotalValue14 = Convert.ToDouble(Totttt);
            SumFooterValue14 += TotalValue14;
            string Totsan = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L3SanAmount")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L3SanAmount")).Text;
            TotalValue15 = Convert.ToDouble(Totsan);
            SumFooterValue15 += TotalValue15;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl1 = (Label)e.Row.FindControl("lbl_L3PrinciTt");
            lbl1.Text = SumFooterValue1.ToString() + "";
            Label lbl2 = (Label)e.Row.FindControl("lbl_L3PCrDramt");
            lbl2.Text = SumFooterValue2.ToString() + "";
            Label lbl3 = (Label)e.Row.FindControl("lbl_L3InttTt");
            lbl3.Text = SumFooterValue3.ToString() + "";
            Label lbl4 = (Label)e.Row.FindControl("lbl_L3PPInttTt");
            lbl4.Text = SumFooterValue4.ToString() + "";
            Label lbl5 = (Label)e.Row.FindControl("lbl_L3IntRec");
            lbl5.Text = SumFooterValue5.ToString() + "";
            Label lbl6 = (Label)e.Row.FindControl("lbl_L3NtcChrg");
            lbl6.Text = SumFooterValue6.ToString() + "";
            Label lbl7 = (Label)e.Row.FindControl("lbl_L3ServiceChrg");
            lbl7.Text = SumFooterValue7.ToString() + "";
            Label lbl8 = (Label)e.Row.FindControl("lbl_L3CourtChrg");
            lbl8.Text = SumFooterValue8.ToString() + "";
            Label lbl9 = (Label)e.Row.FindControl("lbl_L3SurTChrg");
            lbl9.Text = SumFooterValue9.ToString() + "";
            Label lbl10 = (Label)e.Row.FindControl("lbl_L3OtherTChrg");
            lbl10.Text = SumFooterValue10.ToString() + "";
            Label lbl11 = (Label)e.Row.FindControl("lbl_L3BankTTChrg");
            lbl11.Text = SumFooterValue11.ToString() + "";
            Label lbl12 = (Label)e.Row.FindControl("lbl_L3InsTChrg");
            lbl12.Text = SumFooterValue12.ToString() + "";
            Label lbl14 = (Label)e.Row.FindControl("lbl_L3TtotalRec");
            lbl14.Text = SumFooterValue14.ToString() + "";
            Label lbl15 = (Label)e.Row.FindControl("Lbl_L3Sanction");
            lbl15.Text = SumFooterValue15.ToString() + "";

        }
    }
    protected void Lnk_SROView_Click(object sender, EventArgs e)
    {
        LinkButton objlink = (LinkButton)sender;
        string ID = objlink.CommandArgument;
        string name = objlink.CommandName;
        try
        {
            int R1 = 0;
            FL = "Insert";//ankita  15/09/2017
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "CutBk_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            string bankcd = ASM.GetBankcd(Session["BRCD"].ToString());

            string fbrcd = conn.sExecuteScalar("select min(brcd) from BANKNAME where brcd<>0");
            string tbrcd = conn.sExecuteScalar("select max(brcd) from BANKNAME where brcd<>0");

            R1 = SRO.GetSROInfoDT_BR(Grd_SROAccount, Session["BRCD"].ToString(), ID.ToString(), TxtFDate.Text.ToString(), TxtTDate.Text.ToString());
            Div_SROAccount.Visible = true;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtFDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Session["FDate"] = TxtFDate.Text.ToString();
            TxtTDate.Focus(); 
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void TxtTDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Session["TDate"] = TxtTDate.Text.ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void Grd_SROAccount_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Princ = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L2Principle")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L2Principle")).Text;
            TotalValue1 = Convert.ToDouble(Princ);
            SumFooterValue1 += TotalValue1;
            string PriCrDr = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L2PCrDr")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L2PCrDr")).Text;
            TotalValue2 = Convert.ToDouble(PriCrDr);
            SumFooterValue2 += TotalValue2;
            string Inttr = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L2Interest")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L2Interest")).Text;
            TotalValue3 = Convert.ToDouble(Inttr);
            SumFooterValue3 += TotalValue3;
            string PInst = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L2PInterest")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L2PInterest")).Text;
            TotalValue4 = Convert.ToDouble(PInst);
            SumFooterValue4 += TotalValue4;
            string InstRec = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L2InterestRec")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L2InterestRec")).Text;
            TotalValue5 = Convert.ToDouble(InstRec);
            SumFooterValue5 += TotalValue5;
            string Notc = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L2NoticeChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L2NoticeChrg")).Text;
            TotalValue6 = Convert.ToDouble(Notc);
            SumFooterValue6 += TotalValue6;
            string Serchrg = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L2ServiceChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L2ServiceChrg")).Text;
            TotalValue7 = Convert.ToDouble(Serchrg);
            SumFooterValue7 += TotalValue7;
            string Court = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L2CourtChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L2CourtChrg")).Text;
            TotalValue8 = Convert.ToDouble(Court);
            SumFooterValue8 += TotalValue8;
            string Sur = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L2SurChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L2SurChrg")).Text;
            TotalValue9 = Convert.ToDouble(Sur);
            SumFooterValue9 += TotalValue9;
            string OtrChrg = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L2OtherChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L2OtherChrg")).Text;
            TotalValue10 = Convert.ToDouble(OtrChrg);
            SumFooterValue10 += TotalValue10;
            string BnkCrg = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L2BankChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L2BankChrg")).Text;
            TotalValue11 = Convert.ToDouble(BnkCrg);
            SumFooterValue11 += TotalValue11;
            string InstCrgg = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L2InsChrg")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L2InsChrg")).Text;
            TotalValue12 = Convert.ToDouble(InstCrgg);
            SumFooterValue12 += TotalValue12;
            string Totttt = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L2TotalDueAmt")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L2TotalDueAmt")).Text;
            TotalValue14 = Convert.ToDouble(Totttt);
            SumFooterValue14 += TotalValue14;
            string Totsan = string.IsNullOrEmpty(((Label)e.Row.FindControl("Lbl_L2SanAmount")).Text) ? "0" : ((Label)e.Row.FindControl("Lbl_L2SanAmount")).Text;
            TotalValue15 = Convert.ToDouble(Totsan);
            SumFooterValue15 += TotalValue15;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl1 = (Label)e.Row.FindControl("lbl_L2PrinciTt");
            lbl1.Text = SumFooterValue1.ToString() + "";
            Label lbl2 = (Label)e.Row.FindControl("lbl_L2PCrDramt");
            lbl2.Text = SumFooterValue2.ToString() + "";
            Label lbl3 = (Label)e.Row.FindControl("lbl_L2InttTt");
            lbl3.Text = SumFooterValue3.ToString() + "";
            Label lbl4 = (Label)e.Row.FindControl("lbl_L2PPInttTt");
            lbl4.Text = SumFooterValue4.ToString() + "";
            Label lbl5 = (Label)e.Row.FindControl("lbl_L2IntRec");
            lbl5.Text = SumFooterValue5.ToString() + "";
            Label lbl6 = (Label)e.Row.FindControl("lbl_L2NtcChrg");
            lbl6.Text = SumFooterValue6.ToString() + "";
            Label lbl7 = (Label)e.Row.FindControl("lbl_L2ServiceChrg");
            lbl7.Text = SumFooterValue7.ToString() + "";
            Label lbl8 = (Label)e.Row.FindControl("lbl_L2CourtChrg");
            lbl8.Text = SumFooterValue8.ToString() + "";
            Label lbl9 = (Label)e.Row.FindControl("lbl_L2SurTChrg");
            lbl9.Text = SumFooterValue9.ToString() + "";
            Label lbl10 = (Label)e.Row.FindControl("lbl_L2OtherTChrg");
            lbl10.Text = SumFooterValue10.ToString() + "";
            Label lbl11 = (Label)e.Row.FindControl("lbl_L2BankTTChrg");
            lbl11.Text = SumFooterValue11.ToString() + "";
            Label lbl12 = (Label)e.Row.FindControl("lbl_L2InsTChrg");
            lbl12.Text = SumFooterValue12.ToString() + "";
            Label lbl14 = (Label)e.Row.FindControl("lbl_L2TtotalRec");
            lbl14.Text = SumFooterValue14.ToString() + "";
            Label lbl15 = (Label)e.Row.FindControl("Lbl_L2Sanction");
            lbl15.Text = SumFooterValue15.ToString() + "";

        }
    }
    protected void Lnk_SRODTView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkedit = (LinkButton)sender;
            string str = lnkedit.CommandArgument.ToString();
            string[] ARR = str.Split(',');
            string Accno = ARR[0].ToString();
            string Subgl = ARR[1].ToString();
            string Brcd = ARR[2].ToString();

            FL = "Insert";
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "SroRecovry_Rpt" + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());
            string redirectURL = "FrmRView.aspx?FBRCD=" + Brcd.ToString() + "&TBRCD=" + Brcd.ToString() + "&FPRCD=" + Subgl.ToString() + "&TPRCD=" + Subgl.ToString() + "&FDate=" + TxtFDate.Text + "&TDate=" + TxtTDate.Text + "&AType=" + "3" + "&SRO=" + "0000" + "&rptname=Isp_AVS0024.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
}