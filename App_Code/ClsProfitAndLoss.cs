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

public class ClsProfitAndLoss
{
    DbConnection DBconn = new DbConnection();
    string sql = "";
    int Result = 0;
    DataTable DT = new DataTable();

	public ClsProfitAndLoss()
	{

	}

    public DataTable ProfitAndLoss(string BRCD, string FDate, string MID)
    {
        try
        {
            string[] TD = FDate.Split('/');
            sql = "Exec SP_ProfitAndLoss @BRCD='" + BRCD + "',@PFMONTH='" + TD[1].ToString() + "',@PFYEAR='" + TD[2].ToString() + "',@FromDate='" + DBconn.ConvertDate(FDate) + "'";
            DT = new DataTable();
            DT = DBconn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    public DataTable ProfitAndLoss_marathi (string BRCD, string FDate, string MID)
    {
        try
        {
            string[] TD = FDate.Split('/');
            sql = "Exec SP_ProfitAndLoss_Marathi @BRCD='" + BRCD + "',@PFMONTH='" + TD[1].ToString() + "',@PFYEAR='" + TD[2].ToString() + "',@FromDate='" + DBconn.ConvertDate(FDate) + "'";
            DT = new DataTable();
            DT = DBconn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    
    public DataTable GetAdminExp(string BRCD, string FDate, string Tdate)
    {
        try
        {
            string[] TD = FDate.Split('/');
            sql = "Exec ISP_AVS0106 @BRCD='"+BRCD+"',@FromDate='"+DBconn.ConvertDate(FDate)+"',@ToDate='"+DBconn.ConvertDate(Tdate)+"'";
            DT = new DataTable();
            DT = DBconn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }

    public DataTable PrePLBalance(string BranchID, string FDate, string TDate, string MID)
    {
        try
        {
            string[] TD = TDate.Split('/');
            string[] TD1 = FDate.Split('/');
            // sql = "Exec SP_BALANCESNEW_201704 @PBRCD='" + BranchID + "',@PFMONTH='" + TD[1].ToString() + "',@PFYEAR='" + TD[2].ToString() + "',@PFDATE='" + DBconn.ConvertDate(TDate) + "',@PEMONTH='" + TD1[1].ToString() + "',@PEYEAR='" + TD1[2].ToString() + "',@PEDATE='" + DBconn.ConvertDate(FDate) + "'";            
            sql = "Exec RptPNProfitAndLoss @BRCD='" + BranchID + "',@PFMONTH='" + TD1[1].ToString() + "',@PFYEAR='" + TD1[2].ToString() + "',@FromDate='" + DBconn.ConvertDate(FDate) + "',@PLMONTH='" + TD[1].ToString() + "',@PLYEAR='" + TD[2].ToString() + "',@ToDate='" + DBconn.ConvertDate(TDate) + "'";
            DT = new DataTable();
            DT = DBconn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    public DataTable IncomeExpPL (string BranchID, string FDate, string TDate, string MID) 
    {
        try
        {
            sql = "Exec RptIncomeExpReport @BRCD='" + BranchID + "',@PFDT='" + DBconn.ConvertDate(FDate) + "',@PTDT='" + DBconn.ConvertDate(TDate) + "'";
            DT = new DataTable();
            DT = DBconn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);

        }
        return DT;
    }
    
}