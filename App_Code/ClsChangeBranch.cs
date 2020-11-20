using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data.SqlClient;

public class ClsChangeBranch
{
    DbConnection conn = new DbConnection();
    string sql = "",MBr = "";
    int Result = 0, i = 0;
    DataTable DT = new DataTable();

	public ClsChangeBranch()
	{

	}

    // Added By Amol B ON 2011-01-02 For Check Multi Branch Access or not
    public string CheckMultiBrAccess(string LoginId, string BRCD)
    {
        try
        {
            sql = "SELECT MULTIBRANCH FROM Usermaster WHERE LOGINCODE='" + LoginId + "' AND BRCD='" + BRCD + "'";
            MBr = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return MBr;
    }

    // Added By Amol B ON 2011-01-02 For Change Branch
    public int UpdateBranch(string UId, string Pass, string OC, string BRCDNEW, string BRCDOLD)
    {
        try
        {
            sql = "UPDATE Usermaster SET userstatus='" + OC + "', BRCD = '" + BRCDNEW + "' WHERE LOGINCODE='" + UId + "' AND password='" + Pass + "' AND BRCD='" + BRCDOLD + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
}