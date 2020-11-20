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

public class ClsMultiVoucher
{
    string sql, sResult, TableName, sqlc, sqld;
    string sQuery = "";
    DbConnection conn = new DbConnection();
    int Result = 0;
    DataTable DT = new DataTable();
    public ClsMultiVoucher()
    {

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

    public string CheckMultiValidation(string ProdCode, string BRCD)
    {
        string No = "";
        try
        {
            sql = "select TRFDR from GLMAST where SUBGLCODE='" + ProdCode + "' and BRCD='" + BRCD + "'";
            No = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return No;
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
            sql = "SELECT M.CUSTNAME+'_'+CONVERT(VARCHAR(10),AC.CUSTNO) CUSTNAME FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO = M.CUSTNO WHERE AC.ACCNO='" + ACCNO + "' AND AC.SUBGLCODE='" + GLCODE + "' AND AC.BRCD='" + BRCD + "'";

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
            //sql = "Select Abs(IsNull(Sum(A.Credit), 0)) As CREDIT, Abs(IsNull(Sum(A.Debit), 0)) As DEBIT From( " +
            //      "Select (Case When TRXTYPE = '1' Then Sum(Amount) Else '0' End) As Credit, " +
            //      "(Case When TRXTYPE = '2' Then Sum(Amount) Else '0' End) As Debit From Avs_TempMultiTransfer " +
            //      "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '0' And Mid = '" + Mid + "' And PmtMode <> 'TR_INT' " +
            //      "Group By TrxType)A ";
            //DT = new DataTable();
            //DT = conn.GetDatatable(sql);

            sql = "Select Abs(IsNull(Sum(A.Credit), 0)) As CREDIT, Abs(IsNull(Sum(A.Debit), 0)) As DEBIT From( " +
                      "Select (Case When TRXTYPE = '1' Then Sum(Amount) Else '0' End) As Credit, " +
                      "(Case When TRXTYPE = '2' Then Sum(Amount) Else '0' End) As Debit From Avs_TempMultiTransfer " +
                      "Where BrCd = '" + BrCode + "' And PmtMode <> 'TR-INT' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Subglcode not In (Select ListValue From Parameter Where BrCd = '" + BrCode + "' And ListField = 'Net Paid')  And SetNo = '0' And Mid = '" + Mid + "' And PmtMode <> 'TR_INT' " +
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
            sQuery = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return sQuery;
        }
        return sQuery;
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

    public int InsertIntoTable(string BrCode, string CustNo, string GlCode, string SubGlCode, string AccNo, string CustName, string Amount, string TrxType, string Activity, string PmtMode, string SetNo, string Perticulars, string Perticulars2, string ChkNo, string ChkDate, string EDate, string Mid)
    {
        try
        {
            sql = "Insert Into Avs_TempMultiTransfer (BrCd, CustNo, CustName, GlCode, SubGlCode, AccNo, Particulars, Particulars2, Amount, TrxType, Activity, PmtMode, SetNo, RefId, InstNo, InstDate, EntryDate, SystemDate, Mid) " +
                  "VALUES ('" + BrCode + "','" + CustNo + "','" + CustName + "', '" + GlCode + "','" + SubGlCode + "','" + AccNo + "','" + Perticulars + "','" + Perticulars2 + "','" + Amount + "','" + TrxType + "','" + Activity + "','" + PmtMode + "', '" + SetNo + "','0','" + ChkNo + "','" + ChkDate + "', '" + conn.ConvertDate(EDate).ToString() + "', GetDate(), '" + Mid + "')";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
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
            sql = "SELECT ID, BrCd, CustNo, CustName, GlCode, SubGlCode, AccNo, Particulars, Particulars2, Amount, TrxType, Activity, PmtMode, SetNo, RefId, InstNo, Convert(VarChar(10), InstDate, 103) As InstDate, EntryDate, Mid "+
                  "From Avs_TempMultiTransfer Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' And SetNo = '0' Order By SetNo";
            DT = new DataTable();
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string GetTransDetails_Activity(string BrCode, string EDate, string Mid)
    {
        string GLCODE = "";
        try
        {
            sql = "SELECT Top 1 Activity " +
                  "From Avs_TempMultiTransfer Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And Mid = '" + Mid + "' And SetNo = '0' Order By Activity,SetNo";
            GLCODE = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return GLCODE;
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
            sQuery = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Convert.ToDouble(sQuery);
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

    public DataTable GetLoanTotalAmount(string BrCode, string PrCode, string AccNo, string EDate,string FL="CL") // Optional Parameter added by Abhishek for Pen Requirement 04/09/2017
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
    public int Getinfotable(GridView Gview, string smid, string sbrcd, string EDT, string paymst)
    {
        try
        {
            string[] TD = EDT.Replace("12:00:00 AM", "").Split('/');
            TableName = "AVSM_" + TD[2].ToString().Trim() + TD[1].ToString().Trim();

            sql = "Select ConVert(VarChar(10), IsNull(A.SETNO,'0'))+'_'+ConVert(VarChar(10), IsNull(A.Amount,'0'))+'_'+ " +
                  "ConVert(VarChar(10), IsNull(A.SUBGLCODE,'0')) +'_'+ConVert(VarChar(10), IsNull(A.ACCNO,'0')) Dens,A.SETNO SETNO, " +
                  "A.SUBGLCODE AT, A.ACCNO ACNO, M.CUSTNAME CUSTNAME, A.AMOUNT,  A.PARTICULARS PARTICULARS, UM.USERNAME MAKER " +
                  "From " + TableName + " A " +
                  "Inner Join USERMASTER UM ON UM.PERMISSIONNO=A.MID " +
                  "Left Join AVS_ACC ACC ON ACC.ACCNO=A.ACCNO AND ACC.BRCD = A.BRCD AND A.SUBGLCODE=ACC.SUBGLCODE " +
                  "Left Join MASTER M ON M.CUSTNO = ACC.CUSTNO " +
                  "Where A.BRCD= '" + sbrcd + "' AND A.STAGE = '1001'  AND A.ACTIVITY='7' AND A.ENTRYDATE = '" + conn.ConvertDate(EDT) + "' " +
                  "and A.PAYMAST= '" + paymst + "' " +
                  "Order By A.SETNO,A.SCROLLNO";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

}