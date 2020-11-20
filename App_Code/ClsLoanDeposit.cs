using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsLoanDeposit
/// </summary>
public class ClsLoanDeposit
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0;
	public ClsLoanDeposit()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetLDSummary(string flag,string fdate,string tdate,string fbrcd,string tbrcd)
    {
        DataTable DT = new DataTable();
        try
        {

            sql = "EXEC ISP_AVS0102 @FLAG='"+flag+"',@FDATE='"+conn.ConvertDate(fdate)+"',@TDATE='"+conn.ConvertDate(tdate)+"',@FBRCD='"+fbrcd+"',@TBRCD='"+tbrcd+"'";
               DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}