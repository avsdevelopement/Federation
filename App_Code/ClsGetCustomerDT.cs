using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data.SqlClient;


/// <summary>
/// Summary description for ClsGetCustomerDT
/// </summary>
public class ClsGetCustomerDT
{
    DbConnection DBCON = new DbConnection();
    DataTable dt = new DataTable();
    string sql = "", sResult = "";
    int Result = 0;
    string Accname = "";
    
	public ClsGetCustomerDT()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string GetAccountName(string AccNo, string SubGlCode, string brcd)
    {
        try
        {
            sql = "SELECT (CONVERT(VARCHAR(10),A.CUSTNO)+'_'+CUSTNAME+'_'+Convert(VARCHAR(50),A.ACCNO)) CUSTNAME FROM AVS_ACC A INNER JOIN MASTER B ON A.CUSTNO=B.CUSTNO WHERE A.CUSTNO = B.CUSTNO AND A.ACCNO='" + AccNo + "' AND A.SUBGLCODE=4 AND A.BRCD='" + brcd + "'";
            sResult = DBCON.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataTable RptShareTZMPDTShow(string FBRCD, string FDATE, string TDATE, string FACCNO, string TACCNO, string FL, string FLT)
    {
        DataTable DT = new DataTable();
        try
        {
            string sql2 = "Exec RptSHRCertPrint @Brcd='" + FBRCD + "',@FDate='" + DBCON.ConvertDate(FDATE) + "',@TDate='" + DBCON.ConvertDate(TDATE) + "',@FMemNo='" + FACCNO + "',@TMemNo='" + TACCNO + "',@Divsion='" + FL + "',@Deprtment='" + FLT + "'";
            DT = DBCON.GetDatatable(sql2);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable RptSanchitTZMPDTShow (string FBRCD, string FDATE, string TDATE, string FACCNO, string TACCNO, string FL, string FLT)
    {
        DataTable DT = new DataTable();
        try
        {
            string sql2 = "Exec RptSanchitThevDP @Brcd='" + FBRCD + "',@PFDT='" + DBCON.ConvertDate(FDATE) + "',@PTDT='" + DBCON.ConvertDate(TDATE) + "',@FAccno='" + FACCNO + "',@TAccno='" + TACCNO + "',@Divsion='" + FL + "',@Deprtment='" + FLT + "'";
            DT = DBCON.GetDatatable(sql2);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable RptLnBalListODDT(string BRCD, string FPRCD, string TPRCD, string AsOnDate)
    {
        DataTable DT = new DataTable();
        try
        {
            string sql2 = "Exec RptAllLnBalListOD @BrCode='" + BRCD + "',@FSubglcode='" + FPRCD + "',@TSubglcode='" + TPRCD + "', @WorkDate ='" + DBCON.ConvertDate(AsOnDate) + "' ";
            DT = DBCON.GetDatatable(sql2);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable ODlIstDivWiseDT_TZMP(string BRCD, string FPRCD, string TPRCD, string AsOnDate, string FL, string FLT)
    {
        DataTable DT = new DataTable();
        try
        {
            string sql2 = "Exec RptODlIstDivWise_TZMP @FBrCode='" + BRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "', @Edate ='" + DBCON.ConvertDate(AsOnDate) + "',@Divsion='" + FL + "',@Deprtment='" + FLT + "' ";
            DT = DBCON.GetDatatable(sql2);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable RptNonMemList_DT(string BRCD, string FL, string AsOnDate)
    {
        DataTable DT = new DataTable();
        try
        {
            string sql2 = "Exec RptNonMemList @BrCd='" + BRCD + "',@Subglcode='" + FL + "',@AsOnDate ='" + DBCON.ConvertDate(AsOnDate) + "' ";
            DT = DBCON.GetDatatable(sql2);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable RecPayCLBal_ALLDT(string FBrcd, string TBrcd, string FDate, string TDate)
    {
        DataTable DT = new DataTable();
        try
        {
            string sql2 = "Exec RptRecPayCLBal_ALL @FBRCD='" + FBrcd + "',@TBRCD='" + TBrcd + "',@PFDT ='" + DBCON.ConvertDate(FDate) + "',@PTDT ='" + DBCON.ConvertDate(TDate) + "' ";
            DT = DBCON.GetDatatable(sql2);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    
}