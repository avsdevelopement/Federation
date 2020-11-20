using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public class ClsOpenClose
{
    string sql = "";
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    int Result;
    double rtndouble = 0;
    string resultstring = "";
    decimal BALANCE = 0;

    public ClsOpenClose()
    {

    }


    public double GetCPOpenClose(string FL, string Brcd, string Glcode, string Subglcode, string Accno, string Edt, string ACCYN = "OPT")  //dded by abhihshek for ACCYN =N then remove accno Condition 31-10-2017
    {
        double CL = 0;
        try
        {
            sql = "exec Isp_CashPayClosing @Flag='" + FL + "',@Brcd='" + Brcd + "',@Glcode='" + Glcode + "',@Subglcode='" + Subglcode + "',@Accno='" + Accno + "',@Edt='" + conn.ConvertDate(Edt) + "',@ACCYN='" + ACCYN + "'";
            CL = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return CL;
    }
    // OPENING and CLOSING balance
    public double GetOpenClose(string Flag, string Fyear, string FMonth, string PT, string ACC, string BRCD, string EDT, string GL, string ACCYN = "OPT",string RECSRNO="0") //dded by abhihshek for ACCYN =N then remove accno Condition 31-10-2017
    {
        double BAL = 0;
        try
        {
            sql = "Exec SP_OPENCLOSE @P_FLAG='" + Flag + "',@P_FYEAR='" + Fyear + "',@P_FMONTH='" + FMonth + "',@p_job='" + PT + "',@p_job1='" + ACC + "',@p_job2='" + BRCD + "',@p_job3='" + conn.ConvertDate(EDT) + "',@p_job4='" + GL + "',@ACCYN='" + ACCYN + "',@RecSrno='"+RECSRNO+"'";
            //sql = "SELECT FN_OPENCLOS('" + Flag + "','" + Fyear + "','" + FMonth + "','" + PT + "','" + ACC + "','" + BRCD + "','" + EDT + "','" + GL + "') BAL FROM DUAL";
            BAL = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return BAL;
    }
    public int getaccno(string SUBGL,string GL,string BRCD)
    {
        int ACCNO;
        try
        {
            sql = "SELECT  ACCNO FROM AVS_ACC A " +
           " INNER JOIN MASTER M ON M.CUSTNO=A.CUSTNO AND A.BRCD=M.Brcd " +
          " WHERE A.SUBGLCODE='1003'  AND A.GLCODE='2' AND A.BRCD='2' AND A.ACC_STATUS=1 ";
            ACCNO = Convert.ToInt32(conn.sExecuteScalar(sql));
            return ACCNO;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return ACCNO='0';
        
        }
    }
    public string getaccno(string agcode)
    {
        sql = "RIGHT('00000'+ CONVERT(VARCHAR,'"+agcode+"'),6)";
      string   Result = conn.sExecuteScalar(sql);
        return Result;

    }
    
    //For Getting OpeningDate ,Period and Closing for DDSCLOSE -- ABHISHEK
    public DataTable GetOpeningDate(string BRCD, string SUBGL, string ACCNO, string EDT)
    {
        try
        {
            sql = "SELECT CONVERT(VARCHAR(20),OPENINGDATE,103) OPENINGDATE,D_PERIOD AS PERIOD,(SELECT CONVERT(VARCHAR(20),(DATEADD(MONTH,CONVERT(INT,D_PERIOD),OPENINGDATE)),103)) CLOSINGDATE FROM AVS_ACC WHERE SUBGLCODE='" + SUBGL + "' AND ACCNO='" + ACCNO + "' AND BRCD='" + BRCD + "'";
            //sql = "SELECT CONVERT(VARCHAR(11),OPENINGDATE,103) OPENINGDATE,CONVERT(VARCHAR(11),isnull(LASTINTDATE,OPENINGDATE),103) LASTINTDATE,PERIOD AS PERIOD,CONVERT(VARCHAR(11),DUEDATE,103) CLOSINGDATE FROM DEPOSITINFO WHERE DEPOSITGLCODE='" + SUBGL + "' AND CUSTACCNO='" + ACCNO + "' AND BRCD='" + BRCD + "' and LMSTATUS='1'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public string GetLastIntDateDDSHISTORY(string BRCD, string SUBGL, string ACCNO)
    {
        try
        {
            sql = "Select Convert(Varchar(11),MAX(ENTRYDATE),103) as LASTINTDATE from AVS_DDSHISTORY " +
                    " where BRCD='" + BRCD + "'  " +
                    " and SUBGLCODE='" + SUBGL + "'" +
                    " and ACCNO='" + ACCNO + "' ";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }

    //Get Minimum month value from charges master --ABHISHEK
    public string GetMinMonth(string BRCD)
    {
        try
        {
            sql = "SELECT MINMONTH FROM CHARGESMASTER WHERE CHARGESTYPE=2 AND BRCD='" + BRCD + "'";
            resultstring = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return resultstring;
    }

    //Get authorize and created transaction balance -- ABHISHEK
    public DataTable GetBalOC(string BRCD,string EDT,string SUBGL,string ACCNO)
    {
        try
        {
            string[] TB;
            string tbname;
            TB = EDT.Split('/');
            tbname = TB[2].ToString() + TB[1].ToString();

            sql = "SELECT " +
                    " SUM(CASE WHEN TRXTYPE=2 THEN -1*AMOUNT ELSE AMOUNT END) CLEARBAL, " +
                    " SUM(CASE WHEN TRXTYPE=1 THEN AMOUNT ELSE 0 END)+ SUM(CASE WHEN TRXTYPE=2 THEN -1*AMOUNT ELSE AMOUNT END) TOTALBAL " +
                    " FROM AVSB_" + tbname + " " +
                    " WHERE ACCNO='" + ACCNO + "' AND  " +
                    " SUBGLCODE='" + SUBGL + "'" +
                    " AND BRCD='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }

    //fETCHING instrument details from table --ABHISHEK
    public DataTable GetInstruDetail(string BRCD, string EDT, string SUBGL, string ACCNO)
    {
        try
        {
            sql = "SELECT FSERIES STARTNO,TSERIES ENDNO,NOOFLEAVES BOOKSIZE FROM AVS_INSTRUMENTISSUE " +
                " WHERE ACCNO='" + ACCNO + "' AND " +
                " SUBGLCODE='" + SUBGL + "' AND BRCD='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }

    //Fetching Charges from table --ABHISHEK
    public string GetCharges(string BRCD,string EDT,string FL)
    {
        try
        {
            if (FL == "UCC")
            {
                sql = "SELECT CHARGES FROM CHARGESMASTER WHERE CHARGESTYPE=1 AND DESCRIPTION='UNUSED CHEQUE CHARGE'";
            }
            else if (FL == "MCM")
            {
                sql = "SELECT CHARGES FROM CHARGESMASTER WHERE CHARGESTYPE=2 AND DESCRIPTION='EARLY CLOSURE CHARGES'";
            }
            resultstring = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return resultstring;
    }


    //Get Last int date for an A/C --ABHISHEK
    public string GetLastIntDate(string BRCD, string EDT, string ACCNO, string SUBGL)
    {
        try
        {
            sql = "SELECT CONVERT(VARCHAR(11),LASTINTDATE,103) LASTINTDATE FROM AVS_ACC WHERE BRCD='" + BRCD + "' AND ACCNO='" + ACCNO + "' AND SUBGLCODE='" + SUBGL + "'";
            resultstring = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return resultstring;
    }


    //gET aCCOUNT aCTIVATE OR cLOSE --ABHISHEK
    public string GETACCStatus(string BRCD,string ACCNO,string SUBGL)
    {
        try
        {
            sql = "SELECT ACC_STATUS FROM AVS_ACC WHERE  ACCNO='" + ACCNO + "' AND  SUBGLCODE='" + SUBGL + "' AND BRCD='" + BRCD + "'";
            resultstring = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
            return resultstring;
    }
    //OPENING CLOSING BALANCE for general Ledger (Without Acc No)
    public decimal GetOpenCloseGL(string Flag, string Fyear, string FMonth, string PT, string BRCD, string EDT, string GL)
    {
        try
        {
            sql = "SELECT FN_OPENCLOS_GL('" + Flag + "','" + Fyear + "','" + FMonth + "','" + PT + "','" + BRCD + "','" + EDT + "','" + GL + "') BAL FROM DUAL";
            BALANCE = Convert.ToDecimal(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return BALANCE;
    }

    // Get Closing Principle Payable and Interest Payable
    public string GetClosingPrincInt(string Fyear, string FMonth, string PT, string ACC, string BRCD, string EDT, string GLCODE)
    {
        try
        {
            sql = "SELECT FN_CLOSING_PRINC_INTRST('" + Fyear + "','" + FMonth + "','" + PT + "','" + ACC + "','" + BRCD + "','" + EDT + "', '" + GLCODE + "') BAL FROM DUAL";
            resultstring = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return resultstring;
    }

    public int GetAgentexit(string AN, string BRCD)//BRCD ADDED --Abhishek
    {
        try
        {
            //sql = "SELECT count(*) FROM GLMAST WHERE SUBGLCODE='" + AN + "'";
            sql = "SELECT count(*) FROM AGENTMAST WHERE AGENTCODE='" + AN + "' and BRCD='" + BRCD + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return Result;
    }
    public String GetAgentName(string AN, string BRCD)//BRCD ADDED --Abhishek
    {
        try
        {
            sql = "SELECT AGENTNAME FROM AGENTMAST WHERE AGENTCODE='" + AN + "' and BRCD='" + BRCD + "'";
            resultstring = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return resultstring;
    }

    public int GetSAaccexit(string SA, string BRCD) //BRCD ADDED --Abhishek
    {
        try
        {
            sql = "SELECT count(*) FROM AVS_ACC WHERE ACCNO='" + SA + "' and BRCD='" + BRCD + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return Result;
    }
    public int GetSAcustno(string SA, string BRCD) //BRCD ADDED --Abhishek
    {
        try
        {
            sql = "SELECT CUSTNO FROM AVS_ACC WHERE ACCNO='" + SA + "' and BRCD='" + BRCD + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return Result;
    }
    public double NetCommission(string ComAmt, string TDamt, string BRCD)
    {
        double a, b;
        try
        {
            a = Convert.ToDouble(ComAmt);
            b = Convert.ToDouble(TDamt);
            rtndouble = a - b;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return rtndouble;
    }
    public double Comamt(string Tcoll, string Com, string BRCD)
    {
        double a, b;
        try
        {
            a = Convert.ToDouble(Tcoll);
            b = Convert.ToDouble(Com);
            rtndouble = Convert.ToDouble((a * b) / 100);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return rtndouble;
    }

    public double TDamount(string Camt, string TDded, string BRCD)
    {
        double a, b;
        try
        {
            a = Convert.ToDouble(Camt);
            b = Convert.ToDouble(TDded);
            rtndouble = Convert.ToDouble((a * b) / 100);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return rtndouble;
    }

    public string GetFinStartDate(string EDate)
    {
        try
        {
            DT = new DataTable();

            sql = "Exec SP_GetCurrFinStartDate '" + conn.ConvertDate(EDate).ToString() + "'";
            resultstring = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return resultstring;
    }

    public string GetAccOpenDate(string BrCode, string SubGlCode, string AccNo)
    {
        try
        {
            sql = "Select Convert(VarChar(10), OpeningDate, 103) From Avs_Acc Where Brcd = '" + BrCode + "' And SubGlCode = '" + SubGlCode + "' And AccNo = '" + AccNo + "'";
            resultstring = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return resultstring;
    }

    public string GetMigDate(string BrCode)
    {
        try
        {
            sql = "Select Convert(VarChar(10), Implementatin, 120) From BankName Where Brcd = '" + BrCode + "'";
            resultstring = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return resultstring;
    }

    public DataTable GetAccStatDetails(string PFMONTH, string PTMONTH, string PFYEAR, string PTYEAR, string PFDT, string PTDT, string PAC, string PAT, string BRCD)
    {
        try
        {
            DT = new DataTable();

            sql = "Exec SP_ACCSTATUS_R @pfmonth='" + PFMONTH + "',@ptmonth='" + PTMONTH + "',@PFDT='" + conn.ConvertDate(PFDT) + "',@PTDT='" + conn.ConvertDate(PTDT) + "',@pfyear='" + PFYEAR + "',@ptyear='" + PTYEAR + "',@pac='" + PAC + "',@pat='" + PAT + "', @BRCD='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}