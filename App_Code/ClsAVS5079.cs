using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsAVS5079
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sResult = "";
    int Result = 0;

	public ClsAVS5079()
	{
		
	}

    public string LastProdNo(string BrCode, string PrCode)
    {
        try
        {
            sql = "Select Cast(LastNo As BigInt)+1 From GlMast Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' ";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetProductName(string BrCode, string ProdCode)
    {
        try
        {
            sql = "Select (IsNull(GlName, '') +'_'+ ConVert(VarChar(10), GlCode) +'_'+ ConVert(VarChar(10), SubGlCode)) As Name " +
                  "From GlMast Where BrCd = '" + BrCode + "' And SubGlCode ='" + ProdCode + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public string GetAccountName(string BrCode, string ProdCode, string AccNo)
    {
        try
        {
            sql = "Select (M.CustName+'_'+ ConVert(VarChar(10), ConVert(BigInt, A.AccNo))+'_'+ ConVert(VarChar(10), ConVert(BigInt, A.CustNo))) As Name " +
                  "From Avs_Acc A With(NoLock) " +
                  "Left Join Master M With(NoLock) On A.CustNo = M.CustNo " +
                  "Where A.BrCd = '" + BrCode + "' And A.SubGlCode = '" + ProdCode + "' And A.AccNo = '" + AccNo + "' And A.Stage <> '1004'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public DataTable GetAccDetails(string BrCode, string PrCode, string AccNo)
    {
        try
        {
            sql = "Select Acc_Status, ConVert(VarChar(10), OpeningDate, 103) As OpenDate From AVS_Acc "+
                  "Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return DT;
    }

    public string CheckAccExists(string BrCode, string ProdCode, string AccNo)
    {
        try
        {
            sql = "Select A.AccNo From Avs_Acc A With(NoLock) Where A.BrCd = '" + BrCode + "' And A.SubGlCode = '" + ProdCode + "' And A.AccNo = '" + AccNo + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

    public int TransferAcc(string BrCode, string FGLCode, string FProdCode, string FAccNo, string TGlCode, string TProdCode, string TAccNo, string OpenDate, string EDate, string Mid)
    {
        try
        {
            sql = "Insert Into AVS5079(BRCD, GlCode, SubGlCode, CustNo, AccNo, OpeningDate, AccStatus, Acc_Type, Opr_Type, MinorAcc, " +
                "M_CustNo, M_OprName, Remark1, Remark2, LastIntDate, DDSLastIntPro, EntryDate, Stage, Mid, FBrCd, FSubGlCode, FAccNo, SystemDate) " +
                "Select BRCD, '" + TGlCode + "', '" + TProdCode + "', CustNo, '" + TAccNo + "', '" + conn.ConvertDate(OpenDate).ToString() + "', Acc_Status, Acc_Type, Opr_Type, Minor_Acc, " +
                "M_CustNo, M_OprName, Remark1, Remark2, LastIntDt, DDSLastIntPro, '" + conn.ConvertDate(EDate).ToString() + "', '1001', '" + Mid + "', '" + BrCode + "', '" + FProdCode + "', '" + FAccNo + "', GetDate() " +
                "From Avs_Acc Where BrCd = '" + BrCode + "' And SubGlCode = '" + FProdCode + "' And AccNo = '" + FAccNo + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public void UnAuthorised(GridView Gview, string BrCode, string EDate, string Stage)
    {
        try
        {
            sql = "Select A.ID, A.SubGlCode, A.CustNo, A.AccNo, M.CustName, ConVert(VarChar(10), A.OpeningDate, 103) As OpenDate From AVS5079 A " +
                  "Left Join Master M With(NoLock) On A.CustNo = M.CustNo "+
                  "Where A.BrCd = '" + BrCode + "' And A.EntryDate = '" + conn.ConvertDate(EDate) + "' And A.Stage = '" + Stage + "'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
    }

    public DataTable GetAccountData(string BrCode, string UniqueId)
    {
        try
        {
            sql = "Select * From AVS5079 Where BrCd = '" + BrCode + "' And Id = '" + UniqueId + "'";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int ForAuthorised(string BrCode, string FGLCode, string FProdCode, string FAccNo, string TGlCode, string TProdCode, string TAccNo, string EDate, string Mid)
    {
        try
        {
            sql = "Exec ISP_AVS0117 @BrCode = '" + BrCode + "', @FGLCode = '" + FGLCode + "', @FProdCode = '" + FProdCode + "', @FAccNo = '" + FAccNo + "', @TGlCode = '" + TGlCode + "', @TProdCode = '" + TProdCode + "', @TAccNo = '" + TAccNo + "', @EDate = '" + conn.ConvertDate(EDate) + "', @Mid = '" + Mid + "', @Flag = 'AT'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

   

}