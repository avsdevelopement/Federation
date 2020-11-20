using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClsLoanCaseFile
/// </summary>
public class ClsLoanCaseFile
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int res = 0;
	public ClsLoanCaseFile()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetCustName(string GLCODE, string ACCNO, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME+'_'+CONVERT(VARCHAR(10),AC.CUSTNO) CUSTNAME,AC.ACC_STATUS FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO AND M.BRCD=AC.BRCD WHERE AC.ACCNO='" + ACCNO + "' AND AC.SUBGLCODE='" + GLCODE + "' AND AC.BRCD='" + BRCD + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetInfo(string accno, string subgl, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT * FROM LOANINFO WHERE BRCD='"+BRCD+"' AND CUSTACCNO='"+accno+"' AND LOANGLCODE='"+subgl+"' AND LMSTATUS=1 AND STAGE<>1004";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int update(string accno, string brcd, string custno, string subgl,string CASEOFDATE,string MID,string CID,string REASON)
    {
        try
        {
            sql = "exec SP_LOANCASEFILE @FLAG='SUBMIT',@BRCD='" + brcd + "',@LOANGLCODE='" + subgl + "',@ACCNO='" + accno + "',@CUSTNO='" + custno + "',@CASEOFDATE='" +conn.ConvertDate(CASEOFDATE) + "',@MID='" + MID + "',@CID='" + CID + "',@REASON='" + REASON + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }

    public int Autho(string accno, string brcd, string custno, string subgl, string CID)
    {
        try
        {
            sql = "exec SP_LOANCASEFILE @FLAG='AUTHO',@BRCD='" + brcd + "',@LOANGLCODE='" + subgl + "',@ACCNO='" + accno + "',@CUSTNO='" + custno + "',@CID='" + CID + "'";
            res = conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;
    }
}