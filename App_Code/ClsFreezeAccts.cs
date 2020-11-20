using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;

public class ClsFreezeAccts
{
    DbConnection conn = new DbConnection();
    string sql = "";
    int Result = 0;

	public ClsFreezeAccts()
	{

	}

    public string GetAccType(string AccT, string BRCD)
    {
        try
        {
            sql = "SELECT GLNAME FROM GLMAST WHERE SUBGLCODE='" + AccT + "' AND BRCD='" + BRCD + "'";
            AccT = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return AccT;
    }

    public DataTable GetCustName(string AccT, string AccNo, string BRCD)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "SELECT M.CUSTNAME,AC.OPENINGDATE FROM MASTER M INNER JOIN AVS_ACC AC ON AC.CUSTNO=M.CUSTNO AND M.BRCD=AC.BRCD WHERE AC.ACCNO='" + AccNo + "' AND AC.SUBGLCODE='" + AccT + "' AND AC.BRCD='" + BRCD + "'";

            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public int FreezeAccInst(string BrCode, string PrCode, string AccNo, string FreezType, double Amount, string Reason, string Mid, string EDate)
    {
        int RM;
        try
        {
            sql = "Exec SP_FreezUnFreezAccount @BrCode = '" + BrCode + "', @SubGlCode = '" + PrCode + "', @AccNo = '" + AccNo + "', @TrxType = '" + FreezType + "', @Amount = '" + Amount + "', @Reason = '" + Reason + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Mid = '" + Mid + "', @PcName = '" + conn.PCNAME().ToString() + "', @Flag = 'Freez'";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return RM = 0;
        }
        return RM;
    }

    public int FreezeAccAth(string BrCode, string PrCode, string AccNo, string Mid, string EDate)
    {
        int RM;
        try
        {
            sql = "Exec SP_FreezUnFreezAccount @BrCode = '" + BrCode + "', @SubGlCode = '" + PrCode + "', @AccNo = '" + AccNo + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Mid = '" + Mid + "', @Flag = 'AT',@PcName=''";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return RM = 0;
        }
        return RM;
    }

    public int FreezeAccDel(string BrCode, string PrCode, string AccNo, string Mid, string EDate)
    {
        int RM;
        try
        {
            sql = "Exec SP_FreezUnFreezAccount @BrCode = '" + BrCode + "', @SubGlCode = '" + PrCode + "', @AccNo = '" + AccNo + "', @EntryDate = '" + conn.ConvertDate(EDate).ToString() + "', @Mid = '" + Mid + "', @Flag = 'UFreez',@PcName=''";
            RM = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return RM = 0;
        }
        return RM;
    }

    public string GetStage(string BrCode, string PrCode, string AccNo)
    {
        try
        {
            sql = "Select Stage From Avs_FreezAccDetails Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "'";
            BrCode = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BrCode;
    }

    public string ChkExists(string BrCode, string PrCode, string AccNo)
    {
        try
        {
            sql = "Select Distinct 1 From Avs_FreezAccDetails Where BrCd = '" + BrCode + "' And SubGlCode = '" + PrCode + "' And AccNo = '" + AccNo + "' And Stage <> '1004'";
            BrCode = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return BrCode;
    }

    public DataTable GetInfo(string BrCode, string PrCode, string AccNo)
    {
        DataTable DT = new DataTable();
        try
        {
            sql = "Exec SP_FreezUnFreezAccount @BrCode = '" + BrCode + "', @SubGlCode = '" + PrCode + "', @AccNo = '" + AccNo + "', @Flag = 'VW',@PcName=''";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
            return null;
        }
        return DT;
    }

    public int BindGrid(GridView Gview, string BrCode)
    {
        try
        {
            sql = "Select Convert(VarChar(10), SubGlCode)+'_'+Convert(VarChar(10), AccNo) As Id, * From Avs_FreezAccDetails Where BrCd = '" + BrCode + "' And Stage Not In ('1003','1004')";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int BindGrid1(GridView Gview, string BrCode)
    {
        try
        {
            sql = "Select Convert(VarChar(10), SubGlCode)+'_'+Convert(VarChar(10), AccNo) As Id, * From Avs_FreezAccDetails Where BrCd = '" + BrCode + "' And Stage = '1003'";
            Result = conn.sBindGrid(Gview, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
}