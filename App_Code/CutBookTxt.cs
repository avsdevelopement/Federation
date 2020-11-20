using AVSCore.CommonUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for CutBookTxt
/// </summary>
/// 
public class CutBookTxt : Process
{
    ReportLvlStart reportLvlStart;
    Details details;
    ReportLvlEnd reportLvlEnd;

    string wsReportName;
    string wsUserName;
    string wsGL;
    string wstxtdate;
    string wsPType;
    string wsbrcd;
    bool wsAll;
    bool wsCN;


    string wsMemType;
    double wsAccNo;
    int wsCustNo;
   string wsMName;
    double wsDbBal;
    double wsCrBal;
    
   double wsRptTotal;
   double wsRptOpBal;
   double wsRptDrBal;
   double wsRptCrBal;
    double wsRptClosBal;

    public CutBookTxt()
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
        wsGL = lst[2].ToString();
      //  wsPType = lst[3].ToString();
        wsbrcd = lst[4].ToString();
        wstxtdate = lst[5].ToString();
      
    }

    public override void Start()
    {
        DoneFlag = false;
        Report.Init(this, 1, wsReportName);
        Report.PrintBlock(reportLvlStart);

        DataTable dt = new DataTable();
       // string FDT;

        ClsCutBook CB = new ClsCutBook();
        dt = new DataTable();
      //  dt = CB.CreateCutBook(wsUserName, wsGL, wsPType, wsbrcd, wstxtdate);
        if (dt != null)
        {
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    wsMemType = "1";
                    
                    wsDbBal = 0;
                    wsCrBal = 0;                  
                    wsAccNo=Convert.ToDouble( dt.Rows[i]["AccNo"].ToString());
                    wsCustNo=Convert.ToInt32( dt.Rows[i]["CustNo"].ToString());
                    wsMName = dt.Rows[i]["Custname"].ToString();                 
                    wsDbBal = Math.Round(Convert.ToDouble(dt.Rows[i]["DR"].ToString()), 2);
                    wsCrBal = Math.Round(Convert.ToDouble(dt.Rows[i]["CR"].ToString()), 2);
                   
                    ++wsRptTotal;
                   // wsRptOpBal += wsOpBal;
                    wsRptDrBal += wsDbBal;
                    wsRptCrBal += wsCrBal;
                //    wsRptClosBal += Math.Round(wsOpBal - wsDrBal + wsCrBal, 2);
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
        public CutBookTxt Instance { get; set; }

        public PageLvlStart()
        {
            BlockFormat =
            " ~ST-1~                                                                                                                                                             \r\n" +
            " Name        :~ST-20~                                                     RUN DATE :~DT~                                PAGE : ~NU-5~                               \r\n" +           
            "|------------------------------------------------------------------------------------------------------------------------------------------------------------------|\r\n" +
            "|Srno  |   Member Type   |    Acc No    |    Cust No   |                  Member Name                                |    Debit Balance     |    Credit Balance    |\r\n" +
            "|------|-----------------|--------------|--------------|-------------------------------------------------------------|----------------------|----------------------|\r\n";
            //"|~NU-4~|~NU-5~           |~NU-4~        |~NU-4~        |~ST-58~                                                      |~AM-17.2~           |~AM-17.2~             |\r\n";
        }
        public string GetPrintData()
        {
            
                return Instance.Report.ParseBlock(BlockFormat, new List<object>{
                Convert.ToChar(15).ToString(),//to print report in compress mode
                Instance.wsReportName,
                DateTime.Now.Date,
                ++pageCount
                //Instance.wsbrcd,
               // Instance.wsRe
                
            });
           
        }
    }

    internal class ReportLvlStart : IRBlock
    {
        public string BlockFormat { get; set; }
        public CutBookTxt Instance { get; set; }

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
                    Instance.wstxtdate, 
                    Instance.wstxtdate,                   
                });
        }
    }

    internal class PageLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public CutBookTxt Instance { get; set; }

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
        public CutBookTxt Instance { get; set; }

        public Details()
        {

            BlockFormat =
            "|~NU-4~|~NU-5~           |~NU-4~        |~NU-4~        |~ST-58~                                                      |~AM-17.2~             |~AM-17.2~             |\r\n";

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                 ++SrNo,
                 Instance.wsMemType,
                 Instance.wsAccNo,
                 Instance.wsCustNo,
                 Instance.wsMName,
                 Instance.wsDbBal,
                 Instance.wsCrBal,
                });
        }
    }

    internal class ReportLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public CutBookTxt Instance { get; set; }

        public ReportLvlEnd()
        {
            BlockFormat =
            "|--------------------------------------------------------------------|----------------------|------------------------|----------------------|----------------------|\r\n" +
            "|Total Records :=>~NU-9~                                                                                             |~AM-17.2~             |~AM-17.2~             |\r\n" +
            "|================================================================================================================================================================|\r\n";

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {

                  Instance.wsRptTotal,
                 //   Instance.wsRptOpBal,
                    Instance.wsRptDrBal,
                    Instance.wsRptCrBal,
                //    Instance.wsRptClosBal
                });
        }
    }

    #endregion
}