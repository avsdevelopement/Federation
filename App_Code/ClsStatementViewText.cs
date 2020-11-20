using AVSCore.CommonUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class ClsStatementViewText : Process
{
    Details details;
    ReportLvlEnd reportLvlEnd;

    string wsBankName;
    string wsBranchName;
    string wsReportName;
    string wsUserName;
    string wsBrCode;
    string wsGlCode;
    string wsPrCode;
    string wsAccNo;
    string wsFrDate;
    string wsToDate;

    string wsEntryDate;
    string wsParticulars;
    string wsInstNo;
    double wsDrAmount;
    double wsCrAmount;
    double wsTotal;
    string wsDrCr;
    double wsTotalDrAmt;
    double wsTotalCrAmt;
    double wsRptTotal;

    public ClsStatementViewText()
    {
        Report = new TReport();
        pageLvlStart = new PageLvlStart { Instance = this };
        details = new Details { Instance = this };
        pageLvlEnd = new PageLvlEnd { Instance = this };
        reportLvlEnd = new ReportLvlEnd { Instance = this };
    }

    public override void RInit(object sender)
    {
        List<object> lst = (List<object>)sender;
        wsBankName = lst[0].ToString();
        wsBranchName = lst[1].ToString();
        wsReportName = lst[2].ToString();
        wsUserName = lst[3].ToString();
        wsBrCode = lst[4].ToString();
        wsGlCode = lst[5].ToString();
        wsPrCode = lst[6].ToString();
        wsAccNo = lst[7].ToString();
        wsFrDate = lst[8].ToString();
        wsToDate = lst[9].ToString();
    }

    public override void Start()
    {
        Report.Init(this, 1, wsReportName);

        DataTable dt = new DataTable();
        ClsStatementView SV = new ClsStatementView();
        dt = new DataTable();

        dt = SV.GetStatementView(wsBrCode, wsGlCode, wsPrCode, wsAccNo, wsFrDate, wsToDate);

        if (dt != null)
        {
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    wsEntryDate = dt.Rows[i]["EntryDate"].ToString();
                    wsParticulars = dt.Rows[i]["Particular"].ToString();
                    wsInstNo = dt.Rows[i]["InstNo"].ToString();
                    wsDrAmount = Convert.ToDouble(dt.Rows[i]["PDebit"].ToString());
                    wsCrAmount = Convert.ToDouble(dt.Rows[i]["PCredit"].ToString());
                    wsTotal = Convert.ToDouble(dt.Rows[i]["PBalance"].ToString());
                    wsDrCr = dt.Rows[i]["PDrCr"].ToString();

                    wsTotalDrAmt += wsDrAmount;
                    wsTotalCrAmt += wsCrAmount;
                    Report.PrintBlock(details);
                }
            }
        }

        Report.PrintBlock(reportLvlEnd);
        Report.Finalize();
    }

    #region --Report Block--

    internal class PageLvlStart : IRBlock
    {
        int pageCount = 0;
        public string BlockFormat { get; set; }
        public ClsStatementViewText Instance { get; set; }

        public PageLvlStart()
        {
            BlockFormat =
            "                                 ~ST-80~                                                                                                                               \r\n" +
            " Branch Name  : ~ST-80~                                                                                                                                                \r\n" +
            " Report Name  : Statements Of Accounts Instrumentwise                                                                                                                                               \r\n" +
            " From Date    : ~ST-15~             To Date    : ~ST-15~                                                                                                 PAGE : ~NU-5~ \r\n" +
            "|---------------------------------------------------------------------------------------------------------------------------------------------------------------------|\r\n" +
            "|      Date      |      Particulars                               |   Instruments       |    Dr Amount        |    Cr Amount        |    Total Amount     |   Dr/Cr   |\r\n" +
            "|----------------|------------------------------------------------|---------------------|---------------------|---------------------|---------------------|-----------|\r\n";
        }

        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>{
                Instance.wsBankName,
                Instance.wsBranchName,
                Instance.wsFrDate,
                Instance.wsToDate,
                ++pageCount
            });
        }
    }

    internal class PageLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public ClsStatementViewText Instance { get; set; }

        public PageLvlEnd()
        {
            BlockFormat = "|---------------------------------------------------------------------------------------------------------------------------------------------------------------------|\r\n";
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
        public string BlockFormat { get; set; }
        public ClsStatementViewText Instance { get; set; }

        public Details()
        {
            BlockFormat =
            "| ~ST-15~        | ~ST-45~                                        | ~NU-10~             | ~AM-17.2~           | ~AM-17.2~           | ~AM-17.2~           | ~ST-10~   |\r\n";
        }

        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                 Instance.wsEntryDate,
                 Instance.wsParticulars,
                 Instance.wsInstNo,
                 Instance.wsDrAmount,
                 Instance.wsCrAmount,
                 Instance.wsTotal,
                 Instance.wsDrCr
                });
        }
    }

    internal class ReportLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public ClsStatementViewText Instance { get; set; }

        public ReportLvlEnd()
        {
            BlockFormat =
         "|----------------|------------------------------------------------|---------------------|---------------------|---------------------|---------------------|-----------|\r\n" +
         "|Total                                                                                  | ~AM-17.2~           | ~AM-17.2~           |                                 |\r\n"+
         "|---------------------------------------------------------------------------------------------------------------------------------------------------------------------|\r\n";

        }

        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                  Instance.wsTotalDrAmt,
                  Instance.wsTotalCrAmt,
                });
        }
    }

    #endregion

}