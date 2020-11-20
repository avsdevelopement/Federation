using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
public class ClsAVS5115
{
    DataTable DT = new DataTable();
    string sql = "";
    DbConnection conn = new DbConnection();
    
	public ClsAVS5115()
	{
		
	}
    public DataTable GetRecIntRec(string FBrcd, string TBrcd, string Prd, string Month, string Year)
    {
        try
        {

            sql = "Exec ISP_AVS0148 @Fbrcd='" + FBrcd + "',@Tbrcd='" + TBrcd + "',@Subgl='" + Prd + "',@Month='" + Month + "',@Year='" + Year + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
}