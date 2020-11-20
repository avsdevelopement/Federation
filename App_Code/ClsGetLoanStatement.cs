using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsGetLoanStatement
/// </summary>
public class ClsGetLoanStatement
{
    string sql = "";
    int Result;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

	public ClsGetLoanStatement()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetLnStatData(string FL, string FDate, string TDate, string PT, string FLT, string AC, string BRCD)
    {
        try
        {
            sql = "EXEC RptLaonAccStatement @FLAG ='" + FL + "', @FromDate ='" + conn.ConvertDate(FDate) + "', @ToDate='" + conn.ConvertDate(TDate) + "',@PRDTCD ='" + PT + "',@GLCD='" + FLT + "',@ACCNO ='" + AC + "',@BRCD='" + BRCD + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetLnStatDataIntcal(string FDate, string TDate, string PT, string FLT, string AC, string BRCD)
    {
        try
        {
            sql = "EXEC RptLoanIntCalSts  @FDATE ='" + conn.ConvertDate(FDate) + "', @TDATE ='" + conn.ConvertDate(TDate) + "',@PRDTCD ='" + PT + "',@GLCD='" + FLT + "',@ACCNO ='" + AC + "',@BRCD='" + BRCD + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}