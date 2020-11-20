using AVSCore.CommonUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FDPrintTxt
/// </summary>
public class FDPrintTxt :Process
{
    Details details;
  
    string wsIReportName;
    string wsIBRCD;
    string wsIProdCode;
    string wsIAccno;
    string wsIMID;
    string wsIEDT;

    string WsBranchName;
    string WsScheme;
    string WsCustName;
    int    WsAccno;
    string WsSumRupees;
    double WsDepoAmt;
    string WsPeriod;
    string WsEDT;
    string WsROI;
    string WsMatDate;
    double wsMatAmt;


	public FDPrintTxt()
	{
        Report = new TReport();
        details = new Details { Instance = this };
	}

    public override void RInit(object sender)
    {
        List<object> lst = (List<object>)sender;
        wsIReportName = lst[0].ToString();
        wsIBRCD = lst[1].ToString();
        wsIProdCode = lst[2].ToString();
        wsIAccno = lst[3].ToString();
        wsIMID = lst[4].ToString();
        wsIEDT = lst[5].ToString();
    }

    public override void Start()
    {
        DoneFlag = false;
        Report.Init(this, 1, wsIReportName);
        // Report.PrintBlock(reportLvlStart);

        DataTable dt = new DataTable();
        string FDT;
        if (string.IsNullOrEmpty(WsEDT))
        {
            FDT = "01/01/1990";
        }
        else
        {
            FDT = WsEDT;
        }
        ClsDocRegister DR = new ClsDocRegister();
        dt = DR.FDRecipt(wsIBRCD, wsIProdCode, wsIAccno, wsIMID, wsIEDT, "ORG");

        if (dt != null)
        {
            if (dt.Rows.Count != 0)
            {

                WsBranchName = dt.Rows[0]["MIDNAME"].ToString();
                WsScheme = dt.Rows[0]["GLNAME"].ToString();
                WsCustName = dt.Rows[0]["CustName"].ToString();
                WsAccno = Convert.ToInt32(dt.Rows[0]["AccNo"].ToString());
                WsSumRupees = dt.Rows[0]["WAMT"].ToString();
                WsDepoAmt = Convert.ToDouble(dt.Rows[0]["PRNAMT"].ToString());
                WsPeriod = dt.Rows[0]["Period"].ToString();
                WsEDT = dt.Rows[0]["OPENINGDATE"].ToString();
                WsROI = dt.Rows[0]["RATEOFINT"].ToString();
                WsMatDate = dt.Rows[0]["DueDate"].ToString(); 
                wsMatAmt = Convert.ToDouble(dt.Rows[0]["MATURITYAMT"].ToString());


                Report.PrintBlock(details);

            }
        }
        Report.PrintBlock(details);
        Report.Finalize();
        DoneFlag = true;

    }

    #region --Report Block--

  
    internal class Details : IRBlock
    {
        int SrNo = 0;
        
        public string BlockFormat { get; set; }
        
        public FDPrintTxt Instance { get; set; }

        public Details()
        {
            BlockFormat =
             
                 "                ~ST-65~                                                                          ~NU-4~       \r\n"+
                 "                ~ST-65~                                                                                       \r\n"+
                 "                ~ST-65~                                                                          ~ST-10~        \r\n\n"+
                 "                                                                          \n" +
                 "                ~ST-65~                                                                                       \r\n"+
                 "                                                                          \n" +
                 "                                                                          \n" +
                 "                                                                          \n" +
                 "                                                                          \n" +
                 "       ~AM-17.2~                  ~ST-65~                                                                          ~ST-10~          ~AM-5~       ~ST-10~              ~AM-17.2~                   \r\n";
        }

        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                 Instance.WsBranchName,
                 Instance.WsAccno,
                 Instance.WsScheme,
                 Instance.WsCustName,
                 Instance.WsEDT,
                 Instance.WsSumRupees,
                 Instance.WsDepoAmt,
                 Instance.WsPeriod,
                 Instance.WsEDT,
                 Instance.WsROI,
                 Instance.WsMatDate,
                 Instance.wsMatAmt,
                });
        }
    }              
    
    #endregion

}