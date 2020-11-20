using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

/// <summary>
/// Summary description for clsbanklookup
/// </summary>
public class clsbanklookup
{
    string sql;
    int result;
    DbConnection conn = new DbConnection();
	public clsbanklookup()
	{
		//
		// TODO: Add constructor logic here
        //created by ashok misal
		//
	}
    public int insertdata(string bankcd,string desc,string statecd,string dist,string zone)
    {
        sql = "insert into RBIBANK (BANKCD,DESCR,STATECD,DISTRICT,ZONETIME,BRANCHCD)values ('"+bankcd+"','"+desc+"','"+statecd+"','"+dist+"','"+zone+"','0')";
        result = conn.sExecuteQuery(sql);
        return result;
    }

    public int ModifyData(string bankcd, string desc, string statecd, string dist, string zone)
    {
        sql = "update RBIBANK set DESCR='"+desc+"' ,STATECD='"+statecd+"' ,DISTRICT='"+dist+"' ,ZONETIME='"+zone+"' where BANKCD='"+bankcd+"'";
        result = conn.sExecuteQuery(sql);
        return result;
    }

    public int DeleteData(string banchcd)
    {
        sql = "delete from RBIBANK where BANKCD='" + banchcd + "'";
        result = conn.sExecuteQuery(sql);
        return result;
    }
    public int bindgrid(string Banckcode,GridView Gview)
    {

        sql = "select BANKCD,DESCR,DISTRICT from RBIBANK order by bankcd desc";
        int Result = conn.sBindGrid(Gview, sql);
        return Result;


    }
    public int bindbranch( GridView Gview)
    {

        sql = "select (CONVERT(VARCHAR(30),BRANCHCD)+'_'+CONVERT(NVARCHAR(25),BANKCD)) ID,BANKCD,BRANCHCD,DESCR,DISTRICT from RBIBANK order by BANKCD desc";
        int Result = conn.sBindGrid(Gview, sql);
        return Result;


    }
    public int insertbranch(string state, string dist, string zone, string bankcd, string branchcd, string branchname,string rbibcode,string rbibrcode)
    {
        sql = "insert INto RBIBANK (BANKRBICD,BRANCHRBICD,BANKCD,BRANCHCD,DESCR,ZONETIME,DISTRICT,STATECD)values('"+rbibcode+"','"+rbibrcode+"','" + bankcd + "','" + branchcd + "','" + branchname + "','" + zone + "','" + dist + "','" + state + "')";
        result = conn.sExecuteQuery(sql);
        return result;
    }
    public int ModifyBranch(string state, string dist, string zone, string bankcd, string branchcd, string branchname)
    {
        sql = "update RBIBANK set DESCR='"+branchname+"',DISTRICT='"+dist+"' ,STATECD='"+state+"', ZONETIME='"+zone+"' where BANKCD='"+bankcd+"' and BRANCHCD='"+branchcd+"'";
        result = conn.sExecuteQuery(sql);
        return result;
    }
    public int deletebranch( string bankcd, string branchcd)
    {
        sql = "delete from RBIBANK where BANKCD='"+bankcd+"' and BRANCHCD='"+branchcd+"'";
        result = conn.sExecuteQuery(sql);
        return result;
    }
    public DataTable showdata(string bankcd)
    {
        DataTable DT = new DataTable();
        sql = "select BANKCD,DESCR,DISTRICT,ZONETIME from RBIBANK where BANKCD='"+bankcd+"'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public DataTable SHOWBRANCH(string bankcd)
    {
        DataTable DT = new DataTable();
        sql = "select BANKCD,DESCR,DISTRICT,ZONETIME from RBIBANK where BANKCD='" + bankcd + "'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public DataTable getbranch(string bankcd, string branchcd)
    {
        DataTable DT = new DataTable();
        sql = "select BANKCD,BRANCHCD,DESCR,ZONETIME,DISTRICT from RBIBANK where BANKCD='"+bankcd+"' and BRANCHCD='"+branchcd+"'";
        DT = conn.GetDatatable(sql);
        return DT;
    }
    public string getbankname(string bcode)
    {
        sql = "select DESCR from RBIBANK where BANKCD='"+bcode+"' and BRANCHCD=0";
       string result = conn.sExecuteScalar(sql);
        return result;
    }
    public string checkbranch(string bankcode,string branchcode)
    {
        sql = "select BRANCHCD from RBIBANK where BANKCD='" + bankcode + "' and BRANCHCD='" + branchcode + "'";
        string  result = conn.sExecuteScalar(sql);
        return result;
    }
    public string getbranchname(string bankcode, string branchcode)
    {
        sql = "select DESCR from rbibank where bankcd='"+bankcode+"' and BRANCHCD='"+branchcode+"'";
        string  result = conn.sExecuteScalar(sql);
        return result;
    }
    public string  autoidbank()
    {
        sql = "select MAX(BANKCD)+1 from RBIBANK";
       string  result = conn.sExecuteScalar(sql);
        return result;
    }
}