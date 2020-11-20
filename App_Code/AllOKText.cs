using AVSCore.CommonUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for AllOKText
/// </summary>
public class AllOKText :Process
{
    
    ReportLvlStart reportLvlStart;
    Details details;
    ReportLvlEnd reportLvlEnd;

    string wsReportName;
    string wsUserName;
    string wsbrcd;
    string wsTdate;
    string wsflag;
    string wsEntry;
    string wsAsonDate;
    string wsBankName;
    double wsSubglcode;
    string wsGlname;
    double wsAvsm;
    double wsAvsb;
    double wsDiff;
    string wsBranchName;
    double wsRptTotal;
    double wsRptDrBal;
    double wsRptCrBal;
    double wsBTotalAmt;
    double wsMTotalAmt;
    double wsDTotalAmt;

    
	public AllOKText()
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
        wsBankName = lst[0].ToString();
        wsBranchName = lst[1].ToString(); 
        wsReportName = lst[2].ToString();
        wsUserName = lst[3].ToString();
        wsbrcd = lst[4].ToString();
        wsEntry=lst[5].ToString();
        wsAsonDate=lst[6].ToString();
       
      
    }

    public override void Start()
    {
        DoneFlag = false;
        Report.Init(this, 1, wsReportName);
        Report.PrintBlock(reportLvlStart);

        DataTable dt = new DataTable();
       // string FDT;

        ClsAllOKReport OIR = new ClsAllOKReport();
        dt = new DataTable();
        
        dt = OIR.CreateAllOK(wsAsonDate,wsbrcd);
     
        if (dt != null)
        {
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                  
                    
                    wsSubglcode=Convert.ToDouble(dt.Rows[i]["SUBGLCODE"].ToString());
                    wsGlname=dt.Rows[i]["GLNAME"].ToString();
                    wsAvsm=Convert.ToDouble(dt.Rows[i]["AVSM"].ToString());
                    wsAvsb=Convert.ToDouble(dt.Rows[i]["AVSB"].ToString());
                    wsDiff=Convert.ToDouble(dt.Rows[i]["DIFF"].ToString());

                    wsRptTotal++;
                    wsMTotalAmt +=wsAvsm;
                    wsBTotalAmt+=wsAvsb;
                    wsDTotalAmt+=wsDiff;
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
        public AllOKText Instance { get; set; }

        public PageLvlStart()
        {
            BlockFormat =
            " Bank Name    : ~ST-40~                                                             Branch Name    :~ST-40~                                                                                                             \r\n"+        
            " Report Name  : ~ST-30~                                                                                                                                                                                                 \r\n" +
            " As On Date   : ~ST-15~                                                             PAGE : ~NU-5~                                                                                                                       \r\n" +           
            "|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|\r\n" +
            "|      Sr.No      |      PRODUCT CODE      |                                NAME                                |            AS PER PL            |            AS PER GL            |            DIFF PL_GL            |\r\n" +
            "|-----------------|------------------------|--------------------------------------------------------------------|---------------------------------|---------------------------------|----------------------------------|\r\n";
             }
        public string GetPrintData()
        {
            
                return Instance.Report.ParseBlock(BlockFormat, new List<object>{
                Convert.ToChar(30).ToString(),//to print report in compress mode
                Instance.wsBankName,
                Instance.wsBranchName,
                Instance.wsReportName,
                Instance.wsAsonDate,
          
                ++pageCount
               
                
            });
           
        }
    }

    internal class ReportLvlStart : IRBlock
    {
        public string BlockFormat { get; set; }
        public AllOKText Instance { get; set; }

        public ReportLvlStart()
        {
            BlockFormat =
           
            "|======================================================================================================================================================================================================================|\r\n";

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
        public AllOKText Instance { get; set; }

        public PageLvlEnd()
        {
    
            BlockFormat =
            "|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|\r\n";
 
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
        public AllOKText Instance { get; set; }

        public Details()
        {

               BlockFormat =
             "|~NU-6~           |~NU-6~                  |~ST-40~                                                             |~AM-17.2~                    |~AM-17.2~                    |~AM-17.2~                     |\r\n";

         
         }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                 ++SrNo,
                 Instance.wsSubglcode,
                 Instance.wsGlname,
                 Instance.wsAvsm,
                 Instance.wsAvsb,
                 Instance.wsDiff
                 
                });
        }
    }

    internal class ReportLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public AllOKText Instance { get; set; }

        public ReportLvlEnd()
        {
               BlockFormat =
            "|-----------------|------------------------|--------------------------------------------------------------------|---------------------------------|---------------------------------|----------------------------------|\r\n"+
            "|Total Records :=>~NU-9~                                                                                        |~AM-17.2~                        |~AM-17.2~                        |~AM-17.2~                         |\r\n" +
            "|======================================================================================================================================================================================================================|\r\n";
       
           }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {

                  Instance.wsRptTotal,
                  Instance.wsMTotalAmt,
                  Instance.wsBTotalAmt,
                  Instance.wsDTotalAmt,
                
                });
        }
    }

    #endregion
}

