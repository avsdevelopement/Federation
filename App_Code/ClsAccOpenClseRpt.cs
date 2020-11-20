using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClsAccOpenClseRpt
/// </summary>
public class ClsAccOpenClseRpt
{
    string sql = "";
    DbConnection conn = new DbConnection();
	public ClsAccOpenClseRpt()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetAccOpClRpt(string fl, string fdate, string tdate, string fbrcd, string tbrcd, string prd)////Added by ankita on 13/06/2017 To display account opening and closing details
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "EXEC AN_ACCOPCLRPT @flag='"+fl+"',@fbrcd='"+fbrcd+"',@tbrcd='"+tbrcd+"',@fdate='"+fdate+"',@tdate='"+tdate+"',@subgl='" + prd + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable GetLoanNillRpt(string fdate, string tdate, string fbrcd, string tbrcd, string fprd,string Tprdcd)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "EXEC Isp_AVS0091 '"+fbrcd+"','"+tbrcd+"','"+conn.ConvertDate(fdate)+"','"+conn.ConvertDate(tdate)+"','"+fprd+"','"+Tprdcd+"'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable GetDetailsForPLALLBrReport(string fdate, string tdate, string fbrcd, string tbrcd,string FL)
    {
        DataTable dt = new DataTable();
        try
        {
            if (FL == "R")
            {
                sql = "EXEC RptPLALLBrReport_CRDR @FBRCD='" + fbrcd + "',@TBRCD='" + tbrcd + "',@PFDT='" + conn.ConvertDate(fdate) + "',@PTDT='" + conn.ConvertDate(tdate) + "'";
                dt = conn.GetDatatable(sql);
            }
            else
            {
                sql = "EXEC RptPLALLBrReport_PayCRDR @FBRCD='" + fbrcd + "',@TBRCD='" + tbrcd + "',@PFDT='" + conn.ConvertDate(fdate) + "',@PTDT='" + conn.ConvertDate(tdate) + "'";
                dt = conn.GetDatatable(sql);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable GetDetailsForPLALLRt (string fdate, string tdate, string fbrcd, string tbrcd, string FL)
    {
        DataTable dt = new DataTable();
        try
        {
            if (FL == "R")
            {
                sql = "EXEC RptPLALLBrReport_Rec @FBRCD='" + fbrcd + "',@TBRCD='" + tbrcd + "',@PFDT='" + conn.ConvertDate(fdate) + "',@PTDT='" + conn.ConvertDate(tdate) + "'";
                dt = conn.GetDatatable(sql);
            }
            else
            {
                sql = "EXEC RptPLALLBrReport_Pay @FBRCD='" + fbrcd + "',@TBRCD='" + tbrcd + "',@PFDT='" + conn.ConvertDate(fdate) + "',@PTDT='" + conn.ConvertDate(tdate) + "'";
                dt = conn.GetDatatable(sql);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    
}