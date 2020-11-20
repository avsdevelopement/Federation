using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

public class ClsCashPayment
{
    string sql, sResult, sqlc, sqld;
    string TableName = "";
    int Result = 0;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

    public ClsCashPayment()
    {

    }

    public int InsertNewSetNo(string Entrydate, string GLCODE, string SUBGLCODE, string ACCNO, string PARTICULARS, string AMOUNT, string SETNO, string PARTICULARS2, string BRCD, string MID, string PCMAC, string CUSTNO, string CUSTNAME)
    {
        int result = 0;
        try
        {
            sqlc = "INSERT INTO ALLVCR (ENTRYDATE, POSTINGDATE, FUNDINGDATE, GLCODE, SUBGLCODE, ACCNO, PARTICULARS, CREDIT, DEBIT, ACTIVITY, PMTMODE, SETNO, SCROLLNO, PARTICULARS2, STAGE, BRCD, MID, PCMAC, CUSTNO, CUSTNAME, SYSTEMDATE ) VALUES ('" + (Entrydate) + "','DD-MM-YYYY'), '" + (Entrydate) + "','DD-MM-YYYY'), '" + (Entrydate) + "','DD-MM-YYYY'), '" + GLCODE + "', '" + SUBGLCODE + "', '" + ACCNO + "', '" + PARTICULARS + "', '0', '" + AMOUNT + "', '4', 'CASH P','" + SETNO + "','1','" + PARTICULARS2 + "','1001','" + BRCD + "','" + MID + "','" + PCMAC + "','" + CUSTNO + "', '" + CUSTNAME + "', SYSDATE))";
            sqld = "INSERT INTO ALLVCR (ENTRYDATE, POSTINGDATE, FUNDINGDATE, GLCODE, SUBGLCODE, ACCNO, PARTICULARS, CREDIT, DEBIT, ACTIVITY, PMTMODE, SETNO, SCROLLNO, PARTICULARS2, STAGE, BRCD, MID, PCMAC, CUSTNO, CUSTNAME, SYSTEMDATE ) VALUES ('" + (Entrydate) + "','DD-MM-YYYY'), '" + (Entrydate) + "','DD-MM-YYYY'), '" + (Entrydate) + "','DD-MM-YYYY'), '99', '0', '99', '" + PARTICULARS + "',  '" + AMOUNT + "','" + '0' + "', '4', 'CASH P','" + SETNO + "','1','" + PARTICULARS2 + "','1001','" + BRCD + "','" + MID + "','" + PCMAC + "','" + CUSTNO + "', '" + CUSTNAME + "', SYSDATE))";
            string sqlu = "update AVS1000 SET LASTNO='" + SETNO + "' WHERE  TYPE='CASH-P' AND BRCD='" + BRCD + "'"; //BRCD ADDED --Abhishek

            result = conn.sExecuteQuery(sqlc);
            int result1 = conn.sExecuteQuery(sqld);
            int result2 = conn.sExecuteQuery(sqlu);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public string getsetno(string BRCD)
    {
        string setnumb = "";
        try
        {
            sql = "select LASTNO from avs1000 where TYPE='CASH-P' AND BRCD='" + BRCD + "' AND  Activityno='4'";
            setnumb = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return (Convert.ToInt32(setnumb) + 1).ToString();
    }

    public string checkDenom(string BRCD, string setno, string EDate)
    {
        try
        {
            setno = conn.sExecuteScalar("SELECT SETNO FROM AVS5012 WHERE BRCD='" + BRCD + "' and SETNO='" + setno + "' AND EFFECTDATE = '" + conn.ConvertDate(EDate).ToString() + "'");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return setno = null;
        }

        return setno;
    }

    public string getGlCode(string BRCD, string subglcode)
    {
        string glcode = "";
        try
        {

            string sql = "SELECT GLCODE FROM GLMAST WHERE  SUBGLCODE='" + subglcode + "' and BRCD='" + BRCD + "'";
            glcode = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return glcode;
    }

    public string GetAccountNo(string AccNo, string SubGlCode, string brcd)
    {
        string Accname = "";
        try
        {
            string sql = "SELECT B.CUSTNO FROM AVS_ACC A Inner Join MASTER B ON A.CUSTNO = B.CUSTNO WHERE A.ACCNO='" + AccNo + "' AND A.SUBGLCODE='" + SubGlCode + "' AND A.BRCD='" + brcd + "'";
            Accname = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Accname;
    }

    // Cancel / Delete Cash receipt
    public int CancelCashreceipt(string setno, string brcd)
    {
        int result = 0;
        try
        {
            string sql = "update ALLVCR SET STAGE = '1004' WHERE SETNO='" + setno + "' AND BRCD='" + brcd + "'";
            int Accname = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public int UpdateSet(string setno, string BRCD)
    {
        try
        {
            sql = "UPDATE AVS1000 SET LASTNO='" + setno + "'  where TYPE='CASH-P' AND BRCD='" + BRCD + "' AND  Activityno='4'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int Getinfotable(GridView Gview, string smid, string sbrcd, string EDT, string paymst)
    {
        try
        {
            string[] TD = EDT.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

            sql = "Select ConVert(VarChar(10),isnull(a.SETNO,'0'))+'_'+ConVert(VarChar(10),isnull(A.Amount,'0'))+'_'+ConVert(VarChar(10),isnull(ACC.SUBGLCODE,'0'))+'_'+ConVert(VarChar(10),isnull(a.ACCNO,'0')) Dens, " +
                  "A.SETNO SETNO, ACC.SUBGLCODE AT, A.ACCNO ACNO, M.CUSTNAME CUSTNAME, A.Amount, A.PARTICULARS PARTICULARS,A.INSTRUMENTNO,A.INSTRUMENTDATE, UM.USERNAME MAKER "+
                  "From " + TableName + " A Left Join UserMaster UM ON UM.PERMISSIONNO=A.MID " +
                  "Left Join Avs_Acc ACC ON ACC.ACCNO=A.ACCNO AND ACC.BRCD = A.BRCD AND A.SUBGLCODE=ACC.SUBGLCODE "+
                  "Left Join Master M ON M.CUSTNO=ACC.CUSTNO " +
                  "Where A.BRCD='" + sbrcd + "' AND A.STAGE = '1001' AND A.TrxType <> '1' AND A.ACTIVITY='4' AND A.ENTRYDATE = '" + conn.ConvertDate(EDT).ToString() + "' and A.PAYMAST='" + paymst + "' " + 
                  "Order By A.SETNO,A.SCROLLNO ";

            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int CancelCashpayment(string setno, string brcd, string edt)
    {
        int result = 0;
        try
        {
            string[] tbname = edt.Split('/');
            string avsm = "AVSM_" + tbname[2].ToString() + tbname[1].ToString() + "";
            string avsb = "AVSB_" + tbname[2].ToString() + tbname[1].ToString() + "";

            string sql = "update ALLVCR SET STAGE = '1004' WHERE SETNO='" + setno + "' AND BRCD='" + brcd + "' and EntryDate='" + conn.ConvertDate(edt) + "'";
            int result1 = conn.sExecuteQuery(sql);
            string sql1 = "update " + avsm + " SET STAGE = '1004' WHERE SETNO='" + setno + "' AND BRCD='" + brcd + "' and EntryDate='" + conn.ConvertDate(edt) + "'";
            int result2 = conn.sExecuteQuery(sql1);

            if (result1 > 0 || result2 > 0)
            {
                result = 1;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public DataTable GetAccStatDetails(string PFMONTH, string PTMONTH, string PFYEAR, string PTYEAR, string PFDT, string PTDT, string PAC, string PAT, string BRCD)
    {
        try
        {
            DT = new DataTable();

            if (Convert.ToInt32(PFYEAR.ToString()) < 2000)
            {
                PFYEAR = "2000";
                string[] aa = @PFDT.ToString().Split('/');
                @PFDT = aa[0].ToString() + '/' + aa[1].ToString() + '/' + PFYEAR;
            }

            sql = "Exec SP_ACCSTATUS_R @pfmonth='" + PFMONTH + "',@ptmonth='" + PTMONTH + "',@PFDT='" + conn.ConvertDate(PFDT) + "',@PTDT='" + conn.ConvertDate(PTDT) + "',@pfyear='" + PFYEAR + "',@ptyear='" + PTYEAR + "',@pac='" + PAC + "',@pat='" + PAT + "', @BRCD='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetAccStatReport(string PFMONTH, string PTMONTH, string PFYEAR, string PTYEAR, string PFDT, string PTDT, string PAC, string PAT, string BRCD)
    {
        try
        {
            DT = new DataTable();

            if (Convert.ToInt32(PFYEAR.ToString()) < 2000)
            {
                PFYEAR = "2000";
                string[] aa = @PFDT.ToString().Split('/');
                @PFDT = aa[0].ToString() + '/' + aa[1].ToString() + '/' + PFYEAR;
            }

            sql = "Exec SP_ACCSTATUS_Report @pfmonth='" + PFMONTH + "',@ptmonth='" + PTMONTH + "',@PFDT='" + conn.ConvertDate(PFDT) + "',@PTDT='" + conn.ConvertDate(PTDT) + "',@pfyear='" + PFYEAR + "',@ptyear='" + PTYEAR + "',@pac='" + PAC + "',@pat='" + PAT + "', @BRCD='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public string GetFinStartDate(string EDate)
    {
        try
        {
            DT = new DataTable();

            sql = "Exec SP_GetCurrFinStartDate '" + conn.ConvertDate(EDate).ToString() + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataTable getVouchtype(string brcd, string setno, string accno)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "SELECT particulars2,instrumentno FROM AVSM_201705 WHERE BRCD= '" + brcd + "' AND SETNO='" + setno + "' AND ACCNO='" + accno + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public DataTable GetAccDetails(string BrCode, string CustNo)
    {
        DT = new DataTable();
        try
        {
            sql = "Exec sGetAccDetails '" + BrCode + "','" + CustNo + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public string GetTotalAmt(string brcd, string setno, string edate)
    {
        string amt = "";
        try
        {
            string[] tbname = edate.Split('/');
            string avsm = "AVSM_" + tbname[2].ToString() + tbname[1].ToString() + "";
            sql = "select SUM(AMOUNT) AMOUNT from " + avsm + " where SETNO='" + setno + "' and PMTMODE not in ('TR-INT','TR_INT') AND BRCD='" + brcd + "' AND TRXTYPE=2 AND ENTRYDATE='" + conn.ConvertDate(edate) + "'";
            amt = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return amt;
    }
}