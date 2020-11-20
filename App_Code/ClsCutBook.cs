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

public class ClsCutBook
{
    DbConnection DBconn = new DbConnection();
    string sql = "";
    int Result;
    DataTable DT = new DataTable();
    public ClsCutBook()
    {
    }
    public DataTable CreateCutBook(string MID, string GLCD, string SUBGLCD, string BRCD, string EDT)
    {
        SqlCommand cmd = new SqlCommand();
        ClsOpenClose OC = new ClsOpenClose();
        double BALANCE = 0;       
        try
        {
            string[] TD = EDT.Split('/');

            try
            {
                sql = "Exec RptCutBookReport @AsonDate='" + DBconn.ConvertDate(EDT) + "',@BrCd='" + BRCD + "' ,@GlCode='" + GLCD + "',@SubGlCode='" + SUBGLCD + "',@MID='" + MID + "' ";
                DT = new DataTable();
                DT = DBconn.GetDatatable(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return DT;
            
        }       
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return DT;
    }
    public DataTable CreateCutBookSaving(string MID, string GLCD, string SUBGLCD, string BRCD, string EDT)
    {
        SqlCommand cmd = new SqlCommand();
        ClsOpenClose OC = new ClsOpenClose();
        double BALANCE = 0;
        try
        {
            string[] TD = EDT.Split('/');

            try
            {
                sql = "Exec RptCutBookReportSaving @AsonDate='" + DBconn.ConvertDate(EDT) + "',@BrCd='" + BRCD + "' ,@GlCode='" + GLCD + "',@SubGlCode='" + SUBGLCD + "',@MID='" + MID + "' ";
                DT = new DataTable();
                DT = DBconn.GetDatatable(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return DT;

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }

    public DataTable CreateCutBookRecSrno(string MID, string GLCD, string SUBGLCD, string BRCD, string EDT)
    {
        SqlCommand cmd = new SqlCommand();
        ClsOpenClose OC = new ClsOpenClose();
        try
        {
            string[] TD = EDT.Split('/');

            try
            {
                sql = "Exec RptCutBookReport_RecSrno @AsonDate='" + DBconn.ConvertDate(EDT) + "',@BrCd='" + BRCD + "' ,@GlCode='" + GLCD + "',@SubGlCode='" + SUBGLCD + "',@MID='" + MID + "' ";
                DT = new DataTable();
                DT = DBconn.GetDatatable(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return DT;

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    public DataTable CreateCutBook_Pal (string MID, string GLCD, string SUBGLCD, string BRCD, string EDT)
    {
        SqlCommand cmd = new SqlCommand();
        ClsOpenClose OC = new ClsOpenClose();
        double BALANCE = 0;
        try
        {
            string[] TD = EDT.Split('/');

            try
            {
                sql = "Exec RptCuteBook_Pal @AsonDate='" + DBconn.ConvertDate(EDT) + "',@BrCd='" + BRCD + "' ,@GlCode='" + GLCD + "',@SubGlCode='" + SUBGLCD + "',@MID='" + MID + "' ";
                DT = new DataTable();
                DT = DBconn.GetDatatable(sql);
            }
            catch (Exception Ex)
            {
                ExceptionLogging.SendErrorToText(Ex);
            }
            return DT;

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    public int InsertData(string MT, string ACCNO, string CUSTNO, string CUSTNM, double BALANCE, string MID)
    {
        string CR, DR;
        CR = DR = "";
        try 
        {
        if (BALANCE > 0)
        {
            CR = BALANCE.ToString();
            DR = "0";
        }
        else if (BALANCE < 0)
        {
            DR = BALANCE.ToString();
            CR = "0";
        }
        if (BALANCE != 0)
        {
            sql = "INSERT INTO " + MID + "_Cutbook (MEMTYPE,CUSTNO,ACCNO,MEMBERNAME,CR,DR) VALUES('" + MT + "','" + CUSTNO + "','" + ACCNO + "','" + CUSTNM.Replace('&', ' ') + "','" + CR + "','" + DR + "')";
            Result = DBconn.sExecuteQuery(sql);
        }
        else
        {
            Result = 0;
        }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return Result;
    }
    public void DropCutBook(string MID)
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
    }
    public DataTable GetAccountInfo(string GL, string Subgl, string BRCD, string EDT)
    {
        try 
        {
        string[] TD = EDT.Split('/');
        string TBNAME = "AVSB_" + TD[2].ToString() + TD[1].ToString();
        DT = new DataTable();
        sql = "SELECT DISTINCT A.ACCNO,M.CUSTNAME,M.CUSTNO FROM " + TBNAME + " A " +
              " LEFT JOIN AVS_ACC AC ON AC.ACCNO=A.ACCNO AND A.BRCD=AC.BRCD AND AC.GLCODE=A.GLCODE AND AC.SUBGLCODE=A.SUBGLCODE " +
              " LEFT JOIN  MASTER M ON M.CUSTNO=AC.CUSTNO AND M.BRCD=AC.BRCD WHERE A.SUBGLCODE='" + Subgl + "' AND A.GLCODE='" + GL + "' AND A.BRCD='" + BRCD + "' ORDER BY A.ACCNO";
        DT = DBconn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }   
        return DT;
    }
    public string GetGlcode(string brcd, string subglcode)
    {
        string Result = "";
        try
        {
            sql = "SELECT GLCODE FROM GLMAST WHERE BRCD='"+brcd+"' AND SUBGLCODE='"+subglcode+"'";
             Result = DBconn.sExecuteScalar(sql);
            
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
        
    }
}