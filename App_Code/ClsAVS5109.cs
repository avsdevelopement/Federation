using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsAVS5109
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sResult = "";
    double Balance = 0;
    string TableName = "";
    int Result = 0;

	public ClsAVS5109()
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

    public double GetOpenClose(string Brcode, string ProdCode, string AccNo, string EDate, string Flag)
    {
        try
        {
            sql = "Exec SP_OpClBalance @BrCode = '" + Brcode + "', @SubGlCode = '" + ProdCode + "', @AccNo = '" + AccNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = '" + Flag + "'";
            Balance = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Balance;
    }

    public DataTable BindGridData(string BrCode, string PrCode, string AccNo, string FrDate, string ToDate, string Mid)
    {
        try
        {
            DT = new DataTable();
            sql = "Exec ISP_AVS0143 @BrCode = '" + BrCode + "', @PrCode = '" + PrCode + "', @AccNo = '" + AccNo + "', @FromDate = '" + conn.ConvertDate(FrDate) + "', @ToDate = '" + conn.ConvertDate(ToDate) + "', @Mid = '" + Mid + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int InsertTrans(string CustAcc, string BrCode, string GlCode, string SubGlCode, string AccNo)
    {
        try
        {
            string EntryDate = "", SetNo = "";
            string[] CusAcc = CustAcc.ToString().Split('_');

            if (CusAcc.Length > 1)
            {
                EntryDate = CusAcc[0].ToString();
                SetNo = CusAcc[1].ToString();

                string TableName;
                string[] TD = EntryDate.Replace("12:00:00 AM", "").Split('/');
                TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

                sql = "Insert Into AVS5076(BrCd, EntryDate, GlCode, SubGlCode, AccNo, Particular, Particular2, Amount, TrxType, Activity, PmtMode, SetNo, ScrollNo, InstNo, InstDate, Stage, Mid, SystemDate) " +
                      "Select BrCd, EntryDate, GlCode, SubGlCode, AccNo, Particulars, Particulars2, Amount, TrxType, Activity, PmtMode, SetNo, ScrollNo, InstrumentNo, InstrumentDate, Stage, Mid, SystemDate  " +
                      "From " + TableName + " Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EntryDate).ToString() + "' And SetNo = '" + SetNo + "' " +
                      "And GlCode = '" + GlCode + "' And SubGlCode = '" + SubGlCode + "' And AccNo = '" + AccNo + "' And TrxType = '1' And Stage = '1003'";
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