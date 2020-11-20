using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsRDFDSettng
/// </summary>
public class ClsRDFDSettng
{
    DbConnection conn = new DbConnection();
    int result = 0;
    string sql = "";
    DataTable DT = new DataTable();
       
	public ClsRDFDSettng()
	{
		
	}
    public void BindRD(GridView grd)
    {
        try
        {
            sql = "select id,Heading,PRows,PColumn,MID,EntryDate,ColumnStatus from AVS_RDParameter";
            conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindFD(GridView grd)
    {
        try
        {
            sql = "select id,Heading,PRows,PColumn,MID,EntryDate,ColumnStatus from AVS_FDParameter";
            conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public DataTable GetRD(string id)
    {
        try
        {
            sql = "select id,Heading,PRows,PColumn,MID,EntryDate,ColumnStatus from AVS_RDParameter where id='" + id + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public int InsertRD(string Heading, string PRows, string PColumn, string MID, string ColumnStatus)
    {
        try
        {
            sql = "delete from AVS_RDParameter where ColumnStatus='" + ColumnStatus + "'";
            result = conn.sExecuteQuery(sql);
            sql = "insert into AVS_RDParameter(Heading,PRows,PColumn,MID,ColumnStatus) values('" + Heading + "','" + PRows + "','" + PColumn + "','" + MID + "',"+
                " '" + ColumnStatus + "')";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
    public int ModifyRD(string id, string Heading, string PRows, string PColumn, string MID, string ColumnStatus)
    {
        try
        {
            sql = "update AVS_RDParameter set Heading='" + Heading + "',PRows='" + PRows + "',PColumn='" + PColumn + "',MID='" + MID + "',ColumnStatus='" + ColumnStatus + "'  where id='" + id + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
    public int DeleteRD(string id)
    {
        try
        {
            sql = "Delete from AVS_RDParameter where id='" + id + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
    public DataTable GetFDDetails(string id)
    {
       try
        {
            sql = "select id,Heading,PRows,PColumn,ColumnStatus,EntryDate,MID from AVS_FDParameter where id='" + id + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
       return DT;
    }
    public int InsertFD(string Heading, string BRows, string Arows, string MID, string ColumnStatus)
    {
        try
        {
            sql = "delete from AVS_FDParameter where columnstatus='" + ColumnStatus + "' ";
            result = conn.sExecuteQuery(sql);
            sql = "insert into AVS_FDParameter(Heading,PRows,PColumn,MID,ColumnStatus) values('" + Heading + "','" + BRows + "','" + Arows + "','" + MID + "','" + ColumnStatus + "')";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
    public int ModifyFD(string id, string Heading, string PRows, string PColumn, string MID, string ColumnStatus)
    {
        try
        {
            sql = "update AVS_FDParameter set Heading='" + Heading + "',PRows='" + PRows + "',PColumn='" + PColumn + "',MID='" + MID + "',ColumnStatus='" + ColumnStatus + "' "+
            "where id='" + id + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
    public int DeleteFD(string id)
    {
        try
        {
            sql = "Delete from AVS_FDParameter where id='" + id + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
}