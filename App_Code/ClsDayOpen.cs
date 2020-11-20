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

public class ClsDayOpen : IAdminAcces
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result;
    DataTable DT = new DataTable();

	public ClsDayOpen()
	{

	}

    public string checkAdmin(string BRCD, string LoginCode, string Mid)
    {
        string setnumb = "";
        try
        {
            sql = "Select LoginCode From UserMaster Where BRCD = '" + BRCD + "' And PermissionNo = '" + Mid.ToString() + "' And UserGroup In (1, 2) And LoginCode = '" + LoginCode + "' AND UserStatus = 1";
            setnumb = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return setnumb;
    }

    public int CheckLoginUser(GridView Gview, string BrCode, string Mid)
    {
        try
        {
            sql = "Select U.BrCd, U.LoginCode, U.UserName, G.GroupDesc, U.PermissionNo, 'BUL' As AFLAG, 'LOGIN USER' As EntryType From UserMaster U " +
                  "Inner Join UserGroup G With(NoLock) On G.GroupCode = U.UserGroup " +
                  "Where U.BrCd = '" + BrCode.ToString() + "' And U.PermissionNo <> '" + Mid.ToString() + "' And G.GroupCode <> '1' And U.UserStatus = 1";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return Result;
        }
        return Result;
    }

    public int CheckDayStatus(string BrCode, string EDate)
    {
        try
        {
            sql = "Select Status From AVS1025 Where Brcd = '" + BrCode + "' And DayBeginDate = '" + conn.ConvertDate(EDate) + "' And Status <> 99";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DayOpnLstProc(string BRCD, string EDate, string Mid, string LoginCode)
    {
        string cnt = "";
        try
        {
            sql = "Exec dbo.SP_DayOpActivity @BrCode = '" + BRCD + "', @NextDay = '" + conn.ConvertDate(EDate).ToString() + "', @Mid = '" + Mid + "', @PcName = '" + conn.PCNAME() + "', @LoginCode = '" + LoginCode + "'";
            cnt = conn.sExecuteQuery(sql).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToInt32(cnt);
    }
    public string AdminAuthorityYN(string BRCD)
    {
        string queryResult = "";
        try
        {
            sql = "Select isnull(LISTVALUE,'N') from parameter where LISTFIELD='AdminAccessYN' and BRCD=" + BRCD;
            queryResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            queryResult = "N";
            ExceptionLogging.SendErrorToText(Ex);
        }
        return queryResult == null ? "N" : queryResult;
    }


    public string GetPara_DAYOPEN()
    {
        try
        {
            sql = "SELECT LISTVALUE FROM PARAMETER WHERE LISTFIELD='DAYOPENYN'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public int CheckAdminAccess(string BRCD, string userGroup)
    {
        int allowAccess = 0;
        try
        {
            if (AdminAuthorityYN(BRCD: Convert.ToString(BRCD)).ToUpper().Equals("Y"))
            {
                if (!(Convert.ToString(userGroup).Equals("1") || Convert.ToString(userGroup).Equals("3")))
                {

                    allowAccess = 1;
                }
            }
            else
            {
                allowAccess = 0;
            }
        }
        catch (Exception ex)
        {
            allowAccess = 0;
            ExceptionLogging.SendErrorToText(ex);

        }
        return allowAccess;
    }
}