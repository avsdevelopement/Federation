using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClsTDACalculator
/// </summary>
public class ClsTDACalculator
{

    string sql = "", ResStr = "";
    int ResInt = 0;
    DbConnection Conn = new DbConnection();
	public ClsTDACalculator()
	{
		//
		// TODO: Add constructor logic here
		//
	}


 
    
    public string Get_TDSRate(string Fl, string Prdcd)
    {
        try
        {
           // sql = "Exec Isp_TDACalculator @Flag='" + Fl + "',@PrdCd='" + Prdcd + "'";
            sql = "Exec Isp_TDACalculator_CBS2 @Flag='" + Fl + "',@PrdCd='" + Prdcd + "'";
            sql = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
}