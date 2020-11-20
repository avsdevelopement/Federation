using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Data;
/// <summary>
/// Summary description for ClsAVS5137
/// </summary>
public class ClsAVS5137
{

    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sResult = "";
    int Result = 0;
	public ClsAVS5137()
	{
		//
		// TODO: Add constructor logic here
		//
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

            if ((sResult == null) || (sResult == ""))
                sResult = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetMaxIno(string BrCode)
    {
        try
        {
            sql = "Select IsNull(Max(cast(StartInsNo As BigInt)), 0) + 1 As MaxInstruNo From AVS_InstruStockMaster " +
                  "Where BrCd = '" + BrCode + "' And InsType = '0' And Status In (0, 1, 2, 3, 4, 5, 6) And Stage <> '1004'";
            sResult = conn.sExecuteScalar(sql);

            if ((sResult == null) || (sResult == ""))
                sResult = "";
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string CheckExists(string BrCode, string PrCode, string ChequeNo, string sFlag)
    {
        try
        {
            if (sFlag.ToString() == "1")
                sql = "Select Count(1) As MaxInstruNo From AVS_InstruStockMaster Where BrCd = '" + BrCode + "' And InsType = '0' " +
                      "And StartInsNo = Right('000000' + Rtrim('" + ChequeNo + "'), 6) And Status In (0, 1, 2, 3, 4, 5, 6) And Stage <> '1004' ";
            else if (sFlag.ToString() == "2")
                sql = "Select Count(1) As MaxInstruNo From AVS_InstruStockMaster Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' " +
                      "And StartInsNo = Right('000000' + Rtrim('" + ChequeNo + "'), 6) And Stage <> '1004'";

            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataTable BindStockGrid(string BrCode, string EDate)
    {
        try
        {
            sql = "Select C.ID, C.BrCd, G.GlCode, C.SubGlCode, G.GlName, C.InsType, ConVert(VarChar(10), C.IssuedDate, 103) As IssuedDate, " +
                  "C.BookSize, C.NoOfBooks, C.StartInsNo, C.Remarks, C.Mid, U.LoginCode As Maker " +
                  "From AVS_InstruStockMaster C With(NoLock) " +
                  "Left Join GlMast G With(NoLock) On C.BrCd = G.BrCd And C.SubGlCode = G.SubGlCode " +
                  "Left Join UserMaster U With(NoLock) On C.Mid = U.PERMISSIONNO " +
                  "Where C.BrCd = '" + BrCode + "' And C.Stage Not In (1003, 1004) Order By C.ID";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int AuthorizeStock(string BrCode, string VerifyMid)
    {
        try
        {
            sql = "Update AVS_InstruStockMaster Set Stage = '1003', Vid = '" + VerifyMid + "' " +
                 "Where BrCd = '" + BrCode + "' And InsType = '0' And Stage Not In (1003, 1004) ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int DeleteStock(string BrCode, string VerifyMid)
    {
        try
        {
            sql = "Update AVS_InstruStockMaster Set Stage = '1004', Cid = '" + VerifyMid + "' " +
                 "Where BrCd = '" + BrCode + "' And InsType = '0' And Stage Not In (1003, 1004) ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int BankChequeStock(string BrCode,string Bankcd, string NoOfBooks, string BookSize, string SInstNo, string EInstNo, string IssueDate,
       string EDate, string Remark, string Mid)
    {
        try
        {
            for (double i = 1; i <= Convert.ToDouble(Convert.ToDouble(NoOfBooks) * Convert.ToDouble(BookSize)); i++)
            {
                sql = "Insert Into AVS_InstruStockMaster(BRCD,SubGlCode, CreateDate, InsType, NoOfBooks, Booksize, StartInsNo, IssuedDate, Remarks, Status, MID, Stage, RTime) " +
                      "Values('" + BrCode + "','" + Bankcd + "', '" + conn.ConvertDate(EDate) + "', '0', '" + NoOfBooks + "', '" + BookSize + "', Right('000000' + Rtrim('" + SInstNo + "'), 6), " +
                      "'" + conn.ConvertDate(IssueDate) + "', '" + Remark + "', '1', '" + Mid + "', '1001', GetDate())";
                Result = conn.sExecuteQuery(sql);
                SInstNo = Convert.ToInt32(Convert.ToInt32(SInstNo) + 1).ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int ChequeStock(string BrCode, string NoOfBooks, string BookSize, string SInstNo, string EInstNo, string IssueDate,
        string EDate, string Remark, string Mid)
    {
        try
        {
            for (double i = 1; i <= Convert.ToDouble(Convert.ToDouble(NoOfBooks) * Convert.ToDouble(BookSize)); i++)
            {
                sql = "Insert Into AVS_InstruStockMaster(BRCD, CreateDate, InsType, NoOfBooks, Booksize, StartInsNo, IssuedDate, Remarks, Status, MID, Stage, RTime) " +
                      "Values('" + BrCode + "', '" + conn.ConvertDate(EDate) + "', '0', '" + NoOfBooks + "', '" + BookSize + "', Right('000000' + Rtrim('" + SInstNo + "'), 6), " +
                      "'" + conn.ConvertDate(IssueDate) + "', '" + Remark + "', '1', '" + Mid + "', '1001', GetDate())";
                Result = conn.sExecuteQuery(sql);
                SInstNo = Convert.ToInt32(Convert.ToInt32(SInstNo) + 1).ToString();
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int LooseChqStock(string BrCode, string PrCode, string AccNo, string SInstNo, string EInstNo, string IssueDate, string EDate, string Remark, string Mid)
    {
        try
        {
            sql = "Insert Into AVS_InstruStockMaster(BRCD, CreateDate, SubGlCode, AccNo, InsType, NoOfBooks, Booksize, StartInsNo, IssuedDate, Remarks, Status, MID, Stage, RTime) " +
                  "Values('" + BrCode + "', '" + conn.ConvertDate(EDate) + "', '" + PrCode + "', '" + AccNo + "', '1', '1', '1', Right('000000' + Rtrim('" + SInstNo + "'), 6), " +
                  "'" + conn.ConvertDate(IssueDate) + "', '" + Remark + "', '1', '" + Mid + "', '1003', GetDate())";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int ChequeIssue(string BrCode, string PrCode, string AccNo, string SInstNo, string IssueDate, string NoOfBooks, string BookSize, string EDate, string Remark, string Mid)
    {
        try
        {
            sql = "Insert Into AVS_InstruIssueMaster(BRCD, EntryDate, SubGlCode, AccNo, InsType, NoOfBook, BookSize, InstrumentNo, InstrumentDate, Status, Stage, RTime, MID, Remark) " +
                  "Values ('" + BrCode + "', '" + conn.ConvertDate(EDate) + "', '" + PrCode + "', '" + AccNo + "', '1', '" + NoOfBooks + "', '" + BookSize + "', " +
                  "Right('000000' + Rtrim('" + SInstNo + "'), 6), '" + conn.ConvertDate(IssueDate) + "', '1', '1003', GetDate(), '" + Mid + "', '" + Remark + "') ";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
}