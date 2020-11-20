using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsInsertTrans
{
    ClsEncryptValue Ecry = new ClsEncryptValue();
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string EntryMid, verifyMid, DeleteMid = "";
    string ResBrCode, TableName = "";
    string sResult, SCROLLNO, sql = "";
    int Result;

    public ClsInsertTrans()
    {

    }

    //For Normal Module Only
    public int Authorized(string ENTRYDATE, string POSTINGDATE, string FUNDINGDATE, string GLCODE, string SUBGLCODE, string ACCNO, string PARTICULARS, string PARTICULARS2,
        double AMOUNT, string TRXTYPE, string ACTIVITY, string PMTMODE, string SETNO, string INSTRUMENTNO, string INSTRUMENTDATE, string INSTBANKCD, string INSTBRCD,
        string STAGE, string RTIME, string BRCD, string MID, string PrCode_AccNo, string VID, string PAYMAST, string CUSTNO, string CUSTNAME, string REFID)
    {
        try
        {

            double CR, DR;
            string[] TD = ENTRYDATE.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

            CR = DR = 0;
            if (TRXTYPE == "1")
            {
                CR = Convert.ToDouble(AMOUNT);
                DR = 0;
            }
            else if (TRXTYPE == "2")
            {
                DR = Convert.ToDouble(AMOUNT);
                CR = 0;
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

            if (Convert.ToDouble(AMOUNT) > 0)
            {
                sql = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                    "VALUES('" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "','" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "','" + PARTICULARS2 + "','" + CR + "','" + DR + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "','" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "','0','" + VID + "','" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + REFID + "',GETDATE())";
                Result = conn.sExecuteQuery(sql);

                sql = "INSERT INTO " + TableName + " (AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT, AMOUNT_1,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO, INSTRUMENTNO, INSTRUMENTDATE, INSTBANKCD, INSTBRCD, STAGE, RTIME, BRCD, MID, CID, VID, PCMAC, PAYMAST, CUSTNO, CUSTNAME, REFID, F1, SYSTEMDATE) " +
                   " VALUES ('1','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(POSTINGDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "', '" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "', '" + PARTICULARS2 + "','" + AMOUNT + "', '" + PrCode_AccNo + "','" + TRXTYPE + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "', '" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "','0','" + VID + "','" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + REFID + "', '" + EntryMid + "', GETDATE())";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }






    //For CBS2  28




    //For Normal Module Only
    public int Authorized(string ENTRYDATE, string POSTINGDATE, string FUNDINGDATE, string GLCODE, string SUBGLCODE, string ACCNO, string PARTICULARS, string PARTICULARS2,
        double AMOUNT, string TRXTYPE, string ACTIVITY, string PMTMODE, string SETNO, string INSTRUMENTNO, string INSTRUMENTDATE, string INSTBANKCD, string INSTBRCD,
        string STAGE, string RTIME, string BRCD, string MID, string PrCode_AccNo, string RefBrCode, string PAYMAST, string CUSTNO, string CUSTNAME, string REFID, string RECSRNO = "0")
    {
        try
        {

            double CR, DR;
            string[] TD = ENTRYDATE.Replace("12:00:00 AM", "").Split('/');
            if (GLCODE == "5")
            {

                TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

                CR = DR = 0;
                if (TRXTYPE == "1")
                {
                    CR = Convert.ToDouble(AMOUNT);
                    DR = 0;
                }
                else if (TRXTYPE == "2")
                {
                    DR = Convert.ToDouble(AMOUNT);
                    CR = 0;
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

                if (Convert.ToDouble(AMOUNT) > 0)
                {
                    sql = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                          "VALUES('" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "','" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "','" + PARTICULARS2 + "','" + CR + "','" + DR + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "','" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "', '0', '0','" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + REFID + "',GETDATE())";
                    Result = conn.sExecuteQuery(sql);

                    sql = "INSERT INTO " + TableName + " (RECSRNO,AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT, AMOUNT_1,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO, INSTRUMENTNO, INSTRUMENTDATE, INSTBANKCD, INSTBRCD, STAGE, RTIME, BRCD, MID, CID, VID, PCMAC, PAYMAST, CUSTNO, CUSTNAME, RefBrCd, REFID, F1, SYSTEMDATE) " +
                          " VALUES ('" + RECSRNO + "','1','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(POSTINGDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "', '" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "', '" + PARTICULARS2 + "','" + AMOUNT + "', '" + PrCode_AccNo + "','" + TRXTYPE + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "', '" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "', '" + MID + "','0', '0','" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "', '" + RefBrCode + "', '" + REFID + "', '" + EntryMid + "', GETDATE())";
                    Result = conn.sExecuteQuery(sql);
                }
            }
            else
            {
                TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

                CR = DR = 0;
                if (TRXTYPE == "1")
                {
                    CR = Convert.ToDouble(AMOUNT);
                    DR = 0;
                }
                else if (TRXTYPE == "2")
                {
                    DR = Convert.ToDouble(AMOUNT);
                    CR = 0;
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

                if (Convert.ToDouble(AMOUNT) > 0)
                {
                    sql = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                        "VALUES('" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "','" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "','" + PARTICULARS2 + "','" + CR + "','" + DR + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "','" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "', '0', '0','" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + REFID + "',GETDATE())";
                    Result = conn.sExecuteQuery(sql);

                    sql = "INSERT INTO " + TableName + " (AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT, AMOUNT_1,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO, INSTRUMENTNO, INSTRUMENTDATE, INSTBANKCD, INSTBRCD, STAGE, RTIME, BRCD, MID, CID, VID, PCMAC, PAYMAST, CUSTNO, CUSTNAME, RefBrCd, REFID, F1, SYSTEMDATE) " +
                       " VALUES ('1','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(POSTINGDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "', '" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "', '" + PARTICULARS2 + "','" + AMOUNT + "', '" + PrCode_AccNo + "','" + TRXTYPE + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "', '" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "', '0', '0','" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "', '" + RefBrCode + "', '" + REFID + "', '" + EntryMid + "', GETDATE())";
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






    //For ABB Module Only
    public int AddMTable(string ENTRYDATE, string POSTINGDATE, string FUNDINGDATE, string GLCODE, string SUBGLCODE, string ACCNO, string PARTICULARS, string PARTICULARS2,
        double AMOUNT, string TRXTYPE, string ACTIVITY, string PMTMODE, string SETNO, string INSTRUMENTNO, string INSTRUMENTDATE, string INSTBANKCD, string INSTBRCD, string STAGE,
        string RTIME, string BRCD, string MID, string PrCode_AccNo, string VID, string PAYMAST, string CUSTNO, string CUSTNAME, string REFID, string RefBrCode, string OrgBrCode)
    {
        try
        {

            double CR, DR;
            string[] TD = ENTRYDATE.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

            CR = DR = 0;
            if (TRXTYPE == "1")
            {
                CR = Convert.ToDouble(AMOUNT);
                DR = 0;
            }
            else if (TRXTYPE == "2")
            {
                DR = Convert.ToDouble(AMOUNT);
                CR = 0;
            }
            if (ACCNO == "")
                ACCNO = "0";
            if (CUSTNO == "")
                CUSTNO = "0";

            if (Convert.ToDouble(SETNO) < 20000)
                sql = "Select IsNull(Max(ScrollNo), 0) + 1 From AllVcr Where BrCd = '" + BRCD + "' And EntryDate = '" + conn.ConvertDate(ENTRYDATE) + "' And SetNo = '" + SETNO + "'";
            else
                sql = "Select IsNull(Max(ScrollNo), 0) + 1 From AllVcr Where EntryDate = '" + conn.ConvertDate(ENTRYDATE) + "' And SetNo = '" + SETNO + "'";
            SCROLLNO = conn.sExecuteScalar(sql);

            REFID = REFID.ToString() == "" ? "0" : REFID;
            EntryMid = Ecry.GetMK(MID.ToString());

            if (Convert.ToDouble(AMOUNT) > 0)
            {
                sql = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, RefBrCd, OrgBrCd, SYSTEMDATE) " +
                "VALUES('" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "','" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "','" + PARTICULARS2 + "','" + CR + "','" + DR + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "','" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "','0','" + VID + "','" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + REFID + "', '" + RefBrCode + "', '" + OrgBrCode + "', GETDATE())";
                Result = conn.sExecuteQuery(sql);

                sql = "INSERT INTO " + TableName + " (AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT, AMOUNT_1,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD, MID, CID, VID, PCMAC, PAYMAST, CUSTNO, CUSTNAME, REFID, RefBrCd, OrgBrCd, F1, SYSTEMDATE) " +
                   " VALUES ('1','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(POSTINGDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "', '" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "', '" + PARTICULARS2 + "','" + AMOUNT + "', '" + PrCode_AccNo + "','" + TRXTYPE + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "', '" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "','0','" + VID + "','" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + REFID + "', '" + RefBrCode + "', '" + OrgBrCode + "', '" + EntryMid + "', GETDATE())";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int LoanTrx(string BRCD, string LoanGlCode, string SUBGLCODE, string ACCNO, string HeadDesc, string TRXTYPE, string Activity, string PARTICULARS,
        string AMOUNT, string SETNO, string Stage, string MID, string VID, string ENTRYDATE, string REFID)
    {
        try
        {
            if (ACCNO == "")
                ACCNO = "0";
            if (Convert.ToDouble(SETNO) < 20000)
                sql = "Select IsNull(Max(ScrollNo), 0) + 1 From AllVcr Where BrCd = '" + BRCD + "' And EntryDate = '" + conn.ConvertDate(ENTRYDATE) + "' And SetNo = '" + SETNO + "'";
            else
                sql = "Select IsNull(Max(ScrollNo), 0) + 1 From AllVcr Where EntryDate = '" + conn.ConvertDate(ENTRYDATE) + "' And SetNo = '" + SETNO + "'";
            SCROLLNO = conn.sExecuteScalar(sql);

            if (Convert.ToDouble(AMOUNT) > 0)
            {
                sql = "Insert Into AVS_LnTrx (BRCD, LoanGlCode, SubGlCode, AccountNo, HeadDesc, TrxType, Activity, Narration, Amount, SetNo, Stage, ScrollNo, MID, MID_EntryDate, PCMAC, EntryDate, RefId, SystemDate)" +
                "VALUES('" + BRCD + "','" + LoanGlCode + "','" + SUBGLCODE + "','" + ACCNO + "', '" + HeadDesc + "','" + TRXTYPE + "', '" + Activity + "', '" + PARTICULARS + "', '" + AMOUNT + "','" + SETNO + "', '" + Stage + "','" + SCROLLNO + "', '" + MID + "', '" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "', '" + conn.PCNAME().ToString() + "', '" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "', '" + REFID + "', GETDATE())";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    //End ABB Module

    //For IBT Module Only
    public int InsertMTable(string ENTRYDATE, string POSTINGDATE, string FUNDINGDATE, string GLCODE, string SUBGLCODE, string ACCNO, string PARTICULARS, string PARTICULARS2,
        double AMOUNT, string TRXTYPE, string ACTIVITY, string PMTMODE, string SETNO, string INSTRUMENTNO, string INSTRUMENTDATE, string INSTBANKCD, string INSTBRCD, string STAGE,
        string RTIME, string BRCD, string MID, string PrCode_AccNo, string VID, string PAYMAST, string CUSTNO, string CUSTNAME, string RefBrCode, string OrgBrCode, string ResBrCode, string REFID)
    {
        try
        {

            double CR, DR;
            string[] TD = ENTRYDATE.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

            CR = DR = 0;
            if (TRXTYPE == "1")
            {
                CR = Convert.ToDouble(AMOUNT);
                DR = 0;
            }
            else if (TRXTYPE == "2")
            {
                DR = Convert.ToDouble(AMOUNT);
                CR = 0;
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

            if (Convert.ToDouble(AMOUNT) > 0)
            {
                sql = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME, RefBrcd, OrgBrCd, ResBrCd, REFID, SYSTEMDATE) " +
                    " VALUES('" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "','" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "','" + PARTICULARS2 + "','" + CR + "','" + DR + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "','" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "','0','" + VID + "','" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + RefBrCode + "', '" + OrgBrCode + "', '" + ResBrCode + "', '" + REFID + "', GETDATE())";
                Result = conn.sExecuteQuery(sql);

                sql = "INSERT INTO " + TableName + " (AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT, AMOUNT_1,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD, MID, CID, VID, PCMAC, PAYMAST, CUSTNO, CUSTNAME, RefBrcd, OrgBrCd, ResBrCd, REFID, F1, SYSTEMDATE) " +
                     " VALUES ('1','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(POSTINGDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "', '" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "', '" + PARTICULARS2 + "','" + AMOUNT + "', '" + PrCode_AccNo + "','" + TRXTYPE + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "', '" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "','0','" + VID + "','" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + RefBrCode + "', '" + OrgBrCode + "', '" + ResBrCode + "', '" + REFID + "', '" + EntryMid + "', GETDATE())";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int InsertLoanTrx(string BRCD, string LoanGlCode, string SUBGLCODE, string ACCNO, string HeadDesc, string TRXTYPE, string Activity, string PARTICULARS,
        string AMOUNT, string SETNO, string Stage, string MID, string VID, string ENTRYDATE, string REFID)
    {
        try
        {
            if (Convert.ToDouble(SETNO) < 20000)
                sql = "Select IsNull(Max(ScrollNo), 0) + 1 From AllVcr Where BrCd = '" + BRCD + "' And EntryDate = '" + conn.ConvertDate(ENTRYDATE) + "' And SetNo = '" + SETNO + "'";
            else
                sql = "Select IsNull(Max(ScrollNo), 0) + 1 From AllVcr Where EntryDate = '" + conn.ConvertDate(ENTRYDATE) + "' And SetNo = '" + SETNO + "'";
            SCROLLNO = conn.sExecuteScalar(sql);

            if (Convert.ToDouble(AMOUNT) > 0)
            {
                sql = "Insert Into AVS_LnTrx (BRCD, LoanGlCode, SubGlCode, AccountNo, HeadDesc, TrxType, Activity, Narration, Amount, SetNo, Stage, ScrollNo, MID, MID_EntryDate, PCMAC, EntryDate, RefId, SystemDate)" +
                "VALUES('" + BRCD + "','" + LoanGlCode + "','" + SUBGLCODE + "','" + ACCNO + "', '" + HeadDesc + "','" + TRXTYPE + "', '" + Activity + "', '" + PARTICULARS + "', '" + AMOUNT + "','" + SETNO + "', '" + Stage + "','" + SCROLLNO + "', '" + MID + "', '" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "', '" + conn.PCNAME().ToString() + "', '" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "', '" + REFID + "', GETDATE())";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int InsertIBTTable(string BRCD, string ENTRYDATE, string GLCODE, string SUBGLCODE, string ACCNO, string PARTICULARS, string PARTICULARS2,
        double AMOUNT, string TRXTYPE, string ACTIVITY, string PMTMODE, string SETNO, string INSTRUMENTNO, string INSTRUMENTDATE, string STAGE,
        string MID, string VID, string PAYMAST, string CUSTNO, string CUSTNAME, string RefBrCode, string OrgBrCd, string ResBrCd, string REFID)
    {
        try
        {
            //if (TRXTYPE == "1")
            //    ResBrCode = CheckAdminHead(SUBGLCODE);
            //else if(TRXTYPE == "2")
            //    ResBrCode = ResBrCd;

            sql = "Insert Into InterBranchTrans(BRCD, EntryDate, GLCODE, SUBGLCODE, ACCNO, PARTICULARS, PARTICULARS2, AMOUNT, TRXTYPE, ACTIVITY, PMTMODE, SETNO, SCROLLNO, " +
                  "INSTRUMENTNO, INSTRUMENTDATE, STAGE, MID, VID, PCMAC, PAYMAST, CUSTNO, CUSTNAME, RefBrcd, OrgBrCd, ResBrCd, RefId, TokenNo, SystemDate) " +
                  "VALUES ('" + BRCD + "', '" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "', '" + GLCODE + "', '" + SUBGLCODE + "', '" + ACCNO + "', '" + PARTICULARS + "', " +
                  "'" + PARTICULARS2 + "', '" + AMOUNT + "', '" + TRXTYPE + "', '" + ACTIVITY + "', '" + PMTMODE + "', '" + SETNO + "', '0', '" + INSTRUMENTNO + "', " +
                  "'" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "', '" + STAGE + "', '" + MID + "', '" + VID + "', '" + conn.PCNAME().ToString() + "', " +
                  "'" + PAYMAST + "', '" + CUSTNO + "', '" + CUSTNAME + "', '" + RefBrCode + "', '" + OrgBrCd + "', '" + ResBrCd + "', '" + REFID + "', '0', GETDATE())";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    //End IBT Module

    //For Insert Temporary records Only
    public int InsertIntoTable(string BrCode, string CustNo, string GlCode, string SubGlCode, string AccNo, string CustName, string Amount, string TrxType, string Activity, string PmtMode, string SetNo, string Perticulars, string Perticulars2, string ChkNo, string ChkDate, string EDate, string Mid)
    {
        try
        {
            if (GlCode != "99" && SubGlCode != "99")
            {
                sql = "Insert Into Avs_TempMultiTransfer (BrCd, CustNo, CustName, GlCode, SubGlCode, AccNo, Particulars,Particulars2, Amount, TrxType, Activity, PmtMode, SetNo, RefId, InstNo, InstDate, EntryDate, SystemDate, Mid) " +
                      "VALUES ('" + BrCode + "','" + CustNo + "','" + CustName + "', '" + GlCode + "','" + SubGlCode + "','" + AccNo + "','" + Perticulars + "','" + Perticulars2 + "','" + Amount + "','" + TrxType + "','" + Activity + "','" + PmtMode + "', '" + SetNo + "','0','" + ChkNo + "','" + ChkDate + "', '" + conn.ConvertDate(EDate).ToString() + "', GetDate(), '" + Mid + "')";
                Result = conn.sExecuteQuery(sql);
            }
            else
            {
                sql = "Select 1 From Avs_TempMultiTransfer Where BrCd = '" + BrCode + "' And GlCode = '99' And SubGlCode = '99' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' And TrxType = '" + TrxType + "'";
                sResult = conn.sExecuteScalar(sql);

                if (sResult == "" || sResult == null)
                {
                    sql = "Insert Into Avs_TempMultiTransfer (BrCd, CustNo, CustName, GlCode, SubGlCode, AccNo, Particulars, Particulars2, Amount, TrxType, Activity, PmtMode, SetNo, RefId, InstNo, InstDate, EntryDate, SystemDate, Mid) " +
                          "VALUES ('" + BrCode + "','" + CustNo + "','" + CustName + "', '" + GlCode + "','" + SubGlCode + "','" + AccNo + "','" + Perticulars + "','" + Perticulars2 + "','" + Amount + "','" + TrxType + "','" + Activity + "','" + PmtMode + "', '" + SetNo + "','0','" + ChkNo + "','" + ChkDate + "', '" + conn.ConvertDate(EDate).ToString() + "', GetDate(), '" + Mid + "')";
                    Result = conn.sExecuteQuery(sql);
                }
                else
                {
                    sql = "Update Avs_TempMultiTransfer Set Amount = Amount + '" + Convert.ToDouble(Amount) + "' Where BrCd = '" + BrCode + "' And GlCode = '99' And SubGlCode = '99' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' And TrxType = '" + TrxType + "'";
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

    public int TempSaction(string ENTRYDATE, string POSTINGDATE, string FUNDINGDATE, string GLCODE, string SUBGLCODE, string ACCNO, string PARTICULARS, string PARTICULARS2,
        double AMOUNT, string TRXTYPE, string ACTIVITY, string PMTMODE, string SETNO, string INSTRUMENTNO, string INSTRUMENTDATE, string INSTBANKCD, string INSTBRCD,
        string STAGE, string RTIME, string BRCD, string MID, string PrCode_AccNo, string VID, string PAYMAST, string CUSTNO, string CUSTNAME, string REFID)
    {
        try
        {
            if (ACCNO == "")
                ACCNO = "0";

            REFID = REFID.ToString() == "" ? "0" : REFID;
            if (Convert.ToDouble(AMOUNT) > 0)
            {
                sql = "INSERT INTO TempMTable (AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT, AMOUNT_1,TRXTYPE,ACTIVITY, PMTMODE, SETNO, INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                    " VALUES ('1','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(POSTINGDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "', '" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "', '" + PARTICULARS2 + "','" + AMOUNT + "', '" + PrCode_AccNo + "','" + TRXTYPE + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "', '" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "','0','" + VID + "','" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + REFID + "',GETDATE())";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int TempLoanTrx(string BRCD, string LoanGlCode, string SUBGLCODE, string ACCNO, string HeadDesc, string TRXTYPE, string Activity, string PARTICULARS,
        string AMOUNT, string SETNO, string MID, string VID, string ENTRYDATE)
    {
        try
        {

            string CR = "", DR = "";

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

            if (Convert.ToDouble(AMOUNT) > 0)
            {
                sql = "Insert Into TempLnTrx (BRCD, LoanGlCode, SubGlCode, AccountNo, HeadDesc, TrxType, Activity, Narration, Amount, SetNo, Stage, MID, MID_EntryDate, PCMAC, EntryDate, SystemDate)" +
                "VALUES('" + BRCD + "','" + LoanGlCode + "','" + SUBGLCODE + "','" + ACCNO + "', '" + HeadDesc + "','" + TRXTYPE + "', '" + Activity + "','" + PARTICULARS + "', '" + AMOUNT + "','" + SETNO + "','1001', '" + MID + "', '" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "', '" + conn.PCNAME().ToString() + "', '" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "', GETDATE())";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public void DelAllRecTable(string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Delete From TempMTable Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "'" +
                  "Delete From TempLnTrx Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public DataTable GetAllTransDetails(string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "SELECT BRCD, ENTRYDATE, POSTINGDATE, FUNDINGDATE, GLCODE, SUBGLCODE, ACCNO, PARTICULARS, PARTICULARS2, AMOUNT, AMOUNT_1, TRXTYPE, ACTIVITY, PMTMODE, SETNO, " +
                "SCROLLNO, INSTRUMENTNO, INSTRUMENTDATE, INSTBANKCD, INSTBRCD, STAGE, RTIME, MID, CID, VID, PCMAC, PAYMAST, CUSTNO, CUSTNAME, RefBrcd, RecPrint, REFID, SYSTEMDATE, Amount_2 " +
                "From TempMTable Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' Order By SystemDate";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetAllLnTransDetails(string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Select BRCD, LoanGlCode, SubGlCode, AccountNo, HeadDesc, TrxType, Activity,  Narration, Amount, SetNo, Stage, MID, MID_EntryDate, PCMAC, EntryDate From TempLnTrx Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' Order By SystemDate";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int CloseLoanAcc(string BRCD, string SubglCode, string AccNo, string EDate, string Mid)
    {
        try
        {
            sql = "Update LoanInfo Set LmStatus = 99, Prev_IntDt = LastIntDate, LastIntDate = '" + conn.ConvertDate(EDate).ToString() + "', Stage = 1003, Vid = '" + Mid + "', Mod_Date = '" + conn.ConvertDate(EDate).ToString() + "' Where BRCD = '" + BRCD + "' AND LOANGLCODE = '" + SubglCode + "' AND CUSTACCNO = '" + AccNo + "'";
            Result = conn.sExecuteQuery(sql);

            if (Convert.ToInt32(Result) > 0)
            {
                sql = "Update Avs_Acc Set Acc_status = 3, Stage = 1003, LastIntDt = '" + conn.ConvertDate(EDate).ToString() + "', CLOSINGDATE = '" + conn.ConvertDate(EDate).ToString() + "', Vid = '" + Mid + "' Where BRCD = '" + BRCD + "' AND SUBGLCODE = '" + SubglCode + "' AND ACCNO = '" + AccNo + "'";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public void UpdateStatus(string BRCD, string ProdCode, string AccNo)
    {
        try
        {
            sql = "update avs_acc set ACC_STATUS=3 where brcd='" + BRCD + "' and SUBGLCODE='" + ProdCode + "' and ACCNO='" + AccNo + "' and stage<>1004";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public string CheckAdminHead(string SubGlCode)
    {
        try
        {
            sql = "Select BRCD From BankName Where BRCD <> 0 And ADMSubGlCode = '" + SubGlCode + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
}