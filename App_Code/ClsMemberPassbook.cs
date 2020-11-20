using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsMemberPassbook
/// </summary>
public class ClsMemberPassbook
{
    DataTable Dt = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";

	public ClsMemberPassbook()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetmemberDT (string FDate, string TDate, string PT, string FBC)
    {
        try
        {
            sql = "Exec RptGlobalStatement @PFDT='" + Conn.ConvertDate(FDate).ToString() + "' ,@PTDT='" + Conn.ConvertDate(TDate).ToString() + "',@Custno='" + PT + "',@BRCD='" + FBC + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetmemberDT1(string FDate, string TDate, string PT, string FBC)
    {
        try
        {
            string[] fdate = FDate.Split('/');
            string[] tdate = TDate.Split('/');
            sql = "EXEC ISP_AVS0108 '" + fdate[1].ToString() + "','" + tdate[1].ToString() + "','" + Conn.ConvertDate(FDate) + "','" + Conn.ConvertDate(TDate) + "','" + fdate[2].ToString() + "','" + tdate[2].ToString() + "','" + FBC + "','" + PT + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetmemberDetails (string FDate, string TDate, string PT, string FBC)
    {
        try
        {
            string[] fdate = FDate.Split('/');
            string[] tdate = TDate.Split('/');
            sql = "EXEC ISP_AVS0108_Details '" + fdate[1].ToString() + "','" + tdate[1].ToString() + "','" + Conn.ConvertDate(FDate) + "','" + Conn.ConvertDate(TDate) + "','" + fdate[2].ToString() + "','" + tdate[2].ToString() + "','" + FBC + "','" + PT + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetStaffmemberDT (string FDate, string TDate, string PT, string FBC)
    {
        try
        {
            string[] fdate = FDate.Split('/');
            string[] tdate = TDate.Split('/');
            sql = "EXEC RptStaffMemPassbook '" + fdate[1].ToString() + "','" + tdate[1].ToString() + "','" + Conn.ConvertDate(FDate) + "','" + Conn.ConvertDate(TDate) + "','" + fdate[2].ToString() + "','" + tdate[2].ToString() + "','" + FBC + "','" + PT + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    
    public string GetBankcd(string BRCD)
    {
        sql = "select Bankcd from Bankname where brcd='" + BRCD + "'";
        string BANKCD = Conn.sExecuteScalar(sql);
        return BANKCD;
    }
    public DataTable GetmemberDT_ALL(string FDate, string TDate, string PT, string FL, string FBC, string S1, string S2)
    {
        try
        {
            string[] fdate = FDate.Split('/');
            string[] tdate = TDate.Split('/');
            sql = "EXEC RptGlobalStatement_All @pfmonth='" + fdate[1].ToString() + "',@ptmonth='" + tdate[1].ToString() + "',@PFDT='" + Conn.ConvertDate(FDate) + "',@PTDT='" + Conn.ConvertDate(TDate) + "',@pfyear='" + fdate[2].ToString() + "',@ptyear='" + tdate[2].ToString() + "',@BRCD='" + FBC + "',@FCustno='" + PT + "',@TCustno='" + FL + "',@Divsion='" + S1 + "',@Deprtment='" + S2 + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetmemberPassfarmer(string FDate, string TDate, string PT, string FBC)
    {
        try
        {
            string[] fdate = FDate.Split('/');
            string[] tdate = TDate.Split('/');
            sql = "EXEC ISP_AVS0149 '" + fdate[1].ToString() + "','" + tdate[1].ToString() + "','" + Conn.ConvertDate(FDate) + "','" + Conn.ConvertDate(TDate) + "','" + fdate[2].ToString() + "','" + tdate[2].ToString() + "','" + FBC + "','" + PT + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
}