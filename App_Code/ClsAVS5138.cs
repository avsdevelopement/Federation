using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsAVS5138
{
    ClsEncryptValue Ecry = new ClsEncryptValue();
    ClsBindDropdown BD = new ClsBindDropdown();
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sResult = "";
    string TableName = "";
    int Result = 0;

    public ClsAVS5138()
    {

    }

    public DataTable GetDrCr(string BrCode, string EDate, string SetNo, string sFlag)
    {
        DT = new DataTable();
        try
        {
            sql = "Exec ISP_AVS0177 @BrCode = '" + BrCode + "', @EDate = '" + conn.ConvertDate(EDate) + "', @SetNo = '" + SetNo + "', @sFlag = '" + sFlag + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetVoucher(string BrCode, string EDate, string SetNo, string sFlag)
    {
        DT = new DataTable();
        try
        {
            sql = "Exec ISP_AVS0177 @BrCode = '" + BrCode + "', @EDate = '" + conn.ConvertDate(EDate) + "', @SetNo = '" + SetNo + "', @sFlag = '" + sFlag + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int BindVoucher(GridView GView, string BrCode, string EDate, string SetNo, string sFlag)
    {
        DT = new DataTable();
        try
        {
            sql = "Exec ISP_AVS0177 @BrCode = '" + BrCode + "', @EDate = '" + conn.ConvertDate(EDate) + "', @SetNo = '" + SetNo + "', @sFlag = '" + sFlag + "'";
            Result = conn.sBindGrid(GView, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetVoucherScroll(string BrCode, string EDate, string SetNo, string ScrollNo, string sFlag)
    {
        DT = new DataTable();
        try
        {
            sql = "Exec ISP_AVS0177 @BrCode = '" + BrCode + "', @EDate = '" + conn.ConvertDate(EDate) + "', @SetNo = '" + SetNo + "', @ScrollNo = '" + ScrollNo + "', @sFlag = '" + sFlag + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int MoveVoucher(string BrCode, string EDate, string SetNo, string Mid)
    {
        try
        {
            sql = "Exec SP_MoveVoucherTans @BrCode = '" + BrCode + "', @WorkDate = '" + conn.ConvertDate(EDate).ToString() + "', @SetNo = '" + SetNo + "', @UserMid = '" + Mid + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int UpdateRecord(string BrCode, string EDate, string SetNo, string ScrollNo, string GlCode, string PrCode, string AccNo, string Parti1, string Amount,
          string TrxType, string Activity, string PmtMode, string InstNo, string InstDate, string OldPrCode, string OldAccNo, string OldAmount, string WorkDate, string Mid)
    {
        try
        {
            string[] TD = EDate.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

            //  Update voucher to unauthorise into main table (M-Table)
            sql = "Update " + TableName + " Set Stage = '1001', Mid = '" + Mid + "', Vid = '' " +
                  "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' ";
            Result = conn.sExecuteQuery(sql);

            if (Result > 0)
            {
                //  Update record into main table (M-Table)
                sql = "Update " + TableName + " Set GlCode = '" + GlCode + "', SubGlCode = '" + PrCode + "', AccNo = '" + AccNo + "', PartiCulars = '" + Parti1 + "', " +
                      "Amount = '" + Amount + "', TrxType = '" + TrxType + "', Activity = '" + Activity + "', PmtMode = '" + PmtMode + "', InstrumentNo = '" + InstNo + "', " +
                      "InstrumentDate = '" + conn.ConvertDate(InstDate) + "', Mid = '" + Mid + "' " +
                      "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' And ScrollNo = '" + ScrollNo + "'";
                Result = conn.sExecuteQuery(sql);
            }

            if (Result > 0)
            {
                //  Update voucher to unauthorise into main table (AVS_LnTrx Table)
                sql = "Update AVS_LnTrx Set Stage = '1001', Mid = '" + Mid + "', Vid = '' " +
                      "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' ";
                conn.sExecuteQuery(sql);

                //  Update record into main table (AVS_LnTrx Table)
                sql = "Update AVS_LnTrx Set SubGlCode = '" + PrCode + "', AccountNo = '" + AccNo + "', Amount = '" + Amount + "', Narration = '" + Parti1 + "', " +
                      "TrxType = '" + TrxType + "', Activity = '" + Activity + "', Mid = '" + Mid + "', MID_EntryDate = '" + conn.ConvertDate(WorkDate) + "'  " +
                      "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' " +
                      "And SubGlCode = '" + OldPrCode + "' And AccountNo = '" + OldAccNo + "' And Amount = '" + OldAmount + "' And Trxtype= '" + TrxType + "' ";  // SubGlCode = '" + OldPrCode + "' 
                conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int InsertRecord(string BrCode, string EDate, string SetNo, string ScrollNo, string GlCode, string PrCode, string AccNo, string Parti1, string Amount,
        string TrxType, string Activity, string PmtMode, string InstNo, string InstDate, string Mid)
    {
        try
        {
            string NewScrollNo = "";
            string[] TD = EDate.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

            //  Generate new scroll number
            if (Convert.ToDouble(SetNo.ToString()) > 20000)
                sql = "Select IsNull(Max(ScrollNo), 0) + 1 From " + TableName + " Where EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
            else
                sql = "Select IsNull(Max(ScrollNo), 0) + 1 From " + TableName + " Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
            NewScrollNo = conn.sExecuteScalar(sql);

            //  Update voucher to unauthorise into main table (M-Table)
            sql = "Update " + TableName + " Set Stage = '1001', Mid = '" + Mid + "', Vid = '' " +
                  "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' ";
            Result = conn.sExecuteQuery(sql);

            if (Result > 0)
            {
                //  Insert record into main table (M-Table)
                sql = "Insert Into " + TableName + " (AID, BrCd, EntryDate, PostingDate, FundingDate, GlCode, SubGlCode, AccNo, PartiCulars, Amount, TrxType, Activity, PmtMode, " +
                      "SetNo, ScrollNo, InstrumentNo, InstrumentDate, Stage, MID, VID, PayMast, RefBrcd, SystemDate) " +
                      "Select AID, BrCd, EntryDate, PostingDate, FundingDate, '" + GlCode + "', '" + PrCode + "', '" + AccNo + "', '" + Parti1 + "', '" + Amount + "', '" + TrxType + "', " +
                      "'" + Activity + "', '" + PmtMode + "', SetNo, '" + NewScrollNo + "', InstrumentNo, InstrumentDate, Stage, '" + Mid + "', VID, PayMast, RefBrcd, GetDate() As SystemDate " +
                      "From " + TableName + " " +
                      "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' And ScrollNo = '" + ScrollNo + "' ";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    //  Update voucher to unauthorise into main table (AVS_LnTrx Table)
                    sql = "Update AVS_LnTrx Set Stage = '1001', Mid = '" + Mid + "', Vid = '' " +
                          "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' ";
                    conn.sExecuteQuery(sql);

                    //  Insert record into main table (AVS_LnTrx Table)
                    if (GlCode.ToString() == "11")
                    {
                        sql = "Insert InTo AVS_LnTrx(BrCd, LoanGlCode, SubGlCode, AccountNo, HeadDesc, TrxType, Narration, Amount, SetNo, ScrollNo, Stage, MID, MID_EntryDate, EntryDate, SystemDate) " +
                              "Select A.BrCd, G.Subglcode As LoanGlCode, A.SubGlCode, A.AccNo, '2' As HeadDesc, A.TrxType, A.PartiCulars, A.Amount, A.SetNo, A.ScrollNo, A.Stage, A.Mid, A.EntryDate, A.EntryDate, GetDate() As SystemDate " +
                              "From " + TableName + " A With(NoLock) " +
                              "Inner Join GlMast G With(NoLock) On A.Brcd = G.Brcd And G.IR = A.SubGlCode " +
                              "Where A.BrCd = '" + BrCode + "' And A.EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And A.SetNo = '" + SetNo + "' And A.ScrollNo = '" + ScrollNo + "' ";
                        Result = conn.sExecuteQuery(sql);
                    }
                    else if (GlCode.ToString() == "12")
                    {
                        sql = "Insert InTo AVS_LnTrx(BrCd, LoanGlCode, SubGlCode, AccountNo, HeadDesc, TrxType, Narration, Amount, SetNo, ScrollNo, Stage, MID, MID_EntryDate, EntryDate, SystemDate) " +
                              "Select A.BrCd, G.Subglcode As LoanGlCode, A.SubGlCode, A.AccNo, '3' As HeadDesc, A.TrxType, A.PartiCulars, A.Amount, A.SetNo, A.ScrollNo, A.Stage, A.Mid, A.EntryDate, A.EntryDate, GetDate() As SystemDate " +
                              "From " + TableName + " A With(NoLock) " +
                              "Inner Join GlMast G With(NoLock) On A.Brcd = G.Brcd And G.IOR = A.SubGlCode " +
                              "Where A.BrCd = '" + BrCode + "' And A.EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And A.SetNo = '" + SetNo + "' And A.ScrollNo = '" + ScrollNo + "' ";
                        Result = conn.sExecuteQuery(sql);
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string GetEntryMid(string BrCode, string EDate, string SetNo)
    {
        try
        {
            string[] TD = EDate.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

            sql = "Select Distinct Mid From " + TableName + " Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' " +
                  "And SetNo = '" + SetNo + "' ";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public int VoucherAuthorise(string BrCode, string EDate, string SetNo, string WorkDate, string Mid)
    {
        try
        {
            string[] TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            if (Convert.ToDouble(SetNo) < 20000)
            {
                //  Update voucher to authorise into main table (M-Table)
                sql = "Update " + TableName + " Set Stage = '1003', Vid = '" + Mid + "', PartiCulars2 = 'Modify date - " + WorkDate + "' " +
                      "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' ";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    //  Update voucher to authorise into main table (AllVcr Table)
                    sql = "Update AllVcr Set Stage = 1003, Vid = '" + Mid + "', PartiCulars2 = 'Modify date - " + WorkDate + "' " +
                          "Where SetNo = '" + SetNo + "' And BrCd = '" + BrCode + "' And  EntryDate = '" + conn.ConvertDate(EDate) + "'";
                    conn.sExecuteQuery(sql);

                    //  Update voucher to authorise into main table (Avs_LnTrx Table)
                    sql = "Update Avs_LnTrx Set Stage = 1003, Vid = '" + Mid + "', Vid_EntryDate = '" + conn.ConvertDate(WorkDate) + "' " +
                          "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    conn.sExecuteQuery(sql);
                }
            }
            else
            {
                //  Update voucher to authorise into main table (M-Table)
                sql = "Update " + TableName + " Set Stage = '1003', Vid = '" + Mid + "', PartiCulars2 = 'Modify date - " + WorkDate + "' " +
                      "Where EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' ";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    //  Update voucher to authorise into main table (AllVcr Table)
                    sql = "Update AllVcr Set Stage = 1003, Vid = '" + Mid + "', PartiCulars2 = 'Modify date - " + WorkDate + "' " +
                          "Where EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' ";
                    conn.sExecuteQuery(sql);

                    //  Update voucher to authorise into main table (Avs_LnTrx Table)
                    sql = "Update Avs_LnTrx Set Stage = 1003, Vid = '" + Mid + "', Vid_EntryDate = '" + conn.ConvertDate(WorkDate) + "' " +
                          "Where EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    conn.sExecuteQuery(sql);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int VoucherTransfer(string BrCode, string EDate, string SetNo, string TrfDate, string WorkDate, string Mid)
    {
        try
        {
            Result = 0;
            string NewSetNo = "";
            string NewRefNo = "";

            NewRefNo = (Convert.ToInt32(BD.GetMaxRefid(BrCode.ToString(), TrfDate.ToString(), "REFID")) + 1).ToString();
            if (Convert.ToDouble(SetNo == "" ? "0" : SetNo) > 20000)
                NewSetNo = BD.GetSetNo(TrfDate.ToString(), "IBTSetNo", BrCode.ToString()).ToString();
            else
                NewSetNo = BD.GetSetNo(TrfDate.ToString(), "DaySetNo", BrCode.ToString()).ToString();

            if ((Convert.ToDouble(NewRefNo == "" ? "0" : NewRefNo) > 0) && (Convert.ToDouble(NewSetNo == "" ? "0" : NewSetNo) > 0))
            {
                Result = VoucherCancel(BrCode, EDate, SetNo, "0", WorkDate, Mid);

                if (Result > 0)
                    Result = Transfer(BrCode, EDate, SetNo, WorkDate, TrfDate, NewSetNo, NewRefNo, Mid);

                if (Result > 0)
                    Result = Convert.ToInt32(NewSetNo);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int Transfer(string BrCode, string EDate, string SetNo, string WorkDate, string TrfDate, string NewSetNo, string NewRefNo, string Mid)
    {
        try
        {
            string TableName1 = "";
            string TableName2 = "";
            string[] TD1 = EDate.Split('/');
            string[] TD2 = TrfDate.Split('/');
            TableName1 = "AVSM_" + TD1[2].ToString() + TD1[1].ToString();
            TableName2 = "AVSM_" + TD2[2].ToString() + TD2[1].ToString();

            if (Convert.ToDouble(NewSetNo) < 20000)
            {
                sql = "Insert Into " + TableName2 + "(Aid, BrCd, EntryDate, PostingDate, FundingDate, GlCode, SubGlCode, AccNo, PartiCulars, PartiCulars2, Amount, Amount_1, " +
                      "Amount_2, TrxType, Activity, PmtMode, SetNo, ScrollNo, InstrumentNo, InstrumentDate, InstBankCD, InstBrCd, Stage, RTime, MID, PayMast, " +
                      "CustNo, CustName, RefBrcd, RecPrint, OrgBrCd, ResBrCd, RefId, TokenNo, Ref_Agent, F1, F2, F3, CReason, SystemDate, RecSrNo) " +
                      "Select Aid, BrCd, '" + conn.ConvertDate(TrfDate).ToString() + "', '" + conn.ConvertDate(TrfDate).ToString() + "', '" + conn.ConvertDate(TrfDate).ToString() + "', " +
                      "GlCode, SubGlCode, AccNo, PartiCulars, 'Modify date - " + WorkDate + "', Amount, Amount_1, Amount_2, TrxType, Activity, PmtMode, '" + NewSetNo + "', ScrollNo, InstrumentNo, " +
                      "InstrumentDate, InstBankCD, InstBrCd, '1001', RTime, '" + Mid + "', PayMast, CustNo, CustName, RefBrcd, RecPrint, OrgBrCd, ResBrCd, '" + NewRefNo + "', " +
                      "TokenNo, Ref_Agent, F1, F2, F3, CReason, SystemDate, RecSrNo " +
                      "From " + TableName1 + " Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' ";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    sql = "Insert Into AVS_LnTrx(BrCd, SubGlCode, AccountNo, HeadDesc, TrxType, Narration, Amount, SetNo, ScrollNo, LoanGlCode, Stage, MID, MID_EntryDate, EntryDate, SystemDate, Activity, RefId) " +
                          "Select BrCd, SubGlCode, AccountNo, HeadDesc, TrxType, Narration, Amount, '" + NewSetNo + "', ScrollNo, LoanGlCode, '1001', '" + Mid + "', " +
                          "'" + conn.ConvertDate(TrfDate).ToString() + "', '" + conn.ConvertDate(TrfDate).ToString() + "', GetDate(), Activity, '" + NewRefNo + "' " +
                          "From AVS_LnTrx Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    conn.sExecuteQuery(sql);

                    sql = "Insert Into AllVCR (BrCd, EntryDate, PostingDate, FundingDate, GlCode, SubGlCode, AccNo, PartiCulars, CREDIT, DEBIT, ACTIVITY, PMTMODE, SETNO, SCROLLNO, PARTICULARS2, "+
                          "INSTRUMENTNO, INSTRUMENTDATE, STAGE, RTIME, BRCD, MID, PAYMAST, CUSTNO, CUSTNAME, REFID, SYSTEMDATE, RefBrcd, RecPrint, OrgBrCd, ResBrCd, ref_agent) " +
                          "Select BrCd, EntryDate, PostingDate, FundingDate, GlCode, SubGlCode, AccNo, PartiCulars, Case When TrxType = '1' Then Amount Else 0 End As CREDIT, Case When TrxType = '2' Then Amount Else 0 End As DEBIT, " +
                          "ACTIVITY, PMTMODE, SETNO, SCROLLNO, 'Modify date - " + WorkDate + "', INSTRUMENTNO, INSTRUMENTDATE, STAGE, RTIME, MID, PAYMAST, CUSTNO, CUSTNAME, REFID, SYSTEMDATE, RefBrcd, RecPrint, OrgBrCd, ResBrCd, ref_agent " +
                          "From " + TableName2 + " Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(TrfDate).ToString() + "' And SetNo = '" + NewSetNo + "'";
                    conn.sExecuteQuery(sql);
                }
            }
            else
            {
                sql = "Insert Into " + TableName2 + "(Aid, BrCd, EntryDate, PostingDate, FundingDate, GlCode, SubGlCode, AccNo, PartiCulars, PartiCulars2, Amount, Amount_1, " +
                      "Amount_2, TrxType, Activity, PmtMode, SetNo, ScrollNo, InstrumentNo, InstrumentDate, InstBankCD, InstBrCd, Stage, RTime, MID, CID, VID, PcMac, PayMast, " +
                      "CustNo, CustName, RefBrcd, RecPrint, OrgBrCd, ResBrCd, RefId, TokenNo, Ref_Agent, F1, F2, F3, CReason, SystemDate, RecSrNo) " +
                      "Select Aid, BrCd, '" + conn.ConvertDate(TrfDate).ToString() + "', '" + conn.ConvertDate(TrfDate).ToString() + "', '" + conn.ConvertDate(TrfDate).ToString() + "', " +
                      "GlCode, SubGlCode, AccNo, PartiCulars, PartiCulars2, Amount, Amount_1, Amount_2, TrxType, Activity, PmtMode, '" + NewSetNo + "', ScrollNo, InstrumentNo, " +
                      "InstrumentDate, InstBankCD, InstBrCd, '1001', RTime, '" + Mid + "', CID, VID, PcMac, PayMast, CustNo, CustName, RefBrcd, RecPrint, OrgBrCd, ResBrCd, '" + NewRefNo + "', " +
                      "TokenNo, Ref_Agent, F1, F2, F3, CReason, SystemDate, RecSrNo " +
                      "From " + TableName1 + " Where EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' ";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    sql = "Insert Into AVS_LnTrx(BrCd, SubGlCode, AccountNo, HeadDesc, TrxType, Narration, Amount, SetNo, ScrollNo, LoanGlCode, Stage, MID, MID_EntryDate, EntryDate, SystemDate, Activity, RefId) " +
                          "Select BrCd, SubGlCode, AccountNo, HeadDesc, TrxType, Narration, Amount, '" + NewSetNo + "', ScrollNo, LoanGlCode, '1001', '" + Mid + "', " +
                          "'" + conn.ConvertDate(TrfDate).ToString() + "', '" + conn.ConvertDate(TrfDate).ToString() + "', GetDate(), Activity, '" + NewRefNo + "' " +
                          "From AVS_LnTrx Where EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    conn.sExecuteQuery(sql);

                    sql = "Insert Into AllVCR (BrCd, EntryDate, PostingDate, FundingDate, GlCode, SubGlCode, AccNo, PartiCulars, CREDIT, DEBIT, ACTIVITY, PMTMODE, SETNO, SCROLLNO, PARTICULARS2, " +
                          "INSTRUMENTNO, INSTRUMENTDATE, STAGE, RTIME, BRCD, MID, PAYMAST, CUSTNO, CUSTNAME, REFID, SYSTEMDATE, RefBrcd, RecPrint, OrgBrCd, ResBrCd, ref_agent) " +
                          "Select BrCd, EntryDate, PostingDate, FundingDate, GlCode, SubGlCode, AccNo, PartiCulars, Case When TrxType = '1' Then Amount Else 0 End As CREDIT, Case When TrxType = '2' Then Amount Else 0 End As DEBIT, " +
                          "ACTIVITY, PMTMODE, SETNO, SCROLLNO, PARTICULARS2, INSTRUMENTNO, INSTRUMENTDATE, STAGE, RTIME, MID, PAYMAST, CUSTNO, CUSTNAME, REFID, SYSTEMDATE, RefBrcd, RecPrint, OrgBrCd, ResBrCd, ref_agent " +
                          "From " + TableName2 + " Where EntryDate = '" + conn.ConvertDate(TrfDate).ToString() + "' And SetNo = '" + NewSetNo + "'";
                    conn.sExecuteQuery(sql);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int VoucherCancel(string BrCode, string EDate, string SetNo, string Reason, string WorkDate, string Mid)
    {
        try
        {
            string[] TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            if (Convert.ToDouble(SetNo) < 20000)
            {
                //  Update voucher to authorise into main table (M-Table)
                sql = "Update " + TableName + " Set Stage = '1004', Cid = '" + Mid + "', PartiCulars2 = 'Cancel date - " + WorkDate + "', CReason = '" + Reason + "' " +
                      "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' ";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    //  Update voucher to authorise into main table (AllVcr Table)
                    sql = "Update AllVcr Set Stage = 1004, Cid = '" + Mid + "', PartiCulars2 = 'Cancel date - " + WorkDate + "' " +
                          "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' ";
                    conn.sExecuteQuery(sql);

                    //  Update voucher to authorise into main table (Avs_LnTrx Table)
                    sql = "Update Avs_LnTrx Set Stage = 1004, Vid = '" + Mid + "', Vid_EntryDate = '" + conn.ConvertDate(WorkDate) + "' " +
                          "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    conn.sExecuteQuery(sql);
                }
            }
            else
            {
                //  Update voucher to authorise into main table (M-Table)
                sql = "Update " + TableName + " Set Stage = '1004', Cid = '" + Mid + "', PartiCulars2 = 'Cancel date - " + WorkDate + "', CReason = '" + Reason + "' " +
                      "Where EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' ";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    //  Update voucher to authorise into main table (AllVcr Table)
                    sql = "Update AllVcr Set Stage = 1004, Cid = '" + Mid + "', PartiCulars2 = 'Cancel date - " + WorkDate + "' " +
                          "Where EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' ";
                    conn.sExecuteQuery(sql);

                    //  Update voucher to authorise into main table (Avs_LnTrx Table)
                    sql = "Update Avs_LnTrx Set Stage = 1004, Vid = '" + Mid + "', Vid_EntryDate = '" + conn.ConvertDate(WorkDate) + "' " +
                          "Where EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    conn.sExecuteQuery(sql);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

}