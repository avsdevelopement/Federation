using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsChequeIssueRegister
{
    string sql = "";
    int Result;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

	public ClsChequeIssueRegister()
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
            sql = "SELECT (CONVERT(VARCHAR(10),MAX(LASTNO)+1))+'-'+(CONVERT (VARCHAR(10),GLCODE))+'-'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' "
                  + " AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }

    public DataTable GetCustName(string AccT, string AccNo, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME,AC.OPENINGDATE FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO AND M.BRCD=AC.BRCD WHERE AC.ACCNO='" + AccNo + "' AND AC.SUBGLCODE='" + AccT + "' AND AC.BRCD='" + BRCD + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int BindGrid(GridView Gview, string BRCD, string ProdCode, string AccNo, string AsOnDate, string RButton)
    {
        try
        {
            if (RButton == "S")
            {
                sql = "SELECT SUBGLCODE,SUBGLNAME,ACCNO,ACCNAME,NOOFLEAVES,FSERIES,TSERIES,EFFECTDATE FROM AVS_INSTRUMENTISSUE WHERE BRCD = '" + BRCD + "' "
                      + " AND SUBGLCODE = '" + ProdCode + "' AND ACCNO = '" + AccNo + "' AND CONVERT(DATE, EFFECTDATE) <= '" + conn.ConvertDate(AsOnDate).ToString() + "' AND STAGE = 1003";
            }
            else if (RButton == "A")
            {
                sql = "SELECT SUBGLCODE,SUBGLNAME,ACCNO,ACCNAME,NOOFLEAVES,FSERIES,TSERIES,EFFECTDATE FROM AVS_INSTRUMENTISSUE WHERE BRCD = '" + BRCD + "' "
                      + " AND CONVERT(DATE, EFFECTDATE) <= '" + conn.ConvertDate(AsOnDate).ToString() + "' AND STAGE = 1003";
            }

            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetData(string BRCD, string ProdCode, string AccNo, string AsOnDate, string RButton)
    {
        DataTable DT = new DataTable();
        try
        {
            if (RButton == "S")
            {
                sql = "SELECT SUBGLCODE,SUBGLNAME,ACCNO,ACCNAME,NOOFLEAVES,FSERIES,TSERIES,EFFECTDATE FROM AVS_INSTRUMENTISSUE WHERE BRCD = '" + BRCD + "' "
                      + " AND SUBGLCODE = '" + ProdCode + "' AND ACCNO = '" + AccNo + "' AND CONVERT(DATE, EFFECTDATE) <= '" + conn.ConvertDate(AsOnDate).ToString() + "' AND STAGE = 1003";
            }
            else if (RButton == "A")
            {
                sql = "SELECT SUBGLCODE,SUBGLNAME,ACCNO,ACCNAME,NOOFLEAVES,FSERIES,TSERIES,EFFECTDATE FROM AVS_INSTRUMENTISSUE WHERE BRCD = '" + BRCD + "' "
                      + " AND CONVERT(DATE, EFFECTDATE) <= '" + conn.ConvertDate(AsOnDate).ToString() + "' AND STAGE = 1003";
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