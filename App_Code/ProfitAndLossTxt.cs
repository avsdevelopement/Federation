using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AVSCore.CommonUtility;
using System.Data;

public class ProfitAndLossTxt : Process
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

    string wsGlCode;
    string wsSubGlCode;
    string wsGlName;
    double wsOpBal;
    double wsCrBal;
    double wsDrBal;
    double wsClBal;

    double wsRptOpTotal;
    double wsRptCrTotal;
    double wsRptDrTotal;
    double wsRptClTotal;

    public ProfitAndLossTxt()
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

        ClsProfitAndLoss PandL = new ClsProfitAndLoss();
        dt = PandL.ProfitAndLoss(wsBRCD, FDT, wsUserName);

        if (dt != null)
        {
            if (dt.Rows.Count != 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    wsOpBal = 0;
                    wsCrBal = 0;
                    wsDrBal = 0;
                    wsClBal = 0;

                    wsGlCode = dt.Rows[i]["GlCode"].ToString();
                    wsSubGlCode = dt.Rows[i]["SubGlCode"].ToString();
                    wsGlName = dt.Rows[i]["GlName"].ToString();
                    wsOpBal = Math.Round(Convert.ToDouble(dt.Rows[i]["OpBal"].ToString()), 2);
                    wsCrBal = Math.Round(Convert.ToDouble(dt.Rows[i]["CrBal"].ToString()), 2);
                    wsDrBal = Math.Round(Convert.ToDouble(dt.Rows[i]["DrBal"].ToString()), 2);
                    wsClBal = Math.Round(Convert.ToDouble(dt.Rows[i]["ClBal"].ToString()), 2);

                    wsRptOpTotal = wsRptOpTotal + wsOpBal;
                    wsRptCrTotal = wsRptCrTotal + wsCrBal;
                    wsRptDrTotal = wsRptDrTotal + wsDrBal;
                    wsRptClTotal = wsRptClTotal + wsClBal;
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
        public ProfitAndLossTxt Instance { get; set; }

        public PageLvlStart()
        {
            BlockFormat =
            " ~ST-1~                                                                                                                                            \r\n" +
            " REPORT ID :~ST-23~                As On ~ST-10~                     RUN DATE :~DT~                                                 PAGE : ~NU-5~  \r\n" +
            "|--------------------------------------------------------------------------------------------------------------------------------------------------|\r\n" +
            "|  Gl Code  |Sub Gl Code|   Gl Name                    |   Opening Balance    |   Credit Balance     |    Debit Balance     |   Closing Balance    |\r\n" +
            "|-----------|-----------|------------------------------|----------------------|----------------------|----------------------|----------------------|\r\n";

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
        public ProfitAndLossTxt Instance { get; set; }

        public ReportLvlStart()
        {
            BlockFormat =
            "| SELECTION :-  As On Date : ~ST-10~                                                                                                               |\r\n" +
            "|==================================================================================================================================================|\r\n";
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
        public ProfitAndLossTxt Instance { get; set; }

        public PageLvlEnd()
        {
            BlockFormat =
            "|--------------------------------------------------------------------------------------------------------------------------------------------------|\r\n";
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
        public ProfitAndLossTxt Instance { get; set; }

        public Details()
        {
            BlockFormat =
            "| ~ST-10~   | ~ST-10~   |~ST-30~                       | ~AM-17.2~            | ~AM-17.2~            | ~AM-17.2~            | ~AM-17.2~            |\r\n";

        }
        
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                 Instance.wsGlCode,
                 Instance.wsSubGlCode,
                 Instance.wsGlName,
                 Instance.wsOpBal,
                 Instance.wsCrBal,
                 Instance.wsDrBal,
                 Instance.wsClBal
                });
        }
    }

    internal class ReportLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public ProfitAndLossTxt Instance { get; set; }

        public ReportLvlEnd()
        {
            BlockFormat =
            "|------------------------------------------------------|----------------------|----------------------|----------------------|----------------------|\r\n" +
            "| Total                                                | ~AM-17.2~            | ~AM-17.2~            | ~AM-17.2~            | ~AM-17.2~            |\r\n" +
            "|==================================================================================================================================================|\r\n";

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                    Instance.wsRptOpTotal,
                    Instance.wsRptCrTotal,
                    Instance.wsRptDrTotal,
                    Instance.wsRptClTotal
                });
        }
    }
    
    #endregion
}