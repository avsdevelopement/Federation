using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AVSCore.CommonUtility;
using System.Data;

public class SavingPassbookTxt : Process
{
    Details details;
    DataRow Lib, Ass;
    int i, j = 0;

    string wsReportName;
    string wsUserName;
    string wsFromDate;
    string wsToDate;
    string wsProdCode;
    string wsAccNo;
    string wsGL;
    string wsMid;
    string wsBrCode;

    string wsdate;
    string wsPerti;
    string wsInst;
    double wsCredit;
    double wsDebit;
    double wsBalance;

    double wsRptCredit;
    double wsRptDebit;
    double wsRptBalance;

	public SavingPassbookTxt()
	{
        Report = new TReport();
        pageLvlStart = new PageLvlStart { Instance = this };
        details = new Details { Instance = this };
        pageLvlEnd = new PageLvlEnd { Instance = this };
	}

    public override void RInit(object sender)
    {
        List<object> lst = (List<object>)sender;
        wsReportName = lst[0].ToString();
        wsUserName = lst[1].ToString();
        wsFromDate = lst[2].ToString();
        wsToDate = lst[3].ToString();
        wsProdCode = lst[4].ToString();
        wsAccNo = lst[5].ToString();
        wsGL = lst[6].ToString();
        wsMid = lst[7].ToString();
        wsBrCode = lst[8].ToString();
    }

    public override void Start()
    {
        DoneFlag = false;
        Report.Init(this, 1, wsReportName);

        DataTable dt = new DataTable();
        ClsSavPassBookPrint Pass = new ClsSavPassBookPrint();

        string FDT = "";
        string[] DTF;

        DTF = wsFromDate.ToString().Split('/');
        FDT = wsFromDate;
        string[] DTT = wsToDate.ToString().Split('/');

        //dt = Pass.SavingPassbook(wsBrCode, DTF[1].ToString(), DTT[1].ToString(), DTF[2].ToString(), DTT[2].ToString(), FDT, wsToDate.Trim(), wsAccNo, wsProdCode, wsMid.ToString(), wsBrCode.ToString(), wsGL);
        if (dt != null)
        {
            if (dt.Rows.Count != 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    wsCredit = 0;
                    wsDebit = 0;
                    wsBalance = 0;

                    wsdate = dt.Rows[i]["EDATE"].ToString();
                    wsPerti = dt.Rows[i]["PARTI"].ToString();
                    wsInst = dt.Rows[i]["INSTNO"].ToString();
                    wsCredit = Math.Round(Convert.ToDouble(dt.Rows[i]["CREDIT"].ToString()), 2);
                    wsDebit = Math.Round(Convert.ToDouble(dt.Rows[i]["DEBIT"].ToString()), 2);
                    wsBalance = Math.Round(Convert.ToDouble(dt.Rows[i]["BALANCE"].ToString()), 2);

                    wsRptCredit = wsRptCredit + wsCredit;
                    wsRptDebit = wsRptDebit + wsDebit;
                    wsRptBalance = wsRptBalance + wsBalance;

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
        public SavingPassbookTxt Instance { get; set; }

        public PageLvlStart()
        {
            BlockFormat =
            " ~ST-1~                                                                                                                                                                       \r\n" +
            " REPORT ID :~ST-20~                                   RUN DATE :~DT~                                                 PAGE : ~NU-5~  \r\n" +
            "|----------------------------------------------------------------------------------------------------------------------------------|\r\n" +
            "| Date      | Perticular               |  Instrument Number   |   Credit Balance     |   Debit Balance      |     Balance          |\r\n" +
            "|-----------|--------------------------|----------------------|----------------------|----------------------|----------------------|\r\n";

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

    internal class PageLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public SavingPassbookTxt Instance { get; set; }

        public PageLvlEnd()
        {
            BlockFormat =
            "|----------------------------------------------------------------------------------------------------------------------------------|\r\n";
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
        public SavingPassbookTxt Instance { get; set; }

        public Details()
        {
            BlockFormat =
            "| ~ST-10~   | ~ST-25~                  | ~ST-21~              | ~AM-17.2~            | ~AM-17.2~            | ~AM-17.2~            |\r\n";
        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                 Instance.wsdate,
                 Instance.wsPerti,
                 Instance.wsInst,
                 Instance.wsCredit,
                 Instance.wsDebit,
                 Instance.wsBalance
                });
        }
    }
    
    #endregion
}