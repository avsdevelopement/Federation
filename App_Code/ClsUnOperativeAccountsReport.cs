using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsUnOperativeAccountsReport
{
    string sql = "";
    int Result;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

	public ClsUnOperativeAccountsReport()
	{
		
	}

    public string GetAccType(string AccT, string BRCD)
    {
        sql = "SELECT GLNAME FROM GLMAST WHERE SUBGLCODE='" + AccT + "'  AND BRCD='" + BRCD + "'";
        AccT = conn.sExecuteScalar(sql);
        return AccT;
    }

    public string Getaccno(string AT, string BRCD, string GLCD)
    {
        try
        {
            sql = " SELECT (CONVERT(VARCHAR(10),MAX(LASTNO)+1))+'-'+(CONVERT (VARCHAR(10),GLCODE))+'-'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }

    public int BindGrid(GridView Gview, string pcode, string AsOnDate, string fmonth, string fbr, string tbr)
    {
        try
        {
            sql = "SP_UNOPACCRPT '" + fbr + "','" + tbr + "','" + pcode + "','" + conn.ConvertDate(AsOnDate).ToString() + "','" + fmonth + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetData(string fbr, string tbr, string pcode, string AsOnDate, string fmonth)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SP_UNOPACCRPT '" + fbr + "','" + tbr + "','" + pcode + "','" + conn.ConvertDate(AsOnDate).ToString() + "','" + fmonth + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            return DT = null;
        }
        return DT;
    }
}