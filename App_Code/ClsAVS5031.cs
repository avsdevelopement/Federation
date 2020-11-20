using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsAVS5031
/// </summary>
public class ClsAVS5031
{
    DbConnection conn = new DbConnection();
    int result = 0;
    string sql = "";
    public ClsAVS5031()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int insert(string Heading, string BRows, string Arows, string MID, string ColumnStatus, string Cno,string Glcode)
    {
        try
        {
            sql = "delete from AVS_PASSBOOK where Heading='" + Heading + "' and glcode='"+Glcode+"'";
            result = conn.sExecuteQuery(sql);
            sql="insert into AVS_PASSBOOK(Heading,BRows,Arows,MID,ColumnStatus,Cno,glcode) values('"+Heading+"','"+BRows+"','"+Arows+"','"+MID+"','"+ColumnStatus+"','"+Cno+"','"+Glcode+"')";
            result=conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
    public int Modify(string id, string Heading, string BRows, string Arows, string MID, string ColumnStatus, string Cno,string Glcode)
    {
        try
        {
            sql = "update AVS_PASSBOOK set Heading='" + Heading + "',BRows='" + BRows + "',Arows='" + Arows + "',MID='" + MID + "',ColumnStatus='" + ColumnStatus + "',Cno='" + Cno + "',glcode='"+Glcode+"' where id='" + id + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
    public int Delete(string id)
    {
        try
        {
            sql = "Delete from AVS_PASSBOOK where id='" + id + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
    public DataTable getdetails(string id)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "select ID,Heading,BRows,Arows,ColumnStatus,EntryDate,Cno,mid,GLCODE from AVS_PASSBOOK where id='" + id + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public void binddetails(GridView grd)
    {
        try
        {
            sql = "select ID,Heading,BRows,Arows,ColumnStatus,EntryDate,Cno,mid,glcode from AVS_PASSBOOK";
            conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void bindcoverdetails(GridView grd)
    {
        try
        {
            sql = "select id,Heading,PRows,PColumn,ColumnStatus,EntryDate,CNO,MID from AVS_PASSBOOKpara";
            conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public DataTable getdetailscover(string id)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "select id,Heading,PRows,PColumn,ColumnStatus,EntryDate,CNO,MID from AVS_PASSBOOKpara where id='" + id + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public int insertCover(string Heading, string PRows, string PColumn, string MID, string ColumnStatus)
    {
        try
        {
            sql = "delete from AVS_PASSBOOKpara where Heading='" + Heading + "'";
            result = conn.sExecuteQuery(sql);
            sql = "insert into AVS_PASSBOOKpara(Heading,PRows,PColumn,MID,ColumnStatus) values('" + Heading + "','" + PRows + "','" + PColumn + "','" + MID + "','" + ColumnStatus + "')";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
    public int ModifyCover(string id, string Heading, string PRows, string PColumn, string MID, string ColumnStatus)
    {
        try
        {
            sql = "update AVS_PASSBOOKpara set Heading='" + Heading + "',PRows='" + PRows + "',PColumn='" + PColumn + "',MID='" + MID + "',ColumnStatus='" + ColumnStatus + "' where id='" + id + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
    public int DeleteCover(string id)
    {
        try
        {
            sql = "Delete from AVS_PASSBOOKpara where id='" + id + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
}