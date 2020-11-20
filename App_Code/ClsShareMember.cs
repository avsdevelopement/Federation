using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class ClsShareMember
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "", STR = "";
    int Res = 0;
	public ClsShareMember()
	{
	}

    public string GetBranchName(string BrCode)
    {
        try
        {
            sql = "Select MidName From BankName Where BrCd = '" + BrCode + "' And BrCd <> 0";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }

    public string Getaccno(string AT, string BRCD)
    {
        try
        {
            sql = " SELECT (CONVERT (VARCHAR(10),GLCODE))+'_'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }

    public DataSet GetShareMemList(string BrCode, string FDate, string TDate, string EDate, string AppType)
    {
        try
        {
            sql = "Exec ISP_SHARERPT @BrCode = '" + BrCode + "', @FromDate = '" + conn.ConvertDate(FDate).ToString() + "', @ToDate = '" + conn.ConvertDate(TDate).ToString() + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @ApplType = '" + AppType + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
                DS.Tables.Add(DT);
            else
                DS = null;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    public DataSet GetShareMismatchList (string BrCode, string FDate, string TDate, string EDate, string AppType)
    {
        try
        {
            sql = "Exec SP_SharesInfoM @BRCD = '" + BrCode + "', @PFDT = '" + conn.ConvertDate(FDate).ToString() + "', @PTDT = '" + conn.ConvertDate(TDate).ToString() + "', @Flag = '" + AppType + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
                DS.Tables.Add(DT);
            else
                DS = null;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    
    public DataTable getsharenomini( string Brcd, string fdate, string todate)
    {
        try
        {
            sql = "Exec SP_nomiRegister '"+Brcd+"','" + conn.ConvertDate(fdate) + "','" + conn.ConvertDate(todate) + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable getShareBalList(string Brcd, string fdate)
    {
        try
        {
            sql = "Exec SP_SHAREBALLIST '"+Brcd+"','4','"+conn.ConvertDate(fdate)+"'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable getloanClose(string flag, string Brcd, string fdate, string todate)
    {
        try
        {
            sql = "Exec Isp_AVS0021 '"+flag+"','"+Brcd+"','"+conn.ConvertDate(fdate)+"','"+conn.ConvertDate(todate)+"'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetAmountWise(string famt,string tamt, string Brcd, string fdate, string todate)
    {
        try
        {
            sql = "Exec Isp_SANCTIONAMTWISE '" + Brcd + "','" + conn.ConvertDate(fdate) + "','" + conn.ConvertDate(todate) + "','"+famt+"','"+tamt+"'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetDDSToLoanDetails(string glcode,string brcd)
    {
        try
        {
            sql = "Exec SP_DDSTOLOAN '"+brcd+"','"+glcode+"'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}