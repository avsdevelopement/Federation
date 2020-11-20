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

public class ClsTrailBalance
{
    string sql = "";
    int Result = 0;
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    public ClsTrailBalance()
    {
    }
    public DataTable GetInfo(string FDate, string TDate, string BRCD, string ASYN, string CNYN)
    {
        try
        {
            string[] FDT = FDate.Split('/');
            string[] TDT = TDate.Split('/');
            sql = "Exec SP_TRAILBALANCE @PFMONTH='"+TDT[1].ToString()+"',@PFYEAR='"+TDT[2].ToString()+"',@PFDT='"+conn.ConvertDate(TDate)+"',@PBRCD='"+BRCD+"',@CNYN='"+CNYN+"'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
            return DT;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    public DataTable GetInfo_FT(string FDate, string TDate, string BRCD, string FLT)
    {
        try
        {
            string[] FDT = FDate.Split('/');
            string[] TDT = TDate.Split('/');
            sql = "Exec SP_TRAILBALANCE_1 @PFDT='" + conn.ConvertDate(FDate) + "',@PTDT='" + conn.ConvertDate(TDate) + "',@PBRCD='" + BRCD + "',@CNYN='" + FLT + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
            return DT;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    public DataTable GetInfo_FTDPLN(string FDate, string TDate, string BRCD)
    {
        try
        {
            string[] FDT = FDate.Split('/');
            string[] TDT = TDate.Split('/');
            sql = "Exec SP_TRAILBALANCE_DPLN @PFDT='" + conn.ConvertDate(FDate) + "',@PTDT='" + conn.ConvertDate(TDate) + "',@PBRCD='" + BRCD + "' ";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
            return DT;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }

}