using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data.SqlClient;

public class ClsCloseDateRange
{
    DbConnection conn = new DbConnection();
    string sql = "";
    string SQuery = "";
    int result = 0;

	public ClsCloseDateRange()
	{
		
	}

    public string checkAdmin(string BRCD, string LoginCode, string Mid)
    {
        string setnumb = "";
        try
        {
            sql = "Select LoginCode From UserMaster Where BRCD = '" + BRCD + "' And PermissionNo = '" + Mid.ToString() + "' And UserGroup = '1' And LoginCode = '" + LoginCode + "' AND UserStatus = 1";
            setnumb = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return setnumb;
    }

    public string openDay(string BRCD)
    {
        string wdt = "";
        try
        {
            sql = "Select ListValue From Parameter Where ListField = 'DayOpen' And BrCd = '" + BRCD + "' ";
            wdt = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return wdt;
    }

    public int CheckLoginUser(GridView Gview, string BrCode, string Mid)
    {
        try
        {
            sql = "Select U.BrCd, U.LoginCode, U.UserName, G.GroupDesc, U.PermissionNo, 'BUL' As AFLAG, 'LOGIN USER' As EntryType From UserMaster U " +
                  "Inner Join UserGroup G With(NoLock) On G.GroupCode = U.UserGroup " +
                  "Where U.BrCd = '" + BrCode.ToString() + "' And U.PermissionNo <> '" + Mid.ToString() + "' And G.GroupCode <> '1' And U.UserStatus = 1";
            result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return result;
        }
        return result;
    }

    public int BrHandActivity(GridView Gview, string BrCode, string FromDate, string ToDate, string LoginCode, string Mid)
    {
        try
        {
            sql = "Exec Sp_BrHandDateRange @BRCD = '" + BrCode + "', @FromDate = '" + conn.ConvertDate(FromDate).ToString() + "' , @ToDate = '" + conn.ConvertDate(ToDate).ToString() + "', @LoginCode = '" + LoginCode + "', @Mid = '" + Mid + "'";
            result = conn.sBindGrid(Gview, sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

    public int DayCloseActivity(string BrCode, string FromDate, string ToDate, string LoginCode, string Mid)
    {
        try
        {
            sql = "Exec Sp_DayClosedDateRange @BRCD = '" + BrCode + "', @FromDate = '" + conn.ConvertDate(FromDate).ToString() + "' , @ToDate = '" + conn.ConvertDate(ToDate).ToString() + "', @LoginCode = '" + LoginCode + "', @Mid = '" + Mid + "', @PcMac = '" + conn.PCNAME() + "'";
            result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return result;
    }

}