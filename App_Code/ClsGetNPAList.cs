using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ClsGetNPAList
/// </summary>
public class ClsGetNPAList
{
    DataTable Dt = new DataTable();
    DbConnection Conn = new DbConnection();
    string sql = "";
    int Result;
    int Res = 0;

	public ClsGetNPAList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetNPAAccList(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string S1, string S2, string Flag)
    {
        try
        {
            sql = "Exec Isp_AVS0009 @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@FAccNo='" + FACCNO + "',@TAccNo='" + TACCNO + "', @EDate='" + Conn.ConvertDate(FDT) + "',@AccType='" + S1 + "',@SType='" + S2 + "',@SkipSTD ='" + Flag + "'  ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetODSumryFyList(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string FL)
    {
        try
        {
            sql = "Exec RptLoanODSummaryList @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@FAccNo='" + FACCNO + "',@TAccNo='" + TACCNO + "', @EDate='" + Conn.ConvertDate(FDT) + "',@Flag='" + FL + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }

    public DataTable GetNPAAccListSumary(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string FL, string S1, string S2, string Flag)
    {
        try
        {
            sql = "Exec Isp_AVS0023 @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@FAccNo='" + FACCNO + "',@TAccNo='" + TACCNO + "', @EDate='" + Conn.ConvertDate(FDT) + "',@AccType='" + S1 + "',@SType='" + S2 + "',@SkipSTD ='" + Flag + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetNPADetailsListDT_1 (string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string FL, string S1, string S2, string Flag)
    {
        try
        {
            sql = "Exec SP_Isp_AVS0009 @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@FAccNo='" + FACCNO + "',@TAccNo='" + TACCNO + "', @EDate='" + Conn.ConvertDate(FDT) + "',@AccType='" + S1 + "',@SType='" + S2 + "',@SkipSTD ='" + Flag + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }


    public string GetDivMemListDT_Post(string FBRCD, string TBRCD, string EDate, string FLT, string FL, string S1, string S2, string DrPrd)
    {
        try
        {
            sql = "Exec SP_DividentPosting @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@PostDate='" + Conn.ConvertDate(EDate) + "',@Flag='" + FLT + "', @RefID='" + FL + "',@Divsion='" + S1 + "',@Deprtment='" + S2 + "',@DrBank='" + DrPrd + "' ";
            sql = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public DataTable GetNPASumryListDT_1 (string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string FL, string S1, string S2, string Flag)
    {
        try
        {
            sql = "Exec SP_Isp_AVS0023 @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@FAccNo='" + FACCNO + "',@TAccNo='" + TACCNO + "', @EDate='" + Conn.ConvertDate(FDT) + "',@AccType='" + S1 + "',@SType='" + S2 + "',@SkipSTD ='" + Flag + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetNPASelectTypeListDT_1(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FACCNO, string TACCNO, string FDT, string FL, string S1, string S2, string Flag, string FLT)
    {
        try
        {
            sql = "Exec SP_Isp_AVS0009_NPATYpe @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@FAccNo='" + FACCNO + "',@TAccNo='" + TACCNO + "', @EDate='" + Conn.ConvertDate(FDT) + "',@AccType='" + S1 + "',@SType='" + S2 + "',@SkipSTD ='" + Flag + "' ,@NPAType ='" + FLT + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    
    public DataTable GetSRORecovery(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FDT, string TDT, string FL,string FLT)
    {
        try
        {
            sql = "Exec Isp_AVS0024 @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@FromDate='" + Conn.ConvertDate(FDT) + "',@EDate='" + Conn.ConvertDate(TDT) + "',@AccType='" + FL + "',@SRO='" + FLT + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetSRORecoverySumry (string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FDT, string TDT, string FL, string FLT)
    {
        try
        {
            sql = "Exec RptSRORecListSumry @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@FromDate='" + Conn.ConvertDate(FDT) + "',@EDate='" + Conn.ConvertDate(TDT) + "',@AccType='" + FL + "',@SRO='" + FLT + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public int GetSROInfoDT(GridView Gview, string FBRCD, string FDT, string TDT)
    {
        try
        {
            sql = "Exec RptSRORecList_Dash @FBrCode='" + FBRCD + "',@FromDate='" + Conn.ConvertDate(FDT) + "',@EDate='" + Conn.ConvertDate(TDT) + "' ";
            Result = Conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public int GetSROInfoDT_BR(GridView Gview, string FBRCD, string TBRCD, string FDT, string TDT)
    {
        try
        {
            sql = "Exec RptSRORecList_DashDT @FBrCode='" + FBRCD + "',@Branch='" + TBRCD + "',@FromDate='" + Conn.ConvertDate(FDT) + "',@EDate='" + Conn.ConvertDate(TDT) + "' ";
            Result = Conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetMonthlyRecovery (string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FDT, string TDT, string FL)
    {
        try
        {
            sql = "Exec RptMonthlyRecList @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@FromDate='" + Conn.ConvertDate(FDT) + "',@EDate='" + Conn.ConvertDate(TDT) + "',@AccType='" + FL + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetDivMemListDT(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FDT, string TDT, string S1, string S2)
    {
        try
        {
            sql = "Exec RptDivdentMemWiseList @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FMemNo='" + FPRCD + "',@TMemNo='" + TPRCD + "',@FromDate='" + Conn.ConvertDate(FDT) + "',@EDate='" + Conn.ConvertDate(TDT) + "',@Divsion='" + S1 + "',@Deprtment='" + S2 + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetDivMemListSHRDP(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FDT, string TDT, string S1, string S2)
    {
        try
        {
            sql = "Exec SP_DIVMemcalculation @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FMemNo='" + FPRCD + "',@TMemNo='" + TPRCD + "',@FromDate='" + Conn.ConvertDate(FDT) + "',@EDate='" + Conn.ConvertDate(TDT) + "',@Divsion='" + S1 + "',@Deprtment='" + S2 + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }

    public int GetInstInfoDT(GridView Gview, string FBRCD, string FDT, string TDT)
    {
        try
        {
            sql = "Exec RptInstRecList_Dash @FBrCode='" + FBRCD + "',@FromDate='" + Conn.ConvertDate(FDT) + "',@EDate='" + Conn.ConvertDate(TDT) + "' ";
            Result = Conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetInstInfoDT_BR(GridView Gview, string FBRCD, string TBRCD, string FDT, string TDT)
    {
        try
        {
            sql = "Exec RptInstRecList_DashDT @FBrCode='" + FBRCD + "',@Branch='" + TBRCD + "',@FromDate='" + Conn.ConvertDate(FDT) + "',@EDate='" + Conn.ConvertDate(TDT) + "' ";
            Result = Conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
    public DataTable GetDTLoanInstAmt_DASH(string FBC, string FPRCD, string FDate, string TDate)
    {
        try
        {
            sql = "Exec RptLoanInstAmt_DASH @Brcd='" + FBC + "',@Subglcode='" + FPRCD + "',@FromDate='" + Conn.ConvertDate(FDate) + "',@ToDate='" + Conn.ConvertDate(TDate) + "',@Flag='AccWise' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetInstRecList_DT(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FDT, string TDT, string FL, string FLT)
    {
        try
        {
            sql = "Exec RptLoanInstAmt_DASH @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@FromDate='" + Conn.ConvertDate(FDT) + "',@EDate='" + Conn.ConvertDate(TDT) + "',@AccType='" + FL + "',@SRO='" + FLT + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }


    public DataTable GetSRORecovery_DT(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FDT, string TDT, string FL, string FLT, string PT)
    {
        try
        {
            sql = "Exec Isp_AVS0024_DT @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@FromDate='" + Conn.ConvertDate(FDT) + "',@EDate='" + Conn.ConvertDate(TDT) + "',@AccType='" + FL + "',@SRO='" + FLT + "',@TrType='" + PT + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public DataTable GetSRORecoverySumry_DT(string FBRCD, string TBRCD, string FPRCD, string TPRCD, string FDT, string TDT, string FL, string FLT, string PT)
    {
        try
        {
            sql = "Exec RptSRORecListSumry_DT @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FSGlCode='" + FPRCD + "',@TSGlCode='" + TPRCD + "',@FromDate='" + Conn.ConvertDate(FDT) + "',@EDate='" + Conn.ConvertDate(TDT) + "',@AccType='" + FL + "',@SRO='" + FLT + "',@TrType='" + PT + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }

    public DataTable GetDivMemPendingListDT(string FBRCD, string TBRCD, string FDT, string TDT, string FLT, string FL, string S1, string S2)
    {
        try
        {
            sql = "Exec RptDivPendingList @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FromDate='" + Conn.ConvertDate(FDT) + "',@ToDate='" + Conn.ConvertDate(TDT) + "',@Flag='" + FLT + "',@RefID='" + FL + "',@Divsion='" + S1 + "',@Deprtment='" + S2 + "' ";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }

    public string GetDivMemPendingListDT_Total(string FBRCD, string TBRCD, string FDT, string TDT, string FLT, string FL, string S1, string S2)
    {
        string RES = "";
        try
        {
            sql = "Exec RptDivPendingList @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FromDate='" + Conn.ConvertDate(FDT) + "',@ToDate='" + Conn.ConvertDate(TDT) + "',@Flag='" + FLT + "',@RefID='" + FL + "',@Divsion='" + S1 + "',@Deprtment='" + S2 + "' ";
            RES = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return RES;
    }
    public int GetDivMemPendingListDT_1(GridView GRD, string FBRCD, string TBRCD, string FDT, string TDT, string FLT, string FL, string S1, string S2)
    {
        try
        {
            sql = "Exec RptDivPendingList @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FromDate='" + Conn.ConvertDate(FDT) + "',@ToDate='" + Conn.ConvertDate(TDT) + "',@Flag='" + FLT + "', @RefID='" + FL + "',@Divsion='" + S1 + "',@Deprtment='" + S2 + "' ";
            Res = Conn.sBindGrid(GRD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }

    public int GetDivMemPendingListDT_CRE(string FBRCD, string TBRCD, string FDT, string TDT, string FLT, string FL, string S1, string S2)
    {
        try
        {
            sql = "Exec RptDivPendingList @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FromDate='" + Conn.ConvertDate(FDT) + "',@ToDate='" + Conn.ConvertDate(TDT) + "',@Flag='" + FLT + "', @RefID='" + FL + "',@Divsion='" + S1 + "',@Deprtment='" + S2 + "' ";
            Res = Conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }

    public int InsertTrans(string FPRCD, string F1, string F2, string Chq, string Div, string Dept)
    {
        try
        {
            sql = "UPDATE AVS_Divident SET RC_INT='" + F1 + "',RC_DIV='" + F2 + "',INSTNO='" + Chq + "',RecDiv='" + Div + "',RecDept='" + Dept + "',stage='1002' WHERE AccNO = '" + FPRCD + "' And Stage <> 1004";
            Result = Conn.sExecuteQuery(sql);

        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public string GetDivMemPendingListDT_Post(string FBRCD, string TBRCD, string FDT, string TDT, string FLT, string FL, string S1, string S2, string DrPrd)
    {
        try
        {
            sql = "Exec RptDivPendingList @FBrCode='" + FBRCD + "',@TBrCode='" + TBRCD + "',@FromDate='" + Conn.ConvertDate(FDT) + "',@ToDate='" + Conn.ConvertDate(TDT) + "',@Flag='" + FLT + "', @RefID='" + FL + "',@Divsion='" + S1 + "',@Deprtment='" + S2 + "',@DrBank='" + DrPrd + "' ";
            sql = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public DataTable GetHeadAcListDT(string FBRCD, string FPRCD, string TPRCD, string TBRCD, string FDT, string FACCNO, string TACCNO, string FLT, string FL)
    {
        try
        {
            sql = "Exec HeadAcListVoucherTRF @BRCD='" + FBRCD + "',@PFDT='" + Conn.ConvertDate(FDT) + "',@SUBGLCODE='" + FPRCD + "',@SUBGLCODE_2='" + TPRCD + "',@SUBGLCODE_3='" + TBRCD + "',@FAccno='" + FACCNO + "',@TAccno='" + TACCNO + "',@Amt='" + FLT + "',@Flag='" + FL + "'";
            Dt = Conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Dt;
    }
    public string GetHeadAcListDT_Post(string FBRCD, string FPRCD, string TPRCD, string TBRCD, string FDT, string FACCNO, string TACCNO, string FLT, string FL)
    {
        try
        {
            sql = "Exec HeadAcListVoucherTRF @BRCD='" + FBRCD + "',@PFDT='" + Conn.ConvertDate(FDT) + "',@SUBGLCODE='" + FPRCD + "',@SUBGLCODE_2='" + TPRCD + "',@SUBGLCODE_3='" + TBRCD + "',@FAccno='" + FACCNO + "',@TAccno='" + TACCNO + "',@Amt='" + FLT + "',@Flag='" + FL + "'";
            sql = Conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
}