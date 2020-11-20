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
/// Summary description for ClsReport
/// </summary>
public class ClsReport
{
    DbConnection conn = new DbConnection();
    string sql = "", PA = "", instDate = "";
    int Result = 0;

	public ClsReport()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string Getaccno(string AT, string BRCD, string GLCD)
    {
        try
        {
            sql = " SELECT Isnull((CONVERT(VARCHAR(10),MAX(LASTNO)+1)),'0')+'-'+(CONVERT (VARCHAR(10),GLCODE))+'-'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }

    public DataTable GetCustName(string GLCODE, string ACCNO, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME+'_'+CONVERT(VARCHAR(10),AC.CUSTNO) CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO AND M.BRCD=AC.BRCD WHERE AC.ACCNO='" + ACCNO + "' AND AC.SUBGLCODE='" + GLCODE + "' AND AC.BRCD='" + BRCD + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}