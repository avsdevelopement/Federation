using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsAVS5038
/// </summary>
public class ClsAVS5038
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int res = 0;
	public ClsAVS5038()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void BindData(GridView grd)
    {
        try
        {
            sql = "select DIRNO,CUSTNO,DIRNAME,POST,MOBILENO,SMSYN,FROMDATE,TODATE,VID,MID,STAGE from director where stage<>'1004'";
            conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public int insert(string DIRNO, string DIRNAME, string POST, string MOBILENO, string CUSTNO, string FROMDATE, string TODATE, string SMSYN, string MID)
    {
        try
        {
            sql = "insert into director(DIRNO,DIRNAME,POST,MOBILENO,CUSTNO,FROMDATE,TODATE,SMSYN,MID,STAGE) values('" + DIRNO + "','" + DIRNAME + "','" + POST + "','" + MOBILENO + "','" + CUSTNO + "','" + FROMDATE + "','" + TODATE + "','" + SMSYN + "','" + MID + "','1001')";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int Modify(string DIRNO, string DIRNAME, string POST, string MOBILENO, string CUSTNO, string FROMDATE, string TODATE, string SMSYN, string MID)
    {
        try
        {
            sql = "update director set DIRNAME='" + DIRNAME + "',POST='" + POST + "',MOBILENO='" + MOBILENO + "',CUSTNO='" + CUSTNO + "',FROMDATE='" + FROMDATE + "',TODATE='" + TODATE + "',SMSYN='" + SMSYN + "',VID='" + MID + "',STAGE='1002' WHERE DIRNO='" + DIRNO + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int Delete(string DIRNO, string MID)
    {
        try
        {
            sql = "update director set VID='" + MID + "',STAGE='1004' WHERE DIRNO='" + DIRNO + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public DataTable getdata(string id)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "select DIRNO,CUSTNO,DIRNAME,POST,MOBILENO,SMSYN,convert(varchar(10),FROMDATE,103)FROMDATE,convert(varchar(10),TODATE,103)TODATE,VID,MID,STAGE from director where stage<>'1004' and DIRNO='"+id+"'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public string getsrno()
    {
        string srno = "";
        try
        {
            sql = "select isnull(max(DIRNO),0)+1 from director";
            srno = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return srno;
    }
    public string getmobile(string custno)
    {
        string mobile = "";
        try
        {
            sql = "select Mobile1 from avs_contactD where custno='" + custno + "'";
            mobile = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return mobile;
    }
}