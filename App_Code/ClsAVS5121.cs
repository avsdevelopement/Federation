using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsAVS5121
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sResult = "";
    int Result = 0;

    public ClsAVS5121()
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

    #region Insert Main Function

    public int NPAMarking1(string BrCode, string PrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Exec ISP_NPAMarkingProc1 @BrCode = '" + BrCode + "', @PrCode = '" + PrCode + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Mid = '" + Mid + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int NPAMarking2(string BrCode, string PrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Exec ISP_NPAMarkingProc2 @BrCode = '" + BrCode + "', @PrCode = '" + PrCode + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Mid = '" + Mid + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    #endregion
}