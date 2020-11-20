using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AVS5036 : System.Web.UI.Page
{
    ClsInsertTrans ITrans = new ClsInsertTrans();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsAVS5036 SI = new ClsAVS5036();
    DataTable LoanBal = new DataTable();
    DataTable DT = new DataTable();
    string CrGlCode = "", CrSubGlCode = "", CrAccNo = "", CrCustNo = "";
    string DrGlCode = "", DrSubGlCode = "", DrAccNo = "", EDate = "";
    double DebitAmt, PrincAmt, TotalDrAmt, TotalAmt = 0;
    double SurTotal, OtherChrg, CurSurChrg = 0;
    string sResult = "", SetNo = "", SetNo1 = "", RefNumber = "";
    int Result = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BD.BindPosting(ddlOperation);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string Flag1 = "", Flag2 = "";
            string Parti1 = "", Parti2 = "";

            if (rbtnSIType.SelectedValue == "1")
            {
                Flag1 = "SBTORD";

                if (ddlOperation.SelectedValue == "1")
                {
                    Flag2 = "Post";

                    sResult = SI.SIPost(Session["BRCD"].ToString(), txtPostDate.Text.Trim().ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), Flag1.ToString(), Flag2.ToString());

                    if (Convert.ToInt32(sResult) > 0)
                    {
                        WebMsgBox.Show("Successfully Post With SetNo : " + sResult + "....!", this.Page);
                        return;
                    }
                }
                else if (ddlOperation.SelectedValue == "2")
                {
                    Flag2 = "Trail";

                    string redirectURL = "FrmReportViewer.aspx?BRCD=" + Session["BRCD"].ToString() + "&EDate=" + txtPostDate.Text.Trim().ToString() + "&WDate=" + Session["EntryDate"].ToString() + "&MID=" + Session["MID"].ToString() + "&Flag1=" + Flag1.ToString() + "&Flag2=" + Flag2.ToString() + "&rptname=RptAVS5036.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
            else if (rbtnSIType.SelectedValue == "2")
            {
                Flag1 = "SBTOLN";

                if (ddlOperation.SelectedValue == "1")
                {
                    Flag2 = "Post";
                    LoanBal = SI.GetSIPostData(Session["BRCD"].ToString(), txtPostDate.Text.ToString(), Session["EntryDate"].ToString(), Session["MID"].ToString(), Flag1.ToString(), Flag2.ToString());

                    if (LoanBal.Rows.Count > 0)
                    {
                        //Generate First Set Number Here
                        SetNo = "";
                        SetNo = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString());
                        SetNo1 = "";
                        SetNo1 = BD.GetSetNo(Session["EntryDate"].ToString(), "DaySetNo", Session["BRCD"].ToString());
                        RefNumber = "";
                        RefNumber = BD.GetMaxRefid(Session["BRCD"].ToString(), Session["EntryDate"].ToString(), "REFID");
                        ViewState["RID"] = (Convert.ToInt32(RefNumber) + 1).ToString();

                        for (int i = 0; i < LoanBal.Rows.Count; i++)
                        {
                            CrGlCode = LoanBal.Rows[i]["CrGlCode"].ToString();
                            CrSubGlCode = LoanBal.Rows[i]["CrPrCode"].ToString();
                            CrAccNo = LoanBal.Rows[i]["CrAccNo"].ToString();
                            CrCustNo = LoanBal.Rows[i]["CrCustNo"].ToString();
                            DrGlCode = LoanBal.Rows[i]["DrGlCode"].ToString();
                            DrSubGlCode = LoanBal.Rows[i]["DrPrCode"].ToString();
                            DrAccNo = LoanBal.Rows[i]["DrAccNo"].ToString();
                            EDate = Session["EntryDate"].ToString();
                            Parti1 = "SI Post Dr From " + DrSubGlCode.ToString() + "-" + DrAccNo.ToString();
                            Parti2 = "SI Post Cr To " + CrSubGlCode.ToString() + "-" + CrAccNo.ToString();

                            if (Convert.ToDouble(LoanBal.Rows[i]["SBBalance"].ToString() == "" ? "0" : LoanBal.Rows[i]["SBBalance"].ToString()) > 0.00)
                            {
                                DT = new DataTable();
                                DT = SI.GetLoanTotalAmount(Session["BRCD"].ToString(), CrSubGlCode.ToString(), CrAccNo.ToString(), EDate.ToString());

                                TotalAmt = Math.Round(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Principle"].ToString()) < 0 ? "0" : DT.Rows[0]["Principle"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["InterestRec"].ToString()) < 0 ? "0" : DT.Rows[0]["InterestRec"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["NoticeChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["NoticeChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["ServiceChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["ServiceChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CourtChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["CourtChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["SurChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["OtherChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["BankChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["BankChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["InsChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["InsChrg"].ToString()));

                                if (Convert.ToDouble(LoanBal.Rows[i]["SBBalance"].ToString() == "" ? "0" : LoanBal.Rows[i]["SBBalance"].ToString()) < TotalAmt)
                                {
                                    TotalDrAmt = Convert.ToDouble(LoanBal.Rows[i]["SIAmount"].ToString() == "" ? "0" : LoanBal.Rows[i]["SIAmount"].ToString());

                                    if (DT.Rows[0]["Acc_Status"].ToString() == "9")
                                    {
                                        SurTotal = Math.Round(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["InterestRec"].ToString()) < 0 ? "0" : DT.Rows[0]["InterestRec"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Principle"].ToString()) < 0 ? "0" : DT.Rows[0]["Principle"].ToString()));

                                        if (Convert.ToDouble(TotalDrAmt) > Convert.ToDouble(SurTotal))
                                        {
                                            CurSurChrg = Math.Round(Convert.ToDouble((SurTotal * 1.75) / 100));
                                        }
                                        else
                                        {
                                            CurSurChrg = Math.Round(Convert.ToDouble((TotalDrAmt * 1.75) / 100));
                                        }
                                        TotalAmt = TotalAmt + CurSurChrg;
                                    }
                                    else
                                    {
                                        CurSurChrg = 0;
                                    }

                                    OtherChrg = Math.Round(Convert.ToDouble(((Math.Abs(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Principle"].ToString()) < 0 ? "0" : DT.Rows[0]["Principle"].ToString())) * Convert.ToDouble(SI.GetOtherIntRate(Session["BRCD"].ToString(), DT.Rows[0]["SubGlCode"].ToString()))) * (Convert.ToInt32(DT.Rows[0]["Days"].ToString()))) / 36500));

                                    if (OtherChrg == null || OtherChrg.ToString() == "")
                                        OtherChrg = 0;
                                    else
                                        TotalAmt = TotalAmt + OtherChrg;

                                    if (DT.Rows[0]["IntCalType"].ToString() == "1" || DT.Rows[0]["IntCalType"].ToString() == "2")
                                    {
                                        //Saving Account Dabit
                                        Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DrGlCode, DrSubGlCode, DrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "2", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());
                                        
                                        //  For Insurance Charge
                                        if (Result > 0)
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["InsChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["InsChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["InsChrg"].ToString()))
                                            {
                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["InsChrgGl"].ToString(), DT.Rows[0]["InsChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(DT.Rows[0]["InsChrg"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                if (Result > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["InsChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["InsChrg"].ToString()) > 0)
                                                    {
                                                        //  Insurance Charge Credit To 11 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["InsChrgSub"].ToString(), CrAccNo.ToString(), "11", "1", "7", "Insurance Charge Credit", DT.Rows[0]["InsChrg"].ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["InsChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["InsChrg"].ToString()));
                                            }
                                            else if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["InsChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                            {
                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["InsChrgGl"].ToString(), DT.Rows[0]["InsChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                if (Result > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["InsChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["InsChrg"].ToString()) > 0)
                                                    {
                                                        //  Insurance Charge Credit To 11 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["InsChrgSub"].ToString(), CrAccNo.ToString(), "11", "1", "7", "Insurance Charge Credit", TotalDrAmt.ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = 0;
                                            }
                                        }

                                        //  For Bank Charges
                                        if (Result > 0)
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["BankChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["BankChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["BankChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["BankChrg"].ToString()))
                                            {
                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["BankChrgGl"].ToString(), DT.Rows[0]["BankChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(DT.Rows[0]["BankChrg"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                if (Result > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["BankChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["BankChrg"].ToString()) > 0)
                                                    {
                                                        // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["BankChrgSub"].ToString(), CrAccNo.ToString(), "10", "1", "7", "Bank Charges Credit", DT.Rows[0]["BankChrg"].ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["BankChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["BankChrg"].ToString()));
                                            }
                                            else if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["BankChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["BankChrg"].ToString()) > 0 && TotalDrAmt > 0 && TotalDrAmt > 0)
                                            {
                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["BankChrgGl"].ToString(), DT.Rows[0]["BankChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString()); 

                                                if (Result > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["BankChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["BankChrg"].ToString()) > 0)
                                                    {
                                                        // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["BankChrgSub"].ToString(), CrAccNo.ToString(), "10", "1", "7", "Bank Charges Credit", TotalDrAmt.ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = 0;
                                            }
                                        }

                                        //  For Other Charges
                                        if (Result > 0)
                                        {
                                            if ((Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0 && TotalDrAmt >= (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["OtherChrg"].ToString()) + OtherChrg))
                                            {
                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["OtherChrgGl"].ToString(), DT.Rows[0]["OtherChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["OtherChrg"].ToString()) + OtherChrg), "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                if (Result > 0)
                                                {
                                                    if (OtherChrg > 0)
                                                    {
                                                        // Other Charges Amt Debit To 9 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["OtherChrgSub"].ToString(), CrAccNo.ToString(), "9", "2", "7", "Other Charges Debit", OtherChrg.ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }
                                                if (Result > 0)
                                                {
                                                    if ((Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0)
                                                    {
                                                        // Other Charges Amt Credit To 9 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["OtherChrgSub"].ToString(), CrAccNo.ToString(), "9", "1", "7", "Other Charges Credit", Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["OtherChrg"].ToString()) + OtherChrg).ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["OtherChrg"].ToString()) + OtherChrg));
                                            }
                                            else if ((Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0 && TotalDrAmt > 0)
                                            {
                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["OtherChrgGl"].ToString(), DT.Rows[0]["OtherChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                if (Result > 0)
                                                {
                                                    if (OtherChrg > 0)
                                                    {
                                                        // Other Charges Amt Debit To 9 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["OtherChrgSub"].ToString(), CrAccNo.ToString(), "9", "2", "7", "Other Charges Debit", OtherChrg.ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }
                                                if (Result > 0)
                                                {
                                                    if ((Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0)
                                                    {
                                                        // Other Charges Amt Credit To 9 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["OtherChrgSub"].ToString(), CrAccNo.ToString(), "9", "1", "7", "Other Charges Credit", TotalDrAmt.ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = 0;
                                            }
                                        }

                                        //  For Sur Charges
                                        if (Result > 0)
                                        {
                                            if ((Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0 && TotalDrAmt >= (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["SurChrg"].ToString()) + CurSurChrg))
                                            {
                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["SurChrgGl"].ToString(), DT.Rows[0]["SurChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["SurChrg"].ToString()) + CurSurChrg), "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                if (Result > 0)
                                                {
                                                    if (CurSurChrg > 0)
                                                    {
                                                        // Sur Charges Debited To 8 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["SurChrgSub"].ToString(), CrAccNo.ToString(), "8", "2", "7", "Sur Charges Debited", CurSurChrg.ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                    if ((Convert.ToDouble(DT.Rows[0]["SurChrg"].ToString() == "" ? "0" : DT.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0)
                                                    {
                                                        // Sur Charges Credit To 8 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["SurChrgSub"].ToString(), CrAccNo.ToString(), "8", "1", "7", "Sur Charges Credit", Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["SurChrg"].ToString()) + CurSurChrg).ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["SurChrg"].ToString()) + CurSurChrg));
                                            }
                                            else if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["SurChrg"].ToString() + CurSurChrg) > 0 && TotalDrAmt > 0)
                                            {
                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["SurChrgGl"].ToString(), DT.Rows[0]["SurChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                if (Result > 0)
                                                {
                                                    if (CurSurChrg > 0)
                                                    {
                                                        // Sur Charges Debited To 8 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["SurChrgSub"].ToString(), CrAccNo.ToString(), "8", "2", "7", "Sur Charges Debited", CurSurChrg.ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                    if ((Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0)
                                                    {
                                                        // Sur Charges Credit To 8 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["SurChrgSub"].ToString(), CrAccNo.ToString(), "8", "1", "7", "Sur Charges Credit", TotalDrAmt.ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = 0;
                                            }
                                        }

                                        // For Court Charges
                                        if (Result > 0)
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CourtChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["CourtChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CourtChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["CourtChrg"].ToString()))
                                            {
                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["CourtChrgGl"].ToString(), DT.Rows[0]["CourtChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(DT.Rows[0]["CourtChrg"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                if (Result > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CourtChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["CourtChrg"].ToString()) > 0)
                                                    {
                                                        // Court Charges Amt Credit To 7 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["CourtChrgSub"].ToString(), CrAccNo.ToString(), "7", "1", "7", "Court Charges Credit", DT.Rows[0]["CourtChrg"].ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CourtChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["CourtChrg"].ToString()));
                                            }
                                            else if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CourtChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["CourtChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                            {
                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["CourtChrgGl"].ToString(), DT.Rows[0]["CourtChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                if (Result > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CourtChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["CourtChrg"].ToString()) > 0)
                                                    {
                                                        // Court Charges Amt Credit To 7 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["CourtChrgSub"].ToString(), CrAccNo.ToString(), "7", "1", "7", "Court Charges Credit", TotalDrAmt.ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = 0;
                                            }
                                        }

                                        //  For Service Charges
                                        if (Result > 0)
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["ServiceChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["ServiceChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["ServiceChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["ServiceChrg"].ToString()))
                                            {
                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["ServiceChrgGl"].ToString(), DT.Rows[0]["ServiceChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(DT.Rows[0]["ServiceChrg"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                if (Result > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["ServiceChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["ServiceChrg"].ToString()) > 0)
                                                    {
                                                        // Service Charges Amt Credit To 6 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["ServiceChrgSub"].ToString(), CrAccNo.ToString(), "6", "1", "7", "Service Charges Credit", DT.Rows[0]["ServiceChrg"].ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["ServiceChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["ServiceChrg"].ToString()));
                                            }
                                            else if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["ServiceChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["ServiceChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                            {
                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["ServiceChrgGl"].ToString(), DT.Rows[0]["ServiceChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                if (Result > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["ServiceChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["ServiceChrg"].ToString()) > 0)
                                                    {
                                                        // Service Charges Amt Credit To 6 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["ServiceChrgSub"].ToString(), CrAccNo.ToString(), "6", "1", "7", "Service Charges Credit", TotalDrAmt.ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = 0;
                                            }
                                        }

                                        //  For Notice Charges
                                        if (Result > 0)
                                        {
                                            if (Convert.ToDouble(DT.Rows[0]["NoticeChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["NoticeChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["NoticeChrg"].ToString()))
                                            {
                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["NoticeChrgGl"].ToString(), DT.Rows[0]["NoticeChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(DT.Rows[0]["NoticeChrg"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                if (Result > 0)
                                                {
                                                    if (Convert.ToDouble(DT.Rows[0]["NoticeChrg"].ToString()) > 0)
                                                    {
                                                        // Notice Charges Credit To 5 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["NoticeChrgSub"].ToString(), CrAccNo.ToString(), "5", "1", "7", "Notice Charges Credit", DT.Rows[0]["NoticeChrg"].ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["NoticeChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["NoticeChrg"].ToString()));
                                            }
                                            else if (Convert.ToDouble(DT.Rows[0]["NoticeChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                            {
                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["NoticeChrgGl"].ToString(), DT.Rows[0]["NoticeChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                if (Result > 0)
                                                {
                                                    if (Convert.ToDouble(DT.Rows[0]["NoticeChrg"].ToString()) > 0)
                                                    {
                                                        // Notice Charges Credit To 5 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["NoticeChrgSub"].ToString(), CrAccNo.ToString(), "5", "1", "7", "Notice Charges Credit", TotalDrAmt.ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }
                                                TotalDrAmt = 0;
                                            }
                                        }

                                        //  For Interest Receivable
                                        if (Result > 0)
                                        {
                                            if (Convert.ToDouble(DT.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["InterestRec"].ToString()) < 0 ? "0" : DT.Rows[0]["InterestRec"].ToString()))
                                            {
                                                if (DT.Rows[0]["IntCalType"].ToString() == "1")
                                                {
                                                    // Interest Received credit to GL 11
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["InterestRecGl"].ToString(), DT.Rows[0]["InterestRecSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(DT.Rows[0]["InterestRec"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(DT.Rows[0]["InterestRec"].ToString()) > 0)
                                                        {
                                                            // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["InterestRecSub"].ToString(), CrAccNo.ToString(), "4", "1", "7", "Interest Received  Credit", DT.Rows[0]["InterestRec"].ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                }
                                                else if (DT.Rows[0]["IntCalType"].ToString() == "2")
                                                {
                                                    // Interest Received credit to GL 3
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), CrGlCode.ToString(), CrSubGlCode.ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(DT.Rows[0]["InterestRec"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());
                                                }
                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["InterestRec"].ToString()) < 0 ? "0" : DT.Rows[0]["InterestRec"].ToString()));
                                            }
                                            else if (Convert.ToDouble(DT.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt > 0)
                                            {
                                                if (DT.Rows[0]["IntCalType"].ToString() == "1")
                                                {
                                                    // Interest Received credit to GL 11
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["InterestRecGl"].ToString(), DT.Rows[0]["InterestRecSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(DT.Rows[0]["InterestRec"].ToString()) > 0)
                                                        {
                                                            // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["InterestRecSub"].ToString(), CrAccNo.ToString(), "4", "1", "7", "Interest Received  Credit", TotalDrAmt.ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                }
                                                else if (DT.Rows[0]["IntCalType"].ToString() == "2")
                                                {
                                                    // Interest Received credit to GL 3
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), CrGlCode.ToString(), CrSubGlCode.ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());
                                                }
                                                TotalDrAmt = 0;
                                            }
                                        }

                                        //  For Penal Charge
                                        if (Result > 0)
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString())))
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                {
                                                    //Penal Charge Credit To GL 12
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["PInterestGl"].ToString(), DT.Rows[0]["PInterestSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString())), "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());
                                                }

                                                if (Result > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString()) > 0)
                                                    {
                                                        //Penal Interest Debit To 3 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["PInterestSub"].ToString(), CrAccNo.ToString(), "3", "2", "7", "Penal Interest Debit", DT.Rows[0]["CurrPInterest"].ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }

                                                if (Result > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                    {
                                                        //Penal Interest Credit To 3 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["PInterestSub"].ToString(), CrAccNo.ToString(), "3", "1", "7", "Penal Interest Credit", Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString())).ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }

                                                if (Result > 0)
                                                {
                                                    //Penal Charge Contra
                                                    if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                    {
                                                        //Penal chrg Applied Debit To GL 12
                                                        Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["PInterestGl"].ToString(), DT.Rows[0]["PInterestSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString())), "2", "12", "TR_INT", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                        if (Result > 0)
                                                        {
                                                            //Penal chrg Applied Credit to GL 100
                                                            Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), "100", DT.Rows[0]["PlAccNo2"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString())), "1", "12", "TR_INT", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());
                                                        }
                                                    }
                                                }

                                                TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString())));
                                            }
                                            else if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                            {
                                                //Penal Charge Credit To GL 12
                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["PInterestGl"].ToString(), DT.Rows[0]["PInterestSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                if (Result > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString()) > 0)
                                                    {
                                                        //Penal Interest Debit To 3 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["PInterestSub"].ToString(), CrAccNo.ToString(), "3", "2", "7", "Penal Interest Debit", DT.Rows[0]["CurrPInterest"].ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }

                                                if (Result > 0)
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                    {
                                                        //Penal Interest Credit To 3 In AVS_LnTrx
                                                        Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["PInterestSub"].ToString(), CrAccNo.ToString(), "3", "1", "7", "Penal Interest Credit", Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString())).ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                    }
                                                }

                                                if (Result > 0)
                                                {
                                                    //Penal Charge Contra
                                                    if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                    {
                                                        //Penal chrg Applied Debit To GL 12
                                                        Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["PInterestGl"].ToString(), DT.Rows[0]["PInterestSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "2", "12", "TR_INT", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                        if (Result > 0)
                                                        {
                                                            //Penal chrg Applied Credit to GL 100
                                                            Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), "100", DT.Rows[0]["PlAccNo2"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "12", "TR_INT", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());
                                                        }
                                                    }
                                                }

                                                TotalDrAmt = 0;
                                            }
                                        }

                                        //  For Interest
                                        if (Result > 0)
                                        {
                                            if (DT.Rows[0]["IntCalType"].ToString() == "1")
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())))
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())) > 0)
                                                    {
                                                        //interest Credit to GL 11
                                                        Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["InterestGl"].ToString(), DT.Rows[0]["InterestSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString())), "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());
                                                    }

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString()) > 0)
                                                        {
                                                            //Current Interest Debit To 2 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["InterestSub"].ToString(), CrAccNo.ToString(), "2", "2", "7", "Interest Debit", DT.Rows[0]["CurrInterest"].ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())) > 0)
                                                        {
                                                            //Current Interest Credit To 2 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["InterestSub"].ToString(), CrAccNo.ToString(), "2", "1", "7", "Interest Credit", Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString())).ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (Result > 0)
                                                    {
                                                        //interest Applied Contra
                                                        if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())) > 0)
                                                        {
                                                            //interest Applied Debit To GL 11
                                                            Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["InterestGl"].ToString(), DT.Rows[0]["InterestSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString())), "2", "11", "TR_INT", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                            if (Result > 0)
                                                            {
                                                                //interest Applied Credit to GL 100
                                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), "100", DT.Rows[0]["PlAccNo1"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString())), "1", "11", "TR_INT", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());
                                                            }
                                                        }
                                                    }

                                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())));
                                                }
                                                else if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                                {
                                                    //interest Credit to GL 11
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["InterestGl"].ToString(), DT.Rows[0]["InterestSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString()) > 0)
                                                        {
                                                            //Current Interest Debit To 2 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["InterestSub"].ToString(), CrAccNo.ToString(), "2", "2", "7", "Interest Debit", DT.Rows[0]["CurrInterest"].ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())) > 0)
                                                        {
                                                            //Current Interest Credit To 2 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["InterestSub"].ToString(), CrAccNo.ToString(), "2", "1", "7", "Interest Credit", TotalDrAmt.ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (Result > 0)
                                                    {
                                                        //interest Applied Contra
                                                        if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())) > 0)
                                                        {
                                                            //interest Applied Debit To GL 11
                                                            Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["InterestGl"].ToString(), DT.Rows[0]["InterestSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "2", "11", "TR_INT", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                            if (Result > 0)
                                                            {
                                                                //interest Applied Credit to GL 100
                                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), "100", DT.Rows[0]["PlAccNo1"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "11", "TR_INT", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());
                                                            }
                                                        }
                                                    }

                                                    TotalDrAmt = 0;
                                                }
                                            }
                                            else if (DT.Rows[0]["IntCalType"].ToString() == "2")
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())))
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())) > 0)
                                                    {
                                                        //interest Received Credit to GL 3
                                                        Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), CrGlCode.ToString(), CrSubGlCode.ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString())), "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());
                                                    }

                                                    //  Added As Per ambika mam Instruction 22-06-2017
                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())) > 0)
                                                        {
                                                            //interest Applied Debit To GL 3
                                                            Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), CrGlCode.ToString(), CrSubGlCode.ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString())), "2", "11", "TR_INT", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                            if (Result > 0)
                                                            {
                                                                //interest Applied Credit to GL 100
                                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), "100", DT.Rows[0]["PlAccNo1"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString())), "1", "11", "TR_INT", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());
                                                            }
                                                        }
                                                    }

                                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())));
                                                }
                                                else if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                                {
                                                    //interest Received Credit to GL 3
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), CrGlCode.ToString(), CrSubGlCode.ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    //  Added As Per ambika mam Instruction 22-06-2017
                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())) > 0)
                                                        {
                                                            //interest Applied Debit To GL 3
                                                            Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), CrGlCode.ToString(), CrSubGlCode.ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "2", "11", "TR_INT", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                            if (Result > 0)
                                                            {
                                                                //interest Applied Credit to GL 100
                                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), "100", DT.Rows[0]["PlAccNo1"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "11", "TR_INT", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());
                                                            }
                                                        }
                                                    }

                                                    TotalDrAmt = 0;
                                                }
                                            }
                                        }

                                        //Principle O/S Credit To Specific GL (e.g 3)
                                        if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Principle"].ToString()) < 0 ? "0" : DT.Rows[0]["Principle"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Principle"].ToString()) < 0 ? "0" : DT.Rows[0]["Principle"].ToString()))
                                        {
                                            Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), CrGlCode.ToString(), CrSubGlCode.ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(DT.Rows[0]["Principle"].ToString()), "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                            if (Result > 0)
                                            {
                                                //Current Principle Debit To 1 In AVS_LnTrx
                                                Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), CrSubGlCode.ToString(), CrAccNo.ToString(), "1", "2", "7", "Principle Debit", DT.Rows[0]["Principle"].ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                            }

                                            if (Result > 0)
                                            {
                                                //Current Principle Credit To 1 In AVS_LnTrx
                                                Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), CrSubGlCode.ToString(), CrAccNo.ToString(), "1", "1", "7", "Principle Credit", DT.Rows[0]["Principle"].ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                            }

                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Principle"].ToString()) < 0 ? "0" : DT.Rows[0]["Principle"].ToString()));
                                        }
                                        else if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Principle"].ToString()) < 0 ? "0" : DT.Rows[0]["Principle"].ToString()) > 0 && TotalDrAmt > 0)
                                        {
                                            Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), CrGlCode.ToString(), CrSubGlCode.ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                            if (Result > 0)
                                            {
                                                //Current Principle Debit To 1 In AVS_LnTrx
                                                Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), CrSubGlCode.ToString(), CrAccNo.ToString(), "1", "2", "7", "Principle Debit", DT.Rows[0]["Principle"].ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                            }

                                            if (Result > 0)
                                            {
                                                //Current Principle Credit To 1 In AVS_LnTrx
                                                Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), CrSubGlCode.ToString(), CrAccNo.ToString(), "1", "1", "7", "Principle Credit", TotalDrAmt.ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                            }

                                            TotalDrAmt = 0;
                                        }
                                    }
                                    else if (DT.Rows[0]["IntCalType"].ToString() == "3")
                                    {
                                        //Principle O/S Credit To Specific GL (e.g 3)
                                        Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), CrGlCode.ToString(), CrSubGlCode.ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                        if (Result > 0)
                                        {
                                            if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Principle"].ToString()) < 0 ? "0" : DT.Rows[0]["Principle"].ToString()) > 0)
                                            {
                                                //Current Principle Debit To 1 In AVS_LnTrx
                                                Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), CrSubGlCode.ToString(), CrAccNo.ToString(), "1", "2", "7", "Principle Debit", DT.Rows[0]["Principle"].ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());

                                                if (Result > 0)
                                                {
                                                    //Current Principle Credit To 1 In AVS_LnTrx
                                                    Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), CrSubGlCode.ToString(), CrAccNo.ToString(), "1", "1", "7", "Principle Credit", TotalDrAmt.ToString(), SetNo.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                }
                                            }
                                            //TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(dt.Rows[0]["Principle"].ToString()));
                                            PrincAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["InsChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["InsChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["BankChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["BankChrg"].ToString()) +
                                                Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["OtherChrg"].ToString()) + OtherChrg + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["SurChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CourtChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["CourtChrg"].ToString()) +
                                                Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["ServiceChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["ServiceChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["NoticeChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["NoticeChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["InterestRec"].ToString()) < 0 ? "0" : DT.Rows[0]["InterestRec"].ToString()) +
                                                Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString())) +
                                                Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())))) < 0 ? 0 :
                                                Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["InsChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["InsChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["BankChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["BankChrg"].ToString()) +
                                                Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["OtherChrg"].ToString()) + OtherChrg + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["SurChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CourtChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["CourtChrg"].ToString()) +
                                                Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["ServiceChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["ServiceChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["NoticeChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["NoticeChrg"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["InterestRec"].ToString()) < 0 ? "0" : DT.Rows[0]["InterestRec"].ToString()) +
                                                Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString())) +
                                                Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString()))));
                                            TotalDrAmt = Convert.ToDouble(TotalDrAmt - PrincAmt);
                                        }

                                        //Principle O/S Debit To Specific GL (e.g 3) And Credit to interest GL (e.g 11)
                                        if (Result > 0)
                                        {
                                            Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), CrGlCode.ToString(), CrSubGlCode.ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "2", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                            //  For Insurance Charge
                                            if (Result > 0)
                                            {
                                                if (Convert.ToDouble(DT.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(DT.Rows[0]["InsChrg"].ToString()))
                                                {
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["InsChrgGl"].ToString(), DT.Rows[0]["InsChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(DT.Rows[0]["InsChrg"].ToString()), "1", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(DT.Rows[0]["InsChrg"].ToString()) > 0)
                                                        {
                                                            //  Insurance Charge Credit To 11 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["InsChrgSub"].ToString(), CrAccNo.ToString(), "11", "1", "7", "Insurance Charge Credit", DT.Rows[0]["InsChrg"].ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(DT.Rows[0]["InsChrg"].ToString()));
                                                }
                                                else if (Convert.ToDouble(DT.Rows[0]["InsChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                                {
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["InsChrgGl"].ToString(), DT.Rows[0]["InsChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(DT.Rows[0]["InsChrg"].ToString()) > 0)
                                                        {
                                                            //  Insurance Charge Credit To 11 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["InsChrgSub"].ToString(), CrAccNo.ToString(), "11", "1", "7", "Insurance Charge Credit", TotalDrAmt.ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                    TotalDrAmt = 0;
                                                }
                                            }

                                            //  For Bank Charges
                                            if (Result > 0)
                                            {
                                                if (Convert.ToDouble(DT.Rows[0]["BankChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(DT.Rows[0]["BankChrg"].ToString()))
                                                {
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["BankChrgGl"].ToString(), DT.Rows[0]["BankChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(DT.Rows[0]["BankChrg"].ToString()), "1", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(DT.Rows[0]["BankChrg"].ToString()) > 0)
                                                        {
                                                            // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["BankChrgSub"].ToString(), CrAccNo.ToString(), "10", "1", "7", "Bank Charges Credit", DT.Rows[0]["BankChrg"].ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(DT.Rows[0]["BankChrg"].ToString()));
                                                }
                                                else if (Convert.ToDouble(DT.Rows[0]["BankChrg"].ToString()) > 0 && TotalDrAmt > 0 && TotalDrAmt > 0)
                                                {
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["BankChrgGl"].ToString(), DT.Rows[0]["BankChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(DT.Rows[0]["BankChrg"].ToString()) > 0)
                                                        {
                                                            // Bank Charges Amt Credit To 10 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["BankChrgSub"].ToString(), CrAccNo.ToString(), "10", "1", "7", "Bank Charges Credit", TotalDrAmt.ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                    TotalDrAmt = 0;
                                                }
                                            }

                                            //  For Other Charges
                                            if (Result > 0)
                                            {
                                                if ((Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0 && TotalDrAmt >= (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["OtherChrg"].ToString()) + OtherChrg))
                                                {
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["OtherChrgGl"].ToString(), DT.Rows[0]["OtherChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["OtherChrg"].ToString()) + OtherChrg), "1", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (OtherChrg > 0)
                                                        {
                                                            // Other Charges Amt Debit To 9 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["OtherChrgSub"].ToString(), CrAccNo.ToString(), "9", "2", "7", "Other Charges Debit", OtherChrg.ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                    if (Result > 0)
                                                    {
                                                        if ((Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0)
                                                        {
                                                            // Other Charges Amt Credit To 9 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["OtherChrgSub"].ToString(), CrAccNo.ToString(), "9", "1", "7", "Other Charges Credit", Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["OtherChrg"].ToString()) + OtherChrg).ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["OtherChrg"].ToString()) + OtherChrg));
                                                }
                                                else if ((Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0 && TotalDrAmt > 0)
                                                {
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["OtherChrgGl"].ToString(), DT.Rows[0]["OtherChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (OtherChrg > 0)
                                                        {
                                                            // Other Charges Amt Debit To 9 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["OtherChrgSub"].ToString(), CrAccNo.ToString(), "9", "2", "7", "Other Charges Debit", OtherChrg.ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                    if (Result > 0)
                                                    {
                                                        if ((Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["OtherChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["OtherChrg"].ToString()) + OtherChrg) > 0)
                                                        {
                                                            // Other Charges Amt Credit To 9 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["OtherChrgSub"].ToString(), CrAccNo.ToString(), "9", "1", "7", "Other Charges Credit", TotalDrAmt.ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                    TotalDrAmt = 0;
                                                }
                                            }

                                            //  For Sur Charges
                                            if (Result > 0)
                                            {
                                                if ((Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["SurChrg"].ToString()) < 0  ? "0" : DT.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0 && TotalDrAmt >= (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["SurChrg"].ToString()) < 0  ? "0" : DT.Rows[0]["SurChrg"].ToString()) + CurSurChrg))
                                                {
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["SurChrgGl"].ToString(), DT.Rows[0]["SurChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["SurChrg"].ToString()) + CurSurChrg), "1", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (CurSurChrg > 0)
                                                        {
                                                            // Sur Charges Debited To 8 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["SurChrgSub"].ToString(), CrAccNo.ToString(), "8", "2", "7", "Sur Charges Debited", CurSurChrg.ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                        if ((Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["SurChrg"].ToString()) < 0  ? "0" : DT.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0)
                                                        {
                                                            // Sur Charges Credit To 8 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["SurChrgSub"].ToString(), CrAccNo.ToString(), "8", "1", "7", "Sur Charges Credit", Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["SurChrg"].ToString()) < 0 ? "0" : DT.Rows[0]["SurChrg"].ToString()) + CurSurChrg).ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["SurChrg"].ToString()) < 0  ? "0" : DT.Rows[0]["SurChrg"].ToString()) + CurSurChrg));
                                                }
                                                else if ((Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["SurChrg"].ToString()) < 0  ? "0" : DT.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0 && TotalDrAmt > 0)
                                                {
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["SurChrgGl"].ToString(), DT.Rows[0]["SurChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (CurSurChrg > 0)
                                                        {
                                                            // Sur Charges Debited To 8 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["SurChrgSub"].ToString(), CrAccNo.ToString(), "8", "2", "7", "Sur Charges Debited", CurSurChrg.ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                        if ((Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["SurChrg"].ToString()) < 0  ? "0" : DT.Rows[0]["SurChrg"].ToString()) + CurSurChrg) > 0)
                                                        {
                                                            // Sur Charges Credit To 8 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["SurChrgSub"].ToString(), CrAccNo.ToString(), "8", "1", "7", "Sur Charges Credit", TotalDrAmt.ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                    TotalDrAmt = 0;
                                                }
                                            }

                                            // For Court Charges
                                            if (Result > 0)
                                            {
                                                if (Convert.ToDouble(DT.Rows[0]["CourtChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(DT.Rows[0]["CourtChrg"].ToString()))
                                                {
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["CourtChrgGl"].ToString(), DT.Rows[0]["CourtChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(DT.Rows[0]["CourtChrg"].ToString()), "1", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(DT.Rows[0]["CourtChrg"].ToString()) > 0)
                                                        {
                                                            // Court Charges Amt Credit To 7 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["CourtChrgSub"].ToString(), CrAccNo.ToString(), "7", "1", "7", "Court Charges Credit", DT.Rows[0]["CourtChrg"].ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(DT.Rows[0]["CourtChrg"].ToString()));
                                                }
                                                else if (Convert.ToDouble(DT.Rows[0]["CourtChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                                {
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["CourtChrgGl"].ToString(), DT.Rows[0]["CourtChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(DT.Rows[0]["CourtChrg"].ToString()) > 0)
                                                        {
                                                            // Court Charges Amt Credit To 7 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["CourtChrgSub"].ToString(), CrAccNo.ToString(), "7", "1", "7", "Court Charges Credit", TotalDrAmt.ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                    TotalDrAmt = 0;
                                                }
                                            }

                                            //  For Service Charges
                                            if (Result > 0)
                                            {
                                                if (Convert.ToDouble(DT.Rows[0]["ServiceChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(DT.Rows[0]["ServiceChrg"].ToString()))
                                                {
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["ServiceChrgGl"].ToString(), DT.Rows[0]["ServiceChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(DT.Rows[0]["ServiceChrg"].ToString()), "1", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(DT.Rows[0]["ServiceChrg"].ToString()) > 0)
                                                        {
                                                            // Service Charges Amt Credit To 6 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["ServiceChrgSub"].ToString(), CrAccNo.ToString(), "6", "1", "7", "Service Charges Credit", DT.Rows[0]["ServiceChrg"].ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(DT.Rows[0]["ServiceChrg"].ToString()));
                                                }
                                                else if (Convert.ToDouble(DT.Rows[0]["ServiceChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                                {
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["ServiceChrgGl"].ToString(), DT.Rows[0]["ServiceChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(DT.Rows[0]["ServiceChrg"].ToString()) > 0)
                                                        {
                                                            // Service Charges Amt Credit To 6 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["ServiceChrgSub"].ToString(), CrAccNo.ToString(), "6", "1", "7", "Service Charges Credit", TotalDrAmt.ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                    TotalDrAmt = 0;
                                                }
                                            }

                                            //  For Notice Charges
                                            if (Result > 0)
                                            {
                                                if (Convert.ToDouble(DT.Rows[0]["NoticeChrg"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(DT.Rows[0]["NoticeChrg"].ToString() == "" ? "0" : DT.Rows[0]["NoticeChrg"].ToString()))
                                                {
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["NoticeChrgGl"].ToString(), DT.Rows[0]["NoticeChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(DT.Rows[0]["NoticeChrg"].ToString()), "1", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(DT.Rows[0]["NoticeChrg"].ToString()) > 0)
                                                        {
                                                            // Notice Charges Credit To 5 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["NoticeChrgSub"].ToString(), CrAccNo.ToString(), "5", "1", "7", "Notice Charges Credit", DT.Rows[0]["NoticeChrg"].ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(DT.Rows[0]["NoticeChrg"].ToString()));
                                                }
                                                else if (Convert.ToDouble(DT.Rows[0]["NoticeChrg"].ToString()) > 0 && TotalDrAmt > 0)
                                                {
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["NoticeChrgGl"].ToString(), DT.Rows[0]["NoticeChrgSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(DT.Rows[0]["NoticeChrg"].ToString()) > 0)
                                                        {
                                                            // Notice Charges Credit To 5 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["NoticeChrgSub"].ToString(), CrAccNo.ToString(), "5", "1", "7", "Notice Charges Credit", TotalDrAmt.ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                    TotalDrAmt = 0;
                                                }
                                            }

                                            //  For Interest Receivable
                                            if (Result > 0)
                                            {
                                                if (Convert.ToDouble(DT.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt >= Convert.ToDouble(DT.Rows[0]["InterestRec"].ToString()))
                                                {
                                                    // Interest Received credit to GL 11
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["InterestRecGl"].ToString(), DT.Rows[0]["InterestRecSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(DT.Rows[0]["InterestRec"].ToString()), "1", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(DT.Rows[0]["InterestRec"].ToString()) > 0)
                                                        {
                                                            // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["InterestRecSub"].ToString(), CrAccNo.ToString(), "4", "1", "7", "Interest Received Credit", DT.Rows[0]["InterestRec"].ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(DT.Rows[0]["InterestRec"].ToString()));
                                                }
                                                else if (Convert.ToDouble(DT.Rows[0]["InterestRec"].ToString()) > 0 && TotalDrAmt > 0)
                                                {
                                                    // Interest Received credit to GL 11
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["InterestRecGl"].ToString(), DT.Rows[0]["InterestRecSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(DT.Rows[0]["InterestRec"].ToString()), "1", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", CrCustNo.ToString(), "SBTOLN", "", "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(DT.Rows[0]["InterestRec"].ToString()) > 0)
                                                        {
                                                            // Interest Received Amt Credit To 4 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["InterestRecSub"].ToString(), CrAccNo.ToString(), "4", "1", "7", "Interest Received Credit", TotalDrAmt.ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }
                                                    TotalDrAmt = 0;
                                                }
                                            }

                                            //  For Penal Charge
                                            if (Result > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString())))
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                    {
                                                        //Penal Charge Credit To GL 12
                                                        Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["PInterestGl"].ToString(), DT.Rows[0]["PInterestSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString())), "1", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());
                                                    }

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString()) > 0)
                                                        {
                                                            //Penal Interest Debit To 3 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["PInterestSub"].ToString(), CrAccNo.ToString(), "3", "2", "7", "Penal Interest Debit", DT.Rows[0]["CurrPInterest"].ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                        {
                                                            //Penal Interest Credit To 3 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["PInterestSub"].ToString(), CrAccNo.ToString(), "3", "1", "7", "Penal Interest Credit", Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString())).ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (Result > 0)
                                                    {
                                                        //Penal Charge Contra
                                                        if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                        {
                                                            //Penal chrg Applied Debit To GL 12
                                                            Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["PInterestGl"].ToString(), DT.Rows[0]["PInterestSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString())), "2", "12", "TR_INT", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                            if (Result > 0)
                                                            {
                                                                //Penal chrg Applied Credit to GL 100
                                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), "100", DT.Rows[0]["PlAccNo2"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString())), "1", "12", "TR_INT", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());
                                                            }
                                                        }
                                                    }

                                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString())));
                                                }
                                                else if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                                {
                                                    //Penal Charge Credit To GL 12
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["PInterestGl"].ToString(), DT.Rows[0]["PInterestSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString()) > 0)
                                                        {
                                                            //Penal Interest Debit To 3 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["PInterestSub"].ToString(), CrAccNo.ToString(), "3", "2", "7", "Penal Interest Debit", DT.Rows[0]["CurrPInterest"].ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                        {
                                                            //Penal Interest Credit To 3 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["PInterestSub"].ToString(), CrAccNo.ToString(), "3", "1", "7", "Penal Interest Credit", Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString())).ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (Result > 0)
                                                    {
                                                        //Penal Charge Contra
                                                        if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["PInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["PInterest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrPInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrPInterest"].ToString())) > 0)
                                                        {
                                                            //Penal chrg Applied Debit To GL 12
                                                            Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["PInterestGl"].ToString(), DT.Rows[0]["PInterestSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "2", "12", "TR_INT", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                            if (Result > 0)
                                                            {
                                                                //Penal chrg Applied Credit to GL 100
                                                                Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), "100", DT.Rows[0]["PlAccNo2"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "12", "TR_INT", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());
                                                            }
                                                        }
                                                    }

                                                    TotalDrAmt = 0;
                                                }
                                            }

                                            //  For Interest
                                            if (Result > 0)
                                            {
                                                if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt >= Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())))
                                                {
                                                    if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())) > 0)
                                                    {
                                                        //interest Credit to GL 11
                                                        Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["InterestGl"].ToString(), DT.Rows[0]["InterestSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString())), "1", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());
                                                    }

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString()) > 0)
                                                        {
                                                            //Current Interest Debit To 2 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["InterestSub"].ToString(), CrAccNo.ToString(), "2", "2", "7", "Interest Debit", DT.Rows[0]["CurrInterest"].ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())) > 0)
                                                        {
                                                            //Current Interest Credit To 2 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["InterestSub"].ToString(), CrAccNo.ToString(), "2", "1", "7", "Interest Credit", Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString())).ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    TotalDrAmt = Convert.ToDouble(TotalDrAmt - Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())));
                                                }
                                                else if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())) > 0 && TotalDrAmt > 0)
                                                {
                                                    //interest Credit to GL 11
                                                    Result = ITrans.Authorized(EDate.ToString(), EDate.ToString(), EDate.ToString(), DT.Rows[0]["InterestGl"].ToString(), DT.Rows[0]["InterestSub"].ToString(), CrAccNo, Parti1.ToString(), Parti2.ToString(), TotalDrAmt, "1", "7", "TR", SetNo1, "", "", "0", "0", "1003", "", Session["BRCD"].ToString(), Session["MID"].ToString(), "", "", "SBTOLN", CrCustNo.ToString(), "", ViewState["RID"].ToString());

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString()) > 0)
                                                        {
                                                            //Current Interest Debit To 2 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["InterestSub"].ToString(), CrAccNo.ToString(), "2", "2", "7", "Interest Debit", DT.Rows[0]["CurrInterest"].ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    if (Result > 0)
                                                    {
                                                        if (Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["Interest"].ToString()) < 0 ? "0" : DT.Rows[0]["Interest"].ToString()) + Convert.ToDouble(Convert.ToDouble(DT.Rows[0]["CurrInterest"].ToString()) < 0 ? "0" : DT.Rows[0]["CurrInterest"].ToString())) > 0)
                                                        {
                                                            //Current Interest Credit To 2 In AVS_LnTrx
                                                            Result = ITrans.LoanTrx(Session["BRCD"].ToString(), CrSubGlCode.ToString(), DT.Rows[0]["InterestSub"].ToString(), CrAccNo.ToString(), "2", "1", "7", "Interest Credit", TotalDrAmt.ToString(), SetNo1.ToString(), "1003", Session["MID"].ToString(), "0", Session["EntryDate"].ToString(), ViewState["RID"].ToString());
                                                        }
                                                    }

                                                    TotalDrAmt = 0;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (Result > 0)
                            {
                                SI.UpdateNextShedule(Session["BRCD"].ToString(), CrSubGlCode, CrAccNo, txtPostDate.Text.ToString(), Session["EntryDate"].ToString());
                            }
                        }

                        if (Result > 0)
                        {
                            WebMsgBox.Show("Successfully Post With SetNo : " + SetNo + "....!", this.Page);
                            return;
                        }
                    }
                }
                else if (ddlOperation.SelectedValue == "2")
                {
                    Flag2 = "Trail";

                    string redirectURL = "FrmReportViewer.aspx?BRCD=" + Session["BRCD"].ToString() + "&EDate=" + txtPostDate.Text.Trim().ToString() + "&WDate=" + Session["EntryDate"].ToString() + "&MID=" + Session["MID"].ToString() + "&Flag1=" + Flag1.ToString() + "&Flag2=" + Flag2.ToString() + "&rptname=RptAVS5036.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
            else if (rbtnSIType.SelectedValue == "3")
            {
                Flag1 = "DDSTOLN";

                if (ddlOperation.SelectedValue == "1")
                {
                    Flag2 = "Post";
                }
                else if (ddlOperation.SelectedValue == "2")
                {
                    Flag2 = "Trail";

                    string redirectURL = "FrmReportViewer.aspx?BRCD=" + Session["BRCD"].ToString() + "&EDate=" + txtPostDate.Text.Trim().ToString() + "&EDate=" + Session["EntryDate"].ToString() + "&MID=" + Session["MID"].ToString() + "&Flag1=" + Flag1.ToString() + "&Flag2=" + Flag2.ToString() + "&rptname=RptAVS5036.rdlc";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string redirectURL = "";

            if (rbtnSIType.SelectedValue == "1")
                redirectURL = "FrmRView.aspx?GlCode=1&BRCD=" + Session["BRCD"].ToString() + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptDDSToLoan.rdlc";
            else if (rbtnSIType.SelectedValue == "2")
                redirectURL = "FrmRView.aspx?GlCode=2&BRCD=" + Session["BRCD"].ToString() + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptDDSToLoan.rdlc";
            else if (rbtnSIType.SelectedValue == "3")
                redirectURL = "FrmRView.aspx?GlCode=1&BRCD=" + Session["BRCD"].ToString() + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptDDSToLoan.rdlc";

            //string redirectURL = "FrmRView.aspx?GlCode=" + glcode + "&BRCD=" + Session["BRCD"].ToString() + "&UserName=" + Session["UserName"].ToString() + "&rptname=RptDDSToLoan.rdlc";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + redirectURL + "','_blank')", true);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            rbtnSIType.SelectedValue = "0";
            ddlOperation.SelectedValue = "0";
            txtPostDate.Text = "";
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

}