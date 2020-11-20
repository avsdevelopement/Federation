using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClsFDPrint
/// </summary>
public class ClsFDPrint
{
    string sql = "";
    DbConnection DBconn = new DbConnection();
    DataTable DT = new DataTable();
	public ClsFDPrint()
	{
	}
    public DataTable FDShivsamarth(string BRCD, string SGL, string accno, string MID, string EDT, string FL)
    {
        try
        {
            sql = "Exec SP_FDPRINTING @Flag='" + FL + "', @BRCD='" + BRCD + "',@SubGlCode='" + SGL + "',  @Accno='" + accno + "',@MID='" + MID + "',@EDT='" + DBconn.ConvertDate(EDT) + "'";
            DT = DBconn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public DataTable GetFDPara(string Flag)
    {
        try
        {
            sql = "Exec AVS_FDPara @Flag='" + Flag + "'";
            DT = DBconn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
}