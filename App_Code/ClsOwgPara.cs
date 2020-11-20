using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data.SqlClient;

public class ClsOwgPara
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0;
    public ClsOwgPara()
    {

    }
    public int Insertpara(string FL, string POID, string P_EDATE, string P_BRCD, string P_OWGT, string P_ENTRYDT, string P_FUNDDT, string P_STS, string P_CLGGLNO, string P_CLGNAME, string P_RTGLNO, string P_RTGLNAME,string MID,string OUTIN)
    {
        try
        {
        //sql = "EXEC PACK_PARAMETER.INSERT_UPDATE_OWG('" + FL + "','" + POID + "','" + P_EDATE + "','" + P_BRCD + "','" + P_OWGT + "','" + P_ENTRYDT + "','" + P_FUNDDT + "','" + P_STS + "','" + P_CLGGLNO + "','" + P_CLGNAME + "','" + P_RTGLNO + "','" + P_RTGLNAME + "')";
        //SqlCommand cmd = new SqlCommand(sql, conn.GetDBConnection());

        ////Result = conn.sExecuteQuery(sql);
        //cmd.ExecuteScalar();
        //return Result;
        //SqlCommand cmd = new SqlCommand("PACK_PARAMETER.INSERT_UPDATE_OWG", conn.GetDBConnection());      
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.BindByName = true;
        //cmd.Parameters.Add("PFLAG", OracleDbType.Varchar2).Value = FL;
        //cmd.Parameters.Add("P_OID", OracleDbType.Decimal).Value = POID;
        //cmd.Parameters.Add("P_EDATE", OracleDbType.Date).Value = Convert.ToDateTime(P_EDATE).ToString("MM/dd/yyyy");
        //cmd.Parameters.Add("P_BRCD", OracleDbType.Decimal).Value = P_BRCD;
        //cmd.Parameters.Add("P_OWGT", OracleDbType.Varchar2).Value = P_OWGT;
        //cmd.Parameters.Add("P_ENTRYDT", OracleDbType.Decimal).Value = P_ENTRYDT;
        //cmd.Parameters.Add("P_FUNDDT", OracleDbType.Decimal).Value = P_FUNDDT;
        //cmd.Parameters.Add("P_STS", OracleDbType.Decimal).Value = P_STS;
        //cmd.Parameters.Add("P_CLGGLNO", OracleDbType.Decimal).Value = P_CLGGLNO;
        //cmd.Parameters.Add("P_CLGNAME", OracleDbType.Varchar2).Value = P_CLGNAME;
        //cmd.Parameters.Add("P_RTGLNO", OracleDbType.Decimal).Value = P_RTGLNO;
        //cmd.Parameters.Add("P_RTGLNAME", OracleDbType.Varchar2).Value = P_RTGLNAME;
        //Result = cmd.ExecuteNonQuery();
            string TBNAME = "";
            if (OUTIN == "OUT")
            { 
                TBNAME="OWG_PARAMETER";
            }
            else if(OUTIN=="IN")
            {
                TBNAME="INWORD_PARAMETER";
            }
            sql = "INSERT INTO "+TBNAME+"(EFFECT_DATE,BRCD,OWG_TYPE,ENTRYDATE,FUNDINGDATE,STATUS,CLG_GL_NO,CLG_GL_NAME,RETURN_GL_NO,RETURN_GL_NAME,STAGE,MID,SYS_DATE)" +
                "VALUES('" + conn.ConvertDate(P_EDATE) + "','" + P_BRCD + "','" + P_OWGT + "','" + P_ENTRYDT + "','" + P_FUNDDT + "','"+P_STS+"','"+P_CLGGLNO+"','"+P_CLGNAME+"','"+P_RTGLNO+"','"+P_RTGLNAME+"',1001,'"+MID+"',getdate())";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }
    public int DeletePara(string P_OID)
    {
        try
        {
        ////SqlCommand cmd = new SqlCommand("PACK_PARAMETER.DELETE_OWG", conn.GetDBConnection());
        ////cmd.CommandType = CommandType.StoredProcedure;
        ////cmd.BindByName = true;     
        ////cmd.Parameters.Add("P_OID", OracleDbType.Decimal).Value = P_OID;       
        ////Result = cmd.ExecuteNonQuery();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }
    public void BindGrid(GridView GView, string BRCD)
    {
        try 
        {
        sql = "SELECT * FROM TABLE (PACK_PARAMETER.GETPARA('ALLPARA','0','" + BRCD + "'))";
        Result = conn.sBindGrid(GView, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
    }
    public DataTable GetInfo(string POID, string BRCD)
    {
        DataTable DT = new DataTable();
        try 
        {
        sql = "SELECT * FROM TABLE (PACK_PARAMETER.GETPARA('SPEPARA','" + POID + "','" + BRCD + "'))";
        DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return DT;
    }
    public string GetGLName(string Subgl, string BRCD)
    {
        try 
        {
        //sql = "select glname from glmast where subglcode='" + Subgl + "' and brcd='" + BRCD + "' and glcode='"+GLCD+"'";
            sql = "select glname from glmast where subglcode='" + Subgl + "' and brcd='" + BRCD + "'";
        Subgl = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Subgl;
    }

}