using System;
using System.Data;
/// <summary>
/// Summary description for ClsAVS5152
/// </summary>
public class ClsAVS5152
{

    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    DataSet DS = new DataSet();
    string sql = "", sResult = "";
    string TableName = "";
    int Result = 0;
	public ClsAVS5152()
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

    public DataSet TrailReport(string Flag, string BrCode, string PrCode, string FDate, string TDate, string Flag1, string FlagValue, string Flag2, string Mid)
    {
        try
        {
            sql = "Exec ISP_AVS0195 @sFlag='" + Flag + "', @BrCode = '" + BrCode + "', @ProdCode = '" + PrCode + "', @FDate = '" + conn.ConvertDate(FDate) + "', @TDate = '" + conn.ConvertDate(TDate) + "', " +
                  "@sFlag1 = '" + Flag1 + "', @FlagValue = '" + FlagValue + "', @sFlag2 = '" + Flag2 + "', @Mid = '" + Mid + "'";
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

    public DataTable ChkAlrdyPosted(string BrCode, string SubGlCode, string FDate, string TDate)
    {
        try
        {
            string[] TD = FDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "Select Distinct SetNo, ConVert(VarChar(10), EntryDate, 103) As EntryDate From " + TableName + " Where BrCd = '" + BrCode + "' " +
                  "And SubGlCode = '" + SubGlCode + "' And EntryDate Between '" + conn.ConvertDate(FDate) + "' And '" + conn.ConvertDate(TDate) + "' " +
                  "And TrxType = '2' And Particulars Like 'Penal apply form%' And Stage <> '1004' ";
            DT = conn.GetDatatable(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return DT;
    }

    public string ApplyInterest(string Flag, string BrCode, string PrCode, string FDate, string TDate, string Flag1, string FlagValue, string Flag2, string Mid)
    {
        try
        {
            sql = "Exec ISP_AVS0195 @sFlag='" + Flag + "', @BrCode = '" + BrCode + "', @ProdCode = '" + PrCode + "', @FDate = '" + conn.ConvertDate(FDate) + "', @TDate = '" + conn.ConvertDate(TDate) + "', " +
                  "@sFlag1 = '" + Flag1 + "', @FlagValue = '" + FlagValue + "', @sFlag2 = '" + Flag2 + "', @Mid = '" + Mid + "'";
            sResult = conn.sExecuteScalar(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return sResult;
    }

}