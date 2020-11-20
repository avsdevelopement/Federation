using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmCustomerDetails : System.Web.UI.Page
{
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsCustomerDetails CD = new ClsCustomerDetails();
    DataTable DT = new DataTable();
    DataTable DT1 = new DataTable();
    ClsCommon com = new ClsCommon();
    ClsLogMaintainance CLM = new ClsLogMaintainance();
    DbConnection conn = new DbConnection();

    string FL = "";
    double SumFooterValue = 0, SumFooterValue1 = 0, SumFooterValue2 = 0, SumFooterValue3 = 0, SumFooterValue4 = 0, SumFooterValue5 = 0, SumFooterValue6 = 0, SumFooterValue7 = 0;
    double TotalValue = 0, TotalValue1 = 0, TotalValue2 = 0, TotalValue3 = 0, TotalValue4 = 0, TotalValue5 = 0, TotalValue6 = 0, TotalValue7 = 0;
    string CustNo = "";
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
                autoglname.ContextKey = Session["BRCD"].ToString();
                automemname.ContextKey = Session["BRCD"].ToString();
                string allow = com.ChkECS(Session["Brcd"].ToString());
                if (allow == "Y")
                {
                    div_schl.Visible = true;
                    DivDep.Visible = true;
                    BtnMemPassbk.Visible = true;
                    LoanApp.Visible = true;
                    BtnRetireVouch.Visible = true;
                    BtnCustTransfer.Visible = true;
                }
                else
                {
                    div_schl.Visible = false;
                    DivDep.Visible = false;
                    BtnMemPassbk.Visible = false;
                    LoanApp.Visible = false;
                    BtnRetireVouch.Visible = false;
                    BtnCustTransfer.Visible = false;
                }
               txtCustNo.Focus();
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtCustNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DT = CD.GetStage(txtCustNo.Text);

            if (DT.Rows[0]["STAGE"].ToString() == "1001")
            {
                WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
                txtCustNo.Focus();
                return;
            }
            else if (DT.Rows[0]["STAGE"].ToString() == "1004")
            {
                WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                txtCustNo.Focus();

                return;
            }
            else if (DT.Rows[0]["STAGE"].ToString() == "" || DT.Rows[0]["STAGE"].ToString() == null)
            {
                WebMsgBox.Show("Customer Not Exists...!!", this.Page);
                txtCustNo.Focus();

                return;
            }
            else
            {
                DT1 = CD.GetCustName(txtCustNo.Text); //ankita 21/11/2017 brcd removed
                txtCustName.Text = DT1.Rows[0]["CUSTNAME"].ToString();
                DT = CD.GetCustAccInfo(Session["BRCD"].ToString(), txtCustNo.Text.ToString());

                if (DT.Rows.Count > 0)
                {
                    txtBOD.Text = DT.Rows[0]["DOB"].ToString();
                    txtOldCustNo.Text = DT.Rows[0]["OLDCTNO"].ToString();
                    Txtmemno.Text = DT.Rows[0]["AccNo"].ToString();
                    TxtMemname.Text = txtCustName.Text;
                    txtOpenDate.Text = DT.Rows[0]["OPENINGDATE"].ToString();
                    txtAddress1.Text = DT.Rows[0]["Permanent"].ToString();
                    txtAddress2.Text = DT.Rows[0]["Present"].ToString();
                    TxtOffcAddr.Text = DT.Rows[0]["Office"].ToString();
                    TxtMob.Text = DT.Rows[0]["MOBILE1"].ToString();
                    TxtPan.Text = DT.Rows[0]["DOC_NO"].ToString();
                    //CD.GetLoanInfo(grdLoan, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString());
                    //CD.GetDepositeInfo(grdDeposite, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString());
                    CD.GetAccountInfo(grdAccDetails, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "SB");
                    CD.GetAccountInfo(GrdDirectLiab, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "DL");
                    //  CD.GetFromSurity(GrdDividend, Session["BRCD"].ToString(), txtCustNo.Text);
                    CD.GetAccountInfo(GrdInDirectLiab, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "SUR");
                    //   CD.GetAccountInfo(GrdOtherAccDetails, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "OA");
                    CD.GetFromSurity(GrdFromSurity, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString());
                    CD.GetNominee(GrdNominee, txtCustNo.Text);
                    string allow = com.ChkECS(Session["Brcd"].ToString());
                    if (allow == "Y")
                    {
                        div_schl.Visible = true;
                        DivDep.Visible = true;
                        TxtSchlNme.Text = CD.GetSchlName(txtCustNo.Text);
                        BtnMemPassbk.Visible = true;
                        LoanApp.Visible = true;
                        BtnRetireVouch.Visible = true;
                        DT = CD.GetDivnameCfno(txtCustNo.Text, Session["Brcd"].ToString());
                        if (DT.Rows.Count > 0)
                        {
                            TxtDivName.Text = DT.Rows[0]["NAMEOFDIV"].ToString();
                            TxtCPFNo.Text = DT.Rows[0]["EMPNO"].ToString();
                            TxtDOR.Text = DT.Rows[0]["DOR"].ToString();
                            TxtRetPeriod.Text = DT.Rows[0]["RTGAGE"].ToString();
                        }
                    }
                    else
                    {
                        div_schl.Visible = false;
                        DivDep.Visible = false;
                        BtnMemPassbk.Visible = false;
                        LoanApp.Visible = false;
                        BtnRetireVouch.Visible = false;
                    }
                    DT = CD.GetDashInfo(Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString());

                    if (DT == null)
                    {
                        lblLoans.Text = "0.00";
                        lblDeposite.Text = "0.00";
                        lblShares.Text = "0.00";
                        lblCASADep.Text = "0.00";
                    }
                    else if (DT.Rows.Count > 0 && DT != null)
                    {
                        lblLoans.Text = Convert.ToDouble(DT.Rows[0]["LOANS"].ToString() == "" ? "0" : DT.Rows[0]["LOANS"]).ToString("0.00");
                        lblDeposite.Text = Convert.ToDouble(DT.Rows[0]["DEPOSITS"].ToString() == "" ? "0" : DT.Rows[0]["DEPOSITS"]).ToString("0.00");
                        lblShares.Text = Convert.ToDouble(DT.Rows[0]["SHARES"].ToString() == "" ? "0" : DT.Rows[0]["SHARES"]).ToString("0.00");
                        lblCASADep.Text = Convert.ToDouble(DT.Rows[0]["CSDEPOSITS"].ToString() == "" ? "0" : DT.Rows[0]["CSDEPOSITS"]).ToString("0.00");
                    }
                    Txtmemno.Focus();
                }
                else
                {
                    txtCustNo.Focus();
                }
            }
            ViewState["ACT"] = "1";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void txtCustName_TextChanged(object sender, EventArgs e)
    {
        try
        {

            string CUNAME = txtCustName.Text;
            string[] custnob = CUNAME.Split('_');

            if (custnob.Length > 1)
            {
                txtCustName.Text = custnob[0].ToString();
                txtCustNo.Text = (string.IsNullOrEmpty(custnob[1].ToString()) ? "" : custnob[1].ToString());
                DT = CD.GetStage(txtCustNo.Text);

                if (DT.Rows[0]["STAGE"].ToString() == "1001")
                {
                    WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
                    return;
                }
                else if (DT.Rows[0]["STAGE"].ToString() == "1004")
                {
                    WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                    return;
                }
                else
                {

                    DT = CD.GetCustAccInfo(Session["BRCD"].ToString(), txtCustNo.Text.ToString());//ankita 21/11/2017 brcd removed

                    if (DT.Rows.Count > 0)
                    {
                        txtBOD.Text = DT.Rows[0]["DOB"].ToString();
                        txtOldCustNo.Text = DT.Rows[0]["OLDCTNO"].ToString();
                        txtOpenDate.Text = DT.Rows[0]["OPENINGDATE"].ToString();
                        txtAddress1.Text = DT.Rows[0]["Permanent"].ToString();
                        txtAddress2.Text = DT.Rows[0]["Present"].ToString();
                        TxtOffcAddr.Text = DT.Rows[0]["Office"].ToString();
                        TxtMob.Text = DT.Rows[0]["MOBILE1"].ToString();
                        TxtPan.Text = DT.Rows[0]["DOC_NO"].ToString();
                        //CD.GetLoanInfo(grdLoan, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString());
                        //CD.GetDepositeInfo(grdDeposite, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString());
                        CD.GetAccountInfo(grdAccDetails, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "SB");
                        CD.GetAccountInfo(GrdDirectLiab, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "DL");
                        CD.GetAccountInfo(GrdInDirectLiab, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "SUR");
                        // CD.GetAccountInfo(GrdOtherAccDetails, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "OA");
                        CD.GetFromSurity(GrdFromSurity, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString());
                        CD.GetNominee(GrdNominee, txtCustNo.Text);
                        string allow = com.ChkECS(Session["Brcd"].ToString());
                        if (allow == "Y")
                        {
                            div_schl.Visible = true;
                            DivDep.Visible = true;
                            TxtSchlNme.Text = CD.GetSchlName(txtCustNo.Text);
                            BtnMemPassbk.Visible = true;
                            LoanApp.Visible = true;
                            BtnRetireVouch.Visible = true;
                            DT = CD.GetDivnameCfno(txtCustNo.Text, Session["Brcd"].ToString());
                            if (DT.Rows.Count > 0)
                            {
                                TxtDivName.Text = DT.Rows[0]["NAMEOFDIV"].ToString();
                                TxtCPFNo.Text = DT.Rows[0]["EMPNO"].ToString();
                                TxtDOR.Text = DT.Rows[0]["DOR"].ToString();
                                TxtRetPeriod.Text = DT.Rows[0]["RTGAGE"].ToString();
                            }
                        }
                        else
                        {
                            div_schl.Visible = false;
                            DivDep.Visible = false;
                            BtnMemPassbk.Visible = false;
                            LoanApp.Visible = false;
                            BtnRetireVouch.Visible = false;
                        }
                        DT = CD.GetDashInfo(Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString());

                        if (DT == null)
                        {
                            lblLoans.Text = "0.00";
                            lblDeposite.Text = "0.00";
                            lblShares.Text = "0.00";
                            lblCASADep.Text = "0.00";
                        }
                        else if (DT.Rows.Count > 0 && DT != null)
                        {
                            lblLoans.Text = Convert.ToDouble(DT.Rows[0]["LOANS"].ToString() == "" ? "0" : DT.Rows[0]["LOANS"]).ToString("0.00");
                            lblDeposite.Text = Convert.ToDouble(DT.Rows[0]["DEPOSITS"].ToString() == "" ? "0" : DT.Rows[0]["DEPOSITS"]).ToString("0.00");
                            lblShares.Text = Convert.ToDouble(DT.Rows[0]["SHARES"].ToString() == "" ? "0" : DT.Rows[0]["SHARES"]).ToString("0.00");
                            lblCASADep.Text = Convert.ToDouble(DT.Rows[0]["CSDEPOSITS"].ToString() == "" ? "0" : DT.Rows[0]["CSDEPOSITS"]).ToString("0.00");
                        }

                    }
                }
            }
            ViewState["ACT"] = "1";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    //protected void grdLoan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    try
    //    {
    //        grdLoan.PageIndex = e.NewPageIndex;
    //        BindGrid();
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}

    //protected void grdDeposite_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    try
    //    {
    //        grdDeposite.PageIndex = e.NewPageIndex;
    //        BindGrid();
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //}

    public void BindGrid()
    {
        try
        {
            //CD.GetLoanInfo(grdLoan, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString());
            //CD.GetDepositeInfo(grdDeposite, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString());
            if (ViewState["ACT"].ToString() == "1")
                CD.GetAccountInfo(grdAccDetails, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "SB");
            if (ViewState["ACT"].ToString() == "2")
                CD.GetAccountInfo(grdAccDetails, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "SB1");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void grdAccDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdAccDetails.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void BtnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string edate, brcd;
            edate = Session["EntryDate"].ToString();
            brcd = Session["BRCD"].ToString();
            FL = "Insert";//Dhanya Shetty
            string Res = CLM.LOGDETAILS(FL, Session["BRCD"].ToString(), Session["MID"].ToString(), "Cust_Details_Rpt _" + txtCustNo.Text + "_" + Session["LOGINCODE"].ToString() + "", "00", Session["MID"].ToString());

            string redirectURL = "FrmRView.aspx?Date=" + edate + "&BRCD=" + brcd + "&CustNo=" + txtCustNo.Text + "&CustName=" + txtCustName.Text + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptCustomerDetails.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void GrdFromSurity_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdFromSurity.PageIndex = e.NewPageIndex;
            CD.GetFromSurity(GrdFromSurity, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString());
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void TxtMemname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string MMNAME = TxtMemname.Text;
            string[] memnob = MMNAME.Split('_');

            if (memnob.Length > 1)
            {
                TxtMemname.Text = memnob[0].ToString();
                Txtmemno.Text = (string.IsNullOrEmpty(memnob[1].ToString()) ? "" : memnob[1].ToString());
                DT = CD.GetMemInfo(Txtmemno.Text);
                if (DT.Rows.Count > 0)
                {
                    txtCustNo.Text = DT.Rows[0]["custno"].ToString();
                    txtCustName.Text = DT.Rows[0]["custname"].ToString();
                    txtBOD.Text = DT.Rows[0]["DOB"].ToString();
                    txtOldCustNo.Text = DT.Rows[0]["OLDCTNO"].ToString();
                    txtOpenDate.Text = DT.Rows[0]["OPENINGDATE"].ToString();
                    txtAddress1.Text = DT.Rows[0]["Permanent"].ToString();
                    txtAddress2.Text = DT.Rows[0]["Present"].ToString();
                    TxtOffcAddr.Text = DT.Rows[0]["Office"].ToString();
                    TxtMob.Text = DT.Rows[0]["MOBILE1"].ToString();
                    TxtPan.Text = DT.Rows[0]["DOC_NO"].ToString();
                    CD.GetAccountInfo(grdAccDetails, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "SB");
                    CD.GetAccountInfo(GrdDirectLiab, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "DL");
                    CD.GetAccountInfo(GrdInDirectLiab, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "SUR");
                    CD.GetFromSurity(GrdFromSurity, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString());
                    CD.GetNominee(GrdNominee, txtCustNo.Text);
                    string allow = com.ChkECS(Session["Brcd"].ToString());
                    if (allow == "Y")
                    {
                        div_schl.Visible = true;
                        DivDep.Visible = true;
                        TxtSchlNme.Text = CD.GetSchlName(txtCustNo.Text);
                        BtnMemPassbk.Visible = true;
                        LoanApp.Visible = true;
                        BtnRetireVouch.Visible = true;
                        DT = CD.GetDivnameCfno(txtCustNo.Text, Session["Brcd"].ToString());
                        if (DT.Rows.Count > 0)
                        {
                            TxtDivName.Text = DT.Rows[0]["NAMEOFDIV"].ToString();
                            TxtCPFNo.Text = DT.Rows[0]["EMPNO"].ToString();
                            TxtDOR.Text = DT.Rows[0]["DOR"].ToString();
                            TxtRetPeriod.Text = DT.Rows[0]["RTGAGE"].ToString();
                        }
                    }
                    else
                    {
                        div_schl.Visible = false;
                        DivDep.Visible = false;
                        BtnMemPassbk.Visible = false;
                        LoanApp.Visible = false;
                        BtnRetireVouch.Visible = false;
                    }
                }
                DT = CD.GetDashInfo(Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString());

                if (DT == null)
                {
                    lblLoans.Text = "0.00";
                    lblDeposite.Text = "0.00";
                    lblShares.Text = "0.00";
                    lblCASADep.Text = "0.00";
                }
                else if (DT.Rows.Count > 0 && DT != null)
                {
                    lblLoans.Text = Convert.ToDouble(DT.Rows[0]["LOANS"].ToString() == "" ? "0" : DT.Rows[0]["LOANS"]).ToString("0.00");
                    lblDeposite.Text = Convert.ToDouble(DT.Rows[0]["DEPOSITS"].ToString() == "" ? "0" : DT.Rows[0]["DEPOSITS"]).ToString("0.00");
                    lblShares.Text = Convert.ToDouble(DT.Rows[0]["SHARES"].ToString() == "" ? "0" : DT.Rows[0]["SHARES"]).ToString("0.00");
                    lblCASADep.Text = Convert.ToDouble(DT.Rows[0]["CSDEPOSITS"].ToString() == "" ? "0" : DT.Rows[0]["CSDEPOSITS"]).ToString("0.00");
                }
                ViewState["ACT"] = "1";
                txtCustNo.Focus();
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void Txtmemno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TxtMemname.Text = BD.GetMemName(Txtmemno.Text);
            DT = CD.GetMemInfo(Txtmemno.Text);
            if (DT.Rows.Count > 0)
            {
                txtCustNo.Text = DT.Rows[0]["custno"].ToString();
                txtCustName.Text = DT.Rows[0]["custname"].ToString();
                txtBOD.Text = DT.Rows[0]["DOB"].ToString();
                txtOldCustNo.Text = DT.Rows[0]["OLDCTNO"].ToString();
                txtOpenDate.Text = DT.Rows[0]["OPENINGDATE"].ToString();
                txtAddress1.Text = DT.Rows[0]["Permanent"].ToString();
                txtAddress2.Text = DT.Rows[0]["Present"].ToString();
                TxtOffcAddr.Text = DT.Rows[0]["Office"].ToString();
                TxtMob.Text = DT.Rows[0]["MOBILE1"].ToString();
                TxtPan.Text = DT.Rows[0]["DOC_NO"].ToString();
                CD.GetAccountInfo(grdAccDetails, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "SB");
                CD.GetAccountInfo(GrdDirectLiab, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "DL");
                CD.GetAccountInfo(GrdInDirectLiab, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "SUR");
                CD.GetFromSurity(GrdFromSurity, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString());
                CD.GetNominee(GrdNominee, txtCustNo.Text);
                string allow = com.ChkECS(Session["Brcd"].ToString());
                if (allow == "Y")
                {
                    div_schl.Visible = true;
                    TxtSchlNme.Text = CD.GetSchlName(txtCustNo.Text);
                    DivDep.Visible = true;
                    BtnMemPassbk.Visible = true;
                    LoanApp.Visible = true;
                    BtnRetireVouch.Visible = true;
                    DT = CD.GetDivnameCfno(txtCustNo.Text, Session["Brcd"].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        TxtDivName.Text = DT.Rows[0]["NAMEOFDIV"].ToString();
                        TxtCPFNo.Text = DT.Rows[0]["EMPNO"].ToString();
                        TxtDOR.Text = DT.Rows[0]["DOR"].ToString();
                        TxtRetPeriod.Text = DT.Rows[0]["RTGAGE"].ToString();
                    }
                }
                else
                {
                    div_schl.Visible = false;
                    DivDep.Visible = false;
                    BtnMemPassbk.Visible = false;
                    LoanApp.Visible = false;
                    BtnRetireVouch.Visible = false;
                }
            }
            DT = CD.GetDashInfo(Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString());

            if (DT == null)
            {
                lblLoans.Text = "0.00";
                lblDeposite.Text = "0.00";
                lblShares.Text = "0.00";
                lblCASADep.Text = "0.00";
            }
            else if (DT.Rows.Count > 0 && DT != null)
            {
                lblLoans.Text = Convert.ToDouble(DT.Rows[0]["LOANS"].ToString() == "" ? "0" : DT.Rows[0]["LOANS"]).ToString("0.00");
                lblDeposite.Text = Convert.ToDouble(DT.Rows[0]["DEPOSITS"].ToString() == "" ? "0" : DT.Rows[0]["DEPOSITS"]).ToString("0.00");
                lblShares.Text = Convert.ToDouble(DT.Rows[0]["SHARES"].ToString() == "" ? "0" : DT.Rows[0]["SHARES"]).ToString("0.00");
                lblCASADep.Text = Convert.ToDouble(DT.Rows[0]["CSDEPOSITS"].ToString() == "" ? "0" : DT.Rows[0]["CSDEPOSITS"]).ToString("0.00");
            }
            ViewState["ACT"] = "1";
            txtCustNo.Focus();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void GrdDirectLiab_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdDirectLiab.PageIndex = e.NewPageIndex;
            if (ViewState["ACT"].ToString() == "1")
                CD.GetAccountInfo(GrdDirectLiab, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "DL");
            if (ViewState["ACT"].ToString() == "2")
                CD.GetAccountInfo(GrdDirectLiab, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "DL1");
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void GrdInDirectLiab_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdInDirectLiab.PageIndex = e.NewPageIndex;
            CD.GetAccountInfo(GrdInDirectLiab, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "SUR");
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    //protected void GrdDividend_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{

    //}
    //protected void GrdOtherAccDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{

    //}
    protected void GrdDirectLiab_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string sanc = string.IsNullOrEmpty(((Label)e.Row.FindControl("lblsancamt")).Text) ? "0" : ((Label)e.Row.FindControl("lblsancamt")).Text;
                TotalValue = Convert.ToDouble(sanc);
                SumFooterValue += TotalValue;
                string sanc1 = string.IsNullOrEmpty(((Label)e.Row.FindControl("lbloutbal")).Text) ? "0" : ((Label)e.Row.FindControl("lbloutbal")).Text;
                TotalValue1 = Convert.ToDouble(sanc1);
                SumFooterValue1 += TotalValue1;
                string sanc2 = string.IsNullOrEmpty(((Label)e.Row.FindControl("lblodamt")).Text) ? "0" : ((Label)e.Row.FindControl("lblodamt")).Text;
                TotalValue2 = Convert.ToDouble(sanc2);
                SumFooterValue2 += TotalValue2;
                string Amt = string.IsNullOrEmpty(((Label)e.Row.FindControl("lblInstAmt")).Text) ? "0" : ((Label)e.Row.FindControl("lblInstAmt")).Text;
                TotalValue3 = Convert.ToDouble(Amt);
                SumFooterValue3 += TotalValue3;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl = (Label)e.Row.FindControl("Lbl_TotalLimit");
                lbl.Text = SumFooterValue.ToString() + ".00";
                Label lbl1 = (Label)e.Row.FindControl("Lbl_TotalBal");
                lbl1.Text = SumFooterValue1.ToString() + ".00";
                Label lbl2 = (Label)e.Row.FindControl("Lbl_TotalOD");
                lbl2.Text = SumFooterValue2.ToString() + ".00";
                Label lbl3 = (Label)e.Row.FindControl("Lbl_TotaliNSTALL");
                lbl3.Text = SumFooterValue3.ToString() + ".00";
                if (lbl.Text == "0")
                    lbl.Text = "";
                if (lbl1.Text == "0")
                    lbl1.Text = "";
                if (lbl2.Text == "0")
                    lbl2.Text = "";
                if (lbl3.Text == "0")
                    lbl3.Text = "";
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    //protected void GrdOtherAccDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            string Amt = string.IsNullOrEmpty(((Label)e.Row.FindControl("lblsancamt")).Text) ? "0" : ((Label)e.Row.FindControl("lblsancamt")).Text;
    //            TotalValue3 = Convert.ToDouble(Amt);
    //            SumFooterValue3 += TotalValue3;
    //            string Amt1 = string.IsNullOrEmpty(((Label)e.Row.FindControl("lblbal")).Text) ? "0" : ((Label)e.Row.FindControl("lblbal")).Text;
    //            TotalValue4 = Convert.ToDouble(Amt1);
    //            SumFooterValue4 += TotalValue4;

    //        }
    //        if (e.Row.RowType == DataControlRowType.Footer)
    //        {
    //            Label lbl3 = (Label)e.Row.FindControl("Lbl_TotalBalDAmt");
    //            lbl3.Text = SumFooterValue3.ToString() + ".00";
    //            Label lbl4 = (Label)e.Row.FindControl("Lbl_TotalBalBl");
    //            lbl4.Text = SumFooterValue4.ToString() + ".00";
    //            if (lbl3.Text == "0")
    //                lbl3.Text = "";
    //            if (lbl4.Text == "0")
    //                lbl4.Text = "";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionLogging.SendErrorToText(ex);
    //    }
    //}
    protected void GrdInDirectLiab_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string sanc5 = string.IsNullOrEmpty(((Label)e.Row.FindControl("lblsancamtIn")).Text) ? "0" : ((Label)e.Row.FindControl("lblsancamtIn")).Text;
                TotalValue5 = Convert.ToDouble(sanc5);
                SumFooterValue5 += TotalValue5;
                string sanc6 = string.IsNullOrEmpty(((Label)e.Row.FindControl("lbloutbalIn")).Text) ? "0" : ((Label)e.Row.FindControl("lbloutbalIn")).Text;
                TotalValue6 = Convert.ToDouble(sanc6);
                SumFooterValue6 += TotalValue6;
                string sanc7 = string.IsNullOrEmpty(((Label)e.Row.FindControl("lblodamtIn")).Text) ? "0" : ((Label)e.Row.FindControl("lblodamtIn")).Text;
                TotalValue7 = Convert.ToDouble(sanc7);
                SumFooterValue7 += TotalValue7;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl5 = (Label)e.Row.FindControl("Lbl_TotalLimitIn");
                lbl5.Text = SumFooterValue5.ToString() + ".00";
                Label lbl6 = (Label)e.Row.FindControl("Lbl_TotalBalIn");
                lbl6.Text = SumFooterValue6.ToString() + ".00";
                Label lbl7 = (Label)e.Row.FindControl("Lbl_TotalODIn");
                lbl7.Text = SumFooterValue7.ToString() + ".00";
                if (lbl5.Text == "0")
                    lbl5.Text = "";
                if (lbl6.Text == "0")
                    lbl6.Text = "";
                if (lbl7.Text == "0")
                    lbl7.Text = "";
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void LoanApp_Click(object sender, EventArgs e)
    {
        if (txtCustNo.Text == "")
        {
            WebMsgBox.Show("Please enter customer number first..!!", this.Page);
            return;
        }
        else
        {
            string Para = Convert.ToString(conn.sExecuteScalar("select LISTVALUE from PARAMETER where LISTFIELD='LnAPPValidat'"));

            if (Para == "Y")
            {
                CustNo = txtCustNo.Text;
                Response.Redirect("~/FrmAVS5027_F7.aspx?Flag=AD&CUSTNO=" + CustNo + "&CustName=" + txtCustName.Text);
            }
            else
            {
                CustNo = txtCustNo.Text;
                Response.Redirect("~/FrmAVS5027.aspx?Flag=AD&CUSTNO=" + CustNo + "&CustName=" + txtCustName.Text);
            }
        }
    }
    protected void BtnMemPassbk_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCustNo.Text == "")
            {
                WebMsgBox.Show("Please enter customer number first..!!", this.Page);
                return;
            }
            else
            {
                HttpContext.Current.Response.Redirect("FrmMemberPassbook.aspx?CUSTNO=" + txtCustNo.Text + "", true);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnRetireVouch_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCustNo.Text == "")
            {
                WebMsgBox.Show("Please enter customer number first..!!", this.Page);
                return;
            }
            else
            {
                HttpContext.Current.Response.Redirect("FrmAVS5040.aspx?CUSTNO=" + txtCustNo.Text + "", true);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkClose_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string[] strnumId = objlink.CommandArgument.Split('-');
            ViewState["GLCODE"] = strnumId[0].ToString();
            ViewState["SUBGLCODE"] = strnumId[1].ToString();
            ViewState["ACCNO"] = strnumId[2].ToString();
            if (ViewState["GLCODE"].ToString() == "5")
            {
                HttpContext.Current.Response.Redirect("frmTDAClear.aspx?SUBGLCODE=" + ViewState["SUBGLCODE"].ToString() + "&ACCNO=" + ViewState["ACCNO"].ToString() + "", true);
            }
            else if (ViewState["GLCODE"].ToString() == "3")
            {
                HttpContext.Current.Response.Redirect("FrmLoanClosure.aspx?SUBGLCODE=" + ViewState["SUBGLCODE"].ToString() + "&ACCNO=" + ViewState["ACCNO"].ToString() + "", true);
            }
            else if (ViewState["GLCODE"].ToString() == "1")
            {
                HttpContext.Current.Response.Redirect("FrmCheckACClose.aspx?SUBGLCODE=" + ViewState["SUBGLCODE"].ToString() + "&ACCNO=" + ViewState["ACCNO"].ToString() + "", true);
            }
            else
            {
                WebMsgBox.Show("Details cannot be found..!!", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton objlink = (LinkButton)sender;
            string[] strnumId = objlink.CommandArgument.Split('-');
            ViewState["MEMNO"] = strnumId[0].ToString();
            string custno = CD.getCustnoMem(ViewState["MEMNO"].ToString());
            CD.GetAccountInfo(GrdDirectLiab1, Session["BRCD"].ToString(), custno, Session["EntryDate"].ToString(), "DL");
            string Modal_Flag = "LOANDETAILS";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#" + Modal_Flag + "').modal('show');");
            sb.Append(@"</script>");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnShwClsAcc_Click(object sender, EventArgs e)
    {
        try
        {
            CD.GetAccountInfo(grdAccDetails, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "SB1");
            CD.GetAccountInfo(GrdDirectLiab, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "DL1");
            CD.GetAccountInfo(GrdInDirectLiab, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "SUR");
            CD.GetFromSurity(GrdFromSurity, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString());
            CD.GetNominee(GrdNominee, txtCustNo.Text);
            string allow = com.ChkECS(Session["Brcd"].ToString());
            if (allow == "Y")
            {
                div_schl.Visible = true;
                DivDep.Visible = true;
                TxtSchlNme.Text = CD.GetSchlName(txtCustNo.Text);
                BtnMemPassbk.Visible = true;
                LoanApp.Visible = true;
                BtnRetireVouch.Visible = true;
                DT = CD.GetDivnameCfno(txtCustNo.Text, Session["Brcd"].ToString());
                if (DT.Rows.Count > 0)
                {
                    TxtDivName.Text = DT.Rows[0]["NAMEOFDIV"].ToString();
                    TxtCPFNo.Text = DT.Rows[0]["EMPNO"].ToString();
                    TxtDOR.Text = DT.Rows[0]["DOR"].ToString();
                    TxtRetPeriod.Text = DT.Rows[0]["RTGAGE"].ToString();
                }
            }
            else
            {
                div_schl.Visible = false;
                DivDep.Visible = false;
                BtnMemPassbk.Visible = false;
                LoanApp.Visible = false;
                BtnRetireVouch.Visible = false;
            }
            ViewState["ACT"] = "2";
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BtnCustTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCustNo.Text == "")
            {
                WebMsgBox.Show("Please enter customer number first..!!", this.Page);
                return;
            }
            else
            {
                HttpContext.Current.Response.Redirect("FrmAVS5074.aspx?CUSTNO=" + txtCustNo.Text + "", true);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void txtOldCustNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string NewCust = CD.getNewCust(txtOldCustNo.Text, Session["BRCD"].ToString());
            txtCustNo.Text = NewCust;
            DT = CD.GetStage(NewCust);

            if (DT.Rows[0]["STAGE"].ToString() == "1001")
            {
                WebMsgBox.Show("Customer Not Authoried...!!", this.Page);
                return;
            }
            else if (DT.Rows[0]["STAGE"].ToString() == "1004")
            {
                WebMsgBox.Show("Customer is Deleted...!!", this.Page);
                return;
            }
            else if (DT.Rows[0]["STAGE"].ToString() == "" || DT.Rows[0]["STAGE"].ToString() == null)
            {
                WebMsgBox.Show("Customer Not Exists...!!", this.Page);
                return;
            }
            else
            {
                DT1 = CD.GetCustName(NewCust); //ankita 21/11/2017 brcd removed
                txtCustName.Text = DT1.Rows[0]["CUSTNAME"].ToString();
                DT = CD.GetCustAccInfo(Session["BRCD"].ToString(), NewCust);

                if (DT.Rows.Count > 0)
                {
                    txtBOD.Text = DT.Rows[0]["DOB"].ToString();
                    // txtOldCustNo.Text = DT.Rows[0]["OLDCTNO"].ToString();
                    txtOpenDate.Text = DT.Rows[0]["OPENINGDATE"].ToString();
                    txtAddress1.Text = DT.Rows[0]["Permanent"].ToString();
                    txtAddress2.Text = DT.Rows[0]["Present"].ToString();
                    TxtOffcAddr.Text = DT.Rows[0]["Office"].ToString();
                    TxtMob.Text = DT.Rows[0]["MOBILE1"].ToString();
                    TxtPan.Text = DT.Rows[0]["DOC_NO"].ToString();
                    //CD.GetLoanInfo(grdLoan, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString());
                    //CD.GetDepositeInfo(grdDeposite, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString());
                    CD.GetAccountInfo(grdAccDetails, Session["BRCD"].ToString(), NewCust, Session["EntryDate"].ToString(), "SB");
                    CD.GetAccountInfo(GrdDirectLiab, Session["BRCD"].ToString(), NewCust, Session["EntryDate"].ToString(), "DL");
                    //  CD.GetFromSurity(GrdDividend, Session["BRCD"].ToString(), txtCustNo.Text);
                    CD.GetAccountInfo(GrdInDirectLiab, Session["BRCD"].ToString(), NewCust, Session["EntryDate"].ToString(), "SUR");
                    //   CD.GetAccountInfo(GrdOtherAccDetails, Session["BRCD"].ToString(), txtCustNo.Text, Session["EntryDate"].ToString(), "OA");
                    CD.GetFromSurity(GrdFromSurity, Session["BRCD"].ToString(), NewCust, Session["EntryDate"].ToString());
                    CD.GetNominee(GrdNominee, txtCustNo.Text);
                    string allow = com.ChkECS(Session["Brcd"].ToString());
                    if (allow == "Y")
                    {
                        div_schl.Visible = true;
                        DivDep.Visible = true;
                        TxtSchlNme.Text = CD.GetSchlName(NewCust);
                        BtnMemPassbk.Visible = true;
                        LoanApp.Visible = true;
                        BtnRetireVouch.Visible = true;
                        DT = CD.GetDivnameCfno(txtCustNo.Text, Session["Brcd"].ToString());
                        if (DT.Rows.Count > 0)
                        {
                            TxtDivName.Text = DT.Rows[0]["NAMEOFDIV"].ToString();
                            TxtCPFNo.Text = DT.Rows[0]["EMPNO"].ToString();
                            TxtDOR.Text = DT.Rows[0]["DOR"].ToString();
                            TxtRetPeriod.Text = DT.Rows[0]["RTGAGE"].ToString();
                        }
                    }
                    else
                    {
                        div_schl.Visible = false;
                        DivDep.Visible = false;
                        BtnMemPassbk.Visible = false;
                        LoanApp.Visible = false;
                        BtnRetireVouch.Visible = false;
                    }
                    DT = CD.GetDashInfo(Session["BRCD"].ToString(), NewCust, Session["EntryDate"].ToString());

                    if (DT == null)
                    {
                        lblLoans.Text = "0.00";
                        lblDeposite.Text = "0.00";
                        lblShares.Text = "0.00";
                        lblCASADep.Text = "0.00";
                    }
                    else if (DT.Rows.Count > 0 && DT != null)
                    {
                        lblLoans.Text = Convert.ToDouble(DT.Rows[0]["LOANS"].ToString() == "" ? "0" : DT.Rows[0]["LOANS"]).ToString("0.00");
                        lblDeposite.Text = Convert.ToDouble(DT.Rows[0]["DEPOSITS"].ToString() == "" ? "0" : DT.Rows[0]["DEPOSITS"]).ToString("0.00");
                        lblShares.Text = Convert.ToDouble(DT.Rows[0]["SHARES"].ToString() == "" ? "0" : DT.Rows[0]["SHARES"]).ToString("0.00");
                        lblCASADep.Text = Convert.ToDouble(DT.Rows[0]["CSDEPOSITS"].ToString() == "" ? "0" : DT.Rows[0]["CSDEPOSITS"]).ToString("0.00");
                    }
                }
            }
            ViewState["ACT"] = "1";
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void GrdNominee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdNominee.PageIndex = e.NewPageIndex;
            CD.GetNominee(GrdNominee, txtCustNo.Text);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }
    protected void grdAccDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            DateTime EntryDate = new DateTime();
            DateTime DueDate = new DateTime();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_GLCode = (Label)e.Row.FindControl("lblGLCODE");

                if (lbl_GLCode.Text == "5")
                {
                    Label lblDueDate = (Label)e.Row.FindControl("lblDUEDT");
                    if (!String.IsNullOrEmpty(Convert.ToString(lblDueDate.Text)))
                    {
                        DueDate = Convert.ToDateTime(lblDueDate.Text);
                        EntryDate = Convert.ToDateTime(Session["EntryDate"]);
                        //  string[] dueDateArray = lblDueDate.Text.Split('/');
                        //  string[] entryDateArray = Convert.ToString(Session["EntryDate"]).Split('/');
                        if (DueDate <= EntryDate)
                        {
                            e.Row.BackColor = System.Drawing.Color.Yellow;

                        }

                    }
                    //if (Convert.ToInt32(dueDateArray[0]) == Convert.ToInt32(entryDateArray[0]))
                    //{
                    //    e.Row.BackColor = System.Drawing.Color.Yellow;
                    //}



                }
            }

        }






        catch (Exception)
        {

            throw;
        }
    }
}