using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsChangesView
/// </summary>
public class ClsChangesView
{
    string sql = "";
    int res = 0;
    DbConnection conn = new DbConnection();
    DataTable dt = new DataTable();
	public ClsChangesView()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int GetChangesData(GridView grd)
    {
        try
        {
            sql = "SELECT PID,REMARK,STATUS FROM AVSNEWCHANGE";
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    
    }

    public DataTable GetChangesDataTable(string pid)
    {
        try
        {
            sql = "SELECT PID,REMARK,STATUS FROM AVSNEWCHANGE where pid="+pid+"";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;

    }

    public int INSERT(string FL,string REMARK,string STATUS)
    {
        try
        {
            sql = "EXEC SP_NEWCHANGES @FLAG='"+FL+"',@PID='0',@REMARK=N'"+REMARK+"',@STATUS='"+STATUS+"'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;

    }

    public int UPDATE(string FL,string pid, string REMARK, string STATUS)
    {
        try
        {
            sql = "EXEC SP_NEWCHANGES @FLAG='" + FL + "',@PID='"+pid+"',@REMARK=N'" + REMARK + "',@STATUS='" + STATUS + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;

    }

    public int DELETE(string FL, string pid, string REMARK, string STATUS)
    {
        try
        {
            sql = "EXEC SP_NEWCHANGES @FLAG='" + FL + "',@PID='" + pid + "',@REMARK='" + REMARK + "',@STATUS='" + STATUS + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;

    }
    public DataTable GetChangesinfo()
    {
        try
        {
            sql = "SELECT REMARK FROM AVSNEWCHANGE where STATUS='yes'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;

    }
    public DataSet GetRecord()
    {
        DataSet ds = new DataSet();
        DataTable dtTdaInt = new DataTable();
        dtTdaInt = GetChangesinfo();
        ds.Tables.Add(dtTdaInt);
        return ds;
    }
    

}