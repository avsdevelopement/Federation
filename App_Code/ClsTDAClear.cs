using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public class ClsTDAClear
{
    string sql = "", sResult="";
    DataTable dt = new DataTable();
    DbConnection conn = new DbConnection();
    ClsEncryptValue Enn = new ClsEncryptValue();
    string result = "";
    int rsltint = 0;
    double resultdouble = 0;

    public ClsTDAClear()
    {

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
    // Get Deposit form data all by ProCode and AccNo for GLCode 5
    public DataTable GetAllFieldData(string ProCode, string AccNo, string brcd, string etrydate)
    {
        try
        {
            sql = " SELECT AC.ACC_STATUS,DP.LMSTATUS,M.CUSTNO, M.CUSTNAME, DP.CUSTACCNO, ISNULL(AC.ACC_TYPE,0) ACC_TYPE, ISNULL(AC.OPR_TYPE,0) OPR_TYPE, DP.OPENINGDATE, DP.INTPAYOUT, DP.PRNAMT, DP.PRDTYPE, DP.PERIOD, DP.RATEOFINT, DP.INTAMT, DP.MATURITYAMT, DP.DUEDATE," +
              "isnull((case when Convert(Datetime,DP.LASTINTDATE)<Convert(Datetime,DP.OPENINGDATE) then DP.OPENINGDATE else DP.LASTINTDATE end) ,DP.OPENINGDATE)  LASTINTDATE from DEPOSITINFO DP " +
              " INNER JOIN AVS_ACC AC ON AC.ACCNO=DP.CUSTACCNO AND DP.BRCD = AC.BRCD AND AC.SUBGLCODE=DP.DEPOSITGLCODE " +
              " INNER JOIN MASTER M ON M.CUSTNO=AC.CUSTNO" +
              " WHERE DP.BRCD='" + brcd + "'  AND DP.CUSTACCNO = '" + AccNo + "' AND DP.DEPOSITGLCODE='" + ProCode + "' Order by DP.LMSTATUS";
              
            //AND LMSTATUS<>99 ";
            //AND DP.LMSTATUS='1'";
            //AND DP.DUEDATE <= '" + conn.ConvertDate(etrydate) + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return dt;
    }



    public string GetAccName(string AccNo, string PrCode, string BrCode)
    {
        try
        {
            sql = "Select M.CustName +'-'+ ConVert(VarChar(10), AC.GlCode) +'-'+ ConVert(VarChar(10), ConVert(BigInt, M.CustNo)) FROM AVS_ACC AC " +
                  "Inner Join Master M On AC.CUSTNO = M.CUSTNO " +
                  "Where AC.BRCD = '" + BrCode + "' And AC.SUBGLCODE = '" + PrCode + "' And AC.ACCNO='" + AccNo + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
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


    public string GetAccStage(string BrCode, string PrCode, string AccNo, string RecSrno = "0")
    {
        try
        {
            sql = "SELECT STAGE FROM AVS_ACC WHERE BRCD='" + BrCode + "' AND SUBGLCODE = '" + PrCode + "' AND ACCNO = '" + AccNo + "' AND STAGE <> 1004";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }


    public string PostM(string FL, string Brcd, string MId, string Edt)
    {
        try
        {
            sql = "Exec ISP_AVS0175 @Flag='" + FL + "',@Brcd='" + Brcd + "',@Mid='" + MId + "',@Edt='" + conn.ConvertDate(Edt) + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public DataTable GetAllFieldData_DP(string ProCode, string AccNo, string brcd, string etrydate)
    {
        try
        {
            sql = " SELECT AC.ACC_STATUS,AC.ACC_STATUS,M.CUSTNO, M.CUSTNAME, AC.ACCNO, ISNULL(AC.ACC_TYPE,0) ACC_TYPE, ISNULL(AC.OPR_TYPE,0) OPR_TYPE, AC.OPENINGDATE, DP.INTPAYOUT, '0' PRNAMT, 'MM' PRDTYPE, '0' PERIOD, '0' INTAMT, '0' MATURITYAMT, '2099-01-01' DUEDATE," +
              "isnull((case when Convert(Datetime,AC.LASTINTDT)<Convert(Datetime,AC.OPENINGDATE) then AC.OPENINGDATE else AC.LASTINTDT end) ,AC.OPENINGDATE)  LASTINTDATE from AVS_ACC AC " +
              " Left JOIN DEPOSITINFO DP ON AC.ACCNO=DP.CUSTACCNO AND DP.BRCD = AC.BRCD AND AC.SUBGLCODE=DP.DEPOSITGLCODE " +
              " Left JOIN MASTER M ON M.CUSTNO=AC.CUSTNO" +
              " WHERE AC.BRCD='" + brcd + "'  AND AC.ACCNO= '" + AccNo + "' AND AC.SUBGLCODE='" + ProCode + "' " +
              " Order by DP.LMSTATUS";

            //AND LMSTATUS<>99 ";
            //AND DP.LMSTATUS='1'";
            //AND DP.DUEDATE <= '" + conn.ConvertDate(etrydate) + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return dt;
    }



    public DataTable GetAllFieldData_DPCBS2(string ProCode, string AccNo, string brcd, string etrydate, string RECSRNO = "0")
    {
        try
        {
            sql = "Select Top 1 IsNull(JJ.CUSTNAME, 'NA') As JointName, ConVert(VarChar(100), Lno.Name) +'-'+ ConVert(VarChar(100), AC.ACC_TYPE) As NAME, AC.ACC_STATUS, M.CUSTNO, M.CUSTNAME, " +
              "AC.ACCNO, IsNull(AC.ACC_TYPE, 0) As ACC_TYPE, IsNull(AC.OPR_TYPE, 0) As OPR_TYPE, ConVert(VarChar(10), AC.OPENINGDATE, 103) As OPENINGDATE, DP.INTPAYOUT, '0' As PRNAMT, " +
              "'MM' As PRDTYPE, '0' As PERIOD, '0' As INTAMT, '0' As MATURITYAMT, '01/01/2099' As DUEDATE, " +
              "ConVert(VarChar(10), IsNull((case when ConVert(Datetime, AC.LASTINTDT) < ConVert(Datetime, AC.OPENINGDATE) then AC.OPENINGDATE else AC.LASTINTDT end), AC.OPENINGDATE), 103) As LASTINTDATE " +
              "From AVS_ACC AC " +
              "Left JOIN DEPOSITINFO DP ON AC.ACCNO = DP.CUSTACCNO And DP.BRCD = AC.BRCD And AC.SUBGLCODE = DP.DEPOSITGLCODE " +
              "Left JOIN MASTER M ON M.CUSTNO = AC.CUSTNO " +
              "LEFT JOIN (SELECT DESCRIPTION NAME, SRNO ID FROM Lookupform1 WHERE LNO = '1017') LNO ON LNO.ID = AC.ACC_TYPE " +
              "Left join JOINT JJ ON AC.ACCNO = JJ.ACCNO And DP.BRCD = JJ.BRCD And AC.SUBGLCODE = JJ.SUBGLCODE " +
              "Where AC.BRCD = '" + brcd + "'  And AC.ACCNO = '" + AccNo + "' And AC.SUBGLCODE = '" + ProCode + "' And IsNull(DP.RECSRNO, '0') = '" + RECSRNO + "' " +
              "Order By DP.LMSTATUS";

            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public string CheckCount(string FL, string Mid)
    {
        try
        {
            sql = "Exec ISP_AVS0175 @Flag='" + FL + "',@Mid='" + Mid + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }



    public int DeleteTemp(string MID)
    {
        int rr = 0;
        try
        {
            sql = "Exec ISP_AVS0175 @Flag='Delete',@Mid='" + MID + "'";
            rr = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rr;
    }

    //Added By amolb ON 2017-01-14 for update last interest date
    public int UpdateLastIntDate(string EntryDate, string BrCode, string SglCode, string AccNo, string CustNo, string RECSRNO)
    {
        try
        {

            sql = "UPDATE DEPOSITINFO SET  PREV_INTDATE=LASTINTDATE,LASTINTDATE= '" + conn.ConvertDate(EntryDate).ToString() + "'," +
                  "MOD_DATE= '" + conn.ConvertDate(EntryDate).ToString() + "' WHERE BRCD = '" + BrCode + "' AND CUSTNO = '" + CustNo + "' AND DEPOSITGLCODE = '" + SglCode + "' AND CUSTACCNO = '" + AccNo + "' and RECSRNO='" + RECSRNO + "'";
            rsltint = conn.sExecuteQuery(sql);

            if (RECSRNO != "" && RECSRNO == "0") // added on 20-04-18 fro mamco req abhishek 
            {
                sql = "UPDATE AVS_ACC SET LASTINTDT = '" + conn.ConvertDate(EntryDate).ToString() + "' WHERE BRCD = '" + BrCode + "' AND CUSTNO = '" + CustNo + "' AND SUBGLCODE = '" + SglCode + "' AND ACCNO = '" + AccNo + "'";
                rsltint = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rsltint;
    }



    public int BindMGrid(GridView GD, string FL, string Mid)
    {
        try
        {
            sql = "Exec ISP_AVS0175 @Flag='" + FL + "',@Mid='" + Mid + "'";
            rsltint = conn.sBindGrid(GD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rsltint;
    }


    public DataTable GetIntAmountMA(string BRCD, string MPFLAG, string SUBGL, string ACCNO, string AMOUNT, string RATE, string ODT, string DDT, string LDT, string EDT, string TDATYPE, string RECSRNO)
    {
        try
        {
            sql = "Exec Isp_AVS0013 @Brcd='" + BRCD + "',@TDAType='" + TDATYPE + "',@Flag='" + MPFLAG + "',@LDate='" + conn.ConvertDate(LDT) + "',@EDate='" + conn.ConvertDate(EDT) + "',@Subgl='" + SUBGL + "',@Accno='" + ACCNO + "',@RecSrno='" + RECSRNO + "',@Rate='" + RATE + "',@Amt='" + AMOUNT + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    // Get Deposit form data all by ProCode and AccNo for GLCode 5
    public DataTable GetAllFieldData(string ProCode, string AccNo, string brcd, string etrydate, string RECSRNO = "0")
    {
        try
        {
            sql = "Select top 1 IsNull(JJ.CUSTNAME, 'NA') As JointName, ConVert(VarChar(100), Lno.Name) +'-'+ ConVert(VarChar(100), AC.ACC_TYPE) As NAME, IsNull(DP.RECSRNO, '0') As RECSRNO, " +
              "AC.ACC_STATUS, DP.LMSTATUS, M.CUSTNO, M.CUSTNAME, DP.CUSTACCNO, IsNull(AC.ACC_TYPE, 0) As ACC_TYPE, IsNull(AC.OPR_TYPE, 0) As OPR_TYPE, ConVert(Varchar(10), DP.OPENINGDATE, 103) As OPENINGDATE, " +
              "DP.INTPAYOUT, DP.PRNAMT, DP.PRDTYPE, DP.PERIOD, DP.RATEOFINT, DP.INTAMT, DP.MATURITYAMT, ConVert(VarChar(10), DP.DUEDATE, 103) DUEDATE, " +
              "ConVert(VarChar(10), IsNull((case when ConVert(Datetime, DP.LASTINTDATE) < ConVert(Datetime, DP.OPENINGDATE) then DP.OPENINGDATE else DP.LASTINTDATE end), DP.OPENINGDATE), 103) As LASTINTDATE " +
              "from DEPOSITINFO DP " +
              "INNER JOIN AVS_ACC AC ON AC.ACCNO = DP.CUSTACCNO And DP.BRCD = AC.BRCD And AC.SUBGLCODE = DP.DEPOSITGLCODE " +
              "INNER JOIN MASTER M ON M.CUSTNO = AC.CUSTNO " +
              "LEFT JOIN (SELECT DESCRIPTION NAME, SRNO ID FROM Lookupform1 WHERE LNO = '1017') LNO ON LNO.ID = AC.ACC_TYPE " +
              "Left join JOINT JJ ON AC.ACCNO = JJ.ACCNO And DP.BRCD = JJ.BRCD And AC.SUBGLCODE = JJ.SUBGLCODE " +
              "Where DP.BRCD = '" + brcd + "' And DP.CUSTACCNO = '" + AccNo + "' And DP.DEPOSITGLCODE = '" + ProCode + "' And IsNull(DP.RECSRNO, '0') = '" + RECSRNO + "' " +
              "Order by DP.LMSTATUS";

            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }



    public DataTable GetIntAmountQISMA(string BRCD, string MPFLAG, string SUBGL, string ACCNO, string AMOUNT, string RATE, string ODT, string DDT, string LDT, string EDT, string TDATYPE, string RECSRNO)
    {
        try
        {
            sql = "Exec Isp_AVS0142 @Brcd='" + BRCD + "',@TDAType='" + TDATYPE + "',@Flag='" + MPFLAG + "',@LDate='" + conn.ConvertDate(LDT) + "',@EDate='" + conn.ConvertDate(EDT) + "',@Subgl='" + SUBGL + "',@Accno='" + ACCNO + "',@RecSrno='" + RECSRNO + "',@Rate='" + RATE + "',@Amt='" + AMOUNT + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }



    public string GetLastIntDate(string BrCode, string CustNo, string GlCode, string SglCode, string AccNo, string RecSrno = "0")
    {
        try
        {
            //sql = "SELECT CONVERT(VARCHAR(10), ISNULL(LASTINTDT, OPENINGDATE), 103) AS LASTINTDT FROM AVS_ACC WHERE BRCD = '" + BrCode + "' AND CUSTNO = '" + CustNo + "' AND GLCODE = '" + GlCode + "' AND SUBGLCODE = '" + SglCode + "' AND ACCNO = '" + AccNo + "' and RECSRNO='" + RecSrno + "'";
            sql = "SELECT case when LASTINTDATE<OPENINGDATE then CONVERT(VARCHAR(10), OPENINGDATE, 103) else CONVERT(VARCHAR(10), ISNULL(LASTINTDATE, OPENINGDATE), 103) end AS LASTINTDT FROM DEPOSITINFO  " +
                " WHERE BRCD = '" + BrCode + "'  " +
                " AND CUSTNO = '" + CustNo + "'  " +
                " AND DEPOSITGLCODE = '" + SglCode + "' " +
                " AND CUSTACCNO = '" + AccNo + "' " +
                " and RECSRNO='" + RecSrno + "'";
            result = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public string TDSAmount(string BRCD, string Subglcode, string Accno, string Recno, string Edt, string CurrInt, string sFlag = "")
    {
        try
        {
            sql = "Exec Isp_TDAAmount @SFlag = '" + sFlag + "', @Brcd = '" + BRCD + "', @Subgl = '" + Subglcode + "', @Accno = '" + Accno + "', @RecSrno = '" + Recno + "', @Edt = '" + conn.ConvertDate(Edt) + "', @CurrInterestAmt = '" + CurrInt + "' ";
            result = conn.sExecuteScalar(sql);

            if ((result == null) || (result == ""))
                result = "0";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
    public DataTable GetLoanTotalAmount(string BrCode, string PrCode, string AccNo, string EDate, string Flag)
    {
        try
        {
            sql = "Exec RptAllLoanBalances '" + BrCode + "','" + PrCode + "','" + AccNo + "','" + conn.ConvertDate(EDate).ToString() + "', @sFlag = '" + Flag + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }


    public DataTable TDSAmount_Grid(string BRCD, string Subglcode, string Accno, string Recno, string Edt, string CurrInt, string sFlag = "")
    {
        try
        {
            sql = "Exec Isp_TDAAmount @SFlag = '" + sFlag + "', @Brcd = '" + BRCD + "', @Subgl = '" + Subglcode + "', @Accno = '" + Accno + "', @RecSrno = '" + Recno + "', @Edt = '" + conn.ConvertDate(Edt) + "', @CurrInterestAmt = '" + CurrInt + "' ";
            dt = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public int Add(string FL, string Brcd, string RefBrcd, string Glcode, string Subgl, string Accno, string RecSrno, string Amt, string Edt, string Trxtype, string P1, string Instno, string Instdate,
          string Mid, string PayMast, string PmtMode, string Acti, string Custno, string CustName, string Headdesc = "0", string TbFlag = "NL", string LoanSubgl = "0")
    {
        int rr = 0;
        try
        {
            string F1 = Enn.encrypt(Mid);

            sql = "Exec ISP_AVS0175 @Flag='" + FL + "',@Brcd='" + Brcd + "',@RefBrcd='" + RefBrcd + "',@Glcode='" + Glcode + "',@Subglcode='" + Subgl + "',@Accno='" + Accno + "',@Amount='" + Amt + "',@Edt='" + conn.ConvertDate(Edt) + "',@TrxType='" + Trxtype + "',@P1='" + P1 + "', " +
            " @Instno='" + Instno + "',@InstDate='" + conn.ConvertDate(Instdate) + "',@Mid='" + Mid + "',@PayMast='" + PayMast + "',@PmtMode='" + PmtMode + "',@Activity='" + Acti + "',@Custno='" + Custno + "',@CustName='" + CustName + "',@Recsrno='" + RecSrno + "',@F1='" + F1 + "' " +
            " ,@HeadDesc='" + Headdesc + "',@TbFlag='" + TbFlag + "',@Loancode='" + LoanSubgl + "'";

            rr = conn.sExecuteQuery(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rr;
    }



    //Last Int date Update
    public int UpdateLoanIntDate(string BrCode, string SGlCode, string AccNo, string EDate, string Mid)
    {
        try
        {
            sql = "Select Distinct Int_App From LoanGl Where BrCd = '2' And LoanGlCode = '204'";
            result = conn.sExecuteScalar(sql);

            if (result != null && result == "1")
            {
                sql = "Update LoanInfo Set Prev_IntDt = LastIntDate, LastIntDate = '" + conn.ConvertDate(EDate).ToString() + "', Mod_Date = '" + conn.ConvertDate(EDate).ToString() + "' Where BrCd= '" + BrCode + "' And LOANGLCODE = '" + SGlCode + "' And CUSTACCNO = '" + AccNo + "'";
                rsltint = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rsltint;
    }

    // Get Deposit form data all by ProCode and AccNo for GLCode 5
    public DataTable GetAllFieldDataPre(string ProCode, string AccNo, string brcd, string etrydate)
    {
        try
        {
            sql = " SELECT M.CUSTNO, M.CUSTNAME, DP.CUSTACCNO, AC.ACC_TYPE, AC.OPR_TYPE, DP.OPENINGDATE, DP.INTPAYOUT, DP.PRNAMT, DP.PRDTYPE, DP.PERIOD, DP.RATEOFINT, DP.INTAMT, DP.MATURITYAMT, DP.DUEDATE from DEPOSITINFO DP " +
                  " INNER JOIN AVS_ACC AC ON AC.ACCNO=DP.CUSTACCNO AND DP.BRCD = AC.BRCD AND AC.SUBGLCODE=DP.DEPOSITGLCODE " +
                  " INNER JOIN MASTER M ON M.CUSTNO=AC.CUSTNO " +
                  " WHERE DP.BRCD='" + brcd + "'  AND DP.CUSTACCNO = '" + AccNo + "' AND DP.DEPOSITGLCODE='" + ProCode + "' AND DP.LMSTATUS='1' AND DP.DUEDATE >= '" + conn.ConvertDate(etrydate) + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return dt;
    }

    public string GetPrincipelPayble(string ProCode, string AccNo, string brcd, string etrydate)
    {
        try
        {
            sql = "";
            result = "";
            result = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return result;
    }

    public string GetInterestPayble(string ProCode, string AccNo, string brcd, string etrydate)
    {
        try
        {
            sql = "";
            result = "";
            result = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return result;
    }

    // Get opening balance
    public string GetOpeningBal(string ProCode, string AccNo, string brcd, string entrydate)
    {
        try
        {
            string[] arraydt = entrydate.Split('/');
            string TBLNAME = "AVSB_" + arraydt[2].ToString() + arraydt[1].ToString();
            sql = "select AMOUNT FROM " + TBLNAME + " WHERE TRXTYPE ='3' AND SUBGLCODE='" + ProCode + "' AND GLCODE='5' AND ACCNO='" + AccNo + "' AND BRCD='" + brcd + "'";
            result = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return result;
    }

    // Post voucher TO AVSM
    public int PostVouchTDAClosur(string TBLNAME, string ENTRYDATE, string POSTINGDATE, string FUNDINGDATE, string SUBGLCODE, string ACCNO, string AMOUNT, string TRXTYPE, string ACTIVITY, string PMTMODE, string STAGE, string BRCD, string MID, string PCMAC, string CUSTNO, string CUSTNAME)
    {
        try
        {
            rsltint = 0;
            sql = " INSERT INTO " + TBLNAME + " (ENTRYDATE, POSTINGDATE, FUNDINGDATE, GLCODE, SUBGLCODE, ACCNO, AMOUNT, TRXTYPE, ACTIVITY, PMTMODE, STAGE, BRCD, MID, PCMAC, CUSTNO, CUSTNAME, SYSTEMDATE) " +
                  " VALUES ('" + ENTRYDATE + "', '" + POSTINGDATE + "', '" + FUNDINGDATE + "', '10', '" + SUBGLCODE + "', '" + ACCNO + "', '" + AMOUNT + "', '" + TRXTYPE + "', '" + ACTIVITY + "', '" + PMTMODE + "', '" + STAGE + "', '" + BRCD + "', '" + MID + "', '" + PCMAC + "', '" + CUSTNO + "', '" + CUSTNAME + "', SYSDATE)";

            string sql1 = " INSERT INTO " + TBLNAME + " (ENTRYDATE, POSTINGDATE, FUNDINGDATE, GLCODE, SUBGLCODE, ACCNO, AMOUNT, TRXTYPE, ACTIVITY, PMTMODE, STAGE, BRCD, MID, PCMAC, CUSTNO, CUSTNAME, SYSTEMDATE) " +
                          " VALUES ('" + ENTRYDATE + "', '" + POSTINGDATE + "', '" + FUNDINGDATE + "', '10', '" + SUBGLCODE + "', '" + ACCNO + "', '" + AMOUNT + "', '2', '10', 'TR-NFA', '1001', '" + BRCD + "', '" + MID + "', '" + PCMAC + "', '" + CUSTNO + "', '" + CUSTNAME + "', SYSDATE)";

            string sqlplaccno = "SELECT PLACCNO FROM GLMAST WHERE GLCODE=5 and SUBGLCODE='" + SUBGLCODE + "' and BRCD='" + BRCD + "'"; //BRCD ADDED --Abhishek
            string PLACC = conn.sExecuteScalar(sqlplaccno);

            string sql2 = " INSERT INTO " + TBLNAME + " (ENTRYDATE, POSTINGDATE, FUNDINGDATE, GLCODE, SUBGLCODE, ACCNO, AMOUNT, TRXTYPE, ACTIVITY, PMTMODE, STAGE, BRCD, MID, PCMAC, CUSTNO, CUSTNAME, SYSTEMDATE) " +
                          " VALUES ('" + ENTRYDATE + "', '" + POSTINGDATE + "', '" + FUNDINGDATE + "', '100', '0', '" + PLACC + "', '" + AMOUNT + "', '2', '10', 'TR-INT', '1001', '" + BRCD + "', '" + MID + "', '" + PCMAC + "', '" + CUSTNO + "', '" + CUSTNAME + "', SYSDATE)";

            string sql3 = " INSERT INTO " + TBLNAME + " (ENTRYDATE, POSTINGDATE, FUNDINGDATE, GLCODE, SUBGLCODE, ACCNO, AMOUNT, TRXTYPE, ACTIVITY, PMTMODE, STAGE, BRCD, MID, PCMAC, CUSTNO, CUSTNAME, SYSTEMDATE) " +
                          " VALUES ('" + ENTRYDATE + "', '" + POSTINGDATE + "', '" + FUNDINGDATE + "', '5', '" + SUBGLCODE + "', '" + ACCNO + "', '" + AMOUNT + "', '1', '10', 'TR-NFA', '1001', '" + BRCD + "', '" + MID + "', '" + PCMAC + "', '" + CUSTNO + "', '" + CUSTNAME + "', SYSDATE)";

            conn.sExecuteQuery(sql);
            conn.sExecuteQuery(sql1);
            conn.sExecuteQuery(sql2);
            conn.sExecuteQuery(sql3);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return rsltint;
    }

    public int DelRecord(string FL, string Id)
    {
        try
        {
            sql = "Exec ISP_AVS0175 @Flag='" + FL + "',@Id='" + Id + "'";
            rsltint = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rsltint;
    }


    public string GetRec_Status(string FL, string Brcd, string Subgl, string Accno, string Edt)
    {
        try
        {
            sql = "Exec Isp_TDADetails @Flag='" + FL + "',@Brcd='" + Brcd + "',@Subglcode='" + Subgl + "',@Accno='" + Accno + "',@Edt='" + conn.ConvertDate(Edt) + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }


    // Payment by Cash
    public int PaymentEntryCash(string TBLNAME, string ENTRYDATE, string POSTINGDATE, string FUNDINGDATE, string SUBGLCODE, string ACCNO, string AMOUNT, string TRXTYPE, string ACTIVITY, string PMTMODE, string STAGE, string BRCD, string MID, string PCMAC, string CUSTNO, string CUSTNAME)
    {
        try
        {
            // Debit
            sql = " INSERT INTO " + TBLNAME + " (ENTRYDATE, POSTINGDATE, FUNDINGDATE, GLCODE, SUBGLCODE, ACCNO, AMOUNT, TRXTYPE, ACTIVITY, PMTMODE, STAGE, BRCD, MID, PCMAC, CUSTNO, CUSTNAME, SYSTEMDATE) " +
                   " VALUES ('" + ENTRYDATE + "', '" + POSTINGDATE + "', '" + FUNDINGDATE + "', '99', '0', '99', '" + AMOUNT + "', '1', '" + ACTIVITY + "', 'CASH', '1001', '" + BRCD + "', '" + MID + "', '" + PCMAC + "', '" + CUSTNO + "', '" + CUSTNAME + "', SYSDATE)";

            // Credit
            string sql1 = " INSERT INTO " + TBLNAME + " (ENTRYDATE, POSTINGDATE, FUNDINGDATE, GLCODE, SUBGLCODE, ACCNO, AMOUNT, TRXTYPE, ACTIVITY, PMTMODE, STAGE, BRCD, MID, PCMAC, CUSTNO, CUSTNAME, SYSTEMDATE) " +
                          " VALUES ('" + ENTRYDATE + "', '" + POSTINGDATE + "', '" + FUNDINGDATE + "', '10', '" + SUBGLCODE + "', '" + ACCNO + "', '" + AMOUNT + "', '2', '" + ACTIVITY + "', 'CASH', '1001', '" + BRCD + "', '" + MID + "', '" + PCMAC + "', '" + CUSTNO + "', '" + CUSTNAME + "', SYSDATE)";
            conn.sExecuteQuery(sql);
            conn.sExecuteQuery(sql1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return rsltint;
    }

    // Payment by Transfer
    public int PaymentEntryTrans(string TBLNAME, string ENTRYDATE, string POSTINGDATE, string FUNDINGDATE, string SUBGLCODE, string ACCNO, string AMOUNT, string TRXTYPE, string ACTIVITY, string PMTMODE, string STAGE, string BRCD, string MID, string PCMAC, string CUSTNO, string CUSTNAME)
    {
        try
        {
            // Debit
            sql = " INSERT INTO " + TBLNAME + " (ENTRYDATE, POSTINGDATE, FUNDINGDATE, GLCODE, SUBGLCODE, ACCNO, AMOUNT, TRXTYPE, ACTIVITY, PMTMODE, STAGE, BRCD, MID, PCMAC, CUSTNO, CUSTNAME, SYSTEMDATE) " +
                   " VALUES ('" + ENTRYDATE + "', '" + POSTINGDATE + "', '" + FUNDINGDATE + "', '10', '" + SUBGLCODE + "', '" + ACCNO + "', '" + AMOUNT + "', '1', '" + ACTIVITY + "', '" + PMTMODE + "', '1001', '" + BRCD + "', '" + MID + "', '" + PCMAC + "', '" + CUSTNO + "', '" + CUSTNAME + "', SYSDATE)";

            // Credit
            string sql1 = " INSERT INTO " + TBLNAME + " (ENTRYDATE, POSTINGDATE, FUNDINGDATE, GLCODE, SUBGLCODE, ACCNO, AMOUNT, TRXTYPE, ACTIVITY, PMTMODE, STAGE, BRCD, MID, PCMAC, CUSTNO, CUSTNAME, SYSTEMDATE) " +
                          " VALUES ('" + ENTRYDATE + "', '" + POSTINGDATE + "', '" + FUNDINGDATE + "', '10', '" + SUBGLCODE + "', '" + ACCNO + "', '" + AMOUNT + "', '2', '" + ACTIVITY + "', '" + PMTMODE + "', '" + STAGE + "', '" + BRCD + "', '" + MID + "', '" + PCMAC + "', '" + CUSTNO + "', '" + CUSTNAME + "', SYSDATE)";

            conn.sExecuteQuery(sql);
            conn.sExecuteQuery(sql1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return rsltint;
    }

    // gete Interest Rate
    public double GetIntrstRate()
    {
        try
        {
            sql = "s";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return resultdouble;
    }

    public string GetSetNo(string subglcode, string brcd)
    {
        try
        {
            sql = "SELECT glcode FROM GLMAST where Subglcode='" + subglcode + "' and brcd='" + brcd + "'";
            result = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return result;
    }

    //Added By amolb ON 2017-01-14 for update last interest date
    public int UpdateLastIntDate(string EntryDate,string BrCode, string SglCode, string AccNo, string CustNo)
    {
        try
        {
            //sql = "UPDATE DEPOSITINFO SET PREV_INTDATE=ISNULL((SELECT top 1 LASTINTDATE FROM DEPOSITINFO WHERE DEPOSITGLCODE='" + SglCode + "' AND BRCD='" + BrCode + "' AND CUSTACCNO='" + AccNo + "' AND LMSTATUS=1), " +
            //       " (SELECT top 1 OPENINGDATE FROM DEPOSITINFO WHERE DEPOSITGLCODE='" + SglCode + "' AND BRCD='" + BrCode + "' AND CUSTACCNO='" + AccNo + "' AND LMSTATUS=1)) " +
            //       "  WHERE DEPOSITGLCODE='" + SglCode + "' AND BRCD='" + BrCode + "' AND CUSTACCNO='" + AccNo + "' AND LMSTATUS=1";
            //rsltint = conn.sExecuteQuery(sql);

            sql = "UPDATE DEPOSITINFO SET  PREV_INTDATE=LASTINTDATE,LASTINTDATE= '" + conn.ConvertDate(EntryDate).ToString() + "',"+
                  "MOD_DATE= '" + conn.ConvertDate(EntryDate).ToString() + "' WHERE BRCD = '" + BrCode + "' AND CUSTNO = '" + CustNo + "' AND DEPOSITGLCODE = '" + SglCode + "' AND CUSTACCNO = '" + AccNo + "'";
            rsltint = conn.sExecuteQuery(sql);

            sql = "UPDATE AVS_ACC SET LASTINTDT = '" + conn.ConvertDate(EntryDate).ToString() + "' WHERE BRCD = '" + BrCode + "' AND CUSTNO = '" + CustNo + "' AND SUBGLCODE = '" + SglCode + "' AND ACCNO = '" + AccNo + "'";
            rsltint = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rsltint;
    }

    public string GetLastIntDate(string BrCode, string CustNo, string GlCode, string SglCode, string AccNo)
    {
        try
        {
            sql = "SELECT CONVERT(VARCHAR(10), ISNULL(LASTINTDT, OPENINGDATE), 103) AS LASTINTDT FROM AVS_ACC WHERE BRCD = '" + BrCode + "' AND CUSTNO = '" + CustNo + "' AND GLCODE = '" + GlCode + "' AND SUBGLCODE = '" + SglCode + "' AND ACCNO = '" + AccNo + "'";
            result = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public DataTable GetLoanDetails(string BrCode, string SubGlCode, string AccNo, string EDate)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Exec RptLoanDetailsInfo @BrCode = '" + BrCode + "', @SubGlCode = '" + SubGlCode + "', @AccNo = '" + AccNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = 'DLI'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string Getaccno(string AT, string BRCD, string GLCD)
    {
        try
        {
            sql = " SELECT (CONVERT (VARCHAR(10),GLCODE)) +'_'+ GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }
    public DataTable GetLKPDetails(string PERIOD, string AMT,string FL,string ROI)
    {
        try
        {
           // sql = "EXEC SP_CALCU_LKP @PERIOD='" + PERIOD + "',@AMOUNT='" + AMT + "'";
            sql = "EXEC SP_CALCU_LKP @FLAG='"+FL+"',@ROI='"+ROI+"',@PERIOD='"+PERIOD+"',@AMOUNT='"+AMT+"'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }
    public string GetTDACategory(string BRCD, string SUBGLCODE)
    {
        try
        {
            sql = "SELECT TOP 1 CATEGORY FROM DEPOSITGL where DEPOSITGLCODE='" + SUBGLCODE + "'";// AND BRCD='" + Session["BRCD"].ToString() + "'";";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public DataTable GetPLComm_Admin(string BRCD,string SBGL)
    {
        try
        {
            sql = "SELECT  COMMPL,OTHPL,PRECOMM_RATE,ADMIN_CHG FROM DEPOSITGL WHERE BRCD='" + BRCD + "' and DEPOSITGLCODE='" + SBGL + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }
    public string GetAdminChg(string GLC)
    {
        try
        {
            sql = "Select top 1 isnull(COnvert(Int,Round(ADMIN_CHG,0)),0) as ADMIN_CHG from A50001 where DEPOSITGL='" + GLC + "' and STAGE<>'1004'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public string GetAdminChg_Suraksha(string GLC)
    {
        try
        {
            sql = "Select top 1 isnull(COnvert(Int,Round(ADMIN_CHG,0)),0) as ADMIN_CHG from A50001 where DEPOSITGL='" + GLC + "' and STAGE<>'1004'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public DataTable GetIntAmountMA(string BRCD,string MPFLAG, string SUBGL,string ACCNO,string AMOUNT, string RATE, string ODT, string DDT, string LDT,string EDT, string TDATYPE)
    {
        try
        {
            sql = "Exec Isp_AVS0013 @Brcd='" + BRCD + "',@TDAType='" + TDATYPE + "',@Flag='" + MPFLAG + "',@LDate='" + conn.ConvertDate(LDT) + "',@EDate='" + conn.ConvertDate(EDT) + "',@Subgl='" + SUBGL + "',@Accno='" + ACCNO + "',@Rate='" + RATE + "',@Amt='" + AMOUNT + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }
    public DataTable GetIntAmountQISMA(string BRCD, string MPFLAG, string SUBGL, string ACCNO, string AMOUNT, string RATE, string ODT, string DDT, string LDT, string EDT, string TDATYPE)
    {
        try
        {
            sql = "Exec Isp_AVS0142 @Brcd='" + BRCD + "',@TDAType='" + TDATYPE + "',@Flag='" + MPFLAG + "',@LDate='" + conn.ConvertDate(LDT) + "',@EDate='" + conn.ConvertDate(EDT) + "',@Subgl='" + SUBGL + "',@Accno='" + ACCNO + "',@Rate='" + RATE + "',@Amt='" + AMOUNT + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }
    public string FnMultipleClose_Opr(string FL,string BRCD,string EDT,string SETNO)
    {
        try
        {
            
                sql = "Exec Isp_TDAMulti_Closure @Flag='" + FL + "',@Brcd='" + BRCD + "',@Edt='" + conn.ConvertDate(EDT) + "',@Setno='" + SETNO + "'";
           
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    
}
