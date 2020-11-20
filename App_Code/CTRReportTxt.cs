using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AVSCore.CommonUtility;
using System.Data;

public class CTRReportTxt : Process
{
    ReportLvlStart reportLvlStart;
    Details details;
    ReportLvlEnd reportLvlEnd;

    string wsReportName;
    string wsUserName;
    string wsFPRD;
    string wsTPRD;
    string wsFromDate;
    string wsToDate;
    string wsCTRL;

    string wsSrNo;
    double wsCrBal;
    double wsDrBal;
    string wsAccNumber;
    string wsPrCode;

    double wsRptRecordTotal;
    double wsRptCrBal;
    double wsRptDrBal;

	public CTRReportTxt()
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
        wsUserName = lst[1].ToString();
        wsFPRD = lst[2].ToString();
        wsTPRD = lst[3].ToString();
        wsFromDate = lst[4].ToString();
        wsToDate = lst[5].ToString();
        wsCTRL = lst[6].ToString();
    }

    public override void Start()
    {
        DoneFlag = false;
        Report.Init(this, 1, wsReportName);
        Report.PrintBlock(reportLvlStart);

        DataTable dt = new DataTable();
        string FDT = "";
        if (string.IsNullOrEmpty(wsFromDate))
        {
            FDT = "01/01/1990";
        }
        else
        {
            FDT = wsFromDate;
        }

        string[] FT = FDT.Split('/');
        string[] TT = wsToDate.Split('/');

        ClsCTRReport CTR = new ClsCTRReport();
        dt = CTR.GetCTRTable(wsFPRD, wsTPRD, wsFromDate, wsToDate, wsCTRL);
        if (dt != null)
        {
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    wsCrBal = 0;
                    wsDrBal = 0;

                    wsSrNo = dt.Rows[i]["ID"].ToString();
                    wsCrBal = Math.Round(Convert.ToDouble(dt.Rows[i]["CRBAL"].ToString()), 2);
                    wsDrBal = Math.Round(Convert.ToDouble(dt.Rows[i]["DRBAL"].ToString()), 2);
                    wsAccNumber = dt.Rows[i]["ACCNO"].ToString();
                    wsPrCode = dt.Rows[i]["SUBGLCODE"].ToString();

                    ++wsRptRecordTotal;
                    wsRptCrBal = wsRptCrBal + wsCrBal;
                    wsRptDrBal = wsRptDrBal + wsDrBal;
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
        public CTRReportTxt Instance { get; set; }

        public PageLvlStart()
        {
            BlockFormat =
            " ~ST-1~                                                                                                  \r\n" +
            " REPORT ID :~ST-20~                           RUN DATE :~DT~                              PAGE : ~NU-5~  \r\n" +
            "|--------------------------------------------------------------------------------------------------------|\r\n" +
            "| ID                         |       Credit         |       Debit          | Account Number | Product No |\r\n" +
            "|----------------------------|----------------------|----------------------|----------------|------------|\r\n";

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
        public CTRReportTxt Instance { get; set; }

        public ReportLvlStart()
        {
            BlockFormat =
            "| SELECTION :-  From Date : ~ST-15~                  To Date : ~ST-15~                                   |\r\n" +
            "|========================================================================================================|\r\n";
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
        public CTRReportTxt Instance { get; set; }

        public PageLvlEnd()
        {
            BlockFormat =
            "|--------------------------------------------------------------------------------------------------------|\r\n";
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
        public CTRReportTxt Instance { get; set; }

        public Details()
        {
            BlockFormat =
            "| ~ST-6~                     |~AM-17.2~             |~AM-17.2~             | ~ST-15~        | ~ST-9~     |\r\n";

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                 Instance.wsSrNo,
                 Instance.wsCrBal,
                 Instance.wsDrBal,
                 Instance.wsAccNumber,
                 Instance.wsPrCode,
                });
        }
    }

    internal class ReportLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public CTRReportTxt Instance { get; set; }

        public ReportLvlEnd()
        {
            BlockFormat =
            "|----------------------------|----------------------|----------------------|----------------|------------|\r\n" +
            "|Total Records :=>~NU-9~     |~AM-17.2~             |~AM-17.2~             |                |            |\r\n" +
            "|========================================================================================================|\r\n";

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                    Instance.wsRptRecordTotal,
                    Instance.wsRptCrBal,
                    Instance.wsRptDrBal,
                });
        }
    }

    #endregion
}