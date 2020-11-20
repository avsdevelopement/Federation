using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsAccountOpenCloseReport
{
    string sql = "";
    int Result;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

	public ClsAccountOpenCloseReport()
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

    public int BindGrid(GridView Gview, string BRCD, string ProdCode, string AsOnDate, string RButton)
    {
        try
        {
            string[] DATE;
            DATE = AsOnDate.ToString().Split('/');

            if (RButton == "OPENING")
            {
                sql = "SP_ACCOPCLRPT 'OPENING','" + BRCD + "','" + ProdCode + "','" + conn.ConvertDate(AsOnDate).ToString() + "','" + DATE[2].ToString() + "','" + DATE[1].ToString() + "'";
            }
            else if (RButton == "CLOSING")
            {
                sql = "SP_ACCOPCLRPT 'CLOSING','" + BRCD + "','" + ProdCode + "','" + conn.ConvertDate(AsOnDate).ToString() + "','" + DATE[2].ToString() + "','" + DATE[1].ToString() + "'";
            }

            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetData(string BRCD, string ProdCode, string AsOnDate, string RButton)
    {
        DataTable DT = new DataTable();
        try
        {
            string[] DATE;
            DATE = AsOnDate.ToString().Split('/');

            if (RButton == "OPENING")
            {
                sql = "SP_ACCOPCLRPT 'OPENING','" + BRCD + "','" + ProdCode + "','" + conn.ConvertDate(AsOnDate).ToString() + "','" + DATE[2].ToString() + "','" + DATE[1].ToString() + "'";
            }
            else if (RButton == "CLOSING")
            {
                sql = "SP_ACCOPCLRPT 'CLOSING','" + BRCD + "','" + ProdCode + "','" + conn.ConvertDate(AsOnDate).ToString() + "','" + DATE[2].ToString() + "','" + DATE[1].ToString() + "'";
            }

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            return DT = null;
        }
        return DT;
    }
}