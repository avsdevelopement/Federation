using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsLoanBalanceCer
/// </summary>
public class ClsLoanBalanceCer
{
    DataTable Dt = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "", result = "";
	public ClsLoanBalanceCer()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable RptLoanAmtCer(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        DataTable Dt = new DataTable();
        try
        {
            sql = "Exec ISP_AVS0115 @BrCode='"+BRCD+"',@SubGlCode='"+PCODE+"',@AccNo='"+ACCNO+"',@EDate='"+Conn.ConvertDate(Edate)+"',@Flag='LOAN'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
    public DataTable RptSavingCerti(string BRCD, string PCODE, string ACCNO, string Edate)
    {
        DataTable Dt = new DataTable();
        try
        {
            sql = "Exec ISP_AVS0116 @BrCode='" + BRCD + "',@SubGlCode='" + PCODE + "',@AccNo='" + ACCNO + "',@EDate='" + Conn.ConvertDate(Edate) + "',@Flag='SB'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
        }
        return Dt;
    }
}