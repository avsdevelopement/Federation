using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;

public class ClsDayClosedReport
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "";
    int Result = 0;

    public ClsDayClosedReport()
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

    public int BrHandActivity(GridView Gview, string BRCD, string EntryDate, string LoginCode, string Mid)
    {
        try
        {
            sql = "Exec Sp_BrHandActivity @BRCD = '" + BRCD + "', @ENTRYDATE = '" + conn.ConvertDate(EntryDate) + "' , @EDT = '" + conn.ConvertDate(EntryDate) + "', @LoginCode = '" + LoginCode + "', @Mid = '" + Mid + "'";
            Result = conn.sBindGrid(Gview, sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DayCloseActivity(string BrCode, string EDate, string LoginCode, string Mid)
    {
        try
        {
            sql = "Exec Sp_DayClosedActivity @BrCode = '" + BrCode + "', @EDate = '" + conn.ConvertDate(EDate) + "', @LoginCode = '" + LoginCode + "', @Mid = '" + Mid + "', @PcMac = '" + conn.PCNAME() + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable DayActivity(string BRCD)
    {
        try
        {
            sql = "SELECT BRCD, NAME, REASON FROM DayClosedActivityError WHERE BRCD = '" + BRCD + "'";
            DT = new DataTable(sql);
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return DT = null;
        }
        return DT;
    }

    public DataTable SendSMS(string BRCD, string EDAT)
    {
        try
        {
            sql = "exec Isp_AVS0060 '"+BRCD+"','"+conn.ConvertDate(EDAT)+"'";
            DT = new DataTable(sql);
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }
    public int InsertSMS(string CustNo,string Mobile,string EntryDate,string BRCD,string Msg)
    {
        int result = 0;
        try
        {
            sql = "insert into avs1092 (CUSTNO,MOBILE,SMS_DATE,SMS_TYPE,SMS_DESCRIPTION,SMS_STATUS,BRCD,SYSTEMDATE) values ("+CustNo+",'"+Mobile+"','"+conn.ConvertDate(EntryDate)+"','C','"+Msg+"',1,'"+BRCD+"',convert(varchar(10),getdate(),121)) ";
            result = conn.sExecuteQuery(sql);

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }

    public DataTable GetDetails()
    {
        DataTable dt = new DataTable();
        try
        {
            sql = "Select distinct MOBILENO,CustNo from Director where SMSYN='Y'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
}