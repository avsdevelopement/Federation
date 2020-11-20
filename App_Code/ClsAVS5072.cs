using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for ClsAVS5072
/// </summary>
public class ClsAVS5072
{
    string sql = "", StrResult = "";
    int IntResult = 0;
    DbConnection Conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
	public ClsAVS5072()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet GetReport(string FL, string EDT, string BRCD, string FMM, string FYY,string TMM,string TYY)
    {
        try
        {
            sql = "Exec Isp_AVS0109 @Flag='" + FL + "',@FMM='" + FMM + "',@FYY='" + FYY + "',@TMM='" + TMM + "',@TYY='" + TYY + "',@Edt='" + EDT + "',@Brcd='" + BRCD + "'";
            DT = Conn.GetDatatable(sql);

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
}