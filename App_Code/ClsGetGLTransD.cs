using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsGetGLTransD
/// </summary>
public class ClsGetGLTransD
{
    DataTable Dt = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";

	public ClsGetGLTransD()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetGLTransDReg(string FBC, string PT, string FDate, string TDate)
    {
        try
        {
            sql = "Exec RptGLWiseTransDetails '" + FBC + "','" + PT + "','" + Conn.ConvertDate(FDate).ToString() + "' ,'" + Conn.ConvertDate(TDate).ToString() + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetBrWiseGlDetails (string FBC, string PT, string FDate, string TDate)
    {
        try
        {
            sql = "Exec Isp_AVS0010 @Brcd='" + FBC + "',@SubGlCode='" + PT + "',@FromDate='" + Conn.ConvertDate(FDate).ToString() + "' ,@ToDate='" + Conn.ConvertDate(TDate).ToString() + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetBrWiseGlSumry(string FBC, string PT, string FDate, string TDate)
    {
        try
        {
            sql = "Exec RptBrWiseGLDetails @Brcd='" + FBC + "',@SubGlCode='" + PT + "',@FromDate='" + Conn.ConvertDate(FDate).ToString() + "' ,@ToDate='" + Conn.ConvertDate(TDate).ToString() + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetOfficeGLDT(string FBC, string PT, string FDate, string TDate)
    {
        try
        {
            sql = "Exec RptOfficeGLDetails @Brcd='" + FBC + "',@pat='" + PT + "',@PFDT='" + Conn.ConvertDate(FDate).ToString() + "' ,@PTDT='" + Conn.ConvertDate(TDate).ToString() + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetAdmExpensesDT(string FBC, string FDate, string TDate, string FL)
    {
        try
        {
            sql = "Exec RptAdmExpenses @Brcd='" + FBC + "', @PFDT='" + Conn.ConvertDate(FDate).ToString() + "' ,@PTDT='" + Conn.ConvertDate(TDate).ToString() + "',@Flag='" + FL + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }

    public DataTable GetAdmExpensesSumry (string FBC, string FDate, string TDate, string FL)
    {
        try
        {
            sql = "Exec RptAdmExpenses @Brcd='" + FBC + "', @PFDT='" + Conn.ConvertDate(FDate).ToString() + "' ,@PTDT='" + Conn.ConvertDate(TDate).ToString() + "',@Flag='" + FL + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetAdmExpensesBr(string FBC, string FDate, string TDate, string FL)
    {
        try
        {
            sql = "Exec RptAdmExpenses @Brcd='" + FBC + "', @PFDT='" + Conn.ConvertDate(FDate).ToString() + "' ,@PTDT='" + Conn.ConvertDate(TDate).ToString() + "',@Flag='" + FL + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }

}