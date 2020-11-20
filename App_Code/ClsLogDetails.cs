using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsLogDetails
/// </summary>
public class ClsLogDetails
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "";
    int result;
	public ClsLogDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string GetBranchName(string brcd)
    {
        try
        {
            sql = "select MIDNAME from BANKNAME where BRCD='" + brcd + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }

    public int getLogDetails(GridView grlog, string brcd, string acti,string FDate,string TDate)
    {
        try
        {
            sql = "select BRCD,CONVERT(varchar(10),ENTRYDATE,103) AS ENTRYDATE,VID,SYSTEMDATE,PCMAC,IP,ACTIVITY,OLDVALUE,NEWVALUE,MID,ACTNO from avs500 where BRCD='" + brcd + "' and ACTNO='" + acti + "' and ENTRYDATE BETWEEN '"+conn.ConvertDate(FDate)+"' AND '"+conn.ConvertDate(TDate)+"'";
            result = conn.sBindGrid(grlog, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
    public DataTable GetLDetails(string BRCD, string ACTVT,string FDate,string TDate)//Dhanya shetty //19/09/2017  //modified by ashok misal as per sir instructions
    {

        sql = "EXEC Isp_RptLogdetails '" + BRCD + "','" + ACTVT + "','" + conn.ConvertDate(FDate) + "','" + conn.ConvertDate(TDate) + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    
}