using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsAVS51186
/// </summary>
public class ClsAVS51186
{
    DbConnection conn = new DbConnection();
    DataTable dt1 = new DataTable();
    int res = 0;
    string sql = "", srno = "", custno = "", stage = "", usergrp = "";
    string TableName = "";
	public ClsAVS51186()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertMovemetDetail(string BRCD, string CASENO, string CASEYAER, string MEMNO, string SRO_NO, string ENTRYDATE,string SOCIETYNAME,
     string Movementdate,  string MovementdDet, string RECAMT, string CASESTATUSE, string ACTIONSTATUS, string MID)//string S_O_DT, string ImmuvableDate, string MovableDate, 
    {
        try
        {
            sql = "exec AN_S005 @FL='AD',@BRCD='"+BRCD+"',@PRDCD='',@SRO_NO='"+SRO_NO+"',@CASE_YEAR='"+CASEYAER+"',@CASENO='"+CASENO+"',@MEMBERNO='',@SOCIETYNAME='"+SOCIETYNAME+"', @accno='' ,@ENTRYDATE='"+conn.ConvertDate(ENTRYDATE)+"' ,@MOVEMENTDATE='"+conn.ConvertDate(Movementdate)+"' ,	@MOVEMENTDETAIL='"+MovementdDet+"' ,"+
 	       "@RECAMOUNT='"+RECAMT+"' ,@CASESTATUSNO='"+CASESTATUSE+"' ,@ACTIONSTATUSNO='"+ACTIONSTATUS+"' ,@MID='"+MID+"',@CID='',@VID='',@PCMAC=''";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int ModifyMovemetDetail(string BRCD, string CASENO, string CASEYAER, string MEMNO, string SRO_NO, string ENTRYDATE, string SOCIETYNAME,
    string Movementdate, string MovementdDet, string RECAMT, string CASESTATUSE, string ACTIONSTATUS, string MID)//string S_O_DT, string ImmuvableDate, string MovableDate, 
    {
        try
        {
            sql = "exec AN_S005 @FL='MD',@BRCD='" + BRCD + "',@PRDCD='',@SRO_NO='" + SRO_NO + "',@CASE_YEAR='" + CASEYAER + "',@CASENO='" + CASENO + "',@MEMBERNO='',@SOCIETYNAME='" + SOCIETYNAME + "', @accno='' ,@ENTRYDATE='" + conn.ConvertDate(ENTRYDATE) + "' ,@MOVEMENTDATE='" + conn.ConvertDate(Movementdate) + "' ,	@MOVEMENTDETAIL='" + MovementdDet + "' ," +
           "@RECAMOUNT='" + RECAMT + "' ,@CASESTATUSNO='" + CASESTATUSE + "' ,@ACTIONSTATUSNO='" + ACTIONSTATUS + "',@MID='" + MID + "',@CID='',@VID='',@PCMAC=''";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int AuthoriseMovemetDetail(string BRCD, string CASENO, string CASEYEAR, string SRO_NO, string VID)
    {
        try
        {
            sql = "exec AN_S005 @FL='AT',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASEYEAR + "',@SRO_NO='" + SRO_NO + "',@VID='" + VID + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int deleteMovemetDetail(string BRCD, string CASENO, string CASEYEAR, string SRO_NO, string VID)
    {
        try
        {
            sql = "exec AN_S005 @FL='AT',@BRCD='" + BRCD + "',@CASENO='" + CASENO + "',@CASE_YEAR='" + CASEYEAR + "',@SRO_NO='" + SRO_NO + "',@VID='" + VID + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public DataTable ViewDetailsMOV(string BRCD, string CASENO, string CASE_YEAR)
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "exec AN_S005 @FL='VW', @CASENO	='" + CASENO + "', @CASE_YEAR='" + CASE_YEAR + "'";//
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public string GetMemberID(string CASENO, string CASE_YEAR,string brcd)
    {
        sql = "exec AN_S005 @FL='GETMEMBERNAME', @CASENO='" + CASENO + "', @CASE_YEAR='" + CASE_YEAR + "', @BRCD='" + brcd + "'";// AND MID='" + MID + "' 
        srno = conn.sExecuteScalar(sql);
        return srno;
    }
}