using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsAVS5074
/// </summary>
public class ClsAVS5074
{
    DbConnection conn = new DbConnection();
    ClsEncryptValue Ecry = new ClsEncryptValue();
    string sql = "";
    string Result = "";
    string TableName = "";
    int result = 0;
    public ClsAVS5074()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string GetCustName(string Custno, string BRCD)
    {
        try
        {
            sql = "select Custname from master where custno='" + Custno + "'";
            Result = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
    public string GetBRCD(string BRCD)
    {
        try
        {
            sql = "select MidName from bankname where brcd='" + BRCD + "'";
            Result = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
    public int CheckAccNo(string Subglcode, string BRCD, string accno, string custno)
    {
        int Result = 0;
        try
        {
            sql = "select AccNo from avs_acc where subglcode='" + Subglcode + "' and accno='" + accno + "' and custno ='" + custno + "' and brcd='" + BRCD + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
    public DataTable GetAccDetails(string BRCD, string Custno, string EDate, string FLAG)
    {
        DataTable Dt = new DataTable();
        try
        {
            sql = "Exec SP_CustDashDetails '" + BRCD + "','" + Custno + "','" + conn.ConvertDate(EDate).ToString() + "','" + FLAG + "'";
            Dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetLoanAccDetails(string BRCD, string Custno, string EDate, string FLAG)
    {
        DataTable Dt = new DataTable();
        try
        {
            sql = "Exec SP_CustDashDetails '" + BRCD + "','" + Custno + "','" + conn.ConvertDate(EDate).ToString() + "','" + FLAG + "'";
            Dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }

    public int getAccNo(string Subglcode, string BRCD)
    {
        int Result = 0;
        try
        {
            sql = "select LastNo+1 from glmast where subglcode='" + Subglcode + "' and brcd='" + BRCD + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
            sql = "update glmast set LastNo=Lastno+1 where subglcode='" + Subglcode + "' and brcd='" + BRCD + "'";
            conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }

    public string GetNewAcc(string custno, string accno, string subglcode, string BRCD)
    {
        string Accno = "";
        try
        {
            sql = "select convert(nvarchar(20),accno)+'_'+convert(nvarchar(20),subglcode) from avs_acc where accno=" + accno + " and glcode=" + subglcode + " and custno=" + custno + " and brcd=" + BRCD + "";
            Accno = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Accno;
    }

    public string getBal(string custno, string accno, string subglcode, string BRCD, string EntryDate)
    {
        string Bal = "";
        try
        {
            string[] Table = EntryDate.Split('/');
            sql = "select abs(sum(amount)) from( " +
                  "select sum(case when trxtype=2 then -1*amount else amount end) as amount from avsb_" + Table[2].ToString() + Table[1].ToString() + " where stage=1003 and brcd=" + BRCD + " and subglcode=" + subglcode + " and accno=" + accno + " and entrydate<'" + conn.ConvertDate(EntryDate) + "' " +
                  "union all " +
                  "select sum(case when trxtype=2 then -1*amount else amount end)as amount from avsm_" + Table[2].ToString() + Table[1].ToString() + " where stage=1003 and brcd=" + BRCD + " and subglcode=" + subglcode + " and accno=" + accno + " and entrydate='" + conn.ConvertDate(EntryDate) + "') as a";
            Bal = conn.sExecuteScalar(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Bal;
    }

    public int UpdateAVSAcc(string Subglcode, string OldAccNo, string NewAccNo, string BRCD, string CustNo, string TOBRCD, string MID, string Flag, string Date)
    {
        int Result = 0;
        try
        {
            sql = "exec ISP_AVS0114 @SUBGLCODE='" + Subglcode + "',@OLDACNO='" + OldAccNo + "',@NEWACNO='" + NewAccNo + "',@OLDBRCD='" + BRCD + "',@CUSTNO='" + CustNo + "',@TOBRCD='" + TOBRCD + "',@MID='" + MID + "',@FLAG='" + Flag + "',@DATE='" + conn.ConvertDate(Date) + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Result;
    }
    public DataTable GetSurity(string BRCD, string Custno, string edate)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "Exec SP_SURITYINFO '" + BRCD + "','" + Custno + "','" + conn.ConvertDate(edate) + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }
    public int AddMTable(string ENTRYDATE, string POSTINGDATE, string FUNDINGDATE, string GLCODE, string SUBGLCODE, string ACCNO, string PARTICULARS, string PARTICULARS2,
       double AMOUNT, string TRXTYPE, string ACTIVITY, string PMTMODE, string SETNO, string INSTRUMENTNO, string INSTRUMENTDATE, string INSTBANKCD, string INSTBRCD, string STAGE,
       string RTIME, string BRCD, string MID, string PrCode_AccNo, string VID, string PAYMAST, string CUSTNO, string CUSTNAME, string REFID, string RefBrCode, string OrgBrCode, string RecSrNo)
    {
        string TableName = "";
        string SCROLLNO = "";
        int Result = 0;
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
            string EntryMid = "";
            REFID = REFID.ToString() == "" ? "0" : REFID;
            EntryMid = Ecry.GetMK(MID.ToString());

            if (Convert.ToDouble(AMOUNT) > 0)
            {
                sql = "INSERT INTO ALLVCR (ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,CREDIT,DEBIT,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD,MID,CID,VID,PCMAC,PAYMAST,CUSTNO,CUSTNAME,REFID, RefBrCd, OrgBrCd, SYSTEMDATE,RecSrNo) " +
                "VALUES('" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "','" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "','" + PARTICULARS2 + "','" + CR + "','" + DR + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "','" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "','0','" + VID + "','" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + REFID + "', '" + RefBrCode + "', '" + OrgBrCode + "', GETDATE(), '" + RecSrNo + "')";
                Result = conn.sExecuteQuery(sql);

                sql = "INSERT INTO " + TableName + " (AID,ENTRYDATE,POSTINGDATE, FUNDINGDATE, GLCODE,SUBGLCODE,ACCNO,PARTICULARS, PARTICULARS2,AMOUNT, AMOUNT_1,TRXTYPE,ACTIVITY, PMTMODE, SETNO, SCROLLNO,INSTRUMENTNO, INSTRUMENTDATE,INSTBANKCD, INSTBRCD,STAGE,RTIME, BRCD, MID, CID, VID, PCMAC, PAYMAST, CUSTNO, CUSTNAME, REFID, RefBrCd, OrgBrCd, F1, SYSTEMDATE,RecSrNo) " +
                   " VALUES ('1','" + conn.ConvertDate(ENTRYDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(POSTINGDATE.Replace("12:00:00", "")) + "','" + conn.ConvertDate(FUNDINGDATE.Replace("12:00:00", "")) + "', '" + GLCODE + "','" + SUBGLCODE + "','" + ACCNO + "','" + PARTICULARS + "', '" + PARTICULARS2 + "','" + AMOUNT + "', '" + PrCode_AccNo + "','" + TRXTYPE + "','" + ACTIVITY + "','" + PMTMODE + "','" + SETNO + "', '" + SCROLLNO + "','" + INSTRUMENTNO + "','" + conn.ConvertDate(INSTRUMENTDATE.Replace("12:00:00", "")) + "','" + INSTBANKCD + "','" + INSTBRCD + "','" + STAGE + "',getdate(), '" + BRCD + "','" + MID + "','0','" + VID + "','" + conn.PCNAME().ToString() + "','" + PAYMAST + "','" + CUSTNO + "','" + CUSTNAME + "','" + REFID + "', '" + RefBrCode + "', '" + OrgBrCode + "', '" + EntryMid + "', GETDATE(), '" + RecSrNo + "')";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
   
    
    public DataTable GetADMSubGl(string BrCode)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select ADMGlCode, ADMSubGlCode From BankName Where BrCd = '" + BrCode + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public string GetCashGl(string GL, string BRCD)
    {
        sql = "Select Subglcode from glmast where glcode='" + GL + "' and brcd='" + BRCD + "'";
        BRCD = conn.sExecuteScalar(sql);
        return BRCD;
    }
    public string getGlCode(string subglcode, string brcd)
    {
        string GL = "";
        try
        {
            sql = "select glcode from glmast where brcd='" + brcd + "' and subglcode='" + subglcode + "'";
            GL = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return GL;
    }

    public string getIR(string subglcode, string brcd)
    {
        string GL = "";
        try
        {
            sql = "select IR from glmast where brcd='" + brcd + "' and subglcode='" + subglcode + "'";
            GL = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return GL;
    }

    public string CheckPara(string para)
    {
        string Para = "";
        try
        {
            sql = "select listvalue from PARAMETER where LISTFIELD='SHRALLOT'";
            Para = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Para;
    }
    public int GetByVoucherGridAdmin_Spe(string brcd, string mid, GridView grdv, string sn, string EDT)
    {
        try
        {
            string[] TD; TD = EDT.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "SELECT (ConVert(VarChar(10), AM.SetNo)+'_'+ConVert(VarChar(10), AM.ScrollNo)+'_'+ConVert(VarChar(10), AM.MID)) As ScrollNo, AM.SETNO, " +
                  " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                  " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                  " ,MM.CUSTNAME,AM.PARTICULARS, " +
                  " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                  " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                  " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                  " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                  " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                  " LEFT  JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                  " Left  JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " + //Changed Inner join to Left for User master -- Abhsihek
                  " WHERE AM.SETNO='" + sn + "'AND AM.STAGE NOT IN('1004') AND AM.ENTRYDATE='" + conn.ConvertDate(EDT) + "' " +
                  " And AM.SetNo > 20000 And AM.GlCode <> '99' AND AM.ACTIVITY  NOT IN (31,32)  AND AM.PAYMAST='Cust_TR' " +
                  " ORDER BY AM.SETNO,AM.SCROLLNO ";
            result = conn.sBindGrid(grdv, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public int Getinfotable_All(GridView Gview, string smid, string sbrcd, string EDT)
    {
        try
        {
            string[] TD; TD = EDT.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "SELECT (ConVert(VarChar(10), AM.SetNo)+'_'+ConVert(VarChar(10), AM.ScrollNo)+'_'+ConVert(VarChar(10), AM.MID)) As ScrollNo, AM.SETNO, " +
                      " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                      " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                      " ,MM.CUSTNAME,AM.PARTICULARS, " +
                      " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                      " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                      " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                      " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                      " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                      " LEFT JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                      " INNER JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " +
                      " WHERE  AM.STAGE NOT IN( '1003','1004') AND AM.ENTRYDATE='" + conn.ConvertDate(EDT) + "' And SetNo > 20000 AND AM.GLCODE<>99 AND AM.ACTIVITY  NOT IN (31,32) AND AM.PAYMAST='Cust_TR' " +
                      " ORDER BY AM.SETNO,AM.SCROLLNO ";
            result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public int GetinfotableAdmin_All(GridView Gview, string smid, string sbrcd, string EDT)
    {
        try
        {

            string[] TD; TD = EDT.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "SELECT (ConVert(VarChar(10), AM.SetNo)+'_'+ConVert(VarChar(10), AM.ScrollNo)+'_'+ConVert(VarChar(10), AM.MID)) As ScrollNo, AM.SETNO, " +
                  " (CASE WHEN AC.SUBGLCODE IS NULL THEN GG.SUBGLCODE ELSE AC.SUBGLCODE END) SUBGLCODE " +
                  " ,GG.GLNAME,(CASE WHEN AC.ACCNO IS NULL THEN '0' ELSE AC.ACCNO END) ACCNO " +
                  " ,MM.CUSTNAME,AM.PARTICULARS, " +
                  " (CASE WHEN AM.TRXTYPE=1 THEN AM.AMOUNT ELSE '0' END) CREDIT, " +
                  " (CASE WHEN AM.TRXTYPE=2 THEN AM.AMOUNT ELSE '0' END) DEBIT, " +
                  " AM.MID,CONVERT(VARCHAR(11),AM.ENTRYDATE) ENTRYDATE,UM.LOGINCODE FROM " + TableName + " AM " +
                  " LEFT JOIN GLMAST GG ON GG.SUBGLCODE=AM.SUBGLCODE AND AM.BRCD=GG.BRCD AND AM.GLCODE=GG.GLCODE " +
                  " LEFT JOIN MASTER MM ON MM.CUSTNO=AM.CUSTNO " +
                  " LEFT JOIN AVS_ACC AC ON AM.SUBGLCODE=AC.SUBGLCODE AND AM.ACCNO=AC.ACCNO AND AM.BRCD=AC.BRCD " +
                  " INNER JOIN USERMASTER UM ON CONVERT(VARCHAR(16),UM.PERMISSIONNO)=AM.MID AND UM.BRCD=AM.BRCD " +
                  " WHERE AM.STAGE NOT IN('1004') AND AM.ENTRYDATE='" + conn.ConvertDate(EDT) + "' And SetNo > 20000 AND AM.GLCODE<>99 AND AM.ACTIVITY  NOT IN (31,32)  AND AM.PAYMAST='Cust_TR'" +
                  " ORDER BY AM.SETNO,AM.SCROLLNO ";
            result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public string GetPAYMAST(string BRCD, string EDT, string SETNO)
    {
        try
        {
            sql = "EXEC SP_VOUCHER_AUTHOPROCESS @FLAG='GET_PAYMAST',@SETNO='" + SETNO + "',@EDT='" + conn.ConvertDate(EDT) + "',@BRCD='" + BRCD + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public DataTable GetDetails_ToFill(string SETNO, string BRCD, string EDT, string FL, string ScrollNo)
    {
        DataTable DT = new DataTable();
        try
        {
            string[] TD = EDT.Split('/');
            string TBNAME = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "SELECT M.CUSTNO, A.Setno,A.Scrollno,A.SUBGLCODE,A.ACCNO,A.Particulars,AMOUNT AMT ,A.ENTRYDATE,A.PCMAC,U.LOGINCODE MID,M.CUSTNAME,GL.GLNAME,A.SUBGLCODE FROM " + TBNAME + " A " +
                  " LEFT JOIN AVS_ACC AC ON AC.ACCNO=A.ACCNO AND AC.SUBGLCODE=A.SUBGLCODE AND AC.BRCD=A.BRCD " +
                  " LEFT JOIN MASTER M ON M.CUSTNO=AC.CUSTNO " +
                  " LEFT JOIN USERMASTER U ON A.MID=U.Permissionno AND A.BRCD=U.BRCD " +
                  " LEFT JOIN GLMAST GL ON A.GLCODE=GL.GLCODE AND A.SUBGLCODE=GL.SUBGLCODE AND A.BRCD=GL.BRCD WHERE A.SETNO='" + SETNO + "' AND A.ENTRYDATE='" + conn.ConvertDate(EDT) + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    public string CheckStage(string setno, string EDT, string BRCD)
    {
        string RC = "";
        try
        {
            string[] TD; TD = EDT.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            string sql = "select Stage from " + TableName + " where SETNO='" + setno + "' and EntryDate='" + conn.ConvertDate(EDT) + "' and STAGE<>1004 and BRCD='" + BRCD + "'";
            RC = conn.sExecuteScalar(sql);
            return RC;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return RC;
        }
    }
    public int GetSetMid(string BrCode, string EDate, string SetNo)
    {
        try
        {
            string[] TD; TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "Select Distinct Mid From " + TableName + " Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "'";
            result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public int CancelPost(string BrCode, string EDate, string SetNo, string Mid, string CustNo, string EMID)
    {
        try
        {
            string FrmBRCD = "";
            string ToBRCD = "";
            string[] TD;
            TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();
            sql = "update " + TableName + " set Stage=1004, VID='" + Mid + "' where setno='" + SetNo + "' and entrydate='" + conn.ConvertDate(EDate) + "' ";
            result = conn.sExecuteQuery(sql);
            if (result > 0)
            {
                sql = "select top(1) TrFrm from avs_acc where custno='" + CustNo + "' and TrfDate='" + conn.ConvertDate(EDate) + "'";
                FrmBRCD = conn.sExecuteScalar(sql);
                sql = "select top(1) TrTo  from avs_acc where custno='" + CustNo + "' and TrfDate='" + conn.ConvertDate(EDate) + "'";
                ToBRCD = conn.sExecuteScalar(sql);
                sql = "update avs_acc set stage=1003,ACC_STATUS=1 where TrFrm='" + FrmBRCD + "' and TrTo='" + ToBRCD + "' and custno='" + CustNo + "' and brcd='" + FrmBRCD + "' and stage<>1004 and ACC_STATUS=3 and MID='" + EMID + "'";
                result = conn.sExecuteQuery(sql);
                if (result > 1)
                {
                    sql = "update avs_acc set ACC_STATUS=3 where TrFrm='" + FrmBRCD + "' and TrTo='" + ToBRCD + "' and custno='" + CustNo + "' and brcd='" + ToBRCD + "' and stage=1003 and ACC_STATUS=1 and MID='" + EMID + "'";
                    result = conn.sExecuteQuery(sql);
                    sql = "update loaninfo set stage=1003, lmstatus=1 where custno='" + CustNo + "' and brcd='" + FrmBRCD + "' and  lmstatus=99 and MID='" + EMID + "'";
                    conn.sExecuteQuery(sql);
                    sql = "update loaninfo set lmstatus=99 where custno='" + CustNo + "' and brcd='" + ToBRCD + "' and stage=1003 and lmstatus=1 and MID='" + EMID + "'";
                    conn.sExecuteQuery(sql);
                    sql = "update DEPOSITINFO set stage=1003 where custno='" + CustNo + "' and brcd='" + FrmBRCD + "' and stage<>1004 and MID='" + EMID + "'";
                    conn.sExecuteQuery(sql);
                    sql = "update DEPOSITINFO set stage=1004 where custno='" + CustNo + "' and brcd='" + ToBRCD + "' and stage=1003 and MID='" + EMID + "'";
                    conn.sExecuteQuery(sql);
                    sql = "update AVSLnSurityTable set stage=1003 where custno='" + CustNo + "' and brcd='" + FrmBRCD + "' and stage=1004 and MID='" + EMID + "'";
                    conn.sExecuteQuery(sql);
                    sql = "update AVSLnSurityTable set stage=1004 where custno='" + CustNo + "' and brcd='" + ToBRCD + "' and stage=1003 and MID='" + EMID + "'";
                    conn.sExecuteQuery(sql);
                }
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
}