using AVSCore.CommonUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for IOReportText
/// </summary>
public class DocTextReport : Process
{

    ReportLvlStart reportLvlStart;
    Details details;
    ReportLvlEnd reportLvlEnd;

    string wsReportName;
    string wsUserName;
    string wsbrcd;
    string wsedt;
    string wsFDU;
    string wsTDU;
    string wsFDT;
    string wsTDT;

    double wsAccno;
    string wsCustname;
    string wsDOUpload;
    string wsDocType;

    double wsTotalcust;
    
	public DocTextReport()
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
        List<object> lst=(List<object>)sender;
        wsUserName = lst[0].ToString();
        wsbrcd = lst[1].ToString();
        wsReportName = lst[2].ToString();
        wsedt = lst[3].ToString();
        wsFDU = lst[4].ToString();
        wsTDU = lst[5].ToString();
        wsFDT = lst[6].ToString();
        wsTDT = lst[7].ToString();

    }

    public override void Start()
    {
        DoneFlag = false;
        Report.Init(this, 1, wsReportName);
        Report.PrintBlock(reportLvlStart);
        int wscount=0;
        DataTable dt = new DataTable();
        // string FDT;

        ClsDocRegister OIR = new ClsDocRegister();
        dt = new DataTable();

        dt = OIR.CreateDocReg(wsFDU, wsTDU, wsFDT, wsTDT, wsbrcd);
        
        if (dt != null)
        {
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    wsAccno = Convert.ToDouble(dt.Rows[i]["ACCNO"].ToString());
                    wsCustname = dt.Rows[i]["CUSTNAME"].ToString();
                    wsDOUpload = dt.Rows[i]["DATEOFUPLOAD"].ToString();
                    wsDocType = dt.Rows[i]["Photo_Type"].ToString();
                    ++wscount;

                    Report.PrintBlock(details);
                }
                wsTotalcust = wscount;
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
        public DocTextReport Instance { get; set; }

        public PageLvlStart()
        {
            BlockFormat =
            " Report Name   : ~ST-30~          From Date   : ~ST-15~               From DocType    : ~NU-5~                                                                                                                                                                                                                                    \r\n" +
            " User Name     : ~ST-10~          To Date     :~ST-15~                To DocType      : ~NU-5~    PAGE : ~NU-5~                                                                  \r\n" +
            "|--------------------------------------------------------------------------------------------------------------|\r\n" +
            "|  SR.No. |     ACCNO     |                   CUSTNAME                   |    DATEOFUPLOAD    |   Photo_Type   |\r\n" +
            "|---------|---------------|----------------------------------------------|--------------------|----------------|\r\n";
         }
        public string GetPrintData()
        {

            return Instance.Report.ParseBlock(BlockFormat, new List<object>{
                //Convert.ToChar(30).ToString(),//to print report in compress mode
                Instance.wsReportName,
                Instance.wsFDU,
                Instance.wsFDT,
                Instance.wsUserName,
                Instance.wsTDU,
                Instance.wsTDT,
                ++pageCount
               
                
            });

        }
    }

    internal class ReportLvlStart : IRBlock
    {
        public string BlockFormat { get; set; }
        public DocTextReport Instance { get; set; }

        public ReportLvlStart()
        {

            BlockFormat =

            "|==============================================================================================================|\r\n";

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                                       
                });
        }
    }

    internal class PageLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public DocTextReport Instance { get; set; }

        public PageLvlEnd()
        {

            BlockFormat =
           "|--------------------------------------------------------------------------------------------------------------|\r\n";

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
        public DocTextReport Instance { get; set; }

        public Details()
        {
            BlockFormat =
           "|~NU-6~   |~NU-6~         |~ST-40~                                       |~ST-20~             |~NU-5~          |\r\n";
        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                 ++SrNo,
                 Instance.wsAccno,
                 Instance.wsCustname,
                 Instance.wsDOUpload,
                 Instance.wsDocType
                 
                });
        }
    }

    internal class ReportLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public DocTextReport Instance { get; set; }

        public ReportLvlEnd()
        {
            BlockFormat =
              "|---------|---------------|----------------------------------------------|--------------------|----------------|\r\n" +
              "   TOTAL CUSTOMER -> ~NU-15~                                                                                   |\r\n" +
              "|==============================================================================================================|\r\n";
        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {

                  Instance.wsTotalcust
                });
        }
    }

    #endregion
}

