using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AVSCore.CommonUtility;
using System.Data;

public class DayClosedReportTxt : Process
{
    ReportLvlStart reportLvlStart;
    Details details;
    ReportLvlEnd reportLvlEnd;
    int i, j = 0;

    string wsReportName;
    string wsUserName;
    string wsBrCode;

    string wsIssue;
    string wsDesc;

	public DayClosedReportTxt()
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
        wsBrCode = lst[2].ToString();
    }

    public override void Start()
    {
        DoneFlag = false;
        Report.Init(this, 1, wsReportName);

        DataTable dt = new DataTable();
        ClsDayClosedReport DAY = new ClsDayClosedReport();

        dt = DAY.DayActivity(wsBrCode.ToString());
        if (dt != null)
        {
            if (dt.Rows.Count != 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    wsIssue = dt.Rows[i]["NAME"].ToString();
                    wsDesc = dt.Rows[i]["REASON"].ToString();
                    Report.PrintBlock(details);
                }
            }
        }
        Report.Finalize();
        DoneFlag = true;
    }


    #region --Report Block--

    internal class PageLvlStart : IRBlock
    {
        int pageCount = 0;
        public string BlockFormat { get; set; }
        public DayClosedReportTxt Instance { get; set; }

        public PageLvlStart()
        {
            BlockFormat =
            " ~ST-1~                                                                                                                                                                       \r\n" +
            " REPORT ID :~ST-18~            RUN DATE :~DT~              PAGE : ~NU-5~  \r\n" +
            "|------------------------------------------------------------------------|\r\n" +
            "| Issue                    | Description                                 |\r\n" +
            "|--------------------------|---------------------------------------------|\r\n";

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>{
                Convert.ToChar(15).ToString(),
                Instance.wsReportName,
                DateTime.Now.Date,
                ++pageCount
            });
        }
    }

    internal class ReportLvlStart : IRBlock
    {
        public string BlockFormat { get; set; }
        public DayClosedReportTxt Instance { get; set; }

        public ReportLvlStart()
        {
            BlockFormat =
            "|========================================================================|\r\n";
        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {});
        }
    }

    internal class PageLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public DayClosedReportTxt Instance { get; set; }

        public PageLvlEnd()
        {
            BlockFormat =
            "|------------------------------------------------------------------------|\r\n";
        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object> { });
        }
    }

    internal class Details : IRBlock
    {
        int SrNo = 0;
        public string BlockFormat { get; set; }
        public DayClosedReportTxt Instance { get; set; }

        public Details()
        {
            BlockFormat =
            "|~ST-25~                   |~ST-46~                                      |\r\n";
        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                 Instance.wsIssue,
                 Instance.wsDesc
                });
        }
    }

    internal class ReportLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public DayClosedReportTxt Instance { get; set; }

        public ReportLvlEnd()
        {
            BlockFormat =
            "|------------------------------------------------------------------------|\r\n";
            
        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                { });
        }
    }
    #endregion
}