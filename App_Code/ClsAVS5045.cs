using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
/// <summary>
/// ClsAVS5045 for SMS PArameter
/// </summary>
public class ClsAVS5045
{
    DbConnection conn = new DbConnection();
    string result = "", sql = "";
    int res = 0;
	public ClsAVS5045()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int insert(string brcd, string msg, string activity, string parameter, string mid)
    {
        try
        {
            sql = "insert into avs1090(BRCD,Message,Activity,Parameter,Mid,Stage) values('" + brcd + "','" + msg + "','" + activity + "','" + parameter + "','" + mid + "','1001')";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int Modify(string id,string brcd, string msg, string activity, string parameter, string mid)
    {
        try
        {
            sql = "Update avs1090 set BRCD='" + brcd + "',Message='" + msg + "',Activity='" + activity + "',Parameter='" + parameter + "',vid='" + mid + "',Stage='1002' where id='"+id+"'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int Delete(string id,string mid)
    {
        try
        {
            sql = "Update avs1090 set Stage='1004',vid='" + mid + "' where id='" + id + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int Authorise(string id,string mid)
    {
        try
        {
            sql = "Update avs1090 set Stage='1003',vid='" + mid + "' where id='" + id + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public DataTable getdetails(string id)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "select * from avs1090 where id='" + id + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public void bind(GridView grd)
    {
        try
        {
            sql = "select ID,BRCD,Message,Activity,Parameter from avs1090 where stage<>'1004' order by id";
            conn.sBindGrid(grd,sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
}