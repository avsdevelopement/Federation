using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ClsAvs_AllField
/// </summary>
public class ClsAvs_AllField
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", Gl = "";
    string TableName = "";
    int Result = 0;

    public ClsAvs_AllField()
    {

    }

    public int GetInfo(GridView gd, string FL, string EDT, string ACCNO, string BRCD, string subgl, string setno)
    {
        try
        {
            sql = "EXEC SP_Avs_AllField @FLAG='" + FL + "',@EDATE='" + conn.ConvertDate(EDT).ToString() + "',@ACCNO='" + ACCNO + "',@BRCD='" + BRCD + "',@subglcode='" + subgl + "',@setno='" + setno + "'";

            Result = conn.sBindGrid(gd, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable VoucherTotal(string BrCode, string EDate, string SetNo)
    {
        try
        {
            string[] TD = EDate.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

            sql = "Select Abs(Sum(Case When TrxType = '2' Then Amount End)) As TotalDr, Abs(Sum(Case When TrxType = '1' Then Amount End)) As TotalCr " +
                "From " + TableName + " Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' "+
                "And Setno = '" + SetNo + "' And PmtMode Not In ('TR_INT', 'TR-INT')";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetInfoid(string FL, string BrCode, string EDate, string SetNo, string ScrollNo)
    {
        try
        {
            string[] TD = EDate.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

            sql = "Select ConVert(VarChar(10), EntryDate, 103) As EDate, ConVert(VarChar(10), InstrumentDate, 103) As InstDate, * From " + TableName + " Where BRCD = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' And ScrollNo = '" + ScrollNo + "'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int Modify(string FL, string EDT, string SETNO, string BRCD, string ACCNO, string SUBGLCODE, string CUSTNO, string PARTICULARS, string ACTIVITY, string PMTMODE, string SCROLLNO, string AMOUNT, string TRXTYPE, string ScrollNo)
    {
        try
        {
            string[] TD = EDT.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

            sql = "Update " + TableName + " Set SUBGLCODE = '" + SUBGLCODE + "', ACCNO = '" + ACCNO + "', CUSTNO= '" + CUSTNO + "', PARTICULARS = '" + PARTICULARS + "', PARTICULARS2 = 'MOD', " +
                "ACTIVITY = '" + ACTIVITY + "', PMTMODE = '" + PMTMODE + "', AMOUNT = ConVert(Numeric(18, 2), '" + AMOUNT + "'), TRXTYPE = '" + TRXTYPE + "' " +
                "Where BRCD = '" + BRCD + "' And ENTRYDATE = '" + conn.ConvertDate(EDT).ToString() + "' And SETNO = '" + SETNO + "' And ScrollNo = '" + ScrollNo + "'";
            Result = conn.sExecuteQuery(sql);

            if (Result > 0)
            {
                sql = "Update AVS_LnTrx SET AMOUNT = ConVert(Numeric(18, 2), '" + AMOUNT + "') " +
                     "Where BRCD = '" + BRCD + "' and ENTRYDATE = '" + conn.ConvertDate(EDT).ToString() + "' and SETNO = '" + SETNO + "' " +
                     "And SubGlCode = '" + SUBGLCODE + "' And AccountNo = '" + ACCNO + "' And TrxType = '1'";
                conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int Insertdata(string AID, string ENTRYDATE, string POSTINGDATE, string FUNDINGDATE, string GLCODE, string SUBGLCODE, string ACCNO, string PARTICULARS,
       string PARTICULARS2, string AMOUNT, string AMOUNT_1, string AMOUNT_2, string TRXTYPE, string ACTIVITY, string PMTMODE, string SETNO, string INSTRUMENTNO, string INSTRUMENTDATE,
       string INSTBANKCD, string INSTBRCD, string STAGE, string RTIME, string BRCD, string MID, string CID, string VID, string PCMAC, string PAYMAST, string CUSTNO,
       string CUSTNAME, string RefBrcd, string RecPrint, string OrgBrCd, string ResBrCd, string REFID, string Token, string Ref_Agent)
    {
        try
        {
            string[] TD = ENTRYDATE.Replace("12:00:00 AM", "").Split('/');
            string TBNAME = "";
            TBNAME = TD[2].ToString().Trim() + TD[1].ToString().Trim();

            sql = "SELECT ISNULL(MAX(SCROLLNO),0)+1 FROM ALLVCR WHERE BRCD='" + BRCD + "' AND SETNO='" + SETNO + "' AND ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "'";
            string SCROLLNO = conn.sExecuteScalar(sql);

            sql = "INSERT INTO AVSM_" + TBNAME.Replace("AM", "").Trim() + "(AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT, " +
                  "AMOUNT_1,AMOUNT_2,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO, " +
                  "CUSTNAME,RefBrcd,RecPrint,OrgBrCd,ResBrCd,REFID, SYSTEMDATE) " +
                  "VALUES ('" + AID + "','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(POSTINGDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "', " +
                  "'" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "', '" + PARTICULARS2 + "','" + AMOUNT + "','" + AMOUNT_1 + "', null , " +
                  "'" + TRXTYPE + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "', '" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "', " +
                  "'" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "', GETDATE(), '" + BRCD + "','" + MID + "','" + CID + "','" + VID + "','" + PCMAC + "','" + PAYMAST + "', " +
                  "'" + CUSTNO + "','" + CUSTNAME + "','" + RefBrcd + "','" + RecPrint + "','" + OrgBrCd + "','" + ResBrCd + "', '" + REFID + "',GETDATE())";
            Result = conn.sExecuteQuery(sql);


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int InstLN(string BRCD, string LoanGlCode, string SUBGLCODE, string ACCNO, string HeadDesc, string TRXTYPE, string Activity, string PARTICULARS,
       string AMOUNT, string SETNO, string Stage, string Scrollno, string MID, string MID_EntryDate, string PCMAC, string ENTRYDATE, string REFID, string OldGL, string VID, string VID_EntryDate)
    {
        try
        {
            sql = "Insert Into AVS_LnTrx (BRCD, LoanGlCode, SubGlCode, AccountNo, HeadDesc, TrxType, Activity, Narration, Amount, SetNo, Stage, ScrollNo, MID, MID_EntryDate, PCMAC, EntryDate, RefId, SystemDate,OldGL,VID,VID_EntryDate) " +
                  "VALUES('" + BRCD + "','" + LoanGlCode + "','" + SUBGLCODE + "','" + ACCNO + "', '" + HeadDesc + "','" + TRXTYPE + "', '" + Activity + "', '" + PARTICULARS + "', '" + AMOUNT + "','" + SETNO + "', '" + Stage + "', " +
                  "'" + Scrollno + "', '" + MID + "', '" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "', '" + PCMAC + "', '" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "', '" + REFID + "', GETDATE(),'" + OldGL + "','" + VID + "','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int BindDetails(GridView gd, string FG, string glcode, string BRCD, string subgl, string ACCNO)
    {
        try
        {
            sql = "EXEC SP_Avs_AllField @GFG='" + FG + "',@Glcode='" + glcode + "',@BRCD='" + BRCD + "',@subglcode='" + subgl + "',   @ACCNO='" + ACCNO + "'";
            Result = conn.sBindGrid(gd, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int Openstatus(string BRCD, string subgl, string ACCNO)
    {
        try
        {
            sql = "update avs_acc set ACC_STATUS=1 where brcd='" + BRCD + "' and subglcode='" + subgl + "' and  accno='" + ACCNO + "'  and stage<>1004 ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int Closestatus(string BRCD, string subgl, string ACCNO)
    {
        try
        {
            sql = "update avs_acc set ACC_STATUS=3  where brcd='" + BRCD + "' and subglcode='" + subgl + "' and  accno='" + ACCNO + "'  and stage<>1004 ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int OpenLMstatus(string BRCD, string subgl, string ACCNO)
    {
        try
        {
            sql = "update DEPOSITINFO set LMSTATUS=1 where brcd='" + BRCD + "' and DEPOSITGLCODE='" + subgl + "' and  CUSTACCNO='" + ACCNO + "'  and stage<>1004 ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int CloseLMstatus(string BRCD, string subgl, string ACCNO)
    {
        try
        {
            sql = "update DEPOSITINFO set LMSTATUS=99 where brcd='" + BRCD + "' and DEPOSITGLCODE='" + subgl + "' and  CUSTACCNO='" + ACCNO + "'  and stage<>1004 ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int OpenLLMstatus(string BRCD, string subgl, string ACCNO)
    {
        try
        {
            sql = "update LOANINFO set LMSTATUS=1 where brcd='" + BRCD + "' and LOANGLCODE='" + subgl + "' and  CUSTACCNO='" + ACCNO + "'  and stage<>1004 ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int CloseLLMstatus(string BRCD, string subgl, string ACCNO)
    {
        try
        {
            sql = "update LOANINFO set LMSTATUS=99 where brcd='" + BRCD + "' and LOANGLCODE='" + subgl + "' and  CUSTACCNO='" + ACCNO + "'  and stage<>1004 ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int LastIntdtDep(string BRCD, string subgl, string ACCNO, string Ldate)
    {
        try
        {
            sql = "update avs_acc set LASTINTDT='" + conn.ConvertDate(Ldate).ToString() + "'  where brcd='" + BRCD + "' and subglcode='" + subgl + "' and  accno='" + ACCNO + "'  and stage<>1004 ";
            Result = conn.sExecuteQuery(sql);
            if (Result > 0)
            {
                sql = "update DEPOSITINFO set LASTINTDATE='" + conn.ConvertDate(Ldate).ToString() + "'  where brcd='" + BRCD + "' and DEPOSITGLCODE='" + subgl + "' and  CUSTACCNO='" + ACCNO + "'  and stage<>1004 ";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int LastIntdtLoan(string BRCD, string subgl, string ACCNO, string Ldate)
    {
        try
        {
            sql = "update avs_acc set LASTINTDT='" + conn.ConvertDate(Ldate).ToString() + "'  where brcd='" + BRCD + "' and subglcode='" + subgl + "' and  accno='" + ACCNO + "'  and stage<>1004 ";
            Result = conn.sExecuteQuery(sql);
            if (Result > 0)
            {
                sql = "update LOANINFO set LASTINTDATE='" + conn.ConvertDate(Ldate).ToString() + "'  where brcd='" + BRCD + "' and LOANGLCODE='" + subgl + "' and  CUSTACCNO='" + ACCNO + "'  and stage<>1004 ";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int LastIntdt(string BRCD, string subgl, string ACCNO, string Ldate)
    {
        try
        {
            sql = "update avs_acc set LASTINTDT='" + conn.ConvertDate(Ldate).ToString() + "'  where brcd='" + BRCD + "' and subglcode='" + subgl + "' and  accno='" + ACCNO + "'  and stage<>1004 ";

            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string dispglcode(string BRCD, string PRD)
    {
        try
        {
            sql = "select glcode from glmast where brcd='" + BRCD + "' and subglcode='" + PRD + "'";
            Gl = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return Gl;
    }
    public string Getcustname(string BRCD, string CNO)
    {
        try
        {
            sql = "select custname from master where brcd='" + BRCD + "' and custno='" + CNO + "'";
            Gl = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return Gl;
    }
    //public int Modify(string FL, string EDT, string SETNO, string BRCD, string ACCNO, string SUBGLCODE, string CUSTNO, string PARTICULARS, string ACTIVITY, string PMTMODE, string SCROLLNO, string AMOUNT, string TRXTYPE)
    //{
    //    try
    //    {
    //        sql = "EXEC SP_Avs_AllField @FLAG='" + FL + "',@EDATE='" + conn.ConvertDate(EDT).ToString() + "',@SETNO='" + SETNO + "',@BRCD='" + BRCD + "',@ACCNO='" + ACCNO + "',@SUBGLCODE='" + SUBGLCODE + "',@CUSTNO='" + CUSTNO + "',@PARTICULARS='" + PARTICULARS + "',@ACTIVITY='" + ACTIVITY + "',@PMTMODE='" + PMTMODE + "',@SCROLLNO='" + SCROLLNO + "',@AMOUNT='" + AMOUNT + "',@TRXTYPE='" + TRXTYPE + "'";

    //        Result = conn.sExecuteQuery(sql);
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //    return Result;

    //}
    public DataTable Getrelateddata(string FL, string EDT, string SETNO, string BRCD, string ACCNO, string SUBGLCODE, string TRXTYPE)
    {
        try
        {
            sql = "EXEC SP_Avs_AllField @FLAG='" + FL + "',@EDATE='" + conn.ConvertDate(EDT).ToString() + "',@SETNO='" + SETNO + "',@BRCD='" + BRCD + "',@ACCNO='" + ACCNO + "',@SUBGLCODE='" + SUBGLCODE + "',@TRXTYPE='" + TRXTYPE + "'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    
    public string Getscrollno(string BRCD, string SETNO, string ENTRYDATE)
    {
        string SCROLLNO = "";
        try
        {
            sql = "SELECT ISNULL(MAX(SCROLLNO),0)+1 FROM ALLVCR WHERE BRCD='" + BRCD + "' AND SETNO='" + SETNO + "' AND ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "'";
            SCROLLNO = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return SCROLLNO;
    }

    public DataTable Getlndet(string FL, string EDT, string SETNO, string BRCD, string ACCNO, string SUBGLCODE, string TRXTYPE)
    {
        try
        {
            sql = "EXEC SP_Avs_AllField @FLAG='" + FL + "',@EDATE='" + conn.ConvertDate(EDT).ToString() + "',@SETNO='" + SETNO + "',@BRCD='" + BRCD + "',@ACCNO='" + ACCNO + "',@SUBGLCODE='" + SUBGLCODE + "',@TRXTYPE='" + TRXTYPE + "'";
            DT = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}