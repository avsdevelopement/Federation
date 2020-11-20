using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsGetPLExepenses
/// </summary>
public class ClsGetPLExepenses
{
    DataTable Dt = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";

	public ClsGetPLExepenses()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetPLExepensesDT(string BranchID, string FDT, string TDT)
    {
        try
        {
            sql = "Exec RptPLExpensesReport @BRCD='" + BranchID + "', @PFDT='" + Conn.ConvertDate(FDT).ToString() + "',@PTDT='" + Conn.ConvertDate(TDT).ToString() + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }

    public DataTable GetPLExepensesDT_Bal(string BranchID, string FDT, string TDT)
    {
        try
        {
            sql = "Exec RptRecPayCLBal @BRCD='" + BranchID + "', @PFDT='" + Conn.ConvertDate(FDT).ToString() + "',@PTDT='" + Conn.ConvertDate(TDT).ToString() + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetPLExepensesDT_SkipData (string BranchID, string FDT, string TDT)
    {
        try
        {
            sql = "Exec RptRecPayCLBal_SkipData @BRCD='" + BranchID + "', @PFDT='" + Conn.ConvertDate(FDT).ToString() + "',@PTDT='" + Conn.ConvertDate(TDT).ToString() + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetPLExepensesDT_BalMarathi (string BranchID, string FDT, string TDT)
    {
        try
        {
            sql = "Exec RptRecPayCLBal_Marathi @BRCD='" + BranchID + "', @PFDT='" + Conn.ConvertDate(FDT).ToString() + "',@PTDT='" + Conn.ConvertDate(TDT).ToString() + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetShrBalRegisterDetails(string FBRCD, string TBRCD, string FDT, string TDT)
    {
        try
        {
            sql = "Exec RptShrBalRegister @FBRCD ='" + FBRCD + "', @TBRCD ='" + TBRCD + "', @PFDT='" + Conn.ConvertDate(FDT).ToString() + "',@PTDT='" + Conn.ConvertDate(TDT).ToString() + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetShrBalRegisterSumry (string FBRCD, string TBRCD, string FDT, string TDT)
    {
        try
        {
            sql = "Exec RptShrBalRegisterSumry @FBRCD ='" + FBRCD + "', @TBRCD ='" + TBRCD + "', @PFDT='" + Conn.ConvertDate(FDT).ToString() + "',@PTDT='" + Conn.ConvertDate(TDT).ToString() + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetShrBalCertDT(string FBRCD, string TBRCD, string FDT, string TDT)
    {
        try
        {
            sql = "Exec RptShrBalanceCertWise @FBRCD ='" + FBRCD + "', @TBRCD ='" + TBRCD + "', @PFDT='" + Conn.ConvertDate(FDT).ToString() + "',@PTDT='" + Conn.ConvertDate(TDT).ToString() + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetIntRateWiseDT(string FBRCD, string TBRCD, string FPRCD, string FDT, string FL)
    {
        try
        {
            sql = "Exec RptIntRateWiseDPDetails @FromBr='" + FBRCD + "',@ToBr='" + TBRCD + "',@ProductCode='" + FPRCD + "', @Asondate='" + Conn.ConvertDate(FDT) + "',@Flag='" + FL + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetIntRateWiseSumry (string FBRCD, string TBRCD, string FPRCD, string FDT, string FL)
    {
        try
        {
            sql = "Exec RptIntRateWiseDPDetails @FromBr='" + FBRCD + "',@ToBr='" + TBRCD + "',@ProductCode='" + FPRCD + "', @Asondate='" + Conn.ConvertDate(FDT) + "',@Flag='" + FL + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetPLALLBrDetails(string FBRCD, string TBRCD, string FDT)
    {
        try
        {
            string[] TD = FDT.Split('/');
            sql = "Exec RptPLALLBrReport @FBRCD='" + FBRCD + "',@TBRCD='" + TBRCD + "',@PFMONTH='" + TD[1].ToString() + "',@PFYEAR='" + TD[2].ToString() + "',@FromDate='" + Conn.ConvertDate(FDT) + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    
}