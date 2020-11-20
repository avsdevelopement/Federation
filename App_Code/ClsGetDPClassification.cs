using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsGetDPClassification
/// </summary>
public class ClsGetDPClassification
{
    DataTable Dt = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";

	public ClsGetDPClassification()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetDPClassfySumry(string FBRCD, string TBRCD, string FPRCD, string FDT, string FL)
    {
        try
        {
            sql = "Exec RptAcctypeWiseDP @FromBr='" + FBRCD + "',@ToBr='" + TBRCD + "',@ProductCode='" + FPRCD + "', @Asondate='" + Conn.ConvertDate(FDT) + "',@Flag='" + FL + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetClassiOfLoanDT(string FBRCD, string TBRCD, string FPRCD, string FDT, string FL)
    {
        try
        {
            sql = "Exec Isp_AVS0016 @FromBr='" + FBRCD + "',@ToBr='" + TBRCD + "',@ProductCode='" + FPRCD + "', @Asondate='" + Conn.ConvertDate(FDT) + "',@Flag='" + FL + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetClassiOfODListDT(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FDT, string FL)
    {
        try
        {
            sql = "Exec Isp_AVS0015 @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@EDate='" + Conn.ConvertDate(FDT) + "',@Flag='" + FL + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetClssificationODSumry_DT (string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FDT, string FL)
    {
        try
        {
            sql = "Exec Isp_AVS0015 @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@EDate='" + Conn.ConvertDate(FDT) + "',@Flag='" + FL + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
}