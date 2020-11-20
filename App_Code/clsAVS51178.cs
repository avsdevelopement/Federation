using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for clsAVS51178
/// </summary>
public class clsAVS51178
{
    DbConnection conn = new DbConnection();
    SqlCommand cmd;
    DataTable dt = new DataTable();
    DataSet ds = new DataSet();
    string sql = "";
    int sql1 = 0;
    int Result =0;
    string SResult = "";
    public clsAVS51178()
    {
    }

    public string getbrcode(string brcd)
    {
        sql = "select MIDNAME from bankname where brcd='" + brcd + "'";
        string Result = conn.sExecuteScalar(sql);
        return Result;
    }
    public string getBrcdName(string brcd)
    {

        sql = "SELECT MIDNAME+'_'+CONVERT(VARCHAR(10),brcd) FROM bankname WHERE brcd='" + brcd + "'  AND stage<>1004";
        brcd = conn.sExecuteScalar(sql);
        return brcd;
    }


    public void BindForIssue(string FLAG, string BRCD, GridView GrdAcc)
    {

        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = "SP_IssueMaster";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);
            //  cmd.Parameters.AddWithValue("@VENDORID", VENDORID);
            cmd.Parameters.AddWithValue("@FROMBRCD", BRCD);

            cmd.CommandType = CommandType.StoredProcedure;
            GrdAcc.DataSource = conn.GetData(cmd);
            GrdAcc.DataBind();

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public void BindForIssueSALES(string FLAG, string MEMNO, GridView GrdAcc, string ISSUENO)
    {

        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = "SP_IssueMaster";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);
            //  cmd.Parameters.AddWithValue("@VENDORID", VENDORID);
            cmd.Parameters.AddWithValue("@MEMNO", MEMNO);
            cmd.Parameters.AddWithValue("@ISSUENO", ISSUENO);

            cmd.CommandType = CommandType.StoredProcedure;
            GrdAcc.DataSource = conn.GetData(cmd);
            GrdAcc.DataBind();

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }


    public string IssueMasterATH(string FLAG, string ISSUENO = null, string FROMBRCD = null, string TOBRCD = null, string CGSTPER = null, string PRODID = null, string VENDORID = null, string SRNO = null, string QTY = null, string UNITCOST = null, string SGSTPER = null, string CGSTAMT = null, string SGSTAMT = null, string AMOUNT = null, string SubGlCode = null, string Accno = null, string Particulars = null, string InstNo = null, string InstDate = null, string PmtMode = null, string ENTRYDATE = null, string MID = null, string MEMNO = null, string NONMEMNO = null, string MOBILE = null, string EMAIL = null, string GSTNO = null, string TotalAmount=null)
    {
        string res = "";

        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = "SP_IssueMaster";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);
            cmd.Parameters.AddWithValue("@ISSUENO", ISSUENO);
            cmd.Parameters.AddWithValue("@VENDORID", VENDORID);
            cmd.Parameters.AddWithValue("@PRODID", PRODID);
            cmd.Parameters.AddWithValue("@FROMBRCD", FROMBRCD);
            cmd.Parameters.AddWithValue("@TOBRCD", TOBRCD);
            cmd.Parameters.AddWithValue("@SRNO", SRNO);
            cmd.Parameters.AddWithValue("@QTY", QTY);
            cmd.Parameters.AddWithValue("@UNITCOST", UNITCOST);
            cmd.Parameters.AddWithValue("@SGSTPER", SGSTPER);
            cmd.Parameters.AddWithValue("@CGSTPER", SGSTPER);
            cmd.Parameters.AddWithValue("@SGSTAMT", SGSTAMT);
            cmd.Parameters.AddWithValue("@CGSTAMT", CGSTAMT);
            cmd.Parameters.AddWithValue("@AMOUNT", AMOUNT);
            cmd.Parameters.AddWithValue("@MEMNO", MEMNO);
            cmd.Parameters.AddWithValue("@NONMEMNO", NONMEMNO);
            cmd.Parameters.AddWithValue("@MOBILE", MOBILE);
            cmd.Parameters.AddWithValue("@EMAIL", EMAIL);

            cmd.Parameters.AddWithValue("@SubGlCode", SubGlCode);
            cmd.Parameters.AddWithValue("@Accno", Accno);
            cmd.Parameters.AddWithValue("@Particulars", Particulars);
            cmd.Parameters.AddWithValue("@InstNo ", InstNo);
            cmd.Parameters.AddWithValue("@InstDate", string.IsNullOrEmpty(InstDate) ? (DateTime?)null : Convert.ToDateTime(InstDate));// Convert.ToDateTime((InstDate).ToString() == "" ? "0" : (InstDate).ToString()).ToString()); //Convert.ToDateTime(InstDate).ToString("dd-MMM-yyyy"));
            cmd.Parameters.AddWithValue("@PMTMode", PmtMode);
            cmd.Parameters.AddWithValue("@ENTRYDATE", Convert.ToDateTime(ENTRYDATE).ToString("dd-MMM-yyyy"));
            cmd.Parameters.AddWithValue("@MID", MID);
            cmd.Parameters.AddWithValue("@GSTNO", GSTNO);
            cmd.Parameters.AddWithValue("@TotalAmount", TotalAmount);
            res = (string)conn.sExecuteScalarNew(cmd);
        }

        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;

    }
    public string IssueMaster(string FLAG, string ISSUENO = null, string FROMBRCD = null, string TOBRCD = null, string CGSTPER = null, string PRODID = null, string VENDORID = null, string SRNO = null, string QTY = null, string UNITCOST = null, string SGSTPER = null, string CGSTAMT = null, string SGSTAMT = null, string AMOUNT = null,  string SubGlCode = null, string Accno = null, string Particulars = null, string InstNo = null, string InstDate = null, string PmtMode = null, string ENTRYDATE = null, string MID = null)
    {
        string res = "";

        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = "SP_IssueMaster";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);
            cmd.Parameters.AddWithValue("@ISSUENO", ISSUENO);
            cmd.Parameters.AddWithValue("@VENDORID", VENDORID);
            cmd.Parameters.AddWithValue("@PRODID", PRODID);
            cmd.Parameters.AddWithValue("@FROMBRCD", FROMBRCD);
            cmd.Parameters.AddWithValue("@TOBRCD", TOBRCD);
            cmd.Parameters.AddWithValue("@SRNO", SRNO);
            cmd.Parameters.AddWithValue("@QTY", QTY);
            cmd.Parameters.AddWithValue("@UNITCOST", UNITCOST);
            cmd.Parameters.AddWithValue("@SGSTPER", SGSTPER);
            cmd.Parameters.AddWithValue("@CGSTPER", SGSTPER);
            cmd.Parameters.AddWithValue("@SGSTAMT", SGSTAMT);
            cmd.Parameters.AddWithValue("@CGSTAMT", CGSTAMT);
            cmd.Parameters.AddWithValue("@AMOUNT", AMOUNT);
           
           
            cmd.Parameters.AddWithValue("@SubGlCode"  ,SubGlCode);
            cmd.Parameters.AddWithValue("@Accno"  ,Accno);
            cmd.Parameters.AddWithValue("@Particulars" ,Particulars);
            cmd.Parameters.AddWithValue("@InstNo ",InstNo);
            cmd.Parameters.AddWithValue("@InstDate  ", InstDate);
            cmd.Parameters.AddWithValue("@PMTMode", PmtMode);
            cmd.Parameters.AddWithValue("@ENTRYDATE", Convert.ToDateTime(ENTRYDATE).ToString("dd-MMM-yyyy"));
            cmd.Parameters.AddWithValue("@MID", MID);
            res = (string)conn.sExecuteScalarNew(cmd);
        }

        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;

    }


    public int  CheckMid(string Id)
    {
        
        try
        {
            sql = "SELECT MID FROM IssueMaster WHERE ISSUENO = '" + Id + "'";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //return Result = 0";
        }
        return Result;
    }
    public string CheckStage(string Id)
    {
        try
        {
            sql = "SELECT STAGE FROM IssueMaster WHERE ISSUENO = '" + Id + "' ";
            SResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return SResult;
    }
    public string GetCashGl(string GL, string BRCD)
    {
        sql = "Select Subglcode from glmast where glcode='" + GL + "' and brcd='" + BRCD + "'";
        BRCD = conn.sExecuteScalar(sql);
        return BRCD;
    }

    //public DataTable GetProdCode(string BrCode)
    //{
    //    DataTable DT = new DataTable();
    //    try
    //    {
    //        sql = "Select * From IssueMaster Where TOBRCD = '" + BrCode + "'  And EntryDate = (Select Max(EntryDate) From IssueMaster Where TOBRCD = '" + BrCode + "' And Stage <> 1004)";
    //        DT = conn.GetDatatable(sql);
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //        return null;
    //    }
    //    return DT;
    //}
    public DataTable GetProdCode(string BrCode, string ISSUENO)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select * From IssueMaster Where TOBRCD = '" + BrCode + "' and ISSUENO = '" + ISSUENO + "'  And Stage <> 1004";//And EntryDate = (Select Max(EntryDate) From IssueMaster Where TOBRCD = '" + BrCode + "'
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }


    public string USEDMASTER(string FLAG, string USEID = null, string FROMBRCD = null, string CGST = null, string PRODID = null, string VENDORID = null, string SRNO = null, string QTY = null, string UNITCOST = null, string SGST = null, string CGSTAMT = null, string SGSTAMT = null, string MID = null, string AMOUNT = null, string ENTRYDATE = null)
    {
        string res = "";

        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = "SP_USEDMASTER";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);
            cmd.Parameters.AddWithValue("@USEDNO", USEID);
            cmd.Parameters.AddWithValue("@VENDORID", VENDORID);
            cmd.Parameters.AddWithValue("@PRODID", PRODID);
            cmd.Parameters.AddWithValue("@BRCD", FROMBRCD);

            cmd.Parameters.AddWithValue("@SRNO", SRNO);
            cmd.Parameters.AddWithValue("@QTY", QTY);
            cmd.Parameters.AddWithValue("@UNITCOST", UNITCOST);
            cmd.Parameters.AddWithValue("@SGSTPER", SGST);
            cmd.Parameters.AddWithValue("@CGSTPER", SGST);
            cmd.Parameters.AddWithValue("@SGSTAMT", SGSTAMT);
            cmd.Parameters.AddWithValue("@CGSTAMT", CGSTAMT);
            cmd.Parameters.AddWithValue("@AMOUNT", AMOUNT);
            cmd.Parameters.AddWithValue("@ENTRYDATE", Convert.ToDateTime(ENTRYDATE).ToString("dd-MMM-yyyy"));
            cmd.Parameters.AddWithValue("@MID", MID);
            res = (string)conn.sExecuteScalarNew(cmd);
        }

        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return res;

    }

    public void BindForUse(string FLAG, string BRCD, GridView GrdAcc)
    {

        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = "SP_USEDMASTER";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FLAG", FLAG);
            //  cmd.Parameters.AddWithValue("@VENDORID", VENDORID);
            cmd.Parameters.AddWithValue("@BRCD", BRCD);

            cmd.CommandType = CommandType.StoredProcedure;
            GrdAcc.DataSource = conn.GetData(cmd);
            GrdAcc.DataBind();

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    public DataTable ShowIssue(string FLAG, string IssueID = null)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "SP_IssueMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FLAG", FLAG);

        cmd.Parameters.AddWithValue("@ISSUENO", IssueID);

        return conn.GetData(cmd);



    }
    public DataTable ShowUsed(string FLAG, string UseId = null)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "SP_USEDMASTER";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@FLAG", FLAG);

        cmd.Parameters.AddWithValue("@USEDNO", UseId);

        return conn.GetData(cmd);



    }
    public double GetOpenClose(string Fyear, string FMonth, string PT, string ACC, string BRCD, string EDT, string GL)
    {
        double BAL = 0;
        try
        {
            sql = "Exec SP_OPENCLOSE @P_FLAG='CLOSING',@P_FYEAR='" + Fyear + "',@P_FMONTH='" + FMonth + "',@p_job='" + PT + "',@p_job1='" + ACC + "',@p_job2='" + BRCD + "',@p_job3='" + conn.ConvertDate(EDT) + "',@p_job4='" + GL + "'";
            BAL = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BAL;
    }
    public string Getaccno(string AT, string BRCD, string GLCD)
    {
        try
        {
            sql = " SELECT (CONVERT(VARCHAR(10),MAX(LASTNO)+1))+'-'+(CONVERT (VARCHAR(10),GLCODE))+'-'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }
}