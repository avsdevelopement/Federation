using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsAVS5147
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "", sResult = "";
    int Result = 0;

	public ClsAVS5147()
	{
		
	}

    public string GetProduct(string BrCode, string PrCode)
    {
        try
        {
            sql = "Select ConVert(VarChar(10), GlCode) +'_'+ ConVert(VarChar(10), SubGlCode) +'_'+ GlName From GlMast With(NoLock) " +
                  "Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataSet TrailRun(string FBrCode, string TBrCode, string DrPrCode, string Charges, string CrPrCode, string Parti1, string EDate, string Mid)
    {
        try
        {
            sql = "Exec ISP_AVS0190 '" + FBrCode + "', '" + TBrCode + "', '" + DrPrCode + "', '" + CrPrCode + "', '" + Charges + "', '" + conn.ConvertDate(EDate) + "', '" + Parti1 + "', '" + Mid + "', 'Trail'";
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

    public int PassVoucher(string FBrCode, string TBrCode, string DrPrCode, string Charges, string CrPrCode, string Parti1, string EDate, string Mid)
    {
        try
        {
            sql = "Exec ISP_AVS0190 '" + FBrCode + "', '" + TBrCode + "', '" + DrPrCode + "', '" + CrPrCode + "', '" + Charges + "', '" + conn.ConvertDate(EDate) + "', '" + Parti1 + "', '" + Mid + "', 'Post'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

}