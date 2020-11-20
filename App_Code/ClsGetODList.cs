using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsGetODList
/// </summary>
public class ClsGetODList
{
    DataTable Dt = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";

	public ClsGetODList()
	{
	}

    public DataTable GetLnODlist(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string FLT, string REF, string ODA, string ODI, string FSAN, string TSAN, string FDate, string TDate, string S1, string S2, string FL, string Flag, string AC)
    {
        try
        {
            sql = "Exec RptAllLoanBalancesList @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@FAccNo='" + FACCNO + "',@TAccNo='" + TACCNO + "', @EDate='" + Conn.ConvertDate(FDT) + "', @Type='" + FLT + "',@RefFilter='" + REF + "',@ODAmt ='" + ODA + "',@ODInst ='" + ODI + "',@FSanction ='" + FSAN + "',@TSanction ='" + TSAN + "',@FDate='" + Conn.ConvertDate(FDate) + "',@TDate='" + Conn.ConvertDate(TDate) + "',@S1Type ='" + S1 + "',@S2Type='" + S2 + "',@AcType='" + FL + "' ,@AcStatus='" + Flag + "',@ADDRESSYN ='" + AC + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetLnODlist_AD (string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string FLT, string REF, string ODA, string ODI, string FSAN, string TSAN, string FDate, string TDate, string S1, string S2, string FL, string Flag, string AC)
    {
        try
        {
            sql = "Exec RptAllLoanBalList_ADD @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@FAccNo='" + FACCNO + "',@TAccNo='" + TACCNO + "', @EDate='" + Conn.ConvertDate(FDT) + "', @Type='" + FLT + "',@RefFilter='" + REF + "',@ODAmt ='" + ODA + "',@ODInst ='" + ODI + "',@FSanction ='" + FSAN + "',@TSanction ='" + TSAN + "',@FDate='" + Conn.ConvertDate(FDate) + "',@TDate='" + Conn.ConvertDate(TDate) + "',@S1Type ='" + S1 + "',@S2Type='" + S2 + "',@AcType='" + FL + "' ,@AcStatus='" + Flag + "',@ADDRESSYN ='" + AC + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetLnODlist_ODFD (string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string FLT, string REF, string ODA, string ODI, string FSAN, string TSAN, string FDate, string TDate, string S1, string S2, string FL, string Flag)
    {
        try
        {
            sql = "Exec RptAllLnBalList_DDSFD @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@FAccNo='" + FACCNO + "',@TAccNo='" + TACCNO + "', @EDate='" + Conn.ConvertDate(FDT) + "', @Type='" + FLT + "',@RefFilter='" + REF + "',@ODAmt ='" + ODA + "',@ODInst ='" + ODI + "',@FSanction ='" + FSAN + "',@TSanction ='" + TSAN + "',@FDate='" + Conn.ConvertDate(FDate) + "',@TDate='" + Conn.ConvertDate(TDate) + "',@S1Type ='" + S1 + "',@S2Type='" + S2 + "',@AcType='" + FL + "' ,@AcStatus='" + Flag + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    
    public DataTable GetLnODlist_WithDP (string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string FLT, string REF, string ODA, string ODI, string FSAN, string TSAN, string FDate, string TDate, string S1, string S2, string FL, string Flag)
    {
        try
        {
            sql = "Exec RptAllLoanBalancesList_WithDP @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@FAccNo='" + FACCNO + "',@TAccNo='" + TACCNO + "', @EDate='" + Conn.ConvertDate(FDT) + "', @Type='" + FLT + "',@RefFilter='" + REF + "',@ODAmt ='" + ODA + "',@ODInst ='" + ODI + "',@FSanction ='" + FSAN + "',@TSanction ='" + TSAN + "',@FDate='" + Conn.ConvertDate(FDate) + "',@TDate='" + Conn.ConvertDate(TDate) + "',@S1Type ='" + S1 + "',@S2Type='" + S2 + "',@AcType='" + FL + "' ,@AcStatus='" + Flag + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetLnODlistSumry(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string FLT, string REF, string ODA, string ODI, string FSAN, string TSAN, string FDate, string TDate, string S1, string S2, string FL, string Flag)
    {
        try
        {
            sql = "Exec RptAllLoanBalancesListSumry @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@FAccNo='" + FACCNO + "',@TAccNo='" + TACCNO + "', @EDate='" + Conn.ConvertDate(FDT) + "', @Type='" + FLT + "',@RefFilter='" + REF + "',@ODAmt ='" + ODA + "',@ODInst ='" + ODI + "',@FSanction ='" + FSAN + "',@TSanction ='" + TSAN + "',@FDate='" + Conn.ConvertDate(FDate) + "',@TDate='" + Conn.ConvertDate(TDate) + "',@S1Type ='" + S1 + "',@S2Type='" + S2 + "',@AcType='" + FL + "' ,@AcStatus='" + Flag + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetLnODlist_Ref(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string FLT, string REF, string ODA, string ODI, string FSAN, string TSAN, string FDate, string TDate, string S1, string S2, string FL, string Flag, string SFlag)
    {
        try
        {
            sql = "Exec RptAllLoanBalancesList @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@FAccNo='" + FACCNO + "',@TAccNo='" + TACCNO + "', @EDate='" + Conn.ConvertDate(FDT) + "', @Type='" + FLT + "',@RefFilter='" + REF + "',@ODAmt ='" + ODA + "',@ODInst ='" + ODI + "',@FSanction ='" + FSAN + "',@TSanction ='" + TSAN + "',@FDate='" + Conn.ConvertDate(FDate) + "',@TDate='" + Conn.ConvertDate(TDate) + "',@S1Type ='" + S1 + "',@S2Type='" + S2 + "',@AcType='" + FL + "' ,@AcStatus='" + Flag + "',@SFlag='" + SFlag + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    } 
}