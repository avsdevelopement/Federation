using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsAVS5118
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sResult = "";
    string TableName = "";
    int Result = 0;

    public ClsAVS5118()
    {

    }

    public string GetProduct(string BrCode, string PrCode)
    {
        try
        {
            sql = "Select ConVert(VarChar(10), GlCode) +'_'+ ConVert(VarChar(10), SubGlCode) +'_'+ GlName From GlMast With(NoLock) " +
                  "Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataTable GetAccStage(string BrCode, string PrCode, string AccNo)
    {
        DT = new DataTable();
        try
        {
            sql = "Select AccNo, CustNo, Acc_Status, Stage From Avs_Acc With(NoLock) " +
                  "Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "' And Stage <> 1004 ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string GetCustName(string BrCode, string PrCode, string AccNo)
    {
        try
        {
            sql = "Select M.CustName From Avs_Acc Ac With(NoLock) " +
                  "Inner Join Master M With(NoLock) On Ac.CustNo = M.CustNo " +
                  "Where Ac.BrCd = '" + BrCode + "' And Ac.SubGlCode = '" + PrCode + "' And Ac.AccNo = '" + AccNo + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetGlCode(string BrCode, string PrCode)
    {
        try
        {
            sql = "Select IsNull(GlCode, 0) As GlCode From GlMast Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetSubGlCode(string BrCode)
    {
        try
        {
            sql = "Select IsNull(ListValue, 0) As SubGlCode From Parameter Where BrCd = '" + BrCode + "' And ListField = 'ServChrg'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataTable GetADMSubGl(string BrCode)
    {
        DT = new DataTable();
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

    public DataTable GstDetails(string BrCode)
    {
        DT = new DataTable();
        try
        {
            sql = "Select GST, PrdCd, CGST, CGSTPrdCd, SGST, SGSTPrdCd From GstMaster Where BrCd = '" + BrCode + "' And Stage <> 1004";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public double GetOpenClose(string Brcode, string ProdCode, string AccNo, string RecNo, string EDate, string Flag)
    {
        double BAL = 0;
        try
        {
            sql = "Exec SP_OpClBalance @BrCode = '" + Brcode + "', @SubGlCode = '" + ProdCode + "', @AccNo = '" + AccNo + "', @RecNo = '" + RecNo + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @Flag = '" + Flag + "'";
            BAL = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BAL;
    }

    public void BindIssuedGrid(GridView GrdIssued, string BrCode)
    {
        try
        {
            sql = "Select SrNumber, TransType, ConVert(Varchar(10), IssueDate, 103) As IssueDate, IssueBrCode, IssueGlCode, IssueSubGlCode, IssueAccNo, ChequeAmt, " +
                  "CrBrCode, CrGlCode, CrSubGlCode, PayOrderNo, ConVert(varChar(10), PayOrderDate, 103) As PayOrderDate, ChargesAmt, CGSTAmt, SGSTAmt, TotalAmt, " +
                  "ChequeNo, PayOrderNo, IssueCustNo, IssueCustName, BenefName, Narration, Stage, ConVert(VarChar(10), EntryDate, 103) As EntryDate, Mid, RefNo " +
                  "From AVS3000 Where IssueBrCode = '" + BrCode + "' And Stage = '1001'";
            Result = conn.sBindGrid(GrdIssued, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public DataTable GetDetails(string BrCode, string SrNumber)
    {
        DT = new DataTable();
        try
        {
            sql = "Select SrNumber, TransType, ConVert(Varchar(10), IssueDate, 103) As IssueDate, IssueBrCode, IssueGlCode, IssueSubGlCode, IssueAccNo, ChequeAmt, " +
                  "CrBrCode, CrGlCode, CrSubGlCode, PayOrderNo, ConVert(varChar(10), PayOrderDate, 103) As PayOrderDate, ChargesAmt, CGSTAmt, SGSTAmt, TotalAmt, " +
                  "ChequeNo, PayOrderNo, IssueCustNo, IssueCustName, BenefName, Narration, Stage, ConVert(VarChar(10), EntryDate, 103) As EntryDate, Mid, RefNo " +
                  "From AVS3000 Where IssueBrCode = '" + BrCode + "' And SrNumber = '" + SrNumber + "' And Stage = '1001'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int IssuePayOrder(string TransType, string DrBrCode, string IssueDate, string DrGlCode, string DrPrCode, string DrAccNo, string CustNo, string CustName,
        string ChqAmt, string ChqNo, string CrBrCode, string CrGlCode, string CrPrCode, string PONum, string PODate, string SerAmt, string CGST, string SGST, string TotAmt,
        string BenefName, string Narration, string EDate, string Mid)
    {
        try
        {
            sql = "Insert Into AVS3000(TransType, IssueDate, IssueBrCode, IssueGlCode, IssueSubGlCode, IssueAccNo, ChequeAmt, ChequeNo, IssueCustNo, IssueCustName, CrBrCode, " +
                  "CrGlCode, CrSubGlCode, PayOrderNo, PayOrderDate, ChargesAmt, CGSTAmt, SGSTAmt, TotalAmt, BenefName, Narration, Stage, EntryDate, Mid, SystemDate) " +
                  "Values('" + TransType + "', '" + conn.ConvertDate(IssueDate) + "', '" + DrBrCode + "', '" + DrGlCode + "', '" + DrPrCode + "', '" + DrAccNo + "', " +
                  "'" + ChqAmt + "', Right('000000' + Rtrim('" + ChqNo + "'), 6), '" + CustNo + "', '" + CustName + "', '" + CrBrCode + "', '" + CrGlCode + "', "+
                  "'" + CrPrCode + "', '" + PONum + "', '" + conn.ConvertDate(PODate) + "', " + SerAmt + ", '" + CGST + "', '" + SGST + "', '" + TotAmt + "', "+
                  "'" + BenefName + "', '" + Narration + "', '1001', '" + conn.ConvertDate(EDate) + "', '" + Mid + "', GetDate())";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int AuthorizePayOrder(string BrCode, string SrNumber, string SetNo, string Mid)
    {
        try
        {
            sql = "Update AVS3000 Set Stage = '1003', SetNo = '" + SetNo + "', Vid = '" + Mid + "' " +
                  "Where IssueBrCode = '" + BrCode + "' And SrNumber = '" + SrNumber + "' And Stage = '1001'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DeletePayOrder(string BrCode, string SrNumber, string Mid)
    {
        try
        {
            sql = "Update AVS3000 Set Stage = '1004', Cid = '" + Mid + "' " +
                  "Where IssueBrCode = '" + BrCode + "' And SrNumber = '" + SrNumber + "' And Stage = '1001'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
}