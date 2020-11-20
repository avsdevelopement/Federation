using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AVSCore.CommonUtility;
using System.Data;

public class BalanceSheetReportTxt : Process
{
    ReportLvlStart reportLvlStart;
    Details details;
    ReportLvlEnd reportLvlEnd;
    DataRow Lib, Ass;
    int i, j = 0;

    string wsReportName;
    string wsUserName;
    string wsBRCD;
    string wsFromDate;

    string wsLiab;
    double wsLBal;
    double wsLTotal;
    string wsAss;
    double wsABal;
    double wsATotal;

    double wsRptLSubTotal;
    double wsRptLTotal;
    double wsRptASubTotal;
    double wsRptATotal;

	public BalanceSheetReportTxt()
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
        wsBRCD = lst[2].ToString();
        wsFromDate = lst[3].ToString();
    }

    //public override void Start()
    //{
    //    DoneFlag = false;
    //    Report.Init(this, 1, wsReportName);
    //    Report.PrintBlock(reportLvlStart);

    //    DataTable dt = new DataTable();
    //    string FDT = "";
    //    if (string.IsNullOrEmpty(wsFromDate))
    //    {
    //        FDT = "01/01/1990";
    //    }
    //    else
    //    {
    //        FDT = wsFromDate;
    //    }

    //    ClsBalanceSheet BAL = new ClsBalanceSheet();
    //    dt = BAL.Balance(wsBRCD, FDT, wsUserName);
    //    if (dt != null)
    //    {
    //        if (dt.Rows.Count != 0)
    //        {
    //            string CRGLTP = dt.Rows[0]["CRGLTP"].ToString();
    //            string DRGLTP = dt.Rows[0]["DRGLTP"].ToString();

    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                wsLBal = 0;
    //                wsABal = 0;

    //                if (CRGLTP == dt.Rows[i]["CRGLTP"].ToString())
    //                {
    //                    wsLiab = dt.Rows[i]["CRGLN"].ToString();
    //                    wsLBal = Math.Round(Convert.ToDouble(dt.Rows[i]["CRBAL"].ToString()), 2);
    //                    wsLTotal = 0;

    //                    wsRptLSubTotal = wsRptLSubTotal + wsLBal;
    //                    wsRptLTotal = wsRptLTotal + wsLBal;
    //                }
    //                else
    //                {
    //                    wsLiab = "Sub Total Of " + CRGLTP;
    //                    wsLBal = 0;
    //                    wsLTotal = wsRptLSubTotal;
    //                    wsRptLSubTotal = 0;
    //                }

    //                if (DRGLTP == dt.Rows[i]["DRGLTP"].ToString())
    //                {
    //                    wsAss = dt.Rows[i]["DRGLN"].ToString();
    //                    wsABal = Math.Round(Convert.ToDouble(dt.Rows[i]["DRBAL"].ToString()), 2);
    //                    wsATotal = 0;

    //                    wsRptASubTotal = wsRptASubTotal + wsABal;
    //                    wsRptATotal = wsRptATotal + wsABal;
    //                }
    //                else
    //                {
    //                    wsAss = "Sub Total Of " + DRGLTP;
    //                    wsABal = 0;
    //                    wsATotal = wsRptASubTotal;
    //                    wsRptASubTotal = 0;
    //                }

    //                CRGLTP = dt.Rows[i]["CRGLTP"].ToString();
    //                DRGLTP = dt.Rows[i]["DRGLTP"].ToString();

    //                Report.PrintBlock(details);
    //            }
    //        }
    //    }
    //    Report.PrintBlock(reportLvlEnd);
    //    Report.Finalize();
    //    DoneFlag = true;
    //}

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

        ClsBalanceSheet BAL = new ClsBalanceSheet();
        //dt = BAL.Balance(wsBRCD, FDT, wsUserName,""); Rakesh
        if (dt != null)
        {
            if (dt.Rows.Count != 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    wsLBal = 0;
                    wsABal = 0;

                    wsLiab = dt.Rows[i]["CRGLN"].ToString();
                    wsLBal = Math.Round(Convert.ToDouble(dt.Rows[i]["CRBAL"].ToString()), 2);
                    wsLTotal = 0;
                    wsRptLTotal = wsRptLTotal + wsLBal;

                    wsAss = dt.Rows[i]["DRGLN"].ToString();
                    wsABal = Math.Round(Convert.ToDouble(dt.Rows[i]["DRBAL"].ToString()), 2);
                    wsATotal = 0;
                    wsRptATotal = wsRptATotal + wsABal;

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
        public BalanceSheetReportTxt Instance { get; set; }

        public PageLvlStart()
        {
            BlockFormat =
            " ~ST-1~                                                                                                                                                                       \r\n" +
            " REPORT ID :~ST-20~ As On ~ST-15~                                             RUN DATE :~DT~                                                                   PAGE : ~NU-5~  \r\n" +
            "|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------|\r\n" +
            "| Liability                              |       Balance        |   Total Balance      | Asset                                  |       Balance        |   Total Balance      |\r\n" +
            "|----------------------------------------|----------------------|----------------------|----------------------------------------|----------------------|----------------------|\r\n";

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>{
                Convert.ToChar(15).ToString(),//to print report in compress mode
                Instance.wsReportName,
                Instance.wsFromDate,
                DateTime.Now.Date,
                ++pageCount
            });
        }
    }

    internal class ReportLvlStart : IRBlock
    {
        public string BlockFormat { get; set; }
        public BalanceSheetReportTxt Instance { get; set; }

        public ReportLvlStart()
        {
            BlockFormat =
            "| SELECTION :-  As On Date : ~ST-10~                                                                                                                                             |\r\n" +
            "|=============================================================================================================================================================================|\r\n";
        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                    Instance.wsFromDate
                });
        }
    }

    internal class PageLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public BalanceSheetReportTxt Instance { get; set; }

        public PageLvlEnd()
        {
            BlockFormat =
            "|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------|\r\n";
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
        public BalanceSheetReportTxt Instance { get; set; }

        public Details()
        {
            BlockFormat =
            "| ~ST-39~                                | ~AM-17.2~            | ~AM-17.2~            | ~ST-39~                                | ~AM-17.2~            | ~AM-17.2~            |\r\n";

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                 Instance.wsLiab,
                 Instance.wsLBal,
                 Instance.wsLTotal,
                 Instance.wsAss,
                 Instance.wsABal,
                 Instance.wsATotal
                });
        }
    }

    internal class ReportLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public BalanceSheetReportTxt Instance { get; set; }

        public ReportLvlEnd()
        {
            BlockFormat =
            "|----------------------------------------|----------------------|----------------------|----------------------------------------|----------------------|----------------------|\r\n" +
            "|Total Liability                         |                      | ~AM-17.2~            |Total Asset                             |                      | ~AM-17.2~            |\r\n" +
            "|=============================================================================================================================================================================|\r\n";

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                    Instance.wsRptLTotal,
                    Instance.wsRptATotal,
                });
        }
    }
    #endregion
}