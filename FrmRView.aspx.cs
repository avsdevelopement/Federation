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

public partial class FrmRView : System.Web.UI.Page
{
    int CLID, TrxType, Activity, AMOUNT;
    string PT, AC, BRCD, FDT, TDT, AGC, FD, TD, FL, SUBGLFRM, SUBGLTO, FBC, TBC, SETNO, UN, USERNAME, CNO = "", NAME = "", FN = "", BlankLine = "", LPP = "", BranchID = "", SR;
    string FLT, Wing, Productcode, P_CODE, ADDRESS2, REGISTRATIONNO, RegNo, REGISTRATIONNO1, OUTNO, DATEYEAR, CURRATE;
    string BName, BRName, Edate, bankadd, subglname, Perseva;
    string BankName, BranchName, BrName, CUSTNO, BANKNAME_MAR;
    string VENDORID, PRODID, ENTRYDATE, FLAG;
    public static string RptName, SalDed;
    public double SMDR, SMCR;
    string GL, SGL, MID, YY,id,MEMTYPE,MEMNO;
    string Address, Ref_Agent;
    string ExportT, Adress;
    string Charges = "";
    double OPBAL, DRBAL, CRBAL, CLBAL;
    string CTRL;
    string ToDOCType, FromDOCType;
    string UName, Type, TDAT, sbrcd, FCustNo, TCustNo, Div, Dept;
    string EDT = "";
    string FBrcd, TBrcd, Month, Year;
    string RFlag;
    string CPrd, Period, DPrd, PName;
    string CustNo = "";
    string FBRCD, TBRCD, FACCNO, TACCNO, ASONDT, RBD, ODA, ODI, FSAN, TSAN, REF, S1, S2, S3;
    string FPRDTYPE, TPRDTYPE, CT, PR, FDATE, TDATE, FACTYPE, TACTYPE;
    string LIENTYPE, BRCD1, BRCD2;
    string SL, SFL1, SFL2;
    string ProdCode, AccNo, Status, MemType, Flag;
    string monthdisp, yeardisp, Date, Cno, EDAT;
    string FPRCD, TPRCD, EntryDate, Fl, Name, ACTVT, Prd, FG, PRCD;
    string Ncustname, Ncustadd, Subg, Acc;
    string RATE = "", taluka, Village, Dist;
    string FYear, Fmonth, TYear, Tmonth;
    string CERT_ISSUE1STDATE, CUSTNAME, CERT_NO, SHAREFROM, SHARETO, SHARESVALUE, TOTALSHAREAMT, AMT;
    string FBKcode, TBKcode, FDate, TDate, UserName, ADT, Add, CustNo1, CustNo2;
    string LOANGL = "", ACCNO = "", custname = "", PurName1 = "", PurName2 = "", bal = "", Amt = "", Addr = "", AddFlag = "", CASEY;
    string PRDCD = "", SRNO = "", chqPrintDate = "";
    int isExcelDownload = 0;
    int isPDFDownload = 0;
    string FileName = "";


    string MemberNo, Divident, DepositInt, TotalPay, CheqNo, BankCode, D1, D2, M1, M2, Y1, Y2, Y3, Y4;
    ClsCloseAccDdetails CAD = new ClsCloseAccDdetails();
    ClsLoanSecurity ls = new ClsLoanSecurity();
    ClsLoanBalanceCer LMC = new ClsLoanBalanceCer();
    ClsUnverifiedlist UV = new ClsUnverifiedlist();
    ClsDeadStock DS = new ClsDeadStock();
    ClsDefaulters DF = new ClsDefaulters();
    ClsCustUnification CU = new ClsCustUnification();
    ClsOverDueSummary ODS = new ClsOverDueSummary();
    ClsMaturityLoanReport MLR = new ClsMaturityLoanReport();
    DbConnection conn = new DbConnection();
    ClsCustomerDetails CUR = new ClsCustomerDetails();
    ClsCashLimitMst CLM = new ClsCashLimitMst();
    ClsNPA NPA = new ClsNPA();
    ClsAVS51173 C73 = new ClsAVS51173();
    ClsLoanRecovery lrr = new ClsLoanRecovery();
    ClsLoanBscReport LR = new ClsLoanBscReport();
    ClsLoanIntcal LIC = new ClsLoanIntcal();
    ClsBankReconsile BankReconsile = new ClsBankReconsile();
    clsSanchitCertificate SanCerti = new clsSanchitCertificate();
    ClsLienMark cm = new ClsLienMark();
    ClsFDIntCalculation FFD = new ClsFDIntCalculation();
    ClsAVS5115 RI = new ClsAVS5115();
    ClsCashBook CC = new ClsCashBook();
    ClsGetAcBalReg BRC = new ClsGetAcBalReg();
    ClsGetAccStm AS = new ClsGetAccStm();
    ClsGetGLTransD GLT = new ClsGetGLTransD();
    ClsDigitalBanking clsdigital = new ClsDigitalBanking();
    ClsGetOffGLTransD OGLT = new ClsGetOffGLTransD();
    ClsDepositList DL = new ClsDepositList();
    ClsLoanList DLn = new ClsLoanList();
    ClsInvClosure IC = new ClsInvClosure();
    ClsDocRegister DR = new ClsDocRegister();
    ClsCashPostion CPL = new ClsCashPostion();
    ClsLoanParameter LP = new ClsLoanParameter();
    ClsDlyCashPos CP = new ClsDlyCashPos();
    ClsLoanInfo LI = new ClsLoanInfo();
    ClsAddMenu adm = new ClsAddMenu();
    ClsAccopen AO = new ClsAccopen();
    ClsGoldOraReport GR = new ClsGoldOraReport();
    ClsBindDropdown BD = new ClsBindDropdown();
    ClsRegisterReport LDR = new ClsRegisterReport();
    ClsAgentReport AR = new ClsAgentReport();
    ClsDepositReport DepM = new ClsDepositReport();

    ClsGLreport glrep = new ClsGLreport();
    ClsCutBook CB = new ClsCutBook();
    ClsLogin LG = new ClsLogin();
    ClsNewTDA TDARCT = new ClsNewTDA();
    ClsOpenClose OC = new ClsOpenClose();
    ClsBalanceSheet BS = new ClsBalanceSheet();
    ClsAMLReport AML = new ClsAMLReport();
    ClsTrailBalance TB = new ClsTrailBalance();
    ClsDayBook DBook = new ClsDayBook();
    ClsCTRReport CTR = new ClsCTRReport();
    ClsAccountSTS ACST = new ClsAccountSTS();
    ClsProfitAndLoss PL = new ClsProfitAndLoss();
    ClsDocumentMaster KYC = new ClsDocumentMaster();
    ClsOIRegister OIR = new ClsOIRegister();
    ClsDocRegister DRR = new ClsDocRegister();
    ClsSavPassBookPrint Pass = new ClsSavPassBookPrint();
    ClsAllOKReport AK = new ClsAllOKReport();
    ClsSI_DDSToLoan SI = new ClsSI_DDSToLoan();
    ClsChequeIssueRegister CIR = new ClsChequeIssueRegister();
    ClsChequeStockReport CSR = new ClsChequeStockReport();
    ClsAccountOpenCloseReport AOC = new ClsAccountOpenCloseReport();
    ClsUnOperativeAccountsReport UOA = new ClsUnOperativeAccountsReport();
    ClsLoanOverdue LC = new ClsLoanOverdue();
    ClsCashReciept CR = new ClsCashReciept();
    ClsExcessCashRep EC = new ClsExcessCashRep();
    ClsCashDenom CD = new ClsCashDenom();
    ClsDayBookDetails DBD = new ClsDayBookDetails();
    ClsGetCutBookDetail CBD = new ClsGetCutBookDetail();
    ClsGetFormA FA = new ClsGetFormA();
    ClsUserReport UR = new ClsUserReport();
    ClsGetSubBook VSB = new ClsGetSubBook();
    ClsGetCustdetails Cust = new ClsGetCustdetails();
    ClsGetRiskdetails RT = new ClsGetRiskdetails();
    ClsGetFDRClass FDR = new ClsGetFDRClass();
    ClsAVSBMTransaction MB = new ClsAVSBMTransaction();
    ClsGetFdSlabeDetails FDS = new ClsGetFdSlabeDetails();
    ClsGetIWRegDetails IWD = new ClsGetIWRegDetails();
    ClsGetIWClgRegDetails IWDR = new ClsGetIWClgRegDetails();
    GetIOReturnReg IORE = new GetIOReturnReg();
    ClsGetSharesApplication APP = new ClsGetSharesApplication();
    ClsGetTransSubMonth GLM = new ClsGetTransSubMonth();
    ClsGetTransSumarryMonhWise GLS = new ClsGetTransSumarryMonhWise();
    ClsGetDPLNList DPLN = new ClsGetDPLNList();
    ClsGrtClgMemoDetails MM = new ClsGrtClgMemoDetails();
    ClsGetGLTransMonth GLMT = new ClsGetGLTransMonth();
    ClsGetDayBookDeatilsSetWise DSET = new ClsGetDayBookDeatilsSetWise();
    ClsGetLoanStatement LNS = new ClsGetLoanStatement();
    ClsGetBrDPLNList FYDL = new ClsGetBrDPLNList();
    ClsLoanInstallmen LSS = new ClsLoanInstallmen();
    ClsGetLoanStatDetails LTD = new ClsGetLoanStatDetails();
    ClsDDSIntView DV = new ClsDDSIntView();
    ClsTDAIntMast TDA = new ClsTDAIntMast();
    ClsAccCount CAC = new ClsAccCount();
    ClsDayActivityView DAV = new ClsDayActivityView();
    ClsPLTransfer PLT = new ClsPLTransfer();
    ClsCDR CDR = new ClsCDR();
    ClsDDCR DDS = new ClsDDCR();
    ClsInvClosure ICD = new ClsInvClosure();
    ClsDDSToLoan DDSL = new ClsDDSToLoan();
    ClsGLreport Glrpt = new ClsGLreport();
    ClsRDCommission RD = new ClsRDCommission();
    ClsSIPost CSI = new ClsSIPost();
    ClsAccOpenClseRpt CAOCR = new ClsAccOpenClseRpt();
    ClsGetODList ODL = new ClsGetODList();
    ClsClgReturnReg CRR = new ClsClgReturnReg();
    ClsUnpassEntries UP = new ClsUnpassEntries();
    ClsGetDPClassification DPCL = new ClsGetDPClassification();
    CLSIWOWCharges CH = new CLSIWOWCharges();
    ClsGetPLExepenses PLE = new ClsGetPLExepenses();
    CLsShareCertificate SC = new CLsShareCertificate();
    ClsCDRatio CCDR = new ClsCDRatio();
    ClsShareMember CCD = new ClsShareMember();
    ClsGetNPAList NPAC = new ClsGetNPAList();
    ClsIJRegister IJ = new ClsIJRegister();
    ClsBrWiseDPLNList BDep = new ClsBrWiseDPLNList();
    ClsDividendCalc DC = new ClsDividendCalc();
    ClsLoanSanctionReport LS = new ClsLoanSanctionReport();
    ClsCustWiseBalance SB = new ClsCustWiseBalance();
    ClsCustomerMast MC = new ClsCustomerMast();
    ClsGoldOraDetails GLD = new ClsGoldOraDetails();
    ClsMemberPassbook MPD = new ClsMemberPassbook();
    ClsAVS5042 NOC = new ClsAVS5042();
    ClsLogDetails LD = new ClsLogDetails();
    ClsITax ITax = new ClsITax();
    ClsPhotoSignRpt PS = new ClsPhotoSignRpt();
    ClsVoucherActInfo VA = new ClsVoucherActInfo();
    ClsAVS5048 ODC = new ClsAVS5048();
    ClsMultiAgentCommi MA = new ClsMultiAgentCommi();
    ClsIntrestMastr CurrenCls = new ClsIntrestMastr();
    ClsAmtWsieClassification LNSB = new ClsAmtWsieClassification();
    ClsLoanRepaymentCerti LRPC = new ClsLoanRepaymentCerti();
    ClsEncryptValue EV = new ClsEncryptValue();
    ClsAVS5057 AVS5057 = new ClsAVS5057();
    ClsAdharCard Adhar = new ClsAdharCard();
    ClsAVS5027 avs = new ClsAVS5027();
    ClsIntRateDEpositLoan INTD = new ClsIntRateDEpositLoan();
    ClsLoanDeposit LDE = new ClsLoanDeposit();
    ClsAVS5063 PM = new ClsAVS5063();
    ClsGetCustomerDT CUSTDT = new ClsGetCustomerDT();
    ClsSBIntCalcSum SBS = new ClsSBIntCalcSum();
    ClsPTRegister CUSREC = new ClsPTRegister();
    ClsLoanApplication LNAPP = new ClsLoanApplication();
    ClsDividentPayTran DIVP = new ClsDividentPayTran();
    ClsSBIntCalculation SB1 = new ClsSBIntCalculation();
    ClsAvs5140 BKP = new ClsAvs5140();
    ClsNotice_SRO SRONO = new ClsNotice_SRO();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string glname, accno, accname;
            glname = accno = accname = "";
            string agentc, fdate, tdate, FLim, TLim;
            agentc = fdate = tdate = FLim = TLim = "";
            string fromdate, todate;
            fromdate = todate = "";
            string AsOnDate = "";
            string DdlStatus = "";
            string MOBILE = "";
            string OnDate = "", Subgl = "", Accno = "";
            string Fsubgl = "", Tsubgl = "", FBRCD = "", TBRCD = "", SKIP_DIGIT = ""; ;
            string ExportT = "";

            RptName = Request.QueryString["rptname"].ToString();

            //RptCustWiseBalance
            if (RptName == "RptCustWiseBalance.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ASONDATE"] != null)
                {
                    ASONDT = Request.QueryString["ASONDATE"].ToString().Replace("%27", "");
                }
            }

            if (RptName == "RptFedStatement.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["CustNo"] != null)
                {
                    CustNo = Request.QueryString["CustNo"].ToString().Replace("%27", "");
                }
            }

            if (RptName == "RptOrdReceipt.rdlc")
            {
                if (Request.QueryString["ID"] != null)
                {
                    id = Request.QueryString["ID"].ToString().Replace("%27", "");
                } if (Request.QueryString["MEMTYPE"] != null)
                {
                    MEMTYPE = Request.QueryString["MEMTYPE"].ToString().Replace("%27", "");
                } if (Request.QueryString["MEMNO"] != null)
                {
                    MEMNO = Request.QueryString["MEMNO"].ToString().Replace("%27", "");
                }
               
            }
            if (RptName == "RptDeedStock.rdlc" || RptName == "RptDeedStock_Stock.rdlc")
            {
                if (Request.QueryString["FLAG"] != null)
                {
                    FLAG = Request.QueryString["FLAG"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["VENDORID"] != null)
                {
                    VENDORID = Request.QueryString["VENDORID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PRODID"] != null)
                {
                    PRODID = Request.QueryString["PRODID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ENTRYDATE"] != null)
                {
                    ENTRYDATE = Request.QueryString["ENTRYDATE"].ToString().Replace("%27", "");
                }

            }

            if (RptName == "RptClosingStock.rdlc")
            {
                if (Request.QueryString["FLAG"] != null)
                {
                    FLAG = Request.QueryString["FLAG"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["VENDORID"] != null)
                {
                    VENDORID = Request.QueryString["VENDORID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PRODID"] != null)
                {
                    PRODID = Request.QueryString["PRODID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ENTRYDATE"] != null)
                {
                    ENTRYDATE = Request.QueryString["ENTRYDATE"].ToString().Replace("%27", "");
                }

            }

            if (RptName == "RptVendorMaster.rdlc")
            {
                if (Request.QueryString["FLAG"] != null)
                {
                    FLAG = Request.QueryString["FLAG"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["VENDORID"] != null)
                {
                    VENDORID = Request.QueryString["VENDORID"].ToString().Replace("%27", "");
                }

            }

            if (RptName == "RptProductMaster.rdlc")
            {
                if (Request.QueryString["FLAG"] != null)
                {
                    FLAG = Request.QueryString["FLAG"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["VENDORID"] != null)
                {
                    VENDORID = Request.QueryString["VENDORID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PRODID"] != null)
                {
                    PRODID = Request.QueryString["PRODID"].ToString().Replace("%27", "");
                }

            }
            if (RptName == "RptLienMarkList.rdlc")
            {
                if (Request.QueryString["FrBrCode"] != null)
                {
                    FBRCD = Request.QueryString["FrBrCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ToBrCode"] != null)
                {
                    TBRCD = Request.QueryString["ToBrCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FrGlCode"] != null)
                {
                    FPRCD = Request.QueryString["FrGlCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ToGlCode"] != null)
                {
                    TPRCD = Request.QueryString["ToGlCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOndate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOndate"].ToString().Replace("%27", "");
                }
            }

            if (RptName == "GSTMasterRpt.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptAddressLabelPrint_TZMP.rdlc") // Address Label Print
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FAccno"] != null)
                {
                    AccNo = Request.QueryString["FAccno"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TAccno"] != null)
                {
                    TACCNO = Request.QueryString["TAccno"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Div"] != null)
                {
                    S1 = Request.QueryString["Div"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Dep"] != null)
                {
                    S2 = Request.QueryString["Dep"].ToString().Replace("%27", "");
                }
            }

                // 19012019 rakesh 

            else if (RptName == "RptDemandRecList.rdlc" || RptName == "RptDemandRecList_DT.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["MM"] != null)
                {
                    Month = Request.QueryString["MM"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["YY"] != null)
                {
                    Year = Request.QueryString["YY"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Div"] != null)
                {
                    FL = Request.QueryString["Div"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Dep"] != null)
                {
                    FLT = Request.QueryString["Dep"].ToString().Replace("%27", "");
                }
            }
            //rakesh15012019
            else if (RptName == "RptCKYCList.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FCustNo"] != null)
                {
                    FACCNO = Request.QueryString["FCustNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TCustNo"] != null)
                {
                    TACCNO = Request.QueryString["TCustNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["RFlag"] != null)
                {
                    RFlag = Request.QueryString["RFlag"].ToString().Replace("%27", "");
                }
            }


            //rakesh15012019
            if (RptName == "RptSBIntCalcReport_DT.rdlc" || RptName == "RptSBIntCalcReport_Sumry.rdlc")
            {
                if (Request.QueryString["BC"] != null)
                {
                    BRCD = Request.QueryString["BC"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PC"] != null)
                {
                    Prd = Request.QueryString["PC"].ToString().Replace("%27", "");
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
                    MID = Request.QueryString["MID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptAVS5143.rdlc" || RptName == "RptAVS5143_BACK.rdlc" || RptName == "RptAVS5143_Marathi.rdlc")//Dhanya Shetty//30/08/2018//For ChequePrint
            {
                if (Request.QueryString["MemberNo"] != null)
                {
                    MemberNo = Request.QueryString["MemberNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Divident"] != null)
                {
                    Divident = Request.QueryString["Divident"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["DepositInt"] != null)
                {
                    DepositInt = Request.QueryString["DepositInt"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TotalPay"] != null)
                {
                    TotalPay = Request.QueryString["TotalPay"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["CheqNo"] != null)
                {
                    CheqNo = Request.QueryString["CheqNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BankCode"] != null)
                {
                    BankCode = Request.QueryString["BankCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Flag"] != null)
                {
                    Flag = Request.QueryString["Flag"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Name"] != null)
                {
                    Name = Request.QueryString["Name"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["D1"] != null)
                {
                    D1 = Request.QueryString["D1"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["D2"] != null)
                {
                    D2 = Request.QueryString["D2"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["M1"] != null)
                {
                    M1 = Request.QueryString["M1"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["M2"] != null)
                {
                    M2 = Request.QueryString["M2"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Y1"] != null)
                {
                    Y1 = Request.QueryString["Y1"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Y2"] != null)
                {
                    Y2 = Request.QueryString["Y2"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Y3"] != null)
                {
                    Y3 = Request.QueryString["Y3"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Y4"] != null)
                {
                    Y4 = Request.QueryString["Y4"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["ChqPrintDate"] != null)
                {
                    chqPrintDate = Request.QueryString["ChqPrintDate"].ToString().Replace("%27", "");
                }
            }


            else if (RptName == "RptBalanceS_Marathi.rdlc" || RptName == "RptBalanceSarjudas_Marathi.rdlc")
            {
                if (Request.QueryString["DEX"] != null)
                {
                    FL = Request.QueryString["DEX"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
            }


            //DigitalBanking
            if (RptName == "RptDigitalBanking.rdlc")
            {
                if (Request.QueryString["FromBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FromBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ToBRCD"] != null)
                {
                    TBRCD = Request.QueryString["ToBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Prodcode"] != null)
                {
                    ProdCode = Request.QueryString["Prodcode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Charges"] != null)
                {
                    Charges = Request.QueryString["Charges"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Date"] != null)
                {
                    Date = Request.QueryString["Date"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PL"] != null)
                {
                    PT = Request.QueryString["PL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UName"] != null)
                {
                    UName = Request.QueryString["UName"].ToString().Replace("%27", "");
                }
            }
            //11012018 rakesh

            else if (RptName == "RptShareCertiShr.rdlc")
            {
                if (Request.QueryString["EntryDate"] != null)
                {
                    EntryDate = Request.QueryString["EntryDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MID"] != null)
                {
                    MID = Request.QueryString["MID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AccNo"] != null)
                {
                    AccNo = Request.QueryString["AccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["CerNo"] != null)
                {
                    Fl = Request.QueryString["CerNo"].ToString().Replace("%27", "");
                }
            }


            else if (RptName == "RptProfitAndLoss_Marathi.rdlc" || RptName == "RptProfitAndLossSarjudas_Marathi.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
            }
            //RptDividendCalc
            if (RptName == "RptDividendCalc.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDATE = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDATE = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRDCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRDCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRDCD"] != null)
                {
                    TPRCD = Request.QueryString["TPRDCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FACCNO"] != null)
                {
                    FACCNO = Request.QueryString["FACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TACCNO"] != null)
                {
                    TACCNO = Request.QueryString["TACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["RATE"] != null)
                {
                    RATE = Request.QueryString["RATE"].ToString().Replace("%27", "");
                }
            }


            if (RptName == "RptLoanRegister.rdlc")
            {
                if (Request.QueryString["FDAT"].ToString() != null)
                {
                    FDate = Request.QueryString["FDAT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDAT"].ToString() != null)
                {
                    TDate = Request.QueryString["TDAT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PRD"].ToString() != null)
                {
                    Prd = Request.QueryString["PRD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"].ToString() != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UNAME"] != null)
                {
                    UName = Request.QueryString["UNAME"].ToString().Replace("%27", "");
                }
            }
            //RptIWOWCharges
            if (RptName == "RptIWOWCharges.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FLAG"] != null)
                {
                    FL = Request.QueryString["FLAG"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EDT"] != null)
                {
                    EDT = Request.QueryString["EDT"].ToString().Replace("%27", "");
                }
            }
            //Adhar link report
            if (RptName == "RptAdharLink.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EDAT"] != null)
                {
                    EDAT = Request.QueryString["EDAT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptAdharLinkDetails.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EDAT"] != null)
                {
                    EDAT = Request.QueryString["EDAT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Flag"] != null)
                {
                    Flag = Request.QueryString["Flag"].ToString().Replace("%27", "");
                }
            }
            //Unpass Details
            if (RptName == "RptUnpassDetails.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ASONDATE"] != null)
                {
                    ASONDT = Request.QueryString["ASONDATE"].ToString().Replace("%27", "");
                }
            }
            //Inw Return Detail
            if (RptName == "RptIWReturnRegDetails.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDT = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDT = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SFL"] != null)
                {
                    SFL1 = Request.QueryString["SFL"].ToString().Replace("%27", "");
                }
            }
            //Inw Return Summary
            if (RptName == "RptIWReturnRegSummary.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDT = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDT = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SFL"] != null)
                {
                    SFL1 = Request.QueryString["SFL"].ToString().Replace("%27", "");
                }
            }
            //Outward Return Details
            if (RptName == "RptOWReturnRegDetails.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDT = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDT = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SFL"] != null)
                {
                    SFL1 = Request.QueryString["SFL"].ToString().Replace("%27", "");
                }
            }
            //Outward Return Summary
            if (RptName == "RptOWReturnRegSummary.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDT = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDT = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SFL"] != null)
                {
                    SFL1 = Request.QueryString["SFL"].ToString().Replace("%27", "");
                }
            }
            //Loan Schedule new Rpt_LoanSchedule_Parti
            if (RptName == "Rpt_LoanSchedule_Parti.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["SBGL"] != null)
                {
                    Subgl = Request.QueryString["SBGL"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["ACCNO"] != null)
                {
                    AC = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptTransfer.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Flag"] != null)
                {
                    FL = Request.QueryString["Flag"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["ProdCode"] != null)
                {
                    SGL = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["AccNo"] != null)
                {
                    AC = Request.QueryString["AccNo"].ToString().Replace("%27", "");
                }

                if (Convert.ToInt32(Request.QueryString["Amount"]) != 0)
                {
                    AMOUNT = Convert.ToInt32(Request.QueryString["Amount"].ToString().Replace("%27", ""));
                }

                if (Convert.ToInt32(Request.QueryString["TRXType"]) != 0)
                {
                    TrxType = Convert.ToInt32(Request.QueryString["TRXType"].ToString().Replace("%27", ""));
                }

                if (Convert.ToInt32(Request.QueryString["Activity"]) != 0)
                {
                    Activity = Convert.ToInt32(Request.QueryString["Activity"].ToString().Replace("%27", ""));
                }
                if (Request.QueryString["UNAME"] != null)
                {
                    UName = Request.QueryString["UNAME"].ToString().Replace("%27", "");
                }
            }
            //RptDDSCMonthlySummary Abhishek 17/05/2017
            if (RptName == "RptDDSCMonthlySummary.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["GLC"] != null)
                {
                    GL = Request.QueryString["GLC"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["SUBGL"] != null)
                {
                    Subgl = Request.QueryString["SUBGL"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["ACCNO"] != null)
                {
                    AC = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
            }

            if (RptName == "RptRaidPaid.rdlc")
            {
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["LoanName"] != null)
                {
                    UName = Request.QueryString["LoanName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNo"] != null)
                {
                    Accno = Request.QueryString["ACCNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Surity"] != null)
                {
                    UN = Request.QueryString["Surity"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptInvMat.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PROD"] != null)
                {
                    GL = Request.QueryString["PROD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDAT"] != null)
                {
                    FDate = Request.QueryString["FDAT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDAT"] != null)
                {
                    TDATE = Request.QueryString["TDAT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EDAT"] != null)
                {
                    EDT = Request.QueryString["EDAT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Flag"] != null)
                {
                    FL = Request.QueryString["Flag"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UName"] != null)
                {
                    UName = Request.QueryString["UName"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptInv_Reg.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["PROD"] != null)
                {
                    GL = Request.QueryString["PROD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EDAT"] != null)
                {
                    EDT = Request.QueryString["EDAT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Flag"] != null)
                {
                    FL = Request.QueryString["Flag"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UName"] != null)
                {
                    UName = Request.QueryString["UName"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptInvProd.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["PROD"] != null)
                {
                    GL = Request.QueryString["PROD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EDAT"] != null)
                {
                    EDT = Request.QueryString["EDAT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Flag"] != null)
                {
                    FL = Request.QueryString["Flag"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UName"] != null)
                {
                    UName = Request.QueryString["UName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Value"] != null)
                {
                    FLT = Request.QueryString["Value"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptInvInterest.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UName"] != null)
                {
                    UName = Request.QueryString["UName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRD"] != null)
                {
                    SUBGLFRM = Request.QueryString["FPRD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRD"] != null)
                {
                    SUBGLTO = Request.QueryString["TPRD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EDAT"] != null)
                {
                    EDT = Request.QueryString["EDAT"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptInvMaturity.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PROD"] != null)
                {
                    GL = Request.QueryString["PROD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDAT"] != null)
                {
                    FDATE = Request.QueryString["FDAT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDAT"] != null)
                {
                    TDATE = Request.QueryString["TDAT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Flag"] != null)
                {
                    FL = Request.QueryString["Flag"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UName"] != null)
                {
                    UName = Request.QueryString["UName"].ToString().Replace("%27", "");
                }
            }

            //FD INT Calculation Report
            if (RptName == "RptFDINTCalculation.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRD"] != null)
                {
                    Fsubgl = Request.QueryString["FPRD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRD"] != null)
                {
                    Tsubgl = Request.QueryString["TPRD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FACCNO"] != null)
                {
                    FACCNO = Request.QueryString["FACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TACCNO"] != null)
                {
                    TACCNO = Request.QueryString["TACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ASONDATE"] != null)
                {
                    ASONDT = Request.QueryString["ASONDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MID"] != null)
                {
                    MID = Request.QueryString["MID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SKIP_DIGIT"] != null)
                {
                    SKIP_DIGIT = Request.QueryString["SKIP_DIGIT"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RPTDDSIntMst.rdlc")
            {
                if (Request.QueryString["flag"] != null)
                {
                    FL = Request.QueryString["flag"].ToString().Replace("%27", "");
                }
                //if (Request.QueryString["Brcd"] != null)
                //{
                //    FBRCD = Request.QueryString["Brcd"].ToString().Replace("%27", "");
                //}
                //if (Request.QueryString["BRCD1"] != null)
                //{
                //    TBRCD = Request.QueryString["BRCD1"].ToString();
                //}
                if (Request.QueryString["ProdCode1"] != null)
                {
                    FPRDTYPE = Request.QueryString["ProdCode1"].ToString();
                }
                if (Request.QueryString["PRODUCT2"] != null)
                {
                    TPRDTYPE = Request.QueryString["PRODUCT2"].ToString();
                }
                if (Request.QueryString["UserName"] != null)
                {
                    SGL = Request.QueryString["UserName"].ToString();
                }
                if (Request.QueryString["CT"] != null)
                {
                    CT = Request.QueryString["CT"].ToString();
                }
                if (Request.QueryString["PR"] != null)
                {
                    PR = Request.QueryString["PR"].ToString();
                }
            }
            else if (RptName == "RptDivPayTrans.rdlc")  // Divident pay Transaction
            {
                if (Request.QueryString["FLAG"] != null)
                {
                    FL = Request.QueryString["FLAG"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ID"] != null)
                {
                    Flag = Request.QueryString["ID"].ToString().Replace("%27", "");
                }
            }
            //Day Activity View
            else if (RptName == "RptDayActivity.rdlc")
            {
                if (Request.QueryString["flag"] != null)
                {
                    FL = Request.QueryString["flag"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Brcd"] != null)
                {
                    BRCD = Request.QueryString["Brcd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDATE = Request.QueryString["FDate"].ToString();
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDATE = Request.QueryString["TDate"].ToString();
                }
                if (Request.QueryString["RBD"] != null)
                {
                    RBD = Request.QueryString["RBD"].ToString();
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString();
                }
            }
            //LOAN BASIC REPORT


            else if (RptName == "RptSRONotice10.rdlc")//Dhanya Shetty//20-06-2017//For federation letter
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RptAVS5065.rdlc" || RptName == "RptLoanInthead.rdlc")//Dhanya Shetty//For Loan  Certificate1
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["LOANGL"] != null)
                {
                    LOANGL = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    ACCNO = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
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
            else if (RptName == "RptAVS5066.rdlc" || RptName == "RptLoanclosure.rdlc")//Dhanya Shetty//For Loan Closure
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["LOANGL"] != null)
                {
                    LOANGL = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    ACCNO = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }


            }
            else if (RptName == "RptDayClose.rdlc")
            {

                if (Request.QueryString["flag"] != null)
                {
                    FL = Request.QueryString["flag"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Brcd"] != null)
                {
                    BRCD = Request.QueryString["Brcd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDATE = Request.QueryString["FDate"].ToString();
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDATE = Request.QueryString["TDate"].ToString();
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString();
                }
            }
            else if (RptName == "RptNPASelectType_1.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRCD"] != null)
                {
                    TPRCD = Request.QueryString["TPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FAccNo"] != null)
                {
                    FACCNO = Request.QueryString["FAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TAccNo"] != null)
                {
                    TACCNO = Request.QueryString["TAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Date"] != null)
                {
                    FDT = Request.QueryString["Date"].ToString();
                }
                if (Request.QueryString["AType"] != null)
                {
                    FL = Request.QueryString["AType"].ToString();
                }
                if (Request.QueryString["S1Type"] != null)
                {
                    S1 = Request.QueryString["S1Type"].ToString();
                }
                if (Request.QueryString["S2Type"] != null)
                {
                    S2 = Request.QueryString["S2Type"].ToString();
                }
                if (Request.QueryString["DEX"] != null)
                {
                    Flag = Request.QueryString["DEX"].ToString();
                }
                if (Request.QueryString["NPAType"] != null)
                {
                    FLT = Request.QueryString["NPAType"].ToString();
                }

            }


                //////Report of account no. count
            else if (RptName == "RptAccCount.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["GLCODE"] != null)
                {
                    GL = Request.QueryString["GLCODE"].ToString();
                }

            }

            ///////////////BILL PRINTING SPEC ALL START
            else if (RptName == "RptLoanSchedule.rdlc")
            {
                if (Request.QueryString["PT"] != null)
                {
                    PT = Request.QueryString["PT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AC"] != null)
                {
                    AC = Request.QueryString["AC"].ToString();
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
            }
            else if (RptName == "RptUserMaster.rdlc")
            {
                if (Request.QueryString["UserName"] != null)
                {
                    UserName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }

            }

            //Cheque Issue Register Report AMOL
            else if (RptName == "RptChequeIssueRegister.rdlc")
            {
                if (Request.QueryString["BCode"] != null)
                {
                    FDate = Request.QueryString["BCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AC = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Flag"] != null)
                {
                    GL = Request.QueryString["Flag"].ToString().Replace("%27", "");

                    if (GL == "S")
                    {
                        if (Request.QueryString["ProdCode"] != null)
                        {
                            TDate = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
                        }
                        if (Request.QueryString["AccNo"] != null)
                        {
                            PT = Request.QueryString["AccNo"].ToString().Replace("%27", "");
                        }
                    }
                }
            }

            //Cheque Stock Report AMOL
            else if (RptName == "RptChequeStock.rdlc")
            {
                if (Request.QueryString["BCode"] != null)
                {
                    FDate = Request.QueryString["BCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AC = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Flag"] != null)
                {
                    GL = Request.QueryString["Flag"].ToString().Replace("%27", "");

                    if (GL == "S")
                    {
                        if (Request.QueryString["PCode"] != null)
                        {
                            TDate = Request.QueryString["PCode"].ToString().Replace("%27", "");
                        }
                    }
                }
            }

            //Cheque Saving Passbook Report
            else if (RptName == "RptSavingPassBook.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    PT = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AccNo"] != null)
                {
                    AC = Request.QueryString["AccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["GlName"] != null)
                {
                    GL = Request.QueryString["GlName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BlankLine"] != null)
                {
                    BlankLine = Request.QueryString["BlankLine"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["LPP"] != null)
                {
                    LPP = Request.QueryString["LPP"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SR"] != null)
                {
                    SR = Request.QueryString["SR"].ToString().Replace("%27", "");
                }
            }


            //Account Open Close Report
            else if (RptName == "RptAccountOpenCloseReport.rdlc")
            {
                if (Request.QueryString["BCode"] != null)
                {
                    BRCD = Request.QueryString["BCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PCode"] != null)
                {
                    PT = Request.QueryString["PCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    FDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Flag"] != null)
                {
                    FL = Request.QueryString["Flag"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
            }


            //Un-operative Accounts Report
            else if (RptName == "RptUnOpAccts.rdlc")
            {
                if (Request.QueryString["FBCode"] != null)
                {
                    FBC = Request.QueryString["FBCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBCode"] != null)
                {
                    TBC = Request.QueryString["TBCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PCode"] != null)
                {
                    PT = Request.QueryString["PCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    FDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Month"] != null)
                {
                    FL = Request.QueryString["Month"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
            }

            //User Report
            else if (RptName == "RptUserReport.rdlc")
            {

                if (Request.QueryString["FBRCD"] != null)
                {
                    FBC = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBC = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FLG"] != null)
                {
                    FL = Request.QueryString["FLG"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FLG1"] != null)
                {
                    Flag = Request.QueryString["FLG1"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["USERNAME"] != null)
                {
                    USERNAME = Request.QueryString["USERNAME"].ToString().Replace("%27", "");
                }

            }

            //Top Deposit List 
            else if (RptName == "RptTopDepositList.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    FBC = Request.QueryString["BranchID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SubGlCode"] != null)
                {
                    GL = Request.QueryString["SubGlCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    FDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Count"] != null)
                {
                    FD = Request.QueryString["Count"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    USERNAME = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
            }
            //Top Loan List 
            else if (RptName == "RptTopLoanList.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    FBC = Request.QueryString["BranchID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SubGlCode"] != null)
                {
                    GL = Request.QueryString["SubGlCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    FDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Count"] != null)
                {
                    FD = Request.QueryString["Count"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    USERNAME = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RptIntimationNotice_Sro.rdlc" || RptName == "RptProtectionNotice_Sro.rdlc" || RptName == "RptPossessionNotice_Sro.rdlc" || RptName == "RptUpsetPrizeNotice_Sro.rdlc" || RptName == "RptUpsetCoverletter_Sro.rdlc" || RptName == "RptPublicLetter_Sro.rdlc" || RptName == "RptSushilLetter_Sro.rdlc" || RptName == "RptTermCondition_Notice.rdlc" || RptName == "RptTenderForm_Notice.rdlc" || RptName == "RptAuction_BLetter.rdlc" || RptName == "AuctionNotice(marathi).rdl" || RptName == "RptIntimation_Cheque.rdlc" || RptName == "RptIntimation_ToSocity.rdlc" || RptName == "RptExecutionChargesLetter_ToSocity.rdlc" || RptName == "Rpt31Remainder_Notice.rdlc" || RptName == "RptFinalIntimationLetter.rdlc" || RptName == "AuctionNotice(marathi).rdlc" || RptName == "RPTPublicNotice(E_M).rdlc" || RptName == "RPTproposalforsale.rdlc" || RptName == "RptAutionMarathi.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AddFlag"] != null)
                {
                    AddFlag = Request.QueryString["AddFlag"].ToString().Replace("%27", "");
                }
            }

            //Account Bal register Rakesh
            else if (RptName == "RptAcBalRegisterReport.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    FBC = Request.QueryString["BranchID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["GLCode"] != null)
                {
                    GL = Request.QueryString["GLCode"].ToString();
                }
                if (Request.QueryString["SubGlCode"] != null)
                {
                    FD = Request.QueryString["SubGlCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }

            }


            else if (RptName == "RptAVSBMTableReport.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    FBC = Request.QueryString["BranchID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SubGlCode"] != null)
                {
                    FD = Request.QueryString["SubGlCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }

            }
            //Account Statement 08-12-2016 Rakesh
            else if (RptName == "RptAccountStatementReport.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    PT = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AccNo"] != null)
                {
                    AC = Request.QueryString["AccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    FBC = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptMemberPassBook.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["CustNO"] != null)
                {
                    PT = Request.QueryString["CustNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    FBC = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptMemberPassBook1.rdlc" || RptName == "RptMemberPassBook_DT.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["CustNO"] != null)
                {
                    PT = Request.QueryString["CustNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    FBC = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptPassbokkCover.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    PT = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AccNo"] != null)
                {
                    AC = Request.QueryString["AccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    FBC = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RptReceiptPrint.rdlc" || RptName == "RptReceiptPrint_2.rdlc" || RptName == "RptReceiptPrint_SHIV.rdlc")
            {

                if (Request.QueryString["SETNO"] != null)
                {
                    SETNO = Request.QueryString["SETNO"].ToString();
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EDT"] != null)
                {
                    EDT = Request.QueryString["EDT"].ToString();
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["CNO"] != null)
                {
                    CNO = Request.QueryString["CNO"].ToString();
                }
                if (Request.QueryString["NAME"] != null)
                {
                    NAME = Request.QueryString["NAME"].ToString();
                }
                if (Request.QueryString["FN"] != null)
                {
                    FN = Request.QueryString["FN"].ToString();
                }
            }

            else if (RptName == "RptGLWiseTransDetails.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    FBC = Request.QueryString["BranchID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    PT = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
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

            else if (RptName == "RptBrWiseGLDeatails.rdlc" || RptName == "RptBrWiseGLSummry.rdlc" || RptName == "RptOfficeGLDetails.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    FBC = Request.QueryString["BranchID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    PT = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
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

            else if (RptName == "RptGLWiseTransMonthWise.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    FBC = Request.QueryString["BranchID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    PT = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
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

            else if (RptName == "RptDormantAcList.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    FBC = Request.QueryString["BranchID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    PT = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["IntRate"] != null)
                {
                    FL = Request.QueryString["IntRate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["IntRate"] != null)
                {
                    FL = Request.QueryString["IntRate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Amount"] != null)
                {
                    Amt = Request.QueryString["Amount"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptTransSubGlMonthWise.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    FBC = Request.QueryString["BranchID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    PT = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
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

            else if (RptName == "RptIntimationNotice_Sroword.rdlc" || RptName == "RptProtectionNotice_Sroword.rdlc" || RptName == "RptPossessionNotice_Sroword.rdlc" || RptName == "RptUpsetPrizeNotice_Sroword.rdlc" || RptName == "RptUpsetCoverletter_Sroword.rdlc" || RptName == "RptPublicLetter_Sroword.rdlc" || RptName == "RptSushilLetter_Sroword.rdlc" || RptName == "RptTermCondition_Notice.rdlc" || RptName == "RptTenderForm_Noticeword.rdlc" || RptName == "RptAuction_BLetterword.rdlc" || RptName == "AuctionNotice(marathi)word.rdl" || RptName == "RptIntimation_Chequewordword.rdlc" || RptName == "RptIntimation_ToSocityword.rdlc" || RptName == "RptExecutionChargesLetter_ToSocityword.rdlc" || RptName == "Rpt31Remainder_Noticeword.rdlc" || RptName == "RptFinalIntimationLetterword.rdlc" || RptName == "AuctionNotice(marathi)word.rdlc" || RptName == "RPTPublicNotice(E_M)word.rdlc" || RptName == "RPTproposalforsaleword.rdlc" || RptName == "RptAutionMarathiword.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AddFlag"] != null)
                {
                    AddFlag = Request.QueryString["AddFlag"].ToString().Replace("%27", "");
                }
            }


            else if (RptName == "RptDemandNotice_Sroword.rdlc" || RptName == "RptBeforeAttchment_Sroword.rdlc" || RptName == "RptAttchment_Sroword.rdlc" || RptName == "RptVisit_Sroword.rdlc" || RptName == "RptSymbolicNotice_Sroword.rdlc" || RptName == "RptPropertyNotice_Sroword.rdlc" || RptName == "RptAccAttchNotice_Sroword.rdlc" || RptName == "RptTermCondition_Noticeword.rdlc" || RptName == "RptTenderForm_Noticeword.rdlc" || RptName == "RptPublicLetterNotice_Sroword.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate1"] != null)
                {
                    TDATE = Request.QueryString["Edate1"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AddFlag"] != null)
                {
                    AddFlag = Request.QueryString["AddFlag"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RptIntimationNotice_Sroword.rdlc" || RptName == "RptProtectionNotice_Sroword.rdlc" || RptName == "RptPossessionNotice_Sroword.rdlc" || RptName == "RptSushilLetter_Sroword.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AddFlag"] != null)
                {
                    AddFlag = Request.QueryString["AddFlag"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RptTransSummaryMonthWise.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    FBC = Request.QueryString["BranchID"].ToString().Replace("%27", "");
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
            else if (RptName == "RptOfficeGLWiseTransDetails.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    FBC = Request.QueryString["BranchID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    PT = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
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
            else if (RptName == "RptGenralLedgerWise_BrAdj.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    FBC = Request.QueryString["BranchID"].ToString().Replace("%27", "");
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
            else if (RptName == "RptOfficeGLWiseTransSumary.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    FBC = Request.QueryString["BranchID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    PT = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
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
            //For Vault Cash Denomination
            else if (RptName == "RPTCashDenom.rdlc")
            {
                if (Request.QueryString["FBC"] != null)
                {
                    FBRCD = Request.QueryString["FBC"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBC"] != null)
                {
                    TBRCD = Request.QueryString["TBC"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FD"] != null)
                {
                    AsOnDate = Request.QueryString["FD"].ToString().Replace("%27", "");
                }

            }


            else if (RptName == "RptDepositReg.rdlc") //------- 
            {
                if (Request.QueryString["PType"] != null)
                {
                    PT = Request.QueryString["PType"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Tdate"] != null)
                {
                    FDate = Request.QueryString["Tdate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptDepositReg_Category.rdlc") //------- 
            {
                if (Request.QueryString["PType"] != null)
                {
                    PT = Request.QueryString["PType"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Tdate"] != null)
                {
                    FDate = Request.QueryString["Tdate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptSmsMstReport.rdlc") //------- SMS REPORT ANKITA 13/05/2017
            {
                if (Request.QueryString["FDATE"] != null)
                {
                    FDATE = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDATE = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MOBILE"] != null)
                {
                    MOBILE = Request.QueryString["MOBILE"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptCustDetails.rdlc")   //// Added by ankita on 19/06/2017 to display customer report
            {
                if (Request.QueryString["FDATE"] != null)
                {
                    FDATE = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDATE = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }

            }

            else if (RptName == "RptMenu.rdlc")
            {
                RdlcPrint.Visible = true;
                DataTable DT1 = new DataTable();
                DT1 = LG.GetBankName(Session["BRCD"].ToString());
                if (DT1.Rows.Count > 0)
                {
                    BName = DT1.Rows[0]["BankName"].ToString();
                    BRName = DT1.Rows[0]["BranchName"].ToString();
                }
            }


            else if (RptName == "RptLoanReg.rdlc") //-------
            {
                if (Request.QueryString["PType"] != null)
                {
                    PT = Request.QueryString["PType"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Tdate"] != null)
                {
                    FDate = Request.QueryString["Tdate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
            }

            // Deposit Maturity


            //Outward Register
            else if (RptName == "RptOutRegister.rdlc")
            {
                if (Request.QueryString["ToBankCode"] != null)
                {
                    TBKcode = Request.QueryString["ToBankCode"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["FBankCode"] != null)
                {
                    FBKcode = Request.QueryString["FBankCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
            }

            //Excess Cash Hold
            else if (RptName == "RptExcessCashHold.rdlc")
            {
                if (Request.QueryString["FDATE"] != null)
                {
                    FDate = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["USERNAME"] != null)
                {
                    UName = Request.QueryString["USERNAME"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EDT"] != null)
                {
                    EDT = Request.QueryString["EDT"].ToString().Replace("%27", "");
                }
            }

            //ALL OK REPORT 
            else if (RptName == "RptAllOK.rdlc")
            {
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EXPF"] != null)
                {
                    ExportT = Request.QueryString["EXPF"].ToString();
                }
            }


            //Cash Book
            else if (RptName == "RptCashBook.rdlc" || RptName == "RptCashBook_ALLDetails.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptcashBookSummary.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptAVS5115.rdlc")//Dhanya Shetty//04/04/2018
            {
                if (Request.QueryString["FBrcd"] != null)
                {
                    FBrcd = Request.QueryString["FBrcd"].ToString();
                }
                if (Request.QueryString["TBrcd"] != null)
                {
                    TBrcd = Request.QueryString["TBrcd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Prd"] != null)
                {
                    Prd = Request.QueryString["Prd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Month"] != null)
                {
                    Month = Request.QueryString["Month"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Year"] != null)
                {
                    Year = Request.QueryString["Year"].ToString().Replace("%27", "");
                }


            }
            else if (RptName == "RptAgent.rdlc")
            {
                if (Request.QueryString["Flag"] != null)
                {
                    FL = Request.QueryString["Flag"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    Productcode = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptShareCloseAccDetails.rdlc")
            {

                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptUserReportAll.rdlc")
            {
                if (Request.QueryString["MID"] != null)
                {
                    MID = Request.QueryString["MID"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RptPostCommision.rdlc")
            {

                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                //if (Request.QueryString["P_CODE"] != null)
                //{
                //    P_CODE = Request.QueryString["P_CODE"].ToString().Replace("%27", "");
                //}
                //if (Request.QueryString["ProdCode"] != null)
                //{
                //    Productcode = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
                //}
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RptDailyAgentSlab.rdlc")
            {
                if (Request.QueryString["AsOnDate"] != null)
                {
                    FDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AgentCode"] != null)
                {
                    Productcode = Request.QueryString["AgentCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptCDRatio.rdlc")
            {
                if (Request.QueryString["AsOnDate"] != null)
                {
                    FDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Flag"] != null)
                {
                    FPRCD = Request.QueryString["Flag"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptShareMaster.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDate = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EDATE"] != null)
                {
                    Edate = Request.QueryString["EDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UName"] != null)
                {
                    UName = Request.QueryString["UName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ApplType"] != null)
                {
                    Type = Request.QueryString["ApplType"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptShareMismatchList.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDate = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EDATE"] != null)
                {
                    Edate = Request.QueryString["EDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UName"] != null)
                {
                    UName = Request.QueryString["UName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ApplType"] != null)
                {
                    Type = Request.QueryString["ApplType"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptLoanClose.rdlc" || RptName == "RptloanCityWise.rdlc")
            {
                if (Request.QueryString["FDATE"] != null)
                {
                    FDate = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Flag"] != null)
                {
                    FPRCD = Request.QueryString["Flag"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptBankRecosile2.rdlc")
            {
                if (Request.QueryString["Type"] != null)
                {
                    FL = Request.QueryString["Type"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDT"] != null)
                {
                    FDT = Request.QueryString["FDT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDT"] != null)
                {
                    TDT = Request.QueryString["TDT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["Prod"] != null)
                {
                    ProdCode = Request.QueryString["Prod"].ToString();
                }
            }
            else if (RptName == "RptLoanAmountWise.rdlc")
            {
                if (Request.QueryString["FDATE"] != null)
                {
                    FDate = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FAMT"] != null)
                {
                    FPRCD = Request.QueryString["FAMT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TAMT"] != null)
                {
                    TPRCD = Request.QueryString["TAMT"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptShareNomi.rdlc")
            {
                if (Request.QueryString["FDATE"] != null)
                {
                    FDate = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }

            }
            else if (RptName == "RptDDSToLoan.rdlc")
            {
                if (Request.QueryString["GlCode"] != null)
                {
                    FPRCD = Request.QueryString["GlCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }

            }
            else if (RptName == "RptShareBalList.rdlc")
            {
                if (Request.QueryString["FDATE"] != null)
                {
                    FDate = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }

            }
            else if (RptName == "Rpt_AVS0001.rdlc")//Dipali Nagare//22-07-2017//For ShareRegister
            {
                {
                    if (Request.QueryString["FDate"] != null)
                    {
                        FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                    }
                    if (Request.QueryString["TDate"] != null)
                    {
                        TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                    }

                    if (Request.QueryString["UserName"] != null)
                    {
                        UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                    }
                    if (Request.QueryString["ProdCode"] != null)
                    {
                        Productcode = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
                    }
                    if (Request.QueryString["BRCD"] != null)
                    {
                        BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                    }
                }
            }

            else if (RptName == "Rpt_AVS0004.rdlc")//Dipali Nagare//28-07-2017//For ShareRegister refund
            {
                {
                    if (Request.QueryString["FDate"] != null)
                    {
                        FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                    }
                    if (Request.QueryString["TDate"] != null)
                    {
                        TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                    }

                    if (Request.QueryString["UserName"] != null)
                    {
                        UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                    }
                    if (Request.QueryString["ProdCode"] != null)
                    {
                        Productcode = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
                    }
                    if (Request.QueryString["BRCD"] != null)
                    {
                        BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                    }
                }
            }
            else if (RptName == "RptShareCerti.rdlc" || RptName == "RptShareCerti_Marathwada.rdlc" || RptName == "RptShrYSPM.rdlc" || RptName == "RptShareAjinkyatara.rdlc")
            {

                if (Request.QueryString["AccNo"] != null)
                {
                    AccNo = Request.QueryString["AccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EntryDate"] != null)
                {
                    EntryDate = Request.QueryString["EntryDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MID"] != null)
                {
                    MID = Request.QueryString["MID"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptShareCerti_MarathwadaAddshr.rdlc" || RptName == "RptShrYSPMAddShr.rdlc")
            {
                if (Request.QueryString["EntryDate"] != null)
                {
                    EntryDate = Request.QueryString["EntryDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MID"] != null)
                {
                    MID = Request.QueryString["MID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AccNo"] != null)
                {
                    AccNo = Request.QueryString["AccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["CerNo"] != null)
                {
                    Fl = Request.QueryString["CerNo"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptShareCerti_Palghar.rdlc")
            {
                if (Request.QueryString["AccNo"] != null)
                {
                    AccNo = Request.QueryString["AccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptShareCerti_ShivSamarth.rdlc")//ankita 15/09/17 shivsamarth share certi
            {
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AccNo"] != null)
                {
                    AccNo = Request.QueryString["AccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "Rpt_AVS0003.rdlc")     //Dipali Nagare//25-07-2017//For ShareRegister
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    Productcode = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }



            }

            else if (RptName == "ShareRegister.rdlc")//Dhanya Shetty//30-12-2017//For ShareRegister
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
            }


            else if (RptName == "HeadAcListVoucherTRF.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRCD"] != null)
                {
                    TPRCD = Request.QueryString["TPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TrfPRCD"] != null)
                {
                    TBRCD = Request.QueryString["TrfPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FAccNo"] != null)
                {
                    FACCNO = Request.QueryString["FAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TAccNo"] != null)
                {
                    TACCNO = Request.QueryString["TAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Amt"] != null)
                {
                    FLT = Request.QueryString["Amt"].ToString();
                }
            }
            else if (RptName == "RPTAVS5097.rdlc")//Dhanya Shetty//09-03-2018//For StockReport
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptDormantAcList.rdlc" || RptName == "RptDormantDueAcList.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    FBC = Request.QueryString["BranchID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    PT = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["IntRate"] != null)
                {
                    FL = Request.QueryString["IntRate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["IntRate"] != null)
                {
                    FL = Request.QueryString["IntRate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Amount"] != null)
                {
                    Amt = Request.QueryString["Amount"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RptMonthlyAccStat.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    Productcode = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    AccNo = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptDebitEntry.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    Productcode = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RptDailyCollection.rdlc" || RptName == "RptDailyCollection_A.rdlc") //Dhanya Shetty
            {
                if (Request.QueryString["Date"] != null)
                {
                    Date = Request.QueryString["Date"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    Productcode = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Flag"] != null)
                {
                    FL = Request.QueryString["Flag"].ToString().Replace("%27", "");
                }
            }





            else if (RptName == "RptduedateInvst.rdlc")//Dhanya Shetty
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
                if (Request.QueryString["EDAT"] != null)
                {
                    EDAT = Request.QueryString["EDAT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
            }



            else if (RptName == "RptStartDateInvst.rdlc")//Dhanya Shetty
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

                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RptAVS5027.rdlc")//Dhanya Shetty
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
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RptLogDetails.rdlc")//Dhanya Shetty//19/09/2017
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACTVT"] != null)
                {
                    ACTVT = Request.QueryString["ACTVT"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RptDefaulters.rdlc")//Dhanya Shetty for Defaulters 
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRCD"] != null)
                {
                    TPRCD = Request.QueryString["TPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FAccNo"] != null)
                {
                    FACCNO = Request.QueryString["FAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TAccNo"] != null)
                {
                    TACCNO = Request.QueryString["TAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Date"] != null)
                {
                    Date = Request.QueryString["Date"].ToString();
                }
                if (Request.QueryString["EntryDate"] != null)
                {
                    EntryDate = Request.QueryString["EntryDate"].ToString();
                }

            }
            else if (RptName == "RptFDPrint_Chikotra.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["SubGlCode"] != null)
                {
                    SGL = Request.QueryString["SubGlCode"].ToString();
                }
                if (Request.QueryString["Accno"] != null)
                {
                    accno = Request.QueryString["Accno"].ToString();
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString();
                }
            }
            else if (RptName == "RptUnverifyentry.rdlc")//Dhanya Shetty for Unverified List 
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Date"] != null)
                {
                    Date = Request.QueryString["Date"].ToString();
                }
                if (Request.QueryString["EntryDate"] != null)
                {
                    EntryDate = Request.QueryString["EntryDate"].ToString();
                }

            }

            else if (RptName == "RptInwRegAccWise.rdlc") //Dhanya 12-07-2017
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BankCode"] != null)
                {
                    FBKcode = Request.QueryString["BankCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }

            }

            else if (RptName == "RptOutwardRegAccWise.rdlc") //Dhanya 13-07-2017
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BankCode"] != null)
                {
                    FBKcode = Request.QueryString["BankCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }


            }
            else if (RptName == "RptMemberPassBook_ALL.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["CustNO"] != null)
                {
                    PT = Request.QueryString["CustNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ToCustNO"] != null)
                {
                    FL = Request.QueryString["ToCustNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    FBC = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Div"] != null)
                {
                    S1 = Request.QueryString["Div"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Dep"] != null)
                {
                    S2 = Request.QueryString["Dep"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptIWReturnPatti.rdlc") //Dhanya 12-07-2017
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BankCode"] != null)
                {
                    FBKcode = Request.QueryString["BankCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ADate"] != null)
                {
                    ADT = Request.QueryString["ADate"].ToString().Replace("%27", "");
                }
            }


            else if (RptName == "RptOutwardReturnPatti.rdlc") //Dhanya 12-07-2017
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BankCode"] != null)
                {
                    FBKcode = Request.QueryString["BankCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ADate"] != null)
                {
                    ADT = Request.QueryString["ADate"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RptAVS5048.rdlc")//Dhanya Shetty for Overdue calculation//04/10/2017
            {

                if (Request.QueryString["UserId"] != null)
                {
                    UName = Request.QueryString["UserId"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["brcd"] != null)
                {
                    BRCD = Request.QueryString["brcd"].ToString();
                }
                if (Request.QueryString["EDT"] != null)
                {
                    EDT = Request.QueryString["EDT"].ToString();
                }
            }

            else if (RptName == "RptNPARecoveryList.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRCD"] != null)
                {
                    TPRCD = Request.QueryString["TPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FAccNo"] != null)
                {
                    FACCNO = Request.QueryString["FAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TAccNo"] != null)
                {
                    TACCNO = Request.QueryString["TAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Date"] != null)
                {
                    FDT = Request.QueryString["Date"].ToString();
                }
                if (Request.QueryString["S1Type"] != null)
                {
                    S1 = Request.QueryString["S1Type"].ToString();
                }
                if (Request.QueryString["S2Type"] != null)
                {
                    S2 = Request.QueryString["S2Type"].ToString();
                }
                if (Request.QueryString["DEX"] != null)
                {
                    Flag = Request.QueryString["DEX"].ToString();
                }
            }
            else if (RptName == "RptLoanODSummaryList.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRCD"] != null)
                {
                    TPRCD = Request.QueryString["TPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FAccNo"] != null)
                {
                    FACCNO = Request.QueryString["FAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TAccNo"] != null)
                {
                    TACCNO = Request.QueryString["TAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Date"] != null)
                {
                    FDT = Request.QueryString["Date"].ToString();
                }
                if (Request.QueryString["SL"] != null)
                {
                    FL = Request.QueryString["SL"].ToString();
                }
            }
            else if (RptName == "RptBankReconDetails.rdlc" || RptName == "RptBankReconDetails_Clear.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    FBC = Request.QueryString["BranchID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    PT = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Type"] != null)
                {
                    FL = Request.QueryString["Type"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "Isp_AVS0023.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRCD"] != null)
                {
                    TPRCD = Request.QueryString["TPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FAccNo"] != null)
                {
                    FACCNO = Request.QueryString["FAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TAccNo"] != null)
                {
                    TACCNO = Request.QueryString["TAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Date"] != null)
                {
                    FDT = Request.QueryString["Date"].ToString();
                }
                if (Request.QueryString["AType"] != null)
                {
                    FL = Request.QueryString["AType"].ToString();
                }
                if (Request.QueryString["S1Type"] != null)
                {
                    S1 = Request.QueryString["S1Type"].ToString();
                }
                if (Request.QueryString["S2Type"] != null)
                {
                    S2 = Request.QueryString["S2Type"].ToString();
                }
                if (Request.QueryString["DEX"] != null)
                {
                    Flag = Request.QueryString["DEX"].ToString();
                }
            }
            else if (RptName == "RptNPADetails_1.rdlc" || RptName == "RptNPASumry_1.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRCD"] != null)
                {
                    TPRCD = Request.QueryString["TPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FAccNo"] != null)
                {
                    FACCNO = Request.QueryString["FAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TAccNo"] != null)
                {
                    TACCNO = Request.QueryString["TAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Date"] != null)
                {
                    FDT = Request.QueryString["Date"].ToString();
                }
                if (Request.QueryString["AType"] != null)
                {
                    FL = Request.QueryString["AType"].ToString();
                }
                if (Request.QueryString["S1Type"] != null)
                {
                    S1 = Request.QueryString["S1Type"].ToString();
                }
                if (Request.QueryString["S2Type"] != null)
                {
                    S2 = Request.QueryString["S2Type"].ToString();
                }
                if (Request.QueryString["DEX"] != null)
                {
                    Flag = Request.QueryString["DEX"].ToString();
                }
            }
            else if (RptName == "RptNPASelectType_1.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRCD"] != null)
                {
                    TPRCD = Request.QueryString["TPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FAccNo"] != null)
                {
                    FACCNO = Request.QueryString["FAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TAccNo"] != null)
                {
                    TACCNO = Request.QueryString["TAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Date"] != null)
                {
                    FDT = Request.QueryString["Date"].ToString();
                }
                if (Request.QueryString["AType"] != null)
                {
                    FL = Request.QueryString["AType"].ToString();
                }
                if (Request.QueryString["S1Type"] != null)
                {
                    S1 = Request.QueryString["S1Type"].ToString();
                }
                if (Request.QueryString["S2Type"] != null)
                {
                    S2 = Request.QueryString["S2Type"].ToString();
                }
                if (Request.QueryString["DEX"] != null)
                {
                    Flag = Request.QueryString["DEX"].ToString();
                }
                if (Request.QueryString["NPAType"] != null)
                {
                    FLT = Request.QueryString["NPAType"].ToString();
                }

            }



            if (RptName == "RptBranchWiseDP.rdlc")
            {
                if (Request.QueryString["BrCode"] != null)
                {
                    BranchID = Request.QueryString["BrCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Type"] != null)
                {
                    FL = Request.QueryString["Type"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
            }

            //  BranchWise Deposit With Previous Month Report
            if (RptName == "RptBranchWiseDP_PRCR.rdlc")
            {
                if (Request.QueryString["BrCode"] != null)
                {
                    BranchID = Request.QueryString["BrCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Type"] != null)
                {
                    FL = Request.QueryString["Type"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
            }

            //  BranchWise Loan Report
            if (RptName == "RptBranchWiseLoanList.rdlc")
            {
                if (Request.QueryString["BrCode"] != null)
                {
                    BranchID = Request.QueryString["BrCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Type"] != null)
                {
                    FL = Request.QueryString["Type"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
            }

            //  BranchWise Loan With Previous Month Report
            //if (RptName == "RptBranchWiseLoanList_PrCr.rdlc")
            //{
            //    if (Request.QueryString["BrCode"] != null)
            //    {
            //        BranchID = Request.QueryString["BrCode"].ToString().Replace("%27", "");
            //    }
            //    if (Request.QueryString["Type"] != null)
            //    {
            //        FL = Request.QueryString["Type"].ToString().Replace("%27", "");
            //    }
            //    if (Request.QueryString["AsOnDate"] != null)
            //    {
            //        AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
            //    }
            //}

            if (RptName == "RptBranchWiseLoanList_PrCr.rdlc" || RptName == "RptBranchWiseLoanDT_PrCr.rdlc")
            {
                if (Request.QueryString["BrCode"] != null)
                {
                    BranchID = Request.QueryString["BrCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Type"] != null)
                {
                    FL = Request.QueryString["Type"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptODlIstDivWise_TZMP.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    BRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRCD"] != null)
                {
                    TPRCD = Request.QueryString["TPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Div"] != null)
                {
                    FL = Request.QueryString["Div"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Dep"] != null)
                {
                    FLT = Request.QueryString["Dep"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptAcctypeWiseDPSumry.rdlc")
            {
                if (Request.QueryString["FromBrcd"] != null)
                {
                    FBRCD = Request.QueryString["FromBrcd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ToBrcd"] != null)
                {
                    TBRCD = Request.QueryString["ToBrcd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FromSubGL"] != null)
                {
                    FPRCD = Request.QueryString["FromSubGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    FDT = Request.QueryString["AsOnDate"].ToString();
                }
                if (Request.QueryString["Flag"] != null)
                {
                    FL = Request.QueryString["Flag"].ToString();
                }
            }

            else if (RptName == "Isp_AVS0015.rdlc") // Classification Of OD
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRCD"] != null)
                {
                    TPRCD = Request.QueryString["TPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Date"] != null)
                {
                    FDT = Request.QueryString["Date"].ToString();
                }
                if (Request.QueryString["Flag"] != null)
                {
                    FL = Request.QueryString["Flag"].ToString();
                }
            }
            else if (RptName == "LNODPeriodWiseSumry.rdlc") // Classification Of OD
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRCD"] != null)
                {
                    TPRCD = Request.QueryString["TPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Date"] != null)
                {
                    FDT = Request.QueryString["Date"].ToString();
                }
                if (Request.QueryString["Flag"] != null)
                {
                    FL = Request.QueryString["Flag"].ToString();
                }
            }


            else if (RptName == "RptNPADetails_1.rdlc" || RptName == "RptNPASumry_1.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRCD"] != null)
                {
                    TPRCD = Request.QueryString["TPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FAccNo"] != null)
                {
                    FACCNO = Request.QueryString["FAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TAccNo"] != null)
                {
                    TACCNO = Request.QueryString["TAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Date"] != null)
                {
                    FDT = Request.QueryString["Date"].ToString();
                }
                if (Request.QueryString["AType"] != null)
                {
                    FL = Request.QueryString["AType"].ToString();
                }
                if (Request.QueryString["S1Type"] != null)
                {
                    S1 = Request.QueryString["S1Type"].ToString();
                }
                if (Request.QueryString["S2Type"] != null)
                {
                    S2 = Request.QueryString["S2Type"].ToString();
                }
                if (Request.QueryString["DEX"] != null)
                {
                    Flag = Request.QueryString["DEX"].ToString();
                }
            }

            else if (RptName == "RptIntRateSummaryDPList.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PrdCd"] != null)
                {
                    FPRCD = Request.QueryString["PrdCd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    FDT = Request.QueryString["AsOnDate"].ToString();
                }
            }
            else if (RptName == "RptIntRateSummaryDPList_DT.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PrdCd"] != null)
                {
                    FPRCD = Request.QueryString["PrdCd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    FDT = Request.QueryString["AsOnDate"].ToString();
                }
                if (Request.QueryString["FInTRate"] != null)
                {
                    Flag = Request.QueryString["FInTRate"].ToString();
                }
                if (Request.QueryString["TInTRate"] != null)
                {
                    FLT = Request.QueryString["TInTRate"].ToString();
                }
            }
            else if (RptName == "RptIntRateSummaryLoansList.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PrdCd"] != null)
                {
                    FPRCD = Request.QueryString["PrdCd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    FDT = Request.QueryString["AsOnDate"].ToString();
                }
            }

            else if (RptName == "RptPLALLBrReport.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    FDT = Request.QueryString["AsOnDate"].ToString();
                }
            }
            else if (RptName == "RptIntRateWiseDPSumry.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PrdCd"] != null)
                {
                    FPRCD = Request.QueryString["PrdCd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    FDT = Request.QueryString["AsOnDate"].ToString();
                }
                if (Request.QueryString["Flag"] != null)
                {
                    FL = Request.QueryString["Flag"].ToString();
                }
            }
            else if (RptName == "RptInvBalList.rdlc")////////Ankita Ghadage 31/05/2017 for investment balance list
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["EDAT"] != null)
                {
                    EDAT = Request.QueryString["EDAT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptSB_INTCalcPara.rdlc")
            {
                if (Request.QueryString["BC"] != null)
                {
                    BRCD = Request.QueryString["BC"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PC"] != null)
                {
                    Prd = Request.QueryString["PC"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptCloseInvList.rdlc")////////Ankita Ghadage 01/06/2017 for Closed investment list
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
                if (Request.QueryString["EDAT"] != null)
                {
                    EDAT = Request.QueryString["EDAT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
            }



                //for check account limit....

            else if (RptName == "RptCheckAccLimit.rdlc")
            {
                if (Request.QueryString["FLAG"] != null)
                {
                    FL = Request.QueryString["FLAG"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    Productcode = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["P_CODE"] != null)
                {
                    P_CODE = Request.QueryString["P_CODE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
            }


            else if (RptName == "RptShivCheck.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    Productcode = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["P_CODE"] != null)
                {
                    P_CODE = Request.QueryString["P_CODE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
            }


            //  27/12/2018
            if (RptName == "RptNonMemList.rdlc")
            {
                if (Request.QueryString["Brcd"] != null)
                {
                    BRCD = Request.QueryString["Brcd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Product"] != null)
                {
                    FL = Request.QueryString["Product"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsonDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsonDate"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "InvestmentRpt.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RptSroMonthlyReport.rdlc")
            {
                if (Request.QueryString["FLAG"] != null)
                {
                    FL = Request.QueryString["FLAG"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FSRNO"] != null)
                {
                    FBRCD = Request.QueryString["FSRNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TSRNO"] != null)
                {
                    TBRCD = Request.QueryString["TSRNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDATE = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDATE = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RPTSUMMARYSROM.rdlc" || RptName == "RPTMONTHLYSROREPORT.rdlc")
            {
                if (Request.QueryString["FLAG"] != null)
                {
                    FL = Request.QueryString["FLAG"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FSRNO"] != null)
                {
                    FBRCD = Request.QueryString["FSRNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TSRNO"] != null)
                {
                    TBRCD = Request.QueryString["TSRNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDATE = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDATE = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptLienMark.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Pcode"] != null)
                {
                    Productcode = Request.QueryString["Pcode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }

            }
            else if (RptName == "RptLienMarkLientype.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Pcode"] != null)
                {
                    Productcode = Request.QueryString["Pcode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD1"] != null)
                {
                    BRCD1 = Request.QueryString["BRCD1"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD2"] != null)
                {
                    BRCD2 = Request.QueryString["BRCD2"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD2"] != null)
                {
                    LIENTYPE = Request.QueryString["LIENTYPE"].ToString().Replace("%27", "");
                }
            }
            //Cash Postion
            else if (RptName == "RptCashPostionReport.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptChairmanReport.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
            }
            ////CASH LIMIT REPORT --ankita 04/05/2017
            else if (RptName == "RptCashLimit.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EDATE"] != null)
                {
                    AsOnDate = Request.QueryString["EDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SUBGLCODE"] != null)
                {
                    Subgl = Request.QueryString["SUBGLCODE"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptClearngMemoList.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
            }

            if (RptName == "RptLoansSlabWiseDT.rdlc")
            {
                if (Request.QueryString["FromBrcd"] != null)
                {
                    BRCD = Request.QueryString["FromBrcd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptLoansSlabWise.rdlc")
            {
                if (Request.QueryString["FromBrcd"] != null)
                {
                    BRCD = Request.QueryString["FromBrcd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "Isp_AVS0029.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
            }
            // cash postion
            //LOAN OVERDUE
            else if (RptName == "RptLoanOverdue.rdlc")
            {
                if (Request.QueryString["Date"] != null)
                {
                    OnDate = Request.QueryString["Date"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["brcd"] != null)
                {
                    BRCD = Request.QueryString["brcd"].ToString();
                }
                if (Request.QueryString["EntryDate"] != null)
                {
                    EDT = Request.QueryString["EntryDate"].ToString();
                }
                if (Request.QueryString["SUBGL"] != null)
                {
                    Subgl = Request.QueryString["SUBGL"].ToString();
                }
                if (Request.QueryString["GLNAME"] != null)
                {
                    glname = Request.QueryString["GLNAME"].ToString();
                }
                if (Request.QueryString["SL"] != null)
                {
                    SL = Request.QueryString["SL"].ToString();
                }

            }

            else if (RptName == "RptDashLoanOverdue.rdlc")//Dhanya Shetty for dashboard report
            {
                if (Request.QueryString["Date"] != null)
                {
                    OnDate = Request.QueryString["Date"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["brcd"] != null)
                {
                    BRCD = Request.QueryString["brcd"].ToString();
                }
                if (Request.QueryString["EntryDate"] != null)
                {
                    EDT = Request.QueryString["EntryDate"].ToString();
                }
                if (Request.QueryString["SUBGL"] != null)
                {
                    Subgl = Request.QueryString["SUBGL"].ToString();
                }
                if (Request.QueryString["GLNAME"] != null)
                {
                    glname = Request.QueryString["GLNAME"].ToString();
                }
                if (Request.QueryString["SL"] != null)
                {
                    SL = Request.QueryString["SL"].ToString();
                }

            }


            else if (RptName == "RptOverDueSummary.rdlc")//Dhanya Shetty for Overdue summary
            {
                if (Request.QueryString["Date"] != null)
                {
                    OnDate = Request.QueryString["Date"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["brcd"] != null)
                {
                    BRCD = Request.QueryString["brcd"].ToString();
                }
                if (Request.QueryString["EntryDate"] != null)
                {
                    EDT = Request.QueryString["EntryDate"].ToString();
                }
                if (Request.QueryString["SL"] != null)
                {
                    SL = Request.QueryString["SL"].ToString();
                }
            }

            else if (RptName == "RptMasterListing.rdlc")//Dhanya Shetty //17-06-2017//MasterListing
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    ProdCode = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AccNo"] != null)
                {
                    AccNo = Request.QueryString["AccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Status"] != null)
                {
                    Status = Request.QueryString["Status"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RptLoanOverdue_New.rdlc")
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

            else if (RptName == "RptDeLoanSummery.rdlc" || RptName == "RptDLienDetails.rdlc")  //added by ashok misal for loan deposit lein summary report
            {
                if (Request.QueryString["FDate"] != null)
                {
                    fdate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Fbrcd"] != null)
                {
                    FBRCD = Request.QueryString["Fbrcd"].ToString();
                }
                if (Request.QueryString["Tbrcd"] != null)
                {
                    TBRCD = Request.QueryString["Tbrcd"].ToString();
                }

                if (Request.QueryString["SL"] != null)
                {
                    SL = Request.QueryString["SL"].ToString();
                }

            }
            else if (RptName == "RptLoanOverdue_Only.rdlc")
            {
                if (Request.QueryString["Date"] != null)
                {
                    OnDate = Request.QueryString["Date"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["brcd"] != null)
                {
                    BRCD = Request.QueryString["brcd"].ToString();
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

            //NPA Report
            else if (RptName == "RptODNpaReport.rdlc")
            {
                if (Request.QueryString["Date"] != null)
                {
                    OnDate = Request.QueryString["Date"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["brcd"] != null)
                {
                    BRCD = Request.QueryString["brcd"].ToString();
                }
                if (Request.QueryString["EntryDate"] != null)
                {
                    EDT = Request.QueryString["EntryDate"].ToString();
                }
                if (Request.QueryString["GLNAME"] != null)
                {
                    glname = Request.QueryString["GLNAME"].ToString();
                }
                if (Request.QueryString["Flag"] != null)
                {
                    SL = Request.QueryString["Flag"].ToString();
                }
                if (SL == "S")
                {
                    if (Request.QueryString["SUBGL"] != null)
                    {
                        Subgl = Request.QueryString["SUBGL"].ToString();
                    }
                }
            }


             //NPA Report
            else if (RptName == "RptNPAReg1.rdlc")
            {
                if (Request.QueryString["Date"] != null)
                {
                    OnDate = Request.QueryString["Date"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["brcd"] != null)
                {
                    BRCD = Request.QueryString["brcd"].ToString();
                }
                if (Request.QueryString["EntryDate"] != null)
                {
                    EDT = Request.QueryString["EntryDate"].ToString();
                }
                if (Request.QueryString["GLNAME"] != null)
                {
                    glname = Request.QueryString["GLNAME"].ToString();
                }
                if (Request.QueryString["Flag"] != null)
                {
                    SL = Request.QueryString["Flag"].ToString();
                }
                if (Request.QueryString["SFlag1"] != null)
                {
                    SFL1 = Request.QueryString["SFlag1"].ToString();
                }
                if (Request.QueryString["SFlag2"] != null)
                {
                    SFL2 = Request.QueryString["SFlag2"].ToString();
                }

                if (Request.QueryString["FSUBGL"] != null)
                {
                    Fsubgl = Request.QueryString["FSUBGL"].ToString();
                }
                if (Request.QueryString["TSUBGL"] != null)
                {
                    Tsubgl = Request.QueryString["TSUBGL"].ToString();
                }

            }




            //CDR Report
            else if (RptName == "RptCDR.rdlc" || RptName == "RptCDRSummary.rdlc")
            {
                if (Request.QueryString["Date"] != null)
                {
                    OnDate = Request.QueryString["Date"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["brcd"] != null)
                {
                    BRCD = Request.QueryString["brcd"].ToString();
                }
                if (Request.QueryString["EntryDate"] != null)
                {
                    EDT = Request.QueryString["EntryDate"].ToString();
                }
            }

            //NPA REPORT
            else if (RptName == "RptNPA1Report.rdlc")
            {
                if (Request.QueryString["Date"] != null)
                {
                    OnDate = Request.QueryString["Date"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["brcd"] != null)
                {
                    BRCD = Request.QueryString["brcd"].ToString();
                }
                if (Request.QueryString["EntryDate"] != null)
                {
                    EDT = Request.QueryString["EntryDate"].ToString();
                }
                if (Request.QueryString["SUBGL"] != null)
                {
                    Subgl = Request.QueryString["SUBGL"].ToString();
                }
                if (Request.QueryString["SL"] != null)
                {
                    SL = Request.QueryString["SL"].ToString();
                }

            }

            else if (RptName == "RptReceiptPrintPal.rdlc" || RptName == "RptReceiptPrintsai.rdlc" || RptName == "RptReceiptPrintHSFM.rdlc" || RptName == "RptReceiptPrintHSFM_ShareApp.rdlc")//Dhanya Shetty//28/09/2017
            {

                if (Request.QueryString["EDT"] != null)
                {
                    EDT = Request.QueryString["EDT"].ToString();
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["CNO"] != null)
                {
                    CNO = Request.QueryString["CNO"].ToString();
                }

                if (Request.QueryString["FN"] != null)
                {
                    FN = Request.QueryString["FN"].ToString();
                }

                if (Request.QueryString["Acc"] != null)
                {
                    Acc = Request.QueryString["Acc"].ToString();
                }

                if (Request.QueryString["SCROLLNO"] != null)
                {
                    TBKcode = Request.QueryString["SCROLLNO"].ToString();
                }
                if (Request.QueryString["SETNO"] != null)
                {
                    SETNO = Request.QueryString["SETNO"].ToString();
                }

            }



            else if (RptName == "RPTSTATEMENTACCREC.rdlc")//Dhanya Shetty//28/09/2017
            {
                if (Request.QueryString["CASENO"] != null)
                {
                    SETNO = Request.QueryString["CASENO"].ToString();
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EDT"] != null)
                {
                    EDT = Request.QueryString["EDT"].ToString();
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["CASEY"] != null)
                {
                    CASEY = Request.QueryString["CASEY"].ToString();
                }
                if (Request.QueryString["NAME"] != null)
                {
                    NAME = Request.QueryString["NAME"].ToString();
                }
                if (Request.QueryString["FN"] != null)
                {
                    FN = Request.QueryString["FN"].ToString();
                }
                if (Request.QueryString["Subg"] != null)
                {
                    Subg = Request.QueryString["Subg"].ToString();
                }
                if (Request.QueryString["Acc"] != null)
                {
                    Acc = Request.QueryString["Acc"].ToString();
                }

                if (Request.QueryString["SCROLLNO"] != null)
                {
                    TBKcode = Request.QueryString["SCROLLNO"].ToString();
                }

            }

            //SI DDS TO LOAN
            else if (RptName == "RptSI_DDStoLoan.rdlc")
            {

                if (Request.QueryString["FLAG"] != null)
                {
                    FL = Request.QueryString["FLAG"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FromDate"] != null)
                {
                    FDate = Request.QueryString["FromDate"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ToDate"] != null)
                {
                    TDate = Request.QueryString["ToDate"].ToString();
                }
                if (Request.QueryString["Ddlcheck"] != null)
                {
                    DdlStatus = Request.QueryString["Ddlcheck"].ToString();
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

            }
            else if (RptName == "RptDayBook_SHIV.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["DEX"] != null)
                {
                    FL = Request.QueryString["DEX"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["IEX"] != null)
                {
                    FLT = Request.QueryString["IEX"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptMemberPassBookFamer.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["CustNO"] != null)
                {
                    PT = Request.QueryString["CustNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    FBC = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
            }

            //Details DDSTOLOAN
            else if (RptName == "RptDetailsDDSTOLOAN.rdlc")
            {

                if (Request.QueryString["FLAG"] != null)
                {
                    FL = Request.QueryString["FLAG"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FromDate"] != null)
                {
                    FDate = Request.QueryString["FromDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ToDate"] != null)
                {
                    TDate = Request.QueryString["ToDate"].ToString();
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
            }



            else if (RptName == "RptGLBalanceDataTrf.rdlc") //-------
            {
                if (Request.QueryString["AsOnDate"] != null)
                {
                    FDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBrcd"] != null)
                {
                    FBRCD = Request.QueryString["FBrcd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBrcd"] != null)
                {
                    TBRCD = Request.QueryString["TBrcd"].ToString().Replace("%27", "");
                }
            }


            else if (RptName == "RptDailyBalanceLessThenClg.rdlc") //-------
            {
                if (Request.QueryString["AsOnDate"] != null)
                {
                    FDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBrcd"] != null)
                {
                    FBRCD = Request.QueryString["FBrcd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Product"] != null)
                {
                    Prd = Request.QueryString["Product"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Period"] != null)
                {
                    FL = Request.QueryString["Period"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptCDRatioReport.rdlc" || RptName == "RptCDRatioReport_DT.rdlc") //-------
            {
                if (Request.QueryString["Date"] != null)
                {
                    FDate = Request.QueryString["Date"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Brcd"] != null)
                {
                    BRCD = Request.QueryString["Brcd"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptRisktypeDetails.rdlc") //-------
            {
                if (Request.QueryString["Type"] != null)
                {
                    Type = Request.QueryString["Type"].ToString().Replace("%27", "");
                }


            }
            //Inward Register
            else if (RptName == "RptInwardReg.rdlc")
            {
                if (Request.QueryString["FBankCode"] != null)
                {
                    FBKcode = Request.QueryString["FBankCode"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["ToBankCode"] != null)
                {
                    TBKcode = Request.QueryString["ToBankCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDate = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

            }

                //rakesh15012019
            else if (RptName == "RptStaffMemPassbook.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["CustNO"] != null)
                {
                    PT = Request.QueryString["CustNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    FBC = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
            }

            //DDS Register- Amruta 15/04/2017
            else if (RptName == "RptDDSR.rdlc") //-------
            {
                if (Request.QueryString["Brcd"] != null)
                {
                    BRCD = Request.QueryString["Brcd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDATE = Request.QueryString["FDate"].ToString();
                }
                if (Request.QueryString["PType"] != null)
                {
                    Type = Request.QueryString["PType"].ToString();
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString();
                }
            }

                //PL Transfer- Amruta 19/04/2017
            else if (RptName == "RptPLTransfer.rdlc") //-------
            {
                if (Request.QueryString["EDate"] != null)
                {
                    EDT = Request.QueryString["EDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Date"] != null)
                {
                    FDATE = Request.QueryString["Date"].ToString();
                }
                if (Request.QueryString["AC"] != null)
                {
                    AC = Request.QueryString["AC"].ToString();
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString();
                }
                if (Request.QueryString["Flag"] != null)
                {
                    FL = Request.QueryString["Flag"].ToString();
                }
            }
            else if (RptName == "RptDivIntitalize.rdlc") //-------
            {
                if (Request.QueryString["EDate"] != null)
                {
                    EDAT = Request.QueryString["EDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["GLCode"] != null)
                {
                    GL = Request.QueryString["GLCode"].ToString();
                }
                if (Request.QueryString["SubGlCode"] != null)
                {
                    Subgl = Request.QueryString["SubGlCode"].ToString();
                }
                if (Request.QueryString["MID"] != null)
                {
                    MID = Request.QueryString["MID"].ToString();
                }

            }


            //Document Register

            else if (RptName == "RptDocumentReg.rdlc")
            {
                if (Request.QueryString["ToDocType"] != null)
                {
                    ToDOCType = Request.QueryString["ToDocType"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["FromDocType"] != null)
                {
                    FromDOCType = Request.QueryString["FromDocType"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDate = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

            }




            else if (RptName == "RptGLreport.rdlc") //-------
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }


            }
            else if (RptName == "RptCutBook.rdlc" || RptName == "RptCutBookSa.rdlc" || RptName == "RptCuteBookRecSrno.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["GL"] != null)
                {
                    GL = Request.QueryString["GL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SGL"] != null)
                {
                    SGL = Request.QueryString["SGL"].ToString();
                    PT = Request.QueryString["SGL"].ToString();
                }
                if (Request.QueryString["MID"] != null)
                {
                    MID = Request.QueryString["MID"].ToString();
                }
            }
            else if (RptName == "RptCutBook_Palghar.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["GL"] != null)
                {
                    GL = Request.QueryString["GL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SGL"] != null)
                {
                    SGL = Request.QueryString["SGL"].ToString();
                    PT = Request.QueryString["SGL"].ToString();
                }
                if (Request.QueryString["MID"] != null)
                {
                    MID = Request.QueryString["MID"].ToString();
                }
            }






            else if (RptName == "NoticeB_Attasale.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AddFlag"] != null)
                {
                    AddFlag = Request.QueryString["AddFlag"].ToString().Replace("%27", "");
                }


            }
            else if (RptName == "NoticeB_Attasale1.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["NAME"] != null)
                {
                    UName = Request.QueryString["NAME"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Address"] != null)
                {
                    Address = Request.QueryString["Address"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Surity"] != null)
                {
                    CustNo = Request.QueryString["Surity"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AddFlag"] != null)
                {
                    AddFlag = Request.QueryString["AddFlag"].ToString().Replace("%27", "");
                }

            }

            else if (RptName == "RptConfiscation09.rdlc")//// Added by ankita on 09/06/2017 to display confiscation warrant
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AddFlag"] != null)
                {
                    AddFlag = Request.QueryString["AddFlag"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptPossesionNotice12.rdlc")//// Added by ankita on 10/06/2017 to display Possesion notice
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptDeclaration13.rdlc")//// Added by ankita on 10/06/2017 to display Declaration notice
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptReceiptPrintYuva1.rdlc")
            {
                if (Request.QueryString["SETNO"] != null)
                {
                    SETNO = Request.QueryString["SETNO"].ToString();
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EDT"] != null)
                {
                    EDT = Request.QueryString["EDT"].ToString();
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["CNO"] != null)
                {
                    CNO = Request.QueryString["CNO"].ToString();
                }
                if (Request.QueryString["NAME"] != null)
                {
                    NAME = Request.QueryString["NAME"].ToString();
                }
                if (Request.QueryString["FN"] != null)
                {
                    FN = Request.QueryString["FN"].ToString();
                }
                if (Request.QueryString["Subg"] != null)
                {
                    Subg = Request.QueryString["Subg"].ToString();
                }
                if (Request.QueryString["Acc"] != null)
                {
                    Acc = Request.QueryString["Acc"].ToString();
                }

            }
            else if (RptName == "PropertyRaid.rdlc") //Amruta 09/06/2017
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }


            }
            else if (RptName == "PropertyRaid2.rdlc") //Amruta 09/06/2017
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }


            }

            else if (RptName == "PropertyRaid3.rdlc") //Amruta 09/06/2017
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Surity"] != null)
                {
                    CustNo = Request.QueryString["Surity"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Village"] != null)
                {
                    Village = Request.QueryString["Village"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["taluka"] != null)
                {
                    taluka = Request.QueryString["taluka"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Dist"] != null)
                {
                    Dist = Request.QueryString["Dist"].ToString().Replace("%27", "");
                }

            }
            else if (RptName == "PropertyRaid31.rdlc") //Amruta 09/06/2017
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Name"] != null)
                {
                    Name = Request.QueryString["Name"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Address"] != null)
                {
                    Address = Request.QueryString["Address"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Surity"] != null)
                {
                    CustNo = Request.QueryString["Surity"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Village"] != null)
                {
                    Village = Request.QueryString["Village"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["taluka"] != null)
                {
                    taluka = Request.QueryString["taluka"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Dist"] != null)
                {
                    Dist = Request.QueryString["Dist"].ToString().Replace("%27", "");
                }

            }

            if (RptName == "RptShareTZMP.rdlc" || RptName == "RptSanchitTZMP.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    BRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDATE = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDATE = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FAccNo"] != null)
                {
                    FACCNO = Request.QueryString["FAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TAccNo"] != null)
                {
                    TACCNO = Request.QueryString["TAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Div"] != null)
                {
                    FL = Request.QueryString["Div"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Dep"] != null)
                {
                    FLT = Request.QueryString["Dep"].ToString().Replace("%27", "");
                }
            }


            if (RptName == "RptAllLnBalListOD.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    BRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRCD"] != null)
                {
                    TPRCD = Request.QueryString["TPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
            }

            if (RptName == "RptOrderNotice.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["CustNo"] != null)
                {
                    CustNo = Request.QueryString["CustNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AddFlag"] != null)
                {
                    AddFlag = Request.QueryString["AddFlag"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Title"] != null)
                {
                    Title = Request.QueryString["Title"].ToString().Replace("%27", "");
                }


            }


            else if (RptName == "RptDivPendingList.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRCD"] != null)
                {
                    TPRCD = Request.QueryString["TPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Div"] != null)
                {
                    S1 = Request.QueryString["Div"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Dep"] != null)
                {
                    S2 = Request.QueryString["Dep"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptAVS5077.rdlc" || RptName == "RptSavingCerti.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }

            }


            else if (RptName == "RptSharesCert_SHIV.rdlc")
            {
                if (Request.QueryString["EntryDate"] != null)
                {
                    EntryDate = Request.QueryString["EntryDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MID"] != null)
                {
                    MID = Request.QueryString["MID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AccNo"] != null)
                {
                    AccNo = Request.QueryString["AccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["CerNo"] != null)
                {
                    Fl = Request.QueryString["CerNo"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptPrastav.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["CustNo"] != null)
                {
                    CustNo = Request.QueryString["CustNo"].ToString().Replace("%27", "");
                }


            }
            if (RptName == "RptRecoveryCertificateRem12.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["CustNo"] != null)
                {
                    CustNo = Request.QueryString["CustNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Name"] != null)
                {
                    Name = Request.QueryString["Name"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AddFlag"] != null)
                {
                    AddFlag = Request.QueryString["AddFlag"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Title"] != null)
                {
                    Title = Request.QueryString["Title"].ToString().Replace("%27", "");
                }

            }
            if (RptName == "RptRecoveryCertificateRem21.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["CustNo"] != null)
                {
                    CustNo = Request.QueryString["CustNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Name"] != null)
                {
                    Name = Request.QueryString["Name"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AddFlag"] != null)
                {
                    AddFlag = Request.QueryString["AddFlag"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Title"] != null)
                {
                    Title = Request.QueryString["Title"].ToString().Replace("%27", "");
                }



            }
            if (RptName == "RptOrderNotice1.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Name"] != null)
                {
                    Name = Request.QueryString["Name"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["CustNo"] != null)
                {
                    CustNo = Request.QueryString["CustNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AddFlag"] != null)
                {
                    AddFlag = Request.QueryString["AddFlag"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Title"] != null)
                {
                    Title = Request.QueryString["Title"].ToString().Replace("%27", "");
                }



            }
            else if (RptName == "RptWorkerAccountant.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Village"] != null)
                {
                    Village = Request.QueryString["Village"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["taluka"] != null)
                {
                    taluka = Request.QueryString["taluka"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Dist"] != null)
                {
                    Dist = Request.QueryString["Dist"].ToString().Replace("%27", "");
                }



            }
            else if (RptName == "RptWorkerAccountant1.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["NAME"] != null)
                {
                    Name = Request.QueryString["NAME"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Address"] != null)
                {
                    Address = Request.QueryString["Address"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Surity"] != null)
                {
                    CustNo = Request.QueryString["Surity"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Village"] != null)
                {
                    Village = Request.QueryString["Village"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["taluka"] != null)
                {
                    taluka = Request.QueryString["taluka"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Dist"] != null)
                {
                    Dist = Request.QueryString["Dist"].ToString().Replace("%27", "");
                }


            }

            if (RptName == "ISP_AVS0204.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["IntRate"] != null)
                {
                    FL = Request.QueryString["IntRate"].ToString();
                }
                if (Request.QueryString["CommRate"] != null)
                {
                    FLT = Request.QueryString["CommRate"].ToString();
                }
                if (Request.QueryString["TDSRate"] != null)
                {
                    PT = Request.QueryString["TDSRate"].ToString();
                }
            }
            else if (RptName == "RptVisitnotice.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AddFlag"] != null)
                {
                    AddFlag = Request.QueryString["AddFlag"].ToString().Replace("%27", "");
                }

            }

            else if (RptName == "RptDemandNotice.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AddFlag"] != null)
                {
                    AddFlag = Request.QueryString["AddFlag"].ToString().Replace("%27", "");
                }



            }


            else if (RptName == "RptLoanRepayCerti.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["OUTNO"] != null)
                {
                    OUTNO = Request.QueryString["OUTNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["OUTNO"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                    //1 is for Print ,2 Is for Accured
                }

            }




            else if (RptName == "RptRecoveryCertificateRem1.rdlc")//Dhanya Shetty//09-09-2017//For RecoveryCertificate 1
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();

                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["custname"] != null)
                {
                    Ncustname = Request.QueryString["custname"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Address"] != null)
                {
                    Ncustadd = Request.QueryString["Address"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AddFlag"] != null)
                {
                    AddFlag = Request.QueryString["AddFlag"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Title"] != null)
                {
                    Title = Request.QueryString["Title"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptRecoveryCertificateRem2.rdlc")//Dhanya Shetty//09-09-2017//For RecoveryCertificate 2
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();

                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["custname"] != null)
                {
                    Ncustname = Request.QueryString["custname"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Address"] != null)
                {
                    Ncustadd = Request.QueryString["Address"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AddFlag"] != null)
                {
                    AddFlag = Request.QueryString["AddFlag"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Title"] != null)
                {
                    Title = Request.QueryString["Title"].ToString().Replace("%27", "");
                }

            }
            else if (RptName == "RptLabourAccountent17.rdlc")//Dhanya Shetty//09-09-2017//For Labour Accountant
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Village"] != null)
                {
                    Village = Request.QueryString["Village"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["taluka"] != null)
                {
                    taluka = Request.QueryString["taluka"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Dist"] != null)
                {
                    Dist = Request.QueryString["Dist"].ToString().Replace("%27", "");
                }

            }


            else if (RptName == "RptCutBook_DepWise.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["GL"] != null)
                {
                    GL = Request.QueryString["GL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SGL"] != null)
                {
                    SGL = Request.QueryString["SGL"].ToString();
                    PT = Request.QueryString["SGL"].ToString();
                }
                if (Request.QueryString["MID"] != null)
                {
                    MID = Request.QueryString["MID"].ToString();
                }
            }
            else if (RptName == "RptLabourAccountent171.rdlc")//DIpali Nagare//07-07-2017//For Labour Accountant
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["NAME"] != null)
                {
                    Name = Request.QueryString["NAME"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Surity"] != null)
                {
                    CustNo = Request.QueryString["Surity"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Village"] != null)
                {
                    Village = Request.QueryString["Village"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["taluka"] != null)
                {
                    taluka = Request.QueryString["taluka"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Dist"] != null)
                {
                    Dist = Request.QueryString["Dist"].ToString().Replace("%27", "");
                }

            }
            else if (RptName == "RptLabour2Accountent17.rdlc")//Dhanya Shetty//09-09-2017//For Labour Accountant
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["custname"] != null)
                {
                    Ncustname = Request.QueryString["custname"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Address"] != null)
                {
                    Ncustadd = Request.QueryString["Address"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Surity"] != null)
                {
                    CustNo = Request.QueryString["Surity"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Village"] != null)
                {
                    Village = Request.QueryString["Village"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["taluka"] != null)
                {
                    taluka = Request.QueryString["taluka"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Dist"] != null)
                {
                    Dist = Request.QueryString["Dist"].ToString().Replace("%27", "");
                }


            }

            else if (RptName == "RptJaptiNotice.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AddFlag"] != null)
                {
                    AddFlag = Request.QueryString["AddFlag"].ToString().Replace("%27", "");
                }


            }
            else if (RptName == "RptJaptiNotice1.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }

                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Name"] != null)
                {
                    Name = Request.QueryString["Name"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Address"] != null)
                {
                    Address = Request.QueryString["Address"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Surity"] != null)
                {
                    CustNo = Request.QueryString["Surity"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AddFlag"] != null)
                {
                    AddFlag = Request.QueryString["AddFlag"].ToString().Replace("%27", "");
                }
            }


            //report for active mem
            else if (RptName == "RptActiveMem.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EDATE"] != null)
                {
                    Edate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }

            }

            else if (RptName == "RptCuteBookDetails.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SGL"] != null)
                {
                    SGL = Request.QueryString["SGL"].ToString();
                }
            }

            else if (RptName == "RptVoucherDetailsList.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    BranchID = Request.QueryString["BranchID"].ToString();
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AType"] != null)
                {
                    SGL = Request.QueryString["AType"].ToString();
                }
            }
            else if (RptName == "RptVoucherSummaryList.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    BranchID = Request.QueryString["BranchID"].ToString();
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AType"] != null)
                {
                    SGL = Request.QueryString["AType"].ToString();
                }
            }
            else if (RptName == "RptInComeTaxReport.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    BranchID = Request.QueryString["BranchID"].ToString();
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AType"] != null)
                {
                    SGL = Request.QueryString["AType"].ToString();
                }
                if (Request.QueryString["SType"] != null)
                {
                    FL = Request.QueryString["SType"].ToString();
                }
            }
            else if (RptName == "RptInComeTaxReport_SHR.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    BranchID = Request.QueryString["BranchID"].ToString();
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AType"] != null)
                {
                    SGL = Request.QueryString["AType"].ToString();
                }
                if (Request.QueryString["SType"] != null)
                {
                    FL = Request.QueryString["SType"].ToString();
                }
            }
            else if (RptName == "RptInComeTaxReport_DP.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    BranchID = Request.QueryString["BranchID"].ToString();
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AType"] != null)
                {
                    SGL = Request.QueryString["AType"].ToString();
                }
                if (Request.QueryString["SType"] != null)
                {
                    FL = Request.QueryString["SType"].ToString();
                }
            }
            else if (RptName == "RptInComeTaxReport_LN.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    BranchID = Request.QueryString["BranchID"].ToString();
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AType"] != null)
                {
                    SGL = Request.QueryString["AType"].ToString();
                }
                if (Request.QueryString["SType"] != null)
                {
                    FL = Request.QueryString["SType"].ToString();
                }
            }
            if (RptName == "RptLoanOverdueReport.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    BranchID = Request.QueryString["BranchID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ProdCode"] != null)
                {
                    FL = Request.QueryString["ProdCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    AsOnDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptVoucherPrinting.rdlc" || RptName == "RptVoucherPrinting_Eng.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    BranchID = Request.QueryString["BranchID"].ToString();
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SetNo"] != null)
                {
                    SGL = Request.QueryString["SetNo"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RptVoucherPrintingCRDR.rdlc" || RptName == "RptVoucherPrintingCRDR_Eng.rdlc" || RptName == "RptVoucherPrintingFD.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    BranchID = Request.QueryString["BranchID"].ToString();
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SetNo"] != null)
                {
                    SGL = Request.QueryString["SetNo"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptVoucherPrintRetired.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    BranchID = Request.QueryString["BranchID"].ToString();
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SetNo"] != null)
                {
                    SGL = Request.QueryString["SetNo"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptSharesApplicationList.rdlc")
            {
                if (Request.QueryString["FBrCode"] != null)
                {
                    FBRCD = Request.QueryString["FBrCode"].ToString();
                }
                if (Request.QueryString["TBrCode"] != null)
                {
                    TBRCD = Request.QueryString["TBrCode"].ToString();
                }
                if (Request.QueryString["PrCode"] != null)
                {
                    FPRCD = Request.QueryString["PrCode"].ToString();
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AppType"] != null)
                {
                    FL = Request.QueryString["AppType"].ToString();
                }
                if (Request.QueryString["AType"] != null)
                {
                    SGL = Request.QueryString["AType"].ToString();
                }
            }
            // Deposit Reciept

            else if (RptName == "RptBalanceS.rdlc")
            {
                if (Request.QueryString["DEX"] != null)
                {
                    FL = Request.QueryString["DEX"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptPBalanceS.rdlc" || RptName == "RptPBalanceS_Marathi.rdlc" || RptName == "RptPBalanceSarjudas_Marathi.rdlc")
            {
                if (Request.QueryString["DEX"] != null)
                {
                    FL = Request.QueryString["DEX"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RptPNProfitAndLoss.rdlc" || RptName == "RptPNProfitAndLoss_Marathi.rdlc" || RptName == "RptPNProfitAndLossSarjudas_Marathi.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptIncomeExpReport.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptLoanParameter.rdlc")
            {
                if (Request.QueryString["UserName"] != null)
                {
                    USERNAME = Request.QueryString["UserName"].ToString().Replace("%27", "");
                    BRCD = Session["BRCD"].ToString();
                }
            }


            else if (RptName == "RptTrialBalance.rdlc" || RptName == "RptTrialBalance_M.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString();
                }
                if (Request.QueryString["YN"] != null)
                {
                    FLT = Request.QueryString["YN"].ToString();
                }
                if (Request.QueryString["AF"] != null)
                {
                    AC = Request.QueryString["AF"].ToString();
                }
            }
            else if (RptName == "RptTrialBalance_FromTo.rdlc" || RptName == "RptTrialBal_FrmToMarathi.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString();
                }
                if (Request.QueryString["YN"] != null)
                {
                    FLT = Request.QueryString["YN"].ToString();
                }
            }
            else if (RptName == "RptTRAILBALANCE_DPLN.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString();
                }
            }
            else if (RptName == "RptNOC_Certificate.rdlc")//Pranali Pawar//For N.O.C. for Permit Renewal of Vehicle No
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["PRDCD"] != null)
                {
                    PRDCD = Request.QueryString["PRDCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    ACCNO = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SRNO"] != null)
                {
                    SRNO = Request.QueryString["SRNO"].ToString().Replace("%27", "");
                }


            }
            else if (RptName == "RptTrialBalanceSummary.rdlc" || RptName == "RptTrialBalanceSummary_M.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString();
                }
                if (Request.QueryString["YN"] != null)
                {
                    FLT = Request.QueryString["YN"].ToString();
                }
                if (Request.QueryString["AF"] != null)
                {
                    AC = Request.QueryString["AF"].ToString();
                }
                BRCD = Session["BRCD"].ToString();
            }



            else if (RptName == "RptDayBook.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["DEX"] != null)
                {
                    FL = Request.QueryString["DEX"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["IEX"] != null)
                {
                    FLT = Request.QueryString["IEX"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptDayBook_PEN.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["DEX"] != null)
                {
                    FL = Request.QueryString["DEX"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["IEX"] != null)
                {
                    FLT = Request.QueryString["IEX"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptDayBookDetails.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["DEX"] != null)
                {
                    FL = Request.QueryString["DEX"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["IEX"] != null)
                {
                    FLT = Request.QueryString["IEX"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptDayBookRegistrerDetailsSetWise.rdlc" || RptName == "RptDayBook_ALLDetails.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["DEX"] != null)
                {
                    FL = Request.QueryString["DEX"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["IEX"] != null)
                {
                    FLT = Request.QueryString["IEX"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RptDayBookReg_TZMP.rdlc" || RptName == "RptDayBookReg_FromTo.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["DEX"] != null)
                {
                    FL = Request.QueryString["DEX"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["IEX"] != null)
                {
                    FLT = Request.QueryString["IEX"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptDayBookReg_Renewal.rdlc" || RptName == "RptDayBookDP_Register.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptIWClearngDetails.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BankCode"] != null)
                {
                    FBKcode = Request.QueryString["BankCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptClgRegList.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BankCode"] != null)
                {
                    FBKcode = Request.QueryString["BankCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptCustBalWithSurity.rdlc" || RptName == "RptCustBalWithoutSurity.rdlc") //-------
            {
                if (Request.QueryString["AsOnDate"] != null)
                {
                    FDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Brcd"] != null)
                {
                    BRCD = Request.QueryString["Brcd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FCustNo"] != null)
                {
                    FCustNo = Request.QueryString["FCustNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TCustNo"] != null)
                {
                    TCustNo = Request.QueryString["TCustNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Div"] != null)
                {
                    Div = Request.QueryString["Div"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Dep"] != null)
                {
                    Dept = Request.QueryString["Dep"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptClearngRegister.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BankCode"] != null)
                {
                    FBKcode = Request.QueryString["BankCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SFL"] != null)
                {
                    SGL = Request.QueryString["SFL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptIWClearngSummary.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BankCode"] != null)
                {
                    FBKcode = Request.QueryString["BankCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptClgRegListSummary.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BankCode"] != null)
                {
                    FBKcode = Request.QueryString["BankCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptClearngReturnRegister.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["BankCode"] != null)
                {
                    FBKcode = Request.QueryString["BankCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptClassificationDPLN.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    FDT = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AccType"] != null)
                {
                    FL = Request.QueryString["AccType"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Type"] != null)
                {
                    FLT = Request.QueryString["Type"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptClassificationDPLNSumy.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    FDT = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AccType"] != null)
                {
                    FL = Request.QueryString["AccType"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Type"] != null)
                {
                    FLT = Request.QueryString["Type"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptCTR.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString();
                }
                if (Request.QueryString["FSGL"] != null)
                {
                    SUBGLFRM = Request.QueryString["FSGL"].ToString();
                }
                if (Request.QueryString["TSGL"] != null)
                {
                    SUBGLTO = Request.QueryString["TSGL"].ToString();
                }
                if (Request.QueryString["CTRL"] != null)
                {
                    CTRL = Request.QueryString["CTRL"].ToString();
                }
            }
            else if (RptName == "RptFDClassificationList.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString();
                }
                if (Request.QueryString["FSGL"] != null)
                {
                    SUBGLFRM = Request.QueryString["FSGL"].ToString();
                }
                if (Request.QueryString["TSGL"] != null)
                {
                    SUBGLTO = Request.QueryString["TSGL"].ToString();
                }
                if (Request.QueryString["FAmount"] != null)
                {
                    FBC = Request.QueryString["FAmount"].ToString();
                }
                if (Request.QueryString["TAmount"] != null)
                {
                    TBC = Request.QueryString["TAmount"].ToString();
                }
            }
            else if (RptName == "RptFormA.rdlc")
            {
                if (Request.QueryString["FGL"] != null)
                {
                    SUBGLFRM = Request.QueryString["FGL"].ToString();
                }
                if (Request.QueryString["TGL"] != null)
                {
                    SUBGLTO = Request.QueryString["TGL"].ToString();
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["CTRL"] != null)
                {
                    CTRL = Request.QueryString["CTRL"].ToString();
                }
            }

            else if (RptName == "RptAccountStatement.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString();
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    SUBGLFRM = Request.QueryString["ACCNO"].ToString();
                }
                if (Request.QueryString["PT"] != null)
                {
                    SUBGLTO = Request.QueryString["PT"].ToString();
                }
                if (Request.QueryString["GL"] != null)
                {
                    CTRL = Request.QueryString["GL"].ToString();
                }
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RptProfitAndLoss.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
            }


            else if (RptName == "RptAdminExp.rdlc")  //added by ashok misal
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BranchID = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
            }
            //////////kyc documnet
            else if (RptName == "RptKycDoc.rdlc")
            {
                if (Request.QueryString["KycTP"] != null)
                {
                    FromDOCType = Request.QueryString["KycTP"].ToString();
                }
                if (Request.QueryString["EXPF"] != null)
                {
                    ExportT = Request.QueryString["EXPF"].ToString();
                }
            }
            else if (RptName == "RptFDPrinting.rdlc" || RptName == "RptFDShiv.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["SubGlCode"] != null)
                {
                    SGL = Request.QueryString["SubGlCode"].ToString();
                }
                if (Request.QueryString["Accno"] != null)
                {
                    accno = Request.QueryString["Accno"].ToString();
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString();
                }
            }
            else if (RptName == "RptFDPrintMarathawada.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["SubGlCode"] != null)
                {
                    SGL = Request.QueryString["SubGlCode"].ToString();
                }
                if (Request.QueryString["Accno"] != null)
                {
                    accno = Request.QueryString["Accno"].ToString();
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString();
                }
            }



                //28/12/2018 rakesh


            else if (RptName == "RptFDBackPrint_Palghar.rdlc" || RptName == "RptFDBackPrint_MSEB.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["SubGlCode"] != null)
                {
                    SGL = Request.QueryString["SubGlCode"].ToString();
                }
                if (Request.QueryString["Accno"] != null)
                {
                    accno = Request.QueryString["Accno"].ToString();
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString();
                }
            }


            else if (RptName == "RptFDPrint_MSEB.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["SubGlCode"] != null)
                {
                    SGL = Request.QueryString["SubGlCode"].ToString();
                }
                if (Request.QueryString["Accno"] != null)
                {
                    accno = Request.QueryString["Accno"].ToString();
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString();
                }
            }

            else if (RptName == "RptFDPrintingShivSamarth.rdlc" || RptName == "RptFDPrintAkyt.rdlc" || RptName == "RptFDPrintAkyt_G.rdlc" || RptName == "RptFDPrintAkyt_N.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["SubGlCode"] != null)
                {
                    SGL = Request.QueryString["SubGlCode"].ToString();
                }
                if (Request.QueryString["Accno"] != null)
                {
                    accno = Request.QueryString["Accno"].ToString();
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString();
                }
            }

            else if (RptName == "RptPLExpenses.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    BranchID = Request.QueryString["BranchID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FromDate"] != null)
                {
                    FDT = Request.QueryString["FromDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ToDate"] != null)
                {
                    TDT = Request.QueryString["ToDate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptPLExpenses_WithBal.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    BranchID = Request.QueryString["BranchID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FromDate"] != null)
                {
                    FDT = Request.QueryString["FromDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ToDate"] != null)
                {
                    TDT = Request.QueryString["ToDate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptPLExpenses_WithMarathi.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    BranchID = Request.QueryString["BranchID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FromDate"] != null)
                {
                    FDT = Request.QueryString["FromDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ToDate"] != null)
                {
                    TDT = Request.QueryString["ToDate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptPLExpenses_SkipData.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    BranchID = Request.QueryString["BranchID"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FromDate"] != null)
                {
                    FDT = Request.QueryString["FromDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ToDate"] != null)
                {
                    TDT = Request.QueryString["ToDate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptShrBalRegister.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FromDate"] != null)
                {
                    FDT = Request.QueryString["FromDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ToDate"] != null)
                {
                    TDT = Request.QueryString["ToDate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptShrBalRegisterSumry.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FromDate"] != null)
                {
                    FDT = Request.QueryString["FromDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ToDate"] != null)
                {
                    TDT = Request.QueryString["ToDate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptShrBalanceCertWise.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FromDate"] != null)
                {
                    FDT = Request.QueryString["FromDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ToDate"] != null)
                {
                    TDT = Request.QueryString["ToDate"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptBrWiseDepositLoanList.rdlc")
            {
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Type"] != null)
                {
                    FL = Request.QueryString["Type"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptAccOpCl.rdlc")////Added by ankita on 13/06/2017 To display account opening and closing details//modified by ashok misal.
            {
                if (Request.QueryString["flag"] != null)
                {
                    FL = Request.QueryString["flag"].ToString().Replace("%27", "");
                    if (FL == "OPEN")
                        FLT = "Opening";
                    if (FL == "CLOSE")
                        FLT = "Closing";
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    fdate = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    tdate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SUBGL"] != null)
                {
                    Subgl = Request.QueryString["SUBGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["UserName"] != null)
                {
                    UName = Request.QueryString["UserName"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["prodname"] != null)
                {
                    subglname = Request.QueryString["prodname"].ToString().Replace("%27", "");
                }
            }

            else if (RptName == "RptLoanNill.rdlc")////Added By ashok misal for Loan Nill Report
            {

                if (Request.QueryString["FDATE"] != null)
                {
                    fdate = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    tdate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Sub1"] != null)
                {
                    Subgl = Request.QueryString["Sub1"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["Sub2"] != null)
                {
                    Productcode = Request.QueryString["Sub2"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptPLALLBrReport_CRDR.rdlc")////Added By ashok misal for RptPLALLBrReport_CRDR
            {
                if (Request.QueryString["FDATE"] != null)
                {
                    fdate = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    tdate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptPLALLBrReport_PL.rdlc")////Added By ashok misal for RptPLALLBrReport_CRDR
            {
                if (Request.QueryString["FDATE"] != null)
                {
                    fdate = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    tdate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
            }

            //   if (RptName == "RptIJRegister.rdlc" || RptName == "RptJRegister.rdlc")////Added by ankita on 06/07/2017 to display IJRegister
            if (RptName == "RptIRegister.rdlc" || RptName == "RptIJReg.rdlc" || RptName == "RptJRegister.rdlc")////Added by ankita on 06/07/2017 to display IJRegister
            {
                if (Request.QueryString["brcd"] != null)
                {
                    BRCD = Request.QueryString["brcd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["accno"] != null)
                {
                    AccNo = Request.QueryString["accno"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["taccno"] != null)
                {
                    TACCNO = Request.QueryString["taccno"].ToString().Replace("%27", "");
                }
            }

            //Loan Sanction Report Details

            if (RptName == "RptPhotoSign.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ToDate"] != null)
                {
                    TDate = Request.QueryString["ToDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Flag"] != null)
                {
                    CustNo1 = Request.QueryString["Flag"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Prdcd"] != null)
                {
                    CustNo2 = Request.QueryString["Prdcd"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptPhotoNotscan.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ToDate"] != null)
                {
                    TDate = Request.QueryString["ToDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Flag"] != null)
                {
                    CustNo1 = Request.QueryString["Flag"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Prdcd"] != null)
                {
                    CustNo2 = Request.QueryString["Prdcd"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptDividantPayble.rdlc")
            {
                if (Request.QueryString["BrCode"] != null)
                {
                    FBRCD = Request.QueryString["BrCode"].ToString().Replace("%27", "");
                }

                if (Request.QueryString["Fdate"] != null)
                {
                    FDate = Request.QueryString["Fdate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ToDate"] != null)
                {
                    TDate = Request.QueryString["ToDate"].ToString().Replace("%27", "");
                }

            }
            //Loan Amount wise report
            if (RptName == "RptAVS5025.rdlc")//Ankita on 07/08/2017 to display loan amount wise report
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRDCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRDCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRDCD"] != null)
                {
                    TPRCD = Request.QueryString["TPRDCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDate = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDate = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TYPE"] != null)
                {
                    Type = Request.QueryString["TYPE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AMT"] != null)
                {
                    AMT = Request.QueryString["AMT"].ToString().Replace("%27", "");
                }
            }

            if (RptName == "Isp_AVS0038.rdlc") // For FD Interest Certificate
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    BRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["CutNo"] != null)
                {
                    CNO = Request.QueryString["CutNo"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptTDSDetails.rdlc")
            {
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    BRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FCutNo"] != null)
                {
                    CNO = Request.QueryString["FCutNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TCutNo"] != null)
                {
                    FL = Request.QueryString["TCutNo"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptCustMobile.rdlc")
            {
                if (Request.QueryString["FBrcd"] != null)
                {
                    FBRCD = Request.QueryString["FBrcd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBrcd"] != null)
                {
                    TBRCD = Request.QueryString["TBrcd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FromDate"] != null)
                {
                    FDate = Request.QueryString["FromDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ToDate"] != null)
                {
                    TDate = Request.QueryString["ToDate"].ToString().Replace("%27", "");
                }
            }


            if (RptName == "RptAVS5085.rdlc")   ////Added by ankita on 16/02/2018 gold loan valuation list
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDT"] != null)
                {
                    FDATE = Request.QueryString["FDT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDT"] != null)
                {
                    TDATE = Request.QueryString["TDT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EDT"] != null)
                {
                    EDAT = Request.QueryString["EDT"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PRDNAME"] != null)
                {
                    FPRDTYPE = Request.QueryString["PRDNAME"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["CURRATE"] != null)
                {
                    CURRATE = Request.QueryString["CURRATE"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptAuditrail1.rdlc")   ////Added by ankita on 18/11/2017 CUSTOMER OPEN REPORT
            {
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDATE = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDATE = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptAuditrail2.rdlc")   ////Added by ankita on 18/11/2017 
            {
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDATE = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDATE = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptAuditrail3.rdlc")   ////Added by ankita on 18/11/2017 
            {
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDATE = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDATE = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptAuditrail4.rdlc")   ////Added by ankita on 18/11/2017 
            {
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDATE = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDATE = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptAuditrail5.rdlc")   ////Added by ankita on 18/11/2017 
            {
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDATE = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDATE = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptAuditrail6.rdlc")   ////Added by ankita on 18/11/2017 
            {
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDATE = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDATE = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptDivdentMemWiseList.rdlc" || RptName == "RptDivdentSHRDPList.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TPRCD"] != null)
                {
                    TPRCD = Request.QueryString["TPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDT = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDate"] != null)
                {
                    TDT = Request.QueryString["TDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Div"] != null)
                {
                    S1 = Request.QueryString["Div"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Dep"] != null)
                {
                    S2 = Request.QueryString["Dep"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptAuditrail7.rdlc")   //Added by ankita on 18/11/2017 
            {
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBRCD = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBRCD = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["EDATE"] != null)
                {
                    Edate = Request.QueryString["EDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["GLCODE"] != null)
                {
                    GL = Request.QueryString["GLCODE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDATE"] != null)
                {
                    FDATE = Request.QueryString["FDATE"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TDATE"] != null)
                {
                    TDATE = Request.QueryString["TDATE"].ToString().Replace("%27", "");
                }
            }

            if (RptName == "RptDepositReg_EMP.rdlc") //------- 
            {
                if (Request.QueryString["PType"] != null)
                {
                    PT = Request.QueryString["PType"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Tdate"] != null)
                {
                    FDate = Request.QueryString["Tdate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FL"] != null)
                {
                    FL = Request.QueryString["FL"].ToString().Replace("%27", "");
                }
            }
            //else if (RptName == "RptAdmExpenses_DT.rdlc" || RptName == "RptAdmExpenses_Sumry.rdlc")
            //{
            //    if (Request.QueryString["BranchID"] != null)
            //    {
            //        FBC = Request.QueryString["BranchID"].ToString().Replace("%27", "");
            //    }
            //    if (Request.QueryString["FDate"] != null)
            //    {
            //        FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
            //    }
            //    if (Request.QueryString["TDate"] != null)
            //    {
            //        TDate = Request.QueryString["TDate"].ToString().Replace("%27", "");
            //    }
            //}

            else if (RptName == "RptAdmExpenses_DT.rdlc" || RptName == "RptAdmExpenses_Sumry.rdlc" || RptName == "RptAdmExpenses_Br.rdlc")
            {
                if (Request.QueryString["BranchID"] != null)
                {
                    FBC = Request.QueryString["BranchID"].ToString().Replace("%27", "");
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

            if (RptName == "RptRecPayCLBal_ALL.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBrcd = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBRCD"] != null)
                {
                    TBrcd = Request.QueryString["TBRCD"].ToString().Replace("%27", "");
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

            if (RptName == "RptAddressLabelPrint.rdlc") // Address Label Print
            {
                if (Request.QueryString["FAccno"] != null)
                {
                    AccNo = Request.QueryString["FAccno"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TAccno"] != null)
                {
                    TACCNO = Request.QueryString["TAccno"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptPreSanLoanAPPList.rdlc" || RptName == "RptSanLoanAPPList.rdlc")
            {
                if (Request.QueryString["FrBrCode"] != null)
                {
                    BRCD = Request.QueryString["FrBrCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FrSancDate"] != null)
                {
                    FDate = Request.QueryString["FrSancDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ToSancDate"] != null)
                {
                    TDate = Request.QueryString["ToSancDate"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptDividentTRFProcess.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    FDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Product"] != null)
                {
                    PRDCD = Request.QueryString["Product"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["CrProduct"] != null)
                {
                    TPRCD = Request.QueryString["CrProduct"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["MID"] != null)
                {
                    MID = Request.QueryString["MID"].ToString().Replace("%27", "");
                }
            }
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
            if (RptName == "RptCustomerBalane.rdlc")
            {
                if (Request.QueryString["BrCode"] != null)
                {
                    BRCD = Request.QueryString["BrCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["isExcelDownload"] != null)
                {
                    isExcelDownload = Convert.ToInt16(Request.QueryString["isExcelDownload"].ToString().Replace("%27", ""));
                    FileName = "Customer Balane List";
                }
            }
            if (RptName == "RptLoanBalanceList.rdlc")
            {
                if (Request.QueryString["BrCode"] != null)
                {
                    BRCD = Request.QueryString["BrCode"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AsOnDate"] != null)
                {
                    AsOnDate = Request.QueryString["AsOnDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["isExcelDownload"] != null)
                {
                    isExcelDownload = Convert.ToInt16(Request.QueryString["isExcelDownload"].ToString().Replace("%27", ""));
                    FileName = "Loan Balane List";
                }
            }
            if (RptName == "RptMobileData.rdlc")
            {
                if (Request.QueryString["FBrcd"] != null)
                {
                    FBrcd = Request.QueryString["FBrcd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TBrcd"] != null)
                {
                    TBrcd = Request.QueryString["TBrcd"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FCustNo"] != null)
                {
                    FCustNo = Request.QueryString["FCustNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TCustNo"] != null)
                {
                    TCustNo = Request.QueryString["TCustNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FromDate"] != null)
                {
                    AsOnDate = Request.QueryString["FromDate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Flag"] != null)
                {
                    FL = Request.QueryString["Flag"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Live"] != null)
                {
                    FLT = Request.QueryString["Live"].ToString().Replace("%27", "");
                }
            }
            if (RptName == "RptAVS51182.rdlc")
            {
                if (Request.QueryString["FBRCD"] != null)
                {
                    FBrcd = Request.QueryString["FBRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FPRCD"] != null)
                {
                    FPRCD = Request.QueryString["FPRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FAccNo"] != null)
                {
                    FACCNO = Request.QueryString["FAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["TAccNo"] != null)
                {
                    TACCNO = Request.QueryString["TAccNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Date"] != null)
                {
                    AsOnDate = Request.QueryString["Date"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ShrAmt"] != null)
                {
                    Amt = Request.QueryString["ShrAmt"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["DepAmt"] != null)
                {
                    SL = Request.QueryString["DepAmt"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["DepPeriod"] != null)
                {
                    S1 = Request.QueryString["DepPeriod"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["LoanAmt"] != null)
                {
                    S2 = Request.QueryString["LoanAmt"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["LnPeriod"] != null)
                {
                    S3 = Request.QueryString["LnPeriod"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Atte"] != null)
                {
                    FL = Request.QueryString["Atte"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AtteMin"] != null)
                {
                    FLT = Request.QueryString["AtteMin"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Flag"] != null)
                {
                    Flag = Request.QueryString["Flag"].ToString().Replace("%27", "");
                }
            }

            if (RptName == "Rpt_TDWithdrwalVchr.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["PRD"] != null)
                {
                    Prd = Request.QueryString["PRD"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Accno"] != null)
                {
                    Acc = Request.QueryString["Accno"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["SetNo"] != null)
                {
                    SETNO = Request.QueryString["SetNo"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["FDate"] != null)
                {
                    AsOnDate = Request.QueryString["FDate"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptDemandNotice_Sro.rdlc" || RptName == "RptBeforeAttchment_Sro.rdlc" || RptName == "RptAttchment_Sro.rdlc" || RptName == "RptVisit_Sro.rdlc" || RptName == "RptSymbolicNotice_Sro.rdlc" || RptName == "RptPropertyNotice_Sro.rdlc" || RptName == "RptAccAttchNotice_Sro.rdlc" || RptName == "RptTermCondition_Notice.rdlc" || RptName == "RptTenderForm_Notice.rdlc" || RptName == "RptPublicLetterNotice_Sro.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate1"] != null)
                {
                    TDATE = Request.QueryString["Edate1"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AddFlag"] != null)
                {
                    AddFlag = Request.QueryString["AddFlag"].ToString().Replace("%27", "");
                }
            }
            else if (RptName == "RptIntimationNotice_Sro.rdlc" || RptName == "RptProtectionNotice_Sro.rdlc" || RptName == "RptPossessionNotice_Sro.rdlc" || RptName == "RptSushilLetter_Sro.rdlc")
            {
                if (Request.QueryString["BRCD"] != null)
                {
                    BRCD = Request.QueryString["BRCD"].ToString();
                }
                if (Request.QueryString["LOANGL"] != null)
                {
                    Productcode = Request.QueryString["LOANGL"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["ACCNO"] != null)
                {
                    Accno = Request.QueryString["ACCNO"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["Edate"] != null)
                {
                    Edate = Request.QueryString["Edate"].ToString().Replace("%27", "");
                }
                if (Request.QueryString["AddFlag"] != null)
                {
                    AddFlag = Request.QueryString["AddFlag"].ToString().Replace("%27", "");
                }
            }

            DataSet thisDataSet = new DataSet();
            DataSet thisDataSet1 = new DataSet();

            //loan schedule Rpt_LoanSchedule_Parti
            if (RptName == "Rpt_LoanSchedule_Parti.rdlc")
            {
                // string BRCD = Session["BRCD"].ToString();
                thisDataSet = GetLS(FL, Subgl, AC, BRCD);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            if (RptName == "GSTMasterRpt.rdlc")
            {
                thisDataSet = GSTMaster(BRCD);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptMenu.rdlc")
            {
                thisDataSet.Tables.Add(adm.rptMenu());
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptCustWiseBalance.rdlc")
            {
                thisDataSet = GetStaffBal(BRCD, ASONDT);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptFedStatement.rdlc")
            {
                thisDataSet = GetCustomerStatement(BRCD, CustNo);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptOrdReceipt.rdlc")
            {
                thisDataSet = GetReceiptData(id, MEMTYPE, MEMNO);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptDeedStock.rdlc")
            {
                thisDataSet = GetDeedStock(FLAG, BRCD, VENDORID, PRODID, ENTRYDATE);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptDeedStock_Stock.rdlc")
            {
                thisDataSet = GetDeedStockRPT(FLAG, BRCD, VENDORID, PRODID, ENTRYDATE);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptExecutionChargesLetter_ToSocity.rdlc")
            {
                thisDataSet = SRONO.SP_RptExecutionChargesLetter_ToSocity(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptClosingStock.rdlc")
            {
                thisDataSet = GetClosingStock(FLAG, BRCD, VENDORID, PRODID, ENTRYDATE);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptProductMaster.rdlc")
            {
                thisDataSet = GetProductMaster(FLAG, VENDORID, PRODID);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptVendorMaster.rdlc")
            {
                thisDataSet = GetVendorMaster(FLAG, VENDORID);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptLienMarkList.rdlc")
            {
                thisDataSet = LS.LienMarkListDT(FBRCD, TBRCD, FPRCD, TPRCD, AsOnDate);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //RptDividendCalc
            if (RptName == "RptDividendCalc.rdlc")
            {
                thisDataSet = GetDividendCalc(BRCD, Session["EntryDate"].ToString(), FDATE, TDATE, FPRCD, TPRCD, FACCNO, TACCNO, FL, RATE, Session["MID"].ToString());

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            //RptIWOWCharges
            if (RptName == "RptIWOWCharges.rdlc")
            {
                thisDataSet = GetIWOWChargesRepo(FL, BRCD, EDT);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptLoanRegister.rdlc")
            {
                thisDataSet = GetDetails(BRCD, Prd, FDate, TDate);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //Unpass
            if (RptName == "RptUnpassDetails.rdlc")
            {
                thisDataSet = GetUnpassRep(FBRCD, TBRCD, ASONDT);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //Inw Return Details
            if (RptName == "RptIWReturnRegDetails.rdlc")
            {
                thisDataSet = GetIWOWRDetails(FL, SFL1, FBRCD, TBRCD, FDT, TDT);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //Inw Return Summary
            if (RptName == "RptIWReturnRegSummary.rdlc")
            {
                thisDataSet = GetIWOWRDetails(FL, SFL1, FBRCD, TBRCD, FDT, TDT);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //OW Return Details
            if (RptName == "RptOWReturnRegDetails.rdlc")
            {
                thisDataSet = GetIWOWRDetails(FL, SFL1, FBRCD, TBRCD, FDT, TDT);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //OW Return Details
            if (RptName == "RptOWReturnRegSummary.rdlc")
            {
                thisDataSet = GetIWOWRDetails(FL, SFL1, FBRCD, TBRCD, FDT, TDT);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }






            if (RptName == "RptTransfer.rdlc")
            {
                // string BRCD = Session["BRCD"].ToString();
                thisDataSet = GetRecords(FDT, TDT, SGL, BRCD, AMOUNT, AC, TrxType, Activity, FL);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptAddressLabelPrint_TZMP.rdlc")
            {
                thisDataSet = AddressLabelPrint_TZMP(BRCD, AccNo, TACCNO, FDate, S1, S2);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //RptDDSCMonthlySummary
            if (RptName == "RptDDSCMonthlySummary.rdlc")
            {
                thisDataSet = GetMonSummary(BRCD, GL, Subgl, AC);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptInvMat.rdlc")
            {
                thisDataSet = GetInvDetails(FL, Session["BRCD"].ToString(), FDate, TDATE, EDT, GL);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptInv_Reg.rdlc")
            {
                thisDataSet = GetAccREgRpt(FL, Session["BRCD"].ToString(), EDT, GL, "0");

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptInvProd.rdlc")
            {
                thisDataSet = GetAccREgRpt(FL, Session["BRCD"].ToString(), EDT, GL, FLT);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptInvInterest.rdlc")
            {
                thisDataSet = GetAccInvInt(Session["BRCD"].ToString(), SUBGLFRM, SUBGLTO, EDT);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptInvMaturity.rdlc")
            {
                thisDataSet = GetMaturity(FL, Session["BRCD"].ToString(), FDATE, TDATE);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptDayBook_SHIV.rdlc")
            {
                thisDataSet = GetDayBook_Shiv(BranchID, FL, FLT, FDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //FD INT CALCULATION REPORT
            if (RptName == "RptFDINTCalculation.rdlc")
            {
                string MID = Session["MID"].ToString();
                string BRCD = Session["BRCD"].ToString();
                thisDataSet = GetFDINTCalReport(FBRCD, TBRCD, Fsubgl, Tsubgl, FACCNO, TACCNO, ASONDT, MID, SKIP_DIGIT);
                // thisDataSet = AccountStatement(FDT, TDT, SUBGLFRM, SUBGLTO, MID, BRCD, CTRL, BRCD);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            ///////////////BILL PRINTING SPEC ALL startB
            else if (RptName == "RptAccountStatement.rdlc")
            {
                string MID = Session["MID"].ToString();
                string BRCD = Session["BRCD"].ToString();
                thisDataSet = AccountStatement(FDT, TDT, SUBGLFRM, SUBGLTO, MID, BRCD, CTRL, BRCD);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            ///////////////Cheque Issue Register Report
            else if (RptName == "RptChequeIssueRegister.rdlc")
            {
                string BRCD = Session["BRCD"].ToString();
                thisDataSet = ChequeIssueReg(BRCD, TDT, PT, AC, GL);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RPTDDSIntMst.rdlc")
            {
                thisDataSet = GetDDSIntData(FL, FPRDTYPE, TPRDTYPE, CT, PR);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }

            }
            else if (RptName == "RptAVS5115.rdlc")//Dhanya Shetty///04/04/2018
            {
                thisDataSet = GetRecIntRec(FBrcd, TBrcd, Prd, Month, Year);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //Day Activity View
            else if (RptName == "RptDayActivity.rdlc")
            {
                thisDataSet = GetDayActivityData(FL, BRCD, FDATE, TDATE, RBD);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }

            }
            //Day Close
            else if (RptName == "RptDayClose.rdlc")
            {
                thisDataSet = GetDayclose(FL, BRCD, FDATE, TDATE);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }

            }

            else if (RptName == "RptIntimation_Cheque.rdlc")
            {
                thisDataSet = SRONO.SP_RptIntimation_Cheque(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptIntimation_ToSocity.rdlc")
            {
                thisDataSet = SRONO.SP_RptIntimation_ToSocity(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptMemberPassBookFamer.rdlc")
            {
                thisDataSet = GetmemberPassBkFarmer(FDate, TDate, PT, FBC);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            ////REPORTS OF ACCOUNT NO. COUNT
            else if (RptName == "RptAccCount.rdlc")
            {
                thisDataSet = GetAccnoCount(BRCD, GL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There is No Record...!!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

        ///////////////Cheque Stock Report
            else if (RptName == "RptChequeStock.rdlc")
            {
                string BRCD = Session["BRCD"].ToString();
                thisDataSet = ChequeStock(BRCD, TDate, AC, GL);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            ///////////////Saving Passbook Print
            else if (RptName == "RptSavingPassBook.rdlc")
            {
                string BRCD = Session["BRCD"].ToString();
                thisDataSet = GetSavPassbook(BRCD, FDate, TDate, PT, AC, SR);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptPassbokkCover.rdlc")
            {
                string BRCD = Session["BRCD"].ToString();
                thisDataSet = GetCoverPassbook(BRCD, FDate, TDate, PT, AC, SR);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            ///////////////Account Open Close
            else if (RptName == "RptAccountOpenCloseReport.rdlc")
            {
                thisDataSet = AccountOpenClose(BRCD, PT, FDate, FL);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            //Un-operative Accounts Report
            else if (RptName == "RptUnOpAccts.rdlc")
            {
                thisDataSet = UnOpAccts(FBC, TBC, PT, FDate, FL);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //Top Deposit list
            else if (RptName == "RptTopDepositList.rdlc")
            {
                thisDataSet = GetTopDepositList(FBC, GL, FDate, FD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //USER REPORT
            else if (RptName == "RptUserReport.rdlc")
            {
                thisDataSet = GetUR(FBC, TBC, FL, Flag);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //Top Loan list
            else if (RptName == "RptTopLoanList.rdlc")
            {
                thisDataSet = GetTopLoanList(FBC, GL, FDate, FD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            // Account Bal Register Rakesh
            else if (RptName == "RptAcBalRegisterReport.rdlc")
            {
                thisDataSet = GetAcBalReg(FBC, GL, FD, AsOnDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptAVSBMTableReport.rdlc")
            {
                thisDataSet = AVSBMTransaction(FBC, FD, AsOnDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //Account Statement 08-12-2016 Rakesh
            else if (RptName == "RptAccountStatementReport.rdlc")
            {
                thisDataSet = GetAccStm(FDate, TDate, PT, AC, FBC);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptMemberPassBook.rdlc")
            {
                thisDataSet = GetmemberPassBk(FDate, TDate, PT, FBC);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptMemberPassBook1.rdlc")
            {
                thisDataSet = GetmemberPassBk1(FDate, TDate, PT, FBC);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptMemberPassBook_DT.rdlc")
            {
                thisDataSet = GetmemberPassBk_DT(FDate, TDate, PT, FBC);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }



            else if (RptName == "RptGLWiseTransDetails.rdlc")
            {
                thisDataSet = GetGLTransD(FBC, PT, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptBrWiseGLDeatails.rdlc")
            {
                thisDataSet = GetBrWiseGl(FBC, PT, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptBrWiseGLSummry.rdlc")
            {
                thisDataSet = GetBrWiseGlSumry(FBC, PT, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptGLWiseTransMonthWise.rdlc")
            {
                thisDataSet = GetGLTransMonth(FBC, PT, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "Rpt31Remainder_Notice.rdlc")
            {
                thisDataSet = SRONO.SP_Rpt31Remainder_Notice(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptFinalIntimationLetter.rdlc")
            {
                thisDataSet = SRONO.SP_RptFinalIntimationLetter(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptDormantAcList.rdlc")
            {
                thisDataSet = GetDoramntAcList(FBC, FDate, PT, FL, Amt);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //RptGLWiseTransMonthWise
            else if (RptName == "RptTransSubGlMonthWise.rdlc")
            {
                thisDataSet = GetTransSubMonth(FBC, PT, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptTransSummaryMonthWise.rdlc")
            {
                thisDataSet = GetTransSumarryMonhWise(FBC, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptCutBook_DepWise.rdlc" || RptName == "RptCutBook_Palghar.rdlc")
            {
                thisDataSet = GetCutBook_Palghar(FDT, GL, SGL, MID, BRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptOfficeGLWiseTransDetails.rdlc")
            {
                thisDataSet = OGLT.GetOffGLTransDReg(FBC, PT, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptGenralLedgerWise_BrAdj.rdlc")
            {
                thisDataSet = OGLT.GetOffGLTransDReg_BrAdj(FBC, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptOfficeGLWiseTransSumary.rdlc")
            {
                thisDataSet = GetOffGLTransSumry(FBC, PT, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //For Vault cash Denomination
            else if (RptName == "RPTCashDenom.rdlc")
            {
                thisDataSet = GetCashPos(FBRCD, TBRCD, AsOnDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptProfitAndLoss.rdlc")
            {
                thisDataSet = GetProfitAndLoss(BranchID, FDT);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptAdminExp.rdlc") //added ashok misal
            {
                thisDataSet = GetAdminExp(BranchID, FDT, TDT);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptLoanSchedule.rdlc")
            {
                thisDataSet = GetData(PT, AC, BRCD);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            else if (RptName == "RptLienMark.rdlc")
            {
                thisDataSet = getlienmark(BRCD, FDate, TDate, Productcode);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;

                }


            }
            else if (RptName == "RptLienMarkLientype.rdlc")
            {
                thisDataSet = getlienmarkLientype(BRCD1, BRCD2, FDate, TDate, Productcode, LIENTYPE);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }

            }

            else if (RptName == "RptDepositReg.rdlc" || RptName == "RptLoanReg.rdlc")  //-------
            {
                thisDataSet = GetLoanDeposR(FL, PT, FDate);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;

                }
            }
            else if (RptName == "RptDepositReg_Category.rdlc")  //-------
            {
                thisDataSet = GetLoanDeposR(FL, PT, FDate);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;

                }
            }

            else if (RptName == "RptDormantDueAcList.rdlc")
            {
                thisDataSet = GetDoramntDueAcList(FBC, FDate, PT, FL, Amt);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //SMS REPORT ANKITA 13/05/2017
            else if (RptName == "RptSmsMstReport.rdlc")  //-------
            {
                thisDataSet = Getsmsrep(FDATE, TDATE, FBRCD, TBRCD, MOBILE);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;

                }
            }
            else if (RptName == "RptCustDetails.rdlc") //// Added by ankita on 19/06/2017 to display customer report
            {
                thisDataSet = GetCustRep(FDATE, TDATE, FBRCD, TBRCD);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;

                }
            }
            //CUSTOMER UNIFICATION REPORT ANKITA 13/05/2017

            else if (RptName == "RptLoanReg.rdlc") //-------
            {
                thisDataSet = GetLoanDeposR(FDT, PT, FDate);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;

                }
            }
            else if (RptName == "RptDDSR.rdlc") //-------
            {
                thisDataSet = GetDDSR(BRCD, Type, FDATE);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;

                }
            }


            else if (RptName == "RptGLreport.rdlc") //-------
            {
                thisDataSet = getglrep(FL, BRCD);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;

                }
            }

            else if (RptName == "RptCutBook.rdlc")
            {
                thisDataSet = GetCutBook(FDT, GL, SGL, MID, BRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptCutBookSa.rdlc")
            {
                thisDataSet = GetCutBooksa(FDT, GL, SGL, MID, BRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptCuteBookDetails.rdlc")
            {
                thisDataSet = GetCutBookDetail(BRCD, SGL, FDT, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }



            //rakesh15012019
            else if (RptName == "RptStaffMemPassbook.rdlc")
            {
                thisDataSet = GetStaffmemberPassBk(FDate, TDate, PT, FBC);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            //rakesh15012019
            if (RptName == "RptSBIntCalcReport_DT.rdlc" || RptName == "RptSBIntCalcReport_Sumry.rdlc")
            {
                thisDataSet = SBS.GetSBIntCal_DTRpt(BRCD, Prd, FDate, TDate, MID, FL);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptAVS5143.rdlc" || RptName == "RptAVS5143_BACK.rdlc" || RptName == "RptAVS5143_Marathi.rdlc")//Dhanya Shetty//30/08/2018//For ChequePrint
            {
                thisDataSet = GetChequeMseb(MemberNo, Divident, DepositInt, TotalPay, CheqNo, BankCode, FL, Flag);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }





            else if (RptName == "RptAVS5077.rdlc")  //ashok misal
            {
                thisDataSet = RptAVS5077m(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptDivPendingList.rdlc")
            {
                thisDataSet = GetDivMemPendingList(FBRCD, TBRCD, FDT, TDT, "RPT", FL, S1, S2);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptSavingCerti.rdlc")  //ashok misal
            {
                thisDataSet = RptSavingCerti(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptLoanRepayCerti.rdlc")
            {
                thisDataSet = RptLoanintcert(BRCD, Productcode, Accno, Edate, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    LRPC.updatelog("", OUTNO);
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            else if (RptName == "RptDivdentMemWiseList.rdlc")
            {
                thisDataSet = GetDivMemList(FBRCD, TBRCD, FPRCD, TPRCD, FDT, TDT, S1, S2);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptDivdentSHRDPList.rdlc")
            {
                thisDataSet = GetDivSHRDPList(FBRCD, TBRCD, FPRCD, TPRCD, FDT, TDT, S1, S2);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }



            else if (RptName == "ShareRegister.rdlc")//Dhanya Shetty//30-12-2017//For ShareRegister
            {
                thisDataSet = ShareRegister(FBRCD, TBRCD, FDate, TDate, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RPTAVS5097.rdlc")//Dhanya Shetty//09-03-2018//For StockReport
            {
                thisDataSet = StockReport(BRCD, FDate, TDate, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }





            else if (RptName == "RptNPASelectType_1.rdlc")
            {
                thisDataSet = GetNPASelectTypeList_1(FBRCD, TBRCD, FPRCD, TPRCD, FACCNO, TACCNO, FDT, FL, S1, S2, Flag, FLT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptVisitnotice.rdlc")
            {
                thisDataSet = RptVisitnotice(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptActiveMem.rdlc")
            {
                thisDataSet = GetActiveMem(BRCD, FDT, TDate, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptVoucherDetailsList.rdlc")
            {
                thisDataSet = GetSubBook(BranchID, FDate, TDate, SGL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptFDPrint_Chikotra.rdlc")
            {
                thisDataSet = GetFD_Chikotra(BRCD, SGL, accno, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptVoucherSummaryList.rdlc")
            {
                thisDataSet = GetSubBookSum(BranchID, FDate, TDate, SGL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptInComeTaxReport.rdlc")
            {
                thisDataSet = GetITaxReport(BranchID, FDate, TDate, SGL, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptInComeTaxReport_SHR.rdlc")
            {
                thisDataSet = GetITaxReport_SHR(BranchID, FDate, TDate, SGL, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptInComeTaxReport_DP.rdlc")
            {
                thisDataSet = GetITaxReport_DP(BranchID, FDate, TDate, SGL, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptInComeTaxReport_LN.rdlc")
            {
                thisDataSet = GetITaxReport_LN(BranchID, FDate, TDate, SGL, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptSharesCert_SHIV.rdlc")
            {
                thisDataSet = ShareCertiPrintingAddshr(BRCD, AccNo, Fl, EntryDate, MID);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptLoanOverdueReport.rdlc")
            {
                thisDataSet = GetLoanOverdueMIS(BranchID, FL, AsOnDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptVoucherPrinting.rdlc" || RptName == "RptVoucherPrinting_Eng.rdlc")
            {
                thisDataSet = GetVoucherPrint(BranchID, FDate, SGL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "ISP_AVS0204.rdlc")
            {
                thisDataSet = ISP_AVS0204_DT(FBRCD, TBRCD, FPRCD, FDT, TDT, FL, FLT, PT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptVoucherPrintingCRDR.rdlc" || RptName == "RptVoucherPrintingCRDR_Eng.rdlc")
            {
                thisDataSet = GetVoucherPrintCRDR(BranchID, FDate, SGL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptVoucherPrintingFD.rdlc")
            {
                thisDataSet = GetVoucherPrintFD(BranchID, FDate, SGL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptVoucherPrintRetired.rdlc")
            {
                thisDataSet = GetVoucherPrintRetire(BranchID, FDate, SGL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptSharesApplicationList.rdlc")
            {
                thisDataSet = APP.GetShrApp(FBRCD, TBRCD, FPRCD, FDate, TDate, FL, SGL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //Outward Rep
            // GetOutwardReg(string FD, string TD, string FBC,string TBC, string UID)
            else if (RptName == "RptOutRegister.rdlc")
            {
                thisDataSet = GetOutwardReg(FDate, TDate, FBKcode, TBKcode, UName);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //Excess Cash Hold
            //Get_ExcessCash(string FD, string TD, string FBC, string TBC, string BRCD)
            else if (RptName == "RptExcessCashHold.rdlc")
            {
                thisDataSet = Get_ExcessCash(FDate, TDate, FBRCD, TBRCD, BRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }




            //ALL OK REP
            else if (RptName == "RptAllOK.rdlc")
            {
                thisDataSet = GetAllOK(AsOnDate, BRCD, UName);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //Cash Book
            else if (RptName == "RptCashBook.rdlc")
            {
                thisDataSet = GetCashBook(FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptCashBook_ALLDetails.rdlc")
            {
                thisDataSet = GetCashBook_ALL(FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptcashBookSummary.rdlc")
            {
                thisDataSet = GetCBSumry(FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptCheckAccLimit.rdlc")
            {
                thisDataSet = checkacclimit(FL, FDate, TDate, Productcode, P_CODE, BRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            //19012019rakesh

            else if (RptName == "RptDemandRecList.rdlc")
            {
                thisDataSet = GetDemandRecList_DT(BRCD, Month, Year, FL, FLT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptDemandRecList_DT.rdlc")
            {
                thisDataSet = GetDemandRec_DT(BRCD, Month, Year, FL, FLT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptAgent.rdlc")
            {
                thisDataSet = GETAGENT(FL, FDate, TDate, Productcode, BRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptShareCloseAccDetails.rdlc")
            {
                thisDataSet = ShareCloseAccDetails(FDate, TDate, BRCD, Edate, FBRCD, TBRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptUserReportAll.rdlc")
            {
                thisDataSet = GetUserrecord(BRCD, MID);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptPostCommision.rdlc")
            {
                thisDataSet = GetPostRecord(FDate, TDate, BRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptDailyAgentSlab.rdlc")
            {
                thisDataSet = DailyAgentSlab(FDate, Productcode, BRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptNOC_Certificate.rdlc")//Dhanya Shetty//13-09-2017//For Loan Certificate
            {
                thisDataSet = GetNOCPermit(BRCD, LOANGL, ACCNO, Edate, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptCDRatio.rdlc")
            {
                thisDataSet = CDRatio(FDate, BRCD, FPRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptShareMaster.rdlc" || RptName == "RptShareResign.rdlc")
            {
                thisDataSet = CCD.GetShareMemList(BRCD, FDate, TDate, Edate, Type);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptShareMismatchList.rdlc")
            {
                thisDataSet = CCD.GetShareMismatchList(BRCD, FDate, TDate, Edate, Type);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptLoanClose.rdlc" || RptName == "RptloanCityWise.rdlc")
            {
                thisDataSet = LoanInfo(FPRCD, BRCD, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptLoanAmountWise.rdlc")
            {
                thisDataSet = LoanAmountWise(FPRCD, TPRCD, BRCD, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptShareNomi.rdlc")
            {
                thisDataSet = ShareNomini(BRCD, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptDDSToLoan.rdlc")
            {
                thisDataSet = DDSToLoanReport(FPRCD, BRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptShareBalList.rdlc")
            {
                thisDataSet = ShareBalList(BRCD, FDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptShareCerti.rdlc" || RptName == "RptShareCerti_Marathwada.rdlc" || RptName == "RptShrYSPM.rdlc")
            {
                thisDataSet = ShareCertiPrinting(BRCD, AccNo, EntryDate, MID);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptShareAjinkyatara.rdlc")
            {
                thisDataSet = ShareCertiAjinkya(BRCD, AccNo);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptShareCerti_MarathwadaAddshr.rdlc" || RptName == "RptShrYSPMAddShr.rdlc")
            {
                thisDataSet = ShareCertiPrintingAddshr(BRCD, AccNo, Fl, EntryDate, MID);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptShareCerti_Palghar.rdlc")
            {
                thisDataSet = ShareCertiPrint_Palghar(BRCD, AccNo);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptProfitAndLoss_Marathi.rdlc" || RptName == "RptProfitAndLossSarjudas_Marathi.rdlc")
            {
                thisDataSet = GetProfitAndLoss_Marathi(BranchID, FDT);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptShareCerti_ShivSamarth.rdlc")//ankita 15/09/17 shivsamarth share certi
            {
                thisDataSet = ShareCertiPrinting_ShivSamarth(BRCD, AccNo);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "Rpt_AVS0003.rdlc")//Dipali Nagare 25-07-2017
            {
                thisDataSet = GETUserReport(BRCD, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            else if (RptName == "Rpt_AVS0001.rdlc")//Dipali Nagare 25-07-2017
            {
                thisDataSet = GETShareRegister(BRCD, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "Rpt_AVS0004.rdlc")//Dipali Nagare 28-07-2017
            {
                thisDataSet = GETShareRegisterFund(BRCD, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            else if (RptName == "RptMonthlyAccStat.rdlc")
            {
                thisDataSet = MonthlyAccStat(FDate, TDate, Productcode, AccNo, BRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptDebitEntry.rdlc")
            {
                thisDataSet = RptDebitEntry(FDate, Productcode, BRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptDailyCollection.rdlc")//Dhanya Shetty
            {
                thisDataSet = GetDailyCollection(Productcode, BRCD, Date, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptDailyCollection_A.rdlc")//Dhanya Shetty
            {
                thisDataSet = GetDailyCollection_A(Productcode, BRCD, Date);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }



            else if (RptName == "RptAVS5027.rdlc")//Dhanya Shetty
            {
                thisDataSet = GetMCustDetails(BRCD, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptLogDetails.rdlc")//Dhanya Shetty//19/09/2017
            {
                thisDataSet = GetLogDetails(BRCD, ACTVT, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            else if (RptName == "RptAVS5048.rdlc")//Dhanya Shetty for Overdue calculation//04/10/2017
            {
                thisDataSet = GetODcal(BRCD, EDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptduedateInvst.rdlc")//Dhanya Shetty
            {
                thisDataSet = GetDueDateColln(BRCD, FDate, TDate, EDAT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptAdharLink.rdlc")
            {
                thisDataSet = AdharlinkRpt(BRCD, FDate, EDAT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptAdharLinkDetails.rdlc")
            {
                thisDataSet = AdharlinkRptDetails(BRCD, FDate, EDAT, Flag);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            else if (RptName == "RptStartDateInvst.rdlc")//Dhanya Shetty
            {
                thisDataSet = GetStartDateColln(BRCD, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptBankReconDetails_Clear.rdlc")
            {
                thisDataSet = BankReconsile.getRecodetails_Clear(FBC, PT, FDate, TDate, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptBankReconDetails.rdlc")
            {
                thisDataSet = BankReconsile.getRecodetails(FBC, PT, FDate, TDate, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptDefaulters.rdlc")//Dhanya Shetty for Defaulters 
            {
                thisDataSet = GetDefaultesDetails(FBRCD, TBRCD, FPRCD, TPRCD, FACCNO, TACCNO, Date);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptUnverifyentry.rdlc")//Dhanya Shetty for Unverified List 
            {
                thisDataSet = GetUnverifiedDetails(BRCD, Date);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;

                }
            }

            else if (RptName == "RptNPARecoveryList.rdlc")
            {
                thisDataSet = GetNPAList(FBRCD, TBRCD, FPRCD, TPRCD, FACCNO, TACCNO, FDT, S1, S2, Flag);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptLoanODSummaryList.rdlc")
            {
                thisDataSet = GetODSumryFy(FBRCD, TBRCD, FPRCD, TPRCD, FACCNO, TACCNO, FDT, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "Isp_AVS0023.rdlc")
            {
                thisDataSet = GetNPASummaryList(FBRCD, TBRCD, FPRCD, TPRCD, FACCNO, TACCNO, FDT, FL, S1, S2, Flag);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //11012019rakesh

            else if (RptName == "RptShareCertiShr.rdlc")
            {
                thisDataSet = ShareCertiPrintingAddshr(BRCD, AccNo, Fl, EntryDate, MID);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

                //rakesh15012019
            else if (RptName == "RptCKYCList.rdlc")
            {
                thisDataSet = GetCKYCListDT(BRCD, FDate, FACCNO, TACCNO, RFlag);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptNPADetails_1.rdlc")
            {
                thisDataSet = GetNPADetailsList_1(FBRCD, TBRCD, FPRCD, TPRCD, FACCNO, TACCNO, FDT, FL, S1, S2, Flag);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptNPASumry_1.rdlc")
            {
                thisDataSet = GetNPASumryList_1(FBRCD, TBRCD, FPRCD, TPRCD, FACCNO, TACCNO, FDT, FL, S1, S2, Flag);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "Isp_AVS0024.rdlc" || RptName == "Isp_AVS0024_Rec.rdlc")
            {
                thisDataSet = GetSRORecoveryList(FBRCD, TBRCD, FPRCD, TPRCD, FDT, TDT, FL, FLT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }



            else if (RptName == "RptSRORecSumryList.rdlc" || RptName == "RptSRORecSumryList_Rec.rdlc")
            {
                thisDataSet = GetSRORecoverySumry(FBRCD, TBRCD, FPRCD, TPRCD, FDT, TDT, FL, FLT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptBranchWiseDP.rdlc")
            {
                thisDataSet = BDep.GetBrWiseDeposit(BranchID, AsOnDate, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //  BranchWise Deposit With Previous Month Report
            if (RptName == "RptBranchWiseDP_PRCR.rdlc")
            {
                thisDataSet = BDep.GetBrWiseDepositWithPMonth(BranchID, AsOnDate, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //  BranchWise Loan Report
            if (RptName == "RptBranchWiseLoanList.rdlc")
            {
                thisDataSet = BDep.GetBrWiseLoan(BranchID, AsOnDate, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //  BranchWise Loan With Previous Month Report
            if (RptName == "RptBranchWiseLoanList_PrCr.rdlc")
            {
                thisDataSet = BDep.GetBrWiseLoanWithPMonth(BranchID, AsOnDate, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptBranchWiseLoanDT_PrCr.rdlc")
            {
                thisDataSet = BDep.RptBrwiseLoanDT_PrCr(BranchID, AsOnDate, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            if (RptName == "RptDigitalBanking.rdlc")
            {
                thisDataSet = GetDigitalBank(FBRCD, TBRCD, ProdCode, Charges, PT, Date);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            else if (RptName == "Isp_AVS0015.rdlc")
            {
                thisDataSet = GetClassificationOfODList(FBRCD, TBRCD, FPRCD, TPRCD, FDT, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "LNODPeriodWiseSumry.rdlc")
            {
                thisDataSet = GetClssificationODSumryList(FBRCD, TBRCD, FPRCD, TPRCD, FDT, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptMemberPassBook_ALL.rdlc")
            {
                thisDataSet = GetmemberPassBk_ALL(FDate, TDate, PT, FL, FBC, S1, S2);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            else if (RptName == "RptIntRateSummaryDPList.rdlc")
            {
                thisDataSet = GetIntRateWiseDepositDt(FBRCD, TBRCD, FPRCD, FDT, "DP");
                thisDataSet1 = GetIntRateWiseDepositDt(FBRCD, TBRCD, FPRCD, FDT, "Lnv");
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptIntRateSummaryDPList_DT.rdlc")
            {
                thisDataSet = GetIntRateWiseDepositDt_DT(FBRCD, TBRCD, FPRCD, FDT, "DP", Flag, FLT);
                thisDataSet1 = GetIntRateWiseDepositDt_DT(FBRCD, TBRCD, FPRCD, FDT, "Lnv", Flag, FLT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //else if (RptName == "RptIntRateSummaryLoansList.rdlc")
            //{
            //    thisDataSet = GetIntRateWiseLoansDt (FBRCD, TBRCD, FPRCD, FDT);
            //    if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
            //    {
            //        WebMsgBox.Show("Sorry No Record found......!!", this.Page);
            //        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
            //        return;
            //    }
            //}  

            //27/12/2018
            if (RptName == "RptNonMemList.rdlc")
            {
                thisDataSet = RptNonMemList_DT(BRCD, FL, AsOnDate);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            else if (RptName == "RptPLALLBrReport.rdlc")
            {
                thisDataSet = GetPLALLBrReport(FBRCD, TBRCD, FDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptIntRateWiseDPSumry.rdlc")
            {
                thisDataSet = GetIntRateWiseSumry(FBRCD, TBRCD, FPRCD, FDT, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptInwRegAccWise.rdlc") //Dhanya 12-07-2017
            {
                thisDataSet = GetIWRegAcc(BranchID, FBKcode, FDT, TDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptOutwardRegAccWise.rdlc") //Dhanya 13-07-2017
            {
                thisDataSet = GetOutwardRegAcc(BranchID, FBKcode, FDT, TDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptIWReturnPatti.rdlc") //Dhanya 12-07-2017
            {
                thisDataSet = GetIWRetPatti(BranchID, FBKcode, FDT, TDT, ADT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptOutwardReturnPatti.rdlc") //Dhanya 12-07-2017
            {
                thisDataSet = GetOutwardRetPatti(BranchID, FBKcode, FDT, TDT, ADT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptInvBalList.rdlc")////////Ankita Ghadage 31/05/2017 for investment balance list
            {
                thisDataSet = GetBalanceList(BRCD, AsOnDate, EDAT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptCloseInvList.rdlc")////////Ankita Ghadage 01/06/2017 for Closed investment list
            {
                thisDataSet = GetClosedInvList(BRCD, FDate, TDate, EDAT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptCustBalWithSurity.rdlc" || RptName == "RptCustBalWithoutSurity.rdlc") //-------
            {
                thisDataSet = GetCustBalWithSurity(FDate, BRCD, FCustNo, TCustNo, Div, Dept);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "MultiAgentRpt.rdlc")
            {
                thisDataSet = GETMULTIAGENT(FL, FDate, TDate, Productcode, P_CODE, BRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptShivCheck.rdlc")
            {
                thisDataSet = GETMULTIAGENT1(FDate, TDate, Productcode, P_CODE, BRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

                //investment Report
            else if (RptName == "InvestmentRpt.rdlc")
            {
                thisDataSet = getinvestment(FDate, TDate, BRCD, Session["ENTRYDATE"].ToString());
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //Cash postition
            else if (RptName == "RptCashPostionReport.rdlc")
            {
                thisDataSet = GetCashPostion(BRCD, AsOnDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptChairmanReport.rdlc")
            {
                thisDataSet = GetChairman(BranchID, AsOnDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptCashLimit.rdlc")
            {
                thisDataSet = GetCashLimit(AsOnDate, BRCD, Subgl);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //else if (RptName == "RptNPADetails_1.rdlc")
            //{
            //    thisDataSet = GetNPADetailsList_1(FBRCD, TBRCD, FPRCD, TPRCD, FACCNO, TACCNO, FDT, FL, S1, S2, Flag);
            //    if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
            //    {
            //        WebMsgBox.Show("Sorry No Record found......!!", this.Page);
            //        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
            //        return;
            //    }
            //}
            else if (RptName == "RptClearngMemoList.rdlc")
            {
                thisDataSet = GrtClgMemoDetails(BRCD, AsOnDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptLoansSlabWiseDT.rdlc")
            {
                thisDataSet = GetLnSlabWiseDT(BRCD, AsOnDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptLoansSlabWise.rdlc")
            {
                thisDataSet = GetLnSlabWise(BRCD, AsOnDate, "D");
                thisDataSet1 = GetLnSlabWise_1(BRCD, AsOnDate, "S");
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "Isp_AVS0029.rdlc")
            {
                thisDataSet = GetABRALRDetails(BRCD, AsOnDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //LLOAN OVERDUE
            else if (RptName == "RptLoanOverdue.rdlc")
            {
                thisDataSet = GetLoanOver(EDT, BRCD, OnDate, Subgl);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptDashLoanOverdue.rdlc")//Dhanya Shetty for dashboard report
            {
                thisDataSet = GetLoanOverdue_New1(EDT, BRCD, OnDate, Subgl, SL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }



            else if (RptName == "RptOverDueSummary.rdlc")//Dhanya Shetty for Overdue summary
            {
                thisDataSet = GetOverdueDetail(EDT, BRCD, OnDate, SL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptMasterListing.rdlc")//Dhanya Shetty  //17-06-2017//MasterListing
            {
                thisDataSet = GetMasterListing(BRCD, ProdCode, AccNo, Status);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptLoanOverdue_New.rdlc")
            {
                thisDataSet = GetLoanOver_New(EDT, FBRCD, TBRCD, OnDate, Fsubgl, Tsubgl, SL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
                else
                {
                    RptName = "RptLoanOverdue.rdlc";
                }
            }
            else if (RptName == "RptDeLoanSummery.rdlc" || RptName == "RptDLienDetails.rdlc") //addded by ashok misal for loan and deposit lien Summary
            {
                thisDataSet = DeLoanSummery(SL, fdate, TDate, FBRCD, TBRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptLoanOverdue_Only.rdlc")
            {
                thisDataSet = GetLoanOver_Only(EDT, BRCD, OnDate, Fsubgl, Tsubgl, SL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //NPA Report
            else if (RptName == "RptODNpaReport.rdlc")
            {
                if (SL == "S")
                {
                    thisDataSet = GetLoanNPAReport(BRCD, Subgl, OnDate, SL);
                }
                else
                {
                    thisDataSet = GetLoanNPAReport(BRCD, "", OnDate, SL);
                }
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //NPAReg1 Report
            else if (RptName == "RptNPAReg1.rdlc")
            {

                thisDataSet = GetNPAReg1(BRCD, Fsubgl, Tsubgl, OnDate, "NPA", SFL1, SFL2);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //CDr Report
            else if (RptName == "RptCDR.rdlc")
            {
                thisDataSet = GetCDRReport(BRCD, OnDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "HeadAcListVoucherTRF.rdlc")
            {
                thisDataSet = GetHeadAcListVoucherTRF(FBRCD, FPRCD, TPRCD, TBRCD, FDT, FACCNO, TACCNO, FLT, "Trial");
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptCDRSummary.rdlc")
            {
                thisDataSet = GetCDRSUMReport(BRCD, OnDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //LLOAN OVERDUE
            //else if (RptName == "RptNPA1Report.rdlc")
            //{
            //    thisDataSet = GetNPA1(EDT, BRCD, OnDate, Subgl, SL);
            //    if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
            //    {
            //        WebMsgBox.Show("Sorry No Record found......!!", this.Page);
            //        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
            //        return;
            //    }
            //}

            //RECEIPT PRINT

            else if (RptName == "RptReceiptPrint.rdlc" || RptName == "RptReceiptPrint_2.rdlc" || RptName == "RptReceiptPrint_SHIV.rdlc" || RptName == "RptReceiptPrint_AKYT.rdlc")
            {
                thisDataSet = GetPrintreceipt(EDT, BRCD, SETNO, CNO, NAME, FN);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptReceiptPrintPal.rdlc" || RptName == "RptReceiptPrintsai.rdlc")//Dhanya Shetty//28/09/2017
            {
                thisDataSet = GetPrintreceipt(EDT, BRCD, SETNO, CNO, NAME, FN);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptReceiptPrintHSFM.rdlc")//Dhanya Shetty//28/09/2017 RptReceiptPrintHSFM_ShareApp
            {
                thisDataSet = GetPrintreceiptHSFM(EDT, BRCD, SETNO, CNO, NAME, FN, TBKcode);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RPTSTATEMENTACCREC.rdlc")//Dhanya Shetty//28/09/2017 RptReceiptPrintHSFM_ShareAppRPTSTATEMENTACCREC
            {
                thisDataSet = GetStatementACCHSFM(EDT, BRCD, SETNO, CASEY, NAME, FN);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptReceiptPrintHSFM_ShareApp.rdlc")//Dhanya Shetty//28/09/2017 
            {
                thisDataSet = GetPrintreceiptH(EDT, BRCD, SETNO, CNO, NAME, FN, TBKcode);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptSI_DDStoLoan.rdlc")
            {

                thisDataSet = GetSIDDStoLoan(FL, DdlStatus, BRCD, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //Details DDSTOLOAN 
            else if (RptName == "RptDetailsDDSTOLOAN.rdlc")
            {

                thisDataSet = GetDetailsDDSTOLOAN(FL, BRCD, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //Inward Rep
            // GetInwardReg(string FD, string TD, string FBC,string TBC, string UID)
            else if (RptName == "RptInwardReg.rdlc")
            {
                thisDataSet = GetInwardReg(FDate, TDate, FBKcode, TBKcode, UName);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptCDRatioReport.rdlc") //-------
            {
                thisDataSet = GetCDRatioDT(FDate, BRCD);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptCDRatioReport_DT.rdlc") //-------
            {
                thisDataSet = GetCDRatio_Detail(FDate, BRCD);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptGLBalanceDataTrf.rdlc") //-------
            {
                thisDataSet = GetGLBALDataDt(FDate, FBRCD, TBRCD);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;

                }
            }


            else if (RptName == "RptDailyBalanceLessThenClg.rdlc") //-------
            {
                thisDataSet = GetDailyLessClgBal(FDate, FBRCD, Prd, FL);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;

                }
            }
            else if (RptName == "RptRisktypeDetails.rdlc") //-------
            {
                thisDataSet = GetRiskdetails(Type);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;

                }
            }

            //Document Reg GetDocumentReg(string FD, string TD, string FDocT, string TDocT)
            else if (RptName == "RptDocumentReg.rdlc")
            {
                thisDataSet = GetDocumentReg(FDate, TDate, FromDOCType, ToDOCType, UName);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            if (RptName == "RptODlIstDivWise_TZMP.rdlc")
            {
                thisDataSet = RptODlIstDivWiseDT_TZMP(BRCD, FPRCD, TPRCD, AsOnDate, Fl, FLT);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //PLTransfer 19/04/2017
            else if (RptName == "RptPLTransfer.rdlc")
            {
                thisDataSet = GetPLRecord(FDATE, AC, FL, EDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptDivIntitalize.rdlc")
            {
                thisDataSet = GetDIVRecord(EDAT, BRCD, GL, Subgl, MID);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptBalanceS.rdlc")
            {
                thisDataSet = GetBalanceSheet(FL, BranchID, FDT);
                //thisDataSet = GetBalanceSheet(FDT, "LAB");  Rakesh 09-12-2016 
                //thisDataSet1 = GetBalanceSheet(FDT, "ASS"); Rakesh 09-12-2016
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
                // DRBAL = Convert.ToDouble(thisDataSet.Tables[0].Compute("SUM(DRBAL)", string.Empty));
                //  CRBAL = Convert.ToDouble(thisDataSet.Tables[0].Compute("SUM(CRBAL)", string.Empty));
            }


            else if (RptName == "RptBankRecosile2.rdlc")
            {
                thisDataSet = RptBankRecosile(FDT, TDT, FL, BRCD, ProdCode);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptPNProfitAndLoss.rdlc" || RptName == "RptPNProfitAndLoss_Marathi.rdlc" || RptName == "RptPNProfitAndLossSarjudas_Marathi.rdlc")
            {
                thisDataSet = GetPNPL(BranchID, FDT, TDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptIncomeExpReport.rdlc")
            {
                thisDataSet = GetIncomeExpPL(BranchID, FDT, TDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptLoanParameter.rdlc")
            {
                thisDataSet = GetLoanParameter(BRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            else if (RptName == "RptTrialBalance.rdlc" || RptName == "RptTrialBalance_M.rdlc")
            {
                thisDataSet = GetTraiBalance(FDT, TDT, BranchID, AC, FLT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptTrialBalance_FromTo.rdlc" || RptName == "RptTrialBal_FrmToMarathi.rdlc")
            {
                thisDataSet = GetTraiBalance_FT(FDT, TDT, BranchID, FLT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptTRAILBALANCE_DPLN.rdlc")
            {
                thisDataSet = GetTraiBalance_DPLN(FDT, TDT, BranchID);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptCuteBookRecSrno.rdlc")
            {
                thisDataSet = GetCutBookRecSrno(FDT, GL, SGL, MID, BRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptTrialBalanceSummary.rdlc" || RptName == "RptTrialBalanceSummary_M.rdlc")
            {
                thisDataSet = GetTraiBalance(FDT, TDT, BranchID, AC, FLT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            /////////Day Book

            else if (RptName == "RptDayBook.rdlc")
            {
                thisDataSet = GetDayBook(BranchID, FL, FLT, FDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptDayBook_PEN.rdlc")
            {
                thisDataSet = GetDayBook_Pen(BranchID, FL, FLT, FDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptDayBookDetails.rdlc")
            {
                thisDataSet = GetDayBookDeatils(BranchID, FL, FLT, FDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptDayBookRegistrerDetailsSetWise.rdlc")
            {
                thisDataSet = GetDayBookDeatilsSetWise(BranchID, FL, FLT, FDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptDayBook_ALLDetails.rdlc")
            {
                thisDataSet = GetDayBook_ALLDetails(BranchID, FL, FLT, FDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptSB_INTCalcPara.rdlc")
            {
                thisDataSet = SBS.GetSBIntCal_ParaRpt(BRCD, Prd);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptDivPayTrans.rdlc")
            {
                thisDataSet = GetDtDivPayTrans(Flag);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptDayBookReg_TZMP.rdlc")
            {
                thisDataSet = GETDayBookReg_TZMP(BranchID, FL, FLT, FDT, TDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptDayBookReg_FromTo.rdlc")
            {
                thisDataSet = GETDayBookReg_FromTo(BranchID, FL, FLT, FDT, TDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptDayBookReg_Renewal.rdlc")
            {
                thisDataSet = GetDayBookReg_Renewal(BranchID, FDT, TDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptDayBookDP_Register.rdlc")
            {
                thisDataSet = GetDayBookDP_Register(BranchID, FDT, TDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptIWClearngDetails.rdlc")
            {
                thisDataSet = GetIWRegDetails(BranchID, FBKcode, FDT, TDT, FL, "D");
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptSroMonthlyReport.rdlc")
            {
                thisDataSet = SRONO.RptSRNOMONTHLYRPT(FL, FDATE, TDATE, FBRCD, TBRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RPTSUMMARYSROM.rdlc")
            {
                thisDataSet = SRONO.RptSRNOMRPT(FL, FDATE, TDATE, FBRCD, TBRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RPTMONTHLYSROREPORT.rdlc")
            {
                thisDataSet = SRONO.MONTHLYRptSRNOMRPT(FL, FDATE, TDATE, FBRCD, TBRCD);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptClgRegList.rdlc")
            {
                thisDataSet = GetClgRegList(BranchID, FBKcode, FDT, FL, "D");
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptClearngRegister.rdlc")
            {
                thisDataSet = GetIWClgRegDetails(BranchID, FBKcode, FDT, SGL, FL, "D");
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptIWClearngSummary.rdlc")
            {
                thisDataSet = GetIWRegDetails(BranchID, FBKcode, FDT, TDT, FL, "S");
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptClgRegListSummary.rdlc")
            {
                thisDataSet = GetClgRegListSumy(BranchID, FBKcode, FDT, FL, "S");
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptClearngReturnRegister.rdlc")
            {
                thisDataSet = GetIOReturnReg(BranchID, FBKcode, FDT, FL, "S");
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptClassificationDPLN.rdlc")
            {
                thisDataSet = GetDPLNList(BranchID, FDT, FL, FLT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptClassificationDPLNSumy.rdlc")
            {
                thisDataSet = GetDPLNList(BranchID, FDT, FL, FLT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            /////////CTR report   

            else if (RptName == "RptCTR.rdlc")
            {

                thisDataSet = GetCTR(FDT, TDT, SUBGLFRM, SUBGLTO, CTRL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }

            }




            else if (RptName == "RptAVS5065.rdlc" || RptName == "RptLoanInthead.rdlc")//Dhanya Shetty//13-09-2017//For Loan Certificate
            {
                thisDataSet = GetLoanCert111(BRCD, LOANGL, ACCNO, Edate, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptAVS5066.rdlc" || RptName == "RptLoanclosure.rdlc")//Dhanya Shetty//13-09-2017//For Loan Closure
            {
                thisDataSet = GetLoanClosure(BRCD, LOANGL, ACCNO);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptFDClassificationList.rdlc")
            {

                thisDataSet = GetFDRClass(FDT, TDT, SUBGLFRM, SUBGLTO, FBC, TBC);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }

            }


            else if (RptName == "RptFormA.rdlc")
            {

                thisDataSet = GetFormA(SUBGLFRM, SUBGLTO, AsOnDate, FDT, TDT, CTRL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }

            }



            else if (RptName == "RptExecutionChargesLetter_ToSocityword.rdlc")
            {
                thisDataSet = SRONO.SP_RptExecutionChargesLetter_ToSocity(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            else if (RptName == "RptIntimation_Chequeword.rdlc")
            {
                thisDataSet = SRONO.SP_RptIntimation_Cheque(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptIntimation_ToSocityword.rdlc")
            {
                thisDataSet = SRONO.SP_RptIntimation_ToSocity(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "Rpt31Remainder_Noticeword.rdlc")
            {
                thisDataSet = SRONO.SP_Rpt31Remainder_Notice(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptFinalIntimationLetterword.rdlc")
            {
                thisDataSet = SRONO.SP_RptFinalIntimationLetter(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            else if (RptName == "RptAuction_BLetterword.rdlc" || RptName == "RptAutionMarathiword.rdlc" || RptName == "RPTPublicNotice(E_M)word.rdlc" || RptName == "RPTproposalforsaleword.rdlc")//|| RptName == "AuctionNotice(marathi).rdlc"
            {
                thisDataSet = SRONO.SP_RptAuction_BLetter(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptPublicLetterNotice_Sroword.rdlc")
            {
                thisDataSet = SRONO.SP_RptPublicLetterNotice_Sro(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptPropertyNotice_Sroword.rdlc")
            {
                thisDataSet = SRONO.RptNotice_PropertyAttOrdert(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptPublicLetter_Sroword.rdlc")
            {
                thisDataSet = SRONO.RptPublicLetter_Sro_N(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptVisit_Sroword.rdlc")
            {
                thisDataSet = SRONO.RptNotice_Visit(BRCD, Productcode, Accno, Edate, TDATE);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //------

            else if (RptName == "RptSymbolicNotice_Sroword.rdlc")
            {
                WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                thisDataSet = SRONO.RptNotices_SYMBOLIC(BRCD, Productcode, Accno, Edate, TDATE);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptBeforeAttchment_Sroword.rdlc")
            {
                thisDataSet = SRONO.RptNotices_NBA(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptIntimationNotice_Sroword.rdlc")
            {
                thisDataSet = SRONO.RptNotices_Valuation(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptProtectionNotice_Sroword.rdlc")
            {
                thisDataSet = SRONO.RptProtectionNotice_Sro(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptSushilLetter_Sro.rdlc" || RptName == "RptTermCondition_Noticeword.rdlc" || RptName == "RptTenderForm_Noticeword.rdlc")
            {
                thisDataSet = SRONO.RptNotices_Demand(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptPossessionNotice_Sroword.rdlc")
            {
                thisDataSet = SRONO.SP_RptNotice_PossionRpt(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptUpsetCoverletter_Sroword.rdlc" || RptName == "RptUpsetCoverletter_Sroword.rdlc")
            {
                thisDataSet = SRONO.RptUpsetCoverletter_Sro(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptPBalanceS_Marathi.rdlc")
            {
                thisDataSet = GetPBalanceSheet(FL, BranchID, FDT, TDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


          /////////KYC DOCMUNET  
            else if (RptName == "RptKycDoc.rdlc")
            {
                thisDataSet = GetKYC(Session["BRCD"].ToString(), FromDOCType);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptFDPrinting.rdlc" || RptName == "RptFDShiv.rdlc")
            {
                thisDataSet = GetFD(BRCD, SGL, accno, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptFDPrint_Palghar.rdlc")
            {
                thisDataSet = GetFD_PALGHAR(BRCD, SGL, accno, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

                //28/12/2018 rakesh

            else if (RptName == "RptFDBackPrint_Palghar.rdlc" || RptName == "RptFDBackPrint_MSEB.rdlc")
            {
                thisDataSet = GetFD_PALGHAR(BRCD, SGL, accno, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            else if (RptName == "RptFDPrint_MSEB.rdlc")
            {
                thisDataSet = GetFD_PALGHAR(BRCD, SGL, accno, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptFDPrintingShivSamarth.rdlc" || RptName == "RptFDPrintAkyt.rdlc" || RptName == "RptFDPrintAkyt_G.rdlc" || RptName == "RptFDPrintAkyt_N.rdlc")
            {
                thisDataSet = GetFDInfo(BRCD, SGL, accno, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }






            if (RptName == "RptPLExpenses.rdlc")
            {
                thisDataSet = GetPLExepenses(BranchID, FDT, TDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptPLExpenses_WithBal.rdlc")
            {
                thisDataSet = GetPLExepenses_Bal(BranchID, FDT, TDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptPLExpenses_SkipData.rdlc")
            {
                thisDataSet = GetPLExepenses_SkipData(BranchID, FDT, TDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptPLExpenses_WithMarathi.rdlc")
            {
                thisDataSet = GetPLExepenses_BalMarathi(BranchID, FDT, TDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptShrBalRegister.rdlc")
            {
                thisDataSet = GetShrBalRegister(FBRCD, TBRCD, FDT, TDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptShrBalRegisterSumry.rdlc")
            {
                thisDataSet = GetShrBalRegistersumry(FBRCD, TBRCD, FDT, TDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptShrBalanceCertWise.rdlc")
            {
                thisDataSet = GetShrBalCertWise(FBRCD, TBRCD, FDT, TDT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptBrWiseDepositLoanList.rdlc")
            {
                thisDataSet = GetBrDPLNList(AsOnDate, FL);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptAccOpCl.rdlc")////Added by ankita on 13/06/2017 To display account opening and closing details
            {
                thisDataSet = GetAccOpClRpt(FL, fdate, tdate, FBRCD, TBRCD, Subgl);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptLoanNill.rdlc")  //added by ashok misal
            {
                thisDataSet = GetloanNillRpt(fdate, tdate, FBRCD, TBRCD, Subgl, Productcode);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            if (RptName == "RptPLALLBrReport_CRDR.rdlc")  //added by ashok misal
            {
                thisDataSet = DetailsForPLALLBrReport(fdate, tdate, FBRCD, TBRCD, "R");
                thisDataSet1 = DetailsForPLALLBrReport_1(fdate, tdate, FBRCD, TBRCD, "P");
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptPLALLBrReport_PL.rdlc")  //added by ashok misal
            {
                thisDataSet = DtForPLALLBrReport_PL(fdate, tdate, FBRCD, TBRCD, "R");
                thisDataSet1 = DtForPLALLBrReport_PL_1(fdate, tdate, FBRCD, TBRCD, "P");
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //if (RptName == "RptIRegister.rdlc" || RptName == "RptJRegister.rdlc")////Added by ankita on 06/07/2017 to display IJRegister
            if (RptName == "RptIRegister.rdlc" || RptName == "RptIJReg.rdlc" || RptName == "RptJRegister.rdlc")////Added by ankita on 06/07/2017 to display IJRegister
            {

                if (RptName == "RptIRegister.rdlc")
                {
                    thisDataSet = GetIR(BRCD, AccNo, TACCNO);

                }
                if (RptName == "RptJRegister.rdlc")
                {
                    thisDataSet = GetJR(BRCD, AccNo, TACCNO);
                }
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //Loan Sanction Report Details


            if (RptName == "RptDepositSancSumry.rdlc")
            {
                thisDataSet = LS.DepositSancSumry(FBRCD, TBRCD, FPRCD, TPRCD, FLim, TLim, FDate, TDate, EDAT);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptPhotoSign.rdlc")
            {
                thisDataSet = PS.PhotoSignRpt(CustNo1, BRCD, CustNo2, FDate, TDate);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptPhotoNotscan.rdlc")
            {
                thisDataSet = PS.PhotoSignNotScannRpt(CustNo1, BRCD, CustNo2, FDate, TDate);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptDividantPayble.rdlc")
            {
                thisDataSet = LS.DividantPayble(FBRCD, FDate, TDate);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //Loan AMOUNT WISE REPORT
            if (RptName == "RptAVS5025.rdlc") //Ankita on 07/08/2017 to display loan amount wise report
            {
                thisDataSet = LS.LoanAmountWiseRpt(FBRCD, TBRCD, FPRCD, TPRCD, FDate, TDate, Type, AMT);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "Isp_AVS0038.rdlc") // For FD Interest Certificate
            {
                thisDataSet = Cust.FDInterestDetails(FDate, TDate, BRCD, CNO);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptTDSDetails.rdlc")
            {
                thisDataSet = Cust.TDSDetails(FDate, TDate, BRCD, CNO, FL);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptCustMobile.rdlc") // For FD Interest Certificate
            {
                thisDataSet = Cust.CustMobileDT(FBRCD, TBRCD, FDate, TDate);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }


            if (RptName == "RptAVS5085.rdlc")////Added by ankita on 16/02/2018 gold loan valuation list
            {
                thisDataSet = GetGld5085(FBRCD, TBRCD, FPRCD, FDATE, TDATE, EDAT, CURRATE);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptAuditrail1.rdlc") ////Added by ankita on 18/11/2017 CUSTOMER OPEN REPORT
            {
                thisDataSet = GetAuditrail(FBRCD, TBRCD, FL, FDATE, TDATE);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptAuditrail2.rdlc") ////Added by ankita on 18/11/2017 
            {
                thisDataSet = GetAuditrail(FBRCD, TBRCD, FL, FDATE, TDATE);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptAuditrail3.rdlc") ////Added by ankita on 18/11/2017 
            {
                thisDataSet = GetAuditrail(FBRCD, TBRCD, FL, FDATE, TDATE);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptAuditrail4.rdlc") ////Added by ankita on 18/11/2017 
            {
                thisDataSet = GetAuditrail(FBRCD, TBRCD, FL, FDATE, TDATE);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptAuditrail5.rdlc") ////Added by ankita on 18/11/2017 
            {
                thisDataSet = GetAuditrail(FBRCD, TBRCD, FL, FDATE, TDATE);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptAuditrail6.rdlc") ////Added by ankita on 18/11/2017 
            {
                thisDataSet = GetAuditrail(FBRCD, TBRCD, FL, FDATE, TDATE);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptAuditrail7.rdlc") ////Added by ankita on 18/11/2017 
            {
                thisDataSet = GetAuditrail1(FBRCD, TBRCD, FL, Edate, GL, FDATE, TDATE);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptDepositReg_EMP.rdlc")  //-------
            {
                thisDataSet = GetLoanDeposR_EMP(FL, PT, FDate);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;

                }
            }

            if (RptName == "RptRecPayCLBal_ALL.rdlc")
            {
                thisDataSet = RptRecPayCLBal_ALLDT(FBrcd, TBrcd, FDate, TDate);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }

            }
            else if (RptName == "RptAdmExpenses_Br.rdlc")
            {
                thisDataSet = GetAdmExpenses_Br(FBC, FDate, TDate, "DS");
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptShareTZMP.rdlc")
            {
                // string BRCD = Session["BRCD"].ToString();
                thisDataSet = RptShareTZMPDT(BRCD, FDATE, TDATE, FACCNO, TACCNO, FL, FLT);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptSanchitTZMP.rdlc")
            {
                // string BRCD = Session["BRCD"].ToString();
                thisDataSet = RptSanchitTZMPDT(BRCD, FDATE, TDATE, FACCNO, TACCNO, FL, FLT);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptAllLnBalListOD.rdlc")
            {
                // string BRCD = Session["BRCD"].ToString();
                thisDataSet = RptAllLnBalListODDT(BRCD, FPRCD, TPRCD, AsOnDate);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptOfficeGLDetails.rdlc")
            {
                thisDataSet = GetOfficeGLDetails(FBC, PT, FDate, TDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptAdmExpenses_DT.rdlc")
            {
                thisDataSet = GetAdmExpenses_DT(FBC, FDate, TDate, "D");
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptAdmExpenses_Sumry.rdlc")
            {
                thisDataSet = GetAdmExpenses_Sumry(FBC, FDate, TDate, "S");
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptAddressLabelPrint.rdlc")
            {
                thisDataSet = AddressLabelPrint(AccNo, TACCNO, FDate);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptPreSanLoanAPPList.rdlc")
            {
                thisDataSet = DTPreSanLoanAPPList(BRCD, FDate, TDate);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptSanLoanAPPList.rdlc")
            {
                thisDataSet = DTSanLoanAPPList(BRCD, FDate, TDate);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptDividentTRFProcess.rdlc")
            {
                thisDataSet = DTDividentTRFProcess(FDate, BRCD, PRDCD, TPRCD, MID);

                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptSBIntCalculation.rdlc")
            {
                thisDataSet1 = SB1.GetSBIntCal(BRCD, PRCD, FDate, TDate, Session["MID"].ToString());
                if (thisDataSet1 == null || thisDataSet1.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry There Is No Record...!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptCustomerBalane.rdlc")
            {
                thisDataSet = BKP.GetCustomerBalane(BRCD, AsOnDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptLoanBalanceList.rdlc")
            {
                thisDataSet = BKP.GetCustomerBalane(BRCD, AsOnDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "RptMobileData.rdlc")
            {
                thisDataSet = BKP.GetMobileData(FBrcd, TBrcd, FCustNo, TCustNo, AsOnDate, FL, FLT);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            if (RptName == "RptAVS51182.rdlc")
            {
                thisDataSet = BKP.GetAVS51182(FBrcd, FPRCD, FACCNO, TACCNO, AsOnDate, Amt, SL, S1, S2, S3, FL, FLT, Flag);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "AuctionNotice(marathi).rdlc")
            {
                thisDataSet = SRONO.SP_RptAuction_BLetter(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RPTproposalforsale.rdlc")
            {
                thisDataSet = SRONO.Sp_RPTproposalforsale(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptAuction_BLetter.rdlc" || RptName == "RPTPublicNotice(E_M).rdlc")//|| RptName == "AuctionNotice(marathi).rdlc"
            {
                thisDataSet = SRONO.SP_RptAuction_BLetter(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptAutionMarathi.rdlc")//|| RptName == "AuctionNotice(marathi).rdlc"
            {
                thisDataSet = SRONO.SP_RptAuction_Marathi(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptPublicLetterNotice_Sro.rdlc")
            {
                thisDataSet = SRONO.SP_RptPublicLetterNotice_Sro(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            if (RptName == "Rpt_TDWithdrwalVchr.rdlc")
            {
                thisDataSet = BKP.Get_TDWithdrwalVchr(BRCD, Prd, Acc, SETNO, AsOnDate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("Sorry No Record found......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            //else if (RptName == "RptDemandNotice_Sro.rdlc" || RptName == "RptBeforeAttchment_Sro.rdlc" || RptName == "RptAttchment_Sro.rdlc" || RptName == "RptVisit_Sro.rdlc" || RptName == "RptSymbolicNotice_Sro.rdlc" || RptName == "RptPropertyNotice_Sro.rdlc" || RptName == "RptAccAttchNotice_Sro.rdlc")
            //{
            //    thisDataSet = SRONO.RptNotices_SRO (BRCD, Productcode, Accno, Edate);
            //    if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
            //    {
            //        WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
            //        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
            //        return;
            //    }
            //}
            else if (RptName == "RptAccAttchNotice_Sro.rdlc")
            {
                thisDataSet = SRONO.RptNotice_ACAttOrder(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptPropertyNotice_Sro.rdlc")
            {
                thisDataSet = SRONO.RptNotice_PropertyAttOrdert(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptPublicLetter_Sro.rdlc")
            {
                thisDataSet = SRONO.RptPublicLetter_Sro_N(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptVisit_Sro.rdlc")
            {
                thisDataSet = SRONO.RptNotice_Visit(BRCD, Productcode, Accno, Edate, TDATE);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptSymbolicNotice_Sro.rdlc")
            {
                thisDataSet = SRONO.RptNotices_SYMBOLIC(BRCD, Productcode, Accno, Edate, TDATE);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptDemandNotice_Sro.rdlc")
            {
                thisDataSet = SRONO.RptNotices_Demand(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //----------------------------------------------------word
            else if (RptName == "RptDemandNotice_Sroword.rdlc")
            {
                thisDataSet = SRONO.RptNotices_Demand(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptAttchment_Sro.rdlc")
            {
                thisDataSet = SRONO.RptNotices_ATTACHMENTDATE(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

                //----------------------------word
            else if (RptName == "RptAttchment_Sroword.rdlc")
            {
                thisDataSet = SRONO.RptNotices_ATTACHMENTDATE(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptBeforeAttchment_Sro.rdlc")
            {
                thisDataSet = SRONO.RptNotices_NBA(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptIntimationNotice_Sro.rdlc")
            {
                thisDataSet = SRONO.RptNotices_Valuation(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptProtectionNotice_Sro.rdlc")
            {
                thisDataSet = SRONO.RptProtectionNotice_Sro(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptSushilLetter_Sro.rdlc" || RptName == "RptTermCondition_Notice.rdlc" || RptName == "RptTenderForm_Notice.rdlc")
            {
                thisDataSet = SRONO.RptNotices_Demand(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            else if (RptName == "RptPossessionNotice_Sro.rdlc")
            {
                thisDataSet = SRONO.SP_RptNotice_PossionRpt(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            //RptName == "RptProtectionNotice_Sro.rdlc" || 
            else if (RptName == "RptSushilLetter_Sro.rdlc" || RptName == "RptTermCondition_Notice.rdlc" || RptName == "RptTenderForm_Notice.rdlc")
            {
                thisDataSet = SRONO.RptNotices_SRO(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptUpsetCoverletter_Sro.rdlc" || RptName == "RptUpsetCoverletter_Sro.rdlc")
            {
                thisDataSet = SRONO.RptUpsetCoverletter_Sro(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }
            else if (RptName == "RptUpsetPrizeNotice_Sro.rdlc")
            {
                thisDataSet = SRONO.RptUpsetletter_Sro(BRCD, Productcode, Accno, Edate);
                if (thisDataSet == null || thisDataSet.Tables[0].Rows.Count == 0)
                {
                    WebMsgBox.Show("sorry There Is No Record......!!", this.Page);
                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
                    return;
                }
            }

            ReportDataSource DataSource = new ReportDataSource("ReportDS", thisDataSet.Tables["Table1"]);
            ReportDataSource DataSource1 = new ReportDataSource("ReportDS1", thisDataSet1.Tables["Table1"]);

            RdlcPrint.LocalReport.ReportPath = Server.MapPath("~/" + RptName + "");
            RdlcPrint.LocalReport.DataSources.Clear();
            RdlcPrint.LocalReport.DataSources.Add(DataSource);

            if (RptName == "RptBalanceS.rdlc")
            {
                RdlcPrint.LocalReport.DataSources.Add(DataSource);
                RdlcPrint.LocalReport.DataSources.Add(DataSource1);
            }
            RdlcPrint.LocalReport.Refresh();
            string fileName = "";

            //glname = BD.GetAccType(PT, Session["BRCD"].ToString());
            //int RC = 0;// LI.CheckAccount(AC, PT, Session["BRCD"].ToString());
            //accno = AC;
            //accname = "";//AO.Getcustname(RC.ToString(), Session["BRCD"].ToString());
            RdlcPrint.Visible = true;

            DataTable DT = new DataTable();
            DT = LG.GetBankName(Session["BRCD"].ToString());
            if (DT.Rows.Count > 0)
            {
                BName = DT.Rows[0]["BankName"].ToString();
                BRName = DT.Rows[0]["BranchName"].ToString();
                //string BB = DT.Rows[0]["BankName"].ToString();
                //string[] BRB = BB.Split('_');
                //BName = BRB[0].ToString();
                //BRName = BRB[1].ToString();
            }

            DT = LG.GetBankNameDetails(BranchID.ToString());
            if (DT.Rows.Count > 0)
            {
                BranchName = DT.Rows[0]["BranchName"].ToString();
            }
            DT = LG.getaddregno(Session["BRCD"].ToString());
            DataTable dt1 = new DataTable();
            dt1 = LG.getaddBankno(Session["BRCD"].ToString());
            if (DT.Rows.Count > 0)
            {
                ADDRESS2 = dt1.Rows[0]["ADDRESS2"].ToString();
                bankadd = dt1.Rows[0]["bankadd"].ToString();
                MOBILE = dt1.Rows[0]["MOBILE"].ToString();
                REGISTRATIONNO = dt1.Rows[0]["REGISTRATIONNO"].ToString();
                RegNo = dt1.Rows[0]["ADDRESS2"].ToString();
                BANKNAME_MAR = dt1.Rows[0]["BANKNAME_MAR"].ToString();
                REGISTRATIONNO1 = dt1.Rows[0]["regno"].ToString();
                DATEYEAR = dt1.Rows[0]["Dateyear"].ToString();
                //Perseva = dt1.Rows[0]["Perseva"].ToString();
            }

            DT = LG.GetGlname(Session["BRCD"].ToString(), SGL);
            if (DT.Rows.Count > 0)
            {

                glname = DT.Rows[0]["GLNAME"].ToString();
            }
            //////Dhanya shetty(To display month name in Report)
            //string sqlm = "Select DateName( month , DateAdd( month , (Select DATEPART(Month,Date)), -1 ) )";
            //monthdisp = conn.sExecuteScalar(sqlm);
            //To display year name
            //string sqly = "Select DATEPART(YEAR,Date)";
            //yeardisp = conn.sExecuteScalar(sqly);


            //RptCustWiseBalance
            if (RptName == "RptCustWiseBalance.rdlc")
            {
                fileName = "Customer Wise Balance";
                if (RptName == "RptCustWiseBalance.rdlc")
                {
                    RptName = "Customer Wise Balance";
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("AS_ON_DATE", ASONDT.ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
                }
            }

            if (RptName == "RptFedStatement.rdlc")
            {
                fileName = "Statement Report";

                RptName = "Customer Wise Balance";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("CUSTNO", CustNo);
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });

            }

            if (RptName == "RptOrdReceipt.rdlc")
            {
                fileName = "Receipt Report";

                RptName = "Receipt";
                //ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                //ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                //ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                //ReportParameter rp4 = new ReportParameter("CUSTNO", CustNo);
                //RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });

            }

            if (RptName == "RptLienMarkList.rdlc")
            {
                fileName = "Lien Report";
                if (RptName == "RptLienMarkList.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_ID", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("AsOnDate", AsOnDate.ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
                }
            }

            if (RptName == "GSTMasterRpt.rdlc")
            {
                fileName = "Customer Wise Balance";
                if (RptName == "GSTMasterRpt.rdlc")
                {
                    RptName = "Customer Wise Balance";
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                    // ReportParameter rp4 = new ReportParameter("AS_ON_DATE", ASONDT.ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                }
            }

            if (RptName == "RptMenu.rdlc")
            {
                fileName = "Menu Report";
                RptName = "Menu Report";
                ReportParameter rp1 = new ReportParameter("BANKNAME", BName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1 });
            }
            //RptDividendCalc
            if (RptName == "RptDividendCalc.rdlc")
            {
                fileName = "Shares Dividend Calc";
                if (RptName == "RptDividendCalc.rdlc")
                {
                    RptName = "Shares Dividend Calc";
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("FROM_DATE", FDATE.ToString());
                    ReportParameter rp5 = new ReportParameter("TO_DATE", TDATE.ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
                }
            }
            else if (RptName == "RptDigitalBanking.rdlc")
            {
                fileName = "Digital Banking Trail";
                if (RptName == "RptDigitalBanking.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("UserName", Session["UserName"].ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                }
            }
            //RptIWOWCharges
            if (RptName == "RptIWOWCharges.rdlc")
            {
                fileName = "IWOW Charges Report";
                if (RptName == "RptIWOWCharges.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_ID", Session["UserName"].ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                }
            }

            //Unpass
            if (RptName == "RptUnpassDetails.rdlc")
            {
                fileName = "Unpass Details";
                if (RptName == "RptUnpassDetails.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_ID", Session["UserName"].ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                }
            }

            //IW Return detials
            if (RptName == "RptIWReturnRegDetails.rdlc")
            {
                fileName = "IW Return Details";
                if (RptName == "RptIWReturnRegDetails.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_ID", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("FROM_DATE", FDT.ToString());
                    ReportParameter rp5 = new ReportParameter("TO_DATE", TDT.ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
                }
            }
            //IW Return SUMMARY
            if (RptName == "RptIWReturnRegSummary.rdlc")
            {
                fileName = "IW Return Summary";
                if (RptName == "RptIWReturnRegSummary.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_ID", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("FROM_DATE", FDT.ToString());
                    ReportParameter rp5 = new ReportParameter("TO_DATE", TDT.ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
                }
            }
            //OW Return DETAILS
            if (RptName == "RptOWReturnRegDetails.rdlc")
            {
                fileName = "OW Return Details";
                if (RptName == "RptOWReturnRegDetails.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_ID", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("FROM_DATE", FDT.ToString());
                    ReportParameter rp5 = new ReportParameter("TO_DATE", TDT.ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
                }
            }
            //OW Return SUMMARY
            if (RptName == "RptOWReturnRegSummary.rdlc")
            {
                fileName = "OW Return Summary";
                if (RptName == "RptOWReturnRegSummary.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_ID", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("FROM_DATE", FDT.ToString());
                    ReportParameter rp5 = new ReportParameter("TO_DATE", TDT.ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
                }
            }


            //loan schedule Report
            if (RptName == "Rpt_LoanSchedule_Parti.rdlc")
            {
                fileName = "Loan Schedule Report";
                if (RptName == "Rpt_LoanSchedule_Parti.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                }
            }

            if (RptName == "RptTransfer.rdlc")
            {
                fileName = "Transfer Print Report";
                if (RptName == "RptTransfer.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_NAME", UName.ToString());
                    ReportParameter rp4 = new ReportParameter("FROM_DATE", FDT.ToString());
                    ReportParameter rp5 = new ReportParameter("TO_DATE", TDT.ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
                }
            }

            //RptDDSCMonthlySummary
            if (RptName == "RptDDSCMonthlySummary.rdlc")
            {
                fileName = "DDS Monthly Summary";
                if (RptName == "RptDDSCMonthlySummary.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("REPORT_NAME", "DDS MONTHLY SUMMARY");
                    ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
                }
            }

            if (RptName == "RptPLTransfer.rdlc")// Amruta 19/04/2017- PLTransfer
            {
                fileName = "PL";
                if (RptName == "RptPLTransfer.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("FDate", FDATE);
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
                }
            }
            if (RptName == "RptDivIntitalize.rdlc")// Amruta 19/04/2017- PLTransfer
            {
                fileName = "RptDivIntitalize";
                if (RptName == "RptDivIntitalize.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("FDate", EDAT);
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
                }
            }

            if (RptName == "RptFDINTCalculation.rdlc")
            {
                fileName = "Trail Entry Report";
                if (RptName == "RptFDINTCalculation.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                }
            }

            else if (RptName == "RptBalanceS_Marathi.rdlc" || RptName == "RptBalanceSarjudas_Marathi.rdlc")
            {
                fileName = "Balance Sheet";
                ReportParameter rp1 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNM", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RGNO", BranchName);
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                //ReportParameter rp5 = new ReportParameter("DRSM", DRBAL.ToString());
                //ReportParameter rp6 = new ReportParameter("CRSM", CRBAL.ToString());
                // RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }


            if (RptName == "RptAVS5143.rdlc" || RptName == "RptAVS5143_BACK.rdlc" || RptName == "RptAVS5143_Marathi.rdlc")
            {
                string separateDate = AO.SeparateDateFormat(chqPrintDate.ToString());
                fileName = "RptAVS5143.rdlc";
                ReportParameter rp1 = new ReportParameter("Name", Name.ToString());
                ReportParameter rp2 = new ReportParameter("TotalPay", TotalPay.ToString());
                ReportParameter rp3 = new ReportParameter("D1", D1.ToString());
                ReportParameter rp4 = new ReportParameter("D2", D2.ToString());
                ReportParameter rp5 = new ReportParameter("M1", M1.ToString());
                ReportParameter rp6 = new ReportParameter("M2", M2.ToString());
                ReportParameter rp7 = new ReportParameter("Y1", Y1.ToString());
                ReportParameter rp8 = new ReportParameter("Y2", Y2.ToString());
                ReportParameter rp9 = new ReportParameter("Y3", Y3.ToString());
                ReportParameter rp10 = new ReportParameter("Y4", Y4.ToString());

                ReportParameter rp11 = new ReportParameter("ChqPrintDate", separateDate);
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11 });
            }
            ////-------Bill spec all
            else if (RptName == "RptLoanSchedule.rdlc")
            {
                fileName = "Loan Schedule";
                if (RptName == "RptLoanSchedule.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("GLNAME", glname.ToString());
                    ReportParameter rp2 = new ReportParameter("ACCNO", accno.ToString());
                    ReportParameter rp3 = new ReportParameter("AccName", accname.ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                }
            }
            else if (RptName == "RPTDDSIntMst.rdlc")
            {
                fileName = "DDS Interest Master";
                if (RptName == "RPTDDSIntMst.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("UserName", Session["UserName"].ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                }
            }
            if (RptName == "RptLoanRegister.rdlc")
            {
                fileName = "Loan Register";
                if (RptName == "RptLoanRegister.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_NAME", UName.ToString());
                    ReportParameter rp4 = new ReportParameter("FDATE", FDate.ToString());
                    ReportParameter rp5 = new ReportParameter("TDATE", TDate.ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
                }
            }
            //Day Activate View

            else if (RptName == "RptDayActivity.rdlc")
            {
                fileName = "DAY ACTIVITY VIEW";
                if (RptName == "RptDayActivity.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("UserName", Session["UserName"].ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                }
            }




                ////REPORT OF ACCOUNT NO. COUNT
            else if (RptName == "RptAccCount.rdlc")
            {
                fileName = "Account No. Count";
                if (RptName == "RptAccCount.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("Bank_Name", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("Branch_Name", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("GLCODE", GL.ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
                }

            }


            //Cheque Issue RegisterReport
            else if (RptName == "RptChequeIssueRegister.rdlc")
            {
                fileName = "ChequeIssueRegister_";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("AsOnDate", AC.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }

            //Cheque Stock Report
            else if (RptName == "RptChequeStock.rdlc")
            {
                fileName = "ChequeStockReport_";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("AsOnDate", AC.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }

            //Saving Passbook Print
            else if (RptName == "RptSavingPassBook.rdlc")
            {
                fileName = "SavingPassbookPrintReport_";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("AsOnDate", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp5 = new ReportParameter("NoofLinePerPage", LPP.ToString());
                ReportParameter rp6 = new ReportParameter("BlankLines", BlankLine.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }
            //Account Open Close Report
            else if (RptName == "RptAccountOpenCloseReport.rdlc")
            {
                fileName = "AccountOpenCloseReport_";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("AsOnDate", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }



            //Un-operative Accounts Report
            else if (RptName == "RptUnOpAccts.rdlc")
            {
                fileName = "UnOperativeAccts_";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("AsOnDate", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }

            else if (RptName == "RptTopDepositList.rdlc")
            {
                fileName = "TopDepositList_";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("REGNO", BRName);
                ReportParameter rp3 = new ReportParameter("USERID", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptTopLoanList.rdlc")
            {
                fileName = "TopLoanList";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("REGNO", ACST.GetBranchName(FBC));
                ReportParameter rp3 = new ReportParameter("USERID", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDate.ToString());
                ReportParameter rp5 = new ReportParameter("FD", FD.ToString());
                ReportParameter rp6 = new ReportParameter("GL", GL.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }
            //For Vault cash Denomination
            else if (RptName == "RPTCashDenom.rdlc")
            {
                fileName = "VaultCashDenomination_";
                ReportParameter rp1 = new ReportParameter("ASONDATE", AsOnDate.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName);
                ReportParameter rp3 = new ReportParameter("REGNO", BRName);
                ReportParameter rp4 = new ReportParameter("FBRCD", FBRCD);
                ReportParameter rp5 = new ReportParameter("TBRCD", TBRCD);
                ReportParameter rp6 = new ReportParameter("USERID", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }

            else if (RptName == "RptCutBook.rdlc" || RptName == "RptCutBookSa.rdlc" || RptName == "RptCuteBookRecSrno.rdlc")
            {
                fileName = "CutBook Report_" + glname;
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp5 = new ReportParameter("GLNAME", glname.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptCutBook_Palghar.rdlc")
            {
                fileName = "CutBook Report_" + glname;
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp5 = new ReportParameter("GLNAME", glname.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            if (RptName == "RptODlIstDivWise_TZMP.rdlc")
            {
                fileName = "RptODlIstDivWise_TZMP";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FROMDATE", AsOnDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }

            else if (RptName == "RptCuteBookDetails.rdlc")
            {
                fileName = "RptCuteBookDetails";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp4 = new ReportParameter("ToDate", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }


            else if (RptName == "NoticeB_Attasale.rdlc")
            {
                fileName = "NoticeB_Attasale";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                ReportParameter rp7 = new ReportParameter("AddressS", lrr.GetAddressSelected(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString()));
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7 });
            }
            else if ((RptName == "RptNPADetails_1.rdlc" || RptName == "RptNPASumry_1.rdlc")) // OD List
            {
                fileName = "RptNPAList";
                ReportParameter rp1 = new ReportParameter("ASONDATE", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("FBrcd", FBRCD.ToString());
                ReportParameter rp5 = new ReportParameter("TBrcd", TBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("FPrd", FPRCD.ToString());
                ReportParameter rp7 = new ReportParameter("TPrd", TPRCD.ToString());
                ReportParameter rp8 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8 });
            }
            else if (RptName == "NoticeB_Attasale1.rdlc")
            {
                fileName = "NoticeB_Attasale1";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("NAME", UName.ToString());
                ReportParameter rp7 = new ReportParameter("Address", lrr.GetSurityAdd(Session["BRCD"].ToString(), CustNo));
                ReportParameter rp8 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                ReportParameter rp9 = new ReportParameter("AddressS", lrr.GetAddressSelectedGar(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString(), CustNo.ToString()));
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9 });

            }

            //11012019rakesh
            else if (RptName == "RptShareCertiShr.rdlc")
            {

                fileName = "RptShareCerti";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                // ReportParameter rp4 = new ReportParameter("UserId", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }
            else if (RptName == "RptConfiscation09.rdlc")//// Added by ankita on 09/06/2017 to display confiscation warrant
            {
                fileName = "RptConfiscation09";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                ReportParameter rp11 = new ReportParameter("AddressS", lrr.GetAddressSelected(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString()));
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11 });
            }
            else if (RptName == "RptPossesionNotice12.rdlc")//// Added by ankita on 10/06/2017 to display Possesion notice
            {
                fileName = "RptPossesionNotice12";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10 });
            }
            else if (RptName == "RptDeclaration13.rdlc")//// Added by ankita on 10/06/2017 to display declaration notice
            {
                fileName = "RptDeclaration13";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10 });
            }
            else if (RptName == "PropertyRaid.rdlc")//Amruta 09/06/2017
            {
                fileName = "PropertyRaid";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10 });
            }
            else if (RptName == "RptDormantAcList.rdlc" || RptName == "RptDormantDueAcList.rdlc")
            {
                ReportParameter rp1 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp5 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp6 = new ReportParameter("Period", FL.ToString());
                ReportParameter rp7 = new ReportParameter("Amount", Amt.ToString());
                fileName = "RptDormantAcList";
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp3, rp4, rp5, rp6, rp7 });
            }
            else if (RptName == "PropertyRaid2.rdlc")//Amruta 09/06/2017
            {
                fileName = "PropertyRaid";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10 });
            }

            else if (RptName == "RptRaidPaid.rdlc")//Amruta 09/06/2017
            {
                fileName = "PropertyRaid";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("Surity", UN.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10 });
            }
            else if (RptName == "PropertyRaid3.rdlc")//Amruta 09/06/2017
            {
                fileName = "PropertyRaid";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                ReportParameter rp11 = new ReportParameter("Address", lrr.TalukaForLoanee(Session["BRCD"].ToString(), Productcode, Accno));
                ReportParameter rp12 = new ReportParameter("District", lrr.DistrictForLoanee(Session["BRCD"].ToString(), Productcode, Accno));
                ReportParameter rp13 = new ReportParameter("SROName", lrr.GetSROName(BRCD, Productcode, Accno));
                ReportParameter rp14 = new ReportParameter("Village", Village.ToString());
                ReportParameter rp15 = new ReportParameter("taluka", taluka.ToString());
                ReportParameter rp16 = new ReportParameter("Dist", Dist.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11, rp12, rp13, rp14, rp15, rp16 });
            }
            else if (RptName == "PropertyRaid31.rdlc")//Amruta 09/06/2017
            {
                fileName = "PropertyRaid";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("Name", Name.ToString());
                ReportParameter rp11 = new ReportParameter("Address", lrr.TalukaForSurity(Session["BRCD"].ToString(), CustNo));
                ReportParameter rp12 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                ReportParameter rp13 = new ReportParameter("District", lrr.DistrictForSurity(Session["BRCD"].ToString(), CustNo));
                ReportParameter rp17 = new ReportParameter("SROName", lrr.GetSROName(BRCD, Productcode, Accno));
                // ReportParameter rp14 = new ReportParameter("Village", Village.ToString());
                ReportParameter rp15 = new ReportParameter("taluka", taluka.ToString());
                ReportParameter rp16 = new ReportParameter("Dist", Dist.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11, rp12, rp13, rp15, rp16, rp17 });
            }


            else if (RptName == "RptSRONotice10.rdlc")
            {
                fileName = "RptSRONotice10";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                //ReportParameter rp9 = new ReportParameter("perseva", Perseva.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp7, rp8 });
            }
            else if (RptName == "RptOrderNotice.rdlc")
            {
                fileName = "RptOrderNotice";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("ComName", lrr.CompanyName(BRCD, Productcode, Accno));
                ReportParameter rp11 = new ReportParameter("Orderdate", lrr.Orderdate(BRCD, Productcode, Accno));
                ReportParameter rp12 = new ReportParameter("SalDate", lrr.SalDate(BRCD, Productcode, Accno));
                ReportParameter rp13 = new ReportParameter("DemandDate", lrr.DemandDate(BRCD, Productcode, Accno));
                ReportParameter rp14 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                ReportParameter rp15 = new ReportParameter("SROName", lrr.GetSROName(BRCD, Productcode, Accno));
                ReportParameter rp16 = new ReportParameter("ComAddr", lrr.getComapnyAdd1(BRCD, Productcode, Accno));
                ReportParameter rp17 = new ReportParameter("CompAddr1", lrr.GetComapnyAdd(BRCD, CustNo));
                ReportParameter rp18 = new ReportParameter("AddressS", lrr.GetAddressSelected(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString()));
                ReportParameter rp19 = new ReportParameter("Title", Title.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11, rp12, rp13, rp14, rp15, rp16, rp17, rp18, rp19 });
            }
            else if (RptName == "RptAVS5077.rdlc" || RptName == "RptSavingCerti.rdlc")
            {
                fileName = "RptAVS5077";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }


            else if (RptName == "RptPrastav.rdlc")
            {
                fileName = "RptPrastav";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("ComName", lrr.CompanyName(BRCD, Productcode, Accno));
                ReportParameter rp16 = new ReportParameter("ComAddr", lrr.getComapnyAdd1(BRCD, Productcode, Accno));
                ReportParameter rp17 = new ReportParameter("CompAddr1", lrr.GetComapnyAdd(BRCD, CustNo));
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp16, rp17 });
            }
            else if (RptName == "RptRecoveryCertificateRem21.rdlc")
            {
                fileName = "RptRecoveryCertificateRem21";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("ComName", lrr.CompanyName(BRCD, Productcode, Accno));
                ReportParameter rp11 = new ReportParameter("Orderdate", lrr.Orderdate(BRCD, Productcode, Accno));
                ReportParameter rp12 = new ReportParameter("SalDate", lrr.SalDate(BRCD, Productcode, Accno));
                //  ReportParameter rp13 = new ReportParameter("DemandDate", lrr.DemandDate(BRCD, Productcode, Accno));
                ReportParameter rp14 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                ReportParameter rp15 = new ReportParameter("SROName", lrr.GetSROName(BRCD, Productcode, Accno));
                ReportParameter rp16 = new ReportParameter("ComAddr", lrr.getComapnyAdd1(BRCD, Productcode, Accno));
                ReportParameter rp17 = new ReportParameter("CompAddr1", lrr.GetComapnyAdd(BRCD, CustNo));
                ReportParameter rp18 = new ReportParameter("Ncustname", Name.ToString());
                ReportParameter rp19 = new ReportParameter("AddressS", lrr.GetAddressSelectedGar(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString(), CustNo.ToString()));
                ReportParameter rp20 = new ReportParameter("Title", Title.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11, rp12, rp14, rp15, rp16, rp17, rp18, rp19, rp20 });
            }
            else if (RptName == "RptRecoveryCertificateRem12.rdlc")
            {
                fileName = "RptRecoveryCertificateRem12";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("ComName", lrr.CompanyName(BRCD, Productcode, Accno));
                ReportParameter rp11 = new ReportParameter("Orderdate", lrr.Orderdate(BRCD, Productcode, Accno));
                ReportParameter rp12 = new ReportParameter("SalDate", lrr.SalDate(BRCD, Productcode, Accno));
                ReportParameter rp13 = new ReportParameter("DemandDate", lrr.DemandDate(BRCD, Productcode, Accno));
                ReportParameter rp14 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                ReportParameter rp15 = new ReportParameter("SROName", lrr.GetSROName(BRCD, Productcode, Accno));
                ReportParameter rp16 = new ReportParameter("ComAddr", lrr.getComapnyAdd1(BRCD, Productcode, Accno));
                ReportParameter rp17 = new ReportParameter("CompAddr1", lrr.GetComapnyAdd(BRCD, CustNo));
                ReportParameter rp18 = new ReportParameter("Ncustname", Name.ToString());
                ReportParameter rp19 = new ReportParameter("AddressS", lrr.GetAddressSelectedGar(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString(), CustNo.ToString()));
                ReportParameter rp20 = new ReportParameter("Title", Title.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11, rp12, rp13, rp14, rp15, rp16, rp17, rp18, rp19, rp20 });
            }
            else if (RptName == "RptOrderNotice1.rdlc")
            {
                fileName = "RptOrderNotice";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("ComName", lrr.CompanyNameForSurity(BRCD, Productcode, Accno, CustNo));
                ReportParameter rp11 = new ReportParameter("Orderdate", lrr.Orderdate(BRCD, Productcode, Accno));
                ReportParameter rp12 = new ReportParameter("SalDate", lrr.SalDate(BRCD, Productcode, Accno));
                ReportParameter rp13 = new ReportParameter("DemandDate", lrr.DemandDate(BRCD, Productcode, Accno));
                ReportParameter rp14 = new ReportParameter("Name", Name.ToString());
                ReportParameter rp15 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                ReportParameter rp16 = new ReportParameter("SROName", lrr.GetSROName(BRCD, Productcode, Accno));
                ReportParameter rp17 = new ReportParameter("ComAddr", lrr.getComapnyAdd1(BRCD, Productcode, Accno));
                ReportParameter rp18 = new ReportParameter("CompAddr1", lrr.GetComapnyAdd(BRCD, CustNo));
                ReportParameter rp19 = new ReportParameter("AddressS", lrr.GetAddressSelectedGar(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString(), CustNo.ToString()));
                ReportParameter rp20 = new ReportParameter("Title", Title.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11, rp12, rp13, rp14, rp15, rp16, rp17, rp18, rp19, rp20 });
            }
            else if (RptName == "RptWorkerAccountant.rdlc")
            {
                fileName = "RptWorkerAccountant";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                ReportParameter rp11 = new ReportParameter("VillageAddr", lrr.GetVillageAdd(BRCD, Productcode, Accno));
                ReportParameter rp12 = new ReportParameter("Village", Village.ToString());
                ReportParameter rp13 = new ReportParameter("taluka", taluka.ToString());
                ReportParameter rp14 = new ReportParameter("Dist", Dist.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11, rp12, rp13, rp14 });
            }

            else if (RptName == "RptWorkerAccountant1.rdlc")
            {
                fileName = "RptWorkerAccountant";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("Name", Name.ToString());
                // ReportParameter rp11 = new ReportParameter("Address", Address.ToString());
                ReportParameter rp12 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                ReportParameter rp13 = new ReportParameter("VillageAddr1", lrr.GetVillagadd2(BRCD, CustNo));
                ReportParameter rp14 = new ReportParameter("Village", Village.ToString());
                ReportParameter rp15 = new ReportParameter("taluka", taluka.ToString());
                ReportParameter rp16 = new ReportParameter("Dist", Dist.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp12, rp13, rp14, rp15, rp16 });
            }

            else if (RptName == "RptDemandNotice.rdlc")
            {
                fileName = "RptDemandNotice";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("duedate", lrr.getdateDUE(Edate));
                ReportParameter rp9 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                ReportParameter rp10 = new ReportParameter("Hno", lrr.HregNo(BRCD, Productcode, Accno));
                ReportParameter rp11 = new ReportParameter("AddressS", lrr.GetAddressSelected(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString()));
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11 });
            }

            else if (RptName == "RptNoticeBr10.rdlc")
            {
                fileName = "RptNoticeBr10";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("duedate", lrr.getdateDUE(Edate));
                ReportParameter rp9 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                ReportParameter rp10 = new ReportParameter("Hno", lrr.HregNo(BRCD, Productcode, Accno));
                ReportParameter rp11 = new ReportParameter("AddressS", lrr.GetAddressSelected(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString()));
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11 });
            }





            else if (RptName == "RptLoanRepayCerti.rdlc")
            {
                fileName = "RptLoanRepayCerti";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp6 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO1.ToString());
                ReportParameter rp7 = new ReportParameter("Fdate", FDate.ToString());
                ReportParameter rp8 = new ReportParameter("Tdate", TDate.ToString());
                ReportParameter rp9 = new ReportParameter("OUTNO", OUTNO.ToString());
                ReportParameter rp10 = new ReportParameter("DATEYEAR", DATEYEAR.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10 });
            }
            else if (RptName == "RptAVS5065.rdlc" || RptName == "RptLoanInthead.rdlc")
            {
                fileName = "RptAVS5065.rdlc";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", lrr.GetRegno(Session["BRCD"].ToString()));
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp6 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp7 = new ReportParameter("Fdate", FDate.ToString());
                ReportParameter rp8 = new ReportParameter("Tdate", TDate.ToString());
                ReportParameter rp9 = new ReportParameter("LOANGL", LOANGL.ToString());
                ReportParameter rp10 = new ReportParameter("ACCNO", ACCNO.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10 });
            }

            else if (RptName == "RptAVS5066.rdlc" || RptName == "RptLoanclosure.rdlc")
            {
                fileName = "RptAVS5066";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", lrr.GetRegno(Session["BRCD"].ToString()));
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp6 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp7 = new ReportParameter("ACCNO", ACCNO.ToString());
                ReportParameter rp8 = new ReportParameter("LOANGL", LOANGL.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8 });
            }

            else if (RptName == "RptRecoveryCertificateRem1.rdlc")//Dhanya Shetty//09-09-2017//For RecoveryCertificate 1
            {
                fileName = "Recovery Certificateone";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("ComName", lrr.CompanyName(BRCD, Productcode, Accno));
                ReportParameter rp11 = new ReportParameter("Orderdate", lrr.Orderdate(BRCD, Productcode, Accno));
                ReportParameter rp12 = new ReportParameter("SalDate", lrr.SalDate(BRCD, Productcode, Accno));
                ReportParameter rp13 = new ReportParameter("Ncustname", Ncustname.ToString());
                ReportParameter rp14 = new ReportParameter("Ncustadd", string.IsNullOrEmpty(Ncustadd) ? "" : Ncustadd.ToString());
                ReportParameter rp15 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                ReportParameter rp16 = new ReportParameter("ComAddr", lrr.getComapnyAdd2(BRCD, Productcode, Accno));
                ReportParameter rp17 = new ReportParameter("AddressS", lrr.GetAddressSelected(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString()));
                ReportParameter rp18 = new ReportParameter("Title", Title.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11, rp12, rp13, rp14, rp15, rp16, rp17, rp18 });
            }
            else if (RptName == "RptRecoveryCertificateRem2.rdlc")//Dhanya Shetty//09-09-2017//For RecoveryCertificate 2
            {
                fileName = "Recovery Certificate1";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("ComName", lrr.CompanyName(BRCD, Productcode, Accno));
                ReportParameter rp11 = new ReportParameter("Orderdate", lrr.Orderdate(BRCD, Productcode, Accno));
                ReportParameter rp12 = new ReportParameter("SalDate", lrr.SalDate(BRCD, Productcode, Accno));
                ReportParameter rp13 = new ReportParameter("Ncustname", Ncustname.ToString());
                ReportParameter rp14 = new ReportParameter("Ncustadd", string.IsNullOrEmpty(Ncustadd) ? "" : Ncustadd.ToString());
                ReportParameter rp15 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                ReportParameter rp16 = new ReportParameter("ComAddr", lrr.getComapnyAdd2(BRCD, Productcode, Accno));
                ReportParameter rp17 = new ReportParameter("AddressS", lrr.GetAddressSelected(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString()));
                ReportParameter rp18 = new ReportParameter("Title", Title.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11, rp12, rp13, rp14, rp15, rp16, rp17, rp18 });
            }
            else if (RptName == "RptLabourAccountent17.rdlc")//Dhanya Shetty//09-09-2017//For Labour Accountant
            {
                fileName = "LabourAccountant1";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("Hno", lrr.HregNo(BRCD, Productcode, Accno));
                ReportParameter rp11 = new ReportParameter("Villadd", lrr.GetAdd(BRCD, Productcode, Accno));
                ReportParameter rp12 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                ReportParameter rp13 = new ReportParameter("VillageAddr1", lrr.GetVillagadd2(BRCD, CustNo));
                ReportParameter rp14 = new ReportParameter("Village", Village.ToString());
                ReportParameter rp15 = new ReportParameter("taluka", taluka.ToString());
                ReportParameter rp16 = new ReportParameter("Dist", Dist.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11, rp12, rp14, rp15, rp16 });
            }
            else if (RptName == "RptLabourAccountent171.rdlc")//Dipali Nagare//07-07-2017//For Labour Accountant
            {
                fileName = "LabourAccountant1";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("Hno", lrr.HregNo(BRCD, Productcode, Accno));
                ReportParameter rp11 = new ReportParameter("Villadd", lrr.GetAdd(BRCD, Productcode, Accno));
                ReportParameter rp12 = new ReportParameter("Name", Name.ToString());
                ReportParameter rp13 = new ReportParameter("Villadd", lrr.GetAdd(BRCD, Productcode, Accno));
                ReportParameter rp14 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                ReportParameter rp15 = new ReportParameter("VillageAddr1", lrr.GetVillagadd2(BRCD, CustNo));
                ReportParameter rp16 = new ReportParameter("Village", Village.ToString());
                ReportParameter rp17 = new ReportParameter("taluka", taluka.ToString());
                ReportParameter rp18 = new ReportParameter("Dist", Dist.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11, rp12, rp13, rp14, rp15, rp16, rp17, rp18 });
            }
            else if (RptName == "RptReceiptPrintYuva1.rdlc")// Added by prerana pawar on 11/07/2018
            {
                fileName = "RptYuva";
                ReportParameter rp1 = new ReportParameter("BANKNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("SetNo", SETNO.ToString());
                ReportParameter rp3 = new ReportParameter("ADDRESS1", VA.GetAdd1(Session["BNKCDE"].ToString(), Session["BRCD"].ToString()));
                ReportParameter rp4 = new ReportParameter("ADDRESS2", VA.GetAdd2(Session["BNKCDE"].ToString(), Session["BRCD"].ToString()));
                ReportParameter rp5 = new ReportParameter("PRINT_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("Mob", MOBILE.ToString());

                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }


            else if (RptName == "ShareRegister.rdlc")//Dhanya Shety//30-12-2017//For ShareRegister
            {
                fileName = "ShareRegister";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp5 = new ReportParameter("TDATE", TDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }



            else if (RptName == "RPTAVS5097.rdlc")//Dhanya Shety//09-03-2018//For StockReport
            {
                fileName = "RPTAVS5097";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FDate ", FDate.ToString());
                ReportParameter rp5 = new ReportParameter("TDate ", TDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }

            else if (RptName == "RptLabour2Accountent17.rdlc")//Dhanya Shetty//09-09-2017//For Labour Accountant
            {
                fileName = "LabourAccountant1";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("Hno", lrr.HregNo(BRCD, Productcode, Accno));
                ReportParameter rp11 = new ReportParameter("Villadd", lrr.GetAdd(BRCD, Productcode, Accno));
                ReportParameter rp12 = new ReportParameter("Ncustname", Ncustname.ToString());
                ReportParameter rp13 = new ReportParameter("Ncustadd", string.IsNullOrEmpty(Ncustadd) ? "" : Ncustadd.ToString());
                ReportParameter rp14 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                ReportParameter rp15 = new ReportParameter("VillageAddr1", lrr.GetVillagadd2(BRCD, CustNo));
                ReportParameter rp16 = new ReportParameter("Village", Village.ToString());
                ReportParameter rp17 = new ReportParameter("taluka", taluka.ToString());
                ReportParameter rp18 = new ReportParameter("Dist", Dist.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11, rp12, rp13, rp14, rp15, rp16, rp17, rp18 });
            }

            else if (RptName == "RptJaptiNotice.rdlc")
            {
                fileName = "RptJaptiNotice";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                ReportParameter rp10 = new ReportParameter("AddressS", lrr.GetAddressSelected(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString()));
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp10 });
            }
            else if (RptName == "RptJaptiNotice1.rdlc")
            {
                fileName = "RptJaptiNotice1";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("Name", Name.ToString());
                ReportParameter rp9 = new ReportParameter("Address", lrr.GetSurityAdd(Session["BRCD"].ToString(), CustNo));
                ReportParameter rp10 = new ReportParameter("CoBorrower", lrr.getcoborowername(BRCD, Productcode, Accno));
                ReportParameter rp11 = new ReportParameter("AddressS", lrr.GetAddressSelectedGar(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString(), CustNo.ToString()));
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11 });
            }


            else if (RptName == "RptVisitnotice.rdlc")
            {
                fileName = "RptVisitnotice";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("AddressS", lrr.GetAddressSelected(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString()));
                ReportParameter rp11 = new ReportParameter("BANKNAME_MAR", BANKNAME_MAR.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11 });
            }
            else if (RptName == "RptActiveMem.rdlc")
            {
                fileName = "RptActiveMem";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FromDate", FDT.ToString());
                //ReportParameter rp4 = new ReportParameter("TDate", TDT.ToString());
                ReportParameter rp5 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp5 });
            }
            else if (RptName == "RptVoucherDetailsList.rdlc")
            {
                fileName = "RptVoucherDetailsList_";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("FromDate", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("ToDate", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }


            else if (RptName == "RptDemandRecList.rdlc" || RptName == "RptDemandRecList_DT.rdlc")
            {
                fileName = "RptDemandRecList.rdlc";
                ReportParameter rp1 = new ReportParameter("BranchName", BRName.ToString());
                ReportParameter rp2 = new ReportParameter("BankName", BName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("BRCD", BRCD.ToString());
                ReportParameter rp5 = new ReportParameter("Month", Month.ToString());
                ReportParameter rp6 = new ReportParameter("Year", Year.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }
            else if (RptName == "RptVoucherSummaryList.rdlc")
            {
                fileName = "RptVoucherSummaryList_";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("FromDate", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("ToDate", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptInComeTaxReport.rdlc")
            {
                fileName = "RptInComeTaxReport_";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("FromDate", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("ToDate", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }

            else if (RptName == "RptInComeTaxReport_SHR.rdlc")
            {
                fileName = "RptInComeTaxReport_";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("FromDate", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("ToDate", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptInComeTaxReport_DP.rdlc")
            {
                fileName = "RptInComeTaxReport_";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("FromDate", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("ToDate", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptInComeTaxReport_LN.rdlc")
            {
                fileName = "RptInComeTaxReport_";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("FromDate", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("ToDate", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            if (RptName == "RptLoanOverdueReport.rdlc")
            {
                fileName = "RptLoanOverdueReport_";
                ReportParameter rp1 = new ReportParameter("BankName", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BranchName", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("BRCD", BranchID.ToString());
                ReportParameter rp4 = new ReportParameter("ASONDATE", AsOnDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptDeedStock.rdlc" || RptName == "RptDeedStock_Stock.rdlc")
            {
                fileName = "RptDeedStock_";
            }
            else if (RptName == "RptClosingStock.rdlc")
            {
                fileName = "RptClosingStock_";
                ReportParameter rp1 = new ReportParameter("AsOnDate", ENTRYDATE.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1 });
            }
            if (RptName == "RptVendorMaster.rdlc")
            {
                fileName = "RptVendorMaster";
            }
            if (RptName == "RptProductMaster.rdlc")
            {
                fileName = "RptProductMaster";
            }
            else if (RptName == "RptVoucherPrinting.rdlc" || RptName == "RptVoucherPrinting_Eng.rdlc")
            {
                fileName = "RptVoucherPrinting_";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("FromDate", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptVoucherPrintingCRDR.rdlc" || RptName == "RptVoucherPrintingFD.rdlc")
            {
                fileName = "RptVoucherPrintingCRDR_";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("FromDate", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptVoucherPrintRetired.rdlc")
            {
                fileName = "RptVoucherPrintRetired";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("FromDate", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptVoucherPrintingCRDR_Eng.rdlc")
            {
                fileName = "RptVoucherPrintingCRDR_Eng";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("FromDate", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            if (RptName == "RptSharesApplicationList.rdlc")
            {
                fileName = "RptSharesApplicationList_";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("FromDate", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("ToDate", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptDepositReg.rdlc") //-------
            {
                fileName = "DepositDetails ";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptDepositReg_Category.rdlc") //-------
            {
                fileName = "RptDepositReg_Category";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            //SMS REPORT
            else if (RptName == "RptSmsMstReport.rdlc") //-------ANKITA 13/05/2017
            {
                fileName = "SMS REPORT";
                ReportParameter rp1 = new ReportParameter("BankName", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BranchName", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserName", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FDATE", FDATE.ToString());
                ReportParameter rp5 = new ReportParameter("TDATE", TDATE.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }


            else if (RptName == "RptCustDetails.rdlc")  //// Added by ankita on 19/06/2017 to display customer report
            {
                fileName = "SMS REPORT";
                ReportParameter rp1 = new ReportParameter("BankName", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BranchName", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserName", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FDATE", FDATE.ToString());
                ReportParameter rp5 = new ReportParameter("TDATE", TDATE.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }


            else if (RptName == "RptLoanReg.rdlc") //-------
            {
                fileName = "LoanDetails ";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }




            else if (RptName == "RptGLreport.rdlc") //-------
            {
                fileName = "GL-Report";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("user", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });

            }


            else if (RptName == "RptBalanceS.rdlc")
            {
                fileName = "Balance Sheet";
                ReportParameter rp1 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNM", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RGNO", BranchName);
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                //ReportParameter rp5 = new ReportParameter("DRSM", DRBAL.ToString());
                //ReportParameter rp6 = new ReportParameter("CRSM", CRBAL.ToString());
                // RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptPBalanceS.rdlc" || RptName == "RptPBalanceS_Marathi.rdlc" || RptName == "RptPBalanceSarjudas_Marathi.rdlc")
            {
                fileName = "Balance Sheet";
                ReportParameter rp1 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp5 = new ReportParameter("ToDate", TDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNM", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RGNO", BranchName);
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                //ReportParameter rp5 = new ReportParameter("DRSM", DRBAL.ToString());
                //ReportParameter rp6 = new ReportParameter("CRSM", CRBAL.ToString());
                // RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptPNProfitAndLoss.rdlc" || RptName == "RptPNProfitAndLoss_Marathi.rdlc" || RptName == "RptPNProfitAndLossSarjudas_Marathi.rdlc")
            {
                fileName = "RptPNProfitAndLoss";
                ReportParameter rp1 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp5 = new ReportParameter("ToDate", TDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNM", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RGNO", BranchName);
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                //ReportParameter rp5 = new ReportParameter("DRSM", DRBAL.ToString());
                //ReportParameter rp6 = new ReportParameter("CRSM", CRBAL.ToString());
                // RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptIncomeExpReport.rdlc")
            {
                fileName = "RptIncomeExpReport";
                ReportParameter rp1 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp5 = new ReportParameter("ToDate", TDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNM", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RGNO", BranchName);
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptLoanParameter.rdlc")
            {
                fileName = "Loan Parameter";
                ReportParameter rp1 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }

            else if (RptName == "RptTrialBalance.rdlc" || RptName == "RptTrialBalance_M.rdlc")
            {
                fileName = "Trail Balance";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName);
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp5 = new ReportParameter("ToDate", TDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }

                //rakesh15012019
            else if (RptName == "RptStaffMemPassbook.rdlc")
            {
                ReportParameter rp1 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp2 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp5 = new ReportParameter("RegNo", BRName.ToString());
                fileName = "RptStaffMemPassbook";
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }

            //rakesh15012019

            if (RptName == "RptSBIntCalcReport_DT.rdlc" || RptName == "RptSBIntCalcReport_Sumry.rdlc")
            {
                fileName = "RptSBIntCalcReport_DT";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FDate", FDate.ToString());
                ReportParameter rp5 = new ReportParameter("TDate", TDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptTrialBalance_FromTo.rdlc" || RptName == "RptTrialBal_FrmToMarathi.rdlc")
            {
                fileName = "Trail Balance";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName);
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp5 = new ReportParameter("ToDate", TDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptTRAILBALANCE_DPLN.rdlc")
            {
                fileName = "Deposit Loans TB";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName);
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp5 = new ReportParameter("ToDate", TDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptTrialBalanceSummary.rdlc" || RptName == "RptTrialBalanceSummary_M.rdlc")
            {
                fileName = "Trail Balance";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName);
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp5 = new ReportParameter("ToDate", TDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }



            else if (RptName == "RptDayBook.rdlc") //Rakesh 09-12-2016
            {
                fileName = "Day Book";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName);
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp5 = new ReportParameter("CRBAL", DBook.GetOpening(BranchID, "OP", FDT).ToString());
                ReportParameter rp6 = new ReportParameter("DRBAL", DBook.GetOpening(BranchID, "CL", FDT).ToString());
                ReportParameter rp7 = new ReportParameter("CRBankBAL", DBook.GetBankOpening(BranchID, "OP", FDT).ToString());
                ReportParameter rp8 = new ReportParameter("DRBankBAL", DBook.GetBankOpening(BranchID, "CL", FDT).ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8 });
            }
            else if (RptName == "RptDayBook_PEN.rdlc") //Rakesh 09-12-2016
            {
                fileName = "Day Book";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName);
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp5 = new ReportParameter("CRBAL", DBook.GetOpening(BranchID, "OP", FDT).ToString());
                ReportParameter rp6 = new ReportParameter("DRBAL", DBook.GetOpening(BranchID, "CL", FDT).ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }
            else if (RptName == "RptDayBookDetails.rdlc") //Rakesh 09-12-2016
            {
                fileName = "Day Book Details";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName);
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp5 = new ReportParameter("CRBAL", DBook.GetOpening(BranchID, "OP", FDT).ToString());
                ReportParameter rp6 = new ReportParameter("DRBAL", DBook.GetOpening(BranchID, "CL", FDT).ToString());
                ReportParameter rp7 = new ReportParameter("CRBankBAL", DBook.GetBankOpening(BranchID, "OP", FDT).ToString());
                ReportParameter rp8 = new ReportParameter("DRBankBAL", DBook.GetBankOpening(BranchID, "CL", FDT).ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8 });
            }
            else if (RptName == "RptDayBookRegistrerDetailsSetWise.rdlc" || RptName == "RptDayBook_ALLDetails.rdlc") //Rakesh 09-12-2016
            {
                fileName = "RptDayBookRegistrerDetailsSetWise";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName);
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp5 = new ReportParameter("CRBAL", DBook.GetOpening(BranchID, "OP", FDT).ToString());
                ReportParameter rp6 = new ReportParameter("DRBAL", DBook.GetOpening(BranchID, "CL", FDT).ToString());
                ReportParameter rp7 = new ReportParameter("CRBankBAL", DBook.GetBankOpening(BranchID, "OP", FDT).ToString());
                ReportParameter rp8 = new ReportParameter("DRBankBAL", DBook.GetBankOpening(BranchID, "CL", FDT).ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8 });
            }
            else if (RptName == "RptDayBookReg_TZMP.rdlc" || RptName == "RptDayBookReg_FromTo.rdlc") //Rakesh 09-12-2016
            {
                fileName = "RptDayBookReg_TZMP";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName);
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp5 = new ReportParameter("CRBAL", DBook.GetOpening(BranchID, "OP", FDT).ToString());
                ReportParameter rp6 = new ReportParameter("DRBAL", DBook.GetOpening(BranchID, "CL", TDT).ToString());
                ReportParameter rp7 = new ReportParameter("CRBankBAL", DBook.GetBankOpening(BranchID, "OP", FDT).ToString());
                ReportParameter rp8 = new ReportParameter("DRBankBAL", DBook.GetBankOpening(BranchID, "CL", TDT).ToString());
                ReportParameter rp9 = new ReportParameter("ToDate", TDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9 });
            }
            else if (RptName == "RptDayBookReg_Renewal.rdlc" || RptName == "RptDayBookDP_Register.rdlc")
            {
                fileName = "RptDayBookReg_Renewal";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName);
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp5 = new ReportParameter("ToDate", TDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptIWClearngDetails.rdlc") //Rakesh 09-12-2016
            {
                fileName = "Clearing Register Details";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName);
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp5 = new ReportParameter("ToDate", TDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            if (RptName == "RptFDPrint_Chikotra.rdlc")
            {
                fileName = "RptFDPrint_Chikotra";
                ReportParameter rp1 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp2 = new ReportParameter("EDAT", Session["EntryDate"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            }
            else if (RptName == "RptClgRegList.rdlc") //Rakesh 09-12-2016
            {
                fileName = "Clearing Register";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName);
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptClearngRegister.rdlc") //Rakesh 09-12-2016
            {
                fileName = "Clearing Register Report";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName);
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptIWClearngSummary.rdlc") //Rakesh 09-12-2016
            {
                fileName = "Clearing Register Summary";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName);
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp5 = new ReportParameter("ToDate", TDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptCustBalWithSurity.rdlc")
            {
                fileName = "RptCustBalWithSurity";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("UserName", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptCustBalWithoutSurity.rdlc")
            {
                fileName = "RptCustBalWithoutSurity";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("UserName", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "HeadAcListVoucherTRF.rdlc") // OD List
            {
                fileName = "HeadAcListVoucherTRF";
                ReportParameter rp1 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp5 = new ReportParameter("AsOndate", FDT.ToString());
                ReportParameter rp6 = new ReportParameter("Amount", FLT.ToString());
                ReportParameter rp7 = new ReportParameter("Product1", FPRCD.ToString());
                ReportParameter rp8 = new ReportParameter("Product2", TPRCD.ToString());
                ReportParameter rp9 = new ReportParameter("Product3", TBRCD.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9 });
            }
            else if (RptName == "RptClgRegListSummary.rdlc") //Rakesh 09-12-2016
            {
                fileName = "RptClgRegListSummary";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName);
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptClearngReturnRegister.rdlc") //Rakesh 09-12-2016
            {
                fileName = "Return Clearing Register";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName);
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptClassificationDPLN.rdlc") //Rakesh 09-12-2016
            {
                fileName = "RptClassificationDPLN";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName);
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptClassificationDPLNSumy.rdlc") //Rakesh 09-12-2016
            {
                fileName = "RptClassificationDPLN";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName);
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptCTR.rdlc")
            {
                fileName = "Form A";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }
            else if (RptName == "RptFDClassificationList.rdlc")
            {
                fileName = "RptFDClassificationList";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }
            else if (RptName == "RptFormA.rdlc")
            {
                fileName = "CTR REPORT";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FDate", Request.QueryString["FDate"]);
                ReportParameter rp5 = new ReportParameter("TDate", Request.QueryString["TDate"]);
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }


            else if (RptName == "RptFederationLetter.rdlc")//Dhanya Shetty//20-06-2017//For federation letter
            {
                fileName = "Federation Letter";

                ReportParameter rp1 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp2 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp3 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp4 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp5 = new ReportParameter("BANK_NAME", BName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }


            else if (RptName == "RptAccountStatement.rdlc")
            {
                fileName = "Account Statement";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FDate", Request.QueryString["FDate"]);
                ReportParameter rp5 = new ReportParameter("TDate", Request.QueryString["TDate"]);
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            ////////kyc details documnet 
            else if (RptName == "RptKycDoc.rdlc")
            {
                fileName = "KYC Report";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }

            else if (RptName == "RptProfitAndLoss.rdlc")
            {
                fileName = "Profit And Loss";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FDate", Request.QueryString["FDate"]);
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }

            else if (RptName == "RptAdminExp.rdlc")
            {
                fileName = "RptAdminExp";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FDate", FDT.ToString());
                ReportParameter rp5 = new ReportParameter("TDate", TDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }

            else if (RptName == "RptIntimationNotice_Sro.rdlc" || RptName == "RptProtectionNotice_Sro.rdlc" || RptName == "RptPossessionNotice_Sro.rdlc" || RptName == "RptUpsetPrizeNotice_Sro.rdlc" || RptName == "RptUpsetCoverletter_Sro.rdlc" || RptName == "RptPublicLetter_Sro.rdlc" || RptName == "RptSushilLetter_Sro.rdlc" || RptName == "RptTermCondition_Notice.rdlc" || RptName == "RptTenderForm_Notice.rdlc" || RptName == "RptAutionMarathi.rdlc" || RptName == "RptIntimation_Cheque.rdlc" || RptName == "RptIntimation_ToSocity.rdlc" || RptName == "RptExecutionChargesLetter_ToSocity.rdlc" || RptName == "Rpt31Remainder_Notice.rdlc" || RptName == "RptFinalIntimationLetter.rdlc" || RptName == "RptAutionMarathi.rdlc" || RptName == "RPTPublicNotice(E_M).rdlc" || RptName == "RPTproposalforsale.rdlc")
            {

                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("AddressS", lrr.GetAddressSelected(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString()));
                ReportParameter rp11 = new ReportParameter("BANKNAME_MAR", BANKNAME_MAR.ToString());

                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11 });
            }
            else if (RptName == "RptBankRecosile2.rdlc")
            {
                fileName = "Bank Reconsile Report";
                if (RptName == "RptBankRecosile2.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("FROM_DATE", FDT);
                    ReportParameter rp5 = new ReportParameter("TO_DATE", TDT);
                    ReportParameter rp6 = new ReportParameter("PASS", BankReconsile.GetPass(FL, BRCD, FDT, TDT, ProdCode, "1"));
                    ReportParameter rp7 = new ReportParameter("CASH", BankReconsile.GetPass(FL, BRCD, FDT, TDT, ProdCode, "2"));
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7 });
                }
            }
            else if (RptName == "RptOutRegister.rdlc")
            {
                fileName = "Outward Register";
                ReportParameter rp1 = new ReportParameter("FBCode", FBKcode.ToString());
                ReportParameter rp2 = new ReportParameter("TBCode", TBKcode.ToString());
                ReportParameter rp3 = new ReportParameter("FDate", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TDate", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("UserName", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });

            }


            else if (RptName == "RptIntimationNotice_Sroword.rdlc" || RptName == "RptProtectionNotice_Sroword.rdlc" || RptName == "RptPossessionNotice_Sroword.rdlc" || RptName == "RptUpsetPrizeNotice_Sroword.rdlc" || RptName == "RptUpsetCoverletter_Sroword.rdlc" || RptName == "RptPublicLetter_Sroword.rdlc" || RptName == "RptSushilLetter_Sro.rdlc" || RptName == "RptTermCondition_Noticeword.rdlc" || RptName == "RptTenderForm_Notice.rdlc" || RptName == "RptAutionMarathiword.rdlc" || RptName == "RptIntimation_Chequeword.rdlc" || RptName == "RptIntimation_ToSocityword.rdlc" || RptName == "RptExecutionChargesLetter_ToSocityword.rdlc" || RptName == "Rpt31Remainder_Noticeword.rdlc" || RptName == "RptFinalIntimationLetterword.rdlc" || RptName == "RptAutionMarathiword.rdlc" || RptName == "RPTPublicNotice(E_M)word.rdlc" || RptName == "RPTproposalforsaleword.rdlc")
            {

                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("AddressS", lrr.GetAddressSelected(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString()));
                ReportParameter rp11 = new ReportParameter("BANKNAME_MAR", BANKNAME_MAR.ToString());

                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11 });
                if (isExcelDownload == 0)
                {
                    string ExportTo = "Word";
                    string filname = "Notice" + System.DateTime.Now;
                    byte[] bytes = RdlcPrint.LocalReport.Render(ExportTo, null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + filname + "." + extension);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                }


            }

            else if (RptName == "RptIntimationNotice_Sroword.rdlc" || RptName == "RptProtectionNotice_Sroword.rdlc" || RptName == "RptPossessionNotice_Sroword.rdlc" || RptName == "RptSushilLetter_Sro.rdlc")
            {
                fileName = "RptIntimationNotice_Sroword";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("AddressS", lrr.GetAddressSelected(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString()));
                ReportParameter rp11 = new ReportParameter("BANKNAME_MAR", BANKNAME_MAR.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11 });
                if (isExcelDownload == 0)
                {
                    string ExportTo = "Word";
                    string filname = "Notice" + System.DateTime.Now;
                    byte[] bytes = RdlcPrint.LocalReport.Render(ExportTo, null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + filname + "." + extension);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                }
            }
            else if (RptName == "RptExcessCashHold.rdlc")
            {
                fileName = "Excess Cash Holding";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("USER_NAME", UName.ToString());
                ReportParameter rp6 = new ReportParameter("FBRCD", FBRCD.ToString());
                ReportParameter rp7 = new ReportParameter("TBRCD", TBRCD.ToString());
                ReportParameter rp8 = new ReportParameter("ENTRY_DATE", EDT.ToString());
                ReportParameter rp9 = new ReportParameter("REPORT_NAME", "EXCESS CASH HOLDING");

                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9 });

            }
            else if (RptName == "RptAllOK.rdlc")
            {

                fileName = "All Ok Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("REPORT_NAME", "All Ok Report");
                ReportParameter rp4 = new ReportParameter("ASONDATE", AsOnDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("USERNAME", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }
            else if (RptName == "RptCashBook.rdlc" || RptName == "RptCashBook_ALLDetails.rdlc")
            {

                fileName = "RptCashBook";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("REPORT_NAME", "CASH BOOK");
                ReportParameter rp4 = new ReportParameter("FROM_DATE", FDate.ToString());
                ReportParameter rp5 = new ReportParameter("TO_DATE", TDate.ToString());
                ReportParameter rp6 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp7 = new ReportParameter("USER_NAME", UName.ToString());
                ReportParameter rp8 = new ReportParameter("CRBAL", DBook.GetOpening(Session["BRCD"].ToString(), "OP", FDate).ToString());
                ReportParameter rp9 = new ReportParameter("DRBAL", DBook.GetOpening(Session["BRCD"].ToString(), "CL", TDate).ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9 });

            }
            else if (RptName == "RptcashBookSummary.rdlc")
            {

                fileName = "RptcashBookSummary";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("REPORT_NAME", "CASH BOOK");
                ReportParameter rp4 = new ReportParameter("FROM_DATE", FDate.ToString());
                ReportParameter rp5 = new ReportParameter("TO_DATE", TDate.ToString());
                ReportParameter rp6 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp7 = new ReportParameter("USER_NAME", UName.ToString());
                ReportParameter rp8 = new ReportParameter("CRBAL", DBook.GetOpening(Session["BRCD"].ToString(), "OP", FDate).ToString());
                ReportParameter rp9 = new ReportParameter("DRBAL", DBook.GetOpening(Session["BRCD"].ToString(), "CL", FDate).ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9 });

            }
            else if (RptName == "RptAgent.rdlc")
            {

                fileName = "Agent Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("UserId", UName.ToString());

                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });

            }
            else if (RptName == "RptShareCloseAccDetails.rdlc")
            {

                fileName = "RptShareCloseAccDetails Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });

            }
            else if (RptName == "RptUserReportAll.rdlc")
            {

                fileName = "RptUserReportAll";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                // ReportParameter rp6 = new ReportParameter("UserId", UName.ToString());

                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp5 });

            }

            else if (RptName == "Isp_AVS0024_DT.rdlc" || RptName == "RptSRORecSumryList_DT.rdlc" || RptName == "Isp_AVS0024_RecDT.rdlc" || RptName == "RptSRORecSumryList_RecDT.rdlc") // OD List
            {
                fileName = "Isp_AVS0024_DT";
                ReportParameter rp1 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("FBrcd", FBRCD.ToString());
                ReportParameter rp5 = new ReportParameter("TBrcd", TBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("FPrd", FPRCD.ToString());
                ReportParameter rp7 = new ReportParameter("TPrd", TPRCD.ToString());
                ReportParameter rp8 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp9 = new ReportParameter("ToDate", TDT.ToString());
                ReportParameter rp10 = new ReportParameter("Type", PT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10 });
            }

            else if (RptName == "RptCutBook_DepWise.rdlc")
            {
                fileName = "CutBook Report_" + glname;
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp5 = new ReportParameter("GLNAME", glname.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptPostCommision.rdlc")
            {

                fileName = "Agent Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("UserId", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });

            }

            else if (RptName == "RptDailyAgentSlab.rdlc")
            {

                fileName = "RptDailyAgentSlab";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("UserId", UName.ToString());

                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp5, rp6 });

            }
            else if (RptName == "RptCDRatio.rdlc")
            {

                fileName = "RptCDRatio";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", UName.ToString());
                ReportParameter rp5 = new ReportParameter("FPRCD", FPRCD.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });

            }
            else if (RptName == "RptCDR.rdlc" || RptName == "RptCDRSummary.rdlc")
            {

                fileName = "CDR Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("DATE", OnDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("USER_NAME", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp4, rp5, rp6 });
            }
            else if (RptName == "RptProfitAndLoss_Marathi.rdlc" || RptName == "RptProfitAndLossSarjudas_Marathi.rdlc")
            {
                fileName = "Profit And Loss";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FDate", Request.QueryString["FDate"]);
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptShareMaster.rdlc" || RptName == "RptShareMismatchList.rdlc")
            {

                fileName = "RptShareMaster";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("UserId", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });

            }
            else if (RptName == "RptLoanClose.rdlc" || RptName == "RptloanCityWise.rdlc")
            {

                fileName = "RptShareMaster";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp6 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", UName.ToString());
                ReportParameter rp5 = new ReportParameter("FPRCD", FPRCD.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });

            }
            else if (RptName == "RptLoanAmountWise.rdlc")
            {

                fileName = "RptLoanAmountWise";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp6 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", UName.ToString());
                ReportParameter rp5 = new ReportParameter("FPRCD", FPRCD.ToString());
                ReportParameter rp7 = new ReportParameter("TPRCD", FPRCD.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7 });

            }
            else if (RptName == "RptShareNomi.rdlc")
            {

                fileName = "RptShareNomi";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp5 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });

            }
            else if (RptName == "RptDDSToLoan.rdlc")
            {

                fileName = "RptDDSToLoan";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp4 });

            }

            else if (RptName == "RptSharesCert_SHIV.rdlc")
            {

                fileName = "RptShareCerti";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                // ReportParameter rp4 = new ReportParameter("UserId", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }
            else if (RptName == "RptShareBalList.rdlc")
            {

                fileName = "RptShareBalList";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });

            }
            else if (RptName == "RptShareCerti.rdlc" || RptName == "RptShareCerti_Marathwada.rdlc" || RptName == "RptShareCerti_MarathwadaAddshr.rdlc" || RptName == "RptShrYSPM.rdlc" || RptName == "RptShrYSPMAddShr.rdlc" || RptName == "RptShareAjinkyatara.rdlc")
            {

                fileName = "RptShareCerti";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                // ReportParameter rp4 = new ReportParameter("UserId", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }
            else if (RptName == "RptShareCerti_Palghar.rdlc")
            {

                fileName = "RptShareCerti_Palghar";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }
            else if (RptName == "RptShareCerti_ShivSamarth.rdlc")//ankita 15/09/17 shivsamarth share certi
            {

                fileName = "RptShareCerti";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }
            else if (RptName == "Rpt_AVS0003.rdlc")//Dipali Nagare 25-07-2017
            {

                fileName = "Share Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("UserId", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7 });

            }

            else if (RptName == "Rpt_AVS0004.rdlc")//Dipali Nagare 25-07-2017
            {

                fileName = "Share Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("UserId", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7 });

            }

            else if (RptName == "Rpt_AVS0001.rdlc")//Dipali Nagare 25-07-2017
            {

                fileName = "Share Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("UserId", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7 });

            }

            else if (RptName == "RptMonthlyAccStat.rdlc")
            {

                fileName = "RptMonthlyAccStat Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("UserId", UName.ToString());

                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });

            }
            else if (RptName == "RptDebitEntry.rdlc")
            {

                fileName = "RptDebitEntry";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());

                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });

            }

            else if (RptName == "RptDailyCollection.rdlc" || RptName == "RptDailyCollection_A.rdlc")//Dhanya Shetty
            {

                fileName = "Agent Daily collection";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }




            else if (RptName == "RptAVS5027.rdlc")//Dhanya Shetty
            {

                fileName = "Customer Name Modification";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", UName.ToString());
                ReportParameter rp4 = new ReportParameter("FDAT", FDate.ToString());
                ReportParameter rp5 = new ReportParameter("TDAT", TDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }

            else if (RptName == "RptLogDetails.rdlc")//Dhanya Shetty//19/09/2017
            {

                fileName = "Log Details";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", UName.ToString());
                ReportParameter rp4 = new ReportParameter("FDAT", FDate.ToString());
                ReportParameter rp5 = new ReportParameter("TDAT", TDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }



            else if (RptName == "RptAVS5048.rdlc")//Dhanya Shetty for Overdue calculation//04/10/2017
            {

                fileName = "OD calReport";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }

            else if (RptName == "RptduedateInvst.rdlc")//Dhanya Shetty
            {

                fileName = "Due Datewise Investment";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", UName.ToString());
                ReportParameter rp4 = new ReportParameter("FDAT", FDate.ToString());
                ReportParameter rp5 = new ReportParameter("TDAT", TDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }

            else if (RptName == "RptAdharLink.rdlc")
            {

                fileName = "Adharcard Link Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", UName.ToString());
                ReportParameter rp4 = new ReportParameter("FDAT", FDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }

            else if (RptName == "RptAdharLinkDetails.rdlc")
            {

                fileName = "Adharcard Link Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", UName.ToString());
                ReportParameter rp4 = new ReportParameter("FDAT", FDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }



            else if (RptName == "RptStartDateInvst.rdlc")//Dhanya Shetty
            {

                fileName = "Due Datewise Investment";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", UName.ToString());
                ReportParameter rp4 = new ReportParameter("FDAT", FDate.ToString());
                ReportParameter rp5 = new ReportParameter("TDAT", TDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            if (RptName == "RptSB_INTCalcPara.rdlc")
            {
                fileName = "RptSB_INTCalcPara";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("USER_NAME", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }

            else if (RptName == "RptDefaulters.rdlc")//Dhanya Shetty for Defaulters 
            {

                fileName = "Defaulter's Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp4 = new ReportParameter("USER_NAME", UName.ToString());
                ReportParameter rp5 = new ReportParameter("AsOnDate", Date.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }

            else if (RptName == "RptUnverifyentry.rdlc")//Dhanya Shetty for Unverified List 
            {

                fileName = "Unverified List Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp4 = new ReportParameter("USER_NAME", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }

            else if (RptName == "RptInwRegAccWise.rdlc") //Dhanya 12-07-2017
            {
                fileName = "Inward Register Acc Details";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp5 = new ReportParameter("AsOnDate", FDT.ToString());
                ReportParameter rp6 = new ReportParameter("ToDate", TDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }

            else if (RptName == "RptOutwardRegAccWise.rdlc") //Dhanya 13-07-2017
            {
                fileName = "Outward Register Acc Details";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp5 = new ReportParameter("AsOnDate", FDT.ToString());
                ReportParameter rp6 = new ReportParameter("ToDate", TDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }

            else if (RptName == "RptIWReturnPatti.rdlc") //Dhanya 12-07-2017
            {
                fileName = "Inward Return Patti";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp5 = new ReportParameter("AsOnDate", ADT.ToString());
                ReportParameter rp6 = new ReportParameter("ToDate", TDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }

            else if (RptName == "RptOutwardReturnPatti.rdlc") //Dhanya 12-07-2017
            {
                fileName = "Outward Return Patti";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp5 = new ReportParameter("AsOnDate", ADT.ToString());
                ReportParameter rp6 = new ReportParameter("ToDate", TDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }


            else if (RptName == "RptNPARecoveryList.rdlc") // OD List
            {
                fileName = "RptNPARecoveryList";
                ReportParameter rp1 = new ReportParameter("ASONDATE", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("FBrcd", FBRCD.ToString());
                ReportParameter rp5 = new ReportParameter("TBrcd", TBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("FPrd", FPRCD.ToString());
                ReportParameter rp7 = new ReportParameter("TPrd", TPRCD.ToString());
                ReportParameter rp8 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8 });
            }
            else if (RptName == "RptLoanODSummaryList.rdlc") // OD List
            {
                fileName = "RptLoanODSummaryList";
                ReportParameter rp1 = new ReportParameter("ASONDATE", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("FBrcd", FBRCD.ToString());
                ReportParameter rp5 = new ReportParameter("TBrcd", TBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("FPrd", FPRCD.ToString());
                ReportParameter rp7 = new ReportParameter("TPrd", TPRCD.ToString());
                ReportParameter rp8 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8 });
            }
            else if (RptName == "Isp_AVS0023.rdlc") // OD List
            {
                fileName = "RptNPARecoverySumryList";
                ReportParameter rp1 = new ReportParameter("ASONDATE", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("FBrcd", FBRCD.ToString());
                ReportParameter rp5 = new ReportParameter("TBrcd", TBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("FPrd", FPRCD.ToString());
                ReportParameter rp7 = new ReportParameter("TPrd", TPRCD.ToString());
                ReportParameter rp8 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8 });
            }
            else if (RptName == "RptNOC_Certificate.rdlc")
            {
                fileName = "RptNOC_Certificate.rdlc";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());

                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1 });
            }

            else if ((RptName == "RptNPADetails_1.rdlc" || RptName == "RptNPASumry_1.rdlc")) // OD List
            {
                fileName = "RptNPAList";
                ReportParameter rp1 = new ReportParameter("ASONDATE", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("FBrcd", FBRCD.ToString());
                ReportParameter rp5 = new ReportParameter("TBrcd", TBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("FPrd", FPRCD.ToString());
                ReportParameter rp7 = new ReportParameter("TPrd", TPRCD.ToString());
                ReportParameter rp8 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8 });
            }
            else if (RptName == "Isp_AVS0024.rdlc" || RptName == "Isp_AVS0024_Rec.rdlc") // OD List
            {
                fileName = "Isp_AVS0024";
                ReportParameter rp1 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("FBrcd", FBRCD.ToString());
                ReportParameter rp5 = new ReportParameter("TBrcd", TBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("FPrd", FPRCD.ToString());
                ReportParameter rp7 = new ReportParameter("TPrd", TPRCD.ToString());
                ReportParameter rp8 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp9 = new ReportParameter("ToDate", TDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9 });
            }
            else if (RptName == "RptSRORecSumryList.rdlc" || RptName == "RptSRORecSumryList_Rec.rdlc") // OD List
            {
                fileName = "RptSRORecSumryList";
                ReportParameter rp1 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("FBrcd", FBRCD.ToString());
                ReportParameter rp5 = new ReportParameter("TBrcd", TBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("FPrd", FPRCD.ToString());
                ReportParameter rp7 = new ReportParameter("TPrd", TPRCD.ToString());
                ReportParameter rp8 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp9 = new ReportParameter("ToDate", TDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9 });
            }

            //  BranchWise Deposit Report
            if (RptName == "RptBranchWiseDP.rdlc")
            {
                fileName = "RptBranchWiseDP";
                ReportParameter rp1 = new ReportParameter("BankName", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BranchName", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("BrCode", BranchID.ToString());
                ReportParameter rp4 = new ReportParameter("AsOnDate", AsOnDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            if (RptName == "RptDivdentMemWiseList.rdlc" || RptName == "RptDivdentSHRDPList.rdlc")
            {
                fileName = "RptDivdentMemWiseList";
                ReportParameter rp1 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp5 = new ReportParameter("ToDate", TDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }

            //  BranchWise Deposit With Previous Month Report
            if (RptName == "RptBranchWiseDP_PRCR.rdlc")
            {
                fileName = "RptBranchWiseDP_PRCR";
                ReportParameter rp1 = new ReportParameter("BankName", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BranchName", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("BrCode", BranchID.ToString());
                ReportParameter rp4 = new ReportParameter("AsOnDate", AsOnDate.ToString());
                ReportParameter rp5 = new ReportParameter("RGNO", BranchName);
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }

            //  BranchWise Loan Report
            if (RptName == "RptBranchWiseLoanList.rdlc")
            {
                fileName = "RptBranchWiseLoanList";
                ReportParameter rp1 = new ReportParameter("BankName", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BranchName", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("BrCode", BranchID.ToString());
                ReportParameter rp4 = new ReportParameter("AsOnDate", AsOnDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }

            //  BranchWise Loan With Previous Month Report
            //if (RptName == "RptBranchWiseLoanList_PrCr.rdlc")
            //{
            //    fileName = "RptBranchWiseLoanList_PrCr";
            //    ReportParameter rp1 = new ReportParameter("BankName", BName.ToString());
            //    ReportParameter rp2 = new ReportParameter("BranchName", BRName.ToString());
            //    ReportParameter rp3 = new ReportParameter("BrCode", BranchID.ToString());
            //    ReportParameter rp4 = new ReportParameter("AsOnDate", AsOnDate.ToString());
            //    ReportParameter rp5 = new ReportParameter("RGNO", BranchName);
            //    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            //}


            if (RptName == "RptBranchWiseLoanList_PrCr.rdlc" || RptName == "RptBranchWiseLoanDT_PrCr.rdlc")
            {
                fileName = "RptBranchWiseLoanList_PrCr";
                ReportParameter rp1 = new ReportParameter("BankName", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BranchName", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("BrCode", BranchID.ToString());
                ReportParameter rp4 = new ReportParameter("AsOnDate", AsOnDate.ToString());
                ReportParameter rp5 = new ReportParameter("RGNO", BranchName);
                ReportParameter rp6 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }
            else if (RptName == "RptAcctypeWiseDPSumry.rdlc")
            {
                fileName = "RptAcctypeWiseDPSumry";
                ReportParameter rp1 = new ReportParameter("ASONDATE", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp5 = new ReportParameter("FromBrcd", FBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("ToBrcd", TBRCD.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }

            else if (RptName == "Isp_AVS0015.rdlc")
            {
                fileName = "ClassificationOFOverDueList";
                ReportParameter rp1 = new ReportParameter("ASONDATE", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp5 = new ReportParameter("FromBrcd", FBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("ToBrcd", TBRCD.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }
            else if (RptName == "LNODPeriodWiseSumry.rdlc")
            {
                fileName = "LNODPeriodWiseSumry";
                ReportParameter rp1 = new ReportParameter("ASONDATE", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp5 = new ReportParameter("FromBrcd", FBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("ToBrcd", TBRCD.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }

            else if (RptName == "RptIntRateSummaryDPList.rdlc")
            {
                fileName = "RptIntRateSummaryDPList";
                ReportParameter rp1 = new ReportParameter("ASONDATE", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp5 = new ReportParameter("FromBrcd", FBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("ToBrcd", TBRCD.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }
            else if (RptName == "RptIntRateSummaryDPList_DT.rdlc")
            {
                fileName = "RptIntRateSummaryDPList_DT";
                ReportParameter rp1 = new ReportParameter("ASONDATE", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp5 = new ReportParameter("FromBrcd", FBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("ToBrcd", TBRCD.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }
            else if (RptName == "RptIntRateSummaryLoansList.rdlc")
            {
                fileName = "RptIntRateSummaryLoansList";
                ReportParameter rp1 = new ReportParameter("ASONDATE", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp5 = new ReportParameter("FromBrcd", FBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("ToBrcd", TBRCD.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }
            else if (RptName == "RptPLALLBrReport.rdlc")
            {
                fileName = "RptPLALLBrReport";
                ReportParameter rp1 = new ReportParameter("ASONDATE", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp5 = new ReportParameter("FromBrcd", FBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("ToBrcd", TBRCD.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }
            else if (RptName == "RptIntRateWiseDPSumry.rdlc")
            {
                fileName = "RptIntRateWiseDPSumry";
                ReportParameter rp1 = new ReportParameter("ASONDATE", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp5 = new ReportParameter("FromBrcd", FBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("ToBrcd", TBRCD.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }
            else if (RptName == "RptInvBalList.rdlc")////////Ankita Ghadage 31/05/2017 for investment balance list
            {

                fileName = "Investment Balance List";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", UName.ToString());
                ReportParameter rp4 = new ReportParameter("AsOnDate", AsOnDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }

            else if (RptName == "RptCloseInvList.rdlc")////////Ankita Ghadage 01/06/2017 for Closed investment list
            {

                fileName = "Closed Investment List";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", UName.ToString());
                ReportParameter rp4 = new ReportParameter("FDAT", FDate.ToString());
                ReportParameter rp5 = new ReportParameter("TDAT", TDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }



            else if (RptName == "RptShivCheck.rdlc")
            {

                fileName = "Agent Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("UserId", UName.ToString());

                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });

            }
            else if (RptName == "RptCheckAccLimit.rdlc")
            {

                fileName = "RptCheckAccLimit Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("UserId", UName.ToString());

                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });

            }
            else if (RptName == "InvestmentRpt.rdlc")
            {

                fileName = "InvestmentRpt";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("UserId", UName.ToString());

                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });

            }
            else if (RptName == "RptLienMark.rdlc")
            {

                fileName = "Lien Mark Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("UserId", UName.ToString());

                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });

            }

            else if (RptName == "RptBankReconDetails.rdlc" || RptName == "RptBankReconDetails_Clear.rdlc")
            {
                DT = LG.GetBankNameDT(FBC.ToString());
                if (DT.Rows.Count > 0)
                {
                    BrName = DT.Rows[0]["BrName"].ToString();
                }

                ReportParameter rp1 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp2 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp5 = new ReportParameter("RegNo", BrName.ToString());
                ReportParameter rp6 = new ReportParameter("PASS", BankReconsile.GetPass(FL, BRCD, FDT, TDT, ProdCode, "1"));
                ReportParameter rp7 = new ReportParameter("CASH", BankReconsile.GetPass(FL, BRCD, FDT, TDT, ProdCode, "2"));
                fileName = "RptOfficeGLWiseTransDetails";
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7 });
            }


            else if (RptName == "RptLienMarkLientype.rdlc")
            {

                fileName = "Lien Mark Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("UserId", UName.ToString());

                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });

            }
            else if (RptName == "RptCashPostionReport.rdlc") // Cash Postion
            {
                fileName = "RptCashPostionReport";
                ReportParameter rp1 = new ReportParameter("ASONDATE", AsOnDate.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptChairmanReport.rdlc") // Cash Postion
            {
                fileName = "RptChairmanReport";
                ReportParameter rp1 = new ReportParameter("ASONDATE", AsOnDate.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BranchName.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptCashLimit.rdlc") // CASH LIMIT REPORT
            {

                fileName = "RptCashLimit";
                ReportParameter rp1 = new ReportParameter("ASONDATE", AsOnDate.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptClearngMemoList.rdlc") // Cash Postion
            {

                fileName = "RptClearngMemoList";
                ReportParameter rp1 = new ReportParameter("ASONDATE", AsOnDate.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }

            if (RptName == "RptLoansSlabWise.rdlc")
            {
                fileName = "RptLoansSlabWise_";
                ReportParameter rp1 = new ReportParameter("BankName", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BranchName", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("BRCD", BRCD.ToString());
                ReportParameter rp4 = new ReportParameter("ASONDATE", AsOnDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            if (RptName == "RptLoansSlabWiseDT.rdlc")
            {
                fileName = "RptLoansSlabWiseDT_";
                ReportParameter rp1 = new ReportParameter("BankName", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BranchName", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("BRCD", BRCD.ToString());
                ReportParameter rp4 = new ReportParameter("ASONDATE", AsOnDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptNPASelectType_1.rdlc") // OD List
            {
                fileName = "RptNPASelectType_1";
                ReportParameter rp1 = new ReportParameter("ASONDATE", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("FBrcd", FBRCD.ToString());
                ReportParameter rp5 = new ReportParameter("TBrcd", TBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("FPrd", FPRCD.ToString());
                ReportParameter rp7 = new ReportParameter("TPrd", TPRCD.ToString());
                ReportParameter rp8 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8 });
            }
            else if (RptName == "Isp_AVS0029.rdlc") // Cash Postion
            {

                fileName = "Isp_AVS0029";
                ReportParameter rp1 = new ReportParameter("ASONDATE", AsOnDate.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            // Account Balance Register
            else if (RptName == "RptAcBalRegisterReport.rdlc")
            {
                fileName = "RptAcBalRegisterReport";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }
            if (RptName == "RptAVSBMTableReport.rdlc")
            {
                fileName = "RptAVSBMTableReport";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }
            //USER REPORT
            else if (RptName == "RptUserReport.rdlc")
            {
                fileName = "User Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("EDT", Session["EntryDate"].ToString());
                ReportParameter rp4 = new ReportParameter("USERNAME", USERNAME.ToString());
                ReportParameter rp5 = new ReportParameter("FBRCD", FBC.ToString());
                ReportParameter rp6 = new ReportParameter("TBRCD", TBC.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }
            //Account Statement 08-12-2016  
            else if (RptName == "RptAccountStatementReport.rdlc")
            {
                ReportParameter rp1 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp2 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp5 = new ReportParameter("RegNo", BRName.ToString());
                fileName = "RptAccountStatementReport";
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }
            else if (RptName == "RptMemberPassBook.rdlc")
            {
                ReportParameter rp1 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp2 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp5 = new ReportParameter("RegNo", BRName.ToString());
                fileName = "RptAccountStatementReport";
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }
            else if (RptName == "RptMemberPassBook1.rdlc" || RptName == "RptMemberPassBook_DT.rdlc")
            {
                ReportParameter rp1 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp2 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp5 = new ReportParameter("RegNo", BRName.ToString());
                fileName = "RptAccountStatementReport";
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptPassbokkCover.rdlc")
            {
                fileName = "RptPassbokkCover";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp5 = new ReportParameter("RegNo", BRName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            //27/12/2018
            if (RptName == "RptNonMemList.rdlc")
            {
                fileName = "RptNonMemList";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FROMDATE", AsOnDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }


            else if (RptName == "RptGLWiseTransDetails.rdlc")
            {
                ReportParameter rp1 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp2 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp5 = new ReportParameter("RegNo", BRName.ToString());
                fileName = "RptGLWiseTransDetails";
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }

            else if (RptName == "RptBrWiseGLDeatails.rdlc" || RptName == "RptBrWiseGLSummry.rdlc" || RptName == "RptOfficeGLDetails.rdlc")
            {
                ReportParameter rp1 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp2 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp5 = new ReportParameter("RegNo", BRName.ToString());
                fileName = "RptBrWiseGLDeatails";
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }

            else if (RptName == "RptGLWiseTransMonthWise.rdlc")
            {
                ReportParameter rp1 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp2 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp5 = new ReportParameter("RegNo", BRName.ToString());
                fileName = "RptGLWiseTransMonthWise";
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }

            else if (RptName == "RptDormantAcList.rdlc")
            {
                ReportParameter rp1 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp5 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp6 = new ReportParameter("Period", FL.ToString());
                ReportParameter rp7 = new ReportParameter("Amount", Amt.ToString());
                fileName = "RptDormantAcList";
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp3, rp4, rp5, rp6, rp7 });
            }
            else if (RptName == "RptTransSubGlMonthWise.rdlc")
            {
                ReportParameter rp1 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp2 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp5 = new ReportParameter("RegNo", BRName.ToString());
                fileName = "RptTransSubGlMonthWise";
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }


            else if (RptName == "RptTransSummaryMonthWise.rdlc")
            {
                ReportParameter rp1 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp2 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp5 = new ReportParameter("RegNo", BRName.ToString());
                fileName = "RptTransSubGlMonthWise";
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptOfficeGLWiseTransDetails.rdlc")
            {
                DT = LG.GetBankNameDT(FBC.ToString());
                if (DT.Rows.Count > 0)
                {
                    BrName = DT.Rows[0]["BrName"].ToString();
                }

                ReportParameter rp1 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp2 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp5 = new ReportParameter("RegNo", BrName.ToString());
                fileName = "RptOfficeGLWiseTransDetails";
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptGenralLedgerWise_BrAdj.rdlc")
            {
                ReportParameter rp1 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp2 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp5 = new ReportParameter("RegNo", BRName.ToString());
                fileName = "RptOfficeGLWiseTransDetails";
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptOfficeGLWiseTransSumary.rdlc")
            {
                ReportParameter rp1 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp2 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp5 = new ReportParameter("RegNo", BRName.ToString());
                fileName = "RptOfficeGLWiseTransSumary";
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptAdmExpenses_DT.rdlc" || RptName == "RptAdmExpenses_Sumry.rdlc" || RptName == "RptAdmExpenses_Br.rdlc")
            {
                ReportParameter rp1 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp2 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp5 = new ReportParameter("RegNo", BRName.ToString());
                fileName = "RptBrWiseGLDeatails";
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }

            else if (RptName == "RptReceiptPrint_2.rdlc")
            {

                fileName = "Cash Receipt Print";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("SET_NO", SETNO.ToString());
                // ReportParameter rp3 = new ReportParameter("REPORT_NAME", "Receip");
                //ReportParameter rp4 = new ReportParameter("ASONDATE", AsOnDate.ToString());
                ReportParameter rp5 = new ReportParameter("PRINT_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("CHEQUE_NO", "0");
                ReportParameter rp7 = new ReportParameter("CQ_BANK_NAME", "N/A");
                ReportParameter rp8 = new ReportParameter("GSTNO", glrep.getGSTNO("0").ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp5, rp6, rp7, rp8 });
            }
            else if (RptName == "RptReceiptPrint_SHIV.rdlc")
            {

                fileName = "RptReceiptPrint_SHIV";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("SET_NO", SETNO.ToString());
                //ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                //ReportParameter rp4 = new ReportParameter("ASONDATE", AsOnDate.ToString());
                ReportParameter rp5 = new ReportParameter("PRINT_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("CHEQUE_NO", "0");
                ReportParameter rp7 = new ReportParameter("CQ_BANK_NAME", "N/A");
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp5, rp6, rp7 });
            }
            else if (RptName == "RptReceiptPrint_AKYT.rdlc" || RptName == "RptReceiptPrint.rdlc")
            {

                fileName = "Cash Receipt Print";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("SET_NO", SETNO.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                //ReportParameter rp4 = new ReportParameter("ASONDATE", AsOnDate.ToString());
                ReportParameter rp5 = new ReportParameter("PRINT_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("CHEQUE_NO", "0");
                ReportParameter rp7 = new ReportParameter("CQ_BANK_NAME", "N/A");
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp5, rp6, rp7 });
            }
            else if (RptName == "RptReceiptPrintPal.rdlc" || RptName == "RptReceiptPrintsai.rdlc" || RptName == "RptReceiptPrintHSFM.rdlc" || RptName == "RptReceiptPrintHSFM_ShareApp.rdlc" || RptName == "RPTSTATEMENTACCREC.rdlc")//Dhanya Shetty//28/09/2017
            {

                fileName = "Cash Receipt PrintPal";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("SET_NO", SETNO.ToString());
                ReportParameter rp3 = new ReportParameter("ADDRESS1", VA.GetAdd1(Session["BNKCDE"].ToString(), Session["BRCD"].ToString()));
                ReportParameter rp4 = new ReportParameter("ADDRESS2", VA.GetAdd2(Session["BNKCDE"].ToString(), Session["BRCD"].ToString()));
                ReportParameter rp5 = new ReportParameter("PRINT_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("CHEQUE_NO", VA.GetChequeno(Session["BRCD"].ToString(), SETNO, EDT));
                ReportParameter rp7 = new ReportParameter("CQ_BANK_NAME", "N/A");
                ReportParameter rp8 = new ReportParameter("REGISTRATIONNO", VA.RegNo(Session["BNKCDE"].ToString(), Session["BRCD"].ToString()));
                ReportParameter rp9 = new ReportParameter("SCHOOLN", VA.Schoolname(VA.Custno(Session["BRCD"].ToString(), Subg, Acc)));
                ReportParameter rp10 = new ReportParameter("Regno", VA.GetReg(Session["BNKCDE"].ToString(), Session["BRCD"].ToString()));
                ReportParameter rp11 = new ReportParameter("Dateyear", VA.GetDateyear(Session["BNKCDE"].ToString(), Session["BRCD"].ToString()));
                //ReportParameter rp12 = new ReportParameter("Logo", (VA.GetLogo(Session["BNKCDE"].ToString())).Rows[0]["photoImg"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11 });
            }
            //LOAN OVERDUE
            else if (RptName == "RptLoanOverdue.rdlc")
            {
                string RName = "";
                if (SL == "CF")
                    RName = "LOAN BALANCE REPORT (Court File A/C's Only)";
                else if (SL == "NCF")
                    RName = "LOAN BALANCE REPORT (No Court File A/C's )";
                else if (SL == "ALL")
                    RName = "LOAN BALANCE REPORT (All A/C's )";

                fileName = "Loan Balance Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("REPORT_NAME", RName);
                ReportParameter rp4 = new ReportParameter("DATE", OnDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("USER_NAME", UName.ToString());
                ReportParameter rp7 = new ReportParameter("GLNAME", glname.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7 });
            }
            else if (RptName == "RptDayBook_SHIV.rdlc") //Rakesh 09-12-2016
            {
                fileName = "Day Book";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName);
                ReportParameter rp2 = new ReportParameter("RegNo", BranchName);
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp5 = new ReportParameter("CRBAL", DBook.GetOpening(BranchID, "OP", FDT).ToString());
                ReportParameter rp6 = new ReportParameter("DRBAL", DBook.GetOpening(BranchID, "CL", FDT).ToString());
                ReportParameter rp7 = new ReportParameter("CRBankBAL", DBook.GetBankOpening(BranchID, "OP", FDT).ToString());
                ReportParameter rp8 = new ReportParameter("DRBankBAL", DBook.GetBankOpening(BranchID, "CL", FDT).ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8 });
            }
            //Details DDS To Loan
            else if (RptName == "RptDetailsDDSTOLOAN.rdlc")
            {

                fileName = "Details Standing Instruction";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("REPORT_NAME", "Details Standing Instr (DDS to LOAN)");
                ReportParameter rp4 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp5 = new ReportParameter("USER_NAME", UName.ToString());
                ReportParameter rp6 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp7 = new ReportParameter("TDATE", TDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7 });
            }
            else if (RptName == "RptNPASelectType_1.rdlc") // OD List
            {
                fileName = "RptNPASelectType_1";
                ReportParameter rp1 = new ReportParameter("ASONDATE", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("FBrcd", FBRCD.ToString());
                ReportParameter rp5 = new ReportParameter("TBrcd", TBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("FPrd", FPRCD.ToString());
                ReportParameter rp7 = new ReportParameter("TPrd", TPRCD.ToString());
                ReportParameter rp8 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8 });
            }



            else if (RptName == "RptDashLoanOverdue.rdlc")//Dhanya Shetty for dashboard report
            {
                string RName = "";
                if (SL == "CF")
                    RName = "LOAN BALANCE REPORT (Court File A/C's Only)";
                else if (SL == "NCF")
                    RName = "LOAN BALANCE REPORT (No Court File A/C's )";
                else if (SL == "ALL")
                    RName = "LOAN BALANCE REPORT (All A/C's )";

                fileName = "Loan Balance Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("DATE", OnDate.ToString());
                ReportParameter rp4 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp5 = new ReportParameter("USER_NAME", UName.ToString());
                ReportParameter rp6 = new ReportParameter("GLNAME", glname.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });

            }


            else if (RptName == "RptOverDueSummary.rdlc")//Dhanya Shetty for Overdue summary
            {

                fileName = "Over due summary Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("DATE", OnDate.ToString());
                ReportParameter rp4 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp5 = new ReportParameter("USER_NAME", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }

            else if (RptName == "RptMasterListing.rdlc")//Dhanya Shetty  //17-06-2017//MasterListing
            {

                fileName = "Master Listing";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", UName.ToString());
                ReportParameter rp4 = new ReportParameter("BRCD", BRCD.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }

            else if (RptName == "RptLoanOverdue_New.rdlc")
            {
                string RName = "";
                if (SL == "CF")
                    RName = "LOAN BALANCE REPORT (Court File A/C's Only)";
                else if (SL == "NCF")
                    RName = "LOAN BALANCE REPORT (No Court File A/C's )";
                else if (SL == "ALL")
                    RName = "LOAN BALANCE REPORT (All A/C's )";


                fileName = "Loan Overdue Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("REPORT_NAME", RName);
                ReportParameter rp4 = new ReportParameter("DATE", OnDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("USER_NAME", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }
            else if (RptName == "RptMemberPassBook_ALL.rdlc")
            {
                ReportParameter rp1 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp2 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp5 = new ReportParameter("RegNo", BRName.ToString());
                fileName = "RptMemberPassBook_ALL";
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptDeLoanSummery.rdlc" || RptName == "RptDLienDetails.rdlc") //added  by ashok misal
            {
                fileName = "RptDeLoanSummery Report";
                ReportParameter rp1 = new ReportParameter("BankName", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BranchName", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("PrintDate", Session["EntryDate"].ToString());
                ReportParameter rp5 = new ReportParameter("FDate", fdate.ToString());
                ReportParameter rp6 = new ReportParameter("TDate", TDate.ToString());
                ReportParameter rp7 = new ReportParameter("UserId", Session["USERNAME"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp4, rp5, rp6, rp7 });
            }

            else if (RptName == "RptLoanOverdue_Only.rdlc")
            {
                string RName = "";
                if (SL == "CF")
                    RName = "LOAN OVERDUE REPORT (Court File A/C's Only)";
                else if (SL == "NCF")
                    RName = "LOAN OVERDUE REPORT (No Court File A/C's )";
                else if (SL == "ALL")
                    RName = "LOAN OVERDUE REPORT (All A/C's )";

                fileName = "Loan Overdue Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("REPORT_NAME", RName);
                ReportParameter rp4 = new ReportParameter("DATE", OnDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("USER_NAME", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }

            //NPA Report
            else if (RptName == "RptODNpaReport.rdlc")
            {
                fileName = "NPA Report";
                ReportParameter rp1 = new ReportParameter("BankName", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BranchName", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("Rptname", fileName);
                ReportParameter rp4 = new ReportParameter("RptDate", OnDate.ToString());
                ReportParameter rp5 = new ReportParameter("UserName", UName.ToString());
                ReportParameter rp6 = new ReportParameter("PrintDate", Session["EntryDate"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }

            //CDR REPORT
            else if (RptName == "RptCDR.rdlc")
            {

                fileName = "CDR Report";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("DATE", OnDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("USER_NAME", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp4, rp5, rp6 });
            }

            //NPA REPORT
            else if (RptName == "RptNPA1Report.rdlc")
            {
                string RName = "";
                if (SL == "CF")
                    RName = "NON PERFORMING ASSET (Court Files A/C's Only)";
                else if (SL == "NCF")
                    RName = "NON PERFORMING ASSET (No Court Files A/C's )";
                else if (SL == "ALL")
                    RName = "NON PERFORMING ASSET (All A/C's )";

                fileName = "Non Performing Assets";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("REPORT_NAME", RName);
                ReportParameter rp4 = new ReportParameter("DATE", OnDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("USER_NAME", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }

            else if (RptName == "RptNPAReg1.rdlc")
            {
                string RName = "";
                //if (SL == "CF")
                //    RName = "NON PERFORMING ASSET (Court Files A/C's Only)";
                //else if (SL == "NCF")
                //    RName = "NON PERFORMING ASSET (No Court Files A/C's )";
                //else if (SL == "ALL")
                RName = "NON PERFORMING ASSET (All A/C's )";

                fileName = "Non Performing Assets";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("REPORT_NAME", RName);
                ReportParameter rp4 = new ReportParameter("DATE", OnDate.ToString());
                ReportParameter rp5 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp6 = new ReportParameter("USER_NAME", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }


            //SI DDS TO LOAN
            else if (RptName == "RptSI_DDStoLoan.rdlc")
            {
                fileName = "Standing Instruction ";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("REPORT_NAME", "Standing Instruction");
                ReportParameter rp4 = new ReportParameter("FROM_DATE", FDate.ToString());
                ReportParameter rp5 = new ReportParameter("TO_DATE", TDate.ToString());
                ReportParameter rp6 = new ReportParameter("ENTRY_DATE", Session["EntryDate"].ToString());
                ReportParameter rp7 = new ReportParameter("USER_NAME", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7 });
            }

            else if (RptName == "RptInwardReg.rdlc")
            {
                fileName = "Inward Register";
                ReportParameter rp1 = new ReportParameter("FBanKCode", FBKcode.ToString());
                ReportParameter rp2 = new ReportParameter("TBankCode", TBKcode.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("UserName", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptPLExpenses.rdlc")
            {
                fileName = "RptPLExpenses";
                ReportParameter rp1 = new ReportParameter("CLNM", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RGNO", BranchName);
                ReportParameter rp3 = new ReportParameter("FDATE", FDT.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDT.ToString());
                ReportParameter rp5 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptPLExpenses_WithBal.rdlc")
            {
                fileName = "RptPLExpenses_WithBal";
                ReportParameter rp1 = new ReportParameter("CLNM", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RGNO", BranchName);
                ReportParameter rp3 = new ReportParameter("FDATE", FDT.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDT.ToString());
                ReportParameter rp5 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptPLExpenses_SkipData.rdlc")
            {
                fileName = "RptPLExpenses_SkipData";
                ReportParameter rp1 = new ReportParameter("CLNM", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RGNO", BranchName);
                ReportParameter rp3 = new ReportParameter("FDATE", FDT.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDT.ToString());
                ReportParameter rp5 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptPLExpenses_WithMarathi.rdlc")
            {
                fileName = "RptPLExpenses_WithMarathi";
                ReportParameter rp1 = new ReportParameter("CLNM", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RGNO", BranchName);
                ReportParameter rp3 = new ReportParameter("FDATE", FDT.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDT.ToString());
                ReportParameter rp5 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptShrBalRegister.rdlc")
            {
                fileName = "RptShrBalRegister";
                ReportParameter rp1 = new ReportParameter("CLNM", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RGNO", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDT.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDT.ToString());
                ReportParameter rp5 = new ReportParameter("FBRCD", FBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("TBRCD", TBRCD.ToString());
                ReportParameter rp7 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7 });
            }
            else if (RptName == "RptShrBalRegisterSumry.rdlc")
            {
                fileName = "RptShrBalRegisterSumry";
                ReportParameter rp1 = new ReportParameter("CLNM", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RGNO", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDT.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDT.ToString());
                ReportParameter rp5 = new ReportParameter("FBRCD", FBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("TBRCD", TBRCD.ToString());
                ReportParameter rp7 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7 });
            }

            else if (RptName == "RptDivPendingList.rdlc") // OD List
            {
                fileName = "RptDivPendingList";
                ReportParameter rp1 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp5 = new ReportParameter("ToDate", TDT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptShrBalanceCertWise.rdlc")
            {
                fileName = "RptShrBalanceCertWise";
                ReportParameter rp1 = new ReportParameter("CLNM", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RGNO", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDT.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDT.ToString());
                ReportParameter rp5 = new ReportParameter("FBRCD", FBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("TBRCD", TBRCD.ToString());
                ReportParameter rp7 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7 });
            }
            else if (RptName == "RptDDSR.rdlc")// amruta 17/04/2017
            {
                fileName = "RptDDSR_";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserName", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }
            else if (RptName == "RptRisktypeDetails.rdlc")
            {
                fileName = "RptRisktypeDetails_";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserName", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }

            else if (RptName == "RptAddressLabelPrint_TZMP.rdlc")
            {
                fileName = "RptAddressLabelPrint";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            }
            else if (RptName == "RptPreSanLoanAPPList.rdlc" || RptName == "RptSanLoanAPPList.rdlc")
            {
                fileName = "RptPreSanLoanAPPList";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserName", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp5 = new ReportParameter("TDATE", TDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptDivPayTrans.rdlc") // Cash Postion
            {
                fileName = "RptDivPayTrans";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FLAG", FL.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptDividentTRFProcess.rdlc") // Cash Postion
            {
                fileName = "RptDividentTRFProcess";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FDATE", FDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
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
            if (RptName == "RptCustomerBalane.rdlc")
            {
                fileName = "CustomerBalane";
                ReportParameter rp1 = new ReportParameter("BRCD", BRCD.ToString());
                ReportParameter rp2 = new ReportParameter("ASONDATE", AsOnDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            }
            if (RptName == "RptLoanBalanceList.rdlc")
            {
                fileName = "LoanCustomerBalane";
                ReportParameter rp1 = new ReportParameter("BRCD", BRCD.ToString());
                ReportParameter rp2 = new ReportParameter("ASONDATE", AsOnDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            }
            else if (RptName == "RptMobileData.rdlc") // Cash Postion
            {
                fileName = "RptMobileData";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("ASONDATE", AsOnDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptAVS51182.rdlc") // Cash Postion
            {
                fileName = "RptAVS51182";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("ASONDATE", AsOnDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "Rpt_TDWithdrwalVchr.rdlc") // Cash Postion
            {
                fileName = "Rpt_TDWithdrwalVchr";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("ASONDATE", AsOnDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptGLBalanceDataTrf.rdlc")
            {
                fileName = "RptGLBalanceDataTrf";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("FBRCD", FBRCD.ToString());
                ReportParameter rp5 = new ReportParameter("TBRCD", TBRCD.ToString());
                //ReportParameter rp3 = new ReportParameter("UserName", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }

            else if (RptName == "RptDailyBalanceLessThenClg.rdlc")
            {
                fileName = "RptDailyBalanceLessThenClg";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("FBRCD", FBRCD.ToString());
                ReportParameter rp5 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp6 = new ReportParameter("Period", FL.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }
            else if (RptName == "RptCDRatioReport.rdlc" || RptName == "RptCDRatioReport_DT.rdlc")
            {
                fileName = "RptCDRatioReport";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserName", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("AsOndate", FDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            //Amruta 12/04/2017
            else if (RptName == "RptDayClose.rdlc")
            {
                fileName = "RptDayClose_";
                if (RptName == "RptDayClose.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("UserName", UName.ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                }
            }
            else if (RptName == "RptDocumentReg.rdlc")
            {
                fileName = "Document Register";
                ReportParameter rp1 = new ReportParameter("FromDocType", FromDOCType.ToString());
                ReportParameter rp2 = new ReportParameter("ToDocType", ToDOCType.ToString());
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("UserName", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });

            }


            else if (RptName == "RptInvMat.rdlc")
            {
                fileName = "RptInvReg";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UName", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });

            }
            else if (RptName == "RptInv_Reg.rdlc")
            {
                fileName = "RptInv_Reg";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UName", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });

            }
            else if (RptName == "RptInvProd.rdlc")
            {
                fileName = "RptInvProd";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UName", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });

            }
            else if (RptName == "RptInvInterest.rdlc")
            {
                fileName = "RptInvInterest";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UName", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });

            }
            else if (RptName == "RptInvMaturity.rdlc")
            {
                fileName = "RptInvMaturity";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UName", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });

            }
            else if (RptName == "RptMemberPassBookFamer.rdlc")
            {
                fileName = "RptMemberPassBookFamer";
                ReportParameter rp1 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp2 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp5 = new ReportParameter("RegNo", BRName.ToString());
                // fileName = "RptAccountStatementReport";
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            else if (RptName == "RptAVS5115.rdlc")//Dhanya Shetty//04/04/2018
            {
                fileName = "RptAVS5115.rdlc";
                ReportParameter rp1 = new ReportParameter("BranchName", BRName.ToString());
                ReportParameter rp2 = new ReportParameter("BankName", BName.ToString());
                ReportParameter rp3 = new ReportParameter("UserName", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Prd", Prd.ToString());
                ReportParameter rp5 = new ReportParameter("FBrcd", FBrcd.ToString());
                ReportParameter rp6 = new ReportParameter("TBrcd", TBrcd.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }
            else if (RptName == "ISP_AVS0204.rdlc") // OD List
            {
                fileName = "ISP_AVS0204";
                ReportParameter rp1 = new ReportParameter("FromDate", FDT.ToString());
                ReportParameter rp2 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp4 = new ReportParameter("FBrcd", FBRCD.ToString());
                ReportParameter rp5 = new ReportParameter("TBrcd", TBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("FPrd", FPRCD.ToString());
                ReportParameter rp7 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp8 = new ReportParameter("ToDate", TDT.ToString());
                ReportParameter rp9 = new ReportParameter("Rate", FL.ToString());
                ReportParameter rp10 = new ReportParameter("CommRate", FLT.ToString());
                ReportParameter rp11 = new ReportParameter("TDSRate", PT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11 });
            }
            //rakesh15012019
            else if (RptName == "RptCKYCList.rdlc")
            {
                fileName = "RptCKYCList.rdlc";
                ReportParameter rp1 = new ReportParameter("BranchName", BRName.ToString());
                ReportParameter rp2 = new ReportParameter("BankName", BName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FDate", FDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }

            else if (RptName == "RptFDPrinting.rdlc")
            {
                fileName = "FD Printing";


            }
            if (RptName == "RptFDShiv.rdlc")
            {

                fileName = "FD Printing";
                ReportParameter rp1 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1 });
            }
            if (RptName == "RptFDPrintMarathawada.rdlc")
            {

                fileName = "RptFDPrintMarathawada";
                ReportParameter rp1 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1 });
            }



            //28/12/2018 rakesh
            if (RptName == "RptFDBackPrint_Palghar.rdlc" || RptName == "RptFDBackPrint_MSEB.rdlc")
            {
                fileName = "BackPrint_Palghar";
                ReportParameter rp1 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1 });

            }


            if (RptName == "RptFDPrint_MSEB.rdlc")
            {
                fileName = "RptFDPrint";
                ReportParameter rp1 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1 });
            }

            //if (RptName == "RptFDPrintingShivSamarth.rdlc" || RptName == "RptFDPrintAkyt.rdlc")
            //{

            //    fileName = "RptFDPrintingShivSamarth";
            //    ReportParameter rp1 = new ReportParameter("BRANCH_NAME", BRName.ToString());
            //    ReportParameter rp2 = new ReportParameter("BANK_NAME", BName.ToString());
            //    ReportParameter rp3 = new ReportParameter("Edate", Session["ENTRYDATE"].ToString());
            //    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            //}


            if (RptName == "RptFDPrintingShivSamarth.rdlc" || RptName == "RptFDPrintAkyt.rdlc")
            {

                fileName = "RptFDPrintingShivSamarth";
                ReportParameter rp1 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp2 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("Edate", Session["ENTRYDATE"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }


            if (RptName == "RptFDPrintAkyt_N.rdlc")
            {

                fileName = "RptFDPrintingShivSamarth";
                ReportParameter rp1 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp2 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("Edate", Session["ENTRYDATE"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }

            if (RptName == "RptFDPrintAkyt_G.rdlc")
            {

                fileName = "RptFDPrintAkyt_G";
                ReportParameter rp1 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp2 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp3 = new ReportParameter("Edate", Session["ENTRYDATE"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }

            if (RptName == "RptBrWiseDepositLoanList.rdlc")
            {
                fileName = "RptBrWiseDepositLoanList";
                ReportParameter rp1 = new ReportParameter("ASONDATE", AsOnDate.ToString());
                ReportParameter rp2 = new ReportParameter("BANK_NAME", BName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            }
            if (RptName == "Isp_AVS0038.rdlc")
            {
                fileName = "FD Interest Certificate";
                if (RptName == "Isp_AVS0038.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_ID", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("WORK_DATE", Session["EntryDate"].ToString());
                    ReportParameter rp5 = new ReportParameter("FROMDATE", FDate.ToString());
                    ReportParameter rp6 = new ReportParameter("TODATE", TDate.ToString());
                    ReportParameter rp7 = new ReportParameter("RegNo", lrr.GetRegno(Session["BRCD"].ToString()));
                    ReportParameter rp8 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8 });
                }
            }
            if (RptName == "RptTDSDetails.rdlc")
            {
                fileName = "RptTDSDetails";
                ReportParameter rp1 = new ReportParameter("CLNM", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RGNO", BranchName);
                ReportParameter rp3 = new ReportParameter("FDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TDATE", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("UserId", Session["UserName"].ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
            }
            if (RptName == "RptCustMobile.rdlc")
            {
                fileName = "RptCustMobile";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FROMDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("TODATE", TDate.ToString());
                ReportParameter rp5 = new ReportParameter("FBRCD", FBRCD.ToString());
                ReportParameter rp6 = new ReportParameter("TBRCD", TBRCD.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
            }
            if (RptName == "RptAccOpCl.rdlc")////Added by ankita on 13/06/2017 To display account opening and closing details
            {
                fileName = "RptAccOpCl";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FROMDATE", fdate.ToString());
                ReportParameter rp4 = new ReportParameter("TODATE", tdate.ToString());
                ReportParameter rp5 = new ReportParameter("SUBGL", Subgl.ToString());
                ReportParameter rp6 = new ReportParameter("UserName", UName.ToString());
                ReportParameter rp7 = new ReportParameter("Subglname", subglname.ToString());
                ReportParameter rp8 = new ReportParameter("FLT", FLT.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8 });
            }
            if (RptName == "RptLoanNill.rdlc")////Added by ASHOK MISAL
            {
                fileName = "RptLoanNill";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FROMDATE", fdate.ToString());
                ReportParameter rp4 = new ReportParameter("TODATE", tdate.ToString());
                // ReportParameter rp6 = new ReportParameter("UserName", UName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            if (RptName == "RptPLALLBrReport_CRDR.rdlc")
            {
                fileName = "RptPLALLBrReport_CRDR";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FROMDATE", fdate.ToString());
                ReportParameter rp4 = new ReportParameter("TODATE", tdate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            if (RptName == "RptPLALLBrReport_PL.rdlc")
            {
                fileName = "RptPLALLBrReport_PL";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FROMDATE", fdate.ToString());
                ReportParameter rp4 = new ReportParameter("TODATE", tdate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            //else if (RptName == "RptIRegister.rdlc" || RptName == "RptJRegister.rdlc")////Added by ankita on 06/07/2017 to display IJRegister
            else if (RptName == "RptIRegister.rdlc" || RptName == "RptIJReg.rdlc" || RptName == "RptJRegister.rdlc")////Added by ankita on 06/07/2017 to display IJRegister
            {
                if (RptName == "RptJRegister.rdlc")
                {
                    fileName = "JRegister_Print";
                }
                else
                {
                    fileName = "IRegister_Print";
                }
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            }

            //Loan Sanction Report Details


            if (RptName == "RptPhotoSign.rdlc")
            {
                fileName = " RptPhotoSign Report";
                if (RptName == "RptPhotoSign.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_ID", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("WORK_DATE", Session["EntryDate"].ToString());
                    ReportParameter rp5 = new ReportParameter("FDate", FDate.ToString());
                    ReportParameter rp6 = new ReportParameter("TDate", TDate.ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
                }
            }
            if (RptName == "RptPhotoNotscan.rdlc")
            {
                fileName = " RptPhotoNotscan Report";
                if (RptName == "RptPhotoNotscan.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_ID", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("WORK_DATE", Session["EntryDate"].ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
                }
            }
            if (RptName == "RptDividantPayble.rdlc")
            {
                fileName = "RptDividant Payble";
                if (RptName == "RptDividantPayble.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_ID", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("WORK_DATE", Session["EntryDate"].ToString());
                    ReportParameter rp5 = new ReportParameter("FDate", FDATE.ToString());
                    ReportParameter rp6 = new ReportParameter("TDate", TDATE.ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6 });
                }
            }
            if (RptName == "RPTSUMMARYSROM.rdlc" || RptName == "RPTMONTHLYSROREPORT.rdlc")
            {

                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp4 = new ReportParameter("WORK_DATE", Session["EntryDate"].ToString());
                    ReportParameter rp5 = new ReportParameter("FDate", FDATE.ToString());
                    ReportParameter rp6 = new ReportParameter("TDate", TDATE.ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp4, rp5, rp6 });
                }
            }
            //Loan AMOUNT WISE REPORT
            if (RptName == "RptAVS5025.rdlc") //Ankita on 07/08/2017 to display loan amount wise report
            {
                fileName = "Loan Amountwise Report";
                if (RptName == "RptAVS5025.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_ID", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("WORK_DATE", Session["EntryDate"].ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
                }
            }

            if (RptName == "RptAVS5085.rdlc")////Added by ankita on 16/02/2018 gold loan valuation list
            {
                fileName = "Gold Loan valuation List";
                if (RptName == "RptAVS5085.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USER_ID", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("PRDNAME", FPRDTYPE.ToString());
                    ReportParameter rp5 = new ReportParameter("FDATE", FDATE.ToString());
                    ReportParameter rp6 = new ReportParameter("TDATE", TDATE.ToString());
                    ReportParameter rp7 = new ReportParameter("CURRATE", CURRATE.ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7 });
                }
            }
            if (RptName == "RptAuditrail1.rdlc")////Added by ankita on 18/11/2017 CUSTOMER OPEN REPORT
            {
                fileName = "Customer Open Report";
                if (RptName == "RptAuditrail1.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USERNAME", Session["UserName"].ToString());

                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                }
            }
            if (RptName == "RptAuditrail2.rdlc")////Added by ankita on 18/11/2017
            {
                fileName = "A/C WITH NO MOBILENO Report";
                if (RptName == "RptAuditrail2.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USERNAME", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("COUNT", AVS5057.GetCAccWMNo(FBRCD, TBRCD));
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
                }
            }
            if (RptName == "RptAuditrail3.rdlc")////Added by ankita on 18/11/2017
            {
                fileName = "A/C WITH NO Loan Limit Report";
                if (RptName == "RptAuditrail3.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USERNAME", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("COUNTAC", AVS5057.GetCAcLoan(FBRCD, TBRCD));
                    ReportParameter rp5 = new ReportParameter("COUNTLoan", AVS5057.GetCAcLimit(FBRCD, TBRCD));
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5 });
                }
            }

            if (RptName == "RptAuditrail4.rdlc")////Added by ankita on 18/11/2017
            {
                fileName = "A/C WITH NO Depositinfo Report";
                if (RptName == "RptAuditrail4.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USERNAME", Session["UserName"].ToString());
                    ReportParameter rp4 = new ReportParameter("COUNTAC", AVS5057.GetCAcDep(FBRCD, TBRCD));
                    ReportParameter rp5 = new ReportParameter("COUNTDep", AVS5057.GetCAcDep2(FBRCD, TBRCD));
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                }
            }
            else if (RptName == "RptAddressLabelPrint.rdlc")
            {
                fileName = "RptAddressLabelPrint";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            }



            if (RptName == "RptAuditrail5.rdlc")////Added by ankita on 18/11/2017
            {
                fileName = "A/C WITH NO Customer Number Report";
                if (RptName == "RptAuditrail5.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USERNAME", Session["UserName"].ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                }
            }

            if (RptName == "RptAuditrail6.rdlc")////Added by ankita on 18/11/2017
            {
                fileName = "A/C WITH NO Surity Report";
                if (RptName == "RptAuditrail6.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USERNAME", Session["UserName"].ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                }
            }

            if (RptName == "RptAuditrail7.rdlc")////Added by ankita on 18/11/2017
            {
                fileName = "A/C WITH 0 BALANCE Report";
                if (RptName == "RptAuditrail7.rdlc")
                {
                    ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                    ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                    ReportParameter rp3 = new ReportParameter("USERNAME", Session["UserName"].ToString());
                    RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                }
            }

            if (RptName == "RptDepositReg_EMP.rdlc") //-------
            {
                fileName = "DepositDetails ";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("FromDate", FDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            if (RptName == "RptShareTZMP.rdlc" || RptName == "RptSanchitTZMP.rdlc") // OD List
            {
                fileName = "RptShareTZMP";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            }

            if (RptName == "RptAllLnBalListOD.rdlc")
            {
                fileName = "RptAllLnBalListOD";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FROMDATE", AsOnDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            }
            else if (RptName == "RptRecPayCLBal_ALL.rdlc")
            {
                fileName = "RptRecPayCLBal_ALL";
                ReportParameter rp1 = new ReportParameter("BANK_NAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("BRANCH_NAME", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("FROMDATE", FDate.ToString());
                ReportParameter rp4 = new ReportParameter("ToDate", TDate.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4 });
            }
            else if (RptName == "RptDemandNotice_Sro.rdlc" || RptName == "RptBeforeAttchment_Sro.rdlc" || RptName == "RptAttchment_Sro.rdlc" || RptName == "RptVisit_Sro.rdlc" || RptName == "RptSymbolicNotice_Sro.rdlc" || RptName == "RptPropertyNotice_Sro.rdlc" || RptName == "RptAccAttchNotice_Sro.rdlc" || RptName == "RptTermCondition_Notice.rdlc" || RptName == "RptTenderForm_Notice.rdlc" || RptName == "RptPublicLetterNotice_Sro.rdlc")
            {
                fileName = "RptDemandNotice_Sro";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("AddressS", lrr.GetAddressSelected(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString()));
                ReportParameter rp11 = new ReportParameter("BANKNAME_MAR", BANKNAME_MAR.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11 });
            }
            //----------------------------------------word
            else if (RptName == "RptDemandNotice_Sroword.rdlc" || RptName == "RptBeforeAttchment_Sroword.rdlc" || RptName == "RptAttchment_Sroword.rdlc" || RptName == "RptVisit_Sroword.rdlc" || RptName == "RptSymbolicNotice_Sroword.rdlc" || RptName == "RptPropertyNotice_Sroword.rdlc" || RptName == "RptAccAttchNotice_Sroword.rdlc" || RptName == "RptTermCondition_Noticeword.rdlc" || RptName == "RptTenderForm_Noticeword.rdlc" || RptName == "RptPublicLetterNotice_Sroword.rdlc")
            {
                fileName = "RptDemandNotice_Sroword";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("AddressS", lrr.GetAddressSelected(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString()));
                ReportParameter rp11 = new ReportParameter("BANKNAME_MAR", BANKNAME_MAR.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11 });



                if (isExcelDownload == 0)
                {
                    string ExportTo = "Word";
                    string filname = "Notice" + System.DateTime.Now;
                    byte[] bytes = RdlcPrint.LocalReport.Render(ExportTo, null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + filname + "." + extension);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                }
            }
            else if (RptName == "RptSroMonthlyReport.rdlc")
            {
                fileName = "RptSroMonthlyReport";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Session["EntryDate"].ToString());
                //ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                //  ReportParameter rp10 = new ReportParameter("AddressS", lrr.GetAddressSelected(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString()));
                ReportParameter rp11 = new ReportParameter("BANKNAME_MAR", BANKNAME_MAR.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp6, rp7, rp8, rp9, rp11 });
            }
            else if (RptName == "RptIntimationNotice_Sro.rdlc" || RptName == "RptProtectionNotice_Sro.rdlc" || RptName == "RptPossessionNotice_Sro.rdlc" || RptName == "RptSushilLetter_Sro.rdlc")
            {
                fileName = "RptIntimationNotice_Sro";
                ReportParameter rp1 = new ReportParameter("CLNAME", BName.ToString());
                ReportParameter rp2 = new ReportParameter("RegNo", BRName.ToString());
                ReportParameter rp3 = new ReportParameter("UserId", Session["UserName"].ToString());
                ReportParameter rp4 = new ReportParameter("Edate", Edate.ToString());
                ReportParameter rp5 = new ReportParameter("bal", lrr.getclosing(BRCD, Productcode, Accno, Edate));
                ReportParameter rp6 = new ReportParameter("ADDRESS2", ADDRESS2.ToString());
                ReportParameter rp7 = new ReportParameter("REGISTRATIONNO", REGISTRATIONNO.ToString());
                ReportParameter rp8 = new ReportParameter("bankadd", bankadd.ToString());
                ReportParameter rp9 = new ReportParameter("MOBILE", MOBILE.ToString());
                ReportParameter rp10 = new ReportParameter("AddressS", lrr.GetAddressSelected(AddFlag.ToString(), BRCD.ToString(), Productcode.ToString(), Accno.ToString()));
                ReportParameter rp11 = new ReportParameter("BANKNAME_MAR", BANKNAME_MAR.ToString());
                RdlcPrint.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7, rp8, rp9, rp10, rp11 });
            }

            RdlcPrint.LocalReport.DataSources.Clear();
            RdlcPrint.LocalReport.DataSources.Add(DataSource);
            RdlcPrint.LocalReport.DataSources.Add(DataSource1);
            RdlcPrint.LocalReport.Refresh();

            if (isPDFDownload == 1)
            {
                string ExportTo = "PDF";

                byte[] bytes = RdlcPrint.LocalReport.Render(ExportTo, null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName + "." + extension);
                Response.BinaryWrite(bytes);
                Response.Flush();
            }
            if (isExcelDownload == 1)
            {
                string ExportTo = "Excel";

                byte[] bytes = RdlcPrint.LocalReport.Render(ExportTo, null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName + "." + extension);
                Response.BinaryWrite(bytes);
                Response.Flush();
            }
        }
    }

    public DataSet RptRecPayCLBal_ALLDT(string FBrcd, string TBrcd, string FDate, string TDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        try
        {
            dtEmployee1 = CUSTDT.RecPayCLBal_ALLDT(FBrcd, TBrcd, FDate, TDate);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }


    //27/12/2018
    public DataSet RptNonMemList_DT(string BRCD, string FL, string AsOnDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        try
        {
            dtEmployee1 = CUSTDT.RptNonMemList_DT(BRCD, FL, AsOnDate);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }

    public DataSet GetData(string PT, string AC, string BRCD)
    {
        DataSet ds = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = LI.GetInfo(PT, AC, BRCD);
        ds.Tables.Add(dtEmployee);
        return ds;
    }

    //Loan schedule Report
    public DataSet GetLS(string FL, string SBGL, string ACCNO, string BR)
    {
        DataSet ds = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = LSS.GetLoanSchedule(FL, SBGL, ACCNO, BR);
        ds.Tables.Add(dtEmployee);
        return ds;
    }
    public DataSet GetRecords(string FDAT, string EDAT, string SGL, string BRCD, int Amount, string Accno, int trxtype, int activity, string flag)
    {
        DataSet ds = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = Glrpt.GetReport(FDAT, EDAT, SGL, BRCD, Amount, Accno, trxtype, activity, flag);
        ds.Tables.Add(dtEmployee);
        return ds;
    }
    public DataSet GetDigitalBank(string FBRCD, string TBRCD, string ProdCode, string Charges, string PT, string Date)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = clsdigital.getTrailBank(FBRCD, TBRCD, ProdCode, Charges, PT, Date, "1", Session["EntryDate"].ToString(), Session["MID"].ToString());
        ds1.Tables.Add(dtEmployee);
        return ds1;
    }
    //GetStaffBal
    //RptCustWiseBalance
    public DataSet GetStaffBal(string BRCD, string ASON)
    {
        DataSet ds = new DataSet();
        try
        {

            DataTable dtEmployee = new DataTable();
            dtEmployee = SB.GetStaffBalanceReport(BRCD, ASONDT);
            ds.Tables.Add(dtEmployee);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds;
    }


    public DataSet GetCustomerStatement(string BRCD, string custNo)
    {
        DataSet ds = new DataSet();
        FedSub fedSub = new FedSub();
        try
        {

            DataTable statementDetails = new DataTable();
            statementDetails = fedSub.GetCustomerStatement(custNo, BRCD);
            ds.Tables.Add(statementDetails);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds;
    }

    public DataSet GetReceiptData(string id,string MEMTYPE,string MEMNO)
    {
        DataSet ds = new DataSet();
        FedSub fedSub = new FedSub();
        try
        {

            DataTable statementDetails = new DataTable();
            statementDetails = fedSub.GetReceiptDetails(id, MEMTYPE, MEMNO);
            ds.Tables.Add(statementDetails);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds;
    }
    public DataSet GetDeedStock(string FLAG, string BRCD, string VENDORID, string PRODID, string ENTRYDATE)
    {
        DataSet ds = new DataSet();
        try
        {

            DataTable dtEmployee = new DataTable();
            dtEmployee = C73.rptStock(FLAG: FLAG, BRCD: BRCD, VENDORID: VENDORID, PRODID: PRODID, ENTRYDATE: ENTRYDATE);
            ds.Tables.Add(dtEmployee);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds;
    }
    public DataSet GetDeedStockRPT(string FLAG, string BRCD, string VENDORID, string PRODID, string ENTRYDATE)
    {
        DataSet ds = new DataSet();
        try
        {

            DataTable dtEmployee = new DataTable();
            dtEmployee = C73.rptdeedStock(FLAG: FLAG, BRCD: BRCD, VENDORID: VENDORID, PRODID: PRODID, ENTRYDATE: ENTRYDATE);
            ds.Tables.Add(dtEmployee);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds;
    }

    public DataSet GetClosingStock(string FLAG, string BRCD, string VENDORID, string PRODID, string ENTRYDATE)
    {
        DataSet ds = new DataSet();
        try
        {

            DataTable dtEmployee = new DataTable();
            dtEmployee = C73.rptClosingStock(FLAG: FLAG, BRCD: BRCD, VENDORID: VENDORID, PRODID: PRODID, ENTRYDATE: ENTRYDATE);
            ds.Tables.Add(dtEmployee);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds;
    }

    public DataSet GetProductMaster(string FLAG, string VENDORID, string PRODID)
    {
        DataSet ds = new DataSet();
        try
        {

            DataTable dtEmployee = new DataTable();
            dtEmployee = C73.rptProductMaster(FLAG: FLAG, VENDORID: VENDORID, PRODID: PRODID);
            ds.Tables.Add(dtEmployee);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds;
    }

    public DataSet GetVendorMaster(string FLAG, string VENDORID)
    {
        DataSet ds = new DataSet();
        try
        {

            DataTable dtEmployee = new DataTable();
            dtEmployee = C73.rptVendorMaster(FLAG: FLAG, VENDORID: VENDORID);
            ds.Tables.Add(dtEmployee);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds;
    }
    public DataSet GSTMaster(string BRCD)
    {
        DataSet ds = new DataSet();
        try
        {

            DataTable dtEmployee = new DataTable();
            dtEmployee = SB.GetMAster(BRCD);
            ds.Tables.Add(dtEmployee);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds;
    }

    //RptDividendCalc
    public DataSet GetDividendCalc(string BR, string ED, string FD, string TD, string FP, string TP, string FA, string TA, string FL, string RT, string MID)
    {
        DataSet ds = new DataSet();
        try
        {

            DataTable dtEmployee = new DataTable();
            dtEmployee = DC.GetTrailRun("", FL, ED, FD, TD, FP, TP, FA, TA, RT, BR, MID);
            ds.Tables.Add(dtEmployee);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds;
    }

    public DataSet GetAdmExpenses_Br(string FBC, string FDate, string TDate, string FL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = GLT.GetAdmExpensesBr(FBC, FDate, TDate, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetPBalanceSheet(string FL, string BranchID, string FDate, string TDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        ClsBalanceSheet BS = new ClsBalanceSheet();
        dtEmployee1 = BS.PreBalance(FL, BranchID, FDate, TDate, Session["UserName"].ToString());
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetNPADetailsList_1(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string FL, string S1, string S2, string Flag)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = NPAC.GetNPADetailsListDT_1(FBRCD, TBRCD, FPRCD, TPRCD, FACCNO, TACCNO, FDT, FL, S1, S2, Flag);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    //GetIWOWChargesRepo
    public DataSet GetIWOWChargesRepo(string FL, string BC, string ED)
    {
        DataSet ds = new DataSet();
        try
        {

            DataTable dtEmployee = new DataTable();
            dtEmployee = CH.GetIWOWChargesReport(FL, BRCD, EDT);
            ds.Tables.Add(dtEmployee);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds;
    }

    //GetUnpassRep
    public DataSet GetUnpassRep(string FB, string TB, string ASON)
    {
        DataSet ds = new DataSet();
        try
        {

            DataTable dtEmployee = new DataTable();
            dtEmployee = UP.GetUnpassDetails(FB, TB, ASONDT);
            ds.Tables.Add(dtEmployee);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds;
    }
    public DataSet GetDetails(string BRCD, string PRD, string FDAT, string TDAT)
    {
        DataSet ds = new DataSet();
        try
        {
            DataTable dtEmployee = new DataTable();
            dtEmployee = avs.GetDate(BRCD, PRD, FDAT, TDAT);
            ds.Tables.Add(dtEmployee);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds;
    }
    //INW return Details
    public DataSet GetIWOWRDetails(string FL, string SFL, string FB, string TB, string FDT, string TDT)
    {
        DataSet ds = new DataSet();
        try
        {

            DataTable dtEmployee = new DataTable();
            dtEmployee = CRR.GetReturnReg(FL, SFL, FB, TB, FDT, TDT);
            ds.Tables.Add(dtEmployee);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds;
    }


    //GetMonSummary
    public DataSet GetMonSummary(string BR, string GL, string SBGL, string AN)
    {
        DataSet ds = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = DDS.BindCalcuReport(BR, GL, SBGL, AN, Session["EntryDate"].ToString());
        ds.Tables.Add(dtEmployee);
        return ds;
    }
    //FD INT CALCULATION REPORT
    public DataSet GetFDINTCalReport(string FB, string TB, string FPR, string TPR, string FA, string TA, string ASON, string MID, string SKIP)
    {
        DataSet ds = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = FFD.GetReport(FB, TB, FPR, TPR, FA, TA, ASON, SKIP, Session["MID"].ToString());
        ds.Tables.Add(dtEmployee);
        return ds;
    }


    public DataSet GetCustBalWithSurity(string FDate, string BRCD, string FCustNo, string TCustNo, string Div, string Dept)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = Cust.GetCustBalWithSurity_DT(FDate, BRCD, FCustNo, TCustNo, Div, Dept);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet AccountStatement(string fdate, string tdate, string accno, string acctype, string mid, string brcd, string GL, string ForBRCD)
    {
        DataSet ds = new DataSet();
        DataTable dtEmployee = new DataTable();

        string FDT = "";
        string[] dtf;
        dtf = fdate.ToString().Split('/');
        FDT = fdate;
        string[] dTT = tdate.ToString().Split('/');

        dtEmployee = ACST.AccountStatment(dtf[1].ToString(), dTT[1].ToString(), dtf[2].ToString(), dTT[2].ToString(), fdate, tdate, accno, acctype, mid, brcd, GL, ForBRCD);
        ds.Tables.Add(dtEmployee);
        return ds;
    }

    //Cheque Issue Register Report
    public DataSet ChequeIssueReg(string Bcode, string PCode, string AccNo, string AsOnDate, string Flag)
    {
        DataSet ds = new DataSet();
        DataTable dtEmployee = new DataTable();

        dtEmployee = CIR.GetData(Bcode, PCode, AccNo, AsOnDate, Flag);
        ds.Tables.Add(dtEmployee);
        return ds;
    }
    public DataSet ISP_AVS0204_DT(string FBRCD, string TBRCD, string FPRCD, string FDT, string TDT, string FL, string FLT, string PT)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = RD.Get_AVS0204_DT(FBRCD, TBRCD, FPRCD, FDT, TDT, FL, FLT, PT);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetDDSIntData(string FL, string FPRDTYPE, string TPRDTYPE, string CT, string PR)
    {
        DataSet ds = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = DV.GetDDInfo(FL, FPRDTYPE, TPRDTYPE, CT, PR);
        ds.Tables.Add(dtEmployee);
        return ds;
    }
    public DataSet GetSurityRecord(string BRCD, string GL, string AC)
    {
        DataSet ds = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = LC.GetSurityRecordNotice(BRCD, GL, AC, Session["EntryDate"].ToString());
        ds.Tables.Add(dtEmployee);
        return ds;
    }
    public DataSet GetRecIntRec(string FBrcd, string TBrcd, string Prd, string Month, string Year)//Dhanya Shetty//04/04/2018
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = RI.GetRecIntRec(FBrcd, TBrcd, Prd, Month, Year);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetInvDetails(string FL, string BRCD, string FDATE, string TDATE, string EDT, string SGL)
    {
        DataSet ds = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = IC.GetMATInvDetails(FL, BRCD, FDATE, TDATE, EDT, SGL);
        ds.Tables.Add(dtEmployee);
        return ds;
    }
    public DataSet GetAccREgRpt(string FL, string BRCD, string EDT, string SGL, string FLT)
    {
        DataSet ds = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = IC.GetINVReg(FL, BRCD, EDT, SGL, FLT);
        ds.Tables.Add(dtEmployee);
        return ds;
    }
    public DataSet GetAccInvInt(string BRCD, string FSUB, string TSUB, string EDAT)
    {
        DataSet ds = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = IC.GetINVIntReg(BRCD, FSUB, TSUB, EDAT);
        ds.Tables.Add(dtEmployee);
        return ds;
    }
    public DataSet GetMaturity(string FL, string BRCD, string FDATE, string TDATE)
    {
        DataSet ds = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = IC.GetMaturity(BRCD, FDATE, TDATE);
        ds.Tables.Add(dtEmployee);
        return ds;
    }
    //Day Activity view 

    public DataSet GetDayActivityData(string FL, string BRCD, string FDATE, string TDATE, string RBD)
    {
        DataSet ds = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = DAV.GetDayActivityInfo(FL, BRCD, FDATE, TDATE, RBD);
        ds.Tables.Add(dtEmployee);
        return ds;
    }

    //Day Close
    public DataSet GetDayclose(string FL, string BRCD, string FDATE, string TDATE)
    {
        DataSet ds = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = DAV.GetDayCloseInfo(FL, BRCD, FDATE, TDATE);
        ds.Tables.Add(dtEmployee);
        return ds;
    }

    //Cheque Stock Report
    public DataSet ChequeStock(string Bcode, string PCode, string AsOnDate, string Flag)
    {
        DataSet ds = new DataSet();
        DataTable dtEmployee = new DataTable();

        dtEmployee = CSR.GetData(Bcode, PCode, AsOnDate, Flag);
        ds.Tables.Add(dtEmployee);
        return ds;
    }

    //Account Open Close Report
    public DataSet AccountOpenClose(string Bcode, string PCode, string AsOnDate, string Flag)
    {
        DataSet ds = new DataSet();
        DataTable dtEmployee = new DataTable();

        dtEmployee = AOC.GetData(Bcode, PCode, AsOnDate, Flag);
        ds.Tables.Add(dtEmployee);
        return ds;
    }



    //Un Operative Account Report
    public DataSet UnOpAccts(string fbank, string tbank, string PCode, string AsOnDate, string month)
    {
        DataSet ds = new DataSet();
        DataTable dtEmployee = new DataTable();

        dtEmployee = UOA.GetData(fbank, tbank, PCode, AsOnDate, month);
        ds.Tables.Add(dtEmployee);
        return ds;
    }

    public DataSet AddressLabelPrint(string accno, string taccno, string FDate)////Added by ankita on 06/07/2017 to display IJRegister
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = IJ.AddressLabelPrint_DT(accno, taccno, FDate);
        ds1.Tables.Add(dtEmployee);
        return ds1;
    }


    public DataSet getlienmark(string BRCD, string FDT, string TDT, string AGC)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = cm.GetLienmark(BRCD, FDT, TDT, AGC);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet getlienmarkLientype(string BRCD1, string BRCD2, string FDT, string TDT, string Lientype, string AGC)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = cm.GetLienmarktype(BRCD1, BRCD2, FDT, TDT, AGC, Lientype);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet getgoldora(string BRCD1, string BRCD2, string FDT, string TDT, string Lientype, string AGC)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = cm.GetLienmarktype(BRCD1, BRCD2, FDT, TDT, AGC, Lientype);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetLoanDeposR(string FL, string PTP, string Tdate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = LDR.GetReport(FL, PTP, Session["BRCD"].ToString(), Tdate);
        // AR.Getinfo1(FDT, TDT, AGC, BRCD);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetDDSR(string BRCD, string PTP, string TDate) //Amruta 15/04/2017
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = LDR.GetDDSR(PTP, Session["BRCD"].ToString(), TDate);
        // AR.Getinfo1(FDT, TDT, AGC, BRCD);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    // Deposit Maturity


    public DataSet Getsmsrep(string fdate, string tdate, string fbrcd, string tbrcd, string mob)//ankita ghadage 13/05/2017
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DepM.GetSMSInfo(fdate, tdate, fbrcd, tbrcd, mob);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetCustRep(string fdate, string tdate, string fbrcd, string tbrcd) //// Added by ankita on 19/06/2017 to display customer report
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CU.GetCustRept(fdate, tdate, fbrcd, tbrcd);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetCutBookRecSrno(string Fdate, string GL, string SGL, string MID, string BRCD)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CB.CreateCutBookRecSrno(MID, GL, SGL, BRCD, Fdate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetCustUnifi(string fl, string fbrcd, string tbrcd, string fsubgl, string tsubgl)//ankita ghadage 06/06/2017
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CU.GetDataTbl(fl, fbrcd, tbrcd, fsubgl, tsubgl);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet getglrep(string FL, string BRCD)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = glrep.getglreport(FL, BRCD);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetCutBook(string Fdate, string GL, string SGL, string MID, string BRCD)
    {
        //CreateCutBook(string MID, string GLCD, string SUBGLCD, string BRCD, string EDT)
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CB.CreateCutBook(MID, GL, SGL, BRCD, Fdate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetCutBooksa(string Fdate, string GL, string SGL, string MID, string BRCD)
    {
        //CreateCutBook(string MID, string GLCD, string SUBGLCD, string BRCD, string EDT)
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CB.CreateCutBookSaving(MID, GL, SGL, BRCD, Fdate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetCutBook_Palghar(string Fdate, string GL, string SGL, string MID, string BRCD)
    {
        //CreateCutBook(string MID, string GLCD, string SUBGLCD, string BRCD, string EDT)
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CB.CreateCutBook_Pal(MID, GL, SGL, BRCD, Fdate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    //rakesh15012019
    public DataSet GetStaffmemberPassBk(string FDate, string TDate, string PT, string FBC)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = MPD.GetStaffmemberDT(FDate, TDate, PT, FBC);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    // deposit Receipt
    public DataSet GetDepositReceipt(string SGL, string AC)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = TDARCT.ReportDepositReceipt(SGL, AC, Session["BRCD"].ToString());
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetPNPL(string BranchID, string FDate, string TDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = PL.PrePLBalance(BranchID, FDate, TDate, Session["UserName"].ToString());
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetIncomeExpPL(string BranchID, string FDate, string TDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = PL.IncomeExpPL(BranchID, FDate, TDate, Session["UserName"].ToString());
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetBalanceSheet(string FL, string BranchID, string FDate) //Rakesh 09-12-2016
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = BS.Balance(FL, BranchID, FDate, Session["UserName"].ToString());
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetLoanParameter(string brcd)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = LP.GetData(brcd);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    //Profit and Loss
    public DataSet GetProfitAndLoss(string Brcd, string FDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = PL.ProfitAndLoss(BranchID, FDate, Session["UserName"].ToString());
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetAdminExp(string Brcd, string FDate, string TDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = PL.GetAdminExp(BranchID, FDate, TDate.ToString());
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    ///Trail Balance
    public DataSet GetTraiBalance(string FDate, string TDate, string BranchID, string FL, string CN)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = TB.GetInfo(FDate, TDate, BranchID, FL, CN);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetTraiBalance_FT(string FDate, string TDate, string BranchID, string FLT)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = TB.GetInfo_FT(FDate, TDate, BranchID, FLT);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetTraiBalance_DPLN(string FDate, string TDate, string BranchID)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = TB.GetInfo_FTDPLN(FDate, TDate, BranchID);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    ///Day Boook
    ///
    public DataSet GetDayBook(string BranchID, string FL, string FLT, string Fdate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DBook.GetDayInfo(BranchID, FL, FLT, Fdate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetDayBook_Pen(string BranchID, string FL, string FLT, string Fdate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DBook.GetDayInfo_PEN(BranchID, FL, FLT, Fdate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetDayBookDeatils(string BranchID, string FL, string FLT, string Fdate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DBD.GetDayInfoDetails(BranchID, FL, FLT, Fdate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetDayBookDeatilsSetWise(string BranchID, string FL, string FLT, string Fdate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DSET.GetDaySetInfoDetails(BranchID, FL, FLT, Fdate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }


    public DataSet GetDemandRecList_DT(string BRCD, string Month, string Year, string FL, string FLT)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CUSREC.GetDemandRec_DT(BRCD, Month, Year, FL, FLT);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetDemandRec_DT(string BRCD, string Month, string Year, string FL, string FLT)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CUSREC.GetDemandRec_DTREC(BRCD, Month, Year, FL, FLT);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GETDayBookReg_TZMP(string BranchID, string FL, string FLT, string Fdate, string Tdate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DSET.GETDayBookRegDT_TZMP(BranchID, FL, FLT, Fdate, Tdate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GETDayBookReg_FromTo(string BranchID, string FL, string FLT, string Fdate, string Tdate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DSET.GETDayBookReg_FromToDt(BranchID, FL, FLT, Fdate, Tdate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetDayBookReg_Renewal(string BranchID, string Fdate, string Tdate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DSET.GetDayBookReg_RenewalDT(BranchID, Fdate, Tdate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetDayBookDP_Register(string BranchID, string Fdate, string Tdate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DSET.GetDayBookDP_Reg_DT(BranchID, Fdate, Tdate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetCTR(string Fdate, string TDT, string FSGL, string TSGL, string CTRL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CTR.GetCTRTable(FSGL, TSGL, Fdate, TDT, CTRL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetKYC(string BRCD, string KYCTP)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = KYC.GetKycDetail(BRCD, KYCTP);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetOutwardReg(string FD, string TD, string FBC, string TBC, string UID)
    {    //CreateOutward(string FBC,string TBC,string FDT,string TDT, string BRCD,string FL)
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = OIR.CreateOutward(FBC, TBC, FD, TD, BRCD, "OW");
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }
    public DataSet Get_ExcessCash(string FD, string TD, string FBC, string TBC, string BRCD)
    {    //Create_ExcessCash(string FDT, string TDT, string FBRCD, string TBRCD,string BRCD)
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = EC.Create_ExcessCash(FD, TD, FBC, TBC, BRCD);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }

    public DataSet GetAllOK(string ason, string brcd, string UID)
    //CreateAllOK(string Ason,string brcd)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        brcd = Session["BRCD"].ToString();
        dtEmployee1 = AK.CreateAllOK(ason, brcd);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    //Cash Book
    public DataSet GetCashBook(string Fd, string Td)
    //CreateCS(string FD,string TD,string BRCD)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = CC.CreateCB(Fd, Td, BRCD);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetCashBook_ALL(string Fd, string Td)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = CC.CreateCB_ALL(Fd, Td, BRCD);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetCBSumry(string Fd, string Td)
    //CreateCS(string FD,string TD,string BRCD)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = CC.CreateCBSumry(Fd, Td, BRCD);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetDayBook_Shiv(string BranchID, string FL, string FLT, string Fdate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DBook.GetDayInfo_SHIV(BranchID, FL, FLT, Fdate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GETAGENT(string FL, string fd, string td, string pc, string brcd)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = cm.GETAGENT(FL, fd, td, pc, brcd);
        ds1.Tables.Add(dtEmployee1);
        return ds1;


    }
    public DataSet ShareCloseAccDetails(string fd, string td, string brcd, string Edate, string FBRCD, string TBRCD)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = CAD.ShareCloseAccDetails(fd, td, brcd, Edate, FBRCD, TBRCD);
        ds1.Tables.Add(dtEmployee1);
        return ds1;


    }
    public DataSet GetUserrecord(string brcd, string mid)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = cm.GetUserDetails(BRCD, MID);
        ds1.Tables.Add(dtEmployee1);
        return ds1;


    }


    public DataSet GetmemberPassBkFarmer(string FDate, string TDate, string PT, string FBC)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = MPD.GetmemberPassfarmer(FDate, TDate, PT, FBC);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }


    //rakesh15012019
    public DataSet GetCKYCListDT(string BRCD, string FDate, string FACCNO, string TACCNO, string RFlag)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DV.GetCKYCListDTList(BRCD, FDate, FACCNO, TACCNO, RFlag);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }


    public DataSet GetPostRecord(string Fdate, string Tdate, string brcd)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = MA.GetPostData(Fdate, Tdate, brcd);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }

    public DataSet GetDayBook_ALLDetails(string BranchID, string FL, string FLT, string Fdate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DSET.GetDayBook_ALLDetailsDT(BranchID, FL, FLT, Fdate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet DailyAgentSlab(string fd, string pc, string brcd)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = AR.getAgentSlab(fd, pc, brcd);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }
    public DataSet CDRatio(string asondate, string brcd, string flag)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = CCDR.getCDratio(brcd, asondate, flag);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }

    public DataSet LoanInfo(string flag, string brcd, string FDATE, string TDATE)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = CCD.getloanClose(flag, brcd, FDATE, TDATE);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }


    public DataSet LoanAmountWise(string FAMT, string TAMT, string brcd, string FDATE, string TDATE)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = CCD.GetAmountWise(FAMT, TAMT, brcd, FDATE, TDATE);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }
    public DataSet ShareNomini(string brcd, string FDATE, string TDATE)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = CCD.getsharenomini(brcd, FDATE, TDATE);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }
    public DataSet DDSToLoanReport(string glcode, string brcd)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = CCD.GetDDSToLoanDetails(FPRCD, brcd);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }
    public DataSet ShareBalList(string brcd, string FDATE)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = CCD.getShareBalList(brcd, FDATE);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }
    public DataSet ShareCertiPrinting(string brcd, string AccNo, string EntryDate, string MID)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = SC.GetShareCerti(brcd, AccNo, EntryDate, MID);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet ShareCertiPrintingAddshr(string brcd, string AccNo, string cerno, string EntryDate, string MID)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = SC.ShareCertiPrintingAddshr(brcd, AccNo, cerno, EntryDate, MID);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet ShareCertiPrint_Palghar(string brcd, string AccNo)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = SC.GetShareCerti_Palghar(brcd, AccNo);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }




    public DataSet ShareCertiPrinting_ShivSamarth(string brcd, string AccNo)
    {

        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = SC.GetShareCerti_ShivSamarth(brcd, AccNo);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GETShareRegister(string brcd, string fd, string td)//Dipali Nagare 24-07-2017
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = cm.GETShareRegister(brcd, fd, td);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }

    public DataSet GETShareRegisterFund(string brcd, string fd, string td)//Dipali Nagare 28-07-2017
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = cm.GETShareRegisterRefund(brcd, fd, td);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }
    public DataSet GETUserReport(string brcd, string fd, string td)//Dipali Nagare 24-07-2017
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = cm.GETUserReport(brcd, fd, td);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }

    public DataSet MonthlyAccStat(string fd, string td, string pc, string ACCNO, string brcd)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = cm.MonthlyAccStat(fd, td, pc, ACCNO, brcd);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }
    public DataSet RptDebitEntry(string fd, string pc, string brcd)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = cm.getDebitEntry(fd, pc, brcd);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }

    public DataSet GetDailyCollection(string pc, string brcd, string date, string FL)//Dhanya Shetty
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = cm.GetDaily(pc, brcd, date, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }
    public DataSet RptODlIstDivWiseDT_TZMP(string BRCD, string FPRCD, string TPRCD, string AsOnDate, string FL, string FLT)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        try
        {
            dtEmployee1 = CUSTDT.ODlIstDivWiseDT_TZMP(BRCD, FPRCD, TPRCD, AsOnDate, Fl, FLT);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }

    public DataSet GetCustDetails(string brcd, string CustNo, string date)//Dhanya Shetty
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = CUR.GetCustInfo(brcd, CustNo, date);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }
    public DataSet GetODlist_ADD(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string FLT, string REF, string ODA, string ODI, string FSAN, string TSAN, string FDate, string TDate, string S1, string S2, string FL, string Flag, string AC)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = ODL.GetLnODlist_AD(FBRCD, TBRCD, FPRCD, TPRCD, FACCNO, TACCNO, FDT, FLT, REF, ODA, ODI, FSAN, TSAN, FDate, TDate, S1, S2, FL, Flag, AC);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetMCustDetails(string BRCD, string FDate, string TDate)//Dhanya Shetty
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = MC.GetMDetails(BRCD, FDate, TDate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }
    public DataSet GetDoramntDueAcList(string FBC, string FDate, string PT, string FL, string Amt)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = GLMT.GetDoramntDueAcListDetails(FBC, FDate, PT, FL, Amt);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetLogDetails(string BRCD, string ACTVT, string FDate, string TDate)//Dhanya Shetty//19/09/2017
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = LD.GetLDetails(BRCD, ACTVT, FDate, TDate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }
    public DataSet GetODcal(string BRCD, string Edate)//Dhanya Shetty for Overdue calculation//04/10/2017
    {
        DataSet ds1 = new DataSet();
        try
        {

            DataTable dtEmployee1 = new DataTable();
            dtEmployee1 = ODC.GetODcaldetails(BRCD, Edate);
            ds1.Tables.Add(dtEmployee1);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return ds1;

    }
    public DataSet GetDueDateColln(string BRCD, string FDate, string TDate, string EDAT)//Dhanya Shetty
    {
        DataSet ds1 = new DataSet();
        try
        {

            DataTable dtEmployee1 = new DataTable();
            BRCD = Session["BRCD"].ToString();
            dtEmployee1 = ICD.GetDueData(BRCD, FDate, TDate, EDAT);
            ds1.Tables.Add(dtEmployee1);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return ds1;

    }

    public DataSet AdharlinkRpt(string BRCD, string FDate, string EDAT)
    {
        DataSet ds1 = new DataSet();
        try
        {

            DataTable dtEmployee1 = new DataTable();
            BRCD = Session["BRCD"].ToString();
            dtEmployee1 = Adhar.AdharlinkRpt(BRCD, FDate, EDAT);
            ds1.Tables.Add(dtEmployee1);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return ds1;

    }

    public DataSet AdharlinkRptDetails(string BRCD, string FDate, string EDAT, string Flag)//Dhanya Shetty
    {
        DataSet ds1 = new DataSet();
        try
        {

            DataTable dtEmployee1 = new DataTable();
            BRCD = Session["BRCD"].ToString();
            dtEmployee1 = Adhar.AdharlinkRptDetails(BRCD, FDate, EDAT, Flag);
            ds1.Tables.Add(dtEmployee1);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return ds1;

    }

    public DataSet GetIntDetail(string EDAT, string Prd)//Dhanya Shetty//06/10/2017
    {
        DataSet ds1 = new DataSet();
        try
        {

            DataTable dtEmployee1 = new DataTable();
            BRCD = Session["BRCD"].ToString();
            dtEmployee1 = CurrenCls.Display(EDAT, Prd);
            ds1.Tables.Add(dtEmployee1);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return ds1;

    }


    public DataSet GetStartDateColln(string BRCD, string FDate, string TDate)//Dhanya Shetty
    {
        DataSet ds1 = new DataSet();
        try
        {

            DataTable dtEmployee1 = new DataTable();
            BRCD = Session["BRCD"].ToString();
            dtEmployee1 = ICD.GetStartData(BRCD, FDate, TDate);
            ds1.Tables.Add(dtEmployee1);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return ds1;

    }


    public DataSet GetDefaultesDetails(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string Date)//Dhanya Shetty for Defaulters 
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        try
        {
            dtEmployee1 = DF.GetDefault(FBRCD, TBRCD, FPRCD, TPRCD, FACCNO, TACCNO, Date);
            ds1.Tables.Add(dtEmployee1);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }

    public DataSet GetUnverifiedDetails(string BRCD, string Date)    //Dhanya Shetty for Unverified List 
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        try
        {
            dtEmployee1 = UV.GetUnverified(BRCD, Date);
            ds1.Tables.Add(dtEmployee1);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }


    public DataSet GetNPAList(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string S1, string S2, string Flag)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = NPAC.GetNPAAccList(FBRCD, TBRCD, FPRCD, TPRCD, FACCNO, TACCNO, FDT, S1, S2, Flag);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetODSumryFy(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string FL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = NPAC.GetODSumryFyList(FBRCD, TBRCD, FPRCD, TPRCD, FACCNO, TACCNO, FDT, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetNPASummaryList(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string FL, string S1, string S2, string Flag)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = NPAC.GetNPAAccListSumary(FBRCD, TBRCD, FPRCD, TPRCD, FACCNO, TACCNO, FDT, FL, S1, S2, Flag);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    //public DataSet GetNPADetailsList_1(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string FL, string S1, string S2, string Flag)
    //{
    //    DataSet ds1 = new DataSet();
    //    DataTable dtEmployee1 = new DataTable();
    //    dtEmployee1 = NPAC.GetNPADetailsListDT_1(FBRCD, TBRCD, FPRCD, TPRCD, FACCNO, TACCNO, FDT, FL, S1, S2, Flag);
    //    ds1.Tables.Add(dtEmployee1);
    //    return ds1;
    //}
    public DataSet GetNPASumryList_1(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string FL, string S1, string S2, string Flag)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = NPAC.GetNPASumryListDT_1(FBRCD, TBRCD, FPRCD, TPRCD, FACCNO, TACCNO, FDT, FL, S1, S2, Flag);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetSRORecoveryList(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FDT, string TDT, string FL, string FLT)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = NPAC.GetSRORecovery(FBRCD, TBRCD, FPRCD, TPRCD, FDT, TDT, FL, FLT);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetSRORecoverySumry(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FDT, string TDT, string FL, string FLT)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = NPAC.GetSRORecoverySumry(FBRCD, TBRCD, FPRCD, TPRCD, FDT, TDT, FL, FLT);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }



    public DataSet GetClassificationOfODList(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FDT, string FL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DPCL.GetClassiOfODListDT(FBRCD, TBRCD, FPRCD, TPRCD, FDT, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetClssificationODSumryList(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FDT, string FL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DPCL.GetClssificationODSumry_DT(FBRCD, TBRCD, FPRCD, TPRCD, FDT, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetIntRateWiseDPDT(string FBRCD, string TBRCD, string FPRCD, string FDT, string FL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = PLE.GetIntRateWiseDT(FBRCD, TBRCD, FPRCD, FDT, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetIntRateWiseDepositDt(string FBRCD, string TBRCD, string FPRCD, string FDT, string FL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = INTD.GetIntDepositDt(FBRCD, TBRCD, FPRCD, FDT, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetIntRateWiseDepositDt_DT(string FBRCD, string TBRCD, string FPRCD, string FDT, string FL, string Flag, string FLT)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = INTD.GetIntDepositDt_DT(FBRCD, TBRCD, FPRCD, FDT, FL, Flag, FLT);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    //public DataSet GetIntRateWiseLoansDt(string FBRCD, string TBRCD, string FPRCD, string FDT)
    //{
    //    DataSet ds1 = new DataSet();
    //    DataTable dtEmployee1 = new DataTable();
    //    dtEmployee1 = INTD.GetIntLoansitDt (FBRCD, TBRCD, FPRCD, FDT);
    //    ds1.Tables.Add(dtEmployee1);
    //    return ds1;
    //}
    public DataSet GetPLALLBrReport(string FBRCD, string TBRCD, string FDT)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = PLE.GetPLALLBrDetails(FBRCD, TBRCD, FDT);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetIntRateWiseSumry(string FBRCD, string TBRCD, string FPRCD, string FDT, string FL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = PLE.GetIntRateWiseSumry(FBRCD, TBRCD, FPRCD, FDT, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetIWRegAcc(string BranchID, string FBKcode, string FDT, string TDT) //Dhanya 12-07-2017
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = IWD.GetIWAcc(BranchID, FBKcode, FDT, TDT);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetCDRatio_Detail(string FDate, string BRCD)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = Cust.GetCDRatio_Deatils(FDate, BRCD);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetOutwardRegAcc(string BranchID, string FBKcode, string FDT, string TDT) //Dhanya 13-07-2017
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = IWD.GetOutwardAcc(BranchID, FBKcode, FDT, TDT);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetIWRetPatti(string BranchID, string FBKcode, string FDT, string TDT, string ADT) //Dhanya 12-07-2017
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        try
        {
            dtEmployee1 = IWD.GetIWRetP(BranchID, FBKcode, FDT, TDT, ADT);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }


    public DataSet GetOutwardRetPatti(string BranchID, string FBKcode, string FDT, string TDT, string ADT)//Dhanya 12-07-2017
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = IWD.GetOutwardRetP(BranchID, FBKcode, FDT, TDT, ADT);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetBalanceList(string BRCD, string Asondate, string EDAT)////////Ankita Ghadage 31/05/2017 for investment balance list
    {
        DataSet ds1 = new DataSet();
        try
        {
            DataTable dtEmployee1 = new DataTable();
            BRCD = Session["BRCD"].ToString();
            dtEmployee1 = ICD.GetBalList(BRCD, Asondate, EDAT);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return ds1;
    }
    public DataSet GetProfitAndLoss_Marathi(string Brcd, string FDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = PL.ProfitAndLoss_marathi(BranchID, FDate, Session["UserName"].ToString());
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetClosedInvList(string BRCD, string FDate, string TDate, string EDAT)////////Ankita Ghadage 01/06/2017 for Closed investment list
    {
        DataSet ds1 = new DataSet();
        try
        {
            DataTable dtEmployee1 = new DataTable();
            BRCD = Session["BRCD"].ToString();
            dtEmployee1 = ICD.GetClosedList(BRCD, FDate, TDate, EDAT);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return ds1;
    }

    public DataSet GETMULTIAGENT(string FL, string fd, string td, string PCODE1, string PCODE2, string BRCD)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = cm.GETMULTIAGENT(FL, fd, td, PCODE1, PCODE2, BRCD);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }
    public DataSet GETMULTIAGENT1(string fd, string td, string PCODE1, string PCODE2, string BRCD)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = cm.GETMULTIAGENT1(fd, td, PCODE1, PCODE2, BRCD);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }
    public DataSet checkacclimit(string FL, string fd, string td, string PCODE1, string PCODE2, string BRCD)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = cm.checkAccLimit(FL, fd, td, PCODE1, PCODE2, BRCD);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }
    public DataSet getinvestment(string fd, string td, string brcd, string EDATE)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = cm.getinvestmentrpt(fd, td, brcd, EDATE);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }

    //GetPrintreceipt(EDT, BRCD, SETNO)
    public DataSet GetPrintreceipt(string EDT, string brcd, string st, string CNO, string NAME, string FN)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        //brcd = Session["BRCD"].ToString();
        dtEmployee1 = CR.Insert_Receipt(st, EDT, brcd, CNO, NAME, FN);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetPrintreceiptH(string EDT, string brcd, string st, string CNO, string NAME, string FN, string scroll)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        //brcd = Session["BRCD"].ToString();
        dtEmployee1 = CR.Insert_ReceiptH(st, EDT, brcd, CNO, NAME, FN);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetPrintreceiptHSFM(string EDT, string brcd, string st, string CNO, string NAME, string FN, string scroll)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        //brcd = Session["BRCD"].ToString();
        dtEmployee1 = CR.Insert_ReceiptHSFM(st, EDT, brcd, CNO, NAME, FN, scroll);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetStatementACCHSFM(string EDT, string brcd, string st, string CNO, string NAME, string FN)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        //brcd = Session["BRCD"].ToString();
        dtEmployee1 = CR.RECOVERYACCSTATMENTHSFM(st, EDT, brcd, CNO, NAME, FN);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet RptBankRecosile(string FDT, string TDate, string flag, string BRCD, string ProdCode)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = BankReconsile.getdetails(flag, BRCD, FDT, TDT, ProdCode);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetNPASelectTypeList_1(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string FL, string S1, string S2, string Flag, string FLT)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = NPAC.GetNPASelectTypeListDT_1(FBRCD, TBRCD, FPRCD, TPRCD, FACCNO, TACCNO, FDT, FL, S1, S2, Flag, FLT);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetmemberPassBk_ALL(string FDate, string TDate, string PT, string FL, string FBC, string S1, string S2)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = MPD.GetmemberDT_ALL(FDate, TDate, PT, FL, FBC, S1, S2);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    //LOAN OVERDUE
    public DataSet GetLoanOver(string EDT, string BRCD, string OnDate, string Subgl)
    //GetLoanOverdue(string EDT, string BRCD, string DATE, string SUBGL, string ACCNO)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        try
        {
            dtEmployee1 = LC.GetLoanOverdue(EDT, BRCD, OnDate, Subgl, "OD");
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }
    public DataSet GetLoanOverdue_New1(string EDT, string BRCD, string OnDate, string Subgl, string SLF)//Dhanya Shetty for dashboard report
    //GetLoanOverdue(string EDT, string BRCD, string DATE, string SUBGL, string ACCNO)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        try
        {
            dtEmployee1 = LC.GetLoanOverdue_New1(EDT, BRCD, OnDate, Subgl, "OD", SLF);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }
    public DataSet AddressLabelPrint_TZMP(string Brcd, string accno, string taccno, string FDate, string S1, string S2)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = IJ.AddressLabelPrintTZMP_DT(Brcd, accno, taccno, FDate, S1, S2);
        ds1.Tables.Add(dtEmployee);
        return ds1;
    }


    public DataSet GetOverdueDetail(string EDT, string BRCD, string OnDate, string SLF)//Dhanya Shetty for Overdue summary
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        try
        {
            dtEmployee1 = ODS.GetOverdueReport(EDT, BRCD, OnDate, "OD", SLF);
            ds1.Tables.Add(dtEmployee1);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }

    public DataSet DeLoanSummery(string flag, string fdate, string tdate, string fbrcd, string tbrcd)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        try
        {
            dtEmployee1 = LDE.GetLDSummary(flag, fdate, tdate, fbrcd, tbrcd);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }
    //public DataSet ShareCertiPrint_TZMP(string brcd, string AccNo, string EntryDate, string MID) //Added by Akshay 16-08-18
    //{
    //    DataSet ds1 = new DataSet();
    //    DataTable dtEmployee1 = new DataTable();
    //    BRCD = Session["BRCD"].ToString();
    //    dtEmployee1 = SC.GetShareCerti_TZMP(brcd, AccNo, conn.ConvertDate(EntryDate), MID);
    //    ds1.Tables.Add(dtEmployee1);
    //    return ds1;
    //}
    public DataSet SanchitCertiPrint_TZMP(string BRCD, string AccNo, string EntryDate, string MID) //Added by Akshay 16-08-18
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = SanCerti.GetSanchitCerti_TZMP(BRCD, AccNo, conn.ConvertDate(EntryDate), MID);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }




    public DataSet GetLoanOver_New(string EDT, string FB, string TB, string OnDate, string FS, string TS, string SLF)
    //GetLoanOverdue(string EDT, string BRCD, string DATE, string SUBGL, string ACCNO)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        try
        {
            dtEmployee1 = LC.GetLoanOverdue_New(EDT, FB, TB, OnDate, FS, TS, "OD", SLF);
            ds1.Tables.Add(dtEmployee1);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }

    public DataSet GetMasterListing(string BRCD, string ProdCode, string AccNo, string status)//Dhanya Shetty //17-06-2017//MasterListing
    {
        DataSet ds1 = new DataSet();
        try
        {

            DataTable dtEmployee1 = new DataTable();
            BRCD = Session["BRCD"].ToString();
            dtEmployee1 = DS.GetFurnitureListing(BRCD, ProdCode, AccNo, Status);
            ds1.Tables.Add(dtEmployee1);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return ds1;

    }

    //public DataSet GetLoanOverdue_New1(string EDT, string BRCD, string OnDate, string Subgl, string SLF)
    ////GetLoanOverdue(string EDT, string BRCD, string DATE, string SUBGL, string ACCNO)
    //{
    //    DataSet ds1 = new DataSet();
    //    DataTable dtEmployee1 = new DataTable();
    //    try
    //    {
    //        dtEmployee1 = LC.GetLoanOverdue_New1(EDT, BRCD, OnDate, Subgl, "All", SLF);
    //        ds1.Tables.Add(dtEmployee1);

    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //    return ds1;
    //}
    //CDR REPORT GetCDRReport
    public DataSet GetCDRReport(string BRCD, string OnDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        try
        {
            dtEmployee1 = CDR.GetCDR(BRCD, OnDate);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }

    public DataSet GetCDRSUMReport(string BRCD, string OnDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        try
        {
            dtEmployee1 = CDR.GetCDRSUm(BRCD, OnDate);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }


    public DataSet GetLoanOver_Only(string EDT, string BRCD, string OnDate, string FS, string TS, string SLF)
    //GetLoanOverdue(string EDT, string BRCD, string DATE, string SUBGL, string ACCNO)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        try
        {
            dtEmployee1 = LC.GetLoanOverdue_Only(EDT, BRCD, OnDate, FS, TS, "OD", SLF);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }

    public DataSet GetLoanNPAReport(string BrCode, string SubGlCode, string AsOndate, string Flag)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        try
        {
            dtEmployee1 = LC.GetLoanNPAReport(BrCode, SubGlCode, AsOndate, Flag);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }

    //NPAReg1 16-06-2017 _abhishek
    public DataSet GetNPAReg1(string BrCode, string FSubGlCode, string TSubGlCode, string AsOndate, string Flag, string SFL1, string SFL2)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        try
        {
            dtEmployee1 = NPA.GetNPAReg1(Session["Entrydate"].ToString(), BrCode, AsOndate, FSubGlCode, TSubGlCode, Flag, SFL1, SFL2);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }

    //NPA1 Report
    //public DataSet GetNPA1(string EDT, string BRCD, string OnDate, string Subgl,string SLF)
    //{
    //    DataSet ds1 = new DataSet();
    //    DataTable dtEmployee1 = new DataTable();
    //    try
    //    {
    //        dtEmployee1 = NPA.GetNPAReg1(EDT, BRCD, OnDate, Subgl, "NPA", SLF, "");
    //        ds1.Tables.Add(dtEmployee1);
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //    return ds1;
    //}

    //SI DDS TO LOAN
    public DataSet GetSIDDStoLoan(string FL, string status, string brcd, string FDT, string TDT)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = SI.CreateSIDDStoLoan(FL, status, brcd, FDT, TDT);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    //GetDetailsDDSTOLOAN
    public DataSet GetDetailsDDSTOLOAN(string FL, string brcd, string FDT, string TDT)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = DDSL.GetSI_Report1(brcd, FDT, TDT, Session["MID"].ToString(), FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetInwardReg(string FD, string TD, string FBC, string TBC, string UID)
    {    //CreateOutward(string FBC,string TBC,string FDT,string TDT, string BRCD,string FL)
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = OIR.CreateOutward(FBC, TBC, FD, TD, BRCD, "IN");
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }

    public DataSet GetDocumentReg(string FD, string TD, string FDocT, string TDocT, string UID)
    {//CreateDocReg(string FDU, string TDU, string FDT, string TDT, string BRCD)
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DR.CreateDocReg(FD, TD, FDocT, TDocT, BRCD);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetFD(string BRCD, string SGL, string accno, string FLL)
    {//CreateDocReg(string FDU, string TDU, string FDT, string TDT, string BRCD)
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DR.FDRecipt(BRCD, SGL, accno, Session["MID"].ToString(), Session["EntryDate"].ToString(), FLL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }


    public DataSet GetFD_PALGHAR(string BRCD, string SGL, string accno, string FLL)
    {//CreateDocReg(string FDU, string TDU, string FDT, string TDT, string BRCD)
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DR.FDPrint_Palghar(BRCD, SGL, accno, Session["MID"].ToString(), Session["EntryDate"].ToString(), FLL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetFDInfo(string BRCD, string SGL, string accno, string FLL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DR.FDShivsamarth(BRCD, SGL, accno, Session["MID"].ToString(), Session["EntryDate"].ToString(), FLL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }


    public DataSet GetPLExepenses(string BranchID, string FDT, string TDT)
    {
        DataSet ds1 = new DataSet();
        try
        {
            DataTable dtEmployee1 = new DataTable();
            dtEmployee1 = PLE.GetPLExepensesDT(BranchID, FDT, TDT);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return ds1;
    }
    public DataSet GetPLExepenses_Bal(string BranchID, string FDT, string TDT)
    {
        DataSet ds1 = new DataSet();
        try
        {
            DataTable dtEmployee1 = new DataTable();
            dtEmployee1 = PLE.GetPLExepensesDT_Bal(BranchID, FDT, TDT);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return ds1;
    }
    public DataSet GetPLExepenses_SkipData(string BranchID, string FDT, string TDT)
    {
        DataSet ds1 = new DataSet();
        try
        {
            DataTable dtEmployee1 = new DataTable();
            dtEmployee1 = PLE.GetPLExepensesDT_SkipData(BranchID, FDT, TDT);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return ds1;
    }
    public DataSet GetPLExepenses_BalMarathi(string BranchID, string FDT, string TDT)
    {
        DataSet ds1 = new DataSet();
        try
        {
            DataTable dtEmployee1 = new DataTable();
            dtEmployee1 = PLE.GetPLExepensesDT_BalMarathi(BranchID, FDT, TDT);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return ds1;
    }
    public DataSet GetShrBalRegister(string FBRCD, string TBRCD, string FDT, string TDT)
    {
        DataSet ds1 = new DataSet();
        try
        {
            DataTable dtEmployee1 = new DataTable();
            dtEmployee1 = PLE.GetShrBalRegisterDetails(FBRCD, TBRCD, FDT, TDT);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return ds1;
    }
    public DataSet GetHeadAcListVoucherTRF(string FBRCD, string FPRCD, string TPRCD, string TBRCD, string FDT, string FACCNO, string TACCNO, string FLT, string FL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = NPAC.GetHeadAcListDT(FBRCD, FPRCD, TPRCD, TBRCD, FDT, FACCNO, TACCNO, FLT, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetShrBalRegistersumry(string FBRCD, string TBRCD, string FDT, string TDT)
    {
        DataSet ds1 = new DataSet();
        try
        {
            DataTable dtEmployee1 = new DataTable();
            dtEmployee1 = PLE.GetShrBalRegisterSumry(FBRCD, TBRCD, FDT, TDT);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return ds1;
    }
    public DataSet GetShrBalCertWise(string FBRCD, string TBRCD, string FDT, string TDT)
    {
        DataSet ds1 = new DataSet();
        try
        {
            DataTable dtEmployee1 = new DataTable();
            dtEmployee1 = PLE.GetShrBalCertDT(FBRCD, TBRCD, FDT, TDT);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return ds1;
    }
    //Saving passbook
    public DataSet GetSavPassbook(string brcd, string FDate, string TDate, string PrCode, string AcNo, string SR)
    {
        string FDT = "";
        string[] DTF;

        DTF = FDate.ToString().Split('/');
        FDT = FDate;
        string[] DTT = TDate.ToString().Split('/');

        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = Pass.SavingPassbook(brcd, DTF[1].ToString(), DTT[1].ToString(), FDT, TDate.Trim(), AcNo, PrCode, SR);
        ds1.Tables.Add(dtEmployee1);
        // Add For Blank Line
        if (dtEmployee1.Rows.Count > 0)
        {
            for (int i = 0; i < Convert.ToInt32(BlankLine); i++)
            {
                DataRow dr = dtEmployee1.NewRow();
                dtEmployee1.Rows.InsertAt(dr, i);
            }
        }
        return ds1;

    }

    public DataSet GetCoverPassbook(string brcd, string FDate, string TDate, string PrCode, string AcNo, string SR)
    {
        string FDT = "";
        string[] DTF;

        DTF = FDate.ToString().Split('/');
        FDT = FDate;
        string[] DTT = TDate.ToString().Split('/');

        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = Pass.SavingPassbook(brcd, DTF[1].ToString(), DTT[1].ToString(), FDT, TDate.Trim(), AcNo, PrCode, SR);
        ds1.Tables.Add(dtEmployee1);
        // Add For Blank Line

        return ds1;

    }

    public DataSet GetCashPos(string FBC, string TBC, string FD)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CD.CashDenom(FBC, TBC, FD);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    //GetCashPostion
    public DataSet GetCashPostion(String BRCD, string ason)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CPL.CashPostionList(BRCD, ason);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet ShareCertiAjinkya(string brcd, string AccNo)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = SC.GetShareCertiAjinkya(brcd, AccNo);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetChairman(string BranchID, string ason)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CPL.ChairmanDT(BranchID, ason);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    //get cashlimit report --ankita 04/05/2017
    public DataSet GetCashLimit(string ason, string BRCD, string SUBGL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CLM.CashLimitReport(ason, BRCD, SUBGL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    //GetTopDeposit List Rakesh
    public DataSet GetTopDepositList(string FBC, string GL, string FDate, string FD)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DL.DepositList(FBC, GL, FDate, FD);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetDailyCollection_A(string pc, string brcd, string date)//Dhanya Shetty
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = cm.GetDaily_A(pc, brcd, date);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    //USER REPORT
    public DataSet GetUR(string FBC, string TBC, string FLG, string flag)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = UR.GetUserRpt(TBC, FBC, FLG, flag);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }




    //GetTop Loan List Rakesh
    public DataSet GetTopLoanList(string FBC, string GL, string FDate, string FD)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DLn.LoanList(FBC, GL, FDate, FD);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    //GetAcBalReg Rakesh
    public DataSet GetAcBalReg(string BRCD, string GL, string FD, string ason)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = BRC.GetBalReg(BRCD, GL, FD, ason);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    //Account Statement 08-12-2016 Rakesh  
    public DataSet GetAccStm(string FDate, string TDate, string PT, string AC, string FBC)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = AS.GetAccStmReg(FDate, TDate, PT, AC, FBC);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetNOCPermit(string BRCD, string LOANGL, string ACCNO, string Edate, string FDate, string TDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = ls.RptNOCPermit(BRCD, PRDCD, ACCNO, SRNO);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetGLTransD(string FBC, string PT, string FDate, string TDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = GLT.GetGLTransDReg(FBC, PT, FDate, TDate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetBrWiseGl(string FBC, string PT, string FDate, string TDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = GLT.GetBrWiseGlDetails(FBC, PT, FDate, TDate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetGLTransMonth(string FBC, string PT, string FDate, string TDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = GLMT.GetGlMonthWise(FBC, PT, FDate, TDate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetDoramntAcList(string FBC, string FDate, string PT, string FL, string Amt)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = GLMT.GetDoramntAcListDetails(FBC, FDate, PT, FL, Amt);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetOffGLTransSumry(string FBC, string PT, string FDate, string TDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = OGLT.GetOffGLTransDRegSumry(FBC, PT, FDate, TDate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    //GetOffGLTransD
    public DataSet GetCutBookDetail(string BRCD, string SGL, string FDT, string TDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CBD.CutBookDs(BRCD, SGL, FDT, TDate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }







    public DataSet RptAVS5077m(string BRCD, string productcode, string accno, string Edate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = LMC.RptLoanAmtCer(BRCD, Productcode, accno, Edate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }



    public DataSet RptSavingCerti(string BRCD, string productcode, string accno, string Edate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = LMC.RptSavingCerti(BRCD, Productcode, accno, Edate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }


    public DataSet GetLoanCert111(string BRCD, string LOANGL, string ACCNO, string Edate, string FDate, string TDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = lrr.RptLoanIntcert11(BRCD, LOANGL, ACCNO, Edate, FDate, TDate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetLoanClosure(string BRCD, string LOANGL, string ACCNO)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = lrr.RptLoanClosecert(BRCD, LOANGL, ACCNO);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet ShareRegister(string FBRCD, string TBRCD, string FDate, string TDate, string Edate)//Dhanya Shetty ShareRegister.rdlc 30/12/2017
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = cm.GETSHAREREGISTR(FBRCD, TBRCD, FDate, TDate, Edate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }

    public DataSet StockReport(string BRCD, string FDate, string TDate, string Edate)//Dhanya Shetty //09-03-2018//For StockReport
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        BRCD = Session["BRCD"].ToString();
        dtEmployee1 = PM.GetStockReport(BRCD, FDate, TDate, Edate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;

    }




    public DataSet RptVisitnotice(string BRCD, string productcode, string accno, string Edate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = lrr.RptVisitnotice(BRCD, Productcode, accno, Edate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }


    public DataSet GetBalanceSheet_Marathi(string FL, string BranchID, string FDate) //Rakesh 09-12-2016
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = BS.Balance_Marathi(FL, BranchID, FDate, Session["UserName"].ToString());
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet RptLoanintcert(string BRCD, string productcode, string accno, string Edate, string Fdate, string Tdate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = lrr.RptLoanIntcert("", BRCD, Productcode, accno, Edate, Fdate, Tdate, Session["EntryDate"].ToString(), FL);
        if (dtEmployee1 == null)
            LRPC.updatelog("", OUTNO);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetActiveMem(string BRCD, string FDT, string TDate, string Edate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CBD.ActivememReport(BRCD, FDT, TDate, Edate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetFormA(string FSGL, string TSGL, string ason, string Fdate, string TDT, string CTRL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = FA.GetCTRTable(FSGL, TSGL, ason, Fdate, TDT, CTRL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetSubBook(string BranchID, string Fdate, string Tdate, string SGL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = VSB.GetVoucherSubBook(BranchID, Fdate, Tdate, SGL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetSubBookSum(string BranchID, string Fdate, string Tdate, string SGL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = VSB.GetVcrSubBookSum(BranchID, Fdate, Tdate, SGL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetVoucherPrint(string BranchID, string Fdate, string SGL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = VSB.GetVoucherPrintDts(BranchID, Fdate, SGL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetITaxReport(string BranchID, string Fdate, string Tdate, string SGL, string FL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = ITax.GetITaxDT(BranchID, Fdate, Tdate, SGL, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetITaxReport_SHR(string BranchID, string Fdate, string Tdate, string SGL, string FL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = ITax.GetITaxDT_SHR(BranchID, Fdate, Tdate, SGL, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetITaxReport_DP(string BranchID, string Fdate, string Tdate, string SGL, string FL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = ITax.GetITaxDT_DP(BranchID, Fdate, Tdate, SGL, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetITaxReport_LN(string BranchID, string Fdate, string Tdate, string SGL, string FL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = ITax.GetITaxDT_LN(BranchID, Fdate, Tdate, SGL, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetLoanOverdueMIS(string BranchID, string FL, string AsOnDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = ITax.GetLoanOverdueMIS_Details(BranchID, FL, AsOnDate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetVoucherPrintCRDR(string BranchID, string Fdate, string SGL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = VSB.GetVoucherCrDr(BranchID, Fdate, SGL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetVoucherPrintFD(string BranchID, string Fdate, string SGL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = VSB.GetVoucherFD(BranchID, Fdate, SGL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetVoucherPrintRetire(string BranchID, string Fdate, string SGL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = VSB.GetVoucherRetire(BranchID, Fdate, SGL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }



    public DataSet GetAccnoCount(string BRCD, string GLCODE)
    {
        DataSet ds = new DataSet();
        DataTable dtaccnt = new DataTable();
        dtaccnt = CAC.GetCount(BRCD, GLCODE);
        ds.Tables.Add(dtaccnt);
        return ds;
    }


    public DataSet GetCustGrpdetails(string FDate, string CustNo)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = Cust.GetCustGrpDT(FDate, CustNo);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetCDRatioDT(string FDate, string BRCD)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = Cust.GetCDRatioDT_Deatils(FDate, BRCD);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetGLBALDataDt(string FDate, string FBRCD, string TBRCD)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = Cust.GetGLBALdetails(FDate, FBRCD, TBRCD);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }


    public DataSet GetDailyLessClgBal(string FDate, string FBRCD, string Prd, string FL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = Cust.GetDailyLessClgBalDetails(FDate, FBRCD, Prd, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetRiskdetails(string FL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = RT.GetRiskType(FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetFDRClass(string Fdate, string TDT, string FSGL, string TSGL, string FBC, string TBC)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = FDR.GetFDClassDetails(FSGL, TSGL, Fdate, TDT, FBC, TBC);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet AVSBMTransaction(string BRCD, string FD, string ason)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = MB.GetMBDetails(BRCD, FD, ason);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetLnSlabWise(string BRCD, string AsOnDate, string FL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = LNSB.GetLaonAmtClassWise(BRCD, AsOnDate, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetLnSlabWiseDT(string BRCD, string AsOnDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = LNSB.GetLaonAmtClassWiseDet(BRCD, AsOnDate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetLnSlabWise_1(string BRCD, string AsOnDate, string FL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = LNSB.GetLaonAmtClassWise(BRCD, AsOnDate, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetABRALRDetails(string BRCD, string ason)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = FDS.GetABRALRDt(BRCD, ason);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetIWRegDetails(string BranchID, string FBKcode, string FDT, string TDT, string FL, string FLT)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        try
        {
            dtEmployee1 = IWD.GetIWDt(BranchID, FBKcode, FDT, TDT, FL, FLT);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }

    public DataSet GetClgRegListSumy(string BranchID, string FBKcode, string FDT, string FL, string FLT)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = IWD.GetClgReg(BranchID, FBKcode, FDT, FL, FLT);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetClgRegList(string BranchID, string FBKcode, string FDT, string FL, string FLT)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = IWD.GetClgReg(BranchID, FBKcode, FDT, FL, FLT);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetIWClgRegDetails(string BranchID, string FBKcode, string FDT, string SGL, string FL, string FLT)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = IWDR.GetIWReg(BranchID, FBKcode, FDT, SGL, FL, FLT);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetIOReturnReg(string BranchID, string FBKcode, string FDT, string FL, string FLT)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = IORE.GetIOReturnDT(BranchID, FBKcode, FDT, FL, FLT);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetTransSubMonth(string FBC, string PT, string FDate, string TDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = GLM.GetMonthWiseSUBGL(FBC, PT, FDate, TDate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetTransSumarryMonhWise(string FBC, string FDate, string TDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = GLS.GetMonthWiseSumry(FBC, FDate, TDate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetDPLNList(string BranchID, string FDT, string FL, string FLT)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DPLN.DPLNAcclist(BranchID, FDT, FL, FLT);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GrtClgMemoDetails(String BRCD, string ason)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = MM.GetClgMemo(BRCD, ason);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetBrDPLNList(string AsOnDate, string FL)
    {
        DataSet ds1 = new DataSet();
        ds1 = FYDL.GetBrDPLNData(AsOnDate, FL);
        return ds1;
    }

    public DataSet GetLoanStatDetails(string FDate, string TDate, string PT, string AC, string FBC)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = LTD.GetLnStatData(FDate, TDate, PT, AC, FBC);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetAccOpClRpt(string fl, string fdate, string tdate, string fbrcd, string tbrcd, string prd)////Added by ankita on 13/06/2017 To display account opening and closing details
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CAOCR.GetAccOpClRpt(fl, fdate, tdate, fbrcd, tbrcd, prd);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetloanNillRpt(string fdate, string tdate, string fbrcd, string tbrcd, string Fprd, string Tprcd)////
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CAOCR.GetLoanNillRpt(fdate, tdate, fbrcd, tbrcd, Fprd, Tprcd);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet DetailsForPLALLBrReport(string fdate, string tdate, string fbrcd, string tbrcd, string FL)////
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CAOCR.GetDetailsForPLALLBrReport(fdate, tdate, fbrcd, tbrcd, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet DetailsForPLALLBrReport_1(string fdate, string tdate, string fbrcd, string tbrcd, string FL)////
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CAOCR.GetDetailsForPLALLBrReport(fdate, tdate, fbrcd, tbrcd, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet DtForPLALLBrReport_PL(string fdate, string tdate, string fbrcd, string tbrcd, string FL)////
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CAOCR.GetDetailsForPLALLRt(fdate, tdate, fbrcd, tbrcd, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet DtForPLALLBrReport_PL_1(string fdate, string tdate, string fbrcd, string tbrcd, string FL)////
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = CAOCR.GetDetailsForPLALLRt(fdate, tdate, fbrcd, tbrcd, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    //LOAN BASIC DETAIL

    //PLTransfer amruta-19/04/2017
    public DataSet GetPLRecord(string Date, string AC, string Flag, string EDAT)
    {
        DataSet ds1 = new DataSet();
        string mid1 = EV.GetMK(Session["MID"].ToString());
        DataTable dtEmployee = new DataTable();
        dtEmployee = PLT.GetPLRecord(Date, AC, Session["BRCD"].ToString(), Flag, EDAT, Session["MID"].ToString(), mid1);
        ds1.Tables.Add(dtEmployee);
        return ds1;

    }
    public DataSet GetDIVRecord(string Date, string BRCD, string GL, string Subgl, string MID)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = PLT.GetDIVRecord(Date, BRCD, GL, Subgl, MID);
        ds1.Tables.Add(dtEmployee);
        return ds1;

    }
    public DataSet GetIR(string brcd, string accno, string taccno)////Added by ankita on 06/07/2017 to display IJRegister
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = IJ.BindIR(brcd, accno, taccno);
        ds1.Tables.Add(dtEmployee);
        return ds1;
    }

    public DataSet GetJR(string brcd, string accno, string taccno)////Added by ankita on 06/07/2017 to display IJRegister
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = IJ.BindJR(brcd, accno, taccno);
        ds1.Tables.Add(dtEmployee);
        return ds1;
    }

    public DataSet GetGld5085(string FBRCD, string TBRCD, string FPRCD, string FDT, string TDT, string EDT, string CURRATE)////Added by ankita on 16/02/2018 gold loan valuation list
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = GLD.GetGold5085(FBRCD, TBRCD, FPRCD, FDATE, TDATE, EDAT, CURRATE);
        ds1.Tables.Add(dtEmployee);
        return ds1;
    }
    public DataSet GetmemberPassBk(string FDate, string TDate, string PT, string FBC)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = MPD.GetmemberDT(FDate, TDate, PT, FBC);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetmemberPassBk1(string FDate, string TDate, string PT, string FBC)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = MPD.GetmemberDT1(FDate, TDate, PT, FBC);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetmemberPassBk_DT(string FDate, string TDate, string PT, string FBC)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = MPD.GetmemberDetails(FDate, TDate, PT, FBC);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetAuditrail(string FBRCD, string TBRCD, string FL, string FDATE, string TDATE)////Added by ankita on 18/11/2017 
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = AVS5057.GETCUSTOPEN(FL, FBRCD, TBRCD, FDATE, TDATE);
        ds1.Tables.Add(dtEmployee);
        return ds1;
    }
    //public DataSet GetNPASelectTypeList_1(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string FL, string S1, string S2, string Flag, string FLT)
    //{
    //    DataSet ds1 = new DataSet();
    //    DataTable dtEmployee1 = new DataTable();
    //    dtEmployee1 = NPAC.GetNPASelectTypeListDT_1(FBRCD, TBRCD, FPRCD, TPRCD, FACCNO, TACCNO, FDT, FL, S1, S2, Flag, FLT);
    //    ds1.Tables.Add(dtEmployee1);
    //    return ds1;
    //}

    public DataSet GetAuditrail1(string FBRCD, string TBRCD, string FL, string EDATE, string GLCODE, string FDATE, string TDATE)////Added by ankita on 18/11/2017 
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee = new DataTable();
        dtEmployee = AVS5057.GETCUSTOPEN1(FL, FBRCD, TBRCD, EDATE, GLCODE, FDATE, TDATE);
        ds1.Tables.Add(dtEmployee);
        return ds1;
    }
    //public DataSet GetGLIntCalDT(string FBC, string PT, string FDate, string TDate, string FL)
    //{
    //    DataSet ds1 = new DataSet();
    //    DataTable dtEmployee1 = new DataTable();
    //    dtEmployee1 = GLMT.GetGLIntCalDetails(FBC, PT, FDate, TDate, FL);
    //    ds1.Tables.Add(dtEmployee1);
    //    return ds1;
    //}

    public DataSet GetFD_Chikotra(string BRCD, string SGL, string accno, string FLL)
    {//CreateDocReg(string FDU, string TDU, string FDT, string TDT, string BRCD)
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DRR.FDPrint_Chikotra(BRCD, SGL, accno, Session["MID"].ToString(), Session["EntryDate"].ToString(), FLL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetDtDivPayTrans(string FL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = FDS.GetDtDivPayTrans_DT(FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetDivMemList(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FDT, string TDT, string S1, string S2)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = NPAC.GetDivMemListDT(FBRCD, TBRCD, FPRCD, TPRCD, FDT, TDT, S1, S2);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetDivSHRDPList(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FDT, string TDT, string S1, string S2)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = NPAC.GetDivMemListSHRDP(FBRCD, TBRCD, FPRCD, TPRCD, FDT, TDT, S1, S2);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetLoanDeposR_EMP(string FL, string PTP, string Tdate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = LDR.GetLoanDeposR_EMPDT(FL, PTP, Session["BRCD"].ToString(), Tdate);
        // AR.Getinfo1(FDT, TDT, AGC, BRCD);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet RptShareTZMPDT(string FBRCD, string FDATE, string TDATE, string FACCNO, string TACCNO, string FL, string FLT)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        try
        {
            dtEmployee1 = CUSTDT.RptShareTZMPDTShow(BRCD, FDATE, TDATE, FACCNO, TACCNO, FL, FLT);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }
    public DataSet RptSanchitTZMPDT(string FBRCD, string FDATE, string TDATE, string FACCNO, string TACCNO, string FL, string FLT)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        try
        {
            dtEmployee1 = CUSTDT.RptSanchitTZMPDTShow(BRCD, FDATE, TDATE, FACCNO, TACCNO, FL, FLT);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }

    public DataSet RptAllLnBalListODDT(string BRCD, string FPRCD, string TPRCD, string AsOnDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        try
        {
            dtEmployee1 = CUSTDT.RptLnBalListODDT(BRCD, FPRCD, TPRCD, AsOnDate);
            ds1.Tables.Add(dtEmployee1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ds1;
    }

    public DataSet GetBrWiseGlSumry(string FBC, string PT, string FDate, string TDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = GLT.GetBrWiseGlSumry(FBC, PT, FDate, TDate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

    public DataSet GetOfficeGLDetails(string FBC, string PT, string FDate, string TDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = GLT.GetOfficeGLDT(FBC, PT, FDate, TDate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetAdmExpenses_DT(string FBC, string FDate, string TDate, string FL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = GLT.GetAdmExpensesDT(FBC, FDate, TDate, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetAdmExpenses_Sumry(string FBC, string FDate, string TDate, string FL)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = GLT.GetAdmExpensesSumry(FBC, FDate, TDate, FL);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetDivMemPendingList(string FBRCD, string TBRCD, string FDT, string TDT, string FLT, string FL, string S1, string S2)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = NPAC.GetDivMemPendingListDT(FBRCD, TBRCD, FDT, TDT, FLT, FL, S1, S2);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet GetChequeMseb(string MemberNo, string Divident, string DepositInt, string TotalPay, string CheqNo, string BankCode, string FL, string Flag)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = LR.GetChequeMsebS(MemberNo, Divident, DepositInt, TotalPay, CheqNo, BankCode, FL, Flag);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet DTPreSanLoanAPPList(string BRCD, string FDate, string TDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = LNAPP.GetPreSanLoanAPPList(BRCD, FDate, TDate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet DTSanLoanAPPList(string BRCD, string FDate, string TDate)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = LNAPP.DTGetSanLoanAPPList(BRCD, FDate, TDate);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }
    public DataSet DTDividentTRFProcess(string FDate, string BRCD, string PRDCD, string TPRCD, string MID)
    {
        DataSet ds1 = new DataSet();
        DataTable dtEmployee1 = new DataTable();
        dtEmployee1 = DIVP.DTDivTRFProGet(FDate, BRCD, PRDCD, TPRCD, MID);
        ds1.Tables.Add(dtEmployee1);
        return ds1;
    }

}


