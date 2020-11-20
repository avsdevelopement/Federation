using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AVSCore.CommonUtility;
using System.Data;

public class AccountStatementTxt : Process
{
    ReportLvlStart reportLvlStart;
    Details details;
    ReportLvlEnd reportLvlEnd;

    string wsReportName;
    string wsFromDate;
    string wsToDate;
    string wsAccNo;
    string wsAccType;
    string wsMRD;
    string wsBRCD;
    string wsGL;

    string wsAccNumber;
    string wsGLCode;
    string wsEDate;
    string wsInstNumber;
    string wsPerticular;
    double wsCrBal;
    double wsDrBal;
    double wsBal;

    double wsRptTotal;
    double wsRptCrBal;
    double wsRptDrBal;
    double wsRptBal;

	public AccountStatementTxt()
	{
        Report = new TReport();
        pageLvlStart = new PageLvlStart { Instance = this };
        reportLvlStart = new ReportLvlStart { Instance = this };
        details = new Details { Instance = this };
        pageLvlEnd = new PageLvlEnd { Instance = this };
        reportLvlEnd = new ReportLvlEnd { Instance = this };
	}

    public override void RInit(object sender)
    {
        List<object> lst = (List<object>)sender;
        wsReportName = lst[0].ToString();
        wsFromDate = lst[3].ToString();
        wsToDate = lst[4].ToString();
        wsAccNo = lst[5].ToString();
        wsAccType = lst[6].ToString();
        wsMRD = lst[7].ToString();
        wsBRCD = lst[8].ToString();
        wsGL = lst[2].ToString();
    }

    public override void Start()
    {
        DoneFlag = false;
        Report.Init(this, 1, wsReportName);
        Report.PrintBlock(reportLvlStart);

        DataTable dt = new DataTable();
        string FDT="";
        if (string.IsNullOrEmpty(wsFromDate))
        {
            FDT = "01/01/1990";
        }
        else
        {
            FDT = wsFromDate;
        }

        string[] DTF = FDT.ToString().Split('/');
        string[] DTT = wsToDate.ToString().Split('/');

        ClsAccountSTS AStmt = new ClsAccountSTS();
        dt = AStmt.AccountStatment(DTF[1].ToString(), DTT[1].ToString(), DTF[2].ToString(), DTT[2].ToString(), FDT, wsToDate, wsAccNo, wsAccType, wsMRD, wsBRCD, wsGL,wsBRCD); //BRCD ADDED _ABHISHEK..BUT THIS RECHECK THE BRCD
        if (dt != null)
        {
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    wsRptCrBal = 0;
                    wsRptDrBal = 0;
                    wsRptBal = 0;
                             
                    wsAccNumber = dt.Rows[i]["ACCNO"].ToString();
                    wsGLCode = dt.Rows[i]["SUBGLCODE"].ToString();
                    wsEDate = dt.Rows[i]["EDATE"].ToString();
                    wsInstNumber = dt.Rows[i]["INSTNO"].ToString();
                    wsPerticular = dt.Rows[i]["PARTI"].ToString();
                    wsCrBal = Math.Round(Convert.ToDouble(dt.Rows[i]["CREDIT"].ToString()), 2);
                    wsDrBal = Math.Round(Convert.ToDouble(dt.Rows[i]["DEBIT"].ToString()), 2);
                    wsBal = Math.Round(Convert.ToDouble(dt.Rows[i]["BALANCE"].ToString()), 2);

                    ++wsRptTotal;
                    wsRptCrBal = wsRptCrBal + wsCrBal;
                    wsRptDrBal = wsRptDrBal + wsDrBal;
                    wsRptBal = wsRptBal + wsBal;
                    Report.PrintBlock(details);
                }
            }
        }
        Report.PrintBlock(reportLvlEnd);
        Report.Finalize();
        DoneFlag = true;

    }

    #region --Report Block--

    internal class PageLvlStart : IRBlock
    {
        int pageCount = 0;
        public string BlockFormat { get; set; }
        public AccountStatementTxt Instance { get; set; }

        public PageLvlStart()
        {
            BlockFormat =
            " ~ST-1~                                                                                                                                                       \r\n" +
            " REPORT ID :~ST-20~                                                  RUN DATE :~DT~                                             PAGE : ~NU-5~                 \r\n" +
            "|                                                                  Account Statement                                                                          |\r\n" +
            "|-------------------------------------------------------------------------------------------------------------------------------------------------------------|\r\n" +
            "| ID   | Account Number| Sub GL Code  |  EDate   | Inst No |          Particular         |       Credit         |       Debit          |       Balance        |\r\n" +
            "|------|---------------|--------------|----------|---------|-----------------------------|----------------------|----------------------|----------------------|\r\n";

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>{
                Convert.ToChar(15).ToString(),//to print report in compress mode
                Instance.wsReportName,
                DateTime.Now.Date,
                ++pageCount
            });
        }
    }

    internal class ReportLvlStart : IRBlock
    {
        public string BlockFormat { get; set; }
        public AccountStatementTxt Instance { get; set; }

        public ReportLvlStart()
        {
            BlockFormat =
            "| SELECTION :-  From Date : ~ST-15~                  To Date : ~ST-15~                                                                                        |\r\n" +
            "|=============================================================================================================================================================|\r\n";
        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                    Instance.wsFromDate,
                    Instance.wsToDate
                });
        }
    }

    internal class PageLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public AccountStatementTxt Instance { get; set; }

        public PageLvlEnd()
        {
            BlockFormat =
            "|------------------------------------------------------------------------------------------------------------------------------------------------------------|\r\n";
        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
            {

            });
        }
    }

    internal class Details : IRBlock
    {
        int SrNo = 0;
        public string BlockFormat { get; set; }
        public AccountStatementTxt Instance { get; set; }

        public Details()
        {
            BlockFormat =
            "|~ST-6~| ~ST-14~       | ~ST-13~      |~ST-10~   |~ST-9~   |~ST-29~                      |~AM-17.2~             |~AM-17.2~             |~AM-17.2~             |\r\n";

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                 ++SrNo,
                 Instance.wsAccNumber,
                 Instance.wsGLCode,
                 Instance.wsEDate,
                 Instance.wsInstNumber,
                 Instance.wsPerticular,
                 Instance.wsCrBal,
                 Instance.wsDrBal,
                 Instance.wsBal
                });
        }
    }

    internal class ReportLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public AccountStatementTxt Instance { get; set; }

        public ReportLvlEnd()
        {
            BlockFormat =
            "|----------------------------------------------------------------------------------------|----------------------|----------------------|----------------------|\r\n" +
            "|Total Records :=>~NU-6~                                                                 |~AM-17.2~             |~AM-17.2~             |~AM-17.2~             |\r\n" +
            "|=============================================================================================================================================================|\r\n";

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                    Instance.wsRptTotal,
                    Instance.wsRptCrBal,
                    Instance.wsRptDrBal,
                    Instance.wsRptBal
                });
        }
    }

    #endregion
}