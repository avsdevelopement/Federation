using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public class ClsAVS5050
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sResult = "", sql = "";
    int Result = 0;

	public ClsAVS5050()
	{
		
	}

    public string GetDepoGL(string PrCode, string BrCode)
    {
        try
        {
            sql = "Select GlName+'_'+ ConVert(VarChar(10), SubGlCode) From GlMast Where BRCD = '" + BrCode + "' And GlCode In ('2', '15') And SubGlCode = '" + PrCode + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataSet GetDDSIntCal(string BrCode, string FPrCode, string ToPrCode, string FAccNo, string TAccNo, string FDate, string TDate, string MID, string Flag)
    {
        try
        {
            sql = "Exec ISP_AVS0078 '" + BrCode + "', '" + FPrCode + "', '" + ToPrCode + "', '" + FAccNo + "', '" + TAccNo + "', '" + conn.ConvertDate(FDate).ToString() + "', '" + conn.ConvertDate(TDate).ToString() + "', '" + MID + "', '" + Flag + "'";
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

    public int CalculateDDSInt(string BrCode, string FPrCode, string ToPrCode, string FAccNo, string TAccNo, string FDate, string TDate, string MID, string Flag)
    {
        try
        {
            sql = "Exec ISP_AVS0078 '" + BrCode + "', '" + FPrCode + "', '" + ToPrCode + "', '" + FAccNo + "', '" + TAccNo + "', '" + conn.ConvertDate(FDate).ToString() + "', '" + conn.ConvertDate(TDate).ToString() + "', '" + MID + "', '" + Flag + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string ApplyDDSInt(string BrCode, string FPrCode, string ToPrCode, string FAccNo, string TAccNo, string FDate, string TDate, string AsOnDate, string MID, string Flag)
    {
        try
        {
            sql = "Exec ISP_AVS0078 '" + BrCode + "', '" + FPrCode + "', '" + ToPrCode + "', '" + FAccNo + "', '" + TAccNo + "', '" + conn.ConvertDate(FDate).ToString() + "', '" + conn.ConvertDate(TDate).ToString() + "', '" + MID + "', '" + Flag + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

}