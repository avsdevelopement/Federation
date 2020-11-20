using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class ClsTest
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
	public ClsTest()
	{
		
	}
    public DataTable getdate(string BRCD)//BRCD ADDED --Abhishek
    {
        string sql;
        try
        {
            sql = "Select GLCODE,GLNAME,GLGROUP,GLBALANCE,GLPRIORITY,SUBGLCODE,BRCD From GLMAST WHERE GLGROUP='DP' and BRCD='" + BRCD + "'";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable getdata(string CustNo)
    {
        DataTable dt = new DataTable();
        string sql="";
        try
        {
            sql = "select AT,CNO CustNo,JNM as CustName,AC from ACMST where AT between '201' and '301' and CNO=" + CustNo + "";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable GetLast(string BRCD)
    {
        string sql = "";
        DataTable dt = new DataTable();
        try
        {
            sql = "select LASTNO+1 as NO from AVS1000 where activityno=40 and BRCD='"+BRCD+"'";
            dt = conn.GetDatatable(sql);
         }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public int UpdateRecords(string AT, string CustName, string Custno, int LastNo, string AC)
    {
        int result = 0;
        string sql = "";
        try
        {
            sql = "exec UpdateAcc @ProdCode='" + AT + "', @CustName='" + CustName + "',@CustNo="+Custno.Trim()+",@LastNo="+LastNo+",@AC='"+AC.Trim()+"'";
            result=conn.sExecuteQuery(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return result;
    }
}