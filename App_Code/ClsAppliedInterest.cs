using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public class ClsAppliedInterest
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "", sResult = "";
    int Result = 0;

	public ClsAppliedInterest()
	{
		
	}

    public DataSet GetSavingIntCal(string BrCode, string GlCode, string PrCode, string AsOnDate, string CalType)
    {
        try
        {
            sql = "Exec RptAppliedInterest '" + BrCode + "', '" + GlCode + "', '" + PrCode + "', '" + conn.ConvertDate(AsOnDate).ToString() + "', '" + CalType + "'";
            DT = new DataTable();
            DS = new DataSet();
            DT = conn.GetDatatable(sql);

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

    public DataTable  GetInterestCalTextRpt(string BrCode, string GlCode, string PrCode, string AsOnDate, string CalType)
    {
        try
        {
            sql = "Exec AppliedInterestTxtRpt '" + BrCode + "', '" + GlCode + "', '" + PrCode + "', '" + conn.ConvertDate(AsOnDate).ToString() + "', '" + CalType + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

}