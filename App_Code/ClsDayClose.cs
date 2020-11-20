using System;
using System.Data;

public class ClsDayClose : IAdminAcces
{
    DbConnection DBconn = new DbConnection();
    string sql = "";
    int Result;
    DataTable DT = new DataTable();

    public ClsDayClose()
    {


    }

    public int checkULogCnt(string BRCD)
    {
        string cnt = "";
        try
        {
            sql = "SELECT COUNT(LOGINCODE) FROM USERMASTER WHERE BRCD = '" + BRCD + "' AND USERGROUP <> 1 AND USERSTATUS = 1";
            cnt = DBconn.sExecuteScalar(sql);
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
            queryResult = DBconn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            queryResult = "N";
            ExceptionLogging.SendErrorToText(Ex);
        }
        return queryResult == null ? "N" : queryResult;
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