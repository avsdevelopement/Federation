using AVSCore.CommonUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for IOReportText
/// </summary>
public class OIReportText : Process
{
	
    

    ReportLvlStart reportLvlStart;
    Details details;
    ReportLvlEnd reportLvlEnd;

    string wsReportName;
    string wsUserName;
    string wsProdcode;
   
    string wsbrcd;
    string wsFdate;
    string wsTdate;
    string wsFBrcd;
    string wsTbrcd;
    string wsFBname;
    string wsTBname;
    double wsAccno;
    string wsCustname;
    string wsBdescr;
    string wsBRdescr;
    string wsInstruno;
    string wsInsDate;
    string wsGLName;
    double wsInsAmt;
    string wsflag;
    
    
    
    double wsRptTotal;
   
    double wsRptDrBal;
    double wsRptCrBal;
   
    double wsTotalAmt;


    public OIReportText()
    {
        //
        // TODO: Add constructor logic here
        //


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
        wsbrcd = lst[2].ToString();
        wsFdate = lst[3].ToString();
        wsTdate = lst[4].ToString();
        wsFBrcd = lst[5].ToString();
        wsTbrcd = lst[6].ToString();
        wsFBname = lst[7].ToString();
        wsTBname = lst[8].ToString();
        wsflag = lst[9].ToString();


       

            
      
    }

    public override void Start()
    {
        DoneFlag = false;
        Report.Init(this, 1, wsReportName);
        Report.PrintBlock(reportLvlStart);

        DataTable dt = new DataTable();
       // string FDT;

        ClsOIRegister OIR = new ClsOIRegister();
        dt = new DataTable();
        
        dt = OIR.CreateOutward(wsFBrcd, wsTbrcd, wsFdate, wsTdate, wsbrcd,wsflag);
     
        if (dt != null)
        {
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    wsInsAmt = 0;
                    wsAccno = Convert.ToDouble(dt.Rows[i]["ACC_No"].ToString());
                    wsCustname = dt.Rows[i]["CUSTNAME"].ToString();
                    wsProdcode = dt.Rows[i]["PRDUCT_CODE"].ToString();
                    wsGLName = dt.Rows[i]["GLNAME"].ToString();
                    wsBdescr = dt.Rows[i]["BANK_NAME"].ToString();
                    wsBRdescr = dt.Rows[i]["BRANCH_NAME"].ToString();
                    wsInstruno = dt.Rows[i]["INSTRU_NO"].ToString();
                    wsInsDate = dt.Rows[i]["INSTRUDATE"].ToString();
                    wsInsAmt = Math.Round(Convert.ToDouble(dt.Rows[i]["INSTRU_AMOUNT"].ToString()), 2);
                  
                    
                    wsTotalAmt += wsInsAmt;
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
        public OIReportText Instance { get; set; }

        public PageLvlStart()
        {
            BlockFormat =
            " Report Name : ~ST-30~                                                                                                                                                                                                                                            \r\n" +
            " From Date   : ~ST-15~                            To Date :~ST-15~                        From Bank code :~NU-5~                    To Bank code :~NU-5~                           PAGE : ~NU-5~                                                                  \r\n" +           
            "|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|\r\n" +
            "| SR.No. |  ACC_NO  |             CUSTNAME                |  PRDUCT_CODE  |         GLNAME         |                 BANK_NAME                    |                BRANCH_NAME                   |    INSTRU_NO    |    INSTRUDATE       |   INSTRU_AMOUNT       |\r\n" +
            "|--------|----------|-------------------------------------|---------------|------------------------|----------------------------------------------|----------------------------------------------|-----------------|---------------------|-----------------------|\r\n";


        }
        public string GetPrintData()
        {
            
                return Instance.Report.ParseBlock(BlockFormat, new List<object>{
                //Convert.ToChar(30).ToString(),//to print report in compress mode
                Instance.wsReportName,
                Instance.wsFdate,
                Instance.wsTdate,
                Instance.wsFBrcd,
                Instance.wsTbrcd,
                ++pageCount
               
                
            });
           
        }
    }

    internal class ReportLvlStart : IRBlock
    {
        public string BlockFormat { get; set; }
        public OIReportText Instance { get; set; }

        public ReportLvlStart()
        {
            BlockFormat =
           
            "|=================================================================================================================================================================================================================================================================|\r\n";

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                    Instance.wsFdate, 
                    Instance.wsTdate,                   
                });
        }
    }

    internal class PageLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public OIReportText Instance { get; set; }

        public PageLvlEnd()
        {

            BlockFormat =
            "|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|\r\n";

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
        public OIReportText Instance { get; set; }

        public Details()
        {

            BlockFormat =
            "| ~NU-6~ |~NU-6~    |~ST-40~                              |~NU-4~         |~ST-20~                 |~ST-58~                                       |~ST-58~                                       |~NU-6~           |~ST-10~              |~AM-17.2~              |\r\n";

         

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                 ++SrNo,
                 Instance.wsAccno,
                 Instance.wsCustname,
                 Instance.wsProdcode,
                 Instance.wsGLName,
                 Instance.wsBdescr,
                 Instance.wsBRdescr,
                 Instance.wsInstruno,
                 Instance.wsInsDate,
                 Instance.wsInsAmt
                });
        }
    }

    internal class ReportLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public OIReportText Instance { get; set; }

        public ReportLvlEnd()
        {
            BlockFormat =
            "|--------|----------|-------------------------------------|---------------|------------------------|----------------------------------------------|----------------------------------------------|-----------------|---------------------|-----------------------|\r\n" +
            "|Total Records :=>~NU-9~                                                                                                                                                                                                                        ~AM-17.2~        |\r\n" +
            "|=============================================================================================================================================================================================================================================================== |\r\n";
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
