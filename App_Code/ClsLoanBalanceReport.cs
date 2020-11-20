using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public class ClsLoanBalanceReport
{
    DbConnection Conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "", sResult = "";
    int Result = 0;

	public ClsLoanBalanceReport()
	{
		
	}

    public DataSet LoanBalanceReport(string FBrCode, string TBrCode, string FPrCode, string TPrCode, string AsOnDate, string Flag)
    {
        try
        {
            sql = "Exec RptLoanBalanceReport @FBrCode = '" + FBrCode + "', @TBrCode = '" + TBrCode + "', @FPrCode = '" + FPrCode + "', @TPrCode = '" + TPrCode + "', @AsOnDate = '" + Conn.ConvertDate(AsOnDate) + "', @Flag = '" + Flag + "'";
            DT = new DataTable();
            DS = new DataSet();
            DT = Conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
                DS.Tables.Add(DT);
            else
                DS = null;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }

}