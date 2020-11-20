using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClsAVSReconcilation
/// </summary>
public class ClsAVSReconcilation
{
    DbConnection conn = new DbConnection();
    SqlCommand cmd;


    public ClsAVSReconcilation()
    {

    }

    public DataTable getTable(string FLAG,string MID,string BRCD="",string PRCD="", string FDATE="",string TDATE="")
    {
        DataTable dt = new DataTable();
        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = "SP_AVSR_ECO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);
            cmd.Parameters.AddWithValue("@BRCD", BRCD);
            cmd.Parameters.AddWithValue("@PRCD", PRCD);
            cmd.Parameters.AddWithValue("@FDATE", (FDATE == "" ? "" : conn.ConvertDate(FDATE)));
            cmd.Parameters.AddWithValue("@TDATE", (TDATE == "" ? "" : conn.ConvertDate(TDATE)));
            cmd.Parameters.AddWithValue("@MID", MID);
            dt = conn.GetData(cmd);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return dt;
    }

    public string getResult(string FLAG, string MID, string BRCD = "", string PRCD = "", string FDATE = "", string TDATE = "")
    {
        string ans = "";
        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = "SP_AVSR_ECO";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);
            cmd.Parameters.AddWithValue("@BRCD", BRCD);
            cmd.Parameters.AddWithValue("@PRCD", PRCD);
            cmd.Parameters.AddWithValue("@FDATE", (FDATE == "" ? "" : conn.ConvertDate(FDATE)));
            cmd.Parameters.AddWithValue("@TDATE", (TDATE == "" ? "" : conn.ConvertDate(TDATE)));
            cmd.Parameters.AddWithValue("@MID", MID);
            ans = (string)conn.sExecuteScalarNew(cmd);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return ans;
    }



}