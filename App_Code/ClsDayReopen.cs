using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsDayReopen
{
    string sql = "";
    int Result = 0;
    string OutPut = "";
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();

	public ClsDayReopen()
	{
		
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

    public int CheckLoginUser(GridView Gview, string BrCode)
    {
        try
        {
            sql = "Select LOGINCODE, USERNAME, UG.GROUPDESC, 'LOGIN USER','BUL' As AFLAG From UserMaster UM INNER JOIN USERGROUP UG With(NoLock) On UG.GROUPCODE = UM.USERGROUP Where BRCD = '" + BrCode + "' And UG.GroupCode <> 1 And UM.UserStatus = 1";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return Result;
        }
        return Result;
    }

    public string ReOpenDay(string BrCode, string EDate, string LoginCode, string Mid, string Flag)
    {
        string RS = "";
        try
        {
            EDate = conn.AddMonthDay(EDate.ToString(), "-1", "D");

            sql = "Exec SP_DayReOpenActivity @BranchCode = '" + BrCode + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @LoginCode = '" + LoginCode + "', @Mid = '" + Mid + "', @PcName = '" + conn.PCNAME() + "', @Flag = '" + Flag + "'";
            RS = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);            
        }
        return RS;
    }

    public string OpenDay(string BrCode, string OpenDate, string Flag)
    {
        try
        {
            sql = "Exec SP_DayReOpenActivity @BranchCode = '" + BrCode + "', @EntryDate = '" + conn.ConvertDate(OpenDate).ToString() + "', @Flag = '" + Flag + "'";
            OutPut = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return OutPut = "";
        }
        return OutPut;
    }

    public int GetReOpenedDay(GridView Gview, string BrCode, string EDate, string Flag)
    {
        try
        {
            sql = "Exec SP_DayReOpenActivity @BranchCode = '" + BrCode + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = '" + Flag + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
}