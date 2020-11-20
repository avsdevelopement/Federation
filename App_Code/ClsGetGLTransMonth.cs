using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsGetGLTransMonth
/// </summary>
public class ClsGetGLTransMonth
{
    DataTable Dt = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";

	public ClsGetGLTransMonth()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetGlMonthWise (string FBC, string PT, string FDate, string TDate)
    {
        try
        {
            sql = "Exec RptGLWiseTransMonthWise '" + FBC + "','" + PT + "','" + Conn.ConvertDate(FDate).ToString() + "' ,'" + Conn.ConvertDate(TDate).ToString() + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {

            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetGLIntCalDetails(string FBC, string PT, string FDate, string TDate, string FL)
    {
        try
        {
            sql = "Exec RptGlInterestCal @Brcd='" + FBC + "',@SubGlCode='" + PT + "',@FromDate='" + Conn.ConvertDate(FDate).ToString() + "' ,@ToDate='" + Conn.ConvertDate(TDate).ToString() + "',@IntRate='" + FL + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetGLIntCalDetails1(string FBC, string PT, string FDate, string TDate, string FL, string FLT)//Amruta 13/04/2018
    {
        try
        {
            sql = "Exec RptGlInterestCal @Brcd='" + FBC + "',@SubGlCode='" + PT + "',@FromDate='" + Conn.ConvertDate(FDate).ToString() + "' ,@ToDate='" + Conn.ConvertDate(TDate).ToString() + "',@IntRate='" + FL + "',@IntRateDr='"+FLT+"' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetDoramntAcListDetails(string FBC, string FDate, string PT, string FL , string Amt)
    {
        try
        {
            sql = "Exec RptDormantAcList @Brcd='" + FBC + "',@PFDT='" + Conn.ConvertDate(FDate).ToString() + "' ,@SubGlCode='" + PT + "',@PERIOD='" + FL + "' ,@Amt ='" + Amt + "' ";
            Dt = Conn.GetDatatable(sql);
        
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetDoramntDueAcListDetails (string FBC, string FDate, string PT, string FL, string Amt)
    {
        try
        {
            sql = "Exec RptDormantDueAcList @Brcd='" + FBC + "',@PFDT='" + Conn.ConvertDate(FDate).ToString() + "' ,@SubGlCode='" + PT + "',@DueDate='" + Conn.ConvertDate(FL).ToString() + "' ,@Amt ='" + Amt + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    
}