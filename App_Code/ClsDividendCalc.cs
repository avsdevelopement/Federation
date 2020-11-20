using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ClsDividendCalc
/// </summary>
public class ClsDividendCalc
{
    DbConnection conn = new DbConnection();
    ClsEncryptValue EV = new ClsEncryptValue();
    string StrResult = "", EMD = "";
    string sql="";
    int IntResult = 0;
    DataTable DT = new DataTable();
	public ClsDividendCalc()
	{
		
	}
    //
    public DataTable GetTrailRun(string SFL,string CALCTYPE,string EDT, string FDate, string TDate,string FPRDCD,string TPRDCD,string FACCNo,string TACCNO, string RATE, string BRCD,string MID)
    {
        try
        {

            sql = "EXEC Isp_AVS0018 @Flag='TRAILSHOW',@SFlag='" + SFL + "',@CalcType='" + CALCTYPE + "',@FPrdcd='" + FPRDCD + "',@TPrdcd='" + TPRDCD + "',@FAccno='" + FACCNo + "',@TAccno='" + TACCNO + "',@Edt='" + conn.ConvertDate(EDT) + "',@FDate='" + conn.ConvertDate(FDate) + "',@TDate='" + conn.ConvertDate(TDate) + "',@Rate='" + RATE + "',@Brcd='" + BRCD + "',@MID='" + MID + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable CalculateSDC(string FL,string SFL,string CALCTYPE, string EDT, string FDate, string TDate, string FPRDCD, string TPRDCD, string FACCNo, string TACCNO, string RATE, string BRCD, string MID,string Narr)
    {
        try
        {   //CALC  and '1' 
            sql = "EXEC Isp_AVS0018 @Flag='" + FL + "',@CalcType='" + CALCTYPE + "',@SFlag='" + SFL + "',@FPrdcd='" + FPRDCD + "',@TPrdcd='" + TPRDCD + "',@FAccno='" + FACCNo + "',@TAccno='" + TACCNO + "',@Edt='" + conn.ConvertDate(EDT) + "',@FDate='" + conn.ConvertDate(FDate) + "',@TDate='" + conn.ConvertDate(TDate) + "',@Rate='" + RATE + "',@Brcd='" + BRCD + "',@MID='" + MID + "',@Narr='" + Narr + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable CalculateSDC_DT(string FL, string EDT, string FDate, string FACCNo, string TACCNO, string RATE, string BRCD, string MID, string FPRDCD)
    {
        try
        {   //CALC  and '1' 
            sql = "EXEC SP_DivdentMemcalculation @Flag='" + FL + "',@FBrCode='" + BRCD + "',@TBrCode='" + BRCD + "',@FMemNo='" + FACCNo + "',@TMemNo='" + TACCNO + "',@FromDate='" + conn.ConvertDate(FDate) + "',@Edate='" + conn.ConvertDate(EDT) + "',@FPrdcd='" + FPRDCD + "',@Rate='" + RATE + "',@MID='" + MID + "' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    public DataTable CalculateSDC_DTASON (string FL, string EDT, string FDate, string FACCNo, string TACCNO, string RATE, string BRCD, string MID, string FPRDCD)
    {
        try
        {   //CALC  and '1' 
            sql = "EXEC SP_DivdentMemcalcOnBal @Flag='" + FL + "',@FBrCode='" + BRCD + "',@TBrCode='" + BRCD + "',@FMemNo='" + FACCNo + "',@TMemNo='" + TACCNO + "',@FromDate='" + conn.ConvertDate(FDate) + "',@Edate='" + conn.ConvertDate(EDT) + "',@FPrdcd='" + FPRDCD + "',@Rate='" + RATE + "',@MID='" + MID + "' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
    //
    public string PostDividend(string FL, string SFL, string EDT, string Rate, string Brcd, string MID, string CRPRDCD, string DRPRDCD)
    {
        try
        {
            EMD = EV.GetMK(MID);
            sql = "EXEC Isp_AVS0018 @Flag='" + FL + "',@SFlag='" + SFL + "',@Edt='" + conn.ConvertDate(EDT) + "',@Rate='" + Rate + "',@Brcd='" + Brcd + "',@MID='" + MID + "',@CrPrd='" + CRPRDCD + "',@DrPrd='" + DRPRDCD + "',@F1='" + EMD + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sql;
    }
    public int BindGrid(GridView GD,string SFL,string EDT, string FDate, string TDate,string FPRDCD,string TPRDCD,string FACCNo,string TACCNO, string RATE, string BRCD,string MID)
    {
        try
        {
            sql = "EXEC Isp_AVS0018 @Flag='TRAILSHOW',@SFlag='" + SFL + "',@FPrdcd='" + FPRDCD + "',@TPrdcd='" + TPRDCD + "',@FAccno='" + FACCNo + "',@TAccno='" + TACCNO + "',@Edt='" + conn.ConvertDate(EDT) + "',@FDate='" + conn.ConvertDate(FDate) + "',@TDate='" + conn.ConvertDate(TDate) + "',@Rate='" + RATE + "',@Brcd='" + BRCD + "',@MID='" + MID + "'";
            IntResult = conn.sBindGrid(GD,sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return IntResult;
    }
    public int Recalculate(string EDT, string BRCD, string MID)
    {
        try
        {
            sql = "EXEC Isp_AVS0018 @Flag='RECALC',@Edt='" + conn.ConvertDate(EDT) + "',@Brcd='" + BRCD + "',@MID='" + MID + "'";
            IntResult = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return IntResult;
    }

    public DataTable  DividendCalCTextRpt(string EDT, string BRCD, string MID)
    {
        try
        {
            sql = "EXEC DividendCalcTxtRpt @EDATE='" + conn.ConvertDate(EDT) + "',@Brcd='" + BRCD + "',@MID='" + MID + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }
}