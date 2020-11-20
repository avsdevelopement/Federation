using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;

public class ClsSavPassBookPrint
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0;

	public ClsSavPassBookPrint()
	{
		
	}

    public string GetAccType(string AccT, string BRCD)
    {
        try
        {
            sql = "SELECT GLNAME FROM GLMAST WHERE SUBGLCODE='" + AccT + "' AND BRCD='" + BRCD + "'";
            AccT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AccT;
    }

    public DataTable GetCustName(string AccT, string AccNo, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME,AC.OPENINGDATE FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO  WHERE AC.ACCNO='" + AccNo + "' AND AC.SUBGLCODE='" + AccT + "' AND AC.BRCD='" + BRCD + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable SavingPassbook(string brcd, string PFMONTH, string PTMONTH, string PFDT, string PTDT, string accno, string Pcode,string SR)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Exec RptPassBook @FMonth='"+PFMONTH+"',@TMonth='"+PTMONTH+"',@FromDate='"+conn.ConvertDate(PFDT)+"',@ToDate='"+conn.ConvertDate(PTDT)+"',@SubGlCode='"+Pcode+"',@Accno='"+accno+"',@BRCD='"+brcd+"',@SR='"+SR+"'";
            DT = new DataTable(sql);
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            return DT = null;
        }
        return DT;
    }

    public DataTable getCoverDetails(string Accno, string ProdCode, string BRCD, string Custno)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec Isp_AVS0014 @AccNo='"+Accno+"', @ProdCode='"+ProdCode+"',@BRCD='"+BRCD+"', @CustNo='"+Custno+"'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public DataTable GetPassbookPara(string flag,string Glcode)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec Isp_Avs0050 '"+flag+"','"+Glcode+"'"; 
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public string GetAccNoDigit(string AccNo, string Brcd,string ProdCode,string CustNo)
    {
        try
        {
            sql = "exec Isp_AVS0020 @AccNo='"+AccNo+"', @BRCD='"+Brcd+"', @ProdCode='"+ProdCode+"',@CustNo='"+CustNo+"'";
            AccNo = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return AccNo;
    }
    public DataTable GetTime(string BRCD)
    {
        DataTable Dt = new DataTable();
        try
        {
            sql = "Select M_Time,E_Time from BankName where BRCD='"+BRCD+"'";
            Dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
}