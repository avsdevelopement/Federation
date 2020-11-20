using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClsAVS5057
/// </summary>
public class ClsAVS5057
{
    DbConnection conn = new DbConnection();
    string sql = "";

	public ClsAVS5057()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GETCUSTOPEN(string FL, string FBRCD, string TBRCD,string FDATE,string TDATE)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Exec ISP_AVS0092 @FLAG='" + FL + "',@FBRCD='" + FBRCD + "',@TBRCD='" + TBRCD + "',@FDATE='"+conn.ConvertDate(FDATE)+"',@TDATE='"+conn.ConvertDate(TDATE)+"'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return DT = null;
        }
        return DT;
    }
    public DataTable GETCUSTOPEN1(string FL, string FBRCD, string TBRCD,string EDATE,string GLCODE,string FDATE,string TDATE)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Exec ISP_AVS0092 @FLAG='" + FL + "',@FBRCD='" + FBRCD + "',@TBRCD='" + TBRCD + "',@EDate='"+conn.ConvertDate(EDATE)+"',@GLCODE='"+GLCODE+"',@FDATE='"+conn.ConvertDate(FDATE)+"',@TDATE='"+conn.ConvertDate(TDATE)+"'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return DT = null;
        }
        return DT;
    }
    public string GetCAccWMNo(string fbrcd, string tbrcd)
    {
        try
        {
            sql = "select count(accno) from avs_Acc where brcd between '"+fbrcd+"' and '"+tbrcd+"' and custno+brcd in (select custno+brcd from avs_contactd where brcd between '"+fbrcd+"' and '"+tbrcd+"')";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }
    public string GetCAcLoan(string fbrcd, string tbrcd)
    {
        try
        {
            sql = "select count(accno) from avs_Acc where brcd between '" + fbrcd + "' and '" + tbrcd + "' and glcode='3'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }
    public string GetCAcDep(string fbrcd, string tbrcd)
    {
        try
        {
            sql = "select count(accno) from avs_Acc where brcd between '" + fbrcd + "' and '" + tbrcd + "' and glcode='5'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }
    public string GetCAcLimit(string fbrcd, string tbrcd)
    {
        try
        {
            sql = "select count(custaccno) from LoanInfo where brcd between '" + fbrcd + "' and '" + tbrcd + "' and stage<>1004";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }
    public string GetCAcDep2(string fbrcd, string tbrcd)
    {
        try
        {
            sql = "select count(custaccno) from depositinfo where brcd between '" + fbrcd + "' and '" + tbrcd + "' and stage<>1004";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }
}