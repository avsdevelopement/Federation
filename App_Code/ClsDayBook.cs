using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsDayBook
{
    string sql = "";
    int Result = 0;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    double BAL;

    public ClsDayBook()
    {
        
    }
    public DataTable GetDayInfo(string BranchID, string FL, string FLT, string FDate) //Rakesh 09-12-2016 Add BrCD
    {
        try
        {
            sql = "Exec RptDayBookRegister '" + BranchID + "','" + FL + "','" + FLT + "', '" + conn.ConvertDate(FDate).ToString() + "' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetDayInfo_SHIV (string BranchID, string FL, string FLT, string FDate) //Rakesh 09-12-2016 Add BrCD
    {
        try
        {
            sql = "Exec RptDayBookRegister_Shiv '" + BranchID + "','" + FL + "','" + FLT + "', '" + conn.ConvertDate(FDate).ToString() + "' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetDayInfo_PEN (string BranchID, string FL, string FLT, string FDate) //Rakesh 09-12-2016 Add BrCD
    {
        try
        {
            sql = "Exec RptDayBookRegister_Pen '" + BranchID + "','" + FL + "','" + FLT + "', '" + conn.ConvertDate(FDate).ToString() + "' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetDayInfo_Mamco (string BranchID, string FL, string FLT, string FDate) //Rakesh 09-12-2016 Add BrCD
    {
        try
        {
            sql = "Exec RptDayBookRegister_Mamco @BranchID='" + BranchID + "',@SKIP_DAILY='" + FL + "',@SKIP_INT='" + FLT + "',@AsonDate='" + conn.ConvertDate(FDate).ToString() + "' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    
    public DataTable GetDayBoopkD(string Fdate, string BRCD)
    {
        try
        {
            sql = " SELECT GLNAME,RGL,RSGL,RAC,RCNAME,SUM(CCR) CCR,SUM(CBR) CBR,SUM(CTR) CTR,(SUM(CCR)+SUM(CBR)+SUM(CTR)) TOTAL,GLNAME1,PGL,PSGL,PAC,PCNAME,SUM(DCR) DCR,SUM(DBR) DBR,SUM(DTR) DTR,(SUM(DCR)+SUM(DBR)+SUM(DTR)) TOTAL1 " +
                " FROM  " +
                " (  " +
                " SELECT  " +
                "(CASE WHEN ACTIVITY IN (3,5,7) THEN  M.CUSTNAME ELSE '' END) RCNAME,  " +
                "(CASE WHEN ACTIVITY IN (4,6,8) THEN  M.CUSTNAME ELSE '' END) PCNAME,     " +
               " (CASE WHEN ACTIVITY IN (3,5,7) THEN  AM.ACCNO ELSE 0 END) RAC,  " +
               " (CASE WHEN ACTIVITY IN (4,6,8) THEN  AM.ACCNO ELSE 0 END) PAC,  " +
                "(CASE WHEN ACTIVITY IN (3,5,7) THEN  GL.GLNAME ELSE '' END) GLNAME " +
                "(CASE WHEN ACTIVITY IN (4,6,8) THEN  GL.GLNAME ELSE '' END) GLNAME1,  " +
               " (CASE WHEN ACTIVITY IN (3,5,7) THEN  AM.GLCODE ELSE 0 END) RGL,  " +
                "(CASE WHEN ACTIVITY IN (4,6,8) THEN  AM.GLCODE ELSE 0 END) PGL,  " +
                "(CASE WHEN ACTIVITY IN (3,5,7) THEN  AM.SUBGLCODE ELSE 0 END) RSGL, " +
                "(CASE WHEN ACTIVITY IN (4,6,8) THEN  AM.SUBGLCODE ELSE 0 END) PSGL,  " +
                "(CASE WHEN TRXTYPE=1 AND ACTIVITY=3 THEN SUM(AMOUNT) ELSE 0 END )CCR,  " +
                "(CASE WHEN TRXTYPE=1 AND ACTIVITY=5 THEN SUM(AMOUNT) ELSE 0 END)CBR,  " +
                "(CASE WHEN TRXTYPE=1 AND ACTIVITY=7 THEN SUM(AMOUNT) ELSE 0 END )CTR,  " +
                "(CASE WHEN TRXTYPE=2 AND ACTIVITY=4 THEN SUM(AMOUNT) ELSE 0 END )DCR,  " +
                "(CASE WHEN TRXTYPE=2 AND ACTIVITY=6 THEN SUM(AMOUNT) ELSE 0 END )DBR,  " +
                "(CASE WHEN TRXTYPE=2 AND ACTIVITY=8 THEN SUM(AMOUNT) ELSE 0 END )DTR  " +
                "FROM AVSM_201609 AM  " +
                "INNER JOIN GLMAST GL ON GL.GLCODE=AM.GLCODE AND GL.SUBGLCODE =AM.SUBGLCODE AND GL.BRCD=AM.BRCD " +
                "INNER JOIN AVS_ACC AC ON AC.GLCODE=AM.GLCODE AND AC.SUBGLCODE=AM.SUBGLCODE AND AC.ACCNO=AM.ACCNO AND AC.BRCD = AM.BRCD " +
                "INNER JOIN MASTER M ON M.CUSTNO=AC.CUSTNO AND M.BRCD=AC.BRCD " +
                "WHERE AM.ENTRYDATE='" + Fdate + "' AND AM.BRCD='" + BRCD + "'  " +
               " GROUP BY GL.GLNAME,TRXTYPE,ACTIVITY,AM.GLCODE,AM.SUBGLCODE,AM.ACCNO,M.CUSTNAME  " +
                ") MAIN  " +
                "GROUP BY GLNAME,GLNAME1,RGL,PGL,RSGL,PSGL,RAC,PAC,RCNAME,PCNAME ORDER BY GLNAME,GLNAME1 ";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public double GetOpening(string BranchID, string Type, string Fdate) //Rakesh 09-12-2016 Op/Cl cash 
    {
        try
        {
            sql = "Exec RptCashPostionReport_Day '" + BranchID + "','" + Type + "','" + conn.ConvertDate(Fdate).ToString() + "' ";
            BAL = Convert.ToDouble(conn.sExecuteScalar(sql));

            //string B = "";
            //sql = "Exec RptCashPostionReport_Day '" + Type + "','" + conn.ConvertDate(Fdate).ToString() + "' ";
            //B = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BAL;
    }
    public double GetBankOpening (string BranchID, string Type, string Fdate) //Rakesh 09-12-2016 Op/Cl cash 
    {
        try
        {
            sql = "Exec RptBankPostionReport_Day '" + BranchID + "','" + Type + "','" + conn.ConvertDate(Fdate).ToString() + "' ";
            BAL = Convert.ToDouble(conn.sExecuteScalar(sql));

            //string B = "";
            //sql = "Exec RptCashPostionReport_Day '" + Type + "','" + conn.ConvertDate(Fdate).ToString() + "' ";
            //B = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BAL;
    }
    
}