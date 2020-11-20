using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsSBIntCalculation
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "", sResult = "";
    string TableName = "";
    int Result = 0;

	public ClsSBIntCalculation()
	{
		
	}

    public string GetBranchName(string BrCode)
    {
        try
        {
            sql = "Select MidName From BankName With(NoLock) Where BrCd = '" + BrCode + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
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

    public int CalculateSBInt(string BrCode, string PrCode, string FDate, string TDate, string Mid)
    {
        try
        {
            sql = "Exec SB_INTCalculation @BrCode = '" + BrCode + "', @PrCode = '" + PrCode + "', @FrDate = '" + conn.ConvertDate(FDate) + "', @ToDate = '" + conn.ConvertDate(TDate) + "', @Mid = '" + Mid + "', @sFlag = 'Calc'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    //public DataSet GetSBIntCal(string BrCode, string PrCode, string FDate, string TDate, string Mid)
    //{
    //    try
    //    {
    //        sql = "Exec SB_INTCalculation @BrCode = '" + BrCode + "', @PrCode = '" + PrCode + "', @FrDate = '" + conn.ConvertDate(FDate) + "', @ToDate = '" + conn.ConvertDate(TDate) + "', @Mid = '" + Mid + "', @sFlag = 'Report'";
    //        DT = new DataTable();
    //        DS = new DataSet();
    //        DT = conn.GetDatatable(sql);

    //        if (DT.Rows.Count > 0)
    //            DS.Tables.Add(DT);
    //        else
    //            DS = null;
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionLogging.SendErrorToText(Ex);
    //    }
    //    return DS;
    //}
    public DataSet GetSBIntCal(string BrCode, string PrCode, string FDate, string TDate, string Mid)
    {
        try
        {
            sql = "Exec SB_INTCalculation @BrCode = '" + BrCode + "', @PrCode = '" + PrCode + "', @FrDate = '" + conn.ConvertDate(FDate) + "', @ToDate = '" + conn.ConvertDate(TDate) + "', @Mid = '" + Mid + "', @sFlag = 'Report'";
            DT = new DataTable();
            DS = new DataSet();
            DT = conn.GetDatatable(sql);

            if (DT.Rows.Count > 0)
                DS.Tables.Add(DT);
            else
                DS = null;
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DS;
    }


    public DataTable GetSBIntCalForSingleAccNo(string BrCode, string PrCode, string FDate, string TDate, string Mid,string AccNo)
    {
        try
        {
            DT = new DataTable();

            sql = "Exec SB_INTCalculationAccWise @BrCode = '" + BrCode + "', @PrCode = '" + PrCode + "', @FrDate = '" + conn.ConvertDate(FDate) + "', @ToDate = '" + conn.ConvertDate(TDate) + "', @Mid = '" + Mid + "', @sFlag = 'CALC',@AccNo='"+AccNo+"'";
           
            DT = conn.GetDatatable(sql);
           
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }



 

}