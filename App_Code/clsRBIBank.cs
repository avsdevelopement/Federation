using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsRBIBank
/// </summary>
public class clsRBIBank
{

    DbConnection conn = new DbConnection();
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    DataTable dt = new DataTable();
    string sql;

    public string spRBI(string FLAG, string BANKCD = null, string BRANCHCD = null, string DESCR = null, string LMOYN = null, string MSYN = null, string DDYN = null,
                        string MTYN = null,
                        string TTYN = null, string DDLIMIT = null, string MTLIMIT = null, string TTLIMIT = null, string DDCOLLBRCD = null, string TTCOLLBRCD = null,
                        string DISTRICT = null,
                        string STATECD = null, string micrCode = null)
    {
        string ans = "";
        try
        {
            con.ConnectionString = conn.DbName().ToString();
            if (con.State == ConnectionState.Closed) { con.Open(); }
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SP_RBIBANK";
            cmd.Parameters.AddWithValue("@FLAG", FLAG);
            cmd.Parameters.AddWithValue("@BANKCD", BANKCD);
            cmd.Parameters.AddWithValue("@BRANCHCD", BRANCHCD);
            cmd.Parameters.AddWithValue("@DESCR", DESCR);
            cmd.Parameters.AddWithValue("@LMOYN", LMOYN);
            cmd.Parameters.AddWithValue("@MSYN", MSYN);
            cmd.Parameters.AddWithValue("@DDYN", DDYN);
            cmd.Parameters.AddWithValue("@MTYN", MTYN);
            cmd.Parameters.AddWithValue("@TTYN", TTYN);
            cmd.Parameters.AddWithValue("@DDLIMIT", DDLIMIT);
            cmd.Parameters.AddWithValue("@MTLIMIT", MTLIMIT);
            cmd.Parameters.AddWithValue("@TTLIMIT", TTLIMIT);
            cmd.Parameters.AddWithValue("@DDCOLLBRCD", DDCOLLBRCD);
            cmd.Parameters.AddWithValue("@TTCOLLBRCD", TTCOLLBRCD);
            cmd.Parameters.AddWithValue("@DISTRICT", DISTRICT);
            cmd.Parameters.AddWithValue("@STATECD", STATECD);
            cmd.Parameters.AddWithValue("@NMBRANCHCD", micrCode);
            //cmd.Parameters.AddWithValue("@ZONETIME", ZONETIME);
            //cmd.Parameters.AddWithValue("@WEEKOFF", WEEKOFF);
            //cmd.Parameters.AddWithValue("@NMBANKCD", NMBANKCD);
            //cmd.Parameters.AddWithValue("@Mid", getValue(Mid));
            //cmd.Parameters.AddWithValue("@Mid_Date", getValue(Mid_Date));
            //cmd.Parameters.AddWithValue("@Cid", getValue(Cid));
            //cmd.Parameters.AddWithValue("@Cid_Date", getValue(Cid_Date));
            //cmd.Parameters.AddWithValue("@Vid_Date", getValue(Vid_Date));
            //cmd.Parameters.AddWithValue("@Vid", getValue(Vid));
            cmd.CommandType = CommandType.StoredProcedure;
            ans = (string)cmd.ExecuteScalar();
            con.Close();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return ans;
    }


    public string getValue(string str)
    {
        str = str.ToString().Trim();
        if (str == "")
        {
            return null;
        }
        return str;
    }
    public string GetStateName(string BRCD, string STATECD)
    {

        try
        {
            sql = " SELECT (CONVERT (VARCHAR(10)),STATECD)+'-'FROM RBIBank WHERE BANKRBICD='" + BRCD + "' ";
            STATECD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);


        }
        return STATECD;
    }

    public string GetDISTName(string SCode, string DISTRICT)
    {

        try
        {
            sql = " SELECT (CONVERT (VARCHAR(10),DISTRICT)) FROM RBIBank WHERE STATECD='" + SCode + "' ";

            DISTRICT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);


        }
        return DISTRICT;
    }


    public string GetBankName(string BRCD, string DESCR)
    {
        try
        {
            sql = " SELECT (CONVERT (VARCHAR(100),DESCR))+'-'FROM RBIBank WHERE BANKRBICD='" + BRCD + "' ";
            DESCR = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DESCR;
    }
    public string GetBranchName(string BRCD, string BRANCHCD)
    {
        try
        {
            sql = " SELECT (CONVERT (VARCHAR(100),BRANCHCD))+'-'FROM RBIBank WHERE BANKRBICD='" + BRCD + "' ";
            BRANCHCD = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BRANCHCD;
    }



}