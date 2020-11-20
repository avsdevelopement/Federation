using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClsGlMast
/// </summary>
public class ClsGlMast
{
    DbConnection conn = new DbConnection();
    string sql = string.Empty;
    DataTable DT = new DataTable();
	public ClsGlMast()
	{
		//
		// TODO: Add constructor logic here
		//
	}



    public DataTable GetGLDetails(string Subglcode, string BRCD)
    {
        try
        {
            sql = "select IsNull(UnOperate, '0') As UnOperate,ISNULL(GLNAME,'') as GLNAME,GLNAME+'_'+CONVERT(VARCHAR(10),GLCODE) GL,ISNULL(INTACCYN,'N') as INTACCYN from glmast where subglcode='" + Subglcode + "' and brcd='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
}