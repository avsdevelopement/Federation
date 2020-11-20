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

public class ClsBalanceSheet
{
    DbConnection DBconn = new DbConnection();
    string sql = "";
    int Result = 0;
    DataTable DT = new DataTable();
	public ClsBalanceSheet()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable Balance(string FL, string BranchID, string FDate, string MID) //Remove string FLAG Rakesh 09-12-2016
    {
        try
        {
            string[] TD = FDate.Split('/');
            sql = "Exec SP_BALANCESNEW @FLAG='"+ FL  +"' , @PBRCD='" + BranchID + "',@PFMONTH='" + TD[1].ToString() + "',@PFYEAR='" + TD[2].ToString() + "',@PFDATE='" + DBconn.ConvertDate(FDate) + "'";            
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
    public DataTable Balance_Marathi (string FL, string BranchID, string FDate, string MID) //Remove string FLAG Rakesh 09-12-2016
    {
        try
        {
            string[] TD = FDate.Split('/');
            sql = "Exec SP_BALANCES_Marathi @FLAG='"+ FL  +"' , @PBRCD='" + BranchID + "',@PFMONTH='" + TD[1].ToString() + "',@PFYEAR='" + TD[2].ToString() + "',@PFDATE='" + DBconn.ConvertDate(FDate) + "'";            
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

    public DataTable PreBalance(string FL, string BranchID, string FDate, string TDate, string MID) //Amruta 20170427
    {
        try
        {
            string[] TD = TDate.Split('/');
            string[] TD1 = FDate.Split('/');
            sql = "Exec SP_NFormBS @FLAG='" + FL + "' ,@PBRCD='" + BranchID + "',@PFMONTH='" + TD[1].ToString() + "',@PFYEAR='" + TD[2].ToString() + "',@PFDATE='" + DBconn.ConvertDate(TDate) + "',@PEMONTH='" + TD1[1].ToString() + "',@PEYEAR='" + TD1[2].ToString() + "',@PEDATE='" + DBconn.ConvertDate(FDate) + "'";            
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
    public string GetStatus(string Brcd, string Edate)
    {
        sql = "Select Status From Avs1025 Where DayBeginDate='" + DBconn.ConvertDate(Edate) + "' And Brcd='" + Brcd + "' And status <> 99 ";
        string result = DBconn.sExecuteScalar(sql);
        return result;
    }

}