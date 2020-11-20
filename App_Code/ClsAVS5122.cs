using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsAVS5122
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sResult = "";
    int Result = 0;

	public ClsAVS5122()
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

    public DataTable GetAccStage(string BrCode, string PrCode, string AccNo)
    {
        DT = new DataTable();
        try
        {
            sql = "Select AccNo, CustNo, Acc_Status, Stage From Avs_Acc With(NoLock) " +
                  "Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "' And Stage <> 1004 ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string GetCustName(string BrCode, string PrCode, string AccNo)
    {
        try
        {
            sql = "Select M.CustName From Avs_Acc Ac With(NoLock) " +
                  "Inner Join Master M With(NoLock) On Ac.CustNo = M.CustNo " +
                  "Where Ac.BrCd = '" + BrCode + "' And Ac.SubGlCode = '" + PrCode + "' And Ac.AccNo = '" + AccNo + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public int TransferAcc(string BrCode, string FPrCode, string FAccNo, string TPrCode, string TAccNo, string FDate, string TDate, string Mid, string sFlag)
    {
        try
        {
            if (sFlag.ToString() == "P")
            {
                sql = "Exec ISP_AVS0154 @BrCode = '" + BrCode + "', @FProdCode = '" + FPrCode + "', @TProdCode = '" + TPrCode + "', @FDate = '" + conn.ConvertDate(FDate) + "', @TDate = '" + conn.ConvertDate(TDate) + "', @Mid = '" + Mid + "', @sFlag = '" + sFlag + "'";
                Result = conn.sExecuteQuery(sql);
            }
            else if (sFlag.ToString() == "A")
            {
                sql = "Exec ISP_AVS0154 @BrCode = '" + BrCode + "', @FProdCode = '" + FPrCode + "', @FAccNo = '" + FAccNo + "', @TProdCode = '" + TPrCode + "', @TAccNo = '" + TAccNo + "', @FDate = '" + conn.ConvertDate(FDate) + "', @TDate = '" + conn.ConvertDate(TDate) + "', @Mid = '" + Mid + "', @sFlag = '" + sFlag + "'";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

}