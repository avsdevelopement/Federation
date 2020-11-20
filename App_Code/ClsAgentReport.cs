using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
public class ClsAgentReport
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result;
    DataTable DT = new DataTable();
    public ClsAgentReport()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetInfo1(string FL,string FDT, string TDT, string AGN, string BRCD)
    { 
        try 
        {

            sql = "EXEC SP_AGENTREPORT '"+FL+"','"+conn.ConvertDate(FDT)+"','"+conn.ConvertDate(TDT)+"','"+AGN+"','"+BRCD+"'";
           // sql = "select * from avsm_201607 where subglcode='" + AGN + "' and entrydate between '" + FDT + "' and '" + TDT + "' and BRCD='" + BRCD + "'";
        DT = new DataTable();
        DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            //HttpContext.Current.//Response.Redirect("FrmLogin.aspx", true);
        }
        return DT;
    }
    public DataTable getAgentSlab(string FDT,string AGN, string BRCD)
    {
        try
        {

            sql = "EXEC Sp_agentDaily '"+BRCD+"','"+AGN+"','"+conn.ConvertDate(FDT)+"'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
           
        }
        return DT;
    }
}