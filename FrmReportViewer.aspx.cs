using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using Microsoft.ReportingServices;
using System.Data.SqlClient;

public partial class FrmReportViewer : System.Web.UI.Page
{
    public static string RptName;
    string BankName, BranchName;
    string BRCD, FBRCD, TBRCD, PRCD, GLCD, ACNO, RecNo, FPRCD, TPRCD, CustNo, AppNo, FACCNO, TACCNO, EDate, WDate,Div,Dep;
    string OnDate, UName, EDT, Fsubgl, Tsubgl, SL;
    string fileName, InstAmt, IRate, CHG;
    string FLAG1, FLAG2, FBKcode, TBKcode, FDate, TDate, UserName, Mid, MM, YY, FMM, FYY, TMM, TYY, BKCD;
    string LAMT, PERIOD, RATE, REPAYAMT, STARTDATE, PERIODTYPE, FLG, ASONDT, MID, SKIP_DIGIT;
    ClsFDIntCalculation FFD = new ClsFDIntCalculation();
    ClsLoanBalanceReport LB = new ClsLoanBalanceReport();
    ClsSBIntCalculation SB = new ClsSBIntCalculation();
    ClsSBIntCalcSum SBS = new ClsSBIntCalcSum();
    ClsShareAllotTransaction SA = new ClsShareAllotTransaction();
    ClsLoanOverdue LD = new ClsLoanOverdue();
    ClsLogin LG = new ClsLogin();
    ClsScrutinySheet SS = new ClsScrutinySheet();
    ClsSavingIntCal SIC = new ClsSavingIntCal();
    ClsAppliedInterest IA = new ClsAppliedInterest();
    ClsAVS5050 DDS = new ClsAVS5050();
    ClsAVS5036 SI = new ClsAVS5036();
    ClsAVS5070 SSA = new ClsAVS5070();
    ClsEMIEnquiry EE = new ClsEMIEnquiry();
    ClsStatementView SV = new ClsStatementView();
    ClsDlyCashPos CP = new ClsDlyCashPos();
    ClsPTRegister PT = new ClsPTRegister();
    ClsAVS5072 PR = new ClsAVS5072();
    ClsRDIntCalc RDC = new ClsRDIntCalc();
    ClsAVS5153 CR = new ClsAVS5153();
    ClsBalvikasIntCalc BVC = new ClsBalvikasIntCalc();
    ClsUtkarshIntCalc UT = new ClsUtkarshIntCalc();
    ClsAVS5147 FC = new ClsAVS5147();
    ClsInvData INV = new ClsInvData();
    ClsAVS5111 DAP = new ClsAVS5111();
    ClsAVS5152 PI = new ClsAVS5152();
    ClsAVS5170 Para = new ClsAVS5170();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RptName = Request.QueryString["rptname"].ToString();

            if (RptName == "BalvikasCalcReport")
            {
                if (Request.QueryString["FL"] != null)
                {
                    FLAG1 = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRD"] != null)
                {
                    FPRCD = Request.QueryString["FPRD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRD"] != null)
                {
                    TPRCD = Request.QueryString["TPRD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDate = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FACCNO"] != null)
                {
                    FACCNO = Request.QueryString["FACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TACCNO"] != null)
                {
                    TACCNO = Request.QueryString["TACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MID"] != null)
                {
                    MID = Request.QueryString["MID"].ToString().Replace("%27", "");
                }

            }
            if (RptName == "RptPTRegister.rdlc")
            {
                if (Request.QueryString["FL"] != null)
                {
                    FLAG1 = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UID"] != null)
                {
                    UserName = Request.QueryString["UID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MM"] != null)
                {
                    MM = Request.QueryString["MM"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["YY"] != null)
                {
                    YY = Request.QueryString["YY"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ASONDATE"] != null)
                {
                    EDT = Request.QueryString["ASONDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BANKCD"] != null)
                {
                    BKCD = Request.QueryString["BANKCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Div"] != null)
                {
                    Div = Request.QueryString["Div"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Dep"] != null)
                {
                    Dep = Request.QueryString["Dep"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "BalvikasTally")
            {
                if (Request.QueryString["FL"] != null)
                {
                    FLAG1 = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRD"] != null)
                {
                    FPRCD = Request.QueryString["FPRD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRD"] != null)
                {
                    TPRCD = Request.QueryString["TPRD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDate = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FACCNO"] != null)
                {
                    FACCNO = Request.QueryString["FACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TACCNO"] != null)
                {
                    TACCNO = Request.QueryString["TACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MID"] != null)
                {
                    MID = Request.QueryString["MID"].ToString().Replace("%27", "");
                }

            }
            if (RptName == "RptRdIntCalcTally.rdlc")
            {
                if (Request.QueryString["FL"] != null)
                {
                    FLAG1 = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PRCD"] != null)
                {
                    PRCD = Request.QueryString["PRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDate = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FACCNO"] != null)
                {
                    FACCNO = Request.QueryString["FACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TACCNO"] != null)
                {
                    TACCNO = Request.QueryString["TACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MID"] != null)
                {
                    MID = Request.QueryString["MID"].ToString().Replace("%27", "");
                }

            }

          
          
          

            //FD INT Calculation Report
         

            if (RptName == "RptEmiChart.rdlc")
            {
                if (Request.QueryString["LoanAmt"] != null)
                {
                    LAMT = Request.QueryString["LoanAmt"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Period"] != null)
                {
                    PERIOD = Request.QueryString["Period"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Rate"] != null)
                {
                    RATE = Request.QueryString["Rate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["StartDate"] != null)
                {
                    STARTDATE = Request.QueryString["StartDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PeriodType"] != null)
                {
                    PERIODTYPE = Request.QueryString["PeriodType"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["RepayAmount"] != null)
                {
                    REPAYAMT = Request.QueryString["RepayAmount"].ToString().Replace("%27", "");
                }
            }

            if (RptName == "RptLoanBalanceList_Pen.rdlc")
            {
                if (Request.QueryString["Date"] != null)
                {
                    OnDate = Request.QueryString["Date"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Fbrcd"] != null)
                {
                    FBRCD = Request.QueryString["Fbrcd"].ToString();
                }
                if (Request.QueryString["Tbrcd"] != null)
                {
                    TBRCD = Request.QueryString["Tbrcd"].ToString();
                }
                if (Request.QueryString["EntryDate"] != null)
                {
                    EDT = Request.QueryString["EntryDate"].ToString();
                }
                if (Request.QueryString["FSUBGL"] != null)
                {
                    Fsubgl = Request.QueryString["FSUBGL"].ToString();
                }
                if (Request.QueryString["TSUBGL"] != null)
                {
                    Tsubgl = Request.QueryString["TSUBGL"].ToString();
                }
                if (Request.QueryString["SL"] != null)
                {
                    SL = Request.QueryString["SL"].ToString();
                }

            }

            //  Added By amol Scrutiny Sheet Report
            if (RptName == "RptScrutinySheet.rdlc")
            {
                if (Request.QueryString["BC"] != null)
                {
                    BRCD = Request.QueryString["BC"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["CN"] != null)
                {
                    CustNo = Request.QueryString["CN"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AN"] != null)
                {
                    AppNo = Request.QueryString["AN"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ED"] != null)
                {
                    EDate = Request.QueryString["ED"].ToString().Replace("%27", "");
                }
            }

            //  Added By Amol Saving Interest Calculation Report
            if (RptName == "rptSavingIntCalculation.rdlc")
            {
                if (Request.QueryString["Flag1"] != null)
                {
                    FLAG1 = Request.QueryString["Flag1"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Flag2"] != null)
                {
                    FLAG2 = Request.QueryString["Flag2"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PRCD"] != null)
                {
                    PRCD = Request.QueryString["PRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FACCNO"] != null)
                {
                    FACCNO = Request.QueryString["FACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TACCNO"] != null)
                {
                    TACCNO = Request.QueryString["TACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
            }

            //  Added By Amol Applied Interest Report
            if (RptName == "RptAppliedInterest.rdlc" || RptName == "RptAppliedInterest_Parali.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["GLCD"] != null)
                {
                    GLCD = Request.QueryString["GLCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PRCD"] != null)
                {
                    PRCD = Request.QueryString["PRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EDATE"] != null)
                {
                    EDate = Request.QueryString["EDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TYPE"] != null)
                {
                    FLAG1 = Request.QueryString["TYPE"].ToString().Replace("%27", "");
                }
            }

            //  Added By Amol DDS Interest Calculation Report
            if (RptName == "RptAVS5050.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRCD"] != null)
                {
                    TPRCD = Request.QueryString["TPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FACCNO"] != null)
                {
                    FACCNO = Request.QueryString["FACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TACCNO"] != null)
                {
                    TACCNO = Request.QueryString["TACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MID"] != null)
                {
                    Mid = Request.QueryString["MID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FLAG"] != null)
                {
                    FLAG1 = Request.QueryString["FLAG"].ToString().Replace("%27", "");
                }
            }

            //  Added By Amol for customer report
           
                else if (FLG.ToString() == "2")
                {
                    if (Request.QueryString["FD"] != null)
                    {
                        FDate = Request.QueryString["FD"].ToString();
                        ASONDT = Request.QueryString["FD"].ToString();
                    }
                    if (Request.QueryString["TD"] != null)
                    {
                        TDate = Request.QueryString["TD"].ToString();
                    }
                }


                if (Request.QueryString["FD"] != null)
                {
                    FDate = Request.QueryString["FD"].ToString();
                }
                if (Request.QueryString["TD"] != null)
                {
                    TDate = Request.QueryString["TD"].ToString();
                }
            }


            //  Added By Amol SB Interest Calculation Report
            if (RptName == "RptSBIntCalculation.rdlc")
            {
                if (Request.QueryString["BC"] != null)
                {
                    BRCD = Request.QueryString["BC"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PC"] != null)
                {
                    PRCD = Request.QueryString["PC"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FD"] != null)
                {
                    FDate = Request.QueryString["FD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TD"] != null)
                {
                    TDate = Request.QueryString["TD"].ToString().Replace("%27", "");
                }
            }

            //  Added By Amol SB Interest Calculation Summary Report
            if (RptName == "RptSBIntCalcSum.rdlc")
            {
                if (Request.QueryString["BC"] != null)
                {
                    BRCD = Request.QueryString["BC"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PC"] != null)
                {
                    PRCD = Request.QueryString["PC"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AD"] != null)
                {
                    ASONDT = Request.QueryString["AD"].ToString().Replace("%27", "");
                }
            }

            //  Added By Amol For SB to RD (SI Post) Report
            if (RptName == "RptAVS5036.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EDate"] != null)
                {
                    EDate = Request.QueryString["EDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["WDate"] != null)
                {
                    WDate = Request.QueryString["WDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MID"] != null)
                {
                    Mid = Request.QueryString["MID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FLAG1"] != null)
                {
                    FLAG1 = Request.QueryString["FLAG1"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FLAG2"] != null)
                {
                    FLAG2 = Request.QueryString["FLAG2"].ToString().Replace("%27", "");
                }
            }

            //  Added By Amol For Statement View Report
           
            if (RptName == "RptInvStatementView.rdlc")
            {
                if (Request.QueryString["BC"] != null)
                {
                    BRCD = Request.QueryString["BC"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["GC"] != null)
                {
                    GLCD = Request.QueryString["GC"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PC"] != null)
                {
                    PRCD = Request.QueryString["PC"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AN"] != null)
                {
                    ACNO = Request.QueryString["AN"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FD"] != null)
                {
                    FDate = Request.QueryString["FD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TD"] != null)
                {
                    TDate = Request.QueryString["TD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["LAMT"] != null)
                {
                    LAMT = Request.QueryString["LAMT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["InstAmt"] != null)
                {
                    InstAmt = Request.QueryString["InstAmt"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["IRate"] != null)
                {
                    IRate = Request.QueryString["IRate"].ToString().Replace("%27", "");
                }
            }
            
            //  Added By Amol Share allotment transaction's Report
            if (RptName == "RptShareAllotTransaction.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
            }

            //  Added By Amol Loan balance Report
           

            //  Added By Amol Share Suspense Report
            if (RptName == "RptAVS5070.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDate = Request.QueryString["FDATE"].ToString();
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString();
                }
            }


            if (RptName == "RptPTRegister.rdlc")
            {
                if (Request.QueryString["FL"] != null)
                {
                    FLAG1 = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UID"] != null)
                {
                    UserName = Request.QueryString["UID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MM"] != null)
                {
                    MM = Request.QueryString["MM"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["YY"] != null)
                {
                    YY = Request.QueryString["YY"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ASONDATE"] != null)
                {
                    EDT = Request.QueryString["ASONDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BANKCD"] != null)
                {
                    BKCD = Request.QueryString["BANKCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Div"] != null)
                {
                    Div = Request.QueryString["Div"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Dep"] != null)
                {
                    Dep = Request.QueryString["Dep"].ToString().Replace("%27", "");
                }
            }

            if (RptName == "RptPendingRec.rdlc")
            {
                if (Request.QueryString["FL"] != null)
                {
                    FLAG1 = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UID"] != null)
                {
                    UserName = Request.QueryString["UID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FMM"] != null)
                {
                    FMM = Request.QueryString["FMM"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["FYY"] != null)
                {
                    FYY = Request.QueryString["FYY"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TMM"] != null)
                {
                    TMM = Request.QueryString["TMM"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["TYY"] != null)
                {
                    TYY = Request.QueryString["TYY"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ASONDATE"] != null)
                {
                    EDT = Request.QueryString["ASONDATE"].ToString().Replace("%27", "");
                }
            }

            //For Daily cash Position with Denomination
            else if (RptName == "rptDailyCashPosition.rdlc")
            {
                if (Request.QueryString["BC"] != null)
                {
                    BRCD = Request.QueryString["BC"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["DT"] != null)
                {
                    EDate = Request.QueryString["DT"].ToString().Replace("%27", "");
                }
            }

            //  Added by amol for Folio charges report
            if (RptName == "RptAVS5147.rdlc")
            {
                if (Request.QueryString["FB"] != null)
                {
                    FBRCD = Request.QueryString["FB"].ToString();
                }
                if (Request.QueryString["TB"] != null)
                {
                    TBRCD = Request.QueryString["TB"].ToString();
                }
                if (Request.QueryString["DP"] != null)
                {
                    FPRCD = Request.QueryString["DP"].ToString();
                }
                if (Request.QueryString["CP"] != null)
                {
                    TPRCD = Request.QueryString["CP"].ToString();
                }
                if (Request.QueryString["CHG"] != null)
                {
                    CHG = Request.QueryString["CHG"].ToString();
                }
                if (Request.QueryString["Part"] != null)
                {
                    FLAG1 = Request.QueryString["Part"].ToString();
                }
                if (Request.QueryString["ED"] != null)
                {
                    EDate = Request.QueryString["ED"].ToString();
                }
            }

            //  Added By Amol daily passbok Report
          
            //  Added By Amol for penal interest calculation report
            if (RptName == "RptAVS5152.rdlc")
            {
                if (Request.QueryString["Flag"] != null)
                {
                    FLG = Request.QueryString["Flag"].ToString();
                } 
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["PRCD"] != null)
                {
                    PRCD = Request.QueryString["PRCD"].ToString();
                }
                if (Request.QueryString["FD"] != null)
                {
                    FDate = Request.QueryString["FD"].ToString();
                }
                if (Request.QueryString["TD"] != null)
                {
                    TDate = Request.QueryString["TD"].ToString();
                }
                if (Request.QueryString["Flag1"] != null)
                {
                    FLAG1 = Request.QueryString["Flag1"].ToString();
                }
                if (Request.QueryString["ROI"] != null)
                {
                    IRate = Request.QueryString["ROI"].ToString();
                }
                if (Request.QueryString["Flag2"] != null)
                {
                    FLAG2 = Request.QueryString["Flag2"].ToString();
                }
            }

            //  Added By Amol for Interest calculation paraeter report
            if (RptName == "RptAVS5170.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
            }

            DataSet thisDataSet1 = new DataSet();
            DataSet thisDataSet2 = new DataSet();
            DataSet thisDataSet3 = new DataSet();
            DataSet thisDataSet4 = new DataSet();


            if (RptName == "BalvikasCalcReport")
            {
                DataTable dtEmployeeRD = new DataTable();
                dtEmployeeRD = BVC.GetReportCalc(FLAG1, BRCD, FPRCD, TPRCD, MID);
                thisDataSet1.Tables.Add(dtEmployeeRD);

                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
                else
                {
                    RptName = "RptRdIntCalcReport.rdlc";
                }
            }
            if (RptName == "RptRdIntCalcTally.rdlc")
            {
                DataTable dtEmployeeRD = new DataTable();
                dtEmployeeRD = RDC.GetReportTally(FLAG1, FBRCD, TBRCD, PRCD, TDate, MID);
                thisDataSet1.Tables.Add(dtEmployeeRD);

                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

          
            if (RptName == "RptPTRegister.rdlc")
            {
                thisDataSet1 = PT.GetPtRegister(FLAG1, EDT, BRCD, MM, YY, BKCD, Div, Dep);
                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
           
           

           
            if (RptName == "BalvikasTally")
            {
                DataTable dtEmployeeRD = new DataTable();
                dtEmployeeRD = BVC.GetReportTally(FLAG1, BRCD, FPRCD, TPRCD, TDate, MID);
                thisDataSet1.Tables.Add(dtEmployeeRD);

                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
                else
                {
                    RptName = "RptRdIntCalcTally.rdlc";
                }

            }
            //  Added By amol Scrutiny Sheet Report
            if (RptName == "RptEmiChart.rdlc")
            {
                thisDataSet1 = EE.GetLoanEnquiryReport(Convert.ToDouble(LAMT), Convert.ToDouble(RATE), Convert.ToDouble(PERIOD), STARTDATE, PERIODTYPE);
                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //  Added By amol Scrutiny Sheet Report
            if (RptName == "RptScrutinySheet.rdlc")
            {
                thisDataSet1 = SS.GetScrutinySheet1(BRCD, CustNo, AppNo, EDate);
                thisDataSet2 = SS.GetScrutinySheet2(BRCD, CustNo, AppNo, EDate);

                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //  Added By Amol Saving Interest Calculation Report
            if (RptName == "rptSavingIntCalculation.rdlc")
            {
                thisDataSet1 = SIC.GetSavingIntCal(FLAG1, FLAG2, FBRCD, TBRCD, PRCD, FACCNO, TACCNO, FDate, TDate, Session["EntryDate"].ToString(), Session["MID"].ToString());
                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //  Added By Amol Applied Interest Report
            if (RptName == "RptAppliedInterest.rdlc" || RptName == "RptAppliedInterest_Parali.rdlc")
            {
                thisDataSet1 = IA.GetSavingIntCal(BRCD, GLCD, PRCD, EDate, FLAG1);
                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //  Added By Amol DDS Interest Calculation Report
            if (RptName == "RptAVS5050.rdlc")
            {
                thisDataSet1 = DDS.GetDDSIntCal(BRCD, FPRCD, TPRCD, FACCNO, TACCNO, FDate, TDate, Mid, FLAG1);
                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            //  Added By Amol for customer report
        

            //  Added By Amol SB Interest Calculation Report
            if (RptName == "RptSBIntCalculation.rdlc")
            {
                thisDataSet1 = SB.GetSBIntCal(BRCD, PRCD, FDate, TDate, Session["MID"].ToString());
                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //  Added By Amol SB Interest Calculation Summary Report
            if (RptName == "RptSBIntCalcSum.rdlc")
            {
                thisDataSet1 = SBS.GetSBIntCalSum(BRCD, PRCD, ASONDT, Session["MID"].ToString(), FLAG1);
                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptLoanBalanceList_Pen.rdlc")
            {
                thisDataSet1 = LD.GetLoanBalanceListPen(EDT, FBRCD, TBRCD, OnDate, Fsubgl, Tsubgl, "LB", SL);
                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
                else
                {
                    RptName = "RptLoanBalanceList_Pen.rdlc";
                }
            }

            //  Added By Amol For SB to RD (SI Post) Report
            if (RptName == "RptAVS5036.rdlc")
            {
                thisDataSet1 = SI.GetSBTORDCal(BRCD, EDate, WDate, Mid, FLAG1, FLAG2);
                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //  Added By Amol For Statement View Report
           
            if (RptName == "RptInvStatementView.rdlc")
            {
                thisDataSet1 = INV.GetStmtView(BRCD, GLCD, PRCD, ACNO, FDate, TDate);
                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            
            //  Added By Amol Share allotment transaction's Report
            if (RptName == "RptShareAllotTransaction.rdlc")
            {
                thisDataSet1 = SA.GetAllotTrans(BRCD, FDate, TDate);
                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

           

            //  Added By Amol Share Suspense Report
            if (RptName == "RptAVS5070.rdlc")
            {
                thisDataSet1 = SSA.GetShareSusData(BRCD, FDate, TDate);
                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptPTRegister.rdlc")
            {
                thisDataSet1 = PT.GetPtRegister(FLAG1, EDT, BRCD, MM, YY, BKCD,Div,Dep);
                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptPendingRec.rdlc")
            {
                thisDataSet1 = PR.GetReport(FLAG1, EDT, BRCD, FMM, FYY, TMM, TYY);
                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //For Daily cash Position with Denomination
            else if (RptName == "rptDailyCashPosition.rdlc")
            {
                thisDataSet1 = CP.GetDailyCashPos(BRCD, EDate, "OPCL");
                thisDataSet2 = CP.GetDailyCashPos(BRCD, EDate, "DLYCSH");
                if ((thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0) || (thisDataSet2 == null || thisDataSet2.Tables[0].Rows.Count == 0))
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //  Added by amol for Folio charges report
            if (RptName == "RptAVS5147.rdlc")
            {
                thisDataSet1 = FC.TrailRun(FBRCD, TBRCD, FPRCD, CHG, TPRCD, FLAG1, EDate, Session["MID"].ToString());
                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

              

            //  Added By Amol for penal interest calculation report
            if (RptName == "RptAVS5152.rdlc")
            {
                thisDataSet1 = PI.TrailReport(FLG, BRCD, PRCD, FDate, TDate, FLAG1, IRate,FLAG2, Session["MID"].ToString());
                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //  Added By Amol for Interest calculation paraeter report
            if (RptName == "RptAVS5170.rdlc")
            {
                thisDataSet1 = Para.ParameterReport(BRCD);
                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            ReportDataSource DataSource1 = new ReportDataSource("DataSet1", thisDataSet1.Tables["Table1"]);
            ReportDataSource DataSource2 = new ReportDataSource("DataSet2", thisDataSet2.Tables["Table1"]);
            ReportDataSource DataSource3 = new ReportDataSource("DataSet3", thisDataSet3.Tables["Table1"]);
            ReportDataSource DataSource4 = new ReportDataSource("DataSet4", thisDataSet4.Tables["Table1"]);

            RdlcPrint.LocalReport.ReportPath = Server.MapPath("~/" + RptName + "");
            RdlcPrint.LocalReport.DataSources.Clear();
            RdlcPrint.LocalReport.DataSources.Add(DataSource1);

            if (RptName == "RptScrutinySheet.rdlc")
            {
                RdlcPrint.LocalReport.DataSources.Add(DataSource1);
                RdlcPrint.LocalReport.DataSources.Add(DataSource2);
            }
            if (RptName == "rptDailyCashPosition.rdlc")
            {
                RdlcPrint.LocalReport.DataSources.Add(DataSource1);
                RdlcPrint.LocalReport.DataSources.Add(DataSource2);
            }

            RdlcPrint.LocalReport.Refresh();
            RdlcPrint.Visible = true;

            DataTable DT = new DataTable();
            if ((RptName == "RptAVS5152.rdlc") || (RptName == "RptAVS5170.rdlc"))
            {
                DT = LG.GetBankName(BRCD.ToString());
                if (DT.Rows.Count > 0)
                {
                    BankName = DT.Rows[0]["BankName"].ToString();
                    BranchName = Convert.ToString(BRCD.ToString()) == "0000" ? "Consolidate" : DT.Rows[0]["BranchName"].ToString();
                }
            }
            else
            {
                DT = LG.GetBankName(Session["BRCD"].ToString());
                if (DT.Rows.Count > 0)
                {
                    BankName = DT.Rows[0]["BankName"].ToString();
                    BranchName = DT.Rows[0]["BranchName"].ToString();
                }
            }


            if (RptName == "RptLoanBalanceList_Pen.rdlc")
            {
                string RName = "";
                if (SL == "CF")
                    RName = "LOAN BALANCE REPORT (Court File A/C's Only)";
                else if (SL == "NCF")
                    RName = "LOAN BALANCE REPORT (No Court File A/C's )";
                else if (SL == "ALL")
                    RName = "LOAN BALANCE REPORT (All A/C's )";


                fileName = "RptLoanBalanceList_Pen";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BankName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("REPORT_NAME", RName);
                ReportParameter rp4 = new ReportParameter("DATE", OnDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("USER_NAME", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }

            if (RptName == "BalvikasCalcReport")
            {
                fileName = "Trail Entry Report";

                RptName = "RptRdIntCalcReport.rdlc";

                if (RptName == "RptRdIntCalcReport.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BankName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BranchName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                }
            }

            if (RptName == "BalvikasTally")
            {
                fileName = "Trail Entry Report";
                RptName = "RptRdIntCalcTally.rdlc";
                if (RptName == "RptRdIntCalcTally.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BankName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BranchName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                }
            }

            if (RptName == "RptRdIntCalcTally.rdlc")
            {
                fileName = "Trail Entry Report";
                if (RptName == "RptRdIntCalcTally.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BankName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BranchName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                }
            }
            if (RptName == "RptRdIntCalcReport.rdlc")
            {
                fileName = "Trail Entry Report";
                if (RptName == "RptRdIntCalcReport.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BankName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BranchName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                }
            }
          
           


            //  Added By Abhishek
            if (RptName == "RptEmiChart.rdlc")
            {
                fileName = "EMI Chart";
                PERIOD = PERIODTYPE == "Y" ? PERIOD + " Year(s)" : PERIOD + " Month(s)";

                ReportParameter rp1 = new ReportParameter("BANK_NAME", BankName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("LOAN_AMOUNT", LAMT.ToString());
                ReportParameter rp5 = new ReportParameter("PERIOD", PERIOD.ToString());
                ReportParameter rp6 = new ReportParameter("RATE", RATE.ToString());
                ReportParameter rp7 = new ReportParameter("REPAY_AMT", REPAYAMT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7 });
            }
            //  Added By amol Scrutiny Sheet Report
            if (RptName == "RptScrutinySheet.rdlc")
            {
                fileName = "Scrutiny Sheet Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BankName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("WORK_DATE", Session["EntryDate"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }

            //  Added By Amol Saving Interest Calculation Report
            if (RptName == "rptSavingIntCalculation.rdlc")
            {
                fileName = "Saving Int Calculation Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BankName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("WORK_DATE", Session["EntryDate"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }

            //  Added By Amol Applied Interest Report
            if (RptName == "RptAppliedInterest.rdlc" || RptName == "RptAppliedInterest_Parali.rdlc")
            {
                fileName = "Applied Interest Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BankName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("WORK_DATE", Session["EntryDate"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }

            //  Added By Amol DDS Interest Calculation Report
            if (RptName == "RptAVS5050.rdlc")
            {
                fileName = "DDS Interest Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BankName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("WORK_DATE", TDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }

            //  Added By Amol SB Interest Calculation Report
            if (RptName == "RptSBIntCalculation.rdlc")
            {
                fileName = "Saving Interest Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BankName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FROMDATE", FDate.ToString());
                ReportParameter rp5 = new ReportParameter("TODATE", TDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }

            //  Added By Amol SB Interest Calculation Report
            if (RptName == "RptSBIntCalcSum.rdlc")
            {
                fileName = "Saving Interest Summary Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BankName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("ASONDATE", ASONDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }

            //  Added By Amol For SB to RD (SI Post) Report
            if (RptName == "RptAVS5036.rdlc")
            {
                fileName = "SBTORD SI Post Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BankName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("WORK_DATE", EDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }

            //  Added By Amol For Statement View Report
          
            if (RptName == "RptInvStatementView.rdlc")
            {
                fileName = "RptInvStatementView";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BankName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("WORK_DATE", Session["EntryDate"].ToString());
                ReportParameter rp5 = new ReportParameter("FROM_DATE", FDate.ToString());
                ReportParameter rp6 = new ReportParameter("TO_DATE", TDate.ToString());
                ReportParameter rp7 = new ReportParameter("PRCD", PRCD.ToString());
                ReportParameter rp8 = new ReportParameter("LAMT", LAMT.ToString());
                ReportParameter rp9 = new ReportParameter("InstAmt", InstAmt.ToString());
                ReportParameter rp10 = new ReportParameter("IRate", IRate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 ,rp7,rp8,rp9,rp10});
            }
            
            //  Added By Amol Share allotment transaction's Report
            if (RptName == "RptShareAllotTransaction.rdlc")
            {
                fileName = "Share Allotment Transaction Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BankName.ToString());
                ReportParameter rp2 = new ReportParameter("FROM_DATE", FDate.ToString());
                ReportParameter rp3 = new ReportParameter("TO_DATE", TDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }

           

            //  Added By Amol Share Suspense Report
            if (RptName == "RptAVS5070.rdlc")
            {
                fileName = "Share Suspense Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BankName.ToString());
                ReportParameter rp2 = new ReportParameter("BR_CODE", BRCD.ToString());
                ReportParameter rp3 = new ReportParameter("FROM_DATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TO_DATE", TDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            if (RptName == "RptPTRegister.rdlc")
            {
                fileName = "P.T Register";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BankName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("USER_NAME", UserName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }

           
            if (RptName == "RptPendingRec.rdlc")
            {
                fileName = "P.T Register";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BankName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("USER_NAME", UserName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }

            //For Daily cash Position with Denomination
            else if (RptName == "rptDailyCashPosition.rdlc")
            {
                fileName = "DailyCashPosition_";
                ReportParameter rp1 = new ReportParameter("BRCODE", BRCD.ToString());
                ReportParameter rp2 = new ReportParameter("ASONDATE", EDate.ToString());
                ReportParameter rp3 = new ReportParameter("BANK_NAME", BankName.ToString());
                ReportParameter rp4 = new ReportParameter("BRANCH_NAME", BranchName.ToString());
                ReportParameter rp5 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }

            //  Added by amol for Folio charges report
            if (RptName == "RptAVS5147.rdlc")
            {
                ReportParameter rp1 = new ReportParameter("BANKNAME", BankName.ToString());
                ReportParameter rp2 = new ReportParameter("EDATE", EDate.ToString());
                ReportParameter rp3 = new ReportParameter("USERNAME", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }

            //  Added By Amol daily passbok Report
           
            
            //  Added By Amol daily passbok Report
            if (RptName == "RptAVS5152.rdlc")
            {
                ReportParameter rp1 = new ReportParameter("BankName", BankName.ToString());
                ReportParameter rp2 = new ReportParameter("BranchName", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("FDate", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TDate", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("UserName", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }

            //  Added By Amol for Interest calculation paraeter report
            if (RptName == "RptAVS5170.rdlc")
            {
                ReportParameter rp1 = new ReportParameter("BankName", BankName.ToString());
                ReportParameter rp2 = new ReportParameter("BranchName", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("UserName", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }

            RdlcPrint.LocalReport.DataSources.Clear();
            RdlcPrint.LocalReport.DataSources.Add(DataSource1);
            RdlcPrint.LocalReport.DataSources.Add(DataSource2);
            RdlcPrint.LocalReport.DataSources.Add(DataSource3);
            RdlcPrint.LocalReport.DataSources.Add(DataSource4);
            RdlcPrint.LocalReport.Refresh();
        }
    }
