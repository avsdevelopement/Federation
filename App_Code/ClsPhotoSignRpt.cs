using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClsPhotoSignRpt
/// </summary>
public class ClsPhotoSignRpt
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "";
    int Result = 0, i = 0;
	public ClsPhotoSignRpt()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet PhotoSignRpt(string Flag, string BRCD, string PRDCD, string FDATE, string TDATE)
    {
        try
        {
            sql = "Exec Sp_PhotoSignRpt '" + Flag + "','" + BRCD + "','" + PRDCD + "','" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
                DS.Tables.Add(DT);
            else
                DS = null;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    public DataSet PhotoSignNotScannRpt(string Flag, string BRCD, string PRDCD, string FDATE, string TDATE)
    {
        try
        {
            sql = "Exec Sp_PhotoSignRpt '" + Flag + "','" + BRCD + "','" + PRDCD + "','" + conn.ConvertDate(FDATE) + "','" + conn.ConvertDate(TDATE) + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
                DS.Tables.Add(DT);
            else
                DS = null;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }
    public string GetPrdName(string AT, string BRCD)
    {
        try
        {
            sql = "SELECT GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "'";
            AT = conn.sExecuteScalar(sql);
            return AT;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }
}