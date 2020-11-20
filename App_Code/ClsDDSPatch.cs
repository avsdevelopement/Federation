using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// Summary description for ClsDDSPatch
/// </summary>
public class ClsDDSPatch
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "";
    int Result = 0;
	public ClsDDSPatch()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetRecords(string brcd, string prcd, string accno)
    {
        sql = "select convert(varchar(10),OPENINGDATE,103)OPENINGDATE,convert(varchar(10),CLOSINGDATE,103)CLOSINGDATE,convert(varchar(10),LASTINTDT,103)LASTINTDT,round(convert(float,D_AMOUNT),2) D_AMOUNT,ACC_TYPE,ACC_STATUS from avs_acc where brcd='" + brcd + "' and subglcode='" + prcd + "' and accno='" + accno + "'";
        DataTable DT = new DataTable();
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public int Insertolddata(string brcd,string prcd,string accno,string opendate,string lastint,string closedate,string dailyamt,string mid,string acctype,string accstatus)
    {
        sql = "INSERT INTO AVS5055(BRCD,PRCD,ACCNO,ACCOPENDATE,LASTINTDATE,CLOSINGDATE,DAILYAMT,MID,STAGE,STATUS,AccStatus,AccType,sysdate) VALUES('" + brcd + "','" + prcd + "','" + accno + "','" + conn.ConvertDate(opendate) + "','" + conn.ConvertDate(lastint) + "','" + conn.ConvertDate(closedate) + "','" + dailyamt + "','" + mid + "','1001','1','"+accstatus+"','"+acctype+"',getdate())";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public int UpdateData(string brcd, string prcd, string accno, string opendate, string lastint, string closedate, string dailyamt, string mid,string accstatus,string acctype)
    {
        sql = "UPDATE AVS_ACC SET OPENINGDATE='" + conn.ConvertDate(opendate) + "', LASTINTDT='" + conn.ConvertDate(lastint) + "',CLOSINGDATE='" + conn.ConvertDate(closedate) + "',D_AMOUNT='" + dailyamt + "',MID='" + mid + "',ACC_STATUS='" + accstatus + "',ACC_TYPE='" + acctype + "' WHERE BRCD='" + brcd + "' AND SUBGLCODE='" + prcd + "' AND ACCNO='" + accno + "'";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public int binddata(GridView Gview,string BRCD,string PRCD,string ACCNO)
    {
        try
        {
            sql = "select A.BRCD,A.PRCD,A.ACCNO,convert(varchar(10),A.ACCOPENDATE,103)ACCOPENDATE,convert(varchar(10),A.LASTINTDATE,103)LASTINTDATE,convert(varchar(10),A.CLOSINGDATE,103)CLOSINGDATE,A.DAILYAMT,A.AccStatus,L.DESCRIPTION AS STATUSNAME ,A.AccType,L1.DESCRIPTION AS TYPENAME,A.sysdate,U.USERNAME from avs5055 A INNER JOIN USERMASTER U ON A.MID=U.PERMISSIONNO LEFT join lookupform1 l on A.AccStatus=L.SRNO AND L.LNO=1047 LEFT JOIN lookupform1 l1 on A.AccType=L1.SRNO AND L.LNO=1016 WHERE A.brcd='"+BRCD+"' and A.PRCD='"+PRCD+"' AND A.ACCNO='"+ACCNO+"'";
            Result = conn.sBindGrid(Gview, sql);

        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;


    }
    public string CheckGlcode(string brcd,string subglcode)
    {
        sql = "select GLCODE from glmast where brcd='"+brcd+"' and subglcode='"+subglcode+"'";
        string sResult = conn.sExecuteScalar(sql);
        return sResult;
    }
    public int UpdateLoanInfo(string brcd,string prcd,string accno,string status,string LastIntdate)
    {
        sql = "update loaninfo set lmstatus='" + status + "' ,LASTINTDATE='"+conn.ConvertDate(LastIntdate)+"' where brcd='" + brcd + "' and loanglcode='" + prcd + "' and custaccno='" + accno + "'";
        Result = conn.sExecuteQuery(sql);   
        return Result;
    }
    public int UpdateDepositInfo(string brcd, string prcd, string accno, string status, string LastIntdate)
    {
        sql = "update DEPOSITINFO set LMSTATUS='" + status + "', LASTINTDATE='" + conn.ConvertDate(LastIntdate) + "' where brcd='" + brcd + "' and DEPOSITGLCODE='" + prcd + "' and custaccno='" + accno + "'";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }
    public int UpdateDDSHistory(string brcd, string prcd, string accno, string status, string LastIntdate,string Opendate)
    {
        sql = "update avs_ddshistory  set ENTRYDATE='" + conn.ConvertDate(LastIntdate) + "',OPENINGDATE='" + conn.ConvertDate(Opendate) + "' where brcd='" + brcd + "' and subglcode='" + prcd + "' and accno='" + accno + "'";
        Result = conn.sExecuteQuery(sql);
        return Result;
    }
}