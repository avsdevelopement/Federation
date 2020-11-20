using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsDividentPayTran
{
    ClsEncryptValue Ecry = new ClsEncryptValue();
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataTable Dt = new DataTable();
    string sql = "", sResult = "";
    string TableName = "";
    string EntryMid = "";
    string ScrollNo = "";
    int Result = 0;
    int Result1 = 0;
    public string BRCD { get; set; }
    public DropDownList Ddl { get; set; }
    public string RECCODE { get; set; }
    public string RECDIV { get; set; }

    public ClsDividentPayTran()
    {

    }

    public DataTable GetReport(DataTable DT)
    {
        return DT;
    }
    public string GetBranchName(string brcd)
    {
        try
        {
            sql = "select MIDNAME from BANKNAME where BRCD='" + brcd + "'";
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

    public DataTable BindData(string BrCode, string PrCode, string EDate, string sFlag, string Division, string DepMart, string IntRec, string MID)
    {
        try
        {
            sql = "Exec Sp_DivPayTrans '" + BrCode + "', '" + PrCode + "', '" + conn.ConvertDate(EDate) + "', '" + sFlag + "', '" + Division + "', '" + DepMart + "', '" + IntRec + "', '" + MID + "' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int InsertData(string BrCode, string PrCode, string EDate, string SetNo, string sFlag, string Mid)
    {
        try
        {
            sql = "Exec ISP_AVS0188 @BrCode='" + BrCode + "', @PrCode='" + PrCode + "', @EDate='" + conn.ConvertDate(EDate) + "', @SetNo='" + SetNo + "',@Flag='Calc', @sFlag='" + sFlag + "', @Mid='" + Mid + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public DataTable DrInsertData(string BrCode, string PrCode, string EDate, string SetNo, string sFlag, string Mid)
    {
        try
        {
            sql = "Exec ISP_AVS0188 @BrCode='" + BrCode + "', @PrCode='" + PrCode + "', @EDate='" + conn.ConvertDate(EDate) + "', @SetNo='" + SetNo + "',@Flag='Total', @sFlag='" + sFlag + "', @Mid='" + Mid + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public int InsertData_DR(string BrCode, string PrCode, string EDate, string SetNo, string sFlag, string Mid, string DrAmount)
    {
        try
        {
            sql = "Exec ISP_AVS0188 @BrCode='" + BrCode + "', @PrCode='" + PrCode + "', @EDate='" + conn.ConvertDate(EDate) + "', @SetNo='" + SetNo + "',@Flag='DrAmount', @sFlag='" + sFlag + "', @Mid='" + Mid + "', @DrAmount='" + DrAmount + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int Authorized(string BRCD, string ENTRYDATE, string GLCODE, string SUBGLCODE, string ACCNO, string PARTICULARS, string PARTICULARS2, string AMOUNT, string TRXTYPE,
        string ACTIVITY, string PMTMODE, string SETNO, string STAGE, string MID, string CID, string VID, string PAYMAST, string CUSTNO, string CUSTNAME, string REFID)
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
            ScrollNo = conn.sExecuteScalar(sql);

            REFID = REFID.ToString() == "" ? "0" : REFID;
            EntryMid = Ecry.GetMK(MID.ToString());

            sql = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO, STAGE,RTIME, BRCD,MID, PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, SYSTEMDATE) " +
                "VALUES('" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "','" + PARTICULARS + "','" + CR + "','" + DR + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "','" + ScrollNo + "', '" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "', '" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + REFID + "',GETDATE())";
            Result = conn.sExecuteQuery(sql);

            sql = "INSERT INTO " + TableName + " (AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO, STAGE,RTIME, BRCD, MID,VID, PCMAC,PAYMAST, CUSTNO, CUSTNAME, REFID, F1, RefBrcd, OrgBrCd, SystemDate) " +
               " VALUES ('1','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "', '" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "', '" + PARTICULARS2 + "','" + AMOUNT + "','" + TRXTYPE + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "', '" + ScrollNo + "', '" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "','" + VID + "', '" + conn.PCNAME().ToString() + "', '" + PAYMAST + "', '" + CUSTNO + "', '" + CUSTNAME + "', '" + REFID + "', '" + EntryMid + "', '" + CID + "', '" + VID + "', GetDate())";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string DivToOtherAccData(string BrCode, string PrCode, string CrPrCode, string EDate, string Mid, string sFlag)
    {
        try
        {
            sql = "Exec RptDividentTRFProcess @Brcd = '" + BrCode + "', @TSGlCode = '" + PrCode + "', @PTDT = '" + conn.ConvertDate(EDate) + "', @TrfGlCode='" + CrPrCode + "', @Flag='" + sFlag + "', @MID= '" + Mid + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public DataTable DTDivTRFProGet(string FDate, string BRCD, string PRDCD, string TPRCD, string MID)
    {
        DataTable Dt = new DataTable();
        try
        {
            sql = "Exec RptDividentTRFProcess @Brcd = '" + BRCD + "', @TSGlCode = '" + PRDCD + "', @PTDT = '" + conn.ConvertDate(FDate) + "', @TrfGlCode='" + TPRCD + "', @Flag='Trail', @MID= '" + MID + "'";
            Dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
}