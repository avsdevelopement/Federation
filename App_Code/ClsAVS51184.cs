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
/// Summary description for ClsAVS51184
/// </summary>
public class ClsAVS51184
{
    DbConnection conn = new DbConnection();
    string sql = "";
    DataTable dt = new DataTable();
    SqlCommand cmd;
    int Result;
	public ClsAVS51184()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string GetAVS51184(string FLAG,string CUSTNO=null,string BRCD=null,string SUBGL=null,string ACCNO=null)
    {
        try
        {
            sql = "EXEC SP_AVS51184 @FLAG='" + FLAG + "',@CUSTNO='" + CUSTNO + "',@BRCD='" + BRCD + "',@SUBGL='" + SUBGL + "',@ACCNO='" + ACCNO + "'";

            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }
    public int GetAVS51184(string FLAG, string BRCD = null,  string SUBGL = null, string CUSTNO = null, string ACCNO = null,string RECOVERYTYPE=null, string OLDSUBS = null, string NEWSUBS = null, string EFFECTDATE = null,  string MID = null, string YEAR = null, string MONTH = null, string REASON = null)
    {
        int val = 0;
        try
        {
            sql = "EXEC SP_AVS51184 @FLAG='" + FLAG + "',@BRCD=" + BRCD + ",@SUBGL=" + SUBGL + ",@CUSTNO=" + CUSTNO + ",@ACCNO=" + ACCNO + ",@RECOVERYTYPE=" + RECOVERYTYPE + ",@OLDSUBS=" + OLDSUBS + ",@NEWSUBS=" + NEWSUBS + ",@EFFECTDATE='" + conn.ConvertDate(EFFECTDATE) + "',@MID=" + MID + ",@PCMAC='" + conn.PCNAME() + "',@YEAR=" + YEAR + ",@MONTH=" + MONTH + ",@REASON='" + REASON + "'";
            val  = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return val;
    }

    public string GetAVS511841(string FLAG, string BRCD = null, string SUBGL = null, string CUSTNO = null, string ACCNO = null, string RECOVERYTYPE = null, string OLDSUBS = null, string NEWSUBS = null, string EFFECTDATE = null, string MID = null, string YEAR = null, string MONTH = null, string REASON = null)
    {
        string val = "";
        try
        {
            sql = "EXEC SP_AVS51184 @FLAG='" + FLAG + "',@BRCD=" + BRCD + ",@SUBGL=" + SUBGL + ",@CUSTNO=" + CUSTNO + ",@ACCNO=" + ACCNO + ",@RECOVERYTYPE=" + RECOVERYTYPE + ",@OLDSUBS=" + OLDSUBS + ",@NEWSUBS=" + NEWSUBS + ",@EFFECTDATE='" + conn.ConvertDate(EFFECTDATE) + "',@MID=" + MID + ",@PCMAC='" + conn.PCNAME() + "',@YEAR=" + YEAR + ",@MONTH=" + MONTH + ",@REASON='" + REASON + "'";
            val = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return val;
    }
   
   
    public string GetProductName(string PrdCode, string BRCD)//BRCD ADDED --Abhishek
    {
        string sql = "select ISNULL(GLNAME,'') from glmast WHERE SUBGLCODE ='" + PrdCode + "' and BRCD='" + BRCD + "'";
        string ProductName = conn.sExecuteScalar(sql);
        return ProductName;
    }

    public DataTable GetStage(string Custno, string BRCD)
    {
        try
        {
            sql = "SELECT STAGE FROM AVS1015 WHERE CUSTNO='" + Custno + "'and BRCD='" + BRCD + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public int GridViewBind(string FLAG, GridView GrdAcc)
    {

        try
        {
            sql = "EXEC SP_AVS51184 @FLAG='" + FLAG + "'";

            Result = conn.sBindGrid(GrdAcc, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public DataTable ShowGridView(string FLAG,string SUBGL,string ACCNO,string CUSTNO,string BRCD)
    {
        try
        {

            sql = "EXEC SP_AVS51184 @FLAG='" + FLAG + "',@BRCD=" + BRCD + ",@SUBGL=" + SUBGL + ",@CUSTNO=" + CUSTNO + ",@ACCNO=" + ACCNO + "";

            dt = conn.GetDatatable(sql);
        }

        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }
}