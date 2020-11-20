using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsMultiTransaction
{
    string sql = "", sResult = "";
    DbConnection conn = new DbConnection();
    int Result = 0;
    DataTable DT = new DataTable();

	public ClsMultiTransaction()
	{
		
	}

    public double GetOpenClose(string Brcode, string ProdCode, string AccNo, string EDate, string Flag)
    {
        double BAL = 0;
        try
        {
            sql = "Exec SP_OpClBalance @BrCode = '" + Brcode + "', @SubGlCode = '" + ProdCode + "', @AccNo = '" + AccNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = '" + Flag + "'";
            BAL = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BAL;
    }

    public string CheckValidation(string ProdCode, string BRCD)
    {
        try
        {
            sql = "Select CASHDR From GLMAST Where BrCd = '" + BRCD + "' And SubGlCode = '" + ProdCode + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sResult;
    }

    public string CheckMultiValidation(string ProdCode, string BRCD)
    {
        try
        {
            sql = "Select TRFDR From GLMAST Where BrCd = '" + BRCD + "' And SubGlCode = '" + ProdCode + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sResult;
    }

    public string Getaccno(string BRCD, string AT)
    {
        try
        {
            sql = " SELECT (CONVERT (VARCHAR(10),GLCODE))+'_'+GLNAME FROM GLMAST WHERE BRCD='" + BRCD + "' AND SUBGLCODE='" + AT + "' GROUP BY GLCODE,GLNAME";
            AT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AT;
    }

    public DataTable GetCustName(string GLCODE, string ACCNO, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME+'_'+CONVERT(VARCHAR(10),AC.CUSTNO) CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO WHERE AC.ACCNO='" + ACCNO + "' AND AC.SUBGLCODE='" + GLCODE + "' AND AC.BRCD='" + BRCD + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetCRDR(string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Select Abs(IsNull(Sum(A.Credit), 0)) As CREDIT, Abs(IsNull(Sum(A.Debit), 0)) As DEBIT From( " +
                  "Select (Case When TRXTYPE = '1' Then Sum(Amount) Else '0' End) As Credit, " +
                  "(Case When TRXTYPE = '2' Then Sum(Amount) Else '0' End) As Debit From Avs_TempMultiTransfer " +
                  "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '0' And Mid = '" + Mid + "' And PmtMode <> 'TR_INT' " +
                  "Group By TrxType)A ";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetCRVoucher(string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Select A.SubGlCode, Abs(IsNull(A.Credit, 0)) As CREDIT From (" +
                  "Select SubGlCode, (Case When TRXTYPE = '1' Then Amount End) As Credit, SetNo From Avs_TempMultiTransfer " +
                  "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' And TrxType = '1' And PmtMode <> 'TR_INT' )A Order By SetNo";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetDRVoucher(string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Select A.SubGlCode, Abs(IsNull(A.Debit, 0)) As DEBIT From (" +
                  "Select SubGlCode, (Case When TRXTYPE = '2' Then Amount End) As Debit, SetNo From Avs_TempMultiTransfer " +
                  "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' And TrxType = '2' And PmtMode <> 'TR_INT')A Order By SetNo";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string CheckStage(string setno, string edt, string BRCD)
    {
        try
        {
            string sql = "select Stage from ALLVCR where SETNO='" + setno + "' and EntryDate='" + conn.ConvertDate(edt) + "' and BRCD='" + BRCD + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return sResult;
        }
        return sResult;
    }

    public int DeleteSingleRecTable(string id, string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Delete From Avs_TempMultiTransfer Where ID = '" + id + "' And BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public void DelAllRecTable(string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = " Delete From TempLnTrx Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "'" +
                  "Delete From Avs_TempMultiTransfer Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public int Getinfotable(GridView Gview, string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Select ID, SubGlCode, AccNo, CustName, Amount, Particulars2, (Case When TRXTYPE = 1 Then 'Cr' Else 'Dr' End) As TrxType From Avs_TempMultiTransfer Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' And PmtMode <> 'TR_INT' Order By SystemDate Desc";

            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public DataTable GetTransDetails(string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "SELECT ID, BrCd, CustNo, CustName, GlCode, SubGlCode, AccNo, Particulars, Particulars2, Amount, TrxType, Activity, PmtMode, SetNo, RefId, InstNo, Convert(VarChar(10), InstDate, 103) As InstDate, EntryDate, Mid " +
                  "From Avs_TempMultiTransfer Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' And SetNo = '0' Order By SystemDate";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string GetCrTrans(string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Select Count(1) From Avs_TempMultiTransfer Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' "+
                  "And Mid = '" + Mid + "' And SetNo = '0' And SubGlCode = '4' And TrxType = '1'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataTable GetTransDetails1(string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "SELECT ID, BrCd, CustNo, CustName, GlCode, SubGlCode, AccNo, Particulars, Particulars2, Amount, TrxType, Activity, PmtMode, SetNo, RefId, InstNo, Convert(VarChar(10), InstDate, 103) As InstDate, EntryDate, Mid " +
                  "From Avs_TempMultiTransfer Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' And SetNo = '1' Order By SetNo";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetLnTrxTransDetails(string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "SELECT * From TempLnTrx Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' And SetNo = '0' Order By SetNo";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public DataTable GetLnTrxTransDetails1(string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "SELECT * From TempLnTrx Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' And SetNo = '1' Order By SetNo";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public double DebitAmount(string BrCode, string SGLCode, string AccNo, string EDate, string Mid)
    {
        try
        {
            sql = "SELECT ABS(ISNULL(SUM(AMOUNT), 0)) AS DEBIT From Avs_TempMultiTransfer Where BrCd = '" + BrCode + "' And SUBGLCODE = '" + SGLCode + "' AND ACCNO = '" + AccNo + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' AND TRXTYPE = '2'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(sResult);
    }

    public string CheckGlGroup(string BRCD, string PrCode)
    {
        string RC = "";
        try
        {
            string sql = "SELECT ISNULL(GLGROUP, '') FROM GLMAST WHERE BRCD = '" + BRCD + "' and SUBGLCODE='" + PrCode + "' AND GLGROUP = 'CBB' ";
            RC = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return RC;
        }
        return RC;
    }

    public DataTable GetLoanTotalAmount(string BrCode, string PrCode, string AccNo, string EDate, string FL = "CL") // Optional Parameter added by Abhishek for Pen Requirement 04/09/2017
    {
        try
        {
            sql = "Exec RptAllLoanBalances '" + BrCode + "','" + PrCode + "','" + AccNo + "','" + conn.ConvertDate(EDate).ToString() + "','LoanInst','" + FL + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public double GetOtherIntRate(string BrCode, string SubGlCode)
    {
        try
        {
            sql = "Select OTHERCHG From LOANGL Where BrCd = '" + BrCode + "' and LoanGlCode='" + SubGlCode + "'";
            sResult = conn.sExecuteScalar(sql);
            if (sResult == null)
                sResult = Convert.ToDouble(0.00).ToString();
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(sResult == "" ? "0.00" : sResult);
    }

    public DataTable GetADMSubGl(string BrCode)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Select ADMGlCode, ADMSubGlCode From BankName Where BrCd = '" + BrCode + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int InsertData(string BrCode, string ApplType, string MemberNo, string CustNo, string NoOfShr, string ShrValue, string TotalShr, string SetNo, string PmtMode, string Remark, string Parti, string EDate, string Mid)
    {
        try
        {
            if (ApplType == "2")
            {
                sql = "SELECT MAX(ISNULL(A.AppNo, 0)) + 1 AS AppNo FROM (SELECT MAX(ISNULL(AppNo, 0)) AS AppNo FROM AVS_SHRAPP Where MemberClass = 'A')A";
                sResult = conn.sExecuteScalar(sql);
            }

            if (Convert.ToDouble(sResult) > 0)
            {
                sql = "Exec Sp_ShareAppliaction @BRCD = '" + BrCode + "', @ApplType = '" + ApplType + "', @MemberNo = '" + MemberNo + "', @CustNo = '" + CustNo + "', @AppNo = '" + sResult + "', @SHRValue = '" + ShrValue + "', @NoOfSHR = '" + NoOfShr + "', @TotSHRValue = '" + TotalShr + "', @SetNo = '" + SetNo + "', @PMTMode = '" + PmtMode + "', @REAMARK = '" + Remark + "', @MID = '" + Mid + "', @PCMAC = '" + conn.PCNAME().ToString() + "', @Particulars = '" + Parti + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag ='ADD'";
                Result = conn.sExecuteQuery(sql);
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int GetShareSuspGl(string BrCode)
    {
        try
        {
            sql = "Select IsNull(SHARES_GL, 0) As SHARES_GL From AVS_SHRPARA Where BRCD = '" + BrCode + "' And EntryDate = (Select Max(EntryDate) From AVS_SHRPARA Where BRCD = '" + BrCode + "')";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

}