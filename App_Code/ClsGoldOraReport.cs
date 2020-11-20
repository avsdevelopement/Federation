using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClsGoldOraReport
/// </summary>
public class ClsGoldOraReport
{
    DbConnection conn = new DbConnection();
    string sql = "";
    public ClsGoldOraReport()
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

    public DataTable getgolddetails(string brcd1, string brcd2, string pcode, string asdate)
    {
        DataTable DT = new DataTable();
        sql = "EXEC SP_GOLDORAREPORT @BRCD1='" + brcd1 + "' ,@BRCD2='" + brcd2 + "' ,@FDT='" + conn.ConvertDate(asdate) + "',	@PRODUCTCODF='" + pcode + "'";
        DT = conn.GetDatatable(sql);
        return DT;

    }
}
