using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsAVS5119
{
    ClsEncryptValue Ecry = new ClsEncryptValue();
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sResult = "";
    string TableName = "", EntryMid = "";
    int Result = 0;

    #region Functions

    public ClsAVS5119()
    {

    }

    public string GetGlCode(string BrCode, string PrCode)
    {
        try
        {
            sql = "Select GlCode From GlMast Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public int RemoveData(string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Delete From AVS5085 Where RefBrCd = '" + BrCode + "' And SystemDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' " +
                  "Delete From AVS5086 Where RefBrCd = '" + BrCode + "' And SystemDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    #endregion

    #region For Retrive Data

    public DataTable BindGridTrans(string BrCode, string EDate, string Mid)
    {
        DT = new DataTable();
        try
        {
            sql = "Select T.BrCd, ConVert(VarChar(10), T.EntryDate, 103) As EntryDate, T.GlCode, T.SubGlCode, Left(G.GlName, 25) As GlName, Left(T.PartiCulars, 25) As Parti, " +
                  "T.AccNo, (Case When TrxType = 1 Then T.Amount Else 0 End) As Credit, (case When TrxType = 2 Then T.Amount Else 0 End) As Debit " +
                  "From AVS5085 T With(NoLock) Inner Join GlMast G With(NoLock) On T.BrCd = G.BrCd And T.GlCode = G.GlCode And T.SubGlCode = G.SubGlCode " +
                  "Where T.RefBrCd = '" + BrCode + "' And T.SystemDate = '" + conn.ConvertDate(EDate).ToString() + "' And T.Mid = '" + Mid + "' And PmtMode <> 'TR_INT' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetTransDetails(string BrCode, string EDate, string Mid)
    {
        DT = new DataTable();
        try
        {
            sql = "Select T.BrCd, ConVert(VarChar(10), T.EntryDate, 103) As EntryDate, T.GlCode, T.SubGlCode, T.AccNo, T.PartiCulars, T.Amount, " +
                  "T.TrxType, T.Activity, T.PmtMode, T.Stage, T.MID, T.PcMac From AVS5085 T " +
                  "Where T.RefBrCd = '" + BrCode + "' And T.SystemDate = '" + conn.ConvertDate(EDate).ToString() + "' And T.Mid = '" + Mid + "' And T.Amount <> 0 ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetLoanTrans(string BrCode, string EDate, string Mid)
    {
        DT = new DataTable();
        try
        {
            sql = "Select RefBrCd, BrCd, ConVert(VarChar(10), EntryDate, 103) As EntryDate, LoanGlCode, SubGlCode, AccountNo, HeadDesc, TrxType, Narration, " +
                  "Amount, Activity, ScrollNo, Stage, MID, SystemDate " +
                  "From AVS5086 Where RefBrCd = '" + BrCode + "' And SystemDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' And Amount <> 0 ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    #endregion

    #region Insert Temp Function

    public int InsertData(string RefBrCode, string SystemDate, string BrCode, string EDate, string GlCode, string SubGlCode, string AccNo, string Parti,
        string Amount, string TrxType, string Activity, string PmtMode, string InstNo, string InstDate, string Stage, string Mid)
    {
        string ScrollNo = "";
        try
        {
            sql = "Select IsNull(Max(ScrollNo), 0) + 1 From AVS5085 Where RefBrCd = '" + RefBrCode + "' And SystemDate = '" + conn.ConvertDate(SystemDate).ToString() + "' And Mid = '" + Mid + "'";
            ScrollNo = conn.sExecuteScalar(sql);

            sql = "Insert Into AVS5085(RefBrCd, BrCd, EntryDate, GlCode, SubGlCode, AccNo, PartiCulars, Amount, TrxType, Activity, PmtMode, SetNo, ScrollNo, InstNo, InstDate, Stage, MID, PcMac, SystemDate) " +
                  "Values('" + RefBrCode + "', '" + BrCode + "', '" + conn.ConvertDate(EDate.Replace("12:00:00", "")) + "', '" + GlCode + "', '" + SubGlCode + "', '" + AccNo + "', '" + Parti + "', " +
                  "'" + Amount + "', '" + TrxType + "', '" + Activity + "', '" + PmtMode + "', '0', '" + ScrollNo + "', '" + InstNo + "', '" + conn.ConvertDate(InstDate) + "', " +
                  "'1001', '" + Mid + "', '" + conn.PCNAME().ToString() + "', '" + conn.ConvertDate(SystemDate) + "') ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int InsertLoanTrx(string RefBrCode, string SystemDate, string BrCode, string EDate, string LoanGlCode, string SubGlCode, string AccNo, string HeadDesc,
        string TrxType, string Activity, string Parti, string Amount, string Mid)
    {
        string ScrollNo = "";
        try
        {
            sql = "Select IsNull(Max(ScrollNo), 0) + 1 From AVS5086 Where RefBrCd = '" + RefBrCode + "' And SystemDate = '" + conn.ConvertDate(SystemDate).ToString() + "' And Mid = '" + Mid + "'";
            ScrollNo = conn.sExecuteScalar(sql);

            sql = "Insert Into AVS5086(RefBrCd, BrCd, EntryDate, LoanGlCode, SubGlCode, AccountNo, HeadDesc, TrxType, Narration, Amount, Activity, ScrollNo, Stage, MID, SystemDate) " +
                  "Values('" + RefBrCode + "', '" + BrCode + "', '" + conn.ConvertDate(EDate.Replace("12:00:00", "")) + "', '" + LoanGlCode + "', '" + SubGlCode + "', '" + AccNo + "', '" + HeadDesc + "', " +
                  "'" + TrxType + "', '" + Parti + "', '" + Amount + "', '" + Activity + "', '" + ScrollNo + "', '1001', '" + Mid + "', '" + conn.ConvertDate(SystemDate) + "') ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    #endregion

    #region Insert Main Function

    public int Authorized(string BrCode, string EDate, string Mid, string SetNo, string RefId, string Parti2)
    {
        try
        {
            string[] TD = EDate.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            EntryMid = Ecry.GetMK(Mid.ToString());

            sql = "Insert Into " + TableName + " (RecSrNo, AId, BrCd, EntryDate, PostingDate, FundingDate, GlCode, SubGlCode, AccNo, PartiCulars, PartiCulars2, Amount, " +
                  "TrxType, Activity, PmtMode, SetNo, ScrollNo, InstrumentNo, InstrumentDate, Stage, MID, PcMac, PayMast, CustNo, CustName, RefBrcd, RefId, F1, SystemDate) " +
                  "Select 0 As RecSrNo, 1 As AId, BrCd, EntryDate, EntryDate, EntryDate, GlCode, SubGlCode, AccNo, PartiCulars, '" + Parti2 + "' As PartiCulars2, Amount, TrxType, Activity, " +
                  "PmtMode, '" + SetNo + "' As SetNo, ScrollNo, InstNo, InstDate, '1003' As Stage, Mid, PcMac, '' As PayMast, '0' As CustNo, '' As CustName, RefBrCd, " +
                  "'" + RefId + "' As RefId, '" + EntryMid + "' As F1, GetDate() As SystemDate " +
                  "From AVS5085 Where RefBrCd = '" + BrCode + "' And SystemDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' And Amount <> 0";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int LoanTrx(string BrCode, string LoanGlCode, string SubGlCode, string AccNo, string HeadDesc, string TrxType, string Activity, string ScrollNo, string Parti1,
        string Amount, string SetNo, string Stage, string MID, string VID, string EDate, string RefId)
    {
        try
        {
            if (Convert.ToDouble(Amount) > 0)
            {
                sql = "Insert Into AVS_LnTrx (BrCd, LoanGlCode, SubGlCode, AccountNo, HeadDesc, TrxType, Activity, Narration, Amount, SetNo, Stage, ScrollNo, " +
                      "MID, MID_EntryDate, PcMac, EntryDate, RefId, SystemDate)" +
                      "Values('" + BrCode + "','" + LoanGlCode + "','" + SubGlCode + "','" + AccNo + "', '" + HeadDesc + "','" + TrxType + "', '" + Activity + "', " +
                      "'" + Parti1 + "', '" + Amount + "','" + SetNo + "', '" + Stage + "','" + ScrollNo + "', '" + MID + "', '" + conn.ConvertDate(EDate) + "', " +
                      "'" + conn.PCNAME().ToString() + "', '" + conn.ConvertDate(EDate) + "', '" + RefId + "', GetDate())";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    #endregion

    #region Update LastIntDate

    public string GetIntApp(string BrCode, string SGlCode)
    {
        try
        {
            sql = "Select L.Int_App From LoanGl L Where L.BrCd = '" + BrCode + "' And L.LoanGlCode = '" + SGlCode + "' And EffectiveDate = (Select Max(L1.EffectiveDate) From LoanGl L1 " +
                  "Where L.Brcd = L1.Brcd And L.LoanGlCode = L1.LoanGlCode)";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public int UpdateLastIntDate(string BrCode, string SGlCode, string AccNo, string EDate, string Mid)
    {
        try
        {
            sql = "Update LoanInfo Set Prev_IntDt = LastIntDate, LastIntDate = '" + conn.ConvertDate(EDate).ToString() + "', Mod_Date = '" + conn.ConvertDate(EDate).ToString() + "' Where BrCd= '" + BrCode + "' And LOANGLCODE = '" + SGlCode + "' And CUSTACCNO = '" + AccNo + "'";
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