using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class ClsAuthorized
{
    ClsEncryptValue Ecry = new ClsEncryptValue();
    DbConnection conn = new DbConnection();
    DataTable dt = new DataTable();
    string EntryMid, verifyMid, DeleteMid = "";
    string sql, SCROLLNO, sResult, TableName = "";
    int Result;

    public ClsAuthorized()
    {

    }

    #region Insert Functions

    public int EntryForMUltiPosting(string ENTRYDATE, string POSTINGDATE, string FUNDINGDATE, string GLCODE, string SUBGLCODE, string ACCNO, string PARTICULARS,
       string PARTICULARS2, string AMOUNT, string TRXTYPE, string ACTIVITY, string PMTMODE, string SETNO, string INSTRUMENTNO, string INSTRUMENTDATE,
       string INSTBANKCD, string INSTBRCD, string STAGE, string RTIME, string BRCD, string MID, string CID, string VID, string PAYMAST, string CUSTNO,
       string CUSTNAME, string REFID, string Token, string Re_Agent)
    {
        try
        {

            string CR, DR;
            string[] TD = ENTRYDATE.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            CR = DR = "";
            if (TRXTYPE == "1")
            {
                CR = AMOUNT;
                DR = "0";
            }
            else if (TRXTYPE == "2")
            {
                DR = AMOUNT;
                CR = "0";
            }
            if (ACCNO == "")
                ACCNO = "0";

            if (Convert.ToDouble(SETNO) < 20000)
                sql = "Select IsNull(Max(ScrollNo), 0) + 1 From AllVcr Where BrCd = '" + BRCD + "' And EntryDate = '" + conn.ConvertDate(ENTRYDATE) + "' And SetNo = '" + SETNO + "'";
            else
                sql = "Select IsNull(Max(ScrollNo), 0) + 1 From AllVcr Where EntryDate = '" + conn.ConvertDate(ENTRYDATE) + "' And SetNo = '" + SETNO + "'";
            SCROLLNO = conn.sExecuteScalar(sql);

            REFID = REFID.ToString() == "" ? "0" : REFID;
            EntryMid = Ecry.GetMK(MID.ToString());

            sql = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                "VALUES('" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(POSTINGDATE) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "','" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "','" + PARTICULARS + "','" + CR + "','" + DR + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "','" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "','" + CID + "','" + VID + "','" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + REFID + "',GETDATE())";
            Result = conn.sExecuteQuery(sql);

            sql = "INSERT INTO " + TableName + " (AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID, CID, VID, PCMAC, PAYMAST, CUSTNO, CUSTNAME, REFID, F1, SYSTEMDATE, TokenNo, Ref_Agent) " +
               " VALUES ('1','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(POSTINGDATE) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "', '" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "', '" + PARTICULARS2 + "','" + AMOUNT + "','" + TRXTYPE + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "', '" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "','" + CID + "','" + VID + "','" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + REFID + "', '" + EntryMid + "', GETDATE(), '" + Token + "', '" + Re_Agent + "')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int Authorized(string ENTRYDATE, string POSTINGDATE, string FUNDINGDATE, string GLCODE, string SUBGLCODE, string ACCNO, string PARTICULARS,
        string PARTICULARS2, string AMOUNT, string TRXTYPE, string ACTIVITY, string PMTMODE, string SETNO, string INSTRUMENTNO, string INSTRUMENTDATE,
        string INSTBANKCD, string INSTBRCD, string STAGE, string RTIME, string BRCD, string MID, string CID, string VID, string PAYMAST, string CUSTNO,
        string CUSTNAME, string REFID, string Token,string RecSrno="0")
    {
        try
        {

            string CR, DR;
            string[] TD = ENTRYDATE.Replace("12:00:00 AM", "").Split('/');
            string[] TD1 = INSTRUMENTDATE.Replace("12:00:00 AM", "").Split('/');

                TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
                CR = DR = "";
                if (TRXTYPE == "1")
                {
                    CR = AMOUNT;
                    DR = "0";
                }
                else if (TRXTYPE == "2")
                {
                    DR = AMOUNT;
                    CR = "0";
                }
                if (ACCNO == "")
                    ACCNO = "0";

                if (Convert.ToDouble(SETNO) < 20000)
                    sql = "Select IsNull(Max(ScrollNo), 0) + 1 From AllVcr Where BrCd = '" + BRCD + "' And EntryDate = '" + conn.ConvertDate(ENTRYDATE) + "' And SetNo = '" + SETNO + "'";
                else
                    sql = "Select IsNull(Max(ScrollNo), 0) + 1 From AllVcr Where EntryDate = '" + conn.ConvertDate(ENTRYDATE) + "' And SetNo = '" + SETNO + "'";
                SCROLLNO = conn.sExecuteScalar(sql);

                REFID = REFID.ToString() == "" ? "0" : REFID;
                EntryMid = Ecry.GetMK(MID.ToString());

                sql = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID, PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE,TrxType) " +
                    "VALUES('" + conn.ConvertDate(ENTRYDATE) + "','" + conn.ConvertDate(POSTINGDATE) + "','" + conn.ConvertDate(FUNDINGDATE) + "','" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "','" + PARTICULARS + "','" + CR + "','" + DR + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "','" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "', '" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + REFID + "',GETDATE(),'" + TRXTYPE + "')";
                Result = conn.sExecuteQuery(sql);

                sql = "INSERT INTO " + TableName + " (RECSRNO,AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD, MID,VID, PCMAC,PAYMAST, CUSTNO, CUSTNAME, REFID, F1, TokenNo, RefBrcd, OrgBrCd, SystemDate) " +
                   " VALUES ('" + RecSrno + "','1','" + conn.ConvertDate(ENTRYDATE) + "','" + conn.ConvertDate(POSTINGDATE) + "','" + conn.ConvertDate(FUNDINGDATE) + "', '" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "', '" + PARTICULARS2 + "','" + AMOUNT + "','" + TRXTYPE + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "', '" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "','" + VID + "', '" + conn.PCNAME().ToString() + "', '" + PAYMAST + "', '" + CUSTNO + "', '" + CUSTNAME + "', '" + REFID + "', '" + EntryMid + "', '" + Token + "', '" + CID + "', '" + VID + "', GetDate())";
                Result = conn.sExecuteQuery(sql);
            
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int LoanAppAuthorized(string ENTRYDATE, string POSTINGDATE, string FUNDINGDATE, string GLCODE, string SUBGLCODE, string ACCNO, string PARTICULARS,
      string PARTICULARS2, string AMOUNT, string TRXTYPE, string ACTIVITY, string PMTMODE, string SETNO, string INSTRUMENTNO, string INSTRUMENTDATE,
      string INSTBANKCD, string INSTBRCD, string STAGE, string RTIME, string BRCD, string MID, string PrCode_AccNo, string VID, string PAYMAST, string CUSTNO,
      string CUSTNAME, string REFID, string Token)
    {
        try
        {

            string CR, DR;
            string[] TD = ENTRYDATE.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            EntryMid = Ecry.GetMK(MID.ToString());
            CR = DR = "";
            if (TRXTYPE == "1")
            {
                CR = AMOUNT;
                DR = "0";
            }
            else if (TRXTYPE == "2")
            {
                DR = AMOUNT;
                CR = "0";
            }
            if (ACCNO == "")
                ACCNO = "0";

            if (Convert.ToDouble(SETNO) < 20000) 
                sql = "Select IsNull(Max(ScrollNo), 0) + 1 From AllVcr Where BrCd = '" + BRCD + "' And EntryDate = '" + conn.ConvertDate(ENTRYDATE) + "' And SetNo = '" + SETNO + "'";
            else
                sql = "Select IsNull(Max(ScrollNo), 0) + 1 From AllVcr Where EntryDate = '" + conn.ConvertDate(ENTRYDATE) + "' And SetNo = '" + SETNO + "'";
            SCROLLNO = conn.sExecuteScalar(sql);

            REFID = REFID.ToString() == "" ? "0" : REFID;
            EntryMid = Ecry.GetMK(MID.ToString());

            sql = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                "VALUES('" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(POSTINGDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "','" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "','" + PARTICULARS + "','" + CR + "','" + DR + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "','" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "',0,'" + VID + "','" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + REFID + "',GETDATE())";
            Result = conn.sExecuteQuery(sql);

            sql = "INSERT INTO " + TableName + " (AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT, AMOUNT_1,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD, STAGE, RTIME, BRCD, MID, CID, VID, PCMAC, PAYMAST, CUSTNO, CUSTNAME, REFID, F1, SYSTEMDATE) " +
             " VALUES ('1','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(POSTINGDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "', '" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "', '" + PARTICULARS2 + "','" + AMOUNT + "', '" + PrCode_AccNo + "','" + TRXTYPE + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "', '" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "','0','" + VID + "','" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + REFID + "', '" + EntryMid + "', GETDATE())";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int PaymentToken(string ENTRYDATE, string POSTINGDATE, string FUNDINGDATE, string GLCODE, string SUBGLCODE, string ACCNO, string PARTICULARS,
       string PARTICULARS2, string AMOUNT, string TRXTYPE, string ACTIVITY, string PMTMODE, string SETNO, string INSTRUMENTNO, string INSTRUMENTDATE,
       string INSTBANKCD, string INSTBRCD, string STAGE, string RTIME, string BRCD, string MID, string CID, string VID, string PAYMAST, string CUSTNO,
       string CUSTNAME, string REFID, string tokanNo)
    {
        try
        {

            string CR, DR;
            string[] TD = ENTRYDATE.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            CR = DR = "";
            if (TRXTYPE == "1")
            {
                CR = AMOUNT;
                DR = "0";
            }
            else if (TRXTYPE == "2")
            {
                DR = AMOUNT;
                CR = "0";
            }
            if (ACCNO == "")
                ACCNO = "0";

            if (Convert.ToDouble(SETNO) < 20000)
                sql = "Select IsNull(Max(ScrollNo), 0) + 1 From AllVcr Where BrCd = '" + BRCD + "' And EntryDate = '" + conn.ConvertDate(ENTRYDATE) + "' And SetNo = '" + SETNO + "'";
            else
                sql = "Select IsNull(Max(ScrollNo), 0) + 1 From AllVcr Where EntryDate = '" + conn.ConvertDate(ENTRYDATE) + "' And SetNo = '" + SETNO + "'";
            SCROLLNO = conn.sExecuteScalar(sql);

            REFID = REFID.ToString() == "" ? "0" : REFID;
            EntryMid = Ecry.GetMK(MID.ToString());

            sql = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE,TokenNo) " +
                "VALUES('" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "','" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "','" + PARTICULARS + "','" + CR + "','" + DR + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "','" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "','" + CID + "','" + VID + "','" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + REFID + "',GETDATE(),'" + tokanNo + "')";
            Result = conn.sExecuteQuery(sql);

            sql = "INSERT INTO " + TableName + " (AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD, MID, CID, VID, PCMAC, PAYMAST, CUSTNO, CUSTNAME, REFID, F1, SYSTEMDATE, TokenNo) " +
               " VALUES ('1','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(POSTINGDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "', '" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "', '" + PARTICULARS2 + "','" + AMOUNT + "','" + TRXTYPE + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "', '" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "','" + CID + "', '" + VID + "', '" + conn.PCNAME().ToString() + "', '" + PAYMAST + "', '" + CUSTNO + "', '" + CUSTNAME + "', '" + REFID + "', '" + EntryMid + "', GETDATE(), '" + tokanNo + "')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    //StandingInst INSTRUCTION
    public int PostAuhtorizedEntry(string FLAG, string SFLAG, string EDT, string DBGL, string DBSUGL, string DBACCNO, string CRGL, string CRSUGL, string CRACCNO,
                                   string PARTI, string EMIAMT, string SETNO, string STAGE, string BRCD, string MID, string CID, string SINO,
                                    string ND, string MONEXEDAY)
    {
        try
        {
            string PACMAC = conn.PCNAME();
            ND = Convert.ToDateTime(ND).ToString("dd/MM/yyyy");

            sql = "EXEC SP_SI_INSERT @EDT='" + conn.ConvertDate(EDT) + "',@ED='" + conn.ConvertDate(EDT) + "',@DBGL='" + DBGL + "',@DBPRDCD='" + DBSUGL + "',@DBACCNO='" + DBACCNO + "',@CRGL='" + CRGL + "'," +
                " @CRPRDCD='" + CRSUGL + "',@CRACCNO='" + CRACCNO + "',@EMIAMOUNT='" + EMIAMT + "',@SETNO='" + SETNO + "',@STAGE='" + STAGE + "'," +
                " @BRCD='" + BRCD + "',@MID='" + MID + "',@PACMAC='" + PACMAC + "',@FLAG='" + FLAG + "',@SFLAG='" + SFLAG + "',@SINO='" + SINO + "',@NEXTTDATE='" + conn.ConvertDate(ND) + "',@RDBCHECK='MONTHLY',@PARTI='" + PARTI + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    //  Added by amol on 03/01/2018 for insert gst records
    public int InsertForGST(string BrCode, string InvoiceNo, string EntryDate, string CustNo, string SubGlCode, string IntRate, string DiscPer, string Discount, string NetPrice,
        string TaxPer, string Quantity, string PrValue, string DiscValue, string State, string SGST, string CGST, string IGST, string Total, string SetNo, string Mid)
    {
        try
        {
            sql = "Insert Into AVS5058(BRCD, InvoiceNo, LineItem, EntryDate, CustNo, SubGlCode, IntRate, Disc_Per, Discount, NetPrice, TaxPer, Quantity, Product_Value, Discount_Value, State, SGST, CGST, IGST, Total_Value, SetNo, MID, SystemDate) " +
                "Values('" + BrCode + "', '1', (Select IsNull(Max(LineItem), 0) + 1 From AVS5058 Where CustNo = '" + CustNo + "'), '" + conn.ConvertDate(EntryDate) + "', '" + CustNo + "', '" + SubGlCode + "', '" + IntRate + "', '0', '0', '" + NetPrice + "', " +
                "'" + TaxPer + "', '" + Quantity + "', '" + PrValue + "', '" + DiscValue + "', '0', '" + SGST + "', '" + CGST + "', '" + IGST + "', '" + Total + "', '" + SetNo + "', '" + Mid + "', GetDate())";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    #endregion



    #region Authorise Functions

    public int AuthoriseEntry(string BrCode, string SetNo, string Mid, string EDate, string PayMast, string ScrollNo)
    {
        try
        {
            string[] TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            verifyMid = Ecry.GetMK(Mid.ToString());

            if (PayMast == "DDSCLOSE")
            {
                //  Added by amol on 09/11/2017 for authorise DDS set only
                sql = "Select Top 1 RefId From " + TableName + " Where brcd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                sResult = conn.sExecuteScalar(sql);

                if (sResult != null && sResult != "")
                {
                    sql = "UPDATE " + TableName + " SET STAGE = 1003, VID = '" + Mid + "', F2 = '" + verifyMid + "' WHERE BrCd = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And RefId = '" + sResult + "'";
                    Result = conn.sExecuteQuery(sql);

                    if (Result > 0)
                    {
                        sql = "Update Avs_LnTrx Set Stage = 1003, Vid = '" + Mid + "', Vid_EntryDate = '" + conn.ConvertDate(EDate) + "' Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And RefId = '" + sResult + "'";
                        conn.sExecuteQuery(sql);

                        sql = "UPDATE ALLVCR SET STAGE = 1003, VID = '" + Mid + "' WHERE BrCd = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And RefId = '" + sResult + "'";
                        conn.sExecuteQuery(sql);
                    }
                }
            }
            else if (PayMast == "CASHR")
            {
                //  Added by amol on 09/11/2017 for authorise cash receipt set only
                sql = "UPDATE " + TableName + " SET STAGE = 1003, VID = '" + Mid + "', F2 = '" + verifyMid + "' WHERE BrCd = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' And ScrollNo = '" + ScrollNo + "'";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    sql = "Update Avs_LnTrx Set Stage = 1003, Vid = '" + Mid + "', Vid_EntryDate = '" + conn.ConvertDate(EDate) + "' Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    conn.sExecuteQuery(sql);

                    sql = "UPDATE ALLVCR SET STAGE = 1003, VID = '" + Mid + "' WHERE BrCd = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' And ScrollNo = '" + ScrollNo + "'";
                    conn.sExecuteQuery(sql);

                    sql = "Select Count(*) From " + TableName + " WHERE BrCd = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' And Stage <> '1003'";
                    sResult = conn.sExecuteScalar(sql);

                    if (sResult == "1")
                    {
                        sql = "UPDATE " + TableName + " SET STAGE = 1003, VID = '" + Mid + "', F2 = '" + verifyMid + "' WHERE BrCd = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                        Result = conn.sExecuteQuery(sql);

                        sql = "UPDATE ALLVCR SET STAGE = 1003, VID = '" + Mid + "' WHERE BrCd = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                        conn.sExecuteQuery(sql);
                    }
                }
            }
            else
            {
                sql = "UPDATE " + TableName + " SET STAGE = 1003, VID = '" + Mid + "', F2 = '" + verifyMid + "' WHERE BrCd = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    sql = "Update Avs_LnTrx Set Stage = 1003, Vid = '" + Mid + "', Vid_EntryDate = '" + conn.ConvertDate(EDate) + "' Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    conn.sExecuteQuery(sql);

                    sql = "UPDATE ALLVCR SET STAGE = 1003, VID = '" + Mid + "' WHERE BrCd = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
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

    public int AuthoriseEntryMutli(string BrCode, string SetNo, string Mid, string EDate, string Scroll)
    {
        try
        {
            string[] TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            verifyMid = Ecry.GetMK(Mid.ToString());

            if (Result > 0)
            {
                AuthoAccEntryMutli1(BrCode, SetNo, Mid, EDate, Scroll);
                sql = "Update " + TableName + " SET STAGE = 1003, VID = '" + Mid + "', F2 = '" + verifyMid + "' WHERE SETNO = '" + SetNo + "' AND BrCd = '" + BrCode + "' AND  ENTRYDATE = '" + conn.ConvertDate(EDate) + "' and scrollNo='" + Scroll + "'";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    sql = "Update Avs_LnTrx Set Stage = 1003, Vid = '" + Mid + "', Vid_EntryDate = '" + conn.ConvertDate(EDate) + "' Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    conn.sExecuteQuery(sql);

                    sql = "Update ALLVCR SET STAGE = 1003, VID = '" + Mid + "' WHERE SETNO = '" + SetNo + "' AND BrCd = '" + BrCode + "' AND  ENTRYDATE = '" + conn.ConvertDate(EDate) + "'";
                    conn.sExecuteQuery(sql);
                }

                sql = "Select Count(*) From " + TableName + " Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' And Stage Not In (1003, 1004) ";
                sResult = conn.sExecuteScalar(sql);

                if (Convert.ToDouble(sResult) == 1)
                {
                    sql = "Update " + TableName + " SET STAGE = 1003, VID = '" + Mid + "', F2 = '" + verifyMid + "' Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
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
    public void AuthoAccEntryMutli1(string BrCode, string SetNo, string Mid, string EDate, string Scroll)
    {
        try
        {
            DataTable dt = new DataTable();
            string[] TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            verifyMid = Ecry.GetMK(Mid.ToString());

            if (Result > 0)
            {
                sql = "select * from " + TableName + " WHERE SETNO = '" + SetNo + "' AND BrCd = '" + BrCode + "' AND  ENTRYDATE = '" + conn.ConvertDate(EDate) + "' and scrollNo='" + Scroll + "'";
                dt = conn.GetDatatable(sql);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sql = "update avs_acc set stage='1003',pcmac='Direct',VID='" + Mid + "'  Where BrCd = '" + dt.Rows[i]["BRCD"].ToString() + "' AND SUBGLCODE='" + dt.Rows[i]["SUBGLCODE"].ToString() + "' and accno='" + dt.Rows[i]["ACCNO"].ToString() + "' and stage=1001";
                        conn.sExecuteQuery(sql);

                        sql = "update DepositInfo set stage='1003',pcmac='Direct',VID='" + Mid + "' where brcd='" + dt.Rows[i]["BRCD"].ToString() + "' and depositglcode='" + dt.Rows[i]["SUBGLCODE"].ToString() + "' and custaccno='" + dt.Rows[i]["ACCNO"].ToString() + "' and stage=1001";
                        conn.sExecuteQuery(sql);
                    }
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

    }
   

    public int AuthoriseEntryLoan(string BrCode, string SetNo, string Mid, string EDate)
    {
        try
        {
            string[] TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            verifyMid = Ecry.GetVK(Mid.ToString());

            sql = "UPDATE ALLVCR SET STAGE = 1003, VID = '" + Mid + "' WHERE SETNO = '" + SetNo + "' AND BrCd = '" + BrCode + "' AND  ENTRYDATE = '" + conn.ConvertDate(EDate) + "'";
            Result = conn.sExecuteQuery(sql);

            if (Result > 0)
            {
                sql = "UPDATE " + TableName + " SET STAGE = 1003, VID = '" + Mid + "', F2 = '" + verifyMid + "' WHERE SETNO = '" + SetNo + "' AND BrCd = '" + BrCode + "' AND  ENTRYDATE = '" + conn.ConvertDate(EDate) + "'";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    sql = "Update Avs_LnTrx Set Stage = 1003, Vid = '" + Mid + "', Vid_EntryDate = '" + conn.ConvertDate(EDate) + "' Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    Result = conn.sExecuteQuery(sql);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int AuthoriseEntryLoan1(string BrCode, string SetNo, string Mid, string EDate)
    {
        try
        {
            string Accno = "";
            string CustNo = "";
            string[] TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            verifyMid = Ecry.GetVK(Mid.ToString());

            sql = "UPDATE ALLVCR SET STAGE = 1003, VID = '" + Mid + "' WHERE SETNO = '" + SetNo + "' AND BrCd = '" + BrCode + "' AND  ENTRYDATE = '" + conn.ConvertDate(EDate) + "'";
            Result = conn.sExecuteQuery(sql);

            if (Result > 0)
            {
                sql = "Update Avs_LnTrx Set Stage = 1003, Vid = '" + Mid + "', Vid_EntryDate = '" + conn.ConvertDate(EDate) + "' Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                Result = conn.sExecuteQuery(sql);
                sql = "UPDATE " + TableName + " SET STAGE = 1003, VID = '" + Mid + "', F2 = '" + verifyMid + "' WHERE SETNO = '" + SetNo + "' AND BrCd = '" + BrCode + "' AND  ENTRYDATE = '" + conn.ConvertDate(EDate) + "'";
                Result = conn.sExecuteQuery(sql);
                sql = "select max(accno) from " + TableName + " WHERE SETNO = '" + SetNo + "' AND BrCd = '" + BrCode + "' AND  ENTRYDATE = '" + conn.ConvertDate(EDate) + "'";
                Accno = conn.sExecuteScalar(sql);
                sql = "select top(1) replace(custno,'.00','') from " + TableName + " WHERE SETNO = '" + SetNo + "' AND BrCd = '" + BrCode + "' AND  ENTRYDATE = '" + conn.ConvertDate(EDate) + "' and custno<>'0.00'";
                CustNo = conn.sExecuteScalar(sql);
                sql = "update avs1005 set stage=1003, APPSTATUS=4 where BRCD='" + BrCode + "' and setno=" + SetNo + " and custno='" + CustNo + "' and stage in (1002,1001) ";
                conn.sExecuteQuery(sql);
                sql = "update avs1003 set stage=1003, APPSTATUS=4 where BRCD='" + BrCode + "' and setno=" + SetNo + " and custno='" + CustNo + "' and stage in (1002,1001)";
                conn.sExecuteQuery(sql);
                sql = "update avs1004 set stage=1003, APPSTATUS=4 where BRCD='" + BrCode + "' and setno=" + SetNo + " and custno='" + CustNo + "' and stage in (1002,1001)";
                conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    #endregion

    #region Calcel Functions

    public int CancelEntry(string BrCode, string SetNo, string Mid, string EDate)
    {
        try
        {
            string[] TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            DeleteMid = Ecry.GetVK(Mid.ToString());

            sql = "UPDATE " + TableName + " SET STAGE = 1004, CID = '" + Mid + "', F3 = '" + DeleteMid + "' WHERE SETNO = '" + SetNo + "' AND BrCd = '" + BrCode + "' AND  ENTRYDATE = '" + conn.ConvertDate(EDate) + "'";
            Result = conn.sExecuteQuery(sql);

            if (Result > 0)
            {
                // Added by amol On 31/01/2018 for increase and decrease cash
                sql = "Update B Set B.No_Of_Notes = (Case When A.Inn = 0 Then B.No_Of_Notes + Out When A.Out = 0 Then B.No_Of_Notes - Inn End) " +
                      "From AVS5012 A " +
                      "Inner Join AVS5011 B With(NoLock) On A.BrCd = B.BrCd And A.CounterNo = B.V_TYPE And A.NoteType = B.Note_Type " +
                      "Where A.BrCd = '" + BrCode + "' And A.EffectDate = '" + conn.ConvertDate(EDate) + "' And A.SetNo = '" + SetNo + "' And A.Stage <> '1004'";
                conn.sExecuteQuery(sql);

                // Added by amol On 30/01/2018 for cancel cash denomination voucher
                sql = "Update avs5012 Set Stage = '1004' Where BrCd = '" + BrCode + "' And EffectDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                conn.sExecuteQuery(sql);

                sql = "UPDATE ALLVCR SET STAGE = 1004, CID = '" + Mid + "' WHERE SETNO = '" + SetNo + "' AND BrCd = '" + BrCode + "' AND  ENTRYDATE = '" + conn.ConvertDate(EDate) + "'";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int CancelEntryLoan(string BrCode, string PrCode, string AccNo, string SetNo, string EntryMid, string VerifyMid, string EDate)
    {
        try
        {
            string[] TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            DeleteMid = Ecry.GetVK(VerifyMid.ToString());

            //  Added by amol on 14092017 for cancel loan set only
            sql = "Select Top 1 RefId From " + TableName + " Where brcd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "' And Mid = '" + EntryMid + "'";
            sResult = conn.sExecuteScalar(sql);

            if (sResult != null && sResult != "")
            {
                sql = "UPDATE " + TableName + " SET STAGE = 1004, CID = '" + VerifyMid + "', F3 = '" + DeleteMid + "' WHERE BrCd = '" + BrCode + "' AND ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And RefId = '" + sResult + "' And Mid = '" + EntryMid + "'";
                Result = conn.sExecuteQuery(sql);

                if (Result > 0)
                {
                    // Added by amol On 31/01/2018 for increase and decrease cash
                    sql = "Update B Set B.No_Of_Notes = (Case When A.Inn = 0 Then B.No_Of_Notes + Out When A.Out = 0 Then B.No_Of_Notes - Inn End) " +
                          "From AVS5012 A " +
                          "Inner Join AVS5011 B With(NoLock) On A.BrCd = B.BrCd And A.CounterNo = B.V_TYPE And A.NoteType = B.Note_Type " +
                          "Where A.BrCd = '" + BrCode + "' And A.EffectDate = '" + conn.ConvertDate(EDate) + "' And A.SetNo = '" + SetNo + "' And A.Stage <> '1004'";
                    conn.sExecuteQuery(sql);

                    // Added by amol On 30/01/2018 for cancel cash denomination voucher
                    sql = "Update avs5012 Set Stage = '1004' Where BrCd = '" + BrCode + "' And EffectDate = '" + conn.ConvertDate(EDate) + "' And SetNo = '" + SetNo + "'";
                    conn.sExecuteQuery(sql);

                    sql = "Update Avs_LnTrx Set Stage = 1004, Vid = '" + VerifyMid + "', Vid_EntryDate = '" + conn.ConvertDate(EDate) + "' Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate) + "' And RefId = '" + sResult + "' And Mid = '" + EntryMid + "'";
                    Result = conn.sExecuteQuery(sql);

                    if (Result > 0)
                    {
                        sql = "Update LoanInfo Set LASTINTDATE = PREV_INTDT, MOD_DATE = '" + conn.ConvertDate(EDate).ToString() + "' Where BrCd= '" + BrCode + "' And LOANGLCODE = '" + PrCode + "' And CUSTACCNO = '" + AccNo + "'";
                        Result = conn.sExecuteQuery(sql);
                    }

                    //  Added by Amol on 11-10-2017 for suit file
                    if (Result > 0)
                    {
                        sql = "Select IsNull(Case When Acc_Status = '' Then '1' Else Acc_Status End, '1') From AVS5032 Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "'";
                        sResult = conn.sExecuteScalar(sql);

                        if (sResult == "" || sResult == null)
                            sResult = "1";

                        sql = "Update Avs_Acc Set ACC_STATUS = '" + sResult + "' Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "'";
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

    #endregion

    #region Other Functions
    public string GetExactAmount(string BRCD, string SETNO, string EDate)
    {
        string Amount = "0";
        string Int = "0";
        string Result = "0";
        try
        {
            string[] TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();
            sql = "Select sum(amount) from " + TableName + " where brcd='" + BRCD + "' and setno=" + SETNO + " and entrydate='" + conn.ConvertDate(EDate) + "' and trxtype=1 and activity=4";
            Amount = conn.sExecuteScalar(sql);
            sql = "Select sum(amount) from " + TableName + " where brcd='" + BRCD + "' and setno=" + SETNO + " and entrydate='" + conn.ConvertDate(EDate) + "' and trxtype=1 and activity=10";
            Int = conn.sExecuteScalar(sql);
            Result = (Convert.ToDouble(Amount) - Convert.ToDouble(Int)).ToString();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
    public DataTable GetPayMast(string BrCode, string SetNo, string Mid, string EDate)
    {
        try
        {
            string[] TD = EDate.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

            sql = "Select * From " + TableName + " WHERE SETNO = '" + SetNo + "' And RefBrCd = '" + BrCode + "' And ENTRYDATE = '" + conn.ConvertDate(EDate) + "' And PMTMODE like '%ABB%'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public DataTable GetPayMastForDaily(string BrCode, string SetNo, string Mid, string EDate)
    {
        try
        {
            string[] TD = EDate.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

            sql = "Select MID From " + TableName + " WHERE SETNO = '" + SetNo + "' And ENTRYDATE = '" + conn.ConvertDate(EDate) + "' and BRCD='" + BrCode + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public DataTable GetAmount(string BRCD, string SetNo, string EDATE)
    {
        try
        {
            string[] TD = EDATE.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "select AMOUNT_2,AID,SUBGLCODE,TRXTYPE,CONVERT(varchar(20),Amount) as Amount from " + TableName + " where SETNO = '" + SetNo + "' AND BrCd = '" + BRCD + "' AND  ENTRYDATE ='" + conn.ConvertDate(EDATE) + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public void UpdateAmount(string AID, string SubGLCODE, string TRXTYPE, string Amount, string Amount2, string SetNo, string BRCD, string EDAT)
    {
        try
        {
            string[] TD = EDAT.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "Update " + TableName + " set AMOUNT_2=" + Amount2 + " where SETNO = '" + SetNo + "' AND BrCd = '" + BRCD + "' AND  ENTRYDATE ='" + conn.ConvertDate(EDAT) + "' and AID='" + AID + "' and SUBGLCODE='" + SubGLCODE + "' and TRXTYPE='" + TRXTYPE + "' and Amount=" + Amount + "";
            conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public int GetSetMid(string BrCode, string EDate, string SetNo)
    {
        try
        {
            string[] TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "Select Distinct Mid From " + TableName + " Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int SendSMS(string AC, string CT, string BRCD, string BAL, string EDT, string AMT, string TP)
    {
        try
        {
            sql = "Exec SP_SENDSMS @SMS_DATE='" + conn.ConvertDate(EDT) + "',@SMS_TYPE=1,@ACCNO='" + AC + "',@BRCD='" + BRCD + "',@AMOUNT='" + AMT + "',@TP='" + TP + "',@CUSTNO='" + CT + "',@BAL='" + BAL + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string getglcode(string brcd, string subglcode)
    {
        sql = "select glcode from glmast where subglcode='" + subglcode + "' and brcd='" + brcd + "'";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }

    public void InvRenewal(string NAccNo, string AccNo, string Prd, string OD, string CD, string Period, string Rate, string Principle, string Maturity, string Edat, string IntType, string IntDate)
    {
        try
        {
            sql = "EXEC ISP_AVS0025 '" + NAccNo + "','" + AccNo + "','" + Prd + "','" + conn.ConvertDate(OD) + "','" + conn.ConvertDate(CD) + "','" + Period + "','" + Rate + "' ,'" + Principle + "' ,'" + Maturity + "' ,'" + conn.ConvertDate(Edat) + "','" + IntType + "','" + conn.ConvertDate(IntDate) + "'";
            conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }

    //Added  by amol on 26-09-2017 as per darade sir instruction
    public int UpdateCashSet(string BrCode, string EDate, string SetNo, string Amount, string Mid)
    {
        try
        {
            string[] TD = EDate.ToString().Split('/');
            TableName = "AVSM_" + TD[2].ToString() + "" + TD[1].ToString();
            EntryMid = Ecry.GetMK(Mid.ToString());

            sql = "Update " + TableName + " Set Amount = Amount + '" + Convert.ToDouble(Amount) + "', F1 = '" + EntryMid + "' Where BrCd = '" + BrCode + "' " +
                  "And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And GlCode = '99' And SubGlCode = '99' And SetNo = '" + SetNo + "' And TrxType = '2'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    //Dhanya Shetty //03/10/2017/to check Lot passing mid
    public DataTable GetSetMidL(string Flag, string EDate, string FSetNo, string TSetNo, string BrCode, string Mid)
    {
        try
        {
            sql = "Exec Isp_AVS0002 @Flag='" + Flag + "',@Edt='" + conn.ConvertDate(EDate) + "',@FSetno='" + FSetNo + "',@TSetno='" + TSetNo + "',@Mid='" + Mid + "',@Brcd='" + BrCode + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public int UpdtLotSt(string Flag, string EDate, string FSetNo, string TSetNo, string BrCode, string Mid)
    {
        try
        {
            sql = "Exec Isp_AVS0002 @Flag='" + Flag + "',@Edt='" + conn.ConvertDate(EDate) + "',@FSetno='" + FSetNo + "',@TSetno='" + TSetNo + "',@Mid='" + Mid + "',@Brcd='" + BrCode + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }

    public void UpdateInvMaster(string AccNo, string ProdCode)
    {
        try
        {
            sql = "Exec UpdateInvMaster @ACCNO='" + AccNo + "',@ProdCode='" + ProdCode + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    #endregion

}