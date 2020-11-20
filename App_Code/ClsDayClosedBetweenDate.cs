using System;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;

public class ClsDayClosedBetweenDate
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int result = 0;

	public ClsDayClosedBetweenDate()
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

    public string CheckAdmin(string BRCD, string LoginCode)
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

    public int CheckLoginUser(GridView Gview, string BrCode)
    {
        try
        {
            sql = "Select LOGINCODE, USERNAME, UG.GROUPDESC, 'LOGIN USER','BUL' As AFLAG From UserMaster UM INNER JOIN USERGROUP UG With(NoLock) On UG.GROUPCODE = UM.USERGROUP Where BRCD = '" + BrCode + "' And UG.GroupCode <> 1 And UM.UserStatus = 1";
            result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return result;
        }
        return result;
    }

    public int CheckDayStatus(string BrCode, string FromDate, string Todate)
    {
        try
        {
            sql = "Select Status From AVS1025 Where Brcd = '" + BrCode + "' And DayBeginDate = '" + conn.ConvertDate(FromDate) + "' And Status <> 99";
            result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public int BrHandBetweenDate(GridView Gview, string BrCode, string FromDate, string ToDate, string LoginCode, string Mid)
    {
        try
        {
            sql = "Exec Sp_BrHandoverBetweenDate @BranchCode = '" + BrCode + "', @FDate = '" + conn.ConvertDate(FromDate) + "' , @TDate = '" + conn.ConvertDate(ToDate).ToString() + "', @EDT = '" + conn.ConvertDate(FromDate) + "', @LoginCode = '" + LoginCode + "', @Mid = '" + Mid + "', @PcMac = '" + conn.PCNAME().ToString() + "'";
            result = conn.sBindGrid(Gview, sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public int DayCloseBetweenDate(string BrCode, string FromDate, string ToDate, string LoginCode, string Mid)
    {
        try
        {
            sql = "Exec Sp_DayCloseBetweenDate @BranchCode = '" + BrCode + "', @FDate = '" + conn.ConvertDate(FromDate).ToString() + "', @TDate = '" + conn.ConvertDate(ToDate).ToString() + "',@LoginCode = '" + LoginCode + "', @Mid = '" + Mid + "', @PcMac = '" + conn.PCNAME().ToString() + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }
}