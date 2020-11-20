using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClsDocDetails
/// </summary>
public class ClsDocDetails
{
    DbConnection conn = new DbConnection();
    string sql = "";
	public ClsDocDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetDocDetails(string RefId)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "select * from avs5040 where cust_ref_no='"+RefId+"'";
            dt = conn.GetDatatableMob(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
}