using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsGetCutBookDetail
/// </summary>
public class ClsGetCutBookDetail
{
    DataTable Dt = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";

	public ClsGetCutBookDetail()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable CutBookDs(string BRCD, string SGL, string FDT, string TDate)
    {
        try
        {
            sql = "Exec RptCuteBookDetails '" + BRCD + "','" + SGL + "','" + Conn.ConvertDate(FDT).ToString() + "' ,'" + Conn.ConvertDate(TDate).ToString() + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText (ex);
        }
        return Dt;
    }
    public DataTable BalvikasReport(string BRCD, string SGL, string FDT, string TDate,string flag)
    {
        try
        {
            sql = "Exec Rptforbalvikas '" + BRCD + "','" + SGL + "','" + Conn.ConvertDate(FDT) + "' ,'" + Conn.ConvertDate(TDate) + "','" + flag + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
    public DataTable BalvikasIntReport(string BRCD, string SGL, string FDT)
    {
        try
        {
            sql = "Exec ISP_AVS0101 @AsonDate='"+Conn.ConvertDate(FDT)+"',@BrCd='"+BRCD+"',@GlCode='"+SGL+"',@SubGlCode='"+SGL+"',@MID='8'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
    public DataTable ActivememReport(string BRCD, string FDT, string TDate, string Edate)
    {
        try
        {
           // sql = "Exec SP_ACTIVEMEMRPT '"+BRCD+"','"+SGL+"','"+Conn.ConvertDate(FDT)+"' ,'"+Conn.ConvertDate(TDate)+"'";
            sql = " Exec SP_ACT_MEM '" + BRCD + "','" + Conn.ConvertDate(FDT) + "','" + Conn.ConvertDate(TDate) + "','"+Conn.ConvertDate(Edate)+"'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }

    public DataTable SurityDetails(string BRCD, string SGL, string FDT, string flag, string AccNo, string FLT)
    {
        try
        {
            if (AccNo != "")
                sql = "Exec RptSurityReport @AsonDate='" + Conn.ConvertDate(FDT) + "',@Flag='" + flag + "',@BrCd='" + BRCD + "',@SubGlCode='" + SGL + "',@Accno='" + AccNo + "',@Type='" + FLT + "' ";
            else
                sql = "Exec RptSurityReport @AsonDate='" + Conn.ConvertDate(FDT) + "',@Flag='" + flag + "',@BrCd='" + BRCD + "',@SubGlCode='" + SGL + "',@Type='" + FLT + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
}