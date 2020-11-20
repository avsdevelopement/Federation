using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsCustUnification
/// </summary>
public class ClsCustUnification
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int result = 0;
    public ClsCustUnification()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int GetData(GridView grd, string FL, string fbrcd, string tbrcd, string fsubgl, string tsubgl)
    {
        try
        {
            sql = "EXEC AN_CUSTUNIFICATION '" + FL + "','" + fbrcd + "','" + tbrcd + "','" + fsubgl + "','" + tsubgl + "'";
            result = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }

    public DataTable GetDataTbl(string FL, string fbrcd, string tbrcd, string fsubgl, string tsubgl)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "EXEC AN_CUSTUNIFICATION '" + FL + "','" + fbrcd + "','" + tbrcd + "','" + fsubgl + "','" + tsubgl + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable GetCustRept(string fdate, string tdate, string fbrcd, string tbrcd) //// Added by ankita on 19/06/2017 to display customer report
    {
        DataTable DT = new DataTable();
        try
        {
            string sql2 = "SELECT M.BRCD,M.CUSTNO,M.CUSTNAME,M.OPENINGDATE,U.LOGINCODE AS MID,U1.LOGINCODE AS CID FROM MASTER M LEFT JOIN USERMASTER U ON U.PERMISSIONNO=M.MID LEFT JOIN USERMASTER U1 ON U1.PERMISSIONNO=M.CID WHERE M.BRCD BETWEEN '" + fbrcd + "' AND '" + tbrcd + "' AND M.OPENINGDATE BETWEEN '" + conn.ConvertDate(fdate) + "' AND '" + conn.ConvertDate(tdate) + "' ORDER BY M.BRCD";
            DT = conn.GetDatatable(sql2);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}