using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClsAddBranch
/// </summary>
public class ClsAddBranch
{
    DataTable Dt=new DataTable();
    DbConnection conn = new DbConnection();
    string Sql = "";
	public ClsAddBranch()
	{
	
	}
    public DataTable getbankdetails()
    {
        Sql = "select top 1 bankcd,bankname from bankname";
        DataTable dt=new DataTable();
        dt=conn.GetDatatable(Sql);
        return dt;
    }
    public string getbrcode(string brcd)
    {
        Sql = "select brcd from bankname where brcd='"+brcd+"'";
        string Result = conn.sExecuteScalar(Sql);
        return Result;
    }
    public int InsertBrData(string Flag,string bankcd,string brcd,string bankname,string brname,string add1,string add2,string mobile,string email,string regno,string banksname,string  adgl,string adsubgl,string dayopen)
    {
        int Result;
        Sql = "Isp_AVS0061 @Flag='" + Flag + "',@bankcode='" + bankcd + "',@bankname='" + bankname + "',@brcd='" + brcd + "',@brname='" + brname + "',@add1='" + add1 + "',@add2='" + add2 + "',@mobile='" + mobile + "',@email='" + email + "',@RegNo='" + regno + "',@Banksname='" + banksname + "',@AGl='" + adgl + "',@ASubGl='" + adsubgl + "',@DayOpenDate='" + conn.ConvertDate(dayopen) + "'";
        Result = conn.sExecuteQuery(Sql);
        return Result;
    }
    public int updateBrData(string Flag, string bankcd, string brcd, string bankname, string brname, string add1, string add2, string mobile, string email, string regno, string banksname, string adgl, string adsubgl,string dayopen)
    {
        int Result;
        Sql = "Isp_AVS0061 @Flag='" + Flag + "',@bankcode='" + bankcd + "',@bankname='" + bankname + "',@brcd='" + brcd + "',@brname='" + brname + "',@add1='" + add1 + "',@add2='" + add2 + "',@mobile='" + mobile + "',@email='" + email + "',@RegNo='" + regno + "',@Banksname='" + banksname + "',@AGl='" + adgl + "',@ASubGl='" + adsubgl + "',@DayOpenDate='" + dayopen + "'";
        Result = conn.sExecuteQuery(Sql);
        return Result;
    }
    public DataTable getData(string brcd, string bankcd)
    {
        Sql = "SELECT B.BANKCD,B.BRCD,B.BANKNAME,B.ADDRESS1,B.ADDRESS2,B.REGISTRATIONNO,B.MIDNAME,B.MOBILE,B.EMAIL,B.ADMGlCode,B.ADMSubGlCode,B.BankNm_Short,P.LISTVALUE FROM BANKNAME B INNER JOIN parameter p ON B.BRCD=P.BRCD  WHERE B.BRCD='"+brcd+"' AND B.BANKCD='"+bankcd+"' AND P.LISTFIELD='DayOpen' and P.brcd='"+brcd+"'";
        DataTable DT = new DataTable();
        DT = conn.GetDatatable(Sql);
        return DT;
    
    }
}