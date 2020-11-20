using System;
using System.Collections.Generic;
using System.Linq;
using AVSCore.CommonUtility;

/// <summary>
/// Summary description for P009100
/// </summary>
public class P009100 : Process
{
    ReportLvlStart reportLvlStart;
    Details details;
    ReportLvlEnd reportLvlEnd;
    string wsvariable;
	public P009100()
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
        wsvariable = lst[0].ToString();
    }

    public override  void Start()
    {
        DoneFlag = false;
        Report.Init(this, 1, "P009100");
        Report.PrintBlock(reportLvlStart);


        Report.PrintBlock(details);

        Report.PrintBlock(reportLvlEnd);
        Report.Finalize();
        DoneFlag = true;

    }


    #region --Report Block--

    internal class ReportLvlStart : IRBlock
    {
        public string BlockFormat { get; set; }
        public P009100 Instance { get; set; }

        public ReportLvlStart()
        {
            BlockFormat =
        "| SELECTION :-  All / Specific Module   : ~ST-1~~ST-5~                                                                                                       | \r\n" +
        "|               Specific Module         : ~NU-3~   ~ST-15~                                                                                                   | \r\n" +
        "|               Report Date             : ~DT~                                                                                                               | \r\n" +
        "|               Skip Zero Balance (Y/N) : ~ST-2~                                                                                                             | \r\n" +
        "|============================================================================================================================================================| \r\n";

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                    /*Print Required Variables e.g Instance.prmAllSpecific, */
                    Instance.wsvariable,
                    Instance.wsvariable,
                    Instance.wsvariable,
                    Instance.wsvariable,
                    new DateTime(1900,01,01),
                    Instance.wsvariable
                });
        }
    }

    internal class PageLvlStart : IRBlock
    {
        int pageCount = 0;
        public string BlockFormat { get; set; }
        public P009100 Instance { get; set; }

        public PageLvlStart()
        {
            BlockFormat =
        " ~ST-1~                                                                                                                                                        \r\n" +
        " BRANCH      :~ST-60~                                                                                                          NODE : ~NU-5~                   \r\n" +
        " BRANCH CODE :~NU-5~                                                     RUN DATE :~DT~                                        USER : ~ST-15~                  \r\n" +
        " REPORT ID   :~ST-12~                                                                                                          PAGE : ~NU-5~                   \r\n" +
        "|============================================================================================================================================================= \r\n" +
        "| <----------------------------------------------------- Trial Balance Without Print Definition ---------------------------------------------------->        | \r\n" +
        "|------------------------------------------------------------------------------------------------------------------------------------------------------------| \r\n" +
        "|Serial|            Product Code & Description        |Module Code | Cond Ast     | Cond Liab  |      Debit Balance     |     Credit Balance     | CBL Date  | \r\n" +
        "|------|----------------------------------------------|------------|--------------|------------|------------------------|------------------------|-----------| \r\n";

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>{
                   Convert.ToChar(15).ToString(),//to print report in compress mode
                Instance.wsvariable,
                Instance.wsvariable,
                Instance.wsvariable,
                new DateTime(1900,01,11),
                Instance.wsvariable,
                Instance.wsvariable,
                Instance.wsvariable,
                ++pageCount
                                        });
        }
    }

    internal class PageLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public P009100 Instance { get; set; }

        public PageLvlEnd()
        {
            BlockFormat = "\r\n";

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
        public P009100 Instance { get; set; }

        public Details()
        {
            BlockFormat =
        "|~NU-2~|~ST-9~    ~ST-35~                             |~ST-11~     |~ST-13~       |~ST-11~     |~AM~                    |~AM~                    |~DT~       | \r\n";

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                    Instance.wsvariable,
                    Instance.wsvariable,
                    Instance.wsvariable,
                    Instance.wsvariable,
                    Instance.wsvariable,
                    Instance.wsvariable,
                    Instance.wsvariable,
                    Instance.wsvariable,
                    new DateTime(1900,01,01)
                });
        }
    }

    internal class ReportLvlEnd : IRBlock
    {
        public string BlockFormat { get; set; }
        public P009100 Instance { get; set; }

        public ReportLvlEnd()
        {
            BlockFormat =
        "|----------------------------------------------------------------------------------------------|------------------------|------------------------|-----------| \r\n" +
        "| Total Records :=>~NU-4~                                                                      |~AM~                    |~AM~                    |           | \r\n" +
        "|============================================================================================================================================================| \r\n";

        }
        public string GetPrintData()
        {
            return Instance.Report.ParseBlock(BlockFormat, new List<object>
                {
                    Instance.wsvariable,
                    Instance.wsvariable,
                    Instance.wsvariable
                });
        }
    }

    #endregion

}