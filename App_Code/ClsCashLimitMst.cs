using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsCashLimitMst
/// </summary>
public class ClsCashLimitMst
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int res = 0;
    DataTable DT = new DataTable();
	public ClsCashLimitMst()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int insert(string prdcd, string effectdate, string limit,string brcd,string mid,string vid)
    {
        try
        {
            sql = "EXEC SP_CASHLIMITMST @FLAG='AD',@SUBGLCODE='"+prdcd+"',@EFFECTIVEDATE='"+conn.ConvertDate(effectdate)+"',@LIMIT='"+limit+"',@BRCD='"+brcd+"',@MID='"+mid+"',@VID='"+vid+"'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int modify(string id,string prdcd, string effectdate, string limit, string brcd, string mid,string vid)
    {
        try
        {
            sql = "EXEC SP_CASHLIMITMST @FLAG='MD',@ID='"+id+"',@SUBGLCODE='" + prdcd + "',@EFFECTIVEDATE='" + conn.ConvertDate(effectdate) + "',@LIMIT='" + limit + "',@BRCD='" + brcd + "',@MID='" + mid + "',@VID='" + vid + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int delete(string id, string brcd, string mid,string vid)
    {
        try
        {
            sql = "EXEC SP_CASHLIMITMST @FLAG='DL',@ID='"+id+"',@BRCD='" + brcd + "',@MID='" + mid + "',@VID='" + vid + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    public int bindall(GridView grd, string brcd)
    {
        try
        {
            sql = "SELECT ID,SUBGLCODE,CONVERT(VARCHAR(10),EFFECTIVEDATE,103) EFFECTIVEDATE,LIMIT FROM avs_limit_mst WHERE STAGE<>'1004' and BRCD='" + brcd + "'";
            res = conn.sBindGrid(grd, sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
    
    public DataTable GetCashLimit(string id,string brcd)
    {
        DataTable dt=new DataTable();
        try
        {
            sql = "EXEC SP_CASHLIMITMST @FLAG='VW',@ID='" + id + "',@BRCD='" + brcd + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    
    }
    public DataTable CashLimitReport(string Ason, string BRCD,string SUBGL)
    {
        try
        { 
            DT = conn.GetDatatable("Exec SP_CASHLIMITREPORT '" +conn.ConvertDate(Ason) + "' , '" + BRCD + "','"+SUBGL+"'");
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
   
}