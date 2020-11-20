using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public class ClsCDR
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", STR = "";
    int Res = 0;

    public ClsCDR()
    {

    }

    public DataTable GetCDR(string Brcd, string OnDate)
    {
        try
        {
            sql = "Exec ISP_CDRatio @Flag='CDR',@Brcd='" + Brcd + "',@OnDate='" + conn.ConvertDate(OnDate) + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable GetCDRSUm(string Brcd, string OnDate)
    {
        try
        {
            sql = "Exec ISP_AVS0104 @Flag='CDR',@Brcd='" + Brcd + "',@OnDate='" + conn.ConvertDate(OnDate) + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}