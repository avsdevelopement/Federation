using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class ClsAVS5086
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    string sql = "", sResult = "";
    double OpenBalance = 0;
     double OpenBalanceCr = 0;
    double OpenBalanceDr = 0;
    string TableName = "";
    int Result = 0;
    int Res = 0;

	public ClsAVS5086()
	{
		
	}

    public string GetBranchName(string brcd)
    {
        try
        {
            sql = "select MIDNAME from BANKNAME where BRCD='" + brcd + "'";
            sql = conn.sExecuteScalar(sql);
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return sql;
    }

   

  

    public double OpeningBalCr(string BrCode, string PrCode, string AsOnDate)
    {
        try
        {
            sql = "Select IsNull(Sum(Case When Trxtype = '1' Then Amount Else 0 End), 0) From Avs_Reco " +
                  "Where BrCd = '" + BrCode + "' And SubGlcode = '" + PrCode + "' And RCType='RC' And FundingDate <= '" + conn.ConvertDate(AsOnDate).ToString() + "' And Stage <> '1004' ";
            OpenBalanceCr = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return OpenBalanceCr;
    }
    public double OpeningBalDr(string BrCode, string PrCode, string AsOnDate)
    {
        try
        {
            sql = "Select IsNull(Sum(Case When Trxtype = '2' Then Amount Else 0 End), 0) From Avs_Reco " +
                   "Where BrCd = '" + BrCode + "' And SubGlcode = '" + PrCode + "' And RCType='RC' And FundingDate <= '" + conn.ConvertDate(AsOnDate).ToString() + "' And Stage <> '1004' ";
            OpenBalanceDr = Convert.ToDouble(conn.sExecuteScalar(sql));
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return OpenBalanceDr;
    }

  

    public int GetAllTransaction_M(GridView GRD, string BrCode, string PrCode, string FrDate, string Todate, string Flag)
    {
        try
        {
            sql = "Exec ISP_AVS0125 @BrCode = '" + BrCode + "', @ProdCode = '" + PrCode + "', @FromDate = '" + conn.ConvertDate(FrDate) + "', @ToDate = '" + conn.ConvertDate(Todate) + "', @Flag = '" + Flag + "'";
            Res = conn.sBindGrid(GRD, sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Res;
    }

    public int InsertTrans(string BrCode, string PrCode, string EDate, string SetNo, string ScrollNo, string sFlag, string Mid,string EntryDate)
    {
        try
        {
            string[] TD = EDate.Split('/');
            TableName = "AVSM_" + TD[2].ToString() + TD[1].ToString();

            sql = "Select Count(1) As TotCnt From AVS_Reco Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' " +
                  "And SetNo = '" + SetNo + "' And SubGlCode = '" + PrCode + "' And ScrollNo = '" + ScrollNo + "' ";
            Result = Convert.ToInt32(conn.sExecuteScalar(sql));

            if (Result == 0)
            {
                sql = "Insert Into AVS_Reco(BRCD, EntryDate, PostingDate, FundingDate, GlCode, SubGlCode, AccNo, PartiCulars, PartiCulars2, Amount, Amount_1, Amount_2, TrxType, " +
                      "Activity, PmtMode, SetNo, ScrollNo, InstrumentNo, InstrumentDate, Stage, RTime, MID, CID, VID, PcMac, PayMast, CustNo, CustName, RCType, SystemDate) " +
                      "Select BRCD, EntryDate, PostingDate, '"+conn.ConvertDate(EntryDate).ToString()+"', GlCode, SubGlCode, AccNo, PartiCulars, PartiCulars2, Amount, Amount_1, Amount_2, TrxType, " +
                      "Activity, PmtMode, SetNo, ScrollNo, InstrumentNo, InstrumentDate, Stage, RTime, MID, CID, VID, PcMac, PayMast, CustNo, CustName, '" + sFlag + "', SystemDate " +
                      "From " + TableName + " Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' " +
                      "And SubGlCode = '" + PrCode + "' And ScrollNo = '" + ScrollNo + "'";
                Result = conn.sExecuteQuery(sql);
            }
            else
            {
                string sResult = "";
                if (sFlag == "RP")
                {
                    sql = "Select RCType From AVS_Reco Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' " +
                 "And SetNo = '" + SetNo + "' And SubGlCode = '" + PrCode + "' And ScrollNo = '" + ScrollNo + "'";
                    sResult = conn.sExecuteScalar(sql);
                    if (sResult != "RC")
                    {
                        sql = "UPDATE AVS_Reco SET RCType='" + sFlag + "',FundingDate='"+conn.ConvertDate(EntryDate)+"' WHERE BRCD = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' " +
                    "And SubGlCode = '" + PrCode + "' And ScrollNo = '" + ScrollNo + "'";
                        Result = conn.sExecuteQuery(sql);
                    }
                }
                else
                {
                    sql = "UPDATE AVS_Reco SET RCType='" + sFlag + "',FundingDate='" + conn.ConvertDate(EntryDate) + "' WHERE BRCD = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' And SetNo = '" + SetNo + "' " +
                   "And SubGlCode = '" + PrCode + "' And ScrollNo = '" + ScrollNo + "'";
                    Result = conn.sExecuteQuery(sql);
                }
               
            }
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int RemoveTrans(string BrCode, string PrCode, string EDate, string SetNo, string ScrollNo, string Mid)
    {
        try
        {
            sql = "Update AVS_Reco Set Stage = '1004', SystemDate = GetDate(), Vid = '"+ Mid +"' "+
                  "Where BrCd = '" + BrCode + "' And EntryDate = '" + conn.ConvertDate(EDate).ToString() + "' "+
                  "And SetNo = '" + SetNo + "' And SubGlCode = '" + PrCode + "' And ScrollNo = '" + ScrollNo + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }
   

}