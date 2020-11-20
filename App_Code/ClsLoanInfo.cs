using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data.SqlClient;

public class ClsLoanInfo
{
    string sql = "", PA = "", instDate = "", sResult = "";
    int Result;
    DataTable DT = new DataTable();
    DbConnection conn = new DbConnection();

    public ClsLoanInfo()
    {

    }

    public string GETBONDNO(string LGL, string BRCD)
    {
        try
        {
            try
            {
                sql = "SELECT (LASTNO+1) BONDNO  from avs1000 where  TYPE='BONDNO' AND ACTIVITYNO='" + LGL + "' and BRCD='" + BRCD + "'";//BRCD AND LOANGL ADDED --Abhishek
                LGL = conn.sExecuteScalar(sql);
                return LGL;
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return LGL;
    }

    public string CheckCustStatus(string BrCode, string CustNo)
    {
        try
        {
            sql = "Select Stage From Master Where BrCd = '" + BrCode + "' And CustNo = '" + CustNo + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetIntApp(string BrCode, string SGlCode)
    {
        try
        {
            sql = "Select L.Int_App From LoanGl L Where L.BrCd = '" + BrCode + "' And L.LoanGlCode = '" + SGlCode + "' And EffectiveDate = (Select Max(L1.EffectiveDate) From LoanGl L1 " +
                  "Where L.Brcd = L1.Brcd And L.LOANGLCODE = L1.LOANGLCODE)";
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

    public int CheckAccount(string AC, string PT, string BRCD)
    {
        try
        {
            sql = "Select CustNo From Avs_Acc Where BrCd = '" + BRCD + "' And SubGlCode = '" + PT + "' And AccNo = '" + AC + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string GetAccStatus(string BrCode, string PrCode, string AccNo)
    {
        try
        {
            sql = "SELECT ACC_STATUS FROM AVS_ACC WHERE BRCD='" + BrCode + "' AND SUBGLCODE='" + PrCode + "' AND ACCNO='" + AccNo + "' And Stage = '1003'";
            sResult = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return sResult;
    }

    public int CheckAccountExist(string AC, string PT, string BRCD)
    {
        try
        {
            try
            {
                sql = "select  count(*) from AVS_ACC WHERE SUBGLCODE='" + PT + "' AND ACCNO='" + AC + "' AND BRCD='" + BRCD + "'";
                Result = Convert.ToInt32(conn.sExecuteReader(sql));

            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
                return -1;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int CheckExist(string AC, string PT, string BRCD)
    {
        try
        {
            sql = "SELECT * FROM LOANINFO WHERE LOANGLCODE='" + PT + "' AND CUSTACCNO='" + AC + "' AND BRCD='" + BRCD + "' AND Stage <> 1004 And LmStatus = 1";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
            {
                Result = -1;
            }
            else
            {
                Result = 1;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return -1;
        }
        return Result;
    }

    public DataTable GetInfo(string PT, string AC, string CUSTNO, string BNDNO, string BRCD)
    {
        try
        {
            try
            {
                sql = "SELECT l.*,a.secured FROM LOANINFO l left join avs_lnbasic a on a.BRCD=l.brcd and a.PRDCODE=l.loanglcode and a.ACCTNO=l.custaccno WHERE l.LOANGLCODE='" + PT + "' AND l.CUSTNO='" + CUSTNO + "' AND l.CUSTACCNO='" + AC + "' AND l.BONDNO='" + BNDNO + "' AND l.BRCD='" + BRCD + "' AND LMSTATUS=1 and l.stage<>'1004'";
                DT = new DataTable();
                DT = conn.GetDatatable(sql);
                return DT;
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
                return null;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public string GetBondNo(string PT, string AC, string CUSTNO, string BRCD)
    {
        try
        {
            try
            {
                sql = "SELECT max(BONDNO) FROM LOANINFO WHERE LOANGLCODE='" + PT + "' AND CUSTNO='" + CUSTNO + "' AND CUSTACCNO='" + AC + "' AND BRCD='" + BRCD + "' AND LMSTATUS=1 and stage<>'1004'";
                sResult = conn.sExecuteScalar(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
                return null;
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public int InsertLoan(string CUSTNO, string CUSTACCNO, string LOANGLCODE, string AppNo, string LIMIT, string SANSSIONDATE, string INTRATE, string INSTDATE, string INSTALLMENT, string PERIOD, string DUEDATE, string PENAL, string LMSTATUS, string BONDNO, string MID, string BRCD, string FL, string INSTTYPE, string SancAutho, string RecommAutho, string Remark, string Equated, string IntFund, string Frequency, string GraceDInst, string GraceDIntr, string PLRLink, string MoraPeriod, string EffectDate, string secured, string EDate)
    {
        try
        {
            string lastintdt = "";
            try
            {
                if (FL == "1002")
                {
                    sql = "Select ConVert(VarChar(10), LASTINTDATE, 121) As LASTINTDATE From LOANINFO " +
                          "WHERE BRCD = '" + BRCD + "' AND LOANGLCODE = '" + LOANGLCODE + "' AND CUSTACCNO = '" + CUSTACCNO + "' AND LMSTATUS = 1";
                    lastintdt = conn.sExecuteScalar(sql);

                    sql = "UPDATE LOANINFO SET LMSTATUS = '99' WHERE BRCD = '" + BRCD + "' AND LOANGLCODE = '" + LOANGLCODE + "' AND CUSTACCNO = '" + CUSTACCNO + "' AND LMSTATUS = 1";
                    Result = conn.sExecuteQuery(sql);
                    if (Result > 0)
                    {
                        //  Move old loan info and delete
                        sql = "INSERT INTO LOANINFO_H (CUSTNO, CUSTACCNO, LOANGLCODE, LIMIT, SANSSIONDATE, DRAW_POWER, INTRATE, INSTALLMENT, INSTDATE, PERIOD, INSTTERM, DUEDATE, PENAL, LMSTATUS, DEPOSITGLCODE, DEPOSITACCNO, " +
                              "DEPOSITAMOUNT, APPLINO, APPLDATE, APPLAMT, ELIGIBLE, BRCD, MID, CID, VID, PCMAC, STAGE, BONDNO, INCREBOND, MOD_DATE, TIME, LASTINTDATE, DISYN, PREV_INTDT, INSTTYPE, CASE_OF_DATE, CASE_MID, CASE_CID, " +
                              "REASON, EMI, PRINCIPAL, SancAutho, RecommAutho, Remark, Equated, IntFund, Frequency, GraceDInst, GraceDIntr, PLRLink, MoraPeriod, EffectDate, Old_Cust_No, New_Cust_No, LastCrDate) " +
                              "SELECT CUSTNO, CUSTACCNO, LOANGLCODE, LIMIT, SANSSIONDATE, DRAW_POWER, INTRATE, INSTALLMENT, INSTDATE, PERIOD, INSTTERM, DUEDATE, PENAL, LMSTATUS, DEPOSITGLCODE, DEPOSITACCNO, " +
                              "DEPOSITAMOUNT, APPLINO, APPLDATE, APPLAMT, ELIGIBLE, BRCD, MID, CID, VID, PCMAC, STAGE, BONDNO, INCREBOND, MOD_DATE, TIME, LASTINTDATE, DISYN, PREV_INTDT, INSTTYPE, CASE_OF_DATE, CASE_MID, CASE_CID, " +
                              "REASON, EMI, PRINCIPAL, SancAutho, RecommAutho, Remark, Equated, IntFund, Frequency, GraceDInst, GraceDIntr, PLRLink, MoraPeriod, EffectDate, Old_Cust_No, New_Cust_No, LastCrDate " +
                              "FROM LOANINFO WHERE BRCD = '" + BRCD + "' AND LOANGLCODE = '" + LOANGLCODE + "' AND CUSTACCNO = '" + CUSTACCNO + "' AND LMSTATUS = 99 ";
                        Result = conn.sExecuteQuery(sql);
                        if (Result > 0)
                        {
                            sql = "DELETE FROM LOANINFO WHERE LOANGLCODE = '" + LOANGLCODE + "' AND CUSTACCNO = '" + CUSTACCNO + "' AND BRCD = '" + BRCD + "' AND LMSTATUS = 99";
                            Result = conn.sExecuteQuery(sql);
                        }

                        //  Move old loan schedule and delete
                        sql = "INSERT INTO LOANSCHEDULE_H (CUSTNO, CUSTACCNO, LOANGLCODE, SANLIMIT, INSTALLMENT, DISBURSTMENT_AMOUNT, INSTDATE, PERIOD, LIMIT, BALANCE, INTRATE, BONDNO, INTEREST_RECV, REASONCD, " +
                              "BRCD, MID, CID, VID, PCMAC, STAGE, MOD_DATE, Old_Cust_No, New_Cust_No) " +
                              "SELECT CUSTNO, CUSTACCNO, LOANGLCODE, SANLIMIT, INSTALLMENT, DISBURSTMENT_AMOUNT, INSTDATE, PERIOD, LIMIT, BALANCE, INTRATE, BONDNO, INTEREST_RECV, REASONCD, BRCD, MID, CID, VID, PCMAC, STAGE, MOD_DATE, Old_Cust_No, New_Cust_No " +
                              "FROM LOANSCHEDULE WHERE LOANGLCODE = '" + LOANGLCODE + "' AND CUSTACCNO = '" + CUSTACCNO + "' AND BRCD = '" + BRCD + "'";
                        Result = conn.sExecuteQuery(sql);
                        if (Result > 0)
                        {
                            sql = "DELETE FROM LOANSCHEDULE WHERE LOANGLCODE = '" + LOANGLCODE + "' AND CUSTACCNO = '" + CUSTACCNO + "' AND BRCD = '" + BRCD + "'";
                            Result = conn.sExecuteQuery(sql);
                        }
                    }
                    else
                    {
                        return -1;
                    }
                }

                //  Create new loan info
                sql = "INSERT INTO LOANINFO(CUSTNO, CUSTACCNO, LOANGLCODE, APPLINO, LIMIT, SANSSIONDATE, DRAW_POWER, INTRATE, INSTALLMENT, INSTDATE, PERIOD, DUEDATE, PENAL, LMSTATUS, BONDNO, MID, PCMAC, STAGE, BRCD, DISYN, INSTTYPE, SancAutho, RecommAutho, Remark, Equated, IntFund, Frequency, GraceDInst, GraceDIntr, PLRLink, MoraPeriod, EffectDate, MOD_DATE) " +
                      "VALUES('" + CUSTNO + "', '" + CUSTACCNO + "', '" + LOANGLCODE + "', '" + AppNo + "', '" + LIMIT + "','" + conn.ConvertDate(SANSSIONDATE) + "', '" + LIMIT + "', '" + INTRATE + "', '" + INSTALLMENT + "','" + conn.ConvertDate(INSTDATE) + "', '" + PERIOD + "', '" + conn.ConvertDate(DUEDATE) + "', '" + PENAL + "', '" + LMSTATUS + "', '" + BONDNO + "', " +
                      "'" + MID + "', '" + conn.PCNAME() + "', '1001', '" + BRCD + "','1','" + INSTTYPE + "', '" + SancAutho + "', '" + RecommAutho + "', '" + Remark + "', '" + Equated + "', '" + IntFund + "','" + Frequency + "','" + GraceDInst + "', '" + GraceDIntr + "', '" + PLRLink + "', '" + MoraPeriod + "', '" + conn.ConvertDate(EffectDate) + "', '" + conn.ConvertDate(EDate) + "')";
                Result = conn.sExecuteQuery(sql);
                if (Result > 0)
                {
                    sql = "Update avs1000 Set LASTNO='" + BONDNO + "' Where Brcd = '" + BRCD + "' And TYPE='BONDNO' And ACTIVITYNO='" + LOANGLCODE + "'";
                    conn.sExecuteQuery(sql);
                    if (FL == "1002")
                    {
                        if (lastintdt != "")
                        {
                            sql = "UPDATE LOANINFO SET LASTINTDATE='" + conn.ConvertDate(lastintdt) + "', MID='" + MID + "' WHERE LOANGLCODE='" + LOANGLCODE + "' AND CUSTNO='" + CUSTNO + "' AND CUSTACCNO='" + CUSTACCNO + "' AND BRCD='" + BRCD + "' AND LMSTATUS=1";
                            Result = conn.sExecuteQuery(sql);
                        }
                        sql = "UPDATE AVS_ACC SET OPENINGDATE='" + conn.ConvertDate(SANSSIONDATE) + "', MID='" + MID + "' WHERE SUBGLCODE='" + LOANGLCODE + "' AND CUSTNO='" + CUSTNO + "' AND ACCNO='" + CUSTACCNO + "' AND BRCD='" + BRCD + "'";
                        Result = conn.sExecuteQuery(sql);
                    }
                }

                //  Create new loan schedule
                sql = "EXEC SP_LOAN_SCHEDULE @FLAG='LS',@FBRCD='" + BRCD + "',@TBRCD='" + BRCD + "',@FSBGL='" + LOANGLCODE + "',@TSBGL='" + LOANGLCODE + "',@FACCNO='" + CUSTACCNO + "',@TACCNO='" + CUSTACCNO + "',@MID='" + MID + "'";
                Result = conn.sExecuteQuery(sql);
                if (FL == "1002")
                {
                    sql = "Update AVS_LNBasic set SECURED='" + secured + "',CID='" + MID + "',STAGE='1002' where PRDCODE='" + LOANGLCODE + "' and ACCTNO='" + CUSTACCNO + "' and CUSTNO='" + CUSTNO + "' and BRCD='" + BRCD + "'";
                    Result = conn.sExecuteQuery(sql);
                    if (Result == 0)
                    {
                        sql = "insert into AVS_LNBasic(PRDCODE,ACCTNO,CATCODE,BRWTYPE,INDTYPE,INDSUBTYPE,PRPCODE,SUBPRPCODE,PRIORITY,CATEGORY1,SUBPRIORITY,WEAKERSEC,CATEGORY2,SUBWEAKERSEC,LOANTRM,HEALTHCODE,SECURED,SEC58,DIRCT_INDIRCT,EXEDATE,RESLTNNO,CUSTNO,BRCD,MID,CID,VID,PCMAC,STAGE,STATUS,SYSTEMDATE)" +
                              "values('" + LOANGLCODE + "','" + CUSTACCNO + "','1','1','1','1','1','1','1','1','1','1','1','1','1','1','" + secured + "','1','1','" + conn.ConvertDate(SANSSIONDATE) + "','1','" + CUSTNO + "','" + BRCD + "','" + MID + "','0','0','loanlimit','1001','1',getdate())";
                        Result = conn.sExecuteQuery(sql);
                    }
                }
                else
                {
                    sql = "insert into AVS_LNBasic(PRDCODE,ACCTNO,CATCODE,BRWTYPE,INDTYPE,INDSUBTYPE,PRPCODE,SUBPRPCODE,PRIORITY,CATEGORY1,SUBPRIORITY,WEAKERSEC,CATEGORY2,SUBWEAKERSEC,LOANTRM,HEALTHCODE,SECURED,SEC58,DIRCT_INDIRCT,EXEDATE,RESLTNNO,CUSTNO,BRCD,MID,CID,VID,PCMAC,STAGE,STATUS,SYSTEMDATE)" +
                          "values('" + LOANGLCODE + "','" + CUSTACCNO + "','1','1','1','1','1','1','1','1','1','1','1','1','1','1','" + secured + "','1','1','" + conn.ConvertDate(SANSSIONDATE) + "','1','" + CUSTNO + "','" + BRCD + "','" + MID + "','0','0','loanlimit','1001','1',getdate())";
                    Result = conn.sExecuteQuery(sql);
                }
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }



    public int InsertLoanApp(string CUSTNO, string CUSTACCNO, string LOANGLCODE, string AppNo, string LIMIT, string SANSSIONDATE, string INTRATE, string INSTDATE, 
        string INSTALLMENT, string PERIOD, string DUEDATE, string PENAL, string LMSTATUS, string BONDNO, string MID, string BRCD, string FL, string INSTTYPE, string Equated, 
        string IntFund, string Frequency, string PLRLink, string EffectDate)
    {
        string lastintdt = "";

        try
        {
            if (FL == "1001")
            {
                sql = "UPDATE LOANINFO SET LMSTATUS = '99' WHERE BRCD = '" + BRCD + "' AND LOANGLCODE = '" + LOANGLCODE + "' AND CUSTACCNO = '" + CUSTACCNO + "' AND LMSTATUS = 1";
                Result = conn.sExecuteQuery(sql);

                //  Move old loan info and delete
                sql = "INSERT INTO LOANINFO_H (CUSTNO, CUSTACCNO, LOANGLCODE, LIMIT, SANSSIONDATE, DRAW_POWER, INTRATE, INSTALLMENT, INSTDATE, PERIOD, INSTTERM, DUEDATE, PENAL, LMSTATUS, DEPOSITGLCODE, DEPOSITACCNO, " +
                      "DEPOSITAMOUNT, APPLINO, APPLDATE, APPLAMT, ELIGIBLE, BRCD, MID, CID, VID, PCMAC, STAGE, BONDNO, INCREBOND, MOD_DATE, TIME, LASTINTDATE, DISYN, PREV_INTDT, INSTTYPE, CASE_OF_DATE, CASE_MID, CASE_CID, " +
                      "REASON, EMI, PRINCIPAL, SancAutho, RecommAutho, Remark, Equated, IntFund, Frequency, GraceDInst, GraceDIntr, PLRLink, MoraPeriod, EffectDate, Old_Cust_No, New_Cust_No, LastCrDate) " +
                      "SELECT CUSTNO, CUSTACCNO, LOANGLCODE, LIMIT, SANSSIONDATE, DRAW_POWER, INTRATE, INSTALLMENT, INSTDATE, PERIOD, INSTTERM, DUEDATE, PENAL, LMSTATUS, DEPOSITGLCODE, DEPOSITACCNO, " +
                      "DEPOSITAMOUNT, APPLINO, APPLDATE, APPLAMT, ELIGIBLE, BRCD, MID, CID, VID, PCMAC, STAGE, BONDNO, INCREBOND, MOD_DATE, TIME, LASTINTDATE, DISYN, PREV_INTDT, INSTTYPE, CASE_OF_DATE, CASE_MID, CASE_CID, " +
                      "REASON, EMI, PRINCIPAL, SancAutho, RecommAutho, Remark, Equated, IntFund, Frequency, GraceDInst, GraceDIntr, PLRLink, MoraPeriod, EffectDate, Old_Cust_No, New_Cust_No, LastCrDate " +
                      "FROM LOANINFO WHERE BRCD = '" + BRCD + "' AND LOANGLCODE = '" + LOANGLCODE + "' AND CUSTACCNO = '" + CUSTACCNO + "' AND LMSTATUS = 99 ";
                Result = conn.sExecuteQuery(sql);
                if (Result > 0)
                {
                    sql = "DELETE FROM LOANINFO WHERE LOANGLCODE = '" + LOANGLCODE + "' AND CUSTACCNO = '" + CUSTACCNO + "' AND BRCD = '" + BRCD + "' AND LMSTATUS = 99";
                    Result = conn.sExecuteQuery(sql);
                }

                sql = "DELETE FROM LOANSCHEDULE WHERE LOANGLCODE = '" + LOANGLCODE + "' AND CUSTACCNO = '" + CUSTACCNO + "' AND BRCD = '" + BRCD + "'";
                Result = conn.sExecuteQuery(sql);
            }
            else if (FL == "1002")
            {
                sql = "Select ConVert(VarChar(10), LASTINTDATE, 121) As LASTINTDATE From LOANINFO " +
                      "WHERE BRCD = '" + BRCD + "' AND LOANGLCODE = '" + LOANGLCODE + "' AND CUSTACCNO = '" + CUSTACCNO + "' AND LMSTATUS = 1";
                lastintdt = conn.sExecuteScalar(sql);

                sql = "UPDATE LOANINFO SET LMSTATUS = '99' WHERE BRCD = '" + BRCD + "' AND LOANGLCODE = '" + LOANGLCODE + "' AND CUSTACCNO = '" + CUSTACCNO + "' AND LMSTATUS = 1";
                Result = conn.sExecuteQuery(sql);
                if (Result > 0)
                {
                    //  Move old loan info and delete
                    sql = "INSERT INTO LOANINFO_H (CUSTNO, CUSTACCNO, LOANGLCODE, LIMIT, SANSSIONDATE, DRAW_POWER, INTRATE, INSTALLMENT, INSTDATE, PERIOD, INSTTERM, DUEDATE, PENAL, LMSTATUS, DEPOSITGLCODE, DEPOSITACCNO, " +
                          "DEPOSITAMOUNT, APPLINO, APPLDATE, APPLAMT, ELIGIBLE, BRCD, MID, CID, VID, PCMAC, STAGE, BONDNO, INCREBOND, MOD_DATE, TIME, LASTINTDATE, DISYN, PREV_INTDT, INSTTYPE, CASE_OF_DATE, CASE_MID, CASE_CID, " +
                          "REASON, EMI, PRINCIPAL, SancAutho, RecommAutho, Remark, Equated, IntFund, Frequency, GraceDInst, GraceDIntr, PLRLink, MoraPeriod, EffectDate, Old_Cust_No, New_Cust_No, LastCrDate) " +
                          "SELECT CUSTNO, CUSTACCNO, LOANGLCODE, LIMIT, SANSSIONDATE, DRAW_POWER, INTRATE, INSTALLMENT, INSTDATE, PERIOD, INSTTERM, DUEDATE, PENAL, LMSTATUS, DEPOSITGLCODE, DEPOSITACCNO, " +
                          "DEPOSITAMOUNT, APPLINO, APPLDATE, APPLAMT, ELIGIBLE, BRCD, MID, CID, VID, PCMAC, STAGE, BONDNO, INCREBOND, MOD_DATE, TIME, LASTINTDATE, DISYN, PREV_INTDT, INSTTYPE, CASE_OF_DATE, CASE_MID, CASE_CID, " +
                          "REASON, EMI, PRINCIPAL, SancAutho, RecommAutho, Remark, Equated, IntFund, Frequency, GraceDInst, GraceDIntr, PLRLink, MoraPeriod, EffectDate, Old_Cust_No, New_Cust_No, LastCrDate " +
                          "FROM LOANINFO WHERE BRCD = '" + BRCD + "' AND LOANGLCODE = '" + LOANGLCODE + "' AND CUSTACCNO = '" + CUSTACCNO + "' AND LMSTATUS = 99 ";
                    Result = conn.sExecuteQuery(sql);
                    if (Result > 0)
                    {
                        sql = "DELETE FROM LOANINFO WHERE LOANGLCODE = '" + LOANGLCODE + "' AND CUSTACCNO = '" + CUSTACCNO + "' AND BRCD = '" + BRCD + "' AND LMSTATUS = 99";
                        Result = conn.sExecuteQuery(sql);
                    }

                    //  Move old loan schedule and delete
                    sql = "INSERT INTO LOANSCHEDULE_H (CUSTNO, CUSTACCNO, LOANGLCODE, SANLIMIT, INSTALLMENT, DISBURSTMENT_AMOUNT, INSTDATE, PERIOD, LIMIT, BALANCE, INTRATE, BONDNO, INTEREST_RECV, REASONCD, " +
                          "BRCD, MID, CID, VID, PCMAC, STAGE, MOD_DATE, Old_Cust_No, New_Cust_No) " +
                          "SELECT CUSTNO, CUSTACCNO, LOANGLCODE, SANLIMIT, INSTALLMENT, DISBURSTMENT_AMOUNT, INSTDATE, PERIOD, LIMIT, BALANCE, INTRATE, BONDNO, INTEREST_RECV, REASONCD, BRCD, MID, CID, VID, PCMAC, STAGE, MOD_DATE, Old_Cust_No, New_Cust_No " +
                          "FROM LOANSCHEDULE WHERE LOANGLCODE = '" + LOANGLCODE + "' AND CUSTACCNO = '" + CUSTACCNO + "' AND BRCD = '" + BRCD + "'";
                    Result = conn.sExecuteQuery(sql);
                    if (Result > 0)
                    {
                        sql = "DELETE FROM LOANSCHEDULE WHERE LOANGLCODE = '" + LOANGLCODE + "' AND CUSTACCNO = '" + CUSTACCNO + "' AND BRCD = '" + BRCD + "'";
                        Result = conn.sExecuteQuery(sql);
                    }
                }
                else
                {
                    return -1;
                }
            }

            string LoanPurpose = "";
            sql = "select LoanPurpose from avs1004 where custno='" + CUSTNO + "' and brcd='" + BRCD + "' and LOANPRODUCT='" + LOANGLCODE + "' and APPNO='" + AppNo + "' and BONDNO='" + BONDNO + "'";
            LoanPurpose = conn.sExecuteScalar(sql);

            //  Create new loan info
            sql = "INSERT INTO LOANINFO(CUSTNO, CUSTACCNO, LOANGLCODE, APPLINO, LIMIT, SANSSIONDATE, DRAW_POWER, INTRATE, INSTALLMENT, INSTDATE, PERIOD, DUEDATE, PENAL, LMSTATUS, "+
                  "BONDNO, MID, PCMAC, STAGE, BRCD, DISYN, INSTTYPE, Equated, IntFund, Frequency, PLRLink, EffectDate, MOD_DATE, LastINTDate, LoanPurpose) " +
                  "VALUES('" + CUSTNO + "', '" + CUSTACCNO + "', '" + LOANGLCODE + "', '" + AppNo + "', '" + LIMIT + "','" + conn.ConvertDate(SANSSIONDATE) + "', '" + LIMIT + "', '" + INTRATE + "', '" + INSTALLMENT + "', "+
                  "'" + conn.ConvertDate(INSTDATE) + "', '" + PERIOD + "', '" + conn.ConvertDate(DUEDATE) + "', '" + PENAL + "', '" + LMSTATUS + "', '" + BONDNO + "', " +
                  "'" + MID + "', '" + conn.PCNAME() + "', '1003', '" + BRCD + "','1','" + INSTTYPE + "', '" + Equated + "', '" + IntFund + "','" + Frequency + "','" + PLRLink + "', '" + conn.ConvertDate(EffectDate) + "', " +
                  "'" + conn.ConvertDate(EffectDate) + "','" + conn.ConvertDate(EffectDate) + "', '" + LoanPurpose + "')";
            Result = conn.sExecuteQuery(sql);
            if (Result > 0)
            {
                sql = "Update avs1000 Set LASTNO='" + BONDNO + "' Where Brcd = '" + BRCD + "' And TYPE='BONDNO' And ACTIVITYNO='" + LOANGLCODE + "'";
                conn.sExecuteQuery(sql);
                if (FL == "1002")
                {
                    sql = "UPDATE LOANINFO SET LASTINTDATE='" + conn.ConvertDate(lastintdt) + "' WHERE LOANGLCODE='" + LOANGLCODE + "' AND CUSTNO='" + CUSTNO + "' AND CUSTACCNO='" + CUSTACCNO + "' AND BRCD='" + BRCD + "' AND LMSTATUS=1";
                    Result = conn.sExecuteQuery(sql);
                    sql = "UPDATE AVS_ACC SET OPENINGDATE='" + conn.ConvertDate(SANSSIONDATE) + "' WHERE SUBGLCODE='" + LOANGLCODE + "' AND CUSTNO='" + CUSTNO + "' AND ACCNO='" + CUSTACCNO + "' AND BRCD='" + BRCD + "'";
                    Result = conn.sExecuteQuery(sql);
                }
            }

            //  Create new loan schedule
            sql = "EXEC SP_LOAN_SCHEDULE @FLAG='LS',@FBRCD='" + BRCD + "',@TBRCD='" + BRCD + "',@FSBGL='" + LOANGLCODE + "',@TSBGL='" + LOANGLCODE + "',@FACCNO='" + CUSTACCNO + "',@TACCNO='" + CUSTACCNO + "',@MID='" + MID + "'";
            conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }


    public int BindGrid(GridView Gview, string PT, string AC, string BRCD, string edate)
    {
        try
        {
            sql = "";
            if (PT == "" && AC == "")
            {
                sql = "SELECT LI.BONDNO,LI.CUSTACCNO,LI.LOANGLCODE,M.CUSTNAME,LI.LIMIT,LI.INTRATE,LI.INSTALLMENT,LI.PERIOD,LI.DUEDATE,LI.SANSSIONDATE,GL.GLNAME,(isnull(convert(varchar(10),LI.BONDNO),'0')+'_'+isnull(convert(varchar(10),LI.CUSTACCNO),'0')+'_'+isnull(convert(varchar(10),LI.LOANGLCODE),'0')) as ACCSUB from LOANINFO LI " +
                      "INNER JOIN AVS_ACC AC ON AC.ACCNO=LI.CUSTACCNO AND AC.CUSTNO=LI.CUSTNO AND AC.SUBGLCODE=LI.LOANGLCODE AND LI.BRCD=AC.BRCD " +
                      "LEFT JOIN MASTER M ON M.CUSTNO=LI.CUSTNO " +//ankita 22/11/2017 brcd removed
                      "LEFT JOIN GLMAST GL ON GL.SUBGLCODE=LI.LOANGLCODE AND GL.BRCD=LI.BRCD " +
                      "WHERE LI.SANSSIONDATE='" + conn.ConvertDate(edate) + "' AND LI.BRCD='" + BRCD + "' And LI.Stage <> 1004 AND LI.LMSTATUS='1'";
            }
            else
            {
                sql = "SELECT LI.BONDNO,LI.CUSTACCNO,LI.LOANGLCODE,M.CUSTNAME,LI.LIMIT,LI.INTRATE,LI.INSTALLMENT,LI.PERIOD,LI.DUEDATE,LI.SANSSIONDATE,GL.GLNAME,(isnull(convert(varchar(10),LI.BONDNO),'0')+'_'+isnull(convert(varchar(10),LI.CUSTACCNO),'0')+'_'+isnull(convert(varchar(10),LI.LOANGLCODE),'0')) as ACCSUB from LOANINFO LI " +
                      "INNER JOIN AVS_ACC AC ON AC.ACCNO=LI.CUSTACCNO AND AC.CUSTNO=LI.CUSTNO AND AC.SUBGLCODE=LI.LOANGLCODE AND LI.BRCD=AC.BRCD " +
                      "LEFT JOIN MASTER M ON M.CUSTNO=LI.CUSTNO and AC.BRCD=M.BRCD " +//ankita 22/11/2017 brcd removed
                      "LEFT JOIN GLMAST GL ON GL.SUBGLCODE=LI.LOANGLCODE AND GL.BRCD=LI.BRCD " +
                      "WHERE LI.LOANGLCODE='" + PT + "' AND LI.CUSTACCNO='" + AC + "' AND LI.BRCD='" + BRCD + "' And LI.Stage <> 1004 AND LI.LMSTATUS='1'";

            }

            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return -1;
        }
        return Result;
    }

    public int Lon_OPR(string ACCNO, string CUSTNO, string MID, string SUBGL, string FL, string BRCD, string EDate)
    {
        try
        {
            if (FL == "DELETE")
            {
                sql = "UPDATE LOANINFO SET STAGE = 1004, LMSTATUS = 99, Mod_Date = '" + conn.ConvertDate(EDate).ToString() + "', Vid = '" + MID + "' WHERE BRCD='" + BRCD + "' And LOANGLCODE='" + SUBGL + "' AND CUSTACCNO='" + ACCNO + "' AND LMSTATUS <> 99 And Stage <> 1004"; //BRCD ADDED --Abhishe
                Result = conn.sExecuteQuery(sql);
                sql = "INSERT INTO LOANINFO_H SELECT * FROM LOANINFO WHERE LOANGLCODE='" + SUBGL + "' AND CUSTNO='" + CUSTNO + "' AND CUSTACCNO='" + ACCNO + "' AND BRCD='" + BRCD + "' AND LMSTATUS=99";
                Result = conn.sExecuteQuery(sql);
                if (Result > 0)
                {
                    sql = "DELETE FROM LOANINFO WHERE LOANGLCODE='" + SUBGL + "' AND CUSTNO='" + CUSTNO + "' AND CUSTACCNO='" + ACCNO + "' AND BRCD='" + BRCD + "' AND LMSTATUS=99";
                    Result = conn.sExecuteQuery(sql);
                    if (Result > 0)
                    {
                    }
                }
                sql = "Update AVS_LNBasic set CID='" + MID + "',STAGE='1004' where PRDCODE='" + SUBGL + "' and ACCTNO='" + ACCNO + "' and CUSTNO='" + CUSTNO + "' and BRCD='" + BRCD + "'";
                Result = conn.sExecuteQuery(sql);
            }
            else if (FL == "AUTHO")
            {
                sql = "UPDATE LOANINFO SET STAGE = 1003, Mod_Date = '" + conn.ConvertDate(EDate).ToString() + "', Vid = '" + MID + "' WHERE BRCD='" + BRCD + "' And LOANGLCODE='" + SUBGL + "' AND CUSTACCNO='" + ACCNO + "' AND LMSTATUS <> 99 AND MID <> '" + MID + "' And Stage <> 1004"; //BRCD ADDED --Abhishek
                Result = conn.sExecuteQuery(sql);
                sql = "Update AVS_LNBasic set VID='" + MID + "',STAGE='1003' where PRDCODE='" + SUBGL + "' and ACCTNO='" + ACCNO + "' and CUSTNO='" + CUSTNO + "' and BRCD='" + BRCD + "'";
                Result = conn.sExecuteQuery(sql);
            }
            else if (FL == "VIEW")
            {
                sql = "SELECT * FROM LOANINFO WHERE BRCD='" + BRCD + "' And LOANGLCODE='" + SUBGL + "' AND CUSTACCNO='" + ACCNO + "' AND LMSTATUS <> 99 AND MID <> '" + MID + "' And Stage <> 1004"; //BRCD ADDED --Abhishek
                Result = conn.sExecuteQuery(sql);
            }

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;

    }

    public int BindSchedule(GridView Gview, string PT, string AC, string BRCD)
    {
        try
        {
            sql = "Exec SP_SCHEDULELOAN @LGL='" + PT + "',@CACCNO='" + AC + "',@BRCD='" + BRCD + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetInfo(string PT, string AC, string BRCD)
    {
        DataTable DT1 = new DataTable();
        try
        {
            sql = "Exec SP_SCHEDULELOAN @LGL='" + PT + "',@CACCNO='" + AC + "',@BRCD='" + BRCD + "'";
            DT1 = new DataTable();
            DT1 = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT1;
    }

    public double PrAmt(string BRCD, string PT, string AN, string Edate)
    {
        try
        {
            PA = conn.sExecuteScalar("EXEC SP_PRINOD_INTODAMT '" + BRCD + "','" + PT + "','" + AN + "','" + conn.ConvertDate(Edate).ToString() + "','PrAmt'");

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(PA);
    }

    public double IntAmt(string BRCD, string PT, string AN, string Edate)
    {
        try
        {
            PA = conn.sExecuteScalar("EXEC SP_PRINOD_INTODAMT '" + BRCD + "','" + PT + "','" + AN + "','" + conn.ConvertDate(Edate).ToString() + "','IntAmt'");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(PA);
    }

    public string LInstDate(string BRCD, string PT, string AN)
    {
        try
        {
            instDate = conn.sExecuteScalar("SELECT isnull(CONVERT(VARCHAR(10), LASTINTDATE, 121),'01/01/1990') FROM LOANINFO WHERE BRCD = '" + BRCD + "' AND LOANGLCODE = '" + PT + "' AND CUSTACCNO = '" + AN + "'");
            if (instDate != null)
            {
                string[] date = instDate.ToString().Split('-');
                if (date.Length > 0)
                {
                    instDate = "";
                    instDate = date[2].ToString() + '/' + date[1].ToString() + '/' + date[0].ToString();
                }
            }
            else
                instDate = "01/01/1990";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return instDate.ToString();
    }

    //Added By AmolB ON 20170113 For Get a Sanction amount, Rate of Interest, Period, DueDate
    public DataTable GetLienInfo(string BRCD, string CustNo, string LoanSubGl, string LoanAccNo)
    {
        try
        {
            sql = "SELECT SUM(LIENAMOUNT) AS LIENAMOUNT, MAX(PERIOD) AS PERIOD, MAX(RATEOFINT) AS RATEOFINT,MAX(CONVERT(VARCHAR(10), DUEDATE, 121)) AS DUEDATE, MAX(CONVERT(VARCHAR(10), LOAN_DISBDATE, 121)) AS SANCDATE " +
                  "FROM DEPOSITINFO WHERE BRCD = '" + BRCD + "' AND LOANSUBGLCD = '" + LoanSubGl + "' AND LOANACCNO = '" + LoanAccNo + "' AND LIENMARK = 'Y'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    //Added By AmolB ON 20170113 For Get a Extra Rate of Interest to loan against Deposite
    public int GetExtraIntRate()
    {
        try
        {
            sql = "SELECT ISNULL(LISTVALUE, 0) FROM parameter WHERE LISTFIELD = 'LADINT' AND BRCD = '0'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));

            if (Result == 0)
            {
                Result = Convert.ToInt32(2);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return Result = 0;
        }
        return Convert.ToInt32(Result);
    }
    public string GetAccstsNm(string srno)
    {
        try
        {
            sql = "select DESCRIPTION from LOOKUPFORM1 where lno=1016 and SRNO='" + srno + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            return sResult = "";
        }
        return sResult;
    }

    public string GetROI(string BrCode, string PrCode)
    {
        try
        {
            sql = "Select ConVert(VarChar(10), IsNull(ROI, 0)) +'_'+ ConVert(VarChar(10), IsNull(PenalInt, 0)) +'_'+ ConVert(VarChar(10), IsNull(Period, 0)) As ROI " +
                  "From LoanGl Where BrCd = '" + BrCode + "' AND LoanGlCode = '" + PrCode + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            return sResult = "";
        }
        return sResult;
    }

    public string GetLimitROI(string brcd, string subgl, string EDT)
    {
        try
        {
            sql = "select top (1)(isnull(convert(varchar(10),ROI),'0')+'_'+isnull(convert(varchar(10),PENALINT),'0'))ROI from avs5028 where EffectDate<='" + conn.ConvertDate(EDT) + "' and subglcode='" + subgl + "' order by EffectDate desc";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            return sResult = "";
        }
        return sResult;
    }
    public string GetStageMid(string brcd, string subgl, string accno)
    {
        try
        {
            sql = "SELECT MID FROM loaninfo WHERE BRCD='" + brcd + "' AND LOANGLCODE='" + subgl + "' AND CUSTACCNO='" + accno + "'";
            sResult = conn.sExecuteScalar(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sResult;
    }
    public string GetAccType(string BrCode, string PrCode, string AccNo)
    {
        try
        {
            sql = "SELECT ACC_TYPE FROM AVS_ACC WHERE BRCD='" + BrCode + "' AND SUBGLCODE='" + PrCode + "' AND ACCNO='" + AccNo + "' And Stage = '1003'";
            sResult = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return sResult;
    }
    public string GETUSERGRP(string MID)
    {
        string usrgrp = "";
        try
        {
            sql = "select usergroup from usermaster where permissionno='" + MID + "'";
            usrgrp = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return usrgrp;
    }
}