using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;


/// <summary>
/// Summary description for CLsShareAccountStatment
/// </summary>
public class CLsShareAccountStatment
{
    string sql, sResult, TableName, sqlc, sqld;
    int Result = 0;
    double Limit = 0;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

    public CLsShareAccountStatment()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable getAccstatment(string FL, string BRCD, string EDT, string ACCNO)
    {
        try
        {
            sql = "Exec ISP_AVS0110 @Flag='" + FL + "',@FromDate='2015-04-01',@ToDate='" + conn.ConvertDate(EDT) + "',@BRCD='" + BRCD + "',@ACCNO='" + ACCNO + "'";
          //  sql = "exec ISP_AVS0110 '2015-04-01','" + conn.ConvertDate(entrydate) + "','" + brcd + "','" + accno + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

        return DT;

    }

    public int GetStatementGrid(GridView GD, string FL, string BRCD, string EDT,string CUSTNO, string ACCNO)
    {
        try
        {
            sql = "Exec ISP_AVS0110 @Flag='" + FL + "',@FromDate='2015-04-01',@ToDate='" + conn.ConvertDate(EDT) + "',@BRCD='" + BRCD + "',@CUSTNO='" + CUSTNO + "',@ACCNO='" + ACCNO + "'";
            Result = conn.sBindGrid(GD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }

        return Result;
    }

    //
}