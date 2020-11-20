using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClsRptGL
/// </summary>
public class ClsRptGL
{
    DbConnection conn = new DbConnection();
    string sql = "";
    DataTable DT = new DataTable();
	public ClsRptGL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetBranch(string BRCD)
    {
        try
        {
            sql = "select MIDNAME+'_'+Convert(nvarchar(20),BRCD) as Branch,MIDNAME from BANKNAME where BRCD='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}