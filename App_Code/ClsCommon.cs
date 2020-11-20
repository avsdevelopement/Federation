using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI;

public class ClsCommon
{
    string name = "";
    string rtn = "";
    string sql = "";
    int rtnint = 0;
    SqlCommand objCmd = new SqlCommand();
    DbConnection conn = new DbConnection();
    DataTable dt = new DataTable();
    int Result;
    string LInt = "", sResult = "", RES = "";

    public ClsCommon()
    {

    }

    public void BindBRANCHNAME(DropDownList DDL, string BRCD)
    {
        sql = "SELECT Convert(varchar(100),BRCD)+'-'+Convert(varchar(100),MIDNAME) name,BRCD id from BANKNAME WHERE BRCD<>0 ORDER BY BRCD";
        conn.FillDDL(DDL, sql);
    }

    public string GetBrCode(string LoginCode, string Stage)
    {
        try
        {
            sql = "Select BrCd From UserMaster Where LoginCode = '" + LoginCode + "' And Stage <> '1004' ";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetParameter(string BrCode, string ListField, string Stage)
    {
        try
        {
            sql = "Select ListValue From Parameter Where BrCd = '" + BrCode + "' And ListField = '" + ListField + "' And Stage = '" + Stage + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }


    public int GetGLcode(string SA, string BRCD)
    {
        try
        {
            sql = "SELECT GLCODE FROM CHARGESMASTER WHERE CHARGESTYPE='" + SA + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }


    public string GetAccStatus_DepInfo(string BrCode, string ProdCode, string AccNo, string RecSrno)
    {
        try
        {
            sql = "Exec Isp_TDADetails @Flag='CheckStatus',@Brcd='" + BrCode + "',@Subglcode='" + ProdCode + "',@Accno='" + AccNo + "',@RecSrno='" + RecSrno + "'";
            BrCode = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BrCode;
    }


    public string GetAccStatus_DepInfoCBS2(string BrCode, string ProdCode, string AccNo, string RecSrno)
    {
        try
        {
            sql = "Exec Isp_TDADetailsCBS2 @Flag='CheckStatus',@Brcd='" + BrCode + "',@Subglcode='" + ProdCode + "',@Accno='" + AccNo + "',@RecSrno='" + RecSrno + "'";
            BrCode = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BrCode;
    }

    public string GetCustNo(string BrCode, string PrCode, string AccNo)
    {
        try
        {
            sql = "Select CustNo From Avs_Acc Where Brcd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "'";
            RES = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RES;
    }

    public string GetGlCodeForGST(string BrCode, string PrCode)
    {
        try
        {
            sql = "Select GlCode From GlMast A Where A.BRCD = '" + BrCode + "' And A.SubGlCode = '" + PrCode + "' ";
            RES = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RES;
    }

    public DataTable GetGSTDetails(string BrCode, string PrCode)
    {
        try
        {
            sql = "Select * From AVS5057 A Where A.PRDCD = '" + PrCode + "' " +
                  "And EffectDate = (Select Max(EffectDate) From AVS5057 B Where B.PRDCD = A.PRDCD) " +
                    " and BRCD='" + BrCode + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public DataTable CheckCustODAcc(string BrCode, string CustNo, string WorkDate)
    {
        try
        {
            sql = "Exec Sp_CheckCustODAcc @BrCode = '" + BrCode + "', @CustNo = '" + CustNo + "', @EDate = '" + conn.ConvertDate(WorkDate).ToString() + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public DataTable GetMonthYearList(string startdate, string enddate)
    {
        try
        {
            sql = " SELECT TO_CHAR ( ADD_MONTHS ( start_dt , LEVEL - 1), 'MM YYYY') AS month_year " +
                  " FROM ( SELECT TRUNC ( TO_DATE ( '" + startdate + "' , 'DD/MM/YYYY'), 'MONTH') AS start_dt, TRUNC ( TO_DATE ( '" + enddate + "' , 'DD/MM/YYYY'), 'MONTH')AS end_dt FROM dual) " +
                  " CONNECT BY     LEVEL <= 1 + MONTHS_BETWEEN ( end_dt, start_dt)";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    // Get UserName
    public string GetUserName(string brcd, string mid)
    {
        try
        {
            sql = "SELECT USERNAME FROM USERMASTER where BRCD= '" + brcd + "' AND PERMISSIONNO='" + mid + "'";
            name = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return name;
    }

    // Get Customer Name from customer number
    public string GetCustName(string custno, string brcd)
    {
        try
        {
            sql = "select CUSTNAME from master WHERE CUSTNO='" + custno + "' AND BRCD='" + brcd + "'";
            rtn = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtn;
    }

    // Get SetNo for Posting interest
    public string GetSetno(string activity, string brcd)
    {
        try
        {
            sql = "SELECT (Lastno+1) FROM AVS1000 where Activityno='" + activity + "' and brcd='" + brcd + "'";
            rtn = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtn;
    }

    // Get Customer Name from account No 
    public string GetCustNameAc(string AccNO, string BrCode, string SubGlCode)
    {
        try
        {
            sql = "Select M.CUSTNAME From Avs_Acc AC Inner Join Master M With(NoLock) ON M.CUSTNO=AC.CUSTNO where AC.BRCD = '" + BrCode + "' And AC.SUBGLCODE='" + SubGlCode + "' And AC.ACCNO='" + AccNO + "'";
            rtn = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtn;
    }

    // ----------- Get Product NAME / GL NAME for Term Deposit-------
    public string GetProductName(int ProCode, string BRCD)
    {
        string ProductName = "";
        try
        {
            string sql = "select GLNAME from glmast where SUBGLCODE='" + ProCode + "' and BRCD='" + BRCD + "'";//GLCODE=5 removed //BRCD ADDED --Abhishek
            ProductName = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ProductName;
    }

    public string GetGLNameAll(string BRCD, string glcode)
    {
        string GLName = "";
        try
        {
            string sql = "select GLNAME from glmast where glcode=5 AND BRCD='" + BRCD + "'";
            GLName = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return GLName;
    }

    // Get product name for GLCode 3 
    public string GetAllProductName(int ProCode, string BRCD)
    {
        string ProductName = "";
        try
        {
            string sql = "select GLNAME from glmast where SUBGLCODE='" + ProCode + "' and BRCD='" + BRCD + "'";
            ProductName = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ProductName;
    }

    // Get customer No , name of glcode 1
    public DataTable GetCustNoNameGL1(string SubGlCode, string AccNo, string BrCode)//ankita 22/11/2017 brcd removed
    {
        try
        {
            sql = "Select AC.CUSTNO CUSTNO, AC.ACC_TYPE, AC.OPR_TYPE, M.CUSTNAME From Master M Inner Join Avs_Acc AC With(NoLock) ON AC.CUSTNO = M.CUSTNO Where AC.BRCD = '" + BrCode + "' And AC.SUBGLCODE = '" + SubGlCode + "' And AC.ACCNO = '" + AccNo + "' and AC.ACC_STATUS=1";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    // Get Set no from ACTIVITY
    public string GetSetNoAll(string brcd, string activity)
    {
        try
        {
            sql = "SELECT Lastno from Avs1000 WHERE Activityno='" + activity + "' AND BRCD='" + brcd + "'";
            rtn = conn.sExecuteScalar(sql);
            rtnint = Convert.ToInt32(rtn) + 1;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtnint.ToString();
    }

    // Get Set No from ACTIVITY and SRNO
    public string GetSetNo_All(string activity, string srno, string brcd)
    {
        try
        {
            sql = "SELECT ISNULL(MAX(Lastno),0)+1 from Avs1000 WHERE Activityno='" + activity + "' AND BRCD='" + brcd + "' AND TYPE='CASH-R'";
            rtn = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtn.ToString();
    }


    //  Added by amol on 03/10/2018 for insert log details
    public int LogDetails(string BrCode, string EDate, string Activity, string AccNo, string PcMac, string Mid)
    {
        try
        {
            sql = "Exec ISP_AVS0192 @BrCode = '" + BrCode + "', @EDate = '" + conn.ConvertDate(EDate) + "', @Activity = '" + Activity + "', @AccNo = '" + AccNo + "', @PcMac = '" + PcMac + "', @Mid = '" + Mid + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }



    public DataTable BindRDCal(GridView GRD, string brcd, string prcode, string accno, string Recsrno, string EDate)
    {
        try
        {
            sql = "EXEC Sp_Rd_Calc @Flag='Calc',@SFlag='GRID',@Brcd='" + brcd + "',@Sbgl='" + prcode + "',@Accno='" + accno + "',@Recsrno='" + Recsrno + "',@Edt='" + conn.ConvertDate(EDate) + "'";
            dt = conn.GetDatatable(sql);
            //Result = conn.sBindGrid(GRD,sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return dt;
    }

    // update Set no from ACTIVITY and SRNO
    public int UpdateSetNoAll(string activity, string brcd, string SETNO)
    {
        try
        {
            sql = "UPDATE AVS1000 SET LASTNO='" + SETNO + "'  WHERE Activityno='" + activity + "' and brcd='" + brcd + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    // update Set no from ACTIVITY
    public string UpdateSetNo(string brcd, string activity)
    {
        try
        {
            //sql = "SELECT Lastno from Avs1000 WHERE Activityno='" + activity + "' AND BRCD='" + brcd + "'";
            sql = "update Avs1000 set Lastno=(Lastno + 1) WHERE Activityno='" + activity + "' AND BRCD='" + brcd + "'";
            rtn = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtn;
    }

   
    public int GetPLaccno(string SA, string BRCD)
    {
        try
        {
            sql = "SELECT PLACC FROM CHARGESMASTER WHERE CHARGESTYPE='" + SA + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    // GET PLACC 
    public string GetPLACC(string brcd, string glcode, string subglcode)
    {
        try
        {
            sql = "SELECT PLACCNO FROM GLMAST WHERE GLCODE='" + glcode + "' and SUBGLCODE='" + subglcode + "' and brcd='" + brcd + "'";
            rtn = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtn;
    }

    // Get SBINT para
    public string GetSBInt(string brcd)
    {
        try
        {
            sql = "SELECT LISTVALUE from Parameter where Listfield='SBINT' AND BRCD='" + brcd + "'";
            rtn = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtn;
    }

    public string GetSBLimit(string brcd)
    {
        try
        {
            sql = "select LISTVALUE from PARAMETER where LISTFIELD='SBINTLIMIT_DAY' and BRCD='" + brcd + "'";
            rtn = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtn;
    }

    // Check RD
    public string CheckRD(string subgl, string BRCD)
    {
        string rtn = "";
        try
        {
            sql = "SELECT Deposittype FROM Depositgl WHERE Category='RD' AND Depositglcode='" + subgl + "' and BRCD='" + BRCD + "'"; //BRCD ADDED --Abhishek
            rtn = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtn;
    }
    public string CheckLKPLikeRD(string BRCD)
    {
        string rtn = "";
        try
        {
            sql = "Select LISTVALUE from PARAMETER Where LISTFIELD='LKPRD' and BRCD='1'";
            rtn = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtn;
    }

    //  Added by amol on 08/02/2018 for bind grid
    public int sBindGrid(GridView sGview, string sQuery)
    {
        try
        {
            SqlDataAdapter objDA = new System.Data.SqlClient.SqlDataAdapter();
            objCmd.CommandText = sQuery;
            objCmd.Connection = conn.GetDBConnection();
            objDA.SelectCommand = objCmd;
            objCmd.CommandTimeout = 500000;
            objDA.Fill(dt);

            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    sGview.DataSource = dt;
                    sGview.DataBind();
                    rtnint = dt.Rows.Count;
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            return rtnint = -1;
        }
        finally
        {
            objCmd.Dispose();

        }
        return rtnint;
    }




    // Calculate closing balance for TD
    public string calculateInterestTD(string month, string year, string accno, string trxtype, string brcd, string glcode, string subglcode)
    {
        try
        {
            rtn = "";
            sql = "select AMOUNT from avsb_'" + year + "''" + month + "' where Accno = '" + accno + "' AND Trxtype = '" + trxtype + "' AND BRCD = '" + brcd + "' AND GLCODE = '" + glcode + "' AND SUBGLCODE = '" + subglcode + "'";
            rtn = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtn;
    }

    // truncate temporary table
    public int truncateTemp()
    {
        try
        {
            sql = "truncate table DEPINTPAYBLE";
            rtnint = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtnint;
    }

    // Saving in temporary table 
    public string EntryDepPayable(string BRCD, string DEPOSITGLCODE, string CUSTNO, string ACCNO, string FROMDATE, string TODATE, string PRNAMT, string CREDIT, string INTPAY, string CLOSING_BAL, string INTRATE, string DAYS_MONTH, string INTEREST, string PENAL, string PENALRATE, string DEPDATE, string DEPMONTH, string DEPYEAR, string POSTINGDATE, string STAGE, string MID, string CID, string VID, string PCMAC)
    {
        try
        {
            sql = "INSERT INTO DEPINTPAYBLE (BRCD, DEPOSITGLCODE,CUSTNO,ACCNO,FROMDATE,TODATE,PRNAMT,CREDIT,INTPAY,CLOSING_BAL,INTRATE,DAYS_MONTH,INTEREST,PENAL,PENALRATE,DEPDATE,DEPMONTH, DEPYEAR, POSTINGDATE, STAGE, MID, CID, VID, PCMAC, SYS_DATE) VALUES ('" + BRCD + "', '" + DEPOSITGLCODE + "','" + CUSTNO + "','" + ACCNO + "','" + FROMDATE + "','" + TODATE + "','" + PRNAMT + "','" + CREDIT + "','" + INTPAY + "','" + CLOSING_BAL + "','" + INTRATE + "','" + DAYS_MONTH + "','" + INTEREST + "','" + PENAL + "','" + PENALRATE + "','" + DEPDATE + "','" + DEPMONTH + "', '" + DEPYEAR + "', '" + POSTINGDATE + "', '" + STAGE + "', '" + MID + "', '" + CID + "', '" + VID + "', '" + PCMAC + "', SYSDATE)";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtn;
    }

    public string GetDepPayable(string procode, string custacc, string custno, string brcd)
    {
        try
        {
            sql = "SELECT SUM(INTPAY) FROM DEPINTPAYBLE WHERE DEPOSITGLCODE='" + procode + "' AND  ACCNO='" + custacc + "' AND CUSTNO='" + custno + "' AND BRCD='" + brcd + "'";
            rtn = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtn;
    }

    public DateTime ConvertDate(string DT)
    {
        DateTime EDT = new DateTime();
        try
        {
            EDT = Convert.ToDateTime(DT);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return EDT;
        }
        return EDT;
    }

    // Get Subgl and Accno
    public DataTable GetSubglAccNo(string GlCode, string BRCD)
    {
        try
        {
            sql = "SELECT SUBGLCODE, PLACCNO  FROM GLMAST WHERE brcd='" + BRCD + "' and glcode='" + GlCode + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    // Age validation
    public int GetAge(DateTime bornDate)
    {
        int age = 0;
        try
        {
            DateTime today = DateTime.Today;
            age = today.Year - bornDate.Year;
            if (bornDate > today.AddYears(-age))
                age--;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return age;
    }

    public string GetAccLoanType(string prdcode, string GLCD, string BRCD)
    {
        sql = "SELECT GLNAME FROM GLMAST WHERE SUBGLCODE='" + prdcode + "' and BRCD='" + BRCD + "'"; //BRCD ADDED --Abhishek
        prdcode = conn.sExecuteScalar(sql);
        return prdcode;
    }

    //Added By Amol for RD Interest Calculate 
    public DataTable CalRDintrest(string brcd, string prcode, string accno, string EDate, string FL, string RecSrno)
    {
        try
        {
            // sql = "SP_RDINTERESTCAL '" + brcd + "','" + prcode + "','" + accno + "','" + conn.ConvertDate(EDate).ToString() + "'";
            if (FL == "C")
            {
                sql = "EXEC Sp_Rd_Calc @Flag='Calc',@Brcd='" + brcd + "',@Sbgl='" + prcode + "',@Accno='" + accno + "',@RecSrno='" + RecSrno + "',@Edt='" + conn.ConvertDate(EDate) + "'";
            }

            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            dt = null;
        }
        return dt;
    }
    public DataTable CalRDShceduleChk(string brcd, string prcode, string accno, string EDate, string FL)
    {
        try
        {

            sql = "EXEC Sp_Rd_Calc @Flag='Calc',@SFlag='SCH_CHK',@Brcd='" + brcd + "',@Sbgl='" + prcode + "',@Accno='" + accno + "',@Edt='" + conn.ConvertDate(EDate) + "'";

            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            dt = null;
        }
        return dt;
    }


    public DataTable CalRDintrest_1001(string brcd, string prcode, string accno, string EDate, string FL, string RECSRNO)
    {
        try
        {
            // sql = "SP_RDINTERESTCAL '" + brcd + "','" + prcode + "','" + accno + "','" + conn.ConvertDate(EDate).ToString() + "'";
            if (FL == "C")
            {
                sql = "EXEC Sp_Rd_Calc_1001 @Flag='Calc',@Brcd='" + brcd + "',@Sbgl='" + prcode + "',@Accno='" + accno + "',@Edt='" + conn.ConvertDate(EDate) + "',@RecSrno='" + RECSRNO + "'";
            }

            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            dt = null;
        }
        return dt;
    }


    public string CalCredit(string brcd, string prcode, string accno, string EDate)
    {
        try
        {
            sql = "EXEC Sp_Rd_Calc @Flag='Commi',@Brcd='" + brcd + "',@Sbgl='" + prcode + "',@Accno='" + accno + "',@Edt='" + conn.ConvertDate(EDate) + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            sql = null;
        }
        return sql;
    }


    public DataTable CalDpintrest(string brcd, string prcode, string accno, string EDate, string FL)
    {
        try
        {
            // sql = "SP_RDINTERESTCAL '" + brcd + "','" + prcode + "','" + accno + "','" + conn.ConvertDate(EDate).ToString() + "'";
            if (FL == "C")
            {
                sql = "EXEC Sp_Dp_Calc @Flag='Calc',@Brcd='" + brcd + "',@Sbgl='" + prcode + "',@Accno='" + accno + "',@Edt='" + conn.ConvertDate(EDate) + "'";
            }

            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            dt = null;
        }
        return dt;
    }

    public DataTable BindRDCal(GridView GRD, string brcd, string prcode, string accno, string EDate)
    {
        try
        {
            sql = "EXEC Sp_Rd_Calc @Flag='Calc',@SFlag='GRID',@Brcd='" + brcd + "',@Sbgl='" + prcode + "',@Accno='" + accno + "',@Edt='" + conn.ConvertDate(EDate) + "'";
            dt = conn.GetDatatable(sql);
            //Result = conn.sBindGrid(GRD,sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return dt;
    }
    public DataTable BindRDCal_1001(GridView GRD, string brcd, string prcode, string accno, string EDate)
    {
        try
        {
            sql = "EXEC Sp_Rd_Calc_1001 @Flag='Calc',@SFlag='GRID',@Brcd='" + brcd + "',@Sbgl='" + prcode + "',@Accno='" + accno + "',@Edt='" + conn.ConvertDate(EDate) + "'";
            dt = conn.GetDatatable(sql);
            //Result = conn.sBindGrid(GRD,sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return dt;
    }
    public DataTable BindDPCal(GridView GRD, string brcd, string prcode, string accno, string EDate)
    {
        try
        {
            sql = "EXEC Sp_Dp_Calc @Flag='Calc',@SFlag='GRID',@Brcd='" + brcd + "',@Sbgl='" + prcode + "',@Accno='" + accno + "',@Edt='" + conn.ConvertDate(EDate) + "'";
            dt = conn.GetDatatable(sql);
            //Result = conn.sBindGrid(GRD,sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return dt;
    }
    public double Interest(string brcd, string Edate, string SCode, string AccNo)
    {
        try
        {
            string[] date = Edate.ToString().Split('/');
            string subglcode = conn.sExecuteScalar("SELECT IR FROM GLMAST WHERE GLCODE = 5 AND SUBGLCODE = '" + SCode + "'");
            LInt = conn.sExecuteScalar("SELECT ISNULL(SUM(AB.AMOUNT),0) FROM AVSM_" + date[2].ToString() + "" + date[1].ToString() + " AB WHERE AB.GLCODE = 10 AND AB.ACCNO = '" + AccNo + "' AND AB.subglcode = '" + subglcode + "' AND AB.BRCD = '" + brcd + "' AND AB.TRXTYPE = 1");
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(LInt);
    }

    public double GetPPandINT(string FL, string SBGL, string ACCNO, string BRCD, string EDT, string GLCODE, string RECSRNO = "0")
    {
        try
        {
            string[] TD = EDT.Split('/');

            sql = "exec Sp_OPENCLOSE @P_FLAG='" + FL + "',@P_FYEAR='" + TD[2].ToString() + "',@P_FMONTH='" + TD[1].ToString() + "',@p_job='" + SBGL + "',@p_job1='" + ACCNO + "',@p_job2='" + BRCD + "',@p_job3='" + conn.ConvertDate(EDT) + "',@p_job4='" + GLCODE + "',@RecSrno='" + RECSRNO + "'";
            LInt = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(LInt);
    }

    public string GetAccStatus(string BRCD, string SBGL, string AC, string RecSrno = "0")
    {
        try
        {
            sql = "SELECT ACC_STATUS FROM AVS_ACC WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + SBGL + "' AND ACCNO='" + AC + "'";
            AC = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return AC;
    }
    public string GETGLCODE(string BRCD, string ACCNO, string SUBGLCODE)
    {
        sql = "SELECT GLCODE FROM AVS_ACC WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + SUBGLCODE + "' AND ACCNO='" + ACCNO + "'";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public string GETDEPOSITAMT(string BRCD, string ACCNO, string SUBGLCODE)
    {
        sql = "SELECT PRNAMT FROM DEPOSITINFO WHERE BRCD='" + BRCD + "' AND DEPOSITGLCODE='" + SUBGLCODE + "' AND CUSTACCNO='" + ACCNO + "'";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public string Checkapara(string BRCD, string SUBGLCODE)
    {
        sql = "select status from depositgl where depositglcode='" + SUBGLCODE + "' and brcd='" + BRCD + "'";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public string GetIntACCYN(string BRCD, string SBGL)
    {
        try
        {
            sql = "SELECT ISNULL(INTACCYN,'N') FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + SBGL + "'";
            SBGL = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);

        }
        return SBGL;
    }

    //Added by Abhishek on 06/07/2017 for OWC Loan Entires (Can be used for All Loan Entries)
    public string Uni_LoanEntries(string FL, string EDT, string Glc, string Subgl, string Brcd, string Accno, string AccTypeid, string InstTypeid, string OprTypeid, string ClgFlag, string Amt, string P1, string P2, string Acti, string PmtMode, string InstNo, string InstDate, string InstBkcd, string InstBrcd, string Stage, string Mid, string Cid, string Vid, string Paymast, string Custno, string Custname, string PriAmt, string IntAmt, string PenAmt, string RecAmt, string NotAmt, string SerAmt, string CouAmt, string SurAmt, string OthAmt, string BanAmt, string InsAmt, string F1, string REFID)
    {
        try
        {
            sql = "Exec Isp_LoanAccountEntries @Refid='" + REFID + "',@Flag='" + FL + "',@Edt='" + conn.ConvertDate(EDT) + "',@Glc='" + Glc + "',@Sbgl='" + Subgl + "',@Brcd='" + Brcd + "',@Accno='" + Accno + "',@AccTypeid='" + AccTypeid + "',@InstTypeid='" + InstTypeid + "',@OprTypeid='" + OprTypeid + "',@ClgFlag='" + ClgFlag + "',@Amt='" + Amt + "',@P1='" + P1 + "',@P2='" + P2 + "',@Acti='" + Acti + "',@PmtMode='" + PmtMode + "',@InstNo='" + InstNo + "',@InstDate='" + conn.ConvertDate(InstDate) + "',@InstBkcd='" + InstBkcd + "',@InstBrcd='" + InstBrcd + "',@Stage='" + Stage + "',@Mid='" + Mid + "',@Cid='" + Cid + "',@Vid='" + Vid + "',@Paymast='" + Paymast + "',@Custno='" + Custno + "',@Custname='" + Custname + "',@PriAmt='" + PriAmt + "',@IntAmt='" + IntAmt + "',@PenAmt='" + PenAmt + "',@RecAmt='" + RecAmt + "',@NotAmt='" + NotAmt + "',@SerAmt='" + SerAmt + "',@CouAmt='" + CouAmt + "',@SurAmt='" + SurAmt + "',@OthAmt='" + OthAmt + "',@BanAmt='" + BanAmt + "',@InsAmt='" + InsAmt + "',@F1='" + F1 + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public DataTable ShowIMAGE(string CUSTNO, string BRCD, string ACCNO)
    {
        try
        {
            sql = "select id,SignName,PhotoName,SignIMG,PhotoImg from  Imagerelation where BRCD='" + BRCD + "' and CustNo='" + CUSTNO + "' ";
            dt = conn.GetData(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public DataTable LoanDetails(string Fl, string BRCD, string SUBGL, string ACCNO, string EDT)
    {
        try
        {
            //DETAILS
            sql = "Exec Isp_Clearing_Operations @Flag='" + Fl + "',@SFlag='',@Edt='" + conn.ConvertDate(EDT) + "',@Brcd='" + BRCD + "',@Subglcode='" + SUBGL + "',@Accno='" + ACCNO + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }
    public DataTable IWOWSum(string Fl, string SFL, string BRCD, string EDT)
    {
        try
        {
            sql = "Exec Isp_Clearing_Operations @Flag='" + Fl + "',@SFlag='" + SFL + "',@Edt='" + conn.ConvertDate(EDT) + "',@Brcd='" + BRCD + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }
    public string CRCPSUM(string fl, string sfl, string brcd, string edate)
    {
        try
        {
            sql = "exec Isp_AVS0006 @FLAG='" + fl + "',@SFLAG='" + sfl + "',@BRCD='" + brcd + "',@EDATE='" + conn.ConvertDate(edate) + "'";
            RES = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return RES;
    }
    public string GetOpenDate(string brcd, string subgl, string accno)
    {
        try
        {
            sql = "SELECT convert(varchar(10),OPENINGDATE,103)OPENINGDATE FROM AVS_ACC where ACCNO='" + accno + "' AND SUBGLCODE='" + subgl + "' AND BRCD='" + brcd + "' and stage<>'1004'";
            RES = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return RES;
    }
    //Select isnull(LISTVALUE,'N') from PARAMETER where LISTFIELD='RDMYN'
    public string GetRDMYN()
    {
        try
        {
            //sql = "Select isnull(LISTVALUE,'N') from PARAMETER where LISTFIELD='RDMYN'";
            sql = "Select Convert(Varchar(100),A.LISTVALUE)+'_'+Convert(Varchar(100),B.BANKCD) LB from PARAMETER A " +
                " inner join BANKNAME B			on A.Brcd=B.Brcd " +
                " where A.LISTFIELD='RDMYN' " +
                 " and A.BRCD='1'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public string GetIOMid(string FL, string Brcd, string SETNO, string EDT)
    {
        try
        {
            string MM, YY, Tbname;

            string[] DT = EDT.Split('/');

            MM = DT[1].ToString();
            YY = DT[2].ToString();


            if (FL == "IW")
            {
                Tbname = "INWORD_" + YY + MM;
            }
            else
            {
                Tbname = "OWG_" + YY + MM;
            }


            sql = "Select TOP 1 MID from " + Tbname + " where BRCD='" + Brcd + "' and ENTRYDATE='" + conn.ConvertDate(EDT) + "' and SET_NO='" + SETNO + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public string ChkECS(string brcd)
    {
        string listvalue = "";
        try
        {
            sql = "select LISTVALUE from parameter where brcd='" + brcd + "' and LISTFIELD='ECS'";
            listvalue = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return listvalue;
    }
    public string ChkDayBook(string brcd)
    {
        string listvalue = "";
        try
        {
            sql = "select LISTVALUE from parameter where brcd='0' and LISTFIELD='DayBook'";
            listvalue = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return listvalue;
    }
    public string GetNETPAID(string brcd)
    {
        try
        {
            sql = "select LISTVALUE from parameter where brcd='" + brcd + "' and LISTFIELD='NETPAID'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }
    public string GetBankCode()
    {
        try
        {
            sql = "Select TOP 1 BANKCD FROM BANKNAME";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public string GetDDSINT()
    {
        try
        {
            sql = "Select LISTVALUE from PARAMETER where LISTFIELD='DDSINT' and brcd='1'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public string GetRDINT()
    {
        try
        {
            sql = "Select LISTVALUE from PARAMETER where LISTFIELD='RDCINT' and brcd='1'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public string GetTDINT()
    {
        try
        {
            sql = "Select LISTVALUE from PARAMETER where LISTFIELD='TDINT' and brcd='1'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public string GetDDSUG()
    {
        try
        {
            sql = "Select LISTVALUE from PARAMETER where LISTFIELD='DDSUG' and brcd='1'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public string GetRDUG()
    {
        try
        {
            sql = "Select LISTVALUE from PARAMETER where LISTFIELD='RDUG' and brcd='1'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public string GetTDUG()
    {
        try
        {
            sql = "Select LISTVALUE from PARAMETER where LISTFIELD='TDUG' and brcd='1'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public string GetDDSADMINCHG()
    {
        try
        {
            sql = "Select LISTVALUE from PARAMETER where LISTFIELD='DDSADMIN_CHG' and brcd='1'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public string GetUniversalPara(string Parameter, string BrCode = "1")
    {
        try
        {
            sql = "Select LISTVALUE from PARAMETER where LISTFIELD='" + Parameter + "' and brcd='1'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public string GetFunc_MonDiff(string FromDate, string ToDate)
    {
        try
        {
            sql = "Select dbo.UF_MonthDifference('" + conn.ConvertDate(FromDate) + "','" + conn.ConvertDate(ToDate) + "')";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public string GetAMTVAL(string BRCD, string SUBGL)
    {
        try
        {
            sql = "Select isnull(AMTVAL,'N') AMTVAL from DBo.DEPOSITGL where DEPOSITGLCODE='" + SUBGL + "' and BRCD='" + BRCD + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public string GetFDAMT(string BRCD, string SUBGL, string ACCNO, string RECSRNO)
    {
        try
        {
            sql = "Select PRNAMT from dbo.DEPOSITINFO where BRCD='" + BRCD + "' and DEPOSITGLCODE='" + SUBGL + "' and CUSTACCNO='" + ACCNO + "' and isnull(RECSRNO,'0')='" + RECSRNO + "' and LMSTATUS='1'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public DataTable getContct(string custno)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "select * from master where custno='" + custno + "'";
            dt = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }
    public int insertContct(string custno, string brcd, string mobile1, string mobile2, string mid)
    {
        try
        {

            sql = "select count(*) from AVS_CONTACTD where brcd='" + brcd + "' and custno='" + custno + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
            if (Result > 0)
            {
                sql = "update AVS_CONTACTD set MOBILE1='" + mobile1 + "' ,MOBILE2='" + mobile2 + "',STAGE='1003',CID='" + mid + "' Where CUSTNO ='" + custno + "' AND BRCD='" + brcd + "'";
                Result = conn.sExecuteQuery(sql);
            }
            else
            {
                sql = "Insert into AVS_CONTACTD (CUSTNO,MOBILE1,MOBILE2,STAGE,MID,systemdate,BRCD) values ('" + custno + "','" + mobile1 + "','" + mobile2 + "','" + mid + "','1003',GETDATE(),'" + brcd + "')";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public DataTable getMobiles(string custno)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "select * from AVS_CONTACTD where custno='" + custno + "'";
            dt = conn.GetDatatable(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }
    public string GetBANKCode()
    {
        string RES = "";
        try
        {
            sql = "Select BANKCD from BANKNAME where BRCD=0";
            RES = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RES;
    }
    public string InstMonthDiff(string EDT, string INSTDT)
    {
        try
        {
            sql = "select dbo.UF_MonthDifference('" + conn.ConvertDate(INSTDT) + "','" + conn.ConvertDate(EDT) + "')";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public string getShrParam()
    {
        try
        {
            sql = "select LISTVALUE from PARAMETER where LISTFIELD ='SHRALLOT'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public string GetDiviName(string RECDIV)
    {
        try
        {
            sql = "Select DESCR from PAYMAST where RECDIV='" + RECDIV + "' and RECCODE='0'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public string GetDeptName(string RECDIV, string RECCODE)
    {
        try
        {
            sql = "Select DESCR from PAYMAST where RECDIV='" + RECDIV + "' and RECCODE='" + RECCODE + "'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public string CheckRec_Exists(string FL, string Brcd, string Subgl, string Accno, string RecSrno)
    {
        try
        {
            sql = "    Exec Isp_TDADetails @Flag='" + FL + "',@Brcd='" + Brcd + "',@Subglcode='" + Subgl + "',@Accno='" + Accno + "',@RecSrno='" + RecSrno + "'";
            sql = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    public string MultiBranch(string LoginCode)
    {
        try
        {
            sql = "Select IsNull(MultiBranch, 'N') As MultiBranch From UserMaster Where LoginCode = '" + LoginCode + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetLastTallyDate()
    {
        try
        {
            sql = "Select ListValue from Parameter Where BrCd = '0' And ListField = 'TallyDate' And Stage = '1003'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetMaxWorkingDate(string BrCode)
    {
        try
        {
            sql = "Select ConVert(VarChar(10), Max(DayBeginDate), 103) As WorkingDate From AVS1025 Where BrCd = '" + BrCode + "' And Status = '1'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataTable GetAdminPrCode(string BrCode)
    {
        try
        {
            sql = "Select ADMGlCode, ADMSubGlCode From BankName Where BrCd = '" + BrCode + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public int GetDPParam(GridView GView, string BrCode, string PrCode)
    {
        try
        {
            sql = "Exec ISP_AVS0209 @BrCode = '" + BrCode + "', @PrCode = '" + PrCode + "'";
            Result = conn.sBindGrid(GView, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public string ChequeExists(string BrCode, string PrCode, string AccNo, string InstNo)
    {
        try
        {
            //  Added by amol as per new logic (Cheque stock and issue)
            sql = "Select (Case When Status = 0 Or Status = 1 Then 'Issued' When Status = 2 Then 'Stopped' When Status = 3 Then 'Finance' " +
                  "When Status = 4 Then 'Returned' When Status = 5 Then 'RePresented' When Status = 6 Then 'Destroyed' Else 'Invalid' End) " +
                  "From AVS_InstruIssueMaster " +
                  "Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "' " +
                  "And InstrumentNo = Right('000000' + Rtrim('" + InstNo + "'), 6) ";
            sResult = conn.sExecuteScalar(sql);

            if (sResult == null)
                sResult = "Invalid";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }
    //  Added By Amol On 11/09/2018 for update instrument no status
    public void UpdateChqStatus(string BrCode, string PrCode, string AccNo, string InstNo, string EDate, string SetNo, string Mid)
    {
        try
        {
            sql = "Update AVS_InstruIssueMaster Set EntryDate = '" + conn.ConvertDate(EDate) + "', SetNo = '" + SetNo + "', Cid = '" + Mid + "', Status = '3' " +
                  "Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "' And InstrumentNo = Right('000000' + Rtrim('" + InstNo + "'), 6) And Status in ('1', '0')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

}