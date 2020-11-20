using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClsInvInt
/// </summary>
public class ClsInvInt
{
    string sql = "";
    string Result = "";
    DataTable dt = new DataTable();
    DbConnection conn = new DbConnection();
	public ClsInvInt()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable GetAccNo(string FPRD, string TPRD,string flag,string BRCD,string MID,string Date)
    {
        try
        {
            sql = "exec Isp_AVS0084 @BRCD='" + BRCD + "',@FSub='" + FPRD + "',@TSub='" + TPRD + "',@Flag='" + flag + "',@MID='" + MID + "',@EDATE='"+conn.ConvertDate(Date)+"'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
}