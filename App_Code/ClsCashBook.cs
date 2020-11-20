using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Configuration;
using System.Data.SqlClient;

public class ClsCashBook
{
    string sql = "";
    int Result;
    DbConnection connDB = new DbConnection();
    DataTable DT = new DataTable();
    public ClsCashBook()
    {
    }
    public int GetCashBook(GridView Gview, string BRCD, string FDate, string TDate)
    {

        ////SqlConnection conn = new SqlConnection();
        ////SqlCommand cmd = new SqlCommand();
        ////try
        ////{

        ////try
        ////{
        ////    //sql = "CREATE TABLE TCASHBOOK (ID NUMBER NOT NULL,CSUBGL NUMBER NULL,CAACNO NUMBER NULL,CACCNAME VARCHAR2(50) NULL,CAMOUNT NUMBER(18,2) NULL,DSUBGL NUMBER NULL,DAACNO NUMBER NULL,DACCNAME VARCHAR2(50) NULL,DAMOUNT NUMBER(18,2) NULL)";
        ////    sql = "CREATE TABLE TCASHBOOK (ID INT NOT NULL,CSUBGL INT NULL,CAACNO INT NULL,CACCNAME VARCHAR(50) NULL,CAMOUNT MONEY NULL,DSUBGL INT NULL,DAACNO INT NULL,DACCNAME VARCHAR(50) NULL,DAMOUNT MONEY NULL)";
        ////    conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ocbs"].ToString());
        ////    if (conn.State == ConnectionState.Closed)
        ////    {
        ////        conn.Open();
        ////    }
        ////    cmd = new SqlCommand(sql, conn);
        ////    Result = cmd.ExecuteNonQuery();

        ////    sql = "create sequence TCASHBOOK_SEQ";
        ////    conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ocbs"].ToString());
        ////    if (conn.State == ConnectionState.Closed)
        ////    {
        ////        conn.Open();
        ////    }
        ////    cmd = new SqlCommand(sql, conn);
        ////    Result = cmd.ExecuteNonQuery();

        ////    sql = "CREATE OR REPLACE TRIGGER TCASHBOOK_TRG BEFORE INSERT ON TCASHBOOK     FOR EACH ROW  WHEN (new.ID IS NULL)    BEGIN  SELECT TCASHBOOK_SEQ.NEXTVAL  INTO   :new.ID  FROM   dual; END;";
        ////    //sql = "create or replace TRIGGER TCASHBOOK_TRG before insert on tcashbook for each row begin   BEGIN    IF INSERTING AND :NEW.ID IS NULL THEN      SELECT TCASHBOOK_SEQ.NEXTVAL INTO :NEW.ID FROM SYS.DUAL  END IF END COLUMN_SEQUENCES end";
        ////    conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ocbs"].ToString());
        ////    if (conn.State == ConnectionState.Closed)
        ////    {
        ////        conn.Open();
        ////    }
        ////    cmd = new SqlCommand(sql, conn);
        ////    Result = cmd.ExecuteNonQuery();

        ////    //sql = "CREATE TABLE TDEBIT (ID NUMBER NOT NULL,DSUBGL NUMBER NULL,DAACNO NUMBER NULL,DACCNAME VARCHAR2(50) NULL,DAMOUNT NUMBER(18,2) NULL)";
        ////    sql = "CREATE TABLE TDEBIT (ID NUMBER NOT NULL,DSUBGL NUMBER NULL,DAACNO NUMBER NULL,DACCNAME VARCHAR2(50) NULL,DAMOUNT NUMBER(18,2) NULL)";
        ////    conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ocbs"].ToString());
        ////    if (conn.State == ConnectionState.Closed)
        ////    {
        ////        conn.Open();
        ////    }
        ////    cmd = new SqlCommand(sql, conn);
        ////    Result = cmd.ExecuteNonQuery();

        ////    sql = "create sequence TDEBIT_SEQ";
        ////    conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ocbs"].ToString());
        ////    if (conn.State == ConnectionState.Closed)
        ////    {
        ////        conn.Open();
        ////    }
        ////    cmd = new SqlCommand(sql, conn);
        ////    Result = cmd.ExecuteNonQuery();

        ////    sql = "CREATE OR REPLACE TRIGGER TDEBIT_TRG BEFORE INSERT ON TDEBIT FOR EACH ROW  WHEN (new.ID IS NULL)    BEGIN  SELECT TDEBIT_SEQ.NEXTVAL  INTO   :new.ID  FROM   dual; END;";
        ////    // sql = "create or replace TRIGGER TDEBIT_TRG before insert on TDEBIT for each row begin   BEGIN    IF INSERTING AND :NEW.ID IS NULL THEN      SELECT TDEBIT_SEQ.NEXTVAL INTO :NEW.ID FROM SYS.DUAL  END IF END COLUMN_SEQUENCES end";
        ////    conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ocbs"].ToString());
        ////    if (conn.State == ConnectionState.Closed)
        ////    {
        ////        conn.Open();
        ////    }
        ////    cmd = new SqlCommand(sql, conn);
        ////    Result = cmd.ExecuteNonQuery();


        ////    string[] FDT = FDate.Split('/');
        ////    string[] TDT = TDate.Split('/');


        ////    cmd = new SqlCommand("SP_CASHBOOK", conn);
        ////    cmd.CommandType = CommandType.StoredProcedure;
        ////    //cmd.BindByName = true;
        ////    //cmd.Parameters.Add("P_FMONTH", OracleDbType.Varchar2).Value = FDT[1].ToString();
        ////    //cmd.Parameters.Add("P_TMONTH", OracleDbType.Varchar2).Value = TDT[1].ToString();
        ////    //cmd.Parameters.Add("P_FYEAR", OracleDbType.Varchar2).Value = FDT[0].ToString();
        ////    //cmd.Parameters.Add("P_TYEAR", OracleDbType.Varchar2).Value = TDT[0].ToString();
        ////    //cmd.Parameters.Add("P_FDT", OracleDbType.Varchar2).Value = FDate;
        ////    //cmd.Parameters.Add("P_TDT", OracleDbType.Varchar2).Value = TDate;
        ////    //Result = cmd.ExecuteNonQuery();

        ////    sql = "SELECT * FROM TCASHBOOK ORDER BY ID";
        ////    Result = connDB.sBindGrid(Gview, sql);
        //}
        //catch (Exception Ex)
        //{
        //    //return -1;
        //}
        ////finally
        //{
        //    sql = "drop table TCASHBOOK";
        //    conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ocbs"].ToString());
        //    if (conn.State == ConnectionState.Closed)
        //    {
        //        conn.Open();
        //    }
        //    cmd = new SqlCommand(sql, conn);
        //    Result = cmd.ExecuteNonQuery();

        //    sql = "drop sequence TCASHBOOK_SEQ";
        //    conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ocbs"].ToString());
        //    if (conn.State == ConnectionState.Closed)
        //    {
        //        conn.Open();
        //    }
        //    cmd = new SqlCommand(sql, conn);
        //    Result = cmd.ExecuteNonQuery();

        //    sql = "drop table TDEBIT";
        //    conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ocbs"].ToString());
        //    if (conn.State == ConnectionState.Closed)
        //    {
        //        conn.Open();
        //    }
        //    cmd = new SqlCommand(sql, conn);
        //    Result = cmd.ExecuteNonQuery();

        //    sql = "drop sequence TDEBIT_SEQ";
        //    conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ocbs"].ToString());
        //    if (conn.State == ConnectionState.Closed)
        //    {
        //        conn.Open();
        //    }
        //    cmd = new SqlCommand(sql, conn);
        //    Result = cmd.ExecuteNonQuery();
        //}
        try
        {
            sql = "EXEC SP_CASHBOOK '" + connDB.ConvertDate(FDate) + "','" + connDB.ConvertDate(TDate) + "','" + BRCD + "'";
            Result = connDB.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);

        }
        return Result;
    }
    public DataTable CreateCB(string FD, string TD, string BRCD)
    {
        try
        {
            sql = "EXEC SP_CASHBOOK @P_FDT='" + connDB.ConvertDate(FD) + "',@P_TDT='" + connDB.ConvertDate(TD) + "',@BRCD = '" + BRCD + "' ";
            DT = connDB.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable CreateCB_ALL (string FD, string TD, string BRCD)
    {
        try
        {
            sql = "EXEC RptCashBook_ALLDetails @P_FDT='" + connDB.ConvertDate(FD) + "',@P_TDT='" + connDB.ConvertDate(TD) + "',@BRCD = '" + BRCD + "' ";
            DT = connDB.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    
    public DataTable CreateCBSumry(string FD, string TD, string BRCD)
    {
        try
        {
            sql = "EXEC RptcashBookSummary @P_FDT='" + connDB.ConvertDate(FD) + "',@P_TDT='" + connDB.ConvertDate(TD) + "',@BRCD = '" + BRCD + "' ";
            DT = connDB.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}