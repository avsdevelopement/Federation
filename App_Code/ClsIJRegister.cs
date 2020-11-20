using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClsIJRegister
/// </summary>
public class ClsIJRegister
{
    DbConnection conn = new DbConnection();
    DataTable dt = new DataTable();
    string sql = "";
	public ClsIJRegister()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable BindIR(string brcd, string accno, string taccno)
    {
        try
        {
            if (taccno == "")
                sql = "EXEC AN_IJREGISTER @FLAG='BINDIR',@BRCD='" + brcd + "',@ACCNO='" + accno + "',@TACCNO='" + taccno + "'";
            else
                sql = "EXEC AN_IJREGISTER @FLAG='BINDIRALL',@BRCD='" + brcd + "',@ACCNO='" + accno + "',@TACCNO='" + taccno + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
 
    public DataTable BindJR(string brcd, string accno,string taccno)
    {
        try
        {
            if(taccno=="")
            sql = "EXEC AN_IJREGISTER @FLAG='BINDJR',@BRCD='" + brcd + "',@ACCNO='" + accno + "',@TACCNO='"+taccno+"'";
            else
            sql = "EXEC AN_IJREGISTER @FLAG='BINDJRALL',@BRCD='" + brcd + "',@ACCNO='" + accno + "',@TACCNO='" + taccno + "'";

            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public DataTable AddressLabelPrint_DT(string accno, string taccno, string FDate)
    {
        try
        {
            sql = "EXEC RptAddressLabelPrint @FAccno='" + accno + "',@TAccno='" + taccno + "',@AsOnDate ='" + conn.ConvertDate(FDate).ToString() + "'";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }

    public DataTable AddressLabelPrintTZMP_DT(string Brcd, string accno, string taccno, string FDate, string S1, string S2)
    {
        try
        {
            sql = "EXEC RptAddressLabelPrint_TZMP @Brcd = '" + Brcd + "',@FAccno='" + accno + "',@TAccno='" + taccno + "',@AsOnDate ='" + conn.ConvertDate(FDate).ToString() + "',@Divsion='" + S1 + "',@Deprtment='" + S2 + "' ";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    
    public DataTable GetCustno(string brcd, string FACCNO, string TACCNO)
    {
        try
        {
            sql = "select CUSTNO from AVS_ACC where BRCD='" + brcd + "' and GLCODE=4 and ACCNO between '" + FACCNO + "' and '" + TACCNO + "' and ACC_STATUS<>3";
            dt = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return dt;
    }
    public string Getcustname(string custno, string BRCD)
    {
        try
        {
            sql = "SELECT (m.CUSTNAME+'_'+(m.CUSTNAME+'_'+convert(varchar(10),Convert(bigint,m.CUSTNO)))) CUSTNAME FROM MASTER m inner join Avs_Acc a on a.CUSTNO=m.CUSTNO WHERE a.BrCd = '"+BRCD+"' and a.GLCODE=4 and  a.ACCNO = '"+custno+"' And a.Stage In ('1001', '1002', '1003')";
            custno = conn.sExecuteScalar(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return custno;
    }
}