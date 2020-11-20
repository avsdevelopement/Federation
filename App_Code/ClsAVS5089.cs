using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsAVS5089
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sResult = "";
    string TableName = "";
    int Result = 0;

    public ClsAVS5089()
    {

    }

    public string GetBranchName(string BrCode)
    {
        try
        {
            sql = "Select MidName From BankName With(NoLock) Where BrCd = '" + BrCode + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
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
        try
        {
            DT = new DataTable();
            sql = "Select AccNo, CustNo, Acc_Status, Stage From Avs_Acc With(NoLock) "+
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
            sql = "Select M.CustName From Avs_Acc Ac With(NoLock) "+
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

    public DataTable BindGridData(string Brcode, string PrCode, string AccNo, string FrDate, string ToDate, string Mid)
    {
        try
        {
            DT = new DataTable();
            sql = "Exec ISP_AVS0127 @BrCode = '" + Brcode + "', @ProdCode = '" + PrCode + "', @AccNo = '" + AccNo + "', @ChangAccNo = '0', @FromDate = '" + conn.ConvertDate(FrDate) + "', @ToDate = '" + conn.ConvertDate(ToDate) + "', @Mid = '" + Mid + "', @Flag = 'M'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int ChangeAccNo(string Brcode, string PrCode, string AccNo, string NewAccNo, string FrDate, string ToDate, string Mid)
    {
        try
        {
            DT = new DataTable();
            sql = "Exec ISP_AVS0127 @BrCode = '" + Brcode + "', @ProdCode = '" + PrCode + "', @AccNo = '" + AccNo + "', @ChangAccNo = '" + NewAccNo + "', @FromDate = '" + conn.ConvertDate(FrDate) + "', @ToDate = '" + conn.ConvertDate(ToDate) + "', @Mid = '" + Mid + "', @Flag = 'T'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

}