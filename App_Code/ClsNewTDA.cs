using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

public class ClsNewTDA
{
    string sql = "";
    string sql1 = "";
    string rtn = "";
    int rtnint = 0;
    DataTable dt = new DataTable();
    DbConnection conn = new DbConnection();

    public ClsNewTDA()
    {

    }

    // Get Deposit form data all
    public DataTable GetAllFieldData(string ProCode, string AccNo, string brcd, string SRNO)
    {
        try
        {
            sql = " SELECT isnull(DP.RECSRNO,'0') RECSRNO,DP.RECEIPT_NO,DP.STAGE,M.CUSTNO, M.CUSTNAME, DP.CUSTACCNO,DP.TRFSUBTYPE,DP.TRFACCNO,AC.ACC_TYPE, AC.OPR_TYPE, DP.OPENINGDATE, DP.INTPAYOUT, DP.PRNAMT, DP.PRDTYPE, DP.PERIOD, DP.RATEOFINT, DP.INTAMT, DP.MATURITYAMT, DP.DUEDATE from DEPOSITINFO DP " +
                  " INNER JOIN AVS_ACC AC ON AC.ACCNO=DP.CUSTACCNO AND DP.BRCD = AC.BRCD AND DP.CUSTNO=AC.CUSTNO AND AC.SUBGLCODE=DP.DEPOSITGLCODE " +
                  " INNER JOIN MASTER M ON M.CUSTNO=AC.CUSTNO AND M.CUSTNO=DP.CUSTNO  " +         ///AND AC.BRCD=M.BRCD UNIFICATION 
                  " WHERE DP.BRCD='" + brcd + "'  AND DP.CUSTACCNO = '" + AccNo + "' AND DP.DEPOSITGLCODE='" + ProCode + "' and isnull(DP.RECSRNO,'0')='" + SRNO + "' AND DP.LMSTATUS='1'";
            //AND DP.LMSTATUS='1'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return dt;
    }



    // Get Deposit form data all
    public DataTable GetAllFieldDataCBS2(string ProCode, string AccNo, string brcd, string SRNO)
    {
        try
        {
            sql = "SELECT IsNull(DP.RECSRNO, '0') As RECSRNO, DP.RECEIPT_NO, DP.STAGE, M.CUSTNO, M.CUSTNAME, DP.CUSTACCNO, Dp.TrfBrCd, DP.TRFSUBTYPE, DP.TRFACCNO, " +
                  "AC.ACC_TYPE, AC.OPR_TYPE, ConVert(VarChar(10), DP.OPENINGDATE, 103) As OPENINGDATE, DP.INTPAYOUT, DP.PRNAMT, DP.PRDTYPE, DP.PERIOD, DP.RATEOFINT, " +
                  "DP.INTAMT, DP.MATURITYAMT, ConVert(VarChar(10), DP.DUEDATE, 103) As DUEDATE From DepositInfo DP " +
                  "INNER JOIN AVS_ACC AC ON AC.ACCNO=DP.CUSTACCNO AND DP.BRCD = AC.BRCD AND DP.CUSTNO = AC.CUSTNO AND AC.SUBGLCODE = DP.DEPOSITGLCODE " +
                  "INNER JOIN MASTER M ON M.CUSTNO = AC.CUSTNO AND M.CUSTNO = DP.CUSTNO  " +
                  "Where Dp.BrCd = '" + brcd + "' And Dp.DepositGlCode = '" + ProCode + "' And Dp.CustAccNo = '" + AccNo + "' " +
                  "And IsNull(Dp.RecSrNo, 0) = '" + SRNO + "' And Dp.LmStatus = '1' ";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }


    public double GetMaturityParaDetails(double CurrentROI)
    {

        bool isMaturityParaYN = false;
        string ParaYN = "";

        try
        {
            sql = "select ISNULL(LISTVALUE,'N') from PARAMETER where LISTFIELD='FDPREMATUREINTYN'";
            ParaYN = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ParaYN = "N";
            ExceptionLogging.SendErrorToText(ex);
        }
       return  ParaYN == "Y" ?(CurrentROI - 2.0) : ParaYN==null?0.0F:0.0F;
    }





    public string GetInterestRateED(string FL, string SFL, string SBGL, string ACCNO, string RECNO, string CUSTTYPE, string PERIODTYPE, string PERIOD, string OPDT, string EDT, bool SN, string SFLAG2 = "NA", string BRCD = "1") //Added by abhishek for effective date wise rate 1-11-2017
    {
        //float Rtn = 0;
        try
        {
            if (SN == false)
            {

                sql = " Exec Isp_AVS0083CBS2 @Flag='" + FL + "',@Brcd='" + BRCD + "',@SFlag='NoSubgl',@SFlag2='" + SFLAG2 + "',@Subglcode='" + SBGL + "',@Accno='" + ACCNO + "',@RecNo='" + RECNO + "',@CustType='" + CUSTTYPE + "',@PeriodType='" + PERIODTYPE + "',@Period='" + PERIOD + "',@Edt='" + conn.ConvertDate(EDT) + "',@OpeningDate='" + conn.ConvertDate(OPDT) + "'";
                //sql = " Exec Isp_AVS0083 @Flag='" + FL + "',@Brcd='" + BRCD + "',@SFlag='NoSubgl',@SFlag2='" + SFLAG2 + "',@Subglcode='" + SBGL + "',@Accno='" + ACCNO + "',@RecNo='" + RECNO + "',@CustType='" + CUSTTYPE + "',@PeriodType='" + PERIODTYPE + "',@Period='" + PERIOD + "',@Edt='" + conn.ConvertDate(EDT) + "',@OpeningDate='" + conn.ConvertDate(OPDT) + "'";
            }
            else
            {

                sql = " Exec Isp_AVS0083CBS2 @Flag='" + FL + "',@Brcd='" + BRCD + "',@SFlag='WithSubgl',@SFlag2='" + SFLAG2 + "',@Subglcode='" + SBGL + "',@Accno='" + ACCNO + "',@RecNo='" + RECNO + "',@CustType='" + CUSTTYPE + "',@PeriodType='" + PERIODTYPE + "',@Period='" + PERIOD + "',@Edt='" + conn.ConvertDate(EDT) + "',@OpeningDate='" + conn.ConvertDate(OPDT) + "'";
                //   sql = " Exec Isp_AVS0083 @Flag='" + FL + "',@Brcd='" + BRCD + "',@SFlag='WithSubgl',@SFlag2='" + SFLAG2 + "',@Subglcode='" + SBGL + "',@Accno='" + ACCNO + "',@RecNo='" + RECNO + "',@CustType='" + CUSTTYPE + "',@PeriodType='" + PERIODTYPE + "',@Period='" + PERIOD + "',@Edt='" + conn.ConvertDate(EDT) + "',@OpeningDate='" + conn.ConvertDate(OPDT) + "'";
            }
            sql = conn.sExecuteScalar(sql);


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }



    public string GetPanNo(string BRCD, string CustNo)
    {
        string No = "";
        try
        {
            sql = "select DOC_NO from identity_proof where DOC_TYPE=3 and BRcd='" + BRCD + "' and Custno='" + CustNo + "'";
            No = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return No;
    }



    public string GetIntPayout(string BRCD, string SUBGL)
    {
        try
        {
            sql = "Select INTERESTTYPE2 from DEPOSITGL where  DEPOSITGLCODE='" + SUBGL + "' and BRCD='" + BRCD + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }


    public string GetGlCode(string BRCD, string subglcode)
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


    public string Get_PeriodType(string PRDCD)
    {
        try
        {
            sql = "Exec Isp_RenewAutoCheck @Flag='GetPeriodType',@PrdCd='" + PRDCD + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }


    public string GetIntPaid(string BRCD, string GLCD, string SGLCD, string ACC, string FL, string EDT, string RECSRNO = "0")
    {
        try
        {
            sql = "EXEC DepositeClousureState @Edt='" + conn.ConvertDate(EDT) + "',@PRDTCD ='" + SGLCD + "',@GLCD='" + GLCD + "',@ACCNO ='" + ACC + "',@BRCD='" + BRCD + "',@Flag='" + FL + "',@RecSrno='" + RECSRNO + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }


    // Report Get Deposit 
    public DataTable ReportDepositReceipt(string ProCode, string AccNo, string brcd)
    {
        try
        {
            sql = " SELECT M.CUSTNO, M.CUSTNAME, DP.CUSTACCNO, AC.ACC_TYPE, AC.OPR_TYPE, DP.OPENINGDATE, DP.INTPAYOUT, DP.PRNAMT, DP.PRDTYPE, DP.PERIOD, DP.RATEOFINT, DP.INTAMT, DP.MATURITYAMT, DP.DUEDATE from DEPOSITINFO DP " +
                  " INNER JOIN AVS_ACC AC ON AC.ACCNO=DP.CUSTACCNO AND DP.BRCD = AC.BRCD AND DP.CUSTNO=AC.CUSTNO AND AC.SUBGLCODE=DP.DEPOSITGLCODE " +
                  " INNER JOIN MASTER M ON M.CUSTNO=AC.CUSTNO AND M.CUSTNO=DP.CUSTNO " +
                  " WHERE DP.BRCD='" + brcd + "'  AND DP.CUSTACCNO = '" + AccNo + "' AND DP.DEPOSITGLCODE='" + ProCode + "' AND DP.LMSTATUS='1' ";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return dt;
    }

    // Get lien Mark form data
    public DataTable GetLienMarkFieldData(string ProCode, string AccNo, string brcd)
    {
        try
        {
            sql = " SELECT AC.ACC_TYPE,DP.TRFACCTYPE, GL.GLNAME, M.CUSTNO, M.CUSTNAME, DP.LOANSUBGLCD, DP.LOANACCNO, DP.LIENAMOUNT FROM DEPOSITINFO DP " +
                  " INNER JOIN AVS_ACC AC ON AC.ACCNO=DP.CUSTACCNO AND DP.BRCD = AC.BRCD AND DP.CUSTNO=AC.CUSTNO AND AC.SUBGLCODE=DP.DEPOSITGLCODE  " +
                  " LEFT JOIN GLMAST GL ON GL.SUBGLCODE=DP.TRFACCTYPE AND GL.GLCODE='3'" +
                  " INNER JOIN MASTER M ON M.CUSTNO=AC.CUSTNO AND M.CUSTNO=DP.CUSTNO" +
                  " WHERE DP.DEPOSITGLCODE='" + ProCode + "' AND DP.CUSTACCNO='" + AccNo + "' AND DP.BRCD='" + brcd + "' ";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return dt;
    }

    public DataTable GetCustNoName(string subglcode, string AccNo, string BRCD)//BRCD ADDED --Abhishek
    {
        try
        {
            sql = "SELECT AC.CUSTNO CUSTNO, AC.ACC_TYPE, AC.OPR_TYPE, M.CUSTNAME   FROM AVS_ACC AC INNER JOIN MASTER M ON M.CUSTNO=AC.CUSTNO WHERE AC.SUBGLCODE='" + subglcode + "' AND AC.ACCNO='" + AccNo + "' and AC.BRCD='" + BRCD + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return dt;
    }

    // Deposit Record Entry
    public int EntryDeposite(string CUSTNO, string CUSTACCNO, string DEPOSITGLCODE, string PRNAMT, string RATEOFINT, string OPENINGDATE, string DUEDATE, string PERIOD, string INTAMT, string MATURITYAMT, string STAGE, string BRCD, string MID, string PCMAC, string INTPAYOUT, string PRDTYPE, string LMSTATUS, string TSGLCD, string TACCNO, string RECEIPTNO, string EDATE, string RecSrno = "0")
    {
        int rtnint = 0;
        try
        {
            sql = "Exec SP_FDCREATION @FLAG='INSERT',@RecSrno='" + RecSrno + "',@CUSTNO='" + CUSTNO + "',@ACCNO='" + CUSTACCNO + "',@DPGLCODE='" + DEPOSITGLCODE + "',@PRIAMT='" + Convert.ToDouble(PRNAMT) + "',@ROI='" + Convert.ToDouble(RATEOFINT) + "',@OPDATE='" + conn.ConvertDate(OPENINGDATE) + "',@DUEDATE='" + conn.ConvertDate(DUEDATE) + "',@PERIOD='" + PERIOD + "',@INTAMT='" + Convert.ToDouble(INTAMT) + "',@MAMOUNT='" + Convert.ToDouble(MATURITYAMT) + "',@BRCD='" + BRCD + "',@MID='" + MID + "',@PCMAC='" + conn.PCNAME() + "',@INTPAY='" + INTPAYOUT + "',@PRDTYPE='" + PRDTYPE + "',@TSGLCD='" + TSGLCD + "',@TACCNO='" + TACCNO + "',@RECNO='" + RECEIPTNO + "',@EDATE='" + conn.ConvertDate(EDATE) + "'";
            //sql = "INSERT INTO DEPOSITINFO (CUSTNO, CUSTACCNO, DEPOSITGLCODE, PRNAMT, RATEOFINT, OPENINGDATE, DUEDATE, PERIOD, INTAMT, MATURITYAMT, STAGE, BRCD, MID, PCMAC, INTPAYOUT, PRDTYPE, LMSTATUS ) VALUES ('" + CUSTNO + "', '" + CUSTACCNO + "', '" + DEPOSITGLCODE + "', '" + Convert.ToDecimal(PRNAMT) + "', '" + Convert.ToDecimal(RATEOFINT) + "', '" + conn.ConvertDate(OPENINGDATE).ToString() + "', '" + conn.ConvertDate(DUEDATE).ToString() + "', '" + PERIOD + "', '" + Convert.ToDecimal(INTAMT) + "', '" + Convert.ToDecimal(MATURITYAMT) + "', '" + STAGE + "', '" + BRCD + "', '" + MID + "', '" + PCMAC + "', '" + INTPAYOUT + "', '" + PRDTYPE + "', '" + LMSTATUS + "')";
            rtnint = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return rtnint;
    }

    // Deposit Record Entry
    public string EntryDepositeIsExists(string CUSTNO, string CUSTACCNO, string DEPOSITGLCODE, string PRNAMT, string RATEOFINT, string OPENINGDATE, string DUEDATE, string PERIOD, string INTAMT, string MATURITYAMT, string STAGE, string BRCD, string MID, string PCMAC, string INTPAYOUT, string PRDTYPE, string LMSTATUS, string TSGLCD, string TACCNO, string RECEIPTNO, string RecSrno = "0")
    {
        string ans = "";
        try
        {
            sql = "Exec SP_FDCREATION @FLAG='ISEXIST',@RecSrno='" + RecSrno + "',@CUSTNO='" + CUSTNO + "',@ACCNO='" + CUSTACCNO + "',@DPGLCODE='" + DEPOSITGLCODE + "',@PRIAMT='" + Convert.ToDouble(PRNAMT) + "',@ROI='" + Convert.ToDouble(RATEOFINT) + "',@OPDATE='" + conn.ConvertDate(OPENINGDATE) + "',@DUEDATE='" + conn.ConvertDate(DUEDATE) + "',@PERIOD='" + PERIOD + "',@INTAMT='" + Convert.ToDouble(INTAMT) + "',@MAMOUNT='" + Convert.ToDouble(MATURITYAMT) + "',@BRCD='" + BRCD + "',@MID='" + MID + "',@PCMAC='" + conn.PCNAME() + "',@INTPAY='" + INTPAYOUT + "',@PRDTYPE='" + PRDTYPE + "',@TSGLCD='" + TSGLCD + "',@TACCNO='" + TACCNO + "',@RECNO='" + RECEIPTNO + "'";
            //sql = "INSERT INTO DEPOSITINFO (CUSTNO, CUSTACCNO, DEPOSITGLCODE, PRNAMT, RATEOFINT, OPENINGDATE, DUEDATE, PERIOD, INTAMT, MATURITYAMT, STAGE, BRCD, MID, PCMAC, INTPAYOUT, PRDTYPE, LMSTATUS ) VALUES ('" + CUSTNO + "', '" + CUSTACCNO + "', '" + DEPOSITGLCODE + "', '" + Convert.ToDecimal(PRNAMT) + "', '" + Convert.ToDecimal(RATEOFINT) + "', '" + conn.ConvertDate(OPENINGDATE).ToString() + "', '" + conn.ConvertDate(DUEDATE).ToString() + "', '" + PERIOD + "', '" + Convert.ToDecimal(INTAMT) + "', '" + Convert.ToDecimal(MATURITYAMT) + "', '" + STAGE + "', '" + BRCD + "', '" + MID + "', '" + PCMAC + "', '" + INTPAYOUT + "', '" + PRDTYPE + "', '" + LMSTATUS + "')";
            ans = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
            ans = "0";
        }
        return ans;
    }
    // Deposit Record Entry
    public int EntryDepositeCBS2(string CUSTNO, string CUSTACCNO, string DEPOSITGLCODE, string PRNAMT, string RATEOFINT, string OPENINGDATE, string DUEDATE, string PERIOD,
        string INTAMT, string MATURITYAMT, string STAGE, string BRCD, string MID, string INTPAYOUT, string PRDTYPE, string LMSTATUS, string TBrCode, string TSGLCD,
        string TACCNO, string RECEIPTNO, string RecSrno = "0")
    {
        int rtnint = 0;
        try
        {
            sql = "INSERT INTO DEPOSITINFO (RECEIPT_NO, CUSTNO, CUSTACCNO, RecSrNo, DEPOSITGLCODE, PRNAMT, RATEOFINT, OPENINGDATE, DUEDATE, PERIOD, INTAMT, MATURITYAMT, " +
                  "STAGE, BRCD, MID, PCMAC, INTPAYOUT, PRDTYPE, LMSTATUS, TrfBrCd, TRFSUBTYPE, TRFACCNO) " +
                  "VALUES ('" + RECEIPTNO + "', '" + CUSTNO + "', '" + CUSTACCNO + "', '" + RecSrno + "', '" + DEPOSITGLCODE + "', '" + PRNAMT + "', '" + RATEOFINT + "', " +
                  "'" + conn.ConvertDate(OPENINGDATE) + "', '" + conn.ConvertDate(DUEDATE) + "', '" + PERIOD + "', '" + INTAMT + "', '" + MATURITYAMT + "', 1001, " +
                  "'" + BRCD + "', '" + MID + "', '" + conn.PCNAME() + "', '" + INTPAYOUT + "', '" + PRDTYPE + "', 1, '" + TBrCode + "', '" + TSGLCD + "', '" + TACCNO + "')";
            rtnint = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtnint;
    }

    // deposit Record Modify
    public int ModifyDeposit(string CUSTNO, string CUSTACCNO, string DEPOSITGLCODE, string PRNAMT, string RATEOFINT, string OPENINGDATE, string DUEDATE, string PERIOD, string INTAMT, string MATURITYAMT, string STAGE, string BRCD, string MID, string PCMAC, string INTPAYOUT, string PRDTYPE, string TSGLCD, string TAccno, string RECEIPTNO, string EDATE, string RecSrno = "0")
    {
        int rtnint = 0;
        try
        {
            sql1 = "Exec SP_FDCREATION    @FLAG='HISTORY_MOD',@RecSrno='" + RecSrno + "',@ACCNO='" + CUSTACCNO + "',@DPGLCODE='" + DEPOSITGLCODE + "',@BRCD='" + BRCD + "',@MID='" + MID + "'";
            rtnint = conn.sExecuteQuery(sql1);

            if (rtnint > 0)
            {
                sql = "Exec SP_FDCREATION @FLAG='UPDATE',@RecSrno='" + RecSrno + "',@ACCNO='" + CUSTACCNO + "',@DPGLCODE='" + DEPOSITGLCODE + "',@PRIAMT='" + Convert.ToDouble(PRNAMT) + "',@ROI='" + Convert.ToDouble(RATEOFINT) + "',@OPDATE='" + conn.ConvertDate(OPENINGDATE) + "',@DUEDATE='" + conn.ConvertDate(DUEDATE) + "',@PERIOD='" + PERIOD + "',@INTAMT='" + Convert.ToDouble(INTAMT) + "',@MAMOUNT='" + Convert.ToDouble(MATURITYAMT) + "',@BRCD='" + BRCD + "',@MID='" + MID + "',@PCMAC='" + conn.PCNAME() + "',@INTPAY='" + INTPAYOUT + "',@PRDTYPE='" + PRDTYPE + "',@TSGLCD='" + TSGLCD + "',@TACCNO='" + TAccno + "',@RECNO='" + RECEIPTNO + "',@EDATE='" + conn.ConvertDate(EDATE) + "'";
                //sql = "UPDATE DEPOSITINFO SET PRNAMT='" + PRNAMT + "' WHERE CUSTNO= '" + CUSTNO + "' AND CUSTACCNO='" + CUSTACCNO + "' AND BRCD = '" + BRCD + "'";
                rtnint = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return rtnint;
    }
    public int DelAutho(string Prdtype, string CustAccNo, string BRCD, string ST, string MID, string EDATE, string RecSrno)
    {
        int RC = 0;
        int rtnint = 0;
        try
        {
            sql1 = "Exec SP_FDCREATION @FLAG='HISTORY_DEL',@RecSrno='" + RecSrno + "',@ACCNO='" + CustAccNo + "',@DPGLCODE='" + Prdtype + "',@BRCD='" + BRCD + "',@MID='" + MID + "',@EDATE='" + conn.ConvertDate(EDATE) + "'";
            rtnint = conn.sExecuteQuery(sql1);
            if (rtnint > 0)
            {
                sql = "Exec SP_FDCREATION @FLAG='DELAUTHO',@RecSrno='" + RecSrno + "',@ACCNO='" + CustAccNo + "',@DPGLCODE='" + Prdtype + "',@BRCD='" + BRCD + "',@MID='" + MID + "',@STAGE='" + ST + "',@EDATE='" + conn.ConvertDate(EDATE) + "'";
                RC = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RC;
    }




    public int DelAuthoCBS2(string Prdtype, string CustAccNo, string BRCD, string ST, string MID, string RecSrno)
    {
        int RC = 0;
        int rtnint = 0;
        try
        {
            //  Move Records into history table
            sql = "Insert Into DEPOSITINFO_HISTORY(CUSTNO, CUSTACCNO, RecSrNo, DEPOSITGLCODE, PRNAMT, RATEOFINT, OPENINGDATE, DUEDATE, PERIOD, INTAMT, MATURITYAMT, LIENMARK, LIENAMOUNT, " +
                  "LOANSUBGLCD, LOANACCNO, LOAN_DISBDATE, TRFACCTYPE, TRFSUBTYPE, TRFACCNO, LMSTATUS, STAGE, BRCD, MID, CID, VID, PCMAC, MOD_DATE, TIME, INTPAYOUT, PRINT_STATUS, " +
                  "TrfBrCd, PRINTBY, PRINT_DATE, PRDTYPE, RECEIPT_NO, PTD, PTM, PTY, LASTINTDATE, PREV_INTDATE) " +
                  "Select CUSTNO, CUSTACCNO, RecSrNo, DEPOSITGLCODE, PRNAMT, RATEOFINT, OPENINGDATE, DUEDATE, PERIOD, INTAMT, MATURITYAMT, LIENMARK, LIENAMOUNT, LOANSUBGLCD, " +
                  "LOANACCNO, LOAN_DISBDATE, TRFACCTYPE, TRFSUBTYPE, TRFACCNO, LMSTATUS, STAGE, BRCD, MID, CID, VID, PCMAC, MOD_DATE, TIME, INTPAYOUT, PRINT_STATUS, " +
                  "TrfBrCd, PRINTBY, PRINT_DATE, PRDTYPE, RECEIPT_NO, PTD, PTM, PTY, LASTINTDATE, PREV_INTDATE " +
                  "From DepositInfo Where BrCd = '" + BRCD + "' And DepositGlCode = '" + Prdtype + "' And CustAccNo = '" + CustAccNo + "' And RecSrNo = '" + RecSrno + "' ";
            rtnint = conn.sExecuteQuery(sql);

            if (rtnint > 0)
            {
                sql = "Update DepositInfo Set Stage = '" + ST + "' " +
                      "Where BrCd = '" + BRCD + "' And DepositGlCode = '" + Prdtype + "' And CustAccNo = '" + CustAccNo + "' And RecSrNo = '" + RecSrno + "'";
                RC = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RC;
    }


    // deposit Record Modify
    public int ModifyDepositCBS2(string CUSTNO, string CUSTACCNO, string DEPOSITGLCODE, string PRNAMT, string RATEOFINT, string OPENINGDATE, string DUEDATE, string PERIOD, string INTAMT,
        string MATURITYAMT, string STAGE, string BRCD, string MID, string INTPAYOUT, string PRDTYPE, string TBrCode, string TSGLCD, string TAccno, string RECEIPTNO, string RecSrno = "0")
    {
        int rtnint = 0;
        try
        {
            //  Move Records into history table
            sql = "Insert Into DEPOSITINFO_HISTORY(CUSTNO, CUSTACCNO, RecSrNo, DEPOSITGLCODE, PRNAMT, RATEOFINT, OPENINGDATE, DUEDATE, PERIOD, INTAMT, MATURITYAMT, LIENMARK, LIENAMOUNT, " +
                  "LOANSUBGLCD, LOANACCNO, LOAN_DISBDATE, TRFACCTYPE, TRFSUBTYPE, TRFACCNO, LMSTATUS, STAGE, BRCD, MID, CID, VID, PCMAC, MOD_DATE, TIME, INTPAYOUT, PRINT_STATUS, " +
                  "TrfBrCd, PRINTBY, PRINT_DATE, PRDTYPE, RECEIPT_NO, PTD, PTM, PTY, LASTINTDATE, PREV_INTDATE) " +
                  "Select CUSTNO, CUSTACCNO, RecSrNo, DEPOSITGLCODE, PRNAMT, RATEOFINT, OPENINGDATE, DUEDATE, PERIOD, INTAMT, MATURITYAMT, LIENMARK, LIENAMOUNT, LOANSUBGLCD, " +
                  "LOANACCNO, LOAN_DISBDATE, TRFACCTYPE, TRFSUBTYPE, TRFACCNO, LMSTATUS, STAGE, BRCD, MID, CID, VID, PCMAC, MOD_DATE, TIME, INTPAYOUT, PRINT_STATUS, " +
                  "TrfBrCd, PRINTBY, PRINT_DATE, PRDTYPE, RECEIPT_NO, PTD, PTM, PTY, LASTINTDATE, PREV_INTDATE " +
                  "From DepositInfo Where BrCd = '" + BRCD + "' And DepositGlCode = '" + DEPOSITGLCODE + "' And CustAccNo = '" + CUSTACCNO + "' And RecSrNo = '" + RecSrno + "' ";
            rtnint = conn.sExecuteQuery(sql);

            if (rtnint > 0)
            {
                // Update Deposit Details
                sql = "Update DepositInfo Set RATEOFINT = '" + RATEOFINT + "', DUEDATE = '" + conn.ConvertDate(DUEDATE) + "', RECEIPT_NO = '" + RECEIPTNO + "', PRNAMT = '" + PRNAMT + "', " +
                      "INTAMT = '" + INTAMT + "', MATURITYAMT = '" + MATURITYAMT + "', PRDTYPE = '" + PRDTYPE + "', PERIOD = '" + PERIOD + "', INTPAYOUT = '" + INTPAYOUT + "', " +
                      "RecSrNo = '" + RecSrno + "', TrfBrCd = '" + TBrCode + "', TRFSUBTYPE = '" + TSGLCD + "', TRFACCNO = '" + TAccno + "', STAGE = '1002' " +
                      "Where BrCd = '" + BRCD + "' And DepositGlCode = '" + DEPOSITGLCODE + "' And CustAccNo = '" + CUSTACCNO + "' And RecSrNo = '" + RecSrno + "' ";
                rtnint = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtnint;
    }

    // Validation or 
    public string CheckPeriod(string procode, string period, string periodtype, string brcd, string CUSTTYPE) //BRCD ADDED --Abhishek
    {
        string IsPValid = "0";
        try
        {
            sql = "select TOP 1 * FROM A50001 WHERE PERIODTYPE = '" + periodtype + "'and TDCUSTTYPE='" + CUSTTYPE + "' AND (PERIODFROM <= '" + period + "' AND PERIODTO >= '" + period + "') AND DEPOSITGL ='" + procode + "' and STAGE<>'1004'";
            DataTable DT = conn.GetDatatable(sql);
            if (DT.Rows.Count > 0)
                IsPValid = "1";
            else
                IsPValid = "0";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return IsPValid;
    }

    // Get Interest Rate
    public float GetIntrestRateADD(string procode, string period, string brcd, string periodtype, bool TF, string TDCUSTTYPE, string EDT = "2017-01-01")
    {
        float rtnf = 0;
        try
        {
            if (TF == true)
            {
                sql = "SELECT top 1 RATE from A50001 WHERE DEPOSITGL = '" + procode + "' AND (PERIODFROM <= '" + period + "' AND PERIODTO >='" + period + "')  AND PERIODTYPE='D' AND STAGE<>'1004' ";
            }
            else
            {

                // sql = "SELECT top 1 RATE from A50001 WHERE DEPOSITGL = '" + procode + "' AND (PERIODFROM <= '" + period + "' AND PERIODTO >='" + period + "')  AND PERIODTYPE='" + periodtype + "' and TDCUSTTYPE='" + TDCUSTTYPE + "' AND STAGE<>'1004'";
                sql = "SELECT top 1 RATE from A50001 A " +
                    " WHERE A.DEPOSITGL = '" + procode + "'  " +
                    " AND (A.PERIODFROM <= '" + period + "' AND A.PERIODTO >='" + period + "')  " +
                    " AND A.PERIODTYPE='" + periodtype + "' " +
                    " AND A.STAGE<>'1004' " +
                    " and A.TDCUSTTYPE='" + TDCUSTTYPE + "' " +
                    " and A.EFFECTDATE=(Select Max(B.EFFECTDATE) from A50001 B " +
                                            " Where B.DEPOSITGL=A.DEPOSITGL and (A.PERIODFROM <= B.PERIODFROM AND A.PERIODTO >=B.PERIODTO) and A.PERIODTYPE=B.PERIODTYPE " +
                    //" and A.STAGE=B.STAGE " +
                                             "and A.TDCUSTTYPE=B.TDCUSTTYPE " +
                                            "and B.EFFECTDATE<='" + conn.ConvertDate(EDT) + "')  ";
            }
            rtn = conn.sExecuteScalar(sql);
            if (rtnf != null)
            {
                rtnf = float.Parse(rtn);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return rtnf;
    }

    public float GetIntrestRate(string procode, string period, string brcd, string periodtype, bool TF)//, string TDCUSTTYPE)
    {
        float rtnf = 0;
        try
        {
            if (TF == true)
            {
                // sql = "SELECT RATE from A50001 WHERE DEPOSITGL = '" + procode + "' AND (PERIODFROM <= '" + period + "' AND PERIODTO >='" + period + "') AND BRCD ='" + brcd + "' AND PERIODTYPE='D'";
                sql = "SELECT top 1 RATE from A50001 WHERE DEPOSITGL = '" + procode + "' AND (PERIODFROM <= '" + period + "' AND PERIODTO >='" + period + "')  AND PERIODTYPE='D' and STAGE<>'1004'";
            }
            else
            {
                //sql = "SELECT RATE from A50001 WHERE DEPOSITGL = '" + procode + "' AND (PERIODFROM <= '" + period + "' AND PERIODTO >='" + period + "') AND BRCD ='" + brcd + "' AND PERIODTYPE='" + periodtype + "'";// and TDCUSTTYPE='" + TDCUSTTYPE + "'";
                sql = "SELECT top 1 RATE from A50001 WHERE DEPOSITGL = '" + procode + "' AND (PERIODFROM <= '" + period + "' AND PERIODTO >='" + period + "')  AND PERIODTYPE='" + periodtype + "' and STAGE<>'1004'";// and TDCUSTTYPE='" + TDCUSTTYPE + "'";
            }
            rtn = conn.sExecuteScalar(sql);
            if (rtnf != null)
            {
                rtnf = float.Parse(rtn);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return rtnf;
    }

    public string GetInterestRateED(string FL, string SFL, string SBGL, string CUSTTYPE, string PERIODTYPE, string PERIOD, string OPDT, bool SN) //Added by abhishek for effective date wise rate 1-11-2017
    {
        //float Rtn = 0;
        try
        {
            if (SN == false)
            {
                sql = " Exec Isp_AVS0083 @Flag='" + FL + "',@SFlag='NoSubgl',@Subglcode='" + SBGL + "',@CustType='" + CUSTTYPE + "',@PeriodType='" + PERIODTYPE + "',@Period='" + PERIOD + "',@OpeningDate='" + conn.ConvertDate(OPDT) + "'";
            }
            else
            {
                sql = " Exec Isp_AVS0083 @Flag='" + FL + "',@SFlag='WithSubgl',@Subglcode='" + SBGL + "',@CustType='" + CUSTTYPE + "',@PeriodType='" + PERIODTYPE + "',@Period='" + PERIOD + "',@OpeningDate='" + conn.ConvertDate(OPDT) + "'";
            }
            sql = conn.sExecuteScalar(sql);


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }


    public string GetInterestRateED(string FL, string SFL, string SBGL, string CUSTTYPE, string PERIODTYPE, string PERIOD, string OPDT, bool SN,string AccNo) //Added by abhishek for effective date wise rate 1-11-2017
    {
        //float Rtn = 0;
        try
        {
            if (SN == false)
            {
                sql = " Exec Isp_AVS0083 @Flag='" + FL + "',@SFlag='NoSubgl',@Subglcode='" + SBGL + "',@CustType='" + CUSTTYPE + "',@PeriodType='" + PERIODTYPE + "',@Period='" + PERIOD + "',@OpeningDate='" + conn.ConvertDate(OPDT) + "'";
            }
            else
            {
                sql = " Exec Isp_AVS0083 @Flag='" + FL + "',@SFlag='WithSubgl',@Subglcode='" + SBGL + "',@CustType='" + CUSTTYPE + "',@PeriodType='" + PERIODTYPE + "',@Period='" + PERIOD + "',@OpeningDate='" + conn.ConvertDate(OPDT) + "', @AccNo='"+AccNo+"'";
            }
            sql = conn.sExecuteScalar(sql);


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public string GetSRNo(string SBGL, string AccNo, string BRCD) //added by prasad to get SrNo
    {
        //float Rtn = 0;
        try
        {


            sql = " select isnull(recsrno,'0') as recsrno  from DepositInfo  where DEPOSITGLCODE='" + SBGL + "' and CUSTACCNO='" + AccNo + "' and BRCD='" + BRCD + "' and STAGE<>'1004'";

            sql = conn.sExecuteScalar(sql);


        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public DataTable GetFrequency(string BRCD, string SUBGL)
    {
        try
        {
            sql = "Select CATEGORY C ,Premature_Case P,Frequency F from DEPOSITGL where brcd='" + BRCD + "' and DEPOSITGLCODE='" + SUBGL + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public string GetPreClosure(string PRDCD, string Period, string PType, string BRCD, string ACCType)
    {
        string RP = "";
        try
        {
            //DEPOSITGL = '" + PRDCD + "' AND change as per sir disscution 16/01/2017
            //sql = "SELECT convert(varchar(10), RATE)+'_'+convert(varchar(10),PENALTY) from A50001 WHERE  (PERIODFROM <= '" + Period + "' AND PERIODTO >='" + Period + "') AND BRCD ='" + BRCD + "' AND PERIODTYPE='" + PType + "' AND  TDCUSTTYPE='" + ACCType + "' AND DEPOSITGL='" + PRDCD + "'";
            sql = "SELECT top 1 convert(varchar(10), RATE)+'_'+convert(varchar(10),PENALTY) from A50001 WHERE  (PERIODFROM <= '" + Period + "' AND PERIODTO >='" + Period + "') AND PERIODTYPE='" + PType + "' AND  TDCUSTTYPE='" + ACCType + "' AND DEPOSITGL='" + PRDCD + "' and STAGE<>'1004'";
            RP = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RP;
    }

    public string GetPreClosure_NoSubgl(string Period, string PType, string BRCD, string ACCType) //Added for If Dasy and Months Rate not get for Deposit Gl then Skip Deposit Gl Condition --Abhishek 03-06-2017
    {
        string RP = "";
        try
        {
            sql = "SELECT top 1 convert(varchar(10), RATE)+'_'+convert(varchar(10),PENALTY) from A50001 A " +
                " inner join DEPOSITGL B on A.BRCD=B.BRCD and A.DEPOSITGL=B.DEPOSITGLCODE  " +
                " WHERE  (A.PERIODFROM <='" + Period + "' AND A.PERIODTO >='" + Period + "')  " +
                " AND A.PERIODTYPE='" + PType + "'  " +
                " AND A.TDCUSTTYPE='" + ACCType + "' " +
                " AND B.CATEGORY='FDS'";

            RP = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RP;
    }


    // get Penal Interest
    public float GetPenalIntrestRate(string procode, string period, string brcd, string periodtype, bool TF)
    {
        float rtnf = 0;
        try
        {
            if (TF == true)
            {
                //sql = "SELECT penalty from A50001 WHERE DEPOSITGL = '" + procode + "' AND (PERIODFROM <= '" + period + "' AND PERIODTO >='" + period + "') AND BRCD ='" + brcd + "' AND PERIODTYPE='D'";
                sql = "SELECT top 1 penalty from A50001 WHERE DEPOSITGL = '" + procode + "' AND (PERIODFROM <= '" + period + "' AND PERIODTO >='" + period + "')  AND PERIODTYPE='D'";
            }
            else
            {
                //sql = "SELECT penalty from A50001 WHERE DEPOSITGL = '" + procode + "' AND (PERIODFROM <= '" + period + "' AND PERIODTO >='" + period + "') AND BRCD ='" + brcd + "' AND PERIODTYPE='" + periodtype + "'";
                sql = "SELECT top 1 penalty from A50001 WHERE DEPOSITGL = '" + procode + "' AND (PERIODFROM <= '" + period + "' AND PERIODTO >='" + period + "')  AND PERIODTYPE='" + periodtype + "'";
            }
            rtn = conn.sExecuteScalar(sql);
            if (rtnf != null)
            {
                rtnf = float.Parse(rtn);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return rtnf;
    }

    // Entry lien Mark
    public int EntryLienMark(string lienprocode, string lienaccno, string lienamount, string procode, string AccNo, string BRCD)//BRCD ADDED --Abhishek
    {
        try
        {
            rtnint = 0;
            sql = "UPDATE DEPOSITINFO SET LIENMARK='Y', LOANSUBGLCD='" + lienprocode + "', LOANACCNO='" + lienaccno + "', LIENAMOUNT='" + lienamount + "' WHERE DEPOSITGLCODE='" + procode + "' AND CUSTACCNO='" + AccNo + "' AND BRCD='" + BRCD + "' AND STAGE ='1001' ";
            rtnint = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return rtnint;
    }

    // entry Transfer details
    public int EntryTransfer(string procode, string AccNo, string lienprocode, string lienAccNo, string BRCD)//BRCD ADDED --Abhishek
    {
        try
        {
            rtnint = 0;
            sql = "UPDATE DEPOSITINFO SET TRFACCTYPE='" + lienprocode + "', TRFSUBTYPE='" + lienprocode + "', TRFACCNO='" + lienAccNo + "' WHERE DEPOSITGLCODE='" + procode + "' AND CUSTACCNO='" + AccNo + "' AND BRCD='" + BRCD + "'  AND STAGE ='1001'";
            rtnint = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return rtnint;
    }
    public int GetDays(string BRCD)//BRCD ADDED --Abhishek
    {
        int result = 0;
        try
        {
            sql = "SELECT listvalue FROM PARAMETER where listfield='FDGROSSDAYS' and BRCD='" + BRCD + "'";
            result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return result;
    }
    public string GetTDAcNo(string glcode, string subgl, string brcd)
    {
        try
        {
            sql = "SELECT MAX(ACCNO) FROM AVS_ACC WHERE GLCODE='" + glcode + "' AND SUBGLCODE='" + subgl + "' AND BRCD='" + brcd + "' AND  ACC_STATUS=1";
            brcd = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return brcd;
    }
    public int UpdateAcc(string gl, string sgl, string ac, string brcd, string EDT, string RECSRNO)
    {
        int Result = 0;
        string sql2 = "";
        try
        {
            if (RECSRNO == "0")
            {
                sql = "update avs_acc set acc_status=3,CLOSINGDATE='" + conn.ConvertDate(EDT) + "' where glcode='" + gl + "' and subglcode='" + sgl + "' and accno='" + ac + "' and brcd='" + brcd + "'";
                //   sql2 = "update DEPOSITINFO SET LMSTATUS=99 WHERE DEPOSITGLCODE=" + sgl + " AND CUSTACCNO=" + ac + " and brcd='" + brcd + "' and isnull(RECSRNO,'0')='" + RECSRNO + "'";
                sql2 = "update DEPOSITINFO SET LMSTATUS=99 WHERE DEPOSITGLCODE=" + sgl + " AND CUSTACCNO=" + ac + " and brcd='" + brcd + "' ";

                Result = conn.sExecuteQuery(sql);
                if (Result > 0)
                {
                    Result = conn.sExecuteQuery(sql2);
                }
            }
            else
            {
                // sql2 = "update DEPOSITINFO SET LMSTATUS=99 WHERE DEPOSITGLCODE=" + sgl + " AND CUSTACCNO=" + ac + " and brcd='" + brcd + "' and isnull(RECSRNO,'0')='" + RECSRNO + "'";
                sql2 = "update DEPOSITINFO SET LMSTATUS=99 WHERE DEPOSITGLCODE=" + sgl + " AND CUSTACCNO=" + ac + " and brcd='" + brcd + "'";

                Result = conn.sExecuteQuery(sql2);

            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int CheckAccount(string Prdtype, string CustAccNo, string BRCD)
    {
        int RC = 0;
        try
        {
            sql = "Exec SP_FDCREATION @FLAG='CHECKDATA',@ACCNO='" + CustAccNo + "',@DPGLCODE='" + Prdtype + "',@BRCD='" + BRCD + "'";
            RC = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RC;
    }




    public int CheckAccountCBS2(string Prdtype, string CustAccNo, string BRCD)
    {
        int RC = 0;
        try
        {
            sql = "Exec SP_FDCREATIONCBS2 @FLAG='CHECKDATA',@ACCNO='" + CustAccNo + "',@DPGLCODE='" + Prdtype + "',@BRCD='" + BRCD + "'";
            RC = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RC;
    }
    public string GetRecNo(string FL, string Prdtype, string CustAccNo, string BRCD)
    {
        int RC = 0;
        try
        {
            sql = "Exec SP_FDCREATION @FLAG='" + FL + "',@BRCD='" + BRCD + "',@DPGLCODE='" + Prdtype + "',@ACCNO='" + CustAccNo + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }





    public string GetRecNoCBS2(string FL, string Prdtype, string CustAccNo, string BRCD)
    {
        int RC = 0;
        try
        {
            sql = "Exec SP_FDCREATIONCBS2 @FLAG='" + FL + "',@BRCD='" + BRCD + "',@DPGLCODE='" + Prdtype + "',@ACCNO='" + CustAccNo + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public string GetIntPaid(string BRCD, string GLCD, string SGLCD, string ACC, string FL)
    {
        try
        {
            sql = "EXEC DepositeClousureState @PRDTCD ='" + SGLCD + "',@GLCD='" + GLCD + "',@ACCNO ='" + ACC + "',@BRCD='" + BRCD + "',@Flag='" + FL + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    //For Shiv samarth ..need to publish for All 25-07-2017
    public DataTable GetLKPData(string SUBGL, string PERIOD, string PERIODTYPE, string DEPOAMT, string EDT)
    {
        try
        {
            sql = "Select top 1 INSTAMT,MATAMT from A50001 where INSTAMT='" + DEPOAMT + "' and DEPOSITGL='" + SUBGL + "' and PERIODTO=" + PERIOD + " and PERIODTYPE='" + PERIODTYPE + "' and STAGE<>'1004' and EFFECTDATE<='" + conn.ConvertDate(EDT) + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }
    public string GetCUMCal(string BRCD, string SUBGL, string CAT)
    {
        try
        {
            sql = "Select INTERESTTYPE2 from DEPOSITGL where CATEGORY='" + CAT + "' and DEPOSITGLCODE='" + SUBGL + "' and BRCD='" + BRCD + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public DataTable GetRDCalc(string FL, string AMT, string CALCTYPE, string PERIODTYPE, string PERIOD, string ROI)
    {
        try
        {
            sql = "EXEC Isp_AVS0012 @Flag='" + FL + "',@Amt='" + AMT + "',@CalcType='" + CALCTYPE + "',@PeriodType='" + PERIODTYPE + "',@Period='" + PERIOD + "',@Rate='" + ROI + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }
    public string GetAccName(string ACNO, string AT, string BRCD)
    {
        try
        {
            sql = "SELECT M.CUSTNAME+'-'+ Convert(varchar(10),AC.GLCODE)+'-'+ Convert(varchar(10),CONVERT(INT,M.CUSTNO)) FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO  WHERE AC.SUBGLCODE='" + AT + "' AND AC.BRCD='" + BRCD + "' AND AC.ACCNO='" + ACNO + "'";////ADDED CONVET TO INT BY ANKITA ON 07/07/2017
            ACNO = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ACNO;

    }
    public DataTable TDACase(string Flag, string Edt, string ToPeriod, string Amt, string Subgl, string PType)
    {
        try
        {
            sql = "Exec Isp_TDACase @Flag='" + Flag + "',@Edt='" + conn.ConvertDate(Edt) + "',@ToPeriod='" + ToPeriod + "',@Amount='" + Amt + "',@Subglcode='" + Subgl + "',@PeriodType='" + PType + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }
}