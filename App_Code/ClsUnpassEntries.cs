using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsUnpassEntries
/// </summary>
public class ClsUnpassEntries
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    int Res = 0;
    string sql = "";
	public ClsUnpassEntries()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetUnpassDetails(string FBRCD,string TBRCD, string AsOn)
    {
        try
        {
            sql = "EXEC Isp_Unpass_Details @Flag='UNP',@Edt='" + conn.ConvertDate(AsOn) + "',@FBrcd='" + FBRCD + "',@TBrcd='" + TBRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}