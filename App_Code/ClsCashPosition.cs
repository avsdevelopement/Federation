using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ClsCashPosition
/// </summary>
public class ClsCashPosition
{
    DbConnection conn = new DbConnection();
    DataTable DT = new DataTable();
    int Result;
    string sql;
    public ClsCashPosition()
    {
    }

    public int UpdateAllVault(string BrCode, string EDate, string Mid)
    {
        try
        {
            sql = "Update AVS5011 Set NO_OF_NOTES = '0', TOTAL_VALUE = '0', CID = '" + Mid + "', ENTRYDATE = '" + conn.ConvertDate(EDate).ToString() + "' Where BrCd = '" + BrCode + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

    public int UpdateVaultBalance(string BrCode, string EDate, double TwoThousand,double Thousand, double FiveHundred, double TwoHundred, double Hundred, double Fifty, double Twenty, double Ten, double Five, double Two, double One, string Mid)
    {
        try
        {
            sql = "Exec D_CashPosition @BrCode = '" + BrCode + "', @EDate = '" + conn.ConvertDate(EDate).ToString() + "', @TwentyThous = '" + TwoThousand + "', @Thous='" + Thousand + "', @FiveHund = '" + FiveHundred + "', @TwoHundred = '" + TwoHundred + "', @Hundred = '" + Hundred + "', @Fifty = '" + Fifty + "', @Twenty = '" + Twenty + "', @Ten = '" + Ten + "', @Five = '" + Five + "', @Two = '" + Two + "', @One = '" + One + "', @Mid = '" + Mid + "'";
            Result = conn.sExecuteQuery(sql);
        }
        catch (Exception Ex)
        {
            ExceptionLogging.SendErrorToText(Ex);
        }
        return Result;
    }

   

}