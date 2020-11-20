using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data.SqlClient;

public class ClsCashReceiptAuthDo
{
    string sql;
    int result = 0;
    DataTable DT = new DataTable();
    DbConnection conn = new DbConnection();

	public ClsCashReceiptAuthDo()
	{
		
	}

    public DataTable Getformdata(string brcd, string setno)
    {
        try 
        {
        sql = " select AV.ENTRYDATE ENTRYDATE, AV.SUBGLCODE SUBGLCODE, GL.GLNAME ACCOUNTNAME, AV.ACCNO ACCNO, M.CUSTNAME CUSTNAME, AV.PARTICULARS PARTICULARS, AV.PARTICULARS2 PARTICULARS2, AV.CREDIT AMOUNT FROM ALLVCR AV " +
             " LEFT JOIN GLMAST GL ON GL.BRCD =AV.BRCD AND GL.SUBGLCODE=AV.SUBGLCODE " +
             " LEFT JOIN AVS_ACC AC ON AC.ACCNO=AV.ACCNO AND AC.SUBGLCODE=AV.SUBGLCODE AND AC.BRCD=AV.BRCD " +
             " LEFT JOIN MASTER M ON M.CUSTNO=AC.CUSTNO AND M.BRCD=AC.BRCD " +
             " WHERE AV.CREDIT <>'0' AND AV.BRCD='"+brcd+"' AND AV.SETNO='"+setno+"' AND AV.STAGE ='1001' ";
        DT=conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);

        }   
        return DT;
    }

    // Entry After Authorization
    public int fundingCash(string brcd, string setno, string tablename)
    {
        int result = 0;      
        try 
        {
        string sql = " INSERT INTO  AVSM_"+tablename+" "+
                     " (ENTRYDATE, POSTINGDATE, FUNDINGDATE, GLCODE, SUBGLCODE, ACCNO, PARTICULARS, AMOUNT, TRXTYPE, ACTIVITY, PMTMODE, SETNO, SCROLLNO, PARTICULARS2, INSTRUMENTNO, INSTRUMENTDATE, INSTBANKCD, INSTBRCD, STAGE, RTIME, BRCD, MID, PCMAC, PAYMAST, CUSTNO, CUSTNAME, REFID, SYSTEMDATE) "+
                     " SELECT ENTRYDATE, POSTINGDATE, FUNDINGDATE, GLCODE, SUBGLCODE, ACCNO, PARTICULARS, (case when credit>0 then credit else debit end) AMOUNT,(case when credit>0 then '1' else '2' end) TRXTYPE, ACTIVITY, PMTMODE, SETNO, SCROLLNO, PARTICULARS2, INSTRUMENTNO, INSTRUMENTDATE, INSTBANKCD, INSTBRCD, '1001', RTIME, BRCD, MID, PCMAC, PAYMAST, CUSTNO, CUSTNAME, REFID, SYSTEMDATE FROM ALLVCR WHERE SETNO='"+setno+"' AND BRCD= '"+brcd+"' AND STAGE=1003 ";        
        result=conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);

        }   
        return result;
    }

    // Authorization
    public int AuthorizeCash(string SETNO, string BRCD, string MID)
    {
        try 
        {
        sql = "UPDATE ALLVCR SET STAGE = '1003', CID = '"+MID+"' WHERE STAGE = '1001' AND BRCD = '"+BRCD+"' AND SETNO = '"+SETNO+"'";
        result=conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);

        }   
        return result;
    }

    public int Insertpara(string FL, string POID, string P_EDATE, string P_BRCD, string P_OWGT, string P_ENTRYDT, string P_FUNDDT, string P_STS, string P_CLGGLNO, string P_CLGNAME, string P_RTGLNO, string P_RTGLNAME)
    {
        int Result = 0;
        try 
        {
        SqlCommand cmd = new SqlCommand("PACK_PARAMETER.INSERT_UPDATE_OWG", conn.GetDBConnection());
        cmd.CommandType = CommandType.StoredProcedure;
        //cmd.BindByName = true;
        cmd.Parameters.Add("PFLAG", OracleDbType.Varchar2).Value = FL;
        cmd.Parameters.Add("P_OID", OracleDbType.Decimal).Value = POID;
        cmd.Parameters.Add("P_EDATE", OracleDbType.Date).Value = Convert.ToDateTime(P_EDATE).ToString("MM/dd/yyyy");
        cmd.Parameters.Add("P_BRCD", OracleDbType.Decimal).Value = P_BRCD;
        cmd.Parameters.Add("P_OWGT", OracleDbType.Varchar2).Value = P_OWGT;
        cmd.Parameters.Add("P_ENTRYDT", OracleDbType.Decimal).Value = P_ENTRYDT;
        cmd.Parameters.Add("P_FUNDDT", OracleDbType.Decimal).Value = P_FUNDDT;
        cmd.Parameters.Add("P_STS", OracleDbType.Decimal).Value = P_STS;
        cmd.Parameters.Add("P_CLGGLNO", OracleDbType.Decimal).Value = P_CLGGLNO;
        cmd.Parameters.Add("P_CLGNAME", OracleDbType.Varchar2).Value = P_CLGNAME;
        cmd.Parameters.Add("P_RTGLNO", OracleDbType.Decimal).Value = P_RTGLNO;
        cmd.Parameters.Add("P_RTGLNAME", OracleDbType.Varchar2).Value = P_RTGLNAME;
        Result = cmd.ExecuteNonQuery();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);

        }   
        return Result;
    }
}