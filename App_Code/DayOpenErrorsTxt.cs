using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AVSCore.CommonUtility;
using System.Web;

public class DayOpenErrorsTxt : Process
{
    ReportLvlStart reportLvlStart;
    Details details;
    ReportLvlEnd reportLvlEnd;

    string wsReportName;
    string wsFromDate;
    string wsToDate;
    bool wsAll;
    bool wsCN;

    string wsPrdName;
    double wsOpBal;
    double wsDrBal;
    double wsCrBal;
    double wsClosBal;

    double wsRptTotal;
    double wsRptOpBal;
    double wsRptDrBal;
    double wsRptCrBal;
    double wsRptClosBal;

	public DayOpenErrorsTxt()
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
        wsFromDate = lst[1].ToString();
        wsToDate = lst[2].ToString();
        wsAll = Convert.ToBoolean(lst[3].ToString());
        wsCN = Convert.ToBoolean(lst[4].ToString());
    }

    public override void Start()
    {
        DoneFlag = false;
        Report.Init(this, 1, wsReportName);
        Report.PrintBlock(reportLvlStart);

        DataTable dt = new DataTable();
        string FDT;
        if (string.IsNullOrEmpty(wsFromDate))
        {
            FDT = "01/01/1990";
        }
        else
        {
            FDT = wsFromDate;
        }
        ClsTrailBalance TB = new ClsTrailBalance();
        dt = TB.GetInfo(FDT, wsToDate, "1", wsAll == true ? "Y" : "N", wsCN == true ? "C" : "N");
        if (dt != null)
        {
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    wsPrdName = string.Empty;
                    wsOpBal = 0;
                    wsDrBal = 0;
                    wsCrBal = 0;
                    wsClosBal = 0;
                    wsPrdName = dt.Rows[i]["GLCode"].ToString() + "/" + dt.Rows[i]["SubGlCode"].ToString() + "/" + dt.Rows[i]["GlName"].ToString().Trim();
                    wsOpBal = Math.Round(Convert.ToDouble(dt.Rows[i]["OPBAL"].ToString()), 2);
                    wsDrBal = Math.Round(Convert.ToDouble(dt.Rows[i]["DR"].ToString()), 2);
                    wsCrBal = Math.Round(Convert.ToDouble(dt.Rows[i]["CR"].ToString()), 2);
                    wsClosBal = Math.Round(wsOpBal - wsDrBal + wsCrBal, 2);
                    ++wsRptTotal;
                    wsRptOpBal += wsOpBal;
                    wsRptDrBal += wsDrBal;
                    wsRptCrBal += wsCrBal;
                    wsRptClosBal += Math.Round(wsOpBal - wsDrBal + wsCrBal, 2);
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
        public DayOpenErrorsTxt Instance { get; set; }

        public PageLvlStart()
        {
            BlockFormat =
            " ~ST-1~                                                                                                                                                           \r\n" +
            " REPORT ID :~ST-20~                                                     RUN DATE :~DT~                                                     PAGE : ~NU-5~          \r\n" +
            "|                                                                 Trial Balance Without Print Definition                                                         |\r\n" +
            "|----------------------------------------------------------------------------------------------------------------------------------------------------------------|\r\n" +
            "|Serial|            Product Code & Description                       |    Opening Balance   |        Payment       |        Receipt       |    Closing Balance   |\r\n" +
            "|------|-------------------------------------------------------------|----------------------|----------------------|----------------------|----------------------|\r\n";

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
        public DayOpenErrorsTxt Instance { get; set; }

        public ReportLvlStart()
        {
            BlockFormat =
            "| SELECTION :-  From Date : ~ST-15~                  To Date : ~ST-15~                                                                                           |\r\n" +
            "|================================================================================================================================================================|\r\n";

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
        public DayOpenErrorsTxt Instance { get; set; }

        public PageLvlEnd()
        {
            BlockFormat =
            "|----------------------------------------------------------------------------------------------------------------------------------------------------------------|\r\n";

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
        public DayOpenErrorsTxt Instance { get; set; }

        public Details()
        {
            BlockFormat =

            "|~NU-4~|~ST-58~                                                      |~AM-17.2~             |~AM-17.2~             |~AM-17.2~             |~AM-17.2~             |\r\n";

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                 ++SrNo,
                 Instance.wsPrdName,
                 Instance.wsOpBal,
                 Instance.wsDrBal,
                 Instance.wsCrBal,
                 Instance.wsClosBal
                });
        }
    }

    internal class ReportLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public DayOpenErrorsTxt Instance { get; set; }

        public ReportLvlEnd()
        {
            BlockFormat =
            "|--------------------------------------------------------------------|----------------------|----------------------|----------------------|----------------------|\r\n" +
            "|Total Records :=>~NU-9~                                             |~AM-17.2~             |~AM-17.2~             |~AM-17.2~             |~AM-17.2~             |\r\n" +
            "|================================================================================================================================================================|\r\n";

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                    Instance.wsRptTotal,
                    Instance.wsRptOpBal,
                    Instance.wsRptDrBal,
                    Instance.wsRptCrBal,
                    Instance.wsRptClosBal
                });
        }
    }

    #endregion
}