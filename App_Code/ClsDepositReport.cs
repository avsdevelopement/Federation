using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsDepositReport
/// </summary>
public class ClsDepositReport
{
    DbConnection conn = new DbConnection();
    ClsOpenClose OC = new ClsOpenClose();    
    string sql = "";
    int Result = 0;
    public ClsDepositReport()
    {        
    }

    public int InsertSave(string CUSTNO, string CUSTACCNO, string DEPOSITGLCODE, string PRNAMT, string RATEOFINT, string OPENINGDATE, string DUEDATE, string PERIOD, string INTAMT, string MATURITYAMT, string BRCD, string Balance)
    {
        int Result = 0;
        try 
        {
        string sql1 = "insert into depmaturity (CUSTNO,CUSTACCNO,DEPOSITGLCODE,PRNAMT,RATEOFINT,OPENINGDATE,DUEDATE,PERIOD,INTAMT,MATURITYAMT,BRCD,CLOSINGBALANCE) " +
        "values('" + CUSTNO + "','" + CUSTACCNO + "','" + DEPOSITGLCODE + "','" + PRNAMT + "','" + RATEOFINT + "','" + OPENINGDATE + "','" + DUEDATE + "','" + PERIOD + "','" + INTAMT + "','" + MATURITYAMT + "','" + BRCD + "','" + Balance + "')";
        Result = conn.sExecuteQuery(sql1);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }

    public DataTable GetInfoDepM(string FL, string FD, string TD, string SBGLFRM, string SBGLTO, string BRCD,string EDT)
    {
        DataTable DT = new DataTable();
        try 
        {
        //sql = "select D.CUSTNO,D.CUSTACCNO,D.DEPOSITGLCODE,D.PRNAMT,D.RATEOFINT,D.OPENINGDATE,D.DUEDATE,D.PERIOD,D.INTAMT,D.MATURITYAMT,D.BRCD,D.CLOSINGBALANCE,M.CUSTNAME DEPOSITORNAME from depmaturity D  " +
        //    " LEFT JOIN MASTER M ON M.CUSTNO=D.CUSTNO AND M.BRCD=D.BRCD WHERE   D.DUEDATE between TO_DATE ('" + Convert.ToDateTime(FD).ToString("dd/MM/yyyy") + "' and  TO_DATE ('" + Convert.ToDateTime(TD).ToString("dd/MM/yyyy") + "' AND D.brcd='"+BRCD+"'";

        //string sql1 = "SELECT DP.*, '0' AS BAL FROM DEPOSITINFO DP WHERE (DUEDATE BETWEEN '01/10/2018' and  '25/10/2018')  " +
        //              "AND (DEPOSITGLCODE BETWEEN " + SBGLFRM + " AND " + SBGLTO + ") AND BRCD=1 AND STAGE<>1004";

        //string sql2 = "SELECT DP.*, MS.CUSTNAME DEPOSITORNAME,'' AS CLOSINGBALANCE , (CASE WHEN DP.INTPAYOUT = 1 Then 'Monthly' WHEN DP.INTPAYOUT = 2 Then 'quarterly' WHEN DP.INTPAYOUT = 3 Then 'On maturity' END) AS INTFRQ FROM DEPOSITINFO DP  " +
                      //"LEFT JOIN MASTER MS ON  MS.CUSTNO=DP.CUSTNO " +
                      //"WHERE (DP.DUEDATE BETWEEN '"+FD+"' and  '"+TD+"')  AND " +
                      //"(DP.DEPOSITGLCODE BETWEEN " + SBGLFRM + " AND " + SBGLTO + ") AND DP.BRCD='" + BRCD + "' AND DP.STAGE<>1004 ORDER BY DP.CUSTACCNO ";

                      //string sql2 = "SELECT MS.CUSTNO MCUSTNO,DP.*,MS.CUSTNAME DEPOSITORNAME,'' AS CLOSINGBALANCE ,(CASE WHEN DP.INTPAYOUT = 1 Then 'Monthly' WHEN DP.INTPAYOUT = 2 Then 'quarterly' WHEN DP.INTPAYOUT = 3 Then 'On maturity' END) AS INTFRQ FROM DEPOSITINFO DP " +
                      //            " LEFT JOIN AVS_ACC AV ON AV.ACCNO=DP.CUSTACCNO AND AV.SUBGLCODE=DP.DEPOSITGLCODE AND AV.BRCD=DP.BRCD " +
                      //            " LEFT JOIN MASTER MS ON  MS.CUSTNO=AV.CUSTNO AND AV.BRCD=MS.BRCD " +
                      //            " WHERE DP.DUEDATE BETWEEN '" + conn.ConvertDate(FD) + "' and  '" + conn.ConvertDate(TD) + "' " +
                      //            " AND DP.DEPOSITGLCODE BETWEEN " + SBGLFRM + " AND " + SBGLTO + " AND DP.BRCD='" + BRCD + "' AND DP.STAGE<>1004 ORDER BY DP.DUEDATE";
            string sql2 = "EXEC SP_DEPOREPO @FLAG='DP',@FDATE='" + conn.ConvertDate(FD) + "',@TDATE='" + conn.ConvertDate(TD) + "',@FPRD='" + SBGLFRM + "',@TPRD='" + SBGLTO + "',@BRCD='" + BRCD + "',@EDATE='"+conn.ConvertDate(EDT)+"'";
        DT = conn.GetDatatable(sql2);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return DT;
    }

    public DataTable GetSMSInfo(string fdate, string tdate, string fbrcd, string tbrcd,string mob)
    {
        DataTable DT = new DataTable();
        try
        {
            string sql2 = "EXEC SP_SMSMSTREPORT @FDATE='" + conn.ConvertDate(fdate) + "',@TDATE='" + conn.ConvertDate(tdate) + "',@FBRCD='" + fbrcd + "',@TBRCD='" + tbrcd + "',@MOBILE='"+mob+"'";
            DT = conn.GetDatatable(sql2);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable getDepositeReport()
    {
        sql = "";
                DataTable DT = new DataTable();
        try 
        {
        DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return DT;
    }
    public DataTable GetInitimateInfo(string FL, string FD, string TD, string SBGLFRM, string SBGLTO, string BRCD, string EDT)//Dhanya shetty//16/01/2018
    {
        DataTable DT = new DataTable();
        try
        {
            string sql2 = "EXEC Mat_Initimate @FLAG='DP',@FDATE='" + conn.ConvertDate(FD) + "',@TDATE='" + conn.ConvertDate(TD) + "',@FPRD='" + SBGLFRM + "',@TPRD='" + SBGLTO + "',@BRCD='" + BRCD + "',@EDATE='" + conn.ConvertDate(EDT) + "'";
            DT = conn.GetDatatable(sql2);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}