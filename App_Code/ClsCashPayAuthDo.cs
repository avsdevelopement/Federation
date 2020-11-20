using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class ClsCashPayAuthDo
{
    string sql;
    int result = 0;
    DataTable DT = new DataTable();
    DbConnection conn = new DbConnection();    

	public ClsCashPayAuthDo()
	{
		
	}

    public DataTable Getformdata(string brcd, string setno)
    {
        try 
        {
        sql = " select AV.ENTRYDATE ENTRYDATE, AV.SUBGLCODE SUBGLCODE, GL.GLNAME ACCOUNTNAME, AV.ACCNO ACCNO, M.CUSTNAME CUSTNAME, AV.PARTICULARS PARTICULARS, AV.PARTICULARS2 PARTICULARS2, AV.DEBIT AMOUNT FROM ALLVCR AV " +
             " LEFT JOIN GLMAST GL ON GL.BRCD =AV.BRCD AND GL.SUBGLCODE=AV.SUBGLCODE " +
             " LEFT JOIN AVS_ACC AC ON AC.ACCNO=AV.ACCNO AND AC.SUBGLCODE=AV.SUBGLCODE AND AC.BRCD=AV.BRCD " +
             " LEFT JOIN MASTER M ON M.CUSTNO=AC.CUSTNO AND M.BRCD=AC.BRCD " +
             " WHERE AV.DEBIT <>'0' AND AV.BRCD='" + brcd + "' AND AV.SETNO='" + setno + "' AND AV.STAGE ='1001' ";
        DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);

        }
        return DT;
    }

    // Authorization
    public int AuthorizeCash(string SETNO, string BRCD, string MID)
    {
        try 
        {
        sql = "UPDATE ALLVCR SET STAGE = '1003', CID = '" + MID + "' WHERE STAGE = '1001' AND BRCD = '" + BRCD + "' AND SETNO = '" + SETNO + "'";
        result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);

        }
        return result;
    }

    // Entry After Authorization
    public int fundingCash(string brcd, string setno, string tablename)
    {
        int result = 0;
      
        try
        { 
       
        string sql = " INSERT INTO  AVSM_" + tablename + " " +
                     " (ENTRYDATE, POSTINGDATE, FUNDINGDATE, GLCODE, SUBGLCODE, ACCNO, PARTICULARS, AMOUNT, TRXTYPE, ACTIVITY, PMTMODE, SETNO, SCROLLNO, PARTICULARS2, INSTRUMENTNO, INSTRUMENTDATE, INSTBANKCD, INSTBRCD, STAGE, RTIME, BRCD, MID, PCMAC, PAYMAST, CUSTNO, CUSTNAME, REFID, SYSTEMDATE) " +
                     " SELECT ENTRYDATE, POSTINGDATE, FUNDINGDATE, GLCODE, SUBGLCODE, ACCNO, PARTICULARS, (case when credit>0 then credit else debit end) AMOUNT,(case when credit>0 then '1' else '2' end) TRXTYPE, ACTIVITY, PMTMODE, SETNO, SCROLLNO, PARTICULARS2, INSTRUMENTNO, INSTRUMENTDATE, INSTBANKCD, INSTBRCD, '1001', RTIME, BRCD, MID, PCMAC, PAYMAST, CUSTNO, CUSTNAME, REFID, SYSTEMDATE FROM ALLVCR WHERE SETNO='" + setno + "' AND BRCD= '" + brcd + "' AND STAGE=1003 ";
        result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);

        }
        return result;
    }
}