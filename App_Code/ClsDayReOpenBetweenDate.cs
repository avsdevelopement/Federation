using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;

public class ClsDayReOpenBetweenDate
{
    DbConnection conn = new DbConnection();
    string sql = "";
    string SQuery = "";
    int result = 0;

	public ClsDayReOpenBetweenDate()
	{
		
	}

    public string GetBranchName(string BrCode)
    {
        try
        {
            sql = "Select MidName From BankName Where BrCd = '" + BrCode + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }

    public string checkAdmin(string BRCD, string LoginCode)
    {
        string setnumb = "";
        try
        {
            sql = "SELECT LOGINCODE FROM USERMASTER WHERE BRCD = '" + BRCD + "' AND USERGROUP = 1 AND LOGINCODE = '" + LoginCode + "' AND USERSTATUS = 1";
            setnumb = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return setnumb;
    }

    public int CheckLoginUser(string BrCode)
    {
        try
        {
            sql = "Select LOGINCODE, USERNAME, UG.GROUPDESC, 'LOGIN USER','BUL' As AFLAG From UserMaster UM INNER JOIN USERGROUP UG With(NoLock) On UG.GROUPCODE = UM.USERGROUP Where BRCD = '" + BrCode + "' And UG.GroupCode <> 1 And UM.UserStatus = 1";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return result;
        }
        return result;
    }

    public int DayReOpenBetweenDate(string BrCode, string FromDate, string ToDate, string LoginCode, string Mid)
    {
        try
        {
            sql = "Exec SP_DayReOpenBetweenDate @BranchCode = '" + BrCode + "', @FDate = '" + conn.ConvertDate(FromDate).ToString() + "', @TDate = '" + conn.ConvertDate(ToDate).ToString() + "', @LoginCode = '" + LoginCode + "', @Mid = '" + Mid + "', @PcName = '" + conn.PCNAME().ToString() + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
}